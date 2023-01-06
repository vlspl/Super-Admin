using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Web.Services;
using DataAccessHandler;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using System.Data.SqlClient;
using System.Threading;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;

public partial class LabLogin : System.Web.UI.Page
{

    ClsLabLogin objLogin = new ClsLabLogin();
    MD5Hash objHash = new MD5Hash();
    DataAccessLayer DAL = new DataAccessLayer();


    int loginCounter = 0, wlPCounter = 0;
    string _username, _password, x, loginStatus;
    protected static string ReCaptcha_Key = "6Len5dQcAAAAAN-ueQezaQRAhHMgXXzBWMMnd5Mj";
    protected static string ReCaptcha_Secret = "6Len5dQcAAAAABG6u6m7gEKqN_gUShqTwfO9WmJb";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                Response.Redirect("Dashboard.aspx");
                // Response.Redirect("TestBookList.aspx");
                Session.Timeout = 100;
            }
        }
    }
    public bool ValidateReCaptcha(ref string errorMessage)
    {
        var gresponse = Request["g-recaptcha-response"];
        string secret = "6Len5dQcAAAAABG6u6m7gEKqN_gUShqTwfO9WmJb";//"6LcqFg8UAAAAAO_FQuzRejzk9fa004PO86sy8d0";
        string ipAddress = GetIpAddress();

        var client = new WebClient();

        string url = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0} &response={1}&remoteip={2}", secret, gresponse, ipAddress);

        var response = client.DownloadString(url);

        var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaResponse>(response);

        if (captchaResponse.Success)
        {
            return true;
        }
        else
        {
            var error = captchaResponse.ErrorCodes[0].ToLower();
            switch (error)
            {
                case ("missing-input-secret"):
                    errorMessage = "The secret key parameter is missing.";
                    break;
                case ("invalid-input-secret"):
                    errorMessage = "The given secret key parameter is invalid.";
                    break;
                case ("missing-input-response"):
                    errorMessage = "The g-recaptcha-response parameter is missing.";
                    break;
                case ("invalid-input-response"):
                    errorMessage = "The given g-recaptcha-response parameter is invalid.";
                    break;
                default:
                    errorMessage = "reCAPTCHA Error. Please try again!";
                    break;
            }

            return false;
        }
    }

    string GetIpAddress()
    {
        var ipAddress = string.Empty;

        if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
        {
            ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
        else if (!string.IsNullOrEmpty(Request.UserHostAddress))
        {
            ipAddress = Request.UserHostAddress;
        }

        return ipAddress;
    }

    public class ReCaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
    private void AuthenticateRequest(object obj, EventArgs ea)
    {

        HttpApplication objApp = (HttpApplication)obj;
        HttpContext objContext = (HttpContext)objApp.Context;
        // If user identity is not blank, pause for a random amount of time
        if (objApp.User.Identity.Name != "")
        {
            Random rand = new Random();
            Thread.Sleep(rand.Next(100, 200) * 1000);
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        
        string errorMessage = string.Empty;

       // bool isValidCaptcha = ValidateReCaptcha(ref errorMessage);

       // if (isValidCaptcha)
       // {
            _username = CryptoHelper.Encrypt(txtUserName.Value);
            _password = CryptoHelper.Encrypt(txtpassword.Value);
            loginStatus = getLoginStatus();
            if (loginStatus == "")
            {
                //wlPCounter++;
                //ViewState["loginCounter"]= (wlPCounter).ToString();
                // check the viewstate
                if (ViewState["Attempts"] != null)
                {
                    if (((int)(ViewState["Attempts"])) != 3)
                    {

                        ViewState["Attempts"] = Convert.ToInt32(ViewState["Attempts"]) + 1;

                    }
                    else
                    {
                        // loginStatus = "D";
                        lblMessage.InnerText = "You Have Entered Invalid Username or Password " + ViewState["Attempts"] + " times.Your Account Has been Deactivated.Please Contact your Administrator";
                        btnLogin.Enabled = false;
                    }
                }
                else
                {
                    ViewState["Attempts"] = Convert.ToInt32(ViewState["Attempts"]) + 1;

                }
            }
            if (loginStatus == "A")
            {
                using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("update UserLoginMaster set loginAttemptCounter='0' where UserName='" + _username + "' and Password='" + _password + "'", cn);

                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
                try
                {

                    x = CryptoHelper.Decrypt("bmUHvQaI4WPRW1FbPvwHJQ==");

                    Dictionary<string, string> returnValues = new Dictionary<string, string>();

                    SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserName",_username),
                new SqlParameter("@Password",_password)
            };
                    DataTable dt = DAL.ExecuteStoredProcedureDataTable("sp_LoginMaster", param);

                    if (dt.Rows.Count > 0)
                    {
                        string role = dt.Rows[0]["Role"].ToString();
                        if (role == "Lab")
                        {
                            returnValues = objLogin.LabUserDetails(dt.Rows[0]["UserId"].ToString());
                            if (returnValues["status"] == "active")
                            {
                                Session["loggedIn"] = "true";
                                Session["labId"] = returnValues["labId"];
                                Session["labCode"] = returnValues["labCode"];
                                Session["labUser"] = returnValues["labUser"];
                                Session["username"] = returnValues["username"];
                                Session["role"] = returnValues["role"];
                                Session["labUserId"] = returnValues["labUserId"];

                                Response.Cookies["loggedIn"].Value = "true";
                                Response.Cookies["labId"].Value = returnValues["labId"];
                                Response.Cookies["labCode"].Value = returnValues["labCode"];
                                Response.Cookies["labUser"].Value = returnValues["labUser"];
                                Response.Cookies["username"].Value = returnValues["username"];
                                Response.Cookies["role"].Value = returnValues["role"];
                                Response.Cookies["labUserId"].Value = returnValues["labUserId"];

                                Response.Cookies["loggedIn"].Expires = DateTime.Now.AddDays(1);
                                Response.Cookies["labId"].Expires = DateTime.Now.AddDays(1);
                                Response.Cookies["labCode"].Expires = DateTime.Now.AddDays(1);
                                Response.Cookies["labUser"].Expires = DateTime.Now.AddDays(1);
                                Response.Cookies["username"].Expires = DateTime.Now.AddDays(1);
                                Response.Cookies["role"].Expires = DateTime.Now.AddDays(1);
                                Response.Cookies["labUserId"].Expires = DateTime.Now.AddDays(1);

                                Response.Redirect("Dashboard.aspx", false);

                            }
                            else if (returnValues["status"] == "inactive")
                            {
                                lblMessage.InnerText = "Your account has not been activated yet.";
                            }
                            else if (returnValues["status"] == "null")
                            {
                                lblMessage.InnerText = "Invalid Username or Password";
                            }
                            else if (returnValues["status"] == "error")
                            {
                                lblMessage.InnerText = "Error occured";
                            }
                        }
                        else if (role == "Enterprise")
                        {
                            DataTable Empdt = objLogin.EnterpriseUserLoginDetails(dt.Rows[0]["UserId"].ToString());
                            if (Empdt.Rows.Count > 0)
                            {
                                if (Empdt.Rows[0]["IsActive"].ToString().ToLower() == "true")
                                {
                                    Session["loggedIn"] = "true";
                                    Session["OrgId"] = Empdt.Rows[0]["Org_Id"].ToString();
                                    Session["BranchId"] = Empdt.Rows[0]["Branch_ID"].ToString();
                                    Session["HRId"] = Empdt.Rows[0]["ID"].ToString();
                                    Session["Name"] = Empdt.Rows[0]["Name"].ToString();
                                    Session["role"] = Empdt.Rows[0]["Role"].ToString();
                                    Session["ProfilePic"] = Empdt.Rows[0]["ProfilePic"].ToString();

                                    Response.Cookies["loggedIn"].Value = "true";
                                    Response.Cookies["OrgId"].Value = Empdt.Rows[0]["Org_Id"].ToString();
                                    Response.Cookies["BranchId"].Value = Empdt.Rows[0]["Branch_ID"].ToString();
                                    Response.Cookies["HRId"].Value = Empdt.Rows[0]["ID"].ToString();
                                    Response.Cookies["Name"].Value = Empdt.Rows[0]["Name"].ToString();
                                    Response.Cookies["role"].Value = Empdt.Rows[0]["Role"].ToString();
                                    Response.Cookies["ProfilePic"].Value = Empdt.Rows[0]["ProfilePic"].ToString();

                                    Response.Cookies["loggedIn"].Expires = DateTime.Now.AddDays(1);
                                    Response.Cookies["OrgId"].Expires = DateTime.Now.AddDays(1);
                                    Response.Cookies["BranchId"].Expires = DateTime.Now.AddDays(1);
                                    Response.Cookies["HRId"].Expires = DateTime.Now.AddDays(1);
                                    Response.Cookies["Name"].Expires = DateTime.Now.AddDays(1);
                                    Response.Cookies["role"].Expires = DateTime.Now.AddDays(1);
                                    Response.Cookies["ProfilePic"].Expires = DateTime.Now.AddDays(1);

                                    Response.Redirect("~/ENTERPRISE/Dashboard.aspx");

                                }
                                else if (Empdt.Rows[0]["IsActive"].ToString().ToLower() == "false")
                                {
                                    lblMessage.InnerText = "Your account has not been activated yet.";
                                }
                                else
                                {
                                    lblMessage.InnerText = "Invalid Username or Password";
                                }
                            }
                            else
                            {
                                lblMessage.InnerText = "Error occured";
                            }
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        UpdateLoginCounter();
                        lblMessage.InnerText = "You Have Entered Invalid Username or Password '" + loginCounter + "' times";
                    }
                }
                catch (Exception ex)
                {
                    LogError.LoggerCatch(ex);
                }
            }
            else if (loginStatus == "D")
            {
                loginCounter = getLoginCounter();
                lblMessage.InnerText = "You Have Entered Invalid Username or Password '" + loginCounter + "' times.Your Account Has been Deactivated.Please Contact your Administrator";

            }
            else
            {
                UpdateLoginCounter();
                lblMessage.InnerText = "You Have Entered Invalid Username or Password '" + loginCounter + "' times";
            }
      //  }
      //  else
       // {
       //     lblMessage.InnerText = "Captcha Validation is Required.";
        //}

    }
    public void UpdateLoginCounter()
    {
        using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
        {
            cn.Open();
            //loginCounter++;
            // get login counter from database
            loginCounter = getLoginCounter();
            loginCounter++;
            SqlCommand cmd = new SqlCommand("update UserLoginMaster set loginAttemptCounter='" + loginCounter + "' where UserName='" + _username + "' or Password='" + _password + "'", cn);

            cmd.ExecuteNonQuery();
            cn.Close();
        }

        if (loginCounter >= 3)
        {
            using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                cn.Open();
                // loginCounter++;
                SqlCommand cmd = new SqlCommand("update UserLoginMaster set loginStatus='D' where UserName='" + _username + "' and Password='" + _password + "'", cn);

                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
    }

    public int getLoginCounter()
    {
        using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
        {
            cn.Open();
            SqlCommand SqlCmd = new SqlCommand("select loginAttemptCounter from UserLoginMaster where UserName='" + _username + "' or Password='" + _password + "'", cn);
            SqlDataAdapter da = new SqlDataAdapter(SqlCmd);
            //da.Fill(SqlCmd);
            //DatAdptr.Dispose();

            int result;
            result = Convert.ToInt16(SqlCmd.ExecuteScalar());
            return result;
            cn.Close();
        }
    }


    public string getLoginStatus()
    {
        using (SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
        {
            cn.Open();
            SqlCommand SqlCmd = new SqlCommand("select loginStatus from UserLoginMaster where UserName='" + _username + "' or Password='" + _password + "'", cn);
            SqlDataAdapter da = new SqlDataAdapter(SqlCmd);
            //da.Fill(SqlCmd);
            //DatAdptr.Dispose();

            String result;
            result = Convert.ToString(SqlCmd.ExecuteScalar());
            return result;
            cn.Close();
        }
    }


    [WebMethod]
    public static object ForgotPassword(string data)
    {
        DataAccessLayer DAL = new DataAccessLayer();
        int result = 1;
        string Data = data.Replace("!~^!", "¶");
        string[] DataArr = Data.Split('¶');
        ClsLabLogin objLogin = new ClsLabLogin();
        result = Convert.ToInt32((objLogin.mailPassword(DataArr[1])));
        return result;
    }
}
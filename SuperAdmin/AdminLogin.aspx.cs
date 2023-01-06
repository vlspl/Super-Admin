using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class AdminLogin : System.Web.UI.Page
{
    ClsAdminLogin objLogData = new ClsAdminLogin();
	 DBClass db = new DBClass();
    string strs = null;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["username"] = null;
            Session["password"] = null;
            Session["rollMasterId"]=null;
        }
    }

    protected void btn_insert(object sender, EventArgs e)
    {
        try
        {
            string user = CryptoHelper.Encrypt(txtuser.Value.ToString());
            string password = CryptoHelper.Encrypt(txtpassword.Value.ToString());
           ds = objLogData.AdminLogin(user, password);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = objLogData.AdminUserDetails(ds.Tables[0].Rows[0]["UserId"].ToString());
                //string role = ds.Tables[0].Rows[0]["Role"].ToString();
                //if (role.ToLower() == "admin")
                //{
                    Session["username"] = user.ToString();
                    Session["password"] = password.ToString();
                    Session["rollMasterId"] = db.getData("select rollMasterId from UserLoginMaster where UserName='" + user.ToString() + "' and Password='" + password.ToString() + "'");
                   
                    Response.Cookies["AdminId"].Value = dt.Rows[0]["sSuperAdminUserId"].ToString();
                    Response.Cookies["AdminUserName"].Value = dt.Rows[0]["sFullName"].ToString();
                    Response.Cookies["LoginId"].Value = ds.Tables[0].Rows[0]["Id"].ToString();
                    Response.Cookies["AdminId"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["AdminUserName"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["LoginId"].Expires = DateTime.Now.AddDays(1);
                    Response.Redirect("../SuperAdmin/Dash.aspx", false);
               // }
                //else if (role == "Backend")
                //{
                //    Response.Cookies["BackendId"].Value = dt.Rows[0]["sSuperAdminUserId"].ToString();
                //    Response.Cookies["AdminUserName"].Value = dt.Rows[0]["sFullName"].ToString();
                //    Response.Cookies["LoginId"].Value = ds.Tables[0].Rows[0]["Id"].ToString();
                //    Response.Cookies["BackendId"].Expires = DateTime.Now.AddDays(1);
                //    Response.Cookies["AdminUserName"].Expires = DateTime.Now.AddDays(1);
                //    Response.Cookies["LoginId"].Expires = DateTime.Now.AddDays(1);
                //    Response.Redirect("../BackOffice/dashboard.aspx", false);
                    
                //}
               
                
                txtuser.Value = "";
                txtpassword.Value = "";
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string user = CryptoHelper.Encrypt(txtForgotPasswordUserName.Text);
        string mailPassword = objLogData.mailPassword(user);

        if (mailPassword == "1")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Password has been mailed to your registered email id')", true);
        }
        else if (mailPassword == "0")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Username did not match')", true);
        }
        else if (mailPassword == "-1")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred')", true);
        }
    }

} 

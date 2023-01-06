using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using BitsBizLogic;
using DataAccessHandler;
using CrossPlatformAESEncryption.Helper;
using Validation;
using System.Net.Mail;
using System.Web.Configuration;



public partial class SuperAdmin_AddOrganization : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    string mailFrom, mailFrom_password;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Cookies["AdminId"].Value != null)
        {
           if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {

                    SqlParameter[] param = new SqlParameter[]
                     {
                          new SqlParameter ("@orgId",Request.QueryString["ID"])
                     };
                    DataTable dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetOrgnizationDetailById", param);
                    foreach (DataRow row in dt.Rows)
                    {

                        txtOrgName.Text = row["OrgName"].ToString();
                        txtAdress.Text = row["Address"].ToString();
                        txtmobilenumber.Text =CryptoHelper.Decrypt(row["Mobile"].ToString());
                        txtHRName.Text = row["HrName"].ToString();
                        txtEmail.Text = CryptoHelper.Decrypt(row["EmailId"].ToString());
                        txtOrgDetails.Text = row["Org_Details"].ToString();
                        string govType = row["govOrg"].ToString();
                       if (govType == "0")
                           drptype.Text = "Other";
                        else
                           drptype.Text = "Government";

  //txtPassword.Text=row[""].ToString();
                        //txtConfirmPassword.Text=row[""].ToString();

                    }
                    BtnSave.Visible = false;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                }
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx", false);
        }

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string Msg = "", type="";
        {
            string filename = Session.SessionID + OrgLogo.FileName;
            if (true)
            {
				 if (OrgLogo.HasFile)
				 {
                String fileextension = System.IO.Path.GetExtension(OrgLogo.FileName);
                if (fileextension.ToLower() != ".jpg" && fileextension.ToLower() != ".png" && fileextension.ToLower() != ".jpeg" && fileextension.ToLower() != ".gif" && fileextension.ToLower() != ".bmp")
                {
                    Msg = "Please Upload only jpg,png,jpeg,gif,bmp images only</br> ";
                }
                else
                {
                    OrgLogo.PostedFile.SaveAs(Server.MapPath("../images/Logo/" + filename));
                    filename = "../images/Logo/" + Session.SessionID + OrgLogo.FileName;
                }
				 }
                if (Ival.IsTextBoxEmpty(txtHRName.Text))
                {
                    Msg += "● Please Enter Valid HR Name";
                }
                if (Ival.IsTextBoxEmpty(txtOrgName.Text))
                {
                    Msg += "● Please Enter Valid Orgnization Name";
                }
                if (drptype.Text=="-Select Type Here-")
                {
                    Msg += "● Please Select Orgnization Type";
                }
                if (!Ival.IsTextBoxEmpty(txtEmail.Text))
                {
                    if (!Ival.IsValidEmailAddress(txtEmail.Text))
                    {
                        Msg += "● Please Enter Valid Email Id";
                    }
                }
                if (Ival.IsInteger(txtmobilenumber.Text))
                {
                    if (!Ival.MobileValidation(txtmobilenumber.Text))
                    {
                        Msg += "● Please Enter Valid Mobile Number";
                    }
                }
                else
                {
                    Msg += "● Please Enter Valid Mobile Number";
                }
                if (Msg.Length > 0)
                {
                    litErrorMessage.Text = ApplicationLogic.ErrorWarning(Msg);
                }
                else
                {
                    ///Submit Data To Database
                    string _password = CryptoHelper.Encrypt(txtPassword.Text);
                    string _EmailId = txtEmail.Text != "" ? CryptoHelper.Encrypt(txtEmail.Text) : "";
                    string _Mobile = txtmobilenumber.Text != "" ? CryptoHelper.Encrypt(txtmobilenumber.Text) : "";
                    if (drptype.Text == "Government")
                        type = "1";
                    else
                        type = "0";
                    SqlParameter[] param = new SqlParameter[]
                         {
                             new SqlParameter("@OrgName",txtOrgName.Text),
                             new SqlParameter("@OrgAddress",txtAdress.Text),
                             new SqlParameter("@Contact",_Mobile),
                             new SqlParameter("@Email",_EmailId),
                             new SqlParameter("@OrgDetails",txtOrgDetails.Text),
                             new SqlParameter("@OrgLogo",filename),
                              new SqlParameter("@govOrg",type),
                             new SqlParameter("@returnval",SqlDbType.Int)
                          };
                    int Result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOrgnization", param);
                    // Display Message
                    if (Result >= 1)
                    {
                        SqlParameter[] param1 = new SqlParameter[]
                         {
                             new SqlParameter("@Name",txtHRName.Text),
                             new SqlParameter("@Org_Id",Result),
                             new SqlParameter("@Branch_ID",""),
                             new SqlParameter("@Contact",_Mobile),
                             new SqlParameter("@Email",_EmailId),
                             new SqlParameter("@Role","CEO"),
                             new SqlParameter("@Password",""),
                             new SqlParameter("@returnval",SqlDbType.Int)
                          };
                        int Resultval = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOrgnizationUser", param1);

                        SqlParameter[] paramlab = new SqlParameter[]
                                             {
                                                new SqlParameter("@UserId",Resultval),
                                                new SqlParameter("@Mobile",_Mobile),
                                                new SqlParameter("@EmailId",_EmailId),
                                                new SqlParameter("@Role","Enterprise"),
                                                new SqlParameter("@Password",_password),
                                                new SqlParameter("@UserName",""), 
                                                  //   new SqlParameter("@rollMasterId", ""),
                                                new SqlParameter("@Returnval",SqlDbType.Int)
                                              //  new SqlParameter("@loginStatus","A"),
                                             };
                        int ResultVal1 = DAL.ExecuteStoredProcedureRetnInt("Sp_AddUserLoginCredentials", paramlab);
                        if (ResultVal1 == 1)
                        {
							 newWhatsapp wa = new newWhatsapp();
                            //wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
                            wa.sendWhatsappMsg_superadmin("+91" + txtmobilenumber.Text, "Org Welcome", txtOrgName.Text + ',' + txtEmail.Text + ',' + txtPassword.Text);
                            //litErrorMessage.Text = ApplicationLogic.SuccessMessage("● Saved Suceessfully.");
                            sendmail(txtEmail.Text, _password, txtHRName.Text, _Mobile);
                            //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"● Saved Successfully!\");", true);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Organization Added successfully');location.href='AddOrgBranch.aspx';", true);
                            
                        }
                        else if (Result == -2)
                        {
                            litErrorMessage.Text = ApplicationLogic.ErrorWarning("● User Already Exist!");
                        }
                    }
                    else if (Result == -2)
                    {
                        litErrorMessage.Text = ApplicationLogic.ErrorWarning("● Already Exist!");
                    }
                    else
                    {
                        litErrorMessage.Text = ApplicationLogic.ErrorWarning("● Something Went Wrong, Please try Again!");
                    }
                }
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@Id",Request.QueryString["ID"])
            };
        DAL.ExecuteStoredProcedure("Sp_DeleteOrgnization ", param);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Deleted successfully');location.href='ViewOrgnization.aspx';", true);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            string filename = Session.SessionID + OrgLogo.FileName;
            OrgLogo.PostedFile.SaveAs(Server.MapPath("../images/Logo/" + filename));
            filename = "../images/Logo/" + Session.SessionID + OrgLogo.FileName;

        if (OrgLogo.HasFile)
              filename = "../images/Logo/" + Session.SessionID + OrgLogo.FileName;
          else
              filename = "";
            //update Org detials code here
			string org_typ="";
			if(drptype.Text=="Other")
				org_typ="0";
			else
				org_typ="1";
				
            SqlParameter[] param = new SqlParameter[]
                     {
                         new SqlParameter ("@orgId",Request.QueryString["ID"]),
                         new SqlParameter ("@orgName",txtOrgName.Text),
                          new SqlParameter ("@address",txtAdress.Text),
                          new SqlParameter ("@mobileNo",CryptoHelper.Encrypt(txtmobilenumber.Text)),
                          new SqlParameter ("@hrName",txtHRName.Text),
                          new SqlParameter ("@emailId",CryptoHelper.Encrypt(txtEmail.Text)),
                          new SqlParameter ("@orgDetails",txtOrgDetails.Text),
                          new SqlParameter ("@logo",filename),
  						 //new SqlParameter ("@govOrg",drptype.Text)
						new SqlParameter ("@govOrg",org_typ)

                     };
            DAL.ExecuteStoredProcedure("SP_UpdateOrgDetails", param);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Updated successfully');location.href='ViewOrgnization.aspx';", true);
            // Response.Redirect("AddOrganization.aspx");
        }
    }

    public string sendmail(string emailId, string password, string Name, string Username)
    {
        try
        {
            MailMessage MailMsg = new MailMessage();

            mailFrom = WebConfigurationManager.AppSettings["from_EmailID"];
            mailFrom_password = WebConfigurationManager.AppSettings["from_PassWord"];

            MailMsg.From = new MailAddress(mailFrom);//"visionarylifesciences7@gmail.com"
            MailMsg.To.Add(emailId);
            MailMsg.Subject = "HowzU Connect – Thank you for your registration. Your account is verified. Sign in today.";
            MailMsg.Body = 
               
                "Greetings of the day! <br />" +

                "<h3>Dear " + Name + " </h3><br />" +
                  "<h3>Welcome to Howzu.  </h3><br />" +

                "We take this opportunity to welcome you and your team to our best-in-class and state of the art healthcare solution. We have our presence PAN India with HQ based out of Pune. We are pleased that your Organisation decided to be part of our young and dynamic family.<br /><br /> " +
                "Our comprehensive health dashboards enable an Organization to streamline its employees’ health records on different levels ranging from departments,offices,cities etc. and analyze their health in statistical representation for effortless understanding. <br /><br />" +
                 "It also permits HR or any head of the department to authorize its employees to undergo specific tests from specific labs in special situations and update reports in the software. <br /><br />" +
                "<b>Get Started to be the change,</b><br />"+
                   "<b> 1. Click on the below link to login : </b> <br /> https://enterprise.visionarylifescience.com/ <br /><br />" +
                "<b>2 . Kindly use following credentials to Sign in,</b><br /><br />" +
                        "<b>User Name : " + emailId + "</b><br />" +
                        "<b>PassCode : " + CryptoHelper.Decrypt(password) + "</b><br /><br />" +

                "Enjoy the Health journey with us and we are looking for a long and endeavoring business Relationship. <br /><br />" +

                "We are delighted to welcome you to the Howzu family, and are looking for a long and endeavoring business Relationship.<br /><br />" +

                "<b>Yours sincerely ,</b><br />" +
                "<b>Howzu Team. </b> <br />"+
                 "<b>support@howzu.co.in </b> ";
            //MailMsg.Body = " <div style='padding: 18px; font-family: verdana; font-size: small; background-color: #eaf7ec;text-align: center'>" +
            //                "<img src='https://visionarylifescience.com/images/Howzulogo1092020101600.png' height='57px' width: 254px; class='img-thumbnail' /><h4>" +
            //                "Dear " + Name + "</h4><h3>Congratulations!!!</h3>" +
            //                "<span style='font-weight: bold'>Your business account is now live  and we are happy to onboard you on the leading healthcare portal in India. On HowzU Connect, you have access to a wide range of health infographics and  health organogram, available on latest interactive dashboards. !<br /><br />" +
            //                "</span>Please find your login details below.<br />" +
            //                "<span>Your UserName is:<h4 style='font-weight: bold'>" + emailId + "</h4></span>" +                             
            //                "We are also continuously working on expanding our features to make your experience even more seamless. For any queries you can reach out to us on "+
                           
            //                "Regards Team,<br />" +
            //                "Visionary Life Science Pvt. Ltd." +
            //                "</div>";

            //string mailBodyPath = WebConfigurationManager.AppSettings["mailBodyPath"];

            //string text = File.ReadAllText(mailBodyPath);


            //if (text.Contains("@name"))
            //    text.Replace("@name", "Name");
            //if (text.Contains("@userNm"))
            //    text.Replace("@userNm", "Username");
            //if (text.Contains("@pwd"))
            //    text.Replace("@pwd", "password");

            //MailMsg.Body = text;


            MailMsg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("relay-hosting.secureserver.net", 25);

            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(mailFrom, mailFrom_password); //from address credential
            client.EnableSsl = false;
            //Send the msgs  
            client.Send(MailMsg);
            // log.WriteErrorLog("Error: in Mail Send -----on time :" + System.DateTime.Now.ToString("HH:mm") + "   - > " + ex.ToString());

            return "1";
        }
        catch (Exception ex)
        {

            throw ex;
           // log.WriteErrorLog("Error: in Mail Send -----on time :" + System.DateTime.Now.ToString("HH:mm") + "   - > " + ex.ToString());

            return "0";
        }
    }
}
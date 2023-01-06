using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrossPlatformAESEncryption.Helper;
using Validation;
using System.Threading;

public partial class ChangePassword : System.Web.UI.Page
{
    DBClass db = new DBClass();
    ClsChangePassword objChangePassword = new ClsChangePassword();
    InputValidation Ival = new InputValidation(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
            }
            else
            {
                Response.Redirect("LabLogin.aspx");
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        try
        {
            string userName = Request.Cookies["username"].Value.ToString();
            string userId = db.getData("select UserId from UserLoginMaster where UserName='" + userName + "'").ToString();
            string Msg = "";
            if (Ival.IsTextBoxEmpty(txtOldPassword.Value))
            {
                Msg += "● Please Enter Valid old password";
            }
            if (!Ival.ValidatePassword(txtNewPassword.Value))
            {
                Msg += "● Please enter Minimum 6 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character";
            }
            if (Msg.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');", true);
            }
            else
            {
                string oldPassword = CryptoHelper.Encrypt(txtOldPassword.Value);
                string newPassword = CryptoHelper.Encrypt(txtNewPassword.Value);
                string changePassword = objChangePassword.updatePassword(userName, oldPassword, newPassword, userId);
                if (changePassword == "1")
                {
                    Session.Clear();
                    Response.Cookies["loggedIn"].Expires = DateTime.Now.AddDays(-1);
                    Response.Redirect("LabLogin.aspx", false);
                   // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Password changed successfully');location.reload();", true);
                }
                if (changePassword == "2")
                {
                    Session.Clear();
                    Response.Cookies["loggedIn"].Expires = DateTime.Now.AddDays(-1);
                    Response.Redirect("LabLogin.aspx", false);
                   // Response.Redirect(@"LabLogin.aspx");
                    // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Password changed successfully');location.reload();", true);
                }
                else if (changePassword == "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Incorrect old password');", true);
                }
                else if (changePassword == "-1")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
}
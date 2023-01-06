using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using Validation;

public partial class EditProfile : System.Web.UI.Page
{
    CLSEditProfile objeditprofile = new CLSEditProfile();
    InputValidation Ival = new InputValidation();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
                    loadLabContactDetails();
                }
            }
            else
            {
                Response.Redirect("LabLogin.aspx");
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }

    protected void loadLabContactDetails()
    {
        try
        {
            DataSet ds = objeditprofile.getUserDetails(Request.Cookies["labUserId"].Value.ToString());

            if (ds != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //Load Details
                    spanUserName.InnerText = row["sFullName"].ToString();
                    spanUserEmail.InnerText = row["sEmailId"].ToString() != "" ? CryptoHelper.Decrypt(row["sEmailId"].ToString()) : "";
                    spanUserContact.InnerText = row["sContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sContact"].ToString()) : "";
                    spanUserId.InnerText = row["sUserName"].ToString();

                    //Bind details to editable fields
                    txtUserFullName.Text = row["sFullName"].ToString();
                    txtUserEmail.Text = row["sEmailId"].ToString() != "" ? CryptoHelper.Decrypt(row["sEmailId"].ToString()) : "";
                    txtUserContact.Text = row["sContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sContact"].ToString()) : "";
                    //  txtUserID.Text = row["sUserName"].ToString();
                    txtLabid.Text = row["sLabId"].ToString();
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string Msg = "";
            if (Ival.IsTextBoxEmpty(txtUserFullName.Text))
            {
                Msg += "● Please Enter Valid User Name";
            }
            if (!Ival.IsTextBoxEmpty(txtUserEmail.Text))
            {
                if (!Ival.IsValidEmailAddress(txtUserEmail.Text))
                {
                    Msg += "● Please Enter Valid Email Id";
                }
            }
            if (Ival.IsInteger(txtUserContact.Text))
            {
                if (!Ival.MobileValidation(txtUserContact.Text))
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');", true);
            }
            else
            {
                string labId = txtLabid.Text;
                string UserId = Request.Cookies["labUserId"].Value.ToString();
                string UserName = txtUserFullName.Text;
                string UserEmail = txtUserEmail.Text;
                string UserContact = txtUserContact.Text;
                string UserNAmeID = txtUserID.Text;
                Session["labuser"] = UserName;

                if (objeditprofile.updateUserContactDetails(labId, UserId, UserName, UserEmail, UserContact, UserNAmeID) == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.reload();", true);
                }
                if (objeditprofile.updateUserContactDetails(labId, UserId, UserName, UserEmail, UserContact, UserNAmeID) == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
}
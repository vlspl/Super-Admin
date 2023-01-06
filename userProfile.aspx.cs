using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Validation;
using CrossPlatformAESEncryption.Helper;

public partial class userProfile : System.Web.UI.Page
{
    CLSEditProfile objeditprofile = new CLSEditProfile();
    InputValidation Ival = new InputValidation();
    string labid="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnsaveProfile.Visible = false;
            txtaddress.ReadOnly = true;
            txtmobileNo.ReadOnly = true;
            txtfullName.ReadOnly = true;
            txtemailId.ReadOnly = true;
            loadPersonalDetails();
        }
    }
    void loadPersonalDetails()
    {
         DataSet ds = objeditprofile.getUserDetails(Request.Cookies["labUserId"].Value.ToString());

            if (ds != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    txtfullName.Text = row["sFullName"].ToString();
                    txtmobileNo.Text = CryptoHelper.Decrypt(row["sContact"].ToString());
                    lbluserName.Text = CryptoHelper.Decrypt(row["sUserName"].ToString());
                    lblemailId.Text = CryptoHelper.Decrypt(row["sEmailId"].ToString());
                    txtemailId.Text = CryptoHelper.Decrypt(row["sEmailId"].ToString());
                    labid = row["sLabId"].ToString();
                 }
             }
    }
    protected void btneditProfile_Click(object sender, EventArgs e)
    {
        btneditProfile.Visible = false;
        btnsaveProfile.Visible = true;
        txtaddress.ReadOnly = false;
        txtmobileNo.ReadOnly = false;
        txtfullName.ReadOnly = false;
        txtemailId.ReadOnly = false;
    }
    protected void btnsaveProfile_Click(object sender, EventArgs e)
    {
        try
        {
            string Msg = "";
            if (Ival.IsTextBoxEmpty(txtfullName.Text))
            {
                Msg += "● Please Enter Valid User Name";
            }
            if (!Ival.IsTextBoxEmpty(lblemailId.Text))
            {
                if (!Ival.IsValidEmailAddress(lblemailId.Text))
                {
                    Msg += "● Please Enter Valid Email Id";
                }
            }
            if (Ival.IsInteger(txtmobileNo.Text))
            {
                if (!Ival.MobileValidation(txtmobileNo.Text))
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
                string labId = labid;
                string UserId = Request.Cookies["labUserId"].Value.ToString();
                string UserName = txtfullName.Text;
                string UserEmail = txtemailId.Text;
                string UserContact = txtmobileNo.Text;
                 Session["labuser"] = UserName;

                if (objeditprofile.updateUserContactDetails(labId, UserId, UserName, UserEmail, UserContact) == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.href='Dashboard.aspx';", true);
                }
                if (objeditprofile.updateUserContactDetails(labId, UserId, UserName, UserEmail, UserContact) == 0)
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
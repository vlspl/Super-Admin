using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using Validation;


public partial class SuperAdmin_LabsManagementEditLab : System.Web.UI.Page
{
    CLSLabsManagementEditLab objLabsManagementEditLab = new CLSLabsManagementEditLab();
    InputValidation Ival = new InputValidation();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                getLabData();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    public void getLabData()
    {
        int labid = Convert.ToInt32(Request.QueryString["id"].ToString());
        DataSet ds = objLabsManagementEditLab.GetLabList(labid);

        if (ds.Tables[0].Rows.Count > 0)
        {
            LabCode.Text = ds.Tables[0].Rows[0]["sLabCode"].ToString();
            LabName.Text = ds.Tables[0].Rows[0]["sLabName"].ToString();
            LabManager.Text = ds.Tables[0].Rows[0]["sLabManager"].ToString();
            EmailId.Text = ds.Tables[0].Rows[0]["sLabEmailId"].ToString() != "" ? CryptoHelper.Decrypt(ds.Tables[0].Rows[0]["sLabEmailId"].ToString()) : "";
            LabContact.Text = ds.Tables[0].Rows[0]["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(ds.Tables[0].Rows[0]["sLabContact"].ToString()) : "";
            LabAddress.Text = ds.Tables[0].Rows[0]["sLabAddress"].ToString();
        }
    }


    protected void btn_Update_Click(object sender, EventArgs e)
    {
        string Msg = "";
        if (Ival.IsTextBoxEmpty(LabName.Text.ToString()))
        {
            Msg += "● Please Enter Valid Lab Name";
        }
        if (!Ival.IsTextBoxEmpty(EmailId.Text))
        {
            if (!Ival.IsValidEmailAddress(EmailId.Text))
            {
                Msg += "● Please Enter Valid Email Id";
            }
        }
        if (Ival.IsInteger(LabContact.Text))
        {
            if (!Ival.MobileValidation(LabContact.Text))
            {
                Msg += "● Please Enter Valid Mobile Number";
            }
        }
        else
        {
            Msg += "● Please Enter Valid Mobile Number";
        }
        if (Ival.IsTextBoxEmpty(LabManager.Text))
        {
            Msg += "● Please Enter Valid Lab Manager Name";
        }
        if (Msg.Length > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');location.reload();", true);
        }
        else
        {
            int dlabid = Convert.ToInt32(Request.QueryString["id"].ToString());
            string dLabName = LabName.Text.ToString();
            string dLabManager = LabManager.Text.ToString();
            string dLabEmailId = EmailId.Text.ToString() != "" ? CryptoHelper.Encrypt(EmailId.Text.ToString()) : "";
            string dLabContact = LabContact.Text.ToString() != "" ? CryptoHelper.Encrypt(LabContact.Text.ToString()) : "";
            string dLabAddress = LabAddress.Text.ToString();

            if (objLabsManagementEditLab.UpdateLabDetails(dlabid, dLabName, dLabManager, dLabEmailId, dLabContact, dLabAddress) == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Lab Details Update successfully');location.href='LabsManagement.aspx';", true);
            }
            if (objLabsManagementEditLab.UpdateLabDetails(dlabid, dLabName, dLabManager, dLabEmailId, dLabContact, dLabAddress) == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Error Occured');location.href='LabsManagement.aspx';", true);
            }
        }
    }

}
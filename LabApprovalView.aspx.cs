using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Validation;
using System.Web.Configuration;
using System.Data.SqlClient;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_LabApprovalView : System.Web.UI.Page
{
    CLSLabApprovalGetDetails objGetLabApproval = new CLSLabApprovalGetDetails();
    CLSLabApprovalInsert objlabaprreg = new CLSLabApprovalInsert();
    InputValidation Ival = new InputValidation();
    int labid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                getLabData();
                string timestamp = DateTime.UtcNow.ToString("ddMMyyyyHHmmssms");
                string LabCode = "LAB" + timestamp;
                hdnlabcode.Value = LabCode;
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
        DataTable ds = objGetLabApproval.getApproListDtl(labid);

         foreach (DataRow row in ds.Rows)
        {

            txtlabName.Text = row["sLabName"].ToString();
            txtLabOwner.Text = row["sLabManager"].ToString();
            txtLabAddress.Text = row["sLabAddress"].ToString();
            txtemailId.Text = row["sLabEmailId"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabEmailId"].ToString()) : "";
            txtcontactNo.Text = row["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabContact"].ToString()) : "";
            txtcity.Text = row["sLabLocation"].ToString();
            txtapprovalStatus.Text = row["sLabStatus"].ToString();
            txtorgName.Text = row["Name"].ToString();
            txtrequestedDate.Text = row["CreatedAt"].ToString();
            txtrequestedUser.Text = row["CreatedBy"].ToString();
            txtApporvedDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            hdnorgid.Value = row["org_Id"].ToString();
        }
    }
    protected void RegisterLab_Click(object sender, EventArgs e)
    {
        try
        {
            string labid = Request.QueryString["id"].ToString();
            objlabaprreg.sLabCode = hdnlabcode.Value;
            objlabaprreg.sLabName = txtlabName.Text.ToString();
            objlabaprreg.sLabManager = txtLabOwner.Text.ToString();
            objlabaprreg.sEmailId = txtemailId.Text.ToString();
            objlabaprreg.sStatus = drpLabStatus.SelectedItem.ToString();
            objlabaprreg.sLabContact = txtcontactNo.Text.ToString();
            objlabaprreg.sLabAddress = txtLabAddress.Text.ToString();
            objlabaprreg.latLong = txtlatLong.Text;
            objlabaprreg.orgId = hdnorgid.Value;
            objlabaprreg.temp_lab = labid;

            if (objlabaprreg.insertLabaprEntry() == 0)
            {
                Response.Redirect(@"labApproval.aspx",false);
            }

            else if (objlabaprreg.insertLabaprEntry() == 1)
            {
                Label lblMasterStatus = (Label)Master.FindControl("lblmsgText");
                lblMasterStatus.Text = "Error while Approving the lab";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "Yopopupalert();", true);

                // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertError", "alert('Error while registering the lab');", true);
            }
            else if (objlabaprreg.insertLabaprEntry() == 3)
            {
                Label lblMasterStatus = (Label)Master.FindControl("lblmsgText");
                lblMasterStatus.Text = "This username is already registered, please use another username";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "Yopopupalert();", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "dupicateUser", "alert('This username is already registered, please use another username');", true);
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
        }
        
    }
}
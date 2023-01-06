using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class LabContactDetails : System.Web.UI.Page
{
    ClsLabInfo objLabInfo = new ClsLabInfo();

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
            DataSet ds = objLabInfo.getLabDetails(Request.Cookies["labId"].Value.ToString());

            if (ds != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //Load Details
                    spanLabEmail.InnerText = row["sLabEmailId"].ToString();
                    spanLabContact.InnerText = row["sLabContact"].ToString();
                    spanLabAddress.InnerText = row["sLabAddress"].ToString();

                    //Bind details to editable fields
                    txtLabEmail.Text = row["sLabEmailId"].ToString();
                    txtLabContact.Text = row["sLabContact"].ToString();
                    txtLabAddress.Text = row["sLabAddress"].ToString();
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
            string labId = Request.Cookies["labId"].Value.ToString();
            string labEmail = txtLabEmail.Text;
            string labContact = txtLabContact.Text;
            string labAddress = txtLabAddress.Text;
            string labLocation = "";

            if (objLabInfo.updateLabContactDetails(labId, labEmail, labContact, labAddress, labLocation) == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.reload();", true);
            }
            if (objLabInfo.updateLabContactDetails(labId, labEmail, labContact, labAddress, labLocation) == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
}
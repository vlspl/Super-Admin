using System;
using System.Web.UI;
using System.Data;
using System.Web.Services;
using Validation;

public partial class AnalyteMaster : System.Web.UI.Page
{
    ClsAnalyteMaster objAnalyte = new ClsAnalyteMaster();
    InputValidation Ival = new InputValidation();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["loggedIn"] != null)
        {
            loadAnalyte();
        }
        else
        {
            Response.Redirect("LabLogin.aspx");
        }
    }
    protected void loadAnalyte()
    {
        try
        {
            DataSet ds = objAnalyte.getAnalyte();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabContent = "";
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //Load sections
                        tabContent +=
                                    "<tr>" +
                                           "<td scope='col'>" + row["sAnalyteId"].ToString() + "</td>" +
                                         "<td scope='col'>" + row["sAnalyteName"].ToString() + "</td>" +
                                              "<td scope='col'><a class='HideEditbtn' id='" + row["sAnalyteId"].ToString() + "' onclick='javascript:removeAnalyte(this)'><i class='fa fa-trash fa-color' aria-hidden='true'></i></a></td>" +
                                           "</tr>";
                            
                         
                    }
                    tbodyAnalyte.InnerHtml = tabContent;
                }
                else
                {
                    tbodyAnalyte.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
            else
            {
                tbodyAnalyte.InnerHtml = "<tr><td>No records found</td></tr>";
            }
        }
        catch(Exception ex)
        {
            LogError.LoggerCatch(ex);
            lblMessage.Text = "Error occurred While loading Analyte";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
    protected void btnAddAnalyte_click(object sender, EventArgs e)
    {
        try
        {
            int addAnalyte = objAnalyte.addAnalyte(txtAnalyteName.Text.ToString());

            if (addAnalyte >= 1)
            {
                lblMessage.Text = "Analyte added successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
               // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Analyte added successfully.');location.reload(true)", true);
            }
            else if (addAnalyte == 2)
            {
                lblMessage.Text = "Analyte already exists";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Analyte already exists');location.reload(true)", true);
            }
            else if (addAnalyte == 0)
            {
                lblMessage.Text = "Error occurred while Analyte added";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            lblMessage.Text = "Error occurred";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
          //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
    protected void btnUpdateAnalyte_click(object sender, EventArgs e)
    {
        try
        {
            string analyteId = hiddenAnalyteId.Value;
            string analyteName = txtAnalyte.Text;
            int updateAnalyte = objAnalyte.updateAnalyte(analyteId, analyteName);

            if (updateAnalyte == 1)
            {
                lblMessage.Text = "Analyte Updated sucessfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
               // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Updated sucessfully');location.reload(true);", true);
            }
            else if (updateAnalyte == 0)
            {
                lblMessage.Text = "Error occurred while updating Analyte ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
            }
        }
        catch(Exception ex)
        {
            LogError.LoggerCatch(ex);
            lblMessage.Text = "Error occurred";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
        }
    }
    [WebMethod]
    public static string removeAnalyte(string analyteId)
    {
           ClsAnalyteMaster objAnalyte = new ClsAnalyteMaster();
            int removeAnalyte = objAnalyte.deleteAnalyte(analyteId);
            return removeAnalyte.ToString();
    }
}
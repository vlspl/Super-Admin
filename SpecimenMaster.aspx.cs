using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class SpecimenMaster : System.Web.UI.Page
{
    ClsSpecimenMaster objSpecimen = new ClsSpecimenMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                loadSpecimen();
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
    protected void loadSpecimen()
    {
        try{
        DataSet ds = objSpecimen.getSpecimen();

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tabContent = "";/* "<tr>" +
                                     "<th scope='col'>Specimen</th>" +
                                     "<th scope='col'>Quantity</th>" +
                                     "<th scope='col'>Time Period</th>" +
                                     "<th scope='col'></th>" +
                                  "</tr>";*/
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //Load sections
                    tabContent +=
                                 "<tr>" +
                                           "<td scope='col'>" + row["sSpecimenId"].ToString() + "</td>" +
                                         "<td scope='col'>" + row["sSampleType"].ToString() + "</td>" +
                                          "<td scope='col'>" + row["sQuantity"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sTimePeriod"].ToString() + "</td>" +
                                              "<td scope='col'><a class='HideEditbtn' id='" + row["sSpecimenId"].ToString() + "' onclick='javascript:removeSpecimen(this)'><i class='fa fa-trash fa-color'' aria-hidden='true'></i></a></td>" +
                                           "</tr>";
                        
                   
                }

                tbodySpecimen.InnerHtml = tabContent;
            }
            else
            {
                tbodySpecimen.InnerHtml = "<tr><td>No records found</td></tr>";
            }
        }
        else
        {
            tbodySpecimen.InnerHtml = "<tr><td>No records found</td></tr>";
        }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnAddSpecimen_click(object sender, EventArgs e)
    {
        try{
        string sampleType = txtSampleType.Text.ToString();
        string quantity = txtQuantity.Text.ToString();
        string timePeriod = txtTimePeriod.Text.ToString();

        int addSpecimen = objSpecimen.addSpecimen(sampleType, quantity, timePeriod);

        if (addSpecimen == 1)
        {
            lblMessage.Text = "Specimen Added Successfully";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "location.reload(true);", true);
        }
        else if (addSpecimen == 2)
        {
            lblMessage.Text = "Specimen already exists";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Specimen already exists');location.reload(true);", true);
        }
        else if (addSpecimen == 0)
        {
            lblMessage.Text = "Error occurred while adding Specimen ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnUpdateSpecimen_click(object sender, EventArgs e)
    {
        try{
        string specimenId = hiddenSpecimenId.Value;
        string sampleType = txtEditSampleType.Text;
        string quantity = txtEditQuantity.Text;
        string timePeriod = txtEditTimePeriod.Text;
        int updateSpecimen = objSpecimen.updateSpecimen(specimenId,sampleType,quantity,timePeriod) ;

        if (updateSpecimen == 1)
        {
            lblMessage.Text = "Specimen Updated Successfully";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Updated sucessfully');location.reload(true);", true);
        }
        else if (updateSpecimen == 0)
        {
            lblMessage.Text = "Error occurred while Updating Specimen";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    [WebMethod]
    public static string removeSpecimen(string specimenId)
    {
        ClsSpecimenMaster objSpecimen = new ClsSpecimenMaster();
        int removeSpecimen = objSpecimen.deleteSpecimen(specimenId);

        return removeSpecimen.ToString();
    }
}
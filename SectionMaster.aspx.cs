using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class SectionMaster : System.Web.UI.Page
{
    ClsSectionMaster objSection = new ClsSectionMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try{
        if (Request.Cookies["loggedIn"] != null)
        {
            loadSections();
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
    protected void loadSections()
    {
        try{
        DataSet ds = objSection.getSection();

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tabContent = "";/* "<tr>" +
                                    "<th scope='col'>Section</th>" +
                                    "<th scope='col'></th>" +
                                 "</tr>"*/;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //Load sections
                    tabContent +=
                                  "<tr>" +
                                           "<td scope='col'>" + row["sSectionId"].ToString() + "</td>" +
                                         "<td scope='col'>" + row["sSectionName"].ToString() + "</td>" +

                                              "<td scope='col'><a class='HideEditbtn' id='" + row["sSectionId"].ToString() + "' onclick='javascript:removeSection(this)'><i class='fa fa-trash fa-color' aria-hidden='true'></i></a></td>" +
                                           "</tr>";

                     
                }

                tbodySection.InnerHtml = tabContent;
            }
            else
            {
                tbodySection.InnerHtml = "<tr><td>No records found</td></tr>";
            }
        }
        else
        {
            tbodySection.InnerHtml = "<tr><td>No records found</td></tr>";
        }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnAddSection_click(object sender, EventArgs e)
    {
        try{
        int addSection = objSection.addSection(txtSectionName.Text.ToString());

        if (addSection == 1)
        {
            lblMessage.Text = "Section Added Successfully";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "location.reload(true);", true);
        }
        else if (addSection == 2)
        {
            lblMessage.Text = "Section Already Exists";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Section already exists');location.reload(true);", true);
        }
        else if (addSection == 0)
        {
            lblMessage.Text = "Error occurred while Adding Sections";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnUpdateSection_click(object sender, EventArgs e)
    {
        try{
        string sectionId = hiddenSectionId.Value;
        string sectionName = txtEditSectionName.Text;

        int updateSection = objSection.updateSection(sectionId, sectionName);

        if (updateSection == 1)
        {
            lblMessage.Text = "Section Updated Successfully";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Updated sucessfully');location.reload(true);", true);
        }
        else if (updateSection == 0)
        {
            lblMessage.Text = "Error occurred while Updating Section";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
          //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }

    [WebMethod]
    public static string removeSection(string sectionId)
    {
        ClsSectionMaster objSection = new ClsSectionMaster();
        int removeSection =  objSection.deleteSection(sectionId);

        return removeSection.ToString();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class SubAnalyteMaster : System.Web.UI.Page
{
    ClsAnalyteMaster objAnalyte = new ClsAnalyteMaster();
    ClsSubAnalyteMaster objSubAnalyte = new ClsSubAnalyteMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try{
        if (Request.Cookies["loggedIn"] != null)
        {
            if (!Page.IsPostBack)
            {
                DataSet ds = objAnalyte.getAnalyte();

                if (ds != null)
                {
                    selAnalyte.Items.Clear();

                    //for editing sub analyte
                    selEditAnalyte.Items.Clear();
                    
                    selAnalyte.Items.Add(new ListItem("Select Analyte", ""));
                    
                    //for editing sub analyte
                    selEditAnalyte.Items.Add(new ListItem("Select Analyte", ""));

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        selAnalyte.Items.Add(new ListItem(dr["sAnalyteName"].ToString(), dr["sAnalyteId"].ToString()));

                        //for editing sub analyte
                        selEditAnalyte.Items.Add(new ListItem(dr["sAnalyteName"].ToString(), dr["sAnalyteId"].ToString()));
                    }
                }

                loadSubAnalyte();
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
    protected void loadSubAnalyte()
    {
        try{
        DataSet ds = objSubAnalyte.getSubAnalyte();

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tabContent = "";/* "<tr>" +
                                    "<th scope='col'>Sub Analyte</th>" +
                                    "<th scope='col'>Analyte</th>" +
                                    "<th scope='col'></th>" +
                                 "</tr>";*/
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //Load sections
                    tabContent += "<tr>" +
                                           "<td scope='col'>" + row["sSubAnalyteId"].ToString() + "</td>" +
                                         "<td scope='col'>" + row["sSubAnalyteName"].ToString() + "</td>" +
                                          "<td scope='col'>" + row["sAnalyteName"].ToString() + "</td>" +

                                              "<td scope='col'><a class='HideEditbtn' id='" + row["sSubAnalyteId"].ToString() + "' onclick='javascript:removeSubAnalyte(this)'><i class='fa fa-trash fa-color' aria-hidden='true'></i></a></td>" +
                                           "</tr>";
                        
                        
                        
                        
                       
                }

                tbodySubAnalyte.InnerHtml = tabContent;
            }
            else
            {
                tbodySubAnalyte.InnerHtml = "<tr><td>No records found</td></tr>";
            }
        }
        else
        {
            tbodySubAnalyte.InnerHtml = "<tr><td>No records found</td></tr>";
        }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnAddSubAnalyte_click(object sender, EventArgs e)
    {
        try{
        int addSubAnalyte = objSubAnalyte.addSubAnalyte(txtSubAnalyteName.Text.ToString(), selAnalyte.SelectedValue.ToString());

        if (addSubAnalyte == 1)
        {
            lblMessage.Text = "Subanalyte Added Successfully";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "location.reload(true);", true);
        }
        else if (addSubAnalyte == 2)
        {
            lblMessage.Text = "Subanalyte already exists";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Subanalyte already exists');location.reload(true);", true);
        }
        else if (addSubAnalyte == 0)
        {
            lblMessage.Text = "Error occurred While Subanalyte Added";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnUpdateSubAnalyte_click(object sender, EventArgs e)
    {
        try{
        string analyteId =selEditAnalyte.SelectedValue;
        string subAnalyteId = hiddenSubAnalyteId.Value;
        string subAnalyteName = txtEditSubAnalyteName.Text;

        int updateSubAnalyte = objSubAnalyte.updateSubAnalyte(subAnalyteId, analyteId, subAnalyteName);

        if (updateSubAnalyte == 1)
        {
            lblMessage.Text = "subAnalyte Updated Successfully";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Updated sucessfully');location.reload(true);", true);
        }
        else if (updateSubAnalyte == 0)
        {
            lblMessage.Text = "Error occurred while updating subAnalyte";
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
    public static string removeSubAnalyte(string subAnalyteId)
    {
        ClsSubAnalyteMaster objSubAnalyte = new ClsSubAnalyteMaster();
        int removeSubAnalyte = objSubAnalyte.deleteSubAnalyte(subAnalyteId);

        return removeSubAnalyte.ToString();
    }
}
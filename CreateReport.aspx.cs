using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CreateReport : System.Web.UI.Page
{
    ClsCreateReport objTest = new ClsCreateReport();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
                    loadTestTakenList();
                }
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
    protected void loadTestTakenList()
    {
        try
        {
            DataSet ds = objTest.getMyBookings(Request.Cookies["labId"].Value.ToString());

            //Hide CreateReport button if user is not owner or assistant
            string hideShow = "";

            if (Request.Cookies["role"].Value.ToString().ToLower().Contains("owner") || Request.Cookies["role"].Value.ToString().ToLower().Contains("assistant"))
            { }
            else
            {
                hideShow = "style='display:none'";
            }

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabTestTakenList = "";
                    int count = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        count = count + 1;
                        //Load lab test taken list
                        tabTestTakenList += "<tr>" +
                                           "<td scope='col'>" + count + "</td>" +
                                         "<td scope='col'>" + row["sPatient"].ToString() + "</td>" +
                                          "<td scope='col'>" + row["sTestCode"].ToString() + "</td>" +
                                            "<td scope='col'>" + row["sTestName"].ToString() + "</td>" +
                                             "<td scope='col'>" + row["sBookRequestedAt"].ToString() + "</td>" +
                                              "<td scope='col'><a href='CreateReportValues.aspx?bookId=" + row["sBookLabId"] + "&testId=" + row["sTestId"] + "&bookLabTestId=" + row["sBookLabTestId"] + "' class='btn btn-sm btn-color'>Create Report</a></td>" +
                                           "</tr>";
                                          
                                         
                    }
                    tbodyTestTakenList.InnerHtml = tabTestTakenList;
                }
                else
                {
                    tbodyTestTakenList.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
}
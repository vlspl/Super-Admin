using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ViewReport : System.Web.UI.Page
{
    ClsViewReport objReport = new ClsViewReport();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
                    loadReports();
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

    protected void loadReports()
    {
        try
        {
            DataSet ds = objReport.getReports(Request.Cookies["labId"].Value.ToString());

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabReports = "";
                    int count = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        count = count + 1;
                        string color = "";
                        string SendStatus = "";
                        if (row["sApprovalStatus"].ToString().ToLower() == "approved")
                        {
                            color = "#27AE60";
                        }
                        if (row["sApprovalStatus"].ToString().ToLower() == "approval pending")
                        {
                            color = "#fdbe00";
                        }
                        if (row["sApprovalStatus"].ToString().ToLower() == "rejected")
                        {
                            color = "#CC0000";
                        }

                        if (row["sCol10"].ToString() == "1")
                        {
                            SendStatus = "<img src='images/icons/check.png' />";
                        }
                        //Load tests whose reports are created
                        tabReports += "<tr>" +
                                           "<td scope='col'>" + count + "</td>" +
                                            "<td scope='col'>" + row["sPatient"].ToString() + "</td>" +
                                            "<td scope='col'>" + row["sTestCode"].ToString() + "</td>" +
                                             "<td scope='col'>" + row["sTestName"].ToString() + "</td>" +
                                             "<td scope='col'>" + row["sBookRequestedAt"].ToString() + "</td>" +
                                              "<td scope='col'>" + row["sApprovalStatus"].ToString() + "</td>" +
                                               "<td scope='col'><a href='ViewReportValues.aspx?bookId=" + row["sBookLabId"] + "&bookLabTestId=" + row["sBookLabTestId"] + "' class='btn btn-sm btn-color'>View Report</a></td>" +
                                                "<td scope='col'>" + SendStatus + "</td>" +
                                             "</tr>";

                                         
                    }
                    tbodyReports.InnerHtml = tabReports;
                }
                else
                {
                    tbodyReports.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
}
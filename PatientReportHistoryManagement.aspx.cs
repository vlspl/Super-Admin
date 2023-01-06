using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PatientReportHistoryManagement : System.Web.UI.Page
{
    CLSPatientManagementHealthProfile objPatientManagementHealthProfile = new CLSPatientManagementHealthProfile();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadReports();
        }
    }

    protected void loadReports()
    {
        string labid = Request.Cookies["LabId"].Value.ToString(); 
        int appuserid = Convert.ToInt32(Request.QueryString["id"].ToString());
        DataSet ds = objPatientManagementHealthProfile.getTestReport(labid, appuserid.ToString());

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tabReports = "";

                int count = 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;
                    tabReports += "<tr>" +
                                      "<td class='col'>" + row["sBookLabId"].ToString() + "</td>" +
                                      "<td class='col'>" + row["sPatient"].ToString() + "</td>" +
                                      "<td class='col'>" + row["sTestName"].ToString() + " (" + row["sTestCode"].ToString() + ")" + "</td>" +
                                      "<td class='col'>" + row["sTestDate"].ToString() + "</td>" +
                                      "<td class='col'>" + row["sPrice"].ToString() + "</td>" +
                                      "<td class='col'>" + row["sBookMode"].ToString() + "</td>" +


                                       "<td class='col'><a href='ViewReportValues.aspx?bookId=" + row["sBookLabId"].ToString() + "&bookLabTestId=" + row["sBookLabTestId"].ToString() + "' class=''>View Report</a></td>" +

                                     
                                      
                                      // "<td class='col'><a href='report/Report.aspx?bookId=" + row["sBookLabId"].ToString() + "&bookLabTestId=" + row["sBookLabTestId"].ToString() + "' class=''>View Report</a></td>" +
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
    protected void btnviewpayment_Click(object sender, EventArgs e)
    {
       
        Response.Redirect(@"PatientInvoiceHistory.aspx?id=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
    }
}
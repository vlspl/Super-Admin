using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PatientManagementHealthProfile : System.Web.UI.Page
{
    CLSPatientManagementHealthProfile objPatientManagementHealthProfile = new CLSPatientManagementHealthProfile();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!IsPostBack)
            {
                loadReports();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    protected void loadReports()
    {
        string labid = Request.QueryString["id"].ToString();
        string appuserid = Request.QueryString["AppUserId"].ToString();
        DataSet ds = objPatientManagementHealthProfile.getTestReport(labid, appuserid);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tabReports = "";

                int count = 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;

                    //Load tests whose reports are created
                    tabReports += "<tr>" +
                                       "<td scope='col'>" + row["sBookLabId"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sPatient"].ToString() + "</td>" +
                                      
                                       "<td scope='col'>" + row["sTestCode"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sTestName"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sTestDate"].ToString() + "</td>" +
                                     
                                       "<td scope='col'>" + row["sFees"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sBookMode"].ToString() + "</td>" +
                                     
                                       "<td scope='col'>" + row["sApprovalStatus"].ToString() + "</td>" +
                                        "<td scope='col'><a href='PatientManagementViewHealthProfile.aspx?bookId=" + row["sBookLabId"].ToString() + "&bookLabTestId=" + row["sBookLabTestId"].ToString() + "' class='lab-btn-secondary'>Report</a></td>" +
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
}
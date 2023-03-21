using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SuperAdmin_PatientManagement : System.Web.UI.Page
{
    CLSPatientManagement objPatientManagement = new CLSPatientManagement();
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
        DataSet ds = objPatientManagement.getReports(labid);

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
                                       "<td scope='col'>" + row["sAppUserId"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sfullname"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sgender"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sbirthdate"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["saddress"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["smobile"].ToString() + "</td>" +
                        //  "<td scope='col' style='display:none'>" + row["sApprovalStatus"].ToString() + "</td>" +
                                       "<td scope='col'><a href='PatientManagementProfile.aspx?id=" + labid + "&AppUserId=" + row["sAppUserId"].ToString() + "' class='lab-btn-secondary'>Profile</a></td>" +
                                       "<td scope='col'><a href='PatientManagementHealthProfile.aspx?id=" + labid + "&AppUserId=" + row["sAppUserId"].ToString() + "' class='lab-btn-secondary'>Test List</a></td>" +
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
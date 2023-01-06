using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class GlobalSearch : System.Web.UI.Page
{
    CLSGlobalSearch objGlobalSearch = new CLSGlobalSearch();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Cookies["username"].Value != null)
            {
                string keyword = Request.QueryString["keyword"].ToString().Replace("'", "");

                getListOfAllappUserforpatientList();
                getListOfAllappUserfordoctorList();
                getListOfAlltestList();
            }
        }
    }


    public void getListOfAllappUserforpatientList()
    {
        string keyword = Request.QueryString["keyword"].ToString().Replace("'", "");
        string LabIdSession = Request.Cookies["labId"].Value.ToString();
        DataSet ds = objGlobalSearch.PatientList(keyword, LabIdSession);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllLabList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;
                    AllLabList += "<tr>" +
                                   "<td scope='col'>" + row["sAppUserId"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sFullName"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sMobile"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sEmailId"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sAddress"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sRole"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sCountry"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sCity"].ToString() + "</td>" +
                                   "</tr>";
                }
                tbodyAllpatientlist.InnerHtml = AllLabList;
            }
            else
            {
                tbodyAllpatientlist.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }
    }

    public void getListOfAllappUserfordoctorList()
    {
        string keyword = Request.QueryString["keyword"].ToString().Replace("'", "");
        string LabIdSession = Request.Cookies["labId"].Value.ToString();
        DataSet ds = objGlobalSearch.DoctorList(keyword, LabIdSession);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllLabList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;
                    AllLabList += "<tr>" +
                                   "<td scope='col'>" + row["sAppUserId"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sFullName"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sMobile"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sEmailId"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sAddress"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sRole"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sCountry"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sCity"].ToString() + "</td>" +
                                   "</tr>";
                }
                tbodyAllDoctorsList.InnerHtml = AllLabList;
            }
            else
            {
                tbodyAllDoctorsList.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }


    }

    public void getListOfAlltestList()
    {
        string keyword = Request.QueryString["keyword"].ToString().Replace("'", "");
        string tablename = "test";
        string tableappusercolumnName = "sTestName";
        DataSet ds = objGlobalSearch.GetList(tablename, keyword, tableappusercolumnName);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllLabList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;
                    AllLabList += "<tr>" +
                                   "<td scope='col'>" + row["sTestCode"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sTestName"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sTestUsefulFor"].ToString() + "</td>" +
                                //   "<td scope='col'>" + row["sTestInterpretation"].ToString() + "</td>" +
                                   "</tr>";
                }
                tbodyAllTestList.InnerHtml = AllLabList;
            }
            else
            {
                tbodyAllTestList.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }


    }


}
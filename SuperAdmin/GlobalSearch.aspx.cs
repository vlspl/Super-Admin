using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SuperAdmin_GlobalSearch : System.Web.UI.Page
{
    CLSSuperAdminGlobalSearch objGlobalSearch = new CLSSuperAdminGlobalSearch();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                string keyword = Request.QueryString["keyword"].ToString().Replace("'", "");

                getListOfAllappUserforpatientList();
                getListOfAllappUserfordoctorList();
                getListOfAlllabUserList();
                getListOfAlltestList();

            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }


    public void getListOfAllappUserforpatientList()
    {
        string keyword = Request.QueryString["keyword"].ToString().Replace("'", "");
        string tablename = "appUser";
        string tableappusercolumnName = "sFullName";
        string Role = "patient";
        DataSet ds = objGlobalSearch.GetLabList(tablename, keyword, tableappusercolumnName, Role);
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
        string tablename = "appUser";
        string tableappusercolumnName = "sFullName";
        string Role = "doctor";
        DataSet ds = objGlobalSearch.GetLabList(tablename, keyword, tableappusercolumnName, Role);
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


    public void getListOfAlllabUserList()
    {
        string keyword = Request.QueryString["keyword"].ToString().Replace("'", "");
        string tablename = "labUser";
        string tableappusercolumnName = "sFullName";
        DataSet ds = objGlobalSearch.GetLabList(tablename, keyword, tableappusercolumnName);
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
                                     "<td scope='col'>" + row["sLabCode"].ToString() + "</td>" +
                                     "<td scope='col'>" + row["sFullName"].ToString() + "</td>" +
                                     "<td scope='col'>" + row["sEmailId"].ToString() + "</td>" +
                                     "<td scope='col'>" + row["Scontact"].ToString() + "</td>" +
                                     "<td scope='col'>" + row["sRole"].ToString() + "</td>" +
                                     "<td scope='col'>" + row["sDescription"].ToString() + "</td>" +
                                      "</tr>";
                }
                tbodyAllLabUsers.InnerHtml = AllLabList;
            }
            else
            {
                tbodyAllLabUsers.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }


    }


    public void getListOfAlltestList()
    {
        string keyword = Request.QueryString["keyword"].ToString().Replace("'", "");
        string tablename = "test";
        string tableappusercolumnName = "sTestName";
        DataSet ds = objGlobalSearch.GetLabList(tablename, keyword, tableappusercolumnName);
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
                                   "<td scope='col'>" + row["sTestInterpretation"].ToString() + "</td>" +
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
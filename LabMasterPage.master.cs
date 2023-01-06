using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Configuration;

using System.Data.SqlClient;


public partial class LabMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //try
            //{
               CsrfHandler.Validate(this.Page, forgeryToken);
            //    if (Request.Cookies["loggedIn"].Value == null || Request.Cookies["loggedIn"].Value != "true" || Request.Cookies["labUser"].Value.ToString() == "")
            //    {
            //        Response.Redirect("LabLogin.aspx");
            //    }
            //    else
            //    {
            Session["Count"] = "0";
            // lbluser.Text = Request.Cookies["labUser"].Value.ToString();
            Label1.Text = Request.Cookies["labUser"].Value.ToString();
            Label2.Text = Request.Cookies["role"].Value.ToString();
            MenuBar();
            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            Conn.Open();
            string query = "select sLabName from labMaster inner join labUser on labMaster.sLabId=labUser.sLabId where labUser.sFullName='" + Label1.Text + "'";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                lbllabname.Text = dr["sLabName"].ToString();

            }

            Conn.Close();
            // }
            //}
            //catch
            //{
            //    Session.Clear();
            //    Response.Cookies["loggedIn"].Expires = DateTime.Now.AddDays(-1);
            //    Response.Redirect("LabLogin.aspx");
            //}
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Cookies["loggedIn"].Expires = DateTime.Now.AddDays(-1);
        Response.Redirect("LabLogin.aspx");
    }
    public void MenuBar()
    {
        try
        {
            CLSLabMasterPage objLabMasterPage = new CLSLabMasterPage();
            string PageURL = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
            DataSet dss = new DataSet();
            DataSet dsMenu = new DataSet();
            StringBuilder sb1 = new StringBuilder();
            string labuserid = Request.Cookies["labUserId"].Value.ToString();
            dss = objLabMasterPage.getMenuList(labuserid);
            Button btnHideContent;
            HtmlAnchor HtmlLink;
            string access = "1";
            string Noaccess = "0";

            #region pageid

            string Pageid0 = dss.Tables[0].Rows[0]["sPageId"].ToString();
            string sAddid0 = dss.Tables[0].Rows[0]["sAdd"].ToString();
            string sEditid0 = dss.Tables[0].Rows[0]["sEdit"].ToString();
            string sViewid0 = dss.Tables[0].Rows[0]["sView"].ToString();

            string Pageid1 = dss.Tables[0].Rows[1]["sPageId"].ToString();
            string sAddid1 = dss.Tables[0].Rows[1]["sAdd"].ToString();
            string sEditid1 = dss.Tables[0].Rows[1]["sEdit"].ToString();
            string sViewid1 = dss.Tables[0].Rows[1]["sView"].ToString();

            string Pageid2 = dss.Tables[0].Rows[2]["sPageId"].ToString();
            string sAddid2 = dss.Tables[0].Rows[2]["sAdd"].ToString();
            string sEditid2 = dss.Tables[0].Rows[2]["sEdit"].ToString();
            string sViewid2 = dss.Tables[0].Rows[2]["sView"].ToString();

            string Pageid3 = dss.Tables[0].Rows[3]["sPageId"].ToString();
            string sAddid3 = dss.Tables[0].Rows[3]["sAdd"].ToString();
            string sEditid3 = dss.Tables[0].Rows[3]["sEdit"].ToString();
            string sViewid3 = dss.Tables[0].Rows[3]["sView"].ToString();

            string Pageid4 = dss.Tables[0].Rows[4]["sPageId"].ToString();
            string sAddid4 = dss.Tables[0].Rows[4]["sAdd"].ToString();
            string sEditid4 = dss.Tables[0].Rows[4]["sEdit"].ToString();
            string sViewid4 = dss.Tables[0].Rows[4]["sView"].ToString();

            string Pageid5 = dss.Tables[0].Rows[5]["sPageId"].ToString();
            string sAddid5 = dss.Tables[0].Rows[5]["sAdd"].ToString();
            string sEditid5 = dss.Tables[0].Rows[5]["sEdit"].ToString();
            string sViewid5 = dss.Tables[0].Rows[5]["sView"].ToString();

            string Pageid6 = dss.Tables[0].Rows[6]["sPageId"].ToString();
            string sAddid6 = dss.Tables[0].Rows[6]["sAdd"].ToString();
            string sEditid6 = dss.Tables[0].Rows[6]["sEdit"].ToString();
            string sViewid6 = dss.Tables[0].Rows[6]["sView"].ToString();

            string Pageid7 = dss.Tables[0].Rows[7]["sPageId"].ToString();
            string sAddid7 = dss.Tables[0].Rows[7]["sAdd"].ToString();
            string sEditid7 = dss.Tables[0].Rows[7]["sEdit"].ToString();
            string sViewid7 = dss.Tables[0].Rows[7]["sView"].ToString();

            string Pageid8 = dss.Tables[0].Rows[8]["sPageId"].ToString();
            string sAddid8 = dss.Tables[0].Rows[8]["sAdd"].ToString();
            string sEditid8 = dss.Tables[0].Rows[8]["sEdit"].ToString();
            string sViewid8 = dss.Tables[0].Rows[8]["sView"].ToString();

            string Pageid9 = dss.Tables[0].Rows[9]["sPageId"].ToString();
            string sAddid9 = dss.Tables[0].Rows[9]["sAdd"].ToString();
            string sEditid9 = dss.Tables[0].Rows[9]["sEdit"].ToString();
            string sViewid9 = dss.Tables[0].Rows[9]["sView"].ToString();

            string Pageid10 = dss.Tables[0].Rows[10]["sPageId"].ToString();
            string sAddid10 = dss.Tables[0].Rows[10]["sAdd"].ToString();
            string sEditid10 = dss.Tables[0].Rows[10]["sEdit"].ToString();
            string sViewid10 = dss.Tables[0].Rows[10]["sView"].ToString();

            string Pageid11 = dss.Tables[0].Rows[11]["sPageId"].ToString();
            string sAddid11 = dss.Tables[0].Rows[11]["sAdd"].ToString();
            string sEditid11 = dss.Tables[0].Rows[11]["sEdit"].ToString();
            string sViewid11 = dss.Tables[0].Rows[11]["sView"].ToString();

            string Pageid12 = dss.Tables[0].Rows[12]["sPageId"].ToString();
            string sAddid12 = dss.Tables[0].Rows[12]["sAdd"].ToString();
            string sEditid12 = dss.Tables[0].Rows[12]["sEdit"].ToString();
            string sViewid12 = dss.Tables[0].Rows[12]["sView"].ToString();

            string Pageid13 = dss.Tables[0].Rows[13]["sPageId"].ToString();
            string sAddid13 = dss.Tables[0].Rows[13]["sAdd"].ToString();
            string sEditid13 = dss.Tables[0].Rows[13]["sEdit"].ToString();
            string sViewid13 = dss.Tables[0].Rows[13]["sView"].ToString();

            string Pageid14 = dss.Tables[0].Rows[14]["sPageId"].ToString();
            string sAddid14 = dss.Tables[0].Rows[14]["sAdd"].ToString();
            string sEditid14 = dss.Tables[0].Rows[14]["sEdit"].ToString();
            string sViewid14 = dss.Tables[0].Rows[14]["sView"].ToString();

            string Pageid15 = dss.Tables[0].Rows[15]["sPageId"].ToString();
            string sAddid15 = dss.Tables[0].Rows[15]["sAdd"].ToString();
            string sEditid15 = dss.Tables[0].Rows[15]["sEdit"].ToString();
            string sViewid15 = dss.Tables[0].Rows[15]["sView"].ToString();

            string Pageid16 = dss.Tables[0].Rows[16]["sPageId"].ToString();
            string sAddid16 = dss.Tables[0].Rows[16]["sAdd"].ToString();
            string sEditid16 = dss.Tables[0].Rows[16]["sEdit"].ToString();
            string sViewid16 = dss.Tables[0].Rows[16]["sView"].ToString();

            string Pageid17 = dss.Tables[0].Rows[17]["sPageId"].ToString();
            string sAddid17 = dss.Tables[0].Rows[17]["sAdd"].ToString();
            string sEditid17 = dss.Tables[0].Rows[17]["sEdit"].ToString();
            string sViewid17 = dss.Tables[0].Rows[17]["sView"].ToString();

            string Pageid18 = dss.Tables[0].Rows[18]["sPageId"].ToString();
            string sAddid18 = dss.Tables[0].Rows[18]["sAdd"].ToString();
            string sEditid18 = dss.Tables[0].Rows[18]["sEdit"].ToString();
            string sViewid18 = dss.Tables[0].Rows[18]["sView"].ToString();

            string Pageid19 = dss.Tables[0].Rows[19]["sPageId"].ToString();
            string sAddid19 = dss.Tables[0].Rows[19]["sAdd"].ToString();
            string sEditid19 = dss.Tables[0].Rows[19]["sEdit"].ToString();
            string sViewid19 = dss.Tables[0].Rows[19]["sView"].ToString();

            string Pageid20 = dss.Tables[0].Rows[20]["sPageId"].ToString();
            string sAddid20 = dss.Tables[0].Rows[20]["sAdd"].ToString();
            string sEditid20 = dss.Tables[0].Rows[20]["sEdit"].ToString();
            string sViewid20 = dss.Tables[0].Rows[20]["sView"].ToString();

            string Pageid21 = dss.Tables[0].Rows[21]["sPageId"].ToString();
            string sAddid21 = dss.Tables[0].Rows[21]["sAdd"].ToString();
            string sEditid21 = dss.Tables[0].Rows[21]["sEdit"].ToString();
            string sViewid21 = dss.Tables[0].Rows[21]["sView"].ToString();

            string Pageid22 = dss.Tables[0].Rows[22]["sPageId"].ToString();
            string sAddid22 = dss.Tables[0].Rows[22]["sAdd"].ToString();
            string sEditid22 = dss.Tables[0].Rows[22]["sEdit"].ToString();
            string sViewid22 = dss.Tables[0].Rows[22]["sView"].ToString();

            string Pageid23 = dss.Tables[0].Rows[23]["sPageId"].ToString();
            string sAddid23 = dss.Tables[0].Rows[23]["sAdd"].ToString();
            string sEditid23 = dss.Tables[0].Rows[23]["sEdit"].ToString();
            string sViewid23 = dss.Tables[0].Rows[23]["sView"].ToString();

            string Pageid24 = dss.Tables[0].Rows[24]["sPageId"].ToString();
            string sAddid24 = dss.Tables[0].Rows[24]["sAdd"].ToString();
            string sEditid24 = dss.Tables[0].Rows[24]["sEdit"].ToString();
            string sViewid24 = dss.Tables[0].Rows[24]["sView"].ToString();

            string Pageid25 = dss.Tables[0].Rows[25]["sPageId"].ToString();
            string sAddid25 = dss.Tables[0].Rows[25]["sAdd"].ToString();
            string sEditid25 = dss.Tables[0].Rows[25]["sEdit"].ToString();
            string sViewid25 = dss.Tables[0].Rows[25]["sView"].ToString();

            #endregion pageid

            sb1.Append("<ul class=\"list-unstyled components\"> ");
            //sb1.Append("<li> <a id=\"sidebarCollapse\"><i class=\"fa fa-bars\" aria-hidden=\"true\"></i><span>Menu</span></a></li>");
            sb1.Append("<li title=\"Dashboard\"> <a href=\"Dashboard.aspx\"><i class=\"fa fa-fw fa-home\" aria-hidden=\"true\"></i><span>Home</span></a></li>");
            if (Pageid0 == "1" && (sAddid0 == access || sEditid0 == access || sViewid0 == access))
            {
                sb1.Append("<li title=\"Book Test\"> <a href=\"TestBookList.aspx\"><i class=\"fa fa-heartbeat\" aria-hidden=\"true\"></i><span>Book Test</span></a></li>");


                Label TextBox1 = (Label)ContentPlaceHolder1.FindControl("slotdate");
                Control control = this.ContentPlaceHolder1.FindControl("rdoBookAwaiting");
                string asdf = Convert.ToString(Page.Master.FindControl("rdoBookAwaiting"));
                // control.Visible = false; //.Attributes["class"] = "hide";


                TextBox contenttxt = (TextBox)ContentPlaceHolder1.FindControl("TextBox1");
                Button btn = (Button)ContentPlaceHolder1.FindControl("Button1");

                #region PageAccess1

                if (PageURL.ToString().Contains("TestBookList.aspx") == true)
                {
                    if (sAddid0 == Noaccess)
                    {
                        HtmlLink = (HtmlAnchor)ContentPlaceHolder1.FindControl("A2");
                        if (HtmlLink != null)
                        {
                            HtmlLink.Attributes["class"] = "hide";
                        }
                    }
                }

                if (PageURL.ToString().Contains("BookTest.aspx") == true)
                {
                    if (sViewid0 == Noaccess)
                    {
                        btnHideContent = (Button)ContentPlaceHolder1.FindControl("btnConfirmBooking");
                        HtmlLink = (HtmlAnchor)ContentPlaceHolder1.FindControl("AddPatientbtn");
                        btnHideContent.Attributes["class"] = "hide";
                        HtmlLink.Attributes["class"] = "hide";
                    }
                }

                #endregion PageAccess1

            }
            sb1.Append("<li title=\"Manage Reports\" class='sidebar-dropdown'>");
            sb1.Append("<a href=\"#0Submenu\" data-toggle=\"collapse\" aria-expanded=\"false\"> <i class=\"fa fa-plus-square\" aria-hidden=\"true\"></i><span>Manage Reports</span></a> ");
            sb1.Append("<ul class=\"collapse list-unstyled\" id=\"0Submenu\">");

            if (Pageid2 == "3" && (sAddid2 == access || sEditid2 == access || sViewid2 == access))
            {
                sb1.Append("<li title=\"Create Report\"><a href=\"CreateReport.aspx\">&nbsp&nbsp&nbsp<img src=\"images/icons/create-report.png\" /> <img src=\"images/icons/create-report-hover.png\" class=\"hovericon\" /> <span>   Create Report</span></a></li>");
                #region PageAccess3

                if (PageURL.ToString().Contains("CreateReport.aspx") == true)
                {
                    if (sViewid23 == Noaccess)
                    {
                        string script = "window.onload = function() { HideAdd(); };";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);
                    }
                }

                if (PageURL.ToString().Contains("CreateReportValues.aspx") == true)
                {
                    if (sAddid23 == Noaccess)
                    {
                        string script = "window.onload = function() { HideAdd(); };";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);
                    }
                }

                #endregion PageAccess3
            }
            if (Pageid1 == "2" && (sAddid1 == access || sEditid1 == access || sViewid1 == access))
            {
                sb1.Append("<li title=\"View Report\"><a href=\"ViewReport.aspx\">&nbsp&nbsp&nbsp<img src=\"images/icons/view-report.png\" /> <img src=\"images/icons/view-report-hover.png\" class=\"hovericon\" /> <span>   View Reports</span></a></li>");
                #region PageAccess2

                if (PageURL.ToString().Contains("ViewReport.aspx") == true)
                {
                    if (sViewid22 == Noaccess)
                    {
                        string script = "window.onload = function() { HideView(); };";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);
                    }
                }

                #endregion PageAccess2
            }
            sb1.Append("</ul>");
            sb1.Append("</li>");

            if (Pageid3 == "4" && (sAddid3 == access || sEditid3 == access || sViewid3 == access))
            {
                sb1.Append("<li title=\"Patient List\"> <a href=\"PatientList.aspx\"> <i class=\"fa fa-user\" aria-hidden=\"true\"></i><span>Patient</span></a></li>");
                #region PageAccess4

                if (PageURL.ToString().Contains("PatientList.aspx") == true)
                {
                    if (sAddid3 == Noaccess)
                    {

                        HtmlLink = (HtmlAnchor)ContentPlaceHolder1.FindControl("HideAddbtn");
                        HtmlLink.Attributes["class"] = "hide";
                    }

                    if (sEditid3 == Noaccess)
                    {
                        string scriptss = "window.onload = function() { HideEdit(); };";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", scriptss, true);
                    }


                }

                if (PageURL.ToString().Contains("PatientList.aspx") == true)
                {

                }



                #endregion PageAccess4
            }

            if (Pageid4 == "5" && (sAddid4 == access || sEditid4 == access || sViewid4 == access))
            {
                sb1.Append("<li title=\"Doctor List\"> <a href=\"DoctorList.aspx\"><i class=\"fa fa-user-md\" aria-hidden=\"true\"></i><span>Doctors</span></a></li>");
                #region PageAccess5

                if (PageURL.ToString().Contains("DoctorList.aspx") == true)
                {
                    if (sEditid4 == Noaccess)
                    {
                        string script = "window.onload = function() { HideEdit(); };";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);
                    }

                    if (sAddid4 == Noaccess)
                    {
                        HtmlLink = (HtmlAnchor)ContentPlaceHolder1.FindControl("HideAddbtn");
                        HtmlLink.Attributes["class"] = "hide";
                    }
                }

                #endregion PageAccess5
            }
            sb1.Append("<li title=\"Manage Test\" class='sidebar-dropdown'>");
            sb1.Append("<a href=\"#0Submenu1\" data-toggle=\"collapse\" aria-expanded=\"false\"> <i class=\"fa fa-plus-square\" aria-hidden=\"true\"></i><span>Manage Test</span></a> ");
            sb1.Append("<ul class=\"collapse list-unstyled\" id=\"0Submenu1\">");

            if (Pageid5 == "6" && (sAddid5 == access || sEditid5 == access || sViewid5 == access))
            {
                sb1.Append("<li title=\"My Test List\"> <a href=\"TestList.aspx\"> <i class=\"fa fa-list\"></i><span>My Test List</span></a></li>");
                #region PageAccess6

                if (PageURL.ToString().Contains("TestList.aspx") == true)
                {
                    if (sViewid24 == Noaccess)
                    {
                        string script = "window.onload = function() { HideView(); };";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);
                    }

                    if (sEditid5 == Noaccess)
                    {
                        btnHideContent = (Button)ContentPlaceHolder1.FindControl("btnCreateTestPackage");
                        HtmlLink = (HtmlAnchor)ContentPlaceHolder1.FindControl("HideEditbtn");
                        btnHideContent.Attributes["class"] = "hide";
                        HtmlLink.Attributes["class"] = "hide";

                        string scripts = "window.onload = function() { HideEdit(); };";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UpdateTimes", scripts, true);
                    }

                    if (sAddid4 == Noaccess)
                    {
                        HtmlLink = (HtmlAnchor)ContentPlaceHolder1.FindControl("HideAddbtn");
                        //HtmlLink.Attributes["class"] = "hide";
                    }
                }
                #endregion PageAccess6
            }
            sb1.Append("<li title=\"All Test List\"> <a href=\"AllTestList.aspx\"> <i class=\"fa fa-list\"></i><span>All Test List</span></a></li>");
            sb1.Append("</ul>");
            sb1.Append("</li>");
            sb1.Append("<li title=\"Lab Information\" class='sidebar-dropdown'>");
            sb1.Append("<a href=\"#1Submenu\" data-toggle=\"collapse\" aria-expanded=\"false\" id=\"firstdropdown\"> <i class=\"fa fa-flask\" aria-hidden=\"true\"></i><span>Lab Info</span></a> ");
            sb1.Append("<ul class=\"collapse list-unstyled\" id=\"1Submenu\">");

            if (Pageid6 == "7" && (sAddid6 == access || sEditid6 == access || sViewid6 == access))
            {
                sb1.Append("<li title=\"Lab Details\" class=\"hover-color\"><a href=\"LabInfoDetails.aspx\"><i class=\"fa fa-info\" aria-hidden=\"true\"></i><span>Lab Details</span></a></li>");
                #region PageAccess7

                if (PageURL.ToString().Contains("LabInfoDetails.aspx") == true)
                {
                    if (sEditid6 == Noaccess)
                    {
                        HtmlLink = (HtmlAnchor)ContentPlaceHolder1.FindControl("HideEditbtn");
                        HtmlLink.Attributes["class"] = "hide";
                    }
                }

                #endregion PageAccess7
            }

            if (Pageid8 == "9" && (sAddid8 == access || sEditid8 == access || sViewid8 == access))
            {
                sb1.Append("<li title=\"Lab Calendar\" class=\"hover-color\"><a href=\"LabCalendar.aspx\"><i class=\"fa fa-calendar\" aria-hidden=\"true\"></i><span>Lab Calendar</span></a></li>");
                #region PageAccess9

                if (PageURL.ToString().Contains("LabCalendar.aspx") == true)
                {
                    if (sEditid8 == Noaccess)
                    {
                        string script = "window.onload = function() { HideEdit(); };";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);
                    }

                    if (sAddid8 == Noaccess)
                    {
                        HtmlLink = (HtmlAnchor)ContentPlaceHolder1.FindControl("HideAddbtn");
                        HtmlLink.Attributes["class"] = "hide";
                    }
                }

                #endregion PageAccess9
            }

            sb1.Append("</ul>");
            sb1.Append("</li>");
            sb1.Append("<li title=\"Master\" class='sidebar-dropdown'>");
            sb1.Append("<a href=\"#2Submenu\" data-toggle=\"collapse\" aria-expanded=\"false\" id=\"seconddropdown\"> <i class=\"fa fa-star\" aria-hidden=\"true\"></i><span>Master</span></a> ");
            sb1.Append("<ul class=\"collapse list-unstyled\" id=\"2Submenu\">");

            if (Pageid9 == "10" && (sAddid9 == access || sEditid9 == access || sViewid9 == access))
            {
                sb1.Append("<li title=\"Analyte Master\"><a href=\"AnalyteMaster.aspx\">&nbsp&nbsp&nbsp<img src=\"images/icons/analyte.png\" /> <img src=\"images/icons/analyte-hover.png\" class=\"hovericon\" /> <span>  Analyte Master</span></a></li>");
                #region PageAccess10

                if (PageURL.ToString().Contains("/AnalyteMaster.aspx") == true)
                {
                    if (sEditid9 == Noaccess)
                    {
                        string script = "window.onload = function() { HideEdit();};";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UpdateTime", script, true);
                    }

                    if (sAddid9 == Noaccess)
                    {
                        btnHideContent = (Button)ContentPlaceHolder1.FindControl("btnAddAnalyte");
                        btnHideContent.Attributes["class"] = "hide";
                    }
                }

                #endregion PageAccess10
            }

            if (Pageid10 == "11" && (sAddid10 == access || sEditid10 == access || sViewid10 == access))
            {
                sb1.Append("<li title=\"Sub Analyte Master\"><a href=\"SubAnalyteMaster.aspx\">&nbsp&nbsp&nbsp<img src=\"images/icons/sub-analyte.png\" /> <img src=\"images/icons/sub-analyte-hover.png\" class=\"hovericon\" /> <span>  Sub Analyte Master</span></a></li>");

                #region PageAccess11

                if (PageURL.ToString().Contains("SubAnalyteMaster.aspx") == true)
                {
                    if (sEditid10 == Noaccess)
                    {
                        string script = "window.onload = function() { HideEdit();};";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UpdateTime", script, true);
                    }

                    if (sAddid10 == Noaccess)
                    {
                        btnHideContent = (Button)ContentPlaceHolder1.FindControl("btnSubAnalyte");
                        btnHideContent.Attributes["class"] = "hide";
                    }
                }

                #endregion PageAccess11
            }

            if (Pageid11 == "12" && (sAddid11 == access || sEditid11 == access || sViewid11 == access))
            {
                sb1.Append("<li title=\"Specimen Master\"><a href=\"SpecimenMaster.aspx\">&nbsp&nbsp&nbsp<img src=\"images/icons/specimen-master.png\" /> <img src=\"images/icons/specimen-master-hover.png\" class=\"hovericon\" /> <span>  Specimen Master</span></a></li>");

                #region PageAccess12

                if (PageURL.ToString().Contains("SpecimenMaster.aspx") == true)
                {
                    if (sEditid11 == Noaccess)
                    {
                        string script = "window.onload = function() { HideEdit();};";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UpdateTime", script, true);
                    }

                    if (sAddid11 == Noaccess)
                    {
                        btnHideContent = (Button)ContentPlaceHolder1.FindControl("btnAddSpecimen");
                        btnHideContent.Attributes["class"] = "hide";
                    }
                }

                #endregion PageAccess12
            }

            if (Pageid14 == "15" && (sAddid14 == access || sEditid14 == access || sViewid14 == access))
            {
                sb1.Append("<li title=\"Test Master\"><a href=\"TestMaster.aspx\">&nbsp&nbsp&nbsp<img src=\"images/icons/test-master.png\" /> <img src=\"images/icons/test-master-hover.png\" class=\"hovericon\" /> <span>  Test Master</span></a></li>");

                #region PageAccess13

                if (PageURL.ToString().Contains("TestMaster.aspx") == true)
                {
                    if (sEditid14 == Noaccess)
                    {
                        string script = "window.onload = function() { HideEdit();};";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UpdateTime", script, true);
                    }

                    if (sAddid14 == Noaccess)
                    {
                        //btnHideContent = (Button)ContentPlaceHolder1.FindControl("btnAddProfile");
                        //btnHideContent.Attributes["class"] = "hide";
                        string script = "window.onload = function() { HideAdd();};";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UpdateTime", script, true);
                    }
                }

                #endregion PageAccess13
            }
            if (Pageid13 == "14" && (sAddid13 == access || sEditid13 == access || sViewid13 == access))
            {
                sb1.Append("<li title=\"Profile Master\"><a href=\"ProfileMaster.aspx\">&nbsp&nbsp&nbsp<img src=\"images/icons/profile-master.png\" /> <img src=\"images/icons/profile-master-hover.png\" class=\"hovericon\" /> <span>  Profile Master</span></a></li>");

                #region PageAccess13

                if (PageURL.ToString().Contains("ProfileMaster.aspx") == true)
                {
                    if (sEditid13 == Noaccess)
                    {
                        string script = "window.onload = function() { HideEdit();};";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UpdateTime", script, true);
                    }

                    if (sAddid13 == Noaccess)
                    {
                        btnHideContent = (Button)ContentPlaceHolder1.FindControl("btnAddProfile");
                        btnHideContent.Attributes["class"] = "hide";
                    }
                }

                #endregion PageAccess13

            }

            if (Pageid12 == "13" && (sAddid12 == access || sEditid12 == access || sViewid12 == access))
            {
                sb1.Append("<li title=\"Section Master\"><a href=\"SectionMaster.aspx\">&nbsp&nbsp&nbsp<img src=\"images/icons/section-master.png\" /> <img src=\"images/icons/section-master-hover.png\" class=\"hovericon\" /> <span>  Section Master</span></a></li>");
                #region PageAccess13

                if (PageURL.ToString().Contains("SectionMaster.aspx") == true)
                {
                    if (sEditid12 == Noaccess)
                    {
                        string script = "window.onload = function() { HideEdit();};";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UpdateTime", script, true);
                    }

                    if (sAddid12 == Noaccess)
                    {
                        btnHideContent = (Button)ContentPlaceHolder1.FindControl("btnAddSection");
                        btnHideContent.Attributes["class"] = "hide";
                    }
                }


                #endregion PageAccess13
            }
            sb1.Append("</ul>");
            sb1.Append("</li> ");
            sb1.Append("<li title=\"Admin\" class='sidebar-dropdown'>");
            sb1.Append("<a href=\"#3Submenu\" data-toggle=\"collapse\" aria-expanded=\"false\"> <i class=\"fa fa-user\" aria-hidden=\"true\"></i><span>Admin</span></a> ");
            sb1.Append("<ul class=\"collapse list-unstyled\" id=\"3Submenu\">");

            if (Pageid15 == "16" && (sAddid15 == access || sEditid15 == access || sViewid15 == access))
            {
                sb1.Append("<li title=\"User Management\"><a href=\"ManageUsers.aspx\">&nbsp&nbsp&nbsp<img src=\"images/icons/user-management.png\" /> <img src=\"images/icons/user-management-hover.png\" class=\"hovericon\" /> <span>  User Management</span></a></li>");

                #region PageAccess15

                if (PageURL.ToString().Contains("ManageUsers.aspx") == true)
                {
                    if (sEditid15 == Noaccess)
                    {
                        string script = "window.onload = function() { HideEdit();};";
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UpdateTime", script, true);
                    }

                    if (sAddid15 == Noaccess)
                    {
                        HtmlLink = (HtmlAnchor)ContentPlaceHolder1.FindControl("HideAddbtn");
                        HtmlLink.Attributes["class"] = "hide";
                    }
                }

                #endregion PageAccess15
            }

            //if (Pageid16 == "17" && (sAddid16 == access || sEditid16 == access || sViewid16 == access))
            //{
            //    sb1.Append("<li class='hide' title=\"Roles &amp; Rights Management\"><a href=\"ManageRoles.aspx\"><img src=\"images/icons/roles-right-management.png\" /> <img src=\"images/icons/roles-right-management-hover.png\" class=\"hovericon\" /> <span>Roles & Rights Management</span></a></li>");
            //}
            sb1.Append("</ul>");
            sb1.Append("</li>");
            sb1.Append("</ul>");
            ltrMenuBar.Text = sb1.ToString();
        }
        catch
        {
            Session.Clear();
            Response.Cookies["loggedIn"].Expires = DateTime.Now.AddDays(-1);
            Response.Redirect("LabLogin.aspx");
        }
    }
}

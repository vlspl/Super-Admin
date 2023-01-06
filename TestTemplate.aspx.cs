using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Collections;
using System.Web.UI.HtmlControls;


public partial class TestTemplate : System.Web.UI.Page
{

    CLSTemplateBuilder objTemplateBuilder = new CLSTemplateBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
       
        //load Test analytes
        loadAnalyteList();
        loadTemplate();   
        
    }

    protected void loadAnalyteList()
    {
        string pgtestid = Request.QueryString["id"].ToString();
        DataSet ds = objTemplateBuilder.getAnalyte(pgtestid);

      
        if (ds != null)
        {
            
 
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tabContent = "";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //Load sections
                    tabContent += "<tr id='rowAnalyte" + row["sAnalyteId"].ToString() + "'>" +
                                       "<td scope='col'><input type='checkbox' value='" + row["sAnalyteId"].ToString() + "' id='chkAnalyte" + row["sAnalyteId"].ToString() + "' text='" + row["sAnalyteName"].ToString() + "' name='chkAnalyte' clientidmode='Static' onchange='javascript:analyteSelected(this)' ></td>" +
                                       "<td scope='col'>" + row["sAnalyteId"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sAnalyteName"].ToString() + "</td>" +
                                    "</tr>";
                }

                tbodyAnalyteList.InnerHtml = tabContent;
            }
            else
            {
                tbodyAnalyteList.InnerHtml = "<tr><td>No records found</td></tr>";
            }
        }
        else
        {
            tbodyAnalyteList.InnerHtml = "<tr><td>No records found</td></tr>";
        }
    }


    protected void loadTemplate()
    {
        string LabId = Request.QueryString["id"].ToString(); 
        string tabTestValueResult = "";

        DataSet dsTestReport = objTemplateBuilder.getTestReport(LabId);
        DataSet dstemplatebody = objTemplateBuilder.checkTemplateBody(LabId);
        DataSet dstemplatehead = objTemplateBuilder.checkTemplateHead(LabId);

        if (dstemplatebody.Tables[0].Rows.Count > 0)
        {
            string templatebody = Convert.ToString(dstemplatebody.Tables[0].Rows[0]["stemplatestatus"]);
            if (templatebody == "Active") AddPatientbtn.Style["Display"] = "none";            
        }
        else Deletetemplatebtn.Style["Display"] = "none";

        if (dstemplatehead.Tables[0].Rows.Count > 0)
        {
            string templatehead = Convert.ToString(dstemplatehead.Tables[0].Rows[0]["stemplatestatus"]);

            string templateheading = Convert.ToString(dstemplatehead.Tables[0].Rows[0]["sheadtitle"]);
            string templatesubheading = Convert.ToString(dstemplatehead.Tables[0].Rows[0]["ssubtitle"]);
            string templatenotes = Convert.ToString(dstemplatehead.Tables[0].Rows[0]["sNotes"]);
            string templatecomments = Convert.ToString(dstemplatehead.Tables[0].Rows[0]["sComments"]);

            if (templatehead == "ActiveHead") addHEadingbtn.Style["Display"] = "none";
            if (templateheading != "") tempheading.InnerHtml = "<h1>" + templateheading.ToString() + "</h1>";
            if (templatesubheading != "") tempsubheading.InnerHtml = "<h4>" + templatesubheading.ToString() + "</h4>";
            if (templatenotes == "Yes") Notesdiv.InnerHtml = "<p>Notes: <span>" + templatenotes.ToString() + "</span></p>";
            if (templatecomments == "Yes") CommentDiv.InnerHtml = "<p>Comment: <span>" + templatecomments.ToString() + "</span></p>";
        }

        if (dsTestReport != null)
        {
            if (dsTestReport.Tables[0].Rows.Count > 0)
            {
                //spanLabName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabName"].ToString();
                //spanLabContact.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString();
                //spanLabAddress.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabAddress"].ToString();
                //spanBookingId.InnerHtml = dsTestReport.Tables[0].Rows[0]["sBookLabId"].ToString();
                //spanReportId.InnerHtml = dsTestReport.Tables[0].Rows[0]["sBookLabTestId"].ToString();
                //spanPatientName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sPatient"].ToString();
                //spanGender.InnerHtml = dsTestReport.Tables[0].Rows[0]["sGender"].ToString();
                //spanDoctorName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sDoctor"].ToString();
                //spanTestTakenOn.InnerHtml = dsTestReport.Tables[0].Rows[0]["sTestDate"].ToString();
                //spanReportCreatedOn.InnerHtml = dsTestReport.Tables[0].Rows[0]["sReportCreatedOn"].ToString();
                //spanReportCreatedBy.InnerHtml = dsTestReport.Tables[0].Rows[0]["sReportCreatedBy"].ToString();
                //spanApprovalStatus.InnerHtml = dsTestReport.Tables[0].Rows[0]["sApprovalStatus"].ToString();
                //spanComment.InnerHtml = dsTestReport.Tables[0].Rows[0]["sComment"].ToString();
                //spanTestCodeName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() + ", " + dsTestReport.Tables[0].Rows[0]["sTestName"].ToString();
                //spanNotes.InnerHtml = dsTestReport.Tables[0].Rows[0]["sNotes"].ToString();
                //txtNotes.Value = dsTestReport.Tables[0].Rows[0]["sNotes"].ToString();

                tabTestValueResult += "";

                foreach (DataRow row in dsTestReport.Tables[0].Rows)
                {
                    //display  values
                    string AnalyteName = row["sanalytename"].ToString();
                    string Analyteid = row["sanalyteid"].ToString();
                    if(AnalyteName =="")
                    {
                        AnalyteName = row["parentanyltename"].ToString();
                    }
                    tabTestValueResult += "<tr>" +
                                              "<td scope='col'>" + AnalyteName + "</td>" +
                                              "<td scope='col'></td>" +
                                              "<td scope='col'></td>" +
                                      "<tr>";

                    DataSet dssubanalyteReport = objTemplateBuilder.getSubAnalyteReport(Analyteid, LabId);
                    if (dssubanalyteReport != null)
                    {
                        if (dssubanalyteReport.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row1 in dssubanalyteReport.Tables[0].Rows)
                            {
                                string SubAnalyteNamep = row1["ssubanalytename"].ToString();
                                tabTestValueResult += "<tr>" +
                                                "<td scope='col' style='padding-left: 30px;'>" + SubAnalyteNamep + "</td>" +
                                                "<td scope='col'></td>" +
                                                "<td scope='col'></td>" +
                                        "<tr>";
                            }
                        }
                    }
                }
            }
        }

        tbodyTemplateBuilder.InnerHtml = tabTestValueResult;

    }


    protected void btnCreateTestTemplate_Click(object sender, EventArgs e)
    {        
        string reportCreatedOn = DateTime.Now.ToString(); 
        string reportCreatedBy = Request.Cookies["labUser"].Value.ToString();
        string TestID = Request.QueryString["id"].ToString();

        string[] hdnAnalyteId = hiddenAnalyteId.Value.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();
        hdnAnalyteId = (hdnAnalyteId.Length == 0) ? null : hdnAnalyteId;

        string[] hdnSubAnalyteId = hiddenSubAnalyteId.Value.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();
        hdnSubAnalyteId = (hdnSubAnalyteId.Length == 0) ? null : hdnSubAnalyteId;

        string[] hdnAnalyteIdName = hiddenAnalyteIdName.Value.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();
        hdnAnalyteIdName = (hdnAnalyteIdName.Length == 0) ? null : hdnAnalyteIdName;

        string[] hdnSubAnalyteIdName = hiddenSubAnalyteIdName.Value.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();
        hdnSubAnalyteIdName = (hdnSubAnalyteIdName.Length == 0) ? null : hdnSubAnalyteIdName;

        string hdnAnalyteSMR = (hiddenAnalyteSMR.Value == "") ? null : hiddenAnalyteSMR.Value;
        string hdnSubAnalyteSMR = (hiddenSubAnalyteSMR.Value == "") ? null : hiddenSubAnalyteSMR.Value;
        string hdnAnalyteSMRRefVal = (hiddenAnalyteSMRRefVal.Value == "") ? null : hiddenAnalyteSMRRefVal.Value;
        string hdnSubAnalyteSMRRefVal = (hiddenSubAnalyteSMRRefVal.Value == "") ? null : hiddenSubAnalyteSMRRefVal.Value;
        string labId = Request.Cookies["labId"].Value.ToString();

        List<Dictionary<string, object>> jsonAnalyteSMR = null;
        List<Dictionary<string, object>> jsonSubAnalyteSMR = null;
        List<Dictionary<string, object>> jsonAnalyteSMRRefVal = null;
        List<Dictionary<string, object>> jsonSubAnalyteSMRRefVal = null;

        if (hdnAnalyteSMR != null)
        {
            jsonAnalyteSMR = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(hdnAnalyteSMR);
        }

        if (hdnSubAnalyteSMR != null)
        {
            jsonSubAnalyteSMR = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(hdnSubAnalyteSMR);
        }

        if (hdnAnalyteSMRRefVal != null)
        {
            jsonAnalyteSMRRefVal = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(hdnAnalyteSMRRefVal);
        }

        if (hdnSubAnalyteSMRRefVal != null)
        {
            jsonSubAnalyteSMRRefVal = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(hdnSubAnalyteSMRRefVal);
        }
        int createTest = objTemplateBuilder.createTestTemplate(TestID, labId, hdnAnalyteId, hdnSubAnalyteId, reportCreatedBy, reportCreatedOn); 
        if (createTest == 1)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Test Template created'),location.reload(true);", true);
        }
        else if (createTest == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }

    protected void AddHeadings_CLick(object sender, EventArgs e)
    {
        string reportCreatedOn = DateTime.Now.ToString();
        string reportCreatedBy = Request.Cookies["labUser"].Value.ToString();
        string TestID = Request.QueryString["id"].ToString();
        string labId = Request.Cookies["labId"].Value.ToString();

        string headingt = Hedingtitle.Text.ToString();
        string subheadingt = subheadingtitle.Text.ToString();
        string selnotes = ddlselnotes.SelectedValue.ToString();
        string SelComments = ddlSelComments.SelectedValue.ToString();
        string selreferencevalue = ddlselreferencevalue.SelectedValue.ToString();

        int createTestTemplateHEading = objTemplateBuilder.createTestTemplateHeading(TestID, labId, headingt, subheadingt, selnotes,SelComments,selreferencevalue, reportCreatedBy, reportCreatedOn);

        if (createTestTemplateHEading == 1)
       {
           ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Test Template Heading created'),location.reload(true);", true);
           Response.Redirect("TestTemplate.aspx?id=" + TestID);
       }
        else if (createTestTemplateHEading == 0)
       {
           ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
       }
    }

    protected void DeleteTemplate_CLick(object sender, EventArgs e)
    {
        string reportCreatedOn = DateTime.Now.ToString();
        string reportCreatedBy = Request.Cookies["labUser"].Value.ToString();
        string TestID = Request.QueryString["id"].ToString();
        string labId = Request.Cookies["labId"].Value.ToString();

        int createTestTemplateHEading = objTemplateBuilder.DeleteTestTemplateHeading(TestID);

        if (createTestTemplateHEading == 1)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Test Template Heading created'),location.reload(true);", true);
            Response.Redirect("TestTemplate.aspx?id=" + TestID);
        }
        else if (createTestTemplateHEading == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }

    [WebMethod]
    public static string loadSubAnalyte(string analyteIds)
    {
        CLSTemplateBuilder objTemplateBuilder = new CLSTemplateBuilder();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        Dictionary<string, object> response = objTemplateBuilder.loadSubAnalyte(analyteIds);

        return serializer.Serialize(response);
    }

}
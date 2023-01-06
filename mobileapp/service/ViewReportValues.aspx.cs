using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using iTextSharp.text.html;


public partial class ViewReportValues : System.Web.UI.Page
{
    ClsViewReport objReport = new ClsViewReport();
    ClsBookDetails objBookDetails = new ClsBookDetails();

    CLSViewReportValuesinTemplate objTemplate = new CLSViewReportValuesinTemplate();
    CLSTemplateBuilder objTemplateBuilder = new CLSTemplateBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["loggedIn"] != null)
        {
            if (!IsPostBack)
            {
                loadReport();
            }
        }
        else
        {
            Response.Redirect("LabLogin.aspx");
        }
    }

    protected void loadReport()
    {
        string bookLabId = Request.QueryString["bookId"].ToString();
        string bookLabTestId = Request.QueryString["bookLabTestId"].ToString();
        string tabTestValueResult = "";
        string tabTestValueResultEdit = "";

        DataSet dsTestReport = objReport.getTestReport(bookLabTestId);

        if (dsTestReport != null)
        {
            string availpdffile = dsTestReport.Tables[0].Rows[0]["sPDFfiles"].ToString();
            if (availpdffile != "") viewpdf.InnerHtml = "<a href='http://visionarylifescience.com/images/reportPDF/" + availpdffile + ".pdf'  target='_blank' class='pdf-btn'  > View Pdf </a>"; else viewpdf.InnerHtml = "";

                if (dsTestReport.Tables[0].Rows.Count > 0)
                {
                    spanLabName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabName"].ToString();
                    spanLabContact.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString();
                    spanLabAddress.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabAddress"].ToString();
                    spanBookingId.InnerHtml = dsTestReport.Tables[0].Rows[0]["sBookLabId"].ToString();
                    spanReportId.InnerHtml = dsTestReport.Tables[0].Rows[0]["sBookLabTestId"].ToString();
                    if (dsTestReport.Tables[0].Rows[0]["sPaymentStatus"].ToString() != "paid")
                    {
                        spanPaymentStatus.Style.Value = "color:red;font-weight:bold";
                    }
                    spanPaymentStatus.InnerHtml = dsTestReport.Tables[0].Rows[0]["sPaymentStatus"].ToString();
                    spanPatientName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sPatient"].ToString();
                    spanGender.InnerHtml = dsTestReport.Tables[0].Rows[0]["sGender"].ToString();
                    string sGender = dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToString().ToLower();
                    string Male = cGeneralHelper.JSONEscape(dsTestReport.Tables[0].Rows[0]["sMale"].ToString());
                    string Female = cGeneralHelper.JSONEscape(dsTestReport.Tables[0].Rows[0]["sFemale"].ToString());
                    string Range = "";
                    if (sGender == "male")
                    {
                        Range = Male;
                    }
                    if (sGender == "female")
                    {
                        Range = Female;
                    }
                    spanApprovalRange.InnerHtml = Range;
                    spanDoctorName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sDoctor"].ToString();
                    spanTestTakenOn.InnerHtml = dsTestReport.Tables[0].Rows[0]["sTestDate"].ToString();
                    string DateTime = dsTestReport.Tables[0].Rows[0]["sReportCreatedOn"].ToString();
                    string[] SplitDateTime = DateTime.Split(' ');
                    string Date = SplitDateTime[0];
                    string Time = SplitDateTime[1];
                    string[] SplitDate = Date.Split('/');
                    string Day = SplitDate[1];
                    string Month = SplitDate[0];
                    string Year = SplitDate[2];
                    spanReportCreatedOn.InnerHtml = Day + "/" + Month + "/" + Year + " " + Time;
                    spanReportCreatedBy.InnerHtml = dsTestReport.Tables[0].Rows[0]["sReportCreatedBy"].ToString();
                    spanApprovalStatus.InnerHtml = dsTestReport.Tables[0].Rows[0]["sApprovalStatus"].ToString();

                    if (dsTestReport.Tables[0].Rows[0]["sApprovalStatus"].ToString() != "approved")
                    {
                        Button1.Visible = false;
                    }


                    spanComment.InnerHtml = dsTestReport.Tables[0].Rows[0]["sComment"].ToString();
                    spanTestCodeName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() + ", " + dsTestReport.Tables[0].Rows[0]["sTestName"].ToString();
                    spanNotes.InnerHtml = dsTestReport.Tables[0].Rows[0]["sNotes"].ToString();
                    txtNotes.Value = dsTestReport.Tables[0].Rows[0]["sNotes"].ToString();
                   
                    hdnsPatientId.Value = dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString();
                    hdnsDoctorId.Value = dsTestReport.Tables[0].Rows[0]["sDoctorid"].ToString();
                    hdnsReportId.Value = bookLabTestId;

                    tabTestValueResult += "<tr>" +
                        //"<th>TRV</th>"+
                        //"<th scope='col'>Test Id</th>" +
                        //"<th scope='col'>Test Code</th>" +
                                                "<th scope='col'>Analyte</th>" +
                                                "<th scope='col'>Subanalyte</th>" +
                                                "<th scope='col'>Specimen</th>" +
                                                "<th scope='col'>Method</th>" +
                                                "<th scope='col'>Result Type</th>" +
                        //"<th scope='col'>Reference Type</th>" +
                        //"<th scope='col'>Age</th>" +
                        //"<th scope='col'>Male</th>" +
                        //"<th scope='col'>Female</th>" +
                        //"<th scope='col'>Grade</th>" +
                        //"<th scope='col'>Units</th>" +
                        //"<th scope='col'>Interpretation</th>" +
                        //"<th scope='col'>Lower Limit</th>" +
                        //"<th scope='col'>Upper Limit</th>" +
                                                "<th scope='col'>Value</th>" +
                                                "<th scope='col'>Result</th>" +
                                        "</tr>";

                    tabTestValueResultEdit += "<tr>" +
                        //"<th>TRV</th>" +
                                                    "<th scope='col'>Analyte</th>" +
                                                    "<th scope='col'>Subanalyte</th>" +
                                                    "<th scope='col'>Value</th>" +
                                                    "<th scope='col'>Result</th>" +
                                                "</tr>";

                    foreach (DataRow row in dsTestReport.Tables[0].Rows)
                    {
                        hiddenValueIdList.Value += row["sTestReportValuesId"].ToString() + ",";
                        //display test values
                        tabTestValueResult += "<tr>" +
                            //"<td scope='col'>" + row["sTestReportValuesId"].ToString() + "</td>" +
                            //"<td scope='col'>" + row["sTestId"].ToString() + "</td>" +
                            //"<td scope='col'>" + row["sTestCode"].ToString() + "</td>" +
                                                  "<td scope='col'>" + row["sAnalyte"].ToString() + "</td>" +
                                                  "<td scope='col'>" + row["sSubAnalyte"].ToString() + "</td>" +
                                                  "<td scope='col'>" + row["sSpecimen"].ToString() + "</td>" +
                                                  "<td scope='col'>" + row["sMethod"].ToString() + "</td>" +
                                                  "<td scope='col'>" + row["sResultType"].ToString() + "</td>" +
                            //"<td scope='col'>" + row["sReferenceType"].ToString() + "</td>" +
                            //"<td scope='col'>" + row["sAge"].ToString() + "</td>" +
                            //"<td scope='col'>" + row["sMale"].ToString() + "</td>" +
                            //"<td scope='col'>" + row["sFemale"].ToString() + "</td>" +
                            //"<td scope='col'>" + row["sGrade"].ToString() + "</td>" +
                            //"<td scope='col'>" + row["sUnits"].ToString() + "</td>" +
                            //"<td scope='col'>" + row["sInterpretation"].ToString() + "</td>" +
                            //"<td scope='col'>" + row["sUpperLimit"].ToString() + "</td>" +
                            //"<td scope='col'>" + row["sLowerLimit"].ToString() + "</td>" +
                                                  "<td scope='col'>" + row["sValue"].ToString() + " "+ row["sUnits"].ToString() + "</td>" +
                                                  "<td scope='col'>" + row["sResult"].ToString() + "</td>" +
                                          "<tr>";

                        //edit test values
                        tabTestValueResultEdit += "<tr>" +
                            //"<td scope='col'>" + row["sTestReportValuesId"].ToString() + "</td>" +
                                                  "<td scope='col'>" + row["sAnalyte"].ToString() + "</td>" +
                                                  "<td scope='col'>" + row["sSubAnalyte"].ToString() + "</td>" +
                                                  "<td scope='col'><input type='text' name='txtValue" + row["sTestReportValuesId"].ToString() + "' value='" + row["sValue"].ToString() + "'  clientIdMode='static'/></td>";

                        tabTestValueResultEdit += "<td scope='col'><select name='selResult" + row["sTestReportValuesId"].ToString() + "' clientIdMode='static'>";

                        if (row["sResultType"].ToString().ToLower() == "quantitative")
                        {
                            string normal = (row["sResult"].ToString().ToLower() == "normal") ? "selected='selected'" : "";
                            string low = (row["sResult"].ToString().ToLower() == "low") ? "selected='selected'" : "";
                            string high = (row["sResult"].ToString().ToLower() == "high") ? "selected='selected'" : "";

                            tabTestValueResultEdit += "<option value='normal' " + normal + ">Normal</option>" +
                                                "<option value='low' " + low + ">Low</option>" +
                                                "<option value='high' " + high + ">High</option>";
                        }
                        else if (row["sResultType"].ToString().ToLower() == "qualitative")
                        {
                            string negative = (row["sResult"].ToString().ToLower() == "negative") ? "selected='selected'" : "";
                            string positive = (row["sResult"].ToString().ToLower() == "positive") ? "selected='selected'" : "";

                            tabTestValueResultEdit += "<option value='negative' " + negative + ">Negative</option>" +
                                                  "<option value='positive' " + positive + ">Positive</option>";
                        }
                        else if (row["sResultType"].ToString().ToLower() == "descriptive")
                        {
                            string normal = (row["sResult"].ToString().ToLower() == "normal") ? "selected='selected'" : "";
                            string benign = (row["sResult"].ToString().ToLower() == "benign") ? "selected='selected'" : "";
                            string preMalignant = (row["sResult"].ToString().ToLower() == "pre-malignant") ? "selected='selected'" : "";
                            string malignant = (row["sResult"].ToString().ToLower() == "malignant") ? "selected='selected'" : "";

                            tabTestValueResultEdit += "<option value='normal' " + normal + ">Normal</option>" +
                            "<option value='benign' " + benign + ">Benign</option>" +
                            "<option value='pre-malignant' " + preMalignant + ">Pre-Malignant</option>" +
                            "<option value='malignant' " + malignant + ">Malignant</option>";
                        }

                        tabTestValueResultEdit += "</select></td>" +
                                       "</tr>";
                    }
                }
        }

        hiddenValueIdList.Value = hiddenValueIdList.Value.TrimStart(',').TrimEnd(',');
        tbodyTestValueResult.InnerHtml = tabTestValueResult;
        tbodyTestValueResultEdit.InnerHtml = tabTestValueResultEdit;

        if (spanApprovalStatus.InnerText != "approval pending")
        {
            divApproveReject.Style["display"] = "none";            
        }

        if (spanApprovalStatus.InnerText != "rejected")
        {
            btnEditReport.Style["display"] = "none";
        }

        if (spanApprovalStatus.InnerText == "approval pending")
        {
            if (Request.Cookies["role"].Value.ToString().ToLower().Contains("owner") || Request.Cookies["role"].Value.ToString().ToLower().Contains("supervisor"))
            { }
            else
            {
                divApproveReject.Style["display"] = "none";            
            }
        }

        if (spanApprovalStatus.InnerText == "rejected")
        {
            if (Request.Cookies["role"].Value.ToString().ToLower().Contains("owner") || Request.Cookies["role"].Value.ToString().ToLower().Contains("supervisor"))
            { }
            else
            {
                btnEditReport.Style["display"] = "none";
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string bookLabTestId = Request.QueryString["bookLabTestId"].ToString();
        string bookLabId = Request.QueryString["bookId"].ToString();
        string approvalStatus = hiddenReportStatus.Value;
        string comment = txtComment.Text;
        string approvedOn="";
        string approvedBy = "";

        if (approvalStatus == "approved")
        {
            approvedOn = DateTime.Now.ToString();
            approvedBy = Request.Cookies["labUser"].Value.ToString();
        }

        if (objReport.approveRejectReport(bookLabTestId,approvalStatus,approvedOn,approvedBy,comment) == 1)
        {

            if(approvalStatus != "rejected")
            {
            // CAlling For Notification Yogesh 
            DataSet dsBookingDetails = objBookDetails.getBookTestDetailsUserMobId(bookLabId);
           // objBookDetails.sharedReportToDoctor(hdnsPatientId.Value.ToString(),hdnsDoctorId.Value.ToString(), hdnsReportId.Value.ToString());

            if (dsBookingDetails.Tables[0].Rows.Count != 0) {
            string mobnotid = dsBookingDetails.Tables[0].Rows[0]["sdeviceToken"].ToString();
            string labname = dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString();
            string testtime = dsBookingDetails.Tables[0].Rows[0]["sTimeSlot"].ToString();
            string testdate = dsBookingDetails.Tables[0].Rows[0]["stestdate"].ToString();
            string Message = "Your have received a medical report. ";
            string script = "{ sendnotification('" + mobnotid + "', '" + Message + "'); };";
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "updatePanel1Script", script, true);

            
            }
            SavePDF();
            // End CAlling For Notification Yogesh 

            //  CAlling For Inner Bell Notification Yogesh 
            CLSNotification objNotification = new CLSNotification();
            DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId);
            string reportCreatedOn = DateTime.Now.ToString();
            string reportCreatedBy = Request.Cookies["labUser"].Value.ToString();
            objNotification.AppNotification(dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString(), bookLabTestId, "Report Status", "Your have received a medical report.", "1", reportCreatedOn, reportCreatedBy);
            // End CAlling For Inner Bell Notification Yogesh 
       


            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.href='ViewReport.aspx';", true);
            }
            else{
                // CAlling For Notification Yogesh 
                DataSet dsBookingDetails = objBookDetails.getBookTestDetailsUserMobId(bookLabId);
                string mobnotid = dsBookingDetails.Tables[0].Rows[0]["sdeviceToken"].ToString();
                string labname = dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString();
                string testtime = dsBookingDetails.Tables[0].Rows[0]["sTimeSlot"].ToString();
                string testdate = dsBookingDetails.Tables[0].Rows[0]["stestdate"].ToString();
                string Message = "Your medical report has been rejected. ";
                string script = "{ sendnotification('" + mobnotid + "', '" + Message + "'); };";
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "updatePanel1Script", script, true);

               // SavePDF();
                // End CAlling For Notification Yogesh 



                //  CAlling For Inner Bell Notification Yogesh 
                CLSNotification objNotification = new CLSNotification();
                DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId);
                string reportCreatedOn = DateTime.Now.ToString();
                string reportCreatedBy = Request.Cookies["labUser"].Value.ToString();
                objNotification.AppNotification(dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString(), bookLabTestId, "Report Status", "Your medical report has been rejected.", "1", reportCreatedOn, reportCreatedBy);
                // End CAlling For Inner Bell Notification Yogesh 


                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.href='ViewReport.aspx';", true);
            }
        }
        else if (objReport.approveRejectReport(bookLabTestId, approvalStatus, approvedOn, approvedBy, comment) == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
        }
    }

    protected void btnUpdateReport_Click(object sender, EventArgs e)
    {
        string bookLabTestId = Request.QueryString["bookLabTestId"].ToString();
        string bookLabId = Request.QueryString["bookId"].ToString();
        string[] valueIds = hiddenValueIdList.Value.Split(',');
        string notes = txtNotes.Value;

        string queryUpdateReport = "";
        foreach (string testReportValueId in valueIds)
        {
            queryUpdateReport += "update testReportValues set sValue='" + Request.Form["txtValue" + testReportValueId] + "', sResult='" + Request.Form["selResult" + testReportValueId] + "' where sTestReportValuesId='" + testReportValueId + "'";
        }

        if (objReport.updateTestReport(bookLabTestId,queryUpdateReport,notes) == 1)
        {
            // CAlling For Notification Yogesh 
            DataSet dsBookingDetails = objBookDetails.getBookTestDetailsUserMobId(bookLabId);
            string mobnotid = dsBookingDetails.Tables[0].Rows[0]["sdeviceToken"].ToString();
            string labname = dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString();
            string testtime = dsBookingDetails.Tables[0].Rows[0]["sTimeSlot"].ToString();
            string testdate = dsBookingDetails.Tables[0].Rows[0]["stestdate"].ToString();
            string Message = "Your medical report has been updated.";
            string script = "{ sendnotification('" + mobnotid + "', '" + Message + "'); };";
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "updatePanel1Script", script, true);
            // End CAlling For Notification Yogesh 



            //  CAlling For Inner Bell Notification Yogesh 
            CLSNotification objNotification = new CLSNotification();
            DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId);
            string reportCreatedOn = DateTime.Now.ToString();
            string reportCreatedBy = Request.Cookies["labUser"].Value.ToString();
            objNotification.AppNotification(dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString(), bookLabTestId, "Report Status", "Your medical report has been updated.", "1", reportCreatedOn, reportCreatedBy);
            // End CAlling For Inner Bell Notification Yogesh 


            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.href='ViewReport.aspx';", true);
        }
        else if (objReport.updateTestReport(bookLabTestId, queryUpdateReport, notes) == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
        }

    }





    protected void btnsendMail_Click(object sender, EventArgs e)
    {
        try
        {
            LogError.Log("Clicked");
            LogError.Log("Eneterd SendPDF 2 ");
            string bookLabId = Request.QueryString["bookId"].ToString();
            string bookLabTestId = Request.QueryString["bookLabTestId"].ToString();
            string tabTestValueResult = "";
            string notes = txtNotes.Value;

            DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId);

            string useremailid = dsTestReport.Tables[0].Rows[0]["semailid"].ToString();
            string patinetname = dsTestReport.Tables[0].Rows[0]["sPatient"].ToString();

            string labname = dsTestReport.Tables[0].Rows[0]["sLabName"].ToString();
            string labaddress = dsTestReport.Tables[0].Rows[0]["sLabAddress"].ToString();
            string testdate = dsTestReport.Tables[0].Rows[0]["stestdate"].ToString();
            string labimage = dsTestReport.Tables[0].Rows[0]["slablogo"].ToString();
           // string pdffilename = DateTime.UtcNow.ToString("yyyymmddhhmmssffff");

            string malevalue = dsTestReport.Tables[0].Rows[0]["sMale"].ToString();

            objReport.approveRejectReport(bookLabTestId);





            if (malevalue == "NA") malevalue = "--"; else malevalue = malevalue.ToString();
            string femalevalue = dsTestReport.Tables[0].Rows[0]["sFemale"].ToString();


            if (femalevalue == "NA") femalevalue = "--"; else femalevalue = femalevalue.ToString();

            using (StringWriter sw = new StringWriter())
            {
                LogError.Log("Eneterd String");
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();



                    sb.Append(@"<apex:page showHeader='false' applyHtmlTag='false' applyBodyTag='false' renderAs='pdf'><html><head><style> </style> 
</head>      <apex:form ><table width='100%' border='0' cellspacing='0' cellpadding='0' align='center'>");
                    sb.Append("<tbody>");
                    sb.Append("<tr style='border-bottom: 1px solid #000'>");
                    sb.Append("<td style='text-align: center;width: 150px;padding: 15px'><img src='http://visionarylifescience.com/images/" + labimage + "'></td>");
                    sb.Append("<td style='text-align: center;padding: 15px'><h2 style='margin-top: 0'>" + dsTestReport.Tables[0].Rows[0]["sLabName"].ToString() + "</h2><p> " + labaddress + "<br>");
                    //sb.Append("tempor incididunt ut labore et dolore magna aliqua.<br>");
                    sb.Append("Tel:" + dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString() + " </p></td>");
                    sb.Append("<td style='width: 150px'></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='3'> </td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='3'><h2 style='text-align: center;margin-top: 20px'>Pathological Report</h2></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='padding: 15px' colspan='3'>");
                    sb.Append("<table border = '1' width='100%' style='border: 1px solid #000;width: 100%;'>");
                    sb.Append("<tbody>");
                    sb.Append("<tr>");
                    sb.Append("<td style='padding:5px 10px'>Patient ID: " + dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString() + "</td>");
                    sb.Append("<td id='headerdiv' style='padding:5px 10px'>Patient Name: " + dsTestReport.Tables[0].Rows[0]["sFullName"].ToString() + "</td>");
                    sb.Append("<td style='padding:5px 10px'>Age: 30</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='padding:5px 10px'>Gender: " + dsTestReport.Tables[0].Rows[0]["sGender"].ToString() + "</td>");
                    sb.Append("<td style='padding:5px 10px'>Time: 12:00</td>");
                    sb.Append("<td style='padding:5px 10px'>Date: " + dsTestReport.Tables[0].Rows[0]["sTestDate"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='padding:5px 10px'>Refferd by: " + dsTestReport.Tables[0].Rows[0]["sDoctor"].ToString() + "</td>");
                    sb.Append("<td> </td>");
                    sb.Append("<td> </td>");
                    sb.Append("</tr>");
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='padding: 15px' colspan='3'>");
                    sb.Append("<table border='1' width='100%'  style='border: 1px solid #000;width: 100%;'>");
                    sb.Append("<tbody>");
                    sb.Append("<tr>");
                    sb.Append("<td style='padding:5px 10px'>Report ID: " + dsTestReport.Tables[0].Rows[0]["sBookLabTestId"].ToString() + "</td>");
                    sb.Append("<td style='padding:5px 10px'>Booking ID: " + dsTestReport.Tables[0].Rows[0]["sBookLabId"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='padding:5px 10px'>Tests: " + dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() + " | " + dsTestReport.Tables[0].Rows[0]["sTestName"].ToString() + "</td>");
                    sb.Append("<td> </td>");
                    sb.Append("</tr>");
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style='padding: 15px' colspan='3'>");
                    sb.Append("<table width='100%' border='1' style='border: 1px solid #000;width: 100%;' cellspacing='0' cellpadding='0'>");
                    sb.Append("<thead style='background-color: #f2f2f2'>");
                    sb.Append("<tr style='text-align: left'>");
                    sb.Append("<th style='padding:5px 10px;width: 280px;text-align: left'>Test</th>");
                    sb.Append("<th style='padding:5px 10px;text-align: left'>Result</th>");
                    sb.Append("<th style='padding:5px 10px;text-align: left'>Normal Range</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("<tbody>");
                    //int i = 1;
                    foreach (DataRow row in dsTestReport.Tables[0].Rows)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td  style='padding:5px 10px'>" + row["sAnalyte"].ToString() + " <br> " + row["sSpecimen"].ToString() + " </td>");
                        sb.Append("<td style='padding:5px 10px'>" + row["sValue"].ToString() + " " + row["sUnits"].ToString() +"</td>");
                        sb.Append("<td style='padding:5px 10px'>M : "+ malevalue+ ";F : "+ femalevalue + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");


                    //sb.Append("<tr>");
                    //    sb.Append("<td style='padding: 15px' colspan='3'>");
                    //        sb.Append("<table style='border: 1px solid #000;width: 100%;' cellspacing='0' cellpadding='0'>");
                    //            sb.Append("<thead style='background-color: #f2f2f2'>");
                    //                sb.Append("<tr style='text-align: left'>");
                    //                    sb.Append("<th style='padding:5px 10px;width: 280px;text-align: left'>Differential Leucocyte Count</th>");
                    //                    sb.Append("<th style='padding:5px 10px;text-align: left'>&nbsp;</th>");
                    //                    sb.Append("<th style='padding:5px 10px;text-align: left'>&nbsp;</th>");
                    //                sb.Append("</tr>");
                    //            sb.Append("</thead>");
                    //            sb.Append("<tbody>");
                    //                sb.Append("<tr>");
                    //                    sb.Append("<td style='padding:5px 10px'>Haemoglobin</td>");
                    //                    sb.Append("<td style='padding:5px 10px'>15.10g/dl</td>");
                    //                    sb.Append("<td style='padding:5px 10px'>M : 13.0-17.0;F : 12.0-15.0</td>");
                    //                sb.Append("</tr>");
                    //            sb.Append("</tbody>");
                    //        sb.Append("</table>");
                    //    sb.Append("</td>");
                    //sb.Append("</tr>");


                    sb.Append("<tr>");
                    sb.Append("<td style='padding: 15px' colspan='3'>");
                    sb.Append("<table border='1' width='100%'  style='border: 1px solid #000;width: 100%;' >");
                    sb.Append("<tbody>");
                    sb.Append("<tr style='text-align: left'>");
                    sb.Append("<td style='padding:5px 10px'>Comment : " + dsTestReport.Tables[0].Rows[0]["sComment"].ToString() + "</td>");

                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='padding:5px 10px'>Note : " + dsTestReport.Tables[0].Rows[0]["sNotes"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</tbody>");
                    sb.Append("</table> </apex:form> </html> </apex:page> ");

                    StringReader sr = new StringReader(sb.ToString());

                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

                    iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();

                    styles.LoadTagStyle("#headerdiv", "height", "30px");
                    styles.LoadTagStyle("#headerdiv", "font-weight", "bold");
                    styles.LoadTagStyle("#headerdiv", "font-family", "Cambria");
                    styles.LoadTagStyle("#headerdiv", "font-size", "20px");
                    styles.LoadTagStyle("#headerdiv", "background-color", "Blue");
                    styles.LoadTagStyle("#headerdiv", "color", "White");
                    styles.LoadTagStyle("#headerdiv", "padding-left", "5px");
                    styles.LoadTagStyle(HtmlTags.TH, HtmlTags.BGCOLOR, "Gray");
                    styles.LoadTagStyle(HtmlTags.TH, HtmlTags.CELLPADDING, "5");
                    styles.LoadTagStyle(HtmlTags.TD, HtmlTags.CELLPADDING, "5");
                    styles.LoadTagStyle(HtmlTags.TABLE, HtmlTags.WIDTH, "800");

                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    htmlparser.SetStyleSheet(styles);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        pdfDoc.Close();
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();

                        // for generate pdf 
                        //MemoryStream ms = new MemoryStream(bytes);
                        //FileStream fs = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/images/") + pdffilename + ".pdf", FileMode.Create);
                        //ms.WriteTo(fs);
                        //// clean up
                        //ms.Close();
                        //fs.Close();
                        //fs.Dispose();
                        //objReport.updateTestReportwithPDF(bookLabTestId, notes, pdffilename);








                        SmtpClient smtpClient = new SmtpClient("relay-hosting.secureserver.net", 25);
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new System.Net.NetworkCredential("irealities.qa@gmail.com", "Qatest2707@");
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.EnableSsl = false;

                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("irealities.qa@gmail.com", "Visionary Life Science");
                        mail.To.Add("yp0099@gmail.com");


                        mail.Subject = "Test Report";
                        mail.Body = "Dear Mr/Mrs " + patinetname + ",  Please find attached your Lab Report for the tests taken on " + testdate + ",  Wishing you a quick recovery, " + labname + "";
                        
                        mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Test_Report.pdf"));

                        smtpClient.Send(mail);



                       //// MailMessage mm = new MailMessage("irealities.qa@gmail.com", "yp0099@gmail.com");
                       //// mm.Subject = "Test Report";
                       //// mm.Body = "Dear Mr/Mrs " + patinetname + ",  Please find attached your Lab Report for the tests taken on " + testdate + ",  Wishing you a quick recovery, " + labname + "";
                       //// mm.Attachments.Add(new Attachment(new MemoryStream(bytes),  "Test_Report.pdf"));




                       //// mm.IsBodyHtml = true;
                       //// SmtpClient smtp = new SmtpClient();
                       ////// smtp.Host = "smtp.gmail.com";
                       //// smtp.Host = "relay-hosting.securesrver.net";
                       //// smtp.EnableSsl = false;
                       //// NetworkCredential NetworkCred = new NetworkCredential();
                       //// NetworkCred.UserName = "irealities.qa@gmail.com";
                       //// NetworkCred.Password = "Qatest2707@";
                       //// smtp.UseDefaultCredentials = true;
                       //// smtp.Credentials = NetworkCred;
                       //// smtp.Port = 25;
                       //// try
                       //// {

                       ////     smtp.Send(mm);
                       //// }
                       //// catch (Exception ex)
                       //// {
                       ////     Console.WriteLine(ex);   //Should print stacktrace + details of inner exception
                       ////     LogError.LoggerCatch(ex.InnerException);
                       ////     if (ex.InnerException != null)
                       ////     {
                       ////         LogError.LoggerCatch(ex.InnerException);
                       ////     }
                       //// }

                    }
                }
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Report sent successfully');location.reload();", true);
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
        }
    }



    private void SavePDF()
    {
        LogError.Log("Eneterd SendPDF 1 ");
        try
        {
            LogError.Log("Clicked");
            LogError.Log("Eneterd SendPDF 2 ");
            string bookLabId = Request.QueryString["bookId"].ToString();
            string bookLabTestId = Request.QueryString["bookLabTestId"].ToString();
            string tabTestValueResult = "";
            string notes = txtNotes.Value;

            DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId);

            string useremailid = dsTestReport.Tables[0].Rows[0]["semailid"].ToString();
            string patinetname = dsTestReport.Tables[0].Rows[0]["sPatient"].ToString();

            string labname = dsTestReport.Tables[0].Rows[0]["sLabName"].ToString();
            string labaddress = dsTestReport.Tables[0].Rows[0]["sLabAddress"].ToString();
            string testdate = dsTestReport.Tables[0].Rows[0]["stestdate"].ToString();
            string labimage = dsTestReport.Tables[0].Rows[0]["slablogo"].ToString();
            string pdffilename = DateTime.UtcNow.ToString("yyyymmddhhmmssffff");

            string malevalue = dsTestReport.Tables[0].Rows[0]["sMale"].ToString();
            if (malevalue == "NA") malevalue = "--"; else malevalue = malevalue.ToString();
            string femalevalue = dsTestReport.Tables[0].Rows[0]["sFemale"].ToString();
            if (femalevalue == "NA") femalevalue = "--"; else femalevalue = femalevalue.ToString();

            using (StringWriter sw = new StringWriter())
            {
                LogError.Log("Eneterd String");
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();





                    sb.Append(@"<apex:page showHeader='false' applyHtmlTag='false' applyBodyTag='false' renderAs='pdf'><html><head><style> </style> 
</head>      <apex:form ><table width='100%' border='0' cellspacing='0' cellpadding='0' align='center'>");
		sb.Append("<tbody>");
			sb.Append("<tr style='border-bottom: 1px solid #000'>");
            sb.Append("<td style='text-align: center;width: 150px;padding: 15px'><img src='http://visionarylifescience.com/images/" + labimage + "'></td>");
				sb.Append("<td style='text-align: center;padding: 15px'><h2 style='margin-top: 0'>" + dsTestReport.Tables[0].Rows[0]["sLabName"].ToString() + "</h2><p> " +labaddress +"<br>");
				//sb.Append("tempor incididunt ut labore et dolore magna aliqua.<br>");
                sb.Append("Tel:" + dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString() + " </p></td>");
				sb.Append("<td style='width: 150px'></td>");
			sb.Append("</tr>");
			sb.Append("<tr>");
				sb.Append("<td colspan='3'> </td>");
			sb.Append("</tr>");
			sb.Append("<tr>");
				sb.Append("<td colspan='3'><h2 style='text-align: center;margin-top: 20px'>Pathological Report</h2></td>");
			sb.Append("</tr>");
			sb.Append("<tr>");
				sb.Append("<td style='padding: 15px' colspan='3'>");
                sb.Append("<table border = '1' width='100%' style='border: 1px solid #000;width: 100%;'>");
						sb.Append("<tbody>");
							sb.Append("<tr>");
                            sb.Append("<td style='padding:5px 10px'>Patient ID: " + dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString() + "</td>");
                            sb.Append("<td id='headerdiv' style='padding:5px 10px'>Patient Name: " + dsTestReport.Tables[0].Rows[0]["sFullName"].ToString() + "</td>");
								sb.Append("<td style='padding:5px 10px'>Age: 30</td>");
							sb.Append("</tr>");
							sb.Append("<tr>");
                            sb.Append("<td style='padding:5px 10px'>Gender: " + dsTestReport.Tables[0].Rows[0]["sGender"].ToString() + "</td>");
								sb.Append("<td style='padding:5px 10px'>Time: 12:00</td>");
                                sb.Append("<td style='padding:5px 10px'>Date: " + dsTestReport.Tables[0].Rows[0]["sTestDate"].ToString() + "</td>");
							sb.Append("</tr>");
							sb.Append("<tr>");
                            sb.Append("<td style='padding:5px 10px'>Refferd by: " + dsTestReport.Tables[0].Rows[0]["sDoctor"].ToString() + "</td>");
                            sb.Append("<td> </td>");
                            sb.Append("<td> </td>");
							sb.Append("</tr>");
						sb.Append("</tbody>");
					sb.Append("</table>");
				sb.Append("</td>");
			sb.Append("</tr>");
			sb.Append("<tr>");
				sb.Append("<td style='padding: 15px' colspan='3'>");
                sb.Append("<table border='1' width='100%'  style='border: 1px solid #000;width: 100%;'>");
						sb.Append("<tbody>");
							sb.Append("<tr>");
                            sb.Append("<td style='padding:5px 10px'>Report ID: " + dsTestReport.Tables[0].Rows[0]["sBookLabTestId"].ToString() + "</td>");
                            sb.Append("<td style='padding:5px 10px'>Booking ID: " + dsTestReport.Tables[0].Rows[0]["sBookLabId"].ToString() + "</td>");
							sb.Append("</tr>");
							sb.Append("<tr>");
                            sb.Append("<td style='padding:5px 10px'>Tests: " + dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() + " | " + dsTestReport.Tables[0].Rows[0]["sTestName"].ToString() + "</td>");
                            sb.Append("<td> </td>");
							sb.Append("</tr>");
						sb.Append("</tbody>");
					sb.Append("</table>");
				sb.Append("</td>");
			sb.Append("</tr>");

			sb.Append("<tr>");
				sb.Append("<td style='padding: 15px' colspan='3'>");
					sb.Append("<table width='100%' border='1' style='border: 1px solid #000;width: 100%;' cellspacing='0' cellpadding='0'>");
						sb.Append("<thead style='background-color: #f2f2f2'>");
							sb.Append("<tr style='text-align: left'>");
								sb.Append("<th style='padding:5px 10px;width: 280px;text-align: left'>Test</th>");
								sb.Append("<th style='padding:5px 10px;text-align: left'>Result</th>");
								sb.Append("<th style='padding:5px 10px;text-align: left'>Normal Range</th>");
							sb.Append("</tr>");
						sb.Append("</thead>");
						sb.Append("<tbody>");
                     //int i = 1;
                     foreach (DataRow row in dsTestReport.Tables[0].Rows)
                     {
                         sb.Append("<tr>");
                         sb.Append("<td  style='padding:5px 10px'>" + row["sAnalyte"].ToString() + " <br> " + row["sSpecimen"].ToString() + " </td>");
                         sb.Append("<td style='padding:5px 10px'>" + row["sValue"].ToString() + " " + row["sUnits"].ToString() + "</td>");
                         sb.Append("<td style='padding:5px 10px'>M : " + malevalue + ";F : " + femalevalue + "</td>");
                         sb.Append("</tr>");
                     }
						sb.Append("</tbody>");
					sb.Append("</table>");
				sb.Append("</td>");
			sb.Append("</tr>");


            //sb.Append("<tr>");
            //    sb.Append("<td style='padding: 15px' colspan='3'>");
            //        sb.Append("<table style='border: 1px solid #000;width: 100%;' cellspacing='0' cellpadding='0'>");
            //            sb.Append("<thead style='background-color: #f2f2f2'>");
            //                sb.Append("<tr style='text-align: left'>");
            //                    sb.Append("<th style='padding:5px 10px;width: 280px;text-align: left'>Differential Leucocyte Count</th>");
            //                    sb.Append("<th style='padding:5px 10px;text-align: left'>&nbsp;</th>");
            //                    sb.Append("<th style='padding:5px 10px;text-align: left'>&nbsp;</th>");
            //                sb.Append("</tr>");
            //            sb.Append("</thead>");
            //            sb.Append("<tbody>");
            //                sb.Append("<tr>");
            //                    sb.Append("<td style='padding:5px 10px'>Haemoglobin</td>");
            //                    sb.Append("<td style='padding:5px 10px'>15.10g/dl</td>");
            //                    sb.Append("<td style='padding:5px 10px'>M : 13.0-17.0;F : 12.0-15.0</td>");
            //                sb.Append("</tr>");
            //            sb.Append("</tbody>");
            //        sb.Append("</table>");
            //    sb.Append("</td>");
            //sb.Append("</tr>");


			sb.Append("<tr>");
				sb.Append("<td style='padding: 15px' colspan='3'>");
                sb.Append("<table border='1' width='100%'  style='border: 1px solid #000;width: 100%;' >");
						sb.Append("<tbody>");
							sb.Append("<tr style='text-align: left'>");
                            sb.Append("<td style='padding:5px 10px'>Comment : " + dsTestReport.Tables[0].Rows[0]["sComment"].ToString() + "</td>");

							sb.Append("</tr>");
							sb.Append("<tr>");
                            sb.Append("<td style='padding:5px 10px'>Note : " + dsTestReport.Tables[0].Rows[0]["sNotes"].ToString() + "</td>");
							sb.Append("</tr>");
						sb.Append("</tbody>");
					sb.Append("</table>");
				sb.Append("</td>");
			sb.Append("</tr>");
		sb.Append("</tbody>");
        sb.Append("</table> </apex:form> </html> </apex:page> ");
                    StringReader sr = new StringReader(sb.ToString());

                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

                    iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();

                    styles.LoadTagStyle("#headerdiv", "height", "30px");
                    styles.LoadTagStyle("#headerdiv", "font-weight", "bold");
                    styles.LoadTagStyle("#headerdiv", "font-family", "Cambria");
                    styles.LoadTagStyle("#headerdiv", "font-size", "20px");
                    styles.LoadTagStyle("#headerdiv", "background-color", "Blue");
                    styles.LoadTagStyle("#headerdiv", "color", "White");
                    styles.LoadTagStyle("#headerdiv", "padding-left", "5px");
                    styles.LoadTagStyle(HtmlTags.TH, HtmlTags.BGCOLOR, "Gray");
                    styles.LoadTagStyle(HtmlTags.TH, HtmlTags.CELLPADDING, "5");
                    styles.LoadTagStyle(HtmlTags.TD, HtmlTags.CELLPADDING, "5");
                    styles.LoadTagStyle(HtmlTags.TABLE, HtmlTags.WIDTH, "800");

                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    htmlparser.SetStyleSheet(styles);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();
                        htmlparser.StartDocument();
                        htmlparser.Parse(sr);
                        htmlparser.EndDocument();
                        pdfDoc.Close();
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();

                        MemoryStream ms = new MemoryStream(bytes);
                        FileStream fs = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/images/reportPDF/") + pdffilename + ".pdf", FileMode.Create);
                        ms.WriteTo(fs);
                        // clean up
                        ms.Close();
                        fs.Close();
                        fs.Dispose();

                        objReport.updateTestReportwithPDF(bookLabTestId, notes, pdffilename);
                                              

                    }
                }
            }
          //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Mail Send successfully');location.reload();", true);
        }

        catch (Exception e)
        {
            LogError.LoggerCatch(e);
        }
    }




}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PatientReportHistoryManagementDetails : System.Web.UI.Page
{
    CLSPatientManagementViewHealthProfile objPatientManagementViewHealthProfile = new CLSPatientManagementViewHealthProfile();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadReport();
        }
    }

    protected void loadReport()
    {
        int bookLabId = Convert.ToInt32(Request.QueryString["bookId"].ToString());
        int bookLabTestId = Convert.ToInt32(Request.QueryString["bookLabTestId"].ToString());
        string tabTestValueResult = "";
        string tabTestValueResultEdit = "";

        DataSet dsTestReport = objPatientManagementViewHealthProfile.getTestReport(bookLabTestId.ToString());

        if (dsTestReport != null)
        {
            if (dsTestReport.Tables[0].Rows.Count > 0)
            {
                spanLabName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabName"].ToString();
                spanLabContact.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString();
                spanLabAddress.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabAddress"].ToString();
                spanBookingId.InnerHtml = dsTestReport.Tables[0].Rows[0]["sBookLabId"].ToString();
                spanReportId.InnerHtml = dsTestReport.Tables[0].Rows[0]["sBookLabTestId"].ToString();
                spanPatientName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sPatient"].ToString();
                spanGender.InnerHtml = dsTestReport.Tables[0].Rows[0]["sGender"].ToString();
                spanDoctorName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sDoctor"].ToString();
                spanTestTakenOn.InnerHtml = dsTestReport.Tables[0].Rows[0]["sTestDate"].ToString();
                spanReportCreatedOn.InnerHtml = dsTestReport.Tables[0].Rows[0]["sReportCreatedOn"].ToString();
                spanReportCreatedBy.InnerHtml = dsTestReport.Tables[0].Rows[0]["sReportCreatedBy"].ToString();
                spanApprovalStatus.InnerHtml = dsTestReport.Tables[0].Rows[0]["sApprovalStatus"].ToString();
                spanComment.InnerHtml = dsTestReport.Tables[0].Rows[0]["sComment"].ToString();
                spanTestCodeName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() + ", " + dsTestReport.Tables[0].Rows[0]["sTestName"].ToString();
                spanNotes.InnerHtml = dsTestReport.Tables[0].Rows[0]["sNotes"].ToString();
                txtNotes.Value = dsTestReport.Tables[0].Rows[0]["sNotes"].ToString();

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
                                              "<td scope='col'>" + row["sValue"].ToString() + "</td>" +
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
}
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using System.Globalization;
using CrossPlatformAESEncryption.Helper;
using System.Web.Services;
using DataAccessHandler;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public partial class ViewReportValues : System.Web.UI.Page
{
    ClsCreateReport objReport1 = new ClsCreateReport();
    ClsViewReport objReport = new ClsViewReport();
    ClsBookDetails objBookDetails = new ClsBookDetails();
    string patientAge = "";
    CLSViewReportValuesinTemplate objTemplate = new CLSViewReportValuesinTemplate();
    CLSTemplateBuilder objTemplateBuilder = new CLSTemplateBuilder();
    ClsAppNotification objAppNotify = new ClsAppNotification();
    ClsFCMNotification ObjFCM = new ClsFCMNotification();
    string PaymentStatus = "";
DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    static Dictionary<string, string> CalculateYourAge(DateTime Dob)
    {

        Dictionary<string, string> age = new Dictionary<string, string>();
        DateTime dateOfBirth;
        DateTime.TryParse(Dob.ToString(), out dateOfBirth);
        DateTime currentDate = DateTime.Now;
        TimeSpan difference = currentDate.Subtract(dateOfBirth);
        DateTime Age = DateTime.MinValue + difference;
        int ageInYears = Age.Year - 1;
        int ageInMonths = Age.Month - 1;
        int ageInDays = Age.Day - 1;

        age.Add("Years", ageInYears.ToString());
        age.Add("Months", ageInMonths.ToString());
        age.Add("Days", ageInDays.ToString());

        return age;
    }
    protected void loadReport()
    {
        try
        {
            string LabId = Request.Cookies["labId"].Value.ToString();
            int bookLabId = Convert.ToInt32(Request.QueryString["bookId"].ToString());
            int bookLabTestId = Convert.ToInt32(Request.QueryString["bookLabTestId"].ToString());
            string tabTestValueResult = "";
            string tabTestValueResultEdit = "";

            DataSet dsTestReport = objReport.getTestReport(bookLabTestId.ToString(),LabId);
            if (dsTestReport.Tables[0].Rows.Count > 0)
            {
                string DateOfBirth = dsTestReport.Tables[0].Rows[0]["sBirthDate"].ToString();
                DateTime Dob;
                DateTime dtDob;
                if (DateTime.TryParseExact(DateOfBirth, "dd/MM/yyyy", null, DateTimeStyles.None, out Dob))
                {
                    dtDob = Dob;
                }
                else
                {
                    dtDob = Convert.ToDateTime(DateOfBirth);
                }
                if (CalculateYourAge(dtDob)["Years"] != "0")
                {
                    patientAge = CalculateYourAge(dtDob)["Years"] + " year";
                }
                else if (CalculateYourAge(dtDob)["Months"] != "0")
                {
                    patientAge = CalculateYourAge(dtDob)["Months"] + " month";
                }
                else if (CalculateYourAge(dtDob)["Days"] != "0")
                {
                    patientAge = CalculateYourAge(dtDob)["Days"] + " day";
                }
                if (dsTestReport != null)
                {
                    string availpdffile = dsTestReport.Tables[0].Rows[0]["sApprovalStatus"].ToString();
                    string reportPage = (dsTestReport.Tables[0].Rows[0]["reportPage"].ToString() == "") ? "Report.aspx" : dsTestReport.Tables[0].Rows[0]["reportPage"].ToString();
                    //if (availpdffile != "")
                    //    viewpdf.InnerHtml = "<a href='http://visionarylifescience.com/images/reportPDF/" + availpdffile + ".pdf'  target='_blank' class='pdf-btn'> View Pdf </a>";
                    if (availpdffile.ToLower() == "approved")
                        viewpdf.InnerHtml = "<a href='https://visionarylifescience.com/" + reportPage + "?bookLabTestId=" + bookLabTestId + "'  target='_blank' class='btn btn-color'> View Report </a>";

                    else viewpdf.InnerHtml = "";

                    if (dsTestReport.Tables[0].Rows.Count > 0)
                    {
						 hdntestName.Value = dsTestReport.Tables[0].Rows[0]["sTestName"].ToString();
                        spanAge.InnerHtml = patientAge;
                        spanLabName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabName"].ToString();
                        spanLabContact.InnerHtml = (dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString() != "") ? CryptoHelper.Decrypt(dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString()) : "";
                        spanLabAddress.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabAddress"].ToString();
                        spanBookingId.InnerHtml = dsTestReport.Tables[0].Rows[0]["sBookLabId"].ToString();
                        spanReportId.InnerHtml = dsTestReport.Tables[0].Rows[0]["sBookLabTestId"].ToString();
                        if (dsTestReport.Tables[0].Rows[0]["sPaymentStatus"].ToString().ToLower() != "paid")
                        {
							  patst.Visible = true;
                            lblpaystatus.Text = dsTestReport.Tables[0].Rows[0]["sPaymentStatus"].ToString();
                            spanPaymentStatus.Style.Value = "color:red;font-weight:bold";
                        }
                        PaymentStatus = dsTestReport.Tables[0].Rows[0]["sPaymentStatus"].ToString();
                        spanPaymentStatus.InnerHtml = PaymentStatus;
                        spanPatientName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sPatient"].ToString();
                        spanGender.InnerHtml = dsTestReport.Tables[0].Rows[0]["sGender"].ToString();
                        string sGender = dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToString().ToLower();
                        //spanApprovalRange.InnerHtml = Range;
                        spanDoctorName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sDoctor"].ToString();
                        spanTestTakenOn.InnerHtml = dsTestReport.Tables[0].Rows[0]["sBookRequestedAt"].ToString();
                        // string DateTime1 = dsTestReport.Tables[0].Rows[0]["sReportCreatedOn"].ToString();
                        htestCode.Value = dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString();

                        spanReportCreatedOn.InnerHtml = dsTestReport.Tables[0].Rows[0]["sReportCreatedOn"].ToString();
                        spanReportCreatedBy.InnerHtml = dsTestReport.Tables[0].Rows[0]["sReportCreatedBy"].ToString();
                        spanApprovalStatus.InnerHtml = dsTestReport.Tables[0].Rows[0]["sApprovalStatus"].ToString();
                        if (dsTestReport.Tables[0].Rows[0]["sApprovalStatus"].ToString().ToLower() != "approved")
                        {
                            Button1.Visible = false;
                        }
                        spanComment.InnerHtml = dsTestReport.Tables[0].Rows[0]["sComment"].ToString();
                        spanTestCodeName.InnerHtml = dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() + ", " + dsTestReport.Tables[0].Rows[0]["sTestName"].ToString();
                        spanNotes.InnerHtml = dsTestReport.Tables[0].Rows[0]["sNotes"].ToString();
                        txtNotes.Value = dsTestReport.Tables[0].Rows[0]["sNotes"].ToString();
                        hdnsPatientId.Value = dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString();
                        hdnsDoctorId.Value = dsTestReport.Tables[0].Rows[0]["sDoctorid"].ToString();
                        hdnsReportId.Value = bookLabTestId.ToString();

                        tabTestValueResultEdit += "<tr>" +
                                                        "<th scope='col'>Analyte</th>" +
                                                        "<th scope='col'>Subanalyte</th>" +
                                                        "<th scope='col'>Value</th>" +
                                                        "<th scope='col'>Result</th>" +
                                                    "</tr>";
                        int count = 1;
                        foreach (DataRow row in dsTestReport.Tables[0].Rows)
                        {
                            hiddenValueIdList.Value += row["sTestReportValuesId"].ToString() + ",";
                            //display test values
                            if (row["sAnalyte"].ToString() != "")
                            {
                                if (sGender.ToLower() == "male")
                                {
                                    tabTestValueResult += "<li class='table-row'>" +
                                                              "<div class='col col-1 text-center'>" + row["sAnalyte"].ToString() + "</div>" +
                                                              "<div class='col col-2 text-center'>" + row["sSubAnalyte"].ToString() + "</div>" +
                                                              "<div class='col col-3 text-center'>" + row["sSpecimen"].ToString() + "</div>" +
                                                              "<div class='col col-4 text-center'>" + row["sMethod"].ToString() + "</div>" +
                                                              "<div class='col col-5 text-center'>" + row["sMale"].ToString() + "</div>" +
                                                              "<div class='col col-6 text-center '>" + row["sResultType"].ToString() + "</div>" +

                                                               "<div class='col col-7 text-center'>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + "</div>" +
                                                              //    "<div class='col col-7 text-center'>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + " " + row["sUnits"].ToString() + "</div>" +
                                                              "<div class='col col-8 text-center'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</div>" +
                                                      "</li>";
                                }
                                else
                                {
                                    tabTestValueResult += "<li class='table-row'>" +
                                                              "<div class='col col-1 text-center'>" + row["sAnalyte"].ToString() + "</div>" +
                                                              "<div class='col col-2 text-center'>" + row["sSubAnalyte"].ToString() + "</div>" +
                                                              "<div class='col col-3 text-center'>" + row["sSpecimen"].ToString() + "</div>" +
                                                              "<div class='col col-4 text-center'>" + row["sMethod"].ToString() + "</div>" +
                                                              "<div class='col col-5 text-center'>" + row["sFemale"].ToString() + "</div>" +
                                                              "<div class='col col-6 text-center '>" + row["sResultType"].ToString() + "</div>" +
                                                             // "<div class='col col-7 text-center'>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + " " + row["sUnits"].ToString() + "</div>" +

                                                                "<div class='col col-7 text-center'>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + "</div>" +
                                                             "<div class='col col-8 text-center'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</div>" +
                                                      "</li>";
                                }
                            }
                            //edit test values
                            tabTestValueResultEdit += "<tr>" +
                                                      "<td scope='col'>" + row["sAnalyte"].ToString() + "</td>" +
                                                      "<td scope='col'>" + row["sSubAnalyte"].ToString() + "</td>" +
                                                      "<td scope='col'><input type='text' class='form-control'  name='txtValue" + row["sTestReportValuesId"].ToString() + "' value='" + CryptoHelper.Decrypt(row["sValue"].ToString()) + "'  clientIdMode='static'/></td>";

                            tabTestValueResultEdit += "<td scope='col'><select class='form-control' name='selResult" + row["sTestReportValuesId"].ToString() + "' id='resultA" + count + "' clientIdMode='static'>";
                            tabTestValueResultEdit += "<option value='" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</option>";

                            if (row["sResultType"].ToString().ToLower() == "quantitative")
                            {
                                if (sGender.ToLower() == "male")
                                {
                                    string Testcode = row["sTestCode"].ToString().Trim();
                                    switch (Testcode.ToLower())
                                    {
                                        case "afp":
                                            if (row["sProfileName"].ToString().ToLower().Trim() == "cancer profile")
                                            {
                                                tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                      "<option value='Possiblity of tumor'>Possiblity of tumor</option>";
                                            }
                                            if (row["sProfileName"].ToString().Trim() == "PREGNANCY PROFILE")
                                            {
                                                tabTestValueResultEdit += "<option value='Non-Pregnant'>Non-Pregnant</option>" +
                                                                      "<option value='Pregnant'>Pregnant</option>" +
                                                                      "<option value='New Born'>New Born</option>";
                                            }
                                            break;
                                        case "fsh":
                                            tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                                    "<option value='Premenopausal Follicular'>Premenopausal Follicular</option>" +
                                                                    "<option value='Premenopausal Midcycle'>Premenopausal Midcycle</option>" +
                                                                    "<option value='Premenopausal Luteal'>Premenopausal Luteal</option>" +
                                                                    "<option value='Postmenopausal'>Postmenopausal</option>";

                                            break;
                                        case "lh":
                                            tabTestValueResultEdit += "<option value='NA'>NA</option>";
                                            break;
                                        case "pgsn":
                                            tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                                      "<option value='Follicular Phase'>Follicular Phase</option>" +
                                                                      "<option value='Ovulation Phase'>Ovulation Phase</option>" +
                                                                      "<option value='Luteal Phase'>Luteal Phase</option>" +
                                                                      "<option value='1st Trimester Pregnancy'>1st Trimester Pregnancy</option>" +
                                                                      "<option value='2nd Trimester Pregnancy'>2nd Trimester Pregnancy</option>" +
                                                                      "<option value='3rd Trimester Pregnancy'>3rd Trimester Pregnancy</option>";
                                            break;
                                        case "thcg":
                                            tabTestValueResultEdit += "<option value='Negative'>Negative</option>" +
                                                                       "<option value='Indeterminate'>Indeterminate</option>" +
                                                                       "<option value='Positive'>Positive</option>";
                                            break;
                                        case "semb":
                                            tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                                       "<option value='Positive'>Positive</option>" +
                                                                        "<option value='Normal'>Normal</option>";
                                            break;

                                        case "ca19":
                                            tabTestValueResultEdit += "<option value='Negative'>Negative</option>" +
                                                                  "<option value='Positive'>Positive</option>" +
                                                                  "<option value='Normal'>Normal</option>";
                                            break;
                                        case "psaft":
                                            tabTestValueResultEdit += "<option value='49% to 65% risk of prostate cancer depending on age'>49% to 65% risk of prostate cancer depending on age</option>" +
                                                                       "<option value='27% to 41% risk of prostate cancer depending on age'>27% to 41% risk of prostate cancer depending on age</option>" +
                                                                        "<option value='18% to 30% risk of prostate cancer depending on age'>18% to 30% risk of prostate cancer depending on age</option>" +
                                                                        "<option value='9% to 16% risk of prostate cancer depending on age'>9% to 16% risk of prostate cancer depending on age</option>";
                                            break;
                                        case "ca25":
                                            tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                                   "<option value='Positive'>Positive</option>" +
                                                                   "<option value='Normal'>Normal</option>";
                                            break;
                                        case "hba1c":
                                            tabTestValueResultEdit += "<option value='Normal'>Normal</option>" +
                                                                   "<option value='Pre-diabetic'>Pre-diabetic</option>" +
                                                                   "<option value='Diabetic'>Diabetic</option>";
                                            break;
                                        case "ca153":
                                            tabTestValueResultEdit += "<option value='NA'>NA</option>";
                                            break;
                                        case "phsm":
                                            tabTestValueResultEdit += "<option value='Vaccinated'>Vaccinated</option>" +
                                                                  "<option value='Unvaccinated'>Unvaccinated</option>" +
                                                                  "<option value='Positive'>Positive</option>" +
                                                                  "<option value='Negative'>Negative</option>" +
                                                                  "<option value='NA'>NA</option>";
                                            break;
                                        case "qft3":
                                            tabTestValueResultEdit += "<option value='Strong Positive'>Strong Positive</option>" +
                                                                  "<option value='Positive'>Positive</option>" +
                                                                  "<option value='Negative'>Negative</option>" +
                                                                  "<option value='NA'>NA</option>";
                                            break;
                                        case "hepb":
                                            tabTestValueResultEdit += "<option value='Vaccinated'>Vaccinated</option>" +
                                                                  "<option value='Unvaccinated'>Unvaccinated</option>" +
                                                                  "<option value='Positive'>Positive</option>" +
                                                                  "<option value='Negative'>Negative</option>" +
                                                                  "<option value='NA'>NA</option>";
                                            break;
                                        case "flepm":
                                            tabTestValueResultEdit += "<option value='Equivocal'>Equivocal</option>" +
                                                                  "<option value='Positive'>Positive</option>" +
                                                                  "<option value='Negative'>Negative</option>" +
                                                                  "<option value='NA'>NA</option>";
                                            break;
                                        default:
                                            {
                                                if (Testcode.ToLower() == "ahpm" || Testcode.ToLower() == "ige" || Testcode.ToLower() == "CHIKV" || Testcode.ToLower() == "cmvp"
                                                    || Testcode.ToLower() == "dengm" || Testcode.ToLower() == "dnsag" || Testcode.ToLower() == "hav" || Testcode.ToLower() == "corab"
                                                    || Testcode.ToLower() == "heag" || Testcode.ToLower() == "hcscr" || Testcode.ToLower() == "hcvl" || Testcode.ToLower() == "hevg"
                                                    || Testcode.ToLower() == "hevm" || Testcode.ToLower() == "mhsv" || Testcode.ToLower() == "hsmr" || Testcode.ToLower() == "hsvg"
                                                    || Testcode.ToLower() == "vhsv" || Testcode.ToLower() == "vdrl")
                                                {
                                                    tabTestValueResultEdit += "<option value='Positive'>Positive</option>" +
                                                               "<option value='Negative'>Negative</option>" +
                                                               "<option value='NA'>NA</option>";
                                                }
                                                else
                                                {
                                                    DataSet dsgetsInterpretation = objReport1.getInterpretationResult(row["sTestCode"].ToString());
                                                    if (dsgetsInterpretation.Tables[0].Rows.Count > 0)
                                                    {
                                                        foreach (DataRow row1 in dsgetsInterpretation.Tables[0].Rows)
                                                        {
                                                            tabTestValueResultEdit += "<option value='" + row1["Interpretation"].ToString() + "'>" + row1["Interpretation"].ToString() + "</option>";
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    string Testcode = row["sTestCode"].ToString().Trim();
                                    switch (Testcode.ToLower())
                                    {
                                        case "afp":
                                            if (row["sProfileName"].ToString().ToLower().Trim() == "cancer profile")
                                            {
                                                tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                      "<option value='Possiblity of tumor'>Possiblity of tumor</option>";
                                            }
                                            if (row["sProfileName"].ToString().Trim() == "PREGNANCY PROFILE")
                                            {
                                                tabTestValueResultEdit += "<option value='Non-Pregnant'>Non-Pregnant</option>" +
                                                                      "<option value='Pregnant'>Pregnant</option>" +
                                                                      "<option value='New Born'>New Born</option>";
                                            }
                                            break;
                                        case "fsh":
                                            tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                                    "<option value='Premenopausal Follicular'>Premenopausal Follicular</option>" +
                                                                    "<option value='Premenopausal Midcycle'>Premenopausal Midcycle</option>" +
                                                                    "<option value='Premenopausal Luteal'>Premenopausal Luteal</option>" +
                                                                    "<option value='Postmenopausal'>Postmenopausal</option>";

                                            break;
                                        case "lh":
                                            if (row["sProfileName"].ToString().ToUpper().Trim() == "PITUITORY PROFILE" || row["sProfileName"].ToString().ToUpper().Trim() == "INFERTILITY PROFILE")
                                            {
                                                tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                                      "<option value='Pregnant'>Pregnant</option>" +
                                                                      "<option value='Menopause'>Menopause</option>";
                                            }
                                            if (row["sProfileName"].ToString().ToUpper().Trim() == "HORMONAL PROFILE")
                                            {
                                                tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                                      "<option value='Pregnant'>Pregnant</option>" +
                                                                      "<option value='Menopause'>Menopause</option>" +
                                                                      "<option value='Follicular'>Follicular</option>" +
                                                                      "<option value='Ovulation'>Ovulation</option>";
                                            }
                                            break;
                                        case "pgsn":
                                            tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                                      "<option value='Follicular Phase'>Follicular Phase</option>" +
                                                                      "<option value='Ovulation Phase'>Ovulation Phase</option>" +
                                                                      "<option value='Luteal Phase'>Luteal Phase</option>" +
                                                                      "<option value='1st Trimester Pregnancy'>1st Trimester Pregnancy</option>" +
                                                                      "<option value='2nd Trimester Pregnancy'>2nd Trimester Pregnancy</option>" +
                                                                      "<option value='3rd Trimester Pregnancy'>3rd Trimester Pregnancy</option>";
                                            break;
                                        case "thcg":
                                            tabTestValueResultEdit += "<option value='Negative'>Negative</option>" +
                                                                       "<option value='Indeterminate'>Indeterminate</option>" +
                                                                       "<option value='Positive'>Positive</option>";
                                            break;
                                        case "semb":
                                            tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                                       "<option value='Positive'>Positive</option>" +
                                                                        "<option value='Normal'>Normal</option>";
                                            break;

                                        case "ca19":
                                            tabTestValueResultEdit += "<option value='Negative'>Negative</option>" +
                                                                  "<option value='Positive'>Positive</option>" +
                                                                  "<option value='Normal'>Normal</option>";
                                            break;
                                        case "psaft":
                                            tabTestValueResultEdit += "<option value='49% to 65% risk of prostate cancer depending on age'>49% to 65% risk of prostate cancer depending on age</option>" +
                                                                       "<option value='27% to 41% risk of prostate cancer depending on age'>27% to 41% risk of prostate cancer depending on age</option>" +
                                                                        "<option value='18% to 30% risk of prostate cancer depending on age'>18% to 30% risk of prostate cancer depending on age</option>" +
                                                                        "<option value='9% to 16% risk of prostate cancer depending on age'>9% to 16% risk of prostate cancer depending on age</option>";
                                            break;
                                        case "ca25":
                                            tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                                   "<option value='Positive'>Positive</option>" +
                                                                   "<option value='Normal'>Normal</option>";
                                            break;
                                        case "hba1c":
                                            tabTestValueResultEdit += "<option value='Normal'>Normal</option>" +
                                                                   "<option value='Pre-diabetic'>Pre-diabetic</option>" +
                                                                   "<option value='Diabetic'>Diabetic</option>";
                                            break;
                                        case "ca153":
                                            tabTestValueResultEdit += "<option value='NA'>NA</option>";
                                            break;
                                        case "phsm":
                                            tabTestValueResultEdit += "<option value='Vaccinated'>Vaccinated</option>" +
                                                                  "<option value='Unvaccinated'>Unvaccinated</option>" +
                                                                  "<option value='Positive'>Positive</option>" +
                                                                  "<option value='Negative'>Negative</option>" +
                                                                  "<option value='NA'>NA</option>";
                                            break;
                                        case "qft3":
                                            tabTestValueResultEdit += "<option value='Strong Positive'>Strong Positive</option>" +
                                                                  "<option value='Positive'>Positive</option>" +
                                                                  "<option value='Negative'>Negative</option>" +
                                                                  "<option value='NA'>NA</option>";
                                            break;
                                        case "hepb":
                                            tabTestValueResultEdit += "<option value='Vaccinated'>Vaccinated</option>" +
                                                                  "<option value='Unvaccinated'>Unvaccinated</option>" +
                                                                  "<option value='Positive'>Positive</option>" +
                                                                  "<option value='Negative'>Negative</option>" +
                                                                  "<option value='NA'>NA</option>";
                                            break;
                                        case "flepm":
                                            tabTestValueResultEdit += "<option value='Equivocal'>Equivocal</option>" +
                                                                  "<option value='Positive'>Positive</option>" +
                                                                  "<option value='Negative'>Negative</option>" +
                                                                  "<option value='NA'>NA</option>";
                                            break;
                                        default:
                                            {
                                                if (Testcode.ToLower() == "ahpm" || Testcode.ToLower() == "ige" || Testcode.ToLower() == "CHIKV" || Testcode.ToLower() == "cmvp"
                                                    || Testcode.ToLower() == "dengm" || Testcode.ToLower() == "dnsag" || Testcode.ToLower() == "hav" || Testcode.ToLower() == "corab"
                                                    || Testcode.ToLower() == "heag" || Testcode.ToLower() == "hcscr" || Testcode.ToLower() == "hcvl" || Testcode.ToLower() == "hevg"
                                                    || Testcode.ToLower() == "hevm" || Testcode.ToLower() == "mhsv" || Testcode.ToLower() == "hsmr" || Testcode.ToLower() == "hsvg"
                                                    || Testcode.ToLower() == "vhsv" || Testcode.ToLower() == "vdrl")
                                                {
                                                    tabTestValueResultEdit += "<option value='Positive'>Positive</option>" +
                                                               "<option value='Negative'>Negative</option>" +
                                                               "<option value='NA'>NA</option>";
                                                }
                                                else
                                                {
                                                    DataSet dsgetsInterpretation = objReport1.getInterpretationResult(row["sTestCode"].ToString());
                                                    if (dsgetsInterpretation.Tables[0].Rows.Count > 0)
                                                    {
                                                        foreach (DataRow row1 in dsgetsInterpretation.Tables[0].Rows)
                                                        {
                                                            tabTestValueResultEdit += "<option value='" + row1["Interpretation"].ToString() + "'>" + row1["Interpretation"].ToString() + "</option>";
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            else if (row["sResultType"].ToString().ToLower() == "qualitative")
                            {
                                string Testcode = row["sTestCode"].ToString().Trim();
                                switch (Testcode)
                                {
                                    case "FDLS":
                                        tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                              "<option value='Negative'>Negative</option>" +
                                                             "<option value='Negative'>Positive</option>";
                                        break;
                                    case "HIV":

                                        tabTestValueResultEdit += "<option value='NA'>NA</option>" +
                                                              "<option value='Negative'>Negative</option>" +
                                                               "<option value='Negative'>Positive</option>";
                                        break;
                                    case "SPSM":

                                        tabTestValueResultEdit += "<option value='Adequate'>Adequate</option>" +
                                                              "<option value='NA'>NA</option>" +
                                                               "<option value='Normal'>Normal</option>" +
                                                               "<option value='Normocytic Normochomic'>Normocytic Normochomic</option>";
                                        break;
                                    case "MT":
                                        tabTestValueResultEdit += "<option value='Negative'>Negative</option>" +
                                                              "<option value='Positive'>Positive</option>" +
                                                               "<option value='NA'>NA</option>";
                                        break;
                                    case "RUB":
                                        tabTestValueResultEdit += "<option value='Negative'>Negative</option>" +
                                                              "<option value='Positive'>Positive</option>" +
                                                               "<option value='NA'>NA</option>";
                                        break;
                                    default:
                                        DataSet dsgetsInterpretation = objReport1.getsInterpretation("qualitative");
                                        if (dsgetsInterpretation.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow row1 in dsgetsInterpretation.Tables[0].Rows)
                                            {
                                                tabTestValueResultEdit += "<option value='" + row1["Interpretation"].ToString() + "'>" + row1["Interpretation"].ToString() + "</option>";
                                            }
                                        }
                                        break;
                                }
                            }
                            else if (row["sResultType"].ToString().ToLower() == "descriptive")
                            {
                                if (row["sTestCode"].ToString().Trim() == "UER")
                                {
                                    tabTestValueResultEdit += "<option value='Normal'>Normal</option>" +
                                                          "<option value='Abnormal'>Abnormal</option>" +
                                                          "<option value='NA'>NA</option>" +
                                                          "<option value='Present'>Present</option>" +
                                                          "<option value='Absent'>Absent</option>";
                                }
                                else if (row["sTestCode"].ToString().Trim() == "COVID19")
                                {
                                    tabTestValueResultEdit += "<option value='Positive'>Positive</option>" +
                                                          "<option value='Negative'>Negative</option>" +
                                                          "<option value='Inconclusive'>Inconclusive</option>" +
                                                          "<option value='Invalid'>Invalid</option>";
                                }
                                else
                                {
                                    tabTestValueResultEdit += "<option value='Negative'>Negative</option>" +
                                                          "<option value='Not Detected'>Not Detected</option>" +
                                                          "<option value='NA'>NA</option>" +
                                                          "<option value='No Growth'>No Growth</option>" +
                                                          "<option value='Growth'>Growth</option>" +
                                                          "<option value='Positive'>Positive</option>";
                                }

                            }
                            tabTestValueResult += "</select></td>" +
                                           "</tr>";
                        }
                        count++;
                    }

                }

                hiddenValueIdList.Value = hiddenValueIdList.Value.TrimStart(',').TrimEnd(',');
                tbodyTestValueResult.Text = tabTestValueResult;
                tbodyTestValueResultEdit.InnerHtml = tabTestValueResultEdit;

                if (spanApprovalStatus.InnerText.ToLower() != "approval pending")
                {
                    divApproveReject.Style["display"] = "none";
                }

                if (spanApprovalStatus.InnerText.ToLower() != "rejected")
                {
                    btnEditReport.Style["display"] = "none";
                }
            }
            else
            {
                divApproveReject.Style["display"] = "none";
                btnEditReport.Style["display"] = "none";
            }
        }
        
        catch (Exception ex)
        {
            lblMessage.Text = "Error occured While Loading Report";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
           // Response.Redirect("ViewReport.aspx", false);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string LabId = Request.Cookies["labId"].Value.ToString();
            int bookLabTestId = Convert.ToInt32(Request.QueryString["bookLabTestId"].ToString());
            int bookLabId = Convert.ToInt32(Request.QueryString["bookId"].ToString());
            string approvalStatus = hiddenReportStatus.Value;
            string comment = txtComment.Text;
            string approvedOn = "";
            string approvedBy = "";
			string labname="";
            if (approvalStatus.ToLower() == "approved")
            {
                approvedOn = DateTime.Now.ToString("dd/MM/yyyy");
                approvedBy = Request.Cookies["labUserId"].Value.ToString();
                objReport.UpdateReportStatus(bookLabId.ToString(), approvalStatus, LabId);
            }

            if (objReport.approveRejectReport(bookLabTestId.ToString(), approvalStatus, approvedOn, approvedBy, comment) == 1)
            {
				string paymentStatus = string.Empty;
                string Type = "Booking";
                dynamic _Result = new JObject();
                _Result.BookingId = bookLabId;
                string _payload = JsonConvert.SerializeObject(_Result);
                if (approvalStatus.ToLower() != "rejected")
                {
                    // CAlling For Notification Yogesh 
                    DataSet dsBookingDetails = objBookDetails.getBookTestDetailsUserMobId(bookLabId.ToString());

                    if (dsBookingDetails.Tables[0].Rows.Count != 0)
                    {
                        string deviceToken = dsBookingDetails.Tables[0].Rows[0]["sdeviceToken"].ToString();
                         labname = dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString();
                        string testtime = dsBookingDetails.Tables[0].Rows[0]["sTimeSlot"].ToString();
                        string testdate = dsBookingDetails.Tables[0].Rows[0]["stestdate"].ToString();
                        string msg = "Your report has been approved by " + dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString();
                        ObjFCM.SendNotification("Test Booking Status", msg, deviceToken, Type, bookLabId.ToString());
                        DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId.ToString());
                        int _result = objAppNotify.AppNotification(dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString(), "Booking", msg, Type, _payload, Request.Cookies["labUserId"].Value.ToString());

                        string script = "{ sendnotification('" + deviceToken + "', '" + msg + "'); };";
                        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "updatePanel1Script", script, true);
                    }
					 string labId = Request.Cookies["labId"].Value.ToString();
					 string mob = dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString();
                    string mobileNo = CryptoHelper.Decrypt(mob).ToString();
					//string labname = db.getData("select sLabName from labMaster where sLabId='" + Request.Cookies["labId"].Value.ToString() + "'");
                    newWhatsapp wa = new newWhatsapp();
                    ////wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
                    if (paymentStatus == "Not paid")
                    {
                        wa.sendWhatsappMsg("+91" + mobileNo, "Approve Report For Lab", spanPatientName.InnerHtml + ',' + hdntestName.Value + ',' + labname, LabId);
                        wa.sendWhatsappMsg("+91" + mobileNo, "Sent Report for Lab Unpaid", spanPatientName.InnerHtml + ',' + hdntestName.Value + ',' + labname, LabId);
                    }
                    else
                    {
                        wa.sendWhatsappMsg("+91" + mobileNo, "Approve Report For Lab", spanPatientName.InnerHtml + ',' + hdntestName.Value + ',' + labname, LabId);
                    }
                    lblMessage.Text = "Approved Report Updated successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                   // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.href='ViewReport.aspx';", true);
                }
                else
                {
                    // CAlling For Notification Yogesh 
                    DataSet dsBookingDetails = objBookDetails.getBookTestDetailsUserMobId(bookLabId.ToString());
                    string deviceToken = dsBookingDetails.Tables[0].Rows[0]["sdeviceToken"].ToString();
                    labname = dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString();
                    string testtime = dsBookingDetails.Tables[0].Rows[0]["sTimeSlot"].ToString();
                    string testdate = dsBookingDetails.Tables[0].Rows[0]["stestdate"].ToString();
                    string Message = "Your medical report has been rejected. ";
                    ObjFCM.SendNotification("Test Booking Status", Message, deviceToken, Type, bookLabId.ToString());
                    string script = "{ sendnotification('" + deviceToken + "', '" + Message + "'); };";
                    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "updatePanel1Script", script, true);

                    DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId.ToString());
                    int _result = objAppNotify.AppNotification(dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString(), "Booking", Message, Type, _payload, Request.Cookies["labUserId"].Value.ToString());
					 string mob = dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString();
  string mobileNo = CryptoHelper.Decrypt(mob).ToString();
					//string labname = db.getData("select sLabName from labMaster where sLabId='" + Request.Cookies["labId"].Value.ToString() + "'");
                    newWhatsapp wa = new newWhatsapp();
                    ////wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
                    wa.sendWhatsappMsg("+91" + mobileNo, "Reject Report For Lab", spanPatientName.InnerHtml + ',' + hdntestName.Value + ',' + labname, LabId);
                  
                    lblMessage.Text = "Report Updated successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                   // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.href='ViewReport.aspx';", true);
                }
            }
            else if (objReport.approveRejectReport(bookLabTestId.ToString(), approvalStatus, approvedOn, approvedBy, comment) == 0)
            {
                lblMessage.Text = "Error Occured While Rejected Report Updating";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
            }
        }
        catch
        {
            lblMessage.Text = "Error Occured";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
        }
    }
	 string labname="";
    protected void btnUpdateReport_Click(object sender, EventArgs e)
    {
        try
        {
            int bookLabTestId = Convert.ToInt32(Request.QueryString["bookLabTestId"].ToString());
            int bookLabId = Convert.ToInt32(Request.QueryString["bookId"].ToString());
            string[] valueIds = hiddenValueIdList.Value.Split(',');
            string notes = txtNotes.Value;

            string queryUpdateReport = "";
            foreach (string testReportValueId in valueIds)
            {
                queryUpdateReport += "update testReportValues set sValue='" + CryptoHelper.Encrypt(Request.Form["txtValue" + testReportValueId]) + "', sResult='" + CryptoHelper.Encrypt(Request.Form["selResult" + testReportValueId]) + "' where sTestReportValuesId='" + testReportValueId + "'";
            }

            if (objReport.updateTestReport(bookLabTestId.ToString(), queryUpdateReport, notes) == 1)
            {
                DataSet dsBookingDetails = objBookDetails.getBookTestDetailsUserMobId(bookLabId.ToString());
                string deviceToken = dsBookingDetails.Tables[0].Rows[0]["sdeviceToken"].ToString();
                labname = dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString();
                string testtime = dsBookingDetails.Tables[0].Rows[0]["sTimeSlot"].ToString();
                string testdate = dsBookingDetails.Tables[0].Rows[0]["stestdate"].ToString();
                string Message = "Your medical report has been updated.";
                string Type = "Booking";
                dynamic _Result = new JObject();
                _Result.BookingId = bookLabTestId;
                string _payload = JsonConvert.SerializeObject(_Result);
                ObjFCM.SendNotification("Test Booking Status", Message, deviceToken, Type, bookLabTestId.ToString());
                string script = "{ sendnotification('" + deviceToken + "', '" + Message + "'); };";
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "updatePanel1Script", script, true);

                DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId.ToString());

                if (dsTestReport.Tables[0].Rows.Count > 0)
                {
                    int _result = objAppNotify.AppNotification(dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString(), "Booking", Message, Type, _payload, Request.Cookies["labUserId"].Value.ToString());
                }
                lblMessage.Text = "Test Report Update Successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
               // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.href='ViewReport.aspx';", true);
            }
            else if (objReport.updateTestReport(bookLabTestId.ToString(), queryUpdateReport, notes) == 0)
            {
                lblMessage.Text = "Error Occured Wile Test Report Updating";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
               // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
            }
        }
        catch
        {
            lblMessage.Text = "Error Occured";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
        }
    }
    protected void btnsendMail_Click(object sender, EventArgs e)
    {
        try
        {
            LogError.Log("Clicked");
            LogError.Log("Eneterd SendPDF 2 ");
            int bookLabId = Convert.ToInt32(Request.QueryString["bookId"].ToString());
            int bookLabTestId = Convert.ToInt32(Request.QueryString["bookLabTestId"].ToString());
            string notes = txtNotes.Value;

            DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId.ToString());
            string useremailid = dsTestReport.Tables[0].Rows[0]["semailid"].ToString();
            string patinetname = dsTestReport.Tables[0].Rows[0]["sPatient"].ToString();
            string deviceToken = dsTestReport.Tables[0].Rows[0]["sDeviceToken"].ToString();
            string labname = dsTestReport.Tables[0].Rows[0]["sLabName"].ToString();
            string labaddress = dsTestReport.Tables[0].Rows[0]["sLabAddress"].ToString();
            string testdate = dsTestReport.Tables[0].Rows[0]["stestdate"].ToString();
            string labimage = dsTestReport.Tables[0].Rows[0]["slablogo"].ToString();
            string malevalue = dsTestReport.Tables[0].Rows[0]["sMale"].ToString();

            string Message = "You have received a medical report from " + labname + ".";
            string Type = "Booking";
            dynamic _Result = new JObject();
            _Result.BookingId = bookLabId;
            string _payload = JsonConvert.SerializeObject(_Result);
            ObjFCM.SendNotification("Test Booking Status", Message, deviceToken, Type, bookLabId.ToString());
            int _result = objAppNotify.AppNotification(dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString(), "Booking", Message, Type, _payload, Request.Cookies["labUserId"].Value.ToString());
		 string mob = db.getData("select sMobile from appUser where sAppUserId='" + dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString() + "'").ToString();
            string mobileNo = CryptoHelper.Decrypt(mob).ToString();
			//string labname = db.getData("select sLabName from labMaster where sLabId='" + Request.Cookies["labId"].Value.ToString() + "'");
            newWhatsapp wa = new newWhatsapp();
            //wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
            string LabId = Request.Cookies["labId"].Value.ToString();
            wa.sendWhatsappMsg("+91" + mobileNo, "Sent Report For Lab", patinetname + ',' + labname, LabId);
            lblMessage.Text = "Report sent successfully";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Report sent successfully');location.reload();", true);
            objReport.approveRejectReport(bookLabTestId.ToString());

        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            lblMessage.Text = "Error Occured While Sending Report";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
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

            string DateOfBirth = dsTestReport.Tables[0].Rows[0]["sbirthdate"].ToString();

            //string[] datesplit = DateOfBirth.Split('/');
            //string mm = datesplit[0].ToString();
            //string dd = datesplit[1].ToString();
            //string yyyy = datesplit[2].ToString();

            ////string dob = dsBookingDetails.Tables[0].Rows[0]["sBirthDate"].ToString();
            ////  string dob = dd+"/"+mm+"/"+yyyy;

            //string dob = dd + "/" + mm + "/" + yyyy;

            string dob = "";
            if (DateOfBirth.Contains("/"))
            {
                string[] fullDate = DateOfBirth.Split('/');

                string date = fullDate[0];
                string Month = fullDate[1];
                string Year = fullDate[2];
                dob = date + "/" + Month + "/" + Year;
            }
            else if (DateOfBirth.Contains("-"))
            {
                string[] fullDate = DateOfBirth.Split('-');
                string date = fullDate[0];
                string Month = fullDate[1];
                string Year = fullDate[2];
                dob = date + "/" + Month + "/" + Year;
            }
            else
            {
                dob = DateOfBirth;
            }
            //  DateTime dtDob = Convert.ToDateTime(dob);
            DateTime dtDob = DateTime.Parse(dob);
            if (CalculateYourAge(dtDob)["Years"] != "0")
            {
                patientAge = CalculateYourAge(dtDob)["Years"] + " year(s)";
            }
            else if (CalculateYourAge(dtDob)["Months"] != "0")
            {
                patientAge = CalculateYourAge(dtDob)["Months"] + " month(s)";
            }
            else if (CalculateYourAge(dtDob)["Days"] != "0")
            {
                patientAge = CalculateYourAge(dtDob)["Days"] + " day(s)";
            }

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

                    if (labimage != "")
                    {
                        sb.Append("<td style='text-align: center;width: 150px;padding: 15px'><img src='http://visionarylifescience.com/images/" + labimage + "'></td>");
                    }

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
                    sb.Append("<td style='padding:5px 10px'>Age: " + patientAge + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='padding:5px 10px'>Gender: " + dsTestReport.Tables[0].Rows[0]["sGender"].ToString() + "</td>");
                    //	sb.Append("<td style='padding:5px 10px'>Time: 12:00</td>");
                    sb.Append("<td style='padding:5px 10px'>Date: " + dsTestReport.Tables[0].Rows[0]["sBookRequestedAt"].ToString() + "</td>");
                    sb.Append("<td style='padding:5px 10px'>Referred by: " + dsTestReport.Tables[0].Rows[0]["sDoctor"].ToString() + "</td>");
                    sb.Append("<td style='padding:5px 10px'</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='padding:5px 10px'></td>");
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
                    sb.Append("<th style='padding:5px 10px;text-align: left'>Value</th>");
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
                        sb.Append("<td style='padding:5px 10px'>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + " " + row["sUnits"].ToString() + "</td>");
                        sb.Append("<td style='padding:5px 10px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</td>");
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

    [WebMethod]
    public static string getReferences(string id, string checkFrom, string Age, string Gender, string Val)
    {
        DataAccessLayer DAL = new DataAccessLayer();
        DataSet dt = new DataSet();
        string response = "";
        string age = Age.Split(' ')[0];
        string AgeUnit = Age.Split(' ')[1];

        if (checkFrom == "Analyte")
        {
            SqlParameter[] param = new SqlParameter[]
                         {
                            new SqlParameter("@TASMId",id),
                            new SqlParameter("@Gender",Gender),
                            new SqlParameter("@Age",age),
                            new SqlParameter("@AgeUnit",AgeUnit),
                            new SqlParameter("@val",(Val !="")?Val:"0")
                        };
            dt = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestReferencerangeInterpretation2", param);
            if (dt.Tables[0].Rows.Count > 0)
            {
                response = dt.Tables[0].Rows[0]["Interpretation"].ToString();
            }
            else if (dt.Tables[1].Rows.Count > 0)
            {
                response = dt.Tables[1].Rows[0]["Interpretation"].ToString();
            }
            else
            {
                response = "";
            }
        }
        else if (checkFrom == "SubAnalyte")
        {
            SqlParameter[] param1 = new SqlParameter[]
                         {
                            new SqlParameter("@TASMId",id),
                            new SqlParameter("@Gender",Gender),
                            new SqlParameter("@Age",age),
                            new SqlParameter("@AgeUnit",AgeUnit),
                            new SqlParameter("@val",(Val !="")?Val:"0")
                        };
            dt = DAL.ExecuteStoredProcedureDataSet("Sp_GetSubTestReferencerangeInterpretation2", param1);
            if (dt.Tables[0].Rows.Count > 0)
            {
                response = dt.Tables[0].Rows[0]["Interpretation"].ToString();
            }
            else if (dt.Tables[1].Rows.Count > 0)
            {
                response = dt.Tables[1].Rows[0]["Interpretation"].ToString();
            }
            else
            {
                response = "";
            }
        }
        return JsonConvert.SerializeObject(response);
    }
}
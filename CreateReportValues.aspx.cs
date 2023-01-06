using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Globalization;
using CrossPlatformAESEncryption.Helper;
using System.Net;
using System.Data.SqlClient;
using DataAccessHandler;
using Newtonsoft.Json;

public partial class CreateReportValues : System.Web.UI.Page
{
    ClsCreateReport objReport = new ClsCreateReport();
    ClsBookDetails objBookDetails = new ClsBookDetails();
    CLSNotification objNotification = new CLSNotification();
    ClsSMSAPI API = new ClsSMSAPI();
    DataAccessLayer DAL = new DataAccessLayer();
    string labName = "";
    string labAddress = "";
    string labContact = "";
    string patientId = "";
    string patientName = "";
    string patientGender = "";
    string patientAge = "";
    string doctorId = "";
    string doctorName = "";
    string testTakenOn = "";
    string testId = "";
    string testCode = "";
    string testName = "";
    string bookLabId = "";
    string bookLabTestId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
					  string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                    ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
                    loadBookingDetails();
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

    protected void loadBookingDetails()
    {
        try
        {
            bookLabId = Request.QueryString["bookId"].ToString();
            bookLabTestId = Request.QueryString["bookLabTestId"].ToString();
            testId = Request.QueryString["testId"].ToString();
            testCode = "";
            testName = "";
            string labId = Request.Cookies["labId"].Value.ToString();
            DataSet dsBookingDetails = objReport.getBookingDetails(labId, bookLabId);

            if (dsBookingDetails != null)
            {
                if (dsBookingDetails.Tables[0].Rows.Count > 0)
                {
                    labName = dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString(); ;
                    labAddress = dsBookingDetails.Tables[0].Rows[0]["sLabAddress"].ToString();
                    labContact = (dsBookingDetails.Tables[0].Rows[0]["sLabContact"].ToString() != "") ? CryptoHelper.Decrypt(dsBookingDetails.Tables[0].Rows[0]["sLabContact"].ToString()) : "";
                    patientId = dsBookingDetails.Tables[0].Rows[0]["sPatientId"].ToString();
                    patientName = dsBookingDetails.Tables[0].Rows[0]["sPatient"].ToString();
                    patientGender = dsBookingDetails.Tables[0].Rows[0]["sGender"].ToString();
                    string DateOfBirth = dsBookingDetails.Tables[0].Rows[0]["sBirthDate"].ToString();
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
                    doctorId = dsBookingDetails.Tables[0].Rows[0]["sDoctorId"].ToString();
                    doctorName = dsBookingDetails.Tables[0].Rows[0]["sDoctor"].ToString();
                    testTakenOn = dsBookingDetails.Tables[0].Rows[0]["sBookRequestedAt"].ToString();
                }
                else
                {
                    btnSubmit.Visible = false;
                }
            }

            string[] spiltAge = patientAge.Split(' ').ToArray();
            int PatientAge = Convert.ToInt32(spiltAge[0]);
            string patientageunit = spiltAge[1];
            spanPatientName.InnerHtml = patientName;
            spanPatientID.InnerHtml = patientId;
            spanGender.InnerHtml = patientGender;
            spanAge.InnerHtml = patientAge;
            spanDoctorName.InnerHtml = doctorName;
            spanTestTakenOn.InnerHtml = testTakenOn;
            spanBookingId.InnerHtml = bookLabId;
            spanLabName.InnerHtml = labName;
            spanLabAddress.InnerHtml = labAddress;
            spanLabContact.InnerHtml = labContact;

           
            DataSet dsTestSubAnalyte = objReport.getTestRefernceSubAnalyte(patientGender, testId);
            int FromAge;
            int ToAge;
            string AgeUnit = "";
            string tabTestValueResult = "";
            // string ageUnit = "";
            
            #region
            if (dsTestSubAnalyte != null)
            {
                if (dsTestSubAnalyte.Tables[0].Rows.Count > 0)
                {
                    testCode = dsTestSubAnalyte.Tables[0].Rows[0]["sTestCode"].ToString();
                    testName = dsTestSubAnalyte.Tables[0].Rows[0]["sTestName"].ToString();
                      hdntestName.Value = testName;
					spanTestCodeName.InnerHtml = testCode + ", " + testName;
                    htestCode.Value = testCode;
                    hiddenSubAnalyteCount.Value = dsTestSubAnalyte.Tables[0].Rows.Count.ToString();
                    List<string> lstSASM = new List<string>();
                    int count = 1;
                    int flag = 0;
                    foreach (DataRow row in dsTestSubAnalyte.Tables[0].Rows)
                    {
                        #region Male
                        string sasm = row["TSASMId"].ToString();
                        if (patientGender.ToLower() == "male")
                        {
                            FromAge = Convert.ToInt32(row["MaleFromAge"].ToString());
                            ToAge = Convert.ToInt32(row["MaleToAge"].ToString());
                            AgeUnit = row["MaleAgeUnit"].ToString();
                            string Age = FromAge + "-" + ToAge + " " + AgeUnit;
                            if ((FromAge == 0 || FromAge == 1) && ToAge == 100 && AgeUnit == "Year" || AgeUnit == "year")
                            {

                                if (lstSASM.Contains(sasm) == false)
                                {
                                    lstSASM.Add(sasm);
                                    //  string disabled = (row["sResultType"].ToString().ToLower() == "quantitative" && row["ReferenceType"].ToString().ToLower() == "reference as per gender and age") ? "disabled='disabled'" : "";
                                    tabTestValueResult += "<tr>" +
                                                      "<td scope='col' style='display:none'>" + row["sTestId"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'>" + row["sTestCode"].ToString() + "</td>" +
                                                      "<td scope='col'><input type='hidden' id='hiddenAnalyteSA" + count + "' name='hiddenAnalyteSA" + count + "' value='" + row["sAnalyteName"].ToString() + "'/>" + row["sAnalyteName"].ToString() + "</td>" +
                                                      "<td scope='col'><input type='hidden' id='hiddenSubAnalyteSA" + count + "' name='hiddenSubAnalyteSA" + count + "' value='" + row["sSubAnalyteName"].ToString() + "'/>" + row["sSubAnalyteName"].ToString() + "</td>" +
                                                      "<td scope='col'><input type='hidden' id='hiddenSampleTypeSA" + count + "' name='hiddenSampleTypeSA" + count + "' value='" + row["sSampleType"].ToString() + "'/>" + row["sSampleType"].ToString() + "</td>" +
                                                      "<td scope='col'><input type='hidden' id='hiddenMethodNameSA" + count + "' name='hiddenMethodNameSA" + count + "' value='" + row["sMethodName"].ToString() + "'/>" + row["sMethodName"].ToString() + "</td>" +
                                                      "<td scope='col'><input type='hidden' id='hiddenResultTypeSA" + count + "' name='hiddenResultTypeSA" + count + "' value='" + row["sResultType"].ToString() + "'/>" + row["sResultType"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenReferenceTypeSA" + count + "' name='hiddenReferenceTypeSA" + count + "' value='" + row["ReferenceType"].ToString() + "'/>" + row["ReferenceType"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenAgeSA" + count + "' name='hiddenAgeSA" + count + "' value='" + Age + "'/>" + Age + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenMaleSA" + count + "' name='hiddenMaleSA" + count + "' value='" + row["MaleMinValue"].ToString() + "'/>" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenMaleSA1" + count + "' name='hiddenMaleSA1" + count + "' value='" + row["MaleMaxValue"].ToString() + "'/>" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenGradeSA" + count + "' name='hiddenGradeSA" + count + "' value='" + row["Grade"].ToString() + "'/>" + row["Grade"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenUnitsSA" + count + "' name='hiddenUnitsSA" + count + "' value='" + row["Unit"].ToString() + "'/>" + row["Unit"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenInterpretationSA" + count + "' name='hiddenInterpretationSA" + count + "' value='" + row["Interpretation"].ToString() + "'/>" + row["Interpretation"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenUpperLimitSA" + count + "' name='hiddenUpperLimitSA" + count + "' value='" + row["UpperLimit"].ToString() + "'/>" + row["UpperLimit"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenLowerLimitSA" + count + "' name='hiddenLowerLimitSA" + count + "' value='" + row["LowerLimit"].ToString() + "'/>" + row["LowerLimit"].ToString() + "</td>" +
                                                      "<td scope='col'><input type='text' class='form-control' class='form-control' name='valueSA" + count + "' id='SAn|" + row["TSASMId"].ToString() + "|" + row["sResultType"].ToString() + "|" + row["ReferenceType"].ToString() + "|" + patientGender + "|" + patientAge + "'  clientIdMode='static' onkeyup='javascript:loadResultBasedOnValue(this)'/></td>";


                                    //  tabTestValueResult += "<td scope='col' style='width: 117px;'>" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + " </td>";
                                    tabTestValueResult += "<td scope='col' style='width: 117px;'><input type='hidden' id='hiddenMaleReferenceSA" + count + "' name='hiddenMaleReferenceSA" + count + "' value='" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + "'/>" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + " </td>";
                                    tabTestValueResult += "<td scope='col'><select class='form-control select2 select2-hidden-accessible' name='resultSA" + count + "' id='resultSA" + count + "' clientIdMode='static'>";
                                    tabTestValueResult += "<option value='select'>select</option>";

                                    ////Get Test Sub Analyte Interpretation
                                    //DataTable dtInterpretation = objReport.GetTestSubAnylteInterpretaion(sasm);
                                    //if (dtInterpretation.Rows.Count > 0)
                                    //{
                                    //    foreach (DataRow rowInterprtation in dtInterpretation.Rows)
                                    //    {
                                    //        tabTestValueResult += "<option value='" + rowInterprtation["Interpretation"].ToString() + "'>" + rowInterprtation["Interpretation"].ToString() + "</option>";
                                    //    }
                                    //}

                                     string rtype = row["sResultType"].ToString();
                                     if (rtype == "quantitative\t" || rtype == "Quantitative")
                                    {
                                        string Testcode = row["sTestCode"].ToString().Trim();
                                        switch (Testcode)
                                        {
                                            case "afp":
                                                if (row["sProfileName"].ToString().ToLower().Trim() == "cancer profile")
                                                {
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                          "<option value='Possiblity of tumor'>Possiblity of tumor</option>";
                                                }
                                                if (row["sProfileName"].ToString().Trim() == "PREGNANCY PROFILE")
                                                {
                                                    tabTestValueResult += "<option value='NA'>NA</option>";
                                                }
                                                break;
                                            case "fsh":
                                                tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                        "<option value='Normal'>Normal</option>" +
                                                                        "<option value='High'>High</option>" +
                                                                        "<option value='Low'>Low</option>";

                                                break;
                                            case ".CBC":
                                                tabTestValueResult += "<option value='Normal'>Normal</option>" +
                                                                        "<option value='High'>High</option>" +
                                                                        "<option value='Low'>Low</option>";

                                                break;
                                            case "lh":
                                                tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                        "<option value='Normal'>Normal</option>" +
                                                                        "<option value='High'>High</option>" +
                                                                        "<option value='Low'>Low</option>";
                                                break;
                                            case "pgsn":
                                                tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                        "<option value='Normal'>Normal</option>" +
                                                                        "<option value='High'>High</option>" +
                                                                        "<option value='Low'>Low</option>";
                                                break;
                                            case "thcg":
                                                tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                           "<option value='Indeterminate'>Indeterminate</option>" +
                                                                           "<option value='Positive'>Positive</option>";
                                                break;
                                            case "spsa":
                                                tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                           "<option value='NA'>NA</option>" +
                                                                           "<option value='Positive'>Positive</option>";
                                                break;
                                            case "semb":
                                                tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                      "<option value='Normal'>Normal</option>";
                                                break;
                                            case "ca19":
                                                tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                      "<option value='Normal'>Normal</option>";
                                                break;
                                            case "psaft":
                                                tabTestValueResult += "<option value='49% to 65% risk of prostate cancer depending on age'>49% to 65% risk of prostate cancer depending on age</option>" +
                                                                           "<option value='27% to 41% risk of prostate cancer depending on age'>27% to 41% risk of prostate cancer depending on age</option>" +
                                                                            "<option value='18% to 30% risk of prostate cancer depending on age'>18% to 30% risk of prostate cancer depending on age</option>" +
                                                                            "<option value='9% to 16% risk of prostate cancer depending on age'>9% to 16% risk of prostate cancer depending on age</option>";
                                                break;
                                            case "ca25":
                                                tabTestValueResult += "<option value=''></option>";
                                                break;
                                            case "ca153":
                                                tabTestValueResult += "<option value=''></option>";
                                                break;
                                            case "hba1c":
                                                tabTestValueResult += "<option value='Normal'>Normal</option>" +
                                                                       "<option value='Pre-diabetic'>Pre-diabetic</option>" +
                                                                       "<option value='Diabetic'>Diabetic</option>";
                                                break;
                                            case "phsm":
                                                tabTestValueResult += "<option value='Vaccinated'>Vaccinated</option>" +
                                                                      "<option value='Unvaccinated'>Unvaccinated</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                      "<option value='NA'>NA</option>";
                                                break;
                                            case "qft3":
                                                tabTestValueResult += "<option value='Strong Positive'>Strong Positive</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                      "<option value='NA'>NA</option>";
                                                break;
                                            case "hepb":
                                                tabTestValueResult += "<option value='Vaccinated'>Vaccinated</option>" +
                                                                      "<option value='Unvaccinated'>Unvaccinated</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                      "<option value='NA'>NA</option>";
                                                break;
                                            case "flepm":
                                                tabTestValueResult += "<option value='Equivocal'>Equivocal</option>" +
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
                                                        tabTestValueResult += "<option value='Positive'>Positive</option>" +
                                                                   "<option value='Negative'>Negative</option>" +
                                                                   "<option value='NA'>NA</option>";
                                                    }
                                                    else
                                                    {
                                                        DataSet dsgetsInterpretation = objReport.getInterpretationResult(row["sTestCode"].ToString());
                                                        if (dsgetsInterpretation.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow row1 in dsgetsInterpretation.Tables[0].Rows)
                                                            {
                                                                tabTestValueResult += "<option value='" + row1["Interpretation"].ToString() + "'>" + row1["Interpretation"].ToString() + "</option>";
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                     else if (row["sResultType"].ToString().ToLower() == "qualitative" || row["sResultType"].ToString() == "Qualitative")
                                    {
                                        string Testcode = row["sTestCode"].ToString().Trim();
                                        switch (Testcode)
                                        {
                                            case "BG":
                                                tabTestValueResult += "<option value='NA'>A +Ve</option>" +
                                                                      "<option value='Negative'>B +Ve</option>" +
                                                                     "<option value='Negative'>O +Ve</option>"+
                                                "<option value='Negative'>AB +Ve</option>"+
                                                "<option value='Negative'>AB -Ve</option>"+
                                                "<option value='Negative'>B -Ve</option>"+
                                                "<option value='Negative'>A -Ve</option>"+
                                                 "<option value='Negative'>O -Ve</option>" +
                                                        "<option value='Negative'>Positive</option>" +
                                                        "<option value='Negative'>Negative</option>";
                                                break;
                                            case "FDLS":
                                                tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                     "<option value='Negative'>Positive</option>";
                                                break;
                                            case "HIV":

                                                tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                       "<option value='Negative'>Positive</option>";
                                                break;
                                            case "SPSM":

                                                tabTestValueResult += "<option value='Adequate'>Adequate</option>" +
                                                                      "<option value='NA'>NA</option>" +
                                                                       "<option value='Normal'>Normal</option>" +
                                                                       "<option value='Normocytic Normochomic'>Normocytic Normochomic</option>";
                                                break;
                                            case "MT":
                                                tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                       "<option value='NA'>NA</option>";
                                                break;
                                            case "RUB":
                                                tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                       "<option value='NA'>NA</option>";
                                                break;
                                            default:
                                                DataSet dsgetsInterpretation = objReport.getsInterpretation("qualitative");
                                                if (dsgetsInterpretation.Tables[0].Rows.Count > 0)
                                                {
                                                    foreach (DataRow row1 in dsgetsInterpretation.Tables[0].Rows)
                                                    {
                                                        tabTestValueResult += "<option value='" + row1["Interpretation"].ToString() + "'>" + row1["Interpretation"].ToString() + "</option>";
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                     else if (row["sResultType"].ToString().ToLower() == "descriptive" || row["sResultType"].ToString() == "Descriptive")
                                    {
                                        if (row["sTestCode"].ToString().Trim() == "UER")
                                        {
                                            tabTestValueResult += "<option value='Normal'>Normal</option>" +
                                                                  "<option value='Abnormal'>Abnormal</option>" +
                                                                  "<option value='NA'>NA</option>" +
                                                                  "<option value='Present'>Present</option>" +
                                                                  "<option value='Absent'>Absent</option>";
                                        }
                                        else if (row["sTestCode"].ToString().Trim() == "COVID19")
                                        {
                                            tabTestValueResult += "<option value='Positive'>Positive</option>" +
                                                                  "<option value='Negative'>Negative</option>" +
                                                                  "<option value='Inconclusive'>Inconclusive</option>" +
                                                                  "<option value='Invalid'>Invalid</option>";
                                        }
                                        else
                                        {
                                            tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                  "<option value='Not Detected'>Not Detected</option>" +
                                                                  "<option value='NA'>NA</option>" +
                                                                  "<option value='No Growth'>No Growth</option>" +
                                                                  "<option value='Growth'>Growth</option>" +
                                                                  "<option value='Positive'>Positive</option>";
                                        }
                                    }
                                    tabTestValueResult += "</select></td>" +
                                                   "</tr>";
                                    count++;
                                }
                                flag = 1;
                                btnSubmit.Visible = true;
                            }
                           
                            else
                            {
                                if (FromAge <= PatientAge && ToAge >= PatientAge && AgeUnit == patientageunit)
                                {
                                    // string sasm = row["TSASMId"].ToString();
                                    if (lstSASM.Contains(sasm) == false)
                                    {
                                        lstSASM.Add(sasm);
                                        //  string disabled = (row["sResultType"].ToString().ToLower() == "quantitative" && row["ReferenceType"].ToString().ToLower() == "reference as per gender and age") ? "disabled='disabled'" : "";
                                        tabTestValueResult += "<tr>" +
                                                          "<td scope='col' style='display:none'>" + row["sTestId"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'>" + row["sTestCode"].ToString() + "</td>" +
                                                          "<td scope='col'><input type='hidden' id='hiddenAnalyteSA" + count + "' name='hiddenAnalyteSA" + count + "' value='" + row["sAnalyteName"].ToString() + "'/>" + row["sAnalyteName"].ToString() + "</td>" +
                                                          "<td scope='col'><input type='hidden' id='hiddenSubAnalyteSA" + count + "' name='hiddenSubAnalyteSA" + count + "' value='" + row["sSubAnalyteName"].ToString() + "'/>" + row["sSubAnalyteName"].ToString() + "</td>" +
                                                          "<td scope='col'><input type='hidden' id='hiddenSampleTypeSA" + count + "' name='hiddenSampleTypeSA" + count + "' value='" + row["sSampleType"].ToString() + "'/>" + row["sSampleType"].ToString() + "</td>" +
                                                          "<td scope='col'><input type='hidden' id='hiddenMethodNameSA" + count + "' name='hiddenMethodNameSA" + count + "' value='" + row["sMethodName"].ToString() + "'/>" + row["sMethodName"].ToString() + "</td>" +
                                                          "<td scope='col'><input type='hidden' id='hiddenResultTypeSA" + count + "' name='hiddenResultTypeSA" + count + "' value='" + row["sResultType"].ToString() + "'/>" + row["sResultType"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenReferenceTypeSA" + count + "' name='hiddenReferenceTypeSA" + count + "' value='" + row["ReferenceType"].ToString() + "'/>" + row["ReferenceType"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenAgeSA" + count + "' name='hiddenAgeSA" + count + "' value='" + Age + "'/>" + Age + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenMaleSA" + count + "' name='hiddenMaleSA" + count + "' value='" + row["MaleMinValue"].ToString() + "'/>" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenMaleSA1" + count + "' name='hiddenMaleSA1" + count + "' value='" + row["MaleMaxValue"].ToString() + "'/>" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenGradeSA" + count + "' name='hiddenGradeSA" + count + "' value='" + row["Grade"].ToString() + "'/>" + row["Grade"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenUnitsSA" + count + "' name='hiddenUnitsSA" + count + "' value='" + row["Unit"].ToString() + "'/>" + row["Unit"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenInterpretationSA" + count + "' name='hiddenInterpretationSA" + count + "' value='" + row["Interpretation"].ToString() + "'/>" + row["Interpretation"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenUpperLimitSA" + count + "' name='hiddenUpperLimitSA" + count + "' value='" + row["UpperLimit"].ToString() + "'/>" + row["UpperLimit"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenLowerLimitSA" + count + "' name='hiddenLowerLimitSA" + count + "' value='" + row["LowerLimit"].ToString() + "'/>" + row["LowerLimit"].ToString() + "</td>" +
                                                          "<td scope='col'><input type='text' class='form-control' class='form-control' name='valueSA" + count + "' id='SAn|" + row["TSASMId"].ToString() + "|" + row["sResultType"].ToString() + "|" + row["ReferenceType"].ToString() + "|" + patientGender + "|" + patientAge + "'  clientIdMode='static' onkeyup='javascript:loadResultBasedOnValue(this)'/></td>";


                                        //  tabTestValueResult += "<td scope='col' style='width: 117px;'>" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + " </td>";
                                        tabTestValueResult += "<td scope='col' style='width: 117px;'><input type='hidden' id='hiddenMaleReferenceSA" + count + "' name='hiddenMaleReferenceSA" + count + "' value='" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + "'/>" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + " </td>";
                                        tabTestValueResult += "<td scope='col'><select class='form-control select2 select2-hidden-accessible' name='resultSA" + count + "' id='resultSA" + count + "' clientIdMode='static'>";
                                        tabTestValueResult += "<option value='select'>select</option>";

                                        ////Get Test Sub Analyte Interpretation
                                        //  DataTable dtInterpretation = objReport.GetTestSubAnylteInterpretaion(sasm);
                                        //  if (dtInterpretation.Rows.Count > 0)
                                        //  {
                                        //      foreach (DataRow rowInterprtation in dtInterpretation.Rows)
                                        //      {
                                        //          tabTestValueResult += "<option value='" + rowInterprtation["Interpretation"].ToString() + "'>" + rowInterprtation["Interpretation"].ToString() + "</option>";
                                        //      }
                                        //  }

                                        string rtype = row["sResultType"].ToString();
                                        if (rtype == "quantitative\t" || rtype == "Quantitative")
                                        {
                                            string Testcode = row["sTestCode"].ToString().Trim();
                                            switch (Testcode)
                                            {
                                                case "afp":
                                                    if (row["sProfileName"].ToString().ToLower().Trim() == "cancer profile")
                                                    {
                                                        tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                              "<option value='Negative'>Negative</option>" +
                                                                              "<option value='Possiblity of tumor'>Possiblity of tumor</option>";
                                                    }
                                                    if (row["sProfileName"].ToString().Trim() == "PREGNANCY PROFILE")
                                                    {
                                                        tabTestValueResult += "<option value='NA'>NA</option>";
                                                    }
                                                    break;
                                                case ".CBC":
                                                    tabTestValueResult += "<option value='Normal'>Normal</option>" +
                                                                            "<option value='High'>High</option>" +
                                                                            "<option value='Low'>Low</option>";

                                                    break;
                                                case "fsh":
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                            "<option value='Normal'>Normal</option>" +
                                                                            "<option value='High'>High</option>" +
                                                                            "<option value='Low'>Low</option>";

                                                    break;
                                                case "lh":
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                            "<option value='Normal'>Normal</option>" +
                                                                            "<option value='High'>High</option>" +
                                                                            "<option value='Low'>Low</option>";
                                                    break;
                                                case "pgsn":
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                            "<option value='Normal'>Normal</option>" +
                                                                            "<option value='High'>High</option>" +
                                                                            "<option value='Low'>Low</option>";
                                                    break;
                                                case "thcg":
                                                    tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                               "<option value='Indeterminate'>Indeterminate</option>" +
                                                                               "<option value='Positive'>Positive</option>";
                                                    break;
                                                case "spsa":
                                                    tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                               "<option value='NA'>NA</option>" +
                                                                               "<option value='Positive'>Positive</option>";
                                                    break;
                                                case "semb":
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                          "<option value='Normal'>Normal</option>";
                                                    break;
                                                case "ca19":
                                                    tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                          "<option value='Normal'>Normal</option>";
                                                    break;
                                                case "psaft":
                                                    tabTestValueResult += "<option value='49% to 65% risk of prostate cancer depending on age'>49% to 65% risk of prostate cancer depending on age</option>" +
                                                                               "<option value='27% to 41% risk of prostate cancer depending on age'>27% to 41% risk of prostate cancer depending on age</option>" +
                                                                                "<option value='18% to 30% risk of prostate cancer depending on age'>18% to 30% risk of prostate cancer depending on age</option>" +
                                                                                "<option value='9% to 16% risk of prostate cancer depending on age'>9% to 16% risk of prostate cancer depending on age</option>";
                                                    break;
                                                case "ca25":
                                                    tabTestValueResult += "<option value=''></option>";
                                                    break;
                                                case "ca153":
                                                    tabTestValueResult += "<option value=''></option>";
                                                    break;
                                                case "hba1c":
                                                    tabTestValueResult += "<option value='Normal'>Normal</option>" +
                                                                           "<option value='Pre-diabetic'>Pre-diabetic</option>" +
                                                                           "<option value='Diabetic'>Diabetic</option>";
                                                    break;
                                                case "phsm":
                                                    tabTestValueResult += "<option value='Vaccinated'>Vaccinated</option>" +
                                                                          "<option value='Unvaccinated'>Unvaccinated</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                          "<option value='NA'>NA</option>";
                                                    break;
                                                case "qft3":
                                                    tabTestValueResult += "<option value='Strong Positive'>Strong Positive</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                          "<option value='NA'>NA</option>";
                                                    break;
                                                case "hepb":
                                                    tabTestValueResult += "<option value='Vaccinated'>Vaccinated</option>" +
                                                                          "<option value='Unvaccinated'>Unvaccinated</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                          "<option value='NA'>NA</option>";
                                                    break;
                                                case "flepm":
                                                    tabTestValueResult += "<option value='Equivocal'>Equivocal</option>" +
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
                                                            tabTestValueResult += "<option value='Positive'>Positive</option>" +
                                                                       "<option value='Negative'>Negative</option>" +
                                                                       "<option value='NA'>NA</option>";
                                                        }
                                                        else
                                                        {
                                                            DataSet dsgetsInterpretation = objReport.getInterpretationResult(row["sTestCode"].ToString());
                                                            if (dsgetsInterpretation.Tables[0].Rows.Count > 0)
                                                            {
                                                                foreach (DataRow row1 in dsgetsInterpretation.Tables[0].Rows)
                                                                {
                                                                    tabTestValueResult += "<option value='" + row1["Interpretation"].ToString() + "'>" + row1["Interpretation"].ToString() + "</option>";
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        else if (row["sResultType"].ToString().ToLower() == "qualitative" || row["sResultType"].ToString() == "Qualitative")
                                        {
                                            string Testcode = row["sTestCode"].ToString().Trim();
                                            switch (Testcode)
                                            {
                                                case "BG":
                                                    tabTestValueResult += "<option value='NA'>A +Ve</option>" +
                                                                          "<option value='Negative'>B +Ve</option>" +
                                                                         "<option value='Negative'>O +Ve</option>" +
                                                    "<option value='Negative'>AB +Ve</option>" +
                                                    "<option value='Negative'>AB -Ve</option>" +
                                                    "<option value='Negative'>B -Ve</option>" +
                                                    "<option value='Negative'>A -Ve</option>" +
                                                     "<option value='Negative'>O -Ve</option>" +
                                                        "<option value='Negative'>Positive</option>" +
                                                        "<option value='Negative'>Negative</option>";
                                                    break;
                                                case "FDLS":
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                         "<option value='Negative'>Positive</option>";
                                                    break;
                                                case "HIV":

                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                           "<option value='Negative'>Positive</option>";
                                                    break;
                                                case "SPSM":

                                                    tabTestValueResult += "<option value='Adequate'>Adequate</option>" +
                                                                          "<option value='NA'>NA</option>" +
                                                                           "<option value='Normal'>Normal</option>" +
                                                                           "<option value='Normocytic Normochomic'>Normocytic Normochomic</option>";
                                                    break;
                                                case "MT":
                                                    tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                           "<option value='NA'>NA</option>";
                                                    break;
                                                case "RUB":
                                                    tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                           "<option value='NA'>NA</option>";
                                                    break;
                                                default:
                                                    DataSet dsgetsInterpretation = objReport.getsInterpretation("qualitative");
                                                    if (dsgetsInterpretation.Tables[0].Rows.Count > 0)
                                                    {
                                                        foreach (DataRow row1 in dsgetsInterpretation.Tables[0].Rows)
                                                        {
                                                            tabTestValueResult += "<option value='" + row1["Interpretation"].ToString() + "'>" + row1["Interpretation"].ToString() + "</option>";
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        else if (row["sResultType"].ToString().ToLower() == "descriptive" || row["sResultType"].ToString() == "Descriptive")
                                        {
                                            if (row["sTestCode"].ToString().Trim() == "UER")
                                            {
                                                tabTestValueResult += "<option value='Normal'>Normal</option>" +
                                                                      "<option value='Abnormal'>Abnormal</option>" +
                                                                      "<option value='NA'>NA</option>" +
                                                                      "<option value='Present'>Present</option>" +
                                                                      "<option value='Absent'>Absent</option>";
                                            }
                                            else if (row["sTestCode"].ToString().Trim() == "COVID19")
                                            {
                                                tabTestValueResult += "<option value='Positive'>Positive</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                      "<option value='Inconclusive'>Inconclusive</option>" +
                                                                      "<option value='Invalid'>Invalid</option>";
                                            }
                                            else
                                            {
                                                tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                      "<option value='Not Detected'>Not Detected</option>" +
                                                                      "<option value='NA'>NA</option>" +
                                                                      "<option value='No Growth'>No Growth</option>" +
                                                                      "<option value='Growth'>Growth</option>" +
                                                                      "<option value='Positive'>Positive</option>";
                                            }
                                        }
                                        tabTestValueResult += "</select></td>" +
                                                       "</tr>";
                                        count++;
                                    }
                                    flag = 1;
                                    btnSubmit.Visible = true;

                                }
                                else
                                {
                                    if (flag == 0)
                                    {
                                        btnSubmit.Visible = false;
                                    }
                                    else
                                    {
                                        btnSubmit.Visible = true;
                                    }
                                }
                            }
                        }
                        #endregion
                        #region Female
                        else
                        {
                            FromAge = Convert.ToInt32(row["FemaleFromAge"].ToString());
                            ToAge = Convert.ToInt32(row["FemaleToAge"].ToString());
                            AgeUnit = row["FemaleAgeUnit"].ToString();
                            string Age = FromAge + "-" + ToAge + " " + AgeUnit;
                            if ((FromAge == 0 ) && ToAge == 100 && AgeUnit == "Year")
                            {
                                // string sasm = row["TSASMId"].ToString();
                                if (lstSASM.Contains(sasm) == false)
                                {
                                    lstSASM.Add(sasm);
                                    //  string disabled = (row["sResultType"].ToString().ToLower() == "quantitative" && row["ReferenceType"].ToString().ToLower() == "reference as per gender and age") ? "disabled='disabled'" : "";
                                    tabTestValueResult += "<tr>" +
                                                      "<td scope='col' style='display:none'>" + row["sTestId"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'>" + row["sTestCode"].ToString() + "</td>" +
                                                      "<td scope='col'><input type='hidden' id='hiddenAnalyteSA" + count + "' name='hiddenAnalyteSA" + count + "' value='" + row["sAnalyteName"].ToString() + "'/>" + row["sAnalyteName"].ToString() + "</td>" +
                                                      "<td scope='col'><input type='hidden' id='hiddenSubAnalyteSA" + count + "' name='hiddenSubAnalyteSA" + count + "' value='" + row["sSubAnalyteName"].ToString() + "'/>" + row["sSubAnalyteName"].ToString() + "</td>" +
                                                      "<td scope='col'><input type='hidden' id='hiddenSampleTypeSA" + count + "' name='hiddenSampleTypeSA" + count + "' value='" + row["sSampleType"].ToString() + "'/>" + row["sSampleType"].ToString() + "</td>" +
                                                      "<td scope='col'><input type='hidden' id='hiddenMethodNameSA" + count + "' name='hiddenMethodNameSA" + count + "' value='" + row["sMethodName"].ToString() + "'/>" + row["sMethodName"].ToString() + "</td>" +
                                                      "<td scope='col'><input type='hidden' id='hiddenResultTypeSA" + count + "' name='hiddenResultTypeSA" + count + "' value='" + row["sResultType"].ToString() + "'/>" + row["sResultType"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenReferenceTypeSA" + count + "' name='hiddenReferenceTypeSA" + count + "' value='" + row["ReferenceType"].ToString() + "'/>" + row["ReferenceType"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenAgeSA" + count + "' name='hiddenAgeSA" + count + "' value='" + Age + "'/>" + Age + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenFemaleSA" + count + "' name='hiddenFemaleSA" + count + "' value='" + row["FemaleMinValue"].ToString() + "'/>" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenFemaleSA1" + count + "' name='hiddenFemaleSA1" + count + "' value='" + row["FemaleMaxValue"].ToString() + "'/>" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenGradeSA" + count + "' name='hiddenGradeSA" + count + "' value='" + row["Grade"].ToString() + "'/>" + row["Grade"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenUnitsSA" + count + "' name='hiddenUnitsSA" + count + "' value='" + row["Unit"].ToString() + "'/>" + row["Unit"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenInterpretationSA" + count + "' name='hiddenInterpretationSA" + count + "' value='" + row["Interpretation"].ToString() + "'/>" + row["Interpretation"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenUpperLimitSA" + count + "' name='hiddenUpperLimitSA" + count + "' value='" + row["UpperLimit"].ToString() + "'/>" + row["UpperLimit"].ToString() + "</td>" +
                                                      "<td scope='col' style='display:none'><input type='hidden' id='hiddenLowerLimitSA" + count + "' name='hiddenLowerLimitSA" + count + "' value='" + row["LowerLimit"].ToString() + "'/>" + row["LowerLimit"].ToString() + "</td>" +
                                                      "<td scope='col'><input type='text' class='form-control' name='valueSA" + count + "' id='SAn|" + row["TSASMId"].ToString() + "|" + row["sResultType"].ToString() + "|" + row["ReferenceType"].ToString() + "|" + patientGender + "|" + patientAge + "'  clientIdMode='static' onkeyup='javascript:loadResultBasedOnValue(this)'/></td>";

                                    tabTestValueResult += "<td scope='col' style='width: 117px;'><input type='hidden' id='hiddenFemaleReferenceSA" + count + "' name='hiddenFemaleReferenceSA" + count + "' value='" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + "'/>" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + " </td>";
                                    //tabTestValueResult += "<td scope='col' style='width: 117px;'>" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + " </td>";
                                    tabTestValueResult += "<td scope='col'><select class='form-control select2 select2-hidden-accessible' name='resultSA" + count + "' id='resultSA" + count + "' clientIdMode='static'>";
                                    tabTestValueResult += "<option value='select'>select</option>";

                                    ////Get Test Sub Analyte Interpretation
                                    //  DataTable dtInterpretation = objReport.GetTestSubAnylteInterpretaion(sasm);
                                    //  if (dtInterpretation.Rows.Count > 0)
                                    //  {
                                    //      foreach (DataRow rowInterprtation in dtInterpretation.Rows)
                                    //      {
                                    //          tabTestValueResult += "<option value='" + rowInterprtation["Interpretation"].ToString() + "'>" + rowInterprtation["Interpretation"].ToString() + "</option>";
                                    //      }
                                    //  }

                                    if (row["sResultType"].ToString().ToLower() == "quantitative")
                                    {
                                        string Testcode = row["sTestCode"].ToString().Trim();
                                        switch (Testcode.ToLower())
                                        {
                                            case "afp":
                                                if (row["sProfileName"].ToString().ToLower().Trim() == "cancer profile")
                                                {
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                          "<option value='Possiblity of tumor'>Possiblity of tumor</option>";
                                                }
                                                if (row["sProfileName"].ToString().Trim() == "PREGNANCY PROFILE")
                                                {
                                                    tabTestValueResult += "<option value='Non-Pregnant'>Non-Pregnant</option>" +
                                                                          "<option value='Pregnant'>Pregnant</option>" +
                                                                          "<option value='New Born'>New Born</option>";
                                                }
                                                break;
                                            case "fsh":
                                                tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                        "<option value='Premenopausal Follicular'>Premenopausal Follicular</option>" +
                                                                        "<option value='Premenopausal Midcycle'>Premenopausal Midcycle</option>" +
                                                                        "<option value='Premenopausal Luteal'>Premenopausal Luteal</option>" +
                                                                        "<option value='Postmenopausal'>Postmenopausal</option>";

                                                break;
                                            case "lh":
                                                if (row["sProfileName"].ToString().ToUpper().Trim() == "PITUITORY PROFILE" || row["sProfileName"].ToString().ToUpper().Trim() == "INFERTILITY PROFILE")
                                                {
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                          "<option value='Pregnant'>Pregnant</option>" +
                                                                          "<option value='Menopause'>Menopause</option>";
                                                }
                                                if (row["sProfileName"].ToString().ToUpper().Trim() == "HORMONAL PROFILE")
                                                {
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                          "<option value='Pregnant'>Pregnant</option>" +
                                                                          "<option value='Menopause'>Menopause</option>" +
                                                                          "<option value='Follicular'>Follicular</option>" +
                                                                          "<option value='Ovulation'>Ovulation</option>";
                                                }
                                                break;
                                            case "pgsn":
                                                tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                          "<option value='Follicular Phase'>Follicular Phase</option>" +
                                                                          "<option value='Ovulation Phase'>Ovulation Phase</option>" +
                                                                          "<option value='Luteal Phase'>Luteal Phase</option>" +
                                                                          "<option value='1st Trimester Pregnancy'>1st Trimester Pregnancy</option>" +
                                                                          "<option value='2nd Trimester Pregnancy'>2nd Trimester Pregnancy</option>" +
                                                                          "<option value='3rd Trimester Pregnancy'>3rd Trimester Pregnancy</option>";
                                                break;
                                            case "thcg":
                                                tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                           "<option value='Indeterminate'>Indeterminate</option>" +
                                                                           "<option value='Positive'>Positive</option>";
                                                break;
                                            case "semb":
                                                tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                           "<option value='Positive'>Positive</option>" +
                                                                            "<option value='Normal'>Normal</option>";
                                                break;

                                            case "ca19":
                                                tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                      "<option value='Normal'>Normal</option>";
                                                break;
                                            case "psaft":
                                                tabTestValueResult += "<option value='49% to 65% risk of prostate cancer depending on age'>49% to 65% risk of prostate cancer depending on age</option>" +
                                                                           "<option value='27% to 41% risk of prostate cancer depending on age'>27% to 41% risk of prostate cancer depending on age</option>" +
                                                                            "<option value='18% to 30% risk of prostate cancer depending on age'>18% to 30% risk of prostate cancer depending on age</option>" +
                                                                            "<option value='9% to 16% risk of prostate cancer depending on age'>9% to 16% risk of prostate cancer depending on age</option>";
                                                break;
                                            case "ca25":
                                                tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                       "<option value='Positive'>Positive</option>" +
                                                                       "<option value='Negative'>Negative</option>" +
                                                                       "<option value='Normal'>Normal</option>";
                                                break;
                                            case "ca153":
                                                tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                      "<option value='Normal'>Normal</option>";

                                                break;
                                            case "hba1c":
                                                tabTestValueResult += "<option value='Normal'>Normal</option>" +
                                                                       "<option value='Pre-diabetic'>Pre-diabetic</option>" +
                                                                       "<option value='Diabetic'>Diabetic</option>";
                                                break;

                                            case "phsm":
                                                tabTestValueResult += "<option value='Vaccinated'>Vaccinated</option>" +
                                                                      "<option value='Unvaccinated'>Unvaccinated</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                      "<option value='NA'>NA</option>";
                                                break;
                                            case "qft3":
                                                tabTestValueResult += "<option value='Strong Positive'>Strong Positive</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                      "<option value='NA'>NA</option>";
                                                break;
                                            case "hepb":
                                                tabTestValueResult += "<option value='Vaccinated'>Vaccinated</option>" +
                                                                      "<option value='Unvaccinated'>Unvaccinated</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                      "<option value='NA'>NA</option>";
                                                break;
                                            case "flepm":
                                                tabTestValueResult += "<option value='Equivocal'>Equivocal</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                      "<option value='NA'>NA</option>";
                                                break;
                                            case "spsa":
                                                tabTestValueResult += "<option value=''></option>";
                                                break;
                                            default:
                                                {
                                                    if (Testcode.ToLower() == "ahpm" || Testcode.ToLower() == "ige" || Testcode.ToLower() == "CHIKV" || Testcode.ToLower() == "cmvp"
                                                        || Testcode.ToLower() == "dengm" || Testcode.ToLower() == "dnsag" || Testcode.ToLower() == "hav" || Testcode.ToLower() == "corab"
                                                        || Testcode.ToLower() == "heag" || Testcode.ToLower() == "hcscr" || Testcode.ToLower() == "hcvl" || Testcode.ToLower() == "hevg"
                                                        || Testcode.ToLower() == "hevm" || Testcode.ToLower() == "mhsv" || Testcode.ToLower() == "hsmr" || Testcode.ToLower() == "hsvg"
                                                        || Testcode.ToLower() == "vhsv" || Testcode.ToLower() == "vdrl")
                                                    {
                                                        tabTestValueResult += "<option value='Positive'>Positive</option>" +
                                                                   "<option value='Negative'>Negative</option>" +
                                                                   "<option value='NA'>NA</option>";
                                                    }
                                                    else
                                                    {
                                                        DataSet dsgetsInterpretation = objReport.getInterpretationResult(row["sTestCode"].ToString());
                                                        if (dsgetsInterpretation.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow row1 in dsgetsInterpretation.Tables[0].Rows)
                                                            {
                                                                tabTestValueResult += "<option value='" + row1["Interpretation"].ToString() + "'>" + row1["Interpretation"].ToString() + "</option>";
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    else if (row["sResultType"].ToString().ToLower() == "qualitative" || row["sResultType"].ToString() == "Qualitative")
                                    {
                                        string Testcode = row["sTestCode"].ToString().Trim();
                                        switch (Testcode)
                                        {
                                            case "BG":
                                                tabTestValueResult += "<option value='NA'>A +Ve</option>" +
                                                                      "<option value='Negative'>B +Ve</option>" +
                                                                     "<option value='Negative'>O +Ve</option>" +
                                                "<option value='Negative'>AB +Ve</option>" +
                                                "<option value='Negative'>AB -Ve</option>" +
                                                "<option value='Negative'>B -Ve</option>" +
                                                "<option value='Negative'>A -Ve</option>" +
                                                 "<option value='Negative'>O -Ve</option>" +
                                                        "<option value='Negative'>Positive</option>" +
                                                        "<option value='Negative'>Negative</option>";
                                                break;
                                            case "FDLS":
                                                tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                     "<option value='Negative'>Positive</option>";
                                                break;
                                            case "HIV":

                                                tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                       "<option value='Negative'>Positive</option>";
                                                break;
                                            case "SPSM":

                                                tabTestValueResult += "<option value='Adequate'>Adequate</option>" +
                                                                      "<option value='NA'>NA</option>" +
                                                                       "<option value='Normal'>Normal</option>" +
                                                                       "<option value='Normocytic Normochomic'>Normocytic Normochomic</option>";
                                                break;
                                            case "MT":
                                                tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                       "<option value='NA'>NA</option>";
                                                break;
                                            case "RUB":
                                                tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                      "<option value='Positive'>Positive</option>" +
                                                                       "<option value='NA'>NA</option>";
                                                break;
                                            default:
                                                DataSet dsgetsInterpretation = objReport.getsInterpretation("qualitative");
                                                if (dsgetsInterpretation.Tables[0].Rows.Count > 0)
                                                {
                                                    foreach (DataRow row1 in dsgetsInterpretation.Tables[0].Rows)
                                                    {
                                                        tabTestValueResult += "<option value='" + row1["Interpretation"].ToString() + "'>" + row1["Interpretation"].ToString() + "</option>";
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    else if (row["sResultType"].ToString().ToLower() == "descriptive")
                                    {
                                        if (row["sTestCode"].ToString().Trim() == "UER")
                                        {
                                            tabTestValueResult += "<option value='Normal'>Normal</option>" +
                                                                  "<option value='Abnormal'>Abnormal</option>" +
                                                                  "<option value='NA'>NA</option>" +
                                                                  "<option value='Present'>Present</option>" +
                                                                  "<option value='Absent'>Absent</option>";
                                        }
                                        else if (row["sTestCode"].ToString().Trim() == "COVID19")
                                        {
                                            tabTestValueResult += "<option value='Positive'>Positive</option>" +
                                                                  "<option value='Negative'>Negative</option>" +
                                                                  "<option value='Inconclusive'>Inconclusive</option>" +
                                                                  "<option value='Invalid'>Invalid</option>";
                                        }
                                        else
                                        {
                                            tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                  "<option value='Not Detected'>Not Detected</option>" +
                                                                  "<option value='NA'>NA</option>" +
                                                                  "<option value='No Growth'>No Growth</option>" +
                                                                  "<option value='Growth'>Growth</option>" +
                                                                  "<option value='Positive'>Positive</option>";
                                        }
                                    }
                                    tabTestValueResult += "</select></td>" +
                                                   "</tr>";
                                    count++;
                                }
                                flag = 1;
                                btnSubmit.Visible = true;
                            }
                          
                            else
                            {
                                if (FromAge <= PatientAge && ToAge >= PatientAge && AgeUnit == patientageunit)
                                {
                                    // string sasm = row["TSASMId"].ToString();
                                    if (lstSASM.Contains(sasm) == false)
                                    {
                                        lstSASM.Add(sasm);
                                        //  string disabled = (row["sResultType"].ToString().ToLower() == "quantitative" && row["ReferenceType"].ToString().ToLower() == "reference as per gender and age") ? "disabled='disabled'" : "";
                                        tabTestValueResult += "<tr>" +
                                                          "<td scope='col' style='display:none'>" + row["sTestId"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'>" + row["sTestCode"].ToString() + "</td>" +
                                                          "<td scope='col'><input type='hidden' id='hiddenAnalyteSA" + count + "' name='hiddenAnalyteSA" + count + "' value='" + row["sAnalyteName"].ToString() + "'/>" + row["sAnalyteName"].ToString() + "</td>" +
                                                          "<td scope='col'><input type='hidden' id='hiddenSubAnalyteSA" + count + "' name='hiddenSubAnalyteSA" + count + "' value='" + row["sSubAnalyteName"].ToString() + "'/>" + row["sSubAnalyteName"].ToString() + "</td>" +
                                                          "<td scope='col'><input type='hidden' id='hiddenSampleTypeSA" + count + "' name='hiddenSampleTypeSA" + count + "' value='" + row["sSampleType"].ToString() + "'/>" + row["sSampleType"].ToString() + "</td>" +
                                                          "<td scope='col'><input type='hidden' id='hiddenMethodNameSA" + count + "' name='hiddenMethodNameSA" + count + "' value='" + row["sMethodName"].ToString() + "'/>" + row["sMethodName"].ToString() + "</td>" +
                                                          "<td scope='col'><input type='hidden' id='hiddenResultTypeSA" + count + "' name='hiddenResultTypeSA" + count + "' value='" + row["sResultType"].ToString() + "'/>" + row["sResultType"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenReferenceTypeSA" + count + "' name='hiddenReferenceTypeSA" + count + "' value='" + row["ReferenceType"].ToString() + "'/>" + row["ReferenceType"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenAgeSA" + count + "' name='hiddenAgeSA" + count + "' value='" + Age + "'/>" + Age + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenFemaleSA" + count + "' name='hiddenFemaleSA" + count + "' value='" + row["FemaleMinValue"].ToString() + "'/>" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenFemaleSA1" + count + "' name='hiddenFemaleSA1" + count + "' value='" + row["FemaleMaxValue"].ToString() + "'/>" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenGradeSA" + count + "' name='hiddenGradeSA" + count + "' value='" + row["Grade"].ToString() + "'/>" + row["Grade"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenUnitsSA" + count + "' name='hiddenUnitsSA" + count + "' value='" + row["Unit"].ToString() + "'/>" + row["Unit"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenInterpretationSA" + count + "' name='hiddenInterpretationSA" + count + "' value='" + row["Interpretation"].ToString() + "'/>" + row["Interpretation"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenUpperLimitSA" + count + "' name='hiddenUpperLimitSA" + count + "' value='" + row["UpperLimit"].ToString() + "'/>" + row["UpperLimit"].ToString() + "</td>" +
                                                          "<td scope='col' style='display:none'><input type='hidden' id='hiddenLowerLimitSA" + count + "' name='hiddenLowerLimitSA" + count + "' value='" + row["LowerLimit"].ToString() + "'/>" + row["LowerLimit"].ToString() + "</td>" +
                                                          "<td scope='col'><input type='text' class='form-control' name='valueSA" + count + "' id='SAn|" + row["TSASMId"].ToString() + "|" + row["sResultType"].ToString() + "|" + row["ReferenceType"].ToString() + "|" + patientGender + "|" + patientAge + "'  clientIdMode='static' onkeyup='javascript:loadResultBasedOnValue(this)'/></td>";

                                        tabTestValueResult += "<td scope='col' style='width: 117px;'><input type='hidden' id='hiddenFemaleReferenceSA" + count + "' name='hiddenFemaleReferenceSA" + count + "' value='" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + "'/>" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + " </td>";
                                        //tabTestValueResult += "<td scope='col' style='width: 117px;'>" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + " </td>";
                                        tabTestValueResult += "<td scope='col'><select class='form-control select2 select2-hidden-accessible' name='resultSA" + count + "' id='resultSA" + count + "' clientIdMode='static'>";
                                        tabTestValueResult += "<option value='select'>select</option>";

                                        ////Get Test Sub Analyte Interpretation
                                        //  DataTable dtInterpretation = objReport.GetTestSubAnylteInterpretaion(sasm);
                                        //  if (dtInterpretation.Rows.Count > 0)
                                        //  {
                                        //      foreach (DataRow rowInterprtation in dtInterpretation.Rows)
                                        //      {
                                        //          tabTestValueResult += "<option value='" + rowInterprtation["Interpretation"].ToString() + "'>" + rowInterprtation["Interpretation"].ToString() + "</option>";
                                        //      }
                                        //  }

                                        if (row["sResultType"].ToString().ToLower() == "quantitative")
                                        {
                                            string Testcode = row["sTestCode"].ToString().Trim();
                                            switch (Testcode.ToLower())
                                            {
                                                case "afp":
                                                    if (row["sProfileName"].ToString().ToLower().Trim() == "cancer profile")
                                                    {
                                                        tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                              "<option value='Negative'>Negative</option>" +
                                                                              "<option value='Possiblity of tumor'>Possiblity of tumor</option>";
                                                    }
                                                    if (row["sProfileName"].ToString().Trim() == "PREGNANCY PROFILE")
                                                    {
                                                        tabTestValueResult += "<option value='Non-Pregnant'>Non-Pregnant</option>" +
                                                                              "<option value='Pregnant'>Pregnant</option>" +
                                                                              "<option value='New Born'>New Born</option>";
                                                    }
                                                    break;
                                                case "fsh":
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                            "<option value='Premenopausal Follicular'>Premenopausal Follicular</option>" +
                                                                            "<option value='Premenopausal Midcycle'>Premenopausal Midcycle</option>" +
                                                                            "<option value='Premenopausal Luteal'>Premenopausal Luteal</option>" +
                                                                            "<option value='Postmenopausal'>Postmenopausal</option>";

                                                    break;
                                                case "lh":
                                                    if (row["sProfileName"].ToString().ToUpper().Trim() == "PITUITORY PROFILE" || row["sProfileName"].ToString().ToUpper().Trim() == "INFERTILITY PROFILE")
                                                    {
                                                        tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                              "<option value='Pregnant'>Pregnant</option>" +
                                                                              "<option value='Menopause'>Menopause</option>";
                                                    }
                                                    if (row["sProfileName"].ToString().ToUpper().Trim() == "HORMONAL PROFILE")
                                                    {
                                                        tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                              "<option value='Pregnant'>Pregnant</option>" +
                                                                              "<option value='Menopause'>Menopause</option>" +
                                                                              "<option value='Follicular'>Follicular</option>" +
                                                                              "<option value='Ovulation'>Ovulation</option>";
                                                    }
                                                    break;
                                                case "pgsn":
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                              "<option value='Follicular Phase'>Follicular Phase</option>" +
                                                                              "<option value='Ovulation Phase'>Ovulation Phase</option>" +
                                                                              "<option value='Luteal Phase'>Luteal Phase</option>" +
                                                                              "<option value='1st Trimester Pregnancy'>1st Trimester Pregnancy</option>" +
                                                                              "<option value='2nd Trimester Pregnancy'>2nd Trimester Pregnancy</option>" +
                                                                              "<option value='3rd Trimester Pregnancy'>3rd Trimester Pregnancy</option>";
                                                    break;
                                                case "thcg":
                                                    tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                               "<option value='Indeterminate'>Indeterminate</option>" +
                                                                               "<option value='Positive'>Positive</option>";
                                                    break;
                                                case "semb":
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                               "<option value='Positive'>Positive</option>" +
                                                                                "<option value='Normal'>Normal</option>";
                                                    break;

                                                case "ca19":
                                                    tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                          "<option value='Normal'>Normal</option>";
                                                    break;
                                                case "psaft":
                                                    tabTestValueResult += "<option value='49% to 65% risk of prostate cancer depending on age'>49% to 65% risk of prostate cancer depending on age</option>" +
                                                                               "<option value='27% to 41% risk of prostate cancer depending on age'>27% to 41% risk of prostate cancer depending on age</option>" +
                                                                                "<option value='18% to 30% risk of prostate cancer depending on age'>18% to 30% risk of prostate cancer depending on age</option>" +
                                                                                "<option value='9% to 16% risk of prostate cancer depending on age'>9% to 16% risk of prostate cancer depending on age</option>";
                                                    break;
                                                case "ca25":
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                           "<option value='Positive'>Positive</option>" +
                                                                           "<option value='Negative'>Negative</option>" +
                                                                           "<option value='Normal'>Normal</option>";
                                                    break;
                                                case "ca153":
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                          "<option value='Normal'>Normal</option>";

                                                    break;
                                                case "hba1c":
                                                    tabTestValueResult += "<option value='Normal'>Normal</option>" +
                                                                           "<option value='Pre-diabetic'>Pre-diabetic</option>" +
                                                                           "<option value='Diabetic'>Diabetic</option>";
                                                    break;

                                                case "phsm":
                                                    tabTestValueResult += "<option value='Vaccinated'>Vaccinated</option>" +
                                                                          "<option value='Unvaccinated'>Unvaccinated</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                          "<option value='NA'>NA</option>";
                                                    break;
                                                case "qft3":
                                                    tabTestValueResult += "<option value='Strong Positive'>Strong Positive</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                          "<option value='NA'>NA</option>";
                                                    break;
                                                case "hepb":
                                                    tabTestValueResult += "<option value='Vaccinated'>Vaccinated</option>" +
                                                                          "<option value='Unvaccinated'>Unvaccinated</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                          "<option value='NA'>NA</option>";
                                                    break;
                                                case "flepm":
                                                    tabTestValueResult += "<option value='Equivocal'>Equivocal</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                          "<option value='NA'>NA</option>";
                                                    break;
                                                case "spsa":
                                                    tabTestValueResult += "<option value=''></option>";
                                                    break;
                                                default:
                                                    {
                                                        if (Testcode.ToLower() == "ahpm" || Testcode.ToLower() == "ige" || Testcode.ToLower() == "CHIKV" || Testcode.ToLower() == "cmvp"
                                                            || Testcode.ToLower() == "dengm" || Testcode.ToLower() == "dnsag" || Testcode.ToLower() == "hav" || Testcode.ToLower() == "corab"
                                                            || Testcode.ToLower() == "heag" || Testcode.ToLower() == "hcscr" || Testcode.ToLower() == "hcvl" || Testcode.ToLower() == "hevg"
                                                            || Testcode.ToLower() == "hevm" || Testcode.ToLower() == "mhsv" || Testcode.ToLower() == "hsmr" || Testcode.ToLower() == "hsvg"
                                                            || Testcode.ToLower() == "vhsv" || Testcode.ToLower() == "vdrl")
                                                        {
                                                            tabTestValueResult += "<option value='Positive'>Positive</option>" +
                                                                       "<option value='Negative'>Negative</option>" +
                                                                       "<option value='NA'>NA</option>";
                                                        }
                                                        else
                                                        {
                                                            DataSet dsgetsInterpretation = objReport.getInterpretationResult(row["sTestCode"].ToString());
                                                            if (dsgetsInterpretation.Tables[0].Rows.Count > 0)
                                                            {
                                                                foreach (DataRow row1 in dsgetsInterpretation.Tables[0].Rows)
                                                                {
                                                                    tabTestValueResult += "<option value='" + row1["Interpretation"].ToString() + "'>" + row1["Interpretation"].ToString() + "</option>";
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        else if (row["sResultType"].ToString().ToLower() == "qualitative" || row["sResultType"].ToString() == "Qualitative")
                                        {
                                            string Testcode = row["sTestCode"].ToString().Trim();
                                            switch (Testcode)
                                            {
                                                case "BG":
                                                    tabTestValueResult += "<option value='NA'>A +Ve</option>" +
                                                                          "<option value='Negative'>B +Ve</option>" +
                                                                         "<option value='Negative'>O +Ve</option>" +
                                                    "<option value='Negative'>AB +Ve</option>" +
                                                    "<option value='Negative'>AB -Ve</option>" +
                                                    "<option value='Negative'>B -Ve</option>" +
                                                    "<option value='Negative'>A -Ve</option>" +
                                                    "<option value='Negative'>O -Ve</option>"+
                                                        "<option value='Negative'>Positive</option>" +
                                                        "<option value='Negative'>Negative</option>";
                                                    break;
                                                case "FDLS":
                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                         "<option value='Negative'>Positive</option>";
                                                    break;
                                                case "HIV":

                                                    tabTestValueResult += "<option value='NA'>NA</option>" +
                                                                          "<option value='Negative'>Negative</option>" +
                                                                           "<option value='Negative'>Positive</option>";
                                                    break;
                                                case "SPSM":

                                                    tabTestValueResult += "<option value='Adequate'>Adequate</option>" +
                                                                          "<option value='NA'>NA</option>" +
                                                                           "<option value='Normal'>Normal</option>" +
                                                                           "<option value='Normocytic Normochomic'>Normocytic Normochomic</option>";
                                                    break;
                                                case "MT":
                                                    tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                           "<option value='NA'>NA</option>";
                                                    break;
                                                case "RUB":
                                                    tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                          "<option value='Positive'>Positive</option>" +
                                                                           "<option value='NA'>NA</option>";
                                                    break;
													   case "WIDAL":
                                                    tabTestValueResult += "<option value='Negative'>Nil</option>";
                                                                        
                                                    break;
                                                default:
                                                    DataSet dsgetsInterpretation = objReport.getsInterpretation("qualitative");
                                                    if (dsgetsInterpretation.Tables[0].Rows.Count > 0)
                                                    {
                                                        foreach (DataRow row1 in dsgetsInterpretation.Tables[0].Rows)
                                                        {
                                                            tabTestValueResult += "<option value='" + row1["Interpretation"].ToString() + "'>" + row1["Interpretation"].ToString() + "</option>";
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        else if (row["sResultType"].ToString().ToLower() == "descriptive")
                                        {
                                            if (row["sTestCode"].ToString().Trim() == "UER")
                                            {
                                                tabTestValueResult += "<option value='Normal'>Normal</option>" +
                                                                      "<option value='Abnormal'>Abnormal</option>" +
                                                                      "<option value='NA'>NA</option>" +
                                                                      "<option value='Present'>Present</option>" +
                                                                      "<option value='Absent'>Absent</option>";
                                            }
                                            else if (row["sTestCode"].ToString().Trim() == "COVID19")
                                            {
                                                tabTestValueResult += "<option value='Positive'>Positive</option>" +
                                                                      "<option value='Negative'>Negative</option>" +
                                                                      "<option value='Inconclusive'>Inconclusive</option>" +
                                                                      "<option value='Invalid'>Invalid</option>";
                                            }
                                            else
                                            {
                                                tabTestValueResult += "<option value='Negative'>Negative</option>" +
                                                                      "<option value='Not Detected'>Not Detected</option>" +
                                                                      "<option value='NA'>NA</option>" +
                                                                      "<option value='No Growth'>No Growth</option>" +
                                                                      "<option value='Growth'>Growth</option>" +
                                                                      "<option value='Positive'>Positive</option>";
                                            }
                                        }
                                        tabTestValueResult += "</select></td>" +
                                                       "</tr>";
                                        count++;
                                    }
                                    flag = 1;
                                    btnSubmit.Visible = true;
                                }
                                else
                                {
                                    if (flag == 0)
                                    {
                                        btnSubmit.Visible = false;
                                    }
                                    else
                                    {
                                        btnSubmit.Visible = true;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
                else
                {

                }
            }
            #endregion
            tbodyTestValueResult.InnerHtml = tabTestValueResult;
        }
        catch(Exception ex)
        {
            Response.Redirect("CreateReport.aspx" ,false);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Enabled = false;
            bookLabTestId = Request.QueryString["bookLabTestId"].ToString();
            testId = Request.QueryString["testId"].ToString();
            bookLabId = Request.QueryString["bookId"].ToString();
            string reportStatus = "Created";
            string approvalStatus = "Approval Pending";
            string notes = txtNotes.Value;
            string patientID = spanPatientID.InnerText.ToString();
            string reportCreatedOn = DateTime.Now.AddHours(12).AddMinutes(30).ToString("dd/MM/yyyy HH:mm:ss");
            string reportCreatedBy = Request.Cookies["labUser"].Value.ToString();

            if (hiddenAnalyteCount.Value != "" && hiddenAnalyteCount.Value != null && hiddenAnalyteCount.Value != "0")
            {
                for (int i = 1; i <= Convert.ToInt32(hiddenAnalyteCount.Value); i++)
                {
                    string InputVal = (Request.Form["valueA" + i] != "") ? Request.Form["valueA" + i] : "NA";
                    String resultval = Request.Form["resultA" + i];//CryptoHelper.Encrypt(Request.Form["resultA" + i]);
                    if (InputVal != null && resultval.ToLower() != "select")
                    {
                        String value = CryptoHelper.Encrypt(InputVal);
                        string MaleRange = Request.Form["hiddenMaleReferenceA" + i];
                        string FemaleRange = Request.Form["hiddenFemaleReferenceA" + i];
                        SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sBookLabTestId",bookLabTestId),
                        new SqlParameter("@sTestId",testId),
                        new SqlParameter("@sAnalyte",Request.Form["hiddenAnalyteA" + i]),
                        new SqlParameter("@sSubAnalyte",""),
                        new SqlParameter("@sSpecimen",Request.Form["hiddenSampleTypeA" + i]),
                        new SqlParameter("@sMethod",Request.Form["hiddenMethodNameA" + i]),
                        new SqlParameter("@sResultType",Request.Form["hiddenResultTypeA" + i]),
                        new SqlParameter("@sReferenceType",Request.Form["hiddenReferenceTypeA" + i]),
                        new SqlParameter("@sAge",Request.Form["hiddenAgeA" + i]),
                        new SqlParameter("@sMale",(MaleRange != null) ? MaleRange : ""),
                        new SqlParameter("@sFemale",(FemaleRange != null) ? FemaleRange : ""),
                        new SqlParameter("@sGrade",Request.Form["hiddenGradeA" + i]),
                        new SqlParameter("@sUnits",Request.Form["hiddenUnitsA" + i]),
                        new SqlParameter("@sInterpretation",Request.Form["hiddenInterpretationA" + i]),
                        new SqlParameter("@sLowerLimit",(Request.Form["hiddenLowerLimitA" + i] != null) ? Request.Form["hiddenLowerLimitA" + i] : ""),
                        new SqlParameter("@sUpperLimit",(Request.Form["hiddenUpperLimitA" + i] != null) ? Request.Form["hiddenUpperLimitA" + i] : ""),
                        new SqlParameter("@sValue",value),
                        new SqlParameter("@sResult",CryptoHelper.Encrypt(Request.Form["resultA" + i])),
                        new SqlParameter("@returnval",SqlDbType.Int)
                    };
                        int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddTestReport", param);
                    }
                }
            }

            if (hiddenSubAnalyteCount.Value != "" && hiddenSubAnalyteCount.Value != null && hiddenSubAnalyteCount.Value != "0")
            {
                for (int i = 1; i <= Convert.ToInt32(hiddenSubAnalyteCount.Value); i++)
                {
                    string InputVal = (Request.Form["valueSA" + i] != "") ? Request.Form["valueSA" + i] : "NA";
                    String resultval = Request.Form["resultSA" + i];
                    if (InputVal != null && resultval.ToLower() != "select")
                    {
                        String value = CryptoHelper.Encrypt(InputVal);

                        string age = Request.Form["hiddenAgeSA" + i].ToString();
                        string MaleRange = Request.Form["hiddenMaleReferenceSA" + i];
                        string FemaleRange = Request.Form["hiddenFemaleReferenceSA" + i];
                        SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sBookLabTestId",bookLabTestId),
                        new SqlParameter("@sTestId",testId),
                        new SqlParameter("@sAnalyte",Request.Form["hiddenAnalyteSA" + i]),
                        new SqlParameter("@sSubAnalyte",Request.Form["hiddenSubAnalyteSA" + i]),
                        new SqlParameter("@sSpecimen",Request.Form["hiddenSampleTypeSA" + i]),
                        new SqlParameter("@sMethod",Request.Form["hiddenMethodNameSA" + i]),
                        new SqlParameter("@sResultType",Request.Form["hiddenResultTypeSA" + i]),
                        new SqlParameter("@sReferenceType",Request.Form["hiddenReferenceTypeSA" + i]),
                        new SqlParameter("@sAge",age),
                        new SqlParameter("@sMale",(MaleRange != null) ? MaleRange : ""),
                        new SqlParameter("@sFemale",(FemaleRange != null) ? FemaleRange : ""),
                        new SqlParameter("@sGrade",Request.Form["hiddenGradeSA" + i]),
                        new SqlParameter("@sUnits",Request.Form["hiddenUnitsSA" + i]),
                        new SqlParameter("@sInterpretation",Request.Form["hiddenInterpretationSA" + i]),
                        new SqlParameter("@sLowerLimit",(Request.Form["hiddenLowerLimitSA" + i]!= null)?Request.Form["hiddenLowerLimitSA" + i]:""),
                        new SqlParameter("@sUpperLimit",(Request.Form["hiddenUpperLimitSA" + i]!= null)?Request.Form["hiddenUpperLimitSA" + i]:""),
                        new SqlParameter("@sValue",value),
                        new SqlParameter("@sResult",CryptoHelper.Encrypt(Request.Form["resultSA" + i])),
                        new SqlParameter("@returnval",SqlDbType.Int)
                    };
                        int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddTestReport", param);
                    }
                }
            }

            //queryReportValues = queryReportValues.TrimStart(',').TrimEnd(',');

            int createReport = objReport.createReport(bookLabTestId, reportStatus, approvalStatus, reportCreatedOn, reportCreatedBy, notes);

            if (createReport == 1)
            {
                // CAlling For Notification Yogesh 
                string labname = "";
                DataSet dsBookingDetails = objBookDetails.getBookTestDetailsUserMobId(bookLabId);
                if (dsBookingDetails.Tables[0].Rows.Count != 0)
                {
                    string mobnotid = dsBookingDetails.Tables[0].Rows[0]["sdeviceToken"].ToString();
                    labname = dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString();
                    string testtime = dsBookingDetails.Tables[0].Rows[0]["sTimeSlot"].ToString();
                    string testdate = dsBookingDetails.Tables[0].Rows[0]["stestdate"].ToString();
                    string msg = "Your report at " + dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString() + " has been created and is awaiting approval.";
                    FCMPushNotification fcm = new FCMPushNotification();
                    fcm.SendNotification("Report Status", msg, mobnotid);
                    string Message = "Your health Profile has been updated.";
                    string script = "{ sendnotification('" + mobnotid + "', '" + Message + "'); };";
                    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "updatePanel1Script", script, true);

                }
                // End CAlling For Notification Yogesh 

                string responseString = "";
                try
                {
                    //HttpWebRequest myReq = (System.Net.HttpWebRequest)WebRequest.Create
                    //    ("http://www.myvaluefirst.com/smpp/sendsms?username=Visionhtptrns&password=trujd@k34&to=918888898701&from=TESTIN&text=otp&dlr-mask=50&dlr-url=https://visionarylifescience.com/CreateReport.aspx");
                    //myReq.Credentials = new System.Net.NetworkCredential("Visionhtptrns", "trujd@k34");
                    //HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    //System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    // responseString = respStreamReader.ReadToEnd();
                    //respStreamReader.Close();
                    //myResp.Close();
                }
                catch (Exception ex)
                {
                    string x = ex.Message;
                }
              

                  string labId = Request.Cookies["labId"].Value.ToString();
                objNotification.AppNotification(patientID, bookLabTestId, "Report Status", "Your report at " + labName + " has been created and is awaiting approval.", "1", reportCreatedOn, reportCreatedBy);
				
                string mob = dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString();
                string mobileNo = CryptoHelper.Decrypt(mob).ToString();
                newWhatsapp wa = new newWhatsapp();
                ////wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
                wa.sendWhatsappMsg("+91" + mobileNo, "Create Report For Lab", spanPatientName.InnerHtml + ',' + hdntestName.Value + ',' + labname, labId);
             
				  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Report created and sent for approval " + responseString + "');location.href='ViewReport.aspx';", true);
 //string mob = dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString();
              //  string mobileNo = CryptoHelper.Decrypt(mob).ToString();
              //  newWhatsapp wa = new newWhatsapp();
                ////wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
              //  wa.sendWhatsappMsg("+91" + mobileNo, "Create Report By Lab", spanPatientName.InnerHtml + ',' + hdntestName.Value + ',' + labname);
               // Response.Redirect("~/CreateReport.aspx");
            }
            else if (createReport == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
            }
        }
        catch(Exception ex)
        {
			 LogError.LoggerCatch(ex);
            Response.Redirect("~/CreateReport.aspx");
        }
    }
    [WebMethod]
    public static object getReferences(string data)
    {
        DataAccessLayer DAL = new DataAccessLayer();
        DataSet dt = new DataSet();
        string response = "";
        string Data = data.Replace("!~^!", "¶");
        string[] DataArr = Data.Split('¶');
        string id = DataArr[0];
        string checkFrom = DataArr[1];
        string Age = DataArr[2];
        string Gender = DataArr[3];
        string Val = DataArr[4];
        string age = Age.Split(' ')[0];
        string AgeUnit = Age.Split(' ')[1];
        float parsedValue;
        if (float.TryParse(Val, out parsedValue))
        {
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
        }
        return JsonConvert.SerializeObject(response);
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
}
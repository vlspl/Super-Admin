using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class report_Default : System.Web.UI.Page
{
    CLSViewReportValuesinTemplate objTemplate = new CLSViewReportValuesinTemplate();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadReport();
    }

    protected void LoadReport()
    {
        string patientAge = "";
        //string bookLabId = Request.QueryString["bookId"].ToString();
        string bookLabTestId = "1485";//Request.QueryString["bookLabTestId"].ToString().Trim();
        DataTable dsTestReport = objTemplate.getTestReportTable(bookLabTestId);
        string DateOfBirth = dsTestReport.Rows[0]["sbirthdate"].ToString();

        DateTime dtDob = DateTime.ParseExact(DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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
        string useremailid = dsTestReport.Rows[0]["semailid"].ToString();
        string patinetname = dsTestReport.Rows[0]["sPatient"].ToString();
        string labname = dsTestReport.Rows[0]["sLabName"].ToString();
        string doctor = dsTestReport.Rows[0]["sDoctor"].ToString();
        string labaddress = dsTestReport.Rows[0]["sLabAddress"].ToString();
        string labContact = dsTestReport.Rows[0]["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(dsTestReport.Rows[0]["sLabContact"].ToString()) : "";
        string testdate = dsTestReport.Rows[0]["stestdate"].ToString();
        string labimage = dsTestReport.Rows[0]["slablogo"].ToString();
        string DMLT = dsTestReport.Rows[0]["sColG"].ToString();
        string MD = dsTestReport.Rows[0]["sColH"].ToString();
        string malevalue = dsTestReport.Rows[0]["sMale"].ToString();
        if (malevalue == "NA") malevalue = "--"; else malevalue = malevalue.ToString();
        string femalevalue = dsTestReport.Rows[0]["sFemale"].ToString();
        if (femalevalue == "NA") femalevalue = "--"; else femalevalue = femalevalue.ToString();

        StringBuilder sb = new StringBuilder();
        #region header
        sb.Append("<div class=\"container\">");
        sb.Append("<header class=\"row pt-4\">");
        sb.Append("<div class=\"col col-md-6\" style=\"height:auto;max-height:200px\">");
        sb.Append("<h6 style=\"color: #2b9392; font-size: 16px;\">" + labname + "</h6>");
        sb.Append("<p>" + labaddress + "</p>");
        sb.Append("<p>" + labContact + "</p>");
        sb.Append("</div>");
        sb.Append("<div class=\"col-md-6\">");
        sb.Append(" <img src='http://localhost:1534/LocalWorkNewDesignWebsite/images/VLS Logo.jpg' class=\"float-right m-2\" style=\"width:100px;height:100px\">");
        sb.Append("</div>");
        sb.Append("</header>");
        sb.Append("<div class=\"row\">");
        sb.Append("<div class=\"col\" style=\"width:100%;height:30px;background: #2b9392;border:4px solid silver\">");
        sb.Append("<h6 class=\"text-center text-white\">LABORATORY REPORT</h6>");
        sb.Append("</div>");
        sb.Append("</div>");
        #endregion
        #region Patient Details
        /* Patient Details */
        sb.Append("<div class=\"row\">");
        sb.Append("<div class=\"col col-md-6 p-2 \">");
        sb.Append("<div class=\"row\">");
        sb.Append("<div class=\"col-md-2\">");
        sb.Append("<p>Name:</p>");
        sb.Append("<p>Date:</p>");
        sb.Append("<p>Doctor:</p>");
        sb.Append("</div>");
        sb.Append("<div class=\"col-md-10\">");
        sb.Append("<p>" + patinetname + "</p>");
        sb.Append("<p>" + testdate + "</p>");
        sb.Append("<p>" + doctor + "</p>");
        sb.Append("</div>");
        sb.Append("</div>");
        sb.Append("</div>");
        sb.Append("<div class=\"col col-md-6 p-2\">");
        sb.Append("<div class=\"row\">");
        sb.Append("<div class=\"col-md-6 offset-md-4\">");
        sb.Append("<p class=\"text-center\">Patient ID:&nbsp;&nbsp;&nbsp;" + dsTestReport.Rows[0]["sPatientId"].ToString() + "</p>");
        sb.Append("</div>");
        sb.Append("</div>");
        sb.Append("<div class=\"row\">");
        sb.Append("<div class=\"col col-md-6\">");
        sb.Append("<p class=\"text-center\">Age:&nbsp;&nbsp;&nbsp;" + patientAge + "</p>");
        sb.Append("</div>");
        sb.Append("<div class=\"col col-md-6\">");
        sb.Append("<p class=\"text-center\">Sex:&nbsp;&nbsp;&nbsp;" + dsTestReport.Rows[0]["sGender"].ToString() + "</p>");
        sb.Append("</div>");
        sb.Append("</div>");
        sb.Append("<div class=\"row\">");
        sb.Append("<div class=\"col-md-6 offset-md-4\">");
        sb.Append("<p class=\"text-center\">Report ID:&nbsp;&nbsp;&nbsp;" + dsTestReport.Rows[0]["sBookLabTestId"].ToString() + "</p>");
        sb.Append("</div>");
        sb.Append("</div>");
        sb.Append("</div>");
        sb.Append("</div>");
        /* Patient Details End */
        #endregion
        #region Test Details
        /* Test Name */
        sb.Append(" <h5 class=\"text-center p-3\" style=\"font-weight: bold; color:#2b9392\">" + dsTestReport.Rows[0]["sTestName"].ToString() + "(" + dsTestReport.Rows[0]["sTestCode"].ToString() + ")</h5>");
        sb.Append("<hr>");
        sb.Append("<div class=\"container-fluid\">");
            sb.Append("<table class=\"table table-borderless\">");
                sb.Append("<thead>");
                    sb.Append("<tr>");
                        sb.Append("<th scope=\"col\">Test Name</th>");
                        sb.Append("<th scope=\"col\">Result</th>");
                        sb.Append("<th scope=\"col\">Normal Range</th>");
                        sb.Append("<th scope=\"col\">Units</th>");
                    sb.Append("</tr>");
                sb.Append("</thead>");
        sb.Append("<tbody>");
        foreach (DataRow row in dsTestReport.Rows)
        {
            sb.Append("<tr>");
            sb.Append("<td class=\"left\">" + row["sAnalyte"].ToString() + "</td>");
            sb.Append("<td>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + "</td>");
            if (dsTestReport.Rows[0]["sGender"].ToString().ToLower() == "male")
            {
                sb.Append("<td>" + row["sMale"].ToString() + "</td>");
            }
            else
            {
                sb.Append("<td>" + row["sFemale"].ToString() + "</td>");
            }

            sb.Append("<td>" + row["sUnits"].ToString() + "</td>");
            sb.Append("</tr>");
        }
        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("</div>");
        sb.Append("<hr>"); 
        sb.Append("</br>"); 
        sb.Append("</br>");
        #endregion
        #region Report Footer
        sb.Append("<div class=\"row \">");
        sb.Append("<div class=\"col col-md4 offset-md-8 float-right\">");
        sb.Append("<img src='http://localhost:1534/LocalWorkNewDesignWebsite/images/VLS Logo.jpg' alt='' style='display: block; height: 42px; width: 114px;'>");
        sb.Append("<p>DR.Shashwati A.Singh</p>");
        sb.Append("<p>MBBS,DCP, MD Pathologist</p>");
        //sb.Append(" <p class=\"pb-2 pt-5\">Digitally signed by</p>");
        //sb.Append(" <p style=\"font-size:14px;font-style: italic;\">Dr. Cameron Cordana</p>");
        //sb.Append(" <p style=\"font-size:14px;\">GNU Public Key: E44311F4</p>");
        //sb.Append(" <p style=\"font-size:14px;\">Test ID:B120123</p>");

        sb.Append("</div>");
        sb.Append("</div>");
        sb.Append("</div>");




        #endregion

        ltrReport.Text = sb.ToString();


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
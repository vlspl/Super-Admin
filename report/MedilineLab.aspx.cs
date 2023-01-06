using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using CrossPlatformAESEncryption.Helper;

public partial class report_MedilineLab : System.Web.UI.Page
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

        spanTestDate.InnerHtml = dsTestReport.Rows[0]["stestdate"].ToString();
        spanBookId.InnerHtml = dsTestReport.Rows[0]["sBookLabId"].ToString();
        spanPatientName.InnerHtml = dsTestReport.Rows[0]["sPatient"].ToString();
        spanPatientAge.InnerHtml = patientAge;
        spanGender.InnerHtml = dsTestReport.Rows[0]["sGender"].ToString();
        spanDoctorName.InnerHtml = dsTestReport.Rows[0]["sDoctor"].ToString();
        spanLabName.InnerHtml = dsTestReport.Rows[0]["sLabId"].ToString();
        spanTestname.InnerHtml = dsTestReport.Rows[0]["sTestName"].ToString();

        string malevalue = dsTestReport.Rows[0]["sMale"].ToString();
        if (malevalue == "NA") malevalue = "--"; else malevalue = malevalue.ToString();
        string femalevalue = dsTestReport.Rows[0]["sFemale"].ToString();
        if (femalevalue == "NA") femalevalue = "--"; else femalevalue = femalevalue.ToString();
        if (dsTestReport != null)
        {
            if (dsTestReport.Rows.Count > 0)
            {
                string tabMyLabSlots = "";

                foreach (DataRow row in dsTestReport.Rows)
                {
                    //Load lab test list
                    tabMyLabSlots += "<tr>" +
                                       "<td class=\"left\">" + row["sAnalyte"].ToString() + "</td>" +
                                       "<td>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + "</td>";
                    if (dsTestReport.Rows[0]["sGender"].ToString().ToLower() == "male")
                    {
                        tabMyLabSlots += "<td>" + row["sMale"].ToString() + "</td>";
                    }
                    else
                    {
                        tabMyLabSlots += "<td>" + row["sFemale"].ToString() + "</td>";
                    }
                    tabMyLabSlots += "<td>" + row["sUnits"].ToString() + "</td>" +
                "</tr>";
                }
                tbodyReport.InnerHtml = tabMyLabSlots;
            }
            else
            {
                tbodyReport.InnerHtml = "<tr><td>No records found</td></tr>";
            }
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
}
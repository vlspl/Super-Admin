using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;
using System.Text;
using CrossPlatformAESEncryption.Helper;

public partial class report_YugnakLab : System.Web.UI.Page
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
        string bookLabTestId = Request.QueryString["bookLabTestId"].ToString().Trim();
        DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId);
        string DateOfBirth = dsTestReport.Tables[0].Rows[0]["sbirthdate"].ToString();

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
        string useremailid = dsTestReport.Tables[0].Rows[0]["semailid"].ToString();
        string patinetname = dsTestReport.Tables[0].Rows[0]["sPatient"].ToString();
        string labname = dsTestReport.Tables[0].Rows[0]["sLabName"].ToString();
        string labaddress = dsTestReport.Tables[0].Rows[0]["sLabAddress"].ToString();
        string testdate = dsTestReport.Tables[0].Rows[0]["stestdate"].ToString();
        string labimage = dsTestReport.Tables[0].Rows[0]["slablogo"].ToString();
        string pdffilename = DateTime.UtcNow.ToString("yyyymmddhhmmssffff");
        string DMLT = dsTestReport.Tables[0].Rows[0]["sColG"].ToString();
        string MD = dsTestReport.Tables[0].Rows[0]["sColH"].ToString();
        string malevalue = dsTestReport.Tables[0].Rows[0]["sMale"].ToString();
        if (malevalue == "NA") malevalue = "--"; else malevalue = malevalue.ToString();
        string femalevalue = dsTestReport.Tables[0].Rows[0]["sFemale"].ToString();
        if (femalevalue == "NA") femalevalue = "--"; else femalevalue = femalevalue.ToString();

        StringBuilder sb = new StringBuilder();

        sb.Append("<!DOCTYPE html>");
        sb.Append("<html>");
        sb.Append("<body class='_body' style='width: 100%; margin: 0; position: relative;'>");
        sb.Append("<div class='wrapper' style='position: relative;padding: 5px;'>");
        // sb.Append(" <img src='https://visionarylifesciences.in/images/" + labimage + "' alt='' style='width: 20%;text-align:center; margin: 0 auto 20px;display: block;'>");
        sb.Append("<h2 style='text-align: center; font-weight: 400;'>" + dsTestReport.Tables[0].Rows[0]["sLabName"].ToString() + "</h2>");
        sb.Append("<h5 style='text-align: center; font-weight: 400;'>" + dsTestReport.Tables[0].Rows[0]["sLabAddress"].ToString() + "</h5>");
        sb.Append("<table style='position: relative;width: 100%; border: 1px solid #000; text-align: left; padding: 15px; margin-bottom: 10px;'>");

        sb.Append("<tr>");
        sb.Append("<th style='padding: 5px;'>Patient Name :</th>");
        sb.Append("<td style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["sFullName"].ToString() + "</td>");
        sb.Append("<th v>Age/Sex :</th>");
        sb.Append("<td style='padding: 5px;'>" + patientAge + " / " + dsTestReport.Tables[0].Rows[0]["sGender"].ToString() + "</td>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append(" <th style='padding: 5px;'>Referred By :</th>");
        sb.Append("<td style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["sDoctor"].ToString() + "</td>");
        sb.Append("<th style='padding: 5px;'>Date : </th>");
        sb.Append("<td style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["sBookRequestedAt"].ToString() + "</td>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<th style='padding: 5px;'>Reg. no.</th>");
        sb.Append("<td style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["sBookLabId"].ToString() + "</td>");
        sb.Append("<th style='padding: 5px;'>Report ID:</th>");
        sb.Append("<td style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["sBookLabTestId"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        sb.Append("</hr>");
        sb.Append("<h3 style='text-align: center;text-transform: uppercase; margin: 0; line-height: 1.5em;'></h3>");
        sb.Append("<h3 style='text-align: center;font-weight: 600; text-transform: uppercase; margin: 0; line-height: 1.5em;'>" + dsTestReport.Tables[0].Rows[0]["sTestName"].ToString() + "(" + dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() + ")</h3>");

        sb.Append("</hr>");
        sb.Append("<table style='padding: 0px; font-family:Verdana;font-weight:100; width: 100%; border-top: 1px solid; border-bottom: 1px solid; text-align: left;'>");
        sb.Append("<tr>");
        sb.Append("<th style='width: 30%;'>TEST</th>");
        sb.Append("<th style='width: 9%;'>VALUE</th>");
        sb.Append("<th style='width: 15%;'>UNIT</th>");
        sb.Append("<th style='width: 20%;'>Result</th>");
        sb.Append(" <th style='width:30%;'>REFERENCE</th>");
        sb.Append("</tr>");
        if (dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() == "COVID19")
        {
            foreach (DataRow row in dsTestReport.Tables[0].Rows)
            {
                sb.Append("<tr style='font-weight:100'>");
                sb.Append("<th style='font-weight:100;padding:3px'>" + row["sAnalyte"].ToString() + "</th>");
                sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + "</th>");
                sb.Append("<th style='font-weight:100;padding:3px'>" + row["sUnits"].ToString() + "</th>");
                sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</th>");
                if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                {
                    sb.Append("<th style='font-weight:normal;padding:0px'>" + row["sMale"].ToString() + "</th>");
                }
                else
                {
                    sb.Append("<th style='font-weight:normal;padding:0px'>" + row["sFemale"].ToString() + "</th>");
                }
                sb.Append("</tr>");
            }
        }
        else
        {
            if (dsTestReport.Tables[0].Rows[0]["sSubAnalyte"].ToString() == "")
            {
                foreach (DataRow row in dsTestReport.Tables[0].Rows)
                {
                    sb.Append("<tr style='font-weight:100'>");
                    sb.Append("<th style='font-weight:100;padding:3px'>" + row["sAnalyte"].ToString() + "</th>");
                    sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + "</th>");
                    sb.Append("<th style='font-weight:100;padding:3px'>" + row["sUnits"].ToString() + "</th>");
                    sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</th>");
                    if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                    {
                        sb.Append("<th style='font-weight:normal;padding:0px'>" + row["smale"].ToString() + "</th>");
                    }
                    else
                    {
                        sb.Append("<th style='font-weight:normal;padding:0px'>" + row["sFemale"].ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                }
            }
            else
            {
                if (dsTestReport.Tables[0].Rows[0]["sTestId"].ToString() == "395" || dsTestReport.Tables[0].Rows[0]["sTestId"].ToString() == "414")
                {
                    int a = 1;
                    int b = 1;
                    foreach (DataRow row in dsTestReport.Tables[0].Rows)
                    {
                        if (a == Convert.ToInt32(row["RowNo"].ToString()))
                        {
                            sb.Append("<tr style='font-weight:100'>");
                            sb.Append("<th style='font-weight:180px;padding:3px'>" + row["sAnalyte"].ToString() + "</th>");
                            foreach (DataRow rows in dsTestReport.Tables[0].Rows)
                            {
                                b = Convert.ToInt32(rows["RowNo"].ToString());
                                if (a == b)
                                {
                                    sb.Append("<tr style='font-weight:100'>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + rows["sSubAnalyte"].ToString() + "</th>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(rows["sValue"].ToString()) + "</th>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + rows["sUnits"].ToString() + "</th>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(rows["sResult"].ToString()) + "</th>");
                                    if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                                    {
                                        sb.Append("<th style='font-weight:normal;padding:1px'>" + rows["smale"].ToString() + "</th>");
                                    }
                                    else
                                    {
                                        sb.Append("<th style='font-weight:normal;padding:1px'>" + rows["sFemale"].ToString() + "</th>");
                                    }
                                    sb.Append("</tr>");
                                }
                                else
                                {

                                }

                            }
                            a = a + 1;
                            //  b = b + 1;
                            sb.Append("</tr>");
                        }
                    }
                   
                }
                else if (dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString().ToLower() == "cbc")
                {
                    int a = 1;
                    int b = 1;
                    foreach (DataRow row in dsTestReport.Tables[0].Rows)
                    {
                        if (a == Convert.ToInt32(row["RowNo"].ToString()))
                        {
                            sb.Append("<tr style='font-weight:100'>");
                            sb.Append("<th style='font-weight:180px;padding:3px'>" + row["sAnalyte"].ToString() + "</th>");
                            foreach (DataRow rows in dsTestReport.Tables[0].Rows)
                            {
                                b = Convert.ToInt32(rows["RowNo"].ToString());
                                if (a == b)
                                {

                                    sb.Append("<tr style='font-weight:100'>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + rows["sSubAnalyte"].ToString() + "</th>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(rows["sValue"].ToString()) + "</th>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + rows["sUnits"].ToString() + "</th>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(rows["sResult"].ToString()) + "</th>");
                                    if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                                    {
                                        sb.Append("<th style='font-weight:normal;padding:1px'>" + rows["smale"].ToString() + "</th>");
                                    }
                                    else
                                    {
                                        sb.Append("<th style='font-weight:normal;padding:1px'>" + rows["sFemale"].ToString() + "</th>");
                                    }
                                    sb.Append("</tr>");
                                }
                                else
                                {

                                }

                            }
                            a = a + 1;
                            //  b = b + 1;
                            sb.Append("</tr>");
                        }
                    }
                }
                else if (dsTestReport.Tables[0].Rows[0]["sTestId"].ToString() == "391")
                {

                    int a = 2;
                    int b = 1;
                    foreach (DataRow row in dsTestReport.Tables[0].Rows)
                    {
                        if (a == Convert.ToInt32(row["rn"].ToString()))
                        {
                            sb.Append("<tr style='font-weight:100'>");
                            sb.Append("<th style='font-weight:180px;padding:3px'>" + row["sAnalyte"].ToString() + "</th>");
                            foreach (DataRow rows in dsTestReport.Tables[0].Rows)
                            {
                                b = Convert.ToInt32(rows["rn"].ToString());
                                if (a == b)
                                {
                                    sb.Append("<tr style='font-weight:100'>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + rows["sSubAnalyte"].ToString() + "</th>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(rows["sValue"].ToString()) + "</th>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + rows["sUnits"].ToString() + "</th>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(rows["sResult"].ToString()) + "</th>");
                                    if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                                    {
                                        sb.Append("<th style='font-weight:normal;padding:1px'>" + rows["smale"].ToString() + "</th>");
                                    }
                                    else
                                    {
                                        sb.Append("<th style='font-weight:normal;padding:1px'>" + rows["sFemale"].ToString() + "</th>");
                                    }
                                    sb.Append("</tr>");
                                }
                                else
                                {

                                }

                            }

                            if (a == 2)
                            {
                                a = 3;
                            }
                            else if (a == 3)
                            {
                                a = 1;
                            }
                            else
                            {
                                a = 0;
                            }

                            sb.Append("</tr>");
                        }
                    }
                }
                else
                {
                    int a = 1;
                    int b = 1;
                    foreach (DataRow row in dsTestReport.Tables[0].Rows)
                    {
                        if (a == Convert.ToInt32(row["rn"].ToString()))
                        {
                            sb.Append("<tr style='font-weight:100'>");
                            sb.Append("<th style='font-weight:180px;padding:3px'>" + row["sAnalyte"].ToString() + "</th>");
                            foreach (DataRow rows in dsTestReport.Tables[0].Rows)
                            {
                                b = Convert.ToInt32(rows["rn"].ToString());
                                if (a == b)
                                {
                                    sb.Append("<tr style='font-weight:100'>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + rows["sSubAnalyte"].ToString() + "</th>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(rows["sValue"].ToString()) + "</th>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + rows["sUnits"].ToString() + "</th>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(rows["sResult"].ToString()) + "</th>");
                                    if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                                    {
                                        sb.Append("<th style='font-weight:normal;padding:1px'>" + rows["smale"].ToString() + "</th>");
                                    }
                                    else
                                    {
                                        sb.Append("<th style='font-weight:normal;padding:1px'>" + rows["sFemale"].ToString() + "</th>");
                                    }
                                    sb.Append("</tr>");
                                }
                                else
                                {

                                }

                            }
                            a = a + 1;
                            //  b = b + 1;
                            sb.Append("</tr>");
                        }
                    }
                }
            }
        }
        sb.Append("</table>");
        string _method = dsTestReport.Tables[0].Rows[0]["sMethod"].ToString();
        string _Comment = dsTestReport.Tables[0].Rows[0]["sComment"].ToString();
        sb.Append("<table style='padding: 1px; width: 100%; border-top: 1px ;'>");
        if (_method != "")
        {
            sb.Append("<tr>");
            sb.Append("<th style='padding: 5px;'><h6>Method :- " + _method + "</h6> </th>");
            sb.Append("</tr>");
        }
        if (_Comment != "")
        {
            sb.Append("<tr>");
            sb.Append("<th style='padding: 1px;'><h6>Comment :- </h6> " + _Comment + " " + dsTestReport.Tables[0].Rows[0]["sNotes"].ToString() + "</th>");
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        sb.Append("<div class='sign' style='margin-top: 50px;clear: both;'>");
        sb.Append("<div class='left' style='float: left;'>");
        //sb.Append("<img src='https://visionarylifesciences.in/images/" + DMLT + "' alt='' style='margin: 0 auto 20px; display: block; height: 42px; width: 114px;'>");
       // sb.Append("<p>Shri.Swapnil Shinde </br>( DMLT, Lab Incharge )</br>7020357182</p>");

        //sb.Append("<p>7020357182</p>");
        sb.Append("</div>");
        sb.Append("<div class='left' style='float: right;'>");
        // sb.Append("<img src='https://visionarylifesciences.in/images/" + MD + "' alt='' style='margin: 0 auto 20px; display: block; height: 42px; width: 114px;'>");
        sb.Append("<p> &nbsp;&nbsp;&nbsp; Shri.Swapnil Shinde </br>( DMLT, Lab Incharge )</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7020357182</p>");

        //sb.Append("<p>7350444005</p>");

        sb.Append("</div>");
        sb.Append("</div>");
        sb.Append("</div>");

        _ReportDiv.InnerHtml = sb.ToString();
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
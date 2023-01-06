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
using System.Configuration;
using System.Data.SqlClient;

public partial class viewQrReport : System.Web.UI.Page
{
    DataSet dt_DigitalSign = new DataSet();
    CLSViewReportValuesinTemplate objTemplate = new CLSViewReportValuesinTemplate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ReportId"] != null)
        {
            LoadReport();
           
        }
        
    }
  
    protected void LoadReport()
    {
        string patientAge = "";
        //string bookLabId = Request.QueryString["bookId"].ToString();
        int bookLabTestId = Convert.ToInt32(Request.QueryString["ReportId"].ToString().Trim());
        DataSet dsTestReport = objTemplate.getTestReport_mView(bookLabTestId.ToString());
        string DateOfBirth = dsTestReport.Tables[0].Rows[0]["sBirthDate"].ToString();

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
       
        StringBuilder sb = new StringBuilder();

        sb.Append("<!DOCTYPE html>");
        sb.Append("<html>");
        sb.Append("<body class='_body' style='width: 100%; margin: 0; position: relative;'>");
        sb.Append("<div class='wrapper' style='position: relative;padding: 20px;'>");
        sb.Append("<table style='padding: 5px; width: 100%; border-top: 1px ;'>");
        sb.Append("<tr>");
        sb.Append("<th style='padding: 5px;'><h5> <img src='https://www.visionarylifescience.com/images/staticLab.jpg' alt='Logo' style='text-align:center; margin: 0 auto 20px;display: block; width:100px;'> </h5> </th>");
        sb.Append("<th style='padding: 5px;'><h5 style='font-size:30px;'>" + dsTestReport.Tables[0].Rows[0]["LabName"].ToString() + "  </h5> </th>");
        sb.Append("</tr>");
       
        
           // sb.Append("<h2 style='text-align: center; font-weight: 400;'>" +  + "</h2>");
       
        sb.Append("<table style='position: relative;width: 100%;  text-align: left; padding: 15px; margin-bottom: 10px;' class='table'>");
        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Patient Name :</td>");
        sb.Append("<td colspan='2' style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["sFullName"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Age :</td>");
        sb.Append("<td colspan='2' style='padding: 5px;'>" + patientAge  + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Test Date :</td>");
        sb.Append("<td colspan='2' style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["TestDate"].ToString() + "</td>");
       sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Lab Name :</td>");
         sb.Append("<td colspan='2' style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["LabName"].ToString() + "</td>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Referred By :</td>");
        sb.Append("<td colspan='2' style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["RefDoctorName"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
       
     
       
        
     
       
        sb.Append("</table>");
        sb.Append("</hr>");
        sb.Append("<h3 style='text-align: center;text-transform: uppercase; margin: 0; line-height: 1.5em;'></h3>");
        sb.Append("<h3 style='text-align: center;text-transform: uppercase; margin: 0; line-height: 1.5em;'>" + dsTestReport.Tables[0].Rows[0]["TestName"].ToString() +"</h3>");
        sb.Append("</br>");
        sb.Append("</hr>");
        sb.Append("<table style='padding: 5px; font-family:Verdana;font-weight:100;  text-align: left;' class='table' >");
        sb.Append("<tr >");
        sb.Append("<th style='width: 30%;'>Test</th>");
        sb.Append("<th style='width: 9%;'>Value(s)</th>");
        sb.Append("<th style='width: 9%;'>Units</th>");
        //sb.Append("<th style='width: 32%;'>Result</th>");
        sb.Append(" <th style='width:12%;'>Range</th>");
        sb.Append("</tr>");
        foreach (DataRow row in dsTestReport.Tables[0].Rows)
        {
            sb.Append("<tr style='font-weight:400'>");
            sb.Append("<th style='font-weight:400;padding:3px'>" + row["TestName"].ToString() + "</th>");
            sb.Append("<th style='font-weight:400;padding:3px'>" + CryptoHelper.Decrypt(row["Value"].ToString()) + "</th>");
            sb.Append("<th style='font-weight:400;padding:3px'>" + row["Unit"].ToString() + "</th>");
            string min = row["MinRange"].ToString();
            string Max = row["MaxRange"].ToString();
            sb.Append("<th style='font-weight:400;padding:3px'>" + min +"-" + Max + "</th>");
           
            sb.Append("</tr>");
        }
        
       
      
        sb.Append("</table>");
        sb.Append("</br>");
        sb.Append("<table style='padding: 5px; width: 100%; border-top: 1px ;'>");
        sb.Append("<tr>");
        sb.Append("<th style='padding: 5px;'><h5>Note :- " + dsTestReport.Tables[0].Rows[0]["Notes"].ToString() + "</h5> </th>");
        sb.Append("</tr>");
       
        sb.Append("</table>");
        sb.Append("<div class='sign' style='margin-top: 4em;clear: both;'>");
        sb.Append("<div>");
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
    protected void btnprintReport_Click(object sender, EventArgs e)
    {
        int bookLabTestId =  Convert.ToInt32(Request.QueryString["bookLabTestId"].ToString().Trim());
        Response.Redirect(@"printableReport.aspx?bookLabTestId=" + bookLabTestId);
    }

   
    protected void imgBtnPrint_Click(object sender, EventArgs e)
    {
        imgBtnPrint.Visible = false;
        btnprintReport.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "window.print();", true);
    }
}
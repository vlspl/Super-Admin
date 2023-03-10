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
        if (Request.QueryString["bookLabTestId"] != null && Request.QueryString["labId"] != null && Request.QueryString["patientID"] != null)
        {
           try
            {
                loaddt_Signatury();
                LoadReport();
                loadRem();
            }
            catch (Exception ex)
            {
                LogError.LoggerCatch(ex);
                Response.Redirect(@"error.aspx");
            }
        }
        
    }
    string LabId;
    protected void loaddt_Signatury()
    {
        if (Request.QueryString["labId"] != null)
            LabId = Request.QueryString["labId"].ToString();
       
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT DSId,SignHolder,Department,substring(SignImage,4,LEN(SignImage)) as SignImage,SignStatus, SLabId FROM DigitalSignature where sLabId='" + LabId + "'"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        DataList1.DataSource = ds;
                        DataList1.DataBind();
                    }
                }
            }
        }
    }
   
    protected void LoadReport()
    {
        string patientAge = "";
        //string bookLabId = Request.QueryString["bookId"].ToString();
        int bookLabTestId =  Convert.ToInt32(Request.QueryString["bookLabTestId"].ToString().Trim());
       DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId.ToString());
       DataSet dsDocNote = objTemplate.getTestReport_doctorNote(Request.QueryString["bookLabTestId"].ToString().Trim(), Request.QueryString["patientID"].ToString().Trim());
       
        string DateOfBirth = dsTestReport.Tables[0].Rows[0]["sbirthdate"].ToString();
      if (DateOfBirth.Contains('-'))
        {
            DateOfBirth = DateOfBirth.Replace('-', '/');
        }

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
        sb.Append("<div class='wrapper' style='position: relative;padding: 20px;'>");
        sb.Append("<table style='padding: 5px; width: 100%;'>");
        sb.Append("<tr>");
        if (labimage != "")
        {
            sb.Append(" <img src='http://visionarylifescience.com/images/" + labimage + "' alt='Logo' style='text-align:center; margin: 0 auto 20px;display: block; width:370px;'>");
        }
        else
        {
            sb.Append("<th style='padding: 5px;'><h5> <img src='https://www.visionarylifescience.com/images/staticLab.jpg' alt='Logo' style='text-align:center; margin: 0 auto 20px;display: block; width:170px;'> </h5> </th>");
            sb.Append("<th style='padding: 5px;'><h5 style='font-size:30px;'>" + dsTestReport.Tables[0].Rows[0]["sLabName"].ToString() + "  </h5> </th>");
        }
        
        sb.Append("</tr>");
        sb.Append("</table>");
        
      
        sb.Append("<table style='position: relative;width: 100%;  text-align: left; padding: 15px; margin-bottom: 10px;' class='table' >");
        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Patient Name :</td>");
        sb.Append("<td colspan='2' style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["sFullName"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Age/Sex :</td>");
        sb.Append("<td colspan='2' style='padding: 5px;'>" + patientAge + " / " + dsTestReport.Tables[0].Rows[0]["sGender"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Referred By :</td>");
        sb.Append("<td colspan='2' style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["sDoctor"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Reg. No. :</td>");
        sb.Append("<td colspan='2' style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["sBookLabId"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Report ID :</td>");
        sb.Append("<td colspan='2' style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["sBookLabTestId"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Booking Date :</td>");
        string date = dsTestReport.Tables[0].Rows[0]["sReportCreatedOn"].ToString();
        string bookingdate = Convert.ToDateTime(date).ToString("dd/MM/yyyy hh:mm:ss");
        sb.Append("<td colspan='2' style='padding: 5px;'>" + bookingdate + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Collection Date :</td>");
        sb.Append("<td colspan='2' style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["sTestDate"].ToString() + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Report Date :</td>");
        sb.Append("<td colspan='2' style='padding: 5px;'>" + dsTestReport.Tables[0].Rows[0]["sTestDate"].ToString() + "</td>");
        sb.Append("</tr>");
       
        sb.Append("</table>");
        sb.Append("</hr>");
        sb.Append("<h3 style='text-align: center;text-transform: uppercase; margin: 0; line-height: 1.5em;'></h3>");
        sb.Append("<h3 style='text-align: center;text-transform: uppercase; margin: 0; line-height: 1.5em;'>" + dsTestReport.Tables[0].Rows[0]["sTestName"].ToString() + "(" + dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() + ")</h3>");
        sb.Append("</br>");
        sb.Append("</hr>");
        sb.Append("<table style='padding: 5px; font-family:Verdana;font-weight:100;  text-align: left;' class='table' >");
        sb.Append("<tr >");
        sb.Append("<th style='width: 30%;'>Test</th>");
        sb.Append("<th style='width: 9%;'>Value(s)</th>");
        sb.Append("<th style='width: 9%;'>Units</th>");
       // sb.Append("<th style='width: 32%;'>Result</th>");
        sb.Append(" <th style='width:12%;'>Range</th>");
        sb.Append("</tr>");

        if (dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() == "COVID19")
        {
            foreach (DataRow row in dsTestReport.Tables[0].Rows)
            {
                sb.Append("<tr style='font-weight:400>");
                sb.Append("<th style='font-weight:400;padding:3px;'>" + row["sAnalyte"].ToString() + "</th>");
                sb.Append("<th style='font-weight:400;padding:3px'>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + "</th>");
                sb.Append("<th style='font-weight:400;padding:3px'>" + row["sUnits"].ToString() + "</th>");
              //  sb.Append("<th style='font-weight:400;padding:3px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</th>");
                if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                {
                    sb.Append("<th style='font-weight:400;padding:1px'>" + val_refval(row["sMale"].ToString()) + "</th>");
                }
                else
                {
                    sb.Append("<th style='font-weight:400;padding:1px'>" + val_refval(row["sFemale"].ToString()) + "</th>");
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
                    sb.Append("<tr style='font-weight:400'>");
                    sb.Append("<th style='font-weight:400;'>" + row["sAnalyte"].ToString() + "</th>");
                    sb.Append("<th style='font-weight:400;padding:3px; text-align:center;'>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + "</th>");
                    sb.Append("<th style='font-weight:400;padding:3px; text-align:center;'>" + row["sUnits"].ToString() + "</th>");
                  //  sb.Append("<th style='font-weight:400;padding:3px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</th>");
                    if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                    {
                        sb.Append("<th style='font-weight:400;padding:1px'>" + val_refval(row["smale"].ToString()) + "</th>");
                    }
                    else
                    {
                        sb.Append("<th style='font-weight:400;padding:1px'>" + val_refval(row["sFemale"].ToString()) + "</th>");
                    }
                    sb.Append("</tr>");
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
                        sb.Append("<tr style='font-weight:600'>");
                        sb.Append("<th style='font-weight:600;padding:3px'>" + row["sAnalyte"].ToString() + "</th>");
                        foreach (DataRow rows in dsTestReport.Tables[0].Rows)
                        {
                            b = Convert.ToInt32(rows["rn"].ToString());
                            if (a == b)
                            {
                                sb.Append("<tr style='font-weight:400'>");
                                sb.Append("<th style='font-weight:400; '>" +  rows["sSubAnalyte"].ToString() + "</th>");
                                sb.Append("<th style='font-weight:400;padding:3px; text-align:center;'>" + CryptoHelper.Decrypt(rows["sValue"].ToString()) + "</th>");
                                sb.Append("<th style='font-weight:400;padding:3px; text-align:center;'>" + row["sUnits"].ToString() + "</th>");
                            //    sb.Append("<th style='font-weight:400;padding:3px'>" + CryptoHelper.Decrypt(rows["sResult"].ToString()) + "</th>");
                                if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                                {
                                    sb.Append("<th style='font-weight:400;padding:1px; text-align:center;'>" + val_refval(rows["smale"].ToString()) + "</th>");
                                }
                                else
                                {
                                    sb.Append("<th style='font-weight:400;padding:1px; text-align:center;'>" + val_refval(rows["sFemale"].ToString()) + "</th>");
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
        sb.Append("</table>");
        sb.Append("</br>");
        sb.Append("<table style='padding: 5px; width: 100%; border-top: 1px ;'>");
        sb.Append("<tr>");
        sb.Append("<th style='padding: 5px;'><h5>Method :- " + dsTestReport.Tables[0].Rows[0]["sMethod"].ToString() + "</h5> </th>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<th style='padding: 5px;'><h5>Comment :- </h5> " + dsTestReport.Tables[0].Rows[0]["sComment"].ToString() + " " + dsTestReport.Tables[0].Rows[0]["sNotes"].ToString() + "</th>");
        // sb.Append("<th>Comment :"+ dsTestReport.Tables[0].Rows[0]["sComment"].ToString() + " " + dsTestReport.Tables[0].Rows[0]["sNotes"].ToString() + "</th>");
        sb.Append("</tr>");
        sb.Append("</table>");
        sb.Append("<table style='position: relative;width: 100%;  text-align: left; padding: 15px; margin-bottom: 10px;' class='table'>");
        sb.Append("<tr>");
        sb.Append("<td colspan='1' style='padding: 5px; font-weight:600;'>Doctor Note: </td>");
        sb.Append("</tr>");
        sb.Append("<tr >");

        sb.Append("<th style='width: 9%;'>Date</th>");
        sb.Append("<th style='width: 30%;'>Note</th>");
        sb.Append("</tr>");
        foreach (DataRow rowdoc_note in dsDocNote.Tables[0].Rows)
        {
            sb.Append("<tr style='font-weight:400'>");
            sb.Append("<th style='font-weight:400;padding:3px'>" + rowdoc_note["Createddate"].ToString() + "</th>");
            sb.Append("<th style='font-weight:400;padding:3px'>" + rowdoc_note["Note"].ToString() + "</th>");
            sb.Append("</tr>");
        }
        // sb.Append("<th style='padding: 5px;'><h5>Method :- " + dsTestReport.Tables[0].Rows[0]["sMethod"].ToString() + "</h5> </th>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        // sb.Append("<th style='padding: 5px;'><h5>Comment :- </h5> " + dsTestReport.Tables[0].Rows[0]["sComment"].ToString() + " " + dsTestReport.Tables[0].Rows[0]["sNotes"].ToString() + "</th>");
        // sb.Append("<th>Comment :"+ dsTestReport.Tables[0].Rows[0]["sComment"].ToString() + " " + dsTestReport.Tables[0].Rows[0]["sNotes"].ToString() + "</th>");
        sb.Append("</tr>");
        sb.Append("</table>");
        sb.Append("<div class='sign' style='margin-top: 4em;clear: both;'>");
        sb.Append("<div>");
         sb.Append("</div>");
     
        sb.Append("</div>");
        sb.Append("</div>");
        
        //sb.Append("<div class='address' style='text-align: right;clear: both; padding-top: 25px;margin-bottom:5px; margin-right:28px'>");
        
        //    sb.Append("<p>" + dsTestReport.Tables[0].Rows[0]["sLabAddress"].ToString() + "</p>");
        //    sb.Append("<p>" + dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString()) : "" + "</p>");

           
       

        _ReportDiv.InnerHtml = sb.ToString();
    }
	  public string val_refval(string val) // 10.00-20.00
    {
        if (val.Contains('-'))
        {
            string val1 = string.Empty;
            string val2 = string.Empty;
            string[] multipleArr = val.Split('-');
            val1 = multipleArr[0].ToString(); // 10.00
            val2 = multipleArr[1].ToString();//20.00

            return dot_val(val1) + '-' + dot_val(val2);
        }
        else
            return val;

    }

    public string dot_val(string val) // 10.00
    {
        if (val.Contains('.'))
        {
            string valsp1 = string.Empty;
            string valsp2 = string.Empty;
            string[] multipleArrspval = val.Split('.');
            valsp1 = multipleArrspval[0].ToString();
            valsp2 = multipleArrspval[1].ToString();
            if (double.Parse(valsp2) == 0)
                return valsp1;
            else
                return val;


        }
        else
            return val;

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

    void loadRem()
    {
        int bookLabTestId = Convert.ToInt32(Request.QueryString["bookLabTestId"].ToString().Trim());
        DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId.ToString());
        StringBuilder sb1 = new StringBuilder();

        sb1.Append("<div class='address' style='text-align: center;clear: both; padding-top: 25px;margin-bottom:5px; margin-right:28px'>");

        sb1.Append("<p>" + dsTestReport.Tables[0].Rows[0]["sLabAddress"].ToString() + "</p>");
      
        string cont = dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString();
        if (cont != "")
        {
            sb1.Append("<p>" + dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString()) : "" + "</p>");

        }

        _remReport.InnerHtml = sb1.ToString();
    }
    protected void imgBtnPrint_Click(object sender, EventArgs e)
    {
        imgBtnPrint.Visible = false;
        btnprintReport.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "window.print();", true);
    }
}
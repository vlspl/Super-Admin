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
using QRCoder;
using System.Drawing;

public partial class printableReport : System.Web.UI.Page
{
    DBClass db = new DBClass();
    DataSet dt_DigitalSign = new DataSet();
    CLSViewReportValuesinTemplate objTemplate = new CLSViewReportValuesinTemplate();
    DataTable dt_reportFormation = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        loaddt();
        loaddt_Signatury();
        LoadReport();
        loadRem();
        loadimg();

    }
    protected void loaddt()
    {
        string LabId = Request.Cookies["LabId"].Value.ToString();
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT trfID,sLabId,sectionName,status, Details FROM testReportFormation where sLabId='" + LabId + "'"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        dt_reportFormation = dt;
                    }
                }
            }
        }
    }

    void loadimg()
    {
        int bookLabTestId = Convert.ToInt32(Request.QueryString["bookLabTestId"].ToString().Trim());
        DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId.ToString());
        string labimage = dsTestReport.Tables[0].Rows[0]["slablogo"].ToString();
        StringBuilder sb = new StringBuilder();
        DataRow[] drs = dt_reportFormation.Select("sectionName = 'HeaderLogo' and status='1'");
        if (drs.Length > 0)
        {
            sb.Append(" <img src='http://visionarylifescience.com/images/" + labimage + "' alt='Logo' style='text-align:center; margin: 0 auto 20px;display: block; height:200px;'>");

        }
        else
        {
            sb.Append("<p style='height:200px;'></p>");
        }
        headerdiv.InnerHtml = sb.ToString();
    }
    protected void QRCoder(int bookLabTestId, string LabId)
    {
        string code = "https://visionarylifescience.com/viewQrReport.aspx?bookLabTestId=" + bookLabTestId + "&labId=" + LabId;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 100;
        imgBarCode.Width = 100;

        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
            PlaceHolder1.Controls.Add(imgBarCode);


        }
    }
    protected void loaddt_Signatury()
    {
        string LabId = Request.Cookies["LabId"].Value.ToString();
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
        string result = "";
        string range = "";
        //string bookLabId = Request.QueryString["bookId"].ToString();
        int bookLabTestId = Convert.ToInt32(Request.QueryString["bookLabTestId"].ToString().Trim());
        DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId.ToString());
        string DateOfBirth = dsTestReport.Tables[0].Rows[0]["sbirthdate"].ToString();
        string LabId = Request.Cookies["LabId"].Value.ToString();
        QRCoder(bookLabTestId, LabId);

        if (DateOfBirth.Contains('-'))
        {
            DateOfBirth = DateOfBirth.Replace('-', '/');
        }

        DateTime dtDob = DateTime.ParseExact(DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //  DateTime dtDob = Convert.ToDateTime(DateOfBirth);
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

        string pdffilename = DateTime.UtcNow.ToString("yyyymmddhhmmssffff");
        string DMLT = dsTestReport.Tables[0].Rows[0]["sColG"].ToString();
        string MD = dsTestReport.Tables[0].Rows[0]["sColH"].ToString();
        string malevalue = dsTestReport.Tables[0].Rows[0]["sMale"].ToString();
        if (malevalue == "NA") malevalue = "--"; else malevalue = malevalue.ToString();
        string femalevalue = dsTestReport.Tables[0].Rows[0]["sFemale"].ToString();
        if (femalevalue == "NA") femalevalue = "--"; else femalevalue = femalevalue.ToString();

        if (dsTestReport.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dsTestReport.Tables[0].Rows)
            {
                lblpatientName.Text = row["sFullName"].ToString();
                lblsex.Text = patientAge + "/" + row["sGender"].ToString();
                lblrefby.Text = row["sDoctor"].ToString();
                lblcollected.Text = row["sTestDate"].ToString();
                string date = dsTestReport.Tables[0].Rows[0]["sReportCreatedOn"].ToString();
                string bookingdate = Convert.ToDateTime(date).ToString("dd/MM/yyyy hh:mm:ss");
                lblbooking.Text = bookingdate.ToString();
                lblreported.Text = row["sTestDate"].ToString();
                lblregno.Text = row["sBookLabId"].ToString();
                lblreportId.Text = row["sBookLabTestId"].ToString();
            }
        }
        StringBuilder sb = new StringBuilder();


        if (LabId != "1149")
        {


            //  sb.Append("<hr />");
            sb.Append("<h3 style='text-align: center;text-transform: uppercase; margin: 0; line-height: 1.5em;'></h3>");
            // sb.Append("<hr />");
            sb.Append("<h3 style='text-align: center;text-transform: uppercase; margin: 0; line-height: 1.5em;'>" + dsTestReport.Tables[0].Rows[0]["sTestName"].ToString() + "(" + dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() + ")</h3>");
            sb.Append("</br>");
            sb.Append("</hr>");
            sb.Append("<table style='padding: 5px; margin-left:25px; font-family:Verdana;font-weight:100; width: 100%; border-top: 1px solid; border-bottom: 1px solid; text-align: left;'>");
            sb.Append("<tr>");
            string valresult = CryptoHelper.Decrypt(dsTestReport.Tables[0].Rows[0]["sResult"].ToString());
            if (valresult == "NA" || valresult == "A +Ve" || valresult == "B +Ve" || valresult == "O +Ve" || valresult == "AB +Ve" || valresult == "AB -Ve" || valresult == "B -Ve" || valresult == "A -Ve" || valresult == "O -Ve" || valresult == "Positive" || valresult == "Negative" || valresult == "Absent" || valresult == "Present" || valresult == "Reactive" || valresult == "NonReactive" || valresult == "Abnormal" || valresult == "Vaccinated: positive" || valresult == "Unvaccinated: negative")
            {
                sb.Append("<th style='width: 30%;'>Test Description</th>");
                sb.Append("<th style='width: 9%;'>Result(s)</th>");
            }
            else
            {
                sb.Append("<th style='width: 25%;'>Test Description</th>");
                sb.Append("<th style='width: 9%;'>Value(s)</th>");
                sb.Append("<th style='width: 9%;'>Units</th>");
                //sb.Append("<th style='width: 32%;'>Result</th>");
                sb.Append(" <th style='width:12%;'>Reference Range</th>");
            }


            sb.Append("</tr>");

            if (dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() == "COVID19")
            {
                foreach (DataRow row in dsTestReport.Tables[0].Rows)
                {
                    sb.Append("<tr style='font-weight:100'>");
                    sb.Append("<th style='font-weight:100;padding:3px'>" + row["sAnalyte"].ToString() + "</th>");
                    sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + "</th>");
                    sb.Append("<th style='font-weight:100;padding:3px'>" + row["sUnits"].ToString() + "</th>");
                    //sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</th>");
                    if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                    {

                        sb.Append("<th style='font-weight:normal;padding:1px'>" + val_refval(row["smale"].ToString()) + "</th>");
                    }
                    else
                    {

                        sb.Append("<th style='font-weight:normal;padding:1px'>" + val_refval(row["sFemale"].ToString()) + "</th>");
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
                        string valresult1 = string.Empty;
                        sb.Append("<tr style='font-weight:100'>");
                        sb.Append("<th style='font-weight:100;padding:3px'>" + row["sAnalyte"].ToString() + "</th>");

                        valresult1 = CryptoHelper.Decrypt(row["sResult"].ToString());
                        if (valresult1 == "NA" || valresult1 == "A +Ve" || valresult1 == "B +Ve" || valresult1 == "O +Ve" || valresult1 == "AB +Ve" || valresult1 == "AB -Ve" || valresult1 == "B -Ve" || valresult1 == "A -Ve" || valresult1 == "O -Ve" || valresult1 == "Positive" || valresult1 == "Negative" || valresult1 == "Absent" || valresult1 == "Present" || valresult1 == "Reactive" || valresult1 == "NonReactive" || valresult1 == "Abnormal" || valresult1 == "Vaccinated: positive" || valresult1 == "Unvaccinated: negative")
                        {
                            sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</th>");
                        }
                        else
                        {
                            sb.Append("<th style='font-weight:600;padding:3px'>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + "</th>");
                            sb.Append("<th style='font-weight:100;padding:3px'>" + row["sUnits"].ToString() + "</th>");
                            //sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</th>");
                            if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                            {

                                sb.Append("<th style='font-weight:normal;padding:1px'>" + val_refval(row["smale"].ToString()) + "</th>");
                            }
                            else
                            {

                                sb.Append("<th style='font-weight:normal;padding:1px'>" + val_refval(row["sFemale"].ToString()) + "</th>");
                            }
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
                        int val = Convert.ToInt32(row["rn"].ToString());
                        if (a == val)
                        {

                            sb.Append("<tr style='font-weight:100'>");
                            sb.Append("<th style='font-weight:180px;padding:3px'>" + row["sAnalyte"].ToString() + "</th>");
                            foreach (DataRow rows in dsTestReport.Tables[0].Rows)
                            {
                                b = Convert.ToInt32(rows["rn"].ToString());
                                if (a == b)
                                {
                                    string valresult2 = string.Empty;
                                    sb.Append("<tr style='font-weight:100'>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + rows["sSubAnalyte"].ToString() + "</th>");


                                    valresult2 = CryptoHelper.Decrypt(rows["sResult"].ToString());
                                    if (valresult2 == "NA" || valresult2 == "A +Ve" || valresult2 == "B +Ve" || valresult2 == "O +Ve" || valresult2 == "AB +Ve" || valresult2 == "AB -Ve" || valresult2 == "B -Ve" || valresult2 == "A -Ve" || valresult2 == "O -Ve" || valresult2 == "Positive" || valresult2 == "Negative" || valresult2 == "Absent" || valresult2 == "Present" || valresult2 == "Reactive" || valresult2 == "NonReactive" || valresult2 == "Abnormal" || valresult2 == "Vaccinated: positive" || valresult2 == "Unvaccinated: negative")
                                    {
                                        sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(rows["sResult"].ToString()) + "</th>");
                                    }
                                    else
                                    {
                                        sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(rows["sValue"].ToString()) + "</th>");
                                        sb.Append("<th style='font-weight:100;padding:3px'>" + row["sUnits"].ToString() + "</th>");
                                        //sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</th>");
                                        if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                                        {

                                            sb.Append("<th style='font-weight:normal;padding:1px'>" + val_refval(rows["smale"].ToString()) + "</th>");
                                        }
                                        else
                                        {

                                            sb.Append("<th style='font-weight:normal;padding:1px'>" + val_refval(rows["sFemale"].ToString()) + "</th>");
                                        }
                                    }
                                    sb.Append("</tr>");
                                }
                                else
                                {

                                }

                            }// 2nd for loop end
                            a = a + 1;
                            // b = b + 1;
                            sb.Append("</tr>");
                        }
                    } //1st for loop end
                }
            }
            sb.Append("</table>");
            sb.Append("</br>");
            sb.Append("<table style='padding: 5px; margin-left:25px; width: 100%; border-top: 1px ;'>");
            sb.Append("<tr>");
            sb.Append("<th style='padding: 5px;'><h5>Method :- " + dsTestReport.Tables[0].Rows[0]["sMethod"].ToString() + "</h5> </th>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th style='padding: 5px;'><h5>Comment :- </h5> " + dsTestReport.Tables[0].Rows[0]["sComment"].ToString() + " " + dsTestReport.Tables[0].Rows[0]["sNotes"].ToString() + "</th>");
            // sb.Append("<th>Comment :"+ dsTestReport.Tables[0].Rows[0]["sComment"].ToString() + " " + dsTestReport.Tables[0].Rows[0]["sNotes"].ToString() + "</th>");
            sb.Append("</tr>");
            sb.Append("</table>");
            _ReportDiv.InnerHtml = sb.ToString();
        }
        else
        {
            string testCode = dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString();
            if (testCode != "CBC")
            {
                //  sb.Append("<hr />");
                sb.Append("<h3 style='text-align: center;text-transform: uppercase; margin: 0; line-height: 1.5em;'></h3>");
                // sb.Append("<hr />");
                sb.Append("<h3 style='text-align: center;text-transform: uppercase; margin: 0; line-height: 1.5em;'>" + dsTestReport.Tables[0].Rows[0]["sTestName"].ToString() + "(" + dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() + ")</h3>");
                sb.Append("</br>");
                sb.Append("</hr>");
                sb.Append("<table style='padding: 5px; margin-left:25px; font-family:Verdana;font-weight:100; width: 100%; border-top: 1px solid; border-bottom: 1px solid; text-align: left;'>");
                sb.Append("<tr>");
                string valresult = CryptoHelper.Decrypt(dsTestReport.Tables[0].Rows[0]["sResult"].ToString());
                if (valresult == "NA" || valresult == "A +Ve" || valresult == "B +Ve" || valresult == "O +Ve" || valresult == "AB +Ve" || valresult == "AB -Ve" || valresult == "B -Ve" || valresult == "A -Ve" || valresult == "O -Ve" || valresult == "Positive" || valresult == "Negative" || valresult == "Absent" || valresult == "Present" || valresult == "Reactive" || valresult == "NonReactive" || valresult == "Abnormal" || valresult == "Vaccinated: positive" || valresult == "Unvaccinated: negative")
                {
                    sb.Append("<th style='width: 30%;'>Test Description</th>");
                    sb.Append("<th style='width: 9%;'>Result(s)</th>");
                }
                else
                {
                    sb.Append("<th style='width: 25%;'>Test Description</th>");
                    sb.Append("<th style='width: 9%;'>Value(s)</th>");
                    sb.Append("<th style='width: 9%;'>Units</th>");
                    //sb.Append("<th style='width: 32%;'>Result</th>");
                    sb.Append(" <th style='width:12%;'>Reference Range</th>");
                }


                sb.Append("</tr>");



                if (dsTestReport.Tables[0].Rows[0]["sSubAnalyte"].ToString() == "")
                {
                    foreach (DataRow row in dsTestReport.Tables[0].Rows)
                    {
                        string valresult1 = string.Empty;
                        sb.Append("<tr style='font-weight:100'>");
                        sb.Append("<th style='font-weight:100;padding:3px'>" + row["sAnalyte"].ToString() + "</th>");

                        valresult1 = CryptoHelper.Decrypt(row["sResult"].ToString());
                        if (valresult1 == "NA" || valresult1 == "A +Ve" || valresult1 == "B +Ve" || valresult1 == "O +Ve" || valresult1 == "AB +Ve" || valresult1 == "AB -Ve" || valresult1 == "B -Ve" || valresult1 == "A -Ve" || valresult1 == "O -Ve" || valresult1 == "Positive" || valresult1 == "Negative" || valresult1 == "Absent" || valresult1 == "Present" || valresult1 == "Reactive" || valresult1 == "NonReactive" || valresult1 == "Abnormal" || valresult1 == "Vaccinated: positive" || valresult1 == "Unvaccinated: negative")
                        {
                            sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</th>");
                        }
                        else
                        {
                            sb.Append("<th style='font-weight:600;padding:3px'>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + "</th>");
                            sb.Append("<th style='font-weight:100;padding:3px'>" + row["sUnits"].ToString() + "</th>");
                            //sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</th>");
                            if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                            {

                                sb.Append("<th style='font-weight:normal;padding:1px'>" + val_refval(row["smale"].ToString()) + "</th>");
                            }
                            else
                            {

                                sb.Append("<th style='font-weight:normal;padding:1px'>" + val_refval(row["sFemale"].ToString()) + "</th>");
                            }
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
                        int val = Convert.ToInt32(row["rn"].ToString());
                        if (a == val)
                        {

                            sb.Append("<tr style='font-weight:100'>");
                            sb.Append("<th style='font-weight:180px;padding:3px'>" + row["sAnalyte"].ToString() + "</th>");
                            foreach (DataRow rows in dsTestReport.Tables[0].Rows)
                            {
                                b = Convert.ToInt32(rows["rn"].ToString());
                                if (a == b)
                                {
                                    string valresult2 = string.Empty;
                                    sb.Append("<tr style='font-weight:100'>");
                                    sb.Append("<th style='font-weight:100;padding:3px'>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + rows["sSubAnalyte"].ToString() + "</th>");


                                    valresult2 = CryptoHelper.Decrypt(rows["sResult"].ToString());
                                    if (valresult2 == "NA" || valresult2 == "A +Ve" || valresult2 == "B +Ve" || valresult2 == "O +Ve" || valresult2 == "AB +Ve" || valresult2 == "AB -Ve" || valresult2 == "B -Ve" || valresult2 == "A -Ve" || valresult2 == "O -Ve" || valresult2 == "Positive" || valresult2 == "Negative" || valresult2 == "Absent" || valresult2 == "Present" || valresult2 == "Reactive" || valresult2 == "NonReactive" || valresult2 == "Abnormal" || valresult2 == "Vaccinated: positive" || valresult2 == "Unvaccinated: negative")
                                    {
                                        sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(rows["sResult"].ToString()) + "</th>");
                                    }
                                    else
                                    {
                                        sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(rows["sValue"].ToString()) + "</th>");
                                        sb.Append("<th style='font-weight:100;padding:3px'>" + row["sUnits"].ToString() + "</th>");
                                        //sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</th>");
                                        if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                                        {

                                            sb.Append("<th style='font-weight:normal;padding:1px'>" + val_refval(rows["smale"].ToString()) + "</th>");
                                        }
                                        else
                                        {

                                            sb.Append("<th style='font-weight:normal;padding:1px'>" + val_refval(rows["sFemale"].ToString()) + "</th>");
                                        }
                                    }
                                    sb.Append("</tr>");
                                }
                                else
                                {

                                }

                            }// 2nd for loop end
                            a = a + 1;
                            // b = b + 1;
                            sb.Append("</tr>");
                        }
                    } //1st for loop end
                }


                sb.Append("</table>");
                sb.Append("</br>");
                sb.Append("<table style='padding: 5px; margin-left:25px; width: 100%; border-top: 1px ;'>");
                sb.Append("<tr>");
                sb.Append("<th style='padding: 5px;'><h5>Method :- " + dsTestReport.Tables[0].Rows[0]["sMethod"].ToString() + "</h5> </th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th style='padding: 5px;'><h5>Comment :- </h5> " + dsTestReport.Tables[0].Rows[0]["sComment"].ToString() + " " + dsTestReport.Tables[0].Rows[0]["sNotes"].ToString() + "</th>");
                // sb.Append("<th>Comment :"+ dsTestReport.Tables[0].Rows[0]["sComment"].ToString() + " " + dsTestReport.Tables[0].Rows[0]["sNotes"].ToString() + "</th>");
                sb.Append("</tr>");
                sb.Append("</table>");

                _ReportDiv.InnerHtml = sb.ToString();
            }
            else
            {
                //  sb.Append("<hr />");
                sb.Append("<h3 style='text-align: center;text-transform: uppercase; margin: 0; line-height: 1.5em;'></h3>");
                // sb.Append("<hr />");
                sb.Append("<h3 style='text-align: center;text-transform: uppercase; margin: 0; line-height: 1.5em;'>" + dsTestReport.Tables[0].Rows[0]["sTestName"].ToString() + "(" + dsTestReport.Tables[0].Rows[0]["sTestCode"].ToString() + ")</h3>");
                sb.Append("</br>");
                sb.Append("</hr>");
                sb.Append("<table style='padding: 5px; margin-left:25px; font-family:Verdana;font-weight:100; width: 100%; border-top: 1px solid; border-bottom: 1px solid; text-align: left;'>");
                sb.Append("<tr>");
                string valresult = CryptoHelper.Decrypt(dsTestReport.Tables[0].Rows[0]["sResult"].ToString());
                if (valresult == "NA" || valresult == "A +Ve" || valresult == "B +Ve" || valresult == "O +Ve" || valresult == "AB +Ve" || valresult == "AB -Ve" || valresult == "B -Ve" || valresult == "A -Ve" || valresult == "O -Ve" || valresult == "Positive" || valresult == "Negative" || valresult == "Absent" || valresult == "Present" || valresult == "Reactive" || valresult == "NonReactive" || valresult == "Abnormal" || valresult == "Vaccinated: positive" || valresult == "Unvaccinated: negative")
                {
                    sb.Append("<th style='width: 30%;'>Test Description</th>");
                    sb.Append("<th style='width: 9%;'>Result(s)</th>");
                }
                else
                {
                    sb.Append("<th style='width: 25%;'>Test Description</th>");
                    sb.Append("<th style='width: 9%;'>Value(s)</th>");
                    sb.Append("<th style='width: 9%;'>Units</th>");
                    //sb.Append("<th style='width: 32%;'>Result</th>");
                    sb.Append(" <th style='width:12%;'>Reference Range</th>");
                }


                sb.Append("</tr>");

                var distinctRows = (from DataRow dRow in dsTestReport.Tables[0].Rows
                                    select dRow["sAnalyte"]).Distinct();

                string a_val = string.Empty;
                string b_val = string.Empty;

                foreach (string str in distinctRows)
                {
                    a_val = str;
                    sb.Append("<tr style='font-weight:100'>");
                    sb.Append("<th style='font-weight:180px;padding:3px'>" + str + "</th>");


                    foreach (DataRow row in dsTestReport.Tables[0].Rows)
                    {
                        b_val = row["sAnalyte"].ToString();


                        if (a_val == b_val)
                        {
                            string valresult2 = string.Empty;
                            sb.Append("<tr style='font-weight:100'>");
                            sb.Append("<th style='font-weight:100;padding:3px'>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + row["sSubAnalyte"].ToString() + "</th>");


                            valresult2 = CryptoHelper.Decrypt(row["sResult"].ToString());
                            if (valresult2 == "NA" || valresult2 == "A +Ve" || valresult2 == "B +Ve" || valresult2 == "O +Ve" || valresult2 == "AB +Ve" || valresult2 == "AB -Ve" || valresult2 == "B -Ve" || valresult2 == "A -Ve" || valresult2 == "O -Ve" || valresult2 == "Positive" || valresult2 == "Negative" || valresult2 == "Absent" || valresult2 == "Present" || valresult2 == "Reactive" || valresult2 == "NonReactive" || valresult2 == "Abnormal" || valresult2 == "Vaccinated: positive" || valresult2 == "Unvaccinated: negative")
                            {
                                sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</th>");
                            }
                            else
                            {
                                sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sValue"].ToString()) + "</th>");
                                sb.Append("<th style='font-weight:100;padding:3px'>" + row["sUnits"].ToString() + "</th>");
                                //sb.Append("<th style='font-weight:100;padding:3px'>" + CryptoHelper.Decrypt(row["sResult"].ToString()) + "</th>");
                                if (dsTestReport.Tables[0].Rows[0]["sGender"].ToString().ToLower() == "male")
                                {

                                    sb.Append("<th style='font-weight:normal;padding:1px'>" + val_refval(row["smale"].ToString()) + "</th>");
                                }
                                else
                                {

                                    sb.Append("<th style='font-weight:normal;padding:1px'>" + val_refval(row["sFemale"].ToString()) + "</th>");
                                }
                            }
                            sb.Append("</tr>");

                        }



                    } // inner sub anyl end loop

                    sb.Append("</tr>");
                } // distnit foor loop end 


                sb.Append("</table>");
                sb.Append("</br>");
                sb.Append("<table style='padding: 5px; margin-left:25px; width: 100%; border-top: 1px ;'>");
                sb.Append("<tr>");
                sb.Append("<th style='padding: 5px;'><h5>Method :- " + dsTestReport.Tables[0].Rows[0]["sMethod"].ToString() + "</h5> </th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th style='padding: 5px;'><h5>Comment :- </h5> " + dsTestReport.Tables[0].Rows[0]["sComment"].ToString() + " " + dsTestReport.Tables[0].Rows[0]["sNotes"].ToString() + "</th>");
                // sb.Append("<th>Comment :"+ dsTestReport.Tables[0].Rows[0]["sComment"].ToString() + " " + dsTestReport.Tables[0].Rows[0]["sNotes"].ToString() + "</th>");
                sb.Append("</tr>");
                sb.Append("</table>");

                _ReportDiv.InnerHtml = sb.ToString();
            }
        }
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
        int bookLabTestId = Convert.ToInt32(Request.QueryString["bookLabTestId"].ToString().Trim());
        Response.Redirect(@"printableReport.aspx?bookLabTestId=" + bookLabTestId);
    }

    void loadRem()
    {
        loaddt();
        int bookLabTestId = Convert.ToInt32(Request.QueryString["bookLabTestId"].ToString().Trim());
        DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId.ToString());
        StringBuilder sb1 = new StringBuilder();
        DataRow[] drs = dt_reportFormation.Select("sectionName ='footerAddress' and status='1'");
        if (drs.Length > 0)
        {
            sb1.Append("<div class='address' style='text-align: center;clear: both; padding-top: 25px;margin-bottom:5px; margin-right:28px'>");
            sb1.Append("<p>" + dsTestReport.Tables[0].Rows[0]["sLabAddress"].ToString() + "</p>");
            sb1.Append("<p>" + dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString()) : "" + "</p>");
        }


        _remReport.InnerHtml = sb1.ToString();
    }
    protected void imgBtnPrint_Click1(object sender, EventArgs e)
    {
        imgBtnPrint.Visible = false;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "window.print();", true);
    }
	
}
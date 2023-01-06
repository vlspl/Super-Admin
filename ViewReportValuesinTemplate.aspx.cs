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

public partial class ViewReportValuesinTemplate : System.Web.UI.Page
{
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
        string TestId = Request.QueryString["testid"].ToString();
        string tabTestValueResult = "";

        DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId);

        DataSet dstemplatebody = objTemplateBuilder.checkTemplateBody(TestId);
        DataSet dstemplatehead = objTemplateBuilder.checkTemplateHead(TestId);

        if (dsTestReport != null)
        {
            if (dsTestReport.Tables[0].Rows.Count > 0)
            {
                spanlabname.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabName"].ToString();
                labcontactnumber.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabContact"].ToString();
                labaddress.InnerHtml = dsTestReport.Tables[0].Rows[0]["sLabAddress"].ToString();
                spanpatname.InnerHtml = dsTestReport.Tables[0].Rows[0]["sPatient"].ToString();
                spanpatgender.InnerHtml = dsTestReport.Tables[0].Rows[0]["sGender"].ToString();

                if (dstemplatebody.Tables[0].Rows.Count > 0)
                {
                    string templatebody = Convert.ToString(dstemplatebody.Tables[0].Rows[0]["stemplatestatus"]);
                    if (templatebody == "Active") AddPatientbtn.Style["Display"] = "none";
                }

                if (dstemplatehead.Tables[0].Rows.Count > 0)
                {
                    string templatehead = Convert.ToString(dstemplatehead.Tables[0].Rows[0]["stemplatestatus"]);

                    string templateheading = Convert.ToString(dstemplatehead.Tables[0].Rows[0]["sheadtitle"]);
                    string templatesubheading = Convert.ToString(dstemplatehead.Tables[0].Rows[0]["ssubtitle"]);

                    if (templatehead == "ActiveHead") addHEadingbtn.Style["Display"] = "none";
                    if (templateheading != "") tempheading.InnerHtml = "<h1>" + templateheading.ToString() + "</h1>";
                    if (templatesubheading != "") tempsubheading.InnerHtml = "<h4>" + templatesubheading.ToString() + "</h4>";
                    //if (templatenotes == "Yes") Notesdiv.InnerHtml = "<p>Notes: <span>" + templatenotes.ToString() + "</span></p>";
                    //if (templatecomments == "Yes") CommentDiv.InnerHtml = "<p>Comment: <span>" + templatecomments.ToString() + "</span></p>";
                }

        

                tabTestValueResult += "<tr>" +                   
                                            "<th scope='col'>Analyte</th>" +
                                            "<th scope='col'>Subanalyte</th>" +                                           
                                            "<th scope='col'>Value</th>" +
                                            "<th scope='col'>Result</th>" +
                                    "</tr>";

                foreach (DataRow row in dsTestReport.Tables[0].Rows)
                {
                    
                    tabTestValueResult += "<tr>" +
                        
                                              "<td scope='col'>" + row["sAnalyte"].ToString() + "</td>" +
                                              "<td scope='col'>" + row["sSubAnalyte"].ToString() + "</td>" +                                             
                                              "<td scope='col'>" + row["sValue"].ToString() + "</td>" +
                                              "<td scope='col'>" + row["sResult"].ToString() + "</td>" +
                                      "<tr>";
                   
                }

                if (dstemplatehead.Tables[0].Rows.Count > 0)
                {
                    string notes = Convert.ToString(dstemplatehead.Tables[0].Rows[0]["sNotes"]);
                    string comments = Convert.ToString(dstemplatehead.Tables[0].Rows[0]["sComments"]);

                    string templatenotes = Convert.ToString(dsTestReport.Tables[0].Rows[0]["sNotes"]);
                    string templatecomments = Convert.ToString(dsTestReport.Tables[0].Rows[0]["sComment"]);

                    if (notes == "Yes") Notesdiv.InnerHtml = "<p>Notes: <span>" + templatenotes.ToString() + "</span></p>";
                    if (comments == "Yes") CommentDiv.InnerHtml = "<p>Comment: <span>" + templatecomments.ToString() + "</span></p>";
                }

            }
        }

        tbodyTemplateBuilder.InnerHtml = tabTestValueResult;       
    }





    protected void btnsendMail(object sender, EventArgs e)
    {
        SendPDFEmail();

    }



    private void SendPDFEmail()
    {
        string bookLabId = Request.QueryString["bookId"].ToString();
        string bookLabTestId = Request.QueryString["bookLabTestId"].ToString();
        string TestId = Request.QueryString["testid"].ToString();
        string tabTestValueResult = "";

        DataSet dsTestReport = objTemplate.getTestReport(bookLabTestId);

        DataSet dstemplatebody = objTemplateBuilder.checkTemplateBody(TestId);
        DataSet dstemplatehead = objTemplateBuilder.checkTemplateHead(TestId);

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                string companyName = dsTestReport.Tables[0].Rows[0]["sLabName"].ToString();
                int orderNo = 2303;
                StringBuilder sb = new StringBuilder();
                sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Order Sheet "+ dsTestReport.Tables[0].Rows[0]["sLabName"].ToString()+"</b></td></tr>");
                sb.Append("<tr><td colspan = '2'></td></tr>");
                sb.Append("<tr><td><b>Order No:</b>");
                sb.Append(orderNo);
                sb.Append("</td><td><b>Date: </b>");
                sb.Append(DateTime.Now);
                sb.Append(" </td></tr>");
                sb.Append("<tr><td colspan = '2'><b>Company Name :</b> ");
                sb.Append(companyName);
                sb.Append("</td></tr>");
                sb.Append("</table>");
                sb.Append("<br />");
                sb.Append("<table border = '1'>");
                sb.Append("<tr>");
                //foreach (DataRow row in dsTestReport.Tables[0].Rows)
                //{
                    sb.Append("<th style = 'background-color: #D20B0C;color:#ffffff'>");
                    //sb.Append( row["sAnalyte"].ToString() );

                    sb.Append("<th scope='col'>Analyte</th>");
                     sb.Append("<th scope='col'>Subanalyte</th>" );
                     sb.Append("<th scope='col'>Value</th>" );
                     sb.Append("<th scope='col'>Result</th>" );

                    sb.Append("</th>");
                //}
                sb.Append("</tr>");
                foreach (DataRow row in dsTestReport.Tables[0].Rows)
                {
                    sb.Append("<tr>");
                    //foreach (DataColumn column in dt.Columns)
                    //{
                        sb.Append("<td>");
                        sb.Append("1");
                        sb.Append("</td>");

                        sb.Append("<td>");
                        sb.Append(row["sAnalyte"].ToString());
                        sb.Append("</td>");

                        sb.Append("<td>");
                        sb.Append(row["sSubAnalyte"].ToString());
                        sb.Append("</td>");

                        sb.Append("<td>");
                        sb.Append(row["sValue"].ToString());
                        sb.Append("</td>");

                        sb.Append("<td>");
                        sb.Append(row["sResult"].ToString());
                        sb.Append("</td>");
                    //}
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                StringReader sr = new StringReader(sb.ToString());

                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();

                    MailMessage mm = new MailMessage("irealities.qa@gmail.com", "ugesh8@gmail.com");
                    mm.Subject = "Test Report";
                    mm.Body = "iTextSharp PDF Attachment";
                    mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "Test_Report.pdf"));
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential();
                    NetworkCred.UserName = "irealities.qa@gmail.com";
                    NetworkCred.Password = "Qatest2707@";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
            }
        }
    }




}
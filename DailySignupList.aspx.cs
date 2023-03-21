using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Net;
public partial class SuperAdmin_DailySignupList : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                grdviewSignUpDaily.DataSource = GetDailySignUpDetails();
                grdviewSignUpDaily.DataBind();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    protected void grdviewSignUpDaily_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdviewSignUpDaily.PageIndex = e.NewPageIndex;
        grdviewSignUpDaily.DataSource = GetDailySignUpDetails();
        grdviewSignUpDaily.DataBind();
    }
    public DataTable GetDailySignUpDetails()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetDailySignUpdetails");
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("EmailId", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    string Mobile = (dt.Rows[i]["sMobile"].ToString() != "") ? CryptoHelper.Decrypt(dt.Rows[i]["sMobile"].ToString()) : "";
                    string EmailId = (dt.Rows[i]["sEmailId"].ToString() != "") ? CryptoHelper.Decrypt(dt.Rows[i]["sEmailId"].ToString()) : "";
                    row["Mobile"] = Mobile;
                    row["EmailId"] = EmailId;
                }
            }
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }

    public DataTable GetDailySignUpDetailsForExcel()
    {
        DataTable dtNew = new DataTable();
        DataTable dt = new DataTable();
        try
        {
            dtNew = DAL.GetDataTable("Sp_GetDailySignUpdetails");
            dt.Columns.Add("Sr.No.", typeof(string));
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("EmailId", typeof(string));
            dt.Columns.Add("Role", typeof(string));
            dt.Columns.Add("RegisteredFrom", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            if (dtNew.Rows.Count > 0)
            {
                for (int i = 0; i < dtNew.Rows.Count; i++)
                {
                    DataRow row = dt.NewRow();
                    row["Sr.No."] = dtNew.Rows[i]["RN"].ToString();
                    row["FullName"] = dtNew.Rows[i]["sFullName"].ToString();
                    string Mobile = (dtNew.Rows[i]["sMobile"].ToString() != "") ? CryptoHelper.Decrypt(dtNew.Rows[i]["sMobile"].ToString()) : "";
                    string EmailId = (dtNew.Rows[i]["sEmailId"].ToString() != "") ? CryptoHelper.Decrypt(dtNew.Rows[i]["sEmailId"].ToString()) : "";
                    row["Mobile"] = Mobile;
                    row["EmailId"] = EmailId;
                    row["Role"] = dtNew.Rows[i]["sRole"].ToString();
                    row["RegisteredFrom"] = dtNew.Rows[i]["sRegisteredFrom"].ToString();
                    row["Address"] = dtNew.Rows[i]["sAddress"].ToString();
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            EmailCurrMonthSummary();
        }

        catch (Exception Ex)
        { }
    }
    public void EmailCurrMonthSummary()
    {
        try
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add("amol@howzu.co.in");
          //mail.To.Add("anuj@howzu.co.in");
            mail.Subject = "New Signup Users List " + DateTime.Now.ToShortDateString();

            mail.From = new System.Net.Mail.MailAddress("visionarylifesciences7@gmail.com");
            mail.IsBodyHtml = true;
            mail.Body = "New Signup Users List";
            //Get some binary data
            byte[] data = GetData();
            //save the data to a memory stream
            System.IO.MemoryStream ms = new System.IO.MemoryStream(data);

            //create the attachment from a stream. Be sure to name the data with a file and
            //media type that is respective of the data
            mail.Attachments.Add(new System.Net.Mail.Attachment(ms, "Howzuusers.xls", "application/vnd.ms-excel"));
            
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "relay-hosting.secureserver.net";
            client.Port = 25;

            //Setup credentials to login to our sender email address ("UserName", "Password")
            NetworkCredential credentials = new NetworkCredential("visionarylifesciences7@gmail.com", "vls1234$");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            client.Send(mail);

        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
    public byte[] GetData()
    {
        //this method just returns some binary data.
        //it could come from anywhere, such as Sql Server
        //string s = "this is some text";
        System.Data.DataTable workTable = GetDailySignUpDetailsForExcel();
        workTable.TableName = "Customers";

        string strBody = DataTable2ExcelString(workTable);
        byte[] data = Encoding.ASCII.GetBytes(strBody);
        return data;
    }
    static string DataTable2ExcelString(System.Data.DataTable dt)
    {
        StringBuilder sbTop = new StringBuilder();
        sbTop.Append("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ");
        sbTop.Append("xmlns=\" http://www.w3.org/TR/REC-html40\"><head><meta http-equiv=Content-Type content=\"text/html; charset=windows-1252\">");
        sbTop.Append("<meta name=ProgId content=Excel.Sheet ><meta name=Generator content=\"Microsoft Excel 9\"><!--[if gte mso 9]>");
        sbTop.Append("<xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>" + dt.TableName + "</x:Name><x:WorksheetOptions>");
        sbTop.Append("<x:Selected/><x:ProtectContents>False</x:ProtectContents><x:ProtectObjects>False</x:ProtectObjects>");
        sbTop.Append("<x:ProtectScenarios>False</x:ProtectScenarios></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets>");
        sbTop.Append("<x:ProtectStructure>False</x:ProtectStructure><x:ProtectWindows>False</x:ProtectWindows></x:ExcelWorkbook></xml>");
        sbTop.Append("<![endif]-->");
        sbTop.Append("</head><body><table>");
        string bottom = "</table></body></html>";
        StringBuilder sb = new StringBuilder();
        //Header
        sb.Append("<tr>");
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            sb.Append("<td>" + dt.Columns[i].ColumnName + "</td>");
        }
        sb.Append("</tr>");

        //Items
        for (int x = 0; x < dt.Rows.Count; x++)
        {
            sb.Append("<tr>");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append("<td>" + dt.Rows[x][i] + "</td>");
            }
            sb.Append("</tr>");
        }

        string SSxml = sbTop.ToString() + sb.ToString() + bottom;

        return SSxml;
    }
}
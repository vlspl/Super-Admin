using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;

public partial class SuperAdmin_upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
        try
        {
            string serverName = "http://enterprise.visionarylifesciences.in/";
            string folder = "newUploadImg/";
            string fileName = Path.GetFileName(FileUpload1.FileName);
            WebClient webClient = new WebClient();
            webClient.Credentials = new NetworkCredential("ftp_eprise", "VLS@012!@"); ;
            Uri uri = new Uri(serverName + folder + fileName);
            webClient.UploadFile(uri, "POST");
            // Upload to current server.
           // FileUpload1.PostedFile.SaveAs(Server.MapPath("~/" + folder + fileName));
            //string FileName = FileUpload1.FileName;

            //string path = string.Concat(("enterprise.visionarylifesciences.in/newUploadImg/" + FileUpload1.FileName));

            //FileUpload1.PostedFile.SaveAs(path);

          
            string msg = "File Upload Successfully..!";
            ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('" + msg + "');", true);
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
           // ExceptionLogging.SendExcepToDB(ex);
           // Response.Redirect(@"../Login.aspx");
        }
    }
   
}
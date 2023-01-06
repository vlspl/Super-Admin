<%@ WebHandler Language="C#" Class="ReportUploader" %>

using System;
using System.Web;
using System.IO;
using System.Configuration;

public class ReportUploader : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string path1 = "~/ENTERPRISE/UploadFiles/";
        string fname1;
        //bool statusToUpload =true;
        bool replaceAlert = Convert.ToBoolean(context.Request.Form["replaceStatus"]);
        string pqr = "/ENTERPRISE/UploadFiles/";
        string path2 = pqr.Replace("/", @"\");
        string file_name;
        if (context.Request.Files.Count > 0)
        {
            string filepath = "";
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];

                file_name = HttpContext.Current.Session.SessionID + file.FileName;
                fname1 = file_name;
                fname1 = Path.Combine(context.Server.MapPath(path1), fname1);
                file.SaveAs(fname1);
               
                if (i == 0)
                {
                    filepath = file_name;
                }
                else
                {
                    filepath += "," + file_name;
                }
            }
            context.Response.Write("{\"sStatus\":\"success\",\"Path\":\"" + filepath + "\"}");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
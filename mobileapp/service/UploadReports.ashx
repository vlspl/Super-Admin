<%@ WebHandler Language="C#" Class="UploadReports" %>

using System;
using System.Web;
using System.IO;
using System.Configuration;

public class UploadReports : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        string path1 = "~/images/Reports";
        string fname1;
        //bool statusToUpload =true;
        bool replaceAlert = Convert.ToBoolean(context.Request.Form["replaceStatus"]);
        string pqr = "/images/Reports";
        string path2 = pqr.Replace("/", @"\");
        string file_name;
        if (context.Request.Files.Count > 0)
        {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];

                file_name =  file.FileName;
                fname1 = file_name;
                fname1 = Path.Combine(context.Server.MapPath(path1), fname1);
                file.SaveAs(fname1);
                context.Response.Write(file_name);   
            }
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
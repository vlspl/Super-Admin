<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;
using System.Configuration;

public class UploadHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string path1 = "~/images/prescription";
        string fname1;
        //bool statusToUpload =true;
        bool replaceAlert = Convert.ToBoolean(context.Request.Form["replaceStatus"]);
        string pqr = "/images/prescription";
        string path2 = pqr.Replace("/", @"\");        

        

       string file_name;
        if (context.Request.Files.Count > 0)
        {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];

                file_name = HttpContext.Current.Session.SessionID + file.FileName;
                fname1 = file_name;
                fname1 = Path.Combine(context.Server.MapPath(path1), fname1);
                file.SaveAs(fname1);
                context.Response.Write("{\"sStatus\":\"success\",\"Path\":\"" + file_name + "\"}");   
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
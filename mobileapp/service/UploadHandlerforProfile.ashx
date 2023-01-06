<%@ WebHandler Language="C#" Class="UploadHandlerforProfile" %>

using System;
using System.Web;
using System.IO;
using System.Configuration;

public class UploadHandlerforProfile : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
      
        string path1 = "~/images/profileimage";
        string fname1;
        //bool statusToUpload =true;
        bool replaceAlert = Convert.ToBoolean(context.Request.Form["replaceStatus"]);
        string pqr = "/images/profileimage";
        string path2 = pqr.Replace("/", @"\");        

         string file_name="";
     if (context.Request.Files.Count > 0)
        {
		  HttpFileCollection files = context.Request.Files; //context
       
            //HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                file_name = HttpContext.Current.Session.SessionID + file.FileName;
                fname1 = file_name;
                fname1 = Path.Combine(context.Server.MapPath(path1), fname1);
                file.SaveAs(fname1);
                context.Response.Write(fname1);	
				  context.Response.Write("File Name "+path1);
             // context.Response.Write(file_name);
            }
			
			
        }
		else
		{
		     context.Response.Write("File Not Found.."+path1);
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
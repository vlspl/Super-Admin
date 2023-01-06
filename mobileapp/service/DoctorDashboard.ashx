<%@ WebHandler Language="C#" Class="doctorDashboard" %>

using System;
using System.Web;

public class doctorDashboard : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");

        string name = context.Session["Username"].ToString();
        context.Response.ContentType = "text/plain";
        context.Response.Write(name);
        
        
            
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
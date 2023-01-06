<%@ WebHandler Language="C#" Class="SetNewPass" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class SetNewPass : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest (HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();


        switch (Action)
        {
            case "UpdatePass":
                string sUserId = Convert.ToString(context.Request.QueryString["sUserIds"]);
                string Contact = Convert.ToString(context.Request.QueryString["Contact"]);
                string Emails = Convert.ToString(context.Request.QueryString["EmailIds"]);
                
                string ConfPass = Convert.ToString(context.Request.QueryString["ConfPasswords"]);


                int i = selectpass(sUserId, Contact, Emails, ConfPass);
                context.Response.Write(i);

              

                break;
          
        }
    }




    public int selectpass(string userid, string contact, string emails, string sConfPass)
    {
        try
        {

            string Query1 = "update [dbo].[appUser] set sPassword ='" + sConfPass + "' where sMobile= '" + contact + "' and sAppUserId='" + userid + "' and sEmailId='" + emails + "' ";


                scon = new SqlConnection(strcon);
                scon.Open();
                scom = new SqlCommand(Query1, scon);
                int i = scom.ExecuteNonQuery();
                scon.Close();
                return i;
 
        }
        catch (Exception)
        {
            return 0;
            throw;
        }
    }


    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
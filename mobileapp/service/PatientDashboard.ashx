<%@ WebHandler Language="C#" Class="PatientDashboard" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientDashboard : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();


        switch (Action)
        {
            case "Update":


                string Usermobnumberid = Convert.ToString(context.Request.QueryString["Usermobnumberid"]);
                string userregisterid = Convert.ToString(context.Request.QueryString["userregisterid"]);

                int i = Update(Usermobnumberid, userregisterid);
                context.Response.Write(i);

                break;


                
                
        }
    }


    public int Update(string Usermobnumberid, string userregisterid)
    {
        try
        {



            string Query = "update [dbo].[appUser] set sDeviceToken ='" + userregisterid + "'  where sMobile= '" + Usermobnumberid + "' ";


            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            int i = scom.ExecuteNonQuery();
            scon.Close();
            return i;
        }
        catch (Exception)
        {
            scon.Close();
            return 0;
            throw;
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
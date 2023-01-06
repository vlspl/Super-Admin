<%@ WebHandler Language="C#" Class="DoctorChangepass" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class DoctorChangepass : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                string DoctorId = Convert.ToString(context.Request.QueryString["DoctorIds"]);
                string Contact = Convert.ToString(context.Request.QueryString["Contact"]);
                string OldPass = Convert.ToString(context.Request.QueryString["OldPasswords"]);
                string NewPass = Convert.ToString(context.Request.QueryString["NewPasswords"]);
                string ConfPass = Convert.ToString(context.Request.QueryString["ConfPasswords"]);


                int i = selectpass( Contact, OldPass,DoctorId,ConfPass);
                context.Response.Write(i);

              

                break;
          
        }
    }




    public int selectpass(string contact, string Password, string userid, string sConfPass)
    {
        try
        {
            DataTable dt = new DataTable();
            string Query = "Select * from appUser where sMobile='" + contact + "' and sPassword='" + Password + "' and sAppUserId = '" + userid + "'";
            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            sda = new SqlDataAdapter(scom);
            sda.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                string Query1 = "update [dbo].[appUser] set sPassword ='" + sConfPass + "' where sMobile= '" + contact + "' and sAppUserId='" + userid + "' ";


                scon = new SqlConnection(strcon);
                scon.Open();
                scom = new SqlCommand(Query1, scon);
                int i = scom.ExecuteNonQuery();
                scon.Close();
                return i;

            }
            else
            {
                scon.Close();
                return 0;
                
            }
            
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
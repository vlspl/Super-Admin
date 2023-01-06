<%@ WebHandler Language="C#" Class="ForgetPassword" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class ForgetPassword : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "UserData":
                string EmailIds = Convert.ToString(context.Request.QueryString["sEmailId"]);
                string Contact = Convert.ToString(context.Request.QueryString["Contact"]);

                DataTable dt = SelectUser(EmailIds, Contact);
                 if (dt != null && dt.Rows.Count > 0)
                {

                    sbData.Append("[{");
                    sbData.Append("\"sUsername\":\"" + dt.Rows[0]["sMobile"].ToString() + "\",");
                    sbData.Append("\"sPassword\":\"" + dt.Rows[0]["sPassword"].ToString() + "\",");
                    sbData.Append("\"sEmailId\":\"" + dt.Rows[0]["sEmailId"].ToString() + "\",");

                    sbData.Append("\"sAppUserID\":\"" + dt.Rows[0]["sAppUserID"].ToString() + "\",");
                    sbData.Append("\"sDegree\":\"" + dt.Rows[0]["sDegree"].ToString() + "\",");
                    sbData.Append("\"sSpecialization\":\"" + dt.Rows[0]["sSpecialization"].ToString() + "\",");
                    sbData.Append("\"sClinic\":\"" + dt.Rows[0]["sClinic"].ToString() + "\",");
                    
                    
                    sbData.Append("\"sRole\":\"" + dt.Rows[0]["sRole"].ToString() + "\",");
                    sbData.Append("\"sFullName\":\"" + dt.Rows[0]["sFullName"].ToString() + "\",");
                    
                    sbData.Append("\"sStatus\":\"Valid\"");
                    sbData.Append("}]");
                }
                else
                {
                    sbData.Append("[{\"sStatus\":\"Invalid Mobile or Email\"}]");

                }
                context.Response.Write(sbData.ToString());
                
                break;
          
        }
    }




    public DataTable SelectUser(string emails, string contact)
    {
        try
        {
            DataTable dt = new DataTable();
            string Query = "Select * from appUser where sEmailId='" + emails + "' and sMobile='" + contact + "'";
            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            sda = new SqlDataAdapter(scom);
            int i = scom.ExecuteNonQuery();
            sda.Fill(dt);
            return dt;
            
        }
        catch (Exception)
        {
            return null;
            throw;
        }
    }




    
    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
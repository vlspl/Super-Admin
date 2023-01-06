<%@ WebHandler Language="C#" Class="Notifications" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class Notifications : IHttpHandler
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();

    

        switch (Action)
        {
            case "GetNotificationData":

                string Patientid = Convert.ToString(context.Request.QueryString["Patientid"]);
                // string UserID = Convert.ToString(context.Request.QueryString["UserID"]);
                 //string doctorid = Convert.ToString(context.Request.QueryString["UserID"]);

                DataTable dt = GetNotificationData(Patientid);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (i == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sUserappid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sUserappid"].ToString()) + "\",");
                            sbData.Append("\"sLabID\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabID"].ToString()) + "\",");
                            sbData.Append("\"sTitle\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTitle"].ToString().Replace("-", "/")) + "\",");
                            sbData.Append("\"sMessage\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMessage"].ToString()) + "\",");
                            sbData.Append("\"sStatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sStatus"].ToString()) + "\",");
                            //sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");
                            sbData.Append("\"sDate\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sDate"].ToString()) + "\",");
                            sbData.Append("\"sLabUserid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabUserid"].ToString()) + "\",");

                            sbData.Append("\"scol9\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["scol9"].ToString()) + "\"");  
                              
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sUserappid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sUserappid"].ToString()) + "\",");
                            sbData.Append("\"sLabID\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabID"].ToString()) + "\",");
                            sbData.Append("\"sTitle\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTitle"].ToString().Replace("-", "/")) + "\",");
                            sbData.Append("\"sMessage\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMessage"].ToString()) + "\",");
                            sbData.Append("\"sStatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sStatus"].ToString()) + "\",");
                            //sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");
                            sbData.Append("\"sDate\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sDate"].ToString()) + "\",");
                            sbData.Append("\"sLabUserid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabUserid"].ToString()) + "\",");
                            
                            sbData.Append("\"scol9\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["scol9"].ToString()) + "\"");  
                             
                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;



            case "GetNotificationSeenData":

                 Patientid = Convert.ToString(context.Request.QueryString["Patientid"]);
                // string UserID = Convert.ToString(context.Request.QueryString["UserID"]);
                //string doctorid = Convert.ToString(context.Request.QueryString["UserID"]);

                 dt = GetNotificationSeenData(Patientid);

                if (dt != null && dt.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (i == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sUserappid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sUserappid"].ToString()) + "\",");
                            sbData.Append("\"sLabID\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabID"].ToString()) + "\",");
                            sbData.Append("\"sTitle\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTitle"].ToString().Replace("-", "/")) + "\",");
                            sbData.Append("\"sMessage\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMessage"].ToString()) + "\",");
                            sbData.Append("\"sStatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sStatus"].ToString()) + "\",");
                            //sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");
                            sbData.Append("\"sDate\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sDate"].ToString()) + "\",");
                            sbData.Append("\"sLabUserid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabUserid"].ToString()) + "\",");

                            sbData.Append("\"scol9\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["scol9"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sUserappid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sUserappid"].ToString()) + "\",");
                            sbData.Append("\"sLabID\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabID"].ToString()) + "\",");
                            sbData.Append("\"sTitle\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTitle"].ToString().Replace("-", "/")) + "\",");
                            sbData.Append("\"sMessage\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMessage"].ToString()) + "\",");
                            sbData.Append("\"sStatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sStatus"].ToString()) + "\",");
                            //sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");
                            sbData.Append("\"sDate\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sDate"].ToString()) + "\",");
                            sbData.Append("\"sLabUserid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabUserid"].ToString()) + "\",");

                            sbData.Append("\"scol9\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["scol9"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());                     
                break;




            case "ReadNotificationSeenData":

                Patientid = Convert.ToString(context.Request.QueryString["Patientid"]);

                dt = UpdateNotificationData(Patientid);             

                break;



                
                

            case "GetNotificationCountData":

                 Patientid = Convert.ToString(context.Request.QueryString["Patientid"]);
                // string UserID = Convert.ToString(context.Request.QueryString["UserID"]);
                //string doctorid = Convert.ToString(context.Request.QueryString["UserID"]);

                 dt = GetNotificationDataCount(Patientid);

                if (dt != null && dt.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (i == 0)
                        {
                            sbData.Append("{");
                            //sbData.Append("\"sUserappid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sUserappid"].ToString()) + "\",");
                            //sbData.Append("\"sLabID\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabID"].ToString()) + "\",");
                            //sbData.Append("\"sTitle\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTitle"].ToString().Replace("-", "/")) + "\",");
                            //sbData.Append("\"sMessage\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMessage"].ToString()) + "\",");
                            //sbData.Append("\"sStatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sStatus"].ToString()) + "\",");
                            ////sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");
                            //sbData.Append("\"sDate\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sDate"].ToString()) + "\",");
                            //sbData.Append("\"sLabUserid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabUserid"].ToString()) + "\",");

                            sbData.Append("\"newnotifications\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["newnotifications"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            //sbData.Append("\"sUserappid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sUserappid"].ToString()) + "\",");
                            //sbData.Append("\"sLabID\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabID"].ToString()) + "\",");
                            //sbData.Append("\"sTitle\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTitle"].ToString().Replace("-", "/")) + "\",");
                            //sbData.Append("\"sMessage\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMessage"].ToString()) + "\",");
                            //sbData.Append("\"sStatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sStatus"].ToString()) + "\",");
                            ////sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");
                            //sbData.Append("\"sDate\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sDate"].ToString()) + "\",");
                            //sbData.Append("\"sLabUserid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabUserid"].ToString()) + "\",");

                            sbData.Append("\"newnotifications\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["newnotifications"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;


                
                
                
        }
    }


    public DataTable GetNotificationData(string UserID)
    {
        try
        {
            //string Query = @"select * from AppNotification where suserappid=" + UserID + " and sStatus = '1' order by snotificationid desc";
            string Query = @"select Top 100 * from AppNotification where suserappid=" + UserID + "  order by snotificationid desc";
            DataTable dt = new DataTable();
            scon = new SqlConnection(strcon);
            scom = new SqlCommand(Query, scon);
            sda = new SqlDataAdapter(scom);
            sda.Fill(dt);
            return dt;

        }
        catch (Exception)
        {
            return null;
        }
    }


    public DataTable GetNotificationDataCount(string UserID)
    {
        try
        {
            string Query = @"select count(sStatus) as newnotifications from [dbo].[AppNotification] where suserappid=" + UserID + " and sStatus = '1'";

            DataTable dt = new DataTable();
            scon = new SqlConnection(strcon);
            scom = new SqlCommand(Query, scon);
            sda = new SqlDataAdapter(scom);
            sda.Fill(dt);
            return dt;

        }
        catch (Exception)
        {
            return null;
        }
    }


    public DataTable GetNotificationSeenData(string UserID)
    {
        try
        {
            string Query = @"select top 10* from AppNotification where suserappid=" + UserID + " and sStatus = '0' order by snotificationid desc";

            DataTable dt = new DataTable();
            scon = new SqlConnection(strcon);
            scom = new SqlCommand(Query, scon);
            sda = new SqlDataAdapter(scom);
            sda.Fill(dt);
            return dt;

        }
        catch (Exception)
        {
            return null;
        }
    }


    public DataTable UpdateNotificationData(string UserID)
    {
        try
        {
            string Query = @"update AppNotification set sStatus='0'  where suserappid=" + UserID + " ";

            DataTable dt = new DataTable();
            scon = new SqlConnection(strcon);
            scom = new SqlCommand(Query, scon);
            sda = new SqlDataAdapter(scom);
            sda.Fill(dt);
            return dt;

        }
        catch (Exception)
        {
            return null;
        }
    }
    
    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
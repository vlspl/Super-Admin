<%@ WebHandler Language="C#" Class="SendNotification" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;


public class SendNotification : IHttpHandler {

    string strcon = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();


        switch (Action)
        {
            case "InserData":
                {
                    //string Userid = Convert.ToString(context.Request.QueryString["sUserid"]);
                    //string PageId = Convert.ToString(context.Request.QueryString["sPageId"]);
                    //string RoleColumn = Convert.ToString(context.Request.QueryString["sRoleColumn"]);
                    //string RoleColumnValue = Convert.ToString(context.Request.QueryString["sRoleColumnValue"]);


                    string UserAppid = Convert.ToString(context.Request.QueryString["sUserid"]);
                    string LabID = Convert.ToString(context.Request.QueryString["sPageId"]);
                    string Title = Convert.ToString(context.Request.QueryString["sRoleColumn"]);
                    string Message = Convert.ToString(context.Request.QueryString["sRoleColumnValue"]);
                    string Status = Convert.ToString(context.Request.QueryString["sUserid"]);
                    string Date = Convert.ToString(context.Request.QueryString["sPageId"]);

                    int i = insertNotification( UserAppid,  LabID,  Title,  Message,  Status,  Date);
                    context.Response.Write(i);
                }
                break;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public int insertNotification(string UserAppid, string LabID, string Title, string Message, string Status, string Date)
    {
        try
        {
           // string Query = @"update LabUserRolesList set " + RoleColumn + "='" + RoleColumnValue + "' where suserid='" + Userid + "' and sPageID='" + PageId + "' ";
            string Query = @"INSERT INTO appnotification (sUserAppid,sLabID,sTitle,sMessage,sStatus,sDate) VALUES (@sUserAppid,@sLabID,@sTitle,@sMessage,@sStatus,@sDate)";
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


}
<%@ WebHandler Language="C#" Class="EditRoles" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;


public class EditRoles : IHttpHandler {

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
                    string Userid = Convert.ToString(context.Request.QueryString["sUserid"]);
                    string PageId = Convert.ToString(context.Request.QueryString["sPageId"]);
                    string RoleColumn = Convert.ToString(context.Request.QueryString["sRoleColumn"]);
                    string RoleColumnValue = Convert.ToString(context.Request.QueryString["sRoleColumnValue"]);

                    int i =  RoleDataInsert(Userid, PageId, RoleColumn, RoleColumnValue);
                    context.Response.Write(i);                 
                }
                break;



        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public int RoleDataInsert(string Userid, string PageId, string RoleColumn, string RoleColumnValue)
    {
        try
        {
            string Query = @"update LabUserRolesList set " + RoleColumn + "='" + RoleColumnValue + "' where suserid='" + Userid + "' and sPageID='" + PageId + "' ";
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
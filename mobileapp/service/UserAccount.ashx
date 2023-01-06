<%@ WebHandler Language="C#" Class="UserAccount" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Text;
using System.IO;


public class UserAccount : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest (HttpContext context) 
    {
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();
        string Name = "";
        //context.Session["Username"] = Convert.ToString(context.Request.QueryString["Username"]);
        //string strsnm = context.Session["Username"].ToString();
        //if (context.Session["sPatientName"] != null)
        //    Name = context.Session["sPatientName"].ToString();
        //context.Response.Write(Name);
        
        switch (Action)
            {
            case "GetData":
                    DataTable dt = GetData(Name);
                if(dt !=null && dt.Rows.Count >0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sPatientName\":\"" + dt.Rows[i]["sPatientName"].ToString() + "\",");
                            sbData.Append("\"sPatientContact\":\"" + dt.Rows[i]["sPatientContact"].ToString() + "\",");
                            sbData.Append("\"sPatientEmailId\":\"" + dt.Rows[i]["sPatientEmailId"].ToString() + "\",");
                            sbData.Append("\"sPatientAddress\":\"" + dt.Rows[i]["sPatientAddress"].ToString() + "\"");
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sPatientName\":\"" + dt.Rows[i]["sPatientName"].ToString() + "\",");
                            sbData.Append("\"sPatientContact\":\"" + dt.Rows[i]["sPatientContact"].ToString() + "\",");
                            sbData.Append("\"sPatientEmailId\":\"" + dt.Rows[i]["sPatientEmailId"].ToString() + "\",");
                            sbData.Append("\"sPatientAddress\":\"" + dt.Rows[i]["sPatientAddress"].ToString() + "\"");
                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;
            }
        
    }
    public DataTable GetData(string username)
    {
        try
        {
            string strs = "Select * from  patient where sPatientName = '" + username + "'";
            DataTable dt = new DataTable();
            scon = new SqlConnection(strcon);
            scom = new SqlCommand(strs,scon);
            sda = new SqlDataAdapter(scom);
            sda.Fill(dt);
            return dt;
       }
        catch(Exception ex)
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
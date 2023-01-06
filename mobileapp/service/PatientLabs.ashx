<%@ WebHandler Language="C#" Class="PatientLabs" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientLabs : IHttpHandler {
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();

        //context.Session["Username"] = Convert.ToString(context.Request.QueryString["Username"]);

        //string strsnml = context.Session["Username"].ToString();
        //if (context.Session["Username"] != null)
        //    strsnml = context.Session["Username"].ToString();
        //context.Response.Write(strsnml);

        switch (Action)
        {
            case "GetLabData":
                DataTable dt = GetLabData();
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (i == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sLabName\":\"" + dt.Rows[i]["sLabName"].ToString() + "\",");
                            sbData.Append("\"sLabAddress\":\"" + dt.Rows[i]["sLabAddress"].ToString() + "\",");
                            sbData.Append("\"sLabManager\":\"" + dt.Rows[i]["sLabManager"].ToString() + "\"");
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sLabName\":\"" + dt.Rows[i]["sLabName"].ToString() + "\",");
                            sbData.Append("\"sLabAddress\":\"" + dt.Rows[i]["sLabAddress"].ToString() + "\",");
                            sbData.Append("\"sLabManager\":\"" + dt.Rows[i]["sLabManager"].ToString() + "\"");
                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;
        }
    }


    public DataTable GetLabData()
    {
        try
        {
            string Query = "Select * from labMaster";
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
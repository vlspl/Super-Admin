<%@ WebHandler Language="C#" Class="PatientSelectLab" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientSelectLab : IHttpHandler{
    
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
            case "GetLabData":
                {

                    DataTable dt = GetLabData();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sbData.Append("[");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            if (i == 0)
                            {
                                sbData.Append("{");
                                sbData.Append("\"sLabName\":\"" +cGeneralHelper.JSONEscape(dt.Rows[i]["sLabName"].ToString()) + "\",");
                                sbData.Append("\"sLabAddress\":\"" +cGeneralHelper.JSONEscape (dt.Rows[i]["sLabAddress"].ToString()) + "\",");
                                sbData.Append("\"sLabContact\":\"" + cGeneralHelper.JSONEscape (dt.Rows[i]["sLabContact"].ToString()) + "\",");
                                sbData.Append("\"slabid\":\"" + cGeneralHelper.JSONEscape (dt.Rows[i]["slabid"].ToString()) + "\"");
                                sbData.Append("}");
                            }
                            else
                            {
                                sbData.Append(",{");
                                sbData.Append("\"sLabName\":\"" + cGeneralHelper.JSONEscape (dt.Rows[i]["sLabName"].ToString()) + "\",");
                                sbData.Append("\"sLabAddress\":\"" + cGeneralHelper.JSONEscape (dt.Rows[i]["sLabAddress"].ToString()) + "\",");
                                sbData.Append("\"sLabContact\":\"" + cGeneralHelper.JSONEscape (dt.Rows[i]["sLabContact"].ToString()) + "\",");
                                sbData.Append("\"slabid\":\"" + cGeneralHelper.JSONEscape (dt.Rows[i]["slabid"].ToString()) + "\"");
                                sbData.Append("}");
                            }
                        }
                        sbData.Append("]");
                    }
                    context.Response.Write(sbData.ToString());
                }
                break;



            case "GetLabDataId":
                {
                    string Testiddata = Convert.ToString(context.Request.QueryString["Testiddata"]);
                    string Testcount = Convert.ToString(context.Request.QueryString["Testcount"]);
                    string sLabIds = Convert.ToString(context.Request.QueryString["sLabId"]);


                    DataTable dt = GetLabIdData(Testiddata, Testcount, sLabIds);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sbData.Append("[");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            if (i == 0)
                            {
                                sbData.Append("{");
                                sbData.Append("\"sLabName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabName"].ToString()) + "\",");
                                sbData.Append("\"sLabAddress\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabAddress"].ToString()) + "\",");
                                sbData.Append("\"sLabContact\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabContact"].ToString()) + "\",");
                                sbData.Append("\"slabid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["slabid"].ToString()) + "\"");
                                sbData.Append("}");
                            }
                            else
                            {
                                sbData.Append(",{");
                                sbData.Append("\"sLabName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabName"].ToString()) + "\",");
                                sbData.Append("\"sLabAddress\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabAddress"].ToString()) + "\",");
                                sbData.Append("\"sLabContact\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabContact"].ToString()) + "\",");
                                sbData.Append("\"slabid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["slabid"].ToString()) + "\"");
                                sbData.Append("}");
                            }
                        }
                        sbData.Append("]");
                    }
                    context.Response.Write(sbData.ToString());
                }
                break;
                
                
                
                
        }
    }


    public DataTable GetLabData()
    {
        try
        {
          //  string Query = @"select slabname, sLabAddress ,sLabContact , slabid from labmaster where sLabStatus ='Active'";
            string Query = @"select slabname, sLabAddress ,sLabContact , slabid from labmaster where sLabStatus ='Active' and slabid not in (1)";
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


    public DataTable GetLabIdData(string testiddata, string testcount, string LabId)
    {
        try
        {
            string Query = @"select slabname, sLabAddress ,sLabContact , slabid from labmaster where sLabStatus ='Active' and slabid not in (1)";
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
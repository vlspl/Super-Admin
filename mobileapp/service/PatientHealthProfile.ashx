<%@ WebHandler Language="C#" Class="PatientHealthProfile" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientHealthProfile : IHttpHandler
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
            case "GetTestList":
                DataTable dt1 = GetPatientListProfile();
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    sbData.Append("[");
                     for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            var prnt = dt1.Rows[j]["sTestProfileId"].ToString();

                            if (j == 0)
                            {
                                sbData.Append("{");
                                sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sTestName"].ToString()) + "\",");
                                sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sTestCode"].ToString()) + "\",");
                                sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sTestId"].ToString()) + "\"");                             
                                                            
                                                          
                                sbData.Append("}");
                            }
                            else
                            {
                                sbData.Append(",{");
                                sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sTestName"].ToString()) + "\",");
                                sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sTestCode"].ToString()) + "\",");
                                sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sTestId"].ToString()) + "\"");                              
                                                             
                                                          
                                sbData.Append("}");
                            }
                         
                         
                         
                   
                       
                    }
                     sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;




           
                
        }

    }


    public DataTable GetPatientList(string prnt)
    {
        try
        {
            string Query = @"select top 10 * from test where sTestProfileId = " + prnt + "order by sTestCode";
            
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



    public DataTable GetPatientListProfile()
    {
        try
        {
            string Query = "select * from test order by sTestCode ";
            DataTable dt1 = new DataTable();
            scon = new SqlConnection(strcon);
            scom = new SqlCommand(Query, scon);
            sda = new SqlDataAdapter(scom);
            sda.Fill(dt1);
            return dt1;

        }
        catch (Exception)
        {
            return null;
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
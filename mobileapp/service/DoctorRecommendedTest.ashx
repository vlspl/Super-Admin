<%@ WebHandler Language="C#" Class="DoctorRecommendedTest" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class DoctorRecommendedTest : IHttpHandler {

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest (HttpContext context) {
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);

        StringBuilder sbData = new StringBuilder();

        switch (Action)
        {
            case "GetRecommendedTest":
                string PatientsIds = Convert.ToString(context.Request.QueryString["PatientId"]);
                DataTable dt1 = GetDoctorList();
                
                
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        var DoctorId = dt1.Rows[j]["sAppUserId"].ToString();
                        if (j == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sFullName"].ToString()) + "\"");
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sFullName"].ToString()) + "\"");
                            sbData.Append("}");
                        }


                        DataTable dt = GetTestList(PatientsIds, DoctorId);
                        
                        if (dt != null && dt.Rows.Count > 0)
                        {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i == 0 && j==0)
                            {
                                sbData.Append(",{");
                                //sbData.Append("\"sAppUserId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sAppUserId"].ToString()) + "\",");
                                
                                sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestName"].ToString()) + "\",");
                                sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                                sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestId"].ToString()) + "\"");

                                sbData.Append("}");
                            }
                            else
                            {
                                sbData.Append(",{");

                                //sbData.Append("\"sAppUserId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sAppUserId"].ToString()) + "\",");
                                //sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");
                                sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestName"].ToString()) + "\",");
                                sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                                sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestId"].ToString()) + "\"");


                                sbData.Append("}");
                            }
                        }
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;
        }
    }


    public DataTable GetTestList(string PatientsIds, string doctorid)
    {
        try
        {
            string Query = "select * from test,[recommendation],[testRecommended] where [recommendation].sRecommendationId = [testRecommended].sRecommendationId and [testRecommended].stestid =  test.stestid and sPatientID=" + PatientsIds + " and sDoctorId=" + doctorid + " ";
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

    public DataTable GetDoctorList()
    {
        try
        {
            string Query = "select * from appUser where sRole='doctor' ";
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
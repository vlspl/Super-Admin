<%@ WebHandler Language="C#" Class="DoctorPatientReports" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class DoctorPatientReports : IHttpHandler
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();

      

        switch (Action)
        {
            case "GetLabData":

                string Patientid = Convert.ToString(context.Request.QueryString["Patientid"]);
                 //string UserID = Convert.ToString(context.Request.QueryString["UserID"]);
                 string doctorid = Convert.ToString(context.Request.QueryString["DoctorID"]);

                 DataTable dt = GetLabData(Patientid,doctorid);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (i == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestName"].ToString()) + "\",");
                            sbData.Append("\"sBookLabId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sBookLabId"].ToString()) + "\",");
                            sbData.Append("\"sReportCreatedOn\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sReportCreatedOn"].ToString()) + "\",");
                            
                            sbData.Append("\"sBookLabTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sBookLabTestId"].ToString()) + "\"");
                            //sbData.Append("\"sLabName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabName"].ToString()) + "\",");
                            //sbData.Append("\"sLabId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabId"].ToString()) + "\",");
                            //sbData.Append("\"sbooklabid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sbooklabid"].ToString()) + "\",");
                            //sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestId"].ToString()) + "\",");
                            //sbData.Append("\"sBookStatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sBookStatus"].ToString()) + "\"");  
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestName"].ToString()) + "\",");
                            sbData.Append("\"sBookLabId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sBookLabId"].ToString()) + "\",");
                            sbData.Append("\"sReportCreatedOn\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sReportCreatedOn"].ToString()) + "\",");
                            
                            sbData.Append("\"sBookLabTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sBookLabTestId"].ToString()) + "\"");
                            //sbData.Append("\"sLabName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabName"].ToString()) + "\",");
                            //sbData.Append("\"sLabId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabId"].ToString()) + "\",");
                            //sbData.Append("\"sbooklabid\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sbooklabid"].ToString()) + "\",");
                            //sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestId"].ToString()) + "\",");
                            //sbData.Append("\"sBookStatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sBookStatus"].ToString()) + "\"");  
                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;
        }
    }

    

    public DataTable GetLabData(string Patientid, string doctorid)
    {
        try
        {
            string Query = @" select *  from test, bookLabTest,sharedReport where 
 bookLabTest.sBookLabTestId = sharedReport.sReportId and  bookLabTest.stestid = test.stestid and sharedReport.sPatientId = " + Patientid + " and  sharedReport.sDoctorId=" + doctorid + " and sharedReport.sShared=1 ";

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
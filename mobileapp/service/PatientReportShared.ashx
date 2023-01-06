<%@ WebHandler Language="C#" Class="PatientReportShared" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientReportShared : IHttpHandler
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
            case "GetLabData":

                string Patientid = Convert.ToString(context.Request.QueryString["Patientid"]);
                string UserID = Convert.ToString(context.Request.QueryString["UserID"]);
                string doctorid = Convert.ToString(context.Request.QueryString["DoctorsId"]);

                DataTable dt = GetLabData(Patientid, UserID, doctorid);

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
                            //sbData.Append("\"sTimeSlot\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTimeSlot"].ToString()) + "\",");
                            //sbData.Append("\"sTestDate\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestDate"].ToString()) + "\",");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                            
                            sbData.Append("\"sSharedReportId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sSharedReportId"].ToString()) + "\",");
                            
                            
                            sbData.Append("\"sBookLabTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sBookLabTestId"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestName"].ToString()) + "\",");
                            //sbData.Append("\"sTimeSlot\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTimeSlot"].ToString()) + "\",");
                            //sbData.Append("\"sTestDate\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestDate"].ToString()) + "\",");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sSharedReportId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sSharedReportId"].ToString()) + "\",");
                            
                            
                            
                            sbData.Append("\"sBookLabTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sBookLabTestId"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;

            case "UpdateReports":
                string ReportID = Convert.ToString(context.Request.QueryString["listReport"]);
                string Patientids = Convert.ToString(context.Request.QueryString["PatientId"]);
                string Doctorids = Convert.ToString(context.Request.QueryString["DoctorsId"]);

              
                DataSet dsReports = updatereports(ReportID);
                        context.Response.Write(dsReports);
               

            
                break;
        }
    }


    public DataTable GetLabData(string Patientid, string UserID, string doctorid)
    {
        try
        {
            string Query = @" select *  from test, bookLabTest,sharedReport where 
 bookLabTest.sBookLabTestId = sharedReport.sReportId and  bookLabTest.stestid = test.stestid and sharedReport.sDoctorId = '" + doctorid + "' and sharedReport.sPatientId='" + Patientid + "'";

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


    public DataSet updatereports(string reportId)
    {
        DataSet ds = new DataSet();
        try
        {
            string Query = "delete from sharedReport  where sSharedReportId in (" + reportId + ")";
            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            scom.ExecuteNonQuery();
            return ds;
        }
        catch (Exception ex)
        {
          
            scon.Close();
            //return 0;
            throw ex;
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
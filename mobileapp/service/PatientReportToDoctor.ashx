<%@ WebHandler Language="C#" Class="PatientReportToDoctor" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientReportToDoctor : IHttpHandler
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
        ClsBookDetails objBookDetails = new ClsBookDetails();
       
        switch (Action)
        {
            case "GetLabData":

                string Patientid = Convert.ToString(context.Request.QueryString["Patientid"]);
                string UserID = Convert.ToString(context.Request.QueryString["UserID"]);
                string doctorid = Convert.ToString(context.Request.QueryString["UserID"]);

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
                            sbData.Append("\"sTimeSlot\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTimeSlot"].ToString()) + "\",");
                            sbData.Append("\"sTestDate\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestDate"].ToString()) + "\",");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestName"].ToString()) + "\",");
                            
                            sbData.Append("\"sBookLabTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sBookLabTestId"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestName"].ToString()) + "\",");
                            sbData.Append("\"sTimeSlot\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTimeSlot"].ToString()) + "\",");
                            sbData.Append("\"sTestDate\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestDate"].ToString()) + "\",");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                            
                            
                            sbData.Append("\"sBookLabTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sBookLabTestId"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;

            case "InsertReportList":
                string ReportID = Convert.ToString(context.Request.QueryString["listReport"]);
                string Patientids = Convert.ToString(context.Request.QueryString["PatientId"]);
                string Doctorids = Convert.ToString(context.Request.QueryString["DoctorsId"]);

                string[] splitReports = ReportID.ToString().Split(',');
                foreach (string Reports in splitReports)
                    {
                        int j = insertreports(Reports, Doctorids, Patientids);
                        context.Response.Write(j);
                    }

            
                break;



            case "GetDoctorMobId":
                {
                    string DoctorId = Convert.ToString(context.Request.QueryString["doctorid"]);

                    dt = GetDoctorMobId(DoctorId);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sbData.Append("[");
                      
                                sbData.Append("{");
                                sbData.Append("\"sDeviceToken\":\"" + cGeneralHelper.JSONEscape(dt.Rows[0]["sDeviceToken"].ToString()) + "\"");

                                sbData.Append("}");
                           
                        sbData.Append("]");
                    }
                    context.Response.Write(sbData.ToString());
                }
                break;


            case "sharedReportToDoctor":
                string ReportId = Convert.ToString(context.Request.QueryString["ReportId"]);
                string PatientId = Convert.ToString(context.Request.QueryString["PatientId"]);
                string Doctorid = Convert.ToString(context.Request.QueryString["DoctorsId"]);
                string Shared = Convert.ToString(context.Request.QueryString["Sharedid"]);
                int status = 0;

                if (ReportId != null && PatientId != null && Doctorid != null)
                {

                    status = objBookDetails.sharedReportToDoctor(PatientId, Doctorid, ReportId, Shared); 
                }
                context.Response.Write(status);
                break;
                
      
        }
    }






    public DataTable GetDoctorMobId(string doctorid)
    {
        try
        {
            string Query = @"select * from [dbo].[appUser] where sAppUserid = '" + doctorid + "' ";
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

    

    public DataTable GetLabData(string Patientid, string UserID, string doctorid)
    {
        try
        {
            string Query = @" select * from  [bookLab], test, [bookLabTest] where 
 [bookLabTest].sbooklabid = [bookLab].sbooklabid and  [bookLabTest].stestid = test.stestid and spatientid = " + Patientid + " and [bookLabTest].sApprovalStatus='approved' and sPaymentStatus ='paid' and [bookLabTest].sCol10=1 ";

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


    public int insertreports(string reportId,string doctorsId,string patientsId)
    {
        try
        {
            string Query = "Insert Into [sharedReport] " + "([sReportId],[sDoctorId],[sPatientId]) VALUES" + "('" + reportId + "','" + doctorsId + "','" + patientsId + "')";
            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            int j = scom.ExecuteNonQuery();
            return j;
        }
        catch (Exception ex)
        {
          
            scon.Close();
            return 0;
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
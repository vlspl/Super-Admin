<%@ WebHandler Language="C#" Class="DoctorSelectLab" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class DoctorSelectLab : IHttpHandler
{
    
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    CLSNotification objNotification = new CLSNotification();
    public void ProcessRequest (HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();

  
        
        switch (Action)
        {
            case "GetLabData":
                {
                    string Testiddata = Convert.ToString(context.Request.QueryString["Testiddata"]);
                    string Testcount = Convert.ToString(context.Request.QueryString["Testcount"]);

                    DataTable dt = GetLabData(Testiddata, Testcount);

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






            case "GetLabprice":
                {
                    string Testiddata = Convert.ToString(context.Request.QueryString["Testiddata"]);
                    string Testcount = Convert.ToString(context.Request.QueryString["Testcount"]);
                    string Price = Convert.ToString(context.Request.QueryString["Price"]);

                    DataTable dt = GetLabprices(Testiddata, Testcount, Price);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sbData.Append("[");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            if (i == 0)
                            {
                                sbData.Append("{");
                                sbData.Append("\"sPrice\":\"" + cGeneralHelper.JSONEscape (dt.Rows[i]["sPrice"].ToString()) + "\"");
                                sbData.Append("}");
                            }
                            else
                            {
                                sbData.Append(",{");
                                sbData.Append("\"sPrice\":\"" +cGeneralHelper.JSONEscape ( dt.Rows[i]["sPrice"].ToString()) + "\"");
                                sbData.Append("}");
                            }
                        }
                        sbData.Append("]");
                    }
                    context.Response.Write(sbData.ToString());
                }
                break;
            case "InserData":

                string sTestIds = Convert.ToString(context.Request.QueryString["Testiddata"]);
                string sPatientIds = Convert.ToString(context.Request.QueryString["sPatientId"]);
                string doctorid = Convert.ToString(context.Request.QueryString["sDoctorID"]);
                string sLabIds = Convert.ToString(context.Request.QueryString["sLabId"]);
                string DoctorName = Convert.ToString(context.Request.QueryString["sDoctorName"]);
		string LabName = Convert.ToString(context.Request.QueryString["labname"]);
                string reportCreatedOn = DateTime.Now.AddHours(12).AddMinutes(30).ToString("dd/MM/yyyy") + " " + DateTime.Now.AddHours(12).AddMinutes(30).ToString("HH:mm:ss tt");
                string[] splitPatient = sTestIds.ToString().Split(',');

                            InserTestData(doctorid, sPatientIds, splitPatient, sLabIds);
                            objNotification.AppNotification(sPatientIds, sTestIds, "Report Status", "" + DoctorName + " has suggested you a test.Kindly book the same at " + LabName + " the earliest.", "1", reportCreatedOn, "");
                break;
                
               
                
                
                
                
        }
    }


    public DataTable GetLabData(string testiddata, string testcount)
    {
        try
        {
            string Query = @"select slabname, sLabAddress ,sLabContact , slabid from labmaster where slabid in (SELECT  sLabId FROM    testLab WHERE   sTestId IN (" + testiddata + ")  GROUP   BY sLabId HAVING  COUNT(*) = " + testcount + ") and sLabStatus ='Active' and  not slabid=1";
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






    public DataTable GetLabprices(string testiddata, string testcount, string Price)
    {
      
                     
                 try
                 {
                     DataTable dt = new DataTable();

                     string Query = @"SELECT sum(CAST(sprice AS int)) as sPrice FROM testLab  where slabid = " + Price + " and stestid in(" + testiddata + ")";
                         
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
    public void InserTestData(string doctor, string patient, string[] splitpatient,string sLabIDs)
    {
        try
        {

            string Query = "INSERT INTO [dbo].[recommendation]"
         + "(sDoctorId,sPatientId,sRecommendedAt)OUTPUT INSERTED.sRecommendationId  VALUES"
         + "('" + doctor + "','" + patient + "','" + DateTime.Now.AddHours(12).AddMinutes(30).ToString("dd/MM/yyyy") + " " + DateTime.Now.AddHours(12).AddMinutes(30).ToString("HH:mm:ss tt") + "')";


            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);

            int k = (int)scom.ExecuteScalar();

            foreach (string patientid in splitpatient)
            {
                string Queryinsert = "INSERT INTO [dbo].[testRecommended]" + "([sRecommendationId],[sTestId],[sLabId]) VALUES" + "('" + k + "','" + patientid + "','" + sLabIDs + "')";
                scom = new SqlCommand(Queryinsert, scon);
                scom.ExecuteNonQuery();
            }
            scon.Close();

        }
        catch (Exception ex)
        {
            scon.Close();

            throw;
        }
    }
    
    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="PatientBookAppointmentForPrescription" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientBookAppointmentForPrescription : IHttpHandler
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();
        StringBuilder sbData2 = new StringBuilder();

        
        switch (Action)
        {
            case "GetLabData":
                {
                    string Testiddata = Convert.ToString(context.Request.QueryString["Testiddata"]);

                    DataTable dt = GetLabData(Testiddata);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sbData.Append("[");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            if (i == 0)
                            {
                                sbData.Append("{");
                                sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\"");

                                sbData.Append("}");
                            }
                            else
                            {
                                sbData.Append(",{");
                                sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\"");
                                sbData.Append("}");
                            }
                        }
                        sbData.Append("]");
                    }
                    context.Response.Write(sbData.ToString());
                }
                break;





            case "GetLabDataSlot":
                {
                    string Labsiddata = Convert.ToString(context.Request.QueryString["labsiddata"]);
                    string Weekday = Convert.ToString(context.Request.QueryString["Weekday"]);
                    string AppointmentType = Convert.ToString(context.Request.QueryString["sAppointmentType"]);

                    DataTable dt1 = GetLabDataSlot(Labsiddata, Weekday, AppointmentType);

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        sbData2.Append("[");
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {

                            if (j == 0)
                            {
                                sbData2.Append("{");
                                sbData2.Append("\"sFrom\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sFrom"].ToString()) + "\",");
                                sbData2.Append("\"sTo\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sTo"].ToString()) + "\"");
                                //   sbData2.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sTestName"].ToString()) + "\"");
                                sbData2.Append("}");
                            }
                            else
                            {
                                sbData2.Append(",{");
                                sbData2.Append("\"sFrom\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sFrom"].ToString()) + "\",");
                                sbData2.Append("\"sTo\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sTo"].ToString()) + "\"");
                                //    sbData2.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sTestName"].ToString()) + "\"");
                                sbData2.Append("}");
                            }
                        }
                        sbData2.Append("]");
                    }
                    context.Response.Write(sbData2.ToString());
                }
                break;




            case "InsertLabDataSlot":
                {
                    string Labid = Convert.ToString(context.Request.QueryString["Labid"]);
                    string Weekday = Convert.ToString(context.Request.QueryString["Weekday"]);
                    string Todaydate = DateTime.Now.ToString();
                    string RadioValue = Convert.ToString(context.Request.QueryString["RadioValue"]);
                    string sBookStatus = Convert.ToString(context.Request.QueryString["sBookStatus"]);
                    string sBookMode = Convert.ToString(context.Request.QueryString["sBookMode"]);
                    string Patientid = Convert.ToString(context.Request.QueryString["Patientid"]);
                    string Datelist = Convert.ToString(context.Request.QueryString["Datelist"].Replace("-","/"));
                    string Testprices = Convert.ToString(context.Request.QueryString["Testprices"]);
                    string AppointmentType = Convert.ToString(context.Request.QueryString["sAppointmentType"]);
                    string UploadPrescriptionImg = Convert.ToString(context.Request.QueryString["sUploadPrescriptionImg"]);

                    int i = Register(Labid, Weekday, Todaydate, RadioValue, sBookStatus, sBookMode, Patientid, Datelist,  Testprices,  AppointmentType, UploadPrescriptionImg);
                    context.Response.Write(i);
                }
                break;
                
                
        }
    }


    public DataTable GetLabData(string Testcount)
    {
        try
        {
            string Query = @"select * from [test] where sTestId in (" + Testcount + ")";
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



    public DataTable GetLabDataSlot(string labsiddata, string Weekday)
    {
        try
        {
            string Query = @"select * from [labSlot] where sDay = '" + Weekday + "' and sLabId = " + labsiddata + "";
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


    public DataTable GetLabDataSlot(string labsiddata, string Weekday, string AppointmentType)
    {
        try
        {
            string Query = @"select * from [labSlot] where sDay = '" + Weekday + "' and sLabId = " + labsiddata + " and sAppointmentType = '" + AppointmentType + "'";
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



    public int Register(string Labid, string Weekday, string Todaydate, string RadioValue, string sBookStatus, string sBookMode, string Patientid, string Datelist, string Testprices, string AppointmentType, string UploadPrescriptionImg)
    {
        try
        {

            string Query = "INSERT INTO [dbo].[bookLab]"
          + "([sLabId],[sPatientId], [sBookRequestedAt],[sBookConfirmedAt],[sTimeSlot],[sBookStatus],[sTestStatus],[sBookMode],[sTestDate],sFees, sAppointmentType, sUploadPrescriptionImg) VALUES"
          + "('" + Labid + "','" + Patientid + "','" + Datelist + "','" + DateTime.Now.AddHours(12).AddMinutes(30).ToString("dd/MM/yyyy") + " " + DateTime.Now.AddHours(12).AddMinutes(30).ToString("h:mm tt") + "','" + RadioValue + "', 'Awaiting' ,'Pending', '" + sBookMode + "', '" + Datelist + "', '" + Testprices + "', '" + AppointmentType + "', '" + UploadPrescriptionImg + "');  SELECT CAST(scope_identity() AS int)";


            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            int booklabid = (int)scom.ExecuteScalar();
            scon.Close();
           
            
                string Query1 = "INSERT INTO [dbo].[bookLabTest]"
              + "([sBookLabId],[sTestId]) VALUES"
              + "('" + booklabid + "','0')";


                scon = new SqlConnection(strcon);
                scon.Open();
                scom = new SqlCommand(Query1, scon);
                int j = scom.ExecuteNonQuery();
                scon.Close();
                //return j;
           
            return booklabid;
            
        }
        catch (Exception)
        {
            scon.Close();
            return 0;
            throw;
        }
    }

    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
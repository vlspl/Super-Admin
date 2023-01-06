<%@ WebHandler Language="C#" Class="PatientHealthEditProfile" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientHealthEditProfile : IHttpHandler
{


    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    
    public void ProcessRequest (HttpContext context) {
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        
        StringBuilder sbData = new StringBuilder();

        switch (Action)
        {
            //case "GetPatientList":
            //    string DoctorIds = Convert.ToString(context.Request.QueryString["DoctorId"]);
            //    DataTable dt = GetPatientLists(DoctorIds);

            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        sbData.Append("[");
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            if (i == 0)
            //            {
            //                sbData.Append("{");
            //                sbData.Append("\"sAppUserId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sAppUserId"].ToString()) + "\",");
                            
            //                sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");
            //                sbData.Append("\"sMobile\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMobile"].ToString()) + "\",");
            //                sbData.Append("\"sEmailId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sEmailId"].ToString()) + "\"");

            //                sbData.Append("}");
            //            }
            //            else
            //            {
            //                sbData.Append(",{");
            //                sbData.Append("\"sAppUserId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sAppUserId"].ToString()) + "\",");
                            
            //                sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");
                            
            //                sbData.Append("\"sMobile\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMobile"].ToString()) + "\",");
            //                sbData.Append("\"sEmailId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sEmailId"].ToString()) + "\"");

            //                sbData.Append("}");
            //            }
            //        }
            //        sbData.Append("]");
            //    }
            //    context.Response.Write(sbData.ToString());
            //    break;

            case "InsertUserData":

                string sTestId = Convert.ToString(context.Request.QueryString["sTestId"]);
                string sAnalyte = Convert.ToString(context.Request.QueryString["sAnalyte"]);
                string sSubAnalyte = Convert.ToString(context.Request.QueryString["sSubAnalyte"]);
                string sSampleType = Convert.ToString(context.Request.QueryString["sSampleType"]);
                string sMethod = Convert.ToString(context.Request.QueryString["sMethod"]);
                string sResultType = Convert.ToString(context.Request.QueryString["sResultType"]);
                string sReferenceType = Convert.ToString(context.Request.QueryString["sReferenceType"]);
                string sAge = Convert.ToString(context.Request.QueryString["sAge"]);
                string sMale = Convert.ToString(context.Request.QueryString["sMale"]);
                string sFemale = Convert.ToString(context.Request.QueryString["sFemale"]);
                string sGrade = Convert.ToString(context.Request.QueryString["sGrade"]);
                string sUnits = Convert.ToString(context.Request.QueryString["sUnits"]);
                string sInterpretation = Convert.ToString(context.Request.QueryString["sInterpretation"]);
                string sLowerLimit = Convert.ToString(context.Request.QueryString["sLowerLimit"]);
                string sUpperLimit = Convert.ToString(context.Request.QueryString["sUpperLimit"]);
                string sValue = Convert.ToString(context.Request.QueryString["sValue"]);
                string sDate = Convert.ToString(context.Request.QueryString["sDate"].Replace("-","/"));
                string sTime = Convert.ToString(context.Request.QueryString["sTime"]);
                string sUserId = Convert.ToString(context.Request.QueryString["sUserId"]);



                int j = AddPatientData(sTestId, sAnalyte, sSubAnalyte, sSampleType, sMethod, sResultType, sReferenceType, sAge, sMale, sFemale, sGrade, sUnits, sInterpretation, sLowerLimit, sUpperLimit, sValue, sDate, sTime, sUserId);
                context.Response.Write(j);
                break;
        }
       
    }


    //public DataTable GetPatientLists(string DoctorIds)
    //{
    //    try
    //    {
    //        string Query = "select * from appUser where  sAppuserId not in (Select sPatientId from myPatients where sDoctorId='" + DoctorIds + "')and sAppuserId!='" + DoctorIds + "'";
    //        DataTable dt = new DataTable();
    //        scon = new SqlConnection(strcon);
    //        scom = new SqlCommand(Query, scon);
    //        sda = new SqlDataAdapter(scom);
    //        sda.Fill(dt);
    //        return dt;

    //    }
    //    catch (Exception)
    //    {
    //        return null;
    //    }
    //}

    public int AddPatientData(string TestId, string Analyte, string SubAnalyte, string SampleType, string Method, string ResultType, string ReferenceType, string Age, string Male, string Female, string Grade, string Units, string Interpretation, string LowerLimit, string UpperLimit, string value, string Dates, string Time,string sUserid)
    {
        try
        {
            string Query = "Insert Into testReportValues " + "([sTestId],[sAnalyte],[sSubAnalyte],[sSpecimen],[sMethod],[sResultType],[sReferenceType],[sAge],[sMale],[sFemale],[sGrade],[sUnits],[sInterpretation],[sLowerLimit],[sUpperLimit],[sValue],[sDate],[sTime],[sPatientId]) VALUES" + "('" + TestId + "','" + Analyte + "','" + SubAnalyte + "','" + SampleType + "','" + Method + "','" + ResultType + "','" + ReferenceType + "','" + Age + "','" + Male + "','" + Female + "','" + Grade + "','" + Units + "','" + Interpretation + "','" + LowerLimit + "','" + UpperLimit + "','" + value + "','" + Dates + "','" + Time + "','" + sUserid + "')";
           
             scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            int j = scom.ExecuteNonQuery();            
            scon.Close();
            return j;
            
            
        }
        catch (Exception ex)
        {
            scon.Close();
            return 0;
            throw;
        }
       
    }


    //public int AddPatientsIn(string PatientsId, string DoctorsId)
    //{
    //}
    public bool IsReusable {
        get {
            return false;
        }
    }

}
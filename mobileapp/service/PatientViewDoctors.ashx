<%@ WebHandler Language="C#" Class="PatientViewDoctors" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientViewDoctors : IHttpHandler
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
            case "GetDoctorData":

                string PatientsIds = Convert.ToString(context.Request.QueryString["patients"]);
                DataTable dt = GetLabData(PatientsIds);
                if (dt != null && dt.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (i == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sDoctorId\":\"" + dt.Rows[i]["sDoctorId"].ToString() + "\",");
                            
                            sbData.Append("\"sAppUserId\":\"" + dt.Rows[i]["sAppUserId"].ToString() + "\",");

                            sbData.Append("\"sFullName\":\"" + dt.Rows[i]["sFullName"].ToString() + "\",");
                            sbData.Append("\"sMobile\":\"" + dt.Rows[i]["sMobile"].ToString() + "\",");
                            sbData.Append("\"sEmailId\":\"" + dt.Rows[i]["sEmailId"].ToString() + "\"");
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sDoctorId\":\"" + dt.Rows[i]["sDoctorId"].ToString() + "\",");
                            
                            sbData.Append("\"sAppUserId\":\"" + dt.Rows[i]["sAppUserId"].ToString() + "\",");

                            sbData.Append("\"sFullName\":\"" + dt.Rows[i]["sFullName"].ToString() + "\",");
                            sbData.Append("\"sMobile\":\"" + dt.Rows[i]["sMobile"].ToString() + "\",");
                            sbData.Append("\"sEmailId\":\"" + dt.Rows[i]["sEmailId"].ToString() + "\"");
                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;
        }
    }


    public DataTable GetLabData(string patientId)
    {
        try
        {
            string Query = "select distinct sr.sDoctorId,au.sFullName,au.sAppUserId,au.sMobile,au.sEmailId from sharedReport sr join  appUser au on au.sAppuserId=sr.sDoctorId where sr.sPatientId='" + patientId + "'";

            //string Query = "select mp.sMyPatientId,au.sAppUserId,au.sFullName,au.sMobile,au.sEmailId  from myPatients mp join  appUser au on au.sAppuserId=mp.sPatientId where mp.sDoctorId ='" + DoctorId + "'";
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
<%@ WebHandler Language="C#" Class="PatientMyFriends" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientMyFriends : IHttpHandler
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
            case "GetLabData":

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
                            sbData.Append("\"sAppUserId\":\"" + dt.Rows[i]["sAppUserId"].ToString() + "\",");

                            sbData.Append("\"sFullName\":\"" + dt.Rows[i]["sFullName"].ToString() + "\",");
                            sbData.Append("\"sMobile\":\"" + dt.Rows[i]["sMobile"].ToString() + "\",");
                            sbData.Append("\"sEmailId\":\"" + dt.Rows[i]["sEmailId"].ToString() + "\",");
                            sbData.Append("\"sSpecialization\":\"" + dt.Rows[i]["sSpecialization"].ToString() + "\"");
                            
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sAppUserId\":\"" + dt.Rows[i]["sAppUserId"].ToString() + "\",");

                            sbData.Append("\"sFullName\":\"" + dt.Rows[i]["sFullName"].ToString() + "\",");
                            sbData.Append("\"sMobile\":\"" + dt.Rows[i]["sMobile"].ToString() + "\",");
                            sbData.Append("\"sEmailId\":\"" + dt.Rows[i]["sEmailId"].ToString() + "\",");
                            sbData.Append("\"sSpecialization\":\"" + dt.Rows[i]["sSpecialization"].ToString() + "\"");
                            
                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;

            case "GetDeleteDoc":
                {

                    string PatientId = Convert.ToString(context.Request.QueryString["patients"]);
                    string DoctorId = Convert.ToString(context.Request.QueryString["doctors"]);
                    int j = DeleteDoctors(PatientId,DoctorId);
                    context.Response.Write(j);
                }
                break;
        }
    }


    public DataTable GetLabData(string patientId)
    {
        try
        {
            string Query = "select mf.sFriendId,au.sAppUserId,au.sFullName,au.sMobile,au.sEmailId,au.sSpecialization  from myFriends mf join  appUser au on au.sAppuserId=mf.sFriendId where mf.sUserId ='" + patientId + "'";

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


    public int DeleteDoctors(string patient,string doctor)
    {
        try
        {
            string Query = "Delete From  myDoctors where sPatientId='" + patient + "'and sDoctorId='" + doctor + "'";

            string QueryDelete = "Delete From myPatients where sPatientId='" + patient + "'and sDoctorId='" + doctor + "'";

            string QueryDelReport = "Delete From sharedReport where sPatientId='" + patient + "'and sDoctorId='" + doctor + "'";

            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            int j = scom.ExecuteNonQuery();
            scom = new SqlCommand(QueryDelete, scon);
            j = scom.ExecuteNonQuery();
            scom = new SqlCommand(QueryDelReport, scon);
            j = scom.ExecuteNonQuery();
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
    public bool IsReusable {
        get {
            return false;
        }
    }

}
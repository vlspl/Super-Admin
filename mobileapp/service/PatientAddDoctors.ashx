<%@ WebHandler Language="C#" Class="PatientAddDoctors" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientAddDoctors : IHttpHandler
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();

        //context.Session["Username"] = Convert.ToString(context.Request.QueryString["Username"]);

        //string strsnml = context.Session["Username"].ToString();
        //if (context.Session["Username"] != null)
        //    strsnml = context.Session["Username"].ToString();
        //context.Response.Write(strsnml);

        switch (Action)
        {
            case "GetLabData":
                string PatientIds = Convert.ToString(context.Request.QueryString["PatientId"]);
                 DataTable dt = GetDoctorLists(PatientIds);
             
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (i == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sAppUserId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sAppUserId"].ToString()) + "\",");
                            sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");

                            sbData.Append("\"sMobile\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMobile"].ToString()) + "\",");

                            sbData.Append("\"sAddress\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sAddress"].ToString()) + "\",");

                            sbData.Append("\"sSpecialization\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sSpecialization"].ToString()) + "\",");

                            sbData.Append("\"sClinic\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sClinic"].ToString()) + "\"");
                                                                                                                                            
                            
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sAppUserId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sAppUserId"].ToString()) + "\",");
                            sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");

                            sbData.Append("\"sMobile\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMobile"].ToString()) + "\",");

                            sbData.Append("\"sAddress\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sAddress"].ToString()) + "\",");

                            sbData.Append("\"sSpecialization\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sSpecialization"].ToString()) + "\",");

                            sbData.Append("\"sClinic\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sClinic"].ToString()) + "\"");
                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;


            case "InsertPatientList":

                string listDoctor = Convert.ToString(context.Request.QueryString["listdoctor"]);
                string PatientId = Convert.ToString(context.Request.QueryString["PatientId"]);
                string[] splitPatient = listDoctor.ToString().Split(',');
                foreach (string strs in splitPatient)
                {
                    int j = AddDoctors(strs, PatientId);
                    context.Response.Write(j);
                }


                break;
        }
    }


    public DataTable GetDoctorLists(string PatientIds)
    {
        try
        {
            string Query = "select * from appUser where sRole='doctor' and sAppuserId not in (Select sDoctorId from myDoctors where  sPatientId='" + PatientIds + "') and sAppuserId !='"+PatientIds+"'";
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

    public int AddDoctors(string splitPatient, string DoctorId)
    {
        try
        {
           // string Query = "Insert Into myDoctors " + "([sDoctorId],[sPatientId]) VALUES" + "('" + splitPatient + "','" + DoctorId + "')";

         //  string QueryInsert = "Insert Into myPatients " + "([sDoctorId],[sPatientId]) VALUES" + "('" + splitPatient + "','" + DoctorId + "')";
            
            
       string Query = "if not exists(select * from myDoctors where sPatientId = '" + splitPatient + "' and sDoctorId = '" + DoctorId + "')"
 + "Insert Into myDoctors ([sDoctorId],[sPatientId]) VALUES('" + splitPatient + "','" + DoctorId + "')";

            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            int j = scom.ExecuteNonQuery();
          //  scom = new SqlCommand(QueryInsert, scon);
            // j = scom.ExecuteNonQuery();
            
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
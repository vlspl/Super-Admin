<%@ WebHandler Language="C#" Class="DoctorPatientAdd" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class DoctorPatientAdd : IHttpHandler {


    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    
    public void ProcessRequest (HttpContext context) {
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        
        StringBuilder sbData = new StringBuilder();

        switch (Action)
        {
            case "GetPatientList":
                string DoctorIds = Convert.ToString(context.Request.QueryString["DoctorId"]);
                DataTable dt = GetPatientLists(DoctorIds);

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
                            sbData.Append("\"sEmailId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sEmailId"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sAppUserId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sAppUserId"].ToString()) + "\",");
                            
                            sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");
                            
                            sbData.Append("\"sMobile\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMobile"].ToString()) + "\",");
                            sbData.Append("\"sEmailId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sEmailId"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;

            case "InsertPatientList":
                
                string listPatient = Convert.ToString(context.Request.QueryString["listPatient"]);
                string DoctorId = Convert.ToString(context.Request.QueryString["DoctorId"]);
                string [] splitPatient = listPatient.ToString().Split(',');
                foreach (string strs in splitPatient)
                {
                    int j = AddPatient(strs, DoctorId);

               
                    context.Response.Write(j);
                  
                    
                }
                
              
                break;
        }
       
    }


    public DataTable GetPatientLists(string DoctorIds)
    {
        try
        {
           // string Query = "select * from appUser where  sAppuserId not in (Select sPatientId from myPatients where sDoctorId='" + DoctorIds + "')and sAppuserId!='" + DoctorIds + "'";
           
 string Query = "Select * from appUser inner join myDoctors on myDoctors.sPatientId =appUser.sAppUserId where myDoctors.sDoctorId='" + DoctorIds + "' and myDoctors.sPatientId not in (Select sPatientId from myPatients where sDoctorId ='" + DoctorIds + "')";
        

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

    public int AddPatient(string splitPatient, string DoctorId)
    {
        try
        {
           // string Query = "Insert Into myPatients " + "([sPatientId],[sDoctorId]) VALUES" + "('" + splitPatient + "','" + DoctorId + "')";
          //  string QueryInsert = "Insert Into myDoctors " + "([sPatientId],[sDoctorId]) VALUES" + "('" + splitPatient + "','" + DoctorId + "')";
            string Query = "if not exists(select * from myPatients where sPatientId = '" + splitPatient + "' and sDoctorId = '" + DoctorId + "')"
       +"Insert Into myPatients ([sPatientId],[sDoctorId]) VALUES('" + splitPatient + "','" + DoctorId + "')";
             scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            int j = scom.ExecuteNonQuery();
           // scom = new SqlCommand(QueryInsert, scon);
          //j = scom.ExecuteNonQuery();
            
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
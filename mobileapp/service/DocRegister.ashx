<%@ WebHandler Language="C#" Class="DocRegister" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
public class DocRegister : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "DocRegister":
                 string PatientCode = Convert.ToString(context.Request.QueryString["PatientCode"]);
                 string doctorName = Convert.ToString(context.Request.QueryString["doctorName"]);
                int Age = Convert.ToInt32(context.Request.QueryString["Age"]);
                string Gender = Convert.ToString(context.Request.QueryString["Gender"]);
                DateTime DOB = Convert.ToDateTime(context.Request.QueryString["DOB"]);
                string Contact = Convert.ToString(context.Request.QueryString["Contact"]);
                string Address = Convert.ToString(context.Request.QueryString["Address"]);
                string Password = Convert.ToString(context.Request.QueryString["Password"]);
                string Email = Convert.ToString(context.Request.QueryString["Email"]);
                string Designation = Convert.ToString(context.Request.QueryString["Designation"]);
                string SpecialistIn = Convert.ToString(context.Request.QueryString["SpecialistIn"]);

                int i = DocRegisters(PatientCode, doctorName, Age, Gender, DOB, Contact, Address, Password, Email, Designation, SpecialistIn);
                context.Response.Write(i);
                break;
            case "DocLogin":
                
                string Username = Convert.ToString(context.Request.QueryString["Username"]);
                Password = Convert.ToString(context.Request.QueryString["Password"]);
                DataTable dt = GetLogin(Username, Password);
                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    sbData.Append("[{");
                    sbData.Append("\"sUsername\":\"" + dt.Rows[0]["sPatientName"].ToString() + "\",");
                    sbData.Append("\"sPassword\":\"" + dt.Rows[0]["sPatientPassword"].ToString() + "\",");
                    sbData.Append("\"sStatus\":\"Valid\"");
                    sbData.Append("}]");
                }
                else
                {
                    sbData.Append("[{\"sStatus\":\"Invalid Username or Password\"}]");
                   
                }
                context.Response.Write(sbData.ToString());
               
                break;
        }
        
        
        
    }

    public int DocRegisters(string PatientCode, string doctorName, int Age, string Gender, DateTime DOB, string Contact, string Address, string Password, string Email, string Designation, string SpecialistIn)
    {
        try
        {
            string date = DOB.ToString("dd/MM/yyyy hh:mm:ss tt");

            string Query = "INSERT INTO [dbo].[doctor]"
           + "([sPatientCode],[sPatientName],[sPatientAge],[sPatientGender],[sPatientDOB],[sPatientContact],[sPatientAddress],[sPatientUserName],[sPatientPassword],[sPatientEmailId],[sCol1],[sCol2]) VALUES"
           + "('" + PatientCode + "','" + doctorName + "','" + Age + "','" + Gender + "',Convert(datetime,'" + date + "',105),'" + Contact + "','" + Address + "','" + doctorName + "','" + Password + "','" + Email + "','" + Designation + "','" + SpecialistIn + "')";

            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            int i = scom.ExecuteNonQuery();
            scon.Close();
            return i;
        }
        catch (Exception)
        {
            scon.Close();
            return 0;
            throw;
        }
    }
    public DataTable GetLogin(string Username, string Password)
    {
        try
        {
            DataTable dt = new DataTable();
            string Query = "Select * from doctor where sPatientName='" + Username + "' and sPatientPassword='" + Password + "'";
            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            sda = new SqlDataAdapter(scom);
            sda.Fill(dt);
            return dt;
        }
        catch (Exception)
        {
            return null;
            throw;
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="DoctorEditProfile" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class DoctorEditProfile : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest (HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();


        switch (Action)
        {
            case "Update":

               
                string PatientName = Convert.ToString(context.Request.QueryString["PatientName"]);
                string Contact = Convert.ToString(context.Request.QueryString["Contact"]);
                string Email = Convert.ToString(context.Request.QueryString["Email"]);
                string Password = Convert.ToString(context.Request.QueryString["Password"]);
                string Gender = Convert.ToString(context.Request.QueryString["Gender"]);
                string DOB = Convert.ToString(context.Request.QueryString["DOB"]);
               
                string Address = Convert.ToString(context.Request.QueryString["Address"]);
                
                
                string Checkdoctor = Convert.ToString(context.Request.QueryString["Checkdoctor"]);

                string Designation = Convert.ToString(context.Request.QueryString["Designation"]);

                string SpecialIn = Convert.ToString(context.Request.QueryString["SpecialIn"]);
                string Clinic = Convert.ToString(context.Request.QueryString["Clinic"]);
                

                int i = Update(PatientName, Contact, Email, Password, Gender, DOB, Address, Checkdoctor, Designation, SpecialIn, Clinic);
                context.Response.Write(i);

                break;
          

            case "UserData":
                string Usernames = Convert.ToString(context.Request.QueryString["Username"]);

                string struser = context.Session["Username"].ToString();
                DataTable ds = GetUserDetails(struser);
                if (ds != null && ds.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int j = 0; j < ds.Rows.Count; j++)
                    {
                        if (j == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sPatientName\":\"" + ds.Rows[j]["sPatientName"].ToString() + "\",");
                            sbData.Append("\"sPatientContact\":\"" + ds.Rows[j]["sPatientContact"].ToString() + "\",");
                            sbData.Append("\"sPatientEmailId\":\"" + ds.Rows[j]["sPatientEmailId"].ToString() + "\",");
                            sbData.Append("\"sPatientAddress\":\"" + ds.Rows[j]["sPatientAddress"].ToString() + "\"");
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sPatientName\":\"" + ds.Rows[j]["sPatientName"].ToString() + "\",");
                            sbData.Append("\"sPatientContact\":\"" + ds.Rows[j]["sPatientContact"].ToString() + "\",");
                            sbData.Append("\"sPatientEmailId\":\"" + ds.Rows[j]["sPatientEmailId"].ToString() + "\",");
                            sbData.Append("\"sPatientAddress\":\"" + ds.Rows[j]["sPatientAddress"].ToString() + "\"");
                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;
        }
    }


    public int Update(string PatientName, string Contact, string Email, string Password,  string Gender, string DOB, string Address, string Checkdoctor, string Designation, string SpecialistIn,string Clinic)
    {
        try
        {



            string Query = "update [dbo].[appUser] set sFullName ='" + PatientName + "' , sEmailId ='" + Email + "' , sPassword = '" + Password + "'  , sDegree = '" + Designation + "'  , sSpecialization = '" + SpecialistIn + "'  , sClinic = '" + Clinic + "' where sMobile= '" + Contact + "' and sRole='doctor' ";

            
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

    public DataTable GetLogin(string contact, string Password, string usertype)
    {
        try
        {
            DataTable dt = new DataTable();
            string Query = "Select * from appUser where sMobile='" + contact + "' and sPassword='" + Password + "' and sRole = '"+ usertype + "'";
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


    public DataTable GetUserDetails(string struser)
    {
        try
        {

            DataTable ds = new DataTable();
            string Query = "Select * from patient where sPatientName ='" + struser + "'";
            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            sda = new SqlDataAdapter(scom);
            sda.Fill(ds);
            return ds;

        }
        catch (Exception ex)
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
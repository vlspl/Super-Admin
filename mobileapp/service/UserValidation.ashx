<%@ WebHandler Language="C#" Class="UserValidation" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class UserValidation : IHttpHandler,System.Web.SessionState.IRequiresSessionState
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

   
        switch (Action)
        {
            case "Register":

                string PatientCode = Convert.ToString(context.Request.QueryString["PatientCode"]);
                string PatientName = Convert.ToString(context.Request.QueryString["PatientName"]);
                int Age = Convert.ToInt32(context.Request.QueryString["Age"]);
                string Gender = Convert.ToString(context.Request.QueryString["Gender"]);
                DateTime DOB = Convert.ToDateTime(context.Request.QueryString["DOB"]);
                string Contact = Convert.ToString(context.Request.QueryString["Contact"]);
                string Address = Convert.ToString(context.Request.QueryString["Address"]);
                string Email = Convert.ToString(context.Request.QueryString["Email"]);
                string Password = Convert.ToString(context.Request.QueryString["Password"]);
                string Checkdoctor = Convert.ToString(context.Request.QueryString["Checkdoctor"]);
                
                string Designation = Convert.ToString(context.Request.QueryString["Designation"]);

                string SpecialIn = Convert.ToString(context.Request.QueryString["SpecialIn"]);

                int i = Register(PatientCode, PatientName, Age, Gender, DOB, Contact, Address, Email, Password, Checkdoctor, Designation, SpecialIn);
                context.Response.Write(i);

                break;
            case "Login":
                string Username = Convert.ToString(context.Request.QueryString["Username"]);
                Password = Convert.ToString(context.Request.QueryString["Password"]);
                 //context.Session["Username"] = Convert.ToString(context.Request.QueryString["Username"]);
                //string strsnml = context.Session["Username"].ToString();
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
                
            case"UserData":
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

    public int Register(string PatientCode, string PatientName, int Age, string Gender, DateTime DOB, string Contact, string Address, string Email, string Password, string Checkdoctor, string Designation, string SpecialistIn)
    {
        try
        {
            string date = DOB.ToString("dd/MM/yyyy hh:mm:ss tt");

            string Query = "INSERT INTO [dbo].[patient]"
           + "([sPatientCode],[sPatientName],[sPatientAge],[sPatientGender],[sPatientDOB],[sPatientContact],[sPatientAddress],[sPatientUserName],[sPatientPassword],[sPatientEmailId],[sCol1],[sCol2],[sCol3]) VALUES  "
           + "('" + PatientCode + "','" + PatientName + "','" + Age + "','" + Gender + "',Convert(datetime,'" + date + "',105),'" + Contact + "','" + Address + "','" + PatientName + "','" + Password + "','" + Email + "','" + Checkdoctor + "','" + Designation + "','" + SpecialistIn + "')";

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

    public DataTable GetLogin(string Username,string Password)
    {
        try
        {
            DataTable dt = new DataTable();
            string Query = "Select * from patient where sPatientName='" + Username + "' and sPatientPassword='" + Password + "'";
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
            scom = new SqlCommand(Query,scon);
            sda = new SqlDataAdapter(scom);
            sda.Fill(ds);
            return ds;
            
        }
        catch(Exception ex)
        {
            return null;
            throw;
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
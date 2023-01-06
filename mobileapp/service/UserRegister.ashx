<%@ WebHandler Language="C#" Class="UserRegister" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class UserRegister : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "Register":

               
                string PatientName = Convert.ToString(context.Request.QueryString["PatientName"]);
                string Contact = Convert.ToString(context.Request.QueryString["Contact"]);
                string Email = Convert.ToString(context.Request.QueryString["Email"]);
                string Password = Convert.ToString(context.Request.QueryString["Password"]);
                string Gender = Convert.ToString(context.Request.QueryString["Gender"]);
                string DOB = Convert.ToString(context.Request.QueryString["DOB"]).Replace("-","/");
               
                
                string Address = Convert.ToString(context.Request.QueryString["Address"]);
                
                
                string Checkdoctor = Convert.ToString(context.Request.QueryString["Checkdoctor"]);

                string Designation = Convert.ToString(context.Request.QueryString["Designation"]);

                string SpecialIn = Convert.ToString(context.Request.QueryString["SpecialIn"]);
                string Clinic = Convert.ToString(context.Request.QueryString["Clinic"]);
                string RegisteredId = Convert.ToString(context.Request.QueryString["Regdeviceids"]);
              //  string RegisteredId = "1";

                int i = Register(PatientName, Contact, Email, Password, Gender, DOB ,Address, Checkdoctor, Designation, SpecialIn, Clinic, RegisteredId);
                context.Response.Write(i);


                break;
            case "Login":
                string Username = Convert.ToString(context.Request.QueryString["Username"]);
                string usertypes = Convert.ToString(context.Request.QueryString["UserRole"]);
                string FullName = Convert.ToString(context.Request.QueryString["FullName"]);
                Password = Convert.ToString(context.Request.QueryString["Password"]);
               // context.Session["Username"] = Convert.ToString(context.Request.QueryString["Username"]);
              //  string strsnml = context.Session["Username"].ToString();
                DataTable dt = GetLogin(Username, Password, usertypes);
                if (dt != null && dt.Rows.Count > 0)
                {

                    sbData.Append("[{");
                    sbData.Append("\"sUsername\":\"" + dt.Rows[0]["sMobile"].ToString() + "\",");
                    
                    sbData.Append("\"sPassword\":\"" + dt.Rows[0]["sPassword"].ToString() + "\",");
                    sbData.Append("\"sEmailId\":\"" + dt.Rows[0]["sEmailId"].ToString() + "\",");

                    sbData.Append("\"sAppUserID\":\"" + dt.Rows[0]["sAppUserID"].ToString() + "\",");
                    sbData.Append("\"sDegree\":\"" + dt.Rows[0]["sDegree"].ToString() + "\",");
                    sbData.Append("\"sSpecialization\":\"" + dt.Rows[0]["sSpecialization"].ToString() + "\",");
                    sbData.Append("\"sClinic\":\"" + dt.Rows[0]["sClinic"].ToString() + "\",");
                    
                    
                    sbData.Append("\"sRole\":\"" + dt.Rows[0]["sRole"].ToString() + "\",");
                    sbData.Append("\"sFullName\":\"" + dt.Rows[0]["sFullName"].ToString() + "\",");
                    sbData.Append("\"sGender\":\"" + dt.Rows[0]["sGender"].ToString() + "\",");
                    sbData.Append("\"sBirthDate\":\"" + dt.Rows[0]["sBirthDate"].ToString() + "\",");
                    
                    
                    sbData.Append("\"sStatus\":\"Valid\"");
                    sbData.Append("}]");
                }
                else
                {
                    sbData.Append("[{\"sStatus\":\"Invalid Username or Password\"}]");

                }
                context.Response.Write(sbData.ToString());

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



            case "update":
                
                 string sPatientName = Convert.ToString(context.Request.QueryString["PatientName"]);
                string sContact = Convert.ToString(context.Request.QueryString["Contact"]);
                string sEmail = Convert.ToString(context.Request.QueryString["Email"]);
                string sPassword = Convert.ToString(context.Request.QueryString["Password"]);
                string sGender = Convert.ToString(context.Request.QueryString["Gender"]);
                string sDOB = Convert.ToString(context.Request.QueryString["DOB"].Replace('-','/'));
               //DateTime result = DateTime.ParseExact(sDOB, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
               //sDOB = result.ToShortDateString();
                
                string sAddress = Convert.ToString(context.Request.QueryString["Address"]);
                
                
                string sCheckdoctor = Convert.ToString(context.Request.QueryString["Checkdoctor"]);

                string sDesignation = Convert.ToString(context.Request.QueryString["Designation"]);

                string sSpecialIn = Convert.ToString(context.Request.QueryString["SpecialIn"]);
                string sClinic = Convert.ToString(context.Request.QueryString["Clinic"]);


                int k = Updatedata(sPatientName, sContact, sEmail, sPassword, sDOB, sGender, sCheckdoctor, sDesignation, sSpecialIn, sClinic);
                context.Response.Write(k);
                
                break;
        }
    }


    public int Register(string PatientName, string Contact, string Email, string Password, string Gender, string DOB, string Address, string Checkdoctor, string Designation, string SpecialistIn, string Clinic, string sRegisteredId)
    {
        try
        {



            string Query = "if not exists(select * from appUser where sMobile = '" + Contact + "') INSERT INTO [dbo].[appUser]"
          + "([sFullName],[sMobile],[sEmailId],[sPassword],[sGender],[sBirthDate],[sAddress],[sRole],[sDegree],[sSpecialization],[sClinic],[sDeviceToken]) OUTPUT INSERTED.sAppUserId    VALUES"
          + "('" + PatientName + "','" + Contact + "','" + Email + "','" + Password + "','" + Gender + "', '" + DOB + "','" + Address + "','" + Checkdoctor + "','" + Designation + "','" + SpecialistIn + "','" + Clinic + "','"+sRegisteredId+"')";

            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
             int i =(int)scom.ExecuteScalar();
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



    public int Updatedata(string PatientName, string Contact, string Email, string Password, string DOB, string Gender,string sRoles,string sDesignations,string sSpecialists,string sClinics)
    {
        try
        {

            string Query = "update [dbo].[appUser] set sFullName ='" + PatientName + "' , sMobile ='" + Contact + "' , sEmailId ='" + Email + "', sPassword ='" + Password + "' , sBirthDate ='" + DOB + "' , sGender = '" + Gender + "',sRole='" + sRoles + "',sDegree='" + sDesignations + "',sSpecialization='" + sSpecialists + "',sClinic='" + sClinics + "',sRegistered='1' OUTPUT INSERTED.sAppUserId where sMobile= '" + Contact + "'";

            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
         int k= (int)scom.ExecuteScalar();
            scon.Close();
            return k;
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
            string Query = "Select * from appUser where sMobile='" + contact + "' and sPassword='" + Password + "'";// and sRole = '"+ usertype + "'";
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
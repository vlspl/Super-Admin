<%@ WebHandler Language="C#" Class="PatientEditProfileNew" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientEditProfileNew : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                string DOB = Convert.ToString(context.Request.QueryString["DOB"].Replace("-", "/"));
               
                string Address = Convert.ToString(context.Request.QueryString["Address"]);
                
                
                string Checkdoctor = Convert.ToString(context.Request.QueryString["Checkdoctor"]);

                string Designation = Convert.ToString(context.Request.QueryString["Designation"]);

                string SpecialIn = Convert.ToString(context.Request.QueryString["SpecialIn"]);
                string Clinic = Convert.ToString(context.Request.QueryString["Clinic"]);



                string Country = Convert.ToString(context.Request.QueryString["selectCountry"]);
                string Pincode = Convert.ToString(context.Request.QueryString["pincode"]);
                string City = Convert.ToString(context.Request.QueryString["selectCity"]);
                string State = Convert.ToString(context.Request.QueryString["selectState"]);
                string sImagePath = Convert.ToString(context.Request.QueryString["sImagePath"]);

                int i = Update(PatientName, Contact, Email, Password, Gender, DOB, Address, Checkdoctor, Designation, SpecialIn, Clinic, Country, Pincode, City, State, sImagePath);
                context.Response.Write(i);

                break;
          

            case "UserData":
                string Usernames = Convert.ToString(context.Request.QueryString["Username"]);


                string UserID = Convert.ToString(context.Request.QueryString["PatientId"]);

                DataTable ds = GetUserDetails(UserID);
                if (ds != null && ds.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int j = 0; j < ds.Rows.Count; j++)
                    {
                        if (j == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sFullName"].ToString()) + "\",");
                            sbData.Append("\"sMobile\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sMobile"].ToString()) + "\",");
                            sbData.Append("\"sEmailId\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sEmailId"].ToString()) + "\",");
                            sbData.Append("\"sGender\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sGender"].ToString()) + "\",");
                            sbData.Append("\"sBirthDate\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sBirthDate"].ToString().Replace("/", "-")) + "\",");
                            sbData.Append("\"sAddress\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sAddress"].ToString()) + "\",");
                            sbData.Append("\"sRole\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sRole"].ToString()) + "\",");
                            sbData.Append("\"sDegree\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sDegree"].ToString()) + "\",");
                            sbData.Append("\"sSpecialization\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sSpecialization"].ToString()) + "\",");
                            sbData.Append("\"sClinic\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sClinic"].ToString()) + "\",");
                            sbData.Append("\"sCountry\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sCountry"].ToString()) + "\",");
                            sbData.Append("\"sPincode\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sPincode"].ToString()) + "\",");
                            sbData.Append("\"sCity\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sCity"].ToString()) + "\",");
                            sbData.Append("\"sState\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sState"].ToString()) + "\",");
                            sbData.Append("\"sPassword\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sPassword"].ToString()) + "\"");       
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sFullName"].ToString()) + "\",");
                            sbData.Append("\"sMobile\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sMobile"].ToString()) + "\",");
                            sbData.Append("\"sEmailId\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sEmailId"].ToString()) + "\",");
                            sbData.Append("\"sGender\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sGender"].ToString()) + "\",");
                            sbData.Append("\"sBirthDate\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sBirthDate"].ToString().Replace("/","-")) + "\",");
                            sbData.Append("\"sAddress\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sAddress"].ToString()) + "\",");
                            sbData.Append("\"sRole\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sRole"].ToString()) + "\",");
                            sbData.Append("\"sDegree\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sDegree"].ToString()) + "\",");
                            sbData.Append("\"sSpecialization\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sSpecialization"].ToString()) + "\",");
                            sbData.Append("\"sClinic\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sClinic"].ToString()) + "\",");
                            sbData.Append("\"sCountry\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sCountry"].ToString()) + "\",");
                            sbData.Append("\"sPincode\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sPincode"].ToString()) + "\",");
                            sbData.Append("\"sCity\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sCity"].ToString()) + "\",");
                            sbData.Append("\"sState\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sState"].ToString()) + "\",");
                            sbData.Append("\"sPassword\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sPassword"].ToString()) + "\"");           
                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;
        }
    }


    public int Update(string PatientName, string Contact, string Email, string Password, string Gender, string DOB, string Address, string Checkdoctor, string Designation, string SpecialistIn, string Clinic, string Country, string Pincode, string City, string State,string sImagePath)
    {
        try
        {



            string Query = "update [dbo].[appUser] set sFullName ='" + PatientName + "' , sEmailId ='" + Email + "' , sAddress ='" + Address + "' , sGender = '" + Gender + "', sBirthDate = '" + DOB + "' , sCountry = '" + Country + "', sPincode = '" + Pincode + "', sCity = '" + City + "' , sState = '" + State + "' ,sPassword = '" + Password + "' ,sImagePath = '" + sImagePath + "' where sMobile= '" + Contact + "' and sRole='" + Checkdoctor + "' ";

            
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


    public DataTable GetUserDetails(string UserID)
    {
        try
        {

            DataTable ds = new DataTable();
            string Query = "Select * from [appUser] where sAppUserId =" + UserID + "";
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
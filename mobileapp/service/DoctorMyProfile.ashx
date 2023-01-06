
<%@ WebHandler Language="C#" Class="DoctorMyProfile" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class DoctorMyProfile : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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


            case "GetTestList":
                string Usernames = Convert.ToString(context.Request.QueryString["UserID"]);

              //  string struser = context.Session["Username"].ToString();
                DataTable ds = GetUserDetails(Usernames);
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
                                sbData.Append("\"sBirthDate\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sBirthDate"].ToString()) + "\",");
                                sbData.Append("\"sAddress\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sAddress"].ToString()) + "\",");
                                sbData.Append("\"sRole\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sRole"].ToString()) + "\",");
                                sbData.Append("\"sDegree\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sDegree"].ToString()) + "\",");
                                sbData.Append("\"sSpecialization\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sSpecialization"].ToString()) + "\",");
                                sbData.Append("\"sClinic\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sClinic"].ToString()) + "\",");
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
                                sbData.Append("\"sBirthDate\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sBirthDate"].ToString()) + "\",");
                                sbData.Append("\"sAddress\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sAddress"].ToString()) + "\",");
                                sbData.Append("\"sRole\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sRole"].ToString()) + "\",");
                                sbData.Append("\"sDegree\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sDegree"].ToString()) + "\",");
                                sbData.Append("\"sSpecialization\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sSpecialization"].ToString()) + "\",");
                                sbData.Append("\"sClinic\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sClinic"].ToString()) + "\",");
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




    public DataTable GetUserDetails(string struser)
    {
        try
        {

            DataTable ds = new DataTable();
            string Query = "Select * from [appUser] where sAppUserId =" + struser + "";
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



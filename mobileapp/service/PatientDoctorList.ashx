<%@ WebHandler Language="C#" Class="PatientDoctorList" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;


public class PatientDoctorList : IHttpHandler {

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest (HttpContext context)
    {


        string Action = Convert.ToString(context.Request.QueryString["sAction"]);

        StringBuilder sbData = new StringBuilder();

        switch (Action)
        {
            case "GetPatientList":
                string DoctorIds = Convert.ToString(context.Request.QueryString["doctors"]);
                DataTable dt = GetPatientList(DoctorIds);

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
                            sbData.Append("\"sEmailId\":\"" + dt.Rows[i]["sEmailId"].ToString() + "\"");
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
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



    public DataTable GetPatientList(string DoctorId)
    {
        try
        {
            string Query = "select md.sMyDoctorId,au.sAppUserId,au.sFullName,au.sMobile,au.sEmailId  from myDoctors md join  appUser au on au.sAppuserId=md.sDoctorId where md.sDoctorId ='" + DoctorId + "'";
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
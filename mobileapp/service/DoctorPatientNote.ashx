<%@ WebHandler Language="C#" Class="DoctorPatientNote" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class DoctorPatientNote : IHttpHandler
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
            case "GetNoteList":
                string sShareReportId = Convert.ToString(context.Request.QueryString["sReportId"]);
                DataTable dt = GetNoteLists(sShareReportId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sDNSFRId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sDNSFRId"].ToString()) + "\",");

                            sbData.Append("\"sSharedReportId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sSharedReportId"].ToString()) + "\",");
                            sbData.Append("\"sNotes\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sNotes"].ToString()) + "\",");
                            sbData.Append("\"sCreatedAt\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sCreatedAt"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sDNSFRId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sDNSFRId"].ToString()) + "\",");

                            sbData.Append("\"sSharedReportId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sSharedReportId"].ToString()) + "\",");

                            sbData.Append("\"sNotes\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sNotes"].ToString()) + "\",");
                            sbData.Append("\"sCreatedAt\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sCreatedAt"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;

            case "InsertNoteData":

                string Notes = Convert.ToString(context.Request.QueryString["Note"]);
                string Reports = Convert.ToString(context.Request.QueryString["ReportId"]);
                string DateTimes = DateTime.Now.ToString();
                int j = AddNote(Notes, Reports, DateTimes);

               
                    context.Response.Write(j);
                  
              
                
              
                break;
        }
       
    }


    public DataTable GetNoteLists(string SharedReportID)
    {
        try
        {
            string Query = "select * from  DoctorNotesForSharedReport where sSharedReportId='" + SharedReportID + "' ";
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

    public int AddNote(string sNotes, string sReports,string sDates)
    {
        try
        {
            string Query = "Insert Into DoctorNotesForSharedReport " + "([sNotes],[sSharedReportId],[sCreatedAt]) VALUES" + "('" + sNotes + "','" + sReports + "','" + sDates + "')";
           
             scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            int j = scom.ExecuteNonQuery();
           
         
            
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
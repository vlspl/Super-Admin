<%@ WebHandler Language="C#" Class="PatientCheckStatus" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientCheckStatus : IHttpHandler
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
            case "GetLabData":

                string Patientid = Convert.ToString(context.Request.QueryString["Patientid"]);
                string Booklabid = Convert.ToString(context.Request.QueryString["Booklabid"]);
                string Labid = Convert.ToString(context.Request.QueryString["Labid"]);
                string Testid = Convert.ToString(context.Request.QueryString["Testid"]);

                DataTable dt = GetLabData(Patientid, Booklabid, Testid);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (i == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sLabName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabName"].ToString()) + "\",");
                            sbData.Append("\"slabaddress\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["slabaddress"].ToString()) + "\",");
                            sbData.Append("\"slabcontact\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["slabcontact"].ToString()) + "\",");
                            sbData.Append("\"sTimeSlot\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTimeSlot"].ToString()) + "\",");
                            sbData.Append("\"sTestDate\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestDate"].ToString().Replace("-","/")) + "\",");
                            sbData.Append("\"sbookstatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sbookstatus"].ToString()) + "\",");
                            sbData.Append("\"sPaymentstatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sPaymentstatus"].ToString()) + "\",");
                            sbData.Append("\"steststatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["steststatus"].ToString()) + "\",");
                            sbData.Append("\"sSentStatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["SentStatus"].ToString()) + "\",");
                            sbData.Append("\"sapprovalstatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sapprovalstatus"].ToString()) + "\",");
                            sbData.Append("\"sBookStatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sBookStatus"].ToString()) + "\"");
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sLabName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sLabName"].ToString()) + "\",");
                            sbData.Append("\"slabaddress\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["slabaddress"].ToString()) + "\",");
                            sbData.Append("\"slabcontact\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["slabcontact"].ToString()) + "\",");
                            sbData.Append("\"sTimeSlot\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTimeSlot"].ToString()) + "\",");
                            sbData.Append("\"sTestDate\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestDate"].ToString().Replace("-", "/")) + "\",");
                            sbData.Append("\"sbookstatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sbookstatus"].ToString()) + "\",");
                            sbData.Append("\"sPaymentstatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sPaymentstatus"].ToString()) + "\",");
                            sbData.Append("\"steststatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["steststatus"].ToString()) + "\",");
                            sbData.Append("\"sapprovalstatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sapprovalstatus"].ToString()) + "\",");
                            sbData.Append("\"sSentStatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["SentStatus"].ToString()) + "\",");
                            sbData.Append("\"sBookStatus\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sBookStatus"].ToString()) + "\"");
                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;
        }
    }


    public DataTable GetLabData(string Patientid, string Booklabid, string Testid)
    {
        try
        {
            string Query = @"select *,[bookLabTest].[sCol10] as SentStatus from [bookLab], [bookLabTest], [labMaster], [dbo].[test]
where [bookLab].sbooklabid = [bookLabTest].sbooklabid and [labMaster].slabid = [bookLab].slabid and [test].stestid = [bookLabTest].stestid
and [bookLab].sbooklabid = " + Booklabid + " and [bookLab].spatientid= " + Patientid + "and [bookLabTest].stestid =" + Testid + "";

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
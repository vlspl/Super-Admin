<%@ WebHandler Language="C#" Class="DoctorTestList" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class DoctorTestList : IHttpHandler {

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
            case "GetTestList":
                DataTable dt1 = GetDoctorListProfile();
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        var prnt = dt1.Rows[j]["sTestProfileId"].ToString();

                        if (j == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sProfileName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sProfileName"].ToString()) + "\"");
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sProfileName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[j]["sProfileName"].ToString()) + "\"");
                            sbData.Append("}");
                        }



                        DataTable dt = GetDoctorList(prnt);

                        if (dt != null && dt.Rows.Count > 0)
                        {

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (i == 0 && j == 0)
                                {
                                    sbData.Append(",{");

                                    sbData.Append("\"sTestProfileId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestProfileId"].ToString()) + "\",");
                                    sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");

                                    sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestId"].ToString()) + "\",");
                                    sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestName"].ToString()) + "\"");


                                    sbData.Append("}");
                                }
                                else
                                {
                                    sbData.Append(",{");

                                    sbData.Append("\"sTestProfileId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestProfileId"].ToString()) + "\",");
                                    sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");

                                    sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestId"].ToString()) + "\",");
                                    sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestName"].ToString()) + "\"");


                                    sbData.Append("}");
                                }
                            }

                        }

                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;
            case "InsertTest":
                    {
                        string doctorid=Convert.ToString(context.Request.QueryString["doctorid"]);
                        string patientid = Convert.ToString(context.Request.QueryString["patientid"]);
                            string testdata = Convert.ToString(context.Request.QueryString["selectTest"]);

                            
                        
                            string[] splitPatient = testdata.ToString().Split(',');

                            InserTestData(doctorid, patientid,splitPatient);

                        //foreach (string sdata in splitPatient)
                            //{
                            //    int k = InserTestData(doctorid, patientid, sdata);
                            //    context.Response.Write(k);
                            //}
                    }
                    break;
        }
    }

    

    public DataTable GetDoctorList(string prnt)
    {
        try
        {
            string Query = @"select * from test where sTestProfileId = " + prnt + " order by sTestCode ";
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


    public DataTable GetDoctorListProfile()
    {
        try
        {
            string Query = "Select * from [testProfile]  order by sProfileName";
            DataTable dt1 = new DataTable();
            scon = new SqlConnection(strcon);
            scom = new SqlCommand(Query, scon);
            sda = new SqlDataAdapter(scom);
            sda.Fill(dt1);
            return dt1;

        }
        catch (Exception)
        {
            return null;
        }


    }

    public void InserTestData(string doctor,string patient,string[] splitpatient )
    {
        try
        {
            string Query = "INSERT INTO [dbo].[recommendation]"
         + "(sDoctorId,sPatientId,sRecommendedAt)OUTPUT INSERTED.sRecommendationId  VALUES"
         + "('" + doctor + "','" + patient + "','"+DateTime.Now+"')";

          
            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            
            int k = (int)scom.ExecuteScalar();

            foreach (string patientid in splitpatient)
            {
                string Queryinsert = "INSERT INTO [dbo].[testRecommended]" + "([sRecommendationId],[sTestId]) VALUES" + "('" + k + "','" + patientid + "')";
                scom = new SqlCommand(Queryinsert, scon);
                scom.ExecuteNonQuery();
            }
            scon.Close();
       
        }
        catch (Exception ex)
        {
            scon.Close();

            throw;
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}
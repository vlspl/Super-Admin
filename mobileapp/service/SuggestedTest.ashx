<%@ WebHandler Language="C#" Class="SuggestedTest" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
public class SuggestedTest : IHttpHandler {

  
    public void ProcessRequest (HttpContext context) {
         
        string Action = Convert.ToString(context.Request["sAction"]);        
        string PatientID = Convert.ToString(context.Request["Patients"]);
        string recommendedTestsHtml = "";
        StringBuilder sbData = new StringBuilder();
        switch (Action)
        {
            case "GetRecommendedTest":

                DataSet dsRecommendation = getRecommendation(PatientID);
              
                    foreach (DataRow row in dsRecommendation.Tables[0].Rows)
                    {
                        string recommendationId = row["sRecommendationId"].ToString();
                        DataSet dsTestRecommended = getTestRecommended(recommendationId);
                        string testCode = "";
                        string testId = "";
                        int srno = 1;
                        string div = "<div class='panel panel-default'>" +
                                     "<div><span>" + dsTestRecommended.Tables[0].Rows[0]["sComment"] + "</span></div>" +
                                     "<div class='panel-heading'> <h4 class='panel-title'> <a class='accordion-toggle' data-toggle='collapse' data-parent='#myTests' href='#test" + dsTestRecommended.Tables[0].Rows[0]["sRecommendationId"] + "'><span class='srNo'>Recommended By : " + dsTestRecommended.Tables[0].Rows[0]["sDoctor"] + "</span></a> </h4></div>" +
                                     "<div><span class='left-content'>Recommended At : " + dsTestRecommended.Tables[0].Rows[0]["sRecommendedAt"] + "</span></div>";
                        srno++;
                        foreach (DataRow test  in dsTestRecommended.Tables[0].Rows)
                        {
                            testCode += test["sTestCode"].ToString() + ",";
                            testId += test["sTestId"].ToString() + ",";
                        }

                        div += "<div id='test" + dsTestRecommended.Tables[0].Rows[0]["sRecommendationId"] + "' class='panel-collapse collapse in'><div class='panel-body'><ul class='recommended-tests-list'><li><p><span>Recommended Tests : " + testCode.TrimEnd(',').TrimStart(',') + "</span></p></li><li><a href='PatientTestList.html?" + testId.TrimEnd(',').TrimStart(',') + "&&&" + dsTestRecommended.Tables[0].Rows[0]["sDoctorId"] + "&&&" + dsTestRecommended.Tables[0].Rows[0]["sLabId"] + "' class='app-btn-share'>Book Test</a> </li></ul></div></div>" +
                               "<div><input type='hidden' id='hiddenTestId' value='" + testId.TrimEnd(',').TrimStart(',') + "'></div>" +
                              
                               "</div>";

                        recommendedTestsHtml += div;
                    }
                
                break;

            case "GetRecommendedTestList":

                DataSet dsRecommendationlist = getRecommendation(PatientID);
                sbData.Append("[");
                int cnt = 0;
                foreach (DataRow row in dsRecommendationlist.Tables[0].Rows)
                {
                     cnt = cnt + 1;
                    string recommendationId = row["sRecommendationId"].ToString();
                    DataSet dsTestRecommended = getTestRecommended(recommendationId);
                    string testCode = "";
                    string testId = "";
                    string testNameDetails = "";
                    int srno = 1;
                    srno++;
                    foreach (DataRow test in dsTestRecommended.Tables[0].Rows)
                    {
                        testCode += "{\"testName\":\"" + test["sTestName"].ToString() + "\",\"sTestUsefulFor\":\"" + System.Text.RegularExpressions.Regex.Replace(test["sTestUsefulFor"].ToString(), @"\t|\n|\r", "") + "\"},";
                        testId += test["sTestId"].ToString() + ",";
                        testNameDetails += test["sTestName"].ToString() + "&";
                    }
                    
                    if (cnt == 1)
                    {
                        sbData.Append("{");
  			sbData.Append("\"sViewStatus\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sViewStatus"].ToString()) + "\",");
                        sbData.Append("\"sComment\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sComment"].ToString()) + "\",");
                        sbData.Append("\"sRecommendedAt\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sRecommendedAt"].ToString()) + "\",");
                        sbData.Append("\"sRecommendationId\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sRecommendationId"].ToString()) + "\",");
                        sbData.Append("\"sDoctor\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sDoctor"].ToString()) + "\",");
                        sbData.Append("\"sLabId\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sLabId"].ToString()) + "\",");
                        sbData.Append("\"sDoctorId\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sDoctorId"].ToString()) + "\",");
                        //sbData.Append("\"sTestUsefulFor\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sTestUsefulFor"].ToString()) + "\",");
                        sbData.Append("\"testNameDetails\":\"" + testNameDetails.TrimEnd('&').TrimStart('&') + "\",");
                        sbData.Append("\"testId\":\"" + testId.TrimEnd(',').TrimStart(',') + "\",");
                        sbData.Append("\"sTestName\": [" + testCode.TrimEnd(',').TrimStart(',') + "]");
                        
                        sbData.Append("}");
                    }
                    else 
                    {
                        sbData.Append(",{");
			sbData.Append("\"sViewStatus\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sViewStatus"].ToString()) + "\",");
                        sbData.Append("\"sComment\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sComment"].ToString()) + "\",");
                        sbData.Append("\"sRecommendedAt\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sRecommendedAt"].ToString()) + "\",");
                        sbData.Append("\"sRecommendationId\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sRecommendationId"].ToString()) + "\",");
                        sbData.Append("\"sDoctor\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sDoctor"].ToString()) + "\",");
                        sbData.Append("\"sLabId\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sLabId"].ToString()) + "\",");
                        sbData.Append("\"sDoctorId\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sDoctorId"].ToString()) + "\",");
                        //sbData.Append("\"sTestUsefulFor\":\"" + cGeneralHelper.JSONEscape(dsTestRecommended.Tables[0].Rows[0]["sTestUsefulFor"].ToString()) + "\",");
                        sbData.Append("\"testNameDetails\":\"" + testNameDetails.TrimEnd('&').TrimStart('&') + "\",");
                        sbData.Append("\"testId\":\"" + testId.TrimEnd(',').TrimStart(',') + "\",");
                        sbData.Append("\"sTestName\": [" + testCode.TrimEnd(',').TrimStart(',') + "]");
                        
                        sbData.Append("}");
                    }
                }
                sbData.Append("]");
                context.Response.Write(sbData.ToString());
                break;
        }


        context.Response.ContentType = "text/plain";
        context.Response.Write(recommendedTestsHtml);
    }
 
    
    
    public DataSet getRecommendation(string patientId)
    {
        string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        DataSet ds = new DataSet();
        
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();
            
            string query = "select * from recommendation where sPatientId='"+patientId+"'";
            
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds);
                }
            }
        }
        return ds;  
    }

    public DataSet getTestRecommended(string recommendationId)
    {
        string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        DataSet ds = new DataSet();

        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string query = "select r.sRecommendationId,r.sViewStatus ,r.sRecommendedAt,r.sComment,r.sDoctorId, au.sAppUserId,au.sFullName as sDoctor, tr.sTestId,tr.sLabId, t.sTestUsefulFor, t.sTestCode,t.sTestName from recommendation r join testRecommended tr on r.sRecommendationId=tr.sRecommendationId join appUser au on au.sAppUserId=r.sDoctorId join test t on tr.sTestId=t.sTestId where r.sRecommendationId='" + recommendationId + "'";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds);
                }
            }
        }
        return ds;
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
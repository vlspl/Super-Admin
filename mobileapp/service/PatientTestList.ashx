<%@ WebHandler Language="C#" Class="PatientTestList" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class PatientTestList : IHttpHandler {

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
            case "GetTestList":
                DataTable dt1 = GetPatientListProfile();
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
                         
                         
                         
                            DataTable dt = GetPatientList(prnt);

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


              
            case "GetTestListing":
                int sSectionId = Convert.ToInt32(context.Request.QueryString["SectionId"]);
                DataTable dt2 = GetPatientListProfileData(sSectionId);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    //   sbData.Append("[");
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        var prnt = dt2.Rows[j]["sTestProfileId"].ToString();
                        string cnt = j.ToString();
                        if (j == 0)
                        {
                            DataTable dt3 = GetPatientList(prnt);
                            if (dt3.Rows.Count != 0)
                            {

                                sbData.Append("[");
                                sbData.Append("{");
                                sbData.Append("\"TestProfile\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[j]["sProfileName"].ToString()) + "\",");
                                sbData.Append("\"TestProfileid\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[j]["sTestProfileId"].ToString()) + "\",");

                                if (dt3 != null && dt3.Rows.Count > 0)
                                {

                                    for (int i = 0; i < dt3.Rows.Count; i++)
                                    {
                                        if (i == 0)
                                        {
                                            sbData.Append("\"SubTestProfile\":[{\"sTestProfileId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestProfileId"].ToString()) + "\",");
                                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestCode"].ToString()) + "\",");

                                            sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestId"].ToString()) + "\",");
                                            sbData.Append("\"sTestUsefulFor\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestUsefulFor"].ToString()) + "\",");
                                            sbData.Append("\"TestProfileid\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[j]["sTestProfileId"].ToString()) + "\",");
                                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestName"].ToString()) + "\",");
                                            sbData.Append("\"TestProfile\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestName"].ToString()) + "\",");
                                            sbData.Append("\"SubTestProfile\":[]");

                                            sbData.Append("}");

                                        }
                                        else
                                        {
                                            sbData.Append(",{");

                                            sbData.Append("\"sTestProfileId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestProfileId"].ToString()) + "\",");
                                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestCode"].ToString()) + "\",");

                                            sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestId"].ToString()) + "\",");
                                            sbData.Append("\"sTestUsefulFor\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestUsefulFor"].ToString()) + "\",");
                                            sbData.Append("\"TestProfileid\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[j]["sTestProfileId"].ToString()) + "\",");
                                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestName"].ToString()) + "\",");
                                            sbData.Append("\"TestProfile\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestName"].ToString()) + "\",");
                                            sbData.Append("\"SubTestProfile\":[]");

                                            sbData.Append("}");

                                        }
                                    }
                                    sbData.Append("]");
                                    sbData.Append("}");
                                }

                            }
                            //sbData.Append("}");
                        }
                        else
                        {
                            DataTable dt3 = GetPatientList(prnt);
                            if (dt3.Rows.Count != 0)
                            {

                                sbData.Append(",{");
                                sbData.Append("\"TestProfile\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[j]["sProfileName"].ToString()) + "\",");
                                sbData.Append("\"TestProfileid\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[j]["sTestProfileId"].ToString()) + "\",");
                                if (dt3 != null && dt3.Rows.Count > 0)
                                {

                                    for (int i = 0; i < dt3.Rows.Count; i++)
                                    {
                                        if (i == 0)
                                        {
                                            sbData.Append("\"SubTestProfile\":[{\"sTestProfileId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestProfileId"].ToString()) + "\",");
                                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestCode"].ToString()) + "\",");

                                            sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestId"].ToString()) + "\",");
                                            sbData.Append("\"sTestUsefulFor\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestUsefulFor"].ToString()) + "\",");
                                            sbData.Append("\"TestProfileid\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[j]["sTestProfileId"].ToString()) + "\",");
                                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestName"].ToString()) + "\",");
                                            sbData.Append("\"TestProfile\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestName"].ToString()) + "\",");
                                            sbData.Append("\"SubTestProfile\":[]");
                                            sbData.Append("}");

                                        }
                                        else
                                        {
                                            sbData.Append(",{");

                                            sbData.Append("\"sTestProfileId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestProfileId"].ToString()) + "\",");
                                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestCode"].ToString()) + "\",");

                                            sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestId"].ToString()) + "\",");
                                            sbData.Append("\"sTestUsefulFor\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestUsefulFor"].ToString()) + "\",");
                                            sbData.Append("\"TestProfileid\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[j]["sTestProfileId"].ToString()) + "\",");
                                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestName"].ToString()) + "\",");
                                            sbData.Append("\"TestProfile\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestName"].ToString()) + "\",");
                                            sbData.Append("\"SubTestProfile\":[]");
                                            sbData.Append("}");

                                        }
                                    }

                                }
                                sbData.Append("]");
                                sbData.Append("}");

                            }
                        }

                    }
                      sbData.Append("]");
                }
               // context.Response.Write("[{\"TestProfile\":\"HTLV  Associated Lymphoma\",\"TestProfileid\":\"1\",\"SubTestProfile\":[{\"sTestProfileId\":\"1\",\"sTestCode\":\"ASDF\",\"sTestId\":\"475\",\"sTestName\":\"aSDF\"},{\"sTestProfileId\":\"1\",\"sTestCode\":\"ASDF\",\"sTestId\":\"475\",\"sTestName\":\"aSDF\"}]},{\"TestProfile\":\"HTLV  Associated Lymphoma\",\"TestProfileid\":\"2\",\"SubTestProfile\":[{\"sTestProfileId\":\"2\",\"sTestCode\":\"ASDF\",\"sTestId\":\"475\",\"sTestName\":\"aSDF\"},{\"sTestProfileId\":\"2\",\"sTestCode\":\"ASDF\",\"sTestId\":\"475\",\"sTestName\":\"aSDF\"}]}]");
                context.Response.Write(sbData.ToString()); 
               break;
                
                
                //All Test Search Start
            case "ALLTestSearch":
               string TestSearch = context.Request.QueryString["TestSearch"].ToString();
               DataTable dt6 = AllTestSearch(TestSearch);
               if (dt6 != null && dt6.Rows.Count > 0)
               {
                   //   sbData.Append("[");
                   for (int j = 0; j < dt6.Rows.Count; j++)
                   {
                       var prnt = dt6.Rows[j]["sTestProfileId"].ToString();

                       if (j == 0)
                       {
                           sbData.Append("[");
                           sbData.Append("{");
                           DataTable dt3 = GetPatientList(prnt);
                           sbData.Append("\"TestProfile\":\"" + cGeneralHelper.JSONEscape(dt6.Rows[j]["sProfileName"].ToString()) + "\",");
                           sbData.Append("\"sTestProfileid\":\"" + cGeneralHelper.JSONEscape(dt6.Rows[j]["sTestId"].ToString()) + "\",");
                           if (dt3 != null && dt3.Rows.Count > 0)
                           {

                               for (int i = 0; i < dt3.Rows.Count; i++)
                               {
                                   if (i == 0)
                                   {
                                       sbData.Append("\"SubTestProfile\":[{\"sTestProfileId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestProfileId"].ToString()) + "\",");
                                       sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestCode"].ToString()) + "\",");

                                       sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestId"].ToString()) + "\",");
                                       sbData.Append("\"sTestUsefulFor\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestUsefulFor"].ToString()) + "\",");
                                       sbData.Append("\"TestProfileid\":\"" + cGeneralHelper.JSONEscape(dt6.Rows[j]["sTestId"].ToString()) + "\",");
                                       sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestName"].ToString()) + "\"");


                                       sbData.Append("}");

                                   }
                                   else
                                   {
                                       sbData.Append(",{");

                                       sbData.Append("\"sTestProfileId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestProfileId"].ToString()) + "\",");
                                       sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestCode"].ToString()) + "\",");

                                       sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestId"].ToString()) + "\",");
                                       sbData.Append("\"sTestUsefulFor\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestUsefulFor"].ToString()) + "\",");
                                       sbData.Append("\"TestProfileid\":\"" + cGeneralHelper.JSONEscape(dt6.Rows[j]["sTestId"].ToString()) + "\",");
                                       sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestName"].ToString()) + "\"");


                                       sbData.Append("}");

                                   }
                               }
                               sbData.Append("]");
                               sbData.Append("}");
                           }


                           //sbData.Append("}");
                       }
                       else
                       {
                           sbData.Append(",{");
                           sbData.Append("\"TestProfile\":\"" + cGeneralHelper.JSONEscape(dt6.Rows[j]["sProfileName"].ToString()) + "\",");
                           sbData.Append("\"sTestProfileid\":\"" + cGeneralHelper.JSONEscape(dt6.Rows[j]["sTestId"].ToString()) + "\",");
                           DataTable dt3 = GetPatientList(prnt);
                           if (dt3 != null && dt3.Rows.Count > 0)
                           {

                               for (int i = 0; i < dt3.Rows.Count; i++)
                               {
                                   if (i == 0)
                                   {
                                       sbData.Append("\"SubTestProfile\":[{\"sTestProfileId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestProfileId"].ToString()) + "\",");
                                       sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestCode"].ToString()) + "\",");

                                       sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestId"].ToString()) + "\",");
                                       sbData.Append("\"sTestUsefulFor\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestUsefulFor"].ToString()) + "\",");
                                       sbData.Append("\"TestProfileid\":\"" + cGeneralHelper.JSONEscape(dt6.Rows[j]["sTestId"].ToString()) + "\",");
                                       sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestName"].ToString()) + "\"");


                                       sbData.Append("}");

                                   }
                                   else
                                   {
                                       sbData.Append(",{");

                                       sbData.Append("\"sTestProfileId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestProfileId"].ToString()) + "\",");
                                       sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestCode"].ToString()) + "\",");

                                       sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestId"].ToString()) + "\",");
                                       sbData.Append("\"sTestUsefulFor\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestUsefulFor"].ToString()) + "\",");
                                       sbData.Append("\"TestProfileid\":\"" + cGeneralHelper.JSONEscape(dt6.Rows[j]["sTestId"].ToString()) + "\",");
                                       sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt3.Rows[i]["sTestName"].ToString()) + "\"");


                                       sbData.Append("}");

                                   }
                               }

                           }
                           sbData.Append("]");
                           sbData.Append("}");


                       }

                   }
                   sbData.Append("]");
               }
               context.Response.Write(sbData.ToString());
               break;
                
                
                
                

                case "GetSection":
                DataTable dt4 = GetSection();

                if (dt4 != null && dt4.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sSectionId\":\"" + cGeneralHelper.JSONEscape(dt4.Rows[i]["sSectionId"].ToString()) + "\",");
                            sbData.Append("\"sSectionName\":\"" + cGeneralHelper.JSONEscape(dt4.Rows[i]["sSectionName"].ToString()) + "\"");
                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sSectionId\":\"" + cGeneralHelper.JSONEscape(dt4.Rows[i]["sSectionId"].ToString()) + "\",");
                            sbData.Append("\"sSectionName\":\"" + cGeneralHelper.JSONEscape(dt4.Rows[i]["sSectionName"].ToString()) + "\"");
                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;   
        }

    }


    public DataTable GetPatientList(string prnt)
    {
        try
        {
           // string Query = @"select top 10 * from test where sTestProfileId = " + prnt + "order by sTestCode";
            string Query = @"select * from test where sTestProfileId =" + prnt + " ";
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



    public DataTable GetPatientListProfile()
    {
        try
        {
            string Query = "Select * from [testProfile] order by sProfileName ";
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


    public DataTable GetPatientListProfileData(int sSectionId)
    {
        try
        {
           // string Query = "select * from section s inner join testProfile t on s.sSectionId = t.sSectionId inner join test tst on t.sTestProfileId  = tst.sTestProfileId where s.sSectionId ="+ sSectionId +"";

            string Query = "select * from testProfile where sSectionId=" + sSectionId + "";
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


    public DataTable AllTestSearch(string TestSearch)
    {
        try
        {
            string Query = "select * from section s inner join testProfile t on s.sSectionId = t.sSectionId inner join test tst on t.sTestProfileId  = tst.sTestProfileId  where tst.sTestName like  '%"+TestSearch+"%' or tst.sTestCode like '%"+TestSearch+"%'";
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
    

    public DataTable GetSection()
    {
        try
        {
            string Query = "select * from section";
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


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="getsearchtestforbooktest" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.SessionState;

public class getsearchtestforbooktest : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";

        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();



        switch (Action)
        {
            case "GetLabData":

                string inputtext = Convert.ToString(context.Request.QueryString["inputtext"]);
                string LabID = Convert.ToString(HttpContext.Current.Request.Cookies["labId"].Value);

                DataTable dt = GetLabData(LabID, inputtext);

                if (dt != null && dt.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (i == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestId"].ToString()) + "\",");
                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestName"].ToString().Replace("-", "/")) + "\",");
                            sbData.Append("\"sProfileName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sProfileName"].ToString()) + "\",");
                            //sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");
                            sbData.Append("\"sSectionName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sSectionName"].ToString()) + "\",");
                            sbData.Append("\"sPrice\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sPrice"].ToString()) + "\",");
                            sbData.Append("\"sMyTest\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMyTest"].ToString()) + "\",");




                            sbData.Append("\"sMyTest\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMyTest"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestId"].ToString()) + "\",");
                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sTestName"].ToString().Replace("-", "/")) + "\",");
                            sbData.Append("\"sProfileName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sProfileName"].ToString()) + "\",");
                            //sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sFullName"].ToString()) + "\",");
                            sbData.Append("\"sSectionName\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sSectionName"].ToString()) + "\",");
                            sbData.Append("\"sPrice\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sPrice"].ToString()) + "\",");
                            sbData.Append("\"sMyTest\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMyTest"].ToString()) + "\",");



                            sbData.Append("\"sMyTest\":\"" + cGeneralHelper.JSONEscape(dt.Rows[i]["sMyTest"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;




            case "GettestprofileData":

                string inputtxt = Convert.ToString(context.Request.QueryString["inputtext"]);
                string LabIDd = Convert.ToString(HttpContext.Current.Request.Cookies["labId"].Value);
                string section = Convert.ToString(context.Request.QueryString["sectionid"]);
                DataTable dt1 = GettestprofileData(LabIDd, inputtxt, section);

                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {

                        if (i == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"stestprofileid\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["stestprofileid"].ToString()) + "\",");
                            sbData.Append("\"sProfileName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sProfileName"].ToString()) + "\",");
                            //sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sTestName"].ToString().Replace("-", "/")) + "\",");
                            //sbData.Append("\"sProfileName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sProfileName"].ToString()) + "\",");
                            ////sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sFullName"].ToString()) + "\",");
                            //sbData.Append("\"sSectionName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sSectionName"].ToString()) + "\",");
                            //sbData.Append("\"sPrice\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sPrice"].ToString()) + "\",");
                            //sbData.Append("\"sMyTest\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sMyTest"].ToString()) + "\",");
                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sTestName"].ToString()) + "\",");



                            sbData.Append("\"sProfileName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sProfileName"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"stestprofileid\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["stestprofileid"].ToString()) + "\",");
                            sbData.Append("\"sProfileName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sProfileName"].ToString()) + "\",");
                            //sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sTestName"].ToString().Replace("-", "/")) + "\",");
                            //sbData.Append("\"sProfileName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sProfileName"].ToString()) + "\",");
                            ////sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sFullName"].ToString()) + "\",");
                            //sbData.Append("\"sSectionName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sSectionName"].ToString()) + "\",");
                            //sbData.Append("\"sPrice\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sPrice"].ToString()) + "\",");
                            //sbData.Append("\"sMyTest\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sMyTest"].ToString()) + "\",");
                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sTestName"].ToString()) + "\",");


                            sbData.Append("\"sProfileName\":\"" + cGeneralHelper.JSONEscape(dt1.Rows[i]["sProfileName"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;


                
                
                

            case "GetTestdetailsfromsectionid":

                string Sectionid = Convert.ToString(context.Request.QueryString["Profileid"]);
                string LabIDs = Convert.ToString(HttpContext.Current.Request.Cookies["labId"].Value);

                DataTable dt2 = Gettestlistbysection(LabIDs, Sectionid);

                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {

                        if (i == 0)
                        {
                            sbData.Append("{");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sTestId"].ToString()) + "\",");
                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sTestName"].ToString().Replace("-", "/")) + "\",");
                            sbData.Append("\"sProfileName\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sProfileName"].ToString()) + "\",");
                            //sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sFullName"].ToString()) + "\",");
                            sbData.Append("\"sSectionName\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sSectionName"].ToString()) + "\",");
                            sbData.Append("\"sPrice\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sPrice"].ToString()) + "\",");
                            sbData.Append("\"sMyTest\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sMyTest"].ToString()) + "\",");




                            sbData.Append("\"stestprofileid\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["stestprofileid"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sTestId\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sTestId"].ToString()) + "\",");
                            sbData.Append("\"sTestName\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sTestName"].ToString().Replace("-", "/")) + "\",");
                            sbData.Append("\"sProfileName\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sProfileName"].ToString()) + "\",");
                            //sbData.Append("\"sFullName\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sFullName"].ToString()) + "\",");
                            sbData.Append("\"sSectionName\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sSectionName"].ToString()) + "\",");
                            sbData.Append("\"sPrice\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sPrice"].ToString()) + "\",");
                            sbData.Append("\"sMyTest\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["sMyTest"].ToString()) + "\",");



                            sbData.Append("\"stestprofileid\":\"" + cGeneralHelper.JSONEscape(dt2.Rows[i]["stestprofileid"].ToString()) + "\"");

                            sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;
                
        }
    }


    public DataTable GettestprofileData(string labId, string txtsearch, string section)
    {
        try
        {
            string Query = "";
            //if (section != "0")
            //{

                Query = @"select distinct( p.stestprofileid), p.sProfileName, sTestName from test t join testprofile p on t.sTestProfileId=p.sTestProfileId join section s 
            on s.sSectionId=p.sSectionId join testLab tl on tl.sTestId=t.sTestId where tl.sLabId='" + labId + @" ' 
            and t.sTestName like '%" + txtsearch + "%' and s.sSectionId = '" + section + "'";
//            }
//            else
//            {

//                Query = @"select distinct( p.stestprofileid), p.sProfileName, sTestName from test t join testprofile p on t.sTestProfileId=p.sTestProfileId join section s 
//            on s.sSectionId=p.sSectionId join testLab tl on tl.sTestId=t.sTestId where tl.sLabId='" + labId + @"' 
//            and t.sTestName like '%" + txtsearch + "%'";  
//            }
            
            
           

//select t.sTestId,t.sTestCode, t.sTestName, p.sProfileName, s.sSectionName, 
//tl.sPrice,tl.sMyTest from test t join testprofile p on t.sTestProfileId=p.sTestProfileId join section s 
//on s.sSectionId=p.sSectionId join testLab tl on tl.sTestId=t.sTestId where tl.sLabId='" + labId + @"' 
//            and t.sTestCode like '%" + txtsearch + "%' and tl.sPrice <> '0' order by s.sSectionName";

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



    public DataTable GetLabData(string labId, string txtsearch)
    {
        try
        {
            string Query = @"select t.sTestId,t.sTestCode, t.sTestName, p.sProfileName, s.sSectionName, 
            tl.sPrice,tl.sMyTest from test t join testprofile p on t.sTestProfileId=p.sTestProfileId join section s 
            on s.sSectionId=p.sSectionId join testLab tl on tl.sTestId=t.sTestId where tl.sLabId='" + labId + @"' 
                        and t.sTestCode like '%" + txtsearch + "%' and tl.sPrice <> '0' order by s.sSectionName";

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



    public DataTable Gettestlistbysection(string labId, string tstprofileid)
    {
        try
        {
//            string Query = @"select p.stestprofileid, t.sTestId,t.sTestCode, t.sTestName, p.stestprofileid, p.sProfileName, s.sSectionName, 
//tl.sPrice,tl.sMyTest from test t join testprofile p on t.sTestProfileId=p.sTestProfileId join section s 
//on s.sSectionId=p.sSectionId join testLab tl on tl.sTestId=t.sTestId where tl.sLabId='" + labId + @"' 
//            and p.stestprofileid = '"+tstprofileid+"' and tl.sPrice <> '0'";


            string Query = @"select p.stestprofileid, t.sTestId,t.sTestCode, t.sTestName, p.stestprofileid, p.sProfileName, s.sSectionName, 
tl.sPrice,tl.sMyTest from test t join testprofile p on t.sTestProfileId=p.sTestProfileId join section s 
on s.sSectionId=p.sSectionId join testLab tl on tl.sTestId=t.sTestId where tl.sLabId='" + labId + @"' 
            and p.stestprofileid = '" + tstprofileid + "'";
                
            //select t.sTestId,t.sTestCode, t.sTestName, p.sProfileName, s.sSectionName, 
            //tl.sPrice,tl.sMyTest from test t join testprofile p on t.sTestProfileId=p.sTestProfileId join section s 
            //on s.sSectionId=p.sSectionId join testLab tl on tl.sTestId=t.sTestId where tl.sLabId='" + labId + @"' 
            //            and t.sTestCode like '%" + txtsearch + "%' and tl.sPrice <> '0' order by s.sSectionName";

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
    
    
    
    
    
    
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
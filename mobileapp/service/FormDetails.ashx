<%@ WebHandler Language="C#" Class="FormDetails" %>

using System;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Configuration;

using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Script.Serialization;

using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Web.Configuration;
using System.Globalization;
using System.Threading;

public class FormDetails : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string sAction = context.Request.QueryString["sAction"].ToString();
        CultureInfo en = new CultureInfo("es-ES");
        Thread.CurrentThread.CurrentCulture = en;
        string con = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        switch (sAction)
        {

            case "LoadState":
                {

                    int iID = Convert.ToInt32(context.Request.QueryString["iID"].ToString());
                    DataTable dt;
                    StringBuilder sbData = new StringBuilder();
                    using (SqlConnection conn = new SqlConnection(con))
                    {
                        //using (SqlCommand cmd = new SqlCommand("cState_Find", conn))
                        using (SqlCommand cmd = new SqlCommand("cFormState_Find", conn))
                        {
                            dt = new DataTable();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@sSelect", SqlDbType.VarChar).Value = "*";
                            cmd.Parameters.Add("@sFilter", SqlDbType.VarChar).Value = "where IsState=1";
                            cmd.Parameters.Add("@iCurrentUserID", SqlDbType.VarChar).Value = 1;

                            conn.Open();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt);
                            conn.Close();
                        }
                    }

                    sbData.Append("{\"clists\":[");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i > 0) sbData.Append(',');

                        sbData.Append("{\"iID\":" + dt.Rows[i]["iID"].ToString() + ", \"sName\":\"" + dt.Rows[i]["sName"].ToString() + "\" }");
                    }
                    sbData.Append("]}");
                    context.Response.Write(sbData);
                }
                break;

            case "populateCityAccToState":
                {

                    int stateId = Convert.ToInt32(context.Request.QueryString["stateId"]);
                    DataTable dtCity = new DataTable();
                    StringBuilder sbData = new StringBuilder();
                    using (SqlConnection conn = new SqlConnection(con))
                    {
                        //using (SqlCommand cmd = new SqlCommand("cCity_Find", conn))
                        using (SqlCommand cmd = new SqlCommand("cFormCity_Find", conn))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@sSelect", SqlDbType.VarChar).Value = "*";
                            cmd.Parameters.Add("@sFilter", SqlDbType.VarChar).Value = "where objFormState = " + stateId;
                            cmd.Parameters.Add("@iCurrentUserID", SqlDbType.VarChar).Value = 1;

                            conn.Open();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dtCity);
                            conn.Close();
                        }
                    }
                    if (dtCity != null && dtCity.Rows.Count > 0)
                    {
                        sbData.Append("{\"clists\":[");
                        for (int i = 0; i < dtCity.Rows.Count; i++)
                        {
                            if (i > 0)
                                sbData.Append(",");
                            string cityName = Convert.ToString(dtCity.Rows[i]["sName"]);

                            string sId = Convert.ToString(dtCity.Rows[i]["iID"]);

                            sbData.Append("{\"cId\":\"" + sId + "\",");
                            sbData.Append("\"cityN\":\"" + cityName + "\"}");

                        }
                        sbData.Append("]}");
                    }

                    context.Response.Write(sbData);
                }
                break;

            case "mnsRegistrationForm":
                {                    
                    string sName = context.Request.QueryString["sName"].ToString();
                    string iMobileNo = context.Request.QueryString["iMobileNo"].ToString();
                    string sEmailId = context.Request.QueryString["sEmailId"].ToString();
                    //int iState = Convert.ToInt32(context.Request.QueryString["iState"].ToString());
                    //int iCity = Convert.ToInt32(context.Request.QueryString["iCity"].ToString());
                    //string sstate = context.Request.QueryString["sstate"];
                    //string ccity = context.Request.QueryString["ccity"];
                    string iId;
                    
                    string response = "response";
                    using (SqlConnection conn = new SqlConnection(con))
                    {
                        using (SqlCommand cmd = new SqlCommand("cUser_Create", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@sName", SqlDbType.VarChar).Value = sName;
                            cmd.Parameters.Add("@iCreatedBy", SqlDbType.VarChar).Value = "1";
                            cmd.Parameters.Add("@sEmailId", SqlDbType.VarChar).Value = sEmailId;
                            cmd.Parameters.Add("@sMobileNo", SqlDbType.VarChar).Value = iMobileNo;
                            //cmd.Parameters.Add("@objState", SqlDbType.Int).Value = iState;
                            //cmd.Parameters.Add("@objCity", SqlDbType.Int).Value = iCity;

                            
                            conn.Open();
                            try
                            {
                                object o = cmd.ExecuteScalar();
                                iId = o.ToString();
                                response = iId;
                            }
                            catch (Exception ex)
                            {
                                response = "Failure";
                            }
                            conn.Close();

                            context.Response.Write(response);
                        }
                    }

                }
                break;

            case "mnsFullRegistrationForm":
                {
                    string sName = context.Request.QueryString["sName"].ToString();
                    string iMobileNo = context.Request.QueryString["iMobileNo"].ToString();
                    string sEmailId = context.Request.QueryString["sEmailId"].ToString();
                    int objUser = Convert.ToInt32(context.Request.QueryString["objUser"].ToString());
                    string sGender = context.Request.QueryString["sGender"].ToString();
                    int iState = Convert.ToInt32(context.Request.QueryString["iState"].ToString());
                    int iCity = Convert.ToInt32(context.Request.QueryString["iCity"].ToString());
                    //string sstate = context.Request.QueryString["sstate"];
                    //string ccity = context.Request.QueryString["ccity"];
                    string sPincode = context.Request.QueryString["sPincode"].ToString();
                    string sAddress = context.Request.QueryString["sAddress"].ToString();
                    string sEducation = context.Request.QueryString["sEducation"].ToString();
                    string sProfession = context.Request.QueryString["sProfession"].ToString();                    
                    
                    string iId;

                    string response = "response";
                    using (SqlConnection conn = new SqlConnection(con))
                    {
                        using (SqlCommand cmd = new SqlCommand("cUserDetails_Create", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@sName", SqlDbType.VarChar).Value = sName;
                            cmd.Parameters.Add("@sEmailId", SqlDbType.VarChar).Value = sEmailId;
                            cmd.Parameters.Add("@sMobileNo", SqlDbType.VarChar).Value = iMobileNo;
                            cmd.Parameters.Add("@objUser", SqlDbType.Int).Value = objUser;
                            cmd.Parameters.Add("@sGender", SqlDbType.VarChar).Value = sGender;
                            cmd.Parameters.Add("@objState", SqlDbType.Int).Value = iState;
                            cmd.Parameters.Add("@objCity", SqlDbType.Int).Value = iCity;
                            cmd.Parameters.Add("@sPincode", SqlDbType.VarChar).Value = sPincode;
                            cmd.Parameters.Add("@sAddress", SqlDbType.VarChar).Value = sAddress;
                            cmd.Parameters.Add("@sEducation", SqlDbType.VarChar).Value = sEducation;
                            cmd.Parameters.Add("@sProfession", SqlDbType.VarChar).Value = sProfession;

                            conn.Open();
                            try
                            {
                                object o = cmd.ExecuteScalar();
                                iId = o.ToString();
                                response = iId;
                            }
                            catch (Exception ex)
                            {
                                response = "Failure";
                            }
                            conn.Close();

                            context.Response.Write(response);
                        }
                    }

                }
                break;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
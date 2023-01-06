<%@ WebHandler Language="C#" Class="ValidUserRegister" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections.Generic;

public class ValidUserRegister : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest (HttpContext context)
    {
        string Mobile = Convert.ToString(context.Request.QueryString["mobile"]);
        StringBuilder sbData = new StringBuilder();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        System.Collections.Generic.Dictionary<string, object> response = checkUserExist(Mobile);
        context.Response.ContentType = "application/json";//"text/plain";
       context.Response.Write(serializer.Serialize(response));
                
    }


   
    public Dictionary<string, object> checkUserExist(string mobile)
    {
        System.Collections.Generic.Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;

        try
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                conn.Open();
                
                //check if patient already added to lab
                string query = "select * from appUser where sRegistered=1 and sMobile='" + mobile + "'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            returnType = 1;
                            returnData.Add("key", returnType);
                            returnData.Add("value", null);
                            return returnData;
                        }
                    }
                }

                //check if patient exists in appUser but not added to lab
                query = "select * from appUser where sMobile='" + mobile + "'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        DataSet ds = new DataSet();
                        da.SelectCommand = cmd;
                        da.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                            Dictionary<string, object> row;

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in ds.Tables[0].Columns)
                                {
                                    row.Add(col.ColumnName, dr[col]);
                                }
                                rows.Add(row);
                            }

                            returnType = 2;
                            returnData.Add("key", returnType);
                            returnData.Add("value", rows);
                            return returnData;
                        }
                    }
                }

            }

            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
        catch (Exception ex)
        {
            returnType = -1;
            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
    }

   
    /// </summary>
    /// <param name="struser"></param>
    /// <returns></returns>


   
    public bool IsReusable {
        get {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="PatientHealthViewHistory" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Collections;
using System.Configuration;
using System.IO;

public class PatientHealthViewHistory : IHttpHandler
{


    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;

    Dictionary<string, object> returnData = new Dictionary<string, object>();
    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    
    
    public void ProcessRequest (HttpContext context) {
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        string sReportID = Convert.ToString(context.Request.QueryString["sReportID"]);
        StringBuilder sbData = new StringBuilder();

        switch (Action)
        {
            case "GetReportData":
                {
                    DataSet dt1 = GetPatientLists(sReportID);

                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;

                    if (dt1 != null && dt1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt1.Tables[0].Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt1.Tables[0].Columns)
                            {
                                row.Add(col.ColumnName.ToString(), dr[col]);
                            }
                            rows.Add(row);
                        }

                    }
                    //context.Response.Write(sbData.ToString());
                    context.Response.Write(serializer.Serialize(rows));
                    //  string STRS = sbData.ToString();
                }
                break;

            case "InsertUserData":

                
                string sValue = Convert.ToString(context.Request.QueryString["sValue"]);
                string sDate = Convert.ToString(context.Request.QueryString["sDate"].Replace("-","/"));
                string sTime = Convert.ToString(context.Request.QueryString["sTime"]);
                string sUserId = Convert.ToString(context.Request.QueryString["sReportIDs"]);



                int j = AddPatientData(sValue, sDate, sTime, sUserId);
                context.Response.Write(j);
                break;
        }
       
    }


    public DataSet GetPatientLists(string sReportIDs)
    {
        try
        {
            string Query = @"select * from testReportValues where sTestReportValuesId = "+ sReportIDs +"";
            DataSet ds = new DataSet();
            scon = new SqlConnection(strcon);
            scom = new SqlCommand(Query, scon);
            sda = new SqlDataAdapter(scom);
            sda.Fill(ds);
            return ds;

        }
        catch (Exception)
        {
            return null;
        }
    }

    public int AddPatientData( string value, string Dates, string Time,string sUserid)
    {
        try
        {
            
            string Query = "Update  testReportValues set sValue ='" + value + "' , sDate ='" + Dates + "' , sTime ='" + Time + "' where sTestReportValuesId= '" + sUserid + "'";
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
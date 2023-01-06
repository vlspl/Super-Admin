<%@ WebHandler Language="C#" Class="PatientHealthViewProfile" %>

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



public class PatientHealthViewProfile : IHttpHandler
{

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    

    Dictionary<string, object> returnData = new Dictionary<string, object>();
    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        string sPatientID = Convert.ToString(context.Request.QueryString["sUserID"]);
        StringBuilder sbData = new StringBuilder();
        StringBuilder sbData2 = new StringBuilder();
       

        switch (Action)
        {
            case "GetReportValList":
                {
                    DataSet dt1 = GetReportList(sPatientID);

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

           // case "GetSubAnalyteList":
           //     {
           //         string TestIds = Convert.ToString(context.Request.QueryString["sTestids"]);
           //         System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

           //         List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
           //         Dictionary<string, object> row;
           //                DataSet dt = GetSubAnalyte(TestIds);

           //                if (dt != null && dt.Tables[0].Rows.Count > 0)
           //                {
           //                    foreach (DataRow dr in dt.Tables[0].Rows)
           //                    {
           //                        row = new Dictionary<string, object>();
           //                        foreach (DataColumn col in dt.Tables[0].Columns)
           //                        {
           //                            row.Add(col.ColumnName.ToString(), dr[col]);
           //                        }
           //                        rows.Add(row);
           //                    }

                               
           //                }
           //                context.Response.Write(serializer.Serialize(rows));
           //     }
           //break;
       
        }

    }


    public DataSet GetReportList(string patientid)
    {
        try
        {
            string Query = @"select * from testReportValues where sPatientId = "+patientid+"";
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



    public DataSet GetSubAnalyte(string sTestIds)
    {
        try
        {
            string Query = "select * from test t join testprofile p on t.sTestProfileId=p.sTestProfileId join section s on s.sSectionId=p.sSectionId join testsubanalyte tsa on t.sTestId=tsa.sTestId join subanalyte sa on tsa.sSubAnalyteId=sa.sSubAnalyteId join analyte a on a.sAnalyteId=sa.sAnalyteId join testSubAnalyteSpecimenMethod tsasm on tsasm.sTestSubAnalyteId=tsa.sTestSubAnalyteId join specimen sp on sp.sSpecimenId=tsasm.sSpecimenId join method m on m.sMethodId=tsasm.sMethodId join testSubAnalyteReference tsar on tsar.sTSASMId=tsasm.sTSASMId where t.sTestId='" + sTestIds + "'";
            DataSet dt1 = new DataSet();
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
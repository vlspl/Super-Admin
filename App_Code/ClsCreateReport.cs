using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsCreateReport
{
    DataAccessLayer DAL = new DataAccessLayer();
    public ClsCreateReport()
    {
    }
    public DataSet getMyBookings(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetMyBookingsByLabId " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getBookingDetails(string labId, string bookLabId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@labId",labId),
                new SqlParameter("@bookLabId",bookLabId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetBookingDetailsByLabIdandBookLabId ", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestAnalyte(string bookLabId, string testId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@testId",testId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestAnalyte",param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestRefernceAnalyte(string Gender, string testId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@testId",testId),
                new SqlParameter("@Gender",Gender)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestReferencerange", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestRefernceSubAnalyte(string Gender, string testId)
    {
         DataSet ds = new DataSet();
        try
        {
            if (testId == "1189")
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@testId",testId),
                    new SqlParameter("@Gender",Gender)
                };
                ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestSubAnalyteReferenceRange", param);
                return ds;
            }
            else
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@testId",testId),
                    new SqlParameter("@Gender",Gender)
                };
                ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestSubAnalyteReferenceRange_WIDAL", param);
                return ds;
            }
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestSubAnalyte(string bookLabId, string testId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetTestSubAnalyte " + testId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getsInterpretation(string Interpretation)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetsInterpretation " + Interpretation);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getInterpretationResult(string TestCode)
    {
        DataSet ds = new DataSet();        
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@TestCode",TestCode)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_TestInterpretation ", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int createReport(string bookLabTestId, string reportStatus, string approvalStatus, string reportCreatedOn, string reportCreatedBy, string notes)
    {
        int result;
        try
        {
            //using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            //{
            //    conn.Open();

            //    using (SqlCommand cmd = new SqlCommand(queryReportValues, conn))
            //    {
            //        cmd.CommandType = CommandType.Text;
            //        cmd.ExecuteNonQuery();
            //    }
            //}
          
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sReportStatus",reportStatus),
                new SqlParameter("@sApprovalStatus",approvalStatus),
                new SqlParameter("@sReportCreatedOn",reportCreatedOn),
                new SqlParameter("@sReportCreatedBy",reportCreatedBy),
                new SqlParameter("@sBookLabTestId",bookLabTestId),
                new SqlParameter("@sNotes",notes),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_CreateReport", param);
        }
        catch (Exception)
        {
            //return 0 if error
            result = 0;
        }
        return result;
    }
    public Dictionary<string, object> getAnalyteReference(string TASMId)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            //check if analyte already exists
            DataSet ds = new DataSet();
            ds = DAL.GetDataSet("Sp_GetAnalyteReference " + TASMId);
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

                returnType = 1;
                returnData.Add("key", returnType);
                returnData.Add("value", rows);
                return returnData;
            }
            else
            {
                returnType = 2;
                returnData.Add("key", returnType);
                returnData.Add("value", null);
                return returnData;
            }
        }
        catch (Exception)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
    }
    public Dictionary<string, object> getSubAnalyteReference(string TSASMId)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            //check if analyte already exists
            DataSet ds = new DataSet();
            ds = DAL.GetDataSet("Sp_GetSubAnalyteReference " + TSASMId);
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
                returnType = 1;
                returnData.Add("key", returnType);
                returnData.Add("value", rows);
                return returnData;
            }
            else
            {
                returnType = 2;
                returnData.Add("key", returnType);
                returnData.Add("value", null);
                return returnData;
            }
        }
        catch (Exception)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
    }
    // Fetch Test Analyte Interprtation
    public DataTable GetTestAnylteInterpretaion(string TASMId)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetsTestAnalyteInterpretation " + TASMId);
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    // Fetch Test Sub Analyte Interprtation
    public DataTable GetTestSubAnylteInterpretaion(string TSASMId)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetsTestSubAnalyteInterpretation " + TSASMId);
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
}
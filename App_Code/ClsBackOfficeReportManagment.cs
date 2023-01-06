using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
/// <summary>
/// Summary description for ClsBackOfficeReportManagment
/// </summary>
public class ClsBackOfficeReportManagment
{  
    DataAccessLayer DAL = new DataAccessLayer();
	public ClsBackOfficeReportManagment()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet getMyBookings(string Userid)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetMyBookingsformanualPunched " + Userid);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getBookingDetails(string Createdby, string bookLabId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@CreatedBy",Createdby),
                new SqlParameter("@bookLabId",bookLabId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetBookingDetailsforManualPuching ", param);
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
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@testId",testId),
                new SqlParameter("@Gender",Gender)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestSubAnalyteReferenceRange", param);
            return ds;
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
        string code = (TestCode == "25HDN") ? "_25HDN" : TestCode;
        try
        {
            ds = DAL.GetDataSet("Sp_TestInterpretation  " + code);
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
    public DataSet getReports(string CreatedBy)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetReportByCreatedId " + CreatedBy);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getVerifyReports()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetReportByForCrossVerification");
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestReport(string bookLabTestId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetTestReportBybookLabId  " + bookLabTestId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int approveRejectReport(string bookLabTestId, string approvalStatus, string reportApprovedOn, string reportApprovedBy, string comment)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sBookLabTestId", bookLabTestId),
                    new SqlParameter("@sApprovalStatus", approvalStatus),
                    new SqlParameter("@sReportApprovedOn", reportApprovedOn),
                    new SqlParameter("@sReportApprovedBy", reportApprovedBy),
                    new SqlParameter("@sComment", comment)
                };
            DAL.ExecuteStoredProcedure("Sp_Updatebooklabtest", param);
            return 1;
        }
        catch (Exception)
        {
            //return 0 if error
            return 0;
        }
    }
    public void approveRejectReport(string bookLabTestId)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sBookLabTestId",bookLabTestId)
            };
            DAL.ExecuteStoredProcedure("Sp_UpdatebooklabtestByBookLabTestId", param);
        }
        catch (Exception)
        {
            //return 0 if error
            // return 0;
        }
    }
    public void UpdateReportStatus(string bookId, string ReportStatus)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@BookId",bookId),
                new SqlParameter("@ReportStatus",ReportStatus)
            };
            DAL.ExecuteStoredProcedure("Sp_UpdateReportStatus", param);
        }
        catch (Exception)
        {
            //return 0 if error
            // return 0;
        }
    }
    public int updateTestReport(string bookLabTestId, string queryUpdateReport, string notes)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(queryUpdateReport, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                //update bookLabTest table to change report approval status to pending after report edit
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sBookLabTestId",bookLabTestId),
                    new SqlParameter("@sNotes",notes)
                };
                DAL.ExecuteStoredProcedure("Sp_updateTestReport", param);
                return 1;
            }
        }
        catch (Exception)
        {
            //return 0 if error
            return 0;
        }
    }
    public int updateTestReportwithPDF(string bookLabTestId, string notes, string pdffilename)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sBookLabTestId", bookLabTestId),
                new SqlParameter("@sPDFfiles", pdffilename)
            };
            DAL.ExecuteStoredProcedure("Sp_UpdateTestReportwithPDF", param);
            return 1;
        }
        catch (Exception)
        {
            //return 0 if error
            return 0;
        }
    }
}
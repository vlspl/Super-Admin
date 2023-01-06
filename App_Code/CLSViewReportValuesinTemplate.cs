using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

/// <summary>
/// Summary description for CLSViewReportValuesinTemplate
/// </summary>
public class CLSViewReportValuesinTemplate
{
    DataAccessLayer DAL = new DataAccessLayer();
	public CLSViewReportValuesinTemplate()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet getTestReport(string bookLabTestId)
    {
        DataSet ds = new DataSet();
        try
        {
           // ds = DAL.GetDataSet("Sp_GetTestReportDetailsByLabTestId  " + bookLabTestId);

            ds = DAL.GetDataSet("Sp_GetTestReportDetailsByLabTestIdTestUpdated  " + bookLabTestId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestReport_normalReport(string booklabtestid, string patientId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param_normalReport = new SqlParameter[]
            {
                new SqlParameter("@booklabtestid", booklabtestid),
                    new SqlParameter("@UserId", patientId),
                                 
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetReportDetails  " , param_normalReport);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestReport_doctorNote(string booklabtestid, string patientId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param_DocNote = new SqlParameter[]
            {
                new SqlParameter("@SharedReportID", booklabtestid),
                    new SqlParameter("@UserId", patientId),
                                 
            };
            ds = DAL.ExecuteStoredProcedureDataSet("WS_Sp__GetNoteListsBySharedReportID  ", param_DocNote);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestReport_mView(string bookLabTestId)
    {
        DataSet ds = new DataSet();
        try
        {
            // ds = DAL.GetDataSet("Sp_GetTestReportDetailsByLabTestId  " + bookLabTestId);

            ds = DAL.GetDataSet("WS_Sp_GetOldReportDetails_mView  " + bookLabTestId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable getTestReportTable(string bookLabTestId)
    {
        DataTable ds = new DataTable();
        try
        {
            // ds = DAL.GetDataSet("Sp_GetTestReportDetailsByLabTestId  " + bookLabTestId);

            ds = DAL.GetDataTable("Sp_GetTestReportDetailsByLabTestIdTest  " + bookLabTestId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestReportgetbyid(string bookLabTestId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetTestReportgetbyid " + bookLabTestId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestReporttemp(string bookLabTestId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetTestReporttemp " + bookLabTestId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
}
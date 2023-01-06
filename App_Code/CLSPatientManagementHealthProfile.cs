using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

/// <summary>
/// Summary description for CLSPatientManagementHealthProfile
/// </summary>
public class CLSPatientManagementHealthProfile
{
    DataAccessLayer DAL = new DataAccessLayer();
	public CLSPatientManagementHealthProfile()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet getTestReport(string bookLabTestId, string userid)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@bookLabTestId",bookLabTestId),
                new SqlParameter("@userid",userid)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestReportbyTestIdanduserid", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable getAllTestReport(string bookLabTestId, string userid)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@bookLabTestId",bookLabTestId),
                new SqlParameter("@userid",userid)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_GetTestReportbyTestIdanduserid", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
}
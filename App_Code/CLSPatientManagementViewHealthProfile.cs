using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

/// <summary>
/// Summary description for CLSPatientManagementViewHealthProfile
/// </summary>
public class CLSPatientManagementViewHealthProfile
{
    DataAccessLayer DAL = new DataAccessLayer();
	public CLSPatientManagementViewHealthProfile()
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
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@bookLabTestId",bookLabTestId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestReportByBookTestId", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
}
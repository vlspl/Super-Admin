using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
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
            ds = DAL.GetDataSet("Sp_getTestReportByBookLabTestId " + bookLabTestId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
}
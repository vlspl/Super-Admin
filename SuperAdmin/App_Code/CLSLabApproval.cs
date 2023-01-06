using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using DataAccessHandler;

/// <summary>
/// Summary description for CLSLabsManagement
/// </summary>
public class CLSLabApproval
{
    DataAccessLayer DAL = new DataAccessLayer();
	public CLSLabApproval()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet getApproList(int Temp_LabId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetTempLabApprovalDetails");
            return ds;
        }
        catch(Exception)
        {
            return null;
        }
        return ds;
    }
}
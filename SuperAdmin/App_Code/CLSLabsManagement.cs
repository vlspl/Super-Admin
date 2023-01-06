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
public class CLSLabsManagement
{
    DataAccessLayer DAL = new DataAccessLayer();
	public CLSLabsManagement()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet GetLabList()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetLab");
            return ds;
        }
        catch(Exception)
        {
            return null;
        }
    }
}
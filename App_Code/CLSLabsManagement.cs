using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

/// <summary>
/// Summary description for CLSLabsManagement
/// </summary>
public class CLSLabsManagement
{
    DataAccessLayer DAL = new DataAccessLayer();
   // SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["connection"]);
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
            ds =DAL.GetDataSet("Sp_GetLabList");
            return ds;
        }
        catch(Exception)
        {
            return null;
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

/// <summary>
/// Summary description for CLSLabMasterPage
/// </summary>
public class CLSLabMasterPage
{
    DataAccessLayer DAL = new DataAccessLayer();
	public CLSLabMasterPage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet getMenuList(string labUserId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetMenuList " + labUserId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getMenuListRoles(string labUserId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetMenuListRoles " + labUserId);
            return ds;
        }
        catch (Exception ex)
        {
            ds = null;
            return ds;
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

public class ClsManageRoles
{
    DataAccessLayer DAL = new DataAccessLayer();
	public ClsManageRoles()
	{
	}
    public DataSet getRoles(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetRoles " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int deleteRole(string labUserRoleId)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sLabUserRoleId",labUserRoleId)
            };
            DAL.ExecuteStoredProcedure("Sp_DeleteRoleByLabUserRoleId", param);
             return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public int addRole(string labId, string role)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sLabId",labId),
                new SqlParameter("@sRole",role)
            };
            DAL.ExecuteStoredProcedure("Sp_AddRoleByLabId", param);
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
}
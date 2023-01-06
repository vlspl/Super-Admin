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
            ds = DAL.GetDataSet("Sp_getRolesByLabId " + labId);
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
                new SqlParameter("@sRollsID",labUserRoleId)
            };
            DAL.ExecuteStoredProcedure("Sp_DeleteRole", param);
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public int addRole(string labId, string role)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sAdminUserId",labId),
                new SqlParameter("@sRollName",role),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddRole", param);
        }
        catch (Exception)
        {
            result= 0;
        }
        return result;
    }
}
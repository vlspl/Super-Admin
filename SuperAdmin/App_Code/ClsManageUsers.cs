using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using DataAccessHandler;
public class ClsManageUsers
{
    DataAccessLayer DAL = new DataAccessLayer();
	public ClsManageUsers()
	{
	}
    public DataSet getSuperAdminUsers(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_getSuperAdminUsersByLabId " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
   public DataSet getSuperAdminUserRoles(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_getSuperAdminUserRoles " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int addSuperAdminUser(string labId, string labCode, string fullname, string emailId, string contact, string description, string userName, string password, string role)
    { 
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sSuperAdminId", labId),
                    new SqlParameter("@sSuperAdminCode", labCode),
                    new SqlParameter("@sFullName", fullname),
                    new SqlParameter("@sEmailId", emailId),
                    new SqlParameter("@sContact", contact),
                    new SqlParameter("@sDescription", description),
                    new SqlParameter("@sUserName", userName),
                    new SqlParameter("@sPassword", password),
                    new SqlParameter("@sRole", role),
                    new SqlParameter("@returnval", SqlDbType.Int)
                };
                result=DAL.ExecuteStoredProcedureRetnInt("Sp_AddSuperAdminUser",param);
        }
        catch (Exception)
        {
            result= 0;
        }
        return result;
    }
    public int deleteSuperAdminUser(string labUserId)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sSuperAdminUserId",labUserId)
            };
            DAL.ExecuteStoredProcedure("Sp_DeleteSuperAdminUser", param);
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public int updateSuperAdminUserRole(string labUserId, string fullName, string emailId, string contact, string description, string role)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sSuperAdminUserId", labUserId),
                new SqlParameter("@sFullName", fullName),
                new SqlParameter("@sEmailId", emailId),
                new SqlParameter("@sContact", contact),
                new SqlParameter("@sDescription", description),
                new SqlParameter("@sRole", role),
                new SqlParameter("@returnval", SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateSuperAdminUserRole", param);
        }
        catch (Exception)
        {
            //return 0 if error
            result= 0;
        }
        return result;
    }
}
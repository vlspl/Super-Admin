using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsManageUsersEdit
{
    DataAccessLayer DAL = new DataAccessLayer();
	public ClsManageUsersEdit()
	{
	}
    public DataSet getLabUserRoles(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetLabUserRoles " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getLabUser(string labId,string labUserId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@labId",labId),
                new SqlParameter("@labUserId",labUserId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetLabUsersbylabIdandIabuserId", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int updateLabUserRole(string labUserId, string fullName, string emailId, string contact, string description, string role)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@labUserId", labUserId),
                new SqlParameter("@sFullName", fullName),
                new SqlParameter("@sEmailId", emailId),
                new SqlParameter("@sContact", contact),
                new SqlParameter("@sDescription", description),
                new SqlParameter("@sRole", role),
                new SqlParameter("@returnval", SqlDbType.Int),
            };

            result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateLabUserRole", param);
        }
        catch (Exception)
        {
            //return 0 if error
            result= 0;
        }
        return result;
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsChangePassword
{
    DataAccessLayer DAL = new DataAccessLayer();
    public ClsChangePassword()
    {
    }
    public string updatePassword(string userName, string oldPassword, string newPassword, string userId)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@oldPassword",oldPassword),
                new SqlParameter("@userName",userName),
                new SqlParameter("@NewPassword",newPassword),
                 new SqlParameter("@UserId",userId),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_ChangePasswordNewUP", param);
        }
        catch (Exception ex)
        {
            result = -1;
            LogError.LoggerCatch(ex);
        }
        return result.ToString(); 
    }
}
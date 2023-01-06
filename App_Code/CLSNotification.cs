using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

/// <summary>
/// Summary description for CLSNotification
/// </summary>
public class CLSNotification
{
    DataAccessLayer DAL = new DataAccessLayer();
    public CLSNotification()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int AppNotification(string UserAppid, string LabID, string Title, string Message, string Status, string Date, string LabuserID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sUserAppid", UserAppid),
                new SqlParameter("@sLabID", LabID),
                new SqlParameter("@sTitle", Title),
                new SqlParameter("@sMessage", Message),
                new SqlParameter("@sStatus", Status),
                new SqlParameter("@sDate", Date),
                new SqlParameter("@slabUserid", LabuserID)

            };
            DAL.ExecuteStoredProcedure("Sp_AddAppNotification", param);
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
}
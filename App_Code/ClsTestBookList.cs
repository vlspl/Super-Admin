using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

public class ClsTestBookList
{
    DataAccessLayer DAL = new DataAccessLayer();
	public ClsTestBookList()
	{
	}
    public DataSet getMyBookings(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetMyBookingsByIabId " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getMyBookingsForDashboard(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetMyBookingsByIabIdForDashboard " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getMyBookingsHistory(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetMyBookingsHistoryByIabId " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getMyBookingsHistoryByDate(string labId,string  Date)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@labId",labId),
                new SqlParameter("@Date",Date)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetMyBookingsHistoryByIabIdandDate ",param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
}
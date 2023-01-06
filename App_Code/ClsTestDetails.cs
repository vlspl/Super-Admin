using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsTestDetails
{
    DataAccessLayer DAL = new DataAccessLayer();
	public ClsTestDetails()
	{
	}
    public DataSet getTest(string labId,string testId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@labId",labId),
                new SqlParameter("@testId",testId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestByTestIdandlabId", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    //public DataSet getTestAnalyte(string testId)
    //{
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        ds = DAL.GetDataSet("Sp_GetTestAnalyteByTestId " + testId);
    //        return ds;
    //    }
    //    catch (Exception)
    //    {
    //        ds = null;
    //        return ds;
    //    }
    //}
    //public DataSet getTestSubAnalyte(string testId)
    //{
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        ds = DAL.GetDataSet("Sp_GetTestSubAnalyteByTestId " + testId);
    //        return ds;
    //    }
    //    catch (Exception)
    //    {
    //        ds = null;
    //        return ds;
    //    }
    //}

    public DataSet getTestAnalyte(string labId, string testId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@labId",labId),
                new SqlParameter("@testId",testId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestAnalyteByTestIdAndLABID", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestAnalyte1(string labId, string testId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@labId",labId),
                new SqlParameter("@testId",testId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestAnalyteByTestIdAndLABIDOne", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestSubAnalyte(string labId, string testId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@labId",labId),
                new SqlParameter("@testId",testId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestSubAnalyteByTestIdAndLabID", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
}
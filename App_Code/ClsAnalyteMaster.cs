using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using DataAccessHandler;
public class ClsAnalyteMaster
{
    DataAccessLayer DAL = new DataAccessLayer();
    public ClsAnalyteMaster()
    {
    }
    public int addAnalyte(string analyteName)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sAnalyteName",analyteName),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddAnalyte", param);
        }
        catch (Exception)
        {
            result = 0;
        }
        return result;
    }
    public DataSet getAnalyte()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetAnalyte");
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int deleteAnalyte(string analyteId)
    {
        int returnval = 0;
        try
        {
            if (returnval == 0)
            {
                DataTable dt = DAL.GetDataTable("Sp_GettestAnalyteByanalytedId " + analyteId);
                if (dt.Rows.Count > 0)
                {
                    returnval = 2;
                }
            }
            if (returnval == 0)
            {
                DataTable dt1 = DAL.GetDataTable("Sp_GettestSubAnalyteByanalytedId "+analyteId);
                if (dt1.Rows.Count > 0)
                {
                    returnval = 2;
                }
            }
            if (returnval == 0)
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sAnalyteId",analyteId),
                    new SqlParameter("@returnval",SqlDbType.Int)
                };
                int result = DAL.ExecuteStoredProcedureRetnInt("Sp_DeleteAnalyte ", param);
                if (result == 1)
                {
                    returnval = 1;
                }
            }
        }
        catch (Exception)
        {
            returnval = -1;
        }
        return returnval;
    }
    public int updateAnalyte(string analyteId, string analyteName)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sAnalyteId",analyteId),
                new SqlParameter("@sAnalyteName",analyteName),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateAnalyte", param);
        }
        catch (Exception)
        {
            result = 0;
        }
        return result;
    }
}
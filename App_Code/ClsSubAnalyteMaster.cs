using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsSubAnalyteMaster
{
    DataAccessLayer DAL = new DataAccessLayer();
	public ClsSubAnalyteMaster()
	{	
	}
    public int addSubAnalyte(string subAnalyteName, string analyteId)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@subAnalyteName",subAnalyteName),
                new SqlParameter("@sAnalyteId",analyteId),
				  new SqlParameter("@IsActive","1"),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddSubAnalyte", param);
            if (result > 0)
            {
                result = 1;
            }
        }
        catch (Exception)
        {
            result= 0;
        }
        return result;
    }
    public DataSet getSubAnalyte()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetSubAnalyte");
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int deleteSubAnalyte(string subAnalyteId)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sSubAnalyteId",subAnalyteId),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_DeleteSubAnalyte", param);
        }
        catch (Exception)
        {
            result= 0;
        }
        return result;
    }
    public int updateSubAnalyte(string subAnalyteId, string analyteId, string subAnalyteName)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@subAnalyteId",subAnalyteId),
                new SqlParameter("@sSubAnalyteName",subAnalyteName),
                new SqlParameter("@sAnalyteId",analyteId),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateSubAnalyte", param);
        }
        catch (Exception)
        {
            //return 0 if error
            result= 0;
        }
        return result;
    }
}
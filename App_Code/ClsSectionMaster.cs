using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsSectionMaster
{
    DataAccessLayer DAL = new DataAccessLayer();
    public ClsSectionMaster()
    {
    }
    public int addSection(string sectionName)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sectionName",sectionName),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddSection", param);
        }
        catch (Exception)
        {
            //return 0 if error
            result = 0;
        }
        return result;
    }
    public DataSet getSection()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetSection");
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable getHomeCollection(string labId)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_LabHomeCollection " + labId);
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    public int deleteSection(string sectionId)
    {
        int result;
        try
        {
            DataTable dt = DAL.GetDataTable("Sp_GetSectionBySectionId " + sectionId);
            if (dt.Rows.Count > 0)
            {
                result = 2;
            }
            else
            {
                SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sSectionId",sectionId),
                        new SqlParameter("@returnval",SqlDbType.Int)
                    };
                result = DAL.ExecuteStoredProcedureRetnInt("Sp_DeleteSection", param);
            }
        }
        catch (Exception)
        {
            result= 0;
        }
        return result;
    }
    public int updateSection(string sectionId, string sectionName)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sSectionId",sectionId),
                new SqlParameter("@sSectionName",sectionName),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateSection", param);
        }
        catch (Exception)
        {
            //return 0 if error
            result= 0;
        }
        return result;
    }
}
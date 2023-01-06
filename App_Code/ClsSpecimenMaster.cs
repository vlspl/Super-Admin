using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsSpecimenMaster
{
    DataAccessLayer DAL = new DataAccessLayer();
    public ClsSpecimenMaster()
    {
    }
    public DataSet getSpecimen()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetspecimenDetails");
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int addSpecimen(string sampleType, string quantity, string timePeriod)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sampleType",sampleType),
                new SqlParameter("@quantity",quantity),
                new SqlParameter("@timePeriod",timePeriod),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddSpecimen", param);
        }
        catch (Exception ex)
        {
            result = 0;
        }
        return result;
    }
    public int deleteSpecimen(string specimenId)
    {
        int result = 0;
        try
        {
            DataTable dt = DAL.GetDataTable("Sp_GetSpecimenfromtestAnalyteSpecimenMethod " + specimenId);
            if (dt.Rows.Count > 0)
            {
                result = 2;
            }
            if (result == 0)
            {
                DataTable dt1 = DAL.GetDataTable("Sp_GetSpecimenfromtestSubAnalyteSpecimenMethod " + specimenId);
                if (dt1.Rows.Count > 0)
                {
                    result = 2;
                }
            }
            if (result == 0)
            {
                SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sSpecimenId",specimenId),
                        new SqlParameter("@returnval",SqlDbType.Int)
                    };
                result = DAL.ExecuteStoredProcedureRetnInt("Sp_DeleteSpecimen", param);
            }
        }
        catch (Exception)
        {
            result= 0;
        }
        return result;
    }
    public int updateSpecimen(string specimenId, string sampleType, string quantity, string timePeriod)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sampleType",sampleType),
                new SqlParameter("@quantity",quantity),
                new SqlParameter("@timePeriod",timePeriod),
                new SqlParameter("@sSpecimenId",specimenId),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateSpecimen", param);
        }
        catch (Exception)
        {
            //return 0 if error
            result= 0;
        }
        return result;
    }
}
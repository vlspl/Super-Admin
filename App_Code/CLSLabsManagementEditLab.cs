using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class CLSLabsManagementEditLab
{
    DataAccessLayer DAL = new DataAccessLayer();
    public DataSet GetLabList(int labid)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetLabDetailsByLabId " + labid);
            return ds;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public int UpdateLabDetails(int labid, string LabName, string LabManager, string LabEmailId, string LabContact, string LabAddress)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
               {
                    new SqlParameter("@labid", labid),
                    new SqlParameter("@sLabName", LabName),
                    new SqlParameter("@sLabManager", LabManager),
                    new SqlParameter("@sLabEmailId", LabEmailId),
                    new SqlParameter("@sLabContact", LabContact),
                    new SqlParameter("@sLabAddress", LabAddress),
                    new SqlParameter("@returnval", SqlDbType.Int)
             };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateLabDetails", param);
        }
        catch (Exception)
        {
            result = 0;
        }
        return result;
    }
}
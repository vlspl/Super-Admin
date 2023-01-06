using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using DataAccessHandler;
/// <summary>
/// Summary description for CLSLabsManagementEditLab
/// </summary>
public class CLSLabsManagementEditLab
{
    //SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["connection"]);
    DataAccessLayer DAL = new DataAccessLayer();
	public CLSLabsManagementEditLab()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet GetLabList(int labid)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetLabDetails " + labid);
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
            result= 0;
        }
        return result;
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
/// <summary>
/// Summary description for CLSEditRoles
/// </summary>
public class CLSEditRoles
{
    DataAccessLayer DAL = new DataAccessLayer();
    public CLSEditRoles()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataSet getPageList(string username, string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@userId",username),
                new SqlParameter("@sLabID",labId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetPageList", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
        
        
        //DataSet ds = new DataSet();
        //try
        //{
        //    ds = DAL.GetDataSet("Sp_GetPageList " + username);
        //    return ds;
        //}
        //catch (Exception)
        //{
        //    ds = null;
        //    return ds;
        //}
    }
}
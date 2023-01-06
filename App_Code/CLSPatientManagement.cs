using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

/// <summary>
/// Summary description for CLSPatientManagement
/// </summary>
public class CLSPatientManagement
{
    DataAccessLayer DAL = new DataAccessLayer();
    public CLSPatientManagement()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataSet getReports(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetappUserandbookLab " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
}
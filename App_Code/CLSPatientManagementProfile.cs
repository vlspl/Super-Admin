using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
/// <summary>
/// Summary description for CLSPatientManagementProfile
/// </summary>
public class CLSPatientManagementProfile
{
    DataAccessLayer DAL = new DataAccessLayer();
 
	public CLSPatientManagementProfile()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet GetUserProfile(int appuserid)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetUserProfile " + appuserid);
            return ds;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
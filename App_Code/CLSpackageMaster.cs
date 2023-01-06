using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for CLSpackageMaster
/// </summary>
public class CLSpackageMaster
{
    DataAccessLayer DAL = new DataAccessLayer();
    public string packageName { get; set; }
    public string days { get; set; }
    public string masterId { get; set; }
    public string labId { get; set; }
    public string price { get; set; }
    public string status { get; set; }
    public string description { get; set; }
    public string createdDate { get; set; }
    public string assignDate { get; set; }
    public string expiredDate { get; set; }
    public string createdBy { get; set; }
    public string updatedDate { get; set; }
    public string updatedBy { get; set; }
   
	public CLSpackageMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int insertpkgEntry()
    {
       
        int returnVal = 0;
        int packageId;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@packageName", packageName),
                new SqlParameter("@days", days),
                new SqlParameter("@price", price),
                new SqlParameter("@status", status),
                new SqlParameter("@description", description),
                new SqlParameter("@createdDate", createdDate),
                new SqlParameter("@createdBy", createdBy),
                new SqlParameter("@updatedDate", updatedDate),
                new SqlParameter("@updatedBy", updatedBy),
                 new SqlParameter("@returnval", SqlDbType.Int)
              
            };
            packageId = DAL.ExecuteStoredProcedureRetnInt("Sp_insertPackageMasterEntry", param);

           
            
        }
        catch (Exception)
        {
            returnVal = 0;
        }
        return returnVal;

    }
    public int pkgAssign()
    {

        int returnVal = 0;
        int packageId;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@pMasterId", masterId),
                new SqlParameter("@sLabId", labId),
                new SqlParameter("@assignDate", assignDate),
                new SqlParameter("@days", days),
                new SqlParameter("@expiredDate", expiredDate),
                new SqlParameter("@createdDate", createdDate),
                new SqlParameter("@createdBy", createdBy),
                 new SqlParameter("@returnval", SqlDbType.Int)
              
            };
            packageId = DAL.ExecuteStoredProcedureRetnInt("Sp_insertPackageAssignEntry", param);



        }
        catch (Exception)
        {
            returnVal = 0;
        }
        return returnVal;

    }

    public DataSet Get_packageMaster()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_Get_PackageMaster");//Sp_GetTempLabApprovalDetails
        }
        catch (Exception)
        {
            return null;
        }
        return ds;
    }
    public DataSet Get_assignpackageMaster()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_Get_AssignPackageMaster");//Sp_GetTempLabApprovalDetails
        }
        catch (Exception)
        {
            return null;
        }
        return ds;
    }
}
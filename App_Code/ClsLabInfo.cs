using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
using CrossPlatformAESEncryption.Helper;
public class ClsLabInfo
{
    DataAccessLayer DAL = new DataAccessLayer();
	public ClsLabInfo()
	{
	}
    public DataSet getLabDetails(string labId)
    {
        DataSet ds = new DataSet();
        try
        {    ds = DAL.GetDataSet("Sp_GetLabDetails " + labId);
             return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int updateLabDetails(string labId, string labName, string LabRegID, string labImages, string labDetails, string labLogo, string labHomeCollection)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                    new SqlParameter("@labId",labId),
                    new SqlParameter("@sLabName",labName),
                    new SqlParameter("@sLabRegID", LabRegID),
                    new SqlParameter("@sLabImages", labImages),
                    new SqlParameter("@sLabDetails",labDetails),
                    new SqlParameter("@sLabLogo", labLogo),
                    new SqlParameter("@sLabHomeCollection", labHomeCollection)
                  
            };
            DAL.ExecuteStoredProcedure("Sp_UpdateLabinfo", param);
            return 1;
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            //return 0 if error
            return 0;
        }
    }
    public int updateLabContactDetails(string labId, string labEmail, string labContact, string labAddress, string labLocation)
    {
        try
        {
            string _EmailId = (labEmail != "") ? CryptoHelper.Encrypt(labEmail.ToLower()) : "";
            string _Mobile = (labContact != "") ? CryptoHelper.Encrypt(labContact) : ""; 
            SqlParameter[] param = new SqlParameter[]
            {
                    new SqlParameter("labId",labId),
                    new SqlParameter("@sLabEmailId",_EmailId),
                    new SqlParameter("@sLabContact", _Mobile),
                    new SqlParameter("@sLabAddress", labAddress),
                    new SqlParameter("@sLabLocation",labLocation)
            };
            DAL.ExecuteStoredProcedure("Sp_UpdateLabContactinfo", param);
            return 1;
        }
        catch (Exception)
        {
            //return 0 if error
            return 0;
        }
    }
}
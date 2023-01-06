using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsProfileMaster
{
    DataAccessLayer DAL = new DataAccessLayer();
	public ClsProfileMaster()
	{
	}
    public int addProfile(string profileName, string sectionId)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sProfileName",profileName),
                new SqlParameter("@sSectionId",sectionId),
                new SqlParameter("@returnval",SqlDbType.Int),
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddProfile", param);
        }
        catch (Exception)
        {
            result= 0;
        }
        return result;
    }
    public DataSet getTestProfile()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetTestProfile");
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestProfile(string sectionid )
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] Param = new SqlParameter[]
            {
                new SqlParameter("@sSectionId",sectionid)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestProfileBysSectionId", Param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int deleteProfile(string profileId)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sTestProfileId",profileId),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result =DAL.ExecuteStoredProcedureRetnInt("Sp_DeleteProfile",param);
        }
        catch (Exception)
        {
            result= 0;
        }
        return result;
    }
    public int updateProfile(string testProfileId, string sectionId, string profileName)
    {
        int result;
        try
        {
            SqlParameter[] Param = new SqlParameter[]
            {
                new SqlParameter("@sTestProfileId",testProfileId),
                new SqlParameter("@sProfileName",profileName),
                new SqlParameter("@sSectionId",sectionId),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result=DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateProfileBysTestProfileId",Param);
        }
        catch (Exception)
        {
            //return 0 if error
            result= 0;
        }
        return result;
    }
}
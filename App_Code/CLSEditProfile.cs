using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
using CrossPlatformAESEncryption.Helper;
public class CLSEditProfile
{
    DataAccessLayer DAL = new DataAccessLayer();
    public CLSEditProfile()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataSet getUserDetails(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetUserDetails " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int updateUserContactDetails(string LabId, string UserId, string UserName, string UserEmail, string UserContact)
    {
        int result;
        try
        {
            string _EmailId = (UserEmail != "") ? CryptoHelper.Encrypt(UserEmail.ToLower()) : "";
            string _Mobile = (UserContact != "") ? CryptoHelper.Encrypt(UserContact) : "";
            SqlParameter[] param = new SqlParameter[] 
                { 
                     new SqlParameter("@UserId",UserId),
                     new SqlParameter("@sFullName",UserName),
                     new SqlParameter("@sEmailId",_EmailId),
                     new SqlParameter("@sContact",_Mobile),
                     new SqlParameter("@returnval",SqlDbType.Int)                
                };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateUserContactDetails", param);
        }
        catch (Exception)
        {
            //return 0 if error
            result = 0;
        }
        return result;
    }
}
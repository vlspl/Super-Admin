using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
using CrossPlatformAESEncryption.Helper;
using Validation;
public class ClsManageBackendUsers
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();

    public DataSet getBackendUsers(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetBackendUsers " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int addBackendUser(string labId, string labCode, string fullname, string emailId, string contact, string description, string userName, string password, string role)
    {
        //Sp_AddSuperAdminUser sp for this method
        int result;
        try
        {
            string _password = CryptoHelper.Encrypt(password);
            string _EmailId = emailId != "" ? CryptoHelper.Encrypt(emailId.ToLower()) : "";
            string _Mobile = contact != "" ? CryptoHelper.Encrypt(contact) : "";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sSuperAdminId", labId),
                new SqlParameter("@sSuperAdminCode", labCode),
                new SqlParameter("@sFullName", fullname),
                new SqlParameter("@sEmailId", _EmailId),
                new SqlParameter("@sContact", _Mobile),
                new SqlParameter("@sDescription", description),
                new SqlParameter("@sUserName", ""),
                new SqlParameter("@sPassword", ""),
                new SqlParameter("@sRole", role),
                new SqlParameter("@returnval", SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddSuperAdminUser", param);
            if (result >= 1)
            {
                SqlParameter[] param2 = new SqlParameter[]
                                             {
                                                new SqlParameter("@UserId",result),
                                                new SqlParameter("@Mobile",_Mobile),
                                                new SqlParameter("@EmailId",_EmailId),
                                                new SqlParameter("@Role","Backend"),
                                                new SqlParameter("@Password",_password),
                                                new SqlParameter("@UserName",""),
                                                new SqlParameter("@Returnval",SqlDbType.Int)
                                             };
                int ResultVal1 = DAL.ExecuteStoredProcedureRetnInt("Sp_AddUserLoginCredentials", param2);
                if (ResultVal1 == 1)
                {
                    if (Ival.IsValidEmailAddress(emailId))
                    {
                        ClsEmailTemplates emailTemp = new ClsEmailTemplates();
                        string mailSent = emailTemp.sendmail(emailId, password, fullname, contact);
                    }
                }
            }
        }
        catch (Exception)
        {
            result = 0;
        }
        return result;
    }
    public int deleteBackendUser(string labUserId)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sSuperAdminUserId",labUserId)
            };
            DAL.ExecuteStoredProcedure("Sp_DeleteSuperAdminUser", param);
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public int updateBackendUser(string labUserId, string fullName, string emailId, string contact, string description, string role)
    {
        int result;
        try
        {
            string _EmailId = emailId != "" ? CryptoHelper.Encrypt(emailId.ToLower()) : "";
            string _Mobile = contact != "" ? CryptoHelper.Encrypt(contact) : "";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sSuperAdminUserId", labUserId),
                new SqlParameter("@sFullName", fullName),
                new SqlParameter("@sEmailId", _EmailId),
                new SqlParameter("@sContact", _Mobile),
                new SqlParameter("@sDescription", description),
                new SqlParameter("@sRole", role),
                new SqlParameter("@returnval", SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateSuperAdminUserRole", param);
        }
        catch (Exception)
        {
            result = 0;
        }
        return result;
    }
}
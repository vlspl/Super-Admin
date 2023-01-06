using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
using Validation;
using CrossPlatformAESEncryption.Helper;
/// <summary>
/// Summary description for ClsManageSuperUsers
/// </summary>
public class ClsManageSuperUsers
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();

    public DataSet getSuperAdminUsers(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetSuperAdminUsers " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getSuperAdminUserRoles(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_getSuperAdminUserRoles " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int addSuperAdminUser(string labId, string labCode, string fullname, string emailId, string contact, string description, string userName, string password, string role)
    {
        //Sp_AddSuperAdminUser sp for this method
        int result;
        try
        {
            if (Ival.IsValidEmailAddress(emailId))
            {
                ClsEmailTemplates emailTemp = new ClsEmailTemplates();
                string mailSent = emailTemp.sendmail(emailId, password, fullname, contact);
            }
            string _password = CryptoHelper.Encrypt(password);
            string _EmailId = emailId != "" ? CryptoHelper.Encrypt(emailId) : "";
            string _Mobile = contact != "" ? CryptoHelper.Encrypt(contact) : "";
            string _Username = CryptoHelper.Encrypt(userName);
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
                                                new SqlParameter("@Role","Admin"),
                                                new SqlParameter("@Password",_password),
                                                new SqlParameter("@UserName",_Username),
                                                new SqlParameter("@Returnval",SqlDbType.Int)
                                             };
                int ResultVal1 = DAL.ExecuteStoredProcedureRetnInt("Sp_AddUserLoginCredentials", param2);
            }
        }
        catch (Exception)
        {
            result = 0;
        }
        return result;
    }
    public int deleteSuperAdminUser(string labUserId)
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
    public int updateSuperAdminUserRole(string labUserId, string fullName, string emailId, string contact, string description, string role)
    {
        int result;
        try
        {
            string _EmailId = emailId != "" ? CryptoHelper.Encrypt(emailId) : "";
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
            //return 0 if error
            result = 0;
        }
        return result;
    }
}
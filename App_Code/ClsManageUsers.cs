using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
using CrossPlatformAESEncryption.Helper;
public class ClsManageUsers
{
    DataAccessLayer DAL = new DataAccessLayer();
   
    public DataSet getLabUsers(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetLabUsers " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getLabUserRoles(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetLabUserRoles " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int addLabUser(string labId, string labCode, string fullname, string emailId, string contact, string description, string userName, string password, string role)
    {
        int data;
        try
        {
            string badd = "";
            string bedit = "";
            string bview = "";
            string _EmailId = (emailId != "") ? CryptoHelper.Encrypt(emailId.ToLower()) : "";
            string _Mobile = (contact != "") ? CryptoHelper.Encrypt(contact) : "";
            string _userName = CryptoHelper.Encrypt(userName);
            string _password = CryptoHelper.Encrypt(password);
            SqlParameter[] param = new SqlParameter[]
                  {
                     new SqlParameter("@sLabId", labId),
                     new SqlParameter("@sLabCode", labCode),
                     new SqlParameter("@sFullName", fullname),
                     new SqlParameter("@sEmailId", _EmailId),
                     new SqlParameter("@sContact", _Mobile),
                     new SqlParameter("@sDescription", description),
                     new SqlParameter("@sUserName", _userName),
                     new SqlParameter("@sRole", role),
                     new SqlParameter("@returnval",SqlDbType.Int)
                 };
            data = DAL.ExecuteStoredProcedureRetnInt("Sp_AddLabUser", param);
            if (data >= 1)
            {
                for (int i = 1; i <= 26; i++)
                {
                    badd = "0";
                    bedit = "0";
                    bview = "1";
                    SqlParameter[] param2 = new SqlParameter[]
                         {
                             new SqlParameter("@sUserid", data),
                             new SqlParameter("@sPageID", i),
                             new SqlParameter("@sLabId", labId),
                             new SqlParameter("@sAdd", badd),
                             new SqlParameter("@sEdit", bedit),
                             new SqlParameter("@sView", bview),
                             new SqlParameter("@returnval", SqlDbType.Int)
                        };
                    int result = DAL.ExecuteStoredProcedureRetnInt("Sp_InsertlabUserRolesList", param2);
                }

                /// Add Lab Login Credentials
                SqlParameter[] paramlab = new SqlParameter[]
                                             {
                                                new SqlParameter("@UserId",data),
                                                new SqlParameter("@Mobile",_Mobile),
                                                new SqlParameter("@EmailId",_EmailId),
                                                new SqlParameter("@Role","Lab"),
                                                new SqlParameter("@Password",_password),
                                                new SqlParameter("@UserName",_userName),
                                                new SqlParameter("@Returnval",SqlDbType.Int)
                                             };
                int ResultVal1 = DAL.ExecuteStoredProcedureRetnInt("Sp_AddUserLoginCredentials", paramlab);
            }
        }
        catch (Exception)
        {
            data = 0;
        }
        return data;
    }
    public int deleteLabUser(string labUserId)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sLabUserId", labUserId)
            };
            DAL.ExecuteStoredProcedure("Sp_DeleteLabUser", param);
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public int updateLabUserRole(string labUserId, string fullName, string emailId, string contact, string description, string role)
    {
        int result;
        try
        {
            string _EmailId = (emailId != "") ? CryptoHelper.Encrypt(emailId.ToLower()) : "";
            string _Mobile = (contact != "") ? CryptoHelper.Encrypt(contact) : "";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@labUserId", labUserId),
                new SqlParameter("@sFullName", fullName),
                new SqlParameter("@sEmailId", _EmailId),
                new SqlParameter("@sContact", _Mobile),
                new SqlParameter("@sRole",role ),
                new SqlParameter("@sDescription",description ),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateLabUserRole", param);
        }
        catch (Exception)
        {
            //return 0 if error
            result = 0;
        }
        return result;
    }
}
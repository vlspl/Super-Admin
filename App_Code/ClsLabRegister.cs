using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
using CrossPlatformAESEncryption.Helper;
using System.Configuration;
/// <summary>
/// Summary description for CLSLabRegister
/// </summary>
public class ClsLabRegister
{
    DataAccessLayer DAL = new DataAccessLayer();
    public int nLabId { get; set; }
    public string sLabCode { get; set; }
    public string sLabName { get; set; }
    public string sLabManager { get; set; }
    public string sEmailId { get; set; }
    public string sStatus { get; set; }
    public string sLabContact { get; set; }
    public string sLabAddress { get; set; }
    public string sUserName { get; set; }
    public string sPassword { get; set; }
    public string latLong { get; set; }
    public string sColA { get; set; }
    public string sColB { get; set; }
    public string sColC { get; set; }
    public string sColD { get; set; }
    public string sColE { get; set; }
    public string sColF { get; set; }
    public string sColG { get; set; }
    public string sColH { get; set; }
    public string sColI { get; set; }
    public string sColJ { get; set; }

    public string orgId { get; set; }
    public string labStatus { get; set; }

    public ClsLabRegister()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int insertLabEntry()
    {
        int returnVal = 0;
        int labId;
        try
        {
            string _password = CryptoHelper.Encrypt(sPassword);
            string _EmailId = (sEmailId != "") ? CryptoHelper.Encrypt(sEmailId.ToLower()) : "";
            string _Mobile = (sEmailId != "") ? CryptoHelper.Encrypt(sLabContact) : "";
            string _UserName = CryptoHelper.Encrypt(sUserName);
            string roleid = "";
            string badd = "";
            string bedit = "";
            string bview = "";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sUserName", sUserName),
                new SqlParameter("@sLabCode", sLabCode),
                new SqlParameter("@sLabName", sLabName),
                new SqlParameter("@sLabManager", sLabManager),
                new SqlParameter("@sLabEmailId", _EmailId),
                new SqlParameter("@sLabStatus", sStatus),
                new SqlParameter("@sLabContact", _Mobile),
                new SqlParameter("@sLabAddress", sLabAddress),
                new SqlParameter("@sLabLocation", latLong),
                new SqlParameter("@returnval", SqlDbType.Int)
            };
            labId = DAL.ExecuteStoredProcedureRetnInt("Sp_insertLabEntry", param);

            if (labId == -3)
            {
                returnVal = 3;
            }
            else
            {
                SqlParameter[] param2 = new SqlParameter[]
                    {
                        new SqlParameter("@sLabId", labId),
                        new SqlParameter("@sLabCode", sLabCode),
                        new SqlParameter("@sFullName", sLabManager),
                        new SqlParameter("@sEmailId", _EmailId),
                        new SqlParameter("@sContact", _Mobile),
                        new SqlParameter("@sRole", "Owner"),
                        new SqlParameter("@sDescription", "Has all the rights"),
                        new SqlParameter("@sUserName", _UserName),
                        new SqlParameter("@sPassword", ""),
                        new SqlParameter("@returnval", SqlDbType.Int)
                    };
                int result = DAL.ExecuteStoredProcedureRetnInt("Sp_insertLabUser", param2);
                /// Add Lab Login Credentials
                SqlParameter[] param3 = new SqlParameter[]
                    {
                        new SqlParameter("@sUserName",_UserName)
                    };
                roleid = DAL.ExecuteScalarWithProc("Sp_GetUserRoleIdByUserName", param3);
                SqlParameter[] paramlab = new SqlParameter[]
                                             {
                                                new SqlParameter("@UserId",roleid),
                                                new SqlParameter("@Mobile",_Mobile),
                                                new SqlParameter("@EmailId",_EmailId),
                                                new SqlParameter("@Role","Lab"),
                                                new SqlParameter("@Password",_password),
                                                new SqlParameter("@UserName",_UserName),
                                               
                                                new SqlParameter("@Returnval",SqlDbType.Int)
                                             };
                int ResultVal1 = DAL.ExecuteStoredProcedureRetnInt("Sp_AddUserLoginCredentials", paramlab);
                if (labStatus == "Private")
                {
                    SqlParameter[] paramLab = new SqlParameter[]
                 {
                         new SqlParameter("@OrgId",orgId),
                         new SqlParameter("@LabId",labId),
                         new SqlParameter("@ReturnVal",SqlDbType.Int)
                };
                    int resultLab = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOrgLab", paramLab);

                    // update status in labMaster
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update LabMaster set sColJ=1 where sLabId='" + labId + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                for (int i = 1; i <= 26; i++)
                {
                    badd = "1";
                    bedit = "1";
                    bview = "1";
                    SqlParameter[] param4 = new SqlParameter[]
                    {
                        new SqlParameter("@sUserid", roleid),
                        new SqlParameter("@sPageID", i),
                        new SqlParameter("@sLabId", labId),
                        new SqlParameter("@sAdd", badd),
                        new SqlParameter("@sEdit", bedit),
                        new SqlParameter("@sView", bview),
                        new SqlParameter("@returnval", SqlDbType.Int)
                    };
                    returnVal = DAL.ExecuteStoredProcedureRetnInt("Sp_InsertlabUserRolesList", param4);
                }
            }
        }
        catch (Exception)
        {
            returnVal = 0;
        }
        return returnVal;
    }
}
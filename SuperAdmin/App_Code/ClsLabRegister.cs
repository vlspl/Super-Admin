using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

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
        int result;
        try
        {
              string labId = "";
              string userNameExist = "";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sUserName", sLabCode),
                new SqlParameter("@sLabCode", sLabCode),
                new SqlParameter("@sLabName", sLabName),
                new SqlParameter("@sLabManager", sLabManager),
                new SqlParameter("@sLabEmailId", sEmailId),
                new SqlParameter("@sLabStatus", sStatus),
                new SqlParameter("@sLabContact", sLabContact),
                new SqlParameter("@sLabAddress", sLabAddress),
                new SqlParameter("@returnval", SqlDbType.Int)
            };
            userNameExist = DAL.ExecuteScalarWithProc("Sp_insertLabEntry", param);
            if (userNameExist == "-3")
            {
                result = 3;
            }
            else if (userNameExist != "-3" && userNameExist != "0")
            {
                labId = userNameExist;
            }
            if (labId !="")
            {
                SqlParameter[] param1 = new SqlParameter[]
                {
                    new SqlParameter("@sLabId", labId),
                    new SqlParameter("@sPrice", "0")
                };
                DAL.ExecuteStoredProcedure("Sp_inserttestLab", param1);
            }
            SqlParameter[] param2 = new SqlParameter[]
            {
                new SqlParameter("@sLabId", labId),
                new SqlParameter("@sLabCode", sLabCode),
                new SqlParameter("@sFullName", sLabManager),
                new SqlParameter("@sEmailId",sEmailId),
                new SqlParameter("@sContact",sLabContact),
                new SqlParameter("@sRole","Owner"),
                new SqlParameter("@sDescription","Has all the rights"),
                new SqlParameter("@sUserName",sUserName),
                new SqlParameter("@sPassword",sPassword),
                new SqlParameter("@returnval",SqlDbType.Int)
            };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_insertLabUser", param2);

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
        }
        catch(Exception)
        {
            result= 0;
        }
        return result;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessHandler;
using CrossPlatformAESEncryption.Helper;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for CLSLabApprovalInsert
/// </summary>
public class CLSLabApprovalInsert
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
    public string temp_lab { get; set; }
    public CLSLabApprovalInsert()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int insertLabaprEntry()
    {
        int returnVal = 0;
        int labId;
        int tempId;
        try
        {
           
            string _EmailId = (sEmailId != "") ? CryptoHelper.Encrypt(sEmailId.ToLower()) : "";
            string _Mobile = (sEmailId != "") ? CryptoHelper.Encrypt(sLabContact) : "";
            string roleid = "";
            string badd = "";
            string bedit = "";
            string bview = "";
            SqlParameter[] param = new SqlParameter[]
            {
                //new SqlParameter("@sUserName", sUserName),
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
            labId = DAL.ExecuteStoredProcedureRetnInt("Sp_insertLabApproval", param);

          

            if (labId == -3)
            {
                returnVal = 3;
            }
            else
            {
                 
                //SqlParameter[] param2 = new SqlParameter[]
                //    {
                //            new SqlParameter("@Name", sLabName),
                //            new SqlParameter("@Address", sLabAddress),
                //            new SqlParameter("@Contact", _Mobile ),
                //            new SqlParameter("@Email", _EmailId),
                //            new SqlParameter("@Org_Details", ""),
                //            new SqlParameter("@Org_Status", "True"),
                //            new SqlParameter("@CreatedDate", System.DateTime.Now.ToString("dd-MM-yyyy")),
                //            new SqlParameter("@IsActive", "True"),
                //            new SqlParameter("@returnval", SqlDbType.Int)
                //    };
                //orgId = DAL.ExecuteStoredProcedureRetnInt("Sp_insertLabApprovalOrgMstr", param2);
                ///// Add Lab Login Credentials
                SqlParameter[] param3 = new SqlParameter[]
                                             {
                                                new SqlParameter("@Org_ID",orgId),
                                                new SqlParameter("@Lab_Id",labId),
                                                new SqlParameter("@IsActive","True"),
                                                new SqlParameter("@CreatedDate",""),
                                                new SqlParameter("@Returnval",SqlDbType.Int)
                                             };
                int ResultVal = DAL.ExecuteStoredProcedureRetnInt("Sp_insertLabApproval_OrganizationTieupLab", param3);


                SqlParameter[] param1 = new SqlParameter[]
                         {
                        //new SqlParameter("@sUserName", sUserName),
                              new SqlParameter("@Temp_LabId", temp_lab),
                             new SqlParameter("@Returnval",SqlDbType.Int)
                          };
                tempId = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateLabApprovalStatus", param1);
                    }
            
           
        }
        catch (Exception ex)
        {
            returnVal = 0;
        }
        return returnVal;
    }
}
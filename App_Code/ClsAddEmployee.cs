using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessHandler;
using System.Data;
using System.Data.SqlClient;
using Validation;
using BitsBizLogic;
using Org.BouncyCastle.Asn1.Ocsp;
using CrossPlatformAESEncryption.Helper;

/// <summary>
/// Summary description for ClsAddEmployee
/// </summary>
public class ClsAddEmployee
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    public ClsAddEmployee()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string AddEmployee(string Name, string Mobile, string EmailId, string Gender, string Birthdate, string Address,
        string State, string city, string Pincode, string EmployeeId, string AadharCard, string OrgId, string Branchid, string CreatedBy, string password,
        string Department, string UserId)
    {
        string Msg = "";
        int _userId = Convert.ToInt32(UserId);
        string _EmailId = CryptoHelper.Encrypt(EmailId.ToLower());
        string _Mobile = CryptoHelper.Encrypt(Mobile);
        string _Aadhar = AadharCard != "" ? CryptoHelper.Encrypt(AadharCard) : "";
        if (_userId == 0)
        {
            string _password = CryptoHelper.Encrypt(password);
            if (Ival.IsTextBoxEmpty(Name))
            {
                Msg += "● Please Enter Valid Name<br>";
            }
            if (!Ival.IsCharOnly(Name))
            {
                Msg = "● Please Enter Valid Employee Name <br>";
            }
            if (Ival.IsTextBoxEmpty(Address))
            {
                Msg += "● Please Enter Valid Adress<br>";
            }
            if (Ival.PhoneMobileValidation(Mobile))
            {
                Msg += "● Please Enter Valid Mobile Number<br>";
            }
            if (!Ival.IsValidEmailAddress(EmailId))
            {
                Msg += "● Please Enter Valid Email Address <br>";
            }
            else
            {
                ClsEmailTemplates emailTemp = new ClsEmailTemplates();
                string mailSent = emailTemp.sendmail(EmailId, password, Name, Mobile);
            }
            if (Ival.IsValidAadharCard(AadharCard))
            {
                Msg += "● Please Enter Valid Aadhar Number <br>";
            }
            if (!Ival.IsDecimal(Pincode))
            {
                Msg += "● Please Enter Valid Pincode Number <br>";
            }
            if (Msg.Length > 0)
            {
                return Msg;
            }
            else
            {
                SqlParameter[] param = new SqlParameter[]
                {
                      new SqlParameter("@sFullName",Name),
                      new SqlParameter("@sMobile",_Mobile),
                      new SqlParameter("@sEmailId",_EmailId),
                      new SqlParameter("@sGender",Gender),
                      new SqlParameter("@sBirthDate",Birthdate),
                      new SqlParameter("@sAddress",Address),
                      new SqlParameter("@sRole","Employee"),
                      new SqlParameter("@sCountry","India"),
                      new SqlParameter("@sState",State),
                      new SqlParameter("@sCity",city),
                      new SqlParameter("@sPincode",Pincode),
                      new SqlParameter("@EmployeeId",EmployeeId),
                      new SqlParameter("@AadharCard",_Aadhar),
                      new SqlParameter("@Department",Department),
                      new SqlParameter("@Returnval",SqlDbType.Int)
                };
                int Result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddEmployeeUpdate", param);
                if (Result >= 1)
                {
                    SqlParameter[] param1 = new SqlParameter[]
                {
                      new SqlParameter("@EmpId",Result),
                      new SqlParameter("@OrgId",OrgId),
                      new SqlParameter("@BranchId",Branchid),
                      new SqlParameter("@CreatedBy",CreatedBy),
                      new SqlParameter("@Returnval",SqlDbType.Int)
                };
                    int ResultVal = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOrgEmployee", param1);
                    if (ResultVal == 1)
                    {
                        SqlParameter[] param2 = new SqlParameter[]
                {
                      new SqlParameter("@UserId",Result),
                      new SqlParameter("@Mobile",_Mobile),
                      new SqlParameter("@EmailId",_EmailId),
                      new SqlParameter("@Role","Employee"),
                      new SqlParameter("@Password",_password),
                      new SqlParameter("@UserName",""),
                      new SqlParameter("@Returnval",SqlDbType.Int)
                };
                        int ResultVal1 = DAL.ExecuteStoredProcedureRetnInt("Sp_AddUserLoginCredentials", param2);
                        Msg = "1";
                    }
                    else if (ResultVal == -2)
                    {
                        Msg = "-2";
                    }
                    else
                    {
                        Msg = "● Something Went Wrong, Please Try Again After Some Time!";
                    }
                }
                else
                {
                    Msg = Result.ToString();
                }
            }
        }
        else
        {
            SqlParameter[] param = new SqlParameter[]
            {
                  new SqlParameter("@UserId",_userId),          
                  new SqlParameter("@sFullName",Name),
                  new SqlParameter("@EmployeeId",EmployeeId),
                  new SqlParameter("@sBirthDate",Birthdate),
                  new SqlParameter("@AadharCard",_Aadhar),
                  new SqlParameter("@sMobile",_Mobile),
                  new SqlParameter("@sEmailId",_EmailId),
                  new SqlParameter("@sGender ",Gender),
                  new SqlParameter("@sState",State),
                  new SqlParameter("@sCity",city),
                  new SqlParameter("@sPincode",Pincode),
                  new SqlParameter("@sAddress",Address),
                  new SqlParameter("@Returnval",SqlDbType.Int)
            };
            int result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateEmployeeFromPatient", param);
            if (result >= 1)
            {
                SqlParameter[] param1 = new SqlParameter[]
                {
                      new SqlParameter("@EmpId",UserId),
                      new SqlParameter("@OrgId",OrgId),
                      new SqlParameter("@BranchId",Branchid),
                      new SqlParameter("@CreatedBy",CreatedBy),
                      new SqlParameter("@Returnval",SqlDbType.Int)
                };
                int ResultVal = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOrgEmployee", param1);
                if (ResultVal == 1)
                {
                    SqlParameter[] param2 = new SqlParameter[]
                            {
                                new SqlParameter("@UserId",UserId),
                                new SqlParameter("@Returnval",SqlDbType.Int)
                            };
                    int ResultVal1 = DAL.ExecuteStoredProcedureRetnInt("Sp_ConvertPatienttoEmployee", param2);
                    Msg = "1";
                }
            }
        }
        return Msg;
    }
    //public string AddEmployee(string Name, string Mobile, string EmailId, string Gender, string Birthdate, string Address,
    //     string State, string city, string Pincode, string EmployeeId, string AadharCard, string OrgId, string Branchid, string CreatedBy, string password,
    //     string Department, string UserId)
    //{
    //    string Msg = "";
    //    string _password = CryptoHelper.Encrypt(password);
    //    string _EmailId = CryptoHelper.Encrypt(EmailId.ToLower());
    //    string _Mobile = CryptoHelper.Encrypt(Mobile);

    //    if (Ival.IsTextBoxEmpty(Name))
    //    {
    //        Msg += "● Please Enter Valid Name<br>";
    //    }
    //    if (!Ival.IsCharOnly(Name))
    //    {
    //        Msg = "● Please Enter Valid Employee Name <br>";
    //    }
    //    if (Ival.IsTextBoxEmpty(Address))
    //    {
    //        Msg += "● Please Enter Valid Adress<br>";
    //    }
    //    if (Ival.PhoneMobileValidation(Mobile))
    //    {
    //        Msg += "● Please Enter Valid Mobile Number<br>";
    //    }
    //    if (!Ival.IsValidEmailAddress(EmailId))
    //    {
    //        Msg += "● Please Enter Valid Email Address <br>";
    //    }
    //    else
    //    {
    //        ClsEmailTemplates emailTemp = new ClsEmailTemplates();
    //        string mailSent = emailTemp.sendmail(EmailId, password, Name, Mobile);
    //    }
    //    if (Ival.IsValidAadharCard(AadharCard))
    //    {
    //        Msg += "● Please Enter Valid Aadhar Number <br>";
    //    }
    //    if (!Ival.IsDecimal(Pincode))
    //    {
    //        Msg += "● Please Enter Valid Pincode Number <br>";
    //    }
    //    if (Msg.Length > 0)
    //    {
    //        return Msg;
    //    }
    //    else
    //    {
    //        SqlParameter[] param = new SqlParameter[]
    //            {
    //                  new SqlParameter("@sFullName",Name),
    //                  new SqlParameter("@sMobile",_Mobile),
    //                  new SqlParameter("@sEmailId",_EmailId),
    //                  new SqlParameter("@sGender",Gender),
    //                  new SqlParameter("@sBirthDate",Birthdate),
    //                  new SqlParameter("@sAddress",Address),
    //                  new SqlParameter("@sRole","Employee"),
    //                  new SqlParameter("@sCountry","India"),
    //                  new SqlParameter("@sState",State),
    //                  new SqlParameter("@sCity",city),
    //                  new SqlParameter("@sPincode",Pincode),
    //                  new SqlParameter("@EmployeeId",EmployeeId),
    //                  new SqlParameter("@AadharCard",AadharCard),
    //                  new SqlParameter("@Department",Department),
    //                  new SqlParameter("@Returnval",SqlDbType.Int)
    //            };
    //        int Result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddEmployeeUpdate", param);
    //        if (Result >= 1)
    //        {
    //            SqlParameter[] param1 = new SqlParameter[]
    //            {
    //                  new SqlParameter("@EmpId",Result),
    //                  new SqlParameter("@OrgId",OrgId),
    //                  new SqlParameter("@BranchId",Branchid),
    //                  new SqlParameter("@CreatedBy",CreatedBy),
    //                  new SqlParameter("@Returnval",SqlDbType.Int)
    //            };
    //            int ResultVal = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOrgEmployee", param1);
    //            if (ResultVal == 1)
    //            {
    //                SqlParameter[] param2 = new SqlParameter[]
    //            {
    //                  new SqlParameter("@UserId",Result),
    //                  new SqlParameter("@Mobile",_Mobile),
    //                  new SqlParameter("@EmailId",_EmailId),
    //                  new SqlParameter("@Role","Employee"),
    //                  new SqlParameter("@Password",_password),
    //                  new SqlParameter("@UserName",""),
    //                  new SqlParameter("@Returnval",SqlDbType.Int)
    //            };
    //                int ResultVal1 = DAL.ExecuteStoredProcedureRetnInt("Sp_AddUserLoginCredentials", param2);
    //                Msg = "1";
    //            }
    //            else if (ResultVal == -2)
    //            {
    //                Msg = "-2";
    //            }
    //            else
    //            {
    //                Msg = "● Something Went Wrong, Please Try Again After Some Time!";
    //            }
    //        }
    //        else
    //        {
    //            Msg = Result.ToString();
    //        }
    //    }
    //    return Msg;
    //}
    public Dictionary<string, object> checkPatientExist(string mobile)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            string _Mobile = mobile != "" ? CryptoHelper.Encrypt(mobile) : "";
            SqlParameter[] param1 = new SqlParameter[]
            {
                new SqlParameter("@mobile",_Mobile)
            };
            DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetPatientByMobile ", param1);
            if (ds.Tables[0].Rows.Count > 0)
            {
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in ds.Tables[0].Columns)
                    {
                        if (col.ColumnName == "sMobile")
                        {
                            string _mobile = dr[col].ToString() != "" ? CryptoHelper.Decrypt(dr[col].ToString()) : "";
                            row.Add(col.ColumnName, _mobile);
                        }
                        else if (col.ColumnName == "sEmailId")
                        {
                            string _email = dr[col].ToString() != "" ? CryptoHelper.Decrypt(dr[col].ToString()) : "";
                            row.Add(col.ColumnName, _email);
                        }
                        else if (col.ColumnName == "HealthId")
                        {
                            string _HealthId = dr[col].ToString() != "" ? CryptoHelper.Decrypt(dr[col].ToString()) : "";
                            row.Add(col.ColumnName, _HealthId);
                        }
                        else if (col.ColumnName == "AadharCard")
                        {
                            string _AadharCard = dr[col].ToString() != "" ? CryptoHelper.Decrypt(dr[col].ToString()) : "";
                            row.Add(col.ColumnName, _AadharCard);
                        }
                        else
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                    }
                    rows.Add(row);
                }
                returnData.Add("key", returnType);
                returnData.Add("value", rows);
                return returnData;
            }
            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
        catch (Exception)
        {
            returnType = -1;
            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
    }

    public DataTable GetEmployee(string Org_Id, string Branch_Id)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",Org_Id),
               new SqlParameter("@BranchId",Branch_Id)
            };
            dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmployeeList ", param);
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    public DataTable GetEmployeeExcelExport(string Org_Id, string Branch_Id)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",Org_Id),
               new SqlParameter("@BranchId",Branch_Id)
            };
            dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmployeeListForExcelExport ", param);
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    public DataTable GetEmployeeHealthReportList(string Org_Id, string Branch_Id)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",Org_Id),
               new SqlParameter("@BranchId",Branch_Id)
            };
            dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmployeehealthReport ", param);
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    public DataTable GetEmployeeReport(string Org_Id, string Branch_Id)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",Org_Id),
               new SqlParameter("@BranchId",Branch_Id)
            };
            dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmployeeUploadedReport ", param);
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    public DataTable GetEmployeehealthReport(string EmpId, string Gender)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@PatientId",EmpId),
               new SqlParameter("@Gender",Gender.Trim())
            };
            dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetTestReportByPatientId ", param);
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    public DataTable GetEmployeeTestDoneExcelExport(string Org_Id, string Branch_Id)
    {
        DataTable dt = new DataTable();
       
        DataTable dtNew = new DataTable();
        dtNew.Columns.Add("EmployeeId", typeof(string));
        dtNew.Columns.Add("Name", typeof(string));
        dtNew.Columns.Add("BranchName", typeof(string));
        dtNew.Columns.Add("Department", typeof(string));
        dtNew.Columns.Add("TestDate", typeof(string));
        dtNew.Columns.Add("ReportStatus", typeof(string));
        dtNew.Columns.Add("TestName", typeof(string));
        dtNew.Columns.Add("Analyte", typeof(string));
        dtNew.Columns.Add("SubAnalyte", typeof(string));
        dtNew.Columns.Add("Value", typeof(string));
        dtNew.Columns.Add("Unit", typeof(string));
        dtNew.Columns.Add("Result", typeof(string));
        try
        {

            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",Org_Id),
               new SqlParameter("@BranchId",Branch_Id)
            };
            dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmployeeTestDoneListForExcelExport ", param);
            dt.Columns.Add("resultVal", typeof(string));
            dt.Columns.Add("resultVal1", typeof(string));
            foreach (DataRow row in dt.Rows)
            {

                row["resultVal"] = row["Value"].ToString() != "" ? CryptoHelper.Decrypt(row["Value"].ToString()) : "";
                row["resultVal1"] = row["Result"].ToString()!=""?CryptoHelper.Decrypt(row["Result"].ToString()):"";

                DataRow dtrow = dtNew.NewRow();
                dtrow["EmployeeId"] = row["EmployeeId"];
                dtrow["Name"] = row["Name"];
                dtrow["BranchName"] = row["BranchName"];
                dtrow["Department"] = row["Department"];
                dtrow["TestDate"] = row["TestDate"];
                dtrow["ReportStatus"] = row["ReportStatus"];
                dtrow["TestName"] = row["TestName"];
                dtrow["Analyte"] = row["Analyte"];
                dtrow["SubAnalyte"] = row["SubAnalyte"];
                dtrow["Value"] = row["resultVal"];
                dtrow["Unit"] = row["Unit"];
                dtrow["Result"] = row["resultVal1"];
                dtNew.Rows.Add(dtrow);
            }
            return dtNew;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    public DataTable GetEmployeeTestDoneList(string Org_Id, string Branch_Id)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",Org_Id),
               new SqlParameter("@BranchId",Branch_Id)
            };
            dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmployeeTestDoneListForExcelExport ", param);
            dt.Columns.Add("resultVal", typeof(string));
            dt.Columns.Add("resultVal1", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                row["resultVal"] = row["Value"].ToString() != "" ? CryptoHelper.Decrypt(row["Value"].ToString()) : "";
                row["resultVal1"] = row["Result"].ToString() != "" ? CryptoHelper.Decrypt(row["Result"].ToString()) : "";
            }
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    public DataTable GetEmployeeTestCompletedExcelExport(string Org_Id, string Branch_Id)
    {
        DataTable dt = new DataTable();

        DataTable dtNew = new DataTable();
        dtNew.Columns.Add("EmployeeId", typeof(string));
        dtNew.Columns.Add("Name", typeof(string));
        dtNew.Columns.Add("BranchName", typeof(string));
        dtNew.Columns.Add("Department", typeof(string));
        dtNew.Columns.Add("TestDate", typeof(string));
        dtNew.Columns.Add("ReportStatus", typeof(string));
        dtNew.Columns.Add("TestName", typeof(string));
        dtNew.Columns.Add("Analyte", typeof(string));
        dtNew.Columns.Add("SubAnalyte", typeof(string));
        dtNew.Columns.Add("Value", typeof(string));
        dtNew.Columns.Add("Unit", typeof(string));
        dtNew.Columns.Add("Result", typeof(string));
        try
        {

            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",Org_Id),
               new SqlParameter("@BranchId",Branch_Id)
            };
            dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmployeeTestCompltedListForExcelExport ", param);
            dt.Columns.Add("resultVal", typeof(string));
            dt.Columns.Add("resultVal1", typeof(string));
            foreach (DataRow row in dt.Rows)
            {

                row["resultVal"] = row["Value"].ToString() != "" ? CryptoHelper.Decrypt(row["Value"].ToString()) : "";
                row["resultVal1"] = row["Result"].ToString() != "" ? CryptoHelper.Decrypt(row["Result"].ToString()) : "";

                DataRow dtrow = dtNew.NewRow();
                dtrow["EmployeeId"] = row["EmployeeId"];
                dtrow["Name"] = row["Name"];
                dtrow["BranchName"] = row["BranchName"];
                dtrow["Department"] = row["Department"];
                dtrow["TestDate"] = row["TestDate"];
                dtrow["ReportStatus"] = row["ReportStatus"];
                dtrow["TestName"] = row["TestName"];
                dtrow["Analyte"] = row["Analyte"];
                dtrow["SubAnalyte"] = row["SubAnalyte"];
                dtrow["Value"] = row["resultVal"];
                dtrow["Unit"] = row["Unit"];
                dtrow["Result"] = row["resultVal1"];
                dtNew.Rows.Add(dtrow);
            }
            return dtNew;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    public DataTable GetEmployeeTestCompletedList(string Org_Id, string Branch_Id)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",Org_Id),
               new SqlParameter("@BranchId",Branch_Id)
            };
            dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmployeeTestCompltedListForExcelExport ", param);
            dt.Columns.Add("resultVal", typeof(string));
            dt.Columns.Add("resultVal1", typeof(string));
            foreach (DataRow row in dt.Rows)
            {

                row["resultVal"] = row["Value"].ToString() != "" ? CryptoHelper.Decrypt(row["Value"].ToString()) : "";
                row["resultVal1"] = row["Result"].ToString() != "" ? CryptoHelper.Decrypt(row["Result"].ToString()) : "";
            }
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    public DataTable GetEmployeeTestOngoingExcelExport(string Org_Id, string Branch_Id)
    {
        DataTable dt = new DataTable();
        try
        {
         SqlParameter[] param = new SqlParameter[]
         {
            new SqlParameter("@OrgId",Org_Id),
            new SqlParameter("@BranchId",Branch_Id)
         };
         dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmployeeTestOngoingListForExcelExport ", param);
         return dt;
            #region
        //DataTable dtNew = new DataTable();
        //dtNew.Columns.Add("EmployeeId", typeof(string));
        //dtNew.Columns.Add("Name", typeof(string));
        //dtNew.Columns.Add("BranchName", typeof(string));
        //dtNew.Columns.Add("Department", typeof(string));
        //dtNew.Columns.Add("TestDate", typeof(string));
        //dtNew.Columns.Add("ReportStatus", typeof(string));
        //dtNew.Columns.Add("TestName", typeof(string));
       
        //try
        //{

        //    SqlParameter[] param = new SqlParameter[]
        //    {
        //       new SqlParameter("@OrgId",Org_Id),
        //       new SqlParameter("@BranchId",Branch_Id)
        //    };
        //    dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmployeeTestOngoingListForExcelExport ", param);
         
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        DataRow dtrow = dtNew.NewRow();
        //        dtrow["EmployeeId"] = row["EmployeeId"];
        //        dtrow["Name"] = row["Name"];
        //        dtrow["BranchName"] = row["BranchName"];
        //        dtrow["Department"] = row["Department"];
        //        dtrow["TestDate"] = row["TestDate"];
        //        dtrow["ReportStatus"] = row["ReportStatus"];
        //        dtrow["TestName"] = row["TestName"];
        //        dtNew.Rows.Add(dtrow);
        //    }
        //    return dtNew;
#endregion
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessHandler;
using Validation;
using System.Data;
using System.Data.SqlClient;
using CrossPlatformAESEncryption.Helper;

/// <summary>
/// Summary description for ClsBackOfficeBooktest
/// </summary>
public class ClsBackOfficeBooktest
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();

    public DataTable GetPatientDetails(string PatientId)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetPatientDetails " + PatientId);
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    public DataTable GetLabDetails(string LabId)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetLabDetailbyLabId " + LabId);
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    public DataTable GetLabList()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetnonHowzUlabs");
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    public DataSet GetDoctors(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetDoctorsListbyLabId " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int AddLab(string LabName, string EmailId, string Mobile, string Address, string LabManager,string CreatedBy)
    {
        try
        {
           
            string _EmailId = CryptoHelper.Encrypt(EmailId.ToLower());
            string _Mobile = CryptoHelper.Encrypt(Mobile);
            string timestamp = DateTime.UtcNow.ToString("ddMMyyyyHHmmssms");

            SqlParameter[] param = new SqlParameter[]
                {
                        new SqlParameter("@LabCode","LAB" + timestamp),
                        new SqlParameter("@LabName",LabName),
                        new SqlParameter("@LabManager",LabManager),
                        new SqlParameter("@LabEmailId",_EmailId),
                        new SqlParameter("@LabContact",_Mobile),
                        new SqlParameter("@LabAddress",Address),
                        new SqlParameter("@CreatedBy",CreatedBy),
                        new SqlParameter("@returnval",SqlDbType.Int)
                };
            int returnval = DAL.ExecuteStoredProcedureRetnInt("WS_Sp_AddNonHowzuLab", param);
            return returnval;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public int addDoctor(string action, string appUserId, string labId, string fullname, string emailId, string mobile, string gender, string birthDate, string address, string degree, string specialization, string clinic, string country, string state, string city, string pincode)
    {
        try
        {
            string doctorId = "";

            //if doctor doesn't exist i.e not registered
            string Password = CreateRandomPassword();
            string _password = CryptoHelper.Encrypt(Password);
            string _EmailId = CryptoHelper.Encrypt(emailId.ToLower());
            string _Mobile = CryptoHelper.Encrypt(mobile);
            if (action == "0")
            {
                //if (Ival.IsValidEmailAddress(emailId))
                //{
                //    ClsEmailTemplates emailTemp = new ClsEmailTemplates();
                //    string mailSent = emailTemp.sendmail(emailId, Password, fullname, mobile);
                //}
                SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sFullName", fullname),
                        new SqlParameter("@sMobile", _Mobile),
                        new SqlParameter("@sEmailId",_EmailId),
                        new SqlParameter("@sGender", gender),
                        new SqlParameter("@sBirthDate", birthDate),
                        new SqlParameter("@sAddress", address),
                        new SqlParameter("@sDegree", degree),
                        new SqlParameter("@sSpecialization", specialization),
                        new SqlParameter("@sClinic", clinic),
                        new SqlParameter("@sRole", "Doctor"),
                        new SqlParameter("@sCountry", country),
                        new SqlParameter("@sState", state),
                        new SqlParameter("@sCity", city),
                        new SqlParameter("@sPincode", pincode),
                        new SqlParameter("@sRegistered", "0"),
                        new SqlParameter("@sRegisterFrom", "Manual")
                    };
                doctorId = DAL.ExecuteScalarWithProc("Sp_AddDoctor", param);

                if (doctorId != "")
                {
                    SqlParameter[] param1 = new SqlParameter[]
                           {
                               new SqlParameter("@sDoctorId",doctorId),
                               new SqlParameter("@sMobile",_Mobile),
                               new SqlParameter("@sLabId",labId)
                           };
                    DAL.ExecuteStoredProcedure("Sp_Insertlabdoctor", param1);
                    SqlParameter[] param2 = new SqlParameter[]
                                             {
                                                new SqlParameter("@UserId",doctorId),
                                                new SqlParameter("@Mobile",_Mobile),
                                                new SqlParameter("@EmailId",_EmailId),
                                                new SqlParameter("@Role","Doctor"),
                                                new SqlParameter("@Password",_password),
                                                new SqlParameter("@UserName",""),
                                                new SqlParameter("@Returnval",SqlDbType.Int)
                                             };
                    int ResultVal1 = DAL.ExecuteStoredProcedureRetnInt("Sp_AddUserLoginCredentials", param2);
                }
            }
            //if doctor is registered but not added to lab
            else if (action == "2")
            {
                SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sFullName", fullname),
                        new SqlParameter("@sMobile", _Mobile),
                        new SqlParameter("@sEmailId",_EmailId),
                        new SqlParameter("@sGender", gender),
                        new SqlParameter("@sBirthDate", birthDate),
                        new SqlParameter("@sAddress", address),
                        new SqlParameter("@sDegree", degree),
                        new SqlParameter("@sSpecialization", specialization),
                        new SqlParameter("@sClinic", clinic),
                        new SqlParameter("@sRole", "Doctor"),
                        new SqlParameter("@sCountry", country),
                        new SqlParameter("@sState", state),
                        new SqlParameter("@sCity", city),
                        new SqlParameter("@sPincode", pincode),
                        new SqlParameter("@sRegistered", "0"),
                    };
                DAL.ExecuteStoredProcedure("Sp_UpdateDoctor", param);

                SqlParameter[] param1 = new SqlParameter[]
                           {
                               new SqlParameter("@sDoctorId",appUserId),
                               new SqlParameter("@sMobile",_Mobile),
                               new SqlParameter("@sLabId",labId)
                           };
                DAL.ExecuteStoredProcedure("Sp_Insertlabdoctor", param1);
            }
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    private string CreateRandomPassword()
    {
        // Create a string of characters, numbers, special characters that allowed in the password  
        string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
        Random random = new Random();

        // Select one random character at a time from the string  
        // and create an array of chars  
        char[] chars = new char[6];
        for (int i = 0; i < 6; i++)
        {
            chars[i] = validChars[random.Next(0, validChars.Length)];
        }
        return new string(chars);
    }
    public int bookTest(string labId, string patientId, string doctorId, string bookRequestedAt, string bookConfirmedAt, 
        string timeslot, string bookStatus, string testStatus, string bookMode, string testDate, string fees, string testId, 
        string AppointmentType,string CreatedBy)
    {
        int result;
        int bookLabId;
        try
        {
            SqlParameter[] param = new SqlParameter[]
                  {
                        new SqlParameter("@sLabId",labId),
                        new SqlParameter("@sPatientId",patientId),
                        new SqlParameter("@sDoctorId",doctorId),                       
                        new SqlParameter("@sTestDate",testDate),                       
                        new SqlParameter("@CreatedBy",CreatedBy),
                        new SqlParameter("@returnval",SqlDbType.Int)
                 };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddManualBookTestUpdated", param);
            if (result > 0)
            {
                bookLabId = result;
                string[] testIdList = testId.Split(',');
                foreach (string id in testIdList)
                {
                    SqlParameter[] param1 = new SqlParameter[]
                         {
                        
                             new SqlParameter("@sBookLabId",bookLabId),
                             new SqlParameter("@sTestId",id),
                             new SqlParameter("@returnval",SqlDbType.Int)
                         };
                    result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddbookLabTest", param1);
                }
            }
        }
        catch (Exception)
        {
            result = 0;
        }
        return result;
    }
}
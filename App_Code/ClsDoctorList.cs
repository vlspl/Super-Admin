using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
using Validation;
using CrossPlatformAESEncryption.Helper;
public class ClsDoctorList
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    public DataSet getDoctors(string labId)
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
    public int addDoctor(string action, string appUserId, string labId, string fullname, string emailId, string mobile, string gender, string birthDate, string address, string degree, string specialization, string clinic, string country, string state, string city, string pincode)
    {
        try
        {
            string doctorId = "";
            int _returnval = 0;
            //if doctor doesn't exist i.e not registered
            string Password = CreateRandomPassword();
            string _password = CryptoHelper.Encrypt(Password);
            string _EmailId = (emailId != "") ? CryptoHelper.Encrypt(emailId.ToLower()) : "";
            string _Mobile = (mobile != "") ? CryptoHelper.Encrypt(mobile) : "";
            if (action == "0")
            {
                if (Ival.IsValidEmailAddress(emailId))
                {
                    ClsEmailTemplates emailTemp = new ClsEmailTemplates();
                    string mailSent = emailTemp.sendmail(emailId, Password, fullname, mobile);
                }
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
                    if (ResultVal1 >= 1)
                    {
                        _returnval = 1;
                    }
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
                _returnval = 1;
            }
            return _returnval;

        }
        catch (Exception)
        {
            return 0;
        }
    }
    public int updateDoctor(string action, string appUserId, string labId, string fullname, string emailId, string mobile, string gender, string birthDate, string address, string degree, string specialization, string clinic, string country, string state, string city, string pincode)
    {
        try
        {
            string _EmailId = (emailId != "") ? CryptoHelper.Encrypt(emailId.ToLower()) : "";
            string _Mobile = (mobile != "") ? CryptoHelper.Encrypt(mobile) : "";
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
                        new SqlParameter("@appUserId", appUserId),
                    };
            DAL.ExecuteStoredProcedure("Sp_UpdateDoctorByappUserId", param);

            SqlParameter[] param3 = new SqlParameter[]
                    {
                        new SqlParameter("@Mobile", _Mobile),
                        new SqlParameter("@Emailid", _EmailId),
                        new SqlParameter("@Role", "Doctor"),
                        new SqlParameter("@appUserId", appUserId)
                    };
            DAL.ExecuteStoredProcedure("Sp_UpdateMobileinUserlogintable", param3);
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public Dictionary<string, object> checkDoctorExist(string labId, string mobile)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        string _Mobile = mobile != "" ? CryptoHelper.Encrypt(mobile) : "";
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {

            //check if doctor already added to lab
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@labId",labId),
                    new SqlParameter("@sMobile",_Mobile)
                };
            DataTable dt = DAL.ExecuteStoredProcedureDataTable("Sp_CheckDoctorExistInLab", param);
            if (dt.Rows.Count > 0)
            {
                returnType = 1;
                returnData.Add("key", returnType);
                returnData.Add("value", null);
                return returnData;
            };

            //check if doctor exists in appUser but not added to lab
            DataSet ds = new DataSet();
            SqlParameter[] param1 = new SqlParameter[]
                {
                    new SqlParameter("@sMobile",_Mobile)
                };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_CheckDoctorExistInAppUser ", param1);
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
                        else
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                    }
                    rows.Add(row);
                }
                returnType = 2;
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
}
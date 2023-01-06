using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DataAccessHandler;
using System.Data.SqlClient;
using CrossPlatformAESEncryption.Helper;

/// <summary>
/// Summary description for ClsEnterpriseDash
/// </summary>
public class ClsEnterpriseDash
{
    DataAccessLayer DAL = new DataAccessLayer();
    public DataTable GetDepartmentlist(string OrgId)
    {
        DataTable ds = new DataTable();
        try
        {
            ds = DAL.GetDataTable("Sp_GetDepartmentList " + OrgId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable GetEmployeelist(string OrgId, string Branch_Id)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",OrgId),
               new SqlParameter("@BranchId",Branch_Id)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmployeeList ", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable GetEmployeelistForDash(string OrgId, string Branch_Id)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",OrgId),
               new SqlParameter("@BranchId",Branch_Id)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmployeeListForDash ", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable GetEmployeelistwithDepartment(string OrgId, string Department, string Branch_Id)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@OrgId",OrgId),
                new SqlParameter("@Department",Department),
                new SqlParameter("@BranchId",Branch_Id)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_GetAllEmployeeListwithDepartment ", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable GetDepartmentHeadCount(string OrgId, string Branch_Id)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",OrgId),
               new SqlParameter("@BranchId",Branch_Id)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_GetDepartmentHeadCountUpdated ", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable GetEmployeeTestCount(string OrgId, string Branch_Id)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",OrgId),
               new SqlParameter("@BranchId",Branch_Id)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_EnterPriseDashboardDashEmpCountupdate ", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet GetDashboardAnalytics(string OrgId, string Branch_Id)
    {
        DataSet ds = new DataSet();
        DataSet dsNew = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",OrgId),
               new SqlParameter("@BranchId",Branch_Id)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_EnterpriseDashboardGenderandAgeDetailsUpdated ", param);
           
            if (ds.Tables.Count > 0)
            {
                DataTable dt1 = new DataTable();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt1 = GetDashboardTestCountGenderwise(ds.Tables[0]);
                }
                dsNew.Tables.Add(dt1);
                DataTable dt2 = new DataTable();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    dt2 = GetDashboardRevenuCountAgevise(ds.Tables[1]);
                }
                dsNew.Tables.Add(dt2);
            }
            return dsNew;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable GetEmployeeTestCountMonth(string OrgId, string Branch_Id)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",OrgId),
               new SqlParameter("@BranchId",Branch_Id)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("WS_Sp_GetEmployeeMonthvisetestcountUpdated ", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable GetEmployeeTestCountYear(string OrgId , string Branch_Id)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",OrgId),
               new SqlParameter("@BranchId",Branch_Id)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_GetEnterpriseTestDashDataforYearupdated ", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable GetEmployeeTestDataforWeek(string OrgId,string Branch_Id)
    {
        DataTable dtNew = new DataTable();
        try
        {
            dtNew.Columns.Add("WEEK1", typeof(string));
            dtNew.Columns.Add("WEEK2", typeof(string));
            dtNew.Columns.Add("WEEK3", typeof(string));
            dtNew.Columns.Add("WEEK4", typeof(string));

            DataRow Prow = dtNew.NewRow();
            DataRow Nrow = dtNew.NewRow();
            DataRow Inrow = dtNew.NewRow();


            int Week1 = 0;
            int Week2 = 0;
            int Week3 = 0;
            int Week4 = 0;

            SqlParameter[] param = new SqlParameter[]
                    {
                            new SqlParameter("@OrgID",OrgId),
                            new SqlParameter("@BranchId",Branch_Id)
                    };
            DataTable dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEnterpriseTestDashDataforWeekUpdated", param);
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["weekData"].ToString() == "1")
                    {
                        Week1++;
                    }
                    else if (dt.Rows[j]["weekData"].ToString() == "2")
                    {
                        Week2++;
                    }
                    else if (dt.Rows[j]["weekData"].ToString() == "3")
                    {
                        Week3++;
                    }
                    else if (dt.Rows[j]["weekData"].ToString() == "4")
                    {
                        Week4++;
                    }
                }
            }
            Prow["WEEK1"] = Week1;
            Prow["WEEK2"] = Week2;
            Prow["WEEK3"] = Week3;
            Prow["WEEK4"] = Week4;
            dtNew.Rows.Add(Prow);

            return dtNew;
        }
        catch (Exception)
        {
            dtNew = null;
            return dtNew;
        }
    }
    public DataTable GetEmployeeTestCountDay(string OrgId, string Branch_Id)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@OrgId",OrgId),
               new SqlParameter("@BranchId",Branch_Id)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_GetEnterpriseTestDashDataforDaysupdated ", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable GetEmployeeCovidData(string OrgId, string Branch_Id)
    {
        DataTable dtNew = new DataTable();
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param1 = new SqlParameter[]
            {
               new SqlParameter("@OrgId",OrgId),
               new SqlParameter("@BranchId",Branch_Id)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_GetEnterpriseBranchListupdated ", param1);
            if (ds.Rows.Count > 0)
            {
                dtNew.Columns.Add("Branch", typeof(string));
                dtNew.Columns.Add("Empcount", typeof(string));
                dtNew.Columns.Add("Positive", typeof(string));
                dtNew.Columns.Add("Negative", typeof(string));
                dtNew.Columns.Add("Inconclusive", typeof(string));


                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    DataRow row = dtNew.NewRow();
                    int Empcount = 0;
                    int Positive = 0;
                    int Negative = 0;
                    int Inconclusive = 0;
                    SqlParameter[] param = new SqlParameter[]
                    {
                            new SqlParameter("@OrgID",OrgId),
                            new SqlParameter("@BranchID",ds.Rows[i]["ID"].ToString())
                    };
                    DataTable dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEnterpriseCovidDashData", param);
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            Empcount++;
                            string _result = CryptoHelper.Decrypt(dt.Rows[j]["sResult"].ToString());
                            if (_result.ToLower() == "positive")
                            {
                                Positive++;
                            }
                            else if (_result.ToLower() == "negative")
                            {
                                Negative++;
                            }
                            else
                            {
                                Inconclusive++;
                            }
                        }
                    }
                    row["Branch"] = ds.Rows[i]["BranchName"].ToString();
                    row["Empcount"] = Empcount;
                    row["Positive"] = Positive;
                    row["Negative"] = Negative;
                    row["Inconclusive"] = Inconclusive;
                    dtNew.Rows.Add(row);
                }
            }
            return dtNew;
        }
        catch (Exception)
        {
            dtNew = null;
            return dtNew;
        }
    }
    public DataTable GetEmployeeCovidDataforYear(string OrgId, string Branch_Id)
    {
        DataTable dtNew = new DataTable();
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param1 = new SqlParameter[]
            {
               new SqlParameter("@OrgId",OrgId),
               new SqlParameter("@BranchId",Branch_Id)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_GetEnterpriseBranchListupdated ", param1);
            if (ds.Rows.Count > 0)
            {
                dtNew.Columns.Add("Branch", typeof(string));
                dtNew.Columns.Add("Empcount", typeof(string));
                dtNew.Columns.Add("Positive", typeof(string));
                dtNew.Columns.Add("Negative", typeof(string));
                dtNew.Columns.Add("Inconclusive", typeof(string));
                dtNew.Columns.Add("Year", typeof(string));


                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    DataRow row = dtNew.NewRow();
                    int Empcount = 0;
                    int Positive = 0;
                    int Negative = 0;
                    int Inconclusive = 0;
                    SqlParameter[] param = new SqlParameter[]
                    {
                            new SqlParameter("@OrgID",OrgId),
                            new SqlParameter("@BranchID",ds.Rows[i]["ID"].ToString())
                    };
                    DataTable dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEnterpriseCovidDashDataforYear", param);
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            Empcount++;
                            string _result = CryptoHelper.Decrypt(dt.Rows[j]["sResult"].ToString());
                            if (_result.ToLower() == "positive")
                            {
                                Positive++;
                            }
                            else if (_result.ToLower() == "negative")
                            {
                                Negative++;
                            }
                            else
                            {
                                Inconclusive++;
                            }
                        }
                    }
                    row["Branch"] = ds.Rows[i]["BranchName"].ToString();
                    row["Empcount"] = Empcount;
                    row["Positive"] = Positive;
                    row["Negative"] = Negative;
                    row["Inconclusive"] = Inconclusive;
                    row["Year"] = "2021";
                    dtNew.Rows.Add(row);
                }
            }
            return dtNew;
        }
        catch (Exception)
        {
            dtNew = null;
            return dtNew;
        }
    }
    public DataTable GetEmployeeCovidDataforMonth(string OrgId, string Branch_Id)
    {
        DataTable dtNew = new DataTable();
        try
        {
            int count = 12;

            dtNew.Columns.Add("Month", typeof(string));
            dtNew.Columns.Add("Positive", typeof(string));
            dtNew.Columns.Add("Negative", typeof(string));
            dtNew.Columns.Add("Inconclusive", typeof(string));

            for (int i = 1; i <= count; i++)
            {
                DataRow row = dtNew.NewRow();

                int Positive = 0;
                int Negative = 0;
                int Inconclusive = 0;
                SqlParameter[] param = new SqlParameter[]
                    {
                            new SqlParameter("@OrgID",OrgId),
                            new SqlParameter("@Month",i),
                            new SqlParameter("@BranchId",Branch_Id)
                    };
                DataTable dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEnterpriseCovidDashDataformonthUpdate", param);
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        string _result = CryptoHelper.Decrypt(dt.Rows[j]["sResult"].ToString());
                        if (_result.ToLower() == "positive")
                        {
                            Positive = Convert.ToInt32(dt.Rows[j]["empCount"].ToString());
                        }
                        else if (_result.ToLower() == "negative")
                        {
                            Negative = Convert.ToInt32(dt.Rows[j]["empCount"].ToString()); ;
                        }
                        else
                        {
                            Inconclusive = Convert.ToInt32(dt.Rows[j]["empCount"].ToString()); ;
                        }
                    }

                }
                row["Positive"] = Positive;
                row["Negative"] = Negative;
                row["Inconclusive"] = Inconclusive;
                row["Month"] = i;
                dtNew.Rows.Add(row);
            }
            return dtNew;
        }
        catch (Exception)
        {
            dtNew = null;
            return dtNew;
        }
    }
    public DataTable GetEmployeeCovidDataforWeek(string OrgId, string Branch_Id)
    {
        DataTable dtNew = new DataTable();
        try
        {
            dtNew.Columns.Add("Result", typeof(string));
            dtNew.Columns.Add("WEEK1", typeof(string));
            dtNew.Columns.Add("WEEK2", typeof(string));
            dtNew.Columns.Add("WEEK3", typeof(string));
            dtNew.Columns.Add("WEEK4", typeof(string));

            DataRow Prow = dtNew.NewRow();
            DataRow Nrow = dtNew.NewRow();
            DataRow Inrow = dtNew.NewRow();


            int PWeek1 = 0;
            int PWeek2 = 0;
            int PWeek3 = 0;
            int PWeek4 = 0;
            int NWeek1 = 0;
            int NWeek2 = 0;
            int NWeek3 = 0;
            int NWeek4 = 0;
            int InWeek1 = 0;
            int InWeek2 = 0;
            int InWeek3 = 0;
            int InWeek4 = 0;
            SqlParameter[] param = new SqlParameter[]
                    {
                            new SqlParameter("@OrgID",OrgId),
                             new SqlParameter("@BranchId",Branch_Id)
                    };
            DataTable dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEnterpriseCovidDashDataforWeekupdated", param);
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string _result = CryptoHelper.Decrypt(dt.Rows[j]["sResult"].ToString());
                    if (_result.ToLower() == "positive")
                    {
                        if (dt.Rows[j]["weekData"].ToString() == "1")
                        {
                            PWeek1++;
                        }
                        else if (dt.Rows[j]["weekData"].ToString() == "2")
                        {
                            PWeek2++;
                        }
                        else if (dt.Rows[j]["weekData"].ToString() == "3")
                        {
                            PWeek3++;
                        }
                        else if (dt.Rows[j]["weekData"].ToString() == "4")
                        {
                            PWeek4++;
                        }
                    }
                    else if (_result.ToLower() == "negative")
                    {
                        if (dt.Rows[j]["weekData"].ToString() == "1")
                        {
                            NWeek1++;
                        }
                        else if (dt.Rows[j]["weekData"].ToString() == "2")
                        {
                            NWeek2++;
                        }
                        else if (dt.Rows[j]["weekData"].ToString() == "3")
                        {
                            NWeek3++;
                        }
                        else if (dt.Rows[j]["weekData"].ToString() == "4")
                        {
                            NWeek4++;
                        }
                    }
                    else
                    {
                        if (dt.Rows[j]["weekData"].ToString() == "1")
                        {
                            InWeek1++;
                        }
                        else if (dt.Rows[j]["weekData"].ToString() == "2")
                        {
                            InWeek2++;
                        }
                        else if (dt.Rows[j]["weekData"].ToString() == "3")
                        {
                            InWeek3++;
                        }
                        else if (dt.Rows[j]["weekData"].ToString() == "4")
                        {
                            InWeek4++;
                        }
                    }
                }

            }

            Prow["Result"] = "Positive";
            Prow["WEEK1"] = PWeek1;
            Prow["WEEK2"] = PWeek2;
            Prow["WEEK3"] = PWeek3;
            Prow["WEEK4"] = PWeek4;

            Nrow["Result"] = "Negative";
            Nrow["WEEK1"] = NWeek1;
            Nrow["WEEK2"] = NWeek2;
            Nrow["WEEK3"] = NWeek3;
            Nrow["WEEK4"] = NWeek4;

            Inrow["Result"] = "Inconclusive";
            Inrow["WEEK1"] = InWeek1;
            Inrow["WEEK2"] = InWeek2;
            Inrow["WEEK3"] = InWeek3;
            Inrow["WEEK4"] = InWeek4;
            dtNew.Rows.Add(Prow);
            dtNew.Rows.Add(Nrow);
            dtNew.Rows.Add(Inrow);
            return dtNew;
        }
        catch (Exception)
        {
            dtNew = null;
            return dtNew;
        }
    }
    public DataTable GetEmployeeCovidDataForWeekByBranch(string OrgId, string Branch_Id)
    {
        DataTable dtNew = new DataTable();
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param1 = new SqlParameter[]
            {
               new SqlParameter("@OrgId",OrgId),
               new SqlParameter("@BranchId",Branch_Id)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_GetEnterpriseBranchListupdated ", param1);
            if (ds.Rows.Count > 0)
            {
                dtNew.Columns.Add("Branch", typeof(string));
                dtNew.Columns.Add("Empcount", typeof(string));
                dtNew.Columns.Add("Positive", typeof(string));
                dtNew.Columns.Add("Negative", typeof(string));
                dtNew.Columns.Add("Inconclusive", typeof(string));


                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    DataRow row = dtNew.NewRow();
                    int Empcount = 0;
                    int Positive = 0;
                    int Negative = 0;
                    int Inconclusive = 0;
                    SqlParameter[] param = new SqlParameter[]
                    {
                            new SqlParameter("@OrgID",OrgId),
                            new SqlParameter("@BranchID",ds.Rows[i]["ID"].ToString())
                    };
                    DataTable dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetEnterpriseCovidDashDataforWeekBranchVise", param);
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            Empcount++;
                            string _result = CryptoHelper.Decrypt(dt.Rows[j]["sResult"].ToString());
                            if (_result.ToLower() == "positive")
                            {
                                Positive++;
                            }
                            else if (_result.ToLower() == "negative")
                            {
                                Negative++;
                            }
                            else
                            {
                                Inconclusive++;
                            }
                        }
                    }
                    row["Branch"] = ds.Rows[i]["BranchName"].ToString();
                    row["Empcount"] = Empcount;
                    row["Positive"] = Positive;
                    row["Negative"] = Negative;
                    row["Inconclusive"] = Inconclusive;
                    dtNew.Rows.Add(row);
                }
            }
            return dtNew;
        }
        catch (Exception)
        {
            dtNew = null;
            return dtNew;
        }
    }
    public DataTable GetDashboardTestCountGenderwise(DataTable dt)
    {
        DataTable dtNew = new DataTable();
        try
        {
            if (dt.Rows.Count > 0)
            {
                int dtRows = dt.Rows.Count;
                int _EmpSum = 0;
                dtNew.Columns.Add("Gender", typeof(string));
                dtNew.Columns.Add("EmpCount", typeof(string));
                dtNew.Columns.Add("TotalSum", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dtNew.NewRow();
                    _EmpSum += Convert.ToInt32(dt.Rows[i]["EmpCount"].ToString());
                    if (i == 0)
                    {
                        row["Gender"] = dt.Rows[0]["sGender"].ToString();
                        row["EmpCount"] = dt.Rows[0]["EmpCount"].ToString();
                        row["TotalSum"] = _EmpSum;
                        dtNew.Rows.Add(row);
                    }
                    else
                    {
                        row["Gender"] = dt.Rows[1]["sGender"].ToString();
                        row["EmpCount"] = dt.Rows[1]["EmpCount"].ToString();
                        row["TotalSum"] = _EmpSum;
                        dtNew.Rows.Add(row);
                    }
                }
            }
            return dtNew;
        }
        catch (Exception)
        {
            dtNew = null;
            return dtNew;
        }
    }
    public DataTable GetDashboardRevenuCountAgevise(DataTable dt)
    {
        DataTable dtNew = new DataTable();
        try
        {
            if (dt.Rows.Count > 0)
            {
                int dtRows = dt.Rows.Count;
                int _one = 0;
                int _two = 0;
                int _three = 0;
                int _four = 0;
                int _age = 0;

                dtNew.Columns.Add("15-30", typeof(string));
                dtNew.Columns.Add("30-45", typeof(string));
                dtNew.Columns.Add("45-60", typeof(string));
                dtNew.Columns.Add("61-100", typeof(string));
                DataRow row = dtNew.NewRow();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _age = GetAge(dt.Rows[i]["sBirthDate"].ToString());
                    if (_age >= 15 && _age <= 30)
                    {
                        _one = _one + 1;
                    }
                    else if (_age > 30 && _age <= 45)
                    {
                        _two = _two + 1;
                    }
                    else if (_age > 45 && _age <= 60)
                    {
                        _three = _three + 1;
                    }
                    else
                    {
                        _four = _four + 1;
                    }
                }
                row["15-30"] = _one;
                row["30-45"] = _two;
                row["45-60"] = _three;
                row["61-100"] = _four;
                dtNew.Rows.Add(row);
            }
            return dtNew;
        }
        catch (Exception)
        {
            dtNew = null;
            return dtNew;
        }
    }
    public static int GetAge(string birthDay)
    {
        string[] _splitDob = birthDay.Split('/');
        int _year = Convert.ToInt32(_splitDob[2]);
        int today = Convert.ToInt32(DateTime.Now.Year.ToString());
        int age = today - _year;
        return age;
    }
    public double CalculateTotal(int Count, int TotalCount)
    {
        double percentage;
        try
        {
            percentage = Count * 100 / TotalCount;
            return percentage;
        }
        catch (Exception)
        {
            percentage = 0;
            return percentage;
        }
    }
}
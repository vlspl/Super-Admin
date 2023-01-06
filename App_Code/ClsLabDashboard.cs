using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
using System.Collections.Generic;
using System.Globalization;

/// <summary>
/// Summary description for ClsLabDashboard
/// </summary>
public class ClsLabDashboard
{
    DataAccessLayer DAL = new DataAccessLayer();
    public ClsLabDashboard()
    {
    }
    public DataSet getMyBookingsForDashboard(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetMyBookingsByIabIdForDashboard " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable getMyBookingsForDashboardByDate(string labId, string StartDate, string EndDate)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@LabId",labId),
                new SqlParameter("@startDate",StartDate),
                new SqlParameter("@EndDate",EndDate)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_GetMyBookingsByIabIdForDashboardByDate ", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet GetDashboardDetails(string labId, string StartDate, string EndDate)
    {
        DataSet ds = new DataSet();
        DataSet dsNew = new DataSet();
        try
        {
            ds = GetDashboardAnalyticswithDateRange(labId,StartDate,EndDate);
            if (ds.Tables.Count > 0)
            {
                DataTable dt1 = new DataTable();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt1 = GetDashboardTestCountReport(ds.Tables[0]);
                }
                dsNew.Tables.Add(dt1);
                DataTable dt2 = new DataTable();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    dt2 = GetDashboardTestRevenuCount(ds.Tables[1]);
                }
                dsNew.Tables.Add(dt2);
                DataTable dt3 = new DataTable();
                if (ds.Tables[2].Rows.Count > 0)
                {
                    dt3 = GetDashboardBookingPaidDues(ds.Tables[2]);
                }
                dsNew.Tables.Add(dt3);
                DataTable dt4 = new DataTable();
                if (ds.Tables[3].Rows.Count > 0)
                {
                    dt4 = GetDashboardTestCountGenderwise(ds.Tables[3]);
                }
                dsNew.Tables.Add(dt4);
                DataTable dt5 = new DataTable();
                if (ds.Tables[4].Rows.Count > 0)
                {
                    dt5 = GetDashboardDoctorReport(ds.Tables[4]);
                }
                dsNew.Tables.Add(dt5);
                DataTable dt6 = new DataTable();
                if (ds.Tables[5].Rows.Count > 0)
                {
                    dt6 = GetDashboardRevenuCountAgevise(ds.Tables[5]);
                }
                dsNew.Tables.Add(dt6);
            }
            DataTable dt7 = new DataTable();
            dt7 = DashboardKPIByDate(labId,StartDate,EndDate);
            dsNew.Tables.Add(dt7);

            DataTable dt8 = new DataTable();
            dt8 = getMyBookingsForDashboardByDate(labId,StartDate,EndDate);
            dsNew.Tables.Add(dt8);
            return dsNew;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable DashboardKPI(string labId)
    {
        DataTable ds = new DataTable();
        try
        {
            ds = DAL.GetDataTable("Sp_Dashboard " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable DashboardKPIByDate(string labId, string StartDate, string EndDate)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@LabId",labId),
                new SqlParameter("@startDate",StartDate),
                new SqlParameter("@EndDate",EndDate)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_DashboardByDate ", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet GetDashboardAnalyticswithDateRange(string labId, string StartDate, string EndDate)
    {
        DataSet ds = new DataSet();
      
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@LabId",labId),
                new SqlParameter("@startDate",StartDate),
                new SqlParameter("@EndDate",EndDate)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_DashboardAnalyticswithDateRange ", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable GetDashboardTestCountReport(DataTable dt)
    {
        DataTable dtNew = new DataTable();
        try
        {
            if (dt.Rows.Count > 0)
            {
                int dtRows = dt.Rows.Count;
                int _testSum = 0;
                int _OthertestCount = 0;
                dtNew.Columns.Add("TestName", typeof(string));
                dtNew.Columns.Add("TestCount", typeof(string));
                dtNew.Columns.Add("TestSum", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _testSum += Convert.ToInt32(dt.Rows[i]["testCount"].ToString());
                    DataRow row = dtNew.NewRow();
                    if (i == 0)
                    {
                        row["TestName"] = dt.Rows[0]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[0]["testCount"].ToString();
                        row["TestSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 1)
                    {
                        row["TestName"] = dt.Rows[1]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[1]["testCount"].ToString();
                        row["TestSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 2)
                    {
                        row["TestName"] = dt.Rows[2]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[2]["testCount"].ToString();
                        row["TestSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 3)
                    {
                        row["TestName"] = dt.Rows[3]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[3]["testCount"].ToString();
                        row["TestSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if ((dtRows - 1) == i)
                    {
                        row["TestName"] = "Other";
                        row["TestCount"] = _OthertestCount;
                        row["TestSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else
                    {
                        _OthertestCount += Convert.ToInt32(dt.Rows[i]["testCount"].ToString());
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
    public DataTable GetDashboardTestRevenuCount(DataTable dt)
    {
        DataTable dtNew = new DataTable();
        try
        {
            if (dt.Rows.Count > 0)
            {
                int dtRows = dt.Rows.Count;
                int _totaltestSum = 0;
                int _OthertestCount = 0;
                dtNew.Columns.Add("TestName", typeof(string));
                dtNew.Columns.Add("TestCount", typeof(string));
                dtNew.Columns.Add("TotalTestSum", typeof(string));
                dtNew.Columns.Add("TestCode", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _totaltestSum += Convert.ToInt32(dt.Rows[i]["testSum"].ToString() != "" ? dt.Rows[i]["testSum"].ToString() : "0");
                    DataRow row = dtNew.NewRow();
                    if (i == 0)
                    {
                        row["TestCode"] = dt.Rows[0]["sTestCode"].ToString();
                        row["TestName"] = dt.Rows[0]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[0]["testSum"].ToString();
                        row["TotalTestSum"] = _totaltestSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 1)
                    {
                        row["TestCode"] = dt.Rows[1]["sTestCode"].ToString();
                        row["TestName"] = dt.Rows[1]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[1]["testSum"].ToString();
                        row["TotalTestSum"] = _totaltestSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 2)
                    {
                        row["TestCode"] = dt.Rows[2]["sTestCode"].ToString();
                        row["TestName"] = dt.Rows[2]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[2]["testSum"].ToString();
                        row["TotalTestSum"] = _totaltestSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 3)
                    {
                        row["TestCode"] = dt.Rows[3]["sTestCode"].ToString();
                        row["TestName"] = dt.Rows[3]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[3]["testSum"].ToString();
                        row["TotalTestSum"] = _totaltestSum;
                        dtNew.Rows.Add(row);
                    }
                    else if ((dtRows - 1) == i)
                    {
                        _OthertestCount += Convert.ToInt32(dt.Rows[i]["testSum"].ToString() != "" ? dt.Rows[i]["testSum"].ToString() : "0");
                        row["TestCode"] = "Other";
                        row["TestName"] = "Other";
                        row["TestCount"] = _OthertestCount;
                        row["TotalTestSum"] = _totaltestSum;
                        dtNew.Rows.Add(row);
                    }
                    else
                    {
                        _OthertestCount += Convert.ToInt32(dt.Rows[i]["testSum"].ToString() != "" ?dt.Rows[i]["testSum"].ToString():"0");
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
    public DataTable GetDashboardBookingPaidDues(DataTable dt)
    {
        DataTable dtNew = new DataTable();
        try
        {
            if (dt.Rows.Count > 0)
            {
                int dtRows = dt.Rows.Count;
                int _testSum = 0;
                dtNew.Columns.Add("PaymentStatus", typeof(string));
                dtNew.Columns.Add("Amount", typeof(string));
                dtNew.Columns.Add("TotalSum", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _testSum += Convert.ToInt32(dt.Rows[i]["amount"].ToString());
                    DataRow row = dtNew.NewRow();
                    if (i == 0)
                    {
                        row["PaymentStatus"] = "Paid";
                        row["Amount"] = dt.Rows[0]["amount"].ToString();
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else
                    {
                        row["PaymentStatus"] = "Due";
                        row["Amount"] = dt.Rows[1]["amount"].ToString();
                        row["TotalSum"] = _testSum;
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
    public DataTable GetDashboardTestCountGenderwise(DataTable dt)
    {
        DataTable dtNew = new DataTable();
        try
        {
            if (dt.Rows.Count > 0)
            {
                int dtRows = dt.Rows.Count;
                int _testSum = 0;
                dtNew.Columns.Add("Gender", typeof(string));
                dtNew.Columns.Add("TestCount", typeof(string));
                dtNew.Columns.Add("TotalSum", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _testSum += Convert.ToInt32(dt.Rows[i]["totalTest"].ToString());
                    DataRow row = dtNew.NewRow();
                    if (i == 0)
                    {
                        row["Gender"] = dt.Rows[0]["sGender"].ToString();
                        row["TestCount"] = dt.Rows[0]["totalTest"].ToString();
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else
                    {
                        row["Gender"] = dt.Rows[1]["sGender"].ToString();
                        row["TestCount"] = dt.Rows[1]["totalTest"].ToString();
                        row["TotalSum"] = _testSum;
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
    public DataTable GetDashboardDoctorReport(DataTable dt)
    {
        DataTable dtNew = new DataTable();
        try
        {
            if (dt.Rows.Count > 0)
            {
                int dtRows = dt.Rows.Count;
                int _testSum = 0;
                int _OthertestCount = 0;
                dtNew.Columns.Add("DocName", typeof(string));
                dtNew.Columns.Add("DocAmount", typeof(string));
                dtNew.Columns.Add("TotalSum", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _testSum += Convert.ToInt32(dt.Rows[i]["Amount"].ToString());
                    DataRow row = dtNew.NewRow();
                    if (i == 0)
                    {
                        row["DocName"] = dt.Rows[0]["sFullName"].ToString();
                        row["DocAmount"] = dt.Rows[0]["Amount"].ToString();
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 1)
                    {
                        row["DocName"] = dt.Rows[1]["sFullName"].ToString();
                        row["DocAmount"] = dt.Rows[1]["Amount"].ToString();
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 2)
                    {
                        row["DocName"] = dt.Rows[2]["sFullName"].ToString();
                        row["DocAmount"] = dt.Rows[2]["Amount"].ToString();
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 3)
                    {
                        row["DocName"] = dt.Rows[3]["sFullName"].ToString();
                        row["DocAmount"] = dt.Rows[3]["Amount"].ToString();
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if ((dtRows - 1) == i)
                    {
                        row["DocName"] = "Other";
                        row["DocAmount"] = _OthertestCount;
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else
                    {
                        _OthertestCount += Convert.ToInt32(dt.Rows[i]["Amount"].ToString());
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
                int _testSum = 0;
                int _one = 0;
                int _two = 0;
                int _three = 0;
                int _four = 0;
                int _five = 0;
                int _age = 0;

                dtNew.Columns.Add("0-20", typeof(string));
                dtNew.Columns.Add("21-40", typeof(string));
                dtNew.Columns.Add("41-60", typeof(string));
                dtNew.Columns.Add("61-80", typeof(string));
                dtNew.Columns.Add("81-100", typeof(string));
                dtNew.Columns.Add("TotalAmt", typeof(string));
                DataRow row = dtNew.NewRow();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _age = GetAge(dt.Rows[i]["sBirthDate"].ToString());

                    _testSum += Convert.ToInt32(dt.Rows[i]["amt"].ToString());

                    if (_age >= 0 && _age <= 20)
                    {
                        _one += Convert.ToInt32(dt.Rows[i]["amt"].ToString());
                    }
                    else if (_age > 20 && _age <= 40)
                    {
                        _two += Convert.ToInt32(dt.Rows[i]["amt"].ToString());
                    }
                    else if (_age > 40 && _age <= 60)
                    {
                        _three += Convert.ToInt32(dt.Rows[i]["amt"].ToString());
                    }
                    else if (_age > 60 && _age <= 80)
                    {
                        _four += Convert.ToInt32(dt.Rows[i]["amt"].ToString());
                    }
                    else
                    {
                        _five += Convert.ToInt32(dt.Rows[i]["amt"].ToString());
                    }
                }
                row["0-20"] = _one;
                row["21-40"] = _two;
                row["41-60"] = _three;
                row["61-80"] = _four;
                row["81-100"] = _five;
                row["TotalAmt"] = _testSum;
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
    public DataTable GetDashboardTestCountReport(string labId)
    {
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_DashboardTestCountReport " + labId);
            if (dt.Rows.Count > 0)
            {
                int dtRows = dt.Rows.Count;
                int _testSum = 0;
                int _OthertestCount = 0;
                dtNew.Columns.Add("TestName", typeof(string));
                dtNew.Columns.Add("TestCount", typeof(string));
                dtNew.Columns.Add("TestSum", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _testSum += Convert.ToInt32(dt.Rows[i]["testCount"].ToString());
                    DataRow row = dtNew.NewRow();
                    if (i == 0)
                    {
                        row["TestName"] = dt.Rows[0]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[0]["testCount"].ToString();
                        row["TestSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 1)
                    {
                        row["TestName"] = dt.Rows[1]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[1]["testCount"].ToString();
                        row["TestSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 2)
                    {
                        row["TestName"] = dt.Rows[2]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[2]["testCount"].ToString();
                        row["TestSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 3)
                    {
                        row["TestName"] = dt.Rows[3]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[3]["testCount"].ToString();
                        row["TestSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if ((dtRows - 1) == i)
                    {
                        row["TestName"] = "Other";
                        row["TestCount"] = _OthertestCount;
                        row["TestSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else
                    {
                        _OthertestCount += Convert.ToInt32(dt.Rows[i]["testCount"].ToString());
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
    public DataTable GetDashboardTestRevenuCount(string labId)
    {
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_DashboardTestRevenuCount " + labId);
            if (dt.Rows.Count > 0)
            {
                int dtRows = dt.Rows.Count;
                int _totaltestSum = 0;
                int _OthertestCount = 0;
                dtNew.Columns.Add("TestName", typeof(string));
                dtNew.Columns.Add("TestCount", typeof(string));
                dtNew.Columns.Add("TotalTestSum", typeof(string));
                dtNew.Columns.Add("TestCode", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _totaltestSum += Convert.ToInt32(dt.Rows[i]["testSum"].ToString());
                    DataRow row = dtNew.NewRow();
                    if (i == 0)
                    {
                        row["TestCode"] = dt.Rows[0]["sTestCode"].ToString();
                        row["TestName"] = dt.Rows[0]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[0]["testSum"].ToString();
                        row["TotalTestSum"] = _totaltestSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 1)
                    {
                        row["TestCode"] = dt.Rows[1]["sTestCode"].ToString();
                        row["TestName"] = dt.Rows[1]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[1]["testSum"].ToString();
                        row["TotalTestSum"] = _totaltestSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 2)
                    {
                        row["TestCode"] = dt.Rows[2]["sTestCode"].ToString();
                        row["TestName"] = dt.Rows[2]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[2]["testSum"].ToString();
                        row["TotalTestSum"] = _totaltestSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 3)
                    {
                        row["TestCode"] = dt.Rows[3]["sTestCode"].ToString();
                        row["TestName"] = dt.Rows[3]["sTestName"].ToString();
                        row["TestCount"] = dt.Rows[3]["testSum"].ToString();
                        row["TotalTestSum"] = _totaltestSum;
                        dtNew.Rows.Add(row);
                    }
                    else if ((dtRows - 1) == i)
                    {
                        _OthertestCount += Convert.ToInt32(dt.Rows[i]["testSum"].ToString());
                        row["TestCode"] = "Other";
                        row["TestName"] = "Other";
                        row["TestCount"] = _OthertestCount;
                        row["TotalTestSum"] = _totaltestSum;
                        dtNew.Rows.Add(row);
                    }
                    else
                    {
                        _OthertestCount += Convert.ToInt32(dt.Rows[i]["testSum"].ToString());
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
    public DataTable GetDashboardBookingPaidDues(string labId)
    {
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_DashboardBookingPaidandDues " + labId);
            if (dt.Rows.Count > 0)
            {
                int dtRows = dt.Rows.Count;
                int _testSum = 0;
                dtNew.Columns.Add("PaymentStatus", typeof(string));
                dtNew.Columns.Add("Amount", typeof(string));
                dtNew.Columns.Add("TotalSum", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _testSum += Convert.ToInt32(dt.Rows[i]["amount"].ToString());
                    DataRow row = dtNew.NewRow();
                    if (i == 0)
                    {
                        row["PaymentStatus"] = "Paid";
                        row["Amount"] = dt.Rows[0]["amount"].ToString();
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else
                    {
                        row["PaymentStatus"] = "Due";
                        row["Amount"] = dt.Rows[1]["amount"].ToString();
                        row["TotalSum"] = _testSum;
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
    public DataTable GetDashboardTestCountGenderwise(string labId)
    {
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_DashboardTestCountGendervise " + labId);
            if (dt.Rows.Count > 0)
            {
                int dtRows = dt.Rows.Count;
                int _testSum = 0;
                dtNew.Columns.Add("Gender", typeof(string));
                dtNew.Columns.Add("TestCount", typeof(string));
                dtNew.Columns.Add("TotalSum", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _testSum += Convert.ToInt32(dt.Rows[i]["totalTest"].ToString());
                    DataRow row = dtNew.NewRow();
                    if (i == 0)
                    {
                        row["Gender"] = dt.Rows[0]["sGender"].ToString();
                        row["TestCount"] = dt.Rows[0]["totalTest"].ToString();
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else
                    {
                        row["Gender"] = dt.Rows[1]["sGender"].ToString();
                        row["TestCount"] = dt.Rows[1]["totalTest"].ToString();
                        row["TotalSum"] = _testSum;
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
    public DataTable GetDashboardDoctorReport(string labId)
    {
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_DashboardDoctorbusiness " + labId);
            if (dt.Rows.Count > 0)
            {
                int dtRows = dt.Rows.Count;
                int _testSum = 0;
                int _OthertestCount = 0;
                dtNew.Columns.Add("DocName", typeof(string));
                dtNew.Columns.Add("DocAmount", typeof(string));
                dtNew.Columns.Add("TotalSum", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _testSum += Convert.ToInt32(dt.Rows[i]["Amount"].ToString());
                    DataRow row = dtNew.NewRow();
                    if (i == 0)
                    {
                        row["DocName"] = dt.Rows[0]["sFullName"].ToString();
                        row["DocAmount"] = dt.Rows[0]["Amount"].ToString();
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 1)
                    {
                        row["DocName"] = dt.Rows[1]["sFullName"].ToString();
                        row["DocAmount"] = dt.Rows[1]["Amount"].ToString();
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 2)
                    {
                        row["DocName"] = dt.Rows[2]["sFullName"].ToString();
                        row["DocAmount"] = dt.Rows[2]["Amount"].ToString();
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if (i == 3)
                    {
                        row["DocName"] = dt.Rows[3]["sFullName"].ToString();
                        row["DocAmount"] = dt.Rows[3]["Amount"].ToString();
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else if ((dtRows - 1) == i)
                    {
                        row["DocName"] = "Other";
                        row["DocAmount"] = _OthertestCount;
                        row["TotalSum"] = _testSum;
                        dtNew.Rows.Add(row);
                    }
                    else
                    {
                        _OthertestCount += Convert.ToInt32(dt.Rows[i]["Amount"].ToString());
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
    public DataTable GetDashboardRevenuCountAgevise(string labId)
    {
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_DashboardPatientAgeviseTestCount " + labId);
            if (dt.Rows.Count > 0)
            {
                int dtRows = dt.Rows.Count;
                int _testSum = 0;
                int _one = 0;
                int _two = 0;
                int _three = 0;
                int _four = 0;
                int _five = 0;
                int _age = 0;

                dtNew.Columns.Add("0-20", typeof(string));
                dtNew.Columns.Add("21-40", typeof(string));
                dtNew.Columns.Add("41-60", typeof(string));
                dtNew.Columns.Add("61-80", typeof(string));
                dtNew.Columns.Add("81-100", typeof(string));
                dtNew.Columns.Add("TotalAmt", typeof(string));
                DataRow row = dtNew.NewRow();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _age = GetAge(dt.Rows[i]["sBirthDate"].ToString());

                    _testSum += Convert.ToInt32(dt.Rows[i]["amt"].ToString());

                    if (_age >= 0 && _age <= 20)
                    {
                        _one += Convert.ToInt32(dt.Rows[i]["amt"].ToString());
                    }
                    else if (_age > 20 && _age <= 40)
                    {
                        _two += Convert.ToInt32(dt.Rows[i]["amt"].ToString());
                    }
                    else if (_age > 40 && _age <= 60)
                    {
                        _three += Convert.ToInt32(dt.Rows[i]["amt"].ToString());
                    }
                    else if (_age > 60 && _age <= 80)
                    {
                        _four += Convert.ToInt32(dt.Rows[i]["amt"].ToString());
                    }
                    else
                    {
                        _five += Convert.ToInt32(dt.Rows[i]["amt"].ToString());
                    }
                }
                row["0-20"] = _one;
                row["21-40"] = _two;
                row["41-60"] = _three;
                row["61-80"] = _four;
                row["81-100"] = _five;
                row["TotalAmt"] = _testSum;
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
    public DataSet GetDashboardAnalytics(string labId)
    {
        DataSet ds = new DataSet();
        DataSet dsNew = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_DashboardAnalytics " + labId);
            if (ds.Tables.Count > 0)
            {

                DataTable dt1 = new DataTable();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt1 = GetDashboardTestCountReport(ds.Tables[0]);
                }
                dsNew.Tables.Add(dt1);
                DataTable dt2 = new DataTable();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    dt2 = GetDashboardTestRevenuCount(ds.Tables[1]);
                }
                dsNew.Tables.Add(dt2);
                DataTable dt3 = new DataTable();
                if (ds.Tables[2].Rows.Count > 0)
                {
                    dt3 = GetDashboardBookingPaidDues(ds.Tables[2]);
                }
                dsNew.Tables.Add(dt3);
                DataTable dt4 = new DataTable();
                if (ds.Tables[3].Rows.Count > 0)
                {
                    dt4 = GetDashboardTestCountGenderwise(ds.Tables[3]);
                }
                dsNew.Tables.Add(dt4);
                DataTable dt5 = new DataTable();
                if (ds.Tables[4].Rows.Count > 0)
                {
                    dt5 = GetDashboardDoctorReport(ds.Tables[4]);
                }
                dsNew.Tables.Add(dt5);
                //DataTable dt6 = new DataTable();
                //if (ds.Tables[5].Rows.Count > 0)
                //{
                //    dt6 = GetDashboardRevenuCountAgevise(ds.Tables[5]);
                //}
                //dsNew.Tables.Add(dt6);
            }
            return dsNew;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
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
}
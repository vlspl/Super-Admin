using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsBookDetails
{
    DataAccessLayer DAL = new DataAccessLayer();
    public ClsBookDetails()
    {

    }
    public DataSet getBookingDetails(string labId, string bookLabId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@labId",labId),
                new SqlParameter("@bookLabId",bookLabId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetBookingDetails", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getInvoiceBookingDetails(string bookLabId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@bookLabId",bookLabId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetPatientandLabDetails", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getInvoiceBookingDetailswithoutdoctor(string bookLabId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@bookLabId",bookLabId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetPatientandLabDetailsUpdated", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getInvoiceBookingDetails(string bookLabId, string LabId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@bookLabId",bookLabId),
                 new SqlParameter("@LabId",LabId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetPatientandLabDetailsUpdated2", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getInvoiceBookingDetailswithoutdoctor(string bookLabId,string LabId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@bookLabId",bookLabId),
                new SqlParameter("@LabId",LabId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetPatientandLabDetailsUpdated1", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getBookTestDetails(string labId, string bookLabId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@bookLabId",bookLabId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetBookTestDetailsUpdated", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getBookTestDetailslist(string labId, string bookLabId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@bookLabId",bookLabId),
                new SqlParameter("@LabId",labId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetBookTestDetailsbyBookingIdandLabId", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getBookTestDetailsByBookiIdandPaymentId(int Id, string bookLabId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Id",Id),
                new SqlParameter("@bookLabId",bookLabId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetPaymentInvoiceDetailss", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable  GetPaymentDetails(string bookLabId)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@bookLabId",bookLabId)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_GetPaymentHistory", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataTable GetPaymentInvoiceDetails(string bookLabId)
    {
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@bookLabId",bookLabId)
            };
            ds = DAL.ExecuteStoredProcedureDataTable("Sp_GetPaymentInvoiceDetails", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getBookTestDetailsUserMobId(string bookLabId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@bookLabId",bookLabId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetBookTestDetailsUserMobId", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int updateBookingStatus(string bookLabId, string bookStatus, string comment)
    {
        try
        {
            string labId = "";
            string patientId = "";
            string doctorId = "";
            string confirmedAt = "";

            if (bookStatus.ToLower() == "confirmed")
            {
                string[] SplitDateTime = DateTime.Now.ToString().Split(' ');
                string Date = SplitDateTime[0];
                string time = SplitDateTime[1];
                //string x="/";
                if (Date.Contains("/"))
                {
                    string[] fullDate = Date.Split('/');

                    string date = fullDate[0];
                    string Month = fullDate[1];
                    string Year = fullDate[2];
                    string DDMMYY = date + "/" + Month + "/" + Year;
                    confirmedAt = DDMMYY + " " + time;
                }
                else if (Date.Contains("-"))
                {
                    string[] fullDate = Date.Split('-');
                    string date = fullDate[0];
                    string Month = fullDate[1];
                    string Year = fullDate[2];
                    string DDMMYY = date + "/" + Month + "/" + Year;
                    confirmedAt = DDMMYY + " " + time;
                }
                else
                {
                    confirmedAt = Date + " " + time;
                }
            }

            SqlParameter[] param = new SqlParameter[]
                  {
                        new SqlParameter("@bookLabId",bookLabId),
                        new SqlParameter("@sBookStatus",bookStatus),
                        new SqlParameter("@sComment",comment),
                          new SqlParameter("@ConfirmedAt",confirmedAt),
                 };
            DataTable dt = DAL.ExecuteStoredProcedureDataTable("Sp_UpdateBookingStatus", param);
            if (dt.Rows.Count > 0)
            {
                labId = dt.Rows[0]["sLabId"].ToString();
                patientId = dt.Rows[0]["sPatientId"].ToString();
                doctorId = dt.Rows[0]["sDoctorId"].ToString();
            };
            if (bookStatus.ToLower() == "confirmed")
            {

                SqlParameter[] param1 = new SqlParameter[]
                      {
                        new SqlParameter("@patientId",patientId),
                        new SqlParameter("@labId",labId )
                    };
                DAL.ExecuteStoredProcedure("Sp_InsertlabPatientByLabIdandPatientId", param1);
                if (doctorId != "0" && doctorId != "" && doctorId != null)
                {
                    SqlParameter[] param2 = new SqlParameter[]
                             {
                                    new SqlParameter("@doctorId",doctorId),
                                     new SqlParameter("@labId",labId )
                             };
                    DAL.ExecuteStoredProcedure("Sp_InsertlabDoctorByLabIdandDoctorId", param2);
                }
            }
            //if update successfull return 1
            return 1;
        }
        catch (Exception)
        {
            //return 0 if error
            return 0;
        }
    }
    public int updatePaymentStatus(string bookLabId, string testStatus, string paymentStatus, string advancePayment)
    {
        int result;
        try
        {

            SqlParameter[] param = new SqlParameter[]
                  {
                        new SqlParameter("@sTestStatus",testStatus),
                        new SqlParameter("@sPaymentStatus",paymentStatus ),
                        new SqlParameter("@sAdvancePayment",advancePayment),
                        new SqlParameter("@bookLabId",bookLabId ),
                        new SqlParameter("@returnval",SqlDbType.Int)
                 };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdatePaymentStatusByBookLabId", param);
        }
        catch (Exception)
        {
            //return 0 if error
            result = 0;
        }
        return result;
    }
    public int AddPaymentDetails(string bookingId, string Amount, string paymentMethod,string CreatedBy)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
                  {
                        new SqlParameter("@BookingId",bookingId),
                        new SqlParameter("@Amount",Amount),
                        new SqlParameter("@PaymentVia",paymentMethod),
                        new SqlParameter("@CreatedBy",CreatedBy),
                        new SqlParameter("@returnval",SqlDbType.Int)
                 };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddBookingPaymentDetails", param);
        }
        catch (Exception)
        {
            //return 0 if error
            result = 0;
        }
        return result;
    }
    public int ResheduleTestDate(string bookLabId, string TestDate, string TimeSlot, string AppinmentType)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
                  {
                        new SqlParameter("@BookId",bookLabId),
                        new SqlParameter("@TestDate",TestDate),
                        new SqlParameter("@TimeSlot",TimeSlot ),
                        new SqlParameter("@AppoinmentType",AppinmentType),
                        new SqlParameter("@BookStatus","Confirmed"),
                        new SqlParameter("@returnval",SqlDbType.Int)
                 };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_RescheduleTestDateUpdated", param);
        }
        catch (Exception)
        {
            //return 0 if error
            result = 0;
        }
        return result;
    }
    public int sharedReportToDoctor(string sPaientId, string DoctorId, string ReportId, string Shared)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
                  {
                        new SqlParameter("@sPaientId",sPaientId),
                        new SqlParameter("@sDoctorId",DoctorId ),
                        new SqlParameter("@sReportId",ReportId),
                        new SqlParameter("@sShared",Shared ),
                        new SqlParameter("@returnval",SqlDbType.Int)
                 };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_SharedReportToDoctor", param);
        }
        catch (Exception)
        {
            //return 0 if error
            result = 0;
        }
        return result;
    }
    public int UpdatePaymentStatusbyBookingId(string bookLabId, string paymentStatus)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
                  {
                       
                        new SqlParameter("@PaymentStatus",paymentStatus ),
                        new SqlParameter("@bookLabId",bookLabId ),
                        new SqlParameter("@returnval",SqlDbType.Int)
                 };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdatePaymentStatus", param);
        }
        catch (Exception)
        {
            result = 0;
        }
        return result;
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsBookTest
{
    DataAccessLayer DAL = new DataAccessLayer();
    public ClsBookTest()
    {
    }
    public int bookTest(string labId, string patientId, string doctorId,
        string timeslot, string bookStatus, string testStatus, string bookMode, string testDate, string fees,
        string testId, string AppointmentType, string TestPrice)
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
                        new SqlParameter("@sBookRequestedAt", ""),
                        new SqlParameter("@sBookConfirmedAt",""),
                        new SqlParameter("@sTimeSlot",timeslot),
                        new SqlParameter("@sBookStatus",bookStatus),
                        new SqlParameter("@sBookMode",bookMode),
                        new SqlParameter("@sTestStatus",testStatus),
                        new SqlParameter("@sTestDate",testDate),
                        new SqlParameter("@sFees",fees),
                        new SqlParameter("@AppointmentType",AppointmentType),
                        new SqlParameter("@returnval",SqlDbType.Int)
                 };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddBookTest", param);
            if (result > 0)
            {
                bookLabId = result;
                string[] testIdList = testId.Split(',');
                string[] testPrice = TestPrice.Split(',');
                for (int x = 0; x < testIdList.Length; x++)
                {
                    var _testId = testIdList[x];
                    var _testprice = testPrice[x];
                    SqlParameter[] param1 = new SqlParameter[]
                         {
                             new SqlParameter("@bookingId",bookLabId),
                             new SqlParameter("@TestId",_testId),
                             new SqlParameter("@TestPrice",_testprice),
                             new SqlParameter("@Returnval",SqlDbType.Int)
                         };
                    result = DAL.ExecuteStoredProcedureRetnInt("WS_Sp_AddBookAppoinmentTestDetails", param1);
                }
            }
        }
        catch (Exception)
        {
            result = 0;
        }
        return result;
    }
    public int bookTestFromPrescription(string labId, string bookStatus, string bookMode, string fees, string testId, string TestPrice)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[]
                  {
                        new SqlParameter("@sBookLabId",labId),
                        new SqlParameter("@sBookStatus",bookStatus),
                        new SqlParameter("@sBookMode",bookMode),
                        new SqlParameter("@sFees",fees),
                        new SqlParameter("@returnval",SqlDbType.Int)
                  };
            result = DAL.ExecuteStoredProcedureRetnInt("Sp_bookTestFromPrescription", param);

            string[] testIdList = testId.Split(',');
            string[] testPrice = TestPrice.Split(',');
            for (int x = 0; x < testIdList.Length; x++)
            {
                var _testId = testIdList[x];
                var _testprice = testPrice[x];
                SqlParameter[] param1 = new SqlParameter[]
                         {
                             new SqlParameter("@bookingId",labId),
                             new SqlParameter("@TestId",_testId),
                             new SqlParameter("@TestPrice",_testprice),
                             new SqlParameter("@Returnval",SqlDbType.Int)
                         };
                result = DAL.ExecuteStoredProcedureRetnInt("WS_Sp_AddBookAppoinmentTestDetails", param1);

            }
        }
        catch (Exception)
        {
            result = 0;
        }
        return result;
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsTestList
{
    DataAccessLayer DAL = new DataAccessLayer();
    public ClsTestList()
    {
    }
    public DataSet getMyTests(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetMyTests " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getMyTestsForBooking(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetMyTestsForBooking " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getMyTestsForBooking(string labId, string testprofileid)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
             {
                 new SqlParameter("@labId",labId),
                 new SqlParameter("@testprofileid",testprofileid)
             };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetMyTestsForBookingByTestprofileid", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int updatePrice(string query)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                conn.Open();

                //update test price for lab
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    //if update successfull return 1
                    return 1;
                }
                conn.Close();
            }
        }
        catch (Exception)
        {
            //return 0 if error
            return 0;
        }
    }
    public int deleteTest(string testId, string labId)
    {
        int returnval;
        try
        {
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sTestId",testId),
                    new SqlParameter("@sLabId",labId),
                    new SqlParameter("@returnval",SqlDbType.Int)
                };
            returnval = DAL.ExecuteStoredProcedureRetnInt("Sp_deleteTestByTestId", param);
        }
        catch (Exception)
        {
            returnval = 0;
        }
        return returnval;
    }
    public int createTestPackage(string labId, string[] testIds, string packageName, string price)
    {
        int testPackageId;
        int returnVal=0;
        try
        {
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sPackageName",packageName),
                    new SqlParameter("@sPrice",price),
                    new SqlParameter("@sLabId",labId),
                    new SqlParameter("@returnval",SqlDbType.Int)
                };
            testPackageId = DAL.ExecuteStoredProcedureRetnInt("Sp_CreateTestPackage", param);

            if (testPackageId >= 1)
            {
                //add tests to this package
                foreach (string testId in testIds)
                {
                    SqlParameter[] param1 = new SqlParameter[]
                        {
                             new SqlParameter("@sTestPackageId",testPackageId),
                             new SqlParameter("@sTestId",testId),
                             new SqlParameter("@returnval",SqlDbType.Int)
                        };
                    returnVal = DAL.ExecuteStoredProcedureRetnInt("Sp_AddpackageTestList", param1);
                }
            }
            else if (testPackageId == -2)
            {
                returnVal = 2;
            }
        }
        catch (Exception)
        {
            returnVal= 0;
        }
        return returnVal;
    }
    public DataSet getMyTestPackages(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetMyTestPackages " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
}
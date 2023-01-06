using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsViewReport
{
    DataAccessLayer DAL = new DataAccessLayer();
    public ClsViewReport()
    {
    }
    public DataSet getReports(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetReportByLabId " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestReport(string bookLabTestId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetTestReportBybookLabId  " + bookLabTestId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }

    public DataSet getTestReport(string bookLabTestId,string LabId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@bookLabTestId",bookLabTestId),
                new SqlParameter("@LabID",LabId)
            };

            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestReportBybookLabIdUpdated", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }

    public int approveRejectReport(string bookLabTestId, string approvalStatus, string reportApprovedOn, string reportApprovedBy, string comment)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sBookLabTestId", bookLabTestId),
                    new SqlParameter("@sApprovalStatus", approvalStatus),
                    new SqlParameter("@sReportApprovedOn", reportApprovedOn),
                    new SqlParameter("@sReportApprovedBy", reportApprovedBy),
                    new SqlParameter("@sComment", comment)
                };
            DAL.ExecuteStoredProcedure("Sp_UpdatebooklabtestUpdated", param);
            return 1;
        }
        catch (Exception)
        {
            //return 0 if error
            return 0;
        }
    }
    public void approveRejectReport(string bookLabTestId)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sBookLabTestId",bookLabTestId)
            };
            DAL.ExecuteStoredProcedure("Sp_UpdatebooklabtestByBookLabTestId", param);
        }
        catch (Exception)
        {
            //return 0 if error
            // return 0;
        }
    }
    public void UpdateReportStatus(string bookId, string ReportStatus)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@BookId",bookId),
                new SqlParameter("@ReportStatus",ReportStatus)
            };
            DAL.ExecuteStoredProcedure("Sp_UpdateReportStatus", param);
        }
        catch (Exception)
        {
            //return 0 if error
            // return 0;
        }
    }
    public void UpdateReportStatus(string bookId, string ReportStatus,string LabId)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@BookId",bookId),
                new SqlParameter("@ReportStatus",ReportStatus) ,
                new SqlParameter("@LabId",LabId)
            };
            DAL.ExecuteStoredProcedure("Sp_UpdateReportStatusUpdated", param);
        }
        catch (Exception)
        {
            //return 0 if error
            // return 0;
        }
    }
    public int updateTestReport(string bookLabTestId, string queryUpdateReport, string notes)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(queryUpdateReport, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                //update bookLabTest table to change report approval status to pending after report edit
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sBookLabTestId",bookLabTestId),
                    new SqlParameter("@sNotes",notes)
                };
                DAL.ExecuteStoredProcedure("Sp_updateTestReport", param);
                return 1;
            }
        }
        catch (Exception)
        {
            //return 0 if error
            return 0;
        }
    }
    public int updateTestReportwithPDF(string bookLabTestId, string notes, string pdffilename)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sBookLabTestId", bookLabTestId),
                new SqlParameter("@sPDFfiles", pdffilename)
            };
            DAL.ExecuteStoredProcedure("Sp_UpdateTestReportwithPDF", param);
            return 1;
        }
        catch (Exception)
        {
            //return 0 if error
            return 0;
        }
    }
}
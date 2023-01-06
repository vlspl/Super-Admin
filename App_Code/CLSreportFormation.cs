using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for CLSreportFormation
/// </summary>
public class CLSreportFormation
{
    DataAccessLayer DAL = new DataAccessLayer();
    public string sLabId { get; set; }
    public string sectonName { get; set; }
    public string status { get; set; }
    public string details { get; set; }
   
	public CLSreportFormation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int insertFormat()
    {
        int returnVal = 0;
        int labId;
        try
        {

          
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sLabId", sLabId),
                new SqlParameter("@sectionName", sectonName),
                new SqlParameter("@status", status),
                new SqlParameter("@returnval", SqlDbType.Int)
            };
            labId = DAL.ExecuteStoredProcedureRetnInt("Sp_insertBInvoiceFormat", param);



            if (labId == -3)
            {
                returnVal = 3;
            }
            else
            {
                returnVal = 0;
                
            }


        }
        catch (Exception)
        {
            //returnVal = 0;
        }
        return returnVal;
    }
    public int insertFormatreport()
    {
        int returnVal = 0;
        int labId;
        try
        {


            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sLabId", sLabId),
                new SqlParameter("@sectionName", sectonName),
                new SqlParameter("@status", status),
                new SqlParameter("@details", details),
                new SqlParameter("@returnval", SqlDbType.Int)
            };
            labId = DAL.ExecuteStoredProcedureRetnInt("Sp_insertReportFormat", param);



            if (labId == -3)
            {
                returnVal = 3;
            }
            else
            {
                returnVal = 0;

            }


        }
        catch (Exception)
        {
            //returnVal = 0;
        }
        return returnVal;
    }
}
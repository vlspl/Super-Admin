using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsLabCalendar
{
    DataAccessLayer DAL = new DataAccessLayer();
    public ClsLabCalendar()
    {
    }
    public DataSet getLabSlots(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetLabSlots " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getLabLeaves(string labId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetLabLeaves " + labId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int addSlot(string labId, string day, string from, string to)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sLabId",labId),
                new SqlParameter("@sDay",day),
                new SqlParameter("@sFrom",from),
                new SqlParameter("@sTo",to)
            };
            DAL.ExecuteStoredProcedure("Sp_AddLabSlot", param);
            return 1;
        }
        catch (Exception)
        {
            //return 0 if error
            return 0;
        }
    }
    public int addSlot(string labId, string day, string from, string to, string sAppointmentType)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sLabId",labId),
                new SqlParameter("@sDay",day),
                new SqlParameter("@sFrom",from),
                new SqlParameter("@sTo",to),
                new SqlParameter("@sAppointmentType",sAppointmentType)
            };
            DAL.ExecuteStoredProcedure("Sp_AddLabSlotwithAppointmentType", param);
            return 1;
        }
        catch (Exception)
        {
            //return 0 if error
            return 0;
        }
    }
    public int deleteLabSlot(string slotId)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                 new SqlParameter("@sSlotId",slotId)
            };
            DAL.ExecuteStoredProcedure("Sp_DeleteLabSlot", param);
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
}
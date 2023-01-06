using System;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

/// <summary>
/// Summary description for CLSGlobalSearch
/// </summary>
public class CLSGlobalSearch
{
    DataAccessLayer DAL = new DataAccessLayer();
	public CLSGlobalSearch()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet GetList(string tablename, string searchkeyword, string columnname)
    {
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                conn.Open();
                string query = "select * from " + tablename + " where " + columnname + " like '%" + searchkeyword + "%' or sTestCode like '%" + searchkeyword + "%'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public DataSet PatientList(string searchkeyword, string LabIdSession)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@LabIdSession",LabIdSession),
                new SqlParameter("@searchkeyword",searchkeyword)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetPatientList", param);
            return ds;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public DataSet DoctorList(string searchkeyword, string LabIdSession)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@LabIdSession",LabIdSession),
                new SqlParameter("@searchkeyword",searchkeyword)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetDoctorList", param);
            return ds;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public DataSet GetList(string tablename, string searchkeyword, string columnname , string role, string LabIdSession)
    {
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                conn.Open();
                string query = "select * from " + tablename + " where sRole='" + role + "' and " + columnname + " like '%" + searchkeyword + "%' and ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }

        catch (Exception)
        {
            return null;
        }
    }
}
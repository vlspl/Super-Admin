using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

/// <summary>
/// Summary description for CLSGlobalSearch
/// </summary>
public class CLSSuperAdminGlobalSearch
{
    public CLSSuperAdminGlobalSearch()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet GetLabList(string tablename, string searchkeyword, string columnname)
    {
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                conn.Open();
                string query = "select * from "+tablename +" where " + columnname + " like '%"+ searchkeyword +"%'";
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
    public DataSet GetLabList(string tablename, string searchkeyword, string columnname , string role)
    {
        DataSet ds = new DataSet();
        try
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                conn.Open();
                string query = "select * from " + tablename + " where sRole='" + role + "' and " + columnname + " like '%" + searchkeyword + "%'";
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using DataAccessHandler;
using System.Configuration;

/// <summary>
/// Summary description for CLSLabsManagement
/// </summary>
public class CLSLabApprovalGetDetails
{
    DataAccessLayer DAL = new DataAccessLayer();
   SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    public CLSLabApprovalGetDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable getApproListDtl(int labId)
    {
      //  DataTable ds = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_GetLabApprovalDtl", con);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@Temp_LabId", labId);
            DataTable dt = new DataTable();

            sda.Fill(dt);

            
            
           // //SqlConnection con=new SqlConnection();
           //// DAL.OpenConnection();
           //// labId = Convert.ToInt32(Request.QueryString["id"].ToString());
           // con.Open();
           // SqlCommand cmd = new SqlCommand("Sp_GetLabApprovalDtl",con);
           //// ds = DAL.GetDataSet("Sp_GetLabApprovalDtl");
           //           cmd.CommandType = CommandType.StoredProcedure;
           //           cmd.Parameters.AddWithValue("@Temp_LabId", labId);
           //           con.Close();
            return dt;
        }
        catch(Exception)
        {
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for CLSUserProfileFDMList
/// </summary>
public class CLSUserProfileFDMList
{
    DataAccessLayer DAL = new DataAccessLayer();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
	public CLSUserProfileFDMList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable getUPFDMList(int listId)
    {
        //  DataTable ds = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_GetuserProfile_FDM_list", con);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@listId", listId);
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
        catch (Exception)
        {
            return null;
        }
    }
}
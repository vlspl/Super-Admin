using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using System.Configuration;

public partial class SuperAdmin_ViewHealthCampDetails : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetHealthCampDetail");
                grdviewOrgnization.DataBind();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void grdviewOrgnization_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         SqlDataAdapter da;
        SqlConnection con;
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        cmd.Connection = con;
        Label lbldeleteID = (Label)grdviewOrgnization.Rows[e.RowIndex].FindControl("lbltestId");
        cmd.CommandText = "Delete from healthCampMaster where healthcampID='" + lbldeleteID.Text + "' ";
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetHealthCampDetail");
        grdviewOrgnization.DataBind();
    }
    protected void grdviewOrgnization_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdviewOrgnization.PageIndex = e.NewPageIndex;
        grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetHealthCampDetail");
        grdviewOrgnization.DataBind();
    }
    protected void grdviewOrgnization_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = (grdviewOrgnization.DataKeys[grdviewOrgnization.SelectedRow.DataItemIndex].Value).ToString();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@Id",id)
            };

          //  DAL.ExecuteStoredProcedure("Sp_ActiveOrgnization ", param);
            grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetHealthCampDetail");
            grdviewOrgnization.DataBind();
        }
        catch (Exception ex)
        {
            litErrorMessage.Text = "Somthing went Wrong. Please Try Again.!!!";
        }

        grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetHealthCampDetail");
        grdviewOrgnization.DataBind();

    }
    protected void grdviewOrgnization_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
                       DataRowView dr = (DataRowView)e.Row.DataItem;
            string mobNo = (dr["sMobile"].ToString());
            string password = (dr["Password"].ToString());
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            e.Row.Cells[7].Text = CryptoHelper.Decrypt(mobNo);
            e.Row.Cells[8].Text = CryptoHelper.Decrypt(password);

        }
    }
}
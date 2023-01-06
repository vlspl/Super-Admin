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
    protected void grdviewOrgnization_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int deleteID = Convert.ToInt32(grdviewOrgnization.DataKeys[e.RowIndex].Value.ToString());
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@Id",deleteID)
            };

            DAL.ExecuteStoredProcedure("Sp_DeleteOrgnization ", param);
            grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetHealthCampDetail");
            grdviewOrgnization.DataBind();
        }
        catch (Exception ex)
        {
            litErrorMessage.Text = "Somthing went Wrong. Please Try Again.!!!";
        }
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
            e.Row.Cells[6].Text = CryptoHelper.Decrypt(mobNo);
            e.Row.Cells[7].Text = CryptoHelper.Decrypt(password);

        }
    }
}
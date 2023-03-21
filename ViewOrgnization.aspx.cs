using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_ViewOrgnization : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (true)//Request.Cookies["AdminId"].Value != null
        {
            if (!Page.IsPostBack)
            {
                
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
            grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetOrgnizationDetail_type");
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
        grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetOrgnizationDetail_type");
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

            DAL.ExecuteStoredProcedure("Sp_ActiveOrgnization ", param);
            grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetOrgnizationDetail_type");
            grdviewOrgnization.DataBind();
        }
        catch (Exception ex)
        {
            litErrorMessage.Text = "Somthing went Wrong. Please Try Again.!!!";
        }

        grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetOrgnizationDetail_type");
        grdviewOrgnization.DataBind();

    }

    protected void grdviewOrgnization_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            string mobNo = (dr["Mobile"].ToString());
            string email = (dr["EmailId"].ToString());
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            e.Row.Cells[3].Text = CryptoHelper.Decrypt(mobNo);
            e.Row.Cells[4].Text = CryptoHelper.Decrypt(email);

        }
    }

    protected void drpstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type;
        if (drpstatus.Text == "Government")
            type = "1";
        else
            type = "0";
        SqlParameter[] param = new SqlParameter[]
             {
               new SqlParameter("@type",type)
             };

        DataSet ds= DAL.ExecuteStoredProcedureDataSet("Sp_GetOrgnizationDetail_type ", param);
        grdviewOrgnization.DataSource = ds;
        grdviewOrgnization.DataBind();
    }
}
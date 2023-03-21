using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_ViewChannelPartnerList : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                grdviewOrgnization.DataSource = GetPartnerDetails();
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

            DAL.ExecuteStoredProcedure("Sp_DeleteChannelPartner ", param);
            grdviewOrgnization.DataSource = GetPartnerDetails();
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
        grdviewOrgnization.DataSource = GetPartnerDetails();
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
            DAL.ExecuteStoredProcedure("Sp_ActiveChannelPartner ", param);
            grdviewOrgnization.DataSource = GetPartnerDetails();
            grdviewOrgnization.DataBind();
        }
        catch (Exception ex)
        {
            litErrorMessage.Text = "Somthing went Wrong. Please Try Again.!!!";
        }
        grdviewOrgnization.DataSource = GetPartnerDetails();
        grdviewOrgnization.DataBind();

    }

    public DataTable GetPartnerDetails()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetChannelPartnerDetails");
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    string Mobile = (dt.Rows[i]["MobileNumber"].ToString() != "") ? CryptoHelper.Decrypt(dt.Rows[i]["MobileNumber"].ToString()) : "";
                    string EmailId = (dt.Rows[i]["EmailId"].ToString() != "") ? CryptoHelper.Decrypt(dt.Rows[i]["EmailId"].ToString()) : "";
                    row["Mobile"] = Mobile;
                    row["EmailId"] = EmailId;
                }
            }
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
}
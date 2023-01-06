using System;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_MonthlyUserDetailsRecords : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    DataTable dt = new DataTable();
    int id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!IsPostBack)
            {
                if (ddlMonth.SelectedValue == "0")
                {
                    grdviewUserList.DataSource = DAL.GetDataTable("Sp_GetMonthlyUserSignUpRecordByCuurentMonth ");
                    grdviewUserList.DataBind();
                } 
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Id"] = Convert.ToInt32(ddlMonth.SelectedValue);
        try
        {
            grdviewUserList.DataSource = GetBranchDetails(Session["Id"].ToString());
            grdviewUserList.DataBind();
        }
        catch (Exception)
        {
            dt = null;
        }
    }
    public DataTable GetBranchDetails(string Id)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetMonthlyUserSignUpRecord " + Id);
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
}
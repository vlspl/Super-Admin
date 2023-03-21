using System;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_ViewOrgBranch : System.Web.UI.Page
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
                ddlName.DataSource = DAL.GetDataTable("Sp_GetOrgnizationDetailForDDL");
                ddlName.DataBind();
                ListItem lit = new ListItem();
                lit.Text = "Select";
                lit.Value = "0";
                ddlName.Items.Insert(0, lit);
                spcount.Visible = false;
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }
    protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Id"] = Convert.ToInt32(ddlName.SelectedValue);
        try
        {
            grdviewOrgBranch.DataSource = GetBranchDetails(Session["Id"].ToString());
            grdviewOrgBranch.DataBind();
            spcount.Visible = true;
            lbltotalcount.Text = Convert.ToString((grdviewOrgBranch.DataSource as DataTable).Rows.Count);
        }
        catch (Exception)
        {
            dt = null;
        }
    }
    protected void grdviewOrgBranch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdviewOrgBranch.PageIndex = e.NewPageIndex;
        grdviewOrgBranch.DataSource = GetBranchDetails(Session["Id"].ToString());
        grdviewOrgBranch.DataBind();
    }
    protected void grdviewOrgBranch_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int deleteID = Convert.ToInt32(grdviewOrgBranch.DataKeys[e.RowIndex].Value.ToString());
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@Id",deleteID)
            };

            DAL.ExecuteStoredProcedure("Sp_DeleteBranch ", param);
            grdviewOrgBranch.DataSource = GetBranchDetails(Session["Id"].ToString());
            grdviewOrgBranch.DataBind();
        }
        catch (Exception ex)
        {
            litErrorMessage.Text = "Somthing went Wrong. Please Try Again.!!!";
        }
    }

    public DataTable GetBranchDetails(string Id)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetOrgBranchDetail " + Id);
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("EmailId", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    string Mobile = (dt.Rows[i]["Contact"].ToString() != "") ? CryptoHelper.Decrypt(dt.Rows[i]["Contact"].ToString()) : "";
                    string EmailId = (dt.Rows[i]["Email"].ToString() != "") ? CryptoHelper.Decrypt(dt.Rows[i]["Email"].ToString()) : "";
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
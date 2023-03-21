using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using System.Configuration;

public partial class SuperAdmin_AssessmentGroupList : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                gridbind();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }
    
    void  gridbind()
    {
        gridassessmentGroup.DataSource = GetPartnerDetails();
        gridassessmentGroup.DataBind();
    }
    public DataTable GetPartnerDetails()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetassessmentGroupList");
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }

    protected void gridassessmentGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridassessmentGroup.PageIndex = e.NewPageIndex;
        gridbind();
    }

    protected void gridassessmentGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlDataAdapter da;
        SqlConnection con;
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        cmd.Connection = con;
        Label lbldeleteID = (Label)gridassessmentGroup.Rows[e.RowIndex].FindControl("lbltestId");
        cmd.CommandText = "Delete from tbl_selfAssessmentGroupMaster where assessmentGroupID='" + lbldeleteID.Text + "' ";
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Assessment group deleted successfully');location.href='AssessmentGroupList.aspx';", true);

    }
}
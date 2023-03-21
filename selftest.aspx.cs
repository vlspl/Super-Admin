using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_selftest : System.Web.UI.Page
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
    void gridbind()
    {
        gridselfTest.DataSource = GetPartnerDetails();
        gridselfTest.DataBind();
    }
   
    public DataTable GetPartnerDetails()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetassessmenttestList");
            
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }

    protected void gridselfTest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridselfTest.PageIndex = e.NewPageIndex;
        gridbind();
    }

    protected void gridselfTest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DBClass db = new DBClass();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string customerId = gridselfTest.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
            gvOrders.DataSource = db.Displaygrid(string.Format(@"select  optionDetails,optionStatus from tbl_selfAssessmentOptionDetails
 where assessmentMasterID ='{0}'", customerId));
            gvOrders.DataBind();
        }
    }

    protected void gridselfTest_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        gridselfTest.PageIndex = e.NewPageIndex;
        gridbind();
    }
}
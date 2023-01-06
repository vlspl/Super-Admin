using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class SuperAdmin_ViewLog : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
          if (!Page.IsPostBack)
            {
                showLog();
            }
      
    }

    void showLog()
    {
        SqlParameter[] paramv_iewLog = new SqlParameter[]
         {
                           
             
          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_getViewLogList", paramv_iewLog);
        gridViewLog.DataSource = ds;
        gridViewLog.DataBind();
       
    }

    protected void gridViewLog_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlDataAdapter da;
        SqlConnection con;
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        cmd.Connection = con;
        Label lbldeleteID = (Label)gridViewLog.Rows[e.RowIndex].FindControl("lbllogId");
        cmd.CommandText = "Delete from Tbl_ExceptionLoggingToDataBase where Logid='" + lbldeleteID.Text + "' ";
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        showLog(); 
    }
    protected void gridViewLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridViewLog.PageIndex = e.NewPageIndex;
        showLog();
    }
}
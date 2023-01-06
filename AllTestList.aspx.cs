using System;
using DataAccessHandler;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class AllTestList : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    static int LABID;
    SqlConnection con = new SqlConnection(@"Data Source=202.154.161.105;Initial Catalog=MedicalDbBackup;Persist Security Info=True;User ID=Howzu;Password=@Vls1234#?");  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["loggedIn"] != null)
        {
            if (!IsPostBack)
            {
                loadTestList();
                AllTestList.LABID = Convert.ToInt32(Request.Cookies["labId"].Value.ToString());
            }
        }
        else
        {
            Response.Redirect("LabLogin.aspx");
        }
    }
    protected void loadTestList()
    {
        try
        {
            DataSet ds = DAL.GetDataSet("Sp_GetAllTestlist " + Request.Cookies["labId"].Value.ToString());
            Session["allTestList"] = ds;
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridAlltext.DataSource = ds;
                    gridAlltext.DataBind();
                    lblusercount.Text = (gridAlltext.Rows.Count).ToString();
                    Session["allTestList"]=ds;
                }
                else
                {
                    //gridAlltext.EmptyDataText = "No records found";
                }
            }
            gridAlltext.UseAccessibleHeader = true;
            gridAlltext.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
    [System.Web.Services.WebMethod]
    public static string TestEdit(int testId)
    {
       
        DataAccessLayer DAL = new DataAccessLayer();
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@labId",AllTestList.LABID),
            new SqlParameter("@TestId",testId),
            new SqlParameter("@ReturnVal",SqlDbType.Int)
        };
        int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddMyTest", param);
        return result.ToString();
       
    }
 

    protected void btnaddAll_Click(object sender, EventArgs e)
    {
        DataSet getHwDetails = new DataSet();
        getHwDetails = (DataSet)Session["allTestList"];
        string rowId; int LabId;
         //CheckBox chkRow;
         foreach (GridViewRow row in gridAlltext.Rows)
         {
             if (row.RowType == DataControlRowType.DataRow)
             {
                 CheckBox chkRow = (row.Cells[0].FindControl("chkselect") as CheckBox);
                 if (chkRow.Checked)
                 {
                     rowId = (row.Cells[2].FindControl("Label1") as Label).Text;
                     LabId = Convert.ToInt32(Request.Cookies["labId"].Value.ToString());
                     DataAccessLayer DAL = new DataAccessLayer();
                     SqlParameter[] param = new SqlParameter[]
                                 {
                                     new SqlParameter("@labId",LabId),
                                     new SqlParameter("@TestId",rowId),
                                     new SqlParameter("@ReturnVal",SqlDbType.Int)
                                 };
                     int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddMyTest", param);
                     // return result.ToString();
                 }
             }
         }
        //for (int i = 0; i <= getHwDetails.Tables[0].Rows.Count; i++)
        //{
          
        //    chkRow = (gridAlltext.Rows[i].Cells[0].FindControl("chkselect") as CheckBox);
        //    if (chkRow.Checked)
        //    {
        //        rowId = getHwDetails.Tables[0].Rows[i].Field<Int64>("sTestId");
        //        LabId = Convert.ToInt32(Request.Cookies["labId"].Value.ToString());
        //        DataAccessLayer DAL = new DataAccessLayer();
        //        SqlParameter[] param = new SqlParameter[]
        //            {
        //                new SqlParameter("@labId",LabId),
        //                new SqlParameter("@TestId",rowId),
        //                new SqlParameter("@ReturnVal",SqlDbType.Int)
        //            };
        //        int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddMyTest", param);
        //        // return result.ToString();

        //    }
          
        //}
        Response.Redirect(@"AllTestList.aspx");
    }
    //protected void txtsearch_TextChanged(object sender, EventArgs e)
    //{
    //    Int64 labid = Convert.ToInt32(Request.Cookies["labId"].Value.ToString());
    //    string searchdata = txtsearch.Text;

    //    gridAlltext.DataSource = GetBranchDetails(labid, searchdata);
    //    gridAlltext.DataBind();
    //    lblusercount.Text = (gridAlltext.Rows.Count).ToString();
    //}
    //public DataTable GetBranchDetails(Int64 labid, string searchdata)
    //{
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        dt = DAL.GetDataTable("Sp_Alltestlistsearch " + "'" + labid + "','" + searchdata + "'");
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        dt = null;
    //        return dt;
    //    }
    //}
}
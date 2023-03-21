using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class SuperAdmin_TestDelete : System.Web.UI.Page
{
    DBClass db = new DBClass();
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (true)//Request.Cookies["AdminId"].Value != null
        {
            if (!IsPostBack)
            {

                db.bindDrp("select distinct sLabId, sLabName from labMaster where IsActive=1 and sLabStatus='Active' order by sLabName asc", drplablist, "sLabName", "sLabName");
                drplablist.Items.Insert(0, new ListItem("All", "All"));
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    void gridBind()
    {
        string labId = db.getData("select sLabId from labMaster where sLabName='"+drplablist.Text+"'").ToString();
        gridtestlist.DataSource = bindgrid(labId);//"Sp_getlabtestList"+drplablist.Text);
        gridtestlist.DataBind();
    }
    public DataTable bindgrid(string labId)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_getlabtestList " + "'"+labId+"'");
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
  
    //protected void drpfdmType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string type = drpfdmType.Text;
    //    gridFDMList.DataSource = GetBranchDetails(type);
    //    gridFDMList.DataBind();
        
    //}
    //public DataTable GetBranchDetails(string type)
    //{
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        dt = DAL.GetDataTable("Sp_FDM_list_typewise " + "'"+type+"'");
    //        return dt;
    //    }
    //    catch (Exception)
    //    {
    //        dt = null;
    //        return dt;
    //    }
    //}
    protected void drplablist_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridBind();
    }
    protected void gridtestlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridtestlist.PageIndex = e.NewPageIndex;
        gridBind();
    }
    protected void gridtestlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string labId = db.getData("select sLabId from labMaster where sLabName='" + drplablist.Text + "'").ToString();
        SqlDataAdapter da;  
            SqlConnection con;  
            DataSet ds = new DataSet();  
        SqlCommand cmd = new SqlCommand();
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        cmd.Connection = con;
        Label lbldeleteID = (Label)gridtestlist.Rows[e.RowIndex].FindControl("lbltestId");
        cmd.CommandText = "Delete from testLab where sTestId='" + lbldeleteID.Text + "' and sLabId='" + labId + "'";
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        gridBind(); 
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
   
}
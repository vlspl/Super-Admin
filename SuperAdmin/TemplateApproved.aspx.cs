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
    string cs = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection con;
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
        GridView1.DataSource = bindgrid(labId);//"Sp_getlabtestList"+drplablist.Text);
        GridView1.DataBind();
    }
    public DataTable bindgrid(string labId)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_getlabTemplateList " + "'" + labId + "'");
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
  
   
    protected void drplablist_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridBind();
    }

    protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        GridView1.EditIndex = e.NewEditIndex;
        gridBind();
    }
    protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        //Finding the controls from Gridview for the row which is going to update  
        Label id = GridView1.Rows[e.RowIndex].FindControl("lbltestId") as Label;
        //TextBox name = GridView1.Rows[e.RowIndex].FindControl("txt_Name") as TextBox;
        //DropDownList city = GridView1.Rows[e.RowIndex].DataItem as DropDownList;
        string city = (GridView1.Rows[e.RowIndex].FindControl("drp_status") as DropDownList).SelectedItem.Value;
        con = new SqlConnection(cs);
        con.Open();
        //updating the record  
        SqlCommand cmd = new SqlCommand("Update tbl_WhatsappMsgMaster set status='" + city + "' where whatsappMasterId=" + Convert.ToInt32(id.Text), con);
        cmd.ExecuteNonQuery();
        con.Close();
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        GridView1.EditIndex = -1;
        //Call ShowData method for displaying updated data  
        gridBind();
    }
    protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        GridView1.EditIndex = -1;
        gridBind();
    }  
    
  
}
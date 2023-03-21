using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;

public partial class SuperAdmin_DemoRequestNew : System.Web.UI.Page
{
    //public string requestId;
    DemoRequestDAL objdal = new DemoRequestDAL();
    private DataTable dtFillData;
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["requestId"] != null)
        {
            dtFillData = objdal.fillData(Request.QueryString["requestId"]);
            foreach (DataRow row in dtFillData.Rows)
            {
                // basic Info
                
                txtFullName.Text = row["fullName"].ToString();
                txtEmail.Text = row["emailAddress"].ToString();
                txtDemoCat.Text = row["BookDemoCatgory"].ToString();
                txtQuery.Text = row["query"].ToString();
                txtLocation.Text = row["location"].ToString();
                txtPhone.Text = row["phone"].ToString();
                txtDemoDate.Text = row["createdDate"].ToString();
            }
           // dtFillData = objdal.fillData(Request.QueryString["requestId"]);


            BindGrid();
            
        }
        
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        BindGrid(); 
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("Sp_AddDemoRequestFollowUp",con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@requestId", Request.QueryString["requestId"]);
        cmd.Parameters.Add("@Followup_By", txtBy.Text);
        cmd.Parameters.Add("@Followup_Date", System.DateTime.Now.ToString("dd/MM/yyyy hh: mm:ss"));
        cmd.Parameters.Add("@created_By","");
        cmd.Parameters.Add("@created_Date", System.DateTime.Now.ToString("dd/MM/yyyy hh: mm:ss"));
        cmd.Parameters.Add("@Remark",txtRemark.Text);
        cmd.Parameters.Add("@status", drpStatus.SelectedValue);
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
           // ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"Save Successfully!\");", true);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Data has been added Successfully');location.href='DemoList.aspx';", true);
            // lboutput.Text = "Record inserted successfully";
        }
        catch (Exception ex)
        {
            throw ex;
        }

        //Response.Redirect("DemoList.aspx");

    }
    private void BindGrid()
    {
        string requestId = Request.QueryString["requestId"].ToString();
        SqlParameter[] paramEmg_getMaterial = new SqlParameter[]
         {
                           
              new SqlParameter("@requestId", requestId)
          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_getFollowupDetails", paramEmg_getMaterial);

        gridFollowUp.DataSource = ds;
        gridFollowUp.DataBind();


    }

    protected void txtBy_TextChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void gridFollowUpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridFollowUp.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
   
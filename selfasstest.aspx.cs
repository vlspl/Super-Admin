using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using BitsBizLogic;
using DataAccessHandler;
using CrossPlatformAESEncryption.Helper;
using Validation;
using System.Net.Mail;
using System.Web.Configuration;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class SuperAdmin_selfasstest : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Cookies["AdminId"].Value != null)
        {
            if(!IsPostBack)
            {
                db.bindDrp("select distinct assessmentGroupName,assessmentGroupID from tbl_selfAssessmentGroupMaster where groupStatus='Active' order by assessmentGroupName asc", drpassessmentgroup, "assessmentGroupName", "assessmentGroupID");

            }
            
           
        }
        else
        {
            Response.Redirect("AdminLogin.aspx", false);
        }

    }



    protected void btnadd_Click(object sender, EventArgs e)
    {
        if(txtoptions.Text!="" && txtosquence.Text!="")
        {
            db.insert("insert into tbl_tempOption(optionDetails,optionSequences,optionStatus) values('"+txtoptions.Text+"','"+txtosquence.Text+"','"+drpoptionstatus.Text+"')");
            BindGrid();
            txtoptions.Text = "";
            txtosquence.Text = "";
        }
    }
    protected void BindGrid()
    {

        GridView1.DataSource = GetPartnerDetails();
        GridView1.DataBind();
        Session["testOptios"] = GetPartnerDetails();
    }
    public DataTable GetPartnerDetails()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_gettemppotions");
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string lastq;
        if (drplastquestion.Text == "Yes")
            lastq = "1";
        else
            lastq = "0";
        db.insert("insert into tbl_selfAssessmentOptionMaster(assessmentGroup,assessmentQuestion,assessmentDescription,assessmentStatus,assessmentFor,assessmentSquence,assessmentGroupID,islastQuestion) " +
            "values('" + drpassessmentgroup.SelectedItem.Text + "','" + txtquestion.Text + "','" + txtdescription.Text + "','" + drpassessmentstatus.Text + "','" + drpfor.Text + "','" + txtsquence.Text + "','" + txtgroupId.Text + "','" + lastq + "')");
        string maxClientId = db.getData("select max(assessmentMasterID) from tbl_selfAssessmentOptionMaster").ToString();


       
        DataTable dt_FinalUser = new DataTable();

        dt_FinalUser = (DataTable)Session["testOptios"];
        string options,  status;
        int sequence;
        if (dt_FinalUser != null)
        {
            for (int i = 0; i <= dt_FinalUser.Rows.Count - 1; i++)
            {
                options = dt_FinalUser.Rows[i].Field<string>("optionDetails");
                sequence = dt_FinalUser.Rows[i].Field<Int32>("optionSequences");
                status = dt_FinalUser.Rows[i].Field<string>("optionStatus");
               
                db.insert("insert into tbl_selfAssessmentOptionDetails(assessmentMasterID,optionDetails,optionSequences,optionStatus) values('" + maxClientId + "','" + options + "','" + sequence + "','" + status + "')");
            }
        }
        db.insert("delete from tbl_tempOption");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Assessment test added successfully');location.href='selftest.aspx';", true);


    }



    protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        SqlDataAdapter da;
        SqlConnection con;
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        cmd.Connection = con;
        Label lbldeleteID = (Label)GridView1.Rows[e.RowIndex].FindControl("lbltestId");
        cmd.CommandText = "Delete from tbl_tempOption where tempoptionId='" + lbldeleteID.Text + "' ";
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        BindGrid();
    }

    protected void drpassessmentgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtgroupId.Text = drpassessmentgroup.SelectedItem.Value;
    }
}
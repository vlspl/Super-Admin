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

public partial class SuperAdmin_selfGroup : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Cookies["AdminId"].Value != null)
        {
           if (!Page.IsPostBack)
            {
                if (Request.QueryString["assessmentGroupID"] != null)
                {
                    getGroupData();

                }
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx", false);
        }

    }

    public void getGroupData()
    {


        string groupId = Request.QueryString["assessmentGroupID"].ToString();
       DataTable ds = getgroupDtl(groupId);

        foreach (DataRow row in ds.Rows)
        {
            BtnSave.Visible = false;
            btnUpdate.Visible = true;
            txtassessmentGroup.Text = row["assessmentGroupName"].ToString();
            txtdescription.Text = row["assessmentGroupDescription"].ToString();
            drpstatus.Text = row["groupStatus"].ToString();
            
        }
    }
    public DataTable getgroupDtl(string groupId)
    {
        //  DataTable ds = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_GetassessmentGroupListbyId", con);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@groupId", groupId);
            DataTable dt = new DataTable();

            sda.Fill(dt);


            return dt;
        }
        catch (Exception)
        {
            return null;
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        db.insert("insert into tbl_selfAssessmentGroupMaster(assessmentGroupName,assessmentGroupDescription,groupStatus) values('" + txtassessmentGroup.Text + "','" + txtdescription.Text + "','" + drpstatus.Text + "')");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Assessment group added successfully');location.href='AssessmentGroupList.aspx';", true);

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        db.insert("update tbl_selfAssessmentGroupMaster set assessmentGroupName='" + txtassessmentGroup.Text + "',assessmentGroupDescription='" + txtdescription.Text + "',groupStatus='" + drpstatus.Text + "' where assessmentGroupID='"+ Request.QueryString["assessmentGroupID"].ToString() + "'");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Assessment group updated successfully');location.href='AssessmentGroupList.aspx';", true);

    }
}
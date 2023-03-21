using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using System.Configuration;

public partial class SuperAdmin_viewselftest : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["assessmentMasterID"] != null)
                {
                    getMasterData();
                    getDetailsData();
                }
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }
   

    public void getMasterData()
    {


        string groupId = Request.QueryString["assessmentMasterID"].ToString();
        DataTable ds = getgroupDtl(groupId);
        FormView1.DataSource = ds;
        FormView1.DataBind();

    }
    public void getDetailsData()
    {


        string groupId = Request.QueryString["assessmentMasterID"].ToString();
        DataTable ds = getgroupDtloption(groupId);
        gridoptions.DataSource = ds;
        gridoptions.DataBind();

    }
    public DataTable getgroupDtl(string groupId)
    {
        //  DataTable ds = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_GetassessmentTestListbyId", con);

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
    public DataTable getgroupDtloption(string groupId)
    {
        //  DataTable ds = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_GetassessmentOptionListbyId", con);

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


   
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

public partial class SuperAdmin_WhatsAppTempleteCreation : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //db.bindDrp(@"SELECT Distinct [status] as status FROM [tbl_WhatsappMsgMaster]", drpApprovalStatus, "status", "status");
            //drpApprovalStatus.Items.Insert(0, new ListItem("--Select--", "--Select--"));


            BindGrid();
        }

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {

        SqlParameter[] param = new SqlParameter[]
        {
          new SqlParameter("@TempleteName", txtTempName.Text),
           new SqlParameter("@Messagetext", txtMsgText.Text),
            new SqlParameter("@MsgType", drpTrans.SelectedValue),
              new SqlParameter("@requestBy", txtRequestBy.Text),
              new SqlParameter("@Parameter", txtParameter.Text),
               new SqlParameter("@ParameterList", txtParaneterList.Text),

               new SqlParameter("@status", drpApprovalStatus.SelectedValue),
                new SqlParameter("@approveBy", txtApproveBy.Text),
                new SqlParameter("@approveDate",Convert.ToDateTime(txtApproveDate.Text,
                System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat)),
                new SqlParameter("@ReturnVal", SqlDbType.Int)
        };

        int resturnVal = DAL.ExecuteStoredProcedureRetnInt("Sp_AddWhatsappMsgMaster", param);
        if (resturnVal == 0)
            lblError.Text = " Template Name Already Exists.";
        else
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"Save Successfully!\");", true);

        }
        BindGrid();
    }


    private void BindGrid()
    {

        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand();

        //cmd.CommandType = CommandType.StoredProcedure;
        cmd = new SqlCommand("[Sp_getWhatAppMsgDetails]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add("@whatsappMasterId",0);
       
        SqlDataAdapter sda = new SqlDataAdapter();

        cmd.Connection = con;
        sda.SelectCommand = cmd;

        DataTable dt = new DataTable();
        sda.Fill(dt);

        grdviewWhatAppMaster.DataSource = dt;
        grdviewWhatAppMaster.DataBind();


    }
    protected void grdviewWhatAppMaster_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
        grdviewWhatAppMaster.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}


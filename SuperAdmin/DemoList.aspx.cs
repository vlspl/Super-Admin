using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SuperAdmin_DemoList : System.Web.UI.Page
{
    public object id;
    private object objDemoPageRequest;
    DBClass db = new DBClass();
    CLSLabsManagement objLabsManagement = new CLSLabsManagement();

    public object From { get; private set; }
    public object To { get; private set; }

    //private string constr;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            db.bindDrp(@"SELECT Distinct [status] as status FROM [DemoRequestFollowUp]", drpStatus, "status", "status");
            drpStatus.Items.Insert(0, new ListItem("--Select--", "--Select--"));
            db.bindDrp(@"SELECT DISTINCT [bookDemoCatgory] FROM [demoPage]", drpDemoCategory, "bookDemoCatgory", "bookDemoCatgory");
            drpDemoCategory.Items.Insert(0, new ListItem("--Select--", "--Select--"));
            BindGrid();

          //  NameFilter();

        }
    }

  
    private void BindGrid()
    {

        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand();

        //cmd.CommandType = CommandType.StoredProcedure;
        cmd = new SqlCommand("GetDemoRequestByID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add("@requestId", 0);
        SqlDataAdapter sda = new SqlDataAdapter();

        cmd.Connection = con;
        sda.SelectCommand = cmd;

        DataTable dt = new DataTable();
        sda.Fill(dt);

        grdviewDemoList.DataSource = dt;
        grdviewDemoList.DataBind();


    }





    //private void NameFilter()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("sp_DemoPageMultipleFilter", con))
    //        {
    //            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);
    //            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null);

    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@fullName", txtFullName.Text);
    //            cmd.Parameters.AddWithValue("@location", txtLocation.Text);
    //            cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
    //            cmd.Parameters.AddWithValue("@BookDemoCatgory", drpDemoCategory.SelectedItem.Text);
    //            cmd.Parameters.AddWithValue("@Status", drpStatus.SelectedItem.Text);
    //            //cmd.Parameters.AddWithValue("@remark", drpStatus.SelectedItem.Text);
    //            cmd.Parameters.AddWithValue("@Fromdate", fromdate);//
    //            cmd.Parameters.AddWithValue("@Todate", todate);//


    //            // Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy"));



    //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
    //            {
    //                cmd.Connection = con;
    //                sda.SelectCommand = cmd;

    //                using (DataTable dt = new DataTable())
    //                {
    //                    sda.Fill(dt);
    //                    grdviewDemoList.DataSource = dt;
    //                    grdviewDemoList.DataBind();
    //                }
    //            }
    //        }

    //    }

    //}
    private void NameFilter()
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_DemoPageMultipleFilter", con))
            {
                //DateTime todate = DateTime.ParseExact(txtToDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime todate, fromdate;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fullName", txtFullName.Text);
                cmd.Parameters.AddWithValue("@location", txtLocation.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@BookDemoCatgory", drpDemoCategory.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Status", drpStatus.SelectedItem.Text);
                //cmd.Parameters.AddWithValue("@remark", drpStatus.SelectedItem.Text);
                if (txtFromDate.Text != "" && txtToDate.Text != "")
                {
                    todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", null);
                    fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null);
                    cmd.Parameters.AddWithValue("@Fromdate", fromdate);
                    cmd.Parameters.AddWithValue("@Todate", todate);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Fromdate", "");
                    cmd.Parameters.AddWithValue("@Todate", "");
                }





                // Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy"));



                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        grdviewDemoList.DataSource = dt;
                        grdviewDemoList.DataBind();
                    }
                }
            }

        }

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
      NameFilter();
        txtFullName.Text = string.Empty;
        txtLocation.Text = string.Empty;
        txtPhone.Text = string.Empty;


    }



    protected void drpDemoCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
      
       

    }

    protected void grdviewDemoList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdviewDemoList.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}




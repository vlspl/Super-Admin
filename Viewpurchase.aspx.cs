using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validation;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Viewpurchase : System.Web.UI.Page
{
    InputValidation Ival = new InputValidation();
    DataAccessLayer DAL = new DataAccessLayer();
    DBClass db = new DBClass();
    string MasterId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
           {
               string labId = Request.Cookies["labId"].Value.ToString();
                MasterId=Request.QueryString["id"].ToString();
                viewMaster(labId, MasterId);
                viewDetails();
           }
        }
    }
    void viewDetails()
    {
        gridBind();
    }
    void viewMaster(string labId, string id)
    {
        SqlParameter[] param_count = new SqlParameter[]
                {
                    new SqlParameter("@LabId",labId),
                     new SqlParameter("@purchaseMasterId",id)
                    
                 };
        DataTable dt_counter = DAL.ExecuteStoredProcedureDataTable("Sp_getgetPurchaseMasterDetails", param_count);
        foreach (DataRow row_count in dt_counter.Rows)
        {
            txtvendorName.Text = (row_count["vendorName"].ToString());
            txtdate.Text = (row_count["Date"].ToString());
            txtinvoiceNo.Text = (row_count["invoiceNo"].ToString());
            txtgrnNo.Text = (row_count["grnNo"].ToString());
            drpbillType.Text = (row_count["billType"].ToString());
            drptype.Text = (row_count["type"].ToString());
            lblgstinNo.Text = (row_count["gstinNo"].ToString());
            txtdescription.Text = (row_count["description"].ToString());
        }
    }
  
    
  
    void gridBind()
    {
        string labId = Request.Cookies["labId"].Value.ToString();
        temp_material.DataSource = bindgrid(labId,MasterId);//"Sp_getlabtestList"+drplablist.Text);
        temp_material.DataBind();
        footerSum(labId,MasterId);
    }
    void footerSum(string labId, string id)
    {
        
        SqlParameter[] param_count = new SqlParameter[]
                {
                    new SqlParameter("@LabId",labId),
                     new SqlParameter("@purchaseMasterId",id)
                    
                 };
        DataTable dt_counter = DAL.ExecuteStoredProcedureDataTable("Sp_getpurchaseDetails_sum", param_count);
        foreach (DataRow row_count in dt_counter.Rows)
        {
             lblnetamt.Text=(row_count["amt"].ToString());
             lblcgst.Text =(row_count["cgst"].ToString());
           lblsgst.Text =(row_count["sgst"].ToString());
            lbligst.Text =(row_count["igst"].ToString());
            lblgrandTotal.Text = (row_count["gtotal"].ToString());
        }
      
    }
    public DataTable bindgrid(string labId,string id)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_getgetPurchaseMaster " + "'" + labId + "','"+id+"'");
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
   
    
}
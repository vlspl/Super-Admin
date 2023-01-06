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
using System.Globalization;

public partial class purchase : System.Web.UI.Page
{
    InputValidation Ival = new InputValidation();
    DataAccessLayer DAL = new DataAccessLayer();
    DBClass db = new DBClass();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            string labId = Request.Cookies["labId"].Value.ToString();
            db.bindDrp("select distinct vendorMasterId, vendorName from tbl_VendorMaster where sLabId='"+labId+"'  order by vendorName asc", drpvendorName, "vendorName", "vendorMasterId");
            drpvendorName.Items.Insert(0, new ListItem("-Select Vendor-", "-Select Vendor-"));
            db.bindDrp("select distinct materialName from tbl_MaterialMaster where sLabId='" + labId + "'    order by materialName asc", drpmaterial, "materialName", "materialName");
           // drpmaterial.Items.Insert(0, new ListItem("-Select Material-", "-Select Material-"));
           // SqlParameter[] param_maxId = new SqlParameter[]
           //      {
           //           new SqlParameter("@LabId", labId)
           //       };
           //txtgrnNo.Text = DAL.ExecuteScalarWithProc("Sp_getmaxgrnId", param_maxId);
           
        }
    }
  
  
    protected void temp_material_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string labId = Request.Cookies["labId"].Value.ToString();
        SqlDataAdapter da;
        SqlConnection con;
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        cmd.Connection = con;
        Label lbldeleteID = (Label)temp_material.Rows[e.RowIndex].FindControl("lblid");
        cmd.CommandText = "Delete from temp_MaterialMaster where tempMaterialId='" + lbldeleteID.Text + "' and sLabId='" + labId + "'";
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        gridBind(); 
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        string labId = Request.Cookies["labId"].Value.ToString();
       
            if (txtrate.Text != "")
            {
                if (txtqty.Text != "")
                {
                    db.insert("insert into temp_MaterialMaster(materialName,rate,qty,unit,amount,cgst,sgst,igst,Total,sLabId) values('"+drpmaterial.Text+"','"+txtrate.Text+"','"+txtqty.Text+"','"+drpunit.Text+"','"+txtamount.Text+"','"+txtcgst.Text+"','"+txtsgst.Text+"','"+txtigst.Text+"','"+txttotal.Text+"','"+labId+"')");
                    gridBind();
                    clearField();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Enter Material Qty');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Enter Material Rate');", true);
            }
            
       
    }
    void clearField()
    {
       
        txtrate.Text = "";
        txtqty.Text = "";
        txtamount.Text = "";
        txtcgst.Text = "";
        txtsgst.Text = "";
        txtigst.Text = "";
        txttotal.Text = "";
    }
    void gridBind()
    {
        string labId = Request.Cookies["labId"].Value.ToString();
        temp_material.DataSource = bindgrid(labId);//"Sp_getlabtestList"+drplablist.Text);
        temp_material.DataBind();
        footerSum(labId);
    }
    void footerSum(string labId)
    {
        
        SqlParameter[] param_count = new SqlParameter[]
                {
                    new SqlParameter("@LabId",labId),
                    
                 };
        DataTable dt_counter = DAL.ExecuteStoredProcedureDataTable("Sp_gettemp_material_sum", param_count);
        foreach (DataRow row_count in dt_counter.Rows)
        {
             lblnetamt.Text=(row_count["amt"].ToString());
             lblcgst.Text =(row_count["cgst"].ToString());
           lblsgst.Text =(row_count["sgst"].ToString());
            lbligst.Text =(row_count["igst"].ToString());
            lblgrandTotal.Text = (row_count["gtotal"].ToString());
        }
      
    }
    public DataTable bindgrid(string labId)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_gettemp_material " + "'" + labId + "'");
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    protected void txtqty_TextChanged(object sender, EventArgs e)
    {
        string labId = Request.Cookies["labId"].Value.ToString();
        string cgst = db.getData("select cgst from tbl_MaterialMaster where materialName='"+drpmaterial.Text+"' and slabId='"+labId+"'");
        string sgst = db.getData("select sgst from tbl_MaterialMaster where materialName='" + drpmaterial.Text + "' and slabId='" + labId + "'");
        string igst = db.getData("select igst from tbl_MaterialMaster where materialName='" + drpmaterial.Text + "' and slabId='" + labId + "'");
        txtamount.Text = (float.Parse(txtrate.Text) * float.Parse(txtqty.Text)).ToString();
        txtcgst.Text = (float.Parse(txtamount.Text) * float.Parse(cgst) / 100).ToString();
        txtsgst.Text = (float.Parse(txtamount.Text) * float.Parse(sgst) / 100).ToString();
        txtigst.Text = (float.Parse(txtamount.Text) * float.Parse(igst) / 100).ToString();
        txttotal.Text = (float.Parse(txtamount.Text) + float.Parse(txtcgst.Text) + float.Parse(txtsgst.Text) + float.Parse(txtigst.Text)).ToString(); 
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string frodate = txtdate.Text;
        string d1 = frodate;
        DateTime appointdt1 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        string startDate = appointdt1.ToString("MM/dd/yyyy");
        int purchaseMasterId, purchaseDetailsId;
        string labId = Request.Cookies["labId"].Value.ToString();
        if (drpvendorName.Text != "-Select Vendor-")
        {
           
                SqlParameter[] param_Master = new SqlParameter[]
                {   
                    new SqlParameter("@vendorMasterId", drpvendorName.Text),
                    new SqlParameter("@Date", startDate),
                    new SqlParameter("@invoiceNo", txtinvoiceNo.Text),
                    new SqlParameter("@grnNo", txtgrnNo.Text),
                    new SqlParameter("@billType", drpbillType.Text),
                    new SqlParameter("@type", drptype.Text),
                    new SqlParameter("@description", txtdescription.Text),
                    new SqlParameter("@grandTotal", lblgrandTotal.Text),
                    new SqlParameter("@status", "Unpaid"),
                    new SqlParameter("@sLabId", labId),
                    new SqlParameter("@returnval", SqlDbType.Int),
                };
                purchaseMasterId = DAL.ExecuteStoredProcedureRetnInt("Sp_insertpurchaseMaster", param_Master);
                SqlParameter[] param_maxId = new SqlParameter[]
                 {
                      new SqlParameter("@LabId", labId)
                  };
                string maxId = DAL.ExecuteScalarWithProc("Sp_getmaxpmId", param_maxId);
                SqlParameter[] param_DetailsGet = new SqlParameter[]
                 {
                      new SqlParameter("@LabId", labId)
                  };
                DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_gettemp_material", param_DetailsGet);
                if (ds != null)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SqlParameter[] param_dtls = new SqlParameter[]
                        {
                            new SqlParameter("@materialName", row["materialName"].ToString()),
                            new SqlParameter("@rate", row["rate"].ToString()),
                            new SqlParameter("@qty", row["qty"].ToString()),
                            new SqlParameter("@unit", row["unit"].ToString()),
                            new SqlParameter("@amount", row["amount"].ToString()),
                              new SqlParameter("@cgst", row["cgst"].ToString()),
                                new SqlParameter("@sgst", row["sgst"].ToString()),
                                  new SqlParameter("@igst", row["igst"].ToString()),
                                    new SqlParameter("@Total", row["Total"].ToString()),
                                     new SqlParameter("@purchaseMasterId", maxId),
                                    new SqlParameter("@sLabId", row["sLabId"].ToString()),
                                   
                            new SqlParameter("@returnval", SqlDbType.Int),
                        };
                        purchaseDetailsId = DAL.ExecuteStoredProcedureRetnInt("Sp_insertpurchaseDetails", param_dtls);

                     }
                   
                    db.insert("delete from temp_MaterialMaster where sLabId='"+labId+"' ");
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Purchase Entry Added successfully');location.href='purchaseLedger.aspx';", true); 
                   
                }
                
           
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Vendor Name');", true);
        }
    }
    protected void drpvendorName_SelectedIndexChanged(object sender, EventArgs e)
    {
         string labId = Request.Cookies["labId"].Value.ToString();
         lblgstinNo.Text = db.getData("select gstinNo from tbl_VendorMaster where vendorMasterId='" + drpvendorName.Text + "' and sLabId='" + labId + "'");
    }
}
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

public partial class vendorPayment : System.Web.UI.Page
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
           
        }
    }

  
    string InvoiceId;
    Decimal balAmount = 0;
    Decimal pdAmt = 0, totalAmt = 0;
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (drpvendorName.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Vendor Name');", true);
        }
        else if (txtpaidAmount.Text == "0")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Paid Amount Grater Than 0 Amount');", true);
        }
        else if (Double.Parse(txtpaidAmount.Text) > Double.Parse(txtgrandtotal.Text))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Paid Amount should not Grater than Total Amount');", true);
           
        }
        else if (drppaymode.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Payment Mode');", true);
        }
        else
        {
            string invoiceId = "" ;
            CheckBox chkRow;
            DataTable getInvoiceDetails = new DataTable();
            getInvoiceDetails = (DataTable)Session["invoiceDetails"];
            string labId = Request.Cookies["labId"].Value.ToString();
            for (int i = 0; i <= getInvoiceDetails.Rows.Count - 1; i++)
            {
                chkRow = (gridvm.Rows[i].Cells[1].FindControl("chkseletedValues") as CheckBox);
                if (chkRow.Checked)
                {
                    invoiceId = getInvoiceDetails.Rows[i].Field<string>("invoiceNo");
                }
            }
            string frodate = txtdate.Text;
            string d1 = frodate;
            DateTime appointdt1 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string startDate = appointdt1.ToString("MM/dd/yyyy");
            db.insert("insert into tbl_vendorPaymentMaster(vendorMasterId,invoiceNo,date,totalAmount,paymentMode,remark,billSelected,sLabId,transactionId) values('" + drpvendorName.Text + "','" + invoiceId + "','" + startDate + "','" + txtgrandtotal.Text + "','" + drppaymode.Text + "','" + txtremark.Text + "','" + txtbillselected.Text + "','" + labId + "','" + txttransactionId.Text + "')");
            string maxPaymentId = db.getData("select max(vendorPaymentMasterId) from tbl_vendorPaymentMaster where sLabId='" + labId + "'").ToString();
            pdAmt = Decimal.Parse(txtpaidAmount.Text);
            for (int i = 0; i <= getInvoiceDetails.Rows.Count - 1; i++)
            {
                chkRow = (gridvm.Rows[i].Cells[1].FindControl("chkseletedValues") as CheckBox);
                if (chkRow.Checked)
                {

                    int purchaseMasterId = getInvoiceDetails.Rows[i].Field<Int32>("purchaseMasterId");
                    InvoiceId = getInvoiceDetails.Rows[i].Field<string>("invoiceNo");
                    totalAmt = getInvoiceDetails.Rows[i].Field<Decimal>("grandTotal");
                    balAmount = getInvoiceDetails.Rows[i].Field<Decimal>("balanceAmt");
                    // balAmount = Decimal.Parse(txtbalanceAmount.Text);

                    if (pdAmt != 0)
                    {

                        if (pdAmt >= balAmount)
                        {
                            db.insert("update tbl_purchaseMaster set status='Paid' where invoiceNo='" + InvoiceId + "' and sLabId='" + labId + "'");
                            //pdAmt = (pdAmt - balAmount);
                            db.insert("insert into tbl_vendorPaymentDetail(vendorPaymentMasterId,purchaseMasterId,paidAmount,balanceAmount,sLabId) values('" + maxPaymentId + "','" + purchaseMasterId + "','"+txtpaidAmount.Text+"','" + txtbalance.Text + "','" + labId + "')");

                        }
                        else
                        {
                            //balAmount = Math.Round(balAmount - pdAmt);
                            //pdAmt = (pdAmt - balAmount);
                            db.insert("insert into tbl_vendorPaymentDetail(vendorPaymentMasterId,purchaseMasterId,paidAmount,balanceAmount,sLabId) values('" + maxPaymentId + "','" + purchaseMasterId + "','" + txtpaidAmount.Text + "','" + txtbalance.Text + "','" + labId + "')");
                            pdAmt = 0;
                        }
                      
                    }

                }
              
            }
            //Response.Redirect("viewPaymentDetails.aspx?paymentMasterId=" + maxPaymentId + "");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Vendor Payment Paid Successfully');location.href='VendorLedger.aspx';", true);
        }

    }
    protected void drpvendorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string labId = Request.Cookies["labId"].Value.ToString();
        SqlParameter[] param_count = new SqlParameter[]
                {
                    new SqlParameter("@LabId",labId),
                    new SqlParameter("@vendorMasterId",drpvendorName.Text),
                 };
        DataTable dt_counter = DAL.ExecuteStoredProcedureDataTable("Sp_vp_purchaseMaster", param_count);
        gridvm.DataSource = dt_counter;
        gridvm.DataBind();
        Session["invoiceDetails"] = dt_counter;
    }
    protected void drppaymode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drppaymode.Text == "Online")
        {
            online.Visible = true;
        }
        else
        {
            online.Visible = false;
        }
    }
    string multipleemails = "";
    protected void chkseletedValues_CheckedChanged(object sender, EventArgs e)
    {
        string labId = Request.Cookies["labId"].Value.ToString();
        DataTable getHwDetails = new DataTable();
        float Sum = 0;
        getHwDetails = (DataTable)Session["invoiceDetails"];
        string InvoiceId; string totalAmount;
        CheckBox chkRow;
        float amount = 0;
        Decimal balAmt = 0;
        string inviocebill = string.Empty;
        txtbillselected.Text = string.Empty;
        string billno = "";
        for (int i = 0; i <= getHwDetails.Rows.Count - 1; i++)
        {
            chkRow = (gridvm.Rows[i].Cells[1].FindControl("chkseletedValues") as CheckBox);
            if (chkRow.Checked)
            {
                // txtbillselected.Text += getHwDetails.Rows[i].Field<string>("invoiceBillNo") + ",";
                InvoiceId = getHwDetails.Rows[i].Field<string>("invoiceNo");
               balAmt = getHwDetails.Rows[i].Field<Decimal>("balanceAmt");
               billno += InvoiceId + " , ";
                txtbillselected.Text = "";
                multipleemails = billno.Remove(billno.Length - 2);

                if (!db.ChkDb_Value("select invoiceNo from tbl_vendorPaymentDetail  where invoiceNo='" + InvoiceId + "' and sLabId='" + labId + "'"))
                {
                    txtbillselected.Text += getHwDetails.Rows[i].Field<string>("invoiceNo") + ",";
                    Sum += float.Parse(balAmt.ToString());

                }
                else
                {
                    txtbillselected.Text += getHwDetails.Rows[i].Field<string>("invoiceNo") + ",";
                    // amount = float.Parse(objdb.getDbstatus_Value("select balanceAmount from invoicePaymentDetails  where invoiceBillNo='" + inviocebill + "' and companyMasterId='" + hdncompanyMasterId.Value + "'"));
                    //Sum += amount;
                    Sum += float.Parse(balAmt.ToString());

                }


            }
            txtgrandtotal.Text = Sum.ToString();
            txtbalance.Text = Sum.ToString();
            txtbillselected.Text = multipleemails.ToString();
        }
    }
    protected void txtpaidAmount_TextChanged(object sender, EventArgs e)
    {
        txtbalance.Text = ((float.Parse(txtgrandtotal.Text) - float.Parse(txtpaidAmount.Text))).ToString();
    }
}
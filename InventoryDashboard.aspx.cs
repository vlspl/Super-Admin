using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InventoryDashboard : System.Web.UI.Page
{
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showCounter();
        }
    }
    void showCounter()
    {
        string labId = Request.Cookies["labId"].Value.ToString();
        lblmaterialCount.Text = db.getData("select count(*) from tbl_MaterialMaster where sLabId='"+labId+"'");
        lblvendorCount.Text = db.getData("select count(*) from tbl_VendorMaster where sLabId='" + labId + "'");
        lblnoofBills.Text = db.getData("select count(*) from tbl_purchaseMaster where sLabId='" + labId + "'");
        lblunpaidBills.Text = db.getData("select count(*) from tbl_purchaseMaster where sLabId='" + labId + "' and status='Unpaid'");
        lblpaidBills.Text = db.getData("select count(*) from tbl_purchaseMaster where sLabId='" + labId + "' and status='Paid'");
    }
}
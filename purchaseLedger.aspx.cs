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

public partial class purchaseLedger : System.Web.UI.Page
{
    InputValidation Ival = new InputValidation();
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showgrid();
        }
    }
    void showgrid()
    {
        string labId = Request.Cookies["labId"].Value.ToString();
        SqlParameter[] paramGetPurchseEntry = new SqlParameter[]
         {
              new SqlParameter("@sLabId", labId)         

          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_getpurchaseEntryRecord", paramGetPurchseEntry);
       if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllUserList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;
                     AllUserList += "<tr>" +
                                       "<td scope='col'>" + count + "</td>" +
                                     "<td scope='col'>" + row["Date"].ToString() + "</td>" +
                                         "<td scope='col'>" + row["invoiceNo"].ToString() + "</td>" +
                                          "<td scope='col'>" + row["vendorName"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["grandTotal"].ToString() + "</td>" +
                                                 "<td scope='col'>" + row["billType"].ToString() + "</td>" +
                                                  "<td scope='col'>" + row["status"].ToString() + "</td>" +
                                           "<td scope='col'><a href='Viewpurchase.aspx?id=" + row["purchaseMasterId"].ToString() + "' >   View </a></td>" +
                                       "</tr>";



                }
                tbodypurchaseLedger.InnerHtml = AllUserList;
            }
            else
            {
                tbodypurchaseLedger.InnerHtml = "<tr><td colspan='5'>No records found</td></tr>";
            }
        }
    }
   
  
}
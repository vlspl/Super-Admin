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

public partial class VendorMaster : System.Web.UI.Page
{
    InputValidation Ival = new InputValidation();
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showGrid();
        }
    }
    void showGrid()
    {

        string labId = Request.Cookies["labId"].Value.ToString();
        SqlParameter[] paramEmg_getMaterial = new SqlParameter[]
         {
                           
              new SqlParameter("@sLabId", labId)
          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_getVendorList", paramEmg_getMaterial);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllUserList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;
                    //Load lab test taken list
                    AllUserList += "<tr>" +
                                       "<td scope='col'>" + count + "</td>" +
                                     "<td scope='col'>" + row["vendorName"].ToString() + "</td>" +
                                         "<td scope='col'>" + row["companyName"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["mobileNo"].ToString() + "</td>" +
                                             "<td scope='col'>" + row["gstinNo"].ToString() + 
                                            "<td scope='col'>" + row["address"].ToString() + "</td>" +
                                          
                                       "</tr>";



                }
                tbodyMaterialMaster.InnerHtml = AllUserList;
            }
            else
            {
                tbodyMaterialMaster.InnerHtml = "<tr><td colspan='5'>No records found</td></tr>";
            }
        }
    
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int VendorId;
        string labId = Request.Cookies["labId"].Value.ToString();
        if (txtvendorName.Text != "")
        {
            if (txtcompanyName.Text != "")
            {
                if (txtmobileNo.Text != "")
                {
                    SqlParameter[] param_materialMaster = new SqlParameter[]
                        {
                            new SqlParameter("@vendorName", txtvendorName.Text),
                            new SqlParameter("@companyName", txtcompanyName.Text),
                            new SqlParameter("@address", txtaddress.Text),
                            new SqlParameter("@mobileNo", txtmobileNo.Text),
                            new SqlParameter("@panNo", txtpanno.Text),
                            new SqlParameter("@gstinNo", txtgstinNo.Text),
                            new SqlParameter("@sLabId", labId),
                            new SqlParameter("@createdDate", System.DateTime.Now.ToString("MM/dd/yyyy")),
                            new SqlParameter("@returnval", SqlDbType.Int),
                        };
                    VendorId = DAL.ExecuteStoredProcedureRetnInt("Sp_insertVendor", param_materialMaster);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Vendor Added successfully');location.href='VendorMaster.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Enter Mobile No');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Enter Company Name');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Enter Vendor Name');", true);
        }
           
    }
  
}
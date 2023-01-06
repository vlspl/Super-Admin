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

public partial class materialMaster : System.Web.UI.Page
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
         DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_getMaterialList", paramEmg_getMaterial);
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
                                     "<td scope='col'>" + row["materialName"].ToString() + "</td>" +
                                         "<td scope='col'>" + row["unit"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["minStock"].ToString() + "</td>" +
                                          
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
        int materialId ;
        string labId = Request.Cookies["labId"].Value.ToString();
        if(txtmaterialName.Text!="")
        {
            if(drpunit.Text!="-Select Unit-")
            {
                SqlParameter[] param_materialMaster = new SqlParameter[]
                {
                    new SqlParameter("@materialName", txtmaterialName.Text),
                    new SqlParameter("@unit", drpunit.SelectedItem.Text),
                    new SqlParameter("@description", txtdescription.Text),
                    new SqlParameter("@minStock", txtminimumStock.Text),
                    new SqlParameter("@sLabId", labId),
                    new SqlParameter("@cgst", txtcgst.Text),
                new SqlParameter("@sgst", txtsgst.Text),
                new SqlParameter("@igst", txtigst.Text),
                    new SqlParameter("@createdDate", System.DateTime.Now.ToString("MM/dd/yyyy")),
                    new SqlParameter("@returnval", SqlDbType.Int),
                };
                materialId = DAL.ExecuteStoredProcedureRetnInt("Sp_insertMaterial", param_materialMaster);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Material Added successfully');location.href='materialMaster.aspx';", true);
            }
             else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Material Unit');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Enter Material Name');", true);
        }
           
    }
  
}
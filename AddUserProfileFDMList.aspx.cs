using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using Validation;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class SuperAdmin_AddUserProfileFDMList : System.Web.UI.Page
{
    CLSUserProfileFDMList objfdmlist = new CLSUserProfileFDMList();
    CLSUserProfileFDMListInsert objfdmlistInsert = new CLSUserProfileFDMListInsert();
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnupdate.Visible = false;
            if (Request.QueryString["listId"] != null)
            {

                hdnlistId.Value = Request.QueryString["listId"].ToString();
                getfdmList();
            }

        }
           
       
    }
    public void getfdmList()
    {
        int listId = Convert.ToInt32(Request.QueryString["listId"].ToString());
        DataTable ds = objfdmlist.getUPFDMList(listId);
        btnsubmit.Visible = false;
        btnupdate.Visible = true;
        foreach (DataRow row in ds.Rows)
        {

            txtname.Text = row["name"].ToString();
            drpfdmType.SelectedItem.Text = row["type"].ToString();
            txtremark.Text = row["remark"].ToString();
            drpstatus.SelectedItem.Text = row["status"].ToString();
            
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (drpfdmType.Text != "-Select-")
        {
            if(drpstatus.Text!="-Select-")
            {
            objfdmlistInsert.name = txtname.Text.ToString();
            objfdmlistInsert.type = drpfdmType.SelectedItem.Text.ToString();
            objfdmlistInsert.remark = txtremark.Text.ToString();
            objfdmlistInsert.status = drpstatus.SelectedValue.ToString();
            hdncreatedby.Value = Request.Cookies["AdminUserName"].Value.ToString();
            objfdmlistInsert.createdBy = hdncreatedby.Value;
            objfdmlistInsert.createdDate = System.DateTime.Now.ToString();


            if (objfdmlistInsert.insertFDMList() == 0)
            {
                Response.Redirect(@"FDMList.aspx");

            }

            else if (objfdmlistInsert.insertFDMList() == 1)
            {
                Label lblMasterStatus = (Label)Master.FindControl("lblmsgText");
                lblMasterStatus.Text = "Error while Adding FDM List";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "Yopopupalert();", true);


            }
            }
             else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select FDM Status');", true);
        }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select FDM Type');", true);
        }
       
    }
    

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string listId = hdnlistId.Value;
        string name = txtname.Text;
        string type = drpfdmType.Text;
        string remark = txtremark.Text;
        string status = drpstatus.Text;

        string createdBy = Request.Cookies["AdminUserName"].Value.ToString();
        string createdDate = System.DateTime.Now.ToString();
       
        int Uplist = objfdmlistInsert.updateList(listId, name, type, remark, status, createdBy, createdDate);
        if (Uplist == 1)
        {
            Response.Redirect(@"FDMList.aspx");
           
        }
    }
}
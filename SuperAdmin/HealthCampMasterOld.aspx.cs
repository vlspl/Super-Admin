using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using Validation;
using BitsBizLogic;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_HealthCampMaster : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    string orgId="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                ddlName.DataSource = DAL.GetDataTable("Sp_GetOrgnizationDetailForDDL");
                ddlName.DataBind();
                ListItem lit = new ListItem();
                lit.Text = "Select";
                lit.Value = "0";
                ddlName.Items.Insert(0, lit);
              
  
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx", false);
        }
    }
   

    protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLabName.Text != "Select")
        {
            // bind technician 
            orgId = ddlName.SelectedValue;

            // bind lab 
            ddlLabName.DataSource = DAL.GetDataTable("Sp_GetLabNameDetailForDDL " + orgId);
            ddlLabName.DataBind();
            // bind Dept 
            ddlDeptName.DataSource = DAL.GetDataTable("Sp_GetDeptDetailForDDL " + orgId);
            ddlDeptName.DataBind();
            //bind Branch
            ddlBranchName.DataSource = DAL.GetDataTable("Sp_GetBranchDetailForDDL " + orgId);
            ddlBranchName.DataBind();

 if (ddlBranchName.Items.Count == 1)
                {
                    SqlParameter[] param = new SqlParameter[]
                      {
                              new SqlParameter("@orgId",ddlName.SelectedValue),
                              new SqlParameter("@branchId","0"),
                     };
                    //   ddlEmpName.DataSource = DAL.GetDataTable("Sp_GetEmpDetailForDDL " + ddlBranchName.SelectedValue);
                    ddlEmpName.DataSource = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmpDetailForDDL", param);

                    ddlEmpName.DataBind();
                }
        }

    }

    protected void BtnSave_Click1(object sender, EventArgs e)
    {
        string Msg = "";
        if (ddlName.SelectedIndex <= 0)
        {
            Msg += "● Please Select Organization<br>";
        }
        if (ddlLabName.SelectedIndex <= 0)
        {
            Msg += "● Please Enter Valid Lab Name";
        }
        if (ddlEmpName.SelectedIndex <= 0)
        {
            Msg += "● Please Enter Valid Technician Name";
        }
        if (ddlDeptName.SelectedIndex <= 0)
        {
            Msg += "● Please Enter Valid Department Name";
        }
        if (Msg.Length > 0)
        {
            litErrorMessage.Text = ApplicationLogic.ErrorWarning(Msg);
        }
        else
        {
            ///Submit Data To Database


            SqlParameter[] param1 = new SqlParameter[]
                         {
                             new SqlParameter("@userId",ddlEmpName.SelectedValue),
                             
                             new SqlParameter("@returnval",SqlDbType.Int)
                          };
            int Resultval = DAL.ExecuteStoredProcedureRetnInt("SP_updateRole", param1); //organization user
            SqlParameter[] paramHealthCamp = new SqlParameter[]
                         {
                             new SqlParameter("@healthCampName",txthealthcampName.Text),
                             new SqlParameter("@ownerName",txtownerName.Text),
                             new SqlParameter("@otherDetails",txtotherDetails.Text),
                             new SqlParameter("@orgId",ddlName.SelectedValue),
                             new SqlParameter("@userId",ddlEmpName.SelectedValue),
                             new SqlParameter("@labId",ddlLabName.SelectedValue),
                             new SqlParameter("@branchId",ddlBranchName.SelectedValue),
                             new SqlParameter("@branchName",ddlBranchName.SelectedItem.Text),
                             new SqlParameter("@status","1"),
                             new SqlParameter("@returnval",SqlDbType.Int)
                          };
            int ResultvalHealth = DAL.ExecuteStoredProcedureRetnInt("SP_addHealthCampMaster", paramHealthCamp);


            litErrorMessage.Text = ApplicationLogic.SuccessMessage("● Saved Suceessfully.");

        }
    }
    protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@orgId",ddlName.SelectedValue),
            new SqlParameter("@branchId",ddlBranchName.SelectedValue),
        };
     //   ddlEmpName.DataSource = DAL.GetDataTable("Sp_GetEmpDetailForDDL " + ddlBranchName.SelectedValue);
        ddlEmpName.DataSource = DAL.ExecuteStoredProcedureDataTable("Sp_GetEmpDetailForDDL",param);

        ddlEmpName.DataBind();


    }
}
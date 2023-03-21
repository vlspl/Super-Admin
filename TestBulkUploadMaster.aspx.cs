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


public partial class SuperAdmin_TestBulkUploadMaster : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    DBClass db = new DBClass();
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
                if (Request.QueryString["testResultUploadId"] != null)
                {
                    DataTable dt = DAL.GetDataTable("Select Name,testName,testUploadName,colSpecification ,requiredColumnsInNo from tbl_TestResultUploadSpec inner join OrganizationMaster om on om.ID=tbl_TestResultUploadSpec.orgId where testResultUploadId='" + Request.QueryString["testResultUploadId"] + "'");
                  //  string testname = db.getData("Select testName from tbl_TestResultUploadSpec where orgId='" + ddlName.SelectedValue + "'");
                    foreach (DataRow row in dt.Rows)
                    {
                        ddlName.SelectedItem.Text =row["Name"].ToString();
                       // ddlTestName.SelectedItem.Text = testname; //row["testName"].ToString();
                        txtColSpec.Text = row["colSpecification"].ToString();
                        txtTestuploadName.Text = row["testUploadName"].ToString();
                        string status = row["requiredColumnsInNo"].ToString();
                        if (status == "1")
                            CheckBox1.Checked = true;
                        else
                            CheckBox1.Checked = false;
                    }
                    BtnSave.Text = "Update";
                    grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetTestUploadDetailByOrg "+ddlName.SelectedValue);
                    grdviewOrgnization.DataBind();

                }

            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx", false);
        }
    }
    protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTestName.DataSource = DAL.GetDataTable("Sp_GetTestNameForDDL " + ddlName.SelectedValue);
        ddlTestName.DataBind();

        grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetTestUploadDetailByOrg " + ddlName.SelectedValue);
        grdviewOrgnization.DataBind();

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string Msg = "";
        if (ddlName.SelectedIndex <= 0)
        {
            Msg += "● Please Select Organization<br>";
        }
        if (ddlTestName.SelectedIndex <= 0)
        {
            Msg += "● Please Select Test<br>";
        }
        if (Ival.IsTextBoxEmpty(txtColSpec.Text))
        {
            Msg += "● Please Enter Test Analytes<br>";
        }
        if (Ival.IsTextBoxEmpty(txtTestuploadName.Text))
        {
            Msg += "● Please Enter Valid Test Upload Name<br>";
        }
        if (Msg.Length > 0)
        {
            litErrorMessage.Text = ApplicationLogic.ErrorWarning(Msg);
        }
        else
        {
            string testCode, testId, requiredcolumnStatus;

          //  DataTable dt = DAL.GetDataTable("select * from test where sTestName='" + ddlTestName.SelectedItem.Text + "'");
          DataTable dt = DAL.GetDataTable(" select  test.sTestId,sTestCode from OrganizationTieupLab otl  inner join labMaster lm on lm.sLabId=otl.Lab_Id and otl.Org_ID='"+ddlName.SelectedItem.Value+"'  inner join testLab tl on tl.sLabId=lm.sLabId  inner join test on test.sTestId=tl.sTestId and sTestName ='" + ddlTestName.SelectedItem.Text + "' order by sTestId desc");

 		testCode = dt.Rows[0]["sTestCode"].ToString();
            testId = dt.Rows[0]["sTestId"].ToString();
            if (CheckBox1.Checked)
                requiredcolumnStatus = "1";
            else
                requiredcolumnStatus = "0";

            if (Request.QueryString["testResultUploadId"] == null)
            {
                ///Submit Data To Database
                //get Required data here
               

                SqlParameter[] param = new SqlParameter[]
                         {
                             new SqlParameter("@orgId",ddlName.SelectedValue),
                             new SqlParameter("@testCode",testCode),
                             new SqlParameter("@testName",ddlTestName.SelectedItem.Text),
                             new SqlParameter("@colSpec",txtColSpec.Text),
                             new SqlParameter("@status",1),
                             new SqlParameter("@testUploadName",txtTestuploadName.Text),
                             new SqlParameter("@testId",testId),
                             new SqlParameter("@requiredColumnsInNo",requiredcolumnStatus),
                             new SqlParameter("@ReturnVal",SqlDbType.Int)
                          };
                int Result = DAL.ExecuteStoredProcedureRetnInt("SP_addOrgTestMaster", param);   //tbl_TestResultUploadSpec

                    SqlParameter[] paramOrgTest = new SqlParameter[]
                {
                     new SqlParameter("@OrgId",ddlName.SelectedValue),
                     new SqlParameter("@TestId",testId),
                     new SqlParameter("@ReturnVal",SqlDbType.Int)
                  };
                int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOrgTest", paramOrgTest); //Organization Test Master
               

			 grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetTestUploadDetailByOrg " + ddlName.SelectedValue);
                grdviewOrgnization.DataBind();
             
                // Display Message
                if (Result >= 1)
                {
                    litErrorMessage.Text = ApplicationLogic.SuccessMessage("● Saved Suceessfully.");
                }
                else if (Result == -2)
                {
                    litErrorMessage.Text = ApplicationLogic.ErrorWarning("● Already Exist!");
                }
                else
                {
                    litErrorMessage.Text = ApplicationLogic.ErrorWarning("● Something Went Wrong, Please try Again!");
                }
            }
            else
            {

                SqlParameter[] param = new SqlParameter[]
                         {
                             new SqlParameter("@orgId",ddlName.SelectedValue),
                             new SqlParameter("@testCode",testCode),
                             new SqlParameter("@testName",ddlTestName.SelectedItem.Text),
                             new SqlParameter("@colSpec",txtColSpec.Text),
                             new SqlParameter("@status",1),
                             new SqlParameter("@testUploadName",txtTestuploadName.Text),
                             new SqlParameter("@testId",testId),
                             new SqlParameter("@requiredColumnsInNo",requiredcolumnStatus),
                             new SqlParameter("@ReturnVal",SqlDbType.Int)
                          };
                int Result = DAL.ExecuteStoredProcedureRetnInt("SP_UpdateOrgTestMaster", param);  //brnach master
                litErrorMessage.Text = ApplicationLogic.SuccessMessage("● Updated Suceessfully.");
            }
            txtColSpec.Text = txtTestuploadName.Text =  string.Empty;

        }
    }
    protected void grdviewOrgnization_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int orgId = 0, testId = 0;
        try
        {
            int deleteID = Convert.ToInt32(grdviewOrgnization.DataKeys[e.RowIndex].Value.ToString());
           DataTable dt = DAL.GetDataTable("select testResultUploadId,orgId,testId  from tbl_TestResultUploadSpec where testResultUploadId='" + deleteID + "'");
            foreach(DataRow row in dt.Rows)
            {
                 orgId=int.Parse(row["orgId"].ToString());
                 testId = int.Parse(row["testId"].ToString());
            }

            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@Id",deleteID),
               new SqlParameter("@orgId",orgId),
               new SqlParameter("@testId",testId)

            };
           

            DAL.ExecuteStoredProcedure("Sp_DeleteTestUploadDetails ", param);
            grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetTestUploadDetailByOrg " + ddlName.SelectedValue);
            grdviewOrgnization.DataBind();
        }
        catch (Exception ex)
        {
            litErrorMessage.Text = "Somthing went Wrong. Please Try Again.!!!";
        }
    }

    protected void grdviewOrgnization_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdviewOrgnization.PageIndex = e.NewPageIndex;
        grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetTestUploadDetailByOrg " + ddlName.SelectedValue);
        grdviewOrgnization.DataBind();
    }
    protected void grdviewOrgnization_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = (grdviewOrgnization.DataKeys[grdviewOrgnization.SelectedRow.DataItemIndex].Value).ToString();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@Id",id)
            };

            //  DAL.ExecuteStoredProcedure("Sp_ActiveOrgnization ", param);
            grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetTestUploadDetailByOrg " + ddlName.SelectedValue);
            grdviewOrgnization.DataBind();
        }
        catch (Exception ex)
        {
            litErrorMessage.Text = "Somthing went Wrong. Please Try Again.!!!";
        }

        grdviewOrgnization.DataSource = DAL.GetDataTable("Sp_GetTestUploadDetailByOrg " + ddlName.SelectedValue);
        grdviewOrgnization.DataBind();

    }
    protected void grdviewOrgnization_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.DataItem != null)
        //{
        //    DataRowView dr = (DataRowView)e.Row.DataItem;
        //    string mobNo = (dr["sMobile"].ToString());
        //    System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        //    e.Row.Cells[6].Text = CryptoHelper.Decrypt(mobNo);
        //}
    }


}
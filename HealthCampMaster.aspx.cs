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
using System.Configuration;
using System.Web.Configuration;

public partial class SuperAdmin_HealthCampMaster : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    DBClass db = new DBClass();
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
            //ddlDeptName.DataSource = DAL.GetDataTable("Sp_GetDeptDetailForDDL " + orgId);
            //ddlDeptName.DataBind();
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
        int userId, Result=0;
        string Msg = "";
        if (ddlName.SelectedIndex <= 0)
        {
            Msg += "● Please Select Organization<br>";
        }
        if (txtownerName.Text== "")
        {
            Msg += "● Please Enter Valid Owner Name<br>";
        }
        if (ddlLabName.SelectedIndex <= 0)
        {
            Msg += "● Please Enter Valid Lab Name";
        }
        if (drpTechType.Text == "Select")
        {
            Msg += "● Please Enter Valid Technician Type";
        }
        if (drpTechType.Text != "Howzu")
        {
            if (ddlEmpName.SelectedIndex <= 0)
            {
                Msg += "● Please Enter Valid Technician Name";
            }
        }
       
        //if (ddlDeptName.SelectedIndex <= 0)
        //{
        //    Msg += "● Please Enter Valid Department Name";
        //}
        if (Msg.Length > 0)
        {
            litErrorMessage.Text = ApplicationLogic.ErrorWarning(Msg);
        }
        else
        {
            ///Submit Data To Database
            if (drpTechType.Text == "Howzu")
            {
                string password = CreateRandomPassword();
                string _pwd = CryptoHelper.Encrypt(password);
                //insert into emp details
                SqlParameter[] paramEmp = new SqlParameter[]
                {

                 new SqlParameter("@FName", txtTechnicianName.Text),
                 new SqlParameter("@MName", ""),
                 new SqlParameter("@LName", ""),
                 new SqlParameter("@SName", ""),
                 new SqlParameter("@AtdharName", ""),
                 new SqlParameter("@DOB",""),
                 new SqlParameter("@Age", ""),
                 new SqlParameter("@Gender",""),
                 new SqlParameter("@Pphoto", ""),//ObjBO.Pphoto


                 new SqlParameter("@PanNo", ""),
                 new SqlParameter("@Email ", ""),
                 new SqlParameter("@ContactNo",""),
                 new SqlParameter("@AltContact", ""),
                 new SqlParameter("@CAddress", ""),
                 new SqlParameter("@PAddress", ""),
                 new SqlParameter("@State", ""),
                 new SqlParameter("@City", ""),
                 new SqlParameter("@Pincode",""),
                 new SqlParameter("@EmpId", ""),
                 new SqlParameter("@Dsgn",""),
                 new SqlParameter("@Dept", ""),
                 new SqlParameter("@BatchName", ddlBranchName.SelectedItem.Text),
                 new SqlParameter("@DOJ", ""),
                 new SqlParameter("@EmpStatus", ""),
                 new SqlParameter("@Bgroup",""),
                 new SqlParameter("@HealthID", ""),
                 new SqlParameter("@Qualification",""),
                 new SqlParameter("@Pyear", ""),
                 new SqlParameter("@Grade", ""),
                 new SqlParameter("@University", ""),
                 new SqlParameter("@CompName", ""),
                 new SqlParameter("@Period", ""),
                 new SqlParameter("@frm", ""),
                 new SqlParameter("@tto", ""),
                 new SqlParameter("@dsgnn", ""),
                 new SqlParameter("@Documents", ""),//ObjBO.Documents
                 new SqlParameter("@orgId", ddlName.SelectedValue),
                 new SqlParameter("@branchId", ddlBranchName.SelectedValue),
                 new SqlParameter("@deptId", ""),
                 new SqlParameter("@Returnval",SqlDbType.Int),
            };
               string emp=DAL.ExecuteStoredProcedureString("Sp_AddEmployeeDetails", paramEmp);//Insert into Employee Details
               string getMaxEmpId = db.getData("select max(EmployeeId) from EmployeeDetails");
                //insert into orgEmp
                //insert into appuser
                //insert into userLoginMaster
                string getMaxAppUserId=db.getData("Select max(sAppUserId) from appUser");
                string mobNo = (int.Parse("2022000000") + int.Parse(getMaxAppUserId)).ToString();
                string _mobNo = CryptoHelper.Encrypt(mobNo);
                string emailId=mobNo+".technician@howzu.co.in";
                string _email=CryptoHelper.Encrypt(emailId);


                SqlParameter[] param = new SqlParameter[]
                {
                      new SqlParameter("@sFullName",txtTechnicianName.Text),
                      new SqlParameter("@sMobile",_mobNo),
                      new SqlParameter("@sEmailId",_email),
                      new SqlParameter("@sGender",""),
                      new SqlParameter("@sBirthDate",""),
                      new SqlParameter("@sAddress",""),
                      new SqlParameter("@sRole","Technician"),
                      new SqlParameter("@sCountry","India"),
                      new SqlParameter("@sState",""),
                      new SqlParameter("@sCity",""),
                      new SqlParameter("@sPincode",""),
                      new SqlParameter("@EmployeeId",getMaxEmpId),
                      new SqlParameter("@AadharCard",""),
                      new SqlParameter("@Returnval",SqlDbType.Int)
                };
                 Result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddEmployee", param);//Insert into App User
                if (Result >= 1)
                {
                    SqlParameter[] paramOrgEmp = new SqlParameter[]
                {
                      new SqlParameter("@EmpId",Result),
                      new SqlParameter("@OrgId",ddlName.SelectedValue),
                      new SqlParameter("@BranchId",ddlBranchName.SelectedValue),
                      new SqlParameter("@CreatedBy",ddlName.SelectedValue),
                      new SqlParameter("@Returnval",SqlDbType.Int)
                };
                    int ResultVal = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOrgEmployee", paramOrgEmp);// Insert into Organization Employee Details

                    //insert into new table UserOrgAsign
                    SqlParameter[] paramEmpOrg = new SqlParameter[]
                    {
                      new SqlParameter("@branchId",ddlBranchName.SelectedValue),
                      new SqlParameter("@orgId",ddlName.SelectedValue),
                      new SqlParameter("@userId",Result),
                      new SqlParameter("@createdBy",ddlName.SelectedValue),
                      new SqlParameter("@createdDate",System.DateTime.Now),
                      new SqlParameter("@Returnval",SqlDbType.Int)
                    };
                    int ResultValEmpOrg = DAL.ExecuteStoredProcedureRetnInt("SP_AddUserOrgAssign", paramEmpOrg);
              
                    if (ResultVal == 1)
                    {
                        SqlParameter[] param2 = new SqlParameter[]
                   {
                      new SqlParameter("@UserId",Result),
                      new SqlParameter("@Mobile",_mobNo),
                      new SqlParameter("@EmailId",_email),
                      new SqlParameter("@Role","Technician"),
                      new SqlParameter("@Password",_pwd),
                      new SqlParameter("@UserName",""),
                       new SqlParameter("@orgId",ddlName.SelectedValue),
                      new SqlParameter("@Returnval",SqlDbType.Int)

                     };
                        int ResultVal1 = DAL.ExecuteStoredProcedureRetnInt("Sp_AddUserLoginCredentialsEnterprise", param2); //Insert into User Login Master Details

                    }
                }
                ClsEmailTemplates emailTemp = new ClsEmailTemplates();
                string getMailId = WebConfigurationManager.AppSettings["technician_loginDtls_to_emailId"];
                string mailSent = emailTemp.sendmailForTechnicianCredential(getMailId, password, txtTechnicianName.Text, mobNo, ddlName.SelectedItem.Text,ddlBranchName.SelectedItem.Text,txthealthcampName.Text,txtownerName.Text,ddlLabName.SelectedItem.Text);

            }
           
            if (drpTechType.Text == "Howzu")
                userId = Result;
            else
                userId =int.Parse(ddlEmpName.SelectedValue);

            SqlParameter[] param1 = new SqlParameter[]
                         {
                             new SqlParameter("@userId",userId),
                             
                             new SqlParameter("@returnval",SqlDbType.Int)
                          };
            int Resultval = DAL.ExecuteStoredProcedureRetnInt("SP_updateRole", param1); //organization user
            SqlParameter[] paramHealthCamp = new SqlParameter[]
                         {
                             new SqlParameter("@healthCampName",txthealthcampName.Text),
                             new SqlParameter("@ownerName",txtownerName.Text),
                             new SqlParameter("@otherDetails",txtotherDetails.Text),
                             new SqlParameter("@orgId",ddlName.SelectedValue),
                             new SqlParameter("@userId",userId),
                             new SqlParameter("@labId",ddlLabName.SelectedValue),
                             new SqlParameter("@branchId",ddlBranchName.SelectedValue),
                             new SqlParameter("@branchName",ddlBranchName.SelectedItem.Text),
                             new SqlParameter("@status","1"),
                             new SqlParameter("@returnval",SqlDbType.Int)
                          };
            int ResultvalHealth = DAL.ExecuteStoredProcedureRetnInt("SP_addHealthCampMaster", paramHealthCamp);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Healthcamp created successfully');location.href='ViewHealthCampDetails.aspx';", true);
            // litErrorMessage.Text = ApplicationLogic.SuccessMessage("● Saved Suceessfully.");
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
    protected void drpTechType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlEmpName.Visible = true;
        if (drpTechType.Text == "Howzu")
        {
            txtTechnicianName.Visible = true;
            ddlEmpName.Visible = false;
        }
        else
            txtTechnicianName.Visible = false;
        
    }


    private string CreateRandomPassword()
    {
        // Create a string of characters, numbers, special characters that allowed in the password  
        string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
        Random random = new Random();

        // Select one random character at a time from the string  
        // and create an array of chars  
        char[] chars = new char[6];
        for (int i = 0; i < 6; i++)
        {
            chars[i] = validChars[random.Next(0, validChars.Length)];
        }
        return new string(chars);
    }
}
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using Validation;
using BitsBizLogic;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_AddOrgBranch : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    ClsEmailTemplates emailTemp = new ClsEmailTemplates();
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
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string Msg = "";
        if (ddlName.SelectedIndex <= 0)
        {
            Msg += "● Please Select Organization<br>";
        }
        if (Ival.IsTextBoxEmpty(txtHRName.Text))
        {
            Msg += "● Please Enter Valid HR Name";
        }
        if (Ival.IsTextBoxEmpty(txtBranchName.Text))
        {
            Msg += "● Please Enter Valid Branch Name";
        }
        if (Msg.Length > 0)
        {
            litErrorMessage.Text = ApplicationLogic.ErrorWarning(Msg);
        }
        else
        {
            ///Submit Data To Database
            string _password = CryptoHelper.Encrypt(txtPassword.Text);
            string _EmailId = txtEmail.Text != "" ? CryptoHelper.Encrypt(txtEmail.Text) : "";
            string _Mobile = txtmobilenumber.Text != "" ? CryptoHelper.Encrypt(txtmobilenumber.Text) : "";

            SqlParameter[] param = new SqlParameter[]
                         {
                             new SqlParameter("@Org_Id",ddlName.SelectedValue),
                             new SqlParameter("@BranchName",txtBranchName.Text),
                             new SqlParameter("@BranchAddress",txtAdress.Text),
                             new SqlParameter("@Contact",_Mobile),
                             new SqlParameter("@Email",_EmailId),
                             new SqlParameter("@BranchDetails",""),
                             new SqlParameter("@returnval",SqlDbType.Int)
                          };
            int Result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOrgnizationBranch", param);
            // Display Message
            if (Result >= 1)
            {
                SqlParameter[] param1 = new SqlParameter[]
                         {
                             new SqlParameter("@Name",txtHRName.Text),
                             new SqlParameter("@Org_Id",ddlName.SelectedValue),
                             new SqlParameter("@Branch_ID",Result),
                             new SqlParameter("@Contact",_Mobile),
                             new SqlParameter("@Email",_EmailId),
                             new SqlParameter("@Role","Enterprise"),
                             new SqlParameter("@Password",""),
                             new SqlParameter("@returnval",SqlDbType.Int)
                          };
                int Resultval = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOrgnizationUser", param1);
               
                    SqlParameter[] paramlab = new SqlParameter[]
                                             {
                                                new SqlParameter("@UserId",Resultval),
                                                new SqlParameter("@Mobile",_Mobile),
                                                new SqlParameter("@EmailId",_EmailId),
                                                new SqlParameter("@Role","Enterprise"),
                                                new SqlParameter("@Password",_password),
                                                new SqlParameter("@UserName",""),
                                                // new SqlParameter("@rollMasterId",""),
                                                new SqlParameter("@Returnval",SqlDbType.Int)
                                             };
                    int ResultVal1 = DAL.ExecuteStoredProcedureRetnInt("Sp_AddUserLoginCredentials", paramlab);
                   
                    string mailSent = emailTemp.sendmail(txtEmail.Text, txtPassword.Text, txtHRName.Text, txtmobilenumber.Text);
                    //litErrorMessage.Text = ApplicationLogic.SuccessMessage("● Saved Suceessfully.");
   //code for insert into addbrah table of enterprise
                    SqlParameter[] paramBranch = new SqlParameter[]
                         {
                             new SqlParameter("@Org_Id",ddlName.SelectedValue),
                             new SqlParameter("@branchName",txtBranchName.Text),
                             new SqlParameter("@address",txtAdress.Text),
                             new SqlParameter("@mobileNo",_Mobile),
                             new SqlParameter("@emailId",_EmailId),
                            // new SqlParameter("@BranchDetails",""),
                             new SqlParameter("@returnval",SqlDbType.Int)
                          };
                    int ResultVal2 = DAL.ExecuteStoredProcedureRetnInt("Sp_AddBranch", paramBranch);

                    //Insert into Branch sub branchAssign Table
                    SqlParameter[] paramSubBranch = new SqlParameter[]
                         {
                             new SqlParameter("@parentBranchId","0"),
                             new SqlParameter("@branchId",Result),
                             new SqlParameter("@orgId",ddlName.SelectedValue),
                            
                             new SqlParameter("@status","Active"),
                              new SqlParameter("@createdBy",System.DateTime.Now),
                             new SqlParameter("@createdDate",System.DateTime.Now),
                             new SqlParameter("@editedBy",""),
                             new SqlParameter("@editedDate",System.DateTime.Now),
                             new SqlParameter("@deletedBy",""),
                             new SqlParameter("@deletedDate",System.DateTime.Now),
                            
                             new SqlParameter("@returnval",SqlDbType.Int)
                          };
                    int ResultSubBranch = DAL.ExecuteStoredProcedureRetnInt("Sp_subBranchDetails", paramSubBranch);
				  newWhatsapp wa = new newWhatsapp();
                    //wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
                    wa.sendWhatsappMsg_superadmin("+91" + txtmobilenumber.Text, "Branch Welcome", txtHRName.Text + ',' + ddlName.SelectedItem.Text + ',' + txtBranchName.Text+','+txtEmail.Text+','+txtPassword.Text);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Branch Added successfully');location.href='ViewOrgBranch.aspx';", true);
                          
               if (Result == -2)
                {
                    litErrorMessage.Text = ApplicationLogic.ErrorWarning("● User Already Exist!");
                }
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using Validation;
using DataAccessHandler;
using System.Data.SqlClient;

public partial class SuperAdmin_ManageUsers : System.Web.UI.Page
{
    ClsManageSuperUsers objLabUser = new ClsManageSuperUsers();
    DBClass db = new DBClass();
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!IsPostBack)
            {
                loadSuperAdminUsersList();
                db.bindDrp("select distinct rollMasterId, rollName from rollMaster order by rollName asc", drproleMaster, "rollName", "rollMasterId");
                drproleMaster.Items.Insert(0, new ListItem("-Select Role-"));
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }
    protected void loadSuperAdminUsersList()
    {
        //DataSet ds = objLabUser.getSuperAdminUsers(Request.Cookies["AdminId"].Value.ToString());
        SqlParameter[] paramEmgAgeRatio = new SqlParameter[]
         {
                           

          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetSuperAdminUsers", paramEmgAgeRatio);
       // DataSet dsRoles = objLabUser.getSuperAdminUserRoles(Request.Cookies["AdminId"].Value.ToString());

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tabLabUsersList = "";

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string _emailId = row["sEmailId"].ToString() != "" ? CryptoHelper.Decrypt(row["sEmailId"].ToString()) : "";
                    string _mobile = row["sContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sContact"].ToString()) : "";

                    //Load lab users list
                    tabLabUsersList += "<tr>" +
                                        "<td scope='col' id='name" + row["sSuperAdminUserId"].ToString() + "' clientidmode='static'>" + row["sFullName"].ToString() + "</td>" +
                                        "<td scope='col' id='emailId" + row["sSuperAdminUserId"].ToString() + "' clientidmode='static'>" + _emailId + "</td>" +
                                        "<td scope='col' id='contact" + row["sSuperAdminUserId"].ToString() + "' clientidmode='static'>" + _mobile + "</td>" +
                                        "<td scope='col' id='description" + row["sSuperAdminUserId"].ToString() + "' clientidmode='static'>" + row["sRole"].ToString() + "</td>" +
                                        "<td scope='col'><a href='' id='" + row["sSuperAdminUserId"].ToString() + "' data-toggle='modal' data-target='#modalDeleteUserConfirm'><i class='fa fa-trash fa-2x'></i></a></td>" +
                                     "</tr>";
                }
                tbodyLabUsersList.InnerHtml = tabLabUsersList;
            }
            else
            {
                tbodyLabUsersList.InnerHtml = "<tr><td>No records found</td></tr>";
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string Msg = "";
        if (txtFullName.Text != "")
        {
            if (txtEmailId.Text != "")
            {
                if (txtContact.Text != "")
                {
                    if (txtUserName.Text != "")
                    {
                        if (txtPassword.Text != "")
                        {
                            string labId = Request.Cookies["AdminId"].Value.ToString();
                            string labCode = "Null";
                            string fullname = txtFullName.Text;
                            string emailId = txtEmailId.Text.ToLower();
                            string contact = txtContact.Text;
                            string description = txtDescription.Text;
                            string userName = txtUserName.Text;
                            string password = txtPassword.Text;
                            string role = db.getData("select rollName from rollMaster where rollMasterId='" + drproleMaster.Text + "'");
                            string roleId = db.getData("select rollMasterId from rollMaster where rollMasterId='" + drproleMaster.Text + "'");
                            db.insert(@"INSERT INTO SuperAdminUser (sSuperAdminId,sSuperAdminCode,sFullName,sEmailId,sContact,sRole,sDescription,sUserName,sPassword)
                        VALUES ('" + labId + "','" + labCode + "','" + fullname + "','" + CryptoHelper.Encrypt(emailId) + "','" + CryptoHelper.Encrypt(contact) + "','" + role + "','" + description + "','" + userName + "','" + password + "') ");
                            string userId = db.getData("select max(sSuperAdminUserId) from SuperAdminUser");
                            db.insert(@" insert into UserLoginMaster (UserId,[Mobile],[EmailId],[Password],[Role],UserName,rollMasterId)  
                         values('" + userId + "','" + CryptoHelper.Encrypt(contact) + "','" + CryptoHelper.Encrypt(emailId) + "','" + CryptoHelper.Encrypt(password) + "','" + role + "','" + CryptoHelper.Encrypt(userName) + "','" + roleId + "')  ");


                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('User Added successfully');location.href='ManageUsers.aspx';", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Enter User Password');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Enter User Name');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Enter Contact No');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Enter Email ID');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Enter User Full Name');", true);
        }

       
    }
    protected void btnDeleteUserYes_Click(object sender, EventArgs e)
    {
        string labUserId = hiddenDeleteUser.Value;
        db.insert("delete from SuperAdminUser where sSuperAdminUserId='" + labUserId + "'");
        db.insert("delete from UserLoginMaster where UserId='" + labUserId + "'");
       // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('User Deleted successfully');location.href='ManageUsers.aspx';", true);
        //if (objLabUser.deleteSuperAdminUser(labUserId) == 1)
        //{
        //    Label lblMasterStatus = (Label)Master.FindControl("lblmsgText");
        //    lblMasterStatus.Text = "User Delete Sucessfully";
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "Yopopupalert();", true);
        //    // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
        //}
        //else if (objLabUser.deleteSuperAdminUser(labUserId) == 0)
        //{
        //    Label lblMasterStatus = (Label)Master.FindControl("lblmsgText");
        //    lblMasterStatus.Text = "Error Occured while Deleting User";
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "Yopopupalert();", true);
        //    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
        //}
        Response.Redirect("ManageUsers.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string Msg = "";
        if (Ival.IsTextBoxEmpty(txtFullNameEdit.Text))
        {
            Msg += "● Please Enter Valid Name";
        }
        if (!Ival.IsTextBoxEmpty(txtEmailIdEdit.Text))
        {
            if (!Ival.IsValidEmailAddress(txtEmailIdEdit.Text))
            {
                Msg += "● Please Enter Valid Email Id";
            }
        }
        if (Ival.IsInteger(txtContactEdit.Text))
        {
            if (!Ival.MobileValidation(txtContactEdit.Text))
            {
                Msg += "● Please Enter Valid Mobile Number";
            }
        }
        else
        {
            Msg += "● Please Enter Valid Mobile Number";
        }
        if (Msg.Length > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');location.reload();", true);
        }
        else
        {
            string labUserId = hiddenEditUser.Value;
            string fullname = txtFullNameEdit.Text;
            string emailId = txtEmailIdEdit.Text.ToLower();
            string contact = txtContactEdit.Text;
            string description = txtDescriptionEdit.Text;
            string role = "Admin";

            if (objLabUser.updateSuperAdminUserRole(labUserId, fullname, emailId, contact, description, role) == 1)
            {
                Label lblMasterStatus = (Label)Master.FindControl("lblmsgText");
                lblMasterStatus.Text = "User Update Successfully";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "Yopopupalert();", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
            }
            else if (objLabUser.updateSuperAdminUserRole(labUserId, fullname, emailId, contact, description, role) == 0)
            {
                Label lblMasterStatus = (Label)Master.FindControl("lblmsgText");
                lblMasterStatus.Text = "Error Occured While Updating User";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "Yopopupalert();", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
            }
            Response.Redirect("ManageUsers.aspx");
        }
    }
}
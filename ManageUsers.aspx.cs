using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using Validation;

public partial class ManageUsers : System.Web.UI.Page
{
    ClsManageUsers objLabUser = new ClsManageUsers();
    InputValidation Ival = new InputValidation();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
                    loadLabUsersList();
                }
            }
            else
            {
                Response.Redirect("LabLogin.aspx");
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void loadLabUsersList()
    {
        try
        {
            DataSet ds = objLabUser.getLabUsers(Request.Cookies["labId"].Value.ToString());
            DataSet dsRoles = objLabUser.getLabUserRoles(Request.Cookies["labId"].Value.ToString());

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabLabUsersList = "";

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string fullName = "'" + row["sFullName"].ToString() + "'";
                        string emailId = row["sEmailId"].ToString() != "" ? CryptoHelper.Decrypt(row["sEmailId"].ToString()) : "";
                        string contact = row["sContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sContact"].ToString()) : "";
                        string description = row["sDescription"].ToString();

                        
                        //Load lab users list
                        tabLabUsersList += "<li class='table-row'>" +
                                           "<div class='col col-1 text-center' id='name" + row["sLabUserId"].ToString() + "' clientidmode='static'>" + row["sFullName"].ToString() + "</div>" +
                                           "<div class='col col-2 text-center' id='emailId" + row["sLabUserId"].ToString() + "' clientidmode='static'>" + emailId + "</div>" +
                                           "<div class='col col-3 text-center' id='contact" + row["sLabUserId"].ToString() + "' clientidmode='static'>" + contact + "</div>" +
                                           "<div class='col col-4 text-center' id='role" + row["sLabUserId"].ToString() + "' clientidmode='static'>" + row["sRole"].ToString() + "</div>" +
                                           "<div class='col col-5 text-center' id='description" + row["sLabUserId"].ToString() + "' clientidmode='static'>" + row["sDescription"].ToString() + "</div>" +
                                           "<div class='col col-6 text-center' ><a href='' class='HideEditbtn' id='" + row["sLabUserId"].ToString() + "' data-toggle='modal' data-target='#modalEditUser'><i class='fa fa-edit fa-color' aria-hidden='true'></i></a></div>" +
                                           "<div class='col col-7 text-center'><a  class='HideEditbtn' href='editroles.aspx?roleuserid=" + row["sLabUserId"].ToString() + "' id='" + row["sLabUserId"].ToString() + "' ><i class='fa fa-edit fa-color' aria-hidden='true'></i></a></div>" +
                                           "<div class='col col-8 text-center' ><a class='HideEditbtn' href='' id='" + row["sLabUserId"].ToString() + "' data-toggle='modal' data-target='#modalDeleteUserConfirm'><i class='fa fa-trash fa-color' aria-hidden='true'></i></a></div>" +
                                        "</li>";
                    }
                    tbodyLabUsersList.Text = tabLabUsersList;
                }
                else
                {
                    tbodyLabUsersList.Text = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string Msg = "";
            if (!Ival.IsCharOnly(txtFullName.Text))
            {
                Msg += "● Please Enter Valid Name";
            }
            if (!Ival.IsTextBoxEmpty(txtEmailId.Text))
            {
                if (!Ival.IsValidEmailAddress(txtEmailId.Text))
                {
                    Msg += "● Please Enter Valid Email Id";
                }
            }
            if (!Ival.IsTextBoxEmpty(txtContact.Text))
            {
                if (Ival.IsInteger(txtContact.Text))
                {
                    if (!Ival.MobileValidation(txtContact.Text))
                    {
                        Msg += "● Please Enter Valid Mobile Number";
                    }
                }
            }
            if (Ival.IsTextBoxEmpty(txtUserName.Text))
            {
                Msg += "● Please Enter Valid Username";
            }
            if (Ival.IsTextBoxEmpty(txtPassword.Text))
            {
                Msg += "● Please Enter Valid Username";
            }
            if (Msg.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');location.reload();", true);
            }
            else
            {
                string labId = Request.Cookies["labId"].Value.ToString();
                string labCode = Request.Cookies["labCode"].Value.ToString();
                string fullname = txtFullName.Text;
                string emailId = txtEmailId.Text;
                string contact = txtContact.Text;
                string description = txtDescription.Text;
                string userName = txtUserName.Text;
                string password = txtPassword.Text;
                string role = hiddenRoles.Value.TrimStart(',').TrimEnd(',');

                if (objLabUser.addLabUser(labId, labCode, fullname, emailId, contact, description, userName, password, role) >= 1)
                {
                    lblMessage.Text = "Lab User Added Successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
                }
                else if (objLabUser.addLabUser(labId, labCode, fullname, emailId, contact, description, userName, password, role) == 0)
                {
                    lblMessage.Text = "Error occurred while Adding Labuser";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');location.reload();", true);
                }
                else if (objLabUser.addLabUser(labId, labCode, fullname, emailId, contact, description, userName, password, role) == -3)
                {
                    lblMessage.Text = "This username is already registered, please use another username";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('This username is already registered, please use another username');location.reload();", true);
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnDeleteUserYes_Click(object sender, EventArgs e)
    {
        try
        {
            string labUserId = hiddenDeleteUser.Value;

            if (objLabUser.deleteLabUser(labUserId) == 1)
            {
                lblMessage.Text = "Lab User Deleted Successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
            }
            else if (objLabUser.deleteLabUser(labUserId) == 0)
            {
                lblMessage.Text = "Error occurred while Deleting Labuse";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string Msg = "";
            if (!Ival.IsCharOnly(txtFullNameEdit.Text))
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
            if (!Ival.IsTextBoxEmpty(txtContactEdit.Text))
            {
                if (Ival.IsInteger(txtContactEdit.Text))
                {
                    if (!Ival.MobileValidation(txtContactEdit.Text))
                    {
                        Msg += "● Please Enter Valid Mobile Number";
                    }
                }
            }
            if (Msg.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');location.reload();", true);
            }
            else
            {
                string labUserId = hiddenEditUser.Value;
                string fullname = txtFullNameEdit.Text;
                string emailId = txtEmailIdEdit.Text;
                string contact = txtContactEdit.Text;
                string description = txtDescriptionEdit.Text;
                string role = hiddenRolesEdit.Value.TrimStart(',').TrimEnd(',');

                if (objLabUser.updateLabUserRole(labUserId, fullname, emailId, contact, description, role) == 1)
                {
                    lblMessage.Text = "Lab User Updated Successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
                }
                else if (objLabUser.updateLabUserRole(labUserId, fullname, emailId, contact, description, role) == 0)
                {
                    lblMessage.Text = "Error Occured while updating Lab user";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                   // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
}
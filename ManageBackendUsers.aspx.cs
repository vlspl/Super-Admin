using System;
using System.Web.UI;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using Validation;

public partial class SuperAdmin_ManageBackendUsers : System.Web.UI.Page
{
    ClsManageBackendUsers objLabUser = new ClsManageBackendUsers();
    InputValidation Ival = new InputValidation();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["AdminId"].Value != null)
            {
                if (!IsPostBack)
                {
                    loadBackendUsersList();
                }
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void loadBackendUsersList()
    {
        DataSet ds = objLabUser.getBackendUsers(Request.Cookies["AdminId"].Value.ToString());

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
                                       "<td scope='col' id='description" + row["sSuperAdminUserId"].ToString() + "' clientidmode='static'>" + row["sDescription"].ToString() + "</td>" +
                                       "<td scope='col'><a href='' id='" + row["sSuperAdminUserId"].ToString() + "' data-toggle='modal' data-target='#modalEditUser'><i class='fa fa-pencil-square-o margin-0' aria-hidden='true'></i></a></td>" +
                                       "<td scope='col'><a href='' id='" + row["sSuperAdminUserId"].ToString() + "' data-toggle='modal' data-target='#modalDeleteUserConfirm'><i class='fa fa-trash-o margin-0' aria-hidden='true'></i></a></td>" +
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
        if (Ival.IsTextBoxEmpty(txtFullName.Text))
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
        if (Ival.IsInteger(txtContact.Text))
        {
            if (!Ival.MobileValidation(txtContact.Text))
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
            string labId = Request.Cookies["AdminId"].Value.ToString();
            string labCode = "Null";
            string fullname = txtFullName.Text;
            string emailId = txtEmailId.Text;
            string contact = txtContact.Text;
            string description = txtDescription.Text;
            string userName = "";
            string password = CreateRandomPassword();
            string role = "Backend";

            if (objLabUser.addBackendUser(labId, labCode, fullname, emailId, contact, description, userName, password, role) >= 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
            }
            else if (objLabUser.addBackendUser(labId, labCode, fullname, emailId, contact, description, userName, password, role) == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');location.reload();", true);
            }
            else if (objLabUser.addBackendUser(labId, labCode, fullname, emailId, contact, description, userName, password, role) == -3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('This username is already registered, please use another username');location.reload();", true);
            }

            Response.Redirect("ManageBackendUsers.aspx");
        }
    }
    protected void btnDeleteUserYes_Click(object sender, EventArgs e)
    {
        string labUserId = hiddenDeleteUser.Value;

        if (objLabUser.deleteBackendUser(labUserId) == 1)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
        }
        else if (objLabUser.deleteBackendUser(labUserId) == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
        }
        Response.Redirect("ManageBackendUsers.aspx");
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
            string emailId = txtEmailIdEdit.Text;
            string contact = txtContactEdit.Text;
            string description = txtDescriptionEdit.Text;
            string role = "Backend";

            if (objLabUser.updateBackendUser(labUserId, fullname, emailId, contact, description, role) == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
            }
            else if (objLabUser.updateBackendUser(labUserId, fullname, emailId, contact, description, role) == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
            }
            Response.Redirect("ManageBackendUsers.aspx");
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
}

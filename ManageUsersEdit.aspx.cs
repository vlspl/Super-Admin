using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class ManageUsersEdit : System.Web.UI.Page
{
    ClsManageUsersEdit objLabUser = new ClsManageUsersEdit();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
                    loadLabUserDetails();
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
    protected void loadLabUserDetails()
    {
        try
        {
            int labUserId = Convert.ToInt32(Request.QueryString["id"].ToString());
            DataSet dsUser = objLabUser.getLabUser(Request.Cookies["labId"].Value.ToString(), labUserId.ToString());
            DataSet dsRoles = objLabUser.getLabUserRoles(Request.Cookies["labId"].Value.ToString());
            hiddenRoles.Value = "";

            if (dsUser != null)
            {
                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsUser.Tables[0].Rows)
                    {
                        //Load lab user
                        txtFullName.Text = row["sFullName"].ToString();
                        txtEmailId.Text = row["sEmailId"].ToString();
                        txtContact.Text = row["sContact"].ToString();
                        txtDescription.Text = row["sDescription"].ToString();
                        hiddenRoles.Value = row["sRole"].ToString();
                    }
                }
                else
                {
                }
            }

            if (dsRoles != null)
            {
                if (dsRoles.Tables[0].Rows.Count > 0)
                {
                    string roles = "";
                    foreach (DataRow row in dsRoles.Tables[0].Rows)
                    {
                        string role = row["sRole"].ToString();
                        string check = (hiddenRoles.Value.ToString().Contains(role)) ? "checked='checked'" : "";
                        //Load lab user roles
                        roles += "<input type='checkbox' value='" + role + "' " + check + ">" + role + "<br>";
                    }

                    divRoles.InnerHtml = roles;
                }
                else
                {
                }
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
            int labUserId = Convert.ToInt32(Request.QueryString["id"].ToString());
            string fullname = txtFullName.Text;
            string emailId = txtEmailId.Text;
            string contact = txtContact.Text;
            string description = txtDescription.Text;
            string role = hiddenRoles.Value.TrimStart(',').TrimEnd(',');

            if (objLabUser.updateLabUserRole(labUserId.ToString(), fullname, emailId, contact, description, role) == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.href='ManageUsers.aspx';", true);
            }
            else if (objLabUser.updateLabUserRole(labUserId.ToString(), fullname, emailId, contact, description, role) == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
}
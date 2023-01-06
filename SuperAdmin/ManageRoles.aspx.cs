using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SuperAdmin_ManageRoles : System.Web.UI.Page
{
    ClsManageRoles objLabRoles = new ClsManageRoles();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!IsPostBack)
            {
                loadLabRoles();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }
    protected void loadLabRoles()
    {
        DataSet dsRoles = objLabRoles.getRoles(Request.Cookies["AdminId"].Value.ToString());

        if (dsRoles != null)
        {
            if (dsRoles.Tables[0].Rows.Count > 0)
            {
                string tabRolesList = "";

                foreach (DataRow row in dsRoles.Tables[0].Rows)
                {
                    //Load lab roles list
                    tabRolesList += "<tr>" +
                                       "<td scope='col'>" + row["sRollName"].ToString() + "</td>" +
                                       "<td scope='col'><a href='' id='" + row["sRollsID"].ToString() + "' data-toggle='modal' data-target='#modalDeleteRoleConfirm'><i class='fa fa-trash-o margin-0' aria-hidden='true'></i></a></td>" +
                                    "</tr>";
                }

                tbodyRolesList.InnerHtml = tabRolesList;
            }
            else
            {
                tbodyRolesList.InnerHtml = "<tr><td>No records found</td></tr>";
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string labId = Request.Cookies["AdminId"].Value.ToString();
        string role = txtRole.Text.ToString();

        if (objLabRoles.addRole(labId, role) == 1)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
        }
        else if (objLabRoles.addRole(labId, role) == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
        }
        Response.Redirect("ManageRoles.aspx");
    }
    protected void btnDeleteRoleYes_Click(object sender, EventArgs e)
    {
        string labUserRoleId = hiddenDeleteRole.Value;

        if (objLabRoles.deleteRole(labUserRoleId) == 1)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
        }
        else if (objLabRoles.deleteRole(labUserRoleId) == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
        }
        Response.Redirect("ManageRoles.aspx");
    }
}
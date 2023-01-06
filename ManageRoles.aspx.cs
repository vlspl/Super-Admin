using System;
using System.Web.UI;
using System.Data;

public partial class ManageRoles : System.Web.UI.Page
{
    ClsManageRoles objLabRoles = new ClsManageRoles();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
                    loadLabRoles();
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
    protected void loadLabRoles()
    {
        try
        {
            DataSet dsRoles = objLabRoles.getRoles(Request.Cookies["labId"].Value.ToString());

            if (dsRoles != null)
            {
                if (dsRoles.Tables[0].Rows.Count > 0)
                {
                    string tabRolesList = "";

                    foreach (DataRow row in dsRoles.Tables[0].Rows)
                    {
                        //Load lab roles list
                        tabRolesList += "<tr>" +
                                           "<td scope='col'>" + row["sRole"].ToString() + "</td>" +
                                           "<td scope='col'><a href='' id='" + row["sLabUserRoleId"].ToString() + "' data-toggle='modal' data-target='#modalDeleteRoleConfirm'><i class='fa fa-trash-o margin-0' aria-hidden='true'></i></a></td>" +
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
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string labId = Request.Cookies["labId"].Value.ToString();
            string role = selRole.Value;

            if (objLabRoles.addRole(labId, role) == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
            }
            else if (objLabRoles.addRole(labId, role) == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnDeleteRoleYes_Click(object sender, EventArgs e)
    {
        try
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
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }

}
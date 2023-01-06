using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SuperAdmin_Dashboard : System.Web.UI.Page
{
    ClsAdminLogin objLogData = new ClsAdminLogin();
    string strs = null;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["AdminId"].Value != null)
            {
                if (!Page.IsPostBack)
                {
                    getGriddata();

                    //Label lblMasterStatus = (Label)Master.FindControl("lblmsgText");
                   // lblMasterStatus.Text = "Meaasage from content page";
                }
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        catch
        {
            Response.Redirect("AdminLogin.aspx");
        }
    } 
    protected void Labgrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton img = (ImageButton)e.Row.FindControl("img_user");
            if (e.Row.Cells[2].Text == "Active")
            {
                img.ImageUrl = "~/images/Active.png";
            }
            else
            {
                img.ImageUrl = "~/images/Deactive.jpg";
            }
        }
    } 
    protected void Labgrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = (Labgrid.DataKeys[Labgrid.SelectedRow.DataItemIndex].Value).ToString();
        string status = Labgrid.Rows[Labgrid.SelectedRow.DataItemIndex].Cells[2].Text;
        //Or I think you can do this.
        //String ID = GridView1.SelectedDataKey.Value.ToString();

        if (status == "Active")
        {
            status = "Inactive";
        }
        else
        {
            status = "Active";
        }
        objLogData.UpdateStatus(id, status);

        getGriddata();

    }
    public void getGriddata()
    {
        ds = objLogData.getDashData();
        if (ds.Tables[0].Rows.Count > 0)
        {
            Labgrid.DataSource = ds;
            Labgrid.DataBind();
        }
    }
}

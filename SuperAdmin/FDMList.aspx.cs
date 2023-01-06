using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data;

public partial class SuperAdmin_FDMList : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (true)//Request.Cookies["AdminId"].Value != null
        {
            if (!IsPostBack)
            {
                gridBind();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    void gridBind()
    {
        gridFDMList.DataSource = DAL.GetDataTable("Sp_userProfile_FDM_list");
        gridFDMList.DataBind();
    }
    protected void gridFDMList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridFDMList.PageIndex = e.NewPageIndex;
        gridBind();
    }
    protected void drpfdmType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string type = drpfdmType.Text;
        gridFDMList.DataSource = GetBranchDetails(type);
        gridFDMList.DataBind();
        
    }
    public DataTable GetBranchDetails(string type)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_FDM_list_typewise " + "'"+type+"'");
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
}
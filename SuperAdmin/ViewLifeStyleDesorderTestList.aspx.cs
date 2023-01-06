using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using System.Data.SqlClient;
using DataAccessHandler;

public partial class SuperAdmin_ViewLifeStyleDesorderTestList : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    static int LifeStyleId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!IsPostBack)
            {
                ddlName.DataSource = DAL.GetDataTable("Sp_GetLifeStyleDetailForDDL");
                ddlName.DataBind();
                ListItem lit = new ListItem();
                lit.Text = "Select";
                lit.Value = "0";
                ddlName.Items.Insert(0, lit);
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Id"] = Convert.ToInt32(ddlName.SelectedValue);
        SuperAdmin_ViewLifeStyleDesorderTestList.LifeStyleId = Convert.ToInt32(Session["Id"]);
        grdviewOrgnization.DataSource = GetPartnerDetails(Session["Id"].ToString());
        grdviewOrgnization.DataBind();
       
    }
    protected void grdviewOrgnization_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int deleteID = Convert.ToInt32(grdviewOrgnization.DataKeys[e.RowIndex].Value.ToString());
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@Id",deleteID)
            };

            DAL.ExecuteStoredProcedure("Sp_DeleteTestFromLifeStyle ", param);
            grdviewOrgnization.DataSource = GetPartnerDetails(SuperAdmin_ViewLifeStyleDesorderTestList.LifeStyleId.ToString());
            grdviewOrgnization.DataBind();
        }
        catch (Exception ex)
        {
            litErrorMessage.Text = "Somthing went Wrong. Please Try Again.!!!";
        }
    }
   
   
    public DataTable GetPartnerDetails(string ID)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetLifestyleDisorderTestlist " + ID);
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrossPlatformAESEncryption.Helper;
using DataAccessHandler;
using System.Data;

public partial class SuperAdmin_OrgnizationUserList : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["AdminId"].Value != null)
            {
                if (!Page.IsPostBack)
                {
                    grdviewSignUpDaily.DataSource = GetDailySignUpDetails();
                    grdviewSignUpDaily.DataBind();

                    lblOrgname.InnerText = Request.QueryString["Name"].ToString() +" Uesr List";
                }
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        catch
        {
            Response.Redirect("../AdminLogin.aspx");
        }
    }

    public DataTable GetDailySignUpDetails()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetEmployeeListByOrgid " + Request.QueryString["Id"].ToString());
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("EmailId", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    string Mobile = (dt.Rows[i]["sMobile"].ToString() != "") ? CryptoHelper.Decrypt(dt.Rows[i]["sMobile"].ToString()) : "";
                    string EmailId = (dt.Rows[i]["sEmailId"].ToString() != "") ? CryptoHelper.Decrypt(dt.Rows[i]["sEmailId"].ToString()) : "";
                    row["Mobile"] = Mobile;
                    row["EmailId"] = EmailId;
                }
            }
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    protected void grdviewSignUpDaily_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdviewSignUpDaily.PageIndex = e.NewPageIndex;
        grdviewSignUpDaily.DataSource = GetDailySignUpDetails();
        grdviewSignUpDaily.DataBind();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

public partial class SuperAdmin_accessControlMaster : System.Web.UI.MasterPage
{
    DBClass db = new DBClass();
    DataAccessLayer DAL = new DataAccessLayer();
    public DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {

            try
            {
                lbluser.Text = Request.Cookies["AdminUserName"].Value.ToString();

                dt = db.Displaygrid(@"SELECT        screenMaster.screenMasterId,screenMaster.headingClass,screenMaster.submenuClass, screenMaster.screenName, screenMaster.displayName, screenMaster.screenUrl, screenMaster.parentScreenId, screenMaster.menuIcon, screenMaster.createDate, screenMaster.isEnbled
    FROM            roleToScreen INNER JOIN
                             rollMaster ON roleToScreen.rollMasterId = rollMaster.rollMasterId INNER JOIN
                             screenMaster ON roleToScreen.screenMasterId = screenMaster.screenMasterId INNER JOIN
                             UserLoginMaster ON rollMaster.rollMasterId = UserLoginMaster.rollMasterId
    where UserLoginMaster.UserName='" + Session["username"].ToString() + "' and UserLoginMaster.Password='" + Session["password"].ToString() + "'");
            }
            catch (Exception ex)
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
            
       
    }


    protected void btnsearchglobal_Click(object sender, EventArgs e)
    {
        //string searchkeyword = txtsearchglobal.Text.ToString();
       // Response.Redirect("GlobalSearch.aspx?keyword='" + searchkeyword + "'");
    }


    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("AdminLogin.aspx");
    }
}



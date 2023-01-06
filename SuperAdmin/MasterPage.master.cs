using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data;

public partial class SuperAdmin_MasterPage : System.Web.UI.MasterPage
{
    DBClass db = new DBClass();
    DataAccessLayer DAL = new DataAccessLayer();
    public DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value == null)
        {
            if (!IsPostBack)
            {
                Response.Redirect("AdminLogin.aspx");
                ScriptManager.RegisterStartupScript(this, GetType(), "timeup", "alert('Sesstion Timeout');", true);
            }
        }
        else
        {
          //  lbluser.Text = Request.Cookies["AdminUserName"].Value.ToString();

            dt = db.Displaygrid(@"SELECT        screenMaster.screenMasterId,screenMaster.headingClass,screenMaster.submenuClass, screenMaster.screenName, screenMaster.displayName, screenMaster.screenUrl, screenMaster.parentScreenId, screenMaster.menuIcon, screenMaster.createDate, screenMaster.isEnbled
FROM            roleToScreen INNER JOIN
                         rollMaster ON roleToScreen.rollMasterId = rollMaster.rollMasterId INNER JOIN
                         screenMaster ON roleToScreen.screenMasterId = screenMaster.screenMasterId INNER JOIN
                         UserLoginMaster ON rollMaster.rollMasterId = UserLoginMaster.rollMasterId
where UserLoginMaster.UserName='" + Session["username"].ToString() + "' and UserLoginMaster.Password='" + Session["password"].ToString() + "'");

        }
    }
}

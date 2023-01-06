using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using System.Configuration;
using Validation;

public partial class SuperAdmin_ScreenMaster : System.Web.UI.Page
{
    CLSRoleMaster roleMstr = new CLSRoleMaster();
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    DBClass db = new DBClass();
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!IsPostBack)
            {
                db.bindDrp("select distinct screenMasterId, displayName from screenMaster where parentScreenId=0  order by displayName asc", drpparentId, "displayName", "screenMasterId");
                drpparentId.Items.Insert(0, new ListItem("-Select Parent Screen-"));
                gridbind();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }
    void gridbind()
    {
        try
        {


            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("select screenMasterId,screenName,displayName,screenUrl,parentScreenId,menuIcon from screenMaster  order by 1 desc", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds, "screenMaster");
                con.Open();
                gvscreen.DataSource = ds;
                gvscreen.DataBind();
                con.Close();
            }
        }
        catch (Exception ex)
        {

            ex.Message.ToString();
        }
    }
   



    protected void btnaddRole_Click(object sender, EventArgs e)
    {
        //roleMstr.screenname = txtscreenname.Text.ToString();
        //roleMstr.displayname = txtdisplayName.Text.ToString();
        //roleMstr.screenurl = txtscreenurl.Text.ToString();
        //roleMstr.paraintid = drpparentId.Text.ToString();
        //roleMstr.menuicon = txtmenuicon.Text.ToString();
       
        //if (roleMstr.insertscreenEntry() == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Screen Added successfully');location.href='ScreenMaster.aspx';", true);
        //}
        //if (roleMstr.insertscreenEntry() == 1)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Error While Adding Screen');location.href='ScreenMaster.aspx';", true);
        //}
        db.insert("insert into screenMaster(screenName,displayName,screenUrl,parentScreenId,menuIcon) values('"+txtscreenname.Text+"','"+txtdisplayName.Text+"','"+txtscreenurl.Text+"','"+drpparentId.Text+"','"+txtmenuicon.Text+"')");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Screen Added successfully');location.href='ScreenMaster.aspx';", true);
        gridbind();
    }
    protected void gvscreen_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvscreen.PageIndex = e.NewPageIndex;
        gridbind();
    }
}
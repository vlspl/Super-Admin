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

public partial class SuperAdmin_RoleScreenMaster : System.Web.UI.Page
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
                db.bindDrp("select distinct rollMasterId, rollName from rollMaster order by rollName asc", drproleMaster, "rollName", "rollMasterId");
                drproleMaster.Items.Insert(0, new ListItem("-Select Role-"));
                
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
                SqlCommand cmd = new SqlCommand(@"SELECT       roleToScreen.rollToScreenId, rollMaster.rollName, screenMaster.screenName, roleToScreen.specialRole, roleToScreen.remark
FROM            screenMaster INNER JOIN
                         roleToScreen ON screenMaster.screenMasterId = roleToScreen.screenMasterId INNER JOIN
                         rollMaster ON roleToScreen.rollMasterId = rollMaster.rollMasterId
where rollMaster.rollMasterId='"+drproleMaster.Text+"'", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                con.Open();
                gvrolescreenMaster.DataSource = ds;
                gvrolescreenMaster.DataBind();
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
        try
        {
            if (drproleMaster.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Role');", true);
            }
            else
            {
                for (int i = 0; i <= CheckBoxList_screens.Items.Count - 1; i++)
                {
                    if (CheckBoxList_screens.Items[i].Selected)
                    {
                       
                       string screens = CheckBoxList_screens.Items[i].Text;
                       string screenId = db.getData("select screenMasterId from screenMaster where screenName='" + screens + "'").ToString();
                       db.insert("insert into roleToScreen(rollMasterId,screenMasterId,specialRole,remark) values('" + drproleMaster.Text + "','" + screenId + "','False','" + txtremark.Text + "')");
                    }
                    else
                    {
                       // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Screens');", true);
                    }
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Role Screen Master Details Added Successfully');location.href='RoleScreenMaster.aspx';", true);
            }
        }
        catch (Exception ex)
        {
          
        }
        //gridbind();
    }
    protected void drproleMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridbind();
    }

    protected void gvrolescreenMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvrolescreenMaster.PageIndex = e.NewPageIndex;
        gridbind();
    }
    protected void gvrolescreenMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       SqlDataAdapter da;
        SqlConnection con;
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        cmd.Connection = con;
        Label lbldeleteID = (Label)gvrolescreenMaster.Rows[e.RowIndex].FindControl("lblrollscreenId");
        cmd.CommandText = "Delete from roleToScreen where rollToScreenId='" + lbldeleteID.Text + "'";
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Role Screen Deleted Successfully');", true);
        gridbind(); 
    }
}
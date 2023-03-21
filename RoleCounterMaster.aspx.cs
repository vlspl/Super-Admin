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

public partial class SuperAdmin_RoleCounterMaster : System.Web.UI.Page
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
                db.bindDrp("select distinct rollMasterId, rollName from rollMaster where rollName!='Admin'  order by rollName asc", drproleMaster, "rollName", "rollMasterId");
                drproleMaster.Items.Insert(0, new ListItem("-Select Role-"));
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }
    void gridbind(string rollMasterId)
    {
        try
        {


            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT        RoleCounterMaster.roleCounterId, rollMaster.rollName, RoleCounterMaster.counterName, RoleCounterMaster.status
FROM            RoleCounterMaster INNER JOIN
                         rollMaster ON RoleCounterMaster.rollMasterId = rollMaster.rollMasterId where RoleCounterMaster.rollMasterId='" + rollMasterId + "'", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds, "RoleCounterMaster");
                con.Open();
                gvroleCounterMaster.DataSource = ds;
                gvroleCounterMaster.DataBind();
                con.Close();
            }
        }
        catch (Exception ex)
        {

            ex.Message.ToString();
        }
    }
    protected void ChangeStatus(object sender, EventArgs e)
    {
         try
        {

            string Status = "";
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            int ImageId = Convert.ToInt32(this.gvroleCounterMaster.DataKeys[row.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(constr))
            {
                Status = db.getData("select status from RoleCounterMaster WHERE roleCounterId = '" + ImageId + "'");
                if (Status == "Active")
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE RoleCounterMaster SET status = 'DeActive' WHERE roleCounterId = @ImageId", con))
                    {
                        cmd.Parameters.AddWithValue("@ImageId", ImageId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //Response.Redirect(Request.Url.AbsoluteUri);
                    }
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE RoleCounterMaster SET status = 'Active' WHERE roleCounterId = @ImageId", con))
                    {
                        cmd.Parameters.AddWithValue("@ImageId", ImageId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                       
                    }
                }
            }
            Response.Redirect(@"RoleCounterMaster.aspx", false);
        }
        catch (Exception ex)
        {

            ex.Message.ToString();
        }
    }



    protected void btnaddRole_Click(object sender, EventArgs e)
    {

        db.insert("insert into RoleCounterMaster(rollMasterId,counterName,status) values('" + drproleMaster.Text + "','" + drpcounter.Text + "','" + drpstatus.Text + "')");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Role Counter Added successfully');location.href='RoleCounterMaster.aspx';", true);
       
    }
    protected void drproleMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridbind(drproleMaster.Text);
    }
} 
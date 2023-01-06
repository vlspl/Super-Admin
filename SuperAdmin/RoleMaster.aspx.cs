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

public partial class SuperAdmin_RoleMaster : System.Web.UI.Page
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
                SqlCommand cmd = new SqlCommand("select rollMasterId,rollName,validDays,remark from rollMaster  order by 1 desc", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds, "rollMaster");
                con.Open();
                gvroleMaster.DataSource = ds;
                gvroleMaster.DataBind();
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
            int ImageId = Convert.ToInt32(this.gvroleMaster.DataKeys[row.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(constr))
            {
                Status = db.getData("select validDays from rollMaster WHERE rollMasterId = '" + ImageId + "'");
                if (Status == "Active")
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE rollMaster SET validDays = 'DeActive' WHERE rollMasterId = @ImageId", con))
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE rollMaster SET validDays = 'Active' WHERE rollMasterId = @ImageId", con))
                    {
                        cmd.Parameters.AddWithValue("@ImageId", ImageId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                       
                    }
                }
            }
            Response.Redirect(@"RoleMaster.aspx", false);
        }
        catch (Exception ex)
        {

            ex.Message.ToString();
        }
    }



    protected void btnaddRole_Click(object sender, EventArgs e)
    {
        //roleMstr.roleName = txtroleName.Text.ToString();
        //roleMstr.status = drpstatus.Text.ToString();
        //roleMstr.remark = txtremark.Text.ToString();
        //if (roleMstr.insertRoleEntry() == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Role Added successfully');location.href='RoleMaster.aspx';", true);
        //}
        //if (roleMstr.insertRoleEntry() == 1)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Error While Adding Role');location.href='RoleMaster.aspx';", true);
        //}
        //if (roleMstr.insertRoleEntry() == 3)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Role Already Exist');location.href='RoleMaster.aspx';", true);
        //}
        db.insert("insert into rollMaster(rollName,validDays,remark) values('"+txtroleName.Text+"','"+drpstatus.Text+"','"+txtremark.Text+"')");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Role Added successfully');location.href='RoleMaster.aspx';", true);
        gridbind();
    }
}
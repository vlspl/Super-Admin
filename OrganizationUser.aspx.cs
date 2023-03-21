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

public partial class SuperAdmin_orgUser : System.Web.UI.Page
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
                db.bindDrp("select distinct ID, Name from OrganizationMaster where IsActive=1 order by Name asc", drporg, "Name", "ID");
                 drporg.Items.Insert(0, new ListItem("-Select-"));
                
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }
 
    


    protected void btnaddRole_Click(object sender, EventArgs e)
    {
        if (drporg.Text != "-Select-")
        {
            if (txtuserName.Text != "")
            {
                if (!db.chkDBValue("select * from OrganizationUsers where Name='" + txtuserName.Text + "'"))
                {
                    if (txtmobile.Text != "")
                    {
                        if (txtemailId.Text != "")
                        {
                            if (txtpassword.Text != "")
                            {
                                db.insert("insert into OrganizationUsers(Branch_ID,Org_Id,Name,Contact,Email,Role,IsActive,CreatedDate) values('0','" + drporg.Text + "','" + txtuserName.Text + "','" + CryptoHelper.Encrypt(txtmobile.Text) + "','" + CryptoHelper.Encrypt(txtemailId.Text) + "','Enterprise','1','" + DateTime.Now.ToString("MM/dd/yyyy") + "')");
                                string maxId = db.getData("select max(ID) from OrganizationUsers");
                                db.insert("insert into UserLoginMaster(UserId,Mobile,EmailId,UserName,Password,Role,IsActive,CreatedDate) values('" + maxId + "','" + CryptoHelper.Encrypt(txtmobile.Text) + "','" + CryptoHelper.Encrypt(txtemailId.Text) + "','" + CryptoHelper.Encrypt(txtuserName.Text) + "','" + CryptoHelper.Encrypt(txtpassword.Text) + "','Enterprise','1','" + DateTime.Now.ToString("MM/dd/yyyy") + "')");
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Orgnization User Added successfully');location.href='OrganizationUser.aspx';", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Password cannot be Blank ');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Email Id cannot be Blank');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Mobile cannot be Blank');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('User Already Exist');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('User cannot be Blank');", true);
            }  
           
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Please Select Organization');", true);
        }
    }
    protected void drporg_TextChanged(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand(@"SELECT       ou.ID, ou.Name, ulm.Mobile, ulm.EmailId, ulm.UserName, ulm.Password, ulm.Role
FROM            OrganizationUsers ou INNER JOIN
                         UserLoginMaster ulm ON ou.ID = ulm.UserId and ulm.Role='Enterprise' and ou.Org_Id='" + drporg.Text + "'  order by 1 desc", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds, "OrganizationUsers");
            con.Open();
            gvorguser.DataSource = ds;
            gvorguser.DataBind();
            con.Close();
        }
    }

    protected void gvorguser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            string email = (dr["EmailId"].ToString());
            string pass = (dr["Password"].ToString());
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            e.Row.Cells[2].Text = CryptoHelper.Decrypt(email);
            e.Row.Cells[3].Text = CryptoHelper.Decrypt(pass);

        }
    }
}
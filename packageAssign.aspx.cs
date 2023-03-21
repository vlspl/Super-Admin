using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validation;
using DataAccessHandler;
using System.Net.Mail;
using System.Web.Configuration;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class SuperAdmin_packageAssign : System.Web.UI.Page
{
    DBClass db = new DBClass();
    InputValidation Ival = new InputValidation();
    DataAccessLayer DAL = new DataAccessLayer();
    CLSpackageMaster pkgMstr = new CLSpackageMaster();
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["AdminId"].Value != null)
            {
                if (!Page.IsPostBack)
                {
                    db.bindDrp("select distinct pMasterId, packageName from packageMaster where status='Active' order by packageName asc", drppackageName, "packageName", "pMasterId");
                    drppackageName.Items.Insert(0, new ListItem("-Select Package-"));
                    db.bindDrp("select distinct sLabId, sLabName from labMaster where IsActive=1 and sLabStatus='Active' order by sLabName asc", drplab, "sLabName", "sLabId");
                    drplab.Items.Insert(0, new ListItem("-Select Lab-"));
                    txtdescription.ReadOnly = true;
                    txtnoofDays.ReadOnly = true;
                    txtprice.ReadOnly = true;
                    txtstartDate_CalendarExtender.StartDate = DateTime.Now;
                    txtexpireDate_CalendarExtender.StartDate = DateTime.Now;
                }
            }
            else
            {
                Response.Redirect("AdminLogin.aspx");
            }
        }
        catch
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }


    protected void RegisterLab_Click(object sender, EventArgs e)
    {
        if (drppackageName.Text != "-Select Package-")
        {
            if (drplab.Text != "-Select Lab-")
            {
                if (txtstartDate.Text!="" && txtexpireDate.Text!="")
                {
                    string UserName = Request.Cookies["AdminUserName"].Value.ToString();
                    pkgMstr.masterId = drppackageName.Text.ToString();
                    pkgMstr.labId = drplab.Text.ToString();
                    pkgMstr.assignDate = txtstartDate.Text.ToString();
                    pkgMstr.days = txtnoofDays.Text.ToString();
                    pkgMstr.expiredDate = txtexpireDate.Text.ToString();
                    pkgMstr.createdDate = System.DateTime.Now.ToString("MM/dd/yyyy");
                    pkgMstr.createdBy = UserName;

                    if (pkgMstr.pkgAssign() == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Package Assign successfully');location.href='Dash.aspx';", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Start and Expired Date');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Lab Name');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Package Name');", true);
        }
        
       
    }



    protected void drppackageName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(drppackageName.Text!="-Select Package-")
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("select pMasterId,packageName,days,price,status,description from packageMaster where pMasterId='" + drppackageName.Text + "' and status='Active'  order by 1 desc", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds, "packageMaster");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            txtnoofDays.Text = row["days"].ToString();
                            txtprice.Text = row["price"].ToString();
                            txtdescription.Text = row["description"].ToString();
                            drpstatus.Text = row["status"].ToString();
                        }
                    }
                }
            }
        }
    }
}
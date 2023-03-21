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

public partial class SuperAdmin_packageMaster : System.Web.UI.Page
{
    CLSpackageMaster pkgMstr = new CLSpackageMaster();
    InputValidation Ival = new InputValidation();
    DataAccessLayer DAL = new DataAccessLayer();
    string mailFrom, mailFrom_password;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["AdminId"].Value != null)
            {
                if (!Page.IsPostBack)
                {
                   
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
         string Msg = "";
        if (Ival.IsTextBoxEmpty(txtpackageName.Text.ToString()))
        {
            Msg += "● Please Enter Package Name";
        }

      
     
        if (Ival.IsTextBoxEmpty(txtnoofDays.Text))
        {
            Msg += "● Please Enter Valid Days";
        }
        if (Ival.IsTextBoxEmpty(txtprice.Text))
        {
            Msg += "● Please Enter Valid Price";
        }
        if (drpstatus.Text=="-Status-")
        {
            Msg += "● Please Select Valid Status";
        }
        if (Msg.Length > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('" + Msg + "');", true);
            
        }
        else
        {
            string UserName = Request.Cookies["AdminUserName"].Value.ToString();
            pkgMstr.packageName = txtpackageName.Text.ToString();
            pkgMstr.days = txtnoofDays.Text.ToString();
            pkgMstr.price = txtprice.Text.ToString();
            pkgMstr.status = drpstatus.Text.ToString();
            pkgMstr.description = txtdescription.Text.ToString();
            pkgMstr.createdDate = System.DateTime.Now.ToString("MM/dd/yyyy");
            pkgMstr.createdBy = UserName;
            pkgMstr.updatedDate = System.DateTime.Now.ToString("MM/dd/yyyy");
            pkgMstr.updatedBy = UserName;
            if (pkgMstr.insertpkgEntry() >0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Package Created successfully');location.href='ViewPackages.aspx';", true);
            }
            if (pkgMstr.insertpkgEntry() == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Package Already Exist');location.href='ViewPackages.aspx';", true);
            }
        }
       
    }

   
   
}
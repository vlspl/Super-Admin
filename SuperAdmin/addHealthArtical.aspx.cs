using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using BitsBizLogic;
using DataAccessHandler;
using CrossPlatformAESEncryption.Helper;
using Validation;


public partial class SuperAdmin_ChannelPartner : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx", false);
        }

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
       try
       {
           if (txttitle.Text != "")
           {
               db.insert("insert into tbl_healthArtical(title,description,status) values('"+txttitle.Text+"','"+txtdescription.Text+"','1')");
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Health Artical Added successfully');location.href='healthArtical.aspx';", true);
                       
             
           }    

        }
        catch(Exception ex)
       {
            LogError.LoggerCatch(ex);
        }
      }
        
    }

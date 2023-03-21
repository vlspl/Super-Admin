using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using BitsBizLogic;
using DataAccessHandler;
using CrossPlatformAESEncryption.Helper;
using Validation;
using System.Net.Mail;
using System.Web.Configuration;



public partial class SuperAdmin_logs : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    string mailFrom, mailFrom_password;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Logid"] != null)
                {
                     SqlParameter[] paramEmgAgeRatio = new SqlParameter[]
                     {
                            new SqlParameter("@logId",Request.QueryString["Logid"].ToString())

                      };
                    DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_getViewLogList_byLogiD", paramEmgAgeRatio);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                txtlogDate.Text = row["Logdate"].ToString();
                                txtexceptionMsg.Text = row["ExceptionMsg"].ToString();
                                txtexceptionType.Text = row["ExceptionType"].ToString();
                                txtexceptionSource.Text = row["ExceptionSource"].ToString();
                                txtexceptionURL.Text = row["ExceptionURL"].ToString();
                                
                            }
                            
                        }
                        else
                        {
                           
                        }
                    }
                }
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx", false);
        }

    }
   
}
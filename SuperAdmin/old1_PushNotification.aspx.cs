using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public partial class SuperAdmin_PushNotifi : System.Web.UI.Page
{
    DBClass db = new DBClass();
    DataAccessLayer DAL = new DataAccessLayer();
    ClsFCMNotification ObjFCM = new ClsFCMNotification();
    ClsAppNotification objAppNotify = new ClsAppNotification();
    string constr;
    private SqlConnection con;
    private SqlCommand com;
    private void connection()
    {

        constr = ConfigurationManager.ConnectionStrings["constr"].ToString();
        con = new SqlConnection(constr);
        con.Open();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (true)//Request.Cookies["AdminId"].Value != null
        {
            if (!IsPostBack)
            {
                gridBind();
                db.bindDrp("select distinct sRole from appUser ", drprole, "sRole", "sRole");
                drprole.Items.Insert(0, new ListItem("All", "All"));
                //db.bindDrp("select distinct id, template from tbl_cNotification  order by template asc", drpnotification, "template", "template");
                //drpnotification.Items.Insert(0, new ListItem("--Select", "-Select-"));
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    void gridBind()
    {
        griduserActivation.DataSource = DAL.GetDataTable("Sp_userPushNotification_getdata");
        griduserActivation.DataBind();
        lblcount.Text = (griduserActivation.DataSource as DataTable).Rows.Count.ToString();
    }
    protected void griduserActivation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griduserActivation.PageIndex = e.NewPageIndex;
        gridBind();
    }

    protected void griduserActivation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        string gender = drpgender.Text;
        string role = drprole.Text;

        griduserActivation.DataSource = filterPushNotification(gender, role);
        griduserActivation.DataBind();
        lblcount.Text = (griduserActivation.DataSource as DataTable).Rows.Count.ToString();
    }
    public DataTable filterPushNotification(string gender, string role)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("sp_PushNotification_filter " + "'" + gender + "','" + role + "'");
            return dt;
        }
        catch (Exception ex)
        {
            dt = null;
            return dt;
        }
    }
    protected void btnsendnotification_Click(object sender, EventArgs e)
    {
        connection();
        com = new SqlCommand("Sp_userPushNotification_getdata", con);
        com.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(com);
        DataTable ds_AppUserDetails = new DataTable();
        da.Fill(ds_AppUserDetails);

        string Type = "Reminders";
        dynamic _Result = new JObject();
        //_Result.BookingId = bookLabId;
        string _payload = JsonConvert.SerializeObject(_Result);
        if (ds_AppUserDetails.Rows.Count != 0)
        {
            foreach (DataRow row in ds_AppUserDetails.Rows)
            {
                string deviceToken = row["sDeviceToken"].ToString();
                if (deviceToken != "null")
                {
                    string appUserId = row["sAppUserId"].ToString();
                    string Message = txtmessage.Text;
                    // string msg = "Your Push Notification at "+ Message + " " + ds_AppUserDetails.Rows[0]["Name"].ToString() + " has been created and Send successfully for covid vaccinated.";
                    ObjFCM.SendNotification("HowzU", Message, deviceToken, Type, appUserId.ToString());
                    int _result = objAppNotify.AppNotification(appUserId, "HowzU", Message, Type, _payload, appUserId.ToString());


                    string script = "{ sendnotification('" + deviceToken + "', '" + Message + "'); };";
                    //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "popup", script, true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "popup", script, true);
                }
            }
            txtmessage.Text = string.Empty;
        }
    }
    //protected void drpnotification_TextChanged(object sender, EventArgs e)
    //{
    //    txtmessage.Text = db.getData("select text from tbl_cNotification where template='"+drpnotification.Text+"'").ToString();
    //}
}
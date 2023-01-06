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
        string pincode=txtpincode.Text;
        DataTable dt_filterUser = new DataTable();
        dt_filterUser = filterPushNotification(gender, role, pincode);
        griduserActivation.DataSource = dt_filterUser;
        griduserActivation.DataBind();
        Session["Get_Data"] = dt_filterUser;

        lblcount.Text = (griduserActivation.DataSource as DataTable).Rows.Count.ToString();
    }
    public DataTable filterPushNotification(string gender, string role, string pincode)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("sp_PushNotification_filter " + "'" + gender + "','" + role + "','" + pincode + "'");
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
        //com = new SqlCommand("Sp_userPushNotification_getdata", con);
        //com.CommandType = CommandType.StoredProcedure;
       // SqlDataAdapter da = new SqlDataAdapter(com);
        DataTable ds_AppUserDetails = new DataTable();
        ds_AppUserDetails = (DataTable)Session["Get_Data"];
        //da.Fill(ds_AppUserDetails);

        string Type = "Reminders";
        dynamic _Result = new JObject();
        //_Result.BookingId = bookLabId;
        string _payload = JsonConvert.SerializeObject(_Result);
        if (ds_AppUserDetails.Rows.Count != 0)
        {
            foreach (DataRow row in ds_AppUserDetails.Rows)
            {
                string deviceToken = row["sDeviceToken"].ToString();
                if (deviceToken != "null" && deviceToken != "")
                {
                    string appUserId = row["sAppUserId"].ToString();
                    string Message = txtmessage.Text;
                    // string msg = "Your Push Notification at "+ Message + " " + ds_AppUserDetails.Rows[0]["Name"].ToString() + " has been created and Send successfully for covid vaccinated.";
                    ObjFCM.SendNotification_superAdmin(txttitle.Text, Message, txtimgUrl.Text, deviceToken, Type, appUserId.ToString());
                    int _result = objAppNotify.AppNotification(appUserId, txttitle.Text, Message, Type, _payload, appUserId.ToString());


                    string script = "{ sendnotification('" + deviceToken + "', '" + Message + "'); };";
                    //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "popup", script, true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "popup", script, true);
                }
            }
            txtmessage.Text = string.Empty;
        }
    }
   
}
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
using System.Web.UI.WebControls;
using System.Globalization;



public partial class SuperAdmin_waMessage : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    DBClass db = new DBClass();
    string mailFrom, mailFrom_password;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                db.bindDrp("select msgName from tbl_WhatsappMsgMaster where msgType='prom'  order by msgName asc", drptemplate, "msgName", "msgName");
                drptemplate.Items.Insert(0, new ListItem("-Select-", "-Select-"));
               
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx", false);
        }

    }
  
   
    protected void drptemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtremark.Text = db.getData("select paramList from tbl_WhatsappMsgMaster where msgName='" + drptemplate.Text + "'").ToString();
        txtbody.Text = db.getData("select body from tbl_WhatsappMsgMaster where msgName='" + drptemplate.Text + "'").ToString();
       
    }

    protected void drpuserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpuserType.Text == "Employee")
        {
            org.Visible = true;
            db.bindDrp("select distinct ID, Name from OrganizationMaster where IsActive=1 and Org_Status=1 order by Name asc", drporgnizationName, "Name", "Name");
            drporgnizationName.Items.Insert(0, new ListItem("-Select-", "-Select-"));
        }
        else
        {
            org.Visible = false;
        }
    }
    string mobile = "";
    string startDate = "";
    string EndDate = "";
    protected void BtnSave_Click(object sender, EventArgs e)
    {
       
        if (drpuserType.Text != "Employee")
        {
            if (txtonboardFrom.Text != "" && txttodate.Text != "")
            {
                string frodate = txtonboardFrom.Text;
                string todate = txttodate.Text;
                string d1 = frodate;
                string d2 = todate;
                DateTime appointdt1 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime appointdt2 = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                startDate = appointdt1.ToString("MM/dd/yyyy");
                EndDate = appointdt2.ToString("MM/dd/yyyy");
            }
                string type = drpuserType.Text;
                string gender = drpgender.Text;
                if (txtmobileNo.Text == "")
                {
                    mobile=txtmobileNo.Text;
                }
                else
                {
                    mobile = CryptoHelper.Encrypt(txtmobileNo.Text);
                }
              
               DataTable dt_pand = new DataTable();
                try
                {
                    dt_pand = DAL.GetDataTable("Sp_waMessage_filter " + "'" + type + "','" + startDate + "','" + EndDate + "','" + txtfromage.Text + "' ,'" + txttoage.Text + "' ,'" + mobile + "','" + txtarea.Text + "','" + txtpincode.Text + "' ");
                    Session["sendMsg"] = dt_pand;
                    if (dt_pand != null)
                    {
                        if (dt_pand.Rows.Count > 0)
                        {
                            string AllOtp = "";
                            int count = 0;
                            foreach (DataRow row in dt_pand.Rows)
                            {
                                string _Mobile = row["sMobile"].ToString() != "" ? CryptoHelper.Decrypt(row["sMobile"].ToString()) : "";
                                count = count + 1;
                                AllOtp += "<tr>" +
                                     "<td scope='col'>" + count + "</td>" +
                                            "<td scope='col'>" + row["sFullName"].ToString() + "</td>" +
                                            "<td scope='col'>" + _Mobile + "</td>" +
                                             "</tr>";
                            }
                            tbodyAlluserOTP.InnerHtml = AllOtp;
                        }
                        else
                        {
                            tbodyAlluserOTP.InnerHtml = "<tr><td>No Records Found</td></tr>";
                        }
                    }
                }
                catch (Exception ex)
                {
                    dt_pand = null;
                    
                }
              
        }
        else
        {
            if (txtonboardFrom.Text != "" && txttodate.Text != "")
            {
                string frodate = txtonboardFrom.Text;
                string todate = txttodate.Text;
                string d1 = frodate;
                string d2 = todate;
                DateTime appointdt1 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime appointdt2 = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                startDate = appointdt1.ToString("MM/dd/yyyy");
               EndDate = appointdt2.ToString("MM/dd/yyyy");
            }
            string type = drpuserType.Text;
            string gender = drpgender.Text;
            if (txtmobileNo.Text == "")
            {
                mobile = txtmobileNo.Text;
            }
            else
            {
                mobile = CryptoHelper.Encrypt(txtmobileNo.Text);
            }
            DataTable dt = new DataTable();
            try
            {
                dt = DAL.GetDataTable("Sp_waMessage_filter_Employee " + "'" + type + "','" + drporgnizationName.Text + "','" + startDate + "','" + EndDate + "','" + txtfromage.Text + "' ,'" + txttoage.Text + "' ,'" + mobile + "','" + txtarea.Text + "','" + txtpincode.Text + "' ");
                Session["sendMsg_employee"] = dt;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string AllOtp = "";
                        int count = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            string _Mobile = row["sMobile"].ToString() != "" ? CryptoHelper.Decrypt(row["sMobile"].ToString()) : "";
                            count = count + 1;
                            AllOtp += "<tr>" +
                                 "<td scope='col'>" + count + "</td>" +
                                        "<td scope='col'>" + row["sFullName"].ToString() + "</td>" +
                                        "<td scope='col'>" + _Mobile + "</td>" +
                                         "</tr>";
                        }
                        tbodyAlluserOTP.InnerHtml = AllOtp;
                    }
                    else
                    {
                        tbodyAlluserOTP.InnerHtml = "<tr><td>No Records Found</td></tr>";
                    }
                }
            }
            catch (Exception ex)
            {
                dt = null;

            }
              
        }
    }

    protected void btnsendMsg_Click(object sender, EventArgs e)
    {
        if(drpuserType.Text!="Employee")
        {
            DataTable dt_data = new DataTable();
             dt_data = (DataTable)Session["sendMsg"];
             foreach (DataRow row in dt_data.Rows)
             {
                 string name = row["sFullName"].ToString();
                 string mob = row["sMobile"].ToString();
                 string mobileNo = CryptoHelper.Decrypt(mob);
                 newWhatsapp wa = new newWhatsapp();
                 wa.sendWhatsappMsg("+91" + mobileNo, drptemplate.Text, name);
                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Message Send successfully');location.href='waMessage.aspx';", true);
             }
        }
        else
        {
            DataTable dt_data = new DataTable();
            dt_data = (DataTable)Session["sendMsg_employee"];
            foreach (DataRow row in dt_data.Rows)
            {
                string name = row["sFullName"].ToString();
                string mob = row["sMobile"].ToString();
                string mobileNo = CryptoHelper.Decrypt(mob);
                newWhatsapp wa = new newWhatsapp();
                wa.sendWhatsappMsg("+91" + mobileNo, drptemplate.Text, name);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Message Send successfully');location.href='waMessage.aspx';", true);
            }
        }
        
    }
}
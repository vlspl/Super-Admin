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



public partial class waMessage : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    DBClass db = new DBClass();
    string mailFrom, mailFrom_password;

    protected void Page_Load(object sender, EventArgs e)
    {

      
            if (!Page.IsPostBack)
            {
                string labId = Request.Cookies["labId"].Value.ToString();
                db.bindDrp("select msgName from tbl_WhatsappMsgMaster where sLabId='" + labId + "'  order by msgName asc", drptemplate, "msgName", "msgName");
                drptemplate.Items.Insert(0, new ListItem("-Select-", "-Select-"));
                if (db.chkDBValue("select ChannelPartnerCode from ChannelPartnerMaster where sLabId='" + labId + "'") == false)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Please Create Channel Partner Code For Particular Lab');", true);
                }
                else
                {
                    db.cnclose();
                    txtchanelpartner.Text = db.getData("select ChannelPartnerCode from ChannelPartnerMaster where sLabId='" + labId + "'");
                }
              
            }
      

    }
  
   
    protected void drptemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtremark.Text = db.getData("select paramList from tbl_WhatsappMsgMaster where msgName='" + drptemplate.Text + "'").ToString();
        txtbody.Text = db.getData("select body from tbl_WhatsappMsgMaster where msgName='" + drptemplate.Text + "'").ToString();
       
    }

    protected void drpuserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drpuserType.Text == "Employee")
        //{
        //    org.Visible = true;
        //    db.bindDrp("select distinct ID, Name from OrganizationMaster where IsActive=1 and Org_Status=1 order by Name asc", drporgnizationName, "Name", "Name");
        //    drporgnizationName.Items.Insert(0, new ListItem("-Select-", "-Select-"));
        //}
        //else
        //{
        //    org.Visible = false;
        //}
    }
    string type = string.Empty;
    string mobile = string.Empty;
    string gender = string.Empty;
    
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (drpuserType.Text != "Doctor")
        {
            string labId = Request.Cookies["labId"].Value.ToString();
           
           
            if (drpuserType.Text == "-Select-")
            {
                type = "";
            }
            else
            {
                type = drpuserType.Text;
            }

            if (drpgender.Text == "All")
            {
                gender = "";
            }
            else
            {
                gender = drpgender.Text;
            }
            if (txtmobileNo.Text == "")
            {
                mobile = txtmobileNo.Text;
            }
            else
            {
                mobile = CryptoHelper.Encrypt(txtmobileNo.Text);
            }

            DataTable dt_pand = new DataTable();
            try
            {
                string fromAge = txtfromage.Text;
                string toAge = txttoage.Text;
                string pinCode = txtpincode.Text;
                dt_pand = DAL.GetDataTable("Sp_VacDetails_statuswise_WA " + "'" + type + "','" + mobile + "','" + gender + "' ,'" + fromAge + "','" + toAge + "','" + pinCode + "','" + labId + "' ");
                Session["sendMsg_Patient"] = dt_pand;
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
            string labId = Request.Cookies["labId"].Value.ToString();


            if (drpuserType.Text == "-Select-")
            {
                type = "";
            }
            else
            {
                type = drpuserType.Text;
            }

            if (drpgender.Text == "All")
            {
                gender = "";
            }
            else
            {
                gender = drpgender.Text;
            }
            if (txtmobileNo.Text == "")
            {
                mobile = txtmobileNo.Text;
            }
            else
            {
                mobile = CryptoHelper.Encrypt(txtmobileNo.Text);
            }

            DataTable dt_pand = new DataTable();
            try
            {
                string fromAge = txtfromage.Text;
                string toAge = txttoage.Text;
                string pinCode = txtpincode.Text;
                dt_pand = DAL.GetDataTable("Sp_VacDetails_statuswise_WA_Doc " + "'" + type + "','" + mobile + "','" + gender + "' ,'" + fromAge + "','" + toAge + "','" + pinCode + "','" + labId + "' ");
                Session["sendMsg_Doctor"] = dt_pand;
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
    }

    protected void btnsendMsg_Click(object sender, EventArgs e)
    {
        if (drptemplate.Text != "-Select-")
        {
            if (drptype.Text != "-Select-")
            {
                string labId = Request.Cookies["labId"].Value.ToString();
                string name = string.Empty;
                if (drpuserType.Text != "Doctor")
                {
                    DataTable dt_data = new DataTable();
                    dt_data = (DataTable)Session["sendMsg_Patient"];
                    foreach (DataRow row in dt_data.Rows)
                    {
                        if (drptype.Text == "Name") // Name using Days Message 
                            name = txtname.Text;
                        else
                            name = row["sFullName"].ToString();// Parameter using for grid User Name Values.(Howzu App User Name)

                        // = "Ozone"; //= row["sFullName"].ToString();
                        string mob = row["sMobile"].ToString();
                        string mobileNo = CryptoHelper.Decrypt(mob);
                        newWhatsapp wa = new newWhatsapp();
                        wa.sendWhatsappMsg("+91" + mobileNo, drptemplate.Text, name, labId);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Message Send successfully');location.href='waMessage.aspx';", true);
                    }
                }
                else
                {
                    DataTable dt_data = new DataTable();
                    dt_data = (DataTable)Session["sendMsg_Doctor"];
                    foreach (DataRow row in dt_data.Rows)
                    {
                        if (drptype.Text == "Name")// Name using Days Message 
                            name = txtname.Text;
                        else
                            name = row["sFullName"].ToString();// Parameter using for grid User Name Values.(Howzu App User Name)

                        string mob = row["sMobile"].ToString();
                        string mobileNo = CryptoHelper.Decrypt(mob);
                        newWhatsapp wa = new newWhatsapp();
                        wa.sendWhatsappMsg("+91" + mobileNo, drptemplate.Text, name, labId);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Message Send successfully');location.href='waMessage.aspx';", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Type');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Template');", true);
        }
    }
   
    protected void drptype_TextChanged(object sender, EventArgs e)
    {
        if (drptype.Text == "Name")
        {
            txtname.Visible = true;
        }
        else
        {
            txtname.Visible = false;
        }
    }
}
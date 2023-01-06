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



public partial class SuperAdmin_labwaMessage : System.Web.UI.Page
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
                db.bindDrp("select distinct sLabId,sLabName from labMaster  where IsActive=1 and HowzuLab=1 and sLabStatus='Active' order by sLabName asc", drplablist, "sLabName", "sLabName");
                drplablist.Items.Insert(0, new ListItem("-Select-", "-Select-"));
                db.bindDrp("select msgName from tbl_WhatsappMsgMaster where msgType='prom' and sLabId=0  order by msgName asc", drptemplate, "msgName", "msgName");
                drptemplate.Items.Insert(0, new ListItem("-Select-", "-Select-"));
                showLabDetails();
                
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx", false);
        }

    }
    void showLabDetails()
    {
        SqlParameter[] paramEmgAgeRatio = new SqlParameter[]
         {
                           

          };
        DataTable  dt_details = DAL.ExecuteStoredProcedureDataTable("Sp_GetLabListformsg", paramEmgAgeRatio);
        Session["gvTotalUser"] = dt_details;
        if (dt_details != null)
        {
            if (dt_details.Rows.Count > 0)
            {
                string AllUserList = "";
                int count = 0;
                foreach (DataRow row in dt_details.Rows)
                {
                    count = count + 1;
                    //Load lab test taken list
                    string Patient_Mobile = row["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabContact"].ToString()) : "";
                    AllUserList += "<tr>" +
                                       "<td scope='col'>" + count + "</td>" +
                                     "<td scope='col'>" + row["sLabName"].ToString() + "</td>" +
                                      "<td scope='col'>" + row["sLabManager"].ToString() + "</td>" +
                                      "<td scope='col'>" + Patient_Mobile + "</td>" +
                                       "</tr>";



                }
                lablistbody.InnerHtml = AllUserList;
            }
            else
            {
                lablistbody.InnerHtml = "<tr><td colspan='5'>No records found</td></tr>";
            }
        }
       
    }
   
    protected void drptemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtremark.Text = db.getData("select paramList from tbl_WhatsappMsgMaster where msgName='" + drptemplate.Text + "'").ToString();
        txtbody.Text = db.getData("select body from tbl_WhatsappMsgMaster where msgName='" + drptemplate.Text + "'").ToString();
       
    }

   
   
    protected void btnsendMsg_Click(object sender, EventArgs e)
    {
        string name=string.Empty;
      
            DataTable dt_data = new DataTable();
            dt_data = (DataTable)Session["gvTotalUser"];
             foreach (DataRow row in dt_data.Rows)
             {
                 if(drptype.Text == "Name") // Name using Days Message 
                     name=txtname.Text;
                 else
                     name = row["sLabName"].ToString();// Parameter using for grid User Name Values.(Howzu App User Name)

                 // = "Ozone"; //= row["sFullName"].ToString();
                 string mob = row["sLabContact"].ToString();
                 string mobileNo = CryptoHelper.Decrypt(mob);
                 newWhatsapp wa = new newWhatsapp();
                 wa.sendWhatsappMsg_superadmin("+91" + mobileNo, drptemplate.Text, name);
                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Message Send successfully');location.href='labwaMessage.aspx';", true);
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
    protected void drplablist_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt_pand = DAL.GetDataTable("Sp_GetLabListformsg_filter " + "'" + drplablist.Text + "'");
        Session["gvTotalUser"] = dt_pand;
        if (dt_pand != null)
        {
            if (dt_pand.Rows.Count > 0)
            {
                string AllUserList = "";
                int count = 0;
                foreach (DataRow row in dt_pand.Rows)
                {
                    count = count + 1;
                    //Load lab test taken list
                    string Patient_Mobile = row["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabContact"].ToString()) : "";
                    AllUserList += "<tr>" +
                                       "<td scope='col'>" + count + "</td>" +
                                     "<td scope='col'>" + row["sLabName"].ToString() + "</td>" +
                                      "<td scope='col'>" + row["sLabManager"].ToString() + "</td>" +
                                      "<td scope='col'>" + Patient_Mobile + "</td>" +
                                       "</tr>";



                }
                lablistbody.InnerHtml = AllUserList;
            }
            else
            {
                lablistbody.InnerHtml = "<tr><td colspan='5'>No records found</td></tr>";
            }
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (txtonboardFrom.Text != "" && txttodate.Text != "")
        {
            string frodate = txtonboardFrom.Text;
            string todate = txttodate.Text;
            string d1 = frodate;
            string d2 = todate;
            DateTime appointdt1 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime appointdt2 = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string startDate = appointdt1.ToString("MM/dd/yyyy");
            string EndDate = appointdt2.ToString("MM/dd/yyyy");
            DataTable dt_panddate = DAL.GetDataTable("Sp_GetLabListformsg_filter_date " + "'" + startDate + "','" + EndDate + "'");
            Session["gvTotalUser"] = dt_panddate;
            if (dt_panddate != null)
            {
                if (dt_panddate.Rows.Count > 0)
                {
                    string AllUserList = "";
                    int count = 0;
                    foreach (DataRow row in dt_panddate.Rows)
                    {
                        count = count + 1;
                        //Load lab test taken list
                        string Patient_Mobile = row["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabContact"].ToString()) : "";
                        AllUserList += "<tr>" +
                                           "<td scope='col'>" + count + "</td>" +
                                         "<td scope='col'>" + row["sLabName"].ToString() + "</td>" +
                                          "<td scope='col'>" + row["sLabManager"].ToString() + "</td>" +
                                          "<td scope='col'>" + Patient_Mobile + "</td>" +
                                           "</tr>";



                    }
                    lablistbody.InnerHtml = AllUserList;
                }
                else
                {
                    lablistbody.InnerHtml = "<tr><td colspan='5'>No records found</td></tr>";
                }
            }
        }
    }
}
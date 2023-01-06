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

public partial class LabRegister : System.Web.UI.Page
{
    ClsLabRegister objlabreg = new ClsLabRegister();
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
                    string timestamp = DateTime.UtcNow.ToString("ddMMyyyyHHmmssms");
                    LabCode.Text = "LAB" + timestamp;
                    ddlName.DataSource = DAL.GetDataTable("Sp_GetOrgnizationDetailForDDL");
                    ddlName.DataBind();
                    ListItem lit = new ListItem();
                    lit.Text = "Select";
                    lit.Value = "0";
                    ddlName.Items.Insert(0, lit);
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
        if (Ival.IsTextBoxEmpty(LabName.Text.ToString()))
        {
            Msg += "● Please Enter Valid Lab Name";
        }

        if (!Ival.IsTextBoxEmpty(EmailId.Text))
        {
            if (!Ival.IsValidEmailAddress(EmailId.Text))
            {
                Msg += "● Please Enter Valid Email Id";
            }
        }
        if (Ival.IsInteger(LabContact.Text))
        {
            if (!Ival.MobileValidation(LabContact.Text))
            {
                Msg += "● Please Enter Valid Mobile Number";
            }
        }
        else
        {
            Msg += "● Please Enter Valid Mobile Number";
        }
        if (Ival.IsTextBoxEmpty(txtUserName.Text))
        {
            Msg += "● Please Enter Valid Username";
        }
        if (Ival.IsTextBoxEmpty(txtPassword.Text))
        {
            Msg += "● Please Enter Valid Password";
        }
        if (Ival.IsTextBoxEmpty(LabManager.Text))
        {
            Msg += "● Please Enter Valid Lab Manager Name";
        }
        if (Msg.Length > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');location.reload();", true);
        }
        else
        {
            objlabreg.sLabCode = LabCode.Text.ToString();
            objlabreg.sLabName = LabName.Text.ToString();
            objlabreg.sLabManager = LabManager.Text.ToString();
            objlabreg.sEmailId = EmailId.Text.ToString();
            objlabreg.sUserName = txtUserName.Text.ToString();
            objlabreg.sPassword = txtPassword.Text.ToString();
            objlabreg.sStatus = DropDownStatus.SelectedValue.ToString();
            objlabreg.sLabContact = LabContact.Text.ToString();
            objlabreg.sLabAddress = LabAddress.Text.ToString();
            objlabreg.latLong = txtlatLong.Text;

            objlabreg.orgId = ddlName.SelectedItem.Value;
            objlabreg.labStatus = ddlLabStatus.SelectedItem.Text;

            if (objlabreg.insertLabEntry() == 1)
            {
				 newWhatsapp wa = new newWhatsapp();
                //wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
                wa.sendWhatsappMsg_superadmin("+91" + LabContact.Text, "Lab Registration", LabName.Text + ',' + txtUserName.Text+','+txtPassword.Text);
                //Label lblMasterStatus = (Label)Master.FindControl("lblmsgText");
                //lblMasterStatus.Text = "Lab registered successfully";
               // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "Yopopupalert();", true);
                mailSending(LabManager.Text, EmailId.Text, txtUserName.Text, txtPassword.Text);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Lab registered successfully');location.href='Dashboard.aspx';", true);
            }
            else if (objlabreg.insertLabEntry() == 0)
            {
                Label lblMasterStatus = (Label)Master.FindControl("lblmsgText");
                lblMasterStatus.Text = "Error while registering the lab";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "Yopopupalert();", true);

                // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertError", "alert('Error while registering the lab');", true);
            }
            else if (objlabreg.insertLabEntry() == 3)
            {
                //Label lblMasterStatus = (Label)Master.FindControl("lblmsgText");
                //lblMasterStatus.Text = "This username is already registered, please use another username";
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "Yopopupalert();", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "dupicateUser", "alert('This username is already registered, please use another username');", true);
            }
        }
    }
    void mailSending(string Name, string emailId, string userName, string password)
    {
        StringBuilder sb = new StringBuilder();
        MailMessage mm = new MailMessage();

        mm.From = new MailAddress("visionarylifesciences7@gmail.com");
        mm.To.Add(emailId);
        //mm.Bcc.Add(emailId);
        mm.Bcc.Add("support@howzu.co.in");
        mm.Subject = "HowzU Pathology Dashboard – Thank you for your registration. Your account is verified. Sign in today.";
        //mm.Body = DivToHtml(divmessage);
        //mm.Body = innerText;
        mm.BodyEncoding = System.Text.Encoding.UTF8;
        mm.Body =

                "Greetings of the day! <br />" +

                "<h3>Dear " + Name + " </h3><br />" +
                  "<h3>Welcome to Howzu Pathology Dashboard.  </h3><br />" +

                "We take this opportunity to welcome you and your team to our best-in-class and state of the art healthcare solution. We have our presence PAN India with HQ based out of Pune. We are pleased that your Organisation decided to be part of our young and dynamic family.<br /><br /> " +
                "Our comprehensive health dashboards enable an Organization to streamline its employees’ health records on different levels ranging from departments,offices,cities etc. and analyze their health in statistical representation for effortless understanding. <br /><br />" +
                 "It also permits HR or any head of the department to authorize its employees to undergo specific tests from specific labs in special situations and update reports in the software. <br /><br />" +
                "<b>Get Started to be the change,</b><br /><br />" +
                   "<b> 1. Click on the below link to login : </b> <br /> https://visionarylifescience.com/ <br /><br />" +
                "<b>2 . Kindly use following credentials to Sign in,</b><br /><br />" +
                        "<b>User Name : " + userName + "</b><br />" +
                        "<b>PassCode : " + password + "</b><br /><br />" +

                
                "We are delighted to welcome you to the Howzu family, and are looking for a long and endeavoring business Relationship.<br /><br />" +

                "<b>Yours sincerely ,</b><br />" +
                "<b>Howzu Team. </b> <br />" +
                 "<b>support@howzu.co.in </b> ";

        //mm.Body = ;
        mm.IsBodyHtml = true;
        mm.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "relay-hosting.secureserver.net";
        smtp.EnableSsl = false;
        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
        NetworkCred.UserName = "visionarylifesciences7@gmail.com";
        NetworkCred.Password = "vls1234$";
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = NetworkCred;
        smtp.Port = 25;
        try
        {
            smtp.Send(mm);
        }
        catch (Exception ex)
        {
          
        }
       
    }
    public void Cleardata()
    {
        LabName.Text = "";
        LabManager.Text = "";
        EmailId.Text = "";
        txtPassword.Text = "";
        txtConfPassword.Text = "";
        DropDownStatus.SelectedItem.Text = "Active";
        LabContact.Text = "";
        LabAddress.Text = "";
        txtUserName.Text = "";
    }
    protected void ddlLabStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        if (ddlLabStatus.SelectedItem.Text == "Private")
            Panel1.Visible = true;
    }
}
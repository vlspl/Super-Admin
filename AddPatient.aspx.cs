using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validation;
using System.Data;
using DataAccessHandler;
using CrossPlatformAESEncryption.Helper;

public partial class AddPatient : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    ClsPatientList objPatient = new ClsPatientList();
    InputValidation Ival = new InputValidation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnupdate.Visible = false;
            btnviewpayment.Visible = false;
            btnviewHelth.Visible = false;
            if (Request.QueryString["id"] != null)
            {
                hiddenAppUserId.Value = Request.QueryString["id"].ToString();
                getpatientDetails();
                btnviewpayment.Visible = true;
                btnviewHelth.Visible = true;
                
            }
        }

    }
    public void getpatientDetails()
    {
        int id = Convert.ToInt32(Request.QueryString["id"].ToString());
       int labid = Convert.ToInt32(Request.Cookies["labId"].Value.ToString());
       DataTable ds = GetBranchDetails(labid, id);//
        btnupdate.Visible = true;
        btnAdd.Visible = false;
        foreach (DataRow row in ds.Rows)
        {

            txtMobile.Text = CryptoHelper.Decrypt(row["sMobile"].ToString());
            txtFullName.Text = row["sFullName"].ToString();
             if (txtEmailId.Text != "")
            {
                txtEmailId.Text = CryptoHelper.Decrypt(row["sEmailId"].ToString());
            }
            selGender.SelectedItem.Text = row["sGender"].ToString();
            txtBirthDate.Text = row["sBirthDate"].ToString();
            txtState.Text = row["sState"].ToString();
            txtPincode.Text = row["sPincode"].ToString();
            txtCity.Text = row["sCity"].ToString();
            txtAddress.Text = row["sAddress"].ToString();

        }
    }
    public DataTable GetBranchDetails(int labid, int id)//, 
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetPatientsbyId " + "'" + labid + "','" + id + "'");//
            return dt;
        }
        catch (Exception ex)
        {
            dt = null;
            return dt;
        }
    }
    protected void btnAdd_Click1(object sender, EventArgs e)
    {
        try
        {
            string Msg = "";
            if (Ival.IsTextBoxEmpty(txtFullName.Text))
            {
                Msg += "● Please Enter Valid Patient Name";
            }
            if (selGender.SelectedIndex <= 0)
            {
                Msg += "● Please Select Gender<br>";
            }
            if (!Ival.IsValidDate(txtBirthDate.Text))
            {
                Msg += "● Please Enter Valid Birth Date";
            }
            if (!Ival.IsTextBoxEmpty(txtEmailId.Text))
            {
                if (!Ival.IsValidEmailAddress(txtEmailId.Text))
                {
                    Msg += "● Please Enter Valid Email Id";
                }
            }
            if (Ival.IsInteger(txtMobile.Text))
            {
                if (!Ival.MobileValidation(txtMobile.Text))
                {
                    Msg += "● Please Enter Valid Mobile Number";
                }
            }
            else
            {
                Msg += "● Please Enter Valid Mobile Number";
            }
            //if (!Ival.IsInteger(txtPincode.Text))
            //{
            //    Msg += "● Please Enter Valid Pincode";
            //}
            if (Msg.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');location.reload();", true);
            }
            else
            {
                string labId = Request.Cookies["labId"].Value.ToString();
                string fullname = txtFullName.Text;
                string emailId = txtEmailId.Text;
                string mobile = txtMobile.Text;
                string gender = selGender.SelectedValue;

                string address = txtAddress.Text;
                string country = txtCountry.Text;
                string state = txtState.Text;
                string city = txtCity.Text;
                string pincode = txtPincode.Text;
                string action = hiddenAction.Value;
                string appUserId = hiddenAppUserId.Value;
                string birthDate = txtBirthDate.Text;
                int addPatient = objPatient.addPatient(action, appUserId, labId, fullname, emailId, mobile, gender, birthDate, address, country, state, city, pincode);
                if (addPatient == 1)
                {
                   newWhatsapp wa = new newWhatsapp();
                    wa.sendWhatsappMsg_superadmin("+91" + mobile, "Doctor/Patient Welcome", fullname);


                    Response.Redirect(@"PatientList.aspx", false);

                   // string MSG = "Hi Mr " + fullname + ", Please find the url to get yourself registered as a referral doctor. Please sign up at your earliest. Howzu";



                    // SendOTP objsms = new SendOTP();
                    // objsms.InvationSMSToDoctor(mobile, MSG);


                }
                else if (addPatient == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');location.reload();", true);
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnviewpayment_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"PatientInvoiceHistory.aspx?id=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
    }
    protected void btnviewHelth_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"PatientReportHistoryManagement.aspx?id=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
           
                string labId = Request.Cookies["labId"].Value.ToString();
                string fullname = txtFullName.Text;
                string emailId = txtEmailId.Text;
                string mobile = txtMobile.Text;
                string gender = selGender.SelectedValue;
                string address = txtAddress.Text;
                string country = txtCountry.Text;
                string state = txtState.Text;
                string city = txtCity.Text;
                string pincode = txtPincode.Text;
                string action = hiddenAction.Value;
                string appUserId = hiddenAppUserId.Value;
                string birthDate = txtBirthDate.Text;
                int addPatient = objPatient.updatePatient(action, appUserId, labId, fullname, emailId, mobile, gender, birthDate, address, country, state, city, pincode);
                if (addPatient == 1)
                {
                    Response.Redirect(@"PatientList.aspx", false);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
                }
                else if (addPatient == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');location.reload();", true);
                }
            
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
}
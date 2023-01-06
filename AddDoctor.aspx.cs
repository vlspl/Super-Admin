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
using System.Globalization;

public partial class AddDoctor : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    ClsDoctorList objDoctors = new ClsDoctorList();
    InputValidation Ival = new InputValidation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnupdate.Visible = false;
            
            if (Request.QueryString["id"] != null)
            {
                hiddenAppUserId.Value = Request.QueryString["id"].ToString();
                getpatientDetails();
               
                
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
            txtEmailId.Text = CryptoHelper.Decrypt(row["sEmailId"].ToString());
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
            dt = DAL.GetDataTable("Sp_GetDoctorsListbyid " + "'" + labid + "','" + id + "'");//
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
                Msg += "● Please Enter Valid Doctor Name";
            }
            if (selGender.SelectedIndex <= 0)
            {
                Msg += "● Please Select Gender<br>";
            }
            if (!Ival.IsTextBoxEmpty(txtBirthDate.Text))
            {
                if (!Ival.IsValidDate(txtBirthDate.Text))
                {
                    Msg += "● Please Enter Valid Birth Date";
                }
            }
            if (!Ival.IsTextBoxEmpty(txtEmailId.Text))
            {
                if (!Ival.IsValidEmailAddress(txtEmailId.Text))
                {
                    Msg += "● Please Enter Valid Email Id";
                }
            }
            if (!Ival.IsTextBoxEmpty(txtMobile.Text))
            {
                if (Ival.IsInteger(txtMobile.Text))
                {
                    if (!Ival.MobileValidation(txtMobile.Text))
                    {
                        Msg += "● Please Enter Valid Mobile Number";
                    }
                }
            }
            if (!Ival.IsInteger(txtPincode.Text))
            {
                Msg += "● Please Enter Valid Pincode";
            }
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
                string degree = txtDegree.Text;
                string specialization = txtSpecialization.SelectedItem.Text;
                string clinic = txtClinic.Text;
                string country = txtCountry.Text;
                string state = txtState.SelectedItem.Text;
                string city = txtCity.Text;
                string pincode = txtPincode.Text;
                string action = hiddenAction.Value;
                string appUserId = hiddenAppUserId.Value;
                string birthDate = "";
                if (txtBirthDate.Text == "")
                {
                    birthDate = DateTime.Now.ToString("dd/MM/yyyy");
                    if (birthDate.Contains("/"))
                    {
                        DateTime dt = DateTime.ParseExact(birthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        birthDate = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        DateTime dt = DateTime.ParseExact(birthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        birthDate = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    birthDate = txtBirthDate.Text;
                }
                int addDoctor = objDoctors.addDoctor(action, appUserId, labId, fullname, emailId, mobile, gender, birthDate, address, degree, specialization, clinic, country, state, city, pincode);

                if (addDoctor == 1)
                {
					 newWhatsapp wa = new newWhatsapp();
                    wa.sendWhatsappMsg_superadmin("+91" + mobile, "Doctor/Patient Welcome", fullname);

                    Response.Redirect(@"DoctorList.aspx", false);
                   
                }
                else if (addDoctor == 0)
                {
                  //  lblMessage.Text = "Error occured While Doctor Added";
                  //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');location.reload();", true);
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
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
                string degree = txtDegree.Text;
                string specialization = txtSpecialization.SelectedItem.Text;
                string clinic = txtClinic.Text;
                string country = txtCountry.Text;
                string state = txtState.Text;
                string city = txtCity.Text;
                string pincode = txtPincode.Text;
                string action = hiddenAction.Value;
                string appUserId = hiddenAppUserId.Value;
                string birthDate = "";
                if (txtBirthDate.Text == "")
                {
                    birthDate = DateTime.Now.ToString("dd/MM/yyyy");
                    if (birthDate.Contains("/"))
                    {
                        DateTime dt = DateTime.ParseExact(birthDate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        birthDate = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        DateTime dt = DateTime.ParseExact(birthDate, "dd-mm-yyyy", CultureInfo.InvariantCulture);
                        birthDate = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    birthDate = txtBirthDate.Text;
                }
                int addDoctor = objDoctors.updateDoctor(action, appUserId, labId, fullname, emailId, mobile, gender, birthDate, address, degree, specialization, clinic, country, state, city, pincode);

                if (addDoctor == 1)
                {
                    Response.Redirect(@"DoctorList.aspx", false);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
                }
                else if (addDoctor == 0)
                {
                   
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');location.reload();", true);
                }

        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
}
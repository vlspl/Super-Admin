using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.Services;
using System.Globalization;
using DataAccessHandler;
using System.Data.SqlClient;
using Newtonsoft.Json;
using CrossPlatformAESEncryption.Helper;
using Validation;
public partial class DoctorList : System.Web.UI.Page
{
    ClsDoctorList objDoctors = new ClsDoctorList();
    InputValidation Ival = new InputValidation();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"].Value != null && Request.Cookies["loggedIn"].Value.ToString() == "true")
            {
                if (!IsPostBack)
                {
                    loadMyDoctorsList();
                    Calendar1.EndDate = DateTime.Now.Date;
                    CalendarExtender1.EndDate = DateTime.Now.Date;
                    txtBirthDate.Attributes.Add("readonly", "readonly");
                    txtEditBirthDate.Attributes.Add("readonly", "readonly");
                }
            }
            else
            {
                Response.Redirect("LabLogin.aspx");
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void loadMyDoctorsList()
    {
        try
        {
            DataSet ds = objDoctors.getDoctors(Request.Cookies["labId"].Value.ToString());

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabMyDoctorsList = "";

                    int count = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        count = count + 1;
                        string mobile = (row["sMobile"].ToString() != "") ? CryptoHelper.Decrypt(row["sMobile"].ToString()) : "";
                        tabMyDoctorsList += "<li class='table-row'>" +
                                      "<div class='col col-1 text-center' data-label='Sr. No.' id='SrNo" + row["sappuserid"].ToString() + "' clientidmode='static'>" + count + "</div>" +
                                        "<div class='col col-2 text-center' TextAlig=right; data-label='Name' id='Fullname" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + row["sFullName"].ToString() + "</div>" +
                                        "<div class='col col-3 text-center' data-label='Gender' id='gender" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + row["sGender"].ToString() + "</div>" +
                                        "<div class='col col-2 text-center' data-label='Mobile' id='mobile" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + mobile + "</div>" +
                                        "<div class='col col-5 text-center' data-label='Address' id='address" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + row["sAddress"].ToString() + "</div>" +
                                        "<div class='col col-6 text-center' data-label='Degree'  id='degree" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + row["sDegree"].ToString() + "</div>" +
                                        "<div class='col col-7 text-center' data-label='Specialization' id='specialize" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + row["sSpecialization"].ToString() + "</div>" +
                                        "<div class='col col-8 text-center' data-label='Clinic' id='clinic" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + row["sClinic"].ToString() + "</div>" +
                                         "<div class='col col-9 text-center fa-color' data-label='Edit' ><a href='' id='" + row["sAppUserId"].ToString() + "' data-toggle='modal' data-target='#modalEditDoctor' class='HideEditbtn'><i class='fa fa-edit' aria-hidden='true'></i></a></div>" +
                                     "</li>";
                    }
                    tbodyDoctorList.Text = tabMyDoctorsList;
                }
                else
                {
                    tbodyDoctorList.Text = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Doctor added successfully');location.reload();", true);
                }
                else if (addDoctor == 0)
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

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            string Msg = "";
            if (Ival.IsTextBoxEmpty(txtEditFullName.Text))
            {
                Msg += "● Please Enter Valid Doctor Name";
            }
            if (selEditGender.SelectedIndex <= 0)
            {
                Msg += "● Please Select Gender<br>";
            }
            if (!Ival.IsTextBoxEmpty(txtEditBirthDate.Text))
            {
                if (!Ival.IsValidDate(txtEditBirthDate.Text))
                {
                    Msg += "● Please Enter Valid Birth Date";
                }
            }
            if (!Ival.IsTextBoxEmpty(txtEditEmailId.Text))
            {
                if (!Ival.IsValidEmailAddress(txtEditEmailId.Text))
                {
                    Msg += "● Please Enter Valid Email Id";
                }
            }
            if (!Ival.IsTextBoxEmpty(txtEditMobile.Text))
            {
                if (Ival.IsInteger(txtEditMobile.Text))
                {
                    if (!Ival.MobileValidation(txtEditMobile.Text))
                    {
                        Msg += "● Please Enter Valid Mobile Number";
                    }
                }
            }
            if (!Ival.IsInteger(txtEditPincode.Text))
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
                string fullname = txtEditFullName.Text;
                string emailId = txtEditEmailId.Text;
                string mobile = txtEditMobile.Text;
                string gender = selEditGender.SelectedValue;
                string address = txtEditAddress.Text;
                string degree = txtEditDegree.Text;
                string specialization = txtEditSpecialization.SelectedItem.Text;
                string clinic = txtEditClinic.Text;
                string country = txtEditCountry.Text;
                string state = txtEditState.Text;
                string city = txtEditCity.Text;
                string pincode = txtEditPincode.Text;
                string action = hiddenEditAction.Value;
                string appUserId = txthiddenEditAppUserId.Text;
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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
                }
                else if (addDoctor == 0)
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

    [WebMethod]
    public static string searchRecord(string mobile)
    {
        ClsDoctorList objPat = new ClsDoctorList();
        string labId = HttpContext.Current.Request.Cookies["labId"].Value.ToString();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        Dictionary<string, object> response = objPat.checkDoctorExist(labId, mobile);

        return serializer.Serialize(response);
    }

    [WebMethod]
    public static string GetDoctorDetails(int DoctorId)
    {
        DataAccessLayer DAL = new DataAccessLayer();
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
                {
                        new SqlParameter("@UserId",DoctorId)
                };
            ds = DAL.ExecuteStoredProcedureDataTable("WS_Sp__GetPatientDetailsById", param);
        }
        catch (Exception)
        {
            ds = null;
        }
        string sappuserid = "";
        string sFullName = "";
        string sGender = "";
        string sMobile = "";
        string sAddress = "";
        string sbirthdate = "";
        string semailid = "";
        string scountry = "";
        string sstate = "";
        string scity = "";
        string spincode = "";
        string sClinic = "";
        string sSpecialization = "";
        string sDegree = "";
        if (ds.Rows.Count > 0)
        {
            sappuserid = ds.Rows[0]["sappuserid"].ToString();
            sFullName = ds.Rows[0]["sFullName"].ToString();
            sGender = ds.Rows[0]["sGender"].ToString();
            sMobile = (ds.Rows[0]["sMobile"].ToString() != "") ? CryptoHelper.Decrypt(ds.Rows[0]["sMobile"].ToString()) : "";
            sAddress = ds.Rows[0]["sAddress"].ToString();
            sbirthdate = ds.Rows[0]["sbirthdate"].ToString();
            semailid = (ds.Rows[0]["semailid"].ToString() != "") ? CryptoHelper.Decrypt(ds.Rows[0]["semailid"].ToString()) : "";
            scountry = ds.Rows[0]["scountry"].ToString();
            sstate = ds.Rows[0]["sstate"].ToString();
            scity = ds.Rows[0]["scity"].ToString();
            spincode = ds.Rows[0]["spincode"].ToString();
            sClinic = ds.Rows[0]["sClinic"].ToString();
            sSpecialization = ds.Rows[0]["sSpecialization"].ToString();
            sDegree = ds.Rows[0]["sDegree"].ToString();
        }
        return JsonConvert.SerializeObject("[{'sappuserid':&&&" + sappuserid + "&&&'sFullName':&&&" + sFullName +
         "&&&'sGender':&&&" + sGender + "&&&'sMobile':&&&" + sMobile + "&&&'sAddress':&&&" + sAddress +
         "&&&'sbirthdate':&&&" + sbirthdate + "&&&'semailid':&&&" + semailid + "&&&'scountry':&&&" + scountry +
         "&&&'sstate':&&&" + sstate + "&&&'scity':&&&" + scity +
         "&&&'spincode':&&&" + spincode + "&&&'sClinic':&&&" + sClinic + "&&&'sSpecialization':&&&" + sSpecialization + "&&&'sDegree':&&&" + sDegree + "&&&}]");
    }
}
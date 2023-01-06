using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.Services;
using DataAccessHandler;
using Newtonsoft.Json;
using System.Data.SqlClient;
using CrossPlatformAESEncryption.Helper;
using Validation;
using System.Web.Script.Serialization;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public partial class PatientList : System.Web.UI.Page
{

    ClsPatientList objPatient = new ClsPatientList();
    InputValidation Ival = new InputValidation();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
                  //  SentOtpToPatient();
                    loadMyPatientsList();
                    Calendar1.EndDate = DateTime.Now.Date;
                    CalendarExtender1.EndDate = DateTime.Now.Date;
                    txtBirthDate.Attributes.Add("readonly", "readonly");
                    txtEditBirthDate.Attributes.Add("readonly", "readonly");
                    string sMonth = DateTime.Now.ToString("MM");
                    string _day = DateTime.Now.Day.ToString();
                    if (_day.Length == 1)
                    {
                        _day = "0" + _day;
                    }
                    hYear.Value = DateTime.Now.Year.ToString();
                    hMonth.Value = sMonth;
                    hDay.Value = _day;
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

 
    protected void loadMyPatientsList()
    {
        try
        {
            DataSet ds = objPatient.getPatients(Request.Cookies["labId"].Value.ToString());

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabMyPatientList = "";
                    int count = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //Load lab patient list
                        count = count + 1;
                      
                       // string mobile = (row["sMobile"].ToString() != "") ? CryptoHelper.Decrypt(row["sMobile"].ToString()) :CryptoHelper.Decrypt( "";
                        tabMyPatientList +=
                                             "<tr>" +
                                           "<td scope='col'>" + row["sappuserid"].ToString() + "</td>" +
                                            "<td scope='col'>" + row["sFullName"].ToString() + "</td>" +
                                            "<td scope='col'>" + row["sGender"].ToString() + "</td>" +
                                             "<td scope='col'>" + CryptoHelper.Decrypt(row["sMobile"].ToString()) + "</td>" +
                                             "<td scope='col'>" + row["sAddress"].ToString() + "</td>" +


                                               "<td scope='col'><a href='AddPatient.aspx?id=" + row["sAppUserId"].ToString() + "' '><i class='fa fa-edit fa-2x'></i></a></td>" +

                                             "</tr>";
                            
                         

                    }
                    tbodyPatientList.InnerHtml = tabMyPatientList;
                }
                else
                {
                    tbodyPatientList.InnerHtml = "<tr><td>No records found</td></tr>";
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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Patient added successfully');location.reload();", true);



                    string MSG = "Hi Mr " + fullname + ", Please find the url to get yourself registered as a referral doctor. Please sign up at your earliest. Howzu";



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
    private void SentOtpToPatient()
    {
        string apiUrl = "http://endpoint.visionarylifescience.com/Auth";
        object input = new
        {

            Mobile = txtMobile.Text.Trim(),
           

        };
        string inputJson = (new JavaScriptSerializer()).Serialize(input);
        WebClient client = new WebClient();
        client.Headers["Content-type"] = "application/json";
        client.Encoding = Encoding.UTF8;
        string json = client.UploadString(apiUrl + "/SendOTP", inputJson);






    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            string Msg = "";
            if (Ival.IsTextBoxEmpty(txtEditFullName.Text))
            {
                Msg += "● Please Enter Valid Patient Name";
            }
            if (selEditGender.SelectedIndex <= 0)
            {
                Msg += "● Please Select Gender";
            }
            if (!Ival.IsValidDate(txtEditBirthDate.Text))
            {
                Msg += "● Please Enter Valid Birth Date";
            }
            if (!Ival.IsTextBoxEmpty(txtEditEmailId.Text))
            {
                if (!Ival.IsValidEmailAddress(txtEditEmailId.Text))
                {
                    Msg += "● Please Enter Valid Email Id";
                }
            }
            if (Ival.IsInteger(txtEditMobile.Text))
            {
                if (!Ival.MobileValidation(txtEditMobile.Text))
                {
                    Msg += "● Please Enter Valid Mobile Number";
                }
            }
            else
            {
                Msg += "● Please Enter Valid Mobile Number";
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
                string country = txtEditCountry.Text;
                string state = txtEditState.Text;
                string city = txtEditCity.Text;
                string pincode = txtEditPincode.Text;
                string action = hiddenAction.Value;
                string appUserId = txthiddenEditAppUserId.Text;
                string birthDate = txtEditBirthDate.Text;
                int addPatient = objPatient.updatePatient(action, appUserId, labId, fullname, emailId, mobile, gender, birthDate, address, country, state, city, pincode);
                if (addPatient == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
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
    [WebMethod]
    public static string searchRecord(string mobile)
    {
        ClsPatientList objPat = new ClsPatientList();
        string labId = HttpContext.Current.Request.Cookies["labId"].Value.ToString();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        Dictionary<string, object> response = objPat.checkPatientExist(labId, mobile);

        return serializer.Serialize(response);
    }
    [WebMethod]
    public static string GetPatientDetails(int PatientId)
    {
        DataAccessLayer DAL = new DataAccessLayer();
        string ss = PatientId.ToString();
        int nn = PatientId;
        string TSAMID = "";
        DataTable ds = new DataTable();
        try
        {
            SqlParameter[] param = new SqlParameter[]
                {
                        new SqlParameter("@UserId",PatientId)
                };
            ds = DAL.ExecuteStoredProcedureDataTable("WS_Sp__GetPatientDetailsById", param);
        }
        catch (Exception)
        {
            ds = null;
            // return ds;
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
        }
        return JsonConvert.SerializeObject("[{'sappuserid':&&&" + sappuserid + "&&&'sFullName':&&&" + sFullName +
         "&&&'sGender':&&&" + sGender + "&&&'sMobile':&&&" + sMobile + "&&&'sAddress':&&&" + sAddress +
         "&&&'sbirthdate':&&&" + sbirthdate + "&&&'semailid':&&&" + semailid + "&&&'scountry':&&&" + scountry +
         "&&&'sstate':&&&" + sstate + "&&&'scity':&&&" + scity +
         "&&&'spincode':&&&" + spincode + "&&&}]");
    }
}
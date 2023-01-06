using System;
using System.Web.UI;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using DataAccessHandler;
using System.Data.SqlClient;
using CrossPlatformAESEncryption.Helper;
using Validation;
using System.Globalization;

public partial class BookTest : System.Web.UI.Page
{
    ClsPatientList objPatient = new ClsPatientList();
    ClsTestList objTestList = new ClsTestList();
    ClsLabCalendar objLabCalendar = new ClsLabCalendar();
    ClsBookTest objBookTest = new ClsBookTest();
    ClsDoctorList objDoctors = new ClsDoctorList();
    ClsSectionMaster objSelectSection = new ClsSectionMaster();
    ClsProfileMaster objProfileMaster = new ClsProfileMaster();
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                Calendar1.EndDate = DateTime.Now.Date;
                CalendarExtender1.EndDate = DateTime.Now.Date;
                CalendarExtender2.StartDate = DateTime.Now.Date;
                if (!IsPostBack)
                {
                    string CurruntDate = DateTime.Now.ToString("dd/MM/yyyy");
                    hiddenTestDate.Value = CurruntDate;
                    txtTestDate.Text = hiddenTestDate.Value;

                    loadMyPatientsList();
                    loadMyTestList();
                    loadMyCalendar();
                    loadMyDoctorsList();
                    LoadSections();

                   // txtBirthDate.Attributes.Add("readonly", "readonly");
                   // txtBirthDate1.Attributes.Add("readonly", "readonly");
                    txtTestDate.Attributes.Add("readonly", "readonly");
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
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');", true);
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
                    int i = 1;
                    int count = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                       // string _Mobile = row["sMobile"].ToString() != "" ? CryptoHelper.Decrypt(row["sMobile"].ToString()) : "";
                        count = count + 1;
                        //Load lab patient list
                        tabMyPatientList += "<li class='table-row'>" +
                                           "<div class='col col-1 text-center' data-label='Action'><input class='radio-custom icheck-success'  type='radio' id='patselid" + i + "' value='" + row["sAppUserId"].ToString() + "|" + row["sFullName"].ToString() + "|" + row["sGender"].ToString() + "|" + CryptoHelper.Decrypt(row["sMobile"].ToString()) + "' name='rdoPatient' clientidmode='Static' onclick='patientSelect(this)'><label class='radio-custom-label'></label></div>" +
                                           "<div class='col col-2 text-center' data-label='Sr. No.'>" + count + "</div>" +
                                           "<div class='col col-3 text-center' data-label='Name'>" + row["sFullName"].ToString() + "</div>" +
                                           "<div class='col col-4 text-center' data-label='Gender'>" + row["sGender"].ToString() + "</div>" +
                                           "<div class='col col-5 text-center' data-label='Mobile'>" + CryptoHelper.Decrypt(row["sMobile"].ToString()) + "</div>" +
                                        "</li>";
                        i++;
                    }
                    tbodyPatientList.Text = tabMyPatientList;
                }
                else
                {
                    tbodyPatientList.Text = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');", true);
        }
    }
    protected void loadMyTestList()
    {
        try
        {
            //DataSet ds = objTestList.getMyTests(Request.Cookies["labId"].Value.ToString());
            DataSet ds = objTestList.getMyTestsForBooking(Request.Cookies["labId"].Value.ToString());
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabMyTestList = "";
                    int count = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        count = count + 1;
                        //Load lab test list
                        tabMyTestList += "<tr>" +
                                           "<td scope='col' style='width:100px;'><input class='radio-custom' type='checkbox' value='" + row["sTestId"].ToString() + "|" + row["sPrice"].ToString() + "|" + row["sTestCode"].ToString() + "' id='chkTest'  name='chkTest' clientidmode='Static' ><label class='radio-custom-label'></label></td>" +
                                           "<td scope='col' style='width:150px;' id='testcode" + row["sTestId"].ToString() + "' clientidmode='static'>" + row["sTestCode"].ToString() + "</td>" +
                                           "<td scope='col' style='width:225px;' id='testname" + row["sTestId"].ToString() + "' clientidmode='static'>" + row["sTestName"].ToString() + "</td>" +
                                           "<td scope='col' style='width:250px;' id='testProfileName" + row["sTestId"].ToString() + "' clientidmode='static'>" + row["sProfileName"].ToString() + "</td>" +
                                           "<td scope='col' style='width:178px;' id='testSectionName" + row["sTestId"].ToString() + "' clientidmode='static'>" + row["sSectionName"].ToString() + "</td>" +
                                         
                                           "</tr>";
                    }
                    tbodyMyTestList.InnerHtml = tabMyTestList;
                }
                else
                {
                    tbodyMyTestList.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');", true);
        }
    }
    protected void loadMyCalendar()
    {
        try
        {
            DataSet ds = objLabCalendar.getLabSlots(Request.Cookies["labId"].Value.ToString());
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabMyLabSlots = "";

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //Load lab test list
                        tabMyLabSlots += "<tr >" +
                                           "<td><input class='radio-custom' type='radio'name='RDBrecords' value='" + row["sFrom"].ToString() + "-" + row["sTo"].ToString() + "/" + row["sAppointmentType"].ToString() + "'  name='rdoLabSlot' clientidmode='Static' onclick='javascript:timeSlotSelect(this)'><label class='radio-custom-label'></label></td>" +
                                           "<td>" + row["sDay"].ToString() + "</td>" +
                                           "<td>" + row["sFrom"].ToString() + "</td>" +
                                           "<td>" + row["sTo"].ToString() + "</td>" +
                                           "<td>" + row["sAppointmentType"].ToString() + "</td>" +
                                        "</tr>";
                    }
                    tbodyLabSlots.InnerHtml = tabMyLabSlots;
                }
                else
                {
                    tbodyLabSlots.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');", true);
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
                        string _Mobile = row["sMobile"].ToString() != "" ? CryptoHelper.Decrypt(row["sMobile"].ToString()) : "";
                        count = count + 1;
                        //Load lab Doctor list
                        tabMyDoctorsList += "<li class='table-row'>" +
                                            "<div class='col col-1 text-center'><input class='radio-custom' type='radio' value='" + row["sAppUserId"].ToString() + "|" + row["sFullName"].ToString() + "|" + row["sGender"].ToString() + "|" + _Mobile + "' name='rdoDoctor' clientidmode='Static' onclick='javascript:doctorSelect(this)'><label class='radio-custom-label'></label></div>" +
                                            "<div class='col col-2 text-center' id='Sr.No" + row["sappuserid"].ToString() + "' clientidmode='static'>" + count + "</div>" +
                                            "<div class='col col-3 text-center' id='Fullname" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + row["sFullName"].ToString() + "</div>" +
                                            "<div class='col col-4 text-center' id='gender" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + row["sGender"].ToString() + "</div>" +
                                            "<div class='col col-5 text-center' id='mobile" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + _Mobile + "</div>" +
                                            "<div class='col col-6 text-center' id='address" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + row["sAddress"].ToString() + "</div>" +
                                            "<div class='col col-7 text-center' class='' id='degree" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + row["sDegree"].ToString() + "</div>" +
                                            "<div class='col col-8 text-center' class='' id='specialize" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + row["sSpecialization"].ToString() + "</div>" +
                                            "<div class='col col-9 text-center' class='' id='clinic" + row["sAppUserId"].ToString() + "' clientidmode='static'>" + row["sClinic"].ToString() + "</div>" +
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
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');", true);
        }
    }
    protected void LoadSections()
    {
        try
        {
            DataSet ds = DAL.GetDataSet("Sp_GetMySection " + Request.Cookies["labId"].Value.ToString());
            //  DataSet ds = objSelectSection.getSection();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabsection = "";
                    string tabProfile = "";
                    int counte = 0;
                    int count = 0;

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        count = count + 1;
                        //Load lab test list
                        tabsection += "<a  data-toggle='tab'  class='hidediv1' onclick='sectionclick(" + row["sSectionid"].ToString() + ")'  href='#tab" + count + "' id='tabclick" + count + "'>" + row["sSectionName"].ToString() + "</a>";
                        string sectionid = row["sSectionId"].ToString();
                        tabProfile += "<div id='tab" + count + "'  class='tab-pane fade'>";
                        tabProfile += "<div class='panel-group test-content' id='accordion" + counte + "'>";
                        // DataSet ds1 = objProfileMaster.getTestProfile(sectionid);
                        SqlParameter[] paramTestProfile = new SqlParameter[]
                    {
                        new SqlParameter("@sSectionId",sectionid),
                        new SqlParameter("@LabId",Request.Cookies["labId"].Value.ToString())
                    };
                        DataSet ds1 = DAL.ExecuteStoredProcedureDataSet("Sp_GetMyTestProfileBysSectionId", paramTestProfile);
                        if (ds1 != null)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow rown in ds1.Tables[0].Rows)
                                {
                                    counte = counte + 1;
                                    //Load lab test list
                                    string testProfileId = rown["sTestProfileid"].ToString();
                                    StringBuilder sb123 = new StringBuilder();
                                    string ss = Request.Cookies["labId"].Value.ToString();
                                    DataSet ds2 = objTestList.getMyTestsForBooking(Request.Cookies["labId"].Value.ToString(), testProfileId);
                                    if (ds2 != null)
                                    {
                                        string tt = ds2.Tables[0].Rows.Count.ToString();
                                        if (ds2.Tables[0].Rows.Count > 0)
                                        {
                                            int counter = 0;
                                            foreach (DataRow rowno in ds2.Tables[0].Rows)
                                            {
                                                sb123.Append(rowno["sTestname"].ToString() + " ");
                                                counter = counter + 1;
                                                //Load lab test list
                                            }
                                        }
                                    }
                                    tabProfile += "<div class='panel panel-default " + Regex.Replace(sb123.ToString(), @"[^0-9a-zA-Z]+", " ") + "'>";
                                    tabProfile += "<div class='panel-heading'>";
                                    tabProfile += "<div class='checkbox test-checkbox checkbox-info checkbox-circle'>";
                                    tabProfile += "<input id='checkbox" + counte + "' class='styled' name='profile' type='checkbox' value='" + rown["sTestProfileId"] + "'>";
                                    tabProfile += "<label for='checkbox" + counte + "'>";
                                    tabProfile += "<h4 class='panel-title'> <a class='accordion-toggle collapsed' data-toggle='collapse' data-parent='#accordion" + counte + "' href='#collapse" + counte + "' aria-expanded='false'> " + counte + ". " + rown["sProfileName"].ToString() + " </a> </h4>";
                                    tabProfile += "</label>";
                                    tabProfile += "</div>";
                                    tabProfile += "</div>";
                                    tabProfile += "<div id='collapse" + counte + "' class='panel-collapse collapse' aria-expanded='false'>";
                                    tabProfile += "<div class='panel-body testlist'>";
                                    //  DataSet ds2 = objTestList.getMyTestsForBooking(Request.Cookies["labId"].Value.ToString(), testProfileId);
                                    if (ds2 != null)
                                    {
                                        string tt = ds2.Tables[0].Rows.Count.ToString();
                                        if (ds2.Tables[0].Rows.Count > 0)
                                        {
                                            int counter = 0;
                                            foreach (DataRow rowno in ds2.Tables[0].Rows)
                                            {
                                                counter = counter + 1;
                                                //Load lab test list
                                                string seltestid = rowno["sTestid"].ToString();

                                                tabProfile += "<div class='checkbox test-checkbox checkbox-info checkbox-circle'>";
                                                tabProfile += "<input id='" + seltestid + "' value='" + rowno["sTestid"].ToString() + "|" + rowno["sprice"].ToString() + "|" + rowno["sTestcode"].ToString() + "|" + counte + "|" + rowno["sTestProfileId"].ToString() + "|" + rowno["count"].ToString() + "' class='styled' name='testname'  type='checkbox'>";
                                                tabProfile += "<label for='" + seltestid + "'>";
                                                tabProfile += rowno["sTestcode"].ToString() + "  -  " + rowno["sTestname"].ToString();
                                                tabProfile += "</label>";
                                                tabProfile += "</div>";
                                            }
                                        }
                                    }
                                    tabProfile += "</div>";
                                    tabProfile += "</div>";
                                    tabProfile += "</div>";
                                }
                            }
                            else
                            {
                                //  tbodyMyTestList.InnerHtml = "<tr><td>No records found</td></tr>";
                            }
                        }
                        tabProfile += "<div class='panel panel-default' id='NoResult" + count + "' style='display: none;'><div class='panel-heading'> No Result found</div></div>";
                        tabProfile += "</div></div>";
                    }
                    profilelist.InnerHtml = tabProfile;
                    sectionlist.InnerHtml = tabsection;
                }
                else
                {
                    //  tbodyMyTestList.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');", true);
        }
    }
    protected void btnEditTestPrice_Click(object sender, EventArgs e)
    {
        try
        {
            string labId = Request.Cookies["labId"].Value.ToString();
            string labtestPrice = txtEditTestPrice.Text.ToString();
            string labtestPriceid = hiddenEditTestCodeId.Value;

            string query = "UPDATE testLab SET sPrice = " + labtestPrice + " where sTestId = " + labtestPriceid + " and sLabId='" + labId + "'";

            if (objTestList.updatePrice(query) == 1)
            { }
            else if (objTestList.updatePrice(query) == 0)
            { }
            // loadMyTestList();
            Response.Redirect("BookTest.aspx");
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');", true);
        }
    }
    protected void btnConfirmBooking_Click(object sender, EventArgs e)
    {
        try
        {
            string TestDate = "";
            if (hiddenTestDate.Value != "")
            {
                TestDate = hiddenTestDate.Value;
                if (TestDate.Contains("/"))
                {
                    DateTime dt = DateTime.ParseExact(TestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    TestDate = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    DateTime dt = DateTime.ParseExact(TestDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    TestDate = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
            }

            string Msg = "";
            if (!Ival.IsInteger(hiddenPatientId.Value))
            {
                Msg += "● Please Select Patient";
            }
            if (!Ival.IsInteger(HiddenDoctorid.Value))
            {
                Msg += "● Please Select doctor";
            }
            if (Ival.IsTextBoxEmpty(hiddenAppointmentType.Value))
            {
                Msg += "● Please Select Appointment Type";
            }
            if (Ival.IsTextBoxEmpty(hiddenTimeSlot.Value))
            {
                Msg += "● Please Select Time Slot";
            }
            if (Ival.IsTextBoxEmpty(hiddenTestList.Value))
            {
                Msg += "● Please Select Valid Test";
            }
            if (!Ival.IsValidDate(TestDate))
            {
                Msg += "● Please Enter Valid Test Date";
            }
            if (Msg.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');location.reload();", true);
            }
            else
            {
                string labId = Request.Cookies["labId"].Value.ToString();
                string patientId = hiddenPatientId.Value;
                string AppointmentType = hiddenAppointmentType.Value;
                string doctorId = HiddenDoctorid.Value;
                string timeSlot = hiddenTimeSlot.Value;
                string bookStatus = "Confirmed";
                string testStatus = "Pending";
                string bookMode = "Manual";
                string testDate = TestDate;
                string fees = HFinalAmount.Value;
                string testId = hiddenTestList.Value;
                string TestPrice = hTestPricearray.Value;

                int bookTest = objBookTest.bookTest(labId, patientId, doctorId, timeSlot,
                 bookStatus, testStatus, bookMode, testDate, fees, testId, AppointmentType, TestPrice);
                if (bookTest == 1)
                {
                   string mob = db.getData("select sMobile from appUser where sAppUserId='"+ patientId + "'");
					 string fullname = db.getData("select sFullName from appUser where sAppUserId='" + patientId + "'");
                  
                    string mobileNo = CryptoHelper.Decrypt(mob).ToString();
					 string labName = db.getData("select sLabName from labMaster where sLabId='" + labId + "'");
                    newWhatsapp wa = new newWhatsapp();
                    wa.sendWhatsappMsg("+91" + mobileNo, "Confirm Booking For LAb", fullname + ',' + labName, labId);


                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.href='TestBookList.aspx';", true);
                }
                else if (bookTest == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');", true);
        }
    }
	string mobile = "";
    string appUserId = "";
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
                Msg += "● Please Select Gender";
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
                
                string gender = selGender.SelectedValue;
                string address = txtAddress.Text;
                string country = "India";
                string state = txtState.SelectedItem.Text;
                string city = txtCity.Text;
                string pincode = txtPincode.Text;
                string action = hiddenAction.Value;
                appUserId = hiddenAppUserId.Value;
                string birthDate = txtBirthDate.Text;
                string MsgTemplate = "";
                if (txtMobile.Text != "")
                {
                    mobile = txtMobile.Text;
                }
                else
                {
                    string year = DateTime.Now.ToString("yy");
                    appUserId = db.getData("select max(sAppUserId) from appUser ");
                    mobile = year + labId + appUserId  ;
                }
                int addPatient = objPatient.addPatient(action, appUserId, labId, fullname, emailId, mobile, gender, birthDate, address, country, state, city, pincode);

                if (addPatient == 1)
                {
                    newWhatsapp wa = new newWhatsapp();
                       string labName = db.getData("select sLabName from labMaster where sLabId='" + labId + "'");
                  
                    //wa.sendWhatsappMsg_superadmin("+91" + mobile, "Doctor/Patient Welcome", fullname);
   wa.sendWhatsappMsg("+91" + mobile, "Doctor/Patient Registration For Lab", fullname + ',' + labName + ',' + mobile + ',' + labName, labId);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal","alert('New Patient Added ');location.reload();", true);
                }
                else if (addPatient == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');location.reload();", true);
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
    protected void btnAddDoctor_Click(object sender, EventArgs e)
    {
        try
        {
            string Msg = "";
            if (Ival.IsTextBoxEmpty(txtFullName1.Text))
            {
                Msg += "● Please Enter Valid Doctor Name";
            }
            if (selGender1.SelectedIndex <= 0)
            {
                Msg += "● Please Select Gender<br>";
            }
            if (!Ival.IsTextBoxEmpty(txtBirthDate1.Text))
            {
                if (!Ival.IsValidDate(txtBirthDate1.Text))
                {
                    Msg += "● Please Enter Valid Birth Date";
                }
            }
            if (!Ival.IsTextBoxEmpty(txtEmailId1.Text))
            {
                if (!Ival.IsValidEmailAddress(txtEmailId1.Text))
                {
                    Msg += "● Please Enter Valid Email Id";
                }
            }
            if (!Ival.IsTextBoxEmpty(txtMobile1.Text))
            {
                if (Ival.IsInteger(txtMobile1.Text))
                {
                    if (!Ival.MobileValidation(txtMobile1.Text))
                    {
                        Msg += "● Please Enter Valid Mobile Number";
                    }
                }
            }
            if (!Ival.IsInteger(txtPincode1.Text))
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
                string fullname = txtFullName1.Text;
                string emailId = txtEmailId1.Text;
                string mobile = txtMobile1.Text;
                string gender = selGender1.SelectedValue;
                string address = txtAddress1.Text;
                string degree = txtDegree1.Text;
                string specialization = txtSpecialization1.SelectedItem.Text;
                string clinic = txtClinic1.Text;
                string country = "India";
                string state = txtState1.SelectedItem.Text;
                string city = txtCity1.Text;
                string pincode = txtPincode1.Text;
                string action = hiddenAction1.Value;
                string appUserId = hiddenAppUserId1.Value;
                string birthDate = "";
                if (txtBirthDate1.Text == "")
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
   string labName = db.getData("select sLabName from labMaster where sLabId='" + labId + "'");
                  
                   // wa.sendWhatsappMsg_superadmin("+91" + mobile, "Doctor/Patient Welcome", fullname);
                 wa.sendWhatsappMsg("+91" + mobile, "Doctor/Patient Registration For Lab", fullname + ',' + labName + ',' + mobile + ',' + labName, labId);

                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal","alert('New Doctor Added ');location.reload();", true);
                }
                else if (addDoctor == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');location.reload();", true);
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
}
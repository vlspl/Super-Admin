using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using Validation;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using CrossPlatformAESEncryption.Helper;

public partial class BookDetails : System.Web.UI.Page
{
    ClsBookDetails objBookDetails = new ClsBookDetails();
    ClsTestList objTestList = new ClsTestList();
    ClsBookTest objBookTest = new ClsBookTest();
    ClsLabCalendar objLabCalendar = new ClsLabCalendar();
    ClsAppNotification objAppNotify = new ClsAppNotification();
    ClsFCMNotification ObjFCM = new ClsFCMNotification();
    InputValidation Ival = new InputValidation();
    int tolfees;
    string LoginId;
    string Createdby = "0";
DBClass db = new DBClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["loggedIn"] != null)
        {
            CalendarExtender2.StartDate = DateTime.Now;
            if (!IsPostBack)
            {
                loadBookingDetails();
                loadMyCalendar();
            }
            Createdby = Request.Cookies["labUserId"].Value.ToString();
        }
        else
        {
            Response.Redirect("LabLogin.aspx");
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
                        tabMyLabSlots += "<tr>" +
                                           "<td scope='col'><input class='radio-custom' type='radio'name='RDBrecords' value='" + row["sFrom"].ToString() + "-" + row["sTo"].ToString() + "/" + row["sAppointmentType"].ToString() + "'  name='rdoLabSlot' clientidmode='Static' onclick='javascript:timeSlotSelect(this)'><label class='radio-custom-label'></label></td>" +
                                           "<td scope='col'>" + row["sDay"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sFrom"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sTo"].ToString() + "</td>" +
                                            "<td scope='col'>" + row["sAppointmentType"].ToString() + "</td>" +
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
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
    protected void loadBookingDetails()
    {
        try
        {
            int bookLabId = Convert.ToInt32(Request.QueryString["id"].ToString());
            int labId = Convert.ToInt32(Request.Cookies["labId"].Value.ToString());
            viewInvoice.InnerHtml = "<a href='BookingInvoice.aspx?bookingId=" + bookLabId + "&Id=0'  class='btn btn-color'> Invoice </a>";
            DataSet dsBookingDetails = objBookDetails.getBookingDetails(labId.ToString(), bookLabId.ToString());
            DataSet dsBookTestDetails = objBookDetails.getBookTestDetailslist(labId.ToString(), bookLabId.ToString());
            DataTable dtPayment = objBookDetails.GetPaymentDetails(bookLabId.ToString());

            string bookingStatus = "";
            string PrescriptionImg = "";

            if (dsBookingDetails != null)
            {
                if (dsBookingDetails.Tables[0].Rows.Count > 0)
                {
                    string aprovalstatus = dsBookingDetails.Tables[0].Rows[0]["sBookStatus"].ToString();
                    string aprovalteststatus = dsBookingDetails.Tables[0].Rows[0]["sTestStatus"].ToString();
                    if (aprovalstatus.ToLower() == "confirmed") aprovalstatus = "Booking Approved";
                    if (aprovalstatus.ToLower() == "awaiting") aprovalstatus = "Awaiting Confirmation";
                    if (aprovalstatus.ToLower() == "canceled") aprovalstatus = "Booking Cancel";
                    if (aprovalteststatus.ToLower() == "taken") aprovalstatus = "Test Taken";

                    if (aprovalteststatus.ToLower() == "taken")
                    {
                        A2.Visible = false;
                    }
                    spanBookingId.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sBookLabId"].ToString();
                    spanBookStatus.InnerHtml = aprovalstatus;

                    bookingStatus = dsBookingDetails.Tables[0].Rows[0]["sBookStatus"].ToString().ToLower();
                    PrescriptionImg = dsBookingDetails.Tables[0].Rows[0]["sUploadPrescriptionImg"].ToString().ToLower();
                    string imagpathpres = dsBookingDetails.Tables[0].Rows[0]["sUploadPrescriptionImg"].ToString().ToLower();
                    string finalimgpath = imagpathpres.Replace("prescription\\", "");
                    if (finalimgpath != "") prescriptionimg.Src = "http://visionarylifescience.com/images/prescription/" + finalimgpath.TrimStart(); else hideprescriptions.Style.Add("display", "none");
                    spanPatientName.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sPatient"].ToString();
                    spanSampleAddress.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["BookingAddress"].ToString();
                    spanBookRequestedAt.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sBookConfirmedAt"].ToString();
                    spanBookId.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sBookLabId"].ToString();
                    spanDoctorName.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sDoctor"].ToString();
                    spanBookMode.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sBookMode"].ToString();
                    //spanPayementStatus.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sPaymentStatus"].ToString();
                    spanAdvancePayment.InnerHtml = (dtPayment.Rows[0]["amount"].ToString() == "") ? "0" : dtPayment.Rows[0]["amount"].ToString();
                    string sfeesty = dsBookingDetails.Tables[0].Rows[0]["sFees"].ToString();
 hdnpmobile.Value = dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString();
                    if (sfeesty == "")
                    {
                    }
                    else
                    {
                        double fees = Convert.ToDouble(sfeesty);

                        double advancePaid = (dtPayment.Rows[0]["amount"].ToString() == "") ? 0 : Convert.ToDouble(dtPayment.Rows[0]["amount"].ToString());
                        double due = fees - advancePaid;
                        string PaymentStatus = "";
                        if (due == 0)
                        {
                            due = 0;
                            spanPayementStatus.InnerHtml = "Paid";
                            txtAdvancePaid.Text = "0";
                            txtAdvancePaid.Visible = false;
                            PaymentStatus = "Paid";
                            int result = objBookDetails.UpdatePaymentStatusbyBookingId(bookLabId.ToString(), PaymentStatus);
                        }
                        else
                        {
                            spanPayementStatus.InnerHtml = "Pending";
                            txtAdvancePaid.Text = due.ToString();
                            txtAdvancePaid.Visible = true;
                            PaymentStatus = "Pending";
                            int result = objBookDetails.UpdatePaymentStatusbyBookingId(bookLabId.ToString(), PaymentStatus);
                        }
                        spanPaymentDue.InnerHtml = due.ToString();
                    }
                    if (dsBookingDetails.Tables[0].Rows[0]["sPaymentStatus"].ToString().ToLower() == "paid" && dsBookingDetails.Tables[0].Rows[0]["sReportStatus"].ToString().ToLower() == "approved")
                    {
                        divPaymentDetailsEdit.Visible = false;
                    }
                    //  spanPaymentMode.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sPaymentMode"].ToString();
                    spanTestDate.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sTestDate"].ToString();
                    spanTimeSlot.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sTimeSlot"].ToString();
                    spanTotalFees.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sFees"].ToString();
                    spanTestStatus.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sTestStatus"].ToString();
                    if (dsBookingDetails.Tables[0].Rows[0]["sBookStatus"].ToString().ToLower() == "confirmed")
                    {
                        divApprovedOn.Style["display"] = "block";
                        // spanConfirmedAt.InnerHtml = DateTime.Parse(dsBookingDetails.Tables[0].Rows[0]["sBookConfirmedAt"].ToString()).ToString("dddd, dd MMMM yyyy HH:mm:ss tt");
                        spanConfirmedAt.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sBookConfirmedAt"].ToString();
                    }
                    else
                    {
                        divApprovedOn.Style["display"] = "none";
                    }
                    if (dsBookingDetails.Tables[0].Rows[0]["sBookStatus"].ToString().ToLower() == "canceled")
                    {
                        btnReject.Visible = false;
                    }
                    //Fill the editable fields with previous values
                    selTestStatus.SelectedIndex = (dsBookingDetails.Tables[0].Rows[0]["sTestStatus"].ToString().ToLower() == "pending") ? 0 : 1;
                    selPaymentStatus.Value = dsBookingDetails.Tables[0].Rows[0]["sPaymentStatus"].ToString();
                    // txtAdvancePaid.Text = (dtPayment.Rows[0]["amount"].ToString() == "") ? "0" : dtPayment.Rows[0]["amount"].ToString();
                }
            }

            if (dsBookTestDetails != null)
            {
                if (dsBookTestDetails.Tables[0].Rows.Count > 0)
                {
                    string tabContent = "";
                    foreach (DataRow row in dsBookTestDetails.Tables[0].Rows)
                    {
						  hdntestName.Value = row["sTestName"].ToString();
                        //Load book test details
                        tabContent += "<li class='table-row'>" +
                                           "<div class='col col-1 text-center' data-label='Sr. No.'>" + row["sTestCode"].ToString() + "</div>" +
                                           "<div class='col col-2 text-center' data-label='Patient Name'>" + row["sTestName"].ToString() + "</div>" +
                                           "<div class='col col-3 text-center' data-label='Doctor Name'>" + row["sTestDate"].ToString() + "</div>" +
                                           "<div class='col col-4 text-center fa-yellow' data-label='Booking Date'>" + row["sTestStatus"].ToString() + "</div>" +
                                           "<div class='col col-5 text-center' data-label='Appointment Date'>" + row["sPrice"].ToString() + "</div>" +
                                        "</li>";
                    }
                    tbodyBookTestDetails.Text = tabContent;
                }
                else
                {
                    tbodyBookTestDetails.Text = "<tr><td>No records found</td></tr>";
                }
            }

            if (bookingStatus.ToLower() == "confirmed")
            {
                divApproveReject.Style["display"] = "none";

                if (Request.Cookies["role"].Value.ToString().ToLower().Contains("owner") || Request.Cookies["role"].Value.ToString().ToLower().Contains("receptionist"))
                {
                    divPaymentDetailsEdit.Style["display"] = "block";
                }
                else
                {
                    divPaymentDetailsEdit.Style["display"] = "none";
                }
            }
            else
            {
                divPaymentDetailsEdit.Style["display"] = "none";

                if (Request.Cookies["role"].Value.ToString().ToLower().Contains("owner") || Request.Cookies["role"].Value.ToString().ToLower().Contains("receptionist"))
                {
                    divApproveReject.Style["display"] = "block";

                    if (PrescriptionImg == "" || PrescriptionImg == null)
                    {
                        btnPreTest.Style["display"] = "none";
                    }
                    else
                    {
                        loadMyTestList();
                        btnPreTest.Style["display"] = "inline-block";
                        btnApprove.Style["display"] = "none";
                    }
                }
                else
                {
                    divApproveReject.Style["display"] = "none";
                }
            }
            if (PrescriptionImg != "" || PrescriptionImg != null)
            {
            }
        }
        catch (Exception ex)
        {
            divPaymentDetailsEdit.Visible = false;
            A2.Visible = false;
            divApproveReject.Style["display"] = "none";
            btnReject.Visible = false;
            btnPreTest.Visible = false;
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');", true);
        }
    }
    protected void btnSchedule_Click(object sender, EventArgs e)
    {
        try
        {
            string TestDate = "";
            if (hiddenTestDate.Value != "")
            {
                TestDate = HDate.Value;
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
            if (Ival.IsTextBoxEmpty(hiddenAppointmentType.Value))
            {
                Msg += "● Please Select Appointment Type";
            }
            if (Ival.IsTextBoxEmpty(hiddenTimeSlot.Value))
            {
                Msg += "● Please Select Time Slot";
            }
            if (!Ival.IsValidDate(HDate.Value))
            {
                Msg += "● Please Enter Valid Test Date";
            }
            if (Msg.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');location.reload();", true);
            }
            else
            {
                int bookLabId = Convert.ToInt32(Request.QueryString["id"].ToString());
                string testDate = HDate.Value;
                string TimeSlot = hiddenTimeSlot.Value;
                string AppoinmentType = hiddenAppointmentType.Value;
                int updatePayment = objBookDetails.ResheduleTestDate(bookLabId.ToString(), testDate, TimeSlot, AppoinmentType);
                if (updatePayment == 1)
                {
                    DataSet dsBookingDetails = objBookDetails.getBookTestDetailsUserMobId(bookLabId.ToString());
                    string UserId = dsBookingDetails.Tables[0].Rows[0]["sappuserid"].ToString();
                    string deviceToken = dsBookingDetails.Tables[0].Rows[0]["sdeviceToken"].ToString();
                    string labname = dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString();
                    string testtime = dsBookingDetails.Tables[0].Rows[0]["sTimeSlot"].ToString();
                    string testdate = dsBookingDetails.Tables[0].Rows[0]["stestdate"].ToString();
                    string Type = "Booking";
                    dynamic _Result = new JObject();
                    _Result.BookingId = bookLabId;
                    string _payload = JsonConvert.SerializeObject(_Result);
                    if (AppoinmentType.ToLower() == "clinic")
                    {
							newWhatsapp wa = new newWhatsapp();
						 string mob = dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString();
                        string mobileNo = CryptoHelper.Decrypt(mob).ToString();
                     
                        ////wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
                        string LabId = Request.Cookies["labId"].Value.ToString();
                        wa.sendWhatsappMsg("+91" + mobileNo, "Booking Reshedule for Lab", spanPatientName.InnerHtml + ',' + hdntestName.Value + ',' + testtime + ',' + testdate + ',' + labname, LabId);


                        string Message = "Your test booking request at " + labname + " has been reshedule. Please visit the lab at " + testtime + " on " + testdate + ". ";
                        ObjFCM.SendNotification("Test Booking Status", Message, deviceToken, Type, bookLabId.ToString());
                        int _result = objAppNotify.AppNotification(UserId, "Booking", Message, Type, _payload, Request.Cookies["labUserId"].Value.ToString());
                    }
                    else
                    {
						newWhatsapp wa = new newWhatsapp();
						 string mob = dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString();
                        string mobileNo = CryptoHelper.Decrypt(mob).ToString();
                       
                        ////wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
                        string LabId = Request.Cookies["labId"].Value.ToString();
                        wa.sendWhatsappMsg("+91" + mobileNo, "Booking Reschedule Home for Lab", spanPatientName.InnerHtml + ',' + hdntestName.Value + ',' + testtime + ',' + testdate + ',' + labname, LabId);
                        
                        string Message = "Your test booking request at " + labname + " has been reshedule. The lab technician will visit your home at " + testtime + " on " + testdate + ". ";
                        ObjFCM.SendNotification("Test Booking Status", Message, deviceToken, Type, bookLabId.ToString());
                        int _result = objAppNotify.AppNotification(UserId, "Booking", Message, Type, _payload, Request.Cookies["labUserId"].Value.ToString());
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.reload();", true);
                }
                else if (updatePayment == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            CLSViewReportValuesinTemplate objTemplate = new CLSViewReportValuesinTemplate();
            int bookLabId = Convert.ToInt32(Request.QueryString["id"].ToString());
            string status = hiddenBookStatus.Value;
            string comment = txtComment.Text;
            int confirmRejectBooking = objBookDetails.updateBookingStatus(bookLabId.ToString(), status, comment);
            string bookLabTestId = Request.QueryString["id"].ToString();
            DataSet dsTestReport = objTemplate.getTestReportgetbyid(bookLabTestId);
            string AppointmentType = dsTestReport.Tables[0].Rows[0]["sAppointmentType"].ToString();
            DataSet dsBookingDetails = objBookDetails.getBookTestDetailsUserMobId(bookLabId.ToString());
            string deviceToken = dsBookingDetails.Tables[0].Rows[0]["sdeviceToken"].ToString();
            string labname = dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString();
            string testtime = dsBookingDetails.Tables[0].Rows[0]["sTimeSlot"].ToString();
            string testdate = dsBookingDetails.Tables[0].Rows[0]["stestdate"].ToString();
            string Type = "Booking";
            dynamic _Result = new JObject();
            _Result.BookingId = bookLabTestId;
            string _payload = JsonConvert.SerializeObject(_Result);
            if (confirmRejectBooking == 1)
            {
                if (status.ToLower() != "canceled")
                {
                    if (AppointmentType.ToLower() == "clinic")
                    {
						
						 // string mob = dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString();
                      //  string mobileNo = CryptoHelper.Decrypt(mob).ToString();
					//	newWhatsapp wa = new newWhatsapp();
                        ////wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
                     //   string LabId = Request.Cookies["labId"].Value.ToString();
                     //  wa.sendWhatsappMsg("+91" + mobileNo, "Booking Cancel For Lab", spanPatientName.InnerHtml + ',' + testtime + ',' + testdate + ',' + labname, LabId);
                    
                        // Calling For Notification Yogesh 
                        string Message = "Your test booking request at " + labname + " has been accepted. Please visit the lab at " + testtime + " on " + testdate + ". ";
                        ObjFCM.SendNotification("Test Booking Status", Message, deviceToken, Type, bookLabId.ToString());
                        int _result = objAppNotify.AppNotification(dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString(), "Booking", Message, Type, _payload, Request.Cookies["labUserId"].Value.ToString());
						
                        string script = "{ sendnotification('" + deviceToken + "', '" + Message + "'); };";
                        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "updatePanel1Script", script, true);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "sendnotification()", true);
                    }
                    else
                    {
						  string mob = dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString();
                        string mobileNo = CryptoHelper.Decrypt(mob).ToString();
						newWhatsapp wa = new newWhatsapp();
                        ////wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
                        string LabId = Request.Cookies["labId"].Value.ToString();
                        wa.sendWhatsappMsg("+91" + mobileNo, "Booking Cancel For Lab", spanPatientName.InnerHtml + ',' + testtime + ',' + testdate + ',' + labname, LabId);
                   
                        string Message = "Your test booking request at " + labname + " has been accepted. The lab technician will visit your home at " + testtime + " on " + testdate + ". ";
                        ObjFCM.SendNotification("Test Booking Status", Message, deviceToken, Type, bookLabId.ToString());
                        int _result = objAppNotify.AppNotification(dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString(), "Booking", Message, Type, _payload, Request.Cookies["labUserId"].Value.ToString());

                        string script = "{ sendnotification('" + deviceToken + "', '" + Message + "'); };";
                        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "updatePanel1Script", script, true);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "sendnotification()", true);
                    }
                }
                else
                {
					 string mob = dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString();
                        string mobileNo = CryptoHelper.Decrypt(mob).ToString();
						newWhatsapp wa = new newWhatsapp();
                        ////wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
                        string LabId = Request.Cookies["labId"].Value.ToString();
                        wa.sendWhatsappMsg("+91" + mobileNo, "Booking Cancel For Lab", spanPatientName.InnerHtml + ',' + testtime + ',' + testdate + ',' + labname, LabId);
                 
                    string Message = "Your test booking request at " + labname + ". has been rejected. Kindly book again or contact the Lab.";
                    ObjFCM.SendNotification("Test Booking Status", Message, deviceToken, Type, bookLabId.ToString());
                    int _result = objAppNotify.AppNotification(dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString(), "Booking", Message, Type, _payload, Request.Cookies["labUserId"].Value.ToString());
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.href='TestBookList.aspx';", true);
            }
            else if (confirmRejectBooking == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
    protected void btnUpdatePaymentDetails_Click(object sender, EventArgs e)
    {
        try
        {
            int bookLabId = Convert.ToInt32(Request.QueryString["id"].ToString());
            string testStatus = selTestStatus.Value;
            string paymentStatus = selPaymentStatus.Value;
            string advancePaid = txtAdvancePaid.Text;
            string paymentMethod = ddlPaymentMethod.SelectedItem.Text;

            int updatePayment = objBookDetails.updatePaymentStatus(bookLabId.ToString(), testStatus, paymentStatus, advancePaid);
            int result = 0;
            if (paymentStatus != "Not paid")
            {
                result = objBookDetails.AddPaymentDetails(bookLabId.ToString(), advancePaid, paymentMethod, Createdby);
            }
            if (result == 1)
            {
				 string patientName = spanPatientName.InnerText;
                string mob = hdnpmobile.Value;
                string mobileNo = CryptoHelper.Decrypt(mob).ToString();
                string labname = db.getData("select sLabName from labMaster where sLabId='" + Request.Cookies["labId"].Value.ToString() + "'");
                newWhatsapp wa = new newWhatsapp();
                //wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
                string LabId = Request.Cookies["labId"].Value.ToString();
                wa.sendWhatsappMsg("+91" + mobileNo, "Lab Payment For Lab", patientName + ',' + txtAdvancePaid.Text + ',' + labname, LabId);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.href='CreateReport.aspx';", true);
            }
            else if (updatePayment == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occurred');location.reload();", true);
            }
            else if (updatePayment == 1)
            {
				 string patientName = spanPatientName.InnerText;
                string mob = hdnpmobile.Value;
                string mobileNo = CryptoHelper.Decrypt(mob).ToString();
                string labname = db.getData("select sLabName from labMaster where sLabId='" + Request.Cookies["labId"].Value.ToString() + "'");
                newWhatsapp wa = new newWhatsapp();
                //wa.sendWhatsappMsg("+919702903233", "OTP", "Ram,1234");
                string LabId = Request.Cookies["labId"].Value.ToString();
                wa.sendWhatsappMsg("+91" + mobileNo, "Lab Payment For Lab", patientName + ',' + txtAdvancePaid.Text + ',' + labname, LabId);
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Updated successfully');location.href='CreateReport.aspx';", true);
            }
        }
        catch (Exception Ex)
        {
            LogError.LoggerCatch(Ex);
            Session.Clear();
            Response.Cookies["loggedIn"].Expires = DateTime.Now.AddDays(-1);
            Response.Redirect("LabLogin.aspx");
        }
    }
    protected void loadMyTestList()
    {
        try
        {
            DataSet ds = objTestList.getMyTests(Request.Cookies["labId"].Value.ToString());

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
                                           "<td scope='col'><input class='radio-custom' type='checkbox' value='" + row["sTestId"].ToString() + "|" + row["sPrice"].ToString() + "|" + row["sTestCode"].ToString() + "' id='chkTest'  name='chkTest' clientidmode='Static' ><label class='radio-custom-label'></label></td>" +
                                           "<td scope='col'>" + row["sTestCode"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sTestName"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sProfileName"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sSectionName"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sPrice"].ToString() + "</td>" +
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
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
    protected void btnIMGprescription_Click(object sender, EventArgs e)
    {
        try
        {
            string Msg = "";
            if (!Ival.IsInteger(spanBookingId.InnerHtml))
            {
                Msg += "● Please enter valid booking id";
            }
            if (!Ival.IsInteger(hiddenTotalFees.Value))
            {
                Msg += "● Please enter valid amount";
            }
            if (Ival.IsTextBoxEmpty(hiddenTestList.Value))
            {
                Msg += "● Please Select Valid Test";
            }
            if (Msg.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + Msg + "');location.reload();", true);
            }
            else
            {
                string BookLabId = spanBookingId.InnerHtml;
                string bookStatus = "Confirmed";
                string bookMode = "Prescription";
                string Amount = hiddenTotalFees.Value;
                tolfees = Convert.ToInt32(spanTotalFees.InnerHtml);
                int newfees = tolfees + Convert.ToInt32(hiddenTotalFees.Value);
                string fees = Convert.ToString(newfees);
                string testId = hiddenTestList.Value;
                string TestPrice = hTestPricearray.Value;
                int bookTest = objBookTest.bookTestFromPrescription(BookLabId, bookStatus, bookMode, fees, testId, TestPrice);
                if (bookTest == 1)
                {
                    loadMyTestList();
                    // Calling For Notification Yogesh 
                    int bookLabId = Convert.ToInt32(Request.QueryString["id"].ToString());
                    DataSet dsBookingDetails = objBookDetails.getBookTestDetailsUserMobId(bookLabId.ToString());
                    string deviceToken = dsBookingDetails.Tables[0].Rows[0]["sdeviceToken"].ToString();
                    string labname = dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString();
                    string testtime = dsBookingDetails.Tables[0].Rows[0]["sTimeSlot"].ToString();
                    string testdate = dsBookingDetails.Tables[0].Rows[0]["stestdate"].ToString();
                    CLSViewReportValuesinTemplate objTemplate = new CLSViewReportValuesinTemplate();
                    string bookLabTestId = Request.QueryString["id"].ToString();
                    DataSet dsTestReport = objTemplate.getTestReportgetbyid(bookLabTestId);
                    string AppointmentType = dsTestReport.Tables[0].Rows[0]["sAppointmentType"].ToString();
                    string Type = "Booking";
                    dynamic _Result = new JObject();
                    _Result.BookingId = bookLabTestId;
                    string _payload = JsonConvert.SerializeObject(_Result);
                    if (AppointmentType.ToLower() == "clinic")
                    {
                        string Message = "Your test booking request at " + labname + " has been accepted. Please visit the lab at " + testtime + " on " + testdate + ". ";

                        ObjFCM.SendNotification("Test Booking Status", Message, deviceToken, Type, bookLabId.ToString());
                        int _result = objAppNotify.AppNotification(dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString(), "Booking", Message, Type, _payload, Request.Cookies["labUserId"].Value.ToString());

                        string script = "{ sendnotification('" + deviceToken + "', '" + Message + "'); };";
                        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "updatePanel1Script", script, true);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "sendnotification()", true);
                    }
                    else
                    {
                        string Message = "Your test booking request at " + labname + " has been accepted. The lab technician will visit your home at " + testtime + " on " + testdate + ". ";
                        string script = "{ sendnotification('" + deviceToken + "', '" + Message + "'); };";
                        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "updatePanel1Script", script, true);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "sendnotification()", true);
                        ObjFCM.SendNotification("Test Booking Status", Message, deviceToken, Type, bookLabId.ToString());
                        int _result = objAppNotify.AppNotification(dsTestReport.Tables[0].Rows[0]["sPatientId"].ToString(), "Booking", Message, Type, _payload, Request.Cookies["labUserId"].Value.ToString());
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.reload();", true);
                }
                else if (bookTest == 0)
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
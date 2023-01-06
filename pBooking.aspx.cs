using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using Validation;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class pBooking : System.Web.UI.Page
{
    ClsPatientList objPatient = new ClsPatientList();
    ClsDoctorList objDoctors = new ClsDoctorList();
    ClsLabCalendar objLabCalendar = new ClsLabCalendar();
    ClsTestList objTestList = new ClsTestList();
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    ClsBookTest objBookTest = new ClsBookTest();
    int total = 0;
    string price = "0";
    DataTable dt_testpriceandid = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
           
            DoctorList.Visible = false;
            testList.Visible = false;
            appointment.Visible = false;
            divReview.Visible = false;
            loadMyPatientsList();
            loadMyDoctorsList();
            loadMyCalendar();
            loadMyTestList();
            string CurruntDate = DateTime.Now.ToString("dd/MM/yyyy");
            hiddenTestDate.Value = CurruntDate;
            txtdate.Text = hiddenTestDate.Value;
            txtdate.ReadOnly = true;
            lblTestDate.InnerText = hiddenTestDate.Value;
            lnkdoctor.Enabled = false;
            lnktest.Enabled = false;
            lnkapp.Enabled = false;
            btn_reviewBooking.Visible = false;
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
                gridviewTest.DataSource = ds;
                gridviewTest.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');", true);
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
                string country = "India";
                string state = txtState.SelectedItem.Text;
                string city = txtCity.Text;
                string pincode = txtPincode.Text;
                string action = "0";
                string appUserId = hiddenAppUserId.Value;
                string birthDate = txtBirthDate.Text;
                string MsgTemplate = "";
                int addPatient = objPatient.addPatient(action, appUserId, labId, fullname, emailId, mobile, gender, birthDate, address, country, state, city, pincode);

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
                string action = "0";
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
                    birthDate = txtBirthDate1.Text;
                }
                int addDoctor = objDoctors.addDoctor(action, appUserId, labId, fullname, emailId, mobile, gender, birthDate, address, degree, specialization, clinic, country, state, city, pincode);

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
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
    protected void chktestselect_CheckedChanged(object sender, EventArgs e)
    {


        dt_testpriceandid.Columns.AddRange(new DataColumn[3] { new DataColumn("sTestId"), new DataColumn("sTestName"), new DataColumn("sPrice") });
        foreach (GridViewRow row in gridviewTest.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chktestselect") as CheckBox);
                if (chkRow.Checked)
                {
                    string testId= (row.Cells[1].FindControl("lbltestId") as Label).Text;
                    string testName = (row.Cells[2].FindControl("lbltest") as Label).Text;
                     price = (row.Cells[3].FindControl("lblpr") as Label).Text;
                     total += int.Parse(price);
                    dt_testpriceandid.Rows.Add(testId, testName, price);
                   
                       
                }
            }
        }
        gridfinalTest.DataSource = dt_testpriceandid;
        gridfinalTest.DataBind();
        gridrbook.DataSource = dt_testpriceandid;
        gridrbook.DataBind();
        Session["TestIdandPrice"] = dt_testpriceandid;
        
        hdntestamount.Value = total.ToString();
        lblFinalAmount.InnerText = hdntestamount.Value.ToString();
        HFinalAmount.Value = lblFinalAmount.InnerText;
        //  Decimal total = dt.AsEnumerable().Sum(row => row.Field<Decimal>("sPrice"));
        gridfinalTest.FooterRow.Cells[1].Text = "Total";
        gridfinalTest.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
        gridfinalTest.FooterRow.Cells[2].Text = total.ToString();
        lnkapp.Enabled = true;

    }
    protected void loadMyPatientsList()
    {
        try
        {
            DataSet ds = objPatient.getPatients(Request.Cookies["labId"].Value.ToString());
            if (ds != null)
            {
                gridpatientDetails.DataSource = ds;
                gridpatientDetails.DataBind();
                Session["patientList"] = ds;
            }
            else
            {
                //tbodyPatientList.Text = "<tr><td>No records found</td></tr>";
            }
        }

        catch (Exception ex)
        {

        }
    }
    protected void rbtselectpatient_CheckedChanged(object sender, EventArgs e)
    {
        // Patient List
        foreach (GridViewRow row in gridpatientDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                RadioButton rbtbuttonpat = (row.Cells[0].FindControl("rbtselectpatient") as RadioButton);
                if (rbtbuttonpat.Checked)
                {
                    lblpatid.Text = (row.Cells[1].FindControl("lblpatientId") as Label).Text;
                    lblpatname.Text = (row.Cells[2].FindControl("lblpname") as Label).Text;
                    string patmobileNo = (row.Cells[3].FindControl("lblpmobile") as Label).Text;
                    lblpatgender.Text = (row.Cells[4].FindControl("lblpgender") as Label).Text;
                    lblpatmobile.Text = CryptoHelper.Decrypt(patmobileNo);
                    lblPatientName.InnerText = lblpatname.Text;
                    lblPatientGender.InnerText = lblpatgender.Text;
                    lblPatientMobile.InnerText = lblpatmobile.Text;
                }
            }
        }
        lnkdoctor.Enabled = true;
    }
    protected void loadMyDoctorsList()
    {
        try
        {
            DataSet ds = objDoctors.getDoctors(Request.Cookies["labId"].Value.ToString());
            if (ds != null)
            {
                griddoctorList.DataSource = ds;
                griddoctorList.DataBind();
                Session["DoctorList"] = ds;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void rbtselectDoctor_CheckedChanged(object sender, EventArgs e)
    {
        // Doctor List
        foreach (GridViewRow row in griddoctorList.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                RadioButton rbtbuttondoc = (row.Cells[0].FindControl("rbtselectDoctor") as RadioButton);
                if (rbtbuttondoc.Checked)
                {
                    lbldid.Text = (row.Cells[1].FindControl("lbldoctorId") as Label).Text;
                    lbldName.Text = (row.Cells[2].FindControl("lbldocname") as Label).Text;
                    string docmobileNo = (row.Cells[3].FindControl("lbldmob") as Label).Text;
                    lbldgender.Text = (row.Cells[4].FindControl("lbldgender") as Label).Text;
                    lbldmobile.Text = CryptoHelper.Decrypt(docmobileNo);
                    lblDoctorName.InnerText = lbldName.Text;
                    HiddenDoctorid.Value = lbldid.Text;
                }
            }
        }
       
        lnktest.Enabled = true;
       
    }
    protected void loadMyCalendar()
    {
        try
        {
            DataSet ds = objLabCalendar.getLabSlots(Request.Cookies["labId"].Value.ToString());
            if (ds != null)
            {
                gridappointment.DataSource = ds;
                gridappointment.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkpatient_Click(object sender, EventArgs e)
    {
        divReview.Visible = false;
        patientList.Visible = true;
        DoctorList.Visible = false;
        testList.Visible = false;
        appointment.Visible = false;
    }
    protected void lnkdoctor_Click(object sender, EventArgs e)
    {
        divReview.Visible = false;
        patientList.Visible = false;
        DoctorList.Visible = true;
        testList.Visible = false;
        appointment.Visible = false;
    }
    protected void lnktest_Click(object sender, EventArgs e)
    {
        divReview.Visible = false;
        patientList.Visible = false;
        DoctorList.Visible = false;
        testList.Visible = true;
        appointment.Visible = false;
    }
    protected void lnkapp_Click(object sender, EventArgs e)
    {
        divReview.Visible = false;
        patientList.Visible = false;
        DoctorList.Visible = false;
        testList.Visible = false;
        appointment.Visible = true;
    }
    protected void txtCollectionCharge_TextChanged(object sender, EventArgs e)
    {
        string finalamt = Math.Round(float.Parse(hdntestamount.Value) + float.Parse(txtCollectionCharge.Text)).ToString();
        lblFinalAmount.InnerText = finalamt.ToString();
        HFinalAmount.Value = lblFinalAmount.InnerText;
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

          
            DataTable dt_PatnerDetails = new DataTable();

            dt_PatnerDetails = (DataTable)Session["TestIdandPrice"];
            for (int i = 0; i < dt_PatnerDetails.Rows.Count; i++)
                {
                    hdntestId.Value += dt_PatnerDetails.Rows[i].Field<string>("sTestId") + ',';
                    hdntestPrice.Value += dt_PatnerDetails.Rows[i].Field<string>("sPrice") + ',';

                }
                string labId = Request.Cookies["labId"].Value.ToString();
                string patientId = lblpatid.Text;
                string AppointmentType = hiddenAppointmentType.Value;
                string doctorId = HiddenDoctorid.Value;
                string timeSlot = hiddenTimeSlot.Value;
                string bookStatus = "Confirmed";
                string testStatus = "Pending";
                string bookMode = "Manual";
                string testDate = TestDate;
                string fees = HFinalAmount.Value;
                string testId = hdntestId.Value;
                string TestPrice = hdntestPrice.Value;
              
                int bookTest = objBookTest.bookTest(labId, patientId, doctorId, timeSlot,
                  bookStatus, testStatus, bookMode, testDate, fees, hdntestId.Value, AppointmentType, hdntestPrice.Value);
                if (bookTest == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.href='TestBookList.aspx';", true);
                }
                else if (bookTest == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error occured');", true);
                }
            
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');", true);
        }
    }
  
    public DataTable testSearch(int Labid, string testName)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetMyTestsForBooking_gridsearch " + "'" + Labid.ToString() + "',  '" + testName + "' ");
            return dt;
        }
        catch (Exception ex)
        {
            dt = null;
            return dt;
        }
    }
    public DataTable patientSearch(string paname, int Labid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetPatients_gridSearch " + "'" + paname + "',  '" + Labid.ToString() + "' ");
            return dt;
        }
        catch (Exception ex)
        {
            dt = null;
            return dt;
        }
    }
    public DataTable DoctorSearch(string docname, int Labid)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GetDoctors_GridSearch " + "'" + docname + "',  '" + Labid.ToString() + "' ");
            return dt;
        }
        catch (Exception ex)
        {
            dt = null;
            return dt;
        }
    }
   
    protected void txtpatientSearch_TextChanged(object sender, EventArgs e)
    {
        int labId =int.Parse(Request.Cookies["LabId"].Value.ToString());
        string paname = txtpatientSearch.Text;
        gridpatientDetails.DataSource = patientSearch(paname, labId);
        gridpatientDetails.DataBind();
    }
    protected void txtdoctorSearch_TextChanged(object sender, EventArgs e)
    {
        int labId = int.Parse(Request.Cookies["LabId"].Value.ToString());
        string docname = txtdoctorSearch.Text;
        griddoctorList.DataSource = DoctorSearch(docname, labId);
        griddoctorList.DataBind();
    }
    protected void btn_reviewBooking_Click(object sender, EventArgs e)
    {
        divReview.Visible = true;
        patientList.Visible = false;
        DoctorList.Visible = false;
        testList.Visible = false;
        appointment.Visible = false;
    }
    protected void rbtselectCalender_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gridappointment.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                RadioButton rbtbuttondoc = (row.Cells[0].FindControl("rbtselectCalender") as RadioButton);
                if (rbtbuttondoc.Checked)
                {
                    lblTimeSlot.InnerText = (row.Cells[2].FindControl("lblfrom") as Label).Text;
                    hiddenTimeSlot.Value = lblTimeSlot.InnerText;
                    hiddenAppointmentType.Value = (row.Cells[4].FindControl("lbltype") as Label).Text;
                   // lblTimeSlot.InnerText = lbldName.Text;
                }
            }
        }
        btn_reviewBooking.Visible = true;
    }

    //protected void gridpatientDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.DataItem != null)
    //    {
    //        DataRowView dr = (DataRowView)e.Row.DataItem;
    //        string mobNo = (dr["sMobile"].ToString());
    //        System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
    //        e.Row.Cells[3].Text = CryptoHelper.Decrypt(mobNo);

    //    }
    //}
    protected void btnnext_Click(object sender, EventArgs e)
    {
        if (lblpatname.Text != "")
        {
            lnkdoctor.Enabled = true;
        }
        if (lbldName.Text != "")
        {
            lnktest.Enabled = true;
        }
        DataTable dt_PatnerDetails = new DataTable();

        dt_PatnerDetails = (DataTable)Session["TestIdandPrice"];
        if (dt_PatnerDetails != null)
        {
            lnkapp.Enabled = true;
        }
    }
    protected void txtsearchtestName_TextChanged(object sender, EventArgs e)
    {
        int labId = int.Parse(Request.Cookies["LabId"].Value.ToString());
        string testName = txtsearchtestName.Text;
        gridviewTest.DataSource = testSearch(labId, testName);
        gridviewTest.DataBind();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class BookingInvoice : System.Web.UI.Page
{
    ClsBookDetails objBookDetails = new ClsBookDetails();
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        loadBookingDetails();
        string bookLabId1 = Request.QueryString["bookingId"].ToString();
        btnback.HRef = "BookDetails.aspx?id=" + bookLabId1;
        showLogo();
       
    }
    void showLogo()
    {
        string labId = Request.Cookies["labId"].Value.ToString();
        string fileName = db.getData("select slablogo from labMaster where sLabId='" + labId + "'");
        Image1.Visible = fileName != "0";
        if (fileName != "0")
        {
            Image1.ImageUrl = "~/images/" + fileName;

        }
    }
    protected void loadBookingDetails()
    {
        try
        {
            int bookLabId = Convert.ToInt32(Request.QueryString["bookingId"].ToString());
            string labId = Request.Cookies["labId"].Value.ToString();
            int Id = Convert.ToInt32(Request.QueryString["Id"].ToString());
          
            DataSet dsBookTestDetails = objBookDetails.getBookTestDetailslist(labId, bookLabId.ToString());
            DataSet ds = objBookDetails.getBookTestDetailsByBookiIdandPaymentId(Id, bookLabId.ToString());
                DataSet dsBookingDetails = new DataSet();
             dsBookingDetails = objBookDetails.getInvoiceBookingDetails(bookLabId.ToString(),labId);

             if (dsBookingDetails != null)
             {
                 if (dsBookingDetails.Tables[0].Rows.Count > 0)
                 {
                     string sfeesty = dsBookingDetails.Tables[0].Rows[0]["sFees"].ToString();

                     if (sfeesty == "")
                     {
                     }
                     else
                     {
                         int fees = Convert.ToInt32(sfeesty);

                         int advancePaid = (ds.Tables[1].Rows[0]["amount"].ToString() == "") ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["amount"].ToString());
                         int due = fees - advancePaid;
                         if (due == 0)
                         {
                             PaidAmount.InnerHtml = fees.ToString();
                             due = 0;
                             BalanceAmount.InnerHtml = "0";
                         }
                         else
                         {
                             PaidAmount.InnerHtml = advancePaid.ToString();
                             BalanceAmount.InnerHtml = due.ToString();
                         }
                     }

                    // spanLabName.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString();
                     spanLabAddress.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sLabAddress"].ToString();
                     spanLabEmail.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sLabEmailId"].ToString() != "" ? CryptoHelper.Decrypt(dsBookingDetails.Tables[0].Rows[0]["sLabEmailId"].ToString()) : "";
                     spanLabContact.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(dsBookingDetails.Tables[0].Rows[0]["sLabContact"].ToString()) : "";
                     spanLabDoctor.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["DoctorName"].ToString();
                     spanLabDoctorDegree.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sDegree"].ToString();
                     spanPatientName.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["patientName"].ToString();
                     spanPatientAddress.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sAddress"].ToString();
                     spanPatientContactNumber.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString() != "" ? CryptoHelper.Decrypt(dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString()) : "";

                     PaymentMode.InnerHtml = ds.Tables[0].Rows[0]["PaymentMethod"].ToString();
                     TotalAmount.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sFees"].ToString();
                     PaidAmount.InnerHtml = ds.Tables[0].Rows[0]["amount"].ToString();
                     PaymentReceiveDate.InnerHtml = ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                     spanLabInvoiceNo.InnerHtml = ds.Tables[0].Rows[0]["Id"].ToString();
                 }

                 else
                 {
                     dsBookingDetails = objBookDetails.getInvoiceBookingDetailswithoutdoctor(bookLabId.ToString(),labId);
                     if (dsBookingDetails != null)
                     {
                         if (dsBookingDetails.Tables[0].Rows.Count > 0)
                         {
                             string sfeesty = dsBookingDetails.Tables[0].Rows[0]["sFees"].ToString();

                             if (sfeesty == "")
                             {
                             }
                             else
                             {
                                 int fees = Convert.ToInt32(sfeesty);

                                 int advancePaid = (ds.Tables[1].Rows[0]["amount"].ToString() == "") ? 0 : Convert.ToInt32(ds.Tables[1].Rows[0]["amount"].ToString());
                                 int due = fees - advancePaid;
                                 if (due == 0)
                                 {
                                     PaidAmount.InnerHtml = fees.ToString();
                                     due = 0;
                                     BalanceAmount.InnerHtml = "0";
                                 }
                                 else
                                 {
                                     PaidAmount.InnerHtml = advancePaid.ToString();
                                     BalanceAmount.InnerHtml = due.ToString();
                                 }
                             }

                           //  spanLabName.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sLabName"].ToString();
                             spanLabAddress.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sLabAddress"].ToString();
                             spanLabEmail.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sLabEmailId"].ToString() != "" ? CryptoHelper.Decrypt(dsBookingDetails.Tables[0].Rows[0]["sLabEmailId"].ToString()) : "";
                             spanLabContact.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(dsBookingDetails.Tables[0].Rows[0]["sLabContact"].ToString()) : "";
                             spanLabDoctor.InnerHtml = "";
                             spanLabDoctorDegree.InnerHtml = "";
                             spanPatientName.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["patientName"].ToString();
                             spanPatientAddress.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sAddress"].ToString();
                             spanPatientContactNumber.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString() != "" ? CryptoHelper.Decrypt(dsBookingDetails.Tables[0].Rows[0]["sMobile"].ToString()) : "";

                             PaymentMode.InnerHtml = ds.Tables[0].Rows[0]["PaymentMethod"].ToString();
                             TotalAmount.InnerHtml = dsBookingDetails.Tables[0].Rows[0]["sFees"].ToString();
                          
                             PaymentReceiveDate.InnerHtml = ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                             spanLabInvoiceNo.InnerHtml = ds.Tables[0].Rows[0]["Id"].ToString();
                         }
                     }
                 }
             }

            if (dsBookTestDetails != null)
            {
                if (dsBookTestDetails.Tables[0].Rows.Count > 0)
                {
                    string tabContent = "";
                    int count = 0;
                    foreach (DataRow row in dsBookTestDetails.Tables[0].Rows)
                    {
                        count = count + 1;
                        //Load book test details
                        tabContent += "<tr'>" +
                                           "<td class='center' data-label='Sr. No.'>" + count + "</td>" +
                                           "<td class='left strong' data-label='Patient Name'>" + row["sTestCode"].ToString() + "</td>" +
                                           "<td class='left strong' data-label='Doctor Name'>" + row["sTestName"].ToString() + "</td>" +
                                           "<td class='right' data-label='Booking Date'>" + row["sTestStatus"].ToString() + "</td>" +
                                           "<td class='right' data-label='Appointment Date'>" + row["sPrice"].ToString() + "</td>" +
                                        "</tr>";
                    }
                    tbodyTestDetails.InnerHtml = tabContent;
                }
                else
                {
                    tbodyTestDetails.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "location.href='BookDetails.aspx?id=" + Request.QueryString["bookingId"].ToString() + "';", true);
        }
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        btnprint.Visible = false;
        btnback.Visible = false;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);
        
    }
}
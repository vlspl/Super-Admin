using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PatientInvoiceHistory : System.Web.UI.Page
{
    ClsPatientList objPatient = new ClsPatientList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            hdnpid.Value = Request.QueryString["id"].ToString();
            loadMyPatientsInvoiceHistory();
        }
    }

    protected void loadMyPatientsInvoiceHistory()
    {

        try
        {
            //int PatientId=Convert.ToInt32(Request.QueryString["Id"].ToString());
            string LabId = Request.Cookies["labId"].Value.ToString();
            //spanPatientName.InnerHtml = Request.QueryString["Name"].ToString();
            DataTable ds = objPatient.getPatientsInvoiceHistory(LabId, hdnpid.Value.ToString());

            if (ds != null)
            {
                if (ds.Rows.Count > 0)
                {
                    string tabMyPatientList = "";
              
                    foreach (DataRow row in ds.Rows)
                    {
                        //Load lab patient listhref='BookingInvoice.aspx?bookingId=" + bookLabId + "&Id=0'
                        tabMyPatientList += "<tr>" +
                                           "<td >" + row["rn"].ToString() + "</td>" +
                                           "<td  >" + row["sBookLabId"].ToString() + "</td>" +
                                           "<td >" + row["Total"].ToString() + "</td>" +
                                           "<td >" + row["Amount"].ToString() + "</td>" +
                                           "<td >" + row["PaymentMethod"].ToString() + "</td>" +
                                           "<td >" + row["Date"].ToString() + "</td>" +
                                           "<td class='fa-color' data-label='View' ><a href='BookingInvoice.aspx?Id=" + row["Id"].ToString() + "&bookingId=" + row["sBookLabId"].ToString() + "' class='btn btn-sm btn-color'>View</a></td>" +
                                       "</tr>";

                    }
                    tbodyPatientInvoiceHistory.InnerHtml = tabMyPatientList;
                }
                else
                {
                    tbodyPatientInvoiceHistory.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
}
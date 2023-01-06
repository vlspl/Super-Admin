using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TestBookList : System.Web.UI.Page
{
    ClsTestBookList objTestBook = new ClsTestBookList();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
                    loadMyTestBookings();
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
    protected void loadMyTestBookings()
    {
        try
        {
            DataSet ds = objTestBook.getMyBookings(Request.Cookies["labId"].Value.ToString());

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabMyTestBookList = "";

                    int count = 0;

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        count = count + 1;
                        //Load lab test book list
                        tabMyTestBookList += "<tr>" +
                                           "<td scope='col'>" + count + "</td>" +
                                           "<td scope='col'>" + row["sPatient"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sDoctor"].ToString() + "</td>" +
                                           "<td scope='col' id='slotdate' class='slotdate '><span class='pr-''><i class='fas fa-calendar-week pr-1 text-gray'></i></span>" + row["bookslotdate"].ToString() + "</td>" +
                                           "<td scope='col'><span class='pr-''><i class='fas fa-calendar-week pr-1 text-gray'></i></span>" + row["sTestDate"].ToString() + " &nbsp;&nbsp; " + row["sTimeSlot"].ToString() + " </td>" +
                                           "<td scope='col'><i class='fa fa-inr' aria-hidden='true' ></i> " + row["sFees"].ToString() + "</td>";
                        if (row["sBookMode"].ToString().ToLower() == "manual")
                        {
                            tabMyTestBookList += "<td scope='col'><span class='hidden'></span><i class='fa fa-user fa-lg' aria-hidden='true' ></i></td>";
                        }
                        else if (row["sBookMode"].ToString().ToLower() == "mobile" || row["sBookMode"].ToString().ToLower() == "prescription")
                        {
                            tabMyTestBookList += "<td scope='col'><span class='hidden'></span> <i class='fa fa-mobile fa-lg' aria-hidden='true'></i></td>";
                        }
                        string color = "";
                        if (row["sBookStatus"].ToString().ToLower() == "confirmed")
                        {
                            color = "#2E86C1";
                        }
                        if (row["sBookStatus"].ToString().ToLower() == "awaiting")
                        {
                            color = "#f2a30f";
                        }
                        if (row["sBookStatus"].ToString().ToLower() == "canceled")
                        {
                            color = "#f51105";
                        }
                       
                        tabMyTestBookList += "<td scope='col'  '><span style='color:" + color + ";font-weight: bold'>" + row["sBookStatus"].ToString() + "</span></td>" +
                                         "<td scope='col' '>" + row["sAppointmentType"].ToString() + "</td>" +
                                             "<td scope='col'><a href='BookDetails.aspx?id=" + row["sBookLabId"].ToString() + "' >   View </a></td>" +
                                          "</tr>";
                    }
                    tbodyTestBookList.InnerHtml = tabMyTestBookList;
                }
                else
                {
                    tbodyTestBookList.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
}
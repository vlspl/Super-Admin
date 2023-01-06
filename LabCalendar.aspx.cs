using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class LabCalendar : System.Web.UI.Page
{
    ClsLabCalendar objLabCal = new ClsLabCalendar();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
                    loadLabSlots();
                    //loadLabLeaves();
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
    protected void loadLabSlots()
    {
        try
        {
            DataSet ds = objLabCal.getLabSlots(Request.Cookies["labId"].Value.ToString());

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabContent = "";
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //Load slot Details
                        tabContent +=
                                         "<tr>" +
                                           "<td scope='col'>" + row["sSlotId"].ToString() + "</td>" +
                                         "<td scope='col'>" + row["sDay"].ToString() + "</td>" +
                                          "<td scope='col'>" + row["sFrom"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sTo"].ToString() + "</td>" +
                                            "<td scope='col'>" + row["sAppointmentType"].ToString() + "</td>" +
                                            
                                              "<td scope='col'><a href='javascript:void(0)' class='HideEditbtn' id='" + row["sSlotId"].ToString() + "' onclick='javascript:removeTimeSlot(this)'><i class='fa fa-trash' aria-hidden='true'></i></a></td>" +
                                           "</tr>";

                            
                            
                         
                    }
                    tbodyLabSlots.InnerHtml = tabContent;
                }
                else
                {
                    tbodyLabSlots.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
            else
            {
                tbodyLabSlots.InnerHtml = "<tr><td>No records found</td></tr>";
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void loadLabLeaves()
    {
        /*DataSet ds = objLabCal.getLabLeaves(Request.Cookies["labId"].Value.ToString());

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tabContent = "<tr>" +
                                    "<th scope='col'>Date</th>" +
                                 "</tr>";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //Load slot Details
                    tabContent += "<tr>" +
                                       "<td scope='col'>" + row["sDate"].ToString() + "</td>" +
                                    "</tr>";

                    //Bind details to editable fields
                }

                tbodyLabLeaves.InnerHtml = tabContent;
            }
            else
            {
                tbodyLabLeaves.InnerHtml = "<tr><td>No records found</td></tr>";
            }
        }
        else
        {
            tbodyLabLeaves.InnerHtml = "<tr><td>No records found</td></tr>";
        }*/
    }
    protected void btnSubmitSlots_Click(object sender, EventArgs e)
    {
        try{
        string[] slots = hiddenSlots.Value.ToString().Split('@');
        string labId = Request.Cookies["labId"].Value.ToString();
        string sAppointmentType;

        if (atclinic.Checked)
            sAppointmentType = "Clinic";
        else
            sAppointmentType = "Home";
        foreach (string slot in slots)
        {
            if (slot != "")
            {
                string day = slot.Split('|')[0];
                string from = slot.Split('|')[1];
                string to = slot.Split('|')[2];

                if (objLabCal.addSlot(labId, day, from, to, sAppointmentType) == 1)
                {

                }
                else if (objLabCal.addSlot(labId, day, from, to, sAppointmentType) == 0)
                {

                }
            }
        }
            Response.Redirect("LabCalendar.aspx",false);
        }
        catch(Exception ex)
        {
            Response.Redirect("Error.htm");
        }
    }
    [WebMethod]
    public static string removeSlot(string slotId)
    {
        ClsLabCalendar objLabCal = new ClsLabCalendar();
        int removeLabSlot = objLabCal.deleteLabSlot(slotId);

        return removeLabSlot.ToString();
    }
}

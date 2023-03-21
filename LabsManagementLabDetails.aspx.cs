using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using CrossPlatformAESEncryption.Helper;


public partial class SuperAdmin_LabsManagementLabDetails : System.Web.UI.Page
{
    CLSLabsManagementLabDetails objLabsManagementLabDetails = new CLSLabsManagementLabDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!IsPostBack)
            {
                loadMyPatientsList();
                loadMyDoctorsList();
                loadMyTestList();
                loadLabDetails();
                loadLabContactDetails();
                loadLabSlots();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    protected void loadMyPatientsList()
    {

        string labid = Request.QueryString["id"].ToString();
        DataSet ds = objLabsManagementLabDetails.getPatients(labid);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tabMyPatientList = "";
               
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string _Mobile = row["smobile"].ToString() != "" ? CryptoHelper.Decrypt(row["smobile"].ToString()) : "";
                    //Load lab patient list
                    tabMyPatientList += "<tr>" +
                                       "<td scope='col'>" + row["sAppUserId"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sfullname"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sgender"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sbirthdate"].ToString() + "</td>";
                    if (row["regtype"].ToString() == "Manual")
                    {
                        tabMyPatientList += "<td scope='col'><i class='fa fa-user fa-lg' aria-hidden='true'></i></td>";
                    }
                    else
                    {
                        tabMyPatientList += "<td scope='col'><i class='fa fa-mobile fa-lg' aria-hidden='true'></i></td>";
                    }
                    tabMyPatientList += "<td scope='col'>" + _Mobile + "</td>" +
                        //  "<td scope='col' style='display:none'>" + row["sApprovalStatus"].ToString() + "</td>" +
                    "<td scope='col'><a href='PatientManagementProfile.aspx?id=" + labid + "&AppUserId=" + row["sAppUserId"].ToString() + "' class='lab-btn-secondary'>Profile</a></td>" +
                    "<td scope='col'><a href='PatientManagementHealthProfile.aspx?id=" + labid + "&AppUserId=" + row["sAppUserId"].ToString() + "' class='lab-btn-secondary'>Test List</a></td>" +
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


    protected void loadMyDoctorsList()
    {
        string labid = Request.QueryString["id"].ToString();
        DataSet ds = objLabsManagementLabDetails.getDoctors(labid);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tabMyDoctorsList = "";

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string _Mobile = row["smobile"].ToString() != "" ? CryptoHelper.Decrypt(row["smobile"].ToString()) : "";
                   
                    //Load lab Doctor list
                    tabMyDoctorsList += "<tr>" +
                                       "<td scope='col'>" + row["sAppUserId"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sFullName"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sGender"].ToString() + "</td>" +
                                       "<td scope='col'>" + _Mobile + "</td>" +
                                       "<td scope='col'>" + row["sAddress"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sDegree"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sSpecialization"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sClinic"].ToString() + "</td>" +
                                    "</tr>";

                }

                tbodyDoctorList.InnerHtml = tabMyDoctorsList;
            }
            else
            {
                tbodyDoctorList.InnerHtml = "<tr><td>No records found</td></tr>";
            }
        }
    }


    protected void loadMyTestList()
    {
        string labid = Request.QueryString["id"].ToString();
        DataSet ds = objLabsManagementLabDetails.getMyTests(labid);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //string tabMyTestList = "<tr>"+
                //                       "<th>Test Code</th>"+
                //                       "<th>Test Name</th>"+
                //                       "<th>Profile</th>"+
                //                       "<th>Section</th>"+
                //                       "<th>Price (Rs)</th>"+
                //                       "<th></th>"+
                //                    "</tr>";

                string tabMyTestList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;
                    tabMyTestList += "<tr>" +
                                     "<td scope='col'>" + count + "</td>" +
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


    public void loadLabDetails()
    {
        string labid = Request.QueryString["id"].ToString();
        DataSet ds = objLabsManagementLabDetails.getLabDetails(labid);

        if (ds != null)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                //Load Details
                spanLabId.InnerText = row["sLabId"].ToString();
                spanLabName.InnerText = row["sLabName"].ToString();
                spanLogo.InnerHtml = "<img title='" + row["sLabLogo"].ToString() + "' src='https://qa-sa.visionarylifescience.com/images/" + row["sLabLogo"].ToString() + "' id='" + row["sLabLogo"].ToString() + "' height='50px' width='50px'/>";
                spanLabDetails.InnerText = row["sLabDetails"].ToString();

                //Bind details to editable fields
                //logoDiv.InnerHtml = "<img title='" + row["sLabLogo"].ToString() + "' src='temp/" + row["sLabLogo"].ToString() + "' id='" + row["sLabLogo"].ToString() + "' height='50px' width='50px'/>";                
            }
        }
    }


    protected void loadLabContactDetails()
    {
        string labid = Request.QueryString["id"].ToString();
        DataSet ds = objLabsManagementLabDetails.getLabDetails(labid);
        if (ds != null)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string _Mobile = row["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabContact"].ToString()) : "";
                string _email = row["sLabEmailId"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabEmailId"].ToString()) : "";
                      
                //Load Details
                spanLabEmail.InnerText = _email;
                spanLabContact.InnerText = _Mobile;
                spanLabAddress.InnerText = row["sLabAddress"].ToString();
            }
        }
    }

    protected void loadLabSlots()
    {
        string labid = Request.QueryString["id"].ToString();
        DataSet ds = objLabsManagementLabDetails.getLabSlots(labid);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tabContent = "<tr>" +
                                    "<th scope='col'>Day</th>" +
                                    "<th scope='col'>From</th>" +
                                    "<th scope='col'>To</th>" +
                                    "<th scope='col'></th>" +
                                 "</tr>";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //Load slot Details
                    tabContent += "<tr id='rowSlot" + row["sSlotId"].ToString() + "'>" +
                                       "<td scope='col'>" + row["sDay"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sFrom"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["sTo"].ToString() + "</td>" +
                                    "</tr>";

                    //Bind details to editable fields
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


}
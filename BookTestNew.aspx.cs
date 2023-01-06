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
using System.Web.UI.WebControls;

public partial class BookTestNew : System.Web.UI.Page
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
    DataTable dt_new = new DataTable();
    DataTable dt_newDoc = new DataTable();
    DataRow dtrow;
    DataRow dtrowDoc;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
               
                if (!IsPostBack)
                {
                    string CurruntDate = DateTime.Now.ToString("dd/MM/yyyy");
                    loadMyPatientsList();
                    loadMyDoctorsList();
                   
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
            lblMessage.Text = "Error occured";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
    protected void loadMyPatientsList()
    {
        try
        {
            DataSet ds_patientList = objPatient.getPatients(Request.Cookies["labId"].Value.ToString());
            if (ds_patientList != null)
            {
                if (ds_patientList != null)
                {
                    DataTable dt_getP_list = ds_patientList.Tables[0];

                    dt_new.Columns.Add("sAppUserId");
                    dt_new.Columns.Add("sFullName");
                    dt_new.Columns.Add("sGender");
                    dt_new.Columns.Add("sMobile");


                    foreach (DataRow dr in dt_getP_list.Rows)
                    {
                        dt_new.Rows.Add(dr["sAppUserId"].ToString(), dr["sFullName"].ToString(), dr["sGender"].ToString(), CryptoHelper.Decrypt(dr["sMobile"].ToString()));
                        gvpatientListDetails.DataSource = dt_new;
                        gvpatientListDetails.DataBind();
                        Session["qrDetails"] = dt_new;
                    }


                }
                else
                {
                    gvpatientListDetails.EmptyDataText = "Patient List Not Showing..";
                }

            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            lblMessage.Text = "Error occured While Loading Patient List ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
    protected void RadioButton_select_CheckedChanged(object sender, EventArgs e)
    {
        DataTable getHwDetails = new DataTable();
        getHwDetails = (DataTable)Session["qrDetails"];
        int rowId; string filename = string.Empty;
        RadioButton chksel;
        for (int i = 0; i <= getHwDetails.Rows.Count - 1; i++)
        {
            chksel = (gvpatientListDetails.Rows[i].Cells[0].FindControl("RadioButton_select") as RadioButton);
            if (chksel.Checked)
            {
                lblid.Text = getHwDetails.Rows[i].Field<string>("sAppUserId");
                lblname.Text = getHwDetails.Rows[i].Field<string>("sFullName");
                lblgenger.Text = getHwDetails.Rows[i].Field<string>("sGender");
                lblmobile.Text = getHwDetails.Rows[i].Field<string>("sMobile");


                //objdb.insert("delete from Temp_Import where temp_ImportId='" + rowId + "' ");



            }
            //lblMessage.Text = "Lot Detail Deleted Successfully.";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }

    protected void RadioButton_selDoc_CheckedChanged(object sender, EventArgs e)
    {
        DataTable getHwDetails = new DataTable();
        getHwDetails = (DataTable)Session["DoctorList"];
        int rowId; string filename = string.Empty;
        RadioButton chkseldoc;
        for (int i = 0; i <= getHwDetails.Rows.Count - 1; i++)
        {
            chkseldoc = (gvpatientListDetails.Rows[i].Cells[0].FindControl("RadioButton_selDoc") as RadioButton);
            if (chkseldoc.Checked)
            {
                lblid.Text = getHwDetails.Rows[i].Field<string>("sAppUserId");
                lblname.Text = getHwDetails.Rows[i].Field<string>("sFullName");
                lblgenger.Text = getHwDetails.Rows[i].Field<string>("sGender");
                lblmobile.Text = getHwDetails.Rows[i].Field<string>("sMobile");


                //objdb.insert("delete from Temp_Import where temp_ImportId='" + rowId + "' ");



            }
            //lblMessage.Text = "Lot Detail Deleted Successfully.";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    protected void gvpatientListDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvpatientListDetails.PageIndex = e.NewPageIndex;
        loadMyPatientsList();
    }
    protected void loadMyDoctorsList()
    {
        try
        {
            DataSet ds_doctor = objDoctors.getDoctors(Request.Cookies["labId"].Value.ToString());
            if (ds_doctor != null)
            {
                if (ds_doctor != null)
                {
                    DataTable dt_getD_list = ds_doctor.Tables[0];
                 
                    dt_newDoc.Columns.Add("sAppUserId");
                    dt_newDoc.Columns.Add("sFullName");
                    dt_newDoc.Columns.Add("sGender");
                    dt_newDoc.Columns.Add("sMobile");
                     dt_newDoc.Columns.Add("sAddress");
                     dt_newDoc.Columns.Add("sdegree");
                     dt_newDoc.Columns.Add("sSpecialization");
                     dt_newDoc.Columns.Add("sClinic");


                     foreach (DataRow drDoc in dt_getD_list.Rows)
                    {
                        dt_newDoc.Rows.Add(drDoc["sAppUserId"].ToString(), drDoc["sFullName"].ToString(), drDoc["sGender"].ToString(), CryptoHelper.Decrypt(drDoc["sMobile"].ToString()), drDoc["sAddress"].ToString(), drDoc["sdegree"].ToString(), drDoc["sSpecialization"].ToString(), drDoc["sClinic"].ToString());
                        gridDoctorList.DataSource = dt_newDoc;
                        gridDoctorList.DataBind();
                        Session["DoctorList"] = dt_newDoc;
                    }


                }
                else
                {
                    gvpatientListDetails.EmptyDataText = "Patient List Not Showing..";
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            lblMessage.Text = "Error occured While Loading Doctor List ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }
   
}
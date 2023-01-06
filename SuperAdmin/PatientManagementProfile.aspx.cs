using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PatientManagementProfile : System.Web.UI.Page
{
    CLSPatientManagementProfile objPatientManagementProfile = new CLSPatientManagementProfile();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                getLabData();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    public void getLabData()
    {
        int appuserid = Convert.ToInt32(Request.QueryString["AppUserId"].ToString());
        DataSet ds = objPatientManagementProfile.GetUserProfile(appuserid);

        if (ds.Tables[0].Rows.Count > 0)
        {
            AppID.Text = ds.Tables[0].Rows[0]["sAppUserId"].ToString();
            Name.Text = ds.Tables[0].Rows[0]["sFullName"].ToString();
            contactdetails.Text = ds.Tables[0].Rows[0]["sMobile"].ToString();
            Emailid.Text = ds.Tables[0].Rows[0]["sEmailId"].ToString();
            gender.Text = ds.Tables[0].Rows[0]["sGender"].ToString();
            dateofbirth.Text = ds.Tables[0].Rows[0]["sBirthDate"].ToString();
            Address.Text = ds.Tables[0].Rows[0]["sAddress"].ToString() + " " + ds.Tables[0].Rows[0]["scity"].ToString() + " " + ds.Tables[0].Rows[0]["spincode"].ToString();
        }
    }



}

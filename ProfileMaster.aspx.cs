using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class ProfileMaster : System.Web.UI.Page
{
    ClsProfileMaster objProfile = new ClsProfileMaster();
    ClsSectionMaster objSection = new ClsSectionMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try{
        if (Request.Cookies["loggedIn"] != null)
        {
            if (!Page.IsPostBack)
            {
                DataSet ds = objSection.getSection();

                if (ds != null)
                {
                    selSection.Items.Clear();

                    //for editing profile
                    selEditSection.Items.Clear();

                    selSection.Items.Add(new ListItem("Custom Section", "94"));

                    //for editing profile
                    selEditSection.Items.Add(new ListItem("Select Section", ""));

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        selSection.Items.Add(new ListItem(dr["sSectionName"].ToString(), dr["sSectionId"].ToString()));

                        //for editing profile
                        selEditSection.Items.Add(new ListItem(dr["sSectionName"].ToString(), dr["sSectionId"].ToString()));
                    }
                }

                loadTestProfile();
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
    protected void loadTestProfile()
    {
        try{
        DataSet ds = objProfile.getTestProfile();

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tabContent = "";/* "<tr>" +
                                    "<th scope='col'>Profile</th>" +
                                    "<th scope='col'>Section</th>" +
                                    "<th scope='col'></th>" +
                                 "</tr>"*/;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //Load sections
                    tabContent +=
                                     "<tr>" +
                                           "<td scope='col'>" + row["sTestProfileId"].ToString() + "</td>" +
                                         "<td scope='col'>" + row["sProfileName"].ToString() + "</td>" +
                                          "<td scope='col'>" + row["sSectionName"].ToString() + "</td>" +

                                              "<td scope='col'><a  class='HideEditbtn' id='" + row["sTestProfileId"].ToString() + "' onclick='javascript:removeProfile(this)'><i class='fa fa-trash fa-color' aria-hidden='true'></i></a></td>" +
                                           "</tr>";
                        
                      
                }

                tbodyProfile.InnerHtml = tabContent;
            }
            else
            {
                tbodyProfile.InnerHtml = "<tr><td>No records found</td></tr>";
            }
        }
        else
        {
            tbodyProfile.InnerHtml = "<tr><td>No records found</td></tr>";
        }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnAddProfile_click(object sender, EventArgs e)
    { 
        try{
        int addProfile = objProfile.addProfile(txtProfileName.Text.ToString(), selSection.SelectedValue.ToString());

        if (addProfile == 1)
        {
            lblMessage.Text = "Profile Added Successfully";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "location.reload(true);", true);
        }
        else if (addProfile == 2)
        {
            lblMessage.Text = "Profile Already Exists";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Profile already exists');location.reload(true);", true);
        }
        else if (addProfile == 0)
        {
            lblMessage.Text = "Error occurred while Adding Profile";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnUpdateProfile_click(object sender, EventArgs e)
    {
        try{
        string sectionId = selEditSection.SelectedValue;
        string testProfileId = hiddenProfileId.Value;
        string profileName = txtEditProfileName.Text;

        int updateProfile = objProfile.updateProfile(testProfileId, sectionId, profileName);

        if (updateProfile == 1)
        {
            lblMessage.Text = "Profile Update Successfully";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Updated sucessfully');location.reload(true);", true);
        }
        else if (updateProfile == 0)
        {
            lblMessage.Text = "Error occurred while Updating Profile";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }

    [WebMethod]
    public static string removeProfile(string profileId)
    {
        ClsProfileMaster objProfile = new ClsProfileMaster();
        int removeProfile = objProfile.deleteProfile(profileId);

        return removeProfile.ToString();
    }
}
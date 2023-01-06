using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Collections;

public partial class TestMaster : System.Web.UI.Page
{
    ClsSectionMaster objSection = new ClsSectionMaster();
    ClsProfileMaster objProfile = new ClsProfileMaster();
    ClsAnalyteMaster objAnalyte = new ClsAnalyteMaster();
    ClsTestMaster objTestMater = new ClsTestMaster();

    ClsSpecimenMaster objSpecimen = new ClsSpecimenMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
                    //load existing sections
                    loadSection();

                    //load existing profiles
                    loadProfile();

                    //load existing analytes
                    loadAnalyteList();

                    //load existing specimen list in popup
                    loadSpecimen();
                }
            }
            else
            {
                Response.Redirect("LabLogin.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(@"lablogin.aspx", false);
        }
    }

    protected void loadSection()
    {
        try
        {
            DataSet ds = objSection.getSection();

            if (ds != null)
            {
                selSection.Items.Clear();
                selSection.Items.Add(new ListItem("Custom Section", "94"));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    selSection.Items.Add(new ListItem(dr["sSectionName"].ToString(), dr["sSectionId"].ToString()));

                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            Response.Redirect(@"LabLogin.aspx", false);
        }
    }

    protected void loadProfile()
    {
        try
        {
            DataSet ds = objProfile.getTestProfile();

            if (ds != null)
            {
                selProfile.Items.Clear();
                selProfile.Items.Add(new ListItem("Custom Profile", "94|305"));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    selProfile.Items.Add(new ListItem(dr["sProfileName"].ToString(), dr["sSectionId"].ToString() + "|" + dr["sTestProfileId"].ToString()));
                }
            }

            int count = 0;
            foreach (ListItem lst in selProfile.Items)
            {
                count++;
                if (count == 1)
                    continue;
                lst.Attributes.Add("style", "display:none");
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            Response.Redirect(@"LabLogin.aspx", false);
        }
    }

    protected void loadAnalyteList()
    {
        try
        {
            DataSet ds = objAnalyte.getAnalyte();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabContent = "";
                    int srno = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //Load sections
                        srno = srno + 1;

                        tabContent += "<tr id='rowAnalyte" + row["sAnalyteId"].ToString() + "'>" +
                                           "<td scope='col'><input type='checkbox' value='" + row["sAnalyteId"].ToString() + "' id='chkAnalyte" + row["sAnalyteId"].ToString() + "' text='" + row["sAnalyteName"].ToString() + "' name='chkAnalyte' clientidmode='Static' onchange='javascript:analyteSelected(this)' ></td>" +
                          
                                           "<td scope='col'>" + srno + "</td>" +
                                           "<td scope='col'>" + row["sAnalyteName"].ToString() + "</td>" +
                                        "</tr>";
                    }
                    tbodyAnalyteList.InnerHtml = tabContent;
                }
                else
                {
                    tbodyAnalyteList.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
            else
            {
                tbodyAnalyteList.InnerHtml = "<tr><td>No records found</td></tr>";
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            Response.Redirect(@"LabLogin.aspx", false);
        }
    }
    protected void loadSpecimen()
    {
        try
        {
            DataSet ds = objSpecimen.getSpecimen();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabRolesList = "";

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //Load sections
                        tabRolesList += "<option value='" + row["sSampleType"].ToString() + "'>";
                    }
                    browsers.InnerHtml = tabRolesList;
                }
                else
                {
                    browsers.InnerHtml = "<option value='no record'>";
                }
            }
            else
            {
                browsers.InnerHtml = "<option value='no record'>";
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            Response.Redirect(@"LabLogin.aspx", false);
        }
    }
    [WebMethod]
    public static string addSection(string sectionName)
    {
        ClsTestMaster objTestMaster = new ClsTestMaster();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        Dictionary<string, object> response = objTestMaster.addSection(sectionName);

        return serializer.Serialize(response);
    }
    [WebMethod]
    public static string addProfile(string profileName, string sectionId)
    {
        ClsTestMaster objTestMaster = new ClsTestMaster();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        Dictionary<string, object> response = objTestMaster.addProfile(profileName, sectionId);

        return serializer.Serialize(response);
    }
    [WebMethod]
    public static string addAnalyte(string analyteName)
    {
        ClsTestMaster objTestMaster = new ClsTestMaster();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        Dictionary<string, object> response = objTestMaster.addAnalyte(analyteName);

        return serializer.Serialize(response);
    }
    [WebMethod]
    public static string loadSubAnalyte(string analyteIds)
    {
        ClsTestMaster objTestMaster = new ClsTestMaster();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        Dictionary<string, object> response = objTestMaster.loadSubAnalyte(analyteIds);

        return serializer.Serialize(response);
    }
    [WebMethod]
    public static string addSubAnalyte(string subAnalyteName, string analyteId)
    {
        ClsTestMaster objTestMaster = new ClsTestMaster();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        Dictionary<string, object> response = objTestMaster.addSubAnalyte(subAnalyteName, analyteId);

        return serializer.Serialize(response);
    }
    [WebMethod]
    public static string addSpecimenMethod(string sampleType, string quantity, string timePeriod, string method)
    {
        ClsTestMaster objTestMaster = new ClsTestMaster();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        Dictionary<string, object> response = objTestMaster.addSpecimenMethod(sampleType, quantity, timePeriod, method);

        return serializer.Serialize(response);
    }
    protected void btnCreateTest_Click(object sender, EventArgs e)
    {
        try
        {
            string sectionId = selSection.SelectedValue;
            string profileId = hiddenProfileId.Value;//selProfile.SelectedValue.Split('|')[1];
            string testCode = txtTestCode.Text;
            string testName = txtTestName.Text;
            string testUsefulFor = txtTestUsefulFor.Text;
            string testInterpretation = txtTestInterpretation.Text;
            string testLimitation = txtTestLimitation.Text;
            string testClinicalReferences = txtTestClinicalReferences.Text;

            string[] hdnAnalyteId = hiddenAnalyteId.Value.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            hdnAnalyteId = (hdnAnalyteId.Length == 0) ? null : hdnAnalyteId;

            string[] hdnSubAnalyteId = hiddenSubAnalyteId.Value.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            hdnSubAnalyteId = (hdnSubAnalyteId.Length == 0) ? null : hdnSubAnalyteId;

            string[] hdnAnalyteIdName = hiddenAnalyteIdName.Value.Split('$').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            hdnAnalyteIdName = (hdnAnalyteIdName.Length == 0) ? null : hdnAnalyteIdName;

            string[] hdnSubAnalyteIdName = hiddenSubAnalyteIdName.Value.Split('$').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            hdnSubAnalyteIdName = (hdnSubAnalyteIdName.Length == 0) ? null : hdnSubAnalyteIdName;

            string hdnAnalyteSMR = (hiddenAnalyteSMR.Value == "") ? null : hiddenAnalyteSMR.Value;
            string hdnSubAnalyteSMR = (hiddenSubAnalyteSMR.Value == "") ? null : hiddenSubAnalyteSMR.Value;
            string hdnAnalyteSMRRefVal = (hiddenAnalyteSMRRefVal.Value == "") ? null : hiddenAnalyteSMRRefVal.Value;
            string hdnSubAnalyteSMRRefVal = (hiddenSubAnalyteSMRRefVal.Value == "") ? null : hiddenSubAnalyteSMRRefVal.Value;
            string labId = Request.Cookies["labId"].Value.ToString();

            List<Dictionary<string, object>> jsonAnalyteSMR = null;
            List<Dictionary<string, object>> jsonSubAnalyteSMR = null;
            List<Dictionary<string, object>> jsonAnalyteSMRRefVal = null;
            List<Dictionary<string, object>> jsonSubAnalyteSMRRefVal = null;

            if (hdnAnalyteSMR != null)
            {
                jsonAnalyteSMR = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(hdnAnalyteSMR);
            }

            if (hdnSubAnalyteSMR != null)
            {
                jsonSubAnalyteSMR = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(hdnSubAnalyteSMR);
            }

            if (hdnAnalyteSMRRefVal != null)
            {
                jsonAnalyteSMRRefVal = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(hdnAnalyteSMRRefVal);
            }

            if (hdnSubAnalyteSMRRefVal != null)
            {
                jsonSubAnalyteSMRRefVal = new JavaScriptSerializer().Deserialize<List<Dictionary<string, object>>>(hdnSubAnalyteSMRRefVal);
            }
            string Customid = "";
            if (Request.Cookies["labUserId"].ToString() == "1")
            {
                Customid = "0";
            }
            else
            {
                Customid = "1";
            }
            Dictionary<string, string> createTest = objTestMater.createTest(labId, profileId, testCode, testName, testUsefulFor, testInterpretation, testLimitation, testClinicalReferences, hdnAnalyteId, hdnSubAnalyteId, jsonAnalyteSMR, jsonSubAnalyteSMR, jsonAnalyteSMRRefVal, jsonSubAnalyteSMRRefVal, Customid);

            if (createTest["key"] == "1")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Test created'),location.reload(true);", true);
            }
            else
            {
               // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            Response.Redirect(@"LabLogin.aspx", false);
        }
    }
}
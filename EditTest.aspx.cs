using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class EditTest : System.Web.UI.Page
{
    ClsEditTest objEditTest = new ClsEditTest();
    ClsSectionMaster objSection = new ClsSectionMaster();
    ClsProfileMaster objProfile = new ClsProfileMaster();

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
                    loadTestDetails();
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

    protected void loadTestDetails()
    {
        try
        {
            int testId = Convert.ToInt32(Request.QueryString["id"].ToString());
            hiddenTestId.Value = testId.ToString();
            string labId = Request.Cookies["labId"].Value.ToString();
            DataSet dsTest = objEditTest.getTest(labId, testId.ToString());
            DataSet dsTestAnalyte = objEditTest.getTestAnalyte(testId.ToString());
            DataSet dsTestSubAnalyte = objEditTest.getTestSubAnalyte(testId.ToString());

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            List<Dictionary<string, object>> lstAnalyteSMRRefVal = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictAnalyteSMR;
            Dictionary<string, string> dictAnalyteSMRRefVal;
            List<Dictionary<string, string>> lstAnalyteRefVal = new List<Dictionary<string, string>>();

            List<Dictionary<string, object>> lstSubAnalyteSMRRefVal = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictSubAnalyteSMR;
            Dictionary<string, string> dictSubAnalyteSMRRefVal;
            List<Dictionary<string, string>> lstSubAnalyteRefVal = new List<Dictionary<string, string>>();

            // Get Test Details
            if (dsTest != null)
            {
                if (dsTest.Tables[0].Rows.Count > 0)
                {
                    txtTestCode.Text = dsTest.Tables[0].Rows[0]["sTestCode"].ToString();
                    txtTestName.Text = dsTest.Tables[0].Rows[0]["sTestName"].ToString();
                    txtTestUsefulFor.Text = dsTest.Tables[0].Rows[0]["sTestUsefulFor"].ToString();
                    txtTestInterpretation.Text = dsTest.Tables[0].Rows[0]["sTestInterpretation"].ToString();
                    txtTestLimitation.Text = dsTest.Tables[0].Rows[0]["sTestLimitation"].ToString();
                    txtTestClinicalReferences.Text = dsTest.Tables[0].Rows[0]["sTestClinicalReferance"].ToString();

                    foreach (ListItem section in selSection.Items)
                    {
                        if (section.Text == dsTest.Tables[0].Rows[0]["sSectionName"].ToString())
                        {
                            selSection.SelectedValue = section.Value;
                            break;
                        }
                    }

                    foreach (ListItem profile in selProfile.Items)
                    {
                        if (profile.Text == dsTest.Tables[0].Rows[0]["sProfileName"].ToString())
                        {
                            selProfile.SelectedValue = profile.Value;
                        }

                        if (profile.Value.Split('|')[0] == selSection.SelectedValue)
                        {
                            profile.Attributes.Remove("style");
                        }
                    }
                }
            }

            // Get Test Analyte Details
            if (dsTestAnalyte != null)
            {
                if (dsTestAnalyte.Tables[0].Rows.Count > 0)
                {
                    List<string> lstASM = new List<string>();

                    //load Analytes drop down to select analyte for adding specimen method 
                    selAnalyte.Items.Clear();
                    selAnalyte.Items.Add(new ListItem("Select Analyte", ""));

                    foreach (DataRow row in dsTestAnalyte.Tables[0].Rows)
                    {
                        string asm = row["sAnalyteId"].ToString() + "-" + row["sSpecimenId"].ToString() + "-" + row["sMethodId"].ToString();

                        if (lstASM.Contains(asm) == false)
                        {
                            lstASM.Add(asm);

                            dictAnalyteSMR = new Dictionary<string, object>();
                            dictAnalyteSMR.Add("testAnalyteId", row["sTestAnalyteId"].ToString());
                            dictAnalyteSMR.Add("analyteId", row["sAnalyteId"].ToString());
                            dictAnalyteSMR.Add("analyteName", row["sAnalyteName"].ToString());
                            dictAnalyteSMR.Add("tasmId", row["sTASMId"].ToString());
                            dictAnalyteSMR.Add("specimenId", row["sSpecimenId"].ToString());
                            dictAnalyteSMR.Add("specimenName", row["sSampleType"].ToString());
                            dictAnalyteSMR.Add("quantity", row["sQuantity"].ToString());
                            dictAnalyteSMR.Add("timePeriod", row["sTimePeriod"].ToString());
                            dictAnalyteSMR.Add("methodId", row["sMethodId"].ToString());
                            dictAnalyteSMR.Add("methodName", row["sMethodName"].ToString());
                            dictAnalyteSMR.Add("resultType", row["sResultType"].ToString());
                            dictAnalyteSMR.Add("refVal", new List<Dictionary<string, string>>());

                            lstAnalyteSMRRefVal.Add(dictAnalyteSMR);

                            dictAnalyteSMRRefVal = new Dictionary<string, string>();
                            dictAnalyteSMRRefVal.Add("tarId", row["sTestAnalyteReferenceId"].ToString());
                            dictAnalyteSMRRefVal.Add("refType", row["sReferenceType"].ToString());
                            dictAnalyteSMRRefVal.Add("age", row["sAge"].ToString());
                            dictAnalyteSMRRefVal.Add("male", row["sMale"].ToString());
                            dictAnalyteSMRRefVal.Add("female", row["sFemale"].ToString());
                            dictAnalyteSMRRefVal.Add("grade", row["sGrade"].ToString());
                            dictAnalyteSMRRefVal.Add("units", row["sUnits"].ToString());
                            dictAnalyteSMRRefVal.Add("interpretation", row["sInterpretation"].ToString());
                            dictAnalyteSMRRefVal.Add("lowerLimit", row["sLowerLimit"].ToString());
                            dictAnalyteSMRRefVal.Add("upperLimit", row["sUpperLimit"].ToString());

                            lstAnalyteRefVal = new List<Dictionary<string, string>>();
                            lstAnalyteRefVal.Add(dictAnalyteSMRRefVal);

                            foreach (var record in lstAnalyteSMRRefVal)
                            {
                                if (record["tasmId"].ToString() == row["sTASMId"].ToString())
                                {
                                    record["refVal"] = lstAnalyteRefVal;
                                }
                            }
                        }
                        else
                        {
                            dictAnalyteSMRRefVal = new Dictionary<string, string>();
                            dictAnalyteSMRRefVal.Add("tarId", row["sTestAnalyteReferenceId"].ToString());
                            dictAnalyteSMRRefVal.Add("refType", row["sReferenceType"].ToString());
                            dictAnalyteSMRRefVal.Add("age", row["sAge"].ToString());
                            dictAnalyteSMRRefVal.Add("male", row["sMale"].ToString());
                            dictAnalyteSMRRefVal.Add("female", row["sFemale"].ToString());
                            dictAnalyteSMRRefVal.Add("grade", row["sGrade"].ToString());
                            dictAnalyteSMRRefVal.Add("units", row["sUnits"].ToString());
                            dictAnalyteSMRRefVal.Add("interpretation", row["sInterpretation"].ToString());
                            dictAnalyteSMRRefVal.Add("lowerLimit", row["sLowerLimit"].ToString());
                            dictAnalyteSMRRefVal.Add("upperLimit", row["sUpperLimit"].ToString());

                            lstAnalyteRefVal.Add(dictAnalyteSMRRefVal);
                            foreach (var record in lstAnalyteSMRRefVal)
                            {
                                if (record["tasmId"].ToString() == row["sTASMId"].ToString())
                                {
                                    record["refVal"] = lstAnalyteRefVal;
                                }
                            }
                        }
                        //load Analytes drop down to select analyte for adding specimen method 
                        if (selAnalyte.Items.Contains(new ListItem(row["sAnalyteName"].ToString(), row["sAnalyteId"].ToString())) == false)
                            selAnalyte.Items.Add(new ListItem(row["sAnalyteName"].ToString(), row["sAnalyteId"].ToString()));
                    }
                }
                else
                {
                    //tbodyTestAnalyteSubAnalyte.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }

            hiddenAnalyteSMRRefVal.Value = serializer.Serialize(lstAnalyteSMRRefVal);
            // Get Test SubAnalyte Details
            if (dsTestSubAnalyte != null)
            {
                if (dsTestSubAnalyte.Tables[0].Rows.Count > 0)
                {
                    List<string> lstSASM = new List<string>();

                    //load Sub Analytes drop down to select analyte for adding specimen method 
                    selSubAnalyte.Items.Clear();
                    selSubAnalyte.Items.Add(new ListItem("Select Sub Analyte", ""));

                    foreach (DataRow row in dsTestSubAnalyte.Tables[0].Rows)
                    {
                        string sasm = row["sSubAnalyteId"].ToString() + "-" + row["sSpecimenId"].ToString() + "-" + row["sMethodId"].ToString();

                        if (lstSASM.Contains(sasm) == false)
                        {
                            lstSASM.Add(sasm);

                            dictSubAnalyteSMR = new Dictionary<string, object>();
                            dictSubAnalyteSMR.Add("testSubAnalyteId", row["sTestSubAnalyteId"].ToString());
                            dictSubAnalyteSMR.Add("analyteId", row["sAnalyteId"].ToString());
                            dictSubAnalyteSMR.Add("analyteName", row["sAnalyteName"].ToString());
                            dictSubAnalyteSMR.Add("subAnalyteId", row["sSubAnalyteId"].ToString());
                            dictSubAnalyteSMR.Add("subAnalyteName", row["sSubAnalyteName"].ToString());
                            dictSubAnalyteSMR.Add("tsasmId", row["sTSASMId"].ToString());
                            dictSubAnalyteSMR.Add("specimenId", row["sSpecimenId"].ToString());
                            dictSubAnalyteSMR.Add("specimenName", row["sSampleType"].ToString());
                            dictSubAnalyteSMR.Add("quantity", row["sQuantity"].ToString());
                            dictSubAnalyteSMR.Add("timePeriod", row["sTimePeriod"].ToString());
                            dictSubAnalyteSMR.Add("methodId", row["sMethodId"].ToString());
                            dictSubAnalyteSMR.Add("methodName", row["sMethodName"].ToString());
                            dictSubAnalyteSMR.Add("resultType", row["sResultType"].ToString());
                            dictSubAnalyteSMR.Add("refVal", new List<Dictionary<string, string>>());

                            lstSubAnalyteSMRRefVal.Add(dictSubAnalyteSMR);
                            dictSubAnalyteSMRRefVal = new Dictionary<string, string>();
                            dictSubAnalyteSMRRefVal.Add("tsarId", row["sTestSubAnalyteReferenceId"].ToString());
                            dictSubAnalyteSMRRefVal.Add("refType", row["sReferenceType"].ToString());
                            dictSubAnalyteSMRRefVal.Add("age", row["sAge"].ToString());
                            dictSubAnalyteSMRRefVal.Add("male", row["sMale"].ToString());
                            dictSubAnalyteSMRRefVal.Add("female", row["sFemale"].ToString());
                            dictSubAnalyteSMRRefVal.Add("grade", row["sGrade"].ToString());
                            dictSubAnalyteSMRRefVal.Add("units", row["sUnits"].ToString());
                            dictSubAnalyteSMRRefVal.Add("interpretation", row["sInterpretation"].ToString());
                            dictSubAnalyteSMRRefVal.Add("lowerLimit", row["sLowerLimit"].ToString());
                            dictSubAnalyteSMRRefVal.Add("upperLimit", row["sUpperLimit"].ToString());

                            lstSubAnalyteRefVal = new List<Dictionary<string, string>>();
                            lstSubAnalyteRefVal.Add(dictSubAnalyteSMRRefVal);

                            foreach (var record in lstSubAnalyteSMRRefVal)
                            {
                                if (record["tsasmId"].ToString() == row["sTSASMId"].ToString())
                                {
                                    record["refVal"] = lstSubAnalyteRefVal;
                                }
                            }
                        }
                        else
                        {
                            dictSubAnalyteSMRRefVal = new Dictionary<string, string>();
                            dictSubAnalyteSMRRefVal.Add("tsarId", row["sTestSubAnalyteReferenceId"].ToString());
                            dictSubAnalyteSMRRefVal.Add("refType", row["sReferenceType"].ToString());
                            dictSubAnalyteSMRRefVal.Add("age", row["sAge"].ToString());
                            dictSubAnalyteSMRRefVal.Add("male", row["sMale"].ToString());
                            dictSubAnalyteSMRRefVal.Add("female", row["sFemale"].ToString());
                            dictSubAnalyteSMRRefVal.Add("grade", row["sGrade"].ToString());
                            dictSubAnalyteSMRRefVal.Add("units", row["sUnits"].ToString());
                            dictSubAnalyteSMRRefVal.Add("interpretation", row["sInterpretation"].ToString());
                            dictSubAnalyteSMRRefVal.Add("lowerLimit", row["sLowerLimit"].ToString());
                            dictSubAnalyteSMRRefVal.Add("upperLimit", row["sUpperLimit"].ToString());

                            lstSubAnalyteRefVal.Add(dictSubAnalyteSMRRefVal);

                            foreach (var record in lstSubAnalyteSMRRefVal)
                            {
                                if (record["tsasmId"].ToString() == row["sTSASMId"].ToString())
                                {
                                    record["refVal"] = lstSubAnalyteRefVal;
                                }
                            }
                        }
                        //load Sub Analytes drop down to select analyte for adding specimen method 
                        if (selSubAnalyte.Items.Contains(new ListItem(row["sSubAnalyteName"].ToString(), row["sAnalyteId"].ToString() + "|" + row["sSubAnalyteId"].ToString())) == false)
                            selSubAnalyte.Items.Add(new ListItem(row["sSubAnalyteName"].ToString(), row["sAnalyteId"].ToString() + "|" + row["sSubAnalyteId"].ToString()));
                    }
                }
                else
                {
                    //tbodyTestAnalyteSubAnalyte.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
            hiddenSubAnalyteSMRRefVal.Value = serializer.Serialize(lstSubAnalyteSMRRefVal);
        }
        catch
        {
            Response.Redirect("Error.htm");
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
                selSection.Items.Add(new ListItem("Select Section", ""));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    selSection.Items.Add(new ListItem(dr["sSectionName"].ToString(), dr["sSectionId"].ToString()));
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
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
                selProfile.Items.Add(new ListItem("Select Profile", ""));

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
        catch
        {
            Response.Redirect("Error.htm");
        }
    }

    [WebMethod]
    public static string updateSpecimenMethod(string insertInto, string smrId, string sampleType, string quantity, string timePeriod, string method, string resultType, string testAnalyteSubAnalyteId)
    {
        ClsEditTest objEditTest = new ClsEditTest();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        Dictionary<string, object> response = null;

        if (insertInto == "analyte")
        {
            response = objEditTest.updateTASM(smrId, sampleType, quantity, timePeriod, method, resultType, testAnalyteSubAnalyteId);
        }
        else if (insertInto == "subAnalyte")
        {
            response = objEditTest.updateTSASM(smrId, sampleType, quantity, timePeriod, method, resultType, testAnalyteSubAnalyteId);
        }

        return serializer.Serialize(response);
    }

    [WebMethod]
    public static string updateReferenceValues(string insertInto, string testSMId, string testReferenceId, string referenceType, string ageRange, string male, string female, string grade, string units, string interpretation, string lowerLimit, string upperLimit)
    {
        ClsEditTest objEditTest = new ClsEditTest();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        Dictionary<string, object> response = null;

        if (insertInto == "analyte")
        {
            response = objEditTest.updateAnalyteReference(testSMId, testReferenceId, referenceType, ageRange, male, female, grade, units, interpretation, lowerLimit, upperLimit);
        }
        else if (insertInto == "subAnalyte")
        {
            response = objEditTest.updateSubAnalyteReference(testSMId, testReferenceId, referenceType, ageRange, male, female, grade, units, interpretation, lowerLimit, upperLimit);
        }

        return serializer.Serialize(response);
    }

    [WebMethod]
    public static string addReferenceValues(string insertInto, string testSMId, string referenceType, string ageRange, string male, string female, string grade, string units, string interpretation, string lowerLimit, string upperLimit)
    {
        ClsEditTest objEditTest = new ClsEditTest();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        Dictionary<string, object> response = null;

        if (insertInto == "analyte")
        {
            response = objEditTest.addAnalyteReference(testSMId, referenceType, ageRange, male, female, grade, units, interpretation, lowerLimit, upperLimit);
        }
        else if (insertInto == "subAnalyte")
        {
            response = objEditTest.addSubAnalyteReference(testSMId, referenceType, ageRange, male, female, grade, units, interpretation, lowerLimit, upperLimit);
        }

        return serializer.Serialize(response);
    }

    [WebMethod]
    public static string deleteReferenceValues(string deleteFrom, string testReferenceId)
    {
        ClsEditTest objEditTest = new ClsEditTest();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        Dictionary<string, object> response = null;

        if (deleteFrom == "analyte")
        {
            response = objEditTest.deleteAnalyteReference(testReferenceId);
        }
        else if (deleteFrom == "subAnalyte")
        {
            response = objEditTest.deleteSubAnalyteReference(testReferenceId);
        }

        return serializer.Serialize(response);
    }

    protected void btnAddSMR_Click(object sender, EventArgs e)
    {
        try
        {
            string testAnalyteId = hiddenTestAnalyteId.Value;
            string testSubAnalyteId = hiddenTestSubAnalyteId.Value;
            string sampleType = txtAddSpecimenName.Text;
            string quantity = txtAddQuantity.Text;
            string timePeriod = txtAddTimePeriod.Text;
            string method = txtAddMethod.Text;
            string resultType = selAddResultType.Value;

            if (testSubAnalyteId == "" || testSubAnalyteId == null)
            {
                string response = objEditTest.addTASM(testAnalyteId, sampleType, quantity, timePeriod, method, resultType);

                if (response == "1")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Specimen added'),location.reload(true);", true);
                }
                else if (response == "2")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Specimen already exists'),location.reload(true);", true);
                }
                else if (response == "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error Occured'),location.reload(true);", true);
                }
            }
            else if (testSubAnalyteId != "" || testSubAnalyteId != null)
            {
                string response = objEditTest.addTSASM(testSubAnalyteId, sampleType, quantity, timePeriod, method, resultType);

                if (response == "1")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Specimen added'),location.reload(true);", true);
                }
                else if (response == "2")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Specimen already exists'),location.reload(true);", true);
                }
                else if (response == "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error Occured'),location.reload(true);", true);
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }

    [WebMethod]
    public static string deleteSMR(string deleteFrom, string testSMId)
    {
        ClsEditTest objEditTest = new ClsEditTest();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        Dictionary<string, object> response = null;

        if (deleteFrom == "analyte")
        {
            response = objEditTest.deleteAnalyteSMR(testSMId);
        }
        else if (deleteFrom == "subAnalyte")
        {
            response = objEditTest.deleteSubAnalyteSMR(testSMId);
        }

        return serializer.Serialize(response);
    }

    [WebMethod]
    public static string getAnalytes(string testId)
    {
        ClsEditTest objEditTest = new ClsEditTest();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, string>> response = new List<Dictionary<string, string>>();

        DataSet dsAnalyte = new DataSet();

        dsAnalyte = objEditTest.getAnalyteNotInTest(testId);

        if (dsAnalyte != null)
        {
            foreach (DataRow row in dsAnalyte.Tables[0].Rows)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("analyteId", row["sAnalyteId"].ToString());
                dict.Add("analyteName", row["sAnalyteName"].ToString());
                response.Add(dict);
            }
        }

        return serializer.Serialize(response);
    }

    [WebMethod]
    public static string addAnalyte(string testId, string analyteId, string analyteName)
    {
        ClsEditTest objEditTest = new ClsEditTest();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        Dictionary<string, object> response = null;

        response = objEditTest.addAnalyte(testId, analyteId, analyteName);

        return serializer.Serialize(response);
    }

    [WebMethod]
    public static string getAnalyteSubAnalyte(string testId)
    {
        ClsEditTest objEditTest = new ClsEditTest();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, string>> response = new List<Dictionary<string, string>>();

        DataSet dsSubAnalyte = new DataSet();

        dsSubAnalyte = objEditTest.getSubAnalyteNotInTest(testId);

        if (dsSubAnalyte != null)
        {
            foreach (DataRow row in dsSubAnalyte.Tables[0].Rows)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("analyteId", row["sAnalyteId"].ToString());
                dict.Add("analyteName", row["sAnalyteName"].ToString());
                dict.Add("subAnalyteId", row["sSubAnalyteId"].ToString());
                dict.Add("subAnalyteName", row["sSubAnalyteName"].ToString());
                response.Add(dict);
            }
        }

        var res = response;

        return serializer.Serialize(response);
    }

    [WebMethod]
    public static string addSubAnalyte(string testId, string subAnalyteId)
    {
        ClsEditTest objEditTest = new ClsEditTest();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        Dictionary<string, object> response = null;

        response = objEditTest.addSubAnalyte(testId, subAnalyteId);

        return serializer.Serialize(response);
    }

    protected void btnUpdateTestDetails_Click(object sender, EventArgs e)
    {
        try
        {
            int testId = Convert.ToInt32(Request.QueryString["id"].ToString());
            string section = selSection.SelectedItem.Value.ToString();
            string profile = selProfile.SelectedItem.Value.ToString().Split('|')[1];
            string testCode = txtTestCode.Text;
            string testName = txtTestName.Text;
            string testUsefulFor = txtTestUsefulFor.Text;
            string testInterpretation = txtTestInterpretation.Text;
            string testLimitation = txtTestLimitation.Text;
            string testClinicalReferences = txtTestClinicalReferences.Text;

            string response = objEditTest.updateTestDetails(testId.ToString(), profile, testCode, testName, testUsefulFor, testInterpretation, testLimitation, testClinicalReferences);

            if (response == "1")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Test Updated'),location.reload(true);", true);
            }
            else if (response == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error Occured'),location.reload(true);", true);
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }

    [WebMethod]
    public static string getSpecimen()
    {
        ClsEditTest objEditTest = new ClsEditTest();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, string>> response = new List<Dictionary<string, string>>();

        DataSet dsAnalyte = new DataSet();

        dsAnalyte = objEditTest.getSpecimen();

        if (dsAnalyte != null)
        {
            foreach (DataRow row in dsAnalyte.Tables[0].Rows)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("specimenName", row["sSampleType"].ToString());
                response.Add(dict);
            }
        }
        return serializer.Serialize(response);
    }
}
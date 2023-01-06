using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Collections;
using System.Data.SqlClient;
using DataAccessHandler;

public partial class TestList : System.Web.UI.Page
{
    ClsTestList objTestList = new ClsTestList();
    DataSet dstemp = new DataSet();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    DataAccessLayer DAL = new DataAccessLayer();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
                    loadMyTestList();
                    loadTestProfiles();
                    loadTestPackages();
                    loadTestListForTemplate();
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
    protected void loadMyTestList()
    {
        try
        {
            string tabMyTestList = "";
            string tabEditMyTestList = "<tr>" +
                                   "<th>Test Code</th>" +
                                   "<th>Test Name</th>" +
                                   "<th>Price</th>" +
                                "</tr>";
            int count = 0;
            int count1 = 0;
            string PrevName = "";
            int i = 0;
            int j = 0;
            try
            {

                dstemp = DAL.GetDataSet("Sp_GetTestCodeAndtestnameByLabId " + Request.Cookies["labId"].Value.ToString());
                for (i = 0; i < dstemp.Tables[0].Rows.Count; i++)
                {
                    string strName = "";
                    string Testcode = "";
                    string TestName = "";
                    tabMyTestList += "<li class='table-row'>" +
                               "<div class='col col-1 text-center'>" + dstemp.Tables[0].Rows[i]["sTestCode"].ToString() + "</div>" +
                               "<div class='col col-5 text-center'>" + dstemp.Tables[0].Rows[i]["sTestName"].ToString() + "</div>";
                    Testcode = dstemp.Tables[0].Rows[i]["sTestCode"].ToString();
                    TestName = dstemp.Tables[0].Rows[i]["sTestName"].ToString();
                    DataSet dstemp1 = new DataSet();

                    SqlParameter[] param = new SqlParameter[]
                                    {
                                        new SqlParameter("@labId",Request.Cookies["labId"].Value.ToString()),
                                        new SqlParameter("@Testcode",Testcode),
                                        new SqlParameter("@TestName",TestName)
                                    };
                    dstemp1 = DAL.ExecuteStoredProcedureDataSet("Sp_GetTestDetailsByLabIdandtestCode", param);
                    //da.SelectCommand = cmd1;
                    //da.Fill(dstemp1);

                    for (j = 0; j < dstemp1.Tables[0].Rows.Count; j++)
                    {
                        strName = strName + "<a href='TestDetails.aspx?id=" + dstemp1.Tables[0].Rows[j]["sTestId"].ToString() + "'>" + dstemp1.Tables[0].Rows[j]["sProfileName"].ToString() + "</a>, ";
                    }
                    tabMyTestList += "<div class='col col-4 text-center'>" + strName.Remove(strName.Length - 2) + "</div>";
                    string deleteTest = (dstemp1.Tables[0].Rows[0]["sMyTest"].ToString() == "1") ? "<a class='HideEditbtn' href='javascript:void(0)' id='" + dstemp1.Tables[0].Rows[0]["sTestId"].ToString() + "' onclick='javascript:removeTest(this)'><i class='fa fa-trash-o margin-0' aria-hidden='true'></i></a>" : "";

                    string tabMyTestList1 = "<div class='col col-3 text-center'>" + dstemp1.Tables[0].Rows[0]["sSectionName"].ToString() + "</div>" +
                        "<div class='col col-2 text-center'>" + dstemp1.Tables[0].Rows[0]["sPrice"].ToString() + "</div>";
                    tabMyTestList += tabMyTestList1;
                    tabMyTestList += "</li>";
                    tbodyMyTestList.Text = tabMyTestList;

                    // Load lab test list for edit in modal
                    tabEditMyTestList += "<tr>" +
                                       "<td scope='col'>" + dstemp1.Tables[0].Rows[0]["sTestCode"].ToString() + "</td>" +
                                       "<td scope='col'>" + dstemp1.Tables[0].Rows[0]["sTestName"].ToString() + "</td>" +
                                       "<td scope='col'>" +
                                       "<input type='hidden' value='" + dstemp1.Tables[0].Rows[0]["sTestId"].ToString() + "' name='txtTest" + count + "' clientIdMode='static'/>" +
                                       "<input  pattern='[0-9]' value='" + dstemp1.Tables[0].Rows[0]["sPrice"].ToString() + "' name='txtPrice" + count + "' id='txtPrice" + count + "' clientIdMode='static'/></td>" +
                                    "</tr>";
                    tbodyMyTestList.Text = tabMyTestList;
                    tbodyEditMyTestList.InnerHtml = tabEditMyTestList;
                }
            }
            catch (Exception)
            {

            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void loadMyTestList1()
    {
        try
        {
            DataSet ds = objTestList.getMyTests(Request.Cookies["labId"].Value.ToString());

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    string tabMyTestList = "";

                    string tabEditMyTestList = "<tr>" +
                                           "<th>Test Code</th>" +
                                           "<th>Test Name</th>" +
                                           "<th>Price</th>" +
                                        "</tr>";

                    int count = 0;
                    int count1 = 0;
                    string PrevName = "";
                    string NextName = "";

                    try
                    {
                        dstemp = DAL.GetDataSet("Sp_GetTestCodebySectionidandTestprofile " + Request.Cookies["labId"].Value.ToString());
                    }
                    catch (Exception)
                    {
                    }
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string strName = "";
                        NextName = row["sTestCode"].ToString();
                        count = count + 1;
                        string deleteTest = (row["sMyTest"].ToString() == "1") ? "<a class='HideEditbtn' href='javascript:void(0)' id='" + row["sTestId"].ToString() + "' onclick='javascript:removeTest(this)'><i class='fa fa-trash-o margin-0' aria-hidden='true'></i></a>" : "";

                        string s = "";
                        foreach (DataRow row1 in ds.Tables[0].Rows)
                        {
                            NextName = row["sTestCode"].ToString();
                            count1 = count1 + 1;
                            if (count1 == 1)
                            {
                                //Load lab test list
                                strName = strName + row1["sTestName"].ToString() + ",";
                            }
                            else
                                if (NextName == PrevName)
                                {
                                    strName = strName + row1["sTestName"].ToString() + ",";
                                }
                            PrevName = row1["sTestCode"].ToString();
                        }
                        tabMyTestList += "<li class='table-row'>" +
                                         "<div class='col col-1 text-center'>" + row["sTestCode"].ToString() + "</div>" +
                                         "<div class='col col-1 text-center'>" + strName + "</div>" +
                                         "<div class='col col-3 text-center'>" + row["sProfileName"].ToString() + "</div>" +
                                         "<div class='col col-3 text-center'>" + row["sSectionName"].ToString() + "</div>" +
                                         "<div class='col col-4 text-center'>" + row["sPrice"].ToString() + "</div>" +
                                       "</li>";

                        //Load lab test list for edit in modal
                        tabEditMyTestList += "<tr>" +
                                           "<td scope='col'>" + row["sTestCode"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sTestName"].ToString() + "</td>" +
                                           "<td scope='col'>" +
                                           "<input type='hidden' value='" + row["sTestId"].ToString() + "' name='txtTest" + count + "' clientIdMode='static'/>" +
                                           "<input type='number'  value='" + row["sPrice"].ToString() + "' name='txtPrice" + count + "' id='txtPrice" + count + "' clientIdMode='static'/></td>" +
                                        "</tr>";
                    }
                    hiddenTotalTests.Value = count.ToString();
                    tbodyMyTestList.Text = tabMyTestList;
                    tbodyEditMyTestList.InnerHtml = tabEditMyTestList;
                }
                else
                {
                    tbodyMyTestList.Text = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }

    protected void loadTestListForTemplate()
    {
        try
        {
            DataSet ds = objTestList.getMyTests(Request.Cookies["labId"].Value.ToString());

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    string tabMyTestList = "";
                    string tabEditMyTestList = "<tr>" +
                                           "<th>Test Code</th>" +
                                           "<th>Test Name</th>" +
                                           "<th>Price</th>" +
                                        "</tr>";

                    int count = 0;

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        count = count + 1;

                        string deleteTest = (row["sMyTest"].ToString() == "1") ? "<a class='HideEditbtn' href='javascript:void(0)' id='" + row["sTestId"].ToString() + "' onclick='javascript:removeTest(this)'><i class='fa fa-trash-o margin-0' aria-hidden='true'></i></a>" : "";

                        //Load lab test list
                        tabMyTestList += "<tr>" +
                                           "<td scope='col'>" + row["sTestCode"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sTestName"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sProfileName"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sSectionName"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sPrice"].ToString() + "</td>" +
                                           "<td scope='col' class='HideViewbtn'><a href='TestTemplate.aspx?id=" + row["sTestId"].ToString() + "' class='lab-btn-default HideViewbtn'>Template</a></td>" +

                                        "</tr>";

                        //Load lab test list for edit in modal
                        tabEditMyTestList += "<tr>" +
                                           "<td scope='col'>" + row["sTestCode"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sTestName"].ToString() + "</td>" +
                                           "<td scope='col'>" +
                                           "<input type='hidden' value='" + row["sTestId"].ToString() + "' name='txtTest" + count + "' clientIdMode='static'/>" +
                                           "<input type='number' onkeypress='return isNumber(event)' value='" + row["sPrice"].ToString() + "' name='txtPrice" + count + "' id='txtPrice" + count + "' clientIdMode='static'/></td>" +
                                        "</tr>";

                    }

                    hiddenTotalTests.Value = count.ToString();
                    tbodyTempalateBuilder.InnerHtml = tabMyTestList;
                    tbodyEditMyTestList.InnerHtml = tabEditMyTestList;
                }
                else
                {
                    tbodyTempalateBuilder.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string labId = Request.Cookies["labId"].Value.ToString();
            string query = "UPDATE testLab SET sPrice = CASE sTestId ";
            for (int i = 1; i <= Convert.ToInt32(hiddenTotalTests.Value); i++)
            {
                string testId = this.Request.Form["txtTest" + i];
                string price = this.Request.Form["txtPrice" + i];
                query += "WHEN '" + testId + "' THEN '" + price + "' ";
            }
            query += "END WHERE sLabId='" + labId + "'";
            if (objTestList.updatePrice(query) == 1)
            {
                lblMessage.Text = "Test Price Update successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
               // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Test Price Update successfully');location.reload();", true);
            }
            else if (objTestList.updatePrice(query) == 0)
            {
                lblMessage.Text = "Error Occured while Updating Test Price";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error Occured');location.reload();", true);
            }
            else
            {
                lblMessage.Text = "Error Occured while Loading Test Price";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
               // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('Error Occured');location.reload();", true);
            }
        }
        catch(Exception Ex)
        {
            Response.Redirect("Error.htm");
        }
    }

    [WebMethod]
    public static string deleteTest(string testId)
    {
        ClsTestList objTestList = new ClsTestList();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        string labId = HttpContext.Current.Request.Cookies["labId"].Value.ToString();
        string response = objTestList.deleteTest(testId, labId).ToString();

        return response;
    }
    protected void loadTestProfiles()
    {
        try
        {
            DataSet ds = objTestList.getMyTests(Request.Cookies["labId"].Value.ToString());
            Dictionary<string, List<string>> testProfiles = new Dictionary<string, List<string>>();
            Dictionary<string, int> dictTestPrice = new Dictionary<string, int>();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (row["sPrice"].ToString() != "0")
                        {
                            dictTestPrice.Add(row["sTestId"].ToString(), Convert.ToInt32(row["sPrice"].ToString()));

                            if (testProfiles.ContainsKey(row["sProfileName"].ToString()))
                            {
                                List<string> list = testProfiles[row["sProfileName"].ToString()];
                                if (list.Contains(row["sTestId"].ToString() + "|" + row["sTestCode"].ToString()) == false)
                                {
                                    list.Add(row["sTestId"].ToString() + "|" + row["sTestCode"].ToString());
                                }
                            }
                            else
                            {
                                List<string> list = new List<string>();
                                list.Add(row["sTestId"].ToString() + "|" + row["sTestCode"].ToString());
                                testProfiles.Add(row["sProfileName"].ToString(), list);
                            }
                        }
                    }
                }
                else
                {
                    divSelectTests.InnerHtml = "No tests found";
                }
            }

            hiddenTestIdPrice.Value = serializer.Serialize(dictTestPrice);

            var tempTestProfiles = testProfiles.OrderBy(key => key.Key);
            var orderedTestProfiles = tempTestProfiles.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);

            string divSelectTestsContent = "";
            foreach (KeyValuePair<string, List<string>> entry in orderedTestProfiles)
            {
                var profile = entry.Key;

                divSelectTestsContent += "<ul>" +
                                             "<li>" +
                                                "<input class='radio-custom' type='checkbox' name='profile'><label class='radio-custom-label'></label><b>" + profile + "</b></input>" +
                                                    "<ul>";
                //orderedTestProfiles[profile].Sort();
                foreach (string test in orderedTestProfiles[profile])
                {
                    var testIdCode = test;
                    var testId = testIdCode.Split('|')[0].ToString();
                    var testCode = testIdCode.Split('|')[1].ToString();

                    divSelectTestsContent += "<li><input class='radio-custom' type='checkbox' name='test' id='" + testId + "'><label class='radio-custom-label'></label>" + testCode + "</input><div style='float:right'>" + dictTestPrice[testId] + "</div></li>";
                }
                divSelectTestsContent += "</ul>" +
                                      "</li>" +
                                    "</ul>";
            }

            divSelectTests.InnerHtml = divSelectTestsContent;
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void loadTestPackages()
    {
        try
        {
            DataSet ds = objTestList.getMyTestPackages(Request.Cookies["labId"].Value.ToString());
            List<Dictionary<string, string>> testPackages = new List<Dictionary<string, string>>();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabMyTestPackageList = "";
                    string packageName = "";

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        if (packageName == row["sPackageName"].ToString())
                        {
                            for (int i = 0; i < testPackages.Count; i++)
                            {
                                if (testPackages[i]["packageName"] == row["sPackageName"].ToString())
                                {
                                    testPackages[i]["testCode"] += "," + row["sTestCode"].ToString();
                                }
                            }
                        }
                        else
                        {
                            packageName = row["sPackageName"].ToString();

                            dict.Add("packageName", row["sPackageName"].ToString());
                            dict.Add("price", row["sPrice"].ToString());
                            dict.Add("testCode", row["sTestCode"].ToString());

                            testPackages.Add(dict);
                        }
                    }
                    foreach (var record in testPackages)
                    {
                        //Load test package list
                        tabMyTestPackageList += "<tr>" +
                                           "<td scope='col'>" + record["packageName"] + "</td>" +
                                           "<td scope='col'>" + record["price"] + "</td>" +
                                           "<td scope='col'>" + record["testCode"] + "</td>" +
                                        "</tr>";
                    }
                    tbodyTestPackage.InnerHtml = tabMyTestPackageList;
                }
                else
                {
                    tbodyTestPackage.InnerHtml = "<tr><td>No records found</td></tr>";
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    protected void btnCreateTestPackage_Click(object sender, EventArgs e)
    {
        try
        {
            string labId = Request.Cookies["labId"].Value.ToString();
            string[] testIds = hiddenTestId.Value.Split(',');
            string packageName = txtPackageName.Text;
            string price = txtPackageDiscountedPrice.Text;
            int createTestPackage = objTestList.createTestPackage(labId, testIds, packageName, price);
            if (createTestPackage == 1)
            {
                lblMessage.Text = "Test package created";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
               // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Test package created'),location.reload(true);", true);
            }
            else if (createTestPackage == 2)
            {
                lblMessage.Text = "Package name already exists";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Package name already exists');location.reload(true);", true);
            }
            else if (createTestPackage == 0)
            {
                lblMessage.Text = "Error occurred while Package Creating";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
}
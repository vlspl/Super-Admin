using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using DataAccessHandler;
using System.Web.Services;

public partial class TestDetails : System.Web.UI.Page
{
    ClsTestDetails objTestDetails = new ClsTestDetails();
    static int Analyte;
    static int LABID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (!IsPostBack)
                {
                    loadTestDetails();
                    TestDetails.LABID = Convert.ToInt32(Request.Cookies["labId"].Value.ToString());

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
            string labId = Request.Cookies["labId"].Value.ToString();
            int LoginlabId = Convert.ToInt32(Request.Cookies["labId"].Value.ToString());
            DataSet dsTest = objTestDetails.getTest(labId, testId.ToString());
            if (dsTest != null)
            {
                if (dsTest.Tables[0].Rows.Count > 0)
                {
                    DataSet dsTestAnalyte = objTestDetails.getTestAnalyte(labId, testId.ToString());
                    if (dsTestAnalyte.Tables[0].Rows.Count == 0)
                    {
                        dsTestAnalyte = objTestDetails.getTestAnalyte1(labId, testId.ToString());
                    }
                    DataSet dsTestSubAnalyte = objTestDetails.getTestSubAnalyte(labId, testId.ToString());
                    string tabContent = "";


                    spanTestCodeName.InnerHtml = dsTest.Tables[0].Rows[0]["sTestCode"].ToString() + ", " + dsTest.Tables[0].Rows[0]["sTestName"].ToString();
                    spanTestId.InnerHtml = dsTest.Tables[0].Rows[0]["sTestId"].ToString();
                    spanPrice.InnerHtml = dsTest.Tables[0].Rows[0]["sPrice"].ToString();
                    spanSection.InnerHtml = dsTest.Tables[0].Rows[0]["sSectionName"].ToString();
                    spanProfile.InnerHtml = dsTest.Tables[0].Rows[0]["sProfileName"].ToString();
                    spanTestInfo.InnerHtml = dsTest.Tables[0].Rows[0]["sTestUsefulFor"].ToString();
                    spanTestInterpretation.InnerHtml = dsTest.Tables[0].Rows[0]["sTestInterpretation"].ToString();
                    spanTestLimitation.InnerHtml = dsTest.Tables[0].Rows[0]["sTestLimitation"].ToString();


                    if (dsTestAnalyte != null)
                    {
                        if (dsTestAnalyte.Tables[0].Rows.Count > 0)
                        {
                            TestDetails.Analyte = 1;
                            List<string> lstASM = new List<string>();

                            foreach (DataRow row in dsTestAnalyte.Tables[0].Rows)
                            {
                                string asm = row["sAnalyteId"].ToString() + "-" + row["sSpecimenId"].ToString() + "-" + row["sMethodId"].ToString();
                                int TestlabId = Convert.ToInt32(row["LabId"].ToString());
                                if (lstASM.Contains(asm) == false)
                                {
                                    lstASM.Add(asm);
                                    //Load test analyte details
                                    tabContent += "<tr>" +
                                                       "<td scope='col'>" + row["sAnalyteName"].ToString() + "</td>" +
                                                       "<td scope='col'>---</td>" +
                                                       "<td scope='col'>" + row["sSampleType"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["sMethodName"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["sResultType"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["ReferenceType"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["MaleFromAge"].ToString() + "-" + row["MaleToAge"].ToString() + " " + row["MaleAgeUnit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["FemaleFromAge"].ToString() + "-" + row["FemaleToAge"].ToString() + " " + row["FemaleAgeUnit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["Grade"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["Unit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["Interpretation"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["UpperLimit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["LowerLimit"].ToString() + "</td>";
                                    if (LoginlabId == TestlabId)
                                    {
                                        tabContent += "<td scope='col'  onclick='edit(" + row["TestAnalyteReferenceID"].ToString() + ")'>" + "<span class='fa fa-edit fa-color mr-1'>Edit</span>" + "</td>";
                                        tabContent += "<td scope='col' <a onclick='Add(" + row["TASMId"].ToString() + ")'>" + "<span class='fa fa-edit fa-color mr-1'>ADD</span></a>" + "</td>";
                                    }
                                    else
                                    {
                                        tabContent += "<td scope='col'></td>";
                                    };
                                    tabContent += "</tr>";
                                }
                                else
                                {
                                    //Load test analyte details
                                    tabContent += "<tr>" +
                                                       "<td scope='col'></td>" +
                                                       "<td scope='col'></td>" +
                                                       "<td scope='col'></td>" +
                                                       "<td scope='col'></td>" +
                                                       "<td scope='col'></td>" +
                                                       "<td scope='col'></td>" +
                                                       "<td scope='col'>" + row["MaleFromAge"].ToString() + "-" + row["MaleToAge"].ToString() + " " + row["MaleAgeUnit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["FemaleFromAge"].ToString() + "-" + row["FemaleToAge"].ToString() + " " + row["FemaleAgeUnit"].ToString() + "</td>" +
                                                      "<td scope='col'>" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["Grade"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["Unit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["Interpretation"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["UpperLimit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["LowerLimit"].ToString() + "</td>";
                                    if (LoginlabId == TestlabId)
                                    {
                                        tabContent += "<td scope='col'  onclick='edit(" + row["TestAnalyteReferenceID"].ToString() + ")'>" + "<span class='fa fa-edit fa-color mr-1'>Edit</span>" + "</td>";
                                        tabContent += "<td scope='col' <a onclick='Addss(" + row["TASMId"].ToString() + ")'>" + "<span class=''></span></a>" + "</td>";
                                    }
                                    else
                                    {
                                        tabContent += "<td scope='col'></td>";
                                    };
                                    tabContent += "</tr>";
                                }
                            }
                        }
                        else
                        {
                            //tbodyTestAnalyteSubAnalyte.InnerHtml = "<tr><td>No records found</td></tr>";
                        }
                    }

                    if (dsTestSubAnalyte != null)
                    {
                        if (dsTestSubAnalyte.Tables[0].Rows.Count > 0)
                        {
                            TestDetails.Analyte = 2;
                            List<string> lstSASM = new List<string>();

                            foreach (DataRow row in dsTestSubAnalyte.Tables[0].Rows)
                            {
                                string sasm = row["sSubAnalyteId"].ToString() + "-" + row["sSpecimenId"].ToString() + "-" + row["sMethodId"].ToString();
                                int TestlabId = Convert.ToInt32(row["LabId"].ToString());
                                if (lstSASM.Contains(sasm) == false)
                                {
                                    lstSASM.Add(sasm);

                                    //Load test sub analyte details
                                    tabContent += "<tr>" +
                                                       "<td scope='col'>" + row["sAnalyteName"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["sSubAnalyteName"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["sSampleType"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["sMethodName"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["sResultType"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["ReferenceType"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["MaleFromAge"].ToString() + "-" + row["MaleToAge"].ToString() + " " + row["MaleAgeUnit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["FemaleFromAge"].ToString() + "-" + row["FemaleToAge"].ToString() + " " + row["FemaleAgeUnit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["Grade"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["Unit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["Interpretation"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["UpperLimit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["LowerLimit"].ToString() + "</td>";
                                    if (LoginlabId == TestlabId)
                                    {
                                        tabContent += "<td scope='col'  onclick='edit(" + row["TestSubAnalyteReferenceID"].ToString() + ")'>" + "<span class='fa fa-edit fa-color mr-1'>Edit</span>" + "</td>";
                                        tabContent += "<td scope='col'  onclick='AddTSAMId(" + row["TSASMId"].ToString() + ")'>" + "<span class='fa fa-Add fa-color mr-1'>ADD</span>" + "</td>";
                                    }
                                    else
                                    {
                                        tabContent += "<td scope='col'></td>";
                                    };
                                    tabContent += "</tr>";
                                }
                                else
                                {
                                    //Load test sub analyte details
                                    tabContent += "<tr>" +

                                                       "<td scope='col'></td>" +
                                                       "<td scope='col'></td>" +
                                                       "<td scope='col'></td>" +
                                                       "<td scope='col'></td>" +
                                                       "<td scope='col'></td>" +
                                                       "<td scope='col'>" + row["ReferenceType"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["MaleFromAge"].ToString() + "-" + row["MaleToAge"].ToString() + " " + row["MaleAgeUnit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["MaleMinValue"].ToString() + "-" + row["MaleMaxValue"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["FemaleFromAge"].ToString() + "-" + row["FemaleToAge"].ToString() + " " + row["FemaleAgeUnit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["FemaleMinValue"].ToString() + "-" + row["FemaleMaxValue"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["Grade"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["Unit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["Interpretation"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["UpperLimit"].ToString() + "</td>" +
                                                       "<td scope='col'>" + row["LowerLimit"].ToString() + "</td>";
                                    if (LoginlabId == TestlabId)
                                    {
                                        tabContent += "<td scope='col'  onclick='edit(" + row["TestSubAnalyteReferenceID"].ToString() + ")'>" + "<span class='fa fa-edit fa-color mr-1'>Edit</span>" + "</td>";
                                        tabContent += "<td scope='col' <a onclick='Addss(" + row["TSASMId"].ToString() + ")'>" + "<span class=''></span></a>" + "</td>";
                                    }
                                    else
                                    {
                                        tabContent += "<td scope='col'></td>";
                                    };
                                    tabContent += "</tr>";
                                }
                            }
                        }
                        else
                        {
                            //tbodyTestAnalyteSubAnalyte.InnerHtml = "<tr><td>No records found</td></tr>";
                        }
                    }
                    tbodyTestAnalyteSubAnalyte.InnerHtml = tabContent;
                }
            }
        }
        catch
        {
            Response.Redirect("Error.htm");
        }
    }
    static int flag;
    [WebMethod]
    public static string TestEdit(int testId)
    {
        DataAccessLayer DAL = new DataAccessLayer();
        string ss = testId.ToString();
        int nn = testId;
        string TSAMID = "";
        DataTable ds = new DataTable();
        try
        {
            if (TestDetails.Analyte == 1)
            {
                ds = DAL.GetDataTable("Sp_GetAnalyteReferenceRangeValue " + testId);
                // return ds;
                TestDetails.flag = 1;
                TSAMID = ds.Rows[0]["TASMId"].ToString();
            }
            else if (TestDetails.Analyte == 2)
            {
                ds = DAL.GetDataTable("Sp_GetSubAnalyteReferenceRangeValue " + testId);
                TestDetails.flag = 2;
                if (ds.Rows.Count > 0)
                {
                    TSAMID = ds.Rows[0]["TSASMId"].ToString();
                }
                else
                {
                    ds = DAL.GetDataTable("Sp_GetAnalyteReferenceRangeValue " + testId);
                    // return ds;
                    TestDetails.flag = 1;
                    TSAMID = ds.Rows[0]["TASMId"].ToString();
                }
            }
        }
        catch (Exception)
        {
            ds = null;
            // return ds;
        }
        string MaleFromAge = "";
        string MaleToAge = "";
        string MaleAgeUnit = "";
        string FemaleFromAge = "";
        string FemaleToAge = "";
        string FemaleAgeUnit = "";
        string Grade = "";
        string Units = "";
        string Interpretation = "";
        string UpperLimit = "";
        string LowerLimit = "";
        string MaleMinValue = "0";
        string MaleMaxValue = "0";
        string FemaleMinValue = "0";
        string FemaleMaxValue = "0";
        int LABID = 1;

        string hRefVal = "";
        if (ds.Rows.Count > 0)
        {
            LABID = Convert.ToInt32(ds.Rows[0]["LabId"].ToString());

            hRefVal = ds.Rows[0]["ReferenceType"].ToString();
            MaleMinValue = ds.Rows[0]["MaleMinValue"].ToString();
            MaleMaxValue = ds.Rows[0]["MaleMaxValue"].ToString();
            FemaleMinValue = ds.Rows[0]["FemaleMinValue"].ToString();
            FemaleMaxValue = ds.Rows[0]["FemaleMaxValue"].ToString();
            MaleFromAge = ds.Rows[0]["MaleFromAge"].ToString();
            MaleToAge = ds.Rows[0]["MaleToAge"].ToString();
            MaleAgeUnit = ds.Rows[0]["MaleAgeUnit"].ToString();
            FemaleFromAge = ds.Rows[0]["FemaleFromAge"].ToString();
            FemaleToAge = ds.Rows[0]["FemaleToAge"].ToString();
            FemaleAgeUnit = ds.Rows[0]["FemaleAgeUnit"].ToString();
            Grade = ds.Rows[0]["Grade"].ToString();
            Units = ds.Rows[0]["Unit"].ToString();
            Interpretation = ds.Rows[0]["Interpretation"].ToString();
            UpperLimit = ds.Rows[0]["UpperLimit"].ToString();
            LowerLimit = ds.Rows[0]["LowerLimit"].ToString();
        }
        return JsonConvert.SerializeObject("[{'MaleFromage':&&&" + MaleFromAge + "&&&'MaleToAge':&&&" + MaleToAge +
         "&&&'MaleAgeUnit':&&&" + MaleAgeUnit + "&&&'MaleMinValue':&&&" + MaleMinValue + "&&&'MaleMaxValue':&&&" + MaleMaxValue +
         "&&&'FemaleFromAge':&&&" + FemaleFromAge + "&&&'FemaleToAge':&&&" + FemaleToAge + "&&&'FemaleAgeUnit':&&&" + FemaleAgeUnit +
         "&&&'FemaleMinValue':&&&" + FemaleMinValue + "&&&'FemaleMaxValue':&&&" + FemaleMaxValue +
         "&&&'Grade':&&&" + Grade + "&&&,'Units':&&&" + Units + "&&&,'Interpretation':&&&" + Interpretation + "&&&,'UpperLimit':&&&" +
         UpperLimit + "&&&,'LowerLimit':&&&" + LowerLimit + "&&&,'LABID':&&&" + LABID + "&&&,'TSAMID':&&&" + TSAMID + "&&&,'hRefVal':&&&" + hRefVal + "&&&}]");
    }

    [WebMethod]
    public static string btnUpdate(int testId, string MaleFromAge, string MaleToAge, string MaleAgeUnit, string MinMaleValue, string MaxMaleValue, string FemaleFromAge, string FemaleToAge,
        string FemaleAgeUnit, string MinFemaleValue, string MaxFemaleValue, string txtGrade,
        string txtUnits, string txtInterpretation, string txtUpperLimit, string txtLowerLimit, int LABId, string TSAMID, string hRefVal)
    {
        DataAccessLayer DAL = new DataAccessLayer();
        try
        {
            if (TestDetails.flag == 1)
            {
                SqlParameter[] param = new SqlParameter[]
                         {
                            new SqlParameter("@testId", testId),
                            new SqlParameter("@MaleFromAge", MaleFromAge),
                            new SqlParameter("@MaleToAge", MaleToAge),
                            new SqlParameter("@MaleAgeUnit", MaleAgeUnit),
                            new SqlParameter("@MaleReferenceMinValue", MinMaleValue),
                            new SqlParameter("@MaleReferenceMaxValue", MaxMaleValue),
                            new SqlParameter("@FemaleFromAge", FemaleFromAge),
                            new SqlParameter("@FemaleToAge", FemaleToAge),
                            new SqlParameter("@FemaleAgeUnit", FemaleAgeUnit),
                            new SqlParameter("@FemaleReferenceMinValue", MinFemaleValue),
                            new SqlParameter("@FemaleReferenceMaxValue", MaxFemaleValue),
                            new SqlParameter("@sGrade", txtGrade),
                            new SqlParameter("@sUnits", txtUnits),
                            new SqlParameter("@sInterpretation", txtInterpretation),
                            new SqlParameter("@sLowerLimit", txtLowerLimit),
                            new SqlParameter("@sUpperLimit", txtUpperLimit)
                        };
                DAL.ExecuteStoredProcedure("Sp_UpdatetestAnalyteReferenceValues", param);
            }
            if (TestDetails.flag == 2)
            {

                SqlParameter[] param = new SqlParameter[]
                         {
                            new SqlParameter("@testId", testId),
                            new SqlParameter("@MaleFromAge", MaleFromAge),
                            new SqlParameter("@MaleToAge", MaleToAge),
                            new SqlParameter("@MaleAgeUnit", MaleAgeUnit),
                            new SqlParameter("@MaleReferenceMinValue", MinMaleValue),
                            new SqlParameter("@MaleReferenceMaxValue", MaxMaleValue),
                            new SqlParameter("@FemaleFromAge", FemaleFromAge),
                            new SqlParameter("@FemaleToAge", FemaleToAge),
                            new SqlParameter("@FemaleAgeUnit", FemaleAgeUnit),
                            new SqlParameter("@FemaleReferenceMinValue", MinFemaleValue),
                            new SqlParameter("@FemaleReferenceMaxValue", MaxFemaleValue),
                            new SqlParameter("@sGrade", txtGrade),
                            new SqlParameter("@sUnits", txtUnits),
                            new SqlParameter("@sInterpretation", txtInterpretation),
                            new SqlParameter("@sLowerLimit", txtLowerLimit),
                            new SqlParameter("@sUpperLimit", txtUpperLimit) 
                         };
                DAL.ExecuteStoredProcedure("Sp_UpdatetestSubAnalyteReferenceValues", param);
            }
        }
        catch (Exception)
        {
        }
        return JsonConvert.SerializeObject("succeed");
    }

    [WebMethod]
    public static string btnAdd(string MaleFromAge, string MaleToAge, string MaleAgeUnit, string MinMaleValue, string MaxMaleValue, string FemaleFromAge, string FemaleToAge,
        string FemaleAgeUnit, string MinFemaleValue, string MaxFemaleValue, string txtGrade,
        string txtUnits, string txtInterpretation, string txtUpperLimit, string txtLowerLimit, string LABId, string TSAMID, string hRefVal, int Flag)
    {
        DataAccessLayer DAL = new DataAccessLayer();
        int result = 0;
        try
        {
            if (Flag == 1)
            {
                SqlParameter[] param = new SqlParameter[]
                         {
                            new SqlParameter("@LABId", TestDetails.LABID),
                            new SqlParameter("@TSAMId", TSAMID),
                            new SqlParameter("@ReferenceType", hRefVal),
                            new SqlParameter("@MaleFromAge",Convert.ToInt32(MaleFromAge)),
                            new SqlParameter("@MaleToAge", Convert.ToInt32(MaleToAge)),
                            new SqlParameter("@MaleAgeUnit", MaleAgeUnit.ToString()),
                            new SqlParameter("@MaleReferenceMinValue", Convert.ToDecimal(MinMaleValue)),
                            new SqlParameter("@MaleReferenceMaxValue", Convert.ToDecimal(MaxMaleValue)),
                            new SqlParameter("@FemaleFromAge", Convert.ToInt32(FemaleFromAge)),
                            new SqlParameter("@FemaleToAge", Convert.ToInt32(FemaleToAge)),
                            new SqlParameter("@FemaleAgeUnit", FemaleAgeUnit.ToString()),
                            new SqlParameter("@FemaleReferenceMinValue", Convert.ToDecimal(MinFemaleValue)),
                            new SqlParameter("@FemaleReferenceMaxValue", Convert.ToDecimal(MaxFemaleValue)),
                            new SqlParameter("@sGrade", txtGrade),
                            new SqlParameter("@sUnits", txtUnits),
                            new SqlParameter("@sInterpretation", txtInterpretation),
                            new SqlParameter("@sLowerLimit", txtLowerLimit),
                            new SqlParameter("@sUpperLimit", txtUpperLimit),
                            new SqlParameter("@returnVal", txtUpperLimit)
                        };
                result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddtestAnalyteReferenceValues", param);
            }
            else
            {
                SqlParameter[] param = new SqlParameter[]
                         {
                            new SqlParameter("@LabId", TestDetails.LABID),
                            new SqlParameter("@TSASMId", TSAMID),
                            new SqlParameter("@ReferenceType", hRefVal),
                            new SqlParameter("@MaleFromAge",Convert.ToInt32(MaleFromAge)),
                            new SqlParameter("@MaleToAge", Convert.ToInt32(MaleToAge)),
                            new SqlParameter("@MaleAgeUnit", MaleAgeUnit.ToString()),
                            new SqlParameter("@MaleReferenceMinValue", Convert.ToDecimal(MinMaleValue)),
                            new SqlParameter("@MaleReferenceMaxValue", Convert.ToDecimal(MaxMaleValue)),
                            new SqlParameter("@FemaleFromAge", Convert.ToInt32(FemaleFromAge)),
                            new SqlParameter("@FemaleToAge", Convert.ToInt32(FemaleToAge)),
                            new SqlParameter("@FemaleAgeUnit", FemaleAgeUnit.ToString()),
                            new SqlParameter("@FemaleReferenceMinValue", Convert.ToDecimal(MinFemaleValue)),
                            new SqlParameter("@FemaleReferenceMaxValue", Convert.ToDecimal(MaxFemaleValue)),
                            new SqlParameter("@sGrade", txtGrade),
                            new SqlParameter("@sUnits", txtUnits),
                            new SqlParameter("@sInterpretation", txtInterpretation),
                            new SqlParameter("@sLowerLimit", txtLowerLimit),
                            new SqlParameter("@sUpperLimit", txtUpperLimit),
                            new SqlParameter("@returnVal", txtUpperLimit)
                        };
                result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddNewtestSubAnalyteReferenceRangeValue", param);
            }
        }
        catch (Exception)
        {
        }
        return JsonConvert.SerializeObject(result);
    }

   
   
}
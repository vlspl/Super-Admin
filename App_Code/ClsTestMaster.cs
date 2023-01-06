using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsTestMaster
{
    DataAccessLayer DAL = new DataAccessLayer();
    public ClsTestMaster()
    {
    }
    public Dictionary<string, object> addSection(string sectionName)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        ClsSectionMaster objSectionMaster = new ClsSectionMaster();
        DataSet dsSection = objSectionMaster.getSection();
        ClsProfileMaster objProfileMaster = new ClsProfileMaster();
        DataSet dsProfile = objProfileMaster.getTestProfile();
        try
        {
            //insert new section into table only if does not exist & return 1 & if section already exists return 2
            SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sectionName",sectionName),
                        new SqlParameter("@returnval",SqlDbType.Int)
                    };
            int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddSection", param);

            if (result == 2)
            {
                returnType = 2;
                returnData.Add("key", returnType);
                returnData.Add("value", null);
                return returnData;
            }
            else if (result == 1)
            {
                DataSet ds = new DataSet();
                ds = DAL.GetDataSet("Sp_GetSection");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }

                    returnType = 1;
                    returnData.Add("key", returnType);
                    returnData.Add("value", rows);
                    return returnData;
                }
            }
            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            returnType = 0;
            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
    }
    public Dictionary<string, object> addProfile(string profileName, string sectionId)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            //insert new section into table only if does not exist & return 1 & if section already exists return 2
            SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sProfileName",profileName),
                        new SqlParameter("@sSectionId",sectionId),
                        new SqlParameter("@returnval",SqlDbType.Int)
                    };
            int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddProfile", param);
            if (result == 2)
            {
                returnType = 2;
                returnData.Add("key", returnType);
                returnData.Add("value", null);
                return returnData;
            }
            else if (result == 1)
            {
                DataSet ds = new DataSet();
                ds = DAL.GetDataSet("Sp_GetAlltestProfile");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }
                    returnType = 1;
                    returnData.Add("key", returnType);
                    returnData.Add("value", rows);
                    return returnData;
                }
            }
            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            returnType = 0;
            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
    }
    public Dictionary<string, object> addAnalyte(string analyteName)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;

        try
        {
            //insert new section into table only if does not exist & return 1 & if section already exists return 2
            SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sAnalyteName",analyteName),
                        new SqlParameter("@returnval",SqlDbType.Int)
                    };
            int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddAnalyte", param);
            if (result == 2)
            {
                returnType = 2;
                returnData.Add("key", returnType);
                returnData.Add("value", null);
                return returnData;
            }
            else if (result >= 1)
            {
                DataSet ds = new DataSet();
                ds = DAL.GetDataSet("Sp_Getanalyte");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }

                    returnType = 1;
                    returnData.Add("key", returnType);
                    returnData.Add("value", rows);
                    return returnData;
                }
            }

            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            returnType = 0;
            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
    }
    //public Dictionary<string, object> loadSubAnalyte(string analyteIds)
    //{
    //    Dictionary<string, object> returnData = new Dictionary<string, object>();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    var returnType = 0;

    //    try
    //    {
    //        DataSet ds = new DataSet();
    //        ds = DAL.GetDataSet("Sp_GetSubAnalyteByAnalteId " + analyteIds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //            Dictionary<string, object> row;

    //            foreach (DataRow dr in ds.Tables[0].Rows)
    //            {
    //                row = new Dictionary<string, object>();
    //                foreach (DataColumn col in ds.Tables[0].Columns)
    //                {
    //                    row.Add(col.ColumnName, dr[col]);
    //                }
    //                rows.Add(row);
    //            }

    //            returnType = 1;
    //            returnData.Add("key", returnType);
    //            returnData.Add("value", rows);
    //            return returnData;
    //        }
    //        else
    //        {
    //            returnType = 2;
    //            returnData.Add("key", returnType);
    //            returnData.Add("value", null);
    //            return returnData;
    //        }
    //        returnData.Add("key", returnType);
    //        returnData.Add("value", null);
    //        return returnData;
    //    }
    //    catch (Exception ex)
    //    {
    //        returnType = 0;
    //        returnData.Add("key", returnType);
    //        returnData.Add("value", null);
    //        return returnData;
    //    }
    //}
    public Dictionary<string, object> loadSubAnalyte(string analyteIds)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;

        try
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["connection"]))
            {
                conn.Open();

                //check if analyte already exists
                using (SqlCommand cmd = new SqlCommand("SELECT sSubAnalyteId,sSubAnalyteName,sAnalyteName FROM subAnalyte sa join analyte a on a.sAnalyteId=sa.sAnalyteId WHERE sa.sAnalyteId in (" + analyteIds + ") and sa.IsActive=1", conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        DataSet ds = new DataSet();
                        da.SelectCommand = cmd;
                        da.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                            Dictionary<string, object> row;

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in ds.Tables[0].Columns)
                                {
                                    row.Add(col.ColumnName, dr[col]);
                                }
                                rows.Add(row);
                            }

                            returnType = 1;
                            returnData.Add("key", returnType);
                            returnData.Add("value", rows);
                            return returnData;
                        }
                        else
                        {
                            returnType = 2;
                            returnData.Add("key", returnType);
                            returnData.Add("value", null);
                            return returnData;
                        }
                    }
                }
            }

            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
        catch (Exception ex)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
    }
    public Dictionary<string, object> addSubAnalyte(string subAnalyteName, string analyteId)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            //insert new section into table only if does not exist & return 1 & if section already exists return 2
            SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@subAnalyteName",subAnalyteName),
                        new SqlParameter("@sAnalyteId",analyteId),
                         new SqlParameter("@IsActive","1"),
                        new SqlParameter("@returnval",SqlDbType.Int)
                    };
            int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddSubAnalyte", param);
            if (result == -2)
            {
                returnType = 2;
                returnData.Add("key", returnType);
                returnData.Add("value", null);
                return returnData;
            }
            else if (result >= 1)
            {
                DataSet ds = new DataSet();
                ds = DAL.GetDataSet("Sp_GetSubAnalyteById " + result);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }

                    returnType = 1;
                    returnData.Add("key", returnType);
                    returnData.Add("value", rows);
                    return returnData;
                }
            }

            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            returnType = 0;
            returnData.Add("key", returnType);
            returnData.Add("value", null);
            return returnData;
        }
    }
    public Dictionary<string, object> addSpecimenMethod(string sampleType, string quantity, string timePeriod, string method)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;

        DataSet dsSpecimen = new DataSet();
        List<Dictionary<string, object>> rowsSpecimen = new List<Dictionary<string, object>>();
        Dictionary<string, object> rowSpecimen;

        DataSet dsMethod = new DataSet();
        List<Dictionary<string, object>> rowsMethod = new List<Dictionary<string, object>>();
        Dictionary<string, object> rowMethod;

        try
        {
            SqlParameter[] param = new SqlParameter[]
                      {
                        new SqlParameter("@sSampleType",sampleType),
                        new SqlParameter("@sQuantity",quantity),
                        new SqlParameter("@sTimePeriod",timePeriod)
                     };
            dsSpecimen = DAL.ExecuteStoredProcedureDataSet("Sp_AddSpecimenMethod", param);
            if (dsSpecimen.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSpecimen.Tables[0].Rows)
                {
                    rowSpecimen = new Dictionary<string, object>();
                    foreach (DataColumn col in dsSpecimen.Tables[0].Columns)
                    {
                        rowSpecimen.Add(col.ColumnName, dr[col]);
                    }
                    rowsSpecimen.Add(rowSpecimen);
                }
            }

            if (method != "")
            {
                SqlParameter[] param1 = new SqlParameter[]
                      {
                        new SqlParameter("@sMethodName",method)
                     };
                dsMethod = DAL.ExecuteStoredProcedureDataSet("Sp_AddMethod", param1);
                if (dsMethod.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsMethod.Tables[0].Rows)
                    {
                        rowMethod = new Dictionary<string, object>();
                        foreach (DataColumn col in dsMethod.Tables[0].Columns)
                        {
                            rowMethod.Add(col.ColumnName, dr[col]);
                        }
                        rowsMethod.Add(rowMethod);
                    }
                }
            }
            returnType = 1;
            returnData.Add("key", returnType);
            returnData.Add("specimen", rowsSpecimen);
            returnData.Add("method", rowsMethod);
            return returnData;

        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            returnType = 0;
            returnData.Add("key", returnType);
            returnData.Add("specimen", null);
            returnData.Add("method", null);
            return returnData;
        }
    }
    //public Dictionary<string, string> createTest(string labId, string profileId, string testCode, string testName, string testUsefulFor, string testInterpretation, string testLimitation, string testClinicalReferences, string[] hdnAnalyteId, string[] hdnSubAnalyteId, List<Dictionary<string, object>> jsonAnalyteSMR, List<Dictionary<string, object>> jsonSubAnalyteSMR, List<Dictionary<string, object>> jsonAnalyteSMRRefVal, List<Dictionary<string, object>> jsonSubAnalyteSMRRefVal, string Customid)
    //{
    //    string testId = "";
    //    List<Dictionary<string, string>> lstTestAnalyte = new List<Dictionary<string, string>>();
    //    List<Dictionary<string, string>> lstTASM = new List<Dictionary<string, string>>();

    //    List<Dictionary<string, string>> lstTestSubAnalyte = new List<Dictionary<string, string>>();
    //    List<Dictionary<string, string>> lstTSASM = new List<Dictionary<string, string>>();

    //    Dictionary<string, string> returnValue = new Dictionary<string, string>();

    //    try
    //    {
    //        //insert test details
    //        SqlParameter[] param = new SqlParameter[]
    //            {
    //                new SqlParameter("@sTestCode", testCode),
    //                new SqlParameter("@sTestName", testName),
    //                new SqlParameter("@sTestProfileId", profileId),
    //                new SqlParameter("@sTestUsefulFor", testUsefulFor),
    //                new SqlParameter("@sTestInterpretation", testInterpretation),
    //                new SqlParameter("@sTestLimitation", testLimitation),
    //                new SqlParameter("@sTestClinicalReferance", testClinicalReferences),
    //                new SqlParameter("@sCustom", Customid)
    //            };
    //        testId = DAL.ExecuteScalarWithProc("Sp_insertTEST", param);

    //        //insert into testAnalyte

    //        if (testId != "")
    //        {
    //            foreach (var analyteId in hdnAnalyteId)
    //            {
    //                DataSet ds = new DataSet();
    //                SqlParameter[] param1 = new SqlParameter[]
    //                        {
    //                            new SqlParameter("@sTestId",testId),
    //                            new SqlParameter("@sAnalyteId",analyteId)
    //                        };
    //                ds = DAL.ExecuteStoredProcedureDataSet("Sp_AddtestAnalyte", param1);

    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    Dictionary<string, string> row;

    //                    foreach (DataRow dr in ds.Tables[0].Rows)
    //                    {
    //                        row = new Dictionary<string, string>();
    //                        foreach (DataColumn col in ds.Tables[0].Columns)
    //                        {
    //                            row.Add(col.ColumnName.ToString(), dr[col].ToString());
    //                        }
    //                        lstTestAnalyte.Add(row);
    //                    }
    //                }
    //            }
    //        }

    //        //insert testAnalyteSpecimenMethod
    //        if (jsonAnalyteSMR != null)
    //        {
    //            foreach (var record in jsonAnalyteSMR)
    //            {
    //                //get testAnalyteId where testId=current test id and analyteId=analyte for which specimen-method-result is to be added
    //                string testAnalyteId = getTestAnalyteId(lstTestAnalyte, testId, record["analyteId"].ToString());
    //                DataSet ds = new DataSet();
    //                SqlParameter[] param2 = new SqlParameter[]
    //                        {
    //                          new SqlParameter("@sTestAnalyteId", testAnalyteId),
    //                          new SqlParameter("@sSpecimenId", record["specimenId"]),
    //                          new SqlParameter("@sMethodId", record["methodId"]),
    //                          new SqlParameter("@sResultType", record["resultType"]),
    //                        };
    //                ds = DAL.ExecuteStoredProcedureDataSet("Sp_AddtestAnalyteSpecimenMethod", param2);

    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    Dictionary<string, string> row;

    //                    foreach (DataRow dr in ds.Tables[0].Rows)
    //                    {
    //                        row = new Dictionary<string, string>();
    //                        foreach (DataColumn col in ds.Tables[0].Columns)
    //                        {
    //                            row.Add(col.ColumnName, dr[col].ToString());
    //                        }
    //                        row.Add("sAnalyteId", record["analyteId"].ToString());
    //                        row.Add("sTestId", testId);
    //                        lstTASM.Add(row);
    //                    }
    //                }
    //            }
    //        }

    //        //insert testAnalyteReference
    //        if (jsonAnalyteSMRRefVal != null)
    //        {
    //            foreach (var record in jsonAnalyteSMRRefVal)
    //            {
    //                ArrayList refVal = record["RefVal"] as ArrayList;
    //                for (int i = 0; i < refVal.Count; i++)
    //                {
    //                    Dictionary<string, object> refValElements = refVal[i] as Dictionary<string, object>;

    //                    //get testAnalyteId where testId=current test id and analyteId=analyte for which specimen-method-result is to be added
    //                    string testAnalyteId = getTestAnalyteId(lstTestAnalyte, testId, record["analyteId"].ToString());

    //                    //get TASMId
    //                    string TASMId = getTASMId(lstTASM, testAnalyteId, record["specimenId"].ToString(), record["methodId"].ToString(), record["resultType"].ToString());
    //                    SqlParameter[] param2 = new SqlParameter[]
    //                    {
    //                        new SqlParameter("@sTASMId", TASMId),
    //                        new SqlParameter("@sReferenceType", refValElements["refType"]),
    //                        new SqlParameter("@sAge", refValElements["age"]),
    //                        new SqlParameter("@sMale", refValElements["male"]),
    //                        new SqlParameter("@sFemale", refValElements["female"]),
    //                        new SqlParameter("@sGrade", refValElements["grade"]),
    //                        new SqlParameter("@sUnits", refValElements["units"]),
    //                        new SqlParameter("@sInterpretation", refValElements["interpretation"]),
    //                        new SqlParameter("@sLowerLimit", refValElements["lowerLimit"]),
    //                        new SqlParameter("@sUpperLimit", refValElements["upperLimit"]),
    //                    };
    //                    DAL.ExecuteStoredProcedure("Sp_AddtestAnalyteReference", param2);
    //                }
    //            }
    //        }

    //        //insert into testSubAnalyte
    //        if (hdnSubAnalyteId != null)
    //        {
    //            foreach (var subAnalyteId in hdnSubAnalyteId)
    //            {
    //                DataSet ds = new DataSet();
    //                SqlParameter[] param3 = new SqlParameter[]
    //                        {
    //                            new SqlParameter("@sTestId", testId),
    //                            new SqlParameter("@sSubAnalyteId", subAnalyteId)
    //                        };
    //                ds = DAL.ExecuteStoredProcedureDataSet("Sp_AddtestSubAnalyte", param3);

    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    Dictionary<string, string> row;

    //                    foreach (DataRow dr in ds.Tables[0].Rows)
    //                    {
    //                        row = new Dictionary<string, string>();
    //                        foreach (DataColumn col in ds.Tables[0].Columns)
    //                        {
    //                            row.Add(col.ColumnName.ToString(), dr[col].ToString());
    //                        }
    //                        lstTestSubAnalyte.Add(row);
    //                    }
    //                }
    //            }
    //        }
    //        //insert testSubAnalyteSpecimenMethod
    //        if (jsonSubAnalyteSMR != null)
    //        {
    //            foreach (var record in jsonSubAnalyteSMR)
    //            {
    //                //get testSubAnalyteId where testId=current test id and subanalyteId=subanalyte for which specimen-method-result is to be added
    //                string testSubAnalyteId = getTestSubAnalyteId(lstTestSubAnalyte, testId, record["subAnalyteId"].ToString());
    //                DataSet ds = new DataSet();
    //                SqlParameter[] param4 = new SqlParameter[]
    //                    {
    //                        new SqlParameter("@sTestSubAnalyteId", testSubAnalyteId),
    //                        new SqlParameter("@sSpecimenId", record["specimenId"]),
    //                        new SqlParameter("@sMethodId", record["methodId"]),
    //                        new SqlParameter("@sResultType", record["resultType"])
    //                    };
    //                ds = DAL.ExecuteStoredProcedureDataSet("Sp_AddtestSubAnalyteSpecimenMethod", param4);
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    Dictionary<string, string> row;

    //                    foreach (DataRow dr in ds.Tables[0].Rows)
    //                    {
    //                        row = new Dictionary<string, string>();
    //                        foreach (DataColumn col in ds.Tables[0].Columns)
    //                        {
    //                            row.Add(col.ColumnName, dr[col].ToString());
    //                        }
    //                        row.Add("sSubAnalyteId", record["subAnalyteId"].ToString());
    //                        row.Add("sTestId", testId);
    //                        lstTSASM.Add(row);
    //                    }
    //                }
    //            }
    //        }

    //        //insert testSubAnalyteReference
    //        if (jsonSubAnalyteSMRRefVal != null)
    //        {
    //            foreach (var record in jsonSubAnalyteSMRRefVal)
    //            {
    //                ArrayList refVal = record["RefVal"] as ArrayList;
    //                for (int i = 0; i < refVal.Count; i++)
    //                {
    //                    Dictionary<string, object> refValElements = refVal[i] as Dictionary<string, object>;

    //                    //get testSubAnalyteId where testId=current test id and subanalyteId=subanalyte for which specimen-method-result is to be added
    //                    string testSubAnalyteId = getTestSubAnalyteId(lstTestSubAnalyte, testId, record["subAnalyteId"].ToString());

    //                    //get TSASMId
    //                    string TSASMId = getTSASMId(lstTSASM, testSubAnalyteId, record["specimenId"].ToString(), record["methodId"].ToString(), record["resultType"].ToString());
    //                    SqlParameter[] param5 = new SqlParameter[]
    //                    {
    //                       new SqlParameter("@sTSASMId", TSASMId),
    //                       new SqlParameter("@sReferenceType", refValElements["refType"]),
    //                       new SqlParameter("@sAge", refValElements["age"]),
    //                       new SqlParameter("@sMale", refValElements["male"]),
    //                       new SqlParameter("@sFemale", refValElements["female"]),
    //                       new SqlParameter("@sGrade", refValElements["grade"]),
    //                       new SqlParameter("@sUnits", refValElements["units"]),
    //                       new SqlParameter("@sInterpretation", refValElements["interpretation"]),
    //                       new SqlParameter("@sLowerLimit", refValElements["lowerLimit"]),
    //                       new SqlParameter("@sUpperLimit", refValElements["upperLimit"])
    //                    };
    //                    DAL.ExecuteStoredProcedure("Sp_AddtestSubAnalyteReference", param5);
    //                }
    //            }
    //        }
    //        //associate this test with the lab
    //        SqlParameter[] param6 = new SqlParameter[]
    //        {
    //            new SqlParameter("@sLabId", labId),
    //            new SqlParameter("@sTestId", testId),
    //            new SqlParameter("@sPrice", "0"),
    //            new SqlParameter("@sMyTest", "1")
    //        };
    //        DAL.ExecuteStoredProcedure("Sp_Addtestprice", param6);
    //        returnValue.Add("key", "1");
    //        returnValue.Add("value", "success");
    //        return returnValue;
    //    }
    //    catch (Exception ex)
    //    {
    //        returnValue.Add("key", "0");
    //        returnValue.Add("value", ex.Message);

    //        return returnValue;
    //    }
    //}

    public string getTestAnalyteId(List<Dictionary<string, string>> lstTestAnalyte, string testId, string analyteId)
    {
        foreach (var record in lstTestAnalyte)
        {
            if (record["sTestId"] == testId && record["sAnalyteId"] == analyteId)
            {
                return record["sTestAnalyteId"];
            }
        }
        return "";
    }
    public string getTASMId(List<Dictionary<string, string>> lstTASM, string testAnalyteId, string specimenId, string methodId, string resultType)
    {
        foreach (var record in lstTASM)
        {
            if (record["sTestAnalyteId"] == testAnalyteId && record["sSpecimenId"] == specimenId && record["sMethodId"] == methodId && record["sResultType"] == resultType)
            {
                return record["sTASMId"];
            }
        }
        return "";
    }
    public string getTestSubAnalyteId(List<Dictionary<string, string>> lstTestSubAnalyte, string testId, string subAnalyteId)
    {
        foreach (var record in lstTestSubAnalyte)
        {
            if (record["sTestId"] == testId && record["sSubAnalyteId"] == subAnalyteId)
            {
                return record["sTestSubAnalyteId"];
            }
        }
        return "";
    }
    public string getTSASMId(List<Dictionary<string, string>> lstTSASM, string testSubAnalyteId, string specimenId, string methodId, string resultType)
    {
        foreach (var record in lstTSASM)
        {
            if (record["sTestSubAnalyteId"] == testSubAnalyteId && record["sSpecimenId"] == specimenId && record["sMethodId"] == methodId && record["sResultType"] == resultType)
            {
                return record["sTSASMId"];
            }
        }
        return "";
    }
    public Dictionary<string, string> createTest(string labId, string profileId, string testCode, string testName, string testUsefulFor, string testInterpretation, string testLimitation, string testClinicalReferences, string[] hdnAnalyteId, string[] hdnSubAnalyteId, List<Dictionary<string, object>> jsonAnalyteSMR, List<Dictionary<string, object>> jsonSubAnalyteSMR, List<Dictionary<string, object>> jsonAnalyteSMRRefVal, List<Dictionary<string, object>> jsonSubAnalyteSMRRefVal, string Customid)
    {
        string testId = "";
        List<Dictionary<string, string>> lstTestAnalyte = new List<Dictionary<string, string>>();
        List<Dictionary<string, string>> lstTASM = new List<Dictionary<string, string>>();

        List<Dictionary<string, string>> lstTestSubAnalyte = new List<Dictionary<string, string>>();
        List<Dictionary<string, string>> lstTSASM = new List<Dictionary<string, string>>();

        Dictionary<string, string> returnValue = new Dictionary<string, string>();

        try
        {
            //insert test details
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sTestCode", testCode),
                    new SqlParameter("@sTestName", testName),
                    new SqlParameter("@sTestProfileId", profileId),
                    new SqlParameter("@sTestUsefulFor", testUsefulFor),
                    new SqlParameter("@sTestInterpretation", testInterpretation),
                    new SqlParameter("@sTestLimitation", testLimitation),
                    new SqlParameter("@sTestClinicalReferance", testClinicalReferences),
                    new SqlParameter("@sCustom", Customid)
                };
            testId = DAL.ExecuteScalarWithProc("Sp_insertTEST", param);

            //insert into testAnalyte

            if (testId != "")
            {
                foreach (var analyteId in hdnAnalyteId)
                {
                    DataSet ds = new DataSet();
                    SqlParameter[] param1 = new SqlParameter[]
                            {
                                new SqlParameter("@sTestId",testId),
                                new SqlParameter("@sAnalyteId",analyteId)
                            };
                    ds = DAL.ExecuteStoredProcedureDataSet("Sp_AddtestAnalyte", param1);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Dictionary<string, string> row;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            row = new Dictionary<string, string>();
                            foreach (DataColumn col in ds.Tables[0].Columns)
                            {
                                row.Add(col.ColumnName.ToString(), dr[col].ToString());
                            }
                            lstTestAnalyte.Add(row);
                        }
                    }
                }
            }

            //insert testAnalyteSpecimenMethod
            if (jsonAnalyteSMR != null)
            {
                foreach (var record in jsonAnalyteSMR)
                {
                    //get testAnalyteId where testId=current test id and analyteId=analyte for which specimen-method-result is to be added
                    string testAnalyteId = getTestAnalyteId(lstTestAnalyte, testId, record["analyteId"].ToString());
                    DataSet ds = new DataSet();
                    SqlParameter[] param2 = new SqlParameter[]
                            {
                              new SqlParameter("@sTestAnalyteId", testAnalyteId),
                              new SqlParameter("@sSpecimenId", record["specimenId"]),
                              new SqlParameter("@sMethodId", record["methodId"]),
                              new SqlParameter("@sResultType", record["resultType"]),
                            };
                    ds = DAL.ExecuteStoredProcedureDataSet("Sp_AddtestAnalyteSpecimenMethod", param2);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Dictionary<string, string> row;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            row = new Dictionary<string, string>();
                            foreach (DataColumn col in ds.Tables[0].Columns)
                            {
                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            row.Add("sAnalyteId", record["analyteId"].ToString());
                            row.Add("sTestId", testId);
                            lstTASM.Add(row);
                        }
                    }
                }
            }

            //insert testAnalyteReference
            if (jsonAnalyteSMRRefVal != null)
            {
                foreach (var record in jsonAnalyteSMRRefVal)
                {
                    ArrayList refVal = record["RefVal"] as ArrayList;
                    for (int i = 0; i < refVal.Count; i++)
                    {
                        Dictionary<string, object> refValElements = refVal[i] as Dictionary<string, object>;

                        //get testAnalyteId where testId=current test id and analyteId=analyte for which specimen-method-result is to be added
                        string testAnalyteId = getTestAnalyteId(lstTestAnalyte, testId, record["analyteId"].ToString());
                        string x = "sdfghdsf dsfgdsf";
                        string z = refValElements["male"].ToString().Split(' ')[0];

                        //get TASMId
                        string TASMId = getTASMId(lstTASM, testAnalyteId, record["specimenId"].ToString(), record["methodId"].ToString(), record["resultType"].ToString());
                        SqlParameter[] param2 = new SqlParameter[]
                        {
                            new SqlParameter("@LabId", labId),
                            new SqlParameter("@TASMId", TASMId),
                            new SqlParameter("@ReferenceType", refValElements["refType"]),
                            new SqlParameter("@MaleFromAge", refValElements["MaleFromAge"]),
                            new SqlParameter("@MaleToAge", refValElements["MaleToAge"]),
                            new SqlParameter("@MaleAgeUnit", refValElements["MaleAgeUnit"]),
                            new SqlParameter("@MaleReferenceMinValue", refValElements["male"].ToString().Split('-')[0]),
                            new SqlParameter("@MaleReferenceMaxValue", refValElements["male"].ToString().Split('-')[1]),
                            new SqlParameter("@FemaleFromAge", refValElements["FemaleFromAge"]),
                            new SqlParameter("@FemaleToAge", refValElements["FemaleToAge"]),
                            new SqlParameter("@FemaleAgeUnit", refValElements["FemaleAgeUnit"]),
                            new SqlParameter("@FemaleReferenceMinValue", refValElements["female"].ToString().Split('-')[0]),
                            new SqlParameter("@FemaleReferenceMaxValue", refValElements["female"].ToString().Split('-')[1]),
                            new SqlParameter("@sGrade", refValElements["grade"]),
                            new SqlParameter("@sUnits", refValElements["units"]),
                            new SqlParameter("@sInterpretation", refValElements["interpretation"]),
                            new SqlParameter("@sLowerLimit", refValElements["lowerLimit"]),
                            new SqlParameter("@sUpperLimit", refValElements["upperLimit"]),
                        };
                        DAL.ExecuteStoredProcedure("Sp_AddtestAnalyteReferenceRangeValues", param2);
                    }
                }
            }

            //insert into testSubAnalyte
            if (hdnSubAnalyteId != null)
            {
                foreach (var subAnalyteId in hdnSubAnalyteId)
                {
                    DataSet ds = new DataSet();
                    SqlParameter[] param3 = new SqlParameter[]
                            {
                                new SqlParameter("@sTestId", testId),
                                new SqlParameter("@sSubAnalyteId", subAnalyteId)
                            };
                    ds = DAL.ExecuteStoredProcedureDataSet("Sp_AddtestSubAnalyte", param3);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Dictionary<string, string> row;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            row = new Dictionary<string, string>();
                            foreach (DataColumn col in ds.Tables[0].Columns)
                            {
                                row.Add(col.ColumnName.ToString(), dr[col].ToString());
                            }
                            lstTestSubAnalyte.Add(row);
                        }
                    }
                }
            }
            //insert testSubAnalyteSpecimenMethod
            if (jsonSubAnalyteSMR != null)
            {
                foreach (var record in jsonSubAnalyteSMR)
                {
                    //get testSubAnalyteId where testId=current test id and subanalyteId=subanalyte for which specimen-method-result is to be added
                    string testSubAnalyteId = getTestSubAnalyteId(lstTestSubAnalyte, testId, record["subAnalyteId"].ToString());
                    DataSet ds = new DataSet();
                    SqlParameter[] param4 = new SqlParameter[]
                        {
                            new SqlParameter("@sTestSubAnalyteId", testSubAnalyteId),
                            new SqlParameter("@sSpecimenId", record["specimenId"]),
                            new SqlParameter("@sMethodId", record["methodId"]),
                            new SqlParameter("@sResultType", record["resultType"])
                        };
                    ds = DAL.ExecuteStoredProcedureDataSet("Sp_AddtestSubAnalyteSpecimenMethod", param4);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Dictionary<string, string> row;

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            row = new Dictionary<string, string>();
                            foreach (DataColumn col in ds.Tables[0].Columns)
                            {
                                row.Add(col.ColumnName, dr[col].ToString());
                            }
                            row.Add("sSubAnalyteId", record["subAnalyteId"].ToString());
                            row.Add("sTestId", testId);
                            lstTSASM.Add(row);
                        }
                    }
                }
            }

            //insert testSubAnalyteReference
            if (jsonSubAnalyteSMRRefVal != null)
            {
                foreach (var record in jsonSubAnalyteSMRRefVal)
                {
                    ArrayList refVal = record["RefVal"] as ArrayList;
                    for (int i = 0; i < refVal.Count; i++)
                    {
                        Dictionary<string, object> refValElements = refVal[i] as Dictionary<string, object>;

                        //get testSubAnalyteId where testId=current test id and subanalyteId=subanalyte for which specimen-method-result is to be added
                        string testSubAnalyteId = getTestSubAnalyteId(lstTestSubAnalyte, testId, record["subAnalyteId"].ToString());

                        //get TSASMId
                        string TSASMId = getTSASMId(lstTSASM, testSubAnalyteId, record["specimenId"].ToString(), record["methodId"].ToString(), record["resultType"].ToString());
                        SqlParameter[] param5 = new SqlParameter[]
                        {
                            new SqlParameter("@LabId", labId),
                            new SqlParameter("@TSASMId", TSASMId),
                            new SqlParameter("@ReferenceType", refValElements["refType"]),
                            new SqlParameter("@MaleFromAge", refValElements["MaleFromAge"]),
                            new SqlParameter("@MaleToAge", refValElements["MaleToAge"]),
                            new SqlParameter("@MaleAgeUnit", refValElements["MaleAgeUnit"]),
                            new SqlParameter("@MaleReferenceMinValue", refValElements["male"].ToString().Split('-')[0]),
                            new SqlParameter("@MaleReferenceMaxValue", refValElements["male"].ToString().Split('-')[1]),
                            new SqlParameter("@FemaleFromAge", refValElements["FemaleFromAge"]),
                            new SqlParameter("@FemaleToAge", refValElements["FemaleToAge"]),
                            new SqlParameter("@FemaleAgeUnit", refValElements["FemaleAgeUnit"]),
                            new SqlParameter("@FemaleReferenceMinValue", refValElements["female"].ToString().Split('-')[0]),
                            new SqlParameter("@FemaleReferenceMaxValue", refValElements["female"].ToString().Split('-')[1]),
                            new SqlParameter("@sGrade", refValElements["grade"]),
                            new SqlParameter("@sUnits", refValElements["units"]),
                            new SqlParameter("@sInterpretation", refValElements["interpretation"]),
                            new SqlParameter("@sLowerLimit", refValElements["lowerLimit"]),
                            new SqlParameter("@sUpperLimit", refValElements["upperLimit"]),
                        };
                        DAL.ExecuteStoredProcedure("Sp_AddtestSubAnalyteReferenceRangeValue", param5);
                    }
                }
            }
            //associate this test with the lab
            SqlParameter[] param6 = new SqlParameter[]
            {
                new SqlParameter("@sLabId", labId),
                new SqlParameter("@sTestId", testId),
                new SqlParameter("@sPrice", "0"),
                new SqlParameter("@sMyTest", "1")
            };
            DAL.ExecuteStoredProcedure("Sp_Addtestprice", param6);
            returnValue.Add("key", "1");
            returnValue.Add("value", "success");
            return returnValue;
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            returnValue.Add("key", "0");
            returnValue.Add("value", ex.Message);

            return returnValue;
        }
    }

}
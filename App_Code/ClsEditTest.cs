using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;
public class ClsEditTest
{
    DataAccessLayer DAL = new DataAccessLayer();
    public ClsEditTest()
    {

    }
    public DataSet getTest(string labId, string testId)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
               new SqlParameter("@TestId",testId),
               new SqlParameter("@labId",labId)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetTest", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestAnalyte(string testId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetTestAnalyteByTestId " + testId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestSubAnalyte(string testId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetTestSubAnalyteByTestId " + testId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public Dictionary<string, object> updateTASM(string tasmId, string sampleType, string quantity, string timePeriod, string method, string resultType, string testAnalyteId)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            string specimenId = "";
            string methodId = "";
         
            //insert specimen into table only if it does not exist
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sSampleType",sampleType),
                    new SqlParameter("@sQuantity",quantity),
                    new SqlParameter("@sTimePeriod",timePeriod)
                };
            specimenId = DAL.ExecuteScalarWithProc("Sp_UpdateTASM", param);
            if (method != "")
            {
                //insert method into table only if it does not exist
                SqlParameter[] param1 = new SqlParameter[]
                     {
                           new SqlParameter("@sMethodName",method)
                     };
                methodId = DAL.ExecuteScalarWithProc("Sp_InsertNewMethod", param1);
            }
            if (specimenId != "")
            {
                SqlParameter[] param2 = new SqlParameter[]
                        {
                            new SqlParameter("@sSpecimenId",specimenId),
                            new SqlParameter("@sMethodId",methodId),
                            new SqlParameter("@sResultType",resultType),
                            new SqlParameter("@sTestAnalyteId",testAnalyteId)
                        };
                string result = DAL.ExecuteScalarWithProc("Sp_GettestAnalyteSpecimenMethod", param2);
                string existingId = result != null ? result : "";
                if (existingId != "")
                {
                    returnType = 2;
                    returnData.Add("key", returnType);
                    return returnData;
                }
                SqlParameter[] param3 = new SqlParameter[]
                    {
                        new SqlParameter("@sSpecimenId",specimenId),
                        new SqlParameter("@sMethodId",methodId),
                        new SqlParameter("@sResultType",resultType),
                        new SqlParameter("@sTASMId",tasmId)
                    };
                DAL.ExecuteStoredProcedure("Sp_UpdatetestAnalyteSpecimenMethod", param3);
            }
            returnType = 1;
            returnData.Add("key", returnType);
            return returnData;
        }
        catch (Exception)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            return returnData;
        }
    }
    public Dictionary<string, object> updateTSASM(string tsasmId, string sampleType, string quantity, string timePeriod, string method, string resultType, string testSubAnalyteId)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            string specimenId = "";
            string methodId = "";
            //insert specimen into table only if it does not exist
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sSampleType",sampleType),
                    new SqlParameter("@sQuantity",quantity),
                    new SqlParameter("@sTimePeriod",timePeriod)
                };
            specimenId = DAL.ExecuteScalarWithProc("Sp_insertspecimen", param);
            if (method != "")
            {
                ////insert method into table only if it does not exist
                SqlParameter[] param1 = new SqlParameter[]
                    {
                        new SqlParameter("@sMethodName",method)
                    };
                methodId = DAL.ExecuteScalarWithProc("Sp_InsertNewMethod", param1);
            }
            if (specimenId != "")
            {
                SqlParameter[] param2 = new SqlParameter[]
                        {
                            new SqlParameter("@sSpecimenId",specimenId),
                            new SqlParameter("@sMethodId",methodId),
                            new SqlParameter("@sResultType",resultType),
                            new SqlParameter("@sTestSubAnalyteId",testSubAnalyteId)
                        };
                string result = DAL.ExecuteScalarWithProc("Sp_GettestSubAnalyteSpecimenMethod", param2);
                string existingId = result != null ? result : "";
                if (existingId != "")
                {
                    returnType = 2;
                    returnData.Add("key", returnType);
                    return returnData;
                }
            }
            SqlParameter[] param3 = new SqlParameter[]
                        {
                            new SqlParameter("@sSpecimenId",specimenId),
                            new SqlParameter("@sMethodId",methodId),
                            new SqlParameter("@sResultType",resultType),
                            new SqlParameter("@sTSASMId",tsasmId)
                        };
            DAL.ExecuteStoredProcedure("Sp_UpdatetestSubAnalyteSpecimenMethod", param3);
            returnType = 1;
            returnData.Add("key", returnType);
            return returnData;
        }
        catch (Exception)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            return returnData;
        }
    }
    public Dictionary<string, object> updateAnalyteReference(string testSMId, string testReferenceId, string referenceType, string ageRange, string male, string female, string grade, string units, string interpretation, string lowerLimit, string upperLimit)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        string existingId = "";
        try
        {
            SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sTASMId",testSMId),
                        new SqlParameter("@sReferenceType",referenceType),
                        new SqlParameter("@sAge",ageRange),
                        new SqlParameter("@sMale",male),
                        new SqlParameter("@sFemale",female),
                        new SqlParameter("@sGrade",grade),
                        new SqlParameter("@sUnits",units),
                        new SqlParameter("@sInterpretation",interpretation),
                        new SqlParameter("@sUpperLimit",upperLimit),
                        new SqlParameter("@sLowerLimit",lowerLimit)
                    };
            existingId = DAL.ExecuteScalarWithProc("Sp_GettestAnalyteReference", param);
            if (existingId != "")
            {
                returnType = 2;
                returnData.Add("key", returnType);
                return returnData;
            }
            //update TestAnalyteReference
            SqlParameter[] param1 = new SqlParameter[]
                    {
                        new SqlParameter("@sTestAnalyteReferenceId",testSMId),
                        new SqlParameter("@sReferenceType",referenceType),
                        new SqlParameter("@sAge",ageRange),
                        new SqlParameter("@sMale",male),
                        new SqlParameter("@sFemale",female),
                        new SqlParameter("@sGrade",grade),
                        new SqlParameter("@sUnits",units),
                        new SqlParameter("@sInterpretation",interpretation),
                        new SqlParameter("@sUpperLimit",upperLimit),
                        new SqlParameter("@sLowerLimit",upperLimit)
                    };
            DAL.ExecuteStoredProcedure("Sp_UpdateTestAnalyteReference", param1);
            returnType = 1;
            returnData.Add("key", returnType);
            return returnData;
        }
        catch (Exception)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            return returnData;
        }
    }
    public Dictionary<string, object> updateSubAnalyteReference(string testSMId, string testReferenceId, string referenceType, string ageRange, string male, string female, string grade, string units, string interpretation, string lowerLimit, string upperLimit)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        string existingId = "";
        try
        {
            SqlParameter[] param = new SqlParameter[]
                     {
                         new SqlParameter("@sTSASMId",testSMId),
                         new SqlParameter("@sReferenceType",referenceType),
                         new SqlParameter("@sAge",ageRange),
                         new SqlParameter("@sMale",male),
                         new SqlParameter("@sFemale",female),
                         new SqlParameter("@sGrade",grade),
                         new SqlParameter("@sUnits",units),
                         new SqlParameter("@sInterpretation",interpretation),
                         new SqlParameter("@sUpperLimit",upperLimit),
                         new SqlParameter("@sLowerLimit",lowerLimit)
                     };
            existingId = DAL.ExecuteScalarWithProc("Sp_GettestSubAnalyteReference", param);
            if (existingId != "")
            {
                returnType = 2;
                returnData.Add("key", returnType);
                return returnData;
            }
            //update TestSubAnalyteReference
            SqlParameter[] param1 = new SqlParameter[]
                     {
                         new SqlParameter("@sTestSubAnalyteReferenceId",testReferenceId),
                         new SqlParameter("@sReferenceType",referenceType),
                         new SqlParameter("@sAge",ageRange),
                         new SqlParameter("@sMale",male),
                         new SqlParameter("@sFemale",female),
                         new SqlParameter("@sGrade",grade),
                         new SqlParameter("@sUnits",units),
                         new SqlParameter("@sInterpretation",interpretation),
                         new SqlParameter("@sUpperLimit",upperLimit),
                         new SqlParameter("@sLowerLimit",lowerLimit),
                     };
            DAL.ExecuteStoredProcedure("Sp_UpdateTestSubAnalyteReference", param1);
            returnType = 1;
            returnData.Add("key", returnType);
            return returnData;
        }
        catch (Exception)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            return returnData;
        }
    }
    public Dictionary<string, object> addAnalyteReference(string testSMId, string referenceType, string ageRange, string male, string female, string grade, string units, string interpretation, string lowerLimit, string upperLimit)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        string existingId = "";
        try
        {
            SqlParameter[] param3 = new SqlParameter[]
                        {
                         new SqlParameter("@sTASMId", testSMId),
                         new SqlParameter("@sReferenceType", referenceType),
                         new SqlParameter("@sAge", ageRange),
                         new SqlParameter("@sMale", male),
                         new SqlParameter("@sFemale", female),
                         new SqlParameter("@sGrade", grade),
                         new SqlParameter("@sUnits", units),
                         new SqlParameter("@sInterpretation", interpretation),
                         new SqlParameter("@sUpperLimit", upperLimit),
                         new SqlParameter("@sLowerLimit", lowerLimit)
                       };
            existingId = DAL.ExecuteScalarWithProc("Sp_GettestAnalyteReference", param3);
            if (existingId != "")
            {
                returnType = 2;
                returnData.Add("key", returnType);
                return returnData;
            }
            //insert TestAnalyteReference
            SqlParameter[] param4 = new SqlParameter[]
                        {
                             new SqlParameter("@sReferenceType", referenceType),
                             new SqlParameter("@sAge", ageRange),
                             new SqlParameter("@sMale", male),
                             new SqlParameter("@sFemale", female),
                             new SqlParameter("@sGrade", grade),
                             new SqlParameter("@sUnits", units),
                             new SqlParameter("@sInterpretation", interpretation),
                             new SqlParameter("@sUpperLimit", upperLimit),
                             new SqlParameter("@sLowerLimit", lowerLimit),
                             new SqlParameter("@sTASMId", testSMId)
                        };
            DAL.ExecuteStoredProcedure("Sp_InsertTestAnalyteReference", param4);
            returnType = 1;
            returnData.Add("key", returnType);
            return returnData;
        }
        catch (Exception)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            return returnData;
        }
    }
    public Dictionary<string, object> addSubAnalyteReference(string testSMId, string referenceType, string ageRange, string male, string female, string grade, string units, string interpretation, string lowerLimit, string upperLimit)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        string existingId = "";
        try
        {
            SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sTSASMId", testSMId),
                        new SqlParameter("@sReferenceType", referenceType),
                        new SqlParameter("@sAge", ageRange),
                        new SqlParameter("@sMale", male),
                        new SqlParameter("@sFemale", female),
                        new SqlParameter("@sGrade", grade),
                        new SqlParameter("@sUnits", units),
                        new SqlParameter("@sInterpretation", interpretation),
                        new SqlParameter("@sUpperLimit", upperLimit),
                        new SqlParameter("@sLowerLimit", lowerLimit),
                    };
            existingId = DAL.ExecuteScalarWithProc("Sp_GettestAnalyteReference", param);

            if (existingId != "")
            {
                returnType = 2;
                returnData.Add("key", returnType);
                return returnData;
            }
            //insert TestAnalyteReference
            SqlParameter[] Param1 = new SqlParameter[]
            {
                new SqlParameter("@sReferenceType", referenceType),
                new SqlParameter("@sAge", ageRange),
                new SqlParameter("@sMale", male),
                new SqlParameter("@sFemale", female),
                new SqlParameter("@sGrade", grade),
                new SqlParameter("@sUnits", units),
                new SqlParameter("@sInterpretation", interpretation),
                new SqlParameter("@sUpperLimit", upperLimit),
                new SqlParameter("@sLowerLimit", lowerLimit),
                new SqlParameter("@sTSASMId", testSMId)
            };
            DAL.ExecuteStoredProcedure("Sp_InsertTestAnalyteReference", Param1);
            returnType = 1;
            returnData.Add("key", returnType);
            return returnData;
        }
        catch (Exception)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            return returnData;
        }
    }
    public Dictionary<string, object> deleteAnalyteReference(string tarId)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sTestAnalyteReferenceId",tarId)
                };
            DAL.ExecuteStoredProcedure("Sp_DeleteTestAnalyteReference ", param);

            returnType = 1;
            returnData.Add("key", returnType);
            return returnData;
        }
        catch (Exception)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            return returnData;
        }
    }
    public Dictionary<string, object> deleteSubAnalyteReference(string tsarId)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@@sTestSubAnalyteReferenceId",tsarId)
                };
            DAL.ExecuteStoredProcedure("Sp_DeletetestSubAnalyteReference ", param);
            returnType = 1;
            returnData.Add("key", returnType);
            return returnData;
        }
        catch (Exception)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            return returnData;
        }
    }
    public string addTASM(string testAnalyteId, string sampleType, string quantity, string timePeriod, string method, string resultType)
    {
        try
        {
            string specimenId = "";
            string methodId = "";
            string existingId = "";
            //insert specimen into table only if it does not exist
            SqlParameter[] Param = new SqlParameter[]
                {
                    new SqlParameter("@sSampleType",sampleType),
                    new SqlParameter("@sQuantity",quantity),
                    new SqlParameter("@sTimePeriod",timePeriod),
                };
            specimenId = DAL.ExecuteScalarWithProc("Sp_insertspecimen", Param);
            if (method != "")
            {
                //insert method into table only if it does not exist
                SqlParameter[] Param1 = new SqlParameter[]
                         {
                             new SqlParameter("@sMethodName",method)
                         };
                methodId = DAL.ExecuteScalarWithProc("Sp_InsertNewMethod", Param1);
            }
            if (specimenId != "")
            {
                SqlParameter[] Param2 = new SqlParameter[]
                          {
                                new SqlParameter("@sSpecimenId",specimenId),
                                new SqlParameter("@sMethodId",methodId),
                                new SqlParameter("@sResultType",resultType),
                                new SqlParameter("@sTestAnalyteId", testAnalyteId)
                            };
                existingId = DAL.ExecuteScalarWithProc("Sp_GettestAnalyteSpecimenMethod", Param2);
                if (existingId != "")
                {
                    return "2";
                }
            }
            //add TASM
            SqlParameter[] param3 = new SqlParameter[]
            {
                 new SqlParameter("@sSpecimenId", specimenId),
                 new SqlParameter("@sMethodId", methodId),
                 new SqlParameter("@sResultType", resultType),
                 new SqlParameter("@sTestAnalyteId", testAnalyteId)
            };
            DAL.ExecuteStoredProcedure("Sp_inserttestAnalyteSpecimenMethod", param3);
            return "1";
        }
        catch (Exception)
        {
            return "0";
        }
    }
    public string addTSASM(string testSubAnalyteId, string sampleType, string quantity, string timePeriod, string method, string resultType)
    {
        try
        {
            string specimenId = "", methodId = "", existingId = "";
            //insert specimen into table only if it does not exist
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sSampleType", sampleType),
                    new SqlParameter("@sQuantity", quantity),
                    new SqlParameter("@sTimePeriod", timePeriod),
                };
            specimenId = DAL.ExecuteScalarWithProc("Sp_AddTSASM", param);
            if (method != "")
            {
                //insert method into table only if it does not exist
                SqlParameter[] param1 = new SqlParameter[]
                    {
                        new SqlParameter("@sMethodName",method)
                    };
                methodId = DAL.ExecuteScalarWithProc("Sp_InsertNewMethod", param1);
            }
            if (specimenId != "")
            {
                SqlParameter[] param2 = new SqlParameter[]
                        {
                            new SqlParameter("@sSpecimenId", specimenId),
                            new SqlParameter("@sMethodId", methodId),
                            new SqlParameter("@sResultType", resultType),
                            new SqlParameter("@sTestSubAnalyteId", testSubAnalyteId)
                        };
                existingId = DAL.ExecuteScalarWithProc("Sp_GettestSubAnalyteSpecimenMethod", param2);
                if (existingId != "")
                {
                    return "2";
                }                SqlParameter[] param3 = new SqlParameter[]
                    {
                        new SqlParameter("@sSpecimenId", specimenId),
                        new SqlParameter("@sMethodId", methodId),
                        new SqlParameter("@sResultType", resultType),
                        new SqlParameter("@sTestSubAnalyteId", testSubAnalyteId),
                    };
                DAL.ExecuteStoredProcedure("Sp_InserttestSubAnalyteSpecimenMethod", param3);
                return "1";
            }
            return "0";
        }
        catch (Exception)
        {
            return "0";
        }
    }
    public Dictionary<string, object> deleteAnalyteSMR(string tasmId)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            //delete TestAnalyteReference
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sTASMId",tasmId)
                };
            DAL.ExecuteStoredProcedure("Sp_DeletetestAnalyteReferenc", param);
            //delete TestAnalyteSpecimenMethod
            SqlParameter[] param1 = new SqlParameter[]
                {
                    new SqlParameter("@sTASMId",tasmId)
                };
            DAL.ExecuteStoredProcedure("Sp_DeletetestAnalyteSpecimenMethod", param1);
            returnType = 1;
            returnData.Add("key", returnType);
            return returnData;
        }
        catch (Exception)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            return returnData;
        }
    }
    public Dictionary<string, object> deleteSubAnalyteSMR(string tsasmId)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            //delete TestAnalyteReference
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sTSASMId",tsasmId)
                };
            DAL.ExecuteStoredProcedure("Sp_DeletetestSubAnalyteReferenceBysTSASMId", param);
            //delete TestAnalyteSpecimenMethod
            SqlParameter[] param1 = new SqlParameter[]
                {
                    new SqlParameter("@sTSASMId",tsasmId)
                };
            DAL.ExecuteStoredProcedure("Sp_DeletetestSubAnalyteSpecimenMethod", param1);
            returnType = 1;
            returnData.Add("key", returnType);
            return returnData;
        }
        catch (Exception ex)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            return returnData;
        }
    }
    public DataSet getAnalyteNotInTest(string testId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetAnalyteNotInTest " + testId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public Dictionary<string, object> addAnalyte(string testId, string analyteId, string analyteName)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            if (analyteId == "" || analyteId == null)
            {
                string newAnalyteId = "";
                SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sAnalyteName",analyteName)
                    };
                DataTable dt = DAL.ExecuteStoredProcedureDataTable("Sp_InsertAnalyte", param);
                if (dt.Rows.Count > 0)
                {
                    newAnalyteId = dt.Rows[0]["sAnalyteId"].ToString();
                };
                SqlParameter[] param1 = new SqlParameter[]
                    {
                        new SqlParameter("@sTestId",testId),
                        new SqlParameter("@sAnalyteId",newAnalyteId)
                    };
                DAL.ExecuteStoredProcedure("Sp_InsertTestAnalyte", param1);
            }
            else
            {
                SqlParameter[] param1 = new SqlParameter[]
                    {
                        new SqlParameter("@sTestId",testId),
                        new SqlParameter("@sAnalyteId",analyteId)
                    };
                DAL.ExecuteStoredProcedure("Sp_InsertTestAnalyte", param1);
            }
            returnType = 1;
            returnData.Add("key", returnType);
            return returnData;
        }
        catch (Exception ex)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            return returnData;
        }
    }
    public DataSet getSubAnalyteNotInTest(string testId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetSubAnalyteNotInTest " + testId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public Dictionary<string, object> addSubAnalyte(string testId, string subAnalyteId)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sTestId",testId),
                    new SqlParameter("@sSubAnalyteId",subAnalyteId)
                };
            DAL.ExecuteStoredProcedure("Sp_InserttestSubAnalyte", param);
            returnType = 1;
            returnData.Add("key", returnType);
            return returnData;

        }
        catch (Exception)
        {
            returnType = 0;
            returnData.Add("key", returnType);
            return returnData;
        }
    }
    public string updateTestDetails(string testId, string profile, string testCode, string testName, string testUsefulFor, string testInterpretation, string testLimitation, string testClinicalReferences)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
                {
                     new SqlParameter("@sTestId", testId),
                     new SqlParameter("@sTestCode", testCode),
                     new SqlParameter("@sTestName", testName),
                     new SqlParameter("@sTestProfileId", profile),
                     new SqlParameter("@sTestUsefulFor", testUsefulFor),
                     new SqlParameter("@sTestInterpretation", testInterpretation),
                     new SqlParameter("@sTestLimitation", testLimitation),
                     new SqlParameter("@sTestClinicalReferance", testClinicalReferences)
                };
            DAL.ExecuteStoredProcedure("Sp_UpdateTestDetails", param);
            return "1";
        }
        catch (Exception)
        {
            return "0";
        }
    }
    public DataSet getSpecimen()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_getSpecimen");
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
}
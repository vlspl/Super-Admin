using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

/// <summary>
/// Summary description for CLSTemplateBuilder
/// </summary>
public class CLSTemplateBuilder
{
    DataAccessLayer DAL = new DataAccessLayer();
    public CLSTemplateBuilder()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataSet getAnalyte(string testid)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetAnalyteByTestid " + testid);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getTestReport(string TestId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_GetTestReportByTestId " + TestId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet checkTemplateBody(string TestId)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = DAL.GetDataSet("Sp_checkTemplateBody " + TestId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet checkTemplateHead(string TestId)
    {
        DataSet ds = new DataSet();
        try
        {            ds = DAL.GetDataSet("Sp_CheckTemplateHead " + TestId);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public DataSet getSubAnalyteReport(string analyteId, string labid)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@analyteId",analyteId),
                new SqlParameter("@labid",labid)
            };
            ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetSubAnalyteReport", param);
            return ds;
        }
        catch (Exception)
        {
            ds = null;
            return ds;
        }
    }
    public int createTestTemplate(string TestID, string TestLabID, string[] hdnAnalyteId, string[] hdnSubAnalyteId, string CreatedBy, string CreatedDate)
    {
        // string testId = "";
        List<Dictionary<string, string>> lstTestAnalyte = new List<Dictionary<string, string>>();
        List<Dictionary<string, string>> lstTASM = new List<Dictionary<string, string>>();

        List<Dictionary<string, string>> lstTestSubAnalyte = new List<Dictionary<string, string>>();
        List<Dictionary<string, string>> lstTSASM = new List<Dictionary<string, string>>();

        try
        {
                foreach (var TestAnalyteid in hdnAnalyteId)
                {
                    SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sTestID", TestID),
                        new SqlParameter("@sTestLabID", TestLabID),
                        new SqlParameter("@sTestAnalyteID", TestAnalyteid),
                        new SqlParameter("@sTASMIDforAnalyte", ""),
                        new SqlParameter("@sTestAnalyteReferenceIDforAnalyte", ""),
                        new SqlParameter("@sTestSubAnalyteid", ""),
                        new SqlParameter("@sTASMIDforSubAnalyte", ""),
                        new SqlParameter("@sTestAnalyteReferenceIDforSubAnalyte", ""),
                        new SqlParameter("@sTemplateStatus", "Active"),
                        new SqlParameter("@sTemplateEdit", ""),
                        new SqlParameter("@sCreatedBy", CreatedBy),
                        new SqlParameter("@sCreatedDate", CreatedDate),
                        new SqlParameter("@sModifiedBy", ""),
                        new SqlParameter("@sModifiedDate", "")
                    };
                    DAL.ExecuteStoredProcedure("Sp_CreateTestTemplate", param);
                }

                //insert into testSubAnalyte
                foreach (var TestSubAnalyteid in hdnSubAnalyteId)
                {
                                       SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@sTestID", TestID),
                        new SqlParameter("@sTestLabID", TestLabID),
                        new SqlParameter("@sTestAnalyteID", ""),
                        new SqlParameter("@sTASMIDforAnalyte", ""),
                        new SqlParameter("@sTestAnalyteReferenceIDforAnalyte", ""),
                        new SqlParameter("@sTestSubAnalyteid", TestSubAnalyteid),
                        new SqlParameter("@sTASMIDforSubAnalyte", ""),
                        new SqlParameter("@sTestAnalyteReferenceIDforSubAnalyte", ""),
                        new SqlParameter("@sTemplateStatus", "Active"),
                        new SqlParameter("@sTemplateEdit", ""),
                        new SqlParameter("@sCreatedBy", CreatedBy),
                        new SqlParameter("@sCreatedDate", CreatedDate),
                        new SqlParameter("@sModifiedBy", ""),
                        new SqlParameter("@sModifiedDate", "")
                    };
                    DAL.ExecuteStoredProcedure("Sp_CreateTestTemplate", param);

                }
                return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public int createTestTemplateHeading(string TestID, string TestLabID, string HEading, string SubHEading, string Notes, string Comments, string ReferenceValue, string CreatedBy, string CreatedDate)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@sTestID", TestID),
                    new SqlParameter("@sTestLabID", TestLabID),
                    new SqlParameter("@sHeadTitle", HEading),
                    new SqlParameter("@sSubTitle", SubHEading),
                    new SqlParameter("@sNotes", Notes),
                    new SqlParameter("@sComments", Comments),
                    new SqlParameter("@sReferenceValue", ReferenceValue),
                    new SqlParameter("@sTemplateStatus", "ActiveHead"),
                    new SqlParameter("@sCreatedBy", CreatedBy),
                    new SqlParameter("@sCreatedDate", CreatedDate),
                    new SqlParameter("@sModifiedBy", ""),
                    new SqlParameter("@sModifiedDate", "")
                };
            DAL.ExecuteStoredProcedure("Sp_CreateTestTemplateHeading", param);

            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public int DeleteTestTemplateHeading(string TestID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@sTestID",TestID)
            };
            DAL.ExecuteStoredProcedure("Sp_DeleteTemplateBuilder", param);
            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
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
    public Dictionary<string, object> loadSubAnalyte(string analyteIds)
    {
        Dictionary<string, object> returnData = new Dictionary<string, object>();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var returnType = 0;
        try
        {
            DataSet ds = new DataSet();
            ds = DAL.GetDataSet("Sp_GetSubAnalyteByAnalyteId  " + analyteIds);
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
}
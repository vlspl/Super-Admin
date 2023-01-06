using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using Validation;
using System.IO;
using Spire.Xls;


public partial class SuperAdmin_TestBulkUpload : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    DBClass db = new DBClass();
    private SqlConnection con;
    private SqlCommand com;
    private string constr, query, Query;
    OleDbConnection Econ;
    int uploadId;
    

    private void connection()
    {
        constr = ConfigurationManager.ConnectionStrings["constr"].ToString();
        con = new SqlConnection(constr);
        con.Open();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            string LabId = "253";
            bindGrid(LabId);
        }
      
        else
        {
            //Response.Redirect("AdminLogin.aspx");
        }
    }


   
    private void ExcelConn(string FilePath)
    {
        constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0 Xml;HDR=YES;""", FilePath);
        Econ = new OleDbConnection(constr);
    }
    DataTable RemoveEmptyRowsFromDataTable(DataTable Exceldt)
    {
        for (int i = Exceldt.Rows.Count - 1; i >= 0; i--)
        {
            if (Exceldt.Rows[i][1] == DBNull.Value)
                Exceldt.Rows[i].Delete();
        }
        Exceldt.AcceptChanges();
        return Exceldt;
    }
 
    protected void DownloadFile(object sender, CommandEventArgs e)
    {
        string LabId = "253";
        InputValidation Ival = new InputValidation();
        connection();
        string val = e.CommandArgument.ToString();
       // Label lbldeleteID = (Label)GridTestUpload.Rows[e.RowIndex].FindControl("lbltestId");
        SqlCommand cmd = new SqlCommand("select * from TestBuklUpload_Temp where sLabId='" + LabId + "' and UploadId='" + val + "'", con);//uploadeId='"+0+"' and 
        //cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        // DataTable dt = new DataTable();

        DataTable dt = new DataTable();// (DataTable)ViewState["DataTable"];
        da.Fill(dt);
        CreateExcelFile(dt);

        ExportToExcel();
       
    }
    public void CreateExcelFile(DataTable Excel)
    {
        //Clears all content output from the buffer stream.    
        Response.ClearContent();
        //Adds HTTP header to the output stream    
        Response.AddHeader("content-disposition", string.Format("attachment; filename=TestLogFile.xls"));
        // Gets or sets the HTTP MIME type of the output stream    
        Response.ContentType = "application/vnd.ms-excel";
        string space = "";
        foreach (DataColumn dcolumn in Excel.Columns)
        {
            Response.Write(space + dcolumn.ColumnName);
            space = "\t";
        }
        Response.Write("\n");
        int countcolumn;
        foreach (DataRow dr in Excel.Rows)
        {
            space = "";
            for (countcolumn = 0; countcolumn < Excel.Columns.Count; countcolumn++)
            {
                Response.Write(space + dr[countcolumn].ToString());
                space = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }

    private void ExportToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "TestLogFile" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        Response.Write(strwritter.ToString());
        Response.End();

    }
 
   
    protected void btnupload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload_testList.HasFile)
            {
                string LabId = "253";

               string user = Request.Cookies["AdminUserName"].Value.ToString();
                string FileName = FileUpload_testList.FileName;
                string path = string.Concat(Server.MapPath("UploadTest/" + FileUpload_testList.FileName));
                FileUpload_testList.PostedFile.SaveAs(path);
                connection();
                SqlCommand cmd = new SqlCommand("Sp_AddTestbuUploadMaster", con);
                cmd.Parameters.AddWithValue("@fileName", FileUpload_testList.FileName);
                cmd.Parameters.AddWithValue("@uploadDate", System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:tt"));
                cmd.Parameters.AddWithValue("@uploadBy", user);
                cmd.Parameters.AddWithValue("@labId", LabId);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                // get id from return val through sp
                uploadId = int.Parse(DAL.ExecuteScalar("select max(testBuUploadId) from TestBuUploadMaster"));
                bindGrid(LabId);
                excelupload(path, uploadId, LabId);
                string status = "Success";
                InsertIntoTest(uploadId, status);
                string msg1 = "File Upload Successfully..!";
                ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('" + msg1 + "');", true);
               
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
        }
    }
    public void excelupload(string path, int uploadID, string labId)
    {
        Workbook workbook = new Workbook();


        workbook.LoadFromFile(@path);

        Worksheet sheet = workbook.Worksheets[0];

        DataTable tbl = sheet.ExportDataTable();

     //   DataTable tbl = new DataTable();
        string Status;
        for (int i = 0; i < tbl.Rows.Count; i++)
        {
            string Message = "";

            string Section = tbl.Rows[i].Field<string>("Section");
            if (Section == null || Section == "0")
            {
                Message += "Section Name Cannot be Blank" + ',';
            }
            string Profile = tbl.Rows[i].Field<string>("Profile");
            if (Profile == null || Profile == "0")
            {
                Message += "Profile Name Cannot be Blank" + ',';
            }
            string TestCode = tbl.Rows[i].Field<string>("TestCode");
            if (TestCode == null || TestCode == "0")
            {
                Message += "Test Code Cannot be Blank" + ',';
            }
            string TestName = tbl.Rows[i].Field<string>("TestName");
            if (TestName == null || TestName == "0")
            {
                Message += "Test Name Cannot be Blank" + ',';
            }
            string TestGroup = tbl.Rows[i].Field<string>("TestGroup");
            string TestUsefullFor = tbl.Rows[i].Field<string>("TestUsefullFor");
            string TestInterpretation = tbl.Rows[i].Field<string>("TestInterpretation");
            string TestLimitation = tbl.Rows[i].Field<string>("TestLimitation");
            string TestClinicalRef = tbl.Rows[i].Field<string>("TestClinicalRef");
            string Analyte = tbl.Rows[i].Field<string>("Analyte");
            if (Analyte == null || Analyte=="0")
            {
                Message += "Analyte Name Cannot be Blank" + ',';
            }
            string SubAnalyte = tbl.Rows[i].Field<string>("SubAnalyte");
            if (SubAnalyte == null || SubAnalyte=="0")
            {
                Message += "SubAnalyte Name Cannot be Blank" + ',';
            }
            string Method = tbl.Rows[i].Field<string>("Method");
            if (Method == null || Method=="0")
            {
                Message += "Method Name Cannot be Blank" + ',';
            }
            string SampleType = tbl.Rows[i].Field<string>("SampleType");
            if (SampleType == null || SampleType=="0")
            {
                Message += "SampleType Cannot be Blank" + ',';
            }
            string Quantity = tbl.Rows[i].Field<string>("Quantity");
            if (Quantity == null || Quantity=="0")
            {
                Message += "Quantity Cannot be Blank" + ',';
            }
            string TimePeriod = tbl.Rows[i].Field<string>("TimePeriod");
            if (TimePeriod == null || TimePeriod=="0")
            {
                Message += "TimePeriod Cannot be Blank" + ',';
            }
            string ResultType = tbl.Rows[i].Field<string>("ResultType");
            string RefrenceType = tbl.Rows[i].Field<string>("RefrenceType");
            string MaleFromAge = tbl.Rows[i].Field<string>("MaleFromAge");
            string MaleToAge = tbl.Rows[i].Field<string>("MaleToAge");
            string MaleAgeUnit = tbl.Rows[i].Field<string>("MaleAgeUnit");
            string MaleMinValue = tbl.Rows[i].Field<string>("MaleMinValue");
            string MaleMaxValue = tbl.Rows[i].Field<string>("MaleMaxValue");
            string FemaleFromAge = tbl.Rows[i].Field<string>("FemaleFromAge");
            string FemaleToAge = tbl.Rows[i].Field<string>("FemaleToAge");
            string FemaleAgeUnit = tbl.Rows[i].Field<string>("FemaleAgeUnit");
            string FemaleMinValue = tbl.Rows[i].Field<string>("FemaleMinValue");
            string FemaleMaxValue = tbl.Rows[i].Field<string>("FemaleMaxValue");
            string Grade = tbl.Rows[i].Field<string>("Grade");
            string Unit = tbl.Rows[i].Field<string>("Unit");
            string Interpritation = tbl.Rows[i].Field<string>("Interpritation");
            if (Interpritation == null)
            {
                Message += "Interpritation Name Cannot be Blank" + ',';
            }
            string UpperLimit = tbl.Rows[i].Field<string>("UpperLimit");
            string LowerLimit = tbl.Rows[i].Field<string>("LowerLimit");
            string Price = tbl.Rows[i].Field<string>("Price");
            if (Price == null)
            {
                Message += "Price Cannot be Blank" + ',';
            }
            string createdDate = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:tt");
            if (Message != "")
                Status = "Failed";
            else

                Status = "Success";


            db.insert(@"insert into TestBuklUpload_Temp(Section,Profile,TestCode,TestName,TestGroup,TestUsefullFor,TestInterpretation,TestLimitation,TestClinicalRef,Analyte,SubAnalyte,Method,SampleType,Quantity,TimePeriod,ResultType,RefrenceType,MaleFromAge,MaleToAge,MaleAgeUnit,MaleMinValue,MaleMaxValue,FemaleFromAge,FemaleToAge,FemaleAgeUnit,FemaleMinValue,FemaleMaxValue,Grade,Unit,Interpritation,UpperLimit,LowerLimit,Price,sLabId,createdDate,Status,Remark,UploadId) 
                        values('" + Section + "','" + Profile + "','" + TestCode + "','" + TestName + "','" + TestGroup + "','" + TestUsefullFor + "','" + TestInterpretation + "','" + TestLimitation + "','" + TestClinicalRef + "','" + Analyte + "','" + SubAnalyte + "','" + Method + "','" + SampleType + "','" + Quantity + "','" + TimePeriod + "','" + ResultType + "','" + RefrenceType + "','" + MaleFromAge + "','" + MaleToAge + "','" + MaleAgeUnit + "','" + MaleMinValue + "','" + MaleMaxValue + "','" + FemaleFromAge + "','" + FemaleToAge + "','" + FemaleAgeUnit + "','" + FemaleMinValue + "','" + FemaleMaxValue + "','" + Grade + "','" + Unit + "','" + Interpritation + "','" + UpperLimit + "','" + LowerLimit + "','" + Price + "','" + labId + "','" + createdDate + "','" + Status + "','" + Message + "','" + uploadID + "')");

        }

    }
    public void InsertIntoTest(int uploadId, string status)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
           {
                new SqlParameter("@uploadId", uploadId),
                 new SqlParameter("@status", status),
           };
            DataSet ds_tempData = DAL.ExecuteStoredProcedureDataSet("sp_gettempbukUploadData", param);// sp added newly
            if (ds_tempData != null)
            {
                if (ds_tempData.Tables[0].Rows.Count > 0)
                {
                    string AllUserList = "";
                    int count = 0;
                   

                    foreach (DataRow row in ds_tempData.Tables[0].Rows)
                    {
                        string sectionId = "";
                        string profileId = "";
                        string AnalyteId = "";
                        string SubAnalyteId = "";
                        string TestAnalyteId = "";
                        string testSubAnalyteId = "";
                        string specimenID = "";
                        string methodId = "";
                        string refId = "";
                        string testsubanspecimenID = "";
                        string subanrefId = "";
                        string testanspecimenID = "";
                        // Insert Section
                        string section = row["Section"].ToString();
                        if (!db.ChkDb_Value("select sSectionName from section where sSectionName='" + section + "'"))
                        {
                            SqlParameter[] param_section = new SqlParameter[]
                           {
                                new SqlParameter("@section", section),
                                    new SqlParameter("@IsActive", '1'),
                                 
                           };
                            sectionId = DAL.ExecuteScalarWithProc("Sp_insertSection", param_section);

                        }
                        else
                        {
                            SqlParameter[] param_sectionGetMax = new SqlParameter[]
                           {
                                new SqlParameter("@section", section)
                           };
                            sectionId = DAL.ExecuteScalarWithProc("Sp_getMaxSectionId", param_sectionGetMax);
                        }

                        // Insert Profile
                        string profile = row["Profile"].ToString();
                        if (!db.ChkDb_Value("select sProfileName from testProfile where sProfileName='" + profile + "'"))
                        {

                            SqlParameter[] param_profile = new SqlParameter[]
                           {
                                new SqlParameter("@sProfileName", profile),
                                 new SqlParameter("@sSectionId", sectionId),
                                    new SqlParameter("@IsActive", '1')
   
                           };
                            profileId = DAL.ExecuteScalarWithProc("Sp_insertProfile", param_profile);

                        }
                        else
                        {
                            SqlParameter[] param_profileGetMax = new SqlParameter[]
                           {
                                new SqlParameter("@profile", profile)
                           };
                            profileId = DAL.ExecuteScalarWithProc("Sp_getMaxProfileId", param_profileGetMax);
                        }

                        // test insert
                        string testId = "";
                        int returnVal = 0;
                        string TName = row["TestName"].ToString();
                        if (!db.ChkDb_Value("select sTestName from test where sTestName='" + TName + "'"))
                        {

                            SqlParameter[] param_test = new SqlParameter[]
                            {
                                new SqlParameter("@sTestCode", row["TestCode"].ToString()),
                                    new SqlParameter("@sTestName", row["TestName"].ToString()),
                                    new SqlParameter("@sTestProfileId", profileId),
                                    new SqlParameter("@sTestUsefulFor", row["TestUsefullFor"].ToString()),
                                    new SqlParameter("@sTestInterpretation", row["TestInterpretation"].ToString()),
                                    new SqlParameter("@sTestLimitation", row["TestLimitation"].ToString()),
                                    new SqlParameter("@sTestClinicalReferance", row["TestClinicalRef"].ToString()),
                                    new SqlParameter("@sCustom", '1')
                            };
                            testId = DAL.ExecuteScalarWithProc("Sp_insertTEST", param_test);

                            SqlParameter[] param_testPrice = new SqlParameter[]
                                 {
                                    new SqlParameter("@sLabId", row["sLabId"].ToString()),
                                    new SqlParameter("@sTestId", testId),
                                    new SqlParameter("@sPrice", row["Price"].ToString()),
                                    new SqlParameter("@sMyTest", "1")
                                 };
                            DAL.ExecuteStoredProcedure("Sp_Addtestprice", param_testPrice);

                        }
                        else
                        {
                            SqlParameter[] param_profileGetMax = new SqlParameter[]
                           {
                                new SqlParameter("@sTestName", TName)
                           };
                            testId = DAL.ExecuteScalarWithProc("Sp_getMaxtestId", param_profileGetMax);
                            if (!db.ChkDb_Value("select sTestId,sLabId from testLab where sTestId='" + testId + "' and sLabId='" + row["sLabId"].ToString() + "'"))
                            {
                                SqlParameter[] param_testPrice = new SqlParameter[]
                                 {
                                    new SqlParameter("@sLabId", row["sLabId"].ToString()),
                                    new SqlParameter("@sTestId", testId),
                                    new SqlParameter("@sPrice", row["Price"].ToString()),
                                    new SqlParameter("@sMyTest", "1")
                                 };
                                DAL.ExecuteStoredProcedure("Sp_Addtestprice", param_testPrice);
                            }
                        }
                       
                       
                  
                    // add analyte Insert Code
                        string analyte = row["Analyte"].ToString();
                        if (!db.ChkDb_Value("select sAnalyteName from analyte where sAnalyteName='" + analyte + "'"))
                        {
                            SqlParameter[] param_analyte = new SqlParameter[]
                           {
                                new SqlParameter("@analyte", analyte),
                                    new SqlParameter("@IsActive", '1'),
                                 
                           };
                            AnalyteId = DAL.ExecuteScalarWithProc("Sp_insertAnalyte_bu", param_analyte);

                        }
                        else
                        {
                            SqlParameter[] param_analyteMax = new SqlParameter[]
                           {
                                new SqlParameter("@analyte", analyte)
                           };
                            AnalyteId = DAL.ExecuteScalarWithProc("Sp_getMaxAnalyteId", param_analyteMax);
                        }
                        // add Sub analyte Insert Code
                        string Subanalyte = row["SubAnalyte"].ToString();
                        if (!db.ChkDb_Value("select sSubAnalyteName from subAnalyte where sSubAnalyteName='" + Subanalyte + "' and sAnalyteId='" + AnalyteId + "'"))
                        {
                            SqlParameter[] param_subanalyte = new SqlParameter[]
                           {
                                new SqlParameter("@analyteId", AnalyteId),
                                 new SqlParameter("@Subanalyte", Subanalyte),
                                    new SqlParameter("@IsActive", '1'),
                                 
                           };
                            SubAnalyteId = DAL.ExecuteScalarWithProc("Sp_insertSubAnalyte_bu", param_subanalyte);

                        }
                        else
                        {
                            SqlParameter[] param_subanGetMax = new SqlParameter[]
                           {
                                new SqlParameter("@analyteId", AnalyteId),
                                 new SqlParameter("@Subanalyte", Subanalyte)
                           };
                            SubAnalyteId = DAL.ExecuteScalarWithProc("Sp_getMaxsubAnalyteId", param_subanGetMax);
                        }

                        // add Test analyte Insert Code
                        if (!db.ChkDb_Value("select sTestId,sAnalyteId from testAnalyte where sTestId='" + testId + "' and sAnalyteId='" + AnalyteId + "' "))
                        {
                            SqlParameter[] param_Testanalyte = new SqlParameter[]
                           {
                                new SqlParameter("@sTestId", testId),
                                new SqlParameter("@analyteId", AnalyteId),
                                    new SqlParameter("@IsActive", '1'),
                                 
                           };
                            TestAnalyteId = DAL.ExecuteScalarWithProc("Sp_TestAnalyte_bu", param_Testanalyte);
                        }
                        else
                        {
                            SqlParameter[] param_testAnalyteGetMax = new SqlParameter[]
                           {
                                new SqlParameter("@sTestId", testId),
                                new SqlParameter("@analyteId", AnalyteId),
                           };
                            TestAnalyteId = DAL.ExecuteScalarWithProc("Sp_getMaxtestAnalyteId", param_testAnalyteGetMax);
                        }





                      
                        // add Test Subanalyte Insert Code

                        if (!db.ChkDb_Value("select sTestId,sSubAnalyteId from testSubAnalyte where sTestId='" + testId + "' and sSubAnalyteId='" + SubAnalyteId + "' "))
                        {
                            SqlParameter[] param_TestSubanalyte = new SqlParameter[]
                           {
                                new SqlParameter("@sTestId", testId),
                                new SqlParameter("@SubanalyteId", SubAnalyteId),
                                    new SqlParameter("@IsActive", '1'),
                                 
                           };
                            testSubAnalyteId = DAL.ExecuteScalarWithProc("Sp_TestSubAnalyte_bu", param_TestSubanalyte);

                        }
                        else
                        {
                            SqlParameter[] param_testsubAnalyteGetMax = new SqlParameter[]
                           {
                                new SqlParameter("@sTestId", testId),
                                new SqlParameter("@SubanalyteId", SubAnalyteId),
                           };
                            testSubAnalyteId = DAL.ExecuteScalarWithProc("Sp_getMaxtestSubAnalyteId", param_testsubAnalyteGetMax);
                        }


                       


                        // method insert

                        if (!db.ChkDb_Value("select sMethodId from method where sMethodName='" + row["Method"].ToString() + "'  "))
                        {
                            SqlParameter[] param_method = new SqlParameter[]
                           {
                                new SqlParameter("@sMethodName", row["Method"].ToString()),
                                    new SqlParameter("@IsActive", '1'),
                                 
                           };
                            methodId = DAL.ExecuteScalarWithProc("Sp_method_bu", param_method);
                        }
                        else
                        {
                            SqlParameter[] param_methodGetMax = new SqlParameter[]
                           {
                               new SqlParameter("@sMethodName", row["Method"].ToString()),
                           };
                            methodId = DAL.ExecuteScalarWithProc("Sp_getMaxMethodId", param_methodGetMax);
                        }

                       
                      
                     // insert specimen insert
                        if (!db.ChkDb_Value("select sSpecimenId from specimen where sSampleType='" + row["SampleType"].ToString() + "' and sQuantity='" + row["Quantity"].ToString() + "' and sTimePeriod='" + row["TimePeriod"].ToString() + "'  "))
                        {
                            SqlParameter[] param_Specimen = new SqlParameter[]
                           {
                                new SqlParameter("@sSampleType", row["SampleType"].ToString()),
                                new SqlParameter("@sQuantity", row["Quantity"].ToString()),
                                 new SqlParameter("@sTimePeriod", row["TimePeriod"].ToString()),
                                    new SqlParameter("@IsActive", '1'),
                                 
                           };
                            specimenID = DAL.ExecuteScalarWithProc("Sp_specimen_bu", param_Specimen);
                        }
                        else
                        {
                            SqlParameter[] param_specimenGetMax = new SqlParameter[]
                           {
                                 new SqlParameter("@sSampleType", row["SampleType"].ToString()),
                                new SqlParameter("@sQuantity", row["Quantity"].ToString()),
                                 new SqlParameter("@sTimePeriod", row["TimePeriod"].ToString())
                           };
                            specimenID = DAL.ExecuteScalarWithProc("Sp_getMaxSpecimenId", param_specimenGetMax);
                        }

                       
                        // testAnalyte specimen Insert

                        if (!db.ChkDb_Value("select sTASMId from testAnalyteSpecimenMethod where sTestAnalyteId='" + TestAnalyteId + "' and sSpecimenId='" + specimenID + "' and sMethodId='" + methodId + "' "))
                        {
                            SqlParameter[] param_testanalyteSpecimen = new SqlParameter[]
                           {
                                new SqlParameter("@sTestAnalyteId", TestAnalyteId),
                                new SqlParameter("@sSpecimenId", specimenID),
                                 new SqlParameter("@sMethodId", methodId),
                                 new SqlParameter("@sResultType", row["ResultType"].ToString()),
                                    new SqlParameter("@IsActive", '1'),
                                 
                           };
                            testanspecimenID = DAL.ExecuteScalarWithProc("sp_testanalytespecimen_bu", param_testanalyteSpecimen);
                        }
                        else
                        {
                            SqlParameter[] param_testAnalytespeGetMax = new SqlParameter[]
                           {
                                  new SqlParameter("@sTestAnalyteId", TestAnalyteId),
                                new SqlParameter("@sSpecimenId", specimenID),
                                 new SqlParameter("@sMethodId", methodId)
                           };
                            testanspecimenID = DAL.ExecuteScalarWithProc("Sp_getMaxTASMId", param_testAnalytespeGetMax);
                        }

                      
                        // testAnalyte ref range Insert
                        SqlParameter[] param_testanalyterefrange = new SqlParameter[]
                           {
                                new SqlParameter("@LabID",  row["sLabId"].ToString()),
                                new SqlParameter("@TASMId", testanspecimenID),
                                 new SqlParameter("@ReferenceType", row["RefrenceType"].ToString()),
                                 new SqlParameter("@MaleFromAge", row["MaleFromAge"].ToString()),
                                  new SqlParameter("@MaleToAge", row["MaleToAge"].ToString()),
                                   new SqlParameter("@MaleAgeUnit", row["MaleAgeUnit"].ToString()),
                                    new SqlParameter("@MaleMinValue", row["MaleMinValue"].ToString()),
                                     new SqlParameter("@MaleMaxValue", row["MaleMaxValue"].ToString()),
                                      new SqlParameter("@FemaleFromAge", row["FemaleFromAge"].ToString()),
                                       new SqlParameter("@FemaleToAge", row["FemaleToAge"].ToString()),
                                        new SqlParameter("@FemaleAgeUnit", row["FemaleAgeUnit"].ToString()),
                                         new SqlParameter("@FemaleMinValue", row["FemaleMinValue"].ToString()),
                                          new SqlParameter("@FemaleMaxValue", row["FemaleMaxValue"].ToString()),
                                          new SqlParameter("@Grade", row["Grade"].ToString()),
                                          new SqlParameter("@Unit", row["Unit"].ToString()),
                                          new SqlParameter("@Interpretation", row["Interpritation"].ToString()),
                                          new SqlParameter("@UpperLimit", row["UpperLimit"].ToString()),
                                          new SqlParameter("@LowerLimit", row["LowerLimit"].ToString()),
                                          
                                    new SqlParameter("@IsActive", '1'),
                                 
                           };
                        refId = DAL.ExecuteScalarWithProc("sp_testanalyterefrange_bu", param_testanalyterefrange);

                        // Test sub analyte specimen method
                        if (!db.ChkDb_Value("select sTSASMId from testSubAnalyteSpecimenMethod where sTestSubAnalyteId='" + testSubAnalyteId + "' and sSpecimenId='" + specimenID + "' and sMethodId='" + methodId + "' "))
                        {
                            SqlParameter[] param_testsubanalyteSpecimen = new SqlParameter[]
                           {
                                new SqlParameter("@sTestSubAnalyteId", testSubAnalyteId),
                                new SqlParameter("@sSpecimenId", specimenID),
                                 new SqlParameter("@sMethodId", methodId),
                                 new SqlParameter("@sResultType", row["ResultType"].ToString()),
                                    new SqlParameter("@IsActive", '1'),
                                 
                           };
                            testsubanspecimenID = DAL.ExecuteScalarWithProc("sp_testsubanalytespecimen_bu", param_testsubanalyteSpecimen);
                        }
                        else
                        {
                            SqlParameter[] param_testsubAnalytespeGetMax = new SqlParameter[]
                           {
                                  new SqlParameter("@sTestSubAnalyteId", testSubAnalyteId),
                                new SqlParameter("@sSpecimenId", specimenID),
                                 new SqlParameter("@sMethodId", methodId)
                           };
                            testsubanspecimenID = DAL.ExecuteScalarWithProc("Sp_getMaxTSASMId", param_testsubAnalytespeGetMax);
                        }
                         
                        // test sub analyte reg range method
                        SqlParameter[] param_testsubanalyterefrange = new SqlParameter[]
                           {
                                new SqlParameter("@LabID",  row["sLabId"].ToString()),
                                new SqlParameter("@TSASMId", testsubanspecimenID),
                                 new SqlParameter("@ReferenceType", row["RefrenceType"].ToString()),
                                 new SqlParameter("@MaleFromAge", row["MaleFromAge"].ToString()),
                                  new SqlParameter("@MaleToAge", row["MaleToAge"].ToString()),
                                   new SqlParameter("@MaleAgeUnit", row["MaleAgeUnit"].ToString()),
                                    new SqlParameter("@MaleMinValue", row["MaleMinValue"].ToString()),
                                     new SqlParameter("@MaleMaxValue", row["MaleMaxValue"].ToString()),
                                      new SqlParameter("@FemaleFromAge", row["FemaleFromAge"].ToString()),
                                       new SqlParameter("@FemaleToAge", row["FemaleToAge"].ToString()),
                                        new SqlParameter("@FemaleAgeUnit", row["FemaleAgeUnit"].ToString()),
                                         new SqlParameter("@FemaleMinValue", row["FemaleMinValue"].ToString()),
                                          new SqlParameter("@FemaleMaxValue", row["FemaleMaxValue"].ToString()),
                                          new SqlParameter("@Grade", row["Grade"].ToString()),
                                          new SqlParameter("@Unit", row["Unit"].ToString()),
                                          new SqlParameter("@Interpretation", row["Interpritation"].ToString()),
                                          new SqlParameter("@UpperLimit", row["UpperLimit"].ToString()),
                                          new SqlParameter("@LowerLimit", row["LowerLimit"].ToString()),
                                          
                                    new SqlParameter("@IsActive", '1'),
                                 
                           };
                        subanrefId = DAL.ExecuteScalarWithProc("sp_testsubanalyterefrange_bu", param_testsubanalyterefrange);
                    }

                }
            }


        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
        }
    }
    public void bindGrid(string labId)
    {

        connection();
        SqlCommand cmd = new SqlCommand("sp_BindTestBuUploadDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@labId", labId);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridTestUpload.DataSource = dt;
        GridTestUpload.DataBind();
        con.Close();
    }
   
}
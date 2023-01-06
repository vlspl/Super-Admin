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


public partial class SuperAdmin_labTestPriceBulkUpload : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    DBClass db = new DBClass();
    private SqlConnection con;
    private SqlCommand com;
    private string constr, query, Query;
    OleDbConnection Econ;
    string maxSectionId = "";
    string maxProfileId = "";
    string sectionId;
    string profileId;
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
            db.bindDrp("select distinct sLabId, sLabName from labMaster where IsActive=1 and sLabStatus='Active' order by sLabName asc", ddllabName, "sLabName", "sLabName");
            ddllabName.Items.Insert(0, new ListItem("-Select Lab-"));
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
    public void InsertExcelRecords(string path, int uploadID, string labId)
    {
        try
        {
            ExcelConn(path);
            string consString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                con.Open();
                using (SqlTransaction sqlTransaction = con.BeginTransaction())
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, sqlTransaction))
                    {


                        Query = string.Format("Select [Section_Name],[Profile_Name],[Test_Code],[Test_Name],[Test_Usefull_For],[Test_Interprition],[Test_Limitation],[Test_Clinicial_Ref],[Price],[Status],[Remark] FROM [{0}]", "Sheet1$");//
                        OleDbCommand Ecom = new OleDbCommand(Query, Econ);
                        Econ.Open();

                        DataSet ds = new DataSet();
                        DataTable tbl = new DataTable();
                        OleDbDataAdapter oda = new OleDbDataAdapter(Query, Econ);
                        Econ.Close();
                        oda.Fill(tbl);
                        //RemoveEmptyRowsFromDataTable(tbl);
                        string Status;
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            string Message = "";

                            string section = tbl.Rows[i].Field<string>("Section_Name");
                            if (section == null)
                            {
                                Message += "Section Name Cannot be Blank" + ',';
                            }
                            string profile = tbl.Rows[i].Field<string>("Profile_Name");
                            if (profile == null)
                            {
                                Message += "Profile Name Cannot be Blank" + ',';
                            }
                            string code = tbl.Rows[i].Field<string>("Test_Code");
                            if (code == null)
                            {
                                Message += "Test Code Cannot be Blank" + ',';
                            }
                            string name = tbl.Rows[i].Field<string>("Test_Name");
                            if (name == null)
                            {
                                Message += "Test Name Cannot be Blank" + ',';
                            }
                            string use = tbl.Rows[i].Field<string>("Test_Usefull_For");
                            string interprition = tbl.Rows[i].Field<string>("Test_Interprition");
                            string limitation = tbl.Rows[i].Field<string>("Test_Limitation");
                            string clinicalRef = tbl.Rows[i].Field<string>("Test_Clinicial_Ref");
                            Double price = tbl.Rows[i].Field<Double>("Price");

                            //string Status = tbl.Rows[i].Field<string>("Status");
                            // string Remark = tbl.Rows[i].Field<string>("Remark");

                            if (Message != "")
                                Status = "Failed";
                            else

                                Status = "Success";


                            db.insert(@"insert into temp_TestListBulkUpload(Section_Name,Profile_Name,Test_Code,Test_Name,Test_Usefull_For,
                            Test_Interprition,Test_Limitation,Test_Clinicial_Ref,Price,sLabId,Status,Remark,uploadId) 
                        values('" + section + "','" + profile + "','" + code + "','" + name + "','" + use + "','" + interprition + "','" + limitation + "','" + clinicalRef + "','" + price + "','" + labId + "','" + Status + "','" + Message + "','" + uploadID + "')");

                        }

                        con.Close();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
        }

    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        string LabId = db.getData("select sLabId from labMaster where IsActive=1 and sLabStatus='Active' and sLabName='" + ddllabName.Text + "'").ToString();
        InputValidation Ival = new InputValidation();
        connection();
        SqlCommand cmd = new SqlCommand("select * from temp_TestListBulkUpload where sLabId='" + LabId + "' ", con);//uploadeId='"+0+"' and 
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
 
    public int insertTestEntry(string code, string name, string use, string inter, string limit, string clRef, string price, string LabId)
    {
        string testId = "";
        int returnVal = 0;
       
        try
        {
           
            SqlParameter[] param1 = new SqlParameter[]
            {
                new SqlParameter("@sTestCode", code),
                    new SqlParameter("@sTestName", name),
                    new SqlParameter("@sTestProfileId", profileId),
                    new SqlParameter("@sTestUsefulFor", use),
                    new SqlParameter("@sTestInterpretation", inter),
                    new SqlParameter("@sTestLimitation", limit),
                    new SqlParameter("@sTestClinicalReferance", clRef),
                    new SqlParameter("@sCustom", '1')
            };
            testId = DAL.ExecuteScalarWithProc("Sp_insertTEST", param1);

            SqlParameter[] param6 = new SqlParameter[]
                 {
                    new SqlParameter("@sLabId", LabId),
                    new SqlParameter("@sTestId", testId),
                    new SqlParameter("@sPrice", price),
                    new SqlParameter("@sMyTest", "1")
                 };
            DAL.ExecuteStoredProcedure("Sp_Addtestprice", param6);
        }
        catch (Exception)
        {
            returnVal = 0;
        }
        return returnVal;
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload_testList.HasFile)
            {
                string LabId = db.getData("select sLabId from labMaster where IsActive=1 and sLabStatus='Active' and sLabName='" + ddllabName.Text + "'").ToString();

                string user = Request.Cookies["AdminUserName"].Value.ToString();
                string FileName = FileUpload_testList.FileName;
                string path = string.Concat(Server.MapPath("UploadTest/" + FileUpload_testList.FileName));
                FileUpload_testList.PostedFile.SaveAs(path);
                connection();
                SqlCommand cmd = new SqlCommand("Sp_AddTestbulkUploadMaster", con);
                cmd.Parameters.AddWithValue("@fileName", FileUpload_testList.FileName);
                cmd.Parameters.AddWithValue("@uploadDate", System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:tt"));
                cmd.Parameters.AddWithValue("@uploadBy", user);
                cmd.Parameters.AddWithValue("@labId", LabId);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
               // get id from return val through sp
                uploadId = int.Parse(DAL.ExecuteScalar("select max(testUploadId) from testBulkUploadMaster"));


                //display Bulk Upload master details
                bindGrid(LabId);

                InsertExcelRecords(path, uploadId, LabId);
                string status="Success";
                InsertIntoTest(uploadId, status);
                //int test = int.Parse(DAL.ExecuteScalar("truncate table temp_TestListBulkUpload"));
               //insertData();
                string msg1 = "File Upload Successfully..!";
                ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('" + msg1 + "');", true);
               
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
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
            DataSet ds_tempData = DAL.ExecuteStoredProcedureDataSet("sp_gettempUploadData", param);
            if (ds_tempData != null)
            {
                if (ds_tempData.Tables[0].Rows.Count > 0)
                {
                    string AllUserList = "";
                    int count = 0;
                    foreach (DataRow row in ds_tempData.Tables[0].Rows)
                    {
                        string section = row["Section_Name"].ToString();
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
                        string profile = row["Profile_Name"].ToString();
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

                        insertTestEntry(row["Test_Code"].ToString(), row["Test_Name"].ToString(), row["Test_Usefull_For"].ToString(), row["Test_Interprition"].ToString(),
                            row["Test_Limitation"].ToString(), row["Test_Clinicial_Ref"].ToString(), row["Price"].ToString(), row["sLabId"].ToString());
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
        SqlCommand cmd = new SqlCommand("sp_BindTestBulkUploadDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@labId", labId);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridTestUpload.DataSource = dt;
        GridTestUpload.DataBind();
        con.Close();
    }
    protected void ddllabName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string LabId = db.getData("select sLabId from labMaster where IsActive=1 and sLabStatus='Active' and sLabName='" + ddllabName.Text + "'").ToString();
        bindGrid(LabId);
    }
}
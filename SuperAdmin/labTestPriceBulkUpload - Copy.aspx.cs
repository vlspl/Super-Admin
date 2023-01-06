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
            db.bindDrp("select distinct sLabId, sLabName from labMaster where IsActive=1 order by sLabName asc", ddllabName, "sLabName", "sLabName");
            ddllabName.Items.Insert(0, new ListItem("-Select Lab-"));
        }
      
        else
        {
            //Response.Redirect("AdminLogin.aspx");
        }
    }


   
    private void ExcelConn(string FilePath)
    {
        constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", FilePath);
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
     void InsertExcelRecords(string path)
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
                                       

               Query = string.Format("Select [Section_Name],[Profile_Name],[Test_Code],[Test_Name],[Test_Usefull_For],[Test_Interprition],[Test_Limitation],[Test_Clinicial_Ref],[Price] FROM [{0}]", "Sheet1$");//
                    OleDbCommand Ecom = new OleDbCommand(Query, Econ);
                    Econ.Open();

                    DataSet ds = new DataSet();
                    DataTable tbl = new DataTable();
                    OleDbDataAdapter oda = new OleDbDataAdapter(Query, Econ);
                    Econ.Close();
                    oda.Fill(tbl);
                    RemoveEmptyRowsFromDataTable(tbl);
                    for (int i = 0; i <= tbl.Rows.Count - 1; i++)
                    {
                        string section = tbl.Rows[i].Field<string>("Section_Name");
                        string profile = tbl.Rows[i].Field<string>("Profile_Name");
                        string code = tbl.Rows[i].Field<string>("Test_Code");
                        string name = tbl.Rows[i].Field<string>("Test_Name");
                        string use = tbl.Rows[i].Field<string>("Test_Usefull_For");
                        string interprition = tbl.Rows[i].Field<string>("Test_Interprition");
                        string limitation = tbl.Rows[i].Field<string>("Test_Limitation");
                        string clinicalRef = tbl.Rows[i].Field<string>("Test_Clinicial_Ref");
                        Double price = tbl.Rows[i].Field<Double>("Price");
                        string LabId = db.getData("select sLabId from labMaster where IsActive=1 and sLabName='"+ddllabName.Text+"'").ToString();
                       

                        db.insert(@"insert into temp_TestListBulkUpload(Section_Name,Profile_Name,Test_Code,Test_Name,Test_Usefull_For,
                            Test_Interprition,Test_Limitation,Test_Clinicial_Ref,Price,sLabId) 
                        values('" + section + "','" + profile + "','" + code + "','" + name + "','" + use + "','" + interprition + "','" + limitation + "','" + clinicalRef + "','" + price + "','"+ LabId + "')");

                    }

                    con.Close();

                }
            }
           // con.Close();
        }


    }

    void insertData()
    {
       
        SqlParameter[] param = new SqlParameter[]
           {

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
                    if (!db.ChkDb_Value("select sSectionName from section where sSectionName='"+ section + "'"))
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
                        sectionId = db.getData("select max(sSectionId) from section where sSectionName='" + section + "'");
                    }
                    string profile = row["Profile_Name"].ToString();
                    if (!db.ChkDb_Value("select sProfileName from testProfile where sProfileName='" + profile + "'"))
                    {

                        SqlParameter[] param_profile = new SqlParameter[]
                           {
                                new SqlParameter("@sProfileName", profile),
                                 new SqlParameter("@sSectionId", sectionId),
                                    new SqlParameter("@IsActive", '1'),
   
                           };
                        profileId = DAL.ExecuteScalarWithProc("Sp_insertProfile", param_profile);

                    }
                    else
                    {
                        profileId = db.getData("select max(sTestProfileId) from testProfile where sProfileName='" + section + "'");
                    }
                    
                    insertTestEntry(row["Test_Code"].ToString(), row["Test_Name"].ToString(), row["Test_Usefull_For"].ToString(), row["Test_Interprition"].ToString(),
                        row["Test_Limitation"].ToString(), row["Test_Clinicial_Ref"].ToString(), row["Price"].ToString(), row["sLabId"].ToString());
                }
            }
        }
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
                    new SqlParameter("@sPrice", "0"),
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
                string FileName = FileUpload_testList.FileName;
                string path = string.Concat(Server.MapPath("UploadTest/" + FileUpload_testList.FileName));
                FileUpload_testList.PostedFile.SaveAs(path);
                InsertExcelRecords(path);
                insertData();
                string msg1 = "File Upload Successfully..!";
                ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('" + msg1 + "');", true);
               
            }
        }
        catch (Exception ex)
        {
            //ExceptionLogging.SendErrorToText(ex);
        }
    }
}
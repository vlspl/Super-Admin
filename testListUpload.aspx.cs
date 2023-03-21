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

public partial class SuperAdmin_testListUpload : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    DBClass db = new DBClass();
    private SqlConnection con;
    private SqlCommand com;
    private string constr, query, Query;
    OleDbConnection Econ;
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
    private void InsertExcelRecords(string path)
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
                    Query = string.Format("Select [Code],[Section],[TestProfile],[TestName],[Analyte],[subAnalyte],[SpecimenType],[SpecimenQty],[SpecimenTime],[TestMethod],[TestType],[RefRangeAge],[RefRangeMale],[RefRangeFemale],[RefRangeUnit],[RefValueGrade],[RefValueMale],[RefValueFemale],[RefValueUnit],[RefRangeInterpretation] FROM [{0}]", "Sheet1$");//
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
                        string Code = tbl.Rows[i].Field<string>("Code");
                        string Section = tbl.Rows[i].Field<string>("Section");
                        string TestProfile = tbl.Rows[i].Field<string>("TestProfile");
                        string TestName = tbl.Rows[i].Field<string>("TestName");
                        string Analyte = tbl.Rows[i].Field<string>("Analyte");
                        string subAnalyte = tbl.Rows[i].Field<string>("subAnalyte");
                        string SpecimenType = tbl.Rows[i].Field<string>("SpecimenType");
                        string SpecimenQty = tbl.Rows[i].Field<string>("SpecimenQty");
                        string SpecimenTime = tbl.Rows[i].Field<string>("SpecimenTime");
                        string TestMethod = tbl.Rows[i].Field<string>("TestMethod");
                        string TestType = tbl.Rows[i].Field<string>("TestType");
                        string RefRangeAge = tbl.Rows[i].Field<string>("RefRangeAge");
                        string RefRangeMale = tbl.Rows[i].Field<string>("RefRangeMale");
                        string RefRangeFemale = tbl.Rows[i].Field<string>("RefRangeFemale");
                        string RefRangeUnit = tbl.Rows[i].Field<string>("RefRangeUnit");
                        string RefValueGrade = tbl.Rows[i].Field<string>("RefValueGrade");
                        string RefValueMale = tbl.Rows[i].Field<string>("RefValueMale");
                        string RefValueFemale = tbl.Rows[i].Field<string>("RefValueFemale");
                        string RefValueUnit = tbl.Rows[i].Field<string>("RefValueUnit");
                        string RefRangeInterpretation = tbl.Rows[i].Field<string>("RefRangeInterpretation");

                        db.insert(@"insert into temp_TestListBulkUpload(Code,Section,TestProfile,TestName,Analyte,subAnalyte,SpecimenType,
                            SpecimenQty,SpecimenTime,TestMethod,TestType,RefRangeAge,RefRangeMale,RefRangeFemale,
                        RefRangeUnit,RefValueGrade,RefValueMale,RefValueFemale,
                        RefValueUnit,RefRangeInterpretation) 
                        values('" + Code + "','" + Section + "','" + TestProfile + "','" + TestName + "','" + Analyte + "','" + subAnalyte + "','" + SpecimenType + "','" + SpecimenQty + "','" + SpecimenTime + "','" + TestMethod + "','" + TestType + "','" + RefRangeAge + "','" + RefRangeMale + "','" + RefRangeFemale + "','" + RefRangeUnit + "','" + RefValueGrade + "','" + RefValueMale + "','" + RefValueFemale + "','" + RefValueUnit + "','" + RefRangeInterpretation + "')");

                    }

                    con.Close();

                }
            }
            con.Close();
        }


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
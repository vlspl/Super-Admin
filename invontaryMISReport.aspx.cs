using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validation;
using System.Data;
using DataAccessHandler;
using CrossPlatformAESEncryption.Helper;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;
using System.IO;
using ClosedXML.Excel;


public partial class invontaryMISReport : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    ClsPatientList objPatient = new ClsPatientList();
    InputValidation Ival = new InputValidation();
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtfromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txttodate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btnexporttoexcel.Visible = false;
            string LabId=Request.Cookies["LabId"].Value.ToString();
            db.bindDrp("select rpt_Name from tbl_inventoryMISReport  ", drpreportName, "rpt_Name", "rpt_Name");
            drpreportName.Items.Insert(0, new ListItem("All", "All"));
        }

    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        string LabId = Request.Cookies["LabId"].Value.ToString();
        string spName = db.getData("select spName from tbl_inventoryMISReport where rpt_Name='" + drpreportName.Text + "'").ToString();
        string frodate = txtfromDate.Text;
        string todate = txttodate.Text;
        string d1 = frodate;
        string d2 = todate;
        DateTime appointdt1 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime appointdt2 = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        string startDate = appointdt1.ToString("MM/dd/yyyy");
        string EndDate = appointdt2.ToString("MM/dd/yyyy");
        gridmisreport.DataSource = GetBranchDetails(spName, LabId, startDate, EndDate);
        gridmisreport.DataBind();
        btnexporttoexcel.Visible = true;
        DataTable dt = new DataTable();
        dt = GetBranchDetails(spName, LabId, startDate, EndDate);//
      
        //int total = dt.AsEnumerable().Sum(row => row.Field<int>("Amount"));
        //gridmisreport.FooterRow.Cells[1].Text = "Total";
        //gridmisreport.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
        //gridmisreport.FooterRow.Cells[2].Text = total.ToString("N2");
    }
    public DataTable GetBranchDetails(string spName, string LabId, string startDate, string EndDate)//
    {
        DataTable dt = new DataTable();
        try
        {
            SqlParameter[] paraminvMISReport = new SqlParameter[]
             {
                                     new SqlParameter("@LabId",LabId),
                                     new SqlParameter("@startDate",startDate),
                                     new SqlParameter("@EndDate",EndDate)

              };
            dt = DAL.ExecuteStoredProcedureDataTable(spName, paraminvMISReport);
            return dt;
        }
        catch (Exception ex)
        {
            dt = null;
            return dt;
        }
        
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
       
    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
       
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", " " + drpreportName.Text + "" + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gridmisreport.AllowPaging = false;
            //BindGridview();
            //Change the Header Row back to white color
            gridmisreport.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Applying stlye to gridview header cells
            for (int i = 0; i < gridmisreport.HeaderRow.Cells.Count; i++)
            {
                gridmisreport.HeaderRow.Cells[i].Style.Add("background-color", "#f3f6ff");
            }
            gridmisreport.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
       

    }
}
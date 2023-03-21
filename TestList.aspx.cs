using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data;
using System.Data.SqlClient;

public partial class SuperAdmin_TestList : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    static int ORGID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
           
            if (!IsPostBack)
            {
                ddlName.DataSource = DAL.GetDataTable("Sp_GetOrgnizationDetailForDDL");
                ddlName.DataBind();
                ListItem lit = new ListItem();
                lit.Text = "Select";
                lit.Value = "0";
                ddlName.Items.Insert(0, lit);
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }  
    [System.Web.Services.WebMethod]
    public static string TestEdit(int testId)
    {
        DataAccessLayer DAL = new DataAccessLayer();
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@OrgId",SuperAdmin_TestList.ORGID),
            new SqlParameter("@TestId",testId),
            new SqlParameter("@ReturnVal",SqlDbType.Int)
        };
        int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOrgTest", param);
        return result.ToString();
    }   
    protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["OrdId"] = Convert.ToInt32(ddlName.SelectedValue);
        SuperAdmin_TestList.ORGID = Convert.ToInt32(Session["OrdId"]);
        DataSet ds = DAL.GetDataSet("Sp_OrgGetAllTestlist " + Session["OrdId"]);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tabTestTakenList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;
                    //Load lab test taken list
                    tabTestTakenList += "<tr >" +
                                       "<td scope='col' style='text-align:center'>" + row["sTestCode"].ToString() + "</td>" +
                                       "<td scope='col' style='text-align:center'>" + row["sTestName"].ToString() + "</td>" +
                                       "<td scope='col' style='text-align:center'>" + row["sProfileName"].ToString() + "</td>" +
                                       "<td scope='col' style='text-align:center'>" + row["sSectionName"].ToString() + "</td>" +
                                       "<td scope='col' style='text-align:center'><a onclick='edit(" + row["sTestId"].ToString() + ")'>" + "<span class='lab-btn-primary nextbtn' >Add</span></a>" + "</td>" +
                                       "</tr>";
                }
                tbodyMyTestList.InnerHtml = tabTestTakenList;
            }
            else
            {
                tbodyMyTestList.InnerHtml = "<tr><td colspan='5'>No records found</td></tr>";
            }
        }
    }
}
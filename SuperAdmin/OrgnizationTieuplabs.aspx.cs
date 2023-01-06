using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_OrgnizationTieuplabs : System.Web.UI.Page
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
    public static string AddLab(int LabId)
    {
        DataAccessLayer DAL = new DataAccessLayer();
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@OrgId",SuperAdmin_OrgnizationTieuplabs.ORGID),
            new SqlParameter("@LabId",LabId),
            new SqlParameter("@ReturnVal",SqlDbType.Int)
        };
        int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOrgLab", param);
        return result.ToString();
    }
    protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["OrgId"] = Convert.ToInt32(ddlName.SelectedValue);
        SuperAdmin_OrgnizationTieuplabs.ORGID = Convert.ToInt32(Session["OrgId"]);
        DataSet ds = DAL.GetDataSet("Sp_OrgGetOrglablist " + Session["OrgId"]);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllLabList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string _mobile = row["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabContact"].ToString()) : "";
                    count = count + 1;
                    AllLabList += "<tr>" +
                                "<td scope='col'>" + row["sLabName"].ToString() + "</td>" +
                                 "<td scope='col'>" + row["sLabAddress"].ToString() + "</td>" +
                                "<td scope='col'>" + row["sLabManager"].ToString() + "</td>" +
                                "<td scope='col'>" + row["sLabStatus"].ToString() + "</td>" +
                                "<td scope='col'>" + _mobile + "</td>" +
                                "<td scope='col' style='text-align:center'><a onclick='edit(" + row["sLabId"].ToString() + ")'>" + "<span class='lab-btn-primary nextbtn' >Add</span></a>" + "</td>" +
                                "</tr>";
                }
                tbodyAllLabList.InnerHtml = AllLabList;
            }
            else
            {
                tbodyAllLabList.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }
    }
}
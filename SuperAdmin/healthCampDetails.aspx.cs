using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Configuration;
using CrossPlatformAESEncryption.Helper;
using System.Text;

public partial class SuperAdmin_healthCampDetails : System.Web.UI.Page
{
    DBClass db = new DBClass();
    DataAccessLayer DAL = new DataAccessLayer();
    string query = string.Empty;
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           if(Request.QueryString["id"]!=null)
           {
               hdnhelthcampId.Value=Request.QueryString["id"].ToString();
               lblhcname.Text = db.getData("select healthCampName from healthCampMaster where healthcampID='" + hdnhelthcampId .Value+ "'");
               showhealthcampDetails();
               hcOrgDetails();
               hcUserDetails();
               hcLabDetails();
           }
        }
    }
    void showhealthcampDetails()
    {
        SqlParameter[] healthcamp = new SqlParameter[]
         {
                new SqlParameter("@id",hdnhelthcampId.Value)           

          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetHealthCamp_ListwithID", healthcamp);
        FormView_labDetails.DataSource = ds;
        FormView_labDetails.DataBind();
    }
    void hcOrgDetails()
    {
        SqlParameter[] labTestList = new SqlParameter[]
        {
                   new SqlParameter("@id",hdnhelthcampId.Value)

         };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetHealthCamp_OrgList", labTestList);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllLabList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;
                    AllLabList += "<tr>" +
                              "<td scope='col'>" + count + "</td>" +
                                "<td scope='col'>" + row["Name"].ToString() + "</td>" +
                                    
                                "<td scope='col'>" + CryptoHelper.Decrypt(row["Contact"].ToString()) + "</td>" +
                                 "<td scope='col'>" + CryptoHelper.Decrypt(row["Email"].ToString()) + "</td>" +
                                  "<td scope='col'>" + row["Org_Status"].ToString() + "</td>" +
                                  "<td scope='col'>" + row["Address"].ToString() + "</td>" +

                                "</tr>";
                }
                tbodyHCOrgList.InnerHtml = AllLabList;
            }
            else
            {
                tbodyHCOrgList.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }
    }

    void hcUserDetails()
    {
        SqlParameter[] hcUser = new SqlParameter[]
        {
                   new SqlParameter("@id",hdnhelthcampId.Value)

         };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetHealthCamp_UserList", hcUser);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllLabList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;
                    AllLabList += "<tr>" +
                              "<td scope='col'>" + count + "</td>" +
                                "<td scope='col'>" + row["sFullName"].ToString() + "</td>" +

                                "<td scope='col'>" + CryptoHelper.Decrypt(row["sMobile"].ToString()) + "</td>" +
                                 "<td scope='col'>" + CryptoHelper.Decrypt(row["sEmailId"].ToString()) + "</td>" +
                                
                                "</tr>";
                }
                tbodyHealthcampUser.InnerHtml = AllLabList;
            }
            else
            {
                tbodyHealthcampUser.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }
    }

    void hcLabDetails()
    {
        SqlParameter[] hcUser = new SqlParameter[]
        {
                   new SqlParameter("@id",hdnhelthcampId.Value)

         };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetHealthCamp_LabList", hcUser);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllLabList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;
                    AllLabList += "<tr>" +
                              "<td scope='col'>" + row["sLabId"].ToString() + "</td>" +
                                "<td scope='col'>" + row["sLabCode"].ToString() + "</td>" +
                                 "<td scope='col'>" + row["sLabName"].ToString() + "</td>" +
                                "<td scope='col'>" + row["sLabManager"].ToString() + "</td>" +
                                 "<td scope='col'>" + CryptoHelper.Decrypt(row["sLabEmailId"].ToString()) + "</td>" +
                                  "<td scope='col'>" + row["sLabStatus"].ToString() + "</td>" +
                                "</tr>";
                }
                tbody_hclab.InnerHtml = AllLabList;
            }
            else
            {
                tbody_hclab.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }
    }
   
}
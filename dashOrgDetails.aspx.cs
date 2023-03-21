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

public partial class SuperAdmin_dashOrgDetails : System.Web.UI.Page
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
               hdnorgid.Value=Request.QueryString["id"].ToString();
               lblorgname.Text = db.getData("select Name from OrganizationMaster where ID='" + hdnorgid.Value + "'");
               showorgDetails();
               OrgBranchDetails();
               employeeList();
               logo();
               //hcLabDetails();
           }
        }
    }
    void logo()
    {
        string getImagePath = db.getData("select Org_Logo FROM OrganizationMaster where ID='" + Request.QueryString["id"].ToString() + "'");
        string str = getImagePath.Replace("../", "");
        StringBuilder sb = new StringBuilder();
        sb.Append(" <img src='http://localhost:54389/vls-asp-dot-net/SuperAdmin/" + str + "' alt='Logo' style='text-align:center; margin: 0 auto 0px;display: block;'>");
        headerdiv.InnerHtml = sb.ToString();
    }
    void showorgDetails()
    {
        SqlParameter[] healthcamp = new SqlParameter[]
         {
                new SqlParameter("@id",hdnorgid.Value)           

          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_Getorg_ListwithID", healthcamp);
        FormView_labDetails.DataSource = ds;
        FormView_labDetails.DataBind();
    }
    void OrgBranchDetails()
    {
        SqlParameter[] labTestList = new SqlParameter[]
        {
                   new SqlParameter("@id",hdnorgid.Value)

         };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_Getorg_Branch_ListwithID", labTestList);

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
                                "<td scope='col'>" + row["branchName"].ToString() + "</td>" +
                                     "<td scope='col'>" + row["status"].ToString() + "</td>" +
                                      "<td scope='col'>" + row["address"].ToString() + "</td>" +
                                       "<td scope='col'>" + row["country"].ToString() + "</td>" +
                                        "<td scope='col'>" + row["state"].ToString() + "</td>" +
                                 "<td scope='col'>" + row["city"].ToString() + "</td>" +
                                  "<td scope='col'>" + row["zipCode"].ToString() + "</td>" +
                                    "<td scope='col'>" + row["mobileNo"].ToString() + "</td>" +
                                      "<td scope='col'>" + row["emailId"].ToString() + "</td>" +
                                "</tr>";
                }
                tbodyorgbrList.InnerHtml = AllLabList;
            }
            else
            {
                tbodyorgbrList.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }
    }

    void employeeList()
    {
        SqlParameter[] hcUser = new SqlParameter[]
        {
                   new SqlParameter("@orgId",hdnorgid.Value)

         };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("sp_getorgdrillEmployeeList", hcUser);

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
                                "<td scope='col'>" + row["FName"].ToString() + "</td>" +
                                 "<td scope='col'>" + row["LName"].ToString() + "</td>" +
                                  "<td scope='col'>" + row["AtdharName"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["Gender"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["Email"].ToString() + "</td>" +
                                "<td scope='col'>" + row["ContactNo"].ToString() + "</td>" +
                                "</tr>";
                }
                tbodyemployeeList.InnerHtml = AllLabList;
            }
            else
            {
                tbodyemployeeList.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }
    }

    //void hcLabDetails()
    //{
    //    SqlParameter[] hcUser = new SqlParameter[]
    //    {
    //               new SqlParameter("@id",hdnhelthcampId.Value)

    //     };
    //    DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetHealthCamp_LabList", hcUser);

    //    if (ds != null)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            string AllLabList = "";
    //            int count = 0;
    //            foreach (DataRow row in ds.Tables[0].Rows)
    //            {
    //                count = count + 1;
    //                AllLabList += "<tr>" +
    //                          "<td scope='col'>" + row["sLabId"].ToString() + "</td>" +
    //                            "<td scope='col'>" + row["sLabCode"].ToString() + "</td>" +
    //                             "<td scope='col'>" + row["sLabName"].ToString() + "</td>" +
    //                            "<td scope='col'>" + row["sLabManager"].ToString() + "</td>" +
    //                             "<td scope='col'>" + CryptoHelper.Decrypt(row["sLabEmailId"].ToString()) + "</td>" +
    //                              "<td scope='col'>" + row["sLabStatus"].ToString() + "</td>" +
    //                            "</tr>";
    //            }
    //            tbody_hclab.InnerHtml = AllLabList;
    //        }
    //        else
    //        {
    //            tbody_hclab.InnerHtml = "<tr><td>No Records Found</td></tr>";
    //        }
    //    }
    //}
   
}
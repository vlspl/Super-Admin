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

public partial class labsdrilldown : System.Web.UI.Page
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
               hdnlabId.Value = Request.QueryString["id"].ToString();
               lbllabName.Text = db.getData("select sLabName from labMaster where sLabId='" + hdnlabId.Value + "'");
               showLabDetails();
               ShowTestList();
               signatury();
               hdnlogoname.Value = db.getData("select slablogo from labMaster where sLabId='"+hdnlabId.Value+"'");
               PatientList();
                DoctorList();
                Package();
                logo();
           }
        }
    }
    void logo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" <img src='http://localhost:54389/vls-asp-dot-net/images/" + hdnlogoname.Value + "' alt='Logo' style='text-align:center; margin: 0 auto 0px;display: block;'>");
        headerdiv.InnerHtml = sb.ToString();
    }
    void Package()
    {
        SqlParameter[] lablist = new SqlParameter[]
         {
                new SqlParameter("@sLabId",hdnlabId.Value)           

          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetLabpackage", lablist);
        FormView_PackageDetails.DataSource = ds;
        FormView_PackageDetails.DataBind();
        
    }
    void PatientList()
    {
        SqlParameter[] labTestList = new SqlParameter[]
        {
                   new SqlParameter("@labId",hdnlabId.Value)

         };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetPatients", labTestList);

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
                                  "<td scope='col'>" + row["sGender"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sAddress"].ToString() + "</td>" +

                                "</tr>";
                }
                tbodyLabPatientList.InnerHtml = AllLabList;
            }
            else
            {
                tbodyLabPatientList.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }
    }
    void DoctorList()
    {
        SqlParameter[] labTestList = new SqlParameter[]
        {
                   new SqlParameter("@labId",hdnlabId.Value)

         };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetDoctorsListbyLabId", labTestList);

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
                                  "<td scope='col'>" + row["sGender"].ToString() + "</td>" +
                                   "<td scope='col'>" + row["sAddress"].ToString() + "</td>" +

                                "</tr>";
                }
                tbodyLabDoctors.InnerHtml = AllLabList;
            }
            else
            {
                tbodyLabDoctors.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }
    }
    void showLabDetails()
    {
         SqlParameter[] lablist = new SqlParameter[]
         {
                new SqlParameter("@sLabId",hdnlabId.Value)           

          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetLabListwithLabId", lablist);
        FormView_labDetails.DataSource = ds;
        FormView_labDetails.DataBind();
    }
   
         void ShowTestList()
    {
         SqlParameter[] labTestList = new SqlParameter[]
         {
                   new SqlParameter("@sLabId",hdnlabId.Value)                     

          };
         DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetLabTestListbydashboard", labTestList);

        gridlabTestList.DataSource = ds;//"Sp_getlabtestList"+drplablist.Text);
        gridlabTestList.DataBind();
    }

         protected void gridlabTestList_PageIndexChanging(object sender, GridViewPageEventArgs e)
         {
             gridlabTestList.PageIndex = e.NewPageIndex;
             ShowTestList();
         }
 
    void signatury()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("select DSId,SignHolder,Department,SignImage ,SignStatus,SLabId from DigitalSignature where sLabId='" + hdnlabId.Value + "'  order by 1 desc", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds, "DigitalSignature");
            con.Open();
            gvImages.DataSource = ds;
            gvImages.DataBind();
            con.Close();
        }
    }
   
   
}
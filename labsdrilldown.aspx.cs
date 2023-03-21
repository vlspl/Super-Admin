using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessHandler;
using System.Data.SqlClient;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_labsdrilldown : System.Web.UI.Page
{
    DBClass db = new DBClass();
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showLabList();
           
        }
    }
    void showLabList()
    {
        SqlParameter[] lablist = new SqlParameter[]
         {
               // new SqlParameter("@sLabId",drplablist.SelectedValue)           

          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetLabList", lablist);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllLabList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string _mobile = row["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabContact"].ToString()) : "";
                    string EmailId = row["sLabEmailId"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabEmailId"].ToString()) : "";
                    count = count + 1;
                    AllLabList += "<tr>" +
                          "<td scope='col'><a href='labDetails.aspx?id=" + row["sLabId"].ToString() + "' class='lab-btn-secondary'>View</a></td>" +
                              //"<td scope='col'>" + count + "</td>" +
                                "<td scope='col'>" + row["sLabName"].ToString() + "</td>" +
                                "<td scope='col'>" + row["sLabManager"].ToString() + "</td>" +
                                  "<td scope='col'>" + _mobile + "</td>" +
                                   "<td scope='col'>" + EmailId + "</td>" +
                              
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
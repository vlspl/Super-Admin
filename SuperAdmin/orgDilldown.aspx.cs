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

public partial class SuperAdmin_orgDilldown : System.Web.UI.Page
{
    DBClass db = new DBClass();
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showhealthcamp();
           
        }
    }
    void showhealthcamp()
    {
        SqlParameter[] healthcamp = new SqlParameter[]
         {
               // new SqlParameter("@sLabId",drplablist.SelectedValue)           

          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_Getorg_List", healthcamp);
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
                          "<td scope='col'><a href='dashOrgDetails.aspx?id=" + row["ID"].ToString() + "' class='lab-btn-secondary'>View</a></td>" +
                                "<td scope='col'>" + row["ID"].ToString() + "</td>" +
                                "<td scope='col'>" + row["Name"].ToString() + "</td>" +
                                  "<td scope='col'>" + row["Address"].ToString() + "</td>" +
                                   "<td scope='col'>" + CryptoHelper.Decrypt(row["Contact"].ToString()) + "</td>" +
                                    "<td scope='col'>" + CryptoHelper.Decrypt(row["Email"].ToString()) + "</td>" +
                                "</tr>";
                }
                tbodyAllorgList.InnerHtml = AllLabList;
            }
            else
            {
                tbodyAllorgList.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }
       
    }
   
   
   
  
   
}
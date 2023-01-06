using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DataAccessHandler;

public partial class CRMDashboard : System.Web.UI.Page
{
    DBClass db = new DBClass();
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showGrid();
        }
    }
    void showGrid()
    {
        string labId = Request.Cookies["labId"].Value.ToString();
        SqlParameter[] paramEmg_getMaterial = new SqlParameter[]
         {
                           
              new SqlParameter("@sLabId", labId)
          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_getmsgStatus", paramEmg_getMaterial);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllUserList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;
                   // string name = db.getData("select sFullName from appUser where sMobile='" + row["mobileNo"].ToString() + "'");
                    //Load lab test taken list
                    AllUserList += "<tr>" +
                                       "<td scope='col'>" + count + "</td>" +
                                       //   "<td scope='col'>" + name + "</td>" +
                                     "<td scope='col'>" + row["mobileNo"].ToString() + "</td>" +
                                         "<td scope='col'>" + row["msgName"].ToString() + "</td>" +
                                          
                                            "<td scope='col'>" + row["date"].ToString() + "</td>" +
                                             "<td scope='col'>" + row["status"].ToString() + "</td>" +
                                       "</tr>";



                }
                tbody_messageStatus.InnerHtml = AllUserList;
            }
            else
            {
                tbody_messageStatus.InnerHtml = "<tr><td colspan='5'>No records found</td></tr>";
            }
        }

    }
}
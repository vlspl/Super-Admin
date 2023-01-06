using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

public partial class ViewTemplateRequest : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    DBClass db = new DBClass();
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
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_gettemplateList", paramEmg_getMaterial);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllUserList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    count = count + 1;
                    //Load lab test taken list
                    AllUserList += "<tr>" +
                                       "<td scope='col'>" + count + "</td>" +
                                     "<td scope='col'>" + row["msgName"].ToString() + "</td>" +
                                         "<td scope='col'>" + row["body"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["paramList"].ToString() + "</td>" +
                                            "<td scope='col'>" + row["requestBy"].ToString() + "</td>" +
                                             "<td scope='col'>" + row["approveDate"].ToString() + "</td>" +
                                              "<td scope='col'>" + row["status"].ToString() + "</td>" +
                                               "<td scope='col'><a href='TemplateRequest.aspx?requestId="+ row["whatsappMasterId"] +"' class='btn btn-sm btn-color'>View</a></td>" +
                                       "</tr>";



                }
                tbodytemplateRequest.InnerHtml = AllUserList;
            }
            else
            {
                tbodytemplateRequest.InnerHtml = "<tr><td colspan='5'>No records found</td></tr>";
            }
        }

    }



   
}


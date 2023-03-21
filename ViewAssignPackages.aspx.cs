using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_ViewAssignPackages : System.Web.UI.Page
{
    CLSpackageMaster pkgmstr = new CLSpackageMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                getListOfpackageMaster();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }
    public void getListOfpackageMaster()
    {
        DataSet ds = pkgmstr.Get_assignpackageMaster();
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllLabList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //string _Mobile = row["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabContact"].ToString()) : "";
                    count = count + 1;
                    AllLabList += "<tr>" + "<td scope='col'>" + count + "</td>" +
                                "<td scope='col'>" + row["packageName"].ToString() + "</td>" +
                                "<td scope='col'>" + row["sLabName"].ToString() + "</td>" +
                                "<td scope='col'>" + row["price"].ToString() + "</td>" +
                                "<td scope='col'>" + row["assignDate"].ToString() + "</td>" +
                                 "<td scope='col'>" + row["days"].ToString() + "</td>" +
                              "<td scope='col'>" + row["expiredDate"].ToString() + "</td>" +
                           
                                "</tr>";
                }
                tbodyAllLabApproval.InnerHtml = AllLabList;
            }
            else
            {
                tbodyAllLabApproval.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }
    }
}
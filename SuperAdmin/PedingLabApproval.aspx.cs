using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_PedingLabApproval : System.Web.UI.Page
{
    CLSLabApproval objLabsApproval = new CLSLabApproval();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                getListOfLabsApproval();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }
    public void getListOfLabsApproval()
    {
        DataSet ds = objLabsApproval.GetLabApprovalList_dash();
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
                    AllLabList += "<tr>" + "<td scope='col'>" + row["Temp_LabId"].ToString() + "</td>" +
                                "<td scope='col'>" + row["sLabName"].ToString() + "</td>" +
                                "<td scope='col'>" + row["sLabAddress"].ToString() + "</td>" +
                                "<td scope='col'>" + row["Name"].ToString() + "</td>" +
                                "<td scope='col'>" + row["sLabStatus"].ToString() + "</td>" +
                                //"<td scope='col'>" + _Mobile + "</td>" +
                                "<td scope='col'><a href='LabApprovalView.aspx?id=" + row["Temp_LabId"].ToString() + "' class='lab-btn-secondary'>View</a></td>" +
                               
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
using System;
using System.Web.UI;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_LabsManagement : System.Web.UI.Page
{
    CLSLabsManagement objLabsManagement = new CLSLabsManagement();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                getListOfLabs();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }
    public void getListOfLabs()
    {
        DataSet ds = objLabsManagement.GetLabList();
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllLabList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string _Mobile = row["sLabContact"].ToString() != "" ? CryptoHelper.Decrypt(row["sLabContact"].ToString()) : "";
                    count = count + 1;
                    AllLabList += "<tr>" +
                                "<td scope='col'>" + row["sLabCode"].ToString() + "</td>" +
                                "<td scope='col'>" + row["sLabName"].ToString() + "</td>" +
                                "<td scope='col'>" + row["sLabManager"].ToString() + "</td>" +
                                "<td scope='col'>" + row["sLabStatus"].ToString() + "</td>" +
                                "<td scope='col'>" + _Mobile + "</td>" +
                                "<td scope='col'><a href='LabsManagementEditLab.aspx?id=" + row["sLabId"].ToString() + "' class='lab-btn-secondary'>Edit</a></td>" +
                                "<td scope='col'><a href='LabsManagementLabDetails.aspx?id=" + row["sLabId"].ToString() + "' class='lab-btn-secondary'>Details</a></td>" +
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
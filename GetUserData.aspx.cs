using System;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;

public partial class SuperAdmin_GetUserData : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    DataTable dt = new DataTable();
    int id;
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!IsPostBack)
            {

                getUserOTP();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    void getUserOTP()
    {
        SqlParameter[] paramEmgAgeRatio = new SqlParameter[]
         {
                           

          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_getOTPList", paramEmgAgeRatio);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllOtp = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string _Mobile = row["sMobile"].ToString() != "" ? CryptoHelper.Decrypt(row["sMobile"].ToString()) : "";
                    count = count + 1;
                    AllOtp += "<tr>" +
                         "<td scope='col'>" + count + "</td>" +
                                "<td scope='col'>" + row["sFullName"].ToString() + "</td>" +
                                "<td scope='col'>" + _Mobile + "</td>" +
                                "<td scope='col'>" + row["OTP"].ToString() + "</td>" +
                                 "</tr>";
                }
                tbodyAlluserOTP.InnerHtml = AllOtp;
            }
            else
            {
                tbodyAlluserOTP.InnerHtml = "<tr><td>No Records Found</td></tr>";
            }
        }
      
    }
   

  
}
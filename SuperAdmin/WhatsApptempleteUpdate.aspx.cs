using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class SuperAdmin_WhatsApptempleteUpdate : System.Web.UI.Page
{
    private DataTable dtFillData;
    //DemoRequestDAL objdal = new DemoRequestDAL();
    WhatsAppMasterDAL objdal = new WhatsAppMasterDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
        {
            if (Request.QueryString["whatsappMasterId"] != null)
            {
                dtFillData = objdal.fillData(Request.QueryString["whatsappMasterId"]);
               // string Status = "";
                foreach (DataRow row in dtFillData.Rows)
                {
                    txtApproveDate.Text = row["approveDate"].ToString();
                    txtApprovalBy.Text = row["approveBy"].ToString();
                    txtTempName.Text = row["msgName"].ToString();
                    txtMsgText.Text = row["body"].ToString();
                    txtParameter.Text = row["noOfParameters"].ToString();
                    txtparamList.Text = row["paramList"].ToString();
                    txtRequestBy.Text = row["requestBy"].ToString();
                    drpApprovalStatus.SelectedValue = row["status"].ToString();
                }
            }
        }
    }
   



    protected void whatsAppTemplete_Click(object sender, EventArgs e)
    {
    //    int status = 0;
    //    if (txtApprovalStatus.Text == "Approved")
    //        status = 0;
    //    else if (txtApprovalStatus.Text == "Rejected")
        
    //        status = 1;
        
    //    else
        
    //        status = 2;
        
         

        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("Sp_UpdateWhatAppMsgDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@whatsappMasterId", Request.QueryString["whatsappMasterId"].ToString());
        cmd.Parameters.AddWithValue("@status", drpApprovalStatus.SelectedValue);
        cmd.Parameters.AddWithValue("@approveDate", Convert.ToDateTime(txtApproveDate.Text,
        System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat));
        cmd.Parameters.AddWithValue("@TempleteName", txtTempName.Text);
        cmd.Parameters.AddWithValue("@Messagetext", txtMsgText.Text);
        cmd.Parameters.AddWithValue("@Parameter", txtParameter.Text);
        cmd.Parameters.AddWithValue("@requestBy ", txtRequestBy.Text);
        cmd.Parameters.AddWithValue("@approveBy ", txtApprovalBy.Text);

        con.Open();

        cmd.ExecuteNonQuery();
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert(\"Update Successfully!\");location.href='WhatsAppTempleteCreation.aspx';", true);
        con.Close();
    }
}
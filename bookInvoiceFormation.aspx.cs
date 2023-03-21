using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Validation;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Configuration;
using DataAccessHandler;


public partial class SuperAdmin_bookInvoiceFormation : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    DBClass db = new DBClass();
    CLSLabApprovalGetDetails objGetLabApproval = new CLSLabApprovalGetDetails();
    CLSreportFormation objreportfor = new CLSreportFormation();
    InputValidation Ival = new InputValidation();
    int labid;
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
               
                ddllabName.DataSource = DAL.GetDataTable("Sp_GetlabNameForDDL");
                ddllabName.DataBind();
              

            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }
   
   protected void ChangeStatus(object sender, EventArgs e)
    {
        //string constr = ConfigurationManager.ConnectionStrings["ConString2"].ConnectionString;

        try
        {

            string Status = "";
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            int ImageId = Convert.ToInt32(this.gridreportFormation.DataKeys[row.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(constr))
            {
                Status = db.getData("select status from  BookingInvoiceFormation WHERE BookingInvocerptId = '" + ImageId + "'");
                if (Status == "1")
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE BookingInvoiceFormation SET status = '0' WHERE BookingInvocerptId = @ImageId", con))
                    {
                        cmd.Parameters.AddWithValue("@ImageId", ImageId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //Response.Redirect(Request.Url.AbsoluteUri);
                    }
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE BookingInvoiceFormation SET status = '1' WHERE BookingInvocerptId = @ImageId", con))
                    {
                        cmd.Parameters.AddWithValue("@ImageId", ImageId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //Response.Redirect(Request.Url.AbsoluteUri);
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Update successfully');location.href='bookInvoiceFormation.aspx';", true);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ddllabName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string labId = db.getData("select sLabId from LabMaster where sLabName='"+ddllabName.SelectedItem.Text+"'").ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("select BookingInvocerptId,sLabId,sectionName,status from BookingInvoiceFormation where sLabId='" + labId + "'", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds, "BookingInvoiceFormation");
            con.Open();
            gridreportFormation.DataSource = ds;
            gridreportFormation.DataBind();
            con.Close();
        }
    }
    void bindGrid(string labId)
    {
       
    }
    protected void btnaddreportFormation_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpsection.Text != "-Select-")
            {
                string status = string.Empty;
                if (txtstatus.Text == "")
                {
                    status = "0";
                }
                else
                {
                    status = txtstatus.Text;
                }
                string labId = db.getData("select sLabId from LabMaster where sLabName='" + ddllabName.SelectedItem.Text + "'").ToString();
                objreportfor.sLabId = labId.ToString();
                objreportfor.sectonName = drpsection.Text.ToString();
                objreportfor.status = status;


                if (objreportfor.insertFormat() == 0)
                {
                    Response.Redirect(@"bookInvoiceFormation.aspx");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Section');", true);
            }

          
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
        }

    }

    //protected void gridreportFormation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    SqlDataAdapter da;
    //    SqlConnection con;
    //    DataSet ds = new DataSet();
    //    SqlCommand cmd = new SqlCommand();
    //    con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    //    cmd.Connection = con;
    //    Label lbldeleteID = (Label)gridreportFormation.Rows[e.RowIndex].FindControl("trfID");
    //    cmd.CommandText = "Delete from testReportFormation where trfID='" + lbldeleteID.Text + "'";
    //    con.Open();
    //    cmd.ExecuteNonQuery();
    //    con.Close();
    //    Response.Redirect("reportFormation.aspx", false);
    //}
}
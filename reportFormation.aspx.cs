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


public partial class SuperAdmin_reportFormation : System.Web.UI.Page
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
                db.bindDrp("select distinct SectionName from ReportSectionMaster", drpsection, "SectionName", "SectionName");
               // drpsection.Items.Insert(0, new ListItem("All", "All"));
                drpsection.Items.Insert(0, new ListItem("-Select Section-"));

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
                Status = db.getData("select status from testReportFormation WHERE trfID = '" + ImageId + "'");
                if (Status == "1")
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE testReportFormation SET status = '0' WHERE trfID = @ImageId", con))
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE testReportFormation SET status = '1' WHERE trfID = @ImageId", con))
                    {
                        cmd.Parameters.AddWithValue("@ImageId", ImageId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //Response.Redirect(Request.Url.AbsoluteUri);
                    }
                }
            }
			  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Update successfully');location.href='reportFormation.aspx';", true);
            //Response.Redirect(@"reportFormation.aspx", false);
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
            SqlCommand cmd = new SqlCommand("select trfID,sLabId,sectionName,status,Details from testReportFormation where sLabId='" + labId + "'", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds, "testReportFormation");
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
            string status = string.Empty;
            if (txtstatus.Text == "")
            {
                status = "1";
            }
            else
            {
                status = txtstatus.Text;
            }
            if (ddllabName.Text != "Select Lab")
            {
                if (drpsection.Text != "-Select Section-")
                {
                    string labId = db.getData("select sLabId from LabMaster where sLabName='" + ddllabName.SelectedItem.Text + "'").ToString();
                    objreportfor.sLabId = labId.ToString();
                    objreportfor.sectonName = drpsection.Text.ToString();
                    objreportfor.status = status;
                    objreportfor.details = txtdetails.Text.ToString();


                    if (objreportfor.insertFormatreport() == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Added successfully');location.href='reportFormation.aspx';", true);

                    }

                    else if (objreportfor.insertFormatreport() == 1)
                    {
                        Label lblMasterStatus = (Label)Master.FindControl("lblmsgText");
                        lblMasterStatus.Text = "Error while Report Formatting";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "Yopopupalert();", true);

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertError", "alert('Error while registering the lab');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Section');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Lab');", true);
            }
           
           
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
        }

    }

    protected void gridreportFormation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlDataAdapter da;
        SqlConnection con;
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        cmd.Connection = con;
        Label lbldeleteID = (Label)gridreportFormation.Rows[e.RowIndex].FindControl("trfID");
        cmd.CommandText = "Delete from testReportFormation where trfID='" + lbldeleteID.Text + "'";
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        Response.Redirect("reportFormation.aspx", false);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
}
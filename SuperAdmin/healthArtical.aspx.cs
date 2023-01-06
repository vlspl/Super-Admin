using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using System.Configuration;

public partial class SuperAdmin_healthArtical : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminId"].Value != null)
        {
            if (!Page.IsPostBack)
            {
                gridhealthArtical.DataSource = GetarticalDetails();
                gridhealthArtical.DataBind();
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    public DataTable GetarticalDetails()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = DAL.GetDataTable("Sp_GethealthArtical");
            
            return dt;
        }
        catch (Exception)
        {
            dt = null;
            return dt;
        }
    }
    protected void ChangeStatus(object sender, EventArgs e)
    {
       
        try
        {

            string Status = "";
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            int ImageId = Convert.ToInt32(this.gridhealthArtical.DataKeys[row.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(constr))
            {
                Status = db.getData("select status from tbl_healthArtical WHERE healthArticalId = '" + ImageId + "'");
                if (Status == "1")
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE tbl_healthArtical SET status = '0' WHERE healthArticalId = @ImageId", con))
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE tbl_healthArtical SET status = '1' WHERE healthArticalId = @ImageId", con))
                    {
                        cmd.Parameters.AddWithValue("@ImageId", ImageId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //Response.Redirect(Request.Url.AbsoluteUri);
                    }
                }
            }
            Response.Redirect(@"healthArtical.aspx", false);
        }
        catch (Exception ex)
        {

            ex.Message.ToString();
        }
    }
}
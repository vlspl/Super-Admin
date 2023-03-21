using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using System.Data.SqlClient;
using System.Configuration;

public partial class SuperAdmin_ViewPackages : System.Web.UI.Page
{
    DBClass db = new DBClass();
    CLSpackageMaster pkgmstr = new CLSpackageMaster();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    int returnval;
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
        DataSet ds = pkgmstr.Get_packageMaster();
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    { 
       
        con.Open();  
      int id =  int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());  
      string query = "sp_deletePackage";  
      SqlCommand  com = new SqlCommand(query, con);  
      com.CommandType =CommandType .StoredProcedure;  
      com.Parameters.AddWithValue("@packageId", id).ToString();
      com.Parameters.AddWithValue("@returnval", SqlDbType.Int).ToString();
      
        int retrnval=com.ExecuteNonQuery();  
      con.Close();
        if (retrnval == -1)
        
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Package already Used');location.href='ViewPackages.aspx';", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Package deleted successfully');location.href='ViewPackages.aspx';", true);

        //getListOfpackageMaster(); 
    }
    protected void ChangeStatus(object sender, EventArgs e)
    {
        //string constr = ConfigurationManager.ConnectionStrings["ConString2"].ConnectionString;

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string Status = "";
            Button btn = sender as Button;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            int ImageId = Convert.ToInt32(this.GridView1.DataKeys[row.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(constr))
            {
                Status = db.getData("select status from packageMaster WHERE pMasterId = '" + ImageId + "'");
                if (Status == "Active")
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE packageMaster SET status = 'Deactive' WHERE pMasterId = @ImageId", con))
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE packageMaster SET status = 'Active' WHERE pMasterId = @ImageId", con))
                    {
                        cmd.Parameters.AddWithValue("@ImageId", ImageId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //Response.Redirect(Request.Url.AbsoluteUri);
                    }
                }
            }
            Response.Redirect(@"ViewPackages.aspx", false);
        }
        catch (Exception ex)
        {

            ex.Message.ToString();
        }
    }
}
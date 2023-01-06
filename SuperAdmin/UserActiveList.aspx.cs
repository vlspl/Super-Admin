using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data;
using System.Data.SqlClient;

public partial class SuperAdmin_UserActive : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (true)//Request.Cookies["AdminId"].Value != null
        {
            if (!IsPostBack)
            {
                gridBind();
               
            }
        }
        else
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    void gridBind()
    {
        griduserActivation.DataSource = DAL.GetDataTable("Sp_userActivationlist");
        griduserActivation.DataBind();
    }
    protected void griduserActivation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        griduserActivation.PageIndex = e.NewPageIndex;
        gridBind();
    }


    protected void griduserActivation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = griduserActivation.Rows[rowIndex];

            //Fetch value of Name.
            string userId = row.Cells[0].Text;
            //Fetch value of Country

           // string str = "Data Source=.\\sqlexpress;Initial Catalog=MedicalDbBackup;Integrated Security=True";
            string str = "Data Source=202.154.161.105;Initial Catalog=MedicalDbBackup;Persist Security Info=True;User ID=Howzu;Password=@Vls1234#?";
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("Sp_useractivationbygrid", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@loginAttemptCounter", "0");
            cmd.Parameters.AddWithValue("@loginStatus", "A");
            cmd.Parameters.AddWithValue("@UserId", userId);  
          
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            Response.Redirect(@"UserActiveList.aspx");
             
            
        }
    }
}
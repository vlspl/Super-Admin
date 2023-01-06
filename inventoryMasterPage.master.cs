using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Configuration;

using System.Data.SqlClient;


public partial class inventoryMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //try
            //{
               CsrfHandler.Validate(this.Page, forgeryToken);
            //    if (Request.Cookies["loggedIn"].Value == null || Request.Cookies["loggedIn"].Value != "true" || Request.Cookies["labUser"].Value.ToString() == "")
            //    {
            //        Response.Redirect("LabLogin.aspx");
            //    }
            //    else
            //    {
            Session["Count"] = "0";
            // lbluser.Text = Request.Cookies["labUser"].Value.ToString();
            Label1.Text = Request.Cookies["labUser"].Value.ToString();
            Label2.Text = Request.Cookies["role"].Value.ToString();
           SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            Conn.Open();
            string query = "select sLabName from labMaster inner join labUser on labMaster.sLabId=labUser.sLabId where labUser.sFullName='" + Label1.Text + "'";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                lbllabname.Text = dr["sLabName"].ToString();

            }

            Conn.Close();
            // }
            //}
            //catch
            //{
            //    Session.Clear();
            //    Response.Cookies["loggedIn"].Expires = DateTime.Now.AddDays(-1);
            //    Response.Redirect("LabLogin.aspx");
            //}
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Cookies["loggedIn"].Expires = DateTime.Now.AddDays(-1);
        Response.Redirect("LabLogin.aspx");
    }
  
}

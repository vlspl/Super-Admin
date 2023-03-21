using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;
using DataAccessHandler;

public partial class SuperAdmin_SliderUpload : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    DBClass db = new DBClass();
    string query = string.Empty;
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            filldata();
            db.bindDrp("select distinct languageID, languageName from tbl_UserLanguagesMaster  order by languageName asc", drplanguage, "languageName", "languageName");
            drplanguage.Items.Insert(0, new ListItem("-Select-", "-Select-"));
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {




    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {


    }
    private void filldata()
    {



        try
        {


            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("select ImageId,ImageTitle,'..'+ImagePath as ImagePath,prefered_language,IsActive from Dashboardslider where IsActive='1'  order by 1 desc", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds, "Dashboardslider");
                con.Open();
                gvImages.DataSource = ds;
                gvImages.DataBind();
                con.Close();
            }
        }
        catch (Exception ex)
        {

            ex.Message.ToString();
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
            int ImageId = Convert.ToInt32(this.gvImages.DataKeys[row.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(constr))
            {
                Status = db.getData("select IsActive from Dashboardslider WHERE ImageId = '" + ImageId + "'");
                    if (Status == "1")
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE Dashboardslider SET IsActive = '0' WHERE ImageId = @ImageId", con))
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
                        using (SqlCommand cmd = new SqlCommand("UPDATE Dashboardslider SET IsActive = '1' WHERE ImageId = @ImageId", con))
                        {
                            cmd.Parameters.AddWithValue("@ImageId", ImageId);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            //Response.Redirect(Request.Url.AbsoluteUri);
                        }
                    }
            }
            Response.Redirect(@"SliderUpload.aspx", false);
        }
        catch (Exception ex)
        {

            ex.Message.ToString(); 
        }
    }
 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {


            string filename = Path.GetFileName(fileuploadimages.PostedFile.FileName);
            //Save images into Images folder
            fileuploadimages.SaveAs(Server.MapPath("../Images/" + filename));


            //Getting dbconnection from web.config connectionstring
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("Insert into Dashboardslider(ImageTitle,ImagePath,IsActive,prefered_language) values(@ImageTitle,@ImagePath,@IsActive,@lang)", con);
                //Passing parameters to query
                cmd.Parameters.AddWithValue("@ImageTitle", txtimgtitle.Text);
                cmd.Parameters.AddWithValue("@ImagePath", "/Images/" + filename);
                cmd.Parameters.AddWithValue("@IsActive", "1");
                cmd.Parameters.AddWithValue("@lang", drplanguage.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                //Close dbconnection
                con.Close();
                Response.Redirect(@"SliderUpload.aspx");
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
        }
    }
    protected void gvImages_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvImages.PageIndex = e.NewPageIndex;
        filldata();
    }
    protected void drpstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpstatus.Text == "Active")
            {
                query = "select ImageId,ImageTitle,'..'+ImagePath as ImagePath,IsActive,prefered_language from Dashboardslider where IsActive='1'  order by 1 desc";

            }
            else
            {
                query = "select ImageId,ImageTitle,'..'+ImagePath as ImagePath,IsActive,prefered_language from Dashboardslider where IsActive='0'  order by 1 desc";
            }
            using (SqlConnection con = new SqlConnection(constr))
            {

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds, "Dashboardslider");
                con.Open();
                gvImages.DataSource = ds;
                gvImages.DataBind();
                con.Close();
            }
        }
        catch (Exception ex)
        {

            ex.Message.ToString();
        }
    }
}
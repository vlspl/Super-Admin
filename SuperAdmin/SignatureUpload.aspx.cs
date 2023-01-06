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

public partial class SuperAdmin_SignatureUpload : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    DBClass db = new DBClass();
    string query = string.Empty;
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            ddllabName.DataSource = DAL.GetDataTable("Sp_GetlabNameForDDL");
            ddllabName.DataBind();
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
                Status = db.getData("select SignStatus from DigitalSignature WHERE DSId = '" + ImageId + "'");
                    if (Status == "1")
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE DigitalSignature SET SignStatus = '0' WHERE DSId = @ImageId", con))
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
                        using (SqlCommand cmd = new SqlCommand("UPDATE DigitalSignature SET SignStatus = '1' WHERE DSId = @ImageId", con))
                        {
                            cmd.Parameters.AddWithValue("@ImageId", ImageId);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            //Response.Redirect(Request.Url.AbsoluteUri);
                        }
                    }
            }
            Response.Redirect(@"SignatureUpload.aspx", false);
        }
        catch (Exception ex)
        {

            ex.Message.ToString(); 
        }
    }
    protected void DeleteRecord(object sender, EventArgs e)
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
                SqlCommand cmd = new SqlCommand("delete from DigitalSignature WHERE DSId = '" + ImageId + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                //Close dbconnection
                con.Close();
              // string deleterecord=db.getData("");
                Response.Redirect(@"SignatureUpload.aspx", false);
            }
           
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
            fileuploadimages.SaveAs(Server.MapPath("../images/" + filename));


            //Getting dbconnection from web.config connectionstring
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("Insert into DigitalSignature(SignHolder,Department,SignImage,SignStatus,SLabId) values(@SignHolder,@Department,@SignImage,@SignStatus,@SLabId)", con);
                //Passing parameters to query
                cmd.Parameters.AddWithValue("@SignHolder", txtimgtitle.Text);
                cmd.Parameters.AddWithValue("@Department", txtdepartment.Text);
                cmd.Parameters.AddWithValue("@SignImage", "../images/" + filename);
                cmd.Parameters.AddWithValue("@SignStatus", "1");
                cmd.Parameters.AddWithValue("@SLabId", ddllabName.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                //Close dbconnection
                con.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Image Upload successfully');location.href='SignatureUpload.aspx';", true);
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
        }
    }
   
    protected void drpstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpstatus.Text == "Active")
            {
                query = "select DSId,SignHolder,'..'+SignImage ,SignStatus from DigitalSignature where SignStatus='1'  order by 1 desc";

            }
            else
            {
                query = "select DSId,SignHolder,'..'+SignImage ,SignStatus from DigitalSignature where SignStatus='0'  order by 1 desc";
            }
            using (SqlConnection con = new SqlConnection(constr))
            {

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds, "DigitalSignature");
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
    protected void ddllabName_SelectedIndexChanged(object sender, EventArgs e)
    {
       using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("select DSId,SignHolder,Department,SignImage ,SignStatus,SLabId from DigitalSignature where sLabId='" + ddllabName.Text + "'  order by 1 desc", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds, "DigitalSignature");
            con.Open();
            gvImages.DataSource = ds;
            gvImages.DataBind();
            con.Close();
        }
    }
}
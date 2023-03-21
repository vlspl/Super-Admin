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
using System.Drawing;
using System.Drawing.Drawing2D;


public partial class SuperAdmin_imageCompress : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
       
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            //string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //string storedb = "Images/" + filename + "";
            //string targetPath = Server.MapPath("Images/" + filename);
            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //Save images into Images folder
            FileUpload1.SaveAs(Server.MapPath("Images/" + filename));
            string targetPath = Server.MapPath("Images/" + filename);
            Stream strm = FileUpload1.PostedFile.InputStream;
            var targetFile = targetPath;
            string storedb = "Images/" + filename + "";

            ReduceImageSize(0.5, strm, targetFile);
            //insert reduced size image in db
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into tblImageInfo(Name,Content)values('" + txtname.Text + "','" + storedb + "')", con);
            cmd.ExecuteNonQuery();
        }

    }

    protected void BindGridView()
    {
        DataTable dt = new DataTable();
        string strQuery = "select * from tblImageInfo order by id desc";
        SqlCommand cmd = new SqlCommand(strQuery);
        SqlDataAdapter da = new SqlDataAdapter();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            da.SelectCommand = cmd;
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            con.Close();
            da.Dispose();
            con.Dispose();
        }

    }
    private void ReduceImageSize(double scaleFactor, Stream sourcePath, string targetPath)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            var newWidth = (int)(image.Width * scaleFactor);
            var newHeight = (int)(image.Height * scaleFactor);
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(targetPath, image.RawFormat);
        }
    }
}
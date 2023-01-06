using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO; 

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void ExportToImage(object sender, EventArgs e)
    {
        //string base64 = Request.Form[hfImageData.UniqueID].Split(',')[1];
        //byte[] bytes = Convert.FromBase64String(base64);
        //Response.Clear();
        //Response.ContentType = "image/png";
        //Response.AddHeader("Content-Disposition", "attachment; filename=HTML.png");
        //Response.Buffer = true;
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.BinaryWrite(bytes);
        //Response.End();




        string base64 = Request.Form[hfImageData.UniqueID].Split(',')[1];
        byte[] bytes = Convert.FromBase64String(base64);
        Response.Clear();
        Response.ContentType = "image/png";
        Response.AddHeader("Content-Disposition", "attachment; filename=HTML.png");
        Response.Buffer = true;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.BinaryWrite(bytes);
        MemoryStream storeStream = new MemoryStream();
        MemoryStream ms = new MemoryStream(bytes);
        //write to file  
        FileStream file = new FileStream(Server.MapPath("~/images/") + "myimg2.png", FileMode.Create,
        FileAccess.Write);
        ms.WriteTo(file);
        file.Close();
        ms.Close();
         Response.End();

    }

}
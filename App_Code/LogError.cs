using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using context = System.Web.HttpContext;  

/// <summary>
/// Summary description for LogError
/// </summary>
public class LogError
{
	public LogError()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private static String exepurl;
    static SqlConnection con;
    private static void connecttion()
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ToString();
        con = new SqlConnection(constr);
        con.Open();
    }  
    public static void LoggerCatch(Exception ex)
    {
        connecttion();
        exepurl = context.Current.Request.Url.ToString();
        SqlCommand com = new SqlCommand("ExceptionLoggingToDataBase", con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.AddWithValue("@ExceptionMsg", ex.Message.ToString());
        com.Parameters.AddWithValue("@ExceptionType", ex.GetType().Name.ToString());
        com.Parameters.AddWithValue("@ExceptionURL", exepurl);
        com.Parameters.AddWithValue("@ExceptionSource", ex.StackTrace.ToString());
        com.Parameters.AddWithValue("@date", DateTime.Now.ToString("dd/MM/yyyy hh:mm:tt"));
        com.ExecuteNonQuery();
    }
   public static void Log(String error)
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~/ErrorLog/logger.txt"), true))
        {
            file.WriteLine(DateTime.Now + ":\t" + error);
            file.Close();
        }
    }
}
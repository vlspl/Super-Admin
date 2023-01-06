<%@ WebHandler Language="C#" Class="GCMNotification" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class GCMNotification : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();


        switch (Action)
        {
            case "GCMNotification":

                try
                {
                    //RegisterId you got from Android Developer.
                    string deviceId = Convert.ToString(context.Request.QueryString["regKey"]);
                    string response = "Done";
                    string message = "Demo Notification";
                    string tickerText = "Patient Registration";
                    string contentTitle = "Trial GCM";
                    string postData =
                    "{ \"registration_ids\": [ \"" + deviceId + "\" ], " +
                      "\"data\": {\"tickerText\":\"" + tickerText + "\", " +
                                 "\"contentTitle\":\"" + contentTitle + "\", " +
                                 "\"message\": \"" + message + "\"}}";

                    GCMNotification apnGCM = new GCMNotification();
                    response = apnGCM.SendGCMNotification1("AIzaSyAtIq9gFi4j_h3a41VfVntUY6whebQUglw", postData, deviceId);
                    //response = SendGCMNotification("AIzaSyAG25ispDIrDORIW9VYae0Cv5xiMuWK0YU", postData, deviceId);
                    context.Response.Write("DOne =" + response);
                }
                catch (Exception ex)
                {
                    //--------------------------

                    System.IO.StreamWriter file = new System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~/general-insurance/services/testlog.txt"), true);
                    file.WriteLine(DateTime.Now + ex.StackTrace);
                    file.Close();

                    //-------------------------

                    string filepath1 = "~/log/";
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(filepath1)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filepath1));
                    }

                    string filelog1 = filepath1 + "Notification.txt";
                    filelog1 = HttpContext.Current.Server.MapPath(filelog1);


                    File.AppendAllText(filelog1, Environment.NewLine);
                    File.AppendAllText(filelog1, ex.ToString());
                    File.AppendAllText(filelog1, Environment.NewLine);
                    File.AppendAllText(filelog1, Environment.NewLine);
                    File.AppendAllText(filelog1, DateTime.Now.ToString());
                    File.AppendAllText(filelog1, "***********End Section*********");
                    File.AppendAllText(filelog1, Environment.NewLine);
                }

                break;
        }
    }

    private string SendGCMNotification1(string apiKey, string postData1, string deviceId, string postDataContentType = "application/json")
    {
        //--------------------------

        System.IO.StreamWriter file = new System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~/log/testlog.txt"), true);
        //file.WriteLine(DateTime.Now + ex.StackTrace);
        file.Close();

        //-------------------------

        string filepath1 = "~/log/";
        if (!Directory.Exists(HttpContext.Current.Server.MapPath(filepath1)))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filepath1));
        }

        string filelog1 = filepath1 + "Notification.txt";
        filelog1 = HttpContext.Current.Server.MapPath(filelog1);
        //==================================================================

        File.AppendAllText(filelog1, Environment.NewLine + "Entered function and deviceId =" + deviceId);
        
        // your RegistrationID paste here which is received from GCM server.                                                               
        string regId = deviceId;
        // applicationID means google Api key                                                                                                     
        var applicationID = "AIzaSyAtIq9gFi4j_h3a41VfVntUY6whebQUglw";
        // SENDER_ID is nothing but your ProjectID (from API Console- google code)//                                          
        var SENDER_ID = "145729692457";

        var value = "Visionary Life Science"; //message text box                                                                               

        WebRequest tRequest;

        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");

        tRequest.Method = "post";

        tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";

        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

        tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

        //Data post to server                                                                                                                                         
        string postData =
             "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
              + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" +
                 regId + "";




        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        tRequest.ContentLength = byteArray.Length;

        Stream dataStream = tRequest.GetRequestStream();

        dataStream.Write(byteArray, 0, byteArray.Length);

        dataStream.Close();

        try
        {



            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();   //Get response from GCM server.            
            tReader.Close();

            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;      //Assigning GCM response to Label text 
        }
        catch (Exception ex)
        {
            File.AppendAllText(filelog1, Environment.NewLine);
            File.AppendAllText(filelog1, ex.ToString());
            File.AppendAllText(filelog1, Environment.NewLine);

            File.AppendAllText(filelog1, Environment.NewLine);
            File.AppendAllText(filelog1, DateTime.Now.ToString());
            File.AppendAllText(filelog1, "***********End Section*********");
            File.AppendAllText(filelog1, Environment.NewLine);
            return "error";
        }
    }


    public static bool ValidateServerCertificate(
                                                 object sender,
                                                 X509Certificate certificate,
                                                 X509Chain chain,
                                                 SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
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

                //RegisterId you got from Android Developer.
                string deviceId = Convert.ToString(context.Request.QueryString["regKey"]);
              //  string response = "Done";
                string message = "Demo Notification";
                string tickerText = "Patient Registration";
                string contentTitle = "Trial GCM";
                string postData =
                "{ \"registration_ids\": [ \"" + deviceId + "\" ], " +
                  "\"data\": {\"tickerText\":\"" + tickerText + "\", " +
                             "\"contentTitle\":\"" + contentTitle + "\", " +
                             "\"message\": \"" + message + "\"}}";

                string response = SendGCMNotification("AIzaSyAG25ispDIrDORIW9VYae0Cv5xiMuWK0YU", postData);
                context.Response.ContentType = "text/plain";
                context.Response.Write(response);



                string Query = "INSERT INTO [dbo].[appUser]"
          + "([sFullName] ,[sMobile]      ,[sEmailId]      ,[sPassword]      ,[sGender]      ,[sBirthDate]      ,[sAddress]      ,[sRole]      ,[sDegree]      ,[sSpecialization]      ,[sClinic]      ,[sCountry]      ,[sPincode]      ,[sCity]      ,[sState]) VALUES"
          + "('Ramesh Sharma',	'8898181810',	'ramesh@gmail.com',	'NULL',	'Male',	'NULL','NULL'	,'patient',	'NULL',	'NULL',	'NULL',	'NULL',	'NULL',	'NULL'	,'" + deviceId + "')";


            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            int booklabid = (int)scom.ExecuteScalar();
            scon.Close();
                
                break;
        }
    }

    private string SendGCMNotification(string apiKey, string postData, string postDataContentType = "application/json")
    {
        ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

        //  
        //  MESSAGE CONTENT  
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        //  
        //  CREATE REQUEST  
        HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("http://115.97.9.67:32/service/GCMNotification.ashx");
        Request.Method = "POST";
        //  Request.KeepAlive = false;  

        Request.ContentType = postDataContentType;
        Request.Headers.Add(string.Format("Authorization: key={0}", apiKey));
        Request.ContentLength = byteArray.Length;

        Stream dataStream = Request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        //  
        //  SEND MESSAGE  
        try
        {
            WebResponse Response = Request.GetResponse();

            HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
            if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
            {
                var text = "Unauthorized - need new token";
            }
            else if (!ResponseCode.Equals(HttpStatusCode.OK))
            {
                var text = "Response from web service isn't OK";
            }

            StreamReader Reader = new StreamReader(Response.GetResponseStream());
            string responseLine = Reader.ReadToEnd();
            Reader.Close();

            return responseLine;
        }
        catch (Exception e)
        {
        }
        return "error";
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
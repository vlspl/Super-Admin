using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using DataAccessHandler;
using System.Net;
using System.Configuration;
using Newtonsoft.Json;
using System.Linq;
using CrossPlatformAESEncryption.Helper;
using Newtonsoft.Json.Linq;
//using Twilio;
//using Twilio.Types;
//using Twilio.Rest.Api.V2010.Account;
using RestSharp;

/// <summary>
/// Summary description for WebAPI
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebAPI : System.Web.Services.WebService
{

          
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void GenrateOTP(Int64 Mobile)
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        if (HttpContext.Current.Request.HttpMethod == "Options")
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET,POST,PUT");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type,Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "172000");
            HttpContext.Current.Response.End();
        }

        string resultJSON = "", result = "";
        DataAccessLayer DAL = new DataAccessLayer();
        try
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Random generator = new Random();
            string number = generator.Next(1, 10000).ToString("D4");
          //  string MSG = "" + number + " is your HowzU verification code(OTP).";// hide by ramesh due to hash code updating to send otp code on 17-02-2022
	    
          //  string MSG = "" + number + " is the One Time Password (OTP) for your login to the HOWZU App . Please do not share with anyone. vhvig/j9hsx";
	string MSG = "%3C%23%3E " + number + " is the One Time Password (OTP) for your login to the HOWZU App. Please do not share with anyone. vhvig/j9hsx";
			 
			
            result = "Success";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            HttpWebRequest myReq = (System.Net.HttpWebRequest)WebRequest.Create("https://http.myvfirst.com/smpp/sendsms?username=Visionhtptrns&password=trujd@k34&to=91" + Mobile + "&from=HOWZUX&text=" + MSG + "");
            myReq.Credentials = new System.Net.NetworkCredential("Visionhtptrns", "trujd@k34");
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            
                SqlParameter[] param = new SqlParameter[]
                {
                        new SqlParameter("@Mobile",CryptoHelper.Encrypt(Mobile.ToString())),
                        new SqlParameter("@OTP",number),
                        new SqlParameter("@returnval",SqlDbType.Int)
                };
                int _result = DAL.ExecuteStoredProcedureRetnInt("WS_Sp_UpdateOTP", param);
   		 // sendWhatsappMsg("+91" + Mobile.ToString(), "OTP", "User" + ',' + number);
            resultJSON = "{\"Status\":\"" + responseString + "\",\"OTP\" : \"" + number + "\"}";

        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }
        Context.Response.Write(resultJSON);
        // return resultJSON;
    }

	
	// by ramesh more
	
	 [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void GenrateOTP_googleaccount(Int64 Mobile)
    {
       HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        if (HttpContext.Current.Request.HttpMethod == "Options")
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET,POST,PUT");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type,Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "172000");
            HttpContext.Current.Response.End();
        }

        string resultJSON = "", result = "";
        DataAccessLayer DAL = new DataAccessLayer();
        try
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Random generator = new Random();
            string number = generator.Next(1, 10000).ToString("D4");
          //  string MSG = "" + number + " is your HowzU verification code(OTP).";// hide by ramesh due to hash code updating to send otp code on 17-02-2022
	    
          //  string MSG = "" + number + " is the One Time Password (OTP) for your login to the HOWZU App . Please do not share with anyone. vhvig/j9hsx";
	string MSG = "%3C%23%3E " + number + " is the One Time Password (OTP) for your login to the HOWZU App. Please do not share with anyone. vhvig/j9hsx";
			 
			
            result = "Success";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            HttpWebRequest myReq = (System.Net.HttpWebRequest)WebRequest.Create("https://http.myvfirst.com/smpp/sendsms?username=Visionhtptrns&password=trujd@k34&to=91" + Mobile + "&from=HOWZUX&text=" + MSG + "");
            myReq.Credentials = new System.Net.NetworkCredential("Visionhtptrns", "trujd@k34");
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            
                SqlParameter[] param = new SqlParameter[]
                {
                        new SqlParameter("@Mobile",Mobile.ToString()),
                        new SqlParameter("@OTP",number),
                        new SqlParameter("@returnval",SqlDbType.Int)
                };
                int _result = DAL.ExecuteStoredProcedureRetnInt("WS_Sp_UpdateOTP_signTemp", param);
   		 // sendWhatsappMsg("+91" + Mobile.ToString(), "OTP", "User" + ',' + number);
            resultJSON = "{\"Status\":\"" + responseString + "\",\"OTP\" : \"" + number + "\"}";

        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }
        Context.Response.Write(resultJSON);
    }
	
	
	 [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void GenrateOTP_tempuserReg(Int64 Mobile)
    {
       HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        if (HttpContext.Current.Request.HttpMethod == "Options")
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET,POST,PUT");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type,Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "172000");
            HttpContext.Current.Response.End();
        }

        string resultJSON = "", result = "";
        DataAccessLayer DAL = new DataAccessLayer();
        try
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Random generator = new Random();
            string number = generator.Next(1, 10000).ToString("D4");
          //  string MSG = "" + number + " is your HowzU verification code(OTP).";// hide by ramesh due to hash code updating to send otp code on 17-02-2022
	    
          //  string MSG = "" + number + " is the One Time Password (OTP) for your login to the HOWZU App . Please do not share with anyone. vhvig/j9hsx";
	string MSG = "%3C%23%3E " + number + " is the One Time Password (OTP) for your login to the HOWZU App. Please do not share with anyone. vhvig/j9hsx";
			 
			
            result = "Success";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            HttpWebRequest myReq = (System.Net.HttpWebRequest)WebRequest.Create("https://http.myvfirst.com/smpp/sendsms?username=Visionhtptrns&password=trujd@k34&to=91" + Mobile + "&from=HOWZUX&text=" + MSG + "");
            myReq.Credentials = new System.Net.NetworkCredential("Visionhtptrns", "trujd@k34");
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            
                SqlParameter[] param = new SqlParameter[]
                {
                        new SqlParameter("@Mobile",Mobile.ToString()),
                        new SqlParameter("@OTP",number),
                        new SqlParameter("@returnval",SqlDbType.Int)
                };
                int _result = DAL.ExecuteStoredProcedureRetnInt("WS_Sp_UpdateOTP_userReg", param);
   		 // sendWhatsappMsg("+91" + Mobile.ToString(), "OTP", "User" + ',' + number);
            resultJSON = "{\"Status\":\"" + responseString + "\",\"OTP\" : \"" + number + "\"}";

        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }
        Context.Response.Write(resultJSON);
    }
	
	
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void RegisterFamilyMemberOTP(Int64 Mobile, string OTP)
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        if (HttpContext.Current.Request.HttpMethod == "Options")
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET,POST,PUT");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type,Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "172000");
            HttpContext.Current.Response.End();
        }

        string resultJSON = "", result = "";
        DataAccessLayer DAL = new DataAccessLayer();
        try
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
           // string MSG = "" + OTP + " is your HowzU verification code(OTP).";
			
			//OTP=""+OTP;
 //string MSG = ""+ OTP + " is the One Time Password (OTP) for your login to the HOWZU App . Please do not share with anyone. vhvig/j9hsx";
			
string MSG ="%3C%23%3E "+ OTP + " is the One Time Password (OTP) for your login to the HOWZU App. Please do not share with anyone. vhvig/j9hsx";
			
	      result = "Success";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            HttpWebRequest myReq = (System.Net.HttpWebRequest)WebRequest.Create("https://http.myvfirst.com/smpp/sendsms?username=Visionhtptrns&password=trujd@k34&to=91" + Mobile + "&from=HOWZUX&text=" + MSG + "");
            myReq.Credentials = new System.Net.NetworkCredential("Visionhtptrns", "trujd@k34");
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
		//sendWhatsappMsg("+91" + Mobile.ToString(), "OTP", "User" + ',' + OTP);
            resultJSON = "{\"Status\":\"" + responseString + "\",\"OTP\" : \"" + OTP + "\"}";

        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }
        Context.Response.Write(resultJSON);
        // return resultJSON;
    }

	[WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void invitation_sms_to_doctor(Int64 Mobile, string msg)
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        if (HttpContext.Current.Request.HttpMethod == "Options")
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET,POST,PUT");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type,Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "172000");
            HttpContext.Current.Response.End();
        }

        string resultJSON = "", result = "";
        DataAccessLayer DAL = new DataAccessLayer();
        try
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string MSG = msg;
            result = "Success";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            HttpWebRequest myReq = (System.Net.HttpWebRequest)WebRequest.Create("https://http.myvfirst.com/smpp/sendsms?username=Visionhtptrns&password=trujd@k34&to=91" + Mobile + "&from=HOWZUX&text=" + MSG + "");
            myReq.Credentials = new System.Net.NetworkCredential("Visionhtptrns", "trujd@k34");
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
         // sendWhatsappMsg("+91" + Mobile.ToString(), "OTP", "User" + ',' + OTP);
            resultJSON = "{\"Status\":\"" + responseString + "\",\"OTP\" : \"" + msg + "\"}";

        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }
        Context.Response.Write(resultJSON);
        // return resultJSON;
    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void invitation_sms_to_patient(Int64 Mobile, string dName)
    {
        string docName = dName.Split(',')[0].ToString();
        string patientName = dName.Split(',')[1].ToString();
        string msg = "Hey " + patientName + ", " + docName + " has recommended you HowzU app. HowzU is a best-in-class, state-of-the-art, most advanced digital health valet with unlimited features to keep all your health at your fingertips. To explore more for free kindly download the app link at the earliest. iPhone users: https://apps.apple.com/in/app/howzu/id1481816983 Android users: https://play.google.com/store/apps/details?id=com.howzu Team HowzU.";
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        if (HttpContext.Current.Request.HttpMethod == "Options")
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET,POST,PUT");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type,Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "172000");
            HttpContext.Current.Response.End();
        }

        string resultJSON = "", result = "";
        
        DataAccessLayer DAL = new DataAccessLayer();
        try
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            string MSG = msg;
            result = "Success";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            HttpWebRequest myReq = (System.Net.HttpWebRequest)WebRequest.Create("https://http.myvfirst.com/smpp/sendsms?username=Visionhtptrns&password=trujd@k34&to=91" + Mobile + "&from=HOWZUX&text=" + MSG + "");
            myReq.Credentials = new System.Net.NetworkCredential("Visionhtptrns", "trujd@k34");
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
//sendWhatsappMsg("+91" + Mobile.ToString(), "OTP", "User" + ',' + OTP);
            resultJSON = "{\"Status\":\"" + responseString + "\",\"Msg\" : \"" + msg + "\"}";

        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }
        Context.Response.Write(resultJSON);
        // return resultJSON;
    }

   
	

 

}



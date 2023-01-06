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
//using Newtonsoft.Json;
using System.Linq;
//using CrossPlatformAESEncryption.Helper;
//using Newtonsoft.Json.Linq;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

/// <summary>
/// Summary description for WebAPI
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Twilio_WebAPI : System.Web.Services.WebService
{

  
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void SendOTPViaWhatsapp(string Mobile,int OTP)
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

              const string accountSid = "AC33cdc44a09dbe7fc807ccf3bb48565dc";
                const string authToken = "70d189ea498c618137ef78b2df81fb36";
                TwilioClient.Init(accountSid, authToken);
                 var messageOptions = new CreateMessageOptions(
              new PhoneNumber(Mobile));
                messageOptions.From = new PhoneNumber("+14155238886");
              messageOptions.Body = "Hi , Your OTP is "+ OTP + ".Please Don't share it with anyone.";

                  var message = MessageResource.Create(messageOptions);
                
			
            result = "Success";
			
          
            resultJSON = "{\"Status\":\"" + result + "\",\"OTP\" : \"" + OTP + "\"}";

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
    public void BookAppointment(string Mobile)
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

              const string accountSid = "AC33cdc44a09dbe7fc807ccf3bb48565dc";
                const string authToken = "70d189ea498c618137ef78b2df81fb36";
                TwilioClient.Init(accountSid, authToken);
                 var messageOptions = new CreateMessageOptions(
              new PhoneNumber(Mobile));
                messageOptions.From = new PhoneNumber("+14155238886");
              messageOptions.Body = "Your appointment is coming up on July 21 at 3PM";

                  var message = MessageResource.Create(messageOptions);
                
			
            result = "Success";
			
          
            resultJSON = "{\"Status\":\"" + result + "\"}";

        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }
        Context.Response.Write(resultJSON);
        // return resultJSON;
    }
}



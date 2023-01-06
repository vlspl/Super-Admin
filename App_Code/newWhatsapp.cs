using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Net;

/// <summary>
/// Summary description for newWhatsapp
/// </summary>
public class newWhatsapp
{
    DBClass db = new DBClass();
    public newWhatsapp()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    string _getSendWhatsappMsgAPIFrmWebConfig = "";
    public string sendWhatsappMsg(string mobNo, string msgName, string parameters, string labId)
    {
        int firstDigits = int.Parse(mobNo.Substring(3, 1));
        if (firstDigits > 5)
        {
            string JSONString = string.Empty; // Create string object to return final output
            dynamic Result = new JObject();  //Create root JSON Object
            string Msg = "";

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            _getSendWhatsappMsgAPIFrmWebConfig = WebConfigurationManager.AppSettings["sendWhatsappMsg"];


            var client = new RestClient(_getSendWhatsappMsgAPIFrmWebConfig);



            // var client = new RestClient("https://twebapi.visionarylifescience.com/sendWatsappMsgModel/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{" + "\n" +
             @"  ""mobNo"": ""@mobNo""," + "\n" +
             @"  ""msgName"": ""@msgName""," + "\n" +
             @"  ""parameters"": ""@param""" + "\n" +

             @"}";

            body = body.Replace("@mobNo", mobNo);
            body = body.Replace("@msgName", msgName);
            body = body.Replace("@param", parameters);

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            // Console.WriteLine(response.Content);

            db.insert("insert into tbl_msgStatus(mobileNo,msgName,msgBody,parameters,date,status,sLabId) values('" + mobNo + "','" + msgName + "','" + body + "','" + parameters + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "','" + response.StatusCode + "','"+labId+"')");
            return response.Content;
           
           
        }
        else
        {
            return "0";
        }
        
    }

    public string sendWhatsappMsg_superadmin(string mobNo, string msgName, string parameters)
    {
        int firstDigits = int.Parse(mobNo.Substring(3, 1));
        if (firstDigits > 5)
        {
            string JSONString = string.Empty; // Create string object to return final output
            dynamic Result = new JObject();  //Create root JSON Object
            string Msg = "";

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            _getSendWhatsappMsgAPIFrmWebConfig = WebConfigurationManager.AppSettings["sendWhatsappMsg"];


            var client = new RestClient(_getSendWhatsappMsgAPIFrmWebConfig);



            // var client = new RestClient("https://twebapi.visionarylifescience.com/sendWatsappMsgModel/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{" + "\n" +
             @"  ""mobNo"": ""@mobNo""," + "\n" +
             @"  ""msgName"": ""@msgName""," + "\n" +
             @"  ""parameters"": ""@param""" + "\n" +

             @"}";

            body = body.Replace("@mobNo", mobNo);
            body = body.Replace("@msgName", msgName);
            body = body.Replace("@param", parameters);

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            // Console.WriteLine(response.Content);

             return response.Content;


        }
        else
        {
            return "0";
        }

    }

}
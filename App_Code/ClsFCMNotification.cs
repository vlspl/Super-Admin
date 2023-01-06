using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using System.IO;
using Newtonsoft.Json;
/// <summary>
/// Summary description for ClsFCMNotification
/// </summary>
public class ClsFCMNotification
{
	public ClsFCMNotification()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool Successful
    {
        get;
        set;
    }
    public string Response
    {
        get;
        set;
    }
    public Exception Error
    {
        get;
        set;
    }
    public ClsFCMNotification SendNotification(string _title, string _message, string _topic, string Type, string Id)
    {
        ClsFCMNotification result = new ClsFCMNotification();
        try
        {
            result.Successful = true;
            result.Error = null;
            // var value = message;
            var requestUri = "https://fcm.googleapis.com/fcm/send";

            WebRequest webRequest = WebRequest.Create(requestUri);
            webRequest.Method = "POST";
            webRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAdsxbSYs:APA91bFDLpDKhZEKA8fCzB1d9hnlVXWv0lc7mr6xuH4qfV1jklMuRAt9z86TdhOJN1iahJe23bhzsGP1EErdmXQlpkKIO_e5FHajdBWF7_fNcmC6Z4q880uatquRZEg-nOhRQiOfiaDW"));
            webRequest.Headers.Add(string.Format("Sender: id={0}", "510234675595"));
            webRequest.ContentType = "application/json";

            var payload = new
            {
                to = _topic, // Uncoment this if you want to test for single device
                //  to = "/topics/" + _topic, // this is for topic 
                notification = new
                {
                    title = _title,
                    body = _message,
                 icon = "R.drawable.logoz",   //https://www.visionarylifescience.com/Images/icons/check.png
                   // image = "https://www.visionarylifescience.com/Images/temperature.png",
                   // click_action = "com.med.visionarylsci.lifescienes.hzu.vlsmarchup.activity.NotificationActivity",
                    foreground = true
                },
                data = new
                {
                    Type = Type,
                    ID = Id,
                    delivered_priority = "high",
                    collapse_key = "com.howzu",
                    priority = "high",
                    remote = true
                },
            };

            var json = JsonConvert.SerializeObject(payload);

            Byte[] byteArray = Encoding.UTF8.GetBytes(json);

            webRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = webResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();
                            result.Response = sResponseFromServer;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            result.Successful = false;
            result.Response = null;
            result.Error = ex;
        }
        return result;
    }

    public ClsFCMNotification SendNotification_superAdmin(string _title, string _message, string imgurl, string _topic, string Type, string Id)
    {
        ClsFCMNotification result = new ClsFCMNotification();
        try
        {
            result.Successful = true;
            result.Error = null;
            // var value = message;
            var requestUri = "https://fcm.googleapis.com/fcm/send";

            WebRequest webRequest = WebRequest.Create(requestUri);
            webRequest.Method = "POST";
            webRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAdsxbSYs:APA91bFDLpDKhZEKA8fCzB1d9hnlVXWv0lc7mr6xuH4qfV1jklMuRAt9z86TdhOJN1iahJe23bhzsGP1EErdmXQlpkKIO_e5FHajdBWF7_fNcmC6Z4q880uatquRZEg-nOhRQiOfiaDW"));
            webRequest.Headers.Add(string.Format("Sender: id={0}", "510234675595"));
            webRequest.ContentType = "application/json";

            var payload = new
            {
                to = _topic, // Uncoment this if you want to test for single device
                //  to = "/topics/" + _topic, // this is for topic 
                notification = new
                {
                    title = _title,
                    
                    body = _message,
                  //  icon = "R.drawable.logoz",
                   // iconurl = "https://www.visionarylifescience.com/Images/icons/check.png",   //https://www.visionarylifescience.com/Images/icons/check.png
                   // image = "https://www.visionarylifescience.com/Images/temperature.png",
                   image=imgurl,
                   // icon = "https://www.visionarylifescience.com/Images/temperature.png",
                    color = "Green",
                    // click_action = "com.med.visionarylsci.lifescienes.hzu.vlsmarchup.activity.NotificationActivity",
                    foreground = true
                },
               
                data = new
                {
                    
                    Type = Type,
                    ID = Id,
                    delivered_priority = "high",
                    collapse_key = "com.howzu",
                    priority = "high",
                    color = "Green",
                    remote = true
                },
            };

            var json = JsonConvert.SerializeObject(payload);

            Byte[] byteArray = Encoding.UTF8.GetBytes(json);

            webRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = webResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();
                            result.Response = sResponseFromServer;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            result.Successful = false;
            result.Response = null;
            result.Error = ex;
        }
        return result;
    }
}
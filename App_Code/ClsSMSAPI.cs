using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

/// <summary>
/// Summary description for ClsSMSAPI
/// </summary>
public class ClsSMSAPI
{
	public ClsSMSAPI()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string SMSAPI(string Mobile, string MSG)
    {
        string Result = "";
        try
        {
            HttpWebRequest myReq = (System.Net.HttpWebRequest)WebRequest.Create("http://myvaluefirst.com/smpp/sendsms?username=Visionhtptrns&pwd=trujd@k34&to=91" + Mobile + "&from=VFirst&udh=0&text=" + MSG + "&dlr-mask=50&dlr-url=https://visionarylifescience.com/mobileapp/WebAPI/WebAPI.asmx/GetStatus?dlrv=%d");
            myReq.Credentials = new System.Net.NetworkCredential("Visionhtptrns", "trujd@k34");
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            Result = respStreamReader.ReadToEnd();
        }
        catch (Exception ex)
        {
            return Result = "Error Occured";
        }
        return Result;
    }
}
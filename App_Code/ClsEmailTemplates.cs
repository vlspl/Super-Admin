using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

/// <summary>
/// Summary description for ClsEmailTemplates
/// 
/// </summary>
public class ClsEmailTemplates
{
    public string sendmail(string emailId, string password, string Name, string Username)
    {
        try
            
        {
            MailMessage MailMsg = new MailMessage();
            MailMsg.From = new MailAddress("visionarylifesciences7@gmail.com");
            MailMsg.To.Add(emailId);
            MailMsg.Subject = "Welcome to HowzU - " + Name;

            MailMsg.Body = " <div style='padding: 18px; font-family: verdana; font-size: small; background-color: #eaf7ec;text-align: center'>" +
                            "<img src='https://visionarylifescience.com/images/Howzulogo1092020101600.png' height='57px' width: 254px; class='img-thumbnail' /><h4>" +
                            "Dear " + Name + "</h4><h3>Congratulations!!!</h3>" +
                            "<span style='font-weight: bold'>You've successfully signed up for HowzU!<br /><br />" +
                            "</span>Please find your login details below.<br />" +
                            "<span>Your UserName is:<h4 style='font-weight: bold'>" + Username + "</h4></span>" +
                            "<span>Your Password is:<h4 style='font-weight: bold'>" + password + "</h4></span>" +
                            //"<p><b> for iOS Click here: </b><a href='http://onelink.to/ecfka9'>To Download HowzU App.</a></p>" +
                             "<p><b> for iOS User Click here: </b><a href='https://apps.apple.com/in/app/howzu/id1481816983'>To Download HowzU App.</a></p>" + // by ramesh more
                              "<p><b> for Android User Click here: </b><a href='https://play.google.com/store/apps/details?id=com.howzu'>To Download HowzU App.</a></p>" + // ramesh more
                            "Regards Team,<br />" +
                            "Visionary Life Science Pvt. Ltd." +
                            "</div>";

            MailMsg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "relay-hosting.secureserver.net";
            client.Port = 25;

            //Setup credentials to login to our sender email address ("UserName", "Password")
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("visionarylifesciences7@gmail.com", "vls1234$");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            client.Send(MailMsg);
            return "1";
        }
        catch (Exception ex)
        {
            return "0";
        }
    }

    public string sendmailForTechnicianCredential(string emailId, string password, string Name, string Username,string orgName,string branchName,string campName,string ownerName,string labName)
    {
        try
        {
            MailMessage MailMsg = new MailMessage();
            MailMsg.From = new MailAddress("visionarylifesciences7@gmail.com");
            MailMsg.To.Add(emailId);
            MailMsg.Subject = "Login Credential of Technician " + Name;

            MailMsg.Body = " <div style='padding: 18px; font-family: verdana; font-size: small; background-color: #eaf7ec;text-align: center'>" +
                            "<img src='https://visionarylifescience.com/images/Howzulogo1092020101600.png' height='57px' width: 254px; class='img-thumbnail' /><h4>" +
                            "Dear Support Team </h4><h3>Congratulations!!!</h3>" +
                            "<span style='font-weight: bold'>You've successfully created technician for Organization Health Camp!<br /><br />" +
                              "</span>Please find Health Camp details below.<br />" +
                            "<table>"+
                                "<td><tr><span>Technician Name:<h4 style='font-weight: bold'>" + Name + "</h4></span></td></tr>" +
                            "<td><tr><span>Organization Name:<h4 style='font-weight: bold'>" + orgName + "</h4></span></td></tr>" +
                            "<td><tr><span>Branch Name:<h4 style='font-weight: bold'>" + branchName + "</h4></span></td></tr>" +
                            "<td><tr><span>Health Camp Name:<h4 style='font-weight: bold'>" + campName + "</h4></span></td></tr>" +
                            "<td><tr><span>Owner Name:<h4 style='font-weight: bold'>" + ownerName + "</h4></span></td></tr>" +
                            "<td><tr><span>Lab Name Name:<h4 style='font-weight: bold'>" + labName + "</h4></span></td></tr>" +
                            "</table>"+

                            "</span>Please find Technician login details below.<br />" +
                            "<span>Your UserName is:<h4 style='font-weight: bold'>" + Username + "</h4></span>" +
                            "<span>Your Password is:<h4 style='font-weight: bold'>" + password + "</h4></span>" +
                           
                            "Regards Team,<br />" +
                            "Visionary Life Science Pvt. Ltd." +
                            "</div>";

            MailMsg.IsBodyHtml = true;
            SmtpClient smtpclient = new SmtpClient("relay-hosting.secureserver.net", 25);
            smtpclient.UseDefaultCredentials = false;
            smtpclient.Credentials = new System.Net.NetworkCredential("visionarylifesciences7@gmail.com", "vls1234$");
            smtpclient.EnableSsl = true;

            smtpclient.Send(MailMsg);
            return "1";

          
          
        }
        catch (Exception ex)
        {
          
         ex.Message.ToString();
         return "0";
        }
    }

}
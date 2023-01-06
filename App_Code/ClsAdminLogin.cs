using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using DataAccessHandler;

public class ClsAdminLogin
{
    DataAccessLayer DAL = new DataAccessLayer();
    Connection conns = new Connection();
    MD5Hash objHash = new MD5Hash();
    public ClsAdminLogin()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataSet AdminLogin(string user, string pass)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@UserName",user),
            new SqlParameter("@Password",pass)
        };
        ds = DAL.ExecuteStoredProcedureDataSet("sp_LoginMaster", param);
        return ds;
    }
    public DataTable AdminUserDetails(string UserId)
    {
        DataTable ds = new DataTable();
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@UserId",UserId)
        };
        ds = DAL.ExecuteStoredProcedureDataTable("sp_GetAdminUserDetails", param);
        return ds;
    }
    public DataSet getDashData()
    {
        DataSet ds = new DataSet();
        ds = DAL.GetDataSet("Sp_getDashData");
        return ds;
    }
    public void UpdateStatus(string id, string status)
    {
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@labname",id),
            new SqlParameter("@status",status)
        };
        DAL.ExecuteStoredProcedure("Sp_UpdateLabStatus", param);
    }
    public string mailPassword(string userName)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserName",userName)
            };
            DataTable dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetAdminEmailandPassword", param);
            if (dt.Rows.Count > 0)
            {
                string emailId = objHash.decrypt(dt.Rows[0]["email"].ToString());
                string password = objHash.decrypt(dt.Rows[0]["password"].ToString());
                string UserName = dt.Rows[0]["Name"].ToString();
                string mailSent = sendmail(emailId, password, UserName);

                if (mailSent == "1")
                {
                    //if mail sent return 1
                    return "1";
                }
                else
                {
                    return "-1";
                }
            }
            else
            {
                //if username not found
                return "0";
            }

        }
        catch (Exception)
        {
            return "-1";
        }
    }
    public string sendmail(string emailId, string password, string Name)
    {
        try
        {
            MailMessage MailMsg = new MailMessage();
            MailMsg.From = new MailAddress("visionarylifesciences7@gmail.com");
            MailMsg.To.Add(emailId);
            MailMsg.Subject = "Password";

            MailMsg.Body = "<div style='padding:18px; font-family:verdana; font-size:small;  background-color:#eaf7ec;text-align:center '><img src='https://visionarylifescience.com/images/Howzulogo1092020101600.png' height='57px' width: 254px; class='img-thumbnail' /><h4>Dear " + Name + "</h4><br /><h3> Greetings!!!</h3> <br /> From <span style=' font-weight:bold'>VISIONARY LIFE SCIENCES PVT.LTD   .<br />" +
            " <br />" + " </span>Your Email Id is:<h3 style=' color: #fff; font-weight:bold'>" + emailId + "</h3>" + " <br />" + " <span>Your Password Is:<h3 style='  font-weight:bold'>" + password + "</h3></span>" +
             " <br />" +
                " Regards Team," +
                 "<br />" +
                 "Visionary Life Science Pvt.Ltd" +
                " </div>";
            MailMsg.IsBodyHtml = true;
            SmtpClient smtpclient = new SmtpClient("smtp.gmail.com", 587);
            smtpclient.UseDefaultCredentials = true;
            smtpclient.Credentials = new System.Net.NetworkCredential("visionarylifesciences7@gmail.com", "vls1234$");
            smtpclient.EnableSsl = true;

            smtpclient.Send(MailMsg);
            return "1";
        }
        catch (Exception ex)
        {
            return "0";
        }
    }

}
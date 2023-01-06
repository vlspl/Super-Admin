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
        ds = DAL.ExecuteStoredProcedureDataSet("Sp_GetAdminDetails", param);
        return ds;
    }
    public DataSet getDashData()
    {
        DataSet ds = new DataSet();
        ds = DAL.GetDataSet("Sp_getDashData");
        return ds;
    }
    public DataSet UpdateStatus(string Name, string status)
    {
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@labname",Name),
            new SqlParameter("@status",status)
        };
        DAL.ExecuteStoredProcedure("Sp_UpdateLabStatus", param);
        return ds;
    }
    public string mailPassword(string userName)
    {
        try
        {            DataTable dt = DAL.GetDataTable("Sp_GetAdminEmailandPassword " + userName);
            if (dt.Rows.Count > 0)
            {
                string emailId = dt.Rows[0]["sColA"].ToString();
                // string password = objHash.decrypt(dr["sPassword"].ToString());
                string password = dt.Rows[0]["sPassword"].ToString();
                string mailSent = sendMail(emailId, password);
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
        catch (Exception ex)
        {
            return "-1";
        }
    }
    public string sendMail(string emailId, string password)
    {
        try
        {
            MailMessage mail = new MailMessage();

            mail.To.Add(emailId);
            mail.From = new MailAddress("irealities.qa@gmail.com");
            mail.Subject = "Password";
            string Body = "Password : " + password;
            mail.Body = Body;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("irealities.qa@gmail.com", "Qatest2707@");

            //Or your Smtp Email ID and Password
            smtp.EnableSsl = true;
            smtp.Send(mail);
            return "1";
        }
        catch (Exception e)
        {
            return "0";
            //throw new Exception(e.Message);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json.Linq;

public partial class FundTransferSuccess : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    ClsBookDetails objBookDetails = new ClsBookDetails();
    ClsFCMNotification ObjFCM = new ClsFCMNotification();
    ClsAppNotification objAppNotify = new ClsAppNotification();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                NameValueCollection nvc = Request.Form;

                string output = nvc.ToString();
                string[] array = output.Split('&');
                string plaintext = array[1].Split('=')[1];
                byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                int iterations = 65536;
                int keysize = 256;
               // string offone = "EEBC59EE60DE484FB92C4A8BB4DF9185C2165B1439FD2D868BBEAACAC416436742C0B1623E8845669FDC72AFF79B1DF685DCF3D3BB82C8C9CAD4CC96C5A2878163E5CF1E356CE6AD23F88C0CC035B09E322036E310099177C9F425D5D8FAC5132A1CBD71C7BFEA941BE60B885DCB089C67F830A173BDD2EBF6AEB6BCA5E09FCFF9A5E30E1F02ADDC64838250B2B4D68C68AFC3957A2CABDFB3EE35033BFBA4DA10377DB8E48D4692BDF45873B788792F229F7E83905C8281B72005661255058B5CA9D5007EA22CE327F715EC8E5FDC143677821F1CB57054E9AD15A41D6B7D49FEF0E0A49C6BA0B3B0EAF98A78E9E8772DE998D57C14E142F19C62E993C6DA7FB3C737C9D554137C9049FD275AA663C3B865C9FDC065615824C5DE793431F13A2703856F5F0BA56096694CDC6BBB56726C9A17BBF4F0A015E27A40AF264F3C7869F4C18D3AE8A63C245902F062D07ABCD32829B0097F965CD7BD64419BE7F9DC09DA12E0CC03668F76BAF4B29292308FA326912E3A33C9034C57E63E59B472AA0911D10D8CBB05DB3925D7E309F2BB9272C2F65193F9871AE9B380DC8ACB9F53A214029B3EC991B070E6C96F60645A0BE1CC9A80EB489FE6FF9C06C8E0D61C2CEA3F8D2B88678B0A610023BA298EA4C063F9443C5BDE2F80592F7F1B8B1FEC2AA8D360848F75D65D782EB5B57CFAE62D051A762A1DBE387BA52436E75D0A42D804743C0E03EB7540040578C0DD69F8889E9FA60800797EF8783A056ADBA219E972FEDED67DD0DF57EAE037B38444BBDA";
                AtomPaymentASE ObjAtomASE = new AtomPaymentASE();
                string passphrase = "3EDA42A41E227664B1D7E8BC2D3CBD1C";// issue in decript time
                string salt = "3EDA42A41E227664B1D7E8BC2D3CBD1C";
               // string MultEnc = ObjAtomASE.decrypt(plaintext, passphrase, salt, iv, iterations);
                string MultEnc = ObjAtomASE.decrypt(plaintext, passphrase, salt, iv, iterations);
                string [] ResArray = MultEnc.Split('&');
                Dictionary<string, string> returnValues = new Dictionary<string, string>();
                for (int i = 0; i < ResArray.Length-1; i++)
                {
                    string key1 = ResArray[i].Split('=')[0];
                    string key2 = ResArray[i].Split('=')[1];
                    returnValues.Add(key1, key2);
                }
                string postingmmp_txn = returnValues["mmp_txn"];
                string postingmer_txn = returnValues["mer_txn"];
                string postinamount = returnValues["amt"];
                string postingprod = returnValues["prod"];
                string postingdate = returnValues["date"];
                string postingbank_txn = returnValues["bank_txn"];
                string postingf_code = returnValues["f_code"];
                string postingbank_name = returnValues["bank_name"];
                string signature = returnValues["signature"];
                string postingdiscriminator = returnValues["discriminator"];
                string respHashKey = "d253d42297341a7864";
                string ressignature = "";
                string strsignature = postingmmp_txn + postingmer_txn + postingf_code + postingprod + postingdiscriminator + postinamount + postingbank_txn;
                byte[] bytes = Encoding.UTF8.GetBytes(respHashKey);
                byte[] b = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
                ressignature = byteToHexString(b).ToLower();
                
                if (signature == ressignature)
                {
                    if (postingf_code.ToLower() == "ok")
                    {
                        SqlParameter[] param = new SqlParameter[]
                        {
                            new SqlParameter("@Booking_Id",postingmer_txn),
                            new SqlParameter("@AtomTxn_Id",postingmmp_txn),
                            new SqlParameter("@BankTxn_Id",postingbank_txn),
                            new SqlParameter("@TxnProcess","Completed"),
                            new SqlParameter("@F_Code",postingf_code),
                            new SqlParameter("@PaymentStatus","Paid"),
                            new SqlParameter("@returnval",SqlDbType.Int)
                        };
                        int result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateOnlinePaymentDetails", param);
                        result = objBookDetails.AddPaymentDetails(postingmer_txn, postinamount, "Online", "");
                        SqlParameter[] param1 = new SqlParameter[]
                             {
                                 new SqlParameter("@bookLabId",postingmer_txn)
                             };
                        DataTable dt = DAL.ExecuteStoredProcedureDataTable("Sp_GetBookTestDetailsUserMobIdUpdated", param1);
                        if (dt.Rows.Count > 0)
                        {
                            dynamic _Result = new JObject();
                            _Result.BookingId = postingmer_txn;
                            string _payload = JsonConvert.SerializeObject(_Result);
                            string _LabName = dt.Rows[0]["slabname"].ToString();
                            string _Devicetoken = dt.Rows[0]["sDeviceToken"].ToString();
                            string Message = "Your Appointment request is submited at " + _LabName + ". Once Lab confirms we will notify you.";
                            ObjFCM.SendNotification("Test Booking Status", Message, _Devicetoken, "Booking", postingmer_txn.ToString());
                            int _result = objAppNotify.AppNotification(dt.Rows[0]["sappuserid"].ToString(), "Booking", Message, "Booking", _payload, dt.Rows[0]["sappuserid"].ToString());
                        }
                        Response.Redirect("AtomPaymentSuccess.aspx", false);
                    }
                    else
                    {
                        SqlParameter[] param = new SqlParameter[]
                        {
                            new SqlParameter("@Booking_Id",postingmer_txn),
                            new SqlParameter("@AtomTxn_Id",postingmmp_txn),
                            new SqlParameter("@BankTxn_Id",postingbank_txn),
                            new SqlParameter("@TxnProcess","Completed"),
                            new SqlParameter("@F_Code",postingf_code),
                            new SqlParameter("@PaymentStatus","Failed"),
                            new SqlParameter("@returnval",SqlDbType.Int)
                        };
                        int result = DAL.ExecuteStoredProcedureRetnInt("Sp_UpdateOnlinePaymentDetailstoFailed", param);
                        Response.Redirect("AtomPaymentFail.aspx", false);
                    }
                    //Label1.Text = postingf_code;
                }
                else
                {
                    Response.Redirect("AtomPaymentFail.aspx", false);
                    //Label1.Text = postingf_code;
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "reloadPage", "alert('Error occurred');location.reload(true);", true);
        }
    }

    public static string byteToHexString(byte[] byData)
    {
        StringBuilder sb = new StringBuilder((byData.Length * 2));
        for (int i = 0; (i < byData.Length); i++)
        {
            int v = (byData[i] & 255);
            if ((v < 16))
            {
                sb.Append('0');
            }

            sb.Append(v.ToString("X"));

        }

        return sb.ToString();
    }
}
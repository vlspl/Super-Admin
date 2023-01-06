using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Text;
using System.Net;
using System.Configuration;
using System.Data;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Globalization;

public partial class AtomPayment : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string BookingId = Request.QueryString["Id"].ToString();
            DataTable dt = DAL.GetDataTable("Sp_GetAmountandLabDetails " + BookingId);
            if (dt.Rows.Count > 0)
            {
                DataTable dtt = DAL.GetDataTable("Sp_GetLabDetailsforOnlinePayment " + dt.Rows[0]["LabId"].ToString());
                if (dtt.Rows.Count > 0)
                {
                    string AtomProductIdVLS = dtt.Rows[0]["AtomProductId"].ToString();
                    string AtomProductIdLab = dtt.Rows[1]["AtomProductId"].ToString();
                    string PaymentPercentage = dtt.Rows[1]["PaymentPercentage"].ToString();
                    string VlsLabId = dtt.Rows[0]["sLabId"].ToString();
                    string LabId = dtt.Rows[1]["sLabId"].ToString();

                    double _totalAmt = Convert.ToDouble(dt.Rows[0]["Amount"].ToString());
                    double _labPercentage = Convert.ToDouble(dtt.Rows[1]["PaymentPercentage"].ToString());
                    double result = CalulateAmount(_totalAmt, _labPercentage);
                    double _vlsLabAmt = _totalAmt - result;
                    TransferFund(BookingId, _totalAmt.ToString(), AtomProductIdVLS, AtomProductIdLab, _vlsLabAmt.ToString(), result.ToString(), VlsLabId, LabId);
                }
            }
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hideModal", "alert('" + ex.Message + "');", true);
        }
    }



    public static void TransferFund(string TrxId, string TrxAmt, string AtomProductIdVLS, string AtomProductIdLab, string _vlsLabAmt, string labAmt, string VlsLabId, string LabId)
    {
        try
        {
            DataAccessLayer DAL = new DataAccessLayer();
            string strURL, strClientCode, strClientCodeEncoded;
            byte[] b;
            string strResponse = "";
            string MerchantLogin = "119989";
            string MerchantPass = "214eec2b";
            string TransactionType = "NBFundTransfer";
            string ProductID = "Multi";
            string TransactionID = TrxId;
            string TransactionAmount = TrxAmt;
            string TransactionCurrency = "INR";
            string BankID = "2001";
            string ClientCode = "MDAx";
            string TransactionServiceCharge = "0";
            string TransactionDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            string CustomerAccountNo = "50200022905981";
            string MerchantDiscretionaryData = "NB";
            string ProdIdOne = "1";
            string ProdIdOneName = AtomProductIdVLS;
            string ProdIdOneAmt = _vlsLabAmt;
            string ProdIdtwo = "2";
            string ProdIdtwoName = AtomProductIdLab;
            string ProdIdtwoAmt = labAmt;

            for (int i = 1; i <= 2; i++)
            {
                if (i == 1)
                {
                    SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@BookingId",TrxId),
                        new SqlParameter("@LabId",VlsLabId),
                        new SqlParameter("@AtomProductId",AtomProductIdVLS),
                        new SqlParameter("@TransferAmt",_vlsLabAmt),
                        new SqlParameter("@Txn_Date",TransactionDateTime),
                        new SqlParameter("@ReturnVal",SqlDbType.Int)
                    };
                    int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOnlinePaymentDetails", param);
                }
                else
                {
                    SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@BookingId",TrxId),
                        new SqlParameter("@LabId",LabId),
                        new SqlParameter("@AtomProductId",AtomProductIdLab),
                        new SqlParameter("@TransferAmt",labAmt),
                        new SqlParameter("@Txn_Date",TransactionDateTime),
                        new SqlParameter("@ReturnVal",SqlDbType.Int)
                    };
                    int result = DAL.ExecuteStoredProcedureRetnInt("Sp_AddOnlinePaymentDetails", param);
                }
            }
            string ru = "https://www.visionarylifescience.com/AtomPaymentResponce.aspx";

            b = Encoding.UTF8.GetBytes(ClientCode);
            strClientCode = Convert.ToBase64String(b);
            strClientCodeEncoded = HttpUtility.UrlEncode(strClientCode);

            string reqHashKey = "8ef076966699a33072";
            string signature = "";
            string strsignature = MerchantLogin + MerchantPass + TransactionType + ProductID + TransactionID + TransactionAmount + TransactionCurrency;
            byte[] bytes = Encoding.UTF8.GetBytes(reqHashKey);
            byte[] bt = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
            // byte[] b = new HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(prodid));
            signature = byteToHexString(bt).ToLower();

            string plaintext = "login=" + MerchantLogin + "&pass=" + MerchantPass + "&ttype=" + TransactionType + "&amt=" + TransactionAmount + "&txnid=" + TransactionID + "&prodid=" + ProductID + "&txncurr=" + TransactionCurrency + "&clientcode=" + ClientCode + "&date=" + TransactionDateTime + "&txnscamt=" + TransactionServiceCharge + "&custacc=" + CustomerAccountNo + "&ru=" + ru + "&signature=" + signature + "&mprod=<products><product><id>" + ProdIdOne + "</id><name>" + ProdIdOneName + "</name><amount>" + ProdIdOneAmt + "</amount></product><product><id>" + ProdIdtwo + "</id><name>" + ProdIdtwoName + "</name><amount>" + ProdIdtwoAmt + "</amount></product></products>";


            byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            int iterations = 65536;
            int keysize = 256;
            AtomPaymentASE ObjAtomASE = new AtomPaymentASE();

            string passphrase = "3BC164F5FB0AF2869CB3E16C7820706C";
            string salt = "3BC164F5FB0AF2869CB3E16C7820706C";
            string MultEnc = ObjAtomASE.Encrypt(plaintext, passphrase, salt, iv, iterations);
            string MultiEncUrl = "https://payment.atomtech.in/paynetz/epi/fts?login=119989&encdata=" + MultEnc + "";
            HttpContext.Current.Response.Redirect(MultiEncUrl, false);
        }
        catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            throw ex;
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
    public double CalulateAmount(double amount, double percentage)
    {
        double result;
        try
        {
            result = ((amount * percentage) / 100);
            return result;
        }
        catch
        {
            result = 0;
            return result;
        }
    }
}
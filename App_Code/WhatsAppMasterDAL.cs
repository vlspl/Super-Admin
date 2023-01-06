using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DemoRequestDAL
/// </summary>
public class WhatsAppMasterDAL
{
    // DemoRequestDAL objdal = new DemoRequestDAL();
    private SqlConnection con;
    private string Id;

    public WhatsAppMasterDAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable fillData(string whatsappMasterId)
    {
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());

        //here get data by employyee id


        SqlCommand cmd = new SqlCommand("SELECT  Distinct status ,CONVERT(varchar,approveDate,103) as approveDate,approveBy,msgName,noOfParameters,body ,requestBy,paramList  FROM tbl_WhatsappMsgMaster where whatsappMasterId='" + whatsappMasterId + "'", con);

        cmd.CommandType = CommandType.Text;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();

        sda.Fill(dt);

        return dt;
    }
}
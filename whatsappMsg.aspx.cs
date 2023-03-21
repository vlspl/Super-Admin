using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessHandler;
using System.Data.SqlClient;
using System.Data;
using CrossPlatformAESEncryption.Helper;
using System.Configuration;
using Validation;

public partial class SuperAdmin_whatsappMsg: System.Web.UI.Page
{
    CLSRoleMaster roleMstr = new CLSRoleMaster();
    DataAccessLayer DAL = new DataAccessLayer();
    InputValidation Ival = new InputValidation();
    DBClass db = new DBClass();
    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          db.bindDrp("select msgName from tbl_WhatsappMsgMaster where sLabId=0  order by msgName asc", drptemplate, "msgName", "msgName");
            drptemplate.Items.Insert(0, new ListItem("-Select Template-", "-Select Template-"));
        }
       
    }
  
   

    protected void drptemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtremark.Text = db.getData("select paramList from tbl_WhatsappMsgMaster where msgName='" + drptemplate.Text + "'").ToString();
      dt_paramList.DataSource=  getParamValue(txtremark.Text);
      dt_paramList.DataBind();
    }
    protected void btnSendMessage_Click(object sender, EventArgs e)
    {

       int count = dt_paramList.Items.Count;
        string lblText = "";
        foreach (DataListItem item in dt_paramList.Items)
        {
            TextBox myTextBox = (TextBox)item.FindControl("TextBox");
            lblText += myTextBox.Text + " , ";
            // Do whatever you need with that string value here
        }
       
        hdnparam.Value = lblText.ToString();
        newWhatsapp wa = new newWhatsapp();
        wa.sendWhatsappMsg_superadmin("+91" + txtmobileNo.Text, drptemplate.Text, hdnparam.Value);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Message Send successfully');location.href='whatsappMsg.aspx';", true);
    }
    

    public DataTable getParamValue(string paramSource)
    { 

        DataTable table = new DataTable();
 
        DataColumn col_ParamName = table.Columns.Add("ParamName", typeof(string));
        DataColumn col_ParamValue = table.Columns.Add("Values", typeof(string));

        string source = paramSource;

        string[] lines = source.Split(',');

        foreach(var line in lines)
        {
            DataRow row = table.NewRow();   
            row.SetField(col_ParamName, line);
            row.SetField(col_ParamValue, "");

            table.Rows.Add(row);
        }

return table;
    }
       

}
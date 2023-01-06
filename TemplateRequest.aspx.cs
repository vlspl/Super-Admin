using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

public partial class TemplateRequest : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["requestId"] != null)
            {
                hdnrequestId.Value = Request.QueryString["requestId"].ToString();
                viewDetails();
            }
        }

    }

    void viewDetails()
    {
        btndelete.Visible = true;
        btnupdate.Visible = true;
        btnrequest.Visible = false;
        string labId = Request.Cookies["labId"].Value.ToString();
        SqlParameter[] paramEmg_getMaterial = new SqlParameter[]
         {         
              new SqlParameter("@sLabId", labId),
               new SqlParameter("@requestId", hdnrequestId.Value)
          };
        DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_gettemplateListbyId", paramEmg_getMaterial);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                string AllUserList = "";
                int count = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    txtTempName.Text = row["msgName"].ToString();
                    drpTrans.Text = row["msgType"].ToString();
                    txtParameter.Text = row["noOfParameters"].ToString();
                    txtMsgText.Text = row["body"].ToString();
                    txtParaneterList.Text = row["paramList"].ToString();
                    txtRequestBy.Text = row["requestBy"].ToString();
                    txtapprovalStatus.Text = row["status"].ToString();
                    
                    txtApproveDate.Text = System.DateTime.Now.ToString("MM/dd/yyyy");

                   

                }
               
            }
            else
            {
                
            }
        }
    }
    


    protected void btnrequest_Click(object sender, EventArgs e)
    {
        int masterId;
        string labId = Request.Cookies["labId"].Value.ToString();
        if (txtTempName.Text != "")
        {
            if (drpTrans.Text != "-Select-")
            {
                SqlParameter[] param_materialMaster = new SqlParameter[]
                {
                    new SqlParameter("@TempleteName", txtTempName.Text),
                    new SqlParameter("@Messagetext", txtMsgText.Text),
                    new SqlParameter("@MsgType", drpTrans.Text),
                    new SqlParameter("@Parameter", txtParameter.Text),
                    new SqlParameter("@ParameterList", txtParaneterList.Text),
                    new SqlParameter("@status", txtapprovalStatus.Text),
                new SqlParameter("@requestBy", txtRequestBy.Text),
                new SqlParameter("@approveBy", ""),
                    new SqlParameter("@approveDate", System.DateTime.Now.ToString("MM/dd/yyyy")),
                      new SqlParameter("@sLabId", labId.ToString()),
                    new SqlParameter("@returnval", SqlDbType.Int),
                };
                masterId = DAL.ExecuteStoredProcedureRetnInt("Sp_templateRequest", param_materialMaster);
                string labName = db.getData("select sLabName from labMaster where sLabId='" + labId + "'");
                String mobileNo = Convert.ToString(ConfigurationManager.AppSettings["contactNo"]);
                newWhatsapp wa = new newWhatsapp();
                wa.sendWhatsappMsg("+918600666159", "Lab Payment For Lab", labName + ',' + txtTempName.Text + ',' + System.DateTime.Now.ToString("MM/dd/yyyy"), labId);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Template Request Send Successfully..');location.href='ViewTemplateRequest.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Select Material Unit');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Enter Material Name');", true);
        }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string labId = Request.Cookies["labId"].Value.ToString();
        db.insert("delete from tbl_WhatsappMsgMaster where sLabId='" + labId + "' and whatsappMasterId='"+hdnrequestId.Value+"'");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Template Delete Successfully..');location.href='ViewTemplateRequest.aspx';", true);
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        int updatemasterId;
        string labId = Request.Cookies["labId"].Value.ToString();
        SqlParameter[] param_materialMaster = new SqlParameter[]
                {
                    new SqlParameter("@TempleteName", txtTempName.Text),
                    new SqlParameter("@Messagetext", txtMsgText.Text),
                    new SqlParameter("@MsgType", drpTrans.Text),
                    new SqlParameter("@Parameter", txtParameter.Text),
                    new SqlParameter("@ParameterList", txtParaneterList.Text),
                    new SqlParameter("@status", "Pending"),
                new SqlParameter("@requestBy", txtRequestBy.Text),
                new SqlParameter("@approveBy", ""),
                    new SqlParameter("@approveDate", System.DateTime.Now.ToString("MM/dd/yyyy")),
                      new SqlParameter("@sLabId", labId.ToString()), 
                     new SqlParameter("@requestId", hdnrequestId.Value),
                     new SqlParameter("@returnval", SqlDbType.Int),
                };
        updatemasterId = DAL.ExecuteStoredProcedureRetnInt("Sp_templateRequest_update", param_materialMaster);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "insertSuccess", "alert('Template Request Update Successfully..');location.href='ViewTemplateRequest.aspx';", true);
    }
}


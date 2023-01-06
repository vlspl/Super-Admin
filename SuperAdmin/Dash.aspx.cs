using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessHandler;

public partial class SuperAdmin_Dash : System.Web.UI.Page
{
     DataAccessLayer DAL = new DataAccessLayer();
    DBClass db = new DBClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["rollMasterId"] != null)
            {
                hdnrollMasterId.Value = Session["rollMasterId"].ToString();
                SqlParameter[] param = new SqlParameter[]
                   {
                        new SqlParameter("@rollMasterId", hdnrollMasterId.Value),
                
                   };
                DataSet ds_tempData = DAL.ExecuteStoredProcedureDataSet("Sp_rolecounter", param);// sp added newly
                
                    if (ds_tempData.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds_tempData.Tables[0].Rows)
                        {
                            string cName = row["counterName"].ToString();
                            if (cName == "Lab")
                                lb.Visible = true;
                            if (cName == "Organization")
                                org.Visible = true;
                            if (cName == "Government")
                                gov.Visible = true;
                           

                            showCounter();
                        }
                    }
               
                else
                {
                    showCounter();
                    lb.Visible = true;
                    org.Visible = true;
                    gov.Visible = true;
                    Div1.Visible = true;
                }
                
            }
            
           
        }
    }
    void showCounter()
    {
        lbllabs.Text = db.getData("select count(1) from labMaster where sLabStatus='Active' and IsActive=1");
        lblorganizations.Text = db.getData("select count(1) from OrganizationMaster where Org_Status=1 and IsActive=1 and Contact!='' and email!='' and govOrg=0 ");
        lblhealthcamp.Text = db.getData("select count(1) from  healthCampMaster where status=1");
        lblrequestLab.Text = db.getData("select count(1) from  labMaster_Temp where sLabStatus='Pending Approval'");
        lblpackages.Text = db.getData("select count(1) from  packageMaster where status='Active'");
        lblassignpkg.Text = db.getData("select count(1) from  PackageAssignMaster ");
        lblgov.Text = db.getData("select count(1) from  OrganizationMaster where govOrg=1 ");
		 lbldemorequest.Text = db.getData("select count(1) from  demoPage ");
    }
    
}
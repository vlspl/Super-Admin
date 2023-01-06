using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessHandler;
using System.Data.SqlClient;

public partial class mViewReport : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["reportId"] != null)
            {
                string bookingId="";
                string LabId="",pID="";
                string reportId = Request.QueryString["reportId"].ToString();
                SqlParameter[] param = new SqlParameter[]
                 {
                          new SqlParameter("@reportId",reportId)  

                  };
                DataSet ds = DAL.ExecuteStoredProcedureDataSet("Sp_getDetailsforMViewReport", param);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            bookingId = row["sBookLabTestId"].ToString();
                            LabId = row["sLabId"].ToString();
							 pID=row["sPatientId"].ToString();
                        }

                    }
               
                    Response.Redirect(@"https://visionarylifescience.com/viewNReport.aspx?bookLabTestId=" + bookingId + "&labId=" + LabId + "&patientID=" + pID);
                   
                }
            }
        }

    }
}
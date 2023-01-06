
<%@ WebHandler Language="C#" Class="ViewReports" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public class ViewReports : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection scon;
    SqlCommand scom;
    SqlDataAdapter sda;
    public void ProcessRequest (HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Action = Convert.ToString(context.Request.QueryString["sAction"]);
        StringBuilder sbData = new StringBuilder();


        switch (Action)
        {


            case "GetTestList":
                string Booklabtestid = Convert.ToString(context.Request.QueryString["Booklabtestid"]);

              //  string struser = context.Session["Username"].ToString();
                DataTable ds = GetUserDetails(Booklabtestid);
                if (ds != null && ds.Rows.Count > 0)
                {
                    sbData.Append("[");
                    for (int j = 0; j < ds.Rows.Count; j++)
                    {
                        string Gender = cGeneralHelper.JSONEscape(ds.Rows[j]["sGender"].ToString().ToString().ToLower());
                        string Male = cGeneralHelper.JSONEscape(ds.Rows[j]["sMale"].ToString());
                        string Female = cGeneralHelper.JSONEscape(ds.Rows[j]["sFemale"].ToString());
                        string Range = "";
                        if (Gender == "male")
                        {
                            //string[] MaleRange = Male.Split('-');
                            // string StartRange = MaleRange[0];
                            //  string EndRange = MaleRange[0];
                            Range = Male;
                        }
                        if (Gender == "female")
                        {
                            Range = Female;
                        }
                        if (j == 0)
                        {

                            string DateOfBirth = cGeneralHelper.JSONEscape(ds.Rows[j]["sBirthDate"].ToString());
                            string[] DOB = DateOfBirth.Split('/');
                            string year = DOB[2];
                            int Age = Convert.ToInt32(year) -  Convert.ToInt32(DateTime.Now.Year);

                            
                            
                            sbData.Append("{");
                            sbData.Append("\"sLabName\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sLabName"].ToString()) + "\",");
                            sbData.Append("\"sLabContact\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sLabContact"].ToString()) + "\",");
                            sbData.Append("\"sLabAddress\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sLabAddress"].ToString()) + "\",");
                            sbData.Append("\"sBookLabId\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sBookLabId"].ToString()) + "\",");
                            sbData.Append("\"sBookLabTestId\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sBookLabTestId"].ToString()) + "\",");
                            sbData.Append("\"sPatient\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sPatient"].ToString()) + "\",");
                            sbData.Append("\"sGender\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sGender"].ToString()) + "\",");
                            sbData.Append("\"sDoctor\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sDoctor"].ToString()) + "\",");
                            sbData.Append("\"sTestDate\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sBookRequestedAt"].ToString()) + "\",");
                            sbData.Append("\"sReportCreatedOn\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sReportCreatedOn"].ToString()) + "\",");
                            sbData.Append("\"sReportCreatedBy\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sReportCreatedBy"].ToString()) + "\",");   
                            sbData.Append("\"sApprovalStatus\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sApprovalStatus"].ToString()) + "\",");
                            sbData.Append("\"sComment\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sComment"].ToString()) + "\",");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sDoctorId\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sDoctorId"].ToString()) + "\",");
                            sbData.Append("\"sAnalyte\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sAnalyte"].ToString()) + "\",");
                            sbData.Append("\"sSubanalyte\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sSubanalyte"].ToString()) + "\",");
                            sbData.Append("\"sSpecimen\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sSpecimen"].ToString()) + "\",");
                            sbData.Append("\"sMethod\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sMethod"].ToString()) + "\",");
                            sbData.Append("\"sValue\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sValue"].ToString()) + "\",");
                            sbData.Append("\"sResult\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sResult"].ToString()) + "\",");
                            sbData.Append("\"sPDFfiles\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sPDFfiles"].ToString()) + "\",");
                            sbData.Append("\"ssharedReport\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sharedReportCount"].ToString()) + "\",");
                            sbData.Append("\"sUnits\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sUnits"].ToString()) + "\",");
                            sbData.Append("\"sNotes\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sNotes"].ToString()) + "\",");
                            
                            sbData.Append("\"sRange\":\"" + Range + "\"");                                                 
                              sbData.Append("}");
                        }
                        else
                        {
                            sbData.Append(",{");
                            sbData.Append("\"sLabName\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sLabName"].ToString()) + "\",");
                            sbData.Append("\"sLabContact\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sLabContact"].ToString()) + "\",");
                            sbData.Append("\"sLabAddress\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sLabAddress"].ToString()) + "\",");
                            sbData.Append("\"sBookLabId\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sBookLabId"].ToString()) + "\",");
                            sbData.Append("\"sBookLabTestId\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sBookLabTestId"].ToString()) + "\",");
                            sbData.Append("\"sPatient\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sPatient"].ToString()) + "\",");
                            sbData.Append("\"sGender\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sGender"].ToString()) + "\",");
                            sbData.Append("\"sDoctor\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sDoctor"].ToString()) + "\",");
                            sbData.Append("\"sTestDate\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sBookRequestedAt"].ToString()) + "\",");
                            sbData.Append("\"sReportCreatedOn\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sReportCreatedOn"].ToString()) + "\",");
                            sbData.Append("\"sReportCreatedBy\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sReportCreatedBy"].ToString()) + "\",");
                            sbData.Append("\"sApprovalStatus\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sApprovalStatus"].ToString()) + "\",");
                            sbData.Append("\"sComment\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sComment"].ToString()) + "\",");
                            sbData.Append("\"sTestCode\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sTestCode"].ToString()) + "\",");
                            sbData.Append("\"sDoctorId\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sDoctorId"].ToString()) + "\",");
                            sbData.Append("\"sAnalyte\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sAnalyte"].ToString()) + "\",");
                            sbData.Append("\"sSubanalyte\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sSubanalyte"].ToString()) + "\",");
                            sbData.Append("\"sSpecimen\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sSpecimen"].ToString()) + "\",");
                            sbData.Append("\"sMethod\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sMethod"].ToString()) + "\",");
                            sbData.Append("\"sValue\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sValue"].ToString()) + "\",");
                            sbData.Append("\"sResult\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sResult"].ToString()) + "\",");
                            sbData.Append("\"sPDFfiles\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sPDFfiles"].ToString()) + "\",");
                            sbData.Append("\"ssharedReport\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sharedReportCount"].ToString()) + "\",");
                            sbData.Append("\"sUnits\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sUnits"].ToString()) + "\",");                         
                            sbData.Append("\"sNotes\":\"" + cGeneralHelper.JSONEscape(ds.Rows[j]["sNotes"].ToString()) + "\",");

                            sbData.Append("\"sRange\":\"" + Range + "\"");                                           
                             sbData.Append("}");
                        }
                    }
                    sbData.Append("]");
                }
                context.Response.Write(sbData.ToString());
                break;
        }
    }




    public DataTable GetUserDetails(string booklabtestid)
    {
        try
        {
            DataTable ds = new DataTable();
            string Query = @"  select  bl.sBookLabId,  bl.sPatientId, bl.steststatus, bl.spaymentstatus, blt.sapprovalstatus, blt.sPDFfiles, bl.sBookRequestedAt,
		au.sFullName as sPatient,  au.sGender as sGender, au.sBirthDate as sBirthDate,
		bl.sDoctorId, aud.sFullName as sDoctor, 
		lm.sLabId, lm.sLabName, lm.sLabAddress, lm.sLabContact, 
		t.sTestId, t.sTestCode, t.sTestName, 
		bl.sTestDate, bl.sTestStatus, bl.sFees, bl.sBookMode, bl.sBookStatus, 
		blt.sBookLabTestId, blt.sReportStatus, blt.sApprovalStatus, blt.sReportCreatedOn, 
		blt.sReportCreatedBy, blt.sReportApprovedOn, blt.sReportApprovedBy, blt.sNotes, blt.sComment, 
        trv.sTestReportValuesId, trv.sAnalyte, trv.sSubAnalyte, trv.sSpecimen, trv.sMethod, trv.sResultType, 
		trv.sReferenceType, trv.sAge, trv.sMale, trv.sFemale, trv.sGrade, trv.sUnits, trv.sInterpretation, 
		trv.sLowerLimit, trv.sUpperLimit, trv.sValue, trv.sResult,(select count(*) from sharedReport where sReportId= " + booklabtestid + " and sPatientId= bl.sPatientId and sDoctorId= bl.sDoctorId) as sharedReportCount from bookLab bl  full join appUser au on au.sAppUserId=bl.sPatientId full join appUser aud on aud.sAppUserId=bl.sDoctorId join labMaster lm on lm.sLabId=bl.sLabId join bookLabTest blt on blt.sBookLabId=bl.sBookLabId join test t on t.sTestId=blt.sTestId join testReportValues trv on trv.sBookLabTestId=blt.sBookLabTestId where bl.steststatus = 'taken' and blt.sapprovalstatus ='approved' and blt.sBookLabTestId=" + booklabtestid + "  and trv.sAnalyte!= ''";
            
            scon = new SqlConnection(strcon);
            scon.Open();
            scom = new SqlCommand(Query, scon);
            sda = new SqlDataAdapter(scom);
            sda.Fill(ds);
            return ds;

        }
        catch (Exception ex)
        {
            return null;
            throw;
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}



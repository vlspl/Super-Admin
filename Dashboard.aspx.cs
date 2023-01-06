using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessHandler;
using System.Data.SqlClient;
using CrossPlatformAESEncryption.Helper;

public partial class Dashboard : System.Web.UI.Page
{
    DataAccessLayer DAL = new DataAccessLayer();
    ClsLabDashboard clsDashObj = new ClsLabDashboard();
 DBClass db = new DBClass();
    DateTime startDate = DateTime.Now;
    DateTime endDate = DateTime.Now;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["loggedIn"] != null)
            {
                if (Session["Count"] == "1")
                {
                   // OneWeek();
                  Session["Count"] = "0";
                }
                else if (Session["Count"] == "2")
                {
                   // OneMonth();
               Session["Count"] = "0";
                }
                else if (Session["Count"] == "3")
                {
                   // OneYear();
                    Session["Count"] = "0";
                }
                else if (Session["Count"] == "4")
                {
                    //CustomDate();
                    Session["Count"] = "0";
                    Session["startdate"] = "";
                    Session["enddate"] = "";
                }
                else
                {
                    loadMyTestBookings();
                }
            }
            else
            {
                Response.Redirect("LabLogin.aspx", false);
            }
        }
       catch (Exception ex)
        {
           
            Response.Redirect(@"LabLogin.aspx", false);
        }
    }

    protected void loadMyTestBookings()
    {
        try
        {
            //DataTable dt = DAL.GetDataTable("Sp_Dashboard " + Request.Cookies["labId"].Value.ToString());
            DataSet ds = clsDashObj.getMyBookingsForDashboard(Request.Cookies["labId"].Value.ToString());


           // LitTotalTest.Text = dt.Rows[0]["Patient"].ToString();
            //LitpendingTest.Text = dt.Rows[0]["Sheduletest"].ToString();
           // LitcompletedReport.Text = dt.Rows[0]["Pendingreport"].ToString();
           // LitdeliveryReport.Text = dt.Rows[0]["ApprovedReport"].ToString();
           // hLitTotalTest.Value = dt.Rows[0]["Patient"].ToString();
 string labid = Request.Cookies["labId"].Value.ToString();

            LitTotalTest.Text = db.getData("select count(sBookLabId)  from bookLab where sLabId='" + labid + "'").ToString();
            LitpendingTest.Text = db.getData("select count(sBookLabId)  from bookLab  where sLabId='" + labid + "' and sTestStatus='Pending' and sBookStatus !='Canceled'").ToString();// dt.Rows[0]["Sheduletest"].ToString("select count(bl.sBookLabId)  from bookLab bl  where bl.sLabId=968 and bl.sTestStatus='Pending' and bl.sBookStatus !='Canceled'");
            LitcompletedReport.Text = db.getData(@"select  count(bl.sBookLabId) from bookLab bl join bookLabTest blt on Convert(varchar(50),blt.sBookLabId)=Convert(varchar(50),bl.sBookLabId)  join test t on Convert(varchar(50),t.sTestId)=Convert(varchar(50),blt.sTestId)  where bl.sLabId='" + labid + "' and bl.sTestStatus='taken' and bl.sBookStatus !='Canceled' and (blt.sApprovalStatus is null or blt.sApprovalStatus='')");
            LitdeliveryReport.Text = db.getData(@"select count(bl.sBookLabId)  from bookLab bl    
   join bookLabTest blt on blt.sBookLabId=bl.sBookLabId     
   where bl.sLabId='" + labid + "' and bl.sTestStatus='taken' and blt.sApprovalStatus  ='Approved'");
            litpendingApproval.Text = db.getData(@"select count(bl.sBookLabId)  from bookLab bl    
   join bookLabTest blt on blt.sBookLabId=bl.sBookLabId     
   where bl.sLabId='" + labid + "' and bl.sTestStatus='taken' and blt.sApprovalStatus  ='Approval Pending'");

            #region AppointmentList
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string tabMyTestBookList = "";

                    int count = 0;

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        count = count + 1;
                        //Load lab test book list
                        tabMyTestBookList += "<tr>" +
                                           "<td scope='col'>" + count + "</td>" +
                                           "<td scope='col'>" + row["sPatient"].ToString() + "</td>" +
                                           "<td scope='col'>" + row["sDoctor"].ToString() + "</td>" +
                                           "<td scope='col' id='slotdate' class='slotdate '><span class='pr-''><i class='fas fa-calendar-week pr-1 text-gray'></i></span>" + row["bookslotdate"].ToString() + "</td>" +
                                           "<td scope='col'><span class='pr-''><i class='fas fa-calendar-week pr-1 text-gray'></i></span>" + row["sTestDate"].ToString() + " &nbsp;&nbsp; " + row["sTimeSlot"].ToString() + " </td>" +
                                           "<td scope='col'><i class='fa fa-inr' aria-hidden='true' ></i> " + row["sFees"].ToString() + "</td>";
                        if (row["sBookMode"].ToString().ToLower() == "manual")
                        {
                            tabMyTestBookList += "<td scope='col'><span class='hidden'></span><i class='fa fa-user fa-lg' aria-hidden='true' ></i></td>";
                        }
                        else if (row["sBookMode"].ToString().ToLower() == "mobile" || row["sBookMode"].ToString().ToLower() == "prescription")
                        {
                            tabMyTestBookList += "<td scope='col'><span class='hidden'></span> <i class='fa fa-mobile fa-lg' aria-hidden='true'></i></td>";
                        }
                        string color = "";
                        if (row["sBookStatus"].ToString().ToLower() == "confirmed")
                        {
                            color = "#2E86C1";
                        }
                        if (row["sBookStatus"].ToString().ToLower() == "awaiting")
                        {
                            color = "#f2a30f";
                        }
                        if (row["sBookStatus"].ToString().ToLower() == "canceled")
                        {
                            color = "#f51105";
                        }
                        tabMyTestBookList += "<td scope='col'  '><span style='color:" + color + ";font-weight: bold'>" + row["sBookStatus"].ToString() + "</span></td>" +
                                         "<td scope='col' '>" + row["sAppointmentType"].ToString() + "</td>" +
                                             "<td scope='col'><a href='BookDetails.aspx?id=" + row["sBookLabId"].ToString() + "' class='btn btn-dark p-1 text-xs'>   View </a></td>" +
                                          "</tr>";
                    }
                    tbodyTestBookList.InnerHtml = tabMyTestBookList;
                }
                else
                {
                    tbodyTestBookList.InnerHtml = "<tr><td colsoan='10'>No records found</td></tr>";
                }
            }
            #endregion
            DataSet dsDash = clsDashObj.GetDashboardAnalytics(Request.Cookies["labId"].Value.ToString());
            if (dsDash.Tables.Count > 0)
            {
                #region TestReportCount
                DataTable dtTestCount = dsDash.Tables[0];
                if (dtTestCount.Rows.Count > 0)
                {
                    string testName = "";
                    string TestCount = "";
                    string TestSum = "";

                    for (int i = 0; i < dtTestCount.Rows.Count; i++)
                    {
                        testName += dtTestCount.Rows[i]["TestName"].ToString() + ",";
                        TestCount += dtTestCount.Rows[i]["TestCount"].ToString() + ",";
                        TestSum = dtTestCount.Rows[i]["TestSum"].ToString();
                        int id = 1;
                        if (i == 0)
                        {
                            spanTestName1.InnerHtml = dtTestCount.Rows[0]["TestName"].ToString();
                            TCRspanTestName1.InnerHtml = dtTestCount.Rows[0]["TestName"].ToString();
                        }
                        else if (i == 1)
                        {
                            spanTestName2.InnerHtml = dtTestCount.Rows[1]["TestName"].ToString();
                            TCRspanTestName2.InnerHtml = dtTestCount.Rows[1]["TestName"].ToString();
                        }
                        else if (i == 2)
                        {
                            spanTestName3.InnerHtml = dtTestCount.Rows[2]["TestName"].ToString();
                            TCRspanTestName3.InnerHtml = dtTestCount.Rows[2]["TestName"].ToString();
                        }
                        else if (i == 3)
                        {
                            spanTestName4.InnerHtml = dtTestCount.Rows[3]["TestName"].ToString();
                            TCRspanTestName4.InnerHtml = dtTestCount.Rows[3]["TestName"].ToString();
                        }
                        else
                        {
                            spanTestName5.InnerHtml = dtTestCount.Rows[4]["TestName"].ToString();
                            TCRspanTestName5.InnerHtml = dtTestCount.Rows[4]["TestName"].ToString();
                        }
                    }
                    HTestCountReportTestName.Value = testName.TrimEnd(',');
                    HTestCountReportTestCount.Value = TestCount.TrimEnd(',');
                    HTestCountReportTestSum.Value = TestSum;
                    spanTestTotalCount.InnerHtml = TestSum;
                }

                #endregion
                #region TestReportRevenuCount
                DataTable dtTestRevenuCount = dsDash.Tables[1];
                if (dtTestRevenuCount.Rows.Count > 0)
                {
                    string testCode = "";
                    string testName = "";
                    string TestCount = "";
                    string TestSum = "";

                    for (int i = 0; i < dtTestCount.Rows.Count; i++)
                    {
                        testCode += dtTestRevenuCount.Rows[i]["TestCode"].ToString() + ",";
                        testName += dtTestRevenuCount.Rows[i]["TestName"].ToString() + ",";
                        TestCount += dtTestRevenuCount.Rows[i]["TestCount"].ToString() + ",";
                        TestSum = dtTestRevenuCount.Rows[i]["TotalTestSum"].ToString();
                        int id = 1;
                        if (i == 0)
                        {
                            spanTestRevenuCount1.InnerHtml = dtTestRevenuCount.Rows[0]["TestCode"].ToString();
                            TCRspanTestRevenuCount1.InnerHtml = dtTestRevenuCount.Rows[0]["TestCode"].ToString();
                        }
                        else if (i == 1)
                        {
                            spanTestRevenuCount2.InnerHtml = dtTestRevenuCount.Rows[1]["TestCode"].ToString();
                            TCRspanTestRevenuCount2.InnerHtml = dtTestRevenuCount.Rows[1]["TestCode"].ToString();
                        }
                        else if (i == 2)
                        {
                            spanTestRevenuCount3.InnerHtml = dtTestRevenuCount.Rows[2]["TestCode"].ToString();
                            TCRspanTestRevenuCount3.InnerHtml = dtTestRevenuCount.Rows[2]["TestCode"].ToString();
                        }
                        else if (i == 3)
                        {
                            spanTestRevenuCount4.InnerHtml = dtTestRevenuCount.Rows[3]["TestCode"].ToString();
                            TCRspanTestRevenuCount4.InnerHtml = dtTestRevenuCount.Rows[3]["TestCode"].ToString();
                        }
                        else
                        {
                            spanTestRevenuCount5.InnerHtml = dtTestRevenuCount.Rows[4]["TestCode"].ToString();
                            TCRspanTestRevenuCount5.InnerHtml = dtTestRevenuCount.Rows[4]["TestCode"].ToString();
                        }
                    }
                    HTestRevenuCountTestName.Value = testCode.TrimEnd(',');
                    HTestRevenuCountTestCount.Value = TestCount.TrimEnd(',');
                    HTestRevenuCountTestSum.Value = TestSum;
                    spanTestTotalRevenu.InnerHtml = TestSum;
                }

                #endregion
                #region Booking Paid and Dues
                DataTable dtPaidDues = dsDash.Tables[2];
                if (dtPaidDues.Rows.Count > 0)
                {
                    string PaymentStatus = "";
                    string Amount = "";
                    string TotalSum = "";

                    for (int i = 0; i < dtPaidDues.Rows.Count; i++)
                    {
                        PaymentStatus += dtPaidDues.Rows[i]["PaymentStatus"].ToString() + ",";
                        Amount += dtPaidDues.Rows[i]["Amount"].ToString() + ",";
                        TotalSum = dtPaidDues.Rows[i]["TotalSum"].ToString();
                    }
                    HPaymentStatus.Value = PaymentStatus.TrimEnd(',');
                    HPaymentAmount.Value = Amount.TrimEnd(',');
                    HPaymentTotalSum.Value = TotalSum;
                    spanTotalSum.InnerHtml = TotalSum;
                }

                #endregion
                #region Test Count Gender wise
                DataTable dtGenderTest = dsDash.Tables[3];
                if (dtGenderTest.Rows.Count > 0)
                {
                    string Gender = "";
                    string TestCount = "";
                    string TotalSum = "";

                    for (int i = 0; i < dtGenderTest.Rows.Count; i++)
                    {
                        Gender += dtGenderTest.Rows[i]["Gender"].ToString() + ",";
                        TestCount += dtGenderTest.Rows[i]["TestCount"].ToString() + ",";
                        TotalSum = dtGenderTest.Rows[i]["TotalSum"].ToString();
                        int id = 1;
                        if (i == 0)
                        {
                            spanGender1.InnerHtml = dtGenderTest.Rows[0]["Gender"].ToString();
                            PiespanGender1.InnerHtml = dtGenderTest.Rows[0]["Gender"].ToString();
                        }
                        else
                        {
                            spanGender2.InnerHtml = dtGenderTest.Rows[1]["Gender"].ToString();
                            PiespanGender2.InnerHtml = dtGenderTest.Rows[1]["Gender"].ToString();
                        }
                    }
                    HGender.Value = Gender.TrimEnd(',');
                    HTestCountGender.Value = TestCount.TrimEnd(',');
                    HTotalGenderTestCount.Value = TotalSum;
                    spanTotalTestCountGenderwise.InnerHtml = TotalSum;
                }

                #endregion
                #region Doctor business
                DataTable dtDoc = dsDash.Tables[4];
                if (dtDoc.Rows.Count > 0)
                {
                    string DocName = "";
                    string DocAmount = "";
                    string TotalSum = "";

                    for (int i = 0; i < dtDoc.Rows.Count; i++)
                    {
                        DocName += dtDoc.Rows[i]["DocName"].ToString() + ",";
                        DocAmount += dtDoc.Rows[i]["DocAmount"].ToString() + ",";
                        TotalSum = dtDoc.Rows[i]["TotalSum"].ToString();
                        int id = 1;
                        if (i == 0)
                        {
                            spanRefDoc1.InnerHtml = dtDoc.Rows[0]["DocName"].ToString();
                            DonutspanRefDoc1.InnerHtml = dtDoc.Rows[0]["DocName"].ToString();
                        }
                        else if (i == 1)
                        {
                            spanRefDoc2.InnerHtml = dtDoc.Rows[1]["DocName"].ToString();
                            DonutspanRefDoc2.InnerHtml = dtDoc.Rows[1]["DocName"].ToString();
                        }
                        else if (i == 2)
                        {
                            spanRefDoc3.InnerHtml = dtDoc.Rows[2]["DocName"].ToString();
                            DonutspanRefDoc3.InnerHtml = dtDoc.Rows[2]["DocName"].ToString();
                        }
                        else if (i == 3)
                        {
                            spanRefDoc4.InnerHtml = dtDoc.Rows[3]["DocName"].ToString();
                            DonutspanRefDoc4.InnerHtml = dtDoc.Rows[3]["DocName"].ToString();
                        }
                        else
                        {
                            spanRefDoc5.InnerHtml = dtDoc.Rows[4]["DocName"].ToString();
                            DonutspanRefDoc5.InnerHtml = dtDoc.Rows[4]["DocName"].ToString();
                        }
                    }
                    HDocName.Value = DocName.TrimEnd(',');
                    HDocAmount.Value = DocAmount.TrimEnd(',');
                    HDocTotalAmount.Value = TotalSum;
                    spanDocTotalSum.InnerHtml = TotalSum;
                }

                #endregion
              
            }
        }
       catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            Response.Redirect(@"LabLogin.aspx", false);
        }
    }
    protected void loadMyDashboard(DataSet ds)
    {
        try
        {
            DataSet dsDash = ds;

            if (dsDash.Tables.Count > 0)
            {
                #region TestReportCount
                DataTable dtTestCount = dsDash.Tables[0];
                if (dtTestCount.Rows.Count > 0)
                {
                    string testName = "";
                    string TestCount = "";
                    string TestSum = "";

                    for (int i = 0; i < dtTestCount.Rows.Count; i++)
                    {
                        testName += dtTestCount.Rows[i]["TestName"].ToString() + ",";
                        TestCount += dtTestCount.Rows[i]["TestCount"].ToString() + ",";
                        TestSum = dtTestCount.Rows[i]["TestSum"].ToString();
                        int id = 1;
                        if (i == 0)
                        {
                            spanTestName1.InnerHtml = dtTestCount.Rows[0]["TestName"].ToString();
                            TCRspanTestName1.InnerHtml = dtTestCount.Rows[0]["TestName"].ToString();
                        }
                        else if (i == 1)
                        {
                            spanTestName2.InnerHtml = dtTestCount.Rows[1]["TestName"].ToString();
                            TCRspanTestName2.InnerHtml = dtTestCount.Rows[1]["TestName"].ToString();
                        }
                        else if (i == 2)
                        {
                            spanTestName3.InnerHtml = dtTestCount.Rows[2]["TestName"].ToString();
                            TCRspanTestName3.InnerHtml = dtTestCount.Rows[2]["TestName"].ToString();
                        }
                        else if (i == 3)
                        {
                            spanTestName4.InnerHtml = dtTestCount.Rows[3]["TestName"].ToString();
                            TCRspanTestName4.InnerHtml = dtTestCount.Rows[3]["TestName"].ToString();
                        }
                        else
                        {
                            spanTestName5.InnerHtml = dtTestCount.Rows[4]["TestName"].ToString();
                            TCRspanTestName5.InnerHtml = dtTestCount.Rows[4]["TestName"].ToString();
                        }
                    }
                    HTestCountReportTestName.Value = testName.TrimEnd(',');
                    HTestCountReportTestCount.Value = TestCount.TrimEnd(',');
                    HTestCountReportTestSum.Value = TestSum;
                    spanTestTotalCount.InnerHtml = TestSum;
                }

                #endregion
                #region TestReportRevenuCount
                DataTable dtTestRevenuCount = dsDash.Tables[1];
                if (dtTestRevenuCount.Rows.Count > 0)
                {
                    string testCode = "";
                    string testName = "";
                    string TestCount = "";
                    string TestSum = "";

                    for (int i = 0; i < dtTestCount.Rows.Count; i++)
                    {
                        testCode += dtTestRevenuCount.Rows[i]["TestCode"].ToString() + ",";
                        testName += dtTestRevenuCount.Rows[i]["TestName"].ToString() + ",";
                        TestCount += dtTestRevenuCount.Rows[i]["TestCount"].ToString() + ",";
                        TestSum = dtTestRevenuCount.Rows[i]["TotalTestSum"].ToString();
                        int id = 1;
                        if (i == 0)
                        {
                            spanTestRevenuCount1.InnerHtml = dtTestRevenuCount.Rows[0]["TestCode"].ToString();
                            TCRspanTestRevenuCount1.InnerHtml = dtTestRevenuCount.Rows[0]["TestCode"].ToString();
                        }
                        else if (i == 1)
                        {
                            spanTestRevenuCount2.InnerHtml = dtTestRevenuCount.Rows[1]["TestCode"].ToString();
                            TCRspanTestRevenuCount2.InnerHtml = dtTestRevenuCount.Rows[1]["TestCode"].ToString();
                        }
                        else if (i == 2)
                        {
                            spanTestRevenuCount3.InnerHtml = dtTestRevenuCount.Rows[2]["TestCode"].ToString();
                            TCRspanTestRevenuCount3.InnerHtml = dtTestRevenuCount.Rows[2]["TestCode"].ToString();
                        }
                        else if (i == 3)
                        {
                            spanTestRevenuCount4.InnerHtml = dtTestRevenuCount.Rows[3]["TestCode"].ToString();
                            TCRspanTestRevenuCount4.InnerHtml = dtTestRevenuCount.Rows[3]["TestCode"].ToString();
                        }
                        else
                        {
                            spanTestRevenuCount5.InnerHtml = dtTestRevenuCount.Rows[4]["TestCode"].ToString();
                            TCRspanTestRevenuCount5.InnerHtml = dtTestRevenuCount.Rows[4]["TestCode"].ToString();
                        }
                    }
                    HTestRevenuCountTestName.Value = testCode.TrimEnd(',');
                    HTestRevenuCountTestCount.Value = TestCount.TrimEnd(',');
                    HTestRevenuCountTestSum.Value = TestSum;
                    spanTestTotalRevenu.InnerHtml = TestSum;
                }

                #endregion
                #region Booking Paid and Dues
                DataTable dtPaidDues = dsDash.Tables[2];
                if (dtPaidDues.Rows.Count > 0)
                {
                    string PaymentStatus = "";
                    string Amount = "";
                    string TotalSum = "";

                    for (int i = 0; i < dtPaidDues.Rows.Count; i++)
                    {
                        PaymentStatus += dtPaidDues.Rows[i]["PaymentStatus"].ToString() + ",";
                        Amount += dtPaidDues.Rows[i]["Amount"].ToString() + ",";
                        TotalSum = dtPaidDues.Rows[i]["TotalSum"].ToString();
                    }
                    HPaymentStatus.Value = PaymentStatus.TrimEnd(',');
                    HPaymentAmount.Value = Amount.TrimEnd(',');
                    HPaymentTotalSum.Value = TotalSum;
                    spanTotalSum.InnerHtml = TotalSum;
                }

                #endregion
                #region Test Count Gender wise
                DataTable dtGenderTest = dsDash.Tables[3];
                if (dtGenderTest.Rows.Count > 0)
                {
                    string Gender = "";
                    string TestCount = "";
                    string TotalSum = "";

                    for (int i = 0; i < dtGenderTest.Rows.Count; i++)
                    {
                        Gender += dtGenderTest.Rows[i]["Gender"].ToString() + ",";
                        TestCount += dtGenderTest.Rows[i]["TestCount"].ToString() + ",";
                        TotalSum = dtGenderTest.Rows[i]["TotalSum"].ToString();
                        int id = 1;
                        if (i == 0)
                        {
                            spanGender1.InnerHtml = dtGenderTest.Rows[0]["Gender"].ToString();
                            PiespanGender1.InnerHtml = dtGenderTest.Rows[0]["Gender"].ToString();
                        }
                        else
                        {
                            spanGender2.InnerHtml = dtGenderTest.Rows[1]["Gender"].ToString();
                            PiespanGender2.InnerHtml = dtGenderTest.Rows[1]["Gender"].ToString();
                        }
                    }
                    HGender.Value = Gender.TrimEnd(',');
                    HTestCountGender.Value = TestCount.TrimEnd(',');
                    HTotalGenderTestCount.Value = TotalSum;
                    spanTotalTestCountGenderwise.InnerHtml = TotalSum;
                }

                #endregion
                #region Doctor business
                DataTable dtDoc = dsDash.Tables[4];
                if (dtDoc.Rows.Count > 0)
                {
                    string DocName = "";
                    string DocAmount = "";
                    string TotalSum = "";

                    for (int i = 0; i < dtDoc.Rows.Count; i++)
                    {
                        DocName += dtDoc.Rows[i]["DocName"].ToString() + ",";
                        DocAmount += dtDoc.Rows[i]["DocAmount"].ToString() + ",";
                        TotalSum = dtDoc.Rows[i]["TotalSum"].ToString();
                        int id = 1;
                        if (i == 0)
                        {
                            spanRefDoc1.InnerHtml = dtDoc.Rows[0]["DocName"].ToString();
                            DonutspanRefDoc1.InnerHtml = dtDoc.Rows[0]["DocName"].ToString();
                        }
                        else if (i == 1)
                        {
                            spanRefDoc2.InnerHtml = dtDoc.Rows[1]["DocName"].ToString();
                            DonutspanRefDoc2.InnerHtml = dtDoc.Rows[1]["DocName"].ToString();
                        }
                        else if (i == 2)
                        {
                            spanRefDoc3.InnerHtml = dtDoc.Rows[2]["DocName"].ToString();
                            DonutspanRefDoc3.InnerHtml = dtDoc.Rows[2]["DocName"].ToString();
                        }
                        else if (i == 3)
                        {
                            spanRefDoc4.InnerHtml = dtDoc.Rows[3]["DocName"].ToString();
                            DonutspanRefDoc4.InnerHtml = dtDoc.Rows[3]["DocName"].ToString();
                        }
                        else
                        {
                            spanRefDoc5.InnerHtml = dtDoc.Rows[4]["DocName"].ToString();
                            DonutspanRefDoc5.InnerHtml = dtDoc.Rows[4]["DocName"].ToString();
                        }
                    }
                    HDocName.Value = DocName.TrimEnd(',');
                    HDocAmount.Value = DocAmount.TrimEnd(',');
                    HDocTotalAmount.Value = TotalSum;
                    spanDocTotalSum.InnerHtml = TotalSum;
                }

                #endregion
               
                #region DashboardKPI
                if (dsDash.Tables[6].Rows.Count > 0)
                {
                    LitTotalTest.Text = dsDash.Tables[6].Rows[0]["Patient"].ToString();
                    LitpendingTest.Text = dsDash.Tables[6].Rows[0]["Sheduletest"].ToString();
                    LitcompletedReport.Text = dsDash.Tables[6].Rows[0]["Pendingreport"].ToString();
                    LitdeliveryReport.Text = dsDash.Tables[6].Rows[0]["ApprovedReport"].ToString();
                    hLitTotalTest.Value = dsDash.Tables[6].Rows[0]["Patient"].ToString();
                }
                #endregion
                #region AppointmentList
                if (dsDash.Tables[7] != null)
                {
                    if (dsDash.Tables[7].Rows.Count > 0)
                    {
                        string tabMyTestBookList = "";

                        int count = 0;

                        foreach (DataRow row in dsDash.Tables[7].Rows)
                        {
                            count = count + 1;
                            //Load lab test book list
                            tabMyTestBookList += "<tr>" +
                                               "<td scope='col'>" + count + "</td>" +
                                               "<td scope='col'>" + row["sPatient"].ToString() + "</td>" +
                                               "<td scope='col'>" + row["sDoctor"].ToString() + "</td>" +
                                               "<td scope='col' id='slotdate' class='slotdate '><span class='pr-''><i class='fas fa-calendar-week pr-1 text-gray'></i></span>" + row["bookslotdate"].ToString() + "</td>" +
                                               "<td scope='col'><span class='pr-''><i class='fas fa-calendar-week pr-1 text-gray'></i></span>" + row["sTestDate"].ToString() + " &nbsp; " + row["sTimeSlot"].ToString() + " </td>" +
                                               "<td scope='col'><i class='fa fa-inr' aria-hidden='true' ></i> " + row["sFees"].ToString() + "</td>";
                            if (row["sBookMode"].ToString().ToLower() == "manual")
                            {
                                tabMyTestBookList += "<td scope='col'><span class='hidden'></span><i class='fa fa-user fa-lg' aria-hidden='true' ></i></td>";
                            }
                            else if (row["sBookMode"].ToString().ToLower() == "mobile" || row["sBookMode"].ToString().ToLower() == "prescription")
                            {
                                tabMyTestBookList += "<td scope='col'><span class='hidden'></span> <i class='fa fa-mobile fa-lg' aria-hidden='true'></i></td>";
                            }
                            string color = "";
                            if (row["sBookStatus"].ToString().ToLower() == "confirmed")
                            {
                                color = "#2E86C1";
                            }
                            if (row["sBookStatus"].ToString().ToLower() == "awaiting")
                            {
                                color = "#f2a30f";
                            }
                            if (row["sBookStatus"].ToString().ToLower() == "canceled")
                            {
                                color = "#f51105";
                            }
                            tabMyTestBookList += "<td scope='col'  '><span style='color:" + color + ";font-weight: bold'>" + row["sBookStatus"].ToString() + "</span></td>" +
                                             "<td scope='col' '>" + row["sAppointmentType"].ToString() + "</td>" +
                                                 "<td scope='col'><a href='BookDetails.aspx?id=" + row["sBookLabId"].ToString() + "' class='btn btn-dark p-1 text-xs'>   View </a></td>" +
                                              "</tr>";
                        }
                        tbodyTestBookList.InnerHtml = tabMyTestBookList;
                    }
                    else
                    {
                        tbodyTestBookList.InnerHtml = "<tr><td colsoan='10'>No records found</td></tr>";
                    }
                }
                #endregion
            }
        }
       catch (Exception ex)
        {
            LogError.LoggerCatch(ex);
            Response.Redirect(@"LabLogin.aspx", false);
        }
    }
  
}

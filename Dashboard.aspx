 <%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>LAB Admin - Dashboard</title>
    <!-- Custom fonts for this template-->
    <link href="Content/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">
    <!-- Custom styles for this template-->
    <link href="Content/css/sb-admin-2.min.css" rel="stylesheet">
    <link href="Content/css/style.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Page Wrapper -->
    <!-- Page Wrapper -->
    <div id="wrapper">
        <!-- Content Wrapper -->
        <div id="content-wrapper" class="d-flex flex-column">
            <!-- Main Content -->
            <div id="content">
                <!-- Topbar -->
                
                <nav class="navbar navbar-expand navbar-light bg-white topbar mb-2 static-top shadow">
          <h3 class="text-dark d-none d-sm-block" style="width:100%;"><span class="text-primary">Dashboard</span> <span class="text-dgreen" style="float:right;">  <a href="InternalMISReport.aspx" id="A1"  runat="server" class="btn btn-info"><span><i class="fa fa-eye mr-2" area-hidden="true"></i></span> View MIS Report</a> <a href="InventoryDashboard.aspx" id="A2"  runat="server" class="btn btn-secondary"><span><i class="fa fa-file mr-2" area-hidden="true"></i></span> Inventory</a>   <a href="CRMDashboard.aspx" id="A3"  runat="server" class="btn btn-primary"><span><i class="fa fa-users mr-2" area-hidden="true"></i></span> CRM System</a></span> </h3>
                    <!-- Topbar Navbar -->
                       
        </nav>
                <!-- End of Topbar -->
                <!-- Begin Page Content -->
                <div class="container-fluid">
                    <!-- Page Heading -->
                    <!-- <div class="d-sm-flex align-items-center justify-content-between mb-2">
            <h1 class="h3 m-0 P-0 text-gray-800">Dashboard</h1>
          </div> -->
                    <!-- Content Row -->

                 





                    <div class="row mt-3" id="topCards">
                        <asp:HiddenField ID="hLitTotalTest" runat="server" ClientIDMode="Static" />
                        <!-- Earnings (Monthly) Card Example -->
                               <div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-primary shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                               Total Test</div>
                                            <div class="h5 mb-0 font-weight-bold text-gray-800"># &nbsp; <asp:Label ID="LitTotalTest"  runat="server" style="margin-left: -10px;"
                                                    ClientIDMode="Static">100</asp:Label> <i style="float:right;" class="fas fa-file fa-2x text-gray-300"></i></div>
                                        </div>
                                       
                                    </div>
                                </div>
                            </div>
                        </div>
                          <div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-success shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                                Pending Test </div>
                                            <div class="h5 mb-0 font-weight-bold text-gray-800"># &nbsp; <asp:Label ID="LitpendingTest" Style="margin-left: -10px;" runat="server">100</asp:Label> <i style="float:right;" class="fas fa-clipboard-list fa-2x text-gray-300"></i></div>
                                        </div>
                                      
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Earnings (Monthly) Card Example -->
                        <div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-info shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-info text-uppercase mb-1">Completed Reports
                                            </div>
                                             
                                                    <div class="h5 mb-0 mr-3 font-weight-bold text-gray-800"># &nbsp;  <asp:Label ID="LitcompletedReport" Style="margin-left: -10px;"
                                                    runat="server">100</asp:Label> <i style="float:right;" class="fas fa-check-circle fa-2x text-gray-300"></i></div>
                                              
                                        </div>
                                      
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Pending Requests Card Example -->
                        <div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-warning shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-warning text-uppercase mb-1">
                                               Approved Reports</div>
                                            <div class="h5 mb-0 font-weight-bold text-gray-800"># &nbsp;  <asp:Label ID="LitdeliveryReport" Style="margin-left: -10px;"
                                                    runat="server">100</asp:Label> <i style="float:right;"  class="fas fa-comments fa-2x text-gray-300"></i></div>
                                        </div>
                                     
                                    </div>
                                </div>
                            </div>
                        </div>
                          <div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-warning shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-warning text-uppercase mb-1">
                                               Pending Approved Reports</div>
                                            <div class="h5 mb-0 font-weight-bold text-gray-800"># &nbsp;  <asp:Label ID="litpendingApproval" Style="margin-left: -10px;"
                                                    runat="server">100</asp:Label> <i style="float:right;"  class="fas fa-comments fa-2x text-gray-300"></i></div>
                                        </div>
                                     
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                      
                    </div>
                    <!-- Content Row -->
                    <div >
                       <%-- <h4 class="h6 m-0 P-0 font-weight-bold text-dark">- class="d-sm-flex align-items-center justify-content-between mb-4"-%>
                      
                          <%--  Appointment List--%>
                         <h4 class="navbar-brand">  Recent 10 Bookings    </h4>
                    </div>
                    <div class="row pl-2 ">
                       <%-- <div class="form-group has-search mr-2">
                            <span class="fa fa-search form-control-feedback"></span>
                            <input type="text" id="myInputTextField" class="form-control" placeholder="Search"
                                style="min-width: 300px">
                        </div>--%>
               <%--         <div class="filterData">
                            <a class="btn btn-light shadow animated--grow-in" id="filterBtn"><i class="fas fa-filter">
                            </i></a>
                            <div class="selectTime">
                                <div class="row pl-4 pt-2">
                                    <h6>
                                        <i class="fas fa-filter mr-1">Filter</i></h6>
                                    <h6 class="ml-auto mr-4" id="closeFilter">
                                        <i class="fas fa-times"></i>
                                    </h6>
                                </div>
                                <hr>
                                <asp:HiddenField ID="HstartDate" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="HendDate" runat="server" ClientIDMode="Static" />
                                <div class="row d-flex flex-nowrap justify-content-around ">
                                    <asp:Button ID="btn1Week" class="btn btn-light btn-outline-dark oneweek" runat="server"
                                        Text="1 Week" OnClick="btn1Week_Click" ClientIDMode="Static" />
                                    <asp:Button ID="btn1Month" class="btn btn-light btn-outline-dark oneMonth" runat="server"
                                        Text="1 Month" OnClick="btn1Month_Click" ClientIDMode="Static" />
                                    <asp:Button ID="btn1Year" class="btn btn-light btn-outline-dark oneYear" runat="server"
                                        Text="1 Year" OnClick="btn1Year_Click" ClientIDMode="Static" />
                                </div>
                                <div class="row  pl-4 py-2">
                                    <a href="#demo" class="btn btn-light btn-outline-dark" data-toggle="collapse">Custom</a>
                                    <div id="demo" class="collapse pt-2">
                                        <hr>
                                        Select a coustom date of your choice
                                        <div class="form-row " id="filterform">
                                            <div class="form-group col-md-6">
                                                <label for="inputEmail4" class="mb-1">
                                                    From Date</label>
                                                <input type="date" class="form-control text-xs" id="from" placeholder="Select Date"
                                                    clientidmode="Static">
                                            </div>
                                            <div class="form-group col-md-6">
                                                <label for="inputPassword4" class="mb-0">
                                                    To Date</label>
                                                <input type="date" class="form-control text-xs" id="to" placeholder="Select Date"
                                                    clientidmode="Static">
                                            </div>
                                        </div>
                                        <asp:Button ID="btnCustomdate" class="btn btn-dark btn-block p-1 CustomeDate" runat="server"
                                            Text="Apply" OnClientClick="CustomDate()" OnClick="btnCustomdate_Click" ClientIDMode="Static" />
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                    <!-- Data Table-->
                    <div class="table-responsive">
                        <table class="table table-bordered text-small" id="dataTable" width="100%" style="color: #56549b"
                            cellspacing="0">
                            <thead>
                                <tr>
                                    <th>
                                        Sr. No.
                                    </th>
                                    <th>
                                        Patient Name
                                    </th>
                                    <th>
                                        Doctor Name
                                    </th>
                                    <th>
                                        Booking Date
                                    </th>
                                    <th>
                                        Appointment Date
                                    </th>
                                    <th>
                                        Amount
                                    </th>
                                    <th>
                                        Booked Via
                                    </th>
                                    <th>
                                        Status
                                    </th>
                                    <th>
                                        Appointment Type
                                    </th>
                                    <th>
                                        Action
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="tbodyTestBookList" runat="server"  style="text-align: center">
                            </tbody>
                        </table>
                    </div>
                    <!-- Content Row -->
                    <div class="d-sm-flex align-items-center justify-content-between mb-4">
                        <h4 class="h6 m-0 P-0 font-weight-bold text-dark">
                            Analytics Graph</h4>
                    </div>
                    <!-- Graph content row1 -->
                    <div class="row">
                        <!--Charts for Test Count Report -->
                        <div class="col-xl-4 col-lg-4 box1 size">
                            <div class="card shadow mb-4">
                                <!-- Card Header - Dropdown -->
                                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                                    <h6 class="m-0 text-primary">
                                        Test Count Report</h6>
                                    <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="HiddenField2" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="HTestCountReportTestName" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="HTestCountReportTestCount" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="HTestCountReportTestSum" runat="server" ClientIDMode="Static" />
                                    <form class="form-inline ">
                                    <div class="input-group mx-0 mx-md-3">
                                        <select name="p" size="1" title="Department List" class="custom-select">
                                          <option value="pie1">Pie Chart</option>
                                            <option value="bar1">Bar Graph</option>
                                            <option value="line1">Line Graph</option>
                                            <option value="donut1">Doughnut Chart</option>
                                        </select>
                                    </div>
                                    </form>
                                </div>
                                <!-- Card Body -->
                                <div class="card-body">
                                    <div class="row d-flex flex-row justify-content-between flex-nowrap mx-2">
                                        <div class="form-group has-search mr-2">
                                            <span class="fa fa-search form-control-feedback hide"></span>
                                            <input type="text" id="myInputTextField" class="form-control hide" placeholder="Search for perticular test">
                                        </div>
                                        <div class="col-auto text-center border">
                                            <span class="text-xs">Total Count</span><br>
                                            <b><span id="spanTestTotalCount" runat="server" clientidmode="Static"></span></b>
                                        </div>
                                    </div>
                                    <div id="barChart" class="chart1 bar1">
                                        <div class="chart-bar pt-4 pb-2">
                                            <canvas id="myBarChart"></canvas>
                                        </div>
                                    </div>
                                    <div id="Div1" class="chart1 line1">
                                        <div class="chart-area pt-4 pb-2">
                                            <canvas id="testCountLineChart"></canvas>
                                        </div>
                                    </div>
                                    <div id="pieChart" class="chart1 pie1">
                                        <div class="chart-pie pt-4 pb-2">
                                            <canvas id="testCountPieChart"></canvas>
                                        </div>
                                        <div class="mt-4 text-center small">
                                            <span class="mr-2"><i class="fas fa-circle text-purple"></i><span id="spanTestName1"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-blue">
                                                </i><span id="spanTestName2" runat="server" clientidmode="Static"></span></span>
                                            <span class="mr-2"><i class="fas fa-circle text-orange"></i><span id="spanTestName3"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-parot">
                                                </i><span id="spanTestName4" runat="server" clientidmode="Static"></span></span>
                                            <span class="mr-2"><i class="fas fa-circle text-pcblue"></i><span id="spanTestName5"
                                                runat="server" clientidmode="Static"></span></span>
                                        </div>
                                    </div>
                                    <div id="pieChart" class="chart1 donut1">
                                        <div class="chart-pie pt-4 pb-2">
                                            <canvas id="testCountDonutChart"></canvas>
                                        </div>
                                        <div class="mt-4 text-center small">
                                            <span class="mr-2"><i class="fas fa-circle text-purple"></i><span id="TCRspanTestName1"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-blue">
                                                </i><span id="TCRspanTestName2" runat="server" clientidmode="Static"></span>
                                            </span><span class="mr-2"><i class="fas fa-circle text-orange"></i><span id="TCRspanTestName3"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-parot">
                                                </i><span id="TCRspanTestName4" runat="server" clientidmode="Static"></span>
                                            </span><span class="mr-2"><i class="fas fa-circle text-pcblue"></i><span id="TCRspanTestName5"
                                                runat="server" clientidmode="Static"></span></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--Charts for Revenue -->
                        <div class="col-xl-4 col-lg-4 box2 size">
                            <div class="card shadow mb-4">
                                <!-- Card Header - Dropdown -->
                                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                                    <h6 class="m-0 text-primary">
                                        Revenue Chart</h6>
                                    <asp:HiddenField ID="HTestRevenuCountTestName" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="HTestRevenuCountTestCount" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="HTestRevenuCountTestSum" runat="server" ClientIDMode="Static" />
                                    <form class="form-inline ">
                                    <div class="input-group mx-0 mx-md-3">
                                        <select name="p" size="1" title="Department List" class="custom-select">
                                             <option value="pie2">Pie Chart</option>
                                            <option value="bar2">Bar Graph</option>
                                            <option value="donut2">Doughnut Chart</option>
                                            <option value="line2">Line Chart</option>
                                        </select>
                                    </div>
                                    </form>
                                </div>
                                <!-- Card Body -->
                                <div class="card-body">
                                    <div class="row d-flex flex-row justify-content-between flex-nowrap mx-2">
                                        <div class="form-group has-search mr-2">
                                            <span class="fa fa-search form-control-feedback hide"></span>
                                            <input type="text" id="myInputTextField" class="form-control hide" placeholder="Search for Revenue..">
                                        </div>
                                        <div class="col-auto text-center border">
                                            <span class="text-xs">Total Revenue</span><br>
                                            <b><span id="spanTestTotalRevenu" runat="server" clientidmode="Static"></span></b>
                                        </div>
                                    </div>
                                    <div id="pieChart" class="chart2 pie2">
                                        <div class="chart-pie pt-4 pb-2">
                                            <canvas id="RevenuePieChart"></canvas>
                                        </div>
                                        <div class="mt-4 text-center small">
                                            <span class="mr-2"><i class="fas fa-circle text-purple"></i><span id="spanTestRevenuCount1"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-blue">
                                                </i><span id="spanTestRevenuCount2" runat="server" clientidmode="Static"></span>
                                            </span><span class="mr-2"><i class="fas fa-circle text-orange"></i><span id="spanTestRevenuCount3"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-parot">
                                                </i><span id="spanTestRevenuCount4" runat="server" clientidmode="Static"></span>
                                            </span><span class="mr-2"><i class="fas fa-circle text-pcblue"></i><span id="spanTestRevenuCount5"
                                                runat="server" clientidmode="Static"></span></span>
                                        </div>
                                    </div>
                                    <div id="pieChart" class="chart2 donut2">
                                        <div class="chart-pie pt-4 pb-2">
                                            <canvas id="RevenueDonutChart"></canvas>
                                        </div>
                                        <div class="mt-4 text-center small">
                                            <span class="mr-2"><i class="fas fa-circle text-purple"></i><span id="TCRspanTestRevenuCount1"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-blue">
                                                </i><span id="TCRspanTestRevenuCount2" runat="server" clientidmode="Static"></span>
                                            </span><span class="mr-2"><i class="fas fa-circle text-orange"></i><span id="TCRspanTestRevenuCount3"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-parot">
                                                </i><span id="TCRspanTestRevenuCount4" runat="server" clientidmode="Static"></span>
                                            </span><span class="mr-2"><i class="fas fa-circle text-pcblue"></i><span id="TCRspanTestRevenuCount5"
                                                runat="server" clientidmode="Static"></span></span>
                                        </div>
                                    </div>
                                    <div id="LineChart" class="chart2 line2">
                                        <div class="chart-area pt-4 pb-2">
                                            <canvas id="RevenueLineChart"></canvas>
                                        </div>
                                    </div>
                                    <div id="BarChart" class="chart2 bar2">
                                        <div class="chart-bar pt-4 pb-2">
                                            <canvas id="RevenueBarChart"></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- chart for Paid vs Due -->
                        <div class="col-xl-4 col-lg-4 box3 size">
                            <div class="card shadow mb-4">
                                <!-- Card Header - Dropdown -->
                                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                                    <h6 class="m-0 text-primary">
                                        Paid Vs Dues Chart
                                    </h6>
                                    <asp:HiddenField ID="HPaymentStatus" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="HPaymentAmount" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="HPaymentTotalSum" runat="server" ClientIDMode="Static" />
                                    <form class="form-inline ">
                                    <div class="input-group mx-0 mx-md-3">
                                        <select name="p" size="1" title="Department List" class="custom-select">
                                              <option value="pie3">Pie Chart</option>
                                            <option value="bar3">Bar Graph</option>
                                            <option value="donut3">Doughnut Chart</option>
                                            <option value="line3">Line Chart</option>
                                        </select>
                                    </div>
                                    </form>
                                </div>
                                <!-- Card Body -->
                                <div class="card-body">
                                    <div class="row d-flex flex-row justify-content-between flex-nowrap mx-2">
                                        <div class="form-group has-search mr-2">
                                            <span class="fa fa-search form-control-feedback hide"></span>
                                            <input type="text" id="myInputTextField" class="form-control hide" placeholder="Search ..">
                                        </div>
                                        <div class="col-auto text-center border">
                                            <span class="text-xs">Total Revenue</span><br>
                                            <b><span id="spanTotalSum" runat="server" clientidmode="Static"></span></b>
                                        </div>
                                    </div>
                                    <div id="donutChart" class="chart3 donut3">
                                        <div class="chart-pie pt-4 pb-2">
                                            <canvas id="PaidVsDueDonutChart"></canvas>
                                        </div>
                                        <div class="mt-4 text-center small">
                                            <span class="mr-2"><i class="fas fa-circle text-dgreen"></i>Paid </span><span class="mr-2">
                                                <i class="fas fa-circle text-orange"></i>Due </span>
                                        </div>
                                    </div>
                                    <div id="pieChart" class="chart3 pie3">
                                        <div class="chart-pie pt-4 pb-2">
                                            <canvas id="PaidVsDuePieChart"></canvas>
                                        </div>
                                        <div class="mt-4 text-center small">
                                            <span class="mr-2"><i class="fas fa-circle text-dgreen"></i>Paid </span><span class="mr-2">
                                                <i class="fas fa-circle text-orange"></i>Due </span>
                                        </div>
                                    </div>
                                    <div id="BarChart" class="chart3 bar3">
                                        <div class="chart-bar pt-4 pb-2">
                                            <canvas id="PaidVsDuesBarChart"></canvas>
                                        </div>
                                    </div>
                                    <div id="lineChart" class="chart3 line3">
                                        <div class="chart-area pt-4 pb-2">
                                            <canvas id="PaidVsDuesLineChart"></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Graph content row2 -->
                    <div class="row">
                        <!--Charts for Test Count Report -->
                                <div class="col-xl-4 col-lg-4 box4 size">
                            <div class="card shadow mb-4">
                                <!-- Card Header - Dropdown -->
                                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                                    <h6 class="m-0 text-primary">
                                        Males Vs Female Chart</h6>
                                    <asp:HiddenField ID="HGender" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="HTestCountGender" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="HTotalGenderTestCount" runat="server" ClientIDMode="Static" />
                                    <form class="form-inline ">
                                    <div class="input-group mx-0 mx-md-3">
                                        <select name="p" size="1" title="Department List" class="custom-select">
                                            <option value="pie4">Pie Chart</option>
                                            <option value="bar4">Bar Graph</option>
                                            <option value="line4">Line Chart</option>
                                            <option value="donut4">Donut Chart</option>
                                        </select>
                                    </div>
                                    </form>
                                </div>
                                <!-- Card Body -->
                                <div class="card-body">
                                    <div class="row d-flex flex-row justify-content-between flex-nowrap mx-2">
                                        <div class="form-group has-search mr-2">
                                            <span class="fa fa-search form-control-feedback hide"></span>
                                            <input type="text" id="Text2" class="form-control hide" placeholder="Search for perticular test">
                                        </div>
                                        <div class="col-auto text-center border">
                                            <span class="text-xs">Total Count</span><br>
                                            <b><span id="spanTotalTestCountGenderwise" runat="server" clientidmode="Static"></span>
                                            </b>
                                        </div>
                                    </div>
                                    <div id="Div6" class="chart4 bar4">
                                        <div class="chart-bar pt-4 pb-2">
                                            <canvas id="MaleVsFemaleBarChart"></canvas>
                                        </div>
                                    </div>
                                    <div id="Div7" class="chart4 pie4">
                                        <div class="chart-pie pt-4 pb-2">
                                            <canvas id="MaleVsFemalePieChart"></canvas>
                                        </div>
                                        <div class="mt-4 text-center small">
                                            <span class="mr-2"><i class="fas fa-circle text-purple"></i><span id="spanGender1"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-pcblue">
                                                </i><span id="spanGender2" runat="server" clientidmode="Static"></span></span>
                                        </div>
                                    </div>
                                    <div id="Div8" class="chart4 donut4">
                                        <div class="chart-pie pt-4 pb-2">
                                            <canvas id="MaleVsFemaleDonutChart"></canvas>
                                        </div>
                                        <div class="mt-4 text-center small">
                                            <span class="mr-2"><i class="fas fa-circle text-purple"></i><span id="PiespanGender1"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-pcblue">
                                                </i><span id="PiespanGender2" runat="server" clientidmode="Static"></span></span>
                                        </div>
                                    </div>
                                    <div id="Div9" class="chart4 line4">
                                        <div class="chart-area pt-4 pb-2">
                                            <canvas id="MaleVsFemaleLineChart"></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- chart for Paid vs Due -->
                        <div class="col-xl-4 col-lg-4 box6 size">
                            <div class="card shadow mb-4">
                                <!-- Card Header - Dropdown -->
                                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                                    <h6 class="m-0 text-primary">
                                        Ref. Doctor wise business chart
                                    </h6>
                                    <asp:HiddenField ID="HDocName" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="HDocAmount" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="HDocTotalAmount" runat="server" ClientIDMode="Static" />
                                    <form class="form-inline ">
                                    <div class="input-group mx-0 mx-md-3">
                                        <select name="p" size="1" title="Department List" class="custom-select">
                                            <option value="pie6">Pie Chart</option>
                                            <option value="bar6">Bar Graph</option>                                           
                                            <option value="line6">Line Chart</option>
                                            <option value="donut6">Doughnut Chart</option>
                                        </select>
                                    </div>
                                    </form>
                                </div>
                                <!-- Card Body -->
                                <div class="card-body">
                                    <div class="row d-flex flex-row justify-content-between flex-nowrap mx-2">
                                        <div class="form-group has-search mr-2">
                                            <span class="fa fa-search form-control-feedback hide"></span>
                                            <input type="text" id="Text1" class="form-control hide" placeholder="Search for Revenue..">
                                        </div>
                                        <div class="col-auto text-center border">
                                            <span class="text-xs">Total Count</span><br>
                                            <b><span id="spanDocTotalSum" runat="server" clientidmode="Static"></span></b>
                                        </div>
                                    </div>
                                    <div id="Div2" class="chart6 pie6">
                                        <div class="chart-pie pt-4 pb-2">
                                            <canvas id="refDocBarPieChart"></canvas>
                                        </div>
                                        <div class="mt-4 text-center small">
                                            <span class="mr-2"><i class="fas fa-circle text-purple"></i><span id="spanRefDoc1"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-blue">
                                                </i><span id="spanRefDoc2" runat="server" clientidmode="Static"></span></span>
                                            <span class="mr-2"><i class="fas fa-circle text-orange"></i><span id="spanRefDoc3"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-parot">
                                                </i><span id="spanRefDoc4" runat="server" clientidmode="Static"></span></span>
                                            <span class="mr-2"><i class="fas fa-circle text-pcblue"></i><span id="spanRefDoc5"
                                                runat="server" clientidmode="Static"></span></span>
                                        </div>
                                    </div>
                                    <div id="Div3" class="chart6 bar6">
                                        <div class="chart-area pt-4 pb-2">
                                            <canvas id="refDocBarChart"></canvas>
                                        </div>
                                    </div>
                                    <div id="Div4" class="chart6 donut6">
                                        <div class="chart-area pt-4 pb-2">
                                            <canvas id="refDocDonutChart"></canvas>
                                        </div>
                                        <div class="mt-4 text-center small">
                                            <span class="mr-2"><i class="fas fa-circle text-purple"></i><span id="DonutspanRefDoc1"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-blue">
                                                </i><span id="DonutspanRefDoc2" runat="server" clientidmode="Static"></span>
                                            </span><span class="mr-2"><i class="fas fa-circle text-orange"></i><span id="DonutspanRefDoc3"
                                                runat="server" clientidmode="Static"></span></span><span class="mr-2"><i class="fas fa-circle text-parot">
                                                </i><span id="DonutspanRefDoc4" runat="server" clientidmode="Static"></span>
                                            </span><span class="mr-2"><i class="fas fa-circle text-pcblue"></i><span id="DonutspanRefDoc5"
                                                runat="server" clientidmode="Static"></span></span>
                                        </div>
                                    </div>
                                    <div id="Div5" class="chart6 line6">
                                        <div class="chart-area pt-4 pb-2">
                                            <canvas id="refDocLineChart"></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--Charts for Revenue -->
                        
                    </div>
                </div>
                <!-- /.container-fluid -->
            </div>
        </div>
        <!-- End of Main Content -->
        <!-- End of Footer -->
    </div>
    <!-- End of Content Wrapper -->
    <!-- End of Page Wrapper -->
    <!-- Scroll to Top Button-->
    <a class="scroll-to-top rounded" href="#wrapper"><i class="fas fa-angle-up"></i>
    </a>
    <!-- Logout Modal-->
    <div class="modal fade" id="logoutModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">
                        Ready to Leave?</h5>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    Select "Logout" below if you are ready to end your current session.</div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" type="button" data-dismiss="modal">
                        Cancel</button>
                    <a class="btn btn-primary" href="login.html">Logout</a>
                </div>
            </div>
        </div>
    </div>
    <!-- Bootstrap core JavaScript-->
    <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <!-- Core plugin JavaScript-->
    <script src="Content/vendor/jquery-easing/jquery.easing.min.js"></script>
    <!-- Custom scripts for all pages-->
    <script src="Content/js/sb-admin-2.min.js"></script>
    <!-- Page level plugins -->
    <script src="Content/vendor/chart.js/Chart.js"></script>
    <!-- Page level custom scripts -->
    <script src="Content/js/demo/datatables.js"></script>
    <script src="Content/js/common.js"></script>
    <script src="Content/js/card/totalTest-chart-doughnut.js"></script>
    <script src="Content/js/card/pendingTest-chart-doughnut.js"></script>
    <script src="Content/js/card/CompletedReport-chart-doughnut.js"></script>
    <script src="Content/js/card/deliveryReport-chart-doughnut.js"></script>
    <!-- Page Test Count scripts -->
    <script src="Content/js/demo/TestCount-chart-bar.js"></script>
    <script src="Content/js/demo/TestCount-chart-pie.js"></script>
    <script src="Content/js/demo/TestCount-chart-doughnut.js"></script>
    <script src="Content/js/demo/TestCount-chart-line.js"></script>
    <!-- Page Revenue scripts -->
    <script src="Content/js/demo/Revenue-chart-pie.js"></script>
    <script src="Content/js/demo/Revenue-chart-doughnut.js"></script>
    <script src="Content/js/demo/Revenue-chart-bar.js"></script>
    <script src="Content/js/demo/Revenue-chart-line.js"></script>
    <!-- Page PaidVsDues scripts -->
    <script src="Content/js/demo/PaidVsDues-chart-pie.js"></script>
    <script src="Content/js/demo/PaidVsDues-chart-doughnut.js"></script>
    <script src="Content/js/demo/PaidVsDues-chart-bar.js"></script>
    <script src="Content/js/demo/PaidVsDues-chart-line.js"></script>
    <!-- Page MaleVsFemale scripts -->
    <script src="Content/js/demo/MvF-chart-pie.js"></script>
    <script src="Content/js/demo/MvF-chart-bar.js"></script>
    <script src="Content/js/demo/MvF-chart-line.js"></script>
    <script src="Content/js/demo/MvF-chart-doughnut.js"></script>
    <!-- Page AgeGroup scripts -->
    <script src="Content/js/demo/AgeGroup-chart-line.js"></script>
    <script src="Content/js/demo/AgeGroup-chart-bar.js"></script>
    <script src="Content/js/demo/AgeGroup-chart-pie.js"></script>
    <script src="Content/js/demo/AgeGroup-chart-doughnut.js"></script>
    <!-- Page RefDoc scripts -->
    <script src="Content/js/demo/RefDoc-chart-bar.js"></script>
    <script src="Content/js/demo/RefDoc-chart-pie.js"></script>
    <script src="Content/js/demo/RefDoc-chart-doughnut.js"></script>
    <script src="Content/js/demo/RefDoc-chart-line.js"></script>
    <script type="text/javascript">
        $(".oneweek").click(function () {

            location.reload();
        });

        $(".oneMonth").click(function () {

            location.reload();
        });

        $(".oneYear").click(function () {

            location.reload();
        });
        $(".CustomeDate").click(function () {

            location.reload();
        });
    </script>
    <script type="text/javascript">
        function CustomDate() {
            $("#HstartDate").val($('#from').val());
            $("#HendDate").val($('#to').val());
        }
    </script>
</asp:Content>

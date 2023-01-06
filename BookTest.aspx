 <%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="BookTest.aspx.cs" Inherits="BookTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>


	
	<link href="css/date.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrapss.min.css" rel="stylesheet" type="text/css" />
    <link href="css/BookTest.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .color
        {
            background-color: #777;
        }
        .fade.active.in
        {
            display: block;
        }
        .fade
        {
            display: none;
        }
        .input-left, .input-right
        {
            padding: 0;
        }
        .input-left input
        {
            border-right: none;
            border-radius-right: 0px;
        }
        .input-right input
        {
            border-left: none;
            border-radius-left: 0px;
            position: relative;
            right: 5px;
            background-color: #fff !important;
        }
        .addtest label::before
        {
            content: none;
        }
        #profilelist
        {
            height: 250px;
            overflow: auto;
        }
        .adjustloader
        {
            position: absolute;
            top: 42%;
            left: 43%;
            z-index: 999;
        }
        #ContentPlaceHolder1_imgPopup2
        {
            position: relative;
            left: 100%;
            bottom: 30px;
            z-index: 99;
        }
        .active11
        {
            background-color: #cdcdcd;
        }
        .test-list span
        {
            float: right;
            color: #195bb4;
        }
        .testtotal p
        {
            float: left;
        }
        .testtotal span
        {
            float: right;
        }
        .hide
        {
            display: none !important;
        }
        /*.overflow{height: 300px;overflow: auto;overflow-x: hidden;display: block;width: 100%}*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="wrapper">
        <div id="content">
            <div class="wrappercontent testwrap">
                <%-- select patient --%>
                <div id="divPatient" class="testbook first">
                    <asp:HiddenField ID="hiddenPatientId" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hiddenPatientName" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hiddenPatientGender" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hiddenPatientMobile" runat="server" ClientIDMode="Static" />
                    <nav class="navbar navbar-expand-sm navbar-header">
                           <div class="container-fluid">
                                <div class="navbar-title ml-5">
                                     <a href="#" class="navbar-brand">Patient List</a>
                                </div>
                                <div class="mr-5">
                                    <ul class="navbar-nav ml-auto"> 
                                         <li class="nav-item ">
                                            <div class="search-box">
                                                   <input type="text" placeholder="Search" class="input" id="myPatient">
                                                  <div class="search-btn">
                                                    <i class="fa fa-search" aria-hidden="true" style="padding-top:40%"></i>
                                                  </div>
                                           </div>
                                         </li> 
                                         <li class="nav-item mr-3 pt-1">
                                                <a href="#" id="AddPatientbtn" runat="server" data-toggle="modal" data-target="#modalAddPatient"
                                    class="btn btn-color"><span class="fa fa-plus" aria-hidden="true"></span> Add Patient</a>
                                        </li>  
                                          <li class="nav-item pt-1 mr-3"> <a href="TestBookList.aspx"  class="btn btn-color previousbtn"><i class="fa fa-arrow-left mr-2" aria-hidden="true"></i>Back</a></li>    
                                         <li class="nav-item pt-1">
                                                  <button class="btn btn-color nextbtn"> Next <i class="fa fa-arrow-right mr-2" aria-hidden="true"></i></button>
                                         </li>
                                     </ul>
                             </div>
                         </div>
                    </nav>
                    <div class="container-fluid" style="display: none">
                        <div class="row">
                            <div class="bs-wizard" style="border-bottom: 0;">
                                <div class="col-xs-3 bs-wizard-step active">
                                    <div class="text-center bs-wizard-stepnum">
                                        1</div>
                                    <div class="progress">
                                        <div class="progress-bar">
                                        </div>
                                    </div>
                                    <a href="#" class="bs-wizard-dot"></a>
                                    <div class="bs-wizard-info text-center">
                                        Select Patient</div>
                                </div>
                                <div class="col-xs-3 bs-wizard-step disabled">
                                    <div class="text-center bs-wizard-stepnum">
                                        2</div>
                                    <div class="progress">
                                        <div class="progress-bar">
                                        </div>
                                    </div>
                                    <a href="#" class="bs-wizard-dot"></a>
                                    <div class="bs-wizard-info text-center">
                                        Select Doctor</div>
                                </div>
                                <div class="col-xs-3 bs-wizard-step disabled">
                                    <!-- complete -->
                                    <div class="text-center bs-wizard-stepnum">
                                        3</div>
                                    <div class="progress">
                                        <div class="progress-bar">
                                        </div>
                                    </div>
                                    <a href="#" class="bs-wizard-dot"></a>
                                    <div class="bs-wizard-info text-center">
                                        Select Test</div>
                                </div>
                                <div class="col-xs-3 bs-wizard-step disabled">
                                    <!-- complete -->
                                    <div class="text-center bs-wizard-stepnum">
                                        4</div>
                                    <div class="progress">
                                        <div class="progress-bar">
                                        </div>
                                    </div>
                                    <a href="#" class="bs-wizard-dot"></a>
                                    <div class="bs-wizard-info text-center">
                                        Select Date</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table_div">
                        <div class="container-fluid">
                            <div class="holder py-3">
                                <ul class="SteppedProgress">
                                    <li class="complete"><span>Select Patient</span></li>
                                    <li class=""><span>Select Doctor</span></li>
                                    <li class=""><span>Select Test</span></li>
                                    <li class=""><span>Select Date</span></li>
                                </ul>
                            </div>
                            <div class="card mt-2 mb-3">
                                <div class="card-body container">
                                    <h5 class="card-title">
                                        Petient Selected:</h5>
                                    <div class="row pb-2">
                                        <div class="col-md-3 text-size">
                                            ID: <span id="patid"></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Name: <span id="patname"></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Gender: <span id="patgender"></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Mobile: <span id="patmobile"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="PatientList">
                                <ul class="responsive-table">
                                    <li class="table-header">
                                        <div class="col col-1 text-center">
                                            Action</div>
                                        <div class="col col-2 text-center">
                                            Sr. No.</div>
                                        <div class="col col-3 text-center">
                                            Name</div>
                                        <div class="col col-4 text-center">
                                            Gender</div>
                                        <div class="col col-5 text-center">
                                            Mobile</div>
                                    </li>
                                    <div id="page">
                                        <asp:Literal ID="tbodyPatientList" runat="server"></asp:Literal></div>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="container">
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-8">
                                <nav class="pagination-container">  
                                    <ul class="pagination justify-content-center">
                                          <li id="previous-page" class="px-2"><a href="javascript:void(0)" aria-label=Previous><span aria-hidden=true>&laquo;</span></a></li>
                                     </ul>
                                </nav>
                            </div>
                            <div class="col-md-2">
                            </div>
                        </div>
                    </div>
                </div>
                <%--Select doctor  --%>
                <div id="divDoctor" class="testbook">
                    <nav class="navbar navbar-expand-sm navbar-header">
                           <div class="container-fluid">
                                    <div class="navbar-title ml-5">  <a href="#" class="navbar-brand">Doctor List</a></div>
                                     <div class="mr-5">
                                           <ul class="navbar-nav ml-auto"> 
                                                 <li class="nav-item ">
                                                   <div class="search-box">
                                                     <input type="text" placeholder="Search" class="input" id="myDoctor">
                                                     <div class="search-btn">
                                                       <i class="fa fa-search" aria-hidden="true" style="padding-top:40%"></i>
                                                     </div>
                                                  </div>
                                                 </li> 
                                                 <li class="nav-item mr-3 pt-1">
                                                        <a href="#" data-toggle="modal" id="HideAddbtn" runat="server" data-target="#modalAddDoctor"
                                                                           class="btn btn-color"><span class="fa fa-plus" aria-hidden="true"></span> Add Doctor</a>
                                                 </li>      
                                                  <li class="nav-item mr-3 pt-1"> <button class="btn btn-color prevbtn"><span><i class="fa fa-arrow-left mr-2"  area-hidden="true"></i></span>  Previous</button></li>
                                                 <li class="nav-item pt-1">
                                                                   <button class="btn btn-color nextbtn"><span>Next <i class="fa fa-arrow-right mr-2" area-hidden="true"></i></span></button>
                                                 </li>
                                           </ul>
                                  </div>
                           </div>
                     </nav>
                    <div class="table_div">
                        <div class="container-fluid">
                            <div class="holder py-3">
                                <ul class="SteppedProgress">
                                    <li class="complete"><span>Select Patient</span></li>
                                    <li class="complete"><span>Select Doctor</span></li>
                                    <li class=""><span>Select Test</span></li>
                                    <li class=""><span>Select Date</span></li>
                                </ul>
                            </div>
                            <div class="card mt-2 mb-3">
                                <div class="card-body container">
                                    <h5 class="card-title">
                                        Doctor Selected:</h5>
                                    <div class="row pb-2">
                                        <div class="col-md-3 text-size">
                                            ID: <span id="docid"></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Name: <span id="docname"></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Gender: <span id="docgender"></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Mobile: <span id="docmobile"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="DoctorList">
                                <ul class="responsive-table">
                                    <li class="table-header">
                                        <div class="col col-1 text-center">
                                            Action</div>
                                        <div class="col col-2 text-center">
                                            Sr. No.</div>
                                        <div class="col col-3 text-center">
                                            Name</div>
                                        <div class="col col-4 text-center">
                                            Gender</div>
                                        <div class="col col-5 text-center">
                                            Mobile</div>
                                        <div class="col col-6 text-center">
                                            Address</div>
                                        <div class="col col-7 text-center">
                                            Degree</div>
                                        <div class="col col-8 text-center">
                                            Specialization</div>
                                        <div class="col col-9 text-center">
                                            Clinic</div>
                                    </li>
                                    <div id="page1">
                                        <asp:Literal ID="tbodyDoctorList" runat="server"></asp:Literal></div>
                                </ul>
                            </div>
                        </div>
                    </div>
                    
                </div>
                <%-- select test --%>
                <div id="divTestList" class="testbook">
                    <asp:HiddenField ID="hiddenTestList" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hiddenTotalFees" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hiddenTestIdCodePrice" runat="server" ClientIDMode="Static" />
                    <nav class="navbar navbar-expand-sm navbar-header">
                           <div class="container-fluid">
                             <div class="navbar-title ml-5">
                               <a href="#" class="navbar-brand">Test List</a>
                             </div>
                            <div class="mr-5">
                                 <ul class="navbar-nav ml-auto">
                                   <li class="nav-item ">
                                     <div class="search-box">
                                     </div>
                                   </li>
                                      <li class="nav-item pt-1 mr-3">  <button class="btn btn-color prevbtn"><span><i class="fa fa-arrow-left mr-2" area-hidden="true"></i></span>
                                                                                               Previous</button></li>
                                   <li class="nav-item pt-1">
                                     <button class="btn btn-color nextbtn"><span>Next <i class="fa fa-arrow-right mr-2"area-hidden="true"></i></span>
                                                                          </button>
                                   </li>
                                 </ul>
                              </div>
                         </div>
                    </nav>
                    <div class="table_div">
                        <div class="container-fluid">
                            <div class="holder py-4">
                                <ul class="SteppedProgress">
                                    <li class="complete"><span>Select Patient</span></li>
                                    <li class="complete"><span>Select Doctor</span></li>
                                    <li class="complete"><span>Select Test</span></li>
                                    <li class=""><span>Select Date</span></li>
                                </ul>
                            </div>
                            <div class="card mt-2 mb-3">
                                <div class="card-body container">
                                    <h5 class="card-title">
                                        Test Selected:</h5>
                                    <div class="row pb-2">
                                        <div class="col-md-3 text-size">
                                            Test ID : <span id="pattestid"></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Name: <span id="pattestname"></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Total Price : <span id="pattesttotalprice"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- test accordion -->
                    <div class="container-fluid">
                        <div class="form-group hide">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="scrollmenu nav nav-tabs" id="sectionlist" runat="server">
                                    </div>
                                    <%--<input type="text" id="myInput" onkeyup="myFunction()" placeholder="Select Test (by Name)">--%>
                                    <input type="text" id="myInput" placeholder="Search by Test Name.." class="form-control">
                                    <input type="hidden" id="sectionid" />
                                    <div class="tabcontent" id="profilelist" runat="server" clientidmode="Static">
                                        <div id="tab1" class="tab-pane fade in active">
                                            <div class="panel-group test-content" id="accordion">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <div class="checkbox test-checkbox checkbox-info checkbox-circle">
                                                            <input id="checkbox" class="styled" type="checkbox" />
                                                            <label for="checkbox">
                                                                <h4 class="panel-title">
                                                                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion"
                                                                        href="#collapseOne" aria-expanded="false">1. Lipid Profile </a>
                                                                </h4>
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div id="collapseOne" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
                                                        <div class="panel-body testlist" id="searchlistdate">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <div class="checkbox test-checkbox checkbox-info checkbox-circle">
                                                            <input id="checkbox2" class="styled" type="checkbox">
                                                            <label for="checkbox2">
                                                                <h4 class="panel-title">
                                                                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion"
                                                                        href="#collapseTwo" aria-expanded="false">2. Thyroid Profile </a>
                                                                </h4>
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div id="collapseTwo" class="panel-collapse collapse" aria-expanded="false">
                                                        <div class="panel-body testlist">
                                                            <div class="checkbox test-checkbox checkbox-info checkbox-circle">
                                                                <input id="checkbox3" class="styled" type="checkbox">
                                                                <label for="checkbox3">
                                                                    T3
                                                                </label>
                                                            </div>
                                                            <div class="checkbox test-checkbox checkbox-info checkbox-circle">
                                                                <input id="checkbox4" class="styled" type="checkbox">
                                                                <label for="checkbox4">
                                                                    T4
                                                                </label>
                                                            </div>
                                                            <div class="checkbox test-checkbox checkbox-info checkbox-circle">
                                                                <input id="checkbox5" class="styled" type="checkbox">
                                                                <label for="checkbox5">
                                                                    TSH
                                                                </label>
                                                            </div>
                                                            <div class="checkbox test-checkbox checkbox-info checkbox-circle">
                                                                <input id="checkbox6" class="styled" type="checkbox">
                                                                <label for="checkbox6">
                                                                    TSA
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab2" class="tab-pane fade">
                                            <div class="panel-group test-content" id="accordion">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <div class="checkbox test-checkbox checkbox-info checkbox-circle">
                                                            <input id="checkbox" class="styled" type="checkbox">
                                                            <label for="checkbox">
                                                                <h4 class="panel-title">
                                                                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion"
                                                                        href="#collapseOne" aria-expanded="false">2.1. Lipid Profile </a>
                                                                </h4>
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div id="collapseOne" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
                                                        <div class="panel-body testlist">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <div class="checkbox test-checkbox checkbox-info checkbox-circle">
                                                            <input id="checkbox2" class="styled" type="checkbox">
                                                            <label for="checkbox2">
                                                                <h4 class="panel-title">
                                                                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion"
                                                                        href="#collapseTwo" aria-expanded="false">2. Thyroid Profile </a>
                                                                </h4>
                                                            </label>
                                                        </div>
                                                        <!--  -->
                                                    </div>
                                                    <div id="collapseTwo" class="panel-collapse collapse" aria-expanded="false">
                                                        <div class="panel-body testlist">
                                                            <div class="checkbox test-checkbox checkbox-info checkbox-circle">
                                                                <input id="checkbox3" class="styled" type="checkbox">
                                                                <label for="checkbox3">
                                                                    T3
                                                                </label>
                                                            </div>
                                                            <div class="checkbox test-checkbox checkbox-info checkbox-circle">
                                                                <input id="checkbox4" class="styled" type="checkbox">
                                                                <label for="checkbox4">
                                                                    T4
                                                                </label>
                                                            </div>
                                                            <div class="checkbox test-checkbox checkbox-info checkbox-circle">
                                                                <input id="checkbox5" class="styled" type="checkbox">
                                                                <label for="checkbox5">
                                                                    TSH
                                                                </label>
                                                            </div>
                                                            <div class="checkbox test-checkbox checkbox-info checkbox-circle">
                                                                <input id="checkbox6" class="styled" type="checkbox">
                                                                <label for="checkbox6">
                                                                    TSA
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tabcontent" id="Div2" runat="server" clientidmode="Static">
                                        <div id="Div3" class="tab-pane fade in active">
                                            <div class="panel-group test-content" id="Div4" style="height: 250px; overflow: auto;">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6" style="width: 45%">
                                    <br />
                                    <div class="addtest">
                                        <h5 class="primary-col">
                                            Add Test : (by Name or ID)</h5>
                                        <div class="test-list" id="totaltestlist">
                                        </div>
                                    </div>
                                    <div class="testtotal" style="margin-top: 10px; font-weight: bold">
                                        <p>
                                            Total Amount</p>
                                        <span id="alltesttotalprice"></span>
                                    </div>
                                    <%-- <div class="bottombtn">
                                        <button class="lab-btn-secondary prevbtn">
                                            Previous</button>
                                        <button class="lab-btn-primary nextbtn">
                                            Next</button>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:TextBox class="form-control" placeholder="Search test by test name" ID="txtSearchTest" style="display:none;"
                            runat="server" ClientIDMode="Static"></asp:TextBox>
                        <table class="table booking fixed-table table-bordered table-hover" id="dataTable">
                            <thead>
                                <tr>
                                    <th style="width:83px;">
                                        Action
                                    </th>
                                    <th style="width:132px;">
                                        Test Code
                                    </th>
                                    <th style="width:205px;">
                                        Test Name
                                    </th>
                                    <th style="width:233px;">
                                        Profile
                                    </th>
                                    <th>
                                        Section
                                    </th>
                                  
                                   
                                </tr>
                            </thead>
                            <tbody id="tbodyMyTestList" runat="server" clientidmode="Static">
                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="divDateTimeSlot" class="testbook last">
                    <asp:HiddenField ID="hiddenTestDate" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hiddenTimeSlot" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hiddenAppointmentType" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="HiddenDoctorid" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="HiddenDoctorName" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="HiddenDoctorGender" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="HiddenDoctorMobile" runat="server" ClientIDMode="Static" />
                    <nav class="navbar navbar-expand-sm navbar-header">
                          <div class="container-fluid">
                                 <div class="navbar-title ml-5">
                                     <a href="#" class="navbar-brand">Select Date</a>
                                  </div>
                                    <div class="pt-3"></div>
                                 <div class="mr-5" >
                                         <ul class="navbar-nav ml-auto">
                                                 <li class="nav-item pt-1 mr-3">
                                                     <div class="input-group mb-3">
                                                       <div class="input-group-prepend">
                                                         <span class="input-group-text fa-color" style="color: #91c740;"><i class="fa fa-calendar fa-fa-color"  aria-hidden="true"></i></span>
                                                       </div>
                                                        <asp:TextBox ID="txtTestDate" placeholder="Select test date" runat="server" class="form-control" onchange="DateSlot()" ClientIDMode="Static"
                                                                       Style="text-align: center"></asp:TextBox>
                                                                   <cc1:CalendarExtender ID="CalendarExtender2" CssClass="cal_Theme1" PopupButtonID="txtTestDate"
                                                                       runat="server" TargetControlID="txtTestDate" Format="dd/MM/yyyy" >
                                                                   </cc1:CalendarExtender>
                                                      
                                                     </div>
                                                 </li>
                                               
                                                 <li class="nav-item pt-1">
                                                   <button class="btn btn-color prevbtn"><span><i class="fa fa-arrow-left mr-2"
                                                         area-hidden="true"></i></span>Back</button>
                                                      
                                                 </li>
                                         </ul>
                                  </div>
                         </div>
                     </nav>
                    <div class="table_div">
                        <div class="container-fluid">
                            <div class="holder py-5">
                                <ul class="SteppedProgress">
                                    <li class="complete"><span>Select Patient</span></li>
                                    <li class="complete"><span>Select Doctor</span></li>
                                    <li class="complete"><span>Select Test</span></li>
                                    <li class="complete"><span>Select Date</span></li>
                                </ul>
                            </div>
                            <table class="table" id="tabLabSlots">
                                <thead class="table-header">
                                    <tr>
                                        <td>
                                            Action
                                        </td>
                                        <td>
                                            Day
                                        </td>
                                        <td>
                                            From
                                        </td>
                                        <td>
                                            To
                                        </td>
                                        <td>
                                            Appointment Type
                                        </td>
                                    </tr>
                                </thead>
                                <tbody id="tbodyLabSlots" runat="server" clientidmode="Static">
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <!-- FOOTER -->
                    <nav class="navbar navbar-expand-sm navbar-header">
                       <div class="container-fluid">
                            <div class="mr-5 ml-auto">
                                 <ul class="navbar-nav">
                                           <li class="nav-item">
                                             <input type="button" class="btn btn-submit" id="btnReviewBooking" clientidmode="Static" value="Review booking" />
                                          </li>
                                 </ul>
                            </div>
                        </div>
                     </nav>
                </div>
            </div>
            <asp:HiddenField ID="HFinalAmount" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="HAppoinmentType" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="HTestPrice" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="HEmergencyReport" runat="server" ClientIDMode="Static" />
            <!-- Confirm test booking Start-->
            <div id="modalConfirmBookTest" class="modal fade" role="dialog">
                <div class="modal-dialog" style="margin-top: 160px; padding-top: 30px">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <img id="imgloader" src="images/Loader.gif" alt="Loading" class="adjustloader hide" />
                        <div class="modal-header">
                            <h4 class="modal-title">
                                Book test</h4>
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Patient Name :
                                        <label id="lblPatientName" clientidmode="Static">
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Gender :
                                        <label id="lblPatientGender" clientidmode="Static">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Mobile :
                                        <label id="lblPatientMobile" clientidmode="Static">
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Test Date :
                                        <label id="lblTestDate" clientidmode="Static">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Time Slot :
                                        <label id="lblTimeSlot" clientidmode="Static">
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Doctor Name :
                                        <label id="lblDoctorName" clientidmode="Static">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <table class="table booking table-bordered table-hover" id="tabReviewTestList">
                                    <thead>
                                        <tr>
                                            <th>
                                                Test
                                            </th>
                                            <th>
                                                Price
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodyReviewTestList" runat="server" style="text-align: right" clientidmode="Static">
                                    </tbody>
                                    <tfoot style="text-align: right">
                                        <tr>
                                            <td>
                                                Collection Charge:
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" placeholder="Enter Collection Charge" onkeypress="return isNumber(event)"
                                                    ID="txtCollectionCharge" Style="text-align: right" MaxLength="4" runat="server"
                                                    ClientIDMode="Static"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Final Amount :
                                            </td>
                                            <td>
                                                <label id="lblFinalAmount" clientidmode="Static">
                                                </label>
                                            </td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmBooking" class="btn btn-color" runat="server" Text="Confirm"
                                OnClick="btnConfirmBooking_Click" ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- Confirm test booking End-->
            <!-- Modal Add Patient Start-->
            <asp:HiddenField ID="hiddenTestIdPrice" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hTestPricearray" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hMonth" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hDay" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hYear" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hiddenAction" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="hiddenAppUserId" runat="server" ClientIDMode="Static" />
           <div id="modalAddPatient" class="modal fade mt-5" role="dialog">
                <div class="modal-dialog" style="margin-top: 160px; padding-top: 30px">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                Add Patient</h4>
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Enter Mobile Number" onkeypress="return isNumber(event)"
                                            ID="txtMobile" MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                      
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Enter Full Name *" ID="txtFullName"
                                            runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblFullName" class="form-error">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Enter Email Id" ID="txtEmailId" runat="server"
                                            ClientIDMode="Static"></asp:TextBox>
                                      
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList class="form-control" ID="selGender" runat="server" ClientIDMode="Static">
                                            <asp:ListItem Value="select" Selected="True">Select Gender *</asp:ListItem>
                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                        </asp:DropDownList>
                                        <label id="lblGender" class="form-error">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row testDetail">
                                <div class="col-md-6">
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text fa-color" style="color: #91c740;"><i class="fa fa-calendar fa-fa-color"
                                                aria-hidden="true"></i></span>
                                        </div>
                                        <asp:TextBox ID="txtBirthDate" placeholder="Birth date(dd/MM/yyyy) *" runat="server" class="form-control"
                                            onchange="AgeCalulation()" ClientIDMode="Static"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Calendar1" CssClass="cal_Theme1" PopupButtonID="txtBirthDate"
                                            runat="server" TargetControlID="txtBirthDate" Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </div>
                                  
                                   
                                   
                                
                                </div>
                              <div class="col-md-6">
                                    <div class="form-group">
                                      <label id="lblage">
                                    </label>
                                        <asp:TextBox class="form-control" MaxLength="3" placeholder="Enter Age in Year *" onkeyup="GetBirthDate()"
                                            ID="txtyear" runat="server" ReadOnly="false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="txtState" class="form-control select2 select2-hidden-accessible"
                                            runat="server">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                            <asp:ListItem Value="Andaman and Nicobar Islands">	Andaman and Nicobar Islands	</asp:ListItem>
                                            <asp:ListItem Value="Andhra Pradesh">	Andhra Pradesh	</asp:ListItem>
                                            <asp:ListItem Value="Arunachal Pradesh">	Arunachal Pradesh	</asp:ListItem>
                                            <asp:ListItem Value="Assam">	Assam	</asp:ListItem>
                                            <asp:ListItem Value="Bihar">	Bihar	</asp:ListItem>
                                            <asp:ListItem Value="Chandigarh">	Chandigarh	</asp:ListItem>
                                            <asp:ListItem Value="Chattisgarh">	Chattisgarh	</asp:ListItem>
                                            <asp:ListItem Value="Dadra & Nagar Haveli and Daman & Diu">	Dadra & Nagar Haveli and Daman & Diu	</asp:ListItem>
                                            <asp:ListItem Value="Delhi">	Delhi	</asp:ListItem>
                                            <asp:ListItem Value="Goa">	Goa	</asp:ListItem>
                                            <asp:ListItem Value="Gujarat">	Gujarat	</asp:ListItem>
                                            <asp:ListItem Value="Haryana">	Haryana	</asp:ListItem>
                                            <asp:ListItem Value="Himachal Pradesh">	Himachal Pradesh	</asp:ListItem>
                                            <asp:ListItem Value="Jammu & Kashmir">	Jammu & Kashmir	</asp:ListItem>
                                            <asp:ListItem Value="Jharkhand">	Jharkhand	</asp:ListItem>
                                            <asp:ListItem Value="Karnataka">	Karnataka	</asp:ListItem>
                                            <asp:ListItem Value="Kerala">	Kerala	</asp:ListItem>
                                            <asp:ListItem Value="Ladakh">	Ladakh	</asp:ListItem>
                                            <asp:ListItem Value="Lakshadweep">	Lakshadweep	</asp:ListItem>
                                            <asp:ListItem Value="MadhyaPradesh">	MadhyaPradesh	</asp:ListItem>
                                            <asp:ListItem Value="Maharashtra">	Maharashtra	</asp:ListItem>
                                            <asp:ListItem Value="Manipur">	Manipur	</asp:ListItem>
                                            <asp:ListItem Value="Meghalaya">	Meghalaya	</asp:ListItem>
                                            <asp:ListItem Value="Mizoram">	Mizoram	</asp:ListItem>
                                            <asp:ListItem Value="Nagaland">	Nagaland	</asp:ListItem>
                                            <asp:ListItem Value="Odisha">	Odisha	</asp:ListItem>
                                            <asp:ListItem Value="Puducherry">	Puducherry	</asp:ListItem>
                                            <asp:ListItem Value="Punjab">	Punjab	</asp:ListItem>
                                            <asp:ListItem Value="Rajasthan">	Rajasthan	</asp:ListItem>
                                            <asp:ListItem Value="Sikkim">	Sikkim	</asp:ListItem>
                                            <asp:ListItem Value="Tamil Nadu">	Tamil Nadu	</asp:ListItem>
                                            <asp:ListItem Value="Telangana">	Telangana	</asp:ListItem>
                                            <asp:ListItem Value="Tripura">	Tripura	</asp:ListItem>
                                            <asp:ListItem Value="Uttar Pradesh">	Uttar Pradesh	</asp:ListItem>
                                            <asp:ListItem Value="Uttrakhand">	Uttrakhand	</asp:ListItem>
                                            <asp:ListItem Value="West Bengal">	West Bengal	</asp:ListItem>
                                        </asp:DropDownList>
                                      
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Enter Pincode " onkeypress="return isNumber(event)"
                                            ID="txtPincode" MaxLength="6" runat="server" ClientIDMode="Static"></asp:TextBox>
                                     
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" id="">
                                        <asp:TextBox class="form-control" Style="display: none" placeholder="Country" Text="India"
                                            ID="txtCountry" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblCountry" class="form-error hide">
                                        </label>
                                        <asp:TextBox class="form-control" placeholder="Enter City " ID="txtCity" runat="server"
                                            ClientIDMode="Static"></asp:TextBox>
                                     
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group" id="address">
                                        <asp:TextBox class="form-control" placeholder="Enter Address " ID="txtAddress" TextMode="MultiLine"
                                            Rows="2" runat="server" ClientIDMode="Static"></asp:TextBox>
                                       
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="upp" runat="server">
                            <ContentTemplate>
                                <div class="modal-footer">
                                    <asp:Button ID="btnAdd" class="btn btn-color" runat="server" Text="Submit" 
                                        OnClick="btnAdd_Click" />
                                </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upp">
                            <ProgressTemplate>
                                <div class="text form_loader">
                                    <img src="images/Loader.gif" alt="Loading">
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
            </div>
            </div>
            <input type="hidden" id="Parentid" />
            <!-- Modal Edit Patient Start-->
            <div id="modalEditPatient" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                                Edit Test Price</h4>
                        </div>
                        <div class="modal-body">
                            <div class="cus-form">
                                <div class="form-group">
                                    <asp:HiddenField ID="hiddenEditAction" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="HiddenMobileno" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hiddenEditAppUserId" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hiddenEditTestCodeId" runat="server" ClientIDMode="Static" />
                                    <b>Test Code: </b>
                                    <asp:Label ID="lblEditTestCode" runat="server" Text="" ClientIDMode="Static"></asp:Label>
                                </div>
                                <div class="">
                                    <div class="form-group">
                                        <b>Test Name: </b>
                                        <asp:Label ID="lblEditTestName" runat="server" Text="" ClientIDMode="Static"></asp:Label>
                                    </div>
                                </div>
                                <div class="">
                                    <div class="form-group">
                                        <b>Test Profile: </b>
                                        <asp:Label ID="lblEditTestProfile" runat="server" Text="" ClientIDMode="Static"></asp:Label>
                                    </div>
                                </div>
                                <div class="">
                                    <div class="form-group">
                                        <b>Test Section: </b>
                                        <asp:Label ID="lblEditTestSection" runat="server" Text="" ClientIDMode="Static"></asp:Label>
                                    </div>
                                </div>
                                <div class="">
                                    <div class="form-group">
                                        <b>Test Price: </b>
                                        <asp:TextBox class="form-control" placeholder="Price" ID="txtEditTestPrice" runat="server"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnEdit" class="lab-btn-secondary" runat="server" Text="Update" OnClick="btnEditTestPrice_Click"
                                ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal Add Doctor Start-->
            <div id="modalAddDoctor" class="modal fade " role="dialog">
                <div class="modal-dialog" style="margin-top: 160px; padding-top: 30px">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                Add Doctor</h4>
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class=" box box-primary">
                            <div class="box-header with-border">
                                <div class="modal-body">
                                    <asp:HiddenField ID="hiddenAction1" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hiddenAppUserId1" runat="server" ClientIDMode="Static" />
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Enter Mobile Number *" ID="txtMobile1"
                                                    onkeypress="return isNumber(event)" MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblMobile1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Enter Email Id" ID="txtEmailId1" runat="server"
                                                    ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblEmailId1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="input-group mb-3">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text fa-color" style="color: #91c740;"><i class="fa fa-calendar fa-fa-color"
                                                        aria-hidden="true"></i></span>
                                                </div>
                                                <asp:TextBox ID="txtBirthDate1" class="form-control" placeholder="Birth date (dd/mm/yyyy)"
                                                    runat="server" onchange="AgeDoctorCalulation()" ClientIDMode="Static"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" PopupButtonID="imgPopup1"
                                                    runat="server" TargetControlID="txtBirthDate1" Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                            </div>
                                            <label id="lblBirthDate1" class="form-error">
                                            </label>
                                            <label>
                                            </label>
                                            <label id="lblage1">
                                            </label>
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Degree *" ID="txtDegree1" runat="server"
                                                    ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblDegree1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <%-- <asp:TextBox class="form-control" placeholder="Specialization" ID="txtSpecialization1"
                                                    runat="server" ClientIDMode="Static"></asp:TextBox>--%>
                                                <asp:DropDownList class="form-control" ID="txtSpecialization1" runat="server" ClientIDMode="Static">
                                                    <asp:ListItem Value="select" Selected="True">Select Specialization *</asp:ListItem>
                                                    <asp:ListItem Value="Cardiologist">Cardiologist</asp:ListItem>
                                                    <asp:ListItem Value="Gastroenterologist">Gastroenterologist</asp:ListItem>
                                                    <asp:ListItem Value="Gynecologist">Gynecologist</asp:ListItem>
                                                    <asp:ListItem Value="Nephrologist">Nephrologist</asp:ListItem>
                                                    <asp:ListItem Value="Neurologist">Neurologist</asp:ListItem>
                                                    <asp:ListItem Value="Ophthalmologist">Ophthalmologist</asp:ListItem>
                                                    <asp:ListItem Value="Orthopedic">Orthopedic</asp:ListItem>
                                                    <asp:ListItem Value="Urologist">Urologist</asp:ListItem>
                                                    <asp:ListItem Value="Physician">Physician</asp:ListItem>
                                                    <asp:ListItem Value="Pulmonologist">Pulmonologist</asp:ListItem>
                                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                                </asp:DropDownList>
                                                <label id="lblSpecialization1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Clinic *" ID="txtClinic1" runat="server"
                                                    ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblClinic1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="col-lg-6 hide">
                                                <div class="form-group">
                                                    <asp:TextBox class="form-control" placeholder="Country" ID="txtCountry1" runat="server"
                                                        ClientIDMode="Static"></asp:TextBox>
                                                    <label id="lblCountry1" class="form-error">
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Enter Full Name *" ID="txtFullName1"
                                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblFullName1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:DropDownList class="form-control" ID="selGender1" runat="server" ClientIDMode="Static">
                                                    <asp:ListItem Value="select" Selected="True">Select Gender *</asp:ListItem>
                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                </asp:DropDownList>
                                                <label id="Label1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Address *" ID="txtAddress1" TextMode="MultiLine"
                                                    Rows="3" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblAddress1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:DropDownList ID="txtState1" class="form-control select2 select2-hidden-accessible"
                                                    runat="server">
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Value="Andaman and Nicobar Islands">	Andaman and Nicobar Islands	</asp:ListItem>
                                                    <asp:ListItem Value="Andhra Pradesh">	Andhra Pradesh	</asp:ListItem>
                                                    <asp:ListItem Value="Arunachal Pradesh">	Arunachal Pradesh	</asp:ListItem>
                                                    <asp:ListItem Value="Assam">	Assam	</asp:ListItem>
                                                    <asp:ListItem Value="Bihar">	Bihar	</asp:ListItem>
                                                    <asp:ListItem Value="Chandigarh">	Chandigarh	</asp:ListItem>
                                                    <asp:ListItem Value="Chattisgarh">	Chattisgarh	</asp:ListItem>
                                                    <asp:ListItem Value="Dadra & Nagar Haveli and Daman & Diu">	Dadra & Nagar Haveli and Daman & Diu	</asp:ListItem>
                                                    <asp:ListItem Value="Delhi">	Delhi	</asp:ListItem>
                                                    <asp:ListItem Value="Goa">	Goa	</asp:ListItem>
                                                    <asp:ListItem Value="Gujarat">	Gujarat	</asp:ListItem>
                                                    <asp:ListItem Value="Haryana">	Haryana	</asp:ListItem>
                                                    <asp:ListItem Value="Himachal Pradesh">	Himachal Pradesh	</asp:ListItem>
                                                    <asp:ListItem Value="Jammu & Kashmir">	Jammu & Kashmir	</asp:ListItem>
                                                    <asp:ListItem Value="Jharkhand">	Jharkhand	</asp:ListItem>
                                                    <asp:ListItem Value="Karnataka">	Karnataka	</asp:ListItem>
                                                    <asp:ListItem Value="Kerala">	Kerala	</asp:ListItem>
                                                    <asp:ListItem Value="Ladakh">	Ladakh	</asp:ListItem>
                                                    <asp:ListItem Value="Lakshadweep">	Lakshadweep	</asp:ListItem>
                                                    <asp:ListItem Value="MadhyaPradesh">	MadhyaPradesh	</asp:ListItem>
                                                    <asp:ListItem Value="Maharashtra">	Maharashtra	</asp:ListItem>
                                                    <asp:ListItem Value="Manipur">	Manipur	</asp:ListItem>
                                                    <asp:ListItem Value="Meghalaya">	Meghalaya	</asp:ListItem>
                                                    <asp:ListItem Value="Mizoram">	Mizoram	</asp:ListItem>
                                                    <asp:ListItem Value="Nagaland">	Nagaland	</asp:ListItem>
                                                    <asp:ListItem Value="Odisha">	Odisha	</asp:ListItem>
                                                    <asp:ListItem Value="Puducherry">	Puducherry	</asp:ListItem>
                                                    <asp:ListItem Value="Punjab">	Punjab	</asp:ListItem>
                                                    <asp:ListItem Value="Rajasthan">	Rajasthan	</asp:ListItem>
                                                    <asp:ListItem Value="Sikkim">	Sikkim	</asp:ListItem>
                                                    <asp:ListItem Value="Tamil Nadu">	Tamil Nadu	</asp:ListItem>
                                                    <asp:ListItem Value="Telangana">	Telangana	</asp:ListItem>
                                                    <asp:ListItem Value="Tripura">	Tripura	</asp:ListItem>
                                                    <asp:ListItem Value="Uttar Pradesh">	Uttar Pradesh	</asp:ListItem>
                                                    <asp:ListItem Value="Uttrakhand">	Uttrakhand	</asp:ListItem>
                                                    <asp:ListItem Value="West Bengal">	West Bengal	</asp:ListItem>
                                                </asp:DropDownList>
                                                <label id="lblState1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="City*" Text="Pune" ID="txtCity1" runat="server"
                                                    ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblCity1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Pincode *" ID="txtPincode1" onkeypress="return isNumber(event)"
                                                    MaxLength="6" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblPincode1" class="form-error">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="uppp1" runat="server">
                            <ContentTemplate>
                                <div class="modal-footer">
                                    <asp:Button ID="btnAddDoctor" class="btn btn-color" runat="server" Text="Submit"
                                        OnClientClick="javascript:return addDoctorValidateInBookTest()" OnClick="btnAddDoctor_Click"
                                        ClientIDMode="Static" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="uppp1">
                            <ProgressTemplate>
                                <div class="text form_loader">
                                    <img src="images/Loader.gif" alt="Loading">
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
            </div>
            <!-- Modal Add Doctor End-->
        </div>
    </div>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/code.js"></script>
    <script type="text/javascript" src="js/pagination.js"></script>
    <script type="text/javascript">
        // $("#txtBirthDate").focusout(function () {
        function AgeCalulation() {

            var sdob = document.getElementById("txtBirthDate").value;
            var date = sdob.split('/');
            var sDay = date[0];
            var sMonth = date[1];
            var sYear = date[2];
            var fulldob = sYear + "-" + sMonth + "-" + sDay;
            var now = new Date();
            var tday = now.getDate();
            var tmo = (now.getMonth() + 1);
            var tyr = (now.getFullYear());
            var snowday = tyr + "-" + tmo + "-" + tday;
            var age = tyr - sYear;
            if ((tmo < sMonth) || ((tmo == sMonth) && tday < sDay)) {
                age--;
            }
            // $("#lblage").html(age);
            $("#lblage").html(getAge(fulldob));
        }
        // });

        //*******************************Age Calculation Start**************//
        function getAge(dateString) {

            var mdate = dateString;
            var yearThen = parseInt(mdate.substring(0, 4), 10);
            var monthThen = parseInt(mdate.substring(5, 7), 10);
            var dayThen = parseInt(mdate.substring(8, 10), 10);

            var today = new Date();
            var birthday = new Date(yearThen, monthThen - 1, dayThen);

            var differenceInMilisecond = today.valueOf() - birthday.valueOf();

            var year_age = Math.floor(differenceInMilisecond / 31536000000);
            var day_age = Math.floor((differenceInMilisecond % 31536000000) / 86400000);

            if ((today.getMonth() == birthday.getMonth()) && (today.getDate() == birthday.getDate())) {
                alert("Happy B'day!!!");
            }

            var month_age = Math.floor(day_age / 30);

            day_age = day_age % 30;

            if (isNaN(year_age) || isNaN(month_age) || isNaN(day_age)) {
                $("#exact_age").text("Invalid birthday - Please try again!");
            }
            else {
                // $("#exact_age").html("You are<br/><span id=\"age\">" + year_age + " years " + month_age + " months " + day_age + " days</span> old");
            }
            var AgeCalulation = 0;
            if (year_age != 0) {
                AgeCalulation = year_age + ' years ';
            }
            else if (month_age != 0) {
                AgeCalulation = month_age + ' months ';
            }
            else if (day_age != 0) {
                AgeCalulation = day_age + ' days';
            }

            //  var age = years + ' years ' + months + ' months ' + days + ' days';

            var age = "Age : " + AgeCalulation;
            return age;
        }

        //****************************Age Calculation*****************************//


        $(document).ready(function () {
            $(".testbook").each(function (e) {
                if (e != 0)
                    $(this).hide();
            });

            $(".nextbtn").click(function () {
                if ($(".testbook:visible").next().length != 0) {
                    $(".testbook:visible").next().show().prev().hide();
                }
                else {
                    $(".testbook:visible").hide();
                    $(".testbook:first").show();
                }
                return false;
            });

            $(".prevbtn").click(function () {
                if ($(".testbook:visible").prev().length != 0) {
                    $(".testbook:visible").prev().show().next().hide();
                }
                else {
                    $(".testbook:visible").hide();
                    $(".testbook:last").show();
                }
                return false;
            });
        });

    </script>
    <script type="text/javascript" src="js/PatientListValidation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            // For Sorting TAble Column wise in table 

            $('th').click(function () {
                var table = $(this).parents('table').eq(0)
                var rows = table.find('tr:gt(0)').toArray().sort(comparer($(this).index()))
                this.asc = !this.asc
                if (!this.asc) { rows = rows.reverse() }
                for (var i = 0; i < rows.length; i++) { table.append(rows[i]) }
            })
            function comparer(index) {
                return function (a, b) {
                    var valA = getCellValue(a, index), valB = getCellValue(b, index)
                    return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.toString().localeCompare(valB)
                }
            }
            function getCellValue(row, index) { return $(row).children('td').eq(index).text() }

            $("#txtSearchTest").keyup(function () {

                var input, filter, table, tr, td, i;
                input = document.getElementById("txtSearchTest");
                filter = input.value.toUpperCase();
                table = document.getElementById("tbodyMyTestList");
                tr = table.getElementsByTagName("tr");

                for (i = 0; i < tr.length; i++) {
                    td = tr[i].getElementsByTagName("td")[2];

                    if (td) {
                        if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                            tr[i].style.display = "";
                        } else {
                            tr[i].style.display = "none";
                        }
                    }
                }
            });

            var testIds = new Array();
            var testIdCodePrice = new Array();
            var totalFees = 0;
            var pattestIds = new Array();
            var pattestIdCodePrice = new Array();
            var pattotalFees = new Array();

            var rows = $("#tbodyLabSlots").find("tr").hide();
            // $("#txtTestDate").on('input', function () {
            $("#txtTestDate").change(function () {
                //function DateSlot() {
                var testDate = $(this).val().split('/')[1] + "/" + $(this).val().split('/')[0] + "/" + $(this).val().split('/')[2];

                // alert(testDate);

                var datesplit = testDate.split("/");
                var day = datesplit[1];
                var month = datesplit[0];
                var year = datesplit[2];
                var ddmmyy = day + "/" + month + "/" + year;
                $("#hiddenTestDate").val(ddmmyy);

                rows = $("#tbodyLabSlots").find("tr").hide();
                if (isDate(testDate)) {
                    var weekday = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
                    var a = new Date(testDate);
                    var day = weekday[a.getDay()];

                    var filter, table, tr, td, i;

                    filter = day.toUpperCase();
                    table = document.getElementById("tbodyLabSlots");
                    tr = table.getElementsByTagName("tr");

                    for (i = 0; i < tr.length; i++) {
                        td = tr[i].getElementsByTagName("td")[1];

                        if (td) {
                            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                                tr[i].style.display = "";
                            } else {
                                tr[i].style.display = "none";
                            }
                        }
                    }
                    // $("#hiddenTestDate").val(testDate);
                }
                else {
                    //alert("invalid date");
                }
                //    }
            });

            $('#btnReviewBooking').on('click', function () {

                if ($("#hiddenPatientName").val() == "") {
                    alert("Please select patient");
                    return;
                }
                else if ($("#hiddenTestIdCodePrice").val() == "") {
                    alert("Please select tests");
                    return;
                }
                else if ($("#hiddenTestDate").val() == "") {
                    alert("Please select test date");
                    return;
                }
                else if ($("#hiddenTimeSlot").val() == "") {
                    alert("Please select time slot");
                    return;
                }

                $("#lblPatientName").text($("#hiddenPatientName").val());
                $("#lblPatientGender").text($("#hiddenPatientGender").val());
                $("#lblPatientMobile").text($("#hiddenPatientMobile").val());
                $("#lblTestDate").text($("#hiddenTestDate").val());
                $("#lblTimeSlot").text($("#hiddenTimeSlot").val());
                $("#lblDoctorName").text($("#HiddenDoctorName").val());
                $("#lblFinalAmount").text($("#hiddenTotalFees").val());
                $("#HFinalAmount").val($("#hiddenTotalFees").val());

                var testICP = $("#hiddenTestIdCodePrice").val().split(',');

                var testList = "";
                for (i = 0; i < testICP.length; i++) {
                    testList += "<tr><td>" + testICP[i].split('|')[2] + "</td><td>" + testICP[i].split('|')[1] + "</td></tr>";
                }
                testList += "<tr><td><b>Test Price<?b></td><td><b>" + $("#hiddenTotalFees").val() + "</b></td>";
                $("#tbodyReviewTestList").html(testList);

                //  var textbox = $('<input type="text" id="foo" name="foo">');

                $('#modalConfirmBookTest').modal("show");
            });

        });

        function patientSelect(obj) {
            //        obj = obj.rowIndex;
            //        alert(obj);
            $("#patDetails").removeClass("hide");
            $("#hiddenPatientId").val($(obj).val().split('|')[0]);
            $("#hiddenPatientName").val($(obj).val().split('|')[1]);
            $("#hiddenPatientGender").val($(obj).val().split('|')[2]);
            $("#hiddenPatientMobile").val($(obj).val().split('|')[3]);

            // Patient Details after Selectiong PAtient From List 

            $("#patid").text($(obj).val().split('|')[0]);
            $("#patname").text($(obj).val().split('|')[1]);
            $("#patgender").text($(obj).val().split('|')[2]);
            $("#patmobile").text($(obj).val().split('|')[3]);


            //alert($("#hiddenPatientId").val());
        }

        function doctorSelect(obj) {
            $("#docDetails").removeClass("hide");
            $("#HiddenDoctorid").val($(obj).val().split('|')[0]);
            $("#HiddenDoctorName").val($(obj).val().split('|')[1]);
            $("#hiddenDoctorGender").val($(obj).val().split('|')[2]);
            $("#hiddenDoctorMobile").val($(obj).val().split('|')[3]);

            // Patient Details after Selecting Doctor From List 

            $("#docid").text($(obj).val().split('|')[0]);
            $("#docname").text($(obj).val().split('|')[1]);
            $("#docgender").text($(obj).val().split('|')[2]);
            $("#docmobile").text($(obj).val().split('|')[3]);

            //alert($("#hiddenPatientId").val());
        }

        function timeSlotSelect(obj) {
            //$("#hiddenTestDate").val($("#txtTestDate").val());
            var value = $(obj).val().split("/");
            var timeslot = value[0];
            var Appointment = value[1];
            $("#hiddenAppointmentType").val(Appointment);
            $("#hiddenTimeSlot").val(timeslot);
            //alert($("#hiddenTestDate").val() + " : " + $("#hiddenTimeSlot").val());
        }
        function GetBirthDate() {
            var yr = $("#hYear").val();
            var mn = $("#hMonth").val();
            var day = $("#hDay").val();
            var y = $("#txtyear").val();
            var x = yr - y;

            var dob = day + "/" + mn + "/" + x;

            $("#txtBirthDate").val(dob);
            var age = y;
            var Finalage = "";
            if (age != "") {
                age += " years";
                Finalage = " Age :";
            }

            $("#lblage").text(Finalage + age)
        }
        function isDate(txtDate) {

            var currVal = txtDate;
            if (currVal == '')
                return false;

            //Declare Regex 
            var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
            var dtArray = currVal.match(rxDatePattern); // is format OK?

            if (dtArray == null)
                return false;

            //Checks for mm/dd/yyyy format.
            dtMonth = dtArray[1];
            dtDay = dtArray[3];
            dtYear = dtArray[5];

            if (dtMonth < 1 || dtMonth > 12 || dtMonth.length != 2)
                return false;
            else if (dtDay < 1 || dtDay > 31 || dtDay.length != 2)
                return false;
            else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
                return false;
            else if (dtMonth == 2) {
                var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));

                if (dtDay > 29 || (dtDay == 29 && !isleap))
                    return false;
            }
            else {
                var myDate = new Date(txtDate);
                var today = new Date();
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function runAjax(profileid) {
            var Datas = { sAction: "GetTestdetailsfromsectionid",
                Profileid: profileid
            }

            $.ajax({
                type: "GET",
                url: "service/getsearchtestforbooktest.ashx",
                data: Datas,
                dataType: "json",
                success: function (response) {
                    var obje = response;

                    var vale = "";
                    for (var i = 0; i < obje.length; i++) {

                        var TestCode = obje[i].sTestCode;
                        var TestId = obje[i].sTestId;
                        var TestName = obje[i].sTestName;
                        var ProfileName = obje[i].sProfileName;
                        var SectionName = obje[i].sSectionName;
                        var Price = obje[i].sPrice;
                        var MyTest = obje[i].sMyTest;
                        var testprofilei = obje[i].stestprofileid;

                        vale += "<div class='checkbox test-checkbox checkbox-info checkbox-circle'>"
                                                 + "<input id='" + TestId + "' value='" + TestId + "|" + Price + "|" + TestCode + "' class='styled' type='checkbox'>"
                                                 + "<label for='" + TestId + "'>"
                                                 + TestCode + ". " + TestName
                                                 + "</label>"
                                                 + "</div>";

                        // alert(TestCode);

                        $('#a' + testprofilei).append(vale);
                    }
                },
                error: function (error) {
                }
            });
        }

        function sectionclick(id) {
            $("#sectionid").val(id);
        }
        $("#sectionid").val(1);
    </script>
    <script type="text/javascript">
        //start a href click

        $("#tbodyMyTestList a").click(function () {
            if ($(this).attr("id")) {
                var id = $(this).attr("id");
                $("#hiddenEditTestCodeId").val($(this).attr("id"));
                $("#hiddenEditTestCodeId").val(id).html;
                $("#lblEditTestCode").text($("#testcode" + id).html());
                $("#lblEditTestName").text($("#testname" + id).html());
                $("#lblEditTestProfile").text($("#testProfileName" + id).html());
                $("#lblEditTestSection").text($("#testSectionName" + id).html());
                $("#txtEditTestPrice").val($("#testPrice" + id).html());
            }
        });
        //end a href click


        $(document).ready(function () {
            //reset modal fields on modal close
            $("#modalAddPatient").on("hidden.bs.modal", function () {
                $("#txtFullName").val("");
                $("#txtMobile").val("");
                $("#txtEmailId").val("");
                $("#selGender").val("select");
                $("#txtBirthDate").val("");
                $("#txtAddress").val("");
                $("#txtCountry").val("");
                //            $("#txtState").val("");
                //            $("#txtCity").val("");
                $("#txtPincode").val("");

                $("#lblage").html("");
                $("#lblFullName").html("");
                $("#lblMobile").html("");
                $("#lblEmailId").html("");
                $("#lblGender").html("");
                $("#lblBirthDate").html("");
                $("#lblAddress").html("");
                $("#lblCountry").html("");
                $("#lblState").html("");
                $("#lblCity").html("");
                $("#lblPincode").html("");
            });

            $("a.hidediv1").click(function () {
                $('#Div2').addClass('hide');
                $('#profilelist').removeClass('hide');
            });

            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                var tabidary = [1, 2, 3, 4];
                for (var i = 0; i < tabidary.length; i++) {

                    var tabid = "#tab" + tabidary[i] + " *";
                    var noresult = "#NoResult" + tabidary[i];
                    var cnt = 0;
                    $(tabid).filter(function () {
                        if ($(this).attr('class') != undefined) {
                            var cl = $(this).attr('class');
                            var trim = cl.substring(0, 19);
                            if (trim == "panel panel-default") {
                                $(this).toggle($(this).attr('class').toLowerCase().indexOf(value) > -1)
                                var dnone = $(this).css("display");

                                if (dnone == 'block') {
                                    cnt = cnt + 1;
                                }
                            }
                        }
                    });
                    // var resultcnt = $(divcnt).length;
                    //alert(cnt);
                    if (cnt == 0) {
                        $(noresult).show();
                    }
                    else {
                        $(noresult).hide();
                    }
                }
            });


            function runAjax(profileid) {

                var Datas = { sAction: "GetTestdetailsfromsectionid",
                    Profileid: profileid
                }

                $.ajax({
                    type: "GET",
                    url: "service/getsearchtestforbooktest.ashx",
                    data: Datas,
                    dataType: "json",
                    success: function (response) {
                        var obje = response;
                        for (var i = 0; i < obje.length; i++) {
                            var vale = "";
                            var TestCode = obje[i].sTestCode;
                            var TestId = obje[i].sTestId;
                            var TestName = obje[i].sTestName;
                            var ProfileName = obje[i].sProfileName;
                            var SectionName = obje[i].sSectionName;
                            var Price = obje[i].sPrice;
                            var MyTest = obje[i].sMyTest;
                            var testprofilei = obje[i].stestprofileid;
                            vale += "<div class='checkbox test-checkbox checkbox-info checkbox-circle'>"
                                               + "<input id='" + TestId + "' value='" + TestId + "|" + Price + "|" + TestCode + "' class='styled' type='checkbox'>"
                                               + "<label for='" + TestId + "'>"
                                               + TestCode + ". " + TestName
                                               + "</label>"
                                               + "</div>";
                            $('#a' + testprofilei).append(vale);
                        }
                    },

                    error: function (error) {
                    }
                });
            }
            var testIds = new Array();

            //  var testIds = new Array();
            var testIdCodePrice = new Array();
            var totalFees = 0;
            var pattestIds = new Array();
            var pattestIdCodePrice = new Array();
            var pattotalFees = new Array();
            var profileid = new Array();

            // for single test
            $(document).on("change", "input[type='checkbox']", function () {
                //$("input[type='checkbox']").change(function () {
                var testval = $(this).val().split('|');
                var profid = testval[4];
                var testcnt = testval[5];
                var tstid = testval[0];
                $('#Parentid').val(testval[3]);
                var cnt = 0;

                $("#testDetails").removeClass("hide");
                //alert(this);
                if ($(this).attr('name') != "profile") {
                    if ($(this).is(':checked')) {
                        testIds.push($(this).val().split('|')[0]);
                        totalFees += parseInt($(this).val().split('|')[1]);
                        testIdCodePrice.push($(this).val());
                        pattestIds.push($(this).val().split('|')[0]);
                        pattotalFees.push($(this).val().split('|')[1]);
                        //pattestIdCodePrice.push($(this).val().split('|')[2]);
                        pattestIdCodePrice.push($(this).val().split('|')[2]);
                        profileid.push($(this).val().split('|')[4]);
                        var testtt = "";
                        // alert(testIds[1]);
                        for (var k = 0; k < testIds.length; k++) {
                            testtt += "<div class=''> <label> " + pattestIdCodePrice[k] + " </label> <span> " + pattotalFees[k] + " </span></div>";
                        }
                        var checkid = 'checkbox' + $('#Parentid').val();

                        $('#' + checkid).prop('checked', true);
                        $("#totaltestlist").html(testtt);
                        $("#alltesttotalprice").text("₹ " + totalFees);
                    }
                    else {
                        if (tstid != undefined) {
                            var arrposition = 0;
                            for (var c = 0; c < testIds.length; c++) {
                                if (tstid == testIds[c]) {
                                    arrposition = c;
                                }
                            }

                            testIds.splice(arrposition, 1);
                            totalFees -= parseFloat($(this).val().split('|')[1]);
                            testIdCodePrice.splice(arrposition, 1);
                            profileid.splice(arrposition, 1);

                            var checkid = 'checkbox' + $('#Parentid').val();

                            pattestIds.splice(arrposition, 1);
                            pattotalFees.splice(arrposition, 1);
                            pattestIdCodePrice.splice(arrposition, 1);
                            //  pattestIdCodePrice.push($(this).val().split('|')[2]);
                        }
                        else {
                            testIds.splice(testIds.indexOf($(this).val().split('|')[0]), 1);
                            totalFees -= parseFloat($(this).val().split('|')[1]);
                            testIdCodePrice.splice(testIdCodePrice.indexOf($(this).val()), 1);
                            profileid.splice($(this).val().split('|')[4], 1);
                            var checkid = 'checkbox' + $('#Parentid').val();

                            pattestIds.splice(testIds.indexOf($(this).val().split('|')[0]), 1);
                            pattotalFees.splice(testIds.indexOf($(this).val().split('|')[1]), 1);
                            pattestIdCodePrice.splice(testIds.indexOf($(this).val().split('|')[2]), 1);
                            //pattestIdCodePrice.push($(this).val().split('|')[2]);
                        }

                        var testtt = "";
                        // alert(testIds[1]);
                        for (var k = 0; k < testIds.length; k++) {
                            //testtt += "<div class=''> <label> " + testIds[k] + ".  " + pattestIdCodePrice[k] + " </label> <span> " + pattotalFees[k] + " </span></div>";
                            testtt += "<div class=''> <label>" + pattestIdCodePrice[k] + " </label> <span> " + pattotalFees[k] + " </span></div>";

                            if (profid == profileid[k]) {
                                cnt = cnt + 1;
                            }
                        }
                        if (cnt == 0) {
                            //  $('#' + checkid).attr('checked', false);
                            $('#' + checkid).prop('checked', false);
                        }
                        $("#totaltestlist").html(testtt);
                        $("#alltesttotalprice").text("₹ " + totalFees);
                    }
                }
                $("#hiddenTestList").val(testIds.join(','));
                $("#hiddenTotalFees").val(totalFees);
                $("#hTestPricearray").val(pattotalFees.join(','));
                $("#hiddenTestIdCodePrice").val(testIdCodePrice);
                $("#pattestid").text(pattestIds.join(','));
                $("#pattesttotalprice").text(totalFees);
                $("#pattestname").text(pattestIdCodePrice);
            });

            $(document).on("click", "input[name='profile']", function () {
                var testval = $(this).val();
                var intTestid = new Array();
                var Data = { sAction: "SelectList",
                    sProfileid: testval
                };

                if ($(this).is(':checked')) {
                    var curntCheckbox = $(this).parent().parent().parent('div');
                    $('input[type=checkbox]', curntCheckbox).each(function () {

                        if ($(this).attr('name') != "profile") {
                            $(this).prop('checked', true);

                            var tid = $(this).val().split('|')[0];
                            profileid.push($(this).val().split('|')[4]);
                            testIds.push($(this).val().split('|')[0]);
                            totalFees += parseInt($(this).val().split('|')[1]);
                            testIdCodePrice.push($(this).val());

                            pattestIds.push($(this).val().split('|')[0]);
                            pattotalFees.push($(this).val().split('|')[1]);
                            //    pattestIdCodePrice.push($(this).val().split('|')[2]);
                            pattestIdCodePrice.push($(this).val().split('|')[2]);
                            // alert($(this).attr('id') + "+" + $(this).attr('name'));

                            if ($.inArray($(this).attr('id'), testIds) == -1)
                                testIds.push($(this).attr('id'));

                            var testtt = "";
                            // alert(testIds[1]);
                            for (var k = 0; k < testIds.length; k++) {
                                // testtt += "<div class=''> <label> " + testIds[k] + ".  " + pattestIdCodePrice[k] + " </label> <span> " + pattotalFees[k] + " </span></div>";
                                testtt += "<div class=''> <label>" + pattestIdCodePrice[k] + " </label> <span> " + pattotalFees[k] + " </span></div>";
                            }
                            $("#totaltestlist").html(testtt);
                            $("#alltesttotalprice").text("₹ " + totalFees);
                        }
                    });
                }
                else {
                    // alert("unchecked");
                    $('input[type=checkbox]', $(this).parent().parent().parent('div')).each(function () {
                        if ($(this).attr('name') != "profile") {
                            $(this).removeAttr('checked');

                            //  alert($(this).attr('id') + "+" + $(this).attr('name'));
                            for (var m = 0; m < profileid.length; m++) {
                                $(this).prop('checked', false);
                                if (profileid[m] == testval) {
                                    totalFees -= parseFloat(pattotalFees.splice(m, 1));
                                    testIds.splice(m, 1);
                                    testIdCodePrice.splice(m, 1);
                                    profileid.splice(m, 1);
                                    pattestIds.splice(m, 1);
                                    // pattotalFees.splice(m, 1);

                                    pattestIdCodePrice.splice(m, 1);
                                    //  pattestIdCodePrice.push($(this).val().split('|')[2]);

                                    if ($.inArray($(this).attr('id'), testIds) != -1)
                                        testIds.splice(testIds.indexOf($(this).attr('id')), 1);
                                }
                            }

                            var testtt = "";
                            // alert(testIds[1]);
                            for (var k = 0; k < testIds.length; k++) {
                                //testtt += "<div class=''> <label> " + testIds[k] + ".  " + pattestIdCodePrice[k] + " </label> <span> " + pattotalFees[k] + " </span></div>";
                                testtt += "<div class=''> <label>" + pattestIdCodePrice[k] + " </label> <span> " + pattotalFees[k] + " </span></div>";
                                // totalFees = -pattotalFees[k];
                            }
                            $("#totaltestlist").html(testtt);

                            $("#alltesttotalprice").text("₹ " + totalFees);
                        }
                    });
                }

                $("#hiddenTestList").val(testIds.join(','));
                $("#hiddenTotalFees").val(totalFees);
                $("#hiddenTestIdCodePrice").val(testIdCodePrice);
                $("#pattestid").text(pattestIds.join(','));
                $("#pattesttotalprice").text(totalFees);
                $("#pattestname").text(pattestIdCodePrice);
                $("#HTestPrice").val(totalFees);

                var testIdPrice = JSON.parse($("#hiddenTestIdPrice").val());

                var sum = 0;
                for (var i = 0; i < testIds.length; i++) {
                    sum += testIdPrice[testIds[i]];
                }

                $("#txtPackagePrice").val(sum);
                $("#txtPackageDiscountedPrice").val(sum);

                $("#hiddenTestId").val(testIds.join(','));
            });

            $("#txtMobile").focusout(function () {
                if ($("#txtMobile").val() != "") {
                    $("#lblMobile").html('');

                    var parameter = { "mobile": $("#txtMobile").val() };
                    $.ajax({
                        type: "POST",
                        url: "PatientList.aspx/searchRecord",
                        data: JSON.stringify(parameter),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var json = JSON.parse(response.d);
                            console.log(json);
                            //if patient does not exist
                            if (json["key"] == 0) {
                                $("#hiddenAction").val(0);
                                $("#hiddenAppUserId").val("");
                            }
                            //if patient exists and added in labs list
                            else if (json["key"] == 1) {
                                if ($("#txtMobile").val() != "") {
                                    $("#txtMobile").val("");
                                    $("#lblMobile").html("This patient already exists in your list");
                                }
                            }
                            //if patient exists but not added to list
                            else if (json["key"] == 2) {
                                $("#txtMobile").val(json["value"][0]["sMobile"]);
                                $("#txtFullName").val(json["value"][0]["sFullName"]);
                                $("#txtEmailId").val(json["value"][0]["sEmailId"]);

                                if (json["value"][0]["sGender"].toLowerCase() == "female") {
                                    $("#selGender").val("Female");
                                }
                                else if (json["value"][0]["sGender"].toLowerCase() == "male") {
                                    $("#selGender").val("Male");
                                }

                                $("#txtBirthDate").val(json["value"][0]["sBirthDate"]);
                                $("#txtAddress").val(json["value"][0]["sAddress"]);
                                $("#txtCountry").val(json["value"][0]["sCountry"]);
                                $("#txtState").val(json["value"][0]["sState"]);
                                $("#txtCity").val(json["value"][0]["sCity"]);
                                $("#txtPincode").val(json["value"][0]["sPincode"]);

                                $("#hiddenAction").val(2);
                                $("#hiddenAppUserId").val(json["value"][0]["sAppUserId"]);
                            }
                            console.log("success" + response.d);
                        },
                        error: function (response) {
                            console.log("error" + response.d);
                        },
                        failure: function (response) {
                            console.log("fail" + response.d);
                        }
                    });
                }
            });
        });

    
    </script>
    <!------Doctor js Start-------->
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/DoctorValidationBookTest.js"></script>
    <script type="text/javascript">
        var addclass = 'color';
        $("a.hidediv1").first().addClass(addclass);
        var $cols = $('.hidediv1').click(function (e) {
            $cols.removeClass(addclass);
            $(this).addClass(addclass);
        });

        $("#btnConfirmBooking").click(function () {

            $("#imgloader").removeClass('hide');

        });

        //  $("#txtBirthDate1").focusout(function () {
        function AgeDoctorCalulation() {
            var sdob = document.getElementById("txtBirthDate1").value;

            var date = sdob.split('/');
            var sDay = date[0];
            var sMonth = date[1];
            var sYear = date[2];
            // var fulldob = sYear + "/" + sMonth + "/" + sDay;
            var fulldob = sYear + "-" + sMonth + "-" + sDay;
            var now = new Date();
            var tday = now.getDate();
            var tmo = (now.getMonth() + 1);
            var tyr = (now.getFullYear());

            var snowday = tyr + "-" + tmo + "-" + tday;
            var age = tyr - sYear;
            if ((tmo < sMonth) || ((tmo == sMonth) && tday < sDay)) {
                age--;
            }
            // $("#lblage1").html(age);
            // alert(age);
            $("#lblage1").html(getAge(fulldob));
            //   });
        }

        $("#txtEditBirthDate1").focusout(function () {
            var sdob = document.getElementById("txtEditBirthDate1").value;
            var date = sdob.split('/');
            var sDay = date[0];
            var sMonth = date[1];
            var sYear = date[2];
            var fulldob = sYear + "-" + sMonth + "-" + sDay;
            var now = new Date();
            var tday = now.getDate();
            var tmo = (now.getMonth() + 1);
            var tyr = (now.getFullYear());

            var snowday = tyr + "-" + tmo + "-" + tday;
            var age = tyr - sYear;
            if ((tmo < sMonth) || ((tmo == sMonth) && tday < sDay)) {
                age--;
            }
            $("#lbleditage1").html(getAge(fulldob));
        });


        $(document).ready(function () {

            //reset modal fields on modal close
            $("#modalAddDoctor").on("hidden.bs.modal", function () {
                $("#txtFullName").val("");
                $("#txtMobile").val("");
                $("#txtEmailId").val("");
                $("#selGender").val("select");
                $("#txtBirthDate").val("");
                $("#txtAddress").val("");
                $("#txtDegree").val("");
                $("#txtSpecialization").val("");
                $("#txtClinic").val("");
                $("#txtCountry").val("");
                $("#txtPincode").val("");

                $("#lblage").html("");
                $("#lblFullName").html("");
                $("#lblMobile").html("");
                $("#lblEmailId").html("");
                $("#lblGender").html("");
                $("#lblBirthDate").html("");
                $("#lblAddress").html("");
                $("#lblDegree").val("");
                $("#lblSpecialization").val("");
                $("#lblClinic").val("");
                $("#lblCountry").html("");
                $("#lblState").html("");
                $("#lblCity").html("");
                $("#lblPincode").html("");
            });

            //reset modal fields on modal close for edit 
            $("#modalEditDoctor").on("hidden.bs.modal", function () {
                $("#txtEditFullName").val("");
                $("#txtEditMobile").val("");
                $("#txtEditEmailId").val("");
                $("#selEditGender").val("select");
                $("#txtEditBirthDate").val("");
                $("#txtEditAddress").val("");
                $("#txtEditDegree").val("");
                $("#txtEditSpecialization").val("");
                $("#txtEditClinic").val("");
                $("#txtEditCountry").val("");
                $("#txtEditState").val("");
                $("#txtEditCity").val("");
                $("#txtEditPincode").val("");

                $("#lbleditage").html("");
                $("#lblEditFullName").html("");
                $("#lblEditMobile").html("");
                $("#lblEditEmailId").html("");
                $("#lblEditGender").html("");
                $("#lblEditBirthDate").html("");
                $("#lblEditAddress").html("");
                $("#lblEditDegree").val("");
                $("#lblEditSpecialization").val("");
                $("#lblEditClinic").val("");
                $("#lblEditCountry").html("");
                $("#lblEditState").html("");
                $("#lblEditCity").html("");
                $("#lblEditPincode").html("");
            });

            //  $("#tbodyDoctorList a").click(function () {
            $("#tbodyDoctorList").on("click", "a", function () {

                if ($(this).attr("id")) {
                    var id = $(this).attr("id");
                    $("#hiddenEditAppUserId").val($(this).attr("id"));
                    $("#txthiddenEditAppUserId").val(id).html;

                    $("#lblEditTestCode").val($(this).closest("tr").find('td:eq(1)').text());
                    $("#lblEditTestName").val($("#email" + id).html());
                    $("#txtEditMobile").val($("#mobile" + id).html());
                    $("#txtEditAddress").val($("#address" + id).html());
                    $("#HiddenMobileno").val($("#hidenmobile" + id).html());
                    $("#txtEditDegree").val($("#degree" + id).html());
                    $("#txtEditSpecialization").val($("#specialize" + id).html());
                    $("#txtEditClinic").val($("#clinic" + id).html());
                    $("#selEditGender").val($("#gender" + id).text());
                    $("#txtEditBirthDate").val($("#dob" + id).html());
                    $("#txtEditCountry").val($("#country" + id).html());
                    $("#txtEditState").val($("#state" + id).html());
                    $("#txtEditCity").val($("#city" + id).html());
                    $("#txtEditPincode").val($("#pincode" + id).html());

                    var sdob = document.getElementById("txtEditBirthDate").value;

                    var date = sdob.split('/');
                    var sDay = date[0];
                    var sMonth = date[1];
                    var sYear = date[2];

                    var now = new Date();
                    var tday = now.getDate();
                    var tmo = (now.getMonth() + 1);
                    var tyr = (now.getFullYear());

                    var snowday = tyr + "-" + tmo + "-" + tday;
                    var age = tyr - sYear;
                    if ((tmo < sMonth) || ((tmo == sMonth) && tday < sDay)) {
                        age--;
                    }
                    $("#lbleditage").html(age);
                }
            });
            //end a href click

            $("#txtEditMobile").focusout(function () {

                var oldmobilenumber = $("#HiddenMobileno").val()
                var newmobilenumber = $("#txtEditMobile").val()

                if (oldmobilenumber != newmobilenumber) {
                    var parameter = { "mobile": $("#txtEditMobile").val() };
                    $.ajax({
                        type: "POST",
                        url: "DoctorList.aspx/searchRecord",
                        data: JSON.stringify(parameter),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var json = JSON.parse(response.d);

                            //if Doctor does not exist
                            if (json["key"] == 0) {
                                $("#hiddenEditAction").val(0);
                                $("#hiddenEditAppUserId").val("");
                            }
                            //if Doctor exists and added in labs list
                            else if (json["key"] == 1) {
                                $("#txtEditMobile").val("");
                                $("#lblEditMobile").html("This Doctor already exists in your list");
                            }
                            //if Doctor exists but not added to list
                            else if (json["key"] == 2) {
                                $("#txtEditMobile").val(json["value"][0]["sMobile"]);
                                $("#txtEditFullName").val(json["value"][0]["sFullName"]);
                                $("#txtEditEmailId").val(json["value"][0]["sEmailId"]);

                                if (json["value"][0]["sGender"].toLowerCase() == "female") {
                                    $("#selEditGender").val("Female");
                                }
                                else if (json["value"][0]["sGender"].toLowerCase() == "male") {
                                    $("#selEditGender").val("Male");
                                }

                                var date = json["value"][0]["sBirthDate"];

                                var d = new Date(date);
                                console.log(d);
                                var dd = d.getDate();
                                var mm = d.getMonth() + 1;
                                console.log(mm.toString().length);
                                mm = (mm.toString().length == 1) ? ('0' + mm.toString()) : mm;
                                console.log(mm);
                                var yy = d.getFullYear();
                                var bdate = dd + "/" + mm + "/" + yy;
                                console.log(bdate);
                                //$("#dob").val(bdate);

                                $("#txtEditBirthDate").val(bdate);
                                $("#txtEditAddress").val(json["value"][0]["sAddress"]);
                                $("#txtEditDegree").val(json["value"][0]["sDegree"]);
                                $("#txtEditSpecialization").val(json["value"][0]["sSpecialization"]);
                                $("#txtEditClinic").val(json["value"][0]["sClinic"]);
                                $("#txtEditCountry").val(json["value"][0]["sCountry"]);
                                $("#txtEditState").val(json["value"][0]["sState"]);
                                $("#txtEditCity").val(json["value"][0]["sCity"]);
                                $("#txtEditPincode").val(json["value"][0]["sPincode"]);

                                $("#hiddenEditAction").val(2);
                                $("#hiddenEditAppUserId").val(json["value"][0]["sAppUserId"]);
                                $("#txthiddenEditAppUserId").val(json["value"][0]["sAppUserId"]);
                            }
                            console.log("success" + response.d);
                        },
                        error: function (response) {
                            console.log("error" + response.d);
                        },
                        failure: function (response) {
                            console.log("fail" + response.d);
                        }
                    });
                }
                else {

                }
            });

            $("#txtMobile").focusout(function () {
                if ($("#txtMobile").val() != "") {
                    var parameter = { "mobile": $("#txtMobile").val() };
                    $.ajax({
                        type: "POST",
                        url: "DoctorList.aspx/searchRecord",
                        data: JSON.stringify(parameter),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var json = JSON.parse(response.d);

                            //if Doctor does not exist
                            if (json["key"] == 0) {
                                $("#hiddenAction").val(0);
                                $("#hiddenAppUserId").val("");
                            }
                            //if Doctor exists and added in labs list
                            else if (json["key"] == 1) {
                                if ($("#txtMobile").val() != "") {
                                    $("#txtMobile").val("");
                                    $("#lblMobile").html("This Doctor already exists in your list");
                                }
                            }
                            //if Doctor exists but not added to list
                            else if (json["key"] == 2) {
                                $("#txtMobile").val(json["value"][0]["sMobile"]);
                                $("#txtFullName").val(json["value"][0]["sFullName"]);
                                $("#txtEmailId").val(json["value"][0]["sEmailId"]);

                                if (json["value"][0]["sGender"].toLowerCase() == "female") {
                                    $("#selGender").val("Female");
                                }
                                else if (json["value"][0]["sGender"].toLowerCase() == "male") {
                                    $("#selGender").val("Male");
                                }
                                var date = json["value"][0]["sBirthDate"];
                                var d = new Date(date);
                                console.log(d);
                                var dd = d.getDate();
                                var mm = d.getMonth() + 1;
                                console.log(mm.toString().length);
                                mm = (mm.toString().length == 1) ? ('0' + mm.toString()) : mm;
                                console.log(mm);
                                var yy = d.getFullYear();
                                var bdate = dd + "/" + mm + "/" + yy;
                                console.log(bdate);
                                //$("#dob").val(bdate);

                                $("#txtBirthDate").val(bdate);
                                $("#txtAddress").val(json["value"][0]["sAddress"]);
                                $("#txtDegree").val(json["value"][0]["sDegree"]);
                                $("#txtSpecialization").val(json["value"][0]["sSpecialization"]);
                                $("#txtClinic").val(json["value"][0]["sClinic"]);
                                $("#txtCountry").val(json["value"][0]["sCountry"]);
                                $("#txtState").val(json["value"][0]["sState"]);
                                $("#txtCity").val(json["value"][0]["sCity"]);
                                $("#txtPincode").val(json["value"][0]["sPincode"]);

                                $("#hiddenAction").val(2);
                                $("#hiddenAppUserId").val(json["value"][0]["sAppUserId"]);
                            }
                            console.log("success" + response.d);
                        },
                        error: function (response) {
                            console.log("error" + response.d);
                        },
                        failure: function (response) {
                            console.log("fail" + response.d);
                        }
                    });
                }
            });
        });
    
    </script>
    <!--Add Doctor Validation----->
    <script type="text/javascript">
        $("#txtMobile1").focusout(function () {
            var parameter = { "mobile": $("#txtMobile1").val() };
            $.ajax({
                type: "POST",
                url: "DoctorList.aspx/searchRecord",
                data: JSON.stringify(parameter),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var json = JSON.parse(response.d);

                    //if Doctor does not exist
                    if (json["key"] == 0) {
                        $("#hiddenAction1").val(0);
                        $("#hiddenAppUserId1").val("");
                    }
                    //if Doctor exists and added in labs list
                    else if (json["key"] == 1) {
                        $("#txtMobile1").val("");
                        $("#lblMobile1").html("This Doctor already exists in your list");
                    }
                    //if Doctor exists but not added to list
                    else if (json["key"] == 2) {
                        $("#txtMobile1").val(json["value"][0]["sMobile"]);
                        $("#txtFullName1").val(json["value"][0]["sFullName"]);
                        $("#txtEmailId1").val(json["value"][0]["sEmailId"]);

                        if (json["value"][0]["sGender"].toLowerCase() == "female") {
                            $("#selGender1").val("Female");
                        }
                        else if (json["value"][0]["sGender"].toLowerCase() == "male") {
                            $("#selGender1").val("Male");
                        }
                        var date = json["value"][0]["sBirthDate"];
                        var d = new Date(date);
                        console.log(d);
                        var dd = d.getDate();
                        var mm = d.getMonth() + 1;
                        console.log(mm.toString().length);
                        mm = (mm.toString().length == 1) ? ('0' + mm.toString()) : mm;
                        console.log(mm);
                        var yy = d.getFullYear();
                        var bdate = dd + "/" + mm + "/" + yy;
                        console.log(bdate);
                        //$("#dob").val(bdate);

                        $("#txtBirthDate1").val(bdate);
                        $("#txtAddress1").val(json["value"][0]["sAddress"]);
                        $("#txtDegree1").val(json["value"][0]["sDegree"]);
                        $("#txtSpecialization1").val(json["value"][0]["sSpecialization"]);
                        $("#txtClinic1").val(json["value"][0]["sClinic"]);
                        $("#txtCountry1").val(json["value"][0]["sCountry"]);
                        $("#txtState1").val(json["value"][0]["sState"]);
                        $("#txtCity1").val(json["value"][0]["sCity"]);
                        $("#txtPincode1").val(json["value"][0]["sPincode"]);

                        $("#hiddenAction1").val(2);
                        $("#hiddenAppUserId1").val(json["value"][0]["sAppUserId"]);
                    }
                    console.log("success" + response.d);
                },
                error: function (response) {
                    console.log("error" + response.d);
                },
                failure: function (response) {
                    console.log("fail" + response.d);
                }
            });
        });
 
    </script>
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById("tabclick1").click();
        };
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var rows = $("#tbodyLabSlots").find("tr").hide();
            var weekday = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
            var a = new Date();
            var day = weekday[a.getDay()];
            var filter, table, tr, td, i;
            filter = day.toUpperCase();
            table = document.getElementById("tbodyLabSlots");
            tr = table.getElementsByTagName("tr");
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[1];

                if (td) {
                    if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
            $("#hiddenTestDate").val();
        });
    </script>
    <script type='text/javascript'>
        $(document).ready(function () {
            $("#txtCollectionCharge").each(function () {
                $(this).keyup(function () {
                    calculateSum();
                });
            });
        });

        function calculateSum() {
            var TestPrice = $("#hiddenTotalFees").val();
            var sum = (parseFloat(TestPrice));
            //iterate through each textboxes and add the values
            $("#txtCollectionCharge").each(function () {
                //add only if the value is number
                if (!isNaN(this.value) && this.value.length != 0) {
                    sum += parseInt(this.value);
                }
                else {
                    $("#lblFinalAmount").text(sum);
                }

            });
            //.toFixed() method will roundoff the final sum to 2 decimal places
            $("#lblFinalAmount").text(sum);
            $("#HFinalAmount").val(sum);
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#myPatient").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#tbodyPatientList tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });

            $("#myDoctor").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#tbodyDoctorList tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#myPatient").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#page li").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });

            $("#myDoctor").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#page1 li").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>
</asp:Content>

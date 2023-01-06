<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="CreateReportValues.aspx.cs" Inherits="CreateReportValues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .loader
        {
            position: fixed;
            z-index: 50;
            height: 100vh;
            width: 100%;
            background: #fff;
            top: 0;
        }
        .form_loader
        {
            background: #ffffffc4;
            width: 100%;
            height: 100%;
        }
        
        .form_loader img
        {
            vertical-align: middle;
            margin-top: 25%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"
    onload="myFunction()">
	   <style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 2px solid #c1cbd5;
        width: 170px;
        height: 120px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
</style>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function () {
        ShowProgress();
    });
</script>
    <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="images/Loader.gif" alt="" />
</div>
	   <asp:HiddenField ID="hdntestName" runat="server"></asp:HiddenField>
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand">Create Report</a>
      </div>

      <div class="mr-5">
        <ul class="navbar-nav ml-auto">          
          <li class="nav-item pt-1">
           
                  <a href="CreateReport.aspx" class="btn btn-color"><i class="fa fa-arrow-left mr-2" aria-hidden="true"></i>Back</a>
          </li>
        </ul>
      </div>
    </div>
  </nav>
    <div class="table_div">
        <div class="container-fluid">
            <div class="card mt-2 mb-3">
                <div class="card-body container">
                    <div class="row pb-3">
                        <div class="col-md-4">
                            <i class="fa fa-user" aria-hidden="true"></i>&nbsp; Patient: <span id="spanPatientID"
                                runat="server" clientidmode="Static"></span>/ <span id="spanPatientName" runat="server"
                                    clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4">
                            <i class="fa fa-id-badge" aria-hidden="true"></i>&nbsp; Gender : <span id="spanGender"
                                runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4">
                            <i class="fa fa-id-badge" aria-hidden="true"></i>&nbsp; Age : <span id="spanAge"
                                runat="server" clientidmode="Static"></span>
                        </div>
                    </div>
                    <div class="row pb-3">
                        <div class="col-md-4 ">
                            <i class="fa fa-paper-plane pr-2" aria-hidden="true"></i>Booking Id : <span id="spanBookingId"
                                runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4 ">
                            <i class="fa fa-user-md pr-2" aria-hidden="true"></i>Doctor : <span id="spanDoctorName"
                                runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4 ">
                            <i class="fa fa-calendar pr-2" aria-hidden="true"></i>Test taken on : <span id="spanTestTakenOn"
                                runat="server" clientidmode="Static"></span>
                        </div>
                    </div>
                    <div class="row pb-2">
                        <div class="col-md-4 ">
                            <i class="fa fa-paper-plane pr-2" aria-hidden="true"></i>Lab Name : <span id="spanLabName"
                                runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4 ">
                            <i class="fa fa-paper-plane pr-2" aria-hidden="true"></i>Lab Address : <span id="spanLabAddress"
                                runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4 ">
                            <i class="fa fa-paper-plane pr-2" aria-hidden="true"></i>Lab Contact : <span id="spanLabContact"
                                runat="server" clientidmode="Static"></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <div id="createReport2">
                    <h5 class="text-center pt-3 pb-1 text-17">
                        <span id="spanTestCodeName" runat="server" clientidmode="Static"></span>
                    </h5>
                    <table class="table">
                        <thead>
                            <tr>
                                <th scope='col' style="display: none">
                                    Test Id
                                </th>
                                <th scope='col' style="display: none">
                                    Test Code
                                </th>
                                <th scope='col' style="width: 10%">
                                    Analyte
                                </th>
                                <th scope='col' style="width: 10%">
                                    Subanalyte
                                </th>
                                <th scope='col' style="width: 10%">
                                    Specimen
                                </th>
                                <th scope='col' style="width: 10%">
                                    Method
                                </th>
                                <th scope='col' style="width: 10%">
                                    Result Type
                                </th>
                                <th scope='col' style="display: none">
                                    Reference Type
                                </th>
                                <th scope='col' style="display: none">
                                    Age
                                </th>
                                <th scope='col' style="display: none">
                                    Male
                                </th>
                                <th scope='col' style="display: none">
                                    Female
                                </th>
                                <th scope='col' style="display: none">
                                    Grade
                                </th>
                                <th scope='col' style="display: none">
                                    Units
                                </th>
                                <th scope='col' style="display: none">
                                    Interpretation
                                </th>
                                <th scope='col' style="display: none">
                                    Lower Limit
                                </th>
                                <th scope='col' style="display: none">
                                    Upper Limit
                                </th>
                                <th scope='col' style="width: 11%">
                                    Value / Description
                                </th>
                                <th scope='col' style="width: 10%">
                                    Range
                                </th>
                                <th scope='col' style="width: 15%">
                                    Result
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tbodyTestValueResult" runat="server">
                        </tbody>
                    </table>
                </div>
                <div class="form-group">
                    <textarea id="txtNotes" runat="server" placeholder="Notes" clientidmode="Static"
                        class="form-control"></textarea>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="htestCode" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenAnalyteCount" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenSubAnalyteCount" runat="server" ClientIDMode="Static" />
    <!-- Booking Details -->
    <div class="details">
        <div class="container">
        </div>
    </div>
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">  
      <div class="mr-5 ml-auto">
        <ul class="navbar-nav">
          <li class="nav-item">
             <asp:Button ID="btnSubmit" Text="Create and send for Approval" ClientIDMode="Static"
                    runat="server" class="btn btn-submit" OnClientClick="javascript: return reportValueValidation()"
                    OnClick="btnSubmit_Click" />
          </li>
        </ul>
      </div>
      
    </div>
  </nav>
    <!---Popup Modal End--->
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript">
        $('#resultA1').on("change", function () {
            var result = $('#resultA1').val();
            if ($('#htestCode').val() == 'COVID19') {
                switch (result) {
                    case 'Positive':
                        $('#txtNotes').val("2019-nCov RNA detected")
                        break;
                    case 'Negative':
                        $('#txtNotes').val("Negative results do not preclude 2019-nCov infection and should not be used as the sole basis for treatment or other patient management decisions.")
                        break;
                    case 'Inconclusive':
                        $('#txtNotes').val("This test result is inconclusive. It did not meet the full critetia established by the CDC for the presence of 2019-nCov.this speciman wiil be sent to the CDC for addtional testing.Their report will follow.")
                        break;
                    case 'Invalid':
                        $('#txtNotes').val("This speciman exhibited inhibited in the PCR assay or the speciman contained an inadequate aount of clinical material.If clinically warranted,repeat testing is suggested.")
                        break;
                    default:
                        $('#txtNotes').val("")
                }
            }
        });

        function loadResultBasedOnValue(obj) {
            var checkFrom = ($(obj).attr('id').split("|")[0] == 'An') ? "Analyte" : "SubAnalyte";
          
            var id = $(obj).attr('id').split("|")[1];
            var resultType = $(obj).attr('id').split("|")[2];
            var referenceType = $(obj).attr('id').split("|")[3];
            var patientGender = $(obj).attr('id').split("|")[4];
            var patientAgeText = $(obj).attr('id').split("|")[5];

            var patientAge = parseFloat($(obj).attr('id').split("|")[5].split(' ')[0]);
            var patientAgeUnit = "";
            var inputValue = $(obj).val();

            if (patientAgeText.indexOf('year') > -1) {
                patientAgeUnit = "year";
            }
            else if (patientAgeText.indexOf('month') > -1) {
                patientAgeUnit = "month";
            }
            else if (patientAgeText.indexOf('day') > -1) {
                patientAgeUnit = "day";
            }
            //        if (checkFrom == "Analyte" && resultType=="Quantitative" && inputValue != "") {
            var count = $(obj).attr('name').split("value")[1];
           
            var range = 0;
            var StartRange = 0;
            var EndRange = 0;
            var StartAge = 0;
            var EndAge = 0;
            var FirstRange = 0;
            var LastRange = 0;
            var parameter = { "id": id, "checkFrom": checkFrom, "Age": patientAgeText, "Gender": patientGender, "Val": inputValue };

            $.ajax({
                type: "POST",
                url: '<%=Page.ResolveUrl("CreateReportValues.aspx/getReferences") %>',
                data: "{'data':'" + id +
                     "!~^!" + checkFrom +
                      "!~^!" + patientAgeText +
                      "!~^!" + patientGender +
                      "!~^!" + inputValue +
                     "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var json = JSON.parse(response.d);
                      $("#result" + count).val(json);
                },
                error: function (response) {
                    console.log("error" + response.d);
                },
                failure: function (response) {
                    console.log("fail" + response.d);
                }
            });
        }
</script>
    <!-- Report Value validation -->
    <!-- Report Value validation -->
  
</asp:Content>

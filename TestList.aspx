<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="TestList.aspx.cs" Inherits="TestList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <script type="text/javascript">
     function showModal() {
         $("#myModal").modal('show');
     }

    </script>
  <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
 <script type="text/javascript">

     function goBack() {
         window.history.back()
     }
 </script>
 <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Howzu Says</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <asp:Label ID="lblMessage" runat="server"></asp:Label>
      </div>
      <div class="modal-footer">
        <asp:Button ID="btnredirect" class="btn btn-secondary" OnClientClick="goBack()"  runat="server" Text="Close"></asp:Button>
      </div>
    </div>
  </div>  
    </div>
    <style>
        .loader img
        {
            position: absolute;
            top: 50%;
        }
    </style>
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand">Test List</a>
      </div>

      <div class="mr-5">
        <ul class="navbar-nav ml-auto">
          <li class="nav-item ">
            <div class="search-box">
              <input type="text" placeholder="Search by column value" class="input" id="myInput">
              <div class="search-btn">
                <i class="fa fa-search" aria-hidden="true" style="padding-top:40%"></i>
              </div>
            </div>
          </li>
          <li class="nav-item pt-1 mr-3">
         
             <a href="#" id="HideEditbtn" class="btn btn-color" runat="server" data-toggle="modal" data-target="#modalMyTestList"><i class="fa fa-edit" aria-hidden="true"></i>Edit Test</a>
          </li>
         <%-- <li class="nav-item pt-1">
            <button class="btn btn-color"><span><i class="fa fa-arrow-left mr-2"
                  area-hidden="true"></i></span>Back</button>
          </li>--%>
        </ul>
      </div>
    </div>
  </nav>

    <div class="table_div">
        <div class="container-fluid">
            <div id="patientList">
                <ul class="responsive-table">
                    <li class="table-header">
                        <div class="col col-1 text-center">
                            Test Code</div>
                        <div class="col col-5 text-center">
                            Test Name</div>
                        <div class="col col-4 text-center">
                            Profile</div>
                        <div class="col col-3 text-center">
                            Section</div>
                        <div class="col col-2 text-center">
                            Price</div>
                    </li>
                    <div id="page">
                        <asp:Literal ID="tbodyMyTestList" runat="server"></asp:Literal></div>
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
    <div class="wrapper">
        <div id="content" class="">
            <div id="testlist" class="">
                <!-- tab content -->
                <div class="tab-content">
                    <!-- Test Package Start -->
                    <div id="testPackage" class="tab-pane fade">
                        <div id="divPackageDetails">
                            <div class="row testmasterwrap">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Package Name" ID="txtPackageName"
                                            runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblPackageName" clientidmode="Static">
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Package Price" ID="txtPackagePrice"
                                            Enabled="false" onkeydown='return (event.which >= 48 && event.which <= 57) || (event.which >= 96 && event.which <= 105) || event.which == 8 || event.which == 9 || event.which == 46'
                                            runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblPackagePrice" clientidmode="Static">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row testmasterwrap">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Discount percent" ID="txtDiscount"
                                            MaxLength="2" onkeyup="javascript:this.value = this.value.replace(/[^0-9]/g,'');"
                                            runat="server" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Package Price" ID="txtPackageDiscountedPrice"
                                            Text="0" Enabled="false" onkeydown='return (event.which >= 48 && event.which <= 57) || (event.which >= 96 && event.which <= 105) || event.which == 8 || event.which == 9 || event.which == 46'
                                            runat="server" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group" style="margin-bottom: 30px;">
                                        <asp:Button class="lab-btn-secondary" ID="btnCreateTestPackage" Text="Create Package"
                                            runat="server" ClientIDMode="Static" OnClientClick="javascript:return createTestPackageValidate()"
                                            OnClick="btnCreateTestPackage_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hiddenTestId" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hiddenTestIdPrice" runat="server" ClientIDMode="Static" />
                        <div id="divSelectTests" runat="server" clientidmode="Static">
                        </div>
                        <div class="package-wrap">
                            <div id="divPackageList ">
                                <table class="table text-center booking">
                                    <thead>
                                        <tr>
                                            <th>
                                                Package Name
                                            </th>
                                            <th>
                                                Price
                                            </th>
                                            <th>
                                                Tests Code
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodyTestPackage" runat="server" clientidmode="Static">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!-- Test Package End -->
                    <!-- My Test List Tempalate Builder Start -->
                    <div id="TempalateBuilder" class="tab-pane fade">
                        <!-- filter -->
                        <div class="filterbar">
                            <div class="row">
                                <div class="col-md-6">
                                    <h4>
                                        Filter</h4>
                                </div>
                                <div class="col-md-6">
                                    <div class="subheader-search">
                                        <div id="Div2">
                                            <div class="input-group">
                                                <asp:TextBox ID="TextBox1" placeholder="Search Here" runat="server" class="search-query form-control"
                                                    ClientIDMode="Static" onkeyup="Search_Gridview(this, 'tbodyMyTestList')"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-danger search-icon" type="button">
                                                        <span class=" glyphicon glyphicon-search"></span>
                                                    </button>
                                                </span><a href="#" id="A1" runat="server" data-toggle="modal" data-target="#modalMyTestList">
                                                    <i class="fa fa-pencil-square-o" aria-hidden="true"></i></a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="detailtable table-responsive">
                            <table class="table text-center booking" id="TempalateBuilder">
                                <thead>
                                    <tr>
                                        <th>
                                            Test Code
                                        </th>
                                        <th>
                                            Test Name
                                        </th>
                                        <th>
                                            Profile
                                        </th>
                                        <th>
                                            Section
                                        </th>
                                        <th>
                                            Price
                                        </th>
                                        <th>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tbodyTempalateBuilder" runat="server" clientidmode="Static">
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <!-- My Test List Tempalate Builder End -->
                </div>
            </div>
        </div>
    </div>
    <!-- MODALS START-->
    <!-- Modal My Test List Start -->
    <div id="modalMyTestList" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        My Test List</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <div class="modal-body booking-body">
                    <div class="cus-form">
                        <input id="inpCodeName" type="text" placeholder="Search by Test Code/Test Name" style="width: 100%;">
                        <br />
                        <br />
                        <table class="table text-center booking" id="tabEditMyTestList">
                            <tbody id="tbodyEditMyTestList" runat="server">
                            </tbody>
                        </table>
                        <div class="paging" style="display: none;">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:HiddenField ID="hiddenTotalTests" runat="server" ClientIDMode="Static" />
                    <asp:Button ID="btnUpdate" class="btn btn-submit" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/TestListValidation.js"></script>
    <script type="text/javascript" src="js/code.js"></script>
    <script type="text/javascript" src="js/pagination.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtFilterSearch").keyup(function () {
                var rows = $("#tbodyMyTestList").find("tr").hide();
                if (this.value.length) {
                    var data = this.value.split(" ");
                    $.each(data, function (i, v) {
                        rows.filter(":contains('" + v + "')").show();
                    });
                } else rows.show();
            });

            var testIds = new Array();
            $('input[name="profile"]').bind('click', function () {
                if ($(this).is(':checked')) {
                    // alert("checked");

                    $('input[type=checkbox]', $(this).parent('li')).each(function () {
                        if ($(this).attr('name') != "profile") {
                            $(this).prop('checked', true);

                            //alert($(this).attr('id') + "+" + $(this).attr('name'));

                            if ($.inArray($(this).attr('id'), testIds) == -1)
                                testIds.push($(this).attr('id'));
                        }
                    });
                }
                else {
                    // alert("unchecked");
                    $('input[type=checkbox]', $(this).parent('li')).each(function () {
                        if ($(this).attr('name') != "profile") {
                            $(this).removeAttr('checked');

                            //alert($(this).attr('id') + "+" + $(this).attr('name'));

                            if ($.inArray($(this).attr('id'), testIds) != -1)
                                testIds.splice(testIds.indexOf($(this).attr('id')), 1);
                        }
                    });
                }
                var testIdPrice = JSON.parse($("#hiddenTestIdPrice").val());
                var sum = 0;
                for (var i = 0; i < testIds.length; i++) {
                    sum += testIdPrice[testIds[i]];
                }
                $("#txtPackagePrice").val(sum);
                $("#txtPackageDiscountedPrice").val(sum);
                $("#hiddenTestId").val(testIds.join(','));
            });

            $('input[name="test"]').bind('change', function () {
                //alert($(this).attr('id'));
                if ($(this).is(':checked')) {
                    if ($.inArray($(this).attr('id'), testIds) == -1)
                        testIds.push($(this).attr('id'));
                }
                else {
                    if ($.inArray($(this).attr('id'), testIds) != -1)
                        testIds.splice(testIds.indexOf($(this).attr('id')), 1);
                }

                var testIdPrice = JSON.parse($("#hiddenTestIdPrice").val());
                var sum = 0;
                for (var i = 0; i < testIds.length; i++) {
                    sum += testIdPrice[testIds[i]];
                }

                $("#txtPackagePrice").val(sum);
                $("#txtPackageDiscountedPrice").val(sum);

                $("#hiddenTestId").val(testIds.join(','));
            });


            $("#txtDiscount").bind('input', function () {
                if ($("#txtPackagePrice").val() != "") {

                    var discountValue = ($(this).val() == "") ? 0 : parseInt($(this).val());
                    var packagePrice = parseInt($("#txtPackagePrice").val());
                    var discount = (discountValue / 100) * parseInt(packagePrice);
                    var discountedPrice = packagePrice - discount;
                    $("#txtPackageDiscountedPrice").val(Math.round(discountedPrice));
                    //alert("Discount : " + discount + "\nDiscounted Price : " + discountedPrice);                    
                }
            });
        });
    </script>
    <!-- DELETE TEST -->
    <script type="text/javascript">
        function removeTest(obj) {

            var testId = $(obj).attr('id');

            if (confirm("Are you sure you want to delete ?")) {

                var parameter = { "testId": testId };
                $.ajax({
                    type: "POST",
                    url: "TestLIst.aspx/deleteTest",
                    data: JSON.stringify(parameter),
                    contentType: "application/json; charset=utf-8",
                    //dataType: "text/plain",
                    success: function (response) {

                        if (response.d == "0") {
                            alert("Error occured while deleting test");
                        }
                        else if (response.d == "1") {
                            $(obj).closest('tr').remove();
                        }
                        else if (response.d == "2") {
                            alert("This test is being used, hence cannot be deleted");
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
        }    
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#inpCodeName").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#ContentPlaceHolder1_tbodyEditMyTestList tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });


        function Search_Gridview(strKey, strGV) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById(strGV);
            var rowData;
            for (var i = 0; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#page li").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>
</asp:Content>

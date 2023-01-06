 <%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="BookDetails.aspx.cs" Inherits="BookDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/date.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	  <asp:HiddenField ID="hdntestName" runat="server" />
	  <asp:HiddenField ID="hdnpmobile" runat="server" />
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="" class="navbar-brand">View History</a>
      </div>
      <div class=" ml-3">
        <ul class="navbar-nav">          
          <li class="nav-item">
        <a href="" class="navbar-subtitle px-3">Booking ID:<span id="spanBookingId" runat="server" clientidmode="Static"></span></a>
      </li>
      <li class="nav-item">
        <a href="" class="navbar-subtitle px-3">Status: <span id="spanBookStatus" runat="server" clientidmode="Static"></span></a>
      </li>
      <li class="nav-item">
       <div id="divApprovedOn" runat="server" clientidmode="Static">
					 <a href="" class="navbar-subtitle px-3">Approved On:<span id="spanConfirmedAt" runat="server" clientidmode="Static"></span></a>                 
				</div>	
      </li>
      </ul>
      </div>
      <div class="mr-5">
        <ul class="navbar-nav ml-auto">       
          <li class="nav-item">
           <span id="viewInvoice" runat="server" clientidmode="Static" ><a href="" class="btn btn-color"> Invoice </a> </span>
                    <a href="TestBookList.aspx" id="A1"  runat="server" class="btn btn-color"><span><i class="fa fa-arrow-left mr-2"
                  area-hidden="true"></i></span>Back</a> 
          </li>
        </ul>
      </div>
    </div>
  </nav>
    <div class="table_div">
        <div class="container-fluid">
            <div class="card mb-4">
                <div class="card-body container">
                    <div class="row pb-2">
                        <div class="col-md-4">
                            Patient: <span id="spanPatientName" runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4">
                            Booking Date: <span id="spanBookRequestedAt" runat="server" clientidmode="Static">
                            </span>
                        </div>
                        <div class="col-md-4">
                            Booking Id: <span id="spanBookId" runat="server" clientidmode="Static"></span>
                        </div>
                    </div>
                    <div class="row pt-2">
                        <div class="col-md-4">
                            Doctor: <span id="spanDoctorName" runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4">
                            Booked Via: <span id="spanBookMode" runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4">
                            Sample Collection Address: <span id="spanSampleAddress" runat="server" clientidmode="Static">
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <div id="viewBookHistory">
                <ul class="responsive-table">
                    <li class="table-header">
                        <div class="col col-1 text-center">
                            Test Code</div>
                        <div class="col col-2 text-center">
                            Test Name</div>
                        <div class="col col-3 text-center">
                            Test Date
                        </div>
                        <div class="col col-4 text-center">
                            Test Status</div>
                        <div class="col col-5 text-center">
                            Amount</div>
                    </li>
                    <asp:Literal ID="tbodyBookTestDetails" runat="server"></asp:Literal>
                </ul>
            </div>
            <div class="card mt-4">
                <div class="card-body container">
                    <h5 class="card-title">
                        Payment Details :<span id="spanPayementStatus" runat="server" clientidmode="Static">
                        </span>
                    </h5>
                    <div class="row pb-2">
                        <div class="col-md-4">
                            Total fees: <span class="primary-col" id="spanTotalFees" runat="server" clientidmode="Static">
                            </span>
                        </div>
                        <div class="col-md-4">
                            Paid Amount: <span class="primary-col" id="spanAdvancePayment" runat="server" clientidmode="Static">
                            </span>
                        </div>
                        <div class="col-md-4">
                            Payment Due: <span class="balance-col" id="spanPaymentDue" runat="server" clientidmode="Static">
                            </span>
                        </div>
                    </div>
                    <div class="row pt-2">
                        <div class="col-md-4">
                            Test Date: <span class="primary-col" id="spanTestDate" runat="server" clientidmode="Static">
                            </span>
                        </div>
                        <div class="col-md-4">
                            Test Time Slot: <span class="primary-col" id="spanTimeSlot" runat="server" clientidmode="Static">
                            </span>
                        </div>
                        <div class="col-md-4">
                            Test Status: <span class="primary-col" id="spanTestStatus" runat="server" clientidmode="Static">
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <nav class="navbar navbar-expand-sm bg-light navbar-header">
                          <div class="container-fluid">    
                            <div class="mr-5 ml-auto">
                              <ul class="navbar-nav">
                                <li class="nav-item mr-3">
                                <div id="divApproveReject" runat="server" clientidmode="Static">
                                      <asp:HiddenField ID="hiddenBookStatus" runat="server" ClientIDMode="Static" />
                                     <a href="#" class="btn btn-submit" data-toggle="modal" runat="server" data-target="#modalTestlist"  id="btnPreTest" clientidmode="Static">Add Prescription Test</a>
                                
                                   <a href="#" class="btn btn-submit"  data-toggle="modal" data-target="#modalApproveReject" id="btnApprove" runat="server"   clientidmode="Static">Approve</a>
                                   <a href="#" runat="server" class="btn btn-submit" data-toggle="modal" data-target="#modalApproveReject" id="btnReject" clientidmode="Static">Reject</a>
                                 </div>
                                </li>
                                <li class="nav-item mr-3">
                                 <div id="divPaymentDetailsEdit" runat="server" clientidmode="Static">
                                     <a href="#" data-toggle="modal" data-target="#modalPaymentDetail" class="btn btn-submit" data-toggle="modal" data-target="#modalApproveReject" id="btnApprove" clientidmode="Static"> Edit</a>
                                     <a href="#" class="btn btn-submit" data-toggle="modal" data-target="#modalApproveReject" id="btnCancel" clientidmode="Static">Reject</a>
                                      <a href="#" class="btn btn-submit" data-toggle="modal" data-target="#ModelLabSlot" id="A2" runat="server" clientidmode="Static"> Reschedule</a>
                                </div>
                                </li>
                                <li class="nav-item">
                                 
                                </li>
                              </ul>
                            </div>
                          </div>
                        </nav>
    <div class="wrapper">
        <div id="content">
            <div class="container-fluid">
                <div class="payment" id="hideprescriptions" runat="server">
                    <div class="">
                        <div class="paymenttitle">
                            <p>
                                Prescription
                            </p>
                        </div>
                        <div class="row">
                            <div class="prescription-img">
                                <img src="" id="prescriptionimg" clientidmode="Static" runat="server" alt="prescription"
                                    class="img-responsive north" style="width:220px; height:220px;" />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- The Modal -->
                <div id="ModalPopup" class="modal1">
                    <span class="close1">&times;</span>
                    <img class="modal1-content1 north" id="img01" />
                    <div id="caption1">
                    </div>
                    <input type="button" class="btn btn-color float-center" id="btnrotate" value="rotate" />
                </div>
            </div>
            <asp:HiddenField ID="HDate" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hiddenTestDate" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hiddenTimeSlot" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hiddenAppointmentType" runat="server" ClientIDMode="Static" />
            <div id="ModelLabSlot" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                Reschedule</h4>
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <div class="col-xs-6 ">
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text fa-color" style="color: #91c740;"><i class="fa fa-calendar fa-fa-color"
                                                aria-hidden="true"></i></span>
                                        </div>
                                        <asp:TextBox ID="txtTestDate" placeholder="  Enter test date as (dd/mm/yyyy)" runat="server"
                                            class="form-control" onchange="DateSlot()" ClientIDMode="Static" Style="text-align: center"
                                            ReadOnly="true"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" CssClass="cal_Theme1" PopupButtonID="txtTestDate"
                                            runat="server" TargetControlID="txtTestDate" Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-xs-1">
                                </div>
                                <table class="table booking table-bordered table-hover" id="tabLabSlots">
                                    <thead>
                                        <tr>
                                            <th>
                                                Action
                                            </th>
                                            <th>
                                                Day
                                            </th>
                                            <th>
                                                From
                                            </th>
                                            <th>
                                                To
                                            </th>
                                            <th>
                                                Appointment Type
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodyLabSlots" runat="server" clientidmode="Static">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Button2" class="btn btn-color" runat="server" Text="Update" OnClick="btnSchedule_Click"
                                ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal Approve Reject-->
            <div id="modalApproveReject" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                Respond</h4>
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="cus-form">
                                <div class="">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Comment" ID="txtComment" TextMode="MultiLine"
                                            Rows="5" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" class="btn btn-color" runat="server" Text="Update" OnClick="btnUpdate_Click"
                                ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal Test Booking Details-->
            <div id="modalPaymentDetails" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content paymentdetails">
                        <div class="modal-header">
                            <h4 class="modal-title text-center">
                                Payment Details</h4>
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="cus-form">
                                <div class="">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label id="lblTestStatus">
                                                    Test Status :<span style="color: Red">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-8">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label id="lblPaymentStatus">
                                                    Payment Status :<span style="color: Red">*</span>
                                                </label>
                                            </div>
                                            <div class="col-sm-8">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label>
                                                    Advance Paid</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:TextBox class="form-control" placeholder="Advance Paid" ID="txt" runat="server"
                                                    ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblAdvancePaid" class="form-error">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal Testlist-->
            <div id="modalTestlist" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <img id="imgloader2" src="images/Loader.gif" alt="Loading" class="adjustloader hide" />
                        <div class="modal-header">
                            <h4 class="modal-title">
                                Respond</h4>
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="cus-form">
                                <asp:HiddenField ID="hTestPricearray" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hiddenTestList" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hiddenTotalFees" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hiddenTestIdCodePrice" runat="server" ClientIDMode="Static" />
                                <div class="form-group">
                                    <asp:TextBox class="form-control hide" placeholder="Search test by test code" ID="txtSearchTest"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <div class="table-responsive modal-table">
                                        <table class="table booking" id="tabTestList">
                                            <thead>
                                                <tr>
                                                    <th>
                                                    </th>
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
                                                </tr>
                                            </thead>
                                            <tbody id="tbodyMyTestList" runat="server" clientidmode="Static">
                                            </tbody>
                                        </table>
                                        <div class="paging">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Button1" class="btn btn-color" runat="server" Text="Add" OnClick="btnIMGprescription_Click"
                                ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="modalPaymentDetail" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                Payment Details</h4>
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Test Status<span style="color: Red">*</span>
                                        </label>
                                        <select id="selTestStatus" runat="server" class="form-control" clientidmode="Static">
                                            <option value="Pending">Pending</option>
                                            <option value="Taken">Taken</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Payment Status<span style="color: Red">*</span></label>
                                        <select id="selPaymentStatus" runat="server" class="form-control" clientidmode="Static">
                                            <option value="Not paid">Not Paid</option>
                                            <option value="Paid">Paid</option>
                                            <option value="Advance">Advance</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Payment Method :<span style="color: Red">*</span></label>
                                        <asp:DropDownList class="form-control" ID="ddlPaymentMethod" runat="server" ClientIDMode="Static">
                                            <asp:ListItem Value="Cash" Selected="True">Cash</asp:ListItem>
                                            <asp:ListItem Value="cards">Card(Credit/Debit)</asp:ListItem>
                                            <asp:ListItem Value="UPI">UPI</asp:ListItem>
                                            <asp:ListItem Value="NetBanking">Net Banking</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Amount<span style="color: Red">*</span></label>
                                        <asp:TextBox class="form-control" placeholder="Amount" onkeypress="return isNumber(event)"
                                            ID="txtAdvancePaid" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="upp" runat="server">
                            <ContentTemplate>
                                <div class="modal-footer">
                                    <asp:Button ID="btnUpdatePaymentDetails" class="btn btn-color" runat="server" Text="Update"
                                        OnClientClick="javascript:return validateBookingDetails()" OnClick="btnUpdatePaymentDetails_Click"
                                        ClientIDMode="Static" />
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
    </div>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/BookDetailsValidation.js"></script>
    <script type="text/javascript" src="js/DoctorListValidation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnApprove").click(function () {
                $("#hiddenBookStatus").val("Confirmed");
            });

            $("#btnReject").click(function () {
                $("#hiddenBookStatus").val("Canceled");
            });
            $("#btnCancel").click(function () {
                $("#hiddenBookStatus").val("Canceled");
            });
            $("#btnUpdate").click(function () {
                $("#imgloader").removeClass('hide');
            });
            $("#Button1").click(function () {
                $("#imgloader2").removeClass('hide');
            });
        });

        $("#txtSearchTest").keyup(function () {

            var input, filter, table, tr, td, i;
            input = document.getElementById("txtSearchTest");
            filter = input.value.toUpperCase();
            table = document.getElementById("tbodyMyTestList");
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
        });

        $('#btnrotate').click(function () {
            var img = $('#img01');
            img.addClass("img-width-size");
            if (img.hasClass('north')) {
                img.addClass("west");
                img.removeClass("north");
            } else if (img.hasClass('west')) {
                img.addClass("south");
                img.removeClass("west");
            } else if (img.hasClass('south')) {
                img.addClass("east");
                img.removeClass("south");
            } else if (img.hasClass('east')) {
                img.addClass("north");
                img.removeClass("east");
            }
        });
        var testIds = new Array();
        var testIdCodePrice = new Array();
        var totalFees = 0;
      
    </script>
    <!--Image popup-->
    <script type="text/javascript">
        // Get the modal
        var modal = document.getElementById("ModalPopup");

        // Get the image and insert it inside the modal - use its "alt" text as a caption
        var img = document.getElementById("prescriptionimg");
        var modalImg = document.getElementById("img01");
        var captionText = document.getElementById("caption1");
        img.onclick = function () {
            modal.style.display = "block";
            modalImg.src = this.src;
            captionText.innerHTML = this.alt;
        }

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close1")[0];

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
            $('#img01').removeClass("img-width-size");
            var img = $('#img01');
            if (img.hasClass('north')) {
                img.removeClass("north");
            } if (img.hasClass('west')) {
                img.removeClass("west");
            } if (img.hasClass('south')) {
                img.removeClass("south");
            } if (img.hasClass('east')) {
                img.removeClass("east");
            }
            img.addClass("north");
        }
    </script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript">
        $('#tabTestList').datatable({
            pageSize: 9,
            sort: [true, true, true, true, true, true, false],
            filters: [false, true, true, true, true, true],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var rows = $("#tbodyLabSlots").find("tr").hide();
            // $("#txtTestDate").on('input', function () {
            $("#txtTestDate").change(function () {
                //function DateSlot() {
                var testDate = $(this).val().split('/')[1] + "/" + $(this).val().split('/')[0] + "/" + $(this).val().split('/')[2];

                var datesplit = testDate.split("/");
                var day = datesplit[1];
                var month = datesplit[0];
                var year = datesplit[2];
                var ddmmyy = $("#txtTestDate").val();
                $("#HDate").val(ddmmyy);

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
                }
                else {
                    //alert("invalid date");
                }
                //    }
            });
        });


        function timeSlotSelect(obj) {
            //$("#hiddenTestDate").val($("#txtTestDate").val());
            var value = $(obj).val().split("/");
            var timeslot = value[0];
            var Appointment = value[1];
            $("#hiddenAppointmentType").val(Appointment);
            $("#hiddenTimeSlot").val(timeslot);
            //alert($("#hiddenTestDate").val() + " : " + $("#hiddenTimeSlot").val());
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

                //            if (myDate < today) {
                //                return false;
                //            }
            }
            return true;
        }
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
    <script type="text/javascript">
        var testIds = new Array();
        var pattestIds = new Array();
        var testIdCodePrice = new Array();
        var totalFees = 0;
        //$("input[type='checkbox']").change(function () {
        $(document).on("change", "input[type='checkbox']", function () {

            if ($(this).is(':checked')) {
                testIds.push($(this).val().split('|')[0]);
                pattestIds.push($(this).val().split('|')[1]);
                totalFees += parseInt($(this).val().split('|')[1]);
                testIdCodePrice.push($(this).val());
            }
            else {
                //testIds.pop($(this).val().split('|')[0]);
                testIds.splice(testIds.indexOf($(this).val().split('|')[0]), 1);
                pattestIds.splice(testIds.indexOf($(this).val().split('|')[1]), 1);
                totalFees -= parseFloat($(this).val().split('|')[1]);
                testIdCodePrice.splice(testIdCodePrice.indexOf($(this).val()), 1);
            }
            $("#hTestPricearray").val(pattestIds.join(','));
            $("#hiddenTestList").val(testIds.join(','));
            $("#hiddenTotalFees").val(totalFees);
            $("#hiddenTestIdCodePrice").val(testIdCodePrice);
        });
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="ViewReportValues.aspx.cs" Inherits="ViewReportValues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
.pdf-btn
{    
    background: #fff;
color: #195bb4;
border-radius: 5px;
text-align: center;
​​​​​​​display: inline-block;
​​​​​​​ padding: 8px 20px;
​​​​​​​min-width: 100px;
​​​​​​​border: none;
​​​​​​​float:right;
margin-left:70px;
    }
</style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	  <asp:HiddenField ID="hdntestName" runat="server" />
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
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand">Report</a>
      </div>
      <div class="mr-5">
        <ul class="navbar-nav ml-auto">
			 <li class="nav-item pt-1 mr-3">
            <h6 id="patst" runat="server" visible="false">Payment Status : <span><asp:Label ID="lblpaystatus" runat="server" ></asp:Label></span></h6>
          </li>
          <li class="nav-item pt-1 mr-3">
             <span id="viewpdf" runat="server" clientidmode="Static" ><a href="" target="_blank"class="btn btn-color"  > View Report </a> </span>
          </li>
          <li class="nav-item pt-1 mr-3">
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                            <asp:Button ID="Button1" runat="server" class="btn btn-color"  ClientIDMode="Static"   OnClick="btnsendMail_Click" Text="Send Report" /> 
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="text form_loader">
                            <img src="images/Loader.gif" alt="Loading">
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
        </li>
          <li class="nav-item pt-1">
           <a href="ViewReport.aspx" class="btn btn-color"><i class="fa fa-arrow-left mr-2" aria-hidden="true"></i>Back</a>
          </li>
        </ul>
      </div>
    </div>
  </nav>
    <asp:HiddenField ID="hfGridHtml" runat="server" />
    <div class="table_div">
        <div class="container-fluid">
            <div class="card mt-2 mb-3">
                <div class="card-body container">
                    <div class="row pb-3">
                        <div class="col-md-4 ">
                            Lab Name: <span id="spanLabName" runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4 ">
                            Lab Address : <span id="spanLabAddress" runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4 ">
                            Lab Contact : <span id="spanLabContact" runat="server" clientidmode="Static"></span>
                        </div>
                    </div>
                    <div class="row pb-3">
                        <div class="col-md-4 ">
                            Booking ID : <span id="spanBookingId" runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4 ">
                            report Id : <span id="spanReportId" runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4 ">
                            Payment Status : <span id="spanPaymentStatus" runat="server" clientidmode="Static">
                            </span>
                        </div>
                    </div>
                    <div class="row pb-3">
                        <div class="col-md-4 ">
                            Patient : <span id="spanPatientName" runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4 ">
                            Gender : <span id="spanGender" runat="server" clientidmode="Static"></span>
                        </div>
                        <div class="col-md-4 ">
                            Doctor : <span id="spanDoctorName" runat="server" clientidmode="Static"></span>
                        </div>
                    </div>
                    <div class="row pb-3">
                        <div class="col-md-4 ">
                            Test Taken On : <span id="spanTestTakenOn" runat="server" clientidmode="Static">
                            </span>
                        </div>
                        <div class="col-md-4 ">
                            Report Created On : <span id="spanReportCreatedOn" runat="server" clientidmode="Static">
                            </span>
                        </div>
                        <div class="col-md-4 ">
                            Report Created By : <span id="spanReportCreatedBy" runat="server" clientidmode="Static">
                            </span>
                        </div>
                    </div>
                    <div class="row pb-2">
                        <div class="col-md-4 ">
                            Approval Status : <span id="spanApprovalStatus" runat="server" clientidmode="Static">
                            </span>
                        </div>
                        <div class="col-md-4 ">
                            Age : <span id="spanAge" runat="server" clientidmode="Static"></span>
                        </div>
                    </div>
                </div>
            </div>
            <div id="viewReport2">
                <h5 class="text-center pt-3 pb-1 text-17">
                    <span id="spanTestCodeName" runat="server" clientidmode="Static"></span>
                </h5>
                <ul class="responsive-table">
                    <li class="table-header">
                        <div class="col col-1 text-center">
                            Analyte</div>
                        <div class="col col-2 text-center">
                            Sub Analyte</div>
                        <div class="col col-3 text-center">
                            Specimen</div>
                        <div class="col col-4 text-center">
                            Method</div>
                        <div class="col col-5 text-center">
                            Range</div>
                        <div class="col col-6 text-center">
                            Result Type</div>
                        <div class="col col-7 text-center">
                            Value</div>
                        <div class="col col-8 text-center">
                            Result</div>
                    </li>
                    <div id="page">
                        <asp:Literal ID="tbodyTestValueResult" runat="server"></asp:Literal></div>
                </ul>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <textarea class="form-control text-12" readonly="readonly" id="spanNotes" runat="server"
                            rows="3" placeholder="Note"></textarea>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <textarea class="form-control text-12" readonly="readonly" id="spanComment" runat="server"
                            rows="3" placeholder="Comment"></textarea>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="pdfdata" runat="server">
        <!-- Booking Details -->
        <!--------hdnfield start-------->
        <asp:HiddenField ID="hdnsPatientId" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnsDoctorId" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnsReportId" runat="server" ClientIDMode="Static" />
        <nav class="navbar navbar-expand-sm bg-light navbar-header">
                          <div class="container-fluid">    
                            <div class="mr-5 ml-auto">
                              <ul class="navbar-nav">
                                <li class="nav-item mr-3">
                                <div id="divApproveReject" runat="server" clientidmode="Static">
                                    <asp:HiddenField ID="hiddenReportStatus" runat="server" ClientIDMode="Static" />
                                    <a href="" class="btn btn-success" data-toggle="modal" data-target="#modalApproveReject"
                                        id="btnApprove" clientidmode="Static">Approve</a> <a href="" class="btn btn-warning"
                                            data-toggle="modal" data-target="#modalApproveReject" id="btnReject" clientidmode="Static">
                                            Reject</a>
                                 </div>
                                </li>
                                <li class="nav-item mr-3">
                                  <div class="reportvalueedit">
                                      <span id="btnEditReport" runat="server"><a href="" data-toggle="modal" data-target="#modalEditReport"
                                       class="btn btn-color">Edit</a></span>
                                  </div>
                                </li>
                              </ul>
                            </div>
                          </div>
                        </nav>
        <!--------hdnfield end------->
    </div>
    <!-- Modal Approve Reject-->
    <div id="modalApproveReject" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        Approve/Reject</h4>
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
                <asp:UpdatePanel ID="upp" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" class="btn btn-submit" runat="server" Text="Update" OnClick="btnUpdate_Click"
                                ClientIDMode="Static" />
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
    <asp:HiddenField ID="htestCode" runat="server" ClientIDMode="Static" />
    <!-- Modal Edit Report-->
    <div id="modalEditReport" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content" style="width: 135%">
                <div class="modal-header">
                    <h4 class="modal-title">
                        Edit Report</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <div class="modal-body">
                    <div class="cus-form">
                        <div class="">
                            <asp:HiddenField ID="hiddenValueIdList" runat="server" ClientIDMode="Static" />
                            <table class="table text-center booking">
                                <tbody id="tbodyTestValueResultEdit" runat="server">
                                </tbody>
                            </table>
                        </div>
                        <div>
                            <textarea id="txtNotes" runat="server" placeholder="Notes" clientidmode="Static"
                                class="form-control"></textarea>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnUpdateReport" class="btn btn-submit" runat="server" Text="Update"
                        OnClick="btnUpdateReport_Click" ClientIDMode="Static" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jspdf.min.js"></script>
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
            alert(checkFrom);
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
            var count = $(obj).attr('name').split("valueA")[1];

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
                url: "ViewReportValues.aspx/getReferences",
                data: JSON.stringify(parameter),
                contentType: "application/json; charset=utf-8",
                //dataType: "text/plain",
                success: function (response) {
                    var json = JSON.parse(response.d);
                    $("#resultA" + count).val(json);
                    //if error occurs                   
                },
                error: function (response) {
                    console.log("error" + response.d);
                },
                failure: function (response) {
                    console.log("fail" + response.d);
                }
            });
        }

        function PaymentStatus() {

            var bool = "";
            var status = $("#spanPaymentStatus").html();
            if (status.toLowerCase() == "paid") {
                $("#Button1").show();
                bool = true;
            }
            else {

                bool = false;
                //alert("payment is not received");
                $("#Button1").hide();
            }
            return bool;
        }
        PaymentStatus();
        function demoFromHTML() {
            var pdf = new jsPDF('p', 'pt', 'letter');
            // source can be HTML-formatted string, or a reference
            // to an actual DOM element from which the text will be scraped.
            source = $('#pdfdata')[0];

            // we support special element handlers. Register them with jQuery-style 
            // ID selector for either ID or node name. ("#iAmID", "div", "span" etc.)
            // There is no support for any other type of selectors 
            // (class, of compound) at this time.
            specialElementHandlers = {
                // element with id of "bypass" - jQuery style selector
                '#bypassme': function (element, renderer) {
                    // true = "handled elsewhere, bypass text extraction"
                    return true
                }
            };
            margins = {
                top: 80,
                bottom: 60,
                left: 10,
                width: 900
            };
            // all coords and widths are in jsPDF instance's declared units
            // 'inches' in this case
            pdf.fromHTML(
    source, // HTML string or DOM elem ref.
    margins.left, // x coord
    margins.top, { // y coord
        'width': margins.width, // max width of content on PDF
        'elementHandlers': specialElementHandlers
    },
    function (dispose) {
        // dispose: object with X, Y of the last line add to the PDF 
        //          this allow the insertion of new lines after html
        pdf.save('Test.pdf');
    }, margins);
        }

        $(document).ready(function () {
            $("#btnApprove").click(function () {
                $("#hiddenReportStatus").val("Approved");
            });

            $("#btnReject").click(function () {
                $("#hiddenReportStatus").val("Rejected");
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=Button2]").click(function () {
                $("[id*=hfGridHtml]").val($("#pdfdata").html());
            });
        });
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="PatientReportHistoryManagement.aspx.cs" Inherits="PatientReportHistoryManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <link href="css/date.css" rel="stylesheet" type="text/css" />
      <link href="css/date.css" rel="stylesheet" type="text/css" />
   
   <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand">Patient Report History</a>
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
         <asp:HiddenField ID="hdnpatientId" runat="server"></asp:HiddenField>
          <li class="nav-item pt-1">
            <a href="PatientList.aspx"  class="btn btn-color"><i class="fa fa-arrow-left mr-2" aria-hidden="true"></i>Back</a>
            <asp:Button ID="btnviewpayment" class="btn btn-color" runat="server" Text="View Payment History" 
                  onclick="btnviewpayment_Click"></asp:Button>
          </li>
        </ul>
      </div>
    </div>
  </nav>

  




  <table class="table" id="patientmanagementhealthprofile">
            <thead class="table-header">
                <tr >
                    <th>
                        Booking ID
                    </th>
                    <th>
                        Patient Name
                    </th>
                    <th>
                        Test Name
                    </th>
                    <th>
                        Test Taken On
                    </th>
                    <th>
                        Amount
                    </th>
                    <th>
                        Booking Type
                    </th>
                    <th>
                        Action
                    </th>
                </tr>
            </thead>
            <tbody id="tbodyReports" style="text-align: center" runat="server" clientidmode="Static">
            </tbody>
        </table>
       
 
    <%--start Pagination and sorting data --%>
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
      <script type="text/javascript" src="js/code.js"></script>
   <script type="text/javascript">
       $(document).ready(function () {
           $("#myInput").on("keyup", function () {
               var value = $(this).val().toLowerCase();
               $("#tbodyReports tr").filter(function () {
                   $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
               });
           });
       });
    </script>
    <script type="text/javascript">
        $('#patientmanagementhealthprofile').datatable({
            pageSize: 1000,
             onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>
     
</asp:Content>

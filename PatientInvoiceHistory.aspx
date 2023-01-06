<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="PatientInvoiceHistory.aspx.cs" Inherits="PatientInvoiceHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .paging ul
        {
            width: 100%;
            margin-left: auto;
            margin-right: auto;
        }
    </style>
     
   <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand"><span runat="server" id="spanPatientName"></span> Payment History</a>
      </div>
      <div class="mr-5">
        <ul class="navbar-nav ml-auto">
         
          <li class="nav-item pt-1 mr-3">
                    <a href="PatientList.aspx"  class="btn btn-color nextbtn"><i class="fa fa-arrow-left mr-2" aria-hidden="true"></i>Back</a>
          </li>
        </ul>
      </div>
    </div>
  </nav>
    <asp:HiddenField ID="hdnpid" runat="server" />

    <div class="container">
        <div class="row">
          
            <div class="col-lg-12">
  <div class="table-responsive">
                        <table class="table table-bordered text-small" id="dataTable" width="100%" style="color: #56549b"
                            cellspacing="0">
                            <thead>
                                 <tr>
                        <th>
                            Sr. No.
                        </th>
                        <th>
                            Booking Id
                        </th>
                        <th>
                            Total Amount
                        </th>
                        <th>
                            Paid Amount
                        </th>
                        <th>
                            Payment Method
                        </th>
                        <th>
                            Payment Date
                        </th>
                        <th>
                            View
                        </th>
                    </tr>
                            </thead>
                            <tbody id="tbodyPatientInvoiceHistory" runat="server"  style="text-align: center">
                            </tbody>
                        </table>
                    </div>
</div>
</div>
</div>



       
       
    </div>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <%--<script type="text/javascript" src="js/jquery.easyPaginate.js"></script>--%>
    <script type="text/javascript">
        var dtse = $('#tabPatientInvoice').datatable({
            pageSize: 100,
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>
</asp:Content>

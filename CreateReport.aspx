<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="CreateReport.aspx.cs" Inherits="CreateReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand">Tests Taken</a>
      </div>

      <div class="mr-5">
       
      </div>
    </div>
  </nav>
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
                                        Patient Name
                                    </th>
                                    <th>
                                       Test Code
                                    </th>
                                    <th>
                                        Test Name
                                    </th>
                                    <th>
                                        Test Taken On
                                    </th>
                                  
                                    <th>
                                        Action
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="tbodyTestTakenList" runat="server"  style="text-align: center">
                            </tbody>
                        </table>
                    </div>
</div>
</div>
</div>
   
 
    <script type="text/javascript" src="js/jquery.js"></script>
    <%--<script type="text/javascript" src="js/jquery.easyPaginate.js"></script>--%>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/code.js"></script>
    <script type="text/javascript" src="js/pagination.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#rdoBookSlot").change(function () {
                var dNow = new Date();
                var utcdate = (dNow.getMonth() + 1) + '/' + dNow.getDate() + '/' + dNow.getFullYear();
                $('#currentDate').text(utcdate);
                var rows = $("#tbodyTestBookList").find("tr").hide();
                rows.filter(":contains('" + utcdate + "')").show();
            });
        });
      
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

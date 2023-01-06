<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="TestBookListhistory.aspx.cs" Inherits="TestBookListhistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/date.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #ContentPlaceHolder1_imgPopup2
        {
            position: relative;
            left: 100%;
            bottom: 30px;
            z-index: 99;
            
        }
         /* for scrollable table    */

    </style>
    <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="navbar navbar-expand-sm bg-light navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
    <%--    <a href="#" class="navbar-brand">Test Booking Request</a>--%>
        <a href="#" class="navbar-brand">Test Booking Request</a>
      
      </div>
      <div class="mr-5">
        <ul class="navbar-nav ml-auto">
         
          <li class="nav-item">
            <a href="TestBookList.aspx" id="A1"  runat="server" class="btn btn-color"><span><i class="fa fa-arrow-left mr-2"
                  area-hidden="true"></i></span>Back</a> 
          </li>
        </ul>
      </div>
    </div>
  </nav>
   <div class="container">
        <div class="row">
        <div class="col-md-12">
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
</div>
</div>
</div>
     
  
    <script type="text/javascript" src="js/jquery.js"></script>
  
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
</asp:Content>

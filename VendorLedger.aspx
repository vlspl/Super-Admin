<%@ Page Title="" Language="C#" MasterPageFile="~/inventoryMasterPage.master" AutoEventWireup="true" CodeFile="VendorLedger.aspx.cs" Inherits="VendorLedger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <nav class="navbar navbar-expand navbar-light bg-white topbar mb-2 static-top shadow">
          <h3 class="text-dark d-none d-sm-block" style="width:100%;"><span class="text-primary">Vendor Ledger</span> <span class="text-dgreen" style="float:right;">  <a href="vendorPayment.aspx" id="A2"  runat="server" class="btn btn-secondary"><span><i class="fa fa-plus mr-2" area-hidden="true"></i></span> Add New</a></span> </h3>
        
                    <!-- Topbar Navbar -->
                       
        </nav>
    <div class="wrapper" style="margin-left:20px;">
        <div id="content">
       <div class="panel panel-primary filterable">
            <div class="panel-heading"></div>
             <div class="panel-body">
                <div class="col-md-12">
        <div class="table-responsive">
                        <table class="table table-bordered text-small" id="dataTable" width="100%" style="color: #56549b"
                            cellspacing="0">
                            <thead align="center">
                                <tr>
                                    <th>
                                        Sr. No.
                                    </th>
                                    <th>
                                        Date
                                    </th>
                                    <th>
                                       Bill No
                                    </th>
                                    <th>
                                       Vendor Name
                                    </th>
                                    <th>
                                       Grand Total
                                    </th>
                                     <th>
                                       Paid Amount
                                    </th>
                                     <th>
                                       Balance
                                    </th>
                                    <th>
                                       Payment Mode
                                    </th>
                                   
                                </tr>
                            </thead>
                            <tbody id="tbodypurchaseLedger" runat="server"  style="text-align: center">
                            </tbody>
                        </table>
                    </div>
</div>
             </div>
        </div>

           
    </div>

    </div>
     
</asp:Content>


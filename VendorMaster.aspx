<%@ Page Title="" Language="C#" MasterPageFile="~/inventoryMasterPage.master" AutoEventWireup="true" CodeFile="VendorMaster.aspx.cs" Inherits="VendorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <nav class="navbar navbar-expand navbar-light bg-white topbar mb-2 static-top shadow">
          <h3 class="text-dark d-none d-sm-block" style="width:100%;"><span class="text-primary">Vendor Master </span> </h3>
        
                    <!-- Topbar Navbar -->
                       
        </nav>
    <div class="wrapper" style="margin-left:20px;">
        <div id="content">
       <div class="table_div">
                    <div class="container">
                        <div class="row testDetail ">
                            <div class="col col-md-4">
                               <div class="panel panel-primary filterable">
            <div class="panel-heading"></div>
             <div class="panel-body">
                 <div class="form-group">
                 <label>Vendor Name <span style="color:Red;">*</span></label>
                    <asp:TextBox class="form-control" placeholder="Vendor Name" ID="txtvendorName" runat="server"
                        ClientIDMode="Static"></asp:TextBox>
                </div>
                  <div class="form-group">
                 <label>Company Name <span style="color:Red;">*</span></label>
                    <asp:TextBox class="form-control" placeholder="Company Name" ID="txtcompanyName" runat="server"
                        ClientIDMode="Static"></asp:TextBox>
                </div>
                 <div class="form-group">
                 <label>Address </label>
                    <asp:TextBox class="form-control" placeholder="Address" ID="txtaddress" runat="server" TextMode="MultiLine" style="height:100px; resize:none;"
                        ClientIDMode="Static"></asp:TextBox>
                </div>
                 <div class="form-group">
                 <label>Mobile No <span style="color:Red;">*</span></label>
                    <asp:TextBox class="form-control" placeholder="Mobile No" ID="txtmobileNo" runat="server" MaxLength="10" onkeypress="return isNumber(event)"
                        ClientIDMode="Static"></asp:TextBox>
                </div>
                  <div class="form-group">
                 <label>Pan No </label>
                    <asp:TextBox class="form-control" placeholder="Pan No" ID="txtpanno" runat="server" MaxLength="10" 
                        ClientIDMode="Static"></asp:TextBox>
                </div>
                 <div class="form-group">
                 <label>GSTIN No </label>
                    <asp:TextBox class="form-control" placeholder="GSTIN No" ID="txtgstinNo" runat="server" MaxLength="15" 
                        ClientIDMode="Static"></asp:TextBox>
                </div>
                <hr />
                 <div class="form-group text-center">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-secondary" 
                         onclick="btnAdd_Click" />
                </div>
             </div>
            
        </div>
                            </div>
                            <div class="col col-md-8">
                              <div class="panel panel-primary filterable">
            <div class="panel-heading"></div>
             <div class="panel-body">
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
                                        Vendor Name
                                    </th>
                                    <th>
                                       Company
                                    </th>
                                    <th>
                                       Mobile No
                                    </th>
                                    <th>
                                       GSTIN No
                                    </th>
                                    <th>
                                       Address
                                    </th>
                                   
                                </tr>
                            </thead>
                            <tbody id="tbodyMaterialMaster" runat="server"  style="text-align: center">
                            </tbody>
                        </table>
                    </div>
</div>
             </div>
        </div>
                            </div>
                        </div>
                        
                    </div>
                </div>

           
    </div>

    </div>
     
</asp:Content>


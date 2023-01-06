<%@ Page Title="" Language="C#" MasterPageFile="~/inventoryMasterPage.master" AutoEventWireup="true" CodeFile="materialMaster.aspx.cs" Inherits="materialMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <nav class="navbar navbar-expand navbar-light bg-white topbar mb-2 static-top shadow">
          <h3 class="text-dark d-none d-sm-block" style="width:100%;"><span class="text-primary">Material Master </span> </h3>
        
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
                 <label>Material Name <span style="color:Red;">*</span></label>
                    <asp:TextBox class="form-control" placeholder="Material Name" ID="txtmaterialName" runat="server"
                        ClientIDMode="Static"></asp:TextBox>
                </div>
                 <div class="form-group">
                 <label>Unit <span style="color:Red;">*</span></label>
                    <asp:DropDownList ID="drpunit" runat="server"  ClientIDMode="Static" class="form-control">
                    <asp:ListItem>-Select Unit-</asp:ListItem>
                    <asp:ListItem>Kg</asp:ListItem>
                     <asp:ListItem>No</asp:ListItem>
                    </asp:DropDownList>
                </div>
                 <div class="form-group">
                 <label>Description </label>
                    <asp:TextBox class="form-control" placeholder="Description" ID="txtdescription" runat="server" TextMode="MultiLine" style="height:100px; resize:none;"
                        ClientIDMode="Static"></asp:TextBox>
                </div>
                 <div class="form-group">
                 <label>Minimum Stock </label>
                    <asp:TextBox class="form-control" placeholder="Minimum Stock" ID="txtminimumStock" runat="server"
                        ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="form-group">
                 <label>CGST in % </label>
                    <asp:TextBox class="form-control" Text="0" ID="txtcgst" runat="server"
                        ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="form-group">
                 <label>SGST in % </label>
                    <asp:TextBox class="form-control" Text="0" ID="txtsgst" runat="server"
                        ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="form-group">
                 <label>IGST in % </label>
                    <asp:TextBox class="form-control" Text="0" ID="txtigst" runat="server"
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
                                        Material Name
                                    </th>
                                    <th>
                                       Unit
                                    </th>
                                    <th>
                                       Min Stock
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


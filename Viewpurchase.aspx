<%@ Page Title="" Language="C#" MasterPageFile="~/inventoryMasterPage.master" AutoEventWireup="true" CodeFile="Viewpurchase.aspx.cs" Inherits="Viewpurchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <nav class="navbar navbar-expand navbar-light bg-white topbar mb-2 static-top shadow">
          <h3 class="text-dark d-none d-sm-block" style="width:100%;"><span class="text-primary">View Purchase Details </span> <span class="text-dgreen" style="float:right;">  <a href="purchaseLedger.aspx" id="A2"  runat="server" class="btn btn-secondary"><span><i class="fa fa-plus mr-2" area-hidden="true"></i></span> View Ledger</a></span> </h3>
        
                    <!-- Topbar Navbar -->
                       
        </nav>
    <div class="wrapper" style="margin-left:20px;">
        <div id="content">
       <div class="table_div">

                    <div class="container">
                        <div class="row">
                            <div class="col col-md-2">
                            <label>Vendor Name<span style="color:Red;">*</span></label>
                            </div>
                            <div class="col col-md-3">
                                <asp:TextBox ID="txtvendorName" class="form-control" runat="server" style="margin-left: -60px;"></asp:TextBox>
                            </div>
                            <div class="col col-md-3"></div>
                             <div class="col col-md-1">
                            <label>Date<span style="color:Red;">*</span></label>
                            </div>
                            <div class="col col-md-2">
                                   <asp:TextBox class="form-control" placeholder="Select Date" ID="txtdate" runat="server" style="width:185px;"
                        ClientIDMode="Static"></asp:TextBox>
                                   <asp:CalendarExtender ID="txtdate_CalendarExtender" runat="server" 
                                       DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                                       TargetControlID="txtdate" TodaysDateFormat="dd/MM/yyyy">
                                   </asp:CalendarExtender>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col col-md-2">
                            <label>Invoice No</label>
                            </div>
                            <div class="col col-md-2">
                                <asp:TextBox class="form-control" Text="0" ID="txtinvoiceNo" runat="server" style="margin-left:-60px;"
                        ClientIDMode="Static"></asp:TextBox>
                            </div>
                           <div class="col col-md-2">
                            <label>GRN No</label>
                            </div>
                            <div class="col col-md-2">
                                <asp:TextBox class="form-control" Text="0" ID="txtgrnNo" runat="server" style="margin-left:-60px;"
                        ClientIDMode="Static"></asp:TextBox>
                            </div>
                             <div class="col col-md-2">
                            <label>Bill Type</label>
                            </div>
                            <div class="col col-md-2">
                                    <asp:DropDownList ID="drpbillType" runat="server"  ClientIDMode="Static" class="form-control" style="width:185px; margin-left:-85px;">
                                    <asp:ListItem>Cash</asp:ListItem>
                                     <asp:ListItem>Credit</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col col-md-2">
                            <label>Type</label>
                            </div>
                            <div class="col col-md-3">
                                <asp:DropDownList ID="drptype" runat="server"  ClientIDMode="Static" class="form-control" style="margin-left:-60px;">
                                    <asp:ListItem>Invoice</asp:ListItem>
                                     <asp:ListItem>Chalan</asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                           <div class="col col-md-3"></div>
                             <div class="col col-md-2">
                            <label>GSTIN No</label>
                            </div>
                            <div class="col col-md-2">
                                <asp:Label ID="lblgstinNo" runat="server" Text="Label" style="width:185px; margin-left:-85px; color:#007bff!important"></asp:Label>
                            </div>
                        </div><br />
                      <br />
                             <div class="row"  >
                             <div style="width:100%;">
                                <asp:GridView ID="temp_material" runat="server" DataKeyNames="purchaseDetailsId" CssClass="table table-striped table-bordered"
             AutoGenerateColumns="False"
            EmptyDataText = "Material Details Not Showing"  >
                             <Columns>
                              
             <asp:TemplateField HeaderText="Material Name" >
                    <ItemTemplate>
                        <asp:Label ID="lblmname"  runat="server" Text='<%# Bind("materialName") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="Rate" >
                    <ItemTemplate>
                        <asp:Label ID="lblrate"  runat="server" Text='<%# Bind("rate") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="Qty" >
                    <ItemTemplate>
                        <asp:Label ID="lblqty"  runat="server" Text='<%# Bind("qty") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="Unit" >
                    <ItemTemplate>
                        <asp:Label ID="lblunit"  runat="server" Text='<%# Bind("unit") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="Amount" >
                    <ItemTemplate>
                        <asp:Label ID="lblamount"  runat="server" Text='<%# Bind("netAmount") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                    <asp:TemplateField HeaderText="CGST" >
                    <ItemTemplate>
                        <asp:Label ID="lblcgst"  runat="server" Text='<%# Bind("cgst") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="SGST" >
                    <ItemTemplate>
                        <asp:Label ID="lblsgst"  runat="server" Text='<%# Bind("sgst") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="IGST" >
                    <ItemTemplate>
                        <asp:Label ID="lbligst"  runat="server" Text='<%# Bind("igst") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="Total" >
                    <ItemTemplate>
                        <asp:Label ID="lbltotal"  runat="server" Text='<%# Bind("grandTotal") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                 
                             </Columns>
                             </asp:GridView>
                               </div>
                             </div>
                             <br />

                             <div class="row">
                               <div class="col col-md-6">
                                 
                                    <label>Description</label>
                               
                                      <asp:TextBox class="form-control"  ID="txtdescription" runat="server" style="height:100px; resize:none;" TextMode="MultiLine"
                        ClientIDMode="Static"></asp:TextBox>
                              
                            </div>
                            <div class="col col-md-2"></div>
                            <div class="col col-md-4" style="text-align:right;">
                                <table>
                                    <tr>
                                    <td><b>Net Amount</b>  </td>
                                    <td>:</td>
                                    <td style="font-size:large"><b>
                                        <asp:Label ID="lblnetamt" runat="server" Text="0.00"></asp:Label></b></td>
                                    </tr>
                                  
                                    <tr>
                                    <td>CGST   </td>
                                    <td>:</td>
                                    <td> <asp:Label ID="lblcgst" runat="server" Text="0.00"></asp:Label></td>
                                    </tr>
                                     <tr>
                                    <td>SGST </td>
                                    <td>:</td>
                                    <td> <asp:Label ID="lblsgst" runat="server" Text="0.00"></asp:Label></td>
                                    </tr>
                                     <tr>
                                    <td>IGST  </td>
                                    <td>:</td>
                                    <td> <asp:Label ID="lbligst" runat="server" Text="0.00"></asp:Label></td>
                                    </tr>
                                     <tr>
                                    <td><b>Grand Total </b> </td>
                                    <td>:</td>
                                    <td style="font-size:large"><b> <asp:Label ID="lblgrandTotal" runat="server" Text="0.00"></asp:Label></b></td>
                                    </tr>
                                </table>
                            </div>
                             </div>
                                <br />
                                <hr />
                             <div class="row" >
                             <div style="margin-left:200px;">
                               <a class="btn btn-danger" href="purchaseLedger.aspx">Close</a>
                                
                             </div>
                                
                             </div>
                    </div>
                </div>

           
    </div>

    </div>
     
</asp:Content>


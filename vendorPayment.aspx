<%@ Page Title="" Language="C#" MasterPageFile="~/inventoryMasterPage.master" AutoEventWireup="true" CodeFile="vendorPayment.aspx.cs" Inherits="vendorPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <nav class="navbar navbar-expand navbar-light bg-white topbar mb-2 static-top shadow">
          <h3 class="text-dark d-none d-sm-block" style="width:100%;"><span class="text-primary">Vendor Payment </span> <span class="text-dgreen" style="float:right;">  <a href="VendorLedger.aspx" id="A2"  runat="server" class="btn btn-secondary"><span><i class="fa fa-plus mr-2" area-hidden="true"></i></span> View Vendor Ledgar</a></span> </h3>
        
                    <!-- Topbar Navbar -->
                       
        </nav>
    <div class="wrapper" style="margin-left:20px;">
        <div id="content">
       <div class="table_div">

                    <div class="container" style="min-height:300px;">
                        <div class="row">
                            <div class="col col-md-2">
                            <label>Vendor Name<span style="color:Red;">*</span></label>
                            </div>
                            <div class="col col-md-3">
                                 <asp:DropDownList ID="drpvendorName" runat="server"  ClientIDMode="Static" 
                                     class="form-control" style="margin-left:-60px;" AutoPostBack="True" 
                                     onselectedindexchanged="drpvendorName_SelectedIndexChanged">
                                  
                                    </asp:DropDownList>
                            </div>
                            <div class="col col-md-3"></div>
                             <div class="col col-md-1">
                            <label>Date</label>
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
                        <br />
                             <div class="row"  >
                             <div style="width:100%;">
                                <asp:GridView ID="gridvm" runat="server"  CssClass="table table-striped table-bordered"
             AutoGenerateColumns="False"
            EmptyDataText = "Vendor Payment Not Showing"   >
                            <Columns>
                                   <asp:TemplateField HeaderText="Sr No" >
                                        <ItemTemplate>
                                                        <asp:Label ID="lblsrno" runat="server" Text='<%# Bind("srno") %>'>></asp:Label>
                                                    </ItemTemplate>
                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Check" >
                                        <ItemTemplate>
                                                       <asp:CheckBox ID="chkseletedValues" style="margin-left:25px;" runat="server" 
                                                           AutoPostBack="True" oncheckedchanged="chkseletedValues_CheckedChanged"    ></asp:CheckBox>
                                                    </ItemTemplate>
                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bill No" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltn" runat="server" Text='<%# Bind("invoiceNo") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                           
                                                <asp:TemplateField HeaderText="Client Name" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblot"  runat="server" Text='<%# Bind("vendorName") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Total Amount" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrtlt" runat="server" Text='<%# Bind("grandTotal") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Paid Amount" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltaxamt" runat="server" Text='<%# Bind("paidAmount") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Balance" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpdamt" runat="server" Text='<%# Bind("balanceAmt") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Status" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("status") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                 
                                                  
                                                 
                                                 
                                </Columns>
                             </asp:GridView>
                               </div>
                             </div>
                             <br />
                             <div class="row">
                              <div class="col col-md-6">
                              <table>
                                    <tr>
                                    <td>Bill Selected  </td>
                                    <td>:</td>
                                    <td style="font-size:large"><b>
                                        <asp:TextBox ID="txtbillselected" CssClass="form-control" runat="server"></asp:TextBox></b></td>
                                    </tr>
                                  
                                    <tr>
                                    <td>Payment Mode   </td>
                                    <td>:</td>
                                    <td> 
                                        <asp:DropDownList ID="drppaymode" CssClass="form-control" runat="server" 
                                            AutoPostBack="True" onselectedindexchanged="drppaymode_SelectedIndexChanged">
                                        <asp:ListItem>-Select Mode-</asp:ListItem>
                                        <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Online</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    </tr>
                                     <tr id="online" runat="server" visible="false">
                                    <td>Transaction Id   </td>
                                    <td>:</td>
                                    <td> 
                                       <asp:TextBox ID="txttransactionId" CssClass="form-control" runat="server"></asp:TextBox>
                                    </td>
                                    </tr>
                                     <tr>
                                    <td>Remark </td>
                                    <td>:</td>
                                    <td> <asp:TextBox ID="txtremark" CssClass="form-control" runat="server" TextMode="MultiLine" style="height:100px; resize:none;"></asp:TextBox></td>
                                    </tr>
                                    
                                </table>
                              </div>
                                <div class="col col-md-6">
                                <table>
                                    <tr>
                                    <td>Total Amount  </td>
                                    <td>:</td>
                                    <td style="font-size:large"><b>
                                        <asp:TextBox ID="txtgrandtotal" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox></b></td>
                                    </tr>
                                  
                                    <tr>
                                    <td>Paid Amount   </td>
                                    <td>:</td>
                                    <td> 
                                        <asp:TextBox ID="txtpaidAmount" CssClass="form-control" runat="server" 
                                            AutoPostBack="True" ontextchanged="txtpaidAmount_TextChanged"></asp:TextBox>
                                    </td>
                                    </tr>

                                     <tr>
                                    <td>Balance Amount</td>
                                    <td>:</td>
                                    <td>   <asp:TextBox ID="txtbalance" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox></td>
                                    </tr>
                                    
                                </table>
                                </div>
                                </div>
                                <hr />
                             <div class="row" >
                             <div style="margin-left:200px;">
                              <asp:Button ID="btnsave" runat="server" Text="Save and Pay" 
                                     CssClass="btn btn-secondary" onclick="btnsave_Click" />
                               
                                 <asp:Button ID="btnclose" runat="server" Text="Close" CssClass="btn btn-danger" />
                             </div>
                                
                             </div>
                    </div>
                </div>

           
    </div>

    </div>
     
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="ViewPackages.aspx.cs" Inherits="SuperAdmin_ViewPackages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div class="wrapper">
        <div id="content">
            <nav class="primary-col-back subheader">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-sm-6">
                     <h4>View Packages</h4>
                  </div>   
                   <div class="col-sm-6 text-right">
                        <a href="packageMaster.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Package</a>
                     </div>              
               </div>
            </div>
         </nav>
          
            <!-- wrapper content -->
            <div class="wrappercontent">
                <div class="box-body" style="padding: 20px">
                <asp:GridView ID="GridView1" CssClass="Grid" runat="server" Width="100%" DataKeyNames="pMasterId"  onrowdeleting="GridView1_RowDeleting" AutoGenerateColumns="false" >
    <Columns>
     <asp:TemplateField HeaderText="Package ID" >
                    <ItemTemplate>
                        <asp:Label ID="lblpackageId"  runat="server" Text='<%# Bind("pMasterId") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
  
        <asp:BoundField DataField="packageName" HeaderText="Package Name" />
        <asp:BoundField DataField="days" HeaderText="Days" />
         <asp:BoundField DataField="price" HeaderText="Price" />
        <asp:BoundField DataField="status" HeaderText="Status" />
         <asp:BoundField DataField="description" HeaderText="Description" />
        <asp:TemplateField HeaderText="Delete">  
                                        <HeaderStyle HorizontalAlign="Left" />  
                                        <ItemStyle HorizontalAlign="Left" />  
                                        <ItemTemplate>                                                                          
                                                <asp:LinkButton runat="server" ID="btnDelete" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are You Sure You want to Delete the Record?');" ToolTip="Click here to Delete the record" />  
                                            </span>  
                                        </ItemTemplate>                                         
                                    </asp:TemplateField>  
                                     <asp:TemplateField HeaderText="Action ">
                    <ItemTemplate>
                        <asp:Button ID="btnChangeStatus" runat="server" OnClick="ChangeStatus" Text="Status" />
                    </ItemTemplate>
                </asp:TemplateField>
    </Columns>
</asp:GridView>
                    
                  
                </div>
            </div>
        </div>
    </div>
    <%--<script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var rows = $("#tbodyAllLabApproval").find("tr").hide();
            rows.filter(":contains('Pending Approval')").show();

            $("#rdoActive").change(function () {
                var rows = $("#tbodyAllLabApproval").find("tr").hide();
                rows.filter(":contains('Pending Approval')").show();
            });

            $("#rdoInactive").change(function () {
                var rows = $("#tbodyAllLabApproval").find("tr").hide();
                rows.filter(":contains('Pending Approval')").show();
            });
        });
    </script>--%>
    <%--start Pagination and sorting data --%>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
   
</asp:Content>


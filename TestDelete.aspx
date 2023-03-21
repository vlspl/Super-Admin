<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="TestDelete.aspx.cs" Inherits="SuperAdmin_TestDelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script type="text/javascript">

        $('#gridtestlist').datatable({
            pageSize: 10,
            sort: [true, true, false],
            filters: [false, false, true, true, true, true],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>


             
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Test List Delete</h4>
                     </div>
                    
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">
    <div class="col-lg-5">
     <div class="form-group">
                    <label  class="control-label col-sm-4">Select Lab</label>
                        <div class="col-sm-8">
                    <asp:DropDownList ID="drplablist" runat="server" CssClass="form-control" 
                                AutoPostBack="True" 
                                onselectedindexchanged="drplablist_SelectedIndexChanged" >
                   
                    </asp:DropDownList>
                    </div>
                </div>
    </div>
   <div style="clear:both;"><br /></div>
        <asp:GridView ID="gridtestlist" runat="server" DataKeyNames="sTestId" CssClass="table table-striped table-bordered" 
            PageSize="10" AllowPaging="True" AutoGenerateColumns="False" 
            EmptyDataText = "Test List Not Found for Selected Criteria." 
            onpageindexchanging="gridtestlist_PageIndexChanging" 
            onrowdeleting="gridtestlist_RowDeleting" >
            <Columns>
                <asp:TemplateField HeaderText="Test ID" >
                    <ItemTemplate>
                        <asp:Label ID="lbltestId"  runat="server" Text='<%# Bind("sTestId") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="Test Code" >
                    <ItemTemplate>
                        <asp:Label ID="lbltestcode"  runat="server" Text='<%# Bind("sTestCode") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="Test Name" >
                    <ItemTemplate>
                        <asp:Label ID="lbltestName"  runat="server" Text='<%# Bind("sTestName") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="Profile Name" >
                    <ItemTemplate>
                        <asp:Label ID="lblprofileName"  runat="server" Text='<%# Bind("sProfileName") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="Section Name" >
                    <ItemTemplate>
                        <asp:Label ID="lblsectionName"  runat="server" Text='<%# Bind("sSectionName") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                      <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandName="Delete" OnClientClick="return confirm('Are you sure want to delete')"  Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                     <%-- <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" ShowHeader="true" onclclick="return confirm('Are you sure you want to delete this item')" />--%>
                   
               
               
            </Columns>
        </asp:GridView>
    </div>
        
   
</asp:Content>


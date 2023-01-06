<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="AllTestList.aspx.cs" Inherits="AllTestList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
<link href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%= gridAlltext.ClientID %>').DataTable();
    });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .loader img
        {
            position: absolute;
            top: 50%;
        }
    </style>
  <%--   <script type="text/javascript"  lang="js">
         $(function () {
             $("#<%=txtsearch.ClientID %>").keypress(function () {
                 alert("Wow; Its Work!.")
             });
         });  
    </script> --%> 
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand">All Test List</a>
      </div>
        <div class="mr-5">
        <%-- <asp:TextBox ID="txtsearch" runat="server" placeholder="Search by test name" 
                    CssClass="form-control" AutoPostBack="True" ontextchanged="txtsearch_TextChanged"></asp:TextBox>--%>
       <asp:Button ID="btnaddAll" runat="server" Text="Test Add" style="margin-left:23px; margin-top:20px;"  CssClass="btn btn-info"
                    onclick="btnaddAll_Click" />
      </div>
     
   </div>
  </nav>

   <div class="container">
        <div class="row">
        
                
            <div class="col-lg-12" >
                 
                <asp:GridView ID="gridAlltext" CssClass="table table-striped table-bordered" runat="server" AutoGenerateColumns="false"  EmptyDataText="Test List Data Not Showing">
                  <Columns>

                    <asp:TemplateField HeaderText="Select" >
                                                   
                                                    <ItemTemplate>
                                                       <asp:CheckBox ID="chkselect"   runat="server"></asp:CheckBox>
                                                    </ItemTemplate>
                                                  
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Test ID" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1"  runat="server" Text='<%# Bind("sTestId") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                  
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Test Code" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2"  runat="server" Text='<%# Bind("sTestCode") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                  
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Test Name" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3"  runat="server" Text='<%# Bind("sTestName") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Profile Name" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4"  runat="server" Text='<%# Bind("sProfileName") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Section Name" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5"  runat="server" Text='<%# Bind("sSectionName") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                  
                                                </asp:TemplateField>
                                                <%--  <asp:TemplateField HeaderText="Action" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button1" runat="server"  Text="Add" />
                                                    </ItemTemplate>
                                                  
                                                </asp:TemplateField>--%>
                                                     
                                                  
                                                
                                           </Columns>


                </asp:GridView>
              
            </div>
          
             <div class="mr-5">
      
      </div>
        <div class="mr-5" style="display:none;">
        <p style="margin-top:25px;">Number of Records&nbsp;:&nbsp;<asp:Label ID="lblusercount" runat="server" Text="0"></asp:Label> </p>
        </div>
            </div>
            </div>

                <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script type="text/javascript">

        $('#gridAlltext').datatable({
            pageSize: 10,
            sort: [true, true, false],
            filters: [true, true, true, true, false],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>


  
  
   
</asp:Content>

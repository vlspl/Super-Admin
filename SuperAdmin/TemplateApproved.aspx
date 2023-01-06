<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="TemplateApproved.aspx.cs" Inherits="SuperAdmin_TestDelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
<link href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%= GridView1.ClientID %>').DataTable();
    });
</script>

             
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Personalize Template Approval</h4>
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
        <asp:HiddenField ID="hdntemplateId" runat="server" />

         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6" OnRowCancelingEdit="GridView1_RowCancelingEdit" EmptyDataText="No Data Found"  
  
OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">  
            <Columns>  
                <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                    </EditItemTemplate>  
                </asp:TemplateField>  
               <asp:TemplateField HeaderText="Template ID" >
                    <ItemTemplate>
                        <asp:Label ID="lbltestId"  runat="server" Text='<%# Bind("whatsappMasterId") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="Template Name" >
                    <ItemTemplate>
                        <asp:Label ID="lbltestcode"  runat="server" Text='<%# Bind("msgName") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="Body" >
                    <ItemTemplate>
                        <asp:Label ID="lbltestName"  runat="server" Text='<%# Bind("body") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="Parameter List" >
                    <ItemTemplate>
                        <asp:Label ID="lblprofileName"  runat="server" Text='<%# Bind("paramList") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_City" runat="server" Text='<%#Eval("status") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>
                        <asp:DropDownList ID="drp_status" runat="server" CssClass="form-control">
                        <asp:ListItem>-Select-</asp:ListItem>
                        <asp:ListItem>Approved</asp:ListItem>
                        <asp:ListItem>Rejected</asp:ListItem>
                        </asp:DropDownList>
                      
                    </EditItemTemplate>  
                </asp:TemplateField>  
            </Columns>  
            <HeaderStyle BackColor="#663300" ForeColor="#ffffff"/>  
            <RowStyle BackColor="#e7ceb6"/>  
        </asp:GridView>  







       
    </div>
        
   
</asp:Content>


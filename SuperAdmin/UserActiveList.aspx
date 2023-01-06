<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="UserActiveList.aspx.cs" Inherits="SuperAdmin_UserActive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>User Activation List</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                       
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">
   
   <div style="clear:both;"><br /></div>
        <asp:GridView ID="griduserActivation" runat="server" DataKeyNames="Id"  CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText = "User Activation List Not Found"
            onpageindexchanging="griduserActivation_PageIndexChanging" 
            onrowcommand="griduserActivation_RowCommand" >
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Sr No" />
                <asp:BoundField DataField="sLabName" HeaderText="Lab Name" />
                 <asp:BoundField DataField="sFullName" HeaderText="Full Name" />
                <asp:BoundField DataField="Role" HeaderText="Role" />
                
                <asp:BoundField DataField="loginAttemptCounter" HeaderText="login Counter" />
                 <asp:BoundField DataField="loginStatus" HeaderText="login Status" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                       <asp:Button ID="Button1" Text="Active" runat="server" CssClass="btn btn-info" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:HiddenField ID="hdnid" runat="server" />
        <div style="clear:both;"><br /><br /></div>
       
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="EditRoles.aspx.cs" Inherits="EditRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand">User Name : <asp:Label ID="username" runat="server" Text="" ></asp:Label></a>
      </div>

      <div class="mr-5">
        <ul class="navbar-nav ml-auto">
          <li class="nav-item pt-1 mr-2">
         
          </li>
           <li class="nav-item pt-1">
             <a href="ManageUsers.aspx"  class="btn btn-color"><i class="fa fa-arrow-left mr-2" aria-hidden="true"></i>Back</a>
           
          </li>
        </ul>
      </div>
    </div>
  </nav>
    <div class="wrappercontent">
        <div class="col-md-12" style="height:420px; overflow:auto;">
            <asp:GridView ID="gridEditRole" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
            <Columns>
                 <asp:TemplateField HeaderText="Page ID" >
                                                     
                        <ItemTemplate>
                            <asp:Label ID="Label1"  runat="server" Text='<%# Bind("sRoleTypeId") %>'>></asp:Label>
                        </ItemTemplate>
                                                  
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Page Name" >
                                                     
                        <ItemTemplate>
                            <asp:Label ID="Label2"  runat="server" Text='<%# Bind("sPageDisplayName") %>'>></asp:Label>
                        </ItemTemplate>
                                                  
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Add" >
                              <HeaderTemplate>
                          <asp:CheckBox ID="checkAddAll" runat="server" AutoPostBack="true" OnCheckedChanged = "checkAddAll" Text="Add" />

                        </HeaderTemplate>                        
                        <ItemTemplate>
                             <asp:CheckBox ID="chkadd" Checked='<%#Convert.ToBoolean(Eval("sAdd")) %>'  runat="server"></asp:CheckBox>
                        </ItemTemplate>
                                                  
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" >
                                <HeaderTemplate>
                          <asp:CheckBox ID="checkEditAll" runat="server" AutoPostBack="true"  OnCheckedChanged = "checkEditAll" Text="Edit"/>

                        </HeaderTemplate>                             
                        <ItemTemplate>
                             <asp:CheckBox ID="chkedit" Checked='<%#Convert.ToBoolean(Eval("sEdit")) %>'   runat="server"></asp:CheckBox>
                        </ItemTemplate>
                                                  
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="View" >
                                                     
                        <ItemTemplate>
                              <asp:CheckBox ID="chkview" Checked='<%#Convert.ToBoolean(Eval("sView")) %>'    runat="server"></asp:CheckBox>
                        </ItemTemplate>
                                                  
                    </asp:TemplateField>
            </Columns>
            </asp:GridView>
        </div>
        <div style="clear:both;"><br /></div>
        <asp:UpdatePanel ID="upurole" runat="server">
        <ContentTemplate>
        
       
        <span style="float: right;  padding-right: 30px;">
            <asp:Button ID="btneditrole" runat="server" CssClass="btn btn-info" 
            Text="Update Roles" onclick="btneditrole_Click" />
        </span>
         </ContentTemplate>
        </asp:UpdatePanel>
          <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upurole">
                    <ProgressTemplate>
                        <div class="text form_loader">
                            <img src="../../images/Loader.gif" alt="Loading">
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
    </div>
    
</asp:Content>

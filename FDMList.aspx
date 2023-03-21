<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="FDMList.aspx.cs" Inherits="SuperAdmin_FDMList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>User Profile FDM List</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                        <a href="AddUserProfileFDMList.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add User Profile FDM List</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">
    <div class="col-lg-5">
     <div class="form-group">
                    <label  class="control-label col-sm-4">Select Type</label>
                        <div class="col-sm-8">
                    <asp:DropDownList ID="drpfdmType" runat="server" CssClass="form-control" 
                                AutoPostBack="True" onselectedindexchanged="drpfdmType_SelectedIndexChanged">
                    <asp:ListItem>-Select-</asp:ListItem>
                    <asp:ListItem>Food</asp:ListItem>
                    <asp:ListItem>Drug</asp:ListItem>
                     <asp:ListItem>Medical Condition</asp:ListItem>
                      <asp:ListItem>Family History</asp:ListItem>
                    </asp:DropDownList>
                    </div>
                </div>
    </div>
   <div style="clear:both;"><br /></div>
        <asp:GridView ID="gridFDMList" runat="server" DataKeyNames="listId"  CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText = "User Profile FDM List Not Found"
            onpageindexchanging="gridFDMList_PageIndexChanging" >
            <Columns>
                <asp:BoundField DataField="listId" HeaderText="Sr No" />
                <asp:BoundField DataField="name" HeaderText="Name" />
                <asp:BoundField DataField="type" HeaderText="Type" />
                <asp:BoundField DataField="remark" HeaderText="Remark" />
                <asp:BoundField DataField="status" HeaderText="Status" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                       <a title="Edit FDM List" href='AddUserProfileFDMList.aspx?listId=<%# Eval("listId") %>' class='btn btn-secondary'> <i class="fa fa-edit fa-2x"></i></a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>


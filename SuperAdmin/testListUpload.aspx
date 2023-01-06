<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="testListUpload.aspx.cs" Inherits="SuperAdmin_testListUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Test List Upload</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                       
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">
    <div class="col-lg-4">
    <br />
     <div class="form-group">
                    <label  class="control-label col-sm-4">Select File</label>
                        <div class="col-sm-8">
                            <asp:FileUpload ID="FileUpload_testList" runat="server" />
                    </div>
                </div>
    </div>
     <div class="col-lg-5">
     <br />
         <asp:Button ID="btnupload" runat="server" Text="Upload" CssClass="btn btn-info" onclick="btnupload_Click" 
             />
     </div>
   <div style="clear:both;"><br /></div>
        <asp:GridView ID="gridFDMList" runat="server" DataKeyNames="listId"  CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText = "User Profile FDM List Not Found"
           >
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


<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="healthArtical.aspx.cs" Inherits="SuperAdmin_healthArtical" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
     <script type="text/javascript">
         function deleteitem() {
             if (confirm("Are you sure you want to Deactive...? ")) {
                 return true;
             }
             return false;
         }
    </script>
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>View Health Articles</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                        <a href="addHealthArtical.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add New Articles</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">

        <asp:GridView ID="gridhealthArtical" runat="server" DataKeyNames="healthArticalId"  CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="true" AutoGenerateColumns="False" >
            <Columns>
               <asp:BoundField DataField="healthArticalId" HeaderText="ID" />
                <asp:BoundField DataField="title" HeaderText="Title" />
                <asp:BoundField DataField="description" HeaderText="Description" />
                 <asp:BoundField DataField="status" HeaderText="Status" />
              <asp:TemplateField HeaderText="Action " ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Button ID="btnChangeStatus" runat="server" OnClick="ChangeStatus" Text="Change Status" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
   
</asp:Content>


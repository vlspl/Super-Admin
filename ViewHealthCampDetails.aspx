<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="ViewHealthCampDetails.aspx.cs" Inherits="SuperAdmin_ViewHealthCampDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script type="text/javascript">
      function deleteitem() {
          if (confirm("Are you sure you want to delete...? ")) {
              return true;
          }
          return false;
      }
    </script>
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Health Camp Master Management</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                        <a href="HealthCampMaster.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Health Camp Master</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">

        <asp:GridView ID="grdviewOrgnization" runat="server"  CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="true" AutoGenerateColumns="False" OnPageIndexChanging="grdviewOrgnization_PageIndexChanging"
            OnRowDeleting="grdviewOrgnization_RowDeleting" OnRowDataBound="grdviewOrgnization_RowDataBound"
            OnSelectedIndexChanged="grdviewOrgnization_SelectedIndexChanged">
            <Columns>
               <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="30">
                                            <ItemTemplate>
                                                 <%#Container.DataItemIndex+1 %>
                                             
                                            </ItemTemplate>
                                        </asp:TemplateField>
                <asp:BoundField DataField="healthCampName" HeaderText="Camp Name" />
                <asp:BoundField DataField="ownerName" HeaderText="Owner Name" />
                <asp:BoundField DataField="Name" HeaderText="Organization Name" />
                <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                <asp:BoundField DataField="sLabName" HeaderText="Lab Name" />
                <asp:BoundField DataField="sFullName" HeaderText="Technician Name" />
                 <asp:BoundField DataField="sMobile" HeaderText="Mobile No" />
                 <asp:BoundField DataField="Password" HeaderText="Password" />
              <asp:TemplateField HeaderText="Test ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbltestId"  runat="server" Text='<%# Bind("healthcampID") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandName="Delete" OnClientClick="return confirm('Are you sure want to delete healthcamp')"  Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="pad" style="color: Red">
        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
</asp:Content>


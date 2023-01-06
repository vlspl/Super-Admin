<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="ViewOrgnization.aspx.cs" Inherits="SuperAdmin_ViewOrgnization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                        <h4>Organization Management</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                        <a href="AddOrganization.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Organization</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">
    <span style="float:right;">Number Of Records&nbsp;:&nbsp;<asp:Label ID="lbltotalcount" runat="server" Text="Label"></asp:Label></span>
        <asp:GridView ID="grdviewOrgnization" runat="server" DataKeyNames="ID"  CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="true" AutoGenerateColumns="False" OnPageIndexChanging="grdviewOrgnization_PageIndexChanging"
            OnRowDeleting="grdviewOrgnization_RowDeleting" 
            OnSelectedIndexChanged="grdviewOrgnization_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField HeaderText="Logo">
                    <ItemTemplate>
                        <img src="<%# Eval("Org_Logo")%>" height="40px" width="80px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Address" HeaderText="Address" />
                <asp:BoundField DataField="Mobile" HeaderText="Contact" />
                <asp:BoundField DataField="EmailId" HeaderText="Email" />
                 <asp:BoundField DataField="ID" HeaderText="ID" Visible="false"/>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                    <!-- <asp:ImageButton ID="img_user" runat="server" CommandName="Delete" OnClientClick="return deleteitem();" ImageUrl='~/images/Active.png'
                        Width="30px" Height="30px" Visible='<%# Eval("IsActive").ToString() == "True" ? true : false %>' ></asp:ImageButton>
                          <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ImageUrl='~/images/Deactive.jpg'
                        Width="30px" Height="30px" Visible='<%# Eval("IsActive").ToString() == "False" ? true : false %>' ></asp:ImageButton>-->
                      <a href='AddOrganization.aspx?ID=<%# Eval("ID") %>' > Edit</a>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View">
                    <ItemTemplate>
                       <a href='OrgnizationUserList.aspx?ID=<%# Eval("Id") %>&Name=<%# Eval("Name") %>'> View</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="pad" style="color: Red">
        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
</asp:Content>

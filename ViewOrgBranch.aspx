<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="ViewOrgBranch.aspx.cs" Inherits="SuperAdmin_ViewOrgBranch" %>

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
                        <h4>Organization Branch Management</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                      <a href="AddOrganization.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Organization</a>
                        <a href="AddOrgBranch.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Branch</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:25px" >
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Organization Name <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="ddlName" DataTextField="Name" DataValueField="ID" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True" OnSelectedIndexChanged="ddlName_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
      
         <span id="spcount" runat="server">Number Of Records&nbsp;:&nbsp;<asp:Label ID="lbltotalcount" runat="server" Text="Label"></asp:Label></span>
        <br />
        <asp:GridView ID="grdviewOrgBranch" runat="server" DataKeyNames="ID" CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="true" AutoGenerateColumns="False" OnPageIndexChanging="grdviewOrgBranch_PageIndexChanging"
            OnRowDeleting="grdviewOrgBranch_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <%# Eval("BranchName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address">
                    <ItemTemplate>
                        <%# Eval("BranchAddress")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mobile ">
                    <ItemTemplate>
                        <%# Eval("Mobile")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email ">
                    <ItemTemplate>
                        <%# Eval("EmailId")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="Button2" CommandName="Delete" ToolTip="Delete" runat="server"
                            OnClientClick="return deleteitem();" CssClass="fa fa-trash "></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
        <div class="pad" style="color: Red">
            <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
</asp:Content>

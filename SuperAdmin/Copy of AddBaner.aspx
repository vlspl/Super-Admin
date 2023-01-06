<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="Copy of AddBaner.aspx.cs" Inherits="SuperAdmin_plugins_AddBaner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Baner Management</h4>
                     </div>
                     <%-- <div class="col-sm-6 text-right">
                        <a href="ViewOrgnization.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Organization</a>
                     </div>--%>
                     
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 25px">
        <div class="row">
            <div class="col-lg-4 col-md-4">
                <div class="form-group">
                    <label>
                        Baner<span style="color: Red">*</span></label>
                    <asp:FileUpload ID="OrgLogo" CssClass=" btn btn-default" runat="server" />
                </div>
            </div>
            <br />
            <div class="col-lg-1 col-md-1">
                <asp:Button ID="BtnSave" runat="server" type="submit" class="fa fa-save btn btn-lg btn-primary"
                    Text="Save" OnClick="BtnSave_Click" />
                <div class="pad">
                    <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
            </div>
        </div>
        <asp:GridView ID="grdviewOrgnization" runat="server" DataKeyNames="ID" CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="true" AutoGenerateColumns="False" OnPageIndexChanging="grdviewOrgnization_PageIndexChanging"
            OnRowDeleting="grdviewOrgnization_RowDeleting" OnSelectedIndexChanged="grdviewOrgnization_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField HeaderText="Baner">
                    <ItemTemplate>
                        <img src="../SuperAdmin/images/Baner/<%# Eval("Path")%>" height="40px" width="80px" />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="UploadDate" HeaderText="Upload Date" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:ImageButton ID="img_user" runat="server" CommandName="Delete" OnClientClick="return deleteitem();"
                            ImageUrl='~/images/Active.png' Width="30px" Height="30px" Visible='<%# Eval("IsActive").ToString() == "True" ? true : false %>'>
                        </asp:ImageButton>
                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ImageUrl='~/images/Deactive.jpg'
                            Width="30px" Height="30px" Visible='<%# Eval("IsActive").ToString() == "False" ? true : false %>'>
                        </asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

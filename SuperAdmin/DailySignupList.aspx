<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="DailySignupList.aspx.cs" Inherits="SuperAdmin_DailySignupList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-10">
                        <h4>New Users List</h4>
                     </div>
                      <div class="col-lg-2 pt-3">
                    <asp:Button ID="BtnSave" runat="server" type="submit" class="lab-btn-white"
                        Text="Export To Excel" OnClick="BtnSave_Click" />
                </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">
             <asp:GridView ID="grdviewSignUpDaily" runat="server"  CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="true" AutoGenerateColumns="False" OnPageIndexChanging="grdviewSignUpDaily_PageIndexChanging"
            EmptyDataText="No record found">
            <Columns>
                <asp:TemplateField HeaderText="Sr.No.">
                    <ItemTemplate>
                        <%# Eval("RN")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Name">
                    <ItemTemplate>
                        <%# Eval("sFullName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address ">
                    <ItemTemplate>
                        <%# Eval("sAddress")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mobile ">
                    <ItemTemplate>
                        <%# Eval("Mobile")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="EmailId ">
                    <ItemTemplate>
                        <%# Eval("EmailId")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Role ">
                    <ItemTemplate>
                        <%# Eval("sRole")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Registered From ">
                    <ItemTemplate>
                        <%# Eval("sRegisteredFrom")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="pad" style="color: Red">
        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
</asp:Content>


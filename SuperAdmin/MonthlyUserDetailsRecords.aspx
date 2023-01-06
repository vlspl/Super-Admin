<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="MonthlyUserDetailsRecords.aspx.cs" Inherits="SuperAdmin_MonthlyUserDetailsRecords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Monthly Users Records</h4>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 25px">
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Month List <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="ddlMonth"  class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <asp:GridView ID="grdviewUserList" runat="server"  CssClass="table table-bordered table-hover table-striped"
            PageSize="31" AutoGenerateColumns="False" EmptyDataText="No record found">
            <Columns>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <%# Eval("Date")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Patient">
                    <ItemTemplate>
                        <%# Eval("Patient")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Doctor ">
                    <ItemTemplate>
                        <%# Eval("Doctor")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee ">
                    <ItemTemplate>
                        <%# Eval("Employee")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="pad" style="color: Red">
            <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
</asp:Content>

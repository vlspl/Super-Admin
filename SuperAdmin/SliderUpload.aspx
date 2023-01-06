<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="SliderUpload.aspx.cs" Inherits="SuperAdmin_SliderUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Slider Upload</h4>
                     </div>
                     <%-- <div class="col-sm-6 text-right">
                        <a href="ViewOrgnization.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Organization</a>
                     </div>--%>
                     
                  </div>
               </div>
            </nav>
             <div class="container mt-5" style="width: 850px;">

             <h2 class="text-center mb-5">
                Slider Upload</h2>
    <br />
    <div class="row">
        <div class="col-md-12">
         <div class="col-md-3">
             <asp:TextBox ID="txtimgtitle" style="width:200px;" placeholder="Image Title" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
            <asp:FileUpload ID="fileuploadimages" style="width:200px;" CssClass="form-control" runat="server" />
            </div>
            <div class="col-md-2">
            <asp:Button ID="btnSubmit" runat="server" style="margin-top:4px; margin-left:15px;" CssClass="btn btn-success"  Text="Upload" onclick="btnSubmit_Click" />
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="drpstatus" CssClass="form-control" runat="server" 
                    AutoPostBack="True" onselectedindexchanged="drpstatus_SelectedIndexChanged">
                <asp:ListItem>-Select Status-</asp:ListItem>
                <asp:ListItem>Active</asp:ListItem>
                <asp:ListItem>DeActive</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>

<br /><br />
 
 

 <asp:GridView ID="gvImages" runat="server" AutoGenerateColumns="False" style="width:800px;"

        OnRowDataBound="GridView1_RowDataBound" CellPadding="4"  DataKeyNames="ImageId"

        OnSelectedIndexChanging="GridView1_SelectedIndexChanging" ForeColor="#333333" 
                     AllowPaging="false" onpageindexchanging="gvImages_PageIndexChanging">

        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />   
<Columns>
<asp:BoundField DataField="ImageId" HeaderText="ImageId" />
<asp:BoundField DataField="ImageTitle" HeaderText="Image Name" />
<asp:ImageField HeaderText="Image" DataImageUrlField="ImagePath" ItemStyle-Width="200px" 
ControlStyle-Width="180" ControlStyle-Height = "100" />
  <asp:BoundField DataField="IsActive" HeaderText="Status " />
 <asp:TemplateField HeaderText="Action ">
                    <ItemTemplate>
                        <asp:Button ID="btnChangeStatus" runat="server" OnClick="ChangeStatus" Text="Change Status" />
                    </ItemTemplate>
                </asp:TemplateField>
            
</Columns>
</asp:GridView>
<%--<asp:SqlDataSource ID="sqldataImages" runat="server"  ConnectionString="<%$ConnectionStrings:dbconnection %>"
SelectCommand="select * from Dashboardslider" >
</asp:SqlDataSource>--%>
 

</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="SignatureUpload.aspx.cs" Inherits="SuperAdmin_SignatureUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Digital Signature Upload</h4>
                     </div>
                     <%-- <div class="col-sm-6 text-right">
                        <a href="ViewOrgnization.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Organization</a>
                     </div>--%>
                     
                  </div>
               </div>
            </nav>
             <div class="container mt-5" style="width: 850px;">

             <h2 class="text-center mb-5">
                Signature Upload</h2>
    <br />
    <div class="row">
        <div class="col-md-12">
        <div class="col-md-3">
          <asp:DropDownList ID="ddllabName" DataTextField="sLabName" DataValueField="sLabId" 
                        runat="server" Style="    margin-left: 44px;
    width: 222px;" AutoPostBack="True" 
                        CssClass="form-control" 
                onselectedindexchanged="ddllabName_SelectedIndexChanged"         >
                    </asp:DropDownList>
        </div>
         <div class="col-md-3">
             <asp:TextBox ID="txtimgtitle" style=" margin-left: 67px;  width: 130px;" placeholder="Sign Holder" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
             <div class="col-md-3">
             <asp:TextBox ID="txtdepartment" style="width:200px;" placeholder="Department" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
            <asp:FileUpload ID="fileuploadimages" style="width:200px;" CssClass="form-control" runat="server" />
            </div>
           

            <div class="col-md-4" style="display:none;">
                <asp:DropDownList ID="drpstatus" CssClass="form-control" runat="server" 
                    AutoPostBack="True" onselectedindexchanged="drpstatus_SelectedIndexChanged">
                <asp:ListItem>-Select Status-</asp:ListItem>
                <asp:ListItem>Active</asp:ListItem>
                <asp:ListItem>DeActive</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
          <br /><br />
         <div class="col-md-12">
         <div class="col-md-2" style="float:right;">
            <asp:Button ID="btnSubmit" runat="server" style="margin-top:4px; margin-left:15px;" CssClass="btn btn-success"  Text="Upload" onclick="btnSubmit_Click" />
            </div>
          </div>
    </div>

<br /><br />
 
 

 <asp:GridView ID="gvImages" runat="server" AutoGenerateColumns="False" style="width:800px;"

        CellPadding="4"  DataKeyNames="DSId"

         ForeColor="#333333" 
                     AllowPaging="false" >

        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />   
<Columns>
<asp:BoundField DataField="DSId" HeaderText="Sign ID" />
<asp:BoundField DataField="SignHolder" HeaderText="Sign Holder" />
<asp:BoundField DataField="Department" HeaderText="Department" />
<asp:ImageField HeaderText="Sign Image" DataImageUrlField="SignImage" ItemStyle-Width="200px" 
ControlStyle-Width="180" ControlStyle-Height = "100" />
<%--<asp:BoundField DataField="sLabId" HeaderText="Lab ID" />--%>
  <asp:BoundField DataField="SignStatus" HeaderText="Status " />
 <asp:TemplateField HeaderText="Action ">
                    <ItemTemplate>
                        <asp:Button ID="btnChangeStatus" runat="server" OnClick="ChangeStatus" Text="Change Status" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action ">
                    <ItemTemplate>
                        <asp:Button ID="btndelete" runat="server" OnClick="DeleteRecord" Text="Delete" />
                    </ItemTemplate>
                </asp:TemplateField>
</Columns>
</asp:GridView>

 

</asp:Content>


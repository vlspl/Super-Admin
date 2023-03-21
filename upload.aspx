<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="SuperAdmin_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:FileUpload ID="FileUpload1" runat="server" /><br />
    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    <asp:Image ID="Image1" runat="server" />
</asp:Content>


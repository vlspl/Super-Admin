<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="imageCompress.aspx.cs" Inherits="SuperAdmin_imageCompress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="up1" runat="server">
<ContentTemplate>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 <div align="center">
        <br />
        Name:<asp:TextBox ID="txtname" runat="server"></asp:TextBox>
        <br />
        <br />
      
    UploadImage :<asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
        <br />
<asp:Button ID="btnupload" runat="server" Text="Upload" OnClick="btnupload_Click"  />
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns = "false"
       Font-Names = "Arial" >
    <Columns>
       <asp:BoundField DataField = "id" HeaderText = "ID" Visible="false" />
       <asp:BoundField DataField = "Name" HeaderText = "Image Name" />
       <asp:TemplateField>
           <ItemTemplate>
               <img src="<%#Eval("Content")%>" alt="<%#Eval("Name")%>" width="125px" height="150px"/>
           </ItemTemplate>
       </asp:TemplateField>
    </Columns>
    </asp:GridView>

        
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


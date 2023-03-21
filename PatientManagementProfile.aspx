<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="PatientManagementProfile.aspx.cs" Inherits="PatientManagementProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="header-wrap">
      <asp:Label class="primary-col" ID="Label1" runat="server" Text="Patient ID"></asp:Label>
         <asp:Label ID="AppID" ReadOnly="true" runat="server"></asp:Label>
      
   </div>
<div class="container-fluid">
        <div class="labregister">
            <div class="header-wrap">
                <div class="row">
                    <div class="col-md-6">
                  <div class="form-group">
                    <asp:Label class="primary-col" ID="Label2" runat="server" Text=" Name:"></asp:Label>
        <asp:Label ID="Name"  runat="server"></asp:Label>    
                  </div>
               </div>
               <div class="col-md-6">
                  <div class="form-group">
                      <asp:Label class="primary-col" ID="Label7" runat="server" Text="Contact number:"></asp:Label>
        <asp:Label ID="contactdetails"  runat="server"></asp:Label>  
                  </div>
               </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                  <div class="form-group">
                     <asp:Label class="primary-col" ID="Label3" runat="server" Text="Email id:"></asp:Label>
        <asp:Label ID="Emailid"  runat="server"></asp:Label>  
                  </div>
               </div>
               <div class="col-md-6">
                  <div class="form-group">
                      <asp:Label class="primary-col" ID="Label4" runat="server" Text="Gender:"></asp:Label>
        <asp:Label ID="gender"  runat="server"></asp:Label>
                  </div>
               </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                  <div class="form-group">
                     <asp:Label class="primary-col" ID="Label5" runat="server" Text="Date Of Birth: "></asp:Label>
        <asp:Label ID="dateofbirth" MaxLength="10" runat="server"></asp:Label>
                  </div>
               </div>
               <div class="col-md-6">
                  <div class="form-group">
                     <asp:Label class="primary-col" ID="Label6" runat="server" Text=" Address:"></asp:Label>
        <asp:Label ID="Address" TextMode="MultiLine" placeholder="Address" Rows="5" runat="server"></asp:Label> 
                  </div>
               </div>
                </div>
            </div>
        </div>
    </div>
  
</asp:Content>


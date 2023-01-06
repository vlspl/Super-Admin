<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true" CodeFile="userProfile.aspx.cs" Inherits="userProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div class="row">
        
                
            <div class="col-lg-12"  >
<div class="container rounded bg-white mt-5 mb-5"  >
    <div class="row">
        <div class="col-md-3 border-left border-top border-bottom"">
            <div class="d-flex flex-column align-items-center text-center p-3 py-5"><img class="rounded-circle mt-5" width="150px" src="images/avtr.png"><span class="font-weight-bold"><asp:Label
        ID="lbluserName" runat="server" ></asp:Label></span><span class="text-black-50"><asp:Label
            ID="lblemailId" runat="server" ></asp:Label></span><span> </span></div>
        </div>
        <div class="col-md-5 border-right border-left border-top border-bottom" >
           
            <div class="p-3 py-5">
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <h4 class="text-center">Personal Details</h4>
                </div>
               <hr />
                <div class="row mt-3">
                <div class="col-md-12"><label class="labels">Full Name</label><asp:TextBox ID="txtfullName" runat="server" class="form-control" ></asp:TextBox></div>
                    <div class="col-md-12"><label class="labels">Mobile Number</label><asp:TextBox ID="txtmobileNo" runat="server" class="form-control" ></asp:TextBox></div>
                    <div class="col-md-12"><label class="labels">Email ID</label><asp:TextBox ID="txtemailId" runat="server" class="form-control" ></asp:TextBox></div>
                    <div class="col-md-12"><label class="labels">Address</label><asp:TextBox ID="txtaddress" runat="server" class="form-control" TextMode="MultiLine" style="height:100px; resize:none;"  ></asp:TextBox></div>
                   
                </div>
               
                <div class="mt-5 text-center">
                
                <asp:Button ID="btneditProfile" class="btn btn-primary profile-button" 
                        runat="server" Text="Edit Profile" onclick="btneditProfile_Click" />
                <asp:Button ID="btnsaveProfile" class="btn btn-primary profile-button" 
                        runat="server" Text="Update Profile" onclick="btnsaveProfile_Click" />
               </div>
            </div>
        </div>
       
    </div>
</div>
</div>
</div>
</div>
</asp:Content>


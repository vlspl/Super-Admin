<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="PushNotification.aspx.cs" Inherits="SuperAdmin_PushNotifi" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


  <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>HowzU Notification</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                       
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">
   <div style="clear:both;"><br /></div>
   <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Custom HowzU Notification</h5>
       
      </div>
      <div class="modal-body">
       <asp:TextBox ID="txttitle" CssClass="form-control" placeholder="Title" runat="server"></asp:TextBox>
      <%--  <asp:DropDownList ID="drptemplate" runat="server" CssClass="form-control" 
             >
             <asp:ListItem>-Select-</asp:ListItem>
             <asp:ListItem>Check Your BMI</asp:ListItem>
             <asp:ListItem>Check Your Sugar Level</asp:ListItem>
             <asp:ListItem>Set Your hydration Reminder</asp:ListItem>
             <asp:ListItem>Add Your Family Member</asp:ListItem>
             <asp:ListItem>Book Your Patholohy Appointment</asp:ListItem>
             <asp:ListItem>Check Your Health Record</asp:ListItem>
             <asp:ListItem>Complete Your My Health Diary</asp:ListItem>
             <asp:ListItem>Copmplete Your Profile</asp:ListItem>
             <asp:ListItem>Search Your Neariast Lab For Book appointment</asp:ListItem>
             <asp:ListItem>Take Your Medicine</asp:ListItem>
             <asp:ListItem>Book Home Sample Collection</asp:ListItem>
             <asp:ListItem>Download Your Report</asp:ListItem>
          </asp:DropDownList>--%><br />
           <asp:TextBox ID="txtimgUrl" CssClass="form-control" placeholder="https://www.visionarylifescience.com/Images/temperature.png" runat="server"></asp:TextBox><br />
          <asp:TextBox ID="txtmessage" CssClass="form-control" TextMode="MultiLine" style="height:100px; resize:none;" runat="server"></asp:TextBox>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          <asp:Button ID="btnsendnotification" runat="server" class="btn btn-primary" Text="Send Notification" onclick="btnsendnotification_Click" />
       
      </div>
    </div>
  </div>
</div>
   <div class="col-md-2">
  <label for="drpgender"> Select Gender</label>
   <asp:DropDownList ID="drpgender" runat="server" CssClass="form-control">
   <asp:ListItem>All</asp:ListItem>
   <asp:ListItem>Male</asp:ListItem>
   <asp:ListItem>Female</asp:ListItem>
   </asp:DropDownList>
   </div>
    <div class="col-md-2">
    <label for="drprole"> Select Role</label>
   <asp:DropDownList ID="drprole" runat="server" CssClass="form-control">
   

   </asp:DropDownList>
   </div>
   
    <div class="col-md-2">
     <label for="drprole"> Pin Code</label>
        <asp:TextBox ID="txtpincode" runat="server" CssClass="form-control" placeholder="Enter Pincode"></asp:TextBox>
    </div>
   <div class="col-md-3">
  <span style="margin-top:30px;">
       <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-info" 
           onclick="btnsearch_Click" />
     <button type="button" class="btn btn-primary" style="margin-left:10px;" data-toggle="modal" data-target="#exampleModal">
  Push Notification
</button>
</span>
       </div>
   <div style="clear:both;"><br /></div>
   <span style="font-size:18px;">No of Records : 
                                <asp:Label ID="lblcount" runat="server" Text="Label"></asp:Label></span>
        <asp:GridView ID="griduserActivation" runat="server" DataKeyNames="sAppUserId"  CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText = "User Activation List Not Found"
            onpageindexchanging="griduserActivation_PageIndexChanging" 
            onrowcommand="griduserActivation_RowCommand" >
            <Columns>
                <asp:BoundField DataField="sAppUserId" HeaderText="Sr No" />
                <asp:BoundField DataField="sFullName" HeaderText="Full Name" />
                 <asp:BoundField DataField="sMobile" HeaderText="Mobile" />
                <asp:BoundField DataField="sEmailId" HeaderText="Email Id" />
                <asp:BoundField DataField="sGender" HeaderText="Gender" />
                <asp:BoundField DataField="sAddress" HeaderText="Address" />
                <asp:BoundField DataField="sRole" HeaderText="Role" />
               
             
            </Columns>
        </asp:GridView>
        <asp:HiddenField ID="hdnid" runat="server" />
        <div style="clear:both;"><br /><br /></div>
       
    </div>
   
</asp:Content>


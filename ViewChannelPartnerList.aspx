<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="ViewChannelPartnerList.aspx.cs" Inherits="SuperAdmin_ViewChannelPartnerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
     <script type="text/javascript">
         function deleteitem() {
             if (confirm("Are you sure you want to Deactive...? ")) {
                 return true;
             }
             return false;
         }
    </script>
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Channel Partner Management</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                        <a href="ChannelPartner.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Channel Partner</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">

        <asp:GridView ID="grdviewOrgnization" runat="server" DataKeyNames="Id"  CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="true" AutoGenerateColumns="False" OnPageIndexChanging="grdviewOrgnization_PageIndexChanging"
            OnRowDeleting="grdviewOrgnization_RowDeleting" 
            OnSelectedIndexChanged="grdviewOrgnization_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField HeaderText="Profile Picture">
                    <ItemTemplate>
                        <img src="<%# Eval("ProfilePic")%>" height="40px" width="80px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ChannelPartnerName" HeaderText="Name" />
                <asp:BoundField DataField="ChannelPartnerCode" HeaderText="Channel Partner Code" />
                <asp:BoundField DataField="Address" HeaderText="Address" />
                <asp:BoundField DataField="Mobile" HeaderText="Contact" />
                <asp:BoundField DataField="Email" HeaderText="Email" />

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                     <asp:ImageButton ID="img_user" runat="server" CommandName="Delete" OnClientClick="return deleteitem();" ImageUrl='~/images/Active.png'
                        Width="30px" Height="30px" Visible='<%# Eval("IsActive").ToString() == "True" ? true : false %>' ></asp:ImageButton>
                          <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ImageUrl='~/images/Deactive.jpg'
                        Width="30px" Height="30px" Visible='<%# Eval("IsActive").ToString() == "False" ? true : false %>' ></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="pad" style="color: Red">
        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
</asp:Content>


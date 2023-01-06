<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="OrganizationUser.aspx.cs" Inherits="SuperAdmin_orgUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	 <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
      
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Organization User Creation</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                      
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:20px">
        <div class="row">
            <div class="col-lg-4 col-md-6">
                <div class="form-group">
                    <label>
                        Organization Name <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="drporg" CssClass="form-control" runat="server" 
                        AutoPostBack="True" ontextchanged="drporg_TextChanged">
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>
                        User Name <span style="color: Red">*</span></label>
                    <asp:TextBox ID="txtuserName" CssClass="form-control" placeholder="User Name" runat="server"></asp:TextBox>
                </div>
                 <div class="form-group">
                    <label>
                        Mobile No <span style="color: Red">*</span></label>
                    <asp:TextBox ID="txtmobile" CssClass="form-control" onkeypress="return isNumber(event)" MaxLength="10" placeholder="Mobile No" runat="server"></asp:TextBox>
                </div>
                  <div class="form-group">
                    <label>
                        Email ID <span style="color: Red">*</span></label>
                    <asp:TextBox ID="txtemailId" CssClass="form-control" placeholder="Email ID" runat="server"></asp:TextBox>
                </div>
                 <div class="form-group">
                    <label>
                        Password <span style="color: Red">*</span></label>
                    <asp:TextBox ID="txtpassword" CssClass="form-control" placeholder="Password" TextMode="Password" runat="server"></asp:TextBox>
                </div>
                <hr />
                 <div class="form-group">
                     <asp:Button ID="btnaddRole" runat="server" Text="Submit" OnClientClick="ShowProgress()"
                         CssClass="btn btn-success" style="margin-left:100px;" 
                         onclick="btnaddRole_Click" />
                </div>
            </div>
              <div class="col-lg-8 col-md-6">
                 <div class="form-group">
                     <asp:GridView ID="gvorguser" runat="server" AutoGenerateColumns="False" style="width:90%;" DataKeyNames="ID">

        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />   
<Columns>
<asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="200" />
  <asp:BoundField DataField="EmailId" HeaderText="Email ID "  ItemStyle-Width="200" />
  
 
            
</Columns>
</asp:GridView>
                </div>
                
            </div>
        </div>
        <!-- tab content --><br />
      
    </div>
      <div class="loading" align="center">
    Loading. Please wait.
    <img src="../images/2.gif" />
</div>
<style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #2ac88e;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
</style>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function () {
        ShowProgress();
    });
</script> 
</asp:Content>

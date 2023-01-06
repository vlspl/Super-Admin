<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="LabRegister.aspx.cs" Inherits="LabRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"
        rel="stylesheet" type="text/css" />
  
    <script type="text/javascript">
        $(function () {
            $("#toggle_pwd").click(function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
                $("[id*=txtPassword]").attr("type", type);
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
   
     <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-8" >
                      <h4>  <sapn>Lab ID :  </sapn>
                        <asp:Label ID="LabCode" placeholder="LabID" ReadOnly="true" runat="server"></asp:Label></h4>
                     </div>
                      <div class="col-sm-4 text-right">
                       
                        <a href="ViewLabs.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Labs</a>
                     </div>
                     
                  </div>
               </div>
            </nav>
    <div class="container-fluid">
        <div class="labregister">
            <div class="header-wrap">
            <div class="row">
              <div class="col-md-6">
                      
                            <div class="form-group">
                           <asp:DropDownList ID="ddlLabStatus"  
                                class="form-control select2 select2-hidden-accessible" runat="server" 
                                Style="width: 100%" AutoPostBack="True" 
                                onselectedindexchanged="ddlLabStatus_SelectedIndexChanged">
                           <asp:ListItem>Public</asp:ListItem>
                           <asp:ListItem>Private</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                          </div>
                           <div class="col-md-6">
                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                        
                        <div class="form-group" >
                        <asp:DropDownList ID="ddlName" DataTextField="Name" DataValueField="ID" class="form-control  select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True">
                         </asp:DropDownList>
                        </div>
                        </asp:Panel>
                  </div>
            </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:TextBox ID="LabName" placeholder="Lab Name" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="LabnameReq" ControlToValidate="LabName" ErrorMessage=" *"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:TextBox ID="LabManager" placeholder="Lab Owner" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="labManagerReq" ControlToValidate="LabManager" ErrorMessage=" *"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:TextBox ID="EmailId" placeholder="Email ID" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="EmailIdReq" ControlToValidate="EmailId" ErrorMessage=" *"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="EmailValidation" runat="server" ErrorMessage="Invalid Email ID"
                                ControlToValidate="EmailId" ValidationGroup="labregister" Display="Dynamic" ForeColor="Red"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:TextBox ID="LabContact" placeholder="Contact Number" MaxLength="10" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="LabContactReq" ControlToValidate="LabContact" ErrorMessage=" *"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="Contactnumbervalidation" runat="server" ControlToValidate="LabContact"
                                ErrorMessage="Invalid mobile number" ValidationGroup="labregister" Display="Dynamic"
                                ForeColor="Red" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:TextBox ID="txtUserName" placeholder="Username" runat="server" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="userNameReq" ControlToValidate="txtUserName" ErrorMessage=" *"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
                            </asp:RequiredFieldValidator>
                          
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtPassword" placeholder="Password" TextMode="Password" runat="server"  style=" width: 461px; margin-left: 38px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="passwordReq" ControlToValidate="txtPassword" ErrorMessage=" *"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
                            </asp:RequiredFieldValidator>
                            <div class="input-group-append">  
                              <span id="toggle_pwd" class="btn btn-primary fa fa-fw fa-eye field_icon" style="margin-top: -42px; height: 42px; width:40px;"></span>
                                
                                </div> 
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtConfPassword" placeholder="Confirm Password" TextMode="Password"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ConfpasswordReq" ControlToValidate="txtConfPassword"
                                ErrorMessage=" *" ValidationGroup="labregister" Display="Dynamic" ForeColor="Red"
                                runat="server">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="ComparePassword" runat="server" ControlToValidate="txtConfPassword"
                                ControlToCompare="txtPassword" ErrorMessage="Confirm password must match password"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red"></asp:CompareValidator>
                            
                        </div>
                    </div>
                     <div class="col-md-6">
                       <div class="form-group">
                            <asp:TextBox ID="LabAddress" TextMode="MultiLine" placeholder="Address" Rows="6"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="LabAddressReq" ControlToValidate="LabAddress" ErrorMessage=" *"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
                            </asp:RequiredFieldValidator>
                        </div>
                     </div>
                     </div>
 		 <div class="row">
				 <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:DropDownList ID="DropDownStatus" runat="server">
                                    <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                    <asp:ListItem  Value="Inactive">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="txtlatLong" placeholder="Latitude,Longitude(Ex.18.5533408,73.8009082)" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                     <div class="col-md-6">
                        <div class="text-right">
                            <asp:Button class="lab-btn-primary" ID="RegisterLab" ValidationGroup="labregister" OnClientClick="ShowProgress()"
                                runat="server" Text="Register" OnClick="RegisterLab_Click" />
                        </div>
                    </div>
			</div>
               
             
            </div>
        </div>
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
        border: 5px solid #67CFF5;
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
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

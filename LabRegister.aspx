<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master"
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
    <script language="Javascript" type="text/javascript">

        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-8" >
                      <h4>  <sapn>Lab ID :  </sapn>
                        <asp:Label ID="LabCode" placeholder="LabID" ReadOnly="true" runat="server"></asp:Label></h4>
                     </div>
                      <div class="col-sm-4 text-right">
                       
                        <a href="Dashboard.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Labs</a>
                     </div>
                     
                  </div>
               </div>
            </nav>
    <div class="container-fluid">
        <div class="labregister">
            <div class="header-wrap">
            	<div class="row clearfix">
            <p style="color:red; margin-left:20px;">Note : ( * ) Denotes Mandatory Fields.</p><br />
        </div>
            <div class="row">
            
              <div class="col-md-6">
                      
                            <div class="form-group">
                              <label>Lab Type <span style="color: Red">*</span></label>
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
                          <label>Organization Name <span style="color: Red">*</span></label>
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
                          <label>Lab Name <span style="color: Red">*</span></label>
                            <asp:TextBox ID="LabName" placeholder="Lab Name" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="LabnameReq" ControlToValidate="LabName" ErrorMessage=" *"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                          <label>Owner Name <span style="color: Red">*</span></label>
                            <asp:TextBox ID="LabManager" placeholder="Lab Owner"   runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="labManagerReq" ControlToValidate="LabManager" ErrorMessage=" *"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                          <label>Email ID <span style="color: Red">*</span></label>
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
                          <label>Contact No <span style="color: Red">*</span></label>
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
                          <label>User Name <span style="color: Red">*</span></label>
                            <asp:TextBox ID="txtUserName" placeholder="Username" runat="server" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="userNameReq" ControlToValidate="txtUserName" ErrorMessage=" *"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
                            </asp:RequiredFieldValidator>
                          
                        </div>
                        <div class="form-group">
                          <label>Password <span style="color: Red">*</span></label>
                            <asp:TextBox ID="txtPassword" placeholder="Password" TextMode="Password" runat="server"  style=" width: 461px; margin-left: 38px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="passwordReq" ControlToValidate="txtPassword" ErrorMessage=" *"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
                            </asp:RequiredFieldValidator>
                            <div class="input-group-append">  
                              <span id="toggle_pwd" class="btn btn-primary fa fa-fw fa-eye field_icon" style="margin-top: -42px; height: 42px; width:40px;"></span>
                                
                                </div> 
                        </div>
                        <div class="form-group">
                          <label> Confirm Password <span style="color: Red">*</span></label>
                       
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
                         <label>Address</label>
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
                              <label>Status </label>
                                <asp:DropDownList ID="DropDownStatus" runat="server">
                                    <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                    <asp:ListItem  Value="Inactive">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="txtlatLong" style="margin-top:22px;" placeholder="Latitude,Longitude(Ex.18.5533408,73.8009082)" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                     <div class="col-md-6">
                        <div class="text-right">
                            <asp:Button class="lab-btn-primary" ID="RegisterLab" ValidationGroup="labregister" 
                                runat="server" Text="Register" OnClick="RegisterLab_Click" />
                        </div>
                    </div>
			</div>
               
             
            </div>
        </div>
    </div>
      


</asp:Content>

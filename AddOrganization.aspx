<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="AddOrganization.aspx.cs" Inherits="SuperAdmin_AddOrganization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">  
 <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>  
    <script type="text/javascript">
        $(document).ready(function () {
            $('#show_password').hover(function show() {
                //Change the attribute to text  
                $('#txtPassword').attr('type', 'text');
               
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
            function () {
                //Change the attribute back to password  
                $('#txtPassword').attr('type', 'password');
               
                $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            });
            //CheckBox Show Password  
            $('#ShowPassword').click(function () {
                $('#txtPassword').attr('type', $(this).is(':checked') ? 'text' : 'password');
              
            });
        });  
    </script>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Organization Management</h4>
                     </div>
                      <div class="col-sm-6 text-right">
                        <a href="AddOrgBranch.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Branch</a>
                   
                        <a href="ViewOrgnization.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Organization</a>
                     </div>
                     
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:25px">
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Organization Name<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtOrgName"
                        required placeholder="Enter Organization Name"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>
                        Address<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" Style="resize: None;" TextMode="MultiLine"
                        CssClass="form-control" ID="txtAdress" placeholder="Address"></asp:TextBox>
                </div>
               <div class="form-group">
                    <label>
                        Mobile Number<span style="color: Red">*</span></label>
                    <div class="input-group">
                        <div class="input-group-addon">
                            <i class="fa fa-phone"></i>
                        </div>
                        <asp:TextBox ID="txtmobilenumber"  onkeypress="return isNumber(event)" MaxLength="10" runat="server" ClientIDMode="Static" CssClass="form-control"
                            Placeholder="Enter Mobile Number" ></asp:TextBox>
                    </div>
                    
     
                </div>
                 <div class="form-group">
                    <label>
                        HR Name<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtHRName"
                         placeholder="Enter HR Name"></asp:TextBox>
                      
                </div>
                  <div class="form-group">
                    <label>
                        Type<span style="color: Red">*</span></label>
                      <asp:DropDownList ID="drptype" runat="server" CssClass="form-control">
                      <asp:ListItem>-Select Type Here-</asp:ListItem>
                      <asp:ListItem>Government</asp:ListItem>
                      <asp:ListItem>Other</asp:ListItem>
                      </asp:DropDownList>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label for="Email">
                        Email Id<span style="color: Red">*</span></label>
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                        <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtEmail"
                            required placeholder="Email Id"></asp:TextBox>
                    </div>


                         <asp:RegularExpressionValidator ID="EmailValidation" runat="server" ErrorMessage="Invalid Email ID"
                                    ControlToValidate="txtEmail"  Display="Dynamic" ForeColor="Red"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                </asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label>
                        Organization Details</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" Style="resize: None;" TextMode="MultiLine"
                        CssClass="form-control" ID="txtOrgDetails" placeholder="Address"></asp:TextBox>
                </div>
                <div class="form-group">
                    <div class="form-group has-feedback">
                        <div class="row">
                            <div class="col-lg-4 col-md-1">
                                <label>
                                    Organization Logo</label></div>
                            <div class="col-lg-6 col-md-6">
                                <asp:FileUpload ID="OrgLogo" CssClass=" btn btn-default" runat="server" /></div>
                        </div>
                    </div>
                </div>
                
                 <div class="form-group">
                    <label>
                        Password<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtPassword" TextMode="Password" style=" width: 472px; margin-left: 38px;"
                         placeholder="Enter password"></asp:TextBox>
                       <!--  <asp:RequiredFieldValidator ID="passwordReq" ControlToValidate="txtPassword" ErrorMessage="Please Enter Password"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
                            </asp:RequiredFieldValidator> -->
                             <div class="input-group-append">  
                                    <button id="show_password" class="btn btn-primary" style="margin-top: -42px; height: 42px;" type="button">  
                                        <span class="fa fa-eye-slash icon"></span>  
                                    </button>  
                                </div> 
                </div>
                 <div class="form-group">
                    <label>
                        Confirm Password<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtConfirmPassword" 
                    TextMode="Password"    placeholder="Enter password"></asp:TextBox>
                     <!--    <asp:RequiredFieldValidator ID="ConfpasswordReq" ControlToValidate="txtConfirmPassword"
                                ErrorMessage=" *" ValidationGroup="labregister" Display="Dynamic" ForeColor="Red"
                                runat="server">
                            </asp:RequiredFieldValidator> -->
                            <asp:CompareValidator ID="ComparePassword" runat="server" ControlToValidate="txtConfirmPassword"
                                ControlToCompare="txtPassword" ErrorMessage="Confirm password must match password"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red"></asp:CompareValidator>
                               
                </div>
                <div class="box-footer" align="right">
                    <asp:Button ID="BtnSave" runat="server" type="submit" class="fa fa-save btn btn-lg btn-primary"
                        Text="Save" onclick="BtnSave_Click" />
                      <asp:Button ID="btnUpdate" runat="server"  class="fa fa-save btn btn-lg btn-primary"
                        Text="Update" visible="false" onclick="btnUpdate_Click" />

                          <asp:Button ID="btnDelete" runat="server" class="fa fa-save btn btn-lg btn-primary"
                        Text="Delete" visible="false" onclick="btnDelete_Click"/>

                    <div class="pad">
                        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="Copy of AddOrgBranch.aspx.cs" Inherits="SuperAdmin_AddOrgBranch" %>

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
                        <h4>Organization Branch Management</h4>
                     </div>
                      <div class="col-sm-6 text-right">
                        <a href="ViewOrgBranch.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Branch</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:25px" >
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Organization Name <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="ddlName" DataTextField="Name" DataValueField="ID" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Branch Name<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtBranchName"
                        required placeholder="Enter Branch Name"></asp:TextBox>
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
                        <asp:TextBox ID="txtmobilenumber" onkeypress="return isNumber(event)" MaxLength="10"  runat="server" ClientIDMode="Static" CssClass="form-control"
                            Placeholder="Enter Mobile Number"></asp:TextBox>
                    </div>
                    <%-- <asp:RegularExpressionValidator ID="Contactnumbervalidation" runat="server" ControlToValidate="txtmobilenumber"
                                ErrorMessage="Invalid mobile number" onkeypress="return isNumber(event)" MaxLength="10"  Display="Dynamic"
                                ForeColor="Red" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>--%>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        HR Name<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtHRName"
                         placeholder="Enter HR Name"></asp:TextBox>
                          <asp:RegularExpressionValidator ID="regName" runat="server" 
       ControlToValidate="txtHRName" ForeColor="Red"
       ValidationExpression="^[a-zA-Z'.\s]{1,50}"
       Text="Enter a valid Name" /> 
                </div>
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
                        Password<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtPassword" TextMode="Password" style=" width: 472px; margin-left: 38px;"
                        required placeholder="Enter password"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="passwordReq" ControlToValidate="txtPassword" ErrorMessage="Please Enter Password"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
                            </asp:RequiredFieldValidator>
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
                    TextMode="Password"   required placeholder="Enter password"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="ConfpasswordReq" ControlToValidate="txtConfirmPassword"
                                ErrorMessage=" *" ValidationGroup="labregister" Display="Dynamic" ForeColor="Red"
                                runat="server">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="ComparePassword" runat="server" ControlToValidate="txtConfirmPassword"
                                ControlToCompare="txtPassword" ErrorMessage="Confirm password must match password"
                                ValidationGroup="labregister" Display="Dynamic" ForeColor="Red"></asp:CompareValidator>
                </div>
                <div class="box-footer" align="right">
                    <asp:Button ID="BtnSave" runat="server" type="submit" class="fa fa-save btn btn-lg btn-primary"
                        Text="Save" OnClick="BtnSave_Click" />
                    <div class="pad">
                        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

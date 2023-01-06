<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />  
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>  
    <script type="text/javascript">
        $(document).ready(function () {
            $('#show_password').hover(function show() {
                //Change the attribute to text  
                $('#txtOldPassword').attr('type', 'text');
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
            function () {
                //Change the attribute back to password  
                $('#txtOldPassword').attr('type', 'password');
                $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            });
            //CheckBox Show Password  
            $('#Show_Password').click(function () {
                $('#txtOldPassword').attr('type', $(this).is(':checked') ? 'text' : 'password');
              
            });
        });
        $(document).ready(function () {
            $('#show_password1').hover(function show() {
                //Change the attribute to text  
                $('#txtNewPassword').attr('type', 'text');
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
            function () {
                //Change the attribute back to password  
                $('#txtNewPassword').attr('type', 'password');
                $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            });
            //CheckBox Show Password  
            $('#show_password1').click(function () {
                $('#txtNewPassword').attr('type', $(this).is(':checked') ? 'text' : 'password');

            });
        });
        $(document).ready(function () {
            $('#show_password2').hover(function show() {
                //Change the attribute to text  
                $('#txtConfirmPassword').attr('type', 'text');
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
            function () {
                //Change the attribute back to password  
                $('#txtConfirmPassword').attr('type', 'password');
                $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            });
            //CheckBox Show Password  
            $('#show_password2').click(function () {
                $('#txtConfirmPassword').attr('type', $(this).is(':checked') ? 'text' : 'password');

            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5 ">
        <a href="#" class="navbar-brand"></a>
      </div>
    </div>
  </nav>
    <div class="table_div">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-6">
                    <div class="card ">
                        <h5 class="card-header fa-color">
                            Change Password<span></span></h5>
                        <div class="card-body">
                            <div class="panel-body">
                                <div class="form-group">
                                    <label>
                                        Old Password</label>
                                    <input type="password" class="form-control cus-care" placeholder="Old Password" id="txtOldPassword"  style="margin-left: 40px; width: 422px;"
                                        runat="server" clientidmode="Static" />
                                         <div class="input-group-append">  
                                        <button id="show_password" class="btn btn-primary" style="margin-top:-39px;" type="button">  
                                            <span class="fa fa-eye-slash icon"></span>  
                                        </button>  
                                        </div> 
                                   
                                </div>
                                <div class="form-group ">
                                    <label>
                                        New Password</label>
                                    <input type="password" class="form-control cus-care" placeholder="New Password" id="txtNewPassword" style="margin-left: 40px; width: 422px;"
                                        runat="server" clientidmode="Static" />
                                    <div class="input-group-append">  
                                        <button id="show_password1" class="btn btn-primary" style="margin-top:-39px;" type="button">  
                                            <span class="fa fa-eye-slash icon"></span>  
                                        </button>  
                                        </div> 
                                </div>
                                <div class="form-group ">
                                    <label>
                                        Confirm Password</label>
                                    <input type="password" class="form-control cus-care" placeholder="Confirm Password" style="margin-left: 40px; width: 422px;"
                                        id="txtConfirmPassword" runat="server" clientidmode="Static" />
                                     <div class="input-group-append">  
                                        <button id="show_password2" class="btn btn-primary" style="margin-top:-39px;" type="button">  
                                            <span class="fa fa-eye-slash icon"></span>  
                                        </button>  
                                        </div> 
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:LinkButton ID="btnChangePassword" class="btn btn-color" runat="server" OnClientClick="javascript:return changePasswordValidate()" style="float:right;"
                                            OnClick="btnChangePassword_Click" ClientIDMode="Static">Update Password</asp:LinkButton>
                                  </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <span id="spanLogo" runat="server" clientidmode="Static"></span><span id="spanLabName"
                            style="font-size: larger" runat="server" clientidmode="Static"></span>
                    </h3>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/ChangePasswordValidation.js"></script>
  <%--  <script type="text/javascript">
        $('#txtNewPassword').on('blur', function () {
            if (this.value.length < 6) { // checks the password value length
                alert('● Please enter Minimum 6 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character');
                $(this).focus(); // focuses the current field.
                return false; // stops the execution.
            }
        });
    </script>--%>
</asp:Content>

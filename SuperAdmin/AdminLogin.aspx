<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Super Admin Login</title>
    <meta name="description" content="" />
    <meta name="keyword" content="" />
   
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/style.css" />
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <%-- <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script> --%>
       <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">  
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>  
    <script type="text/javascript">
        $(document).ready(function () {
            $('#show_password').hover(function show() {
                //Change the attribute to text  
                $('#txtpassword').attr('type', 'text');
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
            function () {
                //Change the attribute back to password  
                $('#txtpassword').attr('type', 'password');
                $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            });
            //CheckBox Show Password  
            $('#ShowPassword').click(function () {
                $('#txtpassword').attr('type', $(this).is(':checked') ? 'text' : 'password');
            });
        });  
    </script>
</head>
<body>
    <div class="loginheader">
        <div class="container">
            <div class="row">
                <div class="">
                    <div class="header-left">
                        <img src="../images/labcare-logo.png" alt="adani-logo">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div>
        <div class="container">
            <div id="" class="col-sm-6">
                <img src="images/admin-screen.jpg">
            </div>
            <div class="col-sm-6">
                <div class="panel-body" style="">
                    <div class="login-form">
                        <ol class="breadcrumb">
                            <li>
                                <h4>
                                    <i class="fa fa-sign-in" aria-hidden="true"></i> Welcome to Super Admin
                                </h4>
                            </li>
                        </ol>
                        <form id="form1" runat="server">
                        <div class="form-group">
                            <label for="">
                                User Name</label>
                            <input id="txtuser" runat="server" type="text" class="form-control" name="user" value="" style="margin-left: 35px;  width: 445px;"
                                placeholder="User" required>
                                 <div class="input-group">  
                                    <button  class="btn btn-primary" style="margin-top: -42px; height: 42px;" >  
                                        <span class="fa fa-user"></span>  
                                    </button>  
                                  </div>
        </div>
          <div class="form-group">
                            <label for="">
                                Password</label>
                            <input id="txtpassword" runat="server" type="password" textmode="Password" class="form-control" style="margin-left: 35px;  width: 445px;"
                                name="password" placeholder="Password" required>
                                 <div class="input-group-append">  
                                    <button id="show_password" class="btn btn-primary" style="margin-top: -42px; height: 42px;" type="button">  
                                        <span class="fa fa-eye-slash icon"></span>  
                                    </button>  
                                </div> 
                        </div>
                        <div class="login-btn">
                            <asp:Button ID="Button1" type="submit" href="#" runat="server" OnClick="btn_insert" style="float:right;"
                                class="lab-btn-secondary" Text="Log In" />
                            <div>
                                <label id="lblMessage" runat="server">
                                </label>
                            </div>
                            <h4 class="primary-col">
                                <a href="" data-toggle="modal" data-target="#forgotPassword">Forgot passord ?</a></h4>
                        </div>
                        <!-- MODAL START -->
                        <div id="forgotPassword" class="modal fade" role="dialog">
                            <div class="modal-dialog">
                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">
                                            &times;</button>
                                        <h4 class="modal-title">
                                            Forgot password ?</h4>
                                    </div>
                                    <div>
                                        <h5>
                                            Please enter your username below and your password shall be mailed to your registered
                                            email id.</h5>
                                    </div>
                                    <div class="modal-body">
                                        <div class="cus-form">
                                            <div class="">
                                                <div class="form-group">
                                                    <asp:TextBox class="form-control" placeholder="User Name *" ID="txtForgotPasswordUserName"
                                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup='valGroupForgotPassword'
                                                        runat="server" ErrorMessage="required *" ControlToValidate="txtForgotPasswordUserName"></asp:RequiredFieldValidator>
                                                </div>
                                                <%--<div class="form-group">
                        <asp:TextBox class="form-control" placeholder="Lab Name *" id="txtForgotPasswordLabName" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <label id="lblForgotPasswordLabName" class="form-error"></label>
                    </div>  --%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnSubmit" ValidationGroup='valGroupForgotPassword' class="lab-btn-default"
                                            runat="server" Text="Submit" OnClick="btnSubmit_Click" ClientIDMode="Static"  />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- MODAL END -->
                        </form>
                     
                     
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="particles">
    </div>
   <%-- </div>
    <script src="js1/jquery.js"></script>
    <script src="js1/jquery.easing.1.3.js"></script>
    <script src="js1/bootstrap.min.js"></script>
    <script src="js1/jquery.fancybox.pack.js"></script>
    <script src="js1/jquery.fancybox-media.js"></script>
    <script src="js1/portfolio/jquery.quicksand.js"></script>
    <script src="js1/portfolio/setting.js"></script>
    <script src="js1/jquery.flexslider.js"></script>
    <script src="js1/animate.js"></script>
    <script src="js1/custom.js"></script>--%>
    <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
   <%-- <script src="js1/menuscript.js"></script>--%>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabLogin.aspx.cs" Inherits="LabLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.0.13/css/all.css"
        integrity="sha384-DNOHZ68U8hZfKXOrtjWvjxusGo9WQnrNx2sqG0tfsghAvtVlRW3tvkXWZh58N9jp"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css"
        integrity="sha384-WskhaSGFgHYWDcbwN70/dfYBj47jz9qbsMId/iRN3ewGhXQFZCSftd1LZCfmhktB"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="css/Newstyle.css" />
    <title>Admin Login</title>
     <!--Captcha -->
    <script src="https://www.google.com/recaptcha/api.js"></script>
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
<body style="    overflow-y: hidden;">
    <form id="form1" runat="server" Method="POST" name="csrf">
    <input type="hidden" id="LoginNodule" value="LAB" runat="server" />
    <div class="contaier">
        <nav class="navbar navbar-expand-sm navbar-dark bg-dark fixed-top" id="main-nav">
            <div class="container">
              <a href="" class="navbar-brand"><img src="images/Final logo registered.png" alt="Logo" style="width:150px;"/></a>
              <div class="navabar-nav">
                <h4 class="ml-auto text-white">Welcome to Admin Panel !</h4>
              </div>
            </div>
        </nav>
        <!--Home-->
        <div id="home-section">
            <div class="dark-overlay">
                <div class="home-inner container">
                    <div class="row">
                        <div class="col-lg-7 d-none d-lg-block">
                        </div>
                        <!--Login Card -->
                        <div class="col-lg-4">
                            <div class="card bg-white text-center card-form">
                                <div class="card-body">
                                    <ul class="nav justify-content-center ">
                                        <li class="nav-item active lab"><a class="nav-link A1" id="LAB" href="#">LAB</a>
                                            <div class="msg active" id="labL">
                                                <p style="font-size: 15px; color: gray; position: absolute; top: 70px; left: 25px;">
                                                    Login as Lab Admin
                                                </p>
                                            </div>
                                        </li>
                                        <li class="nav-item lab"><a class="nav-link A1" id="ENTERPRISE" href="#">ENTERPRISE</a>
                                            <div class="msg">
                                                <p style="font-size: 15px; color: gray; position: absolute; top: 70px; left: 60px;">
                                                    Login as Enterprise Admin
                                                </p>
                                            </div>
                                        </li>
                                        <li class="nav-item lab"><a class="nav-link A1" id="GOVERNMENT" href="#">GOVERNMENT</a>
                                            <div class="msg">
                                                <p style="font-size: 15px; color: gray; position: absolute; top: 70px; left: 145px;">
                                                    Login as Government Admin
                                                </p>
                                            </div>
                                        </li>
                                    </ul>
                                   <form class="pt-2" id="form-section" method="post" action="">
                                    <div class="form-group mt-5 inputwidthIcon">
                                        <input type="text" class="form-control form-control-md inputfield" placeholder="User Name"
                                            id="txtUserName" runat="server" />
                                       
 						<div class="input-group">  
                                    <button  class="btn btn-primary" style="margin-top: -38px; height: 37px;" >  
                                        <span class="fa fa-user"></span>  
                                    </button>  
                                  </div>
                                    </div>
                                    <div class="form-group mt-4 inputwidthIcon">
                                        <input type="password" class="form-control form-control-md inputfield" placeholder="Password"
                                            id="txtpassword" runat="server" />
                                       
  						 <div class="input-group-append">  
                                    <button id="show_password" class="btn btn-primary" style="margin-top: -38px; height: 37px;" type="button">  
                                        <span class="fa fa-eye-slash icon"></span>  
                                    </button>  
                                </div> 
                                    </div>
                                     	
                                    <asp:Button ID="btnLogin" class="btn mt-4 btn-block" type="submit" runat="server"
                                        Text="LOGIN" OnClick="btnLogin_Click" />
                                    <div>
                                        <label id="lblMessage" class="form-error" runat="server">
                                        </label>
                                    </div>
                                  <%--  <div class="mt-3 mb-5">
                                        <a href="" data-toggle="modal" data-target="#forgotPassword">Forgot Your Password?</a></div>--%>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="forgotPassword">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <%--  <button type="button" class="close" data-dismiss="modal" ari-label="Close">
                        <span aria-hidden="true">&times;</span></button>--%>
                    <h4 class="modal-title">
                        Forgot Password?</h4>
                </div>
                <%-- <div class=" box box-primary">
                    <div class="box-header with-border">--%>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Enter Your User Name " ID="txtForgotPasswordUserName"
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup='valGroupForgotPassword'
                                    runat="server" Style="color: Red" ErrorMessage=" This field is required" ControlToValidate="txtForgotPasswordUserName"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <%--
                    </div>
                </div>--%>
                <div class="modal-footer">
                    <input id="btnSaveChanges" type="button" class="btn btn-primary" value="Submit" />
                    <button type="button" id="btnclose" class="btn btn-default pull-left" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    </form>
    <script src="js/Jquery-3.1.1.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"
        integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49"
        crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"
        integrity="sha384-smHYKdLADwkXOn1EmN1qk/HfnUcbVRZyYmZ4qpPea6sjB/pTJ0euyQp0Mk8ck+5T"
        crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(".nav li").on("click", function () {
            $(".nav li, .nav li a,.nav li div ").removeClass("active");
            $(this).addClass("active");
        })
        $(".lab .msg ").hide();
        $(".lab #labL ").show();
        $(".lab a").click(function () {
            $(".lab .msg, .lab #labL").hide('fast');
            $(this).parent().find("div").toggle("fast");
        })   
    </script>
    <script type="text/javascript">
        $(document).ready(function () 
        {
            $(".A1").click(function ()
             {      
                var Panal = $(this).text();
                $("#LoginNodule").val(Panal);
            });
            $(function () {
                $('#btnSaveChanges').click(function () {
                    var admin = $("#LoginNodule").val();

                    $.ajax({
                        type: "POST",
                        url: "LabLogin.aspx/ForgotPassword",

                        data: "{'data':'" + admin + "!~^!" + $('#<%=txtForgotPasswordUserName.ClientID %> ').val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",

                        success: function (msg) {
                            if (msg.d == 1) {
                                alert('Password has been send to your registered email id')
                                location.reload();
                            }
                            else if (msg.d == 0) {
                                alert('Username did not match')
                            }
                            else {
                                alert('Something Went Wrong')
                            };
                        },
                        error: function (data) {
                            alert('Something Went Wrong')
                        }
                    });
                });
            });
        });
    </script>
    <!-- for Capta -->
    <script type="text/javascript">
        var onloadCallback = function () {
            grecaptcha.render('dvCaptcha', {
                'sitekey': '<%=ReCaptcha_Key %>',
                'callback': function (response) {
                    $.ajax({
                        type: "POST",
                        url: "CS.aspx/VerifyCaptcha",
                        data: "{response: '" + response + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            var captchaResponse = jQuery.parseJSON(r.d);
                            if (captchaResponse.success) {
                                $("[id*=txtCaptcha]").val(captchaResponse.success);
                                $("[id*=rfvCaptcha]").hide();
                            } else {
                                $("[id*=txtCaptcha]").val("");
                                $("[id*=rfvCaptcha]").show();
                                var error = captchaResponse["error-codes"][0];
                                $("[id*=rfvCaptcha]").html("RECaptcha error. " + error);
                            }
                        }
                    });
                }
            });
        };
</script>
</body>
</html>

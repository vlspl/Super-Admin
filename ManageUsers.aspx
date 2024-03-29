﻿<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="SuperAdmin_ManageUsers" %>

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
    <div class="wrapper">
        <div id="content" class="">
            <div id="testlist" class="">
                <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>User Management</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                        <a href="" data-toggle="modal" data-target="#modalAddLabUser" class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add User</a>
                     </div>
                  </div>
               </div>
            </nav>
                <!-- tab content -->
                <div class="tab-content">
                    <div class="box-body" style="padding: 20px">
                        <!-- Lab User List Start -->
                        <div id="divLabUserList" class="tab-pane fade in active">
                            <asp:HiddenField ID="hiddenDeleteUser" runat="server" ClientIDMode="Static" />
                            <table class="table booking">
                                <thead>
                                    <tr>
                                        <th>
                                            Name
                                        </th>
                                        <th>
                                            Email ID
                                        </th>
                                        <th>
                                            Contact
                                        </th>
                                      
                                        <th>
                                            Role
                                        </th>
                                        
                                        <th>
                                            Delete
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tbodyLabUsersList" runat="server" clientidmode="Static">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal Add User Start-->
        <div id="modalAddLabUser" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">
                            Add Lab User</h4>
                    </div>
                    <div class="modal-body">
                        <div class="cus-form">
                        <div class="row clearfix">
            <p style="color:red; margin-left:20px;">Note : ( * ) Denotes Mandatory Fields.</p><br />
        </div>
                            <div class="">
                                <div class="form-group">
                                    <label>Full Name <span style="color: Red">*</span></label>
                                    <asp:TextBox class="form-control" placeholder="Enter Full Name" ID="txtFullName"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <label id="lblFullName" class="form-error">
                                    </label>
                                </div>
                                <div class="form-group">
                                     <label>Email ID <span style="color: Red">*</span></label>
                                    <asp:TextBox class="form-control" placeholder="Enter Email Id" ID="txtEmailId"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <label id="lblEmailId" class="form-error">
                                    </label>
                                </div>
                                <div class="form-group">
                                    <label>Contact Number</label>
                                    <asp:TextBox class="form-control" placeholder="Enter Contact Number" ID="txtContact" onkeypress="return isNumber(event)" 
                                        MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <label id="lblContact" class="form-error">
                                    </label>
                                </div>
                                  <div class="form-group">
                                       <label>Select Role</label>
                                    <asp:DropDownList ID="drproleMaster"  class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%"  >
                       
                    </asp:DropDownList>
                                   
                                </div>
                                <div class="form-group">
                                      <label>Description</label>
                                    <asp:TextBox class="form-control" placeholder="Description" ID="txtDescription" runat="server" TextMode="MultiLine" Style="resize:none; height:100px;"
                                        ClientIDMode="Static"></asp:TextBox>
                                    <label id="lblDescription" class="form-error">
                                    </label>
                                </div>
                                <div class="form-group">
                                     <label>User Name</label>
                                    <asp:TextBox class="form-control" placeholder="Enter User Name" ID="txtUserName"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <label id="lblUserName" class="form-error">
                                    </label>
                                </div>
                                
                                <div class="form-group">
                                     <label>Password</label>
                                    <asp:TextBox class="form-control" placeholder="Password" ID="txtPassword" TextMode="Password" style="margin-left:39px; width:530px;"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                   <div class="input-group-append">  
                                    <button id="show_password" class="btn btn-primary" style="margin-top: -42px; height: 42px;" type="button">  
                                        <span class="fa fa-eye-slash icon"></span>  
                                    </button>  
                                </div> 
                                    <label id="lblPassword" class="form-error">
                                    </label>
                                </div>
                                <asp:HiddenField ID="hiddenRoles" runat="server" ClientIDMode="Static" />
                                
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnAdd" class="lab-btn-default" runat="server" Text="Submit" OnClientClick="javascript:return validateUserAdd()"
                            OnClick="btnAdd_Click" ClientIDMode="Static" />
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal Add User End-->
        <!-- Modal Edit User Start-->
        <asp:HiddenField ID="hiddenEditUser" runat="server" ClientIDMode="Static" />
        <div id="modalEditUser" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">
                            Edit Lab User</h4>
                    </div>
                    <div class="modal-body">
                        <div class="cus-form">
                            <div class="">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Enter Full Name *" ID="txtFullNameEdit"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <label id="lblFullNameEdit" class="form-error">
                                    </label>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Enter Email Id *" ID="txtEmailIdEdit"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <label id="lblEmailIdEdit" class="form-error">
                                    </label>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Enter Contact Number" ID="txtContactEdit"
                                        MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <label id="lblContactEdit" class="form-error">
                                    </label>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Description" ID="txtDescriptionEdit" ReadOnly="true"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <label id="lblDescriptionEdit" class="form-error">
                                    </label>
                                </div>
                                <asp:HiddenField ID="hiddenRolesEdit" runat="server" ClientIDMode="Static" />
                                <%--        <div class="form-group" id="divRolesEdit"  runat="server" clientidmode="Static">
                        
                </div>--%>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnUpdate" class="lab-btn-default" runat="server" Text="Submit" OnClientClick="javascript:return validateUserEdit()"
                            OnClick="btnUpdate_Click" ClientIDMode="Static" />
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal Edit User End-->
        <!-- modal Delete User Confirm Start-->
        <div class="modal fade" id="modalDeleteUserConfirm" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-body text-center">
                        <h1 class="ad-primary-color" style="margin-top: 10px;">
                            <i class="fa fa-file-text" aria-hidden="true"></i>
                        </h1>
                        <h4 class="ad-primary-color">
                            Are you sure want to delete this user</h4>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnDeleteUserYes" class="btn btn-default ad-btn-secondary" runat="server"
                            Text="Yes" OnClick="btnDeleteUserYes_Click" ClientIDMode="Static" />
                        <asp:Button ID="btnDeleteUserNo" class="btn btn-default ad-btn-secondary" runat="server"
                            data-dismiss="modal" Text="No" ClientIDMode="Static" />
                    </div>
                </div>
            </div>
        </div>
        <!-- modal Delete User Confirm End-->
        <script type="text/javascript" src="../js/jquery.js"></script>
        <script type="text/javascript" src="../js/ManageUsersValidation.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#divRoles :checkbox').change(function () {

                    if (this.checked) {
                        $("#hiddenRoles").val($("#hiddenRoles").val() + "," + $(this).val());
                        $("#hiddenRoles").val($("#hiddenRoles").val().replace(",,", ","));
                        //alert($("#hiddenRoles").val());
                    } else {
                        $("#hiddenRoles").val($("#hiddenRoles").val().replace($(this).val(), ""));
                        $("#hiddenRoles").val($("#hiddenRoles").val().replace(",,", ","));
                        //alert($("#hiddenRoles").val());
                    }
                });

                $("#tbodyLabUsersList a").click(function () {

                    $("#lblFullNameEdit").hide();
                    $("#lblEmailIdEdit").hide();
                    $("#lblContactEdit").hide();

                    if ($(this).attr("id")) {
                        var id = $(this).attr("id");

                        $("#hiddenDeleteUser").val($(this).attr("id"));
                        $("#hiddenEditUser").val($(this).attr("id"));

                        $("#txtFullNameEdit").val($("#name" + id).html());
                        $("#txtEmailIdEdit").val($("#emailId" + id).html());
                        $("#txtContactEdit").val($("#contact" + id).html());
                        $("#txtDescriptionEdit").val($("#description" + id).html());
                        $("#hiddenRolesEdit").val($("#role" + id).html());

                        $("#divRolesEdit input[type=checkbox]").each(function () {
                            if ($("#hiddenRolesEdit").val().indexOf($(this).val()) >= 0) {
                                $(this).prop('checked', true);
                            }
                            else {
                                $(this).prop('checked', false);
                            }
                        });
                        //alert($("#hiddenDeleteUser").val());
                    }
                });

                $('#divRolesEdit :checkbox').change(function () {

                    if (this.checked) {
                        $("#hiddenRolesEdit").val($("#hiddenRolesEdit").val() + "," + $(this).val());
                        $("#hiddenRolesEdit").val($("#hiddenRolesEdit").val().replace(",,", ","));
                        //alert($("#hiddenRolesEdit").val());
                    } else {
                        $("#hiddenRolesEdit").val($("#hiddenRolesEdit").val().replace($(this).val(), ""));
                        $("#hiddenRolesEdit").val($("#hiddenRolesEdit").val().replace(",,", ","));
                        //alert($("#hiddenRolesEdit").val());
                    }
                });
            });

        </script>
          <script type="text/javascript">
              function Yopopupalert() {
                  $('#YoCommonPopup').addClass('in');
                  $('#YoCommonPopup').show();
              };


          </script>
</asp:Content>

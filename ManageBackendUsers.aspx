<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="ManageBackendUsers.aspx.cs" Inherits="SuperAdmin_ManageBackendUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                            <!-- filter -->
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
                                        <%-- <th>Role</th>--%>
                                        <th>
                                            Description
                                        </th>
                                        <th>
                                            Edit
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
                            <div class="">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Enter Full Name *" ID="txtFullName"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="nameReq" ControlToValidate="txtFullName" ErrorMessage=" Please Enter Name"
                                        Display="Dynamic" ForeColor="Red" runat="server">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Enter Email Id *" ID="txtEmailId"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmailId"
                                        ErrorMessage=" Please Enter Email" Display="Dynamic" ForeColor="Red" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="EmailValidation" runat="server" ErrorMessage="Invalid Email ID"
                                        ControlToValidate="txtEmailId" Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Enter Mobile Number" ID="txtContact"
                                        MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtContact"
                                        ErrorMessage=" Please Enter Mobile" Display="Dynamic" ForeColor="Red" runat="server">    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="Contactnumbervalidation" runat="server" ControlToValidate="txtContact"
                                        ErrorMessage="Invalid mobile number" Display="Dynamic" ForeColor="Red" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Description" ID="txtDescription" runat="server"
                                        ClientIDMode="Static"></asp:TextBox>
                                    <label id="lblDescription" class="form-error">
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
                    <div class="pad">
                        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
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
                            Edit backend User</h4>
                    </div>
                    <div class="modal-body">
                        <div class="cus-form">
                            <div class="">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Enter Full Name *" ID="txtFullNameEdit"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtFullNameEdit"
                                        ErrorMessage=" Please Enter Name" Display="Dynamic" ForeColor="Red" runat="server">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Enter Email Id *" ID="txtEmailIdEdit"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEmailIdEdit"
                                        ErrorMessage=" Please Enter Email" Display="Dynamic" ForeColor="Red" runat="server">    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email ID"
                                        ControlToValidate="txtEmailIdEdit" Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Enter Contact Number" ID="txtContactEdit"
                                        MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtContactEdit"
                                        ErrorMessage=" Please Enter Mobile" Display="Dynamic" ForeColor="Red" runat="server">    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtContactEdit"
                                        ErrorMessage="Invalid mobile number" Display="Dynamic" ForeColor="Red" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Description" ID="txtDescriptionEdit"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <asp:HiddenField ID="hiddenRolesEdit" runat="server" ClientIDMode="Static" />
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
                            Are you sure you want to delete this user</h4>
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
    </div>
    <script type="text/javascript" src="js/jquery.js"></script>
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
</asp:Content>

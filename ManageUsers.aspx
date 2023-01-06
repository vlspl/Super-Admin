<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript">
     function showModal() {
         $("#myModal").modal('show');
     }

    </script>
  <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
 <script type="text/javascript">
     function goBack() {
         var url = 'ManageUsers.aspx';
         window.location.href = url;
     }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Howzu Says</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <asp:Label ID="lblMessage" runat="server"></asp:Label>
      </div>
      <div class="modal-footer">
        <asp:Button ID="btnredirect" class="btn btn-secondary" OnClientClick="goBack()"  runat="server" Text="Close"></asp:Button>
      </div>
    </div>
  </div>  
    </div>
 <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand">User Master </a>
      </div>

      <div class="mr-5">
        <ul class="navbar-nav ml-auto">
          <li class="nav-item pt-1 mr-2">
             <a href="#" data-toggle="modal" id='HideAddbtn' runat="server" data-target="#modalAddLabUser" class="btn btn-color">
             <span class="fa fa-plus mr-2" aria-hidden="true"></span> Add User</a>
           
          </li>
          <%-- <li class="nav-item pt-1">
            <button class="btn btn-color"><span><i class="fa fa-arrow-left mr-2"
                  area-hidden="true"></i></span>Back</button>
          </li>--%>
        </ul>
      </div>
    </div>
  </nav>
    <div class="table_div">
    <div class="container-fluid">
          <div id="userManagement">
        <ul class="responsive-table">
          <li class="table-header">
            <div class="col col-1 text-center">Name </div>
            <div class="col col-2 text-center">Email ID </div>
            <div class="col col-3 text-center">Contact</div>
            <div class="col col-4 text-center">Role</div>
            <div class="col col-5 text-center">Description </div>
            <div class="col col-6 text-center">Edit </div>
            <div class="col col-7 text-center">Edit Role</div>
            <div class="col col-8 text-center">Delete</div>
          </li>
          <div id="page">
            <asp:Literal ID="tbodyLabUsersList" runat="server"></asp:Literal>
          </div>
        </ul>
      </div>
    </div>
  </div>
 
  <div class="container">
    <div class="row">
      <div class="col-md-2"></div>
      <div class="col-md-8">
        <nav class="pagination-container">

          <ul class="pagination justify-content-center">
            <li id="previous-page" class="px-2"><a href="javascript:void(0)" aria-label=Previous><span
                  aria-hidden=true>&laquo;</span></a></li>
          </ul>

        </nav>
      </div>
      <div class="col-md-2"></div>
    </div>

  </div>
   
    <asp:HiddenField ID="hiddenDeleteUser"  runat="server" ClientIDMode="Static"/>
<!-- Modal Add User Start-->
<div id="modalAddLabUser" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header"> <h4 class="modal-title">Add Lab User</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
       
      </div>
      <div class="modal-body">
        <div class="cus-form">
              <div class="">                   
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Enter Full Name *" id="txtFullName" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblFullName" class="form-error"></label>
                  </div>

                   <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Enter Email Id *" id="txtEmailId" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblEmailId" class="form-error"></label>
                  </div>
             
           
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Enter Contact Number" id="txtContact"   onkeypress="return isNumber(event)"  MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblContact" class="form-error"></label>
                  </div>
               
                
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Role Description *" id="txtDescription" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblDescription" class="form-error"></label>
                  </div>

                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Enter User Name" id="txtUserName"  runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblUserName" class="form-error"></label>
                  </div>
               
                
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Password" id="txtPassword" TextMode="Password" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblPassword" class="form-error"></label>
                  </div>
                <asp:HiddenField ID="hiddenRoles"  runat="server" ClientIDMode="Static"/>
      <%--          <div class="form-group" id="divRoles"  runat="server" clientidmode="Static">
                        
                </div>--%>
           
              </div>
            </div>
      </div>
      <div class="modal-footer">
         <asp:Button ID="btnAdd" class="btn btn-submit" runat="server" Text="Submit" OnClientClick="javascript:return validateUserAdd()" onclick="btnAdd_Click" ClientIDMode="Static" />  
      </div>
    </div>
  </div>
</div> 
<!-- Modal Add User End-->


<!-- Modal Edit User Start-->
<asp:HiddenField ID="hiddenEditUser"  runat="server" ClientIDMode="Static"/>
<div id="modalEditUser" class="modal fade" role="dialog">
  <div class="modal-dialog">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header"> <h4 class="modal-title">Edit Lab User</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
       
      </div>
      <div class="modal-body">
        <div class="cus-form">
              <div class="">                   
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Enter Full Name *" id="txtFullNameEdit" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblFullNameEdit" class="form-error"></label>
                  </div>
                   <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Enter Email Id" id="txtEmailIdEdit" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblEmailIdEdit" class="form-error"></label>
                  </div>
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Enter Contact Number" id="txtContactEdit"  onkeypress="return isNumber(event)" MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblContactEdit" class="form-error"></label>
                  </div>
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Role Description" id="txtDescriptionEdit" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblDescriptionEdit" class="form-error"></label>
                  </div>
                <asp:HiddenField ID="hiddenRolesEdit"  runat="server" ClientIDMode="Static"/>
              </div>
            </div>
      </div>
      <div class="modal-footer">
         <asp:Button ID="btnUpdate" class="btn btn-submit" runat="server" Text="Submit" OnClientClick="javascript:return validateUserEdit()" onclick="btnUpdate_Click" ClientIDMode="Static" />  
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
        <h1 class="ad-primary-color" style="margin-top: 10px;"><i class="fa fa-file-text" aria-hidden="true"></i></h1>
        <h4 class="ad-primary-color">Are you sure you want to delete this user</h4>
      </div>
      <div class="modal-footer">
        <asp:Button id="btnDeleteUserYes" class="btn btn-default ad-btn-secondary" runat="server"  Text="Yes" OnClick="btnDeleteUserYes_Click" ClientIDMode="Static" />
        <asp:Button id="btnDeleteUserNo" class="btn btn-default ad-btn-secondary" runat="server" data-dismiss="modal"  Text="No" ClientIDMode="Static" />
      </div>
    </div>
  </div>
</div>
<!-- modal Delete User Confirm End-->


<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/ManageUsersValidation.js"></script>
 <script src="js/code.js"></script>
  <script src="js/pagination.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#page a").click(function () {
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
    });
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


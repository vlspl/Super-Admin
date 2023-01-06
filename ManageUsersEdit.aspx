<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true" CodeFile="ManageUsersEdit.aspx.cs" Inherits="ManageUsersEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="wrapper">      
      <div id="content" class="">
         <div id="testlist" class="">
            <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4><a href="ManageUsers.aspx"><i class="fa fa-arrow-left" aria-hidden="true"></i></a>Edit User Details</h4>
                     </div>
                  </div>
               </div>
            </nav>
            <!--  -->            
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
                    <asp:TextBox class="form-control" placeholder="Enter Contact Number" id="txtContact" MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblContact" class="form-error"></label>
                    </div>
                    <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Role Description" id="txtDescription" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <label id="lblDescription" class="form-error"></label>
                    </div>
                    <asp:HiddenField ID="hiddenRoles"  runat="server" ClientIDMode="Static"/>
                    <div class="form-group" id="divRoles"  runat="server" clientidmode="Static">
                    </div>
                </div>
            </div>
            <div>
                 <asp:Button ID="btnUpdate" class="lab-btn-default" runat="server" Text="Update" OnClientClick="javascript:return validateUserEdit()" onclick="btnUpdate_Click" ClientIDMode="Static" />  
            </div>
         </div>
      </div>
   </div>

<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/ManageUsersValidation.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        
        $('#divRoles :checkbox').change(function () {
            
            if (this.checked) {
                $("#hiddenRoles").val($("#hiddenRoles").val() + "," + $(this).val());
                $("#hiddenRoles").val($("#hiddenRoles").val().replace(",,",","));
                //alert($("#hiddenRoles").val());
            } else {
                $("#hiddenRoles").val($("#hiddenRoles").val().replace($(this).val(), ""));
                $("#hiddenRoles").val($("#hiddenRoles").val().replace(",,", ","));
                //alert($("#hiddenRoles").val());
            }
        });
    });

</script>
</asp:Content>


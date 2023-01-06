<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true" CodeFile="ManageRoles.aspx.cs" Inherits="ManageRoles" %>

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
                        <h4>User Management</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                        <a href="" data-toggle="modal" data-target="#modalAddRole" class="lab-btn-white"><span class="fa fa-plus" aria-hidden="true"></span> Add Role</a>
                      </div>
                  </div>
               </div>
            </nav>

            <!-- tab content -->
            <div class="tab-content">
               
               <!-- Lab User List Start -->
               <div id="divRolesList" class="tab-pane fade in active">
                  <!-- filter -->
                 
                  <div class="wrappercontent">
                    <table class="table booking small-table">
                       <thead>
                          <tr>
                             <th>Role</th>
                             <th>Delete</th>
                          </tr>
                       </thead>
                       <tbody id="tbodyRolesList" runat="server" clientidmode="Static">
                       </tbody>
                    </table>
                  </div>
            </div>
         </div>
      </div>
   </div>

<!-- Modal Add Role Start-->
<div id="modalAddRole" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Add Role</h4>
      </div>
      <div class="modal-body">
        <div class="cus-form">
              <div class="">                   
                  <div class="form-group">
                    <%--<asp:TextBox class="form-control" placeholder="Enter role *" id="txtRole" runat="server" ClientIDMode="Static"></asp:TextBox>--%>
                    <select id="selRole" runat="server" clientidmode="Static">
                        <option  value="owner">owner</option>      
                        <option  value="supervisor">supervisor</option>      
                        <option value="assistant">assistant</option>      
                        <option value="receptionist">receptionist</option>                
                    </select>
                    <label id="lblRole" class="form-error"></label>
                  </div>           
              </div>
            </div>
      </div>
      <div class="modal-footer">
         <asp:Button ID="btnAdd" class="lab-btn-secondary" runat="server" Text="Submit" OnClientClick="javascript:return validateRoleAdd()" onclick="btnAdd_Click" ClientIDMode="Static" />  
      </div>
    </div>
  </div>
</div> 
<!-- Modal Add Role End-->

<!-- modal Delete Role Confirm Start-->
<asp:HiddenField ID="hiddenDeleteRole"  runat="server" ClientIDMode="Static"/>
<div class="modal fade" id="modalDeleteRoleConfirm" role="dialog">
  <div class="modal-dialog"> 
    
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-body text-center">
        <h1 class="ad-primary-color" style="margin-top: 10px;"><i class="fa fa-file-text" aria-hidden="true"></i></h1>
        <h4 class="ad-primary-color">Are you sure you want to delete this role</h4>
      </div>
      <div class="modal-footer">
        <asp:Button id="btnDeleteRoleYes" class="btn btn-default ad-btn-secondary" runat="server"  Text="Yes" OnClick="btnDeleteRoleYes_Click" ClientIDMode="Static" />
        <asp:Button id="btnDeleteRoleNo" class="btn btn-default ad-btn-secondary" runat="server" data-dismiss="modal"  Text="No" ClientIDMode="Static" />
      </div>
    </div>
  </div>
</div>

<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/ManageRolesValidation.js"></script>
<script type="text/javascript">
    $(document).ready(function () {

        $("#tbodyRolesList a").click(function () {
            if ($(this).attr("id")) {
                var id = $(this).attr("id");

                $("#hiddenDeleteRole").val($(this).attr("id"));
            }
        });
    });
</script>
</asp:Content>


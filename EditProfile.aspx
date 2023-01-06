<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="EditProfile.aspx.cs" Inherits="EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5 ">
        <a href="#" class="navbar-brand">Personal Details</a>
      </div>

     
    </div>
  </nav>
   <div class="table_div">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-6">
                    <div class="card ">
                        <h5 class="card-header fa-color">
                             <span id="spanUserName" runat="server"></span><span><a id="A1" href="#" runat="server" data-toggle="modal" data-target="#labcontact" class=" float-right"><i class="fa fa-edit fa-color mr-1"></i></a></span></h5>
                        <div class="card-body">
                            <p class=" text-15">
                                Email:   <span id="spanUserEmail" runat="server"></span></p>
                            <p class="text-15">
                                Contact: <span id="spanUserContact" runat="server"></span></p>
                            <p class="text-15">
                                UserId:  <span id="spanUserId" runat="server"></span></p>
                        </div>
                    </div>
                </div>
               
            </div>
        </div>
    </div>
    <div class="wrapper">
        <!-- Sidebar Holder -->
    
        <!-- sub header -->
        <div id="content">
        
            <!-- Modal -->
            <div id="labcontact" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header"> <h4 class="modal-title">
                                Edit Personal Details</h4>
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                           
                        </div>
                        <div class="modal-body">
                            <div class="cus-form">
                                <div class="">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Full Name *" ID="txtUserFullName"
                                            runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblFullName" class="form-error">
                                        </label>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Enter Email id *" ID="txtUserEmail"
                                            runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblUserEmail" class="form-error">
                                        </label>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Enter Contact Number" ID="txtUserContact"
                                            MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblUserContact" class="form-error">
                                        </label>
                                    </div>
                                    <div class="form-group hide">
                                        <asp:TextBox class="form-control" placeholder="Lab Address" ID="txtUserID" runat="server"
                                            ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblUserID" class="form-error">
                                        </label>
                                    </div>
                                    <div class="form-group hide">
                                        <asp:TextBox class="form-control" style="display:none;" placeholder="Lab Address" ID="txtLabid" runat="server"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                           
                            <asp:Button ID="btnUpdate" class="btn btn-color" runat="server" Text="Update"
                                OnClientClick="javascript:return validateLabContact()" OnClick="btnUpdate_Click"
                                ClientIDMode="Static" />
                                 <a href="" class="btn btn-submit" data-dismiss="modal">Close</a>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript" src="js/jquery.js"></script>
            <script type="text/javascript" src="js/LabInfoValidation.js"></script>
</asp:Content>

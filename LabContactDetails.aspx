<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="LabContactDetails.aspx.cs" Inherits="LabContactDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="wrapper">
        <!-- Sidebar Holder -->
        <script src="js/sidebar.js"></script>
        <!-- sub header -->
        <div id="content">
            <nav class="primary-col-back subheader">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-sm-6">
                     <h4>Lab Contact</h4>
                  </div>
                 <%-- <div class="col-sm-6">
                     <div class="subheader-search">
                        <h4 class="primary-col"><a href="" id="HideEditbtn" runat="server" data-toggle="modal" data-target="#labcontact"><i class="fa fa-pencil-square-o margin-0" aria-hidden="true"></i></a></h4>
                        
                     </div>
                  </div>--%>
               </div>
            </div>
         </nav>
            <div class="container-fluid lab-details">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <span id="spanLabName" style="font-size: larger" runat="server" clientidmode="Static">
                                        Lab Contact Details</span> <a href="#" id="A1" runat="server" data-toggle="modal"
                                            data-target="#labcontact"><i class="fa fa-pencil-square-o margin-0 pull-right" aria-hidden="true">
                                                Edit </i></a>
                                </h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <b>Email :</b>
                                    </div>
                                    <div class="col-lg-9">
                                        <span id="spanLabEmail" runat="server"></span>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        <b>Contact :</b>
                                    </div>
                                    <div class="col-lg-9">
                                        <span id="spanLabContact" runat="server"></span>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        <b>Lab Address :</b>
                                    </div>
                                    <div class="col-lg-9">
                                        <span id="spanLabAddress" runat="server"></span>
                                    </div>
                                </div>
                                <br />
                                <%-- <div class="row">
                                    <div class="col-lg-3">
                                        <script src='https://maps.googleapis.com/maps/api/js?v=3.exp'></script>
                                        <div style='overflow: hidden; height: 440px; width: 700px;'>
                                            <div id='gmap_canvas' style='height: 250px; width: 700px;'>
                                            </div>
                                            <style>
                                                #gmap_canvas img
                                                {
                                                    max-width: none !important;
                                                    background: none !important;
                                                }
                                            </style>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal -->
        <div id="labcontact" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">
                            Edit Lab Contact</h4>
                    </div>
                    <div class="modal-body">
                        <div class="cus-form">
                            <div class="">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Enter Email id *" ID="txtLabEmail"
                                        runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <label id="lblLabEmail" class="form-error">
                                    </label>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Enter Contact Number" ID="txtLabContact"
                                        MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <label id="lblLabContact" class="form-error">
                                    </label>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Lab Address" ID="txtLabAddress" runat="server"
                                        ClientIDMode="Static"></asp:TextBox>
                                    <label id="lblLabAddress" class="form-error">
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnUpdate" class="lab-btn-primary nextbtn" runat="server" Text="Update"
                            OnClientClick="javascript:return validateLabContact()" OnClick="btnUpdate_Click"
                            ClientIDMode="Static" />
                     <%--   <a href="" class="lab-btn-default" data-dismiss="modal">Close</a>--%>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript" src="js/jquery.js"></script>
        <script type="text/javascript" src="js/LabInfoValidation.js"></script>
</asp:Content>

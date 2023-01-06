<%@ Page Title="" Language="C#" MasterPageFile="~/CRMMasterPage.master" AutoEventWireup="true" CodeFile="CRMDashboard.aspx.cs" Inherits="CRMDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


  <nav class="navbar navbar-expand-sm bg-light navbar-header"> 
                          <div class="container-fluid">
                            <div class="navbar-title ">
                              <a href="#" class="navbar-brand">CRM Dashboard</a>
                            </div>
                          <div >
                          <ul class="navbar-nav ml-auto"> 
                              <li class="nav-item pt-1 "> 
                             <a href="Dashboard.aspx" id="A2"  runat="server" class="btn btn-secondary"><span><i class="fa fa-home mr-2" area-hidden="true"></i></span>Pathology Dashboard</a>
                              </li> 
                                                        
                          </ul> 
                        </div>
                      </div>
                      </nav>
  <div class="container">
        <div class="row">
          
            <div class="col-lg-12">
  <div class="table-responsive">
                       <table class="table table-bordered text-small" id="dataTable" width="100%" style="color: #56549b"
                            cellspacing="0">
                            <thead>
                                <tr>
                                    <th>
                                        Sr. No.
                                    </th>
                                  <%--  <th>
                                      User Name
                                    </th>--%>
                                    <th>
                                       Mobile No
                                    </th>
                                    <th>
                                        Message Name
                                    </th>
                                    <th>
                                       Date
                                    </th>
                                    <th>
                                    Status
                                    </th>
                                   
                                </tr>
                            </thead>
                            <tbody id="tbody_messageStatus" runat="server"  style="text-align: center">
                            </tbody>
                        </table>

                    </div>
</div>
</div>
</div>


</asp:Content>


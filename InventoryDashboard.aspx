<%@ Page Title="" Language="C#" MasterPageFile="~/inventoryMasterPage.master" AutoEventWireup="true" CodeFile="InventoryDashboard.aspx.cs" Inherits="InventoryDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="wrapper">
        <!-- Content Wrapper -->
        <div id="content-wrapper" class="d-flex flex-column">
            <!-- Main Content -->
            <div id="content">
                <!-- Topbar -->
                
                <nav class="navbar navbar-expand navbar-light bg-white topbar mb-2 static-top shadow">
          <h3 class="text-dark d-none d-sm-block" style="width:100%;"><span class="text-primary">Inventory Dashboard</span> <span class="text-dgreen" style="float:right;">  <a href="Dashboard.aspx" id="A2"  runat="server" class="btn btn-secondary"><span><i class="fa fa-home mr-2" area-hidden="true"></i></span> Dashboard</a></span> </h3>
        
                    <!-- Topbar Navbar -->
                       
        </nav>
                <!-- End of Topbar -->
                <!-- Begin Page Content -->
                <div class="container-fluid">
                    <!-- Page Heading -->
                    <!-- <div class="d-sm-flex align-items-center justify-content-between mb-2">
            <h1 class="h3 m-0 P-0 text-gray-800">Dashboard</h1>
          </div> -->
                    <!-- Content Row -->

             
              


                    <div class="row mt-3" id="topCards">
                        <asp:HiddenField ID="hLitTotalTest" runat="server" ClientIDMode="Static" />
                        <!-- Earnings (Monthly) Card Example -->
                         

                        <!-- Earnings (Monthly) Card Example -->
                        <div class="col-xl-4 col-md-6 mb-4">
                            <div class="card border-left-info shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-info text-uppercase mb-1">No Of Bills
                                            </div>
                                             
                                                    <div class="h5 mb-0 mr-3 font-weight-bold text-gray-800"># &nbsp;  <asp:Label ID="lblnoofBills" Style="margin-left: -10px;"
                                                    runat="server">0</asp:Label> <i style="float:right;" class="fas fa-check-circle fa-2x text-gray-300"></i></div>
                                              
                                        </div>
                                      
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Pending Requests Card Example -->
                        <div class="col-xl-4 col-md-6 mb-4">
                            <div class="card border-left-warning shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-warning text-uppercase mb-1">
                                               Paid Bills</div>
                                            <div class="h5 mb-0 font-weight-bold text-gray-800"># &nbsp;  <asp:Label ID="lblpaidBills" Style="margin-left: -10px;"
                                                    runat="server">0</asp:Label> <i style="float:right;"  class="fas fa-comments fa-2x text-gray-300"></i></div>
                                        </div>
                                     
                                    </div>
                                </div>
                            </div>
                        </div>
                          <div class="col-xl-4 col-md-6 mb-4">
                            <div class="card border-left-danger shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-danger text-uppercase mb-1">
                                               Unpaid Bills</div>
                                            <div class="h5 mb-0 font-weight-bold text-gray-800"># &nbsp;  <asp:Label ID="lblunpaidBills" Style="margin-left: -10px;"
                                                    runat="server">0</asp:Label> <i style="float:right;"  class="fas fa-comments fa-2x text-gray-300"></i></div>
                                        </div>
                                     
                                    </div>
                                </div>
                            </div>
                        </div>
                      <br />
                        <div class="col-xl-4 col-md-6 mb-4">
                            <div class="card border-left-primary shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                               Material Count</div>
                                            <div class="h5 mb-0 font-weight-bold text-gray-800"># &nbsp; <asp:Label ID="lblmaterialCount"  runat="server" style="margin-left: -10px;"
                                                    ClientIDMode="Static">0</asp:Label> <i style="float:right;" class="fas fa-file fa-2x text-gray-100"></i></div>
                                        </div>
                                       
                                    </div>
                                </div>
                            </div>
                        </div>
                          <div class="col-xl-4 col-md-6 mb-4">
                            <div class="card border-left-success shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                                Vendor Count </div>
                                            <div class="h5 mb-0 font-weight-bold text-gray-800"># &nbsp; <asp:Label ID="lblvendorCount" Style="margin-left: -10px;" runat="server">0</asp:Label> <i style="float:right;" class="fas fa-clipboard-list fa-2x text-gray-300"></i></div>
                                        </div>
                                      
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                  
                </div>
                <!-- /.container-fluid -->
            </div>
        </div>
        <!-- End of Main Content -->
        <!-- End of Footer -->
    </div>

</asp:Content>


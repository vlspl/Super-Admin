<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="PedingLabApproval.aspx.cs" Inherits="SuperAdmin_PedingLabApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div class="wrapper">
        <div id="content">
            <nav class="primary-col-back subheader">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-sm-6">
                     <h4>Pending Lab Approval</h4>
                  </div>                 
               </div>
            </div>
         </nav>
          
            <!-- wrapper content -->
            <div class="wrappercontent">
                <div class="box-body" style="padding: 20px">
                    <table class="table text-center booking" id="Labgrid">
                        <thead>
                            <tr>
                              <th>
                                    Sr No
                                </th>
                                <th>
                                    Lab Name
                                </th>
                                <th>
                                    Lab Address
                                </th>
                           
                                <th>
                                   Organization Name
                                </th>
                             
                                 <th>
                                   Status
                                </th>
                               
                                <th>
                                    Action
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tbodyAllLabApproval" runat="server" clientidmode="Static">
                        </tbody>
                    </table>
                    <div class="paging">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var rows = $("#tbodyAllLabApproval").find("tr").hide();
            rows.filter(":contains('Pending Approval')").show();

            $("#rdoActive").change(function () {
                var rows = $("#tbodyAllLabApproval").find("tr").hide();
                rows.filter(":contains('Pending Approval')").show();
            });

            $("#rdoInactive").change(function () {
                var rows = $("#tbodyAllLabApproval").find("tr").hide();
                rows.filter(":contains('Pending Approval')").show();
            });
        });
    </script>--%>
    <%--start Pagination and sorting data --%>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
   
</asp:Content>


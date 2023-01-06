<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="LabsManagement.aspx.cs" Inherits="SuperAdmin_LabsManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="wrapper">
        <div id="content">
            <nav class="primary-col-back subheader">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-sm-6">
                     <h4>Lab List</h4>
                  </div>                 
               </div>
            </div>
         </nav>
            <!-- search by modal -->
            <!-- progress status bae -->
            <%--      <div class="header-wrap">
          <div class="approval">
          <div class="row">
          <div class="col-sm-6">
            <input type="radio" class="radio-custom" id="rdoActive" name="nameActive" value="Active" clientidmode="Static" checked="checked">
            <label for="radio-1" class="radio-custom-label">Active</label>
          </div>
           <div class="col-sm-6">
            <input type="radio" class="radio-custom" id="rdoInactive" name="nameActive" value="Inactive" clientidmode="Static">
            <label for="radio-2" class="radio-custom-label">Inactive</label>
        </div>
      </div>
        </div>

         </div>--%>
            <!-- wrapper content -->
            <div class="wrappercontent">
                <div class="box-body" style="padding: 20px">
                    <table class="table text-center booking" id="Labgrid">
                        <thead>
                            <tr>
                                <th>
                                    Lab ID
                                </th>
                                <th>
                                    Lab Name
                                </th>
                                <th>
                                    Lab Address
                                </th>
                                <th>
                                    Status
                                </th>
                                <th>
                                    Contact Number
                                </th>
                                <th>
                                    Action 1
                                </th>
                                <th>
                                    Action 2
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tbodyAllLabList" runat="server" clientidmode="Static">
                        </tbody>
                    </table>
                    <div class="pagings">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var rows = $("#tbodyAllLabList").find("tr").hide();
            rows.filter(":contains('Active')").show();

            $("#rdoActive").change(function () {
                var rows = $("#tbodyAllLabList").find("tr").hide();
                rows.filter(":contains('Active')").show();
            });

            $("#rdoInactive").change(function () {
                var rows = $("#tbodyAllLabList").find("tr").hide();
                rows.filter(":contains('Inactive')").show();
            });
        });
    </script>
    <script type="text/javascript">
        $(".pagings").quickPagination({ pagerLocation: "both", pageSize: "15" });
    </script>
    <%--start Pagination and sorting data --%>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script>
        $('#Labgrid').datatable({
            pageSize: 10,
            sort: [true, true, false],
            filters: [true, true, true, 'select', true, false, false],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>
    <%--end Pagination and sorting data --%>
</asp:Content>

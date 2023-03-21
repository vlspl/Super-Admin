<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="PatientManagementHealthProfile.aspx.cs" Inherits="PatientManagementHealthProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       <div class="wrapper">
      <div id="content">
         <nav class="primary-col-back subheader">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-sm-6">
                     <h4>Reports</h4>
                  </div>
                 <%-- <div class="col-sm-6">
                     <div class="subheader-search">
                        <a href="addnewtest.html"><i class="fa fa-plus-circle" aria-hidden="true"></i></a>
                        <div id="custom-search-input">
                           <div class="input-group">
                              <input type="text" class="  search-query form-control" placeholder="Search" data-toggle="modal" data-target="#searchmodal"/>
                              <span class="input-group-btn">
                              <button class="btn btn-danger search-icon" type="button">
                              <span class=" glyphicon glyphicon-search"></span>
                              </button>
                              </span>
                           </div>
                        </div>
                     </div>
                  </div>--%>
               </div>
            </div>
         </nav>
         <!-- search by modal -->
         <!-- progress status bae -->
<%--         <div class="header-wrap">      
             <!-- Approved/Unapproved/Rejected reports start -->
             <div class="approval">
          <div class="row">
          <div class="col-sm-4">
            <input type="radio" class="radio-custom" id="rdoReportApproved" name="reportApprovedUnapprovedRejected" value="approved" clientidmode="Static" checked="checked">
            <label for="radio-1" class="radio-custom-label">Approved</label>
          </div>
           <div class="col-sm-4">
            <input type="radio" class="radio-custom" id="rdoReportApprovalPending" name="reportApprovedUnapprovedRejected" value="approval pending" clientidmode="Static">
            <label for="radio-2" class="radio-custom-label">Approval Pending</label>
        </div>
        <div class="col-sm-4">
            <input type="radio" class="radio-custom" id="rdoReportRejected" name="reportApprovedUnapprovedRejected" value="rejected" clientidmode="Static">
            <label for="radio-2" class="radio-custom-label">Rejected</label>
        </div>
      </div>
        </div>
                <%-- <div class="row">
                    <input type="radio" id="rdoReportApproved" name="reportApprovedUnapprovedRejected" value="approved" clientidmode="Static" checked="checked">Approved</input>
                    <input type="radio" id="rdoReportApprovalPending" name="reportApprovedUnapprovedRejected" value="approval pending" clientidmode="Static">Approval Pending</input>
                    <input type="radio" id="rdoReportRejected" name="reportApprovedUnapprovedRejected" value="rejected" clientidmode="Static">Rejected</input>
                </div> --%>
         <!-- Approved/Unapproved/Rejected reports end 
         </div>--%>
         <!-- wrapper content -->
         <div class="wrappercontent">         
      		 <div class="paging"></div>
            <table class="table booking" id="patientmanagementhealthprofile">
               <thead>
                    <tr>
                        <th>Booking ID</th>
                        <th>Patient Name</th>
                       
                        <th>Test Code</th>
                        <th>Test Name</th>
                        <th>Test Taken On</th>   
                                           
                        <th>Fees</th>                            
                        <th>Book Mode</th>                             
                                              
                        <th>Approval Status</th>                         
                        <th>Action</th>                         
                    </tr>
                </thead>
                <tbody id="tbodyReports" runat="server" clientidmode="Static">                  
                </tbody>
            </table>            
      		 <div class="paging"></div>
         </div>
      </div>
   </div>

<script type="text/javascript">
    $(document).ready(function () {
        var rows = $("#tbodyReports").find("tr").hide();
        rows.filter(":contains('approved')").show();

        $("#rdoReportApproved").change(function () {
            var rows = $("#tbodyReports").find("tr").hide();
            rows.filter(":contains('approved')").show();
        });

        $("#rdoReportApprovalPending").change(function () {
            var rows = $("#tbodyReports").find("tr").hide();
            rows.filter(":contains('approval pending')").show();
        });

        $("#rdoReportRejected").change(function () {
            var rows = $("#tbodyReports").find("tr").hide();
            rows.filter(":contains('rejected')").show();
        });
    });
</script>



  <%--start Pagination and sorting data --%>
      <script src="js/jquery.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>

    <script>

        $('#patientmanagementhealthprofile').datatable({
            pageSize: 10,
            sort: [true, true, false],
            filters: [true, true, true, true, true, true, 'select', true, 'select' ],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });

      


    </script>

      <%--end Pagination and sorting data --%>


</asp:Content>


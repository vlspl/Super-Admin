<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="PatientManagementViewHealthProfile.aspx.cs" Inherits="PatientManagementViewHealthProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    <nav class="primary-col-back subheader">
        <div class="container">
			<div class="row">
				<div class="col-sm-4">
					<h4>
                        <a ></a>Report                                               
                    </h4>                                  
				</div>			
			</div>
	    </div>
    </nav>
</div>

<!-- Booking Details -->
<div class="details">
	<div class="container">
        <div class="rowdetails">
		    <div class="row">
			    <div class="col-sm-6">
				    Lab Name : <span id="spanLabName" runat="server" clientidmode="Static"></span>
			    </div>
			    <div class="col-sm-6">
            Lab Contact : <span id="spanLabContact" runat="server" clientidmode="Static"></span>		    
			    </div>				
		    </div>
        </div>

         <div class="rowdetails">
		    <div class="row">
			    <div class="col-sm-6">
				    Lab Address : <span id="spanLabAddress" runat="server" clientidmode="Static"></span>
			    </div>
          <div class="col-sm-6">
            Booking Id : <span id="spanBookingId" runat="server" clientidmode="Static"></span>
          </div>
		    </div>
	    </div> 

         <div class="rowdetails">
		    <div class="row">
			    
                <div class="col-sm-6">
				    Report Id : <span id="spanReportId" runat="server" clientidmode="Static"></span>
			    </div>
		    </div>
	    </div>

	    <div class="rowdetails">
		    <div class="row">
			    <div class="col-sm-6">
				    Patient :  <span id="spanPatientName" runat="server" clientidmode="Static"></span>
			    </div>
			    <div class="col-sm-6">
            Gender :  <span id="spanGender" runat="server" clientidmode="Static"></span>				    
			    </div>				
		    </div>
        </div>

	    <div class="rowdetails">
		    <div class="row">            
			    <div class="col-sm-6">
				     Doctor :  <span id="spanDoctorName" runat="server" clientidmode="Static"></span>
			    </div>
          <div class="col-sm-6">
            Test taken on : <span id="spanTestTakenOn" runat="server" clientidmode="Static"></span>
          </div>
		    </div>
	    </div>
         <div class="rowdetails">
		    <div class="row">
			    <div class="col-sm-6">
				    Report created on : <span id="spanReportCreatedOn" runat="server" clientidmode="Static"></span>
			    </div>
			    <div class="col-sm-6">
				    Report created by : <span id="spanReportCreatedBy" runat="server" clientidmode="Static"></span>
			    </div>
		    </div>
	    </div>
       
        <div class="rowdetails">
		    <div class="row">
			    <div class="col-sm-6">
				    Approval Status : <span id="spanApprovalStatus" runat="server" clientidmode="Static"></span>
			    </div>
		    </div>
	    </div>

       <%-- <div class="rowdetails">
		    <div class="row">
			    <div class="col-sm-6">
				    Comment : <span id="spanComment" runat="server" clientidmode="Static"></span>
			    </div>
		    </div>
	    </div>--%>
	</div>
</div>

<div class="container">
    <div>
        <h4 class="text-center"><span id="spanTestCodeName" runat="server" clientidmode="Static"></span></h4>
    </div>
    <div class="createreporttable">
        <table class="table text-center table-bordered">
            <tbody id="tbodyTestValueResult" runat="server">
            </tbody>
        </table>       
        <div class="reports-notes">
            <p>Notes: <span id="spanNotes" runat="server" clientidmode="Static"></span></p>
        </div>
         
		<div class="reports-notes">
			<p>Comment : <span id="spanComment" runat="server" clientidmode="Static"></span></p>
		</div>		   
    </div>             
    <div id="divApproveReject" runat="server" clientidmode="Static">
        <asp:HiddenField ID="hiddenReportStatus" runat="server" ClientIDMode="Static" />
        <a href="" class="lab-btn-primary" data-toggle="modal" data-target="#modalApproveReject" id="btnApprove" clientidmode="Static">Approve</a>
        <a href="" class="lab-btn-secondary" data-toggle="modal" data-target="#modalApproveReject" id="btnReject" clientidmode="Static">Reject</a>
    </div>
    <div class="reportvalueedit">
    <span id="btnEditReport" runat="server"><a href="" data-toggle="modal" data-target="#modalEditReport" class="lab-btn-secondary"> Edit</a></span>   
  </div>
</div>


<!-- Modal Approve Reject-->
<div id="modalApproveReject" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Approve/Reject</h4>
      </div>
      <div class="modal-body">
        <div class="cus-form">
              <div class="">                   
                  <div class="form-group">
                    <asp:TextBox class="form-control" placeholder="Comment" id="txtComment" TextMode="MultiLine" Rows="5" runat="server" ClientIDMode="Static"></asp:TextBox>                    
                  </div>
              </div>
            </div>
      </div>
<%--      <div class="modal-footer">
        <asp:Button ID="btnUpdate" class="lab-btn-default" runat="server" Text="Update" onclick="btnUpdate_Click" ClientIDMode="Static" />  
      </div>--%>
    </div>
  </div>
</div> 

<!-- Modal Edit Report-->
<div id="modalEditReport" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Edit Report</h4>
      </div>
      <div class="modal-body">
        <div class="cus-form">
              <div class="">                   
                   <asp:HiddenField ID="hiddenValueIdList" runat="server" ClientIDMode="Static" />
                   <table class="table text-center booking">
                        <tbody id="tbodyTestValueResultEdit" runat="server">
                        </tbody>
                    </table> 
              </div>
              <div>
                <textarea id="txtNotes" runat="server" placeholder="Notes" clientidmode="Static" class="form-control"></textarea>
            </div>
            </div>
      </div>
   <%--   <div class="modal-footer">
        <asp:Button ID="btnUpdateReport" class="lab-btn-default" runat="server" Text="Update" onclick="btnUpdateReport_Click" ClientIDMode="Static" />  
      </div>--%>
    </div>
  </div>
</div> 


<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#btnApprove").click(function () {
            $("#hiddenReportStatus").val("approved");
        });

        $("#btnReject").click(function () {
            $("#hiddenReportStatus").val("rejected");
        });
    });
</script>
</asp:Content>


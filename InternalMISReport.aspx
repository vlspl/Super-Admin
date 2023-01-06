<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="InternalMISReport.aspx.cs" Inherits="InternalMISReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
 <script type="text/javascript">
     function showModal() {
         $("#myModal").modal('show');
     }

     </script>
  <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
 <script type="text/javascript">

     function goBack() {
         var url = 'AnalyteMaster.aspx';
         window.location.href = url;
     }
 </script>

 <link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <nav class="navbar navbar-expand-sm bg-light navbar-header"> 
                          <div class="container-fluid">
                            <div class="navbar-title ml-5">
                              <a href="#" class="navbar-brand">MIS Report</a>
                            </div>
                          <div class="mr-5">
                          <ul class="navbar-nav ml-auto"> 
                             
                              
                            
                              <li class="nav-item pt-1 mr-3"> 
                               <a href="Dashboard.aspx" id="A1"  runat="server" class="btn btn-color"><span><i class="fa fa-home mr-2" area-hidden="true"></i></span> Dashboard</a> 
                          
                              </li>                              
                          </ul> 
                        </div>
                      </div>
                      </nav>
    <div class="table_div" >
        <div class="container-fluid">
         <div class="row">
             <div class="col-md-3">
                            <div class="form-group">
                            <label>Report Name</label>
                                <asp:DropDownList ID="drpreportName" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                           <div class="col-md-3">
                            <div class="form-group">
                            <label>From Date</label>
                                <asp:TextBox ID="txtfromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender ID="txtfromDate_CalendarExtender" runat="server" 
                                    DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                                    TargetControlID="txtfromDate" TodaysDateFormat="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                         <div class="col-md-3">
                            <div class="form-group">
                            <label>To Date</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                    DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                                    TargetControlID="txttodate" TodaysDateFormat="dd/MM/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                         <div class="col-md-3">
                             <asp:Button ID="btnsearch" runat="server" Text="Search" 
                                 style="margin-top:30px;" CssClass="btn btn-success" onclick="btnsearch_Click" />
                                   <asp:Button ID="btnexporttoexcel" runat="server" Text="Export to Excel"  style="margin-top:30px; margin-left:5px;"
          CssClass="btn btn-info" onclick="btnexporttoexcel_Click" />
                         </div>
                         
                       
                    </div>

           <div class="container">
        <div class="row">
          
            <div class="col-lg-12">
  <div class="table-responsive">
  <asp:UpdatePanel ID="upgrid" runat="server">
  <ContentTemplate>
    <div style="height:450px; overflow:auto;">
      <asp:GridView ID="gridmisreport" CssClass="table" runat="server"  ShowFooter="true" EmptyDataText="Data Not Found...">
      </asp:GridView>
      </div>
    
       </ContentTemplate>
       <Triggers>
       <asp:PostBackTrigger ControlID="btnexporttoexcel" />
       </Triggers>
  </asp:UpdatePanel>
                    </div>
                   

</div>
 <br />
 
  <div class="col-lg-12">
    
  </div>
</div>
</div>


            
        </div>
    </div>
   
   
   
  
</asp:Content>


















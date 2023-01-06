<%@ Page Title="" Language="C#" MasterPageFile="~/CRMMasterPage.master" AutoEventWireup="true" CodeFile="TemplateRequest.aspx.cs" Inherits="TemplateRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdnrequestId" runat="server" />
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
           <nav class="navbar navbar-expand-sm bg-light navbar-header"> 
                          <div class="container-fluid">
                            <div class="navbar-title ">
                              <a href="#" class="navbar-brand">Add Template Request</a>
                            </div>
                          <div >
                          <ul class="navbar-nav ml-auto"> 
                              <li class="nav-item pt-1 "> 
                               <a href="ViewTemplateRequest.aspx" id="A3"  runat="server" class="btn btn-color"><span><i class="fa fa-eye mr-2" area-hidden="true"></i></span> View Templates</a> 
                          
                              </li> 
                                                        
                          </ul> 
                        </div>
                      </div>
                      </nav>

        <div class="wrapper" style="margin-left:20px;">
        <div id="Div1">
       <div class="table_div">
                    <div class="container" style="min-height:450px;">
                        <div class="row testDetail ">
                            <div class="col col-md-4">
                             <div class="form-group">
                    <label>
                       Template Name <span style="color: Red">*</span></label>
                
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtTempName"
                        required placeholder=" Template Name"></asp:TextBox>
                   
                </div>
                            </div>
                             <div class="col col-md-4">
                             <div class="form-group">
                   <label>
                       Message Type <span style="color: Red">*</span></label>
                         <asp:DropDownList ID="drpTrans" DataTextField="status" DataValueField="status" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True">
                        <asp:ListItem>-Select-</asp:ListItem>
                        <asp:ListItem>trans</asp:ListItem>
							 <asp:ListItem>prom</asp:ListItem>
                    </asp:DropDownList>
                  
                </div>
   </div>
    <div class="col col-md-4">
     <div class="form-group">
                    <label>
                     Parameter Count <span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtParameter"
                        required placeholder="Parameter"></asp:TextBox>
                </div>
                </div>
                            </div>
                             <div class="row ">
                              <div class="col col-md-4">
                                <div class="form-group">
                   <label>
                       Message Body <span style="color: Red">*</span></label>
                   <asp:TextBox runat="server" class="form-control" ID="txtMsgText"
                         placeholder="Message Text" TextMode="MultiLine" style="height:125px; resize:none;"   />
                </div>
                              </div>
                             <div class="col col-md-4">
                                <div class="form-group">
                    <label>
                       Parameter List <span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtParaneterList"
                        required placeholder="Eg: test1,test2,test3"></asp:TextBox>
                </div>
              
                  <div class="form-group">
                    <label>
                       Approval Status <span style="color: Red">*</span></label>
                     <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtapprovalStatus"
                       Text="Pending" ReadOnly="true"></asp:TextBox>

<%--                    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:MedicalDbBackupConnectionString5 %>' SelectCommand="SELECT  Distinct case when status=1 then 'Approved' else'Rejected' End as[status]  FROM  [tbl_WhatsappMsgMaster]"></asp:SqlDataSource>--%>
                </div>
                              </div>
                               <div class="col col-md-4">
                               <div class="form-group">
                    <label>
                      Request By <span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtRequestBy"
                        required placeholder="Request By"></asp:TextBox>
                </div>
              
               <div class="form-group">
                      <label>
                Request Date <span style="color: Red">*</span></label>
                       <asp:TextBox ID="txtApproveDate" runat="server" cssClass="form-control"  ></asp:TextBox>
                     <asp:CalendarExtender ID="txtApproveDate_CalendarExtender" runat="server" 
                            DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                            TargetControlID="txtApproveDate" TodaysDateFormat="dd/MM/yyyy">
                        </asp:CalendarExtender>
                       
                    </div>
                </div>
                             </div>
                              <div class="row ">
                             <div class="col col-md-4">
                               
                             </div>
                                <div class="col col-md-4">
                                <asp:Button ID="btnrequest" runat="server" type="submit" style="margin-top:25px;" class="fa fa-save btn btn-sm btn-primary" 
                        Text="Submit Request" onclick="btnrequest_Click"   />
                             </div>
                              <div class="col col-md-4">
                              <asp:Button ID="btnupdate" runat="server" type="submit" style="margin-top:25px;" 
                                      Visible="false" class="fa fa-save btn btn-sm btn-primary" 
                        Text="Update Request" onclick="btnupdate_Click"    />
                         <asp:Button ID="btndelete" runat="server" type="submit" style="margin-top:25px;" 
                                      Visible="false" class="fa fa-save btn btn-sm btn-danger" 
                        Text="Delete Request" onclick="btndelete_Click"    />
                             </div>
                              </div>
                             
                             <br /><br />
                             
                            </div>
                            </div>
                            </div>
                            </div>


    
         </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


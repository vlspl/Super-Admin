<%@ Page Title="" Language="C#" MasterPageFile="~/CRMMasterPage.master" AutoEventWireup="true" CodeFile="ViewTemplateRequest.aspx.cs" Inherits="ViewTemplateRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="Content/vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">

   <script src="Content/vendor/jquery/jquery.min.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
   
    <script src="Content/js/demo/datatables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
            <nav class="navbar navbar-expand-sm bg-light navbar-header"> 
                          <div class="container-fluid">
                            <div class="navbar-title ">
                              <a href="#" class="navbar-brand">View Templates</a>
                            </div>
                          <div >
                          <ul class="navbar-nav ml-auto"> 
                              <li class="nav-item pt-1 "> 
                              <a href="TemplateRequest.aspx" id="A3"  runat="server" class="btn btn-color"><span><i class="fa fa-plus mr-2" area-hidden="true"></i></span> Add New</a> 
                          
                              </li> 
                                                        
                          </ul> 
                        </div>
                      </div>
                      </nav>
         
        <div class="wrapper" style="margin-left:20px;">
        <div id="Div1">
       <div class="table_div">
                    <div class="container" style="min-height:450px;">
                      
                             <div class="row">
                             
         <div class="col-lg-12 col-md-12" >
             <div class="box-body" style="padding: 20px">
                   <table class="table table-bordered text-small" id="dataTable" width="100%" style="color: #56549b"
                            cellspacing="0">
                        <thead>
                            <tr>
                                <th>
                                   Sr No
                                </th>
                                <th>
                                    Template Name
                                </th>
                                <th>
                                   Body
                                </th>
                               <th>
                                   Parameters
                                </th>
                                <th>
                                   Request By
                                </th>
                                  <th>
                                   Request Date
                                </th>
                                <th>
                                   Approval Status
                                </th>
                                <th>
                                        Action
                                    </th>
                            </tr>
                        </thead>
                        <tbody id="tbodytemplateRequest" runat="server" clientidmode="Static">
                        </tbody>
                    </table>
                    <div class="paging">
                </div>
                </div>
  <%--  <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
   --%>
              <br />
          </div>
                             </div>
                            </div>
                            </div>
                            </div>
                            </div>


    
         </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


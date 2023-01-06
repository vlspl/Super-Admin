<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="PatientManagement.aspx.cs" Inherits="SuperAdmin_PatientManagement" %>

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
        
         <!-- wrapper content -->
         <div class="wrappercontent">
            <table class="table booking">
               <thead>
                    <tr>
                        <th>Patient ID</th>
                        <th>Patient Name</th>
                        <th>Gender</th>
                        <th>Birth Date</th>
                        <th>Address</th>
                        <th>Contact Number</th>        
                        <th>Action 1</th>
                        <th>Action 2</th>                      
                    </tr>
                </thead>
                <tbody id="tbodyReports" runat="server" clientidmode="Static">                  
                </tbody>
            </table>
         </div>
      </div>
   </div>

<script type="text/javascript" src="js/jquery.js"></script>
    
</asp:Content>


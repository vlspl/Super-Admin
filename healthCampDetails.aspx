<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="healthCampDetails.aspx.cs" Inherits="SuperAdmin_healthCampDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4><asp:Label ID="lblhcname" runat="server" Text=""></asp:Label></h4>
                     </div>
                      <div class="col-sm-6 text-right" >
                       <a  href="healthcampDilldown.aspx" class="btn btn-danger" style="margin-right:60px;"><i class="fa fa-arrow-left"></i>Back</a>
                       </div>
                     
                  </div>
               </div>
            </nav><br />
    <asp:HiddenField ID="hdnhelthcampId" runat="server" />
<div class="container"></div>
    <asp:HiddenField ID="hdnlabId" runat="server" />
<div id="exTab2" class="container">	
<ul class="nav nav-tabs">
			<li class="active">
        <a  href="#1" data-toggle="tab">Health Camp`Details</a>
			</li>
			<li><a href="#2" data-toggle="tab">Organization Details</a>
			</li>
			<li><a href="#3" data-toggle="tab">Users Details</a>
			</li>
            <li><a href="#4" data-toggle="tab">Lab Details</a>
			</li>
         
		</ul>

			<div class="tab-content ">
			  <div class="tab-pane active" id="1">
              <br />
               <asp:FormView ID="FormView_labDetails" runat="server">
                     <ItemTemplate>  
                    <table class="table table-bordered table-striped">  
                        <tr>  
                            <td>Health Camp ID</td>  
                            <td><%#Eval("healthcampID")%></td>  
                        </tr>  
                        <tr>  
                            <td>Health Camp Name</td>  
                            <td><%#Eval("healthCampName")%></td>  
                        </tr>  
                        <tr>  
                            <td>Owner Name</td>  
                            <td><%#Eval("ownerName")%></td>  
                        </tr>  
                        <tr>  
                            <td>Description</td>  
                            <td><%#Eval("otherDetails")%></td>  
                        </tr>  
                        
                        
                    </table>  
                </ItemTemplate> 
                    </asp:FormView>
				</div>
				<div class="tab-pane" id="2" style="width:94%; height:420px; overflow:auto;"><br />
              <div class="tab-content">
            <!-- My Test List Start -->
            <div class="detailtable table-responsive">
                <table class="table text-center table-bordered table-hover booking" id="HCORG" style="width:95%;">
                    <thead>
                        <tr>
                           <th>
                                Sr No
                            </th>
                             <th>
                                 Name
                            </th>
                            <th>
                               Contact
                            </th>
                            <th>
                                 Email 
                            </th>
                            <th>
                              Status
                            </th>
                           <th>
                              Address
                            </th>
                         
                        </tr>
                    </thead>
                    <tbody id="tbodyHCOrgList" runat="server" clientidmode="Static">
                    </tbody>
                </table>
               
            </div>
        </div>
         <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
   
				</div>
        <div class="tab-pane" id="3" style="width:94%; height:420px; overflow:auto;">
              <br />
               <div class="tab-content">
            <!-- My Test List Start -->
            <div class="detailtable table-responsive">
                <table class="table text-center table-bordered table-hover booking" id="hUser" style="width:95%;">
                    <thead>
                        <tr>
                           <th>
                                Sr No
                            </th>
                             <th>
                                 User Name
                            </th>
                            <th>
                               Contact
                            </th>
                            <th>
                                 Email 
                            </th>
                            
                         
                        </tr>
                    </thead>
                    <tbody id="tbodyHealthcampUser" runat="server" clientidmode="Static">
                    </tbody>
                </table>
               
            </div>
        </div>
                </div>

                 <div class="tab-pane" id="4" style="width:94%; height:420px; overflow:auto;">
              <br />
               <div class="tab-content">
            <!-- My Test List Start -->
            <div class="detailtable table-responsive">
                <table class="table text-center table-bordered table-hover booking" id="Table1" style="width:95%;">
                    <thead>
                        <tr>
                           <th>
                                Sr No
                            </th>
                             <th>
                                 Lab Code
                            </th>
                            <th>
                               Lab Name
                            </th>
                            <th>
                                 Owner Name 
                            </th>
                            <th>
                                 Email ID
                            </th>
                         <th>
                                Status 
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tbody_hclab" runat="server" clientidmode="Static">
                    </tbody>
                </table>
               
            </div>
        </div>
                </div>
                 
                 
                 


			</div>
  </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="dashOrgDetails.aspx.cs" Inherits="SuperAdmin_dashOrgDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4><asp:Label ID="lblorgname" runat="server" Text=""></asp:Label></h4>
                     </div>
                      <div class="col-sm-6 text-right">
                       </div>
                     
                  </div>
               </div>
            </nav><br />
    <asp:HiddenField ID="hdnorgid" runat="server" />
<div class="container"></div>
    <asp:HiddenField ID="hdnlabId" runat="server" />
<div id="exTab2" class="container">	
<ul class="nav nav-tabs">
			<li class="active">
        <a  href="#1" data-toggle="tab">Organization`Details</a>
			</li>
			<li><a href="#2" data-toggle="tab">Branch Details</a>
			</li>
			<li><a href="#3" data-toggle="tab">Employee List</a>
			</li>
            <%--    <li><a href="#4" data-toggle="tab">Logo</a>
			</li>
          <li><a href="#5" data-toggle="tab">Test List</a>
			</li>
              <li><a href="#6" data-toggle="tab">Lab List</a>
			</li>--%>
         
		</ul>

			<div class="tab-content ">
			  <div class="tab-pane active" id="1">
              <br />
               <asp:FormView ID="FormView_labDetails" runat="server">
                     <ItemTemplate>  
                    <table class="table table-bordered table-striped">  
                        <tr>  
                            <td>Orgnization ID</td>  
                            <td><%#Eval("ID")%></td>  
                        </tr>  
                        <tr>  
                            <td>Orgnization Name</td>  
                            <td><%#Eval("Name")%></td>  
                        </tr>  
                        <tr>  
                            <td>Address</td>  
                            <td><%#Eval("Address")%></td>  
                        </tr>  
                        <tr>  
                            <td>Contact</td>  
                            <td><%#Eval("Contact")%></td>  
                        </tr>  
                         <tr>  
                            <td>Email</td>  
                            <td><%#Eval("Email")%></td>  
                        </tr>  
                        
                    </table>  
                </ItemTemplate> 
                    </asp:FormView>
				</div>
				<div class="tab-pane" id="2" style="width:100%; height:420px; overflow:auto;"><br />
              <div class="tab-content">
            <!-- My Test List Start -->
            <div class="detailtable table-responsive">
                <table class="table text-center table-bordered table-hover booking" id="orgbr" style="width:95%;">
                    <thead>
                        <tr>
                           <th>
                                No
                            </th>
                             <th>
                                 Branch 
                            </th>
                            <th>
                               Status
                            </th>
                            <th>
                                 Address 
                            </th>
                            <th>
                              Country
                            </th>
                           <th>
                              State
                            </th>
                          <th>
                              City
                            </th>
                           <th>
                              Zip 
                            </th>
                             <th>
                              Mobile No
                            </th>
                           <th>
                              Email Id
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tbodyorgbrList" runat="server" clientidmode="Static">
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
                <table class="table text-center table-bordered table-hover booking" id="employeeList" style="width:95%;">
                    <thead>
                        <tr>
                           <th>
                                Employee ID
                            </th>
                             <th>
                                 First Name
                            </th>
                            <th>
                               Last Name
                            </th>
                            <th>
                                 Adhaar No 
                            </th>
                            <th>
                                Gender
                            </th>
                            <th>
                                 Email ID
                            </th>
                          <th>
                                 Contact No
                            </th
                        </tr>
                    </thead>
                    <tbody id="tbodyemployeeList" runat="server" clientidmode="Static">
                    </tbody>
                </table>
               
            </div>
        </div>
                </div>

                  <div class="tab-pane " id="4">
              <br />
                <div class="box-body" style="padding: 20px">
                    <asp:HiddenField ID="hdnlogoname" runat="server" />
               <div id="headerdiv" runat="server"></div>
                </div>
                </div>
                 
                  <div class="tab-pane active" id="Div1">
              <br />

              </div>

               <div class="tab-pane active" id="Div2">
              <br />

              </div>
                 


			</div>
  </div>
</asp:Content>


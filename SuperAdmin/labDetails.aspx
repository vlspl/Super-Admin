<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="labDetails.aspx.cs" Inherits="SuperAdmin_labsdrilldown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4><asp:Label ID="lbllabName" runat="server" Text=""></asp:Label></h4>
                     </div>
                      <div class="col-sm-6 text-right">
                       </div>
                     
                  </div>
               </div>
            </nav><br />

<div class="container"></div>
    <asp:HiddenField ID="hdnlabId" runat="server" />
<div id="exTab2" class="container">	
<ul class="nav nav-tabs">
			<li class="active">
        <a  href="#1" data-toggle="tab">Lab`Details</a>
			</li>
			<li><a href="#2" data-toggle="tab">Lab Test List</a>
			</li>
			<li><a href="#3" data-toggle="tab">Signatury Details</a>
			</li>
            <li><a href="#4" data-toggle="tab">Logo</a>
			</li>
            <li><a href="#5" data-toggle="tab">Patient List</a>
			</li>
            <li><a href="#6" data-toggle="tab">Doctor List</a>
			</li>
           
		</ul>

			<div class="tab-content ">
			  <div class="tab-pane active" id="1">
              <br />
                <div class="box-body" style="padding: 20px">
                <div class="col-md-7">
                <h4>Lab Details : </h4><br />
                <asp:FormView ID="FormView_labDetails" runat="server" EmptyDataText = "No Lab Details Found">
                     <ItemTemplate>  
                    <table class="table table-bordered table-striped">  
                        <tr>  
                            <td>Lab ID</td>  
                            <td><%#Eval("sLabId")%></td>  
                        </tr>  
                        <tr>  
                            <td>Lab Code</td>  
                            <td><%#Eval("sLabCode")%></td>  
                        </tr>  
                        <tr>  
                            <td>Lab Name</td>  
                            <td><%#Eval("sLabName")%></td>  
                        </tr>  
                        <tr>  
                            <td>Owner Name</td>  
                            <td><%#Eval("sLabManager")%></td>  
                        </tr>  
                        
                        <tr>  
                            <td>Email ID</td>  
                            <td><%#Eval("sLabEmailId")%></td>  
                        </tr>  
                        <tr>  
                            <td>Contact No</td>  
                            <td><%#Eval("sLabContact")%></td>  
                        </tr>  
                        <tr>  
                            <td>Address</td>  
                            <td><%#Eval("sLabAddress")%></td>  
                        </tr>  
                        <tr>  
                            <td>Status</td>  
                            <td><%#Eval("sLabStatus")%></td>  
                        </tr> 
                    </table>  
                </ItemTemplate> 
                    </asp:FormView>
                </div>
                 <div class="col-md-5">
                  <h4>Package Details : </h4><br />
                 <asp:FormView ID="FormView_PackageDetails" runat="server" EmptyDataText = "No Package Details Found">
                     <ItemTemplate>  
                    <table class="table table-bordered table-striped">  
                        <tr>  
                            <td>Package ID</td>  
                            <td><%#Eval("pMasterId")%></td>  
                        </tr>  
                        <tr>  
                            <td>Package Name</td>  
                            <td><%#Eval("packageName")%></td>  
                        </tr>  
                        <tr>  
                            <td>Price</td>  
                            <td><%#Eval("price")%></td>  
                        </tr>  
                        <tr>  
                            <td>Days</td>  
                            <td><%#Eval("days")%></td>  
                        </tr>  
                          <tr>  
                            <td>Start Date</td>  
                            <td><%#Eval("assignDate")%></td>  
                        </tr>
                        <tr>  
                            <td>Expired Date</td>  
                            <td><%#Eval("expiredDate")%></td>  
                        </tr>  
                       
                    </table>  
                </ItemTemplate> 
                    </asp:FormView>
                 </div>
                    
                    
               </div>
				</div>
				<div class="tab-pane" id="2" style="width:94%; height:420px; overflow:auto;"><br />
          <asp:GridView ID="gridlabTestList" runat="server" DataKeyNames="sTestId" CssClass="table table-striped table-bordered"
            PageSize="10" AllowPaging="false" AutoGenerateColumns="False" 
            EmptyDataText = "Lab List Not Found" onpageindexchanging="gridlabTestList_PageIndexChanging"  
          >
            <Columns>
                <asp:TemplateField HeaderText="Test ID" >
                    <ItemTemplate>
                        <asp:Label ID="sTestId"  runat="server" Text='<%# Bind("sTestId") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="Test Code" >
                    <ItemTemplate>
                        <asp:Label ID="sTestCode"  runat="server" Text='<%# Bind("sTestCode") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="Test Name" >
                    <ItemTemplate>
                        <asp:Label ID="sTestName"  runat="server" Text='<%# Bind("sTestName") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                   <asp:TemplateField HeaderText="Section" >
                    <ItemTemplate>
                        <asp:Label ID="sSectionName"  runat="server" Text='<%# Bind("sSectionName") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                  
                  <%-- <asp:TemplateField HeaderText="Contact No" >
                    <ItemTemplate>
                        <asp:Label ID="sLabContact"  runat="server" Text='<%# Bind("sLabContact") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>--%>
                     <asp:TemplateField HeaderText="Profile" >
                    <ItemTemplate>
                        <asp:Label ID="sProfileName"  runat="server" Text='<%# Bind("sProfileName") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                     
               
               
            </Columns>
        </asp:GridView>
				</div>
        <div class="tab-pane" id="3">
              <br />
                <div class="box-body" style="padding: 20px">
                     <asp:GridView ID="gvImages" runat="server" AutoGenerateColumns="False" style="width:95%;"

        CellPadding="4"  DataKeyNames="DSId" EmptyDataText="No Signatury Found.."

         ForeColor="#333333" 
                     AllowPaging="false" >

        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />   
<Columns>
<asp:BoundField DataField="DSId" HeaderText="Sign ID" />
<asp:BoundField DataField="SignHolder" HeaderText="Sign Holder" />
<asp:BoundField DataField="Department" HeaderText="Department" />
<asp:ImageField HeaderText="Sign Image" DataImageUrlField="SignImage" ItemStyle-Width="200px" 
ControlStyle-Width="180" ControlStyle-Height = "100" />
<%--<asp:BoundField DataField="sLabId" HeaderText="Lab ID" />--%>
  <asp:BoundField DataField="SignStatus" HeaderText="Status " />

</Columns>
</asp:GridView>
                </div>
                </div>

                 <div class="tab-pane " id="4">
              <br />
                <div class="box-body" style="padding: 20px">
                    <asp:HiddenField ID="hdnlogoname" runat="server" />
               <div id="headerdiv" runat="server"></div>
                </div>
                </div>
                 <div class="tab-pane " id="5">
              <br />
              
                 <div class="tab-content">
            <!-- My Test List Start -->
            <div class="detailtable table-responsive">
                <table class="table text-center table-bordered table-hover booking" id="labPatient" style="width:95%;">
                    <thead>
                        <tr>
                           <th>
                                Sr No
                            </th>
                             <th>
                                Patient Name
                            </th>
                            <th>
                               Mobile
                            </th>
                            <th>
                                Gender
                            </th>
                            <th>
                               Address 
                            </th>
                           
                         
                        </tr>
                    </thead>
                    <tbody id="tbodyLabPatientList" runat="server" clientidmode="Static">
                    </tbody>
                </table>
               
            </div>
        </div>
         <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script type="text/javascript">

        $('#labPatient').datatable({
            pageSize: 10,
            sort: [true, true, false],
            filters: [false, true, true, true, true],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>
               
                </div>
                 <div class="tab-pane " id="6">
              <br />
                <div class="box-body" style="padding: 20px">
                       <div class="tab-content">
            <!-- My Test List Start -->
            <div class="detailtable table-responsive">
                <table class="table text-center table-bordered table-hover booking" id="labDoctor" style="width:95%;">
                    <thead>
                        <tr>
                           <th>
                                Sr No
                            </th>
                             <th>
                                Doctor Name
                            </th>
                            <th>
                               Mobile
                            </th>
                            <th>
                                Gender
                            </th>
                            <th>
                               Address 
                            </th>
                           
                         
                        </tr>
                    </thead>
                    <tbody id="tbodyLabDoctors" runat="server" clientidmode="Static">
                    </tbody>
                </table>
             
            </div>
        </div>
         <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script type="text/javascript">

        $('#labDoctor').datatable({
            pageSize: 10,
            sort: [true, true, false],
            filters: [false, true, true, true, true],
            filterText: 'Type to filter... ',
            onChange: function (old_page, new_page) {
                console.log('changed from ' + old_page + ' to ' + new_page);
            }
        });
    </script>
                </div>
                </div>
                 


			</div>
  </div>
</asp:Content>


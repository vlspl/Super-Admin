<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true" CodeFile="AddPatient.aspx.cs" Inherits="AddPatient" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<script>
     function isNumber(evt) {
         evt = (evt) ? evt : window.event;
         var charCode = (evt.which) ? evt.which : evt.keyCode;
         if (charCode > 31 && (charCode < 48 || charCode > 57)) {
             return false;
         }
         return true;
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <nav class="navbar navbar-expand-sm bg-light navbar-header"> 
                          <div class="container-fluid">
                            <div class="navbar-title ml-5">
                              <a href="#" class="navbar-brand">Add Patient</a>
                            </div>
                          <div class="mr-5">
                          <ul class="navbar-nav ml-auto"> 
                             
                              <li class="nav-item pt-1"> 
                               <asp:Button ID="btnviewHelth" class="btn btn-color" runat="server" 
                                      Text="View Health" onclick="btnviewHelth_Click" 
                 ></asp:Button>
                             </li>  
                              <li class="nav-item pt-1"> 
                               <asp:Button ID="btnviewpayment" class="btn btn-color" runat="server" Text="View Payment History" 
                  onclick="btnviewpayment_Click"></asp:Button>
                             </li> 
                              <li class="nav-item pt-1 mr-3"> 
                               <a href="PatientList.aspx" id="A3"  runat="server" class="btn btn-color"><span><i class="fa fa-eye mr-2" area-hidden="true"></i></span> View Patient List</a> 
                          
                              </li>                              
                          </ul> 
                        </div>
                      </div>
                      </nav>
      <div class="container">
        <div class="row">
         <asp:HiddenField ID="hiddenAction" Value="0" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenAppUserId" runat="server" ClientIDMode="Static" />
        <div class="col-md-12">
        <br />
             <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Enter Mobile Number *" MaxLength="10" onkeypress="return isNumber(event)"
                                    ID="txtMobile"  runat="server" ClientIDMode="Static"></asp:TextBox>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  
                                        ControlToValidate="txtMobile" ErrorMessage="Mobile No Should be 10 Digits" 
                                        ValidationExpression="[0-9]{10}" ForeColor="Red"></asp:RegularExpressionValidator> 
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Enter Full Name *" ID="txtFullName" 
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="regName" runat="server" 
       ControlToValidate="txtFullName" ForeColor="Red"
       ValidationExpression="^[a-zA-Z'.\s]{1,50}"
       Text="Enter a valid Name" /> 
                                <label id="lblFullName" class="form-error">
                                </label>
                            </div>
                        </div>
                         <div class="col-md-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Enter Email Id *" ID="txtEmailId" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailId"
    ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
    Display = "Dynamic" ErrorMessage = "Invalid email address"/>
                                <label id="lblEmailId" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:DropDownList class="form-control" ID="selGender" runat="server" ClientIDMode="Static">
                                    <asp:ListItem Value="select" Selected="True">Select Gender *</asp:ListItem>
                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                </asp:DropDownList>
                                <label id="lblGender" class="form-error">
                                </label>
                            </div>
                        </div>
                         <div class="col-md-4">
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text fa-color" style="color: #91c740;"><i class="fa fa-calendar fa-fa-color"
                                        aria-hidden="true"></i></span>
                                </div>
                                <asp:TextBox ID="txtBirthDate" placeholder="Select Birth date *" runat="server" class="form-control"
                                    onchange="AgeCalulation()" ClientIDMode="Static"></asp:TextBox>
                                <cc1:CalendarExtender ID="Calendar1" CssClass="cal_Theme1" PopupButtonID="txtBirthDate"
                                    runat="server" TargetControlID="txtBirthDate" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </div>
                            <label id="lblBirthDate" class="form-error">
                            </label>
                          
                            <%--<asp:TextBox class="form-control" placeholder="State" Text="Maharashtra" ID="txtage"
                                    runat="server" ClientIDMode="Static" Visible="False"></asp:TextBox>--%>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                              <label id="lblage">
                             
                            </label>
                                <asp:TextBox class="form-control" placeholder="Enter Age in Year" onkeyup="GetBirthDate()" ReadOnly="true" style="display:none;"
                                    ID="txtyear" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                   
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="State" Text="Maharashtra" ID="txtState"
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                              
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Pincode" onkeypress="return isNumber(event)"
                                    ID="txtPincode" MaxLength="6" runat="server" ClientIDMode="Static"></asp:TextBox>
                              
                            </div>
                        </div>
                         <div class="col-md-4">
                            <div class="form-group" id="">
                                <asp:TextBox class="form-control" Style="display: none" placeholder="Country" Text="India"
                                    ID="txtCountry" runat="server" ClientIDMode="Static"></asp:TextBox>
                               
                                <asp:TextBox class="form-control" placeholder="City" ID="txtCity" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                   
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       
                        <div class="col-md-4">
                            <div class="form-group" id="address">
                                <asp:TextBox class="form-control" placeholder="Address" ID="txtAddress" TextMode="MultiLine"
                                    style="resize:none; height:100px;" runat="server" ClientIDMode="Static"></asp:TextBox>
                                  
                            </div>
                        </div>
                        
                    </div>
                    <hr />
                    <div class="row">
                       <div class="col-md-4"></div>
                          <div class="col=md-6">
                              <asp:Button ID="btnAdd" class="btn btn-submit" runat="server" Text="Submit" OnClientClick="javascript:return addPatientValidate()"
                                ClientIDMode="Static" onclick="btnAdd_Click1" />
                                 <asp:Button ID="btnupdate" class="btn btn-info" runat="server" Text="Update" 
                                ClientIDMode="Static" onclick="btnupdate_Click"  />
                        </div>
                    </div>
        </div>
        </div>
        </div>


</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true" CodeFile="AddDoctor.aspx.cs" Inherits="AddDoctor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <nav class="navbar navbar-expand-sm bg-light navbar-header"> 
                          <div class="container-fluid">
                            <div class="navbar-title ml-5">
                              <a href="#" class="navbar-brand">Add Doctor</a>
                            </div>
                          <div class="mr-5">
                          <ul class="navbar-nav ml-auto"> 
                             
                              <li class="nav-item pt-1"> 
                              
                             </li>  
                              <li class="nav-item pt-1"> 
                             
                             </li> 
                              <li class="nav-item pt-1 mr-3"> 
                               <a href="DoctorList.aspx" id="A3"  runat="server" class="btn btn-color"><span><i class="fa fa-eye mr-2" area-hidden="true"></i></span> View Doctor List</a> 
                          
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
                                <asp:TextBox class="form-control" placeholder="Enter Mobile Number *" ID="txtMobile"
                                    onkeypress="return isNumber(event)" MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblMobile" class="form-error">
                                </label>
                            </div>
                        </div>
                        <div class="col-md-4">
                          <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Enter Full Name *" ID="txtFullName"
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="regName" runat="server" ControlToValidate="txtFullName" ForeColor="Red" ValidationExpression="^[a-zA-Z'.\s]{1,50}"  Text="Enter a valid Name" />
                                <label id="lblFullName" class="form-error">
                                </label>
                            </div>
                        </div>
                         <div class="col-md-4">
                          <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Enter Email Id *" ID="txtEmailId" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmailId"
                                            ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                         Display = "Dynamic" ErrorMessage = "Invalid email Address"/>
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
                                    <%-- <asp:ListItem value="Other" >Other</asp:ListItem>--%>
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
                                <asp:TextBox ID="txtBirthDate" class="form-control" placeholder="Birth date (dd/mm/yyyy)"
                                    runat="server" onchange="AgeCalulation()" ClientIDMode="Static"></asp:TextBox>
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
                                <asp:TextBox class="form-control" placeholder="City" Text="Pune" ID="txtCity" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                <label id="lblCity" class="form-error">
                                </label>
                            </div>
                        </div>
                        <div class="col-md-4">
                              <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Degree" ID="txtDegree" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                           ControlToValidate="txtDegree" ForeColor="Red"  ValidationExpression="^[a-zA-Z'.\s]{1,50}"  Text="Enter a valid Degree" />
                                <label id="lblDegree" class="form-error">
                                </label>
                            </div>
                        </div>
                         <div class="col-md-4">
                             <div class="form-group">
                                <asp:DropDownList class="form-control" ID="txtSpecialization" runat="server" ClientIDMode="Static">
                                    <asp:ListItem Value="select" Selected="True">Select Specialization</asp:ListItem>
                                    <asp:ListItem Value="Cardiologist">Cardiologist</asp:ListItem>
                                    <asp:ListItem Value="Gastroenterologist">Gastroenterologist</asp:ListItem>
                                    <asp:ListItem Value="Gynecologist">Gynecologist</asp:ListItem>
                                    <asp:ListItem Value="Nephrologist">Nephrologist</asp:ListItem>
                                    <asp:ListItem Value="Neurologist">Neurologist</asp:ListItem>
                                    <asp:ListItem Value="Ophthalmologist">Ophthalmologist</asp:ListItem>
                                    <asp:ListItem Value="Orthopedic">Orthopedic</asp:ListItem>
                                    <asp:ListItem Value="Urologist">Urologist</asp:ListItem>
                                    <asp:ListItem Value="Physician">Physician</asp:ListItem>
                                    <asp:ListItem Value="Pulmonologist">Pulmonologist</asp:ListItem>
                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                </asp:DropDownList>
                                <label id="lblSpecialization" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       
                        <div class="col-md-4">
                            <div class="form-group">
                              
                                     <asp:DropDownList ID="txtState" class="form-control select2 select2-hidden-accessible"
                                        runat="server">
                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                        <asp:ListItem Value="Andaman and Nicobar Islands">	Andaman and Nicobar Islands	</asp:ListItem>
                                        <asp:ListItem Value="Andhra Pradesh">	Andhra Pradesh	</asp:ListItem>
                                        <asp:ListItem Value="Arunachal Pradesh">	Arunachal Pradesh	</asp:ListItem>
                                        <asp:ListItem Value="Assam">	Assam	</asp:ListItem>
                                        <asp:ListItem Value="Bihar">	Bihar	</asp:ListItem>
                                        <asp:ListItem Value="Chandigarh">	Chandigarh	</asp:ListItem>
                                        <asp:ListItem Value="Chattisgarh">	Chattisgarh	</asp:ListItem>
                                        <asp:ListItem Value="Dadra & Nagar Haveli and Daman & Diu">	Dadra & Nagar Haveli and Daman & Diu	</asp:ListItem>
                                        <asp:ListItem Value="Delhi">	Delhi	</asp:ListItem>
                                        <asp:ListItem Value="Goa">	Goa	</asp:ListItem>
                                        <asp:ListItem Value="Gujarat">	Gujarat	</asp:ListItem>
                                        <asp:ListItem Value="Haryana">	Haryana	</asp:ListItem>
                                        <asp:ListItem Value="Himachal Pradesh">	Himachal Pradesh	</asp:ListItem>
                                        <asp:ListItem Value="Jammu & Kashmir">	Jammu & Kashmir	</asp:ListItem>
                                        <asp:ListItem Value="Jharkhand">	Jharkhand	</asp:ListItem>
                                        <asp:ListItem Value="Karnataka">	Karnataka	</asp:ListItem>
                                        <asp:ListItem Value="Kerala">	Kerala	</asp:ListItem>
                                        <asp:ListItem Value="Ladakh">	Ladakh	</asp:ListItem>
                                        <asp:ListItem Value="Lakshadweep">	Lakshadweep	</asp:ListItem>
                                        <asp:ListItem Value="MadhyaPradesh">	MadhyaPradesh	</asp:ListItem>
                                        <asp:ListItem Value="Maharashtra">	Maharashtra	</asp:ListItem>
                                        <asp:ListItem Value="Manipur">	Manipur	</asp:ListItem>
                                        <asp:ListItem Value="Meghalaya">	Meghalaya	</asp:ListItem>
                                        <asp:ListItem Value="Mizoram">	Mizoram	</asp:ListItem>
                                        <asp:ListItem Value="Nagaland">	Nagaland	</asp:ListItem>
                                        <asp:ListItem Value="Odisha">	Odisha	</asp:ListItem>
                                        <asp:ListItem Value="Puducherry">	Puducherry	</asp:ListItem>
                                        <asp:ListItem Value="Punjab">	Punjab	</asp:ListItem>
                                        <asp:ListItem Value="Rajasthan">	Rajasthan	</asp:ListItem>
                                        <asp:ListItem Value="Sikkim">	Sikkim	</asp:ListItem>
                                        <asp:ListItem Value="Tamil Nadu">	Tamil Nadu	</asp:ListItem>
                                        <asp:ListItem Value="Telangana">	Telangana	</asp:ListItem>
                                        <asp:ListItem Value="Tripura">	Tripura	</asp:ListItem>
                                        <asp:ListItem Value="Uttar Pradesh">	Uttar Pradesh	</asp:ListItem>
                                        <asp:ListItem Value="Uttrakhand">	Uttrakhand	</asp:ListItem>
                                        <asp:ListItem Value="West Bengal">	West Bengal	</asp:ListItem>
                                    </asp:DropDownList>
                                <label id="lblState" class="form-error">
                                </label>
                            </div>
                        </div>
                        <div class="col-md-4">
                         <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Pincode" onkeypress="return isNumber(event)"
                                    ID="txtPincode" MaxLength="6" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblPincode" class="form-error">
                                </label>
                            </div>
                        </div>
                         <div class="col-md-4">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Clinic" ID="txtClinic" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                <label id="lblClinic" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                     <div class="row">
                       <div class="col-md-4">
                        <div class="form-group" id="address">
                                <asp:TextBox class="form-control" Style="display: none" placeholder="Country" Text="India"
                                    ID="txtCountry" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblCountry" class="form-error hide">
                                </label>
                                <asp:TextBox class="form-control" placeholder="Address" ID="txtAddress" TextMode="MultiLine"
                                    Rows="2" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblAddress" class="form-error">
                                </label>
                            </div>
                       </div>
                         <div class="col-md-4"></div>
                           <div class="col-md-4"></div>
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


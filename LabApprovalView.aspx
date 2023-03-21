<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="LabApprovalView.aspx.cs" Inherits="SuperAdmin_LabApprovalView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <nav class="primary-col-back subheader">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-sm-6">
                     <h4>View Lab Approval</h4>
                  </div>   
                    <div class="col-sm-6 text-right" >
                       <a  href="PedingLabApproval.aspx" class="btn btn-danger" style="margin-right:60px;"><i class="fa fa-arrow-left"></i>Back</a>
                       </div>
               </div>
            </div>
         </nav>
    <div class="container-fluid">
        <asp:HiddenField ID="hdnorgid" runat="server" />
        <asp:HiddenField ID="hdnlabcode" runat="server" />
        <div class="labregister">
       
            <div class="header-wrap">
             <span style="color:Green;"><i class="fas fa-caret-right"></i> Lab Details : </span>
               
          
                <div class="row" style="margin-top:10px;">
                    <div class="col-md-6">
                        <div class="form-group">
                           <asp:TextBox ID="txtlabName" placeholder="Lab Name" runat="server"></asp:TextBox>
                       
                        </div>
                    </div>
                    <div class="col-md-6">
                       <div class="form-group">
                         <asp:TextBox ID="txtLabOwner" placeholder="Lab Owner" runat="server"></asp:TextBox>
                          
                       
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                         <div class="form-group">
                           <asp:TextBox ID="txtemailId" placeholder="Email ID" runat="server"></asp:TextBox>
                           
                        </div>
                    </div>
                    <div class="col-md-6">
                       <div class="form-group">
                            <asp:TextBox ID="txtcontactNo" placeholder="Contact No" runat="server"></asp:TextBox>
                          
                          
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                     <div class="form-group">
                            <asp:TextBox ID="txtCountry" placeholder="Country" runat="server"></asp:TextBox>
                          
                        </div>
                   <div class="form-group">
                         <asp:TextBox ID="txtState" placeholder="State" runat="server"></asp:TextBox>
                           
                        </div>
                        <div class="form-group">
                            
                             <asp:TextBox ID="txtcity" placeholder="City" MaxLength="10" runat="server"></asp:TextBox>
                        </div>
                      
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:TextBox ID="txtLabAddress" TextMode="MultiLine" placeholder="Lab Address" Rows="4" style="resize:none;"
                                runat="server"></asp:TextBox>
                         
                        </div>
                          <asp:DropDownList ID="drpLabStatus" runat="server">
                                    <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                    <asp:ListItem  Value="Inactive">Inactive</asp:ListItem>
                                </asp:DropDownList>
                    </div>
                     </div>
                      <div class="row">
                      <div class="col-md-6">
                        <div class="form-group">
                           <asp:TextBox ID="txtpinCode" placeholder="Pin Code" MaxLength="10" runat="server"></asp:TextBox>
                          
                        </div>
                      
                      </div>
                        <div class="col-md-6">
                           <div class="form-group">
                        <asp:TextBox ID="txtlatLong" placeholder="Latitude,Longitude(Ex.18.5533408,73.8009082)" runat="server"></asp:TextBox>
                        </div>
                      </div>
                      </div>
                    
               
               <br />
                <span style="color:Green;"><i class="fas fa-caret-right"></i> Requested Details : </span>
                  <div class="row" style="margin-top:10px;">
                      <div class="col-md-6">
                        <div class="form-group">
                           <asp:TextBox ID="txtorgName" placeholder="Organization Name"  runat="server"></asp:TextBox>
                          
                        </div>
                       <div class="form-group">
                            <asp:TextBox ID="txtrequestedDate" placeholder="Requested Date" runat="server"></asp:TextBox>
                          
                        </div>
                 
                        <div class="form-group">
                        <asp:TextBox ID="txtrequestedUser" placeholder="Requested User"  runat="server"></asp:TextBox>
                        </div>
                      </div>
                        <div class="col-md-6">
                         <div class="form-group">
                            <asp:TextBox ID="txtremark" TextMode="MultiLine" placeholder="Requested Remark" Rows="6" style="resize:none;"
                                runat="server"></asp:TextBox>
                         
                        </div>
                            
                      </div>
                      </div>
                    
             
                 <br />
                     <span style="color:Green;"><i class="fas fa-caret-right"></i> Approval Details : </span>
                 <div class="row" style="margin-top:10px;">
                      <div class="col-md-6">
                         <div class="form-group">
                             <asp:TextBox ID="txtapprovedBy" placeholder="Approval By" runat="server"></asp:TextBox>
                           
                        </div>
                       <div class="form-group">
                             <asp:TextBox ID="txtApporvedDate" placeholder="Approval Date" runat="server"></asp:TextBox>
                           
                        </div>
                         <div class="form-group">
                            <asp:TextBox ID="txtapprovalStatus" ReadOnly="true" placeholder="Approval Date" runat="server"></asp:TextBox>
                          
                        </div>
                      </div>
                        <div class="col-md-6">
                             <div class="form-group">
                            <asp:TextBox ID="txtapprovalRemark" TextMode="MultiLine" placeholder="Approval Remark" Rows="6" style="resize:none;"
                                runat="server"></asp:TextBox>
                         
                        </div>
                            
                      </div>
                      </div>
                 

                   <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-3">
                               
                            </div>
                            <div class="col-md-9">
                               
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="text-right">
                            <asp:Button class="lab-btn-primary" ID="RegisterLab" ValidationGroup="labregister"
                                runat="server" Text="Approval & Submit" onclick="RegisterLab_Click"  />
                        </div>
                    </div>
                </div>
                
            </div>
        </div>
    </div>
</asp:Content>


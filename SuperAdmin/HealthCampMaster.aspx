<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="HealthCampMaster.aspx.cs" Inherits="SuperAdmin_HealthCampMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
  <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Health Camp Master</h4>
                     </div>
                      <div class="col-sm-6 text-right">
                        <a href="ViewHealthCampDetails.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Details</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:25px" >
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>
                        Organization Name <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="ddlName" DataTextField="Name" DataValueField="ID" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True" 
                        onselectedindexchanged="ddlName_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
               <div class="col-md-6">
                <div class="form-group">
                    <label>
                        Branch Name <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="ddlBranchName" DataTextField="BranchName" 
                        DataValueField="ID" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True" 
                        onselectedindexchanged="ddlBranchName_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
           
        </div>
         <div class="row">
           <div class="col-md-6">
                <div class="form-group">
                    <label>
                        Health Camp Name <span style="color: Red">*</span></label>
                    <asp:TextBox ID="txthealthcampName" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
           <div class="col-md-6">
                <div class="form-group">
                    <label>
                       Owner Name <span style="color: Red">*</span></label>
                    <asp:TextBox ID="txtownerName" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
           
         </div>
        <div class="row">
           <div class="col-md-6">
                <div class="form-group">
                 <label>
                        Technician Type<span style="color: Red">*</span></label>
                       <asp:DropDownList ID="drpTechType" DataTextField="technicianType" 
                        DataValueField="sLabId" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True" 
                        onselectedindexchanged="drpTechType_SelectedIndexChanged">
                       <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>Howzu</asp:ListItem>
                        <asp:ListItem>Organization</asp:ListItem>
                         </asp:DropDownList>

              </div>
              
            </div>
            <div class="col-md-6">   
                <div class="form-group">
                    <label>
                        Technician<span style="color: Red">*</span></label>
                       <asp:DropDownList ID="ddlEmpName" DataTextField="sFullName" DataValueField="sAppUserId" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True"> </asp:DropDownList>
                    <asp:TextBox ID="txtTechnicianName" runat="server" Visible="false" class="form-control select2 select2-hidden-accessible"></asp:TextBox>
                     </div>

            </div>
            
             </div>
            <div class="row">
             <div class="col-md-6">   
                 <div class="form-group">
                                     <label>
                        Lab Name<span style="color: Red">*</span></label>
                       <asp:DropDownList ID="ddlLabName" DataTextField="sLabName" DataValueField="sLabId" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True">
                         </asp:DropDownList>

                  <%--  <label>
                      Department<span style="color: Red">*</span></label>
                       <asp:DropDownList ID="ddlDeptName" DataTextField="deptName" DataValueField="deptId" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True"> </asp:DropDownList>--%>
                </div>
            </div>
               <div class="col-md-6"> 
              <div class="form-group">
                    <label>
                       Other Details </label>
                    <asp:TextBox ID="txtotherDetails" TextMode="MultiLine" style="height:100px; resize:none;" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                </div>
                   <div class="col-md-6"> 
                   <div class="form-group text-right" style="margin-top:50px;">
                    <asp:Button ID="BtnSave" runat="server" type="submit" class="fa fa-save btn btn-lg btn-primary"
                        Text="Save" onclick="BtnSave_Click1" />
                   </div>
                   </div>
            </div>
            <div class="col-lg-6 col-md-6">
                  <div class="pad">
                        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
             
               
            </div>
      
 </asp:Content>


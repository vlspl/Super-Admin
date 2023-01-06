<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="packageAssign.aspx.cs" Inherits="SuperAdmin_packageAssign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

  
 <%-- <style>
  .lab-btn-white 
  {
          padding: 2px 9px;
  }
  </style>--%>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      
    
     <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-8" >
                      <h4>  <sapn>Package Assign </sapn>
                       </div>
                      <div class="col-sm-4 text-right">
                       
                          <a href="ViewAssignPackages.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Assign Packages</a>
                     </div>
                     
                  </div>
               </div>
            </nav>
    <div class="container-fluid">
        <div class="labregister">
            <div class="header-wrap">
           <span style="color:Green;"><i class="fas fa-caret-right"></i> Package Master : </span>
                <div class="row" style="margin-top:10px;">
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:DropDownList ID="drppackageName" CssClass="form-control" runat="server" 
                                AutoPostBack="True" 
                                onselectedindexchanged="drppackageName_SelectedIndexChanged">
                            </asp:DropDownList>
                          
                        </div>
                         <div class="form-group">
                            <asp:TextBox ID="txtnoofDays" placeholder="No of Days" runat="server"></asp:TextBox>
                        
                        </div>
                         <div class="form-group">
                            <asp:TextBox ID="txtprice" placeholder="Price" MaxLength="10" runat="server"></asp:TextBox>
                          
                        </div>
                          <div class="form-group">
                            <asp:DropDownList ID="drpstatus" runat="server">
                            <asp:ListItem>-Status-</asp:ListItem>
                            <asp:ListItem>Active</asp:ListItem>
                            <asp:ListItem>Deactive</asp:ListItem>
                            </asp:DropDownList>
                            
                        </div>
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtdescription" TextMode="MultiLine" placeholder="Description" Rows="6"
                                runat="server"></asp:TextBox>
                    </div>
                </div>
                <br />
                 <span style="color:Green;"><i class="fas fa-caret-right"></i> Package Assign : </span>
              <div class="row" style="margin-top:10px;">
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:DropDownList ID="drplab" CssClass="form-control" runat="server" 
                               >
                            </asp:DropDownList>
                          
                        </div>
                         <div class="form-group">
                            <asp:TextBox ID="txtstartDate" placeholder=" Start Date" runat="server"></asp:TextBox>
                          
                             <asp:CalendarExtender ID="txtstartDate_CalendarExtender" runat="server" 
                                 DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                                 TargetControlID="txtstartDate" TodaysDateFormat="dd/MM/yyyy">
                             </asp:CalendarExtender>
                          
                        </div>
                        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        </asp:ToolkitScriptManager>
                         <div class="form-group">
                            <asp:TextBox ID="txtexpireDate" placeholder="Expired Date"  runat="server"></asp:TextBox>
                          
                             <asp:CalendarExtender ID="txtexpireDate_CalendarExtender" runat="server" 
                                 DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                                 TargetControlID="txtexpireDate" TodaysDateFormat="dd/MM/yyyy">
                             </asp:CalendarExtender>
                          
                        </div>
                         
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtremark" TextMode="MultiLine" placeholder="Remark" Rows="6"
                                runat="server"></asp:TextBox>
                    </div>
                </div>
 		 <div class="row">
				  <div class="col-md-6"></div>
                     <div class="col-md-6">
                        <div class="text-right">
                            <asp:Button class="lab-btn-primary" ID="RegisterLab" ValidationGroup="labregister"
                                runat="server" Text="Package Assign" OnClick="RegisterLab_Click" />
                        </div>
                    </div>
			</div>
               
             
            </div>
        </div>
    </div>
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

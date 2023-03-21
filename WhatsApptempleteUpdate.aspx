<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="WhatsApptempleteUpdate.aspx.cs" Inherits="SuperAdmin_WhatsApptempleteUpdate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    

    <div class="header-wrap"> 
       <sapn class="primary-col">View WhatsApp Master details
 </sapn>
        <asp:Label ID="LabCode" placeholder="LabID" ReadOnly="true" runat="server"></asp:Label>
    </div>

    <div class="container-fluid">
        <div class="labregister">
            <div class="header-wrap">
                <div class="row">


                    <div class="col-md-6">
                  <div class="form-group">
                   <asp:Label  class="primary-col" ID="Label1" runat="server" Text="Template Name"></asp:Label>
                  <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtTempName"
                        required placeholder=" Template Name"></asp:TextBox>
              
                      </div>
                          </div>

                          <div class="col-lg-6 col-md-6">
                  <div class="form-group">
                  <%-- <label>
                       Message text<span style="color: Red">*</span></label>--%>
                       <asp:Label  class="primary-col" ID="Label2" runat="server" Text=" Message text"></asp:Label>
                   <asp:TextBox runat="server" class="form-control" ID="txtMsgText"
                        required placeholder="Message Text"  MaxLength="100" TextMode="MultiLine" Width="500px"  /></asp:TextBox>
                </div>
                 </div>

        <div class="col-lg-6 col-md-6">
                <div class="form-group">
               
                     <asp:Label  class="primary-col" ID="Label4" runat="server" Text=" No Of Parameter"></asp:Label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtParameter"
                        required placeholder="No Of Parameter"></asp:TextBox>
                </div>
                </div>
                     <div class="col-lg-6 col-md-6">
                <div class="form-group">
               
                     <asp:Label  class="primary-col" ID="Label8" runat="server" Text=" Parameter List "></asp:Label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtparamList"
                        required placeholder="Parameter List"></asp:TextBox>
                </div>
                </div>

                     <div class="col-lg-6 col-md-6">
                  <div class="form-group">
                    <%--<label>
                      Request By<span style="color: Red">*</span></label>--%>
                       <asp:Label  class="primary-col" ID="Label5" runat="server" Text="Request By"></asp:Label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtRequestBy"
                        required placeholder="Request By"></asp:TextBox>
                </div>
                 </div>

             <div class="col-lg-6 col-md-6">
                <div class="form-group">
                  <asp:Label  class="primary-col" ID="Label6" runat="server" Text="   Approval Status"></asp:Label>
                     <asp:DropDownList ID="drpApprovalStatus" DataTextField="status" DataValueField="status" class="form-control select2 select2-hidden-accessible"
                      runat="server" Style="width: 100%" AutoPostBack="True">
                         <asp:ListItem>Select</asp:ListItem>
                          <asp:ListItem>Pending</asp:ListItem>
                          <asp:ListItem>Approved</asp:ListItem>
                         <asp:ListItem>Rejected</asp:ListItem>
                    </asp:DropDownList>
                    <%-- <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtApprovalStatus"
                        required placeholder="Approval Status"></asp:TextBox>--%>
              <%--<asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:MedicalDbBackupConnectionString5 %>' SelectCommand="SELECT [status] FROM [tbl_WhatsappMsgMaster]"></asp:SqlDataSource>--%>
                </div>
                </div>
                    

                  <div class="col-lg-6 col-md-6">
                  <div class="form-group">
                   <%-- <label>
                    Approve By<span style="color: Red">*</span></label>--%>
                       <asp:Label  class="primary-col" ID="Label7" runat="server" Text=" Approve By"></asp:Label>
                   <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtApprovalBy"
                        required placeholder="Approve By"></asp:TextBox>
                </div>


                       </div>   
                       
                    <div class="col-sm-6">
                    <div class="form-group">
                    <asp:Label  class="primary-col" ID="Label3"  runat="server" Text="Approve Date"></asp:Label>
                        
                     <asp:TextBox ID="txtApproveDate" runat="server" cssClass="form-control" ></asp:TextBox>
                     <asp:CalendarExtender ID="txtApproveDate_CalendarExtender" runat="server" 
                            DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                            TargetControlID="txtApproveDate" TodaysDateFormat="dd/MM/yyyy">
                        </asp:CalendarExtender>
                  <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
                    </div>
                           </div>
                   
              
        
 </div>
            
              
            
            
                <div class="row">
                    <div class="text-right">
                  <div class="form-group">
        <asp:Button ID="whatsAppTemplete"  class="lab-btn-primary"  ValidationGroup="labregister" runat="server" Text="Update" OnClick="whatsAppTemplete_Click"  />
    </div>
               </div>
                </div>
            </div>
        </div>
    </div>
        </asp:Content>


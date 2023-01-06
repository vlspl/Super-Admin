<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="WhatsAppTempleteCreation.aspx.cs" Inherits="SuperAdmin_WhatsAppTempleteCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
     <div class="wrapper">
        <div id="content">
            <nav class="primary-col-back subheader">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-sm-6">
                     <h4>WhatsApp Templete Creation</h4>
                  </div>                 
               </div>
            </div>
              
                
         </nav>
           <%-- </div>--%>

            <br />
             <div class="box-body" style="padding:25px">
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                       Template Name<span style="color: Red">*</span></label>
                    <%--<label>
                        Templete Name<span style="color: Red">*</span></label>--%>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtTempName"
                        required placeholder=" Template Name"></asp:TextBox>
                   
                </div>
                </div>
                
             <div class="col-lg-6 col-md-6">
                  <div class="form-group">
                   <label>
                       Message text<span style="color: Red">*</span></label>
                   <asp:TextBox runat="server" class="form-control" ID="txtMsgText"
                        required placeholder="Message Text"  MaxLength="100" TextMode="MultiLine" Width="500px"  /></asp:TextBox>
                </div>
                 </div>

             <div class="col-lg-6 col-md-6">
                  <div class="form-group">
                   <label>
                       Message type<span style="color: Red">*</span></label>
                         <asp:DropDownList ID="drpTrans" DataTextField="status" DataValueField="status" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>trans</asp:ListItem>
							 <asp:ListItem>prom</asp:ListItem>
                    </asp:DropDownList>
                  
                </div>
                 </div>

             <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                      No Of  Parameter<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtParameter"
                        required placeholder="Parameter"></asp:TextBox>
                </div>
                </div>


             <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                       Parameter List<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtParaneterList"
                        required placeholder="Parameter"></asp:TextBox>
                </div>
                </div>
         
                
             <div class="col-lg-6 col-md-6">
                  <div class="form-group">
                    <label>
                      Request By<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtRequestBy"
                        required placeholder="Request By"></asp:TextBox>
                </div>
                 </div>

             <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                       Approval Status <span style="color: Red">*</span></label>
                     <asp:DropDownList ID="drpApprovalStatus" DataTextField="status" DataValueField="status" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True">
                        <asp:ListItem>Select</asp:ListItem>
                          <asp:ListItem>Pending</asp:ListItem>
                          <asp:ListItem>Approved</asp:ListItem>
                         <asp:ListItem>Rejected</asp:ListItem>
                    </asp:DropDownList>

<%--                    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:MedicalDbBackupConnectionString5 %>' SelectCommand="SELECT  Distinct case when status=1 then 'Approved' else'Rejected' End as[status]  FROM  [tbl_WhatsappMsgMaster]"></asp:SqlDataSource>--%>
                </div>
                </div>


             <div class="col-lg-6 col-md-6">
                  <div class="form-group">
                    <label>
                    Approve By<span style="color: Red">*</span></label>
                   <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtApproveBy"
                        required placeholder="Approve By"></asp:TextBox>
                </div>
        </div>


              <div class="col-lg-6 col-md-6">
                  <div class="form-group">
                      <label>
                Approval Date <span style="color: Red">*</span></label>
                       <asp:TextBox ID="txtApproveDate" runat="server" cssClass="form-control"  Width="350px"  ></asp:TextBox>
                     <asp:CalendarExtender ID="txtApproveDate_CalendarExtender" runat="server" 
                            DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                            TargetControlID="txtApproveDate" TodaysDateFormat="dd/MM/yyyy">
                        </asp:CalendarExtender>
                       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
                    </div>
            </div>
                 
             <div class="col-lg-6 col-md-6">
              <asp:Label ID="lblError" runat="server" Text="" style="color: Red" Font-Bold></asp:Label>
                 </div>
 
                  <div class="row">
                    <div class="text-right">
                  <div class="form-group">
        <asp:Button ID="BtnSave"  class="lab-btn-primary"  runat="server" Text="Save" OnClick="BtnSave_Click"  />
    </div>
               </div>
                </div>
                 
       

                   <div class="col-lg-12">
     <asp:GridView ID="grdviewWhatAppMaster" runat="server" style="width:100%"  AutoGenerateColumns="False" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdviewWhatAppMaster_PageIndexChanged" OnSelectedIndexChanged="Page_Load"  >

            <Columns>
  <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="30">
                                            <ItemTemplate>
                                                 <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>


       
          <asp:BoundField DataField="msgName" HeaderText="Template Name" />
         <%-- <asp:BoundField DataField="body" HeaderText="Message Type" />  --%>
          <asp:BoundField DataField="body" HeaderText="Message Text" />  
          <asp:BoundField DataField="noOfParameters" HeaderText="No Of Paramerter" />
          <asp:BoundField DataField="status" HeaderText="Status" />  
          <asp:BoundField DataField="requestBy" HeaderText="Request By" />
          <asp:BoundField DataField="approveBy" HeaderText="Approve By" />
        
                 <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink runat="server"   NavigateUrl='<%# string.Format("~/SuperAdmin/WhatsApptempleteUpdate.aspx?whatsappMasterId={0}",
                                                         HttpUtility.UrlEncode(Eval("whatsappMasterId").ToString())) %>'>
                                                     <i class="material-icons">View</i></asp:HyperLink>
                                         
                                            </ItemTemplate>
                     </asp:TemplateField>
                </Columns>
         </asp:GridView>

         </div>
    </div>
         </div>
         </div>
         </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


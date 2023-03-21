<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="DemoList.aspx.cs" Inherits="SuperAdmin_DemoList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <div class="wrapper">
        <div id="content">
            <nav class="primary-col-back subheader">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-sm-6">
                     <h4>Demo List</h4>
                  </div> 
                    <div class="col-sm-6 text-right" >
                       <a  href="Dash.aspx" class="btn btn-danger" style="margin-right:60px;"><i class="fa fa-arrow-left"></i>Back</a>
                       </div>
               </div>
            </div>
         </nav>
            <br />
            <div class="col-lg-12">
             <div class="col-lg-2 col-md-2">
                 <div class="form-group">
                    <label> 
                      Full Name<span style="color: Red"></span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtFullName"
                        ></asp:TextBox>
                </div>

                </div> 
              <div class="col-lg-2 col-md-2">
                 <div class="form-group">
                    <label> 
                      Location<span style="color: Red"></span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtLocation"
                      ></asp:TextBox>
                </div>

                </div>
              <div class="col-lg-2 col-md-2">
                 <div class="form-group">
                    <label> 
                      Phone<span style="color: Red"></span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtPhone"
                     ></asp:TextBox>
                </div>

                </div>
              <div class="col-lg-2 col-md-2">
                 <div class="form-group">
                    <label> 
                      Status<span style="color: Red"></span></label>
                     <asp:DropDownList ID="drpStatus" DataTextField="status" DataValueField="status" class="form-control select2 select2-hidden-accessible"
                         runat="server" Style="width: 100%" AutoPostBack="True" >
                     </asp:DropDownList>
                     
                 </div>

                </div>
             <div class="col-lg-3 col-md-3">
                 <div class="form-group">
                    <label> 
                      Demo Category<span style="color: Red"></span></label>
                     <asp:DropDownList ID="drpDemoCategory" DataTextField="bookDemoCatgory" DataValueField="bookDemoCatgory" class="form-control select2 select2-hidden-accessible"
                         runat="server" Style="width: 100%" AutoPostBack="True" OnSelectedIndexChanged="drpDemoCategory_SelectedIndexChanged" >
                         
                     </asp:DropDownList>
                    
                     
                 </div>

                </div>
            
</div>
            <br />
         <div class="col-lg-12">
                 <div class="col-lg-2 col-md-2">
                 <div class="form-group">
                    <label> 
                      From Date<span style="color: Red"></span></label>

                    
                     <asp:TextBox ID="txtFromDate" runat="server" cssClass="form-control" ></asp:TextBox>
                     <asp:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" 
                            DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                            TargetControlID="txtFromDate" TodaysDateFormat="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    
                           </div>
                </div>



              <div class="col-lg-2 col-md-2">
                 <div class="form-group">
                    <label> 
                      To Date<span style="color: Red"></span></label>

                    
                     <asp:TextBox ID="txtToDate" runat="server" cssClass="form-control" ></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                            DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                            TargetControlID="txtToDate" TodaysDateFormat="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    
                           </div>
                </div>

               
                <div class="col-lg-2 col-md-2">
                       <asp:Button ID="BtnSave" runat="server" type="submit" class="fa fa-save btn btn-lg btn-primary"
                        Text="Search" OnClick="BtnSave_Click" style="margin-top:25px;"/>
                      </div>
                </div>

            
            <div style="clear:both;"><hr />
            </div>

             <div class="col-lg-12">
     <asp:GridView ID="grdviewDemoList" runat="server" style="width:100%"
            AutoGenerateColumns="False" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdviewDemoList_PageIndexChanging">
            <Columns>

           <asp:BoundField DataField="requestId" HeaderText="Request Id" />
          <asp:BoundField DataField="fullName" HeaderText="Full Name" />
          <asp:BoundField DataField="Location" HeaderText="Location" />  
          <asp:BoundField DataField="Phone" HeaderText="Phone" />
          <asp:BoundField DataField="bookDemoCatgory" HeaderText="Demo Category" />  
          <asp:BoundField DataField="createdDate" HeaderText="Date" />
          <asp:BoundField DataField="Status" HeaderText="Status" />
         <%--<asp:BoundField DataField="remark" HeaderText="Follow Up" />--%>
               <%-- <asp:BoundField DataField="Follow Up" HeaderText="Follow Up" />
        --%>
                 <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:HyperLink runat="server"   NavigateUrl='<%# string.Format("DemoRequestNew.aspx?requestId={0}",
                                                         HttpUtility.UrlEncode(Eval("requestId").ToString())) %>'>
                                                     <i class="material-icons">View</i></asp:HyperLink>
                                         
                                            </ItemTemplate>
                     </asp:TemplateField>
                </Columns>
         
         <EmptyDataTemplate>
        <div align="center" style= Width="40px" type="Date" Height="40px">No records found.</div>
    </EmptyDataTemplate>
       
          </asp:GridView>


                 </div>
         </div>
         </div>
   
   </asp:Content>
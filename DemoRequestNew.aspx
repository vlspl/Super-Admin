<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="DemoRequestNew.aspx.cs" Inherits="SuperAdmin_DemoRequestNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                 <ContentTemplate>
     <nav class="primary-col-back subheader">
               <div class="container-fluidr">
               
                  <div class="row">
                     <div class="col-sm-6"> 
                        <h4>View Demo Request Page</h4>
                           <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                             <ContentTemplate>--%>

                           

                     <%-- <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtDate"--%>
                     
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:25px">
     <div class="row">
         <div class="col-lg-6 col-md-6">
        <h3 style="text-decoration:underline;">Customer Personal Details</h3>
        </div>
         <div class="col-lg-6 col-md-6">
       
        </div>
       </div> 
        <div class="row">
      
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Full Name<span style="color: Red">*</span></label>
                    <asp:textbox runat="server" ClientIDMode="Static" class="form-control" ID="txtFullName"
                        required placeholder="Full Name" ReadOnly="True"></asp:textbox>
                   
                </div>
                <div class="form-group">
                 <label for="Email">
               Emai Id<span style="color: Red">*</span></label>
                     <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                        <asp:textbox runat="server" ClientIDMode="Static" class="form-control" ID="txtEmail"
                            required placeholder="Email Id" ReadOnly="True"></asp:textbox>
                    </div>
                    </div>
                <div class="form-group">
                    <label>
                      BookDemoCatgory <span style="color: Red">*</span></label>
                         <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtDemoCat"   required placeholder="BookDemoCatgory" ReadOnly="True">
                   </asp:TextBox>
                </div>
                <div class="form-group">
                    <label>
                      Your Query <span style="color: Red">*</span></label>
                    <asp:textbox runat="server" ClientIDMode="Static" class="form-control" ID="txtQuery"
                        required placeholder="Enter  Your Queryme" ReadOnly="True"></asp:textbox>
                </div>

          </div>


            <div class="col-lg-6 col-md-6">
              <div class="form-group">
                    <label>
                        Location<span style="color: Red">*</span></label>
                    <asp:textbox runat="server" ClientIDMode="Static" Style="resize: None;" 
                        CssClass="form-control" ID="txtLocation" placeholder="Location" ReadOnly="True"></asp:textbox>
                </div>
              <div class="form-group">
                    <label>
                        Phone <span style="color: Red">*</span></label>
                    <div class="input-group">
                        <div class="input-group-addon">
                            <i class="fa fa-phone"></i>
                        </div>
                        <asp:textbox ID="txtPhone"  onkeypress="return isNumber(event)" MaxLength="10" runat="server" ClientIDMode="Static" CssClass="form-control"
                            Placeholder="Enter Phone Number"  ReadOnly="True"></asp:textbox>
                    </div>
                    
                        </div>
              <div class="form-group">  
                    <label>
                       Demo date<span style="color: Red">*</span></label>
                    <asp:textbox runat="server" ClientIDMode="Static" class="form-control" ID="txtDemoDate"
                        required placeholder="Enter  Demo date" ReadOnly="True"></asp:textbox>
                </div>
            </div>
              
            </div> 

             <div class="row">
         <div class="col-lg-6 col-md-6">
        <h3 style="text-decoration:underline;">Followup Details</h3>
        </div>
         <div class="col-lg-6 col-md-6">
       
        </div>
       </div> 
          <div class="row">
     
          <div class ="col-lg-4 col-lg-4">
                <div class="form-group">
                    <label>
                       FollowupBy<span style="color: Red" >*</span></label>
                    <asp:textbox runat="server" ClientIDMode="Static" class="form-control" ID="txtBy"
                        required placeholder="Follow By" OnTextChanged="txtBy_TextChanged" ></asp:textbox>
                </div>
              
            </div>
            
             <div class ="col-lg-3 col-lg-3">
            <div class="form-group">
                    <label> 
                      From Date<span style="color: Red"></span></label>

                    
                     <asp:TextBox ID="txtdate" runat="server" cssClass="form-control" ></asp:TextBox>
                     <asp:CalendarExtender ID="txtdate_CalendarExtender" runat="server" 
                            DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" Format="dd/MM/yyyy" 
                            TargetControlID="txtdate" TodaysDateFormat="dd/MM/yyyy">
                        </asp:CalendarExtender>
                     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
                    
                           </div>
                  </div>
          <div class ="col-lg-3 col-lg-3" >
                 <div class="form-group">
                    <label> 
                      Status<span style="color: Red">*</span></label>
                     <asp:DropDownList ID="drpStatus" DataTextField="Name" DataValueField="ID" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True">
                           <asp:ListItem>Followup </asp:ListItem>  
                            <asp:ListItem>Not Intersted </asp:ListItem>  
                            <asp:ListItem>Close </asp:ListItem>  
                    </asp:DropDownList>
                    
                </div>
                </div>
          <div class="col-lg-12 col-md-12">
                <div class="col-lg-3 col-md-3">
                 <div class="form-group">
                    <label> 
                      Remark<span style="color: Red" >*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtRemark" TextMode="MultiLine" 
                        required placeholder="Remark" style="margin-left: -15px; width:875px; resize:none; height:100px;"></asp:TextBox>
                </div>
                </div> 
            </div>
         </div> 
             
           <div class="col-lg-12 col-md-12">
          <div class="col-lg-2 col-md-2">
              <div class="box-footer"  style="width:100%">
                    <asp:Button ID="Button2" runat="server" type="submit" class="fa fa-save btn btn-lg btn-primary" style="margin-left: 200px"
                        Text="submit" OnClick="BtnSave_Click"/>
                    <asp:Label ID="Label2" runat="server" Text="lblError" Visible="False"></asp:Label>
                    <div class="pad">
                        <asp:Literal Text="" ID="Literal2" runat="server" /></div>
                </div>
             </div>
               </div>

       <div class="row">
         <div class="col-lg-6 col-md-6">
        <h3 style="text-decoration:underline;">Followup Log</h3>
        </div>
         <div class="col-lg-6 col-md-6">
       
        </div>
       </div>      

              

        
      <asp:GridView ID="gridFollowUp" runat="server"   CssClass="table table-bordered table-hover table-striped" style="width:100%"
            PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="Followup Details Not Showing" >
            <Columns>
             
                <asp:BoundField DataField="followup_By" HeaderText="FollowUp By" />
                <asp:BoundField DataField="followup_Date" HeaderText="Followup Date" />
                <asp:BoundField DataField="remark" HeaderText="Remark" />
                 <asp:BoundField DataField="Status" HeaderText="Status" />
                  
            </Columns>
          
        </asp:GridView>

        
        </div>
</div>
      </div>
               
           </div>
          
                                   </ContentTemplate>
     </asp:UpdatePanel>

</asp:Content>


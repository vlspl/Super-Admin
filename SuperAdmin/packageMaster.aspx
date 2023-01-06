<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="packageMaster.aspx.cs" Inherits="SuperAdmin_packageMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

  
 <%-- <style>
  .lab-btn-white 
  {
          padding: 2px 9px;
  }
  </style>--%>
   <script>
       function isNumber(evt) {
           evt = (evt) ? evt : window.event;
           var charCode = (evt.which) ? evt.which : evt.keyCode;
           if (charCode > 31 && (charCode < 48 || charCode > 57)) {
               alert('Enter Only Numric Values'); 
           }
           return true;
       }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
   
     <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-8" >
                      <h4>  <sapn>Package Master </sapn>
                       </div>
                      <div class="col-sm-4 text-right">
                       
                        <a href="ViewPackages.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Packages</a>
                     </div>
                     
                  </div>
               </div>
            </nav>
    <div class="container-fluid">
        <div class="labregister">
            <div class="header-wrap">
          
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:TextBox ID="txtpackageName" placeholder="Package Name" runat="server"></asp:TextBox>
                          
                        </div>
                         <div class="form-group">
                            <asp:TextBox ID="txtnoofDays" placeholder="No of Days" onkeypress="return isNumber(event)" MaxLength="3" runat="server"></asp:TextBox>
                        
                        </div>
                         <div class="form-group">
                            <asp:TextBox ID="txtprice" placeholder="Price" onkeypress="return isNumber(event)" MaxLength="5" runat="server"></asp:TextBox>
                          
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
                
              
 		 <div class="row">
				
                     <div class="col-md-6">
                        <div class="text-right">
                            <asp:Button class="lab-btn-primary" ID="RegisterLab" ValidationGroup="labregister"
                                runat="server" Text="Create" OnClick="RegisterLab_Click" />
                        </div>
                    </div>
			</div>
               
             
            </div>
        </div>
    </div>
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

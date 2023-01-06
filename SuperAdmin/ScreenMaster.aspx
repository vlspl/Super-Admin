<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="ScreenMaster.aspx.cs" Inherits="SuperAdmin_ScreenMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Screen Master</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                      
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:20px">
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Screen Name <span style="color: Red">*</span></label>
                    <asp:TextBox ID="txtscreenname" CssClass="form-control" placeholder="Enter Screen Name" runat="server"></asp:TextBox>
                </div>
                  <div class="form-group">
                    <label>
                        Screen Url <span style="color: Red">*</span></label>
                     <asp:TextBox ID="txtscreenurl" class="form-control" placeholder="Enter Screen Url"  runat="server" ></asp:TextBox>
               
                </div>
                 <div class="form-group">
                    <label>
                       Menu Icon <span style="color: Red">*</span></label>
                    <asp:TextBox ID="txtmenuicon" CssClass="form-control" placeholder="Enter Menu Icon" runat="server"></asp:TextBox>
                </div>
            </div>
              <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Display Name <span style="color: Red">*</span></label>
                    <asp:TextBox ID="txtdisplayName" CssClass="form-control" placeholder="Enter Display Name" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>
                       Paraint Screen Id <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="drpparentId"  runat="server" CssClass="form-control">
                   
                    </asp:DropDownList>
                </div>
                <div class="form-group">
               
                     <asp:Button ID="btnaddRole" runat="server" Text="Submit" 
                         CssClass="btn btn-success" style="margin-left:100px; margin-top:30px;" 
                         onclick="btnaddRole_Click" />
                </div>
            </div>
        </div>
        <!-- tab content --><br />
        <div class="tab-content">
            <!-- My Test List Start -->
            <asp:GridView ID="gvscreen" runat="server" AutoGenerateColumns="False" 
                style="width:100%;" DataKeyNames="screenMasterId" AllowPaging="True" 
                onpageindexchanging="gvscreen_PageIndexChanging">

        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />   
<Columns>
<asp:BoundField DataField="screenMasterId" HeaderText="ID" ItemStyle-Width="100" />
<asp:BoundField DataField="screenName" HeaderText="Screen Name" ItemStyle-Width="200" />
  <asp:BoundField DataField="displayName" HeaderText="Display Name"  ItemStyle-Width="200" />
   <asp:BoundField DataField="screenUrl" HeaderText="Screen URL "  ItemStyle-Width="200" />
    <asp:BoundField DataField="parentScreenId" HeaderText="Parient ID"  ItemStyle-Width="100" />
    <asp:BoundField DataField="menuIcon" HeaderText="Menu Icon "  ItemStyle-Width="200" />

            
</Columns>
</asp:GridView>
        </div>
    </div>
   
</asp:Content>

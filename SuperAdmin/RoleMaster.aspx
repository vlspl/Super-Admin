<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="RoleMaster.aspx.cs" Inherits="SuperAdmin_RoleMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Role Master</h4>
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
                        Role Name <span style="color: Red">*</span></label>
                    <asp:TextBox ID="txtroleName" CssClass="form-control" placeholder="Enter Role Name" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>
                        Status <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="drpstatus"  class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" >
                        <asp:ListItem>-Select Status-</asp:ListItem>
                        <asp:ListItem>Active</asp:ListItem>
                        <asp:ListItem>DeActive</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
              <div class="col-lg-6 col-md-6">
                 <div class="form-group">
                    <label>
                       Remark </label>
                    <asp:TextBox ID="txtremark" CssClass="form-control" placeholder="Enter Remark" TextMode="MultiLine" style="height:80px; resize:none;" runat="server"></asp:TextBox>
                </div>
                 <div class="form-group">
                     <asp:Button ID="btnaddRole" runat="server" Text="Submit" 
                         CssClass="btn btn-success" style="margin-left:100px;" 
                         onclick="btnaddRole_Click" />
                </div>
            </div>
        </div>
        <!-- tab content --><br />
        <div class="tab-content">
            <!-- My Test List Start -->
            <asp:GridView ID="gvroleMaster" runat="server" AutoGenerateColumns="False" style="width:100%;" DataKeyNames="rollMasterId">

        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />   
<Columns>
<asp:BoundField DataField="rollMasterId" HeaderText="ID" ItemStyle-Width="100" />
<asp:BoundField DataField="rollName" HeaderText="Role Name" ItemStyle-Width="200" />
  <asp:BoundField DataField="validDays" HeaderText="Status "  ItemStyle-Width="200" />
   <asp:BoundField DataField="remark" HeaderText="Remark "  ItemStyle-Width="300" />
 <asp:TemplateField HeaderText="Action " ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Button ID="btnChangeStatus" runat="server" OnClick="ChangeStatus" Text="Change Status" />
                    </ItemTemplate>
                </asp:TemplateField>
            
</Columns>
</asp:GridView>
        </div>
    </div>
   
</asp:Content>

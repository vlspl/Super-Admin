<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="RoleCounterMaster.aspx.cs" Inherits="SuperAdmin_RoleCounterMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Role Counter Master</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                      
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:20px">
    <div class="row clearfix">
            <p style="color:red; margin-left:20px;">Note : ( * ) Denotes Mandatory Fields.</p><br />
        </div>
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Role Name <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="drproleMaster"  class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True" onselectedindexchanged="drproleMaster_SelectedIndexChanged" 
                        >
                       
                    </asp:DropDownList>
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
                        Counter Name <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="drpcounter"  class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%"  >
                       <asp:ListItem>-Select Counter-</asp:ListItem>
                       <asp:ListItem>Lab</asp:ListItem>
                        <asp:ListItem>Organization</asp:ListItem>
                         <asp:ListItem>Government</asp:ListItem>
                         
                    </asp:DropDownList>
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
            <asp:GridView ID="gvroleCounterMaster" runat="server" AutoGenerateColumns="False" style="width:100%;" DataKeyNames="roleCounterId">

        
<Columns>
<asp:BoundField DataField="roleCounterId" HeaderText="ID" ItemStyle-Width="100" />
<asp:BoundField DataField="rollName" HeaderText="Role Name" ItemStyle-Width="200" />
  <asp:BoundField DataField="counterName" HeaderText="Counter Name "  ItemStyle-Width="200" />
   
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

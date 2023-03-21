<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="RoleScreenMaster.aspx.cs" Inherits="SuperAdmin_RoleScreenMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Role Screen Master</h4>
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
                        runat="server" Style="width: 100%" AutoPostBack="True" 
                        onselectedindexchanged="drproleMaster_SelectedIndexChanged" >
                       
                    </asp:DropDownList>
                </div>
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
              <div class="col-lg-6 col-md-6">
                 <div class="form-group">
                 <p style="color:Green;"><b>Note : Green Color indicates Parent Menu</b></p>
                   <%-- <label>
                       Screen Name <span style="color: Red">*</span></label>--%>
                    <div class="col-sm-12" style="height:255px; overflow:auto;">
                                                <%--<asp:CheckBoxList ID="CheckBoxList_screens" runat="server" style="margin-left:10px;" 
                                                   DataSourceID="SqlDataSource_scrn" DataTextField="screenName" 
                                                   DataValueField="screenMasterId" ></asp:CheckBoxList>
                                               <asp:SqlDataSource ID="SqlDataSource_scrn" runat="server" 
                                                   ConnectionString="<%$ ConnectionStrings:constr %>" 
                                                   SelectCommand="SELECT [screenName], [screenMasterId] FROM [screenMaster]">
                                               </asp:SqlDataSource>--%>
                        <asp:GridView ID="gridscreen" runat="server" DataKeyNames="screenMasterId"  AutoGenerateColumns="false" 
                                                    EmptyDataText="No Data Found." onrowdatabound="gridscreen_RowDataBound">
                        <Columns>
 <asp:TemplateField HeaderText="Select" >
                    <ItemTemplate>
                        <asp:CheckBox ID="chkselect_screen" runat="server" />
                    </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="Screen Name" >
                    <ItemTemplate>
                        <asp:Label ID="lblscreenName"  runat="server" Text='<%# Bind("screenName") %>'>></asp:Label>
                        &nbsp
                    </ItemTemplate>
                 </asp:TemplateField>
                   <%--<asp:TemplateField HeaderText="Paraint ID" >
                    <ItemTemplate>
                        <asp:Label ID="lblparantId" style="display:none;"  runat="server" Text='<%# Bind("parentScreenId") %>'>></asp:Label>
                        &nbsp
                    </ItemTemplate>
                 </asp:TemplateField>--%>
                 </Columns>
                        </asp:GridView>
                        
                    </div>
                </div>
                
            </div>
        </div>
        <!-- tab content --><br />
        <div class="tab-content">
            <!-- My Test List Start -->
            <asp:GridView ID="gvrolescreenMaster" runat="server" 
                AutoGenerateColumns="False" style="width:100%;" DataKeyNames="rollToScreenId" 
                AllowPaging="True" EmptyDataText="No Record Found." 
                onpageindexchanging="gvrolescreenMaster_PageIndexChanging" onrowdeleting="gvrolescreenMaster_RowDeleting" 
               >

        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />   
<Columns>
 <asp:TemplateField HeaderText="ID" >
                    <ItemTemplate>
                        <asp:Label ID="lblrollscreenId"  runat="server" Text='<%# Bind("rollToScreenId") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>

<asp:BoundField DataField="rollName" HeaderText="Role Name" ItemStyle-Width="200" />
  <asp:BoundField DataField="screenName" HeaderText="Screen Name "  ItemStyle-Width="200" />
   <asp:BoundField DataField="specialRole" HeaderText="Special Role "  ItemStyle-Width="300" />
   <asp:BoundField DataField="remark" HeaderText="Remark "  ItemStyle-Width="300" />
    <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" ShowHeader="true"  />
</Columns>
</asp:GridView>
        </div>
    </div>
   
</asp:Content>

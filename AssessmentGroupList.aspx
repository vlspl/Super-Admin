<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="AssessmentGroupList.aspx.cs" Inherits="SuperAdmin_AssessmentGroupList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
     <script type="text/javascript">
         function deleteitem() {
             if (confirm("Are you sure you want to Deactive...? ")) {
                 return true;
             }
             return false;
         }
    </script>
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4> Self Assessment Group List</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                        <a href="selfGroup.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Self Assessment Group</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">

        <asp:GridView ID="gridassessmentGroup" runat="server" DataKeyNames="assessmentGroupID"  CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="true" AutoGenerateColumns="False" OnPageIndexChanging="gridassessmentGroup_PageIndexChanging" OnRowDeleting="gridassessmentGroup_RowDeleting" >
            <Columns>
               <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="30">
                    <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                                             
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="assessmentGroupName" HeaderText="Group Name" />
                <asp:BoundField DataField="assessmentGroupDescription" HeaderText="Group Description" />
                <asp:BoundField DataField="groupStatus" HeaderText="Status" />
                 <asp:TemplateField HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbltestId"  runat="server" Text='<%# Bind("assessmentGroupID") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>                       
                <asp:TemplateField HeaderText="Delete" >
                        <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                      CommandName="Delete" OnClientClick="return confirm('Do you want to delete assessment group')"  Text="<i class='fa fa-trash fa-2x'></i>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:HyperLink  runat="server" NavigateUrl='<%# string.Format("selfGroup.aspx?assessmentGroupID={0}",
HttpUtility.UrlEncode(Eval("assessmentGroupID").ToString())) %>'>
                            <i class="fa fa-edit fa-2x"></i></asp:HyperLink>
                                                
                </ItemTemplate>
            </asp:TemplateField>
               
            </Columns>
        </asp:GridView>
    </div>
    <div class="pad" style="color: Red">
        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
</asp:Content>


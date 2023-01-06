<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="ViewTestUploadDetails.aspx.cs" Inherits="SuperAdmin_ViewTestUploadDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript">
     function deleteitem() {
         if (confirm("Are you sure you want to delete...? ")) {
             return true;
         }
         return false;
     }
    </script>
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Test Bulk Upload Master</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                        <a href="TestBulkUploadMaster.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Test Bulk Upload Master</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">

        <asp:GridView ID="grdviewOrgnization" runat="server"  CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="true" AutoGenerateColumns="False" OnPageIndexChanging="grdviewOrgnization_PageIndexChanging"
            OnRowDeleting="grdviewOrgnization_RowDeleting" OnRowDataBound="grdviewOrgnization_RowDataBound" DataKeyNames="testResultUploadId"
            OnSelectedIndexChanged="grdviewOrgnization_SelectedIndexChanged" >
            <Columns>
            <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="30">
             <ItemTemplate>
                <%#Container.DataItemIndex+1 %>
               </ItemTemplate>
               </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Organization Name" />
                <asp:BoundField DataField="testId" HeaderText="Test ID" />
                <asp:BoundField DataField="testCode" HeaderText="Test Code" />
                <asp:BoundField DataField="testName" HeaderText="Test Name" />
                <asp:BoundField DataField="colSpecification" HeaderText="Column Specification" />
                <asp:BoundField DataField="testUploadName" HeaderText="Test Upload Name" />
                <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="20"  HeaderText= "" DeleteText="Delete"/>
<%--                <asp:CommandField ShowEditButton="true" ItemStyle-Width="20"  HeaderText= "Edit" DeleteText="<i class='material-icons'>edit</i>"/>
--%>                 <asp:TemplateField>
                     <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" HeaderText= "Edit"   NavigateUrl='<%# string.Format("~/SuperAdmin/TestBulkUploadMaster.aspx?testResultUploadId={0}",
                    HttpUtility.UrlEncode(Eval("testResultUploadId").ToString())) %>'>
                         <i class="material-icons">Edit</i></asp:HyperLink>
                      </ItemTemplate>
                </asp:TemplateField>
                 
            </Columns>
            
        </asp:GridView>
    </div>
    <div class="pad" style="color: Red">
        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
</asp:Content>


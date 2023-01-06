<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="ViewLog.aspx.cs" Inherits="SuperAdmin_ViewLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                        <h4>View overall Log</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                       
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">
  <%--  <span style="float:right;">Number Of Records&nbsp;:&nbsp;<asp:Label ID="lbltotalcount" runat="server" Text="Label"></asp:Label></span>
  --%>      <asp:GridView ID="gridViewLog" runat="server" DataKeyNames="Logid"  CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="true" AutoGenerateColumns="False" 
            onpageindexchanging="gridViewLog_PageIndexChanging" 
            onrowdeleting="gridViewLog_RowDeleting">
            <Columns>
             <asp:TemplateField HeaderText="Log ID" >
                    <ItemTemplate>
                        <asp:Label ID="lbllogId"  runat="server" Text='<%# Bind("Logid") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
              
                <asp:BoundField DataField="ExceptionMsg" HeaderText="Exception Msg" />
                <asp:BoundField DataField="ExceptionType" HeaderText="Exception Type" />
                <%--<asp:BoundField DataField="ExceptionSource" HeaderText="Exception Source" />--%>
                <asp:BoundField DataField="ExceptionURL" HeaderText="Exception URL" />
                <asp:BoundField DataField="Logdate" HeaderText="Log Date" />
                  <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" ShowHeader="true"  />
                <asp:TemplateField HeaderText="View">
                    <ItemTemplate>
                      <asp:HyperLink ID="HyperLink1" Target="_blank" runat="server" NavigateUrl='<%# "logs.aspx?Logid="+Eval("Logid") %>'>  View</asp:HyperLink><!---->
                                                 
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
   
</asp:Content>

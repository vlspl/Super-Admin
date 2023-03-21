<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="selftest.aspx.cs" Inherits="SuperAdmin_selftest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $("[src*=plus]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "images/minus.png");
    });
    $("[src*=minus]").live("click", function () {
        $(this).attr("src", "images/plus.png");
        $(this).closest("tr").next().remove();
    });
</script>
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
                        <h4> Self Assessment Test List</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                        <a href="selfasstest.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Self Assessment Test</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">
        <asp:GridView ID="gridselfTest" runat="server" AutoGenerateColumns="false" CssClass="Grid" Width="100%" 
    DataKeyNames="assessmentMasterID" OnRowDataBound="gridselfTest_RowDataBound" AllowPaging="True" OnPageIndexChanging="gridselfTest_PageIndexChanging1" >
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <img alt = "" style="cursor: pointer; width:20px;" src="images/plus.png"  />
                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                    <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid" style="margin-left:200px;">
                        <Columns>
                              <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="30">
                    <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                                             
                    </ItemTemplate>
                </asp:TemplateField>
                              <asp:BoundField DataField="optionDetails" HeaderText="Option" ItemStyle-Width="300"/>
                                 <asp:BoundField DataField="optionStatus" HeaderText="Status" ItemStyle-Width="100"/>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="assessmentGroup" HeaderText="Group Name" />
                <asp:BoundField DataField="assessmentQuestion" HeaderText="Question" />

                <asp:BoundField DataField="assessmentStatus" HeaderText="Status" />
               <asp:BoundField DataField="assessmentFor" HeaderText="For" />
               <asp:BoundField DataField="assessmentSquence" HeaderText="Squence" />
    </Columns>
</asp:GridView>
        
    </div>
    <div class="pad" style="color: Red">
        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
</asp:Content>


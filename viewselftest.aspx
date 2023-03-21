<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="viewselftest.aspx.cs" Inherits="SuperAdmin_viewselftest" %>

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
                        <h4>View Self Assessment Test</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                         <a href="selftest.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Self Test</a>
                    </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">
         <div class="col-sm-6">
        <asp:FormView CssClass="container" ID="FormView1" runat="server" style="width:100%" EmptyDataText="Self Test Details not Found.">  
            <ItemTemplate>  
                <table class="table table-bordered table-striped">  
                    <tr>  
                        <td>ID</td>  
                        <td><%#Eval("assessmentMasterID") %></td>  
                    </tr>  
                    <tr>  
                        <td>Assessment Group</td>  
                        <td><%#Eval("assessmentGroup") %></td>  
                    </tr>  
                    <tr>  
                        <td>Assessment Question</td>  
                        <td><%#Eval("assessmentQuestion") %></td>  
                    </tr>  
                    <tr>  
                        <td>Assessment Description</td>  
                        <td><%#Eval("assessmentDescription") %></td>  
                    </tr>  
                    <tr>  
                        <td>Assessment Status</td>  
                        <td><%#Eval("assessmentStatus") %></td>  
                    </tr>  
                    <tr>  
                        <td>Assessment For</td>  
                        <td><%#Eval("assessmentFor") %></td>  
                    </tr>  
                    <tr>  
                        <td>Assessment Squence</td>  
                        <td><%#Eval("assessmentSquence") %></td>  
                    </tr>  
                    <tr>  
                        <td>Assessment GroupID</td>  
                        <td><%#Eval("assessmentGroupID") %></td>  
                    </tr>  
                    <tr>  
                        <td>isLast Question</td>  
                        <td><%#Eval("islastQuestion") %></td>  
                    </tr>  
                    
                </table>  
            </ItemTemplate>  
        </asp:FormView>  
        </div>
        <div class="col-sm-6">
            <asp:GridView ID="gridoptions" runat="server" AutoGenerateColumns="false" CssClass="table">
                <Columns>
                      <asp:BoundField DataField="assessmentOptionID" HeaderText="ID" />
                      <asp:BoundField DataField="optionDetails" HeaderText="Option" />
                  <asp:BoundField DataField="optionSequences" HeaderText="Sequences" />
                  <asp:BoundField DataField="optionStatus" HeaderText="Status" />
                </Columns>
                
            </asp:GridView>  
        </div>
    </div>
    <div class="pad" style="color: Red">
        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
</asp:Content>


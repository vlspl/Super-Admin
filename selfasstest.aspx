<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="selfasstest.aspx.cs" Inherits="SuperAdmin_selfasstest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="Javascript">
      function isNumberKey(evt)
      {
         var charCode = (evt.which) ? evt.which : evt.keyCode;
         if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;    
         return true;
      }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Add Self Assessment Test</h4>
                     </div>
                      <div class="col-sm-6 text-right">
                       
                        <a href="selftest.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Self Test</a>
                     </div>
                     
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:25px">
    	<div class="row clearfix">
            <p style="color:red; margin-left:20px;">Note : ( * ) Denotes Mandatory Fields.</p><br />
        </div>
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Assessment Group <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="drpassessmentgroup" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="drpassessmentgroup_SelectedIndexChanged">
                       
                    </asp:DropDownList>
                </div>
                 <div class="form-group">
                    <label>
                        Assessment Question <span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" 
                        CssClass="form-control" ID="txtquestion" placeholder="Assessment Question"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtquestion" ForeColor="Red"  runat="server" ErrorMessage="Please Enter Any Question"></asp:RequiredFieldValidator>
               
                </div>
                
                 <div class="form-group">
                    <label>
                        Assessment For <span style="color: Red">*</span></label>
                     <asp:DropDownList ID="drpfor" runat="server" CssClass="form-control">
                        <asp:ListItem>All</asp:ListItem>
                         <asp:ListItem>Male</asp:ListItem>
                        <asp:ListItem>Female</asp:ListItem>
                    </asp:DropDownList>
                </div>
               
            </div>
            <div class="col-lg-6 col-md-6">
                
              
                 <div class="form-group">
                    <label>
                        Assessment Group ID </label>
                    <asp:TextBox runat="server" ClientIDMode="Static" 
                        CssClass="form-control" ReadOnly="true" ID="txtgroupId" onkeypress="return isNumberKey(event)" MaxLength="1" placeholder="Assessment Group ID"></asp:TextBox>
                </div>
                 
                <div class="form-group">
                    <label>
                        Assessment Squence <span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" 
                        CssClass="form-control" ID="txtsquence" onkeypress="return isNumberKey(event)" MaxLength="1" placeholder="Assessment Squence"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtsquence" ForeColor="Red"  runat="server" ErrorMessage="Please Enter Assessment Squence"></asp:RequiredFieldValidator>
               
                </div>
                 
                 <div class="form-group">
                    <label>
                        islast Question <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="drplastquestion" runat="server" CssClass="form-control">
                         <asp:ListItem>No</asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                    </asp:DropDownList>
                </div>
                
               
            </div>
             <div class="col-lg-12 col-md-12">
                 
                 <div class="col-lg-3 col-md-3">
                       <div class="form-group">
                    <label>
                        Option Details</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" 
                        CssClass="form-control" ID="txtoptions" placeholder="Option Details"></asp:TextBox>
                </div>
                 </div>
                 <div class="col-lg-3 col-md-3">
                       <div class="form-group">
                    <label>
                        Option Squence</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" 
                        CssClass="form-control" ID="txtosquence" onkeypress="return isNumberKey(event)" MaxLength="1" placeholder="Option Squence"></asp:TextBox>
                </div>
                 </div>
                 <div class="col-lg-3 col-md-3">
                       <div class="form-group">
                    <label>
                        Option Status</label>
                     <asp:DropDownList ID="drpoptionstatus" runat="server" CssClass="form-control">
                        <asp:ListItem>Active</asp:ListItem>
                        <asp:ListItem>Deactive</asp:ListItem>
                    </asp:DropDownList>
                </div>
                 </div>
                 <div class="col-lg-3 col-md-3">
                     <asp:Button ID="btnadd" runat="server" type="submit" style="margin-top:29px;" class="fa fa-save btn btn-lg btn-primary"
                        Text="Add " OnClick="btnadd_Click"  />
                 </div>
                 <br />
                 <br />
                  <asp:GridView ID="GridView1" DataKeyNames="tempoptionId" runat="server" CssClass="table table-bordered table-striped table-hover dataTable js-exportable" AutoGenerateColumns="false"
                                                    EmptyDataText="No records has been added." AllowPaging="false"  OnRowDeleting="GridView1_RowDeleting" >
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="30">
                                                            <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                             
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="optionDetails" HeaderText="Option Details"  />
                                                        <asp:BoundField DataField="optionSequences" HeaderText="Squence"  />
                                                        <asp:BoundField DataField="optionStatus" HeaderText="Status" />
                                                       <asp:TemplateField HeaderText="ID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltestId"  runat="server" Text='<%# Bind("tempoptionId") %>'>></asp:Label>
                                                            </ItemTemplate>
                                                         </asp:TemplateField>                       
                                                        <asp:TemplateField HeaderText="Delete" >
                                                                <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                                              CommandName="Delete" OnClientClick="return confirm('Do you want to delete option')"  Text="<i class='fa fa-trash fa-2x'></i>"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
             </div>
            <div class="col-lg-6 col-md-6">
                
              
                 <div class="form-group">
                    <label>
                        Assessment Description</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" 
                        CssClass="form-control" ID="txtdescription" TextMode="MultiLine" style="height:100px; resize:none;" placeholder="Assessment Description"></asp:TextBox>
                </div>
               
            </div>
              <div class="col-lg-6 col-md-6">
                
              
                 <div class="form-group">
                    <label>
                        Assessment Status <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="drpassessmentstatus" runat="server" CssClass="form-control">
                        <asp:ListItem>Active</asp:ListItem>
                        <asp:ListItem>Deactive</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="box-footer" align="right">
                    <asp:Button ID="BtnSave" runat="server" type="submit" class="fa fa-save btn btn-lg btn-primary"
                        Text="Save" OnClick="BtnSave_Click"  />
                      
                </div>
            </div>
        </div>
    </div>
</asp:Content>

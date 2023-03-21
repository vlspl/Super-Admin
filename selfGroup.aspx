<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="selfGroup.aspx.cs" Inherits="SuperAdmin_selfGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Add Self Assessment Group</h4>
                     </div>
                      <div class="col-sm-6 text-right">
                          <asp:HiddenField ID="hdngroupId" runat="server" />
                        <a href="AssessmentGroupList.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Selft Group</a>
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
                        Group Name <span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtassessmentGroup"
                         placeholder="Enter Assessment Group Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtassessmentGroup" ErrorMessage="Please Enter Assessment Group" ForeColor="Red"></asp:RequiredFieldValidator>
                
                </div>
                
                <div class="form-group">
                    <label>
                        Group Description </label>
                    <asp:TextBox runat="server" ClientIDMode="Static" 
                        CssClass="form-control" ID="txtdescription" placeholder="Group Descrption" TextMode="MultiLine" Style="height:100px; resize:none;"></asp:TextBox>
                </div>
              
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label for="Email">
                        Status </label>
                    <asp:DropDownList ID="drpstatus" runat="server" CssClass="form-control">
                        <asp:ListItem>Active</asp:ListItem>
                        <asp:ListItem>Deactive</asp:ListItem>
                    </asp:DropDownList>

                </div>
               <br /><br />
                <div class="box-footer" >
                    <asp:Button ID="BtnSave" runat="server" type="submit" class="fa fa-save btn btn-lg btn-primary"
                        Text="Save" OnClick="BtnSave_Click"  />
                      <asp:Button ID="btnUpdate" runat="server"  class="fa fa-save btn btn-lg btn-primary"
                        Text="Update" visible="false" OnClick="btnUpdate_Click" />

                </div>
            </div>
        </div>
    </div>
</asp:Content>

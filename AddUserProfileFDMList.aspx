<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="AddUserProfileFDMList.aspx.cs" Inherits="SuperAdmin_AddUserProfileFDMList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Add User Profile FDM List</h4>
                     </div>
                      <div class="col-sm-6 text-right">
                       
                        <a href="FDMList.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View User Profile FDM List</a>
                     </div>
                     
                  </div>
               </div>
            </nav>
    <asp:HiddenField ID="hdncreatedby" runat="server" />
    <asp:HiddenField ID="hdnlistId" runat="server" />
    <div class="box-body" style="padding:25px">
    	<div class="row clearfix">
            <p style="color:red; margin-left:20px;">Note : ( * ) Denotes Mandatory Fields.</p><br />
        </div>
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Name<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtname"
                        required placeholder="Enter Name"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>
                        Type<span style="color: Red">*</span></label>
                    <asp:DropDownList ID="drpfdmType" runat="server" CssClass="form-control">
                    <asp:ListItem>-Select-</asp:ListItem>
                    <asp:ListItem>Food</asp:ListItem>
                    <asp:ListItem>Drug</asp:ListItem>
                     <asp:ListItem>Medical Condition</asp:ListItem>
                      <asp:ListItem>Family History</asp:ListItem>
                    </asp:DropDownList>
                </div>
                  <div class="form-group">
                   <label>
                        Status<span style="color: Red">*</span></label>
                    <asp:DropDownList ID="drpstatus" runat="server" CssClass="form-control">
                    <asp:ListItem>-Select-</asp:ListItem>
                    <asp:ListItem Value="A">Active</asp:ListItem>
                    <asp:ListItem Value="D">DeActive</asp:ListItem>
                    
                    </asp:DropDownList>
                </div>
               
                
            </div>
            <div class="col-lg-6 col-md-6">
               
                <div class="form-group">
                    <label>
                        Remark</label>
                    <asp:TextBox runat="server" ClientIDMode="Static" Style="resize: None; height:209px;" TextMode="MultiLine"
                        CssClass="form-control" ID="txtremark" placeholder="Remark"></asp:TextBox>
                </div>
              
                
            </div>
             <div style="clear:both;"><br></div>
               
                <div class="box-footer" align="right">
                    <asp:Button ID="btnsubmit" runat="server" type="Submit" class="fa fa-save btn btn-lg btn-primary"
                        Text="Save" onclick="btnsubmit_Click"  />
                   <asp:Button ID="btnupdate" runat="server" type="Update" class="fa fa-save btn btn-lg btn-primary"
                        Text="Update" onclick="btnupdate_Click"  />
                </div>
        </div>
    </div>
</asp:Content>


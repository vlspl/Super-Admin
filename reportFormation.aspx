﻿<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="reportFormation.aspx.cs" Inherits="SuperAdmin_reportFormation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <nav class="primary-col-back subheader">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-sm-6">
                     <h4>Add New Report Formation</h4>
                  </div>                 
               </div>
            </div>
         </nav>
    <div class="container-fluid">
        <asp:HiddenField ID="hdnorgid" runat="server" />
         <asp:HiddenField ID="hdnbranchid" runat="server" />
        <asp:HiddenField ID="hdnlabcode" runat="server" />
        <div class="labregister">
        	<div class="row clearfix">
            <p style="color:red; margin-left:20px;">Note : ( * ) Denotes Mandatory Fields.</p><br />
        </div>
            <div class="header-wrap">
             <span style="color:Green;">Add Report Formation : </span>
              
          
                <div class="row" style="margin-top:10px;">
                    <div class="col-md-6">
                        <div class="form-group">
                         <label>
                        Select Lab <span style="color: Red">*</span></label>
                             <asp:DropDownList ID="ddllabName" DataTextField="sLabName" DataValueField="sLabId" 
                        runat="server" Style="width: 100%" AutoPostBack="True" 
                                 onselectedindexchanged="ddllabName_SelectedIndexChanged" >
                    </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                       <div class="form-group">
                        <label>
                        Select Section <span style="color: Red">*</span></label>
                           <asp:DropDownList ID="drpsection"  runat="server" Style="width: 100%" >
                          
                          </asp:DropDownList>
                       
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                     <div class="form-group">
                      <label>
                        Remark </label>
                            <asp:TextBox ID="txtdetails" placeholder="Details" style="height:100px; resize:none;" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </div>
                       
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                         <label>
                        Status </label>
                           <asp:TextBox ID="txtstatus"  placeholder="Status either 1=Active or 0=Deactive" runat="server"></asp:TextBox>
                           
                        </div>
                         <div class="form-group text-right">
                           <asp:Button class="lab-btn-primary" ID="btnaddreportFormation" 
                                runat="server" Text="Submit" onclick="btnaddreportFormation_Click"   />
                        </div>
                    </div>
                </div>
                
                      
                    
               
               <br />
                <span style="color:Green;"> Report Formation Details : </span>
                  <div class="row" style="margin-top:10px;">
                     <asp:GridView ID="gridreportFormation" runat="server" DataKeyNames="trfID"  CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="True" AutoGenerateColumns="False" 
                          EmptyDataText = "Report Formation Not Found" onrowdeleting="gridreportFormation_RowDeleting" 
            >
            <Columns>
                  <asp:TemplateField HeaderText="Sr No" >
                    <ItemTemplate>
                        <asp:Label ID="trfID"  runat="server" Text='<%# Bind("trfID") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="Lab Id" >
                    <ItemTemplate>
                        <asp:Label ID="sLabId"  runat="server" Text='<%# Bind("sLabId") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Section Name" >
                    <ItemTemplate>
                        <asp:Label ID="sectionName"  runat="server" Text='<%# Bind("sectionName") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Status" >
                    <ItemTemplate>
                        <asp:Label ID="status"  runat="server" Text='<%# Bind("status") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Details" >
                    <ItemTemplate>
                        <asp:Label ID="Details"  runat="server" Text='<%# Bind("Details") %>'>></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                      <ItemTemplate>
                        <asp:Button ID="btnChangeStatus" runat="server"  OnClick="ChangeStatus" style="width:150px;" Text="Change Status" />
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandName="Delete" OnClientClick="return confirm('Are you sure want to delete')"  Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
                      </div>
                    
             
                 <br />
                  

                   
                
            </div>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
  
</asp:Content>


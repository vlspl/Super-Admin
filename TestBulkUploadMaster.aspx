<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="TestBulkUploadMaster.aspx.cs" Inherits="SuperAdmin_TestBulkUploadMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Test Upload Management</h4>
                     </div>
                      <div class="col-sm-6 text-right">
                   
                        <a href="ViewTestUploadDetails.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Upload Master</a>
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
                        Organization Name<span style="color: Red">*</span></label>
                    <asp:DropDownList ID="ddlName" DataTextField="Name" DataValueField="ID" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True" 
                        onselectedindexchanged="ddlName_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="form-group">
                    <label>
                        Test Analyte<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" Style="resize: None;" TextMode="MultiLine"
                        CssClass="form-control" ID="txtColSpec" placeholder=" (e.g analyte1,analyte2)"></asp:TextBox>
                </div>
                
             
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label for="Email">
                        Test Name<span style="color: Red">*</span></label>
                   <%-- <div class="input-group">--%>
                     <%--   <span class="input-group-addon"><i class="fa fa-envelope"></i></span>--%>
                    <asp:DropDownList ID="ddlTestName" DataTextField="sTestName" DataValueField="Test_Id" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True">
                    </asp:DropDownList>
                    <%--</div>--%>


                </div>
                <div class="form-group">
                    <label>
                        Test Upload Name<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" Style="resize: None;" TextMode="MultiLine"
                        CssClass="form-control" ID="txtTestuploadName" placeholder="Test Upload Name"></asp:TextBox>
                </div>
                <div class="form-group">
                    <div class="form-group has-feedback">
                        <div class="row">
                            <div class="col-lg-4 col-md-1">
                                <label>
                                    Show In Top Vulnerability</label></div>
                            <div class="col-lg-6 col-md-6">
                            <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox>
                                </div>
                        </div>
                    </div>
                </div>
                
               
                <div class="box-footer" align="right">
                    <asp:Button ID="BtnSave" runat="server" type="submit" class="fa fa-save btn btn-lg btn-primary"
                        Text="Save" onclick="BtnSave_Click"  /><!-- onclick="BtnSave_Click"-->
                    <div class="pad">
                        <asp:Literal Text="" ID="litErrorMessage" runat="server"  /></div>
                </div>
            </div>
         <div  class="col-lg-8 col-md-6">
          <div class="form-group">
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
                <asp:BoundField DataField="testId" HeaderText="Test ID" />
                <asp:BoundField DataField="testCode" HeaderText="Test Code" />
                <asp:BoundField DataField="testName" HeaderText="Test Name" />
                <asp:BoundField DataField="colSpecification" HeaderText="Column Specification" />
                <asp:BoundField DataField="testUploadName" HeaderText="Test Upload Name" />
                <asp:CommandField ShowDeleteButton="true" ItemStyle-Width="20"  HeaderText= "Delete" DeleteText="Delete"/>
               
                 
            </Columns>
            
        </asp:GridView>
                </div>
         </div>

        </div>
    </div>

</asp:Content>


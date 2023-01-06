<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="Copy of labTestPriceBulkUpload.aspx.cs" Inherits="SuperAdmin_labTestPriceBulkUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Lab Test Price Bulk Upload</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                       
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">
         <div class="col-lg-5">
     <div class="form-group">
                    <label  class="control-label col-sm-4">Select Lab</label>
                        <div class="col-sm-8">
                    <asp:DropDownList ID="ddllabName" runat="server" CssClass="form-control" 
                                AutoPostBack="True" onselectedindexchanged="ddllabName_SelectedIndexChanged" 
                                >
                   
                    </asp:DropDownList>
                    </div>
                </div>
    </div>
        <div style="clear:both;"><br /></div>
    <div class="col-lg-4">
    <br />
     <div class="form-group">
                    <label  class="control-label col-sm-4">Select File</label>
                        <div class="col-sm-8">
                            <asp:FileUpload ID="FileUpload_testList" runat="server" />
                    </div>
                </div>
    </div>
     <div class="col-lg-5">
     <br />
         <asp:Button ID="btnupload" runat="server" Text="Upload" CssClass="btn btn-info" onclick="btnupload_Click" 
             />
     </div>
   <div style="clear:both;"><br /></div>
       <asp:GridView ID="GridTestUpload" CssClass="table table-bordered table-striped table-hover dataTable js-exportable"
                                      EmptyDataText="No Record Found"      runat="server" AutoGenerateColumns="false"  DataKeyNames="testUploadId" >

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                         <%#Container.DataItemIndex+1 %>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                       <asp:BoundField DataField="fileName" HeaderText="File Name" ItemStyle-Width="400" />    
                                                       
                                                <asp:BoundField DataField="uploadDate" HeaderText="Date" ItemStyle-Width="300" />
                                                 <asp:BoundField DataField="uploadBy" HeaderText="Uploaded By" ItemStyle-Width="150" />
                                              
                                              <asp:TemplateField HeaderText="View Log">
                                                    <ItemTemplate>
                                                     <asp:LinkButton ID="lnkDownload" Text="Log" CommandArgument='<%# Eval("testUploadId") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                                  </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                        </asp:GridView>
    </div>
   
                   
</asp:Content>


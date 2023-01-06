<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="AddLifeStyleDisorder.aspx.cs" Inherits="SuperAdmin_AddLifeStyleDisorder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function deleteitem() {
            if (confirm("Are you sure you want to delete...? ")) {
                return true;
            }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Life Style Disorder Management</h4>
                     </div>
                      <div class="col-sm-6 text-right">
                       <a href="AddLifeStyleTestList.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Test</a>
                   
                       <%--  <a href="ViewOrgnization.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Organization</a>
                 --%>    </div>
                     
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 25px">
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Life Style Disorder Name<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtDisorderame"
                         placeholder="Enter  Life Style Disorder Name"></asp:TextBox>
                </div>
                <div class="form-group">
                    <div class="form-group has-feedback">
                        <div class="row">
                            <div class="col-lg-5 col-md-5">
                                <label>
                                    Life Style Disorder Icon</label></div>
                            <div class="col-lg-6 col-md-6">
                                <asp:FileUpload ID="FuIcon" CssClass=" btn btn-default" runat="server" /></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Description<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" Style="resize: None;" TextMode="MultiLine"
                        CssClass="form-control" ID="txtDetails" placeholder="Enter Description"></asp:TextBox>
                </div>
                <div class="box-footer" align="right">
                    <asp:Button ID="BtnSave" runat="server" type="submit" class="fa fa-save btn btn-lg btn-primary"
                        Text="Save" OnClick="BtnSave_Click" />
                    <div class="pad">
                        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
                </div>
            </div>
        </div>
        <hr />
        <h4>View Life Style Disorder List</h4>
        <asp:GridView ID="grdviewLifeStyle" DataKeyNames="Id" runat="server" CssClass="table table-bordered table-hover table-striped"
            PageSize="10" AllowPaging="true" AutoGenerateColumns="False" OnPageIndexChanging="grdviewLifeStyle_PageIndexChanging"
            OnRowDeleting="grdviewLifeStyle_RowDeleting" 
            OnSelectedIndexChanged="grdviewLifeStyle_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:TemplateField HeaderText="Icon">
                    <ItemTemplate>
                        <img src="..<%# Eval("Image")%>" height="40px" width="80px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:ImageButton ID="img_user" runat="server" CommandName="Delete" OnClientClick="return deleteitem();"
                            ImageUrl='~/images/Active.png' Width="30px" Height="30px" Visible='<%# Eval("IsActive").ToString() == "True" ? true : false %>'>
                        </asp:ImageButton>
                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ImageUrl='~/images/Deactive.jpg'
                            Width="30px" Height="30px" Visible='<%# Eval("IsActive").ToString() == "False" ? true : false %>'>
                        </asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

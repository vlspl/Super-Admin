<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master"
    AutoEventWireup="true" CodeFile="ChannelPartner.aspx.cs" Inherits="SuperAdmin_ChannelPartner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Channel Partner Management</h4>
                     </div>
                      <div class="col-sm-6 text-right">
                        <a href="ViewChannelPartnerList.aspx"  class='lab-btn-white'><span class="fa fa-eye" aria-hidden="true"></span> View Channel Partner</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 25px">
     <div class="row">
            <div class="col-lg-6 col-md-6">
               <div class="form-group">
                 <label>Type <span style="color:Red;">*</span></label>
                    <asp:DropDownList ID="drptype" runat="server"  ClientIDMode="Static" 
                       class="form-control" AutoPostBack="True" 
                       onselectedindexchanged="drptype_SelectedIndexChanged">
                    <asp:ListItem>-Select-</asp:ListItem>
                    <asp:ListItem>Lab</asp:ListItem>
                     <asp:ListItem>Other</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
             <div class="col-lg-6 col-md-6">
               <div class="form-group" id="labdrp" runat="server">
                 <label>Select Lab <span style="color:Red;">*</span></label>
                    <asp:DropDownList ID="drplab" runat="server"  ClientIDMode="Static" 
                       class="form-control" AutoPostBack="True" 
                       onselectedindexchanged="drplab_SelectedIndexChanged">
                   
                    </asp:DropDownList>
                </div>
            </div>
            </div>
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Channel Partner Name<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtChnPName"
                        placeholder="Enter Channel Partner Name"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="HRName" runat="server" ControlToValidate="txtChnPName"
                        ForeColor="Red" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid Name" />
                </div>
                <div class="form-group">
                    <label>
                        Channel Partner Code<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtChnnelCode"
                        required placeholder="Enter Channel Partner Code"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>
                        Mobile Number<span style="color: Red">*</span></label>
                    <div class="input-group">
                        <div class="input-group-addon">
                            <i class="fa fa-phone"></i>
                        </div>
                        <asp:TextBox ID="txtmobilenumber" onkeypress="return isNumber(event)" MaxLength="10"
                            runat="server" ClientIDMode="Static" CssClass="form-control" Placeholder="Enter Mobile Number"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Email">
                        Email Id<span style="color: Red">*</span></label>
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                        <asp:TextBox runat="server" ClientIDMode="Static" class="form-control" ID="txtEmail"
                            required placeholder="Email Id"></asp:TextBox>
                    </div>
                    <asp:RegularExpressionValidator ID="EmailValidation" runat="server" ErrorMessage="Invalid Email ID"
                        ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                    </asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Address<span style="color: Red">*</span></label>
                    <asp:TextBox runat="server" ClientIDMode="Static" Style="resize: None;" TextMode="MultiLine"
                        CssClass="form-control" ID="txtAdress" placeholder="Address"></asp:TextBox>
                </div>
                <div class="form-group">
                    <div class="form-group has-feedback">
                        <div class="row">
                            <div class="col-lg-4 col-md-1">
                                <label>
                                    Profile Pic</label></div>
                            <div class="col-lg-6 col-md-6">
                                <asp:FileUpload ID="OrgLogo" CssClass=" btn btn-default" runat="server" /></div>
                        </div>
                    </div>
                </div>
                <div class="box-footer" align="right">
                    <asp:Button ID="BtnSave" runat="server" type="submit" class="fa fa-save btn btn-lg btn-primary"
                        Text="Save" OnClick="BtnSave_Click" />
                    <div class="pad">
                        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

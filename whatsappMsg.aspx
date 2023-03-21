<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master"    AutoEventWireup="true" CodeFile="whatsappMsg.aspx.cs" Inherits="SuperAdmin_whatsappMsg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isNumber(e) {
            var keyCode = (e.which) ? e.which : e.keyCode;

            if (keyCode > 31 && (keyCode < 48 || keyCode > 57)) {
                alert("You can enter only numbers 0 to 9 ");
                return false;
            }
            return true;
        }
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Send Whatsapp Message</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                      
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding:20px">
    	<div class="row clearfix">
            <p style="color:red; margin-left:20px;">Note : ( * ) Denotes Mandatory Fields.</p><br />
        </div>
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Mobile No <span style="color: Red">*</span></label>
                    <asp:TextBox ID="txtmobileNo" CssClass="form-control" MaxLength="10"  onkeypress="return isNumber(event);"  ondrop="return false;" onpaste="return false;" placeholder="Enter Mobile No"  runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>
                        Template <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="drptemplate"  class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" 
                        OnSelectedIndexChanged="drptemplate_SelectedIndexChanged" AutoPostBack="True" >
                      
                    </asp:DropDownList>
                </div>
                
            </div>
              <div class="col-lg-6 col-md-6">
                  <div class="form-group">
                    <label>
                       Remark </label>
                       
                    <asp:TextBox ID="txtremark"  CssClass="form-control"  runat="server" ></asp:TextBox><br />

                      <asp:DataList ID="dt_paramList" runat="server" BackColor="White" 
                          BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                          GridLines="Both" RepeatColumns="1">
                          <AlternatingItemTemplate>
                              <asp:Label ID="Label2" runat="server" Text='<%# Eval("ParamName") %>'></asp:Label>
                              <asp:TextBox ID="TextBox" runat="server" Text='<%# Eval("values") %>'></asp:TextBox>
                              <br />
                          </AlternatingItemTemplate>
                          <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                          <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                          <ItemStyle BackColor="White" ForeColor="#330099" />
                          <ItemTemplate>
                              <asp:Label ID="Label1" runat="server" Text='<%# Eval("paramName") %>'></asp:Label>
                              &nbsp;<asp:TextBox ID="TextBox" runat="server" Text='<%# Eval("values") %>'></asp:TextBox>
                              <br />
                          </ItemTemplate>
                          <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                      </asp:DataList>
                </div>
                 <div class="form-group">
                     <asp:Button ID="btnSendMessage" runat="server" Text="Send Message" style="margin-top:9px;"
                         CssClass="btn btn-success" onclick="btnSendMessage_Click" 
                        />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnparam" runat="server" />
    </div>
    
</asp:Content>

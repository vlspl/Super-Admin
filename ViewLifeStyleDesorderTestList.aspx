<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="ViewLifeStyleDesorderTestList.aspx.cs" Inherits="SuperAdmin_ViewLifeStyleDesorderTestList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script type="text/javascript">
      function deleteitem() {
          if (confirm("Are you sure you want to delete...? ")) {
              return true;
          }
          return false;
      }
    </script>
    <nav class="primary-col-back subheader">
               <div class="container-fluid">
                  <div class="row">
                     <div class="col-sm-6">
                        <h4>Life Style Disorder Test List</h4>
                     </div>
                     <div class="col-sm-6 text-right">
                        <a href="AddLifeStyleTestList.aspx"  class='lab-btn-white'><span class="fa fa-plus" aria-hidden="true"></span> Add Life Style Disorder Test</a>
                     </div>
                  </div>
               </div>
            </nav>
    <div class="box-body" style="padding: 20px">
              <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="form-group">
                    <label>
                        Life Style Disorder Name <span style="color: Red">*</span></label>
                    <asp:DropDownList ID="ddlName" DataTextField="Name" DataValueField="ID" class="form-control select2 select2-hidden-accessible"
                        runat="server" Style="width: 100%" AutoPostBack="True" OnSelectedIndexChanged="ddlName_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <asp:GridView ID="grdviewOrgnization" runat="server"  DataKeyNames="ID" CssClass="table table-bordered table-hover table-striped"
            PageSize="20" AllowPaging="true" AutoGenerateColumns="False" 
            OnRowDeleting="grdviewOrgnization_RowDeleting" 
            >
            <Columns>
               
                <asp:BoundField DataField="sTestCode" HeaderText="Test Code" />
                <asp:BoundField DataField="sTestName" HeaderText="Test Name" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                     <asp:ImageButton ID="img_user" runat="server" CommandName="Delete" OnClientClick="return deleteitem();" ImageUrl='~/images/Active.png'
                        Width="30px" Height="30px" Visible='<%# Eval("IsActive").ToString() == "True" ? true : false %>' ></asp:ImageButton>
                          <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ImageUrl='~/images/Deactive.jpg'
                        Width="30px" Height="30px" Visible='<%# Eval("IsActive").ToString() == "False" ? true : false %>' ></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="pad" style="color: Red">
        <asp:Literal Text="" ID="litErrorMessage" runat="server" /></div>
</asp:Content>


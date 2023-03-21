<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="LabsManagementEditLab.aspx.cs" Inherits="SuperAdmin_LabsManagementEditLab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="header-wrap">
    <div>
        <asp:Label  class="primary-col"  ID="Label1" runat="server" Text="Lab ID"></asp:Label>
        <asp:TextBox ID="LabCode" placeholder="LabID" ReadOnly="true" runat="server"></asp:TextBox>
    </div>
    </div>

    <div class="container-fluid">
        <div class="labregister">
            <div class="header-wrap">
                <div class="row">
                    <div class="col-md-6">
                  <div class="form-group">

        <asp:Label  class="primary-col" ID="Label2" runat="server" Text="Lab Name"></asp:Label>
        <asp:TextBox ID="LabName" placeholder="Lab Name" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="LabnameReq" ControlToValidate="LabName" ErrorMessage=" *"  ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
        </asp:RequiredFieldValidator>
    </div>
               </div>
               <div class="col-md-6">
                  <div class="form-group">

        <asp:Label  class="primary-col" ID="Label3" runat="server" Text="Lab Owner"></asp:Label>
        <asp:TextBox ID="LabManager" placeholder="Lab Owner" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="labManagerReq" ControlToValidate="LabManager" ErrorMessage=" *"  ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
        </asp:RequiredFieldValidator>
 </div>
               </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                  <div class="form-group">

  
        <asp:Label  class="primary-col" ID="Label4"  runat="server" Text="Email Id"></asp:Label>
        <asp:TextBox ID="EmailId" placeholder="Email ID" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="EmailIdReq" ControlToValidate="EmailId" ErrorMessage=" *"  ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="EmailValidation" runat="server" ErrorMessage="Invalid Email ID"
            ControlToValidate="EmailId" ValidationGroup="labregister" Display="Dynamic" ForeColor="Red"
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
        </asp:RegularExpressionValidator>
 </div>
               </div>
               <div class="col-md-6">
                  <div class="form-group">
        <asp:Label  class="primary-col" ID="Label5"  runat="server" Text="Contact Number "></asp:Label>
        <asp:TextBox ID="LabContact" placeholder="Contact Number" MaxLength="10" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="LabContactReq" ControlToValidate="LabContact" ErrorMessage=" *"  ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="Contactnumbervalidation" runat="server" ControlToValidate="LabContact"
            ErrorMessage="Invalid mobile number" ValidationGroup="labregister" Display="Dynamic"
            ForeColor="Red" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
     </div>
               </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                  <div class="form-group">
    
        <asp:Label  class="primary-col" ID="Label6"  runat="server" Text="Lab Address"></asp:Label>
        <asp:TextBox ID="LabAddress" TextMode="MultiLine" placeholder="Address" Rows="5" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="LabAddressReq" ControlToValidate="LabAddress" ErrorMessage=" *"  ValidationGroup="labregister" Display="Dynamic" ForeColor="Red" runat="server">
        </asp:RequiredFieldValidator>
          </div>
               </div>
                </div>
                <div class="row">
                    <div class="text-right">
                  <div class="form-group">
        <asp:Button ID="RegisterLab"  class="lab-btn-primary"  ValidationGroup="labregister" runat="server" Text="Update" OnClick="btn_Update_Click" />
    </div>
               </div>
                </div>
            </div>
        </div>
    </div>

  <script type="text/javascript">
     function Yopopupalert() {
          $('#YoCommonPopup').addClass('in');
          $('#YoCommonPopup').show();
      };


    </script>

    
</asp:Content>


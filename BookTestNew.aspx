<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true" CodeFile="BookTestNew.aspx.cs" Inherits="BookTestNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
<script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
   <script type="text/javascript">
       function showModal() {
           $("#myModal").modal('show');
       }

    </script>

 <script type="text/javascript">

     function goBack() {
         window.history.back()
     }
 </script>
 <style>
 
nav > .nav.nav-tabs{

  border: none;
    color:#fff;
    background:#272e38;
    border-radius:0;

}
nav > div a.nav-item.nav-link,
nav > div a.nav-item.nav-link.active
{
  border: none;
    padding: 18px 25px;
    color:#fff;
    background:#272e38;
    border-radius:0;
}

nav > div a.nav-item.nav-link.active:after
 {
  content: "";
  position: relative;
  bottom: -60px;
  left: -10%;
  border: 15px solid transparent;
  border-top-color: #e74c3c ;
}
.tab-content{
  background: #fdfdfd;
    line-height: 25px;
    border: 1px solid #ddd;
   
    padding:30px 25px;
}

nav > div a.nav-item.nav-link:hover,
nav > div a.nav-item.nav-link:focus
{
  border: none;
    background: #e74c3c;
    color:#fff;
    border-radius:0;
    transition:background 0.20s linear;
}
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Howzu Says</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <asp:Label ID="lblMessage" runat="server"></asp:Label>
      </div>
      <div class="modal-footer">
         <asp:Button ID="btnredirect" class="btn btn-secondary" OnClientClick="goBack()"  runat="server" Text="Close"></asp:Button>
      </div>
    </div>
  </div>  
    </div>
    <asp:UpdatePanel ID="upbooking" runat="server">
    <ContentTemplate>
     
        <div class="wrapper">
      
           <div class="row">
           <div class="col-md-12">
                <div class="col-xs-12 ">
                  <nav>
                    <div class="nav nav-tabs nav-fill" id="nav-tab" role="tablist">
                      <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-PatientList" role="tab" aria-controls="nav-home" aria-selected="true">Patient List</a>
                      <a class="nav-item nav-link" id="nav-profile-tab" data-toggle="tab" href="#nav-DoctorList" role="tab" aria-controls="nav-profile" aria-selected="false">Doctor List</a>
                      <a class="nav-item nav-link" id="nav-contact-tab" data-toggle="tab" href="#nav-TestList" role="tab" aria-controls="nav-contact" aria-selected="false">Test List</a>
                      <a class="nav-item nav-link" id="nav-about-tab" data-toggle="tab" href="#nav-TimeSlot" role="tab" aria-controls="nav-about" aria-selected="false">Select Time Slot</a>
                    </div>
                  </nav>
                  <div class="tab-content py-3 px-3 px-sm-0" id="nav-tabContent">
                    <div class="tab-pane fade show active" id="nav-PatientList" role="tabpanel" aria-labelledby="nav-home-tab" style="padding:10px;">
                       <div class="card mt-2 mb-3">
                                <div class="card-body container">
                                    <h5 class="card-title">
                                        Petient Selected:</h5>
                                    <div class="row pb-2">
                                        <div class="col-md-3 text-size">
                                            ID: 
                                            <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Name:  <asp:Label ID="lblname" runat="server" Text="Label"></asp:Label>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Gender:  <asp:Label ID="lblgenger" runat="server" Text="Label"></asp:Label>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Mobile:  <asp:Label ID="lblmobile" runat="server" Text="Label"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="PatientList">
                              <%--  <ul class="responsive-table">
                                    <li class="table-header">
                                        <div class="col col-1 text-center">
                                            Action</div>
                                        <div class="col col-2 text-center">
                                            Sr. No.</div>
                                        <div class="col col-3 text-center">
                                            Name</div>
                                        <div class="col col-4 text-center">
                                            Gender</div>
                                        <div class="col col-5 text-center">
                                            Mobile</div>
                                    </li>
                                    <div id="page">
                                        <asp:Literal ID="tbodyPatientList" runat="server"></asp:Literal></div>
                                </ul>--%>
                                <div style="overflow:auto; height:500px;">
                                   <asp:GridView runat="server" ID="gvpatientListDetails" AllowPaging="false" 
                PageSize="20" CssClass="table table-hover" 
                AutoGenerateColumns="false" Width="100%" onpageindexchanging="gvpatientListDetails_PageIndexChanging" 
                >
            <HeaderStyle CssClass="headerstyle" />
            <Columns>
              <asp:TemplateField HeaderText="Action" >
                <ItemTemplate>
                    <asp:RadioButton ID="RadioButton_select" AutoPostBack="True" 
                        oncheckedchanged="RadioButton_select_CheckedChanged" runat="server" />
                    
                </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Sr No" ItemStyle-Width="100" >
                <ItemTemplate>
                    <asp:Label ID="lblsrno" runat="server" Text='<%# Bind("sAppUserId") %>'>></asp:Label>
                </ItemTemplate>

<ItemStyle Width="100px"></ItemStyle>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Patient Name" >
                <ItemTemplate>
                    <asp:Label ID="lblsfullName" runat="server" Text='<%# Bind("sFullName") %>'>></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Gender" >
                <ItemTemplate>
                    <asp:Label ID="lblsgender" runat="server" Text='<%# Bind("sGender") %>'>></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Mobile" >
                <ItemTemplate>
                    <asp:Label ID="lblmob" runat="server" Text='<%# Bind("sMobile") %>'>></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
           
            
             
           
            
            </Columns>
            </asp:GridView>
                                </div>
                            </div>
                    </div>
                    <div class="tab-pane fade" id="nav-DoctorList" role="tabpanel" aria-labelledby="nav-profile-tab" style="padding:10px;">
                       <div class="card mt-2 mb-3">
                                <div class="card-body container">
                                    <h5 class="card-title">
                                        Doctor Selected:</h5>
                                    <div class="row pb-2">
                                        <div class="col-md-3 text-size">
                                            ID: <span id="docid"></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Name: <span id="docname"></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Gender: <span id="docgender"></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Mobile: <span id="docmobile"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="DoctorList">
                            <div style="overflow:auto; height:500px;">
                                <asp:GridView runat="server" ID="gridDoctorList" AllowPaging="false" 
                PageSize="20" CssClass="table table-hover" 
                AutoGenerateColumns="false" Width="100%" 
                >
            <HeaderStyle CssClass="headerstyle" />
            <Columns>
              <asp:TemplateField HeaderText="Action" >
                <ItemTemplate>
                   
                    <asp:RadioButton ID="RadioButton_selDoc" AutoPostBack="True" 
                        oncheckedchanged="RadioButton_selDoc_CheckedChanged" runat="server" />
                    
                </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Sr No" ItemStyle-Width="100" >
                <ItemTemplate>
                    <asp:Label ID="lblsrno" runat="server" Text='<%# Bind("sAppUserId") %>'>></asp:Label>
                </ItemTemplate>

<ItemStyle Width="100px"></ItemStyle>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Doctor Name" >
                <ItemTemplate>
                    <asp:Label ID="lblsfullName" runat="server" Text='<%# Bind("sFullName") %>'>></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Gender" >
                <ItemTemplate>
                    <asp:Label ID="lblsgender" runat="server" Text='<%# Bind("sGender") %>'>></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Mobile" >
                <ItemTemplate>
                    <asp:Label ID="lblmob" runat="server" Text='<%# Bind("sMobile") %>'>></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address" >
                <ItemTemplate>
                    <asp:Label ID="lbladd" runat="server" Text='<%# Bind("sAddress") %>'>></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Degree" >
                <ItemTemplate>
                    <asp:Label ID="lblsdegree" runat="server" Text='<%# Bind("sdegree") %>'>></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Specilization" >
                <ItemTemplate>
                    <asp:Label ID="lblsSpecialization" runat="server" Text='<%# Bind("sSpecialization") %>'>></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Clinic" >
                <ItemTemplate>
                    <asp:Label ID="lblsClinic" runat="server" Text='<%# Bind("sClinic") %>'>></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            </Columns>
            </asp:GridView>
                                </div>
                            </div>
                    </div>
                    <div class="tab-pane fade" id="nav-TestList" role="tabpanel" aria-labelledby="nav-contact-tab" style="padding:10px;">
                     
                    </div>
                    <div class="tab-pane fade" id="nav-TimeSlot" role="tabpanel" aria-labelledby="nav-about-tab" style="padding:10px;">
                      Et et consectetur ipsum labore excepteur est proident excepteur ad velit occaecat qui minim occaecat veniam. Fugiat veniam incididunt anim aliqua enim pariatur veniam sunt est aute sit dolor anim. Velit non irure adipisicing aliqua ullamco irure incididunt irure non esse consectetur nostrud minim non minim occaecat. Amet duis do nisi duis veniam non est eiusmod tempor incididunt tempor dolor ipsum in qui sit. Exercitation mollit sit culpa nisi culpa non adipisicing reprehenderit do dolore. Duis reprehenderit occaecat anim ullamco ad duis occaecat ex.
                    </div>
                  </div>
                
                </div>
                </div>
              </div>
    
</div>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


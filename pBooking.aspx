<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true" CodeFile="pBooking.aspx.cs" Inherits="pBooking" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="upone" runat="server">
<ContentTemplate>

    <asp:HiddenField ID="hiddenAppUserId" runat="server" />
    <asp:HiddenField ID="hdntestamount" runat="server" />
    <asp:HiddenField ID="hdnfinalAmt" runat="server" />
    <asp:HiddenField ID="hiddenTestDate" runat="server" />
    <asp:HiddenField ID="hiddenPatientId" runat="server" />
    <asp:HiddenField ID="HiddenDoctorid" runat="server" />
    <asp:HiddenField ID="hiddenAppointmentType" runat="server" />
    <asp:HiddenField ID="hiddenTimeSlot" runat="server" />
    <asp:HiddenField ID="hiddenTestList" runat="server" />
    <asp:HiddenField ID="HFinalAmount" runat="server" />
    <asp:HiddenField ID="hTestPricearray" runat="server" />
  <nav class="navbar navbar-expand-sm bg-light navbar-header"> 
                          <div class="container-fluid">
                            <div class="navbar-title ml-5">
                              <a href="#" class="navbar-brand">Book Test</a>
                            </div>
                          <div class="mr-5">
                          <ul class="navbar-nav ml-auto"> 
                             
                              <li class="nav-item pt-1"> 
                              
                             </li>  
                              <li class="nav-item pt-1"> 
                             
                             </li> 
                              <li class="nav-item pt-1 mr-3"> 
                              <asp:Button ID="btnnext" runat="server" CssClass="btn btn-color" Text="Next" style="display:none;"  onclick="btnnext_Click"></asp:Button>
                              </li>                              
                          </ul> 
                        </div>
                      </div>
                      </nav>
 <br />
<div class="col-12 col-sm-12" style="">  <%--  background: #eeecec;--%>
 <div class="card card-primary card-outline card-outline-tabs">
              <div class="card-header p-0 border-bottom-0">
                <ul class="nav nav-tabs" id="custom-tabs-four-tab" role="tablist">
                  <li class="nav-item ">
                      <asp:LinkButton ID="lnkpatient" CssClass="btn btn-color" runat="server" 
                          onclick="lnkpatient_Click"><i class="fa fa-user"></i>&nbsp;Select Patient</asp:LinkButton>
                   
                  </li>
                  <li class="nav-item">
                    <asp:LinkButton ID="lnkdoctor" CssClass="btn btn-color" runat="server" 
                          onclick="lnkdoctor_Click"><i class="fa fa-user-md"></i>&nbsp;Select Doctor</asp:LinkButton>
                   </li>
                  <li class="nav-item">
                    <asp:LinkButton ID="lnktest" CssClass="btn btn-color" runat="server" 
                          onclick="lnktest_Click"><i class="fa fa-outdent"></i>&nbsp;Select Test</asp:LinkButton>
                  </li>
                  <li class="nav-item">
                    <asp:LinkButton ID="lnkapp" CssClass="btn btn-color" runat="server" 
                          onclick="lnkapp_Click"><i class="far fa-calendar-alt"></i>&nbsp;Select Date</asp:LinkButton>
                   </li>
                </ul>
              </div>
     </div>
     <div id="patientList" runat="server" style="border:1px solid;">
       <div class="col-md-12">
         <div class="card mt-2 mb-3">
                                <div class="card-body container">
                                    <h5 class="card-title">
                                        Patient Selected:</h5>
                                    <div class="row pb-2">
                                        <div class="col-md-2 text-size">
                                            ID: <span ><asp:Label ID="lblpatid" runat="server" Text=""></asp:Label></span>
                                        </div>
                                        <div class="col-md-4 text-size">
                                            Name: <span ><asp:Label ID="lblpatname" runat="server" Text=""></asp:Label></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Gender: <span ><asp:Label ID="lblpatgender" runat="server" Text=""></asp:Label></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Mobile: <span ><asp:Label ID="lblpatmobile" runat="server" Text=""></asp:Label></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
          <div class="col-md-12 ">
                                            Patient Name: <span ><asp:TextBox ID="txtpatientSearch" 
                                                style="margin-top:-28px; margin-left:100px; width:200px;"  
                                                CssClass="form-control" runat="server" AutoPostBack="True" 
                                                ontextchanged="txtpatientSearch_TextChanged"></asp:TextBox></span>
                                                  <a href="#" id="AddPatientbtn" runat="server" data-toggle="modal" data-target="#modalAddPatient" style="margin-top: -34px; float:right;"
    
                                    class="btn btn-color"><span class="fa fa-plus" aria-hidden="true"></span> Add Patient</a>
                                         </div>
           <div class="col-md-12" style="overflow:auto; height:500px;">
          <script language="javascript" type="text/javascript">
              function patientList(rbtselectpatient) {
                  var rdBtn = document.getElementById(rbtselectpatient);
                  var rdBtnList = document.getElementsByTagName("input");
                  for (i = 0; i < rdBtnList.length; i++) {
                      if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                          rdBtnList[i].checked = false;
                      }
                  }
              }
</script>
 <script language="javascript" type="text/javascript">
     function doctorlist(rbtselectDoctor) {
         var rdBtn = document.getElementById(rbtselectDoctor);
         var rdBtnList = document.getElementsByTagName("input");
         for (i = 0; i < rdBtnList.length; i++) {
             if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                 rdBtnList[i].checked = false;
             }
         }
     }
</script>
<script language="javascript" type="text/javascript">
    function Selecttime(rbtselectCalender) {
        var rdBtn = document.getElementById(rbtselectCalender);
        var rdBtnList = document.getElementsByTagName("input");
        for (i = 0; i < rdBtnList.length; i++) {
            if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                rdBtnList[i].checked = false;
            }
        }
    }
</script>
                                            <asp:GridView ID="gridpatientDetails" Width="1040px" 
                                                  CssClass="table" runat="server"  
                                                  EmptyDataText="Patient Data Not Displayed.." 
                                                  AutoGenerateColumns="False" 
                                                ><%--onrowdatabound="gridpatientDetails_RowDataBound" --%>
                                              

                                           <Columns>
                                            <asp:TemplateField HeaderText="Select" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="rbtselectpatient" runat="server" AutoPostBack="True" 
                                                          GroupName="grp"  oncheckedchanged="rbtselectpatient_CheckedChanged" OnClick="javascript:patientList(this.id)" />
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Patient ID" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpatientId" runat="server" Text='<%# Bind("sAppUserId") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                   
                                                
                                               <asp:TemplateField HeaderText="Patient Name"  >
                                                     
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblpname" runat="server" Text='<%# Bind("sFullName") %>'>></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Mobile No">
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpmobile" runat="server" Text='<%# Bind("sMobile") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpaddress" runat="server" Text='<%# Bind("sAddress") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                               
                                           
                                                <asp:TemplateField HeaderText="Gender" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpgender"  runat="server" Text='<%# Bind("sGender") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                  
                                                </asp:TemplateField>
                                                
                                                
                                                 
                                </Columns>
                                          
                                             
                                        </asp:GridView>
                                        </div>
          <br />
       </div>
     </div>
       <div id="DoctorList" runat="server" style="border:1px solid;">
       <div class="col-md-12">
           <div class="card mt-2 mb-3">
                                <div class="card-body container">
                                    <h5 class="card-title">
                                        Doctor Selected:</h5>
                                    <div class="row pb-2">
                                        <div class="col-md-2 text-size">
                                            ID: <span ><asp:Label ID="lbldid" runat="server" Text=""></asp:Label></span>
                                        </div>
                                        <div class="col-md-4 text-size">
                                            Name: <span ><asp:Label ID="lbldName" runat="server" Text=""></asp:Label></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Gender: <span ><asp:Label ID="lbldgender" runat="server" Text=""></asp:Label></span>
                                        </div>
                                        <div class="col-md-3 text-size">
                                            Mobile: <span ><asp:Label ID="lbldmobile" runat="server" Text=""></asp:Label></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                           
                              <div class="col-md-12">
                                            Doctor Name: <asp:TextBox ID="txtdoctorSearch" 
                                                style="margin-top:-28px; margin-left:96px; width:200px;" CssClass="form-control" 
                                                runat="server" AutoPostBack="True" ontextchanged="txtdoctorSearch_TextChanged"></asp:TextBox>
                                                 <a href="#" data-toggle="modal" id="HideAddbtn" runat="server" data-target="#modalAddDoctor" style="    margin-top: -39px; float:right;"   class="btn btn-color"><span class="fa fa-plus" aria-hidden="true"></span> Add Doctor</a>
                                        </div>
                                        <br />
                                        <div class="col-md-12" style="overflow:auto; height:500px;">
                                       
                                            <asp:GridView ID="griddoctorList" Width="1040px" 
                                                  CssClass="table" runat="server"  
                                                  EmptyDataText="Docto Data Not Displayed.." 
                                                  AutoGenerateColumns="False" 
                                                >
                                           <Columns>
                                            <asp:TemplateField HeaderText="Select" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="rbtselectDoctor" runat="server" AutoPostBack="True" 
                                                          GroupName="grp" oncheckedchanged="rbtselectDoctor_CheckedChanged" OnClick="javascript:doctorlist(this.id)"  />
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Doctor ID" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldoctorId" runat="server" Text='<%# Bind("sAppUserId") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                   
                                                
                                               <asp:TemplateField HeaderText="Doctor Name"  >
                                                     
                                                    <ItemTemplate>
                                                       <asp:Label ID="lbldocname" runat="server" Text='<%# Bind("sFullName") %>'>></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Mobile No">
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldmob" runat="server" Text='<%# Bind("sMobile") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldaddress" runat="server" Text='<%# Bind("sAddress") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                               
                                           
                                                <asp:TemplateField HeaderText="Gender" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldgender"  runat="server" Text='<%# Bind("sGender") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                  
                                                </asp:TemplateField>
                                                
                                                
                                                 
                                </Columns>
                                          
                                        </asp:GridView>
                                        </div>
       <br />
       </div>
     </div>
      <div id="testList" runat="server" style="border:1px solid;">
        <div class="col-md-12"><br />
            <asp:HiddenField ID="hdntestId" runat="server" />
            <asp:HiddenField ID="hdntestPrice" runat="server" />
                        <div class="col-md-8">
                          Test: <span >
                            <asp:TextBox ID="txtsearchtestName" 
                                style="margin-top:-28px; margin-left:82px; width:200px;"  
                                CssClass="form-control" runat="server" AutoPostBack="True" ontextchanged="txtsearchtestName_TextChanged" 
                                ></asp:TextBox></span>
                        <div style="overflow:auto; height:400px;">
                        <asp:GridView ID="gridviewTest" Width="1040px" 
                                                  CssClass="table" runat="server"  
                                                  EmptyDataText="Test List Not Displayed.." 
                                                  AutoGenerateColumns="False"  >
                                           <Columns>
                                            <asp:TemplateField HeaderText="Select" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chktestselect" runat="server" AutoPostBack="True" oncheckedchanged="chktestselect_CheckedChanged"    /> 
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Test ID" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltestId" runat="server" Text='<%# Bind("sTestId") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Test Code" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcode" runat="server" Text='<%# Bind("sTestCode") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                   
                                                
                                               <asp:TemplateField HeaderText="Test Name"  >
                                                     
                                                    <ItemTemplate>
                                                       <asp:Label ID="lbltest" runat="server" Text='<%# Bind("sTestName") %>'>></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Profile"  >
                                                     
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblprofileName" runat="server" Text='<%# Bind("sProfileName") %>'>></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Price"  >
                                                     
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblpr" runat="server" Text='<%# Bind("sPrice") %>'>></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                               
                                                 
                                </Columns>
                                          
                                        </asp:GridView>
                                        </div>
                        </div>
                          <div class="col-md-4" style="margin-left: 660px;
    margin-top: -390px; height:400px;">
                          <span style="font-size:20px;">Add Test : (by Name or ID)</span><br />
                          <asp:GridView ID="gridfinalTest"  
                                                  CssClass="table" runat="server"  
                                                  EmptyDataText="Test List Not Displayed.." 
                                                  AutoGenerateColumns="False"  ShowFooter="true" >
                                           <Columns>
                                           <asp:TemplateField HeaderText="Test ID" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltestId1" runat="server" Text='<%# Bind("sTestId") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Test Name" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltestName" runat="server" Text='<%# Bind("sTestName") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                   
                                                
                                               <asp:TemplateField HeaderText="Test Price"  >
                                                     
                                                    <ItemTemplate>
                                                       <asp:Label ID="lbltestprice" runat="server" Text='<%# Bind("sPrice") %>'>></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                               
                                                 
                                </Columns>
                                          
                                        </asp:GridView>
                          </div>
                          <div style="clear:both;"><br /></div>
                    </div>
     </div>
      <div id="appointment" runat="server" style="border:1px solid;">
       <div class="col-md-12"><br />
         <div class="col-md-5 text-size" style="float:right;">
                                              <asp:TextBox ID="txtdate"  CssClass="form-control" style="width:225px;" runat="server"></asp:TextBox>
                                       
                                        <asp:CalendarExtender ID="txtdate_CalendarExtender" 
                          runat="server" Enabled="True" TargetControlID="txtdate" DaysModeTitleFormat="dd-MM-yyyy" 
                                                   Format="dd-MM-yyyy" TodaysDateFormat="dd-MM-yyyy">
                      </asp:CalendarExtender>
             <asp:Button ID="btn_reviewBooking" runat="server" Text="Review Booking" CssClass="btn btn-info"  style="margin-top: -39px;
    margin-left: 243px;" onclick="btn_reviewBooking_Click" />
                   
                                        </div>
                                        <br /><br />
                   <div class="col-md-12" style="overflow:auto; height:450px;">
                                            <asp:GridView ID="gridappointment" Width="1040px" 
                                                  CssClass="table" runat="server"  
                                                  EmptyDataText="Appointment Data Not Displayed.." 
                                                  AutoGenerateColumns="False"  >
                                           <Columns>
                                            <asp:TemplateField HeaderText="Select" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="rbtselectCalender" runat="server" AutoPostBack="True" 
                                                            oncheckedchanged="rbtselectCalender_CheckedChanged" OnClick="javascript:Selecttime(this.id)"   />
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Day" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblday" runat="server" Text='<%# Bind("sDay") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                   
                                                
                                               <asp:TemplateField HeaderText="From"  >
                                                     
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblfrom" runat="server" Text='<%# Bind("sFrom") %>'>></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="To">
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblto" runat="server" Text='<%# Bind("sTo") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="	Appointment Type">
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltype" runat="server" Text='<%# Bind("sAppointmentType") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                               
                                           
                                               
                                                
                                                 
                                </Columns>
                                          
                                        </asp:GridView>
                                        </div>
                                        
                           <br />            
       </div>
     </div>
        <div id="divReview" runat="server">
        <br />
        <div class="col-md-6" style="margin-left:235px;">
        <div class="modal-content" >
                       <%-- <img id="imgloader" src="images/Loader.gif" alt="Loading" class="adjustloader hide" />--%>
                        <div class="modal-header">
                            <h4 class="modal-title">
                                Book test</h4>
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Patient Name :
                                        <label id="lblPatientName" runat="server">
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Gender :
                                        <label id="lblPatientGender"  runat="server">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Mobile :
                                        <label id="lblPatientMobile"  runat="server">
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Test Date :
                                        <label id="lblTestDate"  runat="server">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Time Slot :
                                        <label id="lblTimeSlot"  runat="server">
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        Doctor Name :
                                        <label id="lblDoctorName"  runat="server">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <div class="form-group">
                            <asp:GridView ID="gridrbook" Width="1040px" 
                                                  CssClass="table" runat="server"  
                                                  EmptyDataText="Test List Not Displayed.." 
                                                  AutoGenerateColumns="False"  >
                                           <Columns>
                                          
                                                  <asp:TemplateField HeaderText="Test Name" >
                                                     
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltestName" runat="server" Text='<%# Bind("sTestName") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                   
                                                
                                               <asp:TemplateField HeaderText="Test Price"  >
                                                     
                                                    <ItemTemplate>
                                                       <asp:Label ID="lbltestprice" runat="server" Text='<%# Bind("sPrice") %>'>></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                              
                                                 
                                </Columns>
                                          
                                        </asp:GridView>
                                        <asp:UpdatePanel ID="upamt" runat="server">
                                        <ContentTemplate>
                                        
                                     
                                <table class="table booking table-bordered table-hover" id="tabReviewTestList">
                                    
                                    <tfoot style="text-align: right">
                                        <tr>
                                            <td>
                                                Collection Charge:
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" placeholder="Enter Collection Charge" onkeypress="return isNumber(event)"
                                                    ID="txtCollectionCharge" Style="text-align: right" MaxLength="4" runat="server"
                                                    ClientIDMode="Static" AutoPostBack="True" 
                                                    ontextchanged="txtCollectionCharge_TextChanged"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Final Amount :
                                            </td>
                                            <td>
                                                <label id="lblFinalAmount" runat="server">
                                                </label>
                                            </td>
                                        </tr>
                                    </tfoot>
                                </table>
                                   </ContentTemplate>
                                        </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmBooking" class="btn btn-color" runat="server" Text="Confirm"
                               OnClick="btnConfirmBooking_Click"  ClientIDMode="Static" />
                        </div>
                    </div>
       </div>
        </div>    
     
     <div id="modalAddPatient" class="modal fade mt-5" role="dialog">
                <div class="modal-dialog" >
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                Add Patient</h4>
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Enter Mobile Number" onkeypress="return isNumber(event)"
                                            ID="txtMobile" MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblMobile" class="form-error">
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Enter Full Name *" ID="txtFullName"
                                            runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblFullName" class="form-error">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Enter Email Id" ID="txtEmailId" runat="server"
                                            ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblEmailId" class="form-error">
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList class="form-control" ID="selGender" runat="server" ClientIDMode="Static">
                                            <asp:ListItem Value="select" Selected="True">Select Gender *</asp:ListItem>
                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                        </asp:DropDownList>
                                        <label id="lblGender" class="form-error">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row testDetail">
                                <div class="col-md-6">
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text fa-color" style="color: #91c740;"><i class="fa fa-calendar fa-fa-color"
                                                aria-hidden="true"></i></span>
                                        </div>
                                        <asp:TextBox ID="txtBirthDate" placeholder="Select Birth date *" runat="server" class="form-control"
                                            onchange="AgeCalulation()" ClientIDMode="Static"></asp:TextBox>
                                          <asp:CalendarExtender ID="txtBirthDate_CalendarExtender" 
                          runat="server" Enabled="True" TargetControlID="txtBirthDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                                   Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                      </asp:CalendarExtender>
                                    </div>
                                    <label id="lblBirthDate" class="form-error">
                                    </label>
                                    <label>
                                    </label>
                                   
                                
                                </div>
                              <div class="col-md-6">
                                    <div class="form-group">
                                     <label id="lblage">
                                        <asp:TextBox class="form-control" MaxLength="3" placeholder="Enter Age in Year" 
                                            ID="txtyear" runat="server" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="txtState" class="form-control select2 select2-hidden-accessible"
                                            runat="server">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                            <asp:ListItem Value="Andaman and Nicobar Islands">	Andaman and Nicobar Islands	</asp:ListItem>
                                            <asp:ListItem Value="Andhra Pradesh">	Andhra Pradesh	</asp:ListItem>
                                            <asp:ListItem Value="Arunachal Pradesh">	Arunachal Pradesh	</asp:ListItem>
                                            <asp:ListItem Value="Assam">	Assam	</asp:ListItem>
                                            <asp:ListItem Value="Bihar">	Bihar	</asp:ListItem>
                                            <asp:ListItem Value="Chandigarh">	Chandigarh	</asp:ListItem>
                                            <asp:ListItem Value="Chattisgarh">	Chattisgarh	</asp:ListItem>
                                            <asp:ListItem Value="Dadra & Nagar Haveli and Daman & Diu">	Dadra & Nagar Haveli and Daman & Diu	</asp:ListItem>
                                            <asp:ListItem Value="Delhi">	Delhi	</asp:ListItem>
                                            <asp:ListItem Value="Goa">	Goa	</asp:ListItem>
                                            <asp:ListItem Value="Gujarat">	Gujarat	</asp:ListItem>
                                            <asp:ListItem Value="Haryana">	Haryana	</asp:ListItem>
                                            <asp:ListItem Value="Himachal Pradesh">	Himachal Pradesh	</asp:ListItem>
                                            <asp:ListItem Value="Jammu & Kashmir">	Jammu & Kashmir	</asp:ListItem>
                                            <asp:ListItem Value="Jharkhand">	Jharkhand	</asp:ListItem>
                                            <asp:ListItem Value="Karnataka">	Karnataka	</asp:ListItem>
                                            <asp:ListItem Value="Kerala">	Kerala	</asp:ListItem>
                                            <asp:ListItem Value="Ladakh">	Ladakh	</asp:ListItem>
                                            <asp:ListItem Value="Lakshadweep">	Lakshadweep	</asp:ListItem>
                                            <asp:ListItem Value="MadhyaPradesh">	MadhyaPradesh	</asp:ListItem>
                                            <asp:ListItem Value="Maharashtra">	Maharashtra	</asp:ListItem>
                                            <asp:ListItem Value="Manipur">	Manipur	</asp:ListItem>
                                            <asp:ListItem Value="Meghalaya">	Meghalaya	</asp:ListItem>
                                            <asp:ListItem Value="Mizoram">	Mizoram	</asp:ListItem>
                                            <asp:ListItem Value="Nagaland">	Nagaland	</asp:ListItem>
                                            <asp:ListItem Value="Odisha">	Odisha	</asp:ListItem>
                                            <asp:ListItem Value="Puducherry">	Puducherry	</asp:ListItem>
                                            <asp:ListItem Value="Punjab">	Punjab	</asp:ListItem>
                                            <asp:ListItem Value="Rajasthan">	Rajasthan	</asp:ListItem>
                                            <asp:ListItem Value="Sikkim">	Sikkim	</asp:ListItem>
                                            <asp:ListItem Value="Tamil Nadu">	Tamil Nadu	</asp:ListItem>
                                            <asp:ListItem Value="Telangana">	Telangana	</asp:ListItem>
                                            <asp:ListItem Value="Tripura">	Tripura	</asp:ListItem>
                                            <asp:ListItem Value="Uttar Pradesh">	Uttar Pradesh	</asp:ListItem>
                                            <asp:ListItem Value="Uttrakhand">	Uttrakhand	</asp:ListItem>
                                            <asp:ListItem Value="West Bengal">	West Bengal	</asp:ListItem>
                                        </asp:DropDownList>
                                        <label id="lblState" class="form-error">
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" placeholder="Enter Pincode *" onkeypress="return isNumber(event)"
                                            ID="txtPincode" MaxLength="6" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblPincode" class="form-error">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" id="">
                                        <asp:TextBox class="form-control" Style="display: none" placeholder="Country" Text="India"
                                            ID="txtCountry" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblCountry" class="form-error hide">
                                        </label>
                                        <asp:TextBox class="form-control" placeholder="Enter City *" ID="txtCity" runat="server"
                                            ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblCity" class="form-error">
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group" id="address">
                                        <asp:TextBox class="form-control" placeholder="Enter Address *" ID="txtAddress" TextMode="MultiLine"
                                            Rows="2" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <label id="lblAddress" class="form-error">
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="upp" runat="server">
                            <ContentTemplate>
                                <div class="modal-footer">
                                    <asp:Button ID="btnAdd" class="btn btn-color" runat="server" Text="Submit" OnClientClick="javascript:return addPatientValidate()"
                                        OnClick="btnAdd_Click" ClientIDMode="Static" />
                                </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upp">
                            <ProgressTemplate>
                                <div class="text form_loader">
                                    <img src="images/Loader.gif" alt="Loading">
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
            </div>

            <div id="modalAddDoctor" class="modal fade " role="dialog">
                <div class="modal-dialog" >
                    <!-- Modal content-->
                    <div class="modal-content" style="width:582px;">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                Add Doctor</h4>
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class=" box box-primary">
                            <div class="box-header with-border">
                                <div class="modal-body">
                                    <asp:HiddenField ID="hiddenAction1" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hiddenAppUserId1" runat="server" ClientIDMode="Static" />
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Enter Mobile Number *" ID="txtMobile1"
                                                    onkeypress="return isNumber(event)" MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblMobile1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Enter Email Id" ID="txtEmailId1" runat="server"
                                                    ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblEmailId1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="input-group mb-3">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text fa-color" style="color: #91c740;"><i class="fa fa-calendar fa-fa-color"
                                                        aria-hidden="true"></i></span>
                                                </div>
                                                <asp:TextBox ID="txtBirthDate1" class="form-control" placeholder="Birth date (dd/mm/yyyy)"
                                                    runat="server" onchange="AgeDoctorCalulation()" ClientIDMode="Static"></asp:TextBox>
                                                 <asp:CalendarExtender ID="txtBirthDate1_CalendarExtender" 
                          runat="server" Enabled="True" TargetControlID="txtBirthDate1" DaysModeTitleFormat="dd/MM/yyyy" 
                                                   Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                      </asp:CalendarExtender>
                                            </div>
                                            <label id="lblBirthDate1" class="form-error">
                                            </label>
                                            <label>
                                            </label>
                                            <label id="lblage1">
                                            </label>
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Degree *" ID="txtDegree1" runat="server"
                                                    ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblDegree1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <%-- <asp:TextBox class="form-control" placeholder="Specialization" ID="txtSpecialization1"
                                                    runat="server" ClientIDMode="Static"></asp:TextBox>--%>
                                                <asp:DropDownList class="form-control" ID="txtSpecialization1" runat="server" ClientIDMode="Static">
                                                    <asp:ListItem Value="select" Selected="True">Select Specialization *</asp:ListItem>
                                                    <asp:ListItem Value="Cardiologist">Cardiologist</asp:ListItem>
                                                    <asp:ListItem Value="Gastroenterologist">Gastroenterologist</asp:ListItem>
                                                    <asp:ListItem Value="Gynecologist">Gynecologist</asp:ListItem>
                                                    <asp:ListItem Value="Nephrologist">Nephrologist</asp:ListItem>
                                                    <asp:ListItem Value="Neurologist">Neurologist</asp:ListItem>
                                                    <asp:ListItem Value="Ophthalmologist">Ophthalmologist</asp:ListItem>
                                                    <asp:ListItem Value="Orthopedic">Orthopedic</asp:ListItem>
                                                    <asp:ListItem Value="Urologist">Urologist</asp:ListItem>
                                                    <asp:ListItem Value="Physician">Physician</asp:ListItem>
                                                    <asp:ListItem Value="Pulmonologist">Pulmonologist</asp:ListItem>
                                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                                </asp:DropDownList>
                                                <label id="lblSpecialization1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Clinic *" ID="txtClinic1" runat="server"
                                                    ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblClinic1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="col-lg-6 hide">
                                                <div class="form-group">
                                                    <asp:TextBox class="form-control" placeholder="Country" style="    margin-left: -16px;
    width: 254px;" ID="txtCountry1" runat="server"
                                                        ClientIDMode="Static"></asp:TextBox>
                                                    <label id="lblCountry1" class="form-error">
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Enter Full Name *" ID="txtFullName1"
                                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblFullName1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:DropDownList class="form-control" ID="selGender1" runat="server" ClientIDMode="Static">
                                                    <asp:ListItem Value="select" Selected="True">Select Gender *</asp:ListItem>
                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                </asp:DropDownList>
                                                <label id="Label1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Address *" ID="txtAddress1" TextMode="MultiLine"
                                                    Rows="3" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblAddress1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:DropDownList ID="txtState1" class="form-control select2 select2-hidden-accessible"
                                                    runat="server">
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Value="Andaman and Nicobar Islands">	Andaman and Nicobar Islands	</asp:ListItem>
                                                    <asp:ListItem Value="Andhra Pradesh">	Andhra Pradesh	</asp:ListItem>
                                                    <asp:ListItem Value="Arunachal Pradesh">	Arunachal Pradesh	</asp:ListItem>
                                                    <asp:ListItem Value="Assam">	Assam	</asp:ListItem>
                                                    <asp:ListItem Value="Bihar">	Bihar	</asp:ListItem>
                                                    <asp:ListItem Value="Chandigarh">	Chandigarh	</asp:ListItem>
                                                    <asp:ListItem Value="Chattisgarh">	Chattisgarh	</asp:ListItem>
                                                    <asp:ListItem Value="Dadra & Nagar Haveli and Daman & Diu">	Dadra & Nagar Haveli and Daman & Diu	</asp:ListItem>
                                                    <asp:ListItem Value="Delhi">	Delhi	</asp:ListItem>
                                                    <asp:ListItem Value="Goa">	Goa	</asp:ListItem>
                                                    <asp:ListItem Value="Gujarat">	Gujarat	</asp:ListItem>
                                                    <asp:ListItem Value="Haryana">	Haryana	</asp:ListItem>
                                                    <asp:ListItem Value="Himachal Pradesh">	Himachal Pradesh	</asp:ListItem>
                                                    <asp:ListItem Value="Jammu & Kashmir">	Jammu & Kashmir	</asp:ListItem>
                                                    <asp:ListItem Value="Jharkhand">	Jharkhand	</asp:ListItem>
                                                    <asp:ListItem Value="Karnataka">	Karnataka	</asp:ListItem>
                                                    <asp:ListItem Value="Kerala">	Kerala	</asp:ListItem>
                                                    <asp:ListItem Value="Ladakh">	Ladakh	</asp:ListItem>
                                                    <asp:ListItem Value="Lakshadweep">	Lakshadweep	</asp:ListItem>
                                                    <asp:ListItem Value="MadhyaPradesh">	MadhyaPradesh	</asp:ListItem>
                                                    <asp:ListItem Value="Maharashtra">	Maharashtra	</asp:ListItem>
                                                    <asp:ListItem Value="Manipur">	Manipur	</asp:ListItem>
                                                    <asp:ListItem Value="Meghalaya">	Meghalaya	</asp:ListItem>
                                                    <asp:ListItem Value="Mizoram">	Mizoram	</asp:ListItem>
                                                    <asp:ListItem Value="Nagaland">	Nagaland	</asp:ListItem>
                                                    <asp:ListItem Value="Odisha">	Odisha	</asp:ListItem>
                                                    <asp:ListItem Value="Puducherry">	Puducherry	</asp:ListItem>
                                                    <asp:ListItem Value="Punjab">	Punjab	</asp:ListItem>
                                                    <asp:ListItem Value="Rajasthan">	Rajasthan	</asp:ListItem>
                                                    <asp:ListItem Value="Sikkim">	Sikkim	</asp:ListItem>
                                                    <asp:ListItem Value="Tamil Nadu">	Tamil Nadu	</asp:ListItem>
                                                    <asp:ListItem Value="Telangana">	Telangana	</asp:ListItem>
                                                    <asp:ListItem Value="Tripura">	Tripura	</asp:ListItem>
                                                    <asp:ListItem Value="Uttar Pradesh">	Uttar Pradesh	</asp:ListItem>
                                                    <asp:ListItem Value="Uttrakhand">	Uttrakhand	</asp:ListItem>
                                                    <asp:ListItem Value="West Bengal">	West Bengal	</asp:ListItem>
                                                </asp:DropDownList>
                                                <label id="lblState1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="City*" Text="Pune" ID="txtCity1" runat="server"
                                                    ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblCity1" class="form-error">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox class="form-control" placeholder="Pincode *" ID="txtPincode1" onkeypress="return isNumber(event)"
                                                    MaxLength="6" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                <label id="lblPincode1" class="form-error">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="uppp1" runat="server">
                            <ContentTemplate>
                                <div class="modal-footer">
                                    <asp:Button ID="btnAddDoctor" class="btn btn-color" runat="server" Text="Submit"
                                        OnClientClick="javascript:return addDoctorValidateInBookTest()" OnClick="btnAddDoctor_Click"
                                        ClientIDMode="Static" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="uppp1">
                            <ProgressTemplate>
                                <div class="text form_loader">
                                    <img src="images/Loader.gif" alt="Loading">
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
            </div>
                </div>
          </label>
          </ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>


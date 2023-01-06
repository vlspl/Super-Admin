<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true" CodeFile="GlobalSearch.aspx.cs" Inherits="GlobalSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="wrapper">
      <div id="content">
         <nav class="primary-col-back subheader">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-sm-6">
                     <h4>Search List</h4>
                  </div>                 
               </div>
            </div>
         </nav>
         <!-- search by modal -->
         <!-- progress status bae -->
         <div class="header-wrap">
          <div class="approval">
          <div class="row">

           <div class="col-sm-2">
            <input type="radio" class="radio-custom" id="rdoAll" name="nameActive" value="All" clientidmode="Static"  checked="checked">
            <label for="radio-5" class="radio-custom-label">All</label>
           </div>

          <div class="col-sm-2">
            <input type="radio" class="radio-custom" id="rdoPatients" name="nameActive" value="Patients" clientidmode="Static">
            <label for="radio-1" class="radio-custom-label">Patients</label>
          </div>

           <div class="col-sm-2">
            <input type="radio" class="radio-custom" id="rdoDoctors" name="nameActive" value="Doctors" clientidmode="Static">
            <label for="radio-2" class="radio-custom-label">Doctors</label>
           </div>

           <div class="col-sm-3">
            <input type="radio" class="radio-custom" id="rdoTestList" name="nameActive" value="TestList" clientidmode="Static">
            <label for="radio-4" class="radio-custom-label">Test List</label>
           </div>
      </div>
        </div>
         </div>
         <!-- wrapper content -->
         <div class="wrappercontent" id="patientlist">
            <table class="table text-center booking table-bordered table-hover">
               <thead>
                  <tr>
                     <th>App ID</th>
                     <th>Name</th>
                     <th>Contact No</th>
                     <th>Email</th>
                     <th>Address</th>
                     <th>Role</th>
                     <th>Country</th>
                     <th>City</th>
                  </tr>
               </thead>
               <tbody id="tbodyAllpatientlist" runat="server" clientidmode="Static">
               </tbody>
            </table>
         </div>

         <div class="wrappercontent" id="DoctorsList">
            <table class="table text-center booking table-bordered table-hover">
               <thead>
                  <tr>
                     <th>App ID</th>
                     <th>Name</th>
                     <th>Contact No</th>
                     <th>Email</th>
                     <th>Address</th>
                     <th>Role</th>
                     <th>Country</th>
                     <th>City</th>
                  </tr>
               </thead>
               <tbody id="tbodyAllDoctorsList" runat="server" clientidmode="Static">  
               </tbody>
            </table>
         </div>

         <div class="wrappercontent" id="TestList">
            <table class="table text-center booking table-bordered table-hover">
               <thead>
                  <tr>
                     <th>Test Code</th>
                     <th>Test Name</th>
                     <th>Test Userful For</th>
                  </tr>
               </thead>
               <tbody id="tbodyAllTestList" runat="server" clientidmode="Static"> 
               </tbody>
            </table>
         </div>
      </div>
   </div>

<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript">
    $(document).ready(function () {

        $('#patientlist').removeClass("hide");
        $('#DoctorsList').removeClass("hide");
        $('#LabUsersList').removeClass("hide");
        $('#TestList').removeClass("hide");

        var rows = $("#tbodyAllLabList").find("tr").hide();
        rows.filter(":contains('Active')").show();
        $("#rdoPatients").change(function () {
            $('#patientlist').removeClass("hide");
            $('#DoctorsList').addClass("hide");
            $('#LabUsersList').addClass("hide");
            $('#TestList').addClass("hide");
        });

        $("#rdoDoctors").change(function () {
            $('#patientlist').addClass("hide");
            $('#DoctorsList').removeClass("hide");
            $('#LabUsersList').addClass("hide");
            $('#TestList').addClass("hide");
        });

        $("#rdoTestList").change(function () {
            $('#patientlist').addClass("hide");
            $('#DoctorsList').addClass("hide");
            $('#LabUsersList').addClass("hide");
            $('#TestList').removeClass("hide");
        });

        $("#rdoAll").change(function () {
            $('#patientlist').removeClass("hide");
            $('#DoctorsList').removeClass("hide");
            $('#LabUsersList').removeClass("hide");
            $('#TestList').removeClass("hide");
        });

        $("#rdoInactive").change(function () {
            var rows = $("#tbodyAllLabList").find("tr").hide();
            rows.filter(":contains('Inactive')").show();
        });
    });
</script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/accessControlMaster.master" AutoEventWireup="true" CodeFile="GlobalSearch.aspx.cs" Inherits="SuperAdmin_GlobalSearch" %>

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
            <input type="radio" class="radio-custom" id="rdoLabUsers" name="nameActive" value="LabUsers" clientidmode="Static">
            <label for="radio-3" class="radio-custom-label">Lab Users</label>
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
            <table class="table booking">
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
                  <%--<tr>
                     <td>#LAB0231</td>
                     <td>Pooja Sharma</td>
                     <td>Lipid Profile</td>
                     <td>Rahul Gahilod</td>
                     <td><i class="fa fa-inr" aria-hidden="true"></i> 270</td>
                     <td><i class="fa fa-user fa-lg" aria-hidden="true"></i></td>
                     <td><a href="" class="lab-btn-primary">Confirm</a><a href="details.html" class="lab-btn-secondary">View Details</a></td>
                  </tr>       --%>          
               </tbody>
            </table>
            <%--<div class="pagination">
               <a href="#" style="border-radius: 20px;padding: 8px 14px 8px 8px;"><i class="fa fa-arrow-circle-left fa-lg" aria-hidden="true"></i> PREV</a>
               <a href="#">1</a>
               <a class="active" href="#">2</a>
               <a href="#">3</a>
               <a href="#">4</a>
               <a href="#">5</a>
               <a href="#">6</a>
               <a href="#" style="border-radius: 20px;padding: 8px 8px 8px 14px;">NEXT <i class="fa fa-arrow-circle-right fa-lg" aria-hidden="true"></i></a>
            </div>--%>
         </div>


         <div class="wrappercontent" id="DoctorsList">
            <table class="table booking">
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
                  <%--<tr>
                     <td>#LAB0231</td>
                     <td>Pooja Sharma</td>
                     <td>Lipid Profile</td>
                     <td>Rahul Gahilod</td>
                     <td><i class="fa fa-inr" aria-hidden="true"></i> 270</td>
                     <td><i class="fa fa-user fa-lg" aria-hidden="true"></i></td>
                     <td><a href="" class="lab-btn-primary">Confirm</a><a href="details.html" class="lab-btn-secondary">View Details</a></td>
                  </tr>       --%>          
               </tbody>
            </table>
            <%--<div class="pagination">
               <a href="#" style="border-radius: 20px;padding: 8px 14px 8px 8px;"><i class="fa fa-arrow-circle-left fa-lg" aria-hidden="true"></i> PREV</a>
               <a href="#">1</a>
               <a class="active" href="#">2</a>
               <a href="#">3</a>
               <a href="#">4</a>
               <a href="#">5</a>
               <a href="#">6</a>
               <a href="#" style="border-radius: 20px;padding: 8px 8px 8px 14px;">NEXT <i class="fa fa-arrow-circle-right fa-lg" aria-hidden="true"></i></a>
            </div>--%>
         </div>


         <div class="wrappercontent" id="LabUsersList">
            <table class="table booking">
               <thead>
                  <tr>
                     <th>Lab ID</th>
                     <th>Lab Name</th>
                     <th>Lab Address</th>
                     <th>Status</th>
                     <th>Contact Number</th>
                     <th>Action 1</th>
                     <th>Action 2</th>
                     <th>Action 3</th>
                  </tr>
               </thead>
               <tbody id="tbodyAllLabUsers" runat="server" clientidmode="Static">                  
                  <%--<tr>
                     <td>#LAB0231</td>
                     <td>Pooja Sharma</td>
                     <td>Lipid Profile</td>
                     <td>Rahul Gahilod</td>
                     <td><i class="fa fa-inr" aria-hidden="true"></i> 270</td>
                     <td><i class="fa fa-user fa-lg" aria-hidden="true"></i></td>
                     <td><a href="" class="lab-btn-primary">Confirm</a><a href="details.html" class="lab-btn-secondary">View Details</a></td>
                  </tr>       --%>          
               </tbody>
            </table>
            <%--<div class="pagination">
               <a href="#" style="border-radius: 20px;padding: 8px 14px 8px 8px;"><i class="fa fa-arrow-circle-left fa-lg" aria-hidden="true"></i> PREV</a>
               <a href="#">1</a>
               <a class="active" href="#">2</a>
               <a href="#">3</a>
               <a href="#">4</a>
               <a href="#">5</a>
               <a href="#">6</a>
               <a href="#" style="border-radius: 20px;padding: 8px 8px 8px 14px;">NEXT <i class="fa fa-arrow-circle-right fa-lg" aria-hidden="true"></i></a>
            </div>--%>
         </div>


         <div class="wrappercontent" id="TestList">
            <table class="table booking">
               <thead>
                  <tr>
                     <th>Lab ID</th>
                     <th>Lab Name</th>
                     <th>Lab Address</th>
                     <th>Status</th>
                  </tr>
               </thead>
               <tbody id="tbodyAllTestList" runat="server" clientidmode="Static">                  
                  <%--<tr>
                     <td>#LAB0231</td>
                     <td>Pooja Sharma</td>
                     <td>Lipid Profile</td>
                     <td>Rahul Gahilod</td>
                     <td><i class="fa fa-inr" aria-hidden="true"></i> 270</td>
                     <td><i class="fa fa-user fa-lg" aria-hidden="true"></i></td>
                     <td><a href="" class="lab-btn-primary">Confirm</a><a href="details.html" class="lab-btn-secondary">View Details</a></td>
                  </tr>       --%>          
               </tbody>
            </table>
            <%--<div class="pagination">
               <a href="#" style="border-radius: 20px;padding: 8px 14px 8px 8px;"><i class="fa fa-arrow-circle-left fa-lg" aria-hidden="true"></i> PREV</a>
               <a href="#">1</a>
               <a class="active" href="#">2</a>
               <a href="#">3</a>
               <a href="#">4</a>
               <a href="#">5</a>
               <a href="#">6</a>
               <a href="#" style="border-radius: 20px;padding: 8px 8px 8px 14px;">NEXT <i class="fa fa-arrow-circle-right fa-lg" aria-hidden="true"></i></a>
            </div>--%>
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

//            var rows = $("#tbodyAllLabList").find("tr").hide();
//            rows.filter(":contains('Active')").show();
        });


        $("#rdoDoctors").change(function () {
            $('#patientlist').addClass("hide");
            $('#DoctorsList').removeClass("hide");
            $('#LabUsersList').addClass("hide");
            $('#TestList').addClass("hide");

            //            var rows = $("#tbodyAllLabList").find("tr").hide();
            //            rows.filter(":contains('Active')").show();
        });


        $("#rdoLabUsers").change(function () {
            $('#patientlist').addClass("hide");
            $('#DoctorsList').addClass("hide");
            $('#LabUsersList').removeClass("hide");
            $('#TestList').addClass("hide");

            //            var rows = $("#tbodyAllLabList").find("tr").hide();
            //            rows.filter(":contains('Active')").show();
        });


        $("#rdoTestList").change(function () {
            $('#patientlist').addClass("hide");
            $('#DoctorsList').addClass("hide");
            $('#LabUsersList').addClass("hide");
            $('#TestList').removeClass("hide");

            //            var rows = $("#tbodyAllLabList").find("tr").hide();
            //            rows.filter(":contains('Active')").show();
        });



        $("#rdoAll").change(function () {
            $('#patientlist').removeClass("hide");
            $('#DoctorsList').removeClass("hide");
            $('#LabUsersList').removeClass("hide");
            $('#TestList').removeClass("hide");

            //            var rows = $("#tbodyAllLabList").find("tr").hide();
            //            rows.filter(":contains('Active')").show();
        });

        $("#rdoInactive").change(function () {
            var rows = $("#tbodyAllLabList").find("tr").hide();
            rows.filter(":contains('Inactive')").show();
        });
    });
</script>
</asp:Content>


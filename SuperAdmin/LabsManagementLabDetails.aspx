<%@ Page Title="" Language="C#" MasterPageFile="~/SuperAdmin/accessControlMaster.master" AutoEventWireup="true" CodeFile="LabsManagementLabDetails.aspx.cs" Inherits="SuperAdmin_LabsManagementLabDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style>
    
#easyPaginate {width:300px;}
#easyPaginate img {display:block;margin-bottom:10px;}
.easyPaginateNav a {padding:5px;}
.easyPaginateNav a.current {font-weight:bold;text-decoration:underline;}
.easyPaginateNav { width: 100% !important; }
</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



   <ul class="nav nav-tabs ">
    <li class="active"><a data-toggle="tab" href="#home">Patient</a></li>
    <li><a data-toggle="tab" href="#menu1">Doctors</a></li>
    <li><a data-toggle="tab" href="#menu2">Test List</a></li>
    <li><a data-toggle="tab" href="#menu3">Lab Info</a></li>
  </ul>

<div class="tab-content">

    <div id="home" class="tab-pane fade in active">
            <div class="filterbar">
      <h3>Patient</h3>
    </div>

      <div class="wrappercontent">	
			<table class="table booking" id="tabPatientList" >

             <thead>
                  <tr>
                     <th>Patient ID</th>
                        <th>Patient Name</th>
                        <th>Gender</th>
                        <th>Birth Date</th>
                        <th>User Type</th>
                        <th>Contact Number</th>
                        <th>Action 1</th>
                        <th>Action 2</th>
                  </tr>
               </thead>

				<tbody id="tbodyPatientList" runat="server" clientidmode="Static">
				</tbody>
			</table>
		</div> 
    </div>


    <div id="menu1" class="tab-pane fade">
        <div class="filterbar">
      <h3>Doctors</h3>
    </div>
        <div class="wrappercontent">
            <table class="table booking" id="tabDoctorList" >

                  <thead>
                 <tr>
                                       <th>Doctor Id</th>
                                       <th>Name</th>
                                       <th>Gender</th>
                                       <th>Mobile</th>
                                       <th>Address</th>
                                       <th>Degree</th>
                                       <th>Specialization</th>
                                       <th>Clinic</th>
                 </tr>
               </thead>

               <tbody id="tbodyDoctorList" runat="server" clientidmode="Static">
               </tbody>
            </table>
         </div>
    </div>


    <div id="menu2" class="tab-pane fade">
        <div class="filterbar">
      <h3>Test List</h3>
</div>

        <div class="wrappercontent">
       <table class="table  table-responsive booking" id="sortTestList">
                     <thead>
                        <tr>
                           <th>Test Code</th>
                           <th>Test Name</th>
                           <th>Profile</th>
                           <th>Section</th>
                           <th>Price</th>
                           <th></th>
                        </tr>
                     </thead>
                     <tbody id="tbodyMyTestList" runat="server" clientidmode="Static">
                        <%--<tr>
                           <th>Test Code</th>
                           <th>Test Name</th>
                           <th>Profile</th>
                           <th>Section</th>
                           <th>Price</th>
                           <th></th>
                        </tr>
                        <tr>
                           <td>CBC</td>
                           <td></td>
                           <td>Biochemistry</td>
                           <td>Biochemistry</td>
                           <td><a href="test-listdetail.html" class="lab-btn-default">View</a></td>
                        </tr>--%>
                     </tbody>
                  </table>
                  </div>

    </div>


    <div id="menu3" class="tab-pane fade">
            <div class="filterbar">
      <h3>Lab Info</h3>
</div>

      <div id="Div1">
         <nav class="primary-col-back subheader">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-sm-6">
                     <h4>Lab Details</h4>
                  </div>
             
               </div>
            </div>
         </nav>
         
         <!-- Lab Details -->
         <div class="container-fluid lab-details">
          <div class="row">
            <div class="col-md-2">
              <p>Lab Id :</p>
            </div>
            <div class="col-md-10">
              <span id="spanLabId" runat="server" clientidmode="Static"></span>
            </div>
          </div>
          <div class="row">
            <div class="col-md-2">
              <p>Lab Name :</p>
            </div>
            <div class="col-md-10">
              <span id="spanLabName" runat="server" clientidmode="Static"></span>
            </div>
          </div>
          <div class="row">
            <div class="col-md-2">
              <p>Lab Logo :</p>
            </div>
            <div class="col-md-10">
              <span id="spanLogo" runat="server" clientidmode="Static"></span>
            </div>
          </div>
  
          <div class="row">
            <div class="col-md-2">
              <p>Lab Details :</p>
            </div>
            <div class="col-md-10">
              <span id="spanLabDetails" runat="server" clientidmode="Static"></span>
            </div>
          </div>

           <%-- <p>Lab Id : <span id="spanLabId" runat="server" clientidmode="Static"></span></p>
           <p>Lab Name : <span id="spanLabName" runat="server" clientidmode="Static"></span></p>
           <p>Lab Logo : <span id="spanLogo" runat="server" clientidmode="Static"></span></p>
           <p>Lab Pics : <span id="spanImages" runat="server" clientidmode="Static"></span></p>
           <p>Lab Details : <span id="spanLabDetails" runat="server" clientidmode="Static"></span></p> --%>
         </div>
      </div>


       <div id="contentpage">
         <nav class="primary-col-back subheader">
            <div class="container-fluid">
               <div class="row">
                  <div class="col-sm-6">
                     <h4>Lab Contact</h4>
                  </div>
           
               </div>
            </div>
         </nav>

         <!-- Lab Contact details -->
         <div class="container-fluid lab-details">
          <div class="row">
            <div class="col-md-2">
              <p>Email :</p>
            </div>
            <div class="col-md-10">
              <span id="spanLabEmail" runat="server"></span>
            </div>
          </div>
          <div class="row">
            <div class="col-md-2">
              <p>Contact :</p>
            </div>
            <div class="col-md-10">
              <span id="spanLabContact" runat="server"></span>
            </div>
          </div>
          <div class="row">
            <div class="col-md-2">
              <p>Lab Address :</p>
            </div>
            <div class="col-md-10">
              <span id="spanLabAddress" runat="server"></span>
            </div>
          </div>
          
           <%-- <p>Email :<span id="spanLabEmail" runat="server"></span></p>
           <p>Contact: <span id="spanLabContact" runat="server"></span></p>
           <p>Lab Address : <span id="spanLabAddress" runat="server"></span></p> --%>
           <%--<p>Lab Details Location:</p>
           <script src='https://maps.googleapis.com/maps/api/js?v=3.exp'></script><div style='overflow:hidden;height:440px;width:700px;'><div id='gmap_canvas' style='height:250px;width:700px;'></div><style>#gmap_canvas img{max-width:none!important;background:none!important}</style></div><script type='text/javascript'>                                                                                                                                                                                                                                                                                                    function init_map() { var myOptions = { zoom: 10, center: new google.maps.LatLng(19.0437469, 72.84464750000006), mapTypeId: google.maps.MapTypeId.ROADMAP }; map = new google.maps.Map(document.getElementById('gmap_canvas'), myOptions); marker = new google.maps.Marker({ map: map, position: new google.maps.LatLng(19.0437469, 72.84464750000006) }); infowindow = new google.maps.InfoWindow({ content: '<strong>Title</strong><br>inez,tower<br>' }); google.maps.event.addListener(marker, 'click', function () { infowindow.open(map, marker); }); infowindow.open(map, marker); } google.maps.event.addDomListener(window, 'load', init_map);</script>--%>
         </div>
      </div>



        <div class="container-fluid">
        <div class="ir-table table-responsive" id="pagination">
            <table class="table labcalender" id="tabLabSlots">
                <tbody id="tbodyLabSlots" runat="server"></tbody>
            </table>
        </div>
        </div>


    </div>



         

  </div>



  <%--start Pagination and sorting data --%>
      <script src="js/jquery.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <script type="text/javascript" src="js/jquery.easyPaginate.js"></script>

    <script>

        var tabname = "";

        $('#tabPatientList').datatable({
//        pageSize: 10,
        sort: [true, true, false],
        filters: [true, true, 'select', true, false, true, false, false],
        filterText: 'Type to filter... ',
        onChange: function (old_page, new_page) {
            console.log('changed from ' + old_page + ' to ' + new_page);
        }
    });


    $('#tabDoctorList').datatable({
//        pageSize: 10,
        sort: [true, false, false],
        filters: [true, true, 'select', true, true, true,true, true],
        filterText: 'Type to filter... ',
        onChange: function (old_page, new_page) {
            console.log('changed from ' + old_page + ' to ' + new_page);
        }
    });


    $('#sortTestList').datatable({
//        pageSize: 10,
        sort: [true, true, false],
        filters: [true, true, true, true, true],
        filterText: 'Type to filter... ',
        onChange: function (old_page, new_page) {
            console.log('changed from ' + old_page + ' to ' + new_page);
        }
    });


    </script>


      <%--end Pagination and sorting data --%>


              <script type="text/javascript">

                  $('#tbodyPatientList').easyPaginate({
                      paginateElement: 'tr',
                      elementsPerPage: 10,
                      effect: 'climb'
                  });

                  $('#tbodyDoctorList').easyPaginate({
                      paginateElement: 'tr',
                      elementsPerPage: 10,
                      effect: 'climb'
                  });

                  $('#tbodyMyTestList').easyPaginate({
                      paginateElement: 'tr',
                      elementsPerPage: 10,
                      effect: 'climb'
                  });
</script>


</asp:Content>


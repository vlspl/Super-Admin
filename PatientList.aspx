<%@ Page Title="" Language="C#" MasterPageFile="~/LabMasterPage.master" AutoEventWireup="true"
    CodeFile="PatientList.aspx.cs" Inherits="PatientList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/date.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="navbar navbar-expand-sm navbar-header">
    <div class="container-fluid">
      <div class="navbar-title ml-5">
        <a href="#" class="navbar-brand">Patient List</a>
      </div>
      <div class="mr-5">
        <ul class="navbar-nav ml-auto">
          <li class="nav-item ">
            <div class="search-box">
              <input type="text" placeholder="Search by column value" class="input" id="myInput">
              <div class="search-btn">
                <i class="fa fa-search" aria-hidden="true" style="padding-top:40%"></i>
              </div>
            </div>
          </li>
          <li class="nav-item pt-1 mr-3">
                   <a href="#" data-toggle="modal" id="HideAddbtn" runat="server" data-target="#modalAddPatient" class="btn btn-color">
                   <span class="fa fa-plus mr-2" aria-hidden="true"></span> Add Patient</a>
          </li>
        </ul>
      </div>
    </div>
  </nav>
    <div class="table_div">
        <div class="container-fluid">
            <div id="patientList">
                <ul class="responsive-table">
                    <li class="table-header">
                        <div class="col col-1 text-center">
                            Sr. No.</div>
                        <div class="col col-2 text-center">
                            Name</div>
                        <div class="col col-3 text-center">
                            Gender</div>
                        <div class="col col-4 text-center">
                            Mobile</div>
                        <div class="col col-5 text-center">
                            Address</div>
                        <div class="col col-6 text-center">
                            Edit</div>
                        <div class="col col-7 text-center">
                            Payment</div>
                        <div class="col col-8 text-center">
                            View</div>
                    </li>
                    <div id="page">
                        <asp:Literal ID="tbodyPatientList" runat="server"></asp:Literal></div>
                </ul>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-8">
                <nav class="pagination-container">    
            <ul class="pagination justify-content-center">
              <li id="previous-page" class="px-2"><a href="javascript:void(0)" aria-label=Previous><span aria-hidden=true>&laquo;</span></a></li>
            </ul>
        </nav>
            </div>
            <div class="col-md-2">
            </div>
        </div>
    </div>
    <!-- Modal Add Patient Start-->
    <asp:HiddenField ID="hiddenAction" Value="0" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenAppUserId" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hMonth" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hDay" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hYear" runat="server" ClientIDMode="Static" />
    <div id="modalAddPatient" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h5 class="modal-title">
                        Add Patient</h5>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <!-- Modal body -->
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
                                    <asp:RegularExpressionValidator ID="regName" runat="server" 
       ControlToValidate="txtFullName" ForeColor="Red"
       ValidationExpression="^[a-zA-Z'.\s]{1,50}"
       Text="Enter a valid Name" /> 
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
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailId"
    ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
    Display = "Dynamic" ErrorMessage = "Invalid email address"/>
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
                                <cc1:CalendarExtender ID="Calendar1" CssClass="cal_Theme1" PopupButtonID="txtBirthDate"
                                    runat="server" TargetControlID="txtBirthDate" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </div>
                            <label id="lblBirthDate" class="form-error">
                            </label>
                            <label id="lblage">
                               <%-- <asp:Label ID="lblage" runat="server" Text="Label"></asp:Label>--%>
                          <%--  <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="RangeValidator"  
        ControlToValidate="lblage" MaximumValue="100" MinimumValue="18" Type="Integer"></asp:RangeValidator>--%>
                            </label>
                            <%--<asp:TextBox class="form-control" placeholder="State" Text="Maharashtra" ID="txtage"
                                    runat="server" ClientIDMode="Static" Visible="False"></asp:TextBox>--%>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Enter Age in Year" onkeyup="GetBirthDate()"
                                    ID="txtyear" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="State" Text="Maharashtra" ID="txtState"
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblState" class="form-error">
                                </label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Pincode *" onkeypress="return isNumber(event)"
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
                                <asp:TextBox class="form-control" placeholder="City" ID="txtCity" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
       ControlToValidate="txtCity" ForeColor="Red"
       ValidationExpression="^[a-zA-Z'.\s]{1,50}"
       Text="Enter a valid city" /> 
                                <label id="lblCity" class="form-error">
                                </label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group" id="address">
                                <asp:TextBox class="form-control" placeholder="Address *" ID="txtAddress" TextMode="MultiLine"
                                    Rows="2" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
       ControlToValidate="txtAddress" ForeColor="Red"
       ValidationExpression="^[a-zA-Z'.\s]{1,50}"
       Text="Enter a valid Address" /> 
                                <label id="lblAddress" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="upp" runat="server">
                    <ContentTemplate>
                        <div class="modal-footer">
                            <asp:Button ID="btnAdd" class="btn btn-submit" runat="server" Text="Submit" OnClientClick="javascript:return addPatientValidate()"
                                OnClick="btnAdd_Click" ClientIDMode="Static" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upp">
                    <ProgressTemplate>
                        <div class="text form_loader">
                            <img src="../../images/Loader.gif" alt="Loading">
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
    </div>
    <!-- Modal Add Patient End-->
    <!-- Modal Edit Patient Start-->
    <asp:HiddenField ID="hiddenEditAction" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HiddenMobileno" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenEditAppUserId" runat="server" ClientIDMode="Static" />
    <div id="modalEditPatient" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        Edit Patient</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" Style="display: none" placeholder="Enter Mobile Number"
                                    ID="txthiddenEditAppUserId" MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:TextBox class="form-control" placeholder="Enter Mobile Number" onkeypress="return isNumber(event)"
                                    ID="txtEditMobile" MaxLength="10" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblEditMobile" class="form-error">
                                </label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Enter Full Name" ID="txtEditFullName"
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblEditFullName" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Enter Email Id" ID="txtEditEmailId"
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblEditEmailId" class="form-error">
                                </label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:DropDownList class="form-control" ID="selEditGender" runat="server" ClientIDMode="Static">
                                    <asp:ListItem Value="select" Selected="True">Select</asp:ListItem>
                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                </asp:DropDownList>
                                <label id="lblEditGender" class="form-error">
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
                                <asp:TextBox ID="txtEditBirthDate" runat="server" onchange="AgeEditCalulation()"
                                    class="form-control" ClientIDMode="Static"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" PopupButtonID="txtEditBirthDate"
                                    runat="server" TargetControlID="txtEditBirthDate" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </div>
                            <label id="lblEditBirthDate" class="form-error">
                            </label>
                            <label id="lbleditage">
                            
                            </label>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="City" ID="txtEditCity" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>

                                <label id="lblEditCity" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="State" ID="txtEditState" runat="server"
                                    ClientIDMode="Static"></asp:TextBox>
                                <label id="lblEditState" class="form-error">
                                </label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox class="form-control" placeholder="Pincode" onkeypress="return isNumber(event)"
                                    ID="txtEditPincode" MaxLength="6" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblEditPincode" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-lg-12">
                            <div class="form-group">
                                <asp:TextBox class="form-control" Style="display: none" placeholder="Country" ID="txtEditCountry"
                                    runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblEditCountry" class="form-error hide">
                                </label>
                                <asp:TextBox class="form-control" placeholder="Address" ID="txtEditAddress" TextMode="MultiLine"
                                    Rows="2" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label id="lblEditAddress" class="form-error">
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnEdit" class="btn btn-submit" runat="server" Text="Update" OnClientClick="javascript:return editPatientValidate()"
                        OnClick="btnEdit_Click" ClientIDMode="Static" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Edit Patient End-->
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/PatientListValidation.js"></script>
    <script type="text/javascript" src="js/code.js"></script>
    <script type="text/javascript" src="js/pagination.js"></script>
    <script type="text/javascript">

        // $("#txtBirthDate").focusout(function () {
        function AgeCalulation() {

            var sdob = document.getElementById("txtBirthDate").value;

            var date = sdob.split('/');
            var sDay = date[0];
            var sMonth = date[1];
            var sYear = date[2];

            var fulldob = sYear + "-" + sMonth + "-" + sDay;
            //getAge(fulldob);
            var now = new Date();
            var tday = now.getDate();
            var tmo = (now.getMonth() + 1);
            var tyr = (now.getFullYear());

            var snowday = tyr + "-" + tmo + "-" + tday;
            var age = tyr - sYear;
            if ((tmo < sMonth) || ((tmo == sMonth) && tday < sDay)) {
                age--;
            }
            // $("#lblage").html(age);
            // alert(age);
            $("#lblage").html(getAge(fulldob));
        }
        // });
        //AgeCalulation

        // $("#txtEditBirthDate").focusout(function () {
        function AgeEditCalulation() {
            var sdob = document.getElementById("txtEditBirthDate").value;

            var date = sdob.split('/');
            var sDay = date[0];
            var sMonth = date[1];
            var sYear = date[2];
            var fulldob = sYear + "-" + sMonth + "-" + sDay;
            var now = new Date();
            var tday = now.getDate();
            var tmo = (now.getMonth() + 1);
            var tyr = (now.getFullYear());

            var snowday = tyr + "-" + tmo + "-" + tday;
            var age = tyr - sYear;
            if ((tmo < sMonth) || ((tmo == sMonth) && tday < sDay)) {
                age--;
            }
            //$("#lbleditage").html(age);
            // alert(age);
            $("#lbleditage").html(getAge(fulldob));
            //  });
        }

        //*******************************Age Calculation Start**************//
        function getAge(dateString) {


            var mdate = dateString;
            var yearThen = parseInt(mdate.substring(0, 4), 10);
            var monthThen = parseInt(mdate.substring(5, 7), 10);
            var dayThen = parseInt(mdate.substring(8, 10), 10);

            var today = new Date();
            var birthday = new Date(yearThen, monthThen - 1, dayThen);

            var differenceInMilisecond = today.valueOf() - birthday.valueOf();

            var year_age = Math.floor(differenceInMilisecond / 31536000000);
            var day_age = Math.floor((differenceInMilisecond % 31536000000) / 86400000);

            if ((today.getMonth() == birthday.getMonth()) && (today.getDate() == birthday.getDate())) {
                alert("Happy B'day!!!");
            }

            var month_age = Math.floor(day_age / 30);

            day_age = day_age % 30;

            if (isNaN(year_age) || isNaN(month_age) || isNaN(day_age)) {
                $("#exact_age").text("Invalid birthday - Please try again!");
            }
            else {
                // $("#exact_age").html("You are<br/><span id=\"age\">" + year_age + " years " + month_age + " months " + day_age + " days</span> old");
            }
            var AgeCalulation = 0;
            if (year_age != 0) {
                AgeCalulation = year_age + ' years ';
            }
            else if (month_age != 0) {
                AgeCalulation = month_age + ' months ';
            }
            else if (day_age != 0) {
                AgeCalulation = day_age + ' days';
            }

            //  var age = years + ' years ' + months + ' months ' + days + ' days';

            var age = " Age :" + AgeCalulation;
            return age;
        }

        //****************************Age Calculation*****************************//



        $(document).ready(function () {

            //reset modal fields on modal close
            $("#modalAddPatient").on("hidden.bs.modal", function () {
                $("#txtFullName").val("");
                $("#txtMobile").val("");
                $("#txtEmailId").val("");
                $("#selGender").val("select");
                $("#txtBirthDate").val("");
                $("#txtAddress").val("");
                $("#txtCountry").val("");
                $("#txtState").val("");
                //            $("#txtCity").val("");
                //            $("#txtPincode").val("");

                $("#lblage").html("");
                $("#lblFullName").html("");
                $("#lblMobile").html("");
                $("#lblEmailId").html("");
                $("#lblGender").html("");
                $("#lblBirthDate").html("");
                $("#lblAddress").html("");
                $("#lblCountry").html("");
                $("#lblState").html("");
                $("#lblCity").html("");
                $("#lblPincode").html("");
            });



            //reset modal fields on modal close for Edit
            $("#modalEditPatient").on("hidden.bs.modal", function () {
                $("#txtEditFullName").val("");
                $("#txtEditMobile").val("");
                $("#txtEditEmailId").val("");
                $("#selEditGender").val("select");
                $("#txtEditBirthDate").val("");
                $("#txtEditAddress").val("");
                $("#txtEditCountry").val("");
                $("#txtEditState").val("");
                $("#txtEditCity").val("");
                $("#txtEditPincode").val("");

                $("#lbleditage").html("");
                $("#lblEditFullName").html("");
                $("#lblEditMobile").html("");
                $("#lblEditEmailId").html("");
                $("#lblEditGender").html("");
                $("#lblEditBirthDate").html("");
                $("#lblEditAddress").html("");
                $("#lblEditCountry").html("");
                $("#lblEditState").html("");
                $("#lblEditCity").html("");
                $("#lblEditPincode").html("");
            });

            //start a href click

            $('#page').on('click', 'a', function () {
                var PatientId = $(this).attr("id");
                $.ajax
                 ({
                     url: "PatientList.aspx/GetPatientDetails",

                     data: JSON.stringify({ "PatientId": PatientId }),
                     type: 'POST',
                     contentType: 'application/json; charset=utf-8',
                     success: function (data) {
                         var result = data.d.split("&&&");

                         $("#hiddenEditAppUserId").val(result[1]);
                         $("#txthiddenEditAppUserId").val(result[1]);
                         $('#txtEditFullName').val(result[3])
                         $('#txtEditEmailId').val(result[13])
                         $('#txtEditMobile').val(result[7])
                         $('#txtEditAddress').val(result[9])
                         $('#HiddenMobileno').val(result[7])
                         $('#selEditGender').val(result[5])
                         $('#txtEditBirthDate').val(result[11])
                         $('#txtEditCountry').val(result[15])
                         $('#txtEditState').val(result[17])
                         $('#txtEditCity').val(result[19])
                         $('#txtEditPincode').val(result[21])
                         var sdob = document.getElementById("txtEditBirthDate").value;

                         var date = sdob.split('/');
                         var sDay = date[0];
                         var sMonth = date[1];
                         var sYear = date[2];
                         var fulldob = sYear + "/" + sMonth + "/" + sDay;
                         var now = new Date();
                         var tday = now.getDate();
                         var tmo = (now.getMonth() + 1);
                         var tyr = (now.getFullYear());

                         var snowday = tyr + "-" + tmo + "-" + tday;
                         var age = tyr - sYear;
                         if ((tmo < sMonth) || ((tmo == sMonth) && tday < sDay)) {
                             age--;
                         }
                         //$("#lbleditage").html(age);
                         $("#lbleditage").html(getAge(fulldob));
                     }
                 });
            });
            //end a href click

            $("#txtEditMobile").focusout(function () {

                var oldmobilenumber = $("#HiddenMobileno").val()
                var newmobilenumber = $("#txtEditMobile").val()

                if (oldmobilenumber != newmobilenumber) {

                    var parameter = { "mobile": $("#txtEditMobile").val() };
                    $.ajax({
                        type: "POST",
                        url: "PatientList.aspx/searchRecord",
                        data: JSON.stringify(parameter),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            var json = JSON.parse(response.d);
                            console.log(json);
                            //if patient does not exist
                            if (json["key"] == 0) {
                                $("#hiddenEditAction").val(0);
                                $("#hiddenEditAppUserId").val("");
                                $("#lblEditMobile").html("");
                            }
                            //if patient exists and added in labs list
                            else if (json["key"] == 1) {
                                if ($("#txtMobile").val() != "") {
                                    $("#txtEditMobile").val("");
                                    $("#lblEditMobile").html("This patient already exists in your list");
                                }
                            }
                            //if patient exists but not added to list
                            else if (json["key"] == 2) {
                                $("#txtEditMobile").val(json["value"][0]["sMobile"]);
                                $("#txtEditFullName").val(json["value"][0]["sFullName"]);
                                $("#txtEditEmailId").val(json["value"][0]["sEmailId"]);

                                if (json["value"][0]["sGender"].toLowerCase() == "female") {
                                    $("#selEditGender").val("Female");
                                }
                                else if (json["value"][0]["sGender"].toLowerCase() == "male") {
                                    $("#selEditGender").val("Male");
                                }

                                var date = json["value"][0]["sBirthDate"];

                                var d = new Date(date);
                                console.log(d);
                                var dd = d.getDate();
                                var mm = d.getMonth() + 1;
                                console.log(mm.toString().length);
                                mm = (mm.toString().length == 1) ? ('0' + mm.toString()) : mm;
                                console.log(mm);
                                var yy = d.getFullYear();
                                var bdate = dd + "/" + mm + "/" + yy;
                                console.log(bdate);
                                //$("#dob").val(bdate);


                                $("#txtEditBirthDate").val(bdate);
                                $("#txtEditAddress").val(json["value"][0]["sAddress"]);
                                $("#txtEditCountry").val(json["value"][0]["sCountry"]);
                                $("#txtEditState").val(json["value"][0]["sState"]);
                                $("#txtEditCity").val(json["value"][0]["sCity"]);
                                $("#txtEditPincode").val(json["value"][0]["sPincode"]);
                                $("#hiddenEditAction").val(2);
                                $("#hiddenEditAppUserId").val(json["value"][0]["sAppUserId"]);
                                $("#txthiddenEditAppUserId").val(json["value"][0]["sAppUserId"]);
                            }
                            console.log("success" + response.d);
                        },
                        error: function (response) {
                            console.log("error" + response.d);
                        },
                        failure: function (response) {
                            console.log("fail" + response.d);
                        }
                    });
                }
            });


            $("#txtMobile").focusout(function () {
                var parameter = { "mobile": $("#txtMobile").val() };
                $.ajax({
                    type: "POST",
                    url: "PatientList.aspx/searchRecord",
                    data: JSON.stringify(parameter),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var json = JSON.parse(response.d);
                        console.log(json);
                        //if patient does not exist
                        if (json["key"] == 0) {
                            $("#hiddenAction").val(0);
                            $("#hiddenAppUserId").val("");
                            $("#lblMobile").html("");
                        }
                        //if patient exists and added in labs list
                        else if (json["key"] == 1) {
                            if ($("#txtMobile").val() != "") {
                                $("#txtMobile").val("");
                                $("#lblMobile").html("This patient already exists in your list");
                            }
                        }
                        //if patient exists but not added to list
                        else if (json["key"] == 2) {
                            $("#txtMobile").val(json["value"][0]["sMobile"]);
                            $("#txtFullName").val(json["value"][0]["sFullName"]);
                            $("#txtEmailId").val(json["value"][0]["sEmailId"]);

                            if (json["value"][0]["sGender"].toLowerCase() == "female") {
                                $("#selGender").val("Female");
                            }
                            else if (json["value"][0]["sGender"].toLowerCase() == "male") {
                                $("#selGender").val("Male");
                            }
                            var date = json["value"][0]["sBirthDate"];
                            var d = new Date(date);
                            console.log(d);
                            var dd = d.getDate();
                            var mm = d.getMonth() + 1;
                            console.log(mm.toString().length);
                            mm = (mm.toString().length == 1) ? ('0' + mm.toString()) : mm;
                            console.log(mm);
                            var yy = d.getFullYear();
                            var bdate = dd + "/" + mm + "/" + yy;
                            console.log(bdate);
                            //$("#dob").val(bdate);

                            $("#txtBirthDate").val(bdate);
                            $("#txtAddress").val(json["value"][0]["sAddress"]);
                            $("#txtCountry").val(json["value"][0]["sCountry"]);
                            $("#txtState").val(json["value"][0]["sState"]);
                            $("#txtCity").val(json["value"][0]["sCity"]);
                            $("#txtPincode").val(json["value"][0]["sPincode"]);

                            $("#hiddenAction").val(2);
                            $("#hiddenAppUserId").val(json["value"][0]["sAppUserId"]);
                        }
                        console.log("success" + response.d);
                    },
                    error: function (response) {
                        console.log("error" + response.d);
                    },
                    failure: function (response) {
                        console.log("fail" + response.d);
                    }
                });
            });
        });

    
    </script>
    <%--<script type="text/javascript" src="js/jquery.js"></script>--%>
    <script type="text/javascript" src="js/datatable.jquery.js"></script>
    <script type="text/javascript" src="js/datatable.js"></script>
    <%--<script type="text/javascript" src="js/jquery.easyPaginate.js"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#rdoBookSlot").change(function () {
                var dNow = new Date();
                var utcdate = (dNow.getMonth() + 1) + '/' + dNow.getDate() + '/' + dNow.getFullYear();
                $('#currentDate').text(utcdate);
                var rows = $("#tbodyTestBookList").find("tr").hide();
                rows.filter(":contains('" + utcdate + "')").show();
            });
        });

        function GetBirthDate() {
            var yr = $("#hYear").val();
            var mn = $("#hMonth").val();
            var day = $("#hDay").val();
            var y = $("#txtyear").val();
            var x = yr - y;

            var dob = day + "/" + mn + "/" + x;

            $("#txtBirthDate").val(dob);
            var age = y;
            var Finalage = "";
            if (age != "") {
                age += " years";
                Finalage = " Age :";
            }

            $("#lblage").text(Finalage + age)

        }
    </script>
    <script type="text/javascript">
        function Search_Gridview(strKey, strGV) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById(strGV);
            var rowData;
            for (var i = 0; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#page li").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>
</asp:Content>

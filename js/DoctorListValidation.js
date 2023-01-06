 function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function IsValidEmail12(email) {
  var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    if (validRegex.test(email) == false) {
        return false;
    }

    return true;

}
function onlyNumbers(phone) {
    var error = "";
    var stripped = phone.replace(/[\(\)\.\-\ ]/g, '');

    if (isNaN(parseInt(stripped))) {
        return false;


    } else if (!(stripped.length == 10)) {

        return false;
    }
    else { return true; }
}


function IsValidEmail(email) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

    if (reg.test(email) == false) {
        return false;
    }

    return true;

}

//function IsValidEmail(email) {
//    var spliter = [];
//    if (email.toString().indexOf('@') >= 0) {
//        spliter = email.toString().split("@");
//        if (spliter.length > 2) {
//            return false;
//        } else {
//            if (spliter[0].toString() == "")
//                return false;
//            if (email.toString().indexOf('.') >= 0) {
//                spliter = spliter[1].toString().split('.');
//                if (spliter.length > 2)
//                    return false;
//                else {
//                    if (spliter[1].toString() == "")
//                        return false;
//                    else
//                        return true;
//                }
//            } else
//                return false;
//        }
//    } else
//        return false;
//}



function addDoctorValidate() {
    var icount = 0;
    //    if ($("#txtMobile").val().trim() == "") 
    //    {
    //        icount++;
    //        $('#txtMobile').addClass('has-error');
    //        $("#lblMobile").html("Please enter your Mobile No. *");
    //        $("#lblMobile").show();
    //    }
    //    else 
    //    {
    //        $('#txtMobile').addClass('has-error');
    //        $("#lblMobile").hide();

    //        var e = onlyNumbers($("#txtMobile").val());
    //        if (!e) 
    //        {
    //            icount++;
    //            $('#txtMobile').addClass('has-error');
    //            $("#lblMobile").html("* Please enter a valid Mobile No.");
    //            $("#lblMobile").show();
    //        }
    //        else 
    //        {
    //            $('#txtMobile').removeClass('has-error');
    //            $("#lblMobile").hide();
    //        }
    //    }

    if ($("#txtMobile").val().trim() != "") {
        var e = onlyNumbers($("#txtMobile").val());
        if (!e) {
            icount++;
            $('#txtMobile').addClass('has-error');
            $("#lblMobile").html("* Please enter a valid Mobile No.");
            $("#lblMobile").show();
        } else {
            $('#txtMobile').removeClass('has-error');
            $("#lblMobile").hide();
        }
    }

    if ($("#txtFullName").val() == "") {
        $("#txtFullName").addClass("has-error");
        $("#lblFullName").html("Name required *");
        // return false;
        icount++;
    }
    else {
        $('#txtFullName').removeClass('has-error');
        $("#lblFullName").hide();
    }


    //    if ($("#txtEmailId").val().trim() == "") {
    //        icount++;
    //        $('#txtEmailId').addClass('has-error');
    //        $("#lblEmailId").html("* Please enter your email");
    //        $("#lblEmailId").show();
    //    } else 
    //    {

    if ($("#txtEmailId").val().trim() != "" || ($("#txtEmailId").val()=="")) {
   // if ($("#txtEmailId").val().trim() != "") {
        var e = IsValidEmail12($("#txtEmailId").val());
        if (!e) {
            icount++;
            $('#txtEmailId').addClass('has-error');
            $("#lblEmailId").html("* Please enter a valid email address");
            $("#lblEmailId").show();
        } else {
            $('#lblEmailId').removeClass('has-error');
            $("#lblEmailId").hide();
        }
    }


    if ($("#selGender").val() == "select") {
        $("#selGender").addClass("has-error");
        $("#lblGender").html("Gender required *");
        $("#lblGender").show();
        // return false;
        icount++;
    }
    else {
        $('#selGender').removeClass('has-error');
        $("#lblGender").hide();
    }

    if ($("#txtSpecialization").val() == "select") {
        $("#txtSpecialization").addClass("has-error");
        $("#lblSpecialization").html("Specialization required *");
        $("#lblSpecialization").show();
        // return false;
        icount++;
    }
    else {
        $('#txtSpecialization').removeClass('has-error');
        $("#lblSpecialization").hide();
    }


    //    else if (!isDate($("#txtBirthDate").val())) {
    //        $("#txtBirthDate").addClass("has-error");
    //        $("#lblBirthDate").html("Invalid birthdate *");
    //        return false;
    //    }


    //    if ($("#txtBirthDate").val() == '') {
    //        $("#txtBirthDate").addClass("has-error");
    //        $("#lblBirthDate").html("birthdate * (mm/dd/yyyy) required");
    //        // return false;
    //        icount++;
    //    }
    //    else {
    //        $('#txtBirthDate').removeClass('has-error');
    //        $("#lblBirthDate").hide();
    //    }


    if ($("#txtAddress").val() == "") {
        $("#txtAddress").addClass("has-error");
        $("#lblAddress").html("Address required *");
        //  return false;
        icount++;
    }
    else {
        $('#txtAddress').removeClass('has-error');
        $("#lblAddress").hide();
    }


    if ($("#txtDegree").val() == "") {
        $("#txtDegree").addClass("has-error");
        $("#lblDegree").html("Degree required *");
        //  return false;
        icount++;
    }
    else {
        $('#txtDegree').removeClass('has-error');
        $("#lblDegree").hide();
    }


    //          if ($("#txtSpecialization").val() == "") {
    //        $("#txtSpecialization").addClass("has-error");
    //        $("#lblSpecialization").html("Specialization required *");
    //        //  return false;
    //        icount++;
    //    }
    //    else {
    //        $('#txtSpecialization').removeClass('has-error');
    //        $("#lblSpecialization").hide();
    //    }


    if ($("#txtClinic").val() == "") {
        $("#txtClinic").addClass("has-error");
        $("#lblClinic").html("Clinic required *");
        //  return false;
        icount++;
    }
    else {
        $('#txtClinic').removeClass('has-error');
        $("#lblClinic").hide();
    }


    //    else if ($("#txtCountry").val() == "") {
    //        $("#txtCountry").addClass("has-error");
    //        $("#lblCountry").html("Country required *");
    //        return false;
    //    }
    if ($("#txtState").val() == "") {
        $("#txtState").addClass("has-error");
        $("#lblState").html("State required *");
        //  return false;
        icount++;
    }
    else {
        $('#txtState').removeClass('has-error');
        $("#lblState").hide();
    }


    if ($("#txtCity").val() == "") {
        $("#txtCity").addClass("has-error");
        $("#lblCity").html("City required *");
        //  return false;
        icount++;
    }
    else {
        $('#txtCity').removeClass('has-error');
        $("#lblCity").hide();
    }

    if ($("#txtPincode").val() == "" || isNaN($("#txtPincode").val()) == true) {
        $("#txtPincode").addClass("has-error");
        $("#lblPincode").html("Pincode required *");
        // return false;
        icount++;
    }
    else {
        $('#txtPincode').removeClass('has-error');
        $("#lblPincode").hide();
    }


    if (icount > 0) {

        return false;
    }
    else {
        return true;
    }

}

// edit doctor validation

function editDoctorValidate() {

    //    if ($("#txtEditMobile").val() == "") {
    //        $("#txtEditMobile").addClass("has-error");
    //        $("#lblEditMobile").html("Mobile required *");
    //        return false;
    //    }

    if ($("#txtEditMobile").val().trim() != "") {
        var e = onlyNumbers($("#txtEditMobile").val());
        if (!e) {
            icount++;
            $('#txtEditMobile').addClass('has-error');
            $("#lblEditMobile").html("* Please enter a valid Mobile No.");
            $("#lblEditMobile").show();
            return false;
        }
        else {
            $('#txtEditMobile').removeClass('has-error');
            $("#lblEditMobile").hide();
        }
    }
    else if ($("#txtEditFullName").val() == "") {
        $("#txtEditFullName").addClass("has-error");
        $("#lblEditFullName").html("Name required *");
        return false;
    }
    //    else if (isNaN($("#txtEditMobile").val()) || $("#txtEditMobile").val().length < 10) {
    //        $("#txtEditMobile").addClass("has-error");
    //        $("#lblEditMobile").html("Invalid contact *");
    //        return false;
    //    }
    //    else if ($("#txtEditEmailId").val() == "") {
    //        $("#txtEditEmailId").addClass("has-error");
    //        $("#lblEditEmailId").html("Email id required *");
    //        return false;
    //    }

    else if ($("#txtEditEmailId").val().trim() != "") {
        var e = IsValidEmail12($("#txtEditEmailId").val());
        if (!e) {
            icount++;
            $('#txtEditEmailId').addClass('has-error');
            $("#lblEditEmailId").html("* Please enter a valid email address");
            $("#lblEditEmailId").show();
            return false;
        } else {
            $('#lblEditEmailId').removeClass('has-error');
            $("#lblEditEmailId").hide();
        }
    }
    else if (IsValidEmail($("#txtEditEmailId").val()) == false) {
        $("#txtEditEmailId").addClass("has-error");
        $("#lblEditEmailId").html("Invalid email id *");
        return false;
    }
    else if ($("#selEditGender").val() == "select") {
        $("#txtEditGender").addClass("has-error");
        $("#lblEditGender").html("Gender required *");
        return false;
    }
    else if (!isDate($("#txtEditBirthDate").val())) {
        $("#txtEditBirthDate").addClass("has-error");
        $("#lblEditBirthDate").html("Invalid birthdate *");
        return false;
    }
    else if ($("#txtEditAddress").val() == "") {
        $("#txtEditAddress").addClass("has-error");
        $("#lblEditAddress").html("Address required *");
        return false;
    }
    else if ($("#txtEditDegree").val() == "") {
        $("#txtEditDegree").addClass("has-error");
        $("#lblEditDegree").html("Degree required *");
        return false;
    }
    else if ($("#txtEditSpecialization").val() == "") {
        $("#txtEditSpecialization").addClass("has-error");
        $("#lblEditSpecialization").html("Specialization required *");
        return false;
    }
    else if ($("#txtEditClinic").val() == "") {
        $("#txtEditClinic").addClass("has-error");
        $("#lblEditClinic").html("Clinic required *");
        return false;
    }
    else if ($("#txtEditCountry").val() == "") {
        $("#txtEditCountry").addClass("has-error");
        $("#lblEditCountry").html("Country required *");
        return false;
    }
    else if ($("#txtEditState").val() == "") {
        $("#txtEditState").addClass("has-error");
        $("#lblEditState").html("State required *");
        return false;
    }
    else if ($("#txtEditCity").val() == "") {
        $("#txtEditCity").addClass("has-error");
        $("#lblEditCity").html("City required *");
        return false;
    }
    else if ($("#txtEditPincode").val() == "" || isNaN($("#txtEditPincode").val()) == true) {
        $("#txtEditPincode").addClass("has-error");
        $("#lblEditPincode").html("Pincode required *");
        return false;
    }
    
    else {
        return true;
    }
}





// Validate email id
//function IsValidEmail(email) {
//    var spliter = [];
//    if (email.toString().indexOf('@') >= 0) {
//        spliter = email.toString().split("@");
//        if (spliter.length > 2) {
//            return false;
//        } else {
//            if (spliter[0].toString() == "")
//                return false;
//            if (email.toString().indexOf('.') >= 0) {
//                spliter = spliter[1].toString().split('.');
//                if (spliter.length > 2)
//                    return false;
//                else {
//                    if (spliter[1].toString() == "")
//                        return false;
//                    else
//                        return true;
//                }
//            } else
//                return false;
//        }
//    } else
//        return false;
//}

function isbDate(txtDate) {

    var currVal = txtDate;
    if (currVal == '')
        return false;

    //Declare Regex 
    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?

    if (dtArray == null)
        return false;

    //Checks for mm/dd/yyyy format.
    dtMonth = dtArray[3];
    dtDay = dtArray[1];
    dtYear = dtArray[5];

    if (dtMonth < 1 || dtMonth > 12 || dtMonth.length != 2)
        return false;
    else if (dtDay < 1 || dtDay > 31 || dtDay.length != 2)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));

        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }
    else {
        var myDate = new Date(txtDate);
        var today = new Date();
        if (myDate > today) {
            return false;
        }
    }
    return true;
}

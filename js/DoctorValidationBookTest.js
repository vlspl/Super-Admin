
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
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



function addDoctorValidateInBookTest() {

    var icount = 0;
//    if ($("#txtMobile1").val().trim() == "") {
//        icount++;
//        $('#txtMobile1').addClass('has-error');
//        $("#lblMobile1").html("Please enter your Mobile No. *");
//        $("#lblMobile1").show();
//    } else {
//        $('#txtMobile1').addClass('has-error');
//        $("#lblMobile1").hide();

//        var e = onlyNumbers($("#txtMobile1").val());
//        if (!e) {
//            icount++;
//            $('#txtMobile1').addClass('has-error');
//            $("#lblMobile1").html("* Please enter a valid Mobile No.");
//            $("#lblMobile1").show();
//        } else {
//            $('#txtMobile1').removeClass('has-error');
//            $("#lblMobile1").hide();
//        }
    //    }

    if ($("#txtMobile1").val().trim() != "") {
        var e = onlyNumbers($("#txtMobile1").val());
        if (!e) {
            icount++;
            $('#txtMobile1').addClass('has-error');
            $("#lblMobile1").html("* Please enter a valid Mobile No.");
            $("#lblMobile1").show();
        } else {
            $('#txtMobile1').removeClass('has-error');
            $("#lblMobile1").hide();
        }
    }

    if ($("#txtFullName1").val() == "") {
        $("#txtFullName1").addClass("has-error");
        $("#lblFullName1").html("Name required *");
        // return false;
        icount++;
    }
    else {
        $('#txtFullName1').removeClass('has-error');
        $("#lblFullName1").hide();
    }

    if ($("#txtEmailId1").val().trim() != "") {
        var e = IsValidEmail($("#txtEmailId1").val());
        if (!e) {
            icount++;
            $('#txtEmailId1').addClass('has-error');
            $("#lblEmailId1").html("* Please enter a valid email address");
            $("#lblEmailId1").show();
        } else {
            $('#lblEmailId1').removeClass('has-error');
            $("#lblEmailId1").hide();
        }
    }


    if ($("#selGender1").val() == "select") {
        $("#txtGender1").addClass("has-error");
        $("#lblGender1").html("Gender required *");
        // return false;
        icount++;
    }
    else {
        $('#selGender1').removeClass('has-error');
        $("#lblGender1").hide();
    }


    //    else if (!isDate($("#txtBirthDate").val())) {
    //        $("#txtBirthDate").addClass("has-error");
    //        $("#lblBirthDate").html("Invalid birthdate *");
    //        return false;
    //    }


//    if ($("#txtBirthDate1").val() == '') {
//        $("#txtBirthDate1").addClass("has-error");
//        $("#lblBirthDate1").html("birthdate * (mm/dd/yyyy) required");
//        // return false;
//        icount++;
//    }
//    else {
//        $('#selGender1').removeClass('has-error');
//        $("#lblGender1").hide();
//    }


    if ($("#txtAddress1").val() == "") {
        $("#txtAddress1").addClass("has-error");
        $("#lblAddress1").html("Address required *");
        //  return false;
        icount++;
    }
    else {
        $('#txtAddress1').removeClass('has-error');
        $("#lblAddress1").hide();
    }


    if ($("#txtDegree1").val() == "") {
        $("#txtDegree1").addClass("has-error");
        $("#lblDegree1").html("Degree required *");
        //  return false;
        icount++;
    }
    else {
        $('#txtDegree1').removeClass('has-error');
        $("#lblDegree1").hide();
    }


    if ($("#txtSpecialization1").val() == "") {
        $("#txtSpecialization1").addClass("has-error");
        $("#lblSpecialization1").html("Specialization required *");
        //  return false;
        icount++;
    }
    else {
        $('#txtSpecialization1').removeClass('has-error');
        $("#lblSpecialization1").hide();
    }


    if ($("#txtClinic1").val() == "") {
        $("#txtClinic1").addClass("has-error");
        $("#lblClinic1").html("Clinic required *");
        //  return false;
        icount++;
    }
    else {
        $('#txtClinic1').removeClass('has-error');
        $("#lblClinic1").hide();
    }


    //    else if ($("#txtCountry").val() == "") {
    //        $("#txtCountry").addClass("has-error");
    //        $("#lblCountry").html("Country required *");
    //        return false;
    //    }
    if ($("#txtState1").val() == "") {
        $("#txtState1").addClass("has-error");
        $("#lblState1").html("State required *");
        //  return false;
        icount++;
    }
    else {
        $('#txtState1').removeClass('has-error');
        $("#lblState1").hide();
    }


    if ($("#txtCity1").val() == "") {
        $("#txtCity1").addClass("has-error");
        $("#lblCity1").html("City required *");
        //  return false;
        icount++;
    }
    else {
        $('#txtCity1').removeClass('has-error');
        $("#lblCity1").hide();
    }

    if ($("#txtPincode1").val() == "" || isNaN($("#txtPincode1").val()) == true) {
        $("#txtPincode1").addClass("has-error");
        $("#lblPincode1").html("Pincode required *");
        // return false;
        icount++;
    }
    else {
        $('#txtPincode1').removeClass('has-error');
        $("#lblPincode1").hide();
    }


    if (icount > 0) {

        return false;
    }
    else {
        return true;
    }

}








//VAlidation for Edit Js File 

function editPatientValidate() {

    if ($("#txtEditMobile1").val() == "") {
        $("#txtEditMobile1").addClass("has-error");
        $("#lblEditMobile1").html("Mobile required *");
        return false;
    }
    else if ($("#txtEditFullName1").val() == "") {
        $("#txtEditFullName1").addClass("has-error");
        $("#lblEditFullName1").html("Name required *");
        return false;
    }
    else if (isNaN($("#txtEditMobile1").val()) || $("#txtEditMobile1").val().length < 10) {
        $("#txtEditMobile1").addClass("has-error");
        $("#lblEditMobile1").html("Invalid contact *");
        return false;
    }
    else if ($("#txtEditEmailId1").val() == "") {
        $("#txtEditEmailId1").addClass("has-error");
        $("#lblEditEmailId1").html("Email id required *");
        return false;
    }
    else if (IsValidEmail($("#txtEditEmailId1").val()) == false) {
        $("#txtEditEmailId1").addClass("has-error");
        $("#lblEditEmailId1").html("Invalid email id *");
        return false;
    }
    else if ($("#selEditGender1").val() == "select") {
        $("#txtEditGender1").addClass("has-error");
        $("#lblEditGender1").html("Gender required *");
        return false;
    }
    else if (!isbDate($("#txtEditBirthDate1").val())) {
        $("#txtEditBirthDate1").addClass("has-error");
        $("#lblEditBirthDate1").html("Invalid birthdate * (mm/dd/yyyy) required");
        return false;
    }
    else if ($("#txtEditAddress1").val() == "") {
        $("#txtEditAddress1").addClass("has-error");
        $("#lblEditAddress1").html("Address required *");
        return false;
    }
    //    else if ($("#txtEditCountry").val() == "") {
    //        $("#txtEditCountry").addClass("has-error");
    //        $("#lblEditCountry").html("Country required *");
    //        return false;
    //    }
    else if ($("#txtEditState1").val() == "") {
        $("#txtEditState1").addClass("has-error");
        $("#lblEditState1").html("State required *");
        return false;
    }
    else if ($("#txtEditCity1").val() == "") {
        $("#txtEditCity1").addClass("has-error");
        $("#lblEditCity1").html("City required *");
        return false;
    }
    else if ($("#txtEditPincode1").val() == "" || isNaN($("#txtEditPincode1").val()) == true) {
        $("#txtEditPincode1").addClass("has-error");
        $("#lblEditPincode1").html("Pincode required *");
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

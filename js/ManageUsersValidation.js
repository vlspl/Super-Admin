function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

// Validation for Manager Users user edit
function validateUserEdit1(){
    
    if ($("#txtFullName").val() == "") {
        $("#lblFullName").addClass("has-error");
        $("#lblFullName").html("Name required *");
        return false;
    }
    else if ($("#txtEmailId").val() == "") {
        $("#lblEmailId").addClass("has-error");
        $("#lblEmailId").html("Email id required *");
        return false;
    }
    else if ($("#txtContact").val() == "") {
        $("#lblContact").addClass("has-error");
        $("#lblContact").html("Contact required *");
        return false;
    }
    else if (IsValidEmail($("#txtEmailId").val()) == false) {
        $("#lblEmailId").addClass("has-error");
        $("#lblEmailId").html("Invalid email id *");
        return false;
    }
    else if (isNaN($("#txtContact").val()) || $("#txtContact").val().length < 10) {
        $("#lblContact").addClass("has-error");
        $("#lblContact").html("Invalid contact *");
        return false;
    }
    else {
        return true;
    }
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
// Validation for Manager Users user add
function validateUserAdd() {

//    if ($("#txtFullName").val() == "") {
//        $("#lblFullName").addClass("has-error");
//        $("#lblFullName").html("Name required *");
//        return false;
    //    }
    var icount = 0;
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


//    else if ($("#txtEmailId").val() == "") {
//        $("#lblEmailId").addClass("has-error");
//        $("#lblEmailId").html("Email id required *");
//        return false;
    //    }

//    if ($("#txtEmailId").val().trim() == "") {
//        icount++;
//        $('#txtEmailId').addClass('has-error');
//        $("#lblEmailId").html("Email id required *");
//        $("#lblEmailId").show();
//    } else {
//        var e = IsValidEmail($("#txtEmailId").val());
//        if (!e) {
//            icount++;
//            $('#txtEmailId').addClass('has-error');
//            $("#lblEmailId").html("* Please enter a valid email address");
//            $("#lblEmailId").show();
//        } else {
//            $('#lblEmailId').removeClass('has-error');
//            $("#lblEmailId").hide();
//        }
//    }

    if ($("#txtEmailId").val().trim() != "") {
        var e = IsValidEmail($("#txtEmailId").val());
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

//    else if ($("#txtContact").val() == "") {
//        $("#lblContact").addClass("has-error");
//        $("#lblContact").html("Contact required *");
//        return false;
    //    }


    if ($("#txtContact").val().trim() == "") {
        icount++;
        $('#txtContact').addClass('has-error');
        $("#lblContact").html("Contact required *");
        $("#lblContact").show();
    } else {
        $('#txtContact').addClass('has-error');
        $("#lblContact").hide();

        var e = onlyNumbers($("#txtContact").val());
        if (!e) {
            icount++;
            $('#txtContact').addClass('has-error');
            $("#lblContact").html("* Please enter a valid Mobile No.");
            $("#lblContact").show();
        } else {
            $('#txtContact').removeClass('has-error');
            $("#lblContact").hide();
        }
    }
//    else if ($("#txtUserName").val() == "") {
//        $("#lblUserName").addClass("has-error");
//        $("#lblUserName").html("Username required *");
//        return false;
//    }

    if ($("#txtUserName").val() == "") {
        $("#txtUserName").addClass("has-error");
        $("#lblUserName").html("User Name required *");
        // return false;
        icount++;
    }
    else {
        $('#txtUserName').removeClass('has-error');
        $("#lblUserName").hide();
    }


//    else if ($("#txtPassword").val() == "") {
//        $("#lblPassword").addClass("has-error");
//        $("#lblPassword").html("Password required *");
//        return false;
    //    }
    if ($("#txtPassword").val() == "") {
        $("#txtPassword").addClass("has-error");
        $("#lblPassword").html("Password required *");
        // return false;
        icount++;
    }
    else {
        $('#txtPassword').removeClass('has-error');
        $("#lblPassword").hide();
    }

//    else if (IsValidEmail($("#txtEmailId").val()) == false) {
//        $("#lblEmailId").addClass("has-error");
//        $("#lblEmailId").html("Invalid email id *");
//        return false;
//    }
//    else if (isNaN($("#txtContact").val()) || $("#txtContact").val().length < 10) {
//        $("#lblContact").addClass("has-error");
//        $("#lblContact").html("Invalid contact *");
//        return false;
//    }
//    else {
//        return true;
//    }


    if (icount > 0) {

        return false;
    }
    else {
        return true;
    }



}

function IsValidEmail(email) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

    if (reg.test(email) == false) {
        return false;
    }

    return true;

}


// Validation for Manager Users user edit
function validateUserEdit() {

    if ($("#txtFullNameEdit").val() == "") {
        $("#lblFullNameEdit").show();
        $("#lblFullNameEdit").addClass("has-error");
        $("#lblFullNameEdit").html("Name required *");
        return false;
    }
    else if ($("#txtEmailIdEdit").val() == "") {
        $("#lblEmailIdEdit").show();
        $("#lblEmailIdEdit").addClass("has-error");
        $("#lblEmailIdEdit").html("Email id required *");
        return false;
    }
    else if ($("#txtContactEdit").val() == "") {
        $("#lblContactEdit").show();
        $("#lblContactEdit").addClass("has-error");
        $("#lblContactEdit").html("Contact required *");
        return false;
    }
    else if (IsValidEmail($("#txtEmailIdEdit").val()) == false) {
        $("#lblEmailIdEdit").addClass("has-error");
        $("#lblEmailIdEdit").html("Invalid email id *");
        return false;
    }
    else if (isNaN($("#txtContactEdit").val()) || $("#txtContactEdit").val().length < 10) {
        $("#lblContactEdit").addClass("has-error");
        $("#lblContactEdit").html("Invalid contact *");
        return false;
    }
    else {
        $("#lblFullNameEdit").hide();
        $("#lblEmailIdEdit").hide();
        $("#lblContactEdit").hide();
        return true;
    }
}


// Validate email id
function IsValidEmail(email) {
    var spliter = [];
    if (email.toString().indexOf('@') >= 0) {
        spliter = email.toString().split("@");
        if (spliter.length > 2) {
            return false;
        } else {
            if (spliter[0].toString() == "")
                return false;
            if (email.toString().indexOf('.') >= 0) {
                spliter = spliter[1].toString().split('.');
                if (spliter.length > 2)
                    return false;
                else {
                    if (spliter[1].toString() == "")
                        return false;
                    else
                        return true;
                }
            } else
                return false;
        }
    } else
        return false;
}
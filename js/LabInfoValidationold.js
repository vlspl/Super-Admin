// Validation for lab info
function validateLabInfo() 
{

    if ($("#txtLabName").val() == "") {
        $("#lblLabName").addClass("has-error");
        $("#lblLabName").html("Lab name required *");
        return false;
    }
//    else if ($("#txtLabDetails").val() == "") {
//        $("#lblLabDetails").addClass("has-error");
//        $("#lblLabDetails").html("Lab details required *");
//        return false;
//    }
    else {
        return true;
    }
}
//function IsValidEmail(email) {
//    var re = /\S+@\S+\.\S+/;
//    return re.test(email);
//}

// Validation for lab contact details
function validateLabContact() {

    if ($("#txtLabEmail").val() == "") {
        $("#lblLabEmail").addClass("has-error");
        $("#lblLabEmail").html("Lab email id required *");
        return false;
    }
    else if ($("#txtLabContact").val() == "") {
        $("#lblLabContact").addClass("has-error");
        $("#lblLabContact").html("Lab contact required *");
        return false;
    }
    else if ($("#txtLabAddress").val() == "") {
        $("#lblLabAddress").addClass("has-error");
        $("#lblLabAddress").html("Lab address required *");
        return false;
    }
    else if (IsValidEmail($("#txtLabEmail").val()) == false) {
        $("#lblLabEmail").addClass("has-error");
        $("#lblLabEmail").html("Invalid email id *");
        return false;
    }
    else if (isNaN($("#txtLabContact").val()) || $("#txtLabContact").val().length < 10) {
        $("#lblLabContact").addClass("has-error");
        $("#lblLabContact").html("Invalid contact *");
        return false;
    }
    else {
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
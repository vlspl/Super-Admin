// Validation for payment and test status for booking
function validateBookingDetails() {
   
    if ($("#selPaymentStatus").val() == "advance") {
        if ($("#txtAdvancePaid").val() == "") {
            $("#lblAdvancePaid").addClass("has-error");
            $("#lblAdvancePaid").html("Enter Advance amount");
            
            return false;
        }
        else if (isNaN($("#txtAdvancePaid").val())) {
            $("#lblAdvancePaid").addClass("has-error");
            $("#lblAdvancePaid").html("Enter valid amount");
            return false;
        }
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

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return true;
    }
  
    return false;
}

//document.getElementById("txtAdvancePaid").disabled = true;

//$("#selPaymentStatus").change(function () {
//    if ($("#selPaymentStatus").val() == 'paid') {
//        document.getElementById("txtAdvancePaid").disabled = true;
//      
//    }
//    if ($("#selPaymentStatus").val() == 'advance') {
//        document.getElementById("txtAdvancePaid").disabled = false;
//    }
//    if ($("#selPaymentStatus").val() == 'not paid') {
//        document.getElementById("txtAdvancePaid").disabled = true;
//    }
//});

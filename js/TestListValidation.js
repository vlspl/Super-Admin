function testPriceValidate() {

    var error = 0;    
    for (i = 1; i < $("#tabEditMyTestList tr").length; i++) {
        if ($("#txtPrice" + i).val() == "" || $("#txtPrice" + i).val() == "0") {
            error = 1;      
        }
      }

    if (error == 1) {
        alert("Please enter price for all tests");
        return false;
    }
    else if (error == 0) {
        return true;
    }
}


function createTestPackageValidate() {
    if ($("#txtPackageName").val() == "") {
        $("#lblPackageName").html("required");
        return false;
    }
    else if ($("#txtPackagePrice").val() == "") {
        $("#lblPackagePrice").html("required");
        return false;
    }
    else if ($("#hiddenTestId").val() == "") {
    alert("Select tests");
        return false;
    }
    else {
        return true;
    }
}

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function specimenValidate() {

    if ($("#txtSampleType").val() == "") {
        $("#txtSampleType").addClass("has-error");
        $("#txtSampleType").attr('placeholder', "Sample type required *");        
        return false;
    }
    else if ($("#txtQuantity").val() == "") {
        $("#txtQuantity").addClass("has-error");
        $("#txtQuantity").attr('placeholder', "Quantity required *");
        return false;
    }
    else if ($("#txtTimePeriod").val() == "") {
        $("#txtTimePeriod").addClass("has-error");
        $("#txtTimePeriod").attr('placeholder', "Time period required *");
        return false;
    }
    else {
        return true;
    }
}

function editSpecimenValidate() {

    if ($("#txtEditSampleType").val() == "") {
        $("#txtEditSampleType").addClass("has-error");
        $("#txtEditSampleType").attr('placeholder', "Sample type required *");
        return false;
    }
    else if ($("#txtEditQuantity").val() == "") {
        $("#txtEditQuantity").addClass("has-error");
        $("#txtEditQuantity").attr('placeholder', "Quantity required *");
        return false;
    }
    else if ($("#txtEditTimePeriod").val() == "") {
        $("#txtEditTimePeriod").addClass("has-error");
        $("#txtEditTimePeriod").attr('placeholder', "Time period required *");
        return false;
    }
    else {
        return true;
    }
}
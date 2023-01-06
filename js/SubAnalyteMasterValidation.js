function subAnalyteValidate() {

    if ($("#selAnalyte").val() == "") {
        $("#selAnalyte").addClass("has-error");
        alert("Select Analyte");
        return false;
    }
    else if ($("#txtSubAnalyteName").val() == "") {
        $("#txtSubAnalyteName").addClass("has-error");
        $("#txtSubAnalyteName").attr('placeholder', "Sub analyte name required *");        
        return false;
    }
    else {
        return true;
    }
}

function editSubAnalyteValidate() {

    if ($("#selEditAnalyte").val() == "") {
        $("#selEditAnalyte").addClass("has-error");
        alert("Select Analyte");
        return false;
    }
    else if ($("#txtEditSubAnalyteName").val() == "") {
        $("#txtEditSubAnalyteName").addClass("has-error");
        $("#txtEditSubAnalyteName").attr('placeholder', "Sub analyte name required *");
        return false;
    }
    else {
        return true;
    }
}
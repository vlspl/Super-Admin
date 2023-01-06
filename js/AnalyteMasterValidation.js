function analyteValidate() {    
    if ($("#txtAnalyteName").val() == "") {
        
        $("#txtAnalyteName").addClass("has-error");
        $("#txtAnalyteName").attr('placeholder', "Analyte required *");        
        return false;
    }
    else {
        return true;
    }
}

function editAnalyteValidate() {
   
    if ($("#txtAnalyte").val() == "") {

        $("#txtAnalyte").addClass("has-error");
        $("#txtAnalyte").attr('placeholder', "Analyte required *");
        return false;
    }
    else {
        return true;
    }
}
function sectionValidate() {

    if ($("#txtSectionName").val() == "") {
        $("#txtSectionName").addClass("has-error");
        $("#txtSectionName").attr('placeholder', "Section required *");        
        return false;
    }
    else {
        return true;
    }
}

function editSectionValidate() {

    if ($("#txtEditSectionName").val() == "") {
        $("#txtEditSectionName").addClass("has-error");
        $("#txtEditSectionName").attr('placeholder', "Section required *");
        return false;
    }
    else {
        return true;
    }
}
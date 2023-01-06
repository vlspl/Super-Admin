function profileValidate() {

    if ($("#selSection").val() == "") {
        $("#selSection").addClass("has-error");
        alert("Select section");
        return false;
    }
    else if ($("#txtProfileName").val() == "") {
        $("#txtProfileName").addClass("has-error");
        $("#txtProfileName").attr('placeholder', "Profile name required *");        
        return false;
    }
    else {
        return true;
    }
}

function editProfileValidate() {

    if ($("#selEditSection").val() == "") {
        $("#selEditSection").addClass("has-error");
        alert("Select section");
        return false;
    }
    else if ($("#txtEditProfileName").val() == "") {
        $("#txtEditProfileName").addClass("has-error");
        $("#txtEditProfileName").attr('placeholder', "Profile name required *");
        return false;
    }
    else {
        return true;
    }
}
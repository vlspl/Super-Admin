function changePasswordValidate() {

    if ($("#txtOldPassword").val() == "") {
        $("#txtOldPassword").addClass("has-error");
        $("#txtOldPassword").attr('placeholder', "Old password required *");        
        return false;
    }
    else if ($("#txtNewPassword").val() == "") {
        $("#txtNewPassword").addClass("has-error");
        $("#txtNewPassword").attr('placeholder', "New password required *");        
        return false;
    }
    else if ($("#txtConfirmPassword").val() == "") {
        $("#txtConfirmPassword").addClass("has-error");
        $("#txtConfirmPassword").attr('placeholder', "Please confirm password *");        
        return false;
    }
    else if ($("#txtConfirmPassword").val() != $("#txtNewPassword").val()) {
        $("#txtConfirmPassword").addClass("has-error");
        $("#txtConfirmPassword").val("");
        $("#txtConfirmPassword").attr('placeholder', "Confirm password must match New password");        
        return false;
    }
    else {
        return true;
    }
}
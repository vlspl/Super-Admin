$(document).ready(function () {


    $("#btnLogin").click(function () {
        if ($("#txtUserName").val() == "" || $("#txtpassword").val() == "") {
            $("#lblMessage").html("Enter username and password");
            return false;
        }
        else {
            return true;
        }
    });

});
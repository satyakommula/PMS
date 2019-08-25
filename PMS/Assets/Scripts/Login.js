$(document).ready(function () {
    var url = $("#hdnError").val();
    if (url.indexOf("invalidUser") != -1) {
        $(".alert").removeClass("hide");
        $("#divmsg").html("Invalid User, try again later.");
        window.setTimeout(function () {
            $(".alert").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 4000);
    }
    if (url.indexOf("invalidPassword") != -1) {
        $(".alert").removeClass("hide");
        $("#divmsg").html("Invalid Password, try again later.");
        window.setTimeout(function () {
            $(".alert").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 4000);
    }
    if (url.indexOf("error") != -1) {
        $(".alert").removeClass("hide");
        $("#divmsg").html("Something went wrong, try again later.");
        window.setTimeout(function () {
            $(".alert").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 4000);
    }
});
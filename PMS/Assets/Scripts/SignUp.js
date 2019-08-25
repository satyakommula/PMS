$(document).ready(function () {

    var url = window.location.href;
    if (url.indexOf("error") != -1) {
       // if ($("#hdnError").val() != undefined && $("#hdnError").val() != "") {
            // $('.toast').toast('show');
            $(".alert").removeClass("hide");
            $("#divmsg").html("Something went wrong, try again later.");
            window.setTimeout(function () {
                $(".alert").fadeTo(500, 0).slideUp(500, function () {
                    $(this).remove();
                });
            }, 4000);
      //  }
    }

    $("#formSignUp").submit(function () {

        //if (!$("#firmSignUp").valid()) {
        //    return false;
        //}

        if ($("#password").val() !== $("#confirm_password").val()) {
            var element = $("#confirm_password").get(0);
            element.setCustomValidity("Passwords Don't Match.");

          //  $("#confirm_password").setCustomValidity("Passwords Don't Match");
            return false;
        }

        var signUpModel = {
            FirstName: $("#first_name").val(),
            LastName: $("#last_name").val(),
            Email: $("#email").val(),
            Password: $("#password").val(),

        };
        $.ajax({
            type: 'POST',
            url: '/Account/SignUpUser/',
            contentType: "application/json",
            data: JSON.stringify(signUpModel),
            success: function (data) {
                if (data == 1) {
                    window.location.pathname = "Home/index"
                }
                else {
                    window.location.pathname = "Accout/SignUp?error"
                }
               

            },
            error: function (e) {
                window.location.pathname = "Accout/SignUp?error"
            }
        });
        return false;
    });
});

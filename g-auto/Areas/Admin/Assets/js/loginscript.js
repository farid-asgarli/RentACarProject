$(document).ready(function () {
    setTimeout(function () {
        $(".preloader").fadeOut();
    }, 500)

    $(window).bind('beforeunload', function () {
        $(".preloader").fadeIn();


    });

    $("#reg_btn").click(function () {

        window.location.href = location.origin + "/Admin/Home/Register";
    });




    $("#login_form").validate({

        rules: {
            Password: {
                required: true,
                minlength: 6
            },
            Email: {
                required: true
            }
        },
        messages: {
            Password: {
                required: "Password can not be empty.",
                minlength: "Not a valid password."
            },
            Email: {
                required: "Email can not be empty."
            }
        }

    });

    $("button[type='submit']").on("click", function (e) {
        if (!$("#login_form").valid) {
            e.preventDefault();
        }
    })


    $("#reg_form").validate({
        rules: {
            Code: {
                required: true,
                minlength: 12
            }
          
        },
        messages: {
            Code: {
                required: "Input is empty.",
                minlength: "Invalid input."
            }

        }
    })

    $("#login_btn").click(function () {

        window.location.href = location.origin + "/Admin/Home/Login";
    });

    function wordCarousel() {
        setTimeout(function () {
            $(".word-carousel").fadeOut(function () {

                $(".word-carousel").text("Commercials");
                $(this).fadeIn();


                setTimeout(function () {
                    $(".word-carousel").fadeOut(function () {

                        $(".word-carousel").text("Accounts");
                        $(this).fadeIn();


                        setTimeout(function () {
                            $(".word-carousel").fadeOut(function () {

                                $(".word-carousel").text("Reservations");
                                $(this).fadeIn();


                                setTimeout(function () {
                                    $(".word-carousel").fadeOut(function () {

                                        $(".word-carousel").text("Activities");
                                        $(this).fadeIn();


                                        setTimeout(function () {
                                            $(".word-carousel").fadeOut(function () {

                                                $(".word-carousel").text("Contacts");
                                                $(this).fadeIn();


                                                setTimeout(function () {
                                                    $(".word-carousel").fadeOut(function () {

                                                        $(".word-carousel").text("Stuff");
                                                        $(this).fadeIn();

                                                    })
                                                }, 3000)
                                            })
                                        }, 3000)
                                    })
                                }, 3000)
                            })
                        }, 3000)
                    })
                }, 3000)
            })
        }, 3000)
    }

    wordCarousel();
    window.setInterval(function () {
        wordCarousel();
    }, 21000);
})
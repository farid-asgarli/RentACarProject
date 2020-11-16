$(document).ready(function () {

    $('input[name="phone"]').on('keypress', function (evt) {
        evt = (evt) ? evt : window.event;
        var charcode = (evt.which) ? evt.which : evt.keycode;
        if (charcode > 31 && (charcode < 48 || charcode > 57)) {
            return false;
        }
        return true;
    })

    //**********************************MASKING************************************************

    jQuery('input[name="Phone"]').mask('(000) 000-0000');

    //**********************************MASKING************************************************

    $.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[a-z\s]+$/i.test(value);
    }, "Please use only letters.");


    $("#profile-section").validate({
        rules: {
            FullName: {
                lettersonly: true,
                required: true,
                maxlength: 200,
                minlength: 6
            }, Phone: {
                minlength: 14,
                maxlength: 14,
                required: true
            }, Password: {
                required: true,
                minlength: 5
            }, Password_Confirm: {
                required: true,
                minlength: 5,
                equalTo: "#ps_main"
            }
        },
        messages: {
            FullName: {
                lettersonly: "Please enter characters only.",
                required: "Please enter your full name.",
            }, Phone: {
                minlength: "Please enter a valid phone number.",
                maxlength: "Please enter a valid phone number."
            }, Email: {
                required: "Email field can not be empty."
            }, Password_Confirm: {
                equalTo: "Passwords do not match."
            }, Phone: {
                required: "Please add your phone number."
            }
        }
    });



    $("#next_btn").click(function (e) {

        if ($("#profile-section").valid()) {

            $.ajax({
                url: '/Admin/Home/EmailMatchCheck?email=' + $("#prof_email_field").val(),
                type: 'GET',
                dataType: "json",
                success: function (response) {

                    console.log(response);

                    if (response === "true") {
                        $("#profilepage_1").fadeOut("fast", function () {

                            $("#profilepage_2").show();
                        });
                    } else {
                        e.preventDefault();
                        $.toast("Email is already in use.");

                    }

                },

                error: function (error) {
                    console.log(error)

                }
            });


        }

    });

    $("#prev_btn").click(function () {

        $("#profilepage_2").fadeOut("fast", function () {

            $("#profilepage_1").show();
        });
    })

    $("#finish_btn").on("click", function () {

        if ($("#profile-section").valid()) {




            var formData = new FormData($("#profile-section")[0]);

            $.ajax({
                url: '/Admin/Home/SettingsSetup',
                type: 'POST',
                data: formData,
                enctype: 'multipart/form-data',
                cache: false,
                contentType: false,
                processData: false,
                success: function (response) {

                    if (response === "true") {
                        $("#profile-section").fadeOut(function () {

                            $("#success-section-finish").fadeIn();

                            setTimeout(function () {
                                window.location.href = location.origin + "/Admin/Home/Login";


                            }, 2000)

                        })
                    } else {
                        $.toast("Your session has time out. Redirecting...");
                        setTimeout(function () {
                            window.location.href = location.origin + "/Admin/Home/Login";


                        }, 2000)
                    }

                },
                error: function (error) {
                    console.log(error);

                    $(".error-message").text("Error Message: " + error.status + "")
                    $(".error-code").text("Error Code: " + error.statusText + "")
                    setTimeout(function () {
                        $("#loading-section").fadeOut(function () {
                            $("#error-section").fadeIn();


                        });



                    }, 3000);

                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });

        }
    })

    var imageid;


    function ImageUpload(imageId) {


        function readURL(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $("[id='multi-image-" + imageId + "']").attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }



        $("[id='change-image-" + imageId + "']").change(function (e) {
            var fileName = e.target.files[0].name;
            $("[id='multi-image-" + imageId + "']").attr('title', fileName); //display image title on upload
            $("[id='multi-image-" + imageId + "']").parent().find(".remove-image-button").show();
            $("[id='multi-image-" + imageId + "']").hide().fadeTo(500, 1);


            readURL(this);
        });


    }
    $(".btn-change").each(function () { //change image
        var This = $(this);

        imageid = This.data('id');
        ImageUpload(imageid);

    });

})
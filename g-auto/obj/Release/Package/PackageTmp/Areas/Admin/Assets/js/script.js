

$(document).ready(function () {

    $(window).bind('beforeunload', function () {
        $(".preloader").fadeIn();
       

    });

    $(window).on("load",function () {
        $(".preloader").fadeOut(500);

    });

    //$(".side-menu").on("click", function () {
    //    $(".preloader").fadeIn();
    //});
    //$(".rld-data").on("click", function () {
    //    $(".preloader").fadeIn();
    //});

    //$(".action-pr-btn").on("click", function () {
    //    $(".preloader").fadeIn();
    //});



    autosize($('.custom_textarea'));

    AOS.init();

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



    function GetMonthName(monthNumber) {
        var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        return months[monthNumber - 1];
    }

    function GetFullDate(postdate) {
        var dateString = postdate.substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = GetMonthName(currentTime.getMonth() + 1);
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        var hour = ("00" + currentTime.getHours()).substr(-2)
        var min = ("00" + currentTime.getMinutes()).substr(-2)
        var date = month + " " + day + " " + year + ", " + hour + ":" + min;

        return date;
    }

    //multi-purpose image upload - end

    //multiple image upload start

    var imagesPreview = function (input, index) {

        if (input.files) {
            var filesAmount = input.files.length;

            console.log(input.files[1])

            for (i = 0; i < filesAmount; i++) {
                var reader = new FileReader();

                reader.onload = function (event) {


                    var div1 = $('<div class="w-24 h-24 relative image-fit mb-5 mr-5 cursor-pointer zoom-in"></div>');
                    var img = $('<img class="rounded-md" alt="Midone Tailwind HTML Admin Template" src="' + event.target.result + '">')

                    div1.append(img);

                    $(".imgparent").append(div1).hide().slideDown();



                }

                reader.readAsDataURL(input.files[i]);
            }
        }

    };


    $('#imagemultiple').on('change', function () {
        $(".imgparent").empty();

        imagesPreview(this);
    });





    //multiple image upload end





    $(".remove-image-button").each(function () { //remove image from preview
        var This = $(this);


        if ($(this).prev().attr('title') == "Default") {
            This.hide();
        }

        This.on('click', (e) => {



            This.prev().hide().fadeTo(500, 1).attr('src', "/Areas/Admin/Assets/images/profile-13.jpg");
            This.prev().attr('title', "Default");
            This.parent().next().find("input").val("");
            This.hide();
        });


    });



    //benefit button start


    $("#addbenefitbtn").on('click', function () {

        var div1 = $('<div style="position:relative" class="form-group mt-3"></div>');
        var div2 = $('<div class="mt-2 mb-2 relative"></div>');
        var inp = $('<input  type="text" name="Benefits" class="form-control input w-full border mt-2 bf" placeholder="Additional Benefit" />')
        var rmvbtn = $('<button   type="button" class="rmvbenefitbtn  button w-36 inline-block mr-1 mb-2 mt-2 border border-theme-6 text-theme-6 dark:border-theme-6">Remove this field</button>');

        div1.append(div2);
        div2.append(inp);
        div1.append(rmvbtn);

        div1.hide().appendTo($(".main-view")).show('slow');

    });



    $(document).on("click", "button.rmvbenefitbtn", function () {
        $(this).parent().slideUp(function () {
            $(this).remove();
        })
    });



    $("#add-aboutbenefit").on('click', function () {

        var div2 = $('<div class="form-group mb-3"><label class= "control-label"> Title </label ><div class="mt-5"><input type="text" name="Benefits" placeholder="Add a Benefit" required value="" class="form-control input w-full border mt-2 mb-2 custom-val-input intro-y box input--lg" /></div><button type="button" class="rmvbenefitbtn  button w-36 inline-block mr-1 mb-2 mt-2 border border-theme-6 text-theme-6 dark:border-theme-6">Remove this field</button></div >');

        var div1 = $('#benefit-section');

        div1.append(div2);



    });

    //benefit button end




    //delete-confirmation-modal start

    function deleteModelCRUD(buttonId, controller, btnName) {

        $(btnName).on("click", function () {

            var Id = $(this).data("id");


            $("#delete-confirmation-modal").modal("show");


            $(buttonId).data("id", Id);
        });

        $(buttonId).click(function () {
            var Id = $(this).data("id");


            $.ajax({
                url: '/Admin/' + controller + '/Delete/' + Id,
                type: 'POST',
                cache: false,
                contentType: false,
                processData: false,
                success: function () {
                    $.toast("Succesfully Deleted");



                    $(btnName + "[data-id=" + Id + "]").parent().parent().parent().fadeOut(function () {
                        $(this).remove();
                    });

                    $("#delete-confirmation-modal").modal("hide");

                    $("#total_entries_count").text($("tr").length - 2)

                },
                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });
            return false;

        });
    }

    deleteModelCRUD("#confirm-delete-about", "About", ".del-about");
    deleteModelCRUD("#confirm-delete-blogc", "BlogCategories", ".del-bc");
    deleteModelCRUD("#confirm-delete-blog", "Blog", ".del-blog");
    deleteModelCRUD("#confirm-delete-brand", "Brand", ".del-brand");
    deleteModelCRUD("#confirm-delete-expert", "Expert", ".del-expert");
    deleteModelCRUD("#confirm-delete-feature", "FeatureSet", ".del-fs");
    deleteModelCRUD("#confirm-delete-gallery", "Gallery", ".del-gallery");
    deleteModelCRUD("#confirm-delete-model", "Model", ".del-model");
    deleteModelCRUD("#confirm-delete-pc", "ProductCategories", ".del-pc");
    deleteModelCRUD("#confirm-delete-product", "Product", ".del-product");
    deleteModelCRUD("#confirm-delete-service", "Service", ".del-service");
    deleteModelCRUD("#confirm-delete-si", "ServiceInfo", ".del-si");
    deleteModelCRUD("#confirm-delete-slider", "Slider", ".del-slider");
    deleteModelCRUD("#confirm-delete-tag", "Tags", ".del-tag");
    deleteModelCRUD("#confirm-delete-testimonial", "Testimonial", ".del-testimonial");
    deleteModelCRUD("#confirm-delete-vacancy", "Vacancy", ".del-vacancy");




    //delete-confirmation-modal end


    //API start


    var countryAPI = "https://restcountries.eu/rest/v2/all";

    $.getJSON(countryAPI).done(function (data) {
        var i = 0;
        $.each(data, function (index, item) {
            i++;
            $("<option></option>").html(item.name).attr("value", item.name).appendTo("#country");
        })


    }).fail(function () {
        alert("Fail!")
    });

    var carAPI = "https://private-anon-052ac84dcb-carsapi1.apiary-mock.com/manufacturers";

    $.getJSON(carAPI).done(function (data) {
        var a = [];
        $.each(data, function (index, item) {
            a.push(item.name);
        })




        $.each(a, function (index, value) {
            if (value.length == 3) {
                value = value.toUpperCase();
            }

            $("<option></option>").html(ucwords(value)).attr("value", ucwords(value)).appendTo("#cars");
        })

        function ucwords(str, force) {
            str = force ? str.toLowerCase() : str;
            return str.replace(/(^([a-zA-Z\p{M}]))|([ -][a-zA-Z\p{M}])/g,
                function (firstLetter) {
                    return firstLetter.toUpperCase();
                });
        }

    }).fail(function () {
        alert("Fail!")
    });


    //API end






    $(".row-img").first().removeClass("-ml-5");





    //form-validation custom start

    $("button[type=submit]").on('click', function (e) {
        $(".custom-val-input").each(function () {
            This = $(this);

            if (!$.trim($(This).val())) {
                e.preventDefault();
                var warningLabel = $('<label class="warning-label" style="color:red">This field cannot be empty.</label>');

                if (This.parent().parent().find(".warning-label").length) {

                    return false;

                } else {
                    This.parent().parent().append(warningLabel);
                    warningLabel.hide().fadeTo(300, 1);
                }


            }


        });
    })



    $("input.custom-val-input").keyup(function (e) {
        var warningLabel = $(this).parent().parent().find("label.warning-label");

        warningLabel.fadeOut(function () {
            warningLabel.remove();
        });


    })

    $("input[type=text]").attr('spellcheck', 'false');

    var fh = $(".form-horizontal");

    fh.find("input:text:visible:first").focus();

    //form-validation custom end








    //check list items start


    $("input[name='list-check']").on('change', function () {

        if ($(this).prop('checked')) {
            $(this).addClass("chckd");
            $(this).parent().parent().children().css("background-color", "#D2DFEA");
            $("#checkbutton").fadeTo(100, 1);

            if ($(".chckd").length == $("input[name='list-check']").length) {
                $(".mark-all").addClass("mark-done");
                $(".mark-all").text("Unmark All");
                $("#checkbutton").fadeTo(100, 1);
            }
        }
        else {
            $(this).parent().parent().children().css("background-color", "");
            $(this).removeClass("chckd");
            if ($(".chckd").length == 0) {
                $(".mark-all").removeClass("mark-done");
                $(".mark-all").text("Mark All");
                $("#checkbutton").fadeTo(100, 0);
            }
        }
        var a = $("input[name='list-check']");

        if (!a.filter(":checked").length) { //hide if none is selected
            $("#checkbutton").fadeTo(100, 0, function () {
                $(this).hide();
            });
        }
    });




    $("#checkbutton").on('click', function () {

        $("#total_delete_count").text($(".chckd").length)

        $("#delete-all-modal").modal("toggle");



    })


    $("#confirm-delete-all").on("click", function () {


        $("input[name='list-check']").each(function () {

            if ($(this).hasClass('chckd')) {
                var Id = $(this).data('id');
                var controller = $(this).data('controller');
                var url = "/Admin/" + controller + "/Delete?id=" + Id;
                $(this).parent().parent()
                    .children('td, th')
                    .animate({ paddingBottom: 0, paddingTop: 0 })
                    .wrapInner('<div />')
                    .children()
                    .slideUp(function () {
                        $(this).closest('tr').remove();
                    });;

                $.ajax({
                    url: url,
                    type: 'POST',
                    success: function (data) {
                        $.toast("Succesfully deleted item (Id: " + Id + ").");
                        var a = $("input[name='list-check']");
                        if (!a.length) {
                            $("#checkbutton").fadeTo(100, 0, function () {
                                $(this).parent().next().text("Nothing to show here");
                                $("table").fadeTo(100, 0);
                            });
                        }
                    },
                    error: function (error) {

                        console.log(error);
                        $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");

                    }
                });


            }
        });



        $("#delete-all-modal").modal("hide");
        


        $("#checkbutton").fadeTo(100, 0);

    });


    $(".mark-all").on("click", function () {
        if (!$(this).hasClass("mark-done")) {
            $(this).addClass("mark-done");
            $(this).text("Unmark All");
            var a = $("input[name='list-check']");
            a.prop("checked", true);
            a.addClass("chckd");
            a.parent().parent().children().css("background-color", "#D2DFEA");
            $("#checkbutton").fadeTo(100, 1);
        } else {
            $(this).removeClass("mark-done");
            $(this).text("Mark All");

            var a = $("input[name='list-check']");
            a.prop("checked", false);
            a.removeClass("chckd");
            a.parent().parent().children().css("background-color", "");
            $("#checkbutton").fadeTo(100, 0);
        }

    });




    //check list items end

   

    //content details modal start

    var contentDetails = function () {

        var This
        var Content;
        var Id;

        $(".content-details-1").each(function () {

            This = $(this);


            Content = This.data('content');
            Id = This.data('id');

            var div1 = $('<div class="modal details-modal" id="content-modal-' + Id + '-' + 1 + '"></div>');
            var div2 = $('<div class="modal__content"></div>');
            var div3 = $('<div class="relative p-5">' + Content + '</div>');
            var div4 = $('<div class="text-3xl mt-5 ml-5 mb-5 pt-5">Preview</div>');


            div1.append(div2);
            div2.append(div4);
            div2.append(div3);

            $("body").append(div1);


        });
        $(".content-details-2").each(function () {

            This = $(this);


            Content = This.data('content');
            Id = This.data('id');

            var div1 = $('<div class="modal details-modal" id="content-modal-' + Id + '-' + 2 + '"></div>');
            var div2 = $('<div class="modal__content"></div>');
            var div3 = $('<div class="relative flex items-center p-5">' + Content + '</div>');
            var div4 = $('<div class="text-3xl mt-5 ml-5 mb-5 pt-5">Preview</div>');


            div1.append(div2);
            div2.append(div4);
            div2.append(div3);

            $("body").append(div1);


        });


    }


    contentDetails();
    //content details modal end


    //admin profile update start



    $("#submitForm").validate({

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
            }, Email: {
                required: true,
            }
        },
        messages: {
            FullName: {
                lettersonly: "Please enter characters only.",
                required: "Please enter your full name.",
            }, Phone: {
                minlength: "Please enter a valid phone number.",
                maxlength: "Please enter a valid phone number.",
                required: "Please add your phone number."
            }, Email: {
                required: "Email field can not be empty."
            }
        }

    });

    $("#submitForm").submit(function () {
        if ($("#submitForm").valid()) {

            var formData = new FormData($(this)[0]);


            $.ajax({
                url: '/Admin/Admin/UpdateAdminProfile',
                type: 'POST',
                data: formData,
                cache: false,
                contentType: false,
                enctype: 'multipart/form-data',
                processData: false,
                success: function (response) {
                    $(".admin_name").text(response.FullName);
                    $("#admin_email").text(response.Email);
                    $("#admin_phone").text(response.Phone);
                    $(".prof_image_holder").attr("src", "/Uploads/" + response.ProfilePicture)

                },
                complete: function (data) {
                    $.toast("Succesfully Updated");


                }
            });
            return false;
        }
    });


    $("#saveAccount").click(function (e) {
        var data = $("#updateAccount").serialize();

        var validInput = true;

        $(".custom-val-input").each(function () {

            This = $(this);

            if (!$.trim($(This).val())) {
                validInput = false;
                e.preventDefault();
                var warningLabel = $('<label class="warning-label" style="color:red">This field cannot be empty.</label>');

                if (This.parent().parent().find(".warning-label").length) {

                    return false;

                } else {
                    This.parent().parent().append(warningLabel);
                    warningLabel.hide().fadeTo(300, 1);
                }

                //return false;
            }


        });
        if (validInput) {
            $.ajax({

                type: "Post",
                url: "/Admin/Admin/UpdateAdminAccount",
                data: data,
                success: function (result) {
                    if (result == true) {
                        $.toast("Succesfully Updated, Redirecting...");

                        setTimeout(function () {
                            location.reload();  //Refresh page
                        }, 3000);

                    }

                },
                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            })
        }

    })


    //admin profile update end

    //item visibility update start

    function updateIsActive(controller) {
        $(".input-activate-" + controller + "").on('change', function () {
            var Id = $(this).data('id');
            $.ajax({
                url: '/Admin/' + controller + '/UpdateActive/' + Id,
                type: 'POST',
                cache: false,
                contentType: false,
                enctype: 'multipart/form-data',
                processData: false,
                success: function (response) {
                    $.toast("Item visibility updated.");

                },
                error: function (error) {
                    console.log(error);

                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });
        })


    }

    updateIsActive("Blog");
    updateIsActive("About");
    updateIsActive("Brand");
    updateIsActive("Expert");
    updateIsActive("Gallery");
    updateIsActive("Model");
    updateIsActive("Product");
    updateIsActive("Service");
    updateIsActive("Slider");
    updateIsActive("Testimonial");
    updateIsActive("Vacancy");




    //item visibility update end



    //message-check

    $(".messages_container").on("change",".msg_chck_btn", function () {

        

        if (!$(this).hasClass("msg_chck")) {
            $(this).addClass("msg_chck");

            if ($(".msg_chck").length) {
                $("#delete_marked").fadeIn();
            }

            if ($(".msg_chck").length == $(".msg_chck_btn").length) {
              

                var a = $(".mark_all_messages");
                a.prop("checked", true);
                a.addClass("mark-true");

               
            }
          

        } else {
            $(this).removeClass("msg_chck");

            if ($(".msg_chck").length==0) {
                $("#delete_marked").fadeOut();
            }
          
            if ($(".msg_chck").length == 0) {


                var a = $(".mark_all_messages");
                a.prop("checked", false);
                a.removeClass("mark-true");


            }
        }
    })


    $("#delete_marked").on("click", function () {
        $("#total_delete_count").text($(".msg_chck").length);

        $("#delete-all-modal").modal("toggle");
    })

    $("#confirm-delete-messages").on("click", function () {


        if ($(".msg_chck_btn").length == $(".msg_chck").length) {
            $(".mark_all_messages").fadeOut(function(){
                $(this).remove();
                $('.no-content').append($('<div class="row"><div class= "col-lg-12"><h2 class="intro-y  text-center m-5">Nothing Here.</h2></div></div >'))

            })
        }


        $(".mark_all_messages").prop("checked", false);
        $(".mark_all_messages").removeClass("mark-true");
        $("#delete-all-modal").modal("hide");
        $("#delete_marked").fadeOut();
        $(".msg_chck").each(function () {

            var Id = $(this).data('id');


            $.ajax({
                url:"/Admin/Message/Delete?id=" + Id,
                type: 'POST',
                success: function (response) {
                    $.toast("Succesfully deleted message (Id: " + Id + ").");
                    $(".msg_chck[data-id=" + Id + "]").parent().parent().parent().parent().remove();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });

    })

    $(".mark_all_messages").on("click", function () {
        if (!$(this).hasClass("mark-true")) {
            $(this).addClass("mark-true")

            var a = $(".msg_chck_btn");
            a.prop("checked", true);
            a.addClass("msg_chck");
          
            $("#delete_marked").fadeIn();
        } else {
            $(this).removeClass("mark-true")
            var a = $(".msg_chck_btn");
            a.prop("checked", false);
            a.removeClass("msg_chck");
            //a.parent().parent().children().css("background-color", "#D2DFEA");
            $("#delete_marked").fadeOut();
        }

    });
    //message-check




    //toggle-count start



    $(".element-toggle").on('change', function () {
        console.log("works");
        var formData = new FormData($("#submitToggleForm")[0]);
        $.ajax({
            url: '/Admin/Layout/UpdateLayout',
            type: 'POST',
            data: formData,
            cache: false,
            enctype: 'multipart/form-data',
            contentType: false,
            processData: false,
            success: function (response) {
                $.toast("Succesfully Updated");
            },
            error: function (error) {
                console.log(error);
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
            }
        });
        return false;


    });

    $(".adminsettings-toggle").on('change', function () {
        console.log("works");
        var formData = new FormData($("#submitASettingsForm")[0]);
        $.ajax({
            url: '/Admin/Settings/UpdateSettings',
            type: 'POST',
            data: formData,
            cache: false,
            enctype: 'multipart/form-data',
            contentType: false,
            processData: false,
            success: function (response) {
                $.toast("Succesfully Updated");
            },
            error: function (error) {

                console.log(error);

                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
            }
        });
        return false;


    });

    $(".ref-req").on("change", function () {

        window.location.reload();
    })

    function delayChangeInput() {
        var typingTimer;
        var doneTypingInterval = 1000;
        var $input = $(".adminsettings-input");


        $input.on('keyup', function () {
            clearTimeout(typingTimer);
            typingTimer = setTimeout(doneTyping, doneTypingInterval);
        });


        $input.on('keydown', function () {
            clearTimeout(typingTimer);
        });


        function doneTyping() {
            var formData = new FormData($("#submitASettingsForm")[0]);
            $.ajax({
                url: '/Admin/Settings/UpdateSettings',
                type: 'POST',
                data: formData,
                cache: false,
                enctype: 'multipart/form-data',
                contentType: false,
                processData: false,
                success: function (response) {
                    $.toast("Succesfully Updated");
                },
                error: function (error) {

                    console.log(error);

                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });
            return false;
        }
    }

    delayChangeInput();

    $(".img-change").on('change', function () {
        console.log("works");
        var formData = new FormData($("#submitToggleForm")[0]);
        $.ajax({
            url: '/Admin/Layout/UpdateLogo',
            type: 'POST',
            data: formData,
            cache: false,
            enctype: 'multipart/form-data',
            contentType: false,
            processData: false,
            success: function (response) {
                $.toast("Succesfully Updated");
            },
            error: function (error) {
                console.log(error);

                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
            }
        });
        return false;
    })





    $("#saveContactSection").on('click', function () {
        console.log("works");
        var formData = new FormData($("#submitToggleForm")[0]);
        $.ajax({
            url: '/Admin/Layout/UpdateContactSection',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                $.toast("Succesfully Updated");
            },
            error: function (error) {
                console.log(error);

                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
            }
        });
        return false;


    });

    $("#saveCompanySection").on('click', function () {
        console.log("works");
        var formData = new FormData($("#submitToggleForm")[0]);
        $.ajax({
            url: '/Admin/Layout/UpdateCompanySection',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                $.toast("Succesfully Updated");
            },
            error: function (error) {
                console.log(error);

                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
            }
        });
        return false;


    });

    $(".saveAboutSection").on('click', function () {
        console.log("works");
        var formData = new FormData($("#submitToggleForm")[0]);
        $.ajax({
            url: '/Admin/Layout/UpdateAboutSection',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                $.toast("Succesfully Updated");
            },
            error: function (error) {
                console.log(error);

                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
            }
        });
        return false;


    });


    $(".savePromoSection").on('click', function () {
        console.log("works");
        var formData = new FormData($("#submitToggleForm")[0]);
        $.ajax({
            url: '/Admin/Layout/UpdatePromoSection',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                $.toast("Succesfully Updated");
            },
            error: function (error) {
                console.log(error);

                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
            }
        });
        return false;


    });


    //toggle-count end


    //cancel shipment and cancel product start


    $("#cancel_order").on("click", function (e) {
        e.preventDefault();

        var Id = $(this).data("orderid");

        $("#btn-cancel-order").data("id", Id);

        $('#delete-confirmation-modal').modal('toggle');
    });


    $("#btn-cancel-order").on("click", function () {

        var Id = $(this).data("id");

        console.log(true);

        $.ajax({
            url: '/Admin/Sale/CancelOrder/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                $('#delete-confirmation-modal').modal('hide')
                console.log(response);
                var div = $('<div class="box p-5 mt-5"><div class= "flex mt-4"><div class="m-auto font-medium text-base text-theme-6">Order was cancelled</div></div ></div >')

                $.toast("Succesfully Cancelled");

                $(".info-container").fadeOut(function () {

                    $(this).remove()
                });


                $(".order_status").fadeOut(function () {
                    $(this).remove();
                })

                $(".order_status_container").append($('<div class="ml-auto order_status text-theme-6">Cancelled</div>'));

                $(".double-button-container").fadeOut(function () {

                    $(this).remove();
                    $("#ticket").append(div);

                });


            },
            error: function (error) {
                console.log(error);
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                $('#delete-confirmation-modal').modal('hide')
            }
        });

        return false;

    });

    //cancel shipment and cancel product end

    //create shipment and sell product start

    $("#confirm_order").on("click", function (e) {
        e.preventDefault();

        var Id = $(this).data("orderid");

        $("#btn-charge-order").data("id", Id);

        $('#charge-confirmation-modal').modal('toggle');

    });


    $("#btn-charge-order").on("click", function () {

        var Id = $(this).data("id");
        var button = $("#confirm_order");



        $.ajax({
            url: '/Admin/Sale/ConfirmOrder/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                $.toast("Succesfully Updated");
                $('#charge-confirmation-modal').modal('hide')
                $(".order_status").fadeOut(function () {
                    $(this).remove();
                })


                $(".order_status_container").append($('<div class="ml-auto order_status text-theme-10">Active</div>'));




                button.fadeOut(function () {

                    $(this).remove();
                    $(".double-button-container").append($('<button class="button w-40 bg-gray-200 text-gray-600 shadow-md ml-auto" id="ship_order" data-orderid="' + response + '">Create Shipment</button>'));

                })

            },
            error: function (error) {
                console.log(error);

                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
            }
        });
        return false;

    });



    $(".double-button-container").on("click", "#ship_order", function () {

        var Id = $(this).data("orderid");

        $("#btn-ship-order").data("id", Id);

        $('#shipping-confirmation-modal').modal('toggle');
    });



    //create shipment and sell product start



    $("#btn-ship-order").on("click", function () {

        var Id = $(this).data("id");
        var button = $("#ship_order");



        $.ajax({
            url: '/Admin/Sale/CreateShipment/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                $.toast("Succesfully Updated");
                $('#shipping-confirmation-modal').modal('hide');

                button.fadeOut(function () {
                    $(this).remove();
                })
                $("#cancel_order").fadeOut(function () {
                    $(this).remove();
                })

                var div = $('<div class="box p-5 mt-5 info-container"><div class= "flex"><div class="mr-auto">Shipment Created Date</div><div>' + GetMonthName(response[0].Month) + ' ' + response[0].Day + ' ' + response[0].Year + ', ' + response[0].Hour + ':' + response[0].Minute + '</div></div><div class="flex"><div class="mr-auto">ETA</div><div>' + GetMonthName(response[0].ESTMonth) + ' ' + response[0].ESTDay + ' ' + response[0].ESTYear + ', ' + response[0].ESTHour + ':' + response[0].ESTMinute + '</div></div><div class="flex mt-4"><div class="mr-auto">Shipment Status</div><div><span>Not Yet Delivered</span></div></div><div class="flex mt-4 pt-4 border-t border-gray-200"><div class="mr-auto font-medium text-base">Tracking ID</div><div class="font-medium text-base">#MID' + response[0].Id + '</div></div></div >');

                $("#ticket").append(div).hide().fadeIn();


            },
            error: function (error) {
                console.log(error);

                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
            }
        });
        return false;

    });

    //create shipment and sell product end




    //confirm reservation start


    $("#confirm_res").on("click", function (e) {

        e.preventDefault();

        var Id = $(this).data("orderid");

        $("#btn-charge-res").data("id", Id);

        $('#charge-confirmation-modal').modal('toggle');


    });

    $("#cancel_res").on("click", function (e) {

        e.preventDefault();

        var Id = $(this).data("orderid");

        $("#btn-cancel-res").data("id", Id);

        $('#delete-confirmation-modal').modal('toggle');


    });


    $("#btn-charge-res").on("click", function () {

        var Id = $(this).data("id");
        var button = $("#confirm_res");



        $.ajax({
            url: '/Admin/Reservation/ChargeReservation/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                $.toast("Succesfully Completed");
                $('#charge-confirmation-modal').modal('hide')
                $(".order_status").fadeOut(function () {
                    $(this).remove();
                })


                $(".order_status_container").append($('<div class="ml-auto order_status text-theme-10">Active</div>'));




                button.fadeOut(function () {

                    $(this).remove();

                })

            },
            error: function (error) {
                console.log(error);

                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
            }
        });
        return false;

    });



    $("#btn-cancel-res").on("click", function () {

        var Id = $(this).data("id");

        console.log(true);

        $.ajax({
            url: '/Admin/Reservation/CancelReservation/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                $('#delete-confirmation-modal').modal('hide')
                console.log(response);
                var div = $('<div class="box p-5 mt-5"><div class= "flex mt-4"><div class="m-auto font-medium text-base text-theme-6">Reservation was cancelled</div></div ></div >')

                $.toast("Succesfully Cancelled");

                $(".info-container").fadeOut(function () {

                    $(this).remove()
                });


                $(".order_status").fadeOut(function () {
                    $(this).remove();
                })

                $(".order_status_container").append($('<div class="ml-auto order_status text-theme-6">Cancelled</div>'));

                $(".double-button-container").fadeOut(function () {

                    $(this).remove();
                    $("#ticket").append(div);

                });


            },
            error: function (error) {
                console.log(error);

                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                $('#delete-confirmation-modal').modal('hide')
            }
        });

        return false;

    });

    //confirm reservation end





    $(".confirm_app_service").on("click", function (e) {

        e.preventDefault();

        var Id = $(this).data("id");

        $("#btn-confirm-app").data("id", Id);

        $('#confirm-confirmation-modal').modal('toggle');


    });

    $(".status_container").on("click", ".cancel_app_service", function (e) {

        e.preventDefault();

        var Id = $(this).data("id");

        $("#btn-cancel-app").data("id", Id);

        $('#delete-confirmation-modal-app').modal('toggle');

    });


    $(".status_container").on("click", ".finish_app_service", function (e) {

        e.preventDefault();

        console.log("works");

        var Id = $(this).data("id");

        $("#btn-finish-app").data("id", Id);

        $('#finish-confirmation-modal').modal('toggle');

    });

    $("#btn-confirm-app").on("click", function () {


        var Id = $(this).data("id");

        //var This = $(this);

        console.log(true);


        $.ajax({
            url: '/Admin/Appointment/ConfirmAppointment/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                $('#confirm-confirmation-modal').modal('hide');

                var This = $(".status_container").find(`[data-id='${Id}']`);

                $.toast("Succesfully Confirmed");

                This.parent().parent().prev().empty();

                This.parent().parent().prev().append($('<div class="flex items-center sm:justify-center text-theme-10 service_status"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="feather feather-activity w-4 h-4 mr-2"><polyline points="22 12 18 12 15 21 9 3 6 12 2 12"></polyline></svg> Active </div>'));


                $(".confirm_app_service[data-id=" + Id + "]").parent().append($('<a class="flex items-center mr-3 text-theme-1" href="javascript:;" id="finish_app_service" data-id="' + Id + '"> <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="feather feather-check w-4 h-4 mr-1"><polyline points="20 6 9 17 4 12"></polyline></svg> Finish</a><a class="flex items-center text-theme-6" href="javascript:;" id="cancel_app_service" data-id="' + Id + '"> <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="feather feather-x w-4 h-4 mr-1"><line x1="18" y1="6" x2="6" y2="18"></line><line x1="6" y1="6" x2="18" y2="18"></line></svg> Cancel </a>'))

                $(".confirm_app_service[data-id=" + Id + "]").remove();
                $(".cancel_app_service[data-id=" + Id + "]").remove();

            },
            error: function (error) {
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                //$('#delete-confirmation-modal').modal('hide')
                console.log(error);

            }
        });
    });



    $("#btn-finish-app").on("click", function () {

        var Id = $(this).data("id");

        console.log(true);


        $.ajax({
            url: '/Admin/Appointment/FinishAppointment/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                $('#finish-confirmation-modal').modal('hide');

                var This = $(".status_container").find(`[data-id='${Id}']`);

                $.toast("Succesfully Confirmed");

                This.parent().parent().prev().empty();

                This.parent().parent().prev().append($('<div class="flex items-center sm:justify-center text-theme-9 service_status"> <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="feather feather-check w-4 h-4 mr-2"><polyline points="20 6 9 17 4 12"></polyline></svg> Finished </div>'));


                $(".finish_app_service[data-id=" + Id + "]").parent().append($('<span>No Actions</span>'))

                $(".finish_app_service[data-id=" + Id + "]").remove();
                $(".cancel_app_service[data-id=" + Id + "]").remove();

            },
            error: function (error) {
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                //$('#delete-confirmation-modal').modal('hide')
                console.log(error);

            }
        });
    });




    $("#btn-cancel-app").on("click", function () {

        var Id = $(this).data("id");

        console.log(true);


        $.ajax({
            url: '/Admin/Appointment/CancelAppointment/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {

                $('#delete-confirmation-modal-app').modal('hide');

                $.toast("Succesfully Cancelled");


                var This = $(".status_container").find(`[data-id='${Id}']`);


                This.parent().parent().prev().empty();

                This.parent().parent().prev().append($('<div class="flex items-center sm:justify-center text-theme-6 service_status"> <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="feather feather-x-circle w-4 h-4 mr-2"><circle cx="12" cy="12" r="10"></circle><line x1="15" y1="9" x2="9" y2="15"></line><line x1="9" y1="9" x2="15" y2="15"></line></svg> Cancelled </div>'));


                $(".cancel_app_service[data-id=" + Id + "]").parent().append($('<span>No Actions</span>'))

                $(".confirm_app_service[data-id=" + Id + "]").remove();
                $(".cancel_app_service[data-id=" + Id + "]").remove();
                $(".finish_app_service[data-id=" + Id + "]").remove();

            },
            error: function (error) {
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                $('#delete-confirmation-modal-app').modal('hide')
                console.log(error);

            }
        });
    });



    $(".cancel_res_index").on("click", function (e) {


        e.preventDefault();

        var Id = $(this).data("id");

        $("#btn-cancel-res-index").data("id", Id);

        $('#delete-confirmation-modal-res').modal('toggle');

    });

    $("#btn-cancel-res-index").on("click", function () {

        var Id = $(this).data("id");


        $.ajax({
            url: '/Admin/Reservation/CancelReservation/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {

                $('#delete-confirmation-modal-res').modal('hide');

                $.toast("Succesfully Cancelled");


                var This = $(".status_container").find(`[data-id='${Id}']`);


                This.parent().parent().prev().empty();

                This.parent().parent().prev().append($('<div class="flex items-center sm:justify-center text-theme-6 service_status"> <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="feather feather-x-circle w-4 h-4 mr-2"><circle cx="12" cy="12" r="10"></circle><line x1="15" y1="9" x2="9" y2="15"></line><line x1="9" y1="9" x2="15" y2="15"></line></svg> Cancelled </div>'));


                $(".cancel_res_index[data-id=" + Id + "]").remove();


            },
            error: function (error) {
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                $('#delete-confirmation-modal-res').modal('hide');
                console.log(error);

            }
        });
    });


    $(".cancel_order_index").on("click", function (e) {


        e.preventDefault();

        var Id = $(this).data("id");

        $("#btn-cancel-order-index").data("id", Id);

        $('#delete-confirmation-modal-order').modal('toggle');

    });


    $("#btn-cancel-order-index").on("click", function () {

        var Id = $(this).data("id");


        $.ajax({
            url: '/Admin/Sale/CancelOrder/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {

                $('#delete-confirmation-modal-order').modal('hide');

                $.toast("Succesfully Cancelled");


                var This = $(".status_container").find(`[data-id='${Id}']`);


                This.parent().parent().prev().empty();

                This.parent().parent().prev().append($('<div class="flex items-center sm:justify-center text-theme-6 service_status"> <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="feather feather-x-circle w-4 h-4 mr-2"><circle cx="12" cy="12" r="10"></circle><line x1="15" y1="9" x2="9" y2="15"></line><line x1="9" y1="9" x2="15" y2="15"></line></svg> Cancelled </div>'));


                $(".cancel_order_index[data-id=" + Id + "]").remove();


            },
            error: function (error) {
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                $('#delete-confirmation-modal-order').modal('hide');
                console.log(error);

            }
        });

    });





    $(".cancel_news").on("click", function (e) {


        e.preventDefault();

        var Id = $(this).data("id");

        $("#btn-cancel-news").data("id", Id);

        $('#delete-confirmation-modal').modal('toggle');

    });

    $("#btn-cancel-news").on("click", function () {

        var Id = $(this).data("id");


        $.ajax({
            url: '/Admin/Newsletter/DeleteNews/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {

                $('#delete-confirmation-modal').modal('hide');

                $.toast("Succesfully Deleted");



                $(".cancel_news[data-id=" + Id + "]").parent().parent().parent().fadeOut(function () {
                    $(this).remove();
                });


            },
            error: function (error) {
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                $('#delete-confirmation-modal').modal('hide');
                console.log(error);

            }
        });
    });


    function mailPagination() {

        var indexer = $("input[name=pagecurrent]").val();

        $.ajax({
            url: '/Admin/Message/InboxScroll?part=' + indexer,
            type: 'GET',
            dataType: "json",
            success: function (response) {


                console.log(response);

                if (response.length > 0) {
                    $(".messages_container").empty();

                    for (var i = 0; i < response.length; i++) {

                        if (response[i].IsRead) {
                            var div = $('<div class="intro-y ><div class= "inbox__item inbox__item--active inline-block sm:block text-gray-700 bg-gray-100 border-b border-gray-200 is-read"><div class="flex px-5 py-3"><div class="w-56 flex-none flex items-center mr-10"><input class="input flex-none border border-gray-500 msg_chck_btn" data-id="' + response[i].Id + '" type="checkbox"><div class="w-6 h-6 flex-none image-fit relative ml-5"><img alt="Midone Tailwind HTML Admin Template" class="rounded-full" src="/Areas/Admin/Assets/images/Man-Person-People-Avatar-User-Happy-512.png"></div><div class="inbox__item--sender truncate ml-3 is-read-text">' + response[i].Name + '</div></div><div class="w-64 sm:w-auto truncate cursor-pointer details-modal-toggler" data-id="' + response[i].Id + '"> <span class="inbox__item--highlight is-read-text">' + response[i].Subject + '</span> </div><div class="inbox__item--time whitespace-no-wrap ml-auto pl-10 is-read-text">' + GetMonthName(response[i].PostMonth) + ' ' + response[i].PostDay + ', ' + response[i].PostHour + ':' + ("00" + response[i].PostMin).substr(-2) + '</div></div></div></div>');

                        }
                        else {
                            var div = $('<div class="intro-y ><div class= "inbox__item inbox__item--active inline-block sm:block text-gray-700 bg-gray-100 border-b border-gray-200"><div class="flex px-5 py-3"><div class="w-56 flex-none flex items-center mr-10"><input class="input flex-none border border-gray-500 msg_chck_btn" data-id="' + response[i].Id + '" type="checkbox"><div class="w-6 h-6 flex-none image-fit relative ml-5"><img alt="Midone Tailwind HTML Admin Template" class="rounded-full" src="/Areas/Admin/Assets/images/Man-Person-People-Avatar-User-Happy-512.png"></div><div class="inbox__item--sender truncate ml-3" style="font-weight:700">' + response[i].Name + '</div></div><div class="w-64 sm:w-auto truncate cursor-pointer details-modal-toggler" data-id="' + response[i].Id + '"> <span class="inbox__item--highlight" style="font-weight:700">' + response[i].Subject + '</span> </div><div class="inbox__item--time whitespace-no-wrap ml-auto pl-10" style="font-weight:700">' + GetMonthName(response[i].PostMonth) + ' ' + response[i].PostDay + ', ' + response[i].PostHour + ':' + ("00" + response[i].PostMin).substr(-2) + '</div></div></div></div>');

                        }
                        $(".messages_container").append(div);
                    }
                }


            },
            complete: function (data) {

            },
            error: function (error) {
                console.log(error);

                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");

            }
        });
    }


    $("#dec_page").click(function () {

        if ($("input[name=pagecurrent]").val() != 0) {
            $("input[name=pagecurrent]").val(function (i, oldval) {
                return --oldval;
            });

            mailPagination();
        }
        else {
            return false;
        }
    })

    $("#inc_page").click(function () {


        $.ajax({
            url: '/Admin/Message/MessageCountChecker/',
            type: 'GET',
            dataType: "json",
            success: function (response) {



                if ($("input[name=pagecurrent]").val() < Math.ceil(response / 50)) {
                    $("input[name=pagecurrent]").val(function (i, oldval) {
                        return ++oldval;
                    });
                    mailPagination();


                }
                else {
                    return false;
                }

            },
            complete: function (data) {

            },
            error: function (error) {
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                console.log(error);


            }
        });


    })





    $("#refresh_page").on("click", function () {
        location.reload();
    })





    $(".messages_container").on("click", ".details-modal-toggler", function () {

        var This = $(this);

        var Id = $(this).data("id");

        console.log(Id);

        $.ajax({
            url: '/Admin/Message/ModalContent/' + Id,
            type: 'GET',
            dataType: "json",
            success: function (response) {

                var Name = response[0].Name;
                var Phone = response[0].Phone;
                var Month = GetMonthName(response[0].Month);
                var Day = response[0].Day;
                var Min = ("00" + response[0].Min).substr(-2);
                var Hour = ("00" + response[0].Hour).substr(-2);
                var Content = response[0].Content;
                var Subject = response[0].Subject;
                var Email = response[0].Email;

                $("#details-modal").empty();

                if (response[0].IsReplied) {

                    $.ajax({
                        url: '/Admin/Message/ModalContentReply/' + Id,
                        type: 'GET',
                        dataType: "json",
                        success: function (response) {


                            var div = $('<div class="modal__content modal__content--xl"><div class="p-5"><div class="grid grid-cols-12 gap-5 mt-5"  id="modal_container"> <div class="col-span-12 sm:col-span-6 xxl:col-span-6 box bg-theme-1 p-5 cursor-pointer zoom-in"> <div class="font-medium text-base text-white">Name</div> <div class="text-theme-25">' + Name + '</div> </div> <div class="col-span-12 sm:col-span-6 xxl:col-span-6 box bg-theme-1 p-5 cursor-pointer zoom-in"> <div class="font-medium text-base text-white">Phone</div> <div class="text-theme-25">' + Phone + '</div> </div> <div class="col-span-12 sm:col-span-6 xxl:col-span-6 box bg-theme-1 p-5 cursor-pointer zoom-in"> <div class="font-medium text-base text-white">Email</div> <div class="text-theme-25">' + Email + '</div> </div> <div class="col-span-12 sm:col-span-6 xxl:col-span-6 box bg-theme-1 p-5 cursor-pointer zoom-in"> <div class="font-medium text-base text-white">Date</div> <div class="text-theme-25">' + Month + ' ' + Day + ', ' + Hour + ':' + Min + '</div> </div> <div class="col-span-12 sm:col-span-12 xxl:col-span-12 box p-5 cursor-pointer zoom-in"> <div class="font-medium text-base">Subject</div> <div class="text-gray-600">' + Subject + '</div> </div> <div class="col-span-12 sm:col-span-12 xxl:col-span-12 box p-5 cursor-pointer zoom-in"> <div class="font-medium text-base">Content</div> <div class="text-gray-600">' + Content + '</div> </div><div class="col-span-12 sm:col-span-12 xxl:col-span-12 box p-5 cursor-pointer zoom-in"><div class="font-medium text-base">This message was replied by ' + response[0].ReplyAdmin + ' on ' + GetMonthName(response[0].ReplyMonth) + ' ' + response[0].ReplyDay + ', ' + ("00" + response[0].ReplyHour).substr(-2) + ':' + ("00" + response[0].ReplyMin).substr(-2) + ' </div><div class="text-gray-600">' + response[0].ReplyContent + '</div></div </div> <div class="px-5 pb-8 text-center mt-5"> <button type="button" data-dismiss="modal" class="button w-24 border text-gray-700 mr-1">Dismiss</button> </div> </div> </div>');
                            $("#details-modal").append(div);


                        },
                        complete: function (data) {

                        },
                        error: function (error) {
                            console.log(error);

                            $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                        }
                    });



                } else {
                    var div = $('<div class="modal__content modal__content--xl"><div class="p-5"><div class="grid grid-cols-12 gap-5 mt-5"  id="modal_container"> <div class="col-span-12 sm:col-span-6 xxl:col-span-6 box bg-theme-1 p-5 cursor-pointer zoom-in"> <div class="font-medium text-base text-white">Name</div> <div class="text-theme-25">' + response[0].Name + '</div> </div> <div class="col-span-12 sm:col-span-6 xxl:col-span-6 box bg-theme-1 p-5 cursor-pointer zoom-in"> <div class="font-medium text-base text-white">Phone</div> <div class="text-theme-25">' + response[0].Phone + '</div> </div> <div class="col-span-12 sm:col-span-6 xxl:col-span-6 box bg-theme-1 p-5 cursor-pointer zoom-in"> <div class="font-medium text-base text-white">Email</div> <div class="text-theme-25">' + response[0].Email + '</div> </div> <div class="col-span-12 sm:col-span-6 xxl:col-span-6 box bg-theme-1 p-5 cursor-pointer zoom-in"> <div class="font-medium text-base text-white">Date</div> <div class="text-theme-25">' + GetMonthName(response[0].Month) + ' ' + response[0].Day + ', ' + ("00" + response[0].Hour).substr(-2) + ':' + ("00" + response[0].Min).substr(-2) + '</div> </div> <div class="col-span-12 sm:col-span-12 xxl:col-span-12 box p-5 cursor-pointer zoom-in"> <div class="font-medium text-base">Subject</div> <div class="text-gray-600">' + response[0].Subject + '</div> </div> <div class="col-span-12 sm:col-span-12 xxl:col-span-12 box p-5 cursor-pointer zoom-in"> <div class="font-medium text-base">Content</div> <div class="text-gray-600">' + response[0].Content + '</div> </div>    <div class="col-span-12 sm:col-span-12 xxl:col-span-12 box p-5 cursor-pointer zoom-in"><form id = "replyToContactMessage" method = "post"><div class="pt-4 pb-10 sm:py-4 flex items-center border-t border-gray-200"><input type="hidden" name="MessageId" value="' + response[0].Id + '" /><textarea class="chat__box__input input w-full h-16 resize-none border-transparent px-5 py-3 focus:shadow-none" rows="1" name="Content" minlength="10" required maxlength="1000" placeholder="Type your message..."></textarea><a href="javascript:;" id="replyToContactMessage-btn" class="w-8 h-8 sm:w-10 sm:h-10 block bg-theme-1 text-white rounded-full flex-none flex items-center justify-center mr-5"> <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="feather feather-send w-4 h-4"><line x1="22" y1="2" x2="11" y2="13"></line><polygon points="22 2 15 22 11 13 2 9 22 2"></polygon></svg> </a></div></form ></div ></div> <div class="px-5 pb-8 text-center mt-5"> <button type="button" data-dismiss="modal" class="button w-24 border text-gray-700 mr-1">Dismiss</button> </div> </div> </div>');
                    $("#details-modal").append(div);

                }



                $("#details-modal").modal("show");



            },
            complete: function (data) {
                $.ajax({
                    url: '/Admin/Message/MarkAsRead/' + Id,
                    type: 'POST',
                    cache: false,
                    contentType: false,
                    processData: false,
                    dataType: "json",
                    success: function (response) {

                        This.find(".inbox__item ").addClass("is-read");
                        This.find(".inbox__item--sender").addClass("is-read-text");
                        This.find(".inbox__item--time").addClass("is-read-text");
                        This.find(".inbox__item--highlight").addClass("is-read-text");

                    },
                    complete: function (data) {

                    },
                    error: function (error) {
                        console.log("error while updating");

                    }
                });
            },
            error: function (error) {
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                console.log(error);

            }
        });
    })


    jQuery("#replyToContactMessage").validate();

    $("#details-modal").on("click", "#replyToContactMessage-btn", function () {
        var contentval = $('input[name="Content"]').val();

        if ($("#replyToContactMessage").valid()) {
            var This = $(this);

            var formData = new FormData($("#replyToContactMessage")[0]);
            $.ajax({
                url: '/Admin/Message/ReplyToCustomer',
                type: 'POST',
                data: formData,
                cache: false,
                enctype: 'multipart/form-data',
                contentType: false,
                processData: false,
                success: function (response) {
                    $.toast("Reply was sent succesfully");




                    This.parent().parent().parent().fadeOut(function () {
                        $(this).remove();

                        var div = $(' <div class="col-span-12 sm:col-span-12 xxl:col-span-12 box bg-theme-9 p-5 cursor-pointer zoom-in text-center"><div class= "font-medium text-base text-white"> Reply was sent.</div></div >');

                        $("#modal_container").append(div);
                    })


                },
                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });
            return false;
        }


        else {
            $.toast("Reply content cannot be less than 10 characters");
        }

    });

    jQuery("#replyToReview").validate();

    $("#reply-modal").on("click", "#replyToReview-btn", function () {
        if ($("#replyToReview").valid()) {
            var This = $(this);

            var formData = new FormData($("#replyToReview")[0]);
            $.ajax({
                url: '/Admin/Review/ReplyToCustomerModel',
                type: 'POST',
                data: formData,
                cache: false,
                enctype: 'multipart/form-data',
                contentType: false,
                processData: false,
                success: function (response) {
                    $.toast("Reply was sent succesfully");


                    This.parent().parent().parent().fadeOut(function () {
                        $(this).remove();

                        var div = $(' <div class="col-span-12 sm:col-span-12 xxl:col-span-12 box bg-theme-9 p-5 cursor-pointer zoom-in text-center"><div class= "font-medium text-base text-white"> Reply was sent.</div></div >');

                        $("#modal_container").append(div);
                    })


                    var replyDiv = $('<div class="col-span-12 sm:col-span-6 xxl:col-span-6 box p-5 cursor-pointer zoom-in"><div class= "font-medium text-base"> Replied by ' + response.name + ' on ' + GetFullDate(response.xdb.PostedDate) + ' </div ><div class="text-gray-600 mt-3">' + response.xdb.Content + '</div></div >');


                    console.log($(".replies-container"));

                    setTimeout(function () {

                        $("#reply-modal").modal("hide");

                        $(".replies-container").append(replyDiv).hide().fadeIn();


                    }, 1000)

                },
                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });
            return false;
        }


        else {
            $.toast("Reply content cannot be less than 10 characters");
        }

    });

    $("#reply-modal").on("click", "#replyToReviewProduct-btn", function () {
        if ($("#replyToReview").valid()) {
            var This = $(this);

            var formData = new FormData($("#replyToReview")[0]);
            $.ajax({
                url: '/Admin/Review/ReplyToCustomerProduct',
                type: 'POST',
                data: formData,
                cache: false,
                enctype: 'multipart/form-data',
                contentType: false,
                processData: false,
                success: function (response) {
                    $.toast("Reply was sent succesfully");


                    This.parent().parent().parent().fadeOut(function () {
                        $(this).remove();

                        var div = $(' <div class="col-span-12 sm:col-span-12 xxl:col-span-12 box bg-theme-9 p-5 cursor-pointer zoom-in text-center"><div class= "font-medium text-base text-white"> Reply was sent.</div></div >');

                        $("#modal_container").append(div);
                    })


                    var replyDiv = $('<div class="col-span-12 sm:col-span-6 xxl:col-span-6 box p-5 cursor-pointer zoom-in"><div class= "font-medium text-base"> Replied by ' + response.name + ' on ' + GetFullDate(response.xdb.PostedDate) + ' </div ><div class="text-gray-600 mt-3">' + response.xdb.Content + '</div></div >');


                    console.log($(".replies-container"));

                    setTimeout(function () {

                        $("#reply-modal").modal("hide");

                        $(".replies-container").append(replyDiv).hide().fadeIn();


                    }, 1000)

                },
                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });
            return false;
        }


        else {
            $.toast("Reply content cannot be less than 10 characters");
        }

    });

    $("#cancel_search_messages").on("click", function () {
        mailPagination();

        $("#cancel_search_messages").fadeOut();
        $("#dec_page").fadeIn();
        $("#inc_page").fadeIn();

        $("#search_messages_input").val("");
    });

    $("#search_messages_input").on("keyup", function () {

        $("#delete_marked").fadeOut();

        if ($("#search_messages_input").val()) {
            $("#cancel_search_messages").fadeIn();
            $("#dec_page").fadeOut();
            $("#inc_page").fadeOut();
        } else {
            $("#cancel_search_messages").fadeOut();
            $("#dec_page").fadeIn();
            $("#inc_page").fadeIn();
        }
        $(".messages_container").empty();

        $.ajax({
            url: '/Admin/Message/MessageSearch?text=' + $("#search_messages_input").val(),
            type: 'GET',
            dataType: "json",
            success: function (response) {


                console.log(response);

                if (response.length > 0) {


                    for (var i = 0; i < response.length; i++) {

                        if (response[i].IsRead) {
                            var div = $('<div class="intro-y"><div class= "inbox__item inbox__item--active inline-block sm:block text-gray-700 bg-gray-100 border-b border-gray-200 is-read"><div class="flex px-5 py-3"><div class="w-56 flex-none flex items-center mr-10"><input class="input flex-none border border-gray-500 msg_chck_btn" type="checkbox" data-id="' + response[i].Id + '"><div class="w-6 h-6 flex-none image-fit relative ml-5"><img alt="Midone Tailwind HTML Admin Template" class="rounded-full" src="/Areas/Admin/Assets/images/Man-Person-People-Avatar-User-Happy-512.png"></div><div class="inbox__item--sender truncate ml-3 is-read-text">' + response[i].Name + '</div></div><div class="w-64 sm:w-auto truncate details-modal-toggler" data-id="' + response[i].Id + '"> <span class="inbox__item--highlight is-read-text">' + response[i].Subject + '</span> </div><div class="inbox__item--time whitespace-no-wrap ml-auto pl-10 is-read-text">' + GetMonthName(response[i].PostMonth) + ' ' + response[i].PostDay + ', ' + response[i].PostHour + ':' + ("00" + response[i].PostMin).substr(-2) + '</div></div></div></div>');

                        }
                        else {
                            var div = $('<div class="intro-y"><div class= "inbox__item inbox__item--active inline-block sm:block text-gray-700 bg-gray-100 border-b border-gray-200"><div class="flex px-5 py-3"><div class="w-56 flex-none flex items-center mr-10"><input class="input flex-none border border-gray-500 msg_chck_btn" type="checkbox" data-id="' + response[i].Id + '"><div class="w-6 h-6 flex-none image-fit relative ml-5"><img alt="Midone Tailwind HTML Admin Template" class="rounded-full" src="/Areas/Admin/Assets/images/Man-Person-People-Avatar-User-Happy-512.png"></div><div class="inbox__item--sender truncate ml-3" style="font-weight:700" >' + response[i].Name + '</div></div><div class="w-64 sm:w-auto truncate details-modal-toggler" data-id="' + response[i].Id + '"> <span class="inbox__item--highlight" style="font-weight:700">' + response[i].Subject + '</span> </div><div class="inbox__item--time whitespace-no-wrap ml-auto pl-10" style="font-weight:700">' + GetMonthName(response[i].PostMonth) + ' ' + response[i].PostDay + ', ' + response[i].PostHour + ':' + ("00" + response[i].PostMin).substr(-2) + '</div></div></div></div>');

                        }
                        $(".messages_container").append(div);
                    }
                }


            },
            complete: function (data) {

            },
            error: function (error) {
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                console.log(error);

            }
        });
    });




    function toggleDetailsTag(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/Tags/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {

                    console.log(GetFullDate(response.PostDate));

                    $(".tag_modal_link").attr("href", "/Admin/Tags/Update/" + Id)
                    $(".tag_modal_name").text(response.Name);
                    $(".tag_modal_id").text(response.Id);
                    $(".tag_modal_post").text("Posted by " + response.Admin + " on " + GetFullDate(response.PostDate));
                    if (response.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.EditAdmin + " on " + GetFullDate(response.EditDate));
                    }


                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error)
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });


        })
    }

    toggleDetailsTag("tagmodal")


    function toggleDetailsSlider(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/Slider/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {

                    console.log(GetFullDate(response.PostDate));

                    $(".tag_modal_link").attr("href", "/Admin/Tags/Update/" + Id)
                    $(".tag_modal_name").text(response.Name);
                    $(".tag_modal_id").text(response.Id);
                    $(".tag_modal_sliderlink").text(response.Link);
                    $(".tag_modal_subtitle").text(response.Subtitle);
                    $(".tag_modal_img").attr("src", "/Uploads/" + response.Image);
                    $(".tag_modal_post").text("Posted by " + response.Admin + " on " + GetFullDate(response.PostDate));
                    if (response.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.EditAdmin + " on " + GetFullDate(response.EditDate));
                    }


                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error)
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });


        })
    }

    toggleDetailsSlider("slidermodal");

    function toggleDetailsSI(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/ServiceInfo/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {
                    console.log(response);


                    $(".tag_modal_link").attr("href", "/Admin/ServiceInfo/Update/" + Id)
                    $(".tag_modal_name").text(response.xdb.Name);
                    $(".tag_modal_id").text(response.xdb.Id);
                    $(".tag_modal_content").html(response.content);
                    $(".tag_modal_post").text("Posted by " + response.xdb.Admin + " on " + GetFullDate(response.xdb.PostDate));
                    if (response.xdb.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.xdb.EditAdmin + " on " + GetFullDate(response.xdb.EditDate));
                    }


                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error)
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });


        })
    }

    toggleDetailsSI("simodal");

    function toggleDetailsService(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/Service/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {
                    console.log(response.benefits);


                    $(".tag_modal_img_first").attr("src", "/Uploads/" + response.xdb.ImageFirst);
                    $(".tag_modal_img_second").attr("src", "/Uploads/" + response.xdb.ImageSecond);

                    $(".tag_modal_link").attr("href", "/Admin/Service/Update/" + Id);
                    $(".tag_modal_totalviews").text(response.xdb.TotalViews);
                    $(".tag_modal_name").text(response.xdb.Name);
                    $(".tag_modal_id").text(response.xdb.Id);
                    $(".tag_modal_content").html(response.content);
                    $(".tag_modal_desc").html(response.desc);
                    $(".tag_modal_post").text("Posted by " + response.xdb.Admin + " on " + GetFullDate(response.xdb.PostDate));
                    if (response.xdb.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.xdb.EditAdmin + " on " + GetFullDate(response.xdb.EditDate));
                    }


                    $(".tag_modal_benefit").empty();


                    for (var i = 0; i < response.benefits.length; i++) {
                        var ul = $('<li>·  ' + response.benefits[i].Content + '</li>');

                        $(".tag_modal_benefit").append(ul);
                    }
                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");

                }
            });


        })
    }

    toggleDetailsService("servicemodal");


    function toggleDetailsPC(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/ProductCategories/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {

                    $(".tag_modal_link").attr("href", "/Admin/ProductCategories/Update/" + Id)
                    $(".tag_modal_name").text(response.xdb.Name);
                    $(".tag_modal_id").text(response.xdb.Id);
                    $(".tag_modal_post").text("Posted by " + response.xdb.Admin + " on " + GetFullDate(response.xdb.PostDate));
                    if (response.xdb.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.xdb.EditAdmin + " on " + GetFullDate(response.xdb.EditDate));
                    }

                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");

                }
            });


        })
    }

    toggleDetailsPC("pcmodal");

    function toggleDetailsProduct(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/Product/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {

                    console.log(response.images);
                    console.log(response.xdb);


                    $(".tag_modal_link").attr("href", "/Admin/Product/Update/" + Id);
                    $(".tag_modal_totalviews").text(response.xdb.TotalViews);
                    $(".tag_modal_name").text(response.xdb.Name);
                    $(".tag_modal_price").text("$" + response.xdb.Price);
                    $(".tag_modal_amount").text(response.xdb.Amount);
                    $(".tag_modal_id").text(response.xdb.Id);
                    $(".tag_modal_content").html(response.content);
                    $(".tag_modal_desc").html(response.desc);
                    $(".tag_modal_post").text("Posted by " + response.xdb.Admin + " on " + GetFullDate(response.xdb.PostDate));
                    if (response.xdb.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.xdb.EditAdmin + " on " + GetFullDate(response.xdb.EditDate));
                    }


                    var containerdiv = $('<div class="mx-6 tag_modal_slider"></div>');

                    $(".tag_slider_container").empty();

                    $(".tag_slider_container").append(containerdiv);



                    for (var i = 0; i < response.images.length; i++) {

                        var imgdiv = $('<div class="h-56 px-2"><div class= "h-full image-fit rounded-md overflow-hidden"><img alt="Midone Tailwind HTML Admin Template" src="/Uploads/' + response.images[i].Name + '" /></div></div>');


                        $(".tag_modal_slider").append(imgdiv);

                    }

                    $(".tag_modal_slider").slick({

                        infinite: true,
                        slidesToShow: 3,
                        slidesToScroll: 3
                    });

                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");

                }
            });


        })
    }

    toggleDetailsProduct("productmodal");

    function toggleDetailsModel(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/Model/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {


                    $(".tag_modal_link").attr("href", "/Admin/Model/Update/" + Id);
                    $(".tag_modal_name").text(response.xdb.Name);
                    $(".tag_modal_totalviews").text(response.xdb.TotalViews);
                    $(".tag_modal_id").text(response.xdb.Id);
                    $(".tag_modal_brand").text(response.xdb.BrandName);
                    $(".tag_modal_price").text("$" + response.xdb.Price);
                    $(".tag_modal_doors").text(response.xdb.Doors);
                    $(".tag_modal_engine").text(response.xdb.Engine);
                    $(".tag_modal_el").text(response.xdb.EngineLayout);
                    $(".tag_modal_ft").text(response.xdb.FuelType);
                    $(".tag_modal_hp").text(response.xdb.HorsePower);
                    $(".tag_modal_mass").text(response.xdb.Mass);
                    $(".tag_modal_mileage").text(response.xdb.Mileage);
                    $(".tag_modal_transmission").text(response.xdb.Transmission);
                    $(".tag_modal_year").text(response.xdb.Year);
                    $(".tag_modal_condition").text(response.xdb.Condition);

                    $(".tag_modal_content").html(response.content);
                    $(".tag_modal_desc").html(response.desc);
                    $(".tag_modal_post").text("Posted by " + response.xdb.Admin + " on " + GetFullDate(response.xdb.PostDate));
                    if (response.xdb.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.xdb.EditAdmin + " on " + GetFullDate(response.xdb.EditDate));
                    }


                    var containerdiv = $('<div class="mx-6 tag_modal_slider"></div>');

                    $(".tag_slider_container").empty();

                    $(".tag_slider_container").append(containerdiv);



                    for (var i = 0; i < response.xdb.images.length; i++) {

                        var imgdiv = $('<div class="h-56 px-2"><div class= "h-full image-fit rounded-md overflow-hidden"><img alt="Midone Tailwind HTML Admin Template" src="/Uploads/' + response.xdb.images[i].Name + '" /></div></div>');


                        $(".tag_modal_slider").append(imgdiv);

                    }

                    $(".tag_modal_slider").slick({

                        infinite: true,
                        slidesToShow: 2,
                        slidesToScroll: 2
                    });

                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");

                }
            });


        })
    }

    toggleDetailsModel("modelmodal");


    function toggleDetailsGallery(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/Gallery/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {

                    $(".tag_modal_link").attr("href", "/Admin/Gallery/Update/" + Id)
                    $(".tag_modal_name").text(response.xdb.Name);
                    $(".tag_modal_id").text(response.xdb.Id);
                    $(".tag_modal_content").html(response.content);
                    $(".tag_modal_img").attr("src", "/Uploads/" + response.xdb.Image);
                    $(".tag_modal_post").text("Posted by " + response.xdb.Admin + " on " + GetFullDate(response.xdb.PostDate));
                    if (response.xdb.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.xdb.EditAdmin + " on " + GetFullDate(response.xdb.EditDate));
                    }


                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error)
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });


        })
    }

    toggleDetailsGallery("gallerymodal");


    function toggleDetailsFS(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/FeatureSet/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {
                    console.log(response);


                    $(".tag_modal_link").attr("href", "/Admin/FeatureSet/Update/" + Id)
                    $(".tag_modal_name").text(response.xdb.Name);
                    $(".tag_modal_id").text(response.xdb.Id);
                    $(".tag_modal_price").text("$" + response.xdb.Price);
                    $(".tag_modal_content").html(response.content);
                    $(".tag_modal_post").text("Posted by " + response.xdb.Admin + " on " + GetFullDate(response.xdb.PostDate));
                    if (response.xdb.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.xdb.EditAdmin + " on " + GetFullDate(response.xdb.EditDate));
                    }


                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error)
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });


        })
    }

    toggleDetailsFS("fsmodal");

    function toggleDetailsExpert(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/Expert/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {
                    console.log(response);


                    $(".tag_modal_link").attr("href", "/Admin/Expert/Update/" + Id)
                    $(".tag_modal_year").text(response.Year + " years");
                    $(".tag_modal_name").text(response.Name);
                    $(".tag_modal_id").text(response.Id);
                    $(".tag_modal_fb").text(response.Fb);
                    $(".tag_modal_tw").text(response.Tw);
                    $(".tag_modal_lk").text(response.Lk);
                    $(".tag_modal_ig").text(response.Ig);
                    $(".tag_modal_img").attr("src", "/Uploads/" + response.Image);
                    $(".tag_modal_price").text("$" + response.Price);
                    $(".tag_modal_content").html(response.content);
                    $(".tag_modal_post").text("Posted by " + response.Admin + " on " + GetFullDate(response.PostDate));
                    if (response.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.EditAdmin + " on " + GetFullDate(response.EditDate));
                    }


                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });


        })
    }

    toggleDetailsExpert("expertmodal");


    function toggleDetailsBrand(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/Brand/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {
                    console.log(response);


                    $(".tag_modal_link").attr("href", "/Admin/Brand/Update/" + Id)
                    $(".tag_modal_name").text(response.Name);
                    $(".tag_modal_id").text(response.Id);
                    $(".tag_modal_country").text(response.Country);
                    $(".tag_modal_img").attr("src", "/Uploads/" + response.Image);
                    $(".tag_modal_post").text("Posted by " + response.Admin + " on " + GetFullDate(response.PostDate));
                    if (response.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.EditAdmin + " on " + GetFullDate(response.EditDate));
                    }


                    $(".tag_modal_benefit").empty();
                    for (var i = 0; i < response.Models.length; i++) {
                        var ul = $('<li>·  ' + response.Models[i].Name + '</li>');

                        $(".tag_modal_benefit").append(ul);
                    }

                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error)
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });


        })
    }

    toggleDetailsBrand("brandmodal");

    function toggleDetailsBC(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/BlogCategories/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {

                    console.log(GetFullDate(response.PostDate));

                    $(".tag_modal_link").attr("href", "/Admin/BlogCategories/Update/" + Id)
                    $(".tag_modal_name").text(response.Name);
                    $(".tag_modal_id").text(response.Id);
                    $(".tag_modal_post").text("Posted by " + response.Admin + " on " + GetFullDate(response.PostDate));
                    if (response.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.EditAdmin + " on " + GetFullDate(response.EditDate));
                    }


                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error)
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });


        })
    }

    toggleDetailsBC("bcmodal");



    function toggleDetailsBlog(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/Blog/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {


                    console.log(response.xdb);

                    $(".tag_modal_link").attr("href", "/Admin/Blog/Update/" + Id)
                    $(".tag_modal_totalviews").text(response.xdb.TotalViews);
                    $(".tag_modal_name").text(response.xdb.Name);
                    $(".tag_modal_id").text(response.xdb.Id);
                    $(".tag_modal_content").html(response.content);
                    $(".tag_modal_desc").html(response.desc);
                    $(".tag_modal_img").attr("src", "/Uploads/" + response.xdb.Image);
                    $(".tag_modal_post").text("Posted by " + response.xdb.Admin + " on " + GetFullDate(response.xdb.PostDate));
                    if (response.xdb.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.xdb.EditAdmin + " on " + GetFullDate(response.xdb.EditDate));
                    }


                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");

                }
            });


        })
    }

    toggleDetailsBlog("blogmodal");



    function toggleDetailsAbout(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/About/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {
                    console.log(response);


                    $(".tag_modal_link").attr("href", "/Admin/About/Update/" + Id)
                    $(".tag_modal_year").text(response.xdb.Year + " years");
                    $(".tag_modal_name").text(response.xdb.Name);
                    $(".tag_modal_address").text(response.xdb.Address);
                    $(".tag_modal_phone").text(response.xdb.Phone);
                    $(".tag_modal_email").text(response.xdb.Email);
                    $(".tag_modal_id").text(response.xdb.Id);
                    $(".tag_modal_fb").text(response.xdb.Fb);
                    $(".tag_modal_tw").text(response.xdb.Tw);
                    $(".tag_modal_lk").text(response.xdb.Lk);
                    $(".tag_modal_ig").text(response.xdb.Ig);
                    $(".tag_modal_vm").text(response.xdb.Vm);
                    $(".tag_modal_pt").text(response.xdb.Pt);
                    $(".tag_modal_sk").text(response.xdb.Sk);
                    $(".tag_modal_img_first").attr("src", "/Uploads/" + response.xdb.ImageFirst);
                    $(".tag_modal_img_second").attr("src", "/Uploads/" + response.xdb.ImageSecond);
                    $(".tag_modal_content").html(response.content);
                    $(".tag_modal_post").text("Posted by " + response.xdb.Admin + " on " + GetFullDate(response.xdb.PostDate));
                    if (response.xdb.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.xdb.EditAdmin + " on " + GetFullDate(response.xdb.EditDate));
                    }


                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");

                }
            });


        })
    }

    toggleDetailsAbout("aboutmodal");

    function toggleDetailsTestimonial(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            console.log(Id);

            $.ajax({
                url: '/Admin/Testimonial/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {
                    console.log(response);


                    $(".tag_modal_link").attr("href", "/Admin/Brand/Update/" + Id)
                    $(".tag_modal_id").text(response.Id);
                    $(".tag_modal_content").text(response.Content);
                    if (response.Image === "Default") {

                        $(".tag_modal_img").attr("src", "/Assets/img/user-avatar-default.png");
                    } else {
                        $(".tag_modal_img").attr("src", "/Uploads/" + response.Image);

                    }

                    $(".tag_modal_post").text("Posted by " + response.Name + " on " + GetFullDate(response.PostDate));




                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");

                }
            });


        })
    }

    toggleDetailsTestimonial("testimonialmodal");

    function toggleDetailsVacancy(button) {
        $("." + button).on("click", function () {
            var Id = $(this).data("id");

            $.ajax({
                url: '/Admin/Vacancy/ModalDetails/' + Id,
                type: 'GET',
                dataType: "json",
                success: function (response) {

                    $(".tag_modal_link").attr("href", "/Admin/Vacancy/Update/" + Id)
                    $(".tag_modal_name").text(response.xdb.Name);
                    $(".tag_modal_id").text(response.xdb.Id);
                    $(".tag_modal_content").html(response.content);
                    $(".tag_modal_desc").html(response.xdb.Description);
                    $(".tag_modal_img").attr("src", "/Uploads/" + response.xdb.Image);
                    $(".tag_modal_post").text("Posted by " + response.xdb.Admin + " on " + GetFullDate(response.xdb.PostDate));
                    $(".tag_modal_deadline").text(GetFullDate(response.xdb.Deadline));
                    if (response.xdb.EditDate == null) {

                        $(".tag_modal_modify").text("N/A")

                    } else {
                        $(".tag_modal_modify").text("Modified by " + response.xdb.EditAdmin + " on " + GetFullDate(response.xdb.EditDate));
                    }


                    $("#details-" + button + "").modal("toggle");

                },

                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");

                }
            });


        })
    }

    toggleDetailsVacancy("vacancymodal");

    $("#code_generate_btn").click(function () {

        $.ajax({
            url: '/Admin/Invitation/CodeGenerate/',
            type: 'GET',
            dataType: "json",
            success: function (response) {

                $("#code_generate_field").val(response);

            },

            error: function (error) {
                console.log(error);
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");

            }
        });

    });

    function copyToClipboard(element) {
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(element).select();
        document.execCommand("copy");
        $temp.remove();
    }


    $("#copy_code_btn").click(function () {

        if ($("#code_generate_field").val()) {
            copyToClipboard($("#code_generate_field").val());
            console.log($("#code_generate_field").val());

            $.toast("Copied to clipboard");



            $.ajax({
                url: '/Admin/Invitation/PostCode?code=' + $("#code_generate_field").val(),
                type: 'POST',

                cache: false,
                dataType: "json",
                contentType: false,
                processData: false,
                success: function () {

                },
                error: function (error) {
                    console.log(error);

                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });
        } else {
            $.toast("Please generate a code first");
        }
    })


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
                minlength: 6
            }, Password_Confirm: {
                required: true,
                minlength: 6,
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
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");

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
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
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


    $("#code_gnrt_btn").on("click", function () {

        $("#code_gnrt_modal").modal("toggle");
    })

    $('input[name="Deadline"]').on("change", function () {

        console.log($(this).val());
    })


   

    $("#changepass_modal_btn").on("click", function () {

        $("#change_password_modal").modal("toggle", function () {
            $(".admin_dropdown").trigger("click");

        });
    });

    $(".admin_dropdown").click(function () {
        console.log(true);
    })

    $("#userPasswordForm").validate({
        rules: {

            Password: {
                required: true,
                minlength: 6,

            },
            Con_Password: {
                required: true,
                minlength: 6,
                equalTo: "#confirm_pass"
            },
            Cur_Password: {
                required: true,

            },

        },
        messages: {


            Password: {
                required: "Please input your new password.",

            },
            Con_Password: {
                required: "Please confirm your password.",
                equalTo: "Passwords do not match."
            },
            Cur_Password: {
                required: "Please input your current password.",

            }

        }
    });

    $("#update-pass-btn").on("click", function () {

        if ($("#userPasswordForm").valid()) {

            passvm = {
                oldpass: $('input[name="Cur_Password"]').val(),
                newpass: $('input[name="Password"]').val()
            }


            $.ajax({
                url: '/Admin/Admin/UpdateAdminPassword/',
                type: 'POST',
                data: JSON.stringify(passvm),
                async: true,
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    if (response === "true") {
                        console.log("updated");
                        $.toast("Succesfully Updated");

                        $("#change_password_modal").modal("hide");
                    } else if (response === "false") {
                        console.log("false");
                        $.toast("Password is not correct");
                    }

                    else {
                        $.toast("Your session has timed out. Redirecting...");
                        console.log("session_error");
                        window.location.href = "/Admin/Home/Login"
                    }
                },
                complete: function () {



                },
                error: function (error) {
                    console.log(error);
                    $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                }
            });
            return false;
        }



    });

    $(".reset_ps_btn").on("click", function () {

        $("#btn-reset-password").data("id", $(this).data("id"));

        $("#reset-confirmation-modal").modal("show");

    });

    $("#btn-reset-password").on("click", function () {
        var Id = $(this).data("id");
        $.ajax({
            url: '/Admin/AdminsAll/ResetPassword/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {



                if (response === "true") {
                    $.toast("Reset successful");


                    $("#reset-confirmation-modal").modal("hide");


                } else {

                    $("#reset-confirmation-modal").modal("hide");


                    $.toast("You do not have the privelege to reset password");
                }



            },
            error: function (error) {
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                console.log(error);

            }
        });
        return false;

    })


    $(".button_container").on("click", ".delete_usr_btn",function () {

        $("#btn-delete-admin").data("id", $(this).data("id"));

        $("#delete-confirmation-modal").modal("show");

    });


    $("#btn-delete-admin").on("click", function () {
        var Id = $(this).data("id");
        $.ajax({
            url: '/Admin/AdminsAll/DeleteAdmin/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {



                if (response === "true") {
                    $.toast("Block successful");


                    $("#delete-confirmation-modal").modal("hide");

                    $(".delete_usr_btn[data-id='"+Id+"']").removeClass("bg-theme-6");
                    $(".delete_usr_btn[data-id='" + Id + "']").addClass("bg-theme-9");
                    $(".delete_usr_btn[data-id='" + Id + "']").addClass("unblock_usr_btn");
                    $(".unblock_usr_btn[data-id='" + Id + "']").text("Unblock the user")
                    $(".unblock_usr_btn[data-id='" + Id + "']").removeClass("delete_usr_btn");
                   ;


                } else {

                    $("#delete-confirmation-modal").modal("hide");


                    $.toast("You do not have the privelege to block the user");
                }



            },
            error: function (error) {
                console.log(error);
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
            }
        });
        return false;

    });




    $(".button_container").on("click", ".unblock_usr_btn", function () {

        $("#btn-unblock").data("id", $(this).data("id"));

        $("#unblock-confirmation-modal").modal("show");

    });


    $("#btn-unblock").on("click", function () {
        var Id = $(this).data("id");
        $.ajax({
            url: '/Admin/AdminsAll/UnblockAdmin/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {



                if (response === "true") {
                    $.toast("Block was removed");


                    $("#unblock-confirmation-modal").modal("hide");

                    $(".unblock_usr_btn[data-id='" + Id + "']").removeClass("bg-theme-9");
                    $(".unblock_usr_btn[data-id='" + Id + "']").addClass("bg-theme-6");
                    $(".unblock_usr_btn[data-id='" + Id + "']").addClass("delete_usr_btn");
                    $(".delete_usr_btn[data-id='" + Id + "']").text("Block the user")
                    $(".delete_usr_btn[data-id='" + Id + "']").removeClass("unblock_usr_btn");
                    ;


                } else {

                    $("#delete-confirmation-modal").modal("hide");


                    $.toast("You do not have the privelege to unblock the user");
                }



            },
            error: function (error) {
                console.log(error);
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");

            }
        });
        return false;

    });



    $(".provide_privelege").on('change', function () {
        var Id = $(this).data('id');
        $.ajax({
            url: '/Admin/AdminsAll/ProvidePrivelege/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {

                if (response === "true") {
                    $.toast("Privelege has been updated for the user");


                } else {




                    $.toast("You do not have the right to provide privelege");
                }

            },
            error: function (error) {
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                console.log(error);

            }
        });
    });




    //*************************************OWIN**************************************    

    var notify = jQuery.connection.notifyHub;


    notify.client.modelCreate = function (adminname, datetime, modelname, controller) {

        console.log(adminname, datetime, modelname, controller);
        $.toast({
            text:'<p><strong>'+ datetime+ '</strong><p/><p>' + adminname + ' just created a '+controller+' "' + modelname+'" </p>',
            hideAfter: false,
            position: 'bottom-right'
        })

  
    };

    notify.client.modelUpdate = function (adminname, datetime, modelname, controller) {

        console.log(adminname, datetime, modelname, controller);

        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>' + adminname + ' just modified the ' + controller + ' "' + modelname + '" </p>',

            hideAfter: false,
            position: 'bottom-right'
        })

   
    };

    notify.client.modelDelete = function (adminname, datetime, modelname, controller) {
        console.log(adminname, datetime, modelname, controller);

        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>' + adminname + ' just deleted the ' + controller + ' "' + modelname + '" </p>',

            hideAfter: false,
            position: 'bottom-right'
        })

      
    };

    notify.client.appCancel = function (adminname, datetime, id) {

        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>' + adminname + ' just confirmed the Appointment # ' + id + ' </p><p>Click <a href="/Admin/Appointment/Index">here</a> to view the changes.</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };
    notify.client.appConfirm = function (adminname, datetime, id) {

        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>' + adminname + ' just confirmed the Appointment # ' + id + ' </p><p>Click <a href="/Admin/Appointment/Index">here</a> to view the changes.</p>',


            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.appFinish = function (adminname, datetime, id) {

        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>' + adminname + ' just finished the Appointment # ' + id + ' </p><p>Click <a href="/Admin/Appointment/Index">here</a> to view the changes.</p>',


            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.orderCancel = function (adminname, datetime, id) {

        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>' + adminname + ' just cancelled the Order # ' + id + ' </p><p>Click <a href="/Admin/Sale/Details/'+id+'">here</a> to view the changes.</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.orderConfirm = function (adminname, datetime, id) {

        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>' + adminname + ' just confirmed the Order # ' + id + ' </p><p>Click <a href="/Admin/Sale/Details/' + id + '">here</a> to view the changes.</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.orderFinish = function (adminname, datetime, id) {

        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>' + adminname + ' created a shipment for the Order # ' + id + ' </p><p>Click <a href="/Admin/Sale/Details/' + id + '">here</a> to view the changes.</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.reservationConfirm = function (adminname, datetime, id) {

        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>' + adminname + ' just confirmed the Reservation # ' + id + ' </p><p>Click <a href="/Admin/Reservation/Details/' + id + '">here</a> to view the changes.</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.reservationCancel = function (adminname, datetime, id) {

        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>' + adminname + ' just cancelled Reservation # ' + id + ' </p><p>Click <a href="/Admin/Reservation/Details/' + id + '">here</a> to view the changes.</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.newsCancel = function (adminname, datetime, email) {

        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>' + adminname + ' just removed subscriber "' + email + '" from Newsletter</p><p>Click <a href="/Admin/NewsLetter/Index/">here</a> to view the changes.</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.postReview = function (usrname, datetime, modelname, type) {
      

        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>User "' + usrname + '" just posted a review for ' + type + ' "' + modelname + '" </p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.postTestimonial = function (usrname, datetime) {
   

        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>User "' + usrname + '" just posted a testimonial. </p><p>Click <a href="/Admin/Testimonial/Index">here</a> to view the index.</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.postComment = function (usrname, datetime, blog, id) {


        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>User "' + usrname + '" just posted a comment on Blog "' + blog + '" </p><p>Click <a href="/BlogG/Details/'+id+'">here</a> to view the blog.</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };


    notify.client.newMessage = function (usrname, datetime, subject) {


        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>You have a new message from user "' + usrname + '", with a subject of "' + subject + '" </p><p>Click <a href="/Admin/Message/Index">here</a> to view the messages.</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.newSubscription = function (datetime, email) {


        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>User with an email "' + email + '" just subcribed to Newsletter </p><p>Click <a href="/Admin/Newsletter/Index">here</a> to view</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.newRes = function (datetime, id) {


        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>New Reservation #GA' + id+ ' has just been booked. </p><p>Click <a href="/Admin/Reservation/Details/'+id+'">here</a> to view</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.newOrder = function (datetime, id) {


        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>New Order #GA' + id + ' has just been placed. </p><p>Click <a href="/Admin/Sale/Details/' + id + '">here</a> to view</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };



    notify.client.newApp = function (datetime, id) {


        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>New Appointment #GA' + id + ' has just been placed. </p><p>Click <a href="/Admin/Appointment/Index">here</a> to view</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.newUser = function (datetime, name,  id) {


        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><p>New user "' + name + '" has just registered. </p><p>Click <a href="/Admin/User/Details/'+id+'">here</a> to view</p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.checkOnline = function (datetime, name, profilepicture) {


        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><div class="mt-2"><img style="max-height:50px" src="/Uploads/' + profilepicture+'"></div><p class="mt-3">Admin "' + name + '" is now online. </p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    notify.client.checkReg = function (datetime, name, profilepicture) {


        $.toast({
            text: '<p><strong>' + datetime + '</strong><p/><div class="mt-2"><img style="max-height:50px" src="/Uploads/' + profilepicture + '"></div><p class="mt-3">Admin "' + name + '" has just registered. </p>',

            hideAfter: false,
            position: 'bottom-right'
        })


    };

    jQuery.connection.hub.start().done(function () {
      
    });



    //*************************************OWIN**************************************


    $(".rmv-usr").on("click", function () {
        $("#delete-confirmation-modal-user").modal("toggle");
    });

    $("#btn-remove-user").on("click", function () {
        $("#delete-confirmation-modal-user").modal("hide");
        $("#delete-confirmation-double-modal-user").modal("toggle");
    });

    $("#btn-remove-user-double").on("click", function () {
        var Id = $(this).data("id");
        $.ajax({
            url: '/Admin/User/Delete/' + Id,
            type: 'POST',
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {

                if (response === "true") {
                    $.toast("Delete successful");

                    $("#delete-confirmation-double-modal-user").modal("hide");

                    window.location.href = location.origin + "/Admin/User/Index";

                } else {

                    $("#delete-confirmation-modal").modal("hide");

                    $.toast("Your session has timed out.");
                }



            },
            error: function (error) {
                $.toast("Error occured while updating. Error Code:</br> (" + error.status + ": " + error.statusText + ").");
                console.log(error);

            }
        });
        return false;
    })

    $(".excel_export").on("click", function () {

        setTimeout(function () {

            $(".preloader").fadeOut(500);

            $(".dropdown-box").removeClass("show");
        },1000)
        
    })

    $(".print_page").on("click", function () {

        setTimeout(function () {
            $(".dropdown-box").removeClass("show");

            window.print();



        },500)
      


        
    })

});
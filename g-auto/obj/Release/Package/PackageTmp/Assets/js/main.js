

(function ($) {
	"use strict";

	jQuery(document).ready(function ($) {


		/* 
		=================================================================
		01 - Main Slider JS
		=================================================================	
		*/

		$(".gauto-slide").owlCarousel({
			animateOut: 'fadeOutLeft',
			animateIn: 'fadeIn',
			items: 2,
			nav: true,
			dots: false,
			autoplayTimeout: 9000,
			autoplaySpeed: 5000,
			autoplay: true,
			loop: true,
			navText: ["<img src='/Assets/img/prev-1.png'>", "<img src='/Assets/img/next-1.png'>"],
			mouseDrag: true,
			touchDrag: true,
			responsive: {
				0: {
					items: 1
				},
				480: {
					items: 1
				},
				600: {
					items: 1
				},
				750: {
					items: 1
				},
				1000: {
					items: 1
				},
				1200: {
					items: 1
				}
			}
		});

		$(".gauto-slide").on("translate.owl.carousel", function () {
			$(".main-slide h2").removeClass("animated fadeInUp").css("opacity", "0");
			$(".main-slide p").removeClass("animated fadeInDown").css("opacity", "0");
			$(".main-slide .gauto-btn").removeClass("animated fadeInDown").css("opacity", "0");
		});
		$(".gauto-slide").on("translated.owl.carousel", function () {
			$(".main-slide h2").addClass("animated fadeInUp").css("opacity", "1");
			$(".main-slide p").addClass("animated fadeInDown").css("opacity", "1");
			$(".main-slide .gauto-btn").addClass("animated fadeInDown").css("opacity", "1");
		});

		/* 
		=================================================================
		02 - Select JS
		=================================================================	
		*/

		$('select').niceSelect();

		/* 
		=================================================================
		03 - Clockpicker JS
		=================================================================	
		*/

		$('.clockpicker').clockpicker();

	
		/* 
		=================================================================
		04 - Service Slider JS
		=================================================================	
		*/

			$(".service-slider").owlCarousel({
				autoplay: true,
				//loop: true,
				margin: 25,
				touchDrag: true,
				mouseDrag: true,
				items: 1,
				nav: false,
				dots: true,
				autoplayTimeout: 6000,
				autoplaySpeed: 1200,
				navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
				responsive: {
					0: {
						items: 1
					},
					480: {
						items: 1
					},
					768: {
						items: 2
					},

					1000: {
						items: 3
					},
					1200: {
						items: 3
					}
				}
    })


		/* 
		=================================================================
		05 - Testimonial Slider JS
		=================================================================	
		*/

		$(".testimonial-slider").owlCarousel({
			autoplay: true,
			loop: true,
			margin: 25,
			touchDrag: true,
			mouseDrag: true,
			items: 1,
			nav: false,
			dots: true,
			autoplayTimeout: 6000,
			autoplaySpeed: 1200,
			navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
			responsive: {
				0: {
					items: 1
				},
				480: {
					items: 1
				},
				600: {
					items: 1
				},
				1000: {
					items: 3
				},
				1200: {
					items: 3
				}
			}
		});


		/* 
		=================================================================
		06 - Responsive Menu JS
		=================================================================	
		*/
		$("ul#gauto_navigation").slicknav({
			prependTo: ".gauto-responsive-menu"
		});


		/* 
		=================================================================
		07 - Back To Top
		=================================================================	
		*/
		if ($("body").length) {
			var btnUp = $('<div/>', {
				'class': 'btntoTop'
			});
			btnUp.appendTo('body');
			$(document).on('click', '.btntoTop', function () {
				$('html, body').animate({
					scrollTop: 0
				}, 700);
			});
			$(window).on('scroll', function () {
				if ($(this).scrollTop() > 200) $('.btntoTop').addClass('active');
				else $('.btntoTop').removeClass('active');
			});
		}

		$('#recommended').owlCarousel({
			autoplay: true,
			loop: true,
			margin: 25,
			touchDrag: true,
			mouseDrag: true,
			stopOnHover: true,
			items: 1,
			nav: false,
			dots: true,
			autoplayTimeout: 6000,
			autoplaySpeed: 1200,
			navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
			responsive: {
				0: {
					items: 1
				},
				480: {
					items: 2
				},
				600: {
					items: 2
				},
				770: {
					items: 3
				},
				1000: {
					items: 4
				},
				1200: {
					items: 4
				}
			}
		})



		
	});


}(jQuery));

//*******************Navbar-Scroll-Fixed-Start*************************
window.onscroll = function () { myFunction() };

var navbar = document.getElementById("gauto-navbar");

var sticky = navbar.offsetTop;

function myFunction() {
	if (window.pageYOffset >= sticky) {
		navbar.classList.add("sticky")
	} else {
		navbar.classList.remove("sticky");
	}
}
feather.replace({
	width: '20px', height: '20px'
})

//*******************Navbar-Scroll-Fixed-End*************************

$(document).ready(function () {




	$(function () {
		$("body").niceScroll({
			smoothscroll: true, cursorcolor: "#22abc3",
			scrollspeed: 100,
			cursorwidth: "15px",
			cursorborderradius: "0px",
			zindex: "9999",
			touchbehavior: true,
			preventmultitouchscrolling: false,
			emulatetouch: true,
			cursordragontouch:true
		});
	});


	function resizeNSC() {
		$(document).on('mouseover', 'body', function () {
			$(this).getNiceScroll().resize();
		});
    }

	

	AOS.init({
		once: true,
		duration: 1000,
		mirror: true,
	});


	var $preloader = $('#page-preloader'),
		$spinner = $preloader.find('.spinner-loader');
	$spinner.fadeOut();
	$preloader.delay(50).fadeOut('slow');
	setTimeout(function () {

		$("#page-preloader").fadeOut();
	}, 400)

	$(window).bind('beforeunload', function () {
		$("#page-preloader").fadeIn();


	});

	var urlSplit = window.location.href.split('/');


	$('input[name="Phone"]').on('keypress', function (evt) {
		evt = (evt) ? evt : window.event;
		var charCode = (evt.which) ? evt.which : evt.keyCode;
		if (charCode > 31 && (charCode < 48 || charCode > 57)) {
			return false;
		}
		return true;
	})

	//$('input[name="FullName"]').keypress(function (e) {
	//	var regex = new RegExp("^[a-zA-Z]+$");
	//	var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
	//	if (regex.test(str)) {
	//		return true;
	//	}
	//	else {
	//		e.preventDefault();
	//		return false;
	//	}
	//});

	
	//**********************************MASKING************************************************

	$('input[name="Phone"]').mask('(000) 000-0000');

	//**********************************MASKING************************************************
	
	$("#service_app_scroll").on("click", function () {

		$('html, body').animate({
			scrollTop: $("#accordion").offset().top
		}, 1000);
	});


	function CharsStart() {

		$('.js-chart').easyPieChart({
			barColor: false,
			trackColor: false,
			scaleColor: false,
			scaleLength: false,
			lineCap: false,
			lineWidth: false,
			size: false,
			animate: 2000,

			onStep: function (from, to, percent) {
				$(this.el).find('.js-percent').text(Math.round(percent));
			}
		});
	}

    var scrollIsReached = true;

    if (urlSplit.indexOf("Main") >= 0 && scrollIsReached) {

		$(window).scroll(function (e) {
			if ($(this).scrollTop() > 5500) {
				scrollIsReached = false;
				console.log(true);
				$('.js-chart').each(function () {
					CharsStart();
				});
			}


		});



		

	}

	if (urlSplit.indexOf("AboutG") >= 0 && scrollIsReached) {

		$(window).scroll(function (e) {
			if ($(this).scrollTop() > 1300) {
				scrollIsReached = false;

				$('.js-chart').each(function () {
					CharsStart();
				});
			}


		});

	}

	if ($('.js-slider-for').length) {
		$('.js-slider-for').slick({
			arrows: false,
			fade: true,
			dots:false,
			//asNavFor: '.js-slider-nav'
		});
		$('.js-slider-nav').slick({
			slidesToShow: 5,
			slidesToScroll: 1,
			dots: false,

			//asNavFor: '.js-slider-for',
			focusOnSelect: true
		});
	}

	if ($('.js-slider').length) {
		$('.js-slider').slick({
			dots: false,
		});
	}

	$(".nav-link").click(function () {
		resizeNSC();

    })


	function refreshCart(Id, Count) {
		var cartElem = {
			id: Id,
			count: Count
		};

		$.ajax({
			url: "/ProductG/AddToCart/",
			type: "get",
			dataType: "json",
			data: cartElem,
			success: function (response) {

				if (response === "success-true") {
					var oldCountTrue = parseInt($(".cartCount").text());
					oldCountTrue++;
					$(".cartCount").text(oldCountTrue);
				}
			},
			error: function (error) {
				console.log(error);
			}
		});
	}



	function dropDownCart() {
			var subPrice = 0;

			$.ajax({
				url: '/ProductG/ProductData/',
				type: 'GET',
				dataType: "json",
				async: false,

				success: function (response) {

					var maindiv = $(".pl_container");

					maindiv.empty();

					for (var i = 0; i < response.length; i++) {

						var div = $('<li><div class="cart-btn-product"><a class="product-remove" data-id="'+response[i].Id+'" href="javascript:;"><i class="fa fa-times"></i></a><div class="cart-btn-pro-img"><a href="/ProductG/Details/' + response[i].Id + '"><img src="/Uploads/' + response[i].Image + '" alt="product"></a></div><div class="cart-btn-pro-cont"><h4><a href="/ProductG/Details/' + response[i].Id + '">' + response[i].Name + '</a></h4><div class="shop-dd-info"><p>Quantity: ' + response[i].Count + '</p><span class="price">$' + response[i].Price + '</span></div></div></div></li>');

						subPrice += parseFloat(response[i].Price) * parseFloat(response[i].Count)

						maindiv.append(div);




					}
					$("#subTotalPrice").text(parseFloat(subPrice.toFixed(2)));

					//if (response.length == 0) {





					//}

				},
				complete: function (data) {

				},
				error: function (error) {
					console.log("error custom");

				}
			});
	
	}

	

	$(".dropdown_cart").on("click", function () {
		console.log(true);
		dropDownCart();
		$(".cart-dropdown").fadeToggle();
	})

	


	$(".addToCart").on('click', function (e) {

		Id = parseInt($(this).data("id"));

		Count = $(".productCount").val();

		refreshCart(Id, Count);

		
		setTimeout(function () {
			$(".dropdown_cart").trigger("click");

		}, 200);

	

		
	});

	$(".pl_container").on("click", ".product-remove", function () {

		Id = $(this).data("id");
	
		$(".cartCount").text(parseInt($(".cartCount").text()-1));

		$(this).parent().parent().fadeOut(function () {

			$.ajax({
				url: "/ProductG/RemoveFromCart/" + Id,
				type: "get",
				dataType: "json",
				success: function (response) {
					console.log(response);
					dropDownCart();
				},
				error: function (error) {
					console.log(error);
				}
			});


		})

		
		

		
	});


	$(".cartCountIndex").change(function () {
		var Id = parseInt($(this).data("id"));
		var Count = parseFloat($(this).val());
		var price = parseFloat($(this).data("price"));

		refreshCart(Id, Count);

		//Own total price
		$(".TotalPrice" + Id + "").text(Count * price + ",00");

		//All total price
		var inputs = $(".cartCountIndex");
		var AllTotalPrice = 0;
		for (var i = 0; i < inputs.length; i++) {
			AllTotalPrice += (parseFloat(inputs[i].value) * parseFloat(inputs[i].dataset.price));
		}

		$(".AllTotalPrice").text(AllTotalPrice + ",00");
	});

	$(".cart-table-left").on("click", ".btn-remove", function (e) {
		e.preventDefault();

		var Id = parseInt($(this).data("id"));
		var ThisTotalPrice = parseFloat($(".TotalPrice" + Id + "").text());

		$(this).parent().parent().fadeOut(function () {
			$(this).remove();
		});
		//Decrease count
		var oldCountFalse = parseInt($(".cartCount").text());
		oldCountFalse--;
		$(".cartCount").text(oldCountFalse);

		//Decrease Total price
		var TotalPrice = parseFloat($(".AllTotalPrice").text());
		TotalPrice = TotalPrice - ThisTotalPrice;
		$(".AllTotalPrice").text(parseFloat(TotalPrice).toFixed(2));


		if ($(".shop_item_container").find(".shop-cart-item").length<2) {
			console.log(true);
			$("#clearShoppingCart").remove();
			$(".checkout-action").fadeOut(function () {
				$(this).remove();
			})

			setTimeout(function () {
				$(".table-responsive").append($('<h3 class="text-center d-block">Wow, Such Empty...</h3 >').hide().fadeIn());
			},500)

		} else {
			console.log(false);
			
        }
		//Remove from cookie
		$.ajax({
			url: "/ProductG/RemoveFromCart/" + Id,
			type: "get",
			dataType: "json",
			success: function (response) {
				console.log(response);
			},
			error: function (error) {
				console.log(error);
			}
		});
	});




	$("#clearShoppingCart").on('click', function () {

		$(".AllTotalPrice").text("0.00");

		$(".shop-cart-item").fadeOut(function () {
			$(this).remove();

		});

		$(".checkout-action").fadeOut(function () {
			$(this).remove();

		})

		$("#clearShoppingCart").fadeOut(function () {
			$(this).remove();
		})

		setTimeout(function () {
			$(".table-responsive").append($('<h3 class="text-center d-block">Wow, Such Empty...</h3 >').hide().fadeIn());
		}, 500)


		$.ajax({
			url: "/ProductG/RemoveFromCartAll/",
			type: "get",
			dataType: "json",
			success: function (response) {
				$(".cartCount").text("0");
			},
			error: function (error) {
				console.log(error);
			}
		});

	});



	//***********************************SLICK_SLIDER****************************************
	$('.slider').slick({
		slidesToShow: 1,
		slidesToScroll: 1,
		arrows: false,
		fade: false,
		asNavFor: '.slider-nav-thumbnails',
	});

	$('.slider-nav-thumbnails').slick({
		slidesToShow: 5,
		slidesToScroll: 5,
		asNavFor: '.slider',
		dots: false,
		arrows: false,
		focusOnSelect: true,
		adaptiveHeight: true,
		infinite: true
	});

	//***********************************SLICK_SLIDER****************************************



	//***********************************CHECKBOX-VALDIATION****************************************


	//***********************************CHECKBOX-VALDIATION****************************************

	//***********************************PICKADATE****************************************


	//$("input").on("change", function () {
	//	console.log($(this).val());
	//})

	var datesToDisable = [];

	//datesToDisable.push([2020, 7, 17]);



	function calcDiff() {

		var Date1 = new Date($("#input_to").val());
		var Date2 = new Date($("#input_from").val())
		var diff = Date1.getTime() - Date2.getTime();

		var result = parseFloat(diff / (1000 * 60 * 60 * 24));

		return result;
	}

	function calcTotal() {
		var AllTotalPrice = parseFloat($("#model-price-res").val() * calcDiff());

		$(".checkvalidate").each(function () {
			if ($(this).val() == "yes") {


				AllTotalPrice += parseFloat($(this).parent().find(".serviceBox").val());

			}
		})

		if (isNaN(AllTotalPrice)) {
			$("#priceBox").text(parseFloat($("#model-price-res").val()));
		} else {
			$("#priceBox").text(AllTotalPrice);
		}
	}

	if (urlSplit.indexOf("Reservation") >= 0) {

		$(window).scroll(function (e) {
			if ($(window).width() > 992) {
				if ($(this).scrollTop() > 1800) {
					$('.b-booking-nav').fadeOut(function () {
					});
				}
				else {
					$('.b-booking-nav').fadeIn();


				}
			};
		});

		$("#service-btn").click(function () {

			$('html, body').animate({
				scrollTop: $("#service-section").offset().top
			}, 1000);
		});
		$("#car-style-btn").click(function () {
			$('html, body').animate({
				scrollTop: $("#car-style-section").offset().top
			}, 1000);

        })
		$("#time-btn").click(function () {
			$('html, body').animate({
				scrollTop: $("#time-section").offset().top
			}, 1000);

        })
		$("#summary-btn").click(function () {
			$('html, body').animate({
				scrollTop: $("#summary-section").offset().top
			}, 1000);

		})

		calcDiff();
		console.log(calcDiff());

		var id = $("#dtpModelId").data('id')


		$.ajax({
			url: "/ModelG/DatePicker/" + id,
			type: "get",
			dataType: "json",
			data: "",
			async: false,
			success: function (response) {
				console.log("success")


				for (var i = 0; i < response.length; i++) {
					datesToDisable.push([parseInt(response[i].Year), parseInt(response[i].Month - 1), parseInt(response[i].Day)]);
				}
			},
			error: function (error) {
				console.log(error);
			}
		});

		$('#input_from').pickadate({
			format: 'ddd, d mmm',
			formatSubmit: 'd mmm',
			min: Date.now,
			max: new Date(2021, 7, 14),
			//disable: datesToDisable


		})
		$('#input_to').pickadate({
			format: 'ddd, d mmm',
			formatSubmit: 'd mmm',
			min: Date.now,
			max: new Date(2021, 7, 14)
		})

		var from_$input = $('#input_from').pickadate({
			container: '#root-picker-outlet',
			onOpen: function () {
				$('#root-picker-outlet').css("display", "block");
			},
			onClose: function () {
				$('#root-picker-outlet').css("display", "none");
			},
		}),
			from_picker = from_$input.pickadate('picker')

		from_picker.set('disable', datesToDisable)

		var to_$input = $('#input_to').pickadate({
			container: '#root-picker-outlet',
			onOpen: function () {
				$('#root-picker-outlet').css("display", "block");
			},
			onClose: function () {
				$('#root-picker-outlet').css("display", "none");
			}
		}),
			to_picker = to_$input.pickadate('picker')
		to_picker.set('disable', datesToDisable)
		// Check if there’s a “from” or “to” date to start with.
		if (from_picker.get('value')) {
			to_picker.set('min', from_picker.get('select'))
		}
		if (to_picker.get('value')) {
			from_picker.set('max', to_picker.get('select'))
		}

		// When something is selected, update the “from” and “to” limits.
		from_picker.on('set', function (event) {
			if (event.select) {
				to_picker.set('min', from_picker.get('select'))
			} else if ('clear' in event) {
				to_picker.set('min', false)
			}
		})
		to_picker.on('set', function (event) {
			if (event.select) {
				from_picker.set('max', to_picker.get('select'))
			} else if ('clear' in event) {
				from_picker.set('max', false)
			}
		})

		$("#input_from").on('change', function () {
			$("#startDateBox").text($(this).val());
			calcTotal();


		});
		$("#input_to").on('change', function () {
			$("#endDateBox").text($(this).val());
			calcTotal();



		});
		$("#input_time").on('change', function () {
			$("#startTimeBox").text($(this).val());
			calcTotal();

		})

		$(".picker__button--today").html("Reserved");

	}


	if (urlSplit.indexOf("ProductG") >= 0) {
		$.ajax({
			url: "/ProductG/UpdateProductStatus/",
			type: "get",
			dataType: "json",
			data: "",
			async: false,
			success: function (response) {

			},
			error: function (error) {
				console.log(error);
			}
		});


	}
	$(document).on("change", ".checkbox-validate", function () {
		if ($(this).parent().find(".checkvalidate").val() == "yes") {
			$(this).parent().find(".checkvalidate").val("no")

		}
		else {
			$(this).parent().find(".checkvalidate").val("yes")
		}
	});

	$(document).on("change", ".checkbox-validate", function () {
		calcTotal();
	});


	$(".b-booking-nav__item").on('click', function () {
		$(".b-booking-nav__item").each(function () {
			$(this).removeClass("active");
		});

		$(this).addClass("active");
	});

	//***********************************PICKADATE****************************************




	//***********************************VALIDATE****************************************


	jQuery.validator.addMethod("lettersonly", function (value, element) {
		return this.optional(element) || /^[a-z\s]+$/i.test(value);
	}, "Please use only letters."); 

	$("#bookingForm").validate({
		rules: {
			FullName: {
				lettersonly: true,
				required: true,
				maxlength: 200
			}, Phone: {
				minlength: 14,
				maxlength: 14
			}
		},
		messages: {
			FullName: {
				lettersonly: "Please enter characters only.",
				required: "Please enter your full name.",
			}, Phone: {
				minlength: "Please enter a valid phone number.",
				maxlength: "Please enter a valid phone number."
			}
		}


	});
	$("#checkoutForm").validate({
		rules: {
			FullName: {
				lettersonly: true,
				required: true,
				maxlength: 200
			}, Phone: {
				minlength: 14,
				maxlength: 14
			}
		},
		messages: {
			FullName: {
				lettersonly: "Please enter characters only.",
				required: "Please enter your full name.",
			}, Phone: {
				minlength: "Please enter a valid phone number.",
				maxlength: "Please enter a valid phone number."
			}
		}


	});
	$("#userUpdateForm").validate({
		rules: {
			FullName: {
				lettersonly: true,
				required: true,
				maxlength: 200
			}, Phone: {
				minlength: 14,
				maxlength:14
			}
		},
		messages: {
			FullName: {
				lettersonly: "Please enter characters only.",
				required: "Please enter your full name.",
			}, Phone: {
				minlength:"Please enter a valid phone number.",
				maxlength:"Please enter a valid phone number."
            }
		}
	

	});
	$("#orderReviewPost").validate({
		rules: {
			Rating: {
				required: true
            }
        }

	});
	$("#bookingReviewPost").validate({
		rules: {
			Rating: {
				required: true
			}
		}

	});
	$("#appService").validate({
		rules: {
			FullName: {
				lettersonly: true,
				required: true,
				maxlength: 200
			}, Phone: {
				minlength: 14,
				maxlength: 14
			}
		},
		messages: {
			FullName: {
				lettersonly: "Please enter characters only.",
				required: "Please enter your full name.",
			}, Phone: {
				minlength: "Please enter a valid phone number.",
				maxlength: "Please enter a valid phone number."
			}
		}


	});

	$("#contactSupport").validate({
		rules: {
			Name: {
				lettersonly: true,
				required: true,
				maxlength: 200
			}, Phone: {
				minlength: 14,
				maxlength: 14
			}
		},
		messages: {
			Name: {
				lettersonly: "Please enter characters only.",
				required: "Please enter your full name.",
			}, Phone: {
				minlength: "Please enter a valid phone number.",
				maxlength: "Please enter a valid phone number."
			}
		}


	});
	$("#subscribeNews").validate();

	$("#user_reg_form").validate({

		rules: {
			Email: {
				required: true,

			},
			FullName: {
				required: true,
				lettersonly: true,
				minlength:6,
				maxlength: 200
			},
			Password: {
				required: true,
				minlength: 6,
				
			},
			confirm_password: {
				required: true,
				minlength: 6,
				equalTo:"#confirm_pass"
			},
			Phone: {
				required: true,
				minlength: 14,
				maxlength:14
            }
		},
		messages: {
			Email: {
				required: "Email field can not be empty.",

			},
			FullName: {
				required: "Please input your full name.",
				lettersonly: "Please enter characters only.",
			},
			confirm_password: {
				required:"Please confirm your password.",
				equalTo: "Passwords do not match."
			},
			Password: {
				required: "Please add your password.",
				
			},
			Phone: {
				required: "Please add your phone number.",
				minlength: "Not a valid phone number.",
				maxlength: "Not a valid phone number."
            }
        }
	});

	$("#user_login_form").validate({
		rules: {
			Email: {
				required: true,

			},
			
			Password: {
				required: true,
				minlength: 6,

			},
			
		},
		messages: {
			Email: {
				required: "Please input your email.",

			},
			
			Password: {
				required: "Please input your password.",
				minlength:"Not a valid password."
			},
			
		}

	});

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

	$("#user_updatepswrd_form").validate({
		rules: {

			Password: {
				required: true,
				minlength: 6,

			},
			confirm_password: {
				required: true,
				minlength: 6,
				equalTo: "#confirm_pass"
			},
			

		},
		messages: {


			Password: {
				required: "Please input your new password.",

			},
			confirm_password: {
				required: "Please confirm your password.",
				equalTo: "Passwords do not match."
			},
			

		}
	});

	$("#addTestimonial").validate({
		rules: {
			Content: {
				maxlength: 250,
				minlength: 100,
				required:true

			}
		},
		messages: {
			Content: {
				maxlength: "Maximum allowed content is 250 characters.",
				minlength: "Minimum allowed content is 100 characters.",
				required: "Testimonial can not be empty.",

			}


		}

	});
	//***********************************VALIDATE****************************************





	//$("#calendar").fullCalendar();

	$("#userUpdateButton").on("click", function () {

		if ($("#userUpdateForm").valid()) {

			var formData = new FormData($("#userUpdateForm")[0]);
			$.ajax({
				url: '/UserG/UpdateUser',
				type: 'POST',
				data: formData,
				cache: false,
				contentType: false,
				processData: false,
				success: function (response) {
					if (response === "true") {
						console.log("updated");
						$("#notifyUserProfileUpdate").fadeIn(function () {

							setTimeout(function () {
								$("#notifyUserProfileUpdate").fadeOut();

							}, 3000)
						});
					} else {
						$("#notifyUserProfileError").fadeIn(function () {

							setTimeout(function () {
								$("#notifyUserProfileError").fadeOut();
								location.href = "/Main/Login"
							}, 3000)
						});
					}
				},
				complete: function () {


					resizeNSC();
				},
				error: function (error) {
					console.log("error while updating");

				}
			});
			return false;
		}
		


	});

	$("#update-pass-btn").on("click", function () {

		if ($("#userPasswordForm").valid()) {

			passvm = {
				oldpass:$('input[name="Cur_Password"]').val(),
				newpass:$('input[name="Password"]').val()
            }


			$.ajax({
				url: '/UserG/UpdateUserPassword/',
				type: 'POST',
				data: JSON.stringify(passvm),
				async: true,
				dataType:"json",
				contentType: "application/json",
				success: function (response) {
					if (response === "true") {
						console.log("updated");
						$("#notifyPasswordUpdate").fadeIn(function () {

							setTimeout(function () {
								$("#notifyPasswordUpdate").fadeOut();

							}, 3000)
						});
					}else if (response === "false") {
						console.log("false");
						$("#notifyPasswordError").fadeIn();
					}

					else {
						$("#notifyPasswordError").fadeIn(function () {

							setTimeout(function () {
								$("#notifyPasswordError").fadeOut();
								location.href = "/Main/Login"
							}, 3000)
						});
					}
				},
				complete: function () {


					resizeNSC();
				},
				error: function (error) {
					console.log(error);

				}
			});
			return false;
		}



	});

	$("#addTestButton").on("click", function () {

		if ($("#addTestimonial").valid()) {


			var formData = new FormData($("#addTestimonial")[0]);

			$.ajax({
				url: '/UserG/PostTestimonial/',
				type:"POST",
				data: formData,
				cache: false,
				contentType: false,
				processData: false,
				success: function (response) {
					if (response === "true") {
						console.log("updated");
						$("#addTestimonial").fadeOut(function () {

							$("#notifyTestSuccess").fadeIn();

							
						});
					} else if (response === "false") {
						console.log("false");
						$("#notifyTestError").fadeIn();
					}

					else {
						$("#notifyTestSessionError").fadeIn(function () {

							setTimeout(function () {
								$("#notifyTestSessionError").fadeOut();
								location.href = "/Main/Login"
							}, 3000)
						});
					}
				},
				complete: function () {


					resizeNSC();
				},
				error: function (error) {
					console.log(error);

				}
			});
			return false;
		}



	});

	$("#postProductReview").on("click", function (e) {

		console.log("works");
		if ($("#orderReviewPost").valid()) {

			if ($('input[name="Rating"]:checked').length != 0) {


				console.log($('input[name="Rating"]:checked').length);

				var formData = new FormData($("#orderReviewPost")[0]);

				$.ajax({
					url: '/UserG/PostProductReview',
					type: 'POST',
					data: formData,
					cache: false,
					contentType: false,
					processData: false,
					success: function (response) {
						if (response === "true") {
							console.log("updated");
							$("#orderReviewPost").fadeOut(function () {
								$("#notifyProfileUpdate").fadeIn();

							});
						} else {
							$("#orderReviewPost").fadeOut(function () {
								$("#notifyProfileError").fadeIn();

							});
						}

					},

					complete: function () {


						resizeNSC();
					},
					error: function (error) {
						console.log("error while updating");

					}
				});
				return false;
			} else {
				e.preventDefault();

				$("#rating_error").fadeIn();
            }

        }


	});

	$('input[name="Rating"]').on("change", function () {
		$("#rating_error").fadeOut();

	})

	$("#postBookingReview").on("click", function (e) {

		if ($("#bookingReviewPost").valid()) {

			if ($('input[name="Rating"]:checked').length != 0) {
				console.log("works");
				var formData = new FormData($("#bookingReviewPost")[0]);
				$.ajax({
					url: '/UserG/PostModelReview',
					type: 'POST',
					data: formData,
					cache: false,
					contentType: false,
					processData: false,
					success: function (response) {
						if (response === "true") {
							console.log("updated");
							$("#bookingReviewPost").fadeOut(function () {
								$("#notifyProfileUpdate").fadeIn();

							});
						} else {
							$("#bookingReviewPost").fadeOut(function () {
								$("#notifyProfileError").fadeIn();

							});
						}

					},
					complete: function () {


						resizeNSC();
					},
					error: function (error) {
						console.log("error while updating");

					}
				});
				return false;
			} else {
				e.preventDefault();

				$("#rating_error").fadeIn();
            }
		}

		


	});

	function GetMonthName(monthNumber) {
		var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
		return months[monthNumber - 1];
	}


	$("#postCommentButton").on("click", function () {

		console.log("works");
		var formData = new FormData($("#postComment")[0]);
		$.ajax({
			url: '/BlogG/PostComment',
			type: 'POST',
			data: formData,
			cache: false,
			contentType: false,
			processData: false,
			//async: false,

			success: function (response) {
				if (response != "nousersession") {

					$("#postComment").fadeOut(function () {
						$("#notifyProfileUpdate").fadeIn();
					})

					if (response.ProfPic != "Default") {
						var div = $('<li><article class="comment"><div class="profimage-container"><div class="profimage" style ="background:url(/Uploads/' + response.ProfPic + ')"></div></div><div class="comment-inner"><header class="comment-header row"><div class="col"><cite class="comment-author">' + response.Name + '</cite><time class="comment-datetime" datetime="2012-10-27">' + GetMonthName(response.PostMonth) + ' ' + response.PostDay + ', ' + response.PostYear + ' at ' + response.PostHour + ':' + response.PostMinute + '</time></div><div class="col-auto"><a class="comment-btn reply-toggle" data-commentid="' + response.dataCommentId + '" data-blogid="' + response.dataBlogId + '" data-userid="' + response.dataUserId + '"  href="javacript:;">reply</a></div></header><div class="comment-body"><p>' + response.CommentContent + '</p></div></div></article><ul class="children list-unstyled"></ul></li >');


					} else {
						var div = $('<li><article class="comment"><div class="profimage-container"><div class="profimage" style ="background:url(/Assets/img/user-avatar-default.png)"></div></div><div class="comment-inner"><header class="comment-header row"><div class="col"><cite class="comment-author">' + response.Name + '</cite><time class="comment-datetime" datetime="2012-10-27">' + GetMonthName(response.PostMonth) + ' ' + response.PostDay + ', ' + response.PostYear + ' at ' + response.PostHour + ':' + response.PostMinute + '</time></div><div class="col-auto"><a class="comment-btn reply-toggle" data-commentid="' + response.dataCommentId + '" data-blogid="' + response.dataBlogId + '" data-userid="' + response.dataUserId + '"  href="javacript:;">reply</a></div></header><div class="comment-body"><p>' + response.CommentContent + '</p></div></div></article><ul class="children list-unstyled"></ul></li >');

					}
					var main_ul = $(".comments-list");

					$("#no-comments-label").fadeOut().remove();

					main_ul.append(div).hide().fadeIn();
				} else {
					window.location.href = location.origin + "/Main/Login";
                }

			},
			complete: function () {

				$(".comment-count").text($(".comment").length);
				resizeNSC();
			},
			error: function (error) {
				console.log(error);

			}
		});
		return false;


	});


    $(".comments-list").on("click", ".reply-toggle", function () {


		console.log($(".comment").length)

        $(".reply-container-toggle").each(function () {

            $(this).fadeOut().remove();
        })

		var This = $(this);


        var CommentId = $(this).data("commentid");
        var BlogId = $(this).data("blogid");
        var UserId = $(this).data("userid");


        $.ajax({
            url: "/BlogG/FetchProfPic/",
			dataType: "json",
			type: 'GET',
			//async: false,
			success: function (response) {

				console.log(response);

				if (response === "Default") {
					var div = $('<li class="reply-container-toggle"><article class="comment"> <div class="profimage-container profimage-reply"><div class="profimage" style="background:url(/Assets/img/user-avatar-default.png)"></div></div><div class="comment-inner"><form method="post" id="postReply"><input type="hidden" name="BlogId" value="' + BlogId + '" /><input type="hidden" name="UserId" value="' + UserId + '" /><input type="hidden" name="CommentId" value="' + CommentId + '" /><textarea class="form-control comment-box" maxlength="500" name="Content" placeholder="Your Comment" rows="2"></textarea><div class="row"><div class="col-lg-12"><div class="reply-container"><a class="comment-btn" id="replyToComment" href="javascript:;">post</a><a class="comment-btn" id="cancelComment" href="javascript:;">Cancel</a></div></div></div></form></div></article></li>')
					setTimeout(function () {
						$('.comment-box').focus();
						console.log($('.comment-box'));

					}, 200);


					This.parent().parent().parent().parent().parent().find(".children").append(div).hide().fadeIn();
				}
				else if (response === "nopic") {

					window.location.href = location.origin + "/Main/Login";

				} else {

					var div = $('<li class="reply-container-toggle"><article class="comment"><div class="profimage-container profimage-reply"><div class="profimage" style="background:url(/Uploads/' + response + ')"></div></div><div class="comment-inner"><form method="post" id="postReply"><input type="hidden" name="BlogId" value="' + BlogId + '" /><input type="hidden" name="UserId" value="' + UserId + '" /><input type="hidden" name="CommentId" value="' + CommentId + '" /><textarea class="form-control comment-box" maxlength="500" name="Content" placeholder="Your Comment" rows="2"></textarea><div class="row"><div class="col-lg-12"><div class="reply-container"><a class="comment-btn" id="replyToComment" href="javascript:;">post</a><a class="comment-btn" id="cancelComment" href="javascript:;">Cancel</a></div></div></div></form></div></article></li>')

						setTimeout(function () {
							$('.comment-box').focus();
							console.log($('.comment-box'));

						}, 200);


						This.parent().parent().parent().parent().parent().find(".children").append(div).hide().fadeIn();

				 }
            },
            error: function (error) {
                console.log(error);
            }
        });





    });

	$(".comments-list").on("click", "#cancelComment", function () {

		$(".reply-container-toggle").each(function () {

			$(this).fadeOut().remove();
		})
	});


	$(".comments-list").on("click", "#replyToComment", function () {

		var formData = new FormData($("#postReply")[0]);
		$.ajax({
			url: '/BlogG/PostReply',
			type: 'POST',
			data: formData,
			cache: false,
			contentType: false,
			processData: false,
			//async: false,

			success: function (response) {
				if (response != "nousersession") {
					if (response.ProfPic != "Default") {
						var div = $('<li><article class="comment"><div class="profimage-container profimage-reply"><div class="profimage" style ="background:url(/Uploads/' + response.ProfPic + ')"></div></div><div class="comment-inner"><header class="comment-header row"><div class="col"><cite class="comment-author">' + response.Name + '</cite><time class="comment-datetime" datetime="2012-10-27">' + GetMonthName(response.PostMonth) + ' ' + response.PostDay + ', ' + response.PostYear + ' at ' + response.PostHour + ':' + response.PostMinute + '</time></div></header><div class="comment-body"><p>' + response.CommentContent + '</p></div></div></article></li >');

					} else {
						var div = $('<li><article class="comment"><div class="profimage-container profimage-reply"><div class="profimage" style ="background:url(/Assets/img/user-avatar-default.png)"></div></div><div class="comment-inner"><header class="comment-header row"><div class="col"><cite class="comment-author">' + response.Name + '</cite><time class="comment-datetime" datetime="2012-10-27">' + GetMonthName(response.PostMonth) + ' ' + response.PostDay + ', ' + response.PostYear + ' at ' + response.PostHour + ':' + response.PostMinute + '</time></div></header><div class="comment-body"><p>' + response.CommentContent + '</p></div></div></article></li >');

					}

					$("#replyToComment").parent().parent().parent().parent().parent().parent().parent().fadeOut(function () {
						$("#replyToComment").parent().parent().parent().parent().parent().parent().parent().parent().append(div);

					});


					
				} else {
					window.location.href = location.origin + "/Main/Login";
                }
			},
			complete: function () {
				$(".comment-count").text($(".comment").length);
				
				resizeNSC();
            },
			error: function (error) {
				console.log("error while updating");

			}
		});
		return false;


	});

	$(".brand-list-button").on("click", function () {

		$(".propertu-page-shortby").find(".selected").removeClass("selected");
		$(".propertu-page-shortby").find(`[data-value='Default']`).addClass("selected");
		$(".propertu-page-shortby").find(".current").html("Default");

		$(".service-menu").find(".active").removeClass("active");
		$(this).parent().addClass("active");
		$(".service-menu").find(".valid").removeClass("valid");
		$(this).addClass("valid");





		var Id = $(this).data("brandid");

		$.ajax({
			url: '/ModelG/ModelsList/' + Id,
			type: 'GET',
			cache: false,
			contentType: false,
			processData: false,
			beforeSend: function () {
				// Show image container
				$("#page-preloader").show();
			},

			success: function (response) {
				//console.log(response);

				$(".result_status").html("<b>" + response.length + "</b> Results")

				$(".car-grid-list-row").empty();

				if ($(".view_type").data("view") === "list") {
					for (var i = 0; i < response.length; i++) {
						var div = $('<div class="col-md-12"><div class= "single-offers"><div class="row"><div class="col-sm-6"><div class="offer-image list_view"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><img src="/Uploads/' + response[i].Picture + '" alt="offer 1"></a></div></div><div class="col-sm-6"><div class="offer-text"><a target="_blank"  href="/ModelG/Details/' + response[i].Id + '"><h3>' + response[i].Name + '</h3></a><h4>$' + response[i].Price + '<span>/ Day</span></h4><ul><li><i class="fa fa-car" aria-hidden="true"></i>Model: ' + response[i].Year + '</li><li><i class="fa fa-cogs" aria-hidden="true"></i>' + response[i].Transmission + '</li><li><i class="fa fa-dashboard" aria-hidden="true"></i>' + response[i].Engine + 'L</li><li><i class="fas fa-gas-pump" aria-hidden="true"></i>' + response[i].FuelType + '</li></ul><div class="offer-action"><a target="_blank" href="/ModelG/Reservation/' + response[i].Id + '" class="offer-btn-1">Rent Car</a><a target="_blank" href="/ModelG/Details/' + response[i].Id + '" class="offer-btn-2">Details</a></div></div></div></div></div></div >');


						$(".car-grid-list-row").append(div).hide().fadeIn();
					}

				}
				else {
					for (var i = 0; i < response.length; i++) {

						var div = $('<div class="col-md-6"><div class= "single-offers"><div class="offer-image"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><img src="/Uploads/' + response[i].Picture + '" alt="offer 1"></a></div><div class="offer-text"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><h3>' + response[i].Name + '</h3></a><h4>$' + response[i].Price + '<span>/ Day</span></h4><ul><li><i class="fa fa-car"></i>Model: ' + response[i].Year + '</li><li><i class="fas fa-gas-pump" aria-hidden="true"></i>' + response[i].FuelType + '</li></ul><ul><li><i class="fa fa-dashboard"></i>' + response[i].Engine + 'L</li><li><i class="fa fa-cogs"></i>' + response[i].Transmission + '</li></ul><div class="offer-action"><a target="_blank" href="/ModelG/Reservation/' + response[i].Id + '" class="offer-btn-1">Rent Car</a><a target="_blank" href="/ModelG/Details/' + response[i].Id + '" class="offer-btn-2">Details</a></div></div></div></div >');


						$(".car-grid-list-row").append(div).hide().fadeIn();
					}
				}

			},
			complete: function (data) {
				// Hide image container
				setTimeout(function () {
					$("#page-preloader").fadeOut();

				}, 200);
				resizeNSC();
			},
			error: function (error) {
				console.log("error while updating");

			}
		});

	});




	function sortModelGrid(sortvalue) {
		$("#sortList").on("change", function () {
			if ($(".propertu-page-shortby").find(".selected").data("value") === sortvalue) {
				//console.log("works");

				var id = $(".valid").data("brandid");

				sortData = {
					Id: id,
					Data: sortvalue
				}


				$.ajax({
					url: '/ModelG/SortModelList/',
					type: 'GET',
					data: sortData,
					dataType: "json",
					beforeSend: function () {
						// Show image container
						$("#page-preloader").show();
					},

					success: function (response) {
						//console.log(response);
						$(".result_status").html("<b>" + response.length + "</b> Results")


						$(".car-grid-list-row").empty();

						if ($(".view_type").data("view") === "list") {
							for (var i = 0; i < response.length; i++) {
								var div = $('<div class="col-md-12"><div class= "single-offers"><div class="row"><div class="col-sm-6"><div class="offer-image list_view"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><img src="/Uploads/' + response[i].Picture + '" alt="offer 1"></a></div></div><div class="col-sm-6"><div class="offer-text"><a target="_blank"  href="/ModelG/Details/' + response[i].Id + '"><h3>' + response[i].Name + '</h3></a><h4>$' + response[i].Price + '<span>/ Day</span></h4><ul><li><i class="fa fa-car" aria-hidden="true"></i>Model: ' + response[i].Year + '</li><li><i class="fa fa-cogs" aria-hidden="true"></i>' + response[i].Transmission + '</li><li><i class="fa fa-dashboard" aria-hidden="true"></i>' + response[i].Engine + 'L</li><li><i class="fas fa-gas-pump" aria-hidden="true"></i>' + response[i].FuelType + '</li></ul><div class="offer-action"><a target="_blank" href="/ModelG/Reservation/' + response[i].Id + '" class="offer-btn-1">Rent Car</a><a target="_blank" href="/ModelG/Details/' + response[i].Id + '" class="offer-btn-2">Details</a></div></div></div></div></div></div >');


								$(".car-grid-list-row").append(div).hide().fadeIn();
							}

						}
						else {
							for (var i = 0; i < response.length; i++) {

								var div = $('<div class="col-md-6"><div class= "single-offers"><div class="offer-image"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><img src="/Uploads/' + response[i].Picture + '" alt="offer 1"></a></div><div class="offer-text"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><h3>' + response[i].Name + '</h3></a><h4>$' + response[i].Price + '<span>/ Day</span></h4><ul><li><i class="fa fa-car"></i>Model: ' + response[i].Year + '</li><li><i class="fas fa-gas-pump" aria-hidden="true"></i>' + response[i].FuelType + '</li></ul><ul><li><i class="fa fa-dashboard"></i>' + response[i].Engine + 'L</li><li><i class="fa fa-cogs"></i>' + response[i].Transmission + '</li></ul><div class="offer-action"><a target="_blank" href="/ModelG/Reservation/' + response[i].Id + '" class="offer-btn-1">Rent Car</a><a target="_blank" href="/ModelG/Details/' + response[i].Id + '" class="offer-btn-2">Details</a></div></div></div></div >');


								$(".car-grid-list-row").append(div).hide().fadeIn();
							}
						}


					},
					complete: function (data) {
						// Hide image container
						setTimeout(function () {
							$("#page-preloader").fadeOut();

						}, 200);
						resizeNSC();

					},
					error: function (error) {
						console.log("error while updating");

					}
				});
			}
		})
	}

	sortModelGrid("Price (Low to High)");
	sortModelGrid("Price (High to Low)");
	sortModelGrid("Name (Z to A)");
	sortModelGrid("Name (A to Z)");
	sortModelGrid("Default");
	sortModelGrid("Year (Low to High)");
	sortModelGrid("Year (High to Low)");







	function findModels(searchValue) {
		$("#model-properties-button").on("click", function (e) {
			if ($(".propertu-page-shortby").find(".selected").data("value") === searchValue) {

				e.preventDefault();
				searchData = {
					FuelType: $("#FuelType").val(),
					EngineLayout: $("#EngineLayout").val(),
					DriveTrain: $("#DriveTrain").val(),
					Condition: $("#Condition").val(),
					Transmission: $("#Transmission").val(),
					BrandId: $(".valid").data("brandid"),
					Data: searchValue
				}


				$.ajax({
					url: '/ModelG/ModelProperties/',
					type: 'GET',
					data: searchData,
					dataType: "json",
					beforeSend: function () {
						$("#page-preloader").show();
					},

					success: function (response) {

						$(".result_status").html("<b>" + response.length + "</b> Results")

						$(".car-grid-list-row").empty();

						if ($(".view_type").data("view") === "list") {
							for (var i = 0; i < response.length; i++) {
								var div = $('<div class="col-md-12"><div class= "single-offers"><div class="row"><div class="col-sm-6"><div class="offer-image list_view"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><img src="/Uploads/' + response[i].Picture + '" alt="offer 1"></a></div></div><div class="col-sm-6"><div class="offer-text"><a target="_blank"  href="/ModelG/Details/' + response[i].Id + '"><h3>' + response[i].Name + '</h3></a><h4>$' + response[i].Price + '<span>/ Day</span></h4><ul><li><i class="fa fa-car" aria-hidden="true"></i>Model: ' + response[i].Year + '</li><li><i class="fa fa-cogs" aria-hidden="true"></i>' + response[i].Transmission + '</li><li><i class="fa fa-dashboard" aria-hidden="true"></i>' + response[i].Engine + 'L</li><li><i class="fas fa-gas-pump" aria-hidden="true"></i>' + response[i].FuelType + '</li></ul><div class="offer-action"><a target="_blank" href="/ModelG/Reservation/' + response[i].Id + '" class="offer-btn-1">Rent Car</a><a target="_blank" href="/ModelG/Details/' + response[i].Id + '" class="offer-btn-2">Details</a></div></div></div></div></div></div >');


								$(".car-grid-list-row").append(div).hide().fadeIn();
							}

						}
						else {
							for (var i = 0; i < response.length; i++) {

								var div = $('<div class="col-md-6"><div class= "single-offers"><div class="offer-image"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><img src="/Uploads/' + response[i].Picture + '" alt="offer 1"></a></div><div class="offer-text"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><h3>' + response[i].Name + '</h3></a><h4>$' + response[i].Price + '<span>/ Day</span></h4><ul><li><i class="fa fa-car"></i>Model: ' + response[i].Year + '</li><li><i class="fas fa-gas-pump" aria-hidden="true"></i>' + response[i].FuelType + '</li></ul><ul><li><i class="fa fa-dashboard"></i>' + response[i].Engine + 'L</li><li><i class="fa fa-cogs"></i>' + response[i].Transmission + '</li></ul><div class="offer-action"><a target="_blank" href="/ModelG/Reservation/' + response[i].Id + '" class="offer-btn-1">Rent Car</a><a target="_blank" href="/ModelG/Details/' + response[i].Id + '" class="offer-btn-2">Details</a></div></div></div></div >');


								$(".car-grid-list-row").append(div).hide().fadeIn();
							}
						}

						if (response.length == 0) {

							var noresultdiv = $('<div class="no_results text-center" style="margin-left:200px;"><h2>No Results</h2></div >')

							$(".car-grid-list-row").append(noresultdiv);


						}

					},
					complete: function (data) {
						// Hide image container
						setTimeout(function () {
							$("#page-preloader").fadeOut();

						}, 200);
						resizeNSC();

					},
					error: function (error) {
						console.log("error while updating");

					}
				});
			}
		});
	}

	findModels("Price (Low to High)");
	findModels("Price (High to Low)");
	findModels("Name (Z to A)");
	findModels("Name (A to Z)");
	findModels("Default");
	findModels("Year (Low to High)");
	findModels("Year (High to Low)");


	if (location.pathname == "/ModelG/List") {

		//console.log("works");
		var Id = $(".valid").data("brandid");

		$.ajax({
			url: '/ModelG/ModelsList/' + Id,
			type: 'GET',
			cache: false,
			contentType: false,
			processData: false,
			beforeSend: function () {
				// Show image container
				$("#page-preloader").show();
			},

			success: function (response) {
				//console.log(response);

				$(".result_status").html("<b>" + response.length + "</b> Results")


				$(".car-grid-list-row").empty();

				if ($(".view_type").data("view") === "list") {
					for (var i = 0; i < response.length; i++) {
						var div = $('<div data-name="'+response[i].Name+'" class="col-md-12"><div class= "single-offers"><div class="row"><div class="col-sm-6"><div class="offer-image list_view"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><img src="/Uploads/' + response[i].Picture + '" alt="offer 1"></a></div></div><div class="col-sm-6"><div class="offer-text"><a target="_blank"  href="/ModelG/Details/' + response[i].Id + '"><h3>' + response[i].Name + '</h3></a><h4>$' + response[i].Price + '<span>/ Day</span></h4><ul><li><i class="fa fa-car" aria-hidden="true"></i>Model: ' + response[i].Year + '</li><li><i class="fa fa-cogs" aria-hidden="true"></i>' + response[i].Transmission + '</li><li><i class="fa fa-dashboard" aria-hidden="true"></i>' + response[i].Engine + 'L</li><li><i class="fas fa-gas-pump" aria-hidden="true"></i>' + response[i].FuelType + '</li></ul><div class="offer-action"><a target="_blank" href="/ModelG/Reservation/' + response[i].Id + '" class="offer-btn-1">Rent Car</a><a target="_blank" href="/ModelG/Details/' + response[i].Id + '" class="offer-btn-2">Details</a></div></div></div></div></div></div >');


						$(".car-grid-list-row").append(div).hide().fadeIn();
					}

				}
				else {
					for (var i = 0; i < response.length; i++) {

						var div = $('<div data-name="' + response[i].Name +'" class="col-md-6"><div class= "single-offers"><div class="offer-image"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><img src="/Uploads/' + response[i].Picture + '" alt="offer 1"></a></div><div class="offer-text"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><h3>' + response[i].Name + '</h3></a><h4>$' + response[i].Price + '<span>/ Day</span></h4><ul><li><i class="fa fa-car"></i>Model: ' + response[i].Year + '</li><li><i class="fas fa-gas-pump" aria-hidden="true"></i>' + response[i].FuelType + '</li></ul><ul><li><i class="fa fa-dashboard"></i>' + response[i].Engine + 'L</li><li><i class="fa fa-cogs"></i>' + response[i].Transmission + '</li></ul><div class="offer-action"><a target="_blank" href="/ModelG/Reservation/' + response[i].Id + '" class="offer-btn-1">Rent Car</a><a target="_blank" href="/ModelG/Details/' + response[i].Id + '" class="offer-btn-2">Details</a></div></div></div></div >');


						$(".car-grid-list-row").append(div).hide().fadeIn();
					}
				}

			},
			complete: function (data) {
				// Hide image container
				setTimeout(function () {
					$("#page-preloader").fadeOut();

				}, 2000);

				
				resizeNSC();

			
			},
			error: function (error) {
				console.log("error while updating");

			}
		});

	}


	$(".model_searchbtn").on('click',function (e) {

		console.log("works")

		e.preventDefault();

		searchPane = {
			searchText: $("#model_searchpane").val()
		}
		$(".fl-title-vc").text("Results for '" + $("#model_searchpane").val() + "'")


		$.ajax({
			url: '/ModelG/ModelSearch/',
			type: 'GET',
			data: searchPane,
			dataType: "json",
			beforeSend: function () {
				// Show image container
				$("#page-preloader").show();
			},

			success: function (response) {
				//console.log(response);
				$(".result_status").html("<b>" + response.length + "</b> Results")


				$(".car-grid-list-row").empty();

				if ($(".view_type").data("view") === "list") {
					for (var i = 0; i < response.length; i++) {
						var div = $('<div class="col-md-12"><div class= "single-offers"><div class="row"><div class="col-sm-6"><div class="offer-image list_view"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><img src="/Uploads/' + response[i].Picture + '" alt="offer 1"></a></div></div><div class="col-sm-6"><div class="offer-text"><a target="_blank"  href="/ModelG/Details/' + response[i].Id + '"><h3>' + response[i].Name + '</h3></a><h4>$' + response[i].Price + '<span>/ Day</span></h4><ul><li><i class="fa fa-car" aria-hidden="true"></i>Model: ' + response[i].Year + '</li><li><i class="fa fa-cogs" aria-hidden="true"></i>' + response[i].Transmission + '</li><li><i class="fa fa-dashboard" aria-hidden="true"></i>' + response[i].Engine + 'L</li><li><i class="fas fa-gas-pump" aria-hidden="true"></i>' + response[i].FuelType + '</li></ul><div class="offer-action"><a target="_blank" href="/ModelG/Reservation/' + response[i].Id + '" class="offer-btn-1">Rent Car</a><a target="_blank" href="/ModelG/Details/' + response[i].Id + '" class="offer-btn-2">Details</a></div></div></div></div></div></div >');


						$(".car-grid-list-row").append(div).hide().fadeIn();
					}

				}
				else {
					for (var i = 0; i < response.length; i++) {

						var div = $('<div class="col-md-6"><div class= "single-offers"><div class="offer-image"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><img src="/Uploads/' + response[i].Picture + '" alt="offer 1"></a></div><div class="offer-text"><a target="_blank" href="/ModelG/Details/' + response[i].Id + '"><h3>' + response[i].Name + '</h3></a><h4>$' + response[i].Price + '<span>/ Day</span></h4><ul><li><i class="fa fa-car"></i>Model: ' + response[i].Year + '</li><li><i class="fas fa-gas-pump" aria-hidden="true"></i>' + response[i].FuelType + '</li></ul><ul><li><i class="fa fa-dashboard"></i>' + response[i].Engine + 'L</li><li><i class="fa fa-cogs"></i>' + response[i].Transmission + '</li></ul><div class="offer-action"><a target="_blank" href="/ModelG/Reservation/' + response[i].Id + '" class="offer-btn-1">Rent Car</a><a target="_blank" href="/ModelG/Details/' + response[i].Id + '" class="offer-btn-2">Details</a></div></div></div></div >');


						$(".car-grid-list-row").append(div).hide().fadeIn();
					}
				}

				if (response.length == 0) {

					var noresultdiv = $('<div class="no_results text-center" style="margin-left:200px;"><h2>No Results</h2></div >')

					$(".car-grid-list-row").append(noresultdiv);


				}

			},
			complete: function (data) {
				// Hide image container
				setTimeout(function () {
					$("#page-preloader").fadeOut();

				}, 200);
				resizeNSC();

			},
			error: function (error) {
				console.log("error while updating");

			}
		});
	});


	

	if ((urlSplit.indexOf("ServiceReservation") >= 0)) {
		console.log("works");
		var id = $("#dtpServiceId").data('id')
		var datesToDisableService = [];

		$.ajax({
			url: "/ServiceG/DatePicker/" + id,
			type: "get",
			dataType: "json",
			data: "",
			async: false,
			success: function (response) {
				console.log(response)


				for (var i = 0; i < response.length; i++) {
					datesToDisableService.push([parseInt(response[i].Year), parseInt(response[i].Month - 1), parseInt(response[i].Day)]);
				}
			},

			error: function (error) {
				console.log(error);
			}
		});

		$('#input_date').pickadate({
			format: 'dddd,  d mmmm',
			formatSubmit: 'd mmm',
			min: Date.now,
			max: new Date(2021, 7, 14),
			//disable: datesToDisable


		})
		var appdate_$input = $('#input_date').pickadate({
			container: '#root-picker-outlet',
			onOpen: function () {
				$('#root-picker-outlet').css("display", "block");
			},
			onClose: function () {
				$('#root-picker-outlet').css("display", "none");
			},
		}),
			app_picker = appdate_$input.pickadate('picker')

		app_picker.set('disable', datesToDisableService)
	}


	$("#appServiceButton").on("click", function (e) {

		if ($("#appService").valid()) {

			var formData = new FormData($("#appService")[0]);
			$.ajax({
				url: '/ServiceG/Reservation',
				type: 'POST',
				data: formData,
				cache: false,
				contentType: false,
				processData: false,
				success: function (response) {

					console.log(response);

					if (response === "true") {
						console.log("updated");
						$("#appService").fadeOut(function () {
							$("#notifyProfileUpdate").fadeIn();
						});
					} else if(response==="sessionerror") {
						$("#appService").fadeOut(function () {
							$("#notifySessionError").fadeIn();

							setTimeout(function () {

								window.location.href = location.origin + "/Main/Login";
							},3000);
						});
					}

					else {
						console.log("no updates");
						$("#appService").fadeOut(function () {
							$("#notifyProfileError").fadeIn();
						});
					}

				}, complete: function () {


					resizeNSC();
				},
				error: function (error) {
					console.log(error);
				}
			});
        }


	})


	$("#bookingpost-btn").on("click", function (e) {

		if ($("#bookingForm").valid()) {

			var formData = new FormData($("#bookingForm")[0]);
			$.ajax({
				url: '/ModelG/Reservation',
				type: 'POST',
				data: formData,
				cache: false,
				contentType: false,
				processData: false,
				success: function (response) {


                    if (response === "false") {

                        $("#notifymodelerror").fadeIn();


                    } else if (response === "sessionerror") {
                        $("#notifySessionError").fadeIn();

                        setTimeout(function () {

                            window.location.href = location.origin + "/Main/Login";
                        }, 3000);
                    } else {
                        window.location.href = location.origin + "/ModelG/ResSuccess/" + response;
                    }



				}, complete: function () {


					resizeNSC();
				},
				error: function (error) {
					console.log(error);
				}
			});
		}
	})

	$("#load_more_blog").on("click", function () {

		console.log("works");

		$.ajax({
			url: '/BlogG/IndexScroll?part=' + 1,
			type: 'GET',
			dataType: "json",
			beforeSend: function () {
				$("#page-preloader").show();
			},

			success: function (response) {
				var maindiv = $(".blog-page-left");

				console.log(response)

				for (var i = 0; i < response.length; i++) {
					var div = $('<div class="blog-single bg-light"><div class="half-post-entry d-block d-lg-flex"><a target="_blank" href="/BlogG/Details/' + response[i].BlogId + '"><div class="img-bg" style="background-image: url(/Uploads/' + response[i].Image + ')"></div></a><div class="contents"><span class="caption">Editors Pick</span><h2><a target="_blank" href="/BlogG/Details/' + response[i].BlogId + '">' + response[i].Title + '</a></h2><p class="mb-3">' + response[i].Description + '</p><div class="post-meta"><span class="d-block"><a href="#">' + response[i].Admin + '</a> in <a href="#">' + response[i].Category + '</a></span><span class="date-read">' + GetMonthName(response[i].PostMonth) + " " + response[i].PostDay + '<span class="mx-1">•</span> ' + parseInt(response[i].Content.length / 20000) + ' min read <span class="icon-star2"></span></span></div></div></div></div>');

					maindiv.append(div);
				}

				if (response.length < 4) {
					$("#load_more_blog").fadeOut();
				}

			},
			complete: function (data) {
				setTimeout(function () {
					$("#page-preloader").fadeOut();

				}, 200);
				resizeNSC();
			},
			error: function (error) {
				console.log("error while updating");

			}
		});

	});


	$(".blog_cat_btn").on("click", function () {

		$(".active_seeker").each(function () {
			$(this).removeClass("active");
		})

		$(this).parent().addClass("active");

		CatId = $(this).data("id");

		$.ajax({
			url: '/BlogG/BringCat?id=' + CatId,
			type: 'GET',
			dataType: "json",
			beforeSend: function () {
				$("#page-preloader").show();
			},

			success: function (response) {

			

				$(".fl-title-vc").text("Results for '" + response[0].CatName + "'")

				var maindiv = $(".blog-page-left");

				console.log(response)

				maindiv.empty();

				for (var i = 0; i < response.length; i++) {
					var div = $('<div class="blog-single bg-light"><div class="half-post-entry d-block d-lg-flex"><a target="_blank" href="/BlogG/Details/' + response[i].BlogId + '"><div class="img-bg" style="background-image: url(/Uploads/' + response[i].Image + ')"></div></a><div class="contents"><span class="caption">Editors Pick</span><h2><a target="_blank" href="/BlogG/Details/' + response[i].BlogId + '">' + response[i].Title + '</a></h2><p class="mb-3">' + response[i].Description + '</p><div class="post-meta"><span class="d-block"><a href="#">' + response[i].Admin + '</a> in <a href="#">' + response[i].Category + '</a></span><span class="date-read">' + GetMonthName(response[i].PostMonth) + " " + response[i].PostDay + '<span class="mx-1">•</span> ' + parseInt(response[i].Content.length / 20000) + ' min read <span class="icon-star2"></span></span></div></div></div></div>');

					maindiv.append(div);
				}

				

			},
			complete: function (data) {
				setTimeout(function () {
					$("#page-preloader").fadeOut();

				}, 200);
				resizeNSC();
			},
			error: function (error) {
				console.log(error);

			}
		});

	});

	$(".blog_tag_btn").on("click", function () {

		$(".active_seeker").each(function () {
			$(this).removeClass("active");
		})

		$(this).parent().addClass("active");
		

		TagId = $(this).data("id");

		$.ajax({
			url: '/BlogG/BringTag?id=' + TagId,
			type: 'GET',
			dataType: "json",
			beforeSend: function () {
				$("#page-preloader").show();
			},

			success: function (response) {

			

				$(".fl-title-vc").text("Results for '"+response[0].TagName+"'")



				var maindiv = $(".blog-page-left");



				console.log(response)

				maindiv.empty();

				for (var i = 0; i < response.length; i++) {
					var div = $('<div class="blog-single bg-light"><div class="half-post-entry d-block d-lg-flex"><a target="_blank" href="/BlogG/Details/' + response[i].BlogId + '"><div class="img-bg" style="background-image: url(/Uploads/' + response[i].Image + ')"></div></a><div class="contents"><span class="caption">Editors Pick</span><h2><a target="_blank" href="/BlogG/Details/' + response[i].BlogId + '">' + response[i].Title + '</a></h2><p class="mb-3">' + response[i].Description + '</p><div class="post-meta"><span class="d-block"><a href="#">' + response[i].Admin + '</a> in <a href="#">' + response[i].Category + '</a></span><span class="date-read">' + GetMonthName(response[i].PostMonth) + " " + response[i].PostDay + '<span class="mx-1">•</span> ' + parseInt(response[i].Content.length / 20000) + ' min read <span class="icon-star2"></span></span></div></div></div></div>');

					maindiv.append(div);
				}



			},
			complete: function (data) {
				setTimeout(function () {
					$("#page-preloader").fadeOut();

				}, 200);
				resizeNSC();
			},
			error: function (error) {
				console.log("error while updating");

			}
		});

	});

	//$(".product_search").submit(function (e) {

	//	e.preventDefault();
	//});


	$(".search_blog_btn").on("click", function (e) {
		e.preventDefault();

		var sdata = $(this).prev().val();

		//console.log($(this).parent().val());

		searchText = {
			searchData: $(this).prev().val()
        } 

		$.ajax({
			url: '/BlogG/BlogSearch/',
			type: 'GET',
			data: searchText,
			dataType: "json",
			beforeSend: function () {
				$("#page-preloader").show();
			},

			success: function (response) {

				$(".active_seeker").removeClass("active");

				$(".fl-title-vc").text("Results for '" + sdata + "'")

				var maindiv = $(".blog-page-left");


			

				maindiv.empty();

				for (var i = 0; i < response.length; i++) {
					var div = $('<div class="blog-single bg-light"><div class="half-post-entry d-block d-lg-flex"><a target="_blank" href="/BlogG/Details/' + response[i].BlogId + '"><div class="img-bg" style="background-image: url(/Uploads/' + response[i].Image + ')"></div></a><div class="contents"><span class="caption">Editors Pick</span><h2><a target="_blank" href="/BlogG/Details/' + response[i].BlogId + '">' + response[i].Title + '</a></h2><p class="mb-3">' + response[i].Description + '</p><div class="post-meta"><span class="d-block"><a href="#">' + response[i].Admin + '</a> in <a href="#">' + response[i].Category + '</a></span><span class="date-read">' + GetMonthName(response[i].PostMonth) + " " + response[i].PostDay + '<span class="mx-1">•</span> ' + parseInt(response[i].Content.length / 20000) + ' min read <span class="icon-star2"></span></span></div></div></div></div>');

					maindiv.append(div);
				}

				if (response.length == 0) {

					var noresultdiv = $('<div class="no_results text-center"><h2>No Results</h2></div >')

					maindiv.append(noresultdiv);


				}

			},
			complete: function (data) {
				setTimeout(function () {
					$("#page-preloader").fadeOut();

				}, 200);
				resizeNSC();
			},
			error: function (error) {
				console.log("error while updating");

			}
		});


	});

/*************************************************************************************************************************************************************************************************************************************************************************/
	autosize($('textarea'));




	$("#u_account_sidebtn").on("click", function () {

		$(".item-selection").each(function () {
			$(this).parent().removeClass("active");
		});

		$(".fl-change").text("Account");

		$(this).parent().addClass("active");

		$("#page-preloader").fadeIn();
		$("#u-reservations").fadeOut();
		$("#u-products").fadeOut();
		$("#u-testify").fadeOut();
		$("#u-appointments").fadeOut();
		setTimeout(function () {

			$("#u-profile").fadeIn();
			$("#page-preloader").fadeOut();

		}, 300);

		
		resizeNSC();
	});

	$("#u_res_sidebtn").on("click", function () {

		$(".item-selection").each(function () {
			$(this).parent().removeClass("active");
		});

		$(".fl-change").text("Reservations");


		$(this).parent().addClass("active");

		$("#page-preloader").fadeIn();
		$("#u-profile").fadeOut();
		$("#u-appointments").fadeOut();
		$("#u-products").fadeOut();
		$("#u-testify").fadeOut();

		setTimeout(function () {

			$("#u-reservations").fadeIn();
			$("#page-preloader").fadeOut();

		}, 300);
		
		resizeNSC();
	});

	$("#u_orders_sidebtn").on("click", function () {

		$(".item-selection").each(function () {
			$(this).parent().removeClass("active");
		});

		$(".fl-change").text("Orders");


		$(this).parent().addClass("active");

		$("#page-preloader").fadeIn();
		$("#u-profile").fadeOut();
		$("#u-appointments").fadeOut();
		$("#u-reservations").fadeOut();
		$("#u-testify").fadeOut();

		setTimeout(function () {

			$("#u-products").fadeIn();
			$("#page-preloader").fadeOut();

		}, 300);
		
		resizeNSC();

		$("#page-preloader").fadeOut();
	});

	$("#u_app_sidebtn").on("click", function () {


		$(".item-selection").each(function () {
			$(this).parent().removeClass("active");
		});

		$(".fl-change").text("Appointments");


		$(this).parent().addClass("active");

		$("#page-preloader").fadeIn();
		$("#u-profile").fadeOut();
		$("#u-products").fadeOut();
		$("#u-testify").fadeOut();
		$("#u-reservations").fadeOut();
		setTimeout(function () {

			$("#u-appointments").fadeIn();
			$("#page-preloader").fadeOut();

		}, 300);

		
		resizeNSC();
		$("#page-preloader").fadeOut();
	})
	$("#u_testify_sidebtn").on("click", function () {


		$(".item-selection").each(function () {
			$(this).parent().removeClass("active");
		});

		$(".fl-change").text("Reviews");


		$(this).parent().addClass("active");

		$("#page-preloader").fadeIn();
		$("#u-profile").fadeOut();
		$("#u-products").fadeOut();
		$("#u-reservations").fadeOut();
		$("#u-appointments").fadeOut();
		setTimeout(function () {

			$("#u-testify").fadeIn();
			$("#page-preloader").fadeOut();

		}, 300);


		resizeNSC();
		$("#page-preloader").fadeOut();
	})

	$(".cancel_app").on('click', function () {

		var This = $(this);
		var appId = $(this).data("id");

		$.ajax({
			url: '/UserG/CancelApp/' + appId,
			type: 'POST',
			cache: false,
			contentType: false,
			processData: false,
			beforeSend: function () {
				// Show image container
				$("#page-preloader").show();
			},
			success: function (response) {

				if (response === "true") {
					console.log("updated");
					This.parent().parent().parent().parent().fadeOut(function () {
						$("#notifyAppUpdate").fadeIn();
					});
					

				} else {
					console.log("no updates");
					This.parent().parent().parent().parent().fadeOut(function () {
						$("#notifyAppError").fadeIn();
					});
				}

			},
			complete: function (data) {
				// Hide image container
				setTimeout(function () {
					$("#page-preloader").fadeOut();

				}, 200);
				resizeNSC();
			},
			error: function (error) {
				console.log("error while updating");
			}
		});



	})


	//*****************************************************************sorting-js*************************************************************

	function myfunction() {
		$("#page-preloader").fadeIn();
		var parent = document.querySelector(".car-grid-list-row"),
			itemsArray = Array.prototype.slice.call(parent.children);

		itemsArray.sort(function (a, b) {
			if (a.dataset.name < b.dataset.name) return -1;
			if (a.dataset.name > b.dataset.name) return 1;
			return 0;
		});

		itemsArray.forEach(function (item) {
			parent.appendChild(item);
		});

		

		setTimeout(function () {

			$("#page-preloader").fadeOut();
		},200)
    }

	$("#sortProduct").on("change", function () {
		if ($("#sortProduct").val() == "Price (Low to High)") {
			$("#page-preloader").fadeIn();
			var parent = document.querySelector("#productSort"),
				itemsArray = Array.prototype.slice.call(parent.children);

			itemsArray.sort(function (a, b) {
				if (parseFloat(a.dataset.price) < parseFloat(b.dataset.price)) return -1;
				if (parseFloat(a.dataset.price) > parseFloat(b.dataset.price)) return 1;
				return 0;
			});

			itemsArray.forEach(function (item) {
				parent.appendChild(item);
			});



		

			setTimeout(function () {

				$("#page-preloader").fadeOut();
			}, 200)
		}
		else if ($("#sortProduct").val() == "Price (High to Low)") {
			$("#page-preloader").fadeIn();
			var parent = document.querySelector("#productSort"),
				itemsArray = Array.prototype.slice.call(parent.children);

			itemsArray.sort(function (a, b) {
				if (parseFloat(a.dataset.price) > parseFloat(b.dataset.price)) return -1;
				if (parseFloat(a.dataset.price) < parseFloat(b.dataset.price)) return 1;
				return 0;
			});

			itemsArray.forEach(function (item) {
				parent.appendChild(item);
			});



			setTimeout(function () {

				$("#page-preloader").fadeOut();
			}, 200)
		}

		else if ($("#sortProduct").val() == "Name (A to Z)") {
			$("#page-preloader").fadeIn();
			var parent = document.querySelector("#productSort"),
				itemsArray = Array.prototype.slice.call(parent.children);

			itemsArray.sort(function (a, b) {
				if (a.dataset.name < b.dataset.name) return -1;
				if (a.dataset.name > b.dataset.name) return 1;
				return 0;
			});

			itemsArray.forEach(function (item) {
				parent.appendChild(item);
			});



			setTimeout(function () {

				$("#page-preloader").fadeOut();
			}, 200)
		}
		else if ($("#sortProduct").val() == "Name (Z to A)") {
			$("#page-preloader").fadeIn();
			var parent = document.querySelector("#productSort"),
				itemsArray = Array.prototype.slice.call(parent.children);

			itemsArray.sort(function (a, b) {
				if (a.dataset.name > b.dataset.name) return -1;
				if (a.dataset.name < b.dataset.name) return 1;
				return 0;
			});

			itemsArray.forEach(function (item) {
				parent.appendChild(item);
			});



			setTimeout(function () {

				$("#page-preloader").fadeOut();
			}, 200)
		}

		else if ($("#sortProduct").val() == "Date Added (Low to High)") {
			$("#page-preloader").fadeIn();
			var parent = document.querySelector("#productSort"),
				itemsArray = Array.prototype.slice.call(parent.children);


			itemsArray.sort(function (a, b) {
				if (a.dataset.addeddate < b.dataset.addeddate) return -1;
				if (a.dataset.addeddate > b.dataset.addeddate) return 1;
				return 0;
			});

			itemsArray.forEach(function (item) {
				parent.appendChild(item);
			});



			setTimeout(function () {

				$("#page-preloader").fadeOut();
			}, 200)
		}
		else if ($("#sortProduct").val() == "Date Added (High to Low)") {
			$("#page-preloader").fadeIn();
			var parent = document.querySelector("#productSort"),
				itemsArray = Array.prototype.slice.call(parent.children);


			itemsArray.sort(function (a, b) {
				if (a.dataset.addeddate > b.dataset.addeddate) return -1;
				if (a.dataset.addeddate < b.dataset.addeddate) return 1;
				return 0;
			});

			itemsArray.forEach(function (item) {
				parent.appendChild(item);
			});



			setTimeout(function () {

				$("#page-preloader").fadeOut();
			}, 200)
		}
	})
	//*****************************************************************sorting-js*************************************************************

	//$(document).scroll(function () {
	//	var docHeight = $(document).height();
	//	var winHeight = $(window).height();
	//	var scrollTop = $(window).scrollTop();
		

	//	if (docHeight == (winHeight + scrollTop)) {


	//	}
	//});

	$(".pr_cat").on("click", function () {

		var This = $(this);

		var CatId = $(this).data("id");

		var CatName = $(this).text();

		$(".active_seeker").each(function () {
			$(this).removeClass("active");
		})

		This.parent().addClass("active");

		$.ajax({
			url: '/ProductG/ProductsLoad/'+CatId,
			type: 'GET',
			dataType: "json",
			beforeSend: function () {
				$("#page-preloader").show();
			},

			success: function (response) {

				$(".result_status").text(""+response.length+" result(s)")

				This.parent().addClass("active");


				$(".fl-title-vc").text("Results for '" + CatName + "'")

				var maindiv = $("#productSort");




				maindiv.empty();

				for (var i = 0; i < response.length; i++) {
					if (response[i].IsNew) {
						if (response[i].Amount > 0) {
							var div = $('<div data-name="' + response[i].Name + '" data-addeddate="' + response[i].Date + '" data-price="' + response[i].Price + '"  class="col-12 col-sm-6 col-md-4 col-lg-4 col-xs-12 mt-3 mb-3"><div class= "product-container" ><div class="product-item "><div class="product-image"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '"><div class="pr-image" style="background:url(/Uploads/' + response[i].Picture + ')"></div></a><div class="status-top"><a target="_blank" class="item-condition" href="">New</a></div></div><div class= "product-text" ><div class="product-title"><h3><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">' + response[i].Name + '</a></h3><span class="item-cat">' + response[i].Category + '</span><p>$' + response[i].Price + '</p></div ></div ><div class="add-to-cart"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">View Item</a></div ></div ></div ></div >');

						} else {
							var div = $('<div data-name="' + response[i].Name + '" data-addeddate="' + response[i].Date + '" data-price="' + response[i].Price + '"  class="col-12 col-sm-6 col-md-4 col-lg-4 col-xs-12 mt-3 mb-3"><div class= "product-container" ><div class="product-item "><div class="product-image"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '"><div class="pr-image" style="background:url(/Uploads/' + response[i].Picture + ')"></div></a><div class="status-top"><a target="_blank" class="item-condition" href="">New</a></div><div class="status_sold_out"><span class="item_availability">Sold Out</span></div></div><div class= "product-text" ><div class="product-title"><h3><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">' + response[i].Name + '</a></h3><span class="item-cat">' + response[i].Category + '</span><p>$' + response[i].Price + '</p></div ></div ><div class="add-to-cart"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">View Item</a></div ></div ></div ></div >');

                        }
					} else {
						if (response[i].Amount > 0) {
							var div = $('<div data-name="' + response[i].Name + '" data-addeddate="' + response[i].Date + '" data-price="' + response[i].Price + '"  class="col-12 col-sm-6 col-md-4 col-lg-4 col-xs-12 mt-3 mb-3"><div class= "product-container" ><div class="product-item "><div class="product-image"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '"><div class="pr-image" style="background:url(/Uploads/' + response[i].Picture + ')"></div></a></div><div class= "product-text" ><div class="product-title"><h3><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">' + response[i].Name + '</a></h3><span class="item-cat">' + response[i].Category + '</span><p>$' + response[i].Price + '</p></div ></div ><div class="add-to-cart"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">View Item</a></div ></div ></div ></div >');

						}
						else {
							var div = $('<div data-name="' + response[i].Name + '" data-addeddate="' + response[i].Date + '" data-price="' + response[i].Price + '"  class="col-12 col-sm-6 col-md-4 col-lg-4 col-xs-12 mt-3 mb-3"><div class= "product-container" ><div class="product-item "><div class="product-image"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '"><div class="pr-image" style="background:url(/Uploads/' + response[i].Picture + ')"></div></a><div class="status_sold_out"><span class="item_availability">Sold Out</span></div></div><div class= "product-text" ><div class="product-title"><h3><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">' + response[i].Name + '</a></h3><span class="item-cat">' + response[i].Category + '</span><p>$' + response[i].Price + '</p></div ></div ><div class="add-to-cart"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">View Item</a></div ></div ></div ></div >');

                        }
					}

					maindiv.append(div);

				}

				if (response.length == 0) {

					var noresultdiv = $('<div class="no_results text-center"><h2>No Results</h2></div >')

					maindiv.append(noresultdiv);


				}

			},
			complete: function (data) {
				setTimeout(function () {
					$("#page-preloader").fadeOut();

				}, 200);
				resizeNSC();
			},
			error: function (error) {
				console.log("error while updating");
				console.log(error);

			}
		});


	});

	$(".search_product_btn").on("click", function (e) {
		e.preventDefault();

		var sdata = $(this).prev().val();

		//console.log($(this).parent().val());

		searchText = {
			searchData: $(this).prev().val()
		}

		$.ajax({
			url: '/ProductG/ProductSearch/',
			type: 'GET',
			data: searchText,
			dataType: "json",
			async: false,
			beforeSend: function () {
				$("#page-preloader").show();
			},

			success: function (response) {

				$(".result_status").text("" + response.length + " result(s)")


				$(".active_seeker").removeClass("active");

				$(".fl-title-vc").text("Results for '" + sdata + "'")

				var maindiv = $("#productSort");


				console.log(response);

				maindiv.empty();

				for (var i = 0; i < response.length; i++) {
					
					if (response[i].IsNew) {
						if (response[i].Amount > 0) {
							var div = $('<div data-name="' + response[i].Name + '" data-addeddate="' + response[i].Date + '" data-price="' + response[i].Price + '"  class="col-12 col-sm-6 col-md-4 col-lg-4 col-xs-12 mt-3 mb-3"><div class= "product-container" ><div class="product-item "><div class="product-image"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '"><div class="pr-image" style="background:url(/Uploads/' + response[i].Picture + ')"></div></a><div class="status-top"><a target="_blank" class="item-condition" href="">New</a></div></div><div class= "product-text" ><div class="product-title"><h3><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">' + response[i].Name + '</a></h3><span class="item-cat">' + response[i].Category + '</span><p>$' + response[i].Price + '</p></div ></div ><div class="add-to-cart"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">View Item</a></div ></div ></div ></div >');

						} else {
							var div = $('<div data-name="' + response[i].Name + '" data-addeddate="' + response[i].Date + '" data-price="' + response[i].Price + '"  class="col-12 col-sm-6 col-md-4 col-lg-4 col-xs-12 mt-3 mb-3"><div class= "product-container" ><div class="product-item "><div class="product-image"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '"><div class="pr-image" style="background:url(/Uploads/' + response[i].Picture + ')"></div></a><div class="status-top"><a target="_blank" class="item-condition" href="">New</a><div class="status_sold_out"><span class="item_availability">Sold Out</span></div></div></div><div class= "product-text" ><div class="product-title"><h3><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">' + response[i].Name + '</a></h3><span class="item-cat">' + response[i].Category + '</span><p>$' + response[i].Price + '</p></div ></div ><div class="add-to-cart"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">View Item</a></div ></div ></div ></div >');

						}
					} else {
						if (response[i].Amount > 0) {
							var div = $('<div data-name="' + response[i].Name + '" data-addeddate="' + response[i].Date + '" data-price="' + response[i].Price + '"  class="col-12 col-sm-6 col-md-4 col-lg-4 col-xs-12 mt-3 mb-3"><div class= "product-container" ><div class="product-item "><div class="product-image"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '"><div class="pr-image" style="background:url(/Uploads/' + response[i].Picture + ')"></div></a></div><div class= "product-text" ><div class="product-title"><h3><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">' + response[i].Name + '</a></h3><span class="item-cat">' + response[i].Category + '</span><p>$' + response[i].Price + '</p></div ></div ><div class="add-to-cart"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">View Item</a></div ></div ></div ></div >');

						}
						else {
							var div = $('<div data-name="' + response[i].Name + '" data-addeddate="' + response[i].Date + '" data-price="' + response[i].Price + '"  class="col-12 col-sm-6 col-md-4 col-lg-4 col-xs-12 mt-3 mb-3"><div class= "product-container" ><div class="product-item "><div class="product-image"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '"><div class="pr-image" style="background:url(/Uploads/' + response[i].Picture + ')"></div><div class="status_sold_out"><span class="item_availability">Sold Out</span></div></a></div><div class= "product-text" ><div class="product-title"><h3><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">' + response[i].Name + '</a></h3><span class="item-cat">' + response[i].Category + '</span><p>$' + response[i].Price + '</p></div ></div ><div class="add-to-cart"><a target="_blank" href="/ProductG/Details/' + response[i].ProductId + '">View Item</a></div ></div ></div ></div >');

						}
					}
			
					maindiv.append(div);


					

				}

				if (response.length == 0) {

					var noresultdiv = $('<div class="no_results text-center"><h2>No Results</h2></div >')

					maindiv.append(noresultdiv);


				}

			},
			complete: function (data) {
				setTimeout(function () {
					$("#page-preloader").fadeOut();

				}, 200);
				resizeNSC();
			},
			error: function (error) {
				console.log("error while updating");

			}
		});
	});

	$("#place_order").on("click", function (e) {
		var formData = new FormData($("#checkoutForm")[0]);
		if ($("#checkoutForm").valid()) {

			$.ajax({
				url: '/ProductG/CheckOutAjax',
				type: 'POST',
				cache: false,
				data: formData,
				contentType: false,
				processData: false,
				beforeSend: function () {
					$("#page-preloader").show();
				},
				success: function (response) {
					if (response === "false") {
						$("#notifyUserProfileError").fadeIn();

					} else {

						var TotalPrice = 0;

						for (var i = 0; i < response.length; i++) {
							var div = $('<tr class="woocommerce-table__line-item order_item"><td class="woocommerce-table__product-name product-name"><a href="/ProductG/Details/' + response[i].ProductId + '">' + response[i].Name + '</a><strong class="product-quantity">×&nbsp;' + response[i].Amount + '</strong></td><td class="woocommerce-table__product-total product-total"><span class="woocommerce-Price-amount amount"><span class="woocommerce-Price-currencySymbol">$</span>' + response[i].Price + '</span></td></tr >');

							TotalPrice += response[i].Price;

							$(".appendix").append(div);




						}

						$(".total_price").text(TotalPrice.toFixed(2));
						$(".order_number").text(response[0].SaleId)
						$(".order_date").text("" + GetMonthName(response[0].Month) + " " + response[0].Day + ", " + response[0].Year + "")
						$(".cartCount").text(0);
						$("#checkoutForm").remove();
						$("#order_summary").fadeIn();


					}

				},
				complete: function (data) {
					// Hide image container
					setTimeout(function () {
						$("#page-preloader").fadeOut();

					}, 200);
					resizeNSC();
				},
				error: function (error) {
					console.log(error);

				}
			});
        }
	});


	$("#contactSupportbtn").on('click', function (e) {
		//e.preventDefault();
		console.log(true);

		var formData = new FormData($("#contactSupport")[0]);

		if ($("#contactSupport").valid()) {

			$.ajax({
				url: '/ContactG/ContactSupport',
				type: 'POST',
				cache: false,
				data: formData,
				contentType: false,
				processData: false,
				beforeSend: function () {
					$("#page-preloader").show();
				},
				success: function (response) {

					if (response == "true") {

						console.log("nice");

						$("#contactSupport").fadeOut(function () {
							$(this).remove()
						});
						$("#notifyProfileUpdate").fadeIn();

					}
					else {

					}

				},
				complete: function (data) {
					// Hide image container
					setTimeout(function () {
						$("#page-preloader").fadeOut();

					}, 200);
					resizeNSC();
				},
				error: function (error) {
					console.log(error);

				}
			});

		}
	});

	$("#subscribeNewsbtn").on("click", function () {


		var formData = new FormData($("#subscribeNews")[0]);

		if ($("#subscribeNews").valid()) {
			$.ajax({
				url: '/ContactG/SubscribeNews',
				type: 'POST',
				cache: false,
				data: formData,
				contentType: false,
				processData: false,

				success: function (response) {

					if (response == "true") {
						$(".success_subcription").text("Succesfully subscribed!");

						$(".success_subcription").css({
							"color": "white"
						})

						$(".success_subcription").fadeIn();

						setTimeout(function () {
							$(".success_subcription").fadeOut();

						}, 2000);
					}
					else {
						$(".success_subcription").text("Already subscribed.");

						$(".success_subcription").css({
							"color": "#e2b71c"
						})

						$(".success_subcription").fadeIn();

						setTimeout(function () {
							$(".success_subcription").fadeOut();

						}, 2000);
					}

				},
				error: function (error) {

				}
			});
		}

		
	});

	function readURL(input) {
		if (input.files && input.files[0]) {
			var reader = new FileReader();
			if (input.files[0].size > 528385) {
				$("#notifyImageSizeError").fadeIn();

				setTimeout(function () {
					$("#notifyImageSizeError").fadeOut();

				}, 3000)

				$("#imageUpload").attr("src", "blank");
				$("#imageUpload").hide();
				$('#imageUpload').wrap('<form>').closest('form').get(0).reset();
				$('#imageUpload').unwrap();
				return false;
			}
			if (input.files[0].type.indexOf("image") == -1) {
				$("#notifyImageTypeError").fadeIn();

				setTimeout(function () {
					$("#notifyImageTypeError").fadeOut();

				}, 3000)
				$("#imageUpload").attr("src", "blank");
				$("#imageUpload").hide();
				$('#imageUpload').wrap('<form>').closest('form').get(0).reset();
				$('#imageUpload').unwrap();
				return false;
			}   

			reader.onload = function (e) {
				$('#imagePreview').css('background-image', 'url(' + e.target.result + ')');
				$('#imagePreview').hide();
				$('#imagePreview').fadeIn(650);
			}
			reader.readAsDataURL(input.files[0]);
		}
	}
	$("#imageUpload").change(function () {
		readURL(this);
	});


	
});

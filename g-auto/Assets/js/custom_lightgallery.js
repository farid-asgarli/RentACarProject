/************* Main Js File ************************
    Template Name: Hemio
    Author: Themescare
    Version: 1.0
    Copyright 2019
*************************************************************/


(function ($) {
	"use strict";

	jQuery(document).ready(function ($) {


		/* 
		=================================================================
		masked input JS
		=================================================================	
		*/
		$('#lightgallery').lightGallery({
			selector: '.col-lg-4'
		});
		//var $gallery = $('#lightgallery');

		//$gallery.on('onBeforeOpen.lg', function (event, prevIndex, index) {
		//	$('body').css('overflow', 'hidden')
		//});
		//$gallery.on('onBeforeClose.lg', function (event, prevIndex, index) {
		//	$('body').css('overflow', 'auto')
		//});
	});


}(jQuery));


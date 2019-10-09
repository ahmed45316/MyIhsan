$(document).ready(function () {

    $(".newslider").slick({

        // normal options...
        infinite: true,
        rtl: true,
        autoplay: true,
        autoplaySpeed: 8000,
        fade: true,
        arrows: false
    })

    $('.loginBoxContainer').removeAttr('style');
    $('.loginBoxContainer').addClass('animated');

    $(window).on("load", (function () {
        //console.log('loaded');
    }))

});
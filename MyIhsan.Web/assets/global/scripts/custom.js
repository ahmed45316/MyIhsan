$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    setNavigation();
})
function setNavigation() {
    var path = window.location.href;
    var pathsmall = window.location.pathname;

    path = path.replace(/\/$/, "");
    path = decodeURIComponent(path);

    //console.log(path);
    //console.log(pathsmall);

    var homepath = path.substring(0, path.lastIndexOf(pathsmall));
    //console.log(homepath);
    //console.log(homepath.length);

    var newpath = path.slice(homepath.length);
    //console.log(newpath);



    $(".page-sidebar-menu a").each(function () {
        var href = $(this).attr('href');
        //console.log(href);
        //console.log(path.substring(0, href.length));

        //if (path.substring(0, href.length) === href) {
        if (newpath === href) {
        //if (path === href) {
            console.log(href);

            $(this).closest('li').addClass('activeItems');
            $(this).parents('.sub-menu').css('display', 'block').parents(".nav-item").addClass('open').parents(".nav-item").addClass('open');
        }

    });
    var lastelementactive = $(".page-sidebar-menu .activeItems").last();
    if (lastelementactive.length) {
        lastelementactive.addClass('active current-active-item');
    }

    const psSidebar = new PerfectScrollbar('.page-sidebar-wrapper .page-sidebar .page-sidebar-menu', {
        suppressScrollX: true,
    });
    const psContent = new PerfectScrollbar('.page-content', {
        
    });

    $('.slickslider').slick({
        dots: true,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: false,
        autoplaySpeed: 5000,
        rtl: true,
        arrows: false,
    });

    $('.slickslider1').slick({
        dots: true,
        speed: 500,
        slidesToShow: 4,
        slidesToScroll: 4,
        autoplay: true,
        autoplaySpeed: 7000,
        rtl: true,
        arrows: false,
       
    });
    $('.slickslider-top').slick({
        dots: true,
        speed: 500,
        slidesToShow: 4,
        slidesToScroll: 4,
        autoplay: true,
        autoplaySpeed: 7000,
        rtl: true,
        arrows: false,
       
    });
}
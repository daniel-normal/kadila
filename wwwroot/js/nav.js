$(window).on('scroll',function(){
    if($(window).scrollTop()){
        $('nav').addClass('black');
    }
    else{
        $('nav').removeClass('black');
    }
})
$(document).ready(function(){
    $('.menu h4').click(function(){
        $("nav ul").toggleClass("active")
    })
})

$(document).ready(function () {
    $('.mobile-nav-toggle').click(function () {
        $('nav ul').toggleClass('active');
    });
});
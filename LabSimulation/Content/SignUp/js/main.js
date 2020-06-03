
$(function(){
    $("#form-total").steps({
        headerTag: "h2",
        bodyTag: "section",
        transitionEffect: "fade",
        enableAllSteps: true,
        stepsOrientation: "vertical",
        autoFocus: true,
        transitionEffectSpeed: 500,
        titleTemplate : '<div class="title">#title#</div>',
        labels: {
            previous: '<input type="submit" value="I have account" class="site-btn-s font-weight-bold" onclick="LoginPage()" />',
            next : '<i class="zmdi zmdi-arrow-right"></i>',
            finish : '',
            current : ''
        },
    })
});

function LoginPage(){
    window.location.href="SignIn";
}
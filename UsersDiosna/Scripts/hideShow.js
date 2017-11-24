$(function () {
    var initDone = false;
    if (initDone == false) {
        hideShowDosing();
        hideShowCleaning();
        initDone = true;
    }
});
function hideShowBatches() {
    if ($(".batch").is(":visible")) {
        $(".batch").hide();
    } else {
        $(".batch").show();
    }
}
function hideShowDosing() {
    if ($(".dosing").is(":visible")) {
        $(".dosing").hide();
    } else {
        $(".dosing").show();
    }
}
function hideShowCleaning() {    
    if ($(".cleaning").is(":visible")) {
        $(".cleaning").hide();
    } else {
        $(".cleaning").show();
    }
}
var bMenuIsHide;
function menuHide() {
    if (bMenuIsHide == true) {
        $('#container-body').css('margin-left', 150);
        $('#menuHiding').text("Hide menu");
        $('.sidenav').show();
        bMenuIsHide = false;
        $('#instruction').show();
        $('#top_menu').css('left', $('.sidenav').width() + 20);
        $('#graph_content').css('left', $('.sidenav').width() + 20);
    } else {
        $('#container-body').css('margin-left', 0);
        $('#menuHiding').text("Show menu");
        $('.sidenav').hide();
        bMenuIsHide = true;
        $('#instruction').hide();
        $('#top_menu').css('left', 10);
        $('#graph_content').css('left', 10);
        $(window).css('width', windowWidth() - 1);
    }
    init();
}
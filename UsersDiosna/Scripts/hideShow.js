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

var bMenuIsHide = false;
function menuHide() {
    bMenuIsHide = true;
    $('#container-body').css('margin-left', 0);
    $('#menuShow').text("Show menu");
    $('#menuHide').text("");
    $('.sidenav').hide();
    $('#top_menu').css('left', 10);
    $('#graph_content').css('left', 10);
    $(window).css('width', windowWidth() - 1);
    init();
}
function menuShow() {
    bMenuIsHide = false;
    $('#container-body').css('margin-left', 150);
    $('#menuHide').text("Hide menu");
    $('#menuShow').text("");
    $('.sidenav').show();
    $('#top_menu').css('left', $('.sidenav').width() + 20);
    $('#graph_content').css('left', $('.sidenav').width() + 20);
    init();
}
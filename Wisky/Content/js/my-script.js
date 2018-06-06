$(document).ajaxStart(function () {
    $(".process").fadeIn();
});
$(document).ajaxComplete(function () {
    $(".process").fadeOut();
});
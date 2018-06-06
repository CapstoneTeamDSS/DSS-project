//$('#form-submit').submit(function() id of form
//$('#form-submit').submit(function () {
function visitAd() {
    
    var loginId = document.getElementsByName("loginId")[0].value;
    var viewAdId = document.getElementsByName("viewAdId")[0].value;
    var isReact = true;
    send2Ctrler(loginId, viewAdId, isReact, "NULL");
}
function clickThrough() {
    var loginId = document.getElementsByName("loginId")[0].value;
    var viewAdId = document.getElementsByName("viewAdId")[0].value;
    var isReact = false;
    send2Ctrler(loginId, viewAdId, isReact, "NULL");
}

function getOffer() {
    var loginId = document.getElementsByName("loginId")[0].value;
    var viewAdId = document.getElementsByName("viewAdId")[0].value;
    var isReact = true;
    var email = document.getElementsByName("offer-email")[0].value;
    send2Ctrler(loginId, viewAdId, isReact, email);
}
function answerSurvey(answer) {
    var loginId = document.getElementsByName("loginId")[0].value;
    var viewAdId = document.getElementsByName("viewAdId")[0].value;
    var isReact = true;
    send2Ctrler(loginId, viewAdId, isReact, answer);
}
function send2Ctrler(loginId, viewAdId, isReact, data) {
    $.ajax({
        async: false,
        url: "ReceiveAdResponse",
        data: { 'loginId': loginId, 'viewAdId': viewAdId, 'isReact': isReact, 'data': data},
        type: "POST",
        success: function (result) {
        },
        error: function (error) {
            alert("submit false 2");
        }
    });
}
function PnotifySuccess(text) {
    new PNotify({
        title: "Well done",
        type: "info",
        text: text,
        styling: 'bootstrap3',
        delay: '4000'
    });
}
function PnotifyFail(text) {
    new PNotify({
        title: "We are sorry",
        type: "danger",
        text: text,
        styling: 'bootstrap3',
        delay: '4000'
    });
}
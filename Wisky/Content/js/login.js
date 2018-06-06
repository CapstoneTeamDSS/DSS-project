$("document").ready(function () {
    // Initialize the SDK upon load
    FB.init({
        appId: '408068666020634', // App ID
        channelUrl: '//' + window.location.hostname + '/channel', // Path to your Channel File
        scope: 'id,name,gender,user_birthday,email', // This to get the user details back from Facebook
        status: true, // check login status
        cookie: true, // enable cookies to allow the server to access the session
        xfbml: true // parse XFBML
    });
    // listen for and handle auth.statusChange events
    FB.Event.subscribe('auth.statusChange', OnLogin);
});

function OnLogin(response) {
    if (response.authResponse) {
        FB.api('/me?fields=id,name,gender,email,birthday', LoadValues);
    }
}

//This usingMethod will load the values to the labels
function LoadValues(me) {
    if (me.name) {
        document.forms["social-login-form"]['displayname'].innerHTML = me.name;
        document.forms["social-login-form"]['FBId'].innerHTML = me.id;
        document.forms["social-login-form"]['DisplayEmail'].innerHTML = me.email;
        document.forms["social-login-form"]['Gender'].innerHTML = me.gender;
        document.forms["social-login-form"]['DOB'].innerHTML = me.birthday;
    }
}
var statusLogin;
var socialId;


function statusChangeCallback(response) {
    console.log('statusChangeCallback');
}



window.fbAsyncInit = function () {
    FB.init({
        appId: '386696444872449',
        cookie: true,  // enable cookies to allow the server to access 
        // the session
        xfbml: true,  // parse social plugins on this page
        version: 'v2.3' // use version 2.2
    });

    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
        statusLogin = response.status;
        socialId = response.authResponse.userID;
    });

};

$(document).ready(function () {
    $('[data-role=facebook-login-button]').on('click', function () {
        if (statusLogin == "connected") {
            //alert("connected");
            document.forms["social-login-form"]['SocialId'].value = socialId;
            document.getElementById("facebookSubmit").click();
        } else if (statusLogin == "not_authorized") {
            FB.login(function (response) {
                if (response.status == "connected") {
                    document.forms["social-login-form"]['SocialId'].value = response.authResponse.userID;
                    document.getElementById("facebookSubmit").click();
                }
            }, { scope: 'public_profile,email' });
        } else {
            //alert(" not login");
            FB.login(function (response) {
                if (response.status == "connected") {
                    document.forms["social-login-form"]['SocialId'].value = response.authResponse.userID;
                    document.getElementById("facebookSubmit").click();
                }
            }, { scope: 'public_profile,email' });
        }
    });
});





// Load the SDK asynchronously
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));



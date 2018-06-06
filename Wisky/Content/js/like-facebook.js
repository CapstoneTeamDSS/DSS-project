function likeFacebook() {
    document.getElementById("fbContent").style.visibility = "visible";

}

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "https://connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v2.3&appId=903435729712696";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));
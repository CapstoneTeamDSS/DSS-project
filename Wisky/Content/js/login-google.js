
$(document).ready(function () {
    $('[data-role=google-login-button]').on('click', function () {
        LoginGoogle();
    });
});

function LoginGoogle() {
    gapi.auth.signIn({
        'clientid': '712835632050-nnkilml26revu8i5ke1ps52ndlslqc6p.apps.googleusercontent.com',
        'cookiepolicy': 'single_host_origin',
        'scope': 'https://www.googleapis.com/auth/plus.login https://www.googleapis.com/auth/plus.profile.emails.read',
        userId: 'me',
        'callback': function (a) {
            gapi.client.load('plus', 'v1', function () {
                gapi.client.plus.people.get({ userId: 'me', scope: 'email' }).execute(LoadGoogleValues);
            });
        }
    });
}

function LoadGoogleValues(resq) {
    document.forms["social-login-form"]['SocialId'].value = resq.id;
    document.forms["social-login-form"]['SocialType'].value = 1;
    document.getElementById("googleSubmit").click();
}















//function InitGoogle() {
//    CheckGoogleStatus();
//}
//function CheckGoogleStatus() {
//    gapi.auth.checkSessionState({ 'clientid': '712835632050-nnkilml26revu8i5ke1ps52ndlslqc6p.apps.googleusercontent.com' },
//        function (r) { // true => logout ; false => login
//            if (!r) {
//                gapi.client.load('plus', 'v1', function () {
//                    var request = gapi.client.plus.people.get({
//                        'userId': 'me'
//                    });
//                    request.execute(function (resp) {
//                        console.log('Retrieved profile for:' + resp.totalItems);
//                        alert(resp.totalItems);
//                    });
//                });
//            }
//        });
//} function GoogleLogin() {
//    gapi.auth.signIn({
//        'clientid': '712835632050-nnkilml26revu8i5ke1ps52ndlslqc6p.apps.googleusercontent.com',
//        'cookiepolicy': 'single_host_origin',
//        'scope': 'https://www.googleapis.com/auth/plus.login https://www.googleapis.com/auth/plus.profile.emails.read',
//        userId: 'me',
//        'callback': function (a) {
//            gapi.client.load('plus', 'v1', function () {
//                gapi.client.plus.people.get({ userId: 'me', scope: 'email' }).execute(LoadGoogleValues);
//            });
//        }
//    }); document.forms["social-login-form"]['SocialType'].value = 0;
//    //document.getElementById("googleSubmit").click();
//} function OnGoogleLogin(authResult) {
//    if (authResult['status']['signed_in']) {
//        gapi.client.load('plus', 'v1', function () {
//            gapi.client.plus.people.get({ userId: 'me', scope: 'email' }).execute(LoadGoogleValues);
//        });
//    } else { }
//} function LoadGoogleValues(r) {
//    if (r) {
//        //if (window['lock']) { return; } window['lock'] = true;
//        document.forms["social-login-form"]['SocialId'].value = r.id;
//        document.forms["social-login-form"]['SocialType'].value = 1;
//        //document.getElementById("googleSubmit").click();

//        //if (r.emails != undefined) {
//        //    var primaryEmail;
//        //    for (var i = 0; i < r.emails.length; i++) {
//        //        if (r.emails[i].type === 'account') {
//        //            primaryEmail = r.emails[i].value;
//        //            break;
//        //        }
//        //    }
//        //    document.forms["social-login-form"]['Email'].value = primaryEmail;
//        //} else {
//        //    document.forms["social-login-form"]['Email'].value = '';
//        //}
//        //document.forms["social-login-form"]['Gender'].value = r.gender;
//        //document.forms["social-login-form"]['DOB'].value = '';
//        //document.forms["social-login-form"]['SocialType'].value = 'Googleplus';

//    }

//}
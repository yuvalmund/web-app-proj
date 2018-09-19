
 window.fbAsyncInit = function() {
        FB.init({
            appId: '539023143185296',
            cookie: true,
            xfbml: true,
            version: 'v3.1'
        });

     FB.AppEvents.logPageView();
  };

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

function share(){
    FB.ui({
        method: 'share',
        display: 'popup'
    }, function (response) { });
}
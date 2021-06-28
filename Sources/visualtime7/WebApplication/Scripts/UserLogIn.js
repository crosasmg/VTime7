//Fields
var key = "6LfochoUAAAAACJmzzTyasPxn2qNePslX4oJMRFS";
//Método de validación
function createCaptcha(btnLogin) {
    captchaContainer = grecaptcha.render('captcha_container', {
        'sitekey': key,
        'callback': function (response) {
            btnLogin.SetEnabled(true);
        }
    });
}
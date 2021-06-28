<%@ Page Language="VB" AutoEventWireup="false" CodeFile="logIn.aspx.vb" Inherits="fasi_security_logIn" %>

<!DOCTYPE html>
<html id="app" lang="">

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />

    <title data-i18n="app.title">VisualTIME | Inicio sesión</title>

    <link rel="stylesheet" href="/fasi/assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="/fasi/assets/font-awesome/css/font-awesome.css">
    <link rel="stylesheet" href="/fasi/assets/css/animate.css">
    <link rel="stylesheet" href="/fasi/assets/css/toastr.min.css" />
    <link rel="stylesheet" href="/fasi/assets/css/sweetalert.css" />
    <link rel="stylesheet" href="/fasi/assets/css/awesome-bootstrap-checkbox.css" />
    <link rel="stylesheet" href="/fasi/assets/css/styles.css">
    <link rel="stylesheet" href="/fasi/assets/css/ladda-themeless.min.css">

  
</head>

<body class="gray-bg">
    <div class="middle-box text-center loginscreen animated fadeInDown">
        <div>
            <div>
                <h1 class="logo-name">VT</h1>
            </div>
            <h3 data-i18n="app.header.welcome">Bienvenido a VT</h3>
            <div class="form m-t" role="form">
                <form id="loginForm">
                    <div class="form-group">
                        <input type="email" id="UserName" name="UserName" autocomplete="off" class="form-control" placeholder="Username" required="">
                    </div>
                    <div class="form-group">
                        <input type="password" id="Password" name="Password" autocomplete="off" class="form-control" data-i18n="[placeholder]app.form.password" required="">
                    </div>
                    <div id="ContinerCompanyId" class="form-group">
                        <select id="CompanyId" class="form-control" data-i18n="[placeholder]app.form.company">
                        </select>
                    </div>
                    <div id="ContinergRecaptcha" class="form-group">
                        <div id="captcha_container"></div>
                    </div>
                    <div id="divRememberMe" class="form-group">
                        <div class="checkbox checkbox-inline">
                            <input id="RememberMe" type="checkbox">
                            <label data-i18n="app.form.remain" for="RememberMe"></label>
                        </div>
                    </div>
                    <button id="btnLogIn" class="ladda-button btn btn-primary block full-width m-b" data-style='expand-right'><span class='ladda-label' data-i18n="app.form.btnSave">Iniciar</span><span class='ladda-spinner'></span></button>
                    
                    <a id="aPasswordRecovery" style="display:block; text-align:left !important;"  href="javascript:securitySupport.PasswordRecovery()"><small data-i18n="app.form.forgetpassword"></small></a>
                    <br />
                    <p id="aNothaveaccount" class="text-muted text-left"><small data-i18n="app.form.nothaveaccount"></small></p>
                    <a id="btnregister" class="btn btn-sm btn-white btn-block" href="<%= System.Configuration.ConfigurationSettings.AppSettings("FASI.HTML5.Security.UserRegistrationPage") %>" data-i18n="app.form.btnregister"></a>
                    <input type="hidden" id="UserNameHiddenField" name="UserNameHiddenField" value="<%= UserNameField%>" />
                </form>
            </div>
            <p class="m-t"><small>VisualTIME &copy; 2018</small> </p>
        </div>
    </div>

    <!-- Mainly scripts -->
    <script src="/fasi/assets/js/jquery-1.11.3.min.js"></script>
    <script src="/fasi/assets/js/bootstrap.min.js"></script>
    <script src="/fasi/assets/js/i18next.min.js"></script>
    <script src="/fasi/assets/js/toastr.min.js"></script>
    <script src="/fasi/assets/js/sweetalert.min.js"></script>
    <script src="/fasi/assets/js/spin.min.js"></script>
    <script src="/fasi/assets/js/ladda.min.js"></script>
    <script src="/fasi/assets/js/ladda.jquery.min.js"></script>
    <script src="/fasi/assets/js/jquery.validate.min.js"></script>
    <script src="/fasi/assets/js/jquery.cookie.js"></script>
    <script src="/fasi/app/js/constants.js?rel=1526062220941"></script>
    <script src="/fasi/app/js/core.js?rel=1526062220941"></script>
    <script src="/fasi/app/js/notification.js"></script>
    <script src="/fasi/app/js/general.js?rel=1526062220941"></script>
    <script src="/fasi/app/js/Security.js?rel=1526062220941"></script>
     <script type="text/javascript">
       var onloadCallback = function () {
           
      };
    </script>
    <script src='https://www.google.com/recaptcha/api.js?hl=es&onload=onloadCallback&render=explicit'></script>
    <script src="/fasi/security/LogIn.js?rel=1526062220941"></script>
</body>
</html>
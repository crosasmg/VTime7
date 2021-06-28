<%@ Page Language="C#" AutoEventWireup="true" CodeFile="autologin.aspx.cs" Inherits="fasi_dli_forms_autologin" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Auto login</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css">
   
    <link href="/fasi/assets/css/jquery-ui.min.css" rel="stylesheet" />
   
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.15.1/jquery.validate.min.js"></script>
    <script src="/fasi/assets/js/jquery-ui.min.js"></script>
   
</head>
<body>
    <div class="wrapper" style="margin-left:350px;margin-right:350px">
        <div class="form-signin">
            <h2 class="form-signin-heading">Por favor iniciar sección</h2>
            <input type="text" class="form-control" id="txtUsername" placeholder="Usuario" required="" autofocus="" />
            <input type="password" class="form-control" id="txtPassword" placeholder="Contraseña" required="" />
            <button id="btnStart" class="btn btn-lg btn-primary btn-block">Iniciar</button>
        </div>
    </div>
    <script src="/fasi/dli/forms/autologin.js"></script>
</body>
</html>
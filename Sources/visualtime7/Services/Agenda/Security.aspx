<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Security.aspx.vb" Inherits="VTimeNet_visualtime_Security" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Content/css/bootstrap-theme.min.css" rel="stylesheet">
    <link href="Content/css/bootstrap.min.css" rel="stylesheet">
    <style type="text/css">        
        h4
        {
            font-weight: bold;
        }

        .col-md-6 
        {
            margin-bottom: 35px;
        }
        .form-group {
            margin-bottom: 7px;
        }

        footer 
        {
            margin-top: 40px;
        }
    </style>
</head>
<body>
    <div class="container">
        <h2>Asignación de Usuarios y Passwords para las conexiones a Base de Datos</h2>
        <hr />

        <div class="col-md-12">
            <div class="oracleContainer">
                <h3>Conexiones Oracle</h3>
            </div>
        </div>

        <div class="col-md-12">
            <div class="sqlServerContainer">
                <h3>Conexiones SQLServer</h3>
            </div>
        </div>
		<div class="col-md-12">
            <div class="otrasContainer">
                <h3>Otras conexiones</h3>
            </div>
        </div>

        <div class="col-md-12 text-center">
            <hr />
            <input type="button" name="name" class="btnDecrypt btn btn-danger" value="Desproteger" />
            <input id="EncryptAll" type="button" name="name" class="btn btn-success" value="Actualizar todas" />
        </div>

        <div class="col-md-12">
            <footer>
                <p>Copyright &copy; 2015 EASE Global Insurance Technology, Inc. Todos los derechos reservados</p>
            </footer>
        </div>
		<div class="loadingDiv" style="position: fixed; width: 100%; height: 100%; top:0; left:0; background: black; opacity: 0.3; z-index: 100000;">
            <img style="top:45%; left:50%; width:64px; position: fixed;" src="Content/images/loader-green.gif" alt="loader"/>
        </div>
    </div>
    <script src="Scripts/jquery.min.js"></script>
	<script src="Scripts/json3.min.js"></script>
    <script src="Scripts/security.js"></script>
</body>
</html>
var AutentiaIntegracion = new function () {

    var Autentia = null;

    //$.getScript("http://200.0.156.196/libMB/json2.js");
    //$.getScript("http://200.0.156.196/libMB/jsrasign/ext/jsbn.js");
    //$.getScript("http://200.0.156.196/libMB/jsrasign/ext/jsbn2.js");
    //$.getScript("http://200.0.156.196/libMB/jsrasign/ext/rsa.js");
    //$.getScript("http://200.0.156.196/libMB/jsrasign/ext/rsa2.js");
    //$.getScript("http://200.0.156.196/libMB/jsrasign/ext/base64.js");
    //$.getScript("http://yui.yahooapis.com/2.9.0/build/yahoo/yahoo-min.js");
    //$.getScript("http://crypto-js.googlecode.com/svn/tags/3.1.2/build/components/core.js");
    //$.getScript("http://crypto-js.googlecode.com/svn/tags/3.1.2/build/components/md5.js");
    //$.getScript("http://crypto-js.googlecode.com/svn/tags/3.1.2/build/components/sha1.js");
    //$.getScript("http://crypto-js.googlecode.com/svn/tags/3.1.2/build/components/sha256.js");
    //$.getScript("http://crypto-js.googlecode.com/svn/tags/3.1.2/build/components/ripemd160.js");
    //$.getScript("http://crypto-js.googlecode.com/svn/tags/3.1.2/build/components/x64-core.js");
    //$.getScript("http://crypto-js.googlecode.com/svn/tags/3.1.2/build/components/sha512.js");
    //$.getScript("http://200.0.156.196/libMB/jsrasign/rsapem-1.1.min.js");
    //$.getScript("http://200.0.156.196/libMB/jsrasign/rsasign-1.2.min.js");
    //$.getScript("http://200.0.156.196/libMB/jsrasign/asn1hex-1.1.min.js");
    //$.getScript("http://200.0.156.196/libMB/jsrasign/x509-1.1.min.js");
    //$.getScript("http://200.0.156.196/libMB/jsrasign/crypto-1.1.min.js");   
    //$.getScript("http://200.0.156.196/libMB/jquery-2.1.4.min.js");
    //$.getScript("http://200.0.156.196/libMB/plugin.autentia.js");
    
    // Obtiene el token generado a ser mandado a autentia.
    function generaToken() {
        return $.ajax({
            url: "/Integrations/Autentia.aspx/GToken",
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            async: false,
            error: function (err) {
                console.log("error generating token:" + err);
            }
        }).responseJSON.d;
    }

    // Valida el token que devuelve autentia.
    function validaToken(token) {
        return $.ajax({
            data: JSON.stringify({ "token": token }),
            async: false,
            url: "/Integrations/Autentia.aspx/VToken",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: 'POST',
            error: function (err) {
                console.log("error validating token:" + err);
            }
        }).responseJSON.d;
    }

    this.Verificar = function(rut, auditoria, errorNumber, errorDesc) {
        var token = generaToken();
		var audit = {NroAudit:'',Erc:'',ErcDesc:''};

        try {
            // Asignación de parámetros de entrada
            var entradas = {
                pRut: rut
            };
            // Definición de parámetros de salida
            var salidas = ["Erc", "NroAudit", "ErcDesc"];
            // Asignación a variable focoAutentia, la cual puede ser
            // true (siempre mantiene el foco la ventana Autentia) o false (puede perder el foco la ventana Autentia)
            var focoAutentia = true;
            //Llamada de transacción
            if (Autentia == null) {
                Autentia = new plgAutentiaJS();
            }

            Autentia.Transaccion2('../INMOTION/verificadatos', entradas, salidas, focoAutentia, token, function (resultado) {
                // Se obtienen los valores de retorno de la transacción
                if (validaToken(resultado.token)) {
					audit.NroAudit = resultado.ParamsGet.NroAudit;
					audit.Erc = resultado.ParamsGet.Erc;
					audit.ErcDesc = resultado.ParamsGet.ErcDesc;
                    console.log('Erc : ' + resultado.ParamsGet.Erc + ' - Nro Auditoria : ' + resultado.ParamsGet.NroAudit + ' - ErcDesc : ' + resultado.ParamsGet.ErcDesc);
                    ASPxClientControl.GetControlCollection().GetByName(auditoria).SetValue(audit.NroAudit);
                    ASPxClientControl.GetControlCollection().GetByName(errorNumber).SetValue(audit.Erc);
                    ASPxClientControl.GetControlCollection().GetByName(errorDesc).SetValue(audit.ErcDesc);
                }
            });
        } catch (ex) {
            alert(ex.message);
        }
    }

    this.Iniciar = function(rut) {
        var token = generaToken();
        Autentia.IniciarSesionLogin(rut, token, function (response) {
            if (validaToken(response.token)) {
                if (response.ParamsGet.hasOwnProperty('LoginResult')) {
                    alert(response.ParamsGet.LoginResult);
                }
                if (response.ParamsGet.LoginResult == 0) {
                    alert("Inicio Sesión");
                }
            } else {
                alert('Token invalido...');
            };
        })
    }

    this.Terminar = function () {
        var token = generaToken();
        Autentia.CerrarSesion(token);
    }

}

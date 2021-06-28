var AutentiaIntegracion = new function () {
    this.Verificar = function(rut, auditoria, errorNumber, errorDesc) {
		var transaction_path = "../INMOTION/verificadatos";
		var autentia = null, citrix = false, modalidad = "indefinida";
		
		try {
			autentia = new ActiveXObject("AutentiaRemoteClientX.Autentia");
			citrix = true;
		} catch (e) {
			console.log("Falló la creacion del objeto AutentiaRemoteClientX");
		}
		
		if (citrix === false) {
			try {
				autentia = new ActiveXObject("Autentia32.Autentia");
			}
			catch (e) {
				console.log("Falló la creacion del objeto Autentia32.Autentia");
			}
		}
		
		// alert("autentia: " + autentia + ", citrix: " + citrix);
		if (autentia === null) {
			ProcesarRespuesta(9999, "Autentia no disponible, revisar la instalacion.", "n/a", "", modalidad);
			return;
		}
		
		if (citrix === true) {
			modalidad = "citrix";
			// autentia modo citrix
			var inputs = {"pRut": rut, "DV": ""};
			var outputs = ["Erc", "ErcDesc", "NroAudit", "oNombres", "oSexo", "oFchNac"];
			var resultJson = autentia.Transaccion(transaction_path, JSON.stringify(inputs), JSON.stringify(outputs));
			var result = JSON.parse(resultJson);
			
			if (isError(result)) {
				ProcesarRespuesta(9990, resultJson, "n/a", "", modalidad, auditoria, errorNumber, errorDesc);
			} else {
				ProcesarRespuesta(result.Erc, result.ErcDesc, result.NroAudit, result.URI, modalidad, auditoria, errorNumber, errorDesc);
			}
		} else {
			modalidad = "escritorio";
			// autentia modo escritorio
			var Params = {};
			Params.pRut = rut;
			Params.DV = "";
			Params.Erc = "";
			Params.ErcDesc = "";
			Params.NroAudit = "";
			Params.URI = "";
			Params.Email = "";
			var ErcMetodo = 200;
			
			try {
				ErcMetodo = autentia.Transaccion(transaction_path, Params);
				
				if (ErcMetodo != 0)
					ProcesarRespuesta(9999, "Error: " + ErcMetodo, "n/a", "", modalidad, auditoria, errorNumber, errorDesc);
				else {
					ProcesarRespuesta(Params.Erc, Params.ErcDesc, Params.NroAudit, Params.URI, modalidad, auditoria, errorNumber, errorDesc);
				}				
			} catch (e) {
				ProcesarRespuesta(9999, "Excepcion javascript: " + e.message, "n/a", "", modalidad, auditoria, errorNumber, errorDesc);
			}
		}
    }
	
	function ProcesarRespuesta(Erc, ErcDesc, NroAudit, URI, Modalidad, auditoria, errorNumber, errorDesc) {
		var resp = Erc;
		console.log('Erc: ' + Erc + ' - Nro Auditoria: ' + NroAudit + ' - ErcDesc: ' + ErcDesc + ' - Modalidad: ' + Modalidad);	
		ASPxClientControl.GetControlCollection().GetByName(auditoria).SetValue(NroAudit);
		ASPxClientControl.GetControlCollection().GetByName(errorNumber).SetValue(Erc);
		ASPxClientControl.GetControlCollection().GetByName(errorDesc).SetValue(ErcDesc);
		
		if (resp == 0) {
			console.log("La validacion fue correcta, en estos momentos se está creando la Clave...");
		} else if (resp == 4) {
			console.log("La validacion fue Rechazada");
		} else if (resp != 4 || resp != 0) {
			var msgResp = "La validacion fue Cancelada: ";
			if (typeof resp == "undefined") {
				msgResp = msgResp + "(" + resp + ") Compruebe huellero";
			} else {
				msgResp = msgResp + ErcDesc;
			}
			console.log(msgResp);
		}
	}
	
	function isError(obj) {
		return (typeof obj["$ERROR"]) == "string";
	}
}

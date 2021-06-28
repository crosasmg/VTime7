//-------------------------------------------------------------------------------------------
//+ Constantes.js:  Se definen las constantes utilizadas en el proyecto
//-------------------------------------------------------------------------------------------


//- Se define la lista enumerada para diferenciar el tipo de compa��a
function eCompanyType(){
	this.cstrInsurance = "1"
	this.cstrReinsurance = "2"
	this.cstrBrokerOrBrokerageFirm = "3"
	this.cstrInsuranceReinsurance = "4"
}

//- Se define la lista enumerada para diferenciar los tipos de datos para el valor NULL
function eTypeNulls(){
    this.dtmNull = ""				    //+ Date
    this.strNull = ""          		    //+ String
    this.numNull = -32768   			//+ N�mero
}

//-Se definen las constantes globales para el manejo de las opciones del Combo de acciones de la CA001
function ePolTransac(){
	this.clngPolicyIssue = "1"           //+Emision de Poliza
	this.clngCertifIssue = "2"			 //+Emision de Certificado
	        
	this.clngRecuperation = "3"          //+Recuperacion

	this.clngPolicyQuotation = "4"       //+Cotizacion de Poliza
	this.clngCertifQuotation = "5"       //+Cotizacion de Certificado
	        
	this.clngPolicyProposal = "6"        //+Solicitud de Poliza
	this.clngCertifProposal = "7"        //+Solicitud de Certificado
	        
	this.clngPolicyQuery = "8"           //+Consulta de Poliza"
	this.clngCertifQuery = "9"           //+Consulta de Certificado
	this.clngQuotationQuery = "10"       //+Consulta de Cotizacion
	this.clngProposalQuery = "11"        //+Consulta de Solicitud
	        
	this.clngPolicyAmendment = "12"      //+Modificacion Normal de Poliza
	this.clngTempPolicyAmendment = "13"  //+Modificacion Temporal de Poliza
	this.clngCertifAmendment = "14"      //+Modificacion de Certificado
	this.clngTempCertifAmendment = "15"  //+Modificacion Temporal de Certificados
	        
	this.clngQuotationConvertion = "16"  //+Conversion de Cotizacion a Poliza
	this.clngProposalConvertion = "17"   //+Conversion de Solicitud a Poliza"
	        
	this.clngPolicyReissue = "18"        //+Re-emision de Poliza
	this.clngCertifReissue = "19"        //+Re-emision de Certificado
	        
	this.clngReprint = "20"              //+Re-impresion
	        
	this.clngDeclarations = "21"         //+Declaraciones
	        
	this.clngCoverNote = "22"            //+Nota de Cobertura
	this.clngPropQuotConvertion = "23"   //+Conversion de Solicitud a Cotizaci�n"
	
	this.clngPolicyQuotAmendent = "24"	  //+Cotizaci�n de Modificaci�n de p�liza
    this.clngCertifQuotAmendent = "25"	  //+Cotizaci�n de Modificaci�n de certificado
    this.clngPolicyPropAmendent = "26"	  //+Propuesta de Modificaci�n de p�liza
    this.clngCertifPropAmendent = "27"	  //+Propuesta de Modificaci�n de certificado
    
    this.clngPolicyQuotRenewal = "28"     //+Cotizaci�n de Renovaci�n de p�liza
    this.clngCertifQuotRenewal = "29"     //+Cotizaci�n de Renovaci�n de certificado
    this.clngPolicyPropRenewal = "30"     //+Propuesta de Renovaci�n de p�liza
    this.clngCertifPropRenewal = "31"     //+Propuesta de Renovaci�n de Certificado
    
    this.clngInspections = "32"		      //+Inspecciones	
            
    this.clngQuotAmendConvertion = "33"         //+Conversi�n Cotizacion de Modificaci�n a modificaci�n
    this.clngPropAmendConvertion = "34"         //+Conversi�n Propuesta de Modificaci�n a modificaci�n
    this.clngQuotRenewalConvertion = "35"       //+Conversi�n Cotizaci�n de Renovaci�n a p�liza
    this.clngPropRenewalConvertion = "36"       //+Conversi�n Propuesta de Renovaci�n a p�liza
    this.clngQuotPropAmendentConvertion = "37"  //+Conversi�n Cotizacion de Modificaci�n a Propuesta de Modificaci�n 
    this.clngQuotPropRenewalConvertion = "38"   //+Conversi�n Cotizacion de Renovaci�n a Propuesta de Renovaci�n

    this.clngQuotAmendentQuery = "39"           //+Consulta de Cotizaci�n de Modificaci�n
    this.clngPropAmendentQuery = "40"	        //+Consulta de Propuesta de Modificaci�n
    this.clngQuotRenewalQuery = "41"            //+Consulta de Cotizaci�n de Renovaci�n
    this.clngPropRenewalQuery = "42"            //+Consulta de Propuesta de Renovaci�n	
    this.clngDuplPolicy = "45"                    //+Duplicar Poliza
    this.clngTransHolder = "46"                    //+Traspaso de asegurado
}

//- Tipo para el manejo de las acciones del men�
function TypeActions(){
    this.clngMenuNavegation = "200"          //+  Men� de Navegaci�n
    this.clngActionMainMenu = "201"          //+  Men� principal
    this.clngActionErrorMenu = "202"         //+  Men� de Errores
    this.clngactionpreviouswindow = "203"    //+  Ventana anterior
    this.clngActionGo = "204"                //+  Ir
    this.clngActionBye = "205"               //+  Salir del sistema
    this.clngActionByeError = "206"          //+  Salir del Sistema de Errores
    this.clngActionGenQue = "207"             //+  Ir a la consulta general
        
    this.clngMenuActions = "300"             //+  Men� de Acciones
    this.clngActionadd = "301"               //+  Registrar
    this.clngActionUpdate = "302"            //+  Actualizar
    this.clngActioncut = "303"               //+  Cortar
    this.clngActionInput = "304"             //+  Entrar
    this.clngActionModify = "305"            //+  Modificar
    this.clngActionDuplicate = "306"         //+  Duplicar
    this.clngActionCutTable = "307"           //+  Cortar tabla
    this.clngActionCopyTable = "308"          //+  Duplicar tabla
    this.clngActionCurrency = "309"           //+  Moneda
    this.clngActionDuplicateProduct = "310"   //+  Duplicar Producto
        
    this.clngAcceptdataAccept = "390"        //+  Aceptar
    this.clngAcceptdataCancel = "391"        //+  Cancelar
    this.clngAcceptdatafinish = "392"        //+  Finalizar
    this.clngAcceptdataRefresh = "393"       //+  Ignorar Cambios
               
    this.clngMenuInquiry = "400"             //+  Men� de Consulta
    this.clngActionQuery = "401"             //+  Consulta
    this.clngActionCondition = "402"         //+  Condici�n
    this.clngActionReview = "403"            //+  Revisar
        
    this.clngActionFirst = "490"             //+  Primero
    this.clngActionPrevious = "491"          //+  Anteriores
    this.clngActionNext = "492"              //+  Pr�ximos
    this.clngActionLast = "493"              //+  Ultimo
        
    this.clngMenuHelp = "600"                //+  Men� de Ayuda
    this.clngActionHelp = "601"              //+  Ayuda
    this.clngGlobalsHelp = "602"             //+  �ltimas globales...
    this.clngActionAbout = "603"             //+  Acerca de...
       
    this.clngMenuDelimiter = "99"             //+  Delimitador de Items de Men�
        
    this.clngActionLinkSpecial = "700"       //+  Usado para los enlaces especiales
}

//- Se definen el tipo enumerado para el tipo de numeraci�n
function TypeNumeratorPol_Rec(){
    this.cstrSysNumeGeneral = "1"         //+ General
    this.cstrSysNumeBranch = "2"          //+ Ramo
    this.cstrSysNumeBranchProduct = "3"   //+ Ramo/Producto
}

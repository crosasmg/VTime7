Option Strict Off
Option Explicit On
Module Constantes
	'%-------------------------------------------------------%'
	'% $Workfile:: Constantes.bas                           $%'
	'% $Author:: Nvaplat15                                  $%'
	'% $Date:: 8/04/04 18.16                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'- Tipo enumerado para el tipo de acci�n que se ejecuta sobre la p�liza (Table221)
	Public Enum PolTransac
		clngPolicyIssue = 1 'Emision de Poliza
		clngCertifIssue = 2 'Emision de Certificado
		clngRecuperation = 3 'Recuperacion
		clngPolicyQuotation = 4 'Cotizacion de Poliza
		clngCertifQuotation = 5 'Cotizacion de Certificado
		clngPolicyProposal = 6 'Propuesta de Poliza
		clngCertifProposal = 7 'Propuesta de Certificado
		clngPolicyQuery = 8 'Consulta de Poliza
		clngCertifQuery = 9 'Consulta de Certificado
		clngQuotationQuery = 10 'Consulta de Cotizacion
		clngProposalQuery = 11 'Consulta de Solicitud
		clngPolicyAmendment = 12 'Modificacion Normal de Poliza
		clngTempPolicyAmendment = 13 'Modificacion Temporal de Poliza
		clngCertifAmendment = 14 'Modificacion de Certificado
		clngTempCertifAmendment = 15 'Modificacion Temporal de Certificados
		clngQuotationConvertion = 16 'Conversion de Cotizacion a Poliza
		clngProposalConvertion = 17 'Conversion de Propuesta a Poliza
		clngPolicyReissue = 18 'Re-emision de Poliza
		clngCertifReissue = 19 'Re-emision de Certificado
		clngReprint = 20 'Re-impresion
		clngdeclarations = 21 'Declaraciones
		clngCoverNote = 22 'Nota de Cobertura
		clngPropQuotConvertion = 23 'Conversion de Cotizaci�n a Propuesta
		clngPolicyQuotAmendent = 24 'Cotizaci�n de Modificaci�n de p�liza
		clngCertifQuotAmendent = 25 'Cotizaci�n de Modificaci�n de certificado
		clngPolicyPropAmendent = 26 'Propuesta de Modificaci�n de p�liza
		clngCertifPropAmendent = 27 'Propuesta de Modificaci�n de certificado
		clngPolicyQuotRenewal = 28 'Cotizaci�n de Renovaci�n de p�liza
		clngCertifQuotRenewal = 29 'Cotizaci�n de Renovaci�n de certificado
		clngPolicyPropRenewal = 30 'Propuesta de Renovaci�n de p�liza
		clngCertifPropRenewal = 31 'Propuesta de Renovaci�n de Certificado
		clngInspections = 32 'Inspecciones
		clngQuotAmendConvertion = 33 'Conversi�n Cotizacion de Modificaci�n a modificaci�n
		clngPropAmendConvertion = 34 'Conversi�n Propuesta de Modificaci�n a modificaci�n
		clngQuotRenewalConvertion = 35 'Conversi�n Cotizaci�n de Renovaci�n a p�liza
		clngPropRenewalConvertion = 36 'Conversi�n Propuesta de Renovaci�n a p�liza
		clngQuotPropAmendentConvertion = 37 'Conversi�n Cotizacion de Modificaci�n a Propuesta de Modificaci�n
		clngQuotPropRenewalConvertion = 38 'Conversi�n Cotizacion de Renovaci�n a Propuesta de Renovaci�n
		clngQuotAmendentQuery = 39 'Consulta de Cotizaci�n de Modificaci�n
		clngPropAmendentQuery = 40 'Consulta de Propuesta de Modificaci�n
		clngQuotRenewalQuery = 41 'Consulta de Cotizaci�n de Renovaci�n
		clngPropRenewalQuery = 42 'Consulta de Propuesta de Renovaci�n
		clngProprehabilitate = 43 'Modificaci�n evaluacion propuesta de rehabilitacion
		clngModPropRehabQuery = 44 'Consulta propuesta de rehabilitacion
		clngDuplPolicy = 45 'Duplicar Poliza
	End Enum
	
	'-Enumerado que contiene la transacci�n general de p�liza
	Public Enum eGenPolTransac
		PolTransac
		clngIssue = 101
		clngAmendPropQuot = 102
		clngAmend = 103
		clngQuery = 104
		clngConvert = 105
	End Enum
	
	'-Enumerado que contiene los tipos de registros de p�liza
	Public Enum ePolCertype
        cstrProposal = 1
		cstrPolicy = 2
		cstrQuotation = 3
        cstrAmendQuot = 4
        cstrRenewalQuot = 5
        cstrAmendProposal = 6
        cstrRenewalProposal = 7
        cstrSpecialProposal = 8
	End Enum
	
	'-Enumerado que indica los tipos de p�liza
	Public Enum ePoliType
		cstrIndividual = 1
        cstrColective = 2
        cstrMultiind = 3
	End Enum
	
	Public Enum TypeDefaulti
		cintYes = 1
		cintNot = 0
	End Enum
	
	'**- Enumerated of payment way
	'-Enumerado de v�a de pago
	Public Enum eWayPay
		clngPayByPAC = 1
		clngPayByTransBank = 2
		clngPayByBrief = 3
		clngPayByBulletin = 4
		clngPayByCoupon = 5
		clngPayByAFP_INP = 7
	End Enum
	
	'+Variable para manejar el indicador de contenido de las transacciones de la secuencia de p�lizas
	Public mstrContent As String
End Module







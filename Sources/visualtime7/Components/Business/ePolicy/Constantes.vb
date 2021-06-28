Option Strict Off
Option Explicit On
Module Constantes
	'%-------------------------------------------------------%'
	'% $Workfile:: Constantes.bas                           $%'
	'% $Author:: Nvaplat15                                  $%'
	'% $Date:: 8/04/04 18.16                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'- Tipo enumerado para el tipo de acción que se ejecuta sobre la póliza (Table221)
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
		clngPropQuotConvertion = 23 'Conversion de Cotización a Propuesta
		clngPolicyQuotAmendent = 24 'Cotización de Modificación de póliza
		clngCertifQuotAmendent = 25 'Cotización de Modificación de certificado
		clngPolicyPropAmendent = 26 'Propuesta de Modificación de póliza
		clngCertifPropAmendent = 27 'Propuesta de Modificación de certificado
		clngPolicyQuotRenewal = 28 'Cotización de Renovación de póliza
		clngCertifQuotRenewal = 29 'Cotización de Renovación de certificado
		clngPolicyPropRenewal = 30 'Propuesta de Renovación de póliza
		clngCertifPropRenewal = 31 'Propuesta de Renovación de Certificado
		clngInspections = 32 'Inspecciones
		clngQuotAmendConvertion = 33 'Conversión Cotizacion de Modificación a modificación
		clngPropAmendConvertion = 34 'Conversión Propuesta de Modificación a modificación
		clngQuotRenewalConvertion = 35 'Conversión Cotización de Renovación a póliza
		clngPropRenewalConvertion = 36 'Conversión Propuesta de Renovación a póliza
		clngQuotPropAmendentConvertion = 37 'Conversión Cotizacion de Modificación a Propuesta de Modificación
		clngQuotPropRenewalConvertion = 38 'Conversión Cotizacion de Renovación a Propuesta de Renovación
		clngQuotAmendentQuery = 39 'Consulta de Cotización de Modificación
		clngPropAmendentQuery = 40 'Consulta de Propuesta de Modificación
		clngQuotRenewalQuery = 41 'Consulta de Cotización de Renovación
		clngPropRenewalQuery = 42 'Consulta de Propuesta de Renovación
		clngProprehabilitate = 43 'Modificación evaluacion propuesta de rehabilitacion
		clngModPropRehabQuery = 44 'Consulta propuesta de rehabilitacion
		clngDuplPolicy = 45 'Duplicar Poliza
	End Enum
	
	'-Enumerado que contiene la transacción general de póliza
	Public Enum eGenPolTransac
		PolTransac
		clngIssue = 101
		clngAmendPropQuot = 102
		clngAmend = 103
		clngQuery = 104
		clngConvert = 105
	End Enum
	
	'-Enumerado que contiene los tipos de registros de póliza
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
	
	'-Enumerado que indica los tipos de póliza
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
	'-Enumerado de vía de pago
	Public Enum eWayPay
		clngPayByPAC = 1
		clngPayByTransBank = 2
		clngPayByBrief = 3
		clngPayByBulletin = 4
		clngPayByCoupon = 5
		clngPayByAFP_INP = 7
	End Enum
	
	'+Variable para manejar el indicador de contenido de las transacciones de la secuencia de pólizas
	Public mstrContent As String
End Module







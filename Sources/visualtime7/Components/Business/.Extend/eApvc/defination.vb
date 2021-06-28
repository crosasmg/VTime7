Option Strict Off
Option Explicit On
Module defination
	Public Enum eRmtDataDir
		rdbParamUnknown = 0
		rdbParamInput = 1
		rdbParamOutput = 2
		rdbParamInputOutput = 3
		rdbParamReturnValue = 4
	End Enum


    Public Enum eRmtDataType
        rdbEmpty = 0
        rdbBoolean = 2
        rdbChar = 3
        rdbDate = 4
        rdbDBTime = 4
        rdbNumeric = 5 'No existe equivalente en ADO.NET. Se iguala al valor para rdbDecimal
        rdbDecimal = 5
        rdbDouble = 6 'No existe equivalente en ADO.NET. Se utiliza el valor de SqlDbType.Float
        rdbImage = 7
        rdbInteger = 8
        rdbSmallInt = 16
        rdbDBTimeStamp = 19
        rdbVarchar = 22
    End Enum


	Public Enum eRmtDataAttrib
		rdbParamSigned = 16
		rdbParamNullable = 64
		rdbParamLong = 128
	End Enum
	
	Public Enum eTypeData
		etdDate = 1
		etdInteger = 2
		etdLong = 3
		etdDouble = 4
		etdOthers = 5
	End Enum
	
	Public Enum eConstNull
		NumNull = -32768
		strNull = Nothing
		dtmNull = Nothing
	End Enum
	
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
End Module







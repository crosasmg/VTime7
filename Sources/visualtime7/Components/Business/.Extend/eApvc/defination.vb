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
End Module







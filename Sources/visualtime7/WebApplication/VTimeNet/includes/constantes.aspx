<script language="VB" runat="Server">
'- Posibles acciones a aplicar sobre una p�liza

    Const clngPolicyIssue = "1"           'Emision de Poliza
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngCertifIssue = "2"           'Emision de Certificado
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


    Const clngRecuperation = "3"          'Recuperacion
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


    Const clngPolicyQuotation = "4"       'Cotizacion de Poliza
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngCertifQuotation = "5"       'Cotizacion de Certificado
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


    Const clngPolicyProposal = "6"        'Propuesta de Poliza
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngCertifProposal = "7"        'Propuesta de Certificado
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


    Const clngPolicyQuery = "8"           'Consulta de Poliza"
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngCertifQuery = "9"           'Consulta de Certificado
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngQuotationQuery = "10"       'Consulta de Cotizacion
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngProposalQuery = "11"        'Consulta de Solicitud
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


    Const clngPolicyAmendment = "12"      'Modificacion Normal de Poliza
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngTempPolicyAmendment = "13"  'Modificacion Temporal de Poliza
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngCertifAmendment = "14"      'Modificacion de Certificado
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngTempCertifAmendment = "15"  'Modificacion Temporal de Certificados
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


    Const clngQuotationConvertion = "16"  'Conversion de Cotizacion a Poliza
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngProposalConvertion = "17"   'Conversion de Propuesta a Poliza"
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


    Const clngPolicyReissue = "18"        'Re-emision de Poliza
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngCertifReissue = "19"        'Re-emision de Certificado
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


    Const clngReprint = "20"              'Re-impresion
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


    Const clngdeclarations = "21"         'Declaraciones
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


    Const clngCoverNote = "22"            'Nota de Cobertura
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


    Const clngPropQuotConvertion = "23"   'Conversion de Propuesta a Cotizaci�n"
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


Const clngPolicyQuotAmendent As String = "24" 'Cotizaci�n de Modificaci�n de p�liza

Const clngCertifQuotAmendent As String = "25" 'Cotizaci�n de Modificaci�n de certificado

Const clngPolicyPropAmendent As String = "26" 'Propuesta de Modificaci�n de p�liza

Const clngCertifPropAmendent As String = "27" 'Propuesta de Modificaci�n de certificado


Const clngPolicyQuotRenewal As String = "28" 'Cotizaci�n de Renovaci�n de p�liza

Const clngCertifQuotRenewal As String = "29" 'Cotizaci�n de Renovaci�n de certificado

Const clngPolicyPropRenewal As String = "30" 'Propuesta de Renovaci�n de p�liza

Const clngcertifPropRenewal As String = "31" 'Propuesta de Renovaci�n de Certificado


Const clngInspections As String = "32" 'Inspecciones	


Const clngQuotAmendConvertion As String = "33" 'Conversi�n Cotizacion de Modificaci�n a modificaci�n

Const clngPropAmendConvertion As String = "34" 'Conversi�n Propuesta de Modificaci�n a modificaci�n

Const clngQuotRenewalConvertion As String = "35" 'Conversi�n Cotizaci�n de Renovaci�n a p�liza

Const clngPropRenewalConvertion As String = "36" 'Conversi�n Propuesta de Renovaci�n a p�liza

Const clngQuotPropAmendentConvertion As String = "37" 'Conversi�n Cotizacion de Modificaci�n a Propuesta de Modificaci�n 

Const clngQuotPropRenewalConvertion As String = "38" 'Conversi�n Cotizacion de Renovaci�n a Propuesta de Renovaci�n


Const clngQuotAmendentQuery As String = "39" 'Consulta de Cotizaci�n de Modificaci�n

Const clngPropAmendentQuery As String = "40" 'Consulta de Propuesta de Modificaci�n

Const clngQuotRenewalQuery As String = "41" 'Consulta de Cotizaci�n de Renovaci�n

Const clngPropRenewalQuery As String = "42" 'Consulta de Propuesta de Renovaci�n

Const clngProprehabilitate As String = "43" 'Consulta modificaci�n de propuesta de rehabilitaci�n


Const clngDuplPolicy As String = "45" 'Duplicar P�liza

Const clngTransHolder As String = "46" 'Traspasar Asegurado


'- Declaraci�n de constantes para la ejecuci�n de Stored Procedures.
    Const rdbParamUnknown = 0
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbParamInput = 1
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbParamOutput = 2
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbParamInputOutput = 3
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbParamReturnValue = 4
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


    Const rdbEmpty = 0
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    'Const rdbSmallInt = 2
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbInteger = 3
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbSingle = 4
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbDouble = 5
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbCurrency = 6
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    '    Const rdbDBTimeStamp = 7
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbBSTR = 8
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbIDispatch = 9
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbError = 10
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbBoolean = 11
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbVariant = 12
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbIUnknown = 13
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbDecimal = 14
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbTinyInt = 16
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbUnsignedTinyInt = 17
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbSmallInt = 18
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbUnsignedInt = 19
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbBigInt = 20
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbUnsignedBigInt = 21
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbGUID = 72
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbBinary = 128
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbChar = 129
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbWChar = 130
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbNumeric = 131
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbUserDefined = 132
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbDBDate = 133
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbDBTime = 134
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbDBTimeStamp = 135
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbVarChar = 200
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbLongVarChar = 201
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbVarWChar = 202
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbLongVarWChar = 203
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbVarBinary = 204
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbLongVarBinary = 205
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


    Const rdbParamSigned = 16
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbParamNullable = 64
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const rdbParamLong = 128
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


'- Declaraci�n de constantes de frecuencia.
    Const clngDeclaMonthly = 1
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngDeclaTwoMonth = 2
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngDeclaTrheeMonth = 3
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngDeclaSixMonth = 4
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngDeclaYear = 5
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngNonDecla = 6
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


'- Constantes que indican el equivalente de NULL para cada tipo de dato

Const strNull As String = "" '+ String

Const intNull As Short = -32768 '+ Integer


    'Const intNull As Short = -32768 '+ Num�ro


'- Constantes para indicar el valor de un Check
Const vbChecked As String = "1"

Const vbUnChecked As String = "0"


'- Constantes que indican el tipo de combo a desplegar
    Const clngComboType = 1
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngWindowType = 2
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


'- Tipos de datos
    Const etdDate = 1
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const etdInteger = 2
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const etdLong = 3
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const etdDouble = 4
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


'+ Tipo de compa��a del sistema
    Const cstrInsurance = "1"                   '+ Seguros
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const cstrReinsurance = "2"                 '+ Co/Reaseguro
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const cstrBrokerOrBrokerageFirm = "3"       '+ Sociedades de Corretaje
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const cstrInsuranceReinsurance = "4"        '+ Seguros y Reaseguro
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


'+ Posibles errores en la validaci�n de la estructura del c�digo del cliente.
    Const FieldEmpty = 0                        '+ C�digo vac�o
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const StructInvalid = 2                     '+ Estructura inv�lida
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const TypeNotFound = 1                      '+ Tipo de cliente no encontrado
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

Const IsNotNumeric As Short = 3 '+ Valor no num�rico

Const FieldNotFound As Short = 4 '+ C�digo no encontrado

Const FieldNew As Short = 5 '+ C�digo nuevo (generado autom�ticamente)


'-Se define la variable encargada de indicar el tipo de forma a mostrar

    Const clngSpeWithHeader = 1
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngSeqWithHeader = 2
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngRepWithOutHeader = 3
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngSeqWithOutHeader = 4
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngSpeWithOutHeader = 5
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngRepWithHeader = 6
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngFraSpecific = 7
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngMenu = 8
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngFraRepetitive = 9
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngGeneralTable = 10
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const clngWindowsPopUp = 11
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


'- Se define los tipos de botones posibles a construir bajo una instruccion ButtonAcceptCancel    

    Const OnlyAccept = 1
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const OnlyCancel = 2
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


'- Se define el tipo de dato para el valores posibles
    Const eNumeric = "1"
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const eString = "2"
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


'- Constantes para identificar el tipo de comisi�n del intermediario. 
'- Valores posibles seg�n table47




'-Se definen las constantes globales para el manejo del tipo de p�liza

    Const cstrIndividual = "1"                '+ Individual
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const cstrCollective = "2"                '+ Colectiva
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const cstrMultiLocation = "3"             '+ Multilocalidad
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


'- Tipo de datos para el control de clientes.
'- Indica el tipo de forma a desplegar cuando se llama a la forma.
'- El valor por defecto es el 1
    Const SearchClient = 1
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const SearchClientPolicy = 2
    'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'

    Const SearchClientClaim = 3
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'


'- Se definen constantes para definir los ramos tecnicos (Table37)
Const ebtLife As Short = 1

Const ebtAuto As Short = 3

Const ebtGenerals As Short = 4

Const ebtCombina As Short = 5

Const ebtPrevisionals As Short = 6

Const ebtMedicalAtention As Short = 7

Const ebtTransport As Short = 8


'- Se definen constantes que indican el tipo de producto a desplegar en el control de productos
Const clngAll As Short = 1

Const clngActiveLife As Short = 2

Const clngAnnuitiesLife As Short = 3


Const cstrGrid As Short = 1

Const cstrFolder As Short = 2

    '- Constantes para identificar el tipo de comisi�n del intermediario. 
    '- Valores posibles seg�n table47
    Const CN_COMMTABLE = "1"
    Const CN_COMMFIX = "2"
    Const CN_COMMWITHOUT = "3"

</script>





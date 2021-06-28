Option Strict Off
Option Explicit On
Public Class transport
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'transport' in the system 29/06/2004 11:53:29 a.m.
	'+Objetivo: Propiedades según la tabla 'transport' en el sistema 29/06/2004 11:53:29 a.m.
	
	Public sCertype As String
	Public nBranch As Short
	Public nProduct As Short
	Public nPolicy As Integer
	Public nCertif As Integer
	Public nCurrency As Short
	Public nMaxLimTrip As Double
    Public nDep_rate As Double
    Public nRate_Apply As Double
    Public nDecla_freq As Short
	Public nEstAmount As Double
	Public nOverLine As Double
	Public nModalitySumins As Short
	Public nDep_prem As Double
	
	'**+ Variables used at the collection module
	'+ Variables usadas en el módulo de cobranzas
	Public nAmount As Double
	Public sClient As String
	'Public sDigit As String
	Public sCliename As String
	
	'**%Objective: Updates a registry to the table "transport" using the key for this table.
	'**%Parameters:
	'**%    nUsercode       - Code of the user updating the record
	'**%    sCertype        - Type of record
	'**%    nBranch         - Code of the line of business
	'**%    nProduct        - Code of the product
	'**%    nPolicy         - Policy number
	'**%    nCertif         - Number identifying the certificate
	'**%    dEffecdate      - Effective date
	'**%    nCurrency       - Code of the currency
	'**%    nMaxLimTrip     - Maximum sum insured
	'**%    nDep_rate       - Rate to obtain the deposit premium amount
	'**%    nDecla_freq     - Code of the frecuency of declaration
	'**%    nEstAmount      - Annual maximum sum insured
	'**%    nOverLine       - Percentage of  over line insurance applying to the capitals of the merchandise
	'**%    nModalitySumins - Code of the insured value modality
	'**%    nDep_prem       - Rate to obtain the deposit premium amount
	'%Objetivo: Actualiza un registro a la tabla "transport" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nUsercode       -   Código del usuario que actualiza el registro
	'%    sCertype        -   Tipo de registro
	'%    nBranch         -   Código de la línea del negocio
	'%    nProduct        -   Código del producto
	'%    nPolicy         -   Número de la póliza
	'%    nCertif         -   Número que identifica el certificado
	'%    dEffecdate      -   Fecha de efecto
	'%    nCurrency       -   Código de la moneda
	'%    nMaxLimTrip     -   Límite máximo asegurado
	'%    nDep_rate       -   Porcentaje para obtener el importe de prima depósito
	'%    nDecla_freq     -   Código de la frecuencia de declaración
	'%    nEstAmount      -   Límite máximo asegurado anual
	'%    nOverLine       -   Porcentaje de sobre seguro a aplicar a los capitales de las mercancías
	'%    nModalitySumins -   Código de la modalidad del valor asegurado
	'%    nDep_prem       -   Monto de prima de depósito
    Private Function Update(ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Short, ByVal nMaxLimTrip As Double, ByVal nDep_rate As Double, ByVal nDecla_freq As Short, ByVal nEstAmount As Double, ByVal nOverLine As Double, ByVal nModalitySumins As Short, ByVal nDep_prem As Double, ByVal nRate_Apply As Double) As Boolean
        Dim lclstransport As eRemoteDB.Execute


        lclstransport = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updtransport'. Generated on 29/06/2004 11:53:29 a.m.
        With lclstransport
            .StoredProcedure = "insupdtransport"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMaxLimTrip", nMaxLimTrip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDep_rate", nDep_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDecla_freq", nDecla_freq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEstAmount", nEstAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOverLine", nOverLine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModalitySumins", nModalitySumins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDep_prem", nDep_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate_Apply", nRate_Apply, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

        lclstransport = Nothing

        Exit Function
    End Function
	
	'**%Objective: It verifies the existence of a registry in table "transport" using the key of this table.
	'**%Parameters:
	'**%    sCertype    - Type of record
	'**%    nBranch     - Code of the line of business
	'**%    nProduct    - Code of the product
	'**%    nPolicy     - Policy number
	'**%    nCertif     - Number identifying the certificate
	'**%    dEffecdate  - Effective date
	'%Objetivo: Verifica la existencia de un registro en la tabla "transport" usando la clave de dicha tabla.
	'%Parámetros:
	'%    sCertype    - Tipo de registro
	'%    nBranch     - Código de la línea del negocio
	'%    nProduct    - Código del producto
	'%    nPolicy     - Número de la póliza
	'%    nCertif     - Número que identifica el certificado
	'%    dEffecdate  - Fecha de efecto
    Private Function IsExist(ByVal sCertype As String, _
                             ByVal nBranch As Short, _
                             ByVal nProduct As Short, _
                             ByVal nPolicy As Integer, _
                             ByVal nCertif As Integer, _
                             ByVal dEffecdate As Date) As Boolean
        Dim lclstransport As eRemoteDB.Execute
        Dim lintExist As Short


        lclstransport = New eRemoteDB.Execute
        lintExist = 0

        '+ Define all parameters for the stored procedures 'insudb.valtransportExist'. Generated on 29/06/2004 11:53:29 a.m.
        With lclstransport
            .StoredProcedure = "reatransport_v"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With

        lclstransport = Nothing

        Exit Function
    End Function
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%    sCodispl        - Logical code that identifies the transaction.
	'**%    nMainAction     - Action being executed on the transaction.
	'**%    sAction         - Action begin executed on the grid of the transaction
	'**%    sCertype        - Type of record
	'**%    nBranch         - Code of the line of business
	'**%    nProduct        - Code of the product
	'**%    nPolicy         - Policy number
	'**%    nCertif         - Number identifying the certificate
	'**%    dEffecdate      - Effective date
	'**%    nCurrency       - Code of the currency
	'**%    nMaxLimTrip     - Maximum sum insured
	'**%    nDep_rate       - Rate to obtain the deposit premium amount
	'**%    nDecla_freq     - Code of the frecuency of declaration
	'**%    nEstAmount      - Annual maximum sum insured
	'**%    nOverLine       - Percentage of  over line insurance applying to the capitals of the merchandise
	'**%    nModalitySumins - Code of the insured value modality
	'**%    nDep_prem       - Rate to obtain the deposit premium amount
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl        -   Código lógico que identifica la transacción.
	'%    nMainAction     -   Acción que se ejecuta sobre la transacción.
	'%    sAction         -   Acción que se ejecuta sobre el grid de la transacción
	'%    sCertype        -   Tipo de registro
	'%    nBranch         -   Código de la línea del negocio
	'%    nProduct        -   Código del producto
	'%    nPolicy         -   Número de la póliza
	'%    nCertif         -   Número que identifica el certificado
	'%    dEffecdate      -   Fecha de efecto
	'%    nCurrency       -   Código de la moneda
	'%    nMaxLimTrip     -   Límite máximo asegurado
	'%    nDep_rate       -   Porcentaje para obtener el importe de prima depósito
	'%    nDecla_freq     -   Código de la frecuencia de declaración
	'%    nEstAmount      -   Límite máximo asegurado anual
	'%    nOverLine       -   Porcentaje de sobre seguro a aplicar a los capitales de las mercancías
	'%    nModalitySumins -   Código de la modalidad del valor asegurado
	'%    nDep_prem       -   Monto de prima de depósito
    Public Function InsValTR001(ByVal sCodispl As String, _
                                ByVal nMainAction As Integer, _
                                ByVal sAction As String, _
                                ByVal sCertype As String, _
                                ByVal nBranch As Short, _
                                ByVal nProduct As Short, _
                                ByVal nPolicy As Integer, _
                                ByVal nCertif As Integer, _
                                ByVal dEffecdate As Date, _
                                ByVal nCurrency As Short, _
                                ByVal nMaxLimTrip As Double, _
                                ByVal nDep_rate As Double, _
                                ByVal nDecla_freq As Short, _
                                ByVal nEstAmount As Double, _
                                ByVal nOverLine As Double, _
                                ByVal nModalitySumins As Short, _
                                ByVal nDep_prem As Double, _
                                 ByVal nRate_apply As Double, _
                                ByVal sSche_code As String) As String

        Dim lclsErrors As eFunctions.Errors


        lclsErrors = New eFunctions.Errors

        If (nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 750024)
        End If

        If (nDep_rate > 100 Or nDep_rate <= 0) And nDep_rate <> eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 1935, , , " : ", 10515)
        End If

        If (nDep_rate > 0) Then
            If (nEstAmount = 0 Or nEstAmount = eRemoteDB.Constants.intNull) Then
                Call lclsErrors.ErrorMessage(sCodispl, 703006)
            End If

            If (nDep_prem = 0 Or nDep_prem = eRemoteDB.Constants.intNull) Then
                Call lclsErrors.ErrorMessage(sCodispl, 90389)
            End If
        End If

        If nEstAmount > 0 And nDep_rate = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 38050)
        End If

        If (nOverLine > 100 Or nOverLine <= 0) And nOverLine <> eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 1935, , , " : ", 10516)
        End If

        If IsDeclarative(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sSche_code) Then
            If nDecla_freq = eRemoteDB.Constants.intNull Or nDecla_freq = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 3217)
            End If
        End If

        If (nModalitySumins = 0 Or nModalitySumins = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3195)
        End If

        If (nRate_apply = 0 Or nRate_apply = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 60140)
        End If

        InsValTR001 = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function

    '**%Objective: it return the value TRUE is the policy in treatment is type of Declarative
    '**%Parameters:
    '**%    sCertype        - Type of record
    '**%    nBranch         - Code of the line of business
    '**%    nProduct        - Code of the product
    '**%    nPolicy         - Policy number
    '**%    nCertif         - Number identifying the certificate
    '**%    dEffecdate      - Effective date
    '%Objetivo: retorna verdadero si la póliza en tratamiento es declarativa
    '%Parámetros:
    '%    sCertype        -   Tipo de registro
    '%    nBranch         -   Código de la línea del negocio
    '%    nProduct        -   Código del producto
    '%    nPolicy         -   Número de la póliza
    '%    nCertif         -   Número que identifica el certificado
    '%    dEffecdate      -   Fecha de efecto
    Private Function IsDeclarative(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal sSche_code_user As String) As Object
        Dim lclsCertificat As ePolicy.Certificat



        lclsCertificat = New ePolicy.Certificat

        Call lclsCertificat.insPreCA004(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, 1, sSche_code_user)

        IsDeclarative = Not (lclsCertificat.sDeclari <> "1")

        lclsCertificat = Nothing

        Exit Function
    End Function

    '**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
    '**%Parameters:
    '**%    nHeader         - Indicator of the zone (Header or detail).
    '**%    sCodispl        - Logical code that identifies the transaction.
    '**%    nMainAction     - Action being executed on the transaction.
    '**%    sAction         - Action begin executed on the grid of the transaction
    '**%    nUsercode       - Code of the user creating o updating the record
    '**%    sCertype        - Type of record
    '**%    nBranch         - Code of the line of business
    '**%    nProduct        - Code of the product
    '**%    nPolicy         - Policy number
    '**%    nCertif         - Number identifying the certificate
    '**%    dEffecdate      - Effective date
    '**%    nCurrency       - Code of the currency
    '**%    nMaxLimTrip     - Maximum sum insured
    '**%    nDep_rate       - Rate to obtain the deposit premium amount
    '**%    nDecla_freq     - Code of the frecuency of declaration
    '**%    nEstAmount      - Annual maximum sum insured
    '**%    nOverLine       - Percentage of  over line insurance applying to the capitals of the merchandise
    '**%    nModalitySumins - Code of the insured value modality
    '**%    nDep_prem       - Rate to obtain the deposit premium amount
    '%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
    '%Parámetros:
    '%    nHeader         -   Indicador de zona de encabezado o detalle
    '%    sCodispl        -   Código lógico que identifica la transacción.
    '%    nMainAction     -   Acción que se ejecuta sobre la transacción.
    '%    sAction         -   Acción que se ejecuta sobre el grid de la transacción.
    '%    nUsercode       -   Código del usuario que crea o actualiza el registro
    '%    sCertype        -   Tipo de registro
    '%    nBranch         -   Código de la línea del negocio
    '%    nProduct        -   Código del producto
    '%    nPolicy         -   Número de la póliza
    '%    nCertif         -   Número que identifica el certificado
    '%    dEffecdate      -   Fecha de efecto
    '%    nCurrency       -   Código de la moneda
    '%    nMaxLimTrip     -   Límite máximo asegurado
    '%    nDep_rate       -   Porcentaje para obtener el importe de prima depósito
    '%    nDecla_freq     -   Código de la frecuencia de declaración
    '%    nEstAmount      -   Límite máximo asegurado anual
    '%    nOverLine       -   Porcentaje de sobre seguro a aplicar a los capitales de las mercancías
    '%    nModalitySumins -   Código de la modalidad del valor asegurado
    '%    nDep_prem       -   Monto de prima de depósito
    Public Function InsPostTR001(ByVal nHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Short, ByVal nMaxLimTrip As Double, ByVal nDep_rate As Double, ByVal nDecla_freq As Short, ByVal nEstAmount As Double, ByVal nOverLine As Double, ByVal nModalitySumins As Short, ByVal nDep_prem As Double, ByVal sPoliType As String, ByVal nRate_apply As Double) As Boolean

        Dim lclsPolicyWin As ePolicy.Policy_Win


        InsPostTR001 = Update(nUsercode, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nCurrency, nMaxLimTrip, nDep_rate, nDecla_freq, nEstAmount, nOverLine, nModalitySumins, nDep_prem, nRate_Apply)

        If InsPostTR001 Then
            lclsPolicyWin = New ePolicy.Policy_Win
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "TR001", "2")
            If sPoliType = "2" And nCertif = 0 Then
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "TR6000", "3", , , , False)
            End If
            lclsPolicyWin = Nothing
        End If


        Exit Function
    End Function

    '**%Objective: Function that makes the search in the table 'transport'.
    '**%Parameters:
    '**%    sCertype    - Type of record
    '**%    nBranch     - Code of the line of business
    '**%    nProduct    - Code of the product
    '**%    nPolicy     - Policy number
    '**%    nCertif     - Number identifying the certificate
    '**%    dEffecdate  - Effective date
    '%Objetivo: Función que realiza la búsqueda en la tabla 'transport'.
    '%Parámetros:
    '%    sCertype    - Tipo de registro
    '%    nBranch     - Código de la línea del negocio
    '%    nProduct    - Código del producto
    '%    nPolicy     - Número de póliza
    '%    nCertif     - Número que identifica el certificado
    '%    dEffecdate  - Fecha de efecto
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lclstransport As eRemoteDB.Execute


        lclstransport = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.reatransport'. Generated on 29/06/2004 11:53:30 a.m.
        With lclstransport
            .StoredProcedure = "reatransport_a"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Me.sCertype = sCertype
                Me.nBranch = nBranch
                Me.nProduct = nProduct
                Me.nPolicy = nPolicy
                Me.nCertif = nCertif
                Me.nCurrency = .FieldToClass("nCurrency")
                Me.nMaxLimTrip = .FieldToClass("nMaxLimTrip")
                Me.nDep_rate = .FieldToClass("nDep_rate")
                Me.nDecla_freq = .FieldToClass("nDecla_freq")
                Me.nEstAmount = .FieldToClass("nEstAmount")
                Me.nOverLine = .FieldToClass("nOverLine")
                Me.nModalitySumins = .FieldToClass("nModalitySumins")
                Me.nDep_prem = .FieldToClass("nDep_prem")
                Me.nRate_Apply = .FieldToClass("nRate_Apply")
                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With

        lclstransport = Nothing

        Exit Function
    End Function

    '**%Objective: This Function searches for specific information for collection purposes.
    '**%Parameters:
    '**%    sCertype    - Type of record
    '**%    nPolicy     - Policy number
    '**%    nCertif     - Number identifying the certificate
    '**%    dEffecdate  - Effective date
    '%Objetivo: Función que busca información específica para fines de cobranza
    '%Parámetros:
    '%    sCertype    - Tipo de registro
    '%    nPolicy     - Número de póliza
    '%    nCertif     - Número que identifica el certificado
    '%    dEffecdate  - Fecha de efecto
    Public Function Find_Policy(ByVal sCertype As String, ByVal nPolicy As Double, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lclstransport As eRemoteDB.Execute


        lclstransport = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.reatransport'. Generated on 29/06/2004 11:53:30 a.m.
        With lclstransport
            .StoredProcedure = "reaTransport_roles_a"
            .Parameters.Add("sCertype", IIf(sCertype = String.Empty, "2", sCertype), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                nBranch = .FieldToClass("nBranch")
                nProduct = .FieldToClass("nProduct")
                nCurrency = .FieldToClass("nCurrency")
                nAmount = .FieldToClass("nAmount")
                sClient = .FieldToClass("sclient")
                'sDigit = .FieldToClass("sDigit")
                sCliename = .FieldToClass("sCliename")
                Find_Policy = True
                .RCloseRec()
            Else
                Find_Policy = False
            End If
        End With

        lclstransport = Nothing

        Exit Function
    End Function

    '**%Objective: It is verified if the relation corresponds with a policy of the transport branch
    '**%Parameters:
    '**%    nBordereaux  - Number of the collection schedule or form
    '%Objetivo: Verifica si la relación corresponde con una póliza de transporte
    '%Parámetros:
    '%    nBordereaux    - Número de relación de cobro
    Public Function Find_Relation(ByVal nBordereaux As Double) As Boolean
        Dim lclstransport As eRemoteDB.Execute


        lclstransport = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.reatransport'. Generated on 29/06/2004 11:53:30 a.m.
        With lclstransport
            .StoredProcedure = "reat_Concepts_Trans_a"
            .Parameters.Add("nBordereaux ", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                sClient = .FieldToClass("sclient")
                'sDigit = .FieldToClass("sDigit")
                sCliename = .FieldToClass("sCliename")
                nBranch = .FieldToClass("nBranch")
                nProduct = .FieldToClass("nProduct")
                nPolicy = .FieldToClass("nPolicy")
                Find_Relation = True
                .RCloseRec()
            Else
                Find_Relation = False
            End If
        End With

        lclstransport = Nothing

        Exit Function
    End Function
End Class












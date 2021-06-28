Option Strict Off
Option Explicit On
Public Class FactorByCharge
    '**+Objective: Class that supports the table Execute it's content is:
    '**+Version: $$Revision: 3 $
    '+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
    '+Version: $$Revision: 3 $

    '**+Objective: Properties according to the table 'FactorByCharge' in the system 05/04/2005 05:23:00 p.m.
    '+Objetivo: Propiedades según la tabla 'FactorByCharge' en el sistema 05/04/2005 05:23:00 p.m.

    '+Objetivo: Número que identifica el cargo.
    Public nPosition As Integer

    '+Objetivo: Descripción del cargo.
    Public sDescript As String

    '+Objetivo: Factor a aplicar.
    Public nFactor As Integer

    Public nAction As Integer 'smallint     no        2      5     0     no

    '%Objetivo: Actualiza un registro a la tabla "FactorByCharge" usando la clave para dicha tabla.
    '%Parámetros:
    '%    nUsercode - Código de usuario.
    '%    sCertype - Tipo de registro.
    '%    nBranch - Código del ramo comercial.
    '%    nProduct - Código del producto.
    '%    nPolicy - Número que identifica la póliza/cotización/propuesta.
    '%    nCertif - Número del certificado.
    '%    sComplcod - Indole del riesgo, para establecer tasa básica.
    '%    sDescBussi - Tipo de negocio.
    '%    nConstcat - Detalle del tipo de negocio.
    Private Function Update(ByVal nAction As Integer, ByVal nPosition As Integer, ByVal nFactor As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lclsFactorByCharge As eRemoteDB.Execute


        lclsFactorByCharge = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updmulti_risk'. Generated on 05/04/2005 05:23:00 p.m.
        With lclsFactorByCharge
            .StoredProcedure = "insPostMMU7000"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPosition", nPosition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFactor", nFactor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
        End With
        lclsFactorByCharge = Nothing

        Exit Function
    End Function

    '%Objetivo: Validación de los datos para la página detalle.
    '%Parámetros:
    '%    sCodispl - Código de la transacción.
    '%    nMainAction - Número de la acción.
    '%    sAction - Acción a realizar.
    '%    sCertype - Tipo de registro.
    '%    nBranch - Código del ramo comercial.
    '%    nProduct - Código del producto.
    '%    nPolicy - Número que identifica la póliza/cotización/propuesta.
    '%    nCertif - Número del certificado.
    '%    sComplcod - Indole del riesgo, para establecer tasa básica.
    '%    sDescBussi - Tipo de negocio.
    '%    nConstcat - Detalle del tipo de negocio.
    Public Function insValMMU7000_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nPosition As Integer, ByVal nFactor As Integer, ByVal nUsercode As Integer) As String
        Dim lclsErrors As eFunctions.Errors


        lclsErrors = New eFunctions.Errors

        '+ Se valida que el campo FACTOR DE CAUCIÓN ESTE LLENO
        If (nFactor = 0 Or nFactor = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 9000115)
        End If


        insValMMU7000_K = lclsErrors.Confirm
        lclsErrors = Nothing

        Exit Function
    End Function

    '%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
    '%Parámetros:
    '%    pblnHeader - Indicador, si encuentra en la cabecera de la página.
    '%    sCodispl - Código de la transacción.
    '%    nMainAction -
    '%    sAction - Acción a realizar.
    '%    nUsercode - Código de usuario.
    '%    sCertype - Tipo de registro.
    '%    nBranch - Código del ramo comercial.
    '%    nProduct - Código del producto.
    '%    nPolicy - Número que identifica la póliza/cotización/propuesta.
    '%    nCertif - Número del certificado.
    '%    sComplcod - Indole del riesgo, para establecer tasa básica.
    '%    sDescBussi - Tipo de negocio.
    '%    nConstcat - Detalle del tipo de negocio.
    Public Function insPostMMU7000_K(ByVal sAction As String, ByVal nPosition As Integer, Optional ByVal nFactor As Integer = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lclsPolicyFun As Object
        Dim lComplCod As String

        On Error GoTo insPostMMU7000_K_err

        With Me
            sAction = Trim(sAction)
            Select Case sAction
                Case "Add"
                    .nAction = 1
                Case "Update"
                    .nAction = 2
                Case "Del"
                    .nAction = 3
            End Select
        End With
        insPostMMU7000_K = Update(nAction, nPosition, nFactor, nUsercode)

        'If insPostMMU7000_K Then
        'lclsPolicyWin = New ePolicy.Policy_Win
        ' Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "MU001", "2")
        'lclsPolicyWin = Nothing
        ' End If

        lclsPolicyFun = Nothing

insPostMMU7000_K_err:
        If Err.Number Then
            insPostMMU7000_K = False
        End If
        On Error GoTo 0
    End Function
End Class












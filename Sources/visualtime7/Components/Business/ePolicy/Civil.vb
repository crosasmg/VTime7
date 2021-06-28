Option Strict Off
Option Explicit On
Public Class Civil
	'%-------------------------------------------------------%'
	'% $Workfile:: Civil.cls                                $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Table information in the system on 11/03/2000
	'**-The keys field correspond to sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate
	'-Información de la tabla en el sistema el 03/11/2000
	'-Los campos llave corresponden a sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate
	
	Public sCertype As String
    Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
    'Public dEffecdate As Object
    'Public nArticle As Integer
    'Public nCapital As Double
    'Public nDet_risk As Integer
    'Public dExpiredat As Object
    'Public nGrup As Integer
    'Public dIssuedat As Object
    'Public nNullcode As Integer
    'Public dNulldate As Object
    'Public nPremium As Double
    'Public nRisk As Integer
    'Public dStartdate As Object
    'Public nTransactio As Integer
	Public nUnit_quan As Integer
	Public nUnit_type As Integer
    'Public nUsercode As Integer
	
    '+ Campos añadidos para soportar el Giro de Negocio
    Public sComplCod As String
    Public sDescBussi As String
    Public nConstCat As Short
    '+ Campos para Giro de Negocio
    Public nBusinessty As Short
    Public nCommergrp As Short
    Public nCodkind As Short

    Private mvarCivils As Civils
    Public Property Civils() As Civils
        Get
            If mvarCivils Is Nothing Then
                mvarCivils = New Civils
            End If

            Civils = mvarCivils

            Exit Property
        End Get
        Set(ByVal Value As Civils)

            mvarCivils = Value

            Exit Property
        End Set
    End Property

    Private Sub Class_Terminate_Renamed()

        mvarCivils = Nothing

        Exit Sub
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub

    '**%Objective: Add a record to the table "Civil"
    '**%Parameters:
    '**%    nusercode  - código del usuario
    '**%    sCertype   - tipo de poliza/cotización/ propuesta.
    '**%    nBranch    - code of the branch
    '**%    nProduct   - code of the product
    '**%    nPolicy    - code of the policy
    '**%    nCertif    - code of the Certificat
    '**%    dEffecdate - effective date of the record
    '**%    nunit_type - it indicates the type of assured units
    '**%    nunit_quan - it indicates the amount of assured units
    '**%    sComplCod  - Complete code of the business kind
    '**%    sDescBussi - Specifical description of the business kind
    '**%    nConstCat  - Construction Category

    '%Objetivo: Agrega un registro a la tabla "Civil"
    '%Parámetros:
    '%    nusercode  - código del usuario
    '%    sCertype   - tipo de poliza/cotización/ propuesta.
    '%    nBranch    - código del ramo
    '%    nProduct   - codigo del producto
    '%    nPolicy    - código de la poliza
    '%    nCertif    - código del certificado
    '%    dEffecdate - fecha de efecto del registro
    '%    nunit_type - indica el tipo de unidades aseguradas.
    '%    nunit_quan - indica el monto de las unidades aseguradas.
    '%    sComplCod  - Código completo del giro de negocio
    '%    sDescBussi - Descripción específica del giro de negocio
    '%    nConstCat  - Categoría de Construcción

    Private Function Add(ByVal nUserCode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date, ByVal nUnit_type As Integer, ByVal nUnit_quan As Integer, ByVal sComplCod As String, ByVal sDescBussi As String, ByVal nConstCat As Short) As Boolean
        Dim lclsCivil As eRemoteDB.Execute

        lclsCivil = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.creCivil'. Generated on 14/06/2004 11:08:40 a.m.

        With lclsCivil
            .StoredProcedure = "creCivil"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUnit_type", nUnit_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUnit_quan", nUnit_quan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sComplCod", sComplCod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescBussi", sDescBussi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConstCat", nConstCat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With
        lclsCivil = Nothing

        Exit Function
    End Function

    '**%Objective: Updates a registry to the table "Civil" using the key for this table.
    '**%Parameters:
    '**%    sCodispl   - logical code of the window
    '**%    nusercode  - code of the user that creates/updates a record
    '**%    sCertype   - type of record
    '**%    nBranch    - code of the branch
    '**%    nProduct   - code of the product
    '**%    nPolicy    - code of the policy
    '**%    nCertif    - code of the Certificat
    '**%    dEffecdate - effective date of the record
    '**%    nunit_type - it indicates the type of assured units
    '**%    nunit_quan - it indicates the amount of assured units
    '**%    sComplCod  - Complete code of the business kind
    '**%    sDescBussi - Specifical description of the business kind
    '**%    nConstCat  - Construction Category

    '%Objetivo: Actualiza un registro a la tabla "Civil" usando la clave para dicha tabla.
    '%Parámetros:
    '%    sCodispl   - Código lógico de la ventana
    '%    nusercode  - código del usuario
    '%    sCertype   - tipo de poliza/cotización/ propuesta.
    '%    nBranch    - código del ramo
    '%    nProduct   - codigo del producto
    '%    nPolicy    - código de la poliza
    '%    nCertif    - código del certificado
    '%    dEffecdate - fecha de efecto del registro
    '%    nunit_type - indica el tipo de unidades aseguradas.
    '%    nunit_quan - indica el monto de las unidades aseguradas.
    '%    sComplCod  - Código completo del giro de negocio
    '%    sDescBussi - Descripción específica del giro de negocio
    '%    nConstCat  - Categoría de Construcción
    Private Function Update(ByVal sCodispl As String, ByVal nUserCode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date, ByVal nUnit_type As Integer, ByVal nUnit_quan As Integer, ByVal sComplCod As String, ByVal sDescBussi As String, ByVal nConstCat As Short, ByVal nCodkind As Short, ByVal nBusinessty As Short, ByVal nCommergrp As Short, ByVal nProctype As Short) As Boolean
        Dim lclsCivil As eRemoteDB.Execute

        lclsCivil = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updCivil'. Generated on 14/06/2004 11:08:40 a.m.
        With lclsCivil
            .StoredProcedure = "insupdCivil"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUnit_type", nUnit_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUnit_quan", nUnit_quan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sComplCod", sComplCod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescBussi", sDescBussi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConstCat", nConstCat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCodkind", nCodkind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBusinessty", nBusinessty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommergrp", nCommergrp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProctype", nProctype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
        End With

        lclsCivil = Nothing

        Exit Function
    End Function

    '**%Objective: Delete a registry the table "Civil" using the key for this table.
    '**%Parameters:
    '**%    sCertype   - tipo de poliza/cotización/ propuesta.
    '**%    nBranch    - code of the branch
    '**%    nProduct   - code of the product
    '**%    nPolicy    - code of the policy
    '**%    nCertif    - code of the Certificat
    '**%    dEffecdate - effective date of the record
    '**%    nusercode  - code of the user that creates/updates a record

    '%Objetivo: Elimina un registro a la tabla "Civil" usando la clave para dicha tabla.
    '%Parámetros:
    '%    sCertype   - tipo de poliza/cotización/ propuesta.
    '%    nBranch    - código del ramo
    '%    nProduct   - codigo del producto
    '%    nPolicy    - código de la poliza
    '%    nCertif    - código del certificado
    '%    dEffecdate - fecha de efecto del registro
    '%    nusercode  - Código del usuario que crea/actualiza el registro
    Private Function Delete(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date, ByVal nUserCode As Integer) As Boolean
        Dim lclsCivil As eRemoteDB.Execute

        lclsCivil = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.delCivil'. Generated on 14/06/2004 11:08:40 a.m.
        With lclsCivil
            .StoredProcedure = "delCivil"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

        lclsCivil = Nothing

        Exit Function
    End Function

    '**%Objective: It verifies the existence of a registry in table "Civil" using the key of this table.
    '**%Parameters:
    '**%    sCertype   - tipo de poliza/cotización/ propuesta.
    '**%    nBranch    - code of the branch
    '**%    nProduct   - code of the product
    '**%    nPolicy    - code of the policy
    '**%    nCertif    - code of the Certificat
    '**%    dEffecdate - effective date of the record
    '%Objetivo: Verifica la existencia de un registro en la tabla "Civil" usando la clave de dicha tabla.
    '%Parámetros:
    '%    sCertype   - tipo de poliza/cotización/ propuesta.
    '%    nBranch    - código del ramo
    '%    nProduct   - codigo del producto
    '%    nPolicy    - código de la poliza
    '%    nCertif    - código del certificado
    '%    dEffecdate - fecha de efecto del registro
    Private Function IsExist(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date) As Boolean
        Dim lclsCivil As eRemoteDB.Execute
        Dim lintExist As Short

        lclsCivil = New eRemoteDB.Execute
        lintExist = 0

        '+ Define all parameters for the stored procedures 'insudb.valCivilExist'. Generated on 14/06/2004 11:08:40 a.m.
        With lclsCivil
            .StoredProcedure = "reaCivil_v"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With

        lclsCivil = Nothing

        Exit Function
    End Function


    '**%Objective: Validation of the data for the page details.
    '**%Parameters:
    '**%    sCodispl   - code of the page
    '**%    nMainAction - Main action of the window
    '**%    sAction    - Action that applies to the record in database
    '**%    sCertype   - tipo de poliza/cotización/ propuesta.
    '**%    nBranch    - code of the branch
    '**%    nProduct   - code of the product
    '**%    nPolicy    - code of the policy
    '**%    nCertif    - code of the Certificat
    '**%    dEffecdate - effective date of the record
    '**%    nunit_type - it indicates the type of assured units
    '**%    nunit_quan - it indicates the amount of assured units
    '**%    nBusinessty - Type of Business
    '**%    nCommergrp  - Commercial Group
    '**%    nCodkind    - Business Kind
    '**%    sDescBussi  - Specifical description of the business kind
    '**%    nConstCat   - Construction Category

    '%Objetivo: Validación de los datos para la página detalle.
    '%Parámetros:
    '%    sCodispl   - código de la pagina.
    '%    nMainAction - Acción principal de la ventana
    '%    sAction    - Acción que aplica al registro en base de datos
    '%    sCertype   - tipo de poliza/cotización/ propuesta.
    '%    nBranch    - código del ramo
    '%    nProduct   - codigo del producto
    '%    nPolicy    - código de la poliza
    '%    nCertif    - código del certificado
    '%    dEffecdate - fecha de efecto del registro
    '%    nunit_type - indica el tipo de unidades aseguradas.
    '%    nunit_quan - indica el monto de las unidades aseguradas.
    '%    nBusinessty - Tipo de negocio
    '%    nCommergrp  - Grupo comercial
    '%    nCodkind    - Giro de negocio
    '%    sDescBussi  - Descripción especifica del giro de negocio
    '%    nConstCat   - Categoría de construcción
    Public Function InsValRC001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date, ByVal nUnit_type As Integer, ByVal nUnit_quan As Integer, ByVal nBusinessty As Short, ByVal nCommergrp As Short, ByVal nCodkind As Short, ByVal sDescBussi As String, ByVal nConstCat As Short) As String
        Dim lclsErrors As eFunctions.Errors

        lclsErrors = New eFunctions.Errors

        '+ Valida que este lleno el Tipo - 94020
        If nBusinessty = 0 Or nBusinessty = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 94020)
        End If

        '+ Valida que este lleno el Grupo - 94021
        If nCommergrp = 0 Or nCommergrp = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 94021)
        End If

        '+ Valida que este lleno el Giro - 94022
        If nCodkind = eRemoteDB.Constants.intNull Or nCodkind = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 94022)
        End If

        '+ Valida que se haya ingresado un Tipo de Construcción - 94098
        If nConstCat = eRemoteDB.Constants.intNull Or nConstCat = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 94098)
        End If

        '    If (nArticle = 0 Or _
        ''        nArticle = NumNull) Then
        '        Call lclsErrors.ErrorMessage(sCodispl, 3485)
        '    End If
        '
        '    If (nDetailart = 0 Or _
        ''        nDetailart = NumNull) Then
        '        Call lclsErrors.ErrorMessage(sCodispl, 3486)
        '    End If

        If (nUnit_type <> 0 And nUnit_type <> eRemoteDB.Constants.intNull) Then
            If (nUnit_quan = 0 Or nUnit_quan = eRemoteDB.Constants.intNull) Then
                Call lclsErrors.ErrorMessage(sCodispl, 3515)
            End If
        Else
            If (nUnit_quan <> 0 And nUnit_quan <> eRemoteDB.Constants.intNull) Then
                Call lclsErrors.ErrorMessage(sCodispl, 3516)
            End If
        End If

        InsValRC001 = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function

    '**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
    '**%Parameters:
    '**%    sCodispl   - logical code of the page
    '**%    nMainAction - main action of the window
    '**%    sAction    - action that applies to the record(s) in database
    '**%    sCertype   - tipo de poliza/cotización/ propuesta.
    '**%    nBranch    - code of the branch
    '**%    nProduct   - code of the product
    '**%    nPolicy    - code of the policy
    '**%    nCertif    - code of the Certificat
    '**%    dEffecdate - effective date of the record
    '**%    nunit_type - it indicates the type of assured units
    '**%    nunit_quan - it indicates the amount of assured units
    '**%    nBusinessty - Type of Business
    '**%    nCommergrp  - Commercial Group
    '**%    nCodkind    - Business Kind
    '**%    sDescBussi  - Specifical description of the business kind
    '**%    nConstCat   - Construction Category

    '%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
    '%Parámetros:
    '%    sCodispl   - código de la pagina.
    '%    nMainAction  - Acción principal de la ventana
    '%    sAction    - Acción que aplica al registro(s) en base de datos
    '%    sCertype   - tipo de poliza/cotización/ propuesta.
    '%    nBranch    - código del ramo
    '%    nProduct   - codigo del producto
    '%    nPolicy    - código de la poliza
    '%    nCertif    - código del certificado
    '%    dEffecdate - fecha de efecto del registro
    '%    nunit_type - indica el tipo de unidades aseguradas.
    '%    nunit_quan - indica el monto de las unidades aseguradas.
    '%    nBusinessty - Tipo de negocio
    '%    nCommergrp  - Grupo comercial
    '%    nCodkind    - Giro de negocio
    '%    sDescBussi  - Descripción especifica del giro de negocio
    '%    nConstCat   - Categoría de construcción
    Public Function InsPostRC001(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUserCode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date, ByVal nUnit_type As Integer, ByVal nUnit_quan As Integer, ByVal nBusinessty As Short, ByVal nCommergrp As Short, ByVal nCodkind As Short, ByVal sDescBussi As String, ByVal nConstCat As Short, ByVal nProctype As Short) As Boolean

        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lclsBusinessFun As Object
        Dim lstrComplCod As String

        If sAction = "Del" Then
            InsPostRC001 = Delete(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecDate, nUserCode)
        Else
            '+Construye el código completo del giro: (1 carac.) nBusinessTy + (3 carac.) nCommerGrp + (2 carac.) nCodKind
            lclsBusinessFun = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.Business_Functs")
            lstrComplCod = lclsBusinessFun.calComplCode(nCodkind, nBusinessty, nCommergrp)

            InsPostRC001 = Update(sCodispl, nUserCode, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecDate, nUnit_type, nUnit_quan, lstrComplCod, sDescBussi, nConstCat, nCodkind, nBusinessty, nCommergrp, nProctype)
            lclsBusinessFun = Nothing
        End If

        If InsPostRC001 Then
            lclsPolicyWin = New ePolicy.Policy_Win
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecDate, nUserCode, "RC001", "2")
            lclsPolicyWin = Nothing
        End If

        Exit Function
    End Function


End Class

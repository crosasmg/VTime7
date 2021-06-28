Option Strict Off
Option Explicit On

Imports eFunctions.Extensions
Public Class Warranty
	
	Public dEffecdate As Date
	Public nProduct As Integer 'NUMBER(5),
	Public nBranch As Integer 'NUMBER(5),
	Public sCertype As String 'CHAR(1 BYTE),
	Public nPolicy As Double 'NUMBER(10),
	Public nCertif As Double 'NUMBER(10),
	Public NWARRNUMBER As Integer 'NUMBER(5),
	Public dNulldate As Date
	Public NCAPACITY As Double 'NUMBER(18,6),
	Public NCURRENCY As Integer 'NUMBER(5),
	Public SDOCCREDIT As String 'CHAR(30 BYTE),
	Public NTYPECREDIT As Integer 'NUMBER(5),
	Public DCOMPDATE As Date
	Public nUsercode As Integer 'NUMBER(5),
	Public NNOTENUM As Double 'NUMBER(10),
	Public NTRANSACTIO As Double 'NUMBER(10),
	Public DISSUEDAT As Date
	Public dStartdate As Date
	Public SCLIENT As String 'VARCHAR2(14 BYTE),
	Public DEXPIRDAT As Date
	Public NPREMIUM As Double 'NUMBER(18,6),
	Public NCAPITAL As Double 'NUMBER(18,6),
	Public NMATERIA As Integer 'NUMBER(5),
	Public nLimitCredit As Double 'NUMBER(18,2),
	Public nMaxCapital As Double 'Number(18, 2)
	Public sProjectName As String
	Public sAddress As String
	Public sIdentify As String
    Public dMaturity As Date
    Public sCliename As String
    Public sDescrole As String
	
	Public nLimitUsed As Double
	Public nCapInsured As Double

	Public nTypewarranty As Integer
    Public sDocwarranty As String
    Public nBondStatus As Integer

    '**%depending on whether the stored procedure executed correctly.
    '%Update: Este método se encarga de actualizar registros en la tabla "Warranty". Devolviendo verdadero o
    '%falso dependiendo de si el Stored procedure se ejecutó correctamente.
    Public Function insPostWT001(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sProjectName As String, ByVal sIdentify As String) As Boolean
        Dim lrecupdWarranty As eRemoteDB.Execute

        lrecupdWarranty = New eRemoteDB.Execute

        '**+Stored procedure parameters definition 'insudb.updWarranty'
        '**+Data of 12/07/1999 15:27:04
        '+Definición de parámetros para stored procedure 'insudb.updWarranty'
        '+Información leída el 7/12/1999 15:27:04

        With lrecupdWarranty
            .StoredProcedure = "INSUPDWarranty_WT001"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProjectName", sProjectName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIdentify", sIdentify, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostWT001 = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecupdWarranty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdWarranty = Nothing
    End Function



    '**%depending on whether the stored procedure executed correctly.
    '%Update: Este método se encarga de actualizar registros en la tabla "Warranty". Devolviendo verdadero o
    '%falso dependiendo de si el Stored procedure se ejecutó correctamente.
    Public Function Find_WT001(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nAction As Short) As Boolean
        Dim lrecReaWarranty As eRemoteDB.Execute

        lrecReaWarranty = New eRemoteDB.Execute

        '**+Stored procedure parameters definition 'insudb.updWarranty'
        '**+Data of 12/07/1999 15:27:04
        '+Definición de parámetros para stored procedure 'insudb.updWarranty'
        '+Información leída el 7/12/1999 15:27:04

        With lrecReaWarranty
            .StoredProcedure = "REAWarranty_WT001"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_WT001 = True
                sProjectName = .FieldToClass("sProjectName")
                sIdentify = .FieldToClass("sIdentify")
            End If
        End With
        'UPGRADE_NOTE: Object lrecReaWarranty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaWarranty = Nothing

    End Function



    '**%depending on whether the stored procedure executed correctly.
    '%Update: Este método se encarga de actualizar registros en la tabla "Warranty". Devolviendo verdadero o
    '%falso dependiendo de si el Stored procedure se ejecutó correctamente.
    Public Function insvalWT001(ByVal sCodispl As String, ByVal sPolitype As String, ByVal nCertif As Double, ByVal sProjectName As String, ByVal sIdentify As String) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lblnErr As Boolean

        On Error GoTo insValWT001_err

        lclsErrors = New eFunctions.Errors


        '+Se valida que el campo "Nombre del proyecto" tenga contenido

        If sProjectName = String.Empty Then
            Call lclsErrors.ErrorMessage(sCodispl, 9000012)
        End If


        '+Se valida que el campo "Identificacion del proyecto" tenga contenido

        If sIdentify = String.Empty Then
            If nCertif > 0 Or sPolitype = "1" Then
                Call lclsErrors.ErrorMessage(sCodispl, 1012)
            End If
        End If

        insvalWT001 = lclsErrors.Confirm

insValWT001_err:
        If Err.Number Then
            insvalWT001 = "insValWT001: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

    End Function
    '**%Objective: Updates a registry to the table "Warranty" using the key for this table.
    '**%Parameters:
    '**%     nusercode        -  código del usuario
    '**%     scertype         -  type of registry
    '**%     nbranch          -  branch
    '**%     nproduct         -  product
    '**%     npolicy          -  i number of poliza
    '**%     ncertif          -  i number of certificate
    '**%     deffecdate       -  date of effect of the registry
    '**%     nWarrnumber      -  Consecutive counterguarantee number.
    '**%     nTypewarranty    -  Code of type of counterguarantee
    '**%     sDocwarranty     -  Document number associated with the counterguarantee.
    '**%     nCurrency        -  Code of the currency.
    '**%     nCapacity        -  Amount of the counterguarantee.
    '**%     nNotenum         -  Number of the note containing the comments.
    '%Objetivo: Actualiza un registro a la tabla "Warranty" usando la clave para dicha tabla.
    '%Parámetros:
    '%     nusercode       -   código del usuario
    '%     scertype        -   tipo de registro
    '%     nbranch         -   ramo
    '%     nproduct        -   producto
    '%     npolicy         -   numero de poliza
    '%     ncertif         -   numero de certificado
    '%     deffecdate      -   fecha de efecto del registro
    '%     nWarrnumber     -   Numero consecutivo de contragarantia.
    '%     nTypewarranty   -   Código del tipo de contragarantía
    '%     sDocwarranty    -   Número de documento asociado a la contragarantía.
    '%     nCurrency       -   Código de la moneda.
    '%     nCapacity       -   Monto correspondiente a la contragarantía.
    '%     nNotenum        -   Número de la nota que contiene el texto libre.
    Private Function Update(ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nWarrnumber As Integer, ByVal nTypewarranty As Integer, ByVal sDocwarranty As String, ByVal nCurrency As Integer, ByVal nCapacity As Integer, ByVal nNotenum As Integer, ByVal dMaturity As Date, Optional ByVal sClient As String = "", Optional nBondStatus As Integer = 0) As Boolean
        Dim lclsWarranty As eRemoteDB.Execute

        lclsWarranty = New eRemoteDB.Execute
        With lclsWarranty
            .StoredProcedure = "insupdWarranty"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWarrnumber", nWarrnumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypewarranty", nTypewarranty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDocwarranty", sDocwarranty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapacity", nCapacity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dMaturity", dMaturity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBondStatus", nBondStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
        End With

        lclsWarranty = Nothing

        Exit Function
    End Function


    '**%Objective: Delete a registry the table "Warranty" using the key for this table.
    '**%Parameters:
    '**%     nusercode      -  código del usuario
    '**%     scertype       -  type of registry
    '**%     nbranch        -  branch
    '**%     nproduct       -  product
    '**%     npolicy        -  i number of poliza
    '**%     ncertif        -  i number of certificate
    '**%     deffecdate     -  date of effect of the registry
    '**%     nWarrnumber    -  Consecutive counterguarantee number.
    '**%     nUsercode      -  User Code
    '**%     sIndicator     -  Indicator of elimination.
    '%Objetivo: Elimina un registro a la tabla "Warranty" usando la clave para dicha tabla.
    '%Parámetros:
    '%     nusercode       -   código del usuario
    '%     scertype        -   tipo de registro
    '%     nbranch         -   ramo
    '%     nproduct        -   producto
    '%     npolicy         -   numero de poliza
    '%     ncertif         -   numero de certificado
    '%     deffecdate      -   fecha de efecto del registro
    '%     nWarrnumber     -   Numero consecutivo de contragarantia.
    '%     nUsercode       -   Código del usuario
    '%     sIndicator      -   Indicador de eliminación.
    Public Function Delete(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nWarrnumber As Integer, ByVal nUsercode As Integer, ByVal sIndicator As String) As Boolean
        Dim lclsWarranty As eRemoteDB.Execute

        lclsWarranty = New eRemoteDB.Execute
        With lclsWarranty
            .StoredProcedure = "delWarranty"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWarrnumber", nWarrnumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndicator", sIndicator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

        lclsWarranty = Nothing

        Exit Function
    End Function

    '**%Objective: Validation of the data for the page details.
    '**%Parameters:
    '**%     sCodispl         -  Logical code that identifies the transaction.
    '**%     nMainAction      -  Action being executed on the transaction.
    '**%     sAction          -  Action begin executed on the grid of the transaction
    '**%     scertype         -  type of registry
    '**%     nbranch          -  branch
    '**%     nproduct         -  product
    '**%     npolicy          -  i number of poliza
    '**%     ncertif          -  i number of certificate
    '**%     deffecdate       -  date of effect of the registry
    '**%     nWarrnumber      -  Consecutive counterguarantee number.
    '**%     nTypewarranty    -  Code of type of counterguarantee
    '**%     sDocwarranty     -  Document number associated with the counterguarantee.
    '**%     nCurrency        -  Code of the currency.
    '**%     nCapacity        -  Amount of the counterguarantee.
    '**%     nNotenum         -  Number of the note containing the comments.
    '%Objetivo: Validación de los datos para la página detalle.
    '%Parámetros:
    '%     sCodispl        -   Código lógico que identifica la transacción.
    '%     nMainAction     -   Acción que se ejecuta sobre la transacción.
    '%     sAction         -   Acción que se ejecuta sobre el grid de la transacción.
    '%     scertype        -   tipo de registro
    '%     nbranch         -   ramo
    '%     nproduct        -   producto
    '%     npolicy         -   numero de poliza
    '%     ncertif         -   numero de certificado
    '%     deffecdate      -   fecha de efecto del registro
    '%     nWarrnumber     -   Numero consecutivo de contragarantia.
    '%     nTypewarranty   -   Código del tipo de contragarantía
    '%     sDocwarranty    -   Número de documento asociado a la contragarantía.
    '%     nCurrency       -   Código de la moneda.
    '%     nCapacity       -   Monto correspondiente a la contragarantía.
    '%     nNotenum        -   Número de la nota que contiene el texto libre.
    Public Function InsValCC001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nTypewarranty As Integer, ByVal sDocwarranty As String, ByVal nCurrency As Integer, ByVal nCapacity As Integer, ByVal nNotenum As Integer, ByVal dMaturity As Date) As String
        Dim lclsErrors As eFunctions.Errors
        Dim resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("CC001Frame", False, "Policy", "PolicySeq")
        lclsErrors = New eFunctions.Errors

        If nTypewarranty = eRemoteDB.Constants.intNull And (sDocwarranty <> "" Or nCurrency <> 0 Or nCapacity <> eRemoteDB.Constants.intNull Or nNotenum <> eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3363)
        End If

        If nTypewarranty <> eRemoteDB.Constants.intNull And sDocwarranty = "" Then
            Call lclsErrors.ErrorMessage(sCodispl, 3379)
        End If

        If nTypewarranty <> eRemoteDB.Constants.intNull And nCurrency = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1351)
        End If

        If nTypewarranty <> eRemoteDB.Constants.intNull And nCapacity = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 3365)
        End If

        If nTypewarranty <> eRemoteDB.Constants.intNull And (dEffecdate > dMaturity Or dMaturity = eRemoteDB.Constants.dtmNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("tcdMaturityColumnCaption"))
        End If


        InsValCC001 = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function

    '**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
    '**%Parameters:
    '**%     nHeader          -  Indicator of the zone (Header or detail).
    '**%     sCodispl         -  Logical code that identifies the transaction.
    '**%     nMainAction      -  Action being executed on the transaction.
    '**%     sAction          -  Action begin executed on the grid of the transaction
    '**%     nusercode        -  código del usuario
    '**%     scertype         -  type of registry
    '**%     nbranch          -  branch
    '**%     nproduct         -  product
    '**%     npolicy          -  i number of poliza
    '**%     ncertif          -  i number of certificate
    '**%     deffecdate       -  date of effect of the registry
    '**%     nWarrnumber      -  Consecutive counterguarantee number.
    '**%     nTypewarranty    -  Code of type of counterguarantee
    '**%     sDocwarranty     -  Document number associated with the counterguarantee.
    '**%     nCurrency        -  Code of the currency.
    '**%     nCapacity        -  Amount of the counterguarantee.
    '**%     nNotenum         -  Number of the note containing the comments.
    '%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
    '%Parámetros:
    '%     nHeader         -   Indicador de zona de encabezado o detalle
    '%     sCodispl        -   Código lógico que identifica la transacción.
    '%     nMainAction     -   Acción que se ejecuta sobre la transacción.
    '%     sAction         -   Acción que se ejecuta sobre el grid de la transacción.
    '%     nusercode       -   código del usuario
    '%     scertype        -   tipo de registro
    '%     nbranch         -   ramo
    '%     nproduct        -   producto
    '%     npolicy         -   numero de poliza
    '%     ncertif         -   numero de certificado
    '%     deffecdate      -   fecha de efecto del registro
    '%     nWarrnumber     -   Numero consecutivo de contragarantia.
    '%     nTypewarranty   -   Código del tipo de contragarantía
    '%     sDocwarranty    -   Número de documento asociado a la contragarantía.
    '%     nCurrency       -   Código de la moneda.
    '%     nCapacity       -   Monto correspondiente a la contragarantía.
    '%     nNotenum        -   Número de la nota que contiene el texto libre.
    Public Function InsPostCC001(ByVal nHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nWarrnumber As Integer, ByVal nTypewarranty As Integer, ByVal sDocwarranty As String, ByVal nCurrency As Integer, ByVal nCapacity As Integer, ByVal nNotenum As Integer, ByVal nTransaction As String, ByVal dMaturity As Date, Optional ByVal sclient As String = "", Optional nBondStatus As Integer = 0) As Boolean

        If sAction = "Del" Then
            InsPostCC001 = Delete(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nWarrnumber, nUsercode, "2")
        Else
            If sAction = "Add" Then
                nWarrnumber = eRemoteDB.Constants.intNull
            End If
            InsPostCC001 = Update(nUsercode, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nWarrnumber, nTypewarranty, sDocwarranty, nCurrency, nCapacity, nNotenum, dMaturity, sclient, nBondStatus)
        End If

        Exit Function
    End Function


End Class







Option Strict Off
Option Explicit On
Public Class Commiss_contr
    Public nInsur_area As Integer
    Public nCompany As Double
    Public nBranch_rei As Double
    Public nType As Double
    Public nNumber As Double
    Public nCovergen As Integer
    Public nTypeVal As Integer
    Public nFromValue As Double
    Public dEffecdate As Date

    Public nToValue As Double
    Public nAmountfix As Double
    Public nCurrency As Double
    Public nPercent As Double
    Public sTypecom As String

    Public dNulldate As Date
    Public dCompdate As Date
    Public nUsercode As Integer


    Public Function Add(ByVal nAction As Integer) As Boolean
        Dim lreccreCommiss_contr As eRemoteDB.Execute

        On Error GoTo creCommiss_contr_Err

        lreccreCommiss_contr = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure creCommiss_contr al 04-09-2002 12:49:14
        '+
        With lreccreCommiss_contr
            .StoredProcedure = "insCommiss_contr"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeval", nTypeVal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFromValue", nFromValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nToValue", nToValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmountFix", nAmountfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypecom", sTypecom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

creCommiss_contr_Err:
        If Err.Number Then
            Add = False
        End If
        'UPGRADE_NOTE: Object lreccreCommiss_contr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreCommiss_contr = Nothing
        On Error GoTo 0
    End Function
    Public Function InsValCR783_K(ByVal sCodispl As String, ByVal dEffecdate As Date, ByVal nAction As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsContrproc As eCoReinsuran.Contrproc

        lclsErrors = New eFunctions.Errors
        lclsContrproc = New eCoReinsuran.Contrproc


        On Error GoTo InsValCR783_k_Err
        If dEffecdate = eRemoteDB.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 999999)
        Else
            If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
                '+ Si la fecha está llena y la acción es "modificar", se valida que no exista una modificación posterior a la que se está colocando en la  ventana. 
                '+ De ser así, se envía un mensaje al usuario indicando que la modificación sólo se 
                '+ puede realizar a una fecha posterior o igual a la de la última modificación realizada. 
                If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
                    If FindLastModifyCR783() Then
                        If Me.dEffecdate <> eRemoteDB.Constants.dtmNull AndAlso dEffecdate < Me.dEffecdate Then
                            Call lclsErrors.ErrorMessage(sCodispl, 10869, , , Me.dEffecdate)
                        End If
                    End If
                End If
            End If
        End If
        InsValCR783_K = lclsErrors.Confirm
        lclsErrors = Nothing

InsValCR783_k_Err:
        If Err.Number Then
            InsValCR783_K = InsValCR783_K & Err.Description
        End If
        On Error GoTo 0
    End Function
    '%insValLastnModify: Se realiza la validación de la fecha de última modificación del contrato
    Public Function FindLastModifyCR783() As Boolean
        Dim lrecreaCommiss_contr_effecdate As eRemoteDB.Execute

        lrecreaCommiss_contr_effecdate = New eRemoteDB.Execute

        On Error GoTo insValLastnModify_Err

        '+ Definición de parámetros para stored procedure 'insudb.reaCommiss_contr_effecdate'
        ' Información leída el 07/06/2001 10:30:09 a.m.

        With lrecreaCommiss_contr_effecdate
            .StoredProcedure = "reaCommiss_contr_effecdate"
            If .Run Then
                Me.dEffecdate = .FieldToClass("dEffecdate")
                FindLastModifyCR783 = True
            Else
                FindLastModifyCR783 = False
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaCommiss_contr_effecdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCommiss_contr_effecdate = Nothing

insValLastnModify_Err:
        If Err.Number Then
            FindLastModifyCR783 = False
        End If
    End Function
    '+InsposCR783 : Función que realiza los cambios en la base de datos especificados en CR783
    Public Function InspostCR783Upd(ByVal sAction As String, ByVal nInsur_area As Integer, ByVal nCompany As Double, ByVal nBranch_rei As Double, ByVal nType As Double, ByVal nNumber As Double, ByVal nCovergen As Integer, ByVal nTypeVal As Integer, ByVal nFromValue As Double, ByVal dEffecdate As Date, ByVal nToValue As Double, ByVal nAmountfix As Double, ByVal nCurrency As Double, ByVal nPercent As Double, ByVal sTypecom As String, ByVal nUsercode As Integer) As Object
        Dim lintAction As Integer

        On Error GoTo InspostCR783_Err

        With Me
            .nInsur_area = nInsur_area
            .nCompany = nCompany
            .nBranch_rei = nBranch_rei
            .nType = nType
            .nNumber = nNumber
            .nCovergen = nCovergen
            .nTypeVal = nTypeVal
            .nFromValue = nFromValue
            .dEffecdate = dEffecdate
            .nUsercode = nUsercode
            .nToValue = nToValue
            .nAmountfix = nAmountfix
            .nCurrency = nCurrency
            .nPercent = nPercent
            .sTypecom = sTypecom


            If sAction = "Del" Then
                lintAction = 3
            Else
                If sAction = "Update" Then
                    lintAction = 2
                Else
                    If sAction = "Add" Then
                        lintAction = 1
                    End If
                End If
            End If
            InspostCR783Upd = Add(lintAction)
        End With

InspostCR783_Err:
        If Err.Number Then
            InspostCR783Upd = False
        End If
        On Error GoTo 0
    End Function

    Public Function Find_v(ByVal nAction As Integer, ByVal nInsur_area As Double, ByVal nCompany As Double, ByVal nBranch_Rei As Double, ByVal nType As Integer, ByVal nNumber As Double, nCovergen As Double, ByVal nTypeval As Integer, ByVal nFromValue As Double, ByVal nToValue As Double, ByVal dEffecdate As Date) As Integer
        Dim lreccreCommiss_contr As eRemoteDB.Execute

        On Error GoTo creCommiss_contr_Err

        lreccreCommiss_contr = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure creCommiss_contr al 04-09-2002 12:49:14
        '+
        With lreccreCommiss_contr
            .StoredProcedure = "ReaCommiss_contr_v"
            .Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch_rei", nBranch_Rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeval", nTypeval, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFromValue", nFromValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nToValue", nToValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            If .Run Then
                Find_v = .FieldToClass("nError")
            Else
                Find_v = 3

            End If
        End With

creCommiss_contr_Err:
        If Err.Number Then
            Find_v = 0
        End If
        'UPGRADE_NOTE: Object lreccreCommiss_contr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreCommiss_contr = Nothing
        On Error GoTo 0

    End Function
    Public Function InsValCR783(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal nCompany As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nNumber As Integer, ByVal nCovergen As Integer, ByVal nTypeVal As Integer, ByVal nFromValue As Double, ByVal dEffecdate As Date, ByVal nToValue As Double, ByVal nAmountFix As Double, ByVal nCurrency As Integer, ByVal nPercent As Double, ByVal sTypeCom As String, ByVal nAction As Integer, ByVal sAction As String) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsContrproc As eCoReinsuran.Contrproc
        Dim lintAction As Integer
        Dim lintError As Integer

        lintError = 0
        lclsErrors = New eFunctions.Errors
        lclsContrproc = New eCoReinsuran.Contrproc


        On Error GoTo InsValCR783_Err

        '+ Se valida el ramo del reaseguro
        If nInsur_area = eRemoteDB.Constants.intNull Or nInsur_area = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 60215)
            lintError = 1
        End If

        '+ Se valida el ramo del reaseguro
        If nCompany = eRemoteDB.Constants.intNull Or nCompany = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 55885)
            lintError = 1
        End If
        '+ Se valida el ramo del reaseguro
        If nCovergen = eRemoteDB.Constants.intNull Or nCovergen = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 11163)
            lintError = 1
        End If

        '+ Se valida el ramo del reaseguro
        If nBranch_rei = eRemoteDB.Constants.intNull Or nBranch_rei = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 60314)
            lintError = 1
        End If

        '+Se valida que el código del contrato
        If nNumber = eRemoteDB.Constants.intNull Or nNumber = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 3357)
            lintError = 1
        End If

        '+Se valida que el tipo de contrato
        If nType = eRemoteDB.Constants.intNull Or nType = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 6018)
            lintError = 1
        End If

        If lintError = 0 Then
            '+ Se valida que el registro exista en la tabla CONTRPROC
            If Not lclsContrproc.Find(nNumber, nType, nBranch_rei, dEffecdate) Then
                Call lclsErrors.ErrorMessage(sCodispl, 21002)
            End If
        End If

        '+Se valida que el tipo de contrato
        If nTypeVal = eRemoteDB.Constants.intNull Or nType = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 9040)
            lintError = 1
        End If

        '+Se valida que el tipo de contrato
        If sTypeCom = vbNullString Or sTypeCom = "" Then
            Call lclsErrors.ErrorMessage(sCodispl, 3867)
            lintError = 1
        End If

        '+Se valida el valor inicial y final
        If nFromValue = eRemoteDB.Constants.intNull Or nFromValue = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 10247)
        End If

        '+ Validación del valor hasta
        If nToValue = eRemoteDB.Constants.intNull Or nToValue = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 10248)
        End If

        If nToValue > 0 And nFromValue > 0 Then
            If nFromValue > nToValue Then
                Call lclsErrors.ErrorMessage(sCodispl, 10216, , eFunctions.Errors.TextAlign.RigthAling, ". Valor Prima/Año póliza hasta es menor que valor desde")
            End If
        End If

        If nCurrency = eRemoteDB.Constants.intNull Or nCurrency <= 0 Then
            If nTypeVal = 1 Or nAmountFix > 0 Then '+Si el rango es por prima o si se indicó monto fijo se debe incluir la moneda
                Call lclsErrors.ErrorMessage(sCodispl, 10107)
            End If
        End If

        '+ Validación del porcentaje
        If nPercent = eRemoteDB.Constants.intNull And nAmountFix = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 5)
        End If
        If nPercent > 0 And nAmountFix > 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 3046)
        End If

        '+ Fecha : Debe estar lleno. 
        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 2056)
        End If


        If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
            '+ Si la fecha está llena y la acción es "modificar", se valida que no exista una modificación posterior a la que se está colocando en la  ventana. 
            '+ De ser así, se envía un mensaje al usuario indicando que la modificación sólo se 
            '+ puede realizar a una fecha posterior o igual a la de la última modificación realizada. 
            If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
                If FindLastModifyCR783() Then
                    If Me.dEffecdate <> eRemoteDB.Constants.dtmNull AndAlso dEffecdate < Me.dEffecdate Then
                        Call lclsErrors.ErrorMessage(sCodispl, 10869, , , Me.dEffecdate)
                    End If
                End If
            End If
        End If
        If sAction = "Del" Then
            lintAction = 3
        Else
            If sAction = "Update" Then
                lintAction = 2
            Else
                If sAction = "Add" Then
                    lintAction = 1
                End If
            End If
        End If
        lintError = 0
        lintError = Find_v(lintAction, nInsur_area, nCompany, nBranch_rei, nType, nNumber, nCovergen, nTypeVal, nFromValue, nToValue, dEffecdate)
        '+Si se esta agregando y ya existe un registro con la clave indicada se envía mensaje de error
        If lintError = 1 Then
            Call lclsErrors.ErrorMessage(sCodispl, 10185, , eFunctions.Errors.TextAlign.LeftAling, "Combinación Contrato - Compañia - Valor inicial ")
            '+Si se esta indicando un rango que ya se encuentra dentro de otro rango en la tabla se envía mensaje de error
        ElseIf lintError = 2 Then
            Call lclsErrors.ErrorMessage(sCodispl, 60214)
        End If

        InsValCR783 = lclsErrors.Confirm
        lclsErrors = Nothing
InsValCR783_Err:
        If Err.Number Then
            InsValCR783 = InsValCR783 & Err.Description
        End If
        On Error GoTo 0
    End Function
End Class

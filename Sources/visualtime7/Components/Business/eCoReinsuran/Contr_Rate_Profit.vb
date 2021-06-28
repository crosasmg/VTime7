Option Strict Off
Option Explicit On
Public Class Contr_Rate_Profit
    Public nNumber As Double     'NUMBER(5)                        NOT NULL,
    Public nType As Double       'NUMBER(5)                        NOT NULL,
    Public nBranch_Rei As Double 'NUMBER(5)                        NOT NULL,
    Public nCompany As Double    'NUMBER(5),
    Public nIni_Policy As Double 'NUMBER(10)                       NOT NULL,
    Public nEnd_Policy As Double 'NUMBER(10)                       NOT NULL,
    Public nPercent As Double    'NUMBER(9,6)                      NOT NULL,
    Public dEffecdate As Date    'DATE                             NOT NULL,
    Public dNulldate As Date     'DATE
    Public dCompdate As Date     'DATE       7    0     0    N
    Public nUsercode As Integer


    'NUMBER     22   0     5    N
    Public Function Add(ByVal nAction As Integer) As Boolean
        Dim lreccreContr_rate_profit As eRemoteDB.Execute

        On Error GoTo creContr_rate_profit_Err

        lreccreContr_rate_profit = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure creContr_rate_profit al 04-09-2002 12:49:14
        '+
        With lreccreContr_rate_profit
            .StoredProcedure = "insContr_rate_profit"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch_rei", nBranch_Rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIni_Policy", nIni_Policy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEnd_Policy", nEnd_Policy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

creContr_rate_profit_Err:
        If Err.Number Then
            Add = False
        End If
        'UPGRADE_NOTE: Object lreccreContr_rate_profit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreContr_rate_profit = Nothing
        On Error GoTo 0

    End Function
    Public Function InsValCR782_K(ByVal sCodispl As String, ByVal dEffecdate As Date, ByVal nAction As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsContrproc As eCoReinsuran.Contrproc

        lclsErrors = New eFunctions.Errors
        lclsContrproc = New eCoReinsuran.Contrproc


        On Error GoTo InsValCR782_k_Err
        If dEffecdate = eRemoteDB.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 999999)
        Else
            If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
                '+ Si la fecha está llena y la acción es "modificar", se valida que no exista una modificación posterior a la que se está colocando en la  ventana. 
                '+ De ser así, se envía un mensaje al usuario indicando que la modificación sólo se 
                '+ puede realizar a una fecha posterior o igual a la de la última modificación realizada. 
                If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
                    If FindLastModifyCR782() Then
                        If Me.dEffecdate <> eRemoteDB.Constants.dtmNull AndAlso dEffecdate < Me.dEffecdate Then
                            Call lclsErrors.ErrorMessage(sCodispl, 10869, , , Me.dEffecdate)
                        End If
                    End If
                End If
            End If
        End If
        InsValCR782_K = lclsErrors.Confirm
        lclsErrors = Nothing

InsValCR782_k_Err:
        If Err.Number Then
            InsValCR782_K = InsValCR782_K & Err.Description
        End If
        On Error GoTo 0
    End Function

    Public Function InsValCR782(ByVal sCodispl As String, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCompany As Integer, ByVal nIni_Policy As Integer, ByVal nEnd_Policy As Integer, ByVal nPercent As Double, ByVal dEffecdate As Date, ByVal nAction As Integer, ByVal sAction As String) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsContrproc As eCoReinsuran.Contrproc
        Dim lintAction As Integer
        Dim lintError As Integer

        lclsErrors = New eFunctions.Errors
        lclsContrproc = New eCoReinsuran.Contrproc


        On Error GoTo InsValCR782_Err

        '+ Se valida que el registro exista en la tabla CONTRPROC
        If lclsContrproc.Find(nNumber, nType, nBranch_rei, dEffecdate) Then

            '+ Se valida el ramo del reaseguro
            If nBranch_rei = eRemoteDB.Constants.intNull Or nBranch_rei = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 60314)
            End If

            '+Se valida que el código del contrato
            If nNumber = eRemoteDB.Constants.intNull Or nNumber = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 3357)
            End If

            '+Se valida que el tipo de contrato
            If nType = eRemoteDB.Constants.intNull Or nType = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 6018)
            End If

            '+Se valida la Póliza inicial
            If nIni_Policy = eRemoteDB.Constants.intNull Or nIni_Policy = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 80075)
            End If

            '+ Validación del deducible
            If nEnd_Policy = eRemoteDB.Constants.intNull Or nEnd_Policy = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 80076)
            End If

            If nEnd_Policy > 0 And nIni_Policy > 0 Then
                If nIni_Policy > nEnd_Policy Then
                    Call lclsErrors.ErrorMessage(sCodispl, 3621)
                End If
            End If

            '+ Validación del porcentaje
            If nPercent = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 55540)
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
                    If FindLastModifyCR782() Then
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
            lintError = Find_v(lintAction, nNumber, nType, nBranch_rei, nCompany, nIni_Policy, nEnd_Policy, dEffecdate)
            '+Si se esta agregando y ya existe un registro con la clave indicada se envía mensaje de error
            If lintError = 1 Then
                Call lclsErrors.ErrorMessage(sCodispl, 700016, , eFunctions.Errors.TextAlign.LeftAling, "Combinación Contrato - Compañia - Poliza inicial ")
            End If
            '+Si se esta indicando un rango que ya se encuentra dentro de otro rango en la tabla se envía mensaje de error
            If lintError = 2 Then
                Call lclsErrors.ErrorMessage(sCodispl, 60214)
            End If
        Else
            Call lclsErrors.ErrorMessage(sCodispl, 21002)
        End If

        InsValCR782 = lclsErrors.Confirm
        lclsErrors = Nothing
InsValCR782_Err:
        If Err.Number Then
            InsValCR782 = InsValCR782 & Err.Description
        End If
        On Error GoTo 0
    End Function
    Public Function Find_v(ByVal nAction As Integer, ByVal nNumber As Double, ByVal nType As Integer, ByVal nBranch_Rei As Double, ByVal nCompany As Double, ByVal nIni_Policy As Double, ByVal nEnd_policy As Double, ByVal dEffecdate As Date) As Integer
        Dim lreccreContr_rate_profit As eRemoteDB.Execute

        On Error GoTo creContr_rate_profit_Err

        lreccreContr_rate_profit = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure creContr_rate_profit al 04-09-2002 12:49:14
        '+
        With lreccreContr_rate_profit
            .StoredProcedure = "ReaContr_rate_profit_v"
            .Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch_rei", nBranch_Rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIni_Policy", nIni_Policy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEnd_Policy", nEnd_policy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_v = .FieldToClass("nError")
            Else
                Find_v = 3

            End If
        End With

creContr_rate_profit_Err:
        If Err.Number Then
            Find_v = 0
        End If
        'UPGRADE_NOTE: Object lreccreContr_rate_profit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreContr_rate_profit = Nothing
        On Error GoTo 0

    End Function
    '%insValLastnModify: Se realiza la validación de la fecha de última modificación del contrato
    Public Function FindLastModifyCR782() As Boolean
        Dim lrecreaContrnpro_effecdate As eRemoteDB.Execute

        lrecreaContrnpro_effecdate = New eRemoteDB.Execute

        On Error GoTo insValLastnModify_Err

        '+ Definición de parámetros para stored procedure 'insudb.reaContrnpro_effecdate'
        ' Información leída el 07/06/2001 10:30:09 a.m.

        With lrecreaContrnpro_effecdate
            .StoredProcedure = "reaContr_rate_profit_effecdate"
            If .Run Then
                Me.dEffecdate = .FieldToClass("dEffecdate")
                FindLastModifyCR782 = True
            Else
                FindLastModifyCR782 = False
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaContrnpro_effecdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaContrnpro_effecdate = Nothing

insValLastnModify_Err:
        If Err.Number Then
            FindLastModifyCR782 = False
        End If
    End Function
    '+InsposCR782 : Función que realiza los cambios en la base de datos especificados en CR782
    Public Function InspostCR782Upd(ByVal sAction As String, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCompany As Integer, ByVal nIni_Policy As Double, ByVal nEnd_Policy As Double, nPercent As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Object
        Dim lintAction As Integer

        On Error GoTo InspostCR782_Err

        With Me
            .nNumber = nNumber
            .nBranch_Rei = nBranch_rei
            .nType = nType
            .nCompany = nCompany
            .nIni_Policy = nIni_Policy
            .nEnd_Policy = nEnd_Policy
            .nPercent = nPercent
            .dEffecdate = dEffecdate
            .nUsercode = nUsercode

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
            InspostCR782Upd = Add(lintAction)
        End With

InspostCR782_Err:
        If Err.Number Then
            InspostCR782Upd = False
        End If
        On Error GoTo 0
    End Function
End Class

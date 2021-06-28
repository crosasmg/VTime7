Option Strict Off
Option Explicit On
Public Class Tab_Catevent
    Public nIdcatas As Double 'Number(10)                        Not Null,
    Public nNumber As Integer 'Number(5)                         Not Null,
    Public nType As Integer 'Number(5)                         Not Null,
    Public nType_Rel As Integer 'Number(5)                         Not Null,
    Public nBranch As Integer 'Number(5)                         Not Null,
    Public dCompdate As Date    '                          Not Null,
    Public sDescript As String  'Char(30 Byte)                     Not Null,
    Public sShort_Des As String  'Char(12 Byte)                     Not Null,
    Public sStatregt As String  'Char(1 Byte)                      Not Null,
    Public nUsercode As Integer

    Public Function Add(ByVal nAction As Integer)
        Dim lreccreTab_Catevent As eRemoteDB.Execute

        On Error GoTo creTab_Catevent_Err

        lreccreTab_Catevent = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure creTab_Catevent al 04-09-2002 12:49:14
        '+
        With lreccreTab_Catevent
            .StoredProcedure = "insTab_Catevent"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIdcatas", nIdcatas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_Rel", nType_Rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sShort_Des", sShort_Des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 13, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

creTab_Catevent_Err:
        If Err.Number Then
            Add = False
        End If
        'UPGRADE_NOTE: Object lreccreTab_Catevent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreTab_Catevent = Nothing
        On Error GoTo 0

    End Function
    Public Function Find_Claim(ByVal nIdCatas As Double) As Boolean
        Dim lreccreTab_Catevent As eRemoteDB.Execute

        On Error GoTo Find_Claim_Err

        lreccreTab_Catevent = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure creTab_Catevent al 04-09-2002 12:49:14
        '+
        Find_Claim = False
        With lreccreTab_Catevent
            .StoredProcedure = "REATAB_CATEVENT_CLAIM"
            .Parameters.Add("nIdcatas", nIdCatas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_Claim = True
            End If
        End With

Find_Claim_Err:
        If Err.Number Then
            Find_Claim = False
        End If
        'UPGRADE_NOTE: Object lreccreTab_Catevent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreTab_Catevent = Nothing
        On Error GoTo 0
    End Function
    Public Function Find_v(ByVal nIdCatas As Double) As Integer
        Dim lreccreTab_Catevent As eRemoteDB.Execute

        On Error GoTo Find_v_Err

        lreccreTab_Catevent = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure creTab_Catevent al 04-09-2002 12:49:14
        '+
        Find_v = False
        With lreccreTab_Catevent
            .StoredProcedure = "ReaTab_Catevent_v"
            .Parameters.Add("nIdcatas", nIdCatas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_v = True
            End If
        End With

Find_v_Err:
        If Err.Number Then
            Find_v = False
        End If
        'UPGRADE_NOTE: Object lreccreTab_Catevent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreTab_Catevent = Nothing
        On Error GoTo 0

    End Function
    Public Function InspostSI040Upd(ByVal sAction As String, ByVal nIdCatas As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nNumber As Double, ByVal sStatregt As String, ByVal nBranch_rei As Double, ByVal nType As Double, ByVal nType_rel As Integer, ByVal nUsercode As Integer) As Object
        Dim lintAction As Integer

        On Error GoTo InspostSI040_Err

        With Me
            .nBranch = nBranch_rei
            .nType = nType
            .nType_Rel = nType_rel
            .nNumber = nNumber
            .nUsercode = nUsercode
            .nIdcatas = nIdCatas
            .sDescript = sDescript
            .sShort_Des = sShort_des
            .sStatregt = sStatregt

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
            InspostSI040Upd = Add(lintAction)
        End With

InspostSI040_Err:
        If Err.Number Then
            InspostSI040Upd = False
        End If
        On Error GoTo 0
    End Function
    Public Function InsValMSI040_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nIdcatas As Double, ByVal nNumber As Integer, ByVal nType As Integer, ByVal nType_Rel As Integer, _
                                   ByVal nBranch As Integer, ByVal sDescript As String, ByVal sShort_Des As String, ByVal sStatregt As String, ByVal nUsercode As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsContrproc As eCoReinsuran.Contrnpro

        Dim lintError As Long
        lclsContrproc = New eCoReinsuran.Contrnpro
        lclsErrors = New eFunctions.Errors

        On Error GoTo InsValMSI040_K_Err

        '+ Se valida el ramo del reaseguro
        If sAction = "Del" Then
            If nIdcatas = eRemoteDB.Constants.intNull Or nIdcatas = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 978991)
            Else
                If Find_Claim(nIdcatas) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 978994)
                End If
            End If
        Else
            If nIdcatas = eRemoteDB.Constants.intNull Or nIdcatas = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 978991)
            Else
                If Find_v(nIdcatas) Then
                    If sAction = "Add" Then
                        Call lclsErrors.ErrorMessage(sCodispl, 978992)
                    End If
                Else
                    If sAction <> "Add" Then
                        Call lclsErrors.ErrorMessage(sCodispl, 978993)
                    End If
                End If
            End If


            If nType = eRemoteDB.Constants.intNull Or nType = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 6018)
                lintError = 1
            End If

            If nType_Rel = eRemoteDB.Constants.intNull Or nType_Rel = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 6090)
                lintError = 1
            End If

            If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 60314)
                lintError = 1
            End If

            If nNumber = eRemoteDB.Constants.intNull Or nNumber = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 3357)
                lintError = 1
            Else
                If lintError <> 1 Then
                    If Not lclsContrproc.Find(nNumber, nType, nBranch, Today) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 21002)
                    End If
                End If
            End If

            If sDescript = vbNullString Or sDescript = "" Then
                Call lclsErrors.ErrorMessage(sCodispl, 10005)
            End If

            If sShort_Des = vbNullString Or sShort_Des = "" Then
                Call lclsErrors.ErrorMessage(sCodispl, 10006)
            End If

            If sStatregt = vbNullString Or sStatregt = "" Then
                Call lclsErrors.ErrorMessage(sCodispl, 9089)
            End If
        End If
                InsValMSI040_K = lclsErrors.Confirm
                lclsErrors = Nothing
InsValMSI040_K_Err:
                If Err.Number Then
                    InsValMSI040_K = InsValMSI040_K & Err.Description
                End If
                On Error GoTo 0
    End Function


End Class

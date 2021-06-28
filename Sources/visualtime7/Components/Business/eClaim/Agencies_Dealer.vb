Option Strict Off
Option Explicit On
Public Class Agencies_Dealer

    Public nAgendealer As Integer
    Public sAgendealerdesc As String
    Public sClient_dealer As String

    Public dCompdate As Date    '                          Not Null,
    Public nUsercode As Integer

    Public Function Add(ByVal nAction As Integer)
        Dim lreccreAgencies_Dealer As eRemoteDB.Execute

        On Error GoTo creAgencies_Dealer_Err

        lreccreAgencies_Dealer = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure creAgencies_Dealer al 04-09-2002 12:49:14
        '+
        With lreccreAgencies_Dealer
            .StoredProcedure = "insAgencies_Dealer"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgendealer", nAgendealer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAgendealerdesc", sAgendealerdesc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 90, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient_dealer", sClient_dealer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

creAgencies_Dealer_Err:
        If Err.Number Then
            Add = False
        End If
        'UPGRADE_NOTE: Object lreccreAgencies_Dealer may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreAgencies_Dealer = Nothing
        On Error GoTo 0

    End Function
    Public Function Find_Claim(ByVal nAgendealer As Double) As Boolean
        Dim lreccreAgencies_Dealer As eRemoteDB.Execute

        On Error GoTo Find_Claim_Err

        lreccreAgencies_Dealer = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure creAgencies_Dealer al 04-09-2002 12:49:14
        '+
        Find_Claim = False
        With lreccreAgencies_Dealer
            .StoredProcedure = "REAAgencies_Dealer_CLAIM"
            .Parameters.Add("nAgendealer", nAgendealer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_Claim = True
            End If
        End With

Find_Claim_Err:
        If Err.Number Then
            Find_Claim = False
        End If
        'UPGRADE_NOTE: Object lreccreAgencies_Dealer may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreAgencies_Dealer = Nothing
        On Error GoTo 0
    End Function
    Public Function Find_v(ByVal nAgendealer As Double) As Integer
        Dim lreccreAgencies_Dealer As eRemoteDB.Execute

        On Error GoTo Find_v_Err

        lreccreAgencies_Dealer = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure creAgencies_Dealer al 04-09-2002 12:49:14
        '+
        Find_v = False
        With lreccreAgencies_Dealer
            .StoredProcedure = "ReaAgencies_Dealer_v"
            .Parameters.Add("nIdcatas", nAgendealer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_v = True
            End If
        End With

Find_v_Err:
        If Err.Number Then
            Find_v = False
        End If
        'UPGRADE_NOTE: Object lreccreAgencies_Dealer may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreAgencies_Dealer = Nothing
        On Error GoTo 0

    End Function
    Public Function InspostSI041Upd(ByVal sAction As String, ByVal nAgendealer As Integer, ByVal sAgendealerdesc As String, ByVal sClient_dealer As String, ByVal nUsercode As Integer) As Object
        Dim lintAction As Integer

        On Error GoTo InspostSI040_Err

        With Me

            .nAgendealer = nAgendealer
            .sAgendealerdesc = sAgendealerdesc
            .sClient_dealer = sClient_dealer
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
            InspostSI041Upd = Add(lintAction)
        End With

InspostSI040_Err:
        If Err.Number Then
            InspostSI041Upd = False
        End If
        On Error GoTo 0
    End Function
    Public Function InsValMSI041_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nAgendealer As Integer, ByVal sAgendealerdesc As String, ByVal sClient_dealer As String, ByVal nUsercode As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsContrproc As eCoReinsuran.Contrnpro

        lclsContrproc = New eCoReinsuran.Contrnpro
        lclsErrors = New eFunctions.Errors

        '+ Se valida el ramo del reaseguro
        If sAction = "Del" Then
            If nAgendealer = eRemoteDB.Constants.intNull Or nAgendealer = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 12089)
            Else
                If Find_Claim(nAgendealer) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 12089)
                End If
            End If
        Else
            If nAgendealer = eRemoteDB.Constants.intNull Or nAgendealer = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 55689)
            Else
                If Find_v(nAgendealer) Then
                    If sAction = "Add" Then
                        Call lclsErrors.ErrorMessage(sCodispl, 12089)
                    End If
                Else
                    If sAction <> "Add" Then
                        Call lclsErrors.ErrorMessage(sCodispl, 12089)
                    End If
                End If
            End If


            If sAgendealerdesc = vbNullString Or sAgendealerdesc = "" Then
                Call lclsErrors.ErrorMessage(sCodispl, 55537)
            End If

            If sClient_dealer = vbNullString Or sClient_dealer = "" Then
                Call lclsErrors.ErrorMessage(sCodispl, 55537)
            End If

           
        End If

        InsValMSI041_K = lclsErrors.Confirm
        lclsErrors = Nothing
    End Function
End Class

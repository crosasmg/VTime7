Option Strict Off
Option Explicit On
Public Class Tab_Settlement

    Public nId_Settle As Integer
    Public nBranch As Integer
    Public nProduct As Integer
    Public sFormatname As String
    Public nUsercode As Integer
    Public sAction As String
    Public nCausecod As Integer
    Public sClaimTyp As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
    Public sDescript As String 'char                                                                                                                             no                                  30                      yes                                 no                                  yes
    Public sShort_des As String 'char 
    '+Variables para la DP7002
    Public sSel As String
    Public nOrder As Integer
    Public nCover As Integer
    Public nType_settle As Integer

    '%Función para Elmininar registros de Tab_Settlement
    Public Function Delete_MSI7000(ByVal nId_settle As Integer) As Boolean
        Dim lrecdelClaim_SetleMent As eRemoteDB.Execute

        lrecdelClaim_SetleMent = New eRemoteDB.Execute

        With lrecdelClaim_SetleMent
            .StoredProcedure = "delTab_SettleMent"
            .Parameters.Add("nId_settle", nId_Settle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Delete_MSI7000 = .Run(False)
        End With

        lrecdelClaim_SetleMent = Nothing
    End Function

    '% Función para validar MSI7000

    Public Function insValMSI7000(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal sFormatname As String, ByVal nProduct As Integer, ByVal sDescript As String, ByVal nType_settle As Integer)
        Dim lclsErrors As eFunctions.Errors
        Dim lclsClaim_caus As eClaim.Tab_Settlement
        Dim lclsClaim_SetleMent As eClaim.Tab_Settlements
        Dim lclsValues As New eFunctions.Values
        Dim bError As Boolean = False

        lclsErrors = New eFunctions.Errors
        lclsClaim_caus = New eClaim.Tab_Settlement
        lclsClaim_SetleMent = New eClaim.Tab_Settlements

        On Error GoTo insValMSI7000_Err

        If nBranch = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 1022)
            bError = True
        End If

        If sDescript = "" Or IsNothing(sDescript) Then
            Call lclsErrors.ErrorMessage(sCodispl, 10071)
            bError = True
        End If

        If sFormatname = "" Or IsNothing(sFormatname) Then
            Call lclsErrors.ErrorMessage(sCodispl, 900037, , , "Rutina")
            bError = True
        End If

        If bError = False _
        And (nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull) Then
            If lclsClaim_SetleMent.MSI7000_Find(nBranch, , sFormatname, nType_settle) _
            And sAction <> "Update" Then
                Call lclsErrors.ErrorMessage(sCodispl, 900036)
                bError = True
            End If
        End If

        If bError = False And nProduct > 0 Then
            If lclsClaim_SetleMent.MSI7000_Find(nBranch, nProduct, sFormatname, nType_settle) _
            And sAction <> "Update" Then
                Call lclsErrors.ErrorMessage(sCodispl, 900036)
            End If
        End If

        'If bError = False _
        'And ((lclsClaim_SetleMent.MSI7000_Find(nBranch, , sFormatname, nType_settle) _
        '        Or lclsClaim_SetleMent.MSI7000_Find(nBranch, nProduct, sFormatname, nType_settle)) _
        'And sAction <> "Update") Then
        '    Call lclsErrors.ErrorMessage(sCodispl, 900036)
        'End If

        insValMSI7000 = lclsErrors.Confirm
        lclsErrors = Nothing
        lclsClaim_caus = Nothing

insValMSI7000_Err:
        If Err.Number Then
            insValMSI7000 = lclsErrors.Confirm & Err.Description
        End If
        On Error GoTo 0
    End Function

    Public Function insPostMSI7000(ByVal sAction As String, ByVal nId_settle As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sDescript As String, ByVal sFormatname As String, ByVal nUsercode As Integer, ByVal nType_settle As Integer) As Boolean

        On Error GoTo insPostMSI010_err

        Me.nBranch = nBranch
        Me.nId_Settle = nId_settle
        Me.nProduct = nProduct
        Me.sDescript = sDescript
        Me.sFormatname = sFormatname
        Me.nUsercode = nUsercode
        Me.nType_settle = nType_settle
        Me.sAction = sAction.ToUpper

        insPostMSI7000 = True
        insPostMSI7000 = Claim_Settlement()

insPostMSI010_err:
        If Err.Number Then
            insPostMSI7000 = False
        End If
        On Error GoTo 0

    End Function

    Public Function Claim_Settlement() As Boolean
        Dim lreccreClaim_Settlement As eRemoteDB.Execute

        lreccreClaim_Settlement = New eRemoteDB.Execute

        With lreccreClaim_Settlement
            .StoredProcedure = "InsTab_SettleMent"
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)            
            .Parameters.Add("nId_settle", nId_settle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 80, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFormatname", sFormatname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 80, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)            
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_settle", nType_settle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Claim_Settlement = .Run(False)

        End With
        lreccreClaim_Settlement = Nothing

    End Function

End Class

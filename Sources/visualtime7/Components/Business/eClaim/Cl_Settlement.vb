Option Strict Off
Option Explicit On
Public Class Cl_Settlement

    Public nId_Settle As Integer
    Public nBranch As Integer
    Public nProduct As Integer
    Public sFormatname As String
    Public nUsercode As Integer
    Public sAction As String
    Public nCausecod As Integer
    Public sClaimTyp As String
    Public sDescript As String
    Public sShort_des As String
    '+Variables para la DP7002
    Public sSel As String
    Public nOrder As Integer
    Public nCover As Integer
    Public nModulec As Integer
    Public nCase_num As Integer
    Public nDeman_type As Integer
    Public nClaim As Integer
    Public sCover As String
    '%Función para Elmininar registros de Tab_Settlement
    Public Function Delete_SI764(ByVal nId_settle As Integer, ByVal nClaim As Integer, ByVal nDeman_type As Integer, ByVal nCase_num As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
        Dim lrecdelClaim_SetleMent As eRemoteDB.Execute

        lrecdelClaim_SetleMent = New eRemoteDB.Execute

        With lrecdelClaim_SetleMent
            .StoredProcedure = "delCl_SettleMent"
            .Parameters.Add("nId_settle", nId_settle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Delete_SI764 = .Run(False)
        End With

        lrecdelClaim_SetleMent = Nothing
    End Function

    Public Function insPostSI764(ByVal sAction As String, ByVal nId_settle As Integer, ByVal nClaim As Integer, ByVal nDeman_type As Integer, ByVal nCase_num As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nUsercode As Integer) As Boolean

        On Error GoTo insPostSI764_err

        Me.nId_Settle = nId_settle
        Me.nClaim = nClaim
        Me.nDeman_type = nDeman_type
        Me.nCase_num = nCase_num
        Me.nModulec = nModulec
        Me.nCover = nCover
        Me.nUsercode = nUsercode
        Me.sAction = sAction.ToUpper

        insPostSI764 = True

        insPostSI764 = Claim_Settlement()

insPostSI764_err:
        If Err.Number Then
            insPostSI764 = False
        End If
        On Error GoTo 0

    End Function

    Public Function Claim_Settlement() As Boolean
        Dim lreccreClaim_Settlement As eRemoteDB.Execute

        lreccreClaim_Settlement = New eRemoteDB.Execute

        With lreccreClaim_Settlement
            .StoredProcedure = "InsCl_SettleMent"
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId_settle", nId_Settle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Claim_Settlement = .Run(False)

        End With
        lreccreClaim_Settlement = Nothing

    End Function

End Class

Option Explicit On
Public Class Dynamics_Table_Certificat
    Public sCertype As String
    Public nBranch As Long
    Public nProduct As Long
    Public nPolicy As Double
    Public nCertif As Double
    Public nSheet As Long
    Public nField As Long
    Public dEffecdate As Date
    Public dNulldate As Date
    Public sValue As String
    Public nValue As Double
    Public dValue As Date
    Public nUsercode As Long
 Public Function Find_date(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Double, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nSheet As Long, ByVal nField As Long, ByVal dEffecdate As Date) As Boolean
        Dim lrecreaFund_distribution_1 As eRemoteDB.Execute

        lrecreaFund_distribution_1 = New eRemoteDB.Execute

        On Error GoTo Find_date_Err

        Find_date = True

        With lrecreaFund_distribution_1
            .StoredProcedure = "reaDynamics_Table_Certificat_f"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nField", nField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            If .Run Then
                sValue = .FieldToClass("sValue")
                nValue = .FieldToClass("nValue")
                .RCloseRec()
            Else
                Find_date = False
            End If
        End With

        'UPGRADE_NOTE: Object lrecreaFund_distribution_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaFund_distribution_1 = Nothing

Find_date_Err:
        If Err.Number Then
            Find_date = False
        End If
    End Function
End Class

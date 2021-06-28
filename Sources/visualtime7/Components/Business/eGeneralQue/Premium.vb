Option Strict Off
Option Explicit On
Friend Class Premium
    '%-------------------------------------------------------%'
    '% $Workfile:: Premium.cls                             $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:21p                                $%'
    '% $Revision:: 6                                        $%'
    '%-------------------------------------------------------%'

    '**% Find. This function is used for reading operations depending on the type of folder that called it.
    '%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
    '%tipo de carpeta que la invoco.
    Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
        Dim caseAux As Object = New eRemoteDB.Execute
        Select Case nParentFolder
            '+ Saldos de una poliza
            Case 1
                caseAux = insReaPremium_Move_Acc((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("HdEffecdate").Valor))
        End Select
        Return caseAux
    End Function
    '**% MISSING
    '% MISSING
    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    Private Function insReaPremium_Move_Acc(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, Optional ByRef ldtmEffecdate As Object = Nothing) As eRemoteDB.Execute

        insReaPremium_Move_Acc = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatPremiumPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatPremiumPol'
        '**+ Information read on December 02,1999  09:57:49 a.m.
        '+Información leída el 02/12/1999 09:57:49 a.m.

        With insReaPremium_Move_Acc
            .StoredProcedure = "QuedatPremium_Move_Acc"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                'UPGRADE_NOTE: Object insReaPremium_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaPremium_Move_Acc = Nothing
            End If
        End With
    End Function
End Class







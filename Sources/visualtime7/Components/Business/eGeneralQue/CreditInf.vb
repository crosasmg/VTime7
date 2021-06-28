Option Strict Off
Option Explicit On
Friend Class CreditInf
    '%-------------------------------------------------------%'
    '% $Workfile:: CreditInf.cls                            $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:21p                                $%'
    '% $Revision:: 3                                        $%'
    '%-------------------------------------------------------%'

    '**% Find: This function is used for read operations depending on the type of folder that called it.
    '%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
    '%tipo de carpeta que la invoco.
    Public Function Find(ByRef nParentFolder As Short, ByRef Parameters As Properties) As eRemoteDB.Execute
        Dim caseAux As eRemoteDB.Execute = New eRemoteDB.Execute
        Select Case nParentFolder
            Case 1, 11, 5 '- Coberturas de la póliza/Cotización/solicitud
                caseAux = insReaCreditInfsPol((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), (Parameters("HdEffecdate").Valor))
        End Select
        Return caseAux
    End Function

    '**% insReaCreditInfsPol. This function returns the clients of a policy that receipt as a parameter
    '%insREaCreditInfsPol. esta funcion retorna los Clientes de una póliza que recibe como parámetro
    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    Private Function insReaCreditInfsPol(ByRef lstrCertype As String, ByRef lintBranch As Short, ByRef lintProduct As Short, ByRef llngPolicy As Double, Optional ByRef llngCertif As Integer = 0, Optional ByRef ldtmEffecdate As Date = eRemoteDB.Constants.dtmNull) As eRemoteDB.Execute

        insReaCreditInfsPol = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatClientPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatClientPol'
        '**+ Information read on November 25, 1999  02:52:20 p.m.
        '+Información leída el 25/11/1999 02:52:20 p.m.

        With insReaCreditInfsPol
            .StoredProcedure = "queDatCreditInfPol"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaCreditInfsPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaCreditInfsPol = Nothing
            End If
        End With
    End Function
End Class







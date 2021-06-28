Option Strict Off
Option Explicit On
Friend Class FiscalResidencePol
    '%-------------------------------------------------------%'
    '% $Workfile:: FiscalResidencePol.cls                   $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:21p                                $%'
    '% $Revision:: 6                                        $%'
    '%-------------------------------------------------------%'

    '**% Find. Use this function to obtain the data of the policy
    '%Find. Se utiliza esta funcion para obtener los datos de la póliza
    Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
        If nParentFolder <> 0 Then
            Find = insReaPolicy(Parameters("sCertype").Valor, Parameters("nBranch").Valor, Parameters("nProduct").Valor, Parameters("nPolicy").Valor, IIf(nParentFolder = 1, 0, Parameters("nCertif").Valor), Parameters("HdEffecdate").Valor)
        Else
            'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            Find = Nothing
        End If
    End Function

    '**% insreaPolicy. This function returns the recharge and discounts of a policy/certificate.
    '%insreaPolicy. Esta función se encarga de devolver los recargos y descuentos de una
    '% póliza/certificado.
    Private Function insReaPolicy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Integer, ByVal dEffecdate As Object) As eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.quedatPolicy'
        '+Definición de parámetros para stored procedure 'insudb.quedatPolicy'
        '**+ Information read on December 03,1999 02:08:30 p.m.
        '+Información leída el 03/12/1999 02:08:30 p.m.
        insReaPolicy = New eRemoteDB.Execute
        With insReaPolicy
            .StoredProcedure = "quedatInfCtaCte"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaPolicy = Nothing
            End If
        End With
    End Function
End Class







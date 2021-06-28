Option Strict Off
Option Explicit On
Public Class Funds
    '%-------------------------------------------------------%'
    '% $Workfile:: Funds.cls                                $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:21p                                $%'
    '% $Revision:: 4                                        $%'
    '%-------------------------------------------------------%'

    '**% Find. Use this function to obtain the data of the policy
    '%Find. Se utiliza esta funcion para obtener los datos de la póliza
    Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
        Dim caseAux As eRemoteDB.Execute = New eRemoteDB.Execute
        Select Case nParentFolder
            Case 1
                caseAux = insReaFunds((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), IIf(Parameters("nCertif").Valor <> "" And Parameters("nCertif").Valor <> String.Empty, Parameters("nCertif").Valor, 0), (Parameters("HdEffecdate").Valor))
            Case 72
                caseAux = insReaFunds((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), IIf(Parameters("nCertif").Valor <> "" And Parameters("nCertif").Valor <> String.Empty, Parameters("nCertif").Valor, 0), (Parameters("HdEffecdate").Valor),  , Parameters("nOrigin").Valor)
            Case Else
                If nParentFolder <> 0 Then
                    caseAux = insReaFunds((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), IIf(Parameters("nCertif").Valor <> "" And Parameters("nCertif").Valor <> String.Empty, Parameters("nCertif").Valor, 0), (Parameters("HdEffecdate").Valor), Parameters("nFunds").Valor, Parameters("nOrigin").Valor)
                End If
        End Select
        Return caseAux
    End Function

    '**% insReaFunds. This function returns the funds of a policy.
    '%insReaFunds. Esta función se encarga de devolver los fondos de una póliza.
    Private Function insReaFunds(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, ByRef llngCertif As Integer, ByRef ldmtEffecdate As Object, Optional ByRef lintFunds As Integer = 0, Optional ByRef lintOrigin As Integer = 0) As eRemoteDB.Execute

        insReaFunds = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatFundsPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatFundsPol'
        '**+ Information read on December 03,1999 02:08:30 p.m.
        '+Información leída el 03/12/1999 02:08:30 p.m.

        With insReaFunds
            .StoredProcedure = "queDatFundsPol"

            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldmtEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", lintFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", lintOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaFunds = Nothing
            End If
        End With
    End Function
End Class







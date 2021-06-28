Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Origin_NET.Origin")> Public Class Origin
	
	'%-------------------------------------------------------%'
	'% $Workfile:: Origins.cls                              $%'
	'% $Author:: Gletelier                                  $%'
	'% $Date:: 22/10/09 6:24p                               $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find. Use this function to obtain the data of the policy
	'%Find. Se utiliza esta funcion para obtener los datos de la póliza
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		If Parameters("nCurrentFolder").Valor = 2000 Then
			Find = insReaFundsMirror((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), IIf(Parameters("nCertif").Valor <> "" And Parameters("nCertif").Valor <> String.Empty, Parameters("nCertif").Valor, 0), (Parameters("HdEffecdate").Valor))
		ElseIf Parameters("nCurrentFolder").Valor = 2002 Then 
			Find = InsReaReceptionPol(Parameters("sCertype").Valor, Parameters("nBranch").Valor, Parameters("nProduct").Valor, Parameters("nPolicy").Valor, Parameters("nCertif").Valor, Parameters("HdEffecdate").Valor)
		Else
			Find = insReaFunds((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), IIf(Parameters("nCertif").Valor <> "" And Parameters("nCertif").Valor <> String.Empty, Parameters("nCertif").Valor, 0), (Parameters("HdEffecdate").Valor))
		End If
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
            .StoredProcedure = "queDatFundsPolOrigin"

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

    '**% insReaFunds. This function returns the funds of a policy.
    '%insReaFunds. Esta función se encarga de devolver los fondos de una póliza.
    Private Function insReaFundsMirror(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, ByRef llngCertif As Integer, ByRef ldmtEffecdate As Object, Optional ByRef lintFunds As Integer = 0, Optional ByRef lintOrigin As Integer = 0) As eRemoteDB.Execute

        insReaFundsMirror = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatFundsPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatFundsPol'
        '**+ Information read on December 03,1999 02:08:30 p.m.
        '+Información leída el 03/12/1999 02:08:30 p.m.

        With insReaFundsMirror
            .StoredProcedure = "queDatFundsPolOriginMirror"

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
                'UPGRADE_NOTE: Object insReaFundsMirror may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaFundsMirror = Nothing
            End If
        End With
    End Function

    'InsReaReceptionPol: Retorna los rescates realizados a la póliza/certificado
    Private Function InsReaReceptionPol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Integer, ByVal dEffecdate As Date) As eRemoteDB.Execute
        On Error GoTo InsReaReceptionPol_Err
        InsReaReceptionPol = New eRemoteDB.Execute

        With InsReaReceptionPol
            .StoredProcedure = "QueDatReceptionPol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                'UPGRADE_NOTE: Object InsReaReceptionPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                InsReaReceptionPol = Nothing
            End If
        End With
InsReaReceptionPol_Err:
        If Err.Number Then
            'UPGRADE_NOTE: Object InsReaReceptionPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            InsReaReceptionPol = Nothing
        End If
    End Function
End Class







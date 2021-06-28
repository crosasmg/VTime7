Option Strict Off
Option Explicit On
Friend Class History
	'**% Find. This function is used to obtain the clauses.
	'%Find. Se utiliza esta funcion para obtener las clausulas
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		'    if lHeaderProperties("nCurrentFolder").Valor =
		Select Case nParentFolder
			'- Historia del siniestro
			Case 6
				Find = insReaClaim_his((Parameters("nClaim").Valor))
				'- Historia del caso
			Case 24
				Find = insReaClaim_his((Parameters("nClaim").Valor), (Parameters("nCase_num").Valor), (Parameters("nDeman_type").Valor))
				'- Historia de la Poliza/Certificado, Cotización, Solicitud
			Case 1, 3, 5, 11
				Find = insReaPolicy_his((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), (Parameters("HdEffecdate").Valor))
				
				'**+ Funds Transactions.
				'+ Movimientos de los Fondos.
				
			Case 70
				Find = insReaMove_Funds((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), IIf(Parameters("nCertif").Valor <> "" And Parameters("nCertif").Valor <> String.Empty, Parameters("nCertif").Valor, 0), (Parameters("nFunds").Valor), Parameters("nOrigin").Valor, (Parameters("dEffecdate").Valor), (Parameters("HdEffecdate").Valor))
				
			Case Else
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
		End Select
	End Function
	
	'**% insReaClaim_his. This function returns the clients associated to a claim.
	'%insReaClaim_his. esta funcion retorna los Clientes asociados a  un siniestro
	Private Function insReaClaim_his(ByRef llngClaim As Double, Optional ByRef lintCase_num As Integer = -1, Optional ByRef lintDeman_Type As Integer = -1) As eRemoteDB.Execute
		
		insReaClaim_his = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatClaim_his'
		'+Definición de parámetros para stored procedure 'insudb.queDatClaim_his'
		'**+ Information read on December 06,1999  02:52:20 p.m.
		'+Información leída el 06/12/1999 02:52:20 p.m.
		
		With insReaClaim_his
			.StoredProcedure = "queDatClaim_his"
			.Parameters.Add("nClaim", llngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCase_num", IIf(lintCase_num = -1, System.DBNull.Value, lintCase_num), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nDeman_type", IIf(lintDeman_Type = -1, System.DBNull.Value, lintDeman_Type), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object insReaClaim_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaClaim_his = Nothing
			End If
		End With
	End Function
	
	'---------------------- Funciones de Póliza ----------------------------------------------
	'**% MISSING
	'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    Function insReaPolicy_his(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, ByRef llngCertif As Integer, Optional ByRef ldtmEffecdate As Object = Nothing) As eRemoteDB.Execute
        insReaPolicy_his = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatClient'
        '+Definición de parámetros para stored procedure 'insudb.queDatClientPol'
        '**+ Information read on June 26,2000  05:06:20 p.m.
        '+Información leída el 26/06/2000 05:06:20 p.m.

        With insReaPolicy_his
            .StoredProcedure = "queDatPolicy_his"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaPolicy_his = Nothing
            End If
        End With
    End Function

    '**%insReaMove_Funds: Read the funds transactions of the policy.
    '% insReaMove_Funds: Lee los movimiento de los fondos de una póliza.
    Function insReaMove_Funds(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, ByRef llngCertif As Integer, ByRef lintnFunds As Integer, ByVal lintOrigin As Integer, ByRef ldtmEffecdate As Object, ByRef ldtmEffecdate2 As Object) As eRemoteDB.Execute
        insReaMove_Funds = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatMoveFund'
        '+ Definición de parámetros para stored procedure 'insudb.queDatMoveFund'

        With insReaMove_Funds
            .StoredProcedure = "queDatMoveFund"

            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", lintnFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", lintOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOperdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaMove_Funds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaMove_Funds = Nothing
            End If
        End With
    End Function
End Class







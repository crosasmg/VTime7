Option Strict Off
Option Explicit On
Friend Class Claims
	'%-------------------------------------------------------%'
	'% $Workfile:: Claims.cls                               $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**% find: This function is used to obtain the links and load them to the properties collectio
	'%Find. Se utiliza esta funcion para obtener los nexos y cargarlo a la colección
	'%de propiedades
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			Case 4
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				Find = insReaClaimsCli((Parameters("sClient").Valor), System.DBNull.Value)
			Case 1, 3, 60
				Find = insReaClaimsPol((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), (Parameters("HdEffecdate").Valor))
			Case 6 ' Siniestros
				Find = insReaClaim_o((Parameters("nClaim").Valor))
			Case Else
				If nParentFolder = 0 Then
					Find = insReaClaim_o((Parameters("HnClaim").Valor))
				Else
					'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					Find = Nothing
				End If
		End Select
	End Function
	
	'**% insReaClaimsCli. This function restores the claims of a client (passed as a parameter)
	'%insReaClaimsCli. Esta Función devuelve los siniestros de un cliente (pasado como parametro)
	Private Function insReaClaimsCli(ByRef lstrClient As String, ByRef ldtmOccurdat As Object) As eRemoteDB.Execute
		
		insReaClaimsCli = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatClaimCli'
		'+Definición de parámetros para stored procedure 'insudb.queDatClaimCli'
		'**+ Information read on Novemebr 29, 1999  03:01:50 p.m.
		'+Información leída el 29/11/1999 03:01:50 p.m.
		
		With insReaClaimsCli
			.StoredProcedure = "queDatClaimCli"
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOccurDat", ldtmOccurdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaClaimsCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaClaimsCli = Nothing
			End If
		End With
	End Function
	
	'**% InsReaClaims_o. This function is in charge of making the reading of just one claim.
	'%InsReaClaims_o. Esta funcion se encarge de realizar la lectura de un sólo siniestro
	Private Function insReaClaim_o(ByRef llngClaim As Double) As eRemoteDB.Execute
		insReaClaim_o = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatClaim'
		'+Definición de parámetros para stored procedure 'insudb.queDatClaim'
		'**+ Information read on December 03, 1999  11:28:28 a.m.
		'+Información leída el 03/12/1999 11:28:28 a.m.
		
		With insReaClaim_o
			.StoredProcedure = "queDatClaim"
			.Parameters.Add("nClaim", llngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaClaim_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaClaim_o = Nothing
			End If
		End With
	End Function
	
	'**% insReaClaimsPol. This function restores the claims associated to a specific policy
	'%insReaClaimsPol. Esta funcion devuelve los siniestros asociados a una determinada póliza
	'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    Private Function insReaClaimsPol(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, ByRef llngCertif As Integer, Optional ByRef ldtmEffecdate As Object = Nothing) As eRemoteDB.Execute

        insReaClaimsPol = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatClaimPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatClaimPol'
        '**+ Information read on December 14, 1999 05:04:39 p.m.
        '+Información leída el 14/12/1999 05:04:39 p.m.

        With insReaClaimsPol
            .StoredProcedure = "queDatClaimPol"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaClaimsPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaClaimsPol = Nothing
            End If
        End With

    End Function
End Class







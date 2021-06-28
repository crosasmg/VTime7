Option Strict Off
Option Explicit On
Friend Class Cheques
	'%-------------------------------------------------------%'
	'% $Workfile:: Cheques.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find: Use this function to obtain the links and load them to the properties collection
	'%Find. Se utiliza esta funcion para obtener los nexos y cargarlo a la colección
	'%de propiedades
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			
			'-Cheques de un cliente
			Case 4, 40
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				Find = insReaChequesCli((Parameters("sClient").Valor), System.DBNull.Value)
			'-Cheques de un siniestro
			Case 6
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				Find = insReaChequesClaim((Parameters("nClaim").Valor), System.DBNull.Value)
			Case Else
				If CDbl(0 & nParentFolder) = 0 Then
					Find = insReaCheques_o((Parameters("HsCheque").Valor))
				Else
					'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					Find = Nothing
				End If
		End Select
	End Function
	
	'**% insReaChequesCli. This function restores a client's claims (passed as a parameter)
	'%insReaChequesCli. Esta Función devuelve los siniestros de un cliente (pasado como parametro)
	Private Function insReaChequesCli(ByRef lstrClient As String, ByRef ldtmIssue_dat As Object) As eRemoteDB.Execute
		
		insReaChequesCli = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatChequesCli'
		'+Definición de parámetros para stored procedure 'insudb.queDatChequesCli'
		'**+ Information read on November 29, 1999  03:01:50 p.m.
		'+Información leída el 29/11/1999 03:01:50 p.m.
		
		With insReaChequesCli
			.StoredProcedure = "queDatChequesCli"
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssue_dat", ldtmIssue_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaChequesCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaChequesCli = Nothing
			End If
		End With
	End Function
	
	'**% insReaChequesClaim. This function restores a client's claims (passed as a parameter)
	'%insReaChequesClaim. Esta Función devuelve los siniestros de un cliente (pasado como parametro)
	Private Function insReaChequesClaim(Byval ldblClaim As Double, Byval ldtmIssue_dat As Object) As eRemoteDB.Execute
		
		insReaChequesClaim = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatChequesClaim'
		'+Definición de parámetros para stored procedure 'insudb.queDatChequesClaim'
		'**+ Information read on November 29, 1999  03:01:50 p.m.
		'+Información leída el 29/11/1999 03:01:50 p.m.
		
		With insReaChequesClaim
			.StoredProcedure = "queDatChequesClaim"
            .Parameters.Add("nClaim", ldblClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaChequesClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaChequesClaim = Nothing
			End If
		End With
	End Function

	'**% InsReaCheques_o. This function reads just one claim.
	'%InsReaCheques_o. Esta funcion se encarge de realizar la lectura de un sólo siniestro
	Private Function insReaCheques_o(ByRef lstrCheques As String) As eRemoteDB.Execute
		insReaCheques_o = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatCheques'
		'+Definición de parámetros para stored procedure 'insudb.queDatCheques'
		'**+ Information read on December 03, 1999  11:28:28 a.m.
		'+Información leída el 03/12/1999 11:28:28 a.m.
		
		With insReaCheques_o
			.StoredProcedure = "queDatCheques"
			.Parameters.Add("nCheques", lstrCheques, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaCheques_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaCheques_o = Nothing
			End If
		End With
	End Function
End Class







Option Strict Off
Option Explicit On
Friend Class Contrats
	'%-------------------------------------------------------%'
	'% $Workfile:: Contrats.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find. This function is used to obtain the contracts depending on the folder that calls them.
	'%Find. Se utiliza esta funcion para obtener los contratos depediendo de la carpeta que
	'%lo llama.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			Case 4 '-Contratos de un cliente
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				Find = insReaFincance_coCli((Parameters("sClient").Valor), System.DBNull.Value)
				
			Case Else
				If nParentFolder = 0 Then
					Find = insReaFincance_co_o((Parameters("HnContrat").Valor))
				Else
					'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					Find = Nothing
				End If
		End Select
	End Function
	
	'**% insReaFinance_coCli. This function restores the contracts of a clients,
	'**% passed as a parameter.
	'%insReaFincance_coCli. Esta Función devuelve los contrats de un cliente, el cual ha
	'%sido pasado como parámetro.
	Private Function insReaFincance_coCli(ByRef lstrClient As String, ByRef ldtmEffecdate As Object) As eRemoteDB.Execute
		
		insReaFincance_coCli = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatFinance_coCli'
		'+Definición de parámetros para stored procedure 'insudb.queDatFincance_coCli'
		'**+ Information read on November 29, 1999  03:01:50 p.m.
		'+Información leída el 29/11/1999 03:01:50 p.m.
		
		With insReaFincance_coCli
			.StoredProcedure = "queDatFinance_coCli"
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaFincance_coCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaFincance_coCli = Nothing
			End If
		End With
	End Function
	
	'**% InsReaFinance_co_c. This function is in charge of making the reading of just one contract.
	'%InsReaFincance_co_o. Esta funcion se encarga de realizar la lectura de un sólo contrato
	Private Function insReaFincance_co_o(ByRef lstrFincance_co As Integer) As eRemoteDB.Execute
		insReaFincance_co_o = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatFinance_co'
		'+Definición de parámetros para stored procedure 'insudb.queDatFinance_co'
		'**+ Information read on December 03,1999 11:28:28 a.m.
		'+Información leída el 03/12/1999 11:28:28 a.m.
		
		With insReaFincance_co_o
			.StoredProcedure = "queDatFinance_co"
			.Parameters.Add("nContrat", lstrFincance_co, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaFincance_co_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaFincance_co_o = Nothing
			End If
		End With
	End Function
End Class







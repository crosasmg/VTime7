Option Strict Off
Option Explicit On
Friend Class Part_contr
	'%-------------------------------------------------------%'
	'% $Workfile:: Part_contr.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find. This function is for reading operations depending on the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		
		Select Case nParentFolder
			Case 34 'Distribución de reaseguro
				Find = insReaPart_contr(1, (Parameters("nType").Valor), CInt(0 & Parameters("nNumber").Valor), (Parameters("nBranch_rei").Valor), (Parameters("nCompany").Valor), (Parameters("dEffecdate").Valor))
			Case 76 'Contratos de la cía
				Find = insReaPart_contr(0, 0, 0, 0, (Parameters("nCompany").Valor), (Parameters("dEffecdate").Valor))
			Case 13 'Contratos de la cía
				Find = insReaPart_contr(0, 0, 0, 0, (Parameters("nCompany").Valor), (Parameters("dEffecdate").Valor))
				
			Case 0 'Contratos de la cía
				Find = insReaPart_contr(0, 0, 0, 0, (Parameters("nCompany").Valor), (Parameters("dEffecdate").Valor))
				
			Case Else
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
		End Select
	End Function
	
	'**%insReaPart_contr. This function restores the participant companies of a reinsurance company.
	'%insReaPart_contr. Esta funcion devuelve las compañias participantes de un contrato de
	'%reaseguro.
	Private Function insReaPart_contr(ByRef lintType_rel As Integer, ByRef lintType As Integer, ByRef lintNumber As Integer, ByRef lintBranch_rei As Integer, ByRef lintCompany As Integer, ByRef ldtmEffecdate As Object) As eRemoteDB.Execute
		
		insReaPart_contr = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.queDatPart_contr'
		'+Definición de parámetros para stored procedure 'insudb.queDatPart_contr'
		'**+ Information read on January 05,2000 03:32:25 p.m.
		'+Información leída el 05/01/2000 03:32:25 p.m.
		
		With insReaPart_contr
			.StoredProcedure = "queDatPart_contr"
			.Parameters.Add("nType_rel", lintType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", lintType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", lintNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", lintBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object insReaPart_contr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaPart_contr = Nothing
			End If
		End With
		
	End Function
End Class







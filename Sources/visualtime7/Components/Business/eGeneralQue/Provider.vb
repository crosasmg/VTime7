Option Strict Off
Option Explicit On
Friend Class Provider
	'%-------------------------------------------------------%'
	'% $Workfile:: Provider.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find. This function is used for reading operations depending on the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			'        Case 6 '- Provideres del siniesrto
			'            Set Find = insReaProviderCla(lobjParent("nClaim").Valor)
			'        Case 24 '-Provideres del caso
			'            Set Find = insReaProviderCla(lobjParent("nClaim").Valor, lobjParent("nCase_num").Valor, lobjParent("nDeman_type").Valor)
			Case Else
				Find = insReaProvider_o((Parameters("HnProvider").Valor))
		End Select
	End Function
	
	'**% insReaProviderPol. This function returns the providers
	'%insReaProviderPol. esta funcion retorna los Proveedores
	Private Function insReaProvider_o(ByRef lintProvider As Integer) As eRemoteDB.Execute
		
		If lintProvider <> 0 Then
			insReaProvider_o = New eRemoteDB.Execute
			
			'**+ Parameter defintion for stored procedure 'insudb.queDatProviderCla'
			'+Definición de parámetros para stored procedure 'insudb.queDatProviderCla'
			'**+ Information read on Decemeber 03, 1999  10:09:28 a.m.
			'+Información leída el 03/12/1999 10:09:28 a.m.
			
			With insReaProvider_o
				.StoredProcedure = "queDatTab_provider"
				.Parameters.Add("nProvider", lintProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If Not .Run Then
					.RCloseRec()
					'UPGRADE_NOTE: Object insReaProvider_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					insReaProvider_o = Nothing
				End If
			End With
		Else
			'UPGRADE_NOTE: Object insReaProvider_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			insReaProvider_o = Nothing
		End If
	End Function
End Class







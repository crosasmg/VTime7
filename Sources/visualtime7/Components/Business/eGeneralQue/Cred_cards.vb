Option Strict Off
Option Explicit On
Public Class Cred_cards
	'%-------------------------------------------------------%'
	'% $Workfile:: Cred_cards.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'%Find.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			Case 4
				Find = insReaCred_CardCli((Parameters("sClient").Valor))
			Case Else
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
		End Select
	End Function
	
	'%insReaCred_CardCli. Esta Función devuelve las tarjetas de un cliente
	Private Function insReaCred_CardCli(ByRef lstrClient As String) As eRemoteDB.Execute
		insReaCred_CardCli = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'queDatBk_accountCli'
		'Información leída el 25/06/2001
		With insReaCred_CardCli
			.StoredProcedure = "queDatCred_CardCli"
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object insReaCred_CardCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaCred_CardCli = Nothing
			End If
		End With
	End Function
End Class







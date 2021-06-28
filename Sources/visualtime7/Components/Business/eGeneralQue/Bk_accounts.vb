Option Strict Off
Option Explicit On
Public Class Bk_accounts
	'%-------------------------------------------------------%'
	'% $Workfile:: Bk_accounts.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'%Find.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			Case 4
				Find = insReaBk_accountCli((Parameters("sClient").Valor))
			Case Else
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
		End Select
	End Function
	
	'%insReaBk_accountCli. Esta Función devuelve las cuentas de un cliente
	Private Function insReaBk_accountCli(ByRef lstrClient As String) As eRemoteDB.Execute
		insReaBk_accountCli = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'queDatBk_accountCli'
		'Información leída el 25/06/2001
		With insReaBk_accountCli
			.StoredProcedure = "queDatBk_accountCli"
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object insReaBk_accountCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaBk_accountCli = Nothing
			End If
		End With
	End Function
End Class







Option Strict Off
Option Explicit On
Public Class Bulletins
	'%-------------------------------------------------------%'
	'% $Workfile:: Bulletins.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'%Find.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			Case 4
				Find = insReaBulletinsCli((Parameters("sClient").Valor))
			Case 7
				Find = insReaBulletinsReceipt((Parameters("nReceipt").Valor))

			Case Else
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
		End Select
	End Function
	
	'%insReaBulletinsCli. Esta Función devuelve los boletines de un cliente
	Private Function insReaBulletinsCli(Byval lstrClient As String) As eRemoteDB.Execute
		insReaBulletinsCli = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'queDatBulletinsCli'
		'Información leída el 29/08/2001
		With insReaBulletinsCli
			.StoredProcedure = "queDatBulletinsCli"
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object insReaBulletinsCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaBulletinsCli = Nothing
			End If
		End With
	End Function

	'%insReaBulletinsCli. Esta Función devuelve los boletines de un Recibo
	Private Function insReaBulletinsReceipt(Byval ldblReceipt As Double) As eRemoteDB.Execute
		insReaBulletinsReceipt = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'queDatBulletinsReceipt'
		'Información leída el 29/08/2001
		With insReaBulletinsReceipt
			.StoredProcedure = "queDatBulletinsReceipt"
            .Parameters.Add("nReceipt", ldblReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				insReaBulletinsReceipt = Nothing
			End If
		End With
	End Function

End Class







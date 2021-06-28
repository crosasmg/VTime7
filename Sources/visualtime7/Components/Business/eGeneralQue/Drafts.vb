Option Strict Off
Option Explicit On
Public Class Drafts
	'%-------------------------------------------------------%'
	'% $Workfile:: Drafts.cls                               $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find. This function is used to obtain the contracts depending on the folder that calls them.
	'%Find. Se utiliza esta funcion para obtener los contratos depediendo de la carpeta que
	'%lo llama.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			
			Case 7 '-Contratos/Giros de una póliza
				Find = insReaFinan_Pre((Parameters("nReceipt").Valor))
				
			Case Else
				Find = insReaFinan_Pre((Parameters("nReceipt").Valor))
		End Select
	End Function
	
	'%insReaFinan_Pre: Realiza la lectura de los giros de un contrato/recibo
	Private Function insReaFinan_Pre(ByRef llngReceipt As Integer) As eRemoteDB.Execute
		insReaFinan_Pre = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatFinanc_DraReceipt'
		'+Definición de parámetros para stored procedure 'insudb.queDatFinanc_DraReceipt'
		
		With insReaFinan_Pre
			.StoredProcedure = "queDatFinanc_DraReceipt"
			.Parameters.Add("nReceipt", llngReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If Not .Run Then
				'UPGRADE_NOTE: Object insReaFinan_Pre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaFinan_Pre = Nothing
			End If
		End With
	End Function
End Class







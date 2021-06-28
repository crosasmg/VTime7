Option Strict Off
Option Explicit On
Public Class Company
	'%-------------------------------------------------------%'
	'% $Workfile:: Company.cls                               $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find. This function is used for read operations depending on the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Params As Properties) As eRemoteDB.Execute
		
		Find = New eRemoteDB.Execute
		
		Select Case nParentFolder
			'**+ Parameter definition for stored procedure 'insudb.reaClient'
			'+Definición de parámetros para stored procedure 'insudb.reaClient'
			'**+ Information read on Novemeber 24, 1999  11:48:14 a.m.
			'+Información leída el 24/11/1999 11:48:14 a.m.
			Case 0, 13 'Contratos de la cía
				With Find
					.StoredProcedure = "QUEDATCLIENTCOM"
					.Parameters.Add("nCompany", Params("nCompany").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If Not .Run Then
						'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						Find = Nothing
					End If
				End With
			Case Else
				With Find
					.StoredProcedure = "QUEDATCLIENTCOM"
					.Parameters.Add("nCompany", Params("nCompany").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If Not .Run Then
						'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						Find = Nothing
					End If
				End With
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
		End Select
		
	End Function
End Class







Option Strict Off
Option Explicit On
Public Class Claim_Ces
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_Ces.cls                            $%'
	'% $Author:: Pgarin                                     $%'
	'% $Date:: 5/04/06 16:04                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find. This function is used for read operations depending on the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Params As Properties) As eRemoteDB.Execute
		
		Find = New eRemoteDB.Execute
		
		Select Case nParentFolder
			Case 0, 81 'Siniestros cedidos
				With Find
					.StoredProcedure = "QUECESSIONSIN"
					.Parameters.Add("nPolicy", Params("nPolicy").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If Not .Run Then
						'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						Find = Nothing
					End If
				End With
			Case Else
				With Find
					.StoredProcedure = "QUECESSIONSIN"
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







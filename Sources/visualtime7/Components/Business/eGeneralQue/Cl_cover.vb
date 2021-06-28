Option Strict Off
Option Explicit On
Friend Class Cl_cover
	'%-------------------------------------------------------%'
	'% $Workfile:: Cl_cover.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find: This function is used to obtain the clauses
	'%Find. Se utiliza esta funcion para obtener las clausulas
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			
			'+ Reserva del siniestro
			Case 6
				Find = insReaCl_cover((Parameters("nClaim").Valor))
				'+ Reserva del caso
			Case 24
				Find = insReaCl_cover((Parameters("nClaim").Valor), (Parameters("nCase_num").Valor), (Parameters("nDeman_type").Valor))
			Case Else
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
		End Select
	End Function
	
	'**% insReaCl_cover. This function returns the associated clients of a claim.
	'%insReaCl_cover. esta funcion retorna los Clientes asociados a  un siniestro
	Private Function insReaCl_cover(ByRef llngClaim As Double, Optional ByRef lintCase_num As Integer = -1, Optional ByRef lintDeman_Type As Integer = -1) As eRemoteDB.Execute
		
		insReaCl_cover = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insud.queDatCl_cover'
		'+Definición de parámetros para stored procedure 'insudb.queDatCl_cover'
		'**+ Information read on December 07, 1999
		'+Información leída el 07/12/1999
		
		With insReaCl_cover
			.StoredProcedure = "queDatCl_coverCla"
			.Parameters.Add("nClaim", llngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCase_num", IIf(lintCase_num = -1, System.DBNull.Value, lintCase_num), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nDeman_type", IIf(lintDeman_Type = -1, System.DBNull.Value, lintDeman_Type), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object insReaCl_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaCl_cover = Nothing
			End If
		End With
	End Function
End Class







Option Strict Off
Option Explicit On
Friend Class Claim_case
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_case.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find: This function is used to obtain the clauses.
	'%Find. Se utiliza esta funcion para obtener las clausulas
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			Case 6
				Find = insreaClaim_case((Parameters("nClaim").Valor), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)
			Case 24
				Find = insreaClaim_case((Parameters("nClaim").Valor), (Parameters("nCase_num").Valor), (Parameters("nDeman_type").Valor))
			Case Else
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
		End Select
	End Function
	
	'**% insreaClauses: This function is in charge of searching the policy clauses.
	'%insreaClauses. Esta función se encarga de buscar las clausulas de la poliza
	Private Function insreaClaim_case(ByRef llngClaim As Double, ByRef nCase_num As Integer, ByRef nDeman_type As Integer) As eRemoteDB.Execute
		
		insreaClaim_case = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.quedatDisco_expr'
		'+Definición de parámetros para stored procedure 'insudb.quedatDisco_expr'
		'**+ Information read on December 06, 1999  02:08:30 p.m.
		'+Información leída el 06/12/1999 02:08:30 p.m.
		
		
		With insreaClaim_case
			.StoredProcedure = "queDatClaim_case"
			.Parameters.Add("nClaim", llngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insreaClaim_case may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insreaClaim_case = Nothing
			End If
		End With
	End Function
End Class







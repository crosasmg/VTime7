Option Strict Off
Option Explicit On
Public Class Comm_pol
	
	
	'**% Find: This function is used for read operations depending of the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	'Public Function Find(nParentFolder As Long, Parameters As Properties) As eRemoteDB.Execute
	'    Select Case nParentFolder
	'**+ Policy clientes/certificate
	'+ Clientes de la póliza/Certificado'
	'        Case 0
	'Set Find = insReaIntermed(Parameters("nIntermed").Valor)
	'            Set Find = insReaIntermed(21)
	'**+ Claim clienta.
	'+ Clientes del siniesrto
	'        Case 6
	'            Set Find = insReaClientsCla(Parameters("nClaim").Valor)
	'**+Case clients
	'+Clientes del caso
	'        Case 24
	'            Set Find = insReaClientsCla(Parameters("nClaim").Valor, Parameters("nCase_num").Valor, Parameters("nDeman_type").Valor)
	'        Case Else
	'            If nParentFolder = 0 Then
	'                Set Find = insReaClient_o(Parameters("HsClient").Valor)
	'            Else
	'                Set Find = insReaClient_o(Parameters("sClient").Valor)
	'            End If
	'    End Select
	'End Function
	
	'**% insReaClientsPol. This function returns the clients of a policy that receipt as a parameter.
	'%insREaClientsPol. esta funcion retorna los Clientes de una póliza que recibe como parámetro
	'Private Function insReaIntermed(lnIntermed) As eRemoteDB.Execute
	Public Function Find(ByRef nParentFolder As Integer, ByRef Params As Properties) As eRemoteDB.Execute
		Find = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudbqueDatClientPol'
		'+Definición de parámetros para stored procedure 'insudb.queDatClientPol'
		'**+ Information read on Novemeber 25,1999  02:52:20 p.m.
		'+Información leída el 25/11/1999 02:52:20 p.m.
		
		With Find
			.StoredProcedure = "reaintermed_ge"
			.Parameters.Add("lnIntermed", Params("nIntermed").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'.Parameters.Add "lnIntermed", 21, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
			End If
		End With
		
		'**+ Parameter definition for stored procedure 'insudb.reaClient'
		'+Definición de parámetros para stored procedure 'insudb.reaClient'
		'**+ Information read on Novemeber 24, 1999  11:48:14 a.m.
		'+Información leída el 24/11/1999 11:48:14 a.m.
		
	End Function
	
	'**% insReaClientsCla. This function returns the associated clients of a claim.
	'%insReaClientsCla. esta funcion retorna los Clientes asociados a  un siniestro
	Private Function insReaClientsCla(ByRef llngClaim As Double, Optional ByRef lintCase_num As Integer = -1, Optional ByRef lintDeman_Type As Integer = -1) As eRemoteDB.Execute
		
		insReaClientsCla = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatClient'
		'+Definición de parámetros para stored procedure 'insudb.queDatClient'
		'**+ Information read on Novemeber 25, 1999  02:52:20 p.m.
		'+Información leída el 25/11/1999 02:52:20 p.m.
		
		With insReaClientsCla
			.StoredProcedure = "queDatClientCla"
			.Parameters.Add("nClaim", llngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCase_num", IIf(lintCase_num = -1, System.DBNull.Value, lintCase_num), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nDeman_type", IIf(lintDeman_Type = -1, System.DBNull.Value, lintDeman_Type), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaClientsCla may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaClientsCla = Nothing
			End If
		End With
	End Function
	
	'**% insReaClientsPol. This function returns the clients of a policy that receipt as a parameter
	'%insReaClientsPol. esta funcion retorna los Clientes de una póliza que recibe como parámetro
	Private Function insReaClient_o(ByRef lstrClient As String) As eRemoteDB.Execute
		
		insReaClient_o = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatClientcla'
		'+Definición de parámetros para stored procedure 'insudb.queDatClientCla'
		'**+ Information read on December 03,1999 10:09:28 a.m.
		'+Información leída el 03/12/1999 10:09:28 a.m.
		
		With insReaClient_o
			.StoredProcedure = "queDatClient"
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaClient_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaClient_o = Nothing
			End If
		End With
	End Function
End Class







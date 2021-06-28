Option Strict Off
Option Explicit On
Friend Class Clients
	'%-------------------------------------------------------%'
	'% $Workfile:: Clients.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find: This function is used for read operations depending of the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			'**+ Policy clientes/certificate
			'+ Clientes de la póliza/Certificado'
			Case 1, 11, 5, 3
				Find = insReaClientsPol((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), (Parameters("HdEffecdate").Valor))
				'**+ Claim clienta.
				'+ Clientes del siniesrto
			Case 6
				Find = insReaClientsCla((Parameters("nClaim").Valor))
				
			Case 13
				Find = insReaClientsCom((Parameters("nCompany").Valor))
				
				'**+Case clients
				'+Clientes del caso
			Case 24
				Find = insReaClientsCla((Parameters("nClaim").Valor), (Parameters("nCase_num").Valor), (Parameters("nDeman_type").Valor))
			Case Else
				If nParentFolder = 0 Then
					Find = insReaClient_o((Parameters("HsClient").Valor))
				Else
					Find = insReaClient_o((Parameters("sClient").Valor))
				End If
		End Select
	End Function
	
	'**% insReaClientsPol. This function returns the clients of a policy that receipt as a parameter.
	'%insREaClientsPol. esta funcion retorna los Clientes de una póliza que recibe como parámetro
	'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    Private Function insReaClientsPol(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, Optional ByRef llngCertif As Integer = 0, Optional ByRef ldtmEffecdate As Object = Nothing) As eRemoteDB.Execute

        insReaClientsPol = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudbqueDatClientPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatClientPol'
        '**+ Information read on Novemeber 25,1999  02:52:20 p.m.
        '+Información leída el 25/11/1999 02:52:20 p.m.

        With insReaClientsPol
            .StoredProcedure = "queDatClientPol"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaClientsPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaClientsPol = Nothing
            End If
        End With
    End Function
	
	
	'**% insReaClientsCla. This function returns the associated clients of a claim.
	'%insReaClientsCla. esta funcion retorna los Clientes asociados a  un siniestro
	Private Function insReaClientsCom(ByRef llngCompany As Double) As eRemoteDB.Execute
		
		insReaClientsCom = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatClient'
		'+Definición de parámetros para stored procedure 'insudb.queDatClient'
		'**+ Information read on Novemeber 25, 1999  02:52:20 p.m.
		'+Información leída el 25/11/1999 02:52:20 p.m.
		
		With insReaClientsCom
			.StoredProcedure = "queDatClientCom"
			.Parameters.Add("nCompany", llngCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaClientsCom may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaClientsCom = Nothing
			End If
		End With
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







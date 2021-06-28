Option Strict Off
Option Explicit On
Public Class Tab_modCli
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_modCli.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'Column_name                 Type   Computed   Length   Prec  Scale Nullable  TrimTrailingBlanks       FixedLenNullInSource
	Public sItem As String 'char      no        12                    no           no                          no
	Public sTabname As String 'char      no        12                    no           no                          no
	Public sIndPk As String
	Public sTableError As String
	
	'**%Find: Returns the locality description with a given locality code
	'% Find: Devuelve la descripción de una localidad dado un código de localidad
	Public Function Find(ByVal sNewClientCode As String, ByVal sClient As String, ByVal nUser As Integer) As Boolean
		'**-Variable definition. lrecTab_modcli
		'-Se define variable para realizar operaciones a la BD
		Dim lrecreaTab_modcli As eRemoteDB.Execute
		Dim lrecUpdate As eRemoteDB.Execute
		
		'**- Variable definition. lstrQuery. This variable is used to store the query construction
		'- Se utiliza para almacenar la construcción del query.
		Dim lstrQuery As String
		Dim lstrQuery1 As String
		
		lrecreaTab_modcli = New eRemoteDB.Execute
		lrecUpdate = New eRemoteDB.Execute
		
		sTableError = String.Empty
		
		On Error GoTo Find_Err
		
		With lrecreaTab_modcli
			.StoredProcedure = "reaTab_modcli"
			
			If .Run Then
				Do While Not .EOF
					sItem = UCase(.FieldToClass("sItem"))
					sTabname = UCase(.FieldToClass("sTabname"))
					sIndPk = .FieldToClass("sIndPk")
					lstrQuery = String.Empty
					lstrQuery1 = String.Empty
					
					'**+If it is the table Claim_benef, the system inserts the same record in the master table
					'**+with the new client code, afer that the previous record is deleted
					'+ Si se trata de la tabla Claim_Benef se inserta un registro idéntico en la tabla padre
					'+ con el nuevo código de cliente, luego se elimina el registro anterior
					
					If CDbl(sIndPk) = 1 Then
						lstrQuery = "InsChangeClientCode"
						lrecUpdate.StoredProcedure = lstrQuery
						lrecUpdate.Parameters.Add("sOldClient", Trim(sClient), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecUpdate.Parameters.Add("sNewClient", Trim(sNewClientCode), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecUpdate.Parameters.Add("sTable", Trim(sTabname), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecUpdate.Parameters.Add("nUserCode", nUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					ElseIf CDbl(sIndPk) = 2 Then 
						lstrQuery = "insDelCod_client"
						lrecUpdate.StoredProcedure = lstrQuery
						lrecUpdate.Parameters.Add("sTable", Trim(sTabname), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecUpdate.Parameters.Add("sOldClient", Trim(sClient), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecUpdate.Parameters.Add("nUserCode", nUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecUpdate.Parameters.Add("sItem", Trim(sItem), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					ElseIf CDbl(sIndPk) = 3 Then 
						lstrQuery = "insUpdCod_client"
						lrecUpdate.StoredProcedure = lstrQuery
						lrecUpdate.Parameters.Add("sTable", Trim(sTabname), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecUpdate.Parameters.Add("sNewClient", Trim(sNewClientCode), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecUpdate.Parameters.Add("sOldClient", Trim(sClient), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecUpdate.Parameters.Add("nUserCode", nUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecUpdate.Parameters.Add("sItem", Trim(sItem), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End If
					
					If lrecUpdate.Run(False) Then
						.RNext()
					Else
						Exit Do
					End If
				Loop 
				
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecUpdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdate = Nothing
		'UPGRADE_NOTE: Object lrecreaTab_modcli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_modcli = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
			sTableError = sTabname
		End If
	End Function
	
	
	'% insTabMod_cli: Se encarga de realizar la unificacion en las tablas encontradas en tabmod_cli
	Public Function insTabMod_cli(ByVal sNewClientCode As String, ByVal sClient As String, ByVal nUser As Integer) As Boolean
		'-Se define variable para realizar operaciones a la BD
		Dim lrecreaTab_modcli As eRemoteDB.Execute
		
		lrecreaTab_modcli = New eRemoteDB.Execute
		With lrecreaTab_modcli
			.StoredProcedure = "INSTABMOD_CLI"
			.Parameters.Add("sOldClient", Trim(sClient), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNewClient", Trim(sNewClientCode), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insTabMod_cli = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecreaTab_modcli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_modcli = Nothing
	End Function
End Class







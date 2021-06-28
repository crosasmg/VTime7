Option Strict Off
Option Explicit On
Public Class Province
	
	'Column_name                   Type      Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nProvince As Integer 'smallint  2           5     0     no                                  (n/a)                               (n/a)
	Public sDescript As String 'char      30                      yes                                 no                                  yes
	Public sShort_des As String 'char      12                      yes                                 no                                  yes
	Public nUsercode As Integer 'smallint  2           5     0     no                                  (n/a)                               (n/a)
	
	'%Find: This method returns TRUE or FALSE depending if the records exists in the table "Province"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%      tabla "Province"
	Public Function Find(ByVal Province As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecreaProvince As eRemoteDB.Execute
		
		Find = True
		If Province <> nProvince Or lblnFind Then
			lrecreaProvince = New eRemoteDB.Execute
			
			'+ Definición de parámetros para stored procedure 'insudb.reaProvince'
			With lrecreaProvince
				.StoredProcedure = "reaProvince"
				.Parameters.Add("nProvince", Province, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nProvince = .FieldToClass("nProvince")
					sDescript = .FieldToClass("sDescript")
					sShort_des = .FieldToClass("sShort_des")
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaProvince may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaProvince = Nothing
		End If
	End Function
	
	'% Add: Este método crea un registro en la tabla Province
	Public Function Add(Optional ByRef nProvince As Integer = numNull, Optional ByRef sDescript As String = strNull, Optional ByRef sShort_des As String = strNull, Optional ByRef nUsercode As Integer = numNull) As Boolean
		Dim lreccreProvince As eRemoteDB.Execute
		
		lreccreProvince = New eRemoteDB.Execute
		
		
		If nProvince <> numNull Then
			Me.nProvince = nProvince
		End If
		
		If sDescript <> strNull Then
			Me.sDescript = sDescript
		End If
		
		If sShort_des <> strNull Then
			Me.sShort_des = sShort_des
		End If
		
		If nUsercode <> numNull Then
			Me.nUsercode = nUsercode
		End If
		
		'+ Definición de parámetros para stored procedure 'insudb.creProvince'
		With lreccreProvince
			.StoredProcedure = "creProvince"
			
			.Parameters.Add("nProvince", Me.nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", Me.sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", Me.sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Add = True
			End If
		End With
		'UPGRADE_NOTE: Object lreccreProvince may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreProvince = Nothing
		
	End Function
	
	'% Update: Este método actualiza un registro en la tabla Province
	Public Function Update(Optional ByRef nProvince As Integer = numNull, Optional ByRef sDescript As String = strNull, Optional ByRef sShort_des As String = strNull, Optional ByRef nUsercode As Integer = numNull) As Boolean
		Dim lrecupdProvince As eRemoteDB.Execute
		
		lrecupdProvince = New eRemoteDB.Execute
		
		If nProvince <> numNull Then
			Me.nProvince = nProvince
		End If
		
		If sDescript <> strNull Then
			Me.sDescript = sDescript
		End If
		
		If sShort_des <> strNull Then
			Me.sShort_des = sShort_des
		End If
		
		If nUsercode <> numNull Then
			Me.nUsercode = nUsercode
		End If
		
		'+ Definición de parámetros para stored procedure 'insudb.updProvince'
		With lrecupdProvince
			.StoredProcedure = "updProvince"
			
			.Parameters.Add("nProvince", Me.nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", Me.sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", Me.sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
			End If
		End With
		'UPGRADE_NOTE: Object lrecupdProvince may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdProvince = Nothing
		
	End Function
	
	'% Delete: Este método elimina un registro en la tabla Province
	Public Function Delete(Optional ByRef nProvince As Integer = numNull) As Boolean
		Dim lrecdelProvince As eRemoteDB.Execute
		
		lrecdelProvince = New eRemoteDB.Execute
		
		If nProvince <> numNull Then
			Me.nProvince = nProvince
		End If
		
		'+ Definición de parámetros para stored procedure 'insudb.delProvince'
		With lrecdelProvince
			.StoredProcedure = "delProvince"
			
			.Parameters.Add("nProvince", Me.nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Delete = True
			End If
		End With
		'UPGRADE_NOTE: Object lrecdelProvince may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelProvince = Nothing
		
	End Function
	
	'% Validate:
    Public Function valMS109_K(ByVal sAction As String, ByVal nProvince As Integer, ByVal sDescript As String, ByVal sShort_des As String) As String
        Dim lobjErrors As eFunctions.Errors

        lobjErrors = New eFunctions.Errors

        '+ Validación de la provincia
        If nProvince = numNull Then
            lobjErrors.ErrorMessage("MS109", 10842)
        Else
            If Find(nProvince) And sAction = "Add" Then
                lobjErrors.ErrorMessage("MS109", 10861)
            End If
        End If

        '+ Validación de la descripción abreviada
        If sShort_des = String.Empty Then
            lobjErrors.ErrorMessage("MS109", 10843)
        End If

        '+ Validación de la descripción abreviada
        If sShort_des = String.Empty Then
            lobjErrors.ErrorMessage("MS109", 10844)
        End If

        valMS109_K = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function
	
	'%insPostMS109: Esta función se encarga de realizar la actualización de la ventana MS109
    Public Function insPostMS109(ByVal sAction As String, ByVal nProvince As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nUsercode As Integer) As Boolean
        Select Case sAction
            Case "Add"
                insPostMS109 = Add(nProvince, sDescript, sShort_des, nUsercode)
            Case "Update"
                insPostMS109 = Update(nProvince, sDescript, sShort_des, nUsercode)
            Case "Del"
                insPostMS109 = Delete(nProvince)
        End Select
    End Function
	
	'% reaProvince_inTablocat: Esta función se encarga de verificar si el código de una
	'%                         provincia se encuentra asociado a una localidad.
	Public Function reaProvince_inTablocat(ByVal nProvince As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreatab_locat_v As eRemoteDB.Execute
		
		lrecreatab_locat_v = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reatab_locat_v'
		With lrecreatab_locat_v
			.StoredProcedure = "reatab_locat_v"
			
			.Parameters.Add("nProvince", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				reaProvince_inTablocat = True
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreatab_locat_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatab_locat_v = Nothing
	End Function
	
	'% reaProvince_inAddress: Esta función se encarga de verificar si el código de provincia
	'%                        se encuentra asociada a una dirección
	Public Function reaProvince_inAddress(ByVal nProvince As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaAddress_Local_a As eRemoteDB.Execute
		
		lrecreaAddress_Local_a = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaAddress_Local_a'
		With lrecreaAddress_Local_a
			.StoredProcedure = "reaAddress_Local_a"
			
			.Parameters.Add("nProvince", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				reaProvince_inAddress = True
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaAddress_Local_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAddress_Local_a = Nothing
		
	End Function
	
	'%  reaProvince_inCommiss_pr: Esta función se encarga de verificar si el código de provincia
	'% se encuentra asociada a un recibo
	Public Function reaProvince_inCommiss_pr(ByVal nProvince As Integer) As Boolean
		Dim lrecreaCommiss_pr_p As eRemoteDB.Execute
		
		lrecreaCommiss_pr_p = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaCommiss_pr_p'
		With lrecreaCommiss_pr_p
			.StoredProcedure = "reaCommiss_pr_p"
			.Parameters.Add("nProvince", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				reaProvince_inCommiss_pr = True
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCommiss_pr_p may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCommiss_pr_p = Nothing
	End Function
End Class







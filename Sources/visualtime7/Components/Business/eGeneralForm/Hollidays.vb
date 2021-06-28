Option Strict Off
Option Explicit On
Public Class Hollidays
	
	'Column_name                   Type      Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nMonth As Integer 'smallint  2           5     0     no                                  (n/a)                               (n/a)
	Public nDay As Integer 'smallint  2           5     0     no                                  (n/a)                               (n/a)
	Public sDescript As String 'char      30                      yes                                 no                                  yes
	Public nUsercode As Integer 'smallint  2           5     0     no                                  (n/a)                               (n/a)
	Public nCountry As Integer 'number     5          5     0
	
	
	'%Find: This method returns TRUE or FALSE depending if the records exists in the table "Hollidays"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%      tabla "Hollidays"
	'UPGRADE_NOTE: Day was upgraded to Day_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_NOTE: Month was upgraded to Month_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Function Find(ByVal Month_Renamed As Integer, ByVal Day_Renamed As Integer, Optional ByVal lblnFind As Boolean = False, Optional ByRef Country As Integer = numNull) As Boolean
		
		Dim lrecreaHolliday As eRemoteDB.Execute
		
		Find = True
		If Month_Renamed <> nMonth Or Day_Renamed <> nDay Or Country <> nCountry Or lblnFind Then
			lrecreaHolliday = New eRemoteDB.Execute
			
			'+ Definición de parámetros para stored procedure 'insudb.reaProvince'
			With lrecreaHolliday
				.StoredProcedure = "reaHollidays_1"
				.Parameters.Add("nMonth", Month_Renamed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDay", Day_Renamed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCountry", Country, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nMonth = .FieldToClass("nMonth")
					nDay = .FieldToClass("nDay")
					sDescript = .FieldToClass("sDescript")
					Me.nCountry = .FieldToClass("nCountry")
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaHolliday may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaHolliday = Nothing
		End If
	End Function
	
	'% Add: Este método crea un registro en la tabla Holliday
	Public Function Add(Optional ByRef nMonth As Integer = numNull, Optional ByRef nDay As Integer = numNull, Optional ByRef sDescript As String = strNull, Optional ByRef nUsercode As Integer = numNull, Optional ByRef nCountry As Integer = numNull) As Boolean
		Dim lreccreHolliday As eRemoteDB.Execute
		
		lreccreHolliday = New eRemoteDB.Execute
		
		
		If nMonth <> numNull Then
			Me.nMonth = nMonth
		End If
		
		If nDay <> numNull Then
			Me.nDay = nDay
		End If
		
		If sDescript <> strNull Then
			Me.sDescript = sDescript
		End If
		
		If nUsercode <> numNull Then
			Me.nUsercode = nUsercode
		End If
		
		If nCountry <> numNull Then
			Me.nCountry = nCountry
		End If
		
		'+ Definición de parámetros para stored procedure 'creHolliday'
		With lreccreHolliday
			.StoredProcedure = "creHolliday"
			
			.Parameters.Add("nMonth", Me.nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDay", Me.nDay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", Me.sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCountry", Me.nCountry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Add = True
			End If
		End With
		'UPGRADE_NOTE: Object lreccreHolliday may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreHolliday = Nothing
		
	End Function
	
	'% Update: Este método actualiza un registro en la tabla Holliday
	Public Function Update(Optional ByRef nMonth As Integer = numNull, Optional ByRef nDay As Integer = numNull, Optional ByRef sDescript As String = strNull, Optional ByRef nUsercode As Integer = numNull, Optional ByRef nCountry As Integer = numNull) As Boolean
		Dim lrecupdHolliday As eRemoteDB.Execute
		
		lrecupdHolliday = New eRemoteDB.Execute
		
		If nMonth <> numNull Then
			Me.nMonth = nMonth
		End If
		
		If nDay <> numNull Then
			Me.nDay = nDay
		End If
		
		If sDescript <> strNull Then
			Me.sDescript = sDescript
		End If
		
		If nUsercode <> numNull Then
			Me.nUsercode = nUsercode
		End If
		
		If nCountry <> numNull Then
			Me.nCountry = nCountry
		End If
		
		'+ Definición de parámetros para stored procedure 'updHolliday'
		With lrecupdHolliday
			.StoredProcedure = "updHolliday"
			.Parameters.Add("nMonth", Me.nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDay", Me.nDay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", Me.sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCountry", Me.nCountry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
			End If
		End With
		'UPGRADE_NOTE: Object lrecupdHolliday may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdHolliday = Nothing
		
	End Function
	
	'% Delete: Este método elimina un registro en la tabla Holliday
	Public Function Delete(Optional ByRef nMonth As Integer = numNull, Optional ByRef nDay As Integer = numNull, Optional ByRef nCountry As Integer = numNull) As Boolean
		Dim lrecdelHolliday As eRemoteDB.Execute
		
		lrecdelHolliday = New eRemoteDB.Execute
		
		If nMonth <> numNull Then
			Me.nMonth = nMonth
		End If
		
		If nDay <> numNull Then
			Me.nDay = nDay
		End If
		If nCountry <> numNull Then
			Me.nCountry = nCountry
		End If
		'+ Definición de parámetros para stored procedure 'delHolliday'
		With lrecdelHolliday
			.StoredProcedure = "delHolliday"
			.Parameters.Add("nMonth", Me.nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDay", Me.nDay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCountry", Me.nCountry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Delete = True
			End If
		End With
		'UPGRADE_NOTE: Object lrecdelHolliday may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelHolliday = Nothing
		
	End Function
	
	'% Validate:
	Public Function valMS821_K(ByVal sAction As String, ByVal nMonth As Integer, ByVal nDay As Integer, ByVal sDescript As String, Optional ByRef nCountry As Integer = 0) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		'+ Validación del mes
		If nMonth = numNull Then
			lobjErrors.ErrorMessage("MS821", 1012,  , eFunctions.Errors.TextAlign.RigthAling, "(Mes)")
		Else
			If Find(nMonth, nDay, True, nCountry) And sAction = "Add" Then
				lobjErrors.ErrorMessage("MS821", 38011)
			End If
		End If
		
		'+ Validación del día
		If nDay = numNull Then
			lobjErrors.ErrorMessage("MS821", 1012,  , eFunctions.Errors.TextAlign.RigthAling, "(Día)")
		End If
		
		'+ Validación del pais
		If nCountry = numNull Then
			lobjErrors.ErrorMessage("MS821", 1012,  , eFunctions.Errors.TextAlign.RigthAling, "(País)")
		End If
		
		valMS821_K = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insPostMS821: Esta función se encarga de realizar la actualización de la ventana MS821
    Public Function insPostMS821(ByVal sAction As String, ByVal nMonth As Integer, ByVal nDay As Integer, ByVal sDescript As String, ByVal nUsercode As Integer, Optional ByVal nCountry As Integer = 0) As Boolean
		Select Case sAction
			Case "Add"
				insPostMS821 = Add(nMonth, nDay, sDescript, nUsercode, nCountry)
			Case "Update"
				insPostMS821 = Update(nMonth, nDay, sDescript, nUsercode, nCountry)
			Case "Del"
				insPostMS821 = Delete(nMonth, nDay, nCountry)
		End Select
	End Function
	
	'% insHollidaysArray: Se crea un arreglo con los dias feriados por mes
	Public Function strHollidaysArray(ByVal nMonth As Integer) As String
		Dim lrecHolliday As eRemoteDB.Execute
		Dim lstrHollidays As String
        Try
            lrecHolliday = New eRemoteDB.Execute

            lstrHollidays = "|"

            '+ Definición de parámetros para stored procedure 'insHollidaysList'
            With lrecHolliday
                .StoredProcedure = "insHollidaysList"
                .Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sArrayHollidays", lstrHollidays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run(False) Then
                    strHollidaysArray = .Parameters("sArrayHollidays").Value
                End If
            End With

            Return lrecHolliday
        Catch ex As Exception

        Finally
            'UPGRADE_NOTE: Object lrecHolliday may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecHolliday = Nothing
        End Try
    End Function
End Class







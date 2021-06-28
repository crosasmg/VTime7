Option Strict Off
Option Explicit On
Public Class Valdatconditions
	'%-------------------------------------------------------%'
	'% $Workfile:: Valdatconditions.cls                     $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 6/04/04 1:12p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'Column_name                   Type      Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nId As Integer 'smallint  2           5     0     no                                  (n/a)                               (n/a)
	Public nConcept As Integer 'smallint  2           5     0     no                                  (n/a)                               (n/a)
	Public nBranch As Integer 'smallint  2           5     0     no                                  (n/a)                               (n/a)
	Public nDoctyp As Integer 'smallint  2           5     0     no                                  (n/a)                               (n/a)
	Public nDefaultDat As Integer 'smallint  2           5     0     no                                  (n/a)                               (n/a)
	Public nChangesDat As Integer 'smallint  2           5     0     no                                  (n/a)                               (n/a)
	Public nUsercode As Integer 'smallint  2           5     0     no                                  (n/a)                               (n/a)
	Public dValueDate As Date
	Public dChangesdate As Date
	
	'% Add: Este método crea un registro en la tabla Valdatconditions
	Public Function Add(Optional ByRef nConcept As Integer = eRemoteDB.Constants.intNull, Optional ByRef nBranch As Integer = eRemoteDB.Constants.intNull, Optional ByRef nDoctyp As Integer = eRemoteDB.Constants.intNull, Optional ByRef nDefaultDat As Integer = eRemoteDB.Constants.intNull, Optional ByRef nChangesDat As Integer = eRemoteDB.Constants.intNull, Optional ByRef nUsercode As Integer = eRemoteDB.Constants.intNull) As Boolean
		Dim lreccreValdatconditions As eRemoteDB.Execute
		
        lreccreValdatconditions = New eRemoteDB.Execute

		
		
		If nConcept <> eRemoteDB.Constants.intNull Then
			Me.nConcept = nConcept
		End If
		
		If nBranch > 0 Then
			Me.nBranch = nBranch
		Else
			Me.nBranch = eRemoteDB.Constants.intNull
		End If
		
		If nDoctyp > 0 Then
			Me.nDoctyp = nDoctyp
		Else
			Me.nDoctyp = eRemoteDB.Constants.intNull
		End If
		
		If nDefaultDat > 0 Then
			Me.nDefaultDat = nDefaultDat
		Else
			Me.nDefaultDat = eRemoteDB.Constants.intNull
		End If
		
		If nChangesDat > 0 Then
			Me.nChangesDat = nChangesDat
		Else
			Me.nChangesDat = eRemoteDB.Constants.intNull
		End If
		
		If nUsercode <> eRemoteDB.Constants.intNull Then
			Me.nUsercode = nUsercode
		End If
		
		'+ Definición de parámetros para stored procedure 'creValdatconditions'
		With lreccreValdatconditions
			.StoredProcedure = "creValdatconditions"
			
			.Parameters.Add("nConcept", Me.nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDoctyp", Me.nDoctyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDefaultDat", Me.nDefaultDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChangesDat", Me.nChangesDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Add = True
			End If
		End With
		'UPGRADE_NOTE: Object lreccreValdatconditions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreValdatconditions = Nothing
		
	End Function
	
	'% Update: Este método actualiza un registro en la tabla Valdatconditions
	Public Function Update(Optional ByRef nId As Integer = eRemoteDB.Constants.intNull, Optional ByRef nConcept As Integer = eRemoteDB.Constants.intNull, Optional ByRef nBranch As Integer = eRemoteDB.Constants.intNull, Optional ByRef nDoctyp As Integer = eRemoteDB.Constants.intNull, Optional ByRef nDefaultDat As Integer = eRemoteDB.Constants.intNull, Optional ByRef nChangesDat As Integer = eRemoteDB.Constants.intNull, Optional ByRef nUsercode As Integer = eRemoteDB.Constants.intNull) As Boolean
		Dim lrecupdValdatconditions As eRemoteDB.Execute
		
		lrecupdValdatconditions = New eRemoteDB.Execute
		
		If nId <> eRemoteDB.Constants.intNull Then
			Me.nId = nId
		End If
		
		If nConcept <> eRemoteDB.Constants.intNull Then
			Me.nConcept = nConcept
		End If
		
		If nBranch <> eRemoteDB.Constants.intNull Then
			Me.nBranch = nBranch
		End If
		
		If nDoctyp <> eRemoteDB.Constants.intNull Then
			Me.nDoctyp = nDoctyp
		End If
		
		If nDefaultDat <> eRemoteDB.Constants.intNull Then
			Me.nDefaultDat = nDefaultDat
		End If
		
		If nChangesDat <> eRemoteDB.Constants.intNull Then
			Me.nChangesDat = nChangesDat
		End If
		
		If nUsercode <> eRemoteDB.Constants.intNull Then
			Me.nUsercode = nUsercode
		End If
		
		'+ Definición de parámetros para stored procedure 'updValdatconditions'
		With lrecupdValdatconditions
			.StoredProcedure = "updValdatconditions"
			.Parameters.Add("nId", Me.nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", Me.nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDoctyp", Me.nDoctyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDefaultDat", Me.nDefaultDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChangesDat", Me.nChangesDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
			End If
		End With
		'UPGRADE_NOTE: Object lrecupdValdatconditions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdValdatconditions = Nothing
		
	End Function
	
	'% Delete: Este método elimina un registro en la tabla Valdatconditions
	Public Function Delete(Optional ByRef nId As Integer = eRemoteDB.Constants.intNull) As Boolean
		Dim lrecdelValdatconditions As eRemoteDB.Execute
		
		lrecdelValdatconditions = New eRemoteDB.Execute
		
		If nId <> eRemoteDB.Constants.intNull Then
			Me.nId = nId
		End If
		
		'+ Definición de parámetros para stored procedure 'delValdatconditions'
		With lrecdelValdatconditions
			.StoredProcedure = "delValdatconditions"
			
			.Parameters.Add("nId", Me.nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Delete = True
			End If
		End With
		'UPGRADE_NOTE: Object lrecdelValdatconditions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelValdatconditions = Nothing
		
	End Function
	
	'% Validate:
	Public Function valMOP822_K(ByVal sAction As String, ByVal nConcept As Integer, ByVal nBranch As Integer, ByVal nDoctyp As String, ByVal nDefaultDat As Integer, ByVal nChangesDat As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		'+ Validación del criterio de condiciones
		If nConcept <= 0 And nBranch <= 0 And CDbl(nDoctyp) <= 0 Then
			lobjErrors.ErrorMessage("MOP822", 56032)
		End If
		
		'+ Validación de la fecha por defecto
		If nDefaultDat <= 0 Then
			lobjErrors.ErrorMessage("MOP822", 1012,  , eFunctions.Errors.TextAlign.RigthAling, "(Fecha por defecto)")
		End If
		
		'+ Validación de los cambios permitidos
		If nChangesDat <= 0 Then
			lobjErrors.ErrorMessage("MOP822", 1012,  , eFunctions.Errors.TextAlign.RigthAling, "(Cambios permitidos)")
		End If
		
		valMOP822_K = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insPostMS821: Esta función se encarga de realizar la actualización de la ventana MS821
	Public Function insPostMOP822(ByVal sAction As String, ByVal nId As Integer, ByVal nConcept As Integer, ByVal nBranch As Integer, ByVal nDoctyp As Integer, ByVal nDefaultDat As Integer, ByVal nChangesDat As Integer, ByVal nUsercode As Integer) As Boolean
		Select Case sAction
			Case "Add"
				insPostMOP822 = Add(nConcept, nBranch, nDoctyp, nDefaultDat, nChangesDat, nUsercode)
			Case "Update"
				insPostMOP822 = Update(nId, nConcept, nBranch, nDoctyp, nDefaultDat, nChangesDat, nUsercode)
			Case "Del"
				insPostMOP822 = Delete(nId)
		End Select
	End Function
	
	'% InsFind_ValdatconditionCollect: Este método se encarga de buscar las condiciones de la fecha de valorización
	Public Function InsFind_ValdatconditionCollect(ByVal nConcept As Integer, ByVal nBranch As Integer, ByVal nDoctyp As Integer, ByVal dDefaultDate As Date) As Boolean
		Dim lrecValdatconditions As eRemoteDB.Execute
		
		lrecValdatconditions = New eRemoteDB.Execute
		
		nDefaultDat = eRemoteDB.Constants.intNull
		nChangesDat = eRemoteDB.Constants.intNull
        dValueDate = dtmNull
        dChangesdate = dtmNull



		'+ Definición de parámetros para stored procedure 'updValdatconditions'
		With lrecValdatconditions
			.StoredProcedure = "Find_ValdatConditions_Collect"
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDoctyp", nDoctyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDefaultDate", dDefaultDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDefaultDat", nDefaultDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChangesDat", nChangesDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dValueDate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dChangesdate", dChangesdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run(False) Then
				InsFind_ValdatconditionCollect = True
				nDefaultDat = .Parameters("nDefaultDat").Value
				nChangesDat = .Parameters("nChangesDat").Value
				dValueDate = .Parameters("dValueDate").Value
				dChangesdate = .Parameters("dChangesdate").Value
			End If
		End With
		'UPGRADE_NOTE: Object lrecValdatconditions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValdatconditions = Nothing
		
	End Function
End Class







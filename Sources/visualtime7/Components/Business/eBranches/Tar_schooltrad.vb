Option Strict Off
Option Explicit On
Public Class Tar_schooltrad
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_schooltrad.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	'+ Definición de la tabla TAR_SCHOOLTRAD tomada el 02/11/2001 17:41
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ --------------- - -------- ------- ----- ------ --------
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public dEffecdate As Date ' DATE           7              No
	Public nAge_insu As Integer ' NUMBER        22     5      0 No
	Public nAge_child As Integer ' NUMBER        22     5      0 No
	Public nPeriod_pay As Integer ' NUMBER        22     5      0 Yes
	Public nRate As Double ' NUMBER        22     9      6 Yes
	Public dNulldate As Date ' DATE           7              Yes
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	Public dCompdate As Date ' DATE           7              No
	Private mvarTar_schooltrads As Tar_schooltrads
	Public Property Tar_schooltrads() As Tar_schooltrads
		Get
			If mvarTar_schooltrads Is Nothing Then
				mvarTar_schooltrads = New Tar_schooltrads
			End If
			Tar_schooltrads = mvarTar_schooltrads
		End Get
		Set(ByVal Value As Tar_schooltrads)
			mvarTar_schooltrads = Value
		End Set
	End Property
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarTar_schooltrads may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarTar_schooltrads = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	'% Add: Crea un nuevo registro en la tabla Tar_Schooltrad
	Public Function Add(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAge_insu As Integer, ByVal nAge_child As Integer, ByVal nPeriod_pay As Integer, ByVal nRate As Double, ByVal nUsercode As Integer, Optional ByVal dNulldate As Date = #12:00:00 AM#) As Boolean
		On Error GoTo Add_Err
		Dim lreccreTar_Schooltrad As eRemoteDB.Execute
		
		lreccreTar_Schooltrad = New eRemoteDB.Execute
		
		With lreccreTar_Schooltrad
			.StoredProcedure = "INSUPDTAR_SCHOOLTRAD"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_Insu", nAge_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_child", nAge_child, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriod_pay", nPeriod_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 9, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreTar_Schooltrad may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTar_Schooltrad = Nothing
	End Function
	'% Delete: Elimina un registro de la tabla Tar_Schooltrad
	Public Function Delete(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAge_insu As Integer, ByVal nAge_child As Integer, ByVal nUsercode As Integer) As Boolean
		On Error GoTo Delete_Err
		Dim lrecdelTar_Schooltrad As eRemoteDB.Execute
		
		lrecdelTar_Schooltrad = New eRemoteDB.Execute
		
		With lrecdelTar_Schooltrad
			.StoredProcedure = "INSDELTAR_SCHOOLTRAD"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_insu", nAge_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_child", nAge_child, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelTar_Schooltrad may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTar_Schooltrad = Nothing
	End Function
	'% Update : actualiza la tabla TAR_SCHOOLTRAD
	Public Function Update(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAge_insu As Integer, ByVal nAge_child As Integer, ByVal nPeriod_pay As Integer, ByVal nRate As Double, ByVal nUsercode As Integer, Optional ByVal dNulldate As Date = #12:00:00 AM#) As Boolean
		On Error GoTo Update_Err
		Dim lrecupdTar_Schooltrad As eRemoteDB.Execute
		
		lrecupdTar_Schooltrad = New eRemoteDB.Execute
		
		With lrecupdTar_Schooltrad
			.StoredProcedure = "INSUPDTAR_SCHOOLTRAD"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_Insu", nAge_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_child", nAge_child, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriod_pay", nPeriod_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 9, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdTar_Schooltrad may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTar_Schooltrad = Nothing
	End Function
	'%  Find: Busca un registron dentro de la tabla Tar_schooltrad
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAge_insu As Integer, ByVal nAge_child As Integer) As Boolean
		On Error GoTo Find_Err
		Dim lrecreaTar_Schooltrad As eRemoteDB.Execute
		
		lrecreaTar_Schooltrad = New eRemoteDB.Execute
		
		With lrecreaTar_Schooltrad
			.StoredProcedure = "REATAR_SCHOOLTRAD"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_insu", nAge_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_child", nAge_child, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nPeriod_pay = .FieldToClass("nPeriod_pay")
				nRate = .FieldToClass("nRate")
				dNulldate = .FieldToClass("dNulldate")
				nUsercode = .FieldToClass("nUsercode")
				dCompdate = .FieldToClass("dCompdate")
				Find = True
				.RCloseRec()
			End If
		End With
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTar_Schooltrad may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_Schooltrad = Nothing
	End Function
	'%  InsValMVI771_K: Valida los campos de la zona puntual
	Public Function InsValMVI771_K(ByVal lstrCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		On Error GoTo InsValMVI771_Err
		Dim lobjErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		lobjErrors = New eFunctions.Errors
		
		'+ Valida el campo Ramo
		If nBranch = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(lstrCodispl, 9064)
		End If
		
		'+ Valida el campo Producto
		If nProduct = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(lstrCodispl, 11009)
		End If
		
		'+ Se valida el Campo Fecha
		If dEffecdate = dtmNull Then
			Call lobjErrors.ErrorMessage(lstrCodispl, 1103)
		Else
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActionadd Then
				If Not InsValEffecdate(nBranch, nProduct, dEffecdate) Then
					Call lobjErrors.ErrorMessage(lstrCodispl, 55611)
				End If
			End If
		End If
		
		InsValMVI771_K = lobjErrors.Confirm
		
InsValMVI771_Err: 
		If Err.Number Then
			InsValMVI771_K = "InsValMVI771: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
	End Function
	
	'%InsPostMCA632: Esta función se encarga de crear/actualizar los registros
	'%               correspondientes en la tabla de TAR_SCHOOLTRAD
	Public Function insPostMVI771(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal nAge_insu As Integer = 0, Optional ByVal nAge_child As Integer = 0, Optional ByVal nPeriod_pay As Integer = 0, Optional ByVal nRate As Double = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal dNulldate As Date = #12:00:00 AM#) As Boolean
		On Error GoTo insPostMVI771_err
		insPostMVI771 = True
		Select Case sAction
			Case "Add"
				insPostMVI771 = Me.Add(nBranch, nProduct, dEffecdate, nAge_insu, nAge_child, nPeriod_pay, nRate, nUsercode, dNulldate)
			Case "Update"
				insPostMVI771 = Me.Update(nBranch, nProduct, dEffecdate, nAge_insu, nAge_child, nPeriod_pay, nRate, nUsercode, dNulldate)
			Case "Del"
				insPostMVI771 = Me.Delete(nBranch, nProduct, dEffecdate, nAge_insu, nAge_child, nUsercode)
		End Select
insPostMVI771_err: 
		If Err.Number Then
			insPostMVI771 = False
		End If
		On Error GoTo 0
	End Function
	
	'%  InsValMVI771_K: Valida los campos de la zona masiva
	Public Function InsValMVI771_Upd(ByVal lstrCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal sAction As String = "", Optional ByVal nAge_insu As Integer = 0, Optional ByVal nAge_child As Integer = 0, Optional ByVal nPeriod_pay As Integer = 0, Optional ByVal nRate As Double = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal dNulldate As Date = #12:00:00 AM#) As String
		On Error GoTo InsValMVI771_Err
		Dim lobjErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		lobjErrors = New eFunctions.Errors
		
		'+ Valida campo edad del hijo del asegurado
		If nAge_child = -32768 Then
			Call lobjErrors.ErrorMessage(lstrCodispl, 55656)
		End If
		
		'+ Valida campo edad del asegurado
		If nAge_insu = -32768 Then
			Call lobjErrors.ErrorMessage(lstrCodispl, 6026)
		End If
		
		'+ Valida campo tasa
		If nRate = -32768 Then
			Call lobjErrors.ErrorMessage(lstrCodispl, 2042)
		End If
		
		'+ Valida que no exista registro con la misma clave
		If sAction = "Add" Then
			If Find(nBranch, nProduct, dEffecdate, nAge_insu, nAge_child) Then
				Call lobjErrors.ErrorMessage(lstrCodispl, 55655)
			End If
		End If
		
		InsValMVI771_Upd = lobjErrors.Confirm
		
InsValMVI771_Err: 
		If Err.Number Then
			InsValMVI771_Upd = "InsValMVI771: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción, según error 55611
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTar_Schooltrad As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		lrecreaTar_Schooltrad = New eRemoteDB.Execute
		
		InsValEffecdate = True
		'+ Definición de parámetros para stored procedure 'InsValEffecdate_Tar_Schooltrad'
		With lrecreaTar_Schooltrad
			.StoredProcedure = "InsValEffecdate_Tar_Schooltrad"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsValEffecdate = Not .Run
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTar_Schooltrad may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_Schooltrad = Nothing
	End Function
End Class







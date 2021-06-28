Option Strict Off
Option Explicit On
Public Class Disc_riskInsu
	'%-------------------------------------------------------%'
	'% $Workfile:: Disc_riskInsu.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla DISC_RISKINSU tomada el 07/11/2001 16:14
	
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ --------------- - -------- ------- ----- ------ --------
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public dEffecdate As Date ' DATE           7              No
	Public nCapital_init As Double ' NUMBER        22    18      6 No
	Public nCapital_end As Double ' NUMBER        22    18      6 No
	Public nRate As Double ' NUMBER        22     9      6 No
	Public dNulldate As Date ' DATE           7              Yes
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	
	Private mvarDisc_riskInsus As Disc_riskInsus
	Public Property Disc_riskInsus() As Disc_riskInsus
		Get
			If mvarDisc_riskInsus Is Nothing Then
				mvarDisc_riskInsus = New Disc_riskInsus
			End If
			
			
			Disc_riskInsus = mvarDisc_riskInsus
		End Get
		Set(ByVal Value As Disc_riskInsus)
			mvarDisc_riskInsus = Value
		End Set
	End Property
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarDisc_riskInsus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarDisc_riskInsus = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Add: Crea un nuevo registro en la tabla Disc_riskInsu
	Public Function Add(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCapital_init As Double, ByVal nCapital_end As Double, ByVal nRate As Double, ByVal nUsercode As Integer) As Boolean
		Dim lreccreDisc_riskInsu As eRemoteDB.Execute
		
		lreccreDisc_riskInsu = New eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		With lreccreDisc_riskInsu
			.StoredProcedure = "INSUPDDISC_RISKINSU"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_init", nCapital_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_end", nCapital_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreDisc_riskInsu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreDisc_riskInsu = Nothing
		On Error GoTo 0
	End Function
	
	'% Delete: Elimina un registro de la tabla Disc_riskInsu
	Public Function Delete(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCapital_init As Double) As Boolean
		Dim lrecdelDisc_riskInsu As eRemoteDB.Execute
		
		lrecdelDisc_riskInsu = New eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		With lrecdelDisc_riskInsu
			.StoredProcedure = "DELDISC_RISKINSU"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_init", nCapital_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecdelDisc_riskInsu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelDisc_riskInsu = Nothing
		On Error GoTo 0
		
	End Function
	
	'%  Find: Busca un registron dentro de la tabla Disc_riskInsu
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCapital_init As Double) As Boolean
		Dim lrecreaDisc_riskInsu As eRemoteDB.Execute
		
		lrecreaDisc_riskInsu = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lrecreaDisc_riskInsu
			.StoredProcedure = "READISC_RISKINSU"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_init", nCapital_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nBranch = .FieldToClass("nBranch")
				nProduct = .FieldToClass("nProduct")
				dEffecdate = .FieldToClass("dEffecdate")
				nCapital_init = .FieldToClass("nCapital_init")
				nCapital_end = .FieldToClass("nCapital_end")
				nRate = .FieldToClass("nRate")
				dNulldate = .FieldToClass("dNulldate")
				nUsercode = .FieldToClass("nUsercode")
				Find = True
				.RCloseRec()
			End If
		End With
Find_Err: 
		If Err.Number Then Find = False
		'UPGRADE_NOTE: Object lrecreaDisc_riskInsu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDisc_riskInsu = Nothing
	End Function
	
	'%  Update: Actualiza un registro dentro de la tabla Disc_riskInsu
	Public Function Update(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCapital_init As Double, ByVal nCapital_end As Double, ByVal nRate As Double, ByVal nUsercode As Integer) As Boolean
		Dim lrecupdDisc_riskInsu As eRemoteDB.Execute
		
		lrecupdDisc_riskInsu = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		With lrecupdDisc_riskInsu
			.StoredProcedure = "INSUPDDISC_RISKINSU"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_init", nCapital_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_end", nCapital_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecupdDisc_riskInsu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdDisc_riskInsu = Nothing
		On Error GoTo 0
		
	End Function
	
	'% InsPostMVI805: Esta función se encarga de crear/actualizar los registros
	'% correspondientes en la tabla de Disc_riskInsu
	Public Function insPostMVI805(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCapital_init As Double, ByVal nCapital_end As Double, ByVal nRate As Double, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostMVI805_err
		insPostMVI805 = True
		Select Case sAction
			Case "Add"
				insPostMVI805 = Me.Add(nBranch, nProduct, dEffecdate, nCapital_init, nCapital_end, nRate, nUsercode)
			Case "Update"
				insPostMVI805 = Me.Update(nBranch, nProduct, dEffecdate, nCapital_init, nCapital_end, nRate, nUsercode)
			Case "Del"
				insPostMVI805 = Me.Delete(nBranch, nProduct, dEffecdate, nCapital_init)
		End Select
insPostMVI805_err: 
		If Err.Number Then
			insPostMVI805 = False
		End If
		On Error GoTo 0
	End Function
	
	'%  InsValMVI805_K: Valida los campos de la zona puntual MVI805_K
	Public Function InsValMVI805_K(ByRef sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lobjProduct As eProduct.Product
		Dim lbError As Boolean
		
		On Error GoTo InsValMVI805_Err
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			
			'+ Se valida el Campo Ramo
			If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
				lbError = True
				.ErrorMessage(sCodispl, 9064)
			End If
			
			'+ Se valida el Producto
			If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
				lbError = True
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Producto: ")
			End If
			
			'+ Se valida ramo/producto que sea de Vida
			If Not lbError Then
				lobjProduct = New eProduct.Product
				Call lobjProduct.FindProdMaster(nBranch, nProduct)
				If CStr(lobjProduct.sBrancht) <> "1" Then
					Call .ErrorMessage(sCodispl, 1024)
				End If
			End If
			
			'+ Se valida el Campo Fecha
			If dEffecdate = dtmNull Then
				lbError = True
				Call .ErrorMessage(sCodispl, 1103)
			Else
				If (sAction = "301" Or sAction = "302") And Not lbError Then
					If Not InsValEffecdate(nBranch, nProduct, dEffecdate) Then
						Call .ErrorMessage(sCodispl, 55611)
					End If
				End If
			End If
			
			
			InsValMVI805_K = .Confirm
			
		End With
InsValMVI805_Err: 
		If Err.Number Then
			InsValMVI805_K = "InsValMVI805: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lobjProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjProduct = Nothing
		On Error GoTo 0
	End Function
	
	'%  InsValMVI805_Upd: Valida los campos de la zona Masiva MVI805
	Public Function InsValMVI805_Upd(ByVal sCodispl As String, ByVal Action As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCapital_init As Double, ByVal nCapital_end As Double, ByVal nRate As Double, ByVal nUsercode As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo InsValMVI805_Err
		lobjErrors = New eFunctions.Errors
		With lobjErrors
			If nRate = eRemoteDB.Constants.intNull Or nRate = 0 Then
				Call .ErrorMessage(sCodispl, 2042)
			Else
				If nCapital_init > nCapital_end Then
					Call .ErrorMessage(sCodispl, 11113)
				Else
					If Action <> "Update" Then
						If Not InsValRange(nBranch, nProduct, dEffecdate, nCapital_init, nCapital_end) Then
							Call .ErrorMessage(sCodispl, 55659)
						End If
					End If
				End If
			End If
			InsValMVI805_Upd = .Confirm
		End With
		
InsValMVI805_Err: 
		If Err.Number Then
			InsValMVI805_Upd = "InsValMVI805: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValRange: Valida que el rango indicado no este dentro de otro rango
	Public Function InsValRange(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCapital_init As Double, ByVal nCapital_end As Double) As Boolean
		Dim lrecRatings As eRemoteDB.Execute
		
		On Error GoTo InsValRange_Err
		
		InsValRange = True
		lrecRatings = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ValDisc_RiskInsu'
		With lrecRatings
			.StoredProcedure = "VALDISC_RISKINSU"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_init", nCapital_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_end", nCapital_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				InsValRange = False
				.RCloseRec()
			End If
		End With
		
InsValRange_Err: 
		If Err.Number Then
			InsValRange = False
		End If
		'UPGRADE_NOTE: Object lrecRatings may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRatings = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción, según error 55611
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaDisc_riskInsu As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		lrecreaDisc_riskInsu = New eRemoteDB.Execute
		
		InsValEffecdate = True
		
		'+Definición de parámetros para stored procedure 'InsValEffecdate_Disc_riskInsu'
		With lrecreaDisc_riskInsu
			.StoredProcedure = "InsValEffecdate_Disc_riskInsu"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsValEffecdate = Not .Run
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		'UPGRADE_NOTE: Object lrecreaDisc_riskInsu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDisc_riskInsu = Nothing
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nCapital_init = eRemoteDB.Constants.intNull
		nCapital_end = eRemoteDB.Constants.intNull
		nRate = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class







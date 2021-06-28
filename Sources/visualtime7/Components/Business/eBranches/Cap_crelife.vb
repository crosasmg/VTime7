Option Strict Off
Option Explicit On
Public Class Cap_crelife
	'%-------------------------------------------------------%'
	'% $Workfile:: Cap_crelife.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on October 24,2001.
	'*-Propiedades según la tabla en el sistema el 24/10/2001
	
	'Column_name                 Type                  Nulldeable
	'---------------------   ------------------------ ---------------
	Public nBranch As Integer 'Number(5)       No
	Public nProduct As Integer 'Number(5)       No
	Public nModulec As Integer 'Number(5)       No
	Public nCover As Integer 'Number(5)       No
	Public nDuration As Integer 'Number(5)       No
	Public nYear As Integer 'Number(5)       No
	Public dEffecdate As Date 'Date            No
	Public nCapital As Double 'Number(18,6)    Yes
	Public dNulldate As Date 'Date            Yes
	Public dCompdate As Date 'Date            No
	Public nUsercode As Integer 'Number(5)       Yes
	Public nCurrency As Integer 'Number(5)       Yes
	Private mvarCap_crelifes As Cap_crelifes
	
	
	Public Property Cap_crelifes() As Cap_crelifes
		Get
			If mvarCap_crelifes Is Nothing Then
				mvarCap_crelifes = New Cap_crelifes
			End If
			Cap_crelifes = mvarCap_crelifes
		End Get
		Set(ByVal Value As Cap_crelifes)
			mvarCap_crelifes = Value
		End Set
	End Property
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarCap_crelifes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarCap_crelifes = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%InsUpdCap_crelife: Crea un registro en la tabla
	Private Function InsUpdCap_crelife(ByVal nAction As Integer) As Boolean
		Dim lrecinsupdcap_crelife As eRemoteDB.Execute
		
		On Error GoTo insupdcap_crelife_Err
		
		lrecinsupdcap_crelife = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'InsUpdCap_crelife'
		'**+Information read on October 24,2001 11:58:10 a.m.
		'+Definición de parámetros para stored procedure 'InsUpdCap_crelife'
		'+Información leída el 24/10/2001 11:58:10 AM
		With lrecinsupdcap_crelife
			.StoredProcedure = "InsUpdCap_crelife"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDuration", nDuration, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdCap_crelife = .Run(False)
		End With
		
insupdcap_crelife_Err: 
		If Err.Number Then
			InsUpdCap_crelife = False
		End If
		'UPGRADE_NOTE: Object lrecinsupdcap_crelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsupdcap_crelife = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdCap_crelife(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdCap_crelife(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdCap_crelife(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nDuration As Integer, ByVal nYear As Integer, ByVal dEffecdate As Date, Optional ByVal lbFind As Boolean = False) As Boolean
		Dim lrecReaCap_crelife As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nModulec <> nModulec Or Me.nCover <> nCover Or Me.nDuration <> nDuration Or Me.nYear <> nYear Or Me.dEffecdate <> dEffecdate Or lbFind Then
			
			lrecReaCap_crelife = New eRemoteDB.Execute
			
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.nModulec = nModulec
			Me.nCover = nCover
			Me.nDuration = nDuration
			Me.nYear = nYear
			Me.dEffecdate = dEffecdate
			
			'+Definición de parámetros para stored procedure 'reacap_crelife_by_year'
			With lrecReaCap_crelife
				.StoredProcedure = "reacap_crelife_by_year"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDuration", nDuration, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nModulec = nModulec
					Me.nCover = nCover
					Me.nDuration = .FieldToClass("nDuration")
					Me.nYear = .FieldToClass("nYear")
					Me.dEffecdate = dEffecdate
					Me.nCapital = .FieldToClass("nCapital")
					Me.dNulldate = .FieldToClass("dNulldate")
					Me.nCurrency = .FieldToClass("nCurrency")
					Find = True
					.RCloseRec()
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaCap_crelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCap_crelife = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción, según error 55611
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaCap_crelife As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		lrecReaCap_crelife = New eRemoteDB.Execute
		
		InsValEffecdate = True
		'+Definición de parámetros para stored procedure 'InsValEffecdate_Cap_crelife'
		With lrecReaCap_crelife
			.StoredProcedure = "InsValEffecdate_Cap_crelife"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsValEffecdate = Not .Run
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		'UPGRADE_NOTE: Object lrecReaCap_crelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCap_crelife = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMVI757_K: Validaciones de la transacción(Header)
	'%                Tabla de capitales crecientes(MVI757)
	Public Function InsValMVI757_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsvalField As eFunctions.Values
		Dim lclsProduct As Object
		Dim lblnModulec As Boolean
		Dim lbError As Boolean
		
		On Error GoTo InsValMVI757_K_Err
		lclsErrors = New eFunctions.Errors
		lclsvalField = New eFunctions.Values
		lclsProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
		lbError = False
		
		With lclsErrors
			
			'+ Se valida el Campo Ramo
			If nBranch = eRemoteDB.Constants.intNull Then
				lbError = True
				.ErrorMessage(sCodispl, 9064)
			End If
			
			'+ Se valida el Campo Producto
			If nProduct = eRemoteDB.Constants.intNull Then
				lbError = True
				.ErrorMessage(sCodispl, 11009)
			End If
			
			'+ Se valida que sea un producto de vida
			If Not lbError Then
				lclsProduct = New eProduct.Product
				Call lclsProduct.FindProdMaster(nBranch, nProduct)
				If lclsProduct.sBrancht <> "1" Then
					.ErrorMessage(sCodispl, 1024)
				End If
			End If
			
			'+ Se valida el Campo Módulo
			If Not lbError Then
				lblnModulec = lclsProduct.IsModule(nBranch, nProduct, dEffecdate)
				If lblnModulec Then
					If nModulec = eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 1901)
					Else
						lclsvalField.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lclsvalField.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lclsvalField.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						If Not lclsvalField.IsValid("tabTab_modul", CStr(nModulec), True) Then
							.ErrorMessage(sCodispl, 11117)
						End If
					End If
				Else
					If nModulec > 0 Then
						.ErrorMessage(sCodispl, 11344)
					End If
				End If
			End If
			
			'+ Se valida el Campo Cobertura
			If nCover = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11163)
			End If
			
			'+ Se valida el Campo Fecha
			If dEffecdate = dtmNull Then
				.ErrorMessage(sCodispl, 1103)
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					If Not InsValEffecdate(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
						.ErrorMessage(sCodispl, 55611)
					End If
				End If
			End If
			
			InsValMVI757_K = .Confirm
		End With
		
InsValMVI757_K_Err: 
		If Err.Number Then
			InsValMVI757_K = "InsValMVI757_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalField = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMVI757: Validaciones de la transacción
	'%              Tabla de capitales crecientes(MVI757)
	Public Function InsValMVI757(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nDuration As Integer, ByVal nYear As Integer, ByVal nCapital As Double, ByVal nCurrency As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValTable As eFunctions.Values
		
		On Error GoTo InsValMVI757_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Se valida el campo Período de pago de primas
			If nDuration = eRemoteDB.Constants.intNull Or nDuration = 0 Then
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Período de pago de primas: ")
			End If
			
			'+ Se valida el campo Año(s)
			If nYear = eRemoteDB.Constants.intNull Or nYear = 0 Then
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Año(s): ")
			End If
			
			If nDuration <> eRemoteDB.Constants.intNull And nDuration <> 0 And nYear <> eRemoteDB.Constants.intNull And nYear <> 0 Then
				If sAction = "Add" Then
					If Find(nBranch, nProduct, nModulec, nCover, nDuration, nYear, dEffecdate, True) Then
						.ErrorMessage(sCodispl, 55616)
					End If
				End If
			End If
			
			'+ Se valida el campo Factor capital
			If nCapital = eRemoteDB.Constants.intNull Or nCapital = 0 Then
				.ErrorMessage(sCodispl, 3819)
			End If
			
			'+ Se valida el campo Moneda
			If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
				.ErrorMessage(sCodispl, 10107)
			End If
			
			InsValMVI757 = .Confirm
		End With
		
InsValMVI757_Err: 
		If Err.Number Then
			InsValMVI757 = "InsValMVI757: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValTable may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValTable = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMVI757: Ejecuta el post de la transacción
	'%               Capitales crecientes(MVI757)
	Public Function InsPostMVI757(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nDuration As Integer, ByVal nYear As Integer, ByVal dEffecdate As Date, ByVal nCapital As Double, ByVal dNulldate As Date, ByVal nUsercode As Integer, ByVal nCurrency As Integer) As Boolean
		
		On Error GoTo InsPostMVI757_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
			.nCover = nCover
			.nDuration = nDuration
			.nYear = nYear
			.dEffecdate = dEffecdate
			.nCapital = nCapital
			.dNulldate = dNulldate
			.nUsercode = nUsercode
			.nCurrency = nCurrency
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMVI757 = Add
			Case "Update"
				InsPostMVI757 = Update
			Case "Del"
				InsPostMVI757 = Delete
		End Select
		
InsPostMVI757_Err: 
		If Err.Number Then
			InsPostMVI757 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		nDuration = eRemoteDB.Constants.intNull
		nYear = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nCapital = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class







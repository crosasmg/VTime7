Option Strict Off
Option Explicit On
Public Class Tab_Interest
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Interest.cls                         $%'
	'% $Author:: Nvaplat32                                  $%'
	'% $Date:: 16/10/03 19:55                               $%'
	'% $Revision:: 17                                       $%'
	'%-------------------------------------------------------%'
	
	'-Estructura de tabla Tab_Interest al 11-09-2001 16:26:03
	'-     Property           Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nTypeinvest As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nWarint As Double ' NUMBER     22  10    13    N
	Private mlngUsercode As Integer ' NUMBER     22   0     5    N
	
	'%InsUpdTab_Interest: Se encarga de actualizar la tabla Tab_Interest
	Private Function InsUpdTab_Interest(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdTab_Interest As eRemoteDB.Execute
		
		On Error GoTo InsUpdTab_Interest_Err
		lrecInsUpdTab_Interest = New eRemoteDB.Execute
		'+ Definición de store procedure InsUpdTab_Interest al 11-09-2001 18:13:43
		With lrecInsUpdTab_Interest
			.StoredProcedure = "InsUpdTab_Interest"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeinvest", nTypeinvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWarint", nWarint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 10, 13, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdTab_Interest = .Run(False)
		End With
		
InsUpdTab_Interest_Err: 
		If Err.Number Then
			InsUpdTab_Interest = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdTab_Interest may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdTab_Interest = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTab_Interest(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTab_Interest(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTab_Interest(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nTypeinvest As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaTab_interest As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nModulec <> nModulec Or Me.nTypeinvest <> nTypeinvest Or Me.dEffecdate <> dEffecdate Or bFind Then
			
			lrecreaTab_interest = New eRemoteDB.Execute
			'+ Definición de store procedure ReaTab_Interest al 11-09-2001 18:11:50
			With lrecreaTab_interest
				.StoredProcedure = "ReaTab_Interest"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTypeinvest", nTypeinvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nModulec = nModulec
					Me.nTypeinvest = nTypeinvest
					Me.dEffecdate = dEffecdate
					Me.nWarint = .FieldToClass("nWarint")
					Find = True
					.RCloseRec()
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_interest may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_interest = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValEffecdate: Valida que fecha de efecto de transacción sea mayor al
	'%                  del registro mas reciente
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nTypeinvest As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecInsValEffecdate_Tab_Interest As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		
		lrecInsValEffecdate_Tab_Interest = New eRemoteDB.Execute
		'+ Definición de store procedure InsValEffecdate_Tab_Interest al 11-09-2001 18:14:51
		With lrecInsValEffecdate_Tab_Interest
			.StoredProcedure = "InsValEffecdate_Tab_Interest"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeinvest", nTypeinvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Call .Run(False)
			InsValEffecdate = .Parameters("nExist").Value = 0
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		'UPGRADE_NOTE: Object lrecInsValEffecdate_Tab_Interest may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValEffecdate_Tab_Interest = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMVA619_K: Validaciones de la transacción(Header)
	Public Function InsValMVA619_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nTypeinvest As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As Object
		Dim lblnError As Boolean
		
		On Error GoTo InsValMVA619_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 9064)
				lblnError = True
			End If
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11009)
				lblnError = True
			End If
			
			If nTypeinvest = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60173)
			End If
			
			If Not lblnError Then
				lclsProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
				If lclsProduct.IsModule(nBranch, nProduct, dEffecdate) Then
					If nModulec = eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 12112)
					End If
				End If
			End If
			InsValMVA619_K = .Confirm
		End With
		
InsValMVA619_K_Err: 
		If Err.Number Then
			InsValMVA619_K = "InsValMVA619_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMVA619: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(MVA619)
	Public Function InsValMVA619(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nTypeinvest As Integer, ByVal dEffecdate As Date, ByVal nWarint As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMVA619_Err
		lclsErrors = New eFunctions.Errors
		nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
		With lclsErrors
			
			If dEffecdate = dtmNull Then
				.ErrorMessage(sCodispl, 2056)
			End If
			
			If nWarint = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60179)
			End If
			
			If sAction = "Add" Then
				'+ Validar que fecha sea posterior a la mayor
				If Not InsValEffecdate(nBranch, nProduct, nModulec, nTypeinvest, dEffecdate) Then
					.ErrorMessage(sCodispl, 60178)
				End If
			End If
			
			
			InsValMVA619 = .Confirm
		End With
		
InsValMVA619_Err: 
		If Err.Number Then
			InsValMVA619 = "InsValMVA619: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMVA619: Ejecuta el post de la transacción MVA619
	Public Function InsPostMVA619(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nTypeinvest As Integer, ByVal dEffecdate As Date, ByVal nWarint As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMVA619_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
			.nTypeinvest = nTypeinvest
			.dEffecdate = dEffecdate
			.nWarint = nWarint
			mlngUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMVA619 = Add
			Case "Update"
				InsPostMVA619 = Update
			Case "Del"
				InsPostMVA619 = Delete
		End Select
		
InsPostMVA619_Err: 
		If Err.Number Then
			InsPostMVA619 = False
		End If
		On Error GoTo 0
	End Function
	
	'%insval_cap_index: Verifica la existencia de las tasas de interes para todos
	'% los ndices de inversion apv para la fecha indicada
	Public Function insVal_Cap_Index(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nIndex_table As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecinsval_cap_index As eRemoteDB.Execute
		
		On Error GoTo insval_cap_index_Err
		
		lrecinsval_cap_index = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure 'insval_cap_index'
		'**+ The Information was read on  25/08/2003
		
		'+ Definición de parámetros para stored procedure 'insval_cap_index'
		'+ Información leída el: 25/08/2003
		
		With lrecinsval_cap_index
			.StoredProcedure = "insval_cap_index"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndex_table", nIndex_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insVal_Cap_Index = .Parameters("nExist").Value = 1
			End If
		End With
		
insval_cap_index_Err: 
		If Err.Number Then
			insVal_Cap_Index = False
		End If
		'UPGRADE_NOTE: Object lrecinsval_cap_index may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsval_cap_index = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nTypeinvest = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nWarint = eRemoteDB.Constants.intNull
		mlngUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class







Option Strict Off
Option Explicit On
Public Class Durpay_prod
	'%-------------------------------------------------------%'
	'% $Workfile:: Durpay_prod.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Column_name             Type                     Nulldeable
	'+ ---------------------   ------------------------ ---------------
	Public nId As Integer 'Number(5)       No
	Public nBranch As Integer 'Number(5)       No
	Public nProduct As Integer 'Number(5)       No
	Public dEffecdate As Date 'Date            No
	Public nIdurafix As Integer 'Number(5)       Yes
	Public nPdurafix As Integer 'Number(5)       Yes
	Public nUsercode As Integer 'Number(5)       No
	Public nTypdurins As Integer 'Number(5)       No
	Public nTypdurpay As Integer 'Number(5)       No
	
	'% insUpdDurpay_prod: realiza las actualizaciones de los registros en la tabla
	Private Function insUpdDurpay_prod(ByVal nAction As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo insUpdDurpay_prod_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "insUpdDurpay_prod"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nID", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdurafix", nIdurafix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPdurafix", nPdurafix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdurins", nTypdurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdurpay", nTypdurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdDurpay_prod = .Run(False)
		End With
		
insUpdDurpay_prod_err: 
		If Err.Number Then
			insUpdDurpay_prod = False
		End If
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		On Error GoTo 0
	End Function
	
	'% Add: Crea un registro en la tabla
	Private Function Add() As Boolean
		Add = insUpdDurpay_prod(1)
	End Function
	
	'% Update: Actualiza un registro en la tabla
	Private Function Update() As Boolean
		Update = insUpdDurpay_prod(2)
	End Function
	
	'% Delete: Borra un registro en la tabla
	Private Function Delete() As Boolean
		Delete = insUpdDurpay_prod(3)
	End Function
	
	'% DeleteAll: Borra todos los registros de la tabla, para un producto
	Public Function DeleteAll(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
		End With
		
		DeleteAll = insUpdDurpay_prod(4)
	End Function
	
	'% Find: Lee los datos de la tabla
	Private Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nIdurafix As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nIdurafix <> nIdurafix Or Me.dEffecdate <> dEffecdate Or lblnFind Then
			
			lclsRemote = New eRemoteDB.Execute
			
			'+Definición de parámetros para stored procedure 'reaDurpay_prod'
			With lclsRemote
				.StoredProcedure = "reaDurpay_prod"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIdurafix", nIdurafix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nIdurafix = nIdurafix
					Me.dEffecdate = dEffecdate
					Find = True
					.RCloseRec()
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValDP043UPD: Validaciones de la transacción
	Public Function InsValDP043UPD(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sIdurvari As String, ByVal nIdurafix As Integer, ByVal nPdurafix As Integer, ByVal dEffecdate As Date, ByVal nTypdurins As Integer, ByVal nTypdurpay As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValTable As eFunctions.Values
		
		On Error GoTo InsValDP043UPD_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If sIdurvari = "2" Then
				'+ El campo Pagos - Tiempo - Tipo de duración del seguro, debe estar lleno
				If nTypdurins = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage("DP043", 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Tipo de duración del seguro:")
				End If
				
				'+ Si la duración del seguro es fija, se debe indicar duración del seguro para el pago
				If nIdurafix = eRemoteDB.Constants.intNull Then
					.ErrorMessage("DP043", 55967)
				End If
			End If
			
			'+ Se valida que no exista el registro en la tabla
			If insvalExists(nBranch, nProduct, nIdurafix, nPdurafix, dEffecdate, nTypdurins, nTypdurpay) Then
				.ErrorMessage(sCodispl, 60481)
			End If
			
			'+ El campo Pagos - Tiempo - Tipo de duración de los pagos, debe estar lleno
			If nTypdurpay = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage("DP043", 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Tipo de duración de los pagos:")
			End If
			
			'+ La cantidad del pago debe estar lleno
			If nPdurafix = eRemoteDB.Constants.intNull Then
				.ErrorMessage("DP043", 11191)
			End If
			
			InsValDP043UPD = .Confirm
		End With
		
InsValDP043UPD_Err: 
		If Err.Number Then
			InsValDP043UPD = "InsValDP043UPD: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValTable may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValTable = Nothing
		On Error GoTo 0
	End Function
	
	'% InsPostDP043UPD: Actualiza los datos para la duración de los pagos
	Public Function InsPostDP043UPD(ByVal sAction As String, ByVal nId As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nIdurafix As Integer, ByVal nPdurafix As Integer, ByVal nTypdurins As Integer, ByVal nTypdurpay As Integer, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostDP043UPD_Err
		
		With Me
			.nId = nId
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nIdurafix = nIdurafix
			.nPdurafix = nPdurafix
			.nTypdurins = nTypdurins
			.nTypdurpay = nTypdurpay
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostDP043UPD = Add
			Case "Update"
				InsPostDP043UPD = Update
			Case "Del"
				InsPostDP043UPD = Delete
		End Select
		
InsPostDP043UPD_Err: 
		If Err.Number Then
			InsPostDP043UPD = False
		End If
		On Error GoTo 0
	End Function
	
	'% insvalExists: verifica que la duración del pago no se encuentre en la tabla
	Private Function insvalExists(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nIdurafix As Integer, ByVal nPdurafix As Integer, ByVal dEffecdate As Date, ByVal nTypdurins As Integer, ByVal nTypdurpay As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo valExists_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "valDurpay_prod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdurafix", nIdurafix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPdurafix", nPdurafix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdurins", nTypdurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdurpay", nTypdurpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insvalExists = IIf(.Parameters("nExist").Value = 1, True, False)
			End If
		End With
		
valExists_Err: 
		If Err.Number Then
			insvalExists = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nId = 1
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nIdurafix = eRemoteDB.Constants.intNull
		nPdurafix = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class







Option Strict Off
Option Explicit On
Public Class Ord_type
	'%-------------------------------------------------------%'
	'% $Workfile:: Ord_type.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'- Definición de la tabla ORD_TYPE tomada el 05/04/2002 12:27
	'- Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ --------------- - -------- ------- ----- ------ --------
	Public nCurrency As Integer ' NUMBER        22     5      0 No
	Public dEffecdate As Date ' DATE           7              No
	Public nOrd_typeCost As Integer ' NUMBER        22     1      0 No
	Public nAmount As Double ' NUMBER        22    10      2 No
	Public dNulldate As Date ' DATE           7              Yes
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	
	Public nExist As Integer
	Public lintExist As Integer
	
	Private Const cintActionAdd As Short = 1
	Private Const cintActionUpdate As Short = 2
	Private Const cintActionDel As Short = 3
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdOrd_type(cintActionAdd)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdOrd_type(cintActionUpdate)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdOrd_type(cintActionDel)
	End Function
	
	'%InsValOrd_type: Lee los datos de la tabla
	Public Function InsValOrd_type(ByVal nAction As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nOrd_typeCost As Integer, ByVal nExist As Double) As Boolean
		Dim lrecreaOrd_type_v As eRemoteDB.Execute
		
		On Error GoTo reaOrd_type_v_Err
		
		lrecreaOrd_type_v = New eRemoteDB.Execute
		
		With lrecreaOrd_type_v
			.StoredProcedure = "reaOrd_type_v"
			With .Parameters
				.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nOrd_typeCost", nOrd_typeCost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			
			If .Run(False) Then
				InsValOrd_type = True
				lintExist = .Parameters("nExist").Value
			End If
		End With
		
reaOrd_type_v_Err: 
		If Err.Number Then
			InsValOrd_type = False
		End If
		lrecreaOrd_type_v = Nothing
		On Error GoTo 0
	End Function
	
	'%insValMOS661_k: Esta función se encarga de validar los datos del encabezado
	'% de la transacción Tipos de órdenes de servicios profesionales
	Public Function insValMOS661_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date) As String
		'- Se definen los objetos para el manejo de las clases
		
		Dim lclsErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		Dim lblnError As Boolean
		
		lclsErrors = New eFunctions.Errors
		lobjValues = New eFunctions.Values
		
		On Error GoTo insValMOS661_k_Err
		lblnError = False
		
		'+ Validación de la moneda
		With lclsErrors
			If nCurrency <= 0 Then
				lblnError = True
				Call .ErrorMessage(sCodispl, 10107)
			End If
			
			'+ Validación de fecha de efecto
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				lblnError = True
				Call .ErrorMessage(sCodispl, 2056)
			End If
			
			'+ Validacion de fecha de actualización
			If Not lblnError Then
				If nMainAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					If Find_Date_Greater(nCurrency, dEffecdate) Then
						Call .ErrorMessage(sCodispl, 1021)
					End If
				End If
			End If
			insValMOS661_k = .Confirm
		End With
		
insValMOS661_k_Err: 
		If Err.Number Then
			insValMOS661_k = "insValMOS661_k: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lobjValues = Nothing
	End Function
	
	'%Find_Date_Greater Valida la fecha de efecto de la transacción
	Public Function Find_Date_Greater(ByVal nCurrency As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaOrd_type As eRemoteDB.Execute
		Dim nExist As Integer
		
		On Error GoTo reaOrd_type_v_Err
		
		lrecreaOrd_type = New eRemoteDB.Execute
		
		With lrecreaOrd_type
			.StoredProcedure = "ReaOrd_type_date"
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Find_Date_Greater = (.Parameters("nExist").Value = 1)
			Else
				Find_Date_Greater = False
			End If
		End With
		
reaOrd_type_v_Err: 
		If Err.Number Then
			Find_Date_Greater = False
		End If
		lrecreaOrd_type = Nothing
		On Error GoTo 0
	End Function
	
	'%insValMOS661: Esta función se encarga de validar los datos del Form
	'% Tipos de órdenes de servicios profesionales
	Public Function insValMOS661(ByVal sCodispl As String, ByVal sAction As String, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nOrd_typeCost As Integer, ByRef nAmount As Double) As String
		'- Se define el objeto para el manejo de las clases
		
		Dim lclsErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		Dim lblnError As Boolean
		
		lclsErrors = New eFunctions.Errors
		lobjValues = New eFunctions.Values
		
		On Error GoTo insValMOS661_Err
		lblnError = False
		
		'+ Validación del tipo de orden de servicio
		With lclsErrors
			If nOrd_typeCost <= 0 Then
				lblnError = True
				Call .ErrorMessage(sCodispl, 55667,  , eFunctions.Errors.TextAlign.LeftAling, "Tipo de orden: ")
			End If
			
			'+ Validación de Costo
			If nAmount <= 0 Then
				Call .ErrorMessage(sCodispl, 55668)
			End If
			
			'+ Validación de duplicidad Moneda/Fecha Efecto/Tipo Orden de servicio
			'+ al agregar una fila
			If sAction = "Add" Then
				If Not lblnError Then
					lintExist = 0
					Call InsValOrd_type(cintActionAdd, nCurrency, dEffecdate, nOrd_typeCost, lintExist)
					If lintExist = 1 Then
						Call .ErrorMessage(sCodispl, 10171,  , eFunctions.Errors.TextAlign.LeftAling, "Tipo de orden: ")
					End If
				End If
			End If
			
			'+ Validación de existencia de Ordenes de Servicios del tipo
			'+ Moneda/Fecha Efecto/Tipo Orden de servicio a borrar
			If sAction = "Del" Then
				If Not lblnError Then
					lintExist = 0
					Call InsValOrd_type(cintActionDel, nCurrency, dEffecdate, nOrd_typeCost, lintExist)
					If lintExist = 2 Then
						Call .ErrorMessage(sCodispl, 55669)
					End If
				End If
			End If
			insValMOS661 = .Confirm
		End With
		
insValMOS661_Err: 
		If Err.Number Then
			insValMOS661 = "insValMOS661: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lobjValues = Nothing
	End Function
	
	'%InsPostMOS661Upd: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (MOS661)
	Public Function InsPostMOS661Upd(ByVal sAction As String, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nOrd_typeCost As Integer, ByVal nAmount As Double, ByVal nUsercode As Integer) As Boolean
		Dim lintAction As Integer
		
		Dim lobjValues As eFunctions.Values
		lobjValues = New eFunctions.Values
		
		On Error GoTo InsPostMOS661Upd_Err
		
		With Me
			.nCurrency = nCurrency
			.dEffecdate = dEffecdate
			.nOrd_typeCost = nOrd_typeCost
			.nAmount = nAmount
			.nUsercode = nUsercode
			
			If sAction = "Del" Then
				lintAction = cintActionDel
			Else
				If sAction = "Update" Then
					lintAction = cintActionUpdate
				Else
					If sAction = "Add" Then
						lintAction = cintActionAdd
					End If
				End If
			End If
			
			Select Case lintAction
				Case 1
					'+ Se crea el registro
					InsPostMOS661Upd = .Add
					
					'+ Se modifica el registro
				Case 2
					InsPostMOS661Upd = .Update
					
					'+ Se elimina el registro
				Case 3
					InsPostMOS661Upd = .Delete
					
			End Select
		End With
		
InsPostMOS661Upd_Err: 
		If Err.Number Then
			InsPostMOS661Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsUpdOrd_type: Realiza la actualización de la tabla
	Private Function InsUpdOrd_type(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdOrd_type As eRemoteDB.Execute
		
		On Error GoTo InsUpdOrd_type_Err
		
		lrecInsUpdOrd_type = New eRemoteDB.Execute
		
		With lrecInsUpdOrd_type
			.StoredProcedure = "InsUpdOrd_type"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrd_typeCost", nOrd_typeCost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdOrd_type = .Run(False)
		End With
		
InsUpdOrd_type_Err: 
		If Err.Number Then
			InsUpdOrd_type = False
		End If
		lrecInsUpdOrd_type = Nothing
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: se controla la apertura de la clase
	'%---------------------------------------------------------
	Private Sub Class_Initialize_Renamed()
		'%---------------------------------------------------------
		nCurrency = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nOrd_typeCost = eRemoteDB.Constants.intNull
		nAmount = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class







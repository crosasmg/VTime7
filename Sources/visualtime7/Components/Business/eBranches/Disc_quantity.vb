Option Strict Off
Option Explicit On
Public Class Disc_quantity
	'%-------------------------------------------------------%'
	'% $Workfile:: Disc_quantity.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla DISC_QUANTITY tomada el 21/03/2002 10:00
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ --------------- - -------- ------- ----- ------ --------
	Public nQuantity As Integer ' NUMBER        22     5      0 No
	Public dEffecdate As Date ' DATE           7              No
	Public nRate_disc As Single ' NUMBER        22     5      2 Yes
	Public dNulldate As Date ' DATE           7              Yes
	Public nUsercode As Integer ' NUMBER        22     5      0 Yes
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdDisc_quantity(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdDisc_quantity(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdDisc_quantity(3)
	End Function
	
	'%InsValDisc_quantity: Lee los datos de la tabla Disc_quantity
	Public Function InsValDisc_quantity(ByVal nQuantity As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaDisc_quantity_v As eRemoteDB.Execute
		Dim nExist As Integer
		
		On Error GoTo reaDisc_quantity_v_Err
		
		lrecreaDisc_quantity_v = New eRemoteDB.Execute
		
		With lrecreaDisc_quantity_v
			.StoredProcedure = "reaDisc_quantity_v"
			.Parameters.Add("nQuantity", nQuantity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			InsValDisc_quantity = .Parameters("nExist").Value = 1
		End With
		
reaDisc_quantity_v_Err: 
		If Err.Number Then
			InsValDisc_quantity = False
		End If
		'UPGRADE_NOTE: Object lrecreaDisc_quantity_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDisc_quantity_v = Nothing
		On Error GoTo 0
	End Function
	
	'%insValMCA581_k: Esta función se encarga de validar los datos del encabezado
	'% de la transacción Tabla de descuento por volumen
	Public Function insValMCA581_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal dEffecdate As Date) As String
		'- Se definen los objetos para el manejo de las clases
		Dim lobjErrors As eFunctions.Errors
		Dim lblnError As Boolean
		Dim ldtmDate As Date
		
		On Error GoTo insValMCA581_k_Err
		
		lobjErrors = New eFunctions.Errors
		With lobjErrors
			'+ Validación de fecha
			If dEffecdate = dtmNull Then
				lblnError = True
				Call .ErrorMessage(sCodispl, 11198)
			End If
			
			'+ Validacion de fecha de actualización
			If Not lblnError Then
				If nMainAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					ldtmDate = Find_Date_Greater()
					If ldtmDate <> dtmNull Then
						If dEffecdate < ldtmDate Then
							Call .ErrorMessage(sCodispl, 55611,  , eFunctions.Errors.TextAlign.RigthAling, " (" & ldtmDate & ")")
						End If
					End If
				End If
			End If
			insValMCA581_k = .Confirm
		End With
		
insValMCA581_k_Err: 
		If Err.Number Then
			insValMCA581_k = "insValMCA581_k: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%Find_Date_Greater Valida la fecha de efecto de la transacción
	Public Function Find_Date_Greater() As Date
		Dim lrecreaDisc_quantity As eRemoteDB.Execute
		Dim ldtmDate As Date
		
		On Error GoTo reaDisc_quantity_v_Err
		
		Find_Date_Greater = dtmNull
		
		lrecreaDisc_quantity = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure ReaDisc_quantity_date al 20-03-2002 11:31:00
		'+
		With lrecreaDisc_quantity
			.StoredProcedure = "ReaDisc_quantity_date"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			Find_Date_Greater = .Parameters("dEffecdate").Value
		End With
		
reaDisc_quantity_v_Err: 
		If Err.Number Then
			Find_Date_Greater = dtmNull
		End If
		'UPGRADE_NOTE: Object lrecreaDisc_quantity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDisc_quantity = Nothing
		On Error GoTo 0
	End Function
	
	'%insValMCA581: Esta función se encarga de validar los datos del Form
	'% Tabla de descuento por volumen
	Public Function insValMCA581(ByVal sCodispl As String, ByVal sAction As String, ByVal nQuantity As Integer, ByVal dEffecdate As Date, ByVal nRate_disc As Double) As String
		'- Se define el objeto para el manejo de las clases
		Dim lobjErrors As eFunctions.Errors
		
		Dim lblnError As Boolean
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValMCA581_Err
		lblnError = False
		
		'+ Validación del descuento
		With lobjErrors
			If nQuantity <= 0 Then
				Call .ErrorMessage(sCodispl, 55701)
			Else
				If nQuantity <> eRemoteDB.Constants.intNull And nRate_disc <= 0 Then
					lblnError = True
					Call .ErrorMessage(sCodispl, 10151)
				End If
			End If
			
			'+ Validación de duplicidad cantidad/fecha Efecto
			If sAction = "Add" Then
				If Not lblnError Then
					If InsValDisc_quantity(nQuantity, dEffecdate) Then
						Call .ErrorMessage(sCodispl, 700020)
					End If
				End If
			End If
			insValMCA581 = .Confirm
		End With
		
insValMCA581_Err: 
		If Err.Number Then
			insValMCA581 = "insValMCA581: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%InsPostMCA581Upd: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (MCA581)
	Public Function InsPostMCA581Upd(ByVal sAction As String, ByVal nQuantity As Integer, ByVal dEffecdate As Date, ByVal nRate_disc As Double, ByVal nUsercode As Integer) As Boolean
		Dim lintAction As Integer
		
		On Error GoTo InsPostMCA581Upd_Err
		With Me
			.nQuantity = nQuantity
			.dEffecdate = dEffecdate
			.nRate_disc = nRate_disc
			.nUsercode = nUsercode
			
			If sAction = "Del" Then
				lintAction = 3
			Else
				If sAction = "Update" Then
					lintAction = 2
				Else
					If sAction = "Add" Then
						lintAction = 1
					End If
				End If
			End If
			
			Select Case lintAction
				Case 1
					
					'+ Se crea el registro
					InsPostMCA581Upd = .Add
					
					'+ Se modifica el registro
				Case 2
					InsPostMCA581Upd = .Update
					
					'+ Se elimina el registro
				Case 3
					InsPostMCA581Upd = .Delete
					
			End Select
		End With
		
InsPostMCA581Upd_Err: 
		If Err.Number Then
			InsPostMCA581Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsUpdDisc_quantity: Realiza la actualización de la tabla
	Private Function InsUpdDisc_quantity(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdDisc_quantity As eRemoteDB.Execute
		
		On Error GoTo InsUpdDisc_quantity_Err
		
		lrecInsUpdDisc_quantity = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'InsUpdDisc_quantity'
		'+ Información leída el 23/01/02
		With lrecInsUpdDisc_quantity
			.StoredProcedure = "InsUpdDisc_quantity"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuantity", nQuantity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate_disc", nRate_disc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdDisc_quantity = .Run(False)
		End With
		
InsUpdDisc_quantity_Err: 
		If Err.Number Then
			InsUpdDisc_quantity = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdDisc_quantity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdDisc_quantity = Nothing
		On Error GoTo 0
	End Function
	
	'* Class_Initialize: se controla la apertura de la clase
	'---------------------------------------------------------
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'---------------------------------------------------------
		nQuantity = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nRate_disc = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class







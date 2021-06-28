Option Strict Off
Option Explicit On
Public Class Tab_branch_quant
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_branch_quant.cls                     $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla TAB_BRANCH_QUANT tomada el 19/03/2002 11:11
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	' ------------------------------ --------------- - -------- ------- ----- ------ --------
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public dEffecdate As Date ' DATE           7              No
	Public dNulldate As Date ' DATE           7              Yes
	Public sStatregt As String ' VARCHAR2       1              Yes
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTab_branch_quant(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdTab_branch_quant(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTab_branch_quant(3)
	End Function
	
	'%InsValTab_branch_quant: Lee los datos de la tabla Tab_branch_quant
	Public Function InsValTab_branch_quant(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTab_branch_quant_v As eRemoteDB.Execute
		Dim nExist As Integer
		
		On Error GoTo reaTab_branch_quant_v_Err
		
		lrecreaTab_branch_quant_v = New eRemoteDB.Execute
		
		With lrecreaTab_branch_quant_v
			.StoredProcedure = "reaTab_branch_quant_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExist").Value = 1 Then
				InsValTab_branch_quant = True
			End If
		End With
		
reaTab_branch_quant_v_Err: 
		If Err.Number Then
			InsValTab_branch_quant = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_branch_quant_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_branch_quant_v = Nothing
		On Error GoTo 0
	End Function
	
	'%insValMCA580_k: Esta función se encarga de validar los datos del encabezado
	'% de la transacción Tarifa de automóvil
	Public Function insValMCA580_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal dEffecdate As Date) As String
		'- Se definen los objetos para el manejo de las clases
		Dim lobjErrors As eFunctions.Errors
		Dim lblnError As Boolean
		Dim ldtmDate As Date
		
		On Error GoTo insValMCA580_k_Err
		
		lobjErrors = New eFunctions.Errors
		
		lblnError = False
		
		'+ Validación de fecha
		With lobjErrors
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
			
			insValMCA580_k = .Confirm
		End With
		
insValMCA580_k_Err: 
		If Err.Number Then
			insValMCA580_k = "insValMCA580_k: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%Find_Date_Greater Valida la fecha de efecto de la transacción
	Public Function Find_Date_Greater() As Date
		Dim lrecTab_branch_quant As eRemoteDB.Execute
		Dim ldtmDate As Date
		
		On Error GoTo Find_Date_Greater_Err
		
		Find_Date_Greater = dtmNull
		
		lrecTab_branch_quant = New eRemoteDB.Execute
		
		With lrecTab_branch_quant
			.StoredProcedure = "ReaTab_branch_quant_date"
			.Parameters.Add("dEffecdate", ldtmDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			Find_Date_Greater = .Parameters("dEffecdate").Value
		End With
		
Find_Date_Greater_Err: 
		If Err.Number Then
			Find_Date_Greater = dtmNull
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_branch_quant may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_branch_quant = Nothing
	End Function
	
	'%insValMCA580: Esta función se encarga de validar los datos del Form
	'%Ramos/Productos permitidos para el descuento por volúmen
	Public Function insValMCA580(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sStatregt As String) As String
		'- Se define el objeto para el manejo de las clases
		Dim lobjErrors As eFunctions.Errors
		Dim mobjProduct As eProduct.Product
		
		Dim lblnError As Boolean
		
		On Error GoTo insValMCA580_Err
		
		lobjErrors = New eFunctions.Errors
		
		'+ Validación del ramo
		With lobjErrors
			If nBranch <= 0 Then
				lblnError = True
				Call .ErrorMessage(sCodispl, 1022)
			End If
			
			'+ Validación del producto
			If nProduct = eRemoteDB.Constants.intNull Then
				nProduct = 0
			End If
			
			'+ Validación de duplicidad Ramo/Producto/Fecha Efecto
			If sAction = "Add" Then
				If Not lblnError Then
					If InsValTab_branch_quant(nBranch, nProduct, dEffecdate) Then
						Call .ErrorMessage(sCodispl, 20029)
					End If
				End If
			End If
			
			'+ Validación del estado del registro
			If nBranch <> 0 Then
				If sStatregt = "0" Or sStatregt = String.Empty Then
					lblnError = True
					Call .ErrorMessage(sCodispl, 9089)
				End If
			End If
			insValMCA580 = .Confirm
		End With
		
insValMCA580_Err: 
		If Err.Number Then
			insValMCA580 = "insValMCA580: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%InsPostMCA580Upd: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (MCA580)
	Public Function InsPostMCA580Upd(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
		Dim lintAction As Integer
		
		On Error GoTo InsPostMCA580Upd_Err
		With Me
			.nBranch = nBranch
			If nProduct = eRemoteDB.Constants.intNull Then
				nProduct = 0
			End If
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.sStatregt = sStatregt
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
					InsPostMCA580Upd = .Add
					
					'+ Se modifica el registro
				Case 2
					InsPostMCA580Upd = .Update
					
					'+ Se elimina el registro
				Case 3
					InsPostMCA580Upd = .Delete
					
			End Select
		End With
		
InsPostMCA580Upd_Err: 
		If Err.Number Then
			InsPostMCA580Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsUpdTab_branch_quant: Realiza la actualización de la tabla
	Private Function InsUpdTab_branch_quant(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdTab_branch_quant As eRemoteDB.Execute
		
		On Error GoTo InsUpdTab_branch_quant_Err
		
		lrecInsUpdTab_branch_quant = New eRemoteDB.Execute
		
		With lrecInsUpdTab_branch_quant
			.StoredProcedure = "InsUpdTab_branch_quant"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdTab_branch_quant = .Run(False)
		End With
		
InsUpdTab_branch_quant_Err: 
		If Err.Number Then
			InsUpdTab_branch_quant = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdTab_branch_quant may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdTab_branch_quant = Nothing
		On Error GoTo 0
	End Function
	
	'* Class_Initialize: se controla la apertura de la clase
	'---------------------------------------------------------
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'---------------------------------------------------------
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		sStatregt = String.Empty
		dNulldate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class







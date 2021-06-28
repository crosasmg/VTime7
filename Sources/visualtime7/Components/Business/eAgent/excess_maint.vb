Option Strict Off
Option Explicit On
Public Class excess_maint
	'%-------------------------------------------------------%'
	'% $Workfile:: excess_maint.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'%Propiedades según la tabla 'excess_maint' en el sistema 19/12/2001 02:52:37 p.m.
	
	'%       Column name              Type
	'%  ------------------------- ------------
	Public nInterTyp As Integer
	Public nBranch As Integer
	Public nProduct As Integer
	Public nType_hist As Integer
	Public nDet_transac As Integer
	Public nInitRange As Double
	Public nEndRange As Double
	Public nPercent As Double
	Public nAmount As Double
	Public nUsercode As Integer
	
	'% insUpdExcess_Maint: Método que realiza las actualizaciones pertinentes sobre la tabla "Excess_Maint"
	Public Function insUpdExcess_Maint(ByVal nAction As Integer) As Boolean
		Dim lclsexcess_maint As eRemoteDB.Execute
		
		lclsexcess_maint = New eRemoteDB.Execute
		
		On Error GoTo insUpdExcess_Maint_Err
		
		'+ Define all parameters for the stored procedures 'insudb.insUpdexcess_maint'. Generated on 19/12/2001 02:52:37 p.m.
		
		With lclsexcess_maint
			.StoredProcedure = "insUpdexcess_maint"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_hist", nType_hist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDet_transac", IIf(nDet_transac < 0, 0, nDet_transac), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitRange", nInitRange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndRange", nEndRange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdExcess_Maint = .Run(False)
		End With
		
insUpdExcess_Maint_Err: 
		If Err.Number Then
			insUpdExcess_Maint = False
		End If
		'UPGRADE_NOTE: Object lclsexcess_maint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsexcess_maint = Nothing
		On Error GoTo 0
	End Function
	
	'%insValMAG582_K: Función que realiza la validacion de los datos introducidos en el
	'%                encabezado de la ventana
	Public Function insValMAG582_K(ByVal sCodispl As String, ByVal nInterTyp As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMAG582_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		
		'+ Validación del campo "Tipo de intermediario"
		
		If nInterTyp = 0 Or nInterTyp = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10095)
		End If
		
		'+ Validación del campo "Ramo"
		
		If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1022)
		End If
		
		
		'+ Validación del campo "Producto"
		
		If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3635)
		End If
		
		
		insValMAG582_K = lclsErrors.Confirm
		
insValMAG582_K_Err: 
		If Err.Number Then
			insValMAG582_K = insValMAG582_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insValMAG582: Función que realiza la validacion de los datos introducidor en la sección
	'%              de detalles de la ventana
	Public Function insValMAG582(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nType_hist As Integer, ByVal nDet_transac As Integer, ByVal nInitRange As Double, ByVal nEndRange As Double, ByVal nPercent As Double, ByVal nAmount As Double, ByVal nInterType As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		
		On Error GoTo insValMAG582_Err
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lclsValField.objErr = lclsErrors
		
		'+ Validación del campo "Operación/Transacción"
		
		If (nType_hist = 0 Or nType_hist = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 7133)
		Else
			
			'+ Se valida que si el tipo de operación es "Anulación", el campo "Detalle" debe estar lleno
			If (nType_hist = 29 Or nType_hist = 30) And (nDet_transac = 0 Or nDet_transac = eRemoteDB.Constants.intNull) Then
				Call lclsErrors.ErrorMessage(sCodispl, 55599)
			End If
			
			'+ Se valida que si el tipo de operación es "Modificación o Endoso", el campo "Detalle" debe estar lleno
			If (nType_hist = 11 Or nType_hist = 12 Or nType_hist = 25 Or nType_hist = 54 Or nType_hist = 55) And (nDet_transac = 0 Or nDet_transac = eRemoteDB.Constants.intNull) Then
				Call lclsErrors.ErrorMessage(sCodispl, 55599)
			End If
			
			
			'+ Se valida que si el tipo de operación está lleno, se debe indicar el porcentaje o el monto de incentivo
			If (nPercent = 0 Or nPercent = eRemoteDB.Constants.intNull) And (nAmount = 0 Or nAmount = eRemoteDB.Constants.intNull) Then
				Call lclsErrors.ErrorMessage(sCodispl, 55598)
			End If
		End If
		
		'+ Si el porcentaje de comisión está lleno, éste no debe ser mayor a 100.
		
		If nPercent <> eRemoteDB.Constants.intNull And nPercent <> 0 Then
			lclsValField.Min = 0.01
			lclsValField.Max = 100
			lclsValField.Descript = "Porcentaje"
			lclsValField.ErrRange = 11239
			lclsValField.ValNumber(nPercent)
		End If
		
		'+ Validación del campo "Rango inicial de prima"
		
		If (nInitRange = 0 Or nInitRange = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10182)
		End If
		
		'+ Validación del campo "Rango final de prima"
		
		If (nEndRange = 0 Or nEndRange = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10183)
		End If
		
		'+ Se valida que el rango inicial sea menor al rango final - ACM - 09/05/2002
		If (nInitRange > 0 And nEndRange > 0) And (nEndRange < nInitRange) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10184)
		End If
		
		'+ Se valida que el rango introducido no se encuentre registrado en la BD - ACM - 09/05/2002
		If (nInitRange > 0 And nEndRange > 0) Then
			If Find_range(nInterType, nBranch, nProduct, nInitRange, nEndRange, nType_hist, nDet_transac, sAction) Then
				Call lclsErrors.ErrorMessage(sCodispl, 60214,  ,  , " [" & nInitRange & "," & nEndRange & "] ")
			End If
		End If
		
		insValMAG582 = lclsErrors.Confirm
		
insValMAG582_Err: 
		If Err.Number Then
			insValMAG582 = insValMAG582 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insPostMAG582: Función que realiza la validacion de los datos introducidos por la ventana
	Public Function insPostMAG582(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nInterTyp As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nType_hist As Integer, ByVal nDet_transac As Integer, ByVal nInitRange As Double, ByVal nEndRange As Double, ByVal nPercent As Double, ByVal nAmount As Double) As Boolean
		
		On Error GoTo insPostMAG582_Err
		
		With Me
			.nInterTyp = nInterTyp
			.nBranch = nBranch
			.nProduct = nProduct
			.nType_hist = nType_hist
			.nDet_transac = nDet_transac
			.nInitRange = nInitRange
			.nEndRange = nEndRange
			.nPercent = nPercent
			.nAmount = nAmount
			.nUsercode = nUsercode
			
			Select Case sAction
				
				'+ Acción: Agregar
				Case "Add"
					insPostMAG582 = .insUpdExcess_Maint(1)
					
					'+ Acción: Actualizar
				Case "Update"
					insPostMAG582 = .insUpdExcess_Maint(2)
					
					'+ Acción: Borrar
				Case "Del"
					insPostMAG582 = .insUpdExcess_Maint(3)
			End Select
			
		End With
		
insPostMAG582_Err: 
		If Err.Number Then
			insPostMAG582 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Find_Range: Función que realiza la busqueda de un rango en la base de datos
	'%            para validar su existencia
	Private Function Find_range(ByVal nInterType As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nInit_Range As Double, ByVal nFinal_Range As Double, ByVal nType_hist As Integer, ByVal nDetail As Integer, ByVal sAction As String) As Boolean
		Dim lrecREAEXCESS_MAINT_RANGE As eRemoteDB.Execute
		
		On Error GoTo Find_range_Err
		
		lrecREAEXCESS_MAINT_RANGE = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaTab_comrat_range'
		'+Información leída el 14/05/2001 03:36:18 p.m.
		
		With lrecREAEXCESS_MAINT_RANGE
			.StoredProcedure = "REAEXCESS_MAINT_RANGE"
			.Parameters.Add("nInterType", nInterType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_hist", nType_hist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDet_transac", IIf(nDetail < 0, 0, nDetail), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInit_Range", nInit_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFinal_Range", nFinal_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				If .FieldToClass("nExist") = 1 Then
					
					'+Si se está actualizando, se verifica que no esté validando el mismo registro
					If sAction = "Update" Then
						If .FieldToClass("nInitial_range") = nInit_Range Then
							Find_range = False
						Else
							Find_range = True
						End If
					Else
						Find_range = True
					End If
				Else
					Find_range = False
				End If
			End If
		End With
		
Find_range_Err: 
		If Err.Number Then
			Find_range = False
		End If
		
		'UPGRADE_NOTE: Object lrecREAEXCESS_MAINT_RANGE may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecREAEXCESS_MAINT_RANGE = Nothing
		On Error GoTo 0
		
	End Function
End Class







Option Strict Off
Option Explicit On
Public Class PercentAdvanc
	'%-------------------------------------------------------%'
	'% $Workfile:: PercentAdvanc.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'+Propiedades según la tabla 'PercentAdvanc' en el sistema 26/07/2002 09:51:46 a.m.
	
	'+       Column name              Type
	'+  ------------------------- ------------
	Public nIntermtyp As Integer
	Public nCodmodpay As Integer
	Public nPercent_init As Double
	Public nPercent_end As Double
	Public nUsercode As Integer
	
	'% insUpdPercentAdvanc: Actualiza la tabla PercentAdvanc
	Public Function insUpdPercentAdvanc(ByVal nAction As Integer) As Boolean
		Dim lclsPercentAdvanc As eRemoteDB.Execute
		
		lclsPercentAdvanc = New eRemoteDB.Execute
		
		On Error GoTo insUpdPercentAdvanc_Err
		
		'+ Define all parameters for the stored procedures 'insudb.insUpdPercentAdvanc'.
		With lclsPercentAdvanc
			.StoredProcedure = "insUpdPercentAdvanc"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermtyp", nIntermtyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCodModPay", nCodmodpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent_init", nPercent_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent_end", nPercent_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdPercentAdvanc = .Run(False)
		End With
		
		
insUpdPercentAdvanc_Err: 
		If Err.Number Then
			insUpdPercentAdvanc = False
		End If
		'UPGRADE_NOTE: Object lclsPercentAdvanc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPercentAdvanc = Nothing
		On Error GoTo 0
	End Function
	
	'IsExist: Obtiene los valores maximos y minimos para un tipo de intermediario y nu modalidad
	Public Function IsExist(ByVal nIntermtyp As Integer, ByVal nCodmodpay As Integer) As Boolean
		Dim lclsPercentAdvanc As eRemoteDB.Execute
		
		lclsPercentAdvanc = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.valPercentAdvancExist'. Generated on 17/01/2002 09:51:46 a.m.
		With lclsPercentAdvanc
			
			.StoredProcedure = "reaPercentAdvanc_v"
			.Parameters.Add("nIntermtyp", nIntermtyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCodModPay", nCodmodpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				IsExist = True
				Me.nIntermtyp = nIntermtyp
				Me.nCodmodpay = .FieldToClass("nCodModPay")
				Me.nPercent_init = .FieldToClass("nPercent_init")
				Me.nPercent_end = .FieldToClass("nPercent_end")
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lclsPercentAdvanc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPercentAdvanc = Nothing
	End Function
	
	'insValMAG780_K: Función que realiza la validacion de los datos introducidos en la sección
	'                 del Header
	Public Function insValMAG780_K(ByVal sCodispl As String, ByVal nIntermtyp As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMAG780_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Tipo de intermediario: Debe estar lleno
		
		If nIntermtyp = eRemoteDB.Constants.intNull Or nIntermtyp = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 10095)
		End If
		
		insValMAG780_K = lclsErrors.Confirm
		
insValMAG780_K_Err: 
		If Err.Number Then
			insValMAG780_K = insValMAG780_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'insValMAG780: Función que realiza la validacion de los datos introducidos en la sección
	'                 de detalles de la ventana
	Public Function insValMAG780(ByVal sCodispl As String, ByVal sAction As String, ByVal nIntermtyp As Integer, ByVal nCodmodpay As Integer, ByVal nPercent_init As Double, ByVal nPercent_end As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		Dim lblnError As Boolean
		
		On Error GoTo insValMAG780_Err
		
		lclsErrors = New eFunctions.Errors
		
		lblnError = False
		
		'+ Modalidad: Debe estar lleno
		
		If nCodmodpay = eRemoteDB.Constants.intNull Or nCodmodpay = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3282)
		Else
			If sAction = "Add" Then
				If IsExist(nIntermtyp, nCodmodpay) Then
					Call lclsErrors.ErrorMessage(sCodispl, 55883)
					lblnError = True
				End If
			End If
		End If
		
		If Not lblnError Then
			If nPercent_init = eRemoteDB.Constants.intNull Or nPercent_init = 0 Then
				'+ Porcentaje Mínimo: Debe estar lleno
				Call lclsErrors.ErrorMessage(sCodispl, 60389,  , eFunctions.Errors.TextAlign.LeftAling, "Mínimo:")
				lblnError = True
			Else
				If nPercent_init < 1 Then
					'+ Porcentaje Mínimo: Debe ser mayor que 0
					Call lclsErrors.ErrorMessage(sCodispl, 11238,  , eFunctions.Errors.TextAlign.LeftAling, "Mínimo:")
					lblnError = True
				End If
				If nPercent_init > 100 Then
					'+ Porcentaje Mínimo: Debe ser menor 0 igual a 100
					Call lclsErrors.ErrorMessage(sCodispl, 11239,  , eFunctions.Errors.TextAlign.LeftAling, "Mínimo:")
					lblnError = True
				End If
			End If
			
			If nPercent_end = eRemoteDB.Constants.intNull Or nPercent_end = 0 Then
				'+ Porcentaje Máximo: Debe estar lleno
				Call lclsErrors.ErrorMessage(sCodispl, 60389,  , eFunctions.Errors.TextAlign.LeftAling, "Máximo:")
				lblnError = True
			Else
				If nPercent_end < 1 Then
					'+ Porcentaje Máximo: Debe ser mayor que 0
					Call lclsErrors.ErrorMessage(sCodispl, 11238,  , eFunctions.Errors.TextAlign.LeftAling, "Máximo:")
					lblnError = True
				End If
				If nPercent_end > 100 Then
					'+ Porcentaje Máximo: Debe ser menor 0 igual a 100
					Call lclsErrors.ErrorMessage(sCodispl, 11239,  , eFunctions.Errors.TextAlign.LeftAling, "Máximo:")
					lblnError = True
				End If
			End If
			
			'+ Porcentaje Máximo no debe ser menor al porcentaje mínimo
			If nPercent_end < nPercent_init Then
				Call lclsErrors.ErrorMessage(sCodispl, 60509)
			End If
		End If
		
		insValMAG780 = lclsErrors.Confirm
		
insValMAG780_Err: 
		If Err.Number Then
			insValMAG780 = insValMAG780 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	
	'insPostMAG780: Función que realiza la actualización de los datos introducidos por la ventana
	Public Function insPostMAG780(ByVal sCodispl As String, ByVal sAction As String, ByVal nIntermtyp As Integer, ByVal nCodmodpay As Integer, ByVal nPercent_init As Double, ByVal nPercent_end As Double, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostMAG780_Err
		
		With Me
			.nIntermtyp = nIntermtyp
			.nCodmodpay = nCodmodpay
			.nPercent_init = nPercent_init
			.nPercent_end = nPercent_end
			.nUsercode = nUsercode
			
			Select Case sAction
				
				'+ Acción: Agregar
				Case "Add"
					insPostMAG780 = insUpdPercentAdvanc(1)
					
					'+ Acción: Actualizar
				Case "Update"
					insPostMAG780 = insUpdPercentAdvanc(2)
					
					'+ Acción: Borrar
				Case "Del"
					insPostMAG780 = insUpdPercentAdvanc(3)
					
			End Select
		End With
		
insPostMAG780_Err: 
		If Err.Number Then
			insPostMAG780 = False
		End If
		On Error GoTo 0
	End Function
End Class







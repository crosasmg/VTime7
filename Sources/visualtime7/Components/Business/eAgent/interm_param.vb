Option Strict Off
Option Explicit On
Public Class interm_param
	'%-------------------------------------------------------%'
	'% $Workfile:: interm_param.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'+Propiedades según la tabla 'interm_param' en el sistema 19/12/2001 11:30:43 a.m.
	
	Public nInsu_Assist As Double
	Public nBonus_Curr As Integer
	Public nMax_Bonus As Double
	Public nMax_Accomp As Double
	Public nMinAmount As Double
	Public nDay_Discloan As Integer
	
	'% Añade el registro en la tabla interm_param
	Public Function Add(ByVal nUsercode As Integer, ByVal nInsu_Assist As Double, ByVal nBonus_Curr As Integer, ByVal nMax_Bonus As Double, ByVal nMax_Accomp As Double, ByVal nMinAmount As Double, ByVal nDay_Discloan As Integer) As Boolean
		Dim lclsinterm_param As eRemoteDB.Execute
		
		lclsinterm_param = New eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		'+ Define all parameters for the stored procedures 'insudb.creinterm_param'. Generated on 19/12/2001 11:30:43 a.m.
		
		With lclsinterm_param
			.StoredProcedure = "creinterm_param"
			.Parameters.Add("nInsu_Assist", nInsu_Assist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBonus_Curr", nBonus_Curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_Bonus", nMax_Bonus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_Accomp", nMax_Accomp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinAmount", nMinAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDay_Discloan", nDay_Discloan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lclsinterm_param may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsinterm_param = Nothing
		On Error GoTo 0
	End Function
	
	'% Actualiza un registro en la tabla interm_param
	Public Function Update(ByVal nUsercode As Integer, ByVal nInsu_Assist As Double, ByVal nBonus_Curr As Integer, ByVal nMax_Bonus As Double, ByVal nMax_Accomp As Double, ByVal nMinAmount As Double, ByVal nDay_Discloan As Integer) As Boolean
		
		Dim lclsinterm_param As eRemoteDB.Execute
		
		lclsinterm_param = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'+ Define all parameters for the stored procedures 'insudb.updinterm_param'. Generated on 19/12/2001 11:30:43 a.m.
		With lclsinterm_param
			.StoredProcedure = "updinterm_param"
			.Parameters.Add("nInsu_Assist", nInsu_Assist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBonus_Curr", nBonus_Curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_Bonus", nMax_Bonus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_Accomp", nMax_Accomp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinAmount", nMinAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDay_Discloan", nDay_Discloan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lclsinterm_param may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsinterm_param = Nothing
		On Error GoTo 0
	End Function
	
	'IsExist: Función que realiza la busqueda en la tabla 'insudb.interm_param'
	Public Function IsExist() As Boolean
		Dim lclsinterm_param As eRemoteDB.Execute
		Dim lblnExist As Boolean
		
		lclsinterm_param = New eRemoteDB.Execute
		lblnExist = False
		
		'+ Define all parameters for the stored procedures 'insudb.valinterm_paramExist'. Generated on 19/12/2001 11:30:43 a.m.
		With lclsinterm_param
			.StoredProcedure = "valinterm_paramExist"
			
			.Parameters.Add("bExist", lblnExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = .Parameters("bExist").Value
			Else
				IsExist = False
			End If
		End With
		'UPGRADE_NOTE: Object lclsinterm_param may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsinterm_param = Nothing
	End Function
	
	'%insValMAG576_K: Función que realiza la validacion de los datos introducidos en la sección
	'                 de detalles de la ventana
	Public Function insValMAG576_K(ByVal sCodispl As String, ByVal nInsu_Assist As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		
		On Error GoTo insValMAG576_K_Err
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lclsValField.objErr = lclsErrors
		
		If (nInsu_Assist = 0 Or nInsu_Assist = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 55584)
		Else
			lclsValField.Min = 0.01
			lclsValField.Max = 100#
			lclsValField.Descript = "% de comisión"
			lclsValField.ErrRange = 11239
			lclsValField.ValNumber(nInsu_Assist)
		End If
		
		insValMAG576_K = lclsErrors.Confirm
		
insValMAG576_K_Err: 
		If Err.Number Then
			insValMAG576_K = insValMAG576_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
	End Function
	
	'insPostMAG576_K: Función que realiza la validacion de los datos introducidos por la ventana
	Public Function insPostMAG576_K(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal nUsercode As Integer, ByVal nInsu_Assist As Double, ByVal nBonus_Curr As Integer, ByVal nMax_Bonus As Double, ByVal nMax_Accomp As Double, ByVal nMinAmount As Double, ByVal nDay_Discloan As Integer) As Boolean
		On Error GoTo insPostMAG576_K_Err
		
		If Not insValexist Then
			insPostMAG576_K = Add(nUsercode, nInsu_Assist, nBonus_Curr, nMax_Bonus, nMax_Accomp, nMinAmount, nDay_Discloan)
		Else
			insPostMAG576_K = Update(nUsercode, nInsu_Assist, nBonus_Curr, nMax_Bonus, nMax_Accomp, nMinAmount, nDay_Discloan)
			
		End If
		
insPostMAG576_K_Err: 
		If Err.Number Then
			insPostMAG576_K = False
		End If
		On Error GoTo 0
	End Function
	
	'insValexist: Verifica que la tabla Interm_Param posea registros
	Public Function insValexist() As Boolean
		
		Dim lexeTimes As eRemoteDB.Execute
		insValexist = False
		
		On Error GoTo insValnCodeexist_Err
		
		lexeTimes = New eRemoteDB.Execute
		
		With lexeTimes
			.StoredProcedure = "ValInterm_Param"
			
			If .Run Then
				If .FieldToClass("lCount") > 0 Then
					insValexist = True
				End If
			End If
		End With
		'UPGRADE_NOTE: Object lexeTimes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeTimes = Nothing
		
insValnCodeexist_Err: 
		If Err.Number Then
			insValexist = False
		End If
		On Error GoTo 0
		
	End Function
	'Find: Función que realiza la busqueda en la tabla 'interm_param' para una determinada fecha
	Public Function Find() As Boolean
		Dim lclsinterm_param As eRemoteDB.Execute
		
		lclsinterm_param = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reainterm_param'. Generated on 19/12/2001 11:30:43 a.m.
		With lclsinterm_param
			.StoredProcedure = "reainterm_param"
			If .Run(True) Then
				nInsu_Assist = .FieldToClass("nInsu_Assist")
				nBonus_Curr = .FieldToClass("nBonus_Curr")
				nMax_Bonus = .FieldToClass("nMax_Bonus")
				nMax_Accomp = .FieldToClass("nMax_Accomp")
				nMinAmount = .FieldToClass("nMinAmount")
				nDay_Discloan = .FieldToClass("nDay_Discloan")
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclsinterm_param may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsinterm_param = Nothing
	End Function
End Class







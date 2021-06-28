Option Strict Off
Option Explicit On
Public Class bonus_gen
	'%-------------------------------------------------------%'
	'% $Workfile:: bonus_gen.cls                            $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 26/09/03 12:57                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'+Propiedades según la tabla 'bonus_gen' en el sistema 14/12/2001 11:47:11 a.m.
	
	'+       Column name              Type
	'+  ------------------------- ------------
	
	Public nYear_Ini As Double
	Public nYear_End As Double
	Public nCurrency As Integer
	Public nMinAmount As Double
	Public nPersist As Double
	Public nReal_Goal As Double
	Public nUsercode As Integer
	
	'% Update the links for a specific client
	Public Function insUpdBonus_Gen(ByVal nAction As Integer) As Boolean
		Dim lclsbonus_gen As eRemoteDB.Execute
		
		lclsbonus_gen = New eRemoteDB.Execute
		
		On Error GoTo insUpdBonus_Gen_Err
		
		'+ Define all parameters for the stored procedures 'insudb.updgen_bonsup'. Generated on 14/12/2001 11:47:11 a.m.
		With lclsbonus_gen
			.StoredProcedure = "insUpdBonus_Gen"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_Ini", nYear_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_End", nYear_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinAmount", nMinAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPersist", nPersist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReal_Goal", nReal_Goal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdBonus_Gen = .Run(False)
		End With
		
insUpdBonus_Gen_Err: 
		If Err.Number Then
			insUpdBonus_Gen = False
		End If
		'UPGRADE_NOTE: Object lclsbonus_gen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsbonus_gen = Nothing
		On Error GoTo 0
	End Function
	
	'IsExist: Función que realiza la busqueda en la tabla 'insudb.bonus_gen'
	Public Function IsExist(ByVal nYear_Ini As Double, ByVal nYear_End As Double) As Boolean
		Dim lclsbonus_gen As eRemoteDB.Execute
		Dim lblnExist As Boolean
		
		On Error GoTo IsExist_Err
		lclsbonus_gen = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.valgen_bonsupExist'. Generated on 14/12/2001 11:47:11 a.m.
		With lclsbonus_gen
			.StoredProcedure = "Val_bonus_gen_Exist"
			.Parameters.Add("nYear_Ini", nYear_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_End", nYear_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nExist") = 1 Then
					IsExist = True
				End If
			End If
		End With
		'UPGRADE_NOTE: Object lclsbonus_gen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsbonus_gen = Nothing
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		On Error GoTo 0
	End Function
	
	'insValMAG597_K: Función que realiza la validacion de los datos introducidor en la sección
	'                de detalles de la ventana
	Public Function insValMAG597_K(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nYear_Ini As Double, ByVal nYear_End As Double, ByVal nCurrency As Integer, ByVal nMinAmount As Double, ByVal nPersist As Double, ByVal nReal_Goal As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		
		On Error GoTo insValMAG597_K_Err
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lclsValField.objErr = lclsErrors
		
		'+ Antigüedad mínima para el rango: Debe estar lleno
		
		If (nYear_Ini = 0 Or nYear_Ini = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 55600)
		End If
		
		'+ Antigüedad máxima para el rango: Debe estar lleno
		
		If (nYear_End = 0 Or nYear_End = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 60001)
		End If
		
		
		'+ Antigüedad máxima para el rango: Debe ser mayor al rango inicial
		If nYear_End < nYear_Ini Then
			Call lclsErrors.ErrorMessage(sCodispl, 10184)
		End If
		
		
		'+ Moneda: Debe estar lleno
		If (nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10107)
		End If
		
		
		'+ Monto del bono:Debe estar lleno
		
		If (nReal_Goal = 0 Or nReal_Goal = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 60002)
		End If
		
		
		'+ Si el porcentaje de persistencia está lleno, éste no debe ser mayor a 100.
		
		If nPersist <> eRemoteDB.Constants.intNull And nPersist <> 0 Then
			lclsValField.Min = 0.01
			lclsValField.Max = 100#
			lclsValField.Descript = "Persistencia"
			lclsValField.ErrRange = 11239
			lclsValField.ValNumber(nPersist)
		End If
		
		'+ Registro no debe estar repetido
		If sAction = "Add" Then
			If IsExist(nYear_Ini, nYear_End) Then
				Call lclsErrors.ErrorMessage(sCodispl, 60214)
			End If
		End If
		
		insValMAG597_K = lclsErrors.Confirm
		
insValMAG597_K_Err: 
		If Err.Number Then
			insValMAG597_K = insValMAG597_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'insPostMAG597_K: Función que realiza la validacion de los datos introducido por la ventana
	Public Function insPostMAG597_K(ByVal bHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nYear_Ini As Double, ByVal nYear_End As Double, ByVal nCurrency As Integer, ByVal nMinAmount As Double, ByVal nPersist As Double, ByVal nReal_Goal As Double) As Boolean
		On Error GoTo insPostMAG597_K_Err
		
		With Me
			.nYear_Ini = nYear_Ini
			.nYear_End = nYear_End
			.nCurrency = nCurrency
			.nMinAmount = nMinAmount
			.nPersist = nPersist
			.nReal_Goal = nReal_Goal
			.nUsercode = nUsercode
			
			If bHeader Then
				insPostMAG597_K = True
			Else
				Select Case sAction
					
					'+ Acción: Agregar
					Case "Add"
						insPostMAG597_K = insUpdBonus_Gen(1)
						
						'+ Acción: Actualizar
					Case "Update"
						insPostMAG597_K = insUpdBonus_Gen(2)
						
						'+ Acción: Borrar
					Case "Del"
						insPostMAG597_K = insUpdBonus_Gen(3)
						
				End Select
			End If
			
		End With
		
insPostMAG597_K_Err: 
		If Err.Number Then
			insPostMAG597_K = False
		End If
		On Error GoTo 0
	End Function
End Class







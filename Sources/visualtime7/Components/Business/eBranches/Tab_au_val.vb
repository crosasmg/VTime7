Option Strict Off
Option Explicit On
Public Class Tab_au_val
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_au_val.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'Column_name                   'Type      Length      Prec  Scale Nullable
	Public sVehCode As String 'char      6                       no
	Public nYear As Integer 'smallint  2           5     0     no
	Public nCapital As Double 'decimal   9           18    6     yes
	Private mlngUsercode As Integer 'smallint  2           5     0     yes
	
	'%IsExist: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%         tabla "Tab_au_val"
	Public Function IsExist(ByVal sVehCode As String, ByVal nYear As Integer) As Boolean
		Dim lrecreaTab_au_val As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		lrecreaTab_au_val = New eRemoteDB.Execute
		With lrecreaTab_au_val
			.StoredProcedure = "reaTab_au_val_v"
			.Parameters.Add("sVehcode", sVehCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			IsExist = .Parameters("nCount").Value > 0
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_au_val may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_au_val = Nothing
		On Error GoTo 0
	End Function
	
	'%InsUpdTab_au_val: Actualiza la informacion de la tabla
	Private Function InsUpdTab_au_val(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdtab_au_val As eRemoteDB.Execute
		
		On Error GoTo insUpdtab_au_val_Err
		lrecinsUpdtab_au_val = New eRemoteDB.Execute
		'+ Definición de store procedure insUpdtab_au_val al 10-03-2002 16:40:43
		With lrecinsUpdtab_au_val
			.StoredProcedure = "insUpdtab_au_val"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehcode", sVehCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdTab_au_val = .Run(False)
		End With
		
insUpdtab_au_val_Err: 
		If Err.Number Then
			InsUpdTab_au_val = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdtab_au_val may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdtab_au_val = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Esta función agrega registros a la tabla TAB_AU_VAL
	Public Function Add() As Boolean
		Add = InsUpdTab_au_val(1)
	End Function
	
	'%Update: Esta función actualiza registros en la tabla TAB_AU_VAL
	Public Function Update() As Boolean
		Update = InsUpdTab_au_val(2)
	End Function
	
	'%Delete: Esta función elimina registros de la tabla TAB_AU_VAL
	Public Function Delete() As Boolean
		Delete = InsUpdTab_au_val(3)
	End Function
	
	'%InsValMAU001Upd: Esta función se encarga de validar los datos introducidos en la zona de detalle
	Public Function InsValMAU001Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal sVehCode As String, ByVal nYear As Integer, ByVal nCapital As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTab_au_val As eBranches.Tab_au_val
		Dim lblnError As Boolean
		
		On Error GoTo InsValMAU001Upd_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Se valida la columna: nYear
			If nYear = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 3114)
				lblnError = True
			ElseIf nYear <= 0 Then 
				.ErrorMessage(sCodispl, 11365)
				lblnError = True
			End If
			
			'+ Se valida la columna: nCapital
			If nCapital <= 0 Then
				.ErrorMessage(sCodispl, 10118)
				lblnError = True
			End If
			
			'**+Validations related to columns: nYear and nCapital
			'+ Se validan las columnas: nYear y nCapital
			If Not lblnError Then
				If sAction = "Add" Then
					lclsTab_au_val = New eBranches.Tab_au_val
					If lclsTab_au_val.IsExist(sVehCode, nYear) Then
						.ErrorMessage(sCodispl, 8307)
					End If
				End If
			End If
			
			InsValMAU001Upd = lclsErrors.Confirm
		End With
		
InsValMAU001Upd_Err: 
		If Err.Number Then
			InsValMAU001Upd = "InsValMAU001Upd: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsTab_au_val may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_au_val = Nothing
		On Error GoTo 0
	End Function
	
	'*InsPostMAU001Upd: Esta función se encarga de crear/actualizar los registros
	'*correspondientes en la tabla Tab_au_val
	Public Function InsPostMAU001Upd(ByVal sAction As String, ByVal sVehCode As String, ByVal nYear As Integer, ByVal nCapital As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMAU001Upd_Err
		
		With Me
			Me.sVehCode = sVehCode
			Me.nYear = nYear
			Me.nCapital = nCapital
			mlngUsercode = nUsercode
			
			InsPostMAU001Upd = True
			Select Case sAction
				'+Si la opción seleccionada es Registrar
				Case "Add"
					InsPostMAU001Upd = .Add()
					
					'+Si la opción seleccionada es Modificar
				Case "Update"
					InsPostMAU001Upd = .Update()
					
					'+Si la opción seleccionada es Eliminar
				Case "Del"
					InsPostMAU001Upd = .Delete()
			End Select
		End With
		
InsPostMAU001Upd_Err: 
		If Err.Number Then
			InsPostMAU001Upd = False
		End If
		On Error GoTo 0
	End Function
End Class







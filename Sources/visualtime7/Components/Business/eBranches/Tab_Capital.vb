Option Strict Off
Option Explicit On
Public Class Tab_Capital
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Capital.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'-
	'- Estructura de tabla tab_capital al 04-04-2002 11:25:14
	'-  Property                    Type           DBType
	'-------------------------------------------------------
	Public nBranch As Integer ' NUMBER
	Public nProduct As Integer ' NUMBER
	Public dEffecdate As Date ' DATE
	Public nModulec As Integer ' NUMBER
	Public nCover As Integer ' NUMBER
	Public nRole As Integer ' NUMBER
	Public nId As Double ' NUMBER
	Public nAge_init As Integer ' NUMBER
	Public nAge_End As Integer ' NUMBER
	Public nInipercov As Integer ' NUMBER
	Public nEndpercov As Integer ' NUMBER
	Public nInipaycov As Integer ' NUMBER
	Public nEndpaycov As Integer ' NUMBER
	Public sSexclien As Integer ' CHAR
	Public sSmoking As Integer ' CHAR
	Public nPremanual As Double ' NUMBER
	Public nCapital As Double ' NUMBER
	Public nCurrency As Integer ' NUMBER
	Public nUsercode As Integer ' NUMBER
	
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTab_capital(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTab_capital(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTab_capital(3)
	End Function
	
	'Función que valida los datos igresados en el encabezado de la pagina
	Public Function insvalMVI773(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nZone As Short, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nRole As Integer, ByVal nAge_init As Integer, ByVal nAge_End As Integer, ByVal nInipercov As Integer, ByVal nEndpercov As Integer, ByVal nInipaycov As Integer, ByVal nEndpaycov As Integer, ByVal nPremanual As Double, ByVal nCapital As Double, ByVal sSexclien As String, ByVal sSmoking As String) As String
		Dim lrecinsvalMVI773 As eRemoteDB.Execute
		Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty
		
		On Error GoTo insvalMVI773_Err
		
		lrecinsvalMVI773 = New eRemoteDB.Execute
		
		With lrecinsvalMVI773
			.StoredProcedure = "insMVI773pkg.insValMVI773"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nZone", nZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_End", nAge_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInipercov", nInipercov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndpercov", nEndpercov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInipaycov", nInipaycov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndpaycov", nEndpaycov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremanual", nPremanual, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", IIf(sSmoking = "1", "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
			lstrError = .Parameters("Arrayerrors").Value
			
			If lstrError <> String.Empty Then
				lobjErrors = New eFunctions.Errors
				With lobjErrors
					.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrError)
					insvalMVI773 = .Confirm()
				End With
				'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lobjErrors = Nothing
				
			End If
		End With
		
insvalMVI773_Err: 
		If Err.Number Then
			insvalMVI773 = "insvalMVI773: " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lrecinsvalMVI773 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsvalMVI773 = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMIN651: Ejecuta el post de la transacción Tabla Tar_fire_fh
	Public Function InsPostMVI773(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nRole As Integer, ByVal nAge_init As Integer, ByVal nAge_End As Integer, ByVal nInipercov As Integer, ByVal nEndpercov As Integer, ByVal nInipaycov As Integer, ByVal nEndpaycov As Integer, ByVal nPremanual As Double, ByVal nCapital As Double, ByVal nId As Integer, ByVal sSexclien As String, ByVal sSmoking As String, ByVal nCurrency As Integer, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostMVI773_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nCover = nCover
			.dEffecdate = dEffecdate
			.nRole = nRole
			.nAge_init = nAge_init
			.nAge_End = nAge_End
			.nInipercov = nInipercov
			.nEndpercov = nEndpercov
			.nInipaycov = nInipaycov
			.nEndpaycov = nEndpaycov
			.nPremanual = nPremanual
			.nCapital = nCapital
			.nId = nId
			.sSexclien = CInt(sSexclien)
			.sSmoking = IIf(sSmoking = "1", "1", "2")
			.nCurrency = nCurrency
			.nUsercode = nUsercode
			
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMVI773 = Add
			Case "Update"
				InsPostMVI773 = Update
			Case "Del"
				InsPostMVI773 = Delete
		End Select
		
InsPostMVI773_Err: 
		If Err.Number Then
			InsPostMVI773 = False
		End If
		On Error GoTo 0
	End Function
	
	'InsUpdTab_capital: Se encarga de actualizar la tabla Tab_capital
	Private Function InsUpdTab_capital(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdTab_capital As eRemoteDB.Execute
		On Error GoTo InsUpdTab_capital_Err
		
		lrecInsUpdTab_capital = New eRemoteDB.Execute
		
		With lrecInsUpdTab_capital
			.StoredProcedure = "insMVI773pkg.InsPostMVI773"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_End", nAge_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInipercov", nInipercov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndpercov", nEndpercov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInipaycov", nInipaycov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndpaycov", nEndpaycov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremanual", nPremanual, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdTab_capital = .Run(False)
		End With
		
InsUpdTab_capital_Err: 
		If Err.Number Then
			InsUpdTab_capital = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdTab_capital may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdTab_capital = Nothing
		On Error GoTo 0
	End Function
End Class







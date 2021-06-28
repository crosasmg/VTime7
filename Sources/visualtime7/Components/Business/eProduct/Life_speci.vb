Option Strict Off
Option Explicit On
Public Class Life_speci
	'%-------------------------------------------------------%'
	'% $Workfile:: Life_speci.cls                           $%'
	'% $Author:: Ljimenez                                   $%'
	'% $Date:: 25-09-09 23:48                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'-
	'- Estructura de tabla life_speci al 06-27-2002 10:44:47
	'-        Property                Type         DBType   Size Scale  Prec  Null
	Public nAgeEnd As Integer ' NUMBER     22   0     5    N
	Public nAgeStart As Integer ' NUMBER     22   0     5    N
	Public nCapEnd As Double ' NUMBER     22   0     12   N
	Public nConsec As Double ' NUMBER     22   0     10   N
	Public nCapStart As Double ' NUMBER     22   0     12   N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public nCurrency As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dCompdate As Date ' DATE       7    0     0    S
	Public nCrthecni As Integer ' NUMBER     22   0     5    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public sSexInsur As String ' CHAR       1    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public nRole As Integer ' NUMBER     22   0     5    N
	
	'- Se definen las propiedades utilizadas en la ventana
	'- DP027 - Criterios técnicos - Selección de riesgo.
	
	Public sDesCurrency As String
	Public sDesCrite As String
	
	'- Se define las constantes que contienen los máximos y minimos valores para las
	'- edades y capitales.
	
	Const MaxE As Integer = 130
	Const MinE As Integer = 0
	Const MaxCap As Double = 99999999#
	Const MinCap As Double = 1
	
	'%Add: Permite registrar la información de los criterios de selección de riesgos.
	Public Function Add() As Boolean
		Dim lrecCreLife_speci As eRemoteDB.Execute
		
		lrecCreLife_speci = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		'+ Definición de parámetros para stored procedure 'insudb.creLife_speci'
		
		With lrecCreLife_speci
			.StoredProcedure = "creLife_speci"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeEnd", nAgeEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeStart", nAgeStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapEnd", nCapEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapStart", nCapStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSexInsur", sSexInsur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecCreLife_speci may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreLife_speci = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%Update: Permite actualizar la información de los criterios de selección de riesgos.
	Public Function Update() As Boolean
		Dim lrecUpdLife_speci As eRemoteDB.Execute
		
		lrecUpdLife_speci = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.insMortalityCre'
		
		With lrecUpdLife_speci
			.StoredProcedure = "updLife_speci"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeEnd", nAgeEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeStart", nAgeStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapEnd", nCapEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapStart", nCapStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSexInsur", sSexInsur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecUpdLife_speci may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdLife_speci = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%Delete: Permite borrar la información de criterios de selección de riesgos.
	Public Function Delete() As Boolean
		Dim lrecDelife_speci As eRemoteDB.Execute
		
		lrecDelife_speci = New eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.delMortality'
		
		With lrecDelife_speci
			.StoredProcedure = "delLife_speci"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecDelife_speci may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelife_speci = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		
		On Error GoTo 0
	End Function
	
	'% insValDP027: Realiza la validación de los campos puntuales de la página DP027 - Criterios técnicos - Selección de riesgo.
	Public Function insValDP027(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sSexInsur As String, ByVal nAgeStart As Integer, ByVal nAgeEnd As Integer, ByVal nCapStart As Double, ByVal nCapEnd As Double, ByVal nCrthecni As Integer, ByVal nConsec As Double, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lrecLife_speci As eRemoteDB.Execute
		Dim lObjValField As eFunctions.valField
		Dim llngCount As Integer
		
		lobjErrors = New eFunctions.Errors
		lObjValField = New eFunctions.valField
		
		insValDP027 = String.Empty
		
		On Error GoTo insValDP027_Err
		
		'+ Se realizan las validaciones del campo "Sexo".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sSexInsur) Or IsNothing(sSexInsur) Or Trim(sSexInsur) = String.Empty Or Trim(sSexInsur) = "0" Then
			Call lobjErrors.ErrorMessage(sCodispl, 2007)
		End If
		
		'+ Se realizan las validaciones del campo "Edad inicial".
		
		If nAgeStart = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 11109)
		Else
			If nAgeStart < MinE Or nAgeStart > MaxE Then
				Call lobjErrors.ErrorMessage(sCodispl, 12090,  , eFunctions.Errors.TextAlign.LeftAling, "(Edad inicial: 0-130 años)")
			Else
				If nAgeEnd <> eRemoteDB.Constants.intNull Then
					If insValOtherRange(nAgeStart, sSexInsur, nBranch, nProduct, nConsec, dEffecdate, nModulec, nCover, nCrthecni, nCapStart, nCapEnd, nRole) Then
						Call lobjErrors.ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Edad inicial:")
					Else
						If insValOtherRange_1(nAgeStart, nAgeEnd, sSexInsur, nBranch, nProduct, nConsec, dEffecdate, nModulec, nCover, nCrthecni, nCapStart, nCapEnd, nRole) Then
							Call lobjErrors.ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Edad inicial:")
						End If
					End If
				End If
			End If
		End If
		
		'+ Se realizan las validaciones del campo "Edad Final".
		
		If nAgeEnd = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 11110)
		Else
			If nAgeEnd < MinE Or nAgeEnd > MaxE Then
				Call lobjErrors.ErrorMessage(sCodispl, 12090,  , eFunctions.Errors.TextAlign.LeftAling, "(Edad final: 0-130 años)")
			Else
				If nAgeStart <> eRemoteDB.Constants.intNull Then
					If (nAgeEnd < nAgeStart) Then
						Call lobjErrors.ErrorMessage(sCodispl, 11036)
					Else
						If insValOtherRange(nAgeEnd, sSexInsur, nBranch, nProduct, nConsec, dEffecdate, nModulec, nCover, nCrthecni, nCapStart, nCapEnd, nRole) Then
							Call lobjErrors.ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Edad final:")
						Else
							If insValOtherRange_1(nAgeStart, nAgeEnd, sSexInsur, nBranch, nProduct, nConsec, dEffecdate, nModulec, nCover, nCrthecni, nCapStart, nCapEnd, nRole) Then
								Call lobjErrors.ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Edad final:")
							End If
						End If
					End If
				End If
			End If
		End If
		
		'+ Se realizan las validaciones del campo "Capital inicial".
		
		If nCapStart = 0 Or nCapStart = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage("DP013", 11111)
		Else
			If nCapEnd <> eRemoteDB.Constants.intNull And nCapEnd <> 0 Then
				If insValOtherRangeCap(nCapStart, sSexInsur, nBranch, nProduct, nConsec, dEffecdate, nModulec, nCover, nCrthecni, nAgeStart, nAgeEnd, nRole) Then
					Call lobjErrors.ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Capital inicial:")
				Else
					If insValOtherRangeCap_1(nCapStart, nCapEnd, sSexInsur, nBranch, nProduct, nConsec, dEffecdate, nModulec, nCover, nCrthecni, nAgeStart, nAgeEnd, nRole) Then
						Call lobjErrors.ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Capital inicial:")
					End If
				End If
			End If
		End If
		
		'+ Se realizan las validaciones del campo "Capital final".
		
		If nCapEnd = 0 Or nCapEnd = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage("DP013", 11112)
		Else
			If nCapStart <> eRemoteDB.Constants.intNull And nCapStart <> 0 Then
				If nCapEnd < nCapStart Then
					Call lobjErrors.ErrorMessage(sCodispl, 11113)
				Else
					If insValOtherRangeCap(nCapEnd, sSexInsur, nBranch, nProduct, nConsec, dEffecdate, nModulec, nCover, nCrthecni, nAgeStart, nAgeEnd, nRole) Then
						Call lobjErrors.ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Capital final:")
					Else
						If insValOtherRangeCap_1(nCapStart, nCapEnd, sSexInsur, nBranch, nProduct, nConsec, dEffecdate, nModulec, nCover, nCrthecni, nAgeStart, nAgeEnd, nRole) Then
							Call lobjErrors.ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Capital final:")
						End If
					End If
				End If
			End If
		End If
		
		'+ Se realizan las validaciones del campo "Criterio".
		
		If nCrthecni = 0 Or nCrthecni = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 11408)
		End If
		
		'+ Se realizan las validaciones del campo "Figura-Rol".
		
		If nRole = 0 Or nRole = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 55979)
		End If
		
		insValDP027 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lrecLife_speci may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLife_speci = Nothing
		
insValDP027_Err: 
		If Err.Number Then
			insValDP027 = insValDP027 & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'%insValOtherRange: Esta rutina es la encargada de evitar las intercepciones entre los rangos.
	Private Function insValOtherRange(ByVal nAge As Integer, ByVal sSexclien As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nConsec As Double, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCrthecni As Integer, ByVal nCapStart As Double, ByVal nCapEnd As Double, ByVal nRole As Integer) As Boolean
		Dim lrecLife_speci As eRemoteDB.Execute
		
		On Error GoTo insValOtherRange_Err
		lrecLife_speci = New eRemoteDB.Execute
		With lrecLife_speci
			.StoredProcedure = "insValOtherLife_speci"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSexclien", Trim(sSexclien), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapstart", nCapStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapend", nCapEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If (.Run) Then
				.RCloseRec()
				insValOtherRange = True
			End If
		End With
		
insValOtherRange_Err: 
		If Err.Number Then
			insValOtherRange = False
		End If
		'UPGRADE_NOTE: Object lrecLife_speci may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLife_speci = Nothing
		On Error GoTo 0
	End Function
	
	'%insValOtherRange_1: Esta rutina es la encargada de evitar las intercepciones entre los rangos.
	Private Function insValOtherRange_1(ByVal nAgeStart As Integer, ByVal nAgeEnd As Integer, ByVal sSexclien As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nConsec As Double, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCrthecni As Integer, ByVal nCapStart As Double, ByVal nCapEnd As Double, ByVal nRole As Integer) As Boolean
		Dim lrecLife_speci As eRemoteDB.Execute
		
		On Error GoTo insValOtherRange_1_Err
		
		lrecLife_speci = New eRemoteDB.Execute
		With lrecLife_speci
			.StoredProcedure = "insValOtherLife_speci_2"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgestart", nAgeStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeend", nAgeEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapstart", nCapStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapend", nCapEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				.RCloseRec()
				insValOtherRange_1 = True
			End If
		End With
		
insValOtherRange_1_Err: 
		If Err.Number Then
			insValOtherRange_1 = False
		End If
		'UPGRADE_NOTE: Object lrecLife_speci may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLife_speci = Nothing
		On Error GoTo 0
	End Function
	
	'%insValOtherRangeCap: Esta rutina es la encargada de evitar las intercepciones entre los rangos.
	Private Function insValOtherRangeCap(ByVal nCapital As Double, ByVal sSexclien As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nConsec As Double, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCrthecni As Integer, ByVal nAgeStart As Integer, ByVal nAgeEnd As Integer, ByVal nRole As Integer) As Boolean
		Dim lrecLife_speci As eRemoteDB.Execute
		
		On Error GoTo insValOtherRangeCap_Err
		lrecLife_speci = New eRemoteDB.Execute
		With lrecLife_speci
			.StoredProcedure = "insValOtherLife_speci_1"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgestart", nAgeStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeend", nAgeEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				.RCloseRec()
				insValOtherRangeCap = True
			End If
		End With
		
insValOtherRangeCap_Err: 
		If Err.Number Then
			insValOtherRangeCap = False
		End If
		'UPGRADE_NOTE: Object lrecLife_speci may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLife_speci = Nothing
		On Error GoTo 0
	End Function
	
	'%insValOtherRangeCap_1: Esta rutina es la encargada de evitar las intercepciones entre los rangos.
	Private Function insValOtherRangeCap_1(ByVal nCapStart As Double, ByVal nCapEnd As Double, ByVal sSexclien As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nConsec As Double, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCrthecni As Integer, ByVal nAgeStart As Integer, ByVal nAgeEnd As Integer, ByVal nRole As Integer) As Boolean
		Dim lrecLife_speci As eRemoteDB.Execute
		
		On Error GoTo insValOtherRangeCap_1_Err
		lrecLife_speci = New eRemoteDB.Execute
		With lrecLife_speci
			.StoredProcedure = "insValOtherLife_speci_3"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapstart", nCapStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapend", nCapEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgestart", nAgeStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeend", nAgeEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				.RCloseRec()
				insValOtherRangeCap_1 = True
			End If
		End With
		
insValOtherRangeCap_1_Err: 
		If Err.Number Then
			insValOtherRangeCap_1 = False
		End If
		'UPGRADE_NOTE: Object lrecLife_speci may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLife_speci = Nothing
		On Error GoTo 0
	End Function

    '% insPostDP027: Esta función se encarga de almacenar los datos en las tablas, en este caso Life_speci
    '% ventana DP027 - Criterios técnicos - Selección de riesgo.
    Public Function insPostDP027(ByVal lstrAction As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal ldtmEffecdate As Date, ByVal llngConsec As Double, Optional ByVal lintAgeEnd As Integer = 0, Optional ByVal lintAgeStart As Integer = 0, Optional ByVal ldblCapEnd As Double = 0, Optional ByVal ldblCapStart As Double = 0, Optional ByVal lintCurrency As Integer = 0, Optional ByVal lintCrthecni As Integer = 0, Optional ByVal lstrSexInsur As String = "", Optional ByVal lintUsercode As Integer = 0, Optional ByVal lintnModulec As Integer = 0, Optional ByVal lintnCover As Integer = 0, Optional ByVal lintnRole As Integer = 0) As Boolean

        insPostDP027 = True

        nBranch = lintBranch
        nProduct = lintProduct
        dEffecdate = ldtmEffecdate
        nConsec = llngConsec
        nAgeEnd = lintAgeEnd
        nAgeStart = lintAgeStart
        nCapEnd = ldblCapEnd
        nCapStart = ldblCapStart
        nCurrency = lintCurrency
        nCrthecni = lintCrthecni
        sSexInsur = lstrSexInsur
        nUsercode = lintUsercode
        nModulec = lintnModulec
        nCover = lintnCover
        nRole = lintnRole

        Select Case lstrAction

            '+ Si la opción seleccionada es Registrar.

            Case "Add"
                insPostDP027 = Add()

                '+ Si la opción seleccionada es Modificar.

            Case "Update"
                insPostDP027 = Update()

                '+ Si la opción seleccionada es Eliminar.
            Case "Delete"
                insPostDP027 = Delete()
        End Select
    End Function

    '% FindnCurrency: Recupera la moneda en que están los criterios de selección de riesgo
    '%                para un producto
    Public Function FindCurrency(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaLife_speci_ncurrency As eRemoteDB.Execute
		
		On Error GoTo reaLife_speci_ncurrency_Err
		
		lrecreaLife_speci_ncurrency = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaLife_speci_ncurrency al 06-27-2002 16:18:11
		'+
		With lrecreaLife_speci_ncurrency
			.StoredProcedure = "reaLife_speci_ncurrency"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				FindCurrency = True
				Me.nCurrency = .FieldToClass("nCurrency")
			Else
				FindCurrency = False
			End If
		End With
		
reaLife_speci_ncurrency_Err: 
		If Err.Number Then
			FindCurrency = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaLife_speci_ncurrency may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLife_speci_ncurrency = Nothing
		On Error GoTo 0
		
	End Function
End Class







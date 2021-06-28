Option Strict Off
Option Explicit On
Public Class Prod_Am_Bil
	'%-------------------------------------------------------%'
	'% $Workfile:: Prod_Am_Bil.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	
	'-
	'- Estructura de tabla Prod_Am_Bil al 06-27-2002 10:44:47
	'-        Property                Type
	'----------------------------------------
	Public nBranch As Integer
	Public nProduct As Integer
	Public nModulec As Integer
	Public nCover As Integer
	Public nRole As Integer
	Public nPay_Concep As Integer
	Public nGroup_Pres As Integer
	Public sIllness As String
	Public dEffecdate As Date
	Public nCurrency As Integer
	Public nCurrencyAux As Integer
	Public nDed_Type As Integer
	Public Desc_Ded_Type As String
	Public nDed_Amount As Integer
	Public nDed_Percen As Double
	Public nDed_Quanti As Integer
	Public nIndem_rate As Double
	Public nLimit As Double
	Public nTypLim As Integer
	Public Desc_TypLim As String
	Public nCount As Integer
	Public nLimit_exe As Double
	Public nPunish As Double
	Public nDed_Quanti_2 As Integer
	Public nIndem_Rate_2 As Double
	Public nLimit_2 As Double
	Public nTypLim_2 As Integer
	Public Desc_TypLim_2 As String
	Public nCount_2 As Integer
	Public nLimit_exe_2 As Double
	Public nPunish_2 As Double
	Public dNulldate As Date
	Public nusercode As Integer
	Public dCompdate As Date
	
	'- Se define las constantes que contienen los máximos y minimos valores para las
	'- edades y capitales.
	
	Const MaxE As Integer = 130
	Const MinE As Integer = 0
	Const MaxCap As Double = 99999999#
	Const MinCap As Double = 1
	
	'%Add: Permite registrar la información los conceptos de pago.
	Public Function Add() As Boolean
		Dim lrecCreProd_Am_Bil As eRemoteDB.Execute
		
		lrecCreProd_Am_Bil = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		'+ Definición de parámetros para stored procedure 'insudb.creProd_Am_Bil'
		
		With lrecCreProd_Am_Bil
			
			.StoredProcedure = "creProd_Am_Bil"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_Pres", nGroup_Pres, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_Concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDed_type", nDed_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDed_amount", nDed_Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDed_percen", nDed_Percen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDed_quanti", nDed_Quanti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndem_rate", nIndem_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimit", nLimit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyplim", nTypLim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", nCount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimit_exe", nLimit_exe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPunish", nPunish, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDed_quanti_2", nDed_Quanti_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndem_rate_2", nIndem_Rate_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimit_2", nLimit_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyplim_2", nTypLim_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount_2", nCount_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimit_exe_2", nLimit_exe_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPunish_2", nPunish_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecCreProd_Am_Bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreProd_Am_Bil = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%Update: Permite actualizar la información de los criterios de selección de riesgos.
	Public Function Update() As Boolean
		Dim lrecUpdProd_Am_Bil As eRemoteDB.Execute
		
		lrecUpdProd_Am_Bil = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.updProd_Am_Bil'
		
		With lrecUpdProd_Am_Bil
			.StoredProcedure = "updProd_Am_Bil"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_Pres", nGroup_Pres, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_Concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDed_type", nDed_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDed_amount", nDed_Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDed_percen", nDed_Percen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDed_quanti", nDed_Quanti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndem_rate", nIndem_rate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimit", nLimit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyplim", nTypLim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", nCount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimit_exe", nLimit_exe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPunish", nPunish, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDed_quanti_2", nDed_Quanti_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndem_rate_2", nIndem_Rate_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimit_2", nLimit_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyplim_2", nTypLim_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount_2", nCount_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimit_exe_2", nLimit_exe_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPunish_2", nPunish_2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecUpdProd_Am_Bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdProd_Am_Bil = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%Delete: Permite borrar la información de criterios de selección de riesgos.
	Public Function Delete() As Boolean
		Dim lrecDeProd_Am_Bil As eRemoteDB.Execute
		
		lrecDeProd_Am_Bil = New eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.delProd_Am_Bil'
		
		With lrecDeProd_Am_Bil
			.StoredProcedure = "delProd_Am_Bil"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_Pres", nGroup_Pres, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_Concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecDeProd_Am_Bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDeProd_Am_Bil = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%valExistsTab_am_bil: Esta rutina es la encargada de evitar registros duplicados.
	Public Function valExistsProd_am_bil(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal nPay_Concep As Integer, ByVal sIllness As String, ByVal nGroup_Pres As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecProd_am_bil As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo valExistsProd_am_bil_Err
		
		lrecProd_am_bil = New eRemoteDB.Execute
		
		With lrecProd_am_bil
			.StoredProcedure = "valExistsProd_am_bil"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_Concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_Pres", nGroup_Pres, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			valExistsProd_am_bil = (.Parameters("nExists").Value = 1)
		End With
		
valExistsProd_am_bil_Err: 
		If Err.Number Then
			valExistsProd_am_bil = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecProd_am_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecProd_am_bil = Nothing
	End Function
	
	
	'% insValDP101: Realiza la validación de los campos puntuales de la página DP101 - Criterios técnicos - Selección de riesgo.
	Public Function insValDP101(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal nGroup_Pres As Integer, ByVal nPay_Concep As Integer, ByVal sIllness As String, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nDed_Type As Integer, ByVal nDed_Amount As Double, ByVal nDed_Percen As Double, ByVal nDed_Quanti As Double, ByVal nIndem_rate As Double, ByVal nLimit As Double, ByVal nTypLim As Integer, ByVal nCount As Integer, ByVal nLimit_exe As Double, ByVal nPunish As Integer, ByVal nDed_Quanti_2 As Double, ByVal nIndem_Rate_2 As Double, ByVal nLimit_2 As Double, ByVal nTypLim_2 As Integer, ByVal nCount_2 As Integer, ByVal nLimit_exe_2 As Double, ByVal nPunish_2 As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lrecProd_am_bil As eRemoteDB.Execute
		Dim lObjValField As eFunctions.valField
		Dim llngCount As Integer
		
		lobjErrors = New eFunctions.Errors
		lObjValField = New eFunctions.valField
		
		insValDP101 = String.Empty
		
		On Error GoTo insValDP101_Err
		
		With lobjErrors
			
			'+ Valida que la cobertura se haya incluído y no se haya borrado su contenido
			If nCover <= 0 Then
				.ErrorMessage("DP101", 3552)
			End If
			
			If nRole <= 0 Then
				.ErrorMessage("DP101", 55979)
			End If
			
			If sIllness = String.Empty Or sIllness = "0" Then
				.ErrorMessage("DP101", 4230)
			End If
			
			'+ Valida que la agrupación de prestaciones se haya incluído.
			If nGroup_Pres <= 0 Then
				.ErrorMessage("DP101", 100136)
			End If
			
			'+ Valida que el concepto se haya incluído.
			If nPay_Concep <= 0 Then
				.ErrorMessage("DP101", 3982)
			End If
			
			'+ Se valida que la combinación Concepto-prestación sean únicos.
			If nGroup_Pres > 0 And nPay_Concep >= 0 Then
				If sAction = "Add" Then
					
					If valExistsProd_am_bil(nBranch, nProduct, nModulec, nCover, nRole, nPay_Concep, sIllness, nGroup_Pres, dEffecdate) Then
						.ErrorMessage("DP101", 55700)
					End If
				End If
			End If
			
			'+Se valida que el campo "%Deducible" sea mayor que cero.
			If nDed_Type <= 0 Then
				.ErrorMessage("DP101", 3553)
			Else
				'+ Si es diferente a no tiene (valor 1 del campo tipo)
				If nDed_Type <> 1 Then
					'+ Se debe agregar información de los campos del deducible
					If nDed_Percen <= 0 And nDed_Amount <= 0 And nDed_Quanti <= 0 Then
						.ErrorMessage("DP101", 38038)
					Else
						If nDed_Percen > 0 And nDed_Amount > 0 And nDed_Quanti > 0 Then
							.ErrorMessage("DP101", 3556)
						Else
							If nDed_Percen > 0 And nDed_Amount > 0 Then
								.ErrorMessage("DP101", 3556)
							Else
								If nDed_Percen > 0 And nDed_Quanti > 0 Then
									.ErrorMessage("DP101", 3556)
								Else
									If nDed_Amount > 0 And nDed_Quanti > 0 Then
										.ErrorMessage("DP101", 3556)
									End If
								End If
							End If
						End If
					End If
					
					If nDed_Percen <= 0 And nDed_Amount <= 0 And nDed_Quanti_2 <= 0 Then
						.ErrorMessage("DP101", 38038)
					Else
						If nDed_Percen > 0 And nDed_Amount > 0 And nDed_Quanti_2 > 0 Then
							.ErrorMessage("DP101", 3556)
						Else
							If nDed_Percen > 0 And nDed_Amount > 0 Then
								.ErrorMessage("DP101", 3556)
							Else
								If nDed_Percen > 0 And nDed_Quanti_2 > 0 Then
									.ErrorMessage("DP101", 3556)
								Else
									If nDed_Amount > 0 And nDed_Quanti_2 > 0 Then
										.ErrorMessage("DP101", 3556)
									End If
								End If
							End If
						End If
					End If
				Else
					'+ Si el tipo de deducible es no tiene y alguno de los campos relacionados al deducible tienen valor
					If nDed_Percen > 0 Or nDed_Amount > 0 Or nDed_Quanti > 0 Then
						.ErrorMessage("DP101", 3555)
					End If
					
					If nDed_Percen > 0 Or nDed_Amount > 0 Or nDed_Quanti_2 > 0 Then
						.ErrorMessage("DP101", 3555)
					End If
				End If
			End If
			
			'+Se valida que el campo "%Deducible" mayor que cero.
			If nDed_Percen > 0 Then
				If nDed_Percen > 100 Then
					.ErrorMessage("DP101", 9992,  , eFunctions.Errors.TextAlign.LeftAling, "% Deduc:")
				End If
			End If
			
			'+Se valida que el campo "Monto" mayor que cero.
			If nDed_Amount <> 0 And nDed_Amount <> eRemoteDB.Constants.intNull Then
				If nDed_Amount < 0 Then
					.ErrorMessage("DP101", 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Monto: ")
				End If
			ElseIf nDed_Type <> 0 And nDed_Type <> eRemoteDB.Constants.intNull Then 
				If nDed_Amount = 0 And nDed_Type <> 1 And (nDed_Percen = 0 Or nDed_Percen = eRemoteDB.Constants.intNull) Then
					.ErrorMessage("DP101", 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Monto: ")
				End If
			End If
			
			'+Se valida que el campo "Días" sea mayor que cero.
			If nDed_Quanti <= 0 Then
				'+ Si el concepto coresponde a Habitación-cuartos
				If nPay_Concep = 12 Then
					.ErrorMessage("DP101", 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Días: ")
				End If
			End If
			
			'+Se valida que el campo "Días" sea mayor que cero.
			If nDed_Quanti_2 <= 0 Then
				'+ Si el concepto coresponde a Habitación-cuartos
				If nPay_Concep = 12 Then
					.ErrorMessage("DP101", 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Días: ")
				End If
			End If
			
			'+ Se efectúan las validaciones del campo "%Indemnizar". Se valida que el campo no esté vacio
			If nIndem_rate <= 0 Then
				.ErrorMessage("DP101", 3557)
			Else
				'+ Se valida que el campo este comprendido entre 0 y 100
				If nIndem_rate <= 0 Or nIndem_rate > 100 Then
					.ErrorMessage("DP101", 3558)
				End If
			End If
			
			'+ Se efectúan las validaciones del campo "%Indemnizar". Se valida que el campo no esté vacio
			If nIndem_Rate_2 <= 0 Then
				.ErrorMessage("DP101", 3557)
			Else
				'+ Se valida que el campo este comprendido entre 0 y 100
				If nIndem_Rate_2 <= 0 Or nIndem_Rate_2 > 100 Then
					.ErrorMessage("DP101", 3558)
				End If
			End If
			
			'+Se valida que el campo "Límite" sea mayor que cero.
			If nLimit > 0 Then
				'+ Si el Monto es mayor al límite cobertura
				If nDed_Amount > nLimit Then
					.ErrorMessage("DP101", 38036)
				End If
			End If
			
			'+Se valida que el campo "Límite" sea mayor que cero.
			If nLimit_2 > 0 Then
				'+ Si el Monto es mayor al límite cobertura
				If nDed_Amount > nLimit Then
					.ErrorMessage("DP101", 38036)
				End If
			End If
			
			'+ Se valida que si el campo nTyplim es "Cantidad de veces" el campo nCount debe estar lleno
			If nTypLim = 7 Then
				If nCount <= 0 Then
					Call .ErrorMessage("DP101", 55701)
				End If
			End If
			
			'+ Se valida que si el campo nTyplim es "Cantidad de veces" el campo nCount debe estar lleno
			If nTypLim_2 = 7 Then
				If nCount_2 <= 0 Then
					Call .ErrorMessage("DP101", 55701)
				End If
			End If
			
		End With
		
		insValDP101 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lrecProd_am_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecProd_am_bil = Nothing
		
insValDP101_Err: 
		If Err.Number Then
			insValDP101 = insValDP101 & Err.Description
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lrecProd_am_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecProd_am_bil = Nothing
	End Function

    '% insPostDP101: Esta función se encarga de almacenar los datos en las tablas, en este caso Prod_Am_Bil
    '% ventana DP101 - Criterios técnicos - Selección de riesgo.
    Public Function insPostDP101(ByVal lstrAction As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal lintModulec As Integer, ByVal lintCover As Integer, ByVal lintRole As Integer, ByVal lintGroup_Pres As Integer, ByVal lintPay_concep As Integer, ByVal lstrsIllness As String, ByVal ldtmdEffecdate As Date, Optional ByVal lintCurrency As Integer = 0, Optional ByVal lintDed_type As Integer = 0, Optional ByVal ldblDed_amount As Double = 0, Optional ByVal ldblDed_percen As Double = 0, Optional ByVal ldblDed_quanti As Double = 0, Optional ByVal ldblIndem_rate As Double = 0, Optional ByVal ldblLimit As Double = 0, Optional ByVal lintTyplim As Integer = 0, Optional ByVal lintCount As Integer = 0, Optional ByVal ldblLimit_exe As Double = 0, Optional ByVal lintPunish As Integer = 0, Optional ByVal ldblDed_quanti_2 As Double = 0, Optional ByVal ldblIndem_rate_2 As Double = 0, Optional ByVal ldblLimit_2 As Double = 0, Optional ByVal lintTyplim_2 As Integer = 0, Optional ByVal lintCount_2 As Integer = 0, Optional ByVal ldblLimit_exe_2 As Double = 0, Optional ByVal lintPunish_2 As Integer = 0, Optional ByVal lintUsercode As Integer = 0) As Boolean

        insPostDP101 = True
        nBranch = lintBranch
        nProduct = lintProduct
        nModulec = lintModulec
        nCover = lintCover
        nRole = lintRole
        nGroup_Pres = lintGroup_Pres
        nPay_Concep = lintPay_concep
        sIllness = lstrsIllness
        dEffecdate = ldtmdEffecdate
        nCurrency = lintCurrency
        nDed_Type = lintDed_type
        nDed_Amount = ldblDed_amount
        nDed_Percen = ldblDed_percen
        nDed_Quanti = ldblDed_quanti
        nIndem_rate = ldblIndem_rate
        nLimit = ldblLimit
        nTypLim = lintTyplim
        nCount = lintCount
        nLimit_exe = ldblLimit_exe
        nPunish = lintPunish
        nDed_Quanti_2 = ldblDed_quanti_2
        nIndem_Rate_2 = ldblIndem_rate_2
        nLimit_2 = ldblLimit_2
        nTypLim_2 = lintTyplim_2
        nCount_2 = lintCount_2
        nLimit_exe_2 = ldblLimit_exe_2
        nPunish_2 = lintPunish_2
        nusercode = lintUsercode
        Select Case lstrAction

            '+ Si la opción seleccionada es Registrar.

            Case "Add"
                insPostDP101 = Add()

                '+ Si la opción seleccionada es Modificar.

            Case "Update"
                insPostDP101 = Update()

                '+ Si la opción seleccionada es Eliminar.
            Case "Delete"
                insPostDP101 = Delete()
        End Select
    End Function

    '% FindnCurrency: Recupera la moneda en que están los criterios de selección de riesgo
    '%                para un producto
    Public Function FindCurrency(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal sIllness As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaProd_Am_Bil_ncurrency As eRemoteDB.Execute
		
		On Error GoTo reaProd_Am_Bil_ncurrency_Err
		
		lrecreaProd_Am_Bil_ncurrency = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaProd_Am_Bil_ncurrency al 06-27-2002 16:18:11
		'+
		With lrecreaProd_Am_Bil_ncurrency
			.StoredProcedure = "reaProd_Am_Bil_ncurrency"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				FindCurrency = True
				Me.nCurrency = .FieldToClass("nCurrency")
			Else
				FindCurrency = False
			End If
		End With
		
reaProd_Am_Bil_ncurrency_Err: 
		If Err.Number Then
			FindCurrency = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaProd_Am_Bil_ncurrency may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProd_Am_Bil_ncurrency = Nothing
		On Error GoTo 0
		
	End Function
End Class







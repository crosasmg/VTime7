Option Strict Off
Option Explicit On
Public Class Tab_Ul_Costs
	
	'+ Propiedades según la tabla Tab_ul_costs en el sistema el 01/12/2002.
	'+ El campo llave corresponde a nBranch, nProduct, nMonth_From, nMonth_Until.
	
	'+ Column_name                              Type                 Length Prec Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ -------------------                      -------------------- ------ ---- ----- -------- ------------------ --------------------
	Public nBranch As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nProduct As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nMonth_from As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nMonth_until As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nCost_amount As Double 'decimal  5      12   6     no       (n/a)              (n/a)
	Public nCurrency As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public sCreDeb As String 'char            1          yes
	Public nUsercode As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	'- Se agregan 3 variables públicas requeridas por cambios de APV2 - ACM - 13/08/2003
	Public nType_cost As Integer 'NOT NULL NUMBER(5)
	Public nRate As Double 'NOT NULL NUMBER(8,2)
	Public nMax_amou As Double 'NOT NULL NUMBER(12,2)
	
	'% Add: Crea un registro en la tabla Tab_Ul_Costs.
	Public Function Add() As Boolean
		Add = insUpdTab_Ul_Costs(1)
	End Function
	
	'% Update: Actualiza un registro en la tabla Tab_Ul_Costs.
	Public Function Update() As Boolean
		Update = insUpdTab_Ul_Costs(2)
	End Function
	
	'% Delete: Borra registros de la tabla Tab_Ul_Costs.
	Public Function Delete() As Boolean
		Delete = insUpdTab_Ul_Costs(3)
	End Function
	
	'% insUpdTab_Ul_Costs: Se encarga de actualizar la tabla Tab_Ul_Costs.
	Private Function insUpdTab_Ul_Costs(ByVal nAction As Integer) As Boolean
		Dim lrecUpdTab_Ul_Costs As eRemoteDB.Execute
		
		On Error GoTo insUpdTab_Ul_Costs_Err
		
		lrecUpdTab_Ul_Costs = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_Ul_CostsRange'
		'+ Información leída el 16/12/2002 02:32:15 pm.
		
		With lrecUpdTab_Ul_Costs
			.StoredProcedure = "insUpdTab_Ul_Costs"
			
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth_from", nMonth_from, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth_until", nMonth_until, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCost_amount", IIf(nCost_amount <> eRemoteDB.Constants.intNull, nCost_amount, 0), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCreDeb", sCreDeb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'+ Campos añadidos al SP de actualización [APV2] - ACM - 13/08/2003
			.Parameters.Add("nType_cost", nType_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", IIf(nRate = eRemoteDB.Constants.intNull, 0, nRate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 2, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_amou", IIf(nMax_amou = eRemoteDB.Constants.intNull, 0, nMax_amou), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 2, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdTab_Ul_Costs = .Run(False)
		End With
		
insUpdTab_Ul_Costs_Err: 
		If Err.Number Then
			insUpdTab_Ul_Costs = False
		End If
		
		'UPGRADE_NOTE: Object lrecUpdTab_Ul_Costs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdTab_Ul_Costs = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_Range: Permite verificar si existen registros duplicados dentro de los rangos de meses.
	Public Function Find_Range(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonth_from As Integer, ByVal nMonth_until As Integer, ByVal nType_cost As Integer) As Boolean
		Dim lrecReaTab_Ul_costsRange As eRemoteDB.Execute
		
		lrecReaTab_Ul_costsRange = New eRemoteDB.Execute
		
		On Error GoTo Find_Range_Err
		
		Find_Range = False
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_Ul_CostsRange'
		'+ Información leída el 16/12/2002 02:32:15 pm.
		
		With lrecReaTab_Ul_costsRange
			.StoredProcedure = "reaTab_Ul_CostsRange"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth_from", nMonth_from, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth_until", nMonth_until, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_cost", nType_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_Range = True
				.RCloseRec()
			End If
		End With
		
Find_Range_Err: 
		If Err.Number Then
			Find_Range = False
		End If
		
		'UPGRADE_NOTE: Object lrecReaTab_Ul_costsRange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_Ul_costsRange = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValMVI7001_K: Validación de los datos del encabezado de la página MVI7001.
	Public Function InsValMVI7001_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		
		On Error GoTo InsValMVI7001_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'**+ The validations of the field "Line of business" are performed.
		'+ Se realizan las validaciones del campo "Ramo".
		
		If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1022)
		End If
		
		'**+ The validations of the field "Product" are performed.
		'+ Se realizan las validaciones del campo "Producto".
		
		If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1014)
		Else
			lclsProduct = New eProduct.Product
			With lclsProduct
				If Not .insValProdMaster(nBranch, nProduct) Then
					Call lclsErrors.ErrorMessage(sCodispl, 9066)
				Else
					'**+ Validate that the product corresponds to life or combined.
					'+ Se valida que el producto corresponda a vida o combinado.
					If .blnError Then
						If CStr(.sBrancht) <> "1" And CStr(.sBrancht) <> "2" And CStr(.sBrancht) <> "5" Then
							Call lclsErrors.ErrorMessage(sCodispl, 3987)
						End If
					End If
				End If
				If .FindProduct_li(nBranch, nProduct, Today) Then
					If .nProdClas <> 4 Then
						Call lclsErrors.ErrorMessage(sCodispl, 70177)
					End If
				End If
			End With
		End If
		
		InsValMVI7001_K = lclsErrors.Confirm
		
InsValMVI7001_K_Err: 
		If Err.Number Then
			InsValMVI7001_K = "InsValMVI7001_K:" & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% insValMVI7001: Función que realiza la validación del detalle de los datos introducidos en
	'% la ventana MVI7001.
	Public Function insValMVI7001(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonth_from As Integer, ByVal nMonth_until As Integer, ByVal nCost_amount As Double, ByVal nCurrency As Integer, ByVal nType_calc As Integer, ByVal nRate As Double, ByVal nMax_amou As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMVI7001_Err
		
		lclsErrors = New eFunctions.Errors
		'+ Mes inicial: Debe estar lleno.
		
		If nMonth_from < 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70117)
		End If
		
		'+ Mes final: Debe estar lleno y debe ser mayor al mes inicial.
		
		If nMonth_until < 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70013)
		Else
			If nMonth_from >= 0 Then
				If nMonth_until < nMonth_from Then
					Call lclsErrors.ErrorMessage(sCodispl, 70014)
				End If
			End If
		End If
		
		'+ Monto del costo: Debe estar lleno.
		
		If nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 70119)
		End If
		
		'+ La combinación Tabla, Fecha, Ramo, Producto no debe estar repetido.
		
		If sAction = "Add" Then
			If nMonth_from >= 0 And nMonth_until >= 0 Then
				If Find_Range(nBranch, nProduct, nMonth_from, nMonth_until, nType_calc) Then
					Call lclsErrors.ErrorMessage(sCodispl, 70012)
				End If
			End If
		End If
		
		'+ Validaciones nuevas [APV2] - ACM - 13/08/2003
		If nType_calc <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70151)
		End If
		
		If nCost_amount > 0 And nRate > 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70148)
		End If
		
		If nRate > 0 And nCost_amount > 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70149)
		End If
		
		If nMax_amou < 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 70150)
		End If
		
		insValMVI7001 = lclsErrors.Confirm
		
insValMVI7001_Err: 
		If Err.Number Then
			insValMVI7001 = insValMVI7001 & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% InsPostMVI7001: Ejecuta el post de la transacción MVI7001 - Tabla Tab_Ul_Costs.
	Public Function InsPostMVI7001(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonth_from As Integer, ByVal nMonth_until As Integer, ByVal nCost_amount As Double, ByVal nCurrency As Integer, ByVal sCreDeb As String, ByVal nUsercode As Integer, ByVal nType_cost As Integer, ByVal nRate As Double, ByVal nMax_amou As Double) As Boolean
		On Error GoTo InsPostMVI7001_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nMonth_from = nMonth_from
			.nMonth_until = nMonth_until
			.nCost_amount = nCost_amount
			.nCurrency = nCurrency
			.nType_cost = nType_cost
			.nRate = nRate
			.nMax_amou = nMax_amou
			
			If sCreDeb = "0" Or sCreDeb = String.Empty Then
				.sCreDeb = "2"
			Else
				.sCreDeb = sCreDeb
			End If
			
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMVI7001 = Add
			Case "Update"
				InsPostMVI7001 = Update
			Case "Del"
				InsPostMVI7001 = Delete
		End Select
		
InsPostMVI7001_Err: 
		If Err.Number Then
			InsPostMVI7001 = False
		End If
		
		On Error GoTo 0
	End Function
End Class







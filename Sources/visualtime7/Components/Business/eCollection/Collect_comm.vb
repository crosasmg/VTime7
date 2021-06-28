Option Strict Off
Option Explicit On
Option Compare Text
Public Class Collect_comm
	'%-------------------------------------------------------%'
	'% $Workfile:: Collect_comm.cls                         $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 25/08/03 12:50p                              $%'
	'% $Revision:: 23                                       $%'
	'%-------------------------------------------------------%'
	
	' Desarrollado por: Victor Gajardo
	' Fecha: 24-05-20001
	' Descripcion: Transaccion para manejo de tabla de Numeración de Facturas
	'
	
	'+ Descripcion de la tabla COLLECT_COMM al 26/10/2001 17:01
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ --------------- - -------- ------- ----- ------ --------
	Public nCollectorType As Integer ' NUMBER        22     5      0 No
	Public nConType As Integer ' NUMBER        22     5      0 No
	Public nDaysIni As Integer ' NUMBER        22     5      0 No
	Public nDaysEnd As Integer ' NUMBER        22     5      0 No
	Public nCode As Integer ' NUMBER        22     5      0 No
	Public sDescript As String
	Public sShort_des As String
	Public sStatregt As String
	Public nInChannel As Integer ' NUMBER        22     5      0 No
	Public dEffecDate As Date ' DATE           0     0      0 No
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nInitRange As Double ' NUMBER        22    10      2 No
	Public nEndRange As Double ' NUMBER        22    10      2 Yes
	Public nCommPercent As Double ' NUMBER        22     5      2 Yes
	Public nCurrency As Integer ' NUMBER        22     5      0 Yes
	Public nCommAmount As Double ' NUMBER        22    10      2 Yes
	Public dCompdate As Date ' DATE           7              No
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	Public nMinAmount As Double ' NUMBER        22    10      2 Yes
	Public nMaxAmount As Double ' NUMBER        22    10      2 Yes
	Public sCollecAsig As String ' CHAR           1              No
	
	Public nExiKey As Integer
	Public nContDiasAtr As Integer
	Public nContUFCobr As Integer
	Public nRanFinNull As Integer
	Public nMaxRanFin As Integer
	Public nExist As Integer
	
	'% insValMCO678_k: se realizan las validaciones del encabezado
	'                  de la Tabla de comisiones de cobradores
	Public Function insValMCO678_K(ByVal sCodispl As String, ByVal nCollectorType As Integer, ByVal nConType As Integer, ByVal sCollecAsig As String, ByVal nDaysIni As Integer, ByVal nDaysEnd As Integer, ByVal nCode As Integer, ByVal nMainAction As Integer, ByVal dEffecDate As Date, ByVal sDescript As String, ByVal sShort_des As String, ByVal nInChannel As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsvalField As eFunctions.valField
		Dim ldtmFindDateMax As Date
		On Error GoTo insValMCO678_K_Err
		lclsErrors = New eFunctions.Errors
		lclsvalField = New eFunctions.valField
		
		'+ Validaciones para el número de tabla
		If nCode = eRemoteDB.Constants.intNull Or nCode = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 10048)
		End If
		
		If sDescript = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 10071)
		End If
		
		If sShort_des = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 12019)
		End If
		
		If nMainAction = 301 Then
			If Find_nCode(nCode) Then
				Call lclsErrors.ErrorMessage(sCodispl, 56057)
			End If
		End If
		
		'+ Se verifica que la fecha sea válida
		If Not IsDate(dEffecDate) Or dEffecDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10190)
		Else
			'+ Se verifica que la fecha sea posterior a la de la última transacción
			If nMainAction = 302 Then
				ldtmFindDateMax = FindDateMax(nCode)
				If ldtmFindDateMax <> eRemoteDB.Constants.dtmNull Then
					If dEffecDate <= ldtmFindDateMax Then
						Call lclsErrors.ErrorMessage(sCodispl, 10869)
					End If
				End If
				If dEffecDate <= Today Then
					Call lclsErrors.ErrorMessage(sCodispl, 10868)
				End If
			End If
		End If
		
		' Se Deja en o los valores posibles en Null
		If nCollectorType = eRemoteDB.Constants.intNull Then
			nCollectorType = 0
		End If
		If nConType = eRemoteDB.Constants.intNull Then
			nConType = 0
		End If
		If sCollecAsig = CStr(eRemoteDB.Constants.strNull) Then
			sCollecAsig = ""
		End If
		If nDaysEnd = eRemoteDB.Constants.intNull Then
			nDaysEnd = 0
		End If
		
		'+ Tipo de cobrador de estar lleno : 55571
		If nCollectorType = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55571)
		End If
		
		'+ Tipo de Contrato debe estar lleno : 06018
		If nConType = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 6018)
		End If
		
		'+ Rango de dias de atraso de cobro Desde debe estar lleno : 05033
		'+ Rango de dias de atraso de cobro Hasta debe estar lleno : 05033
		If nDaysIni = eRemoteDB.Constants.intNull Or nDaysEnd = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 5033)
		Else
			'+ Rango de dias de atraso de cobro Hasta debe ser mayor
			'+ al rango de días de atraso de cobro desde: 55577
			If nDaysIni >= nDaysEnd Then
				Call lclsErrors.ErrorMessage(sCodispl, 55577)
			Else
				If Not valCollect_Comm_Range(nCode, nInChannel, nCollectorType, nConType, sCollecAsig, nDaysIni, nDaysEnd, dEffecDate) Then
					Call lclsErrors.ErrorMessage(sCodispl, 60214,  , eFunctions.Errors.TextAlign.RigthAling, " (Dias de Atraso en Cobro)")
				End If
			End If
		End If
		
		' Fin validación encabezado
		insValMCO678_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalField = Nothing
		
insValMCO678_K_Err: 
		If Err.Number Then
			insValMCO678_K = insValMCO678_K & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	'**% FindDateMax: Seachs the last date of modification in the table Fund_inv
	'% FindDateMax: Selecciona la última fecha de modificación de Fund_inv
	Public Function FindDateMax(ByVal nCode As Integer) As Date
		Dim lrecvalFund_inv As eRemoteDB.Execute
		Dim ldtmDate As Date
		lrecvalFund_inv = New eRemoteDB.Execute
		
		ldtmDate = Today
		With lrecvalFund_inv
			.StoredProcedure = "MaxCollect_comm"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ddEffecdate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                FindDateMax = IIf(IsDBNull(.Parameters.Item("ddEffecdate").Value), #1/1/1800#, .Parameters.Item("ddEffecdate").Value)
            End If
        End With
		
		'UPGRADE_NOTE: Object lrecvalFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalFund_inv = Nothing
	End Function
	
	'**% FindDateMax: Seachs the last date of modification in the table Fund_inv
	'% FindDateMax: Selecciona la última fecha de modificación de Fund_inv
	Public Function FindDescript(ByVal nCode As Integer) As Date
		Dim lrecvalFund_inv As eRemoteDB.Execute
		lrecvalFund_inv = New eRemoteDB.Execute
		
		With lrecvalFund_inv
			.StoredProcedure = "FindDescript"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				sDescript = .FieldToClass("sDescript")
				sShort_des = .FieldToClass("sShort_des")
				sStatregt = .FieldToClass("sStatregt")
				FindDescript = System.Date.FromOADate(True)
			Else
				FindDescript = System.Date.FromOADate(False)
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecvalFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalFund_inv = Nothing
	End Function
	
	'% Find: Permite cargar en la colección los datos de la tabla Collect_comm
	Public Function Find_nCode(ByVal nCode As Integer) As Boolean
		Dim lFind_nCode As eRemoteDB.Execute
		On Error GoTo Find_nCode_Err
		lFind_nCode = New eRemoteDB.Execute
		'Definición de parámetros para stored procedure 'Find_nCode'
		With lFind_nCode
			.StoredProcedure = "Find_nCode"
			' Parametros de entrada a la StoreProcedure
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_nCode = True
			Else
				Find_nCode = False
			End If
		End With
		'UPGRADE_NOTE: Object lFind_nCode may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lFind_nCode = Nothing
		
Find_nCode_Err: 
		If Err.Number Then
			Find_nCode = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lFind_nCode may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lFind_nCode = Nothing
		
	End Function
	
	'% insValMCO678: se realizan las validaciones del encabezado
	'                de la Tabla de comisiones de cobradores
	Public Function insValMCO678(ByVal Action As String, ByVal sCodispl As String, ByVal nCollectorType As Integer, ByVal nConType As Integer, ByVal sCollecAsig As String, ByVal nDaysIni As Integer, ByVal nDaysEnd As Integer, ByVal nCode As Integer, ByVal nInChannel As Integer, ByVal dEffecDate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nInitRange As Double, ByVal nEndRange As Double, ByVal nCommPercent As Double, ByVal nCurrency As Integer, ByVal nCommAmount As Double, ByVal nMinAmount As Double, ByVal nMaxAmount As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsvalField As eFunctions.valField
		
		On Error GoTo insValMCO678_Err
		
		lclsErrors = New eFunctions.Errors
		lclsvalField = New eFunctions.valField
		
		' Se Deja en o los valores posibles en Null
		If nBranch = eRemoteDB.Constants.intNull Then
			nBranch = 0
		End If
		
		If nProduct = eRemoteDB.Constants.intNull Then
			nProduct = 0
		End If
		
		If nInitRange = eRemoteDB.Constants.intNull Then
			nInitRange = 0
		End If
		
		If nCommPercent = eRemoteDB.Constants.intNull Then
			nCommPercent = 0
		End If
		
		If nCommAmount = eRemoteDB.Constants.intNull Then
			nCommAmount = 0
		End If
		
		'+ El Ramo debe estar lleno: 11135
		If nBranch = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 11135)
		End If
		
		'+ El Producto debe estar lleno: 11009
		If nProduct = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 11009)
		End If
		
		'+ Rango de UF cobradas Desde debe estar lleno
		If nInitRange = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60126)
		End If
		
		'+ Rango de UF cobradas Desde debe ser Menor a UF cobradas Hasta: 60125
		If nEndRange <> eRemoteDB.Constants.intNull Or nEndRange > 0 Then
			If nInitRange >= nEndRange Then
				Call lclsErrors.ErrorMessage(sCodispl, 60125)
			End If
		End If
		
		'+ Porcentaje de comision o monto de comisión deben tener valor: 55578
		If nCommPercent = 0 And nCommAmount = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55578)
		End If
		
		'+ Los Rangos de numeración no se deben repetir : 60127
		'+ Un rango de numeración no debe estar contenido en otro.
		If nCollectorType > 0 And nConType > 0 And sCollecAsig <> "" And nDaysIni > 0 And nBranch > 0 And nProduct > 0 And nInitRange > 0 Then
			If Action = "Add" Then
				'+ Se valida en caso que la accion corresponda a ADD
				If insCollect_commRange(nCollectorType, nConType, sCollecAsig, nDaysIni, nDaysEnd, nCode, nInChannel, dEffecDate, nBranch, nProduct, nInitRange, nEndRange) Then
					'+          Llave ya existe
					If nExiKey > 0 Then
						Call lclsErrors.ErrorMessage(sCodispl, 60127)
					End If
					'+          Existe rango de dias de atrazo
					If nContDiasAtr > 0 Then
						Call lclsErrors.ErrorMessage(sCodispl, 60214)
					End If
					'+          Existe rango de uf cobradas
					If nContUFCobr > 0 Then
						Call lclsErrors.ErrorMessage(sCodispl, 60214)
					End If
					'+          Rango de UF cobradas Hasta debe estar lleno
					'+          solo para: ran.ini. mayor al max(ran.fin) y no exista ran.fin = null
					If nEndRange = eRemoteDB.Constants.intNull Or nEndRange = 0 Then
						If nRanFinNull > 0 Or nInitRange <= nMaxRanFin Then
							Call lclsErrors.ErrorMessage(sCodispl, 60126)
						End If
					End If
				End If
			Else
				'+ Se valida en caso que la accion corresponda a Update
				If insCollect_commRange_Up(nCollectorType, nConType, sCollecAsig, nDaysIni, nDaysEnd, nCode, nInChannel, dEffecDate, nBranch, nProduct, nInitRange, nEndRange) Then
					If nExist > 0 Then
						Call lclsErrors.ErrorMessage(sCodispl, 60214)
					End If
				End If
			End If
		End If
		
		'+ Se valida en caso que el monto minimo no sea mayor que el maximo
		If nMinAmount > 0 And nMaxAmount > 0 Then
			If nMinAmount > nMaxAmount Then
				Call lclsErrors.ErrorMessage(sCodispl, 10167)
			End If
		End If
		
		insValMCO678 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalField = Nothing
		
insValMCO678_Err: 
		If Err.Number Then
			insValMCO678 = insValMCO678 & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'% insPostMCO678: Se realizan las validaciones del encabezado
	'                 de la Tabla de comisiones de cobradores
	Public Function insPostMCO678(ByVal Action As String, ByVal nCollectorType As Integer, ByVal nConType As Integer, ByVal sCollecAsig As String, ByVal nDaysIni As Integer, ByVal nDaysEnd As Integer, ByVal nCode As Integer, ByVal nInChannel As Integer, ByVal dEffecDate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nInitRange As Double, ByVal nEndRange As Double, ByVal nCommPercent As Double, ByVal nCurrency As Integer, ByVal nCommAmount As Double, ByVal nUsercode As Integer, ByVal nMinAmount As Double, ByVal nMaxAmount As Double, ByVal sDescript As String, ByVal sShort_des As String) As Boolean
		Dim lclsCollect_comm As Collect_comm
		On Error GoTo insPostMCO678_Err
		lclsCollect_comm = New Collect_comm
		With lclsCollect_comm
			.nCollectorType = nCollectorType
			.nConType = nConType
			.sCollecAsig = sCollecAsig
			.nDaysIni = nDaysIni
			.nDaysEnd = nDaysEnd
			.nCode = nCode
			.nInChannel = nInChannel
			.dEffecDate = dEffecDate
			.nBranch = nBranch
			.nProduct = nProduct
			.nInitRange = nInitRange
			.nEndRange = nEndRange
			.nCommPercent = nCommPercent
			.nCurrency = nCurrency
			.nCommAmount = nCommAmount
			.nUsercode = nUsercode
			.nMinAmount = nMinAmount
			.nMaxAmount = nMaxAmount
			.sDescript = sDescript
			.sShort_des = sShort_des
			
			Select Case Action
				Case "Add"
					insPostMCO678 = .Add
				Case "Del"
					insPostMCO678 = .Delete
				Case "Update"
					insPostMCO678 = .Update
			End Select
		End With
		
		'UPGRADE_NOTE: Object lclsCollect_comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCollect_comm = Nothing
		
insPostMCO678_Err: 
		If Err.Number Then
			insPostMCO678 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Add: se crean los registros en Collect_Comm
	Public Function Add() As Boolean
		Dim lrecCollect_Comm As eRemoteDB.Execute
		
		On Error GoTo Add_err
		lrecCollect_Comm = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creCollect_Comm'
		
		With lrecCollect_Comm
			.StoredProcedure = "creCollect_Comm"
			.Parameters.Add("nCollectortype", nCollectorType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContype", nConType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCollecasig", sCollecAsig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysini", nDaysIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysend", nDaysEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInchannel", nInChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitrange", nInitRange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndrange", nEndRange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommpercent", nCommPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommamount", nCommAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinAmount", nMinAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxAmount", nMaxAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCollect_Comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCollect_Comm = Nothing
		
	End Function
	'%Delete: Eliminar un registro de Bills_Num
	Public Function Delete() As Boolean
		Dim lrecdelCollect_Comm As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecdelCollect_Comm = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delCollect_Comm'
		With lrecdelCollect_Comm
			.StoredProcedure = "delCollect_Comm"
			.Parameters.Add("nCollectortype", nCollectorType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContype", nConType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCollecasig", sCollecAsig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysini", nDaysIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysend", nDaysEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInchannel", nInChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitrange", nInitRange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelCollect_Comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelCollect_Comm = Nothing
		
	End Function
	'%Update: Esta función se encarga de agregar/actualizar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Update() As Boolean
		Dim lrecCollect_Comm As eRemoteDB.Execute
		On Error GoTo Update_Err
		lrecCollect_Comm = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updCollect_Comm'
		With lrecCollect_Comm
			.StoredProcedure = "updCollect_Comm"
			.Parameters.Add("nCollectortype", nCollectorType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContype", nConType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCollecasig", sCollecAsig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysini", nDaysIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysend", nDaysEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInchannel", nInChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitrange", nInitRange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndrange", nEndRange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommpercent", nCommPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommamount", nCommAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinAmount", nMinAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxAmount", nMaxAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCollect_Comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCollect_Comm = Nothing
	End Function
	'% insCollect_commRange: Validacion de Rangos
	'                        - Dias de Atrazo
	'                        - UF Cobradas
	Public Function insCollect_commRange(ByVal nCollectorType As Integer, ByVal nConType As Integer, ByVal sCollecAsig As String, ByVal nDaysIni As Integer, ByVal nDaysEnd As Integer, ByVal nCode As Integer, ByVal nInChannel As Integer, ByVal dEffecDate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nInitRange As Double, ByVal nEndRange As Double) As Boolean
		Dim lvalCollect_comm As eRemoteDB.Execute
		On Error GoTo insCollect_commRange_Err
		lvalCollect_comm = New eRemoteDB.Execute
		
		With lvalCollect_comm
			.StoredProcedure = "Reacollect_comm_vr"
			.Parameters.Add("nCollectortype", nCollectorType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContype", nConType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCollecasig", sCollecAsig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysini", nDaysIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysend", nDaysEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInchannel", nInChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitrange", nInitRange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndrange", nEndRange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExiKey", nExiKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContDiasAtr", nContDiasAtr, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContUFCobr", nContUFCobr, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRanFinNull", nRanFinNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxRanFin", nMaxRanFin, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nExiKey = .Parameters("nExiKey").Value
				nContDiasAtr = .Parameters("nContDiasAtr").Value
				nContUFCobr = .Parameters("nContUFCobr").Value
				nRanFinNull = .Parameters("nRanFinNull").Value
				nMaxRanFin = .Parameters("nMaxRanFin").Value
				insCollect_commRange = True
			Else
				insCollect_commRange = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lvalCollect_comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalCollect_comm = Nothing
		
insCollect_commRange_Err: 
		If Err.Number Then
			insCollect_commRange = False
		End If
		
		On Error GoTo 0
	End Function
	'% insCollect_commRange_Up: Validacion de Rango
	'                           - UF Cobradas
	Public Function insCollect_commRange_Up(ByVal nCollectorType As Integer, ByVal nConType As Integer, ByVal sCollecAsig As String, ByVal nDaysIni As Integer, ByVal nDaysEnd As Integer, ByVal nCode As Integer, ByVal nInChannel As Integer, ByVal dEffecDate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nInitRange As Double, ByVal nEndRange As Double) As Boolean
		Dim lvalCollect_comm As eRemoteDB.Execute
		On Error GoTo insCollect_commRange_Up_Err
		lvalCollect_comm = New eRemoteDB.Execute
		
		With lvalCollect_comm
			.StoredProcedure = "Reacollect_comm_up"
			.Parameters.Add("nCollectortype", nCollectorType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContype", nConType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCollecasig", sCollecAsig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysini", nDaysIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysend", nDaysEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInchannel", nInChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitrange", nInitRange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndrange", nEndRange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nExist = .Parameters("nExist").Value
				insCollect_commRange_Up = True
			Else
				insCollect_commRange_Up = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lvalCollect_comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalCollect_comm = Nothing
		
insCollect_commRange_Up_Err: 
		If Err.Number Then
			insCollect_commRange_Up = False
		End If
		
		On Error GoTo 0
	End Function
	'% valCollect_Comm_Range: Validación del rango de días de atraso de cobro
	Public Function valCollect_Comm_Range(ByVal nCode As Integer, ByVal nInChannel As Integer, ByVal nCollectorType As Integer, ByVal nConType As Integer, ByVal sCollecAsig As String, ByVal nDaysIni As Integer, ByVal nDaysEnd As Integer, ByVal dEffecDate As Date) As Boolean
		Dim lrecvalCollect_Comm_Range As eRemoteDB.Execute
		On Error GoTo valCollect_Comm_Range_Err
		lrecvalCollect_Comm_Range = New eRemoteDB.Execute
		
		With lrecvalCollect_Comm_Range
			.StoredProcedure = "val_Collect_Comm_Range"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInChannel", nInChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollectorType", nCollectorType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConType", nConType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCollecAsig", sCollecAsig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysIni", nDaysIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysEnd", nDaysEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIsValid", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				valCollect_Comm_Range = .Parameters("nIsValid").Value = 1
			Else
				valCollect_Comm_Range = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecvalCollect_Comm_Range may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalCollect_Comm_Range = Nothing
		
valCollect_Comm_Range_Err: 
		If Err.Number Then
			valCollect_Comm_Range = False
		End If
		
		On Error GoTo 0
	End Function
End Class







Option Strict Off
Option Explicit On
Public Class tab_am_lim
	'%-------------------------------------------------------%'
	'% $Workfile:: tab_am_lim.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'-
	'- Estructura de tabla insudb.tab_am_lim al 06-23-2002 14:04:29
	'-  Property                    Type            DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public sIllness As String ' CHAR       8    0     0    N
	Public nPay_concep As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nLimit_per As Double ' NUMBER     22   2     5    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	
	'- Variables auxiliares
	Public sBrancht As String
	Public sCurrDes As String
	Public dMaxDate As Date
	Public sDescript As String
	
	'% Add all values related to a specific record
	Public Function Add() As Boolean
		Dim lclsTab_am_lim As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		lclsTab_am_lim = New eRemoteDB.Execute
		
		With lclsTab_am_lim
			.StoredProcedure = "creTab_am_lim"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimit_per", nLimit_per, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_am_lim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_am_lim = Nothing
	End Function
	
	'% Update the links for a specific client
	Public Function Update() As Boolean
		Dim lclsTab_am_lim As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lclsTab_am_lim = New eRemoteDB.Execute
		
		With lclsTab_am_lim
			.StoredProcedure = "updTab_am_lim"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimit_per", nLimit_per, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_am_lim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_am_lim = Nothing
	End Function
	
	'%Delete: Eliminate the corresponding information for a client, year and specific concept
	Public Function Delete() As Boolean
		Dim lclsTab_am_lim As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lclsTab_am_lim = New eRemoteDB.Execute
		
		With lclsTab_am_lim
			.StoredProcedure = "delTab_am_lim"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_am_lim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_am_lim = Nothing
	End Function
	
	'IsExist: Función que realiza la busqueda en la tabla 'insudb.tab_am_lim'
	Public Function IsExist(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCover As Integer, ByVal nPay_concep As Integer, ByVal nModulec As Integer, ByVal sIllness As String) As Boolean
		Dim lclsTab_am_lim As eRemoteDB.Execute
        Dim sExist As String = ""

        On Error GoTo IsExist_Err
		
		lclsTab_am_lim = New eRemoteDB.Execute
		IsExist = False
		
		With lclsTab_am_lim
			.StoredProcedure = "reatab_am_lim_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExist", sExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			IsExist = (.Parameters("sExist").Value = "1")
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_am_lim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_am_lim = Nothing
	End Function
	
	'insValMAM001_k: Función que realiza la validacion de los datos introducidos en el
	'    encabezado de la ventana
	Public Function insValMAM001_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCover As Integer, ByVal nPay_concep As Integer, ByVal nModulec As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		
		On Error GoTo insValMAM001_k_Err
		
		lclsErrors = New eFunctions.Errors
		lclsProduct = New eProduct.Product
		With lclsErrors
			'+ Se efectúan las validaciones del campo "Ramo".
			If nBranch <= 0 Then
				Call .ErrorMessage(sCodispl, 1022)
			End If
			
			'+ Se efectúan las validaciones del campo "Producto".
			If nProduct <= 0 Then
				Call .ErrorMessage(sCodispl, 11009)
			End If
			
			'+ Se valida el producto sea de vida
			If nBranch <> eRemoteDB.Constants.intNull And nBranch > 0 Then
				If nProduct <> eRemoteDB.Constants.intNull And nProduct > 0 Then
					Call lclsProduct.FindProdMaster(nBranch, nProduct)
					If CStr(lclsProduct.sBrancht) <> "1" Then
						Call .ErrorMessage(sCodispl, 1024)
					End If
				End If
			End If
			
			'+ Se efectúan las validaciones del campo "Módulo".
			If nModulec = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 11166)
			End If
			
			'+ Se efectúan las validaciones del campo "Cobertura".
			If nCover <= 0 Then
				Call .ErrorMessage(sCodispl, 11163)
			End If
			
			'+Se efectúan las validaciones del campo "Concepto".
			If nPay_concep <= 0 Then
				'+ Debe estar lleno
				Call .ErrorMessage(sCodispl, 13251)
			End If
			
			'+Se efectúan las validaciones del campo "Fecha".
			If dEffecdate = dtmNull Then
				'+ Debe estar lleno
				Call .ErrorMessage(sCodispl, 2056)
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					If dEffecdate < Today Then
						'+ La fecha debe ser posterior al día en curso, para poder hacer modificaciones
						Call .ErrorMessage(sCodispl, 10868)
					End If
					'+ La fecha debe ser posterior a la última fecha de actualización de los datos a mostrar
					If (nBranch > 0) And (nProduct > 0) And (nCover > 0) And (nPay_concep > 0) Then
						dMaxDate = ValEffecdate(nBranch, nProduct, nCover, nPay_concep, nModulec)
						If dMaxDate <> dtmNull Then
							If dMaxDate > dEffecdate Then
								Call .ErrorMessage(sCodispl, 10869,  , eFunctions.Errors.TextAlign.LeftAling, CStr(dMaxDate) & ": ")
							End If
						End If
					End If
				End If
			End If
			
			lclsProduct.nBranch = nBranch
			lclsProduct.nProduct = nProduct
			If dEffecdate < lclsProduct.CreationDate Then
				Call .ErrorMessage(sCodispl, 11394,  , eFunctions.Errors.TextAlign.RigthAling, " (" & lclsProduct.CreationDate & ")")
			End If
			insValMAM001_k = .Confirm
		End With
		
insValMAM001_k_Err: 
		If Err.Number Then
			insValMAM001_k = insValMAM001_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
	End Function
	
	'insValMAM001: Función que realiza la validacion de los datos introducidor en la sección
	'    de detalles de la ventana
	Public Function insValMAM001(ByVal sCodispl As String, ByVal sAction As String, ByVal sIllness As String, ByVal nLimit_per As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMAM001_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Validación del campo: Enfermedad
			If Trim(sIllness) = String.Empty Then
				Call .ErrorMessage(sCodispl, 4230)
			Else
				If sAction = "Add" Then
					If IsExist(nBranch, nProduct, dEffecdate, nCover, nPay_concep, nModulec, sIllness) Then
						Call .ErrorMessage(sCodispl, 10199)
					End If
				End If
			End If
			
			'+ Validación del campo: %Límite
			If nLimit_per <= 0 Then
				Call .ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "%Límite: ")
			Else
				If nLimit_per > 100 Then
					Call .ErrorMessage(sCodispl, 11239)
				End If
			End If
			insValMAM001 = .Confirm
		End With
		
insValMAM001_Err: 
		If Err.Number Then
			insValMAM001 = insValMAM001 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'PostMAM001: Función que realiza la validacion de los datos introducidor por la ventana
	Public Function insPostMAM001(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCover As Integer, ByVal nPay_concep As Integer, ByVal nModulec As Integer, ByVal sIllness As String, ByVal nLimit_per As Double) As Boolean
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nUsercode = nUsercode
			.dEffecdate = dEffecdate
			.nCover = nCover
			.nPay_concep = nPay_concep
			.nModulec = nModulec
			.sIllness = sIllness
			.nLimit_per = nLimit_per
		End With
		
		If pblnHeader Then
			insPostMAM001 = True
		Else
			Select Case sAction
				'+Si la opción seleccionada es Registrar
				Case "Add"
					insPostMAM001 = Add()
					
					'+Si la opción seleccionada es Modificar
				Case "Update"
					insPostMAM001 = Update()
					
					'+Si la opción seleccionada es Eliminar
				Case "Del"
					insPostMAM001 = Delete()
			End Select
		End If
	End Function
	
	'reaProdMaster1: Esta rutina realiza la lectura en la tabla 'Prodmaster' .
	Function reaProdMaster1(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		'Se define la variable lrecProdMaster para ejecutar el store procedure
		Dim lrecProdMaster As eRemoteDB.Execute
		
		On Error GoTo reaProdMaster1_Err
		
		lrecProdMaster = New eRemoteDB.Execute
		
		With lrecProdMaster
			.StoredProcedure = "reaProdmaster2"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				reaProdMaster1 = True
				sBrancht = .FieldToClass("sBrancht")
				sDescript = .FieldToClass("sDescript")
				.RCloseRec()
			End If
		End With
		
reaProdMaster1_Err: 
		If Err.Number Then
			reaProdMaster1 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecProdMaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecProdMaster = Nothing
	End Function
	
	'%insValGen_cover: El objetivo de este metodo es obtener si existen los datos de la cobertura
	'%de un producto general.
	Public Function insValGen_cover(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sStatregt As String) As Boolean
		'Se define la variable lrecGen_cover para ejecutar el store procedure
		Dim lrecGen_cover As eRemoteDB.Execute
		
		On Error GoTo insValGen_cover_Err
		
		lrecGen_cover = New eRemoteDB.Execute
		
		With lrecGen_cover
			.StoredProcedure = "reaGen_cover_desc_1"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				insValGen_cover = True
				
				sCurrDes = .FieldToClass("sCurrDes")
				
				.RCloseRec()
			End If
		End With
		
insValGen_cover_Err: 
		If Err.Number Then
			insValGen_cover = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecGen_cover = Nothing
	End Function
	
	
	'%insValGen_concept: función que verifica que el concepto se encuentre registrado en la tabla de cobertura
	Public Function insValGen_concept(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal nPay_concep As Integer, ByVal dEffecdate As Date) As Boolean
		'Se define la variable lrecreaConcepCover para ejecutar el store procedure
		Dim lrecreaConcepCover As eRemoteDB.Execute
		
		On Error GoTo insValGen_concept_Err
		
		lrecreaConcepCover = New eRemoteDB.Execute
		
		With lrecreaConcepCover
			.StoredProcedure = "reaConcepCover"
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				insValGen_concept = True
				.RCloseRec()
			End If
		End With
		
insValGen_concept_Err: 
		If Err.Number Then
			insValGen_concept = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaConcepCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaConcepCover = Nothing
	End Function
	
	'% insValEffecdate: El objetivo de esta función es obtener la máxima fecha de modificación.
	Public Function ValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal nPay_concep As Integer, ByVal nModulec As Integer) As Date
		Dim lrecinsValTab_am_lim As eRemoteDB.Execute
		Dim ldtmEffecdate As Date
		
		On Error GoTo ValEffecdate_Err
		
		lrecinsValTab_am_lim = New eRemoteDB.Execute
		
		With lrecinsValTab_am_lim
			.StoredProcedure = "insValTab_am_lim"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			ValEffecdate = .FieldToClass((.Parameters("dEffecdate").Value))
		End With
		
ValEffecdate_Err: 
		If Err.Number Then
			ValEffecdate = dtmNull
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsValTab_am_lim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValTab_am_lim = Nothing
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		sIllness = String.Empty
		nPay_concep = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		dCompdate = dtmNull
		nLimit_per = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class







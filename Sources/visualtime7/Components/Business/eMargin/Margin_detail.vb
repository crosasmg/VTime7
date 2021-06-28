Option Strict Off
Option Explicit On
Public Class Margin_detail
	'%-------------------------------------------------------%'
	'% $Workfile:: Margin_detail.cls                        $%'
	'% $Author:: Nvaplat15                                  $%'
	'% $Date:: 1/12/03 18.39                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'- Variables segun campos en la tabla al 22/05/2003
	
	'+ Nombre                   Tipo                      ¿Nulo?
	'+ ------------------------ ------------------------- ------
	Public nInsur_area As Integer 'NUMBER(5)      NO
	Public dInitdate As Date 'DATE           NO
	Public nIdtable As Integer 'NUMBER(10)     NO
	Public nIdrec As Integer 'NUMBER(10)     NO
	Public nInitialAmoOri As Double 'NUMBER(24,6)   NO
	Public nInitialAmoLoc As Double 'NUMBER(38, 10)
	Public nAdjAmoOri As Double 'NUMBER(24,6)   NO
	Public nAdjAmoLoc As Double 'NUMBER(38, 10)
	Public nBranch As Integer 'NUMBER(5)
	Public nProduct As Integer 'NUMBER(5)
	Public nCurrency As Integer 'NUMBER(5)      NO
	Public dValDate As Date 'DATE
	Public nTypeRec As Integer 'NUMBER(1)
	Public nModulec As Integer 'NUMBER(5)
	Public nCover As Integer 'NUMBER(5)
	Public nSVSClass As Integer 'NUMBER(5)      NO
	Public sStaDet As String 'CHAR(1)        NO
	Public nUsercode As Integer 'NUMBER(5)
	
	'+ Variables auxiliares
	
	'- Monto inicial + Monto de ajustes (en moneda origen)
	Public nAmountOri As Double
	
	'- Monto inicial + Monto de ajustes (en moneda local)
	Public nAmountLoc As Double
	
	'- Factor de cambio del detalle a la fecha de valorización
	Public nExchange As Double
	
	'- Variable para completar la llave de Margin_master
	Private mlngTableTyp As Integer
	Private mlngSource As Integer
	Private mlngClaimClass As Integer
	Private mdtmEndDate As Date
	
	'- Variable para indicar el número de movimientos de ajuste realizados al detalle
	Public nCountAdjust As Short
	
	'% insvalMGS001Upd: se realizan las validaciones de la ventana PopUp
	Public Function insvalMGS001Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nTableTyp As Integer, ByVal nInsur_area As Integer, ByVal dInitdate As Date, ByVal dEndDate As Date, ByVal nSource As Integer, ByVal nClaimClass As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dValDate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nInitialAmoOri As Double, ByVal sModule As String, ByVal nTypeRec As Integer) As String
		Dim ldblMax As Double
		Dim ldblMin As Double
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insvalMGS001Upd_err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			'+ Si el tipo de tabla es diferente de "Pasivos"
			If nTableTyp <> 5 Then
				'+ El ramo debe estar lleno
				If nBranch = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 9064)
				End If
				
				'+ El producto debe estar lleno
				If nProduct = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 1014)
				End If
				
				'+ El módulo debe estar lleno, si el producto es modular
				If nModulec = eRemoteDB.Constants.intNull And sModule = "1" And nTypeRec = 1 Then
					Call .ErrorMessage(sCodispl, 12112)
				End If
				
				'+ La cobertura debe estar llena
				If nCover = eRemoteDB.Constants.intNull And nTypeRec = 1 Then
					Call .ErrorMessage(sCodispl, 4061)
				End If
			End If
			
			'+ La fecha de valorización debe estar llena
			If dValDate = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 55527)
			End If
			
			'+ El monto inicial debe estar lleno
			If nInitialAmoOri = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 55917)
			Else
				ldblMax = 1E+18 '+ 999.999.999.999.999.999,99
				ldblMin = 0.01
				If nInitialAmoOri > ldblMax Or nInitialAmoOri < ldblMin Then
					'+ Debe encontrarse dentro del rango permitido
					Call .ErrorMessage(sCodispl, 1935,  , eFunctions.Errors.TextAlign.RigthAling, "(" & ldblMin & " - 999.999.999.999.999.999,99)")
				End If
			End If
			
			If sAction = "Add" Then
				If insvalExist(nInsur_area, dInitdate, dEndDate, nTableTyp, nSource, nClaimClass, nBranch, nProduct, nModulec, nCover) Then
					Call .ErrorMessage(sCodispl, 55913)
				End If
			End If
			
			insvalMGS001Upd = .Confirm
		End With
		
insvalMGS001Upd_err: 
		If Err.Number Then
			insvalMGS001Upd = "insvalMGS001Upd: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% insvalExist: verifica la existencia del registro en la tabla
	Private Function insvalExist(ByVal nInsur_area As Integer, ByVal dInitdate As Date, ByVal dEndDate As Date, ByVal nTableTyp As Integer, ByVal nSource As Integer, ByVal nClaimClass As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo insvalExist_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "valExist_Margin_detail"
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInitdate", dInitdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTableTyp", nTableTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSource", nSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaimClass", nClaimClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insvalExist = .Parameters("nExists").Value = 1
			End If
		End With
		
insvalExist_err: 
		If Err.Number Then
			insvalExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% inspostMGS001: se actualiza el maestro/detalle para el margen de solvencia
	Public Function inspostMGS001(ByVal sAction As String, ByVal nInsur_area As Integer, ByVal dInitdate As Date, ByVal nIdtable As Integer, ByVal nIdrec As Integer, ByVal nTableTyp As Integer, ByVal nSource As Integer, ByVal nClaimClass As Integer, ByVal dEndDate As Date, Optional ByVal nCurrency As Integer = 0, Optional ByVal nInitialAmoOri As Double = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal dValDate As Date = #12:00:00 AM#, Optional ByVal nTypeRec As Integer = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal nSVSClass As Integer = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		On Error GoTo inspostMGS001_err
		
		With Me
			.nInsur_area = nInsur_area
			.dInitdate = dInitdate
			.nIdtable = nIdtable
			.nIdrec = nIdrec
			mlngTableTyp = nTableTyp
			mlngSource = nSource
			mlngClaimClass = nClaimClass
			mdtmEndDate = dEndDate
			.nCurrency = nCurrency
			.nInitialAmoOri = nInitialAmoOri
			.nBranch = nBranch
			.nProduct = nProduct
			.dValDate = dValDate
			.nTypeRec = nTypeRec
			.nModulec = nModulec
			.nCover = nCover
			.nSVSClass = nSVSClass
			.nUsercode = nUsercode
			'+ Se asigna valor a la acción para ser tomada en el SP
			Select Case sAction
				Case "Add"
					inspostMGS001 = .Add()
				Case "Update"
					inspostMGS001 = .Update(2)
				Case "Del"
					inspostMGS001 = .Delete()
			End Select
		End With
		
inspostMGS001_err: 
		If Err.Number Then
			inspostMGS001 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Add: se crean los registros en la tabla
	Public Function Add() As Boolean
		Add = Update(1)
	End Function
	
	'% Delete: se eliminan los registros en la tabla
	Public Function Delete() As Boolean
		Delete = Update(3)
	End Function
	
	'% Update: actualiza los campos de la tabla
	Public Function Update(ByVal nAction As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo Update_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "insupdMargin_detail"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInitDate", dInitdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdTable", nIdtable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdRec", nIdrec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTableTyp", mlngTableTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSource", mlngSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaimClass", mlngClaimClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndDate", mdtmEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitialAmoOri", nInitialAmoOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 24, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValDate", dValDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeRec", nTypeRec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSVSClass", nSVSClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nIdtable = .Parameters("nIdTable").Value
				Update = True
			End If
		End With
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% Update_Stadet: se actualizan los registros del detalle como procesados
	Public Function Update_Stadet(ByVal nInsur_area As Integer, ByVal dInitdate As Date, ByVal nIdtable As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo Update_Stadet_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "updMargin_detail_sStadet"
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInitDate", dInitdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdTable", nIdtable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_Stadet = .Run(False)
		End With
		
Update_Stadet_err: 
		If Err.Number Then
			Update_Stadet = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
End Class







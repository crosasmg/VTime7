Option Strict Off
Option Explicit On
Option Compare Text
Public Class Bills_Num
	'%-------------------------------------------------------%'
	'% $Workfile:: Bills_Num.cls                            $%'
	'% $Author:: Nvaplat19                                  $%'
	'% $Date:: 25/08/03 6:46p                               $%'
	'% $Revision:: 22                                       $%'
	'%-------------------------------------------------------%'
	
	' Desarrollado por: Victor Gajardo
	' Fecha: 24-05-20001
	' Descripcion: Transaccion para manejo de tabla de Numeración de Facturas
	'
	
	'+ Descripcion de la tabla BILLS_NUM al 19/10/2001 16:20
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	'----------------------------- --------------- - -------- ------- ----- ------ --------
	Public nInsur_area As Integer ' NUMBER        22     5      0 No
	Public sBillType As String ' CHAR           1              No
	Public nInitnumb As Double ' NUMBER        22    10      0 No
	Public nEndnumb As Double ' NUMBER        22    10      0 Yes
	Public nLastbill As Double ' NUMBER        22    10      0 Yes
	Public dLastclosed As Date ' DATE           7              Yes
	Public dCompdate As Date ' DATE           7              No
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	
	Public nSumExis As Integer
	Public nSumCont As Integer
	
	
	'% insValMCO689_k: se realizan las validaciones del encabezado
	'                  de la Tabla de Numeración de Facturas Bills_Num
	Public Function insValMCO689_K(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal sBillType As String) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsvalField As eFunctions.valField
		
		On Error GoTo insValMCO689_K_Err
		
		lclsErrors = New eFunctions.Errors
		lclsvalField = New eFunctions.valField
		
		'+ El area debe estar llena : 55031
		If nInsur_area = eRemoteDB.Constants.intNull Then nInsur_area = 0
		If nInsur_area = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55031)
		End If
		
		
		' Sin mensajes de errore en funcional
		insValMCO689_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalField = Nothing
		
insValMCO689_K_Err: 
		If Err.Number Then
			insValMCO689_K = insValMCO689_K & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'%insValMCO689: se realizan las validaciones para la ventana
	'               Mantencion de Numeración de facturas
	Public Function insValMCO689(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal sBillType As String, ByVal nInitnumb As Double, ByVal nEndnumb As Double, ByVal nLastbill As Double, ByVal dCompdate As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsvalField As eFunctions.valField
		
		lclsErrors = New eFunctions.Errors
		lclsvalField = New eFunctions.valField
		
		On Error GoTo insValMCO689_Err
		
		If nInitnumb = eRemoteDB.Constants.intNull Then
			nInitnumb = 0
		End If
		
		If nEndnumb = eRemoteDB.Constants.intNull Then
			nEndnumb = 0
		End If
		
		'+ Fecha debe estar lleno : 2056
		If dCompdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 2056)
		End If
		
		'+ Rango inicial debe estar lleno : 10247
		If nInitnumb = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 10247)
		End If
		
		'+ Rango Final debe estar lleno: 10248
		If nEndnumb = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 10248)
		End If
		
		'+ Rango Final debe ser mayor a rango inicial: 10184
		If nInitnumb > nEndnumb Then
			Call lclsErrors.ErrorMessage(sCodispl, 10184)
		End If
		
		'+ Los Rangos de numeración no se deben repetir para la misma
		'+ área de seguros y tipos de documento : 55548
		If nInsur_area > 0 And sBillType <> "" And nInitnumb > 0 And nEndnumb > 0 Then
			If insBills_NumRange(nInsur_area, sBillType, nInitnumb, nEndnumb) Then
				If nSumExis > 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 55548)
				End If
				'+ Para la misma área de seguros y tipo de documento, un rango
				'+ de numeración no debe estar contenido en otro.: 55549
				If nSumCont > 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 55549)
				End If
			End If
		End If
		
		'+ Finaliza la validacion
		insValMCO689 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalField = Nothing
		
insValMCO689_Err: 
		If Err.Number Then
			insValMCO689 = "insValMCO689: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% Add: se crean los registros en Bills_Num
	Public Function Add() As Boolean
		Dim lreccreBills_Num As eRemoteDB.Execute
		
		On Error GoTo Add_err
		lreccreBills_Num = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creBills_Num'
		
		With lreccreBills_Num
			.StoredProcedure = "creBills_Num"
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBillType", sBillType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitNumb", nInitnumb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndNumb", nEndnumb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastBill", nLastbill, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLastClosed", dLastclosed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCompDate", dCompdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreBills_Num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreBills_Num = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'%Delete: Eliminar un registro de Bills_Num
	Public Function Delete() As Boolean
		Dim lrecdelBills_Num As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		lrecdelBills_Num = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delBills_Num'
		With lrecdelBills_Num
			.StoredProcedure = "delBills_Num"
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBillType", sBillType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitNumb", nInitnumb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelBills_Num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelBills_Num = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'%Update: Esta función se encarga de agregar/actualizar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Update() As Boolean
		Dim lrecBills_num As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		lrecBills_num = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updBills_Num'
		With lrecBills_num
			.StoredProcedure = "updBills_Num"
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBillType", sBillType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitNumb", nInitnumb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndNumb", nEndnumb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastBill", nLastbill, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLastClosed", dLastclosed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lrecBills_num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecBills_num = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'% insPostMCO689: Se realiza la actualización de los datos
	Public Function insPostMCO689(ByVal Action As String, ByVal nInsur_area As Integer, ByVal sBillType As String, ByVal nInitnumb As Double, ByVal nEndnumb As Double, ByVal nLastbill As Double, ByVal dLastclosed As Date, ByVal dCompdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsBills_num As Bills_Num
		lclsBills_num = New Bills_Num
		
		On Error GoTo insPostMCO689_Err
		
		If nLastbill = eRemoteDB.Constants.intNull Then nLastbill = 0
		
		With lclsBills_num
			.nInsur_area = nInsur_area
			.sBillType = sBillType
			.nInitnumb = nInitnumb
			.nEndnumb = nEndnumb
			.nLastbill = nLastbill
			.dLastclosed = dLastclosed
			.dCompdate = dCompdate
			.nUsercode = nUsercode
			
			
			Select Case Action
				Case "ADD"
					insPostMCO689 = .Add
				Case "DEL"
					insPostMCO689 = .Delete
			End Select
		End With
		
		'UPGRADE_NOTE: Object lclsBills_num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBills_num = Nothing
		
insPostMCO689_Err: 
		If Err.Number Then
			insPostMCO689 = False
		End If
		On Error GoTo 0
	End Function
	
	'% insBills_NumRange:- en tabla Bills_Num verifica que los rangos de numeración
	'                      no se deben repetir para la misma área de seguros y
	'                      tipos de documento, (error 55548).
	'                    - Para la misma área de seguros y tipo de documento, un rango
	'                      de numeración no debe estar contenido en otro.: 55549
	Public Function insBills_NumRange(ByVal nInsur_area As Integer, ByVal sBillType As String, ByVal nInitnumb As Double, ByVal nEndnumb As Double) As Boolean
		Dim lvalBills_Num As eRemoteDB.Execute
		
		On Error GoTo insBills_NumRange_Err
		
		lvalBills_Num = New eRemoteDB.Execute
		
		With lvalBills_Num
			.StoredProcedure = "REABILLS_NUM_RANGE"
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBillType", sBillType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitNumb", nInitnumb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndNumb", nEndnumb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSumExis", nSumExis, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSumCont", nSumCont, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				nSumExis = .Parameters("nSumExis").Value
				nSumCont = .Parameters("nSumCont").Value
				insBills_NumRange = True
			Else
				insBills_NumRange = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lvalBills_Num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalBills_Num = Nothing
		
insBills_NumRange_Err: 
		If Err.Number Then
			insBills_NumRange = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%getLastClosed: Obtiene la fecha máxima de expiración de todos los documentos seleccionados de una relación.
	Public Function getLastClosed(ByVal nInsur_area As Integer, ByVal sBillType As String) As Date
		Dim lrecBills_num As eRemoteDB.Execute
		
		On Error GoTo getLastClosed_Err
		
		lrecBills_num = New eRemoteDB.Execute
		
		getLastClosed = eRemoteDB.Constants.dtmNull
		
		With lrecBills_num
			.StoredProcedure = "reaBills_num_dLastType"
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBillType", sBillType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				getLastClosed = .FieldToClass("dLastClosed")
			End If
			
		End With
		
getLastClosed_Err: 
		If Err.Number Then
			getLastClosed = eRemoteDB.Constants.dtmNull
		End If
		'UPGRADE_NOTE: Object lrecBills_num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecBills_num = Nothing
		On Error GoTo 0
	End Function
	
	'%getLastBill: Obtiene la fecha máxima de expiración de todos los documentos seleccionados de una relación.
	Public Function getLastBill(ByVal nInsur_area As Integer, ByVal sBillType As String) As Double
		Dim lrecBills_num As eRemoteDB.Execute
		
		On Error GoTo getLastBill_Err
		
		lrecBills_num = New eRemoteDB.Execute
		
		getLastBill = 0
		
		With lrecBills_num
			.StoredProcedure = "reaBills_num_nLastBill"
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBillType", sBillType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				getLastBill = .FieldToClass("nLastBill")
			End If
			
		End With
		
getLastBill_Err: 
		If Err.Number Then
			getLastBill = 0
		End If
		'UPGRADE_NOTE: Object lrecBills_num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecBills_num = Nothing
		On Error GoTo 0
	End Function
	
	'%getNumeratorBills_num: Obtiene la fecha máxima de expiración de todos los documentos seleccionados de una relación.
	Public Function getNumeratorBills_num(ByVal nInsur_area As Integer, ByVal sBillType As String, ByVal nUsercode As Integer) As Double
		Dim lrecBills_num As eRemoteDB.Execute
		
		On Error GoTo getNumeratorBills_num_Err
		
		lrecBills_num = New eRemoteDB.Execute
		
		getNumeratorBills_num = -1
		
		With lrecBills_num
			.StoredProcedure = "insNumeratorBills_num"
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBillType", sBillType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNewNum", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				getNumeratorBills_num = .Parameters("nNewNum").Value
			End If
		End With
		
getNumeratorBills_num_Err: 
		If Err.Number Then
			getNumeratorBills_num = -1
		End If
		'UPGRADE_NOTE: Object lrecBills_num may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecBills_num = Nothing
		On Error GoTo 0
	End Function
End Class







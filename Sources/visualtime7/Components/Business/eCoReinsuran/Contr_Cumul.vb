Option Strict Off
Option Explicit On
Public Class Contr_Cumul
	'%-------------------------------------------------------%'
	'% $Workfile:: Contr_Cumul.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:28p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla contr_cumul al 03-25-2002 12:52:20
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nNumber As Integer ' NUMBER     22   0     5    N
	Public nBranch_rei As Integer ' NUMBER     22   0     5    N
	Public nType As Integer ' NUMBER     22   0     5    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdContr_cumul(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdContr_cumul(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdContr_cumul(3)
	End Function
	'+ Update : Actualiza un registro desde la tabla Retentioncov
	Function InsUpdContr_cumul(ByVal nAction As Integer) As Integer
		Dim lrecinsUpdContr_cumul As eRemoteDB.Execute
		On Error GoTo insUpdContr_cumul_Err
		
		lrecinsUpdContr_cumul = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdContr_cumul al 04-02-2002 11:47:40
		'+
		With lrecinsUpdContr_cumul
			.StoredProcedure = "insUpdcontr_cumul"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdContr_cumul = .Run(False)
		End With
		
insUpdContr_cumul_Err: 
		If Err.Number Then
			InsUpdContr_cumul = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdContr_cumul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdContr_cumul = Nothing
		On Error GoTo 0
	End Function
	'+ DelNullContr_Cumul :anula o elimina todos los registros asociados a un
	'+ contrato de la tabla Control de cúmulo por ramo/producto
	Function DelNullContr_Cumul(ByVal nNumber As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nAction As Integer) As Object
		Dim lrecDelNullContr_Cumul As eRemoteDB.Execute
		
		lrecDelNullContr_Cumul = New eRemoteDB.Execute
		On Error GoTo DelNullContr_Cumul_Err
		
		With lrecDelNullContr_Cumul
			.StoredProcedure = "insdelnullContr_Cumul"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("deffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DelNullContr_Cumul = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecDelNullContr_Cumul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelNullContr_Cumul = Nothing
DelNullContr_Cumul_Err: 
		If Err.Number Then
			DelNullContr_Cumul = False
		End If
		On Error GoTo 0
	End Function
	'+InsposCR758 : Función que realiza los cambios en la base de datos especificados en CR758
	Function InspostCR758Upd(ByVal sAction As String, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Object
		Dim lintAction As Integer
		
		On Error GoTo InspostCR758_Err
		
		If nProduct = eRemoteDB.Constants.intNull Then
			nProduct = 0
		End If
		
		With Me
			.nNumber = nNumber
			.nBranch_rei = nBranch_rei
			.nType = nType
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nUsercode = nUsercode
			
			If sAction = "Del" Then
				lintAction = 3
			Else
				If sAction = "Update" Then
					lintAction = 2
				Else
					If sAction = "Add" Then
						lintAction = 1
					End If
				End If
			End If
			
			Select Case lintAction
				Case 1
					'+ Se crea el registro
					InspostCR758Upd = .Add
					
					'+ Se modifica el registro
				Case 2
					InspostCR758Upd = .Update
					
					'+ Se elimina el registro
				Case 3
					InspostCR758Upd = .Delete
					
			End Select
		End With
		
InspostCR758_Err: 
		If Err.Number Then
			InspostCR758Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'+ Esta función realiza las validaciones de la forma CR758
	Function insvalCR758(ByVal sCodispl As String, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValNum As eFunctions.valField
		
		Dim lcolContr_cumuls As Contr_Cumuls
		Dim lclsContr_cumul As Contr_Cumul
		Dim lintCount As Object
		
		lclsErrors = New eFunctions.Errors
		lclsValNum = New eFunctions.valField
		lcolContr_cumuls = New Contr_Cumuls
		lclsContr_cumul = New Contr_Cumul
		
		On Error GoTo insValCR758_Err
		
		If nProduct = eRemoteDB.Constants.intNull Then
			nProduct = 0
		End If
		
		
		lintCount = 0
		
		'+Validación del campo zona
		'+Validación del Tipo de zona debe estar lleno
		If nBranch = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1022)
		Else
			'+Validación zona indicada no debe estar registrada en la tabla
			With lcolContr_cumuls
				If .Find(nNumber, nBranch_rei, nType, dEffecdate) Then
					For	Each lclsContr_cumul In lcolContr_cumuls
						If lclsContr_cumul.nBranch = nBranch And lclsContr_cumul.nProduct = nProduct And lintCount = 0 Then
							Call lclsErrors.ErrorMessage(sCodispl, 60337)
							lintCount = 1
						End If
					Next lclsContr_cumul
				End If
			End With
		End If
		
		
		
		
		insvalCR758 = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValNum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValNum = Nothing
		
insValCR758_Err: 
		If Err.Number Then
			insvalCR758 = insvalCR758 & Err.Description
		End If
		On Error GoTo 0
	End Function
End Class







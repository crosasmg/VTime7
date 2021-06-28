Option Strict Off
Option Explicit On
Public Class Retentioncov
	'%-------------------------------------------------------%'
	'% $Workfile:: Retentioncov.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:28p                                $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla RETENTIONCOV al 03-25-2002 09:37:37
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nNumber As Integer ' NUMBER     22   0     5    N
	Public nBranch_rei As Integer ' NUMBER     22   0     5    N
	Public nType As Integer ' NUMBER     22   0     5    N
	Public nInsur_area As Integer ' NUMBER     22   0     5    N
	Public nCovergen As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nRetention As Double ' NUMBER     22   2     10   S
	Public sRoutine As String ' CHAR       12   0     0    S
	Public nCovpropor As Integer ' NUMBER     22   0     5    S
	Public nComblim As Double ' NUMBER     22   2     10   S
	Public nCovercl As Integer ' NUMBER     22   0     5    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdretentioncov(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdretentioncov(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdretentioncov(3)
	End Function
	'+ Esta función realiza las validaciones de la forma CR572
	Function insvalCR572(ByVal sCodispl As String, ByVal sMainAction As String, ByVal nInsur_area As Integer, ByVal nCovergen As Integer, ByVal nRetention As Double, ByVal sRoutine As String, ByVal nCovpropor As Integer, ByVal nComlim As Double, ByVal nCovercl As Integer, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValNum As eFunctions.valField
		Dim lintCount As Integer
		Dim lintCompanyAUX As Integer
		Dim ldblTotalPercent As Double
		Dim lcolretentioncovs As Retentioncovs
		Dim lclsRetentioncov As Retentioncov
		
		lclsErrors = New eFunctions.Errors
		lclsValNum = New eFunctions.valField
		lcolretentioncovs = New Retentioncovs
		lclsRetentioncov = New Retentioncov
		
		On Error GoTo insValCR572_Err
		
		lintCount = 0
		
		'+Validación del campo cobertura
		If nCovergen = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60315)
		Else
			With lcolretentioncovs
				If .Find(nNumber, nBranch_rei, nType, dEffecdate) Then
					For	Each lclsRetentioncov In lcolretentioncovs
						If lclsRetentioncov.nCovergen = nCovergen Then
							Call lclsErrors.ErrorMessage(sCodispl, 60322)
							lintCount = 1
							Exit For
						End If
					Next lclsRetentioncov
				End If
			End With
		End If
		'+Validación del Tipo de cobertura, debe estar lleno
		If nInsur_area = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60321)
		End If
		'+Valida campo retención
		If sRoutine = String.Empty And nCovpropor = eRemoteDB.Constants.intNull And nComlim = eRemoteDB.Constants.intNull And nCovercl = eRemoteDB.Constants.intNull And nRetention = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60323)
			Call lclsErrors.ErrorMessage(sCodispl, 60324)
			Call lclsErrors.ErrorMessage(sCodispl, 60325)
			Call lclsErrors.ErrorMessage(sCodispl, 60326)
		End If
		'+ se valida cobertura en limite combinado
		If nComlim <> eRemoteDB.Constants.intNull And nCovercl = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60327)
		End If
		insvalCR572 = lclsErrors.Confirm
		
insValCR572_Err: 
		If Err.Number Then
			insvalCR572 = insvalCR572 & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValNum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValNum = Nothing
		
		On Error GoTo 0
	End Function
	'+InsposCR572 : Función que realiza los cambios en la base de datos especificados en CR572
	Public Function InspostCR572Upd(ByVal sAction As String, ByVal nInsur_area As Integer, ByVal nCovergen As Integer, ByVal nRetention As Double, ByVal sRoutine As String, ByVal nCovpropor As Integer, ByVal nComlim As Double, ByVal nCovercl As Integer, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lintAction As Integer
		
		On Error GoTo InspostCR572_Err
		
		With Me
			.nNumber = nNumber
			.nBranch_rei = nBranch_rei
			.nType = nType
			.nInsur_area = nInsur_area
			.nCovergen = nCovergen
			.dEffecdate = dEffecdate
			.nRetention = nRetention
			.sRoutine = sRoutine
			.nCovpropor = nCovpropor
			.nComblim = nComlim
			.nCovercl = nCovercl
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
					InspostCR572Upd = .Add
					'+ Se modifica el registro
				Case 2
					InspostCR572Upd = .Update
					'+ Se elimina el registro
				Case 3
					InspostCR572Upd = .Delete
			End Select
		End With
		
InspostCR572_Err: 
		If Err.Number Then
			InspostCR572Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'+ Update : Actualiza un registro desde la tabla Retentioncov
	Function InsUpdretentioncov(ByVal nAction As Integer) As Boolean
		Dim lrecFind As eRemoteDB.Execute
		Dim lclsRetentioncov As Retentioncov
		
		On Error GoTo Find_Err
		
		lrecFind = New eRemoteDB.Execute
		
		With lrecFind
			.StoredProcedure = "insUpdretentioncov"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRetention", nRetention, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovpropor", nCovpropor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nComblim", nComblim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovercl", nCovercl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdretentioncov = .Run(False)
		End With
		
Find_Err: 
		If Err.Number Then
			InsUpdretentioncov = False
		End If
		
		'UPGRADE_NOTE: Object lrecFind may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFind = Nothing
		
		On Error GoTo 0
	End Function
	
	'+ DelNullRetentioncov :anula o elimina todos los registros asociados a un contrato
	'+ de la tabla de retención por coverturas
	Function DelNullRetentioncov(ByVal nNumber As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nAction As Integer) As Object
		Dim lrecDelNullRetentioncov As eRemoteDB.Execute
		
		On Error GoTo DelNullRetentioncov_Err
		
		lrecDelNullRetentioncov = New eRemoteDB.Execute
		
		With lrecDelNullRetentioncov
			.StoredProcedure = "insdelnullretentioncov"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("deffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DelNullRetentioncov = .Run(False)
		End With
DelNullRetentioncov_Err: 
		If Err.Number Then
			DelNullRetentioncov = False
		End If
		'UPGRADE_NOTE: Object lrecDelNullRetentioncov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelNullRetentioncov = Nothing
		On Error GoTo 0
	End Function
End Class







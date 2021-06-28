Option Strict Off
Option Explicit On
Public Class Retentionzone
	'%-------------------------------------------------------%'
	'% $Workfile:: Retentionzone.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:28p                                $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla RETENTIONzone al 03-25-2002 12:12:58
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nNumber As Integer ' NUMBER     22   0     5    N
	Public nBranch_rei As Integer ' NUMBER     22   0     5    N
	Public nType As Integer ' NUMBER     22   0     5    N
	Public nSeismiczone As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nRetention As Double ' NUMBER     22   2     10   S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdretentionzone(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdretentionzone(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdretentionzone(3)
	End Function
	'+ Esta función realiza las validaciones de la forma CR572
	Function insvalCR760(ByVal sAction As String, ByVal sCodispl As String, ByVal nRetention As Double, ByVal nSeismiczone As Integer, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValNum As eFunctions.valField
		
		Dim lcolretentionzones As Retentionzones
		Dim lclsRetentionzone As Retentionzone
		Dim lintCount As Object
		
		lclsErrors = New eFunctions.Errors
		lclsValNum = New eFunctions.valField
		lcolretentionzones = New Retentionzones
		lclsRetentionzone = New Retentionzone
		On Error GoTo insValCR760_Err
		
		lintCount = 0
		
		'+Validación del campo zona
		'+Validación del Tipo de zona debe estar lleno
		If sAction = "Add" Then
			If nSeismiczone = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60328)
			Else
				'+Validación zona indicada no debe estar registrada en la tabla
				With lcolretentionzones
					If .Find(nNumber, nBranch_rei, nType, dEffecdate) Then
						For	Each lclsRetentionzone In lcolretentionzones
							If lclsRetentionzone.nSeismiczone = nSeismiczone And lintCount = 0 Then
								Call lclsErrors.ErrorMessage(sCodispl, 60329)
								lintCount = 1
							End If
						Next lclsRetentionzone
					End If
				End With
			End If
		End If
		
		'+Valida campo retención
		If nRetention = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60323)
		End If
		
		insvalCR760 = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValNum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValNum = Nothing
		
insValCR760_Err: 
		If Err.Number Then
			insvalCR760 = insvalCR760 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'+ Update : Actualiza un registro desde la tabla Retentioncov
	Function InsUpdretentionzone(ByVal nAction As Integer) As Integer
		Dim lrecinsUpdretentionzone As eRemoteDB.Execute
		On Error GoTo insUpdretentionzone_Err
		
		lrecinsUpdretentionzone = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdretentionzone al 04-02-2002 11:47:40
		'+
		With lrecinsUpdretentionzone
			.StoredProcedure = "insUpdretentionzone"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeismiczone", nSeismiczone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRetention", nRetention, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdretentionzone = .Run(False)
		End With
		
insUpdretentionzone_Err: 
		If Err.Number Then
			InsUpdretentionzone = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdretentionzone may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdretentionzone = Nothing
		On Error GoTo 0
	End Function
	'+ DelNullRetentionzone :anula o elimina todos los registros asociados a un
	'+ contrato de la tabla de retención por zonas
	Function DelNullRetentionzone(ByVal nNumber As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nAction As Integer) As Object
		Dim lrecDelNullRetentionzone As eRemoteDB.Execute
		
		lrecDelNullRetentionzone = New eRemoteDB.Execute
		On Error GoTo DelNullRetentionzone_Err
		
		With lrecDelNullRetentionzone
			.StoredProcedure = "insdelnullretentionzone"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("deffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DelNullRetentionzone = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecDelNullRetentionzone may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelNullRetentionzone = Nothing
DelNullRetentionzone_Err: 
		If Err.Number Then
			DelNullRetentionzone = False
		End If
		On Error GoTo 0
	End Function
	'+InsposCR572 : Función que realiza los cambios en la base de datos especificados en CR572
	Function InspostCR760Upd(ByVal sAction As String, ByVal nSeismiczone As Integer, ByVal nRetention As Double, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Object
		Dim lintAction As Integer
		
		On Error GoTo InspostCR760_Err
		
		With Me
			.nNumber = nNumber
			.nBranch_rei = nBranch_rei
			.nType = nType
			.nSeismiczone = nSeismiczone
			.dEffecdate = dEffecdate
			.nRetention = nRetention
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
					InspostCR760Upd = .Add
					
					'+ Se modifica el registro
				Case 2
					InspostCR760Upd = .Update
					
					'+ Se elimina el registro
				Case 3
					InspostCR760Upd = .Delete
					
			End Select
		End With
		
InspostCR760_Err: 
		If Err.Number Then
			InspostCR760Upd = False
		End If
		On Error GoTo 0
	End Function
End Class







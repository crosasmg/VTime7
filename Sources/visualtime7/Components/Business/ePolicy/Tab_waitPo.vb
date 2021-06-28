Option Strict Off
Option Explicit On
Public Class Tab_waitPo
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_waitPo.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla tab_waitpo al 08-26-2002 17:39:45
	'+      Property                Type         DBType   Size Scale  Prec  Null
	'+---------------------------------------------------------------------------
	Public nWait_code As Integer ' NUMBER     22   0     5    N
	Public dCompdate As Date ' DATE       7    0     0    N
	Public sDescript As String ' CHAR       30   0     0    S
	Public sShort_des As String ' CHAR       12   0     0    S
	Public sStatregt As String ' CHAR       1    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nOrder As Integer ' NUMBER     22   0     5    N
	Public nAreaWait As Integer ' NUMBER     22   0     5    N
	Public sConvert As String ' CHAR       1    0     0    S
	'+ Variables de uso de la clase
	Public nActions As Integer
	Public nExist As Integer
	
	'%Find. Este metodo se encarga de realizar la busqueda de los datos correspondientes para la
	'%tabla "Tab_waitPo". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Find(ByVal nWait_code As Integer, ByVal nOrder As Integer) As Boolean
		Dim lrecTab_waitPo As eRemoteDB.Execute
		On Error GoTo Find_Err
		lrecTab_waitPo = New eRemoteDB.Execute
		Find = False
		With lrecTab_waitPo
			.StoredProcedure = "reaTab_waitPo"
			.Parameters.Add("nWait_code", nWait_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nWait_code = .FieldToClass("nWait_code")
				sDescript = .FieldToClass("sDescript")
				sShort_des = .FieldToClass("sShort_des")
				sStatregt = .FieldToClass("sStatregt")
				nUsercode = .FieldToClass("nUsercode")
				nAreaWait = .FieldToClass("nAreaWait")
				nOrder = .FieldToClass("nOrder")
				sConvert = .FieldToClass("sConvert")
				Find = True
				.RCloseRec()
			End If
		End With
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_waitPo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_waitPo = Nothing
	End Function
	
	'%Add. Este metodo se encarga de realizar la insercion de los datos correspondientes para la
	'%tabla "Tab_waitPo". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Add() As Boolean
		Dim lrecTab_waitPo As eRemoteDB.Execute
		On Error GoTo Add_err
		lrecTab_waitPo = New eRemoteDB.Execute
		With lrecTab_waitPo
			.StoredProcedure = "insTab_waitPo"
			.Parameters.Add("nActions", nActions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWait_code", nWait_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAreaWait", nAreaWait, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sConvert", sConvert, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_waitPo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_waitPo = Nothing
	End Function
	
	'%Update. Este metodo se encarga de realizar actualizar de los datos correspondientes para la
	'%tabla "Tab_waitPo". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Update() As Boolean
		Dim lrecTab_waitPo As eRemoteDB.Execute
		On Error GoTo Update_Err
		lrecTab_waitPo = New eRemoteDB.Execute
		With lrecTab_waitPo
			.StoredProcedure = "insTab_waitPo"
			.Parameters.Add("nActions", nActions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWait_code", nWait_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAreaWait", nAreaWait, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sConvert", sConvert, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_waitPo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_waitPo = Nothing
	End Function
	
	'%Delete. Este metodo se encarga de eliminar los registros  de los datos correspondientes para la
	'%tabla "Tab_waitPo". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Delete() As Boolean
		Dim lrecTab_waitPo As eRemoteDB.Execute
		On Error GoTo Delete_err
		lrecTab_waitPo = New eRemoteDB.Execute
		With lrecTab_waitPo
			.StoredProcedure = "insTab_waitPo"
			.Parameters.Add("nActions", nActions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWait_code", nWait_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAreaWait", nAreaWait, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sConvert", sConvert, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_waitPo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_waitPo = Nothing
	End Function
	
	'%insValMCA005: Validación de los campos que son ingresados en la popup de la pagina MCA005
	Public Function insValMCA005(ByVal sCodispl As String, ByVal sActions As String, ByVal nWait_code As Integer, ByVal nOrder As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nAreaWait As Integer, ByVal sStatregt As String) As String
		Dim lclsErrors As New eFunctions.Errors
		On Error GoTo insValMCA005_err
		
		'+ Si el campo causa no esta lleno, ninguna de los campos debe estar lleno
		If nWait_code = eRemoteDB.Constants.intNull Or nWait_code = 0 Then
			If (nOrder <> eRemoteDB.Constants.intNull And nOrder <> 0) Or sDescript <> String.Empty Or sShort_des <> String.Empty Or (nAreaWait <> eRemoteDB.Constants.intNull And nAreaWait <> 0) Or (sStatregt <> String.Empty And sStatregt <> "0") Then
				Call lclsErrors.ErrorMessage(sCodispl, 1084)
			End If
		End If
		
		'+ El campo Orden debe estar lleno
		If (nOrder = eRemoteDB.Constants.intNull Or nOrder = 0) Then
			Call lclsErrors.ErrorMessage(sCodispl, 60483)
		End If
		
		If sActions = "Add" Then
			'+ Si la acción es registrar el campo causa debe estar lleno
			If nWait_code = eRemoteDB.Constants.intNull Or nWait_code = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 10872)
			Else
				'+ si la acción es registrar no debe existir en el sistema (tab_waitpo)
				If Find(nWait_code, eRemoteDB.Constants.intNull) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10004)
				End If
			End If
			'+ Si la acción es registrar no debe existir en la tabla tab_waitpo
			If (nOrder <> eRemoteDB.Constants.intNull And nOrder <> 0) Then
				If Find(eRemoteDB.Constants.intNull, nOrder) Then
					Call lclsErrors.ErrorMessage(sCodispl, 60484)
				End If
			End If
		End If
		
		'+ si el campo causa esta lleno, la descripcion tambien debe estar llena
		If nWait_code <> eRemoteDB.Constants.intNull And nWait_code <> 0 Then
			If sDescript = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 10005)
			End If
			'+ Si el campo causa esta lleno, la descripcion abreviada debe estar llena
			If sShort_des = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 10006)
			End If
			'+ Si el campo causa esta lleno, el estado debe estar lleno
			If (sStatregt = String.Empty Or sStatregt = "0") Then
				Call lclsErrors.ErrorMessage(sCodispl, 9089)
			End If
		End If
		
		If nAreaWait = eRemoteDB.Constants.intNull Or nAreaWait = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60485)
		End If
		
		insValMCA005 = lclsErrors.Confirm
		
insValMCA005_err: 
		If Err.Number Then
			insValMCA005 = "tab_waitpo.insValMCA005: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insPostMCA005: Actualizaciíon de los datos ingresados en las causas pendientes
	Public Function insPostMCA005(ByVal sActions As String, ByVal nWait_code As Integer, ByVal nOrder As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nAreaWait As Integer, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal sConvert As String) As Boolean
		With Me
			.nWait_code = nWait_code
			.nOrder = nOrder
			.sDescript = sDescript
			.sShort_des = sShort_des
			.nAreaWait = nAreaWait
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			.sConvert = sConvert
			Select Case UCase(sActions)
				Case "ADD"
					.nActions = 1
					insPostMCA005 = .Add()
				Case "UPDATE"
					.nActions = 2
					insPostMCA005 = .Update()
				Case "DEL"
					.nActions = 3
					insPostMCA005 = .Delete()
			End Select
		End With
	End Function
	
	'%Find_WaitCode: Busca en certificat si existe alguna poliza certificado asociada a la causa pendiente
	Private Sub Find_WaitCode(ByVal nWait_code As Integer)
		Dim lrecTab_waitPo As eRemoteDB.Execute
		On Error GoTo Find_WaitCode_Err
		lrecTab_waitPo = New eRemoteDB.Execute
		'Si nexist = 1 existen datos
		'Si nexist = 2 no existen datos
		With lrecTab_waitPo
			.StoredProcedure = "reaCertificat_WaitCode"
			.Parameters.Add("nWait_Code", nWait_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nExist = .Parameters("nExist").Value
			End If
		End With
		
Find_WaitCode_Err: 
		If Err.Number Then
			nExist = 2
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_waitPo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_waitPo = Nothing
	End Sub
End Class







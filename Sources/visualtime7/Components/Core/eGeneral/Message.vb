Option Strict Off
Option Explicit On
Public Class Message
	'%-------------------------------------------------------%'
	'% $Workfile:: Message.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:24p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	Private Const clngActionQuery As Short = 401
	Private Const clngActionCondition As Short = 402
	Private Const clngActionadd As Short = 301
	Private Const clngActioncut As Short = 303
	
	
	Public ntipo As Integer 'smallint   no       2         5    0     no                                  (n/a)                               (n/a)
	Public nErrorNum As Integer 'int        no       4        10    0     no                                  (n/a)                               (n/a)
	Public sMessaged As String 'char       no       1                    yes                                 yes                                 yes
	Public sCodispl As String 'char       no       1                    yes                                 yes                                 yes
	Public nUsercode As Integer 'smallint   no       2         5    0     no                                  (n/a)                               (n/a)
	
	'**% Update: updates records in the message table.
	'%Update: Esta rutina se encarga de actualizar los registros de la tabla message.
	Public Function Update() As Boolean
		
		'**- Variable definition for the execution of the SP and the parameteres.
		'-Se define la variable para la ejecución de los SP y de los parámetros
		
		Dim ltempUpdMessage As eRemoteDB.Execute
		
		On Error GoTo Update_err
		
		ltempUpdMessage = New eRemoteDB.Execute
		
		Update = True
		
		With ltempUpdMessage
			.StoredProcedure = "updMessage"
			.Parameters.Add("nErrornum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMessage", sMessaged, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 80, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run(False) Then
				Update = False
			End If
		End With
		'UPGRADE_NOTE: Object ltempUpdMessage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ltempUpdMessage = Nothing
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Delete: delete records in the message table.
	'%Delete: Esta rutina se encarga de borrar el registros de la tabla message.
	Public Function Delete() As Boolean
		
		'**- Variable definition of the execution of the SP and the parameteres.
		'-Se define la variable para la ejecución de los SP y de los parámetros
		
		Dim ltempDelMessage As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		ltempDelMessage = New eRemoteDB.Execute
		
		'**- Variable definition for the field treatment
		'-Se define la variable para el tratamiento de los campos
		
		Delete = True
		
		With ltempDelMessage
			.StoredProcedure = "delMessage"
			.Parameters.Add("nErrornum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If Not .Run(False) Then
				Delete = False
			End If
		End With
		
		'UPGRADE_NOTE: Object ltempDelMessage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ltempDelMessage = Nothing
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		
	End Function
	'**% Add: add records to the message table.
	'%Add: Esta rutina se encarga de Añadir registro en la tabla message.
	Public Function Add() As Boolean
		
		'**- Variable definition for the use of the SP and the parameters sent to the same.
		'-Se define la variable para el uso del SP y de los parámetros enviados al mismo
		
		Dim ltempCreMessage As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		ltempCreMessage = New eRemoteDB.Execute
		
		Add = True
		
		With ltempCreMessage
			
			.StoredProcedure = "creMessage"
			.Parameters.Add("nErrornum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMessage", sMessaged, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 80, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run(False) Then
				Add = False
			End If
		End With
		
		'UPGRADE_NOTE: Object ltempCreMessage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ltempCreMessage = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Find: validates the control in the message table if the message code already exists.
	'%Find: valida contra la tabla message si ya el código del mensaje existe
	Public Function Find(ByVal nErrorNum As Integer) As Boolean
		
		'**- Variable definition for the treatment with the SP and with the parameteres
		'-Se define la variable para el tratamiento con el SP y con los parámetros
		
		Dim ltempReaMessage_v As eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		ltempReaMessage_v = New eRemoteDB.Execute
		
		Find = True
		
		With ltempReaMessage_v
			.StoredProcedure = "reaMessage"
			.Parameters.Add("nErrornum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
            If .Run Then
                Me.nErrorNum = .FieldToClass("nErrorNum")
                Me.sMessaged = .FieldToClass("sMessaged")
                .RCloseRec()
            Else
                Find = False
            End If
		End With
		
        ltempReaMessage_v = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insValMS001_K: Validates the error's number.
	'% insValMS001_K: Valida el numero de Error
	Public Function insValMS001_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sAction As String, ByVal nErrorNum As Integer, ByVal sMessaged As String) As String
		
		Dim lclsMessages As eGeneral.Messages
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMS001_K_err
		
		lclsMessages = New eGeneral.Messages
		lclsErrors = New eFunctions.Errors
		sAction = Trim(sAction)
		If nAction <> 402 Then
			If nErrorNum = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 10043)
			Else
				If sAction = "Add" Then
					If Find(nErrorNum) Then
						Call lclsErrors.ErrorMessage(sCodispl, 10004)
					End If
				Else
					If Not Find(nErrorNum) Then
						Call lclsErrors.ErrorMessage(sCodispl, 10053)
					Else
						If sAction = "Del" Then
							If lclsMessages.Find_WinMessage(nErrorNum) Then
								Call lclsErrors.ErrorMessage(sCodispl, 10860)
							End If
						End If
					End If
				End If
			End If
			If sMessaged = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 10042)
			End If
		Else
			If nErrorNum <> eRemoteDB.Constants.intNull And nErrorNum <> 0 Then
				If Not Find(nErrorNum) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10053)
				End If
			End If
			If Trim(sMessaged) <> String.Empty Then
				If Not lclsMessages.Find(nErrorNum, sMessaged) Then
					Call lclsErrors.ErrorMessage(sCodispl, 1073)
				End If
			End If
		End If
		insValMS001_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsMessages may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMessages = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMS001_K_err: 
		If Err.Number Then
			insValMS001_K = insValMS001_K & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insPostMS001: Updates the Error Message Window.
	'% insPostMS001: Actualiza la Ventana de Mensajes de Error
	Public Function insPostMS001_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nErrorNum As Integer, ByVal sMessaged As String, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostMS001_K_err
		
		With Me
			.sCodispl = sCodispl
			.nErrorNum = nErrorNum
			.sMessaged = sMessaged
			.nUsercode = nUsercode
			
		End With
		sAction = Trim(sAction)
		Select Case sAction
			
			'**+ If the selected option is Add
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMS001_K = Add
				
				'**+ If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMS001_K = Update
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMS001_K = Delete
				
		End Select
		
insPostMS001_K_err: 
		If Err.Number Then
			insPostMS001_K = False
		End If
		On Error GoTo 0
		
	End Function
End Class







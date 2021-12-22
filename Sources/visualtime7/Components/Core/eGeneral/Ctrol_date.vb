Option Strict Off
Option Explicit On
Public Class Ctrol_date
	Private mvarCtrol_dates As Ctrol_dates
	'%-------------------------------------------------------%'
	'% $Workfile:: Ctrol_date.cls                           $%'
	'% $Author:: Gletelier                                  $%'
	'% $Date:: 28/10/09 12:52p                              $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	Public nType_proce As Integer
	Public dEffecdate As Date
	Public nUsercode As Integer
	Public nStatusinstance As Integer
	Public sDescript As String
	Public dLedgerdat As Date
	
	'%Update: Actualizacion de la tabla Ctrol_date
	Public Function Update() As Boolean
		Dim lrecCtrol_date As New eRemoteDB.Execute
		lrecCtrol_date = New eRemoteDB.Execute
		
		With lrecCtrol_date
			.StoredProcedure = "updCtrol_date"
			.Parameters.Add("nType_proce", nType_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCtrol_date = Nothing
	End Function
	
	'%Delete: Elimina un proceso de la tabla Ctrol_date
	Public Function Delete() As Boolean
		Dim lrecCtrol_date As New eRemoteDB.Execute
		lrecCtrol_date = New eRemoteDB.Execute
		With lrecCtrol_date
			.StoredProcedure = "delCtrol_date"
			.Parameters.Add("nType_proce", nType_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCtrol_date = Nothing
	End Function
	
	'%insCreCtrol_date: Esta función se encarga de agregar la información en tratamiento de la
	'%                  tabla principal para la transacción.
	Private Function Add() As Boolean
		'- Se define la variable lcreCtrol_date para la ejecución del StoredProcedire
		Dim lcreCtrol_date As eRemoteDB.Execute
		
		lcreCtrol_date = New eRemoteDB.Execute
		
		With lcreCtrol_date
			.StoredProcedure = "creCtrol_date"
			.Parameters.Add("nType_proce", nType_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lcreCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcreCtrol_date = Nothing
		
	End Function
	
	'% insValMS100_K: Valida el Ctrol_date
	Public Function insValMS100_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nType_proce As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As String
		
		Dim lclsCtrol_date As eGeneral.GeneralFunction
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMS100_K_err
		
		
		lclsCtrol_date = New eGeneral.GeneralFunction
		lclsErrors = New eFunctions.Errors
		
		sAction = Trim(sAction)
		
		If sAction = "Add" Then
			If nType_proce = eRemoteDB.Constants.intNull Or nType_proce < 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 10867)
			End If
			If Find(nType_proce) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10866)
			End If
		End If
		
		If sAction <> "Del" Then
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 2056)
			End If
		End If
		
		insValMS100_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCtrol_date = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMS100_K_err: 
		If Err.Number Then
			insValMS100_K = insValMS100_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%insPostMS100_K: Actualiza la Ventana de Ctrol_date
	Public Function insPostMS100_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nType_proce As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostMS100_K_err
		
		sAction = Trim(sAction)
		
		With Me
			.nType_proce = nType_proce
			.dEffecdate = dEffecdate
			.nUsercode = nUsercode
			
		End With
		Select Case sAction
			
			'+ If the selected option is Add
			'+ Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMS100_K = Add
				
				'+ If the selected option is Modify
				'+ Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMS100_K = Update
				
				'+ If the selected option is Delete.
				'+ Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMS100_K = Delete
				
		End Select
		
insPostMS100_K_err: 
		If Err.Number Then
			insPostMS100_K = False
		End If
		On Error GoTo 0
	End Function
	
	'%Find: Busca un proceso en la tabla ctrol_date
	Public Function Find(ByVal nType_proce As Integer) As Boolean
		'- Se define la variable lrecCtrol_date para la ejecución del StoredProcedure
		
		Dim lrecCtrol_date As eRemoteDB.Execute
		
		lrecCtrol_date = New eRemoteDB.Execute
		
		With lrecCtrol_date
			.StoredProcedure = "reaCtrol_date"
			.Parameters.Add("nType_proce", nType_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				dEffecdate = .FieldToClass("dEffecdate")
				Me.nType_proce = nType_proce
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCtrol_date = Nothing
	End Function
	
	'%insPostMCA815: Actualización de los datos ingresados en las causas pendientes
	Public Function insPostCP8000(ByVal sActions As String, ByVal nType_proce As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		With Me
			.nType_proce = nType_proce
			.nUsercode = nUsercode
			.dEffecdate = dEffecdate
			Select Case UCase(sActions)
				Case "ADD"
					insPostCP8000 = Add()
				Case "UPDATE"
					insPostCP8000 = Update()
				Case "DEL"
					insPostCP8000 = Delete()
			End Select
		End With
	End Function
	
	Public Function insValCP8000_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nType_proce As Integer, ByVal dEffecdate As Date) As String
		
		Dim lclsCtrol_date As eGeneral.Ctrol_date
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValCP8000_K_err
		
		lclsCtrol_date = New eGeneral.Ctrol_date
		lclsErrors = New eFunctions.Errors
		
		sAction = Trim(sAction)
		
		If sAction = "Add" Then
			If nType_proce = eRemoteDB.Constants.intNull Or nType_proce < 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 10867)
			End If
			If Find(nType_proce) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10866)
			End If
		End If
		
		If sAction = "Update" Then
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 2056)
			End If
			If nType_proce = eRemoteDB.Constants.intNull Or nType_proce < 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 10867)
			End If
		End If
		
		insValCP8000_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCtrol_date = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCP8000_K_err: 
		If Err.Number Then
			insValCP8000_K = insValCP8000_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	Public Function InsValdLedgerdat(ByVal nType_proce As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecCtrol_date As eRemoteDB.Execute
		lrecCtrol_date = New eRemoteDB.Execute
		
		On Error GoTo InsValdLedgerdat_err
		
		With lrecCtrol_date
			.StoredProcedure = "InsValdLedgerdat"
			.Parameters.Add("nType_proce", nType_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters("nCount").Value > 0 Then
					InsValdLedgerdat = True
				Else
					InsValdLedgerdat = False
				End If
			End If
		End With
		'UPGRADE_NOTE: Object lrecCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCtrol_date = Nothing
		
InsValdLedgerdat_err: 
		If Err.Number Then
			InsValdLedgerdat = False
		End If
		On Error GoTo 0
		
	End Function
	
	Public Function Find_dLedgerdat(ByVal nType_proce As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecCtrol_date As eRemoteDB.Execute
		lrecCtrol_date = New eRemoteDB.Execute
		
		With lrecCtrol_date
			.StoredProcedure = "Find_dLedgerdat"
			.Parameters.Add("nType_proce", nType_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NBRANCH", DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPRODUCT", DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NTRANSACTION", DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLedgerdat", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				dLedgerdat = .Parameters("dLedgerdat").Value
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCtrol_date = Nothing
	End Function
End Class







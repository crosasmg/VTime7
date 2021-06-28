Option Strict Off
Option Explicit On
Public Class Tab_am_gex
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_am_gex.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table Tab_am_gex in the system on Octube 17,2001.
	'*- Propiedades según la tabla Tab_am_gex en el sistema el 17/10/2001.
	'+ Column_name                      Type            Computed     Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource                Collation
	'+ -------------------------------  --------------- ------------ ----------- ----- ----- ----------------------------------- ----------------------------------- ----------------------------------- --------------------------------------------------------------------------------------------------------------------------------
	Public dEffecdate As Date 'datetime       no           8                       no                                  (n/a)                               (n/a)                               NULL
	Public sIllness As String 'char           no           8                       no                                  no                                  no                                  SQL_Latin1_General_CP1_CI_AS
	Public nExc_code As Integer 'smallint       no           2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public dExc_date As Date 'datetime       no           8                       no                                  (n/a)                               (n/a)                               NULL
	Public dNulldate As Date 'datetime       no           8                       yes                                 (n/a)                               (n/a)                               NULL
	Public nUsercode As Integer 'smallint       no           2           5     0     yes                                 (n/a)                               (n/a)                               NULL
	
	Public sDesIll As String
	
	'% Add: Permite agregar registros en la tabla Tab_am_gex.
	Public Function Add() As Boolean
		'- Se define la variable lrecCreTab_am_gex.
		Dim lrecCreTab_am_gex As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		lrecCreTab_am_gex = New eRemoteDB.Execute
		
		With lrecCreTab_am_gex
			.StoredProcedure = "creTab_am_Ill_Gex"
			.Parameters.Add("nExc_code", nExc_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExc_Date", dExc_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCreTab_am_gex may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreTab_am_gex = Nothing
	End Function
	
	'% Update: Permite actualizar registros en la tabla Tab_am_gex.
	Public Function Update() As Boolean
		'- Se define la variable lrecUpdTab_am_gex.
		Dim lrecUpdTab_am_gex As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecUpdTab_am_gex = New eRemoteDB.Execute
		
		With lrecUpdTab_am_gex
			.StoredProcedure = "updTab_am_Ill_Gex"
			.Parameters.Add("nExc_code", nExc_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExc_Date", dExc_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecUpdTab_am_gex may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdTab_am_gex = Nothing
	End Function
	
	'% Delete: Permite eliminar registros en la tabla Tab_am_gex.
	Public Function Delete() As Boolean
		'- Se define la variable lrecDelTab_am_gex.
		Dim lrecDelTab_am_gex As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecDelTab_am_gex = New eRemoteDB.Execute
		
		With lrecDelTab_am_gex
			.StoredProcedure = "delTab_am_Ill_Gex"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExc_date", dExc_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDelTab_am_gex may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelTab_am_gex = Nothing
	End Function
	
	'% insValMAM002_K: Realiza la validación de los campos del Header de la ventana
	'% MAM002 - Exclusiones generales de enfermedades.
	Public Function insValMAM002_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal dEffecdate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		Dim ldtmMaxDate As Date
		
		On Error GoTo insValMAM002_K_Err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			'+ Validaciones sobre el campo "Fecha de Efecto".
			If dEffecdate = dtmNull Then
				Call .ErrorMessage(sCodispl, 2056)
			Else
				If Not IsDate(dEffecdate) Then
					Call .ErrorMessage(sCodispl, 1001)
				Else
					If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
						If dEffecdate <= Today Then
							Call .ErrorMessage(sCodispl, 10868)
						End If
						
						ldtmMaxDate = insValMaxEffecdate()
						
						If ldtmMaxDate <> dtmNull Then
							If dEffecdate < ldtmMaxDate Then
								Call .ErrorMessage(sCodispl, 1021,  , eFunctions.Errors.TextAlign.RigthAling, " (" & ldtmMaxDate & ")")
							End If
						End If
					End If
				End If
			End If
			insValMAM002_K = .Confirm
		End With
		
insValMAM002_K_Err: 
		If Err.Number Then
			insValMAM002_K = insValMAM002_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insValMaxEffecdate: Busca la máxima fecha de actualización para una enfermedad en particular.
	Public Function insValMaxEffecdate() As Date
		Dim lrecTab_am_gex As eRemoteDB.Execute
		Dim ldtmEffecdate As Date
		
		lrecTab_am_gex = New eRemoteDB.Execute
		
		On Error GoTo insValMaxEffecdate_Err
		
		With lrecTab_am_gex
			.StoredProcedure = "reaMaxTabAmGex"
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			insValMaxEffecdate = .Parameters("dEffecdate").Value
		End With
		
insValMaxEffecdate_Err: 
		If Err.Number Then
			insValMaxEffecdate = dtmNull
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_am_gex may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_am_gex = Nothing
	End Function
	
	'% insValMAM002: Realiza la validación de los campos del Deatlle de la ventana
	'% MAM002 - Tarifas de granizo.
	Public Function insValMAM002(ByVal sCodispl As String, ByVal sIllness As String, ByVal dEffecdate As Date, ByVal nExc_code As Integer, ByVal dExc_date As Date, ByVal sAction As String) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		insValMAM002 = String.Empty
		
		On Error GoTo insValMAM002_Err
		
		With lobjErrors
			'+ Se realizan las validaciones del campo "Enfermedad".
			If sIllness = "0" Or sIllness = String.Empty Then
				Call .ErrorMessage(sCodispl, 4230)
			Else
				If sAction <> "Update" Then
					If insRea_Dup_Tab_am_Gex(sIllness, dEffecdate) Then
						Call .ErrorMessage(sCodispl, 3609)
					End If
				End If
			End If
			
			'+ Se realizan las validaciones del campo "Causa".
			If nExc_code <= 0 Then
				Call .ErrorMessage(sCodispl, 3978)
			End If
			
			'+ Se valida que el campo "Fecha".
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			If dExc_date = dtmNull Or IsNothing(dExc_date) Then
				Call .ErrorMessage(sCodispl, 2056)
			Else
				If dExc_date <= Today Then
					Call .ErrorMessage(sCodispl, 10109)
				End If
			End If
			
			insValMAM002 = .Confirm
		End With
		
insValMAM002_Err: 
		If Err.Number Then
			insValMAM002 = insValMAM002 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'%insRea_Dup_Tab_am_Gex: Verifica la existencia de registros duplicados para una enfermedad excluída.
	Private Function insRea_Dup_Tab_am_Gex(ByVal sIllness As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecTab_am_gex As eRemoteDB.Execute
		
		On Error GoTo insRea_Dup_Tab_am_Gex_Err
		
		lrecTab_am_gex = New eRemoteDB.Execute
		
		With lrecTab_am_gex
			.StoredProcedure = "reaIll_Gex"
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				insRea_Dup_Tab_am_Gex = True
			End If
		End With
		
insRea_Dup_Tab_am_Gex_Err: 
		If Err.Number Then
			insRea_Dup_Tab_am_Gex = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_am_gex may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_am_gex = Nothing
	End Function
	
	'% insPostMAM002: Esta función se encarga de almacenar los datos en las tablas, en este caso Tab_am_gex.
	Public Function insPostMAM002(ByVal sAction As String, ByVal dEffecdate As Date, ByVal sIllness As String, ByVal nExc_code As Integer, ByVal dExc_date As Date, ByVal nUsercode As Integer) As Boolean
		Me.dEffecdate = dEffecdate
		Me.sIllness = sIllness
		Me.nExc_code = nExc_code
		Me.dExc_date = dExc_date
		Me.nUsercode = nUsercode
		
		Select Case sAction
			
			'+ Si la opción seleccionada es Registrar.
			Case "Add"
				insPostMAM002 = Add()
				
				'+ Si la opción seleccionada es Modificar.
			Case "Update"
				insPostMAM002 = Update()
				
				'+ Si la opción seleccionada es Modificar.
			Case "Del"
				insPostMAM002 = Delete()
				
		End Select
	End Function
End Class







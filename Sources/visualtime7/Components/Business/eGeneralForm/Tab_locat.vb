Option Strict Off
Option Explicit On
Public Class Tab_locat
	
	'Column_name                       Type     Computed    Length   Prec  Scale Nullable     TrimTrailingBlanks     FixedLenNullInSource
	Public nLocal As Integer 'smallint    no         2       5     0     no          (n/a)                         (n/a)
	Public sDescript As String 'char        no        30                   yes          no                            yes
	Public nProvince As Integer 'smallint    no         2       5     0     no          (n/a)                         (n/a)
	Public sShort_des As String 'char        no        12                   yes          no                            yes
	Public sLegal_loc As String 'char        no         6                   yes          no                            yes
	Public nUsercode As Integer 'smallint    no         2       5     0     no          (n/a)                         (n/a)
	Public sDescript_Prov As String 'char        no        30                   yes          no                            yes
	
	'% Find: Devuelve la descripción de una localidad dado un código de localidad
	Public Function Find(ByVal intLocal As Integer, Optional ByRef lblnFind As Boolean = False) As Boolean
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Static lblnRead As Boolean
		
		'- Se define la variable lrecreaTab_locat
		Dim lrecreaTab_locat As eRemoteDB.Execute
		
		lrecreaTab_locat = New eRemoteDB.Execute
		
		If nLocal <> intLocal Or lblnFind Then
			
			nLocal = intLocal
			
			'+ Definición de parámetros para stored procedure 'insudb.reaTab_locat'
			'+ Información leída el 27/10/2000 02:16:16 PM
			With lrecreaTab_locat
				.StoredProcedure = "reaTab_locat"
				.Parameters.Add("nLocal", intLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					nLocal = .FieldToClass("nLocal")
					sDescript = .FieldToClass("sDescript")
					nProvince = .FieldToClass("nProvince")
					sShort_des = .FieldToClass("sShort_des")
					sLegal_loc = .FieldToClass("sLegal_loc")
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find = lblnRead
		'UPGRADE_NOTE: Object lrecreaTab_locat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_locat = Nothing
	End Function
	
	'%Find_Default: Busca la ciudad y la región dado el código postal
	Public Function Find_Default(ByRef Zip_code As Integer) As Boolean
		Dim lrecreaZip_codeDefault As eRemoteDB.Execute
		
		lrecreaZip_codeDefault = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaZip_codeDefault'
		'Información leída el 23/11/2000 4:05:58 PM
		
		With lrecreaZip_codeDefault
			.StoredProcedure = "reaZip_codeDefault"
			.Parameters.Add("nZip_code", Zip_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nLocal = .FieldToClass("nLocal")
				nProvince = .FieldToClass("nProvince")
				sDescript = .FieldToClass("sDescript")
				Find_Default = True
				.RCloseRec()
			Else
				Find_Default = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaZip_codeDefault may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaZip_codeDefault = Nothing
		
	End Function
	
	'% ValExistTab_locat: Valida que el código de la provincia no exista en la tabla de Provincias (Tab_locat)
	Public Function ValExistTab_locat(ByVal nLocal As Integer) As Boolean
		
		Dim lrecreatab_locat_v1 As eRemoteDB.Execute
		lrecreatab_locat_v1 = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reatab_locat_v1'
		'+ Información leída el 06/07/2001 09:51:44 a.m.
		With lrecreatab_locat_v1
			.StoredProcedure = "reatab_locat_v1"
			.Parameters.Add("nLocal", nLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Me.nLocal = .FieldToClass("nLocal")
				sDescript = .FieldToClass("sDescript")
				nProvince = .FieldToClass("nProvince")
				sShort_des = .FieldToClass("sShort_des")
				sLegal_loc = .FieldToClass("sLegal_loc")
				
				.RCloseRec()
				ValExistTab_locat = True
			Else
				ValExistTab_locat = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreatab_locat_v1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatab_locat_v1 = Nothing
	End Function
	
	'% Add: Agrega un registro a la tabla de Localidades (Tab_locat)
	Public Function Add() As Boolean
		Dim lreccreTab_locat As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lreccreTab_locat = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creTab_locat'
		'+ Información leída el 06/07/2001 05:37:41 p.m.
		With lreccreTab_locat
			.StoredProcedure = "creTab_locat"
			.Parameters.Add("nLocal", nLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvince", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLegal_loc", sLegal_loc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreTab_locat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTab_locat = Nothing
	End Function
	
	'% Update : Actualiza un registro en la tabla de Localidades (Tab_locat)
	Public Function Update() As Boolean
		Dim lrecupdTab_locat As eRemoteDB.Execute
		
		On Error GoTo Update_err
		
		lrecupdTab_locat = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updTab_locat'
		'+ Información leída el 06/07/2001 05:42:58 p.m.
		With lrecupdTab_locat
			.StoredProcedure = "updTab_locat"
			.Parameters.Add("nLocal", nLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvince", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLegal_loc", sLegal_loc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdTab_locat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTab_locat = Nothing
	End Function
	
	'% Delete: Elimina un registro de la tabla de Localidades (Tab_locat)
	Public Function Delete() As Boolean
		Dim lrecdelTab_locat As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		lrecdelTab_locat = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delTab_locat'
		'+ Información leída el 06/07/2001 05:47:29 p.m.
		
		With lrecdelTab_locat
			.StoredProcedure = "delTab_locat"
			.Parameters.Add("nLocal", nLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelTab_locat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTab_locat = Nothing
	End Function
	
	'% insValMS108: Valida los datos introducidos en la página
	'---------------------------------------------------------
	Public Function insValMS108(ByVal sCodispl As String, ByVal nLocal As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nProvince As Integer, ByVal sLegal_loc As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sWin_type As String) As String
		'---------------------------------------------------------
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMS108_Err
		
		lclsErrors = New eFunctions.Errors
		
		If sAction <> "Del" Then
			
			'+ Se valida el campo llave "Localidad"
			If sAction = "Add" Then
				If nLocal <> numNull Then
					
					'+ Se valida que el valor introducido en el campo no se encuentre en la tabla registrado
					If ValExistTab_locat(nLocal) Then
						Call lclsErrors.ErrorMessage(sCodispl, 10862)
					End If
				Else
					Call lclsErrors.ErrorMessage(sCodispl, 10838)
				End If
			End If
			
			'+ Si el campo "Provincia" tiene valor los demas campos deben estar llenos
			If ((nMainAction = eFunctions.Menues.TypeActions.clngActionUpdate) Or (nMainAction = eFunctions.Menues.TypeActions.clngActionadd)) And nLocal <> numNull Then
				If sDescript = String.Empty Then
					Call lclsErrors.ErrorMessage(sCodispl, 10836)
				End If
				
				If sShort_des = String.Empty Then
					Call lclsErrors.ErrorMessage(sCodispl, 10837)
				End If
				
				If nProvince = numNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 10839)
				End If
				
				If sLegal_loc = String.Empty Then
					Call lclsErrors.ErrorMessage(sCodispl, 10840)
				End If
			End If
		End If
		
		insValMS108 = lclsErrors.Confirm
		
insValMS108_Err: 
		If Err.Number Then
			insValMS108 = insValMS108 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insPostMS108: Valida los datos introducidos en la zona de contenido para "frame" especifico
	Public Function insPostMS108(ByVal sAction As String, ByVal nMainAction As Integer, ByVal nLocal As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nProvince As Integer, ByVal nUsercode As Integer, ByVal sLegal_loc As String) As Boolean
		With Me
			.nLocal = nLocal
			.sDescript = sDescript
			.sShort_des = sShort_des
			.nProvince = nProvince
			.sLegal_loc = sLegal_loc
			.nUsercode = nUsercode
			
			sAction = Trim(sAction)
			
			Select Case sAction
				Case "Add"
					insPostMS108 = Add
				Case "Del"
					insPostMS108 = Delete
				Case "Update"
					insPostMS108 = Update
			End Select
		End With
		
	End Function
	
	'%Find_by_municipality: Busca la ciudad y la región dada la comuna
	Public Function Find_by_municipality(ByVal nMunicipality As Integer) As Boolean
		Dim lrecReaMunicipalityDefault As eRemoteDB.Execute
		
		On Error GoTo Find_by_municipality_Err
		
		lrecReaMunicipalityDefault = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaMunicipalityDefault'
		'+Información leída el 25/03/2002
		With lrecReaMunicipalityDefault
			.StoredProcedure = "ReaMunicipalityDefault"
			.Parameters.Add("nMunicipality", nMunicipality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_by_municipality = True
				nLocal = .FieldToClass("nLocal")
				nProvince = .FieldToClass("nProvince")
			End If
		End With
		
Find_by_municipality_Err: 
		If Err.Number Then
			Find_by_municipality = False
		End If
		'UPGRADE_NOTE: Object lrecReaMunicipalityDefault may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaMunicipalityDefault = Nothing
		On Error GoTo 0
	End Function
End Class







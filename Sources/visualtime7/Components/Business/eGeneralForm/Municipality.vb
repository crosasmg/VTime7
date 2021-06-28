Option Strict Off
Option Explicit On
Public Class Municipality
	
	'Column_name                       Type     Computed    Length   Prec  Scale Nullable     TrimTrailingBlanks     FixedLenNullInSource
	Public nLocal As Integer 'smallint    no         2       5     0     no          (n/a)                         (n/a)
	Public sDescript As String 'char        no        30                   yes          no                            yes
	Public nMunicipality As Integer 'smallint    no         2       5     0     no          (n/a)                         (n/a)
	Public sShort_des As String 'char        no        12                   yes          no                            yes
	Public nUsercode As Integer 'smallint    no         2       5     0     no          (n/a)                         (n/a)
	Public sDescript_Prov As String 'char        no        30                   yes          no                            yes
	
	'% Find: Devuelve la descripción de una comuna dado el código
	Public Function Find(ByVal nMunicipality As Integer) As Boolean
		'- Se define la variable lrecreaMunicipality
		Dim lrecreaMunicipality As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		Me.nMunicipality = nMunicipality
		lrecreaMunicipality = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaMunicipality'
		'+ Información leída el 27/10/2000 02:16:16 PM
		With lrecreaMunicipality
			.StoredProcedure = "reaMunicipality"
			.Parameters.Add("nMunicipality", nMunicipality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nLocal = .FieldToClass("nLocal")
				sDescript = .FieldToClass("sDescript")
				sShort_des = .FieldToClass("sShort_des")
				.RCloseRec()
				Find = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaMunicipality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMunicipality = Nothing
	End Function
	
	'% Add: Agrega un registro a la tabla de Comunas (Municipality)
	Public Function Add() As Boolean
		Dim lreccreMunicipality As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lreccreMunicipality = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creMunicipality'
		'+ Información leída el 06/07/2001 05:37:41 p.m.
		With lreccreMunicipality
			.StoredProcedure = "creMunicipality"
			.Parameters.Add("nMunicipality", nMunicipality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLocal", nLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreMunicipality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreMunicipality = Nothing
	End Function
	
	'% Update : Actualiza un registro en la tabla de comunas (Municipality)
	Public Function Update() As Boolean
		Dim lrecupdMunicipality As eRemoteDB.Execute
		
		On Error GoTo Update_err
		
		lrecupdMunicipality = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updMunicipality'
		'+ Información leída el 06/07/2001 05:42:58 p.m.
		With lrecupdMunicipality
			.StoredProcedure = "updMunicipality"
			.Parameters.Add("nMunicipality", nMunicipality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLocal", nLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdMunicipality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdMunicipality = Nothing
	End Function
	
	'% Delete: Elimina un registro de la tabla de Comunas (Municipality)
	Public Function Delete() As Boolean
		Dim lrecdelMunicipality As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		lrecdelMunicipality = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delMunicipality'
		'+ Información leída el 06/07/2001 05:47:29 p.m.
		
		With lrecdelMunicipality
			.StoredProcedure = "delMunicipality"
			.Parameters.Add("nMunicipality", nMunicipality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelMunicipality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelMunicipality = Nothing
	End Function
	
	'% insValMS112: Valida los datos introducidos en la página
	'---------------------------------------------------------
	Public Function insValMS112(ByVal sCodispl As String, ByVal nMunicipality As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nLocal As Integer, ByVal sAction As String) As String
		'---------------------------------------------------------
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMS112_Err
		
		lclsErrors = New eFunctions.Errors
		
		If sAction <> "Del" Then
			
			'+ Se valida el campo llave "Localidad"
			If sAction = "Add" Then
				If nMunicipality > 0 Then
					
					'+ Se valida que el valor introducido en el campo no se encuentre en la tabla registrado
					If Find(nMunicipality) Then
						Call lclsErrors.ErrorMessage(sCodispl, 55160)
					End If
				Else
					Call lclsErrors.ErrorMessage(sCodispl, 1970)
				End If
			End If
			
			'+ Si el campo "Provincia" tiene valor los demas campos deben estar llenos
			If nMunicipality > 0 Then
				If sDescript = String.Empty Then
					Call lclsErrors.ErrorMessage(sCodispl, 10005)
				End If
				If sShort_des = String.Empty Then
					Call lclsErrors.ErrorMessage(sCodispl, 10006)
				End If
				If nLocal <= 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 1077)
				End If
			End If
		End If
		insValMS112 = lclsErrors.Confirm
		
insValMS112_Err: 
		If Err.Number Then
			insValMS112 = insValMS112 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insPostMS112: Valida los datos introducidos en la zona de contenido para "frame" especifico
	Public Function insPostMS112(ByVal sAction As String, ByVal nMunicipality As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nLocal As Integer, ByVal nUsercode As Integer) As Boolean
		With Me
			.nLocal = nLocal
			.sDescript = sDescript
			.sShort_des = sShort_des
			.nMunicipality = nMunicipality
			.nUsercode = nUsercode
			
			sAction = Trim(sAction)
			
			Select Case sAction
				Case "Add"
					insPostMS112 = Add
				Case "Del"
					insPostMS112 = Delete
				Case "Update"
					insPostMS112 = Update
			End Select
		End With
		
	End Function
End Class







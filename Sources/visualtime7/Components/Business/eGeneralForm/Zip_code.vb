Option Strict Off
Option Explicit On
Public Class Zip_code
	
	'- Propiedades según la tabla en el sistema 15/11/2000 Zip_code "Códigos postales".
	'- Los campos llave corresponden a: nZip_code y nLocal.
	
	'-  Column_name         Type                  Length Prec Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'- ------------------  --------------------- ------ ---- ----- -------- ------------------ --------------------
	Public nZip_Code As Integer 'int      4      10   0     no       (n/a)              (n/a)
	Public nLocal As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nOffice As Integer 'smallint 2      5    0     yes      (n/a)              (n/a)
	Public nAuto_zone As Integer 'smallint 2      5    0     yes      (n/a)              (n/a)
	Public nOrder As Integer 'smallint 2      5    0     yes      (n/a)              (n/a)
	Public nUsercode As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	
	'- Campo de la tabla Tab_locat
	Public sShort_des As String
	
	'- Se define la variable que determina el estado de la clase
	Public Enum eStatusInstance
		eftNew = 0
		eftQuery = 1
		eftExist = 1
		eftUpDate = 2
		eftDelete = 3
	End Enum
	
	Public nStatInstanc As eStatusInstance
	
	'% Find: Permite seleccionar la información de la tabla Zip_code
	Public Function Find(ByVal lintZip_code As Integer, ByVal lintLocal As Integer, Optional ByVal lFind As Boolean = False) As Boolean
		Dim lrecreaZip_Code As eRemoteDB.Execute
		
		If lintZip_code <> nZip_Code Or lintLocal <> nLocal Or lFind Then
			
			lrecreaZip_Code = New eRemoteDB.Execute
			
			'Definición de parámetros para stored procedure 'insudb.reaZip_code'
			'Información leída el 15/11/2000 10:21:52 AM
			
			With lrecreaZip_Code
				.StoredProcedure = "reaZip_code"
				.Parameters.Add("nZip_code", lintZip_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nLocal", lintLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nZip_Code = .FieldToClass("nZip_code")
					nLocal = .FieldToClass("nLocal")
					nOffice = .FieldToClass("nOffice")
					nAuto_zone = .FieldToClass("nAuto_zone")
					nOrder = .FieldToClass("nOrder")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaZip_Code may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaZip_Code = Nothing
		End If
	End Function
	
	' %Add: Funcion que agrega registros en la tabla Zip_code
	Public Function Add() As Boolean
		
		Dim lreccreZip_Code As eRemoteDB.Execute
		lreccreZip_Code = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creZip_Code'
		'+ Información leída el 15/11/2000 10:30:03 AM
		
		With lreccreZip_Code
			.StoredProcedure = "creZip_Code"
			.Parameters.Add("nZip_Code", nZip_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLocal", nLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAuto_Zone", nAuto_zone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreZip_Code may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreZip_Code = Nothing
	End Function
	
	'%  Update: Permite actualizar un registro en la tabla Zip_code
	Public Function Update() As Boolean
		Dim lrecupdZip_Code As eRemoteDB.Execute
		
		lrecupdZip_Code = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updZip_Code'
		'Información leída el 15/11/2000 10:25:46 AM
		
		With lrecupdZip_Code
			.StoredProcedure = "updZip_Code"
			.Parameters.Add("nZip_Code", nZip_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLocal", nLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAuto_Zone", nAuto_zone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdZip_Code may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdZip_Code = Nothing
	End Function
	
	'% Delete : Permite eliminar un registro de la tabla Zip_code
	Public Function Delete() As Boolean
		Dim lrecdelZip_Code As eRemoteDB.Execute
		
		lrecdelZip_Code = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.delZip_Code'
		'Información leída el 15/11/2000 10:27:02 AM
		
		With lrecdelZip_Code
			.StoredProcedure = "delZip_Code"
			.Parameters.Add("nZip_Code", nZip_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLocal", nLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelZip_Code may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelZip_Code = Nothing
	End Function
	
	Public Function Find_Address_ZipLocal_a(ByVal lintZip_code As Integer, ByVal lintLocal As Integer) As Boolean
		
		Dim lrecreaAddress_ZipLocal_a As eRemoteDB.Execute
		lrecreaAddress_ZipLocal_a = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaAddress_ZipLocal_a'
		'Información leída el 15/11/2000 10:39:54 AM
		
		With lrecreaAddress_ZipLocal_a
			.StoredProcedure = "reaAddress_ZipLocal_a"
			.Parameters.Add("nZip_Code", lintZip_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLocal", lintLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Address_ZipLocal_a = True
				.RCloseRec()
			Else
				Find_Address_ZipLocal_a = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaAddress_ZipLocal_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAddress_ZipLocal_a = Nothing
	End Function
	
	'% ValZip_codeExist: Valida que la provincia a eliminar no se encuentre asociada a una zona postal
	Public Function ValZip_codeExist(ByVal nLocal As Integer) As Boolean
		
		Dim lrecreazip_code_v As eRemoteDB.Execute
		
		On Error GoTo ValZip_codeExist_Err
		
		lrecreazip_code_v = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reazip_code_v'
		'+ Información leída el 06/07/2001 01:56:18 p.m.
		
		With lrecreazip_code_v
			.StoredProcedure = "reazip_code_v"
			.Parameters.Add("nLocal", nLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nZip_Code = .FieldToClass("nZip_Code")
				Me.nLocal = .FieldToClass("nLocal")
				nOffice = .FieldToClass("nOffice")
				nAuto_zone = .FieldToClass("nAuto_zone")
				nOrder = .FieldToClass("nOrder")
				
				.RCloseRec()
				ValZip_codeExist = True
			Else
				ValZip_codeExist = False
			End If
		End With
		
ValZip_codeExist_Err: 
		If Err.Number Then
			ValZip_codeExist = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreazip_code_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreazip_code_v = Nothing
	End Function
	
	'@@@@@@@@@@@@@@@@@@@@ FUNCIONES DE VALIDACIÓN Y EJECUCIÓN (VAL Y POST) @@@@@@@@@@@@@@@@@@@@
	
	'% insValMS105: Valida los datos introducidos en el grid
	'-------------------------------------------------------------
	Public Function insValMS105(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nZip_Code As Integer = 0, Optional ByVal nLocal As Integer = 0, Optional ByVal nOffice As Integer = 0, Optional ByVal nAuto_zone As Integer = 0, Optional ByVal nOrder As Integer = 0) As String
		'-------------------------------------------------------------
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsvalField As eFunctions.valField
		Dim lclsZip_code As eGeneralForm.Zip_code
		
		On Error GoTo insValMS105_Err
		
		lclsErrors = New eFunctions.Errors
		lclsvalField = New eFunctions.valField
		
		'+ Validación del campo "Zona Postal"
		If (nZip_Code = numNull Or nZip_Code = 0) And sAction = "Add" Then
			Call lclsErrors.ErrorMessage(sCodispl, 1037)
		End If
		
		'+ Validación del campo "Localidad"
		If (nLocal = numNull Or nLocal = 0) And sAction = "Add" Then
			Call lclsErrors.ErrorMessage(sCodispl, 1907)
		End If
		
		'+ No se puede repetir la combinación (Zona Postal - Localidad) en la Base de Datos
		If nZip_Code <> numNull And nZip_Code <> 0 And nLocal <> numNull And nLocal <> 0 Then
			lclsZip_code = New eGeneralForm.Zip_code
			
			If sAction = "Add" And lclsZip_code.Find(nZip_Code, nLocal) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10830)
			Else
				
				'+ Validación del campo "Oficina"
				If nOffice = numNull Or nOffice = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 10831)
				End If
				
				'+ Validación del campo "Orden de importancia"
				If nOrder = numNull Or nOrder = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 10832)
				End If
				
				'+ Validación del campo "Zona de Circulación"
				If nAuto_zone = numNull Or nAuto_zone = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 10833)
				End If
			End If
			'UPGRADE_NOTE: Object lclsZip_code may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsZip_code = Nothing
		Else
			
			'+ No debe dejar escribir si la clave está vacía
			
			If (nOffice <> numNull And nOffice <> 0) Or (nAuto_zone <> numNull And nAuto_zone <> 0) Or (nOrder <> numNull And nOrder <> 0) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1084)
			End If
		End If
		
		insValMS105 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalField = Nothing
		
insValMS105_Err: 
		If Err.Number Then
			insValMS105 = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% insPostMS105: Crea/actualiza los registros correspondientes en la tabla de Int_fixval
	Public Function insPostMS105(ByVal sAction As String, Optional ByVal nZip_Code As Integer = 0, Optional ByVal nLocal As Integer = 0, Optional ByVal nOffice As Integer = 0, Optional ByVal nAuto_zone As Integer = 0, Optional ByVal nOrder As Integer = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		
		On Error GoTo insPostMS105_Err
		
		Me.nZip_Code = nZip_Code
		Me.nLocal = nLocal
		Me.nOffice = nOffice
		Me.nAuto_zone = nAuto_zone
		Me.nOrder = nOrder
		Me.nUsercode = nUsercode
		
		Select Case sAction
			
			'+ Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMS105 = Add()
				
				'+ Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMS105 = Update()
				
				'+ Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMS105 = Delete()
		End Select
		
insPostMS105_Err: 
		If Err.Number Then
			insPostMS105 = False
		End If
		On Error GoTo 0
	End Function
	
	'@@@@@@@@@@@@@@@ RUTINAS NECESARIAS PARA LA EJECUCIÓN DE @@@@@@@@@@@@@@@
	'@@@@@@@@@@@@@@@ DE LAS FUNCIONES VAL Y POST             @@@@@@@@@@@@@@@
End Class







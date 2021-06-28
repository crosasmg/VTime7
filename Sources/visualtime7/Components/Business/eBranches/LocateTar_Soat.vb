Option Strict Off
Option Explicit On
Public Class LocateTar_Soat
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'LocateTar_Soat' in the system 04/01/2005 11:42:26 AM
	'+Objetivo: Propiedades según la tabla 'LocateTar_Soat' en el sistema 04/01/2005 11:42:26 AM
	
	'+Objetivo: Fecha de efecto del registro
	Public dEffecdate As Date
	
	'+Objetivo: código de la localidad
	Public nLocal_Type As Double
	
	'+Objetivo: código inicial de la localidad
	Public nZipCode_Ini As Double
	
	'+Objetivo: código final de la localidad
	Public nZipCode_End As Double
	
	'+Objetivo: Descripción de la localidad
	Public sDescript As String
	
	'+Objetivo: Fecha de la anulación del registro
	Public dNulldate As Date
	
	'+Objetivo: verifica si la localidad posee modificaciones posteriores a la fecha de efecto
	Public bEditRecord As Boolean
	
	'%Objetivo: Agrega un registro a la tabla "LocateTar_Soat"
	'%Parámetros:
	'%    nUsercode - Código del usuario
	'%    dEffecDate - Fecha de efecto del registro
	'%    nLocal_Type - código de la localidad
	'%    nZipCode_Ini - código inicial de la localidad
	'%    nZipCode_End - código final de la localidad
	Private Function Add(ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nLocal_Type As Double, ByVal nZipCode_Ini As Double, ByVal nZipCode_End As Double, ByVal sAction As String) As Boolean
		Dim lclsLocateTar_Soat As eRemoteDB.Execute
		
        On Error GoTo ErrorHandler
		
		lclsLocateTar_Soat = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.creLocateTar_Soat'. Generated on 04/01/2005 11:42:26 AM
		
		With lclsLocateTar_Soat
			.StoredProcedure = "insupdLocateTar_Soat"
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLocat_Type", nLocal_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nZipCode_Ini", nZipCode_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nZipCode_End", nZipCode_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		lclsLocateTar_Soat = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            Add = False
        End If
	End Function
	
	'%Objetivo: Actualiza un registro a la tabla "LocateTar_Soat" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nUsercode - Código del usuario
	'%    dEffecDate - Fecha de efecto del registro
	'%    nLocal_Type - código de la localidad
	'%    nZipCode_Ini - código inicial de la localidad
	'%    nZipCode_End - código final de la localidad
	Private Function Update(ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nLocal_Type As Double, ByVal nZipCode_Ini As Double, ByVal nZipCode_End As Double, ByVal sAction As String) As Boolean
		Dim lclsLocateTar_Soat As eRemoteDB.Execute
		
        On Error GoTo ErrorHandler
		
		lclsLocateTar_Soat = New eRemoteDB.Execute		
        With lclsLocateTar_Soat
            .StoredProcedure = "insupdLocateTar_Soat"
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NLOCAT_TYPE", nLocal_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nZipCode_Ini", nZipCode_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nZipCode_End", nZipCode_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With
		lclsLocateTar_Soat = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            Update = False
        End If

	End Function
	
	'%Objetivo: Elimina un registro a la tabla "LocateTar_Soat" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nUsercode - Código del usuario
	'%    dEffecDate - Fecha de efecto del registro
	'%    nLocal_Type - código de la localidad
	'%    nZipCode_Ini - código inicial de la localidad
	'%    nZipCode_End - código final de la localidad
	'%    sAction - Acción que se va realizar
	Private Function Delete(ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nLocal_Type As Double, ByVal nZipCode_Ini As Double, ByVal nZipCode_End As Double, ByVal sAction As String) As Boolean
		Dim lclsLocateTar_Soat As eRemoteDB.Execute
		
        On Error GoTo ErrorHandler
		
		lclsLocateTar_Soat = New eRemoteDB.Execute		
        With lclsLocateTar_Soat
            .StoredProcedure = "insupdLocateTar_Soat"
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NLOCAT_TYPE", nLocal_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nZipCode_Ini", nZipCode_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nZipCode_End", nZipCode_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With
		lclsLocateTar_Soat = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            Delete = False
        End If
	End Function
	
	'%Objetivo: Verifica la existencia de un registro en la tabla "LocateTar_Soat" usando la clave de dicha tabla.
	'%Parámetros:
	'%    dEffecDate - Fecha de efecto del registro
	'%    nLocal_Type - código de la localidad
	'%    nZipCode_Ini - código inicial de la localidad
	Private Function IsExist(ByVal dEffecdate As Date, ByVal nLocal_Type As Double, ByVal nZipCode_Ini As Double) As Boolean
		Dim lclsLocateTar_Soat As eRemoteDB.Execute
		Dim lintExist As Short
		
        On Error GoTo ErrorHandler
		
		lclsLocateTar_Soat = New eRemoteDB.Execute
		lintExist = 0		
        With lclsLocateTar_Soat
            .StoredProcedure = "reaLocateTar_Soat_v"
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NLOCAT_TYPE", nLocal_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nZipCode_Ini", nZipCode_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With
		lclsLocateTar_Soat = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            IsExist = False
        End If
	End Function
	
	'%Objetivo: Validación de los datos para la página del encabezado.
	'%Parámetros:
	'%    sCodispl   - Código de la transacción
	'%    nMainAction   - Código de la acción a realizar
	'%    sAction   - Acción que se va ha realizar
	'%    dEffecDate - Fecha de efecto del registro
	Public Function InsValMSO6000_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
        On Error GoTo ErrorHandler
		
		lclsErrors = New eFunctions.Errors
		
		'+ Se valida que la fecha de efcto no este vacía
		If dEffecdate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 4003)
        Else
            '+ Se valida que si la acción es diferente de consulta la fecha de efecto debe ser mayor a la fecha del computador
            If nMainAction <> eFunctions.Menues.TypeActions.clngActionQuery And dEffecdate <= Today Then
                Call lclsErrors.ErrorMessage(sCodispl, 10868)
            End If
        End If
		
        '+ Verifica si hay registro que no se pueden modificar
		If nMainAction = 302 Then
			If Me.Find(dEffecdate) Then
                Call lclsErrors.ErrorMessage(sCodispl, 91001, , , , , "(" & Me.dNulldate & ")")
			End If
		End If
		
		InsValMSO6000_k = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            InsValMSO6000_k = InsValMSO6000_k & Err.Description
        End If
	End Function
	
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl   - Código de la transacción
	'%    nMainAction   -
	'%    sAction   -
	'%    dEffecDate - Fecha de efecto del registro
	'%    nLocal_Type - código de la localidad
	'%    nZipCode_Ini - código inicial de la localidad
	'%    nZipCode_End - código final de la localidad
	Public Function InsValMSO6000(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal dEffecdate As Date, ByVal nLocal_Type As Double, ByVal nZipCode_Ini As Double, ByVal nZipCode_End As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTypeLocation As TypeLocation
		
        On Error GoTo ErrorHandler
		
		lclsErrors = New eFunctions.Errors
		lclsTypeLocation = New TypeLocation
		
		'+ Se valida que el campo localidad este lleno
		If sAction = "Update" Or sAction = "Add" Then
			If (nLocal_Type = 0 Or nLocal_Type = eRemoteDB.Constants.intNull) Then
				Call lclsErrors.ErrorMessage(sCodispl, 80007)
			End If
			
			'+ Se valida que código de inicio no debe ser mayor a código final
			If nZipCode_Ini <> eRemoteDB.Constants.intNull Then
				If nZipCode_Ini >= nZipCode_End Then
					Call lclsErrors.ErrorMessage(sCodispl, 10184)
				End If
			Else
				Call lclsErrors.ErrorMessage(sCodispl, 76036)
			End If
			
			'+ Se valida que el código inicial no este incluido en otro rango
			If IsExistRango(dEffecdate, nZipCode_End, nZipCode_Ini, nLocal_Type) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10185)
			End If
			
			'+ Se valida la existencia de la localidad en la tabla TYPELOCATION
			If sAction = "Add" Then
				If Not lclsTypeLocation.IsExist(nLocal_Type) Then
					Call lclsErrors.ErrorMessage(sCodispl, 90216)
				End If
				
				'+ Se valida que no existan dos registros validos con el mismo código inicial
				If (nLocal_Type <> 0 Or nLocal_Type <> eRemoteDB.Constants.intNull) And IsExist(dEffecdate, nLocal_Type, nZipCode_Ini) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 91000)
				End If
			End If
			
		End If
		
		InsValMSO6000 = lclsErrors.Confirm
		lclsErrors = Nothing
		lclsTypeLocation = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            InsValMSO6000 = InsValMSO6000 & Err.Description
        End If
	End Function
	
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%    pblnHeader -
	'%    sCodispl  - Código de la transacción
	'%    nMainAction - Núemro de la acción a realizar
	'%    sAction   -
	'%    nUsercode  -Código del usuario
	'%    dEffecDate - Fecha de efecto del registro
	'%    nLocal_Type - código de la localidad
	'%    nZipCode_Ini - código inicial de la localidad
	'%    nZipCode_End - código final de la localidad
	Public Function InsPostMSO6000(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nLocal_Type As Double, ByVal nZipCode_Ini As Double, ByVal nZipCode_End As Double) As Boolean
        On Error GoTo ErrorHandler
		
		If pblnHeader Then
			InsPostMSO6000 = True
		Else
			If sAction = "Add" Then
				InsPostMSO6000 = Add(nUsercode, dEffecdate, nLocal_Type, nZipCode_Ini, nZipCode_End, CStr(1))
			ElseIf sAction = "Update" Then 
				InsPostMSO6000 = Update(nUsercode, dEffecdate, nLocal_Type, nZipCode_Ini, nZipCode_End, CStr(2))
			ElseIf sAction = "Del" Then 
				InsPostMSO6000 = Delete(nUsercode, dEffecdate, nLocal_Type, nZipCode_Ini, nZipCode_End, CStr(3))
			End If
		End If
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            InsPostMSO6000 = False
        End If
	End Function
	
	'%Objetivo: Verifica la existencia de un registro en la tabla "LocateTar_Soat" usando la clave de dicha tabla.
	'%Parámetros:
	'%    dEffecDate - Fecha de efecto del registro
	'%    nLocal_Type - código de la localidad
	'%    nZipCode_Ini - código inicial de la localidad
	'%    nZipCode_End - código final de la localidad
	Private Function IsExistRango(ByVal dEffecdate As Date, ByVal nZipCode_End As Double, ByVal nZipCode_Ini As Double, ByVal nLocal_Type As Double) As Boolean
		Dim lclsLocateTar_Soat As eRemoteDB.Execute
		Dim lintExist As Short
		
        On Error GoTo ErrorHandler
		
		lclsLocateTar_Soat = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valLocateTar_SoatExist'. Generated on 04/01/2005 11:42:26 AM
		With lclsLocateTar_Soat
			.StoredProcedure = "reaLocateTar_Soat_v2"
			.Parameters.Add("nLocat_Type", nLocal_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nZipCode_Ini", nZipCode_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nZipCode_End", nZipCode_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExistRango = (.Parameters("nExist").Value = 1)
			Else
				IsExistRango = False
			End If
		End With
		lclsLocateTar_Soat = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            IsExistRango = False
        End If
	End Function
	
	'%Objetivo: Función que realiza la busqueda en la tabla 'LocateTar_Soat'.
	'%Parámetros:
	'%    dEffecDate - Fecha de efecto del registro
	Public Function Find(ByVal dEffecdate As Date) As Boolean
		Dim lclsLocateTar_Soat As eRemoteDB.Execute
		
        On Error GoTo ErrorHandler
		
		lclsLocateTar_Soat = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reatab_quotint'. Generated on 12/13/2004 1:38:43 PM
		With lclsLocateTar_Soat
			.StoredProcedure = "reaLocateTar_Soat_a"
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					Me.dNulldate = .FieldToClass("dNullDate")
					If Me.dNulldate <> dtmNull Then
						Find = True
						Exit Do
					Else
						Find = False
					End If
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		lclsLocateTar_Soat = Nothing
		
		Exit Function
ErrorHandler: 
		lclsLocateTar_Soat = Nothing
        If Err.Number Then
            Find = False
        End If
	End Function
End Class







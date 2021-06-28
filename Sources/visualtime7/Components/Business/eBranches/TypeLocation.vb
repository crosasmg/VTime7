Option Strict Off
Option Explicit On
Public Class TypeLocation
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 3 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 3 $
	
	'**+Objective: Properties according to the table 'TypeLocation' in the system 20/04/2005 05:32:20 p.m.
	'+Objetivo: Propiedades según la tabla 'TypeLocation' en el sistema 20/04/2005 05:32:20 p.m.
	
	'+Objetivo: Código de la localidad para la tarifa de SOAT
	Public nLocat_Type As Double
	
	'+Objetivo: Descripción de la localidad
	Public sDescript As String
	
	'+Objetivo: Descripción corta de la localidad
	Public sShort_des As String
	
	'+Objetivo: Código del origen del registro
	Public nLocal_Source As Short
	
	'+Objetivo: Estado general del registro.
	Public sStatRegt As String
	
	'%Objetivo: Agrega un registro a la tabla "TypeLocation"
	'%Parámetros:
	'%    nUsercode - Código de usuario.
	'%    nLocat_Type - Código de la localidad para la tarifa de SOAT.
	'%    sDescript - Descripción de la localidad.
	'%    sShort_des - Descripción corta de la localidad.
	'%    nLocal_Source - Código del origen del registro.
	'%    sStatRegt - Estado general del registro.
	Private Function Add(ByVal nUsercode As Integer, ByVal nLocat_Type As Double, ByVal sDescript As String, ByVal sShort_des As String, ByVal nLocal_Source As Short, ByVal sStatRegt As String) As Boolean
		Dim lclsTypeLocation As eRemoteDB.Execute
		
        On Error GoTo ErrorHandler
		
		lclsTypeLocation = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.creTypeLocation'. Generated on 20/04/2005 05:32:20 p.m.
		
		With lclsTypeLocation
			.StoredProcedure = "creTypeLocation"
			.Parameters.Add("nLocat_Type", nLocat_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLocal_Source", nLocal_Source, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatRegt", sStatRegt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		lclsTypeLocation = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            Add = False
        End If

	End Function
	
	'%Objetivo: Actualiza un registro a la tabla "TypeLocation" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nUsercode - Código de usuario.
	'%    nLocat_Type - Código de la localidad para la tarifa de SOAT.
	'%    sDescript - Descripción de la localidad.
	'%    sShort_des - Descripción corta de la localidad.
	'%    nLocal_Source - Código del origen del registro.
	'%    sStatRegt - Estado general del registro.
	Private Function Update(ByVal nUsercode As Integer, ByVal nLocat_Type As Double, ByVal sDescript As String, ByVal sShort_des As String, ByVal nLocal_Source As Short, ByVal sStatRegt As String) As Boolean
		Dim lclsTypeLocation As eRemoteDB.Execute
		
        On Error GoTo ErrorHandler
		
		lclsTypeLocation = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updTypeLocation'. Generated on 20/04/2005 05:32:20 p.m.
		With lclsTypeLocation
			.StoredProcedure = "updTypeLocation"
			.Parameters.Add("nLocat_Type", nLocat_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLocal_Source", nLocal_Source, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatRegt", sStatRegt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		lclsTypeLocation = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            Update = False
        End If

	End Function
	
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl - Código de la transacción.
	'%    nMainAction - Código de la acción a realizar.
	'%    sAction - Acción a realizar.
	'%    nUsercode - Código de usuario.
	'%    nLocat_Type - Código de la localidad para la tarifa de SOAT.
	'%    sDescript - Descripción de la localidad.
	'%    sShort_des - Descripción corta de la localidad.
	'%    nLocal_Source - Código del origen del registro.
	Public Function InsValMSO6005(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nLocat_Type As Double, ByVal sDescript As String, ByVal sShort_des As String, ByVal nLocal_Source As Short) As String
		Dim lclsErrors As eFunctions.Errors
		
        On Error GoTo ErrorHandler
		
		lclsErrors = New eFunctions.Errors
		
		'+ Se valida que el tipo de la localidad no este registrada
		If sAction = "Add" Then
			If IsExist(nLocat_Type) Then
				Call lclsErrors.ErrorMessage(sCodispl, 90206)
			End If
		End If
		
		'+ Si no se esta consultando el campo tipo de localidad debe estear lleno
		If (sAction = "Add" Or sAction = "Update") And (nLocat_Type = 0 Or nLocat_Type = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 80007)
		End If
		
		'+ Si no se esta consultando el campo descripción debe estear lleno
		If (sAction = "Add" Or sAction = "Update") And sDescript = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 10857)
		End If
		
		'+ Si no se esta consultando el campo descripción corta debe estear lleno
		If (sAction = "Add" Or sAction = "Update") And sShort_des = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 10858)
		End If
		
		'+ Si no se esta consultando el campo origen debe estear lleno
		If (sAction = "Add" Or sAction = "Update") And (nLocal_Source = 0 Or nLocal_Source = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 55676)
		End If
		
		InsValMSO6005 = lclsErrors.Confirm
		lclsErrors = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            InsValMSO6005 = InsValMSO6005 & Err.Description
        End If

	End Function
	
	'%Objetivo: Verifica la existencia de un registro en la tabla "TypeLocation" usando la clave de dicha tabla.
	'%Parámetros:
	'%    nLocat_Type - Código de la localidad para la tarifa de SOAT.
	Public Function IsExist(ByVal nLocal_Type As Double) As Boolean
		Dim lclsTypeLocation As eRemoteDB.Execute
		Dim lintExist As Short
		
        On Error GoTo ErrorHandler
		
		lclsTypeLocation = New eRemoteDB.Execute
		lintExist = 0		
        With lclsTypeLocation
            .StoredProcedure = "reaTypeLocation_v"
            .Parameters.Add("NLOCAT_TYPE", nLocal_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With
		lclsTypeLocation = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            IsExist = False
        End If
	End Function
	
	
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%    pblnHeader - Indicador de la cabecera.
	'%    sCodispl - Código de la transacción.
	'%    nMainAction - Código de la acción a realizar.
	'%    sAction - Acción a realizar.
	'%    nUsercode - Código de usuario.
	'%    nLocat_Type - Código de la localidad para la tarifa de SOAT.
	'%    sDescript - Descripción de la localidad.
	'%    sShort_des - Descripción corta de la localidad.
	'%    nLocal_Source - Código del origen del registro.
	'%    sStatRegt - Estado general del registro.
	Public Function InsPostMSO6005(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nLocat_Type As Double, ByVal sDescript As String, ByVal sShort_des As String, ByVal nLocal_Source As Short, ByVal sStatRegt As String) As Boolean
        On Error GoTo ErrorHandler
		
		If pblnHeader Then
			InsPostMSO6005 = True
		Else
			If sAction = "Add" Then
				InsPostMSO6005 = Add(nUsercode, nLocat_Type, sDescript, sShort_des, nLocal_Source, sStatRegt)
			ElseIf sAction = "Update" Then 
				InsPostMSO6005 = Update(nUsercode, nLocat_Type, sDescript, sShort_des, nLocal_Source, sStatRegt)
			End If
		End If
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            InsPostMSO6005 = False
        End If
	End Function
End Class







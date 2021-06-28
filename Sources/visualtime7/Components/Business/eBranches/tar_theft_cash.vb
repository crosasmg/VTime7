Option Strict Off
Option Explicit On
Public Class tar_theft_cash
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'tar_theft_cash' in the system 28/06/2004 03:44:23 p.m.
	'+Objetivo: Propiedades según la tabla 'tar_theft_cash' en el sistema 28/06/2004 03:44:23 p.m.
	Public nTar_theft As Short
	Public dEffecDate As Date
	Public nUbication As Short
	Public nRate As Double
	
	
	
	'**%Objective: Add a record to the table "tar_theft_cash"
	'**%Parameters:
	'**%   nUsercode  -  The user code
	'**%   nTar_theft -  Code of the theft tariff
	'**%   dEffecdate -  Date which from the record is valid.
	'**%   nUbication -  Risk location
	'**%   nRate      -  Rate (0/00) to be applied to a sum insured in order to obtain the premium of a cover
	'%Objetivo: Agrega un registro a la tabla "tar_theft_cash"
	'%Parámetros:
	'%    nUsercode   - Usuario que esta procesando la tabla
	'%    nTar_theft  - Código de la tarifa de robo
	'%    dEffecdate  - Fecha de efecto del registro
	'%    nUbication  - Ubicacion del riesgo
	'%    nRate       - Porcentaje a aplicar
	Private Function Add(ByVal nUsercode As Integer, ByVal nTar_theft As Short, ByVal dEffecDate As Date, ByVal nUbication As Short, ByVal nRate As Double) As Boolean
		Dim lclstar_theft_cash As eRemoteDB.Execute
		
        lclstar_theft_cash = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.cretar_theft_cash'. Generated on 28/06/2004 03:44:23 p.m.
		
		With lclstar_theft_cash
			.StoredProcedure = "cretar_theft_cash"
			.Parameters.Add("nTar_Theft", nTar_theft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUbication", nUbication, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		lclstar_theft_cash = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Updates a registry to the table "tar_theft_cash" using the key for this table.
	'**%Parameters:
	'**%   nUsercode  -  The user code
	'**%   nTar_theft -  Code of the theft tariff
	'**%   dEffecdate -  Date which from the record is valid.
	'**%   nUbication -  Risk location
	'**%   nRate      -  Rate (0/00) to be applied to a sum insured in order to obtain the premium of a cover
	'%Objetivo: Actualiza un registro a la tabla "tar_theft_cash" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nUsercode   - Usuario que esta procesando la tabla
	'%    nTar_theft  - Código de la tarifa de robo
	'%    dEffecdate  - Fecha de efecto del registro
	'%    nUbication  - Ubicacion del riesgo
	'%    nRate       - Porcentaje a aplicar
	Private Function Update(ByVal nUsercode As Integer, ByVal nTar_theft As Short, ByVal dEffecDate As Date, ByVal nUbication As Short, ByVal nRate As Double) As Boolean
		Dim lclstar_theft_cash As eRemoteDB.Execute
		
        lclstar_theft_cash = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updtar_theft_cash'. Generated on 28/06/2004 03:44:23 p.m.
		With lclstar_theft_cash
			.StoredProcedure = "insupdtar_theft_cash"
			.Parameters.Add("nTar_Theft", nTar_theft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUbication", nUbication, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		lclstar_theft_cash = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Delete a registry the table "tar_theft_cash" using the key for this table.
	'**%Parameters:
	'**%   nTar_theft -  Code of the theft tariff
	'**%   dEffecdate -  Date which from the record is valid.
	'**%   nUbication -  Risk location
	'**%   nUsercode  -  The user code
	'%Objetivo: Elimina un registro a la tabla "tar_theft_cash" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nTar_theft  - Código de la tarifa de robo
	'%    dEffecdate  - Fecha de efecto del registro
	'%    nUbication  - Ubicacion del riesgo
	'%    nUsercode   - Usuario que esta procesando la tabla
	Private Function Delete(ByVal nTar_theft As Short, ByVal dEffecDate As Date, ByVal nUbication As Short, ByVal nUsercode As Integer) As Boolean
		Dim lclstar_theft_cash As eRemoteDB.Execute
		
        lclstar_theft_cash = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.deltar_theft_cash'. Generated on 28/06/2004 03:44:23 p.m.
		With lclstar_theft_cash
			.StoredProcedure = "deltar_theft_cash"
			.Parameters.Add("nTar_Theft", nTar_theft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUbication", nUbication, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		lclstar_theft_cash = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: It verifies the existence of a registry in table "tar_theft_cash" using the key of this table.
	'**%Parameters:
	'**%   nTar_theft -  Code of the theft tariff
	'**%   dEffecdate -  Date which from the record is valid.
	'**%   nUbication -  Risk location
	'%Objetivo: Verifica la existencia de un registro en la tabla "tar_theft_cash" usando la clave de dicha tabla.
	'%Parámetros:
	'%    nTar_theft  - Código de la tarifa de robo
	'%    dEffecdate  - Fecha de efecto del registro
	'%    nUbication  - Ubicacion del riesgo
	Private Function IsExist(ByVal nTar_theft As Short, ByVal dEffecDate As Date, ByVal nUbication As Short) As Boolean
		Dim lclstar_theft_cash As eRemoteDB.Execute
		Dim lintExist As Short
		
		lclstar_theft_cash = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valtar_theft_cashExist'. Generated on 28/06/2004 03:44:23 p.m.
		With lclstar_theft_cash
			.StoredProcedure = "reatar_theft_cash_v"
			.Parameters.Add("nTar_Theft", nTar_theft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUbication", nUbication, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		
		lclstar_theft_cash = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validation of the data for the page of the headed one.
	'**%Parameters:
	'**%    sCodispl    - Code of the page.
	'**%    nMaisAction - Action of the Menu.
	'**%    sAction     - Action of grid.
	'**%   nTar_theft -  Code of the theft tariff
	'**%   dEffecdate -  Date which from the record is valid.
	'%Objetivo: Validación de los datos para la página del encabezado.
	'%Parámetros:
	'%    sCodispl    - Código de la página
	'%    nMaisAction - Acción del Menu.
	'%    sAction     - Acción del grid.
	'%    nTar_theft  - Código de la tarifa de robo
	'%    dEffecdate  - Fecha de efecto del registro
	Public Function InsValMRO003_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nTar_theft As Short, ByVal dEffecDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
        lclsErrors = New eFunctions.Errors
		

		If (nTar_theft = 0 Or nTar_theft = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10146)
		End If
		If dEffecDate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 11198)
        Else
            If nMainAction = 302 Then
                If (nTar_theft <> eRemoteDB.Constants.intNull) AndAlso _
                   (IsExist(nTar_theft, dEffecDate, eRemoteDB.Constants.intNull)) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10869)
                End If

                If dEffecDate <= Today Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10868)
                End If
            End If
        End If
        InsValMRO003_k = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%   sCodispl    - Code of the page.
	'**%   nMaisAction - Action of the Menu.
	'**%   sAction     - Action of grid.
	'**%   nTar_theft  -  Code of the theft tariff
	'**%   dEffecdate  -  Date which from the record is valid.
	'**%   nUbication -  Risk location
	'**%   nRate      -  Rate (0/00) to be applied to a sum insured in order to obtain the premium of a cover
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl    - Código de la página
	'%    nMaisAction - Acción del Menu.
	'%    sAction     - Acción del grid.
	'%    nTar_theft  - Código de la tarifa de robo
	'%    dEffecdate  - Fecha de efecto del registro
	'%    nUbication  - Ubicacion del riesgo
	'%    nRate       - Porcentaje a aplicar
	Public Function InsValMRO003(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nTar_theft As Short, ByVal dEffecDate As Date, ByVal nUbication As Short, ByVal nRate As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
        lclsErrors = New eFunctions.Errors
		
		If (nUbication = 0 Or nUbication = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 3483)
		End If
		If sAction = "Add" And IsExist(nTar_theft, dEffecDate, nUbication) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10892)
		End If
		If (nRate = 0 Or nRate = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10121)
		End If
		
		InsValMRO003 = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'**%Parameters:
	'**%   nHeader     - Indicator of the page (headed or it details)
	'**%   sCodispl    - Code of the page.
	'**%   nMaisAction - Action of the Menu.
	'**%   sAction     - Action of grid.
	'**%   nUsercode   -  The user code
	'**%   nTar_theft  -  Code of the theft tariff
	'**%   dEffecdate  -  Date which from the record is valid.
	'**%   nUbication  -  Risk location
	'**%   nRate       -  Rate (0/00) to be applied to a sum insured in order to obtain the premium of a cover
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%    nHeader     - Indicador de la página (encabezado o detalle).
	'%    sCodispl    - Código de la página
	'%    nMaisAction - Acción del Menu.
	'%    sAction     - Acción del grid.
	'%    nUsercode   - Usuario que esta procesando la tabla
	'%    nTar_theft  - Código de la tarifa de robo
	'%    dEffecdate  - Fecha de efecto del registro
	'%    nUbication  - Ubicacion del riesgo
	'%    nRate       - Porcentaje a aplicar
	Public Function InsPostMRO003(ByVal nHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nTar_theft As Short, ByVal dEffecDate As Date, ByVal nUbication As Short, ByVal nRate As Double) As Boolean

		If nHeader Then
			InsPostMRO003 = True
		Else
			If sAction = "Add" Then
				InsPostMRO003 = Add(nUsercode, nTar_theft, dEffecDate, nUbication, nRate)
			ElseIf sAction = "Update" Then 
				InsPostMRO003 = Update(nUsercode, nTar_theft, dEffecDate, nUbication, nRate)
			ElseIf sAction = "Del" Then 
				InsPostMRO003 = Delete(nTar_theft, dEffecDate, nUbication, nUsercode)
			End If
		End If
		
		Exit Function
	End Function
End Class












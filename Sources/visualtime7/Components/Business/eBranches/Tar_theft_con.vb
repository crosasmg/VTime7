Option Strict Off
Option Explicit On
Public Class Tar_theft_con
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'Tar_theft_con' in the system 6/28/2004 10:33:03 AM
	'+Objetivo: Propiedades según la tabla 'Tar_theft_con' en el sistema 6/28/2004 10:33:03 AM
	Public nTar_theft As Short
	Public dEffecDate As Date
	Public nInsured As Short
	Public nRiskClass As Short
	Public nUbication As Short
	Public nRate As Double
	
	
	
	'**%Objective: Add a record to the table "Tar_theft_con"
	'**%Parameters:
	'**%    nUsercode   - the user code
	'**%    nTar_theft  - code of the theft tariff
	'**%    dEffecdate  - date which from the record is valid.
	'**%    nInsured    - insured risk percentage
	'**%    nRiskClass  - risk class
	'**%    nUbication  - risk location
	'**%    nRate       - rate (0/00) to be applied to a sum insured in order to obtain the premium
	'%Objetivo: Agrega un registro a la tabla "Tar_theft_con"
	'%Parámetros:
	'%    nUsercode    - Código del usuario
	'%    nTar_theft   - Código de la tarifa de robo
	'%    dEffecdate   - Fecha de efecto del registro.
	'%    nInsured     - Porcentaje asegurado del riesgo al que le corresponde la tarifa
	'%    nRiskClass   - Clasificación del riego
	'%    nUbication   - Ubicación del riesgo.
	'%    nRate        - Pormilaje a aplicar a un capital para obtener la prima de la cobertura
	Private Function Add(ByVal nUsercode As Integer, ByVal nTar_theft As Short, ByVal dEffecDate As Date, ByVal nInsured As Short, ByVal nRiskClass As Short, ByVal nUbication As Short, ByVal nRate As Double) As Boolean
		Dim lclsTar_theft_con As eRemoteDB.Execute
		
        lclsTar_theft_con = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.creTar_theft_con'. Generated on 6/28/2004 10:33:03 AM
		
		With lclsTar_theft_con
			.StoredProcedure = "creTar_theft_con"
			.Parameters.Add("nTar_theft", nTar_theft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsured", nInsured, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRiskClass", nRiskClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUbication", nUbication, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		lclsTar_theft_con = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Updates a registry to the table "Tar_theft_con" using the key for this table.
	'**%Parameters:
	'**%    nUsercode   - the user code
	'**%    nTar_theft  - code of the theft tariff
	'**%    dEffecdate  - date which from the record is valid.
	'**%    nInsured    - insured risk percentage
	'**%    nRiskClass  - risk class
	'**%    nUbication  - risk location
	'**%    nRate       - rate (0/00) to be applied to a sum insured in order to obtain the premium
	'%Objetivo: Actualiza un registro a la tabla "Tar_theft_con" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nUsercode    - Código del usuario
	'%    nTar_theft   - Código de la tarifa de robo
	'%    dEffecdate   - Fecha de efecto del registro.
	'%    nInsured     - Porcentaje asegurado del riesgo al que le corresponde la tarifa
	'%    nRiskClass   - Clasificación del riego
	'%    nUbication   - Ubicación del riesgo.
	'%    nRate        - Pormilaje a aplicar a un capital para obtener la prima de la cobertura
	Private Function Update(ByVal nUsercode As Integer, ByVal nTar_theft As Short, ByVal dEffecDate As Date, ByVal nInsured As Short, ByVal nRiskClass As Short, ByVal nUbication As Short, ByVal nRate As Double) As Boolean
		Dim lclsTar_theft_con As eRemoteDB.Execute
		
        lclsTar_theft_con = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updTar_theft_con'. Generated on 6/28/2004 10:33:03 AM
		With lclsTar_theft_con
			.StoredProcedure = "insupdTar_theft_con"
			.Parameters.Add("nTar_theft", nTar_theft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsured", nInsured, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRiskClass", nRiskClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUbication", nUbication, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		lclsTar_theft_con = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Delete a registry the table "Tar_theft_con" using the key for this table.
	'**%Parameters:
	'**%    nTar_theft  - code of the theft tariff
	'**%    dEffecdate  - date which from the record is valid.
	'**%    nInsured    - insured risk percentage
	'**%    nRiskClass  - risk class
	'**%    nUbication  - risk location
	'**%    nUsercode   - the user code
	'%Objetivo: Elimina un registro a la tabla "Tar_theft_con" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nTar_theft   - Código de la tarifa de robo
	'%    dEffecdate   - Fecha de efecto del registro.
	'%    nInsured     - Porcentaje asegurado del riesgo al que le corresponde la tarifa
	'%    nRiskClass   - Clasificación del riego
	'%    nUbication   - Ubicación del riesgo.
	'%    nUsercode    - Código del usuario
	Private Function Delete(ByVal nTar_theft As Short, ByVal dEffecDate As Date, ByVal nInsured As Short, ByVal nRiskClass As Short, ByVal nUbication As Short, ByVal nUsercode As Integer) As Boolean
		Dim lclsTar_theft_con As eRemoteDB.Execute
		
        lclsTar_theft_con = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.delTar_theft_con'. Generated on 6/28/2004 10:33:03 AM
		With lclsTar_theft_con
			.StoredProcedure = "delTar_theft_con"
			.Parameters.Add("nTar_theft", nTar_theft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsured", nInsured, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRiskClass", nRiskClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUbication", nUbication, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		lclsTar_theft_con = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: It verifies the existence of a registry in table "Tar_theft_con" using the key of this table.
	'**%Parameters:
	'**%    nTar_theft  - code of the theft tariff
	'**%    dEffecdate  - date which from the record is valid.
	'**%    nInsured    - insured risk percentage
	'**%    nRiskClass  - risk class
	'**%    nUbication  - risk location
	'%Objetivo: Verifica la existencia de un registro en la tabla "Tar_theft_con" usando la clave de dicha tabla.
	'%Parámetros:
	'%    nTar_theft   - Código de la tarifa de robo
	'%    dEffecdate   - Fecha de efecto del registro.
	'%    nInsured     - Porcentaje asegurado del riesgo al que le corresponde la tarifa
	'%    nRiskClass   - Clasificación del riego
	'%    nUbication   - Ubicación del riesgo.
	Private Function IsExist(ByVal nTar_theft As Short, ByVal dEffecDate As Date, ByVal nInsured As Short, ByVal nRiskClass As Short, ByVal nUbication As Short) As Boolean
		Dim lclsTar_theft_con As eRemoteDB.Execute
		Dim lintExist As Short
		
        lclsTar_theft_con = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valTar_theft_conExist'. Generated on 6/28/2004 10:33:03 AM
		With lclsTar_theft_con
			.StoredProcedure = "reaTar_theft_con_v"
			.Parameters.Add("nTar_theft", nTar_theft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsured", nInsured, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRiskClass", nRiskClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUbication", nUbication, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		
		lclsTar_theft_con = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validation of the data for the page of the headed one.
	'**%Parameters:
	'**%    sCodispl    - Code of the page.
	'**%    nMaisAction - Action of the Menu.
	'**%    sAction     - Action of grid.
	'**%    nTar_theft  - code of the theft tariff
	'**%    dEffecdate  - date which from the record is valid.
	'%Objetivo: Validación de los datos para la página del encabezado.
	'%Parámetros:
	'%    sCodispl    - Códigi de la página
	'%    nMaisAction - Acción del Menu.
	'%    sAction     - Acción del grid.
	'%    nTar_theft   - Código de la tarifa de robo
	'%    dEffecdate   - Fecha de efecto del registro.
	Public Function InsValMRO002_k(ByVal sCodispl As String, ByVal nMaisAction As Integer, ByVal sAction As String, ByVal nTar_theft As Short, ByVal dEffecDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
        lclsErrors = New eFunctions.Errors
		
		If (nTar_theft = 0 Or nTar_theft = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10146)
		End If
		If dEffecDate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 11198)
        Else
            If nMaisAction = 302 Then
                If (nTar_theft <> eRemoteDB.Constants.intNull) AndAlso _
                   (IsExist(nTar_theft, dEffecDate, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10869)
                End If

                If dEffecDate <= Today Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10868)
                End If
            End If
        End If
        InsValMRO002_k = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%    sCodispl    - Code of the page.
	'**%    nMaisAction - Action of the Menu.
	'**%    sAction     - Action of grid.
	'**%    nTar_theft  - code of the theft tariff
	'**%    dEffecdate  - date which from the record is valid.
	'**%    nInsured    - insured risk percentage
	'**%    nRiskClass  - risk class
	'**%    nUbication  - risk location
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl    - Códigi de la página
	'%    nMaisAction - Acción del Menu.
	'%    sAction     - Acción del grid.
	'%    nTar_theft   - Código de la tarifa de robo
	'%    dEffecdate   - Fecha de efecto del registro.
	'%    nInsured     - Porcentaje asegurado del riesgo al que le corresponde la tarifa
	'%    nRiskClass   - Clasificación del riego
	'%    nUbication   - Ubicación del riesgo.
	Public Function InsValMRO002(ByVal sCodispl As String, ByVal nMaisAction As Integer, ByVal sAction As String, ByVal nTar_theft As Short, ByVal dEffecDate As Date, ByVal nInsured As Short, ByVal nRiskClass As Short, ByVal nUbication As Short, ByVal nRate As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
        lclsErrors = New eFunctions.Errors

		If (nInsured = 0 Or nInsured = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 3509)
		End If
		If nInsured > 100 And (nInsured <> 0 Or nInsured = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 3510)
		End If
		If (nRiskClass = 0 Or nRiskClass = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 3507)
		End If
		If (nUbication = 0 Or nUbication = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 3483)
        End If
        If sAction = "Add" Then
            If (nInsured > 0) And _
               (nRiskClass > 0) And _
               (nUbication > 0) Then
                If IsExist(nTar_theft, dEffecDate, nInsured, nRiskClass, nUbication) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10893)
                End If
            End If
        End If
        If (nRate = 0 Or nRate = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 10121)
        End If

        InsValMRO002 = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
	End Function
	
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'**%Parameters:
	'**%    nHeader     - Indicator of the page (headed or it details)
	'**%    sCodispl    - Code of the page.
	'**%    nMaisAction - Action of the Menu.
	'**%    sAction     - Action of grid.
	'**%    nTar_theft  - code of the theft tariff
	'**%    dEffecdate  - date which from the record is valid.
	'**%    nInsured    - insured risk percentage
	'**%    nRiskClass  - risk class
	'**%    nUbication  - risk location
	'**%    nRate       - rate (0/00) to be applied to a sum insured in order to obtain the premium
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%    nHeader     - Indicador de la página (encabezado o detalle).
	'%    sCodispl    - Códigi de la página
	'%    nMaisAction - Acción del Menu.
	'%    sAction     - Acción del grid.
	'%    nTar_theft   - Código de la tarifa de robo
	'%    dEffecdate   - Fecha de efecto del registro.
	'%    nInsured     - Porcentaje asegurado del riesgo al que le corresponde la tarifa
	'%    nRiskClass   - Clasificación del riego
	'%    nUbication   - Ubicación del riesgo.
	'%    nRate        - Pormilaje a aplicar a un capital para obtener la prima de la cobertura
	Public Function InsPostMRO002(ByVal nHeader As Boolean, ByVal sCodispl As String, ByVal nMaisAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nTar_theft As Short, ByVal dEffecDate As Date, ByVal nInsured As Short, ByVal nRiskClass As Short, ByVal nUbication As Short, ByVal nRate As Double) As Boolean

		If nHeader Then
			InsPostMRO002 = True
		Else
			If sAction = "Add" Then
				InsPostMRO002 = Add(nUsercode, nTar_theft, dEffecDate, nInsured, nRiskClass, nUbication, nRate)
			ElseIf sAction = "Update" Then 
				InsPostMRO002 = Update(nUsercode, nTar_theft, dEffecDate, nInsured, nRiskClass, nUbication, nRate)
			ElseIf sAction = "Del" Then 
				InsPostMRO002 = Delete(nTar_theft, dEffecDate, nInsured, nRiskClass, nUbication, nUsercode)
			End If
		End If
		
		Exit Function
	End Function
End Class












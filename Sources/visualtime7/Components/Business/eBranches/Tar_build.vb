Option Strict Off
Option Explicit On
Public Class Tar_build
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'Tar_build' in the system 29/06/2004 11:58:05 a.m.
	'+Objetivo: Propiedades según la tabla 'Tar_build' en el sistema 29/06/2004 11:58:05 a.m.
	Public nBranch As Short
	Public nProduct As Short
	Public dEffecDate As Date
	Public nCategory As Short
	Public nExtraPrem As Double
	Public nDiscount As Double
	
	
	
	'**%Objective: Add a record to the table "Tar_build"
	'**%Parameters:
	'**%    nUsercode  - The user code
	'**%    nBranch    - Code of the line of business.
	'**%    nProduct   - Code of the product.
	'**%    dEffecDate - Date which from the record is valid.
	'**%    nCategory  - Risk category
	'**%    nExtraPrem - Percentage of extra-premium per construction category
	'**%    nDiscount  - Percentage of discount per construction category
	'%Objetivo: Agrega un registro a la tabla "Tar_build"
	'%Parámetros:
	'%    nUsercode  - Código del usuario
	'%    nBranch    - Código del ramo comercial.
	'%    nProduct   - Código del producto.
	'%    dEffecDate - Fecha de efecto del registro.
	'%    nCategory  - Categoria del riesgo
	'%    nExtraPrem - Porcentaje de recargo por construcción
	'%    nDiscount  - Porcentaje de descuento por construcción
	Private Function Add(ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date, ByVal nCategory As Short, ByVal nExtraPrem As Double, ByVal nDiscount As Double) As Boolean
		Dim lclsTar_build As eRemoteDB.Execute
		
        lclsTar_build = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.creTar_build'. Generated on 29/06/2004 11:58:05 a.m.
		
		With lclsTar_build
			.StoredProcedure = "creTar_build"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCategory", nCategory, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExtraPrem", nExtraPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDiscount", nDiscount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		lclsTar_build = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Updates a registry to the table "Tar_build" using the key for this table.
	'**%Parameters:
	'**%    nUsercode  - The user code
	'**%    nBranch    - Code of the line of business.
	'**%    nProduct   - Code of the product.
	'**%    dEffecDate - Date which from the record is valid.
	'**%    nCategory  - Risk category
	'**%    nExtraPrem - Percentage of extra-premium per construction category
	'**%    nDiscount  - Percentage of discount per construction category
	'%Objetivo: Actualiza un registro a la tabla "Tar_build" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nUsercode  - Código del usuario
	'%    nBranch    - Código del ramo comercial.
	'%    nProduct   - Código del producto.
	'%    dEffecDate - Fecha de efecto del registro.
	'%    nCategory  - Categoria del riesgo
	'%    nExtraPrem - Porcentaje de recargo por construcción
	'%    nDiscount  - Porcentaje de descuento por construcción
	Private Function Update(ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date, ByVal nCategory As Short, ByVal nExtraPrem As Double, ByVal nDiscount As Double) As Boolean
		Dim lclsTar_build As eRemoteDB.Execute
		
        lclsTar_build = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updTar_build'. Generated on 29/06/2004 11:58:05 a.m.
		With lclsTar_build
			.StoredProcedure = "insupdTar_build"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCategory", nCategory, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExtraPrem", nExtraPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDiscount", nDiscount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		lclsTar_build = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Delete a registry the table "Tar_build" using the key for this table.
	'**%Parameters:
	'**%    nBranch    - Code of the line of business.
	'**%    nProduct   - Code of the product.
	'**%    dEffecDate - Date which from the record is valid.
	'**%    nCategory  - Risk category
	'**%    nUsercode  - The user code
	'%Objetivo: Elimina un registro a la tabla "Tar_build" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nBranch    - Código del ramo comercial.
	'%    nProduct   - Código del producto.
	'%    dEffecDate - Fecha de efecto del registro.
	'%    nCategory  - Categoria del riesgo
	'%    nUsercode  - Código del usuario
	Private Function Delete(ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date, ByVal nCategory As Short, ByVal nUsercode As Integer) As Boolean
		Dim lclsTar_build As eRemoteDB.Execute
		
        lclsTar_build = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.delTar_build'. Generated on 29/06/2004 11:58:05 a.m.
		With lclsTar_build
			.StoredProcedure = "delTar_build"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCategory", nCategory, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		lclsTar_build = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: It verifies the existence of a registry in table "Tar_build" using the key of this table.
	'**%Parameters:
	'**%    nBranch    - Code of the line of business.
	'**%    nProduct   - Code of the product.
	'**%    dEffecDate - Date which from the record is valid.
	'**%    nCategory  - Risk category
	'%Objetivo: Verifica la existencia de un registro en la tabla "Tar_build" usando la clave de dicha tabla.
	'%Parámetros:
	'%    nBranch    - Código del ramo comercial.
	'%    nProduct   - Código del producto.
	'%    dEffecDate - Fecha de efecto del registro.
	'%    nCategory  - Categoria del riesgo
	Private Function IsExist(ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date, ByVal nCategory As Short) As Boolean
		Dim lclsTar_build As eRemoteDB.Execute
		Dim lintExist As Short
		
        lclsTar_build = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valTar_buildExist'. Generated on 29/06/2004 11:58:05 a.m.
		With lclsTar_build
			.StoredProcedure = "reaTar_build_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCategory", nCategory, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		
		lclsTar_build = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validation of the data for the page of the headed one.
	'**%Parameters:
	'**%    sCodispl    - Code of the page.
	'**%    nMaisAction - Action of the Menu.
	'**%    sAction     - Action of grid.
	'**%    nBranch     - Code of the line of business.
	'**%    nProduct    - Code of the product.
	'**%    dEffecDate  - Date which from the record is valid.
	'%Objetivo: Validación de los datos para la página del encabezado.
	'%Parámetros:
	'%    sCodispl    - Código de la página
	'%    nMaisAction - Acción del Menu.
	'%    sAction     - Acción del grid.
	'%    nBranch     - Código del ramo comercial.
	'%    nProduct    - Código del producto.
	'%    dEffecDate -  Fecha de efecto del registro.
	Public Function InsValMRO004_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
        lclsErrors = New eFunctions.Errors
		
		If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 1022)
		End If
		If (nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 11009)
		End If
		If dEffecDate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 2056)
		End If

        If nMainAction = 302 Then
            If (nBranch <> eRemoteDB.Constants.intNull) AndAlso _
               (IsExist(nBranch, nProduct, dEffecDate, eRemoteDB.Constants.intNull)) Then
                Call lclsErrors.ErrorMessage(sCodispl, 10869)
            End If

            If dEffecDate <= Today Then
                Call lclsErrors.ErrorMessage(sCodispl, 10868)
            End If
        End If

		
		InsValMRO004_k = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%    sCodispl    - Code of the page.
	'**%    nMaisAction - Action of the Menu.
	'**%    sAction     - Action of grid.
	'**%    nBranch     - Code of the line of business.
	'**%    nProduct    - Code of the product.
	'**%    dEffecDate  - Date which from the record is valid.
	'**%    nCategory  - Risk category
	'**%    nExtraPrem - Percentage of extra-premium per construction category
	'**%    nDiscount  - Percentage of discount per construction category
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl    - Código de la página
	'%    nMaisAction - Acción del Menu.
	'%    sAction     - Acción del grid.
	'%    nBranch     - Código del ramo comercial.
	'%    nProduct    - Código del producto.
	'%    dEffecDate  -  Fecha de efecto del registro.
	'%    nCategory   - Categoria del riesgo
	'%    nExtraPrem  - Porcentaje de recargo por construcción
	'%    nDiscount   - Porcentaje de descuento por construcción
	Public Function InsValMRO004(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date, ByVal nCategory As Short, ByVal nExtraPrem As Double, ByVal nDiscount As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
        lclsErrors = New eFunctions.Errors
		
		If (nCategory = 0 Or nCategory = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 3505)
		End If
        If nExtraPrem <> eRemoteDB.Constants.intNull And nDiscount <> eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 55685)
        End If
		If (nExtraPrem = 0 Or nExtraPrem = eRemoteDB.Constants.intNull) And (nDiscount = 0 Or nDiscount = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10169)
		End If
		If (nExtraPrem <> 0 Or nExtraPrem = eRemoteDB.Constants.intNull) And nExtraPrem > 100 Then
			Call lclsErrors.ErrorMessage(sCodispl, 11239)
		End If
		If (nDiscount <> 0 Or nDiscount = eRemoteDB.Constants.intNull) And nDiscount > 100 Then
			Call lclsErrors.ErrorMessage(sCodispl, 11239)
		End If
        If sAction = "Add" Then
            If nCategory > 0 Then
                If IsExist(nBranch, nProduct, dEffecDate, nCategory) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 250477)
                End If
            End If
        End If
        InsValMRO004 = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
	End Function
	
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'**%Parameters:
	'**%    nHeader     - Indicator of the page (headed or it details)
	'**%    sCodispl    - Code of the page.
	'**%    nMaisAction - Action of the Menu.
	'**%    sAction     - Action of grid.
	'**%    nusercode   - The code user.
	'**%    nBranch     - Code of the line of business.
	'**%    nProduct    - Code of the product.
	'**%    dEffecDate  - Date which from the record is valid.
	'**%    nCategory  - Risk category
	'**%    nExtraPrem - Percentage of extra-premium per construction category
	'**%    nDiscount  - Percentage of discount per construction category
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%    nHeader     - Indicador de la página (encabezado o detalle).
	'%    sCodispl    - Código de la página
	'%    nMaisAction - Acción del Menu.
	'%    sAction     - Acción del grid.
	'%    nusercode   - Código de usuario.
	'%    nBranch     - Código del ramo comercial.
	'%    nProduct    - Código del producto.
	'%    dEffecDate  -  Fecha de efecto del registro.
	'%    nCategory   - Categoria del riesgo
	'%    nExtraPrem  - Porcentaje de recargo por construcción
	'%    nDiscount   - Porcentaje de descuento por construcción
	Public Function InsPostMRO004(ByVal nHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date, ByVal nCategory As Short, ByVal nExtraPrem As Double, ByVal nDiscount As Double) As Boolean

		If nHeader Then
			InsPostMRO004 = True
		Else
			If sAction = "Add" Then
				InsPostMRO004 = Add(nUsercode, nBranch, nProduct, dEffecDate, nCategory, nExtraPrem, nDiscount)
			ElseIf sAction = "Update" Then 
				InsPostMRO004 = Update(nUsercode, nBranch, nProduct, dEffecDate, nCategory, nExtraPrem, nDiscount)
			ElseIf sAction = "Del" Then 
				InsPostMRO004 = Delete(nBranch, nProduct, dEffecDate, nCategory, nUsercode)
			End If
		End If
		
		Exit Function
	End Function
End Class












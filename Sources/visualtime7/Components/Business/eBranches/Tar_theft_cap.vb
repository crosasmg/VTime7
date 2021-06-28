Option Strict Off
Option Explicit On
Public Class Tar_theft_cap
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'Tar_theft_cap' in the system 25/06/2004 02:34:27 p.m.
	'+Objetivo: Propiedades según la tabla 'Tar_theft_cap' en el sistema 25/06/2004 02:34:27 p.m.
	Public nBranch As Short
	Public nProduct As Short
	Public nCover As Short
	Public nCurrency As Short
	Public dEffecDate As Date
	Public nCap_init As Double
	Public nCap_end As Double
	Public nTar_theft As Short
	
	
	
	'**%Objective: Add a record to the table "Tar_theft_cap"
	'**%Parameters:
	'**%    nUsercode  - The user code
	'**%    nBranch    - Code of the commercial branch.
	'**%    nProduct   - Code of the product.
	'**%    nCover     - Code of the cover.
	'**%    nCurrency  - Code of the currency.
	'**%    dEffecdate - Date which from the record is valid.
	'**%    nCap_init  - Initial sum insured
	'**%    nCap_end   - Final sum insured
	'**%    nTar_theft - Code of the theft tariff
	'%Objetivo: Agrega un registro a la tabla "Tar_theft_cap"
	'%Parámetros:
	'%    nUsercode  - Código del usuario
	'%    nBranch    - Codigo del ramo comercial.
	'%    nProduct   - Codigo del producto.
	'%    nCover     - Codigo de la cobertura.
	'%    nCurrency  - Código de la moneda.
	'%    dEffecdate - Fecha de efecto del registro.
	'%    nCap_init  - Monto de capital inicial
	'%    nCap_end   - Monto de capital final
	'%    nTar_theft - Código de la tarifa de robo
	Private Function Add(ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal nCurrency As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double, ByVal nTar_theft As Short) As Boolean
		Dim lclsTar_theft_cap As eRemoteDB.Execute
		
        lclsTar_theft_cap = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.creTar_theft_cap'. Generated on 25/06/2004 02:34:27 p.m.
		
		With lclsTar_theft_cap
			.StoredProcedure = "creTar_theft_cap"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_init", nCap_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTar_theft", nTar_theft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		lclsTar_theft_cap = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Updates a registry to the table "Tar_theft_cap" using the key for this table.
	'**%Parameters:
	'**%    nUsercode  - The user code
	'**%    nBranch    - Code of the commercial branch.
	'**%    nProduct   - Code of the product.
	'**%    nCover     - Code of the cover.
	'**%    nCurrency  - Code of the currency.
	'**%    dEffecdate - Date which from the record is valid.
	'**%    nCap_init  - Initial sum insured
	'**%    nCap_end   - Final sum insured
	'**%    nTar_theft - Code of the theft tariff
	'%Objetivo: Actualiza un registro a la tabla "Tar_theft_cap" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nUsercode  - Código del usuario
	'%    nBranch    - Codigo del ramo comercial.
	'%    nProduct   - Codigo del producto.
	'%    nCover     - Codigo de la cobertura.
	'%    nCurrency  - Código de la moneda.
	'%    dEffecdate - Fecha de efecto del registro.
	'%    nCap_init  - Monto de capital inicial
	'%    nCap_end   - Monto de capital final
	'%    nTar_theft - Código de la tarifa de robo
	Private Function Update(ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal nCurrency As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double, ByVal nTar_theft As Short) As Boolean
		Dim lclsTar_theft_cap As eRemoteDB.Execute
		
        lclsTar_theft_cap = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updTar_theft_cap'. Generated on 25/06/2004 02:34:27 p.m.
		With lclsTar_theft_cap
			.StoredProcedure = "insupdTar_theft_cap"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_init", nCap_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTar_theft", nTar_theft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		lclsTar_theft_cap = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Delete a registry the table "Tar_theft_cap" using the key for this table.
	'**%Parameters:
	'**%    nBranch    - Code of the commercial branch.
	'**%    nProduct   - Code of the product.
	'**%    nCover     - Code of the cover.
	'**%    nCurrency  - Code of the currency.
	'**%    dEffecdate - Date which from the record is valid.
	'**%    nCap_init  - Initial sum insured
	'**%    nUsercode  - The user code
	'%Objetivo: Elimina un registro a la tabla "Tar_theft_cap" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nBranch    - Codigo del ramo comercial.
	'%    nProduct   - Codigo del producto.
	'%    nCover     - Codigo de la cobertura.
	'%    nCurrency  - Código de la moneda.
	'%    dEffecdate - Fecha de efecto del registro.
	'%    nCap_init  - Monto de capital inicial
	'%    nUsercode  - Código del usuario
	Private Function Delete(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal nCurrency As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nUsercode As Integer) As Boolean
		Dim lclsTar_theft_cap As eRemoteDB.Execute
		
        lclsTar_theft_cap = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.delTar_theft_cap'. Generated on 25/06/2004 02:34:27 p.m.
		With lclsTar_theft_cap
			.StoredProcedure = "delTar_theft_cap"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_init", nCap_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		lclsTar_theft_cap = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: It verifies the existence of a registry in table "Tar_theft_cap" using the key of this table.
	'**%Parameters:
	'**%    nBranch    - Code of the commercial branch.
	'**%    nProduct   - Code of the product.
	'**%    nCover     - Code of the cover.
	'**%    nCurrency  - Code of the currency.
	'**%    dEffecdate - Date which from the record is valid.
	'**%    nCap_init  - Initial sum insured
	'%Objetivo: Verifica la existencia de un registro en la tabla "Tar_theft_cap" usando la clave de dicha tabla.
	'%Parámetros:
	'%    nBranch    - Codigo del ramo comercial.
	'%    nProduct   - Codigo del producto.
	'%    nCover     - Codigo de la cobertura.
	'%    nCurrency  - Código de la moneda.
	'%    dEffecdate - Fecha de efecto del registro.
	'%    nCap_init  - Monto de capital inicial
	Private Function IsExist(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal nCurrency As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double) As Boolean
		Dim lclsTar_theft_cap As eRemoteDB.Execute
		Dim lintExist As Short
		
        lclsTar_theft_cap = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valTar_theft_capExist'. Generated on 25/06/2004 02:34:27 p.m.
		With lclsTar_theft_cap
			.StoredProcedure = "reaTar_theft_cap_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_init", nCap_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		
		lclsTar_theft_cap = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validation of the data for the page of the headed one.
	'**%Parameters:
	'**%    sCodispl    - code of the page.
	'**%    nMainAction -
	'**%    sAction     -
	'**%    nBranch     - Code of the commercial branch.
	'**%    nProduct    - Code of the product.
	'**%    nCover      - Code of the cover.
	'**%    nCurrency   - Code of the currency.
	'**%    dEffecdate  - Date which from the record is valid.
	'%Objetivo: Validación de los datos para la página del encabezado.
	'%Parámetros:
	'%    sCodispl    - code of the page.
	'%    nMainAction -
	'%    sAction     -
	'%    nBranch     - Codigo del ramo comercial.
	'%    nProduct    - Codigo del producto.
	'%    nCover      - Codigo de la cobertura.
	'%    nCurrency   - Código de la moneda.
	'%    dEffecdate  - Fecha de efecto del registro.
	Public Function InsValMRO001_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal nCurrency As Short, ByVal dEffecDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
        lclsErrors = New eFunctions.Errors
		
        If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 1022)
        End If
		If (nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 11009)
		End If
		If (nCover = 0 Or nCover = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 11163)
		End If
		If dEffecDate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 11198)
        Else
            If nMainAction = 302 Then
                If (nBranch > 0) And _
                   (nProduct > 0) And _
                   (nCover > 0) And _
                   (nCurrency > 0) Then
                    If (IsExist(nBranch, nProduct, nCover, nCurrency, dEffecDate, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 10869)
                    End If
                End If

                If dEffecDate <= Today Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10868)
                End If
            End If

        End If

        InsValMRO001_k = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%    sCodispl    - code of the page.
	'**%    nMainAction -
	'**%    sAction     -
	'**%    nBranch     - Code of the commercial branch.
	'**%    nProduct    - Code of the product.
	'**%    nCover      - Code of the cover.
	'**%    nCurrency   - Code of the currency.
	'**%    dEffecdate  - Date which from the record is valid.
	'**%    nCap_init   - Initial sum insured
	'**%    nCap_end    - Final sum insured
	'**%    nTar_theft  - Code of the theft tariff
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl    - code of the page.
	'%    nMainAction -
	'%    sAction     -
	'%    nBranch     - Codigo del ramo comercial.
	'%    nProduct    - Codigo del producto.
	'%    nCover      - Codigo de la cobertura.
	'%    nCurrency   - Código de la moneda.
	'%    dEffecdate  - Fecha de efecto del registro.
	'%    nCap_init   - Monto de capital inicial
	'%    nCap_end    - Monto de capital final
	'%    nTar_theft  - Código de la tarifa de robo
	Public Function InsValMRO001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal nCurrency As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double, ByVal nTar_theft As Short) As String
		Dim lclsErrors As eFunctions.Errors
		
        lclsErrors = New eFunctions.Errors
		
		If (nCap_init = 0 Or nCap_init = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10163)
		End If
		If (nTar_theft = 0 Or nTar_theft = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10146)
		End If
		If (nCap_end = 0 Or nCap_end = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10147)
		End If
		If (nCap_end <> 0 Or nCap_end = eRemoteDB.Constants.intNull) And nCap_end <= nCap_init Then
			Call lclsErrors.ErrorMessage(sCodispl, 10148)
		End If
		
		If sAction = "Add" And IsExist(nBranch, nProduct, nCover, nCurrency, dEffecDate, nCap_init, nCap_end) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10149)
		End If
		
		InsValMRO001 = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'**%Parameters:
	'**%    nHeader     - Indicator of the page (headed or it details)
	'**%    sCodispl    - Code of the page.
	'**%    nMainAction - Action of the Menu.
	'**%    sAction     - Action of grid.
	'**%    nBranch     - Code of the commercial branch.
	'**%    nProduct    - Code of the product.
	'**%    nCover      - Code of the cover.
	'**%    nCurrency   - Code of the currency.
	'**%    dEffecdate  - Date which from the record is valid.
	'**%    nCap_init   - Initial sum insured
	'**%    nCap_end    - Final sum insured
	'**%    nTar_theft  - Code of the theft tariff
	'%Objetivo: Validación de los datos para la página detalle.
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%    nHeader     - Indicador de la página (encabezado o detalle).
	'%    sCodispl    - Códigi de la página
	'%    nMainAction - Acción del Menu.
	'%    sAction     - Acción del grid.
	'%    nBranch     - Codigo del ramo comercial.
	'%    nProduct    - Codigo del producto.
	'%    nCover      - Codigo de la cobertura.
	'%    nCurrency   - Código de la moneda.
	'%    dEffecdate  - Fecha de efecto del registro.
	'%    nCap_init   - Monto de capital inicial
	'%    nCap_end    - Monto de capital final
	'%    nTar_theft  - Código de la tarifa de robo
	Public Function InsPostMRO001(ByVal nHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal nCurrency As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double, ByVal nTar_theft As Short) As Boolean

		If nHeader Then
			InsPostMRO001 = True
		Else
			If sAction = "Add" Then
				InsPostMRO001 = Add(nUsercode, nBranch, nProduct, nCover, nCurrency, dEffecDate, nCap_init, nCap_end, nTar_theft)
			ElseIf sAction = "Update" Then 
				InsPostMRO001 = Update(nUsercode, nBranch, nProduct, nCover, nCurrency, dEffecDate, nCap_init, nCap_end, nTar_theft)
			ElseIf sAction = "Del" Then 
				InsPostMRO001 = Delete(nBranch, nProduct, nCover, nCurrency, dEffecDate, nCap_init, nUsercode)
			End If
		End If
		
		Exit Function
	End Function
End Class












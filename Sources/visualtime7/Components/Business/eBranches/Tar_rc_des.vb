Option Strict Off
Option Explicit On
Public Class Tar_rc_des
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'Tar_rc_des' in the system 16/06/2004 12:07:08 p.m.
	'+Objetivo: Propiedades según la tabla 'Tar_rc_des' en el sistema 16/06/2004 12:07:08 p.m.
	Public nBranch As Short
	Public nProduct As Short
	Public nCover As Short
	Public dEffecDate As Date
	Public nCap_init As Double
	Public nCap_end As Double
	Public nRate As Double
	
	
	
	'**%Objective: Add a record to the table "Tar_rc_des"
	'**%Parameters:
	'**%    nUsercode  - user code
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    nCover     - code of the cover.
	'**%    dEffecDate - date which from the record is valid.
	'**%    nCap_init  - initial volume amount to which the discount belongs
	'**%    nCap_end   - final volume amount to which the discount belongs.
	'**%    nRate      - discount percentage to be applied to the  covers rate
	
	'%Objetivo: Agrega un registro a la tabla "Tar_rc_des"
	'%Parámetros:
	'%    nUsercode  - codigo del usuario
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    nCover     - codigo de la cobertura.
	'%    dEffecDate - fecha de efecto del registro.
	'%    nCap_init  - monto del volumen inicial al que corresponde  el descuento.
	'%    nCap_end   - monto del volumen final al que corresponde el descuento.
	'%    nRate      - porcentaje de descuento a aplicar a la tasa de la cobertura.
	Private Function Add(ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double, ByVal nRate As Double) As Boolean
		Dim lclsTar_rc_des As eRemoteDB.Execute
		
        lclsTar_rc_des = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.creTar_rc_des'. Generated on 16/06/2004 12:07:08 p.m.
		
		With lclsTar_rc_des
			.StoredProcedure = "creTar_rc_des"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_init", nCap_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		lclsTar_rc_des = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Updates a registry to the table "Tar_rc_des" using the key for this table.
	'**%Parameters:
	'**%    nUsercode  - user code
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    nCover     - code of the cover.
	'**%    dEffecDate - date which from the record is valid.
	'**%    nCap_init  - initial volume amount to which the discount belongs
	'**%    nCap_end   - final volume amount to which the discount belongs.
	'**%    nRate      - discount percentage to be applied to the  covers rate
	'%Objetivo: Actualiza un registro a la tabla "Tar_rc_des" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nUsercode  - codigo del usuario
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    nCover     - codigo de la cobertura.
	'%    dEffecDate - fecha de efecto del registro.
	'%    nCap_init  - monto del volumen inicial al que corresponde  el descuento.
	'%    nCap_end   - monto del volumen final al que corresponde el descuento.
	'%    nRate      - porcentaje de descuento a aplicar a la tasa de la cobertura.
	Private Function Update(ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double, ByVal nRate As Double) As Boolean
		Dim lclsTar_rc_des As eRemoteDB.Execute
		
        lclsTar_rc_des = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updTar_rc_des'. Generated on 16/06/2004 12:07:08 p.m.
		With lclsTar_rc_des
			.StoredProcedure = "insupdTar_rc_des"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_init", nCap_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		lclsTar_rc_des = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Delete a registry the table "Tar_rc_des" using the key for this table.
	'**%Parameters:
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    nCover     - code of the cover.
	'**%    dEffecDate - date which from the record is valid.
	'**%    nCap_init  - initial volume amount to which the discount belongs
	'**%    nUsercode  - user code
	
	'%Objetivo: Elimina un registro a la tabla "Tar_rc_des" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    nCover     - codigo de la cobertura.
	'%    dEffecDate - fecha de efecto del registro.
	'%    nCap_init  - monto del volumen inicial al que corresponde  el descuento.
	'%    nUsercode  - codigo del usuario
	Private Function Delete(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nUsercode As Integer) As Boolean
		Dim lclsTar_rc_des As eRemoteDB.Execute
		
        lclsTar_rc_des = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.delTar_rc_des'. Generated on 16/06/2004 12:07:08 p.m.
		With lclsTar_rc_des
			.StoredProcedure = "delTar_rc_des"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_init", nCap_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		lclsTar_rc_des = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: It verifies the existence of a registry in table "Tar_rc_des" using the key of this table.
	'**%Parameters:
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    nCover     - code of the cover.
	'**%    dEffecDate - date which from the record is valid.
	'**%    nCap_init  - initial volume amount to which the discount belongs
	'%Objetivo: Verifica la existencia de un registro en la tabla "Tar_rc_des" usando la clave de dicha tabla.
	'%Parámetros:
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    nCover     - codigo de la cobertura.
	'%    dEffecDate - fecha de efecto del registro.
	'%    nCap_init  - monto del volumen inicial al que corresponde  el descuento.
	Private Function IsExist(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double) As Boolean
		Dim lclsTar_rc_des As eRemoteDB.Execute
		Dim lintExist As Short
		
        lclsTar_rc_des = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valTar_rc_desExist'. Generated on 16/06/2004 12:07:08 p.m.
		With lclsTar_rc_des
			.StoredProcedure = "reaTar_rc_des_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_init", nCap_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		
		lclsTar_rc_des = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validation of the data for the page of the headed one.
	'**%Parameters:
	'**%    sCodispl    - code of the page.
	'**%    nMainAction -
	'**%    sAction     -
	'**%    nBranch     - code of the commercial branch.
	'**%    nProduct    - code of the product.
	'**%    nCover      - code of the cover.
	'**%    dEffecDate  - date which from the record is valid.
	'%Objetivo: Validación de los datos para la página del encabezado.
	'%Parámetros:
	'%    sCodispl    - code of the page.
	'%    nMainAction -
	'%    sAction     -
	'%    nBranch     - codigo del ramo comercial.
	'%    nProduct    - codigo del producto.
	'%    nCover      - codigo de la cobertura.
	'%    dEffecDate  - fecha de efecto del registro.
	Public Function InsValMRC003_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsProduct As eProduct.Product
		
        lclsErrors = New eFunctions.Errors
        lclsProduct = New eProduct.Product
		
		If nMainAction = 302 And IsExist(eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, dEffecDate, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10869)
		End If
		
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
        End If

        If (nBranch > 0) And (nProduct > 0) And (dEffecDate <> dtmNull) Then
            Call lclsProduct.Find(nBranch, nProduct, dEffecDate)
            If lclsProduct.sBrancht <> 4 Then
                Call lclsErrors.ErrorMessage(sCodispl, 1025)
            End If
        End If

        If nMainAction = 302 And dEffecDate <> dtmNull And dEffecDate <= Today Then
            Call lclsErrors.ErrorMessage(sCodispl, 10868)
        End If

        InsValMRC003_k = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%    sCodispl    - code of the page.
	'**%    nMainAction -
	'**%    sAction     -
	'**%    nBranch     - code of the commercial branch.
	'**%    nProduct    - code of the product.
	'**%    nCover      - code of the cover.
	'**%    dEffecDate  - date which from the record is valid.
	'**%    nCap_init   - initial volume amount to which the discount belongs
	'**%    nCap_end    - final volume amount to which the discount belongs.
	'**%    nRate       - discount percentage to be applied to the  covers rate
	
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl    - code of the page.
	'%    nMainAction -
	'%    sAction     -
	'%    nBranch     - codigo del ramo comercial.
	'%    nProduct    - codigo del producto.
	'%    nCover      - codigo de la cobertura.
	'%    dEffecDate  - fecha de efecto del registro.
	'%    nCap_init   - monto del volumen inicial al que corresponde  el descuento.
	'%    nCap_end    - monto del volumen final al que corresponde el descuento.
	'%    nRate       - porcentaje de descuento a aplicar a la tasa de la cobertura.
	Public Function InsValMRC003(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double, ByVal nRate As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
        lclsErrors = New eFunctions.Errors
		
        If nCap_init < 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 767037)
        End If

        If nCap_end < 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 767038)
        End If

        If (nCap_init >= 0) And (nCap_end >= 0) Then
            If nCap_end <= nCap_init Then
                Call lclsErrors.ErrorMessage(sCodispl, 10148)
            End If
        End If

        If nRate < 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1020)
        End If

		If sAction = "Add" And IsExist(nBranch, nProduct, nCover, dEffecDate, nCap_init, nCap_end) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10149)
		End If
		
		InsValMRC003 = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'**%Parameters:
	'**%    sCodispl    - code of the page.
	'**%    nMainAction -
	'**%    sAction     -
	'**%    nBranch     - code of the commercial branch.
	'**%    nProduct    - code of the product.
	'**%    nCover      - code of the cover.
	'**%    dEffecDate  - date which from the record is valid.
	'**%    nCap_init   - initial volume amount to which the discount belongs
	'**%    nCap_end    - final volume amount to which the discount belongs.
	'**%    nRate       - discount percentage to be applied to the  covers rate
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%    sCodispl    - code of the page.
	'%    nMainAction -
	'%    sAction     -
	'%    nBranch     - codigo del ramo comercial.
	'%    nProduct    - codigo del producto.
	'%    nCover      - codigo de la cobertura.
	'%    dEffecDate  - fecha de efecto del registro.
	'%    nCap_init   - monto del volumen inicial al que corresponde  el descuento.
	'%    nCap_end    - monto del volumen final al que corresponde el descuento.
	'%    nRate       - porcentaje de descuento a aplicar a la tasa de la cobertura.
	Public Function InsPostMRC003(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double, ByVal nRate As Double) As Boolean

		If pblnHeader Then
			InsPostMRC003 = True
		Else
			If sAction = "Add" Then
				InsPostMRC003 = Add(nUsercode, nBranch, nProduct, nCover, dEffecDate, nCap_init, nCap_end, nRate)
			ElseIf sAction = "Update" Then 
				InsPostMRC003 = Update(nUsercode, nBranch, nProduct, nCover, dEffecDate, nCap_init, nCap_end, nRate)
			ElseIf sAction = "Del" Then 
				InsPostMRC003 = Delete(nBranch, nProduct, nCover, dEffecDate, nCap_init, nUsercode)
			End If
		End If
		
		Exit Function
	End Function
End Class












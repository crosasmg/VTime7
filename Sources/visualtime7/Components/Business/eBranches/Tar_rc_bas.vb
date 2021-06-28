Option Strict Off
Option Explicit On
Public Class Tar_rc_bas
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'Tar_rc_bas' in the system 6/16/2004 2:38:25 PM
	'+Objetivo: Propiedades según la tabla 'Tar_rc_bas' en el sistema 6/16/2004 2:38:25 PM
	Public nBranch As Short
	Public nProduct As Short
	Public nCover As Short
	Public dEffecDate As Date
	Public nArticle As Short
	Public nDetailArt As Short
    Public nRate As Double
    Public nCommergrp As Short
	
	
	
	'**%Objective: Add a record to the table "Tar_rc_bas"
	'**%Parameters:
	'**%    nUsercode  - user code
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    nCover     - code of the cover.
	'**%    dEffecDate - date which from the record is valid.
	'**%    nArticle   - type of business activity
	'**%    nDetailArt - detail of the type of business.
	'**%    nRate      - rate(o/oo) to be applied to obtain the premium of the cover
	
	'%Objetivo: Agrega un registro a la tabla "Tar_rc_bas"
	'%Parámetros:
	'%    nUsercode  - codigo del usuario
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    nCover     - codigo de la cobertura.
	'%    dEffecDate - fecha de efecto del registro.
	'%    nArticle   - tipo de negocio
	'%    nDetailArt - detalle del tipo de negocio.
	'%    nRate      - pormilaje correspondiente a la tasa basica.
    Private Function Add(ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date, ByVal nArticle As Short, ByVal nDetailArt As Short, ByVal nRate As Double, ByVal nCommergrp As Short) As Boolean
        Dim lclsTar_rc_bas As eRemoteDB.Execute

        lclsTar_rc_bas = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.creTar_rc_bas'. Generated on 6/16/2004 2:38:25 PM

        With lclsTar_rc_bas
            .StoredProcedure = "creTar_rc_bas"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nArticle", nArticle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDetailArt", nDetailArt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommergrp", nCommergrp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

        lclsTar_rc_bas = Nothing

        Exit Function
    End Function
	
	'**%Objective: Updates a registry to the table "Tar_rc_bas" using the key for this table.
	'**%Parameters:
	'**%    nUsercode  - user code
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    nCover     - code of the cover.
	'**%    dEffecDate - date which from the record is valid.
	'**%    nArticle   - type of business activity
	'**%    nDetailArt - detail of the type of business.
	'**%    nRate      - rate(o/oo) to be applied to obtain the premium of the cover
	'%Objetivo: Actualiza un registro a la tabla "Tar_rc_bas" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nUsercode  - codigo del usuario
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    nCover     - codigo de la cobertura.
	'%    dEffecDate - fecha de efecto del registro.
	'%    nArticle   - tipo de negocio
	'%    nDetailArt - detalle del tipo de negocio.
	'%    nRate      - pormilaje correspondiente a la tasa basica.
    Private Function Update(ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date, ByVal nArticle As Short, ByVal nDetailArt As Short, ByVal nRate As Double, ByVal nCommergrp As Short) As Boolean
        Dim lclsTar_rc_bas As eRemoteDB.Execute

        lclsTar_rc_bas = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updTar_rc_bas'. Generated on 6/16/2004 2:38:25 PM
        With lclsTar_rc_bas
            .StoredProcedure = "insupdTar_rc_bas"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nArticle", nArticle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDetailArt", nDetailArt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommergrp", nCommergrp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

        lclsTar_rc_bas = Nothing

        Exit Function
    End Function
	
	'**%Objective: Delete a registry the table "Tar_rc_bas" using the key for this table.
	'**%Parameters:
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    nCover     - code of the cover.
	'**%    dEffecDate - date which from the record is valid.
	'**%    nArticle   - type of business activity
	'**%    nDetailArt - detail of the type of business.
	'**%    nUsercode  - user code
	'%Objetivo: Elimina un registro a la tabla "Tar_rc_bas" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    nCover     - codigo de la cobertura.
	'%    dEffecDate - fecha de efecto del registro.
	'%    nArticle   - tipo de negocio
	'%    nDetailArt - detalle del tipo de negocio.
	'%    nUsercode  - codigo del usuario
    Private Function Delete(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date, ByVal nCommergrp As Short, ByVal nDetailArt As Short, ByVal nUsercode As Integer) As Boolean
        Dim lclsTar_rc_bas As eRemoteDB.Execute

        lclsTar_rc_bas = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.delTar_rc_bas'. Generated on 6/16/2004 2:38:25 PM
        With lclsTar_rc_bas
            .StoredProcedure = "delTar_rc_bas"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommergrp", nCommergrp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDetailArt", nDetailArt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

        lclsTar_rc_bas = Nothing

        Exit Function
    End Function
	
	'**%Objective: It verifies the existence of a registry in table "Tar_rc_bas" using the key of this table.
	'**%Parameters:
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    nCover     - code of the cover.
	'**%    dEffecDate - date which from the record is valid.
	'**%    nArticle   - type of business activity
	'**%    nDetailArt - detail of the type of business.
	'%Objetivo: Verifica la existencia de un registro en la tabla "Tar_rc_bas" usando la clave de dicha tabla.
	'%Parámetros:
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    nCover     - codigo de la cobertura.
	'%    dEffecDate - fecha de efecto del registro.
	'%    nArticle   - tipo de negocio
	'%    nDetailArt - detalle del tipo de negocio.
    Private Function IsExist(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date, ByVal nCommergrp As Short, ByVal nDetailArt As Short) As Boolean
        Dim lclsTar_rc_bas As eRemoteDB.Execute
        Dim lintExist As Short

        lclsTar_rc_bas = New eRemoteDB.Execute
        lintExist = 0

        '+ Define all parameters for the stored procedures 'insudb.valTar_rc_basExist'. Generated on 6/16/2004 2:38:25 PM
        With lclsTar_rc_bas
            .StoredProcedure = "reaTar_rc_bas_v"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommergrp", nCommergrp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDetailArt", nDetailArt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With

        lclsTar_rc_bas = Nothing

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
	Public Function InsValMRC001_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
        lclsErrors = New eFunctions.Errors
		
		If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 1022)
		End If
		If (nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 11009)
		End If
		If (nCover = 0 Or nCover = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 4061)
		End If
		If dEffecDate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 2056)
		End If
		If nMainAction = 302 And dEffecDate <> dtmNull And nMainAction = 302 And dEffecDate <= Today Then
			Call lclsErrors.ErrorMessage(sCodispl, 10868)
		End If
		
		InsValMRC001_k = lclsErrors.Confirm
		
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
	'**%    nArticle   - type of business activity
	'**%    nDetailArt - detail of the type of business.
	'**%    nRate      - rate(o/oo) to be applied to obtain the premium of the cover
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl    - code of the page.
	'%    nMainAction -
	'%    sAction     -
	'%    nBranch     - codigo del ramo comercial.
	'%    nProduct    - codigo del producto.
	'%    nCover      - codigo de la cobertura.
	'%    dEffecDate  - fecha de efecto del registro.
	'%    nArticle   - tipo de negocio
	'%    nDetailArt - detalle del tipo de negocio.
	'%    nRate      - pormilaje correspondiente a la tasa basica.
    Public Function InsValMRC001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date, ByVal nCommergrp As Short, ByVal nDetailArt As Short, ByVal nRate As Double) As String
        Dim lclsErrors As eFunctions.Errors

        lclsErrors = New eFunctions.Errors

        If (nCommergrp = 0 Or nCommergrp = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3485)
        End If
        If (nDetailArt = 0 Or nDetailArt = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3486)
        End If
        If sAction = "Add" And IsExist(nBranch, nProduct, nCover, dEffecDate, nCommergrp, nDetailArt) Then
            Call lclsErrors.ErrorMessage(sCodispl, 80001)
        End If
        If nRate < 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 2042)
        End If

        InsValMRC001 = lclsErrors.Confirm

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
	'**%    nArticle   - type of business activity
	'**%    nDetailArt - detail of the type of business.
	'**%    nRate      - rate(o/oo) to be applied to obtain the premium of the cover
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%    sCodispl    - code of the page.
	'%    nMainAction -
	'%    sAction     -
	'%    nBranch     - codigo del ramo comercial.
	'%    nProduct    - codigo del producto.
	'%    nCover      - codigo de la cobertura.
	'%    dEffecDate  - fecha de efecto del registro.
	'%    nArticle   - tipo de negocio
	'%    nDetailArt - detalle del tipo de negocio.
	'%    nRate      - pormilaje correspondiente a la tasa basica.
    Public Function InsPostMRC001(ByVal pblnHeader As Boolean, _
                                  ByVal sCodispl As String, _
                                  ByVal nMainAction As Integer, _
                                  ByVal sAction As String, _
                                  ByVal nUsercode As Integer, _
                                  ByVal nBranch As Short, _
                                  ByVal nProduct As Short, _
                                  ByVal nCover As Short, _
                                  ByVal dEffecDate As Date, _
                                  ByVal nArticle As Short, _
                                  ByVal nDetailArt As Short, _
                                  ByVal nRate As Double, _
                                  ByVal nCommergrp As Short) As Boolean

        If pblnHeader Then
            InsPostMRC001 = True
        Else
            If sAction = "Add" Then
                InsPostMRC001 = Add(nUsercode, nBranch, nProduct, nCover, dEffecDate, nArticle, nDetailArt, nRate, nCommergrp)
            ElseIf sAction = "Update" Then
                InsPostMRC001 = Update(nUsercode, nBranch, nProduct, nCover, dEffecDate, nArticle, nDetailArt, nRate, nCommergrp)
            ElseIf sAction = "Del" Then
                InsPostMRC001 = Delete(nBranch, nProduct, nCover, dEffecDate, nArticle, nDetailArt, nUsercode)
            End If
        End If

        Exit Function
    End Function
End Class












Option Strict Off
Option Explicit On
Public Class tar_rc_fac
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'tar_rc_fac' in the system 6/16/2004 2:10:07 PM
	'+Objetivo: Propiedades según la tabla 'tar_rc_fac' en el sistema 6/16/2004 2:10:07 PM
	Public nBranch As Short
	Public nProduct As Short
	Public dEffecDate As Date
	Public nCap_init As Double
	Public nCap_end As Double
	Public nRate As Double
	
	
	
	'**%Objective: Add a record to the table "tar_rc_fac"
	'**%Parameters:
	'**%    nUsercode  - user code
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    dEffecDate - date which from the record is valid.
	'**%    nCap_init  - initial volume amount to which the discount belongs
	'**%    nCap_end   - final volume amount to which the discount belongs.
	'**%    nRate      - discount percentage to be applied to the  covers rate
	
	'%Objetivo: Agrega un registro a la tabla "tar_rc_fac"
	'%Parámetros:
	'%    nUsercode  - codigo del usuario
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    dEffecDate - fecha de efecto del registro.
	'%    nCap_init  - monto del volumen inicial al que corresponde  el descuento.
	'%    nCap_end   - monto del volumen final al que corresponde el descuento.
	'%    nRate      - porcentaje de descuento a aplicar a la tasa de la cobertura.
	Private Function Add(ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double, ByVal nRate As Double) As Boolean
		Dim lclstar_rc_fac As eRemoteDB.Execute
		
        lclstar_rc_fac = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.cretar_rc_fac'. Generated on 6/16/2004 2:10:07 PM
		
		With lclstar_rc_fac
			.StoredProcedure = "cretar_rc_fac"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_init", nCap_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		lclstar_rc_fac = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Updates a registry to the table "tar_rc_fac" using the key for this table.
	'**%Parameters:
	'**%    nUsercode  - user code
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    dEffecDate - date which from the record is valid.
	'**%    nCap_init  - initial volume amount to which the discount belongs
	'**%    nCap_end   - final volume amount to which the discount belongs.
	'**%    nRate      - discount percentage to be applied to the  covers rate
	'%Objetivo: Actualiza un registro a la tabla "tar_rc_fac" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nUsercode  - codigo del usuario
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    dEffecDate - fecha de efecto del registro.
	'%    nCap_init  - monto del volumen inicial al que corresponde  el descuento.
	'%    nCap_end   - monto del volumen final al que corresponde el descuento.
	'%    nRate      - porcentaje de descuento a aplicar a la tasa de la cobertura.
	Private Function Update(ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double, ByVal nRate As Double) As Boolean
		Dim lclstar_rc_fac As eRemoteDB.Execute
		
        lclstar_rc_fac = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updtar_rc_fac'. Generated on 6/16/2004 2:10:07 PM
		With lclstar_rc_fac
			.StoredProcedure = "insupdtar_rc_fac"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_init", nCap_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_end", nCap_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		lclstar_rc_fac = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Delete a registry the table "tar_rc_fac" using the key for this table.
	'**%Parameters:
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    dEffecDate - date which from the record is valid.
	'**%    nCap_init  - initial volume amount to which the discount belongs
	'**%    nUsercode  - user code
	'%Objetivo: Elimina un registro a la tabla "tar_rc_fac" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    dEffecDate - fecha de efecto del registro.
	'%    nCap_init  - monto del volumen inicial al que corresponde  el descuento.
	'%    nUsercode  - codigo del usuario
	Private Function Delete(ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nUsercode As Integer) As Boolean
		Dim lclstar_rc_fac As eRemoteDB.Execute
		
        lclstar_rc_fac = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.deltar_rc_fac'. Generated on 6/16/2004 2:10:07 PM
		With lclstar_rc_fac
			.StoredProcedure = "deltar_rc_fac"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_init", nCap_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		lclstar_rc_fac = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: It verifies the existence of a registry in table "tar_rc_fac" using the key of this table.
	'**%Parameters:
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    dEffecDate - date which from the record is valid.
	'**%    nCap_init  - initial volume amount to which the discount belongs
	'%Objetivo: Verifica la existencia de un registro en la tabla "tar_rc_fac" usando la clave de dicha tabla.
	'%Parámetros:
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    dEffecDate - fecha de efecto del registro.
	'%    nCap_init  - monto del volumen inicial al que corresponde  el descuento.
	Private Function IsExist(ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double) As Boolean
		Dim lclstar_rc_fac As eRemoteDB.Execute
		Dim lintExist As Short
		
        lclstar_rc_fac = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valtar_rc_facExist'. Generated on 6/16/2004 2:10:07 PM
		With lclstar_rc_fac
			.StoredProcedure = "reatar_rc_fac_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_init", nCap_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		
		lclstar_rc_fac = Nothing
		
		Exit Function
	End Function


    '**%Objective: It verifies the existence of a range in table "tar_rc_fac" using the key of this table.
    '**%Parameters:
    '**%    nBranch    - code of the commercial branch.
    '**%    nProduct   - code of the product.
    '**%    dEffecDate - date which from the record is valid.
    '**%    nCap_init  - initial volume amount to which the discount belongs
    '%Objetivo: Verifica si el monto de capital ya esta contenido en un rango en la tabla "tar_rc_fac" usando la clave de dicha tabla.
    '%Parámetros:
    '%    nBranch    - codigo del ramo comercial.
    '%    nProduct   - codigo del producto.
    '%    dEffecDate - fecha de efecto del registro.
    '%    nCap_init  - monto del volumen inicial al que corresponde  el descuento.
    Private Function IsExistRange(ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date, ByVal nCapital As Double) As Boolean
        Dim lclstar_rc_fac As eRemoteDB.Execute
        Dim lintExist As Short

        lclstar_rc_fac = New eRemoteDB.Execute
        lintExist = 0

        '+ Define all parameters for the stored procedures 'insudb.valtar_rc_facExist'. Generated on 6/16/2004 2:10:07 PM
        With lclstar_rc_fac
            .StoredProcedure = "reatar_rc_fac_range"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExistRange = (.Parameters("nExist").Value > 0)
            Else
                IsExistRange = False
            End If
        End With

        lclstar_rc_fac = Nothing

        Exit Function
    End Function

    '**%Objective: It verifies if there are changes in the table "tar_rc_fac" with Effecdate after of the Effecdate given.
    '**%Parameters:
    '**%    nBranch    - code of the commercial branch.
    '**%    nProduct   - code of the product.
    '**%    dEffecDate - date which from the record is valid.
    '%Objetivo: Verifica si hay modificaciones en la tabla "tar_rc_fac" posteriores a la fecha de efecto indicada.
    '%Parámetros:
    '%    nBranch    - codigo del ramo comercial.
    '%    nProduct   - codigo del producto.
    '%    dEffecDate - fecha de efecto del registro.
    Private Function IsExistModAfter(ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date) As Boolean
        Dim lclstar_rc_fac As eRemoteDB.Execute
        Dim lintExist As Short

        lclstar_rc_fac = New eRemoteDB.Execute
        lintExist = 0

        '+ Define all parameters for the stored procedures 'insudb.valtar_rc_facExist'. Generated on 6/16/2004 2:10:07 PM
        With lclstar_rc_fac
            .StoredProcedure = "valtar_rc_fac_mod"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExistModAfter = (.Parameters("nExist").Value > 0)
            Else
                IsExistModAfter = False
            End If
        End With

        lclstar_rc_fac = Nothing

        Exit Function
    End Function


	'**%Objective: Validation of the data for the page of the headed one.
	'**%Parameters:
	'**%    sCodispl    - code of the page.
	'**%    nMainAction -
	'**%    sAction     -
	'**%    nBranch     - code of the commercial branch.
	'**%    nProduct    - code of the product.
	'**%    dEffecDate  - date which from the record is valid.
	'%Objetivo: Validación de los datos para la página del encabezado.
	'%Parámetros:
	'%    sCodispl    - code of the page.
	'%    nMainAction -
	'%    sAction     -
	'%    nBranch     - codigo del ramo comercial.
	'%    nProduct    - codigo del producto.
	'%    dEffecDate  - fecha de efecto del registro.
	Public Function InsValMRC002_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
        lclsErrors = New eFunctions.Errors
		
		If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 1022)
		End If
		If (nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 11009)
		End If
		If dEffecDate = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 11198)
        Else
            If nMainAction = 302 Then
                If dEffecDate <= Today Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10868)
                Else
                    If IsExistModAfter(nBranch, nProduct, dEffecDate) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 10869)
                    End If
                End If
            End If
        End If

        InsValMRC002_k = lclsErrors.Confirm

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
	'%    dEffecDate  - fecha de efecto del registro.
	'%    nCap_init   - monto del volumen inicial al que corresponde  el descuento.
	'%    nCap_end    - monto del volumen final al que corresponde el descuento.
	'%    nRate       - porcentaje de descuento a aplicar a la tasa de la cobertura.
	Public Function InsValMRC002(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double, ByVal nRate As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
        lclsErrors = New eFunctions.Errors

        If nMainAction = 301 Then
            If IsExistRange(nBranch, nProduct, dEffecDate, nCap_init) Then
                Call lclsErrors.ErrorMessage(sCodispl, 10149)
            Else
                If IsExistRange(nBranch, nProduct, dEffecDate, nCap_end) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10149)
                End If
            End If
        End If

        If nCap_init < 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1935)
        End If

        If nCap_end < 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1935)
        End If

        If (nCap_init >= 0) And (nCap_end >= 0) Then
            If nCap_end <= nCap_init Then
                Call lclsErrors.ErrorMessage(sCodispl, 10148)
            End If
        End If

        If nRate < 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1935)
        End If


        InsValMRC002 = lclsErrors.Confirm

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
	'%    dEffecDate  - fecha de efecto del registro.
	'%    nCap_init   - monto del volumen inicial al que corresponde  el descuento.
	'%    nCap_end    - monto del volumen final al que corresponde el descuento.
	'%    nRate       - porcentaje de descuento a aplicar a la tasa de la cobertura.
	Public Function InsPostMRC002(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date, ByVal nCap_init As Double, ByVal nCap_end As Double, ByVal nRate As Double) As Boolean

		If pblnHeader Then
			InsPostMRC002 = True
		Else
			If sAction = "Add" Then
				InsPostMRC002 = Add(nUsercode, nBranch, nProduct, dEffecDate, nCap_init, nCap_end, nRate)
			ElseIf sAction = "Update" Then 
				InsPostMRC002 = Update(nUsercode, nBranch, nProduct, dEffecDate, nCap_init, nCap_end, nRate)
			ElseIf sAction = "Del" Then 
				InsPostMRC002 = Delete(nBranch, nProduct, dEffecDate, nCap_init, nUsercode)
			End If
		End If
		
		Exit Function
	End Function
End Class












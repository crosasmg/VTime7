Option Strict Off
Option Explicit On
Public Class Theft
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'Theft' in the system 18/06/2004 01:51:52 p.m.
	'+Objetivo: Propiedades según la tabla 'Theft' en el sistema 18/06/2004 01:51:52 p.m.
	Public sCertype As String
	Public nBranch As Short
	Public nProduct As Short
	Public nPolicy As Integer
	Public nCertif As Integer
	Public nInsured As Short
	Public nEmployees As Short
	Public nArea As Short
	Public nVigilance As Short
	'+ Campos añadidos para soportar el Giro de Negocio
	Public sComplCod As String
	Public sDescBussi As String
	Public nConstCat As Short
	'+ Campos para Giro de Negocio
	Public nBusinessty As Short
	Public nCommergrp As Short
	Public nCodkind As Short
	
	'**%Objective: Add a record to the table "Theft"
	'**%Parameters:
	'**%    nusercode  - user code
	'**%    scertype   - type of registry
	'**%    nbranch    - number of branch
	'**%    nproduct   - number of product
	'**%    npolicy    - number of policy
	'**%    ncertif    - number of certificate
	'**%    deffecdate - effect date
	'**%    ninsured   - percentage insured - first risk
	'**%    nemployees - number of employees transporting the money and/or securities
	'**%    narea      - surveillance area (in m2)
	'**%    nvigilance - number of surveillance watchmen
	'**%    sComplCod  - Complete code of the business kind
	'**%    sDescBussi - Specifical description of the business kind
	'**%    nConstCat  - Construction Category
	
	'%Objetivo: Agrega un registro a la tabla "Theft"
	'%Parámetros:
	'%    nusercode   - Código del usuario
	'%    scertype    - Tipo de registro
	'%    nbranch     - Código del ramo
	'%    nproduct    - Código del producto
	'%    npolicy     - Código de la poliza
	'%    ncertif     - Código del certificado
	'%    deffecdate  - Fecha de efecto del registro
	'%    ninsured    - Porcentaje asegurado por primer riesgo.
	'%    nemployees  - Cantidad de empleados usados para el transporte de valores
	'%    narea       - Area de vigilancia ( metros cuadrados )
	'%    nvigilance  - Cantidad de vigilantes para los bienes asegurados.
	'%    sComplCod   - Código completo del giro de negocio
	'%    sDescBussi  - Descripción específica del giro de negocio
	'%    nConstCat   - Categoría de Construcción
	
	Private Function Add(ByVal nUserCode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date, ByVal nInsured As Short, ByVal nEmployees As Short, ByVal nArea As Short, ByVal nVigilance As Short, ByVal sComplCod As String, ByVal sDescBussi As String, ByVal nConstCat As Short) As Boolean
		Dim lclsTheft As eRemoteDB.Execute
		
        lclsTheft = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.creTheft'. Generated on 18/06/2004 01:51:52 p.m.
		
		With lclsTheft
			.StoredProcedure = "creTheft"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsured", nInsured, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEmployees", nEmployees, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea", nArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVigilance", nVigilance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sComplCod", sComplCod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescBussi", sDescBussi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConstCat", nConstCat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		lclsTheft = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Updates a registry to the table "Theft" using the key for this table.
	'**%Parameters:
	'**%Parameters:
	'**%    nusercode  - user code
	'**%    scertype   - type of registry
	'**%    nbranch    - number of branch
	'**%    nproduct   - number of product
	'**%    npolicy    - number of policy
	'**%    ncertif    - number of certificate
	'**%    deffecdate - effect date
	'**%    ninsured   - percentage insured - first risk
	'**%    nemployees -  number of employees transporting the money and/or securities
	'**%    narea      - surveillance area (in m2)
	'**%    nvigilance - number of surveillance watchmen
	'**%    sCodispl   - Logical code of the window
	'**%    sComplCod  - Complete code of the business kind
	'**%    sDescBussi - Specifical description of the business kind
	'**%    nConstCat  - Construction Category
	
	'%Objetivo: Actualiza un registro a la tabla "Theft" usando la clave para dicha tabla.
	'%Parámetros:
	'%Parámetros:
	'%    nusercode   - Código del usuario
	'%    scertype    - Tipo de registro
	'%    nbranch     - Código del ramo
	'%    nproduct    - Código del producto
	'%    npolicy     - Código de la poliza
	'%    ncertif     - Código del certificado
	'%    deffecdate  - Fecha de efecto del registro
	'%    ninsured    - Porcentaje asegurado por primer riesgo.
	'%    nemployees  - Cantidad de empleados usados para el transporte de valores
	'%    narea       - Area de vigilancia ( metros cuadrados )
	'%    nvigilance  - Cantidad de vigilantes para los bienes asegurados.
	'%    sCodispl    - Código lógico de la ventana
	'%    sComplCod   - Código completo del giro de negocio
	'%    sDescBussi  - Descripción específica del giro de negocio
	'%    nConstCat   - Categoría de Construcción
	Private Function Update(ByVal nUserCode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date, ByVal nInsured As Short, ByVal nEmployees As Short, ByVal nArea As Short, ByVal nVigilance As Short, ByVal sCodispl As String, ByVal sComplCod As String, ByVal sDescBussi As String, ByVal nConstCat As Short, ByVal nCodkind As Short, ByVal nBusinessty As Short, ByVal nCommergrp As Short, ByVal nProctype As Short) As Boolean
		Dim lclsTheft As eRemoteDB.Execute
		
        lclsTheft = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updTheft'. Generated on 18/06/2004 01:51:52 p.m.
		With lclsTheft
			.StoredProcedure = "insupdTheft"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsured", nInsured, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEmployees", nEmployees, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea", nArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVigilance", nVigilance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sComplCod", sComplCod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescBussi", sDescBussi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConstCat", nConstCat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCodkind", nCodkind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBusinessty", nBusinessty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommergrp", nCommergrp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProctype", nProctype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		lclsTheft = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Delete a registry the table "Theft" using the key for this table.
	'**%Parameters:
	'**%    scertype   - type of registry
	'**%    nbranch    - number of branch
	'**%    nproduct   - number of product
	'**%    npolicy    - number of policy
	'**%    ncertif    - number of certificate
	'**%    deffecdate - effect date
	'**%    nusercode  - user code
	'%Objetivo: Elimina un registro a la tabla "Theft" usando la clave para dicha tabla.
	'%Parámetros:
	'%    scertype    - Tipo de registro
	'%    nbranch     - Código del ramo
	'%    nproduct    - Código del producto
	'%    npolicy     - Código de la poliza
	'%    ncertif     - Código del certificado
	'%    deffecdate  - Fecha de efecto del registro
	'%    nusercode   - Código del usuario
	Private Function Delete(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date, ByVal nUserCode As Integer) As Boolean
		Dim lclsTheft As eRemoteDB.Execute
		
        lclsTheft = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.delTheft'. Generated on 18/06/2004 01:51:52 p.m.
		With lclsTheft
			.StoredProcedure = "delTheft"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		lclsTheft = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: It verifies the existence of a registry in table "Theft" using the key of this table.
	'**%Parameters:
	'**%    scertype   - type of registry
	'**%    nbranch    - number of branch
	'**%    nproduct   - number of product
	'**%    npolicy    - number of policy
	'**%    ncertif    - number of certificate
	'**%    deffecdate - effect date
	'%Objetivo: Verifica la existencia de un registro en la tabla "Theft" usando la clave de dicha tabla.
	'%Parámetros:
	'%    scertype    - Tipo de registro
	'%    nbranch     - Código del ramo
	'%    nproduct    - Código del producto
	'%    npolicy     - Código de la poliza
	'%    ncertif     - Código del certificado
	'%    deffecdate  - Fecha de efecto del registro
	Private Function IsExist(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date) As Boolean
		Dim lclsTheft As eRemoteDB.Execute
		Dim lintExist As Short
		
        lclsTheft = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valTheftExist'. Generated on 18/06/2004 01:51:52 p.m.
		With lclsTheft
			.StoredProcedure = "reaTheft_v"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		
		lclsTheft = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%    sCodispl   - code page
	'**%    nMainAction  -
	'**%    sAction    -
	'**%    scertype   - type of registry
	'**%    nbranch    - number of branch
	'**%    nproduct   - number of product
	'**%    npolicy    - number of policy
	'**%    ncertif    - number of certificate
	'**%    deffecdate - effect date
	'**%    ninsured   - percentage insured - first risk
	'**%    nemployees -  number of employees transporting the money and/or securities
	'**%    narea      - surveillance area (in m2)
	'**%    nvigilance - number of surveillance watchmen
	'**%    nBusinessty - Type of Business
	'**%    nCommergrp  - Commercial Group
	'**%    nCodkind    - Business Kind
	'**%    sDescBussi  - Specifical description of the business kind
	'**%    nConstCat   - Construction Category
	
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl    - code page
	'%    nMainAction -
	'%    sAction     -
	'%    scertype    - Tipo de registro
	'%    nbranch     - Código del ramo
	'%    nproduct    - Código del producto
	'%    npolicy     - Código de la poliza
	'%    ncertif     - Código del certificado
	'%    deffecdate  - Fecha de efecto del registro
	'%    ninsured    - Porcentaje asegurado por primer riesgo.
	'%    nemployees  - Cantidad de empleados usados para el transporte de valores
	'%    narea       - Area de vigilancia ( metros cuadrados )
	'%    nvigilance  - Cantidad de vigilantes para los bienes asegurados.
	'%    nBusinessty - Tipo de negocio
	'%    nCommergrp  - Grupo comercial
	'%    nCodkind    - Giro de negocio
	'%    sDescBussi  - Descripción especifica del giro de negocio
	'%    nConstCat   - Categoría de construcción
	Public Function InsValRO001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date, ByVal nInsured As Integer, ByVal nEmployees As Short, ByVal nArea As Short, ByVal nVigilance As Short, ByVal nBusinessty As Short, ByVal nCommergrp As Short, ByVal nCodkind As Short, ByVal sDescBussi As String, ByVal nConstCat As Short) As String
		Dim lclsErrors As eFunctions.Errors
		
        lclsErrors = New eFunctions.Errors
		
		'+ Valida que este lleno el Tipo - 94020
		If nBusinessty = 0 Or nBusinessty = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 94020)
		End If
		
		'+ Valida que este lleno el Grupo - 94021
		If nCommergrp = 0 Or nCommergrp = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 94021)
		End If
		
		'+ Valida que este lleno el Giro - 94022
		If nCodkind = eRemoteDB.Constants.intNull Or nCodkind = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 94022)
		End If
		
		'+ Valida que se haya ingresado un Tipo de Construcción - 94098
		If nConstCat = eRemoteDB.Constants.intNull Or nConstCat = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 94098)
		End If
		
		'If (nUbication = 0 Or _
		''    nUbication = NumNull) Then
		'    Call lclsErrors.ErrorMessage(sCodispl, 3483)
		'End If
		
		'If (nCategory = 0 Or _
		''    nCategory = NumNull) Then
		'    Call lclsErrors.ErrorMessage(sCodispl, 3505)
		'End If
		
		'If (nRiskClass = 0 Or _
		''    nRiskClass = NumNull) Then
		'    Call lclsErrors.ErrorMessage(sCodispl, 3507)
		'End If
		
		'***Validación si la Clasificacion de Tipo de Construccion es X **********
		' Se incluira una busqueda de la clasificacion
		' Si es X se deberá validar que se cuente con la aprobación del supervisor
		'*************************************************************************
		
		If (nInsured = 0 Or nInsured = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 3509)
		Else
			If nInsured > 100 Then
				Call lclsErrors.ErrorMessage(sCodispl, 3992)
			End If
		End If
		
		If (nVigilance = 0 Or nVigilance = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 3512)
		End If
		
		If (nEmployees = 0 Or nEmployees = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 3513)
		End If
		
		If (nArea = 0 Or nArea = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 3511)
		End If
		
		InsValRO001 = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'**%Parameters:
	'**%    sCodispl   - code page
	'**%    nMainAction  -
	'**%    sAction    -
	'**%    nUsercode  - user code
	'**%    scertype   - type of registry
	'**%    nbranch    - number of branch
	'**%    nproduct   - number of product
	'**%    npolicy    - number of policy
	'**%    ncertif    - number of certificate
	'**%    deffecdate - effect date
	'**%    ninsured   - percentage insured - first risk
	'**%    nemployees -  number of employees transporting the money and/or securities
	'**%    narea      - surveillance area (in m2)
	'**%    nvigilance - number of surveillance watchmen
	'**%    nBusinessty - Type of Business
	'**%    nCommergrp  - Commercial Group
	'**%    nCodkind    - Business Kind
	'**%    sDescBussi  - Specifical description of the business kind
	'**%    nConstCat   - Construction Category
	
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%    sCodispl    - code page
	'%    nMainAction -
	'%    sAction     -
	'%    nUsercode   - Código de usuario
	'%    scertype    - Tipo de registro
	'%    nbranch     - Código del ramo
	'%    nproduct    - Código del producto
	'%    npolicy     - Código de la poliza
	'%    ncertif     - Código del certificado
	'%    deffecdate  - Fecha de efecto del registro
	'%    ninsured    - Porcentaje asegurado por primer riesgo.
	'%    nemployees  - Cantidad de empleados usados para el transporte de valores
	'%    narea       - Area de vigilancia ( metros cuadrados )
	'%    nvigilance  -  Cantidad de vigilantes para los bienes asegurados.
	'%    nBusinessty - Tipo de negocio
	'%    nCommergrp  - Grupo comercial
	'%    nCodkind    - Giro de negocio
	'%    sDescBussi  - Descripción especifica del giro de negocio
	'%    nConstCat   - Categoría de construcción
	Public Function InsPostRO001(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUserCode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecDate As Date, ByVal nInsured As Short, ByVal nEmployees As Short, ByVal nArea As Short, ByVal nVigilance As Short, ByVal nBusinessty As Short, ByVal nCommergrp As Short, ByVal nCodkind As Short, ByVal sDescBussi As String, ByVal nConstCat As Short, ByVal nProctype As Short) As Boolean
		
		Dim lclsPolicyWin As ePolicy.Policy_Win
		Dim lclsBusinessFun As Object
		Dim lstrComplCod As String
		
        If sAction = "Del" Then
            InsPostRO001 = Delete(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecDate, nUserCode)
        Else
            '+Construye el código completo del giro: (1 carac.) nBusinessTy + (3 carac.) nCommerGrp + (2 carac.) nCodKind
            lclsBusinessFun = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.Business_Functs")
            lstrComplCod = lclsBusinessFun.calComplCode(nCodkind, nBusinessty, nCommergrp)

            InsPostRO001 = Update(nUserCode, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecDate, nInsured, nEmployees, nArea, nVigilance, sCodispl, lstrComplCod, sDescBussi, nConstCat, nCodkind, nBusinessty, nCommergrp, nProctype)
            lclsBusinessFun = Nothing
        End If
		
		If InsPostRO001 Then
			lclsPolicyWin = New ePolicy.Policy_Win
			Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecDate, nUserCode, "RO001", "2")
			lclsPolicyWin = Nothing
		End If
		
		Exit Function
	End Function
End Class












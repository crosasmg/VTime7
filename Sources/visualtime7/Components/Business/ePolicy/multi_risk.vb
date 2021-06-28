Option Strict Off
Option Explicit On
Public Class multi_risk
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 3 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 3 $
	
	'**+Objective: Properties according to the table 'multi_risk' in the system 05/04/2005 05:23:00 p.m.
	'+Objetivo: Propiedades según la tabla 'multi_risk' en el sistema 05/04/2005 05:23:00 p.m.
	
	'+Objetivo: Tipo de registro.
	Public sCertype As String
	
	'+Objetivo: Código del ramo comercial.
	Public nBranch As Short
	
	'+Objetivo: Código del producto.
	Public nProduct As Short
	
	'+Objetivo: Número que identifica la póliza/cotización/propuesta.
	Public nPolicy As Integer
	
	'+Objetivo: Número del certificado.
	Public nCertif As Integer
	
	'+Objetivo: Codigo completo del tipo de negocio, grupo y codigo de giro de negocio
	Public sComplCod As String
	
	'+Objetivo: Descripcion del giro de negocio
	Public sDescBussi As String
	
	'+Objetivo: Codigo del tipo de construccion
	Public nConstcat As Integer
	
	'+ Variables para el giro del negocio
	Public nBusinessty As Short
	Public nCommergrp As Short
	Public nCodkind As Short
	
	'%Objetivo: Actualiza un registro a la tabla "multi_risk" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nUsercode - Código de usuario.
	'%    sCertype - Tipo de registro.
	'%    nBranch - Código del ramo comercial.
	'%    nProduct - Código del producto.
	'%    nPolicy - Número que identifica la póliza/cotización/propuesta.
	'%    nCertif - Número del certificado.
	'%    sComplcod - Indole del riesgo, para establecer tasa básica.
	'%    sDescBussi - Tipo de negocio.
	'%    nConstcat - Detalle del tipo de negocio.
	Private Function Update(ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal sComplCod As String, ByVal sDescBussi As String, ByVal nConstcat As Integer, ByVal nCodkind As Short, ByVal nBusinessty As Short, ByVal nCommergrp As Short) As Boolean
		Dim lclsmulti_risk As eRemoteDB.Execute
		

		lclsmulti_risk = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updmulti_risk'. Generated on 05/04/2005 05:23:00 p.m.
		With lclsmulti_risk
			.StoredProcedure = "insPostMU001"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sComplcod", sComplCod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescbussi", sDescBussi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConstcat", nConstcat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCodkind", nCodkind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBusinessty", nBusinessty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommergrp", nCommergrp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		lclsmulti_risk = Nothing
		
		Exit Function
	End Function
	
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl - Código de la transacción.
	'%    nMainAction - Número de la acción.
	'%    sAction - Acción a realizar.
	'%    sCertype - Tipo de registro.
	'%    nBranch - Código del ramo comercial.
	'%    nProduct - Código del producto.
	'%    nPolicy - Número que identifica la póliza/cotización/propuesta.
	'%    nCertif - Número del certificado.
	'%    sComplcod - Indole del riesgo, para establecer tasa básica.
	'%    sDescBussi - Tipo de negocio.
	'%    nConstcat - Detalle del tipo de negocio.
	Public Function InsValMU001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nBusinessty As Short, ByVal nCommergrp As Short, ByVal nCodkind As Short, ByVal nConstcat As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		

		lclsErrors = New eFunctions.Errors
		
		'+ Se valida que el campo codigo de tipo de negocio debe estar lleno
		If (nBusinessty = 0 Or nBusinessty = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 94020)
		End If
		
		'+ Se valida que el campo codigo de grupo comercial al que pertenece un giro de negocio debe estar lleno
		If (nCommergrp = 0 Or nCommergrp = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 94021)
		End If
		
		
		'+ Se valida que el campo giro de negocio debe estar lleno
		If (nCodkind = 0 Or nCodkind = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 94022)
		End If
		
		'+ Se valida que el campo tipo de construccion debe estar lleno
		If (nConstcat = 0 Or nConstcat = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 94098)
		End If
		
		InsValMU001 = lclsErrors.Confirm
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%    pblnHeader - Indicador, si encuentra en la cabecera de la página.
	'%    sCodispl - Código de la transacción.
	'%    nMainAction -
	'%    sAction - Acción a realizar.
	'%    nUsercode - Código de usuario.
	'%    sCertype - Tipo de registro.
	'%    nBranch - Código del ramo comercial.
	'%    nProduct - Código del producto.
	'%    nPolicy - Número que identifica la póliza/cotización/propuesta.
	'%    nCertif - Número del certificado.
	'%    sComplcod - Indole del riesgo, para establecer tasa básica.
	'%    sDescBussi - Tipo de negocio.
	'%    nConstcat - Detalle del tipo de negocio.
	Public Function InsPostMU001(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nBusinessty As Short, ByVal nCommergrp As Short, ByVal nCodkind As Short, ByVal sDescBussi As String, ByVal nConstcat As Integer) As Boolean
		Dim lclsPolicyWin As ePolicy.Policy_Win
		Dim lclsPolicyFun As Object
		Dim lComplCod As String
		

		'+ Obteniendo el campo sComplCod
		lclsPolicyFun = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.Business_Functs")
		lComplCod = lclsPolicyFun.calComplCode(nCodkind, nBusinessty, nCommergrp)
		
		InsPostMU001 = Update(nUsercode, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, lComplCod, sDescBussi, nConstcat, nCodkind, nBusinessty, nCommergrp)
		
		If InsPostMU001 Then
			lclsPolicyWin = New ePolicy.Policy_Win
			Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "MU001", "2")
			lclsPolicyWin = Nothing
		End If
		
		lclsPolicyFun = Nothing
		
		Exit Function
    End Function

    Public Function insPreMU700Upd_SI700(ByVal sCheked As String, _
                                           ByVal nUsercode As Double, _
                                            ByVal sCertype As String, _
                                            ByVal nBranch As Double, _
                                            ByVal nProduct As Double, _
                                            ByVal nPolicy As Double, _
                                            ByVal nCertif As Double, _
                                            ByVal nClaim As Double, _
                                            ByVal nCase_num As Double, _
                                            ByVal nType As Double, _
                                            Optional ByVal nSection As Double = 0, _
                                            Optional ByVal nConsec As Double = 0,
                                            Optional ByVal nElement_Type As Double = 0, _
                                            Optional ByVal sDescription As String = "", _
                                            Optional ByVal nCapital As Double = 0, _
                                            Optional ByVal strademark As String = "", _
                                            Optional ByVal sModel As String = "", _
                                            Optional ByVal nYear As Double = 0, _
                                            Optional ByVal sOrigin As String = "",
                                            Optional ByVal sSerialnumber As String = ""
                                ) As Boolean


        Dim lclsmulti_risk As eRemoteDB.Execute


        lclsmulti_risk = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updmulti_risk'. Generated on 05/04/2005 05:23:00 p.m.
        With lclsmulti_risk
            .StoredProcedure = "INSPOSTMULTI_DAMAGEUPD"

            .Parameters.Add("SCHEKED", sCheked, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NUSERCODE", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SCERTYPE", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NBRANCH", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPRODUCT", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPOLICY", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCERTIF", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCLAIM", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCASE_NUM", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NTYPE", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NSECTION", nSection, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCONSEC", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NELEMENT_TYPE", nElement_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SDESCRIPTION", sDescription, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCAPITAL", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("STRADEMARK", strademark, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SMODEL", sModel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NYEAR", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SORIGIN", sOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SSERIALNUMBER", sSerialnumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)



            insPreMU700Upd_SI700 = .Run(False)
        End With
        lclsmulti_risk = Nothing


        Exit Function
    End Function
End Class












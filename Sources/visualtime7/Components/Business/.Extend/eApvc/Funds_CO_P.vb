Option Strict Off
Option Explicit On
Public Class Funds_CO_P
	'%-------------------------------------------------------%'
	'% $Workfile:: Funds_CO_P.cls                            $%'
	'% $Author:: MVazquez                                    $%'
	'% $Date:: 3/07/06 7:39p                                $%'
	'% $Revision:: 21                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Objective: Type or Record. Sole values:     1-  Proposal     2 - Policy     3 - Quotation
	'-Objetivo: Tipo de registro. Valores únicos:    1 - Solicitud    2 - Póliza    3 - Cotización
	Public sCertype As String
	
	'**-Objective: Code of the Line of Business. The possible values as per table 10.
	'-Objetivo: Código del ramo comercial. Valores posibles según tabla 10.
	Public nBranch As Integer
	
	'**-Objective: Code of the product.
	'-Objetivo: Código del producto.
	Public nProduct As Integer
	
	'**-Objective: Number identifying the policy/ quotation/ proposal
	'-Objetivo: Número identificativo de la póliza/ cotización/ solicitud
	Public nPolicy As Double
	
	'**-Objective: Number identifying the Certificate
	'-Objetivo: Número identificativo del certificado
	Public nCertif As Double
	
	'**-Objective: Code of the investment fund
	'-Objetivo: Código del fondo de inversión
	Public nFunds As Integer
	
	'**-Objective: Date which from the record is valid.
	'-Objetivo: Fecha de efecto del registro.
	Public dEffecdate As Date
	
	'**-Objective: Date when the record is cancelled.
	'-Objetivo: Fecha de anulación del registro.
	Public dNulldate As Date
	
	'**-Objective: Percentage of share in the Fund
	'-Objetivo: Porcentaje de participación de la póliza, en el fondo.
	Public nParticip As Double
	Public nIntProy As Double
	Public nIntProyVar As Double
	Public nIntProyVarCle As Double
	
	
	'**-Objective: Redirection indicator Sole values     1 - Affirmative    2 - Negative
	'-Objetivo: Indicador de redirección de salida Valores únicos    1 - Afirmativo    2 - Negativo
	Public sReaddress As String
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
	Public nUsercode As Integer
	
	'**-Objective: Quantity of investment units available
	'-Objetivo: Cantidad disponible de unidades de inversión
	Public nAmount As Double
	Public nBuy_cost As Double
	Public nSell_cost As Double
	Public sActivFound As String
	Public sDescript As String
	Public sIndicator As String
	Public sApv As String
	Public nOrigin As Integer
	Public sPortafol As String
	Public nCount As Integer
	
	
	'**%Objective: Reads the quantity of modification in the funds of the policy.
	'%Objetivo: Permite leer la cantidad de veces que han sido modificados los fondos de la póliza.
	Public Function FindFundsModify(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Decimal
		Dim lrecreaFunds_CO_P_2 As Object
		
		On Error GoTo ErrorHandler
		lrecreaFunds_CO_P_2 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		With lrecreaFunds_CO_P_2
			.StoredProcedure = "reaFunds_CO_P_2"
			
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				
				'**+ Two redirection are one (Input and output)
				'+ Dos redirecciones conforman una sola (Entrada y salida)
				
				FindFundsModify = .FieldToClass("nModify")
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaFunds_CO_P_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds_CO_P_2 = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaFunds_CO_P_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds_CO_P_2 = Nothing
	End Function
	
	
	'%Objetivo: Permite leer la cantidad de cuentas que fueron seleccionadas.
	Public Function Count_Cuentas(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Decimal
		Dim lrecreaCount_Cuentas As Object
		
		On Error GoTo ErrorHandler
		lrecreaCount_Cuentas = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		Count_Cuentas = True
		
		With lrecreaCount_Cuentas
			.StoredProcedure = "REAFUNDS_CO_P_ACOUNTS"
			
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, defination.eRmtDataDir.rdbParamInputOutput, defination.eRmtDataType.rdbNumeric, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			
			Count_Cuentas = .Run
			
			If Count_Cuentas Then
				nCount = .Parameters("nCount").Value
				
				.RCloseRec()
			Else
				nCount = 0
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaCount_Cuentas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCount_Cuentas = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaCount_Cuentas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCount_Cuentas = Nothing
		
		Count_Cuentas = False
	End Function
	
	
	
	'**%Objective: Add an element in the table Funds_CO_P
	'%Objetivo: Permite registrar un elemento en la tabla Funds_CO_P
	Public Function Add() As Boolean
		Dim lreccreFunds_CO_P As Object
		
		On Error GoTo ErrorHandler
		lreccreFunds_CO_P = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		Add = True
		
		With lreccreFunds_CO_P
			.StoredProcedure = "creFunds_CO_P"
			
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParticip", nParticip, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbNumeric, 22, 2, 5, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReaddress", sReaddress, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sActivFound", sActivFound, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sApv", sApv, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntProy", nIntProy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbNumeric, 22, 2, 5, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntProyVar", nIntProyVar, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbNumeric, 22, 2, 5, defination.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreFunds_CO_P may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFunds_CO_P = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lreccreFunds_CO_P may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFunds_CO_P = Nothing
		
		Add = False
	End Function
	
	'**%Objective: Updates the percentage of participation of the policy in a fund
	'%Objetivo: Permite actualizar el porcentaje de participación de la póliza en el fondo
	Public Function Update() As Boolean
		Dim lrecupdFunds_CO_P As Object
		
		On Error GoTo ErrorHandler
		lrecupdFunds_CO_P = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		Update = True
		
		With lrecupdFunds_CO_P
			.StoredProcedure = "updFunds_CO_P"
			
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParticip", nParticip, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbNumeric, 22, 2, 5, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndicator", sIndicator, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReaddress", sReaddress, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sActivFound", sActivFound, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sApv", sApv, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntProy", nIntProy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbNumeric, 22, 2, 5, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntProyVar", nIntProyVar, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbNumeric, 22, 2, 5, defination.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdFunds_CO_P may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdFunds_CO_P = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecupdFunds_CO_P may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdFunds_CO_P = Nothing
		
		Update = False
	End Function
	
	'**%Objective: Deletes a fund related to the policy
	'%Objetivo: Permite eliminar un fondo asociado a una póliza
	Public Function Delete() As Boolean
		Dim lrecdelFunds_CO_P As Object
		
		On Error GoTo ErrorHandler
		lrecdelFunds_CO_P = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		Delete = True
		
		With lrecdelFunds_CO_P
			.StoredProcedure = "delFunds_CO_P"
			
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndicator", sIndicator, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sApv", sApv, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecdelFunds_CO_P may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelFunds_CO_P = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecdelFunds_CO_P may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelFunds_CO_P = Nothing
		
		Delete = False
	End Function
	
	
	'**%Objective: VI006A Page validations
	'%Objetivo: Función que permite efectuar las validaciones.
	Public Function insValVI006A(ByVal sCodispl As String, Optional ByVal sSelected As String = "", Optional ByVal sWindowType As String = "", Optional ByVal nFunds As Integer = 0, Optional ByVal nPartic_min As Integer = 0, Optional ByVal nParticip As Integer = 0, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal nTransaction As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sRedirection As String = "", Optional ByVal sActivFound As String = "", Optional ByVal nOrigin As Integer = 0, Optional ByVal nIntProy As Double = 0, Optional ByVal nIntProyVar As Double = 0) As String
		Dim lblnValVI006A As Boolean
		'    Dim lclsProduct    A
		'    Dim lclsProduct    As eProduct.Product
		'    Dim lclsProduct    As eProduct.Product
		' No borrar comentarios el soursafe se como caracteres, gracias.
		Dim lclsProduct As Object
		Dim lclsFunds_CO_P As Funds_CO_P
		Dim lclsFunds As Funds
		Dim lclsErrors As Object
		Dim lclsvalfield As Object
		Dim lcolFundss As Fundss
		Dim lcolFunds_CO_Ps As Funds_CO_Ps
		Dim lintParticip As Integer
		Dim lintReaddress As Integer
		Dim lbnlParticip As Boolean
		Dim lblnPartic_min As Boolean
		Dim lclsPolicy_Win As Object
		Dim lintCountCtas As Integer
		Dim lintCountCtasFunds As Integer
		Dim lclsTab_ord_origin As Object
		
		On Error GoTo insValVI006A_err
		
		lclsProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
		lclsFunds_CO_P = New Funds_CO_P
		lclsFunds = New Funds
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		lclsvalfield = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.valField")
		lcolFundss = New Fundss
		lcolFunds_CO_Ps = New Funds_CO_Ps
		lclsPolicy_Win = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy_Win")
		
		lclsvalfield.objErr = lclsErrors
		
		lintParticip = 0
		lintReaddress = 0
		lblnValVI006A = True
		
		If sWindowType = "Popup" Then
			'+ Validación del campo " % Participación".
			'+ Si el fondo está seleccionado la partcipación debe estar llena
			If nParticip = defination.eConstNull.NumNull And sActivFound = "1" Then
				Call lclsErrors.ErrorMessage(sCodispl, 3402)
				lblnValVI006A = False
			Else
				With lclsvalfield
					.ErrEmpty = 1937
					.Min = 1
					.Max = 100
					.EqualMin = True
					.EqualMax = True
					.Descript = "Participación"
				End With
				
				If sActivFound = "1" Then
					If Not lclsvalfield.ValNumber(nParticip) Then
						lblnValVI006A = False
					End If
				End If
				If lblnValVI006A Then
					'            Else
					'                If sActivFound <> "1" Then
					'                    Call lclsErrors.ErrorMessage(sCodispl, 3402)
					'                    lblnValVI006A = False
					'                Else
					If lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate) Then
						If lcolFundss.Find(nBranch, nProduct, dEffecdate) Then
							For	Each lclsFunds In lcolFundss
								With lclsFunds
									If .nFunds = nFunds And .nOrigin = nOrigin Then
										If .nParticip <> nParticip Then
											If lclsProduct.sUlfchani = "2" Then
												lbnlParticip = True
												lblnValVI006A = False
											End If
										End If
										
										If .nPartic_min > nParticip And sActivFound = "1" Then
											lblnPartic_min = True
											lblnValVI006A = False
										End If
									End If
								End With
							Next lclsFunds
						End If
						
						If Not lbnlParticip Then
							If sRedirection = "1" Then
								If lcolFunds_CO_Ps.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nOrigin) Then
									For	Each lclsFunds_CO_P In lcolFunds_CO_Ps
										With lclsFunds_CO_P
											If .nFunds = nFunds Then
												If .nParticip <> nParticip Then
													lintReaddress = lintReaddress + 1
												End If
												
												If lclsProduct.nUlrmaxqu < .FindFundsModify(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) + lintReaddress And (nTransaction = defination.PolTransac.clngTempCertifAmendment Or nTransaction = defination.PolTransac.clngTempPolicyAmendment Or nTransaction = defination.PolTransac.clngCertifAmendment Or nTransaction = defination.PolTransac.clngPolicyAmendment) Then
													
													'**+ More redirection permitted by the policy in the product designer must not be permitted
													'+ No debe aceptar más redirecciones de las permitidas por póliza en el diseñador de productos
													
													Call lclsErrors.ErrorMessage(sCodispl, 17008)
													
													lblnValVI006A = True
												End If
											End If
										End With
									Next lclsFunds_CO_P
								End If
							End If
						End If
					End If
					'                End If
				End If
			End If
			
			'**+ The percentage of participation must not be inferior to the minimum defined
			'**+ in the product designer
			'+ El porcentaje de participación no puede ser menor que el mínimo definido en
			'+ el diseñador de productos
			
			If lblnPartic_min Then
				Call lclsErrors.ErrorMessage(sCodispl, 17004)
			End If
			
			'**+ If in the product designer was specify that fund must not be modified
			'+ Si en el diseñador de productos se especificó que no se pueden cambiar
			'+ los fondos, no se puede cambiar este
			If lbnlParticip Then
				Call lclsErrors.ErrorMessage(sCodispl, 11128)
			End If
		Else
			lclsTab_ord_origin = eRemoteDB.NetHelper.CreateClassInstance("eBranches.Tab_ord_origins")
			Call lclsTab_ord_origin.Find(nBranch, nProduct)
			lintCountCtas = lclsTab_ord_origin.Count
			
			Call lclsFunds_CO_P.Count_Cuentas(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
			lintCountCtasFunds = lclsFunds_CO_P.nCount
			
			'If lintCountCtas <> lintCountCtasFunds Then
			'   Call lclsErrors.ErrorMessage(sCodispl, 767092)
			'End If
			
			If sSelected = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 11084)
			Else
				If lcolFunds_CO_Ps.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, defination.eConstNull.NumNull) Then
					For	Each lclsFunds_CO_P In lcolFunds_CO_Ps
						With lclsFunds_CO_P
							lintParticip = lintParticip + .nParticip
						End With
					Next lclsFunds_CO_P
					
					If sWindowType <> "NormalDel" Then
						If lintParticip <> 100 * lintCountCtas Then
							Call lclsErrors.ErrorMessage(sCodispl, 3070)
							If sCodispl = "VI006A" Then
								Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sCodispl, "1")
							End If
						End If
					End If
				End If
			End If
		End If
		
		insValVI006A = lclsErrors.Confirm
		
insValVI006A_err: 
		If Err.Number Then
			insValVI006A = "insValVI006A: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsFunds_CO_P may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFunds_CO_P = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsvalfield may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalfield = Nothing
		'UPGRADE_NOTE: Object lcolFundss may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFundss = Nothing
		'UPGRADE_NOTE: Object lcolFunds_CO_Ps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFunds_CO_Ps = Nothing
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
		'UPGRADE_NOTE: Object lclsTab_ord_origin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_ord_origin = Nothing
	End Function
	
	'**%Objective: Updates the information in the frame VI006A
	'%Objetivo: Permite actualizar los datos del frame VI006A
	Public Function insPostVI006A(ByVal sCodispl As String, ByVal sAction As String, ByVal nFunds As Integer, ByVal nParticip As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal sActivFound As String, Optional ByVal sRedirection As String = "", Optional ByVal nOrigin As Integer = 0, Optional ByVal nIntProy As Double = 0, Optional ByVal nIntProyVar As Double = 0) As Boolean
		Dim lintReaddress As Decimal
		Dim lintAuxParticip As Integer
		Dim lclsFunds_CO_Ps As Funds_CO_Ps
		Dim lclsUl_Move_Acc_pol As Object
		Dim lclsCurrent_pol As Object
		Dim lclsPolicy As Object
		Dim lclsProduct As Object
		
		On Error GoTo insPostVI006A_err
		
		lclsFunds_CO_Ps = New Funds_CO_Ps
		lclsUl_Move_Acc_pol = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.ul_Move_Acc_pol")
		lclsCurrent_pol = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Curren_pol")
		lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		lclsProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
		
		insPostVI006A = True
		
		Call lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate)
		Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)
		Call lclsFunds_CO_Ps.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nOrigin)
		
		With Me
			
			'**+ If is an underwritten, recovery o normal modification in the same day
			'+ Si es emisión, recuperación o modificación normal el mismo dia
			
			If (nTransaction = defination.PolTransac.clngPolicyIssue Or nTransaction = defination.PolTransac.clngPolicyProposal Or nTransaction = defination.PolTransac.clngPolicyQuotation Or nTransaction = defination.PolTransac.clngPolicyReissue Or nTransaction = defination.PolTransac.clngCertifIssue Or nTransaction = defination.PolTransac.clngCertifProposal Or nTransaction = defination.PolTransac.clngCertifQuotation Or nTransaction = defination.PolTransac.clngCertifReissue Or nTransaction = defination.PolTransac.clngRecuperation) Or ((nTransaction = defination.PolTransac.clngCertifAmendment Or nTransaction = defination.PolTransac.clngPolicyAmendment) And dEffecdate = lclsPolicy.dStartdate) Then
				.dNulldate = System.Date.FromOADate(defination.eConstNull.dtmNull)
				.sIndicator = "1"
			Else
				
				'**+ If is a normal modification in the different day
				'+ Si es modificación normal a diferente dia
				
				If nTransaction = defination.PolTransac.clngPolicyAmendment Or nTransaction = defination.PolTransac.clngCertifAmendment Then
					.dNulldate = System.Date.FromOADate(defination.eConstNull.dtmNull)
					.sIndicator = "2"
				Else
					
					'**+ If is a temporary modification in the same day
					'+ Si es modificación temporal al mismo dia
					
					If (nTransaction = defination.PolTransac.clngTempCertifAmendment Or nTransaction = defination.PolTransac.clngTempPolicyAmendment) And dEffecdate = lclsPolicy.dStartdate Then
						.dNulldate = dNulldate
						.sIndicator = "4"
					Else
						
						'**+ If is a temporary modification in the different day
						'+ Si es modificación temporal a diferente dia
						
						If nTransaction = defination.PolTransac.clngTempCertifAmendment Or nTransaction = defination.PolTransac.clngTempPolicyAmendment Then
							.dNulldate = dNulldate
							.sIndicator = "3"
						End If
					End If
				End If
			End If
			
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.dEffecdate = dEffecdate
			.nFunds = nFunds
			.nParticip = IIf(nParticip = defination.eConstNull.NumNull, 0, nParticip)
			.nUsercode = nUsercode
			.sActivFound = sActivFound
			.sApv = lclsProduct.sApv
			.nOrigin = nOrigin
			.nIntProy = IIf(nIntProy = defination.eConstNull.NumNull, 0, nIntProy)
			.nIntProyVar = IIf(nIntProyVar = defination.eConstNull.NumNull, 0, nIntProyVar)
			
			If sAction <> "Del" Then
				If Not lclsFunds_CO_Ps.FindItem(nFunds, nParticip, nOrigin, nIntProy, nIntProyVar) Then
					.sReaddress = "0"
					.Add()
				Else
					.sReaddress = "0"
					
					If sRedirection = "1" Then
						If (nTransaction = defination.PolTransac.clngCertifAmendment Or nTransaction = defination.PolTransac.clngPolicyAmendment Or nTransaction = defination.PolTransac.clngTempCertifAmendment Or nTransaction = defination.PolTransac.clngTempPolicyAmendment) Then
							.sReaddress = "1"
						End If
					End If
					
					.Update()
				End If
			Else
				.Delete()
			End If
		End With
		
		If sRedirection = "1" Then
			If (nTransaction = defination.PolTransac.clngCertifAmendment Or nTransaction = defination.PolTransac.clngPolicyAmendment Or nTransaction = defination.PolTransac.clngTempCertifAmendment Or nTransaction = defination.PolTransac.clngTempPolicyAmendment) Then
				lintReaddress = FindFundsModify(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
				
				If lintReaddress > lclsProduct.nUlrschar Or Not insPeriodFree(nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
					With lclsUl_Move_Acc_pol
						.sCertype = sCertype
						.nBranch = nBranch
						.nProduct = nProduct
						.nPolicy = nPolicy
						.nCertif = nCertif
						
						If lclsCurrent_pol.Find(nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate) Then
							Call lclsCurrent_pol.Val_Curren_pol(0)
							
							.nCurrency = lclsCurrent_pol.nCurrency
						Else
							.nCurrency = "1"
						End If
						
						.dOperDate = dEffecdate
						.nType_Move = 15
						.nIdconsec = 0
						
						.nOutAmount = lclsProduct.nUlrcharg
						.nUsercode = nUsercode
						.nReceipt = defination.eConstNull.NumNull
						.sPayer = String.Empty
						.nInstitution = 1
						.nIntermei = 2
						.nOrigin = defination.eConstNull.NumNull
						.dDate_Origin = dEffecdate
						.nInvested = 2
						.dPosted = defination.eConstNull.dtmNull
						.nLed_Compan = defination.eConstNull.NumNull
						.sAccount = String.Empty
						.sAux_Accoun = String.Empty
						
						insPostVI006A = .insApplyChargeRedi
					End With
				End If
				
				With lclsUl_Move_Acc_pol
					.sCertype = sCertype
					.nBranch = nBranch
					.nProduct = nProduct
					.nPolicy = nPolicy
					.nCertif = nCertif
					.nUsercode = nUsercode
					.dOperDate = dEffecdate
					insPostVI006A = .insApplyRediHis
				End With
			End If
		End If
		
insPostVI006A_err: 
		If Err.Number Then
			insPostVI006A = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsFunds_CO_Ps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFunds_CO_Ps = Nothing
		'UPGRADE_NOTE: Object lclsUl_Move_Acc_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsUl_Move_Acc_pol = Nothing
		'UPGRADE_NOTE: Object lclsCurrent_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurrent_pol = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		
	End Function
	
	'**%Objective: This function return true if the effective date of the
	'**%           operation is between the free charges period
	'%Objetivo: Función que retorna verdadero si la fecha efectiva de
	'%          la transacción está contemplada dentro del periodo libre de cargo
	Public Function insPeriodFree(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lclsProduct As Object
		Dim lclsPolicy As Object
		Dim lclsCertificat As Object
		Dim mresulDate As Date
		Dim ldtmDate As Date
		
		On Error GoTo ErrorHandler
		insPeriodFree = True
		
		lclsProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
		lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
		
		With lclsProduct
			Call .FindProduct_li(nBranch, nProduct, dEffecdate)
			
			If nCertif = 0 Then
				Call lclsPolicy.Find("2", nBranch, nProduct, nPolicy)
				
				ldtmDate = lclsPolicy.dDate_Origi
			Else
				Call lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif)
				
				ldtmDate = lclsCertificat.dDate_Origi
			End If
			
			If .nUlredper <> 0 And .nUlredper <> defination.eConstNull.NumNull Then
				Select Case .nUlredper
					
					'**+ It adds the charge frecuency to the date origin of the policy/certificate
					'+ Se le suma la frecuencia a la fecha de efecto de la poliza/certificado
					
					Case Funds.ePayFrecuency.esdMonthly
						mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, ldtmDate)
						
					Case Funds.ePayFrecuency.esdAnualy
						mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 12, ldtmDate)
						
					Case Funds.ePayFrecuency.esdSemestral
						mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 6, ldtmDate)
						
					Case Funds.ePayFrecuency.esdTrimestral
						mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 3, ldtmDate)
						
					Case Funds.ePayFrecuency.esdBiMestral
						mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 2, ldtmDate)
						
					Case Else
						mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, ldtmDate)
				End Select
				
				'**+ if the result date is inferior to the transacton date
				'**+ then the redirection cost will no be collected
				'+ Si la fecha resultante es menor que la fecha de la transacción
				'+ entonces no se cobrara el costo por redirección
				
				If mresulDate < dEffecdate Then
					insPeriodFree = False
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		
		insPeriodFree = False
	End Function
	
	
	'**%Objective: calculates the available amount to buy units
	'%Objetivo: Calcula el importe disponible para calcular unidades
	Public Function insCalAvailable(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nOrigin As Integer) As Decimal
		Dim lreccreFunds As Object
		On Error GoTo ErrorHandler
		
		lreccreFunds = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		insCalAvailable = 0
		
		With lreccreFunds
			.StoredProcedure = "INS_CAL_POL_ACC_BALANCE_1"
			
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nResult", 0, defination.eRmtDataDir.rdbParamInputOutput, defination.eRmtDataType.rdbNumeric, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nError", 0, defination.eRmtDataDir.rdbParamInputOutput, defination.eRmtDataType.rdbNumeric, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", 0, defination.eRmtDataDir.rdbParamInputOutput, defination.eRmtDataType.rdbNumeric, 22, 2, 10, defination.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters("nError").Value = 0 Then
					insCalAvailable = .Parameters("nBalance").Value
				Else
					insCalAvailable = 0
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lreccreFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFunds = Nothing
		
		Exit Function
		
ErrorHandler: 
		'UPGRADE_NOTE: Object lreccreFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFunds = Nothing
		insCalAvailable = 0
	End Function
	
	'**%Objective: calculates the available amount to buy units
	'%Objetivo: Calcula el importe disponible para calcular unidades
	Public Function insCalAvailable_Contrib(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nOrigin As Integer) As Decimal
		Dim lreccreFunds As Object
		
		On Error GoTo ErrorHandler
		
		lreccreFunds = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		insCalAvailable_Contrib = 0
		
		With lreccreFunds
			.StoredProcedure = "INS_CAL_POL_CONTR_EXT"
			
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", 0, defination.eRmtDataDir.rdbParamInputOutput, defination.eRmtDataType.rdbNumeric, 22, 2, 10, defination.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insCalAvailable_Contrib = .Parameters("nBalance").Value
			Else
				insCalAvailable_Contrib = 0
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lreccreFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFunds = Nothing
		
		Exit Function
		
ErrorHandler: 
		'UPGRADE_NOTE: Object lreccreFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFunds = Nothing
		insCalAvailable_Contrib = 0
	End Function
	
	'**%Objective: calculates the available amount to buy units
	'%Objetivo: Calcula el importe disponible para calcular unidades
	Public Function insCalPolAccBalance(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCurrency As Double, ByVal nOrigin As Integer) As Decimal
		Dim lrecreaFunds_CO_P As Object
		
		On Error GoTo insCalPolAccBalance_err
		
		lrecreaFunds_CO_P = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		insCalPolAccBalance = 0
		
		With lrecreaFunds_CO_P
			.StoredProcedure = "insCalPolAccBalance"
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nResult", 0, defination.eRmtDataDir.rdbParamInputOutput, defination.eRmtDataType.rdbNumeric, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nError", 0, defination.eRmtDataDir.rdbParamInputOutput, defination.eRmtDataType.rdbNumeric, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", 0, defination.eRmtDataDir.rdbParamInputOutput, defination.eRmtDataType.rdbNumeric, 22, 2, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBal_Saving", 0, defination.eRmtDataDir.rdbParamInputOutput, defination.eRmtDataType.rdbNumeric, 22, 2, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBal_Units", 0, defination.eRmtDataDir.rdbParamInputOutput, defination.eRmtDataType.rdbNumeric, 22, 2, 10, defination.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters("nError").Value = 0 Then
					insCalPolAccBalance = .Parameters("nBalance").Value
				Else
					insCalPolAccBalance = 0
				End If
			End If
		End With
		
insCalPolAccBalance_err: 
		If Err.Number Then
			insCalPolAccBalance = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaFunds_CO_P may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds_CO_P = Nothing
	End Function
End Class







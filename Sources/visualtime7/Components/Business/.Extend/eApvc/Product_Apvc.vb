Option Strict Off
Option Explicit On
Public Class Product_Apvc
	'**+Objective: Class that supports the table Funds
	'**+           it's content is: Associated investment funds to a product  A record for every fund allowed in the product
	'**+Version: $$Revision: $
	'+Objetivo: Clase que le da soporte a la tabla Funds
	'+          cuyo contenido es: Fondos de inversión asociados a un producto  Un registro por cada fondo de inversión permitido en el producto
	'+Version: $$Revision: $
	'%-------------------------------------------------------%'
	'% $Workfile::                                          $%'
	'% $Author::                                            $%'
	'% $Date::                                              $%'
	'% $Revision::                                          $%'
	'%-------------------------------------------------------%'
	
	
	Public nBranch As Integer '  NBRANCH     NUMBER(5)                         NOT NULL,
	Public nProduct As Integer '  NPRODUCT    NUMBER(5)                         NOT NULL,
	Public dEffecdate As Date '  DEFFECDATE  DATE                              NOT NULL,
	Public dNulldate As Date '  DNULLDATE   DATE,
	Public nMaxamount As Integer '  NMAXAMOUNT  NUMBER(18,6),
	Public nPermin As Double '  NPERMIN     NUMBER(5,2),
	Public nMonthmin As Integer '  NMONTHMIN   NUMBER(5),
	Public nMinstay As Integer '  NMAXSTAY    NUMBER(5),
	Public nPercentnprem As Double 'NPERCENT           NUMBER(9,6),
	Public nPrem_max As Integer 'NPREMPMAX          NUMBER(18,6),
	Public nPrem_min As Integer
	Public nCurrency As Integer
	Public nUsercode As Integer '  NUSERCODE   NUMBER(5),
	Public nPercentsalary As Double
	Public nAmountnprem As Integer
	'%Objetivo: busca informacion de la tabla de producto para el apvc
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaProduct As Object
		On Error GoTo Find_Err
		lrecReaProduct = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		With lrecReaProduct
			.StoredProcedure = "REAPRODUCT_APVC"
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.dNulldate = .FieldToClass("DNULLDATE")
				Me.nMaxamount = .FieldToClass("NMAXAMOUNT")
				Me.nPermin = .FieldToClass("NPERMIN")
				Me.nMonthmin = .FieldToClass("nMonthmin")
				Me.nMinstay = .FieldToClass("nMinstay")
				Me.nUsercode = .FieldToClass("nusercode")
				Me.nPercentnprem = .FieldToClass("nPercentnprem")
				Me.nPrem_max = .FieldToClass("nPrem_max")
				Me.nPrem_min = .FieldToClass("nPrem_min")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.nPercentsalary = .FieldToClass("npercentsalary")
				Me.nAmountnprem = .FieldToClass("nAmountnprem")
				
				.RCloseRec()
				Find = True
			End If
		End With
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaProduct = Nothing
	End Function
	
	'%Objetivo: Realizar el post de la transacion dp200 product apvc
	Public Function insPostDP200(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nMaxamount As Integer, ByVal nPermin As Double, ByVal nMonthmin As Integer, ByVal nMinstay As Integer, ByVal nPercentnprem As Double, ByVal nPrem_max As Integer, ByVal nPrem_min As Integer, ByVal nCurrency As Integer, ByVal nUsercode As Integer, ByVal nPercentsalary As Double, ByVal nAmountnprem As Integer) As Boolean
		Dim lclsProd_win As Object
		
		On Error GoTo insPostDP200_Err
		
		Dim lrecinsPostDP200 As Object
		
		lrecinsPostDP200 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		With lrecinsPostDP200
			.StoredProcedure = "INSDP200PKG.insPOSTDP200"
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxamount", nMaxamount, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPermin", nPermin, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbNumeric, 22, 6, 18, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthmin", nMonthmin, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinstay", nMinstay, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercentnprem", nPercentnprem, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_max", nPrem_max, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_min", nPrem_min, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nusercode", nUsercode, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("npercentsalary", nPercentsalary, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountnprem", nAmountnprem, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			
			insPostDP200 = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostDP200 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostDP200 = Nothing
		
		
		If insPostDP200 Then
			lclsProd_win = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Prod_win")
			'+ Se actualiza la secuencia de ventana del producto con la transacción enviada como parametro.
			insPostDP200 = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP200", "2", nUsercode)
		End If
		
insPostDP200_Err: 
		If Err.Number Then
			insPostDP200 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
	End Function
	
	
	
	Public Function insValDP200(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nMaxamount As Integer, ByVal nPermin As Double, ByVal nMonthmin As Integer, ByVal nMinstay As Integer, ByVal nPercentnprem As Double, ByVal nPrem_max As Integer, ByVal nPrem_min As Integer, ByVal nCurrency As Integer, ByVal nUsercode As Integer, ByVal nPercentsalary As Integer, ByVal nAmountnprem As Integer) As String
		Dim lrecinsinsValDP200 As Object
		Dim lstrErrors As String
		Dim lclsErrors As Object
		
		On Error GoTo insValDP200_err
		
		lrecinsinsValDP200 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		With lrecinsinsValDP200
			.StoredProcedure = "INSDP200PKG.insValDP200"
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxamount", nMaxamount, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPermin", nPermin, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbNumeric, 22, 6, 18, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthmin", nMonthmin, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinstay", nMinstay, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercentnprem", nPercentnprem, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_max", nPrem_max, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_min", nPrem_min, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nusercode", nUsercode, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("npercentsalary", nPercentsalary, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountnprem", nAmountnprem, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, defination.eRmtDataDir.rdbParamOutput, defination.eRmtDataType.rdbVarChar, 4000, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrErrors = .Parameters("Arrayerrors").Value
		End With
		lclsErrors.ErrorMessage("DP200",  ,  ,  ,  ,  , lstrErrors)
		insValDP200 = lclsErrors.Confirm
		
insValDP200_err: 
		If Err.Number Then
			insValDP200 = "insValDP200: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lrecinsinsValDP200 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsinsValDP200 = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
End Class







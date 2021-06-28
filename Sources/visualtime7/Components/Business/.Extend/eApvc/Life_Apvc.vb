Option Strict Off
Option Explicit On
Public Class Life_Apvc
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
	
	
	Public sCertype As String 'SCERTYPE           CHAR(1 BYTE)               NOT NULL,
	Public nProduct As Integer 'NPRODUCT           NUMBER(5)                  NOT NULL,
	Public nBranch As Integer 'NBRANCH            NUMBER(5)                  NOT NULL,
	Public nPolicy As Double 'NPOLICY            NUMBER(10)                 NOT NULL,
	Public nCertif As Double 'NCERTIF            NUMBER(10)                 NOT NULL,
	Public dEffecdate As Date 'DEFFECDATE         DATE                       NOT NULL,
	Public nPercentsalary As Double 'NPERCENT           NUMBER(9,6),
	Public nPrem_max As Double 'NPREMPMAX          NUMBER(18,6),
	Public nPrem_min As Double 'NPREM_MIN          NUMBER(18,6),
	Public sPremium As String 'SPREMIUM           CHAR(1 BYTE),
	Public nMinstay As Integer 'nMinstay        NUMBER(5),
	Public nTyp_profit As Integer 'NTYP_PROFIT        NUMBER(5),
	Public nBankext As Integer 'NBANKEXT           NUMBER(5),
	Public sAccount As String 'SACCOUNT           CHAR(25 BYTE),
	Public nPremiumc As Integer 'NPREMIUMC          NUMBER(18,6),
	Public nPercentiumc As Double 'NPERCENTIUMC       NUMBER(9,6),
	Public nTyp_profitworker As Integer 'NTYP_PROFITWORKER  NUMBER(5),
	Public dNulldate As Date 'DNULLDATE          DATE,
	Public nUsercode As Integer 'NUSERCODE          NUMBER(5),
	Public nCurrencyempl As Integer
	Public nAmountnprem As Double
	Public nStay As Integer
	Public nAmountsalary As Double
	Public nPercentnprem As Double
	Public nCurrencywork As Integer
	Public nTyp_acc As Integer
	
	
	
	'**%Objective: Reads the actives funds related to line of business - Product
	'%Objetivo: Lee todos los fondos activos asociados a un Ramo - Producto
	
	'**%Objective: Adds an element in to table Funds
	'%Objetivo: Permite registrar un elemento en la tabla Funds
	
	
	'**%Objective: Updates the record in the table Funds
	'%Objetivo: Permite actualizar un registro en la tabla Funds
	
	
	
	
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaProduct As Object
		
		On Error GoTo Find_Err
		lrecReaProduct = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		With lrecReaProduct
			.StoredProcedure = "REALIFE_APVC"
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.sCertype = .FieldToClass("sCertype")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nCertif = .FieldToClass("nCertif")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.nPercentsalary = .FieldToClass("npercentsalary")
				Me.nPrem_max = .FieldToClass("nPrem_max")
				Me.nPrem_min = .FieldToClass("nPrem_min")
				Me.sPremium = .FieldToClass("sPremium")
				Me.nMinstay = .FieldToClass("nMinstay")
				Me.nTyp_profit = .FieldToClass("nTyp_profit")
				Me.nBankext = .FieldToClass("nBankext")
				Me.sAccount = .FieldToClass("sAccount")
				Me.nPremiumc = .FieldToClass("nPremiumc")
				Me.nPercentiumc = .FieldToClass("nPercentiumc")
				Me.nTyp_profitworker = .FieldToClass("nTyp_profitworker")
				Me.dNulldate = .FieldToClass("dNulldate")
				Me.nUsercode = .FieldToClass("nusercode")
				Me.nCurrencyempl = .FieldToClass("nCurrencyempl")
				Me.nAmountnprem = .FieldToClass("nAmountnprem")
				Me.nStay = .FieldToClass("nStay")
				Me.nAmountsalary = .FieldToClass("nAmountsalary")
				Me.nPercentnprem = .FieldToClass("nPercentnprem")
				Me.nCurrencywork = .FieldToClass("nCurrencywork")
				Me.nPercentnprem = .FieldToClass("nPercentnprem")
				Me.nTyp_acc = .FieldToClass("nTyp_acc")
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
	
	
	
	
	
	
	
	
	
	
	'%Objetivo: Rutina para calcular los valores y nro de Unidades totales de un fondo
	'------------------------------------
	
	'%Objetivo: Realizar el post de la transacion ca200 product apvc
	
	Public Function insPostCA200(ByVal nAction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nPercentsalary As Double, ByVal nPrem_max As Double, ByVal nPrem_min As Double, ByVal sPremium As String, ByVal nMinstay As Integer, ByVal nTyp_profit As Integer, ByVal nBankext As Integer, ByVal sAccount As String, ByVal nPremiumc As Integer, ByVal nPercentiumc As Double, ByVal nTyp_profitworker As Integer, ByVal nUsercode As Integer, ByVal nCurrencyempl As Integer, ByVal nAmountnprem As Double, ByVal nStay As Integer, ByVal nAmountsalary As Double, ByVal nPercentnprem As Double, ByVal nCurrencywork As Integer, ByVal nTyp_acc As Integer, ByVal nOption As Integer) As Boolean
		Dim lclsProd_win As Object
		
		On Error GoTo insPostca200_Err
		
		Dim lrecinsPostca200 As Object
		
		lrecinsPostca200 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		With lrecinsPostca200
			.StoredProcedure = "INSCA200PKG.insPOSTCA200"
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_max", nPrem_max, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_min", nPrem_min, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPremium", sPremium, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinstay", nMinstay, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_profit", nTyp_profit, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankext", nBankext, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 25, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremiumc", nPremiumc, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("npercentsalary", nPercentsalary, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_profitworker", nTyp_profitworker, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nusercode", nUsercode, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencyempl", nCurrencyempl, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountnprem", nAmountnprem, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStay", nStay, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountsalary", nAmountsalary, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercentnprem", nPercentnprem, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencywork", nCurrencywork, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercentiumc", nPercentiumc, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acc", nTyp_acc, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			
			
			
			insPostCA200 = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostca200 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostca200 = Nothing
		
		
		
		
insPostca200_Err: 
		If Err.Number Then
			insPostCA200 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
	End Function
	
	
	
	Public Function insValCA200(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nPercentsalary As Double, ByVal nPrem_max As Double, ByVal nPrem_min As Double, ByVal sPremium As String, ByVal nMinstay As Integer, ByVal nTyp_profit As Integer, ByVal nBankext As Integer, ByVal sAccount As String, ByVal nPremiumc As Integer, ByVal nPercentiumc As Double, ByVal nTyp_profitworker As Integer, ByVal nUsercode As Integer, ByVal nCurrencyempl As Integer, ByVal nAmountnprem As Double, ByVal nStay As Integer, ByVal nAmountsalary As Double, ByVal nPercentnprem As Double, ByVal nCurrencywork As Integer, ByVal nTyp_acc As Integer, ByVal nOption As Integer) As String
		Dim lrecinsinsValca200 As Object
		Dim lstrErrors As String
		Dim lclsErrors As Object
		
		On Error GoTo insValca200_err
		
		lrecinsinsValca200 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		With lrecinsinsValca200
			.StoredProcedure = "INSca200PKG.insValca200"
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_max", nPrem_max, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_min", nPrem_min, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPremium", sPremium, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinstay", nMinstay, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_profit", nTyp_profit, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankext", nBankext, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 25, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremiumc", nPremiumc, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("npercentsalary", nPercentsalary, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_profitworker", nTyp_profitworker, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nusercode", nUsercode, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencyempl", nCurrencyempl, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountnprem", nAmountnprem, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStay", nStay, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountsalary", nAmountsalary, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercentnprem", nPercentnprem, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrencywork", nCurrencywork, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercentiumc", nPercentiumc, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acc", nTyp_acc, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, defination.eRmtDataDir.rdbParamOutput, defination.eRmtDataType.rdbVarChar, 4000, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrErrors = .Parameters("Arrayerrors").Value
		End With
		lclsErrors.ErrorMessage("ca200",  ,  ,  ,  ,  , lstrErrors)
		insValCA200 = lclsErrors.Confirm
		
insValca200_err: 
		If Err.Number Then
			insValCA200 = "insValca200: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lrecinsinsValca200 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsinsValca200 = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	
	
	' objetivo busa informacion para la ventana ca200
	
	Public Function FindCA200(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal bCertif As Boolean) As Boolean
		Dim lrecReaProduct As Object
		Dim lblnFind As Boolean
		Dim lobjProduct_Apvc As New Product_Apvc
		' busca infomaticion
		
		On Error GoTo Findca200_Err
		lblnFind = Me.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
		
		
		' si no encuentra registro vigente a la fecha y es certificado busca los datos de la poliza matriz
		If Not lblnFind And bCertif Then
			lblnFind = Me.Find(sCertype, nBranch, nProduct, nPolicy, 0, dEffecdate)
			
		ElseIf Not lblnFind And Not bCertif Then 
			lblnFind = lobjProduct_Apvc.Find(nBranch, nProduct, dEffecdate)
			' si no encutra registro y es poliza matriz muestra la informaicon del producto
			Me.nProduct = lobjProduct_Apvc.nProduct
			Me.nBranch = lobjProduct_Apvc.nBranch
			Me.nPercentsalary = lobjProduct_Apvc.nPercentsalary
			Me.nPrem_max = lobjProduct_Apvc.nPrem_max
			Me.nPrem_min = lobjProduct_Apvc.nPrem_min
			Me.nMinstay = lobjProduct_Apvc.nMinstay
			Me.nPercentnprem = lobjProduct_Apvc.nPercentnprem
			Me.nAmountnprem = lobjProduct_Apvc.nAmountnprem
			Me.nCurrencyempl = lobjProduct_Apvc.nCurrency
			
		End If
Findca200_Err: 
		If Err.Number Then
			FindCA200 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaProduct = Nothing
	End Function
	
	
	
	Public Function insValCA001(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sCodispl As String, ByVal nUsercode As Integer, ByVal nTransaction As Integer) As String
		Dim lrecinsinsValCA001 As Object
		Dim lstrErrors As String
		Dim lclsErrors As Object
		
		On Error GoTo insValCA001_err
		
		lrecinsinsValCA001 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		With lrecinsinsValCA001
			.StoredProcedure = "INSAPVCPKG.INSVALCA001"
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 8, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nusercode", nUsercode, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ntransaction", nTransaction, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, defination.eRmtDataDir.rdbParamOutput, defination.eRmtDataType.rdbVarChar, 4000, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrErrors = .Parameters("Arrayerrors").Value
		End With
		lclsErrors.ErrorMessage("CA001",  ,  ,  ,  ,  , lstrErrors)
		insValCA001 = lclsErrors.Confirm
		
insValCA001_err: 
		If Err.Number Then
			insValCA001 = "insValca001: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lrecinsinsValCA001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsinsValCA001 = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	
	Public Function CertificatQuantity(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Integer
		Dim lrecCertificatQuantity As Object
		On Error GoTo nCertificatQuantity_Err
		
		lrecCertificatQuantity = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		'+ Definición de parámetros para stored procedure 'insudb.insReaAuto_db1'
		'+ Información leída el 03/01/2001 2:32:45 p.m.
		With lrecCertificatQuantity
			.StoredProcedure = "INSAPVCPKG.REACERTIFICAT_QUANTITY1"
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuantity", 0, defination.eRmtDataDir.rdbParamInputOutput, defination.eRmtDataType.rdbNumeric, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			CertificatQuantity = .Parameters("nQuantity").Value
		End With
		
nCertificatQuantity_Err: 
		If Err.Number Then
			CertificatQuantity = False
		End If
		'UPGRADE_NOTE: Object lrecCertificatQuantity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCertificatQuantity = Nothing
		On Error GoTo 0
	End Function
End Class







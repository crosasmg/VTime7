Option Strict Off
Option Explicit On
Public Class Funds
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
	
	'**-Objective: Code of the Line of Business. The possible values as per table 10.
	'-Objetivo: Código del ramo comercial. Valores posibles según tabla 10.
	Public nBranch As Integer
	
	'**-Objective: Code of the investment fund
	'-Objetivo: Código del fondo de inversión
	Public nFunds As Integer
	
	'**-Objective: Code of the product.
	'-Objetivo: Código del producto.
	Public nProduct As Integer
	
	'**-Objective: Date which from the record is valid.
	'-Objetivo: Fecha de efecto del registro.
	Public dEffecdate As Date
	
	'**-Objective: Percentage to collect by purchase of units
	'-Objetivo: Porcentaje a cobrar por compra de unidades
	Public nBuy_cost As Double
	
	'**-Objective: Date when the record is cancelled.
	'-Objetivo: Fecha de anulación del registro.
	Public dNulldate As Object
	
	'**-Objective: Minimum percentage of share in the fund
	'-Objetivo: Porcentaje mínimo de participación en el fondo
	Public nPartic_min As Double
	
	'**-Objective: Percentage of share in the fund
	'-Objetivo: Porcentaje de participación en el fondo
	Public nParticip As Double
	
	'**-Objective: Percentage to collect by sale of units
	'-Objetivo: Porcentaje a cobrar por venta de unidades
	Public nSell_cost As Double
	Public sActivFound As String
	Public sVigen As String
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
	Public nUsercode As Integer
	
	'**-Objective:
	'-Objetivo:
	Public sDescript As String
	
	'**-Objective:
	'-Objetivo:
	Public nCountUnits As Double
	
	'**-Objective:
	'-Objetivo:
	Public nValueUnits As Double
	
	'**-Objective:
	'-Objetivo:
	Public nTotValue As Double
	
	'**-Objective:
	'-Objetivo:
	Public nAmount As Double
	
	'**-Objective:
	'-Objetivo:
	Public nSignal As Double
	
	'**-Objective:
	'-Objetivo:
	Public nUnitsChanged As Double
	
	'**-Objective:
	'-Objetivo:
	Public nValueChanged As Double
	
	'**-Objective:
	'-Objetivo:
	Public nUnitsPurchase As Double
	
	'**-Objective:
	'-Objetivo:
	Public nUnitsSales As Double
	
	'**-Objective:
	'-Objetivo: Costo total del cambio
	Public nSwi_cost_tot As Double
	
	'-Cargos por cambio: fijo y porcentual
	Public nSwi_cost As Double
	Public nSwi_cost_perc As Double
	
	'**-Objective:
	'-Objetivo:
	Public nDeb_acc As Double
	
	'**-Objective:
	'-Objetivo:
	Public nUpdate As Integer
	
	'**-Objective:
	'-Objetivo:
	Public nPolicy As Double
	
	'**-Objective:
	'-Objetivo:
	Public nCertif As Double
	
	'**-Objective:
	'-Objetivo:
	Public sClient As String
	
	'**-Objective:
	'-Objetivo:
	Public nCurrency As Integer
	
	'**-Objective:
	'-Objetivo:
	Public sSource As String
	
	'**-Objective:
	'-Objetivo:
	Public nCantSwitch As Integer
	
	Private Structure udtFunds
		Dim nBranch As Integer
		Dim nFunds As Integer
		Dim nProduct As Integer
		Dim dEffecdate As Date
		Dim nBuy_cost As Double
		Dim dNulldate As Date
		Dim nPartic_min As Double
		Dim nParticip As Double
		Dim nSell_cost As Double
		Dim sDescript As String
		Dim nUnits As Double
		Dim nTotInver As Double
        Dim sVigen As String
        Dim nIntProyVarMax As Double
	End Structure
	
	'**- Enumerate type for the payment frequency
	'**- Values in table table36
	'- Tipo enumerado para la frecuencia de los pagos
	'- indicativo table36
	
	Public Enum ePayFrecuency
		esdAnualy = 1
		esdSemestral = 2
		esdTrimestral = 3
		esdBiMestral = 4
		esdMonthly = 5
		esdQuotas = 7
	End Enum
	
	'**-Objective: Code of the investment fund
	'-Objetivo: Código del fondo de inversión
	Private arrFunds() As udtFunds
	
	'**- The variable to indicates if the fund defined in the product is related to the policy
	'**- in dealing (VI006)
	'- Indicador asociación entre el fondo definido en el producto
	'- y la póliza en tratamiento(VI006)
	
	'**-Objective:
	'-Objetivo:
	Public nSelected As Integer
	Public nOrigin As Integer
	Public sOrigin As String
	Public nIntProy As Double
	Public nIntProyVar As Double
	Public nIntProyVarMax As Double
	Public nIntProyVarCle As Double
	
	
	
	'**%Objective: Reads the actives funds related to line of business - Product
	'%Objetivo: Lee todos los fondos activos asociados a un Ramo - Producto
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaFunds As eRemoteDB.Execute
		Dim lobjValues As Object
		Dim lintCount As Integer
		
		On Error GoTo ErrorHandler
		lrecreaFunds = New eRemoteDB.Execute
		lobjValues = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Values")
		
		Find = True
		
		With lrecreaFunds
			.StoredProcedure = "reaFunds"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nOrigin", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVigen", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find = .Run
			
			If Find Then
				ReDim arrFunds(200)
				lintCount = 0
				
				Do While Not .EOF
					arrFunds(lintCount).nBranch = lobjValues.StringToType(.FieldToClass("nBranch"), eFunctions.Values.eTypeData.etdInteger)
					arrFunds(lintCount).nFunds = lobjValues.StringToType(.FieldToClass("nFunds"), eFunctions.Values.eTypeData.etdInteger)
					arrFunds(lintCount).nProduct = lobjValues.StringToType(.FieldToClass("nProduct"), eFunctions.Values.eTypeData.etdInteger)
					arrFunds(lintCount).dEffecdate = lobjValues.StringToType(.FieldToClass("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
					arrFunds(lintCount).nBuy_cost = lobjValues.StringToType(.FieldToClass("nBuy_cost"), eFunctions.Values.eTypeData.etdDouble)
					arrFunds(lintCount).dNulldate = lobjValues.StringToType(.FieldToClass("dNulldate"), eFunctions.Values.eTypeData.etdDate)
					arrFunds(lintCount).nPartic_min = lobjValues.StringToType(.FieldToClass("nPartic_min"), eFunctions.Values.eTypeData.etdDouble)
					arrFunds(lintCount).nSell_cost = lobjValues.StringToType(.FieldToClass("nSell_cost"), eFunctions.Values.eTypeData.etdDouble)
					arrFunds(lintCount).sDescript = .FieldToClass("sDescript")
                    arrFunds(lintCount).sVigen = .FieldToClass("sVigen")
                    arrFunds(lintCount).nIntProyVarMax = .FieldToClass("nIntProyVarMax")
					lintCount = lintCount + 1
					.RNext()
				Loop 
				
				.RCloseRec()
				ReDim Preserve arrFunds(lintCount)
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		
		Find = False
	End Function
	
	'**%Objective: Searchs an element in the array according the fund code
	'%Objetivo: Permite encontrar un elemento del arreglo de acuerdo al código del fondo
    Public Function FindFund(ByVal nFunds As Integer) As Boolean
        Dim lintPos As Integer
        Dim lblnFind As Boolean

        On Error GoTo ErrorHandler
        lintPos = 0
        lblnFind = False

        Do While lintPos <= UBound(arrFunds) And Not lblnFind
            If arrFunds(lintPos).nFunds = nFunds Then
                lblnFind = True
                FindFund = Item(lintPos)
            End If

            lintPos = lintPos + 1
        Loop

        Exit Function
ErrorHandler:
        FindFund = False
    End Function
	
	'%getSwitchCost: Calcula el monto de costo por switch
	'%               Se incopora para ser usado desde página ASP
	Public Function getSwitchCost(ByVal nAmount As Double) As Double
		
		getSwitchCost = nAmount * (nSwi_cost_perc / 100) + nSwi_cost
		
	End Function
	
	'**%Objective: Searchs an element in the array by it position
	'%Objetivo: Permite encontrar un elemento del arreglo por su posición
    Public Function Item(ByVal lintIndex As Integer) As Boolean
        On Error GoTo ErrorHandler
        If lintIndex <= UBound(arrFunds) Then
            Item = True
            nFunds = arrFunds(lintIndex).nFunds
            nBuy_cost = arrFunds(lintIndex).nBuy_cost
            dNulldate = arrFunds(lintIndex).dNulldate
            nPartic_min = arrFunds(lintIndex).nPartic_min
            nParticip = arrFunds(lintIndex).nParticip
            nSell_cost = arrFunds(lintIndex).nSell_cost
            sDescript = arrFunds(lintIndex).sDescript
            sVigen = arrFunds(lintIndex).sVigen
        End If

        Exit Function
ErrorHandler:
        Item = False
    End Function
	
	'**%Objective: Adds an element in to table Funds
	'%Objetivo: Permite registrar un elemento en la tabla Funds
	Public Function Add() As Boolean
		Dim lreccreFunds As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lreccreFunds = New eRemoteDB.Execute
		
		Add = True
		
		With lreccreFunds
			.StoredProcedure = "creFunds"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBuy_cost", nBuy_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPartic_min", nPartic_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSell_cost", nSell_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntProy", nIntProy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntProyVarMax", nIntProyVarMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntProyVarCle", nIntProyVarCle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVigen", sVigen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFunds = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lreccreFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFunds = Nothing
		Add = False
	End Function
	
	'**%Objective: Updates the record in the table Funds
	'%Objetivo: Permite actualizar un registro en la tabla Funds
	Public Function Update() As Boolean
		Dim lrecupdFunds As eRemoteDB.Execute
		
		
		
		'+Control de error
		On Error GoTo ErrorHandler
		lrecupdFunds = New eRemoteDB.Execute
		
		Update = True
		
		With lrecupdFunds
			.StoredProcedure = "updFunds"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBuy_cost", nBuy_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPartic_min", nPartic_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSell_cost", nSell_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntProy", nIntProy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntProyVarMax", nIntProyVarMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntProyVarCle", nIntProyVarCle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVigen", sVigen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdFunds = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecupdFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdFunds = Nothing
		Update = False
	End Function
	
	'**%Objective: Deletes a record in the table Funds
	'%Objetivo: Permite eliminar un registro en la tabla Funds
	Public Function Delete() As Boolean
		Dim lrecdelFunds As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lrecdelFunds = New eRemoteDB.Execute
		
		Delete = True
		
		With lrecdelFunds
			.StoredProcedure = "delFunds"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecdelFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelFunds = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecdelFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelFunds = Nothing
		Delete = False
	End Function
	
	'%Objetivo: Rutina para calcular los valores y nro de Unidades totales de un fondo
	'------------------------------------
	Public Sub insUnitsCalc(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal lintFunds As Integer)
		'error vss
		'error vss
		'error vss
		'error vss
		'error vss
		'error vs
		'error vss
		'error vss
		
		Dim lclsFunds As ePolicy.Funds
		Dim lclsFund_move As ePolicy.Fund_move
        Dim lcolFund_moves As ePolicy.Fund_moves = New ePolicy.Fund_moves
        Dim lclsCurren_pol As ePolicy.Curren_pol
		Dim lclsGeneral As eGeneral.Exchange
		Dim TotValue As Double
		Dim lintCount As Integer
		Dim lintIndex As Integer
		Dim lintIndexAUX As Integer
		On Error GoTo ErrorHandler
		lclsFunds = New ePolicy.Funds
		lclsFund_move = New ePolicy.Fund_move
		lclsCurren_pol = New ePolicy.Curren_pol
		lclsGeneral = New eGeneral.Exchange
		nCountUnits = 0
		nValueUnits = 0
		TotValue = 0
		lintIndexAUX = 0
		'**+ Searchs all the transactions of sell of units related to the fund
		'+ Buscar todos los movimientos de unidades asociados al fondo
		With lclsFunds
			If Not lcolFund_moves Is Nothing Then
				'UPGRADE_NOTE: Object lcolFund_moves may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lcolFund_moves = Nothing
				lcolFund_moves = New ePolicy.Fund_moves
			Else
				'            Set lcolFund_moves = New ePolic
				lcolFund_moves = New ePolicy.Fund_moves
			End If
			
			With lcolFund_moves
				If .Find(nBranch, nProduct, nPolicy, nCertif, dEffecdate, lintFunds) Then
					TotValue = 0
					nCountUnits = 0
					nValueUnits = 0
					lintIndexAUX = 0
					
					For	Each lclsFund_move In lcolFund_moves
						lintIndexAUX = lintIndexAUX + 1
						
						'**+ If the currency of the fund is diferent to the policy currency
						'**+ the charge is converted to the policy currency
						'+ Si la moneda del fondo es diferente a la de la póliza
						'+ se convierte el recargo a la moneda de la póliza
						
						If lcolFund_moves.Item(lintIndexAUX).nFunds = lintFunds Then
							If lclsCurren_pol.Find(nPolicy, nBranch, nProduct, "2", nCertif, dEffecdate) Then
								For lintIndex = 0 To lclsCurren_pol.CountCurrenPol
									If lclsCurren_pol.Val_Curren_pol(lintIndex) Then
										If lclsCurren_pol.nCurrency <> lclsFund_move.nCurrency Then
											Call lclsGeneral.Convert(0, lclsFund_move.TotValue, lclsFund_move.nCurrency, lclsCurren_pol.nCurrency, dEffecdate, eRemoteDB.Constants.intNull)
											TotValue = lclsGeneral.pdblResult
										Else
											TotValue = lclsFund_move.TotValue
										End If
										
										If lclsFund_move.nType_Move = 1 Or lclsFund_move.nType_Move = 2 Or lclsFund_move.nType_Move = 68 Then
											nCountUnits = nCountUnits + lclsFund_move.nUnits
											nValueUnits = nValueUnits + TotValue
										ElseIf lclsFund_move.nType_Move = 3 Or lclsFund_move.nType_Move = 4 Or lclsFund_move.nType_Move = 69 Then 
											nCountUnits = nCountUnits - lclsFund_move.nUnits
											nValueUnits = nValueUnits - TotValue
										End If
									End If
								Next 
							End If
						End If
					Next lclsFund_move
					
					nTotValue = TotValue
				End If
			End With
		End With
		
		'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFunds = Nothing
		'UPGRADE_NOTE: Object lclsFund_move may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_move = Nothing
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
		'UPGRADE_NOTE: Object lcolFund_moves may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFund_moves = Nothing
		
		Exit Sub
ErrorHandler: 
		'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFunds = Nothing
		'UPGRADE_NOTE: Object lclsFund_move may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_move = Nothing
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
		'UPGRADE_NOTE: Object lcolFund_moves may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFund_moves = Nothing
	End Sub
	
	'**%Objective: Shows the day transactions of the fund
	'%Objetivo: Muestra el movimiento del Fondo del Dia
	Public Sub insShowDetailMove(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nFunds As Integer)
		Dim lclsFund_value As ePolicy.Fund_value
		Dim lclsFund_move As ePolicy.Fund_move
		Dim lclsCurren_pol As ePolicy.Curren_pol
		Dim lclsGeneral As eGeneral.Exchange
		Dim lintIndex As Integer
		
		On Error GoTo ErrorHandler
		lclsFund_value = New ePolicy.Fund_value
		lclsFund_move = New ePolicy.Fund_move
		lclsCurren_pol = New ePolicy.Curren_pol
		lclsGeneral = New eGeneral.Exchange
		
		nAmount = 0
		nSignal = 0
		nUnitsChanged = 0
		nValueChanged = 0
		
		'**+ Calculation of the value of the units in the fund
		'+ Calculo del valor de las unidades que se tienen
		
		With lclsFund_value
			.nFunds = nFunds
			.dEffecdate = dEffecdate
			
			If .Find Then
				If lclsCurren_pol.Find(nPolicy, nBranch, nProduct, "2", nCertif, dEffecdate) Then
					For lintIndex = 0 To lclsCurren_pol.CountCurrenPol
						If lclsCurren_pol.Val_Curren_pol(lintIndex) Then
							If lclsCurren_pol.nCurrency <> .nCurrency Then
								Call lclsGeneral.Convert(0, .nAmount, .nCurrency, lclsCurren_pol.nCurrency, dEffecdate, eRemoteDB.Constants.intNull)
								
								nAmount = lclsGeneral.pdblResult
							Else
								nAmount = .nAmount
							End If
						End If
					Next 
				End If
			End If
		End With
		
		If lclsFund_move.Find(nBranch, nProduct, nPolicy, nCertif, dEffecdate, nFunds) Then
			nUnitsChanged = lclsFund_move.nUnits
			
			'**+ Calculation of the value of the units in the fund
			'+ Calculo del Valor de las unidades que se tienen
			
			nValueChanged = lclsFund_move.nUnits * nAmount
			
			If lclsFund_move.nType_Move = 1 Or lclsFund_move.nType_Move = 2 Or lclsFund_move.nType_Move = 68 Then
				
				'**+ Shows the signe "Plus +" for buying
				'+ Muestra el signo de "Más +" para las compras
				
				nSignal = 1
                sSource = "/VTimeNet/images/btnLargeAddOff.png"
                nBuy_cost = nBuy_cost + (nValueChanged * lclsFund_move.nBuy_cost / 100)
			ElseIf lclsFund_move.nType_Move = 3 Or lclsFund_move.nType_Move = 4 Or lclsFund_move.nType_Move = 69 Then 
				
				'**+ Shows the signe "Minus -" for Sales
				'+ Muestra el signo de "Menos -" para las ventas
				
				nSignal = 2
                sSource = "/VTimeNet/images/btnLargeDeleteOff.png"
                nSell_cost = nSell_cost + (nValueChanged * lclsFund_move.nSell_cost / 100)
			End If
		End If
		
		If nSignal <> 1 And nSignal <> 2 Then
			sSource = "/VTimeNet/images/BlankFrame.gif"
		ElseIf nSignal = 0 Then 
			sSource = "/VTimeNet/images/BlankFrame.gif"
		End If
		
		'UPGRADE_NOTE: Object lclsFund_value may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_value = Nothing
		'UPGRADE_NOTE: Object lclsFund_move may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_move = Nothing
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
		
		Exit Sub
ErrorHandler: 
		'UPGRADE_NOTE: Object lclsFund_value may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_value = Nothing
		'UPGRADE_NOTE: Object lclsFund_move may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_move = Nothing
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
	End Sub
	
	'**%Objective: Calculates the quantity of units availables in the funds stock
	'%Objetivo: Calcula la cantidad de unidades disponibles en el stock de fondos
	Public Function insCalUnits_stock(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nFunds As Integer) As Double
		Dim lclsFund_stock As ePolicy.Fund_stock
		Dim lcolFund_stock As ePolicy.Fund_stocks
		Dim mCantUnits As Double
		
		On Error GoTo ErrorHandler
		lclsFund_stock = New ePolicy.Fund_stock
		lcolFund_stock = New ePolicy.Fund_stocks
		
		insCalUnits_stock = 0
		
		If lcolFund_stock.Find_All_SpecificFund(nBranch, nProduct, dEffecdate, nFunds) Then
			For	Each lclsFund_stock In lcolFund_stock
				With lclsFund_stock
					If .nMove_type = Fund_move.eMovement_Units.esdInitialPurchase Or .nMove_type = Fund_move.eMovement_Units.esdUnitsPurchase Then
						mCantUnits = mCantUnits + .nUnits
					ElseIf .nMove_type = Fund_move.eMovement_Units.esdPolicySale Or .nMove_type = Fund_move.eMovement_Units.esdPolicySale Then 
						mCantUnits = mCantUnits - .nUnits
					End If
				End With
			Next lclsFund_stock
			
			insCalUnits_stock = mCantUnits
		End If
		
		'UPGRADE_NOTE: Object lclsFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_stock = Nothing
		'UPGRADE_NOTE: Object lcolFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFund_stock = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lclsFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_stock = Nothing
		'UPGRADE_NOTE: Object lcolFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFund_stock = Nothing
		insCalUnits_stock = 0
	End Function
	
	'**%Objective: Calculates the number of units available in the stock of funds
	'**%           for a given fund and date.
	'%Objetivo: Calcula la cantidad de unidades disponibles en el stock de fondos a
	'%          una fecha dada.
	Public Function insGetUnitsAvailable(ByVal nFund As Integer, ByVal dOperDate As Date) As Double
		Dim lclsFund_stock As ePolicy.Fund_stock
		Dim lcolFund_stock As ePolicy.Fund_stocks
		Dim mCantUnits As Double
		
		On Error GoTo ErrorHandler
		lclsFund_stock = New ePolicy.Fund_stock
		lcolFund_stock = New ePolicy.Fund_stocks
		
		insGetUnitsAvailable = 0
		
		If lcolFund_stock.Find_UnitsAvailable(nFund, dOperDate) Then
			
			For	Each lclsFund_stock In lcolFund_stock
				With lclsFund_stock
					If .nMove_type = Fund_move.eMovement_Units.esdInitialPurchase Or .nMove_type = Fund_move.eMovement_Units.esdUnitsPurchase Then
						mCantUnits = mCantUnits + .nUnits
					ElseIf .nMove_type = Fund_move.eMovement_Units.esdPolicySale Or .nMove_type = Fund_move.eMovement_Units.esdThirdsSale Then 
						mCantUnits = mCantUnits - .nUnits
					End If
				End With
			Next lclsFund_stock
			
			insGetUnitsAvailable = mCantUnits
		End If
		
		'UPGRADE_NOTE: Object lclsFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_stock = Nothing
		'UPGRADE_NOTE: Object lcolFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFund_stock = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lclsFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_stock = Nothing
		'UPGRADE_NOTE: Object lcolFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFund_stock = Nothing
		insGetUnitsAvailable = 0
	End Function
	
	'**%Objective: Calculates the initial balance of funds for a given date.
	'%Objetivo: Calcula el balance inicial de fondos a una fecha dada.
	Public Function insCalInitialBalance(ByVal dOperDate As Date, ByVal nFund As Integer) As Double
		Dim lclsFund_stock As ePolicy.Fund_stock
		Dim lcolFund_stock As ePolicy.Fund_stocks
		Dim mCantUnits As Double
		
		On Error GoTo ErrorHandler
		lclsFund_stock = New ePolicy.Fund_stock
		lcolFund_stock = New ePolicy.Fund_stocks
		
		insCalInitialBalance = 0
		
		If lcolFund_stock.Find_AllTrans(dOperDate, nFund) Then
			
			For	Each lclsFund_stock In lcolFund_stock
				With lclsFund_stock
					If .nMove_type = Fund_move.eMovement_Units.esdUnitsPurchase Then
						mCantUnits = mCantUnits + .nUnits
					ElseIf .nMove_type = Fund_move.eMovement_Units.esdPolicySale Then 
						mCantUnits = mCantUnits - .nUnits
					End If
				End With
			Next lclsFund_stock
			
			insCalInitialBalance = mCantUnits
		End If
		
		'UPGRADE_NOTE: Object lclsFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_stock = Nothing
		'UPGRADE_NOTE: Object lcolFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFund_stock = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lclsFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_stock = Nothing
		'UPGRADE_NOTE: Object lcolFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFund_stock = Nothing
		insCalInitialBalance = 0
	End Function
	
	'**%Objective: Calculates the quantity of element in the array
	'%Objetivo:
	Public ReadOnly Property CountVI010() As Integer
		Get
			On Error GoTo ErrorHandler
			CountVI010 = UBound(arrFunds)
			
			Exit Property
ErrorHandler: 
			CountVI010 = 0
		End Get
	End Property
	
	'**%Objective: Search an element in the array
	'%Objetivo: Busca un elemento en el arreglo
	Public Function ItemVI010(ByVal lintIndex As Integer) As Boolean
		On Error GoTo ErrorHandler
		If lintIndex <= UBound(arrFunds) Then
			With arrFunds(lintIndex)
				nBranch = .nBranch
				nFunds = .nFunds
				nProduct = .nProduct
				dEffecdate = .dEffecdate
				nBuy_cost = .nBuy_cost
				dNulldate = .dNulldate
				nPartic_min = .nPartic_min
				nSell_cost = .nSell_cost
				sDescript = .sDescript
			End With
			
			ItemVI010 = True
		Else
			ItemVI010 = False
		End If
		
		Exit Function
ErrorHandler: 
		ItemVI010 = False
	End Function
	
	'**%Objective: This function calculates the cost amounts of the switches
	'**%           it is used in the function insPreVI010 of the page VI010
	'%Objetivo: Función que calcula los importes referentes al cambio de fondos de inversión
	'%          Se utiliza como parte de la insPreVI010 en la Página ASP (VI010)
	Public Function insCalcData(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sCompanyType As String, ByVal nCurrency As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsProduct As eProduct.Product
		Dim lclsCurren_pol As ePolicy.Curren_pol
		Dim lclsGeneral As eGeneral.Exchange
		Dim lcolul_Move_Acc_pol As ePolicy.ul_Move_Acc_pols
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsCertificat As ePolicy.Certificat
		Dim sClient As String
		Dim lintIndex As Integer
		
		On Error GoTo ErrorHandler
		lclsProduct = New eProduct.Product
		lclsCurren_pol = New ePolicy.Curren_pol
		lclsGeneral = New eGeneral.Exchange
		lcolul_Move_Acc_pol = New ePolicy.ul_Move_Acc_pols
		lclsPolicy = New ePolicy.Policy
		lclsCertificat = New ePolicy.Certificat
		
		insCalcData = True
		
		If lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate) Then
			
			'**+ Count the actuals switches
			'+ Se cuentan los switches que se tienen actualmente
			
			If nCertif = 0 Then
				If lclsPolicy.FindPolicyOfficeName("2", nBranch, nProduct, nPolicy, sCompanyType) Then
					sClient = lclsPolicy.sClient
				End If
			Else
				If lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif) Then
					sClient = lclsCertificat.sClient
				End If
			End If
			
			Call lcolul_Move_Acc_pol.Find_v(sCertype, nBranch, nProduct, nCurrency, eCashBank.Move_Acc.eMove_Type.esdSwitch, nPolicy, nCertif, dEffecdate)
			
			nCantSwitch = lcolul_Move_Acc_pol.Count
			
			If nCantSwitch >= lclsProduct.nUlsschar Or Not insPeriodPayFree(nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
				
				'**+ If the currency of the product is different to the currency of the policy
				'**+ it is converted to the currency policy
				'+ Si la moneda del producto es diferente a la moneda de la póliza
				'+ se convierte el recargo a la moneda de la póliza
				
				If lclsCurren_pol.Find(nPolicy, nBranch, nProduct, "2", nCertif, dEffecdate) Then
					For lintIndex = 0 To lclsCurren_pol.CountCurrenPol
						If lclsCurren_pol.Val_Curren_pol(lintIndex) Then
							If lclsCurren_pol.nCurrency <> lclsProduct.nCurrency Then
								Call lclsGeneral.Convert(0, lclsProduct.nUlscharg, lclsProduct.nCurrency, lclsCurren_pol.nCurrency, dEffecdate, 0)
								
								nSwi_cost = lclsGeneral.pdblResult
							Else
								nSwi_cost = lclsProduct.nUlscharg
							End If
						End If
					Next 
				End If
				
				nSwi_cost_perc = lclsProduct.nULswchPerc
				
			Else
				nSwi_cost = 0
				nSwi_cost_perc = 0
			End If
		End If
		
		'**+ Sets the amount to be debit from the current account by switch cost and
		'**+ buy/sell cost
		'+ Colocar el importe a debitar a la cuenta, por concepto de
		'+ cargo de swith y costo de compra/venta de unidades
		
		nDeb_acc = nBuy_cost + nSell_cost + nSwi_cost
		
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
		'UPGRADE_NOTE: Object lcolul_Move_Acc_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolul_Move_Acc_pol = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
		'UPGRADE_NOTE: Object lcolul_Move_Acc_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolul_Move_Acc_pol = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		insCalcData = False
	End Function
	
	'**%Objective: This function return true if the effective date of the
	'**%           operation is between the free charges period
	'**%Parameters:
	'**%  nBranch    - Code of the Line of Business. The possible values as per table 10.
	'**%  nProduct   - Code of the product.
	'**%  nPolicy    -
	'**%  nCertif    -
	'**%  dEffecdate - Date which from the record is valid.
	'%Objetivo: Función que retorna verdadero si la fecha efectiva de
	'%          la transacción está contemplada dentro del periodo
	'%Parámetros:
	'%    nBranch    - Código del ramo comercial. Valores posibles según tabla 10.
	'%    nProduct   - Código del producto.
	'%    nPolicy    -
	'%    nCertif    -
	'%    dEffecdate - Fecha de efecto del registro.
	Public Function insPeriodPayFree(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lclsProduct As eProduct.Product
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsCertificat As ePolicy.Certificat
		Dim mresulDate As Date
		Dim ldtmDate As Date
		
		On Error GoTo ErrorHandler
		insPeriodPayFree = True
		
		lclsProduct = New eProduct.Product
		lclsPolicy = New ePolicy.Policy
		lclsCertificat = New ePolicy.Certificat
		
		With lclsProduct
			Call .FindProduct_li(nBranch, nProduct, dEffecdate)
			
			If nCertif = 0 Then
				Call lclsPolicy.Find("2", nBranch, nProduct, nPolicy)
				
				ldtmDate = lclsPolicy.dDate_Origi
			Else
				Call lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif)
				
				ldtmDate = lclsCertificat.dDate_Origi
			End If
			
			If .nUlswiper <> 0 And .nUlswiper <> eRemoteDB.Constants.intNull Then
				Select Case .nUlswiper
					
					'**+ It adds the charge frecuency to the date origin of the policy/certificate
					'+ Se le suma la frecuencia a la fecha de efecto de la poliza/certificado
					
					Case ePayFrecuency.esdMonthly
						mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, ldtmDate)
						
					Case ePayFrecuency.esdAnualy
						mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 12, ldtmDate)
						
					Case ePayFrecuency.esdSemestral
						mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 6, ldtmDate)
						
					Case ePayFrecuency.esdTrimestral
						mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 3, ldtmDate)
						
					Case ePayFrecuency.esdBiMestral
						mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 2, ldtmDate)
						
					Case Else
						mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, ldtmDate)
				End Select
				
				'**+ if the result date is inferior to the transacton date
				'**+ then the redirection cost will no be collected
				'+ Si la fecha resultante es menor que la fecha de la transacción
				'+ entonces no se cobrara el costo por cambio
				
				If mresulDate < dEffecdate Then
					insPeriodPayFree = False
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
		insPeriodPayFree = False
	End Function
End Class







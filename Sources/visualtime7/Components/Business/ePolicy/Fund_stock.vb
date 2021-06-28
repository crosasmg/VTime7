Option Strict Off
Option Explicit On
Public Class Fund_stock
	'**+Objective: Class that supports the table Fund_stock
	'**+           it's content is: Fund stock movements  A record per every purchase or sale movement recorded in the system
	'**+Version: $$Revision: 7 $
	'+Objetivo: Clase que le da soporte a la tabla Fund_stock
	'+          cuyo contenido es: Movimientos de stock de fondos  Un registro por cada movimiento de compra o venta de unidades registrado en el sistema
	'+Version: $$Revision: 7 $
	'%-------------------------------------------------------%'
	'% $Workfile:: Fund_stock.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Objective: Code of the investment fund
	'-Objetivo: Código del fondo de inversión
	Public nFunds As Integer
	
	'**-Objective: Type of movement (Purchase/Sale)  Sole values as per table 415
	'-Objetivo: Tipo de movimiento de compra/venta.   Valores únicos según tabla 415.
	Public nMove_type As Integer
	
	'**-Objective: Number identifying the stock movement
	'-Objetivo: Número que identifica el movimiento
	Public nNum_mov As Integer
	
	'**-Objective: Date which from the record is valid.
	'-Objetivo: Fecha de efecto del registro.
	Public dEffecdate As Date
	
	'**-Objective: Number of investment  units
	'-Objetivo: Cantidad de unidades
	Public nUnits As Double
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
	Public nUsercode As Integer
	
	'**-Objective:
	'-Objetivo:
	Public nBranch As Integer
	
	'**-Objective:
	'-Objetivo:
	Public nProduct As Integer
	
	'**-Objective:
	'-Objetivo:
	Public nSignal As Integer
	
	'**-Objective:
	'-Objetivo:
	Public nSellCost As Double
	
	'**-Objective:
	'-Objetivo:
	Public nBuyCost As Double
	
	'**-Objective:
	'-Objetivo:
	Public nGanancy As Double
	
	'**-Objective:
	'-Objetivo:
	Public nValue As Double
	
	'**-Objective:
	'-Objetivo:
	Public nUnitsChange As Double
	
	'**-Objective:
	'-Objetivo:
	Public nStatInstanc As Fund_inv.eStatusInstance_f
	
	'**-Objective: Type of movement (Purchase/Sale)  Sole values as per table 415
	'-Objetivo: Tipo de movimiento de compra/venta.   Valores únicos según tabla 415.
	Public sMove_type As String
	
	'**-Objective: Code of the investment fund
	'-Objetivo: Código del fondo de inversión
	Public sFunds As String
	
	'**-Objective:
	'-Objetivo:
	Private mdblQuan_avail As Double
	
	'**%Objective: Allows to register a movement in the Fund_stock table
	'%Objetivo: Permite registrar un movimiento en la tabla Fund_stock
	Public Function Add() As Boolean
		Dim lreccreFund_stock As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lreccreFund_stock = New eRemoteDB.Execute
		
		Add = True
		
		With lreccreFund_stock
			.StoredProcedure = "creFund_stock"
			
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMove_type", nMove_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUnits", nUnits, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFund_stock = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lreccreFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreFund_stock = Nothing
		Add = False
	End Function
	
	'**%Objective: Allows to read all the stock cash flows of a specific found
	'**%Parameters:
	'**%  nBranch1   -
	'**%  nProduct1  -
	'**%  dOperDate1 -
	'**%  nFunds1    -
	'%Objetivo: Permite leer todos los movimientos de stock de un fondo especifico.
	'%Parámetros:
	'%    nBranch1   -
	'%    nProduct1  -
	'%    dOperDate1 -
	'%    nFunds1    -
	Public Function Find_All_SpecificFund(ByVal nBranch1 As Integer, ByVal nProduct1 As Integer, ByVal dOperDate1 As Date, ByVal nFunds1 As Integer) As Boolean
		Dim lintPos As Integer
		Dim lrecreaFund_stocks As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		Find_All_SpecificFund = False
		
		If nBranch <> nBranch1 Or nProduct <> nProduct1 Or dEffecdate <> dOperDate1 Or nFunds <> nFunds1 Then
			
			lrecreaFund_stocks = New eRemoteDB.Execute
			
			With lrecreaFund_stocks
				.StoredProcedure = "reaFund_stocks"
				
				.Parameters.Add("nBranch", nBranch1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dOperdate", dOperDate1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nFunds", nFunds1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Find_All_SpecificFund = True
					nStatInstanc = Fund_inv.eStatusInstance_f.eftExist_f
					nBranch = .FieldToClass("nBranch")
					nProduct = .FieldToClass("nProduct")
					dEffecdate = .FieldToClass("dEffecDate")
					nFunds = .FieldToClass("nFunds")
					nMove_type = .FieldToClass("nMove_type")
					nNum_mov = .FieldToClass("nNum_mov")
					nUnits = .FieldToClass("nUnits")
					nSellCost = .FieldToClass("nSell_Cost")
					nBuyCost = .FieldToClass("nBuy_Cost")
					nGanancy = .FieldToClass("Ganancy")
					nValue = .FieldToClass("Value")
					.RNext()
					.RCloseRec()
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecreaFund_stocks may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaFund_stocks = Nothing
		Else
			Find_All_SpecificFund = True
		End If
		
		'UPGRADE_NOTE: Object lrecreaFund_stocks may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFund_stocks = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaFund_stocks may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFund_stocks = Nothing
		Find_All_SpecificFund = False
	End Function
	
	'**%Objective: Allows to update a movement of the fund_stock
	'%Objetivo: Permite actualizar un movimiento de fund_stock
	Public Function Update() As Boolean
		Dim lrecupdFund_stock As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lrecupdFund_stock = New eRemoteDB.Execute
		
		Update = True
		
		With lrecupdFund_stock
			.StoredProcedure = "updFund_stock"
			
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMove_type", nMove_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNum_mov", nNum_mov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUnits", nUnits, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdFund_stock = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecupdFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdFund_stock = Nothing
		Update = False
	End Function
	
	'**%Objective: Allows to delete the Fund_stock cash flows to a date
	'%Objetivo: Permite eliminar los movimientos de Fund_stock a una fecha
	Public Function Delete_by_date() As Boolean
		
		Dim lrecdelFund_stockByFund As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lrecdelFund_stockByFund = New eRemoteDB.Execute
		
		Delete_by_date = True
		
		With lrecdelFund_stockByFund
			.StoredProcedure = "delFund_stockByFund"
			
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete_by_date = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecdelFund_stockByFund may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelFund_stockByFund = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecdelFund_stockByFund may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelFund_stockByFund = Nothing
		Delete_by_date = False
	End Function
	
	'**%Objective: Routine to update the fund stock
	'**%Parameters:
	'**%  nRem_number -
	'%Objetivo: Rutina para actualizar el stock de los fondos
	'%Parámetros:
	'%    nRem_number -
    Public Function insUpdateFund_stock(ByVal nRem_number As Integer) As Boolean
        Dim lclsFund_inv As ePolicy.Fund_inv
        Dim lclsFund_stock As ePolicy.Fund_stock
        Dim lcolFund_stock As ePolicy.Fund_stocks
        Dim mdteEffecDate As Date

        On Error GoTo ErrorHandler
        lclsFund_inv = New ePolicy.Fund_inv
        lclsFund_stock = New ePolicy.Fund_stock
        lcolFund_stock = New ePolicy.Fund_stocks

        insUpdateFund_stock = True

        With lclsFund_stock
            If lcolFund_stock.Find_All_SpecificFund(nBranch, nProduct, dEffecdate, nFunds) Then
                For Each lclsFund_stock In lcolFund_stock

                    '**+ If previous movement existed then are deleted and are create again
                    '+ Si existian movimientos previos se borran y se crean nuevamente

                    If .nFunds = nFunds And .dEffecdate = dEffecdate Then
                        If .dEffecdate <> mdteEffecDate Then
                            mdteEffecDate = .dEffecdate
                            If Not .Delete_by_date Then
                                insUpdateFund_stock = False
                            End If
                        End If
                    End If
                Next lclsFund_stock
            End If

            .nFunds = nFunds
            .dEffecdate = dEffecdate
        End With

        If CStr(nSignal) = "1" Then
            nMove_type = eCashBank.Move_Acc.eMovement_Units_f.esdUnitsPurchase_f
        Else
            nMove_type = eCashBank.Move_Acc.eMovement_Units_f.esdPolicySale_f
        End If

        If Not Add() Then
            insUpdateFund_stock = False
        Else

            '**+ With the proceed units is actualize fund_inv
            '+ Con las unidades tramitadas se actualiza fund_inv

            If lclsFund_inv.Find((Me.nFunds)) Then
                lclsFund_inv.nUsercode = nUsercode

                If nMove_type = eCashBank.Move_Acc.eMovement_Units_f.esdUnitsPurchase_f Then
                    lclsFund_inv.nQuan_avail = IIf(lclsFund_inv.nQuan_avail = eRemoteDB.Constants.intNull, 0, lclsFund_inv.nQuan_avail) - Me.nUnits
                ElseIf nMove_type = eCashBank.Move_Acc.eMovement_Units_f.esdPolicySale_f Then
                    lclsFund_inv.nQuan_avail = IIf(lclsFund_inv.nQuan_avail = eRemoteDB.Constants.intNull, 0, lclsFund_inv.nQuan_avail) + Me.nUnits
                End If

                If Not lclsFund_inv.Update Then
                    insUpdateFund_stock = False
                End If
            End If
        End If

        'UPGRADE_NOTE: Object lclsFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFund_inv = Nothing
        'UPGRADE_NOTE: Object lclsFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFund_stock = Nothing
        'UPGRADE_NOTE: Object lcolFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolFund_stock = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_NOTE: Object lclsFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFund_inv = Nothing
        'UPGRADE_NOTE: Object lclsFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFund_stock = Nothing
        'UPGRADE_NOTE: Object lcolFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolFund_stock = Nothing
        insUpdateFund_stock = False
    End Function
	
	'**%Objective: Validates the entered data in the detail zone to the form
	'**%Parameters:
	'**%  sCodispl   -
	'**%  nFunds     - Code of the investment fund
	'**%  nMove_type - Type of movement (Purchase/Sale)  Sole values as per table 415
	'**%  nUnits     - Number of investment  units
	'**%  dEffecdate - Date which from the record is valid.
	'%Objetivo: Permite validar los datos introducidos en la zona de detalle para
	'%          forma.
	'%Parámetros:
	'%    sCodispl   -
	'%    nFunds     - Código del fondo de inversión
	'%    nMove_type - Tipo de movimiento de compra/venta.   Valores únicos según tabla 415.
	'%    nUnits     - Cantidad de unidades
	'%    dEffecdate - Fecha de efecto del registro.
	Public Function insValMVI005_K(ByVal sCodispl As String, ByVal nFunds As Integer, ByVal nMove_type As Integer, ByVal nUnits As Double, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsvalfield As eFunctions.valField
		Dim lclsValues As eFunctions.Values
		Dim lclsFund_inv As Fund_inv
		
		On Error GoTo ErrorHandler
		lclsErrors = New eFunctions.Errors
		lclsvalfield = New eFunctions.valField
		lclsValues = New eFunctions.Values
		lclsFund_inv = New Fund_inv
		lclsvalfield.objErr = lclsErrors
		
		'**+ Validation of the Found filed
		'+ Validación del campo Fondo
		
		If nFunds <= 0 Then
            lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(916)) 'Fondo:
        End If
		
		'**+ Date validation
		'+ Validación de la fecha
		
		lclsvalfield.ErrEmpty = 4003
		
		If Not lclsvalfield.ValDate(dEffecdate) Then
		Else
			If nFunds > 0 Then
				If lclsFund_inv.Find(nFunds) Then
					If dEffecdate < lclsFund_inv.dInpdate Then
						lclsErrors.ErrorMessage(sCodispl, 10290)
					End If
				End If
			End If
		End If
		
		'**+ Validation of the movement type
		'+ Validación del tipo de movimiento
		
		If nMove_type <= 0 Then
			lclsErrors.ErrorMessage(sCodispl, 7125)
		End If
		
		'**+ Units validation
		'+ Validación de las unidades
		
		If nUnits <= 0 Then
            lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(917)) 'Unidades:
        ElseIf nMove_type > 0 And nFunds > 0 Then 
			
			'**+ If the cash flow type correspond to Buying
			'+ Si el tipo de movimiento corresponde a compra
			
			If nMove_type = 1 Or nMove_type = 2 Then
				If nMove_type = 2 Then
					mdblQuan_avail = IIf(lclsFund_inv.nQuan_avail = eRemoteDB.Constants.intNull, 0, lclsFund_inv.nQuan_avail) + nUnits
				Else
					mdblQuan_avail = nUnits
				End If
				
				If lclsFund_inv.nQuan_max <> eRemoteDB.Constants.intNull And lclsFund_inv.nQuan_max <> 0 Then
					If lclsFund_inv.nQuan_max < mdblQuan_avail Then
						lclsErrors.ErrorMessage(sCodispl, 10292)
					End If
				End If
			Else
				If nMove_type = 3 Or nMove_type = 4 Then
					mdblQuan_avail = IIf(lclsFund_inv.nQuan_avail = eRemoteDB.Constants.intNull, 0, lclsFund_inv.nQuan_avail) - nUnits
					
					If lclsFund_inv.nQuan_min > mdblQuan_avail Then
						lclsErrors.ErrorMessage(sCodispl, 10293)
					End If
				End If
			End If
		End If
		
		insValMVI005_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsvalfield may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalfield = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		'UPGRADE_NOTE: Object lclsFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_inv = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsvalfield may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalfield = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		'UPGRADE_NOTE: Object lclsFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_inv = Nothing
		insValMVI005_K = String.Empty
	End Function
	
	'**%Objective: Makes the updates in the tables
	'**%Parameters:
	'**%  nFunds     - Code of the investment fund
	'**%  nMove_type - Type of movement (Purchase/Sale)  Sole values as per table 415
	'**%  nUnits     - Number of investment  units
	'**%  nUsercode  - Code of the user creating or updating the record.
	'**%  dEffecdate - Date which from the record is valid.
	'%Objetivo: Permite realizar las actualizaciones en las tablas
	'%Parámetros:
	'%    nFunds     - Código del fondo de inversión
	'%    nMove_type - Tipo de movimiento de compra/venta.   Valores únicos según tabla 415.
	'%    nUnits     - Cantidad de unidades
	'%    nUsercode  - Código del usuario que crea o actualiza el registro.
	'%    dEffecdate - Fecha de efecto del registro.
	Public Function insPostMVI005_K(ByVal nFunds As Integer, ByVal nMove_type As Integer, ByVal nUnits As Double, ByVal nUsercode As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsFund_inv As Fund_inv
		
		On Error GoTo ErrorHandler
		lclsFund_inv = New Fund_inv
		
		With Me
			.dEffecdate = dEffecdate
			.nFunds = nFunds
			.nMove_type = nMove_type
			.nUnits = nUnits
			.nUsercode = nUsercode
			insPostMVI005_K = Add
		End With
		
		If insPostMVI005_K Then
			With lclsFund_inv
				If .Find(nFunds) Then
					If nMove_type = 1 Then
						.nQuan_avail = nUnits
					ElseIf nMove_type = 2 Then 
						.nQuan_avail = IIf(.nQuan_avail = eRemoteDB.Constants.intNull, 0, .nQuan_avail) + nUnits
					ElseIf nMove_type = 3 Or nMove_type = 4 Then 
						.nQuan_avail = IIf(.nQuan_avail = eRemoteDB.Constants.intNull, 0, .nQuan_avail) - nUnits
					End If
				End If
				
				.nFunds = nFunds
				.nUsercode = nUsercode
				
				insPostMVI005_K = .UpdateQuan_avail
			End With
		End If
		
		'UPGRADE_NOTE: Object lclsFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_inv = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lclsFund_inv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFund_inv = Nothing
		insPostMVI005_K = False
	End Function
	
	'**%Objective: Executes the corresponding validations according to the funcional specifications
	'**%Parameters:
	'**%  sCodispl -
	'**%  nFunds   - Code of the investment fund
	'**%  dDate    -
	'%Objetivo: Realiza las validaciones correspondientes, según lo indica el funcional de
	'%          la transacción.
	'%Parámetros:
	'%    sCodispl -
	'%    nFunds   - Código del fondo de inversión
	'%    dDate    -
	Public Function insValVIC014_K(ByVal sCodispl As String, ByVal nFunds As Integer, ByVal dDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValues As eFunctions.Values
		
		On Error GoTo ErrorHandler
		lclsErrors = New eFunctions.Errors
		lclsValues = New eFunctions.Values
		
		If nFunds <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(258) & ":")
        End If
		
		If dDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3404)
		End If
		
		insValVIC014_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		insValVIC014_K = String.Empty
	End Function
	
	'**%Objective: Executes the corresponding validations according to the funcional specifications
	'**%Parameters:
	'**%  sCodispl -
	'**%  dDate    -
	'%Objetivo: Realiza las validaciones correspondientes, según lo indica el funcional de
	'%          la transacción.
	'%Parámetros:
	'%    sCodispl -
	'%    dDate    -
	Public Function insValVIC013_K(ByVal sCodispl As String, ByVal dDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo ErrorHandler
		lclsErrors = New eFunctions.Errors
		
		If dDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3404)
		End If
		
		insValVIC013_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		insValVIC013_K = String.Empty
	End Function
End Class







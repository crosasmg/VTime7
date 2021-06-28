Option Strict Off
Option Explicit On
Public Class Fund_move
	'%-------------------------------------------------------%'
	'% $Workfile:: Fund_move.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Enumerated type definition. This will hold the movement type
	'- Tipo enumerado para el tipo de movimiento
	
	Enum eMovement
		'**+ Units buying
		'+ Compra de unidades
		esdPurchase = 1
		'**+ Units selling
		'+ venta de unidades
		esdSale = 2
	End Enum
	
	'**+ Enumerated type defintion. This will hold the units movements according to the table 415
	'+ Se creó el tipo enumerado para los movimientos de unidades según la tabla 415
	
	Enum eMovement_Units
		'**+ Initial buy
		'+ Compra inicial
		esdInitialPurchase = 1
		'**+ Units buying
		'+ Compra de unidades
		esdUnitsPurchase = 2
		'**+ Policy sell
		'+ Venta de la poliza
		esdPolicySale = 3
		'**+ Sell to a third
		'+ Ventas a terceros
		esdThirdsSale = 4
	End Enum
	
	'Column_name                                   Type     Computed   Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public sCertype As String 'char             no        1                       no                                  yes                                 no
	Public nBranch As Integer 'smallint         no        2           5     0     no                                  (n/a)                               (n/a)
	Public nProduct As Integer 'smallint         no        2           5     0     no                                  (n/a)                               (n/a)
	Public nPolicy As Double 'int              no        4           10    0     no                                  (n/a)                               (n/a)
	Public nCertif As Double 'int              no        4           10    0     no                                  (n/a)                               (n/a)
	Public nFunds As Integer 'smallint         no        2           5     0     no                                  (n/a)                               (n/a)
	Public dCompdate As Date 'datetime         no        8                       no                                  (n/a)                               (n/a)
	Public dOperDate As Date 'datetime         no        8                       yes                                 (n/a)                               (n/a)
	Public nRem_number As Integer 'smallint         no        2           5     0     yes                                 (n/a)                               (n/a)
	Public nType_Move As eMovement_Units 'smallint         no        2           5     0     yes                                 (n/a)                               (n/a)
	Public nUnits As Double
	Public nUsercode As Integer 'smallint         no        2           5     0     no                                  (n/a)                               (n/a)
	
	Public nBuy_cost As Double
	Public nSell_cost As Double
	
	Public sBranch As String
	Public sProduct As String
	Public sEntry As String
	Public sClient As String
	Public sCliename As String
	
	'**- Currency of the amount of the fund
	'- Moneda en que viene expresado el valor del fondo
	
	Public nCurrency As Integer
	
	'**- Single unit value
	'- Valor Nominal
	
	Public nAmount As Double
	
	'**- Total value = Units * Single unit value
	'- Valor Total = Unidades * Valor nominal
	
	Public TotValue As Double
	Public nUnit_Balance As Double
	Public nInstitution As Integer
	Public sInstitution As String
	Public nOrigin As Integer
	Public sOrigin As String
	Public dDate_Origin As Date
	
	'**% Find: This function reads the movements of the day
	'% Find: Función para realizar la lectura del movimientos de EL DIA
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dOperDate As Date, ByVal nFunds As Integer) As Boolean
		Dim lrecreaFund_Move As eRemoteDB.Execute
		
		lrecreaFund_Move = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		Find = False
		
		'**+ Stored procedure parameters definition 'insudb.reaFund_Move'
		'**+ Data of 04/09/2001 02:10:05 PM
		'+ Definición de parámetros para stored procedure 'insudb.reaFund_Move'
		'+ Información leída el 09/04/2001 02:10:05 PM
		
		With lrecreaFund_Move
			.StoredProcedure = "reaFund_Move"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nRem_number = .FieldToClass("nRem_number")
				nCertif = .FieldToClass("nCertif")
				dOperDate = .FieldToClass("dOperdate")
				nType_Move = .FieldToClass("nType_move")
				nUnits = .FieldToClass("nUnits")
				nBuy_cost = .FieldToClass("nBuy_cost")
				nSell_cost = .FieldToClass("nSell_cost")
				
				Find = True
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaFund_Move may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFund_Move = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
	End Function
	
	'**%Add: This function adds the cash flows to a fund
	'%Add: Función para realizar la inserción de movimientos de un fondo
	Public Function Add() As Boolean
		
		Dim lreccreMove_fund As eRemoteDB.Execute
		
		lreccreMove_fund = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		Add = False
		
		'**+ Stored procedure parameters definition 'insudb.creMove_fund'
		'**+ Data of 11/15/1999 08:59:42 AM
		'+ Definición de parámetros para stored procedure 'insudb.creMove_fund'
		'+ Información leída el 15/11/1999 08:59:42 AM
		
		With lreccreMove_fund
			.StoredProcedure = "creMove_fund"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperDate", dOperDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", nType_Move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUnits", nUnits, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRem_number", nRem_number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUnit_balance", nUnit_Balance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_origin", dDate_Origin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Add = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lreccreMove_fund may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreMove_fund = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
	End Function
	
	'**% insValVIC012_K: Executes the corresponding validations according to the funcional specifications
	'% insValVIC012_K: Realiza las validaciones correspondientes, según lo indica el funcional de
	'% la transacción.
	Public Function insValVIC012_K(ByVal sCodispl As String, ByVal nFunds As Integer, ByVal dDate As Date) As String
		
		On Error GoTo insValVIC012_K_err
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValues As eFunctions.Values
		
		lclsErrors = New eFunctions.Errors
		lclsValues = New eFunctions.Values
		
		If nFunds <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(258) & ":")
        End If
		
		If dDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3404)
		End If
		
		insValVIC012_K = lclsErrors.Confirm
		
insValVIC012_K_err: 
		If Err.Number Then insValVIC012_K = "insValVIC012_K: " & Err.Description
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		
		On Error GoTo 0
	End Function
End Class







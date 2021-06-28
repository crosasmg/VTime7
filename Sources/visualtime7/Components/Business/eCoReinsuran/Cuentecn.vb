Option Strict Off
Option Explicit On
Public Class Cuentecn
	'%-------------------------------------------------------%'
	'% $Workfile:: Cuentecn.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:28p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the system table on 06/18/2001
	'+ Propiedades según la tabla en el sistema el 18/06/2001
	
	'     Column_name                Type               Computed  Length  Prec  Scale  Nullable  TrimTrailingBlanks  FixedLenNullInSource
	Public nType_rel As Integer 'smallint         no        2     5     0     no             (n/a)                 (n/a)
	Public nIdConsec As Integer 'smallint         no        4     10    0     no             (n/a)                 (n/a)
	Public nNumber As Integer 'smallint         no        2     5     0     no             (n/a)                 (n/a)
	Public nType As Integer 'smallint         no        2     5     0     no             (n/a)                 (n/a)
	Public nBranch As Integer 'smallint         no        2     5     0     no             (n/a)                 (n/a)
	Public sType_acc As String 'char             no        1                 no              no                    no
	Public nType_per As Integer 'smallint         no        2     5     0     no             (n/a)                 (n/a)
	Public nPeriody As Integer 'smallint         no        2     5     0     no             (n/a)                 (n/a)
	Public nCurrency As Integer 'smallint         no        2     5     0     no             (n/a)                 (n/a)
	Public nClaim_ced As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nClaim_rec As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nComision As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nCompany As Integer 'smallint         no        2     5     0     yes            (n/a)                 (n/a)
	Public dCompdate As Date 'datetime         no        8                 yes            (n/a)                 (n/a)
	Public nDev_rescla As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nDev_respre As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nE_car_prem As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nE_car_sin As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public dEffecdate As Date 'datetime         no        8                 yes            (n/a)                 (n/a)
	Public nGasto_reas As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nImpuesto As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nInter_prem As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nInter_sin As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public sMove_Acc As String 'char             no        1                 yes             no                    yes
	Public dNulldate As Date 'datetime         no        8                 yes            (n/a)                 (n/a)
	Public nPart_benef As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nPrem_anu As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nPrem_ced As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nPrem_no_an As Double 'decimal          no        9     10    2     yes            (n/a)                 (n/a)
	Public nPrem_res_a As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public sPrint As String 'char             no        1                 yes             no                    yes
	Public nR_car_prem As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nR_car_sin As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nRes_aj_sin As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nRes_sinpen As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nRet_respre As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nSal_f_comp As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nSal_f_rein As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nClaimAmo As Double 'decimal          no        9     12    0     yes            (n/a)                 (n/a)
	Public nUsercode As Integer 'smallint         no        2     5     0     yes            (n/a)                 (n/a)
	Public nYear As Integer 'smallint         no        2     5     0     yes            (n/a)                 (n/a)
	Public nYear_ser As Integer 'smallint         no        2     5     0     yes            (n/a)                 (n/a)
	Public sActive As String 'char             no        1                 yes             no                    yes
	Public nRequestnu As Integer 'smallint         no        4     10    0     yes            (n/a)                 (n/a)
	Public dDatePay As Date 'datetime         no        8                 yes            (n/a)                 (n/a)
	'+ Propiedades auxiliares
	
	'-Se define la variable global que totalizará todos los campos de la ventana CR006H
	
	Public gcurTotalRei As Decimal
	Public nTotReiH As Decimal
	Public nTotReiD As Decimal
	Public nBalanceH As Decimal
	Public sBalanceReaH As String
	Public sBalanceAseH As String
	Public sBalanceReaD As String
	Public sBalanceAseD As String
	Public blnPremCed As Boolean
	Public blnDevResPre As Boolean
	Public blnInterPrem As Boolean
	Public blnECarPrem As Boolean
	Public blnDevResCla As Boolean
	Public blnInterSin As Boolean
	Public blnECarSin As Boolean
	Public blnPartBenef As Boolean
	
	'-Se define la variable global que totalizará todos los campos de la ventana CR006D
	
	Public gcurTotalInsu As Decimal
	Public nTotInsuH As Decimal
	Public nTotInsuD As Decimal
	Public nBalanceD As Decimal
	Public blnRetResPre As Boolean
	Public blnRCarPrem As Boolean
	Public blnGastoReas As Boolean
	Public blnClaimCed As Boolean
	Public blnResSinPen As Boolean
	Public blnRCarSin As Boolean
	Public blnCommission As Boolean
	Public blnImpuesto As Boolean
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		'+Se indica que el tipo de contratos a procesar son los proporcionales
		nType_rel = 1
		'+Se indica que el código de la moneda por defecto
		nCurrency = 1
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Find: Se realiza la lectura de la información general de las cuentas técnicas
	Public Function Find(ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal nType As Integer, ByVal nCompany As Integer, ByVal nPerType As Integer, ByVal nPerNum As Integer, ByVal sBussiType As String, ByVal nCurrency As Integer) As Boolean
		Dim lrecreaCuentecn As eRemoteDB.Execute
		
		lrecreaCuentecn = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaCuentecn'
		'+ Información leída el 19/06/2001 11:02:59 a.m.
		If nNumber = eRemoteDB.Constants.intNull Then
			nNumber = 0
		End If
		
		With lrecreaCuentecn
			.StoredProcedure = "REACUENTECNPKG.REACUENTECN"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_per", nPerType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriody", nPerNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", sBussiType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				If .FieldToClass("Record") = "True" Then
					Find = True
					nType_rel = .FieldToClass("nType_rel")
					nIdConsec = .FieldToClass("nIdConsec")
					nNumber = .FieldToClass("nNumber")
					nType = .FieldToClass("nType")
					nBranch = .FieldToClass("nBranch")
					sType_acc = .FieldToClass("sType_acc")
					nType_per = .FieldToClass("nType_per")
					nPeriody = .FieldToClass("nPeriody")
					nCurrency = .FieldToClass("nCurrency")
					nClaim_ced = .FieldToClass("nClaim_ced")
					nClaim_rec = .FieldToClass("nClaim_rec")
					nComision = .FieldToClass("nComision")
					nCompany = .FieldToClass("nCompany")
					nDev_rescla = .FieldToClass("nDev_rescla")
					nDev_respre = .FieldToClass("nDev_respre")
					nE_car_prem = .FieldToClass("nE_car_prem")
					nE_car_sin = .FieldToClass("nE_car_sin")
					dEffecdate = .FieldToClass("dEffecdate")
					nGasto_reas = .FieldToClass("nGasto_reas")
					nImpuesto = .FieldToClass("nImpuesto")
					nInter_prem = .FieldToClass("nInter_prem")
					nInter_sin = .FieldToClass("nInter_sin")
					sMove_Acc = .FieldToClass("sMove_Acc")
					dNulldate = .FieldToClass("dNulldate")
					nPart_benef = .FieldToClass("nPart_benef")
					nPrem_anu = .FieldToClass("nPrem_anu")
					nPrem_ced = .FieldToClass("nPrem_ced")
					nPrem_no_an = .FieldToClass("nPrem_no_an")
					nPrem_res_a = .FieldToClass("nPrem_res_a")
					sPrint = .FieldToClass("sPrint")
					nR_car_prem = .FieldToClass("nR_car_prem")
					nR_car_sin = .FieldToClass("nR_car_sin")
					nRes_aj_sin = .FieldToClass("nRes_aj_sin")
					nRes_sinpen = .FieldToClass("nRes_sinpen")
					nRet_respre = .FieldToClass("nRet_respre")
					nSal_f_comp = .FieldToClass("nSal_f_comp")
					nSal_f_rein = .FieldToClass("nSal_f_rein")
					nClaimAmo = .FieldToClass("nClaimAmo")
					nYear = .FieldToClass("nYear")
					nYear_ser = .FieldToClass("nYear_ser")
					nRequestnu = .FieldToClass("nRequestNu")
					dDatePay = .FieldToClass("dDatePay")
				Else
					Find = False
				End If
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCuentecn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCuentecn = Nothing
	End Function
	
	'%insValCR006_k: Esta función se encarga de validar los datos introducidos en la forma CR006_k
	Public Function insValCR006_k(ByVal sCodispl As String, ByVal nReinsurance As Integer, ByVal nNumber As Integer, ByVal nBranchRei As Integer, ByVal nContraType As Integer, ByVal nCompany As Integer, ByVal nPerType As Integer, ByVal nPerNum As Integer, ByVal sBussiType As String, ByVal nCurrency As Integer) As String
		Dim lclsContrmaster As eCoReinsuran.Contrmaster
		Dim lclsPart_contr As eCoReinsuran.Part_contr
		Dim lclsErrors As eFunctions.Errors
		
		lclsContrmaster = New eCoReinsuran.Contrmaster
		lclsPart_contr = New eCoReinsuran.Part_contr
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValCR006_k_Err
		
		'+Validacion del código del contrato
		If nNumber > 0 Then
			If Not lclsContrmaster.Find(nReinsurance, nNumber, nContraType, nBranchRei, eRemoteDB.Constants.dtmNull) Then
				Call lclsErrors.ErrorMessage(sCodispl, 21002)
			End If
		End If
		
		'+Validacion del campo codigo
		If nNumber = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3826)
		End If
		
		'+Validación del código de Compañía.
		If nCompany = eRemoteDB.Constants.intNull Or nCompany = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 6012)
		Else
			If nNumber > 0 Then
				If Not lclsPart_contr.Find(String.Empty, nNumber, nContraType, nBranchRei, Today, nReinsurance) Then
					Call lclsErrors.ErrorMessage(sCodispl, 6002)
				End If
			End If
		End If
		
		'+Validación del campo Periodo-Numero
		If nPerNum = eRemoteDB.Constants.intNull Or nPerNum = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 6077)
		Else
			'+Si el tipo de período es semestral
			If nPerType = 2 Then
				If nPerNum > 3 Then
					Call lclsErrors.ErrorMessage(sCodispl, 6077)
				End If
			End If
			'+Si el tipo de período es trimestral
			If nPerType = 3 Then
				If nPerNum > 5 Then
					Call lclsErrors.ErrorMessage(sCodispl, 6077)
				End If
			End If
			'+Si el tipo de período es mensual
			If nPerType = 4 Then
				If nPerNum > 12 Or nPerNum < 1 Then
					Call lclsErrors.ErrorMessage(sCodispl, 6077)
				End If
			End If
		End If
		
		'+Validacion de la cuenta técnica
		If Not Find(nNumber, nBranchRei, nContraType, nCompany, nPerType, nPerNum, sBussiType, nCurrency) Then
			Call lclsErrors.ErrorMessage(sCodispl, 6052)
		Else
			If Me.nSal_f_comp = Me.nSal_f_rein Then
				Call lclsErrors.ErrorMessage(sCodispl, 60830)
			End If
			'+Validacion de orden de pago.
			If Me.nRequestnu > 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 55810)
			End If
		End If
		
		insValCR006_k = lclsErrors.Confirm
		
insValCR006_k_Err: 
		If Err.Number Then
			insValCR006_k = insValCR006_k & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsContrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContrmaster = Nothing
		'UPGRADE_NOTE: Object lclsPart_contr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPart_contr = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
	End Function
	
	'%insPostCR006_k: Esta función se encarga de validar los datos introducidos en la forma CR006_k
	Public Function insPostCR006_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nNumber As Integer, ByVal nBranchRei As Integer, ByVal nContraType As Integer, ByVal nYearSer As Integer, ByVal nCompany As Integer, ByVal nPerType As Integer, ByVal nPerNum As Integer, ByVal sBussiType As String, ByVal nCurrency As Integer) As Boolean
		
		insPostCR006_k = True
		
		On Error GoTo insPostCR006_k_Err
		
		'+Se inicializan los valores de la llave del contrato
		With Me
			insPostCR006_k = .Find(nNumber, nBranchRei, nContraType, nCompany, nPerType, nPerNum, sBussiType, nCurrency)
		End With
		
insPostCR006_k_Err: 
		If Err.Number Then
			insPostCR006_k = False
		End If
		On Error GoTo 0
	End Function
	
	'%Update: Actualización de un registro en el archivo de las cuentas técnicas
	Public Function Update(ByVal sCodispl As String) As Boolean
		Dim lrecupdCuentecn As eRemoteDB.Execute
		
		lrecupdCuentecn = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.updCuentecn'
		
		Me.nSal_f_comp = IIf((nClaim_ced = eRemoteDB.Constants.intNull), 0, nClaim_ced) + IIf((nComision = eRemoteDB.Constants.intNull), 0, nComision) + IIf((nGasto_reas = eRemoteDB.Constants.intNull), 0, nGasto_reas) + IIf((nImpuesto = eRemoteDB.Constants.intNull), 0, nImpuesto) + IIf((nR_car_prem = eRemoteDB.Constants.intNull), 0, nR_car_prem) + IIf((nR_car_sin = eRemoteDB.Constants.intNull), 0, nR_car_sin) + IIf((nRes_sinpen = eRemoteDB.Constants.intNull), 0, nRes_sinpen) + IIf((nRet_respre = eRemoteDB.Constants.intNull), 0, nRet_respre)
		
		Me.nSal_f_rein = IIf((nPrem_ced = eRemoteDB.Constants.intNull), 0, nPrem_ced) + IIf((nDev_respre = eRemoteDB.Constants.intNull), 0, nDev_respre) + IIf((nDev_rescla = eRemoteDB.Constants.intNull), 0, nDev_rescla) + IIf((nE_car_prem = eRemoteDB.Constants.intNull), 0, nE_car_prem) + IIf((nE_car_sin = eRemoteDB.Constants.intNull), 0, nE_car_sin) + IIf((nInter_prem = eRemoteDB.Constants.intNull), 0, nInter_prem) + IIf((nInter_sin = eRemoteDB.Constants.intNull), 0, nInter_sin) + IIf((nPart_benef = eRemoteDB.Constants.intNull), 0, nPart_benef)
		
		'+ A Favor del Asegurador
		If sCodispl = "CR006H" Then
			lrecupdCuentecn.StoredProcedure = "updCuentecn_Comp"
			lrecupdCuentecn.Parameters.Add("nClaim_ced", Me.nClaim_ced, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nComision", Me.nComision, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nGasto_reas", Me.nGasto_reas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nImpuesto", Me.nImpuesto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nR_car_prem", Me.nR_car_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nR_car_sin", Me.nR_car_sin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nRes_sinpen", Me.nRes_sinpen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nRet_respre", Me.nRet_respre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'+ A Favor del Asegurador
		ElseIf sCodispl = "CR006D" Then 
			lrecupdCuentecn.StoredProcedure = "updCuentecn_Rein"
			lrecupdCuentecn.Parameters.Add("nPrem_ced", Me.nPrem_ced, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nDev_respre", Me.nDev_respre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nDev_rescla", Me.nDev_rescla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nE_car_prem", Me.nE_car_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nE_car_sin", Me.nE_car_sin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nInter_prem", Me.nInter_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nInter_sin", Me.nInter_sin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecupdCuentecn.Parameters.Add("nPart_benef", Me.nPart_benef, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
		
		With lrecupdCuentecn
			.Parameters.Add("nType_rel", Me.nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdConsec", Me.nIdConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", Me.nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", Me.nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_acc", Me.sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_per", Me.nType_per, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriody", Me.nPeriody, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", Me.nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", Me.nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If sCodispl = "CR006H" Then
				.Parameters.Add("nSal_f_comp", Me.nSal_f_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nSal_f_rein", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nSal_f_comp", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nSal_f_rein", Me.nSal_f_rein, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_ser", Me.nYear_ser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdCuentecn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdCuentecn = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'%insPostCR006D: Esta función se encarga de realizar las actualizaciones en las
	'%diferentes tablas involucradas
	Public Function insPostCR006D(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nReinsurance As Integer, ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal nType As Integer, ByVal nYearSer As Integer, ByVal nCompany As Integer, ByVal nPerType As Integer, ByVal nPerNum As Integer, ByVal sBussiType As String, ByVal nCurrency As Integer, ByVal nPrem_ced As Double, ByVal nPart_benef As Double, ByVal nDev_respre As Double, ByVal nDev_rescla As Double, ByVal nInter_prem As Double, ByVal nInter_sin As Double, ByVal nE_car_prem As Double, ByVal nE_car_sin As Double, ByVal nRequestnu As Integer, ByVal nIdConse As Integer, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostCR006D_Err
		
		insPostCR006D = True
		
		If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			With Me
				.nType_rel = nReinsurance
				.nIdConsec = nIdConse
				.nNumber = nNumber
				.nType = nType
				.nBranch = nBranch
				.sType_acc = sBussiType
				.nType_per = nPerType
				.nPeriody = nPerNum
				.nCurrency = nCurrency
				.nCompany = nCompany
				.nDev_rescla = nDev_rescla
				.nDev_respre = nDev_respre
				.nE_car_prem = nE_car_prem
				.nE_car_sin = nE_car_sin
				.nInter_prem = nInter_prem
				.nInter_sin = nInter_sin
				.nPart_benef = nPart_benef
				.nPrem_ced = nPrem_ced
				.nYear_ser = nYear_ser
				.nRequestnu = nRequestnu
				.nUsercode = nUsercode
				
				If Not .nRequestnu > 0 Then
					insPostCR006D = .Update("CR006D")
				End If
			End With
		End If
		
insPostCR006D_Err: 
		If Err.Number Then
			insPostCR006D = False
		End If
	End Function
	
	'%insPostCR006H: Esta función se encarga de realizar las actualizaciones en las
	'%diferentes tablas involucradas
	Public Function insPostCR006H(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nReinsurance As Integer, ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal nType As Integer, ByVal nYearSer As Integer, ByVal nCompany As Integer, ByVal nPerType As Integer, ByVal nPerNum As Integer, ByVal sBussiType As String, ByVal nCurrency As Integer, ByVal nRet_respre As Double, ByVal nRes_sinpen As Double, ByVal nR_car_prem As Double, ByVal nR_car_sin As Double, ByVal nGasto_reas As Double, ByVal nComision As Double, ByVal nImpuesto As Double, ByVal nClaim_ced As Double, ByVal nRequestnu As Integer, ByVal nIdConse As Integer, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostCR006H_Err
		
		insPostCR006H = True
		
		If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			With Me
				.nType_rel = nReinsurance
				.nIdConsec = nIdConse
				.nNumber = nNumber
				.nBranch = nBranch
				.nType = nType
				.nYear_ser = nYear_ser
				.nCompany = nCompany
				.nType_per = nType_per
				.nPeriody = nPeriody
				.sType_acc = sType_acc
				.nCurrency = nCurrency
				.nRet_respre = nRet_respre
				.nRes_sinpen = nRes_sinpen
				.nR_car_prem = nR_car_prem
				.nR_car_sin = nR_car_sin
				.nGasto_reas = nGasto_reas
				.nComision = nComision
				.nImpuesto = nImpuesto
				.nClaim_ced = nClaim_ced
				.nRequestnu = nRequestnu
				.nUsercode = nUsercode
				
				If Not .nRequestnu > 0 Then
					insPostCR006H = .Update("CR006H")
				End If
			End With
		End If
		
insPostCR006H_Err: 
		If Err.Number Then
			insPostCR006H = False
		End If
	End Function
	
	'%DefaultValues: Esta función habilita e inhabilita de los campos de la forma.
	Public Sub DefaultValues(ByVal sCodispl As String, ByVal nPerType As Integer, ByVal nPerNum As Integer, ByVal nAction As Integer)
		If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			Select Case sCodispl
				
				'+A favor del reasegurador
				Case "CR006D"
					If nPerType = 5 Then
						blnPremCed = True
						blnDevResPre = True
						blnInterPrem = True
						blnECarPrem = True
						blnDevResCla = True
						blnInterSin = True
						blnECarSin = True
						blnPartBenef = False
					Else
						blnPremCed = False
						blnDevResPre = False
						blnInterPrem = False
						blnECarPrem = False
						blnDevResCla = False
						blnInterSin = False
						blnECarSin = False
						blnPartBenef = True
					End If
					
					If nPerNum <> 1 Then
						blnECarPrem = True
						blnECarSin = True
					Else
						If Not nPerType = 5 Then
							blnECarPrem = False
						Else
							blnECarPrem = True
						End If
					End If
					
				Case "CR006H"
					If nPerType = 5 Then
						blnRetResPre = True
						blnRCarPrem = True
						blnGastoReas = False
						blnClaimCed = True
						blnResSinPen = True
						blnRCarSin = True
						blnCommission = True
						blnImpuesto = True
					Else
						blnRetResPre = False
						blnGastoReas = True
						blnClaimCed = False
						blnResSinPen = False
						blnRCarSin = False
						blnRCarPrem = False
						blnCommission = False
						blnImpuesto = False
					End If
					
					If nPerNum <> 1 Then
						blnRCarPrem = True
						blnRCarSin = True
					End If
			End Select
		End If
		
	End Sub
	
	'%insValCRL010_K: esta función se encarga de validar, masiva y puntualmente, los campos del grid
	Public Function insValCRL010_K(ByVal sAction As String, ByVal sCodispl As String, ByVal nPerType As Integer, ByVal nPerNum As Integer, ByVal nYear As Integer, ByVal nContraType As Integer, ByVal nCompany As Integer, ByVal nBranchRei As Integer, ByVal nCurrency As Integer) As String
		
		Dim lclsErrors As eFunctions.Errors
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValCRL010_K_Err
		
		'+ Validación del campo Año.
		
		If nYear = 0 Or nYear = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage(sCodispl, 1116)
		End If
		
		'+ Validación del campo período-número
		
		Select Case nPerType
			Case 1
				If Not (nPerNum = 1 Or nPerNum = 2) Then
					lclsErrors.ErrorMessage(sCodispl, 6077)
				End If
				
			Case 2
				If Not (nPerNum = 1 Or nPerNum = 2 Or nPerNum = 3 Or nPerNum = 4) Then
					lclsErrors.ErrorMessage(sCodispl, 6077)
				End If
			Case 3
				If Not (nPerNum = 1 Or nPerNum = 2 Or nPerNum = 3 Or nPerNum = 4 Or nPerNum = 5 Or nPerNum = 6 Or nPerNum = 7 Or nPerNum = 8 Or nPerNum = 9 Or nPerNum = 10 Or nPerNum = 11 Or nPerNum = 12) Then
					lclsErrors.ErrorMessage(sCodispl, 6077)
				End If
		End Select
		
		insValCRL010_K = lclsErrors.Confirm()
		
insValCRL010_K_Err: 
		If Err.Number Then
			insValCRL010_K = insValCRL010_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
End Class







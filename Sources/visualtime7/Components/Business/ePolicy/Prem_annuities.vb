Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Public Class Prem_annuities
	'%-------------------------------------------------------%'
	'% $Workfile:: Prem_annuities.cls                       $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 27                                       $%'
	'%-------------------------------------------------------%'
	
	'-Propiedades según la tabla en el sistema el 08/07/2002
	'-La llave primaria corresponde a sCertype , nBranch, nProduct, nPolicy, nCertif, sClient, dEffecdate, nId
	
	'Column_name               Type                        Computed   Length Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	'------------------------  -------------------------   --------   ------ ----- ----- -------- ------------------  --------------------
	Public sCertype As String 'char       no         1      no    no       no
	Public nBranch As Integer 'smallint   no         2      5     0        no    (n/a)               (n/a)
	Public nProduct As Integer 'smallint   no         2      5     0        no    (n/a)               (n/a)
	Public nPolicy As Double 'int        no         4      10    0        no    (n/a)               (n/a)
	Public nCertif As Double 'int        no         4      10    0        no    (n/a)               (n/a)
	Public nIndrecdep As Integer
	Public nReceipt As Double
	Public nPrem_quot As Double
	Public nRate_disc As Double
	Public nNom_valbon As Double
	Public dIssuedatbon As Date
	Public dExpirdatbon As Date
	Public mlngUsercode As Integer
	Public nCurrency As Integer
	Public nId As Integer
	
	Enum TypeDocument
		clngDocReceipt = 1 '- Recibos
		clngDocDraft = 2 '- Cuotas de financiamiento
		clngDocBulletin = 3 '- Boletines
		clngDocPrimAdi = 4 '- Prima adicional
		clngDocPrimExc = 5 '- Prima exceso
		clngDocImprove_lo = 6 '- Abonos préstamo
		clngDocProponum = 7 '- Propuestas
		clngDocInterest = 8 '- Interes financiero
		clngDocColl_exp = 9 '- Gastos financieros
		clngDocLoansInt = 10 '- Interes por préstamo
		clngDocCountInd = 11 '- Cuenta indivudual
		clngDocReliqpremium = 12 '- Reliquidación de prima
		clngDocBonuss = 13 '- Bono de reconocimiento
		clngDocComplBonus = 14 '- Complemento bono de reconocimiento
		clngDocExBonuss = 15 '- Bono exonerado politicio y adic.
		clngPrivatePremium = 16 '- Prima renta privada
	End Enum
	
	Public bBonus As Boolean
	Public bCBonus As Boolean
	
	'%IsExist: Esta rutina es la encargada de evitar registros duplicados.
	Public Function IsExist(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecReaPrem_annuities_v As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		lrecReaPrem_annuities_v = New eRemoteDB.Execute
		With lrecReaPrem_annuities_v
			.StoredProcedure = "ReaPrem_annuities_v"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = .Parameters("nExist").Value > 0
			End If
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaPrem_annuities_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaPrem_annuities_v = Nothing
	End Function
	
	'%InsUpdPrem_annuities: Realiza la actualización de la tabla
	Private Function InsUpdPrem_annuities(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdPrem_annuities As eRemoteDB.Execute
		
		On Error GoTo InsUpdPrem_annuities_Err
		lrecInsUpdPrem_annuities = New eRemoteDB.Execute
		'+ Definición de store procedure InsUpdPrem_annuities al 01-23-2003 13:57:16
		With lrecInsUpdPrem_annuities
			.StoredProcedure = "InsUpdPrem_annuities"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndrecdep", nIndrecdep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_quot", nPrem_quot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate_disc", nRate_disc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNom_valbon", nNom_valbon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssuedatbon", dIssuedatbon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdatbon", dExpirdatbon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdPrem_annuities = .Run(False)
		End With
		
InsUpdPrem_annuities_Err: 
		If Err.Number Then
			InsUpdPrem_annuities = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdPrem_annuities may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdPrem_annuities = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdPrem_annuities(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdPrem_annuities(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdPrem_annuities(3)
	End Function
	
	'% valPrem_annuities_Bonus: verifica la existencia de bonos de reconocimientos para la póliza
	Public Function valPrem_annuities_Bonus(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo valPrem_annuities_Bonus_Err
		lclsRemote = New eRemoteDB.Execute
		With lclsRemote
			.StoredProcedure = "valPrem_annuities_MR"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists_bonus", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists_cbonus", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			valPrem_annuities_Bonus = .Run(False)
			bBonus = .Parameters("nExists_bonus").Value = 1
			bCBonus = .Parameters("nExists_cbonus").Value = 1
		End With
		
valPrem_annuities_Bonus_Err: 
		If Err.Number Then
			valPrem_annuities_Bonus = False
		End If
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		On Error GoTo 0
	End Function
	
	'%insCalBonus: Función que retorna el valor del recalculo del bono
	Public Function insCalBonus(ByVal nRate_disc As Double, ByVal nNom_valbon As Double, ByVal dIssuedatbon As Date, ByVal dExpirdatbon As Date, ByVal nCollecDocTyp As TypeDocument, ByVal nCurrency As Integer, ByVal dEffecdate1 As Date) As Double
		Dim nValueCurrency_local As Double
		Dim nCurrency_exchange As Double
		Dim nDifmonth_calculate As Integer
		Dim lclsBal_histor As eLedge.Bal_histor
		Dim lclsExchange As eGeneral.Exchange
		Dim nValue1 As Double
		Dim nValue2 As Double
		Dim nDif_year As Integer
		Dim nDif_month As Integer
		Dim nRest As Integer
		Dim nTax_Bonus As Double
		Dim dIssuedatbon_aux As Date
		Dim dEffecdate As Date
		Dim dEffecdate_Change As Date
		Dim nMonth_aux As Integer
		Dim nYear_aux As Integer
		Dim nIpc1 As Double
		Dim nIpc2 As Double
		
		If (nCollecDocTyp = TypeDocument.clngDocBonuss Or nCollecDocTyp = TypeDocument.clngDocExBonuss) Then
			nTax_Bonus = 0.04
		Else
			nTax_Bonus = 0
		End If
		
		'+ Diferencia en años enteros entre fecha de venc del bono y la fecha de emisión
		nDif_year = System.Math.Abs(DateDiff(Microsoft.VisualBasic.DateInterval.Year, dIssuedatbon, dExpirdatbon))
		
		'+ Resto entre división de la diferencia en meses de fecha venc bono y la fecha de emisión
		nDif_month = System.Math.Abs(DateDiff(Microsoft.VisualBasic.DateInterval.Month, dIssuedatbon, dExpirdatbon))
		nRest = nDif_month Mod 12
		
		'+ Calcular Valor1 del funcional CO001
		nValue1 = (nNom_valbon * ((1 + nTax_Bonus) ^ nDif_year)) * (1 + nTax_Bonus) * (nRest / 12)
		
		'+ Fecha de cálculo: último día mes anterior fecha recep bono si fecha recep bono (día) < 8,
		'+ en caso contrario es igual a último día del mes de recepción
		If VB.Day(dIssuedatbon) < 8 Then
			dEffecdate = DateAdd(Microsoft.VisualBasic.DateInterval.Day, -(VB.Day(dIssuedatbon)), dIssuedatbon)
		Else
			dEffecdate = dIssuedatbon
		End If
		
		'+ Calcular diferencia de meses entre fecha fecha emisión bono y fecha de cálculo menos 1 mes
		nDifmonth_calculate = System.Math.Abs(DateDiff(Microsoft.VisualBasic.DateInterval.Month, dEffecdate, dIssuedatbon))
		dEffecdate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, dEffecdate1)
		
		dIssuedatbon_aux = DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, dIssuedatbon)
		lclsBal_histor = New eLedge.Bal_histor
		
		'+ Recuperar IPC a la fecha de cálculo menos 1 mes (IPC2 del funcional)
		nIpc2 = lclsBal_histor.insReaReval_fact(Year(dEffecdate), Month(dEffecdate), 5)
		
		'+ Recuperar IPC a la fecha emisión bono menos 1 mes (IPC1 del funcional)
		nIpc1 = lclsBal_histor.insReaReval_fact(Year(dIssuedatbon_aux), Month(dIssuedatbon_aux), 5)
		
		nValue2 = nValue1 * (nIpc2 / nIpc1)
		nValueCurrency_local = (nValue2 / (1 + nTax_Bonus / 100)) ^ (nDifmonth_calculate / 12)
		dEffecdate_Change = DateAdd(Microsoft.VisualBasic.DateInterval.Day, -(VB.Day(dIssuedatbon)), dIssuedatbon)
		lclsExchange = New eGeneral.Exchange
		Call lclsExchange.Find(nCurrency, dEffecdate_Change, True)
		nCurrency_exchange = lclsExchange.nExchange
		insCalBonus = nValueCurrency_local / nCurrency_exchange
		
insCalBonus_Err: 
		If Err.Number Then
			insCalBonus = 0
		End If
		'UPGRADE_NOTE: Object lclsBal_histor may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBal_histor = Nothing
		'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExchange = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValRV778Upd: Realiza la validación de los campos a actualizar en la ventana RV778
	Public Function InsValRV778Upd(ByVal sCodispl As String, ByVal nIndrecdep As Integer, ByVal nPrem_quot As Double, ByVal nRate_disc As Double, ByVal nNom_valbon As Double, ByVal dIssuedatbon As Date, ByVal dExpirdatbon As Date, ByVal nCurrency As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValRV778Upd_Err
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			'+ Validaciones de tipo de concepto
			If nIndrecdep = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60419)
			End If
			
			'+ Validaciones de la Prima cotizada
			If nPrem_quot = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60420)
			End If
			
			'+ Validaciones de la Tasa de descuento del bono
			If nRate_disc = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60415)
			End If
			
			'+ Validaciones del valor nominal del bono
			If nNom_valbon = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60416)
			End If
			
			'+ Validaciones de la Fecha de emisión del bono
			If dIssuedatbon = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 60417)
			End If
			
			'+ Validaciones de la Fecha de vencimiento del bono
			If dExpirdatbon = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 60418)
			End If
			
			'+ Validaciones del campo moneda
			If nCurrency = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1351)
			End If
			
			InsValRV778Upd = .Confirm
		End With
		
InsValRV778Upd_Err: 
		If Err.Number Then
			InsValRV778Upd = "InsValRV778Upd: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%InsPostRV778Upd: Se realiza la actualización de los datos en la ventana RV778
	Public Function InsPostRV778Upd(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nId As Integer, ByVal nUsercode As Integer, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nReceipt As Integer = 0, Optional ByVal nIndrecdep As Integer = 0, Optional ByVal nPrem_quot As Double = 0, Optional ByVal nRate_disc As Double = 0, Optional ByVal nNom_valbon As Double = 0, Optional ByVal dIssuedatbon As Date = #12:00:00 AM#, Optional ByVal dExpirdatbon As Date = #12:00:00 AM#, Optional ByVal nCurrency As Integer = 0, Optional ByVal nCount As Integer = 0) As Boolean
		Dim lblnUpdPw As Boolean
		Dim lclsPolicy_Win As Policy_Win
		
		On Error GoTo InsPostRV778Upd_Err
		mstrContent = String.Empty
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nId = nId
			.nReceipt = IIf(nReceipt = eRemoteDB.Constants.intNull, 0, nReceipt)
			.nIndrecdep = nIndrecdep
			.nPrem_quot = nPrem_quot
			.nRate_disc = nRate_disc
			.nNom_valbon = nNom_valbon
			.dIssuedatbon = dIssuedatbon
			.dExpirdatbon = dExpirdatbon
			mlngUsercode = nUsercode
			.nCurrency = nCurrency
			'+ Se efectúa el proceso según la acción.
			Select Case sAction
				Case "Add"
					If .Add Then
						InsPostRV778Upd = True
						If nCount <= 0 Then
							mstrContent = "2"
							lblnUpdPw = True
						End If
					End If
					
				Case "Update"
					InsPostRV778Upd = .Update
					
				Case "Del"
					If .Delete Then
						InsPostRV778Upd = True
						If Not IsExist(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
							mstrContent = "1"
							lblnUpdPw = True
						End If
					End If
			End Select
		End With
		
		If lblnUpdPw Then
			lclsPolicy_Win = New ePolicy.Policy_Win
			Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "RV778", mstrContent)
			
			Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA017", "1")
			
		End If
		
InsPostRV778Upd_Err: 
		If Err.Number Then
			InsPostRV778Upd = False
		End If
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
		On Error GoTo 0
	End Function
	
	'*sContent: Obtiene el indicador de contenido de la transacción
	Public ReadOnly Property sContent() As String
		Get
			sContent = mstrContent
		End Get
	End Property
	
	'%Find: Función que retorna la lectura de registros en la tabla 'Prem_annuities' para un recibo especifico
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double) As Boolean
		Dim lrecreaPrem_annuities As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaPrem_annuities = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaPrem_annuities'
		'+Información leída el 08/07/2002
		
		With lrecreaPrem_annuities
			.StoredProcedure = "reaPrem_annuities"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nIndrecdep = .FieldToClass("nIndrecdep")
				nPrem_quot = .FieldToClass("nPrem_quot")
				nRate_disc = .FieldToClass("nRate_disc")
				nNom_valbon = .FieldToClass("nNom_valbon")
				dIssuedatbon = .FieldToClass("dIssuedatbon")
				dExpirdatbon = .FieldToClass("dExpirdatbon")
				Find = True
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPrem_annuities may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPrem_annuities = Nothing
	End Function
End Class







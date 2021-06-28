Option Strict Off
Option Explicit On
Public Class FinanceDrafts
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: FinanceDrafts.cls                        $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 13/10/04 11.15                               $%'
	'% $Revision:: 38                                       $%'
	'%-------------------------------------------------------%'
	
	'- local variable to hold collection
	Private mCol As Collection
	
	Private nContrat As Double
	Private nClaim As Double
	
	Public nAmountPending As Double
	Public nQuotaPending As Double
	
	'% Find_ClaimDraftCollect: Searches for the drafts discounted for a given claim
	'% Find_ClaimDraftCollect: Busca todos los giros descontados a partir de un
	'% código de siniestro dado.
	Public Function Find_ClaimDraftCollect(ByVal llngnClaim As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaClaimdraftcollect As eRemoteDB.Execute
		Dim lclsFinanceDraft As FinanceDraft
		
		On Error GoTo Find_ClaimDraftCollect_Err
		
		If llngnClaim <> nClaim Or lblnFind Then
			
			lrecreaClaimdraftcollect = New eRemoteDB.Execute
			
			'+ Stored procedure parameters definition 'insudb.reaClaimdraftcollect'
			'+ Data of 10/18/1999 09:51:56 AM
			'+ Definición de parámetros para stored procedure 'insudb.reaClaimdraftcollect'
			'+ Información leída el 18/10/1999 09:51:56 AM
			With lrecreaClaimdraftcollect
				.StoredProcedure = "reaClaimdraftcollect"
				.Parameters.Add("nClaim", llngnClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nClaim = llngnClaim
					While Not .EOF
						lclsFinanceDraft = New FinanceDraft
						lclsFinanceDraft.nStatInstanc = FinanceDraft.eStatusInstance.eftQuery
						lclsFinanceDraft.dStat_date = Today
						lclsFinanceDraft.nIntermed = 0
						lclsFinanceDraft.nIntammou = 0
						lclsFinanceDraft.dExpirdat = .FieldToClass("dExpirdat")
						lclsFinanceDraft.nDraft = .FieldToClass("nDraft")
						lclsFinanceDraft.nContrat = .FieldToClass("nContrat")
						lclsFinanceDraft.nCommission = 0
						lclsFinanceDraft.nAmount_net = 0
						lclsFinanceDraft.nStat_draft = 0
						lclsFinanceDraft.nAmount = .FieldToClass("nAmount")
						lclsFinanceDraft.nClaim = llngnClaim
						lclsFinanceDraft.sClient = .FieldToClass("sClient")
						lclsFinanceDraft.sCliename = .FieldToClass("sCliename")
						Call Add(lclsFinanceDraft)
						'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsFinanceDraft = Nothing
						.RNext()
					End While
					.RCloseRec()
					Find_ClaimDraftCollect = True
				Else
					Find_ClaimDraftCollect = False
				End If
			End With
		Else
			Find_ClaimDraftCollect = True
		End If
Find_ClaimDraftCollect_Err: 
		If Err.Number Then
			Find_ClaimDraftCollect = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaClaimdraftcollect may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClaimdraftcollect = Nothing
	End Function
	
	'**%Add: adds a new instance of the "FinanceDraft" class to the collection
	'%Add: Añade una nueva instancia de la clase "FinanceDraft" a la colección
	Public Function Add(ByRef objClass As FinanceDraft) As FinanceDraft
		
		If objClass Is Nothing Then
			objClass = New FinanceDraft
		End If
		
		mCol.Add(objClass, "D" & objClass.nContrat & objClass.nDraft)
		
		Add = objClass
		
	End Function
	
	
	'% Find: This method returns TRUE or FALSE depending if the records exists in the table "Financ_dra_all"
	'% Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'% tabla "Financ_dra_all"
	Public Function Find(ByVal Contrat As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaFinanc_dra_all As eRemoteDB.Execute
		Dim lobjFinanceDraft As FinanceDraft
		
		On Error GoTo Find_Err
		'+ se inicializa monto total de cuotas pendientes
		nAmountPending = 0
		nQuotaPending = 0
		
		'+ Stored procedure parameters definition 'insudb.reaFinanc_dra_all'
		'+ Data of 09/03/1999 09:33:33 AM
		'+ Definición de parámetros para stored procedure 'insudb.reaFinanc_dra_all'
		'+ Información leída el 03/09/1999 09:33:33 AM
		If Contrat <> nContrat Or lblnFind Then
			
			lrecreaFinanc_dra_all = New eRemoteDB.Execute
			
			With lrecreaFinanc_dra_all
				.StoredProcedure = "reaFinanc_dra_all"
				.Parameters.Add("nContrat", Contrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nContrat = Contrat
					Do While Not .EOF
						lobjFinanceDraft = New FinanceDraft
						lobjFinanceDraft.nStatInstanc = FinanceDraft.eStatusInstance.eftQuery
						lobjFinanceDraft.dStat_date = .FieldToClass("dStat_date")
						lobjFinanceDraft.nIntermed = .FieldToClass("nIntermed")
						lobjFinanceDraft.nIntammou = .FieldToClass("nIntammou")
						lobjFinanceDraft.dExpirdat = .FieldToClass("dExpirdat")
						lobjFinanceDraft.nDraft = .FieldToClass("nDraft")
						lobjFinanceDraft.nContrat = nContrat
						lobjFinanceDraft.nCommission = .FieldToClass("nCommission")
						lobjFinanceDraft.nAmount_net = .FieldToClass("nAmount_net")
						lobjFinanceDraft.nStat_draft = .FieldToClass("nStat_draft")
						lobjFinanceDraft.sStat_draft = .FieldToClass("sStat_draft")
						lobjFinanceDraft.nAmount = .FieldToClass("nAmount")
						lobjFinanceDraft.nStatPrint = .FieldToClass("nStatPrint")
						lobjFinanceDraft.nClaim = .FieldToClass("nClaim")
						lobjFinanceDraft.dLimitdate = .FieldToClass("dLimitdate")
						lobjFinanceDraft.nWay_Pay = .FieldToClass("nWay_Pay")
						lobjFinanceDraft.sDesWay_Pay = .FieldToClass("sWay_Pay")
						
						'+ Calculo del total de las cuotas pendiente del recibo
						If lobjFinanceDraft.nStat_draft = 1 Then
							nAmountPending = nAmountPending + lobjFinanceDraft.nAmount
							nQuotaPending = nQuotaPending + 1
						End If
						Call Add(lobjFinanceDraft)
						'UPGRADE_NOTE: Object lobjFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lobjFinanceDraft = Nothing
						.RNext()
					Loop 
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
		Else
			Find = True
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaFinanc_dra_all may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFinanc_dra_all = Nothing
	End Function
	
	'% Find_Contrat: Read the drafts of a specific contract
	'% Find_Contrat: Permite obtener la información de los giros pertenecientes a un contrato,
	'% independientemente de la transacción que se este ejecutando
	Public Function Find_Contrat(ByVal nTransaction As Integer, ByVal nContrat As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaFinanc_dra_all As eRemoteDB.Execute
		Dim lclsFinanceDraft As FinanceDraft
		Dim ldblBalance_Aux As Double
		Dim lclsFinance_co As financeCO
		
		On Error GoTo Find_Contrat_Err
		
		lrecreaFinanc_dra_all = New eRemoteDB.Execute
		
		'+ Stored procedure parameters definition 'insudb.reaFinanc_dra_all'
		'+ Data of 09/03/1999 09:33:33 AM
		'+ Definición de parámetros para stored procedure 'insudb.reaFinanc_dra_all'
		'+ Información leída el 03/09/1999 09:33:33 AM
		
		With lrecreaFinanc_dra_all
			.StoredProcedure = "reaFinanc_dra_all"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'**+ Contract data query
			'+Consulta de los datos de un contrato
			If .Run Then
				Find_Contrat = True
				
				lclsFinance_co = New financeCO
				Call lclsFinance_co.Find(nContrat, dEffecdate)
				
				ldblBalance_Aux = lclsFinance_co.nAmount
				Do While Not .EOF
					lclsFinanceDraft = New FinanceDraft
					lclsFinanceDraft.nStatInstanc = FinanceDraft.eStatusInstance.eftQuery
					lclsFinanceDraft.dStat_date = .FieldToClass("dStat_date")
					lclsFinanceDraft.nIntermed = .FieldToClass("nIntermed")
					lclsFinanceDraft.nIntammou = .FieldToClass("nIntammou")
					lclsFinanceDraft.dExpirdat = .FieldToClass("dExpirdat")
					lclsFinanceDraft.dLimitdate = .FieldToClass("dLimitdate")
					lclsFinanceDraft.nDraft = .FieldToClass("nDraft")
					lclsFinanceDraft.nContrat = nContrat
					lclsFinanceDraft.nCommission = .FieldToClass("nCommission")
					lclsFinanceDraft.nAmount_net = .FieldToClass("nAmount_net")
					lclsFinanceDraft.nStat_draft = .FieldToClass("nStat_draft")
					lclsFinanceDraft.sStat_draft = .FieldToClass("sStat_draft")
					lclsFinanceDraft.nAmount = .FieldToClass("nAmount")
					lclsFinanceDraft.nStatPrint = .FieldToClass("nStatPrint")
					lclsFinanceDraft.nClaim = .FieldToClass("nClaim")
					lclsFinanceDraft.nWay_Pay = .FieldToClass("nWay_Pay")
					lclsFinanceDraft.sDesWay_Pay = .FieldToClass("sWay_Pay")
					
					If lclsFinance_co.sPayment_in = financeCO.EPayment_in.eafirmative Then
						ldblBalance_Aux = ldblBalance_Aux - lclsFinanceDraft.nAmount_net
					Else
						ldblBalance_Aux = ldblBalance_Aux - lclsFinanceDraft.nAmount
					End If
					lclsFinanceDraft.nBalance = IIf(ldblBalance_Aux < 0, 0, ldblBalance_Aux)
					
					Call Add(lclsFinanceDraft)
					'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFinanceDraft = Nothing
					
					.RNext()
				Loop 
				.RCloseRec()
				
				'**+Insert,update or recovery of the data of a contract
				'+Registro,Modificación o Recuperación de los datos de un contrato
			Else
				Find_Contrat = Find_AddUpdRecov(nContrat, nTransaction)
			End If
		End With
		
Find_Contrat_Err: 
		If Err.Number Then
			Find_Contrat = False
		End If
		'UPGRADE_NOTE: Object lrecreaFinanc_dra_all may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFinanc_dra_all = Nothing
		'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceDraft = Nothing
		'UPGRADE_NOTE: Object lclsFinance_co may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinance_co = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_AddUpdRecov: Allows to obtain the information to be shown in the grid in the case that is being created a contract
	'% Find_AddUpdRecov:Permite obtener la información a ser mostrada  en el grid en el caso
	'% de estar creando un contrato
	Private Function Find_AddUpdRecov(ByVal nContrat As Double, ByVal nTransaction As Integer) As Boolean
		Dim lclsExchange As eGeneral.Exchange
		Dim lcolFinanceDraft As eFinance.FinanceDraft
		Dim lclsFinanceCO As eFinance.financeCO
		Dim lclsFinancePres As eFinance.FinancePres
		Dim lclsTables As eFunctions.Tables
		Dim lclsProduct As Object
		
		'- Se definen las variable que va a contener la fecha de vencimiento del giro y
		'- la fecha limite de pago
		Dim ldtmExpirdat As Date
		Dim ldtmLimitDate As Date
		'- Se definen las variable que contienen el Saldo y Saldo neto restante
		Dim llngBalance As Double
		Dim llngBalance_net As Double
		'- Se define la variable que contiene la frecuencia de los giros (Mensual o Trimestral)
		Dim lintFrecuency As Integer
		
		Dim ldblFactor As Double
		Dim lintIndex As Integer
		Dim ldblCommission As Double
		Dim ldblCom_afecDra As Double
		Dim ldblCom_exenDra As Double
		Dim ldblCom_afecDraQ As Double
		Dim ldblCom_exenDraQ As Double
		
		Dim lintQ_draft As Integer
		'-Monto neto del contrato
		Dim ldblAmoCO_Net As Double
		'- Importe del giro en proceso
		Dim ldblCurAmoDra As Double
		'- Importe neto del giro en proceso
		Dim ldblCurAmoDra_Net As Double
		'- Monto calculado de cada cuota
		Dim ldblAmoDraCalc As Double
		'- Monto neto calculado de cada cuota
		Dim ldblAmoDraCalc_Net As Double
		'- Monto neto de primera cuota
		Dim ldblInitial_Net As Double
		
		Dim lintIndicator As FinanceDraft.eStatusInstance
		Dim lstrWay_pay_desc As String
		Dim lstrStatus_desc As String
		Dim nQDays_DifQuo As Short
		
		Find_AddUpdRecov = True
		
		On Error GoTo Find_AddUpdRecov_Err
		
		lclsFinanceCO = New financeCO
		Call lclsFinanceCO.Find(nContrat, Today)
		
		ldtmExpirdat = lclsFinanceCO.dEffecdate
		lintQ_draft = lclsFinanceCO.nQ_draft
		
		'+Se determina si contrato esta financiando recibo
		lclsFinancePres = New eFinance.FinancePres
		If lclsFinancePres.Find(nContrat) Then
			'+Si está financiando, se puede determinar el producto asociado a la poliza
			'+para saber la cantidad de días minimo entre 1ra y 2da cuota
			lclsProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
			Call lclsProduct.Find(lclsFinancePres.Item(1).nBranch, lclsFinancePres.Item(1).nProduct, ldtmExpirdat)
			'+Condicion por si viene nulo
			If lclsProduct.nQDays_DifQuo > 0 Then
				nQDays_DifQuo = lclsProduct.nQDays_DifQuo
			End If
			'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsProduct = Nothing
		End If
		'UPGRADE_NOTE: Object lclsFinancePres may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinancePres = Nothing
		
		lcolFinanceDraft = New FinanceDraft
		lintFrecuency = lcolFinanceDraft.Frecuency(lclsFinanceCO.nFrequency)
		If lintFrecuency = 0 Then
			lintFrecuency = 1
		End If
		
		'+ Recupera la comision de las cuotas '
		Call lcolFinanceDraft.Find_Commission_FI004(nContrat)
		ldblCom_afecDra = lcolFinanceDraft.nCom_afec
		ldblCom_exenDra = lcolFinanceDraft.nCom_exen
		'UPGRADE_NOTE: Object lcolFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFinanceDraft = Nothing
		
		ldblCom_afecDraQ = ldblCom_afecDra / lclsFinanceCO.nQ_draft
		ldblCom_exenDraQ = ldblCom_exenDra / lclsFinanceCO.nQ_draft
		
		ldblFactor = SearchFactor(lclsFinanceCO.nQ_draft, lclsFinanceCO.nInterest, lclsFinanceCO.dEffecdate)
		ldblFactor = ldblFactor * lclsFinanceCO.nQ_draft
		
		'+Monto total sin intereses
		'+Contrato fue registrado con intereses en las transacciones anteriores
		ldblAmoCO_Net = System.Math.Round(lclsFinanceCO.nAmount / ldblFactor, 6)
		
		'+Se inicializa monto de primera cuota (por si viene nula)
		If lclsFinanceCO.nInitial_or <= 0 Then
			lclsFinanceCO.nInitial_or = 0
		End If
		
		'+CALCULO DE MONTOS CON INTERES
		
		'+Se toma el total de cuotas
		lintIndex = lclsFinanceCO.nQ_draft
		'+Si ya existe monto para la primera cuota,
		'+se debe calcular el monto de las restantes
		If lclsFinanceCO.nInitial_or > 0 And lintIndex > 1 Then
			lintIndex = lintIndex - 1
		End If
		
		'+Se calcula monto de cuotas posteriores a la primera
		'+Se usa int( Round(x,7) * 1000000) / 1000000 para truncar a 6
		ldblAmoDraCalc = Int(System.Math.Round((lclsFinanceCO.nAmount - lclsFinanceCO.nInitial_or) / lintIndex, 7) * 1000000) / 1000000
		'+Si no habia cuota inicial, se asigna el mismo valor uniforme obtenido para todas
		If lclsFinanceCO.nInitial_or = 0 Then
			lclsFinanceCO.nInitial_or = ldblAmoDraCalc
		End If
		
		
		'+CALCULO DE MONTOS NETOS
		
		'+Si la primera cuota paga interes, monto neto se obtiene en funcion del total
		'+Para esto se hace cálculo inverso al usado para aplicar interes
		If lclsFinanceCO.sPayment_in = financeCO.EPayment_in.eafirmative Then
			ldblInitial_Net = Int(System.Math.Round(lclsFinanceCO.nInitial_or / ldblFactor, 7) * 1000000) / 1000000
			'+Si no paga interes, monto inicial y monto neto inicial son iguales
		Else
			ldblInitial_Net = lclsFinanceCO.nInitial_or
		End If
		
		'+Se calcula el monto neto de las cuotas posteriores a la primera
		If lclsFinanceCO.nQ_draft > 1 Then
			lintIndex = lclsFinanceCO.nQ_draft - 1
		Else
			lintIndex = 1
		End If
		ldblAmoDraCalc_Net = Int(System.Math.Round((ldblAmoCO_Net - ldblInitial_Net) / lintIndex, 7) * 1000000) / 1000000
		
		lclsTables = New eFunctions.Tables
		'+Descripcion de forma de pago
		Call lclsTables.GetDescription("TABLE5002", CStr(lclsFinanceCO.nWay_Pay))
		lstrWay_pay_desc = lclsTables.Descript
		'+Descripcion de estado de cuota (como son nuevas están pendientes)
		Call lclsTables.GetDescription("TABLE253", CStr(FinanceDraft.eStat_Draft.esdOutStatnding))
		lstrStatus_desc = lclsTables.Descript
		'UPGRADE_NOTE: Object lclsTables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTables = Nothing
		
		For lintIndex = 1 To lintQ_draft
			With lclsFinanceCO
				
				If lintIndex = 1 Then
					'+Calculo de fechas
					ldtmLimitDate = .dFirst_draf
					
					'+Calculo de montos
					ldblCurAmoDra = .nInitial_or
					ldblCurAmoDra_Net = ldblInitial_Net
					
					'+En el balance se resta la primera cuota al contrato
					llngBalance = .nAmount - ldblCurAmoDra
					llngBalance_net = ldblAmoCO_Net - ldblCurAmoDra_Net
					
				Else
					
					'+Calculo de fechas
					ldtmLimitDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, lintFrecuency, ldtmLimitDate)
					'+Se asigna día de pago
					ldtmLimitDate = DateSerial(Year(ldtmLimitDate), Month(ldtmLimitDate), .nBill_Day)
					'+La segunda cuota debe quedar un mínimo de N días después de la primera
					'+según lo definidos en el diseñador. Si es menor se pasa al período sgte
					If lintIndex = 2 Then
						'UPGRADE_WARNING: DateDiff behavior may be different. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"'
						If DateDiff(Microsoft.VisualBasic.DateInterval.Day, .dFirst_draf, ldtmLimitDate) < nQDays_DifQuo Then
							ldtmLimitDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, lintFrecuency, ldtmLimitDate)
						End If
					End If
					
					'+Calculo de montos
					ldblCurAmoDra = ldblAmoDraCalc
					ldblCurAmoDra_Net = ldblAmoDraCalc_Net
					
					'+Si es la ultima cuota se le acumula lo restante al monto
					If lintIndex = lintQ_draft Then
						ldblCurAmoDra = ldblCurAmoDra + System.Math.Round(llngBalance - ldblAmoDraCalc, 6)
						ldblCurAmoDra_Net = ldblCurAmoDra_Net + System.Math.Round(llngBalance_net - ldblAmoDraCalc_Net, 6)
					End If
					
					llngBalance = llngBalance - ldblCurAmoDra
					llngBalance_net = llngBalance_net - ldblCurAmoDra_Net
				End If
				
				Select Case nTransaction
					
					'+ Si la acción es agregar.
					Case financeCO.eFinanceTransac.eftAddContrat
						lintIndicator = FinanceDraft.eStatusInstance.eftNew
						
						'+ Si la acción es Modificar o Recuperar.
					Case financeCO.eFinanceTransac.eftUpDateContrat, financeCO.eFinanceTransac.eftRecoveryContrat
						
						'+ Si la cantidad de giros es mayor que la original
						If lintIndex > lintQ_draft Then
							lintIndicator = FinanceDraft.eStatusInstance.eftNew
						Else
							lcolFinanceDraft = New FinanceDraft
							If lcolFinanceDraft.Find(nContrat, lintIndex) Then
								lintIndicator = FinanceDraft.eStatusInstance.eftUpDate
							Else
								lintIndicator = FinanceDraft.eStatusInstance.eftNew
							End If
							'UPGRADE_NOTE: Object lcolFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							lcolFinanceDraft = Nothing
						End If
				End Select
				
				lcolFinanceDraft = New FinanceDraft
				lcolFinanceDraft.nStatInstanc = lintIndicator
				lcolFinanceDraft.dStat_date = .dEffecdate
				lcolFinanceDraft.nIntermed = eRemoteDB.Constants.intNull
				lcolFinanceDraft.nIntammou = eRemoteDB.Constants.intNull
				lcolFinanceDraft.dExpirdat = ldtmLimitDate
				lcolFinanceDraft.nDraft = lintIndex
				lcolFinanceDraft.nContrat = nContrat
				lcolFinanceDraft.nCommission = ldblCom_afecDraQ + ldblCom_exenDraQ
				lcolFinanceDraft.nCom_afec = ldblCom_afecDraQ
				lcolFinanceDraft.nCom_exen = ldblCom_exenDraQ
				lcolFinanceDraft.nAmount_net = ldblCurAmoDra_Net
				lcolFinanceDraft.nStat_draft = FinanceDraft.eStat_Draft.esdOutStatnding
				lcolFinanceDraft.sStat_draft = lstrStatus_desc
				lcolFinanceDraft.nAmount = ldblCurAmoDra
				lcolFinanceDraft.nClaim = nClaim
				lcolFinanceDraft.dLimitdate = ldtmLimitDate
				lcolFinanceDraft.nBalance = IIf(llngBalance < 0, 0, llngBalance)
				lcolFinanceDraft.nWay_Pay = .nWay_Pay
				lcolFinanceDraft.sDesWay_Pay = lstrWay_pay_desc
				Call Add(lcolFinanceDraft)
				'UPGRADE_NOTE: Object lcolFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lcolFinanceDraft = Nothing
			End With
		Next 
		
		'+ Gets the draft amount
		'+ Toma el importe de un giro
		lclsFinanceCO.nAmount_d = ldblAmoCO_Net
		
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		
		'+ If the quantity of drafts is minor than the original, the system marks the remaining drafts and then deletes them
		'+ Si se modificó el número de giros a un número menor, se marcan para eliminar los giros
		'+ mayores al número especificado.
		
		If mCol.Count() > lintIndex Then
			While mCol.Count() > lintIndex
				lcolFinanceDraft = mCol.Item(lintIndex + 1)
				lcolFinanceDraft.Delete()
				lintIndex = lintIndex + 1
				'UPGRADE_NOTE: Object lcolFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lcolFinanceDraft = Nothing
			End While
		End If
		
Find_AddUpdRecov_Err: 
		If Err.Number Then
			Find_AddUpdRecov = False
		End If
		
		'Set lcolFinancePres = Nothing
		'UPGRADE_NOTE: Object lcolFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFinanceDraft = Nothing
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		On Error GoTo 0
	End Function
	
	'+ SearchFactor: busca el factor en la tabla quotint para el calculo de cuotas
	Public Function SearchFactor(ByVal nDraft As Integer, ByVal nInterest As Double, ByVal dEffecdate As Date) As Double
		Dim lrecSearchFactor As eRemoteDB.Execute
		On Error GoTo SearchFactorErr
		
		SearchFactor = 0
		
		If nInterest = 0 Then
			If nDraft > 0 Then
				'+Se aplica el inverso de la cant de cuotas, para que al multiplicar
				'+por la cantidad de cuotas se anule y quede en 1: nDraft * (1 / nDraft) = 1
				SearchFactor = 1 / nDraft
			Else
				SearchFactor = 1
			End If
			
		Else
			
			lrecSearchFactor = New eRemoteDB.Execute
			
			'+ Stored procedure parameters definition 'insudb.updFinanc_dra_Status'
			'+ Data of 09/15/1999 11:38:28 AM
			'+ Definición de parámetros para stored procedure 'insudb.updFinanc_dra_Status'
			'+ Información leída el 15/09/1999 11:38:28 AM
			
			With lrecSearchFactor
				.StoredProcedure = "reatabquotint"
				.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					SearchFactor = .FieldToClass("nFactor")
				End If
			End With
		End If
		
SearchFactorErr: 
		If Err.Number Then
			SearchFactor = eRemoteDB.Constants.intNull
		End If
		'UPGRADE_NOTE: Object lrecSearchFactor may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecSearchFactor = Nothing
	End Function
	
	
	'% Update: Este método se encarga de actualizar registros en la tabla "FinanceDraft". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function UpDate() As Boolean
        Dim lclsFinanceDraft As FinanceDraft

        UpDate = True
		
		For	Each lclsFinanceDraft In mCol
			With lclsFinanceDraft
				Select Case .nStatInstanc
					Case FinanceDraft.eStatusInstance.eftNew
						UpDate = .Add
						.nStatInstanc = FinanceDraft.eStatusInstance.eftQuery
					Case FinanceDraft.eStatusInstance.eftUpDate
						UpDate = .UpDate
					Case FinanceDraft.eStatusInstance.eftDelete
						UpDate = .Delete
				End Select
			End With
		Next lclsFinanceDraft
		'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceDraft = Nothing
	End Function
	
	'% Find_policy_finan_dra: se buscan las cuotas cobradas, y las cuotas pendientes en vía de cobro
	Public Function Find_policy_finan_dra(ByVal nContrat As Double, ByVal dEffecdate As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		Dim lclsFinanceDraft As FinanceDraft
		
		On Error GoTo Find_policy_finan_dra_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "reaPolicy_finan_dra"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsFinanceDraft = New FinanceDraft
					lclsFinanceDraft.nStatInstanc = FinanceDraft.eStatusInstance.eftQuery
					lclsFinanceDraft.dStat_date = .FieldToClass("dStat_date")
					lclsFinanceDraft.nIntermed = .FieldToClass("nIntermed")
					lclsFinanceDraft.nIntammou = .FieldToClass("nIntammou")
					lclsFinanceDraft.dExpirdat = .FieldToClass("dExpirdat")
					lclsFinanceDraft.nDraft = .FieldToClass("nDraft")
					lclsFinanceDraft.nContrat = nContrat
					lclsFinanceDraft.nCommission = .FieldToClass("nCommission")
					lclsFinanceDraft.nAmount_net = .FieldToClass("nAmount_net")
					lclsFinanceDraft.nStat_draft = .FieldToClass("nStat_draft")
					lclsFinanceDraft.nAmount = .FieldToClass("nAmount")
					lclsFinanceDraft.nStatPrint = .FieldToClass("nStatPrint")
					lclsFinanceDraft.nClaim = .FieldToClass("nClaim")
					lclsFinanceDraft.dLimitdate = .FieldToClass("dLimitdate")
					lclsFinanceDraft.sStat_draft = .FieldToClass("sStat_draft")
					Call Add(lclsFinanceDraft)
					'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFinanceDraft = Nothing
					.RNext()
				Loop 
				.RCloseRec()
				Find_policy_finan_dra = True
			End If
		End With
		
Find_policy_finan_dra_err: 
		If Err.Number Then
			Find_policy_finan_dra = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% Find_certificat_financ_dra: se buscan las cuotas cobradas, y las cuotas pendientes en vía de cobro
	Public Function Find_certificat_financ_dra(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		Dim lclsFinanceDraft As FinanceDraft
		
		On Error GoTo Find_certificat_financ_dra_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "reaCertificat_finan_dra"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsFinanceDraft = New FinanceDraft
					lclsFinanceDraft.nStatInstanc = FinanceDraft.eStatusInstance.eftQuery
					lclsFinanceDraft.dStat_date = .FieldToClass("dStat_date")
					lclsFinanceDraft.nIntermed = .FieldToClass("nIntermed")
					lclsFinanceDraft.nIntammou = .FieldToClass("nIntammou")
					lclsFinanceDraft.dExpirdat = .FieldToClass("dExpirdat")
					lclsFinanceDraft.nDraft = .FieldToClass("nDraft")
					lclsFinanceDraft.nContrat = .FieldToClass("nContrat")
					lclsFinanceDraft.nCommission = .FieldToClass("nCommission")
					lclsFinanceDraft.nAmount_net = .FieldToClass("nAmount_net")
					lclsFinanceDraft.nStat_draft = .FieldToClass("nStat_draft")
					lclsFinanceDraft.nAmount = .FieldToClass("nAmount")
					lclsFinanceDraft.nStatPrint = .FieldToClass("nStatPrint")
					lclsFinanceDraft.nClaim = .FieldToClass("nClaim")
					lclsFinanceDraft.dLimitdate = .FieldToClass("dLimitdate")
					lclsFinanceDraft.sStat_draft = .FieldToClass("sStat_draft")
					lclsFinanceDraft.nAmo_afec = .FieldToClass("nAmo_Afec")
					lclsFinanceDraft.nAmo_exen = .FieldToClass("nAmo_exen")
					lclsFinanceDraft.nIva = .FieldToClass("nIva")
					lclsFinanceDraft.nIntammou = .FieldToClass("nIntammou")
					Call Add(lclsFinanceDraft)
					'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFinanceDraft = Nothing
					.RNext()
				Loop 
				.RCloseRec()
				Find_certificat_financ_dra = True
			End If
		End With
		
Find_certificat_financ_dra_err: 
		If Err.Number Then
			Find_certificat_financ_dra = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	
	'% UpdateStatus: This function updates the status of the draft
	'% UpdateStatus: Esta función modifica el Estado del giro.
	Public Function UpdateStatus(ByVal nContrat As Double, ByVal nStatus As FinanceDraft.eStat_Draft, ByVal nUsercode As Integer) As Boolean
		Dim lclsFinanceDraft As FinanceDraft
		On Error GoTo UpdateStatusErr
		
		For	Each lclsFinanceDraft In mCol
			With lclsFinanceDraft
				.nStat_draft = nStatus
			End With
		Next lclsFinanceDraft
		
		Dim lrecupdFinanc_dra_Status As eRemoteDB.Execute
		
		lrecupdFinanc_dra_Status = New eRemoteDB.Execute
		
		'+ Stored procedure parameters definition 'insudb.updFinanc_dra_Status'
		'+ Data of 09/15/1999 11:38:28 AM
		'+ Definición de parámetros para stored procedure 'insudb.updFinanc_dra_Status'
		'+ Información leída el 15/09/1999 11:38:28 AM
		
		With lrecupdFinanc_dra_Status
			.StoredProcedure = "updFinanc_dra_Status"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStat_draft", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateStatus = .Run(False)
		End With
		
UpdateStatusErr: 
		If Err.Number Then
			UpdateStatus = False
		End If
		'UPGRADE_NOTE: Object lrecupdFinanc_dra_Status may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdFinanc_dra_Status = Nothing
	End Function
	
	'% UpdateStatPrint: This function updates the status of the draft.
	'% UpdateStatPrint: Esta función modifica el Estado del giro.
	Public Function UpdateStatPrint(ByVal nContrat As Double, ByVal nStatPrint As FinanceDraft.eStatPrint, ByVal nUsercode As Integer) As Boolean
		Dim lclsFinanceDraft As FinanceDraft
		Dim lrecupdFinanc_dra_StatPrint As eRemoteDB.Execute
		On Error GoTo UpdateStatPrintErr
		
		lclsFinanceDraft = New FinanceDraft
		lrecupdFinanc_dra_StatPrint = New eRemoteDB.Execute
		
		For	Each lclsFinanceDraft In mCol
			With lclsFinanceDraft
				.nStatPrint = nStatPrint
			End With
		Next lclsFinanceDraft
		
		'+ Stored procedure parameters definition 'insudb.updFinanc_dra_StatPrint'
		'+ Data of 09/16/1999 02:09:39 PM
		'+ Definición de parámetros para stored procedure 'insudb.updFinanc_dra_StatPrint'
		'+ Información leída el 16/09/1999 02:09:39 PM
		
		With lrecupdFinanc_dra_StatPrint
			.StoredProcedure = "updFinanc_dra_StatPrint"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatPrint", nStatPrint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateStatPrint = .Run(False)
		End With
		
UpdateStatPrintErr: 
		If Err.Number Then
			UpdateStatPrint = False
		End If
		'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceDraft = Nothing
		'UPGRADE_NOTE: Object lrecupdFinanc_dra_StatPrint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdFinanc_dra_StatPrint = Nothing
		
	End Function
	
	'% Item: Returns an element of the collection (acording to the index)
	'% Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As FinanceDraft
		Get
			'+ used when referencing an element in the collection
			'+ vntIndexKey contains either the Index or Key to the collection,
			'+ this is why it is declared as a Variant
			'+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Returns the number of elements that the collection has
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'+ used when retrieving the number of elements in the
			'+ collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Enumerates the collection for use in a For Each...Next loop
	'% NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'+ this property allows you to enumerate
			'+ this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Deletes an element from the collection
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'+ used when removing an element from the collection
		'+ vntIndexKey contains either the Index or Key, which is why
		'+ it is declared as a Variant
		'+ Syntax: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'+ creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controls the destruction of an instance of the collection
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'+ destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class







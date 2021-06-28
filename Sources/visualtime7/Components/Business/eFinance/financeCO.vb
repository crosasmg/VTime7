Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Public Class financeCO
	'%-------------------------------------------------------%'
	'% $Workfile:: financeCO.cls                            $%'
	'% $Author:: Nvapla10                                   $%'
	'% $Date:: 7/10/04 5:11p                                $%'
	'% $Revision:: 67                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla FINANCE_CO al 09-10-2002 16:23:58
	'+ Column name            Type                         Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'-  ----------------------------------------------------------------------------------------------------------------------
	Public nAmount As Double 'decimal  6      10    2     yes      (n/a)              (n/a)
	Public nFrequency As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public nAmount_d As Double 'decimal  6      10    2     yes      (n/a)              (n/a)
	Public sClient As String 'char     14                 yes      yes                yes
	Public dCompdate As Date 'datetime 8                  yes      (n/a)              (n/a)
	Public nContrat As Double 'int      4      10    0     no       (n/a)              (n/a)
	Public nCurrency As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public dEffecdate As Date 'datetime 8                  no       (n/a)              (n/a)
	Public dDate_print As Date 'datetime 8                  yes      (n/a)              (n/a)
	Public nDscto_amo As Double 'decimal  6      10    2     yes      (n/a)              (n/a)
	Public nStat_contr As Estat_contr 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public dFirst_draf As Date 'datetime 8                  yes      (n/a)              (n/a)
	Public nInitial As Double 'decimal  6      10    2     yes      (n/a)              (n/a)
	Public nInitial_or As Double 'decimal  6      10    2     yes      (n/a)              (n/a)
	Public nInterest As Double 'decimal  3      4     2     yes      (n/a)              (n/a)
	Public dLedger_dat As Date 'datetime 8                  yes      (n/a)              (n/a)
	Public nNotenum As Integer 'int      4      10    0     yes      (n/a)              (n/a)
	Public dNulldate As Date 'datetime 8                  yes      (n/a)              (n/a)
	Public nOffice As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public sOpt_commi As String 'char     1                  yes      yes                yes
	Public sPayment_in As EPayment_in 'char     1                  yes      yes                yes
	Public nQ_draft As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public sWait_contr As String 'char     1                  yes      yes                yes
	Public nUsercode As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public nBill_Day As Integer
	Public nWay_Day As Integer
	Public sType_Contr As String
	Public nPolicy As Double
	
	'+ Propiedades Auxiliares
	Public sIndicator As String
	Public nNullOption As Integer
	Public sClientName As String
	Public sDigit As String
	Public sCurrency As String
	Public nCommision As Double
	Public gintCompany As Integer
	Public nDraft_amo As Double
	Public dExpirDate As Date
	Public nExpenses As Double
	Public nCurr_cont As Integer
	Public nTotalAmo As Double
	Public blnDscto_amo As Boolean
	Public blnInterest As Boolean
	Private nAmountFD As Double
	Public nWay_Pay As Integer
	Public nPremiumN As Double
	Public nPremiumP As Double
	Public nPremiumT As Double
	Public nReceipt As Integer
	Public nQuota As Integer
	Public nQuotaPend As Integer
	Public nPayfreq As Integer
	
	Public nValquota As Double
	Public nInterest_ori As Double
	'-Se incluye campo con porcentaje de pronto pago
	Public nDscto_pag As Double
	
	
	'- Variable de inspreca017a
	Public bQuota_Dis As Boolean
	Public bInitial_Dis As Boolean
	Public bInterest_Dis As Boolean
	Public bFirst_Draf_Dis As Boolean
	
	'- Variable que guarda un porcentaje de un monto
	Public pdblPercent As Double
	
	'-Se define la variable que va a contener los estados del contrato
	
	Public Enum Estat_contr
		Einitialwait = 1
		Evigour = 2
		Epayment = 3
		Eannul = 4
		Eincompletecapture = 5
	End Enum
	
	'-Se define el tipo enumerado que va a contener el tipo de movimiento sobre los recibos
	Public Enum Etypemovreceipt
		Ereccreate = 1
		Epremiumcollect = 2
		Epremiumreturn = 3
		Ecollectreverse = 4
		Ereturnreverse = 5
		Emanagcollectchange = 6
		Ereceiptnull = 7
		Eerrornull = 8
		Enullreverse = 9
		Ereceiptreinstatment = 10
		Edomicilechange = 11
		Efinancing = 12
		Einvoicecreate = 13
		Ecountcharge = 14
		Eloandetention = 15
	End Enum
	
	'-Se define el tipo enumerado para identificar el pago de intereses con la cuota inicial
	Public Enum EPayment_in
		eafirmative = 1
		enegative = 2
	End Enum
	
	'-Se define la variable que va a contener la frecuencia.
	Public Enum eFrequency
		efNot_Stand = 1
		efMonthly = 2
		efQuarterly = 3
	End Enum
	
	'   Se define la variable que contiene la transacción que se ejecuta en cierto momento.
	Public Enum eFinanceTransac
		eftAddContrat = 1
		eftQuerycontrat = 2
		eftUpDateContrat = 3
		eftRecoveryContrat = 4
	End Enum
	
	'-Se define el tipo enumerado para las opciones de anulación del contrato de financiamiento
	
	Private Enum Eoption
		NoNullRev = 1
		RecRevers = 2
		NullReceipt = 3
		NullPolicy = 4
	End Enum
	
	'- Se define la variable nAmountDraft que contendra el importe de la tabla FinanceDraft
	Public nAmountDraft As Double
	
	'- Se define la variable nIntAmount que contendra el importe de la tabla FinanceDraft
	Public nIntAmount As Double
	
	'- Se define la variable nExchange que contendra el factor de cambio
	Public nExchange As Double
	
	'- Se definen las propiedades auxiliares a ser utilizadas en la consulta
	'- FIC006 - Consulta de búsqueda de contratos.
	
	Private mstrCondition As String
	
	'- Se define la variable mlngErrorNum para indicar el error devuelto al procesar la transacción CA017A.
	Public mlngErrorNum As Integer
	
	'- Variable para indicar el número de contrato al cual se refinanciaron los giros. Utilizada
	'- en la transacción CA017A (Cuotas de un recibo)
	Public nContrat_ref As Integer
	
	'**%Sql: This method holds the query introduced in the InsValFIC006 method
	'% Sql: Este método permite almacenar el valor de la condición introducido en el método
	'% InsValFIC006.
	Public ReadOnly Property Sql() As Object
		Get
			Sql = mstrCondition
		End Get
	End Property
	
	'**%insPreFI005: This function gets the information needed to management window FI005
	'%insPreFI005:Permite obtener la información de necesaria para el manejo de la ventana
	Public Function insPreFI005(ByVal nContrat As Double) As Boolean
		Dim lclsFinanceCO As eFinance.financeCO
		Dim lclsFinanceDraft As eFinance.FinanceDraft
		Dim lclsExchange As eGeneral.Exchange
		
		lclsFinanceCO = New eFinance.financeCO
		lclsFinanceDraft = New eFinance.FinanceDraft
		lclsExchange = New eGeneral.Exchange
		
		If lclsFinanceDraft.Find(nContrat, 0) Then
			nIntAmount = lclsFinanceDraft.nIntammou
		End If
		With lclsFinanceCO
			If .Find_Contrat(nContrat) Then
				nAmountDraft = nIntAmount + .nInitial_or
				Me.nCurrency = .nCurrency
				Me.nOffice = .nOffice
				Me.sClient = .sClient
				Me.nInterest = .nInterest
				Me.nInitial = .nInitial_or
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceDraft = Nothing
	End Function
	
	'**%DefaultValue: This function fills the fields of the window with the data of the table FinanceCO
	'%DefaultValue:Realiza el llenado de cada uno de los campos de la transacción
	'%en caso de existir previamente el registro en la tabla FinanceCO.
	Public Function DefaultValueFI005(ByVal sField As Object) As Object
        Dim lstrReturnValue As Object = New Object

        '**+Loading the values to the fields, the system validates that  the field in the table is not null
        '+Se cargan todos los valores de la ventana, validando que el campo no se encuentre nulo

        Select Case sField
			Case "cbeCurrency"
				lstrReturnValue = nCurrency
			Case "cbeOffice"
				lstrReturnValue = nOffice
			Case "dtcClient"
				lstrReturnValue = sClient
			Case "tcnInterest"
				lstrReturnValue = nInterest
			Case "nInitial"
				lstrReturnValue = nInitial
		End Select
		
		DefaultValueFI005 = lstrReturnValue
	End Function
	
	'**%insValFI005_K: This method validates the header section of the page "FI005_K" as described in the
	'**%functional specifications
	'%InsValFI005_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "FI005_K"
	Public Function insValFI005_K(ByVal nContrat As Double, ByVal dEffecdate As Date) As String
		
		Dim errorNumber As Integer
		Dim lclsFinanco As financeCO
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValFI005_K_err
		
		lclsFinanco = New financeCO
		lclsErrors = New eFunctions.Errors
		
		'**Contrat number validations
		'+Validación del Contrato de Financiamiento
		
		If nContrat <> 0 And nContrat <> eRemoteDB.Constants.intNull Then
			If Not lclsFinanco.Find_Contrat(nContrat) Then
				Call lclsErrors.ErrorMessage("FI005", 21002)
			Else
				'+ Validar que el contrato no este anulado ni con captura incompleta
				'+Variable que contendrá el número del error para evaluar el estado del contrato
				Select Case lclsFinanco.nStat_contr
					Case Estat_contr.Eannul
						errorNumber = 21005
						
					Case Estat_contr.Eincompletecapture
						errorNumber = 21134
						
					Case Estat_contr.Epayment
						errorNumber = 21095
						
					Case Estat_contr.Evigour
						errorNumber = 21058
				End Select
				
				'+Se valida que el contrato esté en Vigor
				If errorNumber <> 0 Then
					Call lclsErrors.ErrorMessage("FI005", errorNumber)
				End If
			End If
		Else
			Call lclsErrors.ErrorMessage("FI005", 21062)
		End If
		
		'**+Collect date validations
		'+Validación de la fecha de cobro
		
		If lclsErrors.Confirm = String.Empty Then
			If dEffecdate <> eRemoteDB.Constants.dtmNull Then
				If dEffecdate < lclsFinanco.dEffecdate Then
					Call lclsErrors.ErrorMessage("FI005", 21007)
					'**+ The date must be minor or equal than today
					'+ La fecha debe ser menor o igual a la fecha del día en curso
				ElseIf dEffecdate > Today Then 
					Call lclsErrors.ErrorMessage("FI005", 1965)
				End If
			Else
				Call lclsErrors.ErrorMessage("FI005", 21059)
			End If
		End If
		
		insValFI005_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsFinanco may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanco = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValFI005_K_err: 
		If Err.Number Then
			insValFI005_K = insValFI005_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostFI005_K. This method updates the database (as described in the functional specifications)
	'**%for the page "FI005_K"
	'%insPostFI005_K: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "FI005_K"
	Public Function insPostFI005_K() As Boolean
		insPostFI005_K = True
		
	End Function
	
	'**%insPostFI005. This method updates the database (as described in the functional specifications)
	'**%for the page "FI005"
	'%insPostFI005: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "FI005"
	Public Function insPostFI005(ByVal nContrat As Double, ByVal nAmount As Double, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostFI005_err
		
		'+ Se realiza el pago de la cuota inicial
		insPostFI005 = PayInitialCollect(nContrat, nAmount, nCurrency, dEffecdate, nUsercode)
		
insPostFI005_err: 
		If Err.Number Then
			insPostFI005 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValFI005: This method validates the page "FI005" as described in the functional specifications
	'%InsValFI005: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "FI005"
	Public Function insValFI005(ByVal nPayment_way As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValFI005_err
		
		lclsErrors = New eFunctions.Errors
		
		'**+The system validates that the field is filled
		'+ Validando que el campo este lleno
		
		If nPayment_way = 0 Or nPayment_way = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("FI005", 21060)
		End If
		
		insValFI005 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValFI005_err: 
		If Err.Number Then
			insValFI005 = insValFI005 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insValFI006_K: This method validates the header section of the page "FI006_K" as described in the
	'**%functional specifications
	'%InsValFI006_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "FI006_K"
	Public Function insValFI006_K(ByVal nContrat As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsFinanceCO As financeCO
		Dim lclsFinanceDraft As eFinance.FinanceDraft
		
		On Error GoTo insValFI006_K_err
		
		lclsErrors = New eFunctions.Errors
		lclsFinanceCO = New financeCO
		lclsFinanceDraft = New FinanceDraft
		
		'**+"Contract" validations
		'+ Validacion del campo "Contrato"
		
		If nContrat = 0 Or nContrat = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("FI006", 21062)
		Else
			With lclsFinanceCO
				If .Find(nContrat, dEffecdate) Then
					
					'**The contract can't be payed, cancelled o incomplete
					'+ El contrato no puede estar pagado, anulado o en captura incompleta
					If .nStat_contr = Estat_contr.Epayment Or .nStat_contr = Estat_contr.Eannul Or .nStat_contr = Estat_contr.Eincompletecapture Then
						Call lclsErrors.ErrorMessage("FI006", 21074)
					End If
				Else
					
					'**+The contract must have a value
					'+ Debe estar lleno
					Call lclsErrors.ErrorMessage("FI006", 21002)
				End If
			End With
			If lclsFinanceDraft.CountDraft(nContrat, FinanceDraft.eStat_Draft.esdCollect) Or lclsFinanceDraft.CountDraft(nContrat, FinanceDraft.eStat_Draft.esdCollectClaimDiscount) Then
				Call lclsErrors.ErrorMessage("FI006", 21137)
			End If
		End If
		
		'**+"Cancellation date"
		'+Validacion del campo "Fecha de anulación"
		
		
		If dNulldate = eRemoteDB.Constants.dtmNull Then
			'**+  Must have a value
			'+ Debe esatar lleno
			Call lclsErrors.ErrorMessage("FI006", 21113)
		Else
			If dNulldate < lclsFinanceCO.dEffecdate Then
				Call lclsErrors.ErrorMessage("FI006", 21045)
			ElseIf dNulldate > Today Then 
				'**The date must be equal or less than today
				'+ La fecha debe ser menor o igual a la fecha del día en curso
				Call lclsErrors.ErrorMessage("FI006", 1965)
			End If
		End If
		
		insValFI006_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceDraft = Nothing
		
insValFI006_K_err: 
		If Err.Number Then
			insValFI006_K = insValFI006_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insValFI006: This method validates the page "FI006" as described in the functional specifications
	'%InsValFI006: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "FI006"
	Public Function insValFI006(ByVal nCause As Integer, ByVal nOption As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		'**+ CbeCause field validations
		'+Validacion del CAMPO cbeCause
		
		If nCause = 0 Or nCause = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("FI006", 21132)
		End If
		
		'**+ CbeOptione field validations
		'+Validacion del CAMPO cbeOption
		
		If nOption = 0 Or nOption = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("FI006", 21133)
		End If
		
		insValFI006 = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValFI006_err: 
		If Err.Number Then
			insValFI006 = insValFI006 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostFI006. This method updates the database (as described in the functional specifications)
	'**%for the page "FI006"
	'%insPostFI006: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "FI006"
	Public Function insPostFI006(ByVal nOption As Integer, ByVal nCause As Integer, ByVal dNulldate As Date, ByVal nCurr_cont As Integer, ByVal nDscto_amo As Double, ByVal nContrat As Double, ByVal nUsercode As Integer) As Boolean
		Dim lclsFinanceCO As financeCO
		
		On Error GoTo insPostFI006_err
		
		lclsFinanceCO = New financeCO
		
		'**- Variable definition. This variable will manage the finance contract cancel options
		'- Se define la variable para las opciones de anulación del contrato
		Me.nContrat = nContrat
		Me.nUsercode = nUsercode
		With lclsFinanceCO
			Select Case nOption
				Case Eoption.NoNullRev
					insPostFI006 = .Cancel(dNulldate, nOption, nCause, nCurr_cont, CDbl(nDscto_amo), eRemoteDB.Constants.intNull, nContrat, nUsercode)
				Case Eoption.RecRevers
					insPostFI006 = .Cancel(dNulldate, nOption, nCause, nCurr_cont, CDbl(nDscto_amo), Etypemovreceipt.Ecollectreverse, nContrat, nUsercode)
				Case Eoption.NullReceipt
					insPostFI006 = .Cancel(dNulldate, nOption, nCause, nCurr_cont, CDbl(nDscto_amo), Etypemovreceipt.Ereceiptnull, nContrat, nUsercode)
				Case Eoption.NullPolicy
					insPostFI006 = .Cancel(dNulldate, nOption, nCause, nCurr_cont, CDbl(nDscto_amo), Etypemovreceipt.Ereceiptnull, nContrat, nUsercode)
			End Select
		End With
		
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		
insPostFI006_err: 
		If Err.Number Then
			insPostFI006 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Upd: Esta función se encarga Cargar los datos de un contrato y actualizar los campos de importe de una prima y un giro
	Public Function Upd(ByVal nContrat As Double, ByVal nContrat_d As Double, ByVal dEffecdate As Date, ByVal dExpirdat As Date, ByVal nPremiu As Double, ByVal nExchange As Double, ByVal nCommission As Double, Optional ByRef nAction As Integer = 0, Optional ByRef bCount As Boolean = False) As Boolean
		'**- If the expiration date is being entered = dtmnull
		'- si estoy insertando el dexpirdate = dtmnull
		Dim nAmou As Double
		Dim nAmou_d As Double
		Dim nComm As Double
		Dim nCurr As Integer
		Dim nStat_contr As Integer
		
		Dim lclsExchange As eGeneral.Exchange
		
		
		If nExchange = 0 Then
			nExchange = 1
		End If
		
		If Find(nContrat_d, dEffecdate) Then
			nCurr = Me.nCurrency
			If dExpirdat <> eRemoteDB.Constants.dtmNull Then
				nAmou = Me.nAmount
				If nAmou > 0 Then
					nAmou = Me.nAmount - nPremiu / nExchange
					nAmou_d = Me.nAmount_d - nPremiu / nExchange
				End If
				If Me.nCommision > 0 Then
					nComm = Me.nCommision - nPremiu / nExchange
				End If
			Else
				
			End If
			
			nStat_contr = 0
			If nAction = RefinanceDraft.eSel.eftIgnore Then
				If bCount Then
					nStat_contr = Estat_contr.Epayment
				End If
			End If
			
			If nAction = RefinanceDraft.eSel.eftDel Then
				If Me.nStat_contr = Estat_contr.Epayment Then
					nStat_contr = Estat_contr.Evigour
				End If
			End If
			
			Call Find(nContrat, dEffecdate)
			
			If Me.nCurrency <> nCurr Then
				lclsExchange = New eGeneral.Exchange
				Call lclsExchange.Convert(eRemoteDB.Constants.intNull, nAmount, nCurr, Me.nCurrency, dEffecdate, 0)
				nAmou_d = Me.nAmount_d + lclsExchange.pdblResult
				nAmou = Me.nAmount + lclsExchange.pdblResult
				Call lclsExchange.Convert(eRemoteDB.Constants.intNull, nCommission, nCurr, Me.nCurrency, dEffecdate, 0)
				nComm = nComm + lclsExchange.pdblResult
				'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsExchange = Nothing
			Else
				nAmou_d = Me.nAmount_d + nPremiu
				nAmou = Me.nAmount + nPremiu
				nComm = Me.nCommision + nPremiu
			End If
		End If
		
		Me.nAmount = nAmou
		Me.nAmount_d = nAmou_d
		
		If nStat_contr <> 0 Then
			'Me.nStat_contr = nStat_contr
		End If
		
		Upd = Me.UpDate
		
		
	End Function
	
	'%insPostFI001_K: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "FI001_K"
	Public Function insPostFI001_K(ByVal nTransaction As Integer, ByVal dEffecdate As Date, ByVal nContrat As Double, ByVal nOffice As Integer, ByVal nCurrency As Integer, ByVal sClient As String, ByVal nInterest As Double, ByVal dLedger_dat As Date, ByVal sOpt_commi As String, ByVal nInitial As Double, ByVal nPayment_in As Integer, ByVal nQ_draft As Integer, ByVal nFrequency As Integer, ByVal dFirst_draf As Date, ByVal nDscto_amo As Double, ByVal nUsercode As Integer, ByVal nBill_Day As Integer, ByVal nWay_Pay As Integer, ByVal nDscto_pag As Double, ByVal sOptType As String, ByVal nPolicy As Double) As Boolean
		Dim lclsFinanceCO As financeCO
		Dim nContra As Double
		
		lclsFinanceCO = New financeCO
		
		If nTransaction <> eFinanceTransac.eftQuerycontrat Then
			With lclsFinanceCO
				
				'+Si no se ingresó el numero de contrato se genera uno automático
				If nContrat = eRemoteDB.Constants.intNull Then
					Call .Find_GenerateContrat()
					nContra = .nContrat
				Else
					nContra = nContrat
					Call Find(nContrat, dEffecdate)
				End If
				
				Me.nContrat = nContra
				
				.nUsercode = nUsercode
				
				Select Case nTransaction
					
					'+Si la opción seleccionada es Registrar un Contrato.
					Case eFinanceTransac.eftAddContrat
						.sClientName = sClientName
						.nStat_contr = Estat_contr.Eincompletecapture
						.nInitial_or = nInitial
						.nInitial = nInitial
						.dEffecdate = dEffecdate
						.nCurrency = nCurrency
						.nContrat = nContra
						.nOffice = nOffice
						.nInterest = IIf(nInterest = eRemoteDB.Constants.intNull, 0, nInterest)
						.dLedger_dat = dLedger_dat
						.sOpt_commi = sOpt_commi
						.nQ_draft = nQ_draft
						.dFirst_draf = dFirst_draf
						.nFrequency = nFrequency
						.nAmount = 0
						.nAmount_d = 0
						.nCommision = 0
						.nBill_Day = nBill_Day
						.nWay_Pay = nWay_Pay
						.nDscto_amo = nDscto_amo
						If .nDscto_amo = eRemoteDB.Constants.intNull Then .nDscto_amo = 0
						.nDscto_pag = nDscto_pag
						If .nDscto_pag = eRemoteDB.Constants.intNull Then .nDscto_pag = 0
						.sPayment_in = IIf(nPayment_in = 1, EPayment_in.eafirmative, EPayment_in.enegative)
						.sClient = sClient
						.sType_Contr = sOptType
						.nPolicy = nPolicy
						insPostFI001_K = .Add
						
						'+Si la opción seleccionada es Consultar un Contrato.
					Case eFinanceTransac.eftQuerycontrat
						.nCommision = 0
						
						'+Si la opción seleccionada es Modificar un Contrato.
					Case eFinanceTransac.eftUpDateContrat, eFinanceTransac.eftRecoveryContrat
						.sClientName = sClientName
						.dEffecdate = dEffecdate
						.nContrat = nContra
						.nOffice = nOffice
						.nCurrency = nCurrency
						.nInterest = IIf(nInterest = eRemoteDB.Constants.intNull, 0, nInterest)
						.dLedger_dat = dLedger_dat
						.sOpt_commi = sOpt_commi
						'+ Valor de la cuota inicial original o bruto
						.nInitial_or = nInitial
						'+ Valor de la cuota inicial neto
						.nInitial = nInitial
						.nQ_draft = nQ_draft
						.dFirst_draf = dFirst_draf
						.nFrequency = nFrequency
						.nDscto_amo = nDscto_amo
						If .nDscto_amo = eRemoteDB.Constants.intNull Then .nDscto_amo = 0
						.nDscto_pag = nDscto_pag
						If .nDscto_pag = eRemoteDB.Constants.intNull Then .nDscto_pag = 0
						.sClient = sClient
						.sCurrency = CStr(nCurrency)
						.dNulldate = eRemoteDB.Constants.dtmNull
						.sPayment_in = IIf(nPayment_in = 1, EPayment_in.eafirmative, EPayment_in.enegative)
						.nAmount = Me.nAmount
						.nAmount_d = Me.nAmount
						.nStat_contr = Estat_contr.Eincompletecapture
						.nStat_contr = IIf(nTransaction = eFinanceTransac.eftUpDateContrat, Estat_contr.Eincompletecapture, Me.nStat_contr)
						.nBill_Day = nBill_Day
						insPostFI001_K = .UpDate
						
					Case Else
						insPostFI001_K = False
				End Select
				
			End With
		Else
			insPostFI001_K = True
		End If
	End Function
	
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Finance_co"
	Public Function Find(ByVal nContrat As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaFinance_co As eRemoteDB.Execute
		Dim lstrValue As String
		
		On Error GoTo Find_Err
		
		If Me.nContrat <> nContrat Or Me.dEffecdate <> dEffecdate Or lblnFind Then
			
			lrecreaFinance_co = New eRemoteDB.Execute
			
			'+ Definición de parámetros para stored procedure 'insudb.reaFinance_co'
			'+ Información leída el 12/08/1999 09:22:32 AM
			
			With lrecreaFinance_co
				.StoredProcedure = "reaFinance_co"
				.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Me.nAmount = .FieldToClass("nAmount")
					nFrequency = .FieldToClass("nFrecuency")
					Me.nAmount_d = .FieldToClass("nAmount_d")
					sClient = .FieldToClass("sClient")
					sDigit = .FieldToClass("sDigit")
					dCompdate = .FieldToClass("dCompdate")
					Me.nContrat = .FieldToClass("nContrat")
					nCurrency = .FieldToClass("nCurrency")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					dDate_print = .FieldToClass("dDate_print")
					nDscto_amo = .FieldToClass("nDscto_amo")
					nDscto_pag = nDscto_amo
					nStat_contr = .FieldToClass("nStat_contr")
					dFirst_draf = .FieldToClass("dFirst_draf")
					nInitial = .FieldToClass("nInitial")
					nInitial_or = .FieldToClass("nInitial_or")
					nInterest = .FieldToClass("nInterest")
					dLedger_dat = .FieldToClass("dLedger_dat")
					nNotenum = .FieldToClass("nNotenum")
					dNulldate = .FieldToClass("dNulldate")
					nOffice = .FieldToClass("nOffice")
					sOpt_commi = .FieldToClass("sOpt_commi")
					lstrValue = .FieldToClass("sPayment_in")
					If lstrValue = String.Empty Then
						sPayment_in = 0
					End If
					sPayment_in = CShort(lstrValue)
					nQ_draft = .FieldToClass("nQ_draft")
					sWait_contr = .FieldToClass("sWait_contr")
					nUsercode = .FieldToClass("nUsercode")
					sClientName = .FieldToClass("sCliename")
					sCurrency = .FieldToClass("sDescript")
					nCommision = .FieldToClass("nCommission")
					nBill_Day = IIf(.FieldToClass("nBill_Day") = eRemoteDB.Constants.intNull, nBill_Day, .FieldToClass("nBill_Day"))
					nWay_Pay = .FieldToClass("nWay_Pay")
					sType_Contr = .FieldToClass("sType_Contr")
					nPolicy = .FieldToClass("nPolicy")
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
		'UPGRADE_NOTE: Object lrecreaFinance_co may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFinance_co = Nothing
	End Function
	
	'**% Find_Stat_contr: Searches for the status of a contract
	'% Find_Stat_contr: busca el estado de un contrato
	Public Function Find_Stat_contr(ByVal Contrat As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaFinance_co_Contrat As eRemoteDB.Execute
		
		On Error GoTo Find_Stat_contr_Err
		lrecreaFinance_co_Contrat = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.reaFinance_co_Contrat'
		'**+Data of 11/08/1999 13:26:20
		'+ Definición de parámetros para stored procedure 'insudb.reaFinance_co_Contrat'
		'+ Información leída el 08/11/1999 13:26:20
		
		With lrecreaFinance_co_Contrat
			.StoredProcedure = "reaFinance_co_Contrat"
			.Parameters.Add("nContrat", Contrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nStat_contr = .FieldToClass("nStat_contr")
				Find_Stat_contr = True
				.RCloseRec()
			Else
				Find_Stat_contr = False
			End If
		End With
		
Find_Stat_contr_Err: 
		If Err.Number Then
			Find_Stat_contr = False
		End If
		'UPGRADE_NOTE: Object lrecreaFinance_co_Contrat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFinance_co_Contrat = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_Contrat: busca los registros correspondientes a un contrato específico
	Public Function Find_Contrat(ByVal Contrat As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaFinance_co_Contrat As eRemoteDB.Execute
        Dim lstrValue As String = ""

        On Error GoTo Find_Contrat_Err
		If Contrat <> nContrat Or lblnFind Then
			
			lrecreaFinance_co_Contrat = New eRemoteDB.Execute
			
			'+ Definición de parámetros para stored procedure 'insudb.reaFinance_co_Contrat'
			'+ Información leída el 09/09/1999 03:47:02 PM
			
			With lrecreaFinance_co_Contrat
				.StoredProcedure = "reaFinance_co_Contrat"
				.Parameters.Add("nContrat", Contrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(True) Then
					nAmount = .FieldToClass("nAmount")
					nFrequency = .FieldToClass("nFrecuency")
					nAmount_d = .FieldToClass("nAmount_d")
					sClient = .FieldToClass("sClient")
					dCompdate = .FieldToClass("dCompdate")
					nContrat = .FieldToClass("nContrat")
					nCurrency = .FieldToClass("nCurrency")
					dEffecdate = .FieldToClass("dEffecdate")
					dDate_print = .FieldToClass("dDate_print")
					nDscto_amo = .FieldToClass("nDscto_amo")
					nDscto_pag = nDscto_amo
					nStat_contr = .FieldToClass("nStat_contr")
					dFirst_draf = .FieldToClass("dFirst_draf")
					nInitial = .FieldToClass("nInitial")
					nInitial_or = .FieldToClass("nInitial_or")
					nInterest = .FieldToClass("nInterest")
					dLedger_dat = .FieldToClass("dLedger_dat")
					nNotenum = .FieldToClass("nNotenum")
					dNulldate = .FieldToClass("dNulldate")
					nOffice = .FieldToClass("nOffice")
					sOpt_commi = .FieldToClass("sOpt_commi")
					lstrValue = IIf(lstrValue = String.Empty, 0, .FieldToClass("sPayment_in"))
					sPayment_in = CShort(lstrValue)
					nQ_draft = .FieldToClass("nQ_draft")
					sWait_contr = .FieldToClass("sWait_contr")
					sClientName = .FieldToClass("sCliename")
					sCurrency = .FieldToClass("sDescript")
					nBill_Day = .FieldToClass("nBill_Day")
					nWay_Pay = .FieldToClass("nWay_Pay")
					.RCloseRec()
					Find_Contrat = True
				Else
					Find_Contrat = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaFinance_co_Contrat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaFinance_co_Contrat = Nothing
		Else
			Find_Contrat = True
		End If
		
Find_Contrat_Err: 
		If Err.Number Then
			Find_Contrat = False
		End If
		On Error GoTo 0
	End Function
	
	
	'% Add: añade un nuevo registro en la tabla
	Public Function Add() As Boolean
		Dim lrecFinance_co As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		lrecFinance_co = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creFinance_co'
		'+ Información leída el 11/08/1999 10:20:00 AM
		
		With lrecFinance_co
			.StoredProcedure = "creFinance_co"
			
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrequency", nFrequency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_d", nAmount_d, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_print", dDate_print, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'+Como en tabla no existe campo para porcentaje de descuento
			'+se usa campo de monto de descuento porque este no se está usando
			.Parameters.Add("nDscto_amo", nDscto_pag, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFirst_draf", dFirst_draf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStat_contr", nStat_contr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitial", nInitial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitial_or", nInitial_or, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedger_dat", dLedger_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOpt_commi", sOpt_commi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPayment_in", sPayment_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_draft", nQ_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWait_contr", sWait_contr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_Day", nBill_Day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_Contr", sType_Contr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecFinance_co may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFinance_co = Nothing
	End Function
	
	'%Update: Este método se encarga de actualizar registros en la tabla "Finance_co". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function UpDate() As Boolean
		Dim lupdFinance_co As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		lupdFinance_co = New eRemoteDB.Execute
		
		sIndicator = "1"
		
		'+ Definición de parámetros para el stored procedure: "updFinance_co"
		
		With lupdFinance_co
			.StoredProcedure = "updFinance_co"
			
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrequency", nFrequency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_d", nAmount_d, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_print", dDate_print, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'+Como en tabla no existe campo para porcentaje de descuento
			'+se usa campo de monto de descuento porque este no se está usando
			.Parameters.Add("nDscto_amo", nDscto_pag, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFirst_draf", dFirst_draf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStat_contr", nStat_contr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitial", nInitial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitial_or", nInitial_or, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedger_dat", dLedger_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dNulldate", IIf(dNulldate = System.Date.FromOADate(0), System.DBNull.Value, dNulldate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOpt_commi", sOpt_commi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPayment_in", sPayment_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_draft", nQ_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWait_contr", sWait_contr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndicator", sIndicator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_Day", nBill_Day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpDate = .Run(False)
			
		End With
		
Update_Err: 
		If Err.Number Then
			UpDate = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lupdFinance_co may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lupdFinance_co = Nothing
	End Function
	
	'**% Cancel: This routine cancels a finance contract
	'% Cancel: Esta función se encarga de anular un contrato.
	Public Function Cancel(ByVal Effecdate As Date, ByVal nNullOption As Integer, ByVal nNullCause As Integer, Optional ByRef nCurr_cont As Integer = 0, Optional ByRef nDscto_amo As Double = 0, Optional ByRef nType As Etypemovreceipt = 0, Optional ByRef nContrat As Double = 0, Optional ByRef nUsercode As Integer = 0) As Boolean
		
		Dim lrecreaAnul_Contrat As eRemoteDB.Execute
		
		On Error GoTo Cancel_Err
		lrecreaAnul_Contrat = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaAnul_Contrat'
		'+ Información leída el 13/09/1999 04:11:06 PM
		
		With lrecreaAnul_Contrat
			.StoredProcedure = "reaAnul_Contrat"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", Effecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNulloption", nNullOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullCause", nNullCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurr_cont", nCurr_cont, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDscto_amo", nDscto_amo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Cancel = .Run(False)
		End With
Cancel_Err: 
		If Err.Number Then
			Cancel = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaAnul_Contrat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAnul_Contrat = Nothing
	End Function
	'**% PayInitialCollect: This function makes the collect of the down payment
	'% PayInitialCollect: Función que realiza el pago de la cuota inicial
	Public Function PayInitialCollect(ByVal Contrat As Double, ByVal Amount As Double, ByVal nCurrency As Integer, ByVal StateDate As Date, ByVal UserCode As Integer) As Boolean
        Dim lrecPayInitialCollect As eRemoteDB.Execute = New eRemoteDB.Execute


        On Error GoTo PayInitialCollect_Err
		If lrecPayInitialCollect Is Nothing Then
			lrecPayInitialCollect = New eRemoteDB.Execute
		End If
		
		With lrecPayInitialCollect
			
			'**+Stored procedure parameters definition 'insudb.reaAnul_Contrat'
			'**+Data of 09/14/1999 04:43:32 PM
			'+ Definición de parámetros para stored procedure 'insudb.insPayInitialCollect'
			'+ Información leída el 14/09/1999 04:43:32 PM
			
			.StoredProcedure = "insPayInitialCollect"
			.Parameters.Add("nContrat", Contrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStateDate", StateDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", UserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			PayInitialCollect = .Run(False)
		End With
PayInitialCollect_Err: 
		If Err.Number Then
			PayInitialCollect = False
		End If
		On Error GoTo 0
	End Function
	'**% DeleteAll: This routine deletes the finance contract associated in each finance table
	'% DeleteAll: Elimina el número asociado al contrato en cada tabla de Financiamiento
	Public Function DeleteAll(ByVal Contrat As Double, ByVal nUsercode As Integer) As Boolean
		Dim lrecdelFinance_All As eRemoteDB.Execute
		
		On Error GoTo DeleteAll_Err
		lrecdelFinance_All = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.delFinance_All'
		'**+Data of 10/06/1999 03:16:36 PM
		'+ Definición de parámetros para stored procedure 'insudb.delFinance_All'
		'+ Información leída el 06/10/1999 03:16:36 PM
		
		With lrecdelFinance_All
			.StoredProcedure = "delFinance_All"
			.Parameters.Add("nContrat", Contrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			DeleteAll = .Run(False)
		End With
DeleteAll_Err: 
		If Err.Number Then
			DeleteAll = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelFinance_All may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelFinance_All = Nothing
	End Function
	'**% AmountDraft: This routine calculates the draft amount
	'% AmountDraft: Esta función se encarga de calcular el importe del giro
	Public Function AmountDraft() As Double
		'**- Variable definition. This variable will contain the rate
		'- Se define la variable que contiene el interés
		Dim ldblInterest As Double
		On Error GoTo AmountDraftErr
		
		ldblInterest = Interest
		
		AmountDraft = ((nAmount - IIf(nInitial <= 0, 0, nInitial)) * ldblInterest) / (1 - (1 / (1 + ldblInterest) ^ nQ_draft))
		
		'+Si no hay interes, podría generar una division por cero
AmountDraftErr: 
		If Err.Number Then
			If nQ_draft = 0 Then
				AmountDraft = nAmount
			Else
				AmountDraft = nAmount / nQ_draft
			End If
		End If
	End Function
	'**% Interest: This function calculates the rate
	'% Interest: Esta función se encarga de calcular el interés
	Public Function Interest(Optional ByRef FirstDraft As Date = #12:00:00 AM#, Optional ByRef SecondDraft As Date = #12:00:00 AM#) As Double
		'**- Variable definition. This variable will contain the quantity of months related to the payment frequency
		'- Se define la variable que contiene el número de meses de la relacionados a la frecuencia
		Dim lintFrecuency As Integer
		
		Select Case nFrequency
			Case eFrequency.efMonthly
				lintFrecuency = 1
			Case eFrequency.efQuarterly
				lintFrecuency = 3
			Case Else
				lintFrecuency = 12
		End Select
		
		Interest = (nInterest / 100) * (lintFrecuency / 12)
	End Function
	'**% InterestQuotaInitial: This routine calculates the draft amount
	'% InterestQuotaInitial: Esta función se encarga de calcular el importe del giro
	Public Function InterestQuotaInitial(Optional ByRef FirstDraft As Date = #12:00:00 AM#, Optional ByRef SecondDraft As Date = #12:00:00 AM#) As Double
		
		InterestQuotaInitial = ((AmountDraft - ((nAmount - IIf(nInitial = eRemoteDB.Constants.intNull, 0, nInitial)) / nQ_draft)) * nQ_draft)
		
	End Function
	'**% valReceiptClaim: This routine verifies if there are premium invoices in a finance contract and
	'**%verifies that correspond with a given policy
	'%                  que correspondan a la póliza dada
	'% valReceiptClaim: Verifica si existen recibos financiados para un determinado contrato
	'%                  que correspondan a la póliza dada
	Public Function valReceiptClaim(ByVal lstrsCertype As String, ByVal llngnpolicy As Double, ByVal lintnBranch As Integer, ByVal lintnProduct As Integer) As Boolean
		Dim lrecvalReceipt_claim As eRemoteDB.Execute
		
		On Error GoTo valReceiptClaim_Err
		lrecvalReceipt_claim = New eRemoteDB.Execute
		
		valReceiptClaim = False
		'**+Stored procedure parameters definition 'insudb.delFinance_All'
		'**+Data of 09/20/1999 01:25:25 PM
		'+ Definición de parámetros para stored procedure 'insudb.valReceipt_claim'
		'+ Información leída el 09/20/1999 01:25:25 PM
		
		With lrecvalReceipt_claim
			.StoredProcedure = "valReceipt_claim"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", lstrsCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", llngnpolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", lintnBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", lintnProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nCount") = 0 Then
					valReceiptClaim = True
				End If
				.RCloseRec()
			End If
		End With
		
valReceiptClaim_Err: 
		If Err.Number Then
			valReceiptClaim = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecvalReceipt_claim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalReceipt_claim = Nothing
	End Function
	
	'% Find_GenerateContrat: genera automáticamente el número de contrato
	Public Function Find_GenerateContrat() As Integer
		Dim lrecinscreNumerator_Finance As eRemoteDB.Execute
		
		On Error GoTo Find_GenerateContrat_Err
		lrecinscreNumerator_Finance = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.inscreNumerator_Finance'
		'+ Información leída el 27/09/1999 12:38:38 PM
		
		With lrecinscreNumerator_Finance
			.StoredProcedure = "inscreNumerator_Finance"
			.Parameters.Add("nTypenum", 14, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrd_num", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nResult", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nContrat = .Parameters.Item("nResult").Value
			End If
		End With
		
Find_GenerateContrat_Err: 
		If Err.Number Then
			Find_GenerateContrat = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinscreNumerator_Finance may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinscreNumerator_Finance = Nothing
	End Function
	
	'%InsValFI001_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "FI001_K"
	Public Function insValFI001_K(ByVal nTransaction As Integer, ByVal dEffecdate As Date, ByVal nContrat As Double, ByVal dLedger_dat As Date, ByVal nCompany As Integer, ByVal nOffice As Integer, ByVal nCurrency As Integer, ByVal sClient As String, ByVal sDigit As String, ByVal nInterest As Double, ByVal nInitial As Double, ByVal nQ_draft As Integer, ByVal nFrequency As Integer, ByVal dFirst_draf As Date, ByVal nDscto_amo As Double, ByVal dFirst_drafSys As Date, ByVal nUsercode As Integer, ByVal nBill_Day As Integer, ByVal nWay_Pay As Integer, ByVal nDscto_pag As Double, ByVal sOptType As String, ByVal nPolicy As Double, ByVal dLast_draft As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsColformRef As eCollection.ColformRef
		Dim lclsLedger As Object
		Dim lclsCtrol_date As eGeneral.Ctrol_date
		Dim lclsFinanceCO As eFinance.financeCO
		Dim lclsFinanceDrafts As eFinance.FinanceDrafts
		Dim lclsClient As eClient.Client
		Dim lclsOptFinance As eFinance.OptFinance
		Dim nMinValue As Double
		Dim nMaxValue As Double
		Dim nFrec As Integer
		Dim nFrequency_aux As Short
		Dim lblnError As Boolean
		Dim lclsPolicy As Object
		
		On Error GoTo insValFI001_K_Err
		
		lclsErrors = New eFunctions.Errors
		lclsFinanceCO = New financeCO
		
		'+ Validacion del campo Transacción
		If nTransaction = 0 Or nTransaction = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("FI001_K", 21009)
			lblnError = True
		End If
		
		'+Validacion de la vía de pago
		If nWay_Pay = eRemoteDB.Constants.intNull And nTransaction = eFinanceTransac.eftAddContrat Then
			Call lclsErrors.ErrorMessage("FI001_K", 38044)
			lblnError = True
		End If
		
		'+Validacion del dia de pago
		If Not (nTransaction = eFinanceTransac.eftQuerycontrat) Then
			If nBill_Day < 1 Or nBill_Day > 31 Then
				Call lclsErrors.ErrorMessage("FI001_K", 55009)
				lblnError = True
			End If
		End If
		
		'+ Se valida el campo fecha de efecto
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If IsNothing(dEffecdate) Or dEffecdate = eRemoteDB.Constants.dtmNull Then
			If nTransaction <> eFinanceTransac.eftQuerycontrat And nTransaction <> eFinanceTransac.eftRecoveryContrat Then
				Call lclsErrors.ErrorMessage("FI001_K", 21006)
				lblnError = True
			End If
		Else
			If nTransaction = eFinanceTransac.eftUpDateContrat Then
				If lclsFinanceCO.Find(nContrat, dEffecdate) Then
					If dEffecdate < lclsFinanceCO.dEffecdate Then
						Call lclsErrors.ErrorMessage("FI001_K", 21007)
						lblnError = True
					End If
				End If
			End If
		End If
		
		'+ Validación del campo Contrato
		If nContrat = 0 Or nContrat = eRemoteDB.Constants.intNull Then
			If nTransaction <> eFinanceTransac.eftAddContrat Then
				Call lclsErrors.ErrorMessage("FI001_K", 3357)
				lblnError = True
			End If
		Else
			If lclsFinanceCO.Find_Contrat(nContrat, True) Then
				Select Case nTransaction
					Case eFinanceTransac.eftAddContrat
						Call lclsErrors.ErrorMessage("FI001_K", 21001)
						lblnError = True
					Case eFinanceTransac.eftRecoveryContrat
						If lclsFinanceCO.nStat_contr <> Estat_contr.Eincompletecapture Then
							Call lclsErrors.ErrorMessage("FI001_K", 21003)
							lblnError = True
						End If
					Case eFinanceTransac.eftUpDateContrat
						If lclsFinanceCO.nStat_contr = Estat_contr.Eannul Then
							Call lclsErrors.ErrorMessage("FI001_K", 21005)
							lblnError = True
						Else
							If lclsFinanceCO.nStat_contr = Estat_contr.Eincompletecapture Then
								Call lclsErrors.ErrorMessage("FI001_K", 21134)
								lblnError = True
							Else
								If lclsFinanceCO.nStat_contr <> Estat_contr.Einitialwait Then
									Call lclsErrors.ErrorMessage("FI001_K", 21004)
									lblnError = True
								End If
							End If
						End If
				End Select
				
				If nTransaction = eFinanceTransac.eftUpDateContrat Then
					If dEffecdate < lclsFinanceCO.dEffecdate Then
						Call lclsErrors.ErrorMessage("FI001_K", 21007)
						lblnError = True
					End If
				End If
				
				'+ Buscar la fecha de efecto para dicho contrato
				If nTransaction = eFinanceTransac.eftQuerycontrat Or nTransaction = eFinanceTransac.eftRecoveryContrat Then
					dEffecdate = lclsFinanceCO.dEffecdate
				End If
				
				'+ Se llama la validación del contrato
			Else
				If nTransaction = eFinanceTransac.eftQuerycontrat Or nTransaction = eFinanceTransac.eftRecoveryContrat Or nTransaction = eFinanceTransac.eftUpDateContrat Then
					Call lclsErrors.ErrorMessage("FI001_K", 21002)
					lblnError = True
				End If
			End If
		End If
		
		If Not lblnError Then
			
			'+ Se valida la fecha de contabilización con respecto a la de los asientos
			lclsLedger = eRemoteDB.NetHelper.CreateClassInstance("eLedge.Ledger")
			If dLedger_dat <> eRemoteDB.Constants.dtmNull Then
				If lclsLedger.Find Then
					If dLedger_dat < lclsLedger.dStart_date Then
						Call lclsErrors.ErrorMessage("FI001_K", 1006)
					End If
				End If
			End If
			'UPGRADE_NOTE: Object lclsLedger may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsLedger = Nothing
			
			lclsCtrol_date = New eGeneral.Ctrol_date
			If lclsCtrol_date.Find(7) Then
				'+ Se valida la fecha de contabilización con respecto a la de los asientos
				If dLedger_dat <> eRemoteDB.Constants.dtmNull Then
					If dLedger_dat < lclsCtrol_date.dEffecdate Then
						Call lclsErrors.ErrorMessage("FI001_K", 1008)
					End If
				End If
			End If
			'UPGRADE_NOTE: Object lclsCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsCtrol_date = Nothing
			
			'+ Validación del campo Zona
			If nTransaction <> eFinanceTransac.eftQuerycontrat Then
				If nOffice = 0 Or nOffice = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage("FI001_K", 1040)
				End If
			End If
			
			'+ Validación del campo Moneda
			If nTransaction <> eFinanceTransac.eftQuerycontrat Then
				If nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage("FI001_K", 10107)
				End If
			End If
			
			'+ Validación del campo Cliente
			If (sClient = String.Empty Or sDigit = String.Empty) And nTransaction <> eFinanceTransac.eftQuerycontrat Then
				Call lclsErrors.ErrorMessage("FI001_K", 2001)
			Else
				lclsClient = New eClient.Client
				If lclsClient.Find(sClient) Then
					sClientName = lclsClient.sCliename
					If nTransaction <> eFinanceTransac.eftQuerycontrat Then
						If lclsClient.dDeathdat <> eRemoteDB.Constants.dtmNull Then
							Call lclsErrors.ErrorMessage("FI001_K", 2051)
						ElseIf lclsClient.sBlockade = "1" Then 
							Call lclsErrors.ErrorMessage("FI001_K", 2052)
						End If
					End If
				End If
				'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsClient = Nothing
			End If
			
			'+ Validación del campo Póliza
			If sOptType = "1" Then
				If nPolicy = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage("FI001_K", 3003)
				Else
					lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
					If Not lclsPolicy.FindPolicybyPolicy("2", nPolicy) Then
						Call lclsErrors.ErrorMessage("FI001_K", 13262)
					Else
						lclsColformRef = New eCollection.ColformRef
						If Not lclsColformRef.valExistsCO001_K("2", lclsPolicy.nBranch, lclsPolicy.nProduct, nPolicy, 0, "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull) Then
							Call lclsErrors.ErrorMessage("FI001_K", 750114)
						End If
						'UPGRADE_NOTE: Object lclsColformRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsColformRef = Nothing
						'+ Valida que la ultima fecha del contrato se encuentre dentro de la vigencia de la póliza
						If lclsPolicy.dExpirdat < dLast_draft Then
							Call lclsErrors.ErrorMessage("FI001_K", 4019,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha vencimiento última cuota:")
						End If
					End If
					'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsPolicy = Nothing
				End If
			End If
			
			'+ Validación del campo Fecha de contabilización
			If nTransaction <> eFinanceTransac.eftQuerycontrat Then
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				If IsNothing(dLedger_dat) Or dLedger_dat = eRemoteDB.Constants.dtmNull Then
					Call lclsErrors.ErrorMessage("FI001_K", 1087)
				End If
			End If
			
			If nTransaction <> eFinanceTransac.eftQuerycontrat Then
				If nInitial = 0 Or Fix(nInitial) = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage("FI001_K", 21084)
				End If
			End If
			
			'+ Validación del campo Cantidadd de giros
			If nTransaction <> eFinanceTransac.eftQuerycontrat Then
				If nQ_draft = 0 Or nQ_draft = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage("FI001_K", 21011)
				Else
					If Not (nQ_draft >= 1 And nQ_draft <= 24) Then
						Call lclsErrors.ErrorMessage("FI001_K", 21012)
					End If
				End If
			End If
			
			'+ Validación del campo "Frcuencia de giros"
			If nTransaction <> eFinanceTransac.eftQuerycontrat Then
				If nFrequency = 0 Or nFrequency = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage("FI001_K", 21013)
				Else
					If Not nQ_draft = 0 And Not nQ_draft = eRemoteDB.Constants.intNull Then
						Select Case nFrequency
							Case eFrequency.efMonthly
								nFrequency_aux = 1 * nQ_draft
							Case eFrequency.efQuarterly
								nFrequency_aux = 3 * nQ_draft
							Case eFrequency.efNot_Stand
								nFrequency_aux = 0
						End Select
						
						If DateAdd(Microsoft.VisualBasic.DateInterval.Month, nFrequency, dEffecdate) > DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, dEffecdate) Then
							Call lclsErrors.ErrorMessage("FI001_K", 21015)
						End If
						
						If nFrequency = 1 Then
							If dFirst_draf > DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, dEffecdate) Then
								Call lclsErrors.ErrorMessage("FI001_K", 21015)
							End If
						End If
					End If
				End If
			End If
			
			'+ Validación del campo "Fecha de vencimiento"
			If nTransaction <> eFinanceTransac.eftQuerycontrat Then
				If dFirst_draf = eRemoteDB.Constants.dtmNull Then
					Call lclsErrors.ErrorMessage("FI001_K", 21083)
				Else
					If dFirst_drafSys > dFirst_draf Then
						Call lclsErrors.ErrorMessage("FI001_K", 55900,  , eFunctions.Errors.TextAlign.LeftAling, "Vencimiento de la primera cuota (" & dFirst_drafSys & ")")
						'+ Se valida que la fecha de vencimiento del primer giro sea compatible con la frecuencia de pago
					ElseIf nFrequency <> 1 Then 
						
						Select Case nFrequency
							Case eFrequency.efMonthly
								nFrec = 1
							Case eFrequency.efQuarterly
								nFrec = 3
							Case eFrequency.efNot_Stand
								nFrec = 0
						End Select
						
						If dFirst_draf < DateAdd(Microsoft.VisualBasic.DateInterval.Month, nFrec, dEffecdate) Then
							Call lclsErrors.ErrorMessage("FI001_K", 21082)
						End If
					End If
				End If
			End If
			
			If nTransaction <> eFinanceTransac.eftQuerycontrat Then
				lclsOptFinance = New OptFinance
				With lclsOptFinance
					
					If Not .Find Then
						.nDscto_pag = nDscto_pag
						.nPay_down = 0
						.nPay_up = 0
					End If
					
					'+ Validación del campo "Porcentaje de interes"
					If nInterest <> 0 And nInterest <> eRemoteDB.Constants.intNull Then
						If nDscto_pag <> 0 And nDscto_pag <> eRemoteDB.Constants.intNull Then
							Call lclsErrors.ErrorMessage("FI001_K", 60594)
						End If
						'+ Validación del factor de interes indicado
						lclsFinanceDrafts = New eFinance.FinanceDrafts
						If lclsFinanceDrafts.SearchFactor(nQ_draft, nInterest, dEffecdate) = 0 Then
							Call lclsErrors.ErrorMessage("FI001_K", 750126)
						End If
						'UPGRADE_NOTE: Object lclsFinanceDrafts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsFinanceDrafts = Nothing
					End If
					
					'+ Validación del campo "Descuento por pronto pago"
					If nDscto_pag <> 0 And nDscto_pag <> eRemoteDB.Constants.intNull Then
						
						If nDscto_pag <> .nDscto_pag Then
							'+Se verifica que se pueda aumentar o disminuir
							If nDscto_pag < .nDscto_pag And .sCh_pay_down <> "1" Then
								Call lclsErrors.ErrorMessage("FI001_K", 55925,  , eFunctions.Errors.TextAlign.RigthAling, " de Pronto Pago")
							ElseIf nDscto_pag > .nDscto_pag And .sCh_pay_up <> "1" Then 
								Call lclsErrors.ErrorMessage("FI001_K", 55926,  , eFunctions.Errors.TextAlign.RigthAling, " de Pronto Pago")
							Else
								'+Se verifica el rango de aumento/disminucion
								nMinValue = .nDscto_pag - ((.nDscto_pag * .nPay_down) / 100)
								nMaxValue = .nDscto_pag + ((.nDscto_pag * .nPay_up) / 100)
								If nDscto_pag > .nDscto_pag And nDscto_pag > nMaxValue Then
									Call lclsErrors.ErrorMessage("FI001_K", 21143,  , eFunctions.Errors.TextAlign.RigthAling, "( hasta " & nMaxValue & "% )")
								ElseIf nDscto_pag < .nDscto_pag And nDscto_pag < nMinValue Then 
									Call lclsErrors.ErrorMessage("FI001_K", 21144,  , eFunctions.Errors.TextAlign.RigthAling, "( desde " & nMinValue & "% )")
								End If
							End If
						End If
					End If
					
				End With
				
				'UPGRADE_NOTE: Object lclsOptFinance may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsOptFinance = Nothing
			End If
		End If
		insValFI001_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		
insValFI001_K_Err: 
		If Err().Number Then
			insValFI001_K = insValFI001_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% CalInterest: This function calculates the interest
	'% CalInterest: Esta función se encarga de calcular el interés a aplicar.
	Public Function CalInterest(ByVal dFirstDate As Date, ByVal dLastDate As Date) As Double
		
		'**- Variable definition. This variable contains the quantity of days between one draft and another.
		'-Se define la variable que contiene la cantidad de dias que hay entre un giro y otro.
		Dim lintDays As Integer
		
		'**- Variable definition. This variable contains the quantity of months between one draft and another.
		'-Se define la variable que contiene la cantidad de meses que hay entre un giro y otro.
		Dim ldtmMonth As Integer
		
		'**- Variable definition. This variable contains the quantity of days between the first date from the first day of the month.
		'-Se define la variable que contiene la cantidad de días que hay en la primera fecha a partir del primer día del mes.
		Dim ldtmDaysFirst As Integer
		
		'**- Variable definition. This variable contains the quantity of days between the second date from the first day of the month.
		'-Se define la variable que contiene la cantidad de días que hay en la segunda fecha a partir del primer día del mes.
		Dim ldtmDaysLast As Integer
		
		'**- Variable definition. This variable contains the quantity of days to calculate the interest.
		'-Se define la variable que indica la cantidad de dias a utilizar en el cálculo del interés.
		Dim lintsTime_exa As Integer
		
		Dim lclsOptFinance As OptFinance
		lclsOptFinance = New OptFinance
		
		On Error GoTo CalInterest_Err
		
		lclsOptFinance.Find()
		
		ldtmDaysFirst = VB.Day(dFirstDate)
		ldtmDaysLast = VB.Day(dLastDate)
		ldtmMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, dFirstDate, dLastDate)
		'UPGRADE_WARNING: DateDiff behavior may be different. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"'
		lintDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, dFirstDate, dLastDate)
		
		'**+If the indicator of exact time is checked
		'+Si el indicador para utilizar Tiempo exacto esta en Verdadero.
		If lclsOptFinance.sTime_exa = CStr(OptFinance.eTime_exa.etExact) Then
			lintsTime_exa = lintDays
		Else
			lintsTime_exa = ldtmMonth * 30 + System.Math.Abs(dLastDate.ToOADate - dFirstDate.ToOADate)
		End If
		
		'**+If the indicator of exact method interest calculation is checked
		'+Si el indicador para calcular el interés es por el método exacto.
		If lclsOptFinance.sInterest_e = "1" Then
			CalInterest = (nInterest / 100) * (lintsTime_exa / 365)
		Else
			CalInterest = (nInterest / 100) * (lintsTime_exa / 360)
		End If
		
		'UPGRADE_NOTE: Object lclsOptFinance may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsOptFinance = Nothing
		
CalInterest_Err: 
		If Err.Number Then
			CalInterest = 0
		End If
	End Function
	
	
	'**%insValFI012_k: This method validates the header section of the page "FI012_k" as described in the
	'**%functional specifications
	'%InsValFI012_k: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "FI012_k"
	Public Function insValFI012_k(ByVal sCodispl As String, ByVal nContrat As Double, ByVal nQ_draft As Integer, ByVal dStat_date As Date) As String
		
		Dim lclsFinanceDraft As eFinance.FinanceDraft
		Dim lclsGeneral As eGeneral.Ctrol_date
		Dim lclsErrors As eFunctions.Errors
		Dim lclsLedge As Object
		
		lclsFinanceDraft = New eFinance.FinanceDraft
		lclsGeneral = New eGeneral.Ctrol_date
		lclsErrors = New eFunctions.Errors
		lclsLedge = eRemoteDB.NetHelper.CreateClassInstance("eLedge.Led_compan")
		
		On Error GoTo insValFI012_k_Err
		
		'**+The "contrat" must be filled
		'+Verifica que el campo "Contrato" esté lleno
		Dim errorNumber As Integer
		If nContrat <> eRemoteDB.Constants.intNull And nContrat <> 0 Then
			
			'**+Verifies that the contrat is registered and and standing
			'+Verifica que el contrato este registrado y que esté vigente
			If Not Find_Contrat(nContrat, True) Then
				Call lclsErrors.ErrorMessage(sCodispl, 21002)
			Else
				'**+This variable contains the error number to evaluate the contract status
				'+Variable que contendrá el número del error para evaluar el estado del contrato
				
				
				'**+Validates that the contract is standing
				'+Se valida que el contrato esté en Vigor
				
				Select Case nStat_contr
					Case Estat_contr.Eannul
						errorNumber = 21005
						
					Case Estat_contr.Eincompletecapture
						errorNumber = 21134
						
					Case Estat_contr.Epayment
						errorNumber = 21095
				End Select
				
				If errorNumber <> 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, errorNumber)
				End If
			End If
		Else
			Call lclsErrors.ErrorMessage(sCodispl, 21062)
		End If
		
		'**+Verifies that the field "Draft" is filled
		'+Verifica que el campo "Giro" esté lleno
		
		If nQ_draft <> eRemoteDB.Constants.intNull Or nQ_draft <> 0 Then
			With lclsFinanceDraft
				If .ValBeforeDraft(nContrat, nQ_draft, FinanceDraft.eStat_Draft.esdOutStatnding) Then
					
					'**+This statement sends the warning about the unpayed previous drafts
					'+Manda el mensaje de advertencia referente a giros anteriores
					'+pendientes
					Call lclsErrors.ErrorMessage(sCodispl, 21064)
				End If
				
				If Not .Find(nContrat, nQ_draft, True) Then
					
					'**Verifies that the draft is registered in the drafts file
					'+Verifica que el giro esta registrado en el archivo de giros
					Call lclsErrors.ErrorMessage(sCodispl, 21041)
				Else
					'**+The draft must be unpayed
					'+El giro debe estar pendiente de cobro
					
					If Not .nStat_draft = FinanceDraft.eStat_Draft.esdOutStatnding Then
						Call lclsErrors.ErrorMessage(sCodispl, 21042)
					Else
						
						'**+The draft can't be collected if the down payment is unpayed
						'+El giro no se puede cobrar  si la cuota
						'+inicial esta pendiente
						
						If Me.nStat_contr = Estat_contr.Einitialwait Then
							Call lclsErrors.ErrorMessage(sCodispl, 21999)
						End If
					End If
				End If
			End With
		Else
			Call lclsErrors.ErrorMessage(sCodispl, 21063)
		End If
		
		'**+Verifies that the field "Collect date" is filled and is valid
		'+Verifica que el campo "Fecha de cobro" esté lleno y que sea valido
		
		If dStat_date = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 21059)
		Else
			
			'**+The collect date must be minor or equal than today
			'+ La fecha de cobro debe ser menor o igual a la fecha del día
			If dStat_date > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 1002)
			End If
			
			'**+ The collection date must be greater than the effective date of the contract
			'+ La fecha de cobro dDebe ser mayor a la fecha de efecto del contrato
			
			If Me.dEffecdate > dStat_date Then
				Call lclsErrors.ErrorMessage(sCodispl, 21007)
			End If
			
			'**+ The collection date must be greater than the date of the last automatic entries process
			'+ Se valida que la fecha de cobro sea posterior al último proceso de asientos automáticos
			
			If lclsLedge.Find_Date_Init(gintCompany) Then
				If dStat_date < lclsLedge.dDate_init Then
					Call lclsErrors.ErrorMessage(sCodispl, 1008)
				End If
			End If
			
			'**+ Validates the collection date in reference to the begining of the standing accounting period
			'+ Se valida la fecha de cobro con respecto a la de inicio del período contable en vigor
			
			If lclsGeneral.Find(1) Then
				If dStat_date < lclsGeneral.dEffecdate Then
					Call lclsErrors.ErrorMessage(sCodispl, 1006)
				End If
			End If
			
		End If
		
		insValFI012_k = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceDraft = Nothing
		'UPGRADE_NOTE: Object lclsLedge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLedge = Nothing
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValFI012_k_Err: 
		If Err.Number Then
			insValFI012_k = insValFI012_k & Err.Description
		End If
	End Function
	
	'%insPreFI012: This function validates the data introduced in the detail zone of  the form
	'%insPreFI012: Esta función se encarga de validar los datos introducidos en la zona de
	'%cabecera.
	Public Function insPreFI012(ByVal nContrat As Double, ByVal nQ_draft As Integer, ByVal dStat_date As Date) As Boolean
		Dim lclsFinanceDraft As eFinance.FinanceDraft
		Dim lclsOptFinance As eFinance.OptFinance
		
		lclsFinanceDraft = New eFinance.FinanceDraft
		lclsOptFinance = New eFinance.OptFinance
		
		insPreFI012 = True
		
		Call Find_Contrat(nContrat, True)
		
		'**+ Reads the draft data
		'+ Se vuelve a buscar los datos del giro
		
		If lclsFinanceDraft.Find(nContrat, nQ_draft) Then
			
			'** Loads the default values of the instalation
			'+Trae los valores por defecto de la installacion
			
			Call lclsOptFinance.Find()
			
			Me.sClientName = Me.sClientName
			Me.sClient = Me.sClient
			Me.nDraft_amo = lclsFinanceDraft.nAmount
			Me.dExpirDate = lclsFinanceDraft.dExpirdat
			nAmountFD = lclsFinanceDraft.nAmount
			
			'**+ Loads the default currency in the variable
			'+ Guarda en la variable la moneda por defecto
			
			nCurrency = Me.nCurrency
			
			'**+ Adds the pay in advance or interest in arrears
			'+ Agrega el Dsto de Pronto pago o el Interesa de Mora
			
			If dStat_date < lclsFinanceDraft.dExpirdat Then
				Me.nInterest = 0
				
				'**+ Discount rate to applie for the pay in advance in the installation options
				'+ Porcentaje de descuento a aplicar de pronto pago por opciones de Instalacion
				
				
				
				'+ El total a descontar es el porcentaje de las opciones de
				'+ instalación - el importe a descontar dado en la secuencia
				
				Me.nDscto_amo = Me.nDscto_amo
			ElseIf dStat_date > lclsFinanceDraft.dExpirdat Then 
				
				'** Loads the interest of the opt_financ table
				'+ El interes es tomado de la tabla Opt_financ
				
				Me.nInterest = lclsFinanceDraft.nAmount * lclsOptFinance.nIntdelay / 100
				Me.nDscto_amo = 0
			End If
			
			Me.nExpenses = 0
			
			'**+ Adds the exchange rate
			'+ Agrega el Factor de Cambio
			
			Me.nCurr_cont = Me.nCurrency
			Call Curr_cont_Change()
			
			'** + This variable contains the exchange rate
			'+ Variable que contiene el factor de cambio
			
			Me.nAmount = Me.nDraft_amo / Me.nExchange
			
			'**+ Total calculation
			'+Calcular Total
			
			Call insTotalAmo()
		End If
		'**+ Verifies in the installation options if it is permited to increase or decrease the pay in advance
		'+ verifica que en las opciones de la instalación se
		'+ tenga el indicador para aumentar o disminuir el porcentaje
		'+ de Dcto pronto pago.
		
		If (lclsOptFinance.sCh_pay_up = CStr(OptFinance.PermissionState.Affirmative) Or lclsOptFinance.sCh_pay_down = CStr(OptFinance.PermissionState.Affirmative)) And lclsFinanceDraft.dExpirdat > Today Then
			blnDscto_amo = False
		Else
			blnDscto_amo = True
		End If
		
		'**+ Verifies in the installation options if it is permited to increase or decrease the interest in arrears.
		'+ verifica que en las opciones de la instalación se
		'+ tenga el indicador para aumentar o disminuir el
		'+ porcentaje de MORA.
		
		If (lclsOptFinance.sCh_del_up = CStr(OptFinance.PermissionState.Affirmative) Or lclsOptFinance.sCh_del_down = CStr(OptFinance.PermissionState.Affirmative)) And lclsFinanceDraft.dExpirdat < Today Then
			blnInterest = False
		Else
			blnInterest = True
		End If
		
		
		'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceDraft = Nothing
		'UPGRADE_NOTE: Object lclsOptFinance may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsOptFinance = Nothing
		
	End Function
	
	'%insTotalAmo: This routine assings the total collection
	'%TOTAL = (Draft amount + interest in arrears/expenses - Pay in advance)
	'%insTotalAmo: Esta rutina es la encargada de asignar
	'%el total general a cobrar:
	'%TOTAL = (Importe del Giro + mora/gastos - Pronto Pago)
	Private Sub insTotalAmo()
		
		'**+ If the interest is null the system assigns zero.
		'+Valida que si es nulo asigne un cero para poder sumar
		
		If Me.nInterest = eRemoteDB.Constants.intNull Then
			Me.nInterest = 0
		End If
		'**+ If the interest is null the system assigns zero.
		'+Valida que si es nulo asigne un cero para poder sumar
		
		If Me.nExpenses = eRemoteDB.Constants.intNull Then
			Me.nExpenses = 0
		End If
		
		'**+ Total = Draft amount + collected amount of interest in arrears/expenses - pay in advance discount
		'+ El total se calcula sumando el monto del giro y el importe
		'+cobrado por mora/gastos y restando el dcto. por pronto pago.
		
		Me.nTotalAmo = (Me.nAmount + Me.nInterest + Me.nExpenses) - Me.nDscto_amo
	End Sub
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		gintCompany = 1
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Sub Curr_cont_Change()
		
		'** Makes the currency changes
		'+ Hace los cambios de la moneda
		
		insExchangeMake()
		
		'**+ After the changes the system keeps the last updted currency
		'+Despues de hacer Los cambios se guarda la ultima moneda actualizada
		
		nCurrency = Me.nCurr_cont
	End Sub
	'**%insExchangeMake: This routine assigns the changes of the values according the chosen currency
	'%insExchangeMake: Esta rutina es la encargada de asignar los cambios
	'%de los valores segun la moneda escogida.
	Private Sub insExchangeMake()
		
		'** Instance the class that manage the currency changes
		'+Se instancia la clase que maneja los cambios de moneda
		
		Dim lclsExchange As eGeneral.Exchange
		Dim lclsFinanceDraft As eFinance.FinanceDraft
		
		lclsExchange = New eGeneral.Exchange
		lclsFinanceDraft = New eFinance.FinanceDraft
		
		'** Makes the change of the amount
		'+Se hace el cambio de El Monto del Importe
		
		With lclsExchange
			.Convert(eRemoteDB.Constants.intNull, nAmountFD, Me.nCurrency, Me.nCurr_cont, Today, 0)
			
			If .pdblExchange = -1 Then
				Me.nExchange = 1
			Else
				Me.nExchange = .pdblExchange
			End If
			
			Me.nAmount = .pdblResult
			
			'**+ Makes the change of the interest in arrears
			'+Se hace el cambio de El interes de MORA
			
			.Convert(eRemoteDB.Constants.intNull, Me.nInterest, Me.nCurrency, Me.nCurr_cont, Today, 0)
			
			Me.nInterest = .pdblResult
			
			'**+ Makes the change of the pay in advance discount
			'+Se hace el cambio de El Dscto por Pronto Pago
			
			.Convert(eRemoteDB.Constants.intNull, Me.nDscto_amo, Me.nCurrency, Me.nCurr_cont, Today, 0)
			
			Me.nDscto_amo = .pdblResult
			
			'**+ Makes the changes of the expenses
			'+Se hace el cambio de Los GASTOS
			
			.Convert(eRemoteDB.Constants.intNull, Me.nExpenses, Me.nCurrency, Me.nCurr_cont, Today, 0)
			
			Me.nExpenses = .pdblResult
		End With
		
		'** Calculate the total
		'+ Calcular el Total
		
		Call insTotalAmo()
	End Sub
	
	'**%insValFI012: This method validates the page "FI012" as described in the functional specifications
	'%InsValFI012: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "FI012"
	Public Function insValFI012(ByVal sCodispl As String, ByVal nContrat As Double, ByVal nQ_draft As Integer, ByVal dStat_date As Date, ByVal nExchange As Double, ByVal nDscto_amo As Double, ByVal nAmount As Double, ByVal nInterest As Double, ByVal sPayment_in As String) As String
		
		Dim lclsFinanceDraft As eFinance.FinanceDraft
		Dim lclsErrors As eFunctions.Errors
		Dim lclsOptFinance As eFinance.OptFinance
		
		lclsFinanceDraft = New eFinance.FinanceDraft
		lclsErrors = New eFunctions.Errors
		lclsOptFinance = New eFinance.OptFinance
		
		'**+Variable that keeps the maximum amount to discount according the installation options
		'+Variable que Guarda el monto Maximo a descontar segun las
		'+opciones de instalacion
		
		Dim ldblMaxValue As Double
		
		'**+ This variable cotains the minimum a to discount according to the intallation options
		'+Variable que Guarda el monto Minimo a descontar segun las
		'+opciones de instalacion
		
		Dim ldblMinValue As Double
		
		Dim llngDescto_amo As Integer
		
		On Error GoTo insValFI012_Err
		
		Call lclsFinanceDraft.Find(nContrat, nQ_draft, True)
		Call Me.Find_Contrat(nContrat, True)
		Call lclsOptFinance.Find()
		
		
		'**+ Pay in advance discount validations
		'+Validacion del CAMPO de Descuento por pronto pago
		
		If dStat_date < lclsFinanceDraft.dExpirdat Then
			If nDscto_amo <> eRemoteDB.Constants.intNull And nDscto_amo <> 0 Then
				
				'+ Validar opciones de permisologia especificas del
				'+ campo de Descuento.
				
				'+ Importe que se sumará al Dscto por pronto esto es tomado de FinanceCo
				'+ Dividido entre el factor de cambio
				
				llngDescto_amo = Me.nDscto_amo / nExchange
				
				'**+ Minimum rate of discount to aply for pay in advance for the intallations options
				'+ Minimo Porcentaje de descuento a aplicar de pronto pago por opciones de Instalacion
				
				ldblMinValue = llngDescto_amo - (llngDescto_amo * lclsOptFinance.nPay_down / 100)
				
				'**+ Maximum rate of discount to aply for pay in advance for the intallations options
				'+ Maximo Porcentaje de descuento a aplicar de pronto pago por opciones de Instalacion
				
				ldblMaxValue = llngDescto_amo + (llngDescto_amo * lclsOptFinance.nPay_up / 100)
				
				'**+ Validate that the applied discount never be greater than the amount
				'+Validar que el descuento a aplicar nunca sea mayor del monto
				
				If nDscto_amo < nAmount Then
					
					'**+ Validates que the pay in advance discount can be increased
					'+Se Valida que se pueda aumentar el Pronto PAGO
					
					If lclsOptFinance.sCh_pay_up = CStr(OptFinance.PermissionState.Negative) Then
						If nDscto_amo > llngDescto_amo Then
							Call lclsErrors.ErrorMessage(sCodispl, 21141)
							Me.nDscto_amo = llngDescto_amo
						End If
					Else
						
						'**+ Validates the maximum percentage of change that can be suffer by the pay in advance discount
						'+Se Valida el porcentaje maximo que puede sufrir el Pronto PAGO
						
						If nDscto_amo > ldblMaxValue And ldblMaxValue > 0 Then
							Call lclsErrors.ErrorMessage(sCodispl, 21143)
							Me.nDscto_amo = ldblMaxValue + llngDescto_amo
						End If
					End If
					
					'**+ Validates that the pay in advance discount can be decreased
					'+Se Valida que se pueda disminuir el Pronto PAGO
					
					If lclsOptFinance.sCh_pay_down = CStr(OptFinance.PermissionState.Negative) Then
						If nDscto_amo < llngDescto_amo Then
							Call lclsErrors.ErrorMessage(sCodispl, 21142)
							Me.nDscto_amo = llngDescto_amo
						End If
					Else
						
						'**+ Validates the minimum rate of change that can be suffer by the pay in advance discount
						'+Se Valida el porcentaje minimo que puede sufrir el Pronto PAGO
						
						If nDscto_amo < ldblMinValue And nDscto_amo > 0 Then
							Call lclsErrors.ErrorMessage(sCodispl, 21144)
							Me.nDscto_amo = ldblMinValue
						End If
					End If
				Else
					Call lclsErrors.ErrorMessage(sCodispl, 21145)
					
					'**+ Clean the field of the pay in advance discount
					'+Limpiar el campo de Descuento de Pronto pago para que no de negativo
					
					Me.nDscto_amo = llngDescto_amo
					
				End If
			Else
				Me.nDscto_amo = 0
			End If
		End If
		
		'**+ Amount collected bye the interests in arrears validations
		'+Validacion del CAMPO de Cantidad Cobrada por Intereses de MORA
		
		Dim ldblMaxValueI As Double
		Dim ldblMinValueI As Double
		If dStat_date > lclsFinanceDraft.dExpirdat Then
			If nInterest <> eRemoteDB.Constants.intNull And nInterest <> 0 Then
				
				'+ Validacion de la permisologia sobre el interes de MORA
				
				'**+ This variable contains the maximum amount to discount according to the installation options
				'+Variable que Guarda el monto Maximo a descontar segun las
				'+opciones de instalacion
				
				'**+ This variable contains the minimum amount to discount according to the installation options
				'+Variable que Guarda el monto Minimo a descontar segun las
				'+opciones de instalacion
				
				
				pdblPercent = nAmount * lclsOptFinance.nIntdelay / 100
				
				'**+ Validates that the interest in arrears can be increased
				'+Se Valida que se pueda aumentar el interes de mora
				
				If lclsOptFinance.sCh_del_up = CStr(OptFinance.PermissionState.Negative) Then
					If nInterest > pdblPercent Then
						Call lclsErrors.ErrorMessage(sCodispl, 21065)
						Me.nInterest = pdblPercent
					End If
				Else
					'**+ Validates the maximum rate of change that can be suffer by the interest in arrears
					'+Se Valida el porcentaje maximo que puede sufrir el Interes de MORA
					
					ldblMaxValueI = pdblPercent + (pdblPercent * lclsOptFinance.nInt_del_up / 100)
					
					If nInterest > ldblMaxValueI Then
						Call lclsErrors.ErrorMessage(sCodispl, 21139)
						Me.nInterest = ldblMaxValueI
					End If
				End If
				
				'**+ Validates that the interest in arrears can be decreased
				'+Se Valida que se pueda disminuir el interes de mora
				
				If lclsOptFinance.sCh_del_down = CStr(OptFinance.PermissionState.Negative) Then
					If nInterest < Int(pdblPercent) Then
						Call lclsErrors.ErrorMessage(sCodispl, 21065)
						Me.nInterest = pdblPercent
					End If
				Else
					'**+ Validates the minimum rate of change that can be suffer by the interest in arrears
					'+Se Valida el porcentaje minimo que puede sufrir el Interes de MORA
					
					ldblMinValueI = pdblPercent - (pdblPercent * lclsOptFinance.nInt_del_down / 100)
					
					If nInterest < ldblMinValueI And nInterest > 0 Then
						Call lclsErrors.ErrorMessage(sCodispl, 21140)
						Me.nInterest = ldblMinValueI
					End If
				End If
			Else
				Me.nInterest = pdblPercent
			End If
		End If
		
		'**+ Pay form validations
		'+Validacion del CAMPO de Foram de Pago
		
		'**+ Verifies that the field is filled
		'+Verificar que el campo este lleno
		
		If sPayment_in = String.Empty Or sPayment_in = "0" Then
			Call lclsErrors.ErrorMessage(sCodispl, 21060)
		End If
		
		'** Calculate the total amount
		'Calcular el total
		
		Call insTotalAmo()
		
		'** Makes the link with the payment windows
		'+ Se realiza el enlace con las ventanas de pago
		'        If cbePayWay.Value = eptRealCash Or _
		''           cbePayWay.Value = eptChqCash Then
		'            If Not insShowOP001 Then
		'                insValFolder = False
		'                lblnError = True
		'            End If
		'        Else
		'            If Not insShowCO001 Then
		'                insValFolder = False
		'            End If
		'        End If
		
		
		insValFI012 = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceDraft = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsOptFinance may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsOptFinance = Nothing
		
		
insValFI012_Err: 
		If Err.Number Then
			insValFI012 = insValFI012 & Err.Description
		End If
	End Function
	
	'**%insPostFI012. This method updates the database (as described in the functional specifications)
	'**%for the page "FI012"
	'%insPostFI012: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "FI012"
	Public Function insPostFI012(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nContrat As Double, ByVal nQ_draft As Integer, ByVal dStat_date As Date, ByVal nExpensive As Double, ByVal nDscto_amo As Double, ByVal nAmount As Double, ByVal nInterest As Double, ByVal nUsercode As Integer, ByVal nCurrency As Integer) As Boolean
		Dim lclsFinanceDraft As eFinance.FinanceDraft
		
		lclsFinanceDraft = New eFinance.FinanceDraft
		
		insPostFI012 = True
		
		On Error GoTo insPostFI012_Err
		
		Select Case nAction
			
			'**+ If the selected option is "Input"
			'+Si la opción es entrar
			
			Case eFunctions.Menues.TypeActions.clngActionInput
				
				'**+ Changes the status of the draft to "Collected" besides updates the history file
				'+ Cambia el giro como  "Cobrado", también se marca en el historial
				
				With lclsFinanceDraft
					Call .Find(nContrat, nQ_draft, True)
					.nContrat = nContrat
					.nDraft = nQ_draft
					
					'**+ Changes the status of the draft to "Collected"
					'+ Cambia el estado del contrato a "Cobrado"
					
					.nStat_draft = FinanceDraft.eStat_Draft.esdCollect
					'**+ Updates the history file
					'+ Movimiento a guardar en el historial como "Cobrado"
					
					.nAmount = nAmount
					.nCurrency = nCurrency
					.nDscto_pag = nDscto_amo
					.nExpensive = nExpensive
					.nInterest = nInterest
					.dStat_date = dStat_date
					.nType = FinanceDraft.eTypeMove.etmPayment
					.nUsercode = nUsercode
					
					If Not .UpDate Then
						insPostFI012 = False
					End If
				End With
				
				'**+ Verifies that this is the last draft of the contract
				'+ Verifica que sea la ultima cuota de un contrato
				
				With Me
					Call .Find_Contrat(nContrat, True)
					.nUsercode = nUsercode
					
					If .nQ_draft = nQ_draft Then
						.nStat_contr = Estat_contr.Epayment
						insPostFI012 = .UpDate
					End If
				End With
				
				'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsFinanceDraft = Nothing
				
		End Select
		
		
insPostFI012_Err: 
		If Err.Number Then
			insPostFI012 = False
		End If
	End Function
	
	'**%insValFIC006: This method validates the page "FIC006" as described in the functional specifications
	'%InsValFIC006: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "FIC006"
	Public Function insValFIC006(ByVal sCodispl As String, ByVal sContrat As String, ByVal sClient As String, ByVal sCliename As String, ByVal sDate As String, ByVal nStat_contr As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsUser As New eSecurity.User
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValFIC006_Err
		
		insValFIC006 = CStr(True)
		
		'**+ Contract validations
		'+ Se realizan las validaciones del campo "Contrato".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sContrat) And Not IsNothing(sContrat) And Trim(sContrat) <> String.Empty And Trim(sContrat) <> "0" Then
			If Not lclsUser.InsConstruct("Finance_co.nContrat", sContrat, eSecurity.User.eTypValConst.ConstNumeric) Then
				Call lobjErrors.ErrorMessage(sCodispl, 10222,  , eFunctions.Errors.TextAlign.LeftAling, "(Contrato) ")
			End If
		End If
		
		'**+ Client validations
		'+ Se realizan las validaciones del campo "Cliente".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sClient) And Not IsNothing(sClient) And Trim(sClient) <> String.Empty And Trim(sClient) <> "0" Then
			If Not lclsUser.InsConstruct("Finance_co.sClient", sClient, eSecurity.User.eTypValConst.ConstString) Then
				Call lobjErrors.ErrorMessage(sCodispl, 10222,  , eFunctions.Errors.TextAlign.LeftAling, "(Cliente) ")
			End If
		End If
		
		'**+ Name validations
		'+ Se realizan las validaciones del campo "Nombre".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sCliename) And Not IsNothing(sCliename) And Trim(sCliename) <> String.Empty And Trim(sCliename) <> "0" Then
			If Not lclsUser.InsConstruct("Client.sCliename", sCliename, eSecurity.User.eTypValConst.ConstString) Then
				Call lobjErrors.ErrorMessage(sCodispl, 10222,  , eFunctions.Errors.TextAlign.LeftAling, "(Nombre) ")
			End If
		End If
		
		'**+ Date validations
		'+ Se realizan las validaciones del campo "Fecha".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sDate) And Not IsNothing(sDate) And Trim(sDate) <> String.Empty And Trim(sDate) <> "0" Then
			If Not lclsUser.InsConstruct("Finance_co.dEffecdate", sDate, eSecurity.User.eTypValConst.ConstDate) Then
				Call lobjErrors.ErrorMessage(sCodispl, 10222,  , eFunctions.Errors.TextAlign.LeftAling, "(Fecha de efecto) ")
			End If
		End If
		
		'**+ Contrat validations
		'+ Se realizan las validaciones del campo "Contrato".
		
		If nStat_contr <> 0 And nStat_contr <> eRemoteDB.Constants.intNull Then
			If Not lclsUser.InsConstruct("Finance_co.nStat_contr", CStr(nStat_contr), eSecurity.User.eTypValConst.ConstNumeric) Then
				Call lobjErrors.ErrorMessage(sCodispl, 10222,  , eFunctions.Errors.TextAlign.LeftAling, "(Estado) ")
			End If
		End If
		
		insValFIC006 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValFIC006_Err: 
		If Err.Number Then
			insValFIC006 = insValFIC006 & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'**%InsExecuteFI008. This method updates the database (as described in the functional specifications)
	'**%for the page "FI008"
	'%InsExecuteFI008: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "FI008"
	Function InsExecuteFI008(ByVal nContrat As Double, ByVal dEffecdate As Date, ByVal sWait_contr As String, ByVal nUsercode As Double) As Boolean
		Dim lrecinsExecutefi008 As eRemoteDB.Execute
		On Error GoTo insExecutefi008_Err
		
		lrecinsExecutefi008 = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insExecutefi008 al 04-08-2004 18:35:59
		'+
		With lrecinsExecutefi008
			.StoredProcedure = "insExecuteFI008"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWait_contr", sWait_contr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsExecuteFI008 = .Run(False)
		End With
		
insExecutefi008_Err: 
		If Err.Number Then
			InsExecuteFI008 = False
		End If
		'UPGRADE_NOTE: Object lrecinsExecutefi008 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsExecutefi008 = Nothing
		On Error GoTo 0
	End Function
	
	
	'%insPostFI015: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "FI015"
	Public Function insPostFI015(ByVal nContrat As Double, ByVal nFirstDraft As Integer, ByVal nLastDraft As Integer, ByVal nType As FinanceDraft.eTypeMove, ByVal nUsercode As Integer, ByVal nAgent As Integer, ByVal nCommission As Double, ByVal nAmount As Double) As Boolean
		Dim lclsFinanceDraft As eFinance.FinanceDraft
		
		lclsFinanceDraft = New eFinance.FinanceDraft
		
		insPostFI015 = True
		
		On Error GoTo insPostFI015_Err
		
		With lclsFinanceDraft
			insPostFI015 = .UpdDraft(nContrat, nFirstDraft, nLastDraft, nType, nUsercode, nAgent, nCommission, nAmount)
		End With
		
		'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceDraft = Nothing
		
insPostFI015_Err: 
		If Err.Number Then
			insPostFI015 = False
		End If
	End Function
	
	Public Function insValFI015_k(ByVal psCodispl As String, ByVal pnContrat As Double, ByVal pdEffecdate As Date, ByVal pnAgent As Integer, ByVal pnFirstDra As Integer, ByVal pnLastDra As Integer, ByVal pnInterest As Integer, ByVal pnCommAmo As Integer) As String
		
		Dim lclsFinanceDraft As eFinance.FinanceDraft
		Dim lclsAgent As Object
		Dim lclsErrors As eFunctions.Errors
		
		lclsFinanceDraft = New eFinance.FinanceDraft
		lclsErrors = New eFunctions.Errors
		lclsAgent = eRemoteDB.NetHelper.CreateClassInstance("eAgent.Agents")
		
		On Error GoTo insValFI015_K_Err
		
		'**+The "contrat" must be filled
		'+Verifica que el campo "Contrato" esté lleno
		Dim errorNumber As Integer
		If pnContrat <> eRemoteDB.Constants.intNull And pnContrat <> 0 Then
			
			'**+Verifies that the contrat is registered and and standing
			'+Verifica que el contrato este registrado y que esté vigente
			If Not Find_Contrat(pnContrat, True) Then
				Call lclsErrors.ErrorMessage(psCodispl, 21002)
			Else
				
				'**+This variable contains the error number to evaluate the contract status
				'+Variable que contendrá el número del error para evaluar el estado del contrato
				
				
				'**+Validates that the contract is standing
				'+Se valida que el contrato esté en Vigor
				
				Select Case nStat_contr
					Case Estat_contr.Eannul
						errorNumber = 21074
						
					Case Estat_contr.Epayment
						errorNumber = 21074
				End Select
				
				If errorNumber <> 0 Then
					Call lclsErrors.ErrorMessage(psCodispl, errorNumber)
				End If
			End If
		Else
			Call lclsErrors.ErrorMessage(psCodispl, 21062)
		End If
		
		'**+Verifies that the field "Effecdate" is filled and is valid
		'+Verifica que el campo "Fecha de efecto" esté lleno y que sea valido
		
		If pdEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(psCodispl, 21006)
		Else
			
			'**+The effective date must be greather than today
			'+ La fecha de efecto debe ser mayor a la fecha del día
			If pdEffecdate <= Today Then
				Call lclsErrors.ErrorMessage(psCodispl, 1964)
			End If
			
			'**+ The effective date must be greater or equal than the date of the first draft
			'+ La fecha de efecto debe ser mayor a la fecha del primer giro
			If pnFirstDra <> eRemoteDB.Constants.intNull And pnFirstDra <> 0 And pnContrat <> eRemoteDB.Constants.intNull And pnContrat <> 0 Then
				If lclsFinanceDraft.Find(pnContrat, pnFirstDra) Then
					If lclsFinanceDraft.dStat_date > pdEffecdate Then
						Call lclsErrors.ErrorMessage(psCodispl, 21085)
					End If
				End If
			End If
		End If
		
		'+ El encargado de cobro debe estar lleno
		If pnAgent = eRemoteDB.Constants.intNull Or pnAgent = 0 Then
			Call lclsErrors.ErrorMessage(psCodispl, 21079)
		Else
			
			'+ el encargado de cobro debe estar registrado en el sistema
			If Not lclsAgent.Find(pnAgent) Then
				Call lclsErrors.ErrorMessage(psCodispl, 21080)
			End If
		End If
		
		'+ El primer giro debe estar lleno
		If pnFirstDra = eRemoteDB.Constants.intNull Or pnFirstDra = 0 Then
			Call lclsErrors.ErrorMessage(psCodispl, 21063)
		Else
			If pnFirstDra <> eRemoteDB.Constants.intNull And pnFirstDra <> 0 And pnContrat <> eRemoteDB.Constants.intNull And pnContrat <> 0 Then
				
				'+ Debe estar registrado en el archivo de giros
				If lclsFinanceDraft.Find(pnContrat, pnFirstDra) Then
					
					'+ Debe estar pendiente de cobro
					If lclsFinanceDraft.nStat_draft <> FinanceDraft.eStat_Draft.esdOutStatnding Then
						Call lclsErrors.ErrorMessage(psCodispl, 21042)
					Else
						
						'+ No debe haber giros anteriores pendientes de cobro
						If lclsFinanceDraft.ValBeforeDraft(pnContrat, pnFirstDra, FinanceDraft.eStat_Draft.esdOutStatnding) Then
							Call lclsErrors.ErrorMessage(psCodispl, 21064)
						End If
					End If
				Else
					Call lclsErrors.ErrorMessage(psCodispl, 21041)
				End If
			End If
		End If
		
		'+ El último giro debe estar lleno
		If pnLastDra = eRemoteDB.Constants.intNull Or pnLastDra = 0 Then
			Call lclsErrors.ErrorMessage(psCodispl, 21063)
		Else
			If pnLastDra <> eRemoteDB.Constants.intNull And pnLastDra <> 0 And pnContrat <> eRemoteDB.Constants.intNull And pnContrat <> 0 Then
				
				'+ Debe estar registrado en el archivo de giros
				If lclsFinanceDraft.Find(pnContrat, pnLastDra) Then
					
					'+ Debe estar pendiente de cobro
					If lclsFinanceDraft.nStat_draft <> FinanceDraft.eStat_Draft.esdOutStatnding Then
						Call lclsErrors.ErrorMessage(psCodispl, 21042)
					Else
						
						'+ Debe ser mayor al primer giro
						If pnLastDra < pnFirstDra Then
							Call lclsErrors.ErrorMessage(psCodispl, 21076)
						Else
							If pnFirstDra <> eRemoteDB.Constants.intNull And pnFirstDra <> 0 Then
								If lclsFinanceDraft.ValIntervdraft(pnContrat, pnFirstDra, pnLastDra, FinanceDraft.eStat_Draft.esdOutStatnding) Then
									Call lclsErrors.ErrorMessage(psCodispl, 21077)
								End If
							End If
						End If
					End If
				Else
					Call lclsErrors.ErrorMessage(psCodispl, 21041)
				End If
			End If
		End If
		
		'+ El interes debe estar lleno
		If pnInterest = eRemoteDB.Constants.intNull Or pnInterest = 0 Then
			Call lclsErrors.ErrorMessage(psCodispl, 21117)
		End If
		
		'+ El importe de comisión debe estar lleno
		If pnCommAmo = eRemoteDB.Constants.intNull Or pnCommAmo = 0 Then
			Call lclsErrors.ErrorMessage(psCodispl, 21116)
		End If
		
		insValFI015_k = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceDraft = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsAgent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAgent = Nothing
		
insValFI015_K_Err: 
		If Err.Number Then
			insValFI015_k = Err.Description
		End If
	End Function
	
	'%InsPreCA017A: Este metodo se encarga de realizar el financiamiento
	Public Function InsPreCA017A(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal nUsercode As Integer, ByVal nReceipt As Integer, ByVal nContrat As Double, ByVal nDraft As Integer, ByVal nInitial As Double, ByVal nInterest As Double, ByVal dFirst_draf As Date) As Boolean
		Dim lrecInsPreCA017A As eRemoteDB.Execute
		Dim lblTransaction As Object
        Dim lstrPrem_first As String = ""

        On Error GoTo lrecInsPreCA017A_Err
		lrecInsPreCA017A = New eRemoteDB.Execute
		
		InsPreCA017A = False
		lblTransaction = False
		
		With lrecInsPreCA017A
			.StoredProcedure = "INSPOLICY_FINAN"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitial", nInitial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dFirst_draf", IIf(dFirst_draf = eRemoteDB.Constants.dtmNull, System.DBNull.Value, dFirst_draf), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Me.mlngErrorNum = .FieldToClass("nErrornum")
				Me.nContrat = .FieldToClass("nContrat")
				Me.nWay_Pay = .FieldToClass("nWay_Pay")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.nPremiumN = .FieldToClass("nPremiumN")
				Me.nPremiumP = .FieldToClass("nPremiumP")
				Me.nPremiumT = .FieldToClass("nPremiumT")
				Me.nQuota = .FieldToClass("nQuota")
				Me.nInterest = .FieldToClass("nInterest")
				Me.nInterest_ori = .FieldToClass("nInterest_ori")
				Me.nReceipt = .FieldToClass("nReceipt")
				Me.nInitial = .FieldToClass("nInitial")
				Me.nPayfreq = .FieldToClass("nPayfreq")
				Me.nQuotaPend = .FieldToClass("nQuotaPend")
				Me.nValquota = .FieldToClass("nValquota")
				Me.dFirst_draf = .FieldToClass("dFirst_draf")
				Me.nContrat_ref = .FieldToClass("nContrat_ref")
				lstrPrem_first = .FieldToClass("sPrem_first")
				
				InsPreCA017A = IIf(.FieldToClass("nOk") = 1, True, False)
				.RCloseRec()
			Else
				InsPreCA017A = False
			End If
			
			'+ Si es transacción de modificación
			If nTransaction = eCollection.Premium.PolTransac.clngPolicyAmendment Or nTransaction = eCollection.Premium.PolTransac.clngTempPolicyAmendment Or nTransaction = eCollection.Premium.PolTransac.clngCertifAmendment Or nTransaction = eCollection.Premium.PolTransac.clngTempCertifAmendment Or nTransaction = eCollection.Premium.PolTransac.clngPolicyQuotAmendent Or nTransaction = eCollection.Premium.PolTransac.clngCertifQuotAmendent Or nTransaction = eCollection.Premium.PolTransac.clngPolicyPropAmendent Or nTransaction = eCollection.Premium.PolTransac.clngCertifPropAmendent Or nTransaction = eCollection.Premium.PolTransac.clngQuotAmendConvertion Or nTransaction = eCollection.Premium.PolTransac.clngPropAmendConvertion Or nTransaction = eCollection.Premium.PolTransac.clngQuotPropAmendentConvertion Then
				'+ Si es endoso no permite modificación de monto inicial ni fecha inicial
				Me.bInitial_Dis = True
				Me.bFirst_Draf_Dis = True
				lblTransaction = True
			Else
				'+ Solo para emision, recuperación se puede modificar depósito de propuesta
				If nTransaction = eCollection.Premium.PolTransac.clngPolicyIssue Or nTransaction = eCollection.Premium.PolTransac.clngCertifIssue Or nTransaction = eCollection.Premium.PolTransac.clngRecuperation Or nTransaction = eCollection.Premium.PolTransac.clngPolicyProposal Or nTransaction = eCollection.Premium.PolTransac.clngCertifQuotation Or nTransaction = eCollection.Premium.PolTransac.clngPolicyQuotation   Or nTransaction = eCollection.Premium.PolTransac.clngProposalConvertion  Or nTransaction = eCollection.Premium.PolTransac.clngPropQuotConvertion Or nTransaction = eCollection.Premium.PolTransac.clngQuotationConvertion   Then
					Me.bInitial_Dis = False
					Me.bInterest_Dis = False
					Me.bFirst_Draf_Dis = lstrPrem_first <> "1"
				Else
					Me.bInitial_Dis = True
					Me.bInterest_Dis = True
					Me.bFirst_Draf_Dis = True
				End If
				Me.bQuota_Dis = True
			End If
		End With
		
lrecInsPreCA017A_Err: 
		If Err.Number Then
			InsPreCA017A = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecInsPreCA017A may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPreCA017A = Nothing
	End Function
End Class







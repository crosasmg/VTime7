Option Strict Off
Option Explicit On
Public Class RefinanceDraft
	'%-------------------------------------------------------%'
	'% $Workfile:: RefinanceDraft.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 2/06/04 1:16p                                $%'
	'% $Revision:: 25                                       $%'
	'%-------------------------------------------------------%'
	'+ Propiedades según la tabla en el sistema el 10/08/1999.
	'+ Los campos llaves corresponden a nContrat nContrat_d y nDraft_d.
	
	'+  Column name               Type                            Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+  ------------------------- ------------------------------- ------ ----- ----- -------- ------------------ ---------------------
	Public nBranch As Integer 'smallint    2      5     0     yes      (n/a)              (n/a)
	Public nCommission As Double 'decimal     6      10    2     yes      (n/a)              (n/a)
	Public dCompdate As Date 'datetime    8                  yes      (n/a)              (n/a)
	Public nContrat As Double 'int         4      10    0     no       (n/a)              (n/a)
	Public nContrat_d As Double 'int         4      10    0     no       (n/a)              (n/a)
	Public nCurrency As Integer 'smallint    2      5     0     yes      (n/a)              (n/a)
	Public nDraft_d As Integer 'smallint    2      5     0     no       (n/a)              (n/a)
	Public dStartdate As Date 'datetime    8                  yes      (n/a)              (n/a)
	Public nExchange As Double 'decimal     6      10    6     yes      (n/a)              (n/a)
	Public dExpirdat As Date 'datetime    8                  yes      (n/a)              (n/a)
	Public nPremium As Double 'decimal     6      10    2     yes      (n/a)              (n/a)
	Public sStat_finpr As String 'char        1                  yes      yes                yes
	Public sStatregt As String 'char        1                  yes      yes                yes
	Public sClient As String 'char        14                 yes      yes                yes
	Public nUsercode As Integer 'smallint    2      5     0     yes      (n/a)              (n/a)
	Public nOpt_draft As Integer 'smallint    2      5     0     yes      (n/a)              (n/a)
	
	'- Auxiliary Properties
	'- Propiedades auxiliares
	Public sCliename As String
	Public sCurrency As String
	Public nDraftValue As Integer
	Public sDigit As String
	
	Public nStatInstanc As FinanceDraft.eStatusInstance
	
	'**Draft Status
	'- Estado del giro
	Public nStatus_pre As FinanceDraft.eStat_Draft
	
	'**The recorset that will be used in the class is defined
	'- Se define el recordset que será utilizado en la clase
	Private lrecReFinan_dra As eRemoteDB.Execute
	
	
	Private mvarRefinanceDrafts As RefinanceDrafts
	
	'**The recorset that will be used in the class is defined
	'- Se define el recordset que será utilizado en la clase
	Public lclsRefinanceDraft As eFinance.RefinanceDraft
	
	'**The recorset that will contain all teh data in the collection is defined
	'- Se define el recordset que contendra la informacion de la colección
	'Public lcolRefinanceDraft As eFinance.RefinanceDrafts
	
	'The variable for the finance general information is declared
	'- Se declara la variable para los datos generales de financiamiento
	Public lclsFinanceCO As eFinance.financeCO
	
	'The variable for the  contract  draft that is going to be refinanced is declared
	'- Se declara la variable para los giros del contrato que se va a refinanciar
	
	
	Public bExist As Boolean
	Public dEffecdate As Date
	
	'The variable tbat contains the column sel values is defined
	'- Se define la variable que contiene los valores de la columna Sel
	
	Public Enum eSel
		eftIgnore = 0
		eftModify = 1
		eftDel = 2
	End Enum
	
	'- The variable that contains the corresponding value to the total amount is defined
	'- Se define la variable que contiene el valor correspondiente al total del importe
	Public nTotamount As Double
	
	'- The variable that contains the corresponding value to the total amount is defined
	'- Se define la variable que contiene el valor correspondiente al total del importe
	Public nTotCommission As Double
	
	'%insPreLoadFI003 : realiza la generación de datos para la transaccion
	Public Function insPreLoadFI003(ByVal nContrat As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nRegen As Integer) As Boolean
		Dim lrecinsPreloadFI003 As eRemoteDB.Execute
		On Error GoTo insPreloadFI003_Err
		
		lrecinsPreloadFI003 = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insPreloadFI003 al 04-08-2004 16:41:42
		'+
		With lrecinsPreloadFI003
			.StoredProcedure = "insPreloadFI003"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRegen", nRegen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPreLoadFI003 = .Run(False)
		End With
		
insPreloadFI003_Err: 
		If Err.Number Then
			insPreLoadFI003 = False
		End If
		'UPGRADE_NOTE: Object lrecinsPreloadFI003 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPreloadFI003 = Nothing
		On Error GoTo 0
	End Function
	
	
	'**%ChangeStatus: This function is in charge of marking the refinanced drafts in pending for collection status
	'% ChangeStatus: Esta funci¢n se encarga de marcar los giros refinanciados
	'%               en estado pendientes de cobro.
	Public Function ChangeStatus() As Boolean
		ChangeStatus = False
		
		Dim lrecinsChangeStatusRefinanDraft As eRemoteDB.Execute
		
		lrecinsChangeStatusRefinanDraft = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insChangeStatusRefinanDraft'
		'Información leída el 30/09/1999 04:53:14 PM
		
		With lrecinsChangeStatusRefinanDraft
			.StoredProcedure = "insChangeStatusRefinanDraft"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStat_Draft", nStatus_pre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOpe_date", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			ChangeStatus = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsChangeStatusRefinanDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsChangeStatusRefinanDraft = Nothing
		
	End Function
	
	'**%AsignValPoPup: Assigns the corresponding values to the popup window once entered the contract to refinance and the draft number.
	'%AsignValPoPup: Asigna los valores correspondientes a la ventana PoPup una vez
	' Introducido el contrato a refinanciar y el número de giro
	Public Function AsignValPoPup(ByVal nContrat As Double, ByVal nContrat_d As Double, ByVal nDraft_d As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsFinanDraft As FinanceDraft
		Dim lclsFinanceCO As financeCO
		Dim lclsExchange As eGeneral.Exchange
		Dim lclsFinanPre As FinancePre
		Dim nCurrencyOld As Integer
		Dim nCommissionOld As Double
		Dim nAmount As Double
		Dim nAmount_d As Double
		
		'+Se busca contrato a refinanciar (antiguo)
		lclsFinanceCO = New financeCO
		If lclsFinanceCO.Find(nContrat_d, dEffecdate) Then
			
			With lclsFinanceCO
				Me.sCurrency = .sCurrency
				Me.sCliename = .sClientName
				Me.nCurrency = .nCurrency
				nCurrencyOld = .nCurrency
				Me.sClient = .sClient
				Me.sDigit = .sDigit
				nCommissionOld = .nCommision
			End With
			
			'+Se busca cuota a refinanciar (antiguo)
			lclsFinanDraft = New FinanceDraft
			If lclsFinanDraft.Find(nContrat_d, nDraft_d) Then
				With lclsFinanDraft
					Me.dExpirdat = .dLimitdate
					Me.nCommission = .nCommission
					nAmount = .nAmount
					Me.nPremium = nAmount
				End With
			End If
			'UPGRADE_NOTE: Object lclsFinanDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsFinanDraft = Nothing
			
			lclsFinanPre = New FinancePre
			If lclsFinanPre.Find(nContrat_d, 0) Then
				Me.nBranch = lclsFinanPre.nBranch
			End If
			'UPGRADE_NOTE: Object lclsFinanPre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsFinanPre = Nothing
			
			'+Se busca el nuevo contrato de financiamiento
			If lclsFinanceCO.Find(nContrat, dEffecdate) Then
				
				'+Si se encuentran los contratos antiguos y nuevos hay datos
				AsignValPoPup = True
				
				'+Si la moneda del contrato antiguo difiere del actual se unifican los montos
				If lclsFinanceCO.nCurrency <> nCurrencyOld Then
					lclsExchange = New eGeneral.Exchange
					Call lclsExchange.Convert(eRemoteDB.Constants.intNull, nAmount, nCurrencyOld, lclsFinanceCO.nCurrency, lclsFinanceCO.dEffecdate, 0)
					Me.nExchange = lclsExchange.pdblExchange
					Me.nPremium = lclsExchange.pdblResult
					
					nAmount_d = lclsExchange.pdblResult
					
					Call lclsExchange.Convert(eRemoteDB.Constants.intNull, nCommission, nCurrencyOld, lclsFinanceCO.nCurrency, lclsFinanceCO.dEffecdate, 0)
					nCommission = lclsExchange.pdblResult
					'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsExchange = Nothing
				Else
					Me.nExchange = 1
					nAmount_d = Me.nPremium
					nAmount = Me.nPremium
					nCommission = nCommission
				End If
			End If
		End If
		
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		
	End Function
	
	'% insValFI003: se realizan las validaciones de los campos del grid
	Public Function insValFI003(ByVal nContrat As Double, ByVal nContrat_d As Double, ByVal dEffecdate As Date, ByVal nDraft_d As Integer, ByVal nInd As Integer, ByVal nOpt_draft As Integer, ByVal nExchange As Double, ByVal sAction As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsFinanceC0 As eFinance.financeCO
		Dim lclsFinanceDraft As eFinance.FinanceDraft
		Dim lclsFinDraft As eFinance.FinanceDraft
		Dim nCurrency As Integer
		
		On Error GoTo insValFI003_Err
		
		lclsErrors = New eFunctions.Errors
		lclsFinanceDraft = New eFinance.FinanceDraft
		
		If nContrat_d = 0 Or nContrat_d = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("FI003", 21062)
		Else
			lclsFinanceC0 = New eFinance.financeCO
			
			If lclsFinanceC0.Find(nContrat_d, dEffecdate) Then
				nCurrency = lclsFinanceC0.nCurrency
				
				If lclsFinanceC0.nStat_contr = financeCO.Estat_contr.Eannul Then
					Call lclsErrors.ErrorMessage("FI003", 21005)
				End If
				'+ No debe estar en Captura incompleta
				If lclsFinanceC0.nStat_contr = financeCO.Estat_contr.Eincompletecapture Then
					Call lclsErrors.ErrorMessage("FI003", 56045)
				End If
				
				lclsFinDraft = New eFinance.FinanceDraft
				'+ La cuota inicial debe estar cobrada
				If lclsFinDraft.Find(nContrat_d, 1) Then
					If lclsFinDraft.nStat_draft <> 2 Then
						Call lclsErrors.ErrorMessage("FI003", 21155)
					End If
				End If
				
				'UPGRADE_NOTE: Object lclsFinDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsFinDraft = Nothing
			Else
				Call lclsErrors.ErrorMessage("FI003", 21002)
			End If
			
			'UPGRADE_NOTE: Object lclsFinanceC0 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsFinanceC0 = Nothing
		End If
		
		'+ Se ejecuta la lectura correspondiente a las tablas FinanceDraft para realizar las validaciones
		If lclsFinanceDraft.Find(nContrat_d, nDraft_d) Then
			
			'+ El giro no puede ser 1, este registro posee la cuota inicial
			If nDraft_d = 1 Then
				Call lclsErrors.ErrorMessage("FI003", 21149)
			Else
				
				'+ Debe estar pendiente de cobro
				If lclsFinanceDraft.nStat_draft <> 1 Then
					Call lclsErrors.ErrorMessage("FI003", 21042)
				End If
				
				'+ No se puede refinanciar dos veces la misma cuota dentro del contrato
				If Find(nContrat_d, nDraft_d) And sAction = "Add" Then
					If Me.nContrat = nContrat Then
						Call lclsErrors.ErrorMessage("FI003", 21043)
					Else
						
						'+ Cuota refinanciada en otro contrato
						Call lclsErrors.ErrorMessage("FI003", 21102)
					End If
				End If
			End If
		Else
			Call lclsErrors.ErrorMessage("FI003", 21041)
		End If
		
		'+ No puede estar vacío el campo Valor de la cuota
		If nOpt_draft = 0 Or nOpt_draft = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("FI003", 21131)
		End If
		
		'+ Si la moneda no corresponde a la moneda del contrato
		If lclsFinanceDraft.nCurrency <> nCurrency Then
			
			'+ No puede estar vacío
			If nExchange = 0 Or nExchange = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage("FI003", 21031)
			End If
		End If
		
		insValFI003 = lclsErrors.Confirm
		
insValFI003_Err: 
		If Err.Number Then
			insValFI003 = insValFI003 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceDraft = Nothing
	End Function
	
	'**%insPostFI003: This method updates the database (as described in the functional specifications)
	'**%for the page "FI003"
	'%insPostFI003: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "FI003"
	Public Function insPostFI003(ByVal nInd As Integer, ByVal nSel As Integer, ByVal sClient As String, ByVal nDraft_d As Integer, ByVal nPremium As Double, ByVal dExpirdat As Date, ByVal nExchange As Double, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nContrat As Double, ByVal nContrat_d As Double, ByVal nCommission As Double, ByVal nOpt_draft As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lcolRefinanceDraft As eFinance.RefinanceDrafts
		Dim lcolFinanceDrafts As eFinance.FinanceDrafts
		Dim lclsFinanceDraft As eFinance.FinanceDraft
		Dim lclsFinanceWin As eFinance.FinanceWin
		Dim lstrContent As String
		Dim ldblFactor As Double
		
		lclsFinanceWin = New eFinance.FinanceWin
		
		Me.dEffecdate = dEffecdate
		If nInd = FinanceDraft.eStatusInstance.eftNew And nSel = eSel.eftIgnore Then
			
			'+ Estado del recibo: "1" Por financiar o "2" Financiado
			sStat_finpr = "1"
			
			'+ Estado general del registro : Activo (ver tabla con identificativo 26)
			sStatregt = "1"
			lclsRefinanceDraft = New RefinanceDraft
			lclsRefinanceDraft.nStatInstanc = FinanceDraft.eStatusInstance.eftNew
			lclsRefinanceDraft.sCurrency = sCurrency
			lclsRefinanceDraft.sCliename = sCliename
			lclsRefinanceDraft.sClient = sClient
			lclsRefinanceDraft.sStatregt = sStatregt
			lclsRefinanceDraft.sStat_finpr = sStat_finpr
			lclsRefinanceDraft.nDraft_d = nDraft_d
			lclsRefinanceDraft.nPremium = nPremium
			lclsRefinanceDraft.dExpirdat = dExpirdat
			lclsRefinanceDraft.nExchange = nExchange
			lclsRefinanceDraft.dStartdate = dStartdate
			lclsRefinanceDraft.nCurrency = nCurrency
			lclsRefinanceDraft.nContrat = nContrat
			lclsRefinanceDraft.nContrat_d = nContrat_d
			lclsRefinanceDraft.nCommission = nCommission
			lclsRefinanceDraft.nBranch = IIf(nBranch = 0, eRemoteDB.Constants.intNull, nBranch)
			lclsRefinanceDraft.nOpt_draft = nOpt_draft
			lclsRefinanceDraft.nTotamount = nTotamount
			lclsRefinanceDraft.nTotCommission = nTotCommission
			lclsRefinanceDraft.nUsercode = nUsercode
			lclsRefinanceDraft.dStartdate = Today
			
			lcolRefinanceDraft = New eFinance.RefinanceDrafts
			Call lcolRefinanceDraft.Add(lclsRefinanceDraft)
			'UPGRADE_NOTE: Object lcolRefinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lcolRefinanceDraft = Nothing
			
			insPostFI003 = lclsRefinanceDraft.Add
			
			If insPostFI003 Then
				Call lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, "FI003", CStr(2), nUsercode, 1)
			End If
			
			'+ Si no hay mas giros pendientes entonces el contrato pasa estado pagado
			'        bExist = lclsFinanceDraft.CountDraft(nContrat, eStat_Draft.esdOutStatnding)
			'
			'        Call lclsFinanceCO.Upd(nContrat, nContrat_d, dEffecdate, _
			''                               dExpirdat,A nPremium, nExchange, _
			''                               nCommission, nSel, bExist)
			
		ElseIf nInd = FinanceDraft.eStatusInstance.eftExist And nSel = eSel.eftModify Then 
			insPostFI003 = UpdDraft(nSel, sClient, nDraft_d, nPremium, dExpirdat, nExchange, nContrat, nContrat_d, nCommission, nOpt_draft, nUsercode)
			
		ElseIf nInd = FinanceDraft.eStatusInstance.eftExist And nSel = eSel.eftDel Then 
			lclsRefinanceDraft = New eFinance.RefinanceDraft
			lclsRefinanceDraft.nContrat = nContrat
			lclsRefinanceDraft.nContrat_d = nContrat_d
			lclsRefinanceDraft.nDraft_d = nDraft_d
			insPostFI003 = lclsRefinanceDraft.Delete
			'UPGRADE_NOTE: Object lclsRefinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsRefinanceDraft = Nothing
			
			lcolRefinanceDraft = New eFinance.RefinanceDrafts
			If Not lcolRefinanceDraft.Find(nContrat, True, True) Then
				Call lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, "FI003", "1", nUsercode, 4)
			End If
			'UPGRADE_NOTE: Object lcolRefinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lcolRefinanceDraft = Nothing
			
			'+ Se cambia el estado del viejo giro a pendiente de cobro nuevamente
			lclsFinanceDraft = New eFinance.FinanceDraft
			Call lclsFinanceDraft.UpdnStat_draft(nContrat_d, nDraft_d, FinanceDraft.eStat_Draft.esdOutStatnding, nUsercode)
			'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsFinanceDraft = Nothing
			
			'+ El contrato pasa a estado pendiente??? solo recalcula monto
			'            Set lclsFinanceCO = New eFinance.financeCO
			'            Call lclsFinanceCO.Upd(nContrat, nContrat_d, dEffecdate, dExpirdat, -nPremium, nExchange, nCommission, nSel)
			'            Set lclsFinanceCO = Nothing
		Else
			
			If nPremium > 0 Then
				lclsFinanceCO = New financeCO
				With lclsFinanceCO
					If .Find(nContrat, dEffecdate) Then
						'+Se aplica porcentaje de descuento por pronto pago
						If .nDscto_pag > 0 Then
							nPremium = nPremium - (nPremium * .nDscto_pag / 100)
						End If
						
						'+Se aplica factor asociado a porcentaje de interés
						lcolFinanceDrafts = New FinanceDrafts
						ldblFactor = lcolFinanceDrafts.SearchFactor(.nQ_draft, .nInterest, dEffecdate)
						'UPGRADE_NOTE: Object lcolFinanceDrafts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lcolFinanceDrafts = Nothing
						If ldblFactor > 0 Then
							.nAmount = nPremium * ldblFactor * .nQ_draft
						Else
							ldblFactor = 1 / .nQ_draft
							.nAmount = nPremium * ldblFactor * .nQ_draft
						End If
						
						.nAmount_d = .nAmount
						.nUsercode = nUsercode
						.nCommision = nCommission
						If .UpDate Then
							insPostFI003 = True
						End If
					End If
					
				End With
				'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsFinanceCO = Nothing
				
			Else
				insPostFI003 = True
			End If
			
			'+Como se actualizó información del contrato,
			'+se eliminan las cuotas que se están creando/modificando
			lclsFinanceDraft = New eFinance.FinanceDraft
			Call lclsFinanceDraft.Delete_All(nContrat)
			'UPGRADE_NOTE: Object lclsFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsFinanceDraft = Nothing
			
			'+Si es finalizar la transaccion, se actualiza estado de secuencia
			lcolRefinanceDraft = New eFinance.RefinanceDrafts
			If lcolRefinanceDraft.Find(nContrat, True, True) Then
				lstrContent = "2"
			Else
				lstrContent = "1"
			End If
			'UPGRADE_NOTE: Object lcolRefinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lcolRefinanceDraft = Nothing
			Call lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, "FI003", lstrContent, nUsercode, financeCO.eFinanceTransac.eftAddContrat)
			
			'+Como se eliminó información de cuotas, se deja sin contenido
			Call lclsFinanceWin.Add_Finan_win(nContrat, dEffecdate, "FI004", "1", nUsercode, financeCO.eFinanceTransac.eftAddContrat)
			
			insPostFI003 = True
			
		End If
		
		
		'UPGRADE_NOTE: Object lclsRefinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRefinanceDraft = Nothing
		'UPGRADE_NOTE: Object lcolRefinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolRefinanceDraft = Nothing
		'UPGRADE_NOTE: Object lclsFinanceWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceWin = Nothing
	End Function
	
	'**% UpdDraft: Updates the information of the refinanced drafts of the contract (FI003)
	'% UpDDraft: Actualiza la información de los giros refinanciado de
	'%                     un contrato (FI003).
	Public Function UpdDraft(ByVal nSel As Integer, ByVal sClient As String, ByVal nDraft_d As Integer, ByVal nPremium As Double, ByVal dExpirdat As Date, ByVal nExchange As Double, ByVal nContrat As Double, ByVal nContrat_d As Double, ByVal nCommission As Double, ByVal nOpt_draft As Integer, ByVal nUsercode As Integer) As Boolean
		If lclsRefinanceDraft.Find(nContrat_d, nDraft_d, True) Then
			With lclsRefinanceDraft
				.nContrat_d = nContrat_d
				.nDraft_d = nDraft_d
				.dExpirdat = dExpirdat
				.nOpt_draft = nOpt_draft
				.nPremium = nPremium
				.nCommission = nCommission
				.sClient = sClient
				.nContrat = nContrat
				.nUsercode = nUsercode
				UpdDraft = .UpDate
			End With
		End If
		
		If UpdDraft Then
			Call lclsFinanceCO.Upd(nContrat, nContrat_d, dEffecdate, dExpirdat, nPremium, nExchange, nCommission, nSel)
		End If
	End Function
	
	
	'**%ADD: This method is in charge of adding new records to the table "ReFinan_dra".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "ReFinan_dra". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		lrecReFinan_dra = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.creReFinan_dra'
		'Información leída el 15/09/1999 08:12:39 AM
		
		On Error GoTo lrecReFinan_dra_err
		
		With lrecReFinan_dra
			.StoredProcedure = "creReFinan_dra"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat_d", nContrat_d, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft_d", nDraft_d, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommission", nCommission, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 11, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStat_finpr", sStat_finpr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOpt_draft", nOpt_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = IIf(.Run(False), True, False)
		End With
		
lrecReFinan_dra_err: 
		If Err.Number Then
			Add = False
		End If
		
		'UPGRADE_NOTE: Object lrecReFinan_dra may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReFinan_dra = Nothing
	End Function
	'**%Delete: This method is in charge of Deleting records in the table "ReFinan_dra".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Delete: Este método se encarga de eliminar registros en la tabla "ReFinan_dra". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		
		On Error GoTo Delete_err
		
		lrecReFinan_dra = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.delReFinan_dra'
		'Información leída el 20/09/1999 12:02:41 PM
		
		With lrecReFinan_dra
			.StoredProcedure = "delReFinan_dra"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat_d", nContrat_d, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft_d", nDraft_d, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecReFinan_dra may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReFinan_dra = Nothing
	End Function
	
	'**%Update: This method is in charge of updating records in the table "ReFinan_dra".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "ReFinan_dra". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function UpDate() As Boolean
		On Error GoTo Update_Err
		
		lrecReFinan_dra = New eRemoteDB.Execute
		
		' Parameters definition for the stored procedure 'insudb.updReFinan_dra'
		'Definición de parámetros para stored procedure 'insudb.updReFinan_dra'
		'Información leída el 15/09/1999 08:26:19 AM
		
		With lrecReFinan_dra
			.StoredProcedure = "updReFinan_dra"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat_d", nContrat_d, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft_d", nDraft_d, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommission", nCommission, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 11, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStat_finpr", sStat_finpr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOpt_draft", nOpt_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpDate = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			UpDate = False
		End If
		'UPGRADE_NOTE: Object lrecReFinan_dra may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReFinan_dra = Nothing
	End Function
	
	
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table "ReFinan_dra"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "ReFinan_dra"
	Public Function Find(ByVal Contrat_d As Double, ByVal Draft_d As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		On Error GoTo Find_Err
		
		If Contrat_d <> nContrat_d Or Draft_d <> nDraft_d Or lblnFind Then
			
			lrecReFinan_dra = New eRemoteDB.Execute
			
			'Parameters definition for the stored procedure 'insudb.reaReFinan_dra'
			'Definición de parámetros para stored procedure 'insudb.reaReFinan_dra'
			'Información leída el 15/09/1999 08:16:41 AM
			
			With lrecReFinan_dra
				.StoredProcedure = "reaReFinan_dra"
				.Parameters.Add("nContrat_d", Contrat_d, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDraft_d", Draft_d, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nContrat = .FieldToClass("nContrat")
					nBranch = .FieldToClass("nBranch")
					nCommission = .FieldToClass("nCommission")
					nContrat_d = Contrat_d
					nCurrency = .FieldToClass("nCurrency")
					nDraft_d = nDraft_d
					dStartdate = .FieldToClass("dStartdate")
					nExchange = .FieldToClass("nExchange")
					dExpirdat = .FieldToClass("dExpirdat")
					nPremium = .FieldToClass("nPremium")
					sStat_finpr = .FieldToClass("sStat_finpr")
					sStatregt = .FieldToClass("sStatregt")
					sClient = .FieldToClass("sClient")
					sCliename = .FieldToClass("sCliename")
					sCurrency = .FieldToClass("sDescript")
					nOpt_draft = .FieldToClass("nOpt_draft")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecReFinan_dra may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReFinan_dra = Nothing
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
	End Function
	
	'**% This property returns the content of the object mvarRefinanceDrafts
	'*Esta propiedad devuelve el contenido del objeto mvarRefinanceDrafts
	
	'**% This property updates the content of the object mvarRefinanceDrafts
	'* Esta propiedad actualiza el contenido del objeto mvarRefinanceDrafts
	Public Property RefinanceDrafts() As RefinanceDrafts
		Get
			If mvarRefinanceDrafts Is Nothing Then
				mvarRefinanceDrafts = New RefinanceDrafts
			End If
			
			
			RefinanceDrafts = mvarRefinanceDrafts
		End Get
		Set(ByVal Value As RefinanceDrafts)
			mvarRefinanceDrafts = Value
		End Set
	End Property
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarRefinanceDrafts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarRefinanceDrafts = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class







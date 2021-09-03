Option Strict Off
Option Explicit On
Public Class Premiums
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Premiums.cls                             $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 16/02/04 7:14p                               $%'
	'% $Revision:: 48                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Auxiliary properties
	'-Propiedades auxiliares
	Private mintOffice As Integer
	Private mintTypOper As Integer
	
	Private mlngInsur_area As Integer
	Private mdtmProcess As Date
	
	Private mstrCertype As String
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mdblPolicy As Double
	Private mdblCertif As Double
	Private mdblReceipt As Double
	Private mdblContrat As Double
	Private mintDraft As Integer
	
	Private mdtmCollSus_ini As Date
	Private mdtmCollSus_end As Date
	'**-Local variable to hold collection
	'-Variable local para almacenar la coleccion
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mdblProponum As Double
	Private mintInd_PolPro As Integer
	
	Private mintAgency As Integer
	Private mstrColltype As String
	Private mintAction As Integer
	Private mlngCollector As Double
	
	Public mlngCountCOC679 As Integer
	Public mstrKey As String
	
	'**%Add: Adds a new class instance to the collection
	'%Add: se añade una nueva instancia de la clase a la colección
	Public Function Add(ByVal sCertype As String, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, ByVal sClient As String, ByVal sCessions As String, ByVal sDirdebit As String, ByVal sLeadinvo As String, ByVal sManauti As String, ByVal sRenewal As String, ByVal sStatusva As String, ByVal sSubstiti As String, ByVal sConColl As String, ByVal dEffecdate As Date, ByVal dExpirDat As Date, ByVal dIssuedat As Date, ByVal dNulldate As Date, ByVal dPayDate As Date, ByVal dStatdate As Date, ByVal nBalance As Double, ByVal nComamou As Double, ByVal nExchange As Double, ByVal nIntammou As Double, ByVal nParticip As Double, ByVal nPremium As Double, ByVal nPremiuml As Double, ByVal nPremiumn As Double, ByVal nPremiums As Double, ByVal nRate As Double, ByVal nTaxamou As Double, ByVal nCollecto As Double, ByVal nContrat As Double, ByVal nInspecto As Integer, ByVal nIntermed As Double, ByVal nPolicy As Double, ByVal nSustit As Integer, ByVal nTransactio As Integer, ByVal nStatus_pre As Premium.StatusReceipt, ByVal nNullCode As Integer, ByVal nCurrency As Integer, ByVal nNoteNum As Double, ByVal nOffice As Integer, ByVal nType As Premium.Collec_Devolu, ByVal nBranch As Integer, ByVal nTratypei As Integer, ByVal nProduct As Integer, ByVal nPeriod As Integer, ByVal nCompany As Integer, ByVal sOrigReceipt As String) As Premium
		Dim objNewMember As Premium
		
		objNewMember = New Premium
		
		With objNewMember
			.sCertype = sCertype
			.nReceipt = nReceipt
			.nDigit = nDigit
			.nPaynumbe = nPaynumbe
			.sClient = sClient
			.sCessions = sCessions
			.sDirdebit = sDirdebit
			.sLeadinvo = sLeadinvo
			.sManauti = sManauti
			.sRenewal = sRenewal
			.sStatusva = sStatusva
			.sSubstiti = sSubstiti
			.sConColl = sConColl
			.dEffecdate = dEffecdate
			.dExpirDat = dExpirDat
			.dIssuedat = dIssuedat
			.dNulldate = dNulldate
			.dPayDate = dPayDate
			.dStatdate = dStatdate
			.nBalance = nBalance
			.nComamou = nComamou
			.nExchange = nExchange
			.nIntammou = nIntammou
			.nParticip = nParticip
			.nPremium = nPremium
			.nPremiuml = nPremiuml
			.nPremiumn = nPremiumn
			.nPremiums = nPremiums
			.nRate = nRate
			.nTaxamou = nTaxamou
			.nCollecto = nCollecto
			.nContrat = nContrat
			.nInspecto = nInspecto
			.nIntermed = nIntermed
			.nPolicy = nPolicy
			.nSustit = nSustit
			.nTransactio = nTransactio
			.nStatus_pre = nStatus_pre
			.nNullCode = nNullCode
			.nCurrency = nCurrency
			.nNoteNum = nNoteNum
			.nOffice = nOffice
			.nType = nType
			.nBranch = nBranch
			.nTratypei = nTratypei
			.nProduct = nProduct
			.nPeriod = nPeriod
			.nCompany = nCompany
			.sOrigReceipt = sOrigReceipt
		End With
		
		mCol.Add(objNewMember, "2" & CStr(nBranch) & CStr(nProduct) & CStr(nReceipt) & "00") '+ 00 corresponde a nDigit y nPaynumbe
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**%Add: Adds a new class instance to the collection
	'%Add: se añade una nueva instancia de la clase a la colección
	Public Function Add_COC679(ByVal objClass As Premium) As Premium
		mCol.Add(objClass, objClass.mlngRows & objClass.nBranch & objClass.nProduct & objClass.nPolicy & objClass.nCertif & objClass.nReceipt & objClass.nDraft)
		
		'return the object created
		Add_COC679 = objClass
	End Function
	
	'**%Add: Adds a new class instance to the collection
	'%Add: se añade una nueva instancia de la clase a la colección
	Public Function Add_COC747(ByVal objClass As Premium) As Premium
		With objClass
			mCol.Add(objClass, "CO" & .nId_NumPay & .nInsur_area & .nBranch & .nProduct & .nPolicy & .nCertif & .nReceipt & .nContrat & .nDraft)
		End With
		
		'return the object created
		Add_COC747 = objClass
	End Function
	
	'%Add: se añade una nueva instancia de la clase a la colección
	Public Function Add_TMP_COL502(ByVal objClass As Premium) As Premium
		With objClass
			mCol.Add(objClass, "CO" & .nId_Register & .nBank_Agree & .sAcc_Number & .sDep_Number & .nAmount & .nAcc_Bank & .dEffecdate & .nMovement & .nBank_code & .nCurrency & .nAccount & .nCommission)
			
		End With
		
		'return the object created
		Add_TMP_COL502 = objClass
	End Function
	
	'**%Add_CO633: Adds a new class instance to the collection
	'%Add_CO633: se añade una nueva instancia de la clase a la colección
	Public Function Add_CO633(ByVal objClass As Premium) As Premium
		With objClass
            mCol.Add(objClass, "CO" & .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nReceipt & .nContrat & .nDraft & .nCount)
		End With
		
		'return the object created
		Add_CO633 = objClass
	End Function
	
	'**% Add_CAC003: adds a new element to the collection used in the CAC003.
	'% Add_CAC003: añade un nuevo elemento a la colección usada en la CAC003.
	Public Function Add_CAC003(ByVal sCertype As String, ByVal nBranch As Integer, ByVal sDescBranch As String, ByVal nProduct As Integer, ByVal sDescProduct As String, ByVal nPolicy As Double, ByVal nReceipt As Double, ByVal nCertif As Double, ByVal nOffice As Integer, ByVal sDescOffice As String, ByVal sClient As String, ByVal sCliename As String) As Premium
		'+ Create a new object
		Dim objNewMember As eCollection.Premium
		
		objNewMember = New eCollection.Premium
		
		'+ Set the properties passed into the method
		With objNewMember
			.sCertype = sCertype
			.nBranch = nBranch
			.sDesBranch = sDescBranch
			.nProduct = nProduct
			.sDesProduct = sDescProduct
			.nReceipt = nReceipt
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nOffice = nOffice
			.sDesOffice = sDescOffice
			.sClient = sClient
			.sCliename = sCliename
		End With
		
		'** Key of the table: sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt
		'+ Llave de la tabla: sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt
		mCol.Add(objNewMember, "A" & sCertype & nBranch & nProduct & nPolicy & nCertif & nReceipt)
		
		'+ Return the object created
		Add_CAC003 = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function

    '% Add_COC002: añade un nuevo elemento a la colección usada en C0C002.
    '* nReceipt: cambio de tipo de dato integer a long - jehh 03092021
    Public Function Add_COC002(ByVal nReceipt As Long, ByVal nBulletins As Double, ByVal nCollecto As Double, ByVal dEffecdate As Date, ByVal dExpirDat As Date, ByVal nCurrency As Integer, ByVal nPremium As Double, ByVal nPremiumn As Double, ByVal nStatus_pre As Integer, ByVal nContrat As Double, ByVal nAmount As Double, ByVal nStat_draft As Integer, ByVal nDraft As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Premium
        '+ Create a new object
        Dim objNewMember As eCollection.Premium

        objNewMember = New eCollection.Premium

        '+ Set the properties passed into the method
        With objNewMember
            .nReceipt = nReceipt
            .nBulletins = nBulletins
            .nCollecto = nCollecto
            .dEffecdate = dEffecdate
            .dExpirDat = dExpirDat
            .nCurrency = nCurrency
            .nPremium = nPremium
            .nPremiumn = nPremiumn
            .nStatus_pre = nStatus_pre
            .nContrat = nContrat
            .nAmount = nAmount
            .nStat_draft = nStat_draft
            .nDraft = nDraft
        End With

        '** Key of the table: sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt, nContrat, nDraft
        '+ Llave de la tabla: sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt, nContrat, nDraft
        mCol.Add(objNewMember, "CO" & "2" & nBranch & nProduct & nPolicy & 0 & nReceipt & nContrat & nDraft)

        '+ Return the object created
        Add_COC002 = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

    '%Find_Receipt_Pol: Permite leer los recibos y financiamientos de una poliza
    Public Function Find_Receipt_Pol(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nProponum As Double, ByVal nInd_PolPro As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPremium As eRemoteDB.Execute
		Dim lclsPremium As Premium
		
		On Error GoTo Find_Receipt_Pol_Err
		
		Find_Receipt_Pol = True
		
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mdblPolicy <> nPolicy Or mdblProponum <> nProponum Or mintInd_PolPro <> nInd_PolPro Or lblnFind Then
			
			lrecreaPremium = New eRemoteDB.Execute
			'+Definición de parámetros para stored procedure 'insudb.reaPremium_Policy'
			
			With lrecreaPremium
				.StoredProcedure = "reaPremium_Policy"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nInd_PolPro", nInd_PolPro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Do While Not .EOF
						Call Add_COC002(.FieldToClass("nReceipt"), .FieldToClass("nBulletins"), .FieldToClass("nCollecto"), .FieldToClass("dEffecdate"), .FieldToClass("dExpirDat"), .FieldToClass("nCurrency"), .FieldToClass("nPremium"), .FieldToClass("nPremiumn"), .FieldToClass("nStatus_Pre"), .FieldToClass("nContrat"), .FieldToClass("nAmount"), .FieldToClass("nStat_Draft"), .FieldToClass("nDraft"), nBranch, nProduct, .FieldToClass("nPolicy"))
						.RNext()
					Loop 
					.RCloseRec()
					
					Find_Receipt_Pol = True
					mlngBranch = nBranch
					mlngProduct = nProduct
					mdblPolicy = nPolicy
					mdblProponum = nProponum
					mintInd_PolPro = nInd_PolPro
				Else
					Find_Receipt_Pol = False
				End If
			End With
		End If
Find_Receipt_Pol_Err: 
		If Err.Number Then
			Find_Receipt_Pol = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium = Nothing
	End Function
	
	'**%Find_IntermedCommiss_pr: This routine reads the general and the commission data
	'**%of the premium invoice (Premium and Commiss_pr)
	'%Find_IntermedCommiss_pr: Esta rútina permite leer información de la tabla Información general
	'%de recibos (premium) y comisiones de un recibo (commiss_pr).
	Public Function Find_IntermedCommiss_pr(ByVal sUnderw As String, ByVal sRenew As String, ByVal sAll As String, ByVal nStatus_pre As Integer, ByVal nCurrency As Integer, ByVal nTypCard As Integer, ByVal dStartDate As Date, ByVal nDays As Integer, ByVal nIntermed As Double, ByVal nSupervis As Double, ByVal sCertype As String, ByVal sPolicyNum As String, ByVal nDigit As Integer, ByVal nPaynumbe As Integer) As Boolean
		Dim lstrSelect As String
		Dim lintPos As Integer
		Dim lstrComplete As String
		
		Dim llngIndex As Integer
		Dim lstrPay_form As String
		
		Dim lrecreaCOC006 As eRemoteDB.Execute
		Dim lexeConstruct As eRemoteDB.ConstructSelect
		Dim lclsPremium As Premium
		
		lrecreaCOC006 = New eRemoteDB.Execute
		lexeConstruct = New eRemoteDB.ConstructSelect
		lclsPremium = New Premium
		
		nIntermed = IIf(nIntermed = eRemoteDB.Constants.intNull, 0, nIntermed)
		nSupervis = IIf(nSupervis = eRemoteDB.Constants.intNull, 0, nSupervis)
		
		On Error GoTo Find_IntermedCommiss_pr_Err
		
		'**+Stored procedure parameters definition 'insudb.reaCOC006'
		'**+Data of 29/03/2001 11:11:28
		'+Definición de parámetros para stored procedure 'insudb.reaCOC006'
		'+Información leída el 29/03/2001 11:11:28
		
		'lexeConstruct.Owner "Insudb"
		
		lstrSelect = " P.nBranch," & " T10.sDescript DesBranch," & " P.nPolicy, P.nReceipt, P.nComamou, P.nProduct, PRO.sDescript DesProduct," & " P.nIntermed, C.sCliename," & " T24.sDescript DesTratypei," & " T19.sDescript DesStatus_pre," & " P.dStatdate, P.dEffecdate," & " T11.sDescript DesCurrency," & " P.nPremium, P.nType, P.nBalance"
		
		'**+Collected premium invoices
		'+Cobrados
		If nStatus_pre = 7 Then
			lstrSelect = lstrSelect & ", PMO.nBordereaux, PMO.sPay_form, T182.sDescript DesPayForm, P.sDirdebit, CPR.nAmount nAmoComm "
		ElseIf nStatus_pre = 0 Then 
			lstrSelect = lstrSelect & ", PMO.nBordereaux, PMO.sPay_form, T182.sDescript DesPayForm, P.sDirdebit, CPR.nAmount nAmoComm "
		Else
			lstrSelect = lstrSelect & ", 0, ' ', ' ',P.sDirdebit, CPR.nAmount nAmoComm "
		End If
		
		lexeConstruct.SelectClause(lstrSelect)
		lexeConstruct.NameFatherTable("Premium", "P")
		lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Table10", "T10", " P.nBranch     = T10.nBranch")
		lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Table24", "T24", " P.nTratypei   = T24.NTRATYPEI")
		lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Table19", "T19", " P.nStatus_pre = T19.NSTATUS_PRE")
		lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Table11", "T11", " P.nCurrency   = T11.nCodigInt")
		lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Prodmaster", "PRO", "     PRO.nBranch   = P.nBranch " & " AND PRO.nProduct  = P.nProduct " & " AND PRO.sStatregt = '1'")
		lstrSelect = "     P.sCertype  = CPR.sCertype  " & " AND P.nBranch   = CPR.nBranch   " & " AND P.nProduct  = CPR.nProduct  " & " AND P.nReceipt  = CPR.nReceipt  " & " AND P.nDigit    = CPR.nDigit    " & " AND P.nPaynumbe = CPR.nPaynumbe "
		
		If nIntermed <> 0 Then
			lstrSelect = lstrSelect & " AND CPR.nIntermed = " & CStr(nIntermed)
		End If
		
		lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Commiss_pr", "CPR", lstrSelect)
		
		lstrSelect = "I.nIntermed = CPR.nIntermed"
		
		If nSupervis <> 0 And nIntermed = 0 Then
			lstrSelect = Trim(lstrSelect) & " AND I.nSupervis = " & CStr(nSupervis)
		End If
		
		lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Intermedia", "I", lstrSelect)
		
		lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Client", "C", "I.sClient = C.sClient")
		
		'**+Collected premium invoices
		'+Cobrados
		If nStatus_pre = 7 Then
			lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Premium_mo", "PMO", "     PMO.sCertype  = P.sCertype " & " AND PMO.nReceipt  = P.nReceipt " & " AND PMO.nBranch   = P.nBranch  " & " AND PMO.nProduct  = P.nProduct " & " AND PMO.nDigit    = P.nDigit   " & " AND PMO.nPaynumbe = P.nPaynumbe" & " AND PMO.nTransac  > 0 " & " AND PMO.nType     = 2 ")
			
			If lrecreaCOC006.Server = eFunctions.Tables.sTypeServer.sOracle Then
				lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Table182", "T182", " TO_NUMBER(PMO.sPay_form) = T182.sPay_form")
			Else
				If lrecreaCOC006.Server = eFunctions.Tables.sTypeServer.sDB2 Then
					lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Table182", "T182", " RTRIM(PMO.sPay_form) = T182.sPay_form")
				Else
					lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Table182", "T182", " Convert(numeric(8),PMO.sPay_form) = T182.sPay_form")
				End If
			End If
		End If
		
		'**+Collected premium invoices
		'+Cobrados
		If nStatus_pre = 0 Then
			lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "Premium_mo", "PMO", "     PMO.sCertype   = P.sCertype " & " AND PMO.nReceipt  = P.nReceipt " & " AND PMO.nBranch   = P.nBranch  " & " AND PMO.nProduct  = P.nProduct " & " AND PMO.nDigit    = P.nDigit   " & " AND PMO.nPaynumbe = P.nPaynumbe" & " AND PMO.nTransac  > 0 " & " AND PMO.nType     = 2 ")
			
			If lrecreaCOC006.Server = eFunctions.Tables.sTypeServer.sOracle Then
				lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "Table182", "T182", " TO_NUMBER(PMO.sPay_form) = T182.sPay_form")
			Else
				If lrecreaCOC006.Server = eFunctions.Tables.sTypeServer.sDB2 Then
					lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "Table182", "T182", " RTRIM(PMO.sPay_form) = RTRIM(CHAR(T182.sPay_form))")
				Else
					lexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "Table182", "T182", " Convert(numeric(8),PMO.sPay_form) = T182.sPay_form")
				End If
			End If
		End If
		
		If Not lexeConstruct.WhereClause("P.sCertype", eRemoteDB.ConstructSelect.eTypeValue.TypCString, "=" & sCertype) Then
		End If
		
		If Not lexeConstruct.WhereClause("P.nDigit", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & nDigit, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
		End If
		
		If Not lexeConstruct.WhereClause("P.nPaynumbe", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & nPaynumbe, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
		End If
		
		If Not lexeConstruct.WhereClause("P.sStatusva", eRemoteDB.ConstructSelect.eTypeValue.TypCString, "> 3", eRemoteDB.ConstructSelect.eWordConnection.eAnd, "(") Then
		End If
		
		If Not lexeConstruct.WhereClause("P.sStatusva", eRemoteDB.ConstructSelect.eTypeValue.TypCString, "< 2", eRemoteDB.ConstructSelect.eWordConnection.eOr,  , ")") Then
		End If
		
		If CDbl(sAll) = 0 Then
			If CDbl(sUnderw) = 1 And CDbl(sRenew) = 1 Then
				If Not lexeConstruct.WhereClause("P.nTratypei", eRemoteDB.ConstructSelect.eTypeValue.TypCString, "IN (1,2)", eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				End If
			ElseIf CDbl(sUnderw) = 1 Then 
				If Not lexeConstruct.WhereClause("P.nTratypei", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 1, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				End If
			ElseIf CDbl(sRenew) = 1 Then 
				If Not lexeConstruct.WhereClause("P.nTratypei", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 2, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				End If
			End If
		End If
		
		'**+All premium invoices
		'+ Todos
		If nStatus_pre <> 0 Then
			'**+Pending for collection premium invoices
			'+ pendientes de cobro
			If nStatus_pre = 1 Then
				If Not lexeConstruct.WhereClause("P.nStatus_pre", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 1, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				End If
				
				If Not lexeConstruct.WhereClause("P.nType", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 1, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				End If
				
				If nDays <> 0 Then
					lstrSelect = ") >= " & nDays
					If Not lexeConstruct.WhereClause("DATEDIFF(P.dEffecdate", eRemoteDB.ConstructSelect.eTypeValue.TypCString, Format(dStartDate, "yyyyMMdd"), eRemoteDB.ConstructSelect.eWordConnection.eAnd,  , lstrSelect) Then
					End If
				End If
			End If
			
			'**+Pending for refund premium invoices
			'+pendientes de devolución
			If nStatus_pre = 2 Then
				If Not lexeConstruct.WhereClause("P.nStatus_pre", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 1, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				End If
				
				If Not lexeConstruct.WhereClause("P.nType", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 2, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				End If
			End If
			
			'**+Refunded premium invoices
			'+ Devueltos
			If nStatus_pre = 3 Then
				If Not lexeConstruct.WhereClause("P.nStatus_pre", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 2, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				End If
				
				If Not lexeConstruct.WhereClause("P.nType", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 2, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				End If
			End If
			
			'**+Cancelled
			'+ Anulados
			If nStatus_pre = 4 Then
				If Not lexeConstruct.WhereClause("P.nStatus_pre", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 3, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				End If
			End If
			
			'**+Direct debited premium invoices
			'+Domiciliados con cargo automático a tarjeta de credito o Domiciliados con cargo automático a banco
			If nStatus_pre = 5 Or nStatus_pre = 6 Then
				If Not lexeConstruct.WhereClause("P.nStatus_pre", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 4, eRemoteDB.ConstructSelect.eWordConnection.eAnd, "(") Then
				End If
				
				If Not lexeConstruct.WhereClause("P.nStatus_pre", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 5, eRemoteDB.ConstructSelect.eWordConnection.eOr,  , ")") Then
				End If
				
				If Not lexeConstruct.WhereClause("P.sDirdebit ", eRemoteDB.ConstructSelect.eTypeValue.TypCString, "=" & 1, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				End If
			End If
			
			'**+Collected premium invoices
			'+ Cobrados
			If nStatus_pre = 7 Then
				If Not lexeConstruct.WhereClause("P.nStatus_pre", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 2, eRemoteDB.ConstructSelect.eWordConnection.eAnd, "(") Then
				End If
				
				If Not lexeConstruct.WhereClause("P.nStatus_pre", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 5, eRemoteDB.ConstructSelect.eWordConnection.eOr,  , ")") Then
				End If
				
				If Not lexeConstruct.WhereClause("P.nType", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & 1, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				End If
			End If
		End If
		
		If Not lexeConstruct.WhereClause("$DATE(P.dStatdate)", eRemoteDB.ConstructSelect.eTypeValue.TypCDate, ">=" & dStartDate, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
		End If
		
		If nCurrency <> 0 Then
			If lexeConstruct.WhereClause("P.nCurrency", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & nCurrency, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
			End If
		End If
		
		lstrSelect = lexeConstruct.Answer
		
		'UPGRADE_NOTE: Object lexeConstruct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeConstruct = Nothing
		
		lintPos = InStr(1, lstrSelect, "IN( LIKE  ")
		
		Do While lintPos > 0
			lstrSelect = Mid(lstrSelect, 1, lintPos - 1) & "IN(" & Mid(lstrSelect, lintPos + 10) & " "
			lintPos = InStr(lintPos, lstrSelect, "IN( LIKE  ")
		Loop 
		
		If lrecreaCOC006.Server = eFunctions.Tables.sTypeServer.sOracle Then
			lintPos = InStr(1, lstrSelect, "DATEDIFF(P.dEffecdate LIKE ")
			
			While lintPos > 0
				lstrSelect = Mid(lstrSelect, 1, lintPos - 1) & "P.dEffecdate, " & Mid(lstrSelect, lintPos + 27) & " "
				lintPos = InStr(CStr(lintPos), "DATEDIFF(P.dEffecdate LIKE ")
			End While
		Else
			If lrecreaCOC006.Server = eFunctions.Tables.sTypeServer.sSQLServer7 Or lrecreaCOC006.Server = eFunctions.Tables.sTypeServer.sSQLServer65 Then
				lintPos = InStr(1, lstrSelect, "DATEDIFF(P.dEffecdate LIKE ")
				
				While lintPos > 0
					lstrSelect = Mid(lstrSelect, 1, lintPos - 1) & "DATEDIFF(DAY,P.dEffecdate, " & Mid(lstrSelect, lintPos + 27) & " "
					lintPos = InStr(CStr(lintPos), "DATEDIFF(P.dEffecdate LIKE ")
				End While
			End If
		End If
		
		lintPos = 2751 - Len(lstrSelect)
		lstrComplete = " "
		
		For llngIndex = 2 To lintPos
			lstrComplete = lstrComplete & " "
		Next 
		
		lstrSelect = lstrSelect & lstrComplete
		
		With lrecreaCOC006
			.StoredProcedure = "reaCOC006"
			
			.Parameters.Add("sCadena1", Mid(lstrSelect, 1, 250), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCadena2", Mid(lstrSelect, 251, 250), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCadena3", Mid(lstrSelect, 501, 250), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCadena4", Mid(lstrSelect, 751, 250), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCadena5", Mid(lstrSelect, 1001, 250), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCadena6", Mid(lstrSelect, 1251, 250), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCadena7", Mid(lstrSelect, 1501, 250), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCadena8", Mid(lstrSelect, 1751, 250), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCadena9", Mid(lstrSelect, 2001, 250), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCadena10", Mid(lstrSelect, 2251, 250), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCadena11", Mid(lstrSelect, 2501, 250), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCadena12", Mid(lstrSelect, 2751, 250), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypList", CStr(nStatus_pre), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			'**+Premium invoices direct debited to a credit card
			'+ Domiciliados con cargo automático a tarjeta de credito
			If nStatus_pre = 6 Then
				If nTypCard <> 0 Then
					.Parameters.Add("sTypCard", CStr(nTypCard), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					.Parameters.Add("sTypCard", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
			Else
				.Parameters.Add("sTypCard", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			.Parameters.Add("sTypeNumeraP", sPolicyNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed1", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					lstrPay_form = .FieldToClass("sPay_form")
					
					lclsPremium = Add(sCertype, .FieldToClass("nReceipt"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), .FieldToClass("dEffecdate"), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, .FieldToClass("nType"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nPremium", 0), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nIntermed"), .FieldToClass("nPolicy"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nType"), .FieldToClass("nBranch"), eRemoteDB.Constants.intNull, .FieldToClass("nProduct"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.strNull))
					lclsPremium.sDesType = lclsPremium.TypeReceipt
					lclsPremium.sDescTratypei = .FieldToClass("DesTratypei")
					lclsPremium.sDescStatus_pre = .FieldToClass("DesStatus_Pre")
					lclsPremium.dStatdate = .FieldToClass("dStatdate")
					lclsPremium.nCertif = .FieldToClass("nCertif")
					lclsPremium.sDescCard_type = .FieldToClass("DesCard_type")
					lclsPremium.sPay_form = .FieldToClass("sPay_form")
					lclsPremium.nBordereaux = .FieldToClass("nBordereaux")
					lclsPremium.dEffecdate = .FieldToClass("dEffecdate")
					lclsPremium.sDescCurrency = .FieldToClass("DesCurrency")
					lclsPremium.nComamou = .FieldToClass("nComamou")
					lclsPremium.nIntermed = .FieldToClass("nIntermed")
					lclsPremium.nAmountP = .FieldToClass("nAmoComm", 0)
					lclsPremium.sCliename = .FieldToClass("sCliename")
					
					.RNext()
				Loop 
				
				Find_IntermedCommiss_pr = True
				
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaCOC006 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCOC006 = Nothing
		'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPremium = Nothing
		
Find_IntermedCommiss_pr_Err: 
		If Err.Number Then
			Find_IntermedCommiss_pr = False
		End If
		On Error GoTo 0
	End Function
	
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Premium
		Get
			
			' used when referencing an element in the collection
			' vntIndexKey contains either the Index or Key to the collection,
			' this is why it is declared as a Variant
			' Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			' used when retrieving the number of elements in the
			' collection. Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		
		' used when removing an element from the collection
		' vntIndexKey contains either the Index or Key, which is why
		' it is declared as a Variant
		' Syntax: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Initializes the class.
	'%Class_Initialize: Inicializa la clase.
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Destroys the class.
	'%Class_Terminate: Finaliza la clase.
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**%FindCAC003: This routine reads the premium invoices that are pending to be printed
	'%FindCAC003: Permite seleccionar todas las recibos pendientes por imprimir.
	Public Function FindCAC003(ByVal nOffice As Integer, ByVal nBranch As Integer, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaCAC003 As eRemoteDB.Execute
		
		On Error GoTo FindCAC003_Err
		lrecReaCAC003 = New eRemoteDB.Execute
		FindCAC003 = True
		
		If mintOffice <> nOffice Or mlngBranch <> nBranch Or lblnFind Then
			'**+Stored procedure parameters definition 'reaPremiumUnprinted'.
			'+Definición de parámetros para stored procedure 'reaPremiumUnprinted'.
			With lrecReaCAC003
				.StoredProcedure = "reaPremiumUnprinted"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mintOffice = nOffice
					mlngBranch = nBranch
					Do While Not .EOF
						Call Add_CAC003("2", .FieldToClass("nBranch"), .FieldToClass("sDesBranch"), .FieldToClass("nProduct"), .FieldToClass("sDesProd"), .FieldToClass("nPolicy"), .FieldToClass("nPolRec"), .FieldToClass("nCertif"), .FieldToClass("nOffice"), .FieldToClass("sDesOffice"), .FieldToClass("sClient"), .FieldToClass("sCliename"))
						.RNext()
					Loop 
					.RCloseRec()
				Else
					FindCAC003 = False
					mintOffice = 0
					mlngBranch = 0
				End If
			End With
		End If
		
FindCAC003_Err: 
		If Err.Number Then
			FindCAC003 = False
		End If
		'UPGRADE_NOTE: Object lrecReaCAC003 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCAC003 = Nothing
		On Error GoTo 0
	End Function
	
	'%insUpdateTmp_COC679:Actualiza la temporal TMP_COC679
	Public Function insUpdateTmp_COC679(ByVal sKey As String, ByVal sChains As String, ByVal nFirstRecord As Integer, ByVal nLastRecord As Integer) As Boolean
		Dim lrecUpdCOC679 As eRemoteDB.Execute
		lrecUpdCOC679 = New eRemoteDB.Execute
		
		On Error GoTo lrecUpdCOC679_err
		
		insUpdateTmp_COC679 = True
		
		With lrecUpdCOC679
			.StoredProcedure = "insUpdCOC679"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChains", sChains, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFirstReg", nFirstRecord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLastReg", nLastRecord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insUpdateTmp_COC679 = .Run(False)
		End With
		
lrecUpdCOC679_err: 
		If Err.Number Then
			insUpdateTmp_COC679 = False
		End If
		
		'UPGRADE_NOTE: Object lrecUpdCOC679 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdCOC679 = Nothing
	End Function
	
	'%FindCOC679: Permite obtener las cartas de aviso de anulación de las pólizas.
	Public Function FindCOC679(ByVal dProcess As Date, Optional ByVal lblnFind As Boolean = True, Optional ByVal intFirstRecord As Integer = 0, Optional ByVal intLastRecord As Integer = 0, Optional ByVal sFind As String = "", Optional ByVal sKey As String = "") As Boolean
		Dim lrecReaCOC679 As eRemoteDB.Execute
		Dim lclsPremium As Premium
		Dim lintRecordsAdd As Integer
		Dim lintTotalRecords As Integer
		
		lrecReaCOC679 = New eRemoteDB.Execute
		
		FindCOC679 = True
		
		On Error GoTo FindCOC679_Err
		
		lintRecordsAdd = 0
		lintTotalRecords = 0
		
		If mdtmProcess <> dProcess Or lblnFind Then
			
			'+Definición de parámetros para stored procedure 'insudb.insReaCOC679'.
			With lrecReaCOC679
				.StoredProcedure = "insreaCOC679"
				.Parameters.Add("dProcess", dProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sFind", sFind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sKeyFind", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nFirstReg", intFirstRecord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nLastReg", intLastRecord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mdtmProcess = dProcess
					
					'+Se guarda el numero total de registros de la busqueda.
					If sFind = "1" Then
						mlngCountCOC679 = lrecReaCOC679.FieldToClass("NROW_COUNT")
					End If
					
					'-Se guarda la clave del registro
					mstrKey = lrecReaCOC679.FieldToClass("sKey")
					
					Do While Not .EOF
						
						lclsPremium = New Premium
						With lclsPremium
							.nBranch = lrecReaCOC679.FieldToClass("nBranch")
							.sDesBranch = lrecReaCOC679.FieldToClass("sBranch")
							.nProduct = lrecReaCOC679.FieldToClass("nProduct")
							.sDescProd = lrecReaCOC679.FieldToClass("sProduct")
							.nPolicy = lrecReaCOC679.FieldToClass("nPolicy")
							.nCertif = lrecReaCOC679.FieldToClass("nCertif")
							.nReceipt = lrecReaCOC679.FieldToClass("nReceipt")
							.dLimitdate = lrecReaCOC679.FieldToClass("dLimitDate")
							.nDaysPend = lrecReaCOC679.FieldToClass("nDaysPend")
							.nNotice = lrecReaCOC679.FieldToClass("nNotice")
							.sDescCurrency = lrecReaCOC679.FieldToClass("sCurrency")
							.nPremium = lrecReaCOC679.FieldToClass("nPremium")
							.nDraft = lrecReaCOC679.FieldToClass("nDraft")
							.mlngRows = lrecReaCOC679.FieldToClass("nRow")
							.sCadena = lrecReaCOC679.FieldToClass("sPrint")
						End With
						Call Add_COC679(lclsPremium)
						'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPremium = Nothing
						
						.RNext()
					Loop 
					.RCloseRec()
				Else
					FindCOC679 = False
					mdtmProcess = eRemoteDB.Constants.dtmNull
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReaCOC679 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaCOC679 = Nothing
		End If
		
FindCOC679_Err: 
		If Err.Number Then
			FindCOC679 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%FindCOC747: Permite obtener el plan de pago de una póliza/certificado.
	Public Function FindCOC747(ByVal nInsur_area As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaCOC747 As eRemoteDB.Execute
		Dim lclsPremium As Premium
		
		lrecReaCOC747 = New eRemoteDB.Execute
		
		FindCOC747 = True
		
		On Error GoTo FindCOC747_Err
		
		If mlngInsur_area <> nInsur_area Or mlngBranch <> nBranch Or mlngProduct <> nProduct Or mdblPolicy <> nPolicy Or mdblCertif <> nCertif Or lblnFind Then
			'**+Stored procedure parameters definition 'insudb.insReaCOC747'.
			'+Definición de parámetros para stored procedure 'insudb.insReaCOC747'.
			
			With lrecReaCOC747
				.StoredProcedure = "insreaCOC747"
				
				.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mlngInsur_area = nInsur_area
					mlngBranch = nBranch
					mlngProduct = nProduct
					mdblPolicy = nPolicy
					mdblCertif = nCertif
					
					Do While Not .EOF
						lclsPremium = New Premium
						With lclsPremium
							.nId_NumPay = lrecReaCOC747.FieldToClass("nId_NumPay")
							.nInsur_area = nInsur_area
							.nBranch = lrecReaCOC747.FieldToClass("nBranch")
							.nProduct = lrecReaCOC747.FieldToClass("nProduct")
							.nPolicy = lrecReaCOC747.FieldToClass("nPolicy")
							.nCertif = lrecReaCOC747.FieldToClass("nCertif")
							.nReceipt = lrecReaCOC747.FieldToClass("nReceipt")
							.nContrat = lrecReaCOC747.FieldToClass("nContrat")
							.nDraft = lrecReaCOC747.FieldToClass("nDraft")
							.dLimitdate = lrecReaCOC747.FieldToClass("dLimitDate")
							.nStat_draft = lrecReaCOC747.FieldToClass("nStat_draft")
							.nAmount = lrecReaCOC747.FieldToClass("nAmount")
						End With
						Call Add_COC747(lclsPremium)
						.RNext()
						'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPremium = Nothing
					Loop 
					.RCloseRec()
				Else
					FindCOC747 = False
					
					mlngInsur_area = 0
					mlngBranch = 0
					mlngProduct = 0
					mdblPolicy = 0
					mdblCertif = 0
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReaCOC747 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaCOC747 = Nothing
		End If
		
FindCOC747_Err: 
		If Err.Number Then
			FindCOC747 = False
		End If
		
		On Error GoTo 0
	End Function
	
	
	'%FindCOL502: Rescata los bancos con depositos efectuados a una fecha.
	Public Function FindTMP_COL502(Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaTCOL502 As eRemoteDB.Execute
		Dim lclsPremium As Premium
		
		lrecReaTCOL502 = New eRemoteDB.Execute
		
		FindTMP_COL502 = True
		
		On Error GoTo FindTCOL502_Err
		
		If lblnFind Then
			'**+Stored procedure parameters definition 'insreaTMP_COL502'.
			With lrecReaTCOL502
				.StoredProcedure = "insreaTMP_COL502"
				
				If .Run Then
					Do While Not .EOF
						lclsPremium = New Premium
						
						lclsPremium.nId_Register = .FieldToClass("nId_Register")
						lclsPremium.nBank_code = .FieldToClass("nBank_Code")
						lclsPremium.sAcc_Number = .FieldToClass("sAcc_Number")
						lclsPremium.sDep_Number = .FieldToClass("sDep_Number")
						lclsPremium.nBank_Agree = .FieldToClass("nBank_Agree")
						lclsPremium.nAmount_PAC = .FieldToClass("nAmount")
						lclsPremium.nCurrency = .FieldToClass("nCurrency")
						lclsPremium.nAcc_Bank = .FieldToClass("nAcc_Bank")
						lclsPremium.dEffecdate = .FieldToClass("dEffecdate")
						lclsPremium.nMovement = .FieldToClass("nMovement")
						lclsPremium.nAccount = .FieldToClass("nAccount")
						lclsPremium.nCommission = .FieldToClass("nCommission")
						lclsPremium.nAmountDoc = .FieldToClass("nAmountDoc")
						lclsPremium.nAmountDif = .FieldToClass("nAmountDif")
						
						Call Add_TMP_COL502(lclsPremium)
						.RNext()
						'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPremium = Nothing
					Loop 
					.RCloseRec()
				Else
					FindTMP_COL502 = False
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReaTCOL502 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaTCOL502 = Nothing
		End If
		
FindTCOL502_Err: 
		If Err.Number Then
			FindTMP_COL502 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%FindCO633: Permite obtener el plan de pago de una póliza/certificado.
	Public Function FindCO633(ByVal nInsur_area As Integer, ByVal nTypOper As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double, ByVal nContrat As Double, ByVal nDraft As Integer, ByVal dCollSus_ini As Date, ByVal dCollSus_end As Date, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecCO633 As eRemoteDB.Execute
		Dim lclsPremium As Premium

        Dim dLimitdate_aa As Object = New Object
        Dim dLimitdate_mm As Object
		Dim dLimitdate_dd As Object
		Dim TotDraft As Object
        Dim mm_aux As Object = New Object


        lrecCO633 = New eRemoteDB.Execute
		
		TotDraft = 0
		
		FindCO633 = True
		
		On Error GoTo FindCO633_Err
		
		If mlngInsur_area <> nInsur_area Or mintTypOper <> nTypOper Or mstrCertype <> sCertype Or mlngBranch <> nBranch Or mlngProduct <> nProduct Or mdblPolicy <> nPolicy Or mdblCertif <> nCertif Or mdblReceipt <> nReceipt Or mdblContrat <> nContrat Or mintDraft <> nDraft Or mdtmCollSus_ini <> dCollSus_ini Or mdtmCollSus_end <> dCollSus_end Or lblnFind Then
			'**+Stored procedure parameters definition 'insudb.insReaCOC747'.
			'+Definición de parámetros para stored procedure 'insudb.insReaCOC747'.
			
			With lrecCO633
				.StoredProcedure = "reaCO633"
				.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTypoper", nTypOper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dCollsus_ini", dCollSus_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dCollsus_end", dCollSus_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					mlngInsur_area = nInsur_area
					mintTypOper = nTypOper
					mstrCertype = sCertype
					mlngBranch = nBranch
					mlngProduct = nProduct
					mdblPolicy = nPolicy
					mdblCertif = nCertif
					mdblReceipt = nReceipt
					mdblContrat = nContrat
					mintDraft = nDraft
					mdtmCollSus_ini = dCollSus_ini
					mdtmCollSus_end = dCollSus_end
					
					Do While Not .EOF
						lclsPremium = New Premium
						With lclsPremium
							.sCertype = lrecCO633.FieldToClass("sCertype")
							.nBranch = lrecCO633.FieldToClass("nBranch")
							.nProduct = lrecCO633.FieldToClass("nProduct")
							.nPolicy = lrecCO633.FieldToClass("nPolicy")
							.nCertif = lrecCO633.FieldToClass("nCertif")
							.nReceipt = lrecCO633.FieldToClass("nReceipt")
							.nContrat = lrecCO633.FieldToClass("nContrat")
							.nDraft = IIf(lrecCO633.FieldToClass("nDraft") = -1, eRemoteDB.Constants.intNull, lrecCO633.FieldToClass("nDraft"))
							.nBulletins = lrecCO633.FieldToClass("nBulletins")
							.nAmount = lrecCO633.FieldToClass("nAmount")
							.nCurrency = lrecCO633.FieldToClass("nCurrency")
							.dStatdate = lrecCO633.FieldToClass("dStatdate")
							.dExpirDat = lrecCO633.FieldToClass("dExpirDat")
                            .nCount = lrecCO633.FieldToClass("nRow")
							'+ Se implementa que cuando corresponda a un recibo con mas de una cuota, calcule el mes siguiente que corresponde
							'+ al vencimiento
							If TotDraft = 0 Then
								.dLimitdate = lrecCO633.FieldToClass("dLimitdate")
								mm_aux = (Mid(Format(.dLimitdate, "dd/MM/yyyy"), 4, 2))
								dLimitdate_aa = Mid(Format(.dLimitdate, "dd/MM/yyyy"), 7)
								TotDraft = TotDraft + 1
							Else
								.dLimitdate = lrecCO633.FieldToClass("dLimitdate")
								dLimitdate_dd = Mid(Format(.dLimitdate, "dd/MM/yyyy"), 1, 2)
								dLimitdate_mm = CStr(mm_aux + 1)
								
								
								If dLimitdate_mm > 12 Then
									dLimitdate_mm = "01"
									dLimitdate_aa = CStr(dLimitdate_aa + 1)
								End If
								.dLimitdate = CDate(Format(CDate(dLimitdate_dd + "/" + dLimitdate_mm + "/" + dLimitdate_aa), "dd/MM/yyyy"))
								mm_aux = dLimitdate_mm
							End If
							
						End With
						Call Add_CO633(lclsPremium)
						.RNext()
						'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPremium = Nothing
					Loop 
					.RCloseRec()
				Else
					FindCO633 = False
					
					mlngInsur_area = eRemoteDB.Constants.intNull
					mintTypOper = eRemoteDB.Constants.intNull
					mstrCertype = String.Empty
					mlngBranch = eRemoteDB.Constants.intNull
					mlngProduct = eRemoteDB.Constants.intNull
					mdblPolicy = eRemoteDB.Constants.intNull
					mdblCertif = eRemoteDB.Constants.intNull
					mdblReceipt = eRemoteDB.Constants.intNull
					mdblContrat = eRemoteDB.Constants.intNull
					mintDraft = eRemoteDB.Constants.intNull
					mdtmCollSus_ini = eRemoteDB.Constants.dtmNull
					mdtmCollSus_end = eRemoteDB.Constants.dtmNull
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecCO633 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecCO633 = Nothing
		End If
		
FindCO633_Err: 
		If Err.Number Then
			FindCO633 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%Add_CO635: Adds a new class instance to the collection
	'%Add_CO635: se añade una nueva instancia de la clase a la colección
	Public Function Add_CO635(ByVal objClass As Premium) As Premium
		With objClass
			mCol.Add(objClass, "CO" & .sCertype & .nBranch & .nProduct & .nPolicy & .nReceipt & .nContrat & .nDraft & .nCurrency & .nAmount & .nBulletins & .dLimitdate & .dEffecdate & .dExpirDat)
		End With
		
		'return the object created
		Add_CO635 = objClass
	End Function
	
	'%FindCO635: Permite obtener los recibos o cuotas pendientes
	Public Function FindCO635(ByVal sKey As String, ByVal nRow As Integer, ByVal sRead As String, ByVal nAgency As Integer, ByVal sColltype As String, ByVal nAction As Integer, ByVal nCollector As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nWay_Pay As Integer, ByVal dLimitdate As Date) As Boolean
		Dim lrecreaCo635 As eRemoteDB.Execute
		Dim lclsPremium As Premium
		
		On Error GoTo reaCO635_Err
		
		lrecreaCo635 = New eRemoteDB.Execute
		
		If mintAgency <> nAgency Or mstrColltype <> sColltype Or mintAction <> nAction Or mlngCollector <> nCollector Then
			'+
			'+ Definición de store procedure reaCo635 al 02-19-2002 09:31:10
			'+
			With lrecreaCo635
				.StoredProcedure = "reaCo635"
				.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRow", nRow, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sRead", sRead, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sColltype", sColltype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", IIf(nBranch = eRemoteDB.Constants.intNull, 0, nBranch), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", IIf(nPolicy = eRemoteDB.Constants.intNull, 0, nPolicy), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dLimitdate", dLimitdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					FindCO635 = True
					mintAgency = nAgency
					mstrColltype = sColltype
					mintAction = nAction
					mlngCollector = nCollector
					Do While Not .EOF
						lclsPremium = New Premium
						lclsPremium.sCertype = .FieldToClass("sCertype")
						lclsPremium.nBranch = .FieldToClass("nBranch")
						lclsPremium.sDesBranch = .FieldToClass("desBranch")
						lclsPremium.nProduct = .FieldToClass("nProduct")
						lclsPremium.sDescProd = .FieldToClass("desProduct")
						lclsPremium.nPolicy = .FieldToClass("nPolicy")
						lclsPremium.nReceipt = .FieldToClass("nReceipt")
						lclsPremium.nContrat = .FieldToClass("nContrat")
						lclsPremium.nDraft = IIf(.FieldToClass("nDraft") = -1, eRemoteDB.Constants.intNull, .FieldToClass("nDraft"))
						lclsPremium.nCurrency = .FieldToClass("nCurrency")
						lclsPremium.sDescCurrency = .FieldToClass("desCurrency")
						lclsPremium.nAmount = .FieldToClass("nAmount")
						lclsPremium.nBulletins = .FieldToClass("nBulletins")
						lclsPremium.dLimitdate = .FieldToClass("dLimitdate")
						lclsPremium.dEffecdate = .FieldToClass("dEffecdate")
						lclsPremium.dExpirDat = .FieldToClass("dExpirdat")
						lclsPremium.sPrint = .FieldToClass("sPrint")
						Call Add_CO635(lclsPremium)
						'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPremium = Nothing
						.RNext()
					Loop 
					.RCloseRec()
				Else
					FindCO635 = False
					mintAgency = eRemoteDB.Constants.intNull
					mstrColltype = String.Empty
					mintAction = eRemoteDB.Constants.intNull
					mlngCollector = eRemoteDB.Constants.intNull
				End If
			End With
		End If
reaCO635_Err: 
		If Err.Number Then
			FindCO635 = False
		End If
		'UPGRADE_NOTE: Object lrecreaCo635 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCo635 = Nothing
		On Error GoTo 0
	End Function
	
	'%Add_CO003: Agrega un elemento a la colección de convenios de pago.
	Public Function Add_CO003(ByVal lclsPremium As Premium) As Premium
		With lclsPremium
			mCol.Add(lclsPremium, "CO" & .nReceipt & .nPaynumbe & .dPayDate & .nIntammou & .nRate & .nPremium & .nStatus_pre)
			
		End With
		
		'+ Retorna el objeto creado
		Add_CO003 = lclsPremium
	End Function
	
	'%Find_CO003: Obtiene la información referente a los convenios de pago.
	Public Function Find_CO003(ByVal nReceipt As Double, ByVal dEffecdate As Date, ByVal nAction As Integer, Optional ByVal lblnAll As Boolean = False) As Boolean
		Dim lrecreaPremium As eRemoteDB.Execute
		Dim lclsPremium As eCollection.Premium
		
		lrecreaPremium = New eRemoteDB.Execute
		
		mCol = New Collection
		
		'Definición de parámetros para stored procedure 'insudb.Premium'
		
		With lrecreaPremium
			.StoredProcedure = "reaAgreement"
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_CO003 = True
				Do While Not .EOF
					lclsPremium = New eCollection.Premium
					lclsPremium.nReceipt = .FieldToClass("nReceipt")
					lclsPremium.nPaynumbe = .FieldToClass("nPaynumbe")
					lclsPremium.dPayDate = .FieldToClass("dPaydate")
					lclsPremium.nIntammou = .FieldToClass("nIntammou")
					lclsPremium.nRate = .FieldToClass("nRate")
					lclsPremium.nPremium = .FieldToClass("nPremium")
					lclsPremium.nStatus_pre = .FieldToClass("nStatus_pre")
					Call Add_CO003(lclsPremium)
					'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsPremium = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find_CO003 = False
			End If
		End With
	End Function
	
	'% insUpdpremium_stat: Cambia a estado valido todos los recivos asociados a una poliza
	Public Function insUpdpremium_stat(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecinsUpdpremium_stat As eRemoteDB.Execute
		On Error GoTo insUpdpremium_stat_Err
		
		lrecinsUpdpremium_stat = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdpremium_stat al 04-16-2003 18:10:17
		'+
		With lrecinsUpdpremium_stat
			.StoredProcedure = "insUpdpremium_stat"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insUpdpremium_stat = .Run(False)
		End With
		
insUpdpremium_stat_Err: 
		If Err.Number Then
			insUpdpremium_stat = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdpremium_stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdpremium_stat = Nothing
		On Error GoTo 0
		
	End Function
	
	
	'**% Add_PremiumQuery: adds a new element to the collection used in the CAC003.
	'% Add_PremiumQuery: añade un nuevo elemento a la colección usada en la CAC003.
	Public Function Add_PremiumQuery(ByVal sCertype As String, ByVal nBranch As Integer, ByVal sDescBranch As String, ByVal nProduct As Integer, ByVal sDescProduct As String, ByVal nPolicy As Double, ByVal nReceipt As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nBalance As Double, ByVal dEffecdate As Date, ByVal dExpirDat As Date, ByVal nStatus_pre As Integer, ByVal sDescStatus_pre As String) As Premium
		'+ Create a new object
		Dim objNewMember As eCollection.Premium
		
		objNewMember = New eCollection.Premium
		
		'+ Set the properties passed into the method
		With objNewMember
			.sCertype = sCertype
			.nBranch = nBranch
			.sDesBranch = sDescBranch
			.nProduct = nProduct
			.sDesProduct = sDescProduct
			.nReceipt = nReceipt
			.nPolicy = nPolicy
			.nCertif = nCertif
			.sClient = sClient
			.nBalance = nBalance
			.dEffecdate = dEffecdate
			.dExpirDat = dExpirDat
			.nStatus_pre = nStatus_pre
			.sDescStatus_pre = sDescStatus_pre
			
		End With
		
		'** Key of the table: sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt
		'+ Llave de la tabla: sCertype, nBranch, nProduct, nPolicy, nCertif, nReceipt
		mCol.Add(objNewMember, "A" & sCertype & nBranch & nProduct & nPolicy & nCertif & nReceipt)
		
		'+ Return the object created
		Add_PremiumQuery = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function


	'**%FindPremiumQuery: This routine reads the premium invoices that are pending to be printed
	'%FindPremiumQuery: Permite seleccionar todas las recibos pendientes por imprimir.
	Public Function FindPremiumQuery(ByVal sClient As String, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecFindPremiumQuery As eRemoteDB.Execute

		On Error GoTo FindPremiumQuery_Err
		lrecFindPremiumQuery = New eRemoteDB.Execute
		FindPremiumQuery = True

		If lblnFind Then
			'**+Stored procedure parameters definition 'reaPremiumUnprinted'.
			'+Definición de parámetros para stored procedure 'reaPremiumUnprinted'.
			With lrecFindPremiumQuery
				.StoredProcedure = "reaPremiumQueryByClient"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

				If .Run Then

					Do While Not .EOF
						Call Add_PremiumQuery(.FieldToClass("sCertype"), .FieldToClass("nBranch"), .FieldToClass("sDescBranch"), .FieldToClass("nProduct"), .FieldToClass("sDescProduct"), .FieldToClass("nPolicy"), .FieldToClass("nReceipt"), .FieldToClass("nCertif"), .FieldToClass("sClient"), .FieldToClass("nBalance"), .FieldToClass("dEffecdate"), .FieldToClass("dExpirDat"), .FieldToClass("nStatus_pre"), .FieldToClass("sDescStatus_pre"))
						.RNext()
					Loop
					.RCloseRec()
				Else
					FindPremiumQuery = False
				End If
			End With
		End If

FindPremiumQuery_Err:
		If Err.Number Then
			FindPremiumQuery = False
		End If
		'UPGRADE_NOTE: Object lrecFindPremiumQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFindPremiumQuery = Nothing
		On Error GoTo 0
	End Function

	'%Objetivo: Esta función permite realizar la búsqueda de la información en la tabla 'Premium' si la
	'%          póliza es individual y si es colectiva busca información en la tabla Policy_His.
	'%Parámetros:
	'%      sCertype -
	'%      nBranch  -
	'%      nProduct -
	'%      nPolicy  -
	'%      nCertif  -
	'------------------------------------------------------------------------------------------------------------------------
	Public Function FindReceipt_Pol(ByVal sCertype As String,
									 ByVal nBranch As Integer,
									 ByVal nProduct As Integer,
									 ByVal nPolicy As Double,
									 ByVal nCertif As Double,
									 ByVal sPolitype As String,
									 ByVal nPremium As Double,
									 ByVal dEffecdate As Date,
									 ByVal sDevReceipt As String) As Boolean
		'------------------------------------------------------------------------------------------------------------------------
		Dim lclsPremium As eRemoteDB.Execute
		Dim lclsPremiumItem As Premium

		On Error GoTo FindReceipt_Pol_err

		lclsPremium = New eRemoteDB.Execute

		With lclsPremium
			.StoredProcedure = "reaReceipt2_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPoliType", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDevReceipt", sDevReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run(True) Then
				Do While Not .EOF
					lclsPremiumItem = New Premium
					lclsPremiumItem.nReceipt = .FieldToClass("nReceipt")
					lclsPremiumItem.dEffecdate = .FieldToClass("dEffecdate")
					lclsPremiumItem.dExpirDat = .FieldToClass("dExpirDat")
					lclsPremiumItem.nPremium = .FieldToClass("nPremium")
					lclsPremiumItem.sCadena = .FieldToClass("sShort_Des")

					lclsPremiumItem.nCurrency = .FieldToClass("nCurrency")
					lclsPremiumItem.nTratypei = .FieldToClass("nTratypei")
					lclsPremiumItem.sLeadinvo = .FieldToClass("sLeadinvo")
					lclsPremiumItem.sClient = .FieldToClass("sClient")
					lclsPremiumItem.sCliename = .FieldToClass("sCliename")

					lclsPremiumItem.nContrat = .FieldToClass("nContrat") 'RQ2019-470 MDP AFU

					Call Add_SCO6000(lclsPremiumItem)

					lclsPremiumItem = Nothing
					.RNext()
				Loop

				FindReceipt_Pol = True
				.RCloseRec()
			Else
				FindReceipt_Pol = False
			End If
		End With


FindReceipt_Pol_err:
		lclsPremium = Nothing
	End Function

	'%Add_CO003: Agrega un elemento a la colección de convenios de pago.
	'--------------------------------------------------------------------------------
	Public Function Add_SCO6000(ByVal lclsPremium As Premium) As Premium
		'--------------------------------------------------------------------------------

		On Error GoTo Add_SCO6000_err

		With lclsPremium
			mCol.Add(lclsPremium, .nReceipt &
								  .dEffecdate &
								  .dExpirDat &
								  .nPremium &
								  .nTratypei)

		End With

		'+ Retorna el objeto creado
		Add_SCO6000 = lclsPremium

Add_SCO6000_err:
		Add_SCO6000 = Nothing
	End Function
End Class







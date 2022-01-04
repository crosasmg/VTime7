Option Strict Off
Option Explicit On
Public Class Premium
	'%-------------------------------------------------------%'
	'% $Workfile:: Premium.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 12/11/04 12:52p                              $%'
	'% $Revision:: 263                                      $%'
	'%-------------------------------------------------------%'
	
	Private mobjOptSystem As eGeneral.Opt_system
	'-Se definen las constantes globales para el manejo del tipo de numeración
	
	Public Enum TypeNumeratorPOL_REC
		cstrSysNumeGeneral = 1 'General
		cstrSysNumeBranch = 2 'Ramo
		cstrSysNumeBranchProduct = 3 'Ramo/Producto
	End Enum
	
	'-Se definen las constantes globales para el manejo del tipo de registro
	
	Public Enum TypeRecord
		cstrRequest = 1 'Solicitud
		cstrPolicy = 2 'Póliza
		cstrQuotation = 3 'Cotización
	End Enum
	'-Se definen las constantes globales para el manejo del estado del recibo
	
	Enum StatusReceipt
		clngPendent = 1 'Pendiente
		clngCollectedReturned = 2 'Cobrado/Devuelto
		clngAnnuled = 3 'Anulado
		clngLodgedPendent = 4 'Domiciliado/Pendiente
		clngLodgedCollected = 5 'Domiciliado/Cobrado
		clngCollectedPayPact = 6 'Cobro/Convenio de pago
		clngFinanced = 8 'Financiado
	End Enum
	
	'-Se definen las constantes globales para el manejo del cobro/devolución
	
	Enum Collec_Devolu
		clngReceptable = 1 'Cobro
		clngToReturn = 2 'Devolución
	End Enum
	
	'- Se define la variable que contiene la transacción que se ejecuta en cierto momento.
	Public Enum eFinanceTransac
		eftAddContrat = 1
		eftQuerycontrat = 2
		eftUpDateContrat = 3
		eftRecoveryContrat = 4
	End Enum
	
	'+ Propiedades según la tabla en el sistema el 18/08/1999.
	'+ Los campos llaves corresponden a nCertype, nReceipt, nDigit, nPaynumbe, nBranch y nProduct.
	
	'+  Column name                Type                            Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+  -------------------------  ------------------------------- ------ ----- ----- -------- ------------------ ---------------------
	Public sCertype As String 'char       1                  no       yes                no
	Public nReceipt As Double 'int        4      10    0     no       (n/a)              (n/a)
	Public nDigit As Integer 'smallint   2      5     0     no       (n/a)              (n/a)
	Public nPaynumbe As Integer 'smallint   2      5     0     no       (n/a)              (n/a)
	Public sClient As String 'char       14                 no       yes                no
	Public sCessions As String 'char       1                  yes      yes                yes
	Public sDirdebit As String 'char       1                  yes      yes                yes
	Public sLeadinvo As String 'char       12                 yes      yes                yes
	Public sManauti As String 'char       1                  yes      yes                yes
	Public sRenewal As String 'char       1                  yes      yes                yes
	Public sStatusva As String 'char       1                  yes      yes                yes
	Public sSubstiti As String 'char       1                  yes      yes                yes
	Public sConColl As String 'char       1                  yes      yes                yes
	Public dCompdate As Date 'datetime   8                  yes      (n/a)              (n/a)
	Public dEffecdate As Date 'datetime   8                  yes      (n/a)              (n/a)
	Public dExpirDat As Date 'datetime   8                  yes      (n/a)              (n/a)
	Public dIssuedat As Date 'datetime   8                  yes      (n/a)              (n/a)
	Public dNulldate As Date 'datetime   8                  yes      (n/a)              (n/a)
	Public dPayDate As Date 'datetime   8                  yes      (n/a)              (n/a)
	Public dStatdate As Date 'datetime   8                  yes      (n/a)              (n/a)
	Public nBalance As Double 'decimal    6      12    2     yes      (n/a)              (n/a)
	Public nComamou As Double 'decimal    6      10    2     yes      (n/a)              (n/a)
	Public nExchange As Double 'decimal    6      10    6     yes      (n/a)              (n/a)
	Public nIntammou As Double 'decimal    6      10    2     yes      (n/a)              (n/a)
	Public nParticip As Double 'decimal    4      5     2     yes      (n/a)              (n/a)
	Public nPremium As Double 'decimal    6      10    2     yes      (n/a)              (n/a)
	Public nPremiuml As Double 'decimal    6      10    2     yes      (n/a)              (n/a)
	Public nPremiumn As Double 'decimal    6      10    2     yes      (n/a)              (n/a)
	Public nPremiums As Double 'decimal    6      10    2     yes      (n/a)              (n/a)
	Public nRate As Double 'decimal    3      4     2     yes      (n/a)              (n/a)
	Public nTaxamou As Double 'decimal    6      10    2     yes      (n/a)              (n/a)
	Public nCollecto As Integer 'int        4      10    0     yes      (n/a)              (n/a)
	Public nContrat As Double 'int        4      10    0     yes      (n/a)              (n/a)
	Public nInspecto As Integer 'int        4      10    0     yes      (n/a)              (n/a)
	Public nIntermed As Double 'int        4      10    0     yes      (n/a)              (n/a)
	Public nPolicy As Double 'int        4      10    0     no       (n/a)              (n/a)
	Public nSustit As Integer 'int        4      10    0     yes      (n/a)              (n/a)
	Public nTransactio As Integer 'int        4      10    0     yes      (n/a)              (n/a)
	Public nStatus_pre As StatusReceipt
	Public nNullcode As Integer 'smallint   2      5     0     no       (n/a)              (n/a)
	Public nCurrency As Integer 'smallint   2      5     0     yes      (n/a)              (n/a)
	Public nNoteNum As Double 'int        4      10    0     yes      (n/a)              (n/a)
	Public nOffice As Integer 'smallint   2      5     0     no       (n/a)              (n/a)
	Public nType As Collec_Devolu
	Public nBranch As Integer 'smallint   2      5     0     no       (n/a)              (n/a)
	Public nTratypei As Integer 'smallint   2      5     0     yes      (n/a)              (n/a)
	Public nProduct As Integer 'smallint   2      5     0     no       (n/a)              (n/a)
	Public nUsercode As Integer 'smallint   2      5     0     yes      (n/a)              (n/a)
	Public nPeriod As Integer 'smallint   2      5     0     yes      (n/a)              (n/a)
	Public nCompany As Integer 'smallint   2      5     0     yes      (n/a)              (n/a)
	Public sOrigReceipt As String 'char       20                 yes      yes                yes
	Public nWay_Pay As Integer 'int        2      5     0     yes      (n/a)              (n/a)
	Public dLimitdate As Date 'datetime   8                  yes      (n/a)              (n/a)
	Public nBulletins As Double 'int        4      10    0     yes      (n/a)              (n/a)
	Public nCod_Agree As Integer 'int        2      5     0     yes      (n/a)              (n/a)
	Public dCollSus_ini As Date 'datetime   8                  yes      (n/a)              (n/a)
	Public dCollSus_end As Date 'datetime   8                  yes      (n/a)              (n/a)
	Public sSus_origi As String 'char       1                  yes      yes                yes
	Public nSus_reason As Integer 'smallint   2      5     0     yes      (n/a)              (n/a)
	Public nInsur_area As Integer 'int        2      5     0     yes      (n/a)              (n/a)
	Public nCollector As Double 'int        4      10    0     yes      (n/a)              (n/a)
	Public nIndRecDep As Integer 'int        4      10    0     yes      (n/a)              (n/a)
	Public nReceipt_Max As Double 'int        4      10    0     no       (n/a)              (n/a)
	Public nReceipt_Min As Double 'int        4      10    0     no       (n/a)              (n/a)
	Public nContrat_Max As Double 'int        4      10    0     no       (n/a)              (n/a)
	Public nContrat_Min As Double 'int        4      10    0     no       (n/a)              (n/a)
	Public nRecrelatedcoll As Double

	'- Propiedades auxiliares
	Public sDescWay_Pay As String
	Public nCount As Integer
	Public sCliename As String
	Public sDigit As String
	Public sCurrency As String
	Public sDesType As String
	Public sDesOffice As String
	Public sDesBranch As String
	Public sDesProduct As String
	Public sDesCurrency As String
	Public sDesStatus_pre As String
	Public sDesTratypei As String
	Public sDesPayFreq As String
	Public sDesNullcodp As String
	Public nBankext As Double
	Public nTyp_crecard As Integer
	Public sTyp_dirdebit As String
	Public sDes_Bankext As String
	Public sDes_Typ_crecard As String
	Public sClienameGestor As String
	Public sClienameProductor As String
	Public sClienameSupervis As String
	Public nSupervis As Double
	Public nAmountP As Double
	Public nAmountS As Double
	Public nInt_mora As Double
	Public sOriginal As String
	Public sClient2 As String
	Public sCompany As String
	Public nOfficeIns As Integer
	Public sOfficeIns As String
	Public sDescProd As String
	Public blnError As Boolean
	Public sNumForm As String
	Public sTypePolicy As String
	Public nCertif As Double
	Public nFlag As Integer
	Public nFlag1 As Integer
	Public sAction As String
	Public nCard_type As Integer
	Public sCard_num As String
	Public sAux_accoun As String
	Public nBank_code As Double
	Public nBordereaux As Double
	Public dCar_datexp As Date
	Public nCause_amen As Integer
	Public sChang_acc As String
	Public sDocnumbe As String
	Public sInd_rever As String
	Public sPay_form As String
	Public nReceipt_fa As String
	Public dLedgerdat As Date
	Public sCadena As String
	Public dRescuedate As Date
	Public sAffect As String
	Public nDaysPend As Integer
	Public nNotice As Integer
	Public nDraft As Integer
	Public nStat_draft As Integer
	Public nAmount As Double
	Public nProponum As Double
	Public nAmount_Tot As Double
	Public nPayfreq As Integer
	Public nAmount_Int As Double
	Public nId_NumPay As Integer
	Public nBank_Agree As Integer
	Public sAcc_Number As String
	Public sDep_Number As String
	Public nAmount_PAC As Double
	Public nAcc_Bank As Integer
	Public nAccount As Integer
	Public nId_Register As Integer
	Public nCommission As Double
	Public nAgency As Integer
	Public nMovement As Integer
	Public nDocument As Double
	Public dExpirdatbon As Date
	Public dIssuedatbon As Date
	Public nRate_disc As Double
	Public nNom_valbon As Double
    Public nOrigin As Integer 
	
	'- Variables auxiliares (COC006)
	Public nIntAmmouPay As Double
	Public nRatePay As Double
	
	'- Variables auxiliares (COL502)
	Public nAmountDoc As Double
	Public nAmountDif As Double
	
	'- Variables auxiliares (COC006)
	Public sDescTratypei As String
	Public sDescStatus_pre As String
	Public sDescCard_type As String
	Public sDescCurrency As String

    Public nIntertyp As Integer

    Public sSerie As String 'fase ii reco. ing ehh

    Private Structure udtReceipts
		Dim nReceipt As Double
	End Structure
	
	'- Propiedades auxiliares (CA017)
	Public sPolitype As String
	Public sColinvot As String
	Public sListReceipt As String
	Public nReceiptdefault As Double
	Public nComission As Double
	
	'- Propiedades auxiliares (CA017A)
	Public nPremiumP As Double
	Public nPremiumT As Double
	Public nInitial As Double
	Public nQuota As Double
	Public nQuotaPend As Double
	Public nValQuota As Double
	Public bQuota_Dis As Boolean
	Public bInitial_Dis As Boolean
	Public dFirst_draf As Date
	
	'-variable usada en el find_coc679 de la coleccion premiums
	Public mlngRows As Integer
	
	'- Tipo enumerado para el tipo de acción que se ejecuta sobre la póliza (Table221)
	Enum PolTransac
		clngPolicyIssue = 1 'Emision de Poliza
		clngCertifIssue = 2 'Emision de Certificado
		clngRecuperation = 3 'Recuperacion
		clngPolicyQuotation = 4 'Cotizacion de Poliza
		clngCertifQuotation = 5 'Cotizacion de Certificado
		clngPolicyProposal = 6 'Propuesta de Poliza
		clngCertifProposal = 7 'Propuesta de Certificado
		clngPolicyQuery = 8 'Consulta de Poliza"
		clngCertifQuery = 9 'Consulta de Certificado
		clngQuotationQuery = 10 'Consulta de Cotizacion
		clngProposalQuery = 11 'Consulta de Solicitud
		clngPolicyAmendment = 12 'Modificacion Normal de Poliza
		clngTempPolicyAmendment = 13 'Modificacion Temporal de Poliza
		clngCertifAmendment = 14 'Modificacion de Certificado
		clngTempCertifAmendment = 15 'Modificacion Temporal de Certificados
		clngQuotationConvertion = 16 'Conversion de Cotizacion a Poliza
		clngProposalConvertion = 17 'Conversion de Propuesta a Poliza
		clngPolicyReissue = 18 'Re-emision de Poliza
		clngCertifReissue = 19 'Re-emision de Certificado
		clngReprint = 20 'Re-impresion
		clngdeclarations = 21 'Declaraciones
		clngCoverNote = 22 'Nota de Cobertura
		clngPropQuotConvertion = 23 'Conversion de Cotización a Propuesta
        clngPolicyQuotAmendent = 24 'Cotización de Modificación de póliza
        clngCertifQuotAmendent = 25 'Cotización de Modificación de certificado
        clngPolicyPropAmendent = 26 'Propuesta de Modificación de póliza
        clngCertifPropAmendent = 27 'Propuesta de Modificación de certificado
        clngPolicyQuotRenewal = 28 'Cotización de Renovación de póliza
        clngCertifQuotRenewal = 29 'Cotización de Renovación de certificado
        clngPolicyPropRenewal = 30 'Propuesta de Renovación de póliza
        clngCertifPropRenewal = 31 'Propuesta de Renovación de Certificado
        clngInspections = 32 'Inspecciones
        clngQuotAmendConvertion = 33 'Conversión Cotizacion de Modificación a modificación
        clngPropAmendConvertion = 34 'Conversión Propuesta de Modificación a modificación
        clngQuotRenewalConvertion = 35 'Conversión Cotización de Renovación a póliza
        clngPropRenewalConvertion = 36 'Conversión Propuesta de Renovación a póliza
        clngQuotPropAmendentConvertion = 37 'Conversión Cotizacion de Modificación a Propuesta de Modificación
        clngQuotPropRenewalConvertion = 38 'Conversión Cotizacion de Renovación a Propuesta de Renovación
        clngQuotAmendentQuery = 39 'Consulta de Cotización de Modificación
        clngPropAmendentQuery = 40 'Consulta de Propuesta de Modificación
        clngQuotRenewalQuery = 41 'Consulta de Cotización de Renovación
        clngPropRenewalQuery = 42 'Consulta de Propuesta de Renovación
	End Enum
	
	Public mobjPremium As Object
	
	'- Variables para el manejo de errores de las páginas, controladas por el método inspre[Codispl]
	Public bError As Boolean
	Public nErrornum As Integer
	
	Public sGeneralNumerator As TypeNumeratorPOL_REC
	Public nTypePremium As Integer
	
	Public sPrint As String
	
	Public nProdClas As Integer
	Public sApv As String
	
	
	Private mstrBrancht As String
	
	Private arrReceipts() As udtReceipts
	
	'- Arreglo para la carga de recibos
	Private marrReceipts() As Integer
	
	'- Arreglo para la carga de las monedas utilizadas por los recibos
	Private marrCurr() As Integer
	
	'- Indica si el arreglo de recibos se cargo o no
	Private mblnCharge As Boolean
	
	'- Arreglo para la carga de boletines asociados a un recibo o cuota
	Private marrBulletins() As Double
	
	
	'% Class_Initialize: Se inicializan las variables de la clase.
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Me.sCertype = String.Empty
		Me.nReceipt = eRemoteDB.Constants.intNull
		Me.nDigit = eRemoteDB.Constants.intNull
		Me.nPaynumbe = eRemoteDB.Constants.intNull
		Me.sClient = String.Empty
		Me.sCessions = String.Empty
		Me.sDirdebit = String.Empty
		Me.sLeadinvo = String.Empty
		Me.sManauti = String.Empty
		Me.sRenewal = String.Empty
		Me.sStatusva = String.Empty
		Me.sSubstiti = String.Empty
		Me.sConColl = String.Empty
		Me.dCompdate = eRemoteDB.Constants.dtmNull
		Me.dEffecdate = eRemoteDB.Constants.dtmNull
		Me.dExpirDat = eRemoteDB.Constants.dtmNull
		Me.dIssuedat = eRemoteDB.Constants.dtmNull
		Me.dNulldate = eRemoteDB.Constants.dtmNull
		Me.dPayDate = eRemoteDB.Constants.dtmNull
		Me.dStatdate = eRemoteDB.Constants.dtmNull
		Me.nBalance = eRemoteDB.Constants.intNull
		Me.nComamou = eRemoteDB.Constants.intNull
		Me.nExchange = eRemoteDB.Constants.intNull
		Me.nIntammou = eRemoteDB.Constants.intNull
		Me.nParticip = eRemoteDB.Constants.intNull
		Me.nPremium = eRemoteDB.Constants.intNull
		Me.nPremiuml = eRemoteDB.Constants.intNull
		Me.nPremiumn = eRemoteDB.Constants.intNull
		Me.nPremiums = eRemoteDB.Constants.intNull
		Me.nRate = eRemoteDB.Constants.intNull
		Me.nTaxamou = eRemoteDB.Constants.intNull
		Me.nCollecto = eRemoteDB.Constants.intNull
		Me.nContrat = eRemoteDB.Constants.intNull
		Me.nInspecto = eRemoteDB.Constants.intNull
		Me.nIntermed = eRemoteDB.Constants.intNull
		Me.nPolicy = eRemoteDB.Constants.intNull
		Me.nSustit = eRemoteDB.Constants.intNull
		Me.nTransactio = eRemoteDB.Constants.intNull
		Me.nNullcode = eRemoteDB.Constants.intNull
		Me.nCurrency = eRemoteDB.Constants.intNull
		Me.nNoteNum = eRemoteDB.Constants.intNull
		Me.nOffice = eRemoteDB.Constants.intNull
		Me.nBranch = eRemoteDB.Constants.intNull
		Me.nTratypei = eRemoteDB.Constants.intNull
		Me.nProduct = eRemoteDB.Constants.intNull
		Me.nUsercode = eRemoteDB.Constants.intNull
		Me.nPeriod = eRemoteDB.Constants.intNull
		Me.nCompany = eRemoteDB.Constants.intNull
		Me.sOrigReceipt = String.Empty
		Me.nWay_Pay = eRemoteDB.Constants.intNull
		Me.dLimitdate = eRemoteDB.Constants.dtmNull
		Me.nBulletins = eRemoteDB.Constants.intNull
		Me.nCod_Agree = eRemoteDB.Constants.intNull
		Me.dCollSus_ini = eRemoteDB.Constants.dtmNull
		Me.dCollSus_end = eRemoteDB.Constants.dtmNull
		Me.sSus_origi = String.Empty
		Me.nSus_reason = eRemoteDB.Constants.intNull
		Me.nInsur_area = eRemoteDB.Constants.intNull
		Me.nCollector = eRemoteDB.Constants.intNull
		Me.nIndRecDep = eRemoteDB.Constants.intNull
		
		Me.sDescWay_Pay = String.Empty
		Me.nCount = eRemoteDB.Constants.intNull
		Me.sCliename = String.Empty
		Me.sCurrency = String.Empty
		Me.sDesType = String.Empty
		Me.sDesOffice = String.Empty
		Me.sDesBranch = String.Empty
		Me.sDesProduct = String.Empty
		Me.sDesCurrency = String.Empty
		Me.sDesStatus_pre = String.Empty
		Me.sDesTratypei = String.Empty
		Me.sDesPayFreq = String.Empty
		Me.sDesNullcodp = String.Empty
		Me.nBankext = eRemoteDB.Constants.intNull
		Me.nTyp_crecard = eRemoteDB.Constants.intNull
		Me.sTyp_dirdebit = String.Empty
		Me.sDes_Bankext = String.Empty
		Me.sDes_Typ_crecard = String.Empty
		Me.sClienameGestor = String.Empty
		Me.sClienameProductor = String.Empty
		Me.sClienameSupervis = String.Empty
		Me.nSupervis = eRemoteDB.Constants.intNull
		Me.nAmountP = eRemoteDB.Constants.intNull
		Me.nAmountS = eRemoteDB.Constants.intNull
		Me.nInt_mora = eRemoteDB.Constants.intNull
		Me.sOriginal = String.Empty
		Me.sClient2 = String.Empty
		Me.sCompany = String.Empty
		Me.nOfficeIns = eRemoteDB.Constants.intNull
		Me.sOfficeIns = String.Empty
		Me.sDescProd = String.Empty
		Me.sNumForm = String.Empty
		Me.sTypePolicy = String.Empty
		Me.nCertif = eRemoteDB.Constants.intNull
		Me.nFlag = eRemoteDB.Constants.intNull
		Me.nFlag1 = eRemoteDB.Constants.intNull
		Me.sAction = String.Empty
		Me.nCard_type = eRemoteDB.Constants.intNull
		Me.sCard_num = String.Empty
		Me.sAux_accoun = String.Empty
		Me.nBank_code = eRemoteDB.Constants.intNull
		Me.nBordereaux = eRemoteDB.Constants.intNull
		Me.dCar_datexp = eRemoteDB.Constants.dtmNull
		Me.nCause_amen = eRemoteDB.Constants.intNull
		Me.sChang_acc = String.Empty
		Me.sDocnumbe = String.Empty
		Me.sInd_rever = String.Empty
		Me.sPay_form = String.Empty
		Me.nReceipt_fa = CStr(eRemoteDB.Constants.intNull)
		Me.dLedgerdat = eRemoteDB.Constants.dtmNull
		Me.sCadena = String.Empty
		Me.dRescuedate = eRemoteDB.Constants.dtmNull
		Me.sAffect = String.Empty
		Me.nDaysPend = eRemoteDB.Constants.intNull
		Me.nNotice = eRemoteDB.Constants.intNull
		Me.nDraft = eRemoteDB.Constants.intNull
		Me.nStat_draft = eRemoteDB.Constants.intNull
		Me.nAmount = eRemoteDB.Constants.intNull
		Me.nProponum = eRemoteDB.Constants.intNull
		Me.nAmount_Tot = eRemoteDB.Constants.intNull
		Me.nPayfreq = eRemoteDB.Constants.intNull
		Me.nAmount_Int = eRemoteDB.Constants.intNull
		Me.nId_NumPay = eRemoteDB.Constants.intNull
		Me.nBank_Agree = eRemoteDB.Constants.intNull
		Me.sAcc_Number = String.Empty
		Me.sDep_Number = String.Empty
		Me.nAmount_PAC = eRemoteDB.Constants.intNull
		Me.nAcc_Bank = eRemoteDB.Constants.intNull
		Me.nAccount = eRemoteDB.Constants.intNull
		Me.nId_Register = eRemoteDB.Constants.intNull
		Me.nCommission = eRemoteDB.Constants.intNull
		Me.nAgency = eRemoteDB.Constants.intNull
		
		Me.sDescTratypei = String.Empty
		Me.sDescStatus_pre = String.Empty
		Me.sDescCard_type = String.Empty
		Me.sDescCurrency = String.Empty

		Me.nIntertyp = eRemoteDB.Constants.intNull
        Me.nRecrelatedcoll = CStr(eRemoteDB.Constants.intNull)

        Me.sSerie = String.Empty

        bError = False
		nErrornum = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Find: Busca los datos correspondiente a un recibo en la tabla Premium.
	Public Function Find(ByVal certype As String, ByVal Receipt As Double, ByVal branch As Integer, ByVal product As Integer, ByVal Digit As Integer, ByVal Paynumbe As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPremium_Receipt As eRemoteDB.Execute
		
		lrecreaPremium_Receipt = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If (certype = sCertype And Receipt = nReceipt And branch = nBranch And product = nProduct And Digit = nDigit And Paynumbe = nPaynumbe) Or lblnFind Then
			Find = True
		Else
			
			'+ Definición de parámetros para stored procedure 'insudb.reaPremiumF_Receipt'
			'+ Información leída el 23/09/1999 1:02:48 PM
			With lrecreaPremium_Receipt
				.StoredProcedure = "insreaPremium_Receiptpkg.insreaPremium_Receipt"
				.Parameters.Add("sCertype", certype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nReceipt", Receipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", branch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", product, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDigit", Digit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPaynumbe", Paynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					sCertype = .FieldToClass("sCertype")
					nReceipt = .FieldToClass("nReceipt")
					nDigit = .FieldToClass("nDigit")
					nPaynumbe = .FieldToClass("nPaynumbe")
					sClient = .FieldToClass("sClient")
					sCessions = .FieldToClass("sCessions")
					sDirdebit = .FieldToClass("sDirdebit")
					sLeadinvo = .FieldToClass("sLeadinvo")
					sManauti = .FieldToClass("sManauti")
					sRenewal = .FieldToClass("sRenewal")
					sStatusva = .FieldToClass("sStatusva")
					sSubstiti = .FieldToClass("sSubstiti")
					sConColl = .FieldToClass("sConColl")
					dEffecdate = .FieldToClass("dEffecdate")
					dExpirDat = .FieldToClass("dExpirdat")
					dIssuedat = .FieldToClass("dIssuedat")
					dNulldate = .FieldToClass("dNulldate")
					dPayDate = .FieldToClass("dPaydate")
					dStatdate = .FieldToClass("dStatdate")
					nBalance = .FieldToClass("nBalance")
					nComamou = .FieldToClass("nComamou")
					nExchange = .FieldToClass("nExchange")
					nIntammou = .FieldToClass("nIntammou")
					nParticip = .FieldToClass("nParticip")
					nPremium = .FieldToClass("nPremium")
					nPremiuml = .FieldToClass("nPremiuml")
					nPremiumn = .FieldToClass("nPremiumn")
					nPremiums = .FieldToClass("nPremiums")
					nRate = .FieldToClass("nRate")
					nTaxamou = .FieldToClass("nTaxamou")
					nCollecto = .FieldToClass("nCollecto")
					nContrat = .FieldToClass("nContrat")
					nInspecto = .FieldToClass("nInspecto")
					nIntermed = .FieldToClass("nIntermed")
					nPolicy = .FieldToClass("nPolicy")
					nSustit = .FieldToClass("nSustit")
					nTransactio = .FieldToClass("nTransactio")
					nStatus_pre = .FieldToClass("nStatus_pre")
					nNullcode = .FieldToClass("nNullcode")
					nCurrency = .FieldToClass("nCurrency")
					nNoteNum = .FieldToClass("nNotenum")
					nOffice = .FieldToClass("nOffice")
					nType = .FieldToClass("nType")
					nBranch = .FieldToClass("nBranch")
					nTratypei = .FieldToClass("nTratypei")
					nProduct = .FieldToClass("nProduct")
					nUsercode = .FieldToClass("nUsercode")
					nPeriod = .FieldToClass("nPeriod")
					nCompany = .FieldToClass("nCompany")
					sOrigReceipt = .FieldToClass("sOrigReceipt")
					sCliename = .FieldToClass("sCliename")
					sCurrency = .FieldToClass("sDescript")
					nCertif = .FieldToClass("nCertif")
					nProponum = .FieldToClass("nProponum", eRemoteDB.Constants.intNull)
					nBulletins = .FieldToClass("nBulletins", eRemoteDB.Constants.intNull)
					dCollSus_ini = .FieldToClass("dCollsus_ini")
					dCollSus_end = .FieldToClass("dCollsus_end")
					nSus_reason = .FieldToClass("nSus_reason")
					sSus_origi = .FieldToClass("sSus_origi")
					nInsur_area = .FieldToClass("nInsur_area", eRemoteDB.Constants.intNull)
					nCollector = .FieldToClass("nCollector", eRemoteDB.Constants.intNull)
					sDigit = .FieldToClass("sDigit")
					sDesBranch = .FieldToClass("sDescBranch")
					sDescProd = .FieldToClass("sDescProduct")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaPremium_Receipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaPremium_Receipt = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremium_Receipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_Receipt = Nothing
	End Function
	
	'% Find_sCliename: busca la descripción de un cliente de una compañía en particular,
	'%                 perteneciente a un recibo determinado
	Public Function Find_sCliename(ByVal Receipt As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPremium_Company As eRemoteDB.Execute
		
		lrecreaPremium_Company = New eRemoteDB.Execute
		
		On Error GoTo Find_sCliename_Err
		
		If Receipt = nReceipt Or lblnFind Then
			Find_sCliename = True
		Else
			
			'+ Definición de parámetros para stored procedure 'insudb.reaPremium_Company'
			'+ Información leída el 03/09/1999 04:32:04 PM
			
			With lrecreaPremium_Company
				.StoredProcedure = "reaPremium_Company"
				.Parameters.Add("nReceipt", Receipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If .FieldToClass("nCompany") Is System.DBNull.Value Then
						sCliename = .FieldToClass("sCliename")
						nCompany = .FieldToClass("nCompany")
						Find_sCliename = True
					Else
						Find_sCliename = False
					End If
					.RCloseRec()
				Else
					Find_sCliename = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaPremium_Company may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaPremium_Company = Nothing
		End If
		
Find_sCliename_Err: 
		If Err.Number Then
			Find_sCliename = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremium_Company may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_Company = Nothing
	End Function
	
	'%Find_DataReceipt:Esta función permite leer la información correspondiente de la tabla Premium
	'%Información general de los recibos, siempre y cuando se le indique el número del recibo.
	Public Function Find_DataReceipt(ByVal sCertype As String, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, ByVal sPolicyNum As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nOrigReceipt As Integer, ByVal nTypePremium As Integer, Optional ByRef lblnFind As Boolean = False) As Boolean
		
		Dim lrecReaPremiumData As eRemoteDB.Execute
		lrecReaPremiumData = New eRemoteDB.Execute
		
		Find_DataReceipt = False
		
		On Error GoTo Find_DataReceipt_Err
		
		If (nBranch = Me.nBranch Or nReceipt = Me.nReceipt) And Not lblnFind Then
			Find_DataReceipt = True
		Else
			
			'Definición de parámetros para stored procedure 'insudb.reaPremium_Company'
			'Información leída el 03/09/1999 04:32:04 PM
			
			Me.nBranch = nBranch
			Me.nReceipt = nReceipt
			
			With lrecReaPremiumData
				.StoredProcedure = "insreaCOC003"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sTypeNumeraP", sPolicyNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sOrigReceipt", sOrigReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTypeReceipt", nTypePremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nCount = 1
					sCertype = sCertype
					nReceipt = .FieldToClass("nReceipt")
					dEffecdate = .FieldToClass("dEffecdate")
					dExpirDat = .FieldToClass("dExpirdat")
					nBranch = .FieldToClass("nBranch")
					nProduct = .FieldToClass("nProduct")
					nPolicy = .FieldToClass("nPolicy")
					dStatdate = .FieldToClass("dStatdate")
					nBalance = .FieldToClass("nBalance")
					sClient = .FieldToClass("sClient")
					nCollecto = .FieldToClass("nCollecto")
					sDirdebit = .FieldToClass("sDirdebit")
					nIntermed = .FieldToClass("nIntermed", 0)
					nPremium = .FieldToClass("nPremium", 0)
					nPremiumn = .FieldToClass("nPremiumn", 0)
					sCliename = .FieldToClass("sCliename")
					sDesType = .FieldToClass("sDesType")
					sDesOffice = .FieldToClass("sDesOffice")
					sDesBranch = .FieldToClass("sDesBranch")
					sDesProduct = .FieldToClass("sDesProduct")
					sDesCurrency = .FieldToClass("sDesCurrency")
					sDesStatus_pre = .FieldToClass("sDesStatus_pre")
					sDesTratypei = .FieldToClass("sDesTratypei")
					sDesPayFreq = .FieldToClass("sDesPayFreq")
					sDesNullcodp = .FieldToClass("sDesNullcodp")
					nBankext = .FieldToClass("nBankext", 0)
					sTyp_dirdebit = .FieldToClass("sTyp_dirdeb")
					sDes_Bankext = .FieldToClass("sDes_Bankext")
					sDes_Typ_crecard = .FieldToClass("sDes_Typ_crecard")
					sClienameGestor = .FieldToClass("sClienameGestor")
					sClienameProductor = .FieldToClass("sClienameProductor")
					sClienameSupervis = .FieldToClass("sClienameSupervis")
					nSupervis = .FieldToClass("nSupervis")
					nAmountP = .FieldToClass("nAmountP")
					nAmountS = .FieldToClass("nAmountS")
					nInt_mora = .FieldToClass("nInt_mora")
					sOriginal = .FieldToClass("sOriginal")
					sOrigReceipt = .FieldToClass("sOrigReceipt")
					nCompany = .FieldToClass("nCompany", 0)
					sClient2 = .FieldToClass("sClient2")
					sCompany = .FieldToClass("sCompany")
					nOfficeIns = .FieldToClass("nOfficeIns", 0)
					sDescProd = .FieldToClass("sDescProd")
					dIssuedat = .FieldToClass("dIssueDat")
					nTaxamou = .FieldToClass("nTaxamou")
					
					Find_DataReceipt = True
					.RCloseRec()
				Else
					nCount = 0
				End If
			End With
		End If
		
Find_DataReceipt_Err: 
		If Err.Number Then
			Find_DataReceipt = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaPremiumData may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaPremiumData = Nothing
	End Function
	
	'% FindClientReceipt: Devuelve información para evaluar si un cliente posee o no recibos
	Public Function FindClientReceipt(ByVal strCertype As String, ByVal strClient As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		
		Static lblnRead As Boolean
		
		'- Se define la variable lrecreaPremium
		
		Dim lrecreaPremium As eRemoteDB.Execute
		
		lrecreaPremium = New eRemoteDB.Execute
		
		On Error GoTo FindClientReceipt_Err
		
		If sCertype <> strCertype Or sClient <> strClient Or lblnFind Then
			
			sCertype = strCertype
			sClient = strClient
			
			'+ Definición de parámetros para stored procedure 'insudb.reaPremium'
			'+ Información leída el 03/11/2000 10:36:40 AM
			
			With lrecreaPremium
				.StoredProcedure = "reaPremium"
				.Parameters.Add("sCertype", strCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", strClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					nPremium = .FieldToClass("nPremium")
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		
		FindClientReceipt = lblnRead
		
FindClientReceipt_Err: 
		If Err.Number Then
			FindClientReceipt = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium = Nothing
	End Function
	
	'%Find_Receipt_COC003: Permite leer los datos de un recibo
	Public Function Find_Receipt_COC003(ByVal nReceipt As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPremium As eRemoteDB.Execute
		
		On Error GoTo Find_Receipt_COC003_Err
		
		Find_Receipt_COC003 = True
		
		lrecreaPremium = New eRemoteDB.Execute
		'+Definición de parámetros para stored procedure 'insudb.reaPremium_COC003'
		With lrecreaPremium
			.StoredProcedure = "reaPremium_COC003"
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.dExpirDat = .FieldToClass("dExpirDat")
				Me.nType = .FieldToClass("nType")
				Me.nTratypei = .FieldToClass("nTratypei")
				Me.nPayfreq = .FieldToClass("nPayFreq")
				Me.nBalance = .FieldToClass("nBalance")
				Me.nCurrency = .FieldToClass("nCurrency_Bal")
				Me.nNullcode = .FieldToClass("nNullCode")
				Me.nStatus_pre = .FieldToClass("nStatus_Pre")
				Me.dStatdate = .FieldToClass("dStatdate")
				Me.nContrat = .FieldToClass("nContrat")
				Me.nWay_Pay = .FieldToClass("nWay_Pay")
				Me.nBulletins = .FieldToClass("nBulletins")
				Me.nOffice = .FieldToClass("nOffice")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.sClient = .FieldToClass("sClient")
				Me.nAmount_Int = .FieldToClass("nAmount_Int")
				Me.nIntermed = .FieldToClass("nIntermed")
				Me.nCollecto = .FieldToClass("nCollecto")
				Me.nPremium = .FieldToClass("nPremium")
				Me.nPremiumn = .FieldToClass("nPremiumn")
				Me.nInt_mora = .FieldToClass("nInt_Mora")
				Find_Receipt_COC003 = True
			Else
				Find_Receipt_COC003 = False
			End If
		End With
Find_Receipt_COC003_Err: 
		If Err.Number Then
			Find_Receipt_COC003 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium = Nothing
	End Function
	
	'%me: Esta rutina permite validar si el recibo ingresado
	'%existe en la tabla Premium (Información general del recibo), para mandar el mensaje
	'%de error correspondiente.
	Public Function FindPremiumExist(ByVal lstrCertype As String, ByVal llngBranch As Integer, ByVal llngProduct As Integer, ByVal llngReceipt As Double, ByVal llngDigit As Integer, ByVal llngPaynumbe As Integer, ByVal lstrGeneralNumerator As TypeNumeratorPOL_REC, Optional ByRef lintTypePremium As Integer = 0, Optional ByVal lstrOrigReceipt As String = "", Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecvalPremiumExists As eRemoteDB.Execute
		
		On Error GoTo FindPremiumExist_Err
		
		If lstrCertype = Me.sCertype And llngBranch = Me.nBranch And llngProduct = Me.nProduct And llngReceipt = Me.nReceipt And llngDigit = Me.nDigit And llngPaynumbe = Me.nPaynumbe And lstrGeneralNumerator = Me.sGeneralNumerator And lintTypePremium = Me.nTypePremium And lstrOrigReceipt = Me.sOrigReceipt And Not bFind Then
			FindPremiumExist = True
		Else
			lrecvalPremiumExists = New eRemoteDB.Execute
			
			With lrecvalPremiumExists
				.StoredProcedure = "reaPremiumExists"
				.Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", llngBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", llngProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nReceipt", IIf(lintTypePremium = 0, llngReceipt, 0), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDigit", llngDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPayNumbe", llngPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sOrigReceipt", IIf(lintTypePremium = 0, 0, lstrOrigReceipt), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sGeneralNum", lstrGeneralNumerator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					blnError = True
					FindPremiumExist = True
					sCertype = .FieldToClass("sCertype")
					nBalance = .FieldToClass("nBalance", 0)
					nReceipt = .FieldToClass("nReceipt")
					nBranch = .FieldToClass("nBranch")
					nProduct = .FieldToClass("nProduct")
					nPolicy = .FieldToClass("nPolicy")
					dEffecdate = .FieldToClass("dEffecdate")
					dExpirDat = .FieldToClass("dExpirdat", eRemoteDB.Constants.dtmNull)
					dStatdate = .FieldToClass("dStatdate")
					nStatus_pre = .FieldToClass("nStatus_pre")
					nOffice = .FieldToClass("nOffice")
					sClient = .FieldToClass("sClient")
					sCliename = .FieldToClass("sCliename")
					nType = .FieldToClass("nType")
					nTratypei = .FieldToClass("nTratypei")
					nCurrency = .FieldToClass("nCurrency")
					sTypePolicy = .FieldToClass("sPolitype")
					nWay_Pay = .FieldToClass("nWay_Pay")
					dLimitdate = .FieldToClass("dLimitdate")
					sDescWay_Pay = .FieldToClass("sDescWay_Pay")
					nBulletins = .FieldToClass("nBulletins", eRemoteDB.Constants.intNull)
					nCod_Agree = .FieldToClass("nCod_agree")
					nInsur_area = .FieldToClass("nInsur_area")
					nContrat = .FieldToClass("nContrat")
					nProponum = .FieldToClass("nProponum", eRemoteDB.Constants.intNull)
					nDigit = .FieldToClass("nDigit")
					nPaynumbe = .FieldToClass("nPaynumbe")
					dCollSus_ini = .FieldToClass("dCollsus_ini")
					dCollSus_end = .FieldToClass("dCollsus_end")
					nSus_reason = .FieldToClass("nSus_reason")
					sSus_origi = .FieldToClass("sSus_origi")
					nInspecto = .FieldToClass("ninspecto")
					nAgency = .FieldToClass("nAgency")
					nCollecto = .FieldToClass("nCollecto", 0)
					nNullcode = .FieldToClass("nNullcode", 0)
					nPremium = .FieldToClass("nPremium", 0)
					nPremiumn = .FieldToClass("nPremiumn", 0)
					sDirdebit = .FieldToClass("sDirdebit", String.Empty)
					nIntermed = .FieldToClass("nIntermed", 0)
					sNumForm = .FieldToClass("sNumForm", "0")
					nIndRecDep = .FieldToClass("nIndRecDep", eRemoteDB.Constants.intNull)
					nCertif = .FieldToClass("nCertif")
					Me.sGeneralNumerator = lstrGeneralNumerator
					Me.nTypePremium = lintTypePremium
					'se usan en el reverso de cobro (CO09)
					sDesCurrency = .FieldToClass("SDESC_CURRENCY")
					sDesOffice = .FieldToClass("SDESC_OFFICE")
					sDesStatus_pre = .FieldToClass("SDESC_NSTATUS_PRE")
					sDesBranch = .FieldToClass("SDESC_BRANCH")
					sDesProduct = .FieldToClass("SDESC_PRODUCT")
					sDescTratypei = .FieldToClass("SDESC_TRATYPEI")
					.RCloseRec()
				Else
					blnError = False
					FindPremiumExist = False
				End If
			End With
		End If
		
FindPremiumExist_Err: 
		If Err.Number Then
			FindPremiumExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecvalPremiumExists may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalPremiumExists = Nothing
    End Function


    '%me: Esta rutina permite validar si el recibo ingresado
    '%existe en la tabla Premium (Información general del recibo), para mandar el mensaje
    '%de error correspondiente // SOLO VENTANA CO009.
    Public Function Findco009(ByVal lstrCertype As String, ByVal llngBranch As Integer, ByVal llngProduct As Integer, ByVal llngReceipt As Double, ByVal llngDigit As Integer, ByVal llngPaynumbe As Integer, ByVal lstrGeneralNumerator As TypeNumeratorPOL_REC, Optional ByRef lintTypePremium As Integer = 0, Optional ByVal lstrOrigReceipt As String = "", Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecvalPremiumExists As eRemoteDB.Execute

        On Error GoTo FindPremiumExist_Err

        If lstrCertype = Me.sCertype And llngBranch = Me.nBranch And llngProduct = Me.nProduct And llngReceipt = Me.nReceipt And llngDigit = Me.nDigit And llngPaynumbe = Me.nPaynumbe And lstrGeneralNumerator = Me.sGeneralNumerator And lintTypePremium = Me.nTypePremium And lstrOrigReceipt = Me.sOrigReceipt And Not bFind Then
            Findco009 = True
        Else
            lrecvalPremiumExists = New eRemoteDB.Execute

            With lrecvalPremiumExists
                .StoredProcedure = "REACO009"
                .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", llngBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", llngProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nReceipt", IIf(lintTypePremium = 0, llngReceipt, 0), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nDigit", llngDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPayNumbe", llngPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sOrigReceipt", IIf(lintTypePremium = 0, 0, lstrOrigReceipt), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sGeneralNum", lstrGeneralNumerator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    blnError = True
                    Findco009 = True
                    sCertype = .FieldToClass("sCertype")
                    nBalance = .FieldToClass("nBalance", 0)
                    nReceipt = .FieldToClass("nReceipt")
                    nBranch = .FieldToClass("nBranch")
                    nProduct = .FieldToClass("nProduct")
                    nPolicy = .FieldToClass("nPolicy")
                    dEffecdate = .FieldToClass("dEffecdate")
                    dExpirDat = .FieldToClass("dExpirdat", eRemoteDB.Constants.dtmNull)
                    dStatdate = .FieldToClass("dStatdate")
                    nStatus_pre = .FieldToClass("nStatus_pre")
                    nOffice = .FieldToClass("nOffice")
                    sClient = .FieldToClass("sClient")
                    sCliename = .FieldToClass("sCliename")
                    nType = .FieldToClass("nType")
                    nTratypei = .FieldToClass("nTratypei")
                    nCurrency = .FieldToClass("nCurrency")
                    sTypePolicy = .FieldToClass("sPolitype")
                    nWay_Pay = .FieldToClass("nWay_Pay")
                    dLimitdate = .FieldToClass("dLimitdate")
                    sDescWay_Pay = .FieldToClass("sDescWay_Pay")
                    nBulletins = .FieldToClass("nBulletins", eRemoteDB.Constants.intNull)
                    nCod_Agree = .FieldToClass("nCod_agree")
                    nInsur_area = .FieldToClass("nInsur_area")
                    nContrat = .FieldToClass("nContrat")
                    nProponum = .FieldToClass("nProponum", eRemoteDB.Constants.intNull)
                    nDigit = .FieldToClass("nDigit")
                    nPaynumbe = .FieldToClass("nPaynumbe")
                    dCollSus_ini = .FieldToClass("dCollsus_ini")
                    dCollSus_end = .FieldToClass("dCollsus_end")
                    nSus_reason = .FieldToClass("nSus_reason")
                    sSus_origi = .FieldToClass("sSus_origi")
                    nInspecto = .FieldToClass("ninspecto")
                    nAgency = .FieldToClass("nAgency")
                    nCollecto = .FieldToClass("nCollecto", 0)
                    nNullcode = .FieldToClass("nNullcode", 0)
                    nPremium = .FieldToClass("nPremium", 0)
                    nPremiumn = .FieldToClass("nPremiumn", 0)
                    sDirdebit = .FieldToClass("sDirdebit", String.Empty)
                    nIntermed = .FieldToClass("nIntermed", 0)
                    sNumForm = .FieldToClass("sNumForm", "0")
                    nIndRecDep = .FieldToClass("nIndRecDep", eRemoteDB.Constants.intNull)
                    nCertif = .FieldToClass("nCertif")
                    Me.sGeneralNumerator = lstrGeneralNumerator
                    Me.nTypePremium = lintTypePremium
                    'se usan en el reverso de cobro (CO09)
                    sDesCurrency = .FieldToClass("SDESC_CURRENCY")
                    sDesOffice = .FieldToClass("SDESC_OFFICE")
                    sDesStatus_pre = .FieldToClass("SDESC_NSTATUS_PRE")
                    sDesBranch = .FieldToClass("SDESC_BRANCH")
                    sDesProduct = .FieldToClass("SDESC_PRODUCT")
                    sDescTratypei = .FieldToClass("SDESC_TRATYPEI")
                    .RCloseRec()
                Else
                    blnError = False
                    Findco009 = False
                End If
            End With
        End If

FindPremiumExist_Err:
        If Err.Number Then
            Findco009 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecvalPremiumExists may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecvalPremiumExists = Nothing
    End Function
	
	'% insValPendPremRehab: Valida que no existan recibos pendientes
	'%      con origen 'Rehabilitacion', asociados a la poliza/certificado
	'%---------------------------------------------------------------
	Public Function insValPendPremRehab(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		'%---------------------------------------------------------------
		Dim lrecinsValcertifpendpremrehab As eRemoteDB.Execute
		
		
		On Error GoTo insValcertifpendpremrehab_Err
		
		lrecinsValcertifpendpremrehab = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insValcertifpendpremrehab al 01-14-2002 18:07:35
		'+
		With lrecinsValcertifpendpremrehab
			.StoredProcedure = "insValCertifPendPremRehab"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insValPendPremRehab = .Parameters("nExists").Value = 0
			End If
		End With
		
insValcertifpendpremrehab_Err: 
		If Err.Number Then
			insValPendPremRehab = False
		End If
		'UPGRADE_NOTE: Object lrecinsValcertifpendpremrehab may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValcertifpendpremrehab = Nothing
		On Error GoTo 0
	End Function
	
	
	'% UpdateClientPremium: Actualiza en la tabla premium el titular del recibo
	Public Function UpdateClientPremium() As Boolean
		'- Se define la variable lrecupdPremium_Client
		
		Dim lrecupdPremium_Client As eRemoteDB.Execute
		
		lrecupdPremium_Client = New eRemoteDB.Execute
		
		On Error GoTo UpdateClientPremium_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.updPremium_Client'
		'+ Información leída el 03/11/2000 04:17:10 PM
		
		With lrecupdPremium_Client
			.StoredProcedure = "updPremium_Client"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateClientPremium = .Run(False)
		End With
		
UpdateClientPremium_Err: 
		If Err.Number Then
			UpdateClientPremium = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdPremium_Client may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdPremium_Client = Nothing
	End Function
	
	'% Find_Receipt:
	Public Function Find_Receipt() As Boolean
		Dim lrecreaPremium_a As eRemoteDB.Execute
		
		lrecreaPremium_a = New eRemoteDB.Execute
		
		On Error GoTo Find_Receipt_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaPremium_a'
		'+ Información leída el 06/11/2000 04:35:06 p.m.
		
		With lrecreaPremium_a
			.StoredProcedure = "reaPremium_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Receipt = True
				nReceipt = .FieldToClass("nReceipt")
				.RCloseRec()
			Else
				Find_Receipt = False
			End If
		End With
		
Find_Receipt_Err: 
		If Err.Number Then
			Find_Receipt = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremium_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_a = Nothing
	End Function
	
	'% valReceiptPayed:
	Public Function valReceiptPayed() As Boolean
		Dim lrecvalReceiptPayed As eRemoteDB.Execute
		
		lrecvalReceiptPayed = New eRemoteDB.Execute
		
		On Error GoTo valReceiptPayed_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.valReceiptPayed'
		'+ Información leída el 06/11/2000 04:40:15 p.m.
		
		With lrecvalReceiptPayed
			.StoredProcedure = "valReceiptPayed"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				valReceiptPayed = True
				nCount = .Parameters.Item("nCount").Value
			Else
				valReceiptPayed = False
			End If
		End With
		
valReceiptPayed_Err: 
		If Err.Number Then
			valReceiptPayed = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecvalReceiptPayed may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalReceiptPayed = Nothing
	End Function
	
	'% CalReceipt: Cálcula los recibos y devuelve los números de recibo emitidos
	Public Function CalReceipt(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sProrShort As String, ByVal dStartDate As Date, ByVal dExpirDat As Date, ByVal dExpirPol As Date, ByVal sColtimre As Object, ByVal nPayfreq As Integer, ByVal dNextReceip As Date, ByVal sPolitype As String, ByVal sColinvot As String, ByVal sDeclari As String, ByVal nProctype As Integer, ByVal dExpirRec As Date, ByVal nAnuality As Integer, ByVal nDaysFQ As Integer, ByVal nDaysSQ As Integer, ByVal nUsercode As Integer, ByVal sBrancht As String, ByVal sDirdebit As String, ByVal nOffice As Integer, ByVal nIntermed As Double, ByVal sClient As String, ByVal nTransactio As Integer, ByVal nGroup As Integer, ByVal sIssue_receipt As String, ByVal nReval_ind As Integer, ByVal nPolicy_dur As Double, ByVal nMin_durat As Integer, ByVal nReceipt As Double, ByVal sKey As String, ByVal dDate_origi As Date, ByVal nParticip As Double, ByVal dLedgerDate As Date, ByVal nWay_Pay As Integer) As Boolean
		
		'- Se define la variable lrecinsCalReceipt
		Dim lrecinsCalReceipt As eRemoteDB.Execute
		Dim llngIndex As Integer
		
		On Error GoTo CalReceipt_Err
		
		lrecinsCalReceipt = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insCalReceipt'
		'+ Información leída el 14/11/2000 13:45:46
		
		With lrecinsCalReceipt
			.StoredProcedure = "insCalReceipt"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProrshort", sProrShort, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartcert", dStartDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpircert", dExpirDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirpol", dExpirPol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColtimre", sColtimre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNextreceip", dNextReceip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColinvot", sColinvot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDeclari", sDeclari, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProctype", nProctype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dT_expirdat", dExpirRec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnuality", nAnuality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysfq", nDaysFQ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDayssq", nDaysSQ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDirdebit", sDirdebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIssue_receipt", sIssue_receipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReval_ind", nReval_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_dur", nPolicy_dur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_durat", nMin_durat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_origi", dDate_origi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedgerdate", dLedgerDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRecursivecall", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFlag", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				llngIndex = -1
				CalReceipt = True
				mblnCharge = True
				llngIndex = 0
				ReDim marrReceipts(100)
				
				Do While Not .EOF
					llngIndex = llngIndex + 1
					marrReceipts(llngIndex) = .FieldToClass("nReceipt")
					.RNext()
				Loop 
				
				.RCloseRec()
				ReDim Preserve marrReceipts(llngIndex)
			Else
				CalReceipt = False
			End If
		End With
		
CalReceipt_Err: 
		If Err.Number Then
			CalReceipt = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsCalReceipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCalReceipt = Nothing
	End Function
	
	'% CalReceipt: Retorna una acadene con las relaciones de imputacion asociadas al recibo
	Public Function Find_Imputations(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer) As String
		
		'- Se define la variable lrecImputations
		Dim lrecImputations As eRemoteDB.Execute
        Dim lstrRet As String = ""

        On Error GoTo Find_Imputations_Err
		
		lrecImputations = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'ReaPremium_Imputations'
		With lrecImputations
			.StoredProcedure = "ReaPremium_Imputations"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lstrRet = lstrRet & .FieldToClass("nBordereaux") & ", "
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
		If lstrRet <> "" Then
			lstrRet = Mid(lstrRet, 1, Len(lstrRet) - Len(", "))
		End If
		Find_Imputations = lstrRet
		
Find_Imputations_Err: 
		If Err.Number Then
			Find_Imputations = "."
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecImputations may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecImputations = Nothing
	End Function
	
	
	'% ReceiptItem: Carga en las variables de la clase la información de un recibo
	Public Function ReceiptItem(ByVal llngIndex As Integer) As Boolean
		If mblnCharge Then
			If llngIndex <= UBound(marrReceipts) Then
				nReceipt = marrReceipts(llngIndex)
				ReceiptItem = True
			Else
				ReceiptItem = False
			End If
		End If
	End Function
	
	'% CountReceipts: Devuelve el número de recibos que se encuentran en el arreglo
	Public ReadOnly Property CountReceipts() As Integer
		Get
			
			If mblnCharge Then
				CountReceipts = UBound(marrReceipts)
			Else
				CountReceipts = -1
			End If
		End Get
	End Property
	
	
	'% CountCurr: Devuelve el número de monedas utilizadas por los recibos
	Public ReadOnly Property CountCurr() As Integer
		Get
			
			If mblnCharge Then
				CountCurr = UBound(marrCurr)
			Else
				CountCurr = -1
			End If
		End Get
	End Property
	
	'%TypeReceipt: Permite obtener la descripción del Tipo de recibo
	Public ReadOnly Property TypeReceipt() As String
		Get
			On Error GoTo TypeReceipt_Err
			TypeReceipt = IIf(nType = 1, "Cobro", "Devolución")
TypeReceipt_Err: 
			If Err.Number Then
				TypeReceipt = String.Empty
			End If
			On Error GoTo 0
		End Get
	End Property
	
	'%CountItem: propiedad que indica el número de registros que se encuentra en determinado momento en el arreglo de la clase
	Public ReadOnly Property CountItem() As Integer
		Get
			CountItem = UBound(arrReceipts)
			
		End Get
	End Property
	
	'% LoadCurr: Permite leer las monedas utilizadas por los recibos
    Public Function LoadCurr(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCurrency As Integer, ByVal nCertif As Double) As Boolean

        Dim llngIndex As Integer

        Dim lrecreaPremium_curr As eRemoteDB.Execute

        lrecreaPremium_curr = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.reaPremium_curr'
        'Información leída el 11/12/2000 09:19:17

        With lrecreaPremium_curr
            .StoredProcedure = "reaPremium_curr"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                llngIndex = -1
                LoadCurr = True
                mblnCharge = True

                ReDim marrCurr(50)

                Do While Not .EOF
                    llngIndex = llngIndex + 1
                    marrCurr(llngIndex) = .FieldToClass("nCurrency")
                    .RNext()
                Loop

                .RCloseRec()
                ReDim Preserve marrCurr(llngIndex)
            Else
                LoadCurr = False
            End If
        End With

        'UPGRADE_NOTE: Object lrecreaPremium_curr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPremium_curr = Nothing

    End Function
	
	'% CurrItem: Carga en las variables de la clase las monedas utilizadas por los recibos
	Public Function CurrItem(ByVal llngIndex As Integer) As Boolean
		
		If mblnCharge Then
			If llngIndex <= UBound(marrCurr) Then
				nCurrency = marrCurr(llngIndex)
				CurrItem = True
			Else
				CurrItem = False
			End If
		End If
	End Function
	
	'% Update_CA034: Permite rehabilitar un recibo
	Public Function Update_CA034() As Boolean
		Dim lrecupdPremiumCA034 As eRemoteDB.Execute
		
		lrecupdPremiumCA034 = New eRemoteDB.Execute
		
		On Error GoTo Update_CA034_Err
		
		'Definición de parámetros para stored procedure 'insudb.updPremiumCA034'
		'Información leída el 04/01/2001 14:24:08
		
		With lrecupdPremiumCA034
			.StoredProcedure = "updPremiumCA034"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFlag", nFlag, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFlag1", nFlag1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_CA034 = .Run(False)
		End With
		
Update_CA034_Err: 
		If Err.Number Then
			Update_CA034 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdPremiumCA034 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdPremiumCA034 = Nothing
	End Function
	
	'% Find_ByPolicy: Busca los datos correspondiente a un recibo en la tabla Premium.
	Public Function Find_ByPolicy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sColinvot As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaPremium_c As eRemoteDB.Execute
		lrecreaPremium_c = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaPremium_c'
		'+ Información leída el 12/01/2001 14.16.13
		
		With lrecreaPremium_c
			.StoredProcedure = "reaPremium_c"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColinvot", sColinvot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find_ByPolicy = .Run()
		End With
		'UPGRADE_NOTE: Object lrecreaPremium_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_c = Nothing
	End Function
	
	'% Find_DocumentOld: Busca los datos correspondiente a un recibo en la tabla Premium.
	Public Function Find_DocumentOld(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCollectDocTyp As Integer) As Boolean
		Dim lrecreaPremium_c As eRemoteDB.Execute
		lrecreaPremium_c = New eRemoteDB.Execute
		
		nDocument = 0
		
		'+ Definición de parámetros para stored procedure 'insudb.reaPremium_c'
		'+ Información leída el 12/01/2001 14.16.13
		
		With lrecreaPremium_c
			.StoredProcedure = "insReaReceipt_ByPolicy"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollectDoctyp", nCollectDocTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDocument", nDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Find_DocumentOld = True
				nDocument = .Parameters("nDocument").Value
				nContrat = .Parameters("ncontrat").Value
			End If
			
		End With
		'UPGRADE_NOTE: Object lrecreaPremium_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_c = Nothing
	End Function
	
	
	'% Find_PremiumOld: Obtiene los datos correspondiente al recibo más antiguo de la póliza.
	Public Function Find_PremiumOld(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPremium As eRemoteDB.Execute
		lrecreaPremium = New eRemoteDB.Execute
		
		On Error GoTo Find_PremiumOld_Err
		
		Find_PremiumOld = True
		'+ Definición de parámetros para stored procedure 'insudb.reaPremium_c'
		'+ Información leída el 12/01/2001 14.16.13
		If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or lblnFind Then
			
			With lrecreaPremium
				.StoredProcedure = "reaPremium_Old"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Find_PremiumOld = True
					Me.sCertype = sCertype
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nPolicy = nPolicy
					Me.nCertif = nCertif
					Me.nReceipt = .FieldToClass("nReceipt")
					Me.sClient = .FieldToClass("sClient")
					Me.nPremium = .FieldToClass("nPremium")
					Me.nBalance = .FieldToClass("nBalance")
					Me.nCurrency = .FieldToClass("nCurrency")
				Else
					Find_PremiumOld = False
					Me.sCertype = CStr(eRemoteDB.Constants.strNull)
					Me.nBranch = eRemoteDB.Constants.intNull
					Me.nProduct = eRemoteDB.Constants.intNull
					Me.nPolicy = eRemoteDB.Constants.intNull
					Me.nCertif = eRemoteDB.Constants.intNull
				End If
			End With
		End If
		
Find_PremiumOld_Err: 
		If Err.Number Then
			Find_PremiumOld = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium = Nothing
	End Function
	
	'% Find_PremiumOldProp: Obtiene los datos correspondiente al recibo más antiguo de la propuesta.
	Public Function Find_PremiumOldProp(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPremium As eRemoteDB.Execute
		
		On Error GoTo Find_PremiumOldProp_Err
		
		If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or lblnFind Then
			
			lrecreaPremium = New eRemoteDB.Execute
			With lrecreaPremium
				.StoredProcedure = "reaPremium_OldProp"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find_PremiumOldProp = True
					Me.sCertype = sCertype
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nPolicy = nPolicy
					Me.nCertif = nCertif
					nReceipt = .FieldToClass("nReceipt")
					sClient = .FieldToClass("sClient")
					nPremium = .FieldToClass("nPremium")
					nBalance = .FieldToClass("nBalance")
					nCurrency = .FieldToClass("nCurrency")
					nCod_Agree = .FieldToClass("nCod_agree")
					nInsur_area = .FieldToClass("nInsur_area")
					nStatus_pre = .FieldToClass("nStatus_Pre")
					nType = .FieldToClass("nType")
					nContrat = .FieldToClass("nContrat")
				End If
			End With
		Else
			Find_PremiumOldProp = True
		End If
		
Find_PremiumOldProp_Err: 
		If Err.Number Then
			Find_PremiumOldProp = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium = Nothing
	End Function
	
	'% FindQuery_COC002: Obtiene los datos correspondiente a una póliza.
	Public Function FindQuery_COC002(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPol_Pro As Double, ByVal sInd_PolPro As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPremium As eRemoteDB.Execute
		lrecreaPremium = New eRemoteDB.Execute
		
		On Error GoTo FindQuery_COC002_Err
		
		FindQuery_COC002 = True
		'+ Definición de parámetros para stored procedure 'insudb.reaQuery_COC002'
		With lrecreaPremium
			.StoredProcedure = "reaQuery_COC002"
			.Parameters.Add("Certype_Pol", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Branch_Pol", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Product_Pol", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Pol_Pro", nPol_Pro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Ind_PolPro", sInd_PolPro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindQuery_COC002 = True
				nWay_Pay = .FieldToClass("Way_Pay")
				nOffice = .FieldToClass("Office_Pol")
				dStatdate = .FieldToClass("StartDate")
				dExpirDat = .FieldToClass("ExpirDat")
				sClient = .FieldToClass("Client_Pol")
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				nAmount_Tot = IIf(IsDbNull(.FieldToClass("Amount_Tot")), eRemoteDB.Constants.intNull, .FieldToClass("Amount_Tot"))
				nCurrency = .FieldToClass("Currency")
			Else
				FindQuery_COC002 = False
			End If
		End With
		
FindQuery_COC002_Err: 
		If Err.Number Then
			FindQuery_COC002 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium = Nothing
	End Function
	
	'%insUpdPremiumProductor: Esta función se encarga de actualizar la información en tratamiento de la tabla premium.
	Public Function insUpdPremiumProductor(ByVal nType As Integer, Optional ByVal sCrehistori As String = "") As Boolean
		
		Dim lrecupdPremiumProductor As eRemoteDB.Execute
		
		lrecupdPremiumProductor = New eRemoteDB.Execute
		
		insUpdPremiumProductor = True
		
		On Error GoTo insUpdPremiumProductor_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.updPremiumProductor'
		'+ Información leída el 20/02/2001 02:36:58 p.m.
		
		With lrecupdPremiumProductor
			.StoredProcedure = "updPremiumProductor"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If sAffect = "2" Then
				.Parameters.Add("sAction", "UNO", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sAction", "TODOS", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			.Parameters.Add("nAmount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If sDirdebit = "2" Then
				.Parameters.Add("nCard_type", nTyp_crecard, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nCard_type", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If sDirdebit = "2" Then
				.Parameters.Add("sCard_num", sCard_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sCard_num", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sAux_accoun", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If sDirdebit = "1" Then
				.Parameters.Add("nBank_code", nBankext, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nBank_code", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nBordereaux", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dCar_datexp", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCash_mov", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCause_amen", nCause_amen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCessicoi", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sChang_acc", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sDocnumbe", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sInd_rever", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInt_mora", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIntermei", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nNullcode", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sPay_form", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPosted", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nReceipt_fa", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStatdate", dStatdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatisi", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedgerdat", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCrehistori", sCrehistori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdPremiumProductor = .Run(False)
		End With
		
insUpdPremiumProductor_Err: 
		If Err.Number Then
			insUpdPremiumProductor = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdPremiumProductor may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdPremiumProductor = Nothing
	End Function
	
	'%insUpdPremiumStatus: Esta función se encarga de actualizar la información en tratamiento de la tabla premium.
	Public Function insUpdPremiumStatus(ByVal llngStatus_pre As Integer, ByVal lstrIndicator As String, ByVal nType As Integer, ByVal lstrCadena As String) As Boolean
		
		Dim lrecupdPremiumStatus As eRemoteDB.Execute
		
		lrecupdPremiumStatus = New eRemoteDB.Execute
		
		insUpdPremiumStatus = True
		
		On Error GoTo insUpdPremiumStatus_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.updPremiumStatus'
		'+ Información leída el 20/02/2001 03:02:39 p.m.
		
		With lrecupdPremiumStatus
			.StoredProcedure = "updPremiumStatus"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_pre", nStatus_pre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDirdebit", sDirdebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If sAffect = "2" Then
				.Parameters.Add("sAction", "UNO", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sAction", "TODOS", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("nAmount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCard_type", nCard_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCard_num", sCard_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCar_datexp", dCar_datexp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCash_mov", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCause_amen", nCause_amen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCessicoi", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChang_acc", sChang_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocnumbe", sDocnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_rever", sInd_rever, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInt_mora", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIntermei", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_form", sPay_form, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPosted", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt_fa", nReceipt_fa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStatdate", dStatdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatisi", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedgerdat", dLedgerdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCadena", lstrCadena, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollecto", nCollecto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdPremiumStatus = .Run(False)
			
		End With
		
insUpdPremiumStatus_Err: 
		If Err.Number Then
			insUpdPremiumStatus = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdPremiumStatus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdPremiumStatus = Nothing
	End Function
	
	'%insValintermed: Esta rutina permite validar si el intermediario ingresado existe en la tabla intermedia
	Public Function insValIntermed(ByVal nIntermed As Double, ByVal nIntertyp As Integer, ByVal dEffecdate As Date, Optional ByVal blnCol_Agree As String = "") As Boolean
		Dim lrecreaIntermediaClient As eRemoteDB.Execute
		
		lrecreaIntermediaClient = New eRemoteDB.Execute
		
		insValIntermed = True
		
		On Error GoTo insValIntermed_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaIntermediaClient'
		'+ Información leída el 16/01/2001 10:34:47
		
		With lrecreaIntermediaClient
			.StoredProcedure = "reaIntermediaClient"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				
				nIntermed = .FieldToClass("nIntermed")
				nIntertyp = .FieldToClass("nIntertyp")
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If Not IsDbNull(.FieldToClass("sCol_Agree")) Then
					blnCol_Agree = CStr(.FieldToClass("sCol_Agree") = "1")
				Else
					blnCol_Agree = CStr(False)
				End If
				
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If IsDbNull(.FieldToClass("nSupervis")) Then
					nSupervis = 0
				Else
					nSupervis = .FieldToClass("nSupervis")
				End If
				sClient = .FieldToClass("sClient")
				sCliename = .FieldToClass("sCliename")
				.RCloseRec()
			Else
				insValIntermed = False
			End If
		End With
		
insValIntermed_Err: 
		If Err.Number Then
			insValIntermed = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaIntermediaClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaIntermediaClient = Nothing
	End Function
	
	'%insValCOC002_k: Esta función se encarga de validar los datos introducidos en la cabecera de la forma.
	Public Function insValCOC002_k(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nProposal As Double, ByVal nOpt_PolPro As Integer) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPolicy As Object '+ ePolicy.Policy
		Dim lblnError As Boolean
		
		lclsErrors = New eFunctions.Errors
		lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		
		On Error GoTo insValCOC002_k_Err
		
		lblnError = False
		'+Validacion del ramo comercial
		If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1022)
			lblnError = True
		End If
		
		'+Validacion del producto
		If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3635)
			lblnError = True
		End If
		
		'+Validacion del filtro de rescate de recibo
		If nOpt_PolPro = 1 Then
			If nPolicy = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 3003)
			Else
				'+ Valida la poliza
				If Not lblnError Then
					With lclsPolicy
						If Not .Find("2", nBranch, nProduct, nPolicy) Then
							Call lclsErrors.ErrorMessage(sCodispl, 3001)
							lblnError = True
						End If
					End With
				End If
			End If
		Else
			If nProposal = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 3789)
			Else
				'+valida la propuesta
				If Not lblnError Then
					With lclsPolicy
						If Not .Find_Proposal_Pol("2", nBranch, nProduct, nProposal) Then
							Call lclsErrors.ErrorMessage(sCodispl, 750015)
						End If
					End With
				End If
			End If
		End If
		insValCOC002_k = lclsErrors.Confirm
		
insValCOC002_k_Err: 
		If Err.Number Then
			insValCOC002_k = insValCOC002_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
	End Function
	
	'%insValCOC003_k: Esta función se encarga de validar los datos introducidos en la cabecera de la forma.
	Public Function insValCOC003_k(ByVal sCodispl As String, ByVal nReceipt As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sReceiptNum As String) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lblnError As Boolean
		Dim lclsPremium As Object '+ eCollection.Premium
		
		
		lclsErrors = New eFunctions.Errors
		lclsPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
		
		On Error GoTo insValCOC003_k_Err
		
		lblnError = False
		'+Validacion del recibo
		If nReceipt = eRemoteDB.Constants.intNull Or nReceipt = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 5053)
			lblnError = True
		End If
		
		'+Validacion del ramo/producto
		If (sReceiptNum = "2" Or sReceiptNum = "3") And Not lblnError Then
			If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 1022)
				lblnError = True
			End If
			If sReceiptNum = "3" Then
				If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 3635)
					lblnError = True
				End If
			End If
		End If
		
		If Not lblnError Then
			If sReceiptNum = "1" Then
				With lclsPremium
					If Not .Find_Receipt_exist(nReceipt) Then
						Call lclsErrors.ErrorMessage(sCodispl, 5004)
					End If
				End With
			End If
			If sReceiptNum = "2" Then
				With lclsPremium
					If Not .Find_Receipt_Branch(nReceipt, nBranch) Then
						Call lclsErrors.ErrorMessage(sCodispl, 5004)
					End If
				End With
			End If
			If sReceiptNum = "3" Then
				With lclsPremium
					If Not Find("2", nReceipt, nBranch, nProduct, 0, 0) Then
						Call lclsErrors.ErrorMessage(sCodispl, 5004)
					End If
				End With
			End If
		End If
		
		insValCOC003_k = lclsErrors.Confirm
		
insValCOC003_k_Err: 
		If Err.Number Then
			insValCOC003_k = insValCOC003_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPremium = Nothing
	End Function
	
	'%insVerifyPolicyType:Se verifica si la póliza es colectiva. en caso de serlo, se verifica si el recibo es por certificado
	Public Function insVerifyPolicyType(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nReceipt As Double, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		
		Dim lrecreaPremiumCerti As eRemoteDB.Execute
		
		lrecreaPremiumCerti = New eRemoteDB.Execute
		
		insVerifyPolicyType = True
		
		On Error GoTo insVerifyPolicyType_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaPremiumCerti'
		'+ Información leída el 01/02/2000 9:52:57
		
		With lrecreaPremiumCerti
			.StoredProcedure = "reaPremiumCerti"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayNumbe", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nCertif = .Parameters("nCertif").Value
			End If
			
		End With
		
insVerifyPolicyType_Err: 
		If Err.Number Then
			insVerifyPolicyType = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremiumCerti may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremiumCerti = Nothing
	End Function
	
	'%insReaRoles: Esta rutina verifica la concidencia del titular del recibo como cliente de la póliza
	Public Function insReaRolesPolicy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, ByVal dEffecdate As Date, ByVal sClient As String, ByVal nPolicy As Double) As Boolean
		
		Dim lrecinsValRolPremium As eRemoteDB.Execute
		
		lrecinsValRolPremium = New eRemoteDB.Execute
		
		insReaRolesPolicy = True
		
		On Error GoTo insReaRolesPolicy_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.insValRolPremium'
		'+ Información leída el 21/02/2001 02:43:15 p.m.
		
		With lrecinsValRolPremium
			.StoredProcedure = "insValRolPremium"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayNumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				.RCloseRec()
			End If
		End With
		
insReaRolesPolicy_Err: 
		If Err.Number Then
			insReaRolesPolicy = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsValRolPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValRolPremium = Nothing
	End Function
	
	'%insValCO003_k: Esta función se encarga de validar los datos introducidos en el encabezado de la forma
	Public Function insValCO003_k(ByVal sCodispl As String, ByVal nReceipt As Double, ByVal dEffecdate As Date, ByVal sPay_form As String, ByVal sAction As String) As String
		Dim lerrTime As eFunctions.Errors
		Dim lvalField As eFunctions.valField
		Dim lclsProduct As eProduct.Product
		
		Dim lblnError As Integer
		
		On Error GoTo insValCO003_k_Err
		
		lerrTime = New eFunctions.Errors
		lvalField = New eFunctions.valField
		lclsProduct = New eProduct.Product
		
		'+Se enciende la variable que controla los errores masivos
		'+Valida el campo recibo
		With lerrTime
			If nReceipt <= 0 Then
				Call .ErrorMessage(sCodispl, 5053)
				lblnError = True
			Else
				If Not Find_Receipt_exist(nReceipt) Then
					Call .ErrorMessage(sCodispl, 5004)
					lblnError = True
				Else
					If nStatus_pre <> 4 And nStatus_pre <> 1 Then
						Call .ErrorMessage(sCodispl, 5005)
						lblnError = True
					End If
				End If
			End If
			If sAction = "1" And Me.nType = StatusReceipt.clngCollectedReturned Then
				Call .ErrorMessage(sCodispl, 5059)
				lblnError = True
			End If
			
			If Not lblnError Then
				'+ Se verifica si el recibo en tratamineto tiene convenios asignados
				If valReceipt_Paynumbe("2", Me.nBranch, Me.nProduct, Me.nPolicy, Me.nReceipt, 0) Then
					If sAction = "1" Then
						Call lerrTime.ErrorMessage(sCodispl, 5110)
						lblnError = True
					End If
				Else
					If sAction <> "1" Then
						Call lerrTime.ErrorMessage(sCodispl, 5054)
						lblnError = True
					End If
				End If
			End If
			
			If Not lblnError Then
				'+ Se verifica si el producto pertenece a la clase de vida: Vida colectiva (nProdClas = 7)
				If lclsProduct.valProdClas(Me.nBranch, Me.nProduct, 7, dEffecdate) Then
					Call lerrTime.ErrorMessage(sCodispl, 55554)
					lblnError = True
				End If
			End If
			
			'+Valida el campo fecha de efecto
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			If IsNothing(dEffecdate) Then
				Call .ErrorMessage(sCodispl, 5055)
				lblnError = True
			Else
				If Not IsDate(dEffecdate) Then
					Call .ErrorMessage(sCodispl, 7114)
					lblnError = True
				End If
			End If
			
			If Not lblnError Then
				'+ Se verifica que la fecha de emisión del recibo sea mayor a la de tratamiento.
				If Me.dEffecdate > dEffecdate Then
					Call .ErrorMessage(sCodispl, 5057)
					lblnError = True
				End If
			End If
			
			'+Valida la forma de pago
			If sAction = "2" And sPay_form = String.Empty Then
				Call .ErrorMessage(sCodispl, 3015)
				lblnError = True
			Else
				If sPay_form = "17" Then
					Call .ErrorMessage(sCodispl, 5128)
					lblnError = True
				End If
			End If
			
			If Not lblnError Then
				If sAction <> "1" And sAction <> "6" Then
					'+ Se verifica la existencia de información válida.
					If Not valExistsCO003_K(nReceipt, dEffecdate) Then
						Call lerrTime.ErrorMessage(sCodispl, 5144)
					End If
				End If
			End If
			
			insValCO003_k = .Confirm
		End With
		
insValCO003_k_Err: 
		If Err.Number Then
			insValCO003_k = "insValCO003_k: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalField = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
	End Function
	
	
	'% Find: Verifica si existe el recibo en premium
	Public Function Find_Receipt_exist(ByVal nReceipt As Double) As Boolean
		Dim lrecreaPremium_nReceipt As eRemoteDB.Execute
		
		lrecreaPremium_nReceipt = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaPremium_nReceipt'
		'Información leída el 01/10/2001 11:49:27 a.m.
		Find_Receipt_exist = False
		
		With lrecreaPremium_nReceipt
			.StoredProcedure = "reaPremium_nReceipt"
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.sStatusva = .FieldToClass("sStatusva")
				Me.nReceipt = nReceipt
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nStatus_pre = .FieldToClass("nStatus_pre")
				Me.nType = .FieldToClass("nType")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Find_Receipt_exist = True
			Else
				Find_Receipt_exist = False
			End If
			
		End With
		'UPGRADE_NOTE: Object lrecreaPremium_nReceipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_nReceipt = Nothing
		
	End Function
	
	'%insValCO003: Esta función se encarga de validar los datos introducidos en la zona de detalle para la forma.
	Public Function insValCO003(ByVal sCodispl As String, ByVal dPayDate As Date, ByVal nPaynumbe As Integer, ByVal dEffecdate As Date, ByVal nIntammou As Double, ByVal sAction As String, ByVal sPay_form As String, ByVal nReceipt As Double, ByVal sWindowstype As String) As String
		Dim lerrTime As eFunctions.Errors
		Dim lcolPremiums As eCollection.Premiums = New eCollection.Premiums
		Dim lclsPremium As eCollection.Premium = New eCollection.Premium
		Dim ldtmPaydate As Date
		
		On Error GoTo insValCO003_Err
		
		lerrTime = New eFunctions.Errors
		
		Dim lblnPremium As Double
		With lerrTime
			'+ Validación puntual.
			If sWindowstype = "PopUp" Then
				'+Valida la fecha
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				If IsNothing(dPayDate) Then
					Call .ErrorMessage(sCodispl, 3587)
				End If
				
				'+ Valida que la fecha de pago no sea anterior a la fecha del proceso.
				If dPayDate < dEffecdate Then
					Call .ErrorMessage(sCodispl, 5065)
				End If
				
				'+ Valida que la fecha de pago no sea anterior a la fecha de emisión del recibo.
				If Find("2", nReceipt, 0, 0, 0, 0) Then
					If dPayDate < Me.dEffecdate Then
						Call .ErrorMessage(sCodispl, 5034)
					End If
				End If
				
				If sAction = "2" Then
					If nPaynumbe > 1 Then
						If Find("2", nReceipt, 0, 0, 0, nPaynumbe - 1) Then
							If Me.nStatus_pre = 1 Then
								Call .ErrorMessage(sCodispl, 5058)
							End If
						End If
					End If
				End If
				
				If sAction = "1" Or sAction = "2" Then
					ldtmPaydate = getMaxPaydate(sCertype, nBranch, nProduct, nReceipt)
					If ldtmPaydate <> eRemoteDB.Constants.dtmNull Then
						If dPayDate <= ldtmPaydate Then
							Call .ErrorMessage(sCodispl, 5035)
						End If
					End If
				End If
				
				If sAction = "4" Then
					If nStatus_pre <> 1 Then
						Call .ErrorMessage(sCodispl, 5070)
					End If
				End If
				
				'+Validación del importe
				If sAction = "1" Or sAction = "6" Then
					If CDbl(sAction) = 1 Then
						If nIntammou <> eRemoteDB.Constants.intNull Then
							If Not IsNumeric(nIntammou) Then
								Call .ErrorMessage(sCodispl, 5061)
							End If
						Else
							Call .ErrorMessage(sCodispl, 5061)
						End If
					End If
				End If
				
			Else
				If sAction = "1" Or sAction = "6" Then
					lcolPremiums = New eCollection.Premiums
					lclsPremium = New eCollection.Premium
					
					
					lblnPremium = 0
					If lcolPremiums.Find_CO003(nReceipt, dEffecdate, CInt(sAction)) Then
						For	Each lclsPremium In lcolPremiums
							lblnPremium = lblnPremium + lclsPremium.nIntammou
						Next lclsPremium
						If Find("2", nReceipt, 0, 0, 0, 0) Then
							If lblnPremium <> nPremium Then
								Call .ErrorMessage(sCodispl, 5060)
							End If
						End If
					End If
					'UPGRADE_NOTE: Object lcolPremiums may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lcolPremiums = Nothing
				End If
			End If
			
			insValCO003 = .Confirm
		End With
		
insValCO003_Err: 
		If Err.Number Then
			insValCO003 = "insValCO003: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lcolPremiums may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolPremiums = Nothing
		'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPremium = Nothing
	End Function
	
	'%insPostCO003: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostCO003(ByVal nReceipt As Integer, ByVal dPayDate As Date, ByVal nPremium As Double, ByVal nPaynumbe As Integer, ByVal nRate As Double, ByVal dEffecdate As Date, ByVal nIntammou As Double, ByVal sAction As String, ByVal nUsercode As Integer, ByVal sPay_form As String) As Boolean
		
		On Error GoTo InsPostCO003_err
		
		With Me
			.nReceipt = nReceipt
			.dCompdate = dCompdate
			.nPremium = nPremium
			.nPaynumbe = nPaynumbe
			.nRate = nRate
			.dEffecdate = dEffecdate
			.nIntammou = nIntammou
			.sAction = sAction
			.nUsercode = nUsercode
			.nBranch = eRemoteDB.Constants.intNull
			.nProduct = eRemoteDB.Constants.intNull
			.nCurrency = eRemoteDB.Constants.intNull
			.sPay_form = sPay_form
			.dPayDate = dPayDate
		End With
		
		insPostCO003 = insAgreement(CInt(sAction))
		
InsPostCO003_err: 
		If Err.Number Then
			insPostCO003 = False
		End If
	End Function
	
	'**%InsValCO004_k: This routine validates the header fields of the form (Header)
	'%InsValCO004_k: Realiza la validación de los campos a actualizar en la ventana CO004 (Header)
	Public Function insValCO004_k(ByVal sCodispl As String, ByVal sGenera As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nReceiptNum As Double, ByVal nContrat As Integer, ByVal nDraft As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lobjPolicy As Object
		Dim lclsFinanceCO As Object
		Dim lobjFinanceDraft As Object
		
		
		lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		lobjPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		lclsFinanceCO = eRemoteDB.NetHelper.CreateClassInstance("eFinance.financeCO")
		
		On Error GoTo InsValCO004_k_Err
		
		'Cambio de via por Póliza
		If sGenera = "1" Then
			If nPolicy = eRemoteDB.Constants.intNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 3003)
			ElseIf Not lobjPolicy.Find("2", nBranch, nProduct, nPolicy, True) Then 
				Call lobjErrors.ErrorMessage(sCodispl, 8071)
			Else
				If lobjPolicy.sStatus_pol > CollectionSeq.TypeStatus_Pol.cstrValid And lobjPolicy.sStatus_pol < CollectionSeq.TypeStatus_Pol.cstrPrintPendent Then
					Call lobjErrors.ErrorMessage(sCodispl, 3720)
				Else
					If lobjPolicy.sStatus_pol = "6" And lobjPolicy.dNulldate <> eRemoteDB.Constants.dtmNull Then
						Call lobjErrors.ErrorMessage(sCodispl, 3063)
					End If
				End If
			End If
		End If
		
		'Cambio de via por Recibo
		If sGenera = "2" Then
			If nReceiptNum = eRemoteDB.Constants.intNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 5053)
			Else
				If Not Me.FindPremiumExist(sCertype, nBranch, nProduct, nReceiptNum, nDigit, nPaynumbe, TypeNumeratorPOL_REC.cstrSysNumeGeneral) Then
					Call lobjErrors.ErrorMessage(sCodispl, 5004)
				Else
					If Me.nStatus_pre <> StatusReceipt.clngPendent And Me.nStatus_pre <> StatusReceipt.clngLodgedPendent And Me.nStatus_pre <> StatusReceipt.clngFinanced Then
						Call lobjErrors.ErrorMessage(sCodispl, 5013)
					Else
						'+ Debe ser recibo de cobro
						If Me.nType = Collec_Devolu.clngToReturn Then
							Call lobjErrors.ErrorMessage(sCodispl, 5012)
						End If
					End If
				End If
			End If
		End If
		
		'Cambio de via por Contrato
		If sGenera = "3" Then
			If nContrat = eRemoteDB.Constants.intNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 21062)
			Else
				If Not lclsFinanceCO.Find_Contrat(nContrat, True) Then
					Call lobjErrors.ErrorMessage(sCodispl, 21002)
				Else
					Me.nWay_Pay = lclsFinanceCO.nWay_Pay
				End If
			End If
		End If
		
		'Cambio de via por Cuota
		If sGenera = "4" Then
			If (nDraft = eRemoteDB.Constants.intNull Or nDraft = 0) And nContrat = eRemoteDB.Constants.intNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 56051)
			Else
				lobjFinanceDraft = eRemoteDB.NetHelper.CreateClassInstance("eFinance.FinanceDraft")
				If lobjFinanceDraft.Find(nContrat, nDraft) Then
					If lobjFinanceDraft.nStat_draft <> 1 Then
						Call lobjErrors.ErrorMessage(sCodispl, 56053)
					Else
						Me.nWay_Pay = lobjFinanceDraft.nWay_Pay
					End If
				Else
					Call lobjErrors.ErrorMessage(sCodispl, 56052)
				End If
				'UPGRADE_NOTE: Object lobjFinanceDraft may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lobjFinanceDraft = Nothing
			End If
		End If
		
		insValCO004_k = lobjErrors.Confirm
		
InsValCO004_k_Err: 
		If Err.Number Then
			insValCO004_k = insValCO004_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjPolicy = Nothing
		'UPGRADE_NOTE: Object lclsFinanceCO may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinanceCO = Nothing
		
	End Function
	'**%InsValCO004: This routine validates the fields of the form CO004
	'%InsValCO004: Realiza la validación de los campos a actualizar en la ventana CO004 (Folder)
	Public Function insValCO004(ByVal sCodispl As String, Optional ByVal sCertype As String = "", Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nCause As Integer = 0, Optional ByVal sChangeWay As String = "", Optional ByVal sChangePremium As String = "", Optional ByVal nWayPay As Integer = 0, Optional ByVal nBankPac As Integer = 0, Optional ByVal sClientPac As String = "", Optional ByVal sAccountPac As String = "", Optional ByVal sBankauthPac As String = "", Optional ByVal nCardTypeTbk As Integer = 0, Optional ByVal sAccountTbk As String = "", Optional ByVal dCardExpirTbk As Date = #12:00:00 AM#, Optional ByVal sClientTbk As String = "", Optional ByVal sNewWayPay As String = "", Optional ByVal nBankPacNew As Integer = 0, Optional ByVal sClientPacNew As String = "", Optional ByVal sAccountPacNew As String = "", Optional ByVal sBankAuthPACNew As String = "", Optional ByVal nCardTypeTbkNew As Integer = 0, Optional ByVal dCardExpirNewTbk As Date = #12:00:00 AM#, Optional ByVal sClientNewTbk As String = "", Optional ByVal sAccountTbkNew As String = "", Optional ByVal nPolicy As Double = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nReceipt As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal nDigit As Integer = 0, Optional ByVal nPaynumbe As Integer = 0, Optional ByVal nContrat As Double = 0, Optional ByVal nDraft As Double = 0, Optional ByVal sWay_Pay As String = "", Optional ByVal sTypeDoc As Object = Nothing, Optional ByVal nAgreementNew As Integer = 0, Optional ByVal nOriginNew As Integer = 0, Optional ByVal nAFPCommiNew As Integer = 0, Optional ByVal nCurrencyNew As Integer = 0, Optional ByVal sApv As String = "", Optional ByVal nProdClas As Integer = 0, Optional ByVal sClientPay As String = "", Optional ByVal sClientEmp As String = "", Optional ByVal nPayfreq As Integer = 0) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsCtrol_Date As eGeneral.Ctrol_date
		Dim lclsDir_debit As Dir_debit
		Dim lclsPolicy_his As Object
		Dim lclsClient As eClient.Client
		Dim lblnFlag As Boolean
		Dim lclsbk_account As eClient.bk_account
		Dim lclscred_card As eClient.cred_card
		Dim lclscred_cards As eClient.cred_cards
		Dim dMaxStatdateP As Date
		Dim lblnExist As Boolean
		Dim lclsWay_pay_prod As eProduct.Way_pay_prod
		Dim lcolAPV_origins As Object
		Dim nTotalDeposits As Double
		Dim nCounter As Integer
		Dim lstrValReq As String
		
		
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValCo004_Err
		
		
		
		
		If nWayPay = 0 Or nWayPay = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 750139)
		Else
			'+ Se valida la gestion de cobro de un recibo/cuota de Aviso a PAC o TBK
			If sTypeDoc = "2" Or sTypeDoc = "3" Or sTypeDoc = "4" Then
                If nWayPay = 4 And (sNewWayPay = "1" Or sNewWayPay = "2") Then
                    Call lobjErrors.ErrorMessage(sCodispl, 750035)
                End If
			End If
			
			'+ Se valida si el cambio afecta a un solo documento (recibo, contrato, cuota)
			'If (sNewWayPay = "1" Or _
			''    sNewWayPay = "2" Or _
			''    sNewWayPay = "4") And _
			''    nWayPay = 3 Then ' Descuento por planilla
			'    Call lobjErrors.ErrorMessage(sCodispl, 750125, , , " ingresada en la ventana")
			'End If
			
			'+Se efectua las validaciones concernientes a la fecha
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 7116)
				lblnFlag = False
			Else
				'+ Si el cambio es a toda la vía de la póliza
				If sChangeWay = "1" Then
					lclsPolicy_his = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy_his")
					dMaxStatdateP = lclsPolicy_his.insReaLastMovPolicy_his(sCertype, nPolicy, nBranch, nProduct)
					If dEffecdate < CDate(dMaxStatdateP) Then
						Call lobjErrors.ErrorMessage(sCodispl, 750090)
						lblnFlag = False
					End If
				End If
			End If
			
			'+Validación de la causa
			If nCause = eRemoteDB.Constants.intNull Or nCause = 0 Then
				Call lobjErrors.ErrorMessage(sCodispl, 750007)
				lblnFlag = False
			End If
			
			If sNewWayPay <> String.Empty Then
				lclsWay_pay_prod = New eProduct.Way_pay_prod
				If lclsWay_pay_prod.Find(nBranch, nProduct, CShort(sNewWayPay), dEffecdate) Or sTypeDoc <> "1" Then
					'+ Valida que si la nueva vía de pago es PAC o TBK, los campos respectivos no deben ser nulos
					If sNewWayPay = "1" Then
						If nBankPacNew = eRemoteDB.Constants.intNull Or sClientPacNew = String.Empty Or sAccountPacNew = String.Empty Or sBankAuthPACNew = String.Empty Then
							Call lobjErrors.ErrorMessage(sCodispl, 750036)
						End If
					End If
					
					If sNewWayPay = "2" Then
						If sAccountTbkNew = String.Empty Or nCardTypeTbkNew = eRemoteDB.Constants.intNull Or dCardExpirNewTbk = eRemoteDB.Constants.dtmNull Or sClientNewTbk = String.Empty Then
							Call lobjErrors.ErrorMessage(sCodispl, 750037)
						End If
					End If
					
					If sNewWayPay = "3" Then
						If nAgreementNew = eRemoteDB.Constants.intNull Then
							Call lobjErrors.ErrorMessage(sCodispl, 55004)
						End If
						
						
					End If
					
					'        If nProdClas = 4 And _
					''          sApv = "1" Then
					'          If nOriginNew = NumNull Then
					'             Call lobjErrors.ErrorMessage(sCodispl, 70090)
					'        End If
					'   End If
					
					If sNewWayPay = "7" Then
						If nAFPCommiNew = eRemoteDB.Constants.intNull Then
							Call lobjErrors.ErrorMessage(sCodispl, 70091)
						End If
						If nCurrencyNew = eRemoteDB.Constants.intNull Then
							Call lobjErrors.ErrorMessage(sCodispl, 70092)
						End If
					End If
				Else
					Call lobjErrors.ErrorMessage(sCodispl, 55702)
				End If
			End If
			
			
			
			
		End If
		
		
		lstrValReq = insValCo004_2(sCertype, nBranch, nProduct, nPolicy, 0, dEffecdate, sClientPay, sClientEmp, CInt(sNewWayPay), nPayfreq, nAgreementNew, nWayPay)
		
		If lstrValReq <> String.Empty Then
			Call lobjErrors.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrValReq)
		End If
		
		insValCO004 = lobjErrors.Confirm
		
insValCo004_Err: 
		If Err.Number Then
			insValCO004 = insValCO004 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsDir_debit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDir_debit = Nothing
		'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_his = Nothing
		'UPGRADE_NOTE: Object lclsbk_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsbk_account = Nothing
		'UPGRADE_NOTE: Object lclscred_card may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscred_card = Nothing
		'UPGRADE_NOTE: Object lclscred_cards may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscred_cards = Nothing
		
	End Function
	
	'%insAgreement: Efectua el proceso final.
	Private Function insAgreement(ByVal nAction As Integer) As Boolean
		
		'-Se defina la variable que servirá para sostener los parámetros a enviar al SP y que ejecutará el mismo
		
		Dim lrecAgreement As New eRemoteDB.Execute
		
		Dim ldblBalance As Double
		
		'-Varible que servirá como contador de la cantidad de columnas del grid
		
		Dim lintIndex As Integer
		
		Dim lblnSuccess As Boolean
		
		
		On Error GoTo insAgreement_Err
		
		insAgreement = True
		
		'+Se realiza un ciclo para recorrer el TDBGrid
		
		
		If nAction = 1 Then '(Registrar)
			
			lrecAgreement.StoredProcedure = "insAgreement"
			
			lrecAgreement.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("dPaydate", dPayDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("nIntAmmou", nIntammou, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 2, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			lrecAgreement.Parameters.Add("nBalance", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			lrecAgreement.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			lrecAgreement.Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			lrecAgreement.Parameters.Add("nCurrency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("sPay_form", sPay_form, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 2, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insAgreement = lrecAgreement.Run(False)
		Else
			
			lrecAgreement.StoredProcedure = "insAgreement"
			
			lrecAgreement.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("dPaydate", dPayDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("nIntAmmou", nIntammou, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 2, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			lrecAgreement.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			lrecAgreement.Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			lrecAgreement.Parameters.Add("nCurrency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecAgreement.Parameters.Add("sPay_form", sPay_form, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 2, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insAgreement = lrecAgreement.Run(False)
			
		End If
		
		nAction = eRemoteDB.Constants.intNull
		
insAgreement_Err: 
		If Err.Number Then
			insAgreement = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecAgreement = Nothing
	End Function
	
	'%insValCO005: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValCO005(ByVal sCodispl As String, ByVal tcdDate As Date, ByVal gmnReceipt As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal optAnul As String, ByVal valCause As Integer) As String
		Dim lerrTime As eFunctions.Errors
		Dim lvaldate As eFunctions.valField
		
		lerrTime = New eFunctions.Errors
		lvaldate = New eFunctions.valField
		
		lvaldate.objErr = lerrTime
		
		On Error GoTo insValCO005_Err
		
		Static lstrValField As String
		
		'-Se define la variable lstrMaxStatdate utilizada para almacenar la máxima fecha del último movimiento del recibo.
		
		Dim ldtmMaxStatdate As Date
		Dim lblnError As Boolean
		Dim llngProduct As Integer
		
		' + Se valida la Operación a realizar-causa
		If optAnul = "" Then
			Call lerrTime.ErrorMessage(sCodispl, 5022)
		End If
		
		'+Se efectua las validaciones concernientes a la fecha de cobro devolución.
		
		Dim lrecreaCtrol_Date As eRemoteDB.Execute
		Dim lclsPremium_mo As eCollection.Premium_mo
		If tcdDate = eRemoteDB.Constants.dtmNull Then
			Call lerrTime.ErrorMessage(sCodispl, 7118)
		Else
			If Not tcdDate = eRemoteDB.Constants.dtmNull Then
				If Not IsDate(tcdDate) Then
					Call lerrTime.ErrorMessage(sCodispl, 7114)
				Else
					'+Se valida que la fecha sea anterior o igual a la fecha del día.
					If CDate(tcdDate) > Today Then
						Call lerrTime.ErrorMessage(sCodispl, 1002)
					End If
					'+Se valida que la fecha introducida sea posterior al último proceso de asientos automáticos.
					lrecreaCtrol_Date = New eRemoteDB.Execute
					'Definición de parámetros para stored procedure 'insudb.reaCtrol_Date'
					'Información leída el 01/03/2001 12:03:13
					With lrecreaCtrol_Date
						.StoredProcedure = "reaCtrol_Date"
						.Parameters.Add("nType_proce", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						If .Run Then
							If CDate(lrecreaCtrol_Date.FieldToClass("dEffecdate")) > CDate(tcdDate) Then
								Call lerrTime.ErrorMessage(sCodispl, 1008)
							End If
							'+ Se valida la fecha con respecto a la de inicio del período contable en vigor
							If CDate(tcdDate) < CDate(lrecreaCtrol_Date.FieldToClass("dEffecdate")) Then
								Call lerrTime.ErrorMessage(sCodispl, 1006)
							End If
							.RCloseRec()
						End If
					End With
					'UPGRADE_NOTE: Object lrecreaCtrol_Date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lrecreaCtrol_Date = Nothing
					'+Se valida que la fecha introducida sea posterior o igual a la del último movimiento del recibo.
					If gmnReceipt <> 0 Then
						lclsPremium_mo = New eCollection.Premium_mo
						ldtmMaxStatdate = lclsPremium_mo.insReaLastMovPremium_mo(gmnReceipt, sCertype, 0, 0, nBranch, nProduct)
						If CDate(tcdDate) < CDate(ldtmMaxStatdate) Then
							Call lerrTime.ErrorMessage(sCodispl, 5001)
						End If
					End If
				End If
			End If
		End If
		
		'+Se realizan las validaciones concernientes al número del recibo.
		Dim clsPremium As eCollection.Premium
		If gmnReceipt = 0 Or gmnReceipt = eRemoteDB.Constants.intNull Then '1
			Call lerrTime.ErrorMessage(sCodispl, 5053)
		Else '1
			clsPremium = New eCollection.Premium
			If clsPremium.FindPremiumExist(sCertype, nBranch, nProduct, gmnReceipt, 0, 0, TypeNumeratorPOL_REC.cstrSysNumeBranch) Then '2
				If optAnul = "1" Then
					If clsPremium.nStatus_pre <> StatusReceipt.clngPendent And clsPremium.nStatus_pre <> StatusReceipt.clngLodgedPendent Then
						Call lerrTime.ErrorMessage(sCodispl, 5021)
					End If
				Else
					If clsPremium.nStatus_pre <> StatusReceipt.clngAnnuled Then
						Call lerrTime.ErrorMessage(sCodispl, 750042)
					Else
						If clsPremium.nNullcode = 30 Or clsPremium.nNullcode = 5 Then 'Falta de pago (Cancelación automática) o a causa de otro recibo
							Call lerrTime.ErrorMessage(sCodispl, 750094)
						Else
							If clsPremium.nType = Collec_Devolu.clngReceptable Then
								Call lerrTime.ErrorMessage(sCodispl, 5039)
							ElseIf clsPremium.nType = Collec_Devolu.clngToReturn Then 
								Call lerrTime.ErrorMessage(sCodispl, 5040)
							End If
						End If
					End If
				End If
				'+Si el recibo no es de Cartera/Renovación o de Intereses por préstamos no se puede reversar ni reinstalar
				If clsPremium.nTratypei <> 2 And clsPremium.nTratypei <> 13 Then
					Call lerrTime.ErrorMessage(sCodispl, 3095,  ,  , "(" & clsPremium.sDescTratypei & ")")
				End If
			Else '2
				Call lerrTime.ErrorMessage(sCodispl, 5004)
			End If '2
			'UPGRADE_NOTE: Object clsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			clsPremium = Nothing
		End If '1
		'+Se realizan las validaciones concernientes a la causa de anulación/reinstalación de recibos.
		
		If valCause = 0 Or valCause = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 10872)
		End If
		
		insValCO005 = lerrTime.Confirm
		
insValCO005_Err: 
		If Err.Number Then
			insValCO005 = "insValCO005: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lvaldate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvaldate = Nothing
	End Function
	
	'%insPostFolder: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostCO005(ByVal tcdDate As Date, ByVal gmnReceipt As Double, ByVal valCause As Integer, ByVal gintUser As Integer, ByVal optAnul As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		On Error GoTo insPostCO005_Err
		insPostCO005 = True
		
		If Me.insAnulReinsReceipt(tcdDate, gmnReceipt, valCause, gintUser, optAnul, sCertype, nBranch, nProduct) Then
			insPostCO005 = True
		Else
			insPostCO005 = False
		End If
		
insPostCO005_Err: 
		If Err.Number Then
			insPostCO005 = False
		End If
	End Function
	
	'%insAnulReinsReceipt:Esta rutina permite anular o rehabilitar un recibo.
	Public Function insAnulReinsReceipt(ByVal dDate As Date, ByVal nReceipt As Double, ByVal nCause As Integer, ByVal nUsercode As Integer, ByVal sOptAnull As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		On Error GoTo insAnulReinsReceipt_Err
		
		insAnulReinsReceipt = True
		
		Dim lrecinsAnulReceipt As eRemoteDB.Execute
		lrecinsAnulReceipt = New eRemoteDB.Execute
		
		With lrecinsAnulReceipt
			.StoredProcedure = "insAnulReceipt"
			.Parameters.Add("dDateProce", dDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOptAnull", sOptAnull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insAnulReinsReceipt = .Run(False)
		End With
		
insAnulReinsReceipt_Err: 
		If Err.Number Then
			insAnulReinsReceipt = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsAnulReceipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsAnulReceipt = Nothing
	End Function
	
	'%insReaDate_Premium :
	Public Function insReaDate_Premium() As Boolean
		Dim lrecreaPremium_Receipt As eRemoteDB.Execute
		
		lrecreaPremium_Receipt = New eRemoteDB.Execute
		
		insReaDate_Premium = True
		
		On Error GoTo insReaDate_Premium_Err
		
		'+Definición de parámetros para stored procedure 'insudb.reaPremium_receipt'
		'+Información leída el 16/03/2001 04:50:58 p.m.
		
		With lrecreaPremium_Receipt
			.StoredProcedure = "reaPremium_receipt"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				dRescuedate = .FieldToClass("dExpirdat")
				.RCloseRec()
			End If
		End With
		
insReaDate_Premium_Err: 
		If Err.Number Then
			insReaDate_Premium = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremium_Receipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_Receipt = Nothing
	End Function
	
	
	'%insUpdNullPremium: Anula los recibos de la poliza
	Public Function insUpdNullPremium() As Boolean
		Dim lrecinsNullPendingPremium As eRemoteDB.Execute
		
		On Error GoTo insUpdNullPremium_Err
		'+Definición de parámetros para stored procedure 'insudb.insNullPendingPremium'
		'+Información leída el 21/01/2000 10:58:07
		lrecinsNullPendingPremium = New eRemoteDB.Execute
		With lrecinsNullPendingPremium
			.StoredProcedure = "insNullPendingPremium"
			.Parameters.Add("nNullcode", 9, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dRescuedate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdNullPremium = .Run(False)
		End With
		
insUpdNullPremium_Err: 
		If Err.Number Then
			insUpdNullPremium = False
		End If
		'UPGRADE_NOTE: Object lrecinsNullPendingPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsNullPendingPremium = Nothing
		On Error GoTo 0
	End Function
	
	'%insValCOC006_k: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValCOC006_k(ByVal sCodispl As String, ByVal sUnderw As String, ByVal sRenew As String, ByVal sAll As String, ByVal nStatus_pre As Integer, ByVal dStartDate As Date, ByVal nIntermed As Double, ByVal nSupervis As Double) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsAgents As Object
		
		lclsErrors = New eFunctions.Errors
		lclsAgents = eRemoteDB.NetHelper.CreateClassInstance("eAgent.Agents")
		
		On Error GoTo insValCOC006_k_Err
		
		'+Validación del estado.
		
		If nStatus_pre = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 750019)
		End If
		
		If dStartDate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 5072)
		Else
			If Not IsDate(dStartDate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 7114) 'No aparece en el funcional
			End If
		End If
		
		'+Validación del Productor
		
		If nIntermed <> 0 And (nSupervis = 0 Or nSupervis = eRemoteDB.Constants.intNull) Then
			If Not lclsAgents.findIntermediaClient(nIntermed, Dir_debit.Interm_typ.clngProducer, Today) Then
				Call lclsErrors.ErrorMessage(sCodispl, 750021)
			End If
		End If
		
		'+Validación del Organizador
		
		If (nSupervis) <> 0 And (nIntermed = 0 Or nIntermed = eRemoteDB.Constants.intNull) Then
			If Not lclsAgents.findIntermediaClient(nSupervis, Dir_debit.Interm_typ.clngOrganizer, Today) Then
				Call lclsErrors.ErrorMessage(sCodispl, 750022)
			End If
		End If
		
		If sUnderw = "0" And sRenew = "0" And sAll = "0" Then
			Call lclsErrors.ErrorMessage(sCodispl, 750034)
		End If
		
		insValCOC006_k = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsAgents may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAgents = Nothing
		
insValCOC006_k_Err: 
		If Err.Number Then
			insValCOC006_k = insValCOC006_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%insValCO675_K: Esta función se encarga de validar los datos introducidos en la forma.
	Public Function insValCO675(ByVal sCodispl As String, ByVal nReceipt As Double, ByVal nStatusPre As Integer, ByVal dLimitdate As Date, ByVal dNewLimitdate As Date, ByVal nBulletins As Double, ByVal nType As Integer, ByVal nExistReceipt As Boolean) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValCO675_Err
		
		'+Validación del recibo.
		If nReceipt = eRemoteDB.Constants.intNull Or nReceipt = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60248)
		Else
			If Not nExistReceipt Then
				Call lclsErrors.ErrorMessage(sCodispl, 60249)
			Else
				If ((nStatusPre > 1 And nStatusPre < 4) Or (nStatusPre > 4 And nStatusPre < 8) Or nType = 2) Then
					Call lclsErrors.ErrorMessage(sCodispl, 21027)
				End If
			End If
		End If
		'+Validación de la nueva fecha de generación de cobranzas.
		If dNewLimitdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60250)
		Else
			If dNewLimitdate <= dLimitdate Then
				Call lclsErrors.ErrorMessage(sCodispl, 55724)
			End If
		End If
		'+Verificación de anulacion de boletin
		If nBulletins = eRemoteDB.Constants.intNull Then
			nBulletins = 0
		End If
		If nBulletins > 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55722)
		End If
		
		insValCO675 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCO675_Err: 
		If Err.Number Then
			insValCO675 = insValCO675 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insPostCO675: Esta función se encaga de realizar las acciones para el cambio de la fecha
	'+ de generación de cobranzas
	Public Function insPostCO675(ByVal sCodispl As String, ByVal sCertype As String, ByVal nReceipt As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, ByVal dNewLimitdate As Date, ByVal nBulletins As Double, ByVal nUsercode As Integer, ByVal nContrat As Double, ByVal nDraft As Integer) As Boolean
		Dim lPremium_Limitdate As New eRemoteDB.Execute
		
		On Error GoTo insPostCO675_Err
		
		'Definición de parámetros para stored procedure 'insudb.insUpdPremium_Limitdate'
		With lPremium_Limitdate
			.StoredProcedure = "insUpdPremium_Limitdate"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNewLimitdate", dNewLimitdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostCO675 = .Run(False)
		End With
		
insPostCO675_Err: 
		If Err.Number Then
			insPostCO675 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lPremium_Limitdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lPremium_Limitdate = Nothing
	End Function
	
	'%insValCOC679_k: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValCOC679_k(ByVal sCodispl As String, ByVal dProcess As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValCOC679_k_Err
		
		'+Validación del estado.
		If dProcess = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 55581)
		End If
		
		insValCOC679_k = lclsErrors.Confirm
		
insValCOC679_k_Err: 
		If Err.Number Then
			insValCOC679_k = insValCOC679_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insUpdCOC679: Esta función se encarga de actualizar el check de imprimir
	Public Function insUpdCOC679(ByVal sKey As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double, ByVal nDraft As Integer, ByVal sPrint As String) As Boolean
		Dim lrecinsUpdCOC679 As eRemoteDB.Execute
		
		On Error GoTo insUpdCOC679_Err
		
		lrecinsUpdCOC679 = New eRemoteDB.Execute
		
		With lrecinsUpdCOC679
			.StoredProcedure = "INSUPDCOC679_PRINT"
			
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPrint", sPrint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insUpdCOC679 = .Run(False)
		End With
		
insUpdCOC679_Err: 
		If Err.Number Then
			insUpdCOC679 = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdCOC679 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdCOC679 = Nothing
		On Error GoTo 0
		
	End Function
	
	'%insUpdCOC679: Esta función se encarga de actualizar el check de imprimir
	Public Function insUpd_sPrint(ByVal sKey As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nReceipt As Double, ByVal nDraft As Integer, ByVal sPrint As String) As Boolean
		Dim lrecinsUpd_sPrint As eRemoteDB.Execute
		
		On Error GoTo insUpd_sPrint_Err
		
		lrecinsUpd_sPrint = New eRemoteDB.Execute
		
		With lrecinsUpd_sPrint
			.StoredProcedure = "INSUPDCO635_PRINT"
			
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPrint", sPrint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insUpd_sPrint = .Run(False)
		End With
		
insUpd_sPrint_Err: 
		If Err.Number Then
			insUpd_sPrint = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpd_sPrint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpd_sPrint = Nothing
		On Error GoTo 0
		
	End Function
	
	'%insValCOC747_k: Esta función se encarga de validar los datos introducidos en la cabecera de la forma.
	Public Function insValCOC747_k(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lobjPolicy As Object '+ ePolicy.Policy
		Dim lblnError As Boolean
		
		lclsErrors = New eFunctions.Errors
		lobjPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		
		On Error GoTo insValCOC747_k_Err
		
		If nInsur_area = eRemoteDB.Constants.intNull Then nInsur_area = 0
		If nBranch = eRemoteDB.Constants.intNull Then nBranch = 0
		If nProduct = eRemoteDB.Constants.intNull Then nProduct = 0
		If nPolicy = eRemoteDB.Constants.intNull Then nPolicy = 0
		
		lblnError = False
		'+ Validación área de seguro
		If nInsur_area = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55031)
		End If
		
		'+ Validación ramo
		If nBranch = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 9064)
			lblnError = True
		End If
		
		'+ Validación producto
		If nProduct = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 11009)
			lblnError = True
		End If
		
		'+ Validación póliza
		If nPolicy = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3003)
			lblnError = True
		End If
		
		If Not lblnError Then '+ validación del número de póliza
			If Not lobjPolicy.Find("2", nBranch, nProduct, nPolicy) Then
				lclsErrors.ErrorMessage(sCodispl, 3001)
				lblnError = True
			End If
			
			If Not lblnError Then
				If lobjPolicy.sStatus_pol > CollectionSeq.TypeStatus_Pol.cstrValid And lobjPolicy.sStatus_pol < CollectionSeq.TypeStatus_Pol.cstrPrintPendent Then
					lclsErrors.ErrorMessage(sCodispl, 3720)
					lblnError = True
				End If
			End If
		End If
		
		insValCOC747_k = lclsErrors.Confirm
		
insValCOC747_k_Err: 
		If Err.Number Then
			insValCOC747_k = insValCOC747_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insValCO634: Esta función se encarga de validar los datos introducidos en la cabecera de la forma.
	Public Function insValCO634(ByVal sCodispl As String, ByVal dStatdate As Date, ByVal nTypTras As Integer, ByVal nBranchOri As Integer, ByVal nBranchDes As Integer, ByVal nProductOri As Integer, ByVal nProductDes As Integer, ByVal nProponumOri As Double, ByVal nProponumDes As Double, ByVal nReceiptOri As Double, ByVal nReceiptDes As Double, ByVal nCurrencyOri As Integer, ByVal nCurrencyDes As Integer, ByVal nAmountOri As Double, ByVal nAmountDes As Double, ByVal nAmountTras As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPremium As eCollection.Premium
		Dim lclsBulletins_det As eCollection.Bulletins_det
		Dim lobjProduct As Object '+ ePolicy.Policy
		Dim lobjCashBank As Object
		Dim lobjCertificat As Object
		Dim lblnError As Boolean
		Dim lintInsur_areaOri As Integer
		Dim lintInsur_areaDes As Integer
        Dim lstrClientOri As String = ""
        Dim lstrClientDes As String = ""
        Dim lintStatus_preOri As Integer
		Dim ldblBalanceDes As Double
		
		lclsErrors = New eFunctions.Errors
		
		lobjProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
		lclsPremium = New eCollection.Premium
		lobjCashBank = eRemoteDB.NetHelper.CreateClassInstance("eCashBank.Move_Acc")
		lobjCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
		lclsBulletins_det = New eCollection.Bulletins_det
		
		On Error GoTo insValCO634_Err
		
		Dim lrecreaCtrol_Date As eRemoteDB.Execute
		With lclsErrors
			'+ Validación del tipo de traspaso 1)Recibo, 2)Propuesta
			If nTypTras = 2 Then
				'+ Validación del ramo origen
				If nBranchOri <= 0 Then
					.ErrorMessage(sCodispl, 60349)
					lblnError = True
				End If
				
				'+ Validación del ramo destino
				If nBranchDes <= 0 Then
					.ErrorMessage(sCodispl, 60357)
					lblnError = True
				End If
				
				'+ Validación del producto origen
				If nProductOri <= 0 Then
					.ErrorMessage(sCodispl, 60350)
					lblnError = True
				End If
				
				'+ Validación del producto destino
				If nProductDes <= 0 Then
					.ErrorMessage(sCodispl, 60358)
					lblnError = True
				Else
					If nBranchDes > 0 Then
						If Not lobjProduct.insValProdMaster(nBranchDes, nProductDes) Then
							.ErrorMessage(sCodispl, 9066)
							lblnError = True
						End If
					End If
				End If
				
				'+ Validación de la propuesta origen
				If nProponumOri <= 0 Then
					.ErrorMessage(sCodispl, 60351)
					lblnError = True
				Else
					If lobjCashBank.Find_nProponum_o(nBranchOri, nProductOri, nProponumOri) Then
						If lobjCashBank.sProcess_ind = "1" Then
							'+ Se verifica que el movimiento no este conciliado.
							.ErrorMessage(sCodispl, 55825)
							lblnError = True
						End If
					Else
						'+ Se verifica que este registrada en el sistema.
						.ErrorMessage(sCodispl, 55824)
						lblnError = True
					End If
				End If
				
				'+ Validación de la propuesta destino
				If nProponumDes <= 0 Then
					.ErrorMessage(sCodispl, 60359)
					lblnError = True
				Else
					If Not lobjCertificat.insValCertifByPropoNum(nBranchDes, nProductDes, nProponumDes) Then
						.ErrorMessage(sCodispl, 55997)
						lblnError = True
					End If
				End If
				
				'+ Se obtiene el área de seguros asociados a la propuesta a partir del tipo de producto.
				If Not lblnError Then
					lintInsur_areaOri = lobjProduct.getInsur_areaBysBrancht(nBranchOri, nProductOri)
					lintInsur_areaDes = lobjProduct.getInsur_areaBysBrancht(nBranchDes, nProductDes)
				End If
				
			Else
				'+ Validación del recibo origen
				If nReceiptOri <= 0 Then
					.ErrorMessage(sCodispl, 60352)
					lblnError = True
				Else
					If Not lclsPremium.FindPremiumExist("2", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nReceiptOri, 0, 0, 1) Then
						.ErrorMessage(sCodispl, 60356)
						lblnError = True
					Else
						lintStatus_preOri = lclsPremium.nStatus_pre
						'+ Validación de si las comisiones están liberadas
						If valComm_polLiberate(lclsPremium.sCertype, lclsPremium.nBranch, lclsPremium.nProduct, lclsPremium.nPolicy, lclsPremium.nCertif, lclsPremium.nReceipt) Then
							.ErrorMessage(sCodispl, 60353)
							lblnError = True
						End If
						
						'+ Si se trata de un recibo de Vida activa se verifica que no se haya calculado el valor póliza.
						If lclsPremium.nIndRecDep <> eRemoteDB.Constants.intNull Then
							If valPremium_ValuePolicy(lclsPremium.sCertype, lclsPremium.nBranch, lclsPremium.nProduct, lclsPremium.nPolicy, lclsPremium.nCertif, lclsPremium.nReceipt) Then
								.ErrorMessage(sCodispl, 60354)
								lblnError = True
							End If
						End If
						
						'+ Validación de que el recibo origen debe ser de cobro (nType = 1)
						If lclsPremium.nType <> Collec_Devolu.clngReceptable Then
							.ErrorMessage(sCodispl, 55826)
							lblnError = True
						End If
						
						lintInsur_areaOri = lclsPremium.nInsur_area
						lstrClientOri = lclsPremium.sClient
						
						'+ Si la fecha del movimiento (premium_mo) tiene valor.
						If dStatdate <> eRemoteDB.Constants.dtmNull Then
							lrecreaCtrol_Date = New eRemoteDB.Execute
							
							'+ Definición de parámetros para stored procedure 'insudb.reaCtrol_Date'
							With lrecreaCtrol_Date
								.StoredProcedure = "reaCtrol_Date"
								.Parameters.Add("nType_proce", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
								If .Run Then
									'+ Se valida la fecha del movimiento del pago (premium_mo.dStatdate) con respecto a la de inicio del período contable en vigor
									If dStatdate < lrecreaCtrol_Date.FieldToClass("dEffecdate") Then
										lclsErrors.ErrorMessage(sCodispl, 1006,  , eFunctions.Errors.TextAlign.RigthAling, "(Fecha de Movimiento: " & dStatdate & " - Fecha inicio contable: " & lrecreaCtrol_Date.FieldToClass("dEffecdate") & ")")
										lblnError = True
									End If
									.RCloseRec()
								End If
							End With
							'UPGRADE_NOTE: Object lrecreaCtrol_Date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							lrecreaCtrol_Date = Nothing
						End If
						
						If lclsPremium.nBulletins > 0 Then
							'+ Si el boletin asociado tiene otros documentos asociados.
							If lclsBulletins_det.valMoreDocBulletins_det(lclsPremium.nBulletins) Then
								lclsErrors.ErrorMessage(sCodispl, 55822)
								lblnError = True
							End If
						End If
					End If
					
				End If
				
				'+ Validación del recibo destino
				If nReceiptDes <= 0 Then
					.ErrorMessage(sCodispl, 55821)
					lblnError = True
				Else
					If Not lclsPremium.FindPremiumExist("2", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nReceiptDes, 0, 0, 1) Then
						.ErrorMessage(sCodispl, 60366)
						lblnError = True
					Else
						'+ Validación del estado del recibo destino debe ser pendiente.
						If lclsPremium.nStatus_pre = StatusReceipt.clngPendent Or lclsPremium.nStatus_pre = StatusReceipt.clngLodgedPendent Or lclsPremium.nStatus_pre = StatusReceipt.clngFinanced Then
						Else
							.ErrorMessage(sCodispl, 60364)
							lblnError = True
						End If
						
						'+ Si el recibo tiene via de pago PAC(1) o Transbank(2)
						If lclsPremium.nWay_Pay = 1 Or lclsPremium.nWay_Pay = 2 Then
							'+ Si ya fue enviado al banco para su cobro.
							If lclsPremium.nBulletins > 0 Then
								.ErrorMessage(sCodispl, 60362)
								lblnError = True
							End If
						End If
						
						lintInsur_areaDes = lclsPremium.nInsur_area
						lstrClientDes = lclsPremium.sClient
						ldblBalanceDes = lclsPremium.nBalance
					End If
				End If
			End If
			
			If Not lblnError Then
				'+ Se valida que los productos de los documentos en tratamiento no sean Unit Linked
				If lobjProduct.FindProduct_li(nBranchOri, nProductOri, Today) Then
					If lobjProduct.nProdClas = 4 Then
						.ErrorMessage(sCodispl, 56195)
						lblnError = True
					Else
						If lobjProduct.FindProduct_li(nBranchDes, nProductDes, Today) Then
							If lobjProduct.nProdClas = 4 Then
								.ErrorMessage(sCodispl, 56195)
								lblnError = True
							End If
						End If
					End If
				End If
				
				'+ Se valida que el área de seguros sea la misma para ambos documentos.
				If lintInsur_areaOri <> lintInsur_areaDes Then
					If nTypTras = 1 Then
						.ErrorMessage(sCodispl, 60365)
						lblnError = True
					Else
						.ErrorMessage(sCodispl, 60360)
						lblnError = True
					End If
				End If
				
				'+ Se verifica de que el documento origen no sea el mismo que el documento destino
				If nTypTras = 1 Then
					'+ Para un recibo
					If nBranchOri = nBranchDes And nProductOri = nProductDes And nReceiptOri = nReceiptDes Then
						.ErrorMessage(sCodispl, 55829)
						lblnError = True
					End If
				Else
					'+ Para una propuesta
					If nBranchOri = nBranchDes And nProductOri = nProductDes And nProponumOri = nProponumDes Then
						.ErrorMessage(sCodispl, 55829)
						lblnError = True
					End If
				End If
				
				'+ Si el tipo de traspaso es por recibo
				If nTypTras = 1 Then
					'+ Se valida que el cliente sea el mismo para ambos documentos.
					If lstrClientOri <> lstrClientDes Then
						.ErrorMessage(sCodispl, 55823)
						lblnError = True
					End If
				End If
				
				'+ Se valida que la moneda sea la misma para ambos documentos.
				If nCurrencyOri <> nCurrencyDes Then
					.ErrorMessage(sCodispl, 55807)
					lblnError = True
				End If
				
				'+ Se valida que el monto (nPremium) sea el mismo para ambos documentos.
				If nAmountOri <> nAmountDes Then
					If lclsPremium.nStatus_pre = StatusReceipt.clngFinanced Then
						.ErrorMessage(sCodispl, 56014)
					Else
						.ErrorMessage(sCodispl, 60367)
					End If
					lblnError = True
				End If
				
				'+ Se verificade que exista monto a traspasar
				If nAmountTras <= 0 Then
					.ErrorMessage(sCodispl, 55828)
					lblnError = True
				Else
					'+ Si corresponde con un traspaso de recibo a recibo.
					If nTypTras = 1 Then
						'+ Si el monto a traspasar es mayor que el disponible del recibo de destino
						If nAmountTras > ldblBalanceDes Then
							.ErrorMessage(sCodispl, 55827)
							lblnError = True
						End If
					End If
				End If
			End If
			
			insValCO634 = .Confirm
		End With
		
insValCO634_Err: 
		If Err.Number Then
			insValCO634 = insValCO634 & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lobjProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjProduct = Nothing
		'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPremium = Nothing
		'UPGRADE_NOTE: Object lobjCashBank may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjCashBank = Nothing
		'UPGRADE_NOTE: Object lclsBulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBulletins_det = Nothing
		On Error GoTo 0
	End Function
	
	'%insValCO633_k: Esta función se encarga de validar los datos introducidos en la cabecera de la forma.
	Public Function insValCO633_k(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal dOperation As Date, ByVal nTypOper As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dCollSus_ini As Date, ByVal dCollSus_end As Date, ByVal nSus_reason As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lobjPolicy As Object '+ ePolicy.Policy
		Dim lobjCertificat As Object '+ ePolicy.Certificat
		Dim lobjPremium As Object
		Dim lobjFinanc_dra As Object
		Dim lblnError As Boolean
		
		lclsErrors = New eFunctions.Errors
		lobjPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		lobjCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
		lobjPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
		lobjFinanc_dra = eRemoteDB.NetHelper.CreateClassInstance("eFinance.FinanceDraft")
		
		
		On Error GoTo insValCO633_k_Err
		
		lblnError = False
		
		With lclsErrors
			'+ Validación área de seguro
            '			If nInsur_area <= 0 Then
            '.ErrorMessage(sCodispl, 55031)
            'lblnError = False
            'End If

            '+ Validación ramo
            If nBranch <= 0 Then
                .ErrorMessage(sCodispl, 9064)
                lblnError = True
            End If

            '+ Validación producto
            If nProduct <= 0 Then
                .ErrorMessage(sCodispl, 11009)
                lblnError = True
            End If

            '+ Validación póliza
            If nPolicy <= 0 Then
                .ErrorMessage(sCodispl, 3003)
                lblnError = True
            End If

            '+ Se verifica que la póliza este registrada y se encuentre válida.
            If Not lblnError Then '+ validación del número de póliza
                If Not lobjPolicy.Find("2", nBranch, nProduct, nPolicy) Then
                    .ErrorMessage(sCodispl, 3001)
                    lblnError = True
                End If

                If Not lblnError Then
                    If lobjPolicy.sStatus_pol > CollectionSeq.TypeStatus_Pol.cstrValid And lobjPolicy.sStatus_pol < CollectionSeq.TypeStatus_Pol.cstrPrintPendent Then
                        .ErrorMessage(sCodispl, 3720)
                        lblnError = True
                    End If
                End If
                If Not lblnError Then
                    If lobjPolicy.sStatus_pol = "6" Then
                        .ErrorMessage(sCodispl, 3063)
                        lblnError = True
                    End If
                End If
                If Not lblnError Then
                    If lobjPolicy.sStatus_pol = "2" Then
                        .ErrorMessage(sCodispl, 3882)
                        lblnError = True
                    End If
                End If


            End If

            'Valida que el tipo de poliza no sea Individual
            If lobjPolicy.sPolitype <> "1" Then

                '+ Validaciones del certicado
                If nCertif < 0 Then
                    .ErrorMessage(sCodispl, 3006)
                    lblnError = True
                End If

                If Not lblnError Then

                    If Not lobjCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif) Then
                        .ErrorMessage(sCodispl, 8215)
                    ElseIf lobjCertificat.sStatusva > CollectionSeq.TypeStatus_Pol.cstrValid And lobjCertificat.sStatusva < CollectionSeq.TypeStatus_Pol.cstrPrintPendent Then
                        .ErrorMessage(sCodispl, 750044)
                    End If

                    If Not lblnError Then

                        If lobjCertificat.sStatusva = "6" Then
                            .ErrorMessage(sCodispl, 3063)
                            lblnError = True
                        End If

                    End If

                    If Not lblnError Then

                        If lobjCertificat.sStatusva = "2" Then
                            .ErrorMessage(sCodispl, 3882)
                            lblnError = True
                        End If

                    End If

                End If

            End If

            If Not lblnError Then
                If dCollSus_end <> eRemoteDB.Constants.dtmNull And lobjPolicy.dExpirDat <> eRemoteDB.Constants.dtmNull Then
                    '+ En caso de que se haya indicado una póliza la fecha fin de suspensión no debe ser mayor a la fecha de vencimiento de la póliza.
                    If dCollSus_end > lobjPolicy.dExpirDat Then
                        .ErrorMessage(sCodispl, 60258, , eFunctions.Errors.TextAlign.RigthAling, " (" & lobjPolicy.dExpirDat & ")")
                        lblnError = True
                    End If
                End If
            End If

            '+ Validación de la fecha de suspensión inicial.
            If dCollSus_ini = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 5072)
                lblnError = True
            End If

            '+ Validación de la fecha de suspensión final.
            If dCollSus_end = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 7164)
                lblnError = True
            End If

            '+ Validación de la fecha de suspensión inicial < fecha suspensión final
            If dCollSus_ini <> eRemoteDB.Constants.dtmNull And dCollSus_end <> eRemoteDB.Constants.dtmNull Then
                If dCollSus_ini > dCollSus_end Then
                    .ErrorMessage(sCodispl, 60113)
                    lblnError = True
                End If
            End If

            '+ Validación de la causa de suspensión
            If nSus_reason <= 0 Then
                .ErrorMessage(sCodispl, 60259)
                lblnError = True
            End If
            insValCO633_k = .Confirm
        End With
		
insValCO633_k_Err: 
		If Err.Number Then
			insValCO633_k = insValCO633_k & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%insValCO633A: Esta función se encarga de validar los datos introducidos en la cabecera de la forma.
	Public Function insValCO633A(ByVal sCodispl As String, ByVal sReceiptBulle As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValCO633A_Err
		
		With lclsErrors
			'+ Validación área de seguro
			If sReceiptBulle <> String.Empty Then
				.ErrorMessage(sCodispl, 60260,  , eFunctions.Errors.TextAlign.RigthAling, " (" & sReceiptBulle & ")")
			End If
			
			insValCO633A = .Confirm
		End With
		
insValCO633A_Err: 
		If Err.Number Then
			insValCO633A = insValCO633A & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insPostCO633A: Efectua la actualización de las tablas de la transacción.
	Public Function insPostCO633A(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal dOperation As Date, ByVal nTypOper As Integer, ByVal sSus_origi As String, ByVal nTypDoc As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double, ByVal nContrat As Double, ByVal nDraft As Integer, ByVal dCollSus_ini As Date, ByVal dCollSus_end As Date, ByVal nSus_reason As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecPremium As eRemoteDB.Execute
		
		lrecPremium = New eRemoteDB.Execute
		
		On Error GoTo insPostCO633A_Err
		
		'Definición de parámetros para stored procedure 'insudb.updPremiumCA034'
		'Información leída el 04/01/2001 14:24:08
		
		With lrecPremium
			.StoredProcedure = "insUpdCO633"
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypOper", nTypOper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypDoc", nTypDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollSus_ini", dCollSus_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollSus_end", dCollSus_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSus_reason", nSus_reason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSus_origi", sSus_origi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperation", dOperation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCO633A = .Run(False)
		End With
		
insPostCO633A_Err: 
		If Err.Number Then
			insPostCO633A = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPremium = Nothing
	End Function
	
	'%insAceptDataVerifyReceipt: Datos de verificación de recibos.
	Public Function insAcceptDataVerifyReceipt(ByVal ldblReceipt As Double, ByVal lstrCertype As String, ByVal llngDigit As Integer, ByVal llngPaynumbe As Integer, ByVal lstrGeneralNumerator As TypeNumeratorPOL_REC, ByVal llngBranch As Integer, ByVal llngProduct As Integer) As Boolean
		Dim lobjErrors As New eFunctions.Errors
		Dim lobjValues As New eFunctions.Values
		
		insAcceptDataVerifyReceipt = Me.Find_DataReceipt(lstrCertype, ldblReceipt, llngDigit, llngPaynumbe, CStr(lstrGeneralNumerator), llngBranch, llngProduct, 0, 0, True)
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		
	End Function
	
	'% insLoadReceiptsPerPolicy: Función que retorna todos los recibos emitidos para una póliza.
	'%** insLoadReceiptsPerPolicy: Function that returns all the receipts generated from a particular policy.
	Public Function insLoadReceiptsPerPolicy(ByVal lstrCertype As String, ByVal llngBranch As Integer, ByVal llngProduct As Integer, ByVal llngPolicy As Double, ByVal llngCertif As Double, ByVal ldtmEffecdate As Date) As Boolean
		Dim lrecDatPremiumPolicy As New eRemoteDB.Execute
		Dim lintCounter As Integer
		
		On Error GoTo insLoadReceiptsPerPolicy_Err
		lintCounter = 0
		'Definición de parámetros para stored procedure 'insudb.queDatPremiumPol'
		'Información leída el 02/12/1999 09:57:49 a.m.
		
		With lrecDatPremiumPolicy
			.StoredProcedure = "queDatPremiumPol"
			.Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", llngBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", llngProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insLoadReceiptsPerPolicy = .Run(True)
			If insLoadReceiptsPerPolicy Then
				ReDim arrReceipts(70)
				Do While Not .EOF
					lintCounter = lintCounter + 1
					arrReceipts(lintCounter).nReceipt = .FieldToClass("nReceipt")
					.RNext()
				Loop 
				ReDim Preserve arrReceipts(lintCounter)
				mblnCharge = True
			End If
			.RCloseRec()
		End With
		
insLoadReceiptsPerPolicy_Err: 
		If Err.Number Then
			insLoadReceiptsPerPolicy = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDatPremiumPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDatPremiumPolicy = Nothing
	End Function
	
	'% FindReceipt_Item: Devuelve "verdadero" si consigue coincidencia con el recibo pasado como parámetro. - ACM - 07/08/2001
	'% FindReceipt_Item: Returns "true" if the receipt number passed as parameter matches with the receipt number located into array. - ACM - 07-Aug-2001
	Public Function FindReceipt_Item(ByVal lintIndex As Integer) As Boolean
		Dim lintCount As Integer
		
		On Error GoTo FindReceipt_Item_err
		
		FindReceipt_Item = False
		lintCount = 0
		For lintCount = 0 To UBound(arrReceipts)
			If arrReceipts(lintCount).nReceipt = lintIndex Then
				FindReceipt_Item = True
				Exit For
			End If
		Next lintCount
		
FindReceipt_Item_err: 
		If Err.Number Then
			FindReceipt_Item = False
		End If
		
		On Error GoTo 0
		
	End Function
	
	'% Find_Receipt: Busca los datos correspondiente a un recibo en la tabla Premium.
	Public Function Find_Receipt_det(ByVal sCertype As String, ByVal nReceipt As Double, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecreaPremium_Receipt As eRemoteDB.Execute
		
		On Error GoTo Find_Receipt_det_err
		
		lrecreaPremium_Receipt = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaPremiumF_Receipt'
		'Información leída el 23/09/1999 1:02:48 PM
		
		With lrecreaPremium_Receipt
			.StoredProcedure = "reaPremium_Receipt_o"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.sCertype = .FieldToClass("sCertype")
				Me.nReceipt = .FieldToClass("nReceipt")
				Me.nBranch = .FieldToClass("nBranch")
				Me.sDesBranch = .FieldToClass("sBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.sDescProd = .FieldToClass("sProduct")
				Me.nDigit = .FieldToClass("nDigit")
				Me.nPaynumbe = .FieldToClass("nPaynumbe")
				Me.sClient = .FieldToClass("sClient")
				Me.sCliename = .FieldToClass("sCliename")
				Me.sCessions = .FieldToClass("sCessions")
				Me.sDirdebit = .FieldToClass("sDirdebit")
				Me.sLeadinvo = .FieldToClass("sLeadinvo")
				Me.sManauti = .FieldToClass("sManauti")
				Me.sRenewal = .FieldToClass("sRenewal")
				Me.sStatusva = .FieldToClass("sStatusva")
				Me.sSubstiti = .FieldToClass("sSubstiti")
				Me.sConColl = .FieldToClass("sConColl")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.dExpirDat = .FieldToClass("dExpirdat")
				Me.dIssuedat = .FieldToClass("dIssuedat")
				Me.dNulldate = .FieldToClass("dNulldate")
				Me.dPayDate = .FieldToClass("dPaydate")
				Me.dStatdate = .FieldToClass("dStatdate")
				Me.nBalance = .FieldToClass("nBalance")
				Me.nComamou = .FieldToClass("nComamou")
				Me.nExchange = .FieldToClass("nExchange")
				Me.nIntammou = .FieldToClass("nIntammou")
				Me.nParticip = .FieldToClass("nParticip")
				Me.nPremium = .FieldToClass("nPremium")
				Me.nPremiuml = .FieldToClass("nPremiuml")
				Me.nPremiumn = .FieldToClass("nPremiumn")
				Me.nPremiums = .FieldToClass("nPremiums")
				Me.nRate = .FieldToClass("nRate")
				Me.nTaxamou = .FieldToClass("nTaxamou")
				Me.nCollecto = .FieldToClass("nCollecto")
				Me.nContrat = .FieldToClass("nContrat")
				Me.nInspecto = .FieldToClass("nInspecto")
				Me.nIntermed = .FieldToClass("nIntermed")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nSustit = .FieldToClass("nSustit")
				Me.nTransactio = .FieldToClass("nTransactio")
				Me.nStatus_pre = .FieldToClass("nStatus_pre")
				Me.nNullcode = .FieldToClass("nNullcode")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.nNoteNum = .FieldToClass("nNotenum")
				Me.nOffice = .FieldToClass("nOffice")
				Me.nType = .FieldToClass("nType")
				Me.nTratypei = .FieldToClass("nTratypei")
				Me.sDesTratypei = .FieldToClass("sTratypei")
				Me.nUsercode = .FieldToClass("nUsercode")
				Me.nPeriod = .FieldToClass("nPeriod")
				Me.nCompany = .FieldToClass("nCompany")
				Me.sOrigReceipt = .FieldToClass("sOrigReceipt")
				Me.sCurrency = .FieldToClass("sCurrency")
				Me.nWay_Pay = .FieldToClass("nWay_Pay")
				Me.dLimitdate = .FieldToClass("dLimitdate")
				Find_Receipt_det = True
				.RCloseRec()
			Else
				Find_Receipt_det = False
			End If
		End With
		
Find_Receipt_det_err: 
		If Err.Number Then
			Find_Receipt_det = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremium_Receipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_Receipt = Nothing
	End Function
	
	'%insValCAL002_K: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValCAL002_K(ByVal sCodispl As String, ByVal nOffice As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nRec_Beg As Double, ByVal nRec_End As Double, ByVal nCon_Beg As Double, ByVal nCon_End As Double, ByVal dStarDate As Date, ByVal dEndDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPolicy As Object
		Dim lclsCertificat As Object
		Dim lblnError As Boolean
		On Error GoTo insValCAL002_K_Err
		lclsErrors = New eFunctions.Errors
		lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
		Static lstrValField As String
		If nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0 Then
			'+      Valida la poliza
			If Not lblnError Then
				With lclsPolicy
					If Not .Find("2", nBranch, nProduct, nPolicy) Then
						'+                  Si la póliza no está registrada
						Call lclsErrors.ErrorMessage(sCodispl, 3001)
						lblnError = True
					Else
						'+                  Se verifica que la póliza esté válida
						If .sStatus_pol = CollectionSeq.TypeStatus_Pol.cstrIncomplete Or .sStatus_pol = CollectionSeq.TypeStatus_Pol.cstrInvalid Then
							Call lclsErrors.ErrorMessage(sCodispl, 3720)
							lblnError = True
						Else
							'+                      Se verifica que la póliza no esté anulada
							If .dNulldate <> eRemoteDB.Constants.dtmNull Then
								Call lclsErrors.ErrorMessage(sCodispl, 3098)
								lblnError = True
							End If
						End If
					End If
				End With
			End If
			'       Poliza con vía de pago cuponera
			If Not lblnError Then
				With lclsCertificat
					If .Find("2", nBranch, nProduct, nPolicy, 0) Then
						If lclsCertificat.nWay_Pay <> 5 Then
							Call lclsErrors.ErrorMessage(sCodispl, 60255)
							lblnError = True
						End If
					Else
						Call lclsErrors.ErrorMessage(sCodispl, 60255)
						lblnError = True
					End If
				End With
			End If
			'+Si incluyó intervalo de fechas las valida
			If Not lblnError Then
				If dEndDate <> eRemoteDB.Constants.dtmNull Then
					If dStarDate = eRemoteDB.Constants.dtmNull Then
						Call lclsErrors.ErrorMessage(sCodispl, 6128)
						lblnError = True
					Else
						If dStarDate > dEndDate Then
							Call lclsErrors.ErrorMessage(sCodispl, 6130)
							lblnError = True
						End If
					End If
				End If
			End If
			
			If Not lblnError Then
				If nRec_Beg > 0 Then
					If Not Find_Receipt_exist(nRec_Beg) Then
						Call lclsErrors.ErrorMessage(sCodispl, 55895)
					Else
						If Not Find_Receipt_exist(nRec_Beg) And sStatusva <> "4" Then
							Call lclsErrors.ErrorMessage(sCodispl, 3984)
						End If
					End If
				End If
				If nRec_End > 0 Then
					If Not Find_Receipt_exist(nRec_End) Then
						Call lclsErrors.ErrorMessage(sCodispl, 55896)
					Else
						If Not Find_Receipt_exist(nRec_End) And sStatusva <> "4" Then
							Call lclsErrors.ErrorMessage(sCodispl, 3984)
						End If
					End If
				End If
				If nCon_Beg > 0 Then
					If Not Find_Contrat_exist(nCon_Beg, Today) Then
						Call lclsErrors.ErrorMessage(sCodispl, 55897)
					End If
				End If
				If nCon_End > 0 Then
					If Not Find_Contrat_exist(nCon_End, Today) Then
						Call lclsErrors.ErrorMessage(sCodispl, 55898)
					End If
				End If
			End If
		End If
		
		insValCAL002_K = lclsErrors.Confirm
		
insValCAL002_K_Err: 
		If Err.Number Then
			insValCAL002_K = insValCAL002_K & Err.Description
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
	End Function
	
	'% Find: Entrega el número máximo y mínimo de nReceipt
	Public Function Find_MaxMin_nReceipt(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecreaPremium_nReceipt As eRemoteDB.Execute
		On Error GoTo Find_MaxMin_nReceipt_Err
		lrecreaPremium_nReceipt = New eRemoteDB.Execute
		
		Find_MaxMin_nReceipt = False
		
		With lrecreaPremium_nReceipt
			.StoredProcedure = "reaMaxMin_nReceipt"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nReceipt_Max = .FieldToClass("nReceipt_Max")
				Me.nReceipt_Min = .FieldToClass("nReceipt_Min")
				Find_MaxMin_nReceipt = True
			Else
				Find_MaxMin_nReceipt = False
			End If
			
		End With
		
Find_MaxMin_nReceipt_Err: 
		If Err.Number Then
			Find_MaxMin_nReceipt = False
		End If
		'UPGRADE_NOTE: Object lrecreaPremium_nReceipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_nReceipt = Nothing
	End Function
	
	'% Find: Entrega el número máximo y mínimo de nContrat
	Public Function Find_MaxMin_nContrat(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecreaPremium_nContrat As eRemoteDB.Execute
		On Error GoTo Find_MaxMin_nContrat_Err
		lrecreaPremium_nContrat = New eRemoteDB.Execute
		
		Find_MaxMin_nContrat = False
		
		With lrecreaPremium_nContrat
			.StoredProcedure = "reaMaxMin_nContrat"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nContrat_Max = .FieldToClass("nContrat_Max")
				Me.nContrat_Min = .FieldToClass("nContrat_Min")
				Find_MaxMin_nContrat = True
			Else
				Find_MaxMin_nContrat = False
			End If
			
		End With
		
Find_MaxMin_nContrat_Err: 
		If Err.Number Then
			Find_MaxMin_nContrat = False
		End If
		'UPGRADE_NOTE: Object lrecreaPremium_nContrat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_nContrat = Nothing
	End Function
	
	
	'% Find: Verifica si existe el recibo en premium
	Public Function Find_Contrat_exist(ByVal nContrat As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaFinance_co_nContrat As eRemoteDB.Execute
		On Error GoTo Find_Contrat_exist_Err
		lrecreaFinance_co_nContrat = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.REAFINANCE_CO_NCONTRAT'
		Find_Contrat_exist = False
		
		With lrecreaFinance_co_nContrat
			.StoredProcedure = "reaFinance_co_nContrat"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nContrat = .FieldToClass("nContrat")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Find_Contrat_exist = True
			Else
				Find_Contrat_exist = False
			End If
		End With
		
Find_Contrat_exist_Err: 
		If Err.Number Then
			Find_Contrat_exist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaFinance_co_nContrat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFinance_co_nContrat = Nothing
	End Function
	'% Find: Verifica si existe el recibo ramo en premium
	Public Function Find_Receipt_Branch(ByVal nReceipt As Double, ByVal nBranch As Integer) As Boolean
		Dim lrecreaPremium_RecBra As eRemoteDB.Execute
		
		lrecreaPremium_RecBra = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaPremium_nReceipt'
		'Información leída el 01/10/2001 11:49:27 a.m.
		Find_Receipt_Branch = False
		
		With lrecreaPremium_RecBra
			.StoredProcedure = "reaPremium_RecBra"
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nBranch = nBranch
				Me.nProduct = .FieldToClass("nProduct")
				Me.nPolicy = .FieldToClass("nPolicy")
				Find_Receipt_Branch = True
			Else
				Find_Receipt_Branch = False
			End If
			
		End With
		'UPGRADE_NOTE: Object lrecreaPremium_RecBra may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_RecBra = Nothing
		
	End Function
	
	'**% This function is in charge to validate the introduced data of page COL007 -
	'**% Reporte of deferred check control.
	'% insValCOL007: Esta función se encarga de validar los datos introducidos de la página COL007 -
	'% Reporte de control de cheques diferidos.
	Public Function insValCOL007(ByVal sCodispl As String, ByVal nOffice As Integer, ByVal nIntermed As Double, ByVal dEffecdate As Date, ByVal dPendDate As Date) As String
		Dim lerrTime As eFunctions.Errors
		'    Dim lclsAgents As eAgent.Agents
		Dim lclsAgents As Object
		
		lerrTime = New eFunctions.Errors
		'    Set lclsAgents = New eAgent.Agents
		lclsAgents = eRemoteDB.NetHelper.CreateClassInstance("eAgent.Agents")
		
		On Error GoTo insValCOL007_Err
		
		'**+ Been worth "the Producing" field.
		'+ Se valida el campo "Intermediario".
		If nIntermed <> 0 And nIntermed <> eRemoteDB.Constants.intNull Then
			If Not lclsAgents.findIntermediaClient(nIntermed, 1, Today) Then
				Call lerrTime.ErrorMessage(sCodispl, 3634)
			End If
		End If
		
		'**+ Been worth the field "Date - Effective to" and "Date - Pending to".
		'+ Se valida el campo "Fecha - Efectivos al" y "Fecha - Pendientes al".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If (IsNothing(dEffecdate) Or dEffecdate = eRemoteDB.Constants.dtmNull) And (IsNothing(dPendDate) Or dPendDate = eRemoteDB.Constants.dtmNull) Then
			Call lerrTime.ErrorMessage(sCodispl, 750052)
		Else
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			If (Not IsNothing(dEffecdate) And dEffecdate <> eRemoteDB.Constants.dtmNull) And (Not IsNothing(dPendDate) And dPendDate <> eRemoteDB.Constants.dtmNull) Then
				Call lerrTime.ErrorMessage(sCodispl, 750051)
			End If
		End If
		
		insValCOL007 = lerrTime.Confirm
		
insValCOL007_Err: 
		If Err.Number Then
			insValCOL007 = insValCOL007 & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
	End Function
	
	'% InsValPendendReceipt: Valida si la póliza tiene recibos pendientes
	Public Function InsValPendendReceipt(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nStatusPre As Integer) As Boolean
		Dim lrecreaPremiumrecpen As eRemoteDB.Execute
		
		On Error GoTo reaPremiumrecpen_Err
		lrecreaPremiumrecpen = New eRemoteDB.Execute
		'+ Definición de store procedure reaPremiumrecpen al 12-29-2001 16:23:42
		With lrecreaPremiumrecpen
			.StoredProcedure = "reaPremiumrecpen"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatuspre", nStatusPre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValPendendReceipt = .Parameters("nCount").Value > 0
			End If
		End With
		
reaPremiumrecpen_Err: 
		If Err.Number Then
			InsValPendendReceipt = False
		End If
		'UPGRADE_NOTE: Object lrecreaPremiumrecpen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremiumrecpen = Nothing
		On Error GoTo 0
	End Function
	
	'% FindLastPayDate: Obtiene la fecha del último recibo cobrado de la póliza
	Public Function FindLastPayDate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecreaPremiumexpirdat As eRemoteDB.Execute
		
		On Error GoTo FindLastPayDate_Err
		
		'+ Definición de store procedure reaPremiumexpirdat al 01-04-2002 10:00:45
		lrecreaPremiumexpirdat = New eRemoteDB.Execute
		With lrecreaPremiumexpirdat
			.StoredProcedure = "reaPremiumexpirdat"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				dExpirDat = IIf(IsDbNull(.Parameters("dExpirdat").Value), eRemoteDB.Constants.dtmNull, .Parameters("dExpirdat").Value)
				FindLastPayDate = True
			End If
		End With
		
FindLastPayDate_Err: 
		If Err.Number Then
			FindLastPayDate = False
		End If
		'UPGRADE_NOTE: Object lrecreaPremiumexpirdat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremiumexpirdat = Nothing
		On Error GoTo 0
	End Function
	
	'%insValCO635_k: Esta función se encarga de validar los datos introducidos en la cabecera de la forma.
	Public Function insValCO635_k(ByVal sCodispl As String, ByVal nCollector As Double) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCollector As eCollection.Collector
		Dim lblnError As Boolean
		
		lclsErrors = New eFunctions.Errors
		lclsCollector = New eCollection.Collector
		
		On Error GoTo insValCO635_k_Err
		
		lblnError = False
		
		'+Validacion del Cobrador
		If nCollector = eRemoteDB.Constants.intNull Or nCollector = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60272)
			lblnError = True
		End If
		With lclsCollector
			If Not .Find(nCollector) And Not lblnError Then
				Call lclsErrors.ErrorMessage(sCodispl, 60276)
			End If
		End With
		
		insValCO635_k = lclsErrors.Confirm
		
insValCO635_k_Err: 
		If Err.Number Then
			insValCO635_k = insValCO635_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsCollector may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCollector = Nothing
	End Function
	
	'% insPostCO634: Se realiza la actualización de los datos en la ventana CO634
	Public Function insPostCO634(ByVal nTypTras As Integer, ByVal sCertypeOri As String, ByVal sCertypeDes As String, ByVal nBranchOri As Integer, ByVal nBranchDes As Integer, ByVal nProductOri As Integer, ByVal nProductDes As Integer, ByVal nProponumOri As Double, ByVal nProponumDes As Double, ByVal nReceiptOri As Double, ByVal nReceiptDes As Double, ByVal nAmountTrans As Double, ByVal nContratOri As Double, ByVal nContratDes As Double, ByVal nDraftOri As Integer, ByVal nDraftDes As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecPremium As eRemoteDB.Execute
		
		On Error GoTo insPostCO634_Err
		
		lrecPremium = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdco634 al 03-16-2002 19:00:07
		'+
		With lrecPremium
			.StoredProcedure = "insUpdco634"
			.Parameters.Add("nTyptras", nTypTras, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertypeori", sCertypeOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertypedes", sCertypeDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranchori", nBranchOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranchdes", nBranchDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProductori", nProductOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProductdes", nProductDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponumori", nProponumOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponumdes", nProponumDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceiptori", nReceiptOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceiptdes", nReceiptDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountTrans", nAmountTrans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContratOri", nContratOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContratDes", nContratDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraftOri", nDraftOri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraftDes", nDraftDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCO634 = .Run(False)
		End With
		
insPostCO634_Err: 
		If Err.Number Then insPostCO634 = False
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPremium = Nothing
	End Function
	
	'% insPostCO635: Se realiza la actualización de los datos en la ventana CO635
	Public Function insPostCO635(ByVal sCodispl As String, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nCollector As Double, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal sCertype As String, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nReceipt As Double, ByVal nContrat As Double, ByVal nDraft As Integer, ByVal nBulletins As Double, ByVal nSelAux As Integer, ByVal nPos As Integer) As Boolean
		
		On Error GoTo insPostCO635_Err
		
		Dim lclsPremium As eCollection.Premium
		Dim lobjValues As eFunctions.Values
		
		lclsPremium = New eCollection.Premium
		lobjValues = New eFunctions.Values
		
		insPostCO635 = True
		
		With lclsPremium
			.nBranch = nBranch
			.sCertype = sCertype
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nReceipt = nReceipt
			.nContrat = nContrat
			.dEffecdate = dEffecdate
			.nDraft = nDraft
			.nBulletins = nBulletins
			.nUsercode = nUsercode
			.nCollector = nCollector
			
			'+Si está modificando se eliminan los registros que se hayan deseleccionado
			If sAction = "Del" Then
				insPostCO635 = .InsUpdDelCO635(nReceipt, nProduct, nBranch, sCertype, dEffecdate, nUsercode, nCollector, nContrat, nDraft)
			Else
				'+Si está registrando se agregan los registros
				If nSelAux = 1 Then
					insPostCO635 = .insUpdCO635(nReceipt, nProduct, nBranch, sCertype, dEffecdate, nUsercode, nCollector, nContrat, nDraft, nBulletins)
				End If
			End If
		End With
insPostCO635_Err: 
		If Err.Number Then insPostCO635 = False
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPremium = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
	End Function
	
	'% UInsUpdCO635: Permite asignar una cartera a un cobrador
	Public Function insUpdCO635(ByVal nReceipt As Integer, ByVal nProduct As Integer, ByVal nBranch As Integer, ByVal sCertype As String, ByVal dStatdate As Date, ByVal nUsercode As Integer, ByVal nCollector As Double, ByVal nContrat As Double, ByVal nDraft As Integer, ByVal nBulletins As Double) As Boolean
		Dim lrecinsUpdCO635 As eRemoteDB.Execute
		
		On Error GoTo insUpdco635_Err
		
		lrecinsUpdCO635 = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdco635 al 02-20-2002 15:26:08
		'+
		With lrecinsUpdCO635
			.StoredProcedure = "insUpdco635"
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStatdate", dStatdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insUpdCO635 = .Run(False)
		End With
		
insUpdco635_Err: 
		If Err.Number Then
			insUpdCO635 = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdCO635 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdCO635 = Nothing
		On Error GoTo 0
		
	End Function
	'% InsBulletinsNullCO635: Permite anular el boletín si no se seleccionaron todos
	'% los recibos o cuotas asociados a él
	Public Function InsBulletinsNullCO635(ByVal nCollector As Double, ByVal nUsercode As Integer) As Boolean
		Dim lrecreaCo635bull_coll As eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo reaCo635bull_coll_Err
		
		lrecreaCo635bull_coll = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaCo635bull_coll al 02-23-2002 16:06:24
		'+
		'+ Se recorre el arreglo
		
		InsBulletinsNullCO635 = True
		For lintCount = 1 To UBound(marrBulletins)
			If marrBulletins(lintCount) <> eRemoteDB.Constants.intNull Then
				With lrecreaCo635bull_coll
					.StoredProcedure = "insupdCO635bull_coll"
					.Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nBulletins", marrBulletins(lintCount), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
					If .Run(False) Then
						InsBulletinsNullCO635 = True
						
					Else
						InsBulletinsNullCO635 = False
					End If
				End With
			End If
		Next lintCount
reaCo635bull_coll_Err: 
		If Err.Number Then
			InsBulletinsNullCO635 = False
		End If
		'UPGRADE_NOTE: Object lrecreaCo635bull_coll may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCo635bull_coll = Nothing
		On Error GoTo 0
		
	End Function
	'% UInsUpdDelCO635: Permite borrar una cartera a un cobrador
	Public Function InsUpdDelCO635(ByVal nReceipt As Integer, ByVal nProduct As Integer, ByVal nBranch As Integer, ByVal sCertype As String, ByVal dStatdate As Date, ByVal nUsercode As Integer, ByVal nCollector As Double, ByVal nContrat As Double, ByVal nDraft As Integer) As Boolean
		Dim lrecinsUpddelcollectorco635 As eRemoteDB.Execute
		On Error GoTo insUpddelcollectorco635_Err
		
		lrecinsUpddelcollectorco635 = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpddelcollectorco635 al 02-25-2002 22:54:24
		'+
		With lrecinsUpddelcollectorco635
			.StoredProcedure = "insUpddelcollectorco635"
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStatdate", dStatdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdDelCO635 = .Run(False)
		End With
		
insUpddelcollectorco635_Err: 
		If Err.Number Then
			InsUpdDelCO635 = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpddelcollectorco635 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpddelcollectorco635 = Nothing
		On Error GoTo 0
		
	End Function
	
	'% valComm_polLiberate: Valida si las comisiones se encuentran liberadas para la póliza pasada como parámetro.
	'% Devuelve: True -> Si están liberadas; False -> Si no están liberadas.
	Public Function valComm_polLiberate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double) As Boolean
		Dim lrecComm_pol As eRemoteDB.Execute
		
		lrecComm_pol = New eRemoteDB.Execute
		
		On Error GoTo valComm_polLiberate_Err
		
		With lrecComm_pol
			.StoredProcedure = "valComm_polLiberate"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters.Item("nExists").Value = 1 Then
					valComm_polLiberate = True
				End If
			End If
			
		End With
		
valComm_polLiberate_Err: 
		If Err.Number Then
			valComm_polLiberate = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecComm_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecComm_pol = Nothing
	End Function
	
	'% valPremium_ValuePolicy: Valida se efectuó el calculo del valor póliza para los recibos de Vida activa.
	'% Devuelve: True -> Si se efectuó el cálculo; False -> Si no se efectuó el cálculo.
	Public Function valPremium_ValuePolicy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double) As Boolean
		Dim lrecPremium As eRemoteDB.Execute
		
		lrecPremium = New eRemoteDB.Execute
		
		On Error GoTo valPremium_ValuePolicy_Err
		
		With lrecPremium
			.StoredProcedure = "valPremium_ValuePolicy"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters.Item("nExists").Value = 1 Then
					valPremium_ValuePolicy = True
				End If
			End If
		End With
		
valPremium_ValuePolicy_Err: 
		If Err.Number Then
			valPremium_ValuePolicy = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPremium = Nothing
	End Function
	
	'%insValCOC009_k: Esta función se encarga de validar los datos introducidos en la cabecera de la forma.
	Public Function insValCOC009_k(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, ByVal sReceiptNum As String) As String
		Dim lclsErrors As eFunctions.Errors

		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValCOC009_k_Err
		
		With lclsErrors
			'+ Validación del Ramo si la opcion de instalación es por ramo-recibo o por ramo-producto-recibo
			If (sReceiptNum = "2" Or sReceiptNum = "3") And nBranch = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1022)
			End If
			
			'+ Validación producto si el ramo está lleno y la opción de instalación es por ramo-producto-recibo
			If nBranch <> eRemoteDB.Constants.intNull And sReceiptNum = "3" And nProduct = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 3635)
			End If
			
			'+ Validación recibo
			If nReceipt = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 5053)
			End If
			
			'+ Validación existencia sólo si no es nulo el número de recibo
			If nReceipt <> eRemoteDB.Constants.intNull Then
                If Not valPremiumExist_COC009(sCertype, nBranch, nProduct, nReceipt, nDigit, nPaynumbe, TypeNumeratorPOL_REC.cstrSysNumeGeneral) Then
                    Call .ErrorMessage(sCodispl, 5004)
                End If
			End If
			insValCOC009_k = .Confirm
		End With
		
insValCOC009_k_Err: 
		If Err.Number Then
			insValCOC009_k = insValCOC009_k & Err.Description
		End If
		On Error GoTo 0
        lclsErrors = Nothing
	End Function
	
	'%valReceipt_Paynumbe: Valida si el recibo ingresado esta en convenio de cobranzas.
	Public Function valReceipt_Paynumbe(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nReceipt As Double, ByVal nDigit As Integer) As Boolean
		Dim lrecPremium As eRemoteDB.Execute
		Dim llngExists As Integer
		
		On Error GoTo valReceipt_Paynumbe_Err
		
		lrecPremium = New eRemoteDB.Execute
		
		With lrecPremium
			.StoredProcedure = "valReceipt_Paynumbe"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			valReceipt_Paynumbe = (.Parameters.Item("nExists").Value = 1)
		End With
		
valReceipt_Paynumbe_Err: 
		If Err.Number Then
			valReceipt_Paynumbe = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPremium = Nothing
	End Function
	
	'%valExistsCO003_K: Valida si el recibo ingresado esta en convenio de cobranzas.
	Public Function valExistsCO003_K(ByVal nReceipt As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecPremium As eRemoteDB.Execute
		Dim llngExists As Integer
		
		On Error GoTo valExistsCO003_K_Err
		
		lrecPremium = New eRemoteDB.Execute
		
		With lrecPremium
			.StoredProcedure = "valExistsCO003_K"
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters.Item("nExists").Value = 1 Then
					valExistsCO003_K = True
				End If
			End If
		End With
		
valExistsCO003_K_Err: 
		If Err.Number Then
			valExistsCO003_K = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPremium = Nothing
	End Function
	
	'%getMaxPaydate. Este metodo se encarga de realizar la busqueda de la maxima fecha del convenio
	Public Function getMaxPaydate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nReceipt As Double) As Date
		Dim lrecPremiumd As eRemoteDB.Execute
		Dim ldtmPaydate As Date
		
		On Error GoTo getMaxPaydate_Err
		lrecPremiumd = New eRemoteDB.Execute
		
		getMaxPaydate = eRemoteDB.Constants.dtmNull
		
		With lrecPremiumd
			.StoredProcedure = "reaMaxPaydate"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPayDate", ldtmPaydate, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				getMaxPaydate = .Parameters("dPaydate").Value
			End If
		End With
		
getMaxPaydate_Err: 
		If Err.Number Then
			getMaxPaydate = eRemoteDB.Constants.dtmNull
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecPremiumd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPremiumd = Nothing
	End Function
	
	'%calCO003. Este metodo se encarga de realizar los cálculos para obtener el interés del pago y el monto del pago
	Public Function calCO003(ByVal dEffecdate As Date, ByVal dPayDate As Date, ByVal nIntammou As Double, ByVal nRate As Double) As Boolean
		Dim lrecPremium As eRemoteDB.Execute
		Dim ldtmPaydate As Date
		Dim ldblIntAmmouPay As Double
		Dim ldblRatePay As Double
		
		On Error GoTo calCO003_Err
		lrecPremium = New eRemoteDB.Execute
		
		With lrecPremium
			.StoredProcedure = "calAmountsCO003"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPaydate", dPayDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntammou", nIntammou, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntammoupay", ldblIntAmmouPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatepay", ldblRatePay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Me.nIntAmmouPay = .Parameters("nIntAmmouPay").Value
				Me.nRatePay = .Parameters("nRatePay").Value
				calCO003 = True
			End If
		End With
		
calCO003_Err: 
		If Err.Number Then
			calCO003 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPremium = Nothing
	End Function
	
	'%getMaxPayNumbe. Este metodo se encarga de realizar la busqueda de la maxima fecha del convenio
	Public Function getMaxPayNumbe(ByVal nReceipt As Double) As Integer
		Dim lrecPremium As eRemoteDB.Execute
		Dim lintPayNumbe As Integer
		
		On Error GoTo getMaxPayNumbe_Err
		lrecPremium = New eRemoteDB.Execute
		
		getMaxPayNumbe = 0
		
		With lrecPremium
			.StoredProcedure = "reaMaxPayNumbe"
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayNumbe", lintPayNumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				getMaxPayNumbe = .Parameters("nPayNumbe").Value
			End If
		End With
		
getMaxPayNumbe_Err: 
		If Err.Number Then
			getMaxPayNumbe = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPremium = Nothing
	End Function
	
	
	'% Item: Carga en la variable "nReceipt" de la clase la información obtenida
	'%       en la función "insLoadReceiptsPerPolicy"
	Public Function Item(ByVal llngIndex As Integer) As Boolean
		If mblnCharge Then
			If llngIndex <= UBound(arrReceipts) Then
				nReceipt = arrReceipts(llngIndex).nReceipt
				Item = True
			Else
				Item = False
			End If
		End If
	End Function
	
	'% Find_Premium_ca001: rescata el valor de un recibo para una poliza/certificado
	Public Function Find_Premium_CA001(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecreaPremium_ca001 As eRemoteDB.Execute
		Dim lclsreaPremium_ca001 As Premium
		
		On Error GoTo reaPremium_ca001_Err
		
		lrecreaPremium_ca001 = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaPremium_ca001 al 06-11-2002 12:13:38
		With lrecreaPremium_ca001
			.StoredProcedure = "reaPremium_ca001"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sList", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				Me.nPremium = IIf(IsDbNull(.Parameters("nPremium").Value), eRemoteDB.Constants.intNull, .Parameters("nPremium").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				Me.nBulletins = IIf(IsDbNull(.Parameters("nBulletins").Value), eRemoteDB.Constants.intNull, .Parameters("nBulletins").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				Me.nReceipt = IIf(IsDbNull(.Parameters("nReceipt").Value), eRemoteDB.Constants.intNull, .Parameters("nReceipt").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				Me.dExpirDat = IIf(IsDbNull(.Parameters("dExpirDat").Value), eRemoteDB.Constants.dtmNull, .Parameters("dExpirDat").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				Me.nWay_Pay = IIf(IsDbNull(.Parameters("nWay_Pay").Value), eRemoteDB.Constants.intNull, .Parameters("nWay_Pay").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				Me.sListReceipt = IIf(IsDbNull(.Parameters("sList").Value), "", .Parameters("sList").Value)
				If Me.sListReceipt <> "" Then
					Me.sListReceipt = Mid(sListReceipt, 2)
				End If
				Find_Premium_CA001 = True
			Else
				Find_Premium_CA001 = False
			End If
		End With
		
reaPremium_ca001_Err: 
		If Err.Number Then
			Find_Premium_CA001 = False
		End If
		'UPGRADE_NOTE: Object lrecreaPremium_ca001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_ca001 = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValReceipt_by_status: Valida si la póliza tiene recibos según el estado
	Public Function InsValReceipt_by_status(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStatus_pre As Integer, ByVal nTratypei As Integer) As Boolean
		Dim lrecreaPremium_by_status As eRemoteDB.Execute
		
		On Error GoTo InsValReceipt_by_status_Err
		'+ Definición de store procedure reaPremium_by_status al 08-06-2002 13:28:35
		lrecreaPremium_by_status = New eRemoteDB.Execute
		With lrecreaPremium_by_status
			.StoredProcedure = "reaPremium_by_status"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_pre", nStatus_pre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValReceipt_by_status = .Parameters("nCount").Value > 0
				nPremium = .Parameters("nPremium").Value
			End If
		End With
		
InsValReceipt_by_status_Err: 
		If Err.Number Then
			InsValReceipt_by_status = False
		End If
		'UPGRADE_NOTE: Object lrecreaPremium_by_status may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium_by_status = Nothing
		On Error GoTo 0
	End Function
	
	'% FindPolicyIssue: devuelve los datos del recibo generado durante la emisión de la póliza
	Public Function FindPolicyIssue(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo FindPolicyIssue_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "reaPremium_PolicyIssue"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				nReceipt = .FieldToClass("nReceipt")
				dExpirDat = .FieldToClass("dExpirDat")
				FindPolicyIssue = True
				.RCloseRec()
			End If
		End With
		
FindPolicyIssue_Err: 
		If Err.Number Then
			FindPolicyIssue = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'%InsPreCA017: Genera los valores necesarios para mostrar el recibo
	Public Function InsPreCA017(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal nUsercode As Integer, ByVal sBrancht As String) As Boolean
		
		Dim lrecinsPreca017 As eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsPreCA017'
		
		On Error GoTo InsPreCA017_Err
		
		lrecinsPreca017 = New eRemoteDB.Execute
		With lrecinsPreca017
            .StoredProcedure = "INSPRECA017"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sList", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nreceipt_default", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColinvot", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsPreCA017 = .Parameters("nreceipt_default").Value > 0
				If InsPreCA017 Then
					With Me
						.sPolitype = Trim(lrecinsPreca017.Parameters("sPolitype").Value)
						.sColinvot = Trim(lrecinsPreca017.Parameters("sColinvot").Value)
						.sListReceipt = lrecinsPreca017.Parameters("sList").Value
						.nReceiptdefault = lrecinsPreca017.Parameters("nreceipt_default").Value
						.nCertif = nCertif
					End With
				End If
			End If
		End With
		
InsPreCA017_Err: 
		If Err.Number Then
			InsPreCA017 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsPreca017 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPreca017 = Nothing
	End Function
	
	'+ insValPrevInfo: Esta función valida que la ventana anterior a esta (CA004) tenga información
	'+                 para que ésta (CA017) pueda ser desplegada
	Public Function insValPrevInfo(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer) As Boolean
		Dim lrecinsValPrevInfoDB01 As eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insValPrevInfoDB01'
		'+Información leída el 10/04/2003
		On Error GoTo lrecinsValPrevInfo_Err
		
		lrecinsValPrevInfoDB01 = New eRemoteDB.Execute
		With lrecinsValPrevInfoDB01
			.StoredProcedure = "insValPrevInfo"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTransaction", CStr(nTransaction), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ValPrevInfo", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insValPrevInfo = .Parameters("ValPrevInfo").Value = 1
			End If
		End With
		
lrecinsValPrevInfo_Err: 
		If Err.Number Then
			insValPrevInfo = CBool("insValPrevInfo: " & Err.Description)
		End If
		
		'UPGRADE_NOTE: Object lrecinsValPrevInfoDB01 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValPrevInfoDB01 = Nothing
		On Error GoTo 0
	End Function
	
	'+ insValInterComm: Esta función valida que la ventana anterior a esta (CA004) tenga información
	'+                  para que ésta (CA017) pueda ser desplegada
	Public Function insValInterComm(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nReceipt As Double) As Boolean
		Dim lrecinsValInterCommB01 As eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insValInterComm'
		'+Información leída el 16/09/2003
		On Error GoTo lrecinsValInterComm_Err
		
		lrecinsValInterCommB01 = New eRemoteDB.Execute
		With lrecinsValInterCommB01
			.StoredProcedure = "insValInterComm"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ValInterComm", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insValInterComm = .Parameters("ValInterComm").Value = 1
			End If
		End With
		
lrecinsValInterComm_Err: 
		If Err.Number Then
			insValInterComm = CBool("insValPrevInfo: " & Err.Description)
		End If
		
		'UPGRADE_NOTE: Object lrecinsValInterCommB01 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValInterCommB01 = Nothing
		On Error GoTo 0
	End Function
	
	'+ InsReaCA017: Se asigna los valores a las propiedades necesarios para mostrar los datos
	'+              del recibo
	Function InsReaCA017(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sPolitype As String, ByVal sColinvot As String, ByVal nReceipt As Double, ByVal sBrancht As String) As Boolean
		Dim lintIndex As Integer
		Dim ldblCommission As Double
		Dim ldblPremium As Double
		Dim lclsProduct_ge As eProduct.Product_ge
		
		On Error GoTo InsReaCA017_Err
		
		If sPolitype = "1" Or (nCertif <> 0 And sColinvot = "2") Or nCertif = 0 Then

            mobjPremium = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Detail_pre")
            If mobjPremium.LoadReceipts(sCertype, nReceipt, 0, 0, dEffecdate, nBranch, nProduct) Then
				
				If mobjPremium.ReceiptItem(0) Then
					InsReaCA017 = True
					mblnCharge = True
					With Me
						.dStatdate = mobjPremium.dStartDate
						.dExpirDat = mobjPremium.dExpirdate
						.nCurrency = mobjPremium.nCurrency
						ldblCommission = 0
						
						For lintIndex = 0 To mobjPremium.CountReceipts
							If mobjPremium.ReceiptItem(lintIndex) Then
								ldblCommission = ldblCommission + mobjPremium.nCommision
								ldblPremium = ldblPremium + mobjPremium.nPremium
							End If
						Next 
						.nComission = ldblCommission
					End With
				End If
			End If
		Else
			mobjPremium = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Out_moveme")
			If mobjPremium.LoadReceipts(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, 1, nReceipt) Then
				
				If mobjPremium.ReceiptItem(0) Then
					InsReaCA017 = True
					mblnCharge = True
					With Me
						.dStatdate = mobjPremium.dStartDate
						.dExpirDat = mobjPremium.dExpirDat
						.nCurrency = mobjPremium.nCurrency
						ldblCommission = 0
						
						For lintIndex = 0 To mobjPremium.CountReceipts
							If mobjPremium.ReceiptItem(lintIndex) Then
								ldblCommission = ldblCommission + mobjPremium.nCommision
								ldblPremium = ldblPremium + mobjPremium.nPremium
							End If
						Next 
						.nComission = ldblCommission
					End With
				End If
			End If
		End If
		
		If sBrancht <> CStr(eProduct.Product.pmBrancht.pmlife) And InsReaCA017 Then
			lclsProduct_ge = New eProduct.Product_ge
			If lclsProduct_ge.Find(nBranch, nProduct, dEffecdate) Then
				'+ La prima del recibo no puede ser menor a la prima mínima permitida para el producto
				If lclsProduct_ge.nPre_issue <> eRemoteDB.Constants.intNull Then
					If ldblPremium < lclsProduct_ge.nPre_issue Then
						bError = True
						nErrornum = 3936
					End If
				End If
			End If
		End If
		
InsReaCA017_Err: 
		If Err.Number Then
			InsReaCA017 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProduct_ge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct_ge = Nothing
	End Function
	
	'% InsValMaxDexpirdateReceipt: Valida que no existan recibos con fecha de expiracion mayor a
	'%                             fecha de anulación
	Public Function InsValMaxDexpirdateReceipt(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dNulldate As Date) As Boolean
		Dim lrecReceipt As eRemoteDB.Execute
		
		On Error GoTo InsValMaxDexpirdateReceipt_Err
		lrecReceipt = New eRemoteDB.Execute
		'+ Definición de store procedure reaMaxDexpirdateReceipt al 12-29-2001 16:23:42
		With lrecReceipt
			.StoredProcedure = "reaMaxDexpirdateReceipt"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNullDate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValMaxDexpirdateReceipt = .Parameters("nCount").Value > 0
			End If
		End With
		
InsValMaxDexpirdateReceipt_Err: 
		If Err.Number Then
			InsValMaxDexpirdateReceipt = False
		End If
		'UPGRADE_NOTE: Object lrecReceipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReceipt = Nothing
		On Error GoTo 0
	End Function
	
	'% FindLastPayDate: Obtiene la fecha del último recibo cobrado de la póliza
	Public Function FindFirtPendDate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecreaPremiumexpirdat_pend As eRemoteDB.Execute
		
		On Error GoTo reaPremiumexpirdat_pend_Err
		
		lrecreaPremiumexpirdat_pend = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaPremiumexpirdat_pend al 11-13-2002 16:32:55
		'+
		With lrecreaPremiumexpirdat_pend
			.StoredProcedure = "reaPremiumexpirdat_pend"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				dExpirDat = IIf(IsDbNull(.Parameters("dExpirdat").Value), eRemoteDB.Constants.dtmNull, .Parameters("dExpirdat").Value)
				FindFirtPendDate = True
			Else
				FindFirtPendDate = False
			End If
		End With
		
reaPremiumexpirdat_pend_Err: 
		If Err.Number Then
			FindFirtPendDate = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaPremiumexpirdat_pend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremiumexpirdat_pend = Nothing
	End Function
	
	'% GetLoansInterest: Obtiene el monto de los intéreses por préstamos
	Public Function GetLoansInterest(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Integer
		Dim lrecreaPremium As eRemoteDB.Execute
		
		On Error GoTo GetLoansInterest_Err
		lrecreaPremium = New eRemoteDB.Execute
		'+ Definición de store procedure reaPremiuminterestloans al 02-24-2003 10:04:56
		With lrecreaPremium
			.StoredProcedure = "ReaPremiumInterestLoans"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				GetLoansInterest = .Parameters("nAmount").Value
			End If
		End With
		
GetLoansInterest_Err: 
		If Err.Number Then
			GetLoansInterest = 0
		End If
		'UPGRADE_NOTE: Object lrecreaPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPremium = Nothing
		On Error GoTo 0
	End Function
	'% GetLoansInterest: Obtiene el monto de los intéreses por préstamos
	Public Function Count_premium_ca050(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Integer
		Dim lrecinsCount_premium_ca050 As eRemoteDB.Execute
        Dim nCount As Object = New Object
        On Error GoTo insCount_premium_ca050_Err
		
		lrecinsCount_premium_ca050 = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insCount_premium_ca050 al 03-03-2003 13:20:38
		'+
		With lrecinsCount_premium_ca050
			.StoredProcedure = "insCount_premium_ca050"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", nCount, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Count_premium_ca050 = .Parameters("nCount").Value
			Else
				Count_premium_ca050 = 0
			End If
		End With
		
insCount_premium_ca050_Err: 
		If Err.Number Then
			Count_premium_ca050 = 0
		End If
		'UPGRADE_NOTE: Object lrecinsCount_premium_ca050 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCount_premium_ca050 = Nothing
		On Error GoTo 0
		
	End Function
	
	'% Find_PremiumProp: Obtiene los datos correspondiente al recibo de la propuesta.
	Public Function Find_PremiumProp(ByVal sCertype As String, ByVal nPolicy As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreapremium_prop As eRemoteDB.Execute
		
		On Error GoTo reapremium_prop_Err
		
		If Me.sCertype <> sCertype Or Me.nPolicy <> nPolicy Or lblnFind Then
			
			lrecreapremium_prop = New eRemoteDB.Execute
			'+Definición de parámetros para stored procedure 'reapremium_prop'
			'+Información leída el 2/4/03
			With lrecreapremium_prop
				.StoredProcedure = "reapremium_prop"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find_PremiumProp = True
					Me.sCertype = sCertype
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.nPolicy = nPolicy
					Me.nCertif = .FieldToClass("nCertif")
					nReceipt = .FieldToClass("nReceipt")
					sClient = .FieldToClass("sClient")
					sDigit = .FieldToClass("sDigit")
					nPremium = .FieldToClass("nPremium")
					nBalance = .FieldToClass("nBalance")
					nCurrency = .FieldToClass("nCurrency")
					nCod_Agree = .FieldToClass("nCod_agree")
					nInsur_area = .FieldToClass("nInsur_area")
					nStatus_pre = .FieldToClass("nStatus_Pre")
					nType = .FieldToClass("nType")
					nContrat = .FieldToClass("nContrat")
                    nOrigin = .FieldToClass("nOrigin")

                    
				End If
			End With
		Else
			Find_PremiumProp = True
		End If
		
reapremium_prop_Err: 
		If Err.Number Then
			Find_PremiumProp = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreapremium_prop may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreapremium_prop = Nothing
	End Function
	
	'%valPremiumExist: Esta rutina permite validar si el recibo ingresado
	'%existe en la tabla Premium (Información general del recibo), para mandar el mensaje
	'%de error correspondiente.
	Public Function valPremiumExist(ByVal lstrCertype As String, ByVal llngBranch As Integer, ByVal llngProduct As Integer, ByVal ldblReceipt As Double, ByVal llngDigit As Integer, ByVal llngPaynumbe As Integer, ByVal lstrGeneralNumerator As TypeNumeratorPOL_REC, Optional ByRef lintTypePremium As Integer = 0, Optional ByVal lstrOrigReceipt As String = "") As Boolean
		Dim lrecvalPremiumExists As eRemoteDB.Execute
		Dim lintExists As Short
		
		On Error GoTo valPremiumExist_Err
		
		lrecvalPremiumExists = New eRemoteDB.Execute
		
		With lrecvalPremiumExists
			.StoredProcedure = "valPremiumExists"
			.Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", llngBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", llngProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", IIf(lintTypePremium = 0, ldblReceipt, 0), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", llngDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayNumbe", llngPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOrigReceipt", IIf(lintTypePremium = 0, 0, lstrOrigReceipt), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sGeneralNum", lstrGeneralNumerator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			blnError = (.Parameters.Item("nExists").Value = 1)
			valPremiumExist = blnError
		End With
		
valPremiumExist_Err: 
		If Err.Number Then
			valPremiumExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecvalPremiumExists may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalPremiumExists = Nothing
	End Function
	
	'%FindPolicyClient: Rescata datos del recibo, poliza y el cliente para ser mostrados desde la secuencia de cobranza
	'%                  para polizas de rentas vitalicias
	Public Function FindPolicyRentVital(ByVal sCertype As String, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCollecDocTyp As Integer) As Boolean
		Dim lrecPremium As eRemoteDB.Execute
		On Error GoTo FindPolicyRentVital_Err
		
		lrecPremium = New eRemoteDB.Execute
		
		With lrecPremium
			.StoredProcedure = "ReaPolicy_RentVital"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", IIf(nCertif < 0, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollecDocTyp", nCollecDocTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.sCliename = .FieldToClass("sCliename")
				Me.sClient = .FieldToClass("sClient")
				Me.sDigit = .FieldToClass("sDigit")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.sPolitype = .FieldToClass("sPolitype")
				Me.sColinvot = .FieldToClass("sColinvot")
				Me.nDocument = .FieldToClass("nDocument")
				Me.nPremium = .FieldToClass("nPremium")
				Me.dExpirdatbon = .FieldToClass("dExpirdatbon")
				Me.dIssuedatbon = .FieldToClass("dIssuedatbon")
				Me.nRate_disc = .FieldToClass("nRate_disc")
				Me.nNom_valbon = .FieldToClass("nNom_Valbon")
				FindPolicyRentVital = True
			Else
				FindPolicyRentVital = False
			End If
		End With
		
FindPolicyRentVital_Err: 
		If Err.Number Then
			FindPolicyRentVital = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPremium = Nothing
	End Function
	
	Public Function insValCo004_2(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sClientPay As String, ByVal sClientEmp As String, ByVal nWayPay As Integer, ByVal nPayfreq As Integer, ByVal nCod_Agree As Integer, ByVal nWayPayold As Integer) As String
		Dim lrecreaLifes As eRemoteDB.Execute
        Dim lstrDes As String = String.Empty
		
		On Error GoTo insValCo004_Err
		
		lrecreaLifes = New eRemoteDB.Execute
		
		With lrecreaLifes
			.StoredProcedure = "insValCo004"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientPay", sClientPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientEmp", sClientEmp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWayPay", nWayPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_Agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWayPayold", nWayPayold, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ArrayErrors", lstrDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
			insValCo004_2 = .Parameters("ArrayErrors").Value
		End With
		
insValCo004_Err: 
		If Err.Number Then
			insValCo004_2 = String.Empty
		End If
		'UPGRADE_NOTE: Object lrecreaLifes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLifes = Nothing
		On Error GoTo 0
    End Function

    '% Find: Busca los datos correspondiente a un recibo en la tabla Premium.
    Public Function Find_COC009(ByVal certype As String, ByVal Receipt As Double, ByVal branch As Integer, ByVal product As Integer, ByVal Digit As Integer, ByVal Paynumbe As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaPremium_Receipt As eRemoteDB.Execute

        lrecreaPremium_Receipt = New eRemoteDB.Execute

        On Error GoTo Find_Err

        If (certype = sCertype And Receipt = nReceipt And branch = nBranch And product = nProduct And Digit = nDigit And Paynumbe = nPaynumbe) Or lblnFind Then
            Find_COC009 = True
        Else

            '+ Definición de parámetros para stored procedure 'insudb.reaPremiumF_Receipt'
            '+ Información leída el 23/09/1999 1:02:48 PM
            With lrecreaPremium_Receipt
                .StoredProcedure = "insreaPremium_COC009pkg.insreaPremium_COC009"
                .Parameters.Add("sCertype", certype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nReceipt", Receipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", branch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", product, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nDigit", Digit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPaynumbe", Paynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    sCertype = .FieldToClass("sCertype")
                    nReceipt = .FieldToClass("nReceipt")
                    nDigit = .FieldToClass("nDigit")
                    nPaynumbe = .FieldToClass("nPaynumbe")
                    sClient = .FieldToClass("sClient")
                    sCessions = .FieldToClass("sCessions")
                    sDirdebit = .FieldToClass("sDirdebit")
                    sLeadinvo = .FieldToClass("sLeadinvo")
                    sManauti = .FieldToClass("sManauti")
                    sRenewal = .FieldToClass("sRenewal")
                    sStatusva = .FieldToClass("sStatusva")
                    sSubstiti = .FieldToClass("sSubstiti")
                    sConColl = .FieldToClass("sConColl")
                    dEffecdate = .FieldToClass("dEffecdate")
                    dExpirDat = .FieldToClass("dExpirdat")
                    dIssuedat = .FieldToClass("dIssuedat")
                    dNulldate = .FieldToClass("dNulldate")
                    dPayDate = .FieldToClass("dPaydate")
                    dStatdate = .FieldToClass("dStatdate")
                    nBalance = .FieldToClass("nBalance")
                    nComamou = .FieldToClass("nComamou")
                    nExchange = .FieldToClass("nExchange")
                    nIntammou = .FieldToClass("nIntammou")
                    nParticip = .FieldToClass("nParticip")
                    nPremium = .FieldToClass("nPremium")
                    nPremiuml = .FieldToClass("nPremiuml")
                    nPremiumn = .FieldToClass("nPremiumn")
                    nPremiums = .FieldToClass("nPremiums")
                    nRate = .FieldToClass("nRate")
                    nTaxamou = .FieldToClass("nTaxamou")
                    nCollecto = .FieldToClass("nCollecto")
                    nContrat = .FieldToClass("nContrat")
                    nInspecto = .FieldToClass("nInspecto")
                    nIntermed = .FieldToClass("nIntermed")
                    nPolicy = .FieldToClass("nPolicy")
                    nSustit = .FieldToClass("nSustit")
                    nTransactio = .FieldToClass("nTransactio")
                    nStatus_pre = .FieldToClass("nStatus_pre")
                    nNullcode = .FieldToClass("nNullcode")
                    nCurrency = .FieldToClass("nCurrency")
                    nNoteNum = .FieldToClass("nNotenum")
                    nOffice = .FieldToClass("nOffice")
                    nType = .FieldToClass("nType")
                    nBranch = .FieldToClass("nBranch")
                    nTratypei = .FieldToClass("nTratypei")
                    nProduct = .FieldToClass("nProduct")
                    nUsercode = .FieldToClass("nUsercode")
                    nPeriod = .FieldToClass("nPeriod")
                    nCompany = .FieldToClass("nCompany")
                    sOrigReceipt = .FieldToClass("sOrigReceipt")
                    sCliename = .FieldToClass("sCliename")
                    sCurrency = .FieldToClass("sDescript")
                    nCertif = .FieldToClass("nCertif")
                    nProponum = .FieldToClass("nProponum", eRemoteDB.Constants.intNull)
                    nBulletins = .FieldToClass("nBulletins", eRemoteDB.Constants.intNull)
                    dCollSus_ini = .FieldToClass("dCollsus_ini")
                    dCollSus_end = .FieldToClass("dCollsus_end")
                    nSus_reason = .FieldToClass("nSus_reason")
                    sSus_origi = .FieldToClass("sSus_origi")
                    nInsur_area = .FieldToClass("nInsur_area", eRemoteDB.Constants.intNull)
                    nCollector = .FieldToClass("nCollector", eRemoteDB.Constants.intNull)
                    sDigit = .FieldToClass("sDigit")
                    sDesBranch = .FieldToClass("sDescBranch")
                    sDescProd = .FieldToClass("sDescProduct")
                    nAgency = .FieldToClass("nAgency")
                    Find_COC009 = True
                    .RCloseRec()
                Else
                    Find_COC009 = False
                End If
            End With
            'UPGRADE_NOTE: Object lrecreaPremium_Receipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecreaPremium_Receipt = Nothing
        End If

Find_Err:
        If Err.Number Then
            Find_COC009 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaPremium_Receipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPremium_Receipt = Nothing
    End Function

	'%valPremiumExist: Esta rutina permite validar si el recibo ingresado
	'%existe en la tabla Premium (Información general del recibo), para mandar el mensaje
	'%de error correspondiente.
	Public Function valPremiumExist_COC009(ByVal lstrCertype As String, ByVal llngBranch As Integer, ByVal llngProduct As Integer, ByVal ldblReceipt As Double, ByVal llngDigit As Integer, ByVal llngPaynumbe As Integer, ByVal lstrGeneralNumerator As TypeNumeratorPOL_REC, Optional ByRef lintTypePremium As Integer = 0, Optional ByVal lstrOrigReceipt As String = "") As Boolean
		Dim lrecvalPremiumExists As eRemoteDB.Execute
		Dim lintExists As Short

		On Error GoTo valPremiumExist_Err

		lrecvalPremiumExists = New eRemoteDB.Execute

		With lrecvalPremiumExists
			.StoredProcedure = "valPremiumExists_COC009"
			.Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", llngBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", llngProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", IIf(lintTypePremium = 0, ldblReceipt, 0), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", llngDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayNumbe", llngPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOrigReceipt", IIf(lintTypePremium = 0, 0, lstrOrigReceipt), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sGeneralNum", lstrGeneralNumerator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			blnError = (.Parameters.Item("nExists").Value = 1)
			valPremiumExist_COC009 = blnError
		End With

valPremiumExist_Err:
		If Err.Number Then
			valPremiumExist_COC009 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecvalPremiumExists may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalPremiumExists = Nothing
	End Function

    '% Find2: Busca los datos correspondiente a un recibo en la tabla Premium.
    '--------------------------------------------------------------------------------
    Public Function Find2(ByVal certype As String,
                         ByVal Receipt As Double,
                         ByVal branch As Long,
                         ByVal product As Long,
                         ByVal Digit As Long,
                         ByVal Paynumbe As Long,
                         Optional ByVal lblnFind2 As Boolean = False,
                         Optional ByVal nValidateStatus As Integer = 0) As Boolean
        '--------------------------------------------------------------------------------
        Dim lrecreaPremium_Receipt As eRemoteDB.Execute

        On Error GoTo Find2_err

        lrecreaPremium_Receipt = New eRemoteDB.Execute

        If (certype = sCertype And
            Receipt = nReceipt And
            branch = nBranch And
            product = nProduct And
            Digit = nDigit And
            Paynumbe = nPaynumbe) Or
            lblnFind2 Then
            Find2 = True
        Else

            '+ Definición de parámetros para stored procedure 'insudb.reaPremiumF_Receipt'
            '+ Información leída el 23/09/1999 1:02:48 PM
            With lrecreaPremium_Receipt
                .StoredProcedure = "insreaPremium_Receiptnpkg.insreaPremium_Receipt"
                .Parameters.Add("sCertype", certype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nReceipt", Receipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", branch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", product, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nDigit", Digit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPaynumbe", Paynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nValidateStatus", nValidateStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    sCertype = .FieldToClass("sCertype")
                    nReceipt = .FieldToClass("nReceipt")
                    nDigit = .FieldToClass("nDigit")
                    nPaynumbe = .FieldToClass("nPaynumbe")
                    sClient = .FieldToClass("sClient")
                    sCessions = .FieldToClass("sCessions")
                    sDirdebit = .FieldToClass("sDirdebit")
                    sLeadinvo = .FieldToClass("sLeadinvo")
                    sManauti = .FieldToClass("sManauti")
                    sRenewal = .FieldToClass("sRenewal")
                    sStatusva = .FieldToClass("sStatusva")
                    sSubstiti = .FieldToClass("sSubstiti")
                    sConColl = .FieldToClass("sConColl")
                    dEffecdate = .FieldToClass("dEffecdate")
                    dExpirDat = .FieldToClass("dExpirdat")
                    dIssuedat = .FieldToClass("dIssuedat")
                    dNulldate = .FieldToClass("dNulldate")
                    dPayDate = .FieldToClass("dPaydate")
                    dStatdate = .FieldToClass("dStatdate")
                    nBalance = .FieldToClass("nBalance")
                    nComamou = .FieldToClass("nComamou")
                    nExchange = .FieldToClass("nExchange")
                    nIntammou = .FieldToClass("nIntammou")
                    nParticip = .FieldToClass("nParticip")
                    nPremium = .FieldToClass("nPremium")
                    nPremiuml = .FieldToClass("nPremiuml")
                    nPremiumn = .FieldToClass("nPremiumn")
                    nPremiums = .FieldToClass("nPremiums")
                    nRate = .FieldToClass("nRate")
                    nTaxamou = .FieldToClass("nTaxamou")
                    nCollecto = .FieldToClass("nCollecto")
                    nContrat = .FieldToClass("nContrat")
                    nInspecto = .FieldToClass("nInspecto")
                    nIntermed = .FieldToClass("nIntermed")
                    nPolicy = .FieldToClass("nPolicy")
                    nSustit = .FieldToClass("nSustit")
                    nTransactio = .FieldToClass("nTransactio")
                    nStatus_pre = .FieldToClass("nStatus_pre")
                    nNullcode = .FieldToClass("nNullcode")
                    nCurrency = .FieldToClass("nCurrency")
                    nNoteNum = .FieldToClass("nNotenum")
                    nOffice = .FieldToClass("nOffice")
                    nType = .FieldToClass("nType")
                    nBranch = .FieldToClass("nBranch")
                    nTratypei = .FieldToClass("nTratypei")
                    nProduct = .FieldToClass("nProduct")
                    nUsercode = .FieldToClass("nUsercode")
                    nPeriod = .FieldToClass("nPeriod")
                    nCompany = .FieldToClass("nCompany")
                    sOrigReceipt = .FieldToClass("sOrigReceipt")
                    sCliename = .FieldToClass("sCliename")
                    sCurrency = .FieldToClass("sDescript")
                    nCertif = .FieldToClass("nCertif")
                    nProponum = .FieldToClass("nProponum", eRemoteDB.Constants.intNull)
                    nBulletins = .FieldToClass("nBulletins", eRemoteDB.Constants.intNull)
                    dCollSus_ini = .FieldToClass("dCollsus_ini")
                    dCollSus_end = .FieldToClass("dCollsus_end")
                    nSus_reason = .FieldToClass("nSus_reason")
                    sSus_origi = .FieldToClass("sSus_origi")
                    nInsur_area = .FieldToClass("nInsur_area", eRemoteDB.Constants.intNull)
                    nCollector = .FieldToClass("nCollector", eRemoteDB.Constants.intNull)
                    sDesBranch = .FieldToClass("sDescBranch")
                    sDescProd = .FieldToClass("sDescProduct")
                    nRecrelatedcoll = .FieldToClass("nRecRelatedColl")

                    Find2 = True
                    .RCloseRec()
                Else
                    Find2 = False
                End If
            End With

        End If

Find2_err:
        lrecreaPremium_Receipt = Nothing
    End Function

    'ehh - Ad. vt fase II reconocimiento de ingresos
    Public Function InsPreCA017_2(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal nUsercode As Integer, ByVal sBrancht As String) As Boolean

        Dim lrecinsPreca017_2 As eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'InsPreCA017'

        On Error GoTo InsPreCA017_2_Err

        lrecinsPreca017_2 = New eRemoteDB.Execute
        With lrecinsPreca017_2
            .StoredProcedure = "PKG_VT_RECONOCIMIENTO_INGRESOS.INSPRECA017"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sList", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nreceipt_default", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColinvot", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                InsPreCA017_2 = .Parameters("nreceipt_default").Value > 0
                If InsPreCA017_2 Then
                    With Me
                        .sPolitype = Trim(lrecinsPreca017_2.Parameters("sPolitype").Value)
                        .sColinvot = Trim(lrecinsPreca017_2.Parameters("sColinvot").Value)
                        .sListReceipt = lrecinsPreca017_2.Parameters("sList").Value
                        .nReceiptdefault = lrecinsPreca017_2.Parameters("nreceipt_default").Value
                        .nCertif = nCertif
                    End With
                End If
            End If
        End With

InsPreCA017_2_Err:
        If Err.Number Then
            InsPreCA017_2 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsPreca017 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPreca017_2 = Nothing
    End Function

    Public Function ReaColinovt(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As String

        Dim lrecReaColinovt As eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'ReaColinovt'

        On Error GoTo ReaColinovt_Err

        lrecReaColinovt = New eRemoteDB.Execute
        With lrecReaColinovt
            .StoredProcedure = "PKG_VT_RECONOCIMIENTO_INGRESOS.REACOLINVOT"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColinvot", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                ReaColinovt = Trim(lrecReaColinovt.Parameters("sColinvot").Value)
            End If
        End With

ReaColinovt_Err:
        If Err.Number Then
            ReaColinovt = Nothing
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsPreca017 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaColinovt = Nothing
    End Function
End Class







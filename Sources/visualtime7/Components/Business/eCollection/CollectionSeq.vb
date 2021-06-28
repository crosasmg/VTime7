Option Strict Off
Option Explicit On
Public Class CollectionSeq
	'%-------------------------------------------------------%'
	'% $Workfile:: CollectionSeq.cls                        $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.35                               $%'
	'% $Revision:: 30                                       $%'
	'%-------------------------------------------------------%'
	
	'    Private mobjColformRef As ColformRef
	'    Public sClient As String
	'    Public nAction As TypeActionsSeqColl
	'    Public nUsercode As String
	'    Public nRelanum As Long
	'    Public nIntermCode As Long
	'    Public nBranch As Long
	'    Public nProduct As Long
	'    Public nPolicy as Double
	'    Public nCertif as Double
	'    Public dCollectdate As Date
	'    Public sReceiptOrig As String
	
	
	'-Se definen las constantes globales para el manejo del cobro/devolución
	
	Enum Type_proce
		clngPremium = 1 '- Primas
		clngClaim = 2 '- Siniestros
		clngAgent = 3 '- Agentes
		clngCoReinsuran = 4 '- Co/Reaseguro
		clngCashInCome = 5 '- Caja ingreso
		clngcashOutCome = 6 '- Caja egreso
		clngFinance = 7 '- Financiamiento
		clngTecniqueAccountRequireReinsuran = 8 '- Cuentas tecnicas de reaseguro obligatorio
		clngTecniqueAccountFacultativeReinsuran = 9 '- Cuentas tecnicas de reaseguro facultativo
		clngCurrentAccountRequireReinsuran = 10 '- Cuentas corrientes de reaseguro obligatorio
		clngCurrentAccountFacultativeReinsuran = 11 '- Cuentas corrientes de reaseguro facultativo
		clngCessionClaimReinsuranNotProportional = 12 '- Cesión de siniestros de reaseguro no proporcional
	End Enum
	
	'-Se definen las constantes globales para el manejo del tipo de facturación
	
	Enum TypeOutMoveme
		cstrByPolicy = 1 '- Por póliza
		cstrByCertif = 2 '- Por certificado
		cstrByMultiple = 3 '- Facturación combinada
		cstrByGroup = 4 '- Por grupo
	End Enum
	
	Enum TypePolicy
		cstrIndividual = 1 '- Individual
		cstrCollective = 2 '- Colectiva
		cstrMultiLocation = 3 '- Multi localidad
	End Enum
	
	Enum TypeStatus_Pol
		cstrValid = 1 '- Valido
		cstrInvalid = 2 '- Invalido
		cstrIncomplete = 3 '- En captura incompleta
		cstrPrintPendent = 4 '- Pendiente por impresión
		cstrPrinted = 5 '- Impreso
	End Enum
	
	Enum TypeStatusSeq
		cstrComplete = 1 '- Completa
		cstrNotComplete = 2 '- Incompleta
        cstrAnnul = 3 '+ Anulada
	End Enum
	
	'-Se definen las constantes globales para el manejo del origen de la relación de cobro
	
	Enum TypeOriBordereaux
        cstrDeductColl = 1 '- Descuento por planilla"
		cstrPolicyColl = 2 '- Poliza"
        cstrPayWinColl = 3 '- Pago en ventanilla"
		cstrClientColl = 4 '- Cliente"
		cstrManualColl = 5 '- Manual
	End Enum
	
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
		clngDocSaldoClient = 17 '- Saldo a favor del cliente
		clngDocAbonoAPV = 18 '- Abono APV
		clngDocTraspasoAPV = 19 '- Traspasos APV
		clngDocTransferAPV = 20 '- Transferencias APV
		clngDocAbonoPropAPV = 21 '- Abono Propuesta APV
		clngDocTraspasoPropAPV = 22 '- Traspasos Propuesta APV
		clngDocTransferPropAPV = 23 '- Transferencias Propuesta APV
	End Enum
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%insLoadTabs: Esta función es la encarga de carga la información necesaria para cada pestaña
	'%que sera mostrada en la forma.
	Public Function insLoadTabs(ByVal sAction As String, ByVal nBordereaux As Double) As String
		Dim lobjSequence As eFunctions.Sequence
		Dim lstrString As String = String.Empty
		Dim larrCodispl() As String
		Dim larrWindows() As String
		Dim lvntWindows As Object
        Dim lstrCommand As String = ""

        lobjSequence = New eFunctions.Sequence
		
		lstrString = getLoadTabsRel(sAction, nBordereaux)
		'+ Si existe información para procesar
		If lstrString <> String.Empty Then
			lstrCommand = lobjSequence.makeTable("DMECOB", "Cobranzas")
			larrWindows = Microsoft.VisualBasic.Split(Mid(lstrString, 2), "||")
			'+ Se tratan cada una de las ventanas
			For	Each lvntWindows In larrWindows
				If lvntWindows <> String.Empty Then
					lvntWindows = lvntWindows + "|"
					larrCodispl = Microsoft.VisualBasic.Split(Mid(lvntWindows, 1), "|")
					'+ Valores del arreglo: 0.- sCodisp; 1.- sCodispl; 2.- sDescript; 3.- sShort_des; 4.- nModule; 5.- nWindowTy; 6.- nImage
					lstrCommand = lstrCommand & lobjSequence.makeRow(larrCodispl(0), larrCodispl(1), CShort(sAction), larrCodispl(3), CShort(larrCodispl(6)),  ,  ,  ,  ,  ,  , larrCodispl(2), CShort(larrCodispl(4)), CShort(larrCodispl(5)))
				End If
			Next lvntWindows
		End If
		
		lstrCommand = lstrCommand & lobjSequence.closeTable
		insLoadTabs = lstrCommand
		
		'UPGRADE_NOTE: Object lobjSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjSequence = Nothing
	End Function
	
	'%getLoadTabsRel: Obtiene la información a procesar para la carga de los Tabs de la secuencia de cobranzas.
	Public Function getLoadTabsRel(ByVal sAction As String, ByVal nBordereaux As Double) As String
		Dim lrecCollectionSeq As eRemoteDB.Execute
		Dim lstrString As String = String.Empty
		
		On Error GoTo getLoadTabsRel_Err
		
		lrecCollectionSeq = New eRemoteDB.Execute
		
		With lrecCollectionSeq
			.StoredProcedure = "getLoadTabsRel"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLoadTabs", lstrString, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 350, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			getLoadTabsRel = .Parameters("sLoadTabs").Value
		End With
		
getLoadTabsRel_Err: 
		If Err.Number Then
			getLoadTabsRel = String.Empty
		End If
		'UPGRADE_NOTE: Object lrecCollectionSeq may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCollectionSeq = Nothing
		On Error GoTo 0
	End Function
End Class







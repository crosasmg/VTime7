Option Strict Off
Option Explicit On
Public Class Loans
	'%-------------------------------------------------------%'
	'% $Workfile:: Loans.cls                                $%'
	'% $Author:: Clobos                                     $%'
	'% $Date:: 17/02/06 18:12                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla insudb.loans al 08-31-2002 13:02:49
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nCode As Double ' NUMBER     22   0     5    N
	Public nAmount As Double ' NUMBER     22   0     12   S
	Public nBalance As Double ' NUMBER     22   0     12   S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nInterest As Double ' NUMBER     22   2     4    S
	Public dLoan_date As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nRequest_nu As Integer ' NUMBER     22   0     10   S
	Public nAmotax As Double ' NUMBER     22   2     10   S
	Public dNextReceipt As Date ' DATE       7    0     0    S
	Public nInterestcap As Double ' NUMBER     22   2     10   S
	Public nAgency As Integer ' NUMBER     22   0     5    N
	Public nAmountLoc As Double
	
	'+ Additional properties
	'+ Propiedades Adicionales
	Public nSumAmount As Double
	'- Orden de pago del cheque
	Public nPayOrder As Integer
	Public nCurrency As Integer
	Public sRouadvan As String
	Public nAmaxloans As Double
	
	Public sClient As String
	Public sDigit As String
	
	'- Variables usadas para carga inicial de datos de transaccion
	Private mstrClient As String
	Private mstrCliename As String
	Private mdblTaxes As Double
	Private mdblInterest As Double
	Private mdblMaxamount As Double
	Private mdblResc_val As Double
	Private mdblResc_valLocal As Double
	Private mstrPayOrder As String
	Private mlngCurrency As Integer
	Private mdblMaxamountLocal As Double
    Private mdblPervsLoans As Double
	'+ Inidica si option button de modod de eejcución está habilitado o deshablitado
	Private mblnOptExecuteEnable As Boolean
	Private mintOptExecuteValue As Integer
	
	Private mdblLoans As Double
	Private mdblAmountInterest As Double
	
	Private nRequest As Double

    '% DefaultValueVI011: Retorna valores por defecto de transaccion VI011
    '%                    cargados en insPreVI011
    Public Function DefaultValueVI011(ByVal strKey As String) As Object
        Dim caseAux As Object = New Object

        Select Case strKey
            Case "tcnClient"
                caseAux = mstrClient
            Case "tctCliename"
                caseAux = mstrCliename
            Case "tcnTaxes"
                caseAux = IIf(mdblTaxes = eRemoteDB.Constants.intNull, 0, mdblTaxes)
            Case "tcnInterest"
                caseAux = mdblInterest
            Case "tcnMaxAmount"
                caseAux = IIf(mdblMaxamount < 0, 0, mdblMaxamount)
            Case "tcnAmount"
                caseAux = nAmount
            Case "tcnResc_val"
                caseAux = IIf(mdblResc_val < 0, 0, mdblResc_val)
            Case "cbePayOrder"
                caseAux = mstrPayOrder
            Case "tcnReques_nu"
                caseAux = nRequest_nu
            Case "optExecuteEnabled"
                caseAux = mblnOptExecuteEnable
            Case "optExecutePre"
                caseAux = IIf(mintOptExecuteValue = 1, "1", "2")
            Case "optExecuteDef"
                caseAux = IIf(mintOptExecuteValue = 2, "1", "2")
            Case "tcnCurrency"
                caseAux = mlngCurrency
            Case "tcnResc_valLocal"
                caseAux = IIf(mdblResc_valLocal < 0, 0, mdblResc_valLocal)
                caseAux = System.Math.Round(caseAux, 0)
            Case "tcnMaxAmountLocal"
                caseAux = IIf(mdblMaxamountLocal < 0, 0, mdblMaxamountLocal)
                caseAux = System.Math.Round(caseAux, 0)
            Case "tcnLoansVig"
                caseAux = IIf(mdblLoans < 0, 0, mdblLoans)
            Case "tcnLoansInt"
                caseAux = IIf(mdblAmountInterest < 0, 0, mdblAmountInterest)
        End Select
        Return caseAux
    End Function

    '% insPreVI011: Carga los valores iniciales de la transaccion VI011
    Public Sub insPreVI011(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dLoan_date As Date, ByVal nLoansCode As Integer)
		On Error GoTo insPreVI011Err
		Dim ldblLoans As Double
		Dim ldblAmountInterest As Double
		Dim ldblAminloans As Double
		Dim ldblAmaxLoans As Double
		Dim ldblPervsLoans As Double
		Dim llngCurrency As Integer
		Dim llngProdclas As Integer
		
		Dim lclsProduct As eProduct.Product
		Dim lclsPolicy_his As Policy_his
		Dim lclsCertificat As Certificat
		Dim lclsRoles As Roles
		Dim lclsRequest As Request
		Dim lclsCurren_pol As Curren_pol
		Dim lclsExchange As eGeneral.Exchange
		
		lclsExchange = New eGeneral.Exchange
		
		'+ Datos del asegurado
		lclsRoles = New Roles
		With lclsRoles
			If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif, 2, "0", dLoan_date) Then
				mstrCliename = .sCliename
				mstrClient = .sClient
			End If
		End With
		
		'+ Impuestos e interes anual
		lclsProduct = New eProduct.Product
		With lclsProduct
			If .FindProduct_li(nBranch, nProduct, dLoan_date) Then
				mdblTaxes = .nTaxes
                ldblAminloans = .nAminloans
                ldblAmaxLoans = .nAmaxloans
                mdblInterest = .nInterest
                ldblPervsLoans = .nPervsloans
                mdblPervsLoans = ldblPervsLoans
                If lclsProduct.sRouadvan = "ROU_TIP" Then
                    ldblPervsLoans = mdblPervsLoans
                End If
                llngCurrency = .nCurrency
                llngProdclas = .nProdClas
            End If
		End With
		
		'+ Moneda
		lclsCurren_pol = New Curren_pol
		If lclsCurren_pol.FindOneOrLocal(sCertype, nBranch, nProduct, nPolicy, nCertif, dLoan_date) Then
			mlngCurrency = lclsCurren_pol.nCurrency
		Else
			mlngCurrency = 1 ' local
		End If
		
		With Me
			.nBranch = nBranch
			.nPolicy = nPolicy
			.nProduct = nProduct
			.nCurrency = mlngCurrency
			.nCertif = nCertif
			.dLoan_date = dLoan_date
			.nAmaxloans = lclsProduct.nAmaxloans
			.sRouadvan = lclsProduct.sRouadvan
			
			If (llngCurrency <> mlngCurrency) Then
				
				Call lclsExchange.Convert(eRemoteDB.Constants.intNull, ldblAminloans, llngCurrency, mlngCurrency, dLoan_date, ldblAminloans)
				ldblAminloans = lclsExchange.pdblResult
				
				Call lclsExchange.Convert(eRemoteDB.Constants.intNull, ldblAmaxLoans, llngCurrency, mlngCurrency, dLoan_date, ldblAmaxLoans)
				ldblAmaxLoans = lclsExchange.pdblResult
				
				llngCurrency = mlngCurrency
			End If
			
			'+ Maximo monto del prestamo en moneda de la póliza
			mdblMaxamount = .insCalcMaxLoan
			
			
			'+ Numero de solicitud de Orden de pago si ya existe
			If nLoansCode <> eRemoteDB.Constants.intNull Then
				If .Find(nBranch, nProduct, nPolicy, nCertif, nLoansCode) Then
				End If
			End If
		End With
		
		Call insreaLoans_byPolicy(sCertype, nBranch, nProduct, nPolicy, nCertif)
		
		lclsCertificat = New Certificat
		'+ Valor de rescate en moneda de la póliza
		mdblResc_val = lclsCertificat.insGetSurrenAmount(sCertype, nBranch, nProduct, nPolicy, nCertif, dLoan_date, CStr(mlngCurrency),  , llngProdclas)
		
		Call lclsExchange.Convert(eRemoteDB.Constants.intNull, mdblResc_val, 1, mlngCurrency, dLoan_date, mdblResc_val)
		mdblResc_val = lclsExchange.pdblResult
		
		'+ Al valor de rescate se resta los prestamos vigentes y los intereses pendientes de pago
		mdblResc_val = mdblResc_val - mdblLoans - mdblAmountInterest
		
		'+ Al máximo valor del préstamo se resta los prestamos vigentes y los intereses pendientes de pago
		
		If llngProdclas <> 7 And mdblMaxamount = 0 Then
			mdblMaxamount = mdblResc_val
		End If
		
		mdblMaxamount = (mdblMaxamount * ldblPervsLoans) / 100
		
		If mdblMaxamount < ldblAminloans Then
			mdblMaxamount = 0
		ElseIf mdblMaxamount > ldblAmaxLoans Then 
			mdblMaxamount = ldblAmaxLoans
		End If
		
		mdblMaxamount = mdblMaxamount - mdblLoans - mdblAmountInterest
		
		If mlngCurrency <> 1 Then
			'+ Si ya está en moneda local no se hace conversion
			If mdblLoans > 0 Then
				Call lclsExchange.Convert(eRemoteDB.Constants.intNull, mdblLoans, mlngCurrency, 1, dLoan_date, ldblLoans)
				ldblLoans = lclsExchange.pdblResult
			End If
			If mdblAmountInterest > 0 Then
				Call lclsExchange.Convert(eRemoteDB.Constants.intNull, mdblAmountInterest, mlngCurrency, 1, dLoan_date, ldblAmountInterest)
				ldblAmountInterest = lclsExchange.pdblResult
			End If
		End If
		
		If mdblResc_val > 0 Then
			Call lclsExchange.Convert(eRemoteDB.Constants.intNull, mdblResc_val, mlngCurrency, 1, dLoan_date, mdblResc_valLocal)
			mdblResc_valLocal = lclsExchange.pdblResult
		End If
		
		
		
		Call lclsExchange.Convert(eRemoteDB.Constants.intNull, ldblAminloans, llngCurrency, 1, dLoan_date, ldblAminloans)
		ldblAminloans = lclsExchange.pdblResult
		
		Call lclsExchange.Convert(eRemoteDB.Constants.intNull, ldblAmaxLoans, llngCurrency, 1, dLoan_date, ldblAmaxLoans)
		ldblAmaxLoans = lclsExchange.pdblResult
		
		'+ Maximo monto del prestamo en moneda local
		mdblMaxamountLocal = insCalMaxLoanLocal - ldblLoans - ldblAmountInterest
		
		mdblMaxamountLocal = (mdblMaxamountLocal * ldblPervsLoans) / 100
		
		If mdblMaxamountLocal < ldblAminloans Then
			mdblMaxamountLocal = 0
		ElseIf mdblMaxamountLocal > ldblAmaxLoans Then 
			mdblMaxamountLocal = ldblAmaxLoans
		End If
		
		
		'+ Tipo de Solicitud de Orden de pago
		'+ El numero de la propuesta se obtiene desde la historia de la poliza
		'+ El tipo de movimiento es propuesta de poliza(9) o propuesta de certificado(10)
		lclsPolicy_his = New Policy_his
		If lclsPolicy_his.FindLastMovementByType(sCertype, nBranch, nProduct, nPolicy, nCertif, IIf(nCertif > 0, 10, 9)) Then
			lclsRequest = New Request
			With lclsRequest
				If .Find("8", nBranch, nProduct, lclsPolicy_his.nProponum, nCertif, dLoan_date) Then
					mstrPayOrder = .sPayorder
				End If
			End With
		End If
		
insPreVI011Err: 
		If Err.Number Then
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_his = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRoles = Nothing
		'UPGRADE_NOTE: Object lclsRequest may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRequest = Nothing
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
		'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExchange = Nothing
	End Sub
	
	'% insPreVI011_K: Obtiene los valores iniciales de la página
	Public Sub insPreVI011_K(ByVal sCodispl As String, ByVal sOperat As String)
		mblnOptExecuteEnable = sCodispl <> "CA767"
		mintOptExecuteValue = 1
		
		If sCodispl = "CA767" Then
			If sOperat = "5" Then ' Actualizar
				mintOptExecuteValue = 1 ' Preliminar
			Else
				mintOptExecuteValue = 2 ' Definitiva
			End If
		End If
	End Sub
	
	'%insValVI011: Esta función se encarga de validar los datos introducidos en la forma VI011 (Folder).
	Public Function insValVI011(ByVal sCodispl As String, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sCompanyType As String = "", Optional ByVal nAmount As Double = 0, Optional ByVal nInterest As Double = 0, Optional ByVal nPayOrder As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal sTypeLoans As String = "", Optional ByVal nUsercode As Integer = 0, Optional ByVal nMaxamount As Double = 0, Optional ByVal nSurrVal As Double = 0) As String
		Dim lclsProduct As eProduct.Product
		Dim lobjErrors As eFunctions.Errors
		Dim lobjClaim As Object
		Dim lclsGeneralE As eGeneral.Exchange
		
		'+ Valor de rescate
		Dim ldblSalvage As Double
		'+ Valor maximo de prestamo/anticipo
		Dim ldblMaxamount As Double
		'+ Indica si se debe mostrar mensaje de advertencia final (3964)
		Dim lblnErrorFound As Boolean
		'+ Monto maximo y minimo para presatamo definido en el producto en la
		'+ moneda del prestamo solicitado
		Dim ldblAmaxLoans As Double
		Dim ldblAminloans As Double
		
		
		On Error GoTo insValVI011_Err
		
		lobjClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim")
		lclsProduct = New eProduct.Product
		lobjErrors = New eFunctions.Errors
		lclsGeneralE = New eGeneral.Exchange
		
		'+ Se valida el campo Importe
		With lobjErrors
			If nAmount = eRemoteDB.Constants.intNull Or nAmount = 0 Then
				Call .ErrorMessage(sCodispl, 3419)
				lblnErrorFound = True
			Else
				If lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate) Then
					With Me
						.nBranch = nBranch
						.nPolicy = nPolicy
						.nProduct = nProduct
						.nCurrency = nCurrency
						.nCertif = nCertif
						.dLoan_date = dEffecdate
						.sRouadvan = lclsProduct.sRouadvan
						.nAmaxloans = lclsProduct.nAmaxloans
					End With
					
					'+ Monto no debe ser mayor al máximo valor permitido por el producto
					If nMaxamount > 0 And nMaxamount <> eRemoteDB.Constants.intNull Then
						If nAmount > nMaxamount Then
							Call .ErrorMessage(sCodispl, 3966)
							lblnErrorFound = True
						End If
					End If
					
					'+ Se calcula Monto maximo y minimo para presatamo definido en el producto en la
					'+ moneda del prestamo solicitado
					'+ Si está en la moneda de la solicitud no se calcula
					If nCurrency <> lclsProduct.nCurrency Then
						Call lclsGeneralE.Convert(0, 0, lclsProduct.nCurrency, nCurrency, dEffecdate, 0)
						ldblAmaxLoans = lclsProduct.nAmaxloans * lclsGeneralE.pdblExchange
						ldblAminloans = lclsProduct.nAminloans * lclsGeneralE.pdblExchange
					Else
						ldblAmaxLoans = lclsProduct.nAmaxloans
						ldblAminloans = lclsProduct.nAminloans
					End If
					
					'+ Monto debe estar en rango definido para producto
					If lclsProduct.nAmaxloans <> eRemoteDB.Constants.intNull And lclsProduct.nAminloans <> eRemoteDB.Constants.intNull Then
						If (nAmount > ldblAmaxLoans And ldblAmaxLoans <> 0) Or nAmount < ldblAminloans Then
							Call .ErrorMessage(sCodispl, 60298)
							lblnErrorFound = True
						End If
					End If
					
					'+ Monto no debe superar porcentaje de valor de rescate fijado para producto
					If nAmount > System.Math.Round(nSurrVal * (lclsProduct.nPervsloans / 100), 6) Then
						Call .ErrorMessage(sCodispl, 60299)
						lblnErrorFound = True
					End If
				End If
			End If
			
			'+ El campo Interés debe estar lleno
			If nInterest = eRemoteDB.Constants.intNull Or nInterest = 0 Then
				Call .ErrorMessage(sCodispl, 3420)
				lblnErrorFound = True
			End If
			
			'+ Se valida que el campo Orden de pago tenga información
			If nPayOrder = eRemoteDB.Constants.intNull Or nPayOrder = 0 Then
				Call .ErrorMessage(sCodispl, 9104)
				lblnErrorFound = True
			End If
			
			'+ Si el tipo de ejecución es "Definitiva", se envía advertencia al usuario para
			'+ que determine si desea proseguir el proceso
			If sTypeLoans <> "1" Then
				If Not lblnErrorFound Then
					Call .ErrorMessage(sCodispl, 3964)
				End If
			End If
			
			'+ Se valida si poliza/certificado posee una declaración de siniestro, si ésta es de vida
			If lobjClaim.reacountclaim(sCertype, nBranch, nProduct, nPolicy, nCertif) <> 0 Then
				Call .ErrorMessage(sCodispl, 55778)
			End If
			
			insValVI011 = .Confirm
		End With
		
insValVI011_Err: 
		If Err.Number Then
			insValVI011 = "insValVI011: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lobjClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjClaim = Nothing
	End Function
	
	'%insPostVI011: Se realiza la actualización de los datos en la ventana VI011
	Public Function insPostVI011(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dLoan_date As Date, ByVal nUsercode As Integer, ByVal sExeType As String, Optional ByVal nAmount As Double = 0, Optional ByVal nInterest As Double = 0, Optional ByVal nOperat As Integer = 0, Optional ByVal sClient As String = "", Optional ByVal sPayorder As String = "", Optional ByVal nAmotax As Double = 0, Optional ByVal nNotenum As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal sCompanyType As String = "", Optional ByVal nAgency As Integer = 0, Optional ByVal sRequest As String = "", Optional ByVal nRequest_nu As Integer = 0, Optional ByVal nSessionId As String = "", Optional ByVal nProponum As Double = 0, Optional ByVal nSurrVal As Double = 0, Optional ByVal nMaxamount As Double = 0, Optional ByVal nLoans As Double = 0) As Boolean
		Dim lclsRequest As Request
		Dim lclsGeneral As eGeneral.GeneralFunction
		Dim lclsCertificat As Certificat
		Dim lclsPolicy_his As Policy_his
		Dim lclsNotes As eGeneralForm.Notes
		Dim lclsValPolicyTra As ValPolicyTra
		Dim lclsGeneralE As eGeneral.Exchange
		Dim lclsCurren_pol As Curren_pol
		Dim lclsProduct As eProduct.Product
		Dim llngCurrency As Integer
		Dim ldblTaxes As Integer
		
		Dim llngProposalNum As Double
		Dim ldblAmountLoc As Double
		On Error GoTo insPostVI011Err
		insPostVI011 = False
		lclsGeneral = New eGeneral.GeneralFunction
		lclsRequest = New Request
		lclsNotes = New eGeneralForm.Notes
		lclsCertificat = New Certificat
		lclsPolicy_his = New Policy_his
		lclsValPolicyTra = New ValPolicyTra
		lclsGeneralE = New eGeneral.Exchange
		
		'+ Se buscan los datos del certificado
		Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
		
		'+ Se busca la moneda de la póliza
		lclsCurren_pol = New Curren_pol
		If lclsCurren_pol.FindOneOrLocal(sCertype, nBranch, nProduct, nPolicy, nCertif, dLoan_date) Then
			llngCurrency = lclsCurren_pol.nCurrency
		Else
			llngCurrency = 1 ' local
		End If
		
		'+ Se calcula el monto en moneda local
		'+ Si ya está en moneda local no se hace conversion
		If llngCurrency <> 1 And llngCurrency <> eRemoteDB.Constants.intNull Then
            Call lclsGeneralE.Convert(0, 0, llngCurrency, 1, Today, 0)
			ldblAmountLoc = nAmount * lclsGeneralE.pdblExchange
			ldblAmountLoc = System.Math.Round(ldblAmountLoc, 0)
		Else
			ldblAmountLoc = System.Math.Round(nAmount, 0)
		End If
		
		'+ Ejecución preliminar
		If sExeType = "1" Then
			If sCodispl = "VI011" Then
				If nAction = eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
					'+ Si se desea generar la solicitud de anticipo
					If sRequest = "1" Then
						If lclsValPolicyTra.AddProposal(sCertype, nBranch, nProduct, nPolicy, nCertif, llngProposalNum, dLoan_date, nUsercode, nAgency) Then
                            Me.nCode = lclsValPolicyTra.nProposal
							'+ Se crea solicitud de anticipo
							With lclsRequest
								.sCertype = "8"
								.nBranch = nBranch
								.nProduct = nProduct
                                .nPolicy = lclsValPolicyTra.nProposal
								.nCertif = nCertif
								'+ Prestamo
								.nOrigin = 9
								.dEffecdate = dLoan_date
								.sPayorder = sPayorder
								.nAmount = nAmount
								.sDescript = sDescript
								.nNotenum = nNotenum
								.nUsercode = nUsercode
								.nInterestrate = nInterest
								'+ El tipo de pago asociado al rescate es "Orden de pago" (Table5527)
								.nTypepay = 1
								.nNullcode = eRemoteDB.Constants.intNull
								.nAgency = nAgency
								insPostVI011 = .Add
                                nRequest = lclsValPolicyTra.nProposal
							End With
						End If
					Else
						insPostVI011 = True
					End If
				End If
			ElseIf sCodispl = "CA767" Then 
				'+ Se obtiene numero de solicitud creada
				If lclsPolicy_his.FindLastMovementByType(sCertype, nBranch, nProduct, nPolicy, nCertif, IIf(nCertif > 0, 10, 9)) Then
					If nAction = eFunctions.Menues.TypeActions.clngAcceptdataCancel Then
						'+ Se elimina nota si cancela la operación
						Call lclsNotes.DeleteNote(nNotenum)
						With lclsRequest
							If .Find("8", nBranch, nProduct, lclsPolicy_his.nProponum, nCertif, dLoan_date) Then
								
							End If
							.nNotenum = 0
							.sDescript = String.Empty
							insPostVI011 = .Update
						End With
					ElseIf nOperat = 5 Then  ' Actualizar
						'+ Se actualiza la solicitud
						With lclsRequest
							If .Find("8", nBranch, nProduct, lclsPolicy_his.nProponum, nCertif, dLoan_date) Then
								.nAmount = nAmount
								.sPayorder = sPayorder
								.nNotenum = nNotenum
								.sDescript = sDescript
								.nInterestrate = nInterest
								
								insPostVI011 = .Update
							End If
						End With
					End If ' Tipo de Accion
				End If ' Find policy_his
			End If ' sCodispl
			'+ Ejecucion definitiva
		Else
			'+ Se crea el anticipo
			With Me
				.nBranch = nBranch
				.nProduct = nProduct
				.nPolicy = nPolicy
				.nCertif = nCertif
				.nAmount = nAmount
				.nInterest = nInterest
				.dLoan_date = dLoan_date
				.nUsercode = nUsercode
				.nAgency = nAgency
				.nRequest_nu = nRequest_nu
				.nAmotax = nAmotax 'ldblAmountLoc * ldblTaxes / 100
				.dNextReceipt = lclsGeneral.GetLastDay(dLoan_date)
				.nCode = eRemoteDB.Constants.intNull
				.nAmountLoc = ldblAmountLoc
				insPostVI011 = .insCreLoans()
				nRequest = .nCode
			End With
			
			'+ Se crea registro en historia de poliza
			If insPostVI011 Then
				With lclsPolicy_his
					.sCertype = sCertype
					.nBranch = nBranch
					.nProduct = nProduct
					.nPolicy = nPolicy
					.nCertif = nCertif
					.dEffecdate = dLoan_date
					.dLedgerDat = dLoan_date
					'+ Anticipo sobre poliza
					.nType = 50
					.nUsercode = nUsercode
					.nAgency = nAgency
					.nMovement = 0
					.sNull_move = "2"
					.nProponum = nProponum
					insPostVI011 = .insCrePolicy_his
				End With
			End If
			
			If sCodispl = "CA767" Then
				If nAction = eFunctions.Menues.TypeActions.clngAcceptdataCancel Then
					'+ Se elimina nota si cancela la operación
					lclsNotes = New eGeneralForm.Notes
					Call lclsNotes.DeleteNote(nNotenum)
					With lclsRequest
						If .Find("8", nBranch, nProduct, nProponum, nCertif, dLoan_date) Then
							.nNotenum = 0
							.sDescript = String.Empty
							insPostVI011 = .Update
						End If
					End With
					'+ Operacion Aprobar
				ElseIf nOperat = 2 Then 
					'+ Se actualiza la solicitud
					With lclsRequest
						If .Find("8", nBranch, nProduct, nProponum, nCertif, dLoan_date) Then
							.nInterestrate = nInterest
							.nAmount = nAmount
							.sPayorder = sPayorder
							.nNotenum = nNotenum
							.sDescript = sDescript
							insPostVI011 = .Update
						End If
					End With
				End If
				If insPostVI011 Then
					'+ Se aprueba propuesta
					With lclsCertificat
						If .Find("8", nBranch, nProduct, nProponum, nCertif) Then
							.nStatquota = Certificat.Stat_quot.esqApprove '
							.dChangdat = dLoan_date
							insPostVI011 = .Update
						End If
					End With
				End If
			End If
		End If
		
		If insPostVI011 Then
			insPostVI011 = UpdateTMP_VIL011(nSessionId, nUsercode, nBranch, nProduct, nPolicy, nCertif, dLoan_date, sExeType, nRequest, nRequest_nu, sPayorder, llngCurrency, nAmount, nInterest, nAmotax, ldblAmountLoc, nSurrVal, nMaxamount, nLoans)
		End If
		
insPostVI011Err: 
		If Err.Number Then
			insPostVI011 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
		'UPGRADE_NOTE: Object lclsRequest may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRequest = Nothing
		'UPGRADE_NOTE: Object lclsNotes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsNotes = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_his = Nothing
		'UPGRADE_NOTE: Object lclsValPolicyTra may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValPolicyTra = Nothing
		'UPGRADE_NOTE: Object lclsGeneralE may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneralE = Nothing
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
	End Function
	
	'% insValVI011_K: Esta función se encarga de validar los datos introducidos en la forma VI011_k (Header)
	Public Function insValVI011_K(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sCompanyType As String = "", Optional ByVal nLoansCode As Integer = 0, Optional ByVal nOffice As Integer = 0, Optional ByVal nOfficeAgen As Integer = 0, Optional ByVal nAgency As Integer = 0, Optional ByVal nProponum As Double = 0, Optional ByVal nUsercode As Integer = 0) As String
		Dim lclsProduct As eProduct.Product
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsCertificat As ePolicy.Certificat
		Dim lobjErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		Dim ldtmStartdate As Date
		'- Variable para indicar si encontró póliza o certificado válido
		Dim lblnFindDoc As Boolean
		Dim lstrQuotProp As String
		
		On Error GoTo insValVI011_k_Err
		
		lobjErrors = New eFunctions.Errors
		lobjValues = New eFunctions.Values
		lclsProduct = New eProduct.Product
		lclsPolicy = New Policy
		lclsCertificat = New Certificat
		
		lblnFindDoc = True
		
		'+Se valida el campo Ramo
		If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 1022)
		End If
		
		'+ Se valida el campo Producto
		If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 1014)
		Else
			If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
				'+ Se valida que el producto corresponda a vida o combinado (1:Vida, 5:Combinado)
				With lclsProduct
					Call .insValProdMaster(nBranch, nProduct)
					If .blnError Then
						If CStr(.sBrancht) <> "1" And CStr(.sBrancht) <> "5" Then
							Call lobjErrors.ErrorMessage(sCodispl, 3987)
						Else
							If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
								If (Trim(.sRouadvan)) = String.Empty Then
									Call lobjErrors.ErrorMessage(sCodispl, 3406)
								End If
							Else
								Call lobjErrors.ErrorMessage(sCodispl, 3406)
							End If
						End If
					Else
						Call lobjErrors.ErrorMessage(sCodispl, 9066)
					End If
				End With
			End If
		End If
		
		'+Se valida el campo póliza
		If nPolicy = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 3003)
			lblnFindDoc = False
		Else
			'+ Se valida que sea una póliza válida
			With lclsPolicy
				If Not .FindPolicyOfficeName(sCertype, nBranch, nProduct, nPolicy, sCompanyType) Then
					Call lobjErrors.ErrorMessage(sCodispl, 3001)
					lblnFindDoc = False
				Else
					If .sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrIncomplete) Or .sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrInvalid) Then
						Call lobjErrors.ErrorMessage(sCodispl, 3720)
						lblnFindDoc = False
					End If
					If .dNulldate <> eRemoteDB.Constants.dtmNull Then
						'+ Si tiene fecha de anulación, se muestra el estado en que se encuentra la póliza/certificado
						Call lobjErrors.ErrorMessage(sCodispl, 3098,  , eFunctions.Errors.TextAlign.RigthAling, "(" & lobjValues.getMessage(.nNullcode, "Table13") & ")")
						lblnFindDoc = False
					End If
				End If
			End With
		End If
		
		'+Se valida el campo certificado
		If nCertif = 0 Or nCertif = eRemoteDB.Constants.intNull Then
			If lblnFindDoc Then
				If lclsPolicy.sPolitype <> "1" Then
					Call lobjErrors.ErrorMessage(sCodispl, 3006)
					lblnFindDoc = False
				End If
			End If
		Else
			With lclsCertificat
				If Not .Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
					Call lobjErrors.ErrorMessage(sCodispl, 3010)
					lblnFindDoc = False
				Else
					'+ El certificado debe estar válido (Statusva -> 2:Inválido, 3:En captura incompleta)
					If .sStatusva = "2" Or .sStatusva = "3" Then
						Call lobjErrors.ErrorMessage(sCodispl, 750044)
						lblnFindDoc = False
					End If
					If .dNulldate <> eRemoteDB.Constants.dtmNull Then
						'+ Si tiene fecha de anulación, se muestra el estado en que se encuentra la póliza/certificado
						Call lobjErrors.ErrorMessage(sCodispl, 3099,  , eFunctions.Errors.TextAlign.RigthAling, "(" & lobjValues.getMessage(.nNullcode, "Table13") & ")")
						lblnFindDoc = False
					End If
				End If
			End With
		End If
		
		'+ Se valida que la póliza/certificado tenga valor de rescate
		If lblnFindDoc And nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			If lclsCertificat.insGetSurrenAmount(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sCodispl,  , lclsProduct.nProdClas, lclsPolicy.dStartdate, lclsProduct.sRousurre) <= 0 Then
				Call lobjErrors.ErrorMessage(sCodispl, 3408)
			End If
		End If
		
		'+Se valida el campo Anticipo
		'+Debe ser ingresado cuando es consulta
		If nLoansCode = eRemoteDB.Constants.intNull And nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			Call lobjErrors.ErrorMessage(sCodispl, 3963)
		End If
		
		'+ Validación de la Fecha de efecto del anticipo.
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 3404)
		Else
			If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
				'+ Fecha no debe ser mayor a la del día
				If dEffecdate > Today Then
					Call lobjErrors.ErrorMessage(sCodispl, 1002)
				End If
				'+ Las siguientes validacioneds solo tiene sentido si hay una poliza o certificado
				If lblnFindDoc Then
					'+ Fecha de rescate debe ser posterior a la fecha de efecto de la Póliza/Certificado
					If nCertif = eRemoteDB.Constants.intNull Or nCertif = 0 Then
						ldtmStartdate = lclsPolicy.dStartdate
					Else
						ldtmStartdate = lclsCertificat.dStartdate
					End If
					If dEffecdate <= ldtmStartdate Then
						Call lobjErrors.ErrorMessage(sCodispl, 3405)
					End If
					'+ Fecha debe ser igual o posterior a ultimo anticipo
					Me.dLoan_date = dEffecdate
					If Not insValLastLoan(nBranch, nProduct, nPolicy, nCertif) Then
						Call lobjErrors.ErrorMessage(sCodispl, 3414)
					End If
					'+ Meses de vigencia de poliza certificado debe ser mayor a la indicada para producto
					If lclsProduct.nQmeploans <> eRemoteDB.Constants.intNull Then
						If DateDiff(Microsoft.VisualBasic.DateInterval.Month, ldtmStartdate, dEffecdate) < lclsProduct.nQmeploans Then
							Call lobjErrors.ErrorMessage(sCodispl, 60295)
						End If
					End If
					'+ Cantidad de prestamos del mes
					If lclsProduct.nQmmloans <> eRemoteDB.Constants.intNull Then
						If insCalQMonth(nBranch, nProduct, nPolicy, nCertif, dEffecdate) >= lclsProduct.nQmmloans Then
							Call lobjErrors.ErrorMessage(sCodispl, 60296)
						End If
					End If
					'+ Cantidad de prestamos en año
					If lclsProduct.nQmyloans <> eRemoteDB.Constants.intNull Then
						If insCalQYear(nBranch, nProduct, nPolicy, nCertif, dEffecdate) >= lclsProduct.nQmyloans Then
							Call lobjErrors.ErrorMessage(sCodispl, 60297)
						End If
					End If
				End If
			End If
		End If
		
		If nOffice = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 9120)
		End If
		If nOfficeAgen = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 55519)
		End If
		If nAgency = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 1080)
		End If
		
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			If nProponum = eRemoteDB.Constants.intNull Then
				'+ Se valida que la poliza/certificado no tenga propuestas especiales/de endoso pendientes
				lstrQuotProp = lclsCertificat.Proposal_val(nBranch, nProduct, nPolicy, nCertif, 1, eRemoteDB.Constants.intNull)
				If lstrQuotProp <> "" Then
					Call lobjErrors.ErrorMessage(sCodispl, 55649,  , eFunctions.Errors.TextAlign.RigthAling, "(" & lstrQuotProp & ")")
				End If
			End If
		End If
		
		insValVI011_K = lobjErrors.Confirm
		
insValVI011_k_Err: 
		If Err.Number Then
			insValVI011_K = insValVI011_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
	End Function
	
	'% calQMonth: Obtiene la cantidad de anticipos por mes
	Public Function insCalQMonth(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Integer
		Dim lreccalQmonth_loans As eRemoteDB.Execute
		
		On Error GoTo calQmonth_loans_Err
		
		insCalQMonth = 0
		
		lreccalQmonth_loans = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure calQmonth_loans al 12-05-2001 18:19:44
		'+
		With lreccalQmonth_loans
			.StoredProcedure = "calQloans"
			With .Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("sType", "M", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nQloans", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			If .Run(False) Then
				insCalQMonth = .Parameters("nQloans").Value
			End If
		End With
		
calQmonth_loans_Err: 
		If Err.Number Then
			insCalQMonth = 0
		End If
		'UPGRADE_NOTE: Object lreccalQmonth_loans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccalQmonth_loans = Nothing
		On Error GoTo 0
	End Function
	
	'% calQYear: Obtiene la cantidad de anticipos por año
	Public Function insCalQYear(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Integer
		Dim lreccalQyear_loans As eRemoteDB.Execute
		
		On Error GoTo calQyear_loans_Err
		
		insCalQYear = 0
		
		lreccalQyear_loans = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure calQyear_loans al 12-05-2001 18:21:20
		'+
		With lreccalQyear_loans
			.StoredProcedure = "calQloans"
			With .Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("sType", "Y", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nQloans", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			If .Run(False) Then
				insCalQYear = .Parameters("nQloans").Value
			End If
		End With
		
calQyear_loans_Err: 
		If Err.Number Then
			insCalQYear = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccalQyear_loans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccalQyear_loans = Nothing
	End Function
	
	'% Find: This routine verifies if the code of the amount in advance is registered in the system
	'% Find: Esta rutina permite verificar si el código del anticipo existe
    Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCode As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecLoans As eRemoteDB.Execute

        lrecLoans = New eRemoteDB.Execute

        On Error GoTo Find_Err

        Find = True

        If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.nCode <> nCode Or lblnFind Then

            With lrecLoans
                .StoredProcedure = "reaLoans_o"
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Find = .Run(True)
                If Find Then
                    Me.nAmount = .FieldToClass("nAmount")
                    Me.nBalance = .FieldToClass("nBalance")
                    Me.nInterest = .FieldToClass("nInterest")
                    Me.dLoan_date = .FieldToClass("dLoan_date")
                    Me.nRequest_nu = .FieldToClass("nRequest_nu")
                    Me.nCurrency = .FieldToClass("nCurrency")
                    Me.nAmountLoc = .FieldToClass("nAmountLoc")
                    Me.nAmotax = .FieldToClass("nAmoTax")
                    Me.nAgency = .FieldToClass("nAgency")
                End If
            End With
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecLoans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecLoans = Nothing
    End Function
	
	'% Find_loansA: Esta rutina permite verificar la informacion del anticipo pendiente mas antiguo
    Public Function Find_loansA(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecLoans As eRemoteDB.Execute

        lrecLoans = New eRemoteDB.Execute

        On Error GoTo Find_loansA_Err

        Find_loansA = True

        If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or lblnFind Then

            With lrecLoans
                .StoredProcedure = "reaLoans_Antique"
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Find_loansA = .Run(False)
                If Find_loansA Then
                    Me.nCode = .Parameters("nCode").Value
                    Me.nBalance = .Parameters("nBalance").Value
                End If
            End With
        End If

Find_loansA_Err:
        If Err.Number Then
            Find_loansA = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecLoans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecLoans = Nothing
    End Function
	
	'% Find_Rel: This routine verifies if the code of the amount in advance is registered in the system
	'% Find_Rel: Esta rutina permite verificar si el código del anticipo existe
    Public Function Find_Rel(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCode As Double, Optional ByVal lblnFind As Boolean = True) As Boolean
        Dim lrecLoans As eRemoteDB.Execute

        On Error GoTo Find_Rel_Err

        Find_Rel = True

        If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.nCode <> nCode Or lblnFind Then

            lrecLoans = New eRemoteDB.Execute

            With lrecLoans
                .StoredProcedure = "reaLoans_rel"
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Find_Rel = .Run(True)
                If Find_Rel Then
                    Me.sClient = .FieldToClass("sClient")
                    Me.sDigit = .FieldToClass("sDigit")
                    Me.nBalance = .FieldToClass("nBalance")
                    Me.nCurrency = .FieldToClass("nCurrency")
                End If
            End With
        End If

Find_Rel_Err:
        If Err.Number Then
            Find_Rel = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecLoans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecLoans = Nothing
    End Function
	
	'% insCalMaxLoanLocal: Calcula el monto maximo del anticipo en la moneda local
	'                      Como usa funcion insCalcMaxLoan, se deben asignar
	'                      previamente los valores de las propiedades a esta clase
	Public Function insCalMaxLoanLocal() As Double
		Dim lclsGeneral As eGeneral.Exchange
		Dim ldblMaxLoan As Double
        Dim lvarExchange As Object = New Object

        On Error GoTo insCalMaxLoanLocalErr
		
		ldblMaxLoan = insCalcMaxLoan
		
		'+ Si ya está en moneda local no se hace conversion
		If nCurrency <> 1 And nCurrency <> eRemoteDB.Constants.intNull Then
			lclsGeneral = New eGeneral.Exchange
			Call lclsGeneral.Convert(lvarExchange, ldblMaxLoan, nCurrency, 1, dLoan_date, 0)
			ldblMaxLoan = ldblMaxLoan * lclsGeneral.pdblExchange
		End If
		
		insCalMaxLoanLocal = ldblMaxLoan
		
insCalMaxLoanLocalErr: 
		If Err.Number Then
			insCalMaxLoanLocal = 0
		End If
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
		On Error GoTo 0
	End Function
	
	'% insValLoans: This routine verifies if the code of the amount in advance is registered in the system
	'% insValLoans: Esta rutina permite verificar si el código del anticipo existe
	Public Function insValLoans(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCode As Double) As Boolean
		Dim lrecreaLoans_1 As eRemoteDB.Execute
		
		lrecreaLoans_1 = New eRemoteDB.Execute
		
		On Error GoTo insValLoans_Err
		
		'+ Stored procedure parameters definition 'insudb.reaLoans_1'
		'+ Data of 12/01/1999 03:31:54 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaLoans_1'
		'+ Información leída el 01/12/1999 03:31:54 p.m.
		
		With lrecreaLoans_1
			.StoredProcedure = "reaLoans_1"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insValLoans = .Run(True)
			If insValLoans Then
				dLoan_date = .FieldToClass("dLoan_date")
				nInterest = .FieldToClass("nInterest")
				nAmount = .FieldToClass("nAmount")
				nPayOrder = .FieldToClass("sRequest_ty")
				nRequest_nu = .FieldToClass("nRequest_nu")
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaLoans_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLoans_1 = Nothing
		
insValLoans_Err: 
		If Err.Number Then
			insValLoans = False
		End If
		On Error GoTo 0
	End Function
	
	'% insValLastLoan: This routine verifies if the date of the last advance is greater than the effective date of the window
	'% insValLastLoan: Esta rutina permite verificar si la fecha del último anticipo es mayor a la fecha de efecto de la ventana.
	Public Function insValLastLoan(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecreaLoans As eRemoteDB.Execute
		
		lrecreaLoans = New eRemoteDB.Execute
		
		On Error GoTo insValLastLoan_Err
		
		'+ Stored procedure parameters definition 'insudb.reaLoans'
		'+ Data of 12/01/1999 03:34:51 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaLoans'
		'+ Información leída el 01/12/1999 03:34:51 p.m.
		
		With lrecreaLoans
			.StoredProcedure = "reaLoans"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insValLastLoan = .Run(True)
			If insValLastLoan Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If Not IsDbNull(.FieldToClass("dLoan_date")) Then
					insValLastLoan = (.FieldToClass("dLoan_date") <= dLoan_date)
				Else
					insValLastLoan = True
				End If
			End If
		End With
		
insValLastLoan_Err: 
		If Err.Number Then
			insValLastLoan = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaLoans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLoans = Nothing
	End Function
	
	'% insCalcMaxLoan:This routine calculates the maximum value of an advance
	'% insCalcMaxLoan: Esta calcula el valor máximo del anticipo
	Public Function insCalcMaxLoan() As Double
		Dim lrecinsCUS_routine_Loans As eRemoteDB.Execute
		
		lrecinsCUS_routine_Loans = New eRemoteDB.Execute
		
		On Error GoTo insCalcMaxLoan_Err
		
		'+ Stored procedure parameters definition 'insudb.insCUS_routine_Loans'
		'+ Data of 12/01/1999 03:48:12 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.insCUS_routine_Loans'
		'+ Información leída el 01/12/1999 03:48:12 p.m.
		
		With lrecinsCUS_routine_Loans
			.StoredProcedure = "insCUS_routine_Loans"
			.Parameters.Add("sRoutine", sRouadvan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dLoan_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoans", nAmaxloans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndicator", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If Not IsDbNull(.FieldToClass("nLoans")) And .FieldToClass("nLoans") <> eRemoteDB.Constants.intNull Then
					insCalcMaxLoan = .FieldToClass("nLoans")
				Else
					insCalcMaxLoan = 0
				End If
				.RCloseRec()
			Else
				insCalcMaxLoan = 0
			End If
		End With
insCalcMaxLoan_Err: 
		If Err.Number Then
			insCalcMaxLoan = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsCUS_routine_Loans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCUS_routine_Loans = Nothing
	End Function
	'% insValAmountLoans: This routine returns the sum of the advances of the policy
	'% insValAmountLoans: Esta rutina lee la suma de todos los anticipos de la póliza.
	Public Function insValAmountLoans() As Double
		Dim lrecinsCalLoans As eRemoteDB.Execute
		
		lrecinsCalLoans = New eRemoteDB.Execute
		
		On Error GoTo 0
		
		'+ Stored procedure parameters definition 'insudb.insCalLoans'
		'+ Data of 12/01/1999 03:43:57 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.insCalLoans'
		'+ Información leída el 01/12/1999 03:43:57 p.m.
		
		With lrecinsCalLoans
			.StoredProcedure = "insCalLoans"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAdv_paymen", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndicator", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If Not IsDbNull(.FieldToClass("nAdv_paymen")) Then
					insValAmountLoans = .FieldToClass("nAdv_paymen")
				Else
					insValAmountLoans = 0
				End If
				.RCloseRec()
			Else
				insValAmountLoans = 0
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsCalLoans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCalLoans = Nothing
	End Function
	'% insCreLoans: This routine adds a record in the table "Loans"
	'% insCreLoans: Esta rutina crea un registro en la tabla Loans
	Public Function insCreLoans() As Boolean
		Dim lreccreLoans As eRemoteDB.Execute
		
		'+ Stored procedure parameters definition 'insudb.creLoans'
		'+ Data of 12/01/1999 04:25:36 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.creLoans'
		'+ Información leída el 01/12/1999 04:25:36 p.m.
		On Error GoTo insCreLoans_Err
		lreccreLoans = New eRemoteDB.Execute
		With lreccreLoans
			.StoredProcedure = "creLoans"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLoan_date", dLoan_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmotax", nAmotax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNextreceipt", dNextReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterestcap", nInterestcap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountLoc", nAmountLoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insCreLoans = .Run(False)
			Me.nCode = .Parameters("nCode").Value
		End With
		
insCreLoans_Err: 
		If Err.Number Then
			insCreLoans = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreLoans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreLoans = Nothing
	End Function
	
	'%InsUpdLoans: Actualiza la información de la tabla de préstamos
	Public Function InsUpdLoans(ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nProduct As Integer, ByVal nCertif As Double, ByVal nCode As Double, ByVal nAmount As Double, ByVal nBalance As Double, ByVal nInterest As Double, ByVal dLoan_date As Date, ByVal nUsercode As Integer, ByVal nRequest_nu As Integer, ByVal nAmotax As Double, ByVal dNextReceipt As Date, ByVal nInterestcap As Double) As Boolean
		Dim lreccreLoans As eRemoteDB.Execute
		
		On Error GoTo InsUpdLoans_Err
		'+ Definición de parámetros para stored procedure 'InsUpdLoans'
		lreccreLoans = New eRemoteDB.Execute
		With lreccreLoans
			.StoredProcedure = "InsUpdLoans"
			.Parameters.Add("nAction", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLoan_date", dLoan_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmotax", nAmotax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNextreceipt", dNextReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterestcap", nInterestcap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdLoans = .Run(False)
		End With
		
InsUpdLoans_Err: 
		If Err.Number Then
			InsUpdLoans = False
		End If
		'UPGRADE_NOTE: Object lreccreLoans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreLoans = Nothing
		On Error GoTo 0
	End Function
	
	'% insupdTMP_VIL011: Se actualizan los datos en la tabla temporal necesarios para la
	'%                   ejecución del reporte
	Private Function insupdTMP_VIL011(ByVal sKey As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dLoan_date As Date, ByVal sExecute As String, ByVal nRequest As Double, ByVal nRequest_nu As Integer, ByVal sPayorder As String, ByVal nCurrency As Integer, ByVal nResc_val As Double, ByVal nMax_loans As Double, ByVal nAmount As Double, ByVal nInterest As Double, ByVal nAmotax As Double, ByVal nAmountLoc As Double, ByVal nLoans As Double) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		lclsRemote = New eRemoteDB.Execute
		
		On Error GoTo insupdTMP_VIL011_Err
		
		With lclsRemote
			.StoredProcedure = "insUpdTMP_VIL011"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExecute", sExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest", nRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLoan_date", dLoan_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPayorder", sPayorder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nResc_val", nResc_val, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_loans", nMax_loans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmotax", nAmotax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountLoc", nAmountLoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoans", nLoans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insupdTMP_VIL011 = .Run(False)
		End With
		
insupdTMP_VIL011_Err: 
		If Err.Number Then
			insupdTMP_VIL011 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% UpdateTMP_VIL011: Se actualizan los datos en la tabla temporal necesarios para la
	'%                   ejecución del reporte
	Public Function UpdateTMP_VIL011(ByVal nSessionId As String, ByVal nUsercode As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dLoan_date As Date, ByVal sExecute As String, ByVal nRequest As Double, ByVal nRequest_nu As Integer, ByVal sPayorder As String, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal nInterest As Double, ByVal nAmotax As Double, ByVal nAmountLoc As Double, ByVal nSurrVal As Double, ByVal nMaxamount As Double, ByVal nLoans As Double) As Boolean
		Dim lstrKey As String
		Dim lclsProduct As eProduct.Product
		
		On Error GoTo UpdateTMP_VIL011_err
		
		lstrKey = "TMP" & nSessionId & nUsercode
		
		lclsProduct = New eProduct.Product
		
		If lclsProduct.FindProduct_li(nBranch, nProduct, dLoan_date) Then
			With Me
				.nBranch = nBranch
				.nPolicy = nPolicy
				.nProduct = nProduct
				.nCurrency = nCurrency
				.nCertif = nCertif
				.dLoan_date = dLoan_date
				.sRouadvan = lclsProduct.sRouadvan
			End With
		End If
		
		UpdateTMP_VIL011 = insupdTMP_VIL011(lstrKey, nBranch, nProduct, nPolicy, nCertif, dLoan_date, sExecute, nRequest, nRequest_nu, sPayorder, nCurrency, nSurrVal, nMaxamount, nAmount, nInterest, nAmotax, nAmountLoc, nLoans)
		
UpdateTMP_VIL011_err: 
		If Err.Number Then
			UpdateTMP_VIL011 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
	End Function
	
	'% insPreVI011: Carga los valores iniciales de la transaccion VI011
	Public Sub insreaLoans_byPolicy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double)
		Dim lclsRemote As eRemoteDB.Execute
		
		lclsRemote = New eRemoteDB.Execute
		
		On Error GoTo insreaLoans_byPolicy_Err
		
		With lclsRemote
			.StoredProcedure = "reaLoansPolicy"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoans", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				mdblLoans = .Parameters("nLoans").Value
				mdblAmountInterest = .Parameters("nAmount").Value
			End If
		End With
		
insreaLoans_byPolicy_Err: 
		If Err.Number Then
			mdblLoans = 0
			mdblAmountInterest = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Sub

    Public Function Find_interest(ByVal sCertype As String,ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecFind_interest As eRemoteDB.Execute

        On Error GoTo Find_interest_Err
        lrecFind_interest = New eRemoteDB.Execute

        '+ Definición de store procedure insCreProposal al 12-05-2001 20:45:19
        With lrecFind_interest
            .StoredProcedure = "insFind_interest"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPervsloans", mdblPervsLoans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_interest = .Run(False)
            If Find_interest Then
                mdblInterest = .Parameters("nInterest").Value
                mdblPervsLoans = .Parameters("nPervsloans").Value
            End If

        End With

Find_interest_Err:
        If Err.Number Then
            Find_interest = False
        End If
        'UPGRADE_NOTE: Object lrecinsCreProposal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecFind_interest = Nothing
        On Error GoTo 0
    End Function
End Class
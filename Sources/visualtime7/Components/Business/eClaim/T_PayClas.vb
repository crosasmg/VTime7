Option Strict Off
Option Explicit On
Public Class T_PayClas
	Implements System.Collections.IEnumerable
	'**-Local variable for the collection handle
	'- Variable local para el manejo de la coleccion
	Private mCol As Collection
	
	'**-Define the variable mdblPayAmount to contein the total acumulated payment in the collection
	'- Se define la variable mdblPayAmount para contener el total del pago acumulado en la colección
	
	Private mdblPayAmount As Double
	Public mintCountCover As Integer
	
	'**% Add: is in charge to used the collection in the SI008
	'% Add: se carga la colección a utilizar en la SI008
    Public Function Add(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCover_curr As Integer, ByVal nCover As Integer, ByVal nPay_concep As Double, ByVal nPay_amount As Double, ByVal nCov_exchange As Double, ByVal nTax As Double, ByVal nTot_amount As Double, ByVal nGroup_insu As Integer, ByVal sIndAuto As String, Optional ByVal sBenef As String = "", Optional ByVal dNext_Pay As Date = #12:00:00 AM#, Optional ByVal nId As Integer = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nOutReserv As Double = 0, Optional ByVal nFra_amount As Double = 0, Optional ByVal sClient_Rep As String = "", Optional ByVal nOffice_pay As Integer = 0, Optional ByVal nAgency_pay As Integer = 0, Optional ByVal nOfficeAgen_pay As Integer = 0, Optional ByVal nCurrency_pay As Integer = 0, Optional ByVal nPaycov_amount As Double = 0, Optional ByVal nTotcov_amount As Double = 0, Optional ByVal nParticip As Double = 0, Optional ByVal nRasa As Double = 0, Optional ByVal nRasaAnnual As Double = 0, Optional ByVal nDepreciateamount As Double = 0, Optional ByVal nDepreciatebase As Double = 0, Optional ByVal nDepreciaterate As Double = 0, Optional ByVal sRasa_routine As String = "", Optional ByVal nId_Settle As Integer = 0) As T_PayCla
        Dim objNewMember As T_PayCla
        objNewMember = New T_PayCla

        With objNewMember
            .nClaim = nClaim
            .nCase_num = nCase_num
            .nDeman_type = nDeman_type
            .nCover_curr = nCover_curr
            .nCover = nCover
            .nPay_concep = nPay_concep
            .nPay_amount = nPay_amount
            .nCov_exchange = nCov_exchange
            .nTax = nTax
            .nTot_amount = nTot_amount
            .nGroup_insu = nGroup_insu
            .sIndAuto = sIndAuto
            .sBenef = sBenef
            .dNext_Pay = dNext_Pay
            .nId = nId
            .nModulec = nModulec
            .nOutReserv = nOutReserv
            .nFra_amount = nFra_amount
            .sClient_Rep = sClient_Rep
            .nOffice_pay = nOffice_pay
            .nAgency_pay = nAgency_pay
            .nOfficeAgen_pay = nOfficeAgen_pay
            .nCurrency_pay = nCurrency_pay
            .nPaycov_amount = nPaycov_amount
            .nTotcov_amount = nTotcov_amount
            .nParticip = nParticip
            .sRasa_routine = sRasa_routine
            .nRasa = nRasa
            .nRasaAnnual = nRasaAnnual
            .nFra_amount = nFra_amount
            .nDepreciaterate = nDepreciaterate
            .nDepreciatebase = nDepreciatebase
            .nDepreciateamount = nDepreciateamount
            .nId_Settle = nId_Settle

        End With

        mCol.Add(objNewMember)

        Add = objNewMember
        objNewMember = Nothing
    End Function
	
	'**% FindSI008: find the necesary data to charge the repetitive part of the page
	'**%            (claim payment)
	'% FindSI008: se buscan los datos necesarios para cargar la parte repetitiva de la
	'%            página SI008 (Pago de siniestros)
    Public Function FindSI008(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nPayType As Integer, ByVal nExchange As Double, ByVal dEffecdate As Date, Optional ByVal nCurrency As Integer = 0, Optional ByVal sbrancht As String = "", Optional ByVal nRole As Short = 0, Optional ByVal sClient As String = "") As Boolean
        Dim mclsCl_cover As Object
        Dim lclsClaimbenef As ClaimBenef
        Dim ldblFra_Amount As Object = New Object
        Dim lclsClaim As Claim
        Dim lclsOpt_sinies As Opt_sinies
        Dim lclsCl_cover As Cl_Cover
        Dim lclsCl_cov_bil As Cl_cov_bil
        Dim lclsGeneral As eGeneral.Exchange
        Dim lclsT_PayCla As T_PayCla
        Dim ldblParticip As Double
        Dim lclsGen_cover As eProduct.Gen_cover
        lclsGen_cover = New eProduct.Gen_cover
        lclsClaim = New Claim
        lclsOpt_sinies = New Opt_sinies
        lclsCl_cover = New Cl_Cover
        lclsCl_cov_bil = New Cl_cov_bil
        lclsGeneral = New eGeneral.Exchange
        lclsT_PayCla = New T_PayCla
        lclsClaimbenef = New ClaimBenef

        '**- Variable to calculate the amount without tax
        '- Variable para calcular el importe sin impuesto
        Dim ldblAmountWOTax As Object
        Dim ldblPayPay As Object
        Dim ldbltotPay As Object
        Dim ldblPaycov As Object
        Dim ldblTotcov As Object
        Dim ldblReserv As Object

        '**- Variable to calculate the amount of the payment with the coverage currency
        '- Variable para calcular el importe del pago en la moneda de la cobertura
        Dim ldblAmountCover As Object = New Object

        '**- Variable to calculate the Exchange factor
        '- Variable para calcular el factor de cambio
        Dim ldblExchange As Object = New Object

        Dim lintCurrencyOpt As Short
        Dim ldblAmountCurrCover As Double
        Dim ldblAmountCoverTotal As Double
        Dim ldblnFra_amount_aux As Double

        FindSI008 = False

        If nPayType = 0 Or nPayType = eRemoteDB.Constants.intNull Then
            Exit Function
        End If
        mintCountCover = 1
        If Not Find(nClaim, nCase_num, nDeman_type, sClient, sbrancht, nRole, dEffecdate, nPayType, nCurrency) Then
            lclsCl_cover = New Cl_Cover
            With lclsCl_cover
                If .Find_SI008(nClaim, nCase_num, nDeman_type) Then
                    FindSI008 = True

                    lclsGen_cover.Find(lclsClaim.nBranch, lclsClaim.nProduct, .nModulec, .nCover, lclsClaim.dOccurdat)

                    If lclsClaim.Find(nClaim) Then
                        If Not lclsCl_cov_bil.Find_Pay_concep(.nModulec, .nCover, lclsClaim.nBranch, lclsClaim.nProduct, lclsClaim.dOccurdat) Then
                            lclsCl_cov_bil.nPay_concep = 0
                            lclsCl_cov_bil.sDesPay_concep = String.Empty
                        End If
                    End If

                    If nPayType = 7 And sbrancht = "3" Then
                        ldblFra_Amount = insDevDed(nClaim, nCase_num, nDeman_type, .nModulec, .nCover, .nCurrency, sClient)
                    ElseIf nPayType = 7 And sbrancht = "1" Then
                        ldblFra_Amount = .insGetFra_Amount(nClaim, nCase_num, nDeman_type, .nModulec, .nCover, .nCurrency, sClient)
                    End If

                    '**+ nPayType = 2: Total Pay
                    '+ nPayType = 2: Pago total

                    '                ldblAmountWOTax = IIf(nPayType = 2, .nReserve - .nPay_amount, 0)
                    '                ldblAmountWOTax = IIf(nPayType = 2, .nReserve, 0)
                    ldblAmountWOTax = .nReserve
                    ldblPayPay = .nReserve
                    ldbltotPay = .nReserve
                    ldblPaycov = .nReserve
                    ldblTotcov = .nReserve
                    If nExchange = 0 Or nExchange = eRemoteDB.Constants.intNull Then
                        If lclsGeneral.Find(.nCurrency, dEffecdate) Then
                            ldblExchange = lclsGeneral.nExchange
                        End If
                    Else
                        ldblExchange = nExchange
                    End If

                    '+ Si se refiere a un pago total es necesario realizar la conversion a la moneda del pago, solo si la misma ya fue indicada.

                    If nPayType = 2 And nCurrency > 0 Then
                        nExchange = 0
                        Call lclsGeneral.Convert(nExchange, ldblAmountWOTax, .nCurrency, nCurrency, dEffecdate, ldblAmountCover)
                        ldblAmountCover = lclsGeneral.pdblResult
                        ldblAmountWOTax = ldblAmountCover
                        nExchange = lclsGeneral.pdblExchange
                    Else
                        If nCurrency > 0 Then
                            If .nCurrency <> nCurrency Then
                                nExchange = 0
                                Call lclsGeneral.Convert(nExchange, ldblAmountWOTax, .nCurrency, nCurrency, dEffecdate, ldblAmountCover)
                                ldblAmountCover = lclsGeneral.pdblResult
                                '                       ldblAmountCover = ldblAmountWOTax
                                Call lclsGeneral.Convert(nExchange, .nReserve, .nCurrency, nCurrency, dEffecdate, ldblAmountCover)
                                ldblPayPay = lclsGeneral.pdblResult
                                ldbltotPay = lclsGeneral.pdblResult
                            Else
                                ldblAmountCover = ldblAmountWOTax
                            End If
                        Else
                            ldblAmountCover = ldblAmountWOTax
                        End If
                    End If

                    '+ Si el ramo corresponde a "Vida" y el Rol es "Beneficiario", se obtiene la sumatoria de los movimientos en Claim_his
                    '+ Se obtiene el porcentaje de participación por cada cobertura de cada beneficiario.

                    If sbrancht = "1" And nRole = 16 Then

                        ldblParticip = lclsClaimbenef.insCalBenefPercent(nClaim, nCase_num, nDeman_type, .nCover, sClient)
                        '+ Se obtiene el cálculo de la provisión pendiente para el beneficiario.
                        If nPayType = 2 Then
                            ldblAmountCoverTotal = Me.insCalnLoc_Amount(nClaim, nCase_num, nDeman_type, .nCover, sClient, .nLoc_Reserv, 2)
                        Else
                            ldblAmountCover = Me.insCalnLoc_Amount(nClaim, nCase_num, nDeman_type, .nCover, sClient, .nLoc_Reserv, 2)
                        End If

                        '+ Se obtiene la moneda local especificada en las opciones de instalacion.
                        If lclsOpt_sinies.Find() Then
                            lintCurrencyOpt = lclsOpt_sinies.nCurrency
                        Else
                            lintCurrencyOpt = 1
                        End If

                        '+ Se convierte el monto pendiente de la moneda local a la moneda de pago.
                        '                    If nCurrency > 0 Then
                        '                        If lintCurrencyOpt <> nCurrency Then
                        '                            Call lclsGeneral.Convert(nExchange, ldblAmountCover, lintCurrencyOpt, nCurrency, dEffecdate, ldblAmountCover)
                        '                            ldblAmountCover = lclsGeneral.pdblResult
                        '                        End If
                        '                    End If
                        '+ Si las monedas tanto del pago como de la cobertura son distintas, se obtiene el monto pendiente en moneda de la cobertura.
                        If nPayType <> 2 Then
                            If nCurrency > 0 Then
                                ldblAmountCurrCover = ldblAmountCover
                                If .nCurrency <> nCurrency Then
                                    nExchange = 0
                                    Call lclsGeneral.Convert(nExchange, ldblAmountCurrCover, .nCurrency, nCurrency, dEffecdate, ldblAmountCurrCover)
                                    ldblAmountCurrCover = lclsGeneral.pdblResult
                                End If
                            Else
                                If lintCurrencyOpt <> .nCurrency Then
                                    Call lclsGeneral.Convert(nExchange, ldblAmountCover, lintCurrencyOpt, .nCurrency, dEffecdate, ldblAmountCurrCover)
                                    ldblAmountCurrCover = lclsGeneral.pdblResult
                                    ldblAmountCurrCover = ldblAmountCover
                                Else
                                    ldblAmountCurrCover = ldblAmountCover
                                End If
                            End If
                        Else
                            If nCurrency > 0 Then
                                nExchange = 0
                                Call lclsGeneral.Convert(nExchange, ldblAmountCoverTotal, .nCurrency, nCurrency, dEffecdate, ldblAmountCoverTotal)
                                'ldblAmountCoverTotal = lclsGeneral.pdblResult
                                'ldblAmountCurrCover = ldblAmountCover
                                ldblAmountCurrCover = lclsGeneral.pdblResult
                            Else
                                ldblAmountCurrCover = ldblAmountCover
                            End If
                        End If

                        If nPayType <> 2 Then
                            ldblReserv = ldblAmountCover
                        Else
                            ldblReserv = ldblAmountCoverTotal
                        End If

                        If nPayType = 2 And nCurrency > 0 Then
                            ldblPayPay = ldblAmountCurrCover
                            ldbltotPay = ldblAmountCurrCover
                            'ldblPaycov = ldblAmountCover
                            'ldblTotcov = ldblAmountCover
                            ldblPaycov = 0
                            ldblTotcov = 0
                        Else
                            ldblPayPay = 0
                            ldbltotPay = 0
                            ldblPaycov = 0
                            ldblTotcov = 0
                        End If
                        '+ Ahora
                        '                    ldblPayPay = ldblAmountCurrCover
                        '                    ldbltotPay = ldblAmountCurrCover
                        '                    ldblPaycov = ldblAmountCover
                        '                    ldblTotcov = ldblAmountCover
                        Call Add(nClaim, nCase_num, nDeman_type, .nCurrency, .nCover, lclsCl_cov_bil.nPay_concep, ldblPayPay, ldblExchange, 0, ldbltotPay, .nGroup_insu, "2", "", eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, .nModulec, ldblReserv, 0, "", 0, 0, 0, 0, ldblPaycov, ldblPaycov, ldblParticip)
                    ElseIf nPayType <> 7 Then
                        ldblReserv = ldblPaycov
                        If nPayType = 2 And nCurrency > 0 Then
                            ldblPayPay = IIf(ldblAmountWOTax <> 0, ldblAmountCover, .nReserve)
                            ldbltotPay = IIf(ldblAmountWOTax <> 0, ldblAmountCover, .nReserve)
                        Else
                            ldblPayPay = 0
                            ldbltotPay = 0
                            ldblPaycov = 0
                            ldblTotcov = 0
                        End If
                        '                    ldblPaycov = .nReserve
                        '                    ldblTotcov = .nReserve
                    ElseIf nPayType = 7 Then
                        ldblnFra_amount_aux = ldblFra_Amount
                        If nCurrency > 0 Then
                            Call lclsGeneral.Convert(eRemoteDB.Constants.intNull, ldblFra_Amount, .nCurrency, nCurrency, dEffecdate, ldblFra_Amount)
                            ldblFra_Amount = lclsGeneral.pdblResult
                        End If

                        ldblPayPay = ldblFra_Amount
                        ldbltotPay = ldblFra_Amount
                        ldblPaycov = .nReserve
                        ldblTotcov = .nReserve

                        Call Add(nClaim, nCase_num, nDeman_type, .nCurrency, .nCover, lclsCl_cov_bil.nPay_concep, ldblPayPay, ldblExchange, 0, ldbltotPay, .nGroup_insu, "2", "", eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, .nModulec, .nReserve, ldblnFra_amount_aux, "", 0, 0, 0, 0, ldblPaycov, ldblTotcov, ldblParticip)
                    End If

                    '                If nPayType <> 2 Then
                    '                    ldblPayPay = 0
                    '                    ldbltotPay = 0
                    '                End If
                    '                    ldblTotcov
                    '                If sBrancht <> "1" Or _
                    ''                    nRole <> 16 Then

                    lclsT_PayCla.nClaim = nClaim
                    lclsT_PayCla.nCase_num = nCase_num
                    lclsT_PayCla.nDeman_type = nDeman_type
                    lclsT_PayCla.nCover_curr = .nCurrency
                    lclsT_PayCla.nModulec = .nModulec
                    lclsT_PayCla.nCover = .nCover
                    lclsT_PayCla.nPay_concep = lclsCl_cov_bil.nPay_concep
                    lclsT_PayCla.nPay_amount = ldblPayPay
                    lclsT_PayCla.nCov_exchange = ldblExchange
                    lclsT_PayCla.nTax = 0
                    lclsT_PayCla.nTot_amount = ldbltotPay
                    lclsT_PayCla.nUsercode = .nUserCode
                    lclsT_PayCla.nGroup_insu = .nGroup_insu
                    lclsT_PayCla.sIndAuto = "2"
                    lclsT_PayCla.nId = eRemoteDB.Constants.intNull
                    lclsT_PayCla.nCurrency_pay = nCurrency
                    lclsT_PayCla.nPaycov_amount = ldblPaycov
                    lclsT_PayCla.nTotcov_amount = ldblTotcov
                    lclsT_PayCla.nParticip = ldblParticip
                    lclsT_PayCla.nRasa = lclsCl_cover.nRasa
                    lclsT_PayCla.nRasaAnnual = lclsCl_cover.nRasaAnnual
                    lclsT_PayCla.nDepreciateamount = lclsCl_cover.nDepreciateamount
                    lclsT_PayCla.nDepreciatebase = lclsCl_cover.nDepreciatebase
                    lclsT_PayCla.nDepreciaterate = lclsCl_cover.nDepreciaterate
                    lclsT_PayCla.nFra_amount = lclsCl_cover.nFra_amount
                    lclsT_PayCla.sRasa_routine = lclsGen_cover.sRASA_routine
                    lclsT_PayCla.Add()

                    '                End If
                    '                End If
                    If nPayType = 2 Then
                        mdblPayAmount = ldblAmountCover
                    Else
                        mdblPayAmount = 0
                    End If
                End If
                mintCountCover = .mintCountCover
            End With
            mclsCl_cover = Nothing
        Else
            FindSI008 = True
        End If

        lclsClaim = Nothing
        lclsGeneral = Nothing
        lclsCl_cover = Nothing
        lclsCl_cov_bil = Nothing
    End Function

    '%insCalnLoc_Amount: Función que retorna el valor calculado de la Provisión pendiente
    Public Function insCalnLoc_Amount(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCover As Integer, ByVal sClient As String, ByVal nLoc_Reserv As Double, ByVal nCurrency_Typ As Short, Optional ByVal nBene_type As Integer = eRemoteDB.Constants.intNull) As Double
        Dim lclsClaim_his As Claim_his
        Dim lclsClaimbenef As ClaimBenef
        Dim lclsCl_cover As Cl_Cover

        '+ Variables que contienen el porcentaje de participación del beneficiario y el total acumulado de pagos realizados.
        Dim ldblParticip As Double
        Dim ldblTot_Amountaux As Double
        Dim ldblLoc_reserv As Double
        Dim ldblLoc_reservBenef As Double
        Dim ldblInitialreservBenef As Double
        Dim lstrClient As String
        Dim ldblTot_Amountpaycov As Double

        lclsClaim_his = New Claim_his
        lclsClaimbenef = New ClaimBenef
        lclsCl_cover = New Cl_Cover

        ldblParticip = lclsClaimbenef.insCalBenefPercent(nClaim, nCase_num, nDeman_type, nCover, sClient)

        Call lclsClaimbenef.Find_client(nClaim, sClient, nCase_num, nDeman_type, True, nBene_type)

        lstrClient = sClient

        '+ Lo que ya tiene pagado un beneficiario
        Call lclsClaim_his.Claim_hisnLoc_Amount(nClaim, nCase_num, nDeman_type, lstrClient, nCover)
        ldblTot_Amountaux = lclsClaim_his.nAmountPay * -1
        ldblTot_Amountpaycov = lclsClaim_his.nAmountCov * -1

        '+ La reserva que esta quedando
        ldblLoc_reserv = IIf(nLoc_Reserv = eRemoteDB.Constants.intNull, 0, nLoc_Reserv)

        '+ Si existe porcentaje de participación buscar monto original de reserva y
        '+    se restan los pagos realizados por el beneficiario
        Call lclsCl_cover.Find_SI008(nClaim, nCase_num, nDeman_type, nCover)
        ldblLoc_reservBenef = lclsCl_cover.nReserve - lclsCl_cover.nFra_amount
        ldblInitialreservBenef = lclsCl_cover.nInitialReserve - lclsCl_cover.nFra_amount
        If ldblParticip <> eRemoteDB.Constants.intNull And ldblParticip > 0 Then
            '+ Se rescata reserva en moneda de la cobertura (original)
            ldblTot_Amountaux = ldblTot_Amountpaycov
            insCalnLoc_Amount = (ldblLoc_reservBenef * ldblParticip / 100) '- ldblTot_Amountaux se comenta el descontar lo pagado ya que se basa en la reserva
        Else
            insCalnLoc_Amount = ldblLoc_reservBenef  '-  ldblTot_Amountpaycov se comenta ya que no debe desontar lo pagado y basarse en la reserva
        End If

        lclsClaim_his = Nothing
        lclsClaimbenef = Nothing
        lclsCl_cover = Nothing
    End Function

    '**% Finf: find the paids that are made for a claim/case/claimant
    '% Find: busca los pagos realizados para un siniestro/caso/demandante
    Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, Optional ByVal sClient As String = "", Optional ByVal sbrancht As String = "", Optional ByVal nRole As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nPayType As Integer = 0, Optional ByRef nCurrency As Integer = 0) As Boolean
        Dim lclsGeneral As Object = New Object
        Dim mdblAmountCover As Object
		Dim lrecreaT_payclaAll As eRemoteDB.Execute
		Dim lclsCl_cover As eClaim.Cl_Cover
		Dim ldblFra_Amount As Double
		Dim ldblFra_AmountPayCurr As Double
		Dim a As Double
		
		lrecreaT_payclaAll = New eRemoteDB.Execute
		lclsCl_cover = New eClaim.Cl_Cover
		
		On Error GoTo Find_Err
		
		mdblPayAmount = 0
		mdblAmountCover = 0
		
		'**+Parameters definition for the stored procedure 'insudb.reaT_payclaAll'
		'**Data read on 02/20/2001 04:38:22 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaT_payclaAll'
		'+ Información leída el 20/02/2001 04:38:22 p.m.
		
		With lrecreaT_payclaAll
			.StoredProcedure = "reaT_payclaAll"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					If sbrancht = "1" And nRole = 16 Then
						mdblAmountCover = Me.insCalnLoc_Amount(nClaim, nCase_num, nDeman_type, .FieldToClass("nCover"), sClient, .FieldToClass("nOutreserv"), 1)
						
                        Call Add(nClaim, nCase_num, nDeman_type, .FieldToClass("nCover_curr"), .FieldToClass("nCover"), .FieldToClass("nPay_concep"), .FieldToClass("nPay_amount"), .FieldToClass("nCov_exchange"), .FieldToClass("nTax"), .FieldToClass("nTot_amount"), .FieldToClass("nGroup_insu"), .FieldToClass("sIndAuto"), "", eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, .FieldToClass("nModulec"), mdblAmountCover, ldblFra_Amount, "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nCurrency_pay"), .FieldToClass("nPaycov_amount"), .FieldToClass("nTotcov_amount"), .FieldToClass("nParticip"), .FieldToClass("nRasa"), .FieldToClass("nRasaAnnual"), .FieldToClass("nDepreciateamount"), .FieldToClass("nDepreciatebase"), .FieldToClass("nDepreciaterate"), .FieldToClass("sRasa_routine"))
						
						If .FieldToClass("nPay_amount") <> eRemoteDB.Constants.intNull Then
							mdblPayAmount = mdblPayAmount + .FieldToClass("nPay_amount")
						End If
					Else
						'+ Tipo de pago: Devolución de deducible
						If nPayType = 7 Then
							If sbrancht = "1" Then
								ldblFra_Amount = lclsCl_cover.insGetFra_Amount(nClaim, nCase_num, nDeman_type, .FieldToClass("nModulec"), .FieldToClass("nCover"), .FieldToClass("nCover_curr"), sClient)
								If nCurrency > 0 Then
									Call lclsGeneral.Convert(.FieldToClass("nCov_exchange"), ldblFra_Amount, .FieldToClass("nCover_curr"), 1, dEffecdate, ldblFra_AmountPayCurr)
									ldblFra_AmountPayCurr = lclsGeneral.pdblResult
								Else
									ldblFra_AmountPayCurr = ldblFra_Amount
								End If
							End If
							
                            Call Add(nClaim, nCase_num, nDeman_type, .FieldToClass("nCover_curr"), .FieldToClass("nCover"), .FieldToClass("nPay_concep"), ldblFra_AmountPayCurr, .FieldToClass("nCov_exchange"), .FieldToClass("nTax"), ldblFra_AmountPayCurr, .FieldToClass("nGroup_insu"), .FieldToClass("sIndAuto"), "", eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, .FieldToClass("nModulec"), .FieldToClass("nOutreserv", 0), ldblFra_Amount, "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nCurrency_pay"), .FieldToClass("nPaycov_amount"), .FieldToClass("nTotcov_amount"), .FieldToClass("nParticip"), .FieldToClass("nRasa"), .FieldToClass("nRasaAnnual"), .FieldToClass("nDepreciateamount"), .FieldToClass("nDepreciatebase"), .FieldToClass("nDepreciaterate"), .FieldToClass("sRasa_routine"))
						Else
                            Call Add(nClaim, nCase_num, nDeman_type, .FieldToClass("nCover_curr"), .FieldToClass("nCover"), .FieldToClass("nPay_concep"), .FieldToClass("nPay_amount"), .FieldToClass("nCov_exchange"), .FieldToClass("nTax"), .FieldToClass("nTot_amount"), .FieldToClass("nGroup_insu"), .FieldToClass("sIndAuto"), "", eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, .FieldToClass("nModulec"), .FieldToClass("nOutreserv", 0), .FieldToClass("nFra_amount", ldblFra_Amount), "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nCurrency_pay"), .FieldToClass("nPaycov_amount"), .FieldToClass("nTotcov_amount"), .FieldToClass("nParticip"), .FieldToClass("nRasa"), .FieldToClass("nRasaAnnual"), .FieldToClass("nDepreciateamount"), .FieldToClass("nDepreciatebase"), .FieldToClass("nDepreciaterate"), .FieldToClass("sRasa_routine"), .FieldToClass("nId_Settle"))
							If .FieldToClass("nPay_amount") <> eRemoteDB.Constants.intNull Then
								mdblPayAmount = mdblPayAmount + .FieldToClass("nPay_amount")
							End If
						End If
					End If
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
                Find = True
			End If
		End With
		lrecreaT_payclaAll = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	'% FindSI773: busca los siniestros de rentas para la transacción de pago de rentas
	Public Function FindSI773(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nClaim As Double, ByVal dStartdate As Date, ByVal dEndDate As Date) As Boolean
		Dim lrecReaClaim_SI773 As eRemoteDB.Execute
		
		lrecReaClaim_SI773 = New eRemoteDB.Execute
		
		On Error GoTo FindSI773_Err
		
		mdblPayAmount = 0
		
		With lrecReaClaim_SI773
			.StoredProcedure = "ReaClaim_SI773"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartDate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("nClaim"), .FieldToClass("nCase_num"), .FieldToClass("nDeman_type"), .FieldToClass("nCurrency"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nAmount"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.strNull), .FieldToClass("sClient"), .FieldToClass("dNext_Pay"), .FieldToClass("nId"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("sClient_Rep"), .FieldToClass("nOffice_Pay"), .FieldToClass("nAgency_Pay"), .FieldToClass("nOfficeAgen_Pay"))
					.RNext()
				Loop 
				.RCloseRec()
				FindSI773 = True
			Else
				FindSI773 = False
			End If
		End With
		lrecReaClaim_SI773 = Nothing
		
FindSI773_Err: 
		If Err.Number Then
			FindSI773 = False
		End If
		On Error GoTo 0
	End Function
	'%insDevDed: Esta funcion se encarga de calcular el importe correspondiente por devolucion de
	'% deducible.
	Public Function insDevDed(ByRef nClaim As Object, ByRef nCase_num As Object, ByRef nDeman_type As Object, ByRef nModulec As Object, ByRef nCover As Object, ByRef nCurrency As Object, ByRef sClient As Object) As Double
		Dim lrecReaClaim_SI773 As eRemoteDB.Execute
		
		lrecReaClaim_SI773 = New eRemoteDB.Execute
		
		insDevDed = 0
		
		With lrecReaClaim_SI773
			.StoredProcedure = "InsDevDed"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_Indemnity", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insDevDed = .Parameters.Item("nAmount_Indemnity").Value
			End If
		End With
		lrecReaClaim_SI773 = Nothing
		
	End Function
	Public ReadOnly Property nTotPayAmo() As Double
		Get
			nTotPayAmo = mdblPayAmount
		End Get
	End Property
	
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As T_PayCla
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'**% Class_Initialize: This method makes the inicialized of the variables and the objects
	'**% for the moment when instance the class.
	'%Class_Initialize. Este método realiza la inicialización de las variables y los objetos al
	'%momento de instanciar la clase.
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
		mdblPayAmount = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class







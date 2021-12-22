<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eClaim" %>
<%@ Import Namespace="eGeneral" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.42
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de	las	funciones generales	de carga de	valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As eFunctions.Grid
    Dim lclsClaim As eClaim.Claim
    Dim lclsExchange As eGeneral.Exchange


    '% insDefineHeader:	se definen los campos del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------

        Dim lblnDisabled As Boolean
        Dim lclsCover As eClaim.Cl_Cover
        Dim lobjColumn As eFunctions.Column

        lclsClaim = New eClaim.Claim
        mobjGrid = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility

        mobjGrid.sCodisplPage = "si008"
        Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
        lclsCover = New eClaim.Cl_Cover
        Session("nServ_order") = Request.QueryString("nServ_order")
        Call lclsClaim.Find(CDbl(Session("nClaim")))

        lblnDisabled = False

        '+ Se definen las columnas del grid
        With mobjGrid.Columns

            '+ Si el Rol corresponde a "Beneficiario", se lee el "TabTable" que obtiene las coberturas por beneficiario.        
            If (CStr(Session("SI008_sBrancht")) = "1" Or CStr(Session("SI008_sBrancht")) = "6") And CDbl(Session("SI008_cbeRole")) = 16 Then
                Call .AddPossiblesColumn(40265, "Cobertura", "valCover", "tabBenefcover", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "ChangeValues(this);", CBool(lblnDisabled), 4, "Cobertura que queda afectada en el pago")
            Else
                Call .AddPossiblesColumn(40265, "Cobertura", "valCover", "tabCl_cover", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "ChangeValues(this);", CBool(lblnDisabled), 4, "Cobertura que queda afectada en el pago")
            End If

            lblnDisabled = False
            Call .AddPossiblesColumn(40266, "Concepto de pago", "tcnConcept", "tabCl_cov_bil", eFunctions.Values.eValuesType.clngWindowType, "", True, , , , , True, 4, "Concepto de pago que queda afectado en el pago")

            Call .AddPossiblesColumn(40266, "Cód. de Finiquito", "tcnId_Settle", "tabcl_settlements", eFunctions.Values.eValuesType.clngWindowType, "",  True, , , , ,  , 4, "Código del finiquito asociado a la cobertura.")

            '+ Tipo de pago "Deducible"
            If CDbl(Session("nPay_Type")) = 7 Then
                lblnDisabled = True
            End If

            Call .AddPossiblesColumn(40265, "Moneda de Cobertura", "cbeCurrency_cov", "Table11", eFunctions.Values.eValuesType.clngComboType, , , , , , , True, , "Moneda origen en la que esta expresada la provision de la cobertura")
            Call .AddNumericColumn(0, "% del Beneficiario", "tcnParticip", 5, , , "Porcentaje del Beneficiario", True, 2, , , , True)
            Call .AddNumericColumn(0, "Monto en moneda de Cobertura", "tcnLocAmount_Pay", 18, , , "Monto expresado en la moneda de origen de la cobertura", True, 6, , , , True)

            lobjColumn = .AddNumericColumn(40267, "Provision pendiente(Moneda de cobertura)", "tcnLocAmount", 18, "", , "Monto correspondiente al importe pendiente de reserva", True, 6, , , "ChangeValues(this);", True)
            lobjColumn.GridVisible = False
            Call .AddNumericColumn(40267, "Pago (Moneda de cobertura)", "tcnAmount_Paycov", 18, "", , "Monto correspondiente al pago, sin impuestos en moneda de la cobertura", True, 6, , , "ChangeValues(this);")
            lobjColumn = .AddNumericColumn(40267, "Provision pendiente(Moneda de pago)", "tcnProvPendPayCurr", 18, "", , "Monto correspondiente al importe pendiente de reserva", True, 6, , , "ChangeValues(this);", True)
            lobjColumn.PopUpVisible = False
            lobjColumn = .AddNumericColumn(40267, "Monto neto (Moneda de pago)", "tcnAmount", 18, "", , "Monto correspondiente al pago, sin impuestos", True, 6, , , "ChangeValues(this);", True)
            lobjColumn.GridVisible = False
            Call .AddHiddenColumn("nAmountBef", CStr(0)) '+ Valor del pago para la cobertura previamente indicado
            Call .AddNumericColumn(40268, "Factor de cambio", "tcnExchange", 11, "", , "Indica el factor de cambio a aplicar al monto del pago con el fin de obtener el monto en la moneda de instalación", True, 6, , , , True)
            Call .AddNumericColumn(40269, "Impuestos", "tcnTax", 6, CStr(0), , "Porcentaje de impuesto a aplicar", True, 2, , , "ChangeValues(this);")
            Call .AddNumericColumn(40270, "Monto a Pagar ", "tcnAmountPayCover", 18, "", , "Monto del pago de la cobertura/concepto, el cual incluye el monto de impuestos, en el caso de que se haya aplicado", True, 6, , , , True)
            lobjColumn = .AddNumericColumn(40270, "Monto a Pagar ", "tcnAmountPayCover2", 18, "", , "Monto del pago de la cobertura/concepto, el cual incluye el monto de impuestos, en el caso de que se haya aplicado", True, 6, , , , True)
            lobjColumn.PopUpVisible = False
            lobjColumn.GridVisible = False
            If Session("nPay_Type") = 2 Then
                lobjColumn = .AddNumericColumn(40271, "Total Pagos Parciales", "tcnAmountPayedCover", 18, "", , "Total pagado hasta la fecha sin incluir el pago en tratamiento.", True, 6, , , , True)
                lobjColumn.GridVisible = False
                lobjColumn = .AddNumericColumn(40272, "Deducible", "tcnFra_amount", 18, "", , "Valor del deducible al momento de hacer el pago.", True, 6, , , "ChangeValues(this);", True)
                lobjColumn.GridVisible = False
                lobjColumn = .AddNumericColumn(40273, "Valor a depreciar", "tcnDepreciatebase", 18, "", , "Valor a ser utilizado para el cálculo de la depreciación.", True, 6, , , "ChangeValues(this);", False)
                lobjColumn.GridVisible = False
                lobjColumn = .AddNumericColumn(40274, "Tasa a aplicar en la depreciación", "tcnDepreciaterate", 18, "", , "Tasa a aplicar en la depreciación.", True, 6, , , "ChangeValues(this);", False)
                lobjColumn.GridVisible = False
                lobjColumn = .AddNumericColumn(40275, "Depreciación", "tcnDepreciateamount", 18, "", , "Valor a ser tomado como depreciación para calcular el monto a indemnizar.", True, 6, , , "ChangeValues(this);", False)
                lobjColumn.GridVisible = False
                lobjColumn = .AddNumericColumn(40276, "RASA", "tcnRasa", 18, "", , "Valor a pagar por concepto de la restitución automática de la suma asegurada (RASA).", True, 6, , , , True)
                lobjColumn.GridVisible = False
                lobjColumn = .AddNumericColumn(40277, "DDR", "tcnDDR", 18, "", , "Valor total de los conceptos de deducibles (Deducible, depreciación, RASA).", True, 6, , , , True)
                lobjColumn.GridVisible = False
            Else
                Call .AddHiddenColumn("tcnAmountPayedCover", CStr(0)) '+ Total Pagos Parciales	- Total pagado hasta la fecha sin incluir el pago en tratamiento.
                Call .AddHiddenColumn("tcnFra_amount", CStr(0)) '+ Deducible - Valor del deducible al momento de hacer el pago.
                Call .AddHiddenColumn("tcnDepreciatebase", CStr(0)) '+ Valor a depreciar -	Valor a ser utilizado para el cálculo de la depreciación.
                Call .AddHiddenColumn("tcnDepreciaterate", CStr(0)) '+ Tasa a aplicar en la depreciación - Tasa a aplicar en la depreciación.
                Call .AddHiddenColumn("tcnDepreciateamount", CStr(0)) '+ Depreciación - Valor a ser tomado como depreciación para calcular el monto a indemnizar.
                Call .AddHiddenColumn("tcnRasa", CStr(0)) '+ RASA - Valor a pagar por concepto de la restitución automática de la suma asegurada (RASA).
                Call .AddHiddenColumn("tcnDDR", CStr(0)) '+ DDR - Valor total de los conceptos de deducibles (Deducible, depreciación, RASA).
            End If
            Call .AddHiddenColumn("hddRasaAnnual", CStr(0)) '+ Monto Anual de RASA
            Call .AddHiddenColumn("sPayconre", CStr(0)) '+ Payconre	- Indicador	de requerimiento de	desglose
            Call .AddHiddenColumn("nModulec", CStr(0)) '+ Modulec - Módulo	al que pertenece la	cobertura
            Call .AddHiddenColumn("nGroup_insu", CStr(0)) '+ Group_insu -	Grupo asegurado	al que pertenece la	cobertura
            Call .AddHiddenColumn("nIndAutomatic", "2") '+ Indicador de	Movimiento automático
            Call .AddHiddenColumn("nCoverCurrency", CStr(0)) '+ Moneda de	la cobertura
            Call .AddHiddenColumn("nAmountPayCover", CStr(0)) '+ Importe del pago de esta ocbertura en moneda del pago.
            Call .AddHiddenColumn("nAmount", CStr(0)) '+ Importe del pago de esta cobertura en moneda del pago.
            Call .AddHiddenColumn("hddClientupd", "") '+ Código del cliente.		
            Call .AddHiddenColumn("hddValdate_aux", "") '+ Fecha de valorización para la conversión.
            Call .AddHiddenColumn("hddServ_ord", "") '+ Número de la orden de servicio.
            Call .AddHiddenColumn("hddOffice", "") '+ Sucursal destino
            Call .AddHiddenColumn("hddOfficeAgen", "") '+ Oficina
            Call .AddHiddenColumn("hddAgency", "") '+ Agencia

            '+ Columnas ocultos para el manejo de la tabla temporal T_PayCla.
            Call .AddHiddenColumn("hddClaim_TPC", "")
            Call .AddHiddenColumn("hddCase_num_TPC", "")
            Call .AddHiddenColumn("hddDeman_type_TPC", "")
            Call .AddHiddenColumn("hddCover_curr_TPC", "")
            Call .AddHiddenColumn("hddModulec_TPC", "")
            Call .AddHiddenColumn("hddCover_TPC", "")
            Call .AddHiddenColumn("hddPay_concep_TPC", "")
            Call .AddHiddenColumn("hddTax_TPC", "")
            Call .AddHiddenColumn("hddtcnAmount", "")


            ' otras variables de t_paycla (TPC)
            '    nPay_amount    NUMBER     18,6 
            '    nCov_exchange  NUMBER     11.6 
            '    nTot_amount    NUMBER     18.6 
            '    nUsercode      NUMBER     5 
            '    nGroup_insu    NUMBER     5 
            '    sIndAuto       CHAR       1 
            Call .AddHiddenColumn("hddCurrency_pay_tpc", "")
            Call .AddHiddenColumn("hddPaycov_amount_tpc", "")
            Call .AddHiddenColumn("hddTotcov_amount_tpc", "")
            Call .AddHiddenColumn("hddRASA_routine", "")

            Call .AddHiddenColumn("hddCl_Cover_Reserve", "")

        End With


        '+ Se definen las propiedades generales	del	grid
        With mobjGrid
            .Codispl = "SI008"
            .Top = 0
            .Height = 600
            .Width = 580
            .DeleteButton = False
            mobjGrid.sEditRecordParam = "sClient='      + document.forms[0].valClient.value        + '" & "&sClient_rep=' + document.forms[0].hddvalClient_rep.value + '" & "&nRole='       + document.forms[0].cbeRole.value          + '" & "&nPayForm='    + document.forms[0].cbePayForm.value       + '" & "&nCurrency='   + document.forms[0].cbeCurrency.value      + '" & "&nExchange='   + document.forms[0].tcnExchange.value      + '" & "&nServ_order=' + document.forms[0].valServ_order.value    + '" & "&nInvoice='    + document.forms[0].tcnInvoice.value       + '" & "&dValdate='    + document.forms[0].tcdValdate.value       + '" & "&dBillDate='   + document.forms[0].tcdBillDate.value      + '" & "&nDoc_Type='   + document.forms[0].cbeDoc_Type.value      + '" & "&dPaydate='    + document.forms[0].tcdPaydate.value       + ' &nDeductible_Met='    + document.forms[0].cbeDeductible_Met.value       + '"

            If lclsCover.Find_SI008(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type"))) Then
                .AddButton = True
                lblnDisabled = False
                If CDbl(Session("nPay_Type")) = 7 Then
                    .Columns("valCover").EditRecord = False
                Else
                    .Columns("valCover").EditRecord = True
                End If
            Else
                .AddButton = True
                lblnDisabled = False
                .Columns("valCover").EditRecord = True
            End If


            .Splits_Renamed.AddSplit(0, " ", 11)
            .Splits_Renamed.AddSplit(0, " ", 12)


            .Columns("Sel").GridVisible = False
            .Columns("tcnAmount_Paycov").GridVisible = False
            If Request.QueryString("Reload") = "1" Then
                .sReloadIndex = Request.QueryString("ReloadIndex")
            End If
            '+ Se pasan	los	parámetros al campo	Cobertura
            .Columns("valCover").Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("valCover").Parameters.Add("nCase_num", Session("nCase_num"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("valCover").Parameters.Add("nDeman_type", Session("nDeman_type"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If (CStr(Session("SI008_sBrancht")) = "1" Or CStr(Session("SI008_sBrancht")) = "6") And CDbl(Session("SI008_cbeRole")) = 16 Then
                .Columns("valCover").Parameters.Add("sClient", Session("SI008_valClient"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Session("SI008_nId") = 0
                .Columns("valCover").Parameters.Add("nId", Session("SI008_nId"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If CStr(Session("SI008_sBrancht")) = "6" Then
                    .Columns("valCover").Parameters.Add("sIsSOAP", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Else
                    .Columns("valCover").Parameters.Add("sIsSOAP", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End If
            End If
            Session("nBranch") = lclsClaim.nBranch
            '+ Se pasan	los	parámetros al campo	Concepto de	pago 
            .Columns("tcnConcept").Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tcnConcept").Parameters.Add("nCover", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tcnConcept").Parameters.Add("nBranch", lclsClaim.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tcnConcept").Parameters.Add("nProduct", lclsClaim.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tcnConcept").Parameters.Add("dEffecdate", lclsClaim.dOccurdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            '+ Se pasan	los	parámetros al campo	Concepto de	pago 
            .Columns("tcnId_Settle").Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tcnId_Settle").Parameters.Add("nDeman_type", Session("nDeman_type"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tcnId_Settle").Parameters.Add("nCase_num", Session("nCase_num"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tcnId_Settle").Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tcnId_Settle").Parameters.Add("nCover", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tcnId_Settle").Parameters.Add("nPay_concep", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

        End With
        'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsClaim = Nothing
        'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsCover = Nothing
    End Sub

    '% insPreSI008:	se muestran	los	campos que no pertencen	al grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreSI008()
        '--------------------------------------------------------------------------------------------
        Dim lcolT_PayClas As eClaim.T_PayClas
        Dim lclsT_PayCla As eClaim.T_PayCla
        Dim lclsExchange As eGeneral.Exchange
        Dim lclsGen_cover As eProduct.Gen_cover
        Dim lclsCurren_pol As ePolicy.Curren_pol
        Dim lintCurrPol As Integer
        Dim lintCurr As Integer
        lclsGen_cover = New eProduct.Gen_cover
        Dim DateAux As Date = mobjValues.StringToDate(CStr(Session("SI008_tcdValdate")))
        If DateAux = eRemoteDB.Constants.dtmNull Then
            DateAux = Today
        End If
        lclsT_PayCla = New eClaim.T_PayCla
        lcolT_PayClas = New eClaim.T_PayClas
        lclsExchange = New eGeneral.Exchange
        lclsCurren_pol = New ePolicy.Curren_pol
        If CInt(Session("SI008_cbeCurrency")) < 1 Then
            Session("SI008_cbeCurrency") = 1
            Session("nCurrPaySI008") = 1

        End If
        If Session("SI008_cbeCurrency") <> vbNullString Then
            lclsClaim = New eClaim.Claim
            Call lclsClaim.Find(CDbl(Session("nClaim")))
            lintCurr = Session("SI008_cbeCurrency")
            If lclsCurren_pol.Find_Currency_Sel(lclsClaim.sCertype, lclsClaim.nBranch, lclsClaim.nProduct, lclsClaim.nPolicy, lclsClaim.nCertif, CDate(Session("dOccurdate_l"))) Then
                lintCurrPol = lclsCurren_pol.nCurrency
            Else
                lintCurrPol = 1
            End If
            lclsClaim = Nothing
            Call lclsExchange.Convert(eRemoteDB.Constants.intNull, 0, lintCurrPol, lintCurr, DateAux, eRemoteDB.Constants.intNull)
            If lclsExchange.pdblExchange > 0 Then
                Session("SI008_tcnExchange") = lclsExchange.pdblExchange
            Else
                Session("SI008_tcnExchange") = 1
            End If
        End If
        lclsCurren_pol = Nothing
        Response.Write("" & vbCrLf)
        Response.Write("<script>" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//- Variable para el controlar la carga de los datos" & vbCrLf)
        Response.Write("	var mintPayType = -1" & vbCrLf)
        Response.Write("	var mintCurrency = -1" & vbCrLf)
        Response.Write("	" & vbCrLf)
        Response.Write("//- Variable para indicar si existen registros en el grid (ver Table23)" & vbCrLf)
        Response.Write("	var mintCount = 2" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//- Variable para indicar si se está recargando la página" & vbCrLf)
        Response.Write("	var mblnReload = false" & vbCrLf)
        Response.Write("	" & vbCrLf)
        Response.Write("	var lstrClient =  ")


        Response.Write("'" & Session("SI008_valClient") & "'")


        Response.Write("  " & vbCrLf)
        Response.Write("	" & vbCrLf)
        Response.Write("//%	insSubmitPage: recarga la página para mostrar la información del grid" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function insSubmitPage(nValue){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------    " & vbCrLf)
        Response.Write("	var lstrLocation = '';" & vbCrLf)
        Response.Write("	lstrLocation += document.location.href;		" & vbCrLf)
        Response.Write("	lstrLocation = lstrLocation.replace(/&sClient.*/,"""")	" & vbCrLf)
        Response.Write("	lstrLocation = lstrLocation + ""&sClient="" + self.document.forms[0].elements[""valClient""].value" & vbCrLf)
        Response.Write("	                            + ""&nServ_order="" + self.document.forms[0].elements[""valServ_order""].value" & vbCrLf)
        Response.Write("	                            + ""&nOffice="" + self.document.forms[0].elements[""cbeOffice""].value" & vbCrLf)
        Response.Write("	                            + ""&nOfficeAgen="" + self.document.forms[0].elements[""cbeOfficeAgen""].value" & vbCrLf)
        Response.Write("	                            + ""&nAgency="" + self.document.forms[0].elements[""cbeAgency""].value;" & vbCrLf)
        Response.Write("	document.location.href = lstrLocation;" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//%	insActionReload: se realizan las acciones una vez que se recarga la página" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function insActionReload(){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("	mblnReload = true" & vbCrLf)
        Response.Write("	with(self.document.forms[0]){" & vbCrLf)
        Response.Write("		cmdAdd.disabled=false;" & vbCrLf)
        Response.Write("		valClient.disabled=(cbeRole.value==0)?true:false;" & vbCrLf)
        Response.Write("		btnvalClient.disabled=(cbeRole.value==0)?true:false;" & vbCrLf)
        Response.Write("	}" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//%	insStateSI008: recarga la página para mostrar la información del grid" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function insStateSI008(lblnEnabled){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("    var lintIndex=0;" & vbCrLf)
        Response.Write("    lblnEnabled = !lblnEnabled" & vbCrLf)
        Response.Write("    with (document.forms[0])" & vbCrLf)
        Response.Write("    {" & vbCrLf)
        Response.Write("        for (lintIndex=0;lintIndex<document.forms[0].elements.length;lintIndex++)" & vbCrLf)
        Response.Write("        {" & vbCrLf)
        Response.Write("            elements[lintIndex].disabled = lblnEnabled" & vbCrLf)
        Response.Write("        }" & vbCrLf)
        Response.Write("    }" & vbCrLf)
        Response.Write("    with (self.document)" & vbCrLf)
        Response.Write("    {" & vbCrLf)
        Response.Write("        //images['btnvalClient'].disabled = lblnEnabled        " & vbCrLf)
        Response.Write("    }" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("</" & "script>" & vbCrLf)
        Response.Write("    <BR>" & vbCrLf)
        Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("	     <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=0>Destinatario</LABEL></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""9130"">Figura</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        If mobjValues.StringToType(CStr(Session("SI008_tcdPayDate")), eFunctions.Values.eTypeData.etdDate) = eRemoteDB.Constants.dtmNull Then
            Session("SI008_tcdPayDate") = Session("dEffecdate")
        End If
        With mobjValues
            .Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eFunctions.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", Session("nCase_num"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", Session("nDeman_type"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(.PossiblesValues("cbeRole", "TABCLAIMBENEF_3", eFunctions.Values.eValuesType.clngComboType, CStr(Session("SI008_cbeRole")), True, , , , , "ChangeValues(this);", , , "Tipo de destinatario al que se va a hacer el pago"))
            'If mobjValues.StringToType (Session("SI008_cbeRole"),eFunctions.Values.eTypeData.etdDouble ) > 0 Then					
            '	Response.Write "<NOTSCRIPT>setTimeout(""insDefValues('Role','nRole=' + self.document.forms[0].cbeRole.value + '&sClient=' + lstrClient,'/VTimeNet/Claim/PaySeq');"",1);</" & "Script>"
            'End if
        End With

        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""40260"">&nbsp;</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""9122"">Cliente</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        With mobjValues
            .Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eFunctions.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", Session("nCase_num"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", Session("nDeman_type"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBene_type", mobjValues.StringToType(CStr(Session("SI008_cbeRole")), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eFunctions.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.ReturnValue("nId", True, "Id", True)
            .Parameters.ReturnValue("sConting", True, "Contingente", True)
            Response.Write(mobjValues.PossiblesValues("valClient", "tabClaimBenef_SI008", eFunctions.Values.eValuesType.clngWindowType, CStr(Session("SI008_valClient")), True,  ,  ,  ,  , "ChangeValues(this);", True, 14, "Código del beneficiario a quien se le hará el pago", eFunctions.Values.eTypeCode.eString,  ,  , False))
            If mobjValues.StringToType(CStr(Session("SI008_cbeRole")), eFunctions.Values.eTypeData.etdDouble) > 0 Then
                Response.Write("<script>setTimeout(""insDefValues('Role','nRole=' + self.document.forms[0].cbeRole.value + '&sClient=' + lstrClient,'/VTimeNet/Claim/PaySeq');"",500);</" & "Script>")
            End If
        End With

        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""5""><HR></TD>" & vbCrLf)
        Response.Write("        </TR>			" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""0"">Titular de la orden de pago</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.DIVControl("valClient_rep", , CStr(Session("SI008_valClient_rep"))))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""0"">&nbsp;</label></TD>			" & vbCrLf)
        Response.Write("    		<TD ><LABEL ID=""0"">Destino del cheque</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        With mobjValues
            '.Parameters.Add("nUsercode", Session("nUsercode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(Request.QueryString("nOffice"), eFunctions.Values.eTypeData.etdInteger), False, , , , , "BlankOfficeDepend();insInitialAgency(1,0);ChangeValues(this);", , 2, "Sucursal donde se registra el pago.", eFunctions.Values.eTypeCode.eNumeric))
        End With
        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>        " & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>Oficina</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        With mobjValues
            .Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.ReturnValue("nBran_off", , , True)
            Response.Write(.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString("nOfficeAgen"), True, , , , , "insInitialAgency(2,0)", False, , "Oficina donde se registra el pago."))
        End With

        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>            " & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>Agencia</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        With mobjValues
            .Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.ReturnValue("nBran_off", , , True)
            .Parameters.ReturnValue("nOfficeAgen", , , True)
            .Parameters.ReturnValue("sDesAgen", , , True)
            Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5555", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString("nAgency"), True, , , , , "insInitialAgency(3,0);ChangeValues(this);", False, , "Agencia donde se registra el pago."))
        End With

        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        </TR>                " & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""9128"">Forma de pago</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")

        'mobjValues.List = "4,5,6,7" 'Efectivo - Cheque manual - Conciliación - Según factura
        'mobjValues.TypeList = 2
        Response.Write(mobjValues.PossiblesValues("cbePayForm", "Table138", eFunctions.Values.eValuesType.clngComboType, IIf(String.IsNullOrEmpty(CStr(Session("SI008_cbePayForm"))), "1", CStr(Session("SI008_cbePayForm"))),    , , , , , "ChangeValues(this);", , , "Forma del pago que se quiere realizar"))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""40261"">&nbsp;</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""9131"">Orden</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")

        Dim valServ_order_disable As Boolean
        With mobjValues
            valServ_order_disable = True
            If CDbl(Session("nPay_Type")) = 3 Then
                valServ_order_disable = False
            Else
                If CDbl(Session("nPay_Type")) = 1 Then
                    valServ_order_disable = False
                Else
                    If CDbl(Session("nPay_Type")) = 2 Then
                        valServ_order_disable = False
                    Else
                        If CDbl(Session("nPay_Type")) = 5 Then
                            valServ_order_disable = False
                        ElseIf (Session("nPay_Type")) = 6  Then
                            valServ_order_disable = False
                        End If
                    End If
                End If
            End If
            .Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eFunctions.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", Session("nCase_num"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eFunctions.Tables.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", Session("nDeman_type"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eFunctions.Tables.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("valServ_order", "tabProf_ord", eFunctions.Values.eValuesType.clngWindowType, IIf(Session("OP006_nServ_order") = Nothing, Session("nServ_order"), Session("OP006_nServ_order")), True, , , , , "ChangeValues(this)", CBool(valServ_order_disable), 10, "Número de orden de servicios profesionales"))

        End With
        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""9126"">Documento</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnInvoice", 10, CStr(Session("SI008_tcnInvoice")), , "Número del documento que da soporte al pago", False, 0, , , , "ChangeValues(this);"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""0"">&nbsp;</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""9126"">Tipo de documento</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")

        mobjValues.BlankPosition = False
        Response.Write(mobjValues.PossiblesValues("cbeDoc_Type", "Table5570", eFunctions.Values.eValuesType.clngComboType,  IIf ( CStr(Session("SI008_cbeDoc_Type"))   = 0 , 9 ,  CStr(Session("SI008_cbeDoc_Type")) ), False, , , , , "ChangeValues(this);", , , "Tipo del documento que da soporte al pago", eFunctions.Values.eTypeCode.eNumeric))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""9126"">Fecha del documento</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.DateControl("tcdBillDate", CStr(Session("SI008_tcdBilldate")), , "Fecha del documento asociado al pago", , , , "ChangeValues(this);"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""0"">&nbsp;</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""9123"">Moneda de pago</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")
        mobjValues.BlankPosition = True

        Response.Write(mobjValues.PossiblesValues("cbeCurrency", "TabCurrency_b", eFunctions.Values.eValuesType.clngComboType, CStr(Session("SI008_cbeCurrency")),  ,  , , , , "ChangeValues(this);", False, , "Moneda en la que se realiza el pago"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=""0"">Fecha de valorización</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdValdate", CStr(Session("SI008_tcdValdate")), , "Fecha a tomar en cuenta para la conversión de la moneda del pago a la moneda local", , , , "ChangeValues(this);"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""0"">&nbsp;</label></TD>			" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""9125"">Factor de cambio</LABEL></TD> " & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnExchange", 11, CStr(Session("SI008_tcnExchange")), , "Indica el factor de cambio a utilizar para convertir el importe en la moneda del pago a la moneda local", True, 6, , , , , True))


        Response.Write("</TD>	" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD><LABEL ID=""9127"">Total pago (Moneda de pago)</label></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnAmountPay", 18, CStr(Session("SI008_tcnAmountPay")), , "Monto total del pago a realizar", True, 6, , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=""0"">&nbsp;</label></TD>			" & vbCrLf)
        Response.Write("            <TD><LABEL ID=""0"">Fecha efectiva de pago</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdPaydate", CStr(Session("SI008_tcdPayDate")), , "Fecha en la que debe hacerse efectivo el pago", , , , "ChangeValues(this);"))


        Response.Write("</TD>		" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)

        Response.Write("<tr> " & vbCrLf)
        Response.Write("  <td colspan=""3""> " & vbCrLf)
        Response.Write("  </td> " & vbCrLf)
        Response.Write("  <td> " & vbCrLf)
        Response.Write("     <label id=""Label1""> Aplicación del deducible</label> " & vbCrLf)
        Response.Write("  </td> " & vbCrLf)
        Response.Write("  <td> " & vbCrLf)
        Response.Write("      " & mobjValues.PossiblesValues("cbeDeductible_Met", "Table7230", eFunctions.Values.eValuesType.clngComboType, Session("SI008_cbeDeductible_Met"), False,   ,  ,  ,  , "ChangeValues(this)", IIf(mobjValues.StringToType(Session("nPay_Type"), eFunctions.Values.eTypeData.etdDouble) = 2, False, IIf(mobjValues.StringToType(Session("nPay_Type"), eFunctions.Values.eTypeData.etdDouble) = 3, False, True)),    , "Metodo de aplicacion del deducible", eFunctions.Values.eTypeCode.eNumeric, 8, False) & vbCrLf)
        Response.Write("  </td> " & vbCrLf)

        Response.Write("</tr>         " & vbCrLf)

        Response.Write("		")

        Response.Write(mobjValues.HiddenControl("hddvalClient_rep", CStr(Session("SI008_valClient_rep"))))
        Response.Write(mobjValues.HiddenControl("hddnBenefParticip", CStr(0)))
        Response.Write("" & vbCrLf)
        Response.Write("	</TABLE>")


        Dim ldblAmount As Object
        Dim lblPaycov_amount As Double
        Dim lblnParticip As Double
        Dim lclsCl_cover As eClaim.Cl_Cover
        lclsCl_cover = New eClaim.Cl_Cover
        ldblAmount = 0

        If lcolT_PayClas.FindSI008(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nPay_Type")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("SI008_tcnExchange")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("dOccurdate_l")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(CStr(Session("SI008_cbeCurrency")), eFunctions.Values.eTypeData.etdLong), CStr(Session("SI008_sBrancht"))) Then

            Session("SI008_tcnAmount_Paycov_sum") = 0

            For Each lclsT_PayCla In lcolT_PayClas
                With mobjGrid
                    Dim mobjQuery As New eRemoteDB.Query
                    Dim sind_applyddr As String = "2"
                    Dim nDDR_amount As Double
                    If mobjQuery.OpenQuery("t_paycla", "sind_applyddr, nDDR_amount ", "     NCLAIM      = " & Session("nClaim") &
                                                    " AND NCASE_NUM   = " & Session("nCase_num") &
                                                    " AND NDEMAN_TYPE = " & Session("nDeman_type") &
                                                    " AND NCOVER_CURR = " & lclsT_PayCla.nCover_curr.ToString() &
                                                    " AND NCOVER      = " & lclsT_PayCla.nCover.ToString() &
                                                    " AND NMODULEC    = " & lclsT_PayCla.nModulec.ToString() &
                                                    " AND NPAY_CONCEP = " & lclsT_PayCla.nPay_concep.ToString()) Then


                        sind_applyddr = mobjQuery.FieldToClass("sind_applyddr")
                        nDDR_amount = mobjValues.StringToType(mobjQuery.FieldToClass("nDDR_amount"), Values.eTypeData.etdDouble)

                    End If
                    lblPaycov_amount = lclsT_PayCla.nPaycov_amount
                    lblnParticip = lclsT_PayCla.nParticip
                    If lclsT_PayCla.nOutReserv > 0 And lblPaycov_amount = 0 Then
                        lblPaycov_amount = lclsT_PayCla.nOutReserv
                    End If

                    .Columns("nCoverCurrency").DefValue = CStr(lclsT_PayCla.nCover_curr)
                    .Columns("cbeCurrency_cov").DefValue = CStr(lclsT_PayCla.nCover_curr)
                    Session("SI008_cbeCurrencyCover") = lclsT_PayCla.nCover_curr
                    If lclsT_PayCla.nCover_curr <> eRemoteDB.Constants.intNull Then
                        lclsExchange.Find(CInt(lclsT_PayCla.nCover_curr), DateAux)
                        .Columns("tcnExchange").DefValue = lclsExchange.nExchange
                    End If
                    .Columns("nModulec").DefValue = CStr(lclsT_PayCla.nModulec)
                    .Columns("valCover").DefValue = CStr(lclsT_PayCla.nCover)
                    With mobjValues
                        Call lclsGen_cover.Find(.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), .StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble), lclsT_PayCla.nModulec, lclsT_PayCla.nCover, .StringToType(CStr(Session("dOccurdate_l")), eFunctions.Values.eTypeData.etdDate))
                        Call lclsCl_cover.Findkey(.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), .StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdLong), .StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdLong), lclsCl_cover.nModulec, lclsCl_cover.nCover, 1, "")
                    End With
                    .Columns("hddRASA_routine").DefValue = lclsT_PayCla.sRasa_routine
                    .Columns("tcnAmountPayedCover").DefValue = CStr(lclsCl_cover.nPay_amount)
                    .Columns("tcnFra_amount").DefValue = CStr(lclsT_PayCla.nFra_amount)
                    .Columns("tcnDepreciatebase").DefValue = CStr(lclsT_PayCla.nDepreciatebase)
                    .Columns("tcnDepreciaterate").DefValue = CStr(lclsT_PayCla.nDepreciaterate)
                    .Columns("tcnDepreciateamount").DefValue = CStr(lclsT_PayCla.nDepreciateamount)
                    .Columns("tcnRasa").DefValue = CStr(lclsT_PayCla.nRasa)
                    .Columns("tcnDDR").DefValue = CStr(lclsT_PayCla.nRasa + lclsT_PayCla.nDepreciateamount + lclsT_PayCla.nFra_amount)

                    If lclsGen_cover.sIndManualDeductible = "1" Then
                        .Columns("tcnFra_amount").Disabled = False
                    Else
                        .Columns("tcnFra_amount").Disabled = True
                    End If
                    .Columns("tcnConcept").DefValue = CStr(lclsT_PayCla.nPay_concep)
                    .Columns("tcnId_Settle").DefValue = CStr(lclsT_PayCla.nId_Settle)
                    .Columns("tcnConcept").Parameters("nModulec").Value = lclsT_PayCla.nModulec
                    .Columns("tcnConcept").Parameters("nCover").Value = lclsT_PayCla.nCover
                    .Columns("tcnConcept").Parameters("nBranch").Value = Session("nBranch")
                    .Columns("tcnConcept").Parameters("nProduct").Value = Session("nProduct")
                    .Columns("tcnConcept").Parameters("dEffecdate").Value = Session("dOccurdate_l")

                    .Columns("tcnAmount").DefValue = CStr(lclsT_PayCla.nPay_amount)
                    .Columns("tcnParticip").DefValue = CStr(lclsT_PayCla.nParticip)
                    If lclsT_PayCla.nPay_amount = eRemoteDB.Constants.intNull Then
                        .Columns("nAmountBef").DefValue = CStr(0)
                    Else
                        .Columns("nAmountBef").DefValue = CStr(lclsT_PayCla.nPay_amount)
                    End If

                    Call lclsExchange.Convert(eRemoteDB.Constants.intNull, 0, lclsT_PayCla.nCover_curr, CInt(Session("SI008_cbeCurrency")), CDate(Session("SI008_tcdValdate")), eRemoteDB.Constants.intNull)

                    .Columns("tcnExchange").DefValue = CStr(lclsExchange.pdblExchange)

                    If lclsT_PayCla.nTax < 0 Then
                        .Columns("tcnTax").DefValue = CStr(mobjValues.TypeToString(lclsT_PayCla.nTax, eFunctions.Values.eTypeData.etdDouble, True, 2) * (-1))
                    Else
                        .Columns("tcnTax").DefValue = mobjValues.TypeToString(lclsT_PayCla.nTax, eFunctions.Values.eTypeData.etdDouble, True, 2)
                    End If
                    .Columns("tcnAmountPayCover").DefValue = mobjValues.TypeToString(lclsT_PayCla.nTot_amount, eFunctions.Values.eTypeData.etdDouble, True, 6)
                    .Columns("tcnLocAmount").DefValue = mobjValues.TypeToString(lblPaycov_amount, eFunctions.Values.eTypeData.etdDouble, True, 6)


                    .Columns("nAmount").DefValue = mobjValues.TypeToString(lclsT_PayCla.nTot_amount, eFunctions.Values.eTypeData.etdDouble, True, 6)

                    .Columns("nAmountPayCover").DefValue = mobjValues.TypeToString(lclsT_PayCla.nPay_amount, eFunctions.Values.eTypeData.etdDouble, True, 6)
                    .Columns("tcnParticip").DefValue = mobjValues.TypeToString(lclsT_PayCla.nParticip, eFunctions.Values.eTypeData.etdDouble, True, 6)
                    .Columns("sPayconre").DefValue = ""
                    .Columns("nGroup_insu").DefValue = CStr(lclsT_PayCla.nGroup_insu)
                    .Columns("nIndAutomatic").DefValue = lclsT_PayCla.sIndAuto
                    .Columns("hddClaim_TPC").DefValue = CStr(lclsT_PayCla.nClaim)
                    .Columns("hddCase_num_TPC").DefValue = CStr(lclsT_PayCla.nCase_num)
                    .Columns("hddDeman_type_TPC").DefValue = CStr(lclsT_PayCla.nDeman_type)
                    .Columns("hddCover_curr_TPC").DefValue = CStr(lclsT_PayCla.nCover_curr)
                    .Columns("hddCover_TPC").DefValue = CStr(lclsT_PayCla.nCover)
                    .Columns("hddPay_concep_TPC").DefValue = CStr(lclsT_PayCla.nPay_concep)
                    .Columns("hddTax_TPC").DefValue = CStr(lclsT_PayCla.nTax)
                    .Columns("hddCurrency_pay_tpc").DefValue = CStr(lclsT_PayCla.nCurrency_pay)
                    .Columns("hddPaycov_amount_tpc").DefValue = CStr(lblPaycov_amount)
                    .Columns("hddTotcov_amount_tpc").DefValue = CStr(lclsT_PayCla.nTotcov_amount)
                    .Columns("tcnId_Settle").Parameters("nModulec").Value = lclsT_PayCla.nModulec
                    .Columns("tcnId_Settle").Parameters("nCover").Value = lclsT_PayCla.nCover

                    .Columns("tcnId_Settle").DefValue = CStr(lclsT_PayCla.nId_Settle)


                    If CDbl(Session("nPay_Type")) = 7 Then
                        Session("SI008_tcnAmount_Paycov_sum") = mobjValues.TypeToString(lclsT_PayCla.nFra_amount, eFunctions.Values.eTypeData.etdDouble, True, 6)
                    End If

                    .sEditRecordParam = "sClient='      + document.forms[0].valClient.value         + '" & "&sClient_rep=' + document.forms[0].hddvalClient_rep.value  + '" & "&nRole='       + document.forms[0].cbeRole.value           + '" & "&nPayForm='    + document.forms[0].cbePayForm.value        + '" & "&nCurrency='   + document.forms[0].cbeCurrency.value       + '" & "&nExchange='   + document.forms[0].tcnExchange.value       + '" & "&nServ_order=' + document.forms[0].valServ_order.value     + '" & "&nInvoice='    + document.forms[0].tcnInvoice.value        + '" & "&dValdate='    + document.forms[0].tcdValdate.value        + '" & "&dBillDate='   + document.forms[0].tcdBillDate.value       + '" & "&nDoc_Type='   + document.forms[0].cbeDoc_Type.value       + '" & "&dPaydate='    + document.forms[0].tcdPaydate.value      +   '&nDoc_Type='    +   document.forms[0].cbeDoc_Type.value  +'&nDeductible_Met='    + document.forms[0].cbeDeductible_Met.value       + '"
                    'se asigna el monto de pago neto, para que se descuente o sume el impuesto en la op006
                    'Segùn el valor de la moneda de pago, se recalcula el monto del campo: Total Pago (moneda de pago).
                    'eCalaim.T_PayCla.insChangeValdate_Currency.


                    .Columns("tcnLocAmount_Pay").DefValue = CStr(lblPaycov_amount)
                    If sind_applyddr = "1" Then
                        .Columns("tcnAmountPayCover2").DefValue = mobjValues.TypeToString(lclsT_PayCla.nPay_amount - nDDR_amount, eFunctions.Values.eTypeData.etdDouble, True, 6)
                        ldblAmount =  mobjValues.TypeToString(lclsT_PayCla.nPay_amount - nDDR_amount, eFunctions.Values.eTypeData.etdDouble, True, 6)
                        .Columns("tcnAmountPayCover").GridVisible = False
                        .Columns("tcnAmountPayCover2").GridVisible = True
                    Else
                        .Columns("tcnAmountPayCover").DefValue = mobjValues.TypeToString(lclsT_PayCla.nPay_amount, eFunctions.Values.eTypeData.etdDouble, True, 6)
                        ldblAmount =  mobjValues.TypeToString(lclsT_PayCla.nPay_amount, eFunctions.Values.eTypeData.etdDouble, True, 6)
                        .Columns("tcnAmountPayCover2").GridVisible = False
                        .Columns("tcnAmountPayCover").GridVisible = True
                    End If

                    .Columns("tcnAmount_Paycov").DefValue = CStr(lblPaycov_amount)
                    .Columns("tcnParticip").DefValue = CStr(lblnParticip)


                    If sind_applyddr = "1" Then
                        Session("SI008_tcnAmount_Paycov_sum") = CDbl(Session("SI008_tcnAmount_Paycov_sum")) + (lblPaycov_amount - nDDR_amount)
                    Else
                        Session("SI008_tcnAmount_Paycov_sum") = CDbl(Session("SI008_tcnAmount_Paycov_sum")) + lblPaycov_amount
                    End If

                    'If CDbl(.Columns("tcnExchange").DefValue) = 0 Then
                    '    .Columns("tcnLocAmount_Pay").DefValue = CStr(lclsT_PayCla.nOutReserv)
                    'Else
                    '    .Columns("tcnLocAmount_Pay").DefValue = CStr(lclsT_PayCla.nOutReserv)
                    'End If

                    .Columns("tcnProvPendPayCurr").DefValue = CStr(lclsT_PayCla.nOutReserv - lblPaycov_amount)
                    Response.Write(.DoRow)
                End With
            Next lclsT_PayCla
        End If
        Response.Write(mobjGrid.closeTable())
        Response.Write("<script> self.document.forms[0].tcnAmountPay.value = '" & mobjValues.TypeToString(ldblAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';</" & "Script>")
        Session("SI008_tcnAmountPay") = mobjValues.TypeToString(ldblAmount, eFunctions.Values.eTypeData.etdDouble, True, 6)
        Session("SI008_nAmountPay") = mobjValues.TypeToString(lcolT_PayClas.nTotPayAmo, eFunctions.Values.eTypeData.etdDouble, 6)

        ldblAmount = FormatNumber(ldblAmount - mobjValues.StringToType(CStr(Session("SI008_Premium")), eFunctions.Values.eTypeData.etdDouble), 6)
        Session("OP006_nAmountPay") = mobjValues.TypeToString(ldblAmount, eFunctions.Values.eTypeData.etdDouble, True, 6)

        If Not String.IsNullOrEmpty(Request.QueryString("bReload")) Then
            If CBool(Request.QueryString("bReload")) Then
                Response.Write("<script>insActionReload()</" & "Script>")
            End If
        End If

        'UPGRADE_NOTE: Object lclsT_PayCla may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsT_PayCla = Nothing
        'UPGRADE_NOTE: Object lcolT_PayClas may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lcolT_PayClas = Nothing
        'UPGRADE_NOTE: Object lclsExchange may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsExchange = Nothing
    End Sub
    '
    '% insPreSI008Upd: se muestran los campos que pertencen	al grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreSI008Upd()
        '--------------------------------------------------------------------------------------------
        Dim lblnEnabled As Byte

        mobjGrid.Columns("tcnExchange").DefValue = Request.QueryString("nExchange")
        If Request.QueryString("Action") = "Update" Then
            mobjGrid.Columns("tcnConcept").Disabled = False
        End If

        With Response
            .Write(mobjValues.HiddenControl("hddPayType", CStr(Session("nPay_Type"))))
            .Write(mobjValues.HiddenControl("hddRole", Request.QueryString("nRole")))
            .Write(mobjValues.HiddenControl("hddClient", Request.QueryString("sClient")))
            .Write(mobjValues.HiddenControl("hddPayForm", Request.QueryString("nPayForm")))
            .Write(mobjValues.HiddenControl("hddCurrency", Request.QueryString("nCurrency")))
            .Write(mobjValues.HiddenControl("hddExchange", Request.QueryString("nExchange")))
            .Write(mobjValues.HiddenControl("hddServ_order", Request.QueryString("nServ_order")))
            .Write(mobjValues.HiddenControl("hddInvoice", Request.QueryString("nInvoice")))
            .Write(mobjValues.HiddenControl("hddValdate", Request.QueryString("dValdate")))
            .Write(mobjValues.HiddenControl("hddBillDate", Request.QueryString("dBillDate")))
            .Write(mobjValues.HiddenControl("hddDoc_Type", Request.QueryString("nDoc_Type")))
            .Write(mobjValues.HiddenControl("hddPaydate", Request.QueryString("dPaydate")))
            .Write(mobjValues.HiddenControl("hddClient_rep", Request.QueryString("sClient_rep")))
            .Write(mobjValues.HiddenControl("hddOffice_pay", Request.QueryString("nOffice_pay")))

            If mobjGrid.AddButton Then
                lblnEnabled = 1
            Else
                lblnEnabled = 0
            End If
            .Write(mobjGrid.DoFormUpd(Request.QueryString("Action"), "valPaySeq.aspx", "SI008", Request.QueryString("nMainAction"), CBool(Session("bQuery")), Request.QueryString("Index")))

            .Write("<script>" & "with(self.document.forms[0]){")
            If lblnEnabled = 0 Then
                .Write("valCover.disabled=false;" & "chkContinue.checked=false;" & "chkContinue.disabled=true;")
            End If
            If Request.QueryString("nServ_order") <> "" Then
                .Write(" tcnAmount_Paycov.disabled=true; ")
            End If
            .Write("hddClientupd.value = top.opener.document.forms[0].elements['valClient'].value;" & "hddServ_ord.value = top.opener.document.forms[0].elements['valServ_order'].value;" & "hddValdate_aux.value = top.opener.document.forms[0].elements['tcdValdate'].value;" & "hddOffice.value = top.opener.document.forms[0].elements['cbeOffice'].value;" & "hddOfficeAgen.value = top.opener.document.forms[0].elements['cbeOfficeAgen'].value;" & "hddAgency.value = top.opener.document.forms[0].elements['cbeAgency'].value;" & "if(insConvertNumber(tcnAmount.value)==0){" & "ChangeValues(tcnAmount_Paycov);}" & "}" & "</" & "Script>")
        End With
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("si008")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "si008"
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    If CStr(Session("SI008_tcdValdate")) = vbNullString Then
        'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
        Session("SI008_tcdValdate") = Today
    End If
%>
<html>
<head>
    <script>
//- Variable que indica el ramo técnico actual.
    var mstrBrancht 
    <%="mstrBrancht='" & Session("SI008_sBrancht") & "'"%>
    
//- Variable que indica el tipo de pago actual.    
    var mlngPay_Type
    <%="mlngPay_Type='" & Session("nPay_Type") & "'"%>    


//%	ChangeValues: se realiza los cambios en los controles dependientes)
//-------------------------------------------------------------------------------------------
function ChangeValues(Field){
//-------------------------------------------------------------------------------------------
    var lstrQString 
    var ldblAmount 
	
	switch(Field.name){
//+ Pago (Moneda de origen)
		case "tcnAmount_Paycov":  
			with(self.document.forms[0])
			{
				if(Field.value=="" || Field.value=="0")
				{
 					tcnAmount_Paycov.value = "0.000000" 
					tcnAmountPayCover.value = "0.000000"
					tcnAmount.value = "0.000000"
				}
				else
				{
//+	Se calcula el monto	con	el porcentaje de impuesto
					lstrQString = 'nAmount=' + Field.value + 
					              '&nCurrency=' + hddCurrency.value + 
					              '&nCoverCurr=' + nCoverCurrency.value + 
					              '&nTaxamo=' + hddTax_TPC.value + 
					              '&dValdate=' + hddValdate_aux.value +
					              '&nTyp=1' 
					insDefValues('AmountPay',lstrQString,'/VTimeNet/Claim/PaySeq');
					
					self.document.forms[0].tcnLocAmount_Pay.value = self.document.forms[0].tcnAmount_Paycov.value.replace('.', '');
					self.document.forms[0].tcnAmount.value = self.document.forms[0].tcnAmount.value;
                    self.document.forms[0].tcnAmountPayCover.value = self.document.forms[0].tcnAmount_Paycov.value.replace('.', '');
					//self.document.forms[0].tcnAmount.value = self.document.forms[0].hddCl_Cover_Reserve.value - self.document.forms[0].tcnAmount_Paycov.value.replace('.', '');
                    
                    lstrQString = 'nClaim=' + hddClaim_TPC.value +  
                                  '&nCase_num=' + self.document.forms[0].hddCase_num_TPC.value + 
                                  '&nDeman_type=' + self.document.forms[0].hddDeman_type_TPC.value +       
				                  '&nModulec=' + nModulec.value + 
				                  '&nGroup_insu='    + nGroup_insu.value +
				                  '&nCover='         + valCover.value +
                                  '&sOrigin=2' +
                                  '&nFra_amount='         + tcnFra_amount.value +                                  
				                  '&nDepreciateamount='       + tcnDepreciateamount.value +
                                  '&nDepreciatebase='       + tcnDepreciatebase.value +
                                  '&nDepreciaterate='       + tcnDepreciaterate.value +
				                  '&nAmountPayedCover='    + tcnAmount.value +
				                  '&nAmountPayCover='    + tcnAmount_Paycov.value +
				                  '&sRASA_routine='    + hddRASA_routine.value       
                    //alert(lstrQString);
                    //insDefValues('CalSi008', lstrQString, '/VTimeNet/Claim/PaySeq');


				}
			}
			break;
			
//+ Monto neto (Moneda de pago)
        case "tcnAmount": 
			with(self.document.forms[0]) 
			{ 
				if(Field.value=="" || Field.value=="0") 
				{ 
 					tcnAmount_Paycov.value = "0.000000" 
					tcnAmountPayCover.value = "0.000000" 
					tcnAmount.value = "0.000000" 
				} 
				else 
				{ 
//+	Se calcula el monto	con	el porcentaje de impuesto 
					lstrQString = 'nAmount=' + Field.value + 
								  '&nCurrency=' + hddCurrency.value + 
								  '&nCoverCurr=' + nCoverCurrency.value + 
								  '&nTaxamo=' + hddTax_TPC.value + 
								  '&dValdate=' + hddValdate_aux.value +
					              '&nTyp=2' 
					insDefValues('AmountPay',lstrQString,'/VTimeNet/Claim/PaySeq');

                    lstrQString = 'nClaim=' + hddClaim_TPC.value +  
                                  '&nCase_num=' + self.document.forms[0].hddCase_num_TPC.value + 
                                  '&nDeman_type=' + self.document.forms[0].hddDeman_type_TPC.value +       
				                  '&nModulec=' + nModulec.value + 
				                  '&nGroup_insu='    + nGroup_insu.value +
				                  '&nCover='         + valCover.value +
                                  '&sOrigin=2' +
                                  '&nFra_amount='         + tcnFra_amount.value +                                  
				                  '&nDepreciateamount='       + tcnDepreciateamount.value +
                                  '&nDepreciatebase='       + tcnDepreciatebase.value +
                                  '&nDepreciaterate='       + tcnDepreciaterate.value +
				                  '&nAmountPayedCover='    + tcnAmountPayedCover.value +
				                  '&nAmountPayCover='    + tcnAmount.value +
				                  '&sRASA_routine='    + hddRASA_routine.value
                    insDefValues('CalSi008', lstrQString, '/VTimeNet/Claim/PaySeq');

				} 
			} 
			break; 

//+Impuestos
		case "tcnTax": 
//+	Se calcula el monto	con	el porcentaje de impuesto
			with(self.document.forms[0])
			{
				if(tcnAmount.value=="" || tcnAmount.value=="0")
					tcnAmountPayCover.value = "0.000000"
				else
				{
//+	Se calcula el monto	con	el porcentaje de impuesto  
					ldblAmount = insConvertNumber(tcnAmountPayCover.value)
                    if (insConvertNumber(hddTax_TPC.value) < 0) { 
					    hddTax_TPC.value = insConvertNumber(tcnTax.value) * -1 }
					else {
					hddTax_TPC.value = insConvertNumber(tcnTax.value) }
					ldblAmount +=  ldblAmount * insConvertNumber(hddTax_TPC.value)/100;
					tcnAmountPayCover.value = VTFormat(ldblAmount,'','','',6,true);
					hddTax_TPC.value = VTFormat(ldblAmount,'','','',6,true);
				}  
			}  
			break;
				
		case "valCover":		//+ Cobertura
              
			with(self.document.forms[0]){
				tcnConcept.value="";
                tcnConcept.disabled=(valCover.value==0)?true:false;
				self.document.images['btntcnConcept'].disabled = (valCover.value==0)?true:false;
				UpdateDiv("tcnConceptDesc","")
				lstrQString = 'nCover='+ Field.value + 
				              '&nModulec=' + self.document.forms[0].elements["nModulec"].value + 
				              '&nTaxamo=' + tcnTax.value + 
				              '&nCurrency=' + hddCurrency.value + 
                              '&Serv_order=' + hddServ_order.value +
				              '&dValdate=' + hddValdate_aux.value + 
				              '&nAmountPayCover=' + tcnAmountPayCover.value
				if(Field.value!='') {
				    if ((tcnLocAmount_Pay.value == '') || 
				        (tcnLocAmount_Pay.value == '0,000000')) 
				        insDefValues('Cover', lstrQString, '/VTimeNet/Claim/PaySeq');
                    else
                     {
                       self.document.forms[0].tcnId_Settle.Parameters.Param4.sValue=self.document.forms[0].elements["nModulec"].value;
                       self.document.forms[0].tcnId_Settle.Parameters.Param5.sValue=Field.value;
                     }

					}
                else { 
    	            nCoverCurrency.value='';
    	            tcnConcept.value= '';
    	            UpdateDiv('tcnConceptDesc','');
    	            cbeCurrency_cov.value='0';
    	            tcnLocAmount.value='0,000000';
    	            tcnAmount.value='0,000000';
    	            tcnExchange.value=VTFormat('0','','','',6);
    	            tcnAmount_Paycov.value='0,000000';
                    tcnAmountPayCover.value='0,000000';
                }
			}
			break;

        case "valClient": //+Cliente  
			with(self.document.forms[0])
			{
	            if (Field.value != "0")
	            {
                    valServ_order.value='';
                    UpdateDiv('valServ_orderDesc','','Normal');
	                lstrQString = 'sClient='    + Field.value
	                            + '&nRole='     + cbeRole.value
	                            + '&nCurrency=' + cbeCurrency.value
	                            + '&dValdate='  + tcdValdate.value
	                            + '&nId='       + valClient_nId.value;
                    insDefValues('Client_rep',lstrQString,'/VTimeNet/Claim/PaySeq');
    	        }
    	    }    
            break;

        

		case "cbeRole":		//+	Figura
			with(self.document.forms[0])
			{
				valClient.disabled=(cbeRole.value==0)?true:false;
				btnvalClient.disabled=(cbeRole.value==0)?true:false;				
	            if (Field.value != "0")
	            {
					
	                self.document.forms[0].valClient.Parameters.Param4.sValue=Field.value;
	                lstrQString = 'nCurrency='    + cbeCurrency.value 
	                            + '&dValdate='    + tcdValdate.value 
	                            + '&nRole='       + Field.value 
	                            + '&nServ_order=' + valServ_order.value 
	                            + '&nOffice='     + cbeOffice.value
	                            + '&nOfficeAgen=' + cbeOfficeAgen.value
	                            + '&nAgency='     + cbeAgency.value;
                    insDefValues('Role', lstrQString, '/VTimeNet/Claim/PaySeq');
                    if (self.document.forms[0].cbePayForm.value == "")
                    {
						self.document.forms[0].cbePayForm.value = 10;
						lstrQString = 'FieldControl=cbePayForm&Value=10' 
                        insDefValues('SI008',lstrQString,'/VTimeNet/Claim/PaySeq');
                    }
                    
	            }	
	            else
	            {	            	
			    	valClient.value=""
			    	hddvalClient_rep.value=""
			    	UpdateDiv("valClientDesc","")
			    	UpdateDiv("valClient_rep","")
			    	lstrQString = 'nCurrency='    + cbeCurrency.value 
			    	            + '&dValdate='    + tcdValdate.value 
			    	            + '&nRole='       + Field.value 
			    	            + '&nServ_order=' + valServ_order.value
	                            + '&nOffice='     + cbeOffice.value
	                            + '&nOfficeAgen=' + cbeOfficeAgen.value
	                            + '&nAgency='     + cbeAgency.value;	                            
			    	insDefValues('Role',lstrQString,'/VTimeNet/Claim/PaySeq');
	      	    }	      	    
			}
			break;

		case "cbeCurrency":	//+	Moneda
			with(self.document.forms[0])
			{
				if(cbeCurrency.value==0)
				{
					tcnExchange.value=VTFormat('0','','','',6);
                    lstrQString = 'FieldControl=' + Field.name + '&Value=' + Field.value
                    insDefValues('SI008',lstrQString,'/VTimeNet/Claim/PaySeq');
				}
				else
				{
/*+ Se calcula el factor de cambio */
/*+ Se eliminan los datos asociados al pago */
					if(mintCurrency!=Field.value)
					{
					    if(typeof(self.document.forms[0].hddClaim_TPC)!='undefined' &&
						   typeof(self.document.forms[0].hddClaim_TPC.value)!='undefined' ){
                            lstrQString = 'nCurrency=' + Field.value + '&dValdate=' + self.document.forms[0].tcdValdate.value + 
                                          '&nClaim_TPC='         + self.document.forms[0].hddClaim_TPC.value + 
                                          '&nCase_num_TPC='      + self.document.forms[0].hddCase_num_TPC.value + 
                                          '&nDeman_type_TPC='    + self.document.forms[0].hddDeman_type_TPC.value + 
                                          '&nCover_curr_TPC='    + self.document.forms[0].hddCover_curr_TPC.value + 
                                          '&nCover_TPC='         + self.document.forms[0].hddCover_TPC.value +
                                          '&nModulec_TPC='       + self.document.forms[0].nModulec.value +  
                                          '&nPay_concep_TPC='    + self.document.forms[0].hddPay_concep_TPC.value +                                       
                                          '&nTax_TPC='           + self.document.forms[0].hddTax_TPC.value +                                       
                                          '&nGroup_insu_TPC='    + self.document.forms[0].nGroup_insu.value +
                                          '&nIndAutomatic_TPC='  + self.document.forms[0].nIndAutomatic.value + 
				                          '&nPaycov_amount_TPC=' + self.document.forms[0].hddPaycov_amount_tpc.value 
				                          '&nCalTaxFix=No'
                        }                  
                        else
                            lstrQString = 'nCurrency=' + Field.value + '&dValdate=' + self.document.forms[0].tcdValdate.value;

<%--					if (cbeDeductible_Met.value == '4') // Cruzado
	                {
	                    lstrQString += "&sApplyDDR=1&nClaim=" + <%=Session("nClaim") %> + "&nCase_Num=" + <%=Session("nCase_num") %> + "&nDeman_type=" + <%=Session("nDeman_type") %> ;
	                }
	                else
	                {
	                    lstrQString += "&sApplyDDR=2&nClaim=" + <%=Session("nClaim") %> + "&nCase_Num=" + <%=Session("nCase_num") %> + "&nDeman_type=" + <%=Session("nDeman_type") %> ;
	                }--%>

                        insDefValues('Currency',lstrQString ,'/VTimeNet/Claim/PaySeq');
					}
				}	
			}
			break;

		case "tcdValdate":	/*+	Fecha de valorización*/
			with(self.document.forms[0])
			{
				if (cbeCurrency.value != 0) {
				    insDefValues('Exchange','dValdate=' + Field.value + '&nCurrency=' + cbeCurrency.value,'/VTimeNet/Claim/PaySeq');
				    if(typeof(self.document.forms[0].hddClaim_TPC)!='undefined'){
				        lstrQString = 'nCurrency=' + cbeCurrency.value +  
				                      '&dValdate=' + Field.value + 
				                      '&nClaim_TPC='         + hddClaim_TPC.value +
				                      '&nCase_num_TPC='      + hddCase_num_TPC.value +
				                      '&nDeman_type_TPC='    + hddDeman_type_TPC.value +
				                      '&nCover_curr_TPC='    + hddCover_curr_TPC.value +
				                      '&nCover_TPC='         + hddCover_TPC.value +
				                      '&nModulec_TPC='       + nModulec.value +
				                      '&nPay_concep_TPC='    + hddPay_concep_TPC.value +
				                      '&nTax_TPC='           + hddTax_TPC.value +
				                      '&nGroup_insu_TPC='    + nGroup_insu.value +
				                      '&nIndAutomatic_TPC='  + nIndAutomatic.value +
				                      '&nPaycov_amount_TPC=' + hddPaycov_amount_tpc.value 
				    }
				    else
				        lstrQString = 'nCurrency=' + cbeCurrency.value + '&dValdate=' + Field.value;

<%--					if (cbeDeductible_Met.value == '4') // Cruzado
	                {
	                    lstrQString += "&sApplyDDR=1&nClaim=" + <%=Session("nClaim") %> + "&nCase_Num=" + <%=Session("nCase_num") %> + "&nDeman_type=" + <%=Session("nDeman_type") %> ;
	                }
	                else
	                {
	                    lstrQString += "&sApplyDDR=2&nClaim=" + <%=Session("nClaim") %> + "&nCase_Num=" + <%=Session("nCase_num") %> + "&nDeman_type=" + <%=Session("nDeman_type") %> ;
	                }--%>

				    insDefValues('Currency',lstrQString ,'/VTimeNet/Claim/PaySeq');
				} 
			}		
			break;

        case "cbeOffice": /*+ Oficina */
			with(self.document.forms[0])
			{
                lstrQString = 'FieldControl=cbeOfficeAux' + '&Value=' + Field.value
                insDefValues('SI008',lstrQString,'/VTimeNet/Claim/PaySeq');
            }
            break;

        case "tcnFra_amount": /* + Deducible */
			with(self.document.forms[0])
			{
                    lstrQString = 'nClaim=' + hddClaim_TPC.value +  
                                  '&nCase_num=' + self.document.forms[0].hddCase_num_TPC.value + 
                                  '&nDeman_type=' + self.document.forms[0].hddDeman_type_TPC.value +       
				                  '&nModulec=' + nModulec.value + 
				                  '&nGroup_insu='    + nGroup_insu.value +
				                  '&nCover='         + valCover.value +
                                  '&sOrigin=1' +
                                  '&nFra_amount='         + tcnFra_amount.value +                                  
				                  '&nDepreciateamount='       + tcnDepreciateamount.value +
                                  '&nDepreciatebase='       + tcnDepreciatebase.value +
                                  '&nDepreciaterate='       + tcnDepreciaterate.value +
				                  '&nAmountPayedCover='    + tcnAmountPayedCover.value +
				                  '&nAmountPayCover='    + tcnAmount_Paycov.value +
				                  '&sRASA_routine='    + hddRASA_routine.value    
                    insDefValues('CalSi008', lstrQString, '/VTimeNet/Claim/PaySeq');


            }
            break;

        case "tcnDepreciatebase": /*+ Base de Depreciacion */
			with(self.document.forms[0])
			{
                    lstrQString = 'nClaim=' + hddClaim_TPC.value +  
                                  '&nCase_num=' + self.document.forms[0].hddCase_num_TPC.value + 
                                  '&nDeman_type=' + self.document.forms[0].hddDeman_type_TPC.value +       
				                  '&nModulec=' + nModulec.value + 
				                  '&nGroup_insu='    + nGroup_insu.value +
				                  '&nCover='         + valCover.value +
                                  '&sOrigin=1' +
                                  '&nFra_amount='         + tcnFra_amount.value +                                  
				                  '&nDepreciateamount='       + tcnDepreciateamount.value +
                                  '&nDepreciatebase='       + tcnDepreciatebase.value +
                                  '&nDepreciaterate='       + tcnDepreciaterate.value +
				                  '&nAmountPayedCover='    + tcnAmountPayedCover.value +
				                  '&nAmountPayCover='    + tcnAmount_Paycov.value +
				                  '&sRASA_routine='    + hddRASA_routine.value    
                    insDefValues('CalSi008', lstrQString, '/VTimeNet/Claim/PaySeq');

            }
            break;

        case "tcnDepreciaterate": /* + Base de Depreciacion */
			with(self.document.forms[0])
			{
                    lstrQString = 'nClaim=' + hddClaim_TPC.value +  
                                  '&nCase_num=' + self.document.forms[0].hddCase_num_TPC.value + 
                                  '&nDeman_type=' + self.document.forms[0].hddDeman_type_TPC.value +       
				                  '&nModulec=' + nModulec.value + 
				                  '&nGroup_insu='    + nGroup_insu.value +
				                  '&nCover='         + valCover.value +
                                  '&sOrigin=1' +
                                  '&nFra_amount='         + tcnFra_amount.value +                                  
				                  '&nDepreciateamount='       + tcnDepreciateamount.value +
                                  '&nDepreciatebase='       + tcnDepreciatebase.value +
                                  '&nDepreciaterate='       + tcnDepreciaterate.value +
				                  '&nAmountPayedCover='    + tcnAmountPayedCover.value +
				                  '&nAmountPayCover='    + tcnAmount_Paycov.value +
				                  '&sRASA_routine='    + hddRASA_routine.value    
                    insDefValues('CalSi008', lstrQString, '/VTimeNet/Claim/PaySeq');



            }
            break;

		case "cbeDoc_Type":	/*+	Tipo de documento */
			with(self.document.forms[0])
			{
				if(cbeCurrency.value==0)
				{
					tcnExchange.value=VTFormat('0','','','',6);
                    lstrQString = 'FieldControl=cbeCurrency' + '&Value=' + cbeCurrency.value
                    insDefValues('SI008',lstrQString,'/VTimeNet/Claim/PaySeq');
				}
				else
				{
//+ Se calcula el factor de cambio
//+ Se eliminan los datos asociados al pago
					if(mintCurrency!=cbeCurrency.value)
					{
						if(typeof(self.document.forms[0].hddClaim_TPC)!='undefined' &&
						   typeof(self.document.forms[0].hddClaim_TPC.value)!='undefined' ){
						    lstrQString = 'nCurrency=' + cbeCurrency.value + '&dValdate=' + self.document.forms[0].tcdValdate.value +
						                  '&nClaim_TPC='         + self.document.forms[0].hddClaim_TPC.value +
						                  '&nCase_num_TPC='      + self.document.forms[0].hddCase_num_TPC.value +
						                  '&nDeman_type_TPC='    + self.document.forms[0].hddDeman_type_TPC.value +
						                  '&nCover_curr_TPC='    + self.document.forms[0].hddCover_curr_TPC.value +
						                  '&nCover_TPC='         + self.document.forms[0].hddCover_TPC.value +
						                  '&nModulec_TPC='       + self.document.forms[0].nModulec.value +
						                  '&nPay_concep_TPC='    + self.document.forms[0].hddPay_concep_TPC.value +
										  '&nTax_TPC='           + self.document.forms[0].hddTax_TPC.value +
						                  '&nGroup_insu_TPC='    + self.document.forms[0].nGroup_insu.value +
						                  '&nIndAutomatic_TPC='  + self.document.forms[0].nIndAutomatic.value +
						                  '&nPaycov_amount_TPC=' + self.document.forms[0].hddPaycov_amount_tpc.value +
						                  '&nDoc_Type='			 + self.document.forms[0].cbeDoc_Type.value +
						                  '&nCalTaxFix=Yes'
						                  
						           
						}                  
						else
						    lstrQString = 'nCurrency='		+ cbeCurrency.value + 
										  '&dValdate='		+ self.document.forms[0].tcdValdate.value + 
										  '&nDoc_Type='		+ Field.value +
										  '&nCalTaxFix=Yes';
						                  	
                    lstrQString = 'FieldControl=cbeDoc_Type' + '&Value=' + cbeDoc_Type.value
                    insDefValues('SI008',lstrQString,'/VTimeNet/Claim/PaySeq');

						/*insDefValues('Currency',lstrQString ,'/VTimeNet/Claim/PaySeq');*/
					}
				}	
			}
			break;
      /*  case "cbeAgency":
              with(self.document.forms[0]){
              lstrQString =  'nAgency=' + cbeAgency.value +
                             '&nOfficeAgen=' + self.document.forms[0].cbeOfficeAgen.value +
                             '&nOffice='+ self.document.forms[0].cbeOffice.value   +
                             '&sClient='+ self.document.forms[0].valClient.value
                             
              insDefValues('Agency',lstrQString ,'/VTimeNet/Claim/PaySeq');
              }
            break;*/
	    case "cbeDeductible_Met":
	        if (Field.value == '4') // Cruzado
	        {
	            lstrQString = "sApplyDDR=1&nClaim=" + <%=Session("nClaim") %> + "&nCase_Num=" + <%=Session("nCase_num") %> + "&nDeman_type=" + <%=Session("nDeman_type") %> ;
	        }
	        else
	        {
	            lstrQString = "sApplyDDR=2&nClaim=" + <%=Session("nClaim") %> + "&nCase_Num=" + <%=Session("nCase_num") %> + "&nDeman_type=" + <%=Session("nDeman_type") %> ;
	        }
	        lstrQString += '&FieldControl='+ Field.name + '&Value=' + Field.value
	        insDefValues('insApply_DDR', lstrQString, '/VTimeNet/Claim/PaySeq');

	        break;
		default:	//+	Todos los demas
			lstrQString = 'FieldControl='+ Field.name + '&Value=' + Field.value
            insDefValues('SI008',lstrQString,'/VTimeNet/Claim/PaySeq');
			break;
	}
}
    </script>
    <meta name="GENERATOR" content="Microsoft Visual Studio	6.0">
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js">	</script>
    <script type="text/javascript" src="/VTimeNet/Scripts/Claim.js"></script>
    <%
        With Response
            .Write(mobjValues.StyleSheet())
            .Write("<script>var	nMainAction	= 0" & Request.QueryString("nMainAction") & "</script>")
            If Request.QueryString("Type") <> "PopUp" Then
                .Write(mobjMenu.setZone(2, "SI008", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
                'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMenu = Nothing
            End If
            .Write(mobjValues.WindowsTitle("SI008", Request.QueryString("sWindowDescript")))
        End With
    %>
</head>
<body onunload="closeWindows();" bgcolor="56">
    <form method="POST" id="FORM" name="frmClaimPayment" action="valPaySeq.aspx?sZone=2&nMainAction=<%=Request.QueryString("nMainAction")%>">
    <%
        Response.Write(mobjValues.ShowWindowsName("SI008", Request.QueryString("sWindowDescript")))
        Call insDefineHeader()
        If Request.QueryString("Type") <> "PopUp" Then
            Call insPreSI008()
        Else
            Call insPreSI008Upd()
        End If
        'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjGrid = Nothing
        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
    %>
    </form>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.42
    Call mobjNetFrameWork.FinishPage("si008")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>

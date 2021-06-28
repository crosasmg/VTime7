<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas    
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

'- Objetos de consulta a tablas
Dim mobjValPolicyTra As ePolicy.ValPolicyTra
Dim mobjClient As eClient.Client
Dim mobjSecurity As eSecurity.SecurScheSurr

'- Variables para manipular campo "Forma de pago"
Dim mblnDisPmtOrd As Boolean
Dim mintDisPmtOrd As Object

Dim mblnDisabled As Boolean
Dim mblnDisabled_SurrAmount As Boolean
Dim mintOperat As Object
Private nUFExchange As Double

'- Se define variable para almacenar QueryString
Dim lstrQueryString As String



'% insDefineHeader:	se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lclsClaim As Object
	Dim lclsCover As Object
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "VI7000"
	
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valOriginColumnCaption"), "valOrigin", "TAB_ORIGIN", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  , Request.QueryString.Item("Type") = "PopUp",  , GetLocalResourceObject("valOriginColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdPaymentDateColumnCaption"), "tcdPaymentDate",  ,  , GetLocalResourceObject("tcdPaymentDateColumnCaption"),  ,  , "InsChangePayDate(this);", mobjSecurity.sModDateP <> "1")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnVpColumnCaption"), "tcnVp", 18,  ,  , GetLocalResourceObject("tcnVpColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAvailBalColumnCaption"), "tcnAvailBal", 18,  ,  , GetLocalResourceObject("tcnAvailBalColumnToolTip"), True, 6,  ,  ,  , True)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnRentabilityColumnCaption"), "tcnRentability", 18, , , GetLocalResourceObject("tcnRentabilityColumnToolTip"), True, 6, , , , True)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnCost_cov_devColumnCaption"), "tcnCost_cov_dev", 18, , , GetLocalResourceObject("tcnCost_cov_devColumnToolTip"), True, 6, , , , True)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmount_rec_devColumnCaption"), "tcnAmount_rec_dev", 18, , , GetLocalResourceObject("tcnAmount_rec_devColumnToolTip"), True, 6, , , , True)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnSurrAmtColumnCaption"), "tcnSurrAmt", 18, , , GetLocalResourceObject("tcnSurrAmtColumnToolTip"), True, 6, , , , True)
		If Request.QueryString.Item("sSurrType") = "1" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnSurrCostColumnCaption"), "tcnSurrCost", 18,  ,  , GetLocalResourceObject("tcnSurrCostColumnToolTip"), True, 6,  ,  ,  , True)
			Call .AddHiddenColumn("tcnWDCost", CStr(0))
		Else
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnWDCostColumnCaption"), "tcnWDCost", 18,  ,  , GetLocalResourceObject("tcnWDCostColumnToolTip"), True, 6,  ,  ,  , True)
			Call .AddHiddenColumn("tcnSurrCost", CStr(0))
		End If
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCost_covColumnCaption"), "tcnCost_cov", 18,  ,  , GetLocalResourceObject("tcnCost_covColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnLoansColumnCaption"), "tcnLoans", 18,  ,  , GetLocalResourceObject("tcnLoansColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIntLoansColumnCaption"), "tcnIntLoans", 18,  ,  , GetLocalResourceObject("tcnIntLoansColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRetentionColumnCaption"), "tcnRetention", 18,  ,  , GetLocalResourceObject("tcnRetentionColumnToolTip"), True, 6,  ,  ,  , True)
		
		If Request.QueryString.Item("sSurrType") = "1" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnRequestedSurrAmtColumnCaption"), "tcnRequestedSurrAmt", 18,  ,  , GetLocalResourceObject("tcnRequestedSurrAmtColumnToolTip"), False, 6,  ,  , "CalCost(insConvertNumber(this.value), true)", True)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnLocalSurrAmtColumnCaption"), "tcnLocalSurrAmt", 12,  ,  , GetLocalResourceObject("tcnLocalSurrAmtColumnToolTip"), True, 0,  ,  , "CalUFRequestedValue(insConvertNumber(this.value))", True)
		Else
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnRequestedSurrAmtColumnCaption"), "tcnRequestedSurrAmt", 18,  ,  , GetLocalResourceObject("tcnRequestedSurrAmtColumnToolTip"), False, 6,  ,  , "CalCost(insConvertNumber(this.value), true)")
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnLocalSurrAmtColumnCaption"), "tcnLocalSurrAmt", 12,  ,  , GetLocalResourceObject("tcnLocalSurrAmtColumnToolTip"), True, 0,  ,  , "CalUFRequestedValue(insConvertNumber(this.value))", False)
		End If
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnUFValueColumnCaption"), "tcnUFValue", 18,  ,  , GetLocalResourceObject("tcnUFValueColumnToolTip"), True, 2,  ,  ,  , True)
		Call .AddHiddenColumn("tcnGrossAmount", CStr(0))
		
		'Call .AddHiddenColumn ("hdnAvailBal", 0)
		
		If CStr(Session("sApv")) = "1" Then
			Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , True)
		End If
		
	End With
	
	'+ Se definen las propiedades generales	del	grid
	With mobjGrid
		.Codispl = "VI7000"
		.Top = 30
		.Left = 30
        .FieldsByRow = 2		
		.Height = 460
		.Width = 600
		.DeleteButton = False
		mobjGrid.sEditRecordParam = "nSurrReas='     + document.forms[0].hddSurrReas.value    + '" & "&nRet_pct='     + document.forms[0].hddnRet_Pct.value    + '" & "&sSurrType='    + document.forms[0].hddSurrType.value    + '" & "&nOffice='      + document.forms[0].hddOffice.value      + '" & "&nOfficeAgen='  + document.forms[0].hddOfficeAgen.value  + '" & "&nAgency='      + document.forms[0].hddAgency.value      + '" & "&sClientBenef=' + document.forms[0].hddClientBenef.value + '" & "&nProponum='    + document.forms[0].hddProponum.value    + '" & "&sClientDest='  + document.forms[0].dtcClient.value      + '"
		.AddButton = False
		
		If Request.QueryString.Item("sSurrType") = "1" Or Request.QueryString.Item("nSurrReas") = CStr(eRemoteDB.Constants.intNull) Or CDbl(Request.QueryString.Item("nSurrReas")) = 0 Or mblnDisabled Then
			.Columns("valOrigin").EditRecord = True
		Else
			.Columns("valOrigin").EditRecord = True
		End If
		.Columns("Sel").GridVisible = True
		.Columns("Sel").Disabled = True
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		'+ Se pasan	los	parámetros al campo	Cuenta Origen
		.Columns("valOrigin").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valOrigin").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valOrigin").Parameters.Add("nCollecDocTyp", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
	End With
	
End Sub

'%insPreVI7000: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreVI7000()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lstrClient As String
	Dim lclsExchange As eGeneral.Exchange
	lclsExchange = New eGeneral.Exchange
	
	mobjValPolicyTra = New ePolicy.ValPolicyTra
	mobjClient = New eClient.Client
	lclsPolicy = New ePolicy.Policy
	
	Dim nTotalRequested As Object
	nTotalRequested = 0
	
	With Request
		Call mobjValPolicyTra.insPreVI7000(.QueryString.Item("sCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nSurrReas"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString.Item("sSurrType"), "VI7000", mobjValues.StringToType(.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble, True))
		
		
	End With
	
	'+Se busca el factor de cambio a la fecha de pago del rescate
	'Call lclsExchange.Find(4, mobjValPolicyTra.dPaymentDate)
	'nUFExchange =  lclsExchange.nExchange
	
	'nUFExchange = mobjValPolicyTra.DefaultValueVI7000("tcnExchange",lclsSurr_origins.dPaymentdate)
	
	'If nUFExchange = 1 Then
	'Response.Write "<NOTSCRIPT>alert('No existe factor de cambio para la moneda de la póliza');</" & "Script>"
	'End if
	
	If lclsPolicy.Find(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		Call mobjClient.Find(lclsPolicy.sClient)
	End If
	
	If mobjValPolicyTra.sClient <> vbNullString Then
		lstrClient = mobjValPolicyTra.sClient
	Else
		lstrClient = Request.QueryString.Item("sClientDest")
	End If
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tctClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""4"">")


Response.Write(mobjValues.ClientControl("tctClient", mobjValPolicyTra.sClient,  , GetLocalResourceObject("tctClientToolTip"),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, Session("nCurrency"),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL></LABEL></TD>" & vbCrLf)
Response.Write("        <TD colspan=2></TD>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL>" & GetLocalResourceObject("cbePmtOrdCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("        <TD>")

	mobjValues.BlankPosition = False
	mobjValues.Parameters.Add("nUserCode", Session("nUserCode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("sLife", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	If Not mblnDisPmtOrd Then
		mintDisPmtOrd = mobjValPolicyTra.DefaultValueVI7000("cbePmtOrd")
	End If
	Response.Write(mobjValues.PossiblesValues("cbePmtOrd", "TABSCHESURRPAYMENT", eFunctions.Values.eValuesType.clngComboType, mintDisPmtOrd, True,  ,  ,  ,  ,  , mblnDisPmtOrd,  , GetLocalResourceObject("cbePmtOrdToolTip")))
Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.AnimatedButtonControl("btnPolicyValues", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnPolicyValuesToolTip"),  , "CallVIC001()", False))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("<BR>")

	
	Dim ldblSurrAmou As Object
	Dim ldblSurrCost As Object
	Dim ldblRetention As Object
	Dim ldblAvailTot As Object
	Dim ldPaymentDate As Object
	
	Dim lclsSurr_origins As Object
	Dim lcolSurr_origins As ePolicy.Surr_originss
	
	lcolSurr_origins = New ePolicy.Surr_originss
	ldblSurrAmou = 0
	ldblSurrCost = 0
	ldblRetention = 0
	
	If lcolSurr_origins.InsPreVI7000_Origins(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nSurrReas"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sSurrType"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("optProcessType"), mobjValues.StringToType(Request.QueryString.Item("nDelete"), eFunctions.Values.eTypeData.etdDouble), mobjSecurity.nTypeResc, mobjValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		Response.Write(mobjValues.HiddenControl("hddIsCancelling", CStr(lcolSurr_origins.bIsCancelling)))
		
		For	Each lclsSurr_origins In lcolSurr_origins
			With mobjGrid
				
				.Columns("Sel").Checked = lclsSurr_origins.sSel_origin
				.Columns("Sel").DefValue = lclsSurr_origins.sSel_origin
				
				.Columns("valOrigin").DefValue = lclsSurr_origins.nOrigin_apv
				.Columns("tcnVp").DefValue = lclsSurr_origins.nGrossAmount
				
				.Columns("tcnAvailBal").DefValue = mobjValues.TypeToString(lclsSurr_origins.nAvailable, eFunctions.Values.eTypeData.etdDouble, True, 6)
				.Columns("tcnSurrCost").DefValue = mobjValues.TypeToString(lclsSurr_origins.nCost_amo, eFunctions.Values.eTypeData.etdDouble, True, 6)
				'.Columns("hdnAvailBal").DefValue = mobjValues.TypeToString(lclsSurr_origins.nAvailable,eFunctions.Values.eTypeData.etdDouble,True,6)
				'+Si el rescate es total, el monto del rescate a retirar de la cuenta, es igual al saldo total 						
				If Request.QueryString.Item("sSurrType") = "1" Then
					.Columns("tcnSurrAmt").DefValue = lclsSurr_origins.nRequestedamount
					ldblSurrAmou = lclsSurr_origins.nRequestedamount
					
					'+si el rescate es total, el monto solicitado en UF es igual al disponible
					.Columns("tcnRequestedSurrAmt").DefValue = mobjValues.TypeToString(lclsSurr_origins.nAvailable, eFunctions.Values.eTypeData.etdDouble, True, 6)
					
					'+si el rescate es total, el monto solicitado en pesos es igual al disponible por factor de cambio
                        'If lclsSurr_origins.nLocal_amount = 0 Then
                        .Columns("tcnLocalSurrAmt").DefValue = mobjValues.TypeToString(lclsSurr_origins.nAvailable * lclsSurr_origins.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6)
                        'Else
                        '   .Columns("tcnLocalSurrAmt").DefValue = mobjValues.TypeToString(lclsSurr_origins.nLocal_amount, eFunctions.Values.eTypeData.etdDouble, True)
                        'End If
				Else
                    '+si el rescate es parcial, el monto solicitado es igual al registrado en la propuesta				
                    .Columns("tcnRequestedSurrAmt").DefValue = mobjValues.TypeToString(lclsSurr_origins.nRequestedAmount, eFunctions.Values.eTypeData.etdDouble, True, 6)
					
                    '+si el rescate es parcial, el monto solicitado en pesos es igual al solicitado por factor de cambio
                    '.Columns("tcnLocalSurrAmt").DefValue = mobjValues.TypeToString(Round(lclsSurr_origins.nRequestedAmount * nUFExchange),eFunctions.Values.eTypeData.etdDouble,True,0)
                    .Columns("tcnLocalSurrAmt").DefValue = mobjValues.TypeToString(lclsSurr_origins.nLocal_amount, eFunctions.Values.eTypeData.etdDouble, True)
					
                    .Columns("tcnSurrAmt").DefValue = mobjValues.TypeToString(lclsSurr_origins.nRequestedAmount + lclsSurr_origins.nCost_amo + lclsSurr_origins.nCost_cov, eFunctions.Values.eTypeData.etdDouble, True, 6)
					
                    ldblSurrAmou = lclsSurr_origins.nRequestedAmount + lclsSurr_origins.nWDCost + lclsSurr_origins.nLoans + lclsSurr_origins.nIntLoans + lclsSurr_origins.nCost_cov
				End If
				
				.Columns("tcnRetention").DefValue = mobjValues.TypeToString(lclsSurr_origins.nRet_amo, eFunctions.Values.eTypeData.etdDouble, True, 6)
				
				.Columns("tcnLoans").DefValue = mobjValues.TypeToString(lclsSurr_origins.nLoans, eFunctions.Values.eTypeData.etdDouble, True, 6)
				.Columns("tcnIntLoans").DefValue = mobjValues.TypeToString(lclsSurr_origins.nIntLoans, eFunctions.Values.eTypeData.etdDouble, True, 6)
				.Columns("tcnWDCost").DefValue = mobjValues.TypeToString(lclsSurr_origins.nCost_amo, eFunctions.Values.eTypeData.etdDouble, True, 6)
				.Columns("tcnCost_cov").DefValue = mobjValues.TypeToString(lclsSurr_origins.nCost_cov, eFunctions.Values.eTypeData.etdDouble, True, 6)
				'.Columns("tcnWDCost").DefValue = mobjValues.TypeToString(lclsSurr_origins.nWDCost,eFunctions.Values.eTypeData.etdDouble,True,6)
				.Columns("tcnGrossAmount").DefValue = lclsSurr_origins.nGrossAmount

                    .Columns("tcnRentability").DefValue = lclsSurr_origins.nRentability
                    .Columns("tcnCost_cov_dev").DefValue = lclsSurr_origins.nCost_cov_dev
                    .Columns("tcnAmount_rec_dev").DefValue = lclsSurr_origins.nAmount_rec_dev
                    
                    '.Columns("tcnUFValue").DefValue = nUFExchange
				'.Columns("tcnUFValue").DefValue = mobjValues.TypeToString(lclsSurr_origins.nExchange,eFunctions.Values.eTypeData.etdLong,True,2)
				
				'If mobjValues.TypeToString(lclsSurr_origins.dPaymentDate,eFunctions.Values.eTypeData.etdDate) = "" Then
				'		.Columns("tcnUFValue").DefValue = nUFExchange
				'	Else
				'		.Columns("tcnUFValue").DefValue = mobjValues.TypeToString(lclsSurr_origins.nExchange,eFunctions.Values.eTypeData.etdLong,True,2)
				'	End If
				If mobjValues.TypeToString(lclsSurr_origins.dPaymentDate, eFunctions.Values.eTypeData.etdDate) = "" Then
					.Columns("tcdPaymentDate").DefValue = CStr(mobjValPolicyTra.dPaymentDate)
					ldPaymentDate = mobjValPolicyTra.dPaymentDate
					nUFExchange = mobjValPolicyTra.nExchange_aux
				Else
					.Columns("tcdPaymentDate").DefValue = lclsSurr_origins.dPaymentDate
					ldPaymentDate = lclsSurr_origins.dPaymentDate
					nUFExchange = lclsSurr_origins.nExchange
				End If
				
				If nUFExchange = 1 Then
					Response.Write("<SCRIPT>alert('No existe factor de cambio para la moneda de la póliza');</" & "Script>")
				End If
				
				.Columns("tcnUFValue").DefValue = CStr(nUFExchange)
				
                    '   nTotalRequested = nTotalRequested + mobjValues.TypeToString(lclsSurr_origins.nRequestedAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) + mobjValues.TypeToString(lclsSurr_origins.nRentability, eFunctions.Values.eTypeData.etdDouble, True, 6) + mobjValues.TypeToString(lclsSurr_origins.nCost_cov_dev, eFunctions.Values.eTypeData.etdDouble, True, 6) + mobjValues.TypeToString(lclsSurr_origins.nAmount_rec_dev, eFunctions.Values.eTypeData.etdDouble, True, 6)
                    nTotalRequested = nTotalRequested + mobjValues.TypeToString(lclsSurr_origins.nRequestedAmount, eFunctions.Values.eTypeData.etdDouble, True, 6)
				If lclsSurr_origins.sSel_origin = "1" Then
					ldblSurrCost = ldblSurrCost + lclsSurr_origins.nCost_amo
					ldblRetention = ldblRetention + lclsSurr_origins.nRet_amo
				End If
				ldblAvailTot = ldblAvailTot + lclsSurr_origins.nAvailable
				.sEditRecordParam = "nSurrReas='     + document.forms[0].hddSurrReas.value       + '" & "&nRet_pct='     + document.forms[0].hddnRet_Pct.value       + '" & "&sSurrType='    + document.forms[0].hddSurrType.value       + '" & "&nOffice='      + document.forms[0].hddOffice.value         + '" & "&nOfficeAgen='  + document.forms[0].hddOfficeAgen.value     + '" & "&nAgency='      + document.forms[0].hddAgency.value         + '" & "&sClientBenef=' + document.forms[0].hddClientBenef.value    + '" & "&nProponum='    + document.forms[0].hddProponum.value       + '" & "&sClientDest='  + document.forms[0].dtcClient.value         + '" & "&nPolicyDuration=" & mobjValPolicyTra.nPolicyDuration & "&nWDCount=" & mobjValPolicyTra.nWDCount & "' + '"
				Response.Write(.DoRow)
			End With
		Next lclsSurr_origins
	End If
	Response.Write(mobjGrid.closeTable())
	lcolSurr_origins = Nothing
	
Response.Write("" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD colspan=2 align=right WIDTH=""50%""><LABEL>" & GetLocalResourceObject("tcnTotalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD align=right>")


Response.Write(mobjValues.NumericControl("tcnTotal", 18, CStr(0),  , GetLocalResourceObject("tcnTotalToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD colspan=2 align=right WIDTH=""50%""><LABEL>" & GetLocalResourceObject("tcnTotal_localCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD align=right>")


Response.Write(mobjValues.NumericControl("tcnTotal_local", 18, CStr(0),  , GetLocalResourceObject("tcnTotal_localToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("</TABLE>")

	
	With Response
		.Write(mobjValues.HiddenControl("hddsCertype", Request.QueryString.Item("sCertype")))
		.Write(mobjValues.HiddenControl("hddnBranch", Request.QueryString.Item("nBranch")))
		.Write(mobjValues.HiddenControl("hddnProduct", Request.QueryString.Item("nProduct")))
		.Write(mobjValues.HiddenControl("hddnPolicy", Request.QueryString.Item("nPolicy")))
		.Write(mobjValues.HiddenControl("hddnCertif", Request.QueryString.Item("nCertif")))
		.Write(mobjValues.HiddenControl("hddnCurrency", Request.QueryString.Item("nCurrency")))
		.Write(mobjValues.HiddenControl("hdddEffecdate", Session("dEffecdate")))
		.Write(mobjValues.HiddenControl("hddnCoverCost", mobjValPolicyTra.DefaultValueVI7000("tcnCoverCost")))
		.Write(mobjValues.HiddenControl("hddnSurrCost", mobjValPolicyTra.DefaultValueVI7000("tcnSurrCost")))
		.Write(mobjValues.HiddenControl("hddnAvailTot", ldblAvailTot))
		.Write(mobjValues.HiddenControl("hddnSurrAmou", ldblSurrAmou))
		.Write(mobjValues.HiddenControl("hddnRet_Pct", mobjValPolicyTra.DefaultValueVI7000("hddnRet_Pct")))
		.Write(mobjValues.HiddenControl("hddnTotSurrCost", ldblSurrCost))
		.Write(mobjValues.HiddenControl("hddnTotRetention", ldblRetention))
		.Write(mobjValues.HiddenControl("hddProcess", Request.QueryString.Item("sProcess")))
		.Write(mobjValues.HiddenControl("hddProponum", Request.QueryString.Item("nProponum")))
		.Write(mobjValues.HiddenControl("hddOffice", Request.QueryString.Item("nOffice")))
		.Write(mobjValues.HiddenControl("hddOfficeAgen", Request.QueryString.Item("nOfficeAgen")))
		.Write(mobjValues.HiddenControl("hddAgency", Request.QueryString.Item("nAgency")))
		.Write(mobjValues.HiddenControl("hddClientBenef", Request.QueryString.Item("sClientBenef")))
		.Write(mobjValues.HiddenControl("hddClientCode", mobjClient.sClient))
		.Write(mobjValues.HiddenControl("hddBirthDate", CStr(mobjClient.dBirthdat)))
		.Write(mobjValues.HiddenControl("hddProfit", mobjValPolicyTra.DefaultValueVI7000("hddProfit")))
		.Write(mobjValues.HiddenControl("hddSurrReas", Request.QueryString.Item("nSurrReas")))
		.Write(mobjValues.HiddenControl("hddSurrType", Request.QueryString.Item("sSurrType")))
		.Write(mobjValues.HiddenControl("hddInd_Insur", Request.QueryString.Item("sInd_Insur")))
		.Write(mobjValues.HiddenControl("hddTotalRequested", nTotalRequested))
		.Write(mobjValues.HiddenControl("hddLocalTotalRequested", CStr(System.Math.Round(nUFExchange * nTotalRequested))))
		.Write(mobjValues.HiddenControl("dtcClient", lstrClient))
		.Write(mobjValues.HiddenControl("dtcClient_Digit", "0"))
		.Write(mobjValues.HiddenControl("btndtcClient", "0"))
		.Write(mobjValues.HiddenControl("chkWorker", "0"))
		.Write(mobjValues.HiddenControl("dtcRetirement", CStr(Today)))
		.Write(mobjValues.HiddenControl("hddsProcessType", Request.QueryString.Item("sProcess")))
		.Write(mobjValues.HiddenControl("hdddPaymentDate", ldPaymentDate))
		
	End With
	lclsPolicy = Nothing
End Sub

'%insPreVI7000Upd: Esta función se encarga de cargar los datos del Grid 
'--------------------------------------------------------------------------------------------
Private Sub insPreVI7000Upd()
	'--------------------------------------------------------------------------------------------
	With Response
		.Write(mobjValues.HiddenControl("hddSurrReas", Request.QueryString.Item("nSurrReas")))
		.Write(mobjValues.HiddenControl("hddnRet_Pct", Request.QueryString.Item("nRet_pct")))
		.Write(mobjValues.HiddenControl("hddSurrType", Request.QueryString.Item("sSurrType")))
		.Write(mobjValues.HiddenControl("hdddEffecdate", Session("dEffecdate")))
		.Write(mobjValues.HiddenControl("hddOffice", Request.QueryString.Item("nOffice")))
		.Write(mobjValues.HiddenControl("hddOfficeAgen", Request.QueryString.Item("nOfficeAgen")))
		.Write(mobjValues.HiddenControl("hddAgency", Request.QueryString.Item("nAgency")))
		.Write(mobjValues.HiddenControl("hddClientBenef", Request.QueryString.Item("sClientBenef")))
		.Write(mobjValues.HiddenControl("hddProponum", Request.QueryString.Item("nProponum")))
		.Write(mobjValues.HiddenControl("hddClientDest", Request.QueryString.Item("sClientDest")))
		.Write(mobjValues.HiddenControl("hddInd_Insur", Request.QueryString.Item("sInd_Insur")))
		.Write(mobjValues.HiddenControl("hddPaymentDate", Request.QueryString.Item("tcdPaymentDate")))
		.Write(mobjValues.HiddenControl("hddUFValue", Request.QueryString.Item("tcnUFValue")))
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valPolicyTra.aspx", "VI7000", Request.QueryString.Item("nMainAction"), Session("bQuery"), CShort(Request.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%
Response.Expires = -1441

mobjSecurity = New eSecurity.SecurScheSurr
mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values

Call mobjSecurity.Find(Session("sSche_Code"), False)

If IsNothing(Request.QueryString.Item("nOperat")) Then
	mintOperat = 0
Else
	mintOperat = Request.QueryString.Item("nOperat")
End If

mblnDisabled = Request.QueryString.Item("sCodisplOri") = "CA767"

If CStr(Session("sSurrType")) = "1" Or mblnDisabled Then
	mblnDisabled_SurrAmount = True
Else
	mblnDisabled_SurrAmount = False
End If

mobjValues.sCodisplPage = "VI7000"

If Session("nSurrReas") = 5 Then
	mblnDisPmtOrd = True
	mintDisPmtOrd = 2
Else
	mblnDisPmtOrd = False
	mintDisPmtOrd = 1
End If
lstrQueryString = "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&nCurrency=" & Request.QueryString.Item("nCurrency")

%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 17/02/06 18:37 $|$$Author: MVazquez $"

//% CalUFRequestedValue: Calcula a partir del monto solicitado, su equivalente en UF
//-------------------------------------------------------------------------------------------
function CalUFRequestedValue(nRequestedValue){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		tcnRequestedSurrAmt.value = VTFormat(nRequestedValue / insConvertNumber(tcnUFValue.value),'', '', '', 6, true);  
		CalCost(insConvertNumber(tcnRequestedSurrAmt.value), false);
		//tcnAvailBal.value = insConvertNumber(tcnVp.value) - insConvertNumber(tcnRequestedSurrAmt.value);
    }   

}

//% CalCost: Calcula el monto total del rescate
//-------------------------------------------------------------------------------------------
function CalCost(nRequestedAmount, bCalcLocal){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
	    tcnSurrAmt.value =  VTFormat(nRequestedAmount+ insConvertNumber(tcnSurrCost.value) + insConvertNumber(tcnLoans.value) + insConvertNumber(tcnCost_cov.value) + insConvertNumber(tcnIntLoans.value) ,'', '', '', 6,true);
		//tcnAvailBal.value = VTFormat(insConvertNumber(tcnVp.value) - insConvertNumber(tcnSurrAmt.value),'', '', '', 6,true);	    
		//hdnAvailBal.value = insConvertNumber(tcnSurrAmt.value) + insConvertNumber(tcnAvailBal.value)   
    if (bCalcLocal)
		{
        tcnLocalSurrAmt.value = VTFormat(nRequestedAmount * insConvertNumber(tcnUFValue.value),'', '', '', 0, true);
        }        
	}
}

//-------------------------------------------------------------------------------------------
function SetEvents()
//-------------------------------------------------------------------------------------------
	{
        with (document.forms[0])
	    {    
		    $(tcnRequestedSurrAmt).change( 
	              function ______replaceJ(){
	                          with (document.forms[0])
	                          {
	                              if (tcnRequestedSurrAmt.sOldValue != tcnRequestedSurrAmt.value)
	                              {
	                                  if(ValNumber(tcnRequestedSurrAmt,".",",","false",6))
	                                  {
	                                      CalCost(insConvertNumber(tcnRequestedSurrAmt.value),  true);
	                                      tcnRequestedSurrAmt.sOldValue = tcnRequestedSurrAmt.value;
	                                  }                          
	                              }
	                          }
	                          
	                      });


		    $(tcnLocalSurrAmt).change( 
	              function ______replaceJ(){
	                          with (document.forms[0])
	                          {

	                              if (tcnLocalSurrAmt.sOldValue != tcnLocalSurrAmt.value)
	                              {
	                                  if(ValNumber(tcnLocalSurrAmt,".",",","false",1))
	                                  {
	                                      CalUFRequestedValue(insConvertNumber(this.value));
	                                  }                          
	                              }
	                          }
	                          
	                      });
		
            tcnLocalSurrAmt.onfocus 
	           =  function ______replaceJ(){
	                  tcnLocalSurrAmt.sOldValue = tcnLocalSurrAmt.value;
	              };

            tcnRequestedSurrAmt.onfocus 
	           =  function ______replaceJ(){
	                  tcnRequestedSurrAmt.sOldValue = tcnRequestedSurrAmt.value;
	              };				  
		}					  
    }



//% CallVIC001: Despliega la ventana de datos particulares.
//-------------------------------------------------------------------------------------------
function CallVIC001(){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		ShowPopUp('/VTimeNet/Common/VIC001_K.aspx?sCertype=2&nBranch=' + hddnBranch.value + 
		          '&nProduct=' + hddnProduct.value + '&nPolicy=' + hddnPolicy.value +
		          '&nCertif=' + '&dEffectDate=' + hdddEffecdate.value,'VIC001_K', 500, 400)
	}
}

//% insCalSurrCurr: Calcula el monto según el factor de cambio
//-------------------------------------------------------------------------------------------
function insCalSurrCurr(){
//-------------------------------------------------------------------------------------------
    var nRetention = 0;
    var nCoverCost = 0;
    var nSurrAmt = 0;
    var nSurrCost = 0;
    var ntotal = 0;
    var frm
    var ldblSurrAmt = 0;
    var ldblAvailBal = 0;
    
	frm = self.document.forms[0] 

//+Costo cobertura sólo aplica para rescate total y razon <> a devolución 	    	
//	if (!frm.hddSurrType.value=='1' || (frm.hddSurrReas.value=='3')){
// 		frm.tcnCoverCost.value=VTFormat('0','', '', '', 6)
//	}
//	else {
//		frm.tcnCoverCost.value = VTFormat(frm.hddnCoverCost.value,'', '', '', 6)
//	}

//+Traspasos o devolución

   <%If Request.QueryString.Item("Type") <> "PopUp" Then%>
	if ((frm.hddSurrReas.value==2) || (frm.hddSurrReas.value==3))  {
		frm.dtcClient.disabled=false
		frm.btndtcClient.disabled=false}
	else {
		frm.dtcClient.value=''
		frm.dtcClient_Digit.value=''
		UpdateDiv("lblCliename","");
		frm.dtcClient.disabled=true
		frm.btndtcClient.disabled=true
		frm.dtcClient_Digit.disabled=true}

    if (frm.hddSurrReas.value==0) 
        frm.tcnTotal.value = VTFormat(0,'', '', '', 6,true);
    else
    {
	    nSurrAmt = frm.hddnSurrAmou.value;
	    nSurrCost = frm.hddnSurrCost.value;
	    nRetention = frm.hddnTotRetention.value; 
	    //nCoverCost = frm.tcnCoverCost.value; 

frm.tcnTotal_local.value = VTFormat(frm.hddLocalTotalRequested.value,'', '', '', 0, true);
frm.tcnTotal.value = frm.hddTotalRequested.value;
//	    frm.tcnTotal.value = VTFormat((insConvertNumber(nSurrAmt)), 
//							   insConvertNumber(nSurrCost) -
//							   insConvertNumber(nRetention) -
//							   insConvertNumber(nCoverCost)),
//	                                  '', '', '', 6,true);
							   
	}

   <%End If%>
	
}

//% insCalSurrCurr: Calcula el monto según el factor de cambio
//-------------------------------------------------------------------------------------------
function insCalRetention(){
//-------------------------------------------------------------------------------------------
    var nRetention = 0;
    var frm
    
	frm = self.document.forms[0] 

//+Retiro de fondos del sistema
	if (frm.hddSurrReas.value==1) {
//+Si es rescate parcial, se calcula retencion según monto a rescatar
		frm.tcnRetention.value = VTFormat(((insConvertNumber(frm.hddnRet_Pct.value)/100)* 
							     (insConvertNumber(frm.tcnSurrAmt.value))),
								  '', '', '', 6, true);
	}
	else {
		frm.tcnRetention.value=VTFormat('0','', '', '', 6)
	}
}

//%InsShowClientRole: Muestra la información del rol indicado
//-------------------------------------------------------------------------------------------
function InsShowClientRole(){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		if (tcnPolicy.value != hddnPolicy_old.value){
			insDefValues('InsShowClientRole', 'sCertype=2&nBranch=' + cbeBranch.value +
			                                  '&nProduct=' + valProduct.value +
			                                  '&nPolicy=' + tcnPolicy.value +
			                                  '&nCertif=0&nRole=1' +
			                                  '&dEffecdate=' + hdddEffecdate.value +
			                                  '&sCodispl=VI7000&sFrame=fraFolder');
			hddnPolicy_old.value = tcnPolicy.value;
		}
	}
}

//%	insSubmitPage: recarga la página recalculados los datos 
//-------------------------------------------------------------------------------------------
function insSubmitPage(){
//-------------------------------------------------------------------------------------------    
	var lstrLocation = '';
	lstrLocation += document.location.href;		
	lstrLocation = lstrLocation.replace(/&sCertype=.*/,"")	
//	lstrLocation = lstrLocation + "&sClient=" + self.document.forms[0].elements["valClient"].value
//	                            + "&nServ_order=" + self.document.forms[0].elements["valServ_order"].value
//	                            + "&nOffice=" + self.document.forms[0].elements["cbeOffice"].value
//	                            + "&nOfficeAgen=" + self.document.forms[0].elements["cbeOfficeAgen"].value
//	                            + "&nAgency=" + self.document.forms[0].elements["cbeAgency"].value;
    with(self.document.forms[0]){
		lstrLocation = lstrLocation + "&sCertype=" + hddsCertype.value
	                                + "&nBranch=" + hddnBranch.value
                                    + "&nProduct=" + hddnProduct.value
									+ "&nPolicy=" + hddnPolicy.value
                                    + "&nCertif=" + hddnCertif.value
                                    + "&nCurrency=" + hddnCurrency.value
                                    + "&sProcess=" + hddProcess.value
                                    + "&nOffice=" + hddOffice.value
                                    + "&nOfficeAgen=" + hddOfficeAgen.value
                                    + "&nAgency=" + hddAgency.value
                                    + "&sClientBenef=" + hddClientBenef.value
                                    + "&nProponum=" + hddProponum.value
                                    + "&nSurrReas=" + hddSurrReas.value 
                                    + "&sSurrType=" + hddSurrType.value
                                    + "&sClientDest=" + dtcClient.value
                                    + "&sClientDest=" + dtcClient.value;
    }
	document.location.href = lstrLocation;
}
//%InsChangePayDate: Cambia la fecha de valorizacion
//-------------------------------------------------------------------------------------------
function InsChangePayDate(sDate){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
 	    
    tcdPaymentDate.value = sDate.value;
    
    if (hddSurrType.value = '1' )
		insDefValues("InsNexchange",'dPaydate=' + sDate.value + 
									'&nRequestedSurrAmt=' + tcnRequestedSurrAmt.value,'/VTimeNet/policy/policytra');
    else
    		insDefValues("InsNexchange",'dPaydate=' + sDate.value + 
									'&nRequestedSurrAmt=' + tcnSurrAmt.value,'/VTimeNet/policy/policytra');

    }
}
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<HTML>
<HEAD>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VI7000", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();"     <%If Request.QueryString.Item("Type") = "PopUp" Then Response.Write("onload='SetEvents()'")%>> 
<FORM METHOD="POST" ID="FORM" NAME="VI7000" ACTION="valPolicyTra.aspx?x=1<%=lstrQueryString%>">
<%
Response.Write(mobjValues.ShowWindowsName("VI7000", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreVI7000()
Else
	Call insPreVI7000Upd()
End If
%>
<SCRIPT LANGUAGE=javascript>
//<!--
    insCalSurrCurr(); 
//-->
</SCRIPT>

</FORM>
</BODY>
</HTML>
<%
mobjValPolicyTra = Nothing
mobjValues = Nothing
mobjClient = Nothing
mobjGrid = Nothing
%>






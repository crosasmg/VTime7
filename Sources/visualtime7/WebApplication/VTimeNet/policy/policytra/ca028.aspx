<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.19
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del grid de la ventana
    Dim mobjGrid As eFunctions.Grid
    

'- Se define las variables para el manejo del Grid de la ventana
Dim mclsTDetail_pre As ePolicy.TDetail_pre

'- Se define la variable para consultar el tipo de producto de vida
Dim mclsProduct_li As eProduct.Product

'- Se define variable para almacenar QueryString
Dim lstrQueryString As String

Dim lstrCertype As Object
Dim lstrBranch As Object
Dim lstrProduct As Object
Dim lstrPolicy As Object
Dim lstrCertif As Object
'	Dim lstrNullDate    
'	Dim lstrNullReceipt 
Dim lstrExeMode As String
Dim lstrExeReport As String
'Dim lstrAgency      
Dim lstrCodisplOrig As String
Dim lstrCodispl As String
Dim lstrOnSeq As String
Dim lstrNewData As String
Dim lstrPolitype As String
Dim lstrExist As String
Dim ldblCapital As Object
Dim ldtmPolStartDate As Object
Dim ldblCerPremium As Object
Dim ldtmPolExpirdat As Object
Dim lstrClient As String
Dim ldtmEffecdate As Object
Dim ldtmEffecdateIni As Object
Dim lstrTypeReceipt As Object
Dim ldtmExpirReceipt As Object
Dim llngReceipt As Object
Dim lstrOrigReceipt As String
Dim llngCurrency As Object
Dim llngTratypei As Object
Dim ldtmIssuedat As Object
Dim lstrKey As String
Dim lstrAdjust As String
Dim lstrAdjReceipt As String
Dim lstrAdjAmount As String
Dim lstrTypePay As String
Dim llngPayfreq As Object
Dim ldblPremiumOri As String
Dim ldblBalanceOri As String
Dim llngProdClas As Object

'+ Vriable para ser usadas si la ventana se encuentra dentro de la secuencia
Dim mblnError As Boolean
Dim mblnSequence As Boolean

Dim mclsPolicy_his As Object


'%insLoadParameterQS: Valores recuperados tras recargar la ventana
'--------------------------------------------------------------------------------------------
Private Sub insLoadParameterQS()
	'--------------------------------------------------------------------------------------------	
	lstrCertype = Request.QueryString.Item("sCertype")
	If lstrCertype = "" Then lstrCertype = Session("sCertype")
	lstrBranch = Request.QueryString.Item("nBranch")
	If lstrBranch = "" Then lstrBranch = Session("nBranch")
	lstrProduct = Request.QueryString.Item("nProduct")
	If lstrProduct = "" Then lstrProduct = Session("nProduct")
	lstrPolicy = Request.QueryString.Item("nPolicy")
	If lstrPolicy = "" Then lstrPolicy = Session("nPolicy")
	lstrCertif = Request.QueryString.Item("nCertif")
        If lstrCertif = "" Then lstrCertif = Session("nCertif")
        If lstrCertif = "" Then lstrCertif = 0
	ldblCapital = Request.QueryString.Item("nCapitalPol")
	ldtmPolStartDate = Request.QueryString.Item("dStartPolicy")
	ldtmPolExpirdat = Request.QueryString.Item("dExpirPolicy")
	ldblCerPremium = Request.QueryString.Item("nPremiumCer")
	lstrClient = Request.QueryString.Item("sClient")
	'   lstrNullDate    = Request.QueryString("dNullDate")
	'	lstrNullReceipt = Request.QueryString("sNullReceipt")
	lstrTypeReceipt = Request.QueryString.Item("sTypeReceipt")
	lstrOrigReceipt = Request.QueryString.Item("sOrigReceipt")
	lstrExeMode = Request.QueryString.Item("nExeMode")
	lstrExeReport = Request.QueryString.Item("sExeReport")
	'	lstrAgency      = Request.QueryString("nAgency") 
	llngTratypei = mobjValues.StringToType(Request.QueryString.Item("nTratypei"), eFunctions.Values.eTypeData.etdLong)
	llngCurrency = mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdLong)
	lstrCodisplOrig = Request.QueryString.Item("sCodisplOrig")
	lstrOnSeq = Request.QueryString.Item("sOnSeq")
	lstrCodispl = Request.QueryString.Item("sCodispl")
	lstrNewData = Request.QueryString.Item("sNewData")
	lstrKey = Request.QueryString.Item("sKey")
	ldtmEffecdate = mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
	ldtmEffecdateIni = mobjValues.StringToType(Request.QueryString.Item("dEffecdateIni"), eFunctions.Values.eTypeData.etdDate)
	ldtmExpirReceipt = mobjValues.StringToType(Request.QueryString.Item("dExpirdate"), eFunctions.Values.eTypeData.etdDate)
	lstrTypePay = Request.QueryString.Item("nTypePay")
	lstrAdjust = "1" '---Request.QueryString("sAdjust")
	lstrAdjReceipt = Request.QueryString.Item("nAdjReceipt")
	lstrAdjAmount = Request.QueryString.Item("nAdjAmount")
	ldtmIssuedat = Today
	ldblPremiumOri = mobjValues.StringToType(Request.QueryString.Item("nPremiumOri"), eFunctions.Values.eTypeData.etdDouble)
	ldblBalanceOri = mobjValues.StringToType(Request.QueryString.Item("nBalanceOri"), eFunctions.Values.eTypeData.etdDouble)
	
End Sub

'%insLoadParameterBD: Valores obtenidos de la BD al carga por primera vez 
'--------------------------------------------------------------------------------------------
Private Sub insLoadParameterBD()
	'--------------------------------------------------------------------------------------------	
	lstrPolitype = mclsTDetail_pre.mclsPolicy.sPolitype
	lstrExist = mclsTDetail_pre.sExist
	ldblCapital = mclsTDetail_pre.mclsPolicy.nCapital
	ldtmPolStartDate = mclsTDetail_pre.mclsCertificat.dStartdate
	ldblCerPremium = mclsTDetail_pre.mclsCertificat.nPremium
	ldtmPolExpirdat = mclsTDetail_pre.mclsCertificat.dExpirdat
	lstrClient = mclsTDetail_pre.mclsCertificat.sClient
	lstrTypeReceipt = mclsTDetail_pre.nTypeReceipt
	ldtmEffecdate = mclsTDetail_pre.mclsCertificat.dNextReceip
	ldtmEffecdateIni = ldtmEffecdate
	
	'+Se calcula fecha de termino del recibo. Se pasa nulo
	Call mclsTDetail_pre.mclsCertificat.insCalcPeriodDates(ldtmEffecdate, mclsTDetail_pre.mclsCertificat.nPayfreq, mclsTDetail_pre.mclsCertificat.sFracreceip, "", mclsTDetail_pre.mclsCertificat.dExpirdat)
	ldtmExpirReceipt = mclsTDetail_pre.mclsCertificat.dEndCurrentPeriod
	
	llngReceipt = mclsTDetail_pre.nReceipt
	llngCurrency = mclsTDetail_pre.nCurrency
	'llngTratypei     = mclsTDetail_pre.nTratypei
	ldtmIssuedat = mclsTDetail_pre.dIssuedat
	lstrKey = mclsTDetail_pre.mcolTDetail_pre.sKey(Session("nUsercode"), Session("SessionID"))
	
End Sub

'% insDefineGrid: se definen las características del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineGrid()
	'--------------------------------------------------------------------------------------------	
	'+ Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddClientColumn(0, GetLocalResourceObject("dtcClientColumnCaption"), "dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientColumnToolTip"),  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCodeItemColumnCaption"), "tcnCodeItem", 5, vbNullString,  , GetLocalResourceObject("tcnCodeItemColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeColumnCaption"), "cbeType", "Table298", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypeColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctElementColumnCaption"), "tctElement", 20, vbNullString,  , GetLocalResourceObject("tctElementColumnToolTip"),  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbePrem_detColumnCaption"), "cbePrem_det", "Table5651", eFunctions.Values.eValuesType.clngComboType, CStr(3),  ,  ,  ,  , "changeValuesField(""Prem_det"",this)",  ,  , GetLocalResourceObject("cbePrem_detColumnToolTip"))
		Call .AddAnimatedColumn(0, GetLocalResourceObject("btnPrem_detColumnCaption"), "btnPrem_det", "/VTimeNet/Images/Window_dolarOff.gif", GetLocalResourceObject("btnPrem_detColumnToolTip"),  , "showDetai();", lstrAdjust = "1")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, vbNullString, False, GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumAColumnCaption"), "tcnPremiumA", 18, vbNullString, False, GetLocalResourceObject("tcnPremiumAColumnToolTip"), True, 6,  ,  , "changeValuesField(""Premium"",this)")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumEColumnCaption"), "tcnPremiumE", 18, vbNullString, False, GetLocalResourceObject("tcnPremiumEColumnToolTip"), True, 6,  ,  , "changeValuesField(""Premium"",this)")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommi_rateColumnCaption"), "tcnCommi_rate", 4, vbNullString,  , GetLocalResourceObject("tcnCommi_rateColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommissionColumnCaption"), "tcnCommission", 18, vbNullString,  , GetLocalResourceObject("tcnCommissionColumnToolTip"), True, 6)
		Call .AddHiddenColumn("hddPremium", vbNullString)
		Call .AddHiddenColumn("hddAddTax", vbNullString)
		Call .AddHiddenColumn("hddBill_item", CStr(0))
		Call .AddHiddenColumn("hddBranch_est", CStr(0))
		Call .AddHiddenColumn("hddBranch_led", CStr(0))
		Call .AddHiddenColumn("hddBranch_rei", CStr(0))
		Call .AddHiddenColumn("hddModulec", CStr(0))
		Call .AddHiddenColumn("hddAddsuini", vbNullString)
		Call .AddHiddenColumn("hddCacalili", vbNullString)
		Call .AddHiddenColumn("hddCommissi_i", vbNullString)
		Call .AddHiddenColumn("hddId_Bill", vbNullString)
		Call .AddHiddenColumn("hddPrem_det_proc", "2")
		Call .AddHiddenColumn("hddPrem_det_old", vbNullString)
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Width = 400
		.Height = 460
		.Top = 50
		.ActionQuery = lstrAdjust = "1"
		.Columns("cbePrem_det").BlankPosition = False
		.Columns("Sel").OnClick = "insSelected(this)"
		.DeleteButton = False
		.AddButton = False
		.DeleteScriptName = vbNullString
		.MoveRecordScript = "changeValuesField(""InitialPopUp"");"
		.sEditRecordParam = "' + getReloadParams() + '"
		Call .Splits_Renamed.AddSplit(0, vbNullString, 6)
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("5ColumnCaption"), 5)
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'%insPreCA028: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreCA028()
	'--------------------------------------------------------------------------------------------
	Dim lclsTDetail_pre As ePolicy.TDetail_pre
	Dim lintCount As Double
	Dim lintIndex As Short
	Dim lstrType_detai As Object
	Dim lintCodeItem As Integer
	Dim ldblAmount As Object
	
	mblnSequence = False
	
	'+ Si se invoca desde la secuencia de Cartera
	If (Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngTempPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngTempCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotAmendent Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifQuotAmendent Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyPropAmendent Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifPropAmendent) And lstrOnSeq = "1" Then
		mblnSequence = True
	End If
	
	If lstrNewData <> "1" Then
		Call mclsTDetail_pre.insPreCA028(lstrCertype, lstrBranch, lstrProduct, lstrPolicy, lstrCertif, ldtmEffecdate, ldtmEffecdate, ldtmExpirReceipt, mobjValues.StringToType(lstrTypeReceipt, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), llngCurrency, ldtmIssuedat, llngTratypei, lstrOrigReceipt, mblnSequence)
		
		If mclsProduct_li.FindProduct_li(lstrBranch, lstrProduct, Today) Then
			llngProdClas = mclsProduct_li.nProdClas
		Else
			llngProdClas = 0
		End If
		
		insLoadParameterBD()
	End If
	
	mblnError = mclsTDetail_pre.bError
	'Response.Write mobjValues.HiddenControl("hddProvince",mclsTDetail_pre.mclsPolicy.nProvince)
	If lstrPolitype <> vbNullString Then
		Session("sPoliType") = lstrPolitype
	End If
	
	Response.Write(mobjValues.HiddenControl("cbeBranch", lstrBranch))
	Response.Write(mobjValues.HiddenControl("valProduct", lstrProduct))
	Response.Write(mobjValues.HiddenControl("tcnPolicy", lstrPolicy))
	Response.Write(mobjValues.HiddenControl("tcnCertif", lstrCertif))
	Response.Write(mobjValues.HiddenControl("cbeBranchPay", lstrBranch))
	Response.Write(mobjValues.HiddenControl("valProductPay", lstrProduct))
	Response.Write(mobjValues.HiddenControl("tcnPolicyPay", lstrPolicy))
	Response.Write(mobjValues.HiddenControl("tcnCertifPay", lstrCertif))
	Response.Write(mobjValues.HiddenControl("hddAdjAmount", lstrAdjAmount))
	Response.Write(mobjValues.HiddenControl("hddEffecdateIni", ldtmEffecdateIni))
	
	'If Not mblnSequence Then
	
Response.Write("    " & vbCrLf)
Response.Write("    <p ALIGN=""Center"">" & vbCrLf)
Response.Write("    <label ID=""41097""><a HREF=""ca028.aspx#" & GetLocalResourceObject("Anchorca028.aspxDatos del reciboCaption") & """> " & GetLocalResourceObject("Anchorca028.aspxDatos del reciboCaption") & "</a></label>" & vbCrLf)
Response.Write("    <table WIDTH=""100%"">" & vbCrLf)
Response.Write("        <tr>" & vbCrLf)
Response.Write("            <td COLSPAN=""5"" CLASS=""HighLighted""><label ID=""41098"">" & GetLocalResourceObject("AnchorCaption") & "</label></td>" & vbCrLf)
Response.Write("        </tr>" & vbCrLf)
Response.Write("        <tr>" & vbCrLf)
Response.Write("            <td CLASS=""HorLine"" COLSPAN=""5""></td>" & vbCrLf)
Response.Write("        </tr>" & vbCrLf)
Response.Write("        <tr>" & vbCrLf)
Response.Write("			<td COLSPAN=""2"" CLASS=""HighLighted""><label ID=""0"">" & GetLocalResourceObject("Anchor2Caption") & "</label></td>" & vbCrLf)
Response.Write("			<td WIDTH=""5%"">&nbsp;</td>" & vbCrLf)
Response.Write("			<td><label ID=""13765"">" & GetLocalResourceObject("tcnCapital_policyCaption") & "</label></td>" & vbCrLf)
Response.Write("            <td>")


Response.Write(mobjValues.NumericControl("tcnCapital_policy", 18, ldblCapital,  , GetLocalResourceObject("tcnCapital_policyToolTip"), True, 6, True))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("        <tr>" & vbCrLf)
Response.Write("            <td CLASS=""HorLine"" COLSPAN=""2""></td>" & vbCrLf)
Response.Write("            <td COLSPAN=""3""></td>" & vbCrLf)
Response.Write("        </tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("            <td WIDTH=""15%""><label ID=""0"">" & GetLocalResourceObject("tcdStartDate_policyCaption") & "</label></td>" & vbCrLf)
Response.Write("            <td>")


Response.Write(mobjValues.DateControl("tcdStartDate_policy", ldtmPolStartDate,  , GetLocalResourceObject("tcdStartDate_policyToolTip"), True))


Response.Write("</td>" & vbCrLf)
Response.Write("            <td>&nbsp;</td>" & vbCrLf)
Response.Write("			<td><label ID=""13770"">" & GetLocalResourceObject("tcnNetPremium_policyCaption") & "</label></td>" & vbCrLf)
Response.Write("            <td>")


Response.Write(mobjValues.NumericControl("tcnNetPremium_policy", 18, ldblCerPremium,  , GetLocalResourceObject("tcnNetPremium_policyToolTip"), True, 6, True))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("		    <td><label ID=""0"">" & GetLocalResourceObject("tcdExpirdate_policyCaption") & "</label></td>" & vbCrLf)
Response.Write("            <td>")


Response.Write(mobjValues.DateControl("tcdExpirdate_policy", ldtmPolExpirdat,  , GetLocalResourceObject("tcdExpirdate_policyToolTip"), True))


Response.Write("</td>" & vbCrLf)
Response.Write("            <td>&nbsp;</td>" & vbCrLf)
Response.Write("            <td><label ID=""41099"">" & GetLocalResourceObject("dtcClient_policyCaption") & "</label></td>" & vbCrLf)
Response.Write("            <td>")


Response.Write(mobjValues.ClientControl("dtcClient_policy", lstrClient,  , GetLocalResourceObject("dtcClient_policyToolTip"),  ,  , "lblCliename", True, True,  ,  ,  ,  ,  , True))


Response.Write("</td>" & vbCrLf)
Response.Write("        </tr>" & vbCrLf)
Response.Write("    </table>" & vbCrLf)
Response.Write("")

	
	'+Como los campos anteriores solo se crean como etiquetas, se almacenan sus valores
	'+para poder usarlos cuando se recarge la ventana
	Response.Write(mobjValues.HiddenControl("tcnCapital_policy", ldblCapital))
	Response.Write(mobjValues.HiddenControl("tcdStartDate_policy", ldtmPolStartDate))
	Response.Write(mobjValues.HiddenControl("tcnNetPremium_policy", ldblCerPremium))
	Response.Write(mobjValues.HiddenControl("tcdExpirdate_policy", ldtmPolExpirdat))
	Response.Write(mobjValues.HiddenControl("dtcClient_policy", lstrClient))
	
	'End If 
	
Response.Write("" & vbCrLf)
Response.Write("	<table WIDTH=""100%"" border=""0"">" & vbCrLf)
Response.Write("        <tr>" & vbCrLf)
Response.Write("            <td COLSPAN=""5"" CLASS=""HighLighted""><label ID=""41098"">" & GetLocalResourceObject("Anchor3Caption") & "</label></td>" & vbCrLf)
Response.Write("        </tr>" & vbCrLf)
Response.Write("        <tr>" & vbCrLf)
Response.Write("            <td CLASS=""HorLine"" COLSPAN=""5""></td>" & vbCrLf)
Response.Write("        </tr>" & vbCrLf)
Response.Write("        <tr>" & vbCrLf)
Response.Write("			<td COLSPAN=""2"" CLASS=""HighLighted""><label ID=""0"">" & GetLocalResourceObject("Anchor2Caption") & "</label></td>" & vbCrLf)
Response.Write("			<td>&nbsp;</td>" & vbCrLf)
Response.Write("			<td COLSPAN=""2"" CLASS=""HighLighted""><label ID=""0"">" & GetLocalResourceObject("Anchor5Caption") & "</label></td>" & vbCrLf)
Response.Write("        </tr>                " & vbCrLf)
Response.Write("        <tr>" & vbCrLf)
Response.Write("			<td COLSPAN=""2"" CLASS=""HorLine""></td>" & vbCrLf)
Response.Write("			<td></td>" & vbCrLf)
Response.Write("			<td COLSPAN=""2"" CLASS=""HorLine""></td>" & vbCrLf)
Response.Write("        </tr>                " & vbCrLf)
Response.Write("        <tr>" & vbCrLf)
Response.Write("			<td><label ID=""13755"">" & GetLocalResourceObject("tcdStartDateRCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.DateControl("tcdStartDateR", ldtmEffecdateIni,  , GetLocalResourceObject("tcdStartDateRToolTip"),  ,  ,  , "changeValuesField(""StartDateR"", this)", lstrAdjReceipt <> "", 1))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td>&nbsp;</td>" & vbCrLf)
Response.Write("			<td COLSPAN=""1"">")


Response.Write(mobjValues.OptionControl(41101, "optType", GetLocalResourceObject("optType_1Caption"), lstrTypeReceipt, "1", "changeValuesField(""CheckType"", this)",  , 3, GetLocalResourceObject("optType_1ToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td COLSPAN=""1"">")


Response.Write(mobjValues.OptionControl(41102, "optType", GetLocalResourceObject("optType_2Caption"), lstrTypeReceipt - 1, "2", "changeValuesField(""CheckType"", this)",  , 4, GetLocalResourceObject("optType_2ToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("            <td><label ID=""13746"">" & GetLocalResourceObject("tcdExpirDateRCaption") & "</label></td>" & vbCrLf)
Response.Write("            <td>")


Response.Write(mobjValues.DateControl("tcdExpirDateR", ldtmExpirReceipt,  , GetLocalResourceObject("tcdExpirDateRToolTip"),  ,  ,  ,  , lstrAdjReceipt <> "", 2))


Response.Write("</td>" & vbCrLf)
Response.Write("            <!--<td>&nbsp;</td>-->" & vbCrLf)
Response.Write("            <!--<td COLSPAN=""2"">&nbsp;</td>-->" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("        <tr>" & vbCrLf)
Response.Write("            <td>&nbsp;</td>     ")

	'If Not mblnSequence and      'llngProdClas <> 4 Then
	Response.Write("<TD>" & mobjValues.CheckControl("chkAdjust", GetLocalResourceObject("chkAdjustCaption"), lstrAdjust, "1", "changeValuesField(""CheckAdjust"", this);", True) & "</TD>")
	'Else
	'Response.Write "<TD>&nbsp;</TD>" & vbcrlf
	'End IF
	
	
Response.Write("" & vbCrLf)
Response.Write("<td>&nbsp;</td>")

	'If Not mblnSequence and llngProdClas <> 4 Then 
Response.Write("" & vbCrLf)
Response.Write("    		<td><label ID=""13752"">" & GetLocalResourceObject("tcnAdjReceiptCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>" & vbCrLf)
Response.Write("			    ")

	
	''= mobjvalues.NumericControl("tcnAdjReceipt",10,lstrAdjReceipt,, GetLocalResourceObject("tcnAdjReceiptToolTip"),,0,,,,"changeValuesField(""AdjReceipt"", this)",True,5) 
	mobjValues.BlankPosition = False
	With mobjValues.Parameters
		.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	mobjValues.Parameters.ReturnValue("dEffecdate", True, "Fecha Desde")
	mobjValues.Parameters.ReturnValue("dExpirdat", True, "Fecha Hasta")
	Response.Write(mobjValues.PossiblesValues("cbenreceipt", "tabNreceipt_pag", eFunctions.Values.eValuesType.clngWindowType, lstrAdjReceipt, True,  ,  ,  ,  , "changeValuesField(""AdjReceipt"", this)", False, 10, GetLocalResourceObject("cbenreceiptToolTip"),  , 6))
	
	
Response.Write("" & vbCrLf)
Response.Write("			</td>")

	'Else 
Response.Write("" & vbCrLf)
Response.Write("            <!--<td COLSPAN=""2"">&nbsp;</td>-->")

	'End IF 
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("        </tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label ID=""13752"">" & GetLocalResourceObject("tcnReceiptCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")

	Response.Write(mobjValues.NumericControl("tcnReceipt", 10, llngReceipt,  , GetLocalResourceObject("tcnReceiptToolTip"),  , 0,  ,  ,  , "changeValuesField(""Receipt"", this)", True, 5))
	Response.Write(" ")
	'If mblnSequence Then 
	Response.Write(mobjValues.CheckControl("chkDelReceipt", GetLocalResourceObject("chkDelReceiptCaption"),  , "1",  , mblnError,  , GetLocalResourceObject("chkDelReceiptToolTip")))
	'End If 
	
Response.Write("" & vbCrLf)
Response.Write("			</td>" & vbCrLf)
Response.Write("    		<td>&nbsp;</td>" & vbCrLf)
Response.Write("			<td COLSPAN=""2"" CLASS=""HighLighted""><label ID=""0"">" & GetLocalResourceObject("Anchor6Caption") & "</label></td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("        <tr>" & vbCrLf)
Response.Write("			<td COLSPAN=""2""></td>" & vbCrLf)
Response.Write("			<td></td>" & vbCrLf)
Response.Write("			<td COLSPAN=""2"" CLASS=""HorLine""></td>" & vbCrLf)
Response.Write("        </tr>                " & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("            <td><label ID=""13745"">" & GetLocalResourceObject("cbeCurrencyCaption") & "</label></td>" & vbCrLf)
Response.Write("            <td>")

	
	mobjValues.BlankPosition = False
	With mobjValues.Parameters
		.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("cbeCurrency", "tabCurren_pol", eFunctions.Values.eValuesType.clngComboType, llngCurrency, True,  ,  ,  ,  ,  , mblnError,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 6))
	
Response.Write("" & vbCrLf)
Response.Write("			</td>" & vbCrLf)
Response.Write("    		<td>&nbsp;</td>")

	'If Not mblnSequence And llngProdClas <> 4 Then 
Response.Write("" & vbCrLf)
Response.Write("    		<td><label ID=""13752"">" & GetLocalResourceObject("tcnPremiumOriCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.NumericControl("tcnPremiumOri", 18, ldblPremiumOri,  , GetLocalResourceObject("tcnPremiumOriToolTip"),  , 6,  ,  ,  ,  , True, 5, True))


Response.Write("</td>")

	'Else
Response.Write("" & vbCrLf)
Response.Write("            <!--<td COLSPAN=""2"">&nbsp;</td>-->")

	'End If
Response.Write("" & vbCrLf)
Response.Write("        </tr>                " & vbCrLf)
Response.Write("        </tr>        " & vbCrLf)
Response.Write("            <td><label ID=""13754"">" & GetLocalResourceObject("cbeSourceCaption") & "</label></td>" & vbCrLf)
Response.Write("            <td>")

	
	mobjValues.TypeList = 2
	'If llngProdClas = 4 Then
	llngTratypei = "16"
	'End IF					
	mobjValues.List = "13"
	
	Response.Write(mobjValues.PossiblesValues("cbeSource", "Table24", 1, llngTratypei,  ,  ,  ,  ,  ,  , llngProdClas = 4,  , GetLocalResourceObject("cbeSourceToolTip"),  , 8))
	
Response.Write("" & vbCrLf)
Response.Write("			</td>" & vbCrLf)
Response.Write("			<td>&nbsp;</td>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("			")

	'If Not mblnSequence And 			     'llngProdClas <> 4 Then 
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    			<td><label ID=""13752"">" & GetLocalResourceObject("tcnBalanceOriCaption") & "</label></td>" & vbCrLf)
Response.Write("				<td>")


Response.Write(mobjValues.NumericControl("tcnBalanceOri", 18, ldblBalanceOri,  , GetLocalResourceObject("tcnBalanceOriToolTip"),  , 6,  ,  ,  ,  , True, 5, True))


Response.Write("</td>" & vbCrLf)
Response.Write("			")

	'Else
Response.Write("" & vbCrLf)
Response.Write("				<!--<td COLSPAN=""2"">&nbsp;</td>-->" & vbCrLf)
Response.Write("			")

	'End If
Response.Write("" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("        </tr>" & vbCrLf)
Response.Write("        <tr>" & vbCrLf)
Response.Write("<!--Se oculta campo para evitar confusiones con Recibo a ajustar     En su reemplazo se ubia el campo de ordenes de pago -->" & vbCrLf)
Response.Write("<!--            <TD><LABEL ID=13748>" & GetLocalResourceObject("tctOrigReceiptCaption") & "</LABEL></TD> -->" & vbCrLf)
Response.Write("<!--            <TD>< %= mobjValues.TextControl(""tctOrigReceipt"",20,mclsTDetail_pre.sOrigReceipt,, GetLocalResourceObject(""tctOrigReceiptToolTip""),,,,,mblnError Or mclsTDetail_pre.mclsPolicy.sBussityp = ""1"",9) % > </TD>-->" & vbCrLf)
Response.Write("            <td><label ID=""13850"">" & GetLocalResourceObject("cbePayWayCaption") & "</label></td>" & vbCrLf)
Response.Write("            <td>")


Response.Write(mobjValues.PossiblesValues("cbePayWay", "Table5527", eFunctions.Values.eValuesType.clngComboType, lstrTypePay,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbePayWayToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("            <td>&nbsp;</td>" & vbCrLf)
Response.Write("			")

	'If Not mblnSequence Then 
	ldblAmount = mobjValues.StringToType(lstrAdjAmount, eFunctions.Values.eTypeData.etdDouble)
	If ldblAmount <> eRemoteDB.Constants.intNull Then
		ldblAmount = System.Math.Abs(ldblAmount)
	End If
	
Response.Write("" & vbCrLf)
Response.Write("				")

	'If llngProdClas = 4 Then
Response.Write("" & vbCrLf)
Response.Write("    				<td><label ID=""13752"">" & GetLocalResourceObject("Anchor7Caption") & "</label></td>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    			")

	'Else
Response.Write("" & vbCrLf)
Response.Write("    				<!--<td><label ID=""13752"">" & GetLocalResourceObject("Anchor7Caption") & "</label></td>-->" & vbCrLf)
Response.Write("    			")

	'End If
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("				<td>")


Response.Write(mobjValues.NumericControl("tcnAdjAmount", 18, ldblAmount,  , GetLocalResourceObject("tcnAdjAmountToolTip"),  , 6,  ,  ,  , "changeValuesField(""AdjAmount"", this)", llngProdClas <> 4, 5, True, False))


Response.Write("</td>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("			")

	'Else
Response.Write("" & vbCrLf)
Response.Write("			  <!--<td COLSPAN=""2"">&nbsp;</td>-->" & vbCrLf)
Response.Write("			")

	'End If
Response.Write("" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("        </tr>" & vbCrLf)
Response.Write("        <tr>    " & vbCrLf)
Response.Write("			")

	'If llngProdClas <> 4 Then
Response.Write("" & vbCrLf)
Response.Write("				<td><label>" & GetLocalResourceObject("btnCalDetailCaption") & "</label></td>" & vbCrLf)
Response.Write("				<td>")


Response.Write(mobjValues.AnimatedButtonControl("btnCalDetail", "/VTimeNet/images/btnAcceptOff.png", GetLocalResourceObject("btnCalDetailToolTip"), "", "changeValuesField(""InsDetail"", this);", False))


Response.Write("</td>" & vbCrLf)
Response.Write("            ")

	'Else
Response.Write("" & vbCrLf)
Response.Write("				<td>&nbsp;</td>" & vbCrLf)
Response.Write("            ")

	'End If
Response.Write("" & vbCrLf)
Response.Write("            <!--<td>&nbsp;</td>-->" & vbCrLf)
Response.Write("			<td><label ID=""13768"">" & GetLocalResourceObject("tcdIssueDateCaption") & "</label></td>" & vbCrLf)
Response.Write("            <td>")


Response.Write(mobjValues.DateControl("tcdIssueDate", ldtmIssuedat,  , GetLocalResourceObject("tcdIssueDateToolTip"),  ,  ,  ,  , mblnError, 7))


Response.Write("</td>" & vbCrLf)
Response.Write("        </tr>" & vbCrLf)
Response.Write("	</table>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//+se ejecutan las rutinas de manejo de campos si los valores no son los cargados por defecto" & vbCrLf)
Response.Write("    if(self.document.forms[0].optType[1].checked)" & vbCrLf)
Response.Write("	    changeValuesField('CheckType');" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    if(self.document.forms[0].chkAdjust.checked)" & vbCrLf)
Response.Write("	    changeValuesField('CheckAdjust', self.document.forms[0].chkAdjust);" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//    changeValuesField('AdjReceipt', self.document.forms[0].tcnAdjReceipt);" & vbCrLf)
Response.Write("</" & "SCRIPT>	" & vbCrLf)
Response.Write("	")

	
	Response.Write(mobjValues.HiddenControl("hddClient_policy", lstrClient))
	Response.Write(mobjValues.HiddenControl("hddKey", lstrKey))
	Response.Write(mobjValues.HiddenControl("hddProdClas", llngProdClas))
	
	'If llngProdClas <> 4 Then
	
	Call mclsTDetail_pre.inspreCA028Grid(lstrCertype, lstrBranch, lstrProduct, lstrPolicy, lstrCertif, ldtmEffecdate, mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), llngCurrency, lstrNewData, lstrKey, lstrAdjust, mobjValues.StringToType(lstrAdjReceipt, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrAdjAmount, eFunctions.Values.eTypeData.etdDouble))
	
	
	lintCount = 0
	lintIndex = 0
	
	If Not mblnError Then
		For	Each lclsTDetail_pre In mclsTDetail_pre.mcolTDetail_pre
			With mobjGrid
				.Columns("Sel").Checked = CShort("2")
				If lclsTDetail_pre.nPremiumA <> eRemoteDB.Constants.intNull Or lclsTDetail_pre.nPremiumE <> eRemoteDB.Constants.intNull Or lclsTDetail_pre.sPrem_Det = "3" Then
					.Columns("Sel").Checked = CShort("1")
				End If
				.Columns("dtcClient").DefValue = lclsTDetail_pre.sClient
				.Columns("tcnCodeItem").DefValue = CStr(lclsTDetail_pre.nItem)
				.Columns("cbeType").DefValue = CStr(lclsTDetail_pre.nType)
				.Columns("tctElement").DefValue = lclsTDetail_pre.sShort_des
				.Columns("tcnCapital").DefValue = CStr(lclsTDetail_pre.nCapital)
				.Columns("hddAddTax").DefValue = lclsTDetail_pre.sAddTax
				.Columns("tcnPremiumA").DefValue = CStr(lclsTDetail_pre.nPremiumA)
				.Columns("tcnPremiumE").DefValue = CStr(lclsTDetail_pre.nPremiumE)
				.Columns("tcnCommi_rate").DefValue = CStr(lclsTDetail_pre.nCommi_rate)
				.Columns("tcnCommission").DefValue = CStr(lclsTDetail_pre.nCommission)
				.Columns("hddBill_item").DefValue = CStr(lclsTDetail_pre.nBill_item)
				.Columns("hddBranch_est").DefValue = CStr(lclsTDetail_pre.nBranch_est)
				.Columns("hddBranch_led").DefValue = CStr(lclsTDetail_pre.nBranch_led)
				.Columns("hddBranch_rei").DefValue = CStr(lclsTDetail_pre.nBranch_rei)
				.Columns("hddModulec").DefValue = CStr(lclsTDetail_pre.nModulec)
				.Columns("hddAddsuini").DefValue = lclsTDetail_pre.sAddsuini
				.Columns("hddCacalili").DefValue = lclsTDetail_pre.sCacalili
				.Columns("hddCommissi_i").DefValue = lclsTDetail_pre.sCommissi_i
				If lstrType_detai <> lclsTDetail_pre.nType Then
					lstrType_detai = lclsTDetail_pre.nType
					lintCodeItem = lclsTDetail_pre.nItem
					lintCount = lintCount + 1
				Else
					If lintCodeItem <> lclsTDetail_pre.nItem Then
						lintCodeItem = lclsTDetail_pre.nItem
						lintCount = lintCount + 1
					End If
				End If
				.Columns("btnPrem_det").Disabled = lstrAdjust = "1"
				'+ Si se trata de una cobertura, o un capital básico no se habilitan las opciones de 
				'+ distribución de prima
				If lstrType_detai = "1" Or lstrType_detai = "7" Then
					.Columns("btnPrem_det").Disabled = True
				End If
				.Columns("btnPrem_det").FieldName = "btnPrem_det" '& lintIndex
				.Columns("btnPrem_det").HRefScript = "showDetai(" & lintIndex & ");"
				.Columns("cbePrem_det").DefValue = CStr(lclsTDetail_pre.nPrem_Det)
				.Columns("hddPrem_det_old").DefValue = CStr(lclsTDetail_pre.nPrem_Det)
				.Columns("hddPrem_det_proc").DefValue = lclsTDetail_pre.sPrem_Det
				.Columns("hddId_Bill").DefValue = CStr(lintCount)
				.Columns("Sel").Disabled = mblnError
			End With
			lintIndex = lintIndex + 1
			Response.Write(mobjGrid.DoRow())
		Next lclsTDetail_pre
	End If
	Response.Write(mobjGrid.CloseTable())
	'End If
	
Response.Write("" & vbCrLf)
Response.Write("	<br>" & vbCrLf)
Response.Write("	<table WIDTH=""100%"">" & vbCrLf)
Response.Write("		<tr>			" & vbCrLf)
Response.Write("			<td><label ID=""13750"">" & GetLocalResourceObject("tcnPremium_AllCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.NumericControl("tcnPremium_All", 18, CStr(mclsTDetail_pre.mcolTDetail_pre.TotPremium),  , GetLocalResourceObject("tcnPremium_AllToolTip"), True, 6, True))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td><label ID=""19274"">" & GetLocalResourceObject("tcnPremiumFact_AllCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.NumericControl("tcnPremiumFact_All", 18, CStr(mclsTDetail_pre.mcolTDetail_pre.Premio),  , GetLocalResourceObject("tcnPremiumFact_AllToolTip"), True, 6, True))


Response.Write("</td>" & vbCrLf)
Response.Write("            <td><label ID=""13744"">" & GetLocalResourceObject("tcnCommisionCaption") & "</label></td>" & vbCrLf)
Response.Write("            <td>")


Response.Write(mobjValues.NumericControl("tcnCommision", 20, CStr(mclsTDetail_pre.mcolTDetail_pre.Commission),  , GetLocalResourceObject("tcnCommisionToolTip"), True, 6, True))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("	</table>" & vbCrLf)
Response.Write("	<br>")

	
	ldblAmount = mclsTDetail_pre.mcolTDetail_pre.Premio
	If ldblAmount <> eRemoteDB.Constants.intNull Then
		ldblAmount = System.Math.Abs(ldblAmount)
	End If
	
	Response.Write(mobjValues.HiddenControl("hddAmountTot", mobjValues.TypeToString(ldblAmount, eFunctions.Values.eTypeData.etdDouble, True, 6)))
	ldblAmount = convertToLocal(ldblAmount, llngCurrency, ldtmEffecdate)
	Response.Write(mobjValues.HiddenControl("hddAmountTotPay", mobjValues.TypeToString(ldblAmount, eFunctions.Values.eTypeData.etdDouble, True, 6)))
	
	Response.Write(mobjValues.BeginPageButton)
	
	If Request.QueryString.Item("Type") <> "PopUp" And Not mblnSequence Then
		If CStr(Session("dEffecdate")) <> vbNullString Then
			If lstrCodisplOrig <> "CA033_CA028" Then
				
Response.Write("" & vbCrLf)
Response.Write("	<table WIDTH=""100%"">" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td CLASS=""HORLINE"" COLSPAN=""3""></td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td WIDTH=""5%"">")


Response.Write(mobjValues.ButtonAbout("CA028"))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td WIDTH=""5%"">")


Response.Write(mobjValues.ButtonHelp("CA028"))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td ALIGN=""RIGHT"">")


Response.Write(mobjValues.ButtonAcceptCancel("EnabledControl()",  , True))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("	</table>")

				
			End If
		End If
	End If
	
	If lstrCodisplOrig = "CA033_CA028" Then
		
Response.Write("" & vbCrLf)
Response.Write("	<table WIDTH=""100%"">" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td CLASS=""HORLINE"" COLSPAN=""3""></td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td WIDTH=""5%"">")


Response.Write(mobjValues.ButtonAbout("CA028"))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td WIDTH=""5%"">")


Response.Write(mobjValues.ButtonHelp("CA028"))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td ALIGN=""RIGHT"">")


Response.Write(mobjValues.ButtonAcceptCancel())


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("	</table>")

		
	End If
End Sub

'% convertToLocal: Convierte monto en moneda local
'---------------------------------------------------------------------------------------------
Private Function convertToLocal(ByRef nAmount As Object, ByRef nCurrency As Object, ByRef dEffecdate As Object) As Object
	'---------------------------------------------------------------------------------------------
	Dim lclsGeneral As eGeneral.Exchange
	lclsGeneral = New eGeneral.Exchange
	
	Call lclsGeneral.Convert(eRemoteDB.Constants.intNull, nAmount, nCurrency, 1, dEffecdate, eRemoteDB.Constants.intNull)
	
	convertToLocal = lclsGeneral.pdblResult
	
	lclsGeneral = Nothing
	
End Function


'% insPreCA028Upd. Se define esta funcion para contruir el contenido de la ventana UPD del recibo manual
'---------------------------------------------------------------------------------------------------------
Private Sub insPreCA028Upd()
	'---------------------------------------------------------------------------------------------------------
	
	'+En ventana popup se crean campos ocultos con informacion de ventana inicial
	'+obtenidas desde el querystring. Estos son todos los campos importantes que 
	'+no forman parte del grid
	With Response
		
		.Write(mobjValues.HiddenControl("tctCertype", lstrCertype))
		.Write(mobjValues.HiddenControl("cbeBranch", lstrBranch))
		.Write(mobjValues.HiddenControl("tcnPolicy", lstrPolicy))
		.Write(mobjValues.HiddenControl("tcnCertif", lstrCertif))
		
		.Write(mobjValues.HiddenControl("optType", lstrTypeReceipt))
		.Write(mobjValues.HiddenControl("cbeCurrency", llngCurrency))
		.Write(mobjValues.HiddenControl("chkAdjust", lstrAdjust))
		
		.Write(mobjValues.HiddenControl("tcdStartDateR", ldtmEffecdate))
		.Write(mobjValues.HiddenControl("tcdExpirDateR", ldtmExpirReceipt))
		.Write(mobjValues.HiddenControl("tcdIssueDate", ldtmIssuedat))
		.Write(mobjValues.HiddenControl("cbeSource", llngTratypei))
		.Write(mobjValues.HiddenControl("tcnReceipt", vbNullString))
		
		'.Write mobjValues.HiddenControl("tcnAdjReceipt", lstrAdjReceipt)
		
		.Write(mobjValues.HiddenControl("cbenreceipt", lstrAdjReceipt))
		.Write(mobjValues.HiddenControl("tcnAdjAmount", lstrAdjAmount))
		.Write(mobjValues.HiddenControl("cbePayWay", lstrTypePay))
		.Write(mobjValues.HiddenControl("hddClient_policy", lstrClient))
		.Write(mobjValues.HiddenControl("hddKey", lstrKey))
		
	End With
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			Call mclsTDetail_pre.inspostCA028Upd(lstrCodispl, .QueryString.Item("Action"), lstrCertype, lstrBranch, lstrProduct, lstrPolicy, lstrCertif, ldtmEffecdate, llngCurrency, mobjValues.StringToType(.QueryString.Item("sType_detai"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBill_item"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCommi_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCommission"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPremiumA"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPremiumE"), eFunctions.Values.eTypeData.etdDouble), CDbl(.QueryString.Item("sAddsuini")), .QueryString.Item("sTypeReceipt"), mobjValues.StringToType(.QueryString.Item("nBill_item"), eFunctions.Values.eTypeData.etdDouble), CInt(.QueryString.Item("sClient")), .QueryString.Item("sAddTax"), Session("nUsercode"), Session("SessionID"), mobjValues.StringToType(.QueryString.Item("nPrem_det"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nPrem_det_old"), eFunctions.Values.eTypeData.etdInteger), CShort("2"))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicyTra.aspx", Request.QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		If Request.QueryString.Item("Action") <> "Del" Then
			Response.Write("<SCRIPT>changeValuesField(""InitialPopUp"")</" & "Script>")
		End If
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca028")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")

Call insLoadParameterQS()

lstrQueryString = "&sCertype=" & Request.QueryString.Item("sCertype") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&dNullDate=" & Request.QueryString.Item("dNullDate") & "&sNullReceipt=" & Request.QueryString.Item("sNullReceipt") & "&sTypeReceipt=" & Request.QueryString.Item("sTypeReceipt") & "&nExeMode=" & Request.QueryString.Item("nExeMode") & "&sExeReport=" & Request.QueryString.Item("sExeReport") & "&nAgency=" & Request.QueryString.Item("nAgency") & "&sCodisplOrig=" & Request.QueryString.Item("sCodisplOrig") & "&sOnSeq=" & Request.QueryString.Item("sOnSeq")

'+ Cuando es llamada desde la CA033 se agregan variables al QueryString	
If lstrCodisplOrig = "CA033_CA028" Then
	lstrQueryString = lstrQueryString & "&sCodispl=" & lstrCodispl & "&sPopUp=1"
End If

'- Se crean las instancias de las variables modulares
With Server
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = lstrCodispl
	Call mobjGrid.SetWindowParameters(lstrCodispl, Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	mclsTDetail_pre = New ePolicy.TDetail_pre
	
	mclsProduct_li = New eProduct.Product
End With
'Dim mclsPremium
%>	
<script>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 14/06/06 18:05 $|$$Author: Gazuaje $"
 
//% showDetai: se muestra la ventana para distribuir la prima de los rec/desc/imp
//-------------------------------------------------------------------------------------------
function showDetai(Index){
//-------------------------------------------------------------------------------------------
	var nCodeItem, nType, sDescript, nPrem_det, nAction, sPrem_det, dIssueDate;
	
//+ Se asigna valor a las variables a mostrar por el QueryString, dependiendo si es Popup o no 
<%If Request.QueryString.Item("Type") = "PopUp" Then%>
	nAction = 302;
	Index = <%=Request.QueryString.Item("Index")%>;
	with(self.document.forms[0]){
		nCodeItem = tcnCodeItem.value;
		dIssueDate= hddIssueDate.value;
		nType = cbeType.value;
		sDescript = tctElement.value;
		nPrem_det = cbePrem_det.value;
		sPrem_det = "2";
	}
<%Else%>
	nAction = 401;
	with(marrArray[Index]){
		nCodeItem = tcnCodeItem;
		dIssueDate = self.document.forms[0].tcdIssueDate.value;
		nType = cbeType;
		sDescript = tctElement;
		nPrem_det = cbePrem_det;
		sPrem_det = hddPrem_det_proc;
	}
<%End If%>

	if(nPrem_det==2 ||
	   sPrem_det=='3')
		ShowPopUp('CA028_1.aspx?dIssueDate=' + dIssueDate + '&nPrem_det=' + nPrem_det + '&sPrem_det=' + sPrem_det + '&nIndex=' + Index + '&nMainAction=' + nAction + '&nCodeItem=' + nCodeItem + '&nType=' + nType + '&sDescript=' + sDescript, 'CA028_1', 650, 400, 'no', 'no', 50, 50) 
}

//% insSelected: se controla la acción sobre la columna SEL
//-------------------------------------------------------------------------------------------
function insSelected(Field){
//-------------------------------------------------------------------------------------------
    var lstrParameters;
    var nPrem_det;
    var nPrem_det_old;
    with(Field){
		nPrem_det = (marrArray[value].cbeType==1)?3:2;
		nPrem_det_old = (marrArray[value].cbeType==1)?nPrem_det:'';
		lstrParameters = 'sType_detai=' + marrArray[value].cbeType + '&nCode=' + marrArray[value].tcnCodeItem + 
		                 '&sClient=' + marrArray[value].dtcClient + '&nBill_item=' + marrArray[value].hddBill_item + 
		                 '&nBranch_est=' + marrArray[value].hddBranch_est + '&nBranch_led=' + marrArray[value].hddBranch_led + 
		                 '&nBranch_rei=' + marrArray[value].hddBranch_rei + '&nCapital=' + marrArray[value].tcnCapital + 
		                 '&nCommi_rate=' + marrArray[value].tcnCommi_rate + '&nCommission=' + marrArray[value].tcnCommission + 
		                 '&nModulec=' + marrArray[value].hddModulec + '&nPremiumA=' + marrArray[value].tcnPremiumA + 
		                 '&nPremiumE=' + marrArray[value].tcnPremiumE + '&sAddsuini=' + marrArray[value].hddAddsuini + 
		                 '&sTypeReceipt=' + self.document.forms[0].hddType.value + '&sAddTax=' + marrArray[value].hddAddTax +
		                 '&dEffecdate=' + self.document.forms[0].tcdIssueDate.value + 
		                 '&nPrem_det=' + nPrem_det + '&nPrem_det_old=' + nPrem_det_old;
		if(checked)
			EditRecord(value, nMainAction, 'Update');
		else
			EditRecord(value, nMainAction, 'Del', lstrParameters);
	}
}

//% changeValuesField: se controla el cambio de valor de los campos de la ventana
//--------------------------------------------------------------------------------------------
function changeValuesField(Option, Field){
//--------------------------------------------------------------------------------------------
	var lstrQS; 
	lstrQS = '<%=lstrQueryString%>';
    switch(Option){
		case "InitialPopUp":
//+ Si se trata de una cobertura, o un capital básico no se habilitan las opciones de 
//+ distribución de prima
			with(self.document.forms[0]){
				cbePrem_det.disabled=(cbeType.value==1 ||
				                      cbeType.value==7);
				tcnPremiumA.disabled=(cbePrem_det.value==2);
				tcnPremiumE.disabled=tcnPremiumA.disabled;
				tcnCommi_rate.disabled=tcnPremiumA.disabled;
				tcnCommission.disabled=tcnPremiumA.disabled;
			}
			break;
		case "cbeSource":
		   <%If llngProdClas = 4 Then%>
		      cbeSource.disabled = true
		   <%End If%>
        case "Prem_det":
        	with(self.document.forms[0]){
        		hddPrem_det_proc.value='2';
//+ Si el tipo de desglose es "Detallar prima", se deshabilitan los campos de prima y comisiones, 
//+ ya que la información de estos campos se grabará al detallar el recargo/descuento/impuesto
				tcnPremiumA.disabled=(Field.value==2);
				tcnPremiumE.disabled=tcnPremiumA.disabled;
				tcnCommi_rate.disabled=tcnPremiumA.disabled;
				tcnCommission.disabled=tcnPremiumA.disabled;
				if(tcnPremiumA.disabled){
					tcnPremiumA.value='';
					tcnPremiumE.value='';
					tcnCommi_rate.value='';
					tcnCommission.value='';
				}
			}
			
			break;
			
        case "Receipt":
//+ Se obtiene y asigna el número de recibo de forma automática
            if(Field.value=="")
/*				if(self.document.forms[0].hddReceipt=="")
					insDefValues('Receipt', "nReceipt=" + Field.value,'/VTimeNet/Policy/PolicyTra/');
				else
					Field.value=self.document.forms[0].hddReceipt.value;
*/            break;

        case "AdjReceipt":
//+ Se recupera la informacion del recibo a ajustar (fechas, moneda, etc.)
            if(Field.value!='')
				insDefValues('AdjReceipt', "nAdjReceipt=" + Field.value,'/VTimeNet/Policy/PolicyTra/');
            else
                with(self.document.forms[0]){
                    tcdStartDateR.value = '';
                    tcdExpirDateR.value = '';
                    tcdStartDateR.disabled=false;
                    tcdExpirDateR.disabled=false;
                    cbeCurrency.value=0;
                    cbeCurrency.disabled=false;
                    tcnPremiumOri.value ='';
                    tcnBalanceOri.value ='';
                }

            break;

        case "Premium":
			with(self.document.forms[0]){
				if (Field.value != '' && 
				    Field.value != '0'){
					hddPremium.value = Field.value;
					if (Field.name == 'tcnPremiumA'){
						tcnPremiumE.value = '';
						hddAddTax.value = '1';
					}
					else{
						tcnPremiumA.value = '';
						hddAddTax.value = '2';
					}
				}
			}
            break;

        case "IssueDate":
			with(self.document.forms[0]){
				self.document.location.href = "ca028.cframe.aspx?sCodispl=<%=Request.QueryString.Item("sCodispl")%>&nMainAction=304&dEffecdate=" + tcdStartDateR.value + 
				    "&dExpirDate=" + tcdExpirDateR.value + 
				    "&nTypeReceipt=" + (optType[0].checked?optType[0].value:optType[1].value) + 
				    "&nReceipt=" + tcnReceipt.value + 
				    "&dIssuedat=" + tcdIssueDate.value + 
				    "&nCurrency=" + cbeCurrency.value + 
				    "&nTratypei=" + cbeSource.value + 
				    "&sOrigReceipt=" + tctOrigReceipt.value + 
				    lstrQS
			}
			break;
			
        case "CheckAdjust":
            with(self.document.forms[0]){
                cbeSource.value = (Field.checked?'2':'0'); //2-Cartera renovación
                cbeSource.disabled = Field.checked;
                //tcnAdjReceipt.disabled = !Field.checked;
                cbenreceipt.disabled = !Field.checked;
                tcnAdjAmount.disabled = !Field.checked;
                if (cbenreceipt.disabled) cbenreceipt.value = '';
                if (tcnAdjAmount.disabled) tcnAdjAmount.value = '';
                
			}
			break;

        case "CheckType":
            with(self.document.forms[0]){
                //cbePayWay.disabled = !optType[1].checked;
                if (optType[1].checked){
					cbePayWay.value = 3;
                }else{
					cbePayWay.value = 0;
                }
//                if(cbePayWay.disabled) cbePayWay.value = 0;
//                tcdStartDateR.disabled = this.checked;
//    		    tcdExpirDateR.disabled = this.checked;
//			 }
			
         //+ Via de pago queda deshabilitada si es recibo de cobro o el recibo original no está pagado            
/*                if(lstrOnSequen != '1'){ 
                    cbePayWay.disabled = (!optType[1].checked) ||  (tcnPremiumOri.value==tcnBalanceOri.value);
                    
                    if(cbePayWay.disabled) cbePayWay.value = 0;
                }                */
//              tcdStartDateR.disabled = this.checked;
//    		  tcdExpirDateR.disabled = this.checked;
			}
			insUpdAdjAmount();
			break;

			
			 
/*			insUpdAdjAmount();
			break;*/

        case "AdjAmount":
            insUpdAdjAmount();
			break;

        case "StartDateR":
//+ Se recupera la informacion del recibo a ajustar (fechas, moneda, etc.)
            if(Field.value!='')
                lstrQS = getCertifParams();
//                alert(lstrQS);
				insDefValues('ExpirDateRec', "dEffecdate=" + Field.value + lstrQS,'/VTimeNet/Policy/PolicyTra/');
            break;
            

        case "InsDetail":
            lstrQS = getReloadParams()
//            alert(lstrQS);
			self.document.location.href = "CA028.aspx?sCodispl=CA028&nMainAction=304&Reload=&ReloadAction=&ReloadIndex=&" +
                                          lstrQS;
            break
   }
}

//%getCertifParams: Retorna cadena con parametros de certificado
//-------------------------------------------------------------------------------------------
function getCertifParams(){
//-------------------------------------------------------------------------------------------
    var sRet;
    
    with(self.document.forms[0]){
        sRet =  '&sCertype=' + '<%=lstrCertype%>' +
    		    '&nBranch=' + cbeBranch.value +
    		    '&nProduct=' + valProduct.value +
    		    '&nPolicy=' + tcnPolicy.value +
    		    '&nCertif=' + tcnCertif.value;
    		    
    }    
    return sRet;
}

//% getReloadParams: Retorna cadena de parametros de querystring para recargar ventana
//-------------------------------------------------------------------------------------------
function getReloadParams(){
//-------------------------------------------------------------------------------------------
    var sRet;
    
    with(self.document.forms[0]){
        sRet =  'sCodisplOrig=' + '<%=lstrCodisplOrig%>' +
    		    getCertifParams() +
    		    '&nCapitalPol=' + tcnCapital_policy.value + 
	            '&dStartPolicy=' + tcdStartDate_policy.value +
	            '&dExpirPolicy=' + tcdExpirdate_policy.value +
	            '&nPremiumCer=' + tcnNetPremium_policy.value +
	            '&sClient=' + dtcClient_policy.value +
    		    '&dEffecdate=' + tcdStartDateR.value +
    		    '&dEffecdateIni=' + tcdStartDateR.value +
    		    '&dExpirDate=' + tcdExpirDateR.value + 
    		    '&sTypeReceipt=' + (optType[0].checked?optType[0].value:optType[1].value) +
                '&nCurrency=' + cbeCurrency.value +
//    		    '&dNullDate=' + tcdNullDate.value +
//    		    '&sNullReceipt=' + chkNullReceipt.value +
    //		    '&nExeMode=' + lstrExeMode +
    //		    '&sExeReport=' + lstrExeReport +
    //		    '&nAgency=' + lstrAgency +
//    		    '&sOnSeq=' + lstrOnSeq +
    		    '&sNewData=1' + //lstrNewData +
    		    '&nTratypei=' + cbeSource.value +
    		    '&sKey=' + hddKey.value +
    	        '&sAdjust=' + (chkAdjust.checked?1:2) +
    	        //'&nAdjReceipt=' + tcnAdjReceipt.value +
    	        '&nAdjReceipt=' + cbenreceipt.value +
    	        '&nAdjAmount=' + hddAdjAmount.value + 
    	        '&nTypePay=' + cbePayWay.value + 
    	        '&nPremiumOri=' + tcnPremiumOri.value +
    	        '&nBalanceOri=' + tcnBalanceOri.value;
    }    
    return sRet;
}

//%insUpdAdjAmount: Almacena en un campo oculto el monto de ajuste(positivo o negativo)
//--------------------------------------------------------------------------------------------
function insUpdAdjAmount() {
//--------------------------------------------------------------------------------------------
    var nValue;
    
    nValue = insConvertNumber(self.document.forms[0].tcnAdjAmount.value);
    
    if(!isNaN(nValue)){
//+Como el monto siempre se muestra positivo, se hace conversion necesaria
//+antes de almacenarlo
        if(self.document.forms[0].optType[1].checked) //+ Recibo de devolucion
            nValue = -nValue;
            
        self.document.forms[0].hddAdjAmount.value = VTFormat(nValue, '','', '', 6, true);
    }        
    else
        self.document.forms[0].hddAdjAmount.value = 0;
}

</script>
<html>
<head>




<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <%
Response.Write(mobjValues.StyleSheet())

'+ Si Session("dEffecdate") está vacío significa que se está trabajando desde el menú 
'+ principal del sistema
If CStr(Session("dEffecdate")) <> vbNullString Then
	If Request.QueryString.Item("Type") <> "PopUp" Then
		Response.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		'+ Si la ventana se está mostrando en la secuencia de la póliza 
		If lstrOnSeq = "1" Then
			Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		End If
	End If
Else
	If Request.QueryString.Item("Type") <> "PopUp" Then
		With Response
			If Request.QueryString.Item("sCodisplOrig") <> "CA033_CA028" Then
				.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
			End If
			.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		End With
	End If
End If
mobjMenu = Nothing
%>
</head>
<body ONUNLOAD="closeWindows();">
<form METHOD="post" ID="FORM" NAME="CA028" ACTION="ValPolicyTra.aspx?sTime=1<%=lstrQueryString%>">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Call insDefineGrid()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCA028()
Else
	Call insPreCA028Upd()
End If

If Request.QueryString.Item("Type") <> "PopUp" And CStr(Session("dEffecdate")) <> vbNullString Then
	Response.Write("<SCRIPT>self.document.forms[0].action='ValPolicyTra.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&sPopUp=1'</SCRIPT>")
End If

If mblnError Then
	Response.Write("<SCRIPT>alert(""Err. 60583: " & eFunctions.Values.GetMessage(60583) & """);</SCRIPT>")
End If

mclsTDetail_pre = Nothing
mobjGrid = Nothing
mobjValues = Nothing
mclsPolicy_his = Nothing
mclsProduct_li = Nothing
'Set mclsPremium = Nothing
%>
</form>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.19
Call mobjNetFrameWork.FinishPage("ca028")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>






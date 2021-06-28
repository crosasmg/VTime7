<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.03
Dim mobjNetFrameWork As eNetFrameWork.Layout

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mclsCover As ePolicy.Cover
Dim mclsTCover As ePolicy.TCover
Dim mobjGeneral As eGeneral.GeneralFunction

Dim mblnFound As Boolean
Dim mstrRole As String
Dim mstrClient As String

Dim mintGroup As Integer
Dim mintCurrency As Integer
Dim mdblLegAmount As Object

Dim mstrError As String
Dim mstrTotalPrima As Double
Dim mstrTotalPrimaS As Object
Dim mstrTotalPrimaT As Object
Dim mstrTotalPrimaM As Object


Dim lcolRole As ePolicy.Roleses
Dim lclsRole As Object


'%insDefineHeader: Este procedimiento se encarga de definir las líneas del encabezado del grid.
'---------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------
	Dim lobjColumn As Object
	Dim lintLength As Short
	Dim lstrVar As Object
	Dim lintGroup As Integer
	Dim lintCurrency As Object
	Dim lclsCurren_pol As ePolicy.Curren_pol
	
	'+ Sólo tiene sentido para póliza matriz
	lintLength = 120
	
	Session("sTyp_module") = ""
	
	lclsCurren_pol = New ePolicy.Curren_pol
	
	Call lclsCurren_pol.Find_Currency_Sel(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
	
	lintCurrency = lclsCurren_pol.nCurrency
	
	lclsCurren_pol = Nothing
	
	mblnFound = mclsCover.InsPreCA014(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(lintCurrency, eFunctions.Values.eTypeData.etdDouble), lintGroup, "CA014", Session("nUsercode"), Session("dNulldate"), Session("nTransaction"), mobjValues.StringToType(mstrRole, eFunctions.Values.eTypeData.etdDouble, True), mstrClient, Session("sBrancht"), Request.QueryString.Item("sKey"), Session("SessionId"), Request.QueryString.Item("sDelTCover"), Nothing, vbNullString, vbNullString, mobjValues.StringToType(Session("nType_amend"), eFunctions.Values.eTypeData.etdLong))
	
	mintGroup = mclsCover.nGroup
	mintCurrency = mclsCover.nCurrency
	
	'+ Variable para controlar la actualización de la información de manera puntual (desde el botón de la ventana)
	Response.Write(mobjValues.HiddenControl("hddbPuntual", CStr(False)))
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddHiddenColumn("hddnExist", CStr(2))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkRequiredColumnCaption"), "chkRequired", "",  ,  , "insChangeRequired()", Request.QueryString.Item("Type") <> "PopUp" Or (Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("sRequire") = "1"), GetLocalResourceObject("chkRequiredColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "table12", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeRoleColumnToolTip"))
		Call .AddTextColumn(40785, GetLocalResourceObject("tctCoverColumnCaption"), "tctCover", lintLength, vbNullString,  , GetLocalResourceObject("tctCoverColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(40807, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, CStr(0), True, GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 2)
		Call .AddNumericColumn(40793, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(0), True, GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 2)
		Call .AddNumericColumn(40793, GetLocalResourceObject("tcnPremium2ColumnCaption"), "tcnPremium2", 18, CStr(0), True, GetLocalResourceObject("tcnPremium2ColumnToolTip"), True, 2)
		Call .AddNumericColumn(40793, GetLocalResourceObject("tcnPremium3ColumnCaption"), "tcnPremium3", 18, CStr(0), True, GetLocalResourceObject("tcnPremium3ColumnToolTip"), True, 2)
		Call .AddNumericColumn(40793, GetLocalResourceObject("tcnPremium4ColumnCaption"), "tcnPremium4", 18, CStr(0), True, GetLocalResourceObject("tcnPremium4ColumnToolTip"), True, 2)
		Call .AddHiddenColumn("hddsKeyGrid", vbNullString)
		Call .AddHiddenColumn("tcnModulec", vbNullString)
		Call .AddHiddenColumn("tcnCover", vbNullString)
		Call .AddHiddenColumn("hddCurrency", vbNullString)
		Call .AddHiddenColumn("hddnModulec", vbNullString)
		Call .AddHiddenColumn("hddnCover", vbNullString)
		Call .AddHiddenColumn("hddnCapital", vbNullString)
		Call .AddHiddenColumn("hddnRatecove", vbNullString)
		Call .AddHiddenColumn("hddnPremium", vbNullString)
		Call .AddHiddenColumn("hddsFrandedi", vbNullString)
		Call .AddHiddenColumn("hddsFrancApl", vbNullString)
		Call .AddHiddenColumn("hddnFraRate", vbNullString)
		Call .AddHiddenColumn("hddnFixamount", vbNullString)
		Call .AddHiddenColumn("hddnMinamount", vbNullString)
		Call .AddHiddenColumn("hddsWait_Type", vbNullString)
		Call .AddHiddenColumn("hddnCapital_o", vbNullString)
		Call .AddHiddenColumn("hddnRatecove_o", vbNullString)
		Call .AddHiddenColumn("hddnPremium_o", vbNullString)
		Call .AddHiddenColumn("hddnMaxamount", vbNullString)
		Call .AddHiddenColumn("hddnDiscount", vbNullString)
		Call .AddHiddenColumn("hddnDisc_amoun", vbNullString)
		Call .AddHiddenColumn("hddnRole", mstrRole)
		Call .AddHiddenColumn("hddnWaitQ", vbNullString)
		Call .AddHiddenColumn("hddnAgeIns", vbNullString)
		Call .AddHiddenColumn("hddnAgeminins", vbNullString)
		Call .AddHiddenColumn("hddnAgemaxins", vbNullString)
		Call .AddHiddenColumn("hddnAgemaxper", vbNullString)
		Call .AddHiddenColumn("hddnAgemininsf", vbNullString)
		Call .AddHiddenColumn("hddnAgemaxinsf", vbNullString)
		Call .AddHiddenColumn("hddnAgemaxperf", vbNullString)
		Call .AddHiddenColumn("hddnCauseupd", vbNullString)
		Call .AddHiddenColumn("hddnBranch_rei", vbNullString)
		Call .AddHiddenColumn("hddnDurinsur", vbNullString)
		Call .AddHiddenColumn("hddnTypdurins", vbNullString)
		Call .AddHiddenColumn("hddsExist", vbNullString)
		Call .AddHiddenColumn("hddsChange", "1")
		Call .AddHiddenColumn("tcnRatecove", vbNullString)
		Call .AddHiddenColumn("cbeFrandedi", vbNullString)
		Call .AddHiddenColumn("cbeFrancApl", vbNullString)
		Call .AddHiddenColumn("tcnFraRate", vbNullString)
		Call .AddHiddenColumn("tcnFixamount", vbNullString)
		Call .AddHiddenColumn("tcnMinamount", vbNullString)
		Call .AddHiddenColumn("cbeWait_Type", vbNullString)
		Call .AddHiddenColumn("tcnMaxamount", vbNullString)
		Call .AddHiddenColumn("tcnDiscount", vbNullString)
		Call .AddHiddenColumn("tcnDisc_amoun", vbNullString)
		Call .AddHiddenColumn("tcnWaitQ", vbNullString)
		Call .AddHiddenColumn("tcnAgeminins", vbNullString)
		Call .AddHiddenColumn("tcnAgemaxins", vbNullString)
		Call .AddHiddenColumn("tcnAgemaxper", vbNullString)
		Call .AddHiddenColumn("cbeCauseupd", vbNullString)
		Call .AddHiddenColumn("tcnAgemininsf", vbNullString)
		Call .AddHiddenColumn("tcnAgemaxinsf", vbNullString)
		Call .AddHiddenColumn("tcnAgemaxperf", vbNullString)
		Call .AddHiddenColumn("valBranch_rei", vbNullString)
		Call .AddHiddenColumn("tcnDurinsur", vbNullString)
		Call .AddHiddenColumn("cbeTypdurins", vbNullString)
		Call .AddHiddenColumn("hddnCapital_Wait", vbNullString)
		Call .AddHiddenColumn("hddnTypdurpay", vbNullString)
		Call .AddHiddenColumn("hddnDurpay", vbNullString)
		Call .AddHiddenColumn("hddnRetarif", vbNullString)
		Call .AddHiddenColumn("hdddfer", vbNullString)
		Call .AddHiddenColumn("tcdFer", vbNullString)
		
	End With
	
	With mobjGrid
		.ActionQuery = Session("bQuery")
		.Codispl = Request.QueryString.Item("sCodispl")
		.FieldsByRow = 2
		
		'+Si se trata de un producto de vida
		.Top = 5
		.Left = 10
		.Width = 770
		
		If Session("sBrancht") = 1 Then
			.Height = 520
		Else
			.Height = 420
		End If
		
		'.Splits_Renamed.AddSplit 0,"",2
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
		
		.DeleteButton = False
		.AddButton = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.sDelRecordParam = "' + marrArray[lintCount].hddsParam + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.EditRecordQuery = mobjValues.ActionQuery
	End With
	
End Sub

'% insPreVI7011: Obtiene la información de las coberturas de una póliza matriz
'-------------------------------------------------------------------------------------------
Private Sub insPreVI7011()
	'-------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lblnOk As Object
	Dim lblnLeg As Object
	Dim lclsPolicy As Object
	Dim lstrDataFound As String
	
	mobjGrid.Columns("hddsKeyGrid").DefValue = mclsCover.sKey
	
	'+Se recorren las coberturas encontradas, para mostrarlas en el Grid
	Dim lobjError As eFunctions.Errors
	If mblnFound Then
		lstrDataFound = "2"
		If mclsCover.mcolTCovers.bDataFound Then
			lstrDataFound = "1"
		End If
		Response.Write(mobjValues.HiddenControl("hddnDataFound", lstrDataFound))
		lintIndex = 0
		mstrTotalPrima = 0
		For	Each mclsTCover In mclsCover.mcolTCovers
			With mobjGrid
				
				.Columns("Sel").Checked = mclsTCover.nSel(mclsCover.mcolTCovers.bDataFound)
				If mclsTCover.nSel(mclsCover.mcolTCovers.bDataFound) = CDbl("1") Then
					mstrTotalPrima = mstrTotalPrima + mclsTCover.nPremium
					'					Response.Write "<NOTSCRIPT>mintTpremium +=insConvertNumber(VTFormat (" & mclsTCover.nPremium & ",'', '', '', 6, true));</" & "Script>"                    
				End If
				
				
				.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
				If mclsTCover.sRequired = "" Then
					.Columns("chkRequired").Checked = 2
				Else
					.Columns("chkRequired").Checked = CShort(mclsTCover.sRequired)
				End If
				If mclsTCover.sRequired = "1" Then
					.Columns("Sel").Disabled = True
				Else
					.Columns("Sel").Disabled = False
				End If
				.Columns("tctCover").DefValue = mclsTCover.sDescript
				.Columns("tcnCapital").DefValue = CStr(mclsTCover.nCapital)
				.Columns("tcnPremium").DefValue = CStr(mclsTCover.nPremium)
				.Columns("tcnPremium2").DefValue = CStr(System.Math.Round(mclsTCover.nPremium / 2, 2))
				.Columns("tcnPremium3").DefValue = CStr(System.Math.Round(mclsTCover.nPremium / 4, 2))
				.Columns("tcnPremium4").DefValue = CStr(System.Math.Round(mclsTCover.nPremium / 12, 2))
				
				.Columns("tcnModulec").DefValue = CStr(mclsTCover.nModulec)
				.Columns("tcnCover").DefValue = CStr(mclsTCover.nCover)
				.Columns("cbeRole").DefValue = CStr(mclsTCover.nRole)
				.Columns("hddCurrency").DefValue = CStr(mclsCover.nCurrency)
				.Columns("hddnModulec").DefValue = CStr(mclsTCover.nModulec)
				.Columns("hddnCover").DefValue = CStr(mclsTCover.nCover)
				.Columns("hddnCapital").DefValue = CStr(mclsTCover.nCapital)
				.Columns("hddnRateCove").DefValue = CStr(mclsTCover.nRateCove)
				.Columns("tcnRatecove").DefValue = CStr(mclsTCover.nRateCove)
				.Columns("hddnPremium").DefValue = CStr(mclsTCover.nPremium)
				.Columns("hddsFrandedi").DefValue = mclsTCover.sFrandedi
				.Columns("hddsFrancApl").DefValue = mclsTCover.sFrancApl
				.Columns("cbeFrandedi").DefValue = mclsTCover.sFrandedi
				.Columns("cbeFrancApl").DefValue = mclsTCover.sFrancApl
				.Columns("tcnFraRate").DefValue = CStr(mclsTCover.nRate)
				.Columns("hddnFraRate").DefValue = CStr(mclsTCover.nRate)
				.Columns("hddnFixAmount").DefValue = CStr(mclsTCover.nFixamount)
				.Columns("hddnMinAmount").DefValue = CStr(mclsTCover.nMinamount)
				.Columns("tcnFixamount").DefValue = CStr(mclsTCover.nFixamount)
				.Columns("tcnMinamount").DefValue = CStr(mclsTCover.nMinamount)
				.Columns("hddsWait_type").DefValue = mclsTCover.sWait_type
				.Columns("cbeWait_Type").DefValue = mclsTCover.sWait_type
				.Columns("hddnCapital_o").DefValue = CStr(mclsTCover.nCapital_o)
				.Columns("hddnRatecove_o").DefValue = CStr(mclsTCover.nRateCove_o)
				.Columns("hddnPremium_o").DefValue = CStr(mclsTCover.nPremium_o)
				.Columns("hddnMaxAmount").DefValue = CStr(mclsTCover.nMaxamount)
				.Columns("hddnDiscount").DefValue = CStr(mclsTCover.nDiscount)
				.Columns("hddnDisc_amoun").DefValue = CStr(mclsTCover.nDisc_amoun)
				.Columns("tcnMaxamount").DefValue = CStr(mclsTCover.nMaxamount)
				.Columns("tcnDiscount").DefValue = CStr(mclsTCover.nDiscount)
				.Columns("tcnDisc_amoun").DefValue = CStr(mclsTCover.nDisc_amoun)
				.Columns("hddnWaitQ").DefValue = CStr(mclsTCover.nWait_quan)
				.Columns("tcnWaitQ").DefValue = CStr(mclsTCover.nWait_quan)
				.Columns("hddnAgeIns").DefValue = CStr(mclsCover.mclsRoles.nAge)
				.Columns("hddnCauseupd").DefValue = CStr(mclsTCover.nCauseupd)
				.Columns("cbeCauseupd").DefValue = CStr(mclsTCover.nCauseupd)
				.Columns("hddnBranch_Rei").DefValue = CStr(mclsTCover.nBranch_rei)
				.Columns("valBranch_rei").DefValue = CStr(mclsTCover.nBranch_rei)
				.Columns("hddsExist").DefValue = mclsTCover.sExist
				.Columns("hddsChange").DefValue = mclsTCover.sChange
				.Columns("hddnCapital_Wait").DefValue = CStr(mclsTCover.nCapital_wait)
				.Columns("hdddfer").DefValue = CStr(mclsTCover.dFer)
				.Columns("tcdFer").DefValue = CStr(mclsTCover.dFer)
				If CStr(Session("sBrancht")) = "1" Then
					.Columns("tcnAgeminins").DefValue = CStr(mclsTCover.nAgeminins)
					.Columns("hddnAgeminins").DefValue = CStr(mclsTCover.nAgeminins)
					.Columns("tcnAgemininsf").DefValue = CStr(mclsTCover.nAgeminins)
					.Columns("hddnAgemininsf").DefValue = CStr(mclsTCover.nAgeminins)
					.Columns("hddnAgemaxins").DefValue = CStr(mclsTCover.nAgemaxins)
					.Columns("tcnAgemaxins").DefValue = CStr(mclsTCover.nAgemaxins)
					.Columns("tcnAgemaxinsf").DefValue = CStr(mclsTCover.nAgemaxins)
					.Columns("hddnAgemaxinsf").DefValue = CStr(mclsTCover.nAgemaxins)
					.Columns("hddnAgemaxper").DefValue = CStr(mclsTCover.nAgemaxper)
					.Columns("hddnAgemaxperf").DefValue = CStr(mclsTCover.nAgemaxper)
					.Columns("tcnAgemaxper").DefValue = CStr(mclsTCover.nAgemaxper)
					.Columns("tcnAgemaxperf").DefValue = CStr(mclsTCover.nAgemaxper)
					.Columns("hddnRetarif").DefValue = CStr(mclsTCover.nRetarif)
					If mclsCover.nProdClas = 1 Or mclsCover.nProdClas = 7 Then
						.Columns("hddnTypdurins").DefValue = CStr(mclsTCover.nTypdurins)
						.Columns("cbeTypdurins").DefValue = CStr(mclsTCover.nTypdurins)
						
						If mclsTCover.nTypdurins <> 3 Then
							.Columns("hddnDurinsur").DefValue = CStr(mclsTCover.nDurinsur)
							.Columns("tcnDurinsur").DefValue = CStr(mclsTCover.nDurinsur)
						End If
						.Columns("hddnTypdurpay").DefValue = CStr(mclsTCover.nTypdurpay)
						.Columns("hddnDurpay").DefValue = CStr(mclsTCover.nDurpay)
					End If
				End If
				Response.Write(.DoRow)
			End With
			lintIndex = lintIndex + 1
		Next mclsTCover
		
	Else
		If mclsCover.nError > 0 Then
			lobjError = New eFunctions.Errors
			'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
			lobjError.sSessionID = Session.SessionID
			lobjError.nUsercode = Session("nUsercode")
			'~End Body Block VisualTimer Utility
			Response.Write(lobjError.ErrorMessage("VI7011", mclsCover.nError,  ,  ,  , True))
			lobjError = Nothing
		End If
		
	End If
	Response.Write(mobjValues.HiddenControl("hddnAge", CStr(mclsCover.mclsRoles.nAge)))
	Response.Write(mobjValues.HiddenControl("hddsVIP", mclsCover.mclsRoles.sVIP))
	Response.Write(mobjValues.HiddenControl("cbeCurrencDes", CStr(mclsCover.nCurrency)))
	Response.Write(mobjValues.HiddenControl("valGroup", CStr(0)))
	Response.Write(mobjValues.HiddenControl("hddnGroup", CStr(0)))
	Response.Write(mobjValues.HiddenControl("hddnProdclas", CStr(mclsCover.nProdClas)))
	Response.Write(mobjValues.HiddenControl("hddsKey", mclsCover.sKey))
	Response.Write(mobjValues.HiddenControl("tcnLeg", vbNullString))
	
	
Response.Write("" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("    <TD ALIGN=CENTER COLSPAN=5><LABEL>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	<TD ALIGN=RIGHT>")

	Response.Write(mobjValues.DIVControl("tcnAnual", False, FormatNumber(mstrTotalPrima, 2)))
Response.Write("</TD>" & vbCrLf)
Response.Write("	<TD ALIGN=RIGHT>")

	Response.Write(mobjValues.DIVControl("tcnsemestral", False, FormatNumber(mstrTotalPrima / 2, 2)))
Response.Write("</TD>" & vbCrLf)
Response.Write("	<TD ALIGN=RIGHT>")

	Response.Write(mobjValues.DIVControl("tcntrimestral", False, FormatNumber(mstrTotalPrima / 4, 2)))
Response.Write("</TD>" & vbCrLf)
Response.Write("	<TD ALIGN=RIGHT>")

	Response.Write(mobjValues.DIVControl("tcnmensual", False, FormatNumber(mstrTotalPrima / 12, 2)))
Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>")

	
	'+Se cierra el recorrido de la tabla
	Response.Write(mobjGrid.CloseTable())
	Response.Write("</BR>")
	
	Response.Write(mobjValues.BeginPageButton)
	
	Response.Write("<SCRIPT>mintTpremium = VTFormat (" & mstrTotalPrima & ",'', '', '', 2, true);</" & "Script>")
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI7011")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjGeneral = New eGeneral.GeneralFunction
mstrError = mobjGeneral.insLoadMessage(55963)
mobjGeneral = Nothing

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With

mobjValues.ActionQuery = Session("bQuery")

lcolRole = New ePolicy.Roleses


Call lcolRole.Find_Tab_Covrol(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), 0)

For	Each lclsRole In lcolRole
	mstrRole = lclsRole.nRole
	mstrClient = lclsRole.sClient
Next lclsRole

lcolRole = Nothing
lclsRole = Nothing

%>

<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 5-04-06 19:01 $"

	var mintGroupChange = 0; mintCurrencyChange = 0; mlngCapital = 0; mlngRatecove = 0; mintRetarif = 0; mintTpremium = "";

//% insAccept: Se acpta la secuencia en tratamiento 
//------------------------------------------------------------------------------------------
function insAccept(){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
		self.document.forms[0].hddbPuntual.value = true;
	}
	top.frames['fraHeader'].ClientRequest(390,2);
}
//------------------------------------------------------------------------------------------
function InsCalTotalPremium(lintTpremium){
//------------------------------------------------------------------------------------------
        mintTpremium= VTFormat (lintTpremium ,'', '', '', 2, true);
        UpdateDiv('tcnAnual', VTFormat(lintTpremium,'', '', '', 2, true));
        UpdateDiv('tcnsemestral', VTFormat(lintTpremium/2,'', '', '', 2, true));
        UpdateDiv('tcntrimestral', VTFormat(lintTpremium/4,'', '', '', 2, true));
        UpdateDiv('tcnmensual', VTFormat(lintTpremium/12,'', '', '', 2, true));                        
}
//% insCheckSelClick: controla la columna Sel, para mostrar la ventana PopUp    
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------
    var lstrQueryString;
    var lstrString;
    var lstrString_Val;
    var lintAge = '';
	var lstrTotPremium = '';
	var lstrAction;
	var lstrChecked;
    
	if (typeof(mstrDoSubmit) == 'undefined') mstrDoSubmit = '1';
	if (mstrDoSubmit == '1'){
        mstrDoSubmit = '2';
		if (typeof(self.document.forms[0].hddnAge) != 'undefined'){
			lintAge = self.document.forms[0].hddnAge.value;
		}
		lstrQueryString = '&nAge=' + lintAge;

		if (!Field.checked){
			if (marrArray[lintIndex].hddsRequire=="1"){
			    alert("Error 55963:" + "<%=mstrError%>");
			    Field.checked = !Field.checked;
			    marrArray[lintIndex].Sel = 1;
			}
			else{
				lstrString = 'sKey=' + marrArray[lintIndex].hddsKeyGrid;
				lstrString = lstrString + '&nModulec=' + marrArray[lintIndex].tcnModulec;
				lstrString = lstrString + '&nCover=' + marrArray[lintIndex].tcnCover;
				lstrString = lstrString + '&nRole=' + '<%=mstrRole%>';
				lstrString = lstrString + '&sClient=' + '<%=mstrClient%>';
				 
				if(typeof(self.document.forms[0].valGroup)!='undefined'){
				    lstrString = lstrString + "&nGroup=" + self.document.forms[0].valGroup.value;
				}
				else{
					lstrString = lstrString + "&nGroup=0"
				}
				lstrString = lstrString + lstrTotPremium
				setPointer('wait');
				if ('<%=Session("sBrancht")%>' == '1'){
			        mstrDoSubmit = '1';
			        lstrAction = 'Del';
			        lstrChecked = '!';
				}
				else{
				    insDefValues("DelTCover", lstrString, '/VTimeNet/Policy/PolicySeq');
				}
			}
		}
		else {
			mstrDoSubmit = '1';
			lstrAction   = 'Update';
	        lstrChecked = '';
		}

        if (mstrDoSubmit == '1'){
		    lstrString_Val = '&nRole=' + '<%=mstrRole%>';
		    lstrString_Val = lstrString_Val + '&nGroup=' + 0;
		    lstrString_Val = lstrString_Val + '&nAge=' + lintAge;
		    lstrString_Val = lstrString_Val + '&sClient=' + '<%=mstrClient%>';
		    lstrString_Val = lstrString_Val + '&nModulec=' + marrArray[lintIndex].tcnModulec;
		    lstrString_Val = lstrString_Val + '&nCover=' + marrArray[lintIndex].tcnCover;
		    lstrString_Val = lstrString_Val + '&nPremium=' + marrArray[lintIndex].tcnPremium;
		    lstrString_Val = lstrString_Val + '&nCurrency=' + self.document.forms[0].cbeCurrencDes.value;
            lstrString_Val = lstrString_Val + '&TotalPrima=' + mintTpremium;
		    lstrString_Val = lstrString_Val + '&nProdclas=' + self.document.forms[0].hddnProdclas.value;
		    lstrString_Val = lstrString_Val + '&nIndexCover=' + '<%=Request.QueryString.Item("nIndexCover")%>';
		    lstrString_Val = lstrString_Val + '&sChecked=' + lstrChecked;
		    lintIndex+=1;
		  
			mstrDoSubmit = '2';
		    document.forms[0].action ="ValPolicySeq.aspx?nZone=2&sCodispl=CA014&Action=" + lstrAction + "&sCodisplori=VI7011" + "&WindowType=PopUp&nMainAction=304&ActionType=Check&nIndex=" + lintIndex + lstrString_Val
		    top.frames['fraFolder'].document.forms[0].target="fraGeneric";
		    self.document.forms[0].cbeCurrencDes.disabled = false;
		    setPointer('wait');
		    self.document.forms[0].submit(); 
		    self.document.forms[0].cbeCurrencDes.disabled = true;

        }
	}
	else{
	    Field.checked = !Field.checked;
		alert('Por favor espere');
	}

}
//% insChangeRequired: Si se selecciona el campo requerido automáticamente se selecciona el campo pre-selección.
//-------------------------------------------------------------------------------------------
function insChangeRequired(){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
		if (chkRequired.checked)
			chkDefaulti.checked = chkRequired.checked;
	}
}

//% insReload: Se encarga de recargar la página al seleccionar cualquier valor de los campos del encabezado del grid.
//-------------------------------------------------------------------------------------------
function insReload(Field){
//-------------------------------------------------------------------------------------------
    var lstrQuery
    var lblnChange
    var lstrDelTCover

//+ Si se cambia la moneda entonces no se borra la tabla temporal tcover de lo contrario si (cuando se cambia el grupo)    
    with (self.document.forms[0]) {
		 lstrQuery = "&sKey=" + hddsKey.value;
//+ Caso en que el grupo esté visible
		lstrQuery = lstrQuery + "&nGroup=0"
		
		if (mintCurrencyChange!=cbeCurrencDes.value) {
			mintCurrencyChange = cbeCurrencDes.value;
			lblnChange = true;
		}

//+ Si hubo algún cambio en cuanto al grupo (si corresponde) o la moneda; se recarga la ventana.
		if (lblnChange==true) {

			lstrQuery = lstrQuery + "&nCurrency=" + cbeCurrencDes.value + "&sDelTCover=";
			document.location.href = document.location.href.replace(/&sKey=.*/,'') + lstrQuery
		}
    }
} 


</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
End With

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & ";</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>

<FORM METHOD="POST" ID="FORM" NAME="CA014" ACTION="ValPolicySeq.aspx?nRole=<%=mstrRole%>&sClient=<%=mstrClient%>&nIndexCover=<%=Request.QueryString.Item("nIndexCover")%>">
<%
mclsCover = New ePolicy.Cover
Call insDefineHeader()
Call insPreVI7011()
mclsCover = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
Call mobjNetFrameWork.FinishPage("VI7011")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>







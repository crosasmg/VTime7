<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.16
Dim mobjNetFrameWork As eNetFrameWork.Layout
Dim sDebtCli As String
Dim sCertype As Object
Dim nBranch As Integer
Dim nProduct As Integer
Dim nPolicy As Double
Dim nCertif As Double
Dim dEffecdate As Date
Dim nConsec As Object
Dim nCurrency As Object

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del grid

Dim mobjGrid As eFunctions.Grid
'+Variables a usar en la busqueda del plazo en credit
Dim mintAge As Object
Dim lclsCertificat As ePolicy.Credit

Dim mcolCreditSales As ePolicy.CreditSaless

Dim lclsCurren_pol As ePolicy.Curren_pol


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lintConsec As Object
	Dim lclsProduct As eProduct.Gen_cover
	Dim lintCountry As Object
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	mobjGrid.ActionQuery = Session("bQuery")
	lintConsec = Request.QueryString.Item("nConsec")
	If lintConsec = vbNullString Then
		lintConsec = nConsec
	End If
	
	lintCountry = 56
	lclsProduct = New eProduct.Gen_cover
	If lclsProduct.FindCoverGen_Product(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), 0, 101) Then
		lintCountry = 99
	End If
	
	
	'+ Se definen las columnas del grid    
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnConsecColumnCaption"), "tcnConsec", 10, Request.QueryString.Item("nConsec"),  , GetLocalResourceObject("tcnConsecColumnToolTip"), False,  ,  ,  ,  , True)
		Call .AddTextColumn(40149, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", 30, sDebtCli,  , GetLocalResourceObject("tctClientColumnToolTip"),  ,  ,  , True)
		Call .AddDateColumn(40150, GetLocalResourceObject("tcdDocdateColumnCaption"), "tcdDocdate",  ,  , GetLocalResourceObject("tcdDocdateColumnToolTip"),  ,  , "ShowChangeValues(this)")
		Call .AddDateColumn(40150, GetLocalResourceObject("tcdExpirdocColumnCaption"), "tcdExpirdoc",  ,  , GetLocalResourceObject("tcdExpirdocColumnToolTip"),  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeDocTypeColumnCaption"), "cbeDocType", "Table9001", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeDocTypeColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctNumDocColumnCaption"), "tctNumDoc", 12, "",  , GetLocalResourceObject("tctNumDocColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, nCurrency,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		If lintCountry = 56 Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCountryColumnCaption"), "cbeCountry", "Table66", eFunctions.Values.eValuesType.clngComboType, lintCountry,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCountryColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCountryColumnCaption"), "cbeCountry", "Table66", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCountryColumnToolTip"))
		End If
		Call .AddNumericColumn(40145, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0),  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		Call .AddButtonColumn(0, GetLocalResourceObject("SCA2-818ColumnCaption"), "SCA2-818", 0, True, Request.QueryString.Item("Type") <> "PopUp",  ,  ,  ,  , "btnNotenum")
		
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CT002"
		.Columns("tcnConsec").EditRecord = True
		.Height = 400
		.Width = 500
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		
		.sDelRecordParam = "nConsec='+ marrArray[lintIndex].tcnConsec + '"
		.Columns("Sel").GridVisible = Not Session("bQuery")
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'% insPreCT002: Lee los valores de la tabla CreditSales
'--------------------------------------------------------------------------------------------
Private Sub insPreCT002()
	'--------------------------------------------------------------------------------------------
	Dim mcolCreditSales As ePolicy.CreditSaless
	Dim lintIndex As Integer
	Dim lintCount As Integer
	Dim lclsGeneral As eGeneral.GeneralFunction
	Dim lintIsError As Byte
	Dim lstrMessage As String
	Dim lclsErrors As eFunctions.Errors
	Dim lstrMessage_tmp As String
	
	
	lclsCurren_pol = Nothing
	
	lclsErrors = New eFunctions.Errors
	lstrMessage_tmp = lclsErrors.ErrorMessage("CT002", 56029,  ,  ,  , True)
	If InStr(1, lstrMessage_tmp, "Err.") > 0 Then
		lintIsError = 1
	Else
		If InStr(1, lstrMessage_tmp, "Adv.") > 0 Then
			lintIsError = 0
		End If
	End If
	lclsGeneral = New eGeneral.GeneralFunction
	lstrMessage = lclsGeneral.insLoadMessage(56029)
	mcolCreditSales = New ePolicy.CreditSaless
	nConsec = 1
	If mcolCreditSales.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		lintCount = mcolCreditSales.Count
		If lintCount > 0 Then
			For lintIndex = 1 To lintCount
				With mcolCreditSales(lintIndex)
					mobjGrid.Columns("tcnConsec").DefValue = CStr(.nConsec)
					mobjGrid.Columns("tctClient").DefValue = sDebtCli
					mobjGrid.Columns("tcdDocdate").DefValue = mobjValues.TypeToString(.dDocdate, eFunctions.Values.eTypeData.etdDate)
					mobjGrid.Columns("tcdExpirdoc").DefValue = mobjValues.TypeToString(.dExpirdoc, eFunctions.Values.eTypeData.etdDate)
					mobjGrid.Columns("cbeDocType").DefValue = CStr(.nType)
					mobjGrid.Columns("tctNumDoc").DefValue = .sNumber
					mobjGrid.Columns("cbeCurrency").DefValue = CStr(.nCurrency)
					mobjGrid.Columns("cbeCountry").DefValue = CStr(.nCountry)
					mobjGrid.Columns("tcnAmount").DefValue = CStr(.nAmount)
					mobjGrid.Columns("btnNotenum").nNotenum = .nNotenum
					nConsec = .nConsec + 1
					mobjGrid.sEditRecordParam = "nConsec=" & lintCount + 1
				End With
				Response.Write(mobjGrid.DoRow())
			Next 
		End If
	End If
	
	mobjGrid.sEditRecordParam = "nConsec=" & nConsec
	Response.Write(mobjGrid.closeTable())
	mcolCreditSales = Nothing
	lclsGeneral = Nothing
End Sub

'% insPreCT002Upd: Se realiza el manejo de los campos del grid 
'--------------------------------------------------------------------------------------------
Private Sub insPreCT002Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjCreditSales As ePolicy.CreditSales
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lobjCreditSales = New ePolicy.CreditSales
			If lobjCreditSales.InsPostCT002(3, Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nConsec"), eFunctions.Values.eTypeData.etdDouble), Today, 0, CStr(0), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), Session("nUserCode"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				
			End If
			lobjCreditSales = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "CT002", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CT002")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
mcolCreditSales = New ePolicy.CreditSaless

mobjValues.ActionQuery = Session("bQuery")

sCertype = Session("sCertype")
nBranch = mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
nProduct = mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
nPolicy = mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
nCertif = mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
dEffecdate = mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)

'+Se realiza la busqueda del cliente - deudor

If mcolCreditSales.FindDebtCli(sCertype, nBranch, nProduct, nPolicy, nCertif, 80, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
	sDebtCli = mcolCreditSales.sDebtCli
End If
'+Se realiza la busqueda de la informacion de credito (plazo)

lclsCertificat = New ePolicy.Credit

If lclsCertificat.Find_CT001(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), 2) Then
	Response.Write("<SCRIPT>mintAge = " & lclsCertificat.nAge & ";</SCRIPT>")
End If

%>
<HTML>
<HEAD>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CT002", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

Response.Write(mobjValues.StyleSheet())
%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 16/09/09 12:08 $|$$Author: Cidler $"
//% ShowChangeValues: Se cargan los valores de acuerdo al auto que se seleccione 
//-------------------------------------------------------------------------------------------
function ShowChangeValues(field){
//-------------------------------------------------------------------------------------------
	var nLimit = 0
	var nRate = 0
	var nPercent = 0
	var nvalue = 0
	var lstrString;
	
	with (self.document.forms[0])
	{
		lstrString = "nAge=" + mintAge + "&dDate=" + field.value ;
		insDefValues("Expirdoc", lstrString, '/VTimeNet/Policy/PolicySeq');
	}

}    
    
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="frmCT002" ACTION="valPolicySeq.aspx?sMode=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("CT002", Request.QueryString.Item("sWindowDescript")))

lclsCurren_pol = New ePolicy.Curren_pol
If lclsCurren_pol.findCurrency(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) <> vbNullString Then
	nCurrency = lclsCurren_pol.nCurrency
End If
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCT002Upd()
Else
	Call insPreCT002()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>


<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.16
Call mobjNetFrameWork.FinishPage("CT002")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 11.58.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores 
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mdblTotalprem As Object
Dim mdblTotalcomm As Object
Dim mblnDisabled As Boolean

Dim mstrCertype As String
Dim mlngBranch As Object
Dim mlngProduct As Object
Dim mlngPolicy As Object
Dim mlngCertif As Object
Dim mlngContrat As Object
Dim lclsFinanceObj As eFinance.FinanceWin


'% insDefineHeader: Se definen los campos del grid 
'-------------------------------------------------------------------------------------------- 
Private Sub insDefineHeader()
	'-------------------------------------------------------------------------------------------- 
	'+ Se definen las columnas del grid 
	mobjGrid = New eFunctions.Grid
	mobjNetFrameWork.sSessionID = Session.SessionID
	mobjNetFrameWork.nUsercode = Session("nUsercode")
	Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
	
	mobjGrid.sCodisplPage = "fi002"
	
	If Request.QueryString.Item("Action") = "Add" Then
		mblnDisabled = False
	Else
		mblnDisabled = True
	End If
	
	With mobjGrid.Columns
		Call .AddHiddenColumn("cbeCompany", CStr(eRemoteDB.Constants.intNull))
		Call .AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"), "valProduct", CStr(eRemoteDB.Constants.intNull),  ,  ,  , mblnDisabled)
		Call .AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"), "cbeBranch", CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  , mblnDisabled)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnReceiptColumnToolTip"),  ,  ,  ,  , "insShowDefValues(this)", mblnDisabled)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnExchangeColumnCaption"), "tcnExchange", 11, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnExchangeColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddClientColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", vbNullString,  , GetLocalResourceObject("tctClientColumnToolTip"),  , True, "tctCliename")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPolicyColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddHiddenColumn("tcnAuxReceipt", CStr(0))
		Call .AddHiddenColumn("tcnIntermed", CStr(0))
		Call .AddHiddenColumn("tctProductDes", CStr(0))
		Call .AddHiddenColumn("tcnOffice", CStr(0))
		Call .AddHiddenColumn("tcdStartdate", CStr(eRemoteDB.Constants.dtmnull))
		Call .AddHiddenColumn("tcdExpirdat", CStr(eRemoteDB.Constants.dtmnull))
		Call .AddHiddenColumn("tcnCommission", CStr(0))
	End With
	
	'+ Se definen las propiedades generales del grid 
	With mobjGrid
		.Codispl = "FI002"
		.Top = 150
		.Height = 400
		.Width = 400
		.WidthDelete = 320
		.AddButton = False
		.Columns("Sel").Title = "Sel"
		
		.ActionQuery = Session("bQuery") Or CStr(Session("optType")) = "1"
		.Columns("Sel").GridVisible = Not (Session("bQuery") Or CStr(Session("optType")) = "1")
		.sDelRecordParam = "nReceipt=' + marrArray[lintIndex].tcnReceipt + '"
		
		If Session("bQuery") <> True Then
			.Columns("tcnReceipt").EditRecord = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreFI002: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreFI002()
	'--------------------------------------------------------------------------------------------
	Dim lclsFinancePre As eFinance.FinancePre
	Dim lcolFinancePres As eFinance.FinancePres
	Dim lstrHTMLRows As String
	
	lclsFinancePre = New eFinance.FinancePre
	lcolFinancePres = New eFinance.FinancePres
	
	If CStr(Session("optType")) = "1" Then
		Call lclsFinancePre.insPreLoadFI002("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), 0, Session("nContrat"), Session("dEffecdate"), Session("nUsercode"), 2)
	End If
	
	If lcolFinancePres.Find_DataReceipt(mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsFinancePre In lcolFinancePres
			With mobjGrid
				.Columns("cbeBranch").DefValue = CStr(lclsFinancePre.nBranch)
				.Columns("valProduct").DefValue = CStr(lclsFinancePre.nProduct)
				.Columns("tcnReceipt").DefValue = CStr(lclsFinancePre.nReceipt)
				.Columns("tcnPremium").DefValue = CStr(lclsFinancePre.nPremium)
				.Columns("cbeCurrency").DefValue = CStr(lclsFinancePre.nCurrency)
				.Columns("tcnExchange").DefValue = CStr(lclsFinancePre.nExchange)
				.Columns("tctClient").DefValue = lclsFinancePre.sClient
				.Columns("tcnPolicy").DefValue = CStr(lclsFinancePre.nPolicy)
				.Columns("tcnAuxReceipt").DefValue = CStr(lclsFinancePre.nReceipt)
				.Columns("tcnIntermed").DefValue = CStr(lclsFinancePre.nIntermed)
				.Columns("tctProductDes").DefValue = lclsFinancePre.sProduct
				.Columns("tcnOffice").DefValue = CStr(lclsFinancePre.nOffice)
				.Columns("tcdStartdate").DefValue = CStr(lclsFinancePre.dStartdate)
				.Columns("tcdExpirdat").DefValue = CStr(lclsFinancePre.dExpirdat)
				.Columns("tcnCommission").DefValue = CStr(lclsFinancePre.nCommission)
				
				mlngBranch = lclsFinancePre.nBranch
				mlngProduct = lclsFinancePre.nProduct
				mlngPolicy = lclsFinancePre.nPolicy
				
				lstrHTMLRows = lstrHTMLRows & .DoRow()
			End With
		Next lclsFinancePre
		
		mdblTotalprem = lcolFinancePres.nTotalAmount
		mdblTotalcomm = lcolFinancePres.nTotalCommision
	End If
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%""> " & vbCrLf)
Response.Write("        <TR><TD WIDTH=""25%""><LABEL>" & GetLocalResourceObject("cbeBranchDefCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("            <TD WIDTH=""25%"">")


Response.Write(mobjValues.BranchControl("cbeBranchDef", GetLocalResourceObject("cbeBranchDefToolTip"), mlngBranch, "valProductDef",  ,  ,  ,  , CStr(Session("optType")) = "1"))


Response.Write(" </TD> " & vbCrLf)
Response.Write("            <TD WIDTH=""25%""><LABEL>" & GetLocalResourceObject("valProductDefCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("            <TD WIDTH=""25%"">")


Response.Write(mobjValues.ProductControl("valProductDef", GetLocalResourceObject("valProductDefToolTip"), mlngBranch, eFunctions.Values.eValuesType.clngWindowType, CStr(Session("optType")) = "1", mlngProduct))


Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD WIDTH=""25%"">&nbsp;</TD> " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR><TD><LABEL>" & GetLocalResourceObject("tcnPolicyDefCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PolicyControl("tcnPolicyDef", GetLocalResourceObject("tcnPolicyDefToolTip"), "cbeBranchdef", mlngBranch, "valProduct", mlngProduct, "2", mlngPolicy, "tcnCertifDef", mlngCertif,  ,  ,  ,  , CStr(Session("optType")) = "1"))


Response.Write(" </TD> " & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("tcnCertifDefCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.ProductControl("tcnCertifDef", GetLocalResourceObject("tcnCertifDefToolTip"), mlngBranch, eFunctions.Values.eValuesType.clngWindowType, CStr(Session("optType")) = "1", mlngCertif))


Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD WIDTH=""25%"">")

	
	If Not mobjValues.ActionQuery And CStr(Session("optType")) = "2" Then
		Response.Write(mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/FindPolicyOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "InitialValues()"))
	End If
	
Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">         " & vbCrLf)
Response.Write("")

	
	Response.Write(lstrHTMLRows)
	Response.Write(mobjGrid.closeTable())
	
	
Response.Write("" & vbCrLf)
Response.Write("	</TABLE> " & vbCrLf)
Response.Write("	<BR></BR> " & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%""> " & vbCrLf)
Response.Write("        <TR><TD WIDTH=""25%""><LABEL>" & GetLocalResourceObject("tcnTotalpremCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("            <TD WIDTH=""25%"">")

	Response.Write(mobjValues.HiddenControl("tcnTotalAmount", mdblTotalprem))
	Response.Write(mobjValues.NumericControl("tcnTotalprem", 18, mdblTotalprem,  ,  , True, 6, True))
	
Response.Write(" " & vbCrLf)
Response.Write("			</TD> " & vbCrLf)
Response.Write("            <TD WIDTH=""25%""><LABEL>" & GetLocalResourceObject("tcnTotalcommCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("            <TD WIDTH=""25%"">")


Response.Write(mobjValues.NumericControl("tcnTotalcomm", 18, mdblTotalcomm,  ,  , True, 6, True))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR> " & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    </TABLE> ")

	
	lclsFinancePre = Nothing
	lcolFinancePres = Nothing
End Sub

'% insPreFI002Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreFI002Upd()
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lclsFinancePre As eFinance.FinancePre
	lclsFinancePre = New eFinance.FinancePre
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lblnPost = lclsFinancePre.insPostFI002Upd(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), Session("nContrat"), CDbl(.QueryString.Item("nReceipt")), eRemoteDB.Constants.intNull, vbNullString, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, vbNullString, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull)
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valFinanceSeq.aspx", "FI002", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index")), "1"))
		
		If lblnPost Then
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Finance/Financeseq/Sequence.aspx?nAction=0" & """;</" & "Script>")
		End If
	End With
	
	lclsFinancePre = Nothing
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "fi002"

mstrCertype = Request.QueryString.Item("sCertype")
mlngBranch = Session("nBranch")
mlngProduct = Session("nProduct")
mlngPolicy = Session("nPolicy")
mlngCertif = 0

%> 
<HTML> 
<HEAD> 
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 29/09/04 17:21 $|$$Author: Nvaplat40 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT> 
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0"> 

 

 
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("FI002"))
	.Write(mobjValues.ShowWindowsName("FI002"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		mobjNetFrameWork.sSessionID = Session.SessionID
		mobjNetFrameWork.nUsercode = Session("nUsercode")
		Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "FI002", "FI002.aspx"))
		mobjMenu = Nothing
	End If
End With%> 
<SCRIPT> 
//% InitialValues: se inicializa el grid de la transacción, 
//% con los datos definidos en el diseñador
//--------------------------------------------------------------------------------------------
function InitialValues(Field){
//--------------------------------------------------------------------------------------------
	var lstrQuery

	with (document.forms[0]) {
		lstrQuery = "sCertype=2&nBranch=" + cbeBranchDef.value  + 
		                     "&nProduct=" + valProductDef.value + 
		                     "&nPolicy=" + tcnPolicyDef.value + 
		                     "&nCertif=" + tcnCertifDef.value ;
    	insDefValues("ReceiptPolicy", lstrQuery)
	}
}

//%insShowDefValues:Se asignan a las columnas del grid los valores leídos de Premium 
//----------------------------------------------------------------- 
function insShowDefValues(Field){ 
//----------------------------------------------------------------- 
	with(self.document.forms[0]) 
		insDefValues("Receipt","nReceipt=" + Field.value + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value)
} 
</SCRIPT>     
</HEAD> 
<BODY ONUNLOAD="closeWindows();"> 
<FORM METHOD="post" ID="FORM" NAME="frmFI002" ACTION="valFinanceSeq.aspx?mode=2"> 
<%
Call insDefineHeader()

'+Se deja requerida la ventana de FI003-Contratos a refinanciar 
lclsFinanceObj = New eFinance.FinanceWin
Call lclsFinanceObj.Add_Finan_win(Session("nContrat"), Session("dEffecdate"), "FI003", "3", Session("nUsercode"), Session("nTransaction"))
lclsFinanceObj = Nothing

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreFI002()
Else
	Call insPreFI002Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%> 
</FORM> 
</HTML> 
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 11.58.23
Call mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>






<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.21
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctModulecColumnCaption"), "tctModulec", 30, vbNullString)
		Call .AddClientColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", vbNullString)
		Call .AddTextColumn(0, GetLocalResourceObject("tctCoverColumnCaption"), "tctCover", 30, vbNullString)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMovementColumnCaption"), "tcnMovement", 10, vbNullString)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, vbNullString,  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, vbNullString,  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPreBasPayColumnCaption"), "tcnPreBasPay", 18, vbNullString,  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommBasPayColumnCaption"), "tcnCommBasPay", 18, vbNullString,  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPreExPayColumnCaption"), "tcnPreExPay", 18, vbNullString,  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommExPayColumnCaption"), "tcnCommExPay", 18, vbNullString,  ,  , True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "Codispl"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 380
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCodispl: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVAC610()
	'--------------------------------------------------------------------------------------------
	Dim lclsMove_Accpol As ePolicy.Move_accpol
	Dim llngCount As Double
	Dim llngItem As Double
	
	lclsMove_Accpol = New ePolicy.Move_accpol
	
	'+ Se cargan datos de llave de busqueda
	With lclsMove_Accpol
		.sCertype = Request.QueryString.Item("sCertype")
		.nBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
		.nProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
		.nPolicy = mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
		.nCertif = mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)
		.nIdmov = mobjValues.StringToType(Request.QueryString.Item("nMovement"), eFunctions.Values.eTypeData.etdDouble)
	End With
	
	If lclsMove_Accpol.FindDetail() Then
		llngCount = lclsMove_Accpol.CountDetails
		llngItem = 1
		While llngItem <= llngCount
			Call lclsMove_Accpol.DetailItem(llngItem)
			
			With mobjGrid
				.Columns("tctModulec").DefValue = CStr(lclsMove_Accpol.nModulec)
				.Columns("tctClient").DefValue = lclsMove_Accpol.sClient
				.Columns("tctCover").DefValue = CStr(lclsMove_Accpol.nCover)
				.Columns("tcnMovement").DefValue = CStr(lclsMove_Accpol.nMov)
				.Columns("tctCover").DefValue = lclsMove_Accpol.sCover
				.Columns("tcnCapital").DefValue = CStr(lclsMove_Accpol.nCapital)
				.Columns("tcnPremium").DefValue = CStr(lclsMove_Accpol.nPremium)
				.Columns("tcnPreBasPay").DefValue = CStr(lclsMove_Accpol.nPrebaspay)
				.Columns("tcnCommBasPay").DefValue = CStr(lclsMove_Accpol.nCommbaspay)
				.Columns("tcnPreExPay").DefValue = CStr(lclsMove_Accpol.nPreexpay)
				.Columns("tcnCommExPay").DefValue = CStr(lclsMove_Accpol.nCommexpay)
				llngItem = llngItem + 1
				Response.Write(.DoRow)
			End With
		End While
	End If
	lclsMove_Accpol = Nothing
	
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnCertif.value='" & Request.QueryString.Item("nCertif") & "';</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.UpdateDiv('divCurrency', '" & Request.QueryString.Item("sDivCurrency") & "');</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.UpdateDiv('divAmount', '" & Request.QueryString.Item("nDivAmount") & "');</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.UpdateDiv('divReceipt', '" & Request.QueryString.Item("nDivReceipt") & "');</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.UpdateDiv('divMoveDate', '" & Request.QueryString.Item("dMoveDate") & "');</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.UpdateDiv('divMoveType', '" & Request.QueryString.Item("sMoveType") & "');</" & "Script>")
	
	Response.Write(mobjGrid.closeTable())
	
End Sub

'% insPreCodisplUpd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVAC610Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjMove_accpol As ePolicy.Move_accpol
	
	lobjMove_accpol = New ePolicy.Move_accpol
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			<%--If lobjMove_accpol.insPostVAC610() Then
			End If--%>
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicyQue.aspx", "VAC610", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjMove_accpol = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vac610")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vac610"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = True 'Request.QueryString("nMainAction") = 401
%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:37 $"
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VAC610", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VAC610" ACTION="ValPolicyQue.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("VAC610", Request.QueryString.Item("sWindowDescript")))

mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "vac610"
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreVAC610Upd()
Else
	Call insPreVAC610()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.21
Call mobjNetFrameWork.FinishPage("vac610")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





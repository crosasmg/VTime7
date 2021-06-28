<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'*++ Modificar nombre del objeto. Modificar "Class" por el nombre de la clase con la cual se trabaja
'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "coc009"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnTransacColumnCaption"), "tcnTransac", 5, vbNullString)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeColumnCaption"), "cbeType", "Table6", eFunctions.Values.eValuesType.clngComboType, CStr(0))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdStatdateColumnCaption"), "tcdStatdate", vbNullString,  , GetLocalResourceObject("tcdStatdateColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, vbNullString,  ,  , True, 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbePay_formColumnCaption"), "cbePay_form", "Table182", eFunctions.Values.eValuesType.clngComboType, CStr(0))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnBordereauxColumnCaption"), "tcnBordereaux", 10, vbNullString)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcninit_moraColumnCaption"), "tcninit_mora", 18, vbNullString,  ,  , True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "COC009"
		.ActionQuery = mobjValues.ActionQuery
		'.Columns("CampoX").EditRecord = True
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		' .Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCOC009: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCOC009()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium_mo As eCollection.Premium_mo
	Dim lcolPremium_mos As eCollection.Premium_mos
	
	lclsPremium_mo = New eCollection.Premium_mo
	lcolPremium_mos = New eCollection.Premium_mos
	
	If lcolPremium_mos.FindCOC009(mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCertype"), mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsPremium_mo In lcolPremium_mos
			With mobjGrid
				.Columns("tcnTransac").DefValue = CStr(lclsPremium_mo.nTransac)
				.Columns("cbeType").DefValue = CStr(lclsPremium_mo.nType)
				.Columns("tcdStatdate").DefValue = CStr(lclsPremium_mo.dStatdate)
				.Columns("tcnPremium").DefValue = CStr(lclsPremium_mo.nPremium)
				.Columns("cbePay_form").DefValue = lclsPremium_mo.sPay_form
				.Columns("tcnBordereaux").DefValue = CStr(lclsPremium_mo.nBordereaux)
				.Columns("tcninit_mora").DefValue = CStr(lclsPremium_mo.nInt_mora)
				Response.Write(.DoRow)
			End With
		Next lclsPremium_mo
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("coc009")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "coc009"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>

	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
	</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "COC009", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="fraContent" ACTION="ValCollectionQue.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("COC009", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
Call insPreCOC009()
%>
</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("coc009")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues



'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "AGC621"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valIntermediaColumnCaption"), "valIntermedia", "Intermedia", 1, CStr(2),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valIntermediaColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPay_CommColumnCaption"), "tcnPay_Comm", 10, "", False, "", False, 0,  ,  ,  , False)
		Call .AddAnimatedColumn(0, "", "sLink", "/VTimeNet/Images/lupa.bmp", "")
	End With
	
	With mobjGrid
		.Codispl = "AGC621"
		.Codisp = "AGC621"
		.Top = 100
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sReloadAction = Request.QueryString.Item("ReloadAction")
		.sReloadIndex = Request.QueryString.Item("ReloadIndex")
	End With
End Sub

'%insPreAGC621. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreAGC621()
	'------------------------------------------------------------------------------
	Dim lcolpay_comms As eAgent.pay_comms
	Dim lclspay_comm As Object
	Dim lnTotal As String
	Dim lnIntermed As Long
	
	lcolpay_comms = New eAgent.pay_comms
	lnIntermed = 0
	
	If lcolpay_comms.find(mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dEffecdateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nPay_comm"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclspay_comm In lcolpay_comms
			With lclspay_comm
				If lnIntermed <> .nIntermed Then
					mobjGrid.Columns("valIntermedia").DefValue = .nIntermed
					mobjGrid.Columns("tcnPay_Comm").DefValue = .nPay_Comm
					mobjGrid.Columns("sLink").HRefScript = "ShowPopUp('AGC621A.aspx?nIntermed=" & .nIntermed & "&nPay_Comm=" & .nPay_Comm & "&nTotal=" & lnTotal & "','AGC621A',750,500,'yes','no',20,20,'no');"
					Response.Write(mobjGrid.DoRow())
				End If
				lnIntermed = .nIntermed
			End With
		Next lclspay_comm
	End If
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lclspay_comm = Nothing
	lcolpay_comms = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AGC621")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "AGC621"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = "401")
%>
<HTML>
<HEAD>
<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "AGC621", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	End If
	mobjMenu = Nothing
	.Write(mobjValues.ShowWindowsName("AGC621", Request.QueryString.Item("sWindowDescript")))
End With

%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
</SCRIPT>    	
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmAGC621" ACTION="ValAgent.aspx?sZone=2">
<%
Call insDefineHeader()
Call insPreAGC621()

mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("AGC621")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
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
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "vi806"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, vbNullString)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10, vbNullString)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdDateFacColumnCaption"), "tcdDateFac")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPayfreqColumnCaption"), "tcnPayfreq", 5, vbNullString)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 10, vbNullString)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, vbNullString,  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCostColumnCaption"), "tcnCost", 9, vbNullString,  ,  ,  , 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 9, vbNullString,  ,  ,  , 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, vbNullString,  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapital_TotColumnCaption"), "tcnCapital_Tot", 18, vbNullString,  ,  , True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "VI806"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnPolicy").EditRecord = False
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
		'        If Request.QueryString("Reload") = "1" then
		'	        .sReloadIndex = Request.QueryString("ReloadIndex")
		'	    End If
	End With
End Sub

'% insPreVI806: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVI806()
	'--------------------------------------------------------------------------------------------
	Dim lclsClass As Object
	Dim mcolClass As ePolicy.TMovprev_Capitals
	Dim sKey As String
	mcolClass = New ePolicy.TMovprev_Capitals
	' AGREGAR USUARIO A LA CONSULTA     
	If CStr(Session("BatchEnabled")) <> "1" Then
		sKey = Session("SESSIONID")
	Else
		sKey = Session("sKey")
	End If
	
	If mcolClass.Find_policy(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), sKey, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write(mobjValues.HiddenControl("hddFindData", "1"))
		For	Each lclsClass In mcolClass
			With mobjGrid
				.Columns("tcnPolicy").DefValue = lclsClass.nPolicy
				.Columns("tcnCertif").DefValue = lclsClass.nCertif
				.Columns("tcdDateFac").DefValue = lclsClass.dEffecdate
				.Columns("tcnPayfreq").DefValue = lclsClass.nPayfreq
				.Columns("tcnReceipt").DefValue = lclsClass.nReceipt
				.Columns("tcnPremium").DefValue = lclsClass.nPremium
				.Columns("tcnCost").DefValue = lclsClass.nCost
				.Columns("tcnPercent").DefValue = lclsClass.nPercent
				.Columns("tcnCapital").DefValue = lclsClass.nCapital
				.Columns("tcnCapital_Tot").DefValue = lclsClass.nCapitaltot
				Response.Write(.DoRow)
			End With
		Next lclsClass
	Else
		Response.Write(mobjValues.HiddenControl("hddFindData", "0"))
	End If
	
	Response.Write(mobjGrid.closeTable())
	mcolClass = Nothing
End Sub

'% insPreVI806Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVI806Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjClass As Object
	
'UPGRADE_NOTE: The 'eDll.Class' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lobjClass = Server.CreateObject("eDll.Class")
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjClass.insPostVI806() Then
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicyTra.aspx", "VI806", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjClass = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vi806")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vi806"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VI806", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 30/12/03 11:46 $|$$Author: Nvaplat26 $"
</SCRIPT>        
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VI806" ACTION="ValPolicyTra.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("VI806", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreVI806Upd()
Else
	Call insPreVI806()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("vi806")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGeneral As eGeneral.GeneralFunction

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
	
	mobjGrid.sCodisplPage = "MVI8015"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("tcnvp_iniColumnCaption"), "tcnvp_ini", 18,  ,  , GetLocalResourceObject("tcnvp_iniColumnToolTip"), True, 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("tcnvp_endColumnCaption"), "tcnvp_end", 18,  ,  , GetLocalResourceObject("tcnvp_endColumnToolTip"), True, 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("tcndisc_perc_vpColumnCaption"), "tcndisc_perc_vp", 9,  ,  , GetLocalResourceObject("tcndisc_perc_vpColumnToolTip"), True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI8015"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 250
		.Width = 350
		.bCheckVisible = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("tcnvp_ini").EditRecord = True
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		.sDelRecordParam = .sEditRecordParam & "&nvp_ini='+ marrArray[lintIndex].tcnvp_ini + '" & "&nvp_end='+ marrArray[lintIndex].tcnvp_end + '"
	End With
End Sub

'% insPreMVI8015: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI8015()
	'--------------------------------------------------------------------------------------------
	Dim lclsClass As Object
	Dim mcolClass As eProduct.Perc_DiscVPs
	
	mcolClass = New eProduct.Perc_DiscVPs
	
	If mcolClass.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsClass In mcolClass
			With mobjGrid
				.Columns("tcnvp_ini").DefValue = lclsClass.nvp_ini
				.Columns("tcnvp_end").DefValue = lclsClass.nvp_end
				.Columns("tcndisc_perc_vp").DefValue = lclsClass.ndisc_perc_vp
				Response.Write(.DoRow)
			End With
		Next lclsClass
	End If
	
	Response.Write(mobjGrid.closeTable())
	mcolClass = Nothing
End Sub

'% insPreMVI8015Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI8015Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjClass As eProduct.Perc_DiscVP
	
	lobjClass = New eProduct.Perc_DiscVP
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lobjClass.insPostMVI8015(.QueryString.Item("Action"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nvp_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nvp_end"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicyTra.aspx", "MVI8015", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjClass = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("MVI8015")

mobjValues = New eFunctions.Values
mobjGeneral = New eGeneral.GeneralFunction

'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "MVI8015"
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
	Response.Write(mobjMenu.setZone(2, "MVI8015", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
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
<FORM METHOD="POST" NAME="MVI8015" ACTION="ValPolicyTra.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("MVI8015", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI8015Upd()
Else
	Call insPreMVI8015()
End If

mobjGrid = Nothing
mobjValues = Nothing
mobjGeneral = Nothing
%>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("MVI8015")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




